namespace JMS.Forms.ACC
{
    partial class LedgerMaster
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LedgerMaster));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Lbl_GroupId = new System.Windows.Forms.Label();
            this.Chk_IsActive = new Gramboo.Controls.GrbCheckBox();
            this.cmb_GroupId = new Gramboo.Controls.GrbComboBox();
            this.TxtLedgerName = new Gramboo.Controls.GrbTextBox();
            this.Lbl_LedgerName = new System.Windows.Forms.Label();
            this.TxtLedgerID = new Gramboo.Controls.GrbTextBox();
            this.Lbl_LedgerID = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv = new Gramboo.Controls.GrbDataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.grbButton1 = new Gramboo.Controls.GrbButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(1015, 333);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(1038, 331);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(1052, 333);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(1052, 307);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(1015, 307);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(1038, 307);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(1038, 359);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(1041, 359);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lbl_GroupId);
            this.groupBox1.Controls.Add(this.Chk_IsActive);
            this.groupBox1.Controls.Add(this.cmb_GroupId);
            this.groupBox1.Controls.Add(this.TxtLedgerName);
            this.groupBox1.Controls.Add(this.Lbl_LedgerName);
            this.groupBox1.Location = new System.Drawing.Point(7, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 98);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // Lbl_GroupId
            // 
            this.Lbl_GroupId.AutoSize = true;
            this.Lbl_GroupId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GroupId.Location = new System.Drawing.Point(6, 46);
            this.Lbl_GroupId.Name = "Lbl_GroupId";
            this.Lbl_GroupId.Size = new System.Drawing.Size(48, 16);
            this.Lbl_GroupId.TabIndex = 146;
            this.Lbl_GroupId.Text = " Group";
            // 
            // Chk_IsActive
            // 
            this.Chk_IsActive.AcceptBlankValue = false;
            this.Chk_IsActive.BindingProperty = "Checked";
            this.Chk_IsActive.Checked = true;
            this.Chk_IsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_IsActive.DataField = "IsActive";
            this.Chk_IsActive.FalseValue = "0";
            this.Chk_IsActive.Location = new System.Drawing.Point(104, 73);
            this.Chk_IsActive.Name = "Chk_IsActive";
            this.Chk_IsActive.Size = new System.Drawing.Size(75, 20);
            this.Chk_IsActive.TabIndex = 2;
            this.Chk_IsActive.TableName = null;
            this.Chk_IsActive.Text = "Is Active";
            this.Chk_IsActive.TrueValue = "1";
            this.Chk_IsActive.UseVisualStyleBackColor = true;
            this.Chk_IsActive.Value = "1";
            // 
            // cmb_GroupId
            // 
            this.cmb_GroupId.BindingProperty = "SelectedValue";
            this.cmb_GroupId.DataField = "Acc_GroupId";
            this.cmb_GroupId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_GroupId.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_GroupId.FormattingEnabled = true;
            this.cmb_GroupId.Location = new System.Drawing.Point(104, 46);
            this.cmb_GroupId.Name = "cmb_GroupId";
            this.cmb_GroupId.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmb_GroupId.Size = new System.Drawing.Size(206, 21);
            this.cmb_GroupId.TabIndex = 1;
            this.cmb_GroupId.TableName = "";
            // 
            // TxtLedgerName
            // 
            this.TxtLedgerName.AcceptBlankValue = false;
            this.TxtLedgerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtLedgerName.DataField = "Acc_LedgerName";
            this.TxtLedgerName.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.TxtLedgerName.Location = new System.Drawing.Point(104, 19);
            this.TxtLedgerName.Name = "TxtLedgerName";
            this.TxtLedgerName.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtLedgerName.Size = new System.Drawing.Size(206, 20);
            this.TxtLedgerName.TabIndex = 0;
            this.TxtLedgerName.Text = "0";
            // 
            // Lbl_LedgerName
            // 
            this.Lbl_LedgerName.AutoSize = true;
            this.Lbl_LedgerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_LedgerName.Location = new System.Drawing.Point(6, 19);
            this.Lbl_LedgerName.Name = "Lbl_LedgerName";
            this.Lbl_LedgerName.Size = new System.Drawing.Size(91, 16);
            this.Lbl_LedgerName.TabIndex = 130;
            this.Lbl_LedgerName.Text = "Ledger Name";
            // 
            // TxtLedgerID
            // 
            this.TxtLedgerID.ActiveBackColor = System.Drawing.SystemColors.Control;
            this.TxtLedgerID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtLedgerID.DataField = "Acc_LedgerId";
            this.TxtLedgerID.IsIDField = true;
            this.TxtLedgerID.Location = new System.Drawing.Point(460, 17);
            this.TxtLedgerID.Name = "TxtLedgerID";
            this.TxtLedgerID.NormalBorderColor = System.Drawing.SystemColors.Control;
            this.TxtLedgerID.ReadOnly = true;
            this.TxtLedgerID.Size = new System.Drawing.Size(206, 20);
            this.TxtLedgerID.TabIndex = 0;
            this.TxtLedgerID.TabStop = false;
            this.TxtLedgerID.Visible = false;
            // 
            // Lbl_LedgerID
            // 
            this.Lbl_LedgerID.AutoSize = true;
            this.Lbl_LedgerID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_LedgerID.Location = new System.Drawing.Point(387, 17);
            this.Lbl_LedgerID.Name = "Lbl_LedgerID";
            this.Lbl_LedgerID.Size = new System.Drawing.Size(67, 16);
            this.Lbl_LedgerID.TabIndex = 129;
            this.Lbl_LedgerID.Text = "Ledger ID";
            this.Lbl_LedgerID.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgv);
            this.groupBox2.Location = new System.Drawing.Point(7, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(919, 377);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // dgv
            // 
            this.dgv.AllowGrouping = false;
            this.dgv.AllowUserToAddRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.DataFields")));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgv.DisplaySumRowHeader = false;
            this.dgv.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.FilterList")));
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv.GroupingFields = null;
            this.dgv.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.HiddenDataFields")));
            this.dgv.Location = new System.Drawing.Point(6, 12);
            this.dgv.Name = "dgv";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgv.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgv.ShowDelete = false;
            this.dgv.ShowEdit = false;
            this.dgv.ShowMoveDown = false;
            this.dgv.ShowMoveUp = false;
            this.dgv.ShowSelectCheckBox = false;
            this.dgv.ShowSerialNo = true;
            this.dgv.Size = new System.Drawing.Size(907, 359);
            this.dgv.SummaryColumns = null;
            this.dgv.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.dgv.SummaryRowSpace = 0;
            this.dgv.SumRowHeaderText = null;
            this.dgv.SumRowHeaderTextBold = false;
            this.dgv.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(449, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 130;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // grbButton1
            // 
            this.grbButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.BackgroundImage")));
            this.grbButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grbButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.grbButton1.FlatAppearance.BorderSize = 2;
            this.grbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grbButton1.Location = new System.Drawing.Point(583, 60);
            this.grbButton1.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.MouseDownImage")));
            this.grbButton1.Name = "grbButton1";
            this.grbButton1.NormalImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.NormalImage")));
            this.grbButton1.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.OnFocusImage")));
            this.grbButton1.SelectedImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.SelectedImage")));
            this.grbButton1.Size = new System.Drawing.Size(75, 23);
            this.grbButton1.TabIndex = 131;
            this.grbButton1.Text = "grbButton1";
            this.grbButton1.UseVisualStyleBackColor = true;
            this.grbButton1.Click += new System.EventHandler(this.grbButton1_Click);
            // 
            // LedgerMaster
            // 
            this.AcceptControl = this.Chk_IsActive;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1008, 492);
            this.Controls.Add(this.grbButton1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.TxtLedgerID);
            this.Controls.Add(this.Lbl_LedgerID);
            this.Name = "LedgerMaster";
            this.Text = "Ledger Master";
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.Lbl_LedgerID, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.TxtLedgerID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Controls.SetChildIndex(this.grbButton1, 0);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Gramboo.Controls.GrbTextBox TxtLedgerID;
        private Gramboo.Controls.GrbTextBox TxtLedgerName;
        private System.Windows.Forms.Label Lbl_LedgerName;
        private System.Windows.Forms.Label Lbl_LedgerID;
        private Gramboo.Controls.GrbDataGridView dgv;
        private Gramboo.Controls.GrbCheckBox Chk_IsActive;
        private System.Windows.Forms.Label Lbl_GroupId;
        private Gramboo.Controls.GrbComboBox cmb_GroupId;
        private System.Windows.Forms.Button button1;
        private Gramboo.Controls.GrbButton grbButton1;
    }
}
