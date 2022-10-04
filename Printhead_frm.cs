using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using Active_Directory_Worker.Interfaces;
using System.Runtime.InteropServices;


namespace BTP
{

    public partial class Printhead_frm : Form
    {
        private void button8_Click(object sender, EventArgs e)
        {
            Param_save(
                trackBar1.Value,
                trackBar2.Value,
                textBox7.Text,
                textBox8.Text,
                textBox9.Text,
                textBox10.Text,
                textBox1.Text,
                textBox2.Text,
                textBox3.Text,
                textBox4.Text,
                textBox5.Text,
                textBox6.Text
                );
        }
        static void Param_save(
            int _focus,
            int _zoom,
            string _obr1,
            string _obr2,
            string _obr3,
            string _obr4,
            string _Ard1,
            string _Ard2,
            string _Ard3,
            string _Ard4,
            string _Ard5,
            string _Ard6
            )
        {
            ConnectionData.focus = _focus;
            ConnectionData.zoom = _zoom;
            ConnectionData.obr1 = _obr1;
            ConnectionData.obr2 = _obr2;
            ConnectionData.obr3 = _obr3;
            ConnectionData.obr4 = _obr4;
            ConnectionData.Ard1 = _Ard1;
            ConnectionData.Ard2 = _Ard2;
            ConnectionData.Ard3 = _Ard3;
            ConnectionData.Ard4 = _Ard4;
            ConnectionData.Ard5 = _Ard5;
            ConnectionData.Ard6 = _Ard6;
        }

        public delegate void delegate_gui();
        public delegate_gui delegate_Gui;
        
        int[,] Mes = new int[10, 3];
        int val = 0;
        int var = 0;
        int len_t = 0;
        int mes_out = 1;
        bool isConnected = false;
        bool isOn_nas = false;
        bool isDown_piston = false;
        private uEye.Camera Camera;
        Color def_c;      
        //Rectangle cloneRect = new Rectangle(0, 0, 1280, 720);
        //System.Drawing.Imaging.PixelFormat format = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
        Bitmap MyBitmap;
        void change_gui()
        {

            switch (mes_out)
            {
                case 1:
                    label16.BackColor = Color.Violet;
                    label17.BackColor = def_c;
                    label18.BackColor = def_c;
                    break;
                case 2:
                    label16.BackColor = def_c;
                    label17.BackColor = Color.Violet;
                    label18.BackColor = def_c;
                    break;
                case 3:
                    label16.BackColor = def_c;
                    label17.BackColor = def_c;
                    label18.BackColor = Color.Violet;
                    break;
                case 4:
                    label21.BackColor = Color.Violet;
                    label20.BackColor = def_c;
                    label19.BackColor = def_c;
                    break;
                case 5:
                    label21.BackColor = def_c;
                    label20.BackColor = Color.Violet;
                    label19.BackColor = def_c;
                    break;
                case 6:
                    label21.BackColor = def_c;
                    label20.BackColor = def_c;
                    label19.BackColor = Color.Violet;
                    break;
                case 7:
                    label25.BackColor = Color.Violet;
                    label24.BackColor = def_c;
                    break;
                case 8:
                    label25.BackColor = def_c;
                    label24.BackColor = Color.Violet;
                    break;
                case 10:
                    label29.BackColor = Color.Violet;
                    label28.BackColor = def_c;
                    break;
                case 11:
                    label29.BackColor = def_c;
                    label28.BackColor = Color.Violet;
                    break;
            }


        }
        public void Gui_load_param()
        {
            textBox1.Text = ConnectionData.Ard1;
            textBox2.Text = ConnectionData.Ard2;
            textBox3.Text = ConnectionData.Ard3;
            textBox4.Text = ConnectionData.Ard4;
            textBox5.Text = ConnectionData.Ard5;
            textBox6.Text = ConnectionData.Ard6;

            textBox7.Text = ConnectionData.obr1;
            textBox8.Text = ConnectionData.obr2;
            textBox9.Text = ConnectionData.obr3;
            textBox10.Text = ConnectionData.obr4;

            trackBar2.Value = ConnectionData.zoom;
            trackBar1.Value = ConnectionData.focus;
            
        }
        public Printhead_frm()
        {
            CallBackMy6.callbackEventHandler = new CallBackMy6.callbackEvent(this.Reload);
            InitializeComponent();
            
            try
            {
                Gui_load_param();
            }
            catch
            {

            }
            def_c = label16.BackColor;
            delegate_Gui = new delegate_gui(change_gui);
            this.serialPort.ReadTimeout = 10;
            InitCamera();
            
        }
       /* private void addtask(int in1, int in2)
        {
            if (isConnected == true)
            {
                try
                {
                    Mes[len_t, 0] = in1;
                    Mes[len_t, 1] = in2;
                    Mes[len_t, 2] = 1;
                    len_t++;
                }
                catch
                {
                    len_t = 0;
                }
            }
        }*/

        private void addtask(int in1, int in2)
        {
            if (isConnected == true)
            {
                try
                {
                    val = in1;
                    var = in2;            
                }
                catch
                {
                }
            }
        }
        private void Prog_stat()
        {
            addtask(0, 7);
        }
        private void InitCamera()
        {
            try {
                Camera = new uEye.Camera();
                Camera.Init();
                //System.Diagnostics.Debug.WriteLine("my string");
                Camera.PixelFormat.Set(uEye.Defines.ColorMode.BGR8Packed);
                Camera.Memory.Allocate();
                Camera.Acquisition.Capture();
                Camera.Focus.Auto.SetEnable(false);
                Camera.EventFrame += onFrameEvent;
                pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            }
            catch
            {

            }
            

        }
        private void onFrameEvent(object sender, EventArgs e)
        {
            if (MyBitmap != null)
            {

                MyBitmap.Dispose();
            }
            try
            {
                uEye.Camera Camera = sender as uEye.Camera;
                Camera.Memory.ToBitmap(1, out MyBitmap);
                MyBitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                pictureBox1.Image = MyBitmap;
            }
            catch
            {

            }
               

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void changeZoom(object sender, EventArgs e)
        {
            var _z1 = Convert.ToDouble(trackBar1.Value);
            var _zoom = _z1 / 100;
            try
            {
                Camera.Zoom.Set(_zoom);
            }
            catch 
            {
                Console.WriteLine("except zoom");
            }
        }
        private void changeFocus(object sender, EventArgs e)
        {
            try
            {
                var _focus = Convert.ToUInt32(trackBar2.Value);
                Camera.Focus.Manual.Set(_focus);
            }
            catch 
            {
                Console.WriteLine("except focus");
            }

        }
        private void arduinoButton_Click(object sender, EventArgs e)
        {
            comboBox.Items.Clear();
            // Получаем список COM портов доступных в системе
            string[] portnames = SerialPort.GetPortNames();
            // Проверяем есть ли доступные
            if (portnames.Length == 0)
            {
                MessageBox.Show("COM PORT not found");
            }
            foreach (string portName in portnames)
            {
                //добавляем доступные COM порты в список           
                comboBox.Items.Add(portName);
                //Console.WriteLine(portnames.Length);
                if (portnames[0] != null)
                {
                    comboBox.SelectedItem = portnames[0];
                }
            }
        }
        private void connectToArduino()
        {
            
            try
            {
                
                string selectedPort = comboBox.GetItemText(comboBox.SelectedItem);
                serialPort.PortName = selectedPort;
                serialPort.BaudRate = 500000;
                serialPort.Open();
                isConnected = true;
                //Создаем новый объект потока (Thread)
                Mes = new int[10, 3];
                len_t = 0;
                try
                {
                    Thread myThread = new Thread(func);
                    myThread.Start(); //запускаем поток
                }
                catch
                {

                }
                

            }
            catch
            {

            }
        }
        async void func()
        {
            while (isConnected == true)
            {
               await Task.Delay(20);
                  if (val!=0 && var !=0)
                  {
                    string Mes1 = "0";
                    string Mes2 = "0";
                    try
                    {
                        Mes1 = Convert.ToString(val);
                        Mes2 = Convert.ToString(var);
                    }
                    catch
                    {
                        Console.WriteLine("except convert");
                    }
                    if (Mes1.Length <= 3 && Mes2.Length <= 2 )
                    {



                        while (Mes1.Length < 3)
                        {
                            Mes1 = "0" + Mes1;
                        }
                        while (Mes2.Length < 2)
                        {
                            Mes2 = "0" + Mes2;
                        }
                        Mes1 = "b" + Mes1 + Mes2;
                        try
                        {
                            Console.WriteLine("Out: " + Mes1);
                            this.serialPort.WriteLine(Mes1);
                        }
                        catch
                        {
                            Console.WriteLine("except sending");
                        }
                        
                    }
                    else
                    {
                        Console.WriteLine("message lengh too long");
                    }
                     val = 0;
                     var = 0;
                  }
                                   
            }
        }
        /*async void func()
        {
            string a = "";
            while (true)
            {
                if (isConnected == true)
                {
                    await Task.Delay(20);
                    //Console.WriteLine(Mes[2]);
                    int rows = Mes.GetUpperBound(0) + 1;
                    for (int i1 = 0; i1 < rows; i1++)
                    {
                        if (Mes[i1, 2] == 1)
                        {
                            string Mes1 = Convert.ToString(Mes[i1, 0]);
                            string Mes2 = Convert.ToString(Mes[i1, 1]);
                            while (Mes1.Length < 3)
                            {
                                Mes1 = "0" + Mes1;
                            }
                            while (Mes2.Length < 2)
                            {
                                Mes2 = "0" + Mes2;
                            }
                            Mes1 = "b" + Mes1 + Mes2;
                            Console.WriteLine("Out: " + Mes1);
                            this.serialPort.WriteLine(Mes1);
                            try
                            {
                                await Task.Delay(2);
                                string a1 = this.serialPort.ReadLine();
                                int a2 = Convert.ToInt32(a1);
                                if (a2 != 9)
                                {
                                    len_t--;
                                    Mes[i1, 2] = 0;
                                    Mes[i1, 1] = 0;
                                    Mes[i1, 0] = 0;
                                }
                            }
                            catch
                            {

                                Console.WriteLine("Catch_1");
                            }
                        }
                    }
                    try
                    {
                        a = this.serialPort.ReadLine();
                        Console.WriteLine("In: " + a);
                        mes_out = Convert.ToInt32(a);
                        Invoke(delegate_Gui);
                    }
                    catch
                    {
                        Console.WriteLine("Catch_2");
                    }
                }
            }
        }
        */
        private void disconnectFromArduino()
        {
            try
            {
                //Mes = new int[10, 3];
                serialPort.Close();
                isConnected = false;
            }
            catch
            {

            }
            
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // При закрытии программы, закрываем порт
            if (serialPort.IsOpen) serialPort.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (isOn_nas == false)
            {
                addtask(6, 1);
                button3.Text = "Выключить насосы";
                isOn_nas = true;

            }
            else
            {
                addtask(5, 1);
                button3.Text = "Включить насосы";
                isOn_nas = false;
            }

        }
        private void Print_spher_Click(object sender, EventArgs e)
        {
            addtask(1, 1);
        }
        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                connectToArduino();
            }
            catch
            {

            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            addtask(2, 1);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (isDown_piston == false)
            {
                addtask(5, 1);
                isDown_piston = true;
                button2.Text = "Задвинуть поршень";
            }
            else
            {
                addtask(6, 1);
                isDown_piston = false;
                button2.Text = "Выдвинуть поршень";
            }

        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            addtask(Convert.ToInt16(textBox1.Text), 2);
            addtask(Convert.ToInt16(textBox2.Text), 3);
            addtask(Convert.ToInt16(textBox3.Text), 4);
            addtask(Convert.ToInt16(textBox4.Text), 5);
            addtask(Convert.ToInt16(textBox6.Text), 6);
        }
        int[] axes;
        int[] directions;
        private void ErorMsg(COMException Ex)
        {
            string Str = Dict.LangStrings.Error + Ex.Source + "\n\r";
            Str = Str + Ex.Message + "\n\r";
            Str = Str + "HRESULT:" + String.Format("0x{0:X}", (Ex.ErrorCode));
            MessageBox.Show(Str, "EnableEvent");
        }
        private double[] encBox(TextBox textBox, TextBox textBox1, TextBox textBox2)
        {
            var ret = new double[3];
            try {
                ret[0] = Convert.ToDouble(textBox.Text);
                ret[1] = Convert.ToDouble(textBox1.Text);
                ret[2] = Convert.ToDouble(textBox2.Text);
            }
            catch
            {

            }
            
            return ret;
        }
        private void send_mes_1eng(int x, int y, double value)
        {            
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(x, y, value);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        async private void Macro1(int eng, double value, double h = 1, int delay=1000)
        {
            addtask(1, 1);
            send_mes_1eng(ConnectionData.Value.ACSC_AMF_RELATIVE, eng, value);            
            await Task.Delay(delay);
            send_mes_1eng(ConnectionData.Value.ACSC_AMF_RELATIVE, 3, -h);            
            await Task.Delay(delay);
            send_mes_1eng(ConnectionData.Value.ACSC_AMF_RELATIVE, 3, h);
            await Task.Delay(delay);
            //X = 0
            //Y = 1
        }
        async private void Macro2(double valueX, double valueY, double h, int delay)
        {
            send_mes_1eng(ConnectionData.Value.ACSC_AMF_RELATIVE, 3, -h);
            await Task.Delay(delay);
            send_mes_1eng(ConnectionData.Value.ACSC_AMF_RELATIVE, 0, valueX);
            send_mes_1eng(ConnectionData.Value.ACSC_AMF_RELATIVE, 1, valueY);
            //X = 0
            //Y = 1
            await Task.Delay(delay);
            send_mes_1eng(ConnectionData.Value.ACSC_AMF_RELATIVE, 3, h);
            await Task.Delay(delay);
        }
        #region XY buttons
        int delay = 1000;
        double value = 1;
        double h = 1;
        
            
        private void but_Xm_Click(object sender, EventArgs e)
        {
            var ret1 = encBox(textBox11, textBox12, textBox13);
            value = ret1[0];
            h = ret1[1];
            delay = Convert.ToInt32(ret1[2]);
            Macro1(0, -value, h, delay);
        }
        private void but_Xp_Click(object sender, EventArgs e)
        {
            var ret1 = encBox(textBox11, textBox12, textBox13);
            value = ret1[0];
            h = ret1[1];
            delay = Convert.ToInt32(ret1[2]);
            Macro1(0, value, h, delay);
        }      
        private void but_Ym_Click(object sender, EventArgs e)
        {
            var ret1 = encBox(textBox11, textBox12, textBox13);
            value = ret1[0];
            h = ret1[1];
            delay = Convert.ToInt32(ret1[2]);
            Macro1(1, -value, h, delay);
        }
        private void but_Yp_Click(object sender, EventArgs e)
        {
            var ret1 = encBox(textBox11, textBox12, textBox13);
            value = ret1[0];
            h = ret1[1];
            delay = Convert.ToInt32(ret1[2]);
            Macro1(1, value, h, delay);
        }
        private void JogS2PistPos_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_8, ConnectionData.SetPtVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void JogS2PistPos_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_8);
        }
        private void JogS2PistMin_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_8, -ConnectionData.SetPtVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void JogS2PistMin_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_8);
        }
        private void S2PistPos100_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    double Dia = Convert.ToDouble(S2DiamTB.Text);
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_8, 100 / ((Math.PI * Math.Pow(Dia, 2) / 4)));
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void S2PistPos10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    double Dia = Convert.ToDouble(S2DiamTB.Text);
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_8, 10 / ((Math.PI * Math.Pow(Dia, 2) / 4)));
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void S2PistPos1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    double Dia = Convert.ToDouble(S2DiamTB.Text);
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_8, 1 / ((Math.PI * Math.Pow(Dia, 2) / 4)));
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void S2PistPos01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    double Dia = Convert.ToDouble(S2DiamTB.Text);
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_8, 0.1 / ((Math.PI * Math.Pow(Dia, 2) / 4)));
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void S2PistMin01_Click(object sender, EventArgs e)
        {

        }
        private void S2PistMin1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    double Dia = Convert.ToDouble(S2DiamTB.Text);
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_8, -1 / ((Math.PI * Math.Pow(Dia, 2) / 4)));
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void S2PistMin10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    double Dia = Convert.ToDouble(S2DiamTB.Text);
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_8, -10 / ((Math.PI * Math.Pow(Dia, 2) / 4)));
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void S2PistMin100_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    double Dia = Convert.ToDouble(S2DiamTB.Text);
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_8, -100 / ((Math.PI * Math.Pow(Dia, 2) / 4)));
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void JogZS2Min_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_3, -ConnectionData.SetZVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void JogZS2Min_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_3);
        }
        private void ZS2Pos10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_3, 10);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void ZS2Pos1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_3, 1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void ZS2Pos01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_3, 0.1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void ZS2Pos001_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_3, 0.01);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void ZS2Min001_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_3, -0.01);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void ZS2Min01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_3, -0.1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void ZS2Min1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_3, -1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void ZS2Min10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_3, -10);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void JogZS2Pos_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_3, ConnectionData.SetZVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void JogZS2Pos_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_3);
        }
        private void XPos001_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_0, 0.01);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void Xpos01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_0, 0.1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void PosX1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_0, 1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void PosX10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_0, 10);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void XMin001_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_0, -0.01);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void XMin10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_0, -10);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void XMin01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_0, -0.1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void XMin1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_0, -1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void YNeg001_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_1, -0.01);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void YNeg01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_1, -0.1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void YNeg1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_1, -1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void YNeg10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_1, -10);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void YPos001_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_1, 0.01);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void YPos01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_1, 0.1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void YPos1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_1, 1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void YPos10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_1, 10);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void JogXPBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_0);
        }
        private void JogXMBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_0);
        }
        private void JogYMBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_1);
        }
        private void JogYPBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_1);
        }
        private void JogYPBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_1, ConnectionData.SetXYVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void JogXPBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_0, ConnectionData.SetXYVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void JogYMBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_1, -ConnectionData.SetXYVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void JogXMBtn_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_0, -ConnectionData.SetXYVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void LBBtn_MouseDown(object sender, MouseEventArgs e)
        {
            directions = new int[2]
        {
                        ConnectionData.Value.ACSC_NEGATIVE_DIRECTION,
                        ConnectionData.Value.ACSC_POSITIVE_DIRECTION
        };
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.JogM(ConnectionData.Value.ACSC_AMF_VELOCITY, axes, directions, ConnectionData.SetXYVel);
                    ConnectionData.Value.EndSequenceM(axes);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void RBBtn_MouseDown(object sender, MouseEventArgs e)
        {
            directions = new int[2]
                {
                        ConnectionData.Value.ACSC_POSITIVE_DIRECTION,
                        ConnectionData.Value.ACSC_POSITIVE_DIRECTION
                };
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.JogM(ConnectionData.Value.ACSC_AMF_VELOCITY, axes, directions, ConnectionData.SetXYVel);
                    ConnectionData.Value.EndSequenceM(axes);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void LButBtn_MouseDown(object sender, MouseEventArgs e)
        {
            directions = new int[2]
                {
                        ConnectionData.Value.ACSC_NEGATIVE_DIRECTION,
                        ConnectionData.Value.ACSC_NEGATIVE_DIRECTION
                };
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.JogM(ConnectionData.Value.ACSC_AMF_VELOCITY, axes, directions, ConnectionData.SetXYVel);
                    ConnectionData.Value.EndSequenceM(axes);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void RButBtn_MouseDown(object sender, MouseEventArgs e)
        {
            directions = new int[2]
                {
                        ConnectionData.Value.ACSC_POSITIVE_DIRECTION,
                        ConnectionData.Value.ACSC_NEGATIVE_DIRECTION
                };
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.JogM(ConnectionData.Value.ACSC_AMF_VELOCITY, axes, directions, ConnectionData.SetXYVel);
                    ConnectionData.Value.EndSequenceM(axes);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void LBBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.KillM(axes);
            ConnectionData.Value.EndSequenceM(axes);
        }
        private void RBBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.KillM(axes);
            ConnectionData.Value.EndSequenceM(axes);
        }
        private void LButBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.KillM(axes);
            ConnectionData.Value.EndSequenceM(axes);
        }
        private void RButBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.KillM(axes);
            ConnectionData.Value.EndSequenceM(axes);
        }
        #endregion
        double OffsetX = 0;
        double OffsetY = 0;
        double OffsetGl = 0;
        int StartTool;
        void Reload(string param)
        {
            try {
                if (ConnectionData.ProgramStart == 10)
                {
                    groupBox4.Enabled = false;
                }
                else
                {
                    groupBox4.Enabled = true;
                }

                OffsetGl = ConnectionData.Value.ReadVariable("ZGLOBAL", ConnectionData.Value.ACSC_NONE);

                MachinePosX_p.Text = string.Format("{0:F2}", ConnectionData.FeedBackX);
                MachinePosY_p.Text = string.Format("{0:F2}", ConnectionData.FeedBackY);
                XToolPos_p.Text = string.Format("{0:F2}", ConnectionData.ToolX);
                YToolPos_p.Text = string.Format("{0:F2}", ConnectionData.ToolY);
                // S2ZPosition.Text = string.Format("{0:F2}", ConnectionData.FeedBackZS2);

                ZToolPos_p.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ2 - OffsetGl);
                MachinePosZ_p.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ2);
                // ConnectionData.ToolX = ConnectionData.FeedBackX - OffsetX;
                // ConnectionData.ToolY = ConnectionData.FeedBackY - OffsetY;

                if (ConnectionData.XRLimit)
                {
                    XRL.BackColor = Color.Red;
                }
                else
                {
                    XRL.BackColor = SystemColors.Control;
                }
                if (ConnectionData.XLLimit)
                {
                    XLL.BackColor = Color.Red;
                }
                else
                {
                    XLL.BackColor = SystemColors.Control;
                }
                if (ConnectionData.YRLimit)
                {
                    YRL.BackColor = Color.Red;
                }
                else
                {
                    YRL.BackColor = SystemColors.Control;
                }
                if (ConnectionData.YLLimit)
                {
                    YLL.BackColor = Color.Red;
                }
                else
                {
                    YLL.BackColor = SystemColors.Control;
                }
            }
            catch
            {
                Console.WriteLine("except reload");
            }
            
                        
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            Gui_load_param();
            S2GB.Enabled = true;
            axes = new int[3]
                  {
                        ConnectionData.Value.ACSC_AXIS_0,
                        ConnectionData.Value.ACSC_AXIS_1,
                        -1
                  };
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void label15_Click(object sender, EventArgs e)
        {

        }
        private void upd(object sender, EventArgs e)
        {
            Update();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Prog_stat();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            Gui_load_param();
        }
        async private void f_push_w()
        {
            addtask(1, 2);
            addtask(2, 3);
            await Task.Delay(500);
            addtask(0, 2);
            addtask(0, 3);
        }
        private void Push_water_Click(object sender, EventArgs e)
        {
            f_push_w();
        }
        private void Fast_open_Click(object sender, EventArgs e)
        {
            addtask(7, 1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InitCamera();
        }

        private void PrintSph_Click(object sender, EventArgs e)
        {
            addtask(1, 1);
        }

        private void PrintSph_1_Click(object sender, EventArgs e)
        {
            addtask(7, 1);
        }

        private void PrintSph_2_Click(object sender, EventArgs e)
        {
            addtask(8, 1);
        }

        private void mdelay_Click(object sender, EventArgs e)
        {
            addtask(Convert.ToInt16(mdelay_box.Text), 3);
        }

        private void mdelay_calib_Click(object sender, EventArgs e)
        {
            addtask(Convert.ToInt16(mdelay_calib_box.Text), 4);
        }

        private void motorUp_Click(object sender, EventArgs e)
        {
            addtask(Convert.ToInt16(motorUp_box.Text), 2);
        }

        private void motorDown_Click(object sender, EventArgs e)
        {
            addtask(Convert.ToInt16(motorDown_box.Text), 6);
        }

        private void Calibration_Click(object sender, EventArgs e)
        {
            addtask(4, 1);
        }

        private void PrintStepsShort_Click(object sender, EventArgs e)
        {
            addtask(Convert.ToInt16(PrintStepsShort_box.Text), 5);
        }

        private void motorUpMove_box_Click(object sender, EventArgs e)
        {
            addtask(3, 1);
        }

        private void motorDownMove_box_Click(object sender, EventArgs e)
        {
            addtask(2, 1);
        }

        private void DisconnectArduino_Click(object sender, EventArgs e)
        {
            try
            {
                disconnectFromArduino();
            }
            catch
            {

            }
            
        }
    }
}