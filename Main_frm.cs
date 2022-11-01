using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Globalization;
using Active_Directory_Worker.Interfaces;
using System.IO;

namespace BTP
{
    public partial class Main_frm : Form, ILanguageChangable
    {
        private Thread MotorStateThr;
        int[] axes;
        int programm_buffer;
        public Main_frm()
        {
            InitializeComponent();

            // Инициализация форм
            Manual = new Manual_frm();
            Program = new Program_frm();
            Auto = new Auto_frm();
            Diagnostics = new Diagnostics_frm();
            Settings = new Settings_frm();
            SelectDrv = new SelectDriver_frm();
            //Camera1frm = new Camera1_frm();
            Printhead1 = new Printhead_frm();
            
            //Camera2frm = new Camera2_frm();
        }
 
        //Camera1_frm Camera1frm;
        //Camera2_frm Camera2frm;

        // Подключение библиотек ACS Motion Control
        //public SPIIPLUSCOM660Lib.Channel ConnectionData.Value;      

        // Обработка кнопок управления
        // Кнопка завершения работы
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            MotorStateThr.Abort();
            ConnectionData.Value.device.connectStop();
            
            Close();
        }

        // Кнопка ручного режима работы
        private void ManualBtn_Click(object sender, EventArgs e)
        {
            //Camera1frm.Hide();
            Program.Hide();
            Auto.Hide();
            Settings.Hide();
            Diagnostics.Hide();
            Printhead1.Hide();
            Manual.Show(this);
            //Camera1frm.Show(Manual);
            // Camera2frm.Show(Manual);
            ManualBtn.Enabled = false;
            ProgramBtn.Enabled = true;
            AutoBtn.Enabled = true;
            SettingsBtn.Enabled = true;
            DiagnosticsBtn.Enabled = true;
            StartBtn.Enabled = true;
            StopBtn.Enabled = true;
            PauseBtn.Enabled = true;
            ResetBtn.Enabled = true;
            PrintheadBtn.Enabled = true;
            PrintheadBtn.Enabled = false;

        }
        // Кнопка работы по программе
        private void ProgramBtn_Click(object sender, EventArgs e)
        {
            Auto.Hide();
            Settings.Hide();
            Diagnostics.Hide();
            Manual.Hide();
            //Camera1frm.Hide();
            //Camera2frm.Hide();
            Program.Show(this);
            Printhead1.Hide();
            ManualBtn.Enabled = true;
            ProgramBtn.Enabled = false;
            AutoBtn.Enabled = true;
            SettingsBtn.Enabled = true;
            DiagnosticsBtn.Enabled = true;
            StartBtn.Enabled = true;
            StopBtn.Enabled = true;
            PauseBtn.Enabled = true;
            ResetBtn.Enabled = true;
            PrintheadBtn.Enabled = true;
            PrintheadBtn.Enabled = false;
        }
        // Кнопка автоматического режима 
        private void AutoBtn_Click(object sender, EventArgs e)
        {
            //Camera1frm.Hide();
            Settings.Hide();
            Diagnostics.Hide();
            Manual.Hide();
            Program.Hide();
            Auto.Show(this);
            //Camera1frm.Show(Auto);
            Printhead1.Hide();
            // Camera2frm.Show(Auto);
            ManualBtn.Enabled = true;
            ProgramBtn.Enabled = true;
            AutoBtn.Enabled = false;
            SettingsBtn.Enabled = true;
            DiagnosticsBtn.Enabled = true;
            StartBtn.Enabled = true;
            StopBtn.Enabled = true;
            PauseBtn.Enabled = true;
            ResetBtn.Enabled = true;
            PrintheadBtn.Enabled = true;
            PrintheadBtn.Enabled = false;
        }
        // Кнопка настроек
        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            Diagnostics.Hide();
            Manual.Hide();
            Program.Hide();
            Auto.Hide();
            //Camera1frm.Hide();
            Printhead1.Hide();
            //Camera2frm.Hide();
            Settings.Show(this);
            ManualBtn.Enabled = true;
            ProgramBtn.Enabled = true;
            AutoBtn.Enabled = true;
            SettingsBtn.Enabled = false;
            DiagnosticsBtn.Enabled = true;
            StartBtn.Enabled = false;
            StopBtn.Enabled = false;
            PauseBtn.Enabled = false;
            ResetBtn.Enabled = false;
            PrintheadBtn.Enabled = true;
            PrintheadBtn.Enabled = false;
        }
        // Кнопка диагностики
        private void DiagnosticsBtn_Click(object sender, EventArgs e)
        {
            Manual.Hide();
            Program.Hide();
            Auto.Hide();
            Settings.Hide();
            //Camera1frm.Hide();
            Printhead1.Hide();
            //Camera2frm.Hide();
            Diagnostics.Show(this);
            ManualBtn.Enabled = true;
            ProgramBtn.Enabled = true;
            AutoBtn.Enabled = true;
            SettingsBtn.Enabled = true;
            DiagnosticsBtn.Enabled = false;
            StartBtn.Enabled = true;
            StopBtn.Enabled = true;
            PauseBtn.Enabled = true;
            ResetBtn.Enabled = true;
            PrintheadBtn.Enabled = true;
            PrintheadBtn.Enabled = false;
        }
        private void PrintheadBtn_Click(object sender, EventArgs e)
        {
            //Camera1frm.Hide();
            Program.Hide();
            Auto.Hide();
            Settings.Hide();
            Diagnostics.Hide();
            Printhead1.Show(this);
            Manual.Hide();
            //Camera1frm.Show(Manual);
            // Camera2frm.Show(Manual);
            ManualBtn.Enabled = true;
            ProgramBtn.Enabled = true;
            AutoBtn.Enabled = true;
            SettingsBtn.Enabled = true;
            DiagnosticsBtn.Enabled = true;
            StartBtn.Enabled = true;
            StopBtn.Enabled = true;
            PauseBtn.Enabled = true;
            ResetBtn.Enabled = true;
            PrintheadBtn.Enabled = false;
        }

        // Обработчик ошибок ACS
        private void ErorMsg(COMException Ex)
        {
            string Str = "Error from " + Ex.Source + "\n\r";
            Str = Str + Ex.Message + "\n\r";
            Str = Str + "HRESULT:" + String.Format("0x{0:X}", (Ex.ErrorCode));
            MessageBox.Show(Str, "EnableEvent");
        }

        // Запуск референцирования по осям X, Y и камерам
        private void FindHome()
        {
            if (ConnectionData.bConnected)
            {              
                ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_0);
                ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_9);
                ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_8);
               // ConnectionData.Value.RunBuffer(ConnectionData.Value.ACSC_BUFFER_4);
            }
        }

        // Обработка диаметров и прочей статической информации 
        private void SendDiam()
        {
            // Задание начальных скоростей
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_0, ConnectionData.SetXYVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_1, ConnectionData.SetXYVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_2, ConnectionData.SetZVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_3, ConnectionData.SetZVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_4, ConnectionData.SetZVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_5, ConnectionData.SetZVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_6, ConnectionData.SetZVel);
            ConnectionData.Value.SetVelocity(ConnectionData.Value.ACSC_AXIS_7, ConnectionData.SetPtVel);

            // Запись диаметров
            ConnectionData.Value.WriteVariable(ConnectionData.S1Diameter, "S1Diameter", ConnectionData.Value.ACSC_NONE);
            ConnectionData.Value.WriteVariable(ConnectionData.S2Diameter, "S2Diameter", ConnectionData.Value.ACSC_NONE);
            ConnectionData.Value.WriteVariable(ConnectionData.S3Diameter, "S3Diameter", ConnectionData.Value.ACSC_NONE);

            ConnectionData.Value.WriteVariable(ConnectionData.X_Laser_offset, "X_Laser_offset", ConnectionData.Value.ACSC_NONE);
            ConnectionData.Value.WriteVariable(ConnectionData.Y_Laser_offset, "Y_Laser_offset", ConnectionData.Value.ACSC_NONE);
            ConnectionData.Value.WriteVariable(ConnectionData.Z_Laser_offset, "Z_Laser_offset", ConnectionData.Value.ACSC_NONE);

            ConnectionData.Value.WriteVariable(ConnectionData.HomeVelocityX,    "VelX", ConnectionData.Value.ACSC_BUFFER_4);
            ConnectionData.Value.WriteVariable(ConnectionData.HomeVelocityY,    "VelY", ConnectionData.Value.ACSC_BUFFER_4);
            ConnectionData.Value.WriteVariable(ConnectionData.HomeVelocityCam,  "VelCam", ConnectionData.Value.ACSC_BUFFER_4);
            ConnectionData.Value.WriteVariable(ConnectionData.HomeVelocityZ,    "Veloc", ConnectionData.Value.ACSC_BUFFER_4);

            Console.WriteLine("after send_vel");
            // Системые переменные
            ConnectionData.Value.WriteVariable(0.01, "XSEGRMIN", ConnectionData.Value.ACSC_NONE);
        }

        // Загрузка данных при старте из файла+-
        private void LoadData()
        {
            string Path = Application.StartupPath + "\\Data\\Settings.dll";
            INIManager manager = new INIManager(Path);

            // IP адрес контроллера
            ConnectionData.ControllerIP = manager.GetPrivateString("System", "IPAddress");
            // Чтение максимальных скоростей
            ConnectionData.MaxXYVel = Convert.ToDouble(manager.GetPrivateString("Velocity", "XYVelocity"));
            ConnectionData.MaxZVel =  Convert.ToDouble(manager.GetPrivateString("Velocity", "ZVelocity"));
            ConnectionData.MaxPtVel = Convert.ToDouble(manager.GetPrivateString("Velocity", "PtVelocity"));
            ConnectionData.MaxPFVel = Convert.ToDouble(manager.GetPrivateString("Velocity", "PFVelocity"));
            ConnectionData.MaxCTVel = Convert.ToDouble(manager.GetPrivateString("Velocity", "CTVelocity"));
            ConnectionData.MaxCBVel = Convert.ToDouble(manager.GetPrivateString("Velocity", "CBVelocity"));

            // Скорости поиска нулевой точки
            ConnectionData.HomeVelocityX = Convert.ToDouble(manager.GetPrivateString("Homing", "HomeVelocityX"));
            ConnectionData.HomeVelocityY = Convert.ToDouble(manager.GetPrivateString("Homing", "HomeVelocityY"));
            ConnectionData.HomeVelocityZ = Convert.ToDouble(manager.GetPrivateString("Homing", "HomeVelocityZ"));
            ConnectionData.HomeVelocityCam = Convert.ToDouble(manager.GetPrivateString("Homing", "HomeVelocityCam"));

            // Чтение установленных при последнем запуске программы скоростей
            ConnectionData.SetXYVel = Convert.ToDouble(manager.GetPrivateString("Dynamic", "XYVeloc"));
            ConnectionData.SetZVel =  Convert.ToDouble(manager.GetPrivateString("Dynamic", "ZVeloc"));
            ConnectionData.SetPtVel = Convert.ToDouble(manager.GetPrivateString("Dynamic", "PtVeloc"));
            ConnectionData.SetPFVel = Convert.ToDouble(manager.GetPrivateString("Dynamic", "PFVeloc"));
            ConnectionData.SetCTVel = Convert.ToDouble(manager.GetPrivateString("Dynamic", "CTVeloc"));
            ConnectionData.SetCBVel = Convert.ToDouble(manager.GetPrivateString("Dynamic", "CBVeloc"));

            // Чтение диаметров и других данных
            ConnectionData.S1Diameter = Convert.ToDouble(manager.GetPrivateString("Data", "S1Diameter"));
            ConnectionData.S2Diameter = Convert.ToDouble(manager.GetPrivateString("Data", "S2Diameter"));
            ConnectionData.S3Diameter = Convert.ToDouble(manager.GetPrivateString("Data", "S3Diameter"));
            ConnectionData.S1ProcDiameter = Convert.ToDouble(manager.GetPrivateString("Data", "S1ProcDiameter"));
            ConnectionData.S2ProcDiameter = Convert.ToDouble(manager.GetPrivateString("Data", "S2ProcDiameter"));
            ConnectionData.S3ProcDiameter = Convert.ToDouble(manager.GetPrivateString("Data", "S3ProcDiameter"));
            ConnectionData.PF1Diameter = Convert.ToDouble(manager.GetPrivateString("Data", "PF1Diameter"));
            ConnectionData.PF2Diameter = Convert.ToDouble(manager.GetPrivateString("Data", "PF2Diameter"));
            ConnectionData.PfMix = Convert.ToDouble(manager.GetPrivateString("Data", "PFValue"));
            ConnectionData.PetriDishDiam = Convert.ToDouble(manager.GetPrivateString("Data", "PetriDishDiam"));
            ConnectionData.WellNum = Convert.ToDouble(manager.GetPrivateString("Data", "WellCount"));

            //ConnectionData.X_Laser_offset = Convert.ToDouble(manager.GetPrivateString("Data", "X_Laser_offset"));
            //ConnectionData.Y_Laser_offset = Convert.ToDouble(manager.GetPrivateString("Data", "Y_Laser_offset"));
            //ConnectionData.Z_Laser_offset = Convert.ToDouble(manager.GetPrivateString("Data", "Z_Laser_offset"));
            ConnectionData.X_Laser_offset = 67.76;
            ConnectionData.Y_Laser_offset = 73.24;
            ConnectionData.Z_Laser_offset = -5.51;
            //Чтение параметров для камеры
            ConnectionData.Camera1SN = manager.GetPrivateString("Cameras", "SerialNumberCam1");
            ConnectionData.Camera2SN = manager.GetPrivateString("Cameras", "SerialNumberCam2");
            ConnectionData.ScaleCam1 = Convert.ToDouble(manager.GetPrivateString("Cameras", "ScaleCam1"));
            ConnectionData.ScaleCam2 = Convert.ToDouble(manager.GetPrivateString("Cameras", "ScaleCam2"));
            ConnectionData.Camera2XStrokeMax = Convert.ToDouble(manager.GetPrivateString("Cameras", "Camera2XStrokeMax"));
            ConnectionData.Camera2YStrokeMax = Convert.ToDouble(manager.GetPrivateString("Cameras", "Camera2YStrokeMax"));
            ConnectionData.Camera1ZoomStrokeMax = Convert.ToDouble(manager.GetPrivateString("Cameras", "Camera1ZoomStroke"));

            //Смещения по осям
            ConnectionData.XOffset = Convert.ToDouble(manager.GetPrivateString("Offsets", "XOffset"));
            ConnectionData.YOffset = Convert.ToDouble(manager.GetPrivateString("Offsets", "YOffset"));
            ConnectionData.ZS1Offset = Convert.ToDouble(manager.GetPrivateString("Offsets", "ZS1Offset"));
            ConnectionData.ZS2Offset = Convert.ToDouble(manager.GetPrivateString("Offsets", "ZS2Offset"));
            ConnectionData.ZS3Offset = Convert.ToDouble(manager.GetPrivateString("Offsets", "ZS3Offset"));
            ConnectionData.ZSpOffset = Convert.ToDouble(manager.GetPrivateString("Offsets", "ZSPOffset"));
            ConnectionData.ZPFOffset = Convert.ToDouble(manager.GetPrivateString("Offsets", "ZPFOffset"));

            // Параметры калибровки
            ConnectionData.CalDistanceX = Convert.ToDouble(manager.GetPrivateString("Calibrate", "DistanceX"));
            ConnectionData.CalDistanceY = Convert.ToDouble(manager.GetPrivateString("Calibrate", "DistanceY"));
            ConnectionData.CalDistanceZ = Convert.ToDouble(manager.GetPrivateString("Calibrate", "DistanceZ"));
            ConnectionData.CalVelocityX = Convert.ToDouble(manager.GetPrivateString("Calibrate", "VelocityX"));
            ConnectionData.CalVelocityY = Convert.ToDouble(manager.GetPrivateString("Calibrate", "VelocityY"));
            ConnectionData.CalVelocityZ = Convert.ToDouble(manager.GetPrivateString("Calibrate", "VelocityZ"));
            ConnectionData.ZXaxis = Convert.ToDouble(manager.GetPrivateString("Calibrate", "ZXaxis"));
            ConnectionData.ZYaxis = Convert.ToDouble(manager.GetPrivateString("Calibrate", "ZYaxis"));
            ConnectionData.UXaxis = Convert.ToDouble(manager.GetPrivateString("Calibrate", "UXaxis"));
            ConnectionData.UYaxis = Convert.ToDouble(manager.GetPrivateString("Calibrate", "UYaxis"));
            ConnectionData.VXaxis = Convert.ToDouble(manager.GetPrivateString("Calibrate", "VXaxis"));
            ConnectionData.VYaxis = Convert.ToDouble(manager.GetPrivateString("Calibrate", "VYaxis"));
            ConnectionData.AXaxis = Convert.ToDouble(manager.GetPrivateString("Calibrate", "AXaxis"));
            ConnectionData.AYaxis = Convert.ToDouble(manager.GetPrivateString("Calibrate", "AYaxis"));
            ConnectionData.WXaxis = Convert.ToDouble(manager.GetPrivateString("Calibrate", "WXaxis"));
            ConnectionData.WYaxis = Convert.ToDouble(manager.GetPrivateString("Calibrate", "WYaxis"));
            ConnectionData.AccuracySelect = Convert.ToInt16(manager.GetPrivateString("Calibrate", "AccuracySel"));
            ConnectionData.Accuracy1 = Convert.ToDouble(manager.GetPrivateString("Calibrate", "Accuracy1"));
            ConnectionData.Accuracy2 = Convert.ToDouble(manager.GetPrivateString("Calibrate", "Accuracy2"));

            // Параметры Preeflow
            ConnectionData.PreeflowDiam = Convert.ToDouble(manager.GetPrivateString("Preeflow", "PFDiameter"));

            // Расстояние до чаши
            ConnectionData.PetriX = Convert.ToDouble(manager.GetPrivateString("PetriDistances", "PetriX"));
            ConnectionData.PetriY = Convert.ToDouble(manager.GetPrivateString("PetriDistances", "PetriY"));

            // Параметры рабочего нуля
            ConnectionData.ZeroHead = Convert.ToInt32(manager.GetPrivateString("ZeroPoints", "ZeroHead"));
            ConnectionData.XZero = Convert.ToDouble(manager.GetPrivateString("ZeroPoints", "XZero"));
            ConnectionData.YZero = Convert.ToDouble(manager.GetPrivateString("ZeroPoints", "YZero"));
            ConnectionData.ZZero = Convert.ToDouble(manager.GetPrivateString("ZeroPoints", "ZZero"));
            ConnectionData.OffsetXZero = Convert.ToDouble(manager.GetPrivateString("ZeroPoints", "OffsetXZero"));
            ConnectionData.OffsetYZero = Convert.ToDouble(manager.GetPrivateString("ZeroPoints", "OffsetYZero"));
            ConnectionData.OffsetZZero = Convert.ToDouble(manager.GetPrivateString("ZeroPoints", "OffsetZZero"));

            //label1.Text = ConnectionData.SetXYVel.ToString();
            
            ConnectionData.zoom = Convert.ToInt32(manager.GetPrivateString("Printhead", "zoom"));
            ConnectionData.focus = Convert.ToInt32(manager.GetPrivateString("Printhead", "focus"));

            ConnectionData.obr1 = manager.GetPrivateString("Printhead", "obr1");
            ConnectionData.obr2 = manager.GetPrivateString("Printhead", "obr2");
            ConnectionData.obr3 = manager.GetPrivateString("Printhead", "obr3");
            ConnectionData.obr4 = manager.GetPrivateString("Printhead", "obr4");

            ConnectionData.Ard1 = manager.GetPrivateString("Printhead", "Ard1");
            ConnectionData.Ard2 = manager.GetPrivateString("Printhead", "Ard2");
            ConnectionData.Ard3 = manager.GetPrivateString("Printhead", "Ard3");
            ConnectionData.Ard4 = manager.GetPrivateString("Printhead", "Ard4");
            ConnectionData.Ard5 = manager.GetPrivateString("Printhead", "Ard5");
            ConnectionData.Ard6 = manager.GetPrivateString("Printhead", "Ard6");
        }
        
        // Сохранение данных при выходе из программы
        private void SaveData()
        {
            string Path = Application.StartupPath + "\\Data\\Settings.dll";
            INIManager manager = new INIManager(Path);
            // IP адрес контроллера
            manager.WritePrivateString("System", "IPAddress", ConnectionData.ControllerIP);

            // Максимальные скорости
            manager.WritePrivateString("Velocity", "XYVelocity", ConnectionData.MaxXYVel.ToString());
            manager.WritePrivateString("Velocity", "ZVelocity", ConnectionData.MaxZVel.ToString());
            manager.WritePrivateString("Velocity", "PtVelocity", ConnectionData.MaxPtVel.ToString());
            manager.WritePrivateString("Velocity", "PFVelocity", ConnectionData.MaxPFVel.ToString());
            manager.WritePrivateString("Velocity", "CTVelocity", ConnectionData.MaxCTVel.ToString());
            manager.WritePrivateString("Velocity", "CBVelocity", ConnectionData.MaxCBVel.ToString());

            // Скорости поиска нулевой точки
            manager.WritePrivateString("Homing", "HomeVelocityX", ConnectionData.HomeVelocityX.ToString());
            manager.WritePrivateString("Homing", "HomeVelocityY", ConnectionData.HomeVelocityY.ToString());
            manager.WritePrivateString("Homing", "HomeVelocityZ", ConnectionData.HomeVelocityZ.ToString());
            manager.WritePrivateString("Homing", "HomeVelocityCam", ConnectionData.HomeVelocityCam.ToString());

            // Установленные скорости.
            manager.WritePrivateString("Dynamic", "XYVeloc", ConnectionData.SetXYVel.ToString());
            manager.WritePrivateString("Dynamic", "ZVeloc", ConnectionData.SetZVel.ToString());
            manager.WritePrivateString("Dynamic", "PtVeloc", ConnectionData.SetPtVel.ToString());
            manager.WritePrivateString("Dynamic", "PFVeloc", ConnectionData.SetPFVel.ToString());
            manager.WritePrivateString("Dynamic", "CTVeloc", ConnectionData.SetCTVel.ToString());
            manager.WritePrivateString("Dynamic", "CBVeloc", ConnectionData.SetCBVel.ToString());

            //Диаметры и прочие данные
            manager.WritePrivateString("Data", "S1Diameter", ConnectionData.S1Diameter.ToString());
            manager.WritePrivateString("Data", "S2Diameter", ConnectionData.S2Diameter.ToString());
            manager.WritePrivateString("Data", "S3Diameter", ConnectionData.S3Diameter.ToString());
            manager.WritePrivateString("Data", "S1ProcDiameter", ConnectionData.S1ProcDiameter.ToString());
            manager.WritePrivateString("Data", "S2ProcDiameter", ConnectionData.S2ProcDiameter.ToString());
            manager.WritePrivateString("Data", "S3ProcDiameter", ConnectionData.S3ProcDiameter.ToString());
            manager.WritePrivateString("Data", "PF1Diameter", ConnectionData.PF1Diameter.ToString());
            manager.WritePrivateString("Data", "PF2Diameter", ConnectionData.PF2Diameter.ToString());
            manager.WritePrivateString("Data", "PFValue", ConnectionData.PfMix.ToString());
            manager.WritePrivateString("Data", "PetriDishDiam", ConnectionData.PetriDishDiam.ToString());
            manager.WritePrivateString("Data", "WellCount", ConnectionData.WellNum.ToString());

            manager.WritePrivateString("Data", "X_Laser_offset", ConnectionData.X_Laser_offset.ToString());
            manager.WritePrivateString("Data", "Y_Laser_offset", ConnectionData.Y_Laser_offset.ToString());
            manager.WritePrivateString("Data", "Z_Laser_offset", ConnectionData.Z_Laser_offset.ToString());
            // Параметры для камер
            manager.WritePrivateString("Cameras", "SerialNumberCam1", ConnectionData.Camera1SN);
            manager.WritePrivateString("Cameras", "SerialNumberCam2", ConnectionData.Camera2SN);
            manager.WritePrivateString("Cameras", "ScaleCam1", ConnectionData.ScaleCam1.ToString());
            manager.WritePrivateString("Cameras", "ScaleCam2", ConnectionData.ScaleCam2.ToString());
            manager.WritePrivateString("Cameras", "Camera2XStrokeMax", ConnectionData.Camera2XStrokeMax.ToString());
            manager.WritePrivateString("Cameras", "Camera2YStrokeMax", ConnectionData.Camera2YStrokeMax.ToString());
            manager.WritePrivateString("Cameras", "Camera1ZoomStroke", ConnectionData.Camera1ZoomStrokeMax.ToString());

            // Смещения по осям
            manager.WritePrivateString("Offsets", "XOffset", ConnectionData.XOffset.ToString());
            manager.WritePrivateString("Offsets", "YOffset", ConnectionData.YOffset.ToString());
            manager.WritePrivateString("Offsets", "ZS1Offset", ConnectionData.ZS1Offset.ToString());
            manager.WritePrivateString("Offsets", "ZS2Offset", ConnectionData.ZS2Offset.ToString());
            manager.WritePrivateString("Offsets", "ZS3Offset", ConnectionData.ZS3Offset.ToString());
            manager.WritePrivateString("Offsets", "ZSPOffset", ConnectionData.ZSpOffset.ToString());
            manager.WritePrivateString("Offsets", "ZPFOffset", ConnectionData.ZPFOffset.ToString());

            // Параметры калибровки
            manager.WritePrivateString("Calibrate", "DistanceX", ConnectionData.CalDistanceX.ToString());
            manager.WritePrivateString("Calibrate", "DistanceY", ConnectionData.CalDistanceY.ToString());
            manager.WritePrivateString("Calibrate", "DistanceZ", ConnectionData.CalDistanceZ.ToString());
            manager.WritePrivateString("Calibrate", "VelocityX", ConnectionData.CalVelocityX.ToString());
            manager.WritePrivateString("Calibrate", "VelocityY", ConnectionData.CalVelocityY.ToString());
            manager.WritePrivateString("Calibrate", "VelocityZ", ConnectionData.CalVelocityZ.ToString());
            manager.WritePrivateString("Calibrate", "ZXaxis", ConnectionData.ZXaxis.ToString());
            manager.WritePrivateString("Calibrate", "ZYaxis", ConnectionData.ZYaxis.ToString());
            manager.WritePrivateString("Calibrate", "UXaxis", ConnectionData.UXaxis.ToString());
            manager.WritePrivateString("Calibrate", "UYaxis", ConnectionData.UYaxis.ToString());
            manager.WritePrivateString("Calibrate", "VXaxis", ConnectionData.VXaxis.ToString());
            manager.WritePrivateString("Calibrate", "VYaxis", ConnectionData.VYaxis.ToString());
            manager.WritePrivateString("Calibrate", "AXaxis", ConnectionData.AXaxis.ToString());
            manager.WritePrivateString("Calibrate", "AYaxis", ConnectionData.AYaxis.ToString());
            manager.WritePrivateString("Calibrate", "WXaxis", ConnectionData.WXaxis.ToString());
            manager.WritePrivateString("Calibrate", "WYaxis", ConnectionData.WYaxis.ToString());
            manager.WritePrivateString("Calibrate", "AccuracySel", ConnectionData.AccuracySelect.ToString());
            manager.WritePrivateString("Calibrate", "Accuracy1", ConnectionData.Accuracy1.ToString());
            manager.WritePrivateString("Calibrate", "Accuracy2", ConnectionData.Accuracy2.ToString());

            manager.WritePrivateString("Preeflow", "PFDiameter", ConnectionData.PreeflowDiam.ToString());

            manager.WritePrivateString("PetriDistances", "PetriX", ConnectionData.PetriX.ToString());
            manager.WritePrivateString("PetriDistances", "PetriY", ConnectionData.PetriY.ToString());

            // Параметры рабочего нуля
            manager.WritePrivateString("ZeroPoints", "ZeroHead", ConnectionData.ZeroHead.ToString());
            manager.WritePrivateString("ZeroPoints", "XZero", ConnectionData.XZero.ToString());
            manager.WritePrivateString("ZeroPoints", "YZero", ConnectionData.YZero.ToString());
            manager.WritePrivateString("ZeroPoints", "ZZero", ConnectionData.ZZero.ToString());
            manager.WritePrivateString("ZeroPoints", "OffsetXZero", ConnectionData.OffsetXZero.ToString());
            manager.WritePrivateString("ZeroPoints", "OffsetYZero", ConnectionData.OffsetYZero.ToString());
            manager.WritePrivateString("ZeroPoints", "OffsetZZero", ConnectionData.OffsetZZero.ToString());

            //настройки принтхеда
            manager.WritePrivateString("Printhead", "zoom", ConnectionData.zoom.ToString());
            manager.WritePrivateString("Printhead", "focus", ConnectionData.focus.ToString());
            manager.WritePrivateString("Printhead", "obr1", ConnectionData.obr1.ToString());
            manager.WritePrivateString("Printhead", "obr2", ConnectionData.obr2.ToString());
            manager.WritePrivateString("Printhead", "obr3", ConnectionData.obr3.ToString());
            manager.WritePrivateString("Printhead", "obr4", ConnectionData.obr4.ToString());
            manager.WritePrivateString("Printhead", "Ard1", ConnectionData.Ard1.ToString());
            manager.WritePrivateString("Printhead", "Ard2", ConnectionData.Ard2.ToString());
            manager.WritePrivateString("Printhead", "Ard3", ConnectionData.Ard3.ToString());
            manager.WritePrivateString("Printhead", "Ard4", ConnectionData.Ard4.ToString());
            manager.WritePrivateString("Printhead", "Ard5", ConnectionData.Ard5.ToString());
            manager.WritePrivateString("Printhead", "Ard6", ConnectionData.Ard6.ToString());

        }

        bool init_bit = false;
        
        // Связь с контроллером ACS
        private void GetMotorState()
        {
            int X = ConnectionData.Value.ACSC_AXIS_0;
            int Y = ConnectionData.Value.ACSC_AXIS_1;

            int Z3 = ConnectionData.Value.ACSC_AXIS_2;
            int F3 = ConnectionData.Value.ACSC_AXIS_3;

            int Z1 = ConnectionData.Value.ACSC_AXIS_4;
            int F1 = ConnectionData.Value.ACSC_AXIS_5;

            int Z2 = ConnectionData.Value.ACSC_AXIS_6;
            int F2 = ConnectionData.Value.ACSC_AXIS_7;
            while (true)
            {
                try
                {
                    CheckForIllegalCrossThreadCalls = false;

                    switch (ConnectionData.ProgramStart)
                    {
                        case 10:
                            Indicator.BackColor = Color.YellowGreen;
                            Indicator.Text = Dict.LangStrings.StatusRun;
                            break;
                        case 20:
                            Indicator.BackColor = Color.Red;
                            Indicator.Text = Dict.LangStrings.StatusStop;
                            break;
                        case 30:
                            Indicator.BackColor = Color.Yellow;
                            Indicator.Text = Dict.LangStrings.StatusPause;
                            break;
                        default:
                            Indicator.BackColor = Color.LightBlue;
                            Indicator.Text = Dict.LangStrings.StatusManual;
                            break;
                    }

                    // Текущее положение осей
                    ConnectionData.FeedBackX =   ConnectionData.Value.GetFPosition(X);
                    ConnectionData.FeedBackY =   ConnectionData.Value.GetFPosition(Y);
                    ConnectionData.FeedBackZ1 = ConnectionData.Value.GetFPosition(Z1);
                    ConnectionData.FeedBackZ2 = ConnectionData.Value.GetFPosition(Z2);
                    ConnectionData.FeedBackZ3 = ConnectionData.Value.GetFPosition(Z3);

                    ConnectionData.GlobalX = ConnectionData.Value.ReadVariable("XGLOBAL", ConnectionData.Value.ACSC_NONE, 0, 0);
                    ConnectionData.GlobalY = ConnectionData.Value.ReadVariable("YGLOBAL", ConnectionData.Value.ACSC_NONE, 0, 0);

                    ConnectionData.XZoffsets = ConnectionData.Value.ReadVariable("OffsetX", ConnectionData.Value.ACSC_NONE, 2, 2);
                    ConnectionData.YZoffsets = ConnectionData.Value.ReadVariable("OffsetY", ConnectionData.Value.ACSC_NONE, 2, 2);

                    ConnectionData.XUoffsets = ConnectionData.Value.ReadVariable("OffsetX", ConnectionData.Value.ACSC_NONE, 3, 3);
                    ConnectionData.YUoffsets = ConnectionData.Value.ReadVariable("OffsetY", ConnectionData.Value.ACSC_NONE, 3, 3);

                    ConnectionData.XVoffsets = ConnectionData.Value.ReadVariable("OffsetX", ConnectionData.Value.ACSC_NONE, 4, 4);
                    ConnectionData.YVoffsets = ConnectionData.Value.ReadVariable("OffsetY", ConnectionData.Value.ACSC_NONE, 4, 4);

                    ConnectionData.XAoffsets = ConnectionData.Value.ReadVariable("OffsetX", ConnectionData.Value.ACSC_NONE, 6, 6);
                    ConnectionData.YAoffsets = ConnectionData.Value.ReadVariable("OffsetY", ConnectionData.Value.ACSC_NONE, 6, 6);

                    // Чтение состояния конечных выключателей
                    ConnectionData.XLLimit =    Convert.ToBoolean((bool)ConnectionData.Value.GetFault(X) & (bool)ConnectionData.Value.ACSC_SAFETY_LL);
                    ConnectionData.XRLimit =    Convert.ToBoolean((bool)ConnectionData.Value.GetFault(X) & (bool)ConnectionData.Value.ACSC_SAFETY_RL);
                    ConnectionData.YLLimit =    Convert.ToBoolean((bool)ConnectionData.Value.GetFault(Y) & (bool)ConnectionData.Value.ACSC_SAFETY_LL);
                    ConnectionData.YRLimit =    Convert.ToBoolean((bool)ConnectionData.Value.GetFault(Y) & (bool)ConnectionData.Value.ACSC_SAFETY_RL);
                    ConnectionData.ZS1LLimit =  Convert.ToBoolean((bool)ConnectionData.Value.GetFault(Z1) & (bool)ConnectionData.Value.ACSC_SAFETY_LL);
                    ConnectionData.ZS1RLimit =  Convert.ToBoolean((bool)ConnectionData.Value.GetFault(Z1) & (bool)ConnectionData.Value.ACSC_SAFETY_RL);
                    ConnectionData.ZS2LLimit =  Convert.ToBoolean((bool)ConnectionData.Value.GetFault(Z2) & (bool)ConnectionData.Value.ACSC_SAFETY_LL);
                    ConnectionData.ZS2RLimit =  Convert.ToBoolean((bool)ConnectionData.Value.GetFault(Z2) & (bool)ConnectionData.Value.ACSC_SAFETY_RL);
                    ConnectionData.ZS3LLimit =  Convert.ToBoolean((bool)ConnectionData.Value.GetFault(Z3) & (bool)ConnectionData.Value.ACSC_SAFETY_LL);
                    ConnectionData.ZS3RLimit =  Convert.ToBoolean((bool)ConnectionData.Value.GetFault(Z3) & (bool)ConnectionData.Value.ACSC_SAFETY_RL);
                    
                    // Чтение номера строки выполняемой в буффере 1 - исполнение G-кода
                    ConnectionData.ExecLine = (int)ConnectionData.Value.ReadVariable("PEXL",   ConnectionData.Value.ACSC_NONE, 2, 2);
                    ConnectionData.BufferSize = (int)ConnectionData.Value.ReadVariable("PCHARS",  ConnectionData.Value.ACSC_NONE, 2, 2);
                    ConnectionData.BufferError = (int)ConnectionData.Value.ReadVariable("PERR",    ConnectionData.Value.ACSC_NONE, 2, 2);
                    ConnectionData.BufferErrorString = (int)ConnectionData.Value.ReadVariable("PERL",    ConnectionData.Value.ACSC_NONE, 2, 2);

                    //label1.Text = (ConnectionData.Value.GetFault(ConnectionData.Value.ACSC_AXIS_0)).ToString();

                    // Обновление данные на других осях
                    //CallBackMy.callbackEventHandler("UpdateCamera");
                    CallBackMy2.callbackEventHandler("UpdateManual");
                    CallBackMy3.callbackEventHandler("UpdateAuto");
                    //CallBackMy4.callbackEventHandler("UpdateSettings");
                    CallBackMy5.callbackEventHandler("UpdateDiagnostics");
                    CallBackMy6.callbackEventHandler("UpdatePrinthead");
                    IPAdressLabel.Text = ConnectionData.ControllerIP;


                    IPAdressLabel.Text = ConnectionData.ControllerIP;

                    // Статус осей
                    int MotorStateX = ConnectionData.Value.GetMotorState(X);
                    int MotorStateY = ConnectionData.Value.GetMotorState(Y);
                    int MotorStateZ1 = ConnectionData.Value.GetMotorState(Z1);
                    int MotorStateZ2 = ConnectionData.Value.GetMotorState(Z2);
                    int MotorStateZ3 = ConnectionData.Value.GetMotorState(Z3);
                    int MotorStateS1 = ConnectionData.Value.GetMotorState(F1);
                    int MotorStateS2 = ConnectionData.Value.GetMotorState(F2);
                    int MotorStateS3 = ConnectionData.Value.GetMotorState(F3);

                    if (Convert.ToBoolean(MotorStateX & ConnectionData.Value.ACSC_MST_MOVE))
                        {
                            XStatusTL.BackColor = Color.YellowGreen;
                        }
                    else
                        {
                            XStatusTL.BackColor = SystemColors.Control;
                        }

                    if (Convert.ToBoolean(MotorStateY & ConnectionData.Value.ACSC_MST_MOVE))
                        {
                            YStatusTL.BackColor = Color.YellowGreen;
                        }
                    else
                        {
                            YStatusTL.BackColor = SystemColors.Control;
                        }

                    if (Convert.ToBoolean(MotorStateZ1 & ConnectionData.Value.ACSC_MST_MOVE))
                        {
                            Z1StatusTL.BackColor = Color.YellowGreen;
                        }
                    else
                        {
                            Z1StatusTL.BackColor = SystemColors.Control;
                        }

                    if (Convert.ToBoolean(MotorStateZ2 & ConnectionData.Value.ACSC_MST_MOVE))
                        {
                            Z2StatusTL.BackColor = Color.YellowGreen;
                        }
                    else
                        {
                            Z2StatusTL.BackColor = SystemColors.Control;
                        }

                    if (Convert.ToBoolean(MotorStateZ3 & ConnectionData.Value.ACSC_MST_MOVE))
                        {
                            Z3StatusTL.BackColor = Color.YellowGreen;
                        }
                    else
                        {
                            Z3StatusTL.BackColor = SystemColors.Control;
                        }

                    if (Convert.ToBoolean(MotorStateS1 & ConnectionData.Value.ACSC_MST_MOVE))
                        {
                            S1StatusTL.BackColor = Color.YellowGreen;
                        }
                    else
                        {
                            S1StatusTL.BackColor = SystemColors.Control;
                        }

                    if (Convert.ToBoolean(MotorStateS2 & ConnectionData.Value.ACSC_MST_MOVE))
                        {
                            S2StatusTL.BackColor = Color.YellowGreen;
                        }
                    else
                        {
                            S2StatusTL.BackColor = SystemColors.Control;
                        }

                    if (Convert.ToBoolean(MotorStateS3 & ConnectionData.Value.ACSC_MST_MOVE))
                        {
                            S3StatusTL.BackColor = Color.YellowGreen;
                        }
                    else
                        {
                            S3StatusTL.BackColor = SystemColors.Control;
                        }
/*
                   */
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
                //Thread.Sleep(10);
            }
        }
        void check_message()
        {
            while(true)
            {
                Console.WriteLine(ConnectionData.Value.device.reseav());
                Thread.Sleep(1000);
            }
            
        }
        // Установка связи с контроллером
        public void SetCommunication()
        {
            ConnectionData.Comport = SelectDrv.getPort();
            ConnectionData.Value = new ChanelOct();
            //ConnectionData.Value.device.connectStart();
            Console.WriteLine(ConnectionData.Value.device.reseav());
            MotorStateThr = new Thread(check_message);
            MotorStateThr.Start();

            axes = new int[]
                            {
                             ConnectionData.Value.ACSC_AXIS_0,
                             ConnectionData.Value.ACSC_AXIS_1,
                             ConnectionData.Value.ACSC_AXIS_2,
                             ConnectionData.Value.ACSC_AXIS_3,
                             ConnectionData.Value.ACSC_AXIS_4,
                             ConnectionData.Value.ACSC_AXIS_5,
                             ConnectionData.Value.ACSC_AXIS_6,
                             ConnectionData.Value.ACSC_AXIS_7,
                             -1
                            };

        }

        private void BTP_Load(object sender, EventArgs e)
        {
            string LogFile = Application.StartupPath + "\\Log\\Log_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".log";
            Console.WriteLine(LogFile);
            ConnectionData.fileStream = new FileStream(LogFile, FileMode.Create, FileAccess.Write);
            ConnectionData.streamWriter = new StreamWriter(ConnectionData.fileStream);
            
            this.DoubleBuffered = true;
            LoadData();
            SetCommunication();
            programm_buffer = ConnectionData.Value.ACSC_BUFFER_1;
            //  SendDiam();
        }

        Manual_frm Manual;
        Program_frm Program;
        Auto_frm Auto;
        Diagnostics_frm Diagnostics;
        Settings_frm Settings;
        SelectDriver_frm SelectDrv;
        Printhead_frm Printhead1;

        public void ChangeFormLanguage(AvaliableLocalizations newLocalization)
        {
            Sett sett1 = new Sett();
           sett1.SetCulture(newLocalization);

            var resources = new ComponentResourceManager(typeof(Main_frm));
            CultureInfo newCultureInfo = new CultureInfo(EnumDescriptionHelper.GetEnumDescription(newLocalization));
            foreach (Control C in this.Controls)
            {
                if (C is Panel)
                {
                    foreach (Control P in C.Controls)
                    {
                        resources.ApplyResources(P, P.Name, newCultureInfo);
                    }
                }
                else
                {
                    resources.ApplyResources(C, C.Name, newCultureInfo);
                }
            }
            resources.ApplyResources(this, "$this", newCultureInfo);
            foreach (var item in SS_Status.Items.Cast<ToolStripItem>().Where(item => (item is ToolStripStatusLabel) != false))
                   {
                         resources.ApplyResources(item, item.Name, newCultureInfo);
                    }
                TSDD_Language.Text = newCultureInfo.NativeName;

            SetCurrenLanguageButtonChecked();
        }

        private void SetCurrenLanguageButtonChecked()
        {
            foreach (ToolStripMenuItem languageButton in TSDD_Language.DropDownItems)
            {
                languageButton.Checked = (languageButton.Text == TSDD_Language.Text);
            }
        }

        private void EnglishTSMI_Click(object sender, EventArgs e)
        {
            ChangeFormLanguage(AvaliableLocalizations.English);
            Manual.ChangeFormLanguage(AvaliableLocalizations.English);
            Auto.ChangeFormLanguage(AvaliableLocalizations.English);
            Diagnostics.ChangeFormLanguage(AvaliableLocalizations.English);
            SelectDrv.ChangeFormLanguage(AvaliableLocalizations.English);
            Settings.ChangeFormLanguage(AvaliableLocalizations.English);
            //Camera1frm.ChangeFormLanguage(AvaliableLocalizations.English);

        }
        
        private void RussianTSMI_Click(object sender, EventArgs e)
        {
            ChangeFormLanguage(AvaliableLocalizations.Russian);
            Manual.ChangeFormLanguage(AvaliableLocalizations.Russian);
            Auto.ChangeFormLanguage(AvaliableLocalizations.Russian);
            Diagnostics.ChangeFormLanguage(AvaliableLocalizations.Russian);
            SelectDrv.ChangeFormLanguage(AvaliableLocalizations.Russian);
            Settings.ChangeFormLanguage(AvaliableLocalizations.Russian);
            //Camera1frm.ChangeFormLanguage(AvaliableLocalizations.Russian);
        }

        private void Main_frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        private void EStopBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.KillAll();
                    ConnectionData.Value.StopBuffer(ConnectionData.Value.ACSC_NONE);
                    //string origFile =   Application.StartupPath + "\\Data\\Homing.dll";
                    //string repFile =    Application.StartupPath + "\\Data\\Homing.prg";
                    //File.Copy(origFile, repFile, true);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            int[] ax = new int[]
                                    {
                                        ConnectionData.Value.ACSC_AXIS_0,
                                        ConnectionData.Value.ACSC_AXIS_1,
                                        ConnectionData.Value.ACSC_AXIS_2,
                                        ConnectionData.Value.ACSC_AXIS_3,
                                        ConnectionData.Value.ACSC_AXIS_4,
                                        ConnectionData.Value.ACSC_AXIS_5,
                                        ConnectionData.Value.ACSC_AXIS_6,
                                        ConnectionData.Value.ACSC_AXIS_7,
                                        -1
                                    };
            //ConnectionData.ProgramStart = false;
            if (ConnectionData.ProgramStart == 30)
            {
                ConnectionData.Value.ExtToPointM(
                    ConnectionData.Value.ACSC_AMF_VELOCITY, ax, 
                    SaveCoordinates,
                    ConnectionData.Value.GetVelocity(ConnectionData.Value.ACSC_AXIS_0),
                    ConnectionData.Value.GetVelocity(ConnectionData.Value.ACSC_AXIS_0));
                //ConnectionData.Value.HaltM(ax);
                ConnectionData.Value.EndSequenceM(ax);
                ConnectionData.Value.WaitMotionEnd(ConnectionData.Value.ACSC_AXIS_0, 100000);
                ConnectionData.Value.WaitMotionEnd(ConnectionData.Value.ACSC_AXIS_1, 100000);
                ConnectionData.Value.WaitMotionEnd(ConnectionData.Value.ACSC_AXIS_2, 100000);
                ConnectionData.Value.WaitMotionEnd(ConnectionData.Value.ACSC_AXIS_3, 100000);
                ConnectionData.Value.WaitMotionEnd(ConnectionData.Value.ACSC_AXIS_4, 100000);
                ConnectionData.Value.WaitMotionEnd(ConnectionData.Value.ACSC_AXIS_5, 100000);
                ConnectionData.Value.WaitMotionEnd(ConnectionData.Value.ACSC_AXIS_6, 100000);
                ConnectionData.Value.RunBuffer(programm_buffer);
                ConnectionData.ProgramStart = 10;
                PauseSignal = false;
            }
            else
            {

                if (Convert.ToBoolean(
                    ((bool)ConnectionData.Value.GetProgramState(programm_buffer)) &
                   (bool) (ConnectionData.Value.ACSC_PST_RUN)) == false)
                {
                    if (Convert.ToBoolean((bool)ConnectionData.Value.GetProgramState(programm_buffer)
                        & (bool)(ConnectionData.Value.ACSC_PST_COMPILED)) == true)
                    {

                        ConnectionData.Value.RunBuffer(programm_buffer);
                        ConnectionData.ProgramStart = 10;
                        ConnectionData.DateStr = DateTime.Now.ToString("dd MMMM yyyy");
                        ConnectionData.TimeStr = DateTime.Now.ToString("HH:mm:ss");
                    }
                }
                else
                    {
                        MessageBox.Show("Buffer not compiled");
                    }    
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            axes = new int[]
                           {
                             ConnectionData.Value.ACSC_AXIS_0,
                             ConnectionData.Value.ACSC_AXIS_1,
                             ConnectionData.Value.ACSC_AXIS_2,
                             ConnectionData.Value.ACSC_AXIS_3,
                             ConnectionData.Value.ACSC_AXIS_4,
                             ConnectionData.Value.ACSC_AXIS_5,
                             ConnectionData.Value.ACSC_AXIS_6,
                             ConnectionData.Value.ACSC_AXIS_7,
                             -1
                           };
            if ((ConnectionData.ProgramStart == 10) || (ConnectionData.ProgramStart == 30))
            {
                ConnectionData.Value.HaltM(axes);
                ConnectionData.Value.StopBuffer(programm_buffer);
                ConnectionData.ProgramStart = 20;
            }
        }

        bool PauseSignal;
        double[] SaveCoordinates;
        int Line;

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            axes = new int[]
               {
                             ConnectionData.Value.ACSC_AXIS_0,
                             ConnectionData.Value.ACSC_AXIS_1,
                             ConnectionData.Value.ACSC_AXIS_2,
                             ConnectionData.Value.ACSC_AXIS_3,
                             ConnectionData.Value.ACSC_AXIS_4,
                             ConnectionData.Value.ACSC_AXIS_5,
                             ConnectionData.Value.ACSC_AXIS_6,
                             ConnectionData.Value.ACSC_AXIS_7,
                             -1
               };
            if (ConnectionData.ProgramStart == 10)
                {
                    ConnectionData.Value.HaltM(axes);
                    ConnectionData.Value.EndSequenceM(axes);
                    ConnectionData.Value.SuspendBuffer(programm_buffer);
                    SaveCoordinates = new double[] 
                        {
                                ConnectionData.FeedBackX,
                                ConnectionData.FeedBackY,
                                ConnectionData.FeedBackZ1,
                                ConnectionData.FeedBackZ2,
                                ConnectionData.FeedBackZ3,

                        };
                    Line = ConnectionData.ExecLine;
                    PauseSignal = true;
                    ConnectionData.ProgramStart = 30;
                }

        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            ConnectionData.Value.ClearBuffer(programm_buffer, 1, 10000);
            //string TempFile = Application.StartupPath + "\\Temp\\CNC.tmp";
            //ConnectionData.first_bit = false;
            //System.IO.File.Delete(TempFile);

        }

        private void Main_frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                /*
                if (ConnectionData.Camera1 != null)
                    {
                    ConnectionData.Camera1.Close();
                    ConnectionData.Camera1.Dispose();
                    ConnectionData.Camera1 = null;
                    }

                if (ConnectionData.Camera2 != null)
                {
                    ConnectionData.Camera2.Close();
                    ConnectionData.Camera2.Dispose();
                    ConnectionData.Camera2 = null;
                }
                */

                if (ConnectionData.bConnected)
                {
                    ConnectionData.Value.CloseMessageBuffer();
                    ConnectionData.Value.KillAll();
                    ConnectionData.Value.StopBuffer(ConnectionData.Value.ACSC_NONE);

                    MotorStateThr.Abort();//прерываем поток
                    ConnectionData.Value.DisableAll();
                    ConnectionData.Value.CancelOperation();
                    ConnectionData.Value.CloseComm();
                }
                string TempFile = Application.StartupPath + "\\Temp\\CNC.tmp";
                if (File.Exists(TempFile))
                {
                    System.IO.File.Delete(TempFile);
                }
                
                TempFile = Application.StartupPath + "\\Temp\\Visu.tmp";

                if (File.Exists(TempFile))
                {
                    System.IO.File.Delete(TempFile);
                }

                                
                TempFile = Application.StartupPath + "\\Temp\\Report.xlsx";

                if (File.Exists(TempFile))
                {
                    System.IO.File.Delete(TempFile);
                }

                ConnectionData.streamWriter.Close();
                ConnectionData.fileStream.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void Main_frm_Shown(object sender, EventArgs e)
        {
            
        }

        private void FindHomeBtn_Click(object sender, EventArgs e)
        {

        }

        
    }
    }