namespace SAFA.Forms.ACC
{
    partial class AdjustingEntries
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdjustingEntries));
            this.dgview = new Gramboo.Controls.GrbDataGridView();
            this.CmbPendingBill = new Gramboo.Controls.GrbComboBox();
            this.TxtPaymentAmt = new Gramboo.Controls.GrbTextBox();
            this.TxtPendingAmt = new Gramboo.Controls.GrbTextBox();
            this.BtnAdd = new Gramboo.Controls.GrbButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtAmount = new Gramboo.Controls.GrbTextBox();
            this.DtpDate = new Gramboo.Controls.GrbDTPicker();
            this.BtnSave = new Gramboo.Controls.GrbButton();
            this.BtnDelete = new Gramboo.Controls.GrbButton();
            this.TxtRecId = new Gramboo.Controls.GrbTextBox();
            this.TxtRecTable = new Gramboo.Controls.GrbTextBox();
            this.TxtAdjId = new Gramboo.Controls.GrbTextBox();
            this.TxtTransId = new Gramboo.Controls.GrbTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtMcAmt = new Gramboo.Controls.GrbTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtRate = new Gramboo.Controls.GrbTextBox();
            this.TxtNonWTAmount = new Gramboo.Controls.GrbTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CmbRateCutting = new Gramboo.Controls.GrbComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtWt = new Gramboo.Controls.GrbTextBox();
            this.TxtBillId = new Gramboo.Controls.GrbTextBox();
            this.TxtRateCutting = new Gramboo.Controls.GrbTextBox();
            this.TxtCurMetalCash = new Gramboo.Controls.GrbTextBox();
            this.TxtPendingNonWTAmount = new Gramboo.Controls.GrbTextBox();
            this.TxtPendingMcAmt = new Gramboo.Controls.GrbTextBox();
            this.TxtRecCode = new Gramboo.Controls.GrbTextBox();
            this.TxtRecVchNo = new Gramboo.Controls.GrbTextBox();
            this.TxtMetalRate = new Gramboo.Controls.GrbTextBox();
            this.BtnAutoAdjust = new Gramboo.Controls.GrbButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgview)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(2245, 512);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(2268, 510);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(2282, 512);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(2282, 486);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(2245, 486);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(2268, 486);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(2268, 538);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(2271, 538);
            // 
            // dgview
            // 
            this.dgview.AllowGrouping = false;
            this.dgview.AllowUserToAddRows = false;
            this.dgview.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgview.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgview.DataFields")));
            this.dgview.DisplaySumRowHeader = false;
            this.dgview.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("dgview.FilterList")));
            this.dgview.FormatString = "F03";
            this.dgview.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgview.GroupingFields = null;
            this.dgview.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgview.HiddenDataFields")));
            this.dgview.Location = new System.Drawing.Point(3, 43);
            this.dgview.Name = "dgview";
            this.dgview.ReadOnly = true;
            this.dgview.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgview.ShowDelete = true;
            this.dgview.ShowEdit = true;
            this.dgview.ShowMoveDown = false;
            this.dgview.ShowMoveUp = false;
            this.dgview.ShowSelectCheckBox = false;
            this.dgview.ShowSerialNo = true;
            this.dgview.Size = new System.Drawing.Size(866, 196);
            this.dgview.SummaryColumns = null;
            this.dgview.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.dgview.SummaryRowSpace = 0;
            this.dgview.SummaryRowVisible = true;
            this.dgview.SumRowHeaderText = null;
            this.dgview.SumRowHeaderTextBold = true;
            this.dgview.TabIndex = 0;
            this.dgview.SummaryCalculated += new Gramboo.Controls.SummaryCalcEventHandler(this.dgview_SummaryCalculated);
            this.dgview.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgview_CellDoubleClick);
            // 
            // CmbPendingBill
            // 
            this.CmbPendingBill.CheckDuplicates = true;
            this.CmbPendingBill.DataField = "BillNo";
            this.CmbPendingBill.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CmbPendingBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbPendingBill.FormattingEnabled = true;
            this.CmbPendingBill.Location = new System.Drawing.Point(7, 18);
            this.CmbPendingBill.Name = "CmbPendingBill";
            this.CmbPendingBill.NormalBorderColor = System.Drawing.Color.Gray;
            this.CmbPendingBill.Size = new System.Drawing.Size(185, 21);
            this.CmbPendingBill.TabIndex = 1;
            this.CmbPendingBill.TableName = "";
            this.CmbPendingBill.SelectedValueChanged += new System.EventHandler(this.CmbPendingBill_SelectedValueChanged);
            // 
            // TxtPaymentAmt
            // 
            this.TxtPaymentAmt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPaymentAmt.DataField = "PaymentAmt";
            this.TxtPaymentAmt.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtPaymentAmt.Location = new System.Drawing.Point(713, 18);
            this.TxtPaymentAmt.Name = "TxtPaymentAmt";
            this.TxtPaymentAmt.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtPaymentAmt.Size = new System.Drawing.Size(101, 20);
            this.TxtPaymentAmt.TabIndex = 2;
            this.TxtPaymentAmt.Text = "0";
            this.TxtPaymentAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtPaymentAmt.TextChanged += new System.EventHandler(this.TxtPaymentAmt_TextChanged);
            // 
            // TxtPendingAmt
            // 
            this.TxtPendingAmt.BackColor = System.Drawing.Color.White;
            this.TxtPendingAmt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPendingAmt.DataField = "PendingAmt";
            this.TxtPendingAmt.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtPendingAmt.Location = new System.Drawing.Point(196, 18);
            this.TxtPendingAmt.Name = "TxtPendingAmt";
            this.TxtPendingAmt.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtPendingAmt.ReadOnly = true;
            this.TxtPendingAmt.Size = new System.Drawing.Size(102, 20);
            this.TxtPendingAmt.TabIndex = 3;
            this.TxtPendingAmt.Text = "0";
            this.TxtPendingAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnAdd.BackgroundImage")));
            this.BtnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.BtnAdd.FlatAppearance.BorderSize = 2;
            this.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAdd.Location = new System.Drawing.Point(818, 13);
            this.BtnAdd.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("BtnAdd.MouseDownImage")));
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.NormalImage = ((System.Drawing.Image)(resources.GetObject("BtnAdd.NormalImage")));
            this.BtnAdd.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("BtnAdd.OnFocusImage")));
            this.BtnAdd.SelectedImage = ((System.Drawing.Image)(resources.GetObject("BtnAdd.SelectedImage")));
            this.BtnAdd.Size = new System.Drawing.Size(44, 24);
            this.BtnAdd.TabIndex = 4;
            this.BtnAdd.Text = "+";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(713, -2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "PaymentAmt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "PendingAmt";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "BillNo";
            // 
            // TxtAmount
            // 
            this.TxtAmount.BackColor = System.Drawing.Color.White;
            this.TxtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAmount.ForeColor = System.Drawing.Color.Red;
            this.TxtAmount.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtAmount.Location = new System.Drawing.Point(727, 10);
            this.TxtAmount.Multiline = true;
            this.TxtAmount.Name = "TxtAmount";
            this.TxtAmount.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtAmount.ReadOnly = true;
            this.TxtAmount.Size = new System.Drawing.Size(152, 22);
            this.TxtAmount.TabIndex = 8;
            this.TxtAmount.Text = "0";
            this.TxtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // DtpDate
            // 
            this.DtpDate.BindingProperty = "Value";
            this.DtpDate.DataField = "AdjDate";
            this.DtpDate.Enabled = false;
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtpDate.Location = new System.Drawing.Point(39, 12);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.NormalBorderColor = System.Drawing.Color.Gray;
            this.DtpDate.Size = new System.Drawing.Size(112, 20);
            this.DtpDate.TabIndex = 9;
            // 
            // BtnSave
            // 
            this.BtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnSave.BackgroundImage")));
            this.BtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.BtnSave.FlatAppearance.BorderSize = 2;
            this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSave.Location = new System.Drawing.Point(331, 309);
            this.BtnSave.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("BtnSave.MouseDownImage")));
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.NormalImage = ((System.Drawing.Image)(resources.GetObject("BtnSave.NormalImage")));
            this.BtnSave.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("BtnSave.OnFocusImage")));
            this.BtnSave.SelectedImage = ((System.Drawing.Image)(resources.GetObject("BtnSave.SelectedImage")));
            this.BtnSave.Size = new System.Drawing.Size(71, 33);
            this.BtnSave.TabIndex = 10;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnDelete.BackgroundImage")));
            this.BtnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.BtnDelete.FlatAppearance.BorderSize = 2;
            this.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDelete.Location = new System.Drawing.Point(408, 309);
            this.BtnDelete.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("BtnDelete.MouseDownImage")));
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.NormalImage = ((System.Drawing.Image)(resources.GetObject("BtnDelete.NormalImage")));
            this.BtnDelete.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("BtnDelete.OnFocusImage")));
            this.BtnDelete.SelectedImage = ((System.Drawing.Image)(resources.GetObject("BtnDelete.SelectedImage")));
            this.BtnDelete.Size = new System.Drawing.Size(71, 33);
            this.BtnDelete.TabIndex = 11;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // TxtRecId
            // 
            this.TxtRecId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtRecId.DataField = "RecId";
            this.TxtRecId.Location = new System.Drawing.Point(23, 12);
            this.TxtRecId.Name = "TxtRecId";
            this.TxtRecId.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtRecId.ReadOnly = true;
            this.TxtRecId.Size = new System.Drawing.Size(10, 20);
            this.TxtRecId.TabIndex = 19;
            this.TxtRecId.Visible = false;
            // 
            // TxtRecTable
            // 
            this.TxtRecTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtRecTable.DataField = "RecTable";
            this.TxtRecTable.Location = new System.Drawing.Point(7, 12);
            this.TxtRecTable.Name = "TxtRecTable";
            this.TxtRecTable.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtRecTable.ReadOnly = true;
            this.TxtRecTable.Size = new System.Drawing.Size(10, 20);
            this.TxtRecTable.TabIndex = 20;
            this.TxtRecTable.Visible = false;
            // 
            // TxtAdjId
            // 
            this.TxtAdjId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtAdjId.DataField = "AdjId";
            this.TxtAdjId.InputMask = Gramboo.Controls.GrbTextBox.Mask.Digit;
            this.TxtAdjId.Location = new System.Drawing.Point(187, 12);
            this.TxtAdjId.Name = "TxtAdjId";
            this.TxtAdjId.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtAdjId.ReadOnly = true;
            this.TxtAdjId.Size = new System.Drawing.Size(10, 20);
            this.TxtAdjId.TabIndex = 21;
            this.TxtAdjId.Text = "0";
            this.TxtAdjId.Visible = false;
            // 
            // TxtTransId
            // 
            this.TxtTransId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtTransId.DataField = "TransId";
            this.TxtTransId.InputMask = Gramboo.Controls.GrbTextBox.Mask.Digit;
            this.TxtTransId.Location = new System.Drawing.Point(157, 12);
            this.TxtTransId.Name = "TxtTransId";
            this.TxtTransId.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtTransId.ReadOnly = true;
            this.TxtTransId.Size = new System.Drawing.Size(10, 20);
            this.TxtTransId.TabIndex = 22;
            this.TxtTransId.Text = "0";
            this.TxtTransId.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TxtMcAmt);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.TxtRate);
            this.groupBox1.Controls.Add(this.TxtNonWTAmount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.CmbRateCutting);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TxtWt);
            this.groupBox1.Controls.Add(this.dgview);
            this.groupBox1.Controls.Add(this.TxtPendingAmt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.BtnAdd);
            this.groupBox1.Controls.Add(this.CmbPendingBill);
            this.groupBox1.Controls.Add(this.TxtBillId);
            this.groupBox1.Controls.Add(this.TxtRateCutting);
            this.groupBox1.Controls.Add(this.TxtPaymentAmt);
            this.groupBox1.Controls.Add(this.TxtCurMetalCash);
            this.groupBox1.Controls.Add(this.TxtPendingNonWTAmount);
            this.groupBox1.Controls.Add(this.TxtPendingMcAmt);
            this.groupBox1.Location = new System.Drawing.Point(10, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(875, 240);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // TxtMcAmt
            // 
            this.TxtMcAmt.BackColor = System.Drawing.Color.White;
            this.TxtMcAmt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtMcAmt.DataField = "MCAmt";
            this.TxtMcAmt.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtMcAmt.Location = new System.Drawing.Point(302, 19);
            this.TxtMcAmt.Name = "TxtMcAmt";
            this.TxtMcAmt.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtMcAmt.Size = new System.Drawing.Size(82, 20);
            this.TxtMcAmt.TabIndex = 564;
            this.TxtMcAmt.Text = "0";
            this.TxtMcAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(304, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 565;
            this.label8.Text = "MC Amt";
            // 
            // TxtRate
            // 
            this.TxtRate.BackColor = System.Drawing.Color.White;
            this.TxtRate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtRate.DataField = "Rate";
            this.TxtRate.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtRate.Location = new System.Drawing.Point(567, 18);
            this.TxtRate.Name = "TxtRate";
            this.TxtRate.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtRate.ReadOnly = true;
            this.TxtRate.Size = new System.Drawing.Size(69, 20);
            this.TxtRate.TabIndex = 31;
            this.TxtRate.Text = "0";
            this.TxtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtRate.TextChanged += new System.EventHandler(this.TxtRate_TextChanged);
            // 
            // TxtNonWTAmount
            // 
            this.TxtNonWTAmount.BackColor = System.Drawing.Color.White;
            this.TxtNonWTAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtNonWTAmount.DataField = "NonWtAmt";
            this.TxtNonWTAmount.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtNonWTAmount.Location = new System.Drawing.Point(388, 18);
            this.TxtNonWTAmount.Name = "TxtNonWTAmount";
            this.TxtNonWTAmount.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtNonWTAmount.Size = new System.Drawing.Size(82, 20);
            this.TxtNonWTAmount.TabIndex = 29;
            this.TxtNonWTAmount.Text = "0";
            this.TxtNonWTAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtNonWTAmount.TextChanged += new System.EventHandler(this.TxtNonWTAmount_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(392, -1);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 30;
            this.label7.Text = "Non Wt Amt";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(469, -2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Rate Cutting";
            // 
            // CmbRateCutting
            // 
            this.CmbRateCutting.DataField = "Rate Cutting";
            this.CmbRateCutting.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CmbRateCutting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbRateCutting.FormattingEnabled = true;
            this.CmbRateCutting.Items.AddRange(new object[] {
            "Fixed",
            "Unfixed"});
            this.CmbRateCutting.Location = new System.Drawing.Point(474, 18);
            this.CmbRateCutting.Name = "CmbRateCutting";
            this.CmbRateCutting.NormalBorderColor = System.Drawing.Color.Gray;
            this.CmbRateCutting.Size = new System.Drawing.Size(89, 21);
            this.CmbRateCutting.TabIndex = 26;
            this.CmbRateCutting.TableName = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(638, -2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Wt";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(564, -2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Rate";
            // 
            // TxtWt
            // 
            this.TxtWt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtWt.DataField = "Weight";
            this.TxtWt.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtWt.Location = new System.Drawing.Point(640, 18);
            this.TxtWt.Name = "TxtWt";
            this.TxtWt.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtWt.Size = new System.Drawing.Size(69, 20);
            this.TxtWt.TabIndex = 26;
            this.TxtWt.Text = "0";
            this.TxtWt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtWt.TextChanged += new System.EventHandler(this.TxtWt_TextChanged);
            // 
            // TxtBillId
            // 
            this.TxtBillId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtBillId.DataField = "BillId";
            this.TxtBillId.Location = new System.Drawing.Point(94, 18);
            this.TxtBillId.Name = "TxtBillId";
            this.TxtBillId.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtBillId.Size = new System.Drawing.Size(10, 20);
            this.TxtBillId.TabIndex = 24;
            // 
            // TxtRateCutting
            // 
            this.TxtRateCutting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtRateCutting.DataField = "RateCuttingMode";
            this.TxtRateCutting.InputMask = Gramboo.Controls.GrbTextBox.Mask.Digit;
            this.TxtRateCutting.Location = new System.Drawing.Point(433, 18);
            this.TxtRateCutting.Name = "TxtRateCutting";
            this.TxtRateCutting.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtRateCutting.Size = new System.Drawing.Size(23, 20);
            this.TxtRateCutting.TabIndex = 563;
            this.TxtRateCutting.Text = "0";
            this.TxtRateCutting.Visible = false;
            // 
            // TxtCurMetalCash
            // 
            this.TxtCurMetalCash.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtCurMetalCash.DataField = "Current_Metal_Cash";
            this.TxtCurMetalCash.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtCurMetalCash.Location = new System.Drawing.Point(655, 18);
            this.TxtCurMetalCash.Name = "TxtCurMetalCash";
            this.TxtCurMetalCash.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtCurMetalCash.ReadOnly = true;
            this.TxtCurMetalCash.Size = new System.Drawing.Size(10, 20);
            this.TxtCurMetalCash.TabIndex = 27;
            this.TxtCurMetalCash.Text = "0";
            this.TxtCurMetalCash.Visible = false;
            // 
            // TxtPendingNonWTAmount
            // 
            this.TxtPendingNonWTAmount.BackColor = System.Drawing.Color.White;
            this.TxtPendingNonWTAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPendingNonWTAmount.DataField = "PendingNonWtAmt";
            this.TxtPendingNonWTAmount.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtPendingNonWTAmount.Location = new System.Drawing.Point(396, 17);
            this.TxtPendingNonWTAmount.Name = "TxtPendingNonWTAmount";
            this.TxtPendingNonWTAmount.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtPendingNonWTAmount.ReadOnly = true;
            this.TxtPendingNonWTAmount.Size = new System.Drawing.Size(18, 20);
            this.TxtPendingNonWTAmount.TabIndex = 27;
            this.TxtPendingNonWTAmount.Text = "0";
            this.TxtPendingNonWTAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtPendingNonWTAmount.Visible = false;
            // 
            // TxtPendingMcAmt
            // 
            this.TxtPendingMcAmt.BackColor = System.Drawing.Color.White;
            this.TxtPendingMcAmt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPendingMcAmt.DataField = "PendingMCAmt";
            this.TxtPendingMcAmt.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtPendingMcAmt.Location = new System.Drawing.Point(302, 20);
            this.TxtPendingMcAmt.Name = "TxtPendingMcAmt";
            this.TxtPendingMcAmt.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtPendingMcAmt.ReadOnly = true;
            this.TxtPendingMcAmt.Size = new System.Drawing.Size(18, 20);
            this.TxtPendingMcAmt.TabIndex = 28;
            this.TxtPendingMcAmt.Text = "0";
            this.TxtPendingMcAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtPendingMcAmt.Visible = false;
            // 
            // TxtRecCode
            // 
            this.TxtRecCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtRecCode.DataField = "RecCode";
            this.TxtRecCode.Location = new System.Drawing.Point(7, 38);
            this.TxtRecCode.Name = "TxtRecCode";
            this.TxtRecCode.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtRecCode.ReadOnly = true;
            this.TxtRecCode.Size = new System.Drawing.Size(10, 20);
            this.TxtRecCode.TabIndex = 24;
            this.TxtRecCode.Visible = false;
            // 
            // TxtRecVchNo
            // 
            this.TxtRecVchNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtRecVchNo.DataField = "RecVchNo";
            this.TxtRecVchNo.Location = new System.Drawing.Point(23, 37);
            this.TxtRecVchNo.Name = "TxtRecVchNo";
            this.TxtRecVchNo.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtRecVchNo.ReadOnly = true;
            this.TxtRecVchNo.Size = new System.Drawing.Size(10, 20);
            this.TxtRecVchNo.TabIndex = 25;
            this.TxtRecVchNo.Visible = false;
            // 
            // TxtMetalRate
            // 
            this.TxtMetalRate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtMetalRate.DataField = "";
            this.TxtMetalRate.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtMetalRate.Location = new System.Drawing.Point(207, 12);
            this.TxtMetalRate.Name = "TxtMetalRate";
            this.TxtMetalRate.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtMetalRate.ReadOnly = true;
            this.TxtMetalRate.Size = new System.Drawing.Size(10, 20);
            this.TxtMetalRate.TabIndex = 26;
            this.TxtMetalRate.Text = "0";
            this.TxtMetalRate.Visible = false;
            this.TxtMetalRate.TextChanged += new System.EventHandler(this.TxtMetalRate_TextChanged);
            // 
            // BtnAutoAdjust
            // 
            this.BtnAutoAdjust.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnAutoAdjust.BackgroundImage")));
            this.BtnAutoAdjust.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAutoAdjust.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAutoAdjust.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.BtnAutoAdjust.FlatAppearance.BorderSize = 2;
            this.BtnAutoAdjust.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnAutoAdjust.ForeColor = System.Drawing.Color.DarkBlue;
            this.BtnAutoAdjust.Location = new System.Drawing.Point(796, 10);
            this.BtnAutoAdjust.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("BtnAutoAdjust.MouseDownImage")));
            this.BtnAutoAdjust.Name = "BtnAutoAdjust";
            this.BtnAutoAdjust.NormalImage = ((System.Drawing.Image)(resources.GetObject("BtnAutoAdjust.NormalImage")));
            this.BtnAutoAdjust.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("BtnAutoAdjust.OnFocusImage")));
            this.BtnAutoAdjust.SelectedImage = ((System.Drawing.Image)(resources.GetObject("BtnAutoAdjust.SelectedImage")));
            this.BtnAutoAdjust.Size = new System.Drawing.Size(83, 25);
            this.BtnAutoAdjust.TabIndex = 27;
            this.BtnAutoAdjust.Text = "Auto Adjust";
            this.BtnAutoAdjust.UseVisualStyleBackColor = true;
            this.BtnAutoAdjust.Visible = false;
            this.BtnAutoAdjust.Click += new System.EventHandler(this.BtnAutoAdjust_Click);
            // 
            // AdjustingEntries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 606);
            this.Controls.Add(this.BtnAutoAdjust);
            this.Controls.Add(this.TxtMetalRate);
            this.Controls.Add(this.TxtRecVchNo);
            this.Controls.Add(this.TxtRecCode);
            this.Controls.Add(this.TxtTransId);
            this.Controls.Add(this.TxtAdjId);
            this.Controls.Add(this.TxtRecTable);
            this.Controls.Add(this.TxtRecId);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.DtpDate);
            this.Controls.Add(this.TxtAmount);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdjustingEntries";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Adjustment Entries";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AdjustingEntries_FormClosed);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.TxtAmount, 0);
            this.Controls.SetChildIndex(this.DtpDate, 0);
            this.Controls.SetChildIndex(this.BtnSave, 0);
            this.Controls.SetChildIndex(this.BtnDelete, 0);
            this.Controls.SetChildIndex(this.TxtRecId, 0);
            this.Controls.SetChildIndex(this.TxtRecTable, 0);
            this.Controls.SetChildIndex(this.TxtAdjId, 0);
            this.Controls.SetChildIndex(this.TxtTransId, 0);
            this.Controls.SetChildIndex(this.TxtRecCode, 0);
            this.Controls.SetChildIndex(this.TxtRecVchNo, 0);
            this.Controls.SetChildIndex(this.TxtMetalRate, 0);
            this.Controls.SetChildIndex(this.BtnAutoAdjust, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Gramboo.Controls.GrbComboBox CmbPendingBill;
        private Gramboo.Controls.GrbTextBox TxtPaymentAmt;
        private Gramboo.Controls.GrbTextBox TxtPendingAmt;
        private Gramboo.Controls.GrbButton BtnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Gramboo.Controls.GrbTextBox TxtRecId;
        private Gramboo.Controls.GrbTextBox TxtRecTable;
        public Gramboo.Controls.GrbDTPicker DtpDate;
        private Gramboo.Controls.GrbTextBox TxtAdjId;
        private Gramboo.Controls.GrbTextBox TxtTransId;
        private System.Windows.Forms.GroupBox groupBox1;
        private Gramboo.Controls.GrbTextBox TxtBillId;
        private Gramboo.Controls.GrbTextBox TxtRecCode;
        private Gramboo.Controls.GrbTextBox TxtRecVchNo;
        private Gramboo.Controls.GrbTextBox TxtWt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private Gramboo.Controls.GrbComboBox CmbRateCutting;
        private Gramboo.Controls.GrbTextBox TxtNonWTAmount;
        private System.Windows.Forms.Label label7;
        private Gramboo.Controls.GrbTextBox TxtRate;
        private Gramboo.Controls.GrbTextBox TxtRateCutting;
        public Gramboo.Controls.GrbTextBox TxtAmount;
        public Gramboo.Controls.GrbButton BtnSave;
        public Gramboo.Controls.GrbButton BtnDelete;
        public Gramboo.Controls.GrbDataGridView dgview;
        private Gramboo.Controls.GrbTextBox TxtCurMetalCash;
        public Gramboo.Controls.GrbTextBox TxtMetalRate;
        private Gramboo.Controls.GrbTextBox TxtPendingNonWTAmount;
        public Gramboo.Controls.GrbButton BtnAutoAdjust;
        private Gramboo.Controls.GrbTextBox TxtMcAmt;
        private System.Windows.Forms.Label label8;
        private Gramboo.Controls.GrbTextBox TxtPendingMcAmt;
    }
}