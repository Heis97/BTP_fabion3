using Active_Directory_Worker.Interfaces;
using AForge.Video.VFW;
using Basler.Pylon;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace BTP
{
    public partial class Camera1_frm : Form, ILanguageChangable
    {
 

       // private PixelDataConverter converter = new PixelDataConverter();
        private Stopwatch stopWatch = new Stopwatch();

        public Camera1_frm()
        {
            CallBackMy.callbackEventHandler = new CallBackMy.callbackEvent(this.Reload);
            InitializeComponent();
        }

        //public Main_frm form;
      
        //Thread SaveVideoCam1 = null;
        //bool ThreadStopCam1 = false;
        //Thread SaveVideoCam2 = null;
        //bool ThreadStopCam2 = false;

       // Thread UpdateDataThread = null;

        AVIWriter writer1 = new AVIWriter("wmv3");
        AVIWriter writer2 = new AVIWriter("wmv3");

        public void ChangeFormLanguage(AvaliableLocalizations newLocalization)
        {
            Sett sett1 = new Sett();
            sett1.SetCulture(newLocalization);

            var resources = new ComponentResourceManager(typeof(Camera1_frm));
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

    void Reload(string param)
        {
            Cam2XCoord.Text = string.Format("{0:F2}", ConnectionData.FeedBackCam2X);
            Cam2YCoord.Text = string.Format("{0:F2}", ConnectionData.FeedBackCam2Y);

            Camera1XCoord.Text = string.Format("{0:F2}", ConnectionData.FeedBackCam1X);
            double Zoomval = (ConnectionData.FeedBackZoom / ConnectionData.Camera1ZoomStrokeMax);
            if (Zoomval < 0.2)
            {
                ZoomLable.Text = Dict.LangStrings.Zoom + string.Format("{0:F0}", 1) + "X";
            }
            else if ((Zoomval > 0.2) && (Zoomval < 0.5))
            {
                ZoomLable.Text = Dict.LangStrings.Zoom + string.Format("{0:F0}", 2) + "X";
            }
            else if ((Zoomval > 0.5) && (Zoomval < 0.8))
            {
                ZoomLable.Text = Dict.LangStrings.Zoom + string.Format("{0:F0}", 3) + "X";
            }
            else
            {
                ZoomLable.Text = Dict.LangStrings.Zoom + string.Format("{0:F0}", 4) + "X";
            }

                if (Convert.ToBoolean(ConnectionData.Value.ReadVariable("MFLAGS", ConnectionData.Value.ACSC_NONE, 11, 11) & 8) &&
                Convert.ToBoolean(ConnectionData.Value.ReadVariable("MFLAGS", ConnectionData.Value.ACSC_NONE, 10, 10) & 8))
            {
                if ((ConnectionData.FeedBackCam2Y < 0) || (ConnectionData.FeedBackCam2Y > ConnectionData.Camera2YStrokeMax))
                {
                    ConnectionData.Value.SetRPosition(ConnectionData.Value.ACSC_AXIS_11, ConnectionData.Camera2XStrokeMax);
                    ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_11);
                }
                if ((ConnectionData.FeedBackCam2X < 0) || (ConnectionData.FeedBackCam2X > ConnectionData.Camera2XStrokeMax))
                {
                    ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_10);
                    ConnectionData.Value.SetRPosition(ConnectionData.Value.ACSC_AXIS_10, 0);
                }
            }
        }

        private void Camera1_frm_Load(object sender, EventArgs e)
            {
            //
                ConnectToCamera(ConnectionData.Camera1SN, ConnectionData.Camera2SN);
                ContinuousShot(ConnectionData.Camera1);
                ContinuousShot(ConnectionData.Camera2);
        }

        private void ErorMsg(COMException Ex)
            {
                string Str = Dict.LangStrings.Error + Ex.Source + "\n\r";
                Str = Str + Ex.Message + "\n\r";
                Str = Str + "HRESULT:" + String.Format("0x{0:X}", (Ex.ErrorCode));
                MessageBox.Show(Str, "EnableEvent");
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

        // Подключение к камерам
        public void ConnectToCamera(string SN1, string SN2)
        {
            try
            {
                // Create a new camera object.
                ConnectionData.Camera2 = new Camera(SN2);

                ConnectionData.Camera2.CameraOpened += Configuration.AcquireContinuous;

                // Register for the events of the image provider needed for proper operation.
                ConnectionData.Camera2.ConnectionLost += OnConnectionLost2;
                ConnectionData.Camera2.CameraOpened += OnCameraOpened2;
                ConnectionData.Camera2.CameraClosed += OnCameraClosed2;
                ConnectionData.Camera2.StreamGrabber.GrabStarted += OnGrabStarted2;
                ConnectionData.Camera2.StreamGrabber.ImageGrabbed += OnImageGrabbed2;
                ConnectionData.Camera2.StreamGrabber.GrabStopped += OnGrabStopped2;
                // Open the connection to the camera device.
                ConnectionData.Camera2.Open();
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
            try
            {

                // Create a new camera object.
                ConnectionData.Camera1 = new Camera(SN1);

                ConnectionData.Camera1.CameraOpened += Configuration.AcquireContinuous;

                // Register for the events of the image provider needed for proper operation.
                ConnectionData.Camera1.ConnectionLost += OnConnectionLost1;
                ConnectionData.Camera1.CameraOpened += OnCameraOpened1;
                ConnectionData.Camera1.CameraClosed += OnCameraClosed1;
                ConnectionData.Camera1.StreamGrabber.GrabStarted += OnGrabStarted1;
                ConnectionData.Camera1.StreamGrabber.ImageGrabbed += OnImageGrabbed1;
                ConnectionData.Camera1.StreamGrabber.GrabStopped += OnGrabStopped1;
                // Open the connection to the camera device.
                ConnectionData.Camera1.Open();
             }
            catch (Exception exception)
            {
                ShowException(exception);
            }
        }

        // Occurs when a device with an opened connection is removed.
        private void OnConnectionLost1(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnConnectionLost1), sender, e);
                return;
            }
        }


        // Occurs when a device with an opened connection is removed.
        private void OnConnectionLost2(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnConnectionLost2), sender, e);
                return;
            }
        }

        // Occurs when the connection to a camera device is opened.
        private void OnCameraOpened2(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnCameraOpened2), sender, e);
                return;
            }
        }

        private void OnCameraOpened1(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnCameraOpened1), sender, e);
                return;
            }
        }

        // Occurs when a camera starts grabbing.
        private void OnGrabStarted2(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnGrabStarted2), sender, e);
                return;
            }

            // Reset the stopwatch used to reduce the amount of displayed images. The camera may acquire images faster than the images can be displayed.
            stopWatch.Reset();
        }

        private void OnGrabStarted1(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnGrabStarted1), sender, e);
                return;
            }

            // Reset the stopwatch used to reduce the amount of displayed images. The camera may acquire images faster than the images can be displayed.
            stopWatch.Reset();
        }

        // Occurs when the connection to a camera device is closed.
        private void OnCameraClosed2(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnCameraClosed2), sender, e);
                return;
            }
        }

        private void OnCameraClosed1(Object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<EventArgs>(OnCameraClosed1), sender, e);
                return;
            }
        }

        // Обработка ошибок
        private static void ShowException(Exception exception)
        {
            MessageBox.Show("Exception caught:\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SnapCam1_Click(object sender, EventArgs e)
        {
            string SnapDir = Application.StartupPath + "\\Screenshots\\Camera1_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".jpg";
            Camera1PictureBox.Image.Save(@SnapDir);
            MessageBox.Show(Dict.LangStrings.FileSaved + SnapDir);
        }

        private void RecVideoCam1_Click(object sender, EventArgs e)
        {
            //SaveVideoCam1 = new Thread(new ThreadStart(SaveCamera1));
            //SaveVideoCam1.IsBackground = true;
            string VideoDir = Application.StartupPath + "\\Video\\Camera1_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".avi";
            if (RecVideoCam1.BackColor == SystemColors.Control)
            {
                //ThreadStopCam1 = false;
                RecVideoCam1.BackColor = Color.YellowGreen;
                writer1.FrameRate = 8;
                writer1.Open(VideoDir, Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Width].GetValue()), Convert.ToInt32(ConnectionData.Camera1.Parameters[PLCamera.Height].GetValue()));
                Camera1SaveTimer.Enabled = true;
                StartRecCam1 = true;
                //SaveVideoCam1.Start();
            }
            else
            {
                RecVideoCam1.BackColor = SystemColors.Control;
                //ThreadStopCam1 = true;
                //SaveVideoCam1.Abort();
                Camera1SaveTimer.Enabled = false;
                StartRecCam1 = false;
                writer1.Close();
                MessageBox.Show(Dict.LangStrings.VideoSaved + " " + VideoDir);
                
            }
        }

        bool StartRecCam2;

        private void RecVideoCam2_Click(object sender, EventArgs e)
        {
            //SaveVideoCam2 = new Thread(new ThreadStart(SaveCamera2));
            //SaveVideoCam2.IsBackground = true;
            string VideoDir = Application.StartupPath + "\\Video\\Camera2_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".avi";
            if (RecVideoCam2.BackColor == SystemColors.Control)
            {
               // ThreadStopCam2 = false;
                RecVideoCam2.BackColor = Color.YellowGreen;
                writer2.FrameRate = 8;
                writer2.Open(VideoDir, Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Width].GetValue()), Convert.ToInt32(ConnectionData.Camera2.Parameters[PLCamera.Height].GetValue()));
                StartRecCam2 = true;
                Camera2SaveTimer.Enabled = true;
                //SaveVideoCam2.Start();
            }
            else
            {
                RecVideoCam2.BackColor = SystemColors.Control;
                //ThreadStopCam2 = true;
                //SaveVideoCam1.Abort();
                Camera2SaveTimer.Enabled = false;
                StartRecCam2 = false;
                writer2.Close();
                MessageBox.Show(Dict.LangStrings.VideoSaved + " " + VideoDir);
                
            }
        }

        // Сохранение изображения с камеры 1
       // void SaveCamera1()
       // {
       //     while (!ThreadStopCam1)
       //     {
       //         Bitmap bmpCam1 = new Bitmap(Camera1PictureBox.Image);
       //         try
       //             {
       //                 writer1.AddFrame(bmpCam1);
       //             }
       //         catch (Exception Ex)
       //             {
       //                 MessageBox.Show(Ex.Message);
       //            }
       //         Thread.Sleep(40);
       //     }
       // }

        // Сохранение изображения с камеры 2
        //void SaveCamera2()
        //{
        //    while (!ThreadStopCam2)
        //    {
        //       Bitmap bmpCam2 = new Bitmap(Camera2PictureBox.Image);
        //        try
        //        {
        //            writer2.AddFrame(bmpCam2);
         //       }
         //       catch (Exception Ex)
         //       {
         //           MessageBox.Show(Ex.Message);
         //       }
         //       Thread.Sleep(40);
        //    }
        //}

        int ClickCountCam2 = 0;
        int OldPosXCam2 = 0;
        int OldPosYCam2 = 0;
        double DimCam2;

        private void SnapCam2_Click(object sender, EventArgs e)
        {
            string SnapDir = Application.StartupPath + "\\Screenshots\\Camera2_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".jpg";
            Camera2PictureBox.Image.Save(@SnapDir);
            MessageBox.Show(Dict.LangStrings.FileSaved + SnapDir);
        }

        // Occurs when an image has been acquired and is ready to be processed.
        private void OnImageGrabbed2(Object sender, ImageGrabbedEventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
                // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread.
                BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed2), sender, e.Clone());
                return;
            }

            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.

                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;

                // Check if the image can be displayed.
                if (grabResult.IsValid)
                {
                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    if (!stopWatch.IsRunning || stopWatch.ElapsedMilliseconds > 33)
                    {
                        stopWatch.Restart();

                        Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                        // Lock the bits of the bitmap.
                        BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        // Place the pointer to the buffer of the bitmap.

                        //Bitmap resized = new Bitmap(bitmap, new Size(480, 320));
                        // Assign a temporary variable to dispose the bitmap after assigning the new bitmap to the display control.
                        Bitmap bitmapOld = Camera2PictureBox.Image as Bitmap;
                        // Provide the display control with the new bitmap. This action automatically updates the display.
                        Camera2PictureBox.Image = bitmap;

                        //if (StartRecCam2)
                        //{
                         //   writer2.AddFrame(bitmap);
                        //}

                        //resized.Dispose();
                        if (bitmapOld != null)
                        {
                            // Dispose the bitmap.
                            bitmapOld.Dispose();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
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
                ShowException(exception);
            }
        }

        bool StartRecCam1;

        // Occurs when an image has been acquired and is ready to be processed.
        private void OnImageGrabbed1(Object sender, ImageGrabbedEventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
                // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread.
                BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed1), sender, e.Clone());
                return;
            }
            try
            {
                // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.

                // Get the grab result.
                IGrabResult grabResult = e.GrabResult;
                // Check if the image can be displayed.
                if (grabResult.IsValid)
                {
                    // Reduce the number of displayed images to a reasonable amount if the camera is acquiring images very fast.
                    if (!stopWatch.IsRunning || stopWatch.ElapsedMilliseconds > 33)
                    {
                        stopWatch.Restart();

                        Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format32bppRgb);
                        // Lock the bits of the bitmap.

                        BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
                        // Place the pointer to the buffer of the bitmap.

                        //Bitmap newImage = new Bitmap(480, 320, PixelFormat.Format24bppRgb);

                        //Bitmap resized = new Bitmap(bitmap, new Size(480, 320));
                        // Assign a temporary variable to dispose the bitmap after assigning the new bitmap to the display control.
                            Bitmap bitmapOld = Camera1PictureBox.Image as Bitmap;
                        // Provide the display control with the new bitmap. This action automatically updates the display.
                        Camera1PictureBox.Image = bitmap;

                        //if (StartRecCam1)
                        //{
                        //    writer1.AddFrame(bitmap);
                       // }

                        //resized.Dispose();
                        if (bitmapOld != null)
                        {
                            // Dispose the bitmap.
                            bitmapOld.Dispose();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                ShowException(exception);
            }
            finally
            {
                // Dispose the grab result if needed for returning it to the grab loop.
                e.DisposeGrabResultIfClone();
            }
        }

        // Occurs when a camera has stopped grabbing.
        private void OnGrabStopped2(Object sender, GrabStopEventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<GrabStopEventArgs>(OnGrabStopped2), sender, e);
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

        private void OnGrabStopped1(Object sender, GrabStopEventArgs e)
        {
            if (InvokeRequired)
            {
                // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
                BeginInvoke(new EventHandler<GrabStopEventArgs>(OnGrabStopped1), sender, e);
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

        private void Camera1_frm_FormClosing(object sender, FormClosingEventArgs e)
        {

            DestroyCamera(ConnectionData.Camera1);
            DestroyCamera(ConnectionData.Camera2);
            //   try
            //   {
            //       UpdateDataThread.Abort();//прерываем поток
            //   }
            //   catch
            //   {

            //    }
            //   DestroyCamera(Camera1);
            //   DestroyCamera(Camera2);
        }

        int ClickCountCam1 = 0;
        int OldPosXCam1 = 0;
        int OldPosYCam1 = 0;
        double DimCam1;

        // Измерение расстояния на плоскости


        private void Camera2PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            ClickCountCam2++;
            if (ClickCountCam2 == 1)
            {
                OldPosXCam2 = e.X;
                OldPosYCam2 = e.Y;
            }
            else if (ClickCountCam2 == 2)
            {
                DimCam2 = Math.Sqrt(Math.Pow(OldPosXCam2 - e.X, 2) + Math.Pow(OldPosYCam2 - e.Y, 2));
                DimensionCam2.Text = string.Format("{0:F2}", DimCam2 * ConnectionData.ScaleCam1) + Dict.LangStrings.mkm;
            }
            else
            {
                ClickCountCam2 = 0;
                DimensionCam2.Text = string.Format("{0:F2}", 0) + Dict.LangStrings.mkm;
            }
        }

        private void Camera1PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            ClickCountCam1++;
            if (ClickCountCam1 == 1)
            {
                OldPosXCam1 = e.X;
                OldPosYCam1 = e.Y;
            }
            else if (ClickCountCam1 == 2)
            {
                DimCam1 = Math.Sqrt(Math.Pow(OldPosXCam1 - e.X, 2) + Math.Pow(OldPosYCam1 - e.Y, 2));
                DimensionCam1.Text = string.Format("{0:F2}", DimCam1 * ConnectionData.ScaleCam1) + Dict.LangStrings.mkm;
            }
            else
            {
                ClickCountCam1 = 0;
                DimensionCam1.Text = string.Format("{0:F2}", 0) + Dict.LangStrings.mkm;
            }
        }

        private void MoveLeftCam1_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_12, -ConnectionData.SetCTVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void MoveLeftCam1_MouseUp(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_12);
            }
        }


        private void MoveRightCam1_MouseUp(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_12);
            }
        }

        private void MoveRightCam1_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_12, ConnectionData.SetCTVel);
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void MoveUpCam2_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_11);
        }

        private void MoveUpCam2_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    if ((ConnectionData.FeedBackCam2Y >= 0) && (ConnectionData.FeedBackCam2Y < ConnectionData.Camera2YStrokeMax))
                    {
                        ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_11, ConnectionData.SetCBVel);
                    }
                    else
                    {
                        ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_11);
                        ConnectionData.Value.SetRPosition(ConnectionData.Value.ACSC_AXIS_11, ConnectionData.Camera2YStrokeMax);
                    }
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void MoveDownCam2_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    if ((ConnectionData.FeedBackCam2Y > 0) && (ConnectionData.FeedBackCam2Y <= ConnectionData.Camera2YStrokeMax))
                    {
                        ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_11, -ConnectionData.SetCBVel);
                    }
                    else
                    {
                        ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_11);
                        ConnectionData.Value.SetRPosition(ConnectionData.Value.ACSC_AXIS_11, 0);
                    }
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void MoveDownCam2_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_11);
        }

        private void MoveLeftCam2_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    if ((ConnectionData.FeedBackCam2X > 0) && (ConnectionData.FeedBackCam2X <= ConnectionData.Camera2XStrokeMax))
                    {
                        ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_10, -ConnectionData.SetCBVel);
                    }
                    else
                    {
                        ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_10);
                        ConnectionData.Value.SetRPosition(ConnectionData.Value.ACSC_AXIS_10, 0);
                    }
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void MoveLeftCam2_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_10);
        }

        private void MoveRightCam2_MouseDown(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    if ((ConnectionData.FeedBackCam2X >= 0) && (ConnectionData.FeedBackCam2X < ConnectionData.Camera2XStrokeMax))
                    {
                        ConnectionData.Value.Jog(ConnectionData.Value.ACSC_AMF_VELOCITY, ConnectionData.Value.ACSC_AXIS_10, ConnectionData.SetCBVel);
                    }
                    else
                    {
                        ConnectionData.Value.SetRPosition(ConnectionData.Value.ACSC_AXIS_10, ConnectionData.Camera2XStrokeMax);
                        ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_10);
                    }
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void MoveRightCam2_MouseUp(object sender, MouseEventArgs e)
        {
            ConnectionData.Value.Kill(ConnectionData.Value.ACSC_AXIS_10);
        }

        private void Camera1_frm_Shown(object sender, EventArgs e)
        {
          //  UpdateDataThread = new Thread(new ThreadStart(UpdateData));
         //   UpdateDataThread.IsBackground = true;
          //  UpdateDataThread.Start();
        }

        private void UpdateData()
        {
            while (true)
            {
                Cam2XCoord.Text = "X: " + string.Format("{0:F2}", ConnectionData.FeedBackCam2X) + Dict.LangStrings.mm;
                Cam2YCoord.Text = "Y: " + string.Format("{0:F2}", ConnectionData.FeedBackCam2Y) + Dict.LangStrings.mm;

                Camera1XCoord.Text = "X: " + string.Format("{0:F2}", ConnectionData.FeedBackCam1X) + Dict.LangStrings.mm;
                ZoomLable.Text = Dict.LangStrings.Zoom + string.Format("{0:F0}", ConnectionData.Camera1ZoomStrokeMax/ConnectionData.FeedBackZoom) + "X";
            }
        }

        private void ZoomOutCam1_MouseClick(object sender, MouseEventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    if ((ConnectionData.FeedBackZoom >= ConnectionData.Camera1ZoomStrokeMax / 4) && (ConnectionData.FeedBackZoom < ConnectionData.Camera1ZoomStrokeMax))
                    {
                        ConnectionData.Value.ExtToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_13, ConnectionData.Camera1ZoomStrokeMax / 4, ConnectionData.SetCTVel, ConnectionData.SetCTVel);
                    }
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void Camera1_frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
               // UpdateDataThread.Abort();//прерываем поток
            }
            catch
            {

            }
            DestroyCamera(ConnectionData.Camera1);
            DestroyCamera(ConnectionData.Camera2);
        }

        private void ZoomInCam1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    if (ConnectionData.FeedBackZoom < ConnectionData.Camera1ZoomStrokeMax)
                    {
                        ConnectionData.Value.ExtToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_13, ConnectionData.Camera1ZoomStrokeMax / 3, ConnectionData.SetCTVel, ConnectionData.SetCTVel);
                    }
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        private void ZoomOutCam1_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    if (ConnectionData.FeedBackZoom >= ConnectionData.Camera1ZoomStrokeMax / 3)
                    {
                        ConnectionData.Value.ExtToPoint(ConnectionData.Value.ACSC_AMF_RELATIVE, ConnectionData.Value.ACSC_AXIS_13, -ConnectionData.Camera1ZoomStrokeMax / 3, ConnectionData.SetCTVel, ConnectionData.SetCTVel);
                    }
                }
                catch (COMException Ex)
                {
                    ErorMsg(Ex);
                }
            }
        }

        Bitmap bmpCam1;
        Bitmap bmpCam2;

        private void Camera1Timer_Tick(object sender, EventArgs e)
        {

        }

        private void Camera1SaveTimer_Tick(object sender, EventArgs e)
        {
            bmpCam1 = new Bitmap(Camera1PictureBox.Image);
            try
            {
                writer1.AddFrame(bmpCam1);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void Camera2SaveTimer_Tick(object sender, EventArgs e)
        {
            bmpCam2 = new Bitmap(Camera1PictureBox.Image);
            try
            {
                writer2.AddFrame(bmpCam1);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}
