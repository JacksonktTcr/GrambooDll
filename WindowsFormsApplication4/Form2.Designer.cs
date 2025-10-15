namespace WindowsFormsApplication4
{
    partial class Form2
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
            this.grbTextBox1 = new Gramboo.Controls.GrbTextBox();
            this.grbTextBox2 = new Gramboo.Controls.GrbTextBox();
            this.SuspendLayout();
            // 
            // grbTextBox1
            // 
            this.grbTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grbTextBox1.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.grbTextBox1.Location = new System.Drawing.Point(423, 169);
            this.grbTextBox1.Name = "grbTextBox1";
            this.grbTextBox1.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbTextBox1.Size = new System.Drawing.Size(100, 20);
            this.grbTextBox1.TabIndex = 17;
            // 
            // grbTextBox2
            // 
            this.grbTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grbTextBox2.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.grbTextBox2.Location = new System.Drawing.Point(423, 126);
            this.grbTextBox2.Name = "grbTextBox2";
            this.grbTextBox2.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbTextBox2.Size = new System.Drawing.Size(100, 20);
            this.grbTextBox2.TabIndex = 17;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(986, 528);
            this.Controls.Add(this.grbTextBox2);
            this.Controls.Add(this.grbTextBox1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.grbTextBox1, 0);
            this.Controls.SetChildIndex(this.grbTextBox2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gramboo.Controls.GrbTextBox grbTextBox1;
        private Gramboo.Controls.GrbTextBox grbTextBox2;
    }
}