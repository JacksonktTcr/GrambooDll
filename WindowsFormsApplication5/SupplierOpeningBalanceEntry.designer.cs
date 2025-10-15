namespace JMS.Forms.PUR
{
    partial class SupplierOpeningBalanceEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SupplierOpeningBalanceEntry));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmb_supplier = new Gramboo.Controls.GrbComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_EntryNo = new System.Windows.Forms.Label();
            this.TxtEntryNo = new Gramboo.Controls.GrbTextBox();
            this.grbDTPicker1 = new Gramboo.Controls.GrbDTPicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.TxtIsactive = new Gramboo.Controls.GrbTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grbDTPicker3 = new Gramboo.Controls.GrbDTPicker();
            this.label12 = new System.Windows.Forms.Label();
            this.txtstaus = new Gramboo.Controls.GrbCheckBox();
            this.grbDTPicker2 = new Gramboo.Controls.GrbDTPicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBillNo = new Gramboo.Controls.GrbTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCash = new Gramboo.Controls.GrbTextBox();
            this.btn_add = new Gramboo.Controls.GrbButton();
            this.dgv = new Gramboo.Controls.GrbDataGridView();
            this.txtWt = new Gramboo.Controls.GrbTextBox();
            this.Lbl_ReceiptWt = new System.Windows.Forms.Label();
            this.TxtTranscId = new Gramboo.Controls.GrbTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtwtbal = new Gramboo.Controls.GrbTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtcashbal = new Gramboo.Controls.GrbTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtWstBal = new Gramboo.Controls.GrbTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMcBal = new Gramboo.Controls.GrbTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtStoneBal = new Gramboo.Controls.GrbTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDiaBal = new Gramboo.Controls.GrbTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(998, 309);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(1021, 307);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(1035, 309);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(1035, 283);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(998, 283);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(1021, 283);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(1021, 335);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(1024, 335);
            // 
            // cmb_supplier
            // 
            this.cmb_supplier.AcceptBlankValue = false;
            this.cmb_supplier.BindingProperty = "SelectedValue";
            this.cmb_supplier.BindToDataGridview = true;
            this.cmb_supplier.DataField = "SuppId";
            this.cmb_supplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_supplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_supplier.FormattingEnabled = true;
            this.cmb_supplier.Location = new System.Drawing.Point(135, 11);
            this.cmb_supplier.Name = "cmb_supplier";
            this.cmb_supplier.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmb_supplier.Size = new System.Drawing.Size(186, 21);
            this.cmb_supplier.TabIndex = 0;
            this.cmb_supplier.TableName = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 16);
            this.label5.TabIndex = 139;
            this.label5.Text = "Supplier Name";
            // 
            // lbl_EntryNo
            // 
            this.lbl_EntryNo.AutoSize = true;
            this.lbl_EntryNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EntryNo.Location = new System.Drawing.Point(830, 82);
            this.lbl_EntryNo.Name = "lbl_EntryNo";
            this.lbl_EntryNo.Size = new System.Drawing.Size(62, 16);
            this.lbl_EntryNo.TabIndex = 148;
            this.lbl_EntryNo.Text = " Entry No";
            this.lbl_EntryNo.Visible = false;
            // 
            // TxtEntryNo
            // 
            this.TxtEntryNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtEntryNo.DataField = "EntryId";
            this.TxtEntryNo.IsIDField = true;
            this.TxtEntryNo.Location = new System.Drawing.Point(930, 80);
            this.TxtEntryNo.Name = "TxtEntryNo";
            this.TxtEntryNo.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtEntryNo.Size = new System.Drawing.Size(87, 20);
            this.TxtEntryNo.TabIndex = 149;
            this.TxtEntryNo.Visible = false;
            // 
            // grbDTPicker1
            // 
            this.grbDTPicker1.AcceptBlankValue = false;
            this.grbDTPicker1.BindingProperty = "Value";
            this.grbDTPicker1.DataField = "OpnDate";
            this.grbDTPicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.grbDTPicker1.Location = new System.Drawing.Point(135, 39);
            this.grbDTPicker1.Name = "grbDTPicker1";
            this.grbDTPicker1.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbDTPicker1.Size = new System.Drawing.Size(120, 20);
            this.grbDTPicker1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 16);
            this.label2.TabIndex = 153;
            this.label2.Text = "Opening Date";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(830, 106);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 16);
            this.label18.TabIndex = 161;
            this.label18.Text = "Is Active";
            this.label18.Visible = false;
            // 
            // TxtIsactive
            // 
            this.TxtIsactive.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtIsactive.DataField = "IsActive";
            this.TxtIsactive.Location = new System.Drawing.Point(930, 106);
            this.TxtIsactive.Name = "TxtIsactive";
            this.TxtIsactive.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtIsactive.Size = new System.Drawing.Size(87, 20);
            this.TxtIsactive.TabIndex = 162;
            this.TxtIsactive.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.grbDTPicker3);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtstaus);
            this.groupBox1.Controls.Add(this.grbDTPicker2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBillNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCash);
            this.groupBox1.Controls.Add(this.btn_add);
            this.groupBox1.Controls.Add(this.dgv);
            this.groupBox1.Controls.Add(this.txtWt);
            this.groupBox1.Controls.Add(this.Lbl_ReceiptWt);
            this.groupBox1.Location = new System.Drawing.Point(15, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(964, 376);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // grbDTPicker3
            // 
            this.grbDTPicker3.AcceptBlankValue = false;
            this.grbDTPicker3.Alias = "";
            this.grbDTPicker3.BindingProperty = "Value";
            this.grbDTPicker3.BindToDataGridview = true;
            this.grbDTPicker3.DataField = "DueDate";
            this.grbDTPicker3.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.grbDTPicker3.Location = new System.Drawing.Point(266, 42);
            this.grbDTPicker3.Name = "grbDTPicker3";
            this.grbDTPicker3.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbDTPicker3.Size = new System.Drawing.Size(120, 20);
            this.grbDTPicker3.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoEllipsis = true;
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(263, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 16);
            this.label12.TabIndex = 158;
            this.label12.Text = "Due Date";
            // 
            // txtstaus
            // 
            this.txtstaus.AcceptBlankValue = false;
            this.txtstaus.Alias = "Settlement Status";
            this.txtstaus.AutoSize = true;
            this.txtstaus.BindingProperty = "Checked";
            this.txtstaus.Checked = true;
            this.txtstaus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtstaus.DataField = "SettlementStatus";
            this.txtstaus.FalseValue = "0";
            this.txtstaus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtstaus.Location = new System.Drawing.Point(805, 36);
            this.txtstaus.Name = "txtstaus";
            this.txtstaus.Size = new System.Drawing.Size(130, 20);
            this.txtstaus.TabIndex = 156;
            this.txtstaus.TableName = null;
            this.txtstaus.Text = "Settlement Status";
            this.txtstaus.TrueValue = "1";
            this.txtstaus.UseVisualStyleBackColor = true;
            this.txtstaus.Value = "1";
            this.txtstaus.Visible = false;
            // 
            // grbDTPicker2
            // 
            this.grbDTPicker2.AcceptBlankValue = false;
            this.grbDTPicker2.Alias = "";
            this.grbDTPicker2.BindingProperty = "Value";
            this.grbDTPicker2.BindToDataGridview = true;
            this.grbDTPicker2.DataField = "BillDate";
            this.grbDTPicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.grbDTPicker2.Location = new System.Drawing.Point(23, 43);
            this.grbDTPicker2.Name = "grbDTPicker2";
            this.grbDTPicker2.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbDTPicker2.Size = new System.Drawing.Size(120, 20);
            this.grbDTPicker2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 155;
            this.label4.Text = "Bill Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(146, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 102;
            this.label1.Text = "Bill No";
            // 
            // txtBillNo
            // 
            this.txtBillNo.AcceptBlankValue = false;
            this.txtBillNo.Alias = "";
            this.txtBillNo.BindToDataGridview = true;
            this.txtBillNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBillNo.DataField = "BillNo";
            this.txtBillNo.InputMask = Gramboo.Controls.GrbTextBox.Mask.Digit;
            this.txtBillNo.Location = new System.Drawing.Point(149, 42);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtBillNo.Size = new System.Drawing.Size(114, 20);
            this.txtBillNo.TabIndex = 1;
            this.txtBillNo.Text = "0";
            this.txtBillNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(469, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 16);
            this.label3.TabIndex = 47;
            this.label3.Text = "Cash";
            // 
            // txtCash
            // 
            this.txtCash.AcceptBlankValue = false;
            this.txtCash.BindToDataGridview = true;
            this.txtCash.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCash.DataField = "Cash";
            this.txtCash.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtCash.Location = new System.Drawing.Point(469, 42);
            this.txtCash.Name = "txtCash";
            this.txtCash.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtCash.Size = new System.Drawing.Size(75, 20);
            this.txtCash.TabIndex = 4;
            this.txtCash.Text = "0";
            this.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_add
            // 
            this.btn_add.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_add.BackgroundImage")));
            this.btn_add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_add.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.btn_add.FlatAppearance.BorderSize = 2;
            this.btn_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Location = new System.Drawing.Point(550, 41);
            this.btn_add.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("btn_add.MouseDownImage")));
            this.btn_add.Name = "btn_add";
            this.btn_add.NormalImage = ((System.Drawing.Image)(resources.GetObject("btn_add.NormalImage")));
            this.btn_add.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("btn_add.OnFocusImage")));
            this.btn_add.SelectedImage = ((System.Drawing.Image)(resources.GetObject("btn_add.SelectedImage")));
            this.btn_add.Size = new System.Drawing.Size(36, 23);
            this.btn_add.TabIndex = 5;
            this.btn_add.Text = "+";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
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
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv.GroupingFields = null;
            this.dgv.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.HiddenDataFields")));
            this.dgv.Location = new System.Drawing.Point(6, 67);
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
            this.dgv.Size = new System.Drawing.Size(954, 300);
            this.dgv.SummaryColumns = null;
            this.dgv.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.dgv.SummaryRowSpace = 0;
            this.dgv.SummaryRowVisible = true;
            this.dgv.SumRowHeaderText = "Total";
            this.dgv.SumRowHeaderTextBold = false;
            this.dgv.TabIndex = 38;
            this.dgv.BeforeEdit += new Gramboo.Controls.BeforeEditEventHandler(this.dgv_BeforeEdit);
            this.dgv.BeforeEditRow += new Gramboo.Controls.BeforeEditRowEventHandler(this.dgv_BeforeEditRow);
            // 
            // txtWt
            // 
            this.txtWt.AcceptBlankValue = false;
            this.txtWt.BindToDataGridview = true;
            this.txtWt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWt.DataField = "Wt";
            this.txtWt.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtWt.Location = new System.Drawing.Point(388, 42);
            this.txtWt.Name = "txtWt";
            this.txtWt.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtWt.Size = new System.Drawing.Size(75, 20);
            this.txtWt.TabIndex = 3;
            this.txtWt.Text = "0";
            this.txtWt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Lbl_ReceiptWt
            // 
            this.Lbl_ReceiptWt.AutoSize = true;
            this.Lbl_ReceiptWt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_ReceiptWt.Location = new System.Drawing.Point(385, 23);
            this.Lbl_ReceiptWt.Name = "Lbl_ReceiptWt";
            this.Lbl_ReceiptWt.Size = new System.Drawing.Size(24, 16);
            this.Lbl_ReceiptWt.TabIndex = 35;
            this.Lbl_ReceiptWt.Text = "Wt";
            // 
            // TxtTranscId
            // 
            this.TxtTranscId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtTranscId.DataField = "TransId";
            this.TxtTranscId.IsIDField = true;
            this.TxtTranscId.Location = new System.Drawing.Point(933, 53);
            this.TxtTranscId.Name = "TxtTranscId";
            this.TxtTranscId.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtTranscId.Size = new System.Drawing.Size(115, 20);
            this.TxtTranscId.TabIndex = 164;
            this.TxtTranscId.Visible = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(830, 57);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(93, 16);
            this.label20.TabIndex = 165;
            this.label20.Text = "Transaction Id";
            this.label20.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 16);
            this.label6.TabIndex = 158;
            this.label6.Text = "Wt Balance";
            this.label6.Visible = false;
            // 
            // txtwtbal
            // 
            this.txtwtbal.AcceptBlankValue = false;
            this.txtwtbal.Alias = "";
            this.txtwtbal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtwtbal.DataField = "WtBalance";
            this.txtwtbal.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtwtbal.Location = new System.Drawing.Point(136, 60);
            this.txtwtbal.Name = "txtwtbal";
            this.txtwtbal.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtwtbal.Size = new System.Drawing.Size(114, 20);
            this.txtwtbal.TabIndex = 3;
            this.txtwtbal.Text = "0";
            this.txtwtbal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtwtbal.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(94, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 16);
            this.label7.TabIndex = 167;
            this.label7.Text = "Cash Balance";
            this.label7.Visible = false;
            // 
            // txtcashbal
            // 
            this.txtcashbal.AcceptBlankValue = false;
            this.txtcashbal.Alias = "";
            this.txtcashbal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcashbal.DataField = "CashBalance";
            this.txtcashbal.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtcashbal.Location = new System.Drawing.Point(217, 65);
            this.txtcashbal.Name = "txtcashbal";
            this.txtcashbal.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtcashbal.Size = new System.Drawing.Size(114, 20);
            this.txtcashbal.TabIndex = 2;
            this.txtcashbal.Text = "0";
            this.txtcashbal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtcashbal.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(602, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 16);
            this.label8.TabIndex = 171;
            this.label8.Text = "Wst Balance";
            // 
            // txtWstBal
            // 
            this.txtWstBal.AcceptBlankValue = false;
            this.txtWstBal.Alias = "";
            this.txtWstBal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWstBal.DataField = "WstBalance";
            this.txtWstBal.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtWstBal.Location = new System.Drawing.Point(725, 13);
            this.txtWstBal.Name = "txtWstBal";
            this.txtWstBal.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtWstBal.Size = new System.Drawing.Size(114, 20);
            this.txtWstBal.TabIndex = 6;
            this.txtWstBal.Text = "0";
            this.txtWstBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(348, 13);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 16);
            this.label9.TabIndex = 169;
            this.label9.Text = "MC Balance";
            // 
            // txtMcBal
            // 
            this.txtMcBal.AcceptBlankValue = false;
            this.txtMcBal.Alias = "";
            this.txtMcBal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMcBal.DataField = "MCBalance";
            this.txtMcBal.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtMcBal.Location = new System.Drawing.Point(471, 13);
            this.txtMcBal.Name = "txtMcBal";
            this.txtMcBal.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtMcBal.Size = new System.Drawing.Size(114, 20);
            this.txtMcBal.TabIndex = 4;
            this.txtMcBal.Text = "0";
            this.txtMcBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(602, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 16);
            this.label10.TabIndex = 175;
            this.label10.Text = "Stone Balance";
            // 
            // txtStoneBal
            // 
            this.txtStoneBal.AcceptBlankValue = false;
            this.txtStoneBal.Alias = "";
            this.txtStoneBal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStoneBal.DataField = "StoneBalance";
            this.txtStoneBal.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtStoneBal.Location = new System.Drawing.Point(725, 39);
            this.txtStoneBal.Name = "txtStoneBal";
            this.txtStoneBal.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtStoneBal.Size = new System.Drawing.Size(114, 20);
            this.txtStoneBal.TabIndex = 7;
            this.txtStoneBal.Text = "0";
            this.txtStoneBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(348, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 16);
            this.label11.TabIndex = 173;
            this.label11.Text = "Dia Balance";
            // 
            // txtDiaBal
            // 
            this.txtDiaBal.AcceptBlankValue = false;
            this.txtDiaBal.Alias = "";
            this.txtDiaBal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDiaBal.DataField = "DiaBalance";
            this.txtDiaBal.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtDiaBal.Location = new System.Drawing.Point(471, 39);
            this.txtDiaBal.Name = "txtDiaBal";
            this.txtDiaBal.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtDiaBal.Size = new System.Drawing.Size(114, 20);
            this.txtDiaBal.TabIndex = 5;
            this.txtDiaBal.Text = "0";
            this.txtDiaBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // SupplierOpeningBalanceEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(991, 444);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtStoneBal);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtDiaBal);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtWstBal);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtMcBal);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtcashbal);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TxtTranscId);
            this.Controls.Add(this.txtwtbal);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.TxtIsactive);
            this.Controls.Add(this.grbDTPicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_EntryNo);
            this.Controls.Add(this.TxtEntryNo);
            this.Controls.Add(this.cmb_supplier);
            this.Controls.Add(this.label5);
            this.Name = "SupplierOpeningBalanceEntry";
            this.Text = "Supplier Opening Balance Entry";
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.cmb_supplier, 0);
            this.Controls.SetChildIndex(this.TxtEntryNo, 0);
            this.Controls.SetChildIndex(this.lbl_EntryNo, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.grbDTPicker1, 0);
            this.Controls.SetChildIndex(this.TxtIsactive, 0);
            this.Controls.SetChildIndex(this.label18, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.txtwtbal, 0);
            this.Controls.SetChildIndex(this.TxtTranscId, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtcashbal, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtMcBal, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.txtWstBal, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.txtDiaBal, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.txtStoneBal, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gramboo.Controls.GrbComboBox cmb_supplier;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_EntryNo;
        private Gramboo.Controls.GrbTextBox TxtEntryNo;
        private Gramboo.Controls.GrbDTPicker grbDTPicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label18;
        private Gramboo.Controls.GrbTextBox TxtIsactive;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private Gramboo.Controls.GrbTextBox txtBillNo;
        private System.Windows.Forms.Label label3;
        private Gramboo.Controls.GrbTextBox txtCash;
        private Gramboo.Controls.GrbButton btn_add;
        private Gramboo.Controls.GrbDataGridView dgv;
        private Gramboo.Controls.GrbTextBox txtWt;
        private System.Windows.Forms.Label Lbl_ReceiptWt;
        private Gramboo.Controls.GrbDTPicker grbDTPicker2;
        private System.Windows.Forms.Label label4;
        private Gramboo.Controls.GrbTextBox TxtTranscId;
        private System.Windows.Forms.Label label20;
        private Gramboo.Controls.GrbCheckBox txtstaus;
        private System.Windows.Forms.Label label6;
        private Gramboo.Controls.GrbTextBox txtwtbal;
        private System.Windows.Forms.Label label7;
        private Gramboo.Controls.GrbTextBox txtcashbal;
        private System.Windows.Forms.Label label8;
        private Gramboo.Controls.GrbTextBox txtWstBal;
        private System.Windows.Forms.Label label9;
        private Gramboo.Controls.GrbTextBox txtMcBal;
        private System.Windows.Forms.Label label10;
        private Gramboo.Controls.GrbTextBox txtStoneBal;
        private System.Windows.Forms.Label label11;
        private Gramboo.Controls.GrbTextBox txtDiaBal;
        private Gramboo.Controls.GrbDTPicker grbDTPicker3;
        private System.Windows.Forms.Label label12;
    }
}
