namespace BTP
{
    partial class Auto_frm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Auto_frm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GCodeViewer = new MacGen.GcodeViewer();
            this.rmbView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuFit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFence = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRotate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSelect = new System.Windows.Forms.ToolStripButton();
            this.tsbRotate = new System.Windows.Forms.ToolStripButton();
            this.tsbFit = new System.Windows.Forms.ToolStripButton();
            this.tsbZoom = new System.Windows.Forms.ToolStripButton();
            this.tsbFence = new System.Windows.Forms.ToolStripButton();
            this.tsbPan = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.GCodeBox = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.PosPF = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.PosSp = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PosS3 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PosS2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PosS1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PosY = new System.Windows.Forms.Label();
            this.PosX = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.ActualVelocityPF2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.ActualVelocityPF1 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.ActualVelocityS3 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.ActualVelocityS2 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.ActualVelocityS1 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.ActualVelocityA = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.ActualVelocityW = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.ActualVelocityV = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.ActualVelocityU = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.ActualVelocityZ = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.ActualVelocityY = new System.Windows.Forms.Label();
            this.ActualVelocityX = new System.Windows.Forms.Label();
            this.CurVelLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.VelocityBar = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.OperatorTB = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.PrintReportBtn = new System.Windows.Forms.Button();
            this.SaveReport = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.rmbView.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VelocityBar)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GCodeViewer);
            this.groupBox1.Controls.Add(this.toolStrip1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // GCodeViewer
            // 
            this.GCodeViewer.AxisIndicatorScale = 0.75F;
            this.GCodeViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GCodeViewer.BreakPoint = -1;
            this.GCodeViewer.ContextMenuStrip = this.rmbView;
            this.GCodeViewer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GCodeViewer.DynamicViewManipulation = true;
            this.GCodeViewer.FourthAxis = 0F;
            resources.ApplyResources(this.GCodeViewer, "GCodeViewer");
            this.GCodeViewer.Name = "GCodeViewer";
            this.GCodeViewer.Pitch = 0F;
            this.GCodeViewer.Roll = 0F;
            this.GCodeViewer.RotaryType = MacGen.RotaryMotionType.BMC;
            this.GCodeViewer.ViewManipMode = MacGen.GcodeViewer.ManipMode.SELECTION;
            this.GCodeViewer.Yaw = 0F;
            // 
            // rmbView
            // 
            this.rmbView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFit,
            this.mnuFence,
            this.mnuPan,
            this.mnuRotate,
            this.mnuZoom,
            this.mnuSelect});
            this.rmbView.Name = "rmbView";
            resources.ApplyResources(this.rmbView, "rmbView");
            // 
            // mnuFit
            // 
            resources.ApplyResources(this.mnuFit, "mnuFit");
            this.mnuFit.Name = "mnuFit";
            this.mnuFit.Tag = "Fit";
            this.mnuFit.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // mnuFence
            // 
            resources.ApplyResources(this.mnuFence, "mnuFence");
            this.mnuFence.Name = "mnuFence";
            this.mnuFence.Tag = "Fence";
            // 
            // mnuPan
            // 
            resources.ApplyResources(this.mnuPan, "mnuPan");
            this.mnuPan.Name = "mnuPan";
            this.mnuPan.Tag = "Pan";
            // 
            // mnuRotate
            // 
            resources.ApplyResources(this.mnuRotate, "mnuRotate");
            this.mnuRotate.Name = "mnuRotate";
            this.mnuRotate.Tag = "Rotate";
            // 
            // mnuZoom
            // 
            resources.ApplyResources(this.mnuZoom, "mnuZoom");
            this.mnuZoom.Name = "mnuZoom";
            this.mnuZoom.Tag = "Zoom";
            // 
            // mnuSelect
            // 
            resources.ApplyResources(this.mnuSelect, "mnuSelect");
            this.mnuSelect.Name = "mnuSelect";
            this.mnuSelect.Tag = "Select";
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSelect,
            this.tsbRotate,
            this.tsbFit,
            this.tsbZoom,
            this.tsbFence,
            this.tsbPan,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbSelect
            // 
            this.tsbSelect.Checked = true;
            this.tsbSelect.CheckOnClick = true;
            this.tsbSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbSelect, "tsbSelect");
            this.tsbSelect.Name = "tsbSelect";
            this.tsbSelect.Tag = "Select";
            this.tsbSelect.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // tsbRotate
            // 
            this.tsbRotate.CheckOnClick = true;
            this.tsbRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbRotate, "tsbRotate");
            this.tsbRotate.Name = "tsbRotate";
            this.tsbRotate.Tag = "Rotate";
            this.tsbRotate.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // tsbFit
            // 
            this.tsbFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbFit, "tsbFit");
            this.tsbFit.Name = "tsbFit";
            this.tsbFit.Tag = "Fit";
            this.tsbFit.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // tsbZoom
            // 
            this.tsbZoom.CheckOnClick = true;
            this.tsbZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbZoom, "tsbZoom");
            this.tsbZoom.Name = "tsbZoom";
            this.tsbZoom.Tag = "Zoom";
            this.tsbZoom.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // tsbFence
            // 
            this.tsbFence.CheckOnClick = true;
            this.tsbFence.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbFence, "tsbFence");
            this.tsbFence.Name = "tsbFence";
            this.tsbFence.Tag = "Fence";
            this.tsbFence.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // tsbPan
            // 
            this.tsbPan.CheckOnClick = true;
            this.tsbPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.tsbPan, "tsbPan");
            this.tsbPan.Name = "tsbPan";
            this.tsbPan.Tag = "Pan";
            this.tsbPan.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Tag = "2DView";
            this.toolStripButton1.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Tag = "3DView";
            this.toolStripButton2.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.GCodeBox);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // GCodeBox
            // 
            resources.ApplyResources(this.GCodeBox, "GCodeBox");
            this.GCodeBox.FormattingEnabled = true;
            this.GCodeBox.Name = "GCodeBox";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.PosPF);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.PosSp);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.PosS3);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.PosS2);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.PosS1);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.PosY);
            this.groupBox5.Controls.Add(this.PosX);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // PosPF
            // 
            resources.ApplyResources(this.PosPF, "PosPF");
            this.PosPF.Name = "PosPF";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // PosSp
            // 
            resources.ApplyResources(this.PosSp, "PosSp");
            this.PosSp.Name = "PosSp";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // PosS3
            // 
            resources.ApplyResources(this.PosS3, "PosS3");
            this.PosS3.Name = "PosS3";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // PosS2
            // 
            resources.ApplyResources(this.PosS2, "PosS2");
            this.PosS2.Name = "PosS2";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // PosS1
            // 
            resources.ApplyResources(this.PosS1, "PosS1");
            this.PosS1.Name = "PosS1";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // PosY
            // 
            resources.ApplyResources(this.PosY, "PosY");
            this.PosY.Name = "PosY";
            // 
            // PosX
            // 
            resources.ApplyResources(this.PosX, "PosX");
            this.PosX.Name = "PosX";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.ActualVelocityPF2);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.ActualVelocityPF1);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.ActualVelocityS3);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.ActualVelocityS2);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.ActualVelocityS1);
            this.groupBox2.Controls.Add(this.label32);
            this.groupBox2.Controls.Add(this.ActualVelocityA);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.ActualVelocityW);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.ActualVelocityV);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.ActualVelocityU);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.ActualVelocityZ);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.ActualVelocityY);
            this.groupBox2.Controls.Add(this.ActualVelocityX);
            this.groupBox2.Controls.Add(this.CurVelLabel);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.VelocityBar);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // ActualVelocityPF2
            // 
            resources.ApplyResources(this.ActualVelocityPF2, "ActualVelocityPF2");
            this.ActualVelocityPF2.Name = "ActualVelocityPF2";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // ActualVelocityPF1
            // 
            resources.ApplyResources(this.ActualVelocityPF1, "ActualVelocityPF1");
            this.ActualVelocityPF1.Name = "ActualVelocityPF1";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // ActualVelocityS3
            // 
            resources.ApplyResources(this.ActualVelocityS3, "ActualVelocityS3");
            this.ActualVelocityS3.Name = "ActualVelocityS3";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // ActualVelocityS2
            // 
            resources.ApplyResources(this.ActualVelocityS2, "ActualVelocityS2");
            this.ActualVelocityS2.Name = "ActualVelocityS2";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // ActualVelocityS1
            // 
            resources.ApplyResources(this.ActualVelocityS1, "ActualVelocityS1");
            this.ActualVelocityS1.Name = "ActualVelocityS1";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // ActualVelocityA
            // 
            resources.ApplyResources(this.ActualVelocityA, "ActualVelocityA");
            this.ActualVelocityA.Name = "ActualVelocityA";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // ActualVelocityW
            // 
            resources.ApplyResources(this.ActualVelocityW, "ActualVelocityW");
            this.ActualVelocityW.Name = "ActualVelocityW";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // ActualVelocityV
            // 
            resources.ApplyResources(this.ActualVelocityV, "ActualVelocityV");
            this.ActualVelocityV.Name = "ActualVelocityV";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // ActualVelocityU
            // 
            resources.ApplyResources(this.ActualVelocityU, "ActualVelocityU");
            this.ActualVelocityU.Name = "ActualVelocityU";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // ActualVelocityZ
            // 
            resources.ApplyResources(this.ActualVelocityZ, "ActualVelocityZ");
            this.ActualVelocityZ.Name = "ActualVelocityZ";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // ActualVelocityY
            // 
            resources.ApplyResources(this.ActualVelocityY, "ActualVelocityY");
            this.ActualVelocityY.Name = "ActualVelocityY";
            // 
            // ActualVelocityX
            // 
            resources.ApplyResources(this.ActualVelocityX, "ActualVelocityX");
            this.ActualVelocityX.Name = "ActualVelocityX";
            // 
            // CurVelLabel
            // 
            resources.ApplyResources(this.CurVelLabel, "CurVelLabel");
            this.CurVelLabel.Name = "CurVelLabel";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // VelocityBar
            // 
            resources.ApplyResources(this.VelocityBar, "VelocityBar");
            this.VelocityBar.LargeChange = 10;
            this.VelocityBar.Maximum = 120;
            this.VelocityBar.Minimum = 1;
            this.VelocityBar.Name = "VelocityBar";
            this.VelocityBar.TickFrequency = 20;
            this.VelocityBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.VelocityBar.Value = 100;
            this.VelocityBar.Scroll += new System.EventHandler(this.VelocityBar_Scroll);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.OperatorTB);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.PrintReportBtn);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // OperatorTB
            // 
            resources.ApplyResources(this.OperatorTB, "OperatorTB");
            this.OperatorTB.Name = "OperatorTB";
            //this.OperatorTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OperatorTB_KeyDown);
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // PrintReportBtn
            // 
            resources.ApplyResources(this.PrintReportBtn, "PrintReportBtn");
            this.PrintReportBtn.Name = "PrintReportBtn";
            this.PrintReportBtn.UseVisualStyleBackColor = true;
            //this.PrintReportBtn.Click += new System.EventHandler(this.PrintReportBtn_Click);
            // 
            // SaveReport
            // 
            this.SaveReport.DefaultExt = "*.xlsx";
            this.SaveReport.FileName = "Report";
            resources.ApplyResources(this.SaveReport, "SaveReport");
            // 
            // Auto_frm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Auto_frm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Activated += new System.EventHandler(this.Auto_frm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Auto_frm_FormClosing);
            this.Load += new System.EventHandler(this.Auto_frm_Load);
            this.Shown += new System.EventHandler(this.Auto_frm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.rmbView.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VelocityBar)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label PosS2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PosS1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label PosY;
        private System.Windows.Forms.Label PosX;
        private System.Windows.Forms.Label PosPF;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label PosSp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label PosS3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox GCodeBox;
        internal System.Windows.Forms.ContextMenuStrip rmbView;
        internal System.Windows.Forms.ToolStripMenuItem mnuFit;
        internal System.Windows.Forms.ToolStripMenuItem mnuFence;
        internal System.Windows.Forms.ToolStripMenuItem mnuPan;
        internal System.Windows.Forms.ToolStripMenuItem mnuRotate;
        internal System.Windows.Forms.ToolStripMenuItem mnuZoom;
        internal System.Windows.Forms.ToolStripMenuItem mnuSelect;
        internal System.Windows.Forms.ToolStripButton tsbSelect;
        internal System.Windows.Forms.ToolStrip toolStrip1;
        internal System.Windows.Forms.ToolStripButton tsbRotate;
        internal System.Windows.Forms.ToolStripButton tsbFit;
        internal System.Windows.Forms.ToolStripButton tsbZoom;
        internal System.Windows.Forms.ToolStripButton tsbFence;
        internal System.Windows.Forms.ToolStripButton tsbPan;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TrackBar VelocityBar;
        private System.Windows.Forms.Label CurVelLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ActualVelocityA;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label ActualVelocityW;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label ActualVelocityV;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label ActualVelocityU;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label ActualVelocityZ;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label ActualVelocityY;
        private System.Windows.Forms.Label ActualVelocityX;
        private System.Windows.Forms.Label ActualVelocityPF2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label ActualVelocityPF1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label ActualVelocityS3;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label ActualVelocityS2;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label ActualVelocityS1;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button PrintReportBtn;
        private System.Windows.Forms.SaveFileDialog SaveReport;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox OperatorTB;
        internal MacGen.GcodeViewer GCodeViewer;
    }
}