namespace SAFA.Forms.STK
{
    partial class PurchaseOrnamentsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseOrnamentsList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grb_list = new Gramboo.Controls.GrbDataGridView();
            this.Dtp_toDate = new Gramboo.Controls.GrbDTPicker();
            this.Dtp_FromDate = new Gramboo.Controls.GrbDTPicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grbButton1 = new Gramboo.Controls.GrbButton();
            ((System.ComponentModel.ISupportInitialize)(this.grb_list)).BeginInit();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(816, 374);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(839, 372);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(853, 374);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(853, 348);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(816, 348);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(839, 348);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(839, 400);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(842, 400);
            // 
            // grb_list
            // 
            this.grb_list.AllowGrouping = false;
            this.grb_list.AllowUserToAddRows = false;
            this.grb_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grb_list.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grb_list.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grb_list.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grb_list.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("grb_list.DataFields")));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grb_list.DefaultCellStyle = dataGridViewCellStyle2;
            this.grb_list.DisplaySumRowHeader = false;
            this.grb_list.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.grb_list.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("grb_list.FilterList")));
            this.grb_list.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grb_list.GroupingFields = null;
            this.grb_list.HeaderHtml = null;
            this.grb_list.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("grb_list.HiddenDataFields")));
            this.grb_list.IsList = true;
            this.grb_list.Location = new System.Drawing.Point(3, 38);
            this.grb_list.Name = "grb_list";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grb_list.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grb_list.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.grb_list.ShowDelete = true;
            this.grb_list.ShowEdit = true;
            this.grb_list.ShowMoveDown = false;
            this.grb_list.ShowMoveUp = false;
            this.grb_list.ShowSelectCheckBox = false;
            this.grb_list.ShowSerialNo = false;
            this.grb_list.Size = new System.Drawing.Size(804, 541);
            this.grb_list.SummaryColumns = null;
            this.grb_list.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.grb_list.SummaryRowSpace = 0;
            this.grb_list.SumRowHeaderText = null;
            this.grb_list.SumRowHeaderTextBold = false;
            this.grb_list.TabIndex = 18;
            this.grb_list.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grbDataGridView1_CellContentClick);
            // 
            // Dtp_toDate
            // 
            this.Dtp_toDate.BindingProperty = "Value";
            this.Dtp_toDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtp_toDate.Location = new System.Drawing.Point(211, 12);
            this.Dtp_toDate.Name = "Dtp_toDate";
            this.Dtp_toDate.NormalBorderColor = System.Drawing.Color.Gray;
            this.Dtp_toDate.Size = new System.Drawing.Size(105, 20);
            this.Dtp_toDate.TabIndex = 275;
            // 
            // Dtp_FromDate
            // 
            this.Dtp_FromDate.BindingProperty = "Value";
            this.Dtp_FromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtp_FromDate.Location = new System.Drawing.Point(54, 12);
            this.Dtp_FromDate.Name = "Dtp_FromDate";
            this.Dtp_FromDate.NormalBorderColor = System.Drawing.Color.Gray;
            this.Dtp_FromDate.Size = new System.Drawing.Size(113, 20);
            this.Dtp_FromDate.TabIndex = 274;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(182, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 16);
            this.label1.TabIndex = 273;
            this.label1.Text = "To";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 16);
            this.label8.TabIndex = 272;
            this.label8.Text = "From";
            // 
            // grbButton1
            // 
            this.grbButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.BackgroundImage")));
            this.grbButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grbButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.grbButton1.FlatAppearance.BorderSize = 2;
            this.grbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grbButton1.Location = new System.Drawing.Point(377, 12);
            this.grbButton1.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.MouseDownImage")));
            this.grbButton1.Name = "grbButton1";
            this.grbButton1.NormalImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.NormalImage")));
            this.grbButton1.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.OnFocusImage")));
            this.grbButton1.SelectedImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.SelectedImage")));
            this.grbButton1.Size = new System.Drawing.Size(75, 23);
            this.grbButton1.TabIndex = 276;
            this.grbButton1.Text = "grbButton1";
            this.grbButton1.UseVisualStyleBackColor = true;
            this.grbButton1.Click += new System.EventHandler(this.grbButton1_Click);
            // 
            // PurchaseOrnamentsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 574);
            this.Controls.Add(this.grbButton1);
            this.Controls.Add(this.Dtp_toDate);
            this.Controls.Add(this.Dtp_FromDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.grb_list);
            this.Name = "PurchaseOrnamentsList";
            this.Text = "PurchaseOrnamentsList";
            this.Controls.SetChildIndex(this.txtDbId, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.grb_list, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.Dtp_FromDate, 0);
            this.Controls.SetChildIndex(this.Dtp_toDate, 0);
            this.Controls.SetChildIndex(this.grbButton1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.grb_list)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gramboo.Controls.GrbDataGridView grb_list; 
        private Gramboo.Controls.GrbDTPicker Dtp_toDate;
        private Gramboo.Controls.GrbDTPicker Dtp_FromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private Gramboo.Controls.GrbButton grbButton1;
    }
}