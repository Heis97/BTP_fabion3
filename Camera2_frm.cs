using AForge.Video.VFW;
using Basler.Pylon;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace BTP
{
    public partial class Camera2_frm : Form
    {
        public Camera2_frm()
        {
            InitializeComponent();
        }



        private Camera Camera2 = null;
        private PixelDataConverter converter = new PixelDataConverter();
        private Stopwatch stopWatch = new Stopwatch();



        AVIWriter writer2 = new AVIWriter("wmv3");


       

        private void Camera2_frm_Shown(object sender, EventArgs e)
        {


            ConnectToCamera(ConnectionData.Camera2SN);
            ContinuousShot(Camera2);
        }

        private void Camera2_frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DestroyCamera(Camera2);
        }

        // Подключение к камерам
        public void ConnectToCamera(string SN2)
        {
            try
            {
                Camera2 = new Camera(SN2);

                Camera2.CameraOpened += Configuration.AcquireContinuous;

                // Register for the events of the image provider needed for proper operation.
                Camera2.ConnectionLost += OnConnectionLost;
                Camera2.CameraOpened += OnCameraOpened;
                Camera2.CameraClosed += OnCameraClosed;
                Camera2.StreamGrabber.GrabStarted += OnGrabStarted;
               // Camera2.StreamGrabber.ImageGrabbed += OnImageGrabbed2;
                Camera2.StreamGrabber.GrabStopped += OnGrabStopped;
                // Open the connection to the camera device.
                Camera2.Open();

            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }

        // Occurs when a device with an opened connection is removed.
        private void OnConnectionLost(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnConnectionLost), sender, e);
                return;
            }
        }
        // Occurs when the connection to a camera device is opened.
        private void OnCameraOpened(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnCameraOpened), sender, e);
                return;
            }
        }

        // Occurs when the connection to a camera device is closed.
        private void OnCameraClosed(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnCameraClosed), sender, e);
                return;
            }
        }

        // Occurs when a camera starts grabbing.
        private void OnGrabStarted(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnGrabStarted), sender, e);
                return;
            }

            // Reset the stopwatch used to reduce the amount of displayed images. The camera may acquire images faster than the images can be displayed.
            stopWatch.Reset();
        }


       
        private void ErorMsg(COMException Ex)
        {
            string Str = Dict.LangStrings.Error + Ex.Source + "\n\r";
            Str = Str + Ex.Message + "\n\r";
            Str = Str + "HRESULT:" + String.Format("0x{0:X}", (Ex.ErrorCode));
            MessageBox.Show(Str, "EnableEvent");
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
                ShowException(exception);
            }
        }

        // Обработка ошибок
        private static void ShowException(Exception exception)
        {
            MessageBox.Show("Exception caught:\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Отображение на экране
        public static void ContinuousShot(Camera cam)
        {
            if (cam != null)
            {
                try
                {
                    // Start the grabbing of images until grabbing is stopped.
                    cam.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                    cam.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                }
                catch (Exception exception)
                {
                    ShowException(exception);
                }
            }
        }

        // Occurs when a camera has stopped grabbing.
        private void OnGrabStopped(Object sender, GrabStopEventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<GrabStopEventArgs>(OnGrabStopped), sender, e);
                return;
            }

            // Reset the stopwatch.
            stopWatch.Reset();

            // If the grabbed stop due to an error, display the error message.
            if (e.Reason != GrabStopReason.UserRequest)
            {
                MessageBox.Show("A grab error occured:\n" + e.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


       
    }
}
