namespace FA.FORMS
{
    partial class LedgerSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LedgerSearch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtId = new Gramboo.Controls.GrbTextBox();
            this.txthouseName = new Gramboo.Controls.GrbTextBox();
            this.txtledgerName = new Gramboo.Controls.GrbTextBox();
            this.txtLedgerCode = new Gramboo.Controls.GrbTextBox();
            this.txtAddress2 = new Gramboo.Controls.GrbTextBox();
            this.txtPhoneNo = new Gramboo.Controls.GrbTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAddress1 = new Gramboo.Controls.GrbTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Cmb_code = new Gramboo.Controls.GrbComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dgv = new Gramboo.Controls.GrbDataGridView();
            this.cmb_type = new Gramboo.Controls.GrbComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFromDate = new Gramboo.Controls.GrbDTPicker();
            this.dtpToDate = new Gramboo.Controls.GrbDTPicker();
            this.btnSearch = new Gramboo.Controls.GrbButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(1010, 368);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(1033, 366);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(1047, 368);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(1047, 342);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(1010, 342);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(1033, 342);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(1033, 394);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(1036, 394);
            // 
            // txtId
            // 
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtId.Location = new System.Drawing.Point(937, 29);
            this.txtId.Name = "txtId";
            this.txtId.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtId.Size = new System.Drawing.Size(10, 20);
            this.txtId.TabIndex = 19;
            this.txtId.Visible = false;
            // 
            // txthouseName
            // 
            this.txthouseName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txthouseName.Location = new System.Drawing.Point(346, 37);
            this.txthouseName.Name = "txthouseName";
            this.txthouseName.NormalBorderColor = System.Drawing.Color.Gray;
            this.txthouseName.Size = new System.Drawing.Size(104, 20);
            this.txthouseName.TabIndex = 3;
            this.txthouseName.TextChanged += new System.EventHandler(this.txthouseName_TextChanged);
            // 
            // txtledgerName
            // 
            this.txtledgerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtledgerName.Location = new System.Drawing.Point(162, 37);
            this.txtledgerName.Name = "txtledgerName";
            this.txtledgerName.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtledgerName.Size = new System.Drawing.Size(178, 20);
            this.txtledgerName.TabIndex = 2;
            this.txtledgerName.TextChanged += new System.EventHandler(this.txtledgerName_TextChanged);
            // 
            // txtLedgerCode
            // 
            this.txtLedgerCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLedgerCode.Location = new System.Drawing.Point(24, 37);
            this.txtLedgerCode.Name = "txtLedgerCode";
            this.txtLedgerCode.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtLedgerCode.Size = new System.Drawing.Size(131, 20);
            this.txtLedgerCode.TabIndex = 1;
            this.txtLedgerCode.TextChanged += new System.EventHandler(this.txtLedgerCode_TextChanged);
            // 
            // txtAddress2
            // 
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress2.Location = new System.Drawing.Point(577, 37);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtAddress2.Size = new System.Drawing.Size(94, 20);
            this.txtAddress2.TabIndex = 5;
            this.txtAddress2.TextChanged += new System.EventHandler(this.txtAddress2_TextChanged);
            // 
            // txtPhoneNo
            // 
            this.txtPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhoneNo.Location = new System.Drawing.Point(680, 37);
            this.txtPhoneNo.Name = "txtPhoneNo";
            this.txtPhoneNo.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtPhoneNo.Size = new System.Drawing.Size(89, 20);
            this.txtPhoneNo.TabIndex = 6;
            this.txtPhoneNo.TextChanged += new System.EventHandler(this.txtPhoneNo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Ledger Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(165, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 26;
            this.label2.Text = "Ledger Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(343, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "House Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(462, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 28;
            this.label4.Text = "Address1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(678, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 16);
            this.label5.TabIndex = 30;
            this.label5.Text = "Phone No";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(580, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 16);
            this.label6.TabIndex = 29;
            this.label6.Text = "Address 2";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtAddress1
            // 
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress1.Location = new System.Drawing.Point(461, 37);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtAddress1.Size = new System.Drawing.Size(101, 20);
            this.txtAddress1.TabIndex = 4;
            this.txtAddress1.TextChanged += new System.EventHandler(this.txtAddress1_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Cmb_code);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dgv);
            this.groupBox1.Controls.Add(this.cmb_type);
            this.groupBox1.Controls.Add(this.txtLedgerCode);
            this.groupBox1.Controls.Add(this.txtAddress1);
            this.groupBox1.Controls.Add(this.txthouseName);
            this.groupBox1.Controls.Add(this.txtledgerName);
            this.groupBox1.Controls.Add(this.txtAddress2);
            this.groupBox1.Controls.Add(this.txtPhoneNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(979, 558);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // Cmb_code
            // 
            this.Cmb_code.BindingProperty = "SelectedValue";
            this.Cmb_code.DataField = "";
            this.Cmb_code.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Cmb_code.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cmb_code.FormattingEnabled = true;
            this.Cmb_code.Items.AddRange(new object[] {
            "Customers",
            "Supplier",
            "Scheme Customers",
            "Others"});
            this.Cmb_code.Location = new System.Drawing.Point(252, 14);
            this.Cmb_code.Name = "Cmb_code";
            this.Cmb_code.NormalBorderColor = System.Drawing.Color.Gray;
            this.Cmb_code.Size = new System.Drawing.Size(97, 21);
            this.Cmb_code.TabIndex = 38;
            this.Cmb_code.TableName = "";
            this.Cmb_code.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(772, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 37;
            this.label9.Text = "Type";
            // 
            // dgv
            // 
            this.dgv.AllowGrouping = false;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Cursor = System.Windows.Forms.Cursors.Default;
            this.dgv.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.DataFields")));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.DisplaySumRowHeader = false;
            this.dgv.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.FilterList")));
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv.GroupingFields = null;
            this.dgv.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.HiddenDataFields")));
            this.dgv.IsList = true;
            this.dgv.Location = new System.Drawing.Point(14, 63);
            this.dgv.Name = "dgv";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgv.ShowDelete = false;
            this.dgv.ShowEdit = false;
            this.dgv.ShowMoveDown = false;
            this.dgv.ShowMoveUp = false;
            this.dgv.ShowSelectCheckBox = false;
            this.dgv.ShowSerialNo = true;
            this.dgv.Size = new System.Drawing.Size(943, 489);
            this.dgv.SummaryColumns = null;
            this.dgv.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.dgv.SummaryRowSpace = 0;
            this.dgv.SummaryRowVisible = true;
            this.dgv.SumRowHeaderText = null;
            this.dgv.SumRowHeaderTextBold = false;
            this.dgv.TabIndex = 33;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            // 
            // cmb_type
            // 
            this.cmb_type.BindingProperty = "";
            this.cmb_type.DataField = "";
            this.cmb_type.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_type.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_type.FormattingEnabled = true;
            this.cmb_type.Items.AddRange(new object[] {
            "Customers",
            "Supplier",
            "Scheme Customers",
            "Others"});
            this.cmb_type.Location = new System.Drawing.Point(775, 34);
            this.cmb_type.Name = "cmb_type";
            this.cmb_type.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmb_type.Size = new System.Drawing.Size(117, 21);
            this.cmb_type.TabIndex = 36;
            this.cmb_type.TableName = "";
            this.cmb_type.SelectedIndexChanged += new System.EventHandler(this.cmb_type_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(643, 522);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 16);
            this.label8.TabIndex = 35;
            this.label8.Text = "To Date";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(644, 526);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 16);
            this.label7.TabIndex = 34;
            this.label7.Text = "From Date";
            this.label7.Visible = false;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.BindingProperty = "Value";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(688, 522);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.NormalBorderColor = System.Drawing.Color.Gray;
            this.dtpFromDate.Size = new System.Drawing.Size(15, 20);
            this.dtpFromDate.TabIndex = 33;
            this.dtpFromDate.Visible = false;
            this.dtpFromDate.VisibleChanged += new System.EventHandler(this.dtpFromDate_VisibleChanged);
            // 
            // dtpToDate
            // 
            this.dtpToDate.BindingProperty = "Value";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(710, 522);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.NormalBorderColor = System.Drawing.Color.Gray;
            this.dtpToDate.Size = new System.Drawing.Size(12, 20);
            this.dtpToDate.TabIndex = 32;
            this.dtpToDate.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.btnSearch.FlatAppearance.BorderSize = 2;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(915, 19);
            this.btnSearch.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.MouseDownImage")));
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.NormalImage")));
            this.btnSearch.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.OnFocusImage")));
            this.btnSearch.SelectedImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.SelectedImage")));
            this.btnSearch.Size = new System.Drawing.Size(10, 30);
            this.btnSearch.TabIndex = 32;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // LedgerSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 563);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtId);
            this.Name = "LedgerSearch";
            this.Text = "LedgerSearch";
            this.Load += new System.EventHandler(this.LedgerSearch_Load);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtId, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.dtpToDate, 0);
            this.Controls.SetChildIndex(this.dtpFromDate, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gramboo.Controls.GrbTextBox txtId;
        private Gramboo.Controls.GrbTextBox txthouseName;
        private Gramboo.Controls.GrbTextBox txtledgerName;
        private Gramboo.Controls.GrbTextBox txtLedgerCode;
        private Gramboo.Controls.GrbTextBox txtAddress2;
        private Gramboo.Controls.GrbTextBox txtPhoneNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Gramboo.Controls.GrbTextBox txtAddress1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Gramboo.Controls.GrbButton btnSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private Gramboo.Controls.GrbDTPicker dtpFromDate;
        private Gramboo.Controls.GrbDTPicker dtpToDate;
        private System.Windows.Forms.Label label9;
        private Gramboo.Controls.GrbComboBox cmb_type;
        private Gramboo.Controls.GrbComboBox Cmb_code;
        public Gramboo.Controls.GrbDataGridView dgv;
    }
}