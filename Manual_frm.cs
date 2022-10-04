using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using Active_Directory_Worker.Interfaces;

namespace BTP
{
    public partial class Manual_frm : Form, ILanguageChangable
    {
        int X = 0;
        int Y = 1;

        int Z3 = 2;
        int F3 = 3;

        int Z1 = 4;
        int F1 = 5;

        int Z2 = 6;
        int F2 = 7;
        public Manual_frm()
        {
            CallBackMy2.callbackEventHandler = new CallBackMy2.callbackEvent(this.Reload);
            InitializeComponent();

        }

        //Thread UpdateDataThread = null;
        double OffsetX = 0;
        double OffsetY = 0;
        double OffsetZ1 = 0;
        double OffsetZ2 = 0;
        double OffsetZ3 = 0;
        double OffsetW = 0;
        double OffsetA = 0;
        double OffsetGl = 0;
        double TimerTime = 0;
        double StopTime = 0;
        
        


        void Reload(string param)
        {
            X = ConnectionData.Value.ACSC_AXIS_0;
            Y = ConnectionData.Value.ACSC_AXIS_1;

            Z3 = ConnectionData.Value.ACSC_AXIS_2;
            F3 = ConnectionData.Value.ACSC_AXIS_3;

            Z1 = ConnectionData.Value.ACSC_AXIS_4;
            F1= ConnectionData.Value.ACSC_AXIS_5;

            Z2 = ConnectionData.Value.ACSC_AXIS_6;
            F2 = ConnectionData.Value.ACSC_AXIS_7;
            if (ConnectionData.ProgramStart == 10)
            {
                groupBox4.Enabled = false;
            }
            else
            {
                groupBox4.Enabled = true;
            }

            OffsetGl = ConnectionData.Value.ReadVariable("ZGLOBAL", ConnectionData.Value.ACSC_NONE);

            MachinePosX.Text = string.Format("{0:F2}", ConnectionData.FeedBackX);
            MachinePosY.Text = string.Format("{0:F2}", ConnectionData.FeedBackY);
            XToolPos.Text = string.Format("{0:F2}", ConnectionData.ToolX);
            YToolPos.Text = string.Format("{0:F2}", ConnectionData.ToolY);
            
            S1ZPosition.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ1);
            S2ZPosition.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ2);
            S3ZPosition.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ3);

            switch(StartTool)
            {
                case 1:
                    ConnectionData.DrawDishX = ConnectionData.GlobalX - (ConnectionData.XZoffsets + ConnectionData.PetriX);
                    ConnectionData.DrawDishY = ConnectionData.GlobalY - (ConnectionData.YZoffsets + ConnectionData.PetriY);
                    break;
                case 2:
                    ConnectionData.DrawDishX = ConnectionData.GlobalX - (ConnectionData.XUoffsets + ConnectionData.PetriX);
                    ConnectionData.DrawDishY = ConnectionData.GlobalY - (ConnectionData.YUoffsets + ConnectionData.PetriY);
                    break;
                case 3:
                    ConnectionData.DrawDishX = ConnectionData.GlobalX - (ConnectionData.XVoffsets + ConnectionData.PetriX);
                    ConnectionData.DrawDishY = ConnectionData.GlobalY - (ConnectionData.YVoffsets + ConnectionData.PetriY);
                    break;
                case 4:
                    ConnectionData.DrawDishX = -ConnectionData.GlobalX - (ConnectionData.WXaxis + ConnectionData.PetriX);
                    ConnectionData.DrawDishY = -ConnectionData.GlobalY - (ConnectionData.WYaxis + ConnectionData.PetriY);
                    break;
                case 5:
                    ConnectionData.DrawDishX = -ConnectionData.GlobalX - (ConnectionData.XAoffsets + ConnectionData.PetriX);
                    ConnectionData.DrawDishY = -ConnectionData.GlobalY - (ConnectionData.YAoffsets + ConnectionData.PetriY);
                    break;
                default:
                    break;
            }
     
            switch (head)
            {
                case 1:
                    ZToolPos.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ1 );
                    MachinePosZ.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ1);
                    ConnectionData.ToolX = ConnectionData.FeedBackX - OffsetX;
                    ConnectionData.ToolY = ConnectionData.FeedBackY - OffsetY;
                    break;
                case 2:
                    ZToolPos.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ2 - OffsetGl);
                    MachinePosZ.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ2);
                    ConnectionData.ToolX = ConnectionData.FeedBackX - OffsetX;
                    ConnectionData.ToolY = ConnectionData.FeedBackY - OffsetY;

                    break;
                case 3:
                    ZToolPos.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ3 - OffsetGl);
                    MachinePosZ.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ3);
                    ConnectionData.ToolX = ConnectionData.FeedBackX - OffsetX;
                    ConnectionData.ToolY = ConnectionData.FeedBackY - OffsetY;

                    break;
                case 4:
                    ZToolPos.Text    = string.Format("{0:F2}", ConnectionData.FeedBackZSp - OffsetGl);
                    MachinePosZ.Text = string.Format("{0:F2}", ConnectionData.FeedBackZSp);
                    ConnectionData.ToolX = ConnectionData.FeedBackX - OffsetX;
                    ConnectionData.ToolY = ConnectionData.FeedBackY - OffsetY;
                    break;

                default:
                    break;
            }

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

            if (ConnectionData.ZS1RLimit)
            {
                ZS1RL.BackColor = Color.Red;
            }
            else
            {
                ZS1RL.BackColor = SystemColors.Control;
            }
            if (ConnectionData.ZS1LLimit)
            {
                ZS1LL.BackColor = Color.Red;
            }
            else
            {
                ZS1LL.BackColor = SystemColors.Control;
            }

            if (ConnectionData.ZS2RLimit)
            {
                ZS2RL.BackColor = Color.Red;
            }
            else
            {
                ZS2RL.BackColor = SystemColors.Control;
            }
            if (ConnectionData.ZS2LLimit)
            {
                ZS2LL.BackColor = Color.Red;
            }
            else
            {
                ZS2LL.BackColor = SystemColors.Control;
            }

            if (ConnectionData.ZS3RLimit)
            {
                ZS3RL.BackColor = Color.Red;
            }
            else
            {
                ZS3RL.BackColor = SystemColors.Control;
            }
            if (ConnectionData.ZS3LLimit)
            {
                ZS3LL.BackColor = Color.Red;
            }
            else
            {
                ZS3LL.BackColor = SystemColors.Control;
            }

            
    }

        int StartTool;
/*
        // Обновление данных на экране 
        void UpdateData()
        {
            while (true)
            {
                try
                {
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_0, ConnectionData.SetXYVel);
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_1, ConnectionData.SetXYVel);
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_2, ConnectionData.SetZVel);
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_3, ConnectionData.SetZVel);
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_4, ConnectionData.SetZVel);
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_5, ConnectionData.SetZVel);
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_6, ConnectionData.SetZVel);
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_7, ConnectionData.SetPtVel);
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_8, ConnectionData.SetPtVel);
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_9, ConnectionData.SetPtVel);
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_14, ConnectionData.SetPFVel);
                    ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_15, ConnectionData.SetPFVel);

                    MachinePosX.Text = string.Format("{0:F2}", ConnectionData.FeedBackX );
                    MachinePosY.Text = string.Format("{0:F2}", ConnectionData.FeedBackY);
                    S1ZPosition.Text = string.Format("{0:F2}", ConnectionData.FeedBackZS1);
                    S2ZPosition.Text = string.Format("{0:F2}", ConnectionData.FeedBackZS2);
                    S3ZPosition.Text = string.Format("{0:F2}", ConnectionData.FeedBackZS3);
                    SpZPosition.Text = string.Format("{0:F2}", ConnectionData.FeedBackZSp);
                    PFZPosition.Text = string.Format("{0:F2}", ConnectionData.FeedBackZPF);
                    XToolPos.Text = string.Format("{0:F2}", ConnectionData.FeedBackX + ConnectionData.XOffset);
                    YToolPos.Text = string.Format("{0:F2}", ConnectionData.FeedBackY + ConnectionData.YOffset);
                                     
                    switch (head)
                    {
                        case 1:
                            ZToolPos.Text = string.Format("{0:F2}", ConnectionData.FeedBackZS1 + ConnectionData.ZOffset);
                            break;
                        case 2:
                            ZToolPos.Text = string.Format("{0:F2}", ConnectionData.FeedBackZS2 + ConnectionData.ZOffset);
                            break;
                        case 3:
                            ZToolPos.Text = string.Format("{0:F2}", ConnectionData.FeedBackZS3 + ConnectionData.ZOffset);
                            break;
                        case 4:
                            ZToolPos.Text = string.Format("{0:F2}", ConnectionData.FeedBackZSp + ConnectionData.ZOffset);
                            break;
                        case 5:
                            ZToolPos.Text = string.Format("{0:F2}", ConnectionData.FeedBackZPF + ConnectionData.ZOffset);
                            break;
                        default:
                            break;
                    }

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

                    if (ConnectionData.ZS1RLimit)
                        {
                            ZS1RL.BackColor = Color.Red;
                        }
                    else
                        {
                            ZS1RL.BackColor = SystemColors.Control;
                        }
                    if (ConnectionData.ZS1LLimit)
                        {
                            ZS1LL.BackColor = Color.Red;
                        }
                    else
                        {
                            ZS1LL.BackColor = SystemColors.Control;
                        }

                    if (ConnectionData.ZS2RLimit)
                        {
                            ZS2RL.BackColor = Color.Red;
                        }
                    else
                        {
                            ZS2RL.BackColor = SystemColors.Control;
                        }
                    if (ConnectionData.ZS2LLimit)
                        {
                            ZS2LL.BackColor = Color.Red;
                        }
                    else
                        {
                            ZS2LL.BackColor = SystemColors.Control;
                        }

                    if (ConnectionData.ZS3RLimit)
                        {
                            ZS3RL.BackColor = Color.Red;
                        }
                    else
                        {
                            ZS3RL.BackColor = SystemColors.Control;
                        }
                    if (ConnectionData.ZS3LLimit)
                        {
                            ZS3LL.BackColor = Color.Red;
                        }
                    else
                        {
                            ZS3LL.BackColor = SystemColors.Control;
                        }

                    if (ConnectionData.ZSpRLimit)
                        {
                            ZSpRL.BackColor = Color.Red;
                        }
                    else
                        {
                            ZSpRL.BackColor = SystemColors.Control;
                        }
                    if (ConnectionData.ZSpLLimit)
                    {
                            ZSpLL.BackColor = Color.Red;
                        }
                    else
                        {
                            ZSpLL.BackColor = SystemColors.Control;
                        }

                    if (ConnectionData.ZPFRLimit)
                        {
                            ZPFRL.BackColor = Color.Red;
                        }
                    else
                        {
                            ZPFRL.BackColor = SystemColors.Control;
                        }
                    if (ConnectionData.ZPFLLimit)
                        {
                            ZPFLL.BackColor = Color.Red;
                        }
                    else
                        {
                            ZPFLL.BackColor = SystemColors.Control;
                        }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                Thread.Sleep(100);
            }

        }
*/
        int[] axes;                              // Массив осей для совместного перемещения
        int[] axes_zero;
        int[] directions;                       // Массив направлений движения для совместного перемещения
        double[] point;                         // Массив перемещений для совместного движения
        int head;                               // Номер головы по оси Z
        int prev_head;                          // Предыдущая голова

         // Установка скорости по оси X и Y
        private void XYVelocity_Scroll(object sender, EventArgs e)
        {
            ConnectionData.SetXYVel = (double)XYVelocity.Value / 100 * ConnectionData.MaxXYVel;
            XYVelocityTB.Text = string.Format("{0:F2}", ConnectionData.SetXYVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_0, ConnectionData.SetXYVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_1, ConnectionData.SetXYVel);
        }

        private void Zvelocity_Scroll(object sender, EventArgs e)
        {
            ConnectionData.SetZVel = (double)Zvelocity.Value / 100 * ConnectionData.MaxZVel;
            ZVelocityTB.Text = string.Format("{0:F2}", ConnectionData.SetZVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_2, ConnectionData.SetZVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_3, ConnectionData.SetZVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_4, ConnectionData.SetZVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_5, ConnectionData.SetZVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_6, ConnectionData.SetZVel);
        }

        private void PtVelocity_Scroll(object sender, EventArgs e)
        {
            ConnectionData.SetPtVel = (double)PtVelocity.Value / 100 * ConnectionData.MaxPtVel;
            PtVelocityTB.Text = string.Format("{0:F2}", ConnectionData.SetPtVel);
        }

        private void PFVelocity_Scroll(object sender, EventArgs e)
        {
            ConnectionData.SetPFVel = (double)PFVelocity.Value / 100 * ConnectionData.MaxPFVel;
            PFVelocityTB.Text = string.Format("{0:F2}", ConnectionData.SetPFVel);
        }

        private void CTVelocity_Scroll(object sender, EventArgs e)
        {
            ConnectionData.SetCTVel = (double)CTVelocity.Value / 100 * ConnectionData.MaxCTVel;

            CTVelocityTB.Text = string.Format("{0:F2}", ConnectionData.SetCTVel);
        }

        private void CBVelocity_Scroll(object sender, EventArgs e)
        {
            ConnectionData.SetCBVel = (double)CBVelocity.Value / 100 * ConnectionData.MaxCBVel;
            CBVelocityTB.Text = string.Format("{0:F2}", ConnectionData.SetCBVel);
        }

        private void Manual_frm_Load(object sender, EventArgs e)
        {
            Accuracy1_Btn.Text = string.Format("{0:F1}", ConnectionData.Accuracy1);
            Accuracy2_Btn.Text = string.Format("{0:F1}", ConnectionData.Accuracy2);
            if (ConnectionData.AccuracySelect == 1)
            {
                Accuracy1_Btn.BackColor = Color.YellowGreen;
                Accuracy2_Btn.BackColor = SystemColors.Control;
            }
            else if (ConnectionData.AccuracySelect == 2)
            {
                Accuracy2_Btn.BackColor = Color.YellowGreen;
                Accuracy1_Btn.BackColor = SystemColors.Control;
            }

            DoubleBuffered = true;
            axes = new int[3]
                  {
                        ConnectionData.Value.ACSC_AXIS_0,
                        ConnectionData.Value.ACSC_AXIS_1,
                        -1
                  };
        }
        private void moveAxis(int axis,int type_moving,double dist)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.ToPoint(type_moving, axis, dist);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void jogAxis(int axis, double veloc)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, axis, veloc);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void XPos001_Click(object sender, EventArgs e)
        {
            moveAxis(X, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  0.01);
        }


        private void Xpos01_Click(object sender, EventArgs e)
        {
            moveAxis(X, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  0.1);
        }

        private void PosX1_Click(object sender, EventArgs e)
        {
            moveAxis(X, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  1);
        }

        private void PosX10_Click(object sender, EventArgs e)
        {
            moveAxis(X, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  10);
        }

        private void XMin001_Click(object sender, EventArgs e)
        {
            moveAxis(X, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  -0.01);
        }

        private void XMin10_Click(object sender, EventArgs e)
        {
            moveAxis(X, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -10);
        }

        private void XMin01_Click(object sender, EventArgs e)
        {
            moveAxis(X, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -0.1);
        }

        private void XMin1_Click(object sender, EventArgs e)
        {
            moveAxis(X, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  -1);
        }

        private void YNeg001_Click(object sender, EventArgs e)
        {
            moveAxis(Y, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  -0.01);
        }

        private void YNeg01_Click(object sender, EventArgs e)
        {
            moveAxis(Y, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  -0.1);
        }

        private void YNeg1_Click(object sender, EventArgs e)
        {
            moveAxis(Y, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  -1);
        }

        private void YNeg10_Click(object sender, EventArgs e)
        {
            moveAxis(Y, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  -10);
        }

        private void YPos001_Click(object sender, EventArgs e)
        {
            moveAxis(Y, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  0.01);
        }

        private void YPos01_Click(object sender, EventArgs e)
        {
            moveAxis(Y, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  0.1);
        }

        private void YPos1_Click(object sender, EventArgs e)
        {
            moveAxis(Y, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  1);
        }

        private void YPos10_Click(object sender, EventArgs e)
        {
            moveAxis(Y, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,  10);
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

        private void HotPlateBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                if (HotPlateBtn.BackColor == SystemColors.Control)
                {
                    HotPlateBtn.BackColor = Color.YellowGreen;
                    ConnectionData.Value.WriteVariable(ConnectionData.Value.ReadVariable("OutVar", ConnectionData.Value.ACSC_NONE) + 1, "OutVar", ConnectionData.Value.ACSC_NONE); // Включение выхода OUT(0).0
                }
                else
                {
                    HotPlateBtn.BackColor = SystemColors.Control;
                    ConnectionData.Value.WriteVariable(ConnectionData.Value.ReadVariable("OutVar", ConnectionData.Value.ACSC_NONE) - 1, "OutVar", ConnectionData.Value.ACSC_NONE);
                }
            }
        }

        private void CoolingBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                if (CoolingBtn.BackColor == SystemColors.Control)
                {
                    CoolingBtn.BackColor = Color.YellowGreen;
                    ConnectionData.Value.WriteVariable(ConnectionData.Value.ReadVariable("OutVar", ConnectionData.Value.ACSC_NONE) + 2, "OutVar", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetOutput(3, 9, 1); // Включение выхода OUT(0).1
                }
                else
                {
                    CoolingBtn.BackColor = SystemColors.Control;
                    ConnectionData.Value.WriteVariable(ConnectionData.Value.ReadVariable("OutVar", ConnectionData.Value.ACSC_NONE) - 2, "OutVar", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetOutput(3, 9, 0); // Выключение выхода OUT(0).1
                }
            }
        }

        private void UVLightBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                if (UVLightBtn.BackColor == SystemColors.Control)
                {
                    UVLightBtn.BackColor = Color.YellowGreen;
                    ConnectionData.Value.WriteVariable(ConnectionData.Value.ReadVariable("OutVar", ConnectionData.Value.ACSC_NONE) + 4, "OutVar", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetOutput(3, 10, 1); // Включение выхода OUT(0).2
                }
                else
                {
                    UVLightBtn.BackColor = SystemColors.Control;
                    ConnectionData.Value.WriteVariable(ConnectionData.Value.ReadVariable("OutVar", ConnectionData.Value.ACSC_NONE) - 4, "OutVar", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetOutput(3, 10, 0); // Выключение выхода OUT(0).2
                }
            }
        }

        private void JogXPBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(X);
        }

        private void JogXMBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(X);
        }

        private void JogYMBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(Y);
        }

        private void JogYPBtn_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(Y);
        }

        private void JogYPBtn_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(Y, ConnectionData.SetXYVel);
        }

        private void JogXPBtn_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(X, ConnectionData.SetXYVel);
        }

        private void JogYMBtn_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(Y, -ConnectionData.SetXYVel);
        }

        private void JogXMBtn_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(X, -ConnectionData.SetXYVel);
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

        double minX = 0;
        double minY = 0;

        // Выбор дозатора S1 для отображения координат
        private void S1Btn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                if (S1Btn.BackColor == SystemColors.Control)
                {
                    
                    prev_head = head;
                    S1Btn.BackColor = Color.YellowGreen;
                    head = 1;
                    ConnectionData.HeadNum = 2;
                    S2Btn.BackColor = SystemColors.Control;
                    S3Btn.BackColor = SystemColors.Control;
                    S1GB.Enabled = true;
                    S2GB.Enabled = false;
                    S3GB.Enabled = false;
                    SpGB.Enabled = false;
                    PFGB.Enabled = false;
                    PF2GB.Enabled = false;

                    switch (prev_head)
                    {
                        case 1:
                            minX = ConnectionData.XZoffsets;
                            minY = ConnectionData.YZoffsets;
                            break;
                        case 2:
                            minX = ConnectionData.XUoffsets;
                            minY = ConnectionData.YUoffsets;
                            break;
                        case 3:
                            minX = ConnectionData.XVoffsets;
                            minY = ConnectionData.YVoffsets;
                            break;
                        case 4:
                            minX = ConnectionData.WXaxis;
                            minY = ConnectionData.WYaxis;
                            break;
                        case 5:
                            minX = ConnectionData.XAoffsets;
                            minY = ConnectionData.YAoffsets;
                            break;
                        default:
                            minX = 0;
                            minY = 0;
                            break;
                    }

                    OffsetX = ConnectionData.FeedBackX - ConnectionData.ToolX + ConnectionData.XZoffsets - minX;
                    OffsetY = ConnectionData.FeedBackY - ConnectionData.ToolY + ConnectionData.YZoffsets - minY;

                    // Включение осей шприца 1
                    //ConnectionData.Value.Enable(ConnectionData.Value.ACSC_AXIS_2);
                    //ConnectionData.Value.Enable(ConnectionData.Value.ACSC_AXIS_7);
                    // Выключение осей шприца 2
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_3);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_8);
                    // Выключение осей шприца 3
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_4);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_9);
                    // Выключение осей спрея
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_5);
                    // Выключение осей дозатора
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_6);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_10);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_11);

                    // Отключение спрея
                    SPOnOff.BackColor = SystemColors.Control;
                   // ConnectionData.Value.SetOutput(0, 3, 0); // Выключение выхода OUT(0).3
                }
            }
        }

        private void S2Btn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                if (S2Btn.BackColor == SystemColors.Control)
                {
                    prev_head = head;
                    S2Btn.BackColor = Color.YellowGreen;
                    head = 2;
                    ConnectionData.HeadNum = 3;
                    S1Btn.BackColor = SystemColors.Control;
                    S3Btn.BackColor = SystemColors.Control;
                    S1GB.Enabled = false;
                    S2GB.Enabled = true;
                    S3GB.Enabled = false;
                    SpGB.Enabled = false;
                    PFGB.Enabled = false;
                    PF2GB.Enabled = false;

                    switch (prev_head)
                    {
                        case 1:
                            minX = ConnectionData.XZoffsets;
                            minY = ConnectionData.YZoffsets;
                            break;
                        case 2:
                            minX = ConnectionData.XUoffsets;
                            minY = ConnectionData.YUoffsets;
                            break;
                        case 3:
                            minX = ConnectionData.XVoffsets;
                            minY = ConnectionData.YVoffsets;
                            break;
                        case 4:
                            minX = ConnectionData.WXaxis;
                            minY = ConnectionData.WYaxis;
                            break;
                        case 5:
                            minX = ConnectionData.XAoffsets;
                            minY = ConnectionData.YAoffsets;
                            break;
                        default:
                            minX = 0;
                            minY = 0;
                            break;
                    }

                    OffsetX = ConnectionData.FeedBackX - ConnectionData.ToolX + ConnectionData.XUoffsets - minX;
                    OffsetY = ConnectionData.FeedBackY - ConnectionData.ToolY + ConnectionData.YUoffsets - minY;

                    // Включение осей шприца 2
                    //ConnectionData.Value.Enable(ConnectionData.Value.ACSC_AXIS_3);
                    //ConnectionData.Value.Enable(ConnectionData.Value.ACSC_AXIS_8);
                    // Выключение осей шприца 1
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_2);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_7);
                    // Выключение осей шприца 3
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_4);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_9);
                    // Выключение осей спрея
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_5);
                    // Выключение осей дозатора
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_6);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_10);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_11);

                    // Отключение спрея
                    SPOnOff.BackColor = SystemColors.Control;
                    //ConnectionData.Value.SetOutput(0, 3, 0); // Выключение выхода OUT(0).3
                }
            }
        }

        private void S3Btn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                if (S3Btn.BackColor == SystemColors.Control)
                {
                    prev_head = head;
                    S3Btn.BackColor = Color.YellowGreen;
                    head = 3;
                    ConnectionData.HeadNum = 4;
                    S1Btn.BackColor = SystemColors.Control;
                    S2Btn.BackColor = SystemColors.Control;
                    S1GB.Enabled = false;
                    S2GB.Enabled = false;
                    S3GB.Enabled = true;
                    SpGB.Enabled = false;
                    PFGB.Enabled = false;
                    PF2GB.Enabled = false;

                    switch (prev_head)
                    {
                        case 1:
                            minX = ConnectionData.XZoffsets;
                            minY = ConnectionData.YZoffsets;
                            break;
                        case 2:
                            minX = ConnectionData.XUoffsets;
                            minY = ConnectionData.YUoffsets;
                            break;
                        case 3:
                            minX = ConnectionData.XVoffsets;
                            minY = ConnectionData.YVoffsets;
                            break;
                        case 4:
                            minX = ConnectionData.WXaxis;
                            minY = ConnectionData.WYaxis;
                            break;
                        case 5:
                            minX = ConnectionData.XAoffsets;
                            minY = ConnectionData.YAoffsets;
                            break;
                        default:
                            minX = 0;
                            minY = 0;
                            break;
                    }

                    OffsetX = ConnectionData.FeedBackX - ConnectionData.ToolX + ConnectionData.XVoffsets - minX;
                    OffsetY = ConnectionData.FeedBackY - ConnectionData.ToolY + ConnectionData.YVoffsets - minY;

                    // Включение осей шприца 3
                    //ConnectionData.Value.Enable(ConnectionData.Value.ACSC_AXIS_4);
                    //ConnectionData.Value.Enable(ConnectionData.Value.ACSC_AXIS_9);
                    // Выключение осей шприца 1
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_2);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_7);
                    // Выключение осей шприца 2
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_3);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_8);
                    // Выключение осей спрея
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_5);
                    // Выключение осей дозатора
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_6);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_10);
                    //ConnectionData.Value.Disable(ConnectionData.Value.ACSC_AXIS_11);

                    // Отключение спрея
                    SPOnOff.BackColor = SystemColors.Control;
                    //ConnectionData.Value.SetOutput(0, 3, 0); // Выключение выхода OUT(0).3
                }
            }

        }

       
        

        private void SetWorkZero_Click(object sender, EventArgs e)
        {
            ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_4);


            /* point = new double[3]
                            {
                                0,
                                0,
                                0
                            };
            if (ConnectionData.bConnected)
            {
                try
                {
                     switch (head)
                     {
                         case 1:
                            //          point[0] = ConnectionData.XZoffsets;
                            //          point[1] = ConnectionData.YZoffsets;
                            axes_zero = new int[4]
                                    { 
                                        X,
                                        Y,
                                        Z1,
                                        -1
                                     };
                            break;
                         case 2:
                            //         point[0] = ConnectionData.XUoffsets;
                            //         point[1] = ConnectionData.YUoffsets;
                            axes_zero = new int[4]
                                    {
                                         X,
                                        Y,
                                        Z2,
                                        -1
                                    };
                            break;
                         case 3:
                            //         point[0] = ConnectionData.XVoffsets;
                            //         point[1] = ConnectionData.YVoffsets;
                            axes_zero = new int[4]
                                    {
                                         X,
                                        Y,
                                        Z3,
                                        -1
                                    };
                            break;
                        case 4:
                            axes_zero = new int[4]
                                    {
                                        ConnectionData.Value.ACSC_AXIS_0,
                                        ConnectionData.Value.ACSC_AXIS_1,
                                        -1,
                                        -1
                                    };
                            break;
                        case 5:
                            //         point[0] = ConnectionData.XAoffsets;
                            //         point[1] = ConnectionData.YAoffsets;
                            axes_zero = new int[4]
                                    {
                                        ConnectionData.Value.ACSC_AXIS_0,
                                        ConnectionData.Value.ACSC_AXIS_1,
                                        -1,
                                        -1
                                    };
                            break;
                         default:
                    //         point[0] = 0;
                    //         point[1] = 0;
      
                             break;
                     }
                    
                    point[0] = -ConnectionData.ToolX;
                    point[1] = -ConnectionData.ToolY; 
                    point[2] = -Convert.ToDouble(ZToolPos.Text);

                    ConnectionData.Value.ToPointM(ConnectionData.Value.ACSC_AMF_RELATIVE, axes_zero, point);
                    ConnectionData.Value.EndSequenceM(axes);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }*/


        }

        private void ErorMsg(COMException Ex)
        {
            string Str = Dict.LangStrings.Error + Ex.Source + "\n\r";
            Str = Str + Ex.Message + "\n\r";
            Str = Str + "HRESULT:" + String.Format("0x{0:X}", (Ex.ErrorCode));
            MessageBox.Show(Str, "EnableEvent");
        }

        private void SetMachineZero_Click(object sender, EventArgs e)
        {
            point = new double[2] { 0, 0 };
            if (ConnectionData.bConnected)
            {
                try
                    {
                    point[0] = 0;
                    point[1] = 0;
                        ConnectionData.Value.ToPointM(0, axes, point);
                        ConnectionData.Value.EndSequenceM(axes);
                    }
                catch (COMException Ex)
                    {
                        ErorMsg(Ex);
                    }
            }
        }

        private void ZS1Pos001_Click(object sender, EventArgs e)
        {
            moveAxis(Z1, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 0.01);
        }

        private void ZS1Pos01_Click(object sender, EventArgs e)
        {
            moveAxis(Z1, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 0.1);
        }

        private void ZS1Pos1_Click(object sender, EventArgs e)
        {
            moveAxis(Z1, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 1);
        }

        private void ZS1Pos10_Click(object sender, EventArgs e)
        {
            moveAxis(Z1, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 10);
        }

        private void ZS1Min001_Click(object sender, EventArgs e)
        {
            moveAxis(Z1, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -0.01);
        }

        private void ZS1Min01_Click(object sender, EventArgs e)
        {
            moveAxis(Z1, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -0.1);
        }

        private void ZS1Min1_Click(object sender, EventArgs e)
        {
            moveAxis(Z1, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -1);
        }

        private void ZS1Min10_Click(object sender, EventArgs e)
        {
            moveAxis(Z1, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -10);
        }

        private void JogZS1Pos_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(Z1);
        }

        private void JogZS1Min_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(Z1);
        }

        private void JogZS1Min_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(Z1, -ConnectionData.SetZVel);
        }

        private void JogZS1Pos_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(Z1, ConnectionData.SetZVel);
        }

        private void Manual_frm_Shown(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                string CurDir = Application.StartupPath + "\\Macro";
                MacroList.Items.Clear();
                
                XYVelocity.Value = (int)(100 * ConnectionData.SetXYVel / ConnectionData.MaxXYVel);
                Zvelocity.Value = (int)(100 * ConnectionData.SetZVel / ConnectionData.MaxZVel);
                Console.WriteLine("ConnectionData.SetPtVel: " + ConnectionData.SetPtVel);
                Console.WriteLine("ConnectionData.MaxPtVel: " + ConnectionData.MaxPtVel);
                PtVelocity.Value = (int)(100 * ConnectionData.SetPtVel / ConnectionData.MaxPtVel);
                /*PFVelocity.Value = (int)(100 * ConnectionData.SetPFVel / ConnectionData.MaxPFVel);
                CTVelocity.Value = (int)(100 * ConnectionData.SetCTVel / ConnectionData.MaxCTVel);
                CBVelocity.Value = (int)(100 * ConnectionData.SetCBVel / ConnectionData.MaxCBVel);*/
                MixTrackBar.Value = (int)(ConnectionData.PfMix);

                XYVelocityTB.Text = string.Format("{0:F2}", ConnectionData.SetXYVel);
                ZVelocityTB.Text =  string.Format("{0:F2}", ConnectionData.SetZVel);
                PtVelocityTB.Text = string.Format("{0:F2}", ConnectionData.SetPtVel);
                /*PFVelocityTB.Text = string.Format("{0:F2}", ConnectionData.SetPFVel);
                CTVelocityTB.Text = string.Format("{0:F2}", ConnectionData.SetCTVel);
                CBVelocityTB.Text = string.Format("{0:F2}", ConnectionData.SetCBVel);*/

                S1DiamTB.Text =     string.Format("{0:F2}", ConnectionData.S1Diameter);
                S2DiamTB.Text =     string.Format("{0:F2}", ConnectionData.S2Diameter);
                S3DiamTB.Text =     string.Format("{0:F2}", ConnectionData.S3Diameter);

                S1DiamPrTB.Text = string.Format("{0:F0}", ConnectionData.S1ProcDiameter);
                S2DiamPrTB.Text = string.Format("{0:F0}", ConnectionData.S2ProcDiameter);
                S3DiamPrTB.Text = string.Format("{0:F0}", ConnectionData.S3ProcDiameter);

                PfMixTB.Text =      string.Format("{0:F2}", ConnectionData.PfMix);
                PetriDishTB.Text =  string.Format("{0:F2}", ConnectionData.PetriDishDiam);
                WellCountTB.Text =  string.Format("{0:F2}", ConnectionData.WellNum);
                
                DirectoryInfo dir = new DirectoryInfo(@CurDir);

                foreach (var item in dir.GetFiles("*.mcr"))
                {
                    MacroList.Items.Add(item.Name);
                }

                //UpdateDataThread = new Thread(new ThreadStart(UpdateData));
                //UpdateDataThread.IsBackground = true;
                //UpdateDataThread.Start();

                OffsetX = ConnectionData.OffsetXZero;
                OffsetY = ConnectionData.OffsetYZero;

                switch (ConnectionData.ZeroHead)
                {
                    case 0:
                        S1GetZeroBtn.BackColor = SystemColors.Control;
                        S2GetZeroBtn.BackColor = SystemColors.Control;
                        S3GetZeroBtn.BackColor = SystemColors.Control;
                        PFGetZeroBtn.BackColor = SystemColors.Control;
                        SpGetZeroBtn.BackColor = SystemColors.Control;
                        break;
                    case 1:
                        S1GetZeroBtn.BackColor = Color.YellowGreen;
                        S2GetZeroBtn.BackColor = SystemColors.Control;
                        S3GetZeroBtn.BackColor = SystemColors.Control;
                        PFGetZeroBtn.BackColor = SystemColors.Control;
                        SpGetZeroBtn.BackColor = SystemColors.Control;
                        OffsetZ1 = ConnectionData.OffsetZZero;
                        ConnectionData.Value.WriteVariable(ConnectionData.XZero, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(ConnectionData.YZero, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(ConnectionData.ZZero, "ZGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(2, "PrevTool", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(2, "StartTool", ConnectionData.Value.ACSC_NONE);
                        StartTool = 1;
                        break;
                    case 2:
                        S1GetZeroBtn.BackColor = SystemColors.Control;
                        S2GetZeroBtn.BackColor = Color.YellowGreen;
                        S3GetZeroBtn.BackColor = SystemColors.Control;
                        PFGetZeroBtn.BackColor = SystemColors.Control;
                        SpGetZeroBtn.BackColor = SystemColors.Control;
                        OffsetZ2 = ConnectionData.OffsetZZero;
                        ConnectionData.Value.WriteVariable(ConnectionData.XZero, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(ConnectionData.YZero, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(ConnectionData.ZZero, "ZGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(3, "PrevTool", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(3, "StartTool", ConnectionData.Value.ACSC_NONE);
                        StartTool = 2;
                        break;
                    case 3:
                        S1GetZeroBtn.BackColor = SystemColors.Control;
                        S2GetZeroBtn.BackColor = SystemColors.Control;
                        S3GetZeroBtn.BackColor = Color.YellowGreen;
                        PFGetZeroBtn.BackColor = SystemColors.Control;
                        SpGetZeroBtn.BackColor = SystemColors.Control;
                        OffsetZ3 = ConnectionData.OffsetZZero;
                        ConnectionData.Value.WriteVariable(ConnectionData.XZero, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(ConnectionData.YZero, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(ConnectionData.ZZero, "ZGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(4, "PrevTool", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(4, "StartTool", ConnectionData.Value.ACSC_NONE);
                        StartTool = 3;
                        break;
                    case 4:
                        S1GetZeroBtn.BackColor = SystemColors.Control;
                        S2GetZeroBtn.BackColor = SystemColors.Control;
                        S3GetZeroBtn.BackColor = SystemColors.Control;
                        PFGetZeroBtn.BackColor = Color.YellowGreen;
                        SpGetZeroBtn.BackColor = SystemColors.Control;
                        OffsetW = ConnectionData.OffsetZZero;
                        ConnectionData.Value.WriteVariable(ConnectionData.XZero, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(ConnectionData.YZero, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(ConnectionData.ZZero, "ZGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(5, "PrevTool", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(5, "StartTool", ConnectionData.Value.ACSC_NONE);
                        StartTool = 4;
                        break;
                    case 5:
                        S1GetZeroBtn.BackColor = SystemColors.Control;
                        S2GetZeroBtn.BackColor = SystemColors.Control;
                        S3GetZeroBtn.BackColor = SystemColors.Control;
                        PFGetZeroBtn.BackColor = SystemColors.Control;
                        SpGetZeroBtn.BackColor = Color.YellowGreen;
                        OffsetA = ConnectionData.OffsetZZero;
                        ConnectionData.Value.WriteVariable(ConnectionData.XZero, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(ConnectionData.YZero, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(ConnectionData.ZZero, "ZGLOBAL", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(6, "PrevTool", ConnectionData.Value.ACSC_NONE);
                        ConnectionData.Value.WriteVariable(6, "StartTool", ConnectionData.Value.ACSC_NONE);
                        StartTool = 5;
                        break;
                    default:

                    break;
                }
            }
        }

        private void S1DiamTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.S1Diameter = Convert.ToDouble(S1DiamTB.Text);
                ConnectionData.S1ProcDiameter = Convert.ToDouble(S1DiamPrTB.Text);
                ConnectionData.Value.WriteVariable(ConnectionData.S1Diameter * ConnectionData.S1ProcDiameter / 100, "S1Diameter", ConnectionData.Value.ACSC_NONE);
            }
        }

        private void S2DiamTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.S2Diameter = Convert.ToDouble(S2DiamTB.Text);
                ConnectionData.S2ProcDiameter = Convert.ToDouble(S2DiamPrTB.Text);
                ConnectionData.Value.WriteVariable(ConnectionData.S2Diameter * ConnectionData.S2ProcDiameter / 100, "S2Diameter", ConnectionData.Value.ACSC_NONE);
            }
        }

        private void S3DiamTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.S3Diameter = Convert.ToDouble(S3DiamTB.Text);
                ConnectionData.S3ProcDiameter = Convert.ToDouble(S3DiamPrTB.Text);
                ConnectionData.Value.WriteVariable(ConnectionData.S3Diameter * ConnectionData.S3ProcDiameter / 100, "S3Diameter", ConnectionData.Value.ACSC_NONE);
            }
        }
         
        private void XYVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.SetXYVel = Convert.ToDouble(XYVelocityTB.Text);
                XYVelocity.Value = (int)(100 * ConnectionData.SetXYVel / ConnectionData.MaxXYVel);
                ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_0, ConnectionData.SetXYVel);
                ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_1, ConnectionData.SetXYVel);
            }
        }

        private void ZVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.SetZVel = Convert.ToDouble(ZVelocityTB.Text);
                Zvelocity.Value = (int)(100 * ConnectionData.SetZVel / ConnectionData.MaxZVel);
                ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_2, ConnectionData.SetZVel);
                ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_3, ConnectionData.SetZVel);
                ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_4, ConnectionData.SetZVel);
                ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_5, ConnectionData.SetZVel);
                ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_6, ConnectionData.SetZVel);
            }
        }

        private void PtVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.SetPtVel = Convert.ToDouble(PtVelocityTB.Text);
                PtVelocity.Value = (int)(100 * ConnectionData.SetPtVel / ConnectionData.MaxPtVel);
                //ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_7, ConnectionData.SetPtVel);
                ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_8, ConnectionData.SetPtVel);
                ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_9, ConnectionData.SetPtVel);
            }
        }

        private void PFVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.SetPFVel = Convert.ToDouble(PFVelocityTB.Text);
                PFVelocity.Value = (int)(100 * ConnectionData.SetPFVel / ConnectionData.MaxPFVel);
            }
        }

        private void CTVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.SetCTVel = Convert.ToDouble(CTVelocityTB.Text);
                CTVelocity.Value = (int)(100 * ConnectionData.SetCTVel / ConnectionData.MaxCTVel);
            }
        }

        private void CBVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.SetCBVel = Convert.ToDouble(CBVelocityTB.Text);
                CBVelocity.Value = (int)(100 * ConnectionData.SetCBVel / ConnectionData.MaxCBVel);
            }
        }

        private void MixTrackBar_Scroll(object sender, EventArgs e)
        {
            ConnectionData.PfMix = (double)MixTrackBar.Value;
            PfMixTB.Text = string.Format("{0:F2}", ConnectionData.PfMix);
        }

        private void PfMixTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.PfMix = Convert.ToDouble(PfMixTB.Text);
                MixTrackBar.Value = (int)(ConnectionData.PfMix);
            }
        }

        private void CustomRB_CheckedChanged(object sender, EventArgs e)
        {
            PetriDishRB.Checked = false;
            WellCNTRB.Checked = false;
            PetriDishTB.Enabled = false;
            WellCountTB.Enabled = false;
            ConnectionData.DrawMode = 0;
        }

        private void PetriDishRB_CheckedChanged(object sender, EventArgs e)
        {
            CustomRB.Checked = false;
            WellCNTRB.Checked = false;
            PetriDishTB.Enabled = true;
            WellCountTB.Enabled = false;
            ConnectionData.DrawMode = 10;
        }

        private void WellCNTRB_CheckedChanged(object sender, EventArgs e)
        {

            CustomRB.Checked = false;
            PetriDishRB.Checked = false;
            PetriDishTB.Enabled = false;
            WellCountTB.Enabled = true;
            ConnectionData.DrawMode = 20;
        }

        private void CustomRB_Click(object sender, EventArgs e)
        {
            CustomRB.Checked = true;
        }

        private void PetriDishRB_Click(object sender, EventArgs e)
        {
            PetriDishRB.Checked = true;
        }

        private void WellCNTRB_Click(object sender, EventArgs e)
        {
            WellCNTRB.Checked = true;
        }

        private void PetriDishTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.PetriDishDiam = Convert.ToDouble(PetriDishTB.Text);
            }
        }

        private void WellCountTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.WellNum = Convert.ToDouble(WellCountTB.Text);
            }
        }

        private void SPOnOff_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                if (SPOnOff.BackColor == SystemColors.Control)
                {
                    SPOnOff.BackColor = Color.YellowGreen;
                    ConnectionData.Value.WriteVariable(ConnectionData.Value.ReadVariable("OutVar", ConnectionData.Value.ACSC_NONE) + 8, "OutVar", ConnectionData.Value.ACSC_NONE); // Включение выхода OUT(0).0

                   // ConnectionData.Value.SetOutput(3, 11, 1); // Включение выхода OUT(0).3
                }
                else
                {
                    SPOnOff.BackColor = SystemColors.Control;
                    ConnectionData.Value.WriteVariable(ConnectionData.Value.ReadVariable("OutVar", ConnectionData.Value.ACSC_NONE) - 8, "OutVar", ConnectionData.Value.ACSC_NONE); // Включение выхода OUT(0).0

                    //ConnectionData.Value.SetOutput(3, 11, 0); // Выключение выхода OUT(0).3
                }
            }
        }

        private void JogZS2Pos_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(Z2, ConnectionData.SetZVel);
        }

        private void JogZS2Pos_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(Z2);
        }

        private void JogZS3Pos_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(Z3, ConnectionData.SetZVel);
        }

        private void JogZS3Pos_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(Z3);
        }

        private void JogZSPPos_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                   // ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_5, ConnectionData.SetZVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void JogZSPPos_MouseUp(object sender, MouseEventArgs e)
        {
            //ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_5);
        }

        private void JogZPfPos_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                   // ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_6, ConnectionData.SetZVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void JogZPfPos_MouseUp(object sender, MouseEventArgs e)
        {
            //ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_6);
        }

        private void JogZS2Min_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(Z2, -ConnectionData.SetZVel);
        }

        private void JogZS2Min_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(Z2);
        }

        private void JogZS3Min_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(Z3, -ConnectionData.SetZVel);
        }

        private void JogZS3Min_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(Z3);
        }

        private void JogZSPMin_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_5, -ConnectionData.SetZVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void JogZSPMin_MouseUp(object sender, MouseEventArgs e)
        {
            //ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_5);
        }

        private void JogZPfMin_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_6, -ConnectionData.SetZVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void JogZPfMin_MouseUp(object sender, MouseEventArgs e)
        {
           // ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_6);
        }

        private void ZS2Pos10_Click(object sender, EventArgs e)
        {
            moveAxis(Z2, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 10);
        }
        private void ZS2Pos1_Click(object sender, EventArgs e)
        {
            moveAxis(Z2, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 1);
        }
        private void ZS2Pos01_Click(object sender, EventArgs e)
        {
            moveAxis(Z2, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 0.1);
        }
        private void ZS2Pos001_Click(object sender, EventArgs e)
        {
            moveAxis(Z2, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 0.01);
        }

        private void ZS2Min001_Click(object sender, EventArgs e)
        {
            moveAxis(Z2, (int)ConnectionData.Value.ACSC_AMF_RELATIVE,-0.01);
        }

        private void ZS2Min01_Click(object sender, EventArgs e)
        {
            moveAxis(Z2, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -0.1);
        }

        private void ZS2Min1_Click(object sender, EventArgs e)
        {
            moveAxis(Z2, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -1);
        }

        private void ZS2Min10_Click(object sender, EventArgs e)
        {
            moveAxis(Z2, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -10);
        }

        private void ZS3Pos10_Click(object sender, EventArgs e)
        {
            moveAxis(Z3, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 10);
        }

        private void ZS3Pos1_Click(object sender, EventArgs e)
        {
            moveAxis(Z3, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 1);
        }

        private void ZS3Pos01_Click(object sender, EventArgs e)
        {
            moveAxis(Z3, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 0.1);
        }

        private void ZS3Pos001_Click(object sender, EventArgs e)
        {
            moveAxis(Z3, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, 0.01);
        }

        private void ZS3Min001_Click(object sender, EventArgs e)
        {
            moveAxis(Z3, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -0.01);
        }

        private void ZS3Min01_Click(object sender, EventArgs e)
        {
            moveAxis(Z3, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -0.1);
        }

        private void ZS3Min1_Click(object sender, EventArgs e)
        {
            moveAxis(Z3, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -1);
        }

        private void ZS3Min10_Click(object sender, EventArgs e)
        {
            moveAxis(Z3, (int)ConnectionData.Value.ACSC_AMF_RELATIVE, -10);
        }

        private void ZSPPos10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_5, 10);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZSPPos1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_5, 1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZSPPos01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_5, 0.1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZSPPos001_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                   // ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_5, 0.01);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZSPMin001_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_5, -0.01);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZSPMin01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                   // ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_5, -0.1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZSPMin1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                   // ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_5, -1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZSPMin10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                   // ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_5, -10);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZPfPos10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                   // ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_6, 10);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZPfPos1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_6, 1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZPfPos01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_6, 0.1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZPfPos001_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_6, 0.01);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZPfMin001_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_6, -0.01);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZPfMin01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                   // ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_6, -0.1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZPfMin1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                  //  ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_6, -1);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZPfMin10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                   // ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_6, -10);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void JogS1PistPos_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(F1, ConnectionData.SetPtVel);
        }

        private void JogS1PistPos_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(F1);
        }

        private void JogS1PistMin_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(F1, -ConnectionData.SetPtVel);
        }

        private void JogS1PistMin_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(F1);
        }

        private void JogS2PistPos_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(F2, ConnectionData.SetPtVel);
        }

        private void JogS2PistPos_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(F2);
        }

        private void JogS2PistMin_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(F2, -ConnectionData.SetPtVel);
        }

        private void JogS2PistMin_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(F2);
        }

        private void JogS3PistPos_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(F3, ConnectionData.SetPtVel);
        }

        private void JogS3PistPos_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(F3);
        }

        private void JogS3PistMin_MouseDown(object sender, MouseEventArgs e)
        {
            jogAxis(F3, -ConnectionData.SetPtVel);
        }

        private void JogS3PistMin_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(F3);
        }

        private void JogPfPistPos_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    double cnt = 0.005 * ConnectionData.PfMix + 0.5;
                    ConnectionData.Value.WriteVariable(ConnectionData.SetPFVel * (1 - cnt), "XPF1", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.SetPFVel * cnt, "XPF2", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_14, ConnectionData.SetPFVel * (1 - cnt));
                    //ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_15, ConnectionData.SetPFVel * cnt);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void JogPfPistPos_MouseUp(object sender, MouseEventArgs e)
        {
            // ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_14);
            // ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_15);
            ConnectionData.Value.WriteVariable(0, "XPF1", ConnectionData.Value.ACSC_NONE);
            ConnectionData.Value.WriteVariable(0, "XPF2", ConnectionData.Value.ACSC_NONE);
        }

        private void JogPfPistMin_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    double cnt = 0.005 * ConnectionData.PfMix + 0.5;
                    //ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_14, -ConnectionData.SetPFVel * (1 - cnt));
                    //ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_15, -ConnectionData.SetPFVel * cnt);
                    ConnectionData.Value.WriteVariable(-ConnectionData.SetPFVel * (1 - cnt), "XPF1", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(-ConnectionData.SetPFVel * cnt, "XPF2", ConnectionData.Value.ACSC_NONE);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void JogPfPistMin_MouseUp(object sender, MouseEventArgs e)
        {
            //ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_14);
            //ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_15);
            ConnectionData.Value.WriteVariable(0, "XPF1", ConnectionData.Value.ACSC_NONE);
            ConnectionData.Value.WriteVariable(0, "XPF2", ConnectionData.Value.ACSC_NONE);
        }

        private void moveDisp(int axis, string diam, double volume)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    double Dia = Convert.ToDouble(diam);
                    ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, axis, volume / ((Math.PI * Math.Pow(Dia, 2) / 4)));
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }
        private void S1PistPos100_Click(object sender, EventArgs e)
        {
            moveDisp(F1, S1DiamTB.Text, 100);
        }

        private void S1PistPos10_Click(object sender, EventArgs e)
        {
            moveDisp(F1, S1DiamTB.Text, 10);
        }

        private void S1PistPos1_Click(object sender, EventArgs e)
        {
            moveDisp(F1, S1DiamTB.Text, 1);
        }

        private void S1PistPos01_Click(object sender, EventArgs e)
        {
            moveDisp(F1, S1DiamTB.Text, 0.1);
        }

        private void S1PistMin01_Click(object sender, EventArgs e)
        {
            moveDisp(F1, S1DiamTB.Text, -0.1);
        }

        private void S1PistMin1_Click(object sender, EventArgs e)
        {
            moveDisp(F1, S1DiamTB.Text, -1);
        }

        private void S1PistMin10_Click(object sender, EventArgs e)
        {
            moveDisp(F1, S1DiamTB.Text, -10);
        }

        private void S1PistMin100_Click(object sender, EventArgs e)
        {
            moveDisp(F1, S1DiamTB.Text, -100);
        }

        private void S2PistPos100_Click(object sender, EventArgs e)
        {
            moveDisp(F2, S2DiamTB.Text, 100);
        }

        private void S2PistPos10_Click(object sender, EventArgs e)
        {
            moveDisp(F2, S2DiamTB.Text, 10);
        }

        private void S2PistPos1_Click(object sender, EventArgs e)
        {
            moveDisp(F2, S2DiamTB.Text, 1);
        }

        private void S2PistPos01_Click(object sender, EventArgs e)
        {
            moveDisp(F2, S2DiamTB.Text, 0.1);
        }

        private void S2PistMin01_Click(object sender, EventArgs e)
        {
            moveDisp(F2, S2DiamTB.Text, -0.1);
        }

        private void S2PistMin1_Click(object sender, EventArgs e)
        {
            moveDisp(F2, S2DiamTB.Text, -1);
        }

        private void S2PistMin10_Click(object sender, EventArgs e)
        {
            moveDisp(F2, S2DiamTB.Text,-10);
        }

        private void S2PistMin100_Click(object sender, EventArgs e)
        {
            moveDisp(F2, S2DiamTB.Text, -100);
        }

        private void S3PistPos100_Click(object sender, EventArgs e)
        {
            moveDisp(F3, S3DiamTB.Text, 100);
        }

        private void S3PistPos10_Click(object sender, EventArgs e)
        {
            moveDisp(F3, S3DiamTB.Text, 10);
        }

        private void S3PistPos1_Click(object sender, EventArgs e)
        {
            moveDisp(F3, S3DiamTB.Text, 1);
        }

        private void S3PistPos01_Click(object sender, EventArgs e)
        {
            moveDisp(F3, S3DiamTB.Text,0.1);
        }

        private void S3PistMin01_Click(object sender, EventArgs e)
        {
            moveDisp(F3, S3DiamTB.Text, -0.1);
        }

        private void S3PistMin1_Click(object sender, EventArgs e)
        {
            moveDisp(F3, S3DiamTB.Text, -1);
        }

        private void S3PistMin10_Click(object sender, EventArgs e)
        {
            moveDisp(F3, S3DiamTB.Text, -10);
        }

        private void S3PistMin100_Click(object sender, EventArgs e)
        {
            moveDisp(F3, S3DiamTB.Text, -100);
        }

        private void PfPistPos100_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    Preeflow_Timer.Enabled = false;
                    double cnt = 0.005 * ConnectionData.PfMix + 0.5;
                    TimerTime = 0;
                    Preeflow_Timer.Enabled = true;
                    StopTime = (4 * 100 / (Math.PI * Math.Pow(ConnectionData.PF1Diameter, 2)) / ConnectionData.SetPFVel / 60 * 100);
                    ConnectionData.Value.WriteVariable(ConnectionData.SetPFVel * (1 - cnt), "XPF1", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.SetPFVel * cnt, "XPF2",       ConnectionData.Value.ACSC_NONE);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void PfPistPos10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    Preeflow_Timer.Enabled = false;
                    double cnt = 0.005 * ConnectionData.PfMix + 0.5;
                    TimerTime = 0;
                    Preeflow_Timer.Enabled = true;
                    StopTime = (4 * 10 / (Math.PI * Math.Pow(ConnectionData.PF1Diameter, 2)) / ConnectionData.SetPFVel / 60 * 100);
                    ConnectionData.Value.WriteVariable(ConnectionData.SetPFVel * (1 - cnt), "XPF1", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.SetPFVel * cnt, "XPF2",       ConnectionData.Value.ACSC_NONE);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void PfPistPos1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    Preeflow_Timer.Enabled = false;
                    double cnt = 0.005 * ConnectionData.PfMix + 0.5;
                    TimerTime = 0;
                    Preeflow_Timer.Enabled = true;
                    StopTime = (4 * 1 / (Math.PI * Math.Pow(ConnectionData.PF1Diameter, 2)) / ConnectionData.SetPFVel / 60 * 100);
                    ConnectionData.Value.WriteVariable(ConnectionData.SetPFVel * (1 - cnt), "XPF1", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.SetPFVel * cnt, "XPF2",       ConnectionData.Value.ACSC_NONE);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void PfPistPos01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    Preeflow_Timer.Enabled = false;
                    double cnt = 0.005 * ConnectionData.PfMix + 0.5;
                    TimerTime = 0;
                    Preeflow_Timer.Enabled = true;
                    StopTime = (4 * 0.1 / (Math.PI * Math.Pow(ConnectionData.PF1Diameter, 2)) / ConnectionData.SetPFVel / 60 * 100);
                    ConnectionData.Value.WriteVariable(ConnectionData.SetPFVel * (1 - cnt), "XPF1", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.SetPFVel * cnt, "XPF2",       ConnectionData.Value.ACSC_NONE);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void PfPistMin01_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    Preeflow_Timer.Enabled = false;
                    double cnt = 0.005 * ConnectionData.PfMix + 0.5;
                    TimerTime = 0;
                    Preeflow_Timer.Enabled = true;
                    StopTime = (4 * 0.1 / (Math.PI * Math.Pow(ConnectionData.PF1Diameter, 2)) / ConnectionData.SetPFVel / 60 * 100);
                    ConnectionData.Value.WriteVariable(-ConnectionData.SetPFVel * (1 - cnt), "XPF1", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(-ConnectionData.SetPFVel * cnt, "XPF2",       ConnectionData.Value.ACSC_NONE);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void PfPistMin1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    Preeflow_Timer.Enabled = false;
                    double cnt = 0.005 * ConnectionData.PfMix + 0.5;
                    TimerTime = 0;
                    Preeflow_Timer.Enabled = true;
                    StopTime = (4 * 1 / (Math.PI * Math.Pow(ConnectionData.PF1Diameter, 2)) / ConnectionData.SetPFVel / 60 * 100);
                    ConnectionData.Value.WriteVariable(-ConnectionData.SetPFVel * (1 - cnt), "XPF1", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(-ConnectionData.SetPFVel * cnt, "XPF2",       ConnectionData.Value.ACSC_NONE);

                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void PfPistMin10_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    Preeflow_Timer.Enabled = false;
                    double cnt = 0.005 * ConnectionData.PfMix + 0.5;
                    TimerTime = 0;
                    Preeflow_Timer.Enabled = true;
                    StopTime = (4 * 10 / (Math.PI * Math.Pow(ConnectionData.PF1Diameter, 2)) / ConnectionData.SetPFVel / 60 * 100);
                    ConnectionData.Value.WriteVariable(-ConnectionData.SetPFVel * (1 - cnt), "XPF1", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(-ConnectionData.SetPFVel * cnt, "XPF2", ConnectionData.Value.ACSC_NONE);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void PfPistMin100_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    Preeflow_Timer.Enabled = false;
                    double cnt = 0.005 * ConnectionData.PfMix + 0.5;
                    TimerTime = 0;
                    Preeflow_Timer.Enabled = true;
                    StopTime = (4 * 100 / (Math.PI * Math.Pow(ConnectionData.PF1Diameter, 2)) / ConnectionData.SetPFVel / 60 * 100);
                    ConnectionData.Value.WriteVariable(-ConnectionData.SetPFVel * (1 - cnt), "XPF1", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(-ConnectionData.SetPFVel * cnt, "XPF2", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_14, -100 * (1 - cnt));
                    //ConnectionData.Value.ToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_15, -100 * (cnt));
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ModifyText(string ProgramFile, string ReplacedText, string ReplaceText)
        {
            string str = string.Empty;
            using (System.IO.StreamReader reader = System.IO.File.OpenText(@ProgramFile))
            {
                str = reader.ReadToEnd();
            }
            str = str.Replace(ReplacedText, ReplaceText);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@ProgramFile))
            {
                file.Write(str);
            }
        }

        private void S1HomeBtn_Click(object sender, EventArgs e)
        {
            //string Homefile = Application.StartupPath + "\\Data\\Homing.prg";
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ModifyText(Homefile, "iAxis = ", "iAxis = 2");                                  // Изменяем содержание файла поиска нулевой точки
                    //ConnectionData.Value.LoadBuffersFromFile(Homefile);                             // Загружаем данные в буфер
                    //OffsetZ = 0;
                    ConnectionData.Value.WriteVariable(Z1, "iAxis", ConnectionData.Value.ACSC_BUFFER_0);
                    ConnectionData.Value.WriteVariable(ConnectionData.HomeVelocityZ, "Veloc", ConnectionData.Value.ACSC_BUFFER_0);
                    ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_0);             // Выполняем буфер
                    //ModifyText(Homefile, "iAxis = 2", "iAxis = ");                                  // Изменяем файл в исходное состояние для следующего использования
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void S2HomeBtn_Click(object sender, EventArgs e)
        {
            //string Homefile = Application.StartupPath + "\\Data\\Homing.prg";
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ModifyText(Homefile, "iAxis = ", "iAxis = 3");                                  // Изменяем содержание файла поиска нулевой точки
                    //ConnectionData.Value.LoadBuffersFromFile(Homefile);                             // Загружаем данные в буфер
                    //OffsetU = 0;
                    ConnectionData.Value.WriteVariable(Z2, "iAxis", ConnectionData.Value.ACSC_BUFFER_0);
                    ConnectionData.Value.WriteVariable(ConnectionData.HomeVelocityZ, "Veloc", ConnectionData.Value.ACSC_BUFFER_0);
                    ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_0);             // Выполняем буфер
                    //ModifyText(Homefile, "iAxis = 3", "iAxis = ");                                  // Изменяем файл в исходное состояние для следующего использования
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void S3HomeBtn_Click(object sender, EventArgs e)
        {
            //string Homefile = Application.StartupPath + "\\Data\\Homing.prg";
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ModifyText(Homefile, "iAxis = ", "iAxis = 4");                                  // Изменяем содержание файла поиска нулевой точки
                    //ConnectionData.Value.LoadBuffersFromFile(Homefile);                             // Загружаем данные в буфер
                    //OffsetV = 0;
                    ConnectionData.Value.WriteVariable(Z3, "iAxis", ConnectionData.Value.ACSC_BUFFER_0);
                    ConnectionData.Value.WriteVariable(ConnectionData.HomeVelocityZ, "Veloc", ConnectionData.Value.ACSC_BUFFER_0);
                    ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_0);             // Выполняем буфер
                    //ModifyText(Homefile, "iAxis = 4", "iAxis = ");                                  // Изменяем файл в исходное состояние для следующего использования
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void button72_Click(object sender, EventArgs e)
        {
            //string Homefile = Application.StartupPath + "\\Data\\Homing.prg";
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ModifyText(Homefile, "iAxis = ", "iAxis = 5");                                  // Изменяем содержание файла поиска нулевой точки
                    //ConnectionData.Value.LoadBuffersFromFile(Homefile);                             // Загружаем данные в буфер
                    OffsetW = 0;
                    ConnectionData.Value.WriteVariable(5, "iAxis", ConnectionData.Value.ACSC_BUFFER_0);
                    ConnectionData.Value.WriteVariable(ConnectionData.HomeVelocityZ, "Veloc", ConnectionData.Value.ACSC_BUFFER_0);
                    ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_0);             // Выполняем буфер
                    //ModifyText(Homefile, "iAxis = 5", "iAxis = ");                                  // Изменяем файл в исходное состояние для следующего использования
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void PFHomeBtn_Click(object sender, EventArgs e)
        {
            //string Homefile = Application.StartupPath + "\\Data\\Homing.prg";
            if (ConnectionData.bConnected)
            {
                try
                {
                    //ModifyText(Homefile, "iAxis = ", "iAxis = 6");                                  // Изменяем содержание файла поиска нулевой точки
                    //ConnectionData.Value.LoadBuffersFromFile(Homefile);                             // Загружаем данные в буфер
                    //OffsetA = 0;
                    ConnectionData.Value.WriteVariable(6, "iAxis", ConnectionData.Value.ACSC_BUFFER_0);
                    ConnectionData.Value.WriteVariable(ConnectionData.HomeVelocityZ, "Veloc", ConnectionData.Value.ACSC_BUFFER_0);
                    ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_0);             // Выполняем буфер
                    //ModifyText(Homefile, "iAxis = 6", "iAxis = ");                                  // Изменяем файл в исходное состояние для следующего использования
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void S1GetZeroBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    S1GetZeroBtn.BackColor = Color.YellowGreen;
                    S2GetZeroBtn.BackColor = SystemColors.Control;
                    S3GetZeroBtn.BackColor = SystemColors.Control;
                    PFGetZeroBtn.BackColor = SystemColors.Control;
                    SpGetZeroBtn.BackColor = SystemColors.Control;
                    //   OffsetZ = OffsetZ + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_2);
                    //   OffsetX = OffsetX + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0);
                    //   OffsetY = OffsetY + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1);
                    //   ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0) + ConnectionData.GlobalX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //   ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1) + ConnectionData.GlobalY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //   ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_2, 0);
                    //   ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_0, 0);
                    //   ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_1, 0);
                    OffsetX = OffsetX + ConnectionData.ToolX;
                    OffsetY = OffsetY + ConnectionData.ToolY;
                    OffsetZ1 = OffsetZ1 + ConnectionData.FeedBackZ1; //Convert.ToDouble(ZToolPos.Text);
 
                    //OffsetGl = OffsetGl + Convert.ToDouble(ZToolPos.Text);

                    ConnectionData.OffsetXZero = OffsetX;
                    ConnectionData.OffsetYZero = OffsetY;
                    ConnectionData.OffsetZZero = OffsetZ1;
                    ConnectionData.XZero = ConnectionData.FeedBackX;
                    ConnectionData.YZero = ConnectionData.FeedBackY;
                    ConnectionData.ZZero = ConnectionData.FeedBackZ1;
                    ConnectionData.ZeroHead = 1;
                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackZ1, "ZGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(2, "PrevTool", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(2, "StartTool", ConnectionData.Value.ACSC_NONE);
                    StartTool = 1;
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void S2GetZeroBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    S2GetZeroBtn.BackColor = Color.YellowGreen;
                    S1GetZeroBtn.BackColor = SystemColors.Control;
                    S3GetZeroBtn.BackColor = SystemColors.Control;
                    PFGetZeroBtn.BackColor = SystemColors.Control;
                    SpGetZeroBtn.BackColor = SystemColors.Control;
                    //  OffsetU = OffsetU + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_3);
                    //  OffsetX = OffsetX + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0);
                    //  OffsetY = OffsetY + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0) + ConnectionData.GlobalX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1) + ConnectionData.GlobalY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_3, 0);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_0, 0);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_1, 0);
                    OffsetX = OffsetX + ConnectionData.ToolX;
                    OffsetY = OffsetY + ConnectionData.ToolY;
                    OffsetZ2 = OffsetZ2 + ConnectionData.FeedBackZ2; //Convert.ToDouble(ZToolPos.Text);
                                                                    //OffsetGl = OffsetGl + Convert.ToDouble(ZToolPos.Text);
                    ConnectionData.OffsetXZero = OffsetX;
                    ConnectionData.OffsetYZero = OffsetY;
                    ConnectionData.OffsetZZero = OffsetZ2;
                    ConnectionData.XZero = ConnectionData.FeedBackX;
                    ConnectionData.YZero = ConnectionData.FeedBackY;
                    ConnectionData.ZZero = ConnectionData.FeedBackZ2;
                    ConnectionData.ZeroHead = 2;

                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackZ2, "ZGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(3, "PrevTool", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(3, "StartTool", ConnectionData.Value.ACSC_NONE);
                    StartTool = 2;
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void S3GetZeroBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    S3GetZeroBtn.BackColor = Color.YellowGreen;
                    S1GetZeroBtn.BackColor = SystemColors.Control;
                    S2GetZeroBtn.BackColor = SystemColors.Control;
                    PFGetZeroBtn.BackColor = SystemColors.Control;
                    SpGetZeroBtn.BackColor = SystemColors.Control;
                    //  OffsetV = OffsetV + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_4);
                    //  OffsetX = OffsetX + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0);
                    //  OffsetY = OffsetY + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0) + ConnectionData.GlobalX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1) + ConnectionData.GlobalY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_4, 0);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_0, 0);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_1, 0);
                    OffsetX = OffsetX + ConnectionData.ToolX;
                    OffsetY = OffsetY + ConnectionData.ToolY;
                    OffsetZ3 = OffsetZ3 + ConnectionData.FeedBackZ3; //Convert.ToDouble(ZToolPos.Text);
                                                                    //OffsetGl = OffsetGl + Convert.ToDouble(ZToolPos.Text);
                    ConnectionData.OffsetXZero = OffsetX;
                    ConnectionData.OffsetYZero = OffsetY;
                    ConnectionData.OffsetZZero = OffsetZ3;
                    ConnectionData.XZero = ConnectionData.FeedBackX;
                    ConnectionData.YZero = ConnectionData.FeedBackY;
                    ConnectionData.ZZero = ConnectionData.FeedBackZ3;
                    ConnectionData.ZeroHead = 3;

                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackZ3, "ZGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(4, "PrevTool", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(4, "StartTool", ConnectionData.Value.ACSC_NONE);
                    StartTool = 3;
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void SpGetZeroBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    SpGetZeroBtn.BackColor = Color.YellowGreen;
                    S1GetZeroBtn.BackColor = SystemColors.Control;
                    S3GetZeroBtn.BackColor = SystemColors.Control;
                    PFGetZeroBtn.BackColor = SystemColors.Control;
                    S2GetZeroBtn.BackColor = SystemColors.Control;
                    // OffsetW = OffsetW + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_5);
                    // OffsetX = OffsetX + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0);
                    // OffsetY = OffsetY + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0) + ConnectionData.GlobalX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1) + ConnectionData.GlobalY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_5, 0);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_0, 0);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_1, 0);
                    OffsetX = OffsetX + ConnectionData.ToolX;
                    OffsetY = OffsetY + ConnectionData.ToolY;
                    OffsetW = OffsetW + ConnectionData.FeedBackZSp; //Convert.ToDouble(ZToolPos.Text);
                                                                    //OffsetGl = OffsetGl + Convert.ToDouble(ZToolPos.Text);

                    ConnectionData.OffsetXZero = OffsetX;
                    ConnectionData.OffsetYZero = OffsetY;
                    ConnectionData.OffsetZZero = OffsetW;
                    ConnectionData.XZero = ConnectionData.FeedBackX;
                    ConnectionData.YZero = ConnectionData.FeedBackY;
                    ConnectionData.ZZero = ConnectionData.FeedBackZSp;
                    ConnectionData.ZeroHead = 4;

                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(ConnectionData.FeedBackZSp, "ZGLOBAL", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(5, "PrevTool", ConnectionData.Value.ACSC_NONE);
                    ConnectionData.Value.WriteVariable(5, "StartTool", ConnectionData.Value.ACSC_NONE);
                    StartTool = 4;
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void PFGetZeroBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void EditMacroBtn_Click(object sender, EventArgs e)
        {
            if (MacroList.SelectedIndex != -1)
            {
                string Macrofile = Application.StartupPath + "\\Macro\\" + MacroList.SelectedItem.ToString();
                //создаем новый процесс
                Process proc = new Process();
                //Запускаем Блокнто
                proc.StartInfo.FileName = "Notepad.exe";
                proc.StartInfo.Arguments = Macrofile;
                proc.Start();
            }
        }

        private void SendMessageToPLC_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    HistoryBox.Text += SendTextBox.Text + Environment.NewLine;

                    ConnectionData.Value.LoadBuffer(ConnectionData.Value.ACSC_BUFFER_1, "N10 " + SendTextBox.Text);
                    ConnectionData.Value.AppendBuffer(ConnectionData.Value.ACSC_BUFFER_1,"N20 M02");
                    ConnectionData.Value.CompileBuffer(ConnectionData.Value.ACSC_BUFFER_1);
                    ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_1);
                }  
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
             }
        }

 /*       void Calibration(int Axis, double Point)
        {
            ConnectionData.Value.ExtToPoint(ConnectionData.Value.ACSC_AMF_VELOCITY, Axis, Point, ConnectionData.CalVelocityZ, ConnectionData.CalVelocityZ);
            ConnectionData.Value.WaitMotionEnd(Axis, 10000);
            ConnectionData.Value.ExtToPointM(ConnectionData.Value.ACSC_AMF_VELOCITY, axi
        }
        */
        private void S1CalibrateBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                if (S1CalibrateBtn.BackColor == SystemColors.Control)
                {
                    S1CalibrateBtn.BackColor = Color.YellowGreen;
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityX, "VelX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityY, "VelY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityZ, "VelZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceX, "DistX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceY, "DistY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceZ, "DistZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.ZXaxis, "StartX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.ZYaxis, "StartY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(0, "StartZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(Z1, "AxisNum", ConnectionData.Value.ACSC_BUFFER_3);
                    
                    if (ConnectionData.AccuracySelect == 1)
                    {
                        ConnectionData.Value.WriteVariable(ConnectionData.Accuracy1 / 1000, "ACCURACY", ConnectionData.Value.ACSC_BUFFER_3);
                    }
                    else if (ConnectionData.AccuracySelect == 2)
                    {
                        ConnectionData.Value.WriteVariable(ConnectionData.Accuracy2 / 1000, "ACCURACY", ConnectionData.Value.ACSC_BUFFER_3);
                    }
                    ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_3);
                    //ConnectionData.Value.SetOutput(0, 4, 1);        // Выключение выхода OUT(0).4
                }
                else
                {
                    S1CalibrateBtn.BackColor = SystemColors.Control;
                    ConnectionData.Value.KillM(axes);
                    ConnectionData.Value.StopBuffer(ConnectionData.Value.ACSC_BUFFER_3);
                    //ConnectionData.Value.SetOutput(0, 4, 0);        // Выключение выхода OUT(0).4
                }
            }
        }

        private void S2CalibrateBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                if (S2CalibrateBtn.BackColor == SystemColors.Control)
                {
                    S2CalibrateBtn.BackColor = Color.YellowGreen;
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityX, "VelX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityY, "VelY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityZ, "VelZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceX, "DistX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceY, "DistY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceZ, "DistZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.UXaxis, "StartX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.UYaxis, "StartY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(0, "StartZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(Z2, "AxisNum", ConnectionData.Value.ACSC_BUFFER_3);
                    if (ConnectionData.AccuracySelect == 1)
                    {
                        ConnectionData.Value.WriteVariable(ConnectionData.Accuracy1 / 1000, "ACCURACY", ConnectionData.Value.ACSC_BUFFER_3);
                    }
                    else if (ConnectionData.AccuracySelect == 2)
                    {
                        ConnectionData.Value.WriteVariable(ConnectionData.Accuracy2 / 1000, "ACCURACY", ConnectionData.Value.ACSC_BUFFER_3);
                    }
                    ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_3);
                    //ConnectionData.Value.SetOutput(0, 5, 1);        // Выключение выхода OUT(0).5
                }
                else
                {
                    S2CalibrateBtn.BackColor = SystemColors.Control;
                    ConnectionData.Value.KillM(axes);
                    ConnectionData.Value.StopBuffer(ConnectionData.Value.ACSC_BUFFER_3);
                    //ConnectionData.Value.SetOutput(0, 5, 0);        // Выключение выхода OUT(0).5
                }
            }
        }

        private void S3CalibrateBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                if (S3CalibrateBtn.BackColor == SystemColors.Control)
                {
                    S3CalibrateBtn.BackColor = Color.YellowGreen;
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityX, "VelX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityY, "VelY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityZ, "VelZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceX, "DistX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceY, "DistY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceZ, "DistZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.VXaxis, "StartX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.VYaxis, "StartY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(0, "StartZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(Z3, "AxisNum", ConnectionData.Value.ACSC_BUFFER_3);
                    if (ConnectionData.AccuracySelect == 1)
                    {
                        ConnectionData.Value.WriteVariable(ConnectionData.Accuracy1 / 1000, "ACCURACY", ConnectionData.Value.ACSC_BUFFER_3);
                    }
                    else if (ConnectionData.AccuracySelect == 2)
                    {
                        ConnectionData.Value.WriteVariable(ConnectionData.Accuracy2 / 1000, "ACCURACY", ConnectionData.Value.ACSC_BUFFER_3);
                    }
                    ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_3);
                    //ConnectionData.Value.SetOutput(0, 6, 1);        // Выключение выхода OUT(0).6
                }
                else
                {
                    S3CalibrateBtn.BackColor = SystemColors.Control;
                    ConnectionData.Value.KillM(axes);
                    ConnectionData.Value.StopBuffer(ConnectionData.Value.ACSC_BUFFER_3);
                    //ConnectionData.Value.SetOutput(0, 6, 0);        // Выключение выхода OUT(0).6
                }
            }
        }

        private void PFCalibrateBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                if (PFCalibrateBtn.BackColor == SystemColors.Control)
                {
                    PFCalibrateBtn.BackColor = Color.YellowGreen;
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityX, "VelX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityY, "VelY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalVelocityZ, "VelZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceX, "DistX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceY, "DistY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.CalDistanceZ, "DistZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.AXaxis, "StartX", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(ConnectionData.AYaxis, "StartY", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(0, "StartZ", ConnectionData.Value.ACSC_BUFFER_3);
                    ConnectionData.Value.WriteVariable(6, "AxisNum", ConnectionData.Value.ACSC_BUFFER_3);
                    if (ConnectionData.AccuracySelect == 1)
                    {
                        ConnectionData.Value.WriteVariable(ConnectionData.Accuracy1 / 1000, "ACCURACY", ConnectionData.Value.ACSC_BUFFER_3);
                    }
                    else if (ConnectionData.AccuracySelect == 2)
                    {
                        ConnectionData.Value.WriteVariable(ConnectionData.Accuracy2 / 1000, "ACCURACY", ConnectionData.Value.ACSC_BUFFER_3);
                    }
                    ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_3);
                    //ConnectionData.Value.SetOutput(0, 7, 1);        // Выключение выхода OUT(0).7
                }
                else
                {
                    PFCalibrateBtn.BackColor = SystemColors.Control;
                    ConnectionData.Value.KillM(axes);
                    ConnectionData.Value.StopBuffer(ConnectionData.Value.ACSC_BUFFER_3);
                    // ConnectionData.Value.SetOutput(0, 7, 0);        // Выключение выхода OUT(0).7
                }
            }
        }

        private void RunMacroBtn_Click(object sender, EventArgs e)
        {
            if (MacroList.SelectedIndex != -1)
            {
                if (ConnectionData.bConnected)
                {
                    try
                    {
                        string Macrofile = Application.StartupPath + "\\Macro\\" + MacroList.SelectedItem.ToString();
                        //HistoryBox.Text = Macrofile;
                        ConnectionData.Value.LoadBuffersFromFile(Macrofile);
                        ConnectionData.Value.CompileBuffer(ConnectionData.Value.ACSC_BUFFER_1);
                        ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_1);
                    }
                    catch (COMException Ex)
                    {
                        ErorMsg(Ex);
                    }

                }
            }
        }

        private void S1DiamTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void PetriDishTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void WellCountTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void S2DiamTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void S3DiamTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void XYVelocityTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void ZVelocityTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void PtVelocityTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void PFVelocityTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void CTVelocityTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void CBVelocityTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void PfMixTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
            }
        }

        private void Manual_frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
               // UpdateDataThread.Abort();//прерываем поток
            }
            catch
            {

            }
        }

        public void ChangeFormLanguage(AvaliableLocalizations newLocalization)
        {
            Sett sett1 = new Sett();
            sett1.SetCulture(newLocalization);

            var resources = new ComponentResourceManager(typeof(Manual_frm));
            CultureInfo newCultureInfo = new CultureInfo(EnumDescriptionHelper.GetEnumDescription(newLocalization));

            foreach (Control C in this.Controls)
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

        private void SetZeroXYS1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //SetZeroXYS1.BackColor = Color.YellowGreen;
                    //OffsetX = OffsetX + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0);
                    //OffsetY = OffsetY + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0) + ConnectionData.GlobalX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1) + ConnectionData.GlobalY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_0, 0);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_1, 0);
                    OffsetX = OffsetX + ConnectionData.ToolX;
                    OffsetY = OffsetY + ConnectionData.ToolY;
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void SetZeroXYS2_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //SetZeroXYS2.BackColor = Color.YellowGreen;
                    //OffsetX = OffsetX + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0);
                    //OffsetY = OffsetY + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0) + ConnectionData.GlobalX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1) + ConnectionData.GlobalY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_0, 0);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_1, 0);
                    OffsetX = OffsetX + ConnectionData.ToolX;
                    OffsetY = OffsetY + ConnectionData.ToolY;
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void SetZeroXYS3_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //SetZeroXYS3.BackColor = Color.YellowGreen;
                    //OffsetX = OffsetX + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0);
                    //OffsetY = OffsetY + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0) + ConnectionData.GlobalX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1) + ConnectionData.GlobalY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_0, 0);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_1, 0);
                    OffsetX = OffsetX + ConnectionData.ToolX;
                    OffsetY = OffsetY + ConnectionData.ToolY;
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void SetZeroXYSp_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    // SetZeroXYSp.BackColor = Color.YellowGreen;
                    //OffsetX = OffsetX + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0);
                    //OffsetY = OffsetY + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0) + ConnectionData.GlobalX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1) + ConnectionData.GlobalY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_0, 0);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_1, 0);
                    OffsetX = OffsetX + ConnectionData.ToolX;
                    OffsetY = OffsetY + ConnectionData.ToolY;
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void SetZeroXYPF_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    //SetZeroXYPF.BackColor = Color.YellowGreen;
                    //OffsetX = OffsetX + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0);
                    //OffsetY = OffsetY + ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_0) + ConnectionData.GlobalX, "XGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.WriteVariable(ConnectionData.Value.GetFPosition(ConnectionData.Value.ACSC_AXIS_1) + ConnectionData.GlobalY, "YGLOBAL", ConnectionData.Value.ACSC_NONE);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_0, 0);
                    //ConnectionData.Value.SetFPosition(ConnectionData.Value.ACSC_AXIS_1, 0);
                    OffsetX = OffsetX + ConnectionData.ToolX;
                    OffsetY = OffsetY + ConnectionData.ToolY;
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void Preeflow_Timer_Tick(object sender, EventArgs e)
        {
            if (TimerTime >= StopTime)
            {
                ConnectionData.Value.WriteVariable(0, "XPF1", ConnectionData.Value.ACSC_NONE);
                ConnectionData.Value.WriteVariable(0, "XPF2", ConnectionData.Value.ACSC_NONE);
                Preeflow_Timer.Enabled = false;
            }
            TimerTime = TimerTime + 1;
        }

        private void HistoryBox_TextChanged(object sender, EventArgs e)
        {
            HistoryBox.SelectionStart = HistoryBox.Text.Length;
            HistoryBox.ScrollToCaret();
        }

        private void Accuracy1_Btn_Click(object sender, EventArgs e)
        {
            if (Accuracy1_Btn.BackColor == SystemColors.Control)
            {
                Accuracy2_Btn.BackColor = SystemColors.Control;
                Accuracy1_Btn.BackColor = Color.YellowGreen;
                ConnectionData.AccuracySelect = 1;
            }
        }

        private void Accuracy2_Btn_Click(object sender, EventArgs e)
        {
            if (Accuracy2_Btn.BackColor == SystemColors.Control)
            {
                Accuracy1_Btn.BackColor = SystemColors.Control;
                Accuracy2_Btn.BackColor = Color.YellowGreen;
                ConnectionData.AccuracySelect = 2;
            }
        }

        private void S1DiamPrTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.S1Diameter = Convert.ToDouble(S1DiamTB.Text);
                ConnectionData.S1ProcDiameter = Convert.ToDouble(S1DiamPrTB.Text);
                ConnectionData.Value.WriteVariable(ConnectionData.S1Diameter * ConnectionData.S1ProcDiameter / 100, "S1Diameter", ConnectionData.Value.ACSC_NONE);
            }
        }

        private void S2DiamPrTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                {
                    ConnectionData.S2Diameter = Convert.ToDouble(S2DiamTB.Text);
                    ConnectionData.S2ProcDiameter = Convert.ToDouble(S2DiamPrTB.Text);
                    ConnectionData.Value.WriteVariable(ConnectionData.S2Diameter * ConnectionData.S2ProcDiameter / 100, "S2Diameter", ConnectionData.Value.ACSC_NONE);
                }
        }

        private void S3DiamPrTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                {
                    ConnectionData.S3Diameter = Convert.ToDouble(S3DiamTB.Text);
                    ConnectionData.S3ProcDiameter = Convert.ToDouble(S3DiamPrTB.Text);
                    ConnectionData.Value.WriteVariable(ConnectionData.S3Diameter * ConnectionData.S3ProcDiameter / 100, "S3Diameter", ConnectionData.Value.ACSC_NONE);
                }
        }

        private void S1DiamPrTB_KeyPress(object sender, KeyPressEventArgs e)
        {
                if (e.KeyChar == ',')
                    e.KeyChar = '.';
                if (e.KeyChar != 22)
                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
                else
                {
                    double d;
                    e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                    MessageBox.Show(Dict.LangStrings.InsertBuffer);
                }
        }

        private void S2DiamPrTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
                e.KeyChar = '.';
            if (e.KeyChar != 22)
                e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
            else
            {
                double d;
                e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                MessageBox.Show(Dict.LangStrings.InsertBuffer);
            }
        }

        private void S3DiamPrTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
                e.KeyChar = '.';
            if (e.KeyChar != 22)
                e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
            else
            {
                double d;
                e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                MessageBox.Show(Dict.LangStrings.InsertBuffer);
            }
        }
    }
}


//
// Библиотека
//
// {
//               if (e.KeyChar == ',')
//                    e.KeyChar = '.';
//                if (e.KeyChar != 22)
//                    e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back && (e.KeyChar != '-' || ((TextBox)sender).SelectionStart != 0 || (((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-")));
//                else
//                {
//                    double d;
//e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d< 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
//                   MessageBox.Show("Не удалось вставить содержимое буфера обмена");
//               }
//            }