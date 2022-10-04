namespace BTP
{
    partial class Program_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Program_frm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.G_code_name = new System.Windows.Forms.Label();
            this.GCodeInputBox = new System.Windows.Forms.RichTextBox();
            this.PasteBtn = new System.Windows.Forms.Button();
            this.CopyBtn = new System.Windows.Forms.Button();
            this.CutBtn = new System.Windows.Forms.Button();
            this.UploadBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.OpenBtn = new System.Windows.Forms.Button();
            this.NewBtn = new System.Windows.Forms.Button();
            this.rmbView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuFit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFence = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRotate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Coordinates = new System.Windows.Forms.Label();
            this.GCodeViewerProgram = new MacGen.GcodeViewer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSelect = new System.Windows.Forms.ToolStripButton();
            this.tsbRotate = new System.Windows.Forms.ToolStripButton();
            this.tsbFit = new System.Windows.Forms.ToolStripButton();
            this.tsbZoom = new System.Windows.Forms.ToolStripButton();
            this.tsbFence = new System.Windows.Forms.ToolStripButton();
            this.tsbPan = new System.Windows.Forms.ToolStripButton();
            this.XY_Button = new System.Windows.Forms.ToolStripButton();
            this.D3_Button = new System.Windows.Forms.ToolStripButton();
            this.OpenGCode = new System.Windows.Forms.OpenFileDialog();
            this.SaveGCode = new System.Windows.Forms.SaveFileDialog();
            this.PicGCode = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.rmbView.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GCodeInputBox);
            this.groupBox1.Controls.Add(this.G_code_name);
            this.groupBox1.Controls.Add(this.PasteBtn);
            this.groupBox1.Controls.Add(this.CopyBtn);
            this.groupBox1.Controls.Add(this.CutBtn);
            this.groupBox1.Controls.Add(this.UploadBtn);
            this.groupBox1.Controls.Add(this.SaveBtn);
            this.groupBox1.Controls.Add(this.OpenBtn);
            this.groupBox1.Controls.Add(this.NewBtn);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // G_code_name
            // 
            resources.ApplyResources(this.G_code_name, "G_code_name");
            this.G_code_name.Name = "G_code_name";
            // 
            // GCodeInputBox
            // 
            resources.ApplyResources(this.GCodeInputBox, "GCodeInputBox");
            this.GCodeInputBox.Name = "GCodeInputBox";
            this.GCodeInputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GCodeInputBox_KeyDown);
            // 
            // PasteBtn
            // 
            resources.ApplyResources(this.PasteBtn, "PasteBtn");
            this.PasteBtn.Name = "PasteBtn";
            this.PasteBtn.UseVisualStyleBackColor = true;
            this.PasteBtn.Click += new System.EventHandler(this.PasteBtn_Click);
            // 
            // CopyBtn
            // 
            resources.ApplyResources(this.CopyBtn, "CopyBtn");
            this.CopyBtn.Name = "CopyBtn";
            this.CopyBtn.UseVisualStyleBackColor = true;
            this.CopyBtn.Click += new System.EventHandler(this.CopyBtn_Click);
            // 
            // CutBtn
            // 
            resources.ApplyResources(this.CutBtn, "CutBtn");
            this.CutBtn.Name = "CutBtn";
            this.CutBtn.UseVisualStyleBackColor = true;
            this.CutBtn.Click += new System.EventHandler(this.CutBtn_Click);
            // 
            // UploadBtn
            // 
            resources.ApplyResources(this.UploadBtn, "UploadBtn");
            this.UploadBtn.Name = "UploadBtn";
            this.UploadBtn.UseVisualStyleBackColor = true;
            this.UploadBtn.Click += new System.EventHandler(this.UploadBtn_Click);
            // 
            // SaveBtn
            // 
            resources.ApplyResources(this.SaveBtn, "SaveBtn");
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // OpenBtn
            // 
            resources.ApplyResources(this.OpenBtn, "OpenBtn");
            this.OpenBtn.Name = "OpenBtn";
            this.OpenBtn.UseVisualStyleBackColor = true;
            this.OpenBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // NewBtn
            // 
            resources.ApplyResources(this.NewBtn, "NewBtn");
            this.NewBtn.Name = "NewBtn";
            this.NewBtn.UseVisualStyleBackColor = true;
            this.NewBtn.Click += new System.EventHandler(this.NewBtn_Click);
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
            this.mnuFence.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // mnuPan
            // 
            resources.ApplyResources(this.mnuPan, "mnuPan");
            this.mnuPan.Name = "mnuPan";
            this.mnuPan.Tag = "Pan";
            this.mnuPan.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // mnuRotate
            // 
            resources.ApplyResources(this.mnuRotate, "mnuRotate");
            this.mnuRotate.Name = "mnuRotate";
            this.mnuRotate.Tag = "Rotate";
            this.mnuRotate.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // mnuZoom
            // 
            resources.ApplyResources(this.mnuZoom, "mnuZoom");
            this.mnuZoom.Name = "mnuZoom";
            this.mnuZoom.Tag = "Zoom";
            this.mnuZoom.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // mnuSelect
            // 
            resources.ApplyResources(this.mnuSelect, "mnuSelect");
            this.mnuSelect.Name = "mnuSelect";
            this.mnuSelect.Tag = "Select";
            this.mnuSelect.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Coordinates);
            this.groupBox2.Controls.Add(this.GCodeViewerProgram);
            this.groupBox2.Controls.Add(this.toolStrip1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // Coordinates
            // 
            resources.ApplyResources(this.Coordinates, "Coordinates");
            this.Coordinates.Name = "Coordinates";
            // 
            // GCodeViewerProgram
            // 
            this.GCodeViewerProgram.AxisIndicatorScale = 0.75F;
            this.GCodeViewerProgram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GCodeViewerProgram.BreakPoint = -1;
            this.GCodeViewerProgram.ContextMenuStrip = this.rmbView;
            this.GCodeViewerProgram.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GCodeViewerProgram.DynamicViewManipulation = true;
            this.GCodeViewerProgram.FourthAxis = 0F;
            resources.ApplyResources(this.GCodeViewerProgram, "GCodeViewerProgram");
            this.GCodeViewerProgram.Name = "GCodeViewerProgram";
            this.GCodeViewerProgram.Pitch = 0F;
            this.GCodeViewerProgram.Roll = 0F;
            this.GCodeViewerProgram.RotaryType = MacGen.RotaryMotionType.BMC;
            this.GCodeViewerProgram.ViewManipMode = MacGen.GcodeViewer.ManipMode.SELECTION;
            this.GCodeViewerProgram.Yaw = 0F;
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
            this.XY_Button,
            this.D3_Button});
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
            // XY_Button
            // 
            this.XY_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.XY_Button, "XY_Button");
            this.XY_Button.Name = "XY_Button";
            this.XY_Button.Tag = "2DView";
            this.XY_Button.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // D3_Button
            // 
            this.D3_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.D3_Button, "D3_Button");
            this.D3_Button.Name = "D3_Button";
            this.D3_Button.Tag = "3DView";
            this.D3_Button.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // OpenGCode
            // 
            this.OpenGCode.FileName = "G-Code";
            resources.ApplyResources(this.OpenGCode, "OpenGCode");
            this.OpenGCode.FilterIndex = 2;
            // 
            // SaveGCode
            // 
            this.SaveGCode.DefaultExt = "*.cnc";
            this.SaveGCode.FileName = "New";
            resources.ApplyResources(this.SaveGCode, "SaveGCode");
            // 
            // PicGCode
            // 
            this.PicGCode.AutoPopDelay = 3000;
            this.PicGCode.InitialDelay = 0;
            this.PicGCode.IsBalloon = true;
            this.PicGCode.OwnerDraw = true;
            this.PicGCode.ReshowDelay = 100;
            this.PicGCode.ToolTipTitle = "G-code source";
            // 
            // Program_frm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Program_frm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Program_frm_Load);
            this.Shown += new System.EventHandler(this.Program_frm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.rmbView.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.ContextMenuStrip rmbView;
        internal System.Windows.Forms.ToolStripMenuItem mnuFit;
        internal System.Windows.Forms.ToolStripMenuItem mnuFence;
        internal System.Windows.Forms.ToolStripMenuItem mnuPan;
        internal System.Windows.Forms.ToolStripMenuItem mnuRotate;
        internal System.Windows.Forms.ToolStripMenuItem mnuZoom;
        internal System.Windows.Forms.ToolStripMenuItem mnuSelect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label Coordinates;
        internal MacGen.GcodeViewer GCodeViewerProgram;
        internal System.Windows.Forms.ToolStrip toolStrip1;
        internal System.Windows.Forms.ToolStripButton tsbSelect;
        internal System.Windows.Forms.ToolStripButton tsbRotate;
        internal System.Windows.Forms.ToolStripButton tsbFit;
        internal System.Windows.Forms.ToolStripButton tsbZoom;
        internal System.Windows.Forms.ToolStripButton tsbFence;
        internal System.Windows.Forms.ToolStripButton tsbPan;
        private System.Windows.Forms.Button PasteBtn;
        private System.Windows.Forms.Button CopyBtn;
        private System.Windows.Forms.Button CutBtn;
        private System.Windows.Forms.Button UploadBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.Button OpenBtn;
        private System.Windows.Forms.Button NewBtn;
        private System.Windows.Forms.RichTextBox GCodeInputBox;
        private System.Windows.Forms.OpenFileDialog OpenGCode;
        private System.Windows.Forms.SaveFileDialog SaveGCode;
        public System.Windows.Forms.ToolTip PicGCode;
        private System.Windows.Forms.Label G_code_name;
        private System.Windows.Forms.ToolStripButton XY_Button;
        private System.Windows.Forms.ToolStripButton D3_Button;
    }
}