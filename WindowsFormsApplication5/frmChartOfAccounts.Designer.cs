namespace JMS.Forms.ACC
{
    partial class frmChartOfAccounts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChartOfAccounts));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAccounts1 = new JMS.Classes.dgvAccounts();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAccounts1
            // 
            this.dgvAccounts1.AllowGrouping = false;
            this.dgvAccounts1.AllowUserToAddRows = false;
            this.dgvAccounts1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvAccounts1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccounts1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAccounts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccounts1.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvAccounts1.DataFields")));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAccounts1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAccounts1.DisplaySumRowHeader = false;
            this.dgvAccounts1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAccounts1.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvAccounts1.FilterList")));
            this.dgvAccounts1.FormatString = "F02";
            this.dgvAccounts1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvAccounts1.GroupingFields = null;
            this.dgvAccounts1.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvAccounts1.HiddenDataFields")));
            this.dgvAccounts1.Location = new System.Drawing.Point(0, 0);
            this.dgvAccounts1.MergeColumns = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvAccounts1.MergeColumns")));
            this.dgvAccounts1.Name = "dgvAccounts1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccounts1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAccounts1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgvAccounts1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccounts1.ShowDelete = false;
            this.dgvAccounts1.ShowEdit = false;
            this.dgvAccounts1.ShowMoveDown = false;
            this.dgvAccounts1.ShowMoveUp = false;
            this.dgvAccounts1.ShowSelectCheckBox = false;
            this.dgvAccounts1.ShowSerialNo = false;
            this.dgvAccounts1.Size = new System.Drawing.Size(626, 444);
            this.dgvAccounts1.SummaryColumns = null;
            this.dgvAccounts1.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.dgvAccounts1.SummaryRowSpace = 0;
            this.dgvAccounts1.SumRowHeaderText = null;
            this.dgvAccounts1.SumRowHeaderTextBold = false;
            this.dgvAccounts1.TabIndex = 17;
            this.dgvAccounts1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccounts1_CellDoubleClick);
            // 
            // frmChartOfAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(626, 444);
            this.Controls.Add(this.dgvAccounts1);
            this.Name = "frmChartOfAccounts";
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.dgvAccounts1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccounts1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Classes.dgvAccounts dgvAccounts1;
    }
}
