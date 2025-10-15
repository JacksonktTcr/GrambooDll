namespace REGAL.Forms.CNRL
{
    partial class AddNewMasterData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddNewMasterData));
            this.btnNew = new Gramboo.Controls.GrbButton();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNew.BackgroundImage")));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.btnNew.FlatAppearance.BorderSize = 2;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Location = new System.Drawing.Point(3, 3);
            this.btnNew.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("btnNew.MouseDownImage")));
            this.btnNew.Name = "btnNew";
            this.btnNew.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnNew.NormalImage")));
            this.btnNew.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("btnNew.OnFocusImage")));
            this.btnNew.SelectedImage = ((System.Drawing.Image)(resources.GetObject("btnNew.SelectedImage")));
            this.btnNew.Size = new System.Drawing.Size(61, 23);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "+";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // AddNewMasterData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnNew);
            this.Name = "AddNewMasterData";
            this.Size = new System.Drawing.Size(67, 32);
            this.ResumeLayout(false);

        }

        #endregion

        private Gramboo.Controls.GrbButton btnNew;
    }
}
