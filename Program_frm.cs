using Active_Directory_Worker.Interfaces;

using MacGen;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BTP
{
    public partial class Program_frm : Form, ILanguageChangable
    {
        //private string mCncFile;
        private clsProcessor mProcessor = clsProcessor.Instance();
        private clsSettings mSetup = clsSettings.Instance();
        private GcodeViewer mViewer;


        public Program_frm()
        {
            InitializeComponent();
            mViewer = this.GCodeViewerProgram;
            mProcessor.OnAddBlock += new clsProcessor.OnAddBlockEventHandler(mProcessor_OnAddBlock);

            mViewer.OnSelection += new GcodeViewer.OnSelectionEventHandler(mViewer_OnSelection);
            // GcodeViewer.MouseLocation += new GcodeViewer.MouseLocationEventHandler(mViewer_MouseLocation);
            // GcodeViewer.OnStatus += new GcodeViewer.OnStatusEventHandler(mViewer_OnStatus);
            
            mSetup.MachineActivated += new clsSettings.MachineActivatedEventHandler(mSetup_MachineActivated);
           // mViewer.DrawAxisIndicator = false;
           // mViewer.DrawAxisLines = false;

            //mSetup.MachineAdded += new clsSettings.MachineAddedEventHandler(mSetup_MachineAdded);
            //mSetup.MachineDeleted += new clsSettings.MachineDeletedEventHandler(mSetup_MachineDeleted);
            //mSetup.MachineLoaded += new clsSettings.MachineLoadedEventHandler(mSetup_MachineLoaded);
            //mSetup.MachineMatched += new clsSettings.MachineMatchedEventHandler(mSetup_MachineMatched);

            //mSetup.LoadAllMachines(System.IO.Directory.GetCurrentDirectory() + "\\Data");
            mProcessor.Init(mSetup.Machine);
            mViewer.Redraw(true);
        }

        private void ViewButtonClicked(object sender, EventArgs e)
        {
            string tag = sender.GetType().GetProperty("Tag").GetValue(sender, null).ToString();
            switch (tag)
            {
                case "Fit":
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        GCodeViewerProgram.FindExtents();
                    }
                    else
                    {
                        mViewer.FindExtents();
                    }

                    break;
                case "Pan":
                    mViewer.ViewManipMode = GcodeViewer.ManipMode.PAN;
                    
                    tsbPan.Checked = true;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Fence":
                    mViewer.ViewManipMode = GcodeViewer.ManipMode.FENCE;
                    tsbFence.Checked = true;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Zoom":
                    mViewer.ViewManipMode = GcodeViewer.ManipMode.ZOOM;
                    tsbZoom.Checked = true;
                    tsbRotate.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Rotate":
                    mViewer.ViewManipMode = GcodeViewer.ManipMode.ROTATE;
                    tsbRotate.Checked = true;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Select":
                    mViewer.ViewManipMode = GcodeViewer.ManipMode.SELECTION;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = true;
                    break;
                case "2DView":
                    mViewer.Pitch = 0;
                    mViewer.Roll = 0;
                    mViewer.Yaw = 0;
                    mViewer.FindExtents();
                    mViewer.Redraw(true);
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = true;
                    break;
                case "3DView":
                    mViewer.Pitch = 315;
                    mViewer.Roll = 0;
                    mViewer.Yaw = 315;
                    mViewer.FindExtents();
                    mViewer.Redraw(true);
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = true;
                    break;
            }
        }

        private void mSetup_MachineActivated(clsMachine m)
        {
            {
                GCodeViewerProgram.RotaryDirection = (RotaryDirection)m.RotaryDir;
                GCodeViewerProgram.RotaryPlane = (Axis)m.RotaryAxis;
                GCodeViewerProgram.RotaryType = (RotaryMotionType)m.RotaryType;
                GCodeViewerProgram.ViewManipMode = GcodeViewer.ManipMode.SELECTION;
            }
        }

        private void mViewer_OnSelection(System.Collections.Generic.List<clsMotionRecord> hits)
        {
            //int index = 0;
            Coordinates.Text = hits[0].Codestring;
            //GCodeInputBox.Select(0, GCodeInputBox.TextLength);
            //GCodeInputBox.SelectionBackColor = Color.White;
            string[] tipString = new string[hits.Count];
            for (int r = 0; r <= hits.Count - 1; r++)
            {
                tipString[r] = hits[r].Codestring;
            }
            this.PicGCode.SetToolTip(mViewer, string.Join(Environment.NewLine, tipString));
            //while (index < GCodeInputBox.Text.IndexOf(hits[0].Codestring))
            //{
            //    GCodeInputBox.Find(hits[0].Codestring, index, GCodeInputBox.TextLength, RichTextBoxFinds.None);
            //    GCodeInputBox.SelectionBackColor = Color.Orange;
            //    index = GCodeInputBox.Text.IndexOf(hits[0].Codestring, index);
            //}

            //GCodeInputBox.Select(10, 20);
        }

        private void mProcessor_OnAddBlock(int idx, int ct)
        {
            try
            {
                //this.Progress.Maximum = ct;
                //this.Progress.Value = idx;
                if (ct > 100000)
                {
                    //Refresh every 1000 blocks 
                    if (10000 % idx == 0)
                    {
                        mViewer.FindExtents();
                        mViewer.Redraw(true);
                    }
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

            var resources = new ComponentResourceManager(typeof(Program_frm));
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

        private void SetDefaultViews()
        {
            //Case "ISO" 
            GCodeViewerProgram.Pitch = 315f;
            GCodeViewerProgram.Roll = 0f;
            GCodeViewerProgram.Yaw = 315f;
            GCodeViewerProgram.FindExtents();
            mViewer.Redraw(true);
        }

        private void Program_frm_Load(object sender, EventArgs e)
        {
            SetDefaultViews();
        }

        private void NewBtn_Click(object sender, EventArgs e)
        {
            GCodeInputBox.Clear();
        }

   /*     private void DrawLayers()
        {
            TreeNode nd = default(TreeNode);
            treeView1.Nodes.Clear();
            foreach (clsToolLayer tl in GcodeViewer.ToolLayers.Values)
            {
                nd = treeView1.Nodes.Add("Layer " + tl.Number.ToString());
                nd.Checked = !tl.Hidden;
                nd.Tag = tl;
            }
        }
*/
        private void OpenBtn_Click(object sender, EventArgs e)
        {
            if (OpenGCode.ShowDialog() == DialogResult.OK)
            {
                list_pr.Clear();
                GCodeInputBox.Text = File.ReadAllText(OpenGCode.FileName);


                //list.Clear();
                list_pr.AddRange(GCodeInputBox.Lines);
                if (Properties.Settings.Default.Virgin == true)
                {
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                    this.Location = Properties.Settings.Default.ViewFormLocation;
                }
                OpenFileProgram(list_pr);
                SetDefaultViews();
                var point = new Point(14, 12);
                this.Location = point;
                Properties.Settings.Default.Virgin = false;
                G_code_name.Text = OpenGCode.FileName;
                //LayerSelector.Maximum = ConnectionData.MaxLayers;
                //DrawLayers();
            }
            else
            {
                return;
            }
            //MessageBox.Show("Выбран файл: " + OpenGCode.FileName);
        }

        private void OpenFileProgram(List<string> list)
        { 
            long[] ticks = new long[2];
            //mCncFile = fileName;
            //mSetup.MatchMachineToFile(mCncFile);

            ProcessFileProgram(list);
            mViewer.BreakPoint = GcodeViewer.MotionBlocks.Count - 1;

            mViewer.Pitch = mSetup.Machine.ViewAngles[0];
            mViewer.Roll = mSetup.Machine.ViewAngles[1];
            mViewer.Yaw = mSetup.Machine.ViewAngles[2];
            mViewer.Init();

            ticks[0] = DateTime.Now.Ticks;
            GCodeViewerProgram.FindExtents();
            ticks[1] = DateTime.Now.Ticks;
            GCodeViewerProgram.DynamicViewManipulation = (ticks[1] - ticks[0]) < 2000000;
            //MG_Viewer2.FindExtents(); 
            //MG_Viewer3.FindExtents(); 
            //MG_Viewer4.FindExtents(); 
        }

        List<string> list_pr = new List<string>();
        //string str;

        private void ProcessFileProgram(List<string> list)
        {
            // if (fileName == null)
            // {
            //     return;
            // }
            //if (!System.IO.File.Exists(fileName))
            // {
            // lblStatus.Text = "File does not exist!";
            //     return;
            // }
            // lblStatus.Text = "Processing...";

            list = new List<string>( filtrCode(list.ToArray()));
            GcodeViewer.MotionBlocks.Clear();

            //StreamReader sr = new System.IO.StreamReader(fileName);

            //while ((str = sr.ReadLine()) != null)
            //{
            //    
            //    list.Add(str);
            //}

            mProcessor.Init(mSetup.Machine);
            mProcessor.ProcessFile(GcodeViewer.MotionBlocks, list);

            // BreakPointSlider.Maximum = GcodeViewer.MotionBlocks.Count - 1;
            if (mViewer.BreakPoint > GcodeViewer.MotionBlocks.Count - 1)
            {
                mViewer.BreakPoint = GcodeViewer.MotionBlocks.Count - 1;
            }
            mViewer.GatherTools();
            // lblStatus.Text = "Done";
            // Progress.Value = 0;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (SaveGCode.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл

            string filename = SaveGCode.FileName;
            //label1.Text = filename;
            // сохраняем текст в файл
            File.WriteAllText(SaveGCode.FileName, GCodeInputBox.Text);
            //GCodeInputBox.SaveFile(@filename);
            // MessageBox.Show("Файл сохранен");
        }

        private void UploadBtn_Click(object sender, EventArgs e)
        {
            if (ConnectionData.bConnected)
            {
                try
                {
                    var code = GCodeInputBox.Text;
                    var progr = code.Split('\n');
                    var progr_f = filtrCode( code.Split('\n'));
                    var text_d = "";
                    foreach (var line in progr_f) text_d += line+"\r\n";
                    
                    GCodeInputBox.Text = text_d;
                    ConnectionData.Value.upload_program(progr_f);
                    Console.WriteLine("upload done");
                    MessageBox.Show("upload done");

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        string[] filtrCode(string[] code)
        {
            var code_f = new List<string>();
            for(int i = 0; i < code.Length; i++)
            {
                if (code[i].Contains(";"))
                {
                    var ind_err = code[i].IndexOf(";");
                    code[i] = code[i].Substring(0,ind_err);
                }
                //code[i] = code[i].Replace(" ", "");
                code[i] = code[i].Replace("\r", "");
                code[i] = code[i].Replace("\n", "");
                if (code[i].Length>1)
                {
                    code_f.Add((code[i]));
                }
            }
            return code_f.ToArray();
        }

        private void Program_frm_Shown(object sender, EventArgs e)
        {
            //OpenFile("New1.cnc");
            //SetDefaultViews();
            var point = new Point(14, 12);
            this.Location = point;
        }

        private void GCodeInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            //string filename = System.IO.Directory.GetCurrentDirectory() + "\\Temp\\Visu.tmp";
            if (e.KeyCode == Keys.Enter)
            {
                list_pr.Clear();
                list_pr.AddRange(GCodeInputBox.Lines);
                //    System.IO.File.Delete(filename);  
                //    File.WriteAllText(filename, GCodeInputBox.Text);
                if (Properties.Settings.Default.Virgin == true)
                {
                    this.StartPosition = FormStartPosition.CenterScreen;
                }
                else
                {
                    this.Location = Properties.Settings.Default.ViewFormLocation;
                }
                OpenFileProgram(list_pr);
                SetDefaultViews();
                var point = new Point(14, 12);
                this.Location = point;
                Properties.Settings.Default.Virgin = false;
              //  DrawLayers();
            }
        }

      /*  private void LayerSelector_ValueChanged(object sender, EventArgs e)
        {
            //  clsToolLayer str;

            //   str = Convert.ToDouble(LayerSelector.Value);
                      foreach (clsToolLayer tl in GcodeViewer.ToolLayers.Values)
                       {
                           nd = frm.tvTools.Nodes.Add("Tool " + tl.Number.ToString());
                           nd.ForeColor = tl.Color;
                           nd.Checked = !tl.Hidden;
                           nd.Tag = tl;
                       }
                       frm.tvTools.BackColor = this.MG_Viewer1.BackColor;
                       frm.StartPosition = FormStartPosition.Manual;
                       frm.Location = Control.MousePosition;
                       frm.ShowDialog();
            //  ((clsToolLayer)(str)).Hidden = true;
            // mViewer.Redraw(true);
        } */

 /*       private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.Unknown)
                return;
            ((clsToolLayer)e.Node.Tag).Hidden = !e.Node.Checked;
            mViewer.Redraw(true);
        }*/

        private void GCodeInputBox_TextChanged(object sender, EventArgs e)
        {
            //string filename = System.IO.Directory.GetCurrentDirectory() + "\\Temp\\Visu.tmp";
            //    System.IO.File.Delete(filename);
            //    File.WriteAllText(filename, GCodeInputBox.Text);
            //    if (Properties.Settings.Default.Virgin == true)
            //    {
            //        this.StartPosition = FormStartPosition.CenterScreen;
            //    }
            //    else
            //    {
            //        this.Location = Properties.Settings.Default.ViewFormLocation;
            //    }
            //    OpenFile(filename);
            //    SetDefaultViews();
            //    var point = new Point(14, 12);
            //    this.Location = point;
            //    Properties.Settings.Default.Virgin = false;
            //    DrawLayers();
        }

        /*     int MouseState;
             byte MClick;

             private void GCodeViewerProgram_MouseDown(object sender, MouseEventArgs e)
             {
                 if (e.Button == System.Windows.Forms.MouseButtons.Right)
                 {

                     mViewer.ViewManipMode = GcodeViewer.ManipMode.ROTATE;
                     MouseState = 1;
                     //MessageBox.Show("Right click");
                 }
                 else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                 {
                     MClick++;
                     if (MClick == 2)
                     {
                         mViewer.FindExtents();
                         MClick = 0;
                     }
                 }
                 else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                 {
                     mViewer.ViewManipMode = GcodeViewer.ManipMode.PAN;
                     MouseState = 1;
                 }
                 //   if (e.Button == System.Windows.Forms.MouseButtons.Left) { MessageBox.Show("Left click"); }
             }

          /*   private void GCodeViewerProgram_MouseUp(object sender, MouseEventArgs e)
             {
                 if (MouseState == 1)
                 {
                     mViewer.ViewManipMode = GcodeViewer.ManipMode.SELECTION;
                     MouseState = 0;
                 }

             }

                 private void GCodeInputBox_Click(object sender, EventArgs e)
                {

                }

               private void RedrawBtn_Click(object sender, EventArgs e)
                {
                    list.Clear();
                    list.AddRange(GCodeInputBox.Lines);
                    //    System.IO.File.Delete(filename);  
                    //    File.WriteAllText(filename, GCodeInputBox.Text);
                    if (Properties.Settings.Default.Virgin == true)
                    {
                        this.StartPosition = FormStartPosition.CenterScreen;
                    }
                    else
                    {
                        this.Location = Properties.Settings.Default.ViewFormLocation;
                    }
                    OpenFileProgram(list);
                    SetDefaultViews();
                    var point = new Point(14, 12);
                    this.Location = point;
                    Properties.Settings.Default.Virgin = false;
                   // DrawLayers();
                }

                private void GCodeInputBox_DoubleClick(object sender, EventArgs e)
                {
                    int CurrentString = GCodeInputBox.GetLineFromCharIndex(GCodeInputBox.SelectionStart) + 1;
                    string Str = GCodeInputBox.Lines[CurrentString].ToString();
                    //mViewer.List
                }*/
    }
}
