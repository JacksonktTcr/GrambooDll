namespace Gramboo.Controls
{
    partial class ImageProcessor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImageViewer = new Gramboo.Controls.KpImageViewer();
            this.SuspendLayout();
            // 
            // ImageViewer
            // 
            this.ImageViewer.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.ImageViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageViewer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ImageViewer.GifAnimation = false;
            this.ImageViewer.GifFPS = 15D;
            this.ImageViewer.Image = null;
            this.ImageViewer.Location = new System.Drawing.Point(0, 0);
            this.ImageViewer.MenuColor = System.Drawing.Color.LightSteelBlue;
            this.ImageViewer.MenuPanelColor = System.Drawing.Color.LightSteelBlue;
            this.ImageViewer.MinimumSize = new System.Drawing.Size(454, 157);
            this.ImageViewer.Name = "ImageViewer";
            this.ImageViewer.NavigationPanelColor = System.Drawing.Color.LightSteelBlue;
            this.ImageViewer.NavigationTextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ImageViewer.OpenButton = false;
            this.ImageViewer.PreviewButton = false;
            this.ImageViewer.PreviewPanelColor = System.Drawing.Color.LightSteelBlue;
            this.ImageViewer.PreviewText = "Preview";
            this.ImageViewer.PreviewTextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ImageViewer.Rotation = 0;
            this.ImageViewer.Scrollbars = false;
            this.ImageViewer.ShowPreview = true;
            this.ImageViewer.Size = new System.Drawing.Size(454, 300);
            this.ImageViewer.TabIndex = 0;
            this.ImageViewer.TextColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ImageViewer.Zoom = 100D;
            // 
            // ImageProcessor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ImageViewer);
            this.Name = "ImageProcessor";
            this.Size = new System.Drawing.Size(450, 300);
            this.ResumeLayout(false);

        }

        #endregion

        public KpImageViewer ImageViewer;

    }
}
