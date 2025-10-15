namespace REGAL.Classes
{
    partial class RefreshButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RefreshButton));
            this.btnRefresh = new Gramboo.Controls.GrbButton();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.btnRefresh.FlatAppearance.BorderSize = 2;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(0, 0);
            this.btnRefresh.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.MouseDownImage")));
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.NormalImage")));
            this.btnRefresh.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.OnFocusImage")));
            this.btnRefresh.SelectedImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.SelectedImage")));
            this.btnRefresh.Size = new System.Drawing.Size(87, 30);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // RefreshButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Name = "RefreshButton";
            this.Size = new System.Drawing.Size(87, 30);
            this.ResumeLayout(false);

        }

        #endregion

        private Gramboo.Controls.GrbButton btnRefresh;
    }
}
