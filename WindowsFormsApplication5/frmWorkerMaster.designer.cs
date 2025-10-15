namespace JMS.Forms.PROD.Worker
{
    partial class frmWorkerMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWorkerMaster));
            this.cmbWorkshop = new Gramboo.Controls.GrbComboBox();
            this.cmbstaff = new Gramboo.Controls.GrbComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbSkills = new Gramboo.Controls.GrbComboBox();
            this.dgvSkills = new Gramboo.Controls.GrbDataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSkillID = new Gramboo.Controls.GrbTextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtIsActive = new Gramboo.Controls.GrbTextBox();
            this.txtEntryID = new Gramboo.Controls.GrbTextBox();
            this.cmbpurity = new Gramboo.Controls.GrbComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.grbButton1 = new Gramboo.Controls.GrbButton();
            this.grbTextBox1 = new Gramboo.Controls.GrbTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkills)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtModifiedDate
            // 
            this.txtModifiedDate.Location = new System.Drawing.Point(633, 339);
            // 
            // txtcreatedDate
            // 
            this.txtcreatedDate.Location = new System.Drawing.Point(656, 337);
            // 
            // txtcompId
            // 
            this.txtcompId.Location = new System.Drawing.Point(670, 339);
            // 
            // txtModuserID
            // 
            this.txtModuserID.Location = new System.Drawing.Point(670, 313);
            // 
            // txtCrUserId
            // 
            this.txtCrUserId.Location = new System.Drawing.Point(633, 313);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(656, 313);
            // 
            // txtCounterId
            // 
            this.txtCounterId.Location = new System.Drawing.Point(656, 365);
            // 
            // txtBranchID
            // 
            this.txtBranchID.Location = new System.Drawing.Point(659, 365);
            // 
            // cmbWorkshop
            // 
            this.cmbWorkshop.AcceptBlankValue = false;
            this.cmbWorkshop.Alias = "WorkShopID";
            this.cmbWorkshop.BindingProperty = "SelectedValue";
            this.cmbWorkshop.DataField = "WorkShopID";
            this.cmbWorkshop.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWorkshop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbWorkshop.FormattingEnabled = true;
            this.cmbWorkshop.Location = new System.Drawing.Point(161, 40);
            this.cmbWorkshop.Name = "cmbWorkshop";
            this.cmbWorkshop.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmbWorkshop.Size = new System.Drawing.Size(306, 21);
            this.cmbWorkshop.TabIndex = 89;
            this.cmbWorkshop.TableName = "";
            // 
            // cmbstaff
            // 
            this.cmbstaff.AcceptBlankValue = false;
            this.cmbstaff.BindingProperty = "SelectedValue";
            this.cmbstaff.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbstaff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbstaff.FormattingEnabled = true;
            this.cmbstaff.Location = new System.Drawing.Point(161, 13);
            this.cmbstaff.Name = "cmbstaff";
            this.cmbstaff.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmbstaff.Size = new System.Drawing.Size(306, 21);
            this.cmbstaff.TabIndex = 88;
            this.cmbstaff.TableName = "";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(21, 39);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(110, 16);
            this.label21.TabIndex = 90;
            this.label21.Text = "Workshop Name";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(21, 13);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(110, 16);
            this.label20.TabIndex = 91;
            this.label20.Text = "Employee Name";
            // 
            // cmbSkills
            // 
            this.cmbSkills.AcceptBlankValue = false;
            this.cmbSkills.Alias = "[Skill Name]";
            this.cmbSkills.BindToDataGridview = true;
            this.cmbSkills.CheckDuplicates = true;
            this.cmbSkills.DataField = "Skill Name";
            this.cmbSkills.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSkills.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSkills.FormattingEnabled = true;
            this.cmbSkills.Location = new System.Drawing.Point(72, 37);
            this.cmbSkills.Name = "cmbSkills";
            this.cmbSkills.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmbSkills.Size = new System.Drawing.Size(353, 23);
            this.cmbSkills.TabIndex = 89;
            this.cmbSkills.TableName = "";
            this.cmbSkills.SelectedIndexChanged += new System.EventHandler(this.cmbSkills_SelectedIndexChanged);
            // 
            // dgvSkills
            // 
            this.dgvSkills.AllowGrouping = false;
            this.dgvSkills.AllowUserToAddRows = false;
            this.dgvSkills.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgvSkills.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSkills.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvSkills.DataFields")));
            this.dgvSkills.DisplaySumRowHeader = false;
            this.dgvSkills.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvSkills.FilterList")));
            this.dgvSkills.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvSkills.GroupingFields = null;
            this.dgvSkills.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgvSkills.HiddenDataFields")));
            this.dgvSkills.Location = new System.Drawing.Point(16, 64);
            this.dgvSkills.Name = "dgvSkills";
            this.dgvSkills.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.dgvSkills.ShowDelete = true;
            this.dgvSkills.ShowEdit = true;
            this.dgvSkills.ShowMoveDown = false;
            this.dgvSkills.ShowMoveUp = false;
            this.dgvSkills.ShowSelectCheckBox = false;
            this.dgvSkills.ShowSerialNo = true;
            this.dgvSkills.Size = new System.Drawing.Size(551, 282);
            this.dgvSkills.SummaryColumns = null;
            this.dgvSkills.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.dgvSkills.SummaryRowSpace = 0;
            this.dgvSkills.SumRowHeaderText = null;
            this.dgvSkills.SumRowHeaderTextBold = false;
            this.dgvSkills.TabIndex = 92;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSkillID);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.dgvSkills);
            this.groupBox1.Controls.Add(this.cmbSkills);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBox1.Location = new System.Drawing.Point(24, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(573, 352);
            this.groupBox1.TabIndex = 93;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Skills";
            // 
            // txtSkillID
            // 
            this.txtSkillID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSkillID.CheckDuplicates = true;
            this.txtSkillID.DataField = "SkillID";
            this.txtSkillID.Location = new System.Drawing.Point(293, 9);
            this.txtSkillID.Name = "txtSkillID";
            this.txtSkillID.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtSkillID.Size = new System.Drawing.Size(32, 22);
            this.txtSkillID.TabIndex = 94;
            this.txtSkillID.Visible = false;
            this.txtSkillID.TextChanged += new System.EventHandler(this.txtSkillID_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(435, 34);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(29, 24);
            this.btnAdd.TabIndex = 93;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtIsActive
            // 
            this.txtIsActive.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIsActive.DataField = "IsActive";
            this.txtIsActive.IsIDField = true;
            this.txtIsActive.Location = new System.Drawing.Point(473, 39);
            this.txtIsActive.Name = "txtIsActive";
            this.txtIsActive.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtIsActive.Size = new System.Drawing.Size(32, 20);
            this.txtIsActive.TabIndex = 95;
            this.txtIsActive.Text = "1";
            this.txtIsActive.Visible = false;
            // 
            // txtEntryID
            // 
            this.txtEntryID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEntryID.CheckDuplicates = true;
            this.txtEntryID.DataField = "EntryID";
            this.txtEntryID.IsIDField = true;
            this.txtEntryID.Location = new System.Drawing.Point(473, 65);
            this.txtEntryID.Name = "txtEntryID";
            this.txtEntryID.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtEntryID.Size = new System.Drawing.Size(32, 20);
            this.txtEntryID.TabIndex = 94;
            this.txtEntryID.Visible = false;
            // 
            // cmbpurity
            // 
            this.cmbpurity.AcceptBlankValue = false;
            this.cmbpurity.BindingProperty = "SelectedValue";
            this.cmbpurity.DataField = "PurityId";
            this.cmbpurity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbpurity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbpurity.FormattingEnabled = true;
            this.cmbpurity.Location = new System.Drawing.Point(161, 67);
            this.cmbpurity.Name = "cmbpurity";
            this.cmbpurity.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmbpurity.Size = new System.Drawing.Size(306, 21);
            this.cmbpurity.TabIndex = 96;
            this.cmbpurity.TableName = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label4.Location = new System.Drawing.Point(21, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 97;
            this.label4.Text = "Purity";
            // 
            // grbButton1
            // 
            this.grbButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.BackgroundImage")));
            this.grbButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grbButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.grbButton1.FlatAppearance.BorderSize = 2;
            this.grbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grbButton1.Location = new System.Drawing.Point(536, 67);
            this.grbButton1.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.MouseDownImage")));
            this.grbButton1.Name = "grbButton1";
            this.grbButton1.NormalImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.NormalImage")));
            this.grbButton1.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.OnFocusImage")));
            this.grbButton1.SelectedImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.SelectedImage")));
            this.grbButton1.Size = new System.Drawing.Size(75, 23);
            this.grbButton1.TabIndex = 98;
            this.grbButton1.Text = "grbButton1";
            this.grbButton1.UseVisualStyleBackColor = true;
            this.grbButton1.Click += new System.EventHandler(this.grbButton1_Click);
            // 
            // grbTextBox1
            // 
            this.grbTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grbTextBox1.DataField = "StaffId";
            this.grbTextBox1.IsIDField = true;
            this.grbTextBox1.Location = new System.Drawing.Point(536, 34);
            this.grbTextBox1.Name = "grbTextBox1";
            this.grbTextBox1.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbTextBox1.Size = new System.Drawing.Size(100, 20);
            this.grbTextBox1.TabIndex = 99;
            this.grbTextBox1.Text = "grbTextBox1";
            // 
            // frmWorkerMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(626, 504);
            this.Controls.Add(this.grbTextBox1);
            this.Controls.Add(this.grbButton1);
            this.Controls.Add(this.cmbpurity);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtEntryID);
            this.Controls.Add(this.txtIsActive);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbWorkshop);
            this.Controls.Add(this.cmbstaff);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Name = "frmWorkerMaster";
            this.Text = "Worker Master";
            this.Controls.SetChildIndex(this.label20, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.cmbstaff, 0);
            this.Controls.SetChildIndex(this.cmbWorkshop, 0);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.txtIsActive, 0);
            this.Controls.SetChildIndex(this.txtEntryID, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.cmbpurity, 0);
            this.Controls.SetChildIndex(this.grbButton1, 0);
            this.Controls.SetChildIndex(this.grbTextBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSkills)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gramboo.Controls.GrbComboBox cmbWorkshop;
        private Gramboo.Controls.GrbComboBox cmbstaff;
        internal System.Windows.Forms.Label label21;
        internal System.Windows.Forms.Label label20;
        private Gramboo.Controls.GrbComboBox cmbSkills;
        private Gramboo.Controls.GrbDataGridView dgvSkills;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private Gramboo.Controls.GrbTextBox txtEntryID;
        private Gramboo.Controls.GrbTextBox txtSkillID;
        private Gramboo.Controls.GrbTextBox txtIsActive;
        private Gramboo.Controls.GrbComboBox cmbpurity;
        private System.Windows.Forms.Label label4;
        private Gramboo.Controls.GrbButton grbButton1;
        private Gramboo.Controls.GrbTextBox grbTextBox1;
    }
}
