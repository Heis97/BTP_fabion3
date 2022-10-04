using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTP
{
    class ChanelOct
    {
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
        public int ACSC_POSITIVE_DIRECTION { get; internal set; }
        public object ACSC_AMF_RELATIVE { get; internal set; }

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

        public void RunBuffer(int Axis)
        {
        }

        internal double ReadVariable(string v1, int aCSC_NONE, int v2, int v3)
        {
            throw new NotImplementedException();
        }

        internal object GetFault(int x)
        {
            throw new NotImplementedException();
        }

        internal void OpenCommDirect()
        {
            throw new NotImplementedException();
        }

        internal void OpenCommEthernet(string controllerIP, object aCSC_SOCKET_STREAM_PORT)
        {
            throw new NotImplementedException();
        }

        internal void EnableM(int[] axes)
        {
            throw new NotImplementedException();
        }

        internal void OpenMessageBuffer(int v)
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

        internal void JogM(object aCSC_AMF_VELOCITY, int[] axes, int[] directions, double setXYVel)
        {
            throw new NotImplementedException();
        }

        internal void Jog(object aCSC_AMF_VELOCITY, int aCSC_AXIS_1, double v)
        {
            throw new NotImplementedException();
        }

        internal void Kill(int aCSC_AXIS_0)
        {
            throw new NotImplementedException();
        }

        internal void ToPoint(object aCSC_AMF_RELATIVE, int aCSC_AXIS_1, int v)
        {
            throw new NotImplementedException();
        }
        internal void ToPoint(object aCSC_AMF_RELATIVE, int aCSC_AXIS_1, double v)
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
