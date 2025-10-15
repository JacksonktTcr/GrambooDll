namespace JMS.Forms.SALE
{
    partial class SalesGridReport1
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
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(766, 325);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(789, 323);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(803, 325);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(803, 299);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(766, 299);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(789, 299);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(789, 351);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(792, 351);
            // 
            // SalesGridReport1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(759, 476);
            this.Name = "SalesGridReport1";
            this.Text = "Sales Grid Report1";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SalesGridReport1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SalesGridReport1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}
