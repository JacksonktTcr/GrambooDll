namespace Gramboo.Controls
{
    partial class frmAuth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAuth));
            this.label1 = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new Gramboo.Controls.GrbTextBox();
            this.txtRemarks = new Gramboo.Controls.GrbTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAction = new Gramboo.Controls.GrbTextBox();
            this.txtLogId = new Gramboo.Controls.GrbTextBox();
            this.btnConfirm = new Gramboo.Controls.GrbButton();
            this.btnCancel = new Gramboo.Controls.GrbButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Confirm Password";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConfirmPassword.Location = new System.Drawing.Point(110, 13);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(225, 20);
            this.txtConfirmPassword.TabIndex = 0;
            // 
            // txtRemarks
            // 
            this.txtRemarks.AcceptBlankValue = false;
            this.txtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemarks.DataField = "Remarks";
            this.txtRemarks.Location = new System.Drawing.Point(110, 39);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtRemarks.Size = new System.Drawing.Size(225, 89);
            this.txtRemarks.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Remarks";
            // 
            // txtAction
            // 
            this.txtAction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAction.DataField = "Action";
            this.txtAction.Location = new System.Drawing.Point(16, 64);
            this.txtAction.Name = "txtAction";
            this.txtAction.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtAction.PasswordChar = '*';
            this.txtAction.Size = new System.Drawing.Size(43, 20);
            this.txtAction.TabIndex = 22;
            this.txtAction.Visible = false;
            // 
            // txtLogId
            // 
            this.txtLogId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLogId.DataField = "LogId";
            this.txtLogId.IsIDField = true;
            this.txtLogId.Location = new System.Drawing.Point(16, 90);
            this.txtLogId.Name = "txtLogId";
            this.txtLogId.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtLogId.PasswordChar = '*';
            this.txtLogId.Size = new System.Drawing.Size(43, 20);
            this.txtLogId.TabIndex = 23;
            this.txtLogId.Visible = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.BackgroundImage")));
            this.btnConfirm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirm.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.btnConfirm.FlatAppearance.BorderSize = 2;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Location = new System.Drawing.Point(110, 146);
            this.btnConfirm.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.MouseDownImage")));
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.NormalImage")));
            this.btnConfirm.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.OnFocusImage")));
            this.btnConfirm.SelectedImage = ((System.Drawing.Image)(resources.GetObject("btnConfirm.SelectedImage")));
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "Continue...";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(203, 144);
            this.btnCancel.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MouseDownImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.OnFocusImage")));
            this.btnCancel.SelectedImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.SelectedImage")));
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(347, 178);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.txtLogId);
            this.Controls.Add(this.txtAction);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAuth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authentication";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAuth_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private GrbTextBox txtConfirmPassword;
        private System.Windows.Forms.Label label2;
        private GrbTextBox txtAction;
        private GrbTextBox txtLogId;
        private GrbButton btnConfirm;
        private GrbButton btnCancel;
        public GrbTextBox txtRemarks;
    }
}