namespace SAFA.Forms.PUR
{
    partial class FrmLotEntryList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLotEntryList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
           
            this.Dtp_toDate = new Gramboo.Controls.GrbDTPicker();
            this.Dtp_FromDate = new Gramboo.Controls.GrbDTPicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgview = new Gramboo.Controls.GrbDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgview)).BeginInit();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(594, 220);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(617, 218);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(631, 220);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(631, 194);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(594, 194);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(617, 194);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(617, 246);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(620, 246);
            // 
            // refreshButton1
            // 
            
            // Dtp_toDate
            // 
            this.Dtp_toDate.BindingProperty = "Value";
            this.Dtp_toDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtp_toDate.Location = new System.Drawing.Point(212, 10);
            this.Dtp_toDate.Name = "Dtp_toDate";
            this.Dtp_toDate.NormalBorderColor = System.Drawing.Color.Gray;
            this.Dtp_toDate.Size = new System.Drawing.Size(105, 20);
            this.Dtp_toDate.TabIndex = 304;
            // 
            // Dtp_FromDate
            // 
            this.Dtp_FromDate.BindingProperty = "Value";
            this.Dtp_FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtp_FromDate.Location = new System.Drawing.Point(55, 10);
            this.Dtp_FromDate.Name = "Dtp_FromDate";
            this.Dtp_FromDate.NormalBorderColor = System.Drawing.Color.Gray;
            this.Dtp_FromDate.Size = new System.Drawing.Size(113, 20);
            this.Dtp_FromDate.TabIndex = 303;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(183, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 16);
            this.label1.TabIndex = 302;
            this.label1.Text = "To";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 16);
            this.label8.TabIndex = 301;
            this.label8.Text = "From";
            // 
            // dgview
            // 
            this.dgview.AllowGrouping = false;
            this.dgview.AllowUserToAddRows = false;
            this.dgview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgview.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgview.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgview.DataFields")));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgview.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgview.DisplaySumRowHeader = false;
            this.dgview.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("dgview.FilterList")));
            this.dgview.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgview.GroupingFields = null;
            this.dgview.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgview.HiddenDataFields")));
            this.dgview.IsList = true;
            this.dgview.Location = new System.Drawing.Point(2, 41);
            this.dgview.Name = "dgview";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgview.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgview.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgview.ShowDelete = true;
            this.dgview.ShowEdit = true;
            this.dgview.ShowMoveDown = false;
            this.dgview.ShowMoveUp = false;
            this.dgview.ShowSelectCheckBox = false;
            this.dgview.ShowSerialNo = false;
            this.dgview.Size = new System.Drawing.Size(578, 225);
            this.dgview.SummaryColumns = null;
            this.dgview.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.dgview.SummaryRowSpace = 0;
            this.dgview.SumRowHeaderText = null;
            this.dgview.SumRowHeaderTextBold = false;
            this.dgview.TabIndex = 300;
            // 
            // FrmLotEntryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 266); 
            this.Controls.Add(this.Dtp_toDate);
            this.Controls.Add(this.Dtp_FromDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dgview);
            this.Name = "FrmLotEntryList";
            this.Text = "Lot Entry List";
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.dgview, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.Dtp_FromDate, 0);
            this.Controls.SetChildIndex(this.Dtp_toDate, 0); 
            ((System.ComponentModel.ISupportInitialize)(this.dgview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
         
        private Gramboo.Controls.GrbDTPicker Dtp_toDate;
        private Gramboo.Controls.GrbDTPicker Dtp_FromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private Gramboo.Controls.GrbDataGridView dgview;
    }
}