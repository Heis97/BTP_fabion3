using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Text;
using Basler.Pylon;
using System.IO;

namespace BTP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main_frm());         
        }
    }

    public static class CallBackMy
    {
        public delegate void callbackEvent(string what);
        public static callbackEvent callbackEventHandler;
    }

    public static class CallBackMy2
    {
        public delegate void callbackEvent(string what);
        public static callbackEvent callbackEventHandler;
    }

    public static class CallBackMy3
    {
        public delegate void callbackEvent(string what);
        public static callbackEvent callbackEventHandler;
    }

    //public static class CallBackMy4
    //{
    //    public delegate void callbackEvent(string what);
    //    public static callbackEvent callbackEventHandler;
   // }

    public static class CallBackMy5
    {
        public delegate void callbackEvent(string what);
        public static callbackEvent callbackEventHandler;
    }

    public static class CallBackMy6
    {
        public delegate void callbackEvent(string what);
        public static callbackEvent callbackEventHandler;
    }

    public static class EnumDescriptionHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
              (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }

    //Класс для чтения/записи INI-файлов
    public class INIManager
    {
        //Конструктор, принимающий путь к INI-файлу
        public INIManager(string aPath)
        {
            path = aPath;
        }

        //Конструктор без аргументов (путь к INI-файлу нужно будет задать отдельно)
        public INIManager() : this("") { }

        //Возвращает значение из INI-файла (по указанным секции и ключу) 
        public string GetPrivateString(string aSection, string aKey)
        {
            //Для получения значения
            StringBuilder buffer = new StringBuilder(SIZE);

            //Получить значение в buffer
            GetPrivateString(aSection, aKey, null, buffer, SIZE, path);

            //Вернуть полученное значение
            return buffer.ToString();
        }

        //Пишет значение в INI-файл (по указанным секции и ключу) 
        public void WritePrivateString(string aSection, string aKey, string aValue)
        {
            //Записать значение в INI-файл
            WritePrivateString(aSection, aKey, aValue, path);
        }

        //Возвращает или устанавливает путь к INI файлу
        public string Path { get { return path; } set { path = value; } }

        //Поля класса
        private const int SIZE = 1024; //Максимальный размер (для чтения значения из файла)
        private string path = null; //Для хранения пути к INI-файлу

        //Импорт функции GetPrivateProfileString (для чтения значений) из библиотеки kernel32.dll
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateString(string section, string key, string def, StringBuilder buffer, int size, string path);

        //Импорт функции WritePrivateProfileString (для записи значений) из библиотеки kernel32.dll
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern int WritePrivateString(string section, string key, string str, string path);
    }


    public class Sett
{
          public AvaliableLocalizations CurrentLocalization;

    internal CultureInfo GetCulture()
    {
        return new CultureInfo(EnumDescriptionHelper.GetEnumDescription(CurrentLocalization));
    }

    internal void SetCulture(AvaliableLocalizations newLocalization)
    {
        CurrentLocalization = newLocalization;
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(EnumDescriptionHelper.GetEnumDescription(CurrentLocalization));
    }
}

    // Универсальные данные
    static class ConnectionData
    {
        // public static SPIIPLUSCOM660Lib.Channel Value { get; set; }
        public static string Comport { get; set; }
        public static ChanelOct Value { get; set; }
        public static Camera Camera1 { get; set; }
        public static Camera Camera2 { get; set; }
        public static FileStream fileStream { get; set; }
        public static StreamWriter streamWriter { get; set; }
        public static double MaxXYVel { get; set; }
        public static double MaxZVel { get; set; }
        public static double MaxPtVel { get; set; }
        public static double MaxPFVel { get; set; }
        public static double MaxCTVel { get; set; }
        public static double MaxCBVel { get; set; }
        public static double PfMix { get; set; }
        public static double SetXYVel { get; set; }
        public static double SetZVel { get; set; }
        public static double SetPtVel { get; set; }
        public static double SetPFVel { get; set; }
        public static double SetCTVel { get; set; }
        public static double SetCBVel { get; set; }
        public static double FeedBackX { get; set; }
        public static double FeedBackY { get; set; }
        public static double FeedBackZ1 { get; set; }
        public static double FeedBackZ2 { get; set; }
        public static double FeedBackZ3 { get; set; }
        public static double FeedBackZSp { get; set; }
        public static double FeedBackCam1X { get; set; }
        public static double FeedBackCam1Y { get; set; }
        public static double FeedBackCam2X { get; set; }
        public static double FeedBackCam2Y { get; set; }
        public static double FeedBackZoom { get; set; }

        public static bool bConnected { get; set; }
        public static double XOffset { get; set; }
        public static double YOffset { get; set; }
        public static double ZOffset { get; set; }
        public static double ZS1Offset { get; set; }
        public static double ZS2Offset { get; set; }
        public static double ZS3Offset { get; set; }
        public static double ZSpOffset { get; set; }
        public static double ZPFOffset { get; set; }
        public static string ControllerIP { get; set; }
        public static double S1Diameter { get; set; }
        public static double S1ProcDiameter { get; set; }
        public static double S2Diameter { get; set; }
        public static double S2ProcDiameter { get; set; }
        public static double S3Diameter { get; set; }
        public static double S3ProcDiameter { get; set; }
        public static double PF1Diameter { get; set; }
        public static double PF2Diameter { get; set; }
        public static double PetriDishDiam { get; set; }
        public static double WellNum { get; set; }
        public static string Camera1SN { get; set; }
        public static string Camera2SN { get; set; }
        public static double ScaleCam1 { get; set; }
        public static double ScaleCam2 { get; set; }
        public static bool XLLimit { get; set; }
        public static bool XRLimit { get; set; }
        public static bool YLLimit { get; set; }
        public static bool YRLimit { get; set; }
        public static bool ZS1LLimit { get; set; }
        public static bool ZS1RLimit { get; set; }
        public static bool ZS2LLimit { get; set; }
        public static bool ZS2RLimit { get; set; }
        public static bool ZS3LLimit { get; set; }
        public static bool ZS3RLimit { get; set; }
        public static bool ZSpLLimit { get; set; }
        public static bool ZSpRLimit { get; set; }
        public static bool ZPFLLimit { get; set; }
        public static bool ZPFRLimit { get; set; }
        public static int ExecLine { get; set; }
        public static int BufferSize { get; set; }
        public static int BufferError { get; set; }
        public static int BufferErrorString { get; set; }
        public static bool first_bit { get; set; }
        public static double Camera2XStrokeMax { get; set; }
        public static double Camera2YStrokeMax { get; set; }
        public static double Camera1ZoomStrokeMax { get; set; }
        public static double CalDistanceX { get; set; }
        public static double CalDistanceY { get; set; }
        public static double CalDistanceZ { get; set; }
        public static double CalVelocityX { get; set; }
        public static double CalVelocityY { get; set; }
        public static double CalVelocityZ { get; set; }
        public static double HomeVelocityZ { get; set; }
        public static double HomeVelocityX { get; set; }
        public static double HomeVelocityY { get; set; }
        public static double HomeVelocityCam { get; set; }
        public static double ZXaxis { get; set; }
        public static double ZYaxis { get; set; }
        public static double UXaxis { get; set; }
        public static double UYaxis { get; set; }
        public static double VXaxis { get; set; }
        public static double VYaxis { get; set; }
        public static double AXaxis { get; set; }
        public static double AYaxis { get; set; }
        public static double WXaxis { get; set; }
        public static double WYaxis { get; set; }
        public static double XZoffsets { get; set; }
        public static double YZoffsets { get; set; }
        public static double XVoffsets { get; set; }
        public static double YVoffsets { get; set; }
        public static double XUoffsets { get; set; }
        public static double YUoffsets { get; set; }
        public static double XAoffsets { get; set; }
        public static double YAoffsets { get; set; }
        public static int ProgramStart { get; set; }
        public static double GlobalX { get; set; }
        public static double GlobalY { get; set; }
        public static double ToolX { get; set; }
        public static double ToolY { get; set; }
        public static double FeedBackS1 { get; set; }
        public static double FeedBackS2 { get; set; }
        public static double FeedBackS3 { get; set; }
        public static double FeedBackPF1 { get; set; }
        public static double FeedBackPF2 { get; set; }
        public static int HeadNum { get; set; }
        public static double PreeflowDiam { get; set; }
        public static double PetriX { get; set; }
        public static double PetriY { get; set; }
        public static string DateStr { get; set; }
        public static string TimeStr { get; set; }
        public static double DrawDishX { get; set; }
        public static double DrawDishY { get; set; }
        public static int DrawMode { get; set; }
        public static int AccuracySelect { get; set; }
        public static double Accuracy1 { get; set; }
        public static double Accuracy2 { get; set; }
        public static double XZero { get; set; }
        public static double YZero { get; set; }
        public static double ZZero { get; set; }
        public static int ZeroHead { get; set; }
        public static double OffsetXZero { get; set; }
        public static double OffsetYZero { get; set; }
        public static double OffsetZZero { get; set; }
        public static int zoom { get; set; }
        public static  int focus { get; set; }
        public static string obr1 { get; set; }
        public static string obr2 { get; set; }
        public static string obr3 { get; set; }
        public static string obr4 { get; set; }
        public static string Ard1 { get; set; }
        public static string Ard2 { get; set; }
        public static string Ard3 { get; set; }
        public static string Ard4 { get; set; }
        public static string Ard5 { get; set; }
        public static string Ard6 { get; set; }

        public static double X_Laser_offset { get; set; }
        public static double Y_Laser_offset { get; set; }
        public static double Z_Laser_offset { get; set; }

        public static int Z1Calibrated { get; set; }
        public static int Z2Calibrated { get; set; }
        public static int Z3Calibrated { get; set; }
    }

}
