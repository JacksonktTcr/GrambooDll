
namespace WindowsFormsApplication5
{
    partial class checkdgv
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(checkdgv));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgviewPending = new Gramboo.Controls.GrbDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgviewPending)).BeginInit();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(684, 288);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(707, 286);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(721, 288);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(721, 262);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(684, 262);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(707, 262);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(707, 314);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(710, 314);
            // 
            // txtDbId
            // 
            this.txtDbId.Location = new System.Drawing.Point(288, 215);
            // 
            // dgviewPending
            // 
            this.dgviewPending.AllowGrouping = false;
            this.dgviewPending.AllowUserToAddRows = false;
            this.dgviewPending.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgviewPending.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgviewPending.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgviewPending.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgviewPending.DataFields")));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgviewPending.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgviewPending.DisplaySumRowHeader = false;
            this.dgviewPending.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("dgviewPending.FilterList")));
            this.dgviewPending.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgviewPending.GroupingFields = null;
            this.dgviewPending.HeaderHtml = null;
            this.dgviewPending.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgviewPending.HiddenDataFields")));
            this.dgviewPending.Location = new System.Drawing.Point(57, 73);
            this.dgviewPending.Name = "dgviewPending";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgviewPending.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgviewPending.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgviewPending.ShowDelete = false;
            this.dgviewPending.ShowEdit = false;
            this.dgviewPending.ShowMoveDown = false;
            this.dgviewPending.ShowMoveUp = false;
            this.dgviewPending.ShowSelectCheckBox = false;
            this.dgviewPending.ShowSerialNo = false;
            this.dgviewPending.Size = new System.Drawing.Size(518, 235);
            this.dgviewPending.SummaryColumns = null;
            this.dgviewPending.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.dgviewPending.SummaryRowSpace = 0;
            this.dgviewPending.SumRowHeaderText = null;
            this.dgviewPending.SumRowHeaderTextBold = false;
            this.dgviewPending.TabIndex = 20;
            // 
            // checkdgv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgviewPending);
            this.Name = "checkdgv";
            this.Text = "checkdgv";
            this.Load += new System.EventHandler(this.Form6_Load);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.txtDbId, 0);
            this.Controls.SetChildIndex(this.dgviewPending, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgviewPending)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gramboo.Controls.GrbDataGridView dgviewPending;
    }
}