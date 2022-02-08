using Active_Directory_Worker.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace BTP
{
    public partial class Diagnostics_frm : Form, ILanguageChangable
    {
        public Diagnostics_frm()
        {
            CallBackMy5.callbackEventHandler = new CallBackMy5.callbackEvent(this.Reload);
            InitializeComponent();
        }

        public void ChangeFormLanguage(AvaliableLocalizations newLocalization)
        {
            Sett sett1 = new Sett();
            sett1.SetCulture(newLocalization);

            var resources = new ComponentResourceManager(typeof(Diagnostics_frm));
            CultureInfo newCultureInfo = new CultureInfo(EnumDescriptionHelper.GetEnumDescription(newLocalization));

            foreach (Control C in Controls)
            {
                resources.ApplyResources(C, C.Name, newCultureInfo);
                if (C is GroupBox)
                {
                    foreach (Control G in C.Controls)
                    {
                        resources.ApplyResources(G, G.Name, newCultureInfo);
                    }
                }
            }
        }

        double ReferenceX = 0;
        double ReferenceY = 0;
        double ReferenceZS1 = 0;
        double ReferenceZS2 = 0;
        double ReferenceZS3 = 0;
        double ReferenceZSp = 0;
        double ReferenceZPF = 0;
        double ReferenceS1 = 0;
        double ReferenceS2 = 0;
        double ReferenceS3 = 0;
        double ReferenceX_old = 0;
        double ReferenceY_old = 0;
        double ReferenceZS1_old = 0;
        double ReferenceZS2_old = 0;
        double ReferenceZS3_old = 0;
        double ReferenceZSp_old = 0;
        double ReferenceZPF_old = 0;
        double ReferenceS1_old = 0;
        double ReferenceS2_old = 0;
        double ReferenceS3_old = 0;

        string SingleMessage = "";
        string SingleMessage_old = "";

        long[] MotorError = new long[16];
        long[] MotorError_old = new long[16];
        long[] MotionError = new long[16];
        long[] MotionError_old = new long[16];
        long[] ProgramError = new long[16];
        long[] ProgramError_old = new long[16];

        int Start_old;

        void Reload(string param)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    // Actual postition
                XPosActText.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0));
                YPosActText.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1));
                S1PosActText.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_2));
                S2PosActText.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_3));
                S3PosActText.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_4));
                P1PosActText.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_7));
                P2PosActText.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_8));
                P3PosActText.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_9));
                SpPosActText.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_5));
                PfPosActText.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_6));

                ReferenceX      = ConnectionData.Value.ReadVariable("TPOS", ConnectionData.Value.ACSC_NONE, 0, 0);
                ReferenceY      = ConnectionData.Value.ReadVariable("TPOS", ConnectionData.Value.ACSC_NONE, 1, 1);
                ReferenceZS1    = ConnectionData.Value.ReadVariable("TPOS", ConnectionData.Value.ACSC_NONE, 2, 2);
                ReferenceZS2    = ConnectionData.Value.ReadVariable("TPOS", ConnectionData.Value.ACSC_NONE, 3, 3);
                ReferenceZS3    = ConnectionData.Value.ReadVariable("TPOS", ConnectionData.Value.ACSC_NONE, 4, 4);
                ReferenceS1     = ConnectionData.Value.ReadVariable("TPOS", ConnectionData.Value.ACSC_NONE, 7, 7);
                ReferenceS2     = ConnectionData.Value.ReadVariable("TPOS", ConnectionData.Value.ACSC_NONE, 8, 8);
                ReferenceS3     = ConnectionData.Value.ReadVariable("TPOS", ConnectionData.Value.ACSC_NONE, 9, 9);
                ReferenceZSp    = ConnectionData.Value.ReadVariable("TPOS", ConnectionData.Value.ACSC_NONE, 5, 5);
                ReferenceZPF    = ConnectionData.Value.ReadVariable("TPOS", ConnectionData.Value.ACSC_NONE, 6, 6);


                // Reference position
                XPosRefText.Text =  string.Format("{0:F2}", ReferenceX);
                YPosRefText.Text =  string.Format("{0:F2}", ReferenceY);
                S1PosRefText.Text = string.Format("{0:F2}", ReferenceZS1);
                S2PosRefText.Text = string.Format("{0:F2}", ReferenceZS2);
                S3PosRefText.Text = string.Format("{0:F2}", ReferenceZS3);
                P1PosRefText.Text = string.Format("{0:F2}", ReferenceS1);
                P2PosRefText.Text = string.Format("{0:F2}", ReferenceS2);
                P3PosRefText.Text = string.Format("{0:F2}", ReferenceS3);
                SpPosRefText.Text = string.Format("{0:F2}", ReferenceZSp);
                PfPosRefText.Text = string.Format("{0:F2}", ReferenceZPF);

                // Remains in Syringes
                P1RemainsTextBox.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_7) * Math.PI * Math.Pow(ConnectionData.S1Diameter, 2) / 4);
                P2RemainsTextBox.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_8) * Math.PI * Math.Pow(ConnectionData.S2Diameter, 2) / 4);
                P3RemainsTextBox.Text = string.Format("{0:F2}", ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_9) * Math.PI * Math.Pow(ConnectionData.S3Diameter, 2) / 4);

                if (ReferenceX != ReferenceX_old)
                {
                    XPosTrackBar.Maximum = Convert.ToInt32(Math.Abs(ReferenceX - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0)));
                    XPosTrackBar.Minimum = -Convert.ToInt32(Math.Abs(ReferenceX - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0)));
                    ReferenceX_old = ReferenceX;
                }

                if (ReferenceY != ReferenceY_old)
                {
                    YPosTrackBar.Maximum = Convert.ToInt32(Math.Abs(ReferenceY - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1)));
                    YPosTrackBar.Minimum = -Convert.ToInt32(Math.Abs(ReferenceY - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1)));
                    ReferenceY_old = ReferenceY;
                }

                if (ReferenceZS1 != ReferenceZS1_old)
                {
                    S1PosTrackBar.Maximum = Convert.ToInt32(Math.Abs(ReferenceZS1 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_2)));
                    S1PosTrackBar.Minimum = -Convert.ToInt32(Math.Abs(ReferenceZS1 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_2)));
                    ReferenceZS1_old = ReferenceZS1;
                }

                if (ReferenceZS2 != ReferenceZS2_old)
                {
                    S2PosTrackBar.Maximum = Convert.ToInt32(Math.Abs(ReferenceZS2 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_3)));
                    S2PosTrackBar.Minimum = -Convert.ToInt32(Math.Abs(ReferenceZS2 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_3)));
                    ReferenceZS2_old = ReferenceZS2;
                }

                if (ReferenceZS3 != ReferenceZS3_old)
                {
                    S3PosTrackBar.Maximum = Convert.ToInt32(Math.Abs(ReferenceZS3 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_4)));
                    S3PosTrackBar.Minimum = -Convert.ToInt32(Math.Abs(ReferenceZS3 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_4)));
                    ReferenceZS3_old = ReferenceZS3;
                }

                if (ReferenceS1 != ReferenceS1_old)
                {
                    P1PosTrackBar.Maximum = Convert.ToInt32(Math.Abs(ReferenceS1 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_7)));
                    P1PosTrackBar.Minimum = -Convert.ToInt32(Math.Abs(ReferenceS1 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_7)));
                    ReferenceS1_old = ReferenceS1;
                }

                if (ReferenceS2 != ReferenceS2_old)
                {
                    P2PosTrackBar.Maximum = Convert.ToInt32(Math.Abs(ReferenceS2 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_8)));
                    P2PosTrackBar.Minimum = -Convert.ToInt32(Math.Abs(ReferenceS2 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_8)));
                    ReferenceS2_old = ReferenceS2;
                }

                if (ReferenceS3 != ReferenceS3_old)
                {
                    P3PosTrackBar.Maximum = Convert.ToInt32(Math.Abs(ReferenceS3 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_9)));
                    P3PosTrackBar.Minimum = -Convert.ToInt32(Math.Abs(ReferenceS3 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_9)));
                    ReferenceS3_old = ReferenceS3;
                }

                if (ReferenceZSp != ReferenceZSp_old)
                {
                    SpPosTrackBar.Maximum = Convert.ToInt32(Math.Abs(ReferenceZSp - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_5)));
                    SpPosTrackBar.Minimum = -Convert.ToInt32(Math.Abs(ReferenceZSp - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_5)));
                    ReferenceZSp_old = ReferenceZSp;
                }

                if (ReferenceZPF != ReferenceZPF_old)
                {
                    PfPosTrackBar.Maximum = Convert.ToInt32(Math.Abs(ReferenceZPF - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_6)));
                    PfPosTrackBar.Minimum = -Convert.ToInt32(Math.Abs(ReferenceZPF - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_6)));
                    ReferenceZPF_old = ReferenceZPF;
                }

                // Error

                    XPosTrackBar.Value = Convert.ToInt32((ReferenceX - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0)));
                    YPosTrackBar.Value = Convert.ToInt32((ReferenceY - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1)));
                    S1PosTrackBar.Value = Convert.ToInt32((ReferenceZS1 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_2)));
                    S2PosTrackBar.Value = Convert.ToInt32((ReferenceZS2 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_3)));
                    S3PosTrackBar.Value = Convert.ToInt32((ReferenceZS3 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_4)));
                    P1PosTrackBar.Value = Convert.ToInt32((ReferenceS1 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_7)));
                    P2PosTrackBar.Value = Convert.ToInt32((ReferenceS2 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_8)));
                    P3PosTrackBar.Value = Convert.ToInt32((ReferenceS3 - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_9)));
                    SpPosTrackBar.Value = Convert.ToInt32((ReferenceZSp - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_5)));
                    PfPosTrackBar.Value = Convert.ToInt32((ReferenceZPF - ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_6)));
                }
                catch
                {

                }

                //ConnectionData.streamWriter.WriteLine("sfdfdf");

                ConnectionData.streamWriter.BaseStream.Seek(ConnectionData.fileStream.Length, SeekOrigin.End);

                if (ConnectionData.ProgramStart != Start_old)
                {
                    if (ConnectionData.ProgramStart == 10)
                    {
                        LogData_RTB.Text += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Program START" + "\n";
                        ConnectionData.streamWriter.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Program START");
                        Start_old = ConnectionData.ProgramStart;
                        return;
                    }
                    if (ConnectionData.ProgramStart == 20)
                    {
                        LogData_RTB.Text += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Program STOP" + "\n";
                        ConnectionData.streamWriter.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Program STOP");
                        Start_old = ConnectionData.ProgramStart;
                        return;
                    }
                    if (ConnectionData.ProgramStart == 30)
                    {
                        LogData_RTB.Text += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Program PAUSE" + "\n";
                        ConnectionData.streamWriter.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Program PAUSE");
                        Start_old = ConnectionData.ProgramStart;
                        return;
                    }
                }

                try
                {
                    for (int p = 0; p <= 15; p++)
                    {
                        MotorError[p] = ConnectionData.Value.GetMotorError(p);
                        MotionError[p] = ConnectionData.Value.GetMotionError(p);
                        ProgramError[p] = ConnectionData.Value.GetProgramError(p);
                        if (MotorError[p] != MotorError_old[p])
                        {
                            //MessageBox.Show("Axis " + p.ToString() + " " + ConnectionData.Value.GetErrorString((int)MotorError[p]));
                            LogData_RTB.Text += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Axis " + p.ToString() + ": " + ConnectionData.Value.GetErrorString((int)MotorError[p]) + "\n";
                            ConnectionData.streamWriter.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Axis " + p.ToString() + ": " + ConnectionData.Value.GetErrorString((int)MotorError[p]));
                            MotorError_old[p] = MotorError[p];
                            return;
                        }
                        if (MotionError[p] != MotionError_old[p])
                        {
                            //MessageBox.Show("Axis " + p.ToString() + " " + ConnectionData.Value.GetErrorString((int)MotionError[p]));
                            LogData_RTB.Text += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Axis " + p.ToString() + ": " + ConnectionData.Value.GetErrorString((int)MotionError[p]) + "\n";
                            ConnectionData.streamWriter.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Axis " + p.ToString() + ": " + ConnectionData.Value.GetErrorString((int)MotionError[p]));
                            MotionError_old[p] = MotionError[p];
                            return;
                        }
                    }
                    for (int p = 0; p <= 9; p++)
                    {
                        if (ProgramError[p] != ProgramError_old[p])
                        {
                            //MessageBox.Show(ConnectionData.Value.GetErrorString((int)ProgramError[p]));
                            LogData_RTB.Text += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Buffer " + p.ToString() + ": " + ConnectionData.Value.GetErrorString((int)ProgramError[p]) + "\n";
                            ConnectionData.streamWriter.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": Buffer " + p.ToString() + ": " + ConnectionData.Value.GetErrorString((int)ProgramError[p]));
                            ProgramError_old[p] = ProgramError[p];
                            return;
                        }
                    }
                    SingleMessage = ConnectionData.Value.GetSingleMessage(10);

                    if (SingleMessage != SingleMessage_old)
                    {
                        LogData_RTB.Text += DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": " + SingleMessage + "\n";
                        ConnectionData.streamWriter.WriteLine(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + ": " + SingleMessage + "\n");

                        SingleMessage_old = SingleMessage;
                    }
                }
                catch 
                {
                    //MessageBox.Show(Ex.Message);
                }


                // Log-data


            }
        }

        private void LogData_RTB_TextChanged(object sender, EventArgs e)
        {
            LogData_RTB.SelectionStart = LogData_RTB.Text.Length;
            LogData_RTB.ScrollToCaret();
        }



        private void Diagnostics_frm_Load(object sender, EventArgs e)
        {

        }

        private void Diagnostics_frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //string LogFile = Application.StartupPath + "\\Log\\Log_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".log";
            //File.WriteAllText(LogFile, LogData_RTB.Text);

        }

        private void Diagnostics_frm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
