namespace BTP
{
    partial class Camera1_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Camera1_frm));
            this.DimensionCam1 = new System.Windows.Forms.Label();
            this.ZoomLable = new System.Windows.Forms.Label();
            this.Camera1XCoord = new System.Windows.Forms.Label();
            this.ZoomInCam1 = new System.Windows.Forms.Button();
            this.CameraImage = new System.Windows.Forms.ImageList(this.components);
            this.ZoomOutCam1 = new System.Windows.Forms.Button();
            this.label44 = new System.Windows.Forms.Label();
            this.MoveRightCam1 = new System.Windows.Forms.Button();
            this.MoveLeftCam1 = new System.Windows.Forms.Button();
            this.label43 = new System.Windows.Forms.Label();
            this.RecVideoCam1 = new System.Windows.Forms.Button();
            this.SnapCam1 = new System.Windows.Forms.Button();
            this.Camera1PictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DimensionCam2 = new System.Windows.Forms.Label();
            this.Cam2YCoord = new System.Windows.Forms.Label();
            this.Cam2XCoord = new System.Windows.Forms.Label();
            this.MoveDownCam2 = new System.Windows.Forms.Button();
            this.MoveUpCam2 = new System.Windows.Forms.Button();
            this.MoveRightCam2 = new System.Windows.Forms.Button();
            this.MoveLeftCam2 = new System.Windows.Forms.Button();
            this.RecVideoCam2 = new System.Windows.Forms.Button();
            this.SnapCam2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Camera2PictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Camera1SaveTimer = new System.Windows.Forms.Timer(this.components);
            this.Camera2SaveTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Camera1PictureBox)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Camera2PictureBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DimensionCam1
            // 
            resources.ApplyResources(this.DimensionCam1, "DimensionCam1");
            this.DimensionCam1.Name = "DimensionCam1";
            // 
            // ZoomLable
            // 
            resources.ApplyResources(this.ZoomLable, "ZoomLable");
            this.ZoomLable.Name = "ZoomLable";
            // 
            // Camera1XCoord
            // 
            resources.ApplyResources(this.Camera1XCoord, "Camera1XCoord");
            this.Camera1XCoord.Name = "Camera1XCoord";
            // 
            // ZoomInCam1
            // 
            resources.ApplyResources(this.ZoomInCam1, "ZoomInCam1");
            this.ZoomInCam1.ImageList = this.CameraImage;
            this.ZoomInCam1.Name = "ZoomInCam1";
            this.ZoomInCam1.UseVisualStyleBackColor = true;
            this.ZoomInCam1.Click += new System.EventHandler(this.ZoomInCam1_Click);
            // 
            // CameraImage
            // 
            this.CameraImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("CameraImage.ImageStream")));
            this.CameraImage.TransparentColor = System.Drawing.Color.Transparent;
            this.CameraImage.Images.SetKeyName(0, "Arrow_down.png");
            this.CameraImage.Images.SetKeyName(1, "Arrow_Left.png");
            this.CameraImage.Images.SetKeyName(2, "Arrow_Right.png");
            this.CameraImage.Images.SetKeyName(3, "Arrow_up.png");
            this.CameraImage.Images.SetKeyName(4, "Zoom_In.png");
            this.CameraImage.Images.SetKeyName(5, "Zoom_Out.png");
            // 
            // ZoomOutCam1
            // 
            resources.ApplyResources(this.ZoomOutCam1, "ZoomOutCam1");
            this.ZoomOutCam1.ImageList = this.CameraImage;
            this.ZoomOutCam1.Name = "ZoomOutCam1";
            this.ZoomOutCam1.UseVisualStyleBackColor = true;
            this.ZoomOutCam1.Click += new System.EventHandler(this.ZoomOutCam1_Click);
            // 
            // label44
            // 
            resources.ApplyResources(this.label44, "label44");
            this.label44.Name = "label44";
            // 
            // MoveRightCam1
            // 
            resources.ApplyResources(this.MoveRightCam1, "MoveRightCam1");
            this.MoveRightCam1.ImageList = this.CameraImage;
            this.MoveRightCam1.Name = "MoveRightCam1";
            this.MoveRightCam1.UseVisualStyleBackColor = true;
            this.MoveRightCam1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveRightCam1_MouseDown);
            this.MoveRightCam1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveRightCam1_MouseUp);
            // 
            // MoveLeftCam1
            // 
            resources.ApplyResources(this.MoveLeftCam1, "MoveLeftCam1");
            this.MoveLeftCam1.ImageList = this.CameraImage;
            this.MoveLeftCam1.Name = "MoveLeftCam1";
            this.MoveLeftCam1.UseVisualStyleBackColor = true;
            this.MoveLeftCam1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveLeftCam1_MouseDown);
            this.MoveLeftCam1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveLeftCam1_MouseUp);
            // 
            // label43
            // 
            resources.ApplyResources(this.label43, "label43");
            this.label43.Name = "label43";
            // 
            // RecVideoCam1
            // 
            resources.ApplyResources(this.RecVideoCam1, "RecVideoCam1");
            this.RecVideoCam1.Name = "RecVideoCam1";
            this.RecVideoCam1.UseVisualStyleBackColor = true;
            this.RecVideoCam1.Click += new System.EventHandler(this.RecVideoCam1_Click);
            // 
            // SnapCam1
            // 
            resources.ApplyResources(this.SnapCam1, "SnapCam1");
            this.SnapCam1.Name = "SnapCam1";
            this.SnapCam1.UseVisualStyleBackColor = true;
            this.SnapCam1.Click += new System.EventHandler(this.SnapCam1_Click);
            // 
            // Camera1PictureBox
            // 
            resources.ApplyResources(this.Camera1PictureBox, "Camera1PictureBox");
            this.Camera1PictureBox.Name = "Camera1PictureBox";
            this.Camera1PictureBox.TabStop = false;
            this.Camera1PictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Camera1PictureBox_MouseClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DimensionCam2);
            this.groupBox3.Controls.Add(this.Cam2YCoord);
            this.groupBox3.Controls.Add(this.Cam2XCoord);
            this.groupBox3.Controls.Add(this.MoveDownCam2);
            this.groupBox3.Controls.Add(this.MoveUpCam2);
            this.groupBox3.Controls.Add(this.MoveRightCam2);
            this.groupBox3.Controls.Add(this.MoveLeftCam2);
            this.groupBox3.Controls.Add(this.RecVideoCam2);
            this.groupBox3.Controls.Add(this.SnapCam2);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.Camera2PictureBox);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // DimensionCam2
            // 
            resources.ApplyResources(this.DimensionCam2, "DimensionCam2");
            this.DimensionCam2.Name = "DimensionCam2";
            // 
            // Cam2YCoord
            // 
            resources.ApplyResources(this.Cam2YCoord, "Cam2YCoord");
            this.Cam2YCoord.Name = "Cam2YCoord";
            // 
            // Cam2XCoord
            // 
            resources.ApplyResources(this.Cam2XCoord, "Cam2XCoord");
            this.Cam2XCoord.Name = "Cam2XCoord";
            // 
            // MoveDownCam2
            // 
            resources.ApplyResources(this.MoveDownCam2, "MoveDownCam2");
            this.MoveDownCam2.ImageList = this.CameraImage;
            this.MoveDownCam2.Name = "MoveDownCam2";
            this.MoveDownCam2.UseVisualStyleBackColor = true;
            this.MoveDownCam2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveDownCam2_MouseDown);
            this.MoveDownCam2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveDownCam2_MouseUp);
            // 
            // MoveUpCam2
            // 
            resources.ApplyResources(this.MoveUpCam2, "MoveUpCam2");
            this.MoveUpCam2.ImageList = this.CameraImage;
            this.MoveUpCam2.Name = "MoveUpCam2";
            this.MoveUpCam2.UseVisualStyleBackColor = true;
            this.MoveUpCam2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveUpCam2_MouseDown);
            this.MoveUpCam2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveUpCam2_MouseUp);
            // 
            // MoveRightCam2
            // 
            resources.ApplyResources(this.MoveRightCam2, "MoveRightCam2");
            this.MoveRightCam2.ImageList = this.CameraImage;
            this.MoveRightCam2.Name = "MoveRightCam2";
            this.MoveRightCam2.UseVisualStyleBackColor = true;
            this.MoveRightCam2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveRightCam2_MouseDown);
            this.MoveRightCam2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveRightCam2_MouseUp);
            // 
            // MoveLeftCam2
            // 
            resources.ApplyResources(this.MoveLeftCam2, "MoveLeftCam2");
            this.MoveLeftCam2.ImageList = this.CameraImage;
            this.MoveLeftCam2.Name = "MoveLeftCam2";
            this.MoveLeftCam2.UseVisualStyleBackColor = true;
            this.MoveLeftCam2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoveLeftCam2_MouseDown);
            this.MoveLeftCam2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoveLeftCam2_MouseUp);
            // 
            // RecVideoCam2
            // 
            resources.ApplyResources(this.RecVideoCam2, "RecVideoCam2");
            this.RecVideoCam2.Name = "RecVideoCam2";
            this.RecVideoCam2.UseVisualStyleBackColor = true;
            this.RecVideoCam2.Click += new System.EventHandler(this.RecVideoCam2_Click);
            // 
            // SnapCam2
            // 
            resources.ApplyResources(this.SnapCam2, "SnapCam2");
            this.SnapCam2.Name = "SnapCam2";
            this.SnapCam2.UseVisualStyleBackColor = true;
            this.SnapCam2.Click += new System.EventHandler(this.SnapCam2_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // Camera2PictureBox
            // 
            resources.ApplyResources(this.Camera2PictureBox, "Camera2PictureBox");
            this.Camera2PictureBox.Name = "Camera2PictureBox";
            this.Camera2PictureBox.TabStop = false;
            this.Camera2PictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Camera2PictureBox_MouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DimensionCam1);
            this.groupBox2.Controls.Add(this.ZoomLable);
            this.groupBox2.Controls.Add(this.SnapCam1);
            this.groupBox2.Controls.Add(this.Camera1XCoord);
            this.groupBox2.Controls.Add(this.RecVideoCam1);
            this.groupBox2.Controls.Add(this.ZoomInCam1);
            this.groupBox2.Controls.Add(this.label43);
            this.groupBox2.Controls.Add(this.ZoomOutCam1);
            this.groupBox2.Controls.Add(this.MoveLeftCam1);
            this.groupBox2.Controls.Add(this.label44);
            this.groupBox2.Controls.Add(this.MoveRightCam1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.Camera1PictureBox);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Camera1SaveTimer
            // 
            this.Camera1SaveTimer.Interval = 40;
            this.Camera1SaveTimer.Tick += new System.EventHandler(this.Camera1SaveTimer_Tick);
            // 
            // Camera2SaveTimer
            // 
            this.Camera2SaveTimer.Interval = 40;
            this.Camera2SaveTimer.Tick += new System.EventHandler(this.Camera2SaveTimer_Tick);
            // 
            // Camera1_frm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Camera1_frm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Camera1_frm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Camera1_frm_FormClosed);
            this.Load += new System.EventHandler(this.Camera1_frm_Load);
            this.Shown += new System.EventHandler(this.Camera1_frm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Camera1PictureBox)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Camera2PictureBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label DimensionCam1;
        private System.Windows.Forms.Label ZoomLable;
        private System.Windows.Forms.Button ZoomInCam1;
        private System.Windows.Forms.Button ZoomOutCam1;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Button MoveRightCam1;
        private System.Windows.Forms.Button MoveLeftCam1;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Button RecVideoCam1;
        private System.Windows.Forms.Button SnapCam1;
        private System.Windows.Forms.PictureBox Camera1PictureBox;
        private System.Windows.Forms.ImageList CameraImage;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label DimensionCam2;
        private System.Windows.Forms.Label Cam2YCoord;
        private System.Windows.Forms.Label Cam2XCoord;
        private System.Windows.Forms.Button MoveDownCam2;
        private System.Windows.Forms.Button MoveUpCam2;
        private System.Windows.Forms.Button MoveRightCam2;
        private System.Windows.Forms.Button MoveLeftCam2;
        private System.Windows.Forms.Button RecVideoCam2;
        private System.Windows.Forms.Button SnapCam2;
        private System.Windows.Forms.Label Camera1XCoord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox Camera2PictureBox;
        private System.Windows.Forms.Timer Camera1SaveTimer;
        private System.Windows.Forms.Timer Camera2SaveTimer;
    }
}