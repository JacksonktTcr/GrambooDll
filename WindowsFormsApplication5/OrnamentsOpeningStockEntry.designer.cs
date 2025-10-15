namespace Kallans.Forms.STK
{
    partial class OrnamentsOpeningStockEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrnamentsOpeningStockEntry));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Dtp_dt = new Gramboo.Controls.GrbDTPicker();
            this.TxtVoucherNo = new Gramboo.Controls.GrbTextBox();
            this.Lbl_voucherNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_EntryNo = new System.Windows.Forms.Label();
            this.txtEntryNo = new Gramboo.Controls.GrbTextBox();
            this.TxtTranscId = new Gramboo.Controls.GrbTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtIsactive = new Gramboo.Controls.GrbTextBox();
            this.cmb_VoucherTypeId = new Gramboo.Controls.GrbComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Lbl_vouctypeId = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_DiaNo = new Gramboo.Controls.GrbTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Diawt = new Gramboo.Controls.GrbTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgv = new Gramboo.Controls.GrbDataGridView();
            this.txtnetwt = new Gramboo.Controls.GrbTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpuritryid = new Gramboo.Controls.GrbTextBox();
            this.cmb_purity = new Gramboo.Controls.GrbComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtItemId = new Gramboo.Controls.GrbTextBox();
            this.cmbItem = new Gramboo.Controls.GrbComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtStonewt = new Gramboo.Controls.GrbTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_add = new Gramboo.Controls.GrbButton();
            this.TxtQty = new Gramboo.Controls.GrbTextBox();
            this.TxtWt = new Gramboo.Controls.GrbTextBox();
            this.lbl_LossWt = new System.Windows.Forms.Label();
            this.lbl_IssueWt = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(1044, 350);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(1067, 348);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(1081, 350);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(1081, 324);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(1044, 324);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(1067, 324);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(1067, 376);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(1070, 376);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Dtp_dt);
            this.groupBox2.Controls.Add(this.TxtVoucherNo);
            this.groupBox2.Controls.Add(this.Lbl_voucherNo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 110);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // Dtp_dt
            // 
            this.Dtp_dt.AcceptBlankValue = false;
            this.Dtp_dt.BindingProperty = "Value";
            this.Dtp_dt.DataField = "VchDate";
            this.Dtp_dt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtp_dt.Location = new System.Drawing.Point(146, 45);
            this.Dtp_dt.Name = "Dtp_dt";
            this.Dtp_dt.NormalBorderColor = System.Drawing.Color.Gray;
            this.Dtp_dt.Size = new System.Drawing.Size(111, 20);
            this.Dtp_dt.TabIndex = 1;
            // 
            // TxtVoucherNo
            // 
            this.TxtVoucherNo.ActiveBackColor = System.Drawing.Color.LightGray;
            this.TxtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtVoucherNo.DataField = "VchNo";
            this.TxtVoucherNo.IsIDField = true;
            this.TxtVoucherNo.Location = new System.Drawing.Point(146, 18);
            this.TxtVoucherNo.Name = "TxtVoucherNo";
            this.TxtVoucherNo.NormalBackColor = System.Drawing.Color.LightGray;
            this.TxtVoucherNo.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtVoucherNo.ReadOnly = true;
            this.TxtVoucherNo.Size = new System.Drawing.Size(195, 20);
            this.TxtVoucherNo.TabIndex = 0;
            this.TxtVoucherNo.TabStop = false;
            this.TxtVoucherNo.TextChanged += new System.EventHandler(this.TxtVoucherNo_TextChanged);
            // 
            // Lbl_voucherNo
            // 
            this.Lbl_voucherNo.AutoSize = true;
            this.Lbl_voucherNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_voucherNo.Location = new System.Drawing.Point(16, 18);
            this.Lbl_voucherNo.Name = "Lbl_voucherNo";
            this.Lbl_voucherNo.Size = new System.Drawing.Size(79, 16);
            this.Lbl_voucherNo.TabIndex = 49;
            this.Lbl_voucherNo.Text = "Voucher No";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.label2.TabIndex = 51;
            this.label2.Text = "Voucher Date";
            // 
            // lbl_EntryNo
            // 
            this.lbl_EntryNo.AutoSize = true;
            this.lbl_EntryNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EntryNo.Location = new System.Drawing.Point(428, 82);
            this.lbl_EntryNo.Name = "lbl_EntryNo";
            this.lbl_EntryNo.Size = new System.Drawing.Size(62, 16);
            this.lbl_EntryNo.TabIndex = 117;
            this.lbl_EntryNo.Text = " Entry No";
            this.lbl_EntryNo.Visible = false;
            // 
            // txtEntryNo
            // 
            this.txtEntryNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEntryNo.DataField = "EntryId";
            this.txtEntryNo.IsIDField = true;
            this.txtEntryNo.Location = new System.Drawing.Point(543, 82);
            this.txtEntryNo.Name = "txtEntryNo";
            this.txtEntryNo.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtEntryNo.Size = new System.Drawing.Size(115, 20);
            this.txtEntryNo.TabIndex = 118;
            this.txtEntryNo.Visible = false;
            // 
            // TxtTranscId
            // 
            this.TxtTranscId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtTranscId.DataField = "TransId";
            this.TxtTranscId.IsIDField = true;
            this.TxtTranscId.Location = new System.Drawing.Point(543, 3);
            this.TxtTranscId.Name = "TxtTranscId";
            this.TxtTranscId.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtTranscId.Size = new System.Drawing.Size(115, 20);
            this.TxtTranscId.TabIndex = 119;
            this.TxtTranscId.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(432, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 16);
            this.label6.TabIndex = 120;
            this.label6.Text = "Is Active";
            this.label6.Visible = false;
            // 
            // TxtIsactive
            // 
            this.TxtIsactive.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtIsactive.DataField = "IsActive";
            this.TxtIsactive.Location = new System.Drawing.Point(543, 56);
            this.TxtIsactive.Name = "TxtIsactive";
            this.TxtIsactive.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtIsactive.Size = new System.Drawing.Size(115, 20);
            this.TxtIsactive.TabIndex = 121;
            this.TxtIsactive.Visible = false;
            // 
            // cmb_VoucherTypeId
            // 
            this.cmb_VoucherTypeId.BindingProperty = "SelectedValue";
            this.cmb_VoucherTypeId.DataField = "VoucherTypeId";
            this.cmb_VoucherTypeId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_VoucherTypeId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_VoucherTypeId.FormattingEnabled = true;
            this.cmb_VoucherTypeId.Location = new System.Drawing.Point(543, 29);
            this.cmb_VoucherTypeId.Name = "cmb_VoucherTypeId";
            this.cmb_VoucherTypeId.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmb_VoucherTypeId.Size = new System.Drawing.Size(115, 21);
            this.cmb_VoucherTypeId.TabIndex = 123;
            this.cmb_VoucherTypeId.TableName = "";
            this.cmb_VoucherTypeId.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(430, 7);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 16);
            this.label8.TabIndex = 122;
            this.label8.Text = "Transaction Id";
            this.label8.Visible = false;
            // 
            // Lbl_vouctypeId
            // 
            this.Lbl_vouctypeId.AutoSize = true;
            this.Lbl_vouctypeId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_vouctypeId.Location = new System.Drawing.Point(430, 30);
            this.Lbl_vouctypeId.Name = "Lbl_vouctypeId";
            this.Lbl_vouctypeId.Size = new System.Drawing.Size(103, 16);
            this.Lbl_vouctypeId.TabIndex = 124;
            this.Lbl_vouctypeId.Text = "VoucherTypeID";
            this.Lbl_vouctypeId.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txt_DiaNo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_Diawt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dgv);
            this.groupBox1.Controls.Add(this.txtnetwt);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtpuritryid);
            this.groupBox1.Controls.Add(this.cmb_purity);
            this.groupBox1.Controls.Add(this.label30);
            this.groupBox1.Controls.Add(this.txtItemId);
            this.groupBox1.Controls.Add(this.cmbItem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TxtStonewt);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btn_add);
            this.groupBox1.Controls.Add(this.TxtQty);
            this.groupBox1.Controls.Add(this.TxtWt);
            this.groupBox1.Controls.Add(this.lbl_LossWt);
            this.groupBox1.Controls.Add(this.lbl_IssueWt);
            this.groupBox1.Location = new System.Drawing.Point(1, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1032, 408);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // txt_DiaNo
            // 
            this.txt_DiaNo.AcceptBlankValue = false;
            this.txt_DiaNo.ActiveBackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_DiaNo.BackColor = System.Drawing.Color.White;
            this.txt_DiaNo.BindToDataGridview = true;
            this.txt_DiaNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_DiaNo.DataField = "DiaNo";
            this.txt_DiaNo.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txt_DiaNo.Location = new System.Drawing.Point(483, 35);
            this.txt_DiaNo.Name = "txt_DiaNo";
            this.txt_DiaNo.NormalBorderColor = System.Drawing.Color.Gray;
            this.txt_DiaNo.Size = new System.Drawing.Size(66, 20);
            this.txt_DiaNo.TabIndex = 4;
            this.txt_DiaNo.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(494, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 16);
            this.label5.TabIndex = 263;
            this.label5.Text = "DiaNo";
            // 
            // txt_Diawt
            // 
            this.txt_Diawt.AcceptBlankValue = false;
            this.txt_Diawt.ActiveBackColor = System.Drawing.SystemColors.HighlightText;
            this.txt_Diawt.BackColor = System.Drawing.Color.White;
            this.txt_Diawt.BindToDataGridview = true;
            this.txt_Diawt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Diawt.DataField = "DiaWt";
            this.txt_Diawt.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txt_Diawt.Location = new System.Drawing.Point(554, 36);
            this.txt_Diawt.Name = "txt_Diawt";
            this.txt_Diawt.NormalBorderColor = System.Drawing.Color.Gray;
            this.txt_Diawt.Size = new System.Drawing.Size(72, 20);
            this.txt_Diawt.TabIndex = 5;
            this.txt_Diawt.Text = "0";
            this.txt_Diawt.TextChanged += new System.EventHandler(this.txt_Diawt_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(557, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 261;
            this.label4.Text = "DiaWt";
            // 
            // dgv
            // 
            this.dgv.AllowEmptyRows = false;
            this.dgv.AllowGrouping = false;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.DataFields")));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.DisplaySumRowHeader = true;
            this.dgv.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.FilterList")));
            this.dgv.FormatString = "F03";
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv.GroupingFields = null;
            this.dgv.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.HiddenDataFields")));
            this.dgv.Location = new System.Drawing.Point(6, 62);
            this.dgv.Name = "dgv";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgv.ShowDelete = true;
            this.dgv.ShowEdit = true;
            this.dgv.ShowMoveDown = false;
            this.dgv.ShowMoveUp = false;
            this.dgv.ShowSelectCheckBox = false;
            this.dgv.ShowSerialNo = true;
            this.dgv.Size = new System.Drawing.Size(1038, 340);
            this.dgv.SummaryColumns = null;
            this.dgv.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.dgv.SummaryRowSpace = 0;
            this.dgv.SummaryRowVisible = true;
            this.dgv.SumRowHeaderText = "Total";
            this.dgv.SumRowHeaderTextBold = false;
            this.dgv.TabIndex = 2;
            this.dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentClick);
            // 
            // txtnetwt
            // 
            this.txtnetwt.ActiveBackColor = System.Drawing.SystemColors.HighlightText;
            this.txtnetwt.BackColor = System.Drawing.Color.White;
            this.txtnetwt.BindToDataGridview = true;
            this.txtnetwt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtnetwt.DataField = "NetWt";
            this.txtnetwt.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtnetwt.Location = new System.Drawing.Point(711, 36);
            this.txtnetwt.Name = "txtnetwt";
            this.txtnetwt.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtnetwt.ReadOnly = true;
            this.txtnetwt.Size = new System.Drawing.Size(73, 20);
            this.txtnetwt.TabIndex = 7;
            this.txtnetwt.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(714, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 16);
            this.label3.TabIndex = 259;
            this.label3.Text = "NetWt";
            // 
            // txtpuritryid
            // 
            this.txtpuritryid.BindToDataGridview = true;
            this.txtpuritryid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtpuritryid.DataField = "PurityId";
            this.txtpuritryid.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtpuritryid.Location = new System.Drawing.Point(303, 13);
            this.txtpuritryid.Name = "txtpuritryid";
            this.txtpuritryid.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtpuritryid.Size = new System.Drawing.Size(25, 20);
            this.txtpuritryid.TabIndex = 257;
            this.txtpuritryid.Text = "0";
            this.txtpuritryid.Visible = false;
            // 
            // cmb_purity
            // 
            this.cmb_purity.AcceptBlankValue = false;
            this.cmb_purity.BindToDataGridview = true;
            this.cmb_purity.DataField = "Purity";
            this.cmb_purity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_purity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_purity.FormattingEnabled = true;
            this.cmb_purity.Location = new System.Drawing.Point(235, 35);
            this.cmb_purity.Name = "cmb_purity";
            this.cmb_purity.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmb_purity.Size = new System.Drawing.Size(98, 21);
            this.cmb_purity.TabIndex = 1;
            this.cmb_purity.TableName = "";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(256, 17);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(41, 16);
            this.label30.TabIndex = 253;
            this.label30.Text = "Purity";
            // 
            // txtItemId
            // 
            this.txtItemId.AcceptBlankValue = false;
            this.txtItemId.BindToDataGridview = true;
            this.txtItemId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItemId.DataField = "ItemId";
            this.txtItemId.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtItemId.Location = new System.Drawing.Point(169, 13);
            this.txtItemId.Name = "txtItemId";
            this.txtItemId.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtItemId.Size = new System.Drawing.Size(41, 20);
            this.txtItemId.TabIndex = 252;
            this.txtItemId.Text = "0";
            this.txtItemId.Visible = false;
            // 
            // cmbItem
            // 
            this.cmbItem.AcceptBlankValue = false;
            this.cmbItem.BindToDataGridview = true;
            this.cmbItem.DataField = "Item Name";
            this.cmbItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbItem.FormattingEnabled = true;
            this.cmbItem.Location = new System.Drawing.Point(88, 35);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmbItem.Size = new System.Drawing.Size(144, 21);
            this.cmbItem.TabIndex = 0;
            this.cmbItem.TableName = "";
            this.cmbItem.SelectedIndexChanged += new System.EventHandler(this.cmbItem_SelectedIndexChanged);
            this.cmbItem.SelectedValueChanged += new System.EventHandler(this.cmbItem_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(91, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 251;
            this.label1.Text = "Item Name";
            // 
            // TxtStonewt
            // 
            this.TxtStonewt.ActiveBackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtStonewt.BackColor = System.Drawing.Color.White;
            this.TxtStonewt.BindToDataGridview = true;
            this.TxtStonewt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtStonewt.DataField = "StoneWt";
            this.TxtStonewt.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtStonewt.Location = new System.Drawing.Point(630, 36);
            this.TxtStonewt.Name = "TxtStonewt";
            this.TxtStonewt.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtStonewt.Size = new System.Drawing.Size(77, 20);
            this.TxtStonewt.TabIndex = 6;
            this.TxtStonewt.Text = "0";
            this.TxtStonewt.TextChanged += new System.EventHandler(this.TxtStonewt_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(634, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 16);
            this.label10.TabIndex = 114;
            this.label10.Text = "StoneWt";
            // 
            // btn_add
            // 
            this.btn_add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_add.BackgroundImage")));
            this.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_add.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.btn_add.FlatAppearance.BorderSize = 2;
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Location = new System.Drawing.Point(791, 33);
            this.btn_add.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("btn_add.MouseDownImage")));
            this.btn_add.Name = "btn_add";
            this.btn_add.NormalImage = ((System.Drawing.Image)(resources.GetObject("btn_add.NormalImage")));
            this.btn_add.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("btn_add.OnFocusImage")));
            this.btn_add.SelectedImage = ((System.Drawing.Image)(resources.GetObject("btn_add.SelectedImage")));
            this.btn_add.Size = new System.Drawing.Size(30, 23);
            this.btn_add.TabIndex = 8;
            this.btn_add.Text = "+";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // TxtQty
            // 
            this.TxtQty.BindToDataGridview = true;
            this.TxtQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtQty.DataField = "Qty";
            this.TxtQty.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtQty.Location = new System.Drawing.Point(336, 36);
            this.TxtQty.Name = "TxtQty";
            this.TxtQty.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtQty.Size = new System.Drawing.Size(69, 20);
            this.TxtQty.TabIndex = 2;
            this.TxtQty.Text = "0";
            // 
            // TxtWt
            // 
            this.TxtWt.AcceptBlankValue = false;
            this.TxtWt.ActiveBackColor = System.Drawing.SystemColors.HighlightText;
            this.TxtWt.BackColor = System.Drawing.Color.White;
            this.TxtWt.BindToDataGridview = true;
            this.TxtWt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtWt.DataField = "Wt";
            this.TxtWt.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtWt.Location = new System.Drawing.Point(407, 36);
            this.TxtWt.Name = "TxtWt";
            this.TxtWt.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtWt.Size = new System.Drawing.Size(72, 20);
            this.TxtWt.TabIndex = 3;
            this.TxtWt.Text = "0";
            this.TxtWt.TextChanged += new System.EventHandler(this.TxtWt_TextChanged);
            // 
            // lbl_LossWt
            // 
            this.lbl_LossWt.AutoSize = true;
            this.lbl_LossWt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_LossWt.Location = new System.Drawing.Point(411, 17);
            this.lbl_LossWt.Name = "lbl_LossWt";
            this.lbl_LossWt.Size = new System.Drawing.Size(24, 16);
            this.lbl_LossWt.TabIndex = 29;
            this.lbl_LossWt.Text = "Wt";
            // 
            // lbl_IssueWt
            // 
            this.lbl_IssueWt.AutoSize = true;
            this.lbl_IssueWt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_IssueWt.Location = new System.Drawing.Point(340, 17);
            this.lbl_IssueWt.Name = "lbl_IssueWt";
            this.lbl_IssueWt.Size = new System.Drawing.Size(28, 16);
            this.lbl_IssueWt.TabIndex = 29;
            this.lbl_IssueWt.Text = "Qty";
            // 
            // OrnamentsOpeningStockEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1037, 527);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_EntryNo);
            this.Controls.Add(this.txtEntryNo);
            this.Controls.Add(this.TxtTranscId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TxtIsactive);
            this.Controls.Add(this.cmb_VoucherTypeId);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Lbl_vouctypeId);
            this.Controls.Add(this.groupBox2);
            this.Name = "OrnamentsOpeningStockEntry";
            this.Text = "Ornaments Opening Stock Entry";
            this.Load += new System.EventHandler(this.OrnamentsOpeningStockEntry_Load);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.Lbl_vouctypeId, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.cmb_VoucherTypeId, 0);
            this.Controls.SetChildIndex(this.TxtIsactive, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.TxtTranscId, 0);
            this.Controls.SetChildIndex(this.txtEntryNo, 0);
            this.Controls.SetChildIndex(this.lbl_EntryNo, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private Gramboo.Controls.GrbDTPicker Dtp_dt;
        private Gramboo.Controls.GrbTextBox TxtVoucherNo;
        private System.Windows.Forms.Label Lbl_voucherNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_EntryNo;
        private Gramboo.Controls.GrbTextBox txtEntryNo;
        private Gramboo.Controls.GrbTextBox TxtTranscId;
        private System.Windows.Forms.Label label6;
        private Gramboo.Controls.GrbTextBox TxtIsactive;
        private Gramboo.Controls.GrbComboBox cmb_VoucherTypeId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label Lbl_vouctypeId;
        private System.Windows.Forms.GroupBox groupBox1;
        private Gramboo.Controls.GrbTextBox txtItemId;
        private Gramboo.Controls.GrbComboBox cmbItem;
        private System.Windows.Forms.Label label1;
        private Gramboo.Controls.GrbTextBox TxtStonewt;
        private System.Windows.Forms.Label label10;
        private Gramboo.Controls.GrbButton btn_add;
        private Gramboo.Controls.GrbTextBox TxtQty;
        private Gramboo.Controls.GrbTextBox TxtWt;
        private System.Windows.Forms.Label lbl_LossWt;
        private System.Windows.Forms.Label lbl_IssueWt;
        private Gramboo.Controls.GrbTextBox txtpuritryid;
        private Gramboo.Controls.GrbComboBox cmb_purity;
        private System.Windows.Forms.Label label30;
        private Gramboo.Controls.GrbTextBox txtnetwt;
        private System.Windows.Forms.Label label3;
        private Gramboo.Controls.GrbDataGridView dgv;
        private Gramboo.Controls.GrbTextBox txt_Diawt;
        private System.Windows.Forms.Label label4;
        private Gramboo.Controls.GrbTextBox txt_DiaNo;
        private System.Windows.Forms.Label label5;
    }
}
