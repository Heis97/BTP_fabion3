namespace BTP
{
    partial class Main_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_frm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.PrintheadBtn = new System.Windows.Forms.Button();
            this.DiagnosticsBtn = new System.Windows.Forms.Button();
            this.SettingsBtn = new System.Windows.Forms.Button();
            this.AutoBtn = new System.Windows.Forms.Button();
            this.ProgramBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.ManualBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.EStopBtn = new System.Windows.Forms.Button();
            this.ResetBtn = new System.Windows.Forms.Button();
            this.PauseBtn = new System.Windows.Forms.Button();
            this.StopBtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.SS_Status = new System.Windows.Forms.StatusStrip();
            this.TSDD_Language = new System.Windows.Forms.ToolStripDropDownButton();
            this.EnglishTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.RussianTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSDD_Connection = new System.Windows.Forms.ToolStripStatusLabel();
            this.IPAdressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.XStatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.YStatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Z1StatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Z2StatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Z3StatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.WStatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.AStatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel10 = new System.Windows.Forms.ToolStripStatusLabel();
            this.S1StatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel11 = new System.Windows.Forms.ToolStripStatusLabel();
            this.S2StatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel12 = new System.Windows.Forms.ToolStripStatusLabel();
            this.S3StatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel13 = new System.Windows.Forms.ToolStripStatusLabel();
            this.PF1StatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel14 = new System.Windows.Forms.ToolStripStatusLabel();
            this.PF2StatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel15 = new System.Windows.Forms.ToolStripStatusLabel();
            this.BStatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel16 = new System.Windows.Forms.ToolStripStatusLabel();
            this.DStatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel17 = new System.Windows.Forms.ToolStripStatusLabel();
            this.CStatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel18 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ZoomStatusTL = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Indicator = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel19 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.timer_printer_pos = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SS_Status.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.PrintheadBtn);
            this.panel1.Controls.Add(this.DiagnosticsBtn);
            this.panel1.Controls.Add(this.SettingsBtn);
            this.panel1.Controls.Add(this.AutoBtn);
            this.panel1.Controls.Add(this.ProgramBtn);
            this.panel1.Controls.Add(this.ExitBtn);
            this.panel1.Controls.Add(this.ManualBtn);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // PrintheadBtn
            // 
            this.PrintheadBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.PrintheadBtn, "PrintheadBtn");
            this.PrintheadBtn.Name = "PrintheadBtn";
            this.PrintheadBtn.UseVisualStyleBackColor = false;
            this.PrintheadBtn.Click += new System.EventHandler(this.PrintheadBtn_Click);
            // 
            // DiagnosticsBtn
            // 
            this.DiagnosticsBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.DiagnosticsBtn, "DiagnosticsBtn");
            this.DiagnosticsBtn.Name = "DiagnosticsBtn";
            this.DiagnosticsBtn.UseVisualStyleBackColor = false;
            this.DiagnosticsBtn.Click += new System.EventHandler(this.DiagnosticsBtn_Click);
            // 
            // SettingsBtn
            // 
            this.SettingsBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.SettingsBtn, "SettingsBtn");
            this.SettingsBtn.Name = "SettingsBtn";
            this.SettingsBtn.UseVisualStyleBackColor = false;
            this.SettingsBtn.Click += new System.EventHandler(this.SettingsBtn_Click);
            // 
            // AutoBtn
            // 
            this.AutoBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.AutoBtn, "AutoBtn");
            this.AutoBtn.Name = "AutoBtn";
            this.AutoBtn.UseVisualStyleBackColor = false;
            this.AutoBtn.Click += new System.EventHandler(this.AutoBtn_Click);
            // 
            // ProgramBtn
            // 
            this.ProgramBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.ProgramBtn, "ProgramBtn");
            this.ProgramBtn.Name = "ProgramBtn";
            this.ProgramBtn.UseVisualStyleBackColor = false;
            this.ProgramBtn.Click += new System.EventHandler(this.ProgramBtn_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.ExitBtn, "ExitBtn");
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // ManualBtn
            // 
            this.ManualBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.ManualBtn, "ManualBtn");
            this.ManualBtn.Name = "ManualBtn";
            this.ManualBtn.UseVisualStyleBackColor = false;
            this.ManualBtn.Click += new System.EventHandler(this.ManualBtn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.EStopBtn);
            this.panel2.Controls.Add(this.ResetBtn);
            this.panel2.Controls.Add(this.PauseBtn);
            this.panel2.Controls.Add(this.StopBtn);
            this.panel2.Controls.Add(this.StartBtn);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // EStopBtn
            // 
            this.EStopBtn.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.EStopBtn, "EStopBtn");
            this.EStopBtn.Name = "EStopBtn";
            this.EStopBtn.UseVisualStyleBackColor = false;
            this.EStopBtn.Click += new System.EventHandler(this.EStopBtn_Click);
            // 
            // ResetBtn
            // 
            this.ResetBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.ResetBtn, "ResetBtn");
            this.ResetBtn.Name = "ResetBtn";
            this.ResetBtn.UseVisualStyleBackColor = false;
            this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // PauseBtn
            // 
            this.PauseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.PauseBtn, "PauseBtn");
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.UseVisualStyleBackColor = false;
            this.PauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // StopBtn
            // 
            this.StopBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.StopBtn, "StopBtn");
            this.StopBtn.Name = "StopBtn";
            this.StopBtn.UseVisualStyleBackColor = false;
            this.StopBtn.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.StartBtn, "StartBtn");
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // SS_Status
            // 
            this.SS_Status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSDD_Language,
            this.toolStripStatusLabel1,
            this.TSDD_Connection,
            this.IPAdressLabel,
            this.toolStripStatusLabel3,
            this.XStatusTL,
            this.toolStripStatusLabel4,
            this.YStatusTL,
            this.toolStripStatusLabel5,
            this.Z1StatusTL,
            this.toolStripStatusLabel6,
            this.Z2StatusTL,
            this.toolStripStatusLabel7,
            this.Z3StatusTL,
            this.toolStripStatusLabel8,
            this.WStatusTL,
            this.toolStripStatusLabel9,
            this.AStatusTL,
            this.toolStripStatusLabel10,
            this.S1StatusTL,
            this.toolStripStatusLabel11,
            this.S2StatusTL,
            this.toolStripStatusLabel12,
            this.S3StatusTL,
            this.toolStripStatusLabel13,
            this.PF1StatusTL,
            this.toolStripStatusLabel14,
            this.PF2StatusTL,
            this.toolStripStatusLabel15,
            this.BStatusTL,
            this.toolStripStatusLabel16,
            this.DStatusTL,
            this.toolStripStatusLabel17,
            this.CStatusTL,
            this.toolStripStatusLabel18,
            this.ZoomStatusTL,
            this.toolStripStatusLabel2,
            this.Indicator,
            this.toolStripStatusLabel19});
            resources.ApplyResources(this.SS_Status, "SS_Status");
            this.SS_Status.Name = "SS_Status";
            // 
            // TSDD_Language
            // 
            this.TSDD_Language.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSDD_Language.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EnglishTSMI,
            this.RussianTSMI});
            resources.ApplyResources(this.TSDD_Language, "TSDD_Language");
            this.TSDD_Language.Name = "TSDD_Language";
            // 
            // EnglishTSMI
            // 
            this.EnglishTSMI.Name = "EnglishTSMI";
            resources.ApplyResources(this.EnglishTSMI, "EnglishTSMI");
            this.EnglishTSMI.Click += new System.EventHandler(this.EnglishTSMI_Click);
            // 
            // RussianTSMI
            // 
            this.RussianTSMI.Checked = true;
            this.RussianTSMI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.RussianTSMI.Name = "RussianTSMI";
            resources.ApplyResources(this.RussianTSMI, "RussianTSMI");
            this.RussianTSMI.Click += new System.EventHandler(this.RussianTSMI_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // TSDD_Connection
            // 
            this.TSDD_Connection.BackColor = System.Drawing.SystemColors.Control;
            this.TSDD_Connection.Name = "TSDD_Connection";
            resources.ApplyResources(this.TSDD_Connection, "TSDD_Connection");
            // 
            // IPAdressLabel
            // 
            this.IPAdressLabel.BackColor = System.Drawing.SystemColors.Control;
            this.IPAdressLabel.Name = "IPAdressLabel";
            resources.ApplyResources(this.IPAdressLabel, "IPAdressLabel");
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            resources.ApplyResources(this.toolStripStatusLabel3, "toolStripStatusLabel3");
            // 
            // XStatusTL
            // 
            this.XStatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.XStatusTL.Name = "XStatusTL";
            resources.ApplyResources(this.XStatusTL, "XStatusTL");
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            resources.ApplyResources(this.toolStripStatusLabel4, "toolStripStatusLabel4");
            // 
            // YStatusTL
            // 
            this.YStatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.YStatusTL.Name = "YStatusTL";
            resources.ApplyResources(this.YStatusTL, "YStatusTL");
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            resources.ApplyResources(this.toolStripStatusLabel5, "toolStripStatusLabel5");
            // 
            // Z1StatusTL
            // 
            this.Z1StatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.Z1StatusTL.Name = "Z1StatusTL";
            resources.ApplyResources(this.Z1StatusTL, "Z1StatusTL");
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            resources.ApplyResources(this.toolStripStatusLabel6, "toolStripStatusLabel6");
            // 
            // Z2StatusTL
            // 
            this.Z2StatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.Z2StatusTL.Name = "Z2StatusTL";
            resources.ApplyResources(this.Z2StatusTL, "Z2StatusTL");
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            resources.ApplyResources(this.toolStripStatusLabel7, "toolStripStatusLabel7");
            // 
            // Z3StatusTL
            // 
            this.Z3StatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.Z3StatusTL.Name = "Z3StatusTL";
            resources.ApplyResources(this.Z3StatusTL, "Z3StatusTL");
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            resources.ApplyResources(this.toolStripStatusLabel8, "toolStripStatusLabel8");
            // 
            // WStatusTL
            // 
            this.WStatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.WStatusTL.Name = "WStatusTL";
            resources.ApplyResources(this.WStatusTL, "WStatusTL");
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            resources.ApplyResources(this.toolStripStatusLabel9, "toolStripStatusLabel9");
            // 
            // AStatusTL
            // 
            this.AStatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.AStatusTL.Name = "AStatusTL";
            resources.ApplyResources(this.AStatusTL, "AStatusTL");
            // 
            // toolStripStatusLabel10
            // 
            this.toolStripStatusLabel10.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel10.Name = "toolStripStatusLabel10";
            resources.ApplyResources(this.toolStripStatusLabel10, "toolStripStatusLabel10");
            // 
            // S1StatusTL
            // 
            this.S1StatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.S1StatusTL.Name = "S1StatusTL";
            resources.ApplyResources(this.S1StatusTL, "S1StatusTL");
            // 
            // toolStripStatusLabel11
            // 
            this.toolStripStatusLabel11.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel11.Name = "toolStripStatusLabel11";
            resources.ApplyResources(this.toolStripStatusLabel11, "toolStripStatusLabel11");
            // 
            // S2StatusTL
            // 
            this.S2StatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.S2StatusTL.Name = "S2StatusTL";
            resources.ApplyResources(this.S2StatusTL, "S2StatusTL");
            // 
            // toolStripStatusLabel12
            // 
            this.toolStripStatusLabel12.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel12.Name = "toolStripStatusLabel12";
            resources.ApplyResources(this.toolStripStatusLabel12, "toolStripStatusLabel12");
            // 
            // S3StatusTL
            // 
            this.S3StatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.S3StatusTL.Name = "S3StatusTL";
            resources.ApplyResources(this.S3StatusTL, "S3StatusTL");
            // 
            // toolStripStatusLabel13
            // 
            this.toolStripStatusLabel13.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel13.Name = "toolStripStatusLabel13";
            resources.ApplyResources(this.toolStripStatusLabel13, "toolStripStatusLabel13");
            // 
            // PF1StatusTL
            // 
            this.PF1StatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.PF1StatusTL.Name = "PF1StatusTL";
            resources.ApplyResources(this.PF1StatusTL, "PF1StatusTL");
            // 
            // toolStripStatusLabel14
            // 
            this.toolStripStatusLabel14.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel14.Name = "toolStripStatusLabel14";
            resources.ApplyResources(this.toolStripStatusLabel14, "toolStripStatusLabel14");
            // 
            // PF2StatusTL
            // 
            this.PF2StatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.PF2StatusTL.Name = "PF2StatusTL";
            resources.ApplyResources(this.PF2StatusTL, "PF2StatusTL");
            // 
            // toolStripStatusLabel15
            // 
            this.toolStripStatusLabel15.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel15.Name = "toolStripStatusLabel15";
            resources.ApplyResources(this.toolStripStatusLabel15, "toolStripStatusLabel15");
            // 
            // BStatusTL
            // 
            this.BStatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.BStatusTL.Name = "BStatusTL";
            resources.ApplyResources(this.BStatusTL, "BStatusTL");
            // 
            // toolStripStatusLabel16
            // 
            this.toolStripStatusLabel16.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel16.Name = "toolStripStatusLabel16";
            resources.ApplyResources(this.toolStripStatusLabel16, "toolStripStatusLabel16");
            // 
            // DStatusTL
            // 
            this.DStatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.DStatusTL.Name = "DStatusTL";
            resources.ApplyResources(this.DStatusTL, "DStatusTL");
            // 
            // toolStripStatusLabel17
            // 
            this.toolStripStatusLabel17.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel17.Name = "toolStripStatusLabel17";
            resources.ApplyResources(this.toolStripStatusLabel17, "toolStripStatusLabel17");
            // 
            // CStatusTL
            // 
            this.CStatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.CStatusTL.Name = "CStatusTL";
            resources.ApplyResources(this.CStatusTL, "CStatusTL");
            // 
            // toolStripStatusLabel18
            // 
            this.toolStripStatusLabel18.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel18.Name = "toolStripStatusLabel18";
            resources.ApplyResources(this.toolStripStatusLabel18, "toolStripStatusLabel18");
            // 
            // ZoomStatusTL
            // 
            this.ZoomStatusTL.BackColor = System.Drawing.SystemColors.Control;
            this.ZoomStatusTL.Name = "ZoomStatusTL";
            resources.ApplyResources(this.ZoomStatusTL, "ZoomStatusTL");
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            // 
            // Indicator
            // 
            this.Indicator.BackColor = System.Drawing.SystemColors.Control;
            this.Indicator.Name = "Indicator";
            resources.ApplyResources(this.Indicator, "Indicator");
            // 
            // toolStripStatusLabel19
            // 
            this.toolStripStatusLabel19.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel19.Name = "toolStripStatusLabel19";
            resources.ApplyResources(this.toolStripStatusLabel19, "toolStripStatusLabel19");
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // timer_printer_pos
            // 
            this.timer_printer_pos.Interval = 10;
            this.timer_printer_pos.Tick += new System.EventHandler(this.timer_printer_pos_Tick);
            // 
            // Main_frm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.SS_Status);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main_frm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_frm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_frm_FormClosed);
            this.Load += new System.EventHandler(this.BTP_Load);
            this.Shown += new System.EventHandler(this.Main_frm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.SS_Status.ResumeLayout(false);
            this.SS_Status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ManualBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.Button ProgramBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button EStopBtn;
        private System.Windows.Forms.Button ResetBtn;
        private System.Windows.Forms.Button PauseBtn;
        private System.Windows.Forms.Button StopBtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.StatusStrip SS_Status;
        private System.Windows.Forms.ToolStripDropDownButton TSDD_Language;
        private System.Windows.Forms.ToolStripMenuItem EnglishTSMI;
        private System.Windows.Forms.ToolStripMenuItem RussianTSMI;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button AutoBtn;
        private System.Windows.Forms.Button SettingsBtn;
        private System.Windows.Forms.Button DiagnosticsBtn;
        private System.Windows.Forms.ToolStripStatusLabel TSDD_Connection;
        private System.Windows.Forms.ToolStripStatusLabel IPAdressLabel;
        private System.Windows.Forms.ToolStripStatusLabel XStatusTL;
        private System.Windows.Forms.ToolStripStatusLabel YStatusTL;
        private System.Windows.Forms.ToolStripStatusLabel Z1StatusTL;
        private System.Windows.Forms.ToolStripStatusLabel Z2StatusTL;
        private System.Windows.Forms.ToolStripStatusLabel Z3StatusTL;
        private System.Windows.Forms.ToolStripStatusLabel WStatusTL;
        private System.Windows.Forms.ToolStripStatusLabel AStatusTL;
        private System.Windows.Forms.ToolStripStatusLabel S1StatusTL;
        private System.Windows.Forms.ToolStripStatusLabel S2StatusTL;
        private System.Windows.Forms.ToolStripStatusLabel S3StatusTL;
        private System.Windows.Forms.ToolStripStatusLabel PF1StatusTL;
        private System.Windows.Forms.ToolStripStatusLabel PF2StatusTL;
        private System.Windows.Forms.ToolStripStatusLabel BStatusTL;
        private System.Windows.Forms.ToolStripStatusLabel DStatusTL;
        private System.Windows.Forms.ToolStripStatusLabel CStatusTL;
        private System.Windows.Forms.ToolStripStatusLabel ZoomStatusTL;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel Indicator;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel10;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel11;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel12;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel13;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel14;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel15;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel16;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel17;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel18;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel19;
        private System.Windows.Forms.Button PrintheadBtn;
        private System.Windows.Forms.Timer timer_printer_pos;
    }
}

