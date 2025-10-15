namespace SAFA.Forms.COM
{
    partial class CustomerSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerSearch));
            this.dgview = new Gramboo.Controls.GrbDataGridView();
            this.LabCls = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.PanBorder1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.PanBorder2 = new System.Windows.Forms.Panel();
            this.PanBorder3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.PanBorder4 = new System.Windows.Forms.Panel();
            this.btnTarget = new System.Windows.Forms.Button();
            this.txt_cardno = new Gramboo.Controls.GrbTextBox();
            this.TxtCode = new Gramboo.Controls.GrbTextBox();
            this.TxtName = new Gramboo.Controls.GrbTextBox();
            this.TxtHouseName = new Gramboo.Controls.GrbTextBox();
            this.TxtAdd1 = new Gramboo.Controls.GrbTextBox();
            this.TxtAdd2 = new Gramboo.Controls.GrbTextBox();
            this.TxtPhone = new Gramboo.Controls.GrbTextBox();
            this.BtnSearch = new Gramboo.Controls.GrbButton();
            this.PanBack = new System.Windows.Forms.Panel();
            this.Cmb = new Gramboo.Controls.GrbComboBox();
            this.TxtCustId = new Gramboo.Controls.GrbTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgview)).BeginInit();
            this.PanBack.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(706, 379);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(729, 377);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(743, 379);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(743, 353);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(706, 353);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(729, 353);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(729, 405);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(732, 405);
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
            this.dgview.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgview.GroupingFields = null;
            this.dgview.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgview.HiddenDataFields")));
            this.dgview.Location = new System.Drawing.Point(6, 147);
            this.dgview.Name = "dgview";
            this.dgview.ReadOnly = true;
            this.dgview.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgview.ShowDelete = false;
            this.dgview.ShowEdit = false;
            this.dgview.ShowMoveDown = false;
            this.dgview.ShowMoveUp = false;
            this.dgview.ShowSelectCheckBox = false;
            this.dgview.ShowSerialNo = false;
            this.dgview.Size = new System.Drawing.Size(726, 372);
            this.dgview.SummaryColumns = null;
            this.dgview.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.dgview.SummaryRowSpace = 0;
            this.dgview.SumRowHeaderText = null;
            this.dgview.SumRowHeaderTextBold = false;
            this.dgview.TabIndex = 8;
            this.dgview.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgview_CellDoubleClick);
            this.dgview.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgview_KeyDown);
            // 
            // LabCls
            // 
            this.LabCls.AutoSize = true;
            this.LabCls.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabCls.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabCls.Location = new System.Drawing.Point(718, 4);
            this.LabCls.Name = "LabCls";
            this.LabCls.Size = new System.Drawing.Size(17, 16);
            this.LabCls.TabIndex = 323;
            this.LabCls.Text = "X";
            this.LabCls.Click += new System.EventHandler(this.LabCls_Click);
            this.LabCls.MouseLeave += new System.EventHandler(this.LabCls_MouseLeave);
            this.LabCls.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LabCls_MouseMove);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panel2.Location = new System.Drawing.Point(3, 40);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(733, 1);
            this.panel2.TabIndex = 99;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(21, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 16);
            this.label8.TabIndex = 97;
            this.label8.Text = "Search Dialogue";
            // 
            // PanBorder1
            // 
            this.PanBorder1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.PanBorder1.Location = new System.Drawing.Point(101, 56);
            this.PanBorder1.Name = "PanBorder1";
            this.PanBorder1.Size = new System.Drawing.Size(620, 3);
            this.PanBorder1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label1.Location = new System.Drawing.Point(49, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 98;
            this.label1.Text = "Targets";
            // 
            // PanBorder2
            // 
            this.PanBorder2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.PanBorder2.Location = new System.Drawing.Point(18, 56);
            this.PanBorder2.Name = "PanBorder2";
            this.PanBorder2.Size = new System.Drawing.Size(30, 3);
            this.PanBorder2.TabIndex = 99;
            // 
            // PanBorder3
            // 
            this.PanBorder3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.PanBorder3.Location = new System.Drawing.Point(18, 100);
            this.PanBorder3.Name = "PanBorder3";
            this.PanBorder3.Size = new System.Drawing.Size(30, 3);
            this.PanBorder3.TabIndex = 102;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label2.Location = new System.Drawing.Point(53, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 101;
            this.label2.Text = "Filters";
            // 
            // PanBorder4
            // 
            this.PanBorder4.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.PanBorder4.Location = new System.Drawing.Point(101, 100);
            this.PanBorder4.Name = "PanBorder4";
            this.PanBorder4.Size = new System.Drawing.Size(620, 3);
            this.PanBorder4.TabIndex = 100;
            // 
            // btnTarget
            // 
            this.btnTarget.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTarget.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTarget.ForeColor = System.Drawing.Color.White;
            this.btnTarget.Location = new System.Drawing.Point(32, 65);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(93, 26);
            this.btnTarget.TabIndex = 104;
            this.btnTarget.TabStop = false;
            this.btnTarget.Text = "CUSTOMER";
            this.btnTarget.UseVisualStyleBackColor = false;
            // 
            // txt_cardno
            // 
            this.txt_cardno.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_cardno.DataField = "";
            this.txt_cardno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cardno.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txt_cardno.Location = new System.Drawing.Point(16, 114);
            this.txt_cardno.Name = "txt_cardno";
            this.txt_cardno.NormalBorderColor = System.Drawing.Color.Gray;
            this.txt_cardno.Size = new System.Drawing.Size(75, 21);
            this.txt_cardno.TabIndex = 0;
            this.txt_cardno.Text = "Card No";
            this.txt_cardno.Enter += new System.EventHandler(this.txt_cardno_Enter);
            this.txt_cardno.Leave += new System.EventHandler(this.txt_cardno_Leave);
            // 
            // TxtCode
            // 
            this.TxtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtCode.DataField = "";
            this.TxtCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCode.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.TxtCode.Location = new System.Drawing.Point(94, 114);
            this.TxtCode.Name = "TxtCode";
            this.TxtCode.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtCode.Size = new System.Drawing.Size(75, 21);
            this.TxtCode.TabIndex = 1;
            this.TxtCode.Text = "Code";
            this.TxtCode.Enter += new System.EventHandler(this.TxtCode_Enter);
            this.TxtCode.Leave += new System.EventHandler(this.TxtCode_Leave);
            // 
            // TxtName
            // 
            this.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtName.DataField = "";
            this.TxtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtName.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.TxtName.Location = new System.Drawing.Point(172, 114);
            this.TxtName.Name = "TxtName";
            this.TxtName.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtName.Size = new System.Drawing.Size(137, 21);
            this.TxtName.TabIndex = 2;
            this.TxtName.Text = "Name";
            this.TxtName.Enter += new System.EventHandler(this.TxtName_Enter);
            this.TxtName.Leave += new System.EventHandler(this.TxtName_Leave);
            // 
            // TxtHouseName
            // 
            this.TxtHouseName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtHouseName.DataField = "";
            this.TxtHouseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtHouseName.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.TxtHouseName.Location = new System.Drawing.Point(312, 114);
            this.TxtHouseName.Name = "TxtHouseName";
            this.TxtHouseName.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtHouseName.Size = new System.Drawing.Size(91, 21);
            this.TxtHouseName.TabIndex = 3;
            this.TxtHouseName.Text = "House Name";
            this.TxtHouseName.Enter += new System.EventHandler(this.TxtHouseName_Enter);
            this.TxtHouseName.Leave += new System.EventHandler(this.TxtHouseName_Leave);
            // 
            // TxtAdd1
            // 
            this.TxtAdd1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtAdd1.DataField = "";
            this.TxtAdd1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAdd1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.TxtAdd1.Location = new System.Drawing.Point(406, 114);
            this.TxtAdd1.Name = "TxtAdd1";
            this.TxtAdd1.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtAdd1.Size = new System.Drawing.Size(91, 21);
            this.TxtAdd1.TabIndex = 4;
            this.TxtAdd1.Text = "Address1";
            this.TxtAdd1.Enter += new System.EventHandler(this.TxtAdd1_Enter);
            this.TxtAdd1.Leave += new System.EventHandler(this.TxtAdd1_Leave);
            // 
            // TxtAdd2
            // 
            this.TxtAdd2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtAdd2.DataField = "";
            this.TxtAdd2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAdd2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.TxtAdd2.Location = new System.Drawing.Point(500, 114);
            this.TxtAdd2.Name = "TxtAdd2";
            this.TxtAdd2.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtAdd2.Size = new System.Drawing.Size(91, 21);
            this.TxtAdd2.TabIndex = 5;
            this.TxtAdd2.Text = "Address2";
            this.TxtAdd2.Enter += new System.EventHandler(this.TxtAdd2_Enter);
            this.TxtAdd2.Leave += new System.EventHandler(this.TxtAdd2_Leave);
            // 
            // TxtPhone
            // 
            this.TxtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtPhone.DataField = "";
            this.TxtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPhone.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.TxtPhone.Location = new System.Drawing.Point(594, 114);
            this.TxtPhone.Name = "TxtPhone";
            this.TxtPhone.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtPhone.Size = new System.Drawing.Size(91, 21);
            this.TxtPhone.TabIndex = 6;
            this.TxtPhone.Text = "Phone Number";
            this.TxtPhone.Enter += new System.EventHandler(this.TxtPhone_Enter);
            this.TxtPhone.Leave += new System.EventHandler(this.TxtPhone_Leave);
            // 
            // BtnSearch
            // 
            this.BtnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BtnSearch.BackgroundImage")));
            this.BtnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.BtnSearch.FlatAppearance.BorderSize = 2;
            this.BtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat; 
            this.BtnSearch.Location = new System.Drawing.Point(688, 109);
            this.BtnSearch.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("BtnSearch.MouseDownImage")));
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.NormalImage = ((System.Drawing.Image)(resources.GetObject("BtnSearch.NormalImage")));
            this.BtnSearch.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("BtnSearch.OnFocusImage")));
            this.BtnSearch.SelectedImage = ((System.Drawing.Image)(resources.GetObject("BtnSearch.SelectedImage")));
            this.BtnSearch.Size = new System.Drawing.Size(35, 32);
            this.BtnSearch.TabIndex = 7;
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // PanBack
            // 
            this.PanBack.Controls.Add(this.Cmb);
            this.PanBack.Controls.Add(this.TxtCustId);
            this.PanBack.Location = new System.Drawing.Point(0, 43);
            this.PanBack.Name = "PanBack";
            this.PanBack.Size = new System.Drawing.Size(738, 488);
            this.PanBack.TabIndex = 30009;
            this.PanBack.Paint += new System.Windows.Forms.PaintEventHandler(this.PanBack_Paint);
            // 
            // Cmb
            // 
            this.Cmb.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Cmb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cmb.FormattingEnabled = true;
            this.Cmb.Location = new System.Drawing.Point(608, 25);
            this.Cmb.Name = "Cmb";
            this.Cmb.NormalBorderColor = System.Drawing.Color.Gray;
            this.Cmb.Size = new System.Drawing.Size(32, 21);
            this.Cmb.TabIndex = 1;
            this.Cmb.TableName = "";
            this.Cmb.Visible = false;
            // 
            // TxtCustId
            // 
            this.TxtCustId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtCustId.Location = new System.Drawing.Point(646, 25);
            this.TxtCustId.Name = "TxtCustId";
            this.TxtCustId.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtCustId.Size = new System.Drawing.Size(10, 20);
            this.TxtCustId.TabIndex = 0;
            this.TxtCustId.Visible = false;
            this.TxtCustId.TextChanged += new System.EventHandler(this.TxtCustId_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Location = new System.Drawing.Point(3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 1);
            this.panel1.TabIndex = 30010;
            // 
            // CustomerSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(235)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(739, 531);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.LabCls);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.BtnSearch);
            this.Controls.Add(this.TxtPhone);
            this.Controls.Add(this.TxtAdd2);
            this.Controls.Add(this.TxtAdd1);
            this.Controls.Add(this.TxtHouseName);
            this.Controls.Add(this.TxtName);
            this.Controls.Add(this.TxtCode);
            this.Controls.Add(this.txt_cardno);
            this.Controls.Add(this.btnTarget);
            this.Controls.Add(this.PanBorder3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PanBorder4);
            this.Controls.Add(this.PanBorder2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PanBorder1);
            this.Controls.Add(this.dgview);
            this.Controls.Add(this.PanBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomerSearch";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CustomerSearch_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CustomerSearch_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CustomerSearch_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CustomerSearch_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CustomerSearch_MouseUp);
            this.Controls.SetChildIndex(this.PanBack, 0);
            this.Controls.SetChildIndex(this.dgview, 0);
            this.Controls.SetChildIndex(this.PanBorder1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.PanBorder2, 0);
            this.Controls.SetChildIndex(this.PanBorder4, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.PanBorder3, 0);
            this.Controls.SetChildIndex(this.btnTarget, 0);
            this.Controls.SetChildIndex(this.txt_cardno, 0);
            this.Controls.SetChildIndex(this.TxtCode, 0);
            this.Controls.SetChildIndex(this.TxtName, 0);
            this.Controls.SetChildIndex(this.TxtHouseName, 0);
            this.Controls.SetChildIndex(this.TxtAdd1, 0);
            this.Controls.SetChildIndex(this.TxtAdd2, 0);
            this.Controls.SetChildIndex(this.TxtPhone, 0);
            this.Controls.SetChildIndex(this.BtnSearch, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.LabCls, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgview)).EndInit();
            this.PanBack.ResumeLayout(false);
            this.PanBack.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gramboo.Controls.GrbDataGridView dgview;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel PanBorder1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel PanBorder2;
        private System.Windows.Forms.Panel PanBorder3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PanBorder4;
        private System.Windows.Forms.Button btnTarget;
        private Gramboo.Controls.GrbTextBox txt_cardno;
        private Gramboo.Controls.GrbTextBox TxtCode;
        private Gramboo.Controls.GrbTextBox TxtName;
        private Gramboo.Controls.GrbTextBox TxtHouseName;
        private Gramboo.Controls.GrbTextBox TxtAdd1;
        private Gramboo.Controls.GrbTextBox TxtAdd2;
        private Gramboo.Controls.GrbButton BtnSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel PanBack;
        private System.Windows.Forms.Label LabCls;
        private System.Windows.Forms.Panel panel1;
        public Gramboo.Controls.GrbTextBox TxtPhone;
        private Gramboo.Controls.GrbTextBox TxtCustId;
        private Gramboo.Controls.GrbComboBox Cmb;
    }
}