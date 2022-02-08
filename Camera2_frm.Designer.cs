namespace BTP
{
    partial class Camera2_frm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Camera2_frm));
            this.CameraImage = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
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
            // Camera2_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 320);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(670, 606);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Camera2_frm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Camera2_frm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Camera2_frm_FormClosing);
            this.Shown += new System.EventHandler(this.Camera2_frm_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList CameraImage;
    }
}