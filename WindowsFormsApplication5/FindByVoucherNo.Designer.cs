namespace REGAL.Classes
{
    partial class FindByVoucherNo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindByVoucherNo));
            this.txtVchNo = new Gramboo.Controls.GrbTextBox();
            this.btnOpen = new Gramboo.Controls.GrbButton();
            this.btnCancel = new Gramboo.Controls.GrbButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtVchNo
            // 
            this.txtVchNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtVchNo.CueBanner = "Type Entry Number  Here and Press Open ";
            this.txtVchNo.Location = new System.Drawing.Point(6, 19);
            this.txtVchNo.Name = "txtVchNo";
            this.txtVchNo.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtVchNo.Size = new System.Drawing.Size(229, 20);
            this.txtVchNo.TabIndex = 0;
            // 
            // btnOpen
            // 
        //    this.btnOpen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpen.BackgroundImage")));
            this.btnOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.btnOpen.FlatAppearance.BorderSize = 2;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Location = new System.Drawing.Point(78, 45);
          //  this.btnOpen.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("btnOpen.MouseDownImage")));
            this.btnOpen.Name = "btnOpen";
            //this.btnOpen.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnOpen.NormalImage")));
            //this.btnOpen.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("btnOpen.OnFocusImage")));
            //this.btnOpen.SelectedImage = ((System.Drawing.Image)(resources.GetObject("btnOpen.SelectedImage")));
            this.btnOpen.Size = new System.Drawing.Size(75, 28);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnCancel
            // 
           // this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(159, 45);
           // this.btnCancel.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MouseDownImage")));
            this.btnCancel.Name = "btnCancel";
            //this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            //this.btnCancel.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.OnFocusImage")));
            //this.btnCancel.SelectedImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.SelectedImage")));
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtVchNo);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 84);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // FindByVoucherNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "FindByVoucherNo";
            this.Size = new System.Drawing.Size(241, 84);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gramboo.Controls.GrbTextBox txtVchNo;
        private Gramboo.Controls.GrbButton btnOpen;
        private Gramboo.Controls.GrbButton btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
