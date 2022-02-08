using Active_Directory_Worker.Interfaces;
using Basler.Pylon;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace BTP
{
    public partial class Settings_frm : Form, ILanguageChangable
    {
        public Settings_frm()
        {
            InitializeComponent();
        }

        // Заполнение таблицы параметров
        private void Settings_frm_Load(object sender, EventArgs e)
        {
            IPAddressTB.Text = ConnectionData.ControllerIP.ToString();
            Camera1SNTB.Text = ConnectionData.Camera1SN;
            Camera2SNTB.Text = ConnectionData.Camera2SN;
            Camera1ScaleTB.Text = string.Format("{0:F2}", ConnectionData.ScaleCam1);
            Camera2ScaleTB.Text = string.Format("{0:F2}", ConnectionData.ScaleCam2);
            XYVelocityTB.Text = string.Format("{0:F2}", ConnectionData.MaxXYVel);
            ZVelocityTB.Text = string.Format("{0:F2}", ConnectionData.MaxZVel);
            PtVelocityTB.Text = string.Format("{0:F2}", ConnectionData.MaxPtVel);
            PFVelocityTB.Text = string.Format("{0:F2}", ConnectionData.MaxPFVel);
            CTVelocityTB.Text = string.Format("{0:F2}", ConnectionData.MaxCTVel);
            CBVelocityTB.Text = string.Format("{0:F2}", ConnectionData.MaxCBVel);
            XoffsetTB.Text = string.Format("{0:F2}", ConnectionData.XOffset);
            YoffsetTB.Text = string.Format("{0:F2}", ConnectionData.YOffset);
            ZS1OffsetTB.Text = string.Format("{0:F2}", ConnectionData.ZS1Offset);
            ZS2OffsetTB.Text = string.Format("{0:F2}", ConnectionData.ZS2Offset);
            ZS3OffsetTB.Text = string.Format("{0:F2}", ConnectionData.ZS3Offset);
            ZSPOffsetTB.Text = string.Format("{0:F2}", ConnectionData.ZSpOffset);
            ZPFOffsetTB.Text = string.Format("{0:F2}", ConnectionData.ZPFOffset);
            Camera2XStroke.Text = string.Format("{0:F2}", ConnectionData.Camera2XStrokeMax);
            Camera2YStroke.Text = string.Format("{0:F2}", ConnectionData.Camera2YStrokeMax);
            Camera1ZoomStroke.Text = string.Format("{0:F2}", ConnectionData.Camera1ZoomStrokeMax);
            CalVelX.Text = string.Format("{0:F2}", ConnectionData.CalVelocityX);
            CalVelY.Text = string.Format("{0:F2}", ConnectionData.CalVelocityY);
            CalVelZ.Text = string.Format("{0:F2}", ConnectionData.CalVelocityZ);
            CalDistX.Text = string.Format("{0:F2}", ConnectionData.CalDistanceX);
            CalDistY.Text = string.Format("{0:F2}", ConnectionData.CalDistanceY);
            CalDistZ.Text = string.Format("{0:F2}", ConnectionData.CalDistanceZ);
            HomeVelocityX.Text = string.Format("{0:F2}", ConnectionData.HomeVelocityX);
            HomeVelocityY.Text = string.Format("{0:F2}", ConnectionData.HomeVelocityY);
            HomeVelocityZ.Text = string.Format("{0:F2}", ConnectionData.HomeVelocityZ);
            HomeVelocityCam.Text = string.Format("{0:F2}", ConnectionData.HomeVelocityCam);
            AXAxisSP.Text = string.Format("{0:F2}", ConnectionData.AXaxis);
            AYAxisSP.Text = string.Format("{0:F2}", ConnectionData.AYaxis);
            ZXAxisSP.Text = string.Format("{0:F2}", ConnectionData.ZXaxis);
            ZYAxisSP.Text = string.Format("{0:F2}", ConnectionData.ZYaxis);
            VXAxisSP.Text = string.Format("{0:F2}", ConnectionData.VXaxis);
            VYAxisSP.Text = string.Format("{0:F2}", ConnectionData.VYaxis);
            UXAxisSP.Text = string.Format("{0:F2}", ConnectionData.UXaxis);
            UYAxisSP.Text = string.Format("{0:F2}", ConnectionData.UYaxis);
            WXAxisSP.Text = string.Format("{0:F2}", ConnectionData.WXaxis);
            WYAxisSP.Text = string.Format("{0:F2}", ConnectionData.WYaxis);
            if (ConnectionData.AccuracySelect == 1)
            {
                Accuracy50Select_CB.Checked = true;
                Accuracy100Select_CB.Checked = false;
            }
            else if (ConnectionData.AccuracySelect == 2)
            {
                Accuracy100Select_CB.Checked = true;
                Accuracy50Select_CB.Checked = false;
            }

            Accuracy50_TB.Text = string.Format("{0:F2}", ConnectionData.Accuracy1);
            Accuracy100_TB.Text = string.Format("{0:F2}", ConnectionData.Accuracy2);

            //PreeflowDiam_TB.Text = string.Format("{0:F2}", ConnectionData.PreeflowDiam);
            PF1Diameter_TB.Text = string.Format("{0:F2}", ConnectionData.PF1Diameter);
            PF2Diameter_TB.Text = string.Format("{0:F2}", ConnectionData.PF2Diameter);
            PetiXDistance.Text = string.Format("{0:F2}", ConnectionData.PetriX);
            PetiYDistance.Text = string.Format("{0:F2}", ConnectionData.PetriY);
        }

        // Sets the parameter displayed by the user control.

        public void ChangeFormLanguage(AvaliableLocalizations newLocalization)
        {
            Sett sett1 = new Sett();
            sett1.SetCulture(newLocalization);

            var resources = new ComponentResourceManager(typeof(Settings_frm));
            CultureInfo newCultureInfo = new CultureInfo(EnumDescriptionHelper.GetEnumDescription(newLocalization));

            foreach (Control C in Controls)
            {
                resources.ApplyResources(C, C.Name, newCultureInfo);
                if (C is GroupBox)
                {
                    foreach (Control G in C.Controls)
                    {
                        resources.ApplyResources(G, G.Name, newCultureInfo);
                        if (G is GroupBox)
                            foreach (Control F in G.Controls)
                                {
                                  resources.ApplyResources(F, F.Name, newCultureInfo);
                                }
                    }
                }
            }
        }

        private void IPAddressTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.ControllerIP = IPAddressTB.Text;
            }
        }

        private void Camera1SNTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.Camera1SN = Camera1SNTB.Text;
            }
        }

        private void Camera2SNTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.Camera2SN = Camera2SNTB.Text;
            }
        }

        private void Camera1ScaleTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.ScaleCam1 = Convert.ToDouble(Camera1ScaleTB.Text);
            }
        }

        private void Camera2ScaleTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.ScaleCam2 = Convert.ToDouble(Camera2ScaleTB.Text);
            }
        }

        private void XYVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.MaxXYVel = Convert.ToDouble(XYVelocityTB.Text);
            }
        }

        private void ZVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.MaxZVel = Convert.ToDouble(ZVelocityTB.Text);
            }
        }

        private void PtVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.MaxPtVel = Convert.ToDouble(PtVelocityTB.Text);
            }
        }

        private void PFVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.MaxPFVel = Convert.ToDouble(PFVelocityTB.Text);
            }
        }

        private void CTVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.MaxCTVel = Convert.ToDouble(CTVelocityTB.Text);
            }
        }

        private void CBVelocityTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.MaxCBVel = Convert.ToDouble(CBVelocityTB.Text);
            }
        }

        private void Camera1ScaleTB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Camera2ScaleTB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void XoffsetTB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void YoffsetTB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ZS1OffsetTB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ZS2OffsetTB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ZS3OffsetTB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ZSPOffsetTB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ZPFOffsetTB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void XoffsetTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.XOffset = Convert.ToDouble(XoffsetTB.Text);
            }
        }

        private void YoffsetTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.YOffset = Convert.ToDouble(YoffsetTB.Text);
            }
        }

        private void ZS1OffsetTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.ZS1Offset = Convert.ToDouble(ZS1OffsetTB.Text);
            }
        }

        private void ZS2OffsetTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.ZS2Offset = Convert.ToDouble(ZS2OffsetTB.Text);
            }
        }

        private void ZS3OffsetTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.ZS3Offset = Convert.ToDouble(ZS3OffsetTB.Text);
            }
        }

        private void ZSPOffsetTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.ZSpOffset = Convert.ToDouble(ZSPOffsetTB.Text);
            }
        }

        private void ZPFOffsetTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.ZPFOffset = Convert.ToDouble(ZPFOffsetTB.Text);
            }
        }

        private void IPAddressTB_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.ControllerIP = IPAddressTB.ToString();
            }
        }

        private void Camera2XStroke_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.Camera2XStrokeMax = Convert.ToDouble(Camera2XStroke.Text);
            }
        }

        private void Camera2YStroke_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.Camera2YStrokeMax = Convert.ToDouble(Camera2YStroke.Text);
            }
        }

        private void Camera2XStroke_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Camera2YStroke_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Camera1ZoomStroke_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.Camera1ZoomStrokeMax = Convert.ToDouble(Camera1ZoomStroke.Text);
            }
        }

        private void Camera1ZoomStroke_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CalDistX_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CalDistY_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CalDistZ_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CalVelX_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CalVelY_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CalVelZ_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CalDistX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.CalDistanceX = Convert.ToDouble(CalDistX.Text);
            }
        }

        private void CalDistY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.CalDistanceY = Convert.ToDouble(CalDistY.Text);
            }
        }

        private void CalDistZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.CalDistanceZ = Convert.ToDouble(CalDistZ.Text);
            }
        }

        private void CalVelX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.CalVelocityX = Convert.ToDouble(CalVelX.Text);
            }
        }

        private void CalVelY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.CalVelocityY = Convert.ToDouble(CalVelY.Text);
            }
        }

        private void CalVelZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.CalVelocityZ = Convert.ToDouble(CalVelZ.Text);
            }
        }

        private void HomeVelocity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void HomeVelocity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.HomeVelocityZ = Convert.ToDouble(HomeVelocityZ.Text);
            }
        }

        private void ZXAxisSP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ZYAxisSP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void UXAxisSP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void UYAxisSP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void VXAxisSP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void VYAxisSP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void AXAxisSP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void AYAxisSP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ZXAxisSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.ZXaxis = Convert.ToDouble(ZXAxisSP.Text);
            }
        }

        private void ZYAxisSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.ZYaxis = Convert.ToDouble(ZYAxisSP.Text);
            }
        }

        private void UXAxisSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.UXaxis = Convert.ToDouble(UXAxisSP.Text);
            }
        }

        private void UYAxisSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.UYaxis = Convert.ToDouble(UYAxisSP.Text);
            }
        }

        private void VXAxisSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.VXaxis = Convert.ToDouble(VXAxisSP.Text);
            }
        }

        private void VYAxisSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.VYaxis = Convert.ToDouble(VYAxisSP.Text);
            }
        }

        private void AXAxisSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.AXaxis = Convert.ToDouble(AXAxisSP.Text);
            }
        }

        private void AYAxisSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.AYaxis = Convert.ToDouble(AYAxisSP.Text);
            }
        }

        private void HomeVelocityX_KeyPress(object sender, KeyPressEventArgs e)
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

        private void HomeVelocityY_KeyPress(object sender, KeyPressEventArgs e)
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

        private void HomeVelocityCam_KeyPress(object sender, KeyPressEventArgs e)
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

        private void HomeVelocityX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.HomeVelocityX = Convert.ToDouble(HomeVelocityX.Text);
            }
        }

        private void HomeVelocityY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.HomeVelocityY = Convert.ToDouble(HomeVelocityY.Text);
            }
        }

        private void HomeVelocityCam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.HomeVelocityCam = Convert.ToDouble(HomeVelocityCam.Text);
            }
        }

        private void Camera1GainTrackBar_Scroll(object sender, EventArgs e)
        {
            ConnectionData.Camera1.Parameters[PLCamera.Gain].SetValue(Camera1GainTrackBar.Value);
            Camera1GainUpDown.Value = Camera1GainTrackBar.Value;
        }

        private void Settings_frm_Activated(object sender, EventArgs e)
        {
            Camera1EnableCheckBox.Checked = false;
            Camera2EnableCheckBox.Checked = false;
            groupBox14.Enabled = false;
            groupBox13.Enabled = false;
            // Setting for Camera 1
            if (ConnectionData.Camera1 != null)
                {
                    groupBox11.Enabled = true;
                }
                else
                {
                    groupBox11.Enabled = false;
                }

                // Settings for Camera 2

                if (ConnectionData.Camera2 != null)
                {
                    groupBox12.Enabled = true;
                }
                else
                {
                    groupBox12.Enabled = false;
                  }
            }

        private void Camera1GainButtonCont_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Continuous);
                Camera1GainTrackBar.Enabled = false;
                Camera1GainUpDown.Enabled = false;
                Camera1GainButtonCont.Enabled = false;
                Camera1GainButtonOff.Enabled = true;
            }
            catch
            { }
        }

        private void Camera1GainButtonOff_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Off);
                Camera1GainTrackBar.Enabled = true;
                Camera1GainUpDown.Enabled = true;
                Camera1GainButtonCont.Enabled = true;
                Camera1GainButtonOff.Enabled = false;
            }
            catch
            { }
        }

        private void Camera1GainUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.Gain].SetValue(Convert.ToInt16(Camera1GainUpDown.Value));
                Camera1GainTrackBar.Value = Convert.ToInt16(Camera1GainUpDown.Value);
            }
            catch
            { }
        }

        private void Settings_frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            groupBox13.Enabled = false;
            groupBox14.Enabled = false;
            Camera1EnableCheckBox.Checked = false;
            Camera2EnableCheckBox.Checked = false;
            try
            {
                DestroyCamera(ConnectionData.Camera1);
                DestroyCamera(ConnectionData.Camera2);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        // Отключение от камер
        public static void DestroyCamera(Camera cam)
        {
            // Destroy the camera object.
            try
            {
                if (cam != null)
                {
                    cam.Close();
                    cam.Dispose();
                    cam = null;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void Camera1BlckLvlUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.Gain].SetValue(Convert.ToInt16(Camera1BlckLvlUpDown.Value));
                Camera1BlckLvlTrackBar.Value = Convert.ToInt16(Camera1BlckLvlUpDown.Value);
            }
            catch
            { }
        }

        private void Camera1BlckLvlTrackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.Gain].SetValue(Camera1BlckLvlTrackBar.Value);
                Camera1BlckLvlUpDown.Value = Camera1BlckLvlTrackBar.Value;
            }
            catch
            { }
        }

        private void Camera1GammaUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.Gain].SetValue(Convert.ToInt16(Camera1GammaUpDown.Value));
                Camera1GammaTrackBar.Value = Convert.ToInt16(Camera1GammaUpDown.Value);
            }
            catch
            { }
        }

        private void Camera1GammaTrackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.Gamma].SetValue(Camera1GammaTrackBar.Value);
                Camera1GammaUpDown.Value = Camera1GammaTrackBar.Value;
            }
            catch
            { }
        }

        private void Camera1ExposureUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].SetValue(Convert.ToInt16(Camera1ExposureUpDown.Value));
                Camera1ExposureTrackBar.Value = Convert.ToInt16(Camera1ExposureUpDown.Value);
            }
            catch
            { }
        }

        private void Camera1ExposureTrackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].SetValue(Camera1ExposureTrackBar.Value);
                Camera1ExposureUpDown.Value = Camera1ExposureTrackBar.Value;
            }
            catch
            { }
        }

        private void Camera1ExposureButtonCont_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Continuous);
                Camera1ExposureTrackBar.Enabled = false;
                Camera1ExposureUpDown.Enabled = false;
                Camera1ExposureButtonCont.Enabled = false;
                Camera1ExposureButtonOff.Enabled = true;
            }
            catch
            { }
        }

        private void Camera1ExposureButtonOff_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Off);
                Camera1ExposureTrackBar.Enabled = true;
                Camera1ExposureUpDown.Enabled = true;
                Camera1ExposureButtonCont.Enabled = true;
                Camera1ExposureButtonOff.Enabled = false;
            }
            catch
            { }
        }

        private void FrameRateTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void FrameRateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    ConnectionData.Camera1.Parameters[PLCamera.AcquisitionFrameRate].SetValue(Convert.ToInt16(FrameRateTextBox.Text));
                }
                catch
                { }               
            }
        }

        private void Camera1EnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Camera1EnableCheckBox.Checked == true) 
            {
                groupBox14.Enabled = true;
                try
                {
                    if (ConnectionData.Camera1.Parameters[PLCamera.GainAuto].GetValue() == PLCamera.GainAuto.Continuous)
                    {
                        Camera1GainButtonCont.Enabled   = false;
                        Camera1GainButtonOff.Enabled    = true;
                        Camera1GainTrackBar.Enabled     = false;
                        Camera1GainUpDown.Enabled       = false;
                    }
                    else
                    {
                        Camera1GainButtonCont.Enabled   = true;
                        Camera1GainButtonOff.Enabled    = false;
                        Camera1GainTrackBar.Enabled     = true;
                        Camera1GainUpDown.Enabled       = true;
                    }

                    if (ConnectionData.Camera1.Parameters[PLCamera.ExposureAuto].GetValue() == PLCamera.ExposureAuto.Continuous)
                    {
                        Camera1ExposureButtonCont.Enabled   = false;
                        Camera1ExposureButtonOff.Enabled    = true;
                        Camera1ExposureTrackBar.Enabled     = false;
                        Camera1ExposureUpDown.Enabled       = false;
                    }
                    else
                    {
                        Camera1ExposureButtonCont.Enabled   = true;
                        Camera1ExposureButtonOff.Enabled    = false;
                        Camera1ExposureTrackBar.Enabled     = true;
                        Camera1ExposureUpDown.Enabled       = true;
                    }

                    // Gain
                    Camera1GainTrackBar.Minimum     = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Gain].GetMinimum());
                    Camera1GainTrackBar.Maximum     = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Gain].GetMaximum());
                    Camera1GainUpDown.Minimum       = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Gain].GetMinimum());
                    Camera1GainUpDown.Maximum       = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Gain].GetMaximum());
                    Camera1GainMin.Text             = string.Format("{0:F2}", ConnectionData.Camera1.Parameters[PLCamera.Gain].GetMinimum());
                    Camera1GainMax.Text             = string.Format("{0:F2}", ConnectionData.Camera1.Parameters[PLCamera.Gain].GetMaximum());
                    Camera1GainUpDown.Value         = Convert.ToDecimal(ConnectionData.Camera1.Parameters[PLCamera.Gain].GetValue());
                    Camera1GainTrackBar.Value       = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Gain].GetValue());

                    // Black Level
                    Camera1BlckLvlMin.Text          = string.Format("{0:F2}", ConnectionData.Camera1.Parameters[PLCamera.BlackLevel].GetMinimum());
                    Camera1BlckLvlMax.Text          = string.Format("{0:F2}", ConnectionData.Camera1.Parameters[PLCamera.BlackLevel].GetMaximum());
                    Camera1BlckLvlUpDown.Minimum    = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.BlackLevel].GetMinimum());
                    Camera1BlckLvlUpDown.Maximum    = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.BlackLevel].GetMaximum());
                    Camera1BlckLvlTrackBar.Minimum  = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.BlackLevel].GetMinimum());
                    Camera1BlckLvlTrackBar.Maximum  = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.BlackLevel].GetMaximum());
                    Camera1BlckLvlUpDown.Value      = Convert.ToDecimal(ConnectionData.Camera1.Parameters[PLCamera.BlackLevel].GetValue());
                    Camera1BlckLvlTrackBar.Value    = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.BlackLevel].GetValue());

                    // Gamma
                    Camera1GammaMin.Text            = string.Format("{0:F2}", ConnectionData.Camera1.Parameters[PLCamera.Gamma].GetMinimum());
                    Camera1GammaMax.Text            = string.Format("{0:F2}", ConnectionData.Camera1.Parameters[PLCamera.Gamma].GetMaximum());
                    Camera1GammaUpDown.Minimum      = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Gamma].GetMinimum());
                    Camera1GammaUpDown.Maximum      = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Gamma].GetMaximum());
                    Camera1GammaTrackBar.Minimum    = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Gamma].GetMinimum());
                    Camera1GammaTrackBar.Maximum    = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Gamma].GetMaximum());
                    Camera1GammaUpDown.Value        = Convert.ToDecimal(ConnectionData.Camera1.Parameters[PLCamera.Gamma].GetValue());
                    Camera1GammaTrackBar.Value      = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Gamma].GetValue());

                    // Exposure
                    Camera1ExposureMin.Text         = string.Format("{0:F2}", ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].GetMinimum());
                    Camera1ExposureMax.Text         = string.Format("{0:F2}", ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].GetMaximum());
                    Camera1ExposureUpDown.Minimum   = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].GetMinimum());
                    Camera1ExposureUpDown.Maximum   = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].GetMaximum());
                    Camera1ExposureTrackBar.Minimum = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].GetMinimum());
                    Camera1ExposureTrackBar.Maximum = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].GetMaximum());
                    Camera1ExposureUpDown.Value     = Convert.ToDecimal(ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].GetValue());
                    Camera1ExposureTrackBar.Value   = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].GetValue());

                    // Acquisition Frame Rate
                    FrameRateTextBox.Text           = string.Format("{0:F2}", ConnectionData.Camera1.Parameters[PLCamera.AcquisitionFrameRate].GetValue());

                    // Contrast
                    //Camera1ContrastMin.Text = string.Format("{0:F2}", ConnectionData.Camera1.Parameters[PLCamera.ContrastEnhancement].GetMinimum());
                    //Camera1ContrastMax.Text = string.Format("{0:F2}", ConnectionData.Camera1.Parameters[PLCamera.ContrastEnhancement].GetMaximum());
                    //Camera1ContrastUpDown.Minimum = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.ContrastEnhancement].GetMinimum());
                    //Camera1ContrastUpDown.Maximum = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.ContrastEnhancement].GetMaximum());
                    //Camera1ContrastUpDown.Value = Convert.ToDecimal(ConnectionData.Camera1.Parameters[PLCamera.ContrastEnhancement].GetValue());
                    //Camera1ContrastTrackBar.Minimum = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.ContrastEnhancement].GetMinimum());
                    //Camera1ContrastTrackBar.Maximum = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.ContrastEnhancement].GetMaximum());
                    //Camera1ContrastTrackBar.Value = Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.ContrastEnhancement].GetValue());
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            else
            {
                groupBox14.Enabled = false;
            }
        }

        private void Camera1ContrastUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].SetValue(Convert.ToInt16(Camera1ContrastUpDown.Value));
                Camera1ContrastTrackBar.Value = Convert.ToInt16(Camera1ContrastUpDown.Value);
            }
            catch
            { }
        }

        private void Camera1ContrastTrackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera1.Parameters[PLCamera.ExposureTime].SetValue(Camera1ContrastTrackBar.Value);
                Camera1ExposureUpDown.Value = Camera1ContrastTrackBar.Value;
            }
            catch
            { }
        }

        private void Camera2EnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Camera2EnableCheckBox.Checked == true)
            {
                groupBox13.Enabled = true;
                try
                {
                    // Gain
                    Camera2GainTrackBar.Minimum     = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Gain].GetMinimum());
                    Camera2GainTrackBar.Maximum     = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Gain].GetMaximum());
                    Camera2GainUpDown.Minimum       = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Gain].GetMinimum());
                    Camera2GainUpDown.Maximum       = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Gain].GetMaximum());
                    Camera2GainMin.Text             = string.Format("{0:F2}", ConnectionData.Camera2.Parameters[PLCamera.Gain].GetMinimum());
                    Camera2GainMax.Text             = string.Format("{0:F2}", ConnectionData.Camera2.Parameters[PLCamera.Gain].GetMaximum());
                    Camera2GainUpDown.Value         = Convert.ToDecimal(ConnectionData.Camera2.Parameters[PLCamera.Gain].GetValue());
                    Camera2GainTrackBar.Value       = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Gain].GetValue());

                    // Black Level
                    Camera2BlckLvlMin.Text          = string.Format("{0:F2}", ConnectionData.Camera2.Parameters[PLCamera.BlackLevel].GetMinimum());
                    Camera2BlckLvlMax.Text          = string.Format("{0:F2}", ConnectionData.Camera2.Parameters[PLCamera.BlackLevel].GetMaximum());
                    Camera2BlckLvlUpDown.Minimum    = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.BlackLevel].GetMinimum());
                    Camera2BlckLvlUpDown.Maximum    = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.BlackLevel].GetMaximum());
                    Camera2BlckLvlTrackBar.Minimum  = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.BlackLevel].GetMinimum());
                    Camera2BlckLvlTrackBar.Maximum  = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.BlackLevel].GetMaximum());
                    Camera2BlckLvlTrackBar.Value    = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.BlackLevel].GetValue());
                    Camera2BlckLvlUpDown.Value = Convert.ToDecimal(ConnectionData.Camera2.Parameters[PLCamera.BlackLevel].GetValue());

                    // Gamma
                    Camera2GammaMin.Text            = string.Format("{0:F2}", ConnectionData.Camera2.Parameters[PLCamera.Gamma].GetMinimum());
                    Camera2GammaMax.Text            = string.Format("{0:F2}", ConnectionData.Camera2.Parameters[PLCamera.Gamma].GetMaximum());
                    Camera2GammaUpDown.Minimum      = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Gamma].GetMinimum());
                    Camera2GammaUpDown.Maximum      = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Gamma].GetMaximum());
                    Camera2GammaTrackBar.Minimum    = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Gamma].GetMinimum());
                    Camera2GammaTrackBar.Maximum    = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Gamma].GetMaximum());
                    Camera2GammaTrackBar.Value      = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Gamma].GetValue());
                    Camera2GammaUpDown.Value = Convert.ToDecimal(ConnectionData.Camera2.Parameters[PLCamera.Gamma].GetValue());

                    // Exposure
                    Camera2ExposureMin.Text         = string.Format("{0:F2}", ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].GetMinimum());
                    Camera2ExposureMax.Text         = string.Format("{0:F2}", ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].GetMaximum());
                    Camera2ExposureUpDown.Minimum   = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].GetMinimum());
                    Camera2ExposureUpDown.Maximum   = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].GetMaximum());

                    Camera2ExposureTrackBar.Minimum = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].GetMinimum());
                    Camera2ExposureTrackBar.Maximum = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].GetMaximum());
                    Camera2ExposureTrackBar.Value   = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].GetValue());
                    Camera2ExposureUpDown.Value = Convert.ToDecimal(ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].GetValue());

                    // Acquisition Frame Rate
                    Camera2FrameRateTextBox.Text    = string.Format("{0:F2}", ConnectionData.Camera2.Parameters[PLCamera.AcquisitionFrameRate].GetValue());

                    // Contrast
                    Camera2ContrastMin.Text         = string.Format("{0:F2}", ConnectionData.Camera2.Parameters[PLCamera.ContrastEnhancement].GetMinimum());
                    Camera2ContrastMax.Text         = string.Format("{0:F2}", ConnectionData.Camera2.Parameters[PLCamera.ContrastEnhancement].GetMaximum());
                    Camera2ContrastUpDown.Minimum   = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.ContrastEnhancement].GetMinimum());
                    Camera2ContrastUpDown.Maximum   = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.ContrastEnhancement].GetMaximum());
                    Camera2ContrastUpDown.Value     = Convert.ToDecimal(ConnectionData.Camera2.Parameters[PLCamera.ContrastEnhancement].GetValue());
                    Camera2ContrastTrackBar.Minimum = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.ContrastEnhancement].GetMinimum());
                    Camera2ContrastTrackBar.Maximum = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.ContrastEnhancement].GetMaximum());
                    Camera2ContrastTrackBar.Value   = Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.ContrastEnhancement].GetValue());

                    if (ConnectionData.Camera2.Parameters[PLCamera.GainAuto].GetValue() == PLCamera.GainAuto.Continuous)
                    {
                        Camera2GainButtonCont.Enabled = false;
                        Camera2GainButtonOff.Enabled = true;
                        Camera2GainTrackBar.Enabled = false;
                        Camera2GainUpDown.Enabled = false;
                    }
                    else
                    {
                        Camera2GainButtonCont.Enabled = true;
                        Camera2GainButtonOff.Enabled = false;
                        Camera2GainTrackBar.Enabled = true;
                        Camera2GainUpDown.Enabled = true;
                    }

                    if (ConnectionData.Camera2.Parameters[PLCamera.ExposureAuto].GetValue() == PLCamera.ExposureAuto.Continuous)
                    {
                        Camera2ExposureButtonCont.Enabled = false;
                        Camera2ExposureButtonOff.Enabled = true;
                        Camera2ExposureTrackBar.Enabled = false;
                        Camera2ExposureUpDown.Enabled = false;
                    }
                    else
                    {
                        Camera2ExposureButtonCont.Enabled = true;
                        Camera2ExposureButtonOff.Enabled = false;
                        Camera2ExposureTrackBar.Enabled = true;
                        Camera2ExposureUpDown.Enabled = true;
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            else
            {
                groupBox13.Enabled = false;
            }
        }

        private void Camera2GainButtonCont_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Continuous);
                Camera2GainTrackBar.Enabled = false;
                Camera2GainUpDown.Enabled = false;
                Camera2GainButtonCont.Enabled = false;
                Camera2GainButtonOff.Enabled = true;
            }
            catch
            { }
        }

        private void Camera2GainButtonOff_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Off);
                Camera2GainTrackBar.Enabled = true;
                Camera2GainUpDown.Enabled = true;
                Camera2GainButtonCont.Enabled = true;
                Camera2GainButtonOff.Enabled = false;
            }
            catch
            { }
        }

        private void Camera2GainUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.Gain].SetValue(Convert.ToInt16(Camera2GainUpDown.Value));
                Camera2GainTrackBar.Value = Convert.ToInt16(Camera2GainUpDown.Value);
            }
            catch
            { }
        }

        private void Camera2GainTrackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.Gain].SetValue(Convert.ToInt16(Camera2GainUpDown.Value));
                Camera2GainTrackBar.Value = Convert.ToInt16(Camera2GainUpDown.Value);
            }
            catch
            { }
        }

        private void Camera2BlckLvlUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.Gain].SetValue(Convert.ToInt16(Camera2BlckLvlUpDown.Value));
                Camera2BlckLvlTrackBar.Value = Convert.ToInt16(Camera2BlckLvlUpDown.Value);
            }
            catch
            { }
        }

        private void Camera2BlckLvlTrackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.Gain].SetValue(Camera2BlckLvlTrackBar.Value);
                Camera2BlckLvlUpDown.Value = Camera2BlckLvlTrackBar.Value;
            }
            catch
            { }
        }

        private void Camera2GammaUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.Gain].SetValue(Convert.ToInt16(Camera2GammaUpDown.Value));
                Camera2GammaTrackBar.Value = Convert.ToInt16(Camera2GammaUpDown.Value);
            }
            catch
            { }
        }

        private void Camera2GammaTrackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.Gamma].SetValue(Camera2GammaTrackBar.Value);
                Camera2GammaUpDown.Value = Camera2GammaTrackBar.Value;
            }
            catch
            { }
        }

        private void Camera2ExposureButtonCont_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Continuous);
                Camera2ExposureTrackBar.Enabled = false;
                Camera2ExposureUpDown.Enabled = false;
                Camera2ExposureButtonCont.Enabled = false;
                Camera2ExposureButtonOff.Enabled = true;
            }
            catch
            { }
        }

        private void Camera2ExposureButtonOff_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Off);
                Camera2ExposureTrackBar.Enabled = true;
                Camera2ExposureUpDown.Enabled = true;
                Camera2ExposureButtonCont.Enabled = true;
                Camera2ExposureButtonOff.Enabled = false;
            }
            catch
            { }
        }

        private void Camera2ExposureUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].SetValue(Convert.ToInt16(Camera2ExposureUpDown.Value));
                Camera2ExposureTrackBar.Value = Convert.ToInt16(Camera2ExposureUpDown.Value);
            }
            catch
            { }
        }

        private void Camera2ExposureTrackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].SetValue(Camera2ExposureTrackBar.Value);
                Camera2ExposureUpDown.Value = Camera2ExposureTrackBar.Value;
            }
            catch
            { }
        }

        private void Camera2ContrastTrackBar_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].SetValue(Camera2ContrastTrackBar.Value);
                Camera2ExposureUpDown.Value = Camera2ContrastTrackBar.Value;
            }
            catch
            { }
        }

        private void Camera2ContrastUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ConnectionData.Camera2.Parameters[PLCamera.ExposureTime].SetValue(Convert.ToInt16(Camera2ContrastUpDown.Value));
                Camera2ContrastTrackBar.Value = Convert.ToInt16(Camera2ContrastUpDown.Value);
            }
            catch
            { }
        }

        private void Camera2FrameRateTextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Camera2FrameRateTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    ConnectionData.Camera2.Parameters[PLCamera.AcquisitionFrameRate].SetValue(Convert.ToInt16(Camera2FrameRateTextBox.Text));
                }
                catch
                { }
            }
        }

        private void Settings_frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
             //   DestroyCamera(ConnectionData.Camera1);
             //   DestroyCamera(ConnectionData.Camera2);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void WXAxisSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.WXaxis = Convert.ToDouble(WXAxisSP.Text);
            }
        }

        private void WYAxisSP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.WYaxis = Convert.ToDouble(WYAxisSP.Text);
            }
        }

        private void WXAxisSP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void WYAxisSP_KeyPress(object sender, KeyPressEventArgs e)
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

        private void PreeflowDiam_TB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void PreeflowDiam_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //ConnectionData.PreeflowDiam = Convert.ToDouble(PreeflowDiam_TB.Text);
            }
        }

        private void PF1Diameter_TB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void PF2Diameter_TB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void PF1Diameter_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.PF1Diameter = Convert.ToDouble(PF1Diameter_TB.Text);
            }
        }

        private void PF2Diameter_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.PF2Diameter = Convert.ToDouble(PF2Diameter_TB.Text);
            }
        }

        private void PetiXDistance_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == ',')
                e.KeyChar = '.';
            if (e.KeyChar != 22)
                e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar != '-') && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
            else
            {
                double d;
                e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                MessageBox.Show(Dict.LangStrings.InsertBuffer);
            }
        }

        private void PetiYDistance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ',')
                e.KeyChar = '.';
            if (e.KeyChar != 22)
                e.Handled = !Char.IsDigit(e.KeyChar) && (e.KeyChar !='-') && (e.KeyChar != '.' || (((TextBox)sender).Text.Contains(".") && !((TextBox)sender).SelectedText.Contains("."))) && e.KeyChar != (char)Keys.Back;
            else
            {
                double d;
                e.Handled = !double.TryParse(Clipboard.GetText(), out d) || (d < 0 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.Contains("-") && !((TextBox)sender).SelectedText.Contains("-"))) || ((d - (int)d) != 0 && ((TextBox)sender).Text.Contains(",") && !((TextBox)sender).SelectedText.Contains(","));
                MessageBox.Show(Dict.LangStrings.InsertBuffer);
            }
            
        }

        private void PetiXDistance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.PetriX = Convert.ToDouble(PetiXDistance.Text);
            }
        }

        private void PetiYDistance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.PetriY = Convert.ToDouble (PetiYDistance.Text);
            }
        }

        private void Accuracy50Select_CB_CheckedChanged(object sender, EventArgs e)
        {
            if (Accuracy50Select_CB.Checked == true)
            {
                Accuracy100Select_CB.Checked = false;
                ConnectionData.AccuracySelect = 1;
            }
        }

        private void Accuracy100Select_CB_CheckedChanged(object sender, EventArgs e)
        {
            if (Accuracy100Select_CB.Checked == true)
            {
                Accuracy50Select_CB.Checked = false;
                ConnectionData.AccuracySelect = 2;
            }
        }

        private void Accuracy50_TB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Accuracy100_TB_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Accuracy50_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.Accuracy1 = Convert.ToDouble(Accuracy50_TB.Text);
            }
        }

        private void Accuracy100_TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConnectionData.Accuracy2 = Convert.ToDouble(Accuracy100_TB.Text);
            }
        }
    }
}
