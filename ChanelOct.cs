using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connection;
using System.Threading;

namespace BTP
{
    class ChanelOct
    {
        public DeviceMarlin device;
        public int ACSC_BUFFER_0 { get; set; }
        public int ACSC_BUFFER_1 { get; set; }
        public int ACSC_BUFFER_2 { get; set; }
        public int ACSC_BUFFER_3 { get; set; }
        public int ACSC_BUFFER_4 { get; set; }
        public int ACSC_BUFFER_5 { get; set; }
        public int ACSC_BUFFER_6 { get; set; }
        public int ACSC_BUFFER_7 { get; set; }
        public int ACSC_BUFFER_8 { get; set; }
        public int ACSC_BUFFER_9 { get; set; }
        public int ACSC_AXIS_0 { get; set; }
        public int ACSC_AXIS_1 { get; set; }
        public int ACSC_AXIS_2 { get; set; }
        public int ACSC_AXIS_3 { get; set; }
        public int ACSC_AXIS_4 { get; set; }
        public int ACSC_AXIS_5 { get; set; }
        public int ACSC_AXIS_6 { get; set; }
        public int ACSC_AXIS_7 { get; set; }
        public int ACSC_AXIS_8 { get; set; }
        public int ACSC_AXIS_9 { get; set; }
        public int ACSC_AXIS_10 { get; set; }
        public int ACSC_AXIS_11 { get; set; }
        public int ACSC_AXIS_12 { get; set; }
        public int ACSC_AXIS_13 { get; set; }
        public int ACSC_AXIS_14 { get; set; }
        public int ACSC_AXIS_15 { get; set; }
        public int ACSC_AXIS_16 { get; set; }
        public int ACSC_NONE { get; internal set; }
        public object ACSC_SAFETY_LL { get; internal set; }
        public object ACSC_SAFETY_RL { get; internal set; }
        public int ACSC_MST_MOVE { get; internal set; }
        public object ACSC_SOCKET_STREAM_PORT { get; internal set; }
        public object ACSC_AMF_VELOCITY { get; internal set; }
        public object ACSC_PST_RUN { get; internal set; }
        public object ACSC_PST_COMPILED { get; internal set; }
        public int ACSC_NEGATIVE_DIRECTION { get; internal set; }
        public int ACSC_POSITIVE_DIRECTION  { get; internal set; }
        public object ACSC_AMF_RELATIVE { get; internal set; }

        public double xy_vel;
        public double z_vel;
        public double e_vel;
        public ChanelOct()
        {
            ACSC_AMF_RELATIVE = 1;
            device = new DeviceMarlin(ConnectionData.Comport);
            xy_vel = 600;
            z_vel = 300;
            e_vel = 60;
        }
        public void HomeAll()
        {
            device.sendCommand("G28");
        }

        string buf_name= "buf2.gco";
        public string[] prog;
        public bool prog_loaded;
        public void upload_program(string[] prog)
        {
            if (prog == null) return;
            stopAutoPos();
            device.sendCommand("M27 S0");
            device.sendCommand("M30 " + buf_name);
            Thread.Sleep(300);
            device.sendCommand("M28 " + buf_name);
            Thread.Sleep(200);
            size_program(prog);
            this.prog = (string[])prog.Clone();
            foreach (var line in prog) device.sendCommand(line);
            Thread.Sleep(100);
            device.sendCommand("M29");
            startAutoPos();
        }
        
        int[] cur_line;
        public void size_program(string[] prog)
        {
            int size = 0;
            for(int i=0; i<prog.Length;i++)
            {
                for (int j = 0; j < prog[i].Length; j++)
                {
                    size ++;
                }
                size += 2;
            }
            cur_line = new int[size];
            size = 0;
            for (int i = 0; i < prog.Length; i++)
            {
                for (int j = 0; j < prog[i].Length; j++)
                {
                    
                    cur_line[size] = i;
                    size++;
                }
                
                cur_line[size] = i;
                size++;
                
                cur_line[size] = i;
                size++;
            }

            Console.WriteLine(size);
        }

        public int get_cur_line(int cur_byte)
        {

            if (cur_line != null)
                if (cur_byte < cur_line.Length)
                    return cur_line[cur_byte];

            return 0;
        }

        public void start_program()
        {
            device.sendCommand("M23 " + buf_name);
            device.sendCommand("M24");
            device.sendCommand("M27 S100");
        }

        public void pause_program()
        {
           // device.sendCommand("M410");
            device.sendCommand("M25");
            
        }


        public void HomeDisp(int num)
        {
            device.sendCommand("G28 "+axis_from_num(num));
        }
        public void calibrateDisp()
        {
            device.sendCommand("G25");
        }
        public void startAutoPos()
        {
            device.sendCommand("M154 S25");
          //  device.sendCommand("M154 S1000");
        }
        public void stopAutoPos()
        {
            device.sendCommand("M154 S0");
            //  device.sendCommand("M154 S1000");
        }
        public void enableExtrud()
        {
            device.sendCommand("M302 S0");
        }
        public void zeroDisp(int num)
        {
            device.sendCommand("G92", new string[] { "X","Y", axis_from_num(num) }, new object[] { 0,0,0 });
        }

        public int active_disp = 0;
        internal void setActiveDisp(int num)
        {
            active_disp = num;
            device.sendCommand("M302", new string[] { "S"}, new object[] {0 });
            device.sendCommand("", new string[] {"T" }, new object[] { num});
        }
        internal void ToPoint(object move_type, int Axis, int v)
        {
            device.sendCommand("G91");
            device.sendCommand("G1", new string[] { axis_from_num(Axis),"F" }, new object[] { v, vel_from_num(Axis) });
        }
        internal void ToPoint(object move_type, int Axis, double v)
        {
            device.sendCommand("G91");
            device.sendCommand("G1", new string[] { axis_from_num(Axis), "F" }, new object[] { v, vel_from_num(Axis) });
        }

        internal void JogM(object aCSC_AMF_VELOCITY, int[] axes, int[] directions, double setXYVel)
        {
            //throw new NotImplementedException();
        }
        //
        internal void Jog(int Axis, double veloc)
        {
            string sign = "";
            if (veloc < 0) sign = "-";
            device.sendCommand("G1", new string[] { axis_from_num(Axis), "F" }, new object[] { sign+ "0.1", vel_from_num(Axis) });
        }
        //остановка всех осей
        internal void Kill(int aCSC_AXIS_0)
        {
            device.sendCommand("M410");
            //device.sendCommand("M999");
        }
        //включение переферии
        internal void fan_on( int num)
        {
            device.sendCommand("M106", new string[] { "P"}, new object[] {num });
        }
        //выключение переферии
        internal void fan_off(int num)
        {
            device.sendCommand("M107", new string[] { "P" }, new object[] { num });
        }
        double vel_from_num(int axis)
        {
            double vel = 0;
            switch (axis)
            {
                case 0: vel = xy_vel; break;
                case 1: vel = xy_vel; break;
                case 4: vel = z_vel; break;
                case 2: vel = z_vel; break;
                case 6: vel = z_vel; break;
                case 7: vel = e_vel; break;
            }
            return vel;
        }
        //вывод номера оси в зависимости от символьного обозначения
        string axis_from_num(int axis)
        {
            string ax = "";
            switch (axis)
            {
                case 0: ax = "X"; break;
                case 1: ax = "Y"; break;
                case 4: ax = "Z"; break;
                case 2: ax = "A"; break;
                case 6: ax = "B"; break;
                case 7: ax = "E"; break;
            }
            return ax;
        }
        public void SetVelocity(int Axis, double Value)
        {
            
        }

        public void WriteVariable(object Value,string Variable,int nBuf)
        {

        }

        public dynamic ReadVariable(string Variable, int nBuf)
        {
            return null;
        }

        public double GetFPosition(int Axis)
        {
            return 0;
        }

        public int GetMotorState(int Axis)
        {
            return 0;
        }

        public void RunBuffer(int Buffer)
        {

        }

        internal double ReadVariable(string v1, int aCSC_NONE, int v2, int v3)
        {
            throw new NotImplementedException();
        }

        internal void KillAll()
        {
            throw new NotImplementedException();
        }

        internal void StopBuffer(int aCSC_NONE)
        {
            throw new NotImplementedException();
        }

        internal void ExtToPointM(object aCSC_AMF_VELOCITY, int[] ax, double[] saveCoordinates, object value1, object value2)
        {
            throw new NotImplementedException();
        }

        internal void EndSequenceM(int[] ax)
        {
            throw new NotImplementedException();
        }

        internal void WaitMotionEnd(int aCSC_AXIS_0, int v)
        {
            throw new NotImplementedException();
        }

        internal object GetProgramState(int programm_buffer)
        {
            throw new NotImplementedException();
        }

        internal object GetVelocity(int aCSC_AXIS_0)
        {
            throw new NotImplementedException();
        }

        internal void HaltM(int[] axes)
        {
            throw new NotImplementedException();
        }

        internal void SuspendBuffer(int programm_buffer)
        {
            throw new NotImplementedException();
        }

        internal void ClearBuffer(int programm_buffer, int v1, int v2)
        {
            throw new NotImplementedException();
        }

        internal void CloseMessageBuffer()
        {
            throw new NotImplementedException();
        }

        internal object GetFVelocity(int aCSC_AXIS_0)
        {
            throw new NotImplementedException();
        }

        internal void DisableAll()
        {
            throw new NotImplementedException();
        }

        internal void CancelOperation()
        {
            throw new NotImplementedException();
        }

        internal void CloseComm()
        {
            throw new NotImplementedException();
        }

        internal object UploadBuffer(int aCSC_BUFFER_2)
        {
            throw new NotImplementedException();
        }

        internal void SetVelocityImm(int aCSC_AXIS_0, double v)
        {
            throw new NotImplementedException();
        }

        internal void LoadBuffer(int v, string acs_code)
        {
            throw new NotImplementedException();
        }

        internal void CompileBuffer(int aCSC_BUFFER_1)
        {
            throw new NotImplementedException();
        }

        internal void KillM(int[] axes)
        {
            throw new NotImplementedException();
        }

        

        
        internal void LoadBuffersFromFile(string macrofile)
        {
            throw new NotImplementedException();
        }

        internal void AppendBuffer(int aCSC_BUFFER_1, string v)
        {
            throw new NotImplementedException();
        }

        internal void ToPointM(int v, int[] axes, double[] point)
        {
            throw new NotImplementedException();
        }

        internal long GetMotorError(int p)
        {
            throw new NotImplementedException();
        }

        internal long GetMotionError(int p)
        {
            throw new NotImplementedException();
        }

        internal long GetProgramError(int p)
        {
            throw new NotImplementedException();
        }

        internal string GetErrorString(int v)
        {
            throw new NotImplementedException();
        }

        internal string GetSingleMessage(int v)
        {
            throw new NotImplementedException();
        }

        internal void ExtToPoint(object aCSC_AMF_RELATIVE, int aCSC_AXIS_13, double v, double setCTVel1, double setCTVel2)
        {
            throw new NotImplementedException();
        }

        internal void SetRPosition(int aCSC_AXIS_10, double camera2XStrokeMax)
        {
            throw new NotImplementedException();
        }
    }
    
}
