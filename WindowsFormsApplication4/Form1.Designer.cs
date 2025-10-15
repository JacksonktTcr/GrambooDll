namespace WindowsFormsApplication4
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Cmb_LedgerHead = new Gramboo.Controls.GrbComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtdebit = new Gramboo.Controls.GrbTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEntryNarration = new Gramboo.Controls.GrbTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCredit = new Gramboo.Controls.GrbTextBox();
            this.txtnarration = new Gramboo.Controls.GrbTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtTranscId = new Gramboo.Controls.GrbTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_EntryNo = new System.Windows.Forms.Label();
            this.TxtEntryNo = new Gramboo.Controls.GrbTextBox();
            this.txttotalcredit = new Gramboo.Controls.GrbTextBox();
            this.txttotaldebit = new Gramboo.Controls.GrbTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtIsactive = new Gramboo.Controls.GrbTextBox();
            this.cmb_VoucherTypeId = new Gramboo.Controls.GrbComboBox();
            this.Lbl_vouctypeId = new System.Windows.Forms.Label();
            this.txtvochAmount = new Gramboo.Controls.GrbTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtVoucherNo = new Gramboo.Controls.GrbTextBox();
            this.Dtp_dt = new Gramboo.Controls.GrbDTPicker();
            this.Lbl_voucherNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grbButton1 = new Gramboo.Controls.GrbButton();
            this.cmbAssortId = new Gramboo.Controls.GrbComboBox();
            this.grbDataGridView1 = new Gramboo.Controls.GrbDataGridView();
            this.grbPictureBox1 = new Gramboo.Controls.GrbPictureBox();
            this.grbButton2 = new Gramboo.Controls.GrbButton();
            ((System.ComponentModel.ISupportInitialize)(this.grbDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grbPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(1060, 309);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(1083, 307);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(1097, 309);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(1097, 283);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(1060, 283);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(1083, 283);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(1083, 335);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(1086, 335);
            // 
            // Cmb_LedgerHead
            // 
            this.Cmb_LedgerHead.AcceptBlankValue = false;
            this.Cmb_LedgerHead.BindingProperty = "SelectedValue";
            this.Cmb_LedgerHead.CheckDuplicates = true;
            this.Cmb_LedgerHead.DataField = "Acc_LedgerId";
            this.Cmb_LedgerHead.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.Cmb_LedgerHead.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cmb_LedgerHead.FormattingEnabled = true;
            this.Cmb_LedgerHead.Location = new System.Drawing.Point(115, 54);
            this.Cmb_LedgerHead.MultiColumn = true;
            this.Cmb_LedgerHead.Name = "Cmb_LedgerHead";
            this.Cmb_LedgerHead.NormalBorderColor = System.Drawing.Color.Gray;
            this.Cmb_LedgerHead.Size = new System.Drawing.Size(179, 21);
            this.Cmb_LedgerHead.TabIndex = 3;
            this.Cmb_LedgerHead.TableName = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 146;
            this.label5.Text = "Ledger Head";
            // 
            // txtdebit
            // 
            this.txtdebit.AcceptBlankValue = false;
            this.txtdebit.ActiveBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtdebit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtdebit.DataField = "Debit";
            this.txtdebit.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtdebit.Location = new System.Drawing.Point(115, 81);
            this.txtdebit.Name = "txtdebit";
            this.txtdebit.NormalBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtdebit.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtdebit.Size = new System.Drawing.Size(179, 20);
            this.txtdebit.TabIndex = 2;
            this.txtdebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 147;
            this.label6.Text = "Debit";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 16);
            this.label8.TabIndex = 149;
            this.label8.Text = "Narration";
            // 
            // txtEntryNarration
            // 
            this.txtEntryNarration.ActiveBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtEntryNarration.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEntryNarration.DataField = "EntryNarration";
            this.txtEntryNarration.Location = new System.Drawing.Point(115, 135);
            this.txtEntryNarration.Name = "txtEntryNarration";
            this.txtEntryNarration.NormalBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtEntryNarration.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtEntryNarration.Size = new System.Drawing.Size(427, 20);
            this.txtEntryNarration.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 16);
            this.label7.TabIndex = 148;
            this.label7.Text = "Credit";
            // 
            // txtCredit
            // 
            this.txtCredit.AcceptBlankValue = false;
            this.txtCredit.ActiveBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtCredit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCredit.DataField = "Credit";
            this.txtCredit.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtCredit.Location = new System.Drawing.Point(115, 108);
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.NormalBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtCredit.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtCredit.Size = new System.Drawing.Size(179, 20);
            this.txtCredit.TabIndex = 4;
            this.txtCredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtnarration
            // 
            this.txtnarration.ActiveBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtnarration.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtnarration.DataField = "Narration";
            this.txtnarration.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtnarration.Location = new System.Drawing.Point(0, 304);
            this.txtnarration.Multiline = true;
            this.txtnarration.Name = "txtnarration";
            this.txtnarration.NormalBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtnarration.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtnarration.Size = new System.Drawing.Size(1053, 140);
            this.txtnarration.TabIndex = 139;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 361);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 160;
            this.label4.Text = "Remark";
            // 
            // TxtTranscId
            // 
            this.TxtTranscId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtTranscId.DataField = "VchDetId";
            this.TxtTranscId.InputMask = Gramboo.Controls.GrbTextBox.Mask.Digit;
            this.TxtTranscId.IsIDField = true;
            this.TxtTranscId.Location = new System.Drawing.Point(883, 54);
            this.TxtTranscId.Name = "TxtTranscId";
            this.TxtTranscId.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtTranscId.Size = new System.Drawing.Size(115, 20);
            this.TxtTranscId.TabIndex = 158;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(793, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 16);
            this.label11.TabIndex = 159;
            this.label11.Text = "Transaction Id";
            this.label11.Visible = false;
            // 
            // lbl_EntryNo
            // 
            this.lbl_EntryNo.AutoSize = true;
            this.lbl_EntryNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_EntryNo.Location = new System.Drawing.Point(811, 58);
            this.lbl_EntryNo.Name = "lbl_EntryNo";
            this.lbl_EntryNo.Size = new System.Drawing.Size(62, 16);
            this.lbl_EntryNo.TabIndex = 156;
            this.lbl_EntryNo.Text = " Entry No";
            this.lbl_EntryNo.Visible = false;
            // 
            // TxtEntryNo
            // 
            this.TxtEntryNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtEntryNo.DataField = "VoucherEntryId";
            this.TxtEntryNo.IsIDField = true;
            this.TxtEntryNo.Location = new System.Drawing.Point(856, 77);
            this.TxtEntryNo.Name = "TxtEntryNo";
            this.TxtEntryNo.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtEntryNo.Size = new System.Drawing.Size(115, 20);
            this.TxtEntryNo.TabIndex = 157;
            this.TxtEntryNo.Visible = false;
            // 
            // txttotalcredit
            // 
            this.txttotalcredit.AcceptBlankValue = false;
            this.txttotalcredit.ActiveBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txttotalcredit.BindToDataGridview = true;
            this.txttotalcredit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txttotalcredit.DataField = "";
            this.txttotalcredit.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txttotalcredit.Location = new System.Drawing.Point(910, 134);
            this.txttotalcredit.Name = "txttotalcredit";
            this.txttotalcredit.NormalBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txttotalcredit.NormalBorderColor = System.Drawing.Color.Gray;
            this.txttotalcredit.Size = new System.Drawing.Size(88, 20);
            this.txttotalcredit.TabIndex = 155;
            this.txttotalcredit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txttotalcredit.Visible = false;
            // 
            // txttotaldebit
            // 
            this.txttotaldebit.AcceptBlankValue = false;
            this.txttotaldebit.ActiveBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txttotaldebit.BindToDataGridview = true;
            this.txttotaldebit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txttotaldebit.DataField = "";
            this.txttotaldebit.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txttotaldebit.Location = new System.Drawing.Point(910, 108);
            this.txttotaldebit.Name = "txttotaldebit";
            this.txttotaldebit.NormalBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txttotaldebit.NormalBorderColor = System.Drawing.Color.Gray;
            this.txttotaldebit.Size = new System.Drawing.Size(88, 20);
            this.txttotaldebit.TabIndex = 154;
            this.txttotaldebit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txttotaldebit.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(815, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 16);
            this.label3.TabIndex = 150;
            this.label3.Text = "Is Active";
            this.label3.Visible = false;
            // 
            // TxtIsactive
            // 
            this.TxtIsactive.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtIsactive.DataField = "IsActive";
            this.TxtIsactive.Location = new System.Drawing.Point(883, 30);
            this.TxtIsactive.Name = "TxtIsactive";
            this.TxtIsactive.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtIsactive.Size = new System.Drawing.Size(115, 20);
            this.TxtIsactive.TabIndex = 151;
            this.TxtIsactive.Visible = false;
            // 
            // cmb_VoucherTypeId
            // 
            this.cmb_VoucherTypeId.BindingProperty = "SelectedValue";
            this.cmb_VoucherTypeId.DataField = "VoucherTypeId";
            this.cmb_VoucherTypeId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_VoucherTypeId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_VoucherTypeId.FormattingEnabled = true;
            this.cmb_VoucherTypeId.Location = new System.Drawing.Point(883, 3);
            this.cmb_VoucherTypeId.Name = "cmb_VoucherTypeId";
            this.cmb_VoucherTypeId.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmb_VoucherTypeId.Size = new System.Drawing.Size(115, 21);
            this.cmb_VoucherTypeId.TabIndex = 152;
            this.cmb_VoucherTypeId.TableName = "";
            this.cmb_VoucherTypeId.Visible = false;
            // 
            // Lbl_vouctypeId
            // 
            this.Lbl_vouctypeId.AutoSize = true;
            this.Lbl_vouctypeId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_vouctypeId.Location = new System.Drawing.Point(770, 4);
            this.Lbl_vouctypeId.Name = "Lbl_vouctypeId";
            this.Lbl_vouctypeId.Size = new System.Drawing.Size(103, 16);
            this.Lbl_vouctypeId.TabIndex = 153;
            this.Lbl_vouctypeId.Text = "VoucherTypeID";
            this.Lbl_vouctypeId.Visible = false;
            // 
            // txtvochAmount
            // 
            this.txtvochAmount.ActiveBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtvochAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtvochAmount.DataField = "VoucherAmount";
            this.txtvochAmount.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.txtvochAmount.Location = new System.Drawing.Point(577, 5);
            this.txtvochAmount.Name = "txtvochAmount";
            this.txtvochAmount.NormalBackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtvochAmount.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtvochAmount.Size = new System.Drawing.Size(179, 20);
            this.txtvochAmount.TabIndex = 144;
            this.txtvochAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtvochAmount.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(465, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 145;
            this.label1.Text = "Voucher Amount";
            this.label1.Visible = false;
            // 
            // TxtVoucherNo
            // 
            this.TxtVoucherNo.ActiveBackColor = System.Drawing.Color.LightGray;
            this.TxtVoucherNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtVoucherNo.DataField = "VoucherNo";
            this.TxtVoucherNo.Location = new System.Drawing.Point(115, 0);
            this.TxtVoucherNo.Name = "TxtVoucherNo";
            this.TxtVoucherNo.NormalBackColor = System.Drawing.Color.LightGray;
            this.TxtVoucherNo.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtVoucherNo.ReadOnly = true;
            this.TxtVoucherNo.Size = new System.Drawing.Size(179, 20);
            this.TxtVoucherNo.TabIndex = 0;
            this.TxtVoucherNo.TabStop = false;
            // 
            // Dtp_dt
            // 
            this.Dtp_dt.AcceptBlankValue = false;
            this.Dtp_dt.BindingProperty = "Value";
            this.Dtp_dt.DataField = "VoucherDate";
            this.Dtp_dt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dtp_dt.Location = new System.Drawing.Point(115, 27);
            this.Dtp_dt.Name = "Dtp_dt";
            this.Dtp_dt.NormalBorderColor = System.Drawing.Color.Gray;
            this.Dtp_dt.Size = new System.Drawing.Size(179, 20);
            this.Dtp_dt.TabIndex = 1;
            // 
            // Lbl_voucherNo
            // 
            this.Lbl_voucherNo.AutoSize = true;
            this.Lbl_voucherNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_voucherNo.Location = new System.Drawing.Point(6, 4);
            this.Lbl_voucherNo.Name = "Lbl_voucherNo";
            this.Lbl_voucherNo.Size = new System.Drawing.Size(79, 16);
            this.Lbl_voucherNo.TabIndex = 142;
            this.Lbl_voucherNo.Text = "Voucher No";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 16);
            this.label2.TabIndex = 143;
            this.label2.Text = "Voucher Date";
            // 
            // grbButton1
            // 
            this.grbButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.BackgroundImage")));
            this.grbButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grbButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.grbButton1.FlatAppearance.BorderSize = 2;
            this.grbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grbButton1.Location = new System.Drawing.Point(577, 188);
            this.grbButton1.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.MouseDownImage")));
            this.grbButton1.Name = "grbButton1";
            this.grbButton1.NormalImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.NormalImage")));
            this.grbButton1.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.OnFocusImage")));
            this.grbButton1.SelectedImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.SelectedImage")));
            this.grbButton1.Size = new System.Drawing.Size(75, 23);
            this.grbButton1.TabIndex = 161;
            this.grbButton1.Text = "grbButton1";
            this.grbButton1.UseVisualStyleBackColor = true;
            this.grbButton1.Click += new System.EventHandler(this.grbButton1_Click);
            // 
            // cmbAssortId
            // 
            this.cmbAssortId.AcceptBlankValue = false;
            this.cmbAssortId.BindingProperty = "SelectedValue";
            this.cmbAssortId.BindToDataGridview = true;
            this.cmbAssortId.DataField = "AssortId";
            this.cmbAssortId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbAssortId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAssortId.FormattingEnabled = true;
            this.cmbAssortId.Location = new System.Drawing.Point(433, 212);
            this.cmbAssortId.Name = "cmbAssortId";
            this.cmbAssortId.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmbAssortId.Size = new System.Drawing.Size(186, 21);
            this.cmbAssortId.TabIndex = 6;
            this.cmbAssortId.TableName = "";
            // 
            // grbDataGridView1
            // 
            this.grbDataGridView1.AllowGrouping = false;
            this.grbDataGridView1.AllowUserToAddRows = false;
            this.grbDataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grbDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grbDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grbDataGridView1.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("grbDataGridView1.DataFields")));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grbDataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.grbDataGridView1.DisplaySumRowHeader = false;
            this.grbDataGridView1.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("grbDataGridView1.FilterList")));
            this.grbDataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grbDataGridView1.GroupingFields = null;
            this.grbDataGridView1.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("grbDataGridView1.HiddenDataFields")));
            this.grbDataGridView1.Location = new System.Drawing.Point(548, 58);
            this.grbDataGridView1.Name = "grbDataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grbDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grbDataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.grbDataGridView1.ShowDelete = false;
            this.grbDataGridView1.ShowEdit = false;
            this.grbDataGridView1.ShowMoveDown = false;
            this.grbDataGridView1.ShowMoveUp = false;
            this.grbDataGridView1.ShowSelectCheckBox = false;
            this.grbDataGridView1.ShowSerialNo = false;
            this.grbDataGridView1.Size = new System.Drawing.Size(240, 386);
            this.grbDataGridView1.SummaryColumns = null;
            this.grbDataGridView1.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.grbDataGridView1.SummaryRowSpace = 0;
            this.grbDataGridView1.SummaryRowVisible = true;
            this.grbDataGridView1.SumRowHeaderText = null;
            this.grbDataGridView1.SumRowHeaderTextBold = false;
            this.grbDataGridView1.TabIndex = 162;
            // 
            // grbPictureBox1
            // 
            this.grbPictureBox1.Browsable = true;
            this.grbPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("grbPictureBox1.Image")));
            this.grbPictureBox1.Location = new System.Drawing.Point(183, 161);
            this.grbPictureBox1.Name = "grbPictureBox1";
            this.grbPictureBox1.Size = new System.Drawing.Size(190, 137);
            this.grbPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.grbPictureBox1.TabIndex = 163;
            this.grbPictureBox1.TabStop = false;
            // 
            // grbButton2
            // 
            this.grbButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbButton2.BackgroundImage")));
            this.grbButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grbButton2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.grbButton2.FlatAppearance.BorderSize = 2;
            this.grbButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grbButton2.Location = new System.Drawing.Point(41, 188);
            this.grbButton2.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("grbButton2.MouseDownImage")));
            this.grbButton2.Name = "grbButton2";
            this.grbButton2.NormalImage = ((System.Drawing.Image)(resources.GetObject("grbButton2.NormalImage")));
            this.grbButton2.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("grbButton2.OnFocusImage")));
            this.grbButton2.SelectedImage = ((System.Drawing.Image)(resources.GetObject("grbButton2.SelectedImage")));
            this.grbButton2.Size = new System.Drawing.Size(75, 23);
            this.grbButton2.TabIndex = 164;
            this.grbButton2.Text = "grbButton2";
            this.grbButton2.UseVisualStyleBackColor = true;
            this.grbButton2.Click += new System.EventHandler(this.grbButton2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1053, 444);
            this.Controls.Add(this.grbButton2);
            this.Controls.Add(this.grbPictureBox1);
            this.Controls.Add(this.grbDataGridView1);
            this.Controls.Add(this.cmbAssortId);
            this.Controls.Add(this.grbButton1);
            this.Controls.Add(this.Cmb_LedgerHead);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtdebit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtEntryNarration);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtCredit);
            this.Controls.Add(this.txtnarration);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TxtTranscId);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lbl_EntryNo);
            this.Controls.Add(this.TxtEntryNo);
            this.Controls.Add(this.txttotalcredit);
            this.Controls.Add(this.txttotaldebit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtIsactive);
            this.Controls.Add(this.cmb_VoucherTypeId);
            this.Controls.Add(this.Lbl_vouctypeId);
            this.Controls.Add(this.txtvochAmount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtVoucherNo);
            this.Controls.Add(this.Dtp_dt);
            this.Controls.Add(this.Lbl_voucherNo);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.Lbl_voucherNo, 0);
            this.Controls.SetChildIndex(this.Dtp_dt, 0);
            this.Controls.SetChildIndex(this.TxtVoucherNo, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtvochAmount, 0);
            this.Controls.SetChildIndex(this.Lbl_vouctypeId, 0);
            this.Controls.SetChildIndex(this.cmb_VoucherTypeId, 0);
            this.Controls.SetChildIndex(this.TxtIsactive, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txttotaldebit, 0);
            this.Controls.SetChildIndex(this.txttotalcredit, 0);
            this.Controls.SetChildIndex(this.TxtEntryNo, 0);
            this.Controls.SetChildIndex(this.lbl_EntryNo, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.TxtTranscId, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtnarration, 0);
            this.Controls.SetChildIndex(this.txtCredit, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtEntryNarration, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.txtdebit, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.Cmb_LedgerHead, 0);
            this.Controls.SetChildIndex(this.grbButton1, 0);
            this.Controls.SetChildIndex(this.cmbAssortId, 0);
            this.Controls.SetChildIndex(this.grbDataGridView1, 0);
            this.Controls.SetChildIndex(this.grbPictureBox1, 0);
            this.Controls.SetChildIndex(this.grbButton2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.grbDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grbPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gramboo.Controls.GrbComboBox Cmb_LedgerHead;
        private System.Windows.Forms.Label label5;
        private Gramboo.Controls.GrbTextBox txtdebit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private Gramboo.Controls.GrbTextBox txtEntryNarration;
        private System.Windows.Forms.Label label7;
        private Gramboo.Controls.GrbTextBox txtCredit;
        private Gramboo.Controls.GrbTextBox txtnarration;
        private System.Windows.Forms.Label label4;
        private Gramboo.Controls.GrbTextBox TxtTranscId;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_EntryNo;
        private Gramboo.Controls.GrbTextBox TxtEntryNo;
        private Gramboo.Controls.GrbTextBox txttotalcredit;
        private Gramboo.Controls.GrbTextBox txttotaldebit;
        private System.Windows.Forms.Label label3;
        private Gramboo.Controls.GrbTextBox TxtIsactive;
        private Gramboo.Controls.GrbComboBox cmb_VoucherTypeId;
        private System.Windows.Forms.Label Lbl_vouctypeId;
        private Gramboo.Controls.GrbTextBox txtvochAmount;
        private System.Windows.Forms.Label label1;
        private Gramboo.Controls.GrbTextBox TxtVoucherNo;
        private Gramboo.Controls.GrbDTPicker Dtp_dt;
        private System.Windows.Forms.Label Lbl_voucherNo;
        private System.Windows.Forms.Label label2;
        private Gramboo.Controls.GrbButton grbButton1;
        private Gramboo.Controls.GrbComboBox cmbAssortId;
        private Gramboo.Controls.GrbDataGridView grbDataGridView1;
        private Gramboo.Controls.GrbPictureBox grbPictureBox1;
        private Gramboo.Controls.GrbButton grbButton2;

    }
}
