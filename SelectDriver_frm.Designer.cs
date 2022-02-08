namespace BTP
{
    partial class SelectDriver_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectDriver_frm));
            this.CancelBtn = new System.Windows.Forms.Button();
            this.OKBtn = new System.Windows.Forms.Button();
            this.SimulatorRadioBtn = new System.Windows.Forms.RadioButton();
            this.ControllerRadioBtn = new System.Windows.Forms.RadioButton();
            this.IPMaskedText = new IPAddressControlLib.IPAddressControl();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            resources.ApplyResources(this.CancelBtn, "CancelBtn");
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // OKBtn
            // 
            resources.ApplyResources(this.OKBtn, "OKBtn");
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // SimulatorRadioBtn
            // 
            resources.ApplyResources(this.SimulatorRadioBtn, "SimulatorRadioBtn");
            this.SimulatorRadioBtn.Name = "SimulatorRadioBtn";
            this.SimulatorRadioBtn.TabStop = true;
            this.SimulatorRadioBtn.UseVisualStyleBackColor = true;
            this.SimulatorRadioBtn.CheckedChanged += new System.EventHandler(this.SimulatorRadioBtn_CheckedChanged);
            // 
            // ControllerRadioBtn
            // 
            resources.ApplyResources(this.ControllerRadioBtn, "ControllerRadioBtn");
            this.ControllerRadioBtn.Name = "ControllerRadioBtn";
            this.ControllerRadioBtn.TabStop = true;
            this.ControllerRadioBtn.UseVisualStyleBackColor = true;
            this.ControllerRadioBtn.CheckedChanged += new System.EventHandler(this.ControllerRadioBtn_CheckedChanged);
            // 
            // IPMaskedText
            // 
            this.IPMaskedText.AllowInternalTab = false;
            this.IPMaskedText.AutoHeight = true;
            this.IPMaskedText.BackColor = System.Drawing.SystemColors.Window;
            this.IPMaskedText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.IPMaskedText.Cursor = System.Windows.Forms.Cursors.IBeam;
            resources.ApplyResources(this.IPMaskedText, "IPMaskedText");
            this.IPMaskedText.Name = "IPMaskedText";
            this.IPMaskedText.ReadOnly = false;
            // 
            // SelectDriver_frm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.IPMaskedText);
            this.Controls.Add(this.ControllerRadioBtn);
            this.Controls.Add(this.SimulatorRadioBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.CancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectDriver_frm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.SelectDriver_frm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.RadioButton SimulatorRadioBtn;
        private System.Windows.Forms.RadioButton ControllerRadioBtn;
        private IPAddressControlLib.IPAddressControl IPMaskedText;
    }
}