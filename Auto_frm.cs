using Active_Directory_Worker.Interfaces;
using Basler.Pylon;
using MacGen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace BTP
{
    public partial class Auto_frm : Form, ILanguageChangable
    {

        //private string mCncFile;
        private Stopwatch stopWatch = new Stopwatch();
        //private PixelDataConverter converter = new PixelDataConverter();
        //Thread UpdateDataThread = null;
        private clsProcessor mProcessor = clsProcessor.Instance();
        private clsSettings mSetup = clsSettings.Instance();
        private GcodeViewer mViewerAuto;

        int OldExecLine = 0;
        double StartVelocityX = 0;
        double StartVelocityY = 0;
        double StartVelocityZ = 0;
        double StartVelocityU = 0;
        double StartVelocityV = 0;
        double StartVelocityW = 0;
        double StartVelocityA = 0;
        double StartVelocityS1 = 0;
        double StartVelocityS2 = 0;
        double StartVelocityS3 = 0;
        double StartVelocityS = 0;
        double StartVelocityPF1 = 0;
        double StartVelocityPF2 = 0;

        double StopVelocityX = 0;
        double StopVelocityY = 0;
        double StopVelocityS = 0;
        double StopVelocityPF = 0;

        string Operator;

        public Auto_frm()
        {
            CallBackMy3.callbackEventHandler = new CallBackMy3.callbackEvent(this.Reload);
            InitializeComponent();

            mViewerAuto = this.GCodeViewer;
            mProcessor.OnAddBlock += new clsProcessor.OnAddBlockEventHandler(mProcessor_OnAddBlock);

            //GcodeViewer.OnSelection += new GcodeViewer.OnSelectionEventHandler(mViewer_OnSelection);
            //GcodeViewer.MouseLocation += new GcodeViewer.MouseLocationEventHandler(mViewer_MouseLocation);
            //GcodeViewer.OnStatus += new GcodeViewer.OnStatusEventHandler(mViewer_OnStatus);

            mSetup.MachineActivated += new clsSettings.MachineActivatedEventHandler(mSetup_MachineActivated);
            mSetup.MachineAdded += new clsSettings.MachineAddedEventHandler(mSetup_MachineAdded);
            mSetup.MachineDeleted += new clsSettings.MachineDeletedEventHandler(mSetup_MachineDeleted);
            mSetup.MachineLoaded += new clsSettings.MachineLoadedEventHandler(mSetup_MachineLoaded);
            mSetup.MachineMatched += new clsSettings.MachineMatchedEventHandler(mSetup_MachineMatched);


            mSetup.LoadAllMachines(System.IO.Directory.GetCurrentDirectory() + "\\Data");
            mProcessor.Init(mSetup.Machine);
        }

        void Reload(string param)
        {
            try
            {
                #region labels
                PosX.Text = string.Format("{0:F2}", ConnectionData.FeedBackX);
                PosY.Text = string.Format("{0:F2}", ConnectionData.FeedBackY);
                PosS1.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ1);
                PosS2.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ2);
                PosS3.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ3);
                PosSp.Text = string.Format("{0:F2}", ConnectionData.FeedBackZSp);

                StartVelocityX = ConnectionData.Value.ReadVariable("STARTVELLIN", ConnectionData.Value.ACSC_NONE);
                StartVelocityY = StartVelocityX;
                StartVelocityZ = StartVelocityX;
                StartVelocityU = StartVelocityX;
                StartVelocityV = StartVelocityX;
                StartVelocityW = StartVelocityX;
                StartVelocityA = StartVelocityX;
                StartVelocityS1 = ConnectionData.Value.ReadVariable("STARTVELOUT1", ConnectionData.Value.ACSC_NONE);
                StartVelocityS2 = StartVelocityS1;
                StartVelocityS3 = StartVelocityS1;
                StartVelocityPF1 = StartVelocityS1;
                StartVelocityPF2 = ConnectionData.Value.ReadVariable("STARTVELOUT2", ConnectionData.Value.ACSC_NONE);

                ActualVelocityX.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_0));
                ActualVelocityY.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_1));
                ActualVelocityZ.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_2));
                ActualVelocityU.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_3));
                ActualVelocityV.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_4));
                ActualVelocityW.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_5));
                ActualVelocityA.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_6));
                ActualVelocityS1.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_7));
                ActualVelocityS2.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_8));
                ActualVelocityS3.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_9));
                ActualVelocityPF1.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_14));
                ActualVelocityPF2.Text = string.Format("{0:F2}", ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_15));
                #endregion
                if (ConnectionData.ProgramStart == 10)
                {
                        groupBox2.Enabled = true;
                        if (ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_0) != 0)
                        {
                            StopVelocityX = ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_0);
                        }
                        if (ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_1) != 0)
                        {
                            StopVelocityY = ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_1);
                        }
                        if (ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_7) != 0)
                        {
                            StopVelocityS = ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_7);
                        }
                        if (ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_14) != 0)
                        {
                            StopVelocityPF = ConnectionData.Value.GetFVelocity(ConnectionData.Value.ACSC_AXIS_14);
                        }
                  }
                else if (ConnectionData.ProgramStart == 20)
                {
                    groupBox2.Enabled = false;
                }

                if (Convert.ToBoolean(ConnectionData.Value.GetProgramState(ConnectionData.Value.ACSC_BUFFER_2) & ConnectionData.Value.ACSC_PST_RUN) == false)
                {
                    if (ConnectionData.ProgramStart == 10)
                  {
                        ConnectionData.ProgramStart = 20;
                  }
                }

             if ((ConnectionData.BufferSize != 0))
                {
                    if (ConnectionData.first_bit == false)
                    {
                        try
                        {
                            GCodeBox.Items.Clear();
                            list.Clear();

                            string[] words = ConnectionData.Value.UploadBuffer(ConnectionData.Value.ACSC_BUFFER_2).Split(new char[] { (char)10 });

                            foreach (string s in words)
                            {
                                GCodeBox.Items.Add(s);
                                list.Add(s);
                            }

                            //string TempFile = Application.StartupPath + "\\Temp\\CNC.tmp";
                            //System.IO.File.WriteAllText(TempFile, ConnectionData.Value.UploadBuffer(ConnectionData.Value.ACSC_BUFFER_2));

                            if (Properties.Settings.Default.Virgin == true)
                            {
                                this.StartPosition = FormStartPosition.CenterScreen;
                            }
                            else
                            {
                                this.Location = Properties.Settings.Default.ViewFormLocation;
                            }
                            OpenFile(list);
                            SetDefaultViews();
                            var point = new Point(14, 12);
                            this.Location = point;
                            Properties.Settings.Default.Virgin = false;
                        }
                        catch
                        {
                            MessageBox.Show("Wrong G-code");
                        }

                        ConnectionData.first_bit = true;

                    }

                    if (GCodeBox.Items.Count > 0)
                    {
                        GCodeBox.SelectedIndex = Convert.ToInt16(ConnectionData.ExecLine - 1);
                        if (ConnectionData.ExecLine != OldExecLine)
                        {
                            mViewerAuto.BreakPoint = Convert.ToInt16(ConnectionData.ExecLine - 1);
                            mViewerAuto.Redraw(true);
                            OldExecLine = ConnectionData.ExecLine;                            
                        }

                        var point = new Point(14, 12);
                        this.Location = point;
                    }
                }
                else
                {
                    ConnectionData.first_bit = false;
                    //GCodeBox.Items.Clear();
                }
            }
            catch
            {

            }
            }

        public void ChangeFormLanguage(AvaliableLocalizations newLocalization)
        {
            Sett sett1 = new Sett();
            sett1.SetCulture(newLocalization);

            var resources = new ComponentResourceManager(typeof(Auto_frm));
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

        private void mSetup_MachineActivated(clsMachine m)
        {
            {
                GCodeViewer.RotaryDirection = (RotaryDirection)m.RotaryDir;
                GCodeViewer.RotaryPlane = (Axis)m.RotaryAxis;
                GCodeViewer.RotaryType = (RotaryMotionType)m.RotaryType;
                GCodeViewer.ViewManipMode = GcodeViewer.ManipMode.SELECTION;

                //MG_Viewer1.FindExtents();
                //MG_Viewer2.FindExtents();
                //MG_Viewer3.FindExtents();
                //MG_Viewer4.FindExtents();
                //mViewer.Redraw(true);

            }
        }

      private void mSetup_MachineAdded(clsMachine m)
     {
            //tscboMachines.Items.Add(m.Name);
     }

        private void mSetup_MachineDeleted(string name)
            {
            //tscboMachines.Items.RemoveAt(tscboMachines.FindStringExact(name));
           }

        private void mSetup_MachineLoaded(clsMachine m)
        {
            //tscboMachines.Items.Add(m.Name);
        }

      //  private void mViewer_MouseLocation(float x, float y)
      //  {
            //Coordinates.Text = "X=" + x.ToString("0.000") + " Y=" + y.ToString("0.000");
      //  }

         private void mSetup_MachineMatched(clsMachine m)
         {
            //tscboMachines.SelectedIndex = tscboMachines.FindStringExact(m.Name);
         }

        private void mProcessor_OnAddBlock(int idx, int ct)
        {
            try
            {
                //this.Progress.Maximum = ct;
                //this.Progress.Value = idx;
                if (ct > 10000)
                {
                    //Refresh every 1000 blocks 
                    if (1000 % idx == 0)
                    {
                        mViewerAuto.FindExtents();
                        mViewerAuto.Redraw(true);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }


//        private void mViewer_OnSelection(System.Collections.Generic.List<clsMotionRecord> hits)
//        {
//            //lblStatus.Text = hits[0].Codestring;
//            string[] tipString = new string[hits.Count];
//            for (int r = 0; r <= hits.Count - 1; r++)
//            {
//                tipString[r] = hits[r].Codestring;
//            }
//            //this.CodeTip.SetToolTip(mViewer, string.Join(Environment.NewLine, tipString));
//        }

//        private void mViewer_OnStatus(string msg, int index, int max)
//        {
            //lblStatus.Text = msg;
            //Progress.Maximum = max;
            //Progress.Value = index;
            //StatusStrip1.Refresh();
//        }

        private void Auto_frm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            SetDefaultViews();
            CurVelLabel.Text = VelocityBar.Value + " %";
        }

        private void OpenFile(List<string> list)
        {
            long[] ticks = new long[2];
            //mCncFile = fileName;
            //mSetup.MatchMachineToFile(mCncFile);

            ProcessFile(list);
            mViewerAuto.BreakPoint = GcodeViewer.MotionBlocks.Count - 1;

            mViewerAuto.Pitch = mSetup.Machine.ViewAngles[0];
            mViewerAuto.Roll = mSetup.Machine.ViewAngles[1];
            mViewerAuto.Yaw = mSetup.Machine.ViewAngles[2];
            mViewerAuto.Init();

            ticks[0] = DateTime.Now.Ticks;
            GCodeViewer.FindExtents();
            ticks[1] = DateTime.Now.Ticks;
            GCodeViewer.DynamicViewManipulation = (ticks[1] - ticks[0]) < 2000000;
            //MG_Viewer2.FindExtents(); 
            //MG_Viewer3.FindExtents(); 
            //MG_Viewer4.FindExtents(); 
            mViewerAuto.Redraw(true);
        }

        // Подключение к камерам

                // Обработка ошибок
        private static void ShowException(Exception exception)
        {
            MessageBox.Show("Exception caught:\n" + exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Occurs when a device with an opened connection is removed.


       // Обновление данных на экране 
        void UpdateData()
        {
            while (true)
            {
                try
                {
                    PosX.Text = string.Format("{0:F2}", ConnectionData.FeedBackX);
                    PosY.Text = string.Format("{0:F2}", ConnectionData.FeedBackY);
                    PosS1.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ1);
                    PosS2.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ2);
                    PosS3.Text = string.Format("{0:F2}", ConnectionData.FeedBackZ3);
                    PosSp.Text = string.Format("{0:F2}", ConnectionData.FeedBackZSp);


                    if (ConnectionData.BufferSize != 0)
                    {
                        if (ConnectionData.first_bit == false)
                        {
                            GCodeBox.Items.Clear();
                            list.Clear();

                            string[] words = ConnectionData.Value.UploadBuffer(ConnectionData.Value.ACSC_BUFFER_2).Split(new char[] { (char)10 });

                            foreach (string s in words)
                                {
                                    GCodeBox.Items.Add(s);
                                    list.Add(s);
                                }

                            string TempFile = Application.StartupPath + "\\Temp\\CNC.tmp";

                            System.IO.File.WriteAllText(TempFile, ConnectionData.Value.UploadBuffer(ConnectionData.Value.ACSC_BUFFER_2));

                            if (Properties.Settings.Default.Virgin == true)
                                {
                                    this.StartPosition = FormStartPosition.CenterScreen;
                                }
                            else
                                {
                                    this.Location = Properties.Settings.Default.ViewFormLocation;
                                }
                            OpenFile(list);
                            SetDefaultViews();
                            Properties.Settings.Default.Virgin = false;
                            ConnectionData.first_bit = true;
                        }

                        if (GCodeBox.Items.Count > 0)
                        {
                            GCodeBox.SelectedIndex = Convert.ToInt16(ConnectionData.ExecLine - 1);
                                if (ConnectionData.ExecLine != OldExecLine)
                                {
                                    mViewerAuto.BreakPoint = Convert.ToInt16(ConnectionData.ExecLine - 1);
                                    mViewerAuto.Redraw(true);
                                    OldExecLine = ConnectionData.ExecLine;
                                }

                            var point = new Point(14, 12);
                            this.Location = point;
                        }
                    }
                    else
                    {
                        ConnectionData.first_bit = false;
                        GCodeBox.Items.Clear();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                Thread.Sleep(100);
            }
        }

        private void SetDefaultViews()
        {
            //Case "Top" 
            //MG_Viewer1.Pitch = 0f; 
            //MG_Viewer1.Roll = 0f; 
            //MG_Viewer1.Yaw = 0f; 
            //MG_Viewer1.FindExtents(); 
            //Case "Front" 
            //MG_Viewer3.Pitch = 270f; 
            //MG_Viewer3.Roll = 0f; 
            //MG_Viewer3.Yaw = 360f; 
            //MG_Viewer3.FindExtents(); 

            //Case "Right" 
            //MG_Viewer4.Pitch = 270f; 
            //MG_Viewer4.Roll = 0f; 
            //MG_Viewer4.Yaw = 270f; 
            //MG_Viewer4.FindExtents(); 

            //Case "ISO" 
            GCodeViewer.Pitch = 315f;
            GCodeViewer.Roll = 0f;
            GCodeViewer.Yaw = 315f;
            GCodeViewer.FindExtents();
            mViewerAuto.Redraw(true);
        }

        List<string> list = new List<string>();

        // Обработка G-кода
        private void ProcessFile(List<string> list)
        {
            //if (fileName == null)
            //{
            //    return;
            //}
            //if (!System.IO.File.Exists(fileName))
            //{
            // lblStatus.Text = "File does not exist!";
            //    return;
            //}
            // lblStatus.Text = "Processing...";
            GcodeViewer.MotionBlocks.Clear();
            mProcessor.Init(mSetup.Machine);
            mProcessor.ProcessFile(GcodeViewer.MotionBlocks, list);

            // BreakPointSlider.Maximum = GcodeViewer.MotionBlocks.Count - 1;
            if (mViewerAuto.BreakPoint > GcodeViewer.MotionBlocks.Count - 1)
            {
                mViewerAuto.BreakPoint = GcodeViewer.MotionBlocks.Count - 1;
            }
            mViewerAuto.GatherTools();

            // lblStatus.Text = "Done";
            // Progress.Value = 0;

        }

        private void Auto_frm_Shown(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                //UpdateDataThread = new Thread(new ThreadStart(UpdateData));
                //UpdateDataThread.IsBackground = true;
                //UpdateDataThread.Start();
            }
            CurVelLabel.Text = VelocityBar.Value + " %";
        }


        private void Auto_frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
           //     UpdateDataThread.Abort();//прерываем поток
            }
            catch
            {

            }
        }

        private void ViewButtonClicked(object sender, EventArgs e)
        {
            string tag = sender.GetType().GetProperty("Tag").GetValue(sender, null).ToString();
            switch (tag)
            {
                case "Fit":
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        GCodeViewer.FindExtents();
                    }
                    else
                    {
                        mViewerAuto.FindExtents();
                    }

                    break;
                case "Pan":
                    mViewerAuto.ViewManipMode = GcodeViewer.ManipMode.PAN;
                    tsbPan.Checked = true;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Fence":
                    mViewerAuto.ViewManipMode = GcodeViewer.ManipMode.FENCE;
                    tsbFence.Checked = true;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Zoom":
                    mViewerAuto.ViewManipMode = GcodeViewer.ManipMode.ZOOM;
                    tsbZoom.Checked = true;
                    tsbRotate.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Rotate":
                    mViewerAuto.ViewManipMode = GcodeViewer.ManipMode.ROTATE;
                    tsbRotate.Checked = true;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Select":
                    mViewerAuto.ViewManipMode = GcodeViewer.ManipMode.SELECTION;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = true;
                    break;
                case "2DView":
                    mViewerAuto.Pitch = 0;
                    mViewerAuto.Roll = 0;
                    mViewerAuto.Yaw = 0;
                    mViewerAuto.FindExtents();
                    mViewerAuto.Redraw(true);
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = true;
                    break;
                case "3DView":
                    mViewerAuto.Pitch = 315;
                    mViewerAuto.Roll = 0;
                    mViewerAuto.Yaw = 315;
                    mViewerAuto.FindExtents();
                    mViewerAuto.Redraw(true);
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = true;
                    break;
            }

        }

        double Velocity = 1;
        int oldScrollValue = 1;

        private void VelocityBar_Scroll(object sender, EventArgs e)
        {
            
            Velocity = (double)VelocityBar.Value / 100.0;

            if ((Velocity * StartVelocityS1 < ConnectionData.MaxPtVel) && (Velocity * StartVelocityS2 < ConnectionData.MaxPtVel) && (Velocity * StartVelocityS3 < ConnectionData.MaxPtVel))
            {
                CurVelLabel.Text = VelocityBar.Value + " %";

                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_0, Velocity * StartVelocityX);
                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_1, Velocity * StartVelocityY);
                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_2, Velocity * StartVelocityZ);
                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_3, Velocity * StartVelocityU);
                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_4, Velocity * StartVelocityV);
                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_5, Velocity * StartVelocityW);
                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_6, Velocity * StartVelocityA);
                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_7, Velocity * StartVelocityS1);
                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_8, Velocity * StartVelocityS2);
                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_9, Velocity * StartVelocityS3);
                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_14, Velocity * StartVelocityPF1);
                ConnectionData.Value.SetVelocityImm(ConnectionData.Value.ACSC_AXIS_15, Velocity * StartVelocityPF2);

                ConnectionData.Value.WriteVariable(Velocity, "REDUCER", ConnectionData.Value.ACSC_NONE);

                oldScrollValue = VelocityBar.Value;
            } 
            else
            {
                VelocityBar.Value = oldScrollValue;
            }
        }

        private void Auto_frm_Activated(object sender, EventArgs e)
        {

        }

        /*private Excel.Application ObjExcel = new Excel.Application();
        private Excel.Workbook ObjWorkBook;
        private Excel.Worksheet ObjWorkSheet;

        public void LoadFile(string path, int sheetNumber)
        {
            ObjWorkBook = ObjExcel.Workbooks.Open(path, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[sheetNumber];
        }

        public void SetCell(string cell, string value)
        {
            Excel.Range range = ObjWorkSheet.get_Range(cell);
            range.Value = value;
        }

        public void SaveFile(string fn)
        {
            //ObjWorkBook.SaveAs(fn);
            ObjWorkBook.Close(true, fn, false);
        }

        public void CloseFile()
        {
            ObjExcel.Quit();
            Marshal.ReleaseComObject(ObjWorkSheet);
            Marshal.ReleaseComObject(ObjWorkBook);
            Marshal.ReleaseComObject(ObjExcel);
        }

        public void SearchInfo(string str)
        {

        }

        private void PrintReportBtn_Click(object sender, EventArgs e)
        {
            string extrusion_dist_1 = "";
            string extrusion_dist_2 = "";
            string extrusion_dist_3 = "";
            string extrusion_dist_5 = "";

            string extrusion_dist_layer_1 = "";
            string extrusion_dist_layer_2 = "";
            string extrusion_dist_layer_3 = "";
            string extrusion_dist_layer_5 = "";

            string sferoid_dist_1 = "";
            string sferoid_dist_2 = "";
            string sferoid_dist_3 = "";
            string sferoid_dist_5 = "";

            string sferoid_dist_layer_1 = "";
            string sferoid_dist_layer_2 = "";
            string sferoid_dist_layer_3 = "";
            string sferoid_dist_layer_5 = "";

            string material_sferoid_1 = "";
            string material_sferoid_2 = "";
            string material_sferoid_3 = "";

            string material_hydrogel_1 = "";
            string material_hydrogel_2 = "";
            string material_hydrogel_3 = "";

            string preeflow_proportion = "";

            string Exfile =     Application.StartupPath + "\\Data\\Report.dll";
            string Tempfile =   Application.StartupPath + "\\Temp\\Report.xlsx";
            string CNCfile =    Application.StartupPath + "\\Temp\\CNC.tmp";
            //Workbook book = Workbook.Load(Exfile);
            //Worksheet sheet = book.Worksheets[0];
            char[] charsToTrim = { '(', ')', '1', '2', '3', '5' };


            try
            {
                string[] lines = System.IO.File.ReadAllLines(@CNCfile);

                foreach (string line in lines)
                {
                    string[] split = line.Split(new Char[] { ' ', ',', '-' });
                    if (line.Contains("extrusion_dist-"))
                        {
                            for (int i = 0; i < split.Length; i++)
                            {
                                if (split[i].Trim().EndsWith("(1)"))
                                {
                                    extrusion_dist_1 = split[i].TrimEnd(charsToTrim); 
                                }
                                else if (split[i].Trim().EndsWith("(2)"))
                                {
                                    extrusion_dist_2 = split[i].TrimEnd(charsToTrim);
                            }
                                else if (split[i].Trim().EndsWith("(3)"))
                                {
                                    extrusion_dist_3 = split[i].TrimEnd(charsToTrim);
                            }
                                else if (split[i].Trim().EndsWith("(5)"))
                                {
                                    extrusion_dist_5 = split[i].TrimEnd(charsToTrim);
                            }
                            }
                        }

                    else if (line.Contains("extrusion_dist_layer"))
                    {
                        for (int i = 0; i < split.Length; i++)
                        {
                            if (split[i].Trim().EndsWith("(1)"))
                            {
                                extrusion_dist_layer_1 = split[i].TrimEnd(charsToTrim);
                            }
                            else if (split[i].Trim().EndsWith("(2)"))
                            {
                                extrusion_dist_layer_2 = split[i].TrimEnd(charsToTrim);
                            }
                            else if (split[i].Trim().EndsWith("(3)"))
                            {
                                extrusion_dist_layer_3 = split[i].TrimEnd(charsToTrim);
                            }
                            else if (split[i].Trim().EndsWith("(5)"))
                            {
                                extrusion_dist_layer_5 = split[i].TrimEnd(charsToTrim);
                            }
                        }
                    }

                    else if (line.Contains("sferoid_dist-"))
                    {
                        for (int i = 0; i < split.Length; i++)
                        {
                            if (split[i].Trim().EndsWith("(1)"))
                            {
                                sferoid_dist_1 = split[i].TrimEnd(charsToTrim);
                            }
                            else if (split[i].Trim().EndsWith("(2)"))
                            {
                                sferoid_dist_2 = split[i].TrimEnd(charsToTrim);
                            }
                            else if (split[i].Trim().EndsWith("(3)"))
                            {
                                sferoid_dist_3 = split[i].TrimEnd(charsToTrim);
                            }
                        }
                    }

                    else if (line.Contains("sferoid_dist_layer"))
                    {
                        for (int i = 0; i < split.Length; i++)
                        {
                            if (split[i].Trim().EndsWith("(1)"))
                            {
                                sferoid_dist_layer_1 = split[i].TrimEnd(charsToTrim);
                            }
                            else if (split[i].Trim().EndsWith("(2)"))
                            {
                                sferoid_dist_layer_2 = split[i].TrimEnd(charsToTrim);
                            }
                            else if (split[i].Trim().EndsWith("(3)"))
                            {
                                sferoid_dist_layer_3 = split[i].TrimEnd(charsToTrim);
                            }
                        }
                    }

                    else if (line.Contains("material_sferoid"))
                    {
                        for (int i = 0; i < split.Length; i++)
                        {
                            if (split[i].Trim().EndsWith("(1)"))
                            {
                                material_sferoid_1 = split[i].TrimEnd(charsToTrim);
                            }
                            else if (split[i].Trim().EndsWith("(2)"))
                            {
                                material_sferoid_2 = split[i].TrimEnd(charsToTrim);
                            }
                            else if (split[i].Trim().EndsWith("(3)"))
                            {
                                material_sferoid_3 = split[i].TrimEnd(charsToTrim);
                            }
                        }
                    }

                    else if (line.Contains("material_hydrogel"))
                    {
                        for (int i = 0; i < split.Length; i++)
                        {
                            if (split[i].Trim().EndsWith("(1)"))
                            {
                                material_hydrogel_1 = split[i].TrimEnd(charsToTrim);
                            }
                            else if (split[i].Trim().EndsWith("(2)"))
                            {
                                material_hydrogel_2 = split[i].TrimEnd(charsToTrim);
                            }
                            else if (split[i].Trim().EndsWith("(3)"))
                            {
                                material_hydrogel_3 = split[i].TrimEnd(charsToTrim);
                            }
                        }
                    }

                    else if (line.Contains("preeflow_proportion"))
                    {
                        for (int i = 0; i < split.Length; i++)
                        {
                            preeflow_proportion = split[i];
                        }
                    }
                }
            }
            catch
            {

            }
            File.Copy(Exfile, Tempfile, true);

            SaveReport.FileName = "Report_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");

            if (SaveReport.ShowDialog() != DialogResult.Cancel)
            {

                LoadFile(Tempfile, 1);

                // Data
                //Date
                SetCell("C3", ConnectionData.DateStr);
                // Time
                SetCell("C4", ConnectionData.TimeStr);
                // Operator
                SetCell("C5", Operator);


                // Extrusion
                // Distance between filaments
                SetCell("C8", extrusion_dist_1);
                SetCell("D8", extrusion_dist_2);
                SetCell("E8", extrusion_dist_3);
                SetCell("F8", extrusion_dist_5);

                // Distance between layers
                SetCell("C9", extrusion_dist_layer_1);
                SetCell("D9", extrusion_dist_layer_2);
                SetCell("E9", extrusion_dist_layer_3);
                SetCell("F9", extrusion_dist_layer_5);

                // Material
                SetCell("C10", material_hydrogel_1);
                SetCell("D10", material_hydrogel_2);
                SetCell("E10", material_hydrogel_3);

                // Spheroid
                // Distance between filaments
                SetCell("C12", sferoid_dist_1);
                SetCell("D12", sferoid_dist_2);
                SetCell("E12", sferoid_dist_3);
                SetCell("F12", sferoid_dist_5);

                // Distance between layers
                SetCell("C13", sferoid_dist_layer_1);
                SetCell("D13", sferoid_dist_layer_2);
                SetCell("E13", sferoid_dist_layer_3);
                SetCell("F13", sferoid_dist_layer_5);

                // Material
                SetCell("C14", material_sferoid_1);
                SetCell("D14", material_sferoid_2);
                SetCell("E14", material_sferoid_3);

                // Speed
                SetCell("C16", string.Format("{0:F2}", StartVelocityX));
                SetCell("C17", string.Format("{0:F2}", StartVelocityY));
                SetCell("C18", string.Format("{0:F2}", StartVelocityS));
                SetCell("C19", string.Format("{0:F2}", StartVelocityPF1));

                SetCell("D16", string.Format("{0:F0} %", VelocityBar.Value));

                SetCell("E16", string.Format("{0:F2}", StopVelocityX));
                SetCell("E17", string.Format("{0:F2}", StopVelocityY));
                SetCell("E18", string.Format("{0:F2}", StopVelocityS));
                SetCell("E19", string.Format("{0:F2}", StopVelocityPF));


                // Preflow Propotion
                SetCell("F20", preeflow_proportion);

                //sheet.Cells[0, 0] = new Cell(1); //Cell(ConnectionData.DateStr);
                //book.Save(TempFile);



                string filename = SaveReport.FileName;
                //File.WriteAllText(SaveReport.FileName, Exfile);
                //File.Copy(Exfile, filename, true);

                SaveFile(filename);

                //xlWorkBook.SaveAs(filename);
                //xlWorkBook.Close(true, misValue, misValue);
                //xlApp.Quit();



                //Marshal.ReleaseComObject(xlWorkSheet);
                //Marshal.ReleaseComObject(xlWorkBook);
                //Marshal.ReleaseComObject(xlApp);
                CloseFile();
            }
            else
            {
                return;
            }

        }

        private void OperatorTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Operator = OperatorTB.Text;
            }
        }*/
    }
}