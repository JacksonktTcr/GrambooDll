namespace FA
{
    partial class GroupMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupMaster));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Chk_IsActive = new Gramboo.Controls.GrbCheckBox();
            this.TxtGroupName = new Gramboo.Controls.GrbTextBox();
            this.Lbl_Under = new System.Windows.Forms.Label();
            this.lbl_ItemName = new System.Windows.Forms.Label();
            this.Cmb_Under = new Gramboo.Controls.GrbComboBox();
            this.Lbl_GroupName = new System.Windows.Forms.Label();
            this.Cmb_CategoryName = new Gramboo.Controls.GrbComboBox();
            this.chk_offGross = new Gramboo.Controls.GrbCheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv = new Gramboo.Controls.GrbDataGridView();
            this.TxtGroupID = new Gramboo.Controls.GrbTextBox();
            this.Lbl_GroupID = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
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
            this.groupBox1.Controls.Add(this.Chk_IsActive);
            this.groupBox1.Controls.Add(this.TxtGroupName);
            this.groupBox1.Controls.Add(this.Lbl_Under);
            this.groupBox1.Controls.Add(this.lbl_ItemName);
            this.groupBox1.Controls.Add(this.Cmb_Under);
            this.groupBox1.Controls.Add(this.Lbl_GroupName);
            this.groupBox1.Controls.Add(this.Cmb_CategoryName);
            this.groupBox1.Controls.Add(this.chk_offGross);
            this.groupBox1.Location = new System.Drawing.Point(7, -1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(358, 123);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // Chk_IsActive
            // 
            this.Chk_IsActive.AcceptBlankValue = false;
            this.Chk_IsActive.BindingProperty = "Checked";
            this.Chk_IsActive.Checked = true;
            this.Chk_IsActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_IsActive.DataField = "IsActive";
            this.Chk_IsActive.FalseValue = "0";
            this.Chk_IsActive.Location = new System.Drawing.Point(227, 99);
            this.Chk_IsActive.Name = "Chk_IsActive";
            this.Chk_IsActive.Size = new System.Drawing.Size(75, 20);
            this.Chk_IsActive.TabIndex = 4;
            this.Chk_IsActive.TableName = null;
            this.Chk_IsActive.Text = "Is Active";
            this.Chk_IsActive.TrueValue = "1";
            this.Chk_IsActive.UseVisualStyleBackColor = true;
            this.Chk_IsActive.Value = "1";
            // 
            // TxtGroupName
            // 
            this.TxtGroupName.AcceptBlankValue = false;
            this.TxtGroupName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtGroupName.DataField = "Acc_GroupName";
            this.TxtGroupName.Location = new System.Drawing.Point(126, 18);
            this.TxtGroupName.Name = "TxtGroupName";
            this.TxtGroupName.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtGroupName.Size = new System.Drawing.Size(195, 20);
            this.TxtGroupName.TabIndex = 0;
            // 
            // Lbl_Under
            // 
            this.Lbl_Under.AutoSize = true;
            this.Lbl_Under.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Under.Location = new System.Drawing.Point(7, 45);
            this.Lbl_Under.Name = "Lbl_Under";
            this.Lbl_Under.Size = new System.Drawing.Size(45, 16);
            this.Lbl_Under.TabIndex = 136;
            this.Lbl_Under.Text = "Under";
            // 
            // lbl_ItemName
            // 
            this.lbl_ItemName.AutoSize = true;
            this.lbl_ItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ItemName.Location = new System.Drawing.Point(7, 72);
            this.lbl_ItemName.Name = "lbl_ItemName";
            this.lbl_ItemName.Size = new System.Drawing.Size(103, 16);
            this.lbl_ItemName.TabIndex = 140;
            this.lbl_ItemName.Text = "Category Name";
            // 
            // Cmb_Under
            // 
            this.Cmb_Under.BindingProperty = "SelectedValue";
            this.Cmb_Under.DataField = "Acc_Under";
            this.Cmb_Under.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Cmb_Under.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cmb_Under.FormattingEnabled = true;
            this.Cmb_Under.Location = new System.Drawing.Point(126, 45);
            this.Cmb_Under.Name = "Cmb_Under";
            this.Cmb_Under.NormalBorderColor = System.Drawing.Color.Gray;
            this.Cmb_Under.Size = new System.Drawing.Size(195, 21);
            this.Cmb_Under.TabIndex = 1;
            this.Cmb_Under.TableName = "";
            // 
            // Lbl_GroupName
            // 
            this.Lbl_GroupName.AutoSize = true;
            this.Lbl_GroupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GroupName.Location = new System.Drawing.Point(7, 18);
            this.Lbl_GroupName.Name = "Lbl_GroupName";
            this.Lbl_GroupName.Size = new System.Drawing.Size(85, 16);
            this.Lbl_GroupName.TabIndex = 138;
            this.Lbl_GroupName.Text = "Group Name";
            // 
            // Cmb_CategoryName
            // 
            this.Cmb_CategoryName.AcceptBlankValue = false;
            this.Cmb_CategoryName.BindingProperty = "SelectedValue";
            this.Cmb_CategoryName.DataField = "Acc_CategoryId";
            this.Cmb_CategoryName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Cmb_CategoryName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cmb_CategoryName.FormattingEnabled = true;
            this.Cmb_CategoryName.Location = new System.Drawing.Point(126, 72);
            this.Cmb_CategoryName.Name = "Cmb_CategoryName";
            this.Cmb_CategoryName.NormalBorderColor = System.Drawing.Color.Gray;
            this.Cmb_CategoryName.Size = new System.Drawing.Size(196, 21);
            this.Cmb_CategoryName.TabIndex = 2;
            this.Cmb_CategoryName.TableName = "";
            // 
            // chk_offGross
            // 
            this.chk_offGross.AcceptBlankValue = false;
            this.chk_offGross.BindingProperty = "Checked";
            this.chk_offGross.Checked = true;
            this.chk_offGross.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_offGross.DataField = "Acc_AffGross";
            this.chk_offGross.FalseValue = "0";
            this.chk_offGross.Location = new System.Drawing.Point(126, 99);
            this.chk_offGross.Name = "chk_offGross";
            this.chk_offGross.Size = new System.Drawing.Size(75, 20);
            this.chk_offGross.TabIndex = 3;
            this.chk_offGross.TableName = null;
            this.chk_offGross.Text = "Off Gross";
            this.chk_offGross.TrueValue = "1";
            this.chk_offGross.UseVisualStyleBackColor = true;
            this.chk_offGross.Value = "1";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dgv);
            this.groupBox2.Location = new System.Drawing.Point(7, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(989, 356);
            this.groupBox2.TabIndex = 1;
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
            this.dgv.DisplaySumRowHeader = false;
            this.dgv.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.FilterList")));
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgv.GroupingFields = null;
            this.dgv.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("dgv.HiddenDataFields")));
            this.dgv.Location = new System.Drawing.Point(9, 14);
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
            this.dgv.ShowDelete = false;
            this.dgv.ShowEdit = false;
            this.dgv.ShowMoveDown = false;
            this.dgv.ShowMoveUp = false;
            this.dgv.ShowSelectCheckBox = false;
            this.dgv.ShowSerialNo = true;
            this.dgv.Size = new System.Drawing.Size(974, 336);
            this.dgv.SummaryColumns = null;
            this.dgv.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.dgv.SummaryRowSpace = 0;
            this.dgv.SumRowHeaderText = null;
            this.dgv.SumRowHeaderTextBold = false;
            this.dgv.TabIndex = 0;
            // 
            // TxtGroupID
            // 
            this.TxtGroupID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtGroupID.DataField = "Acc_GroupId";
            this.TxtGroupID.IsIDField = true;
            this.TxtGroupID.Location = new System.Drawing.Point(754, 27);
            this.TxtGroupID.Name = "TxtGroupID";
            this.TxtGroupID.NormalBorderColor = System.Drawing.Color.Gray;
            this.TxtGroupID.Size = new System.Drawing.Size(125, 20);
            this.TxtGroupID.TabIndex = 125;
            this.TxtGroupID.Visible = false;
            // 
            // Lbl_GroupID
            // 
            this.Lbl_GroupID.AutoSize = true;
            this.Lbl_GroupID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_GroupID.Location = new System.Drawing.Point(673, 27);
            this.Lbl_GroupID.Name = "Lbl_GroupID";
            this.Lbl_GroupID.Size = new System.Drawing.Size(61, 16);
            this.Lbl_GroupID.TabIndex = 133;
            this.Lbl_GroupID.Text = "Group ID";
            this.Lbl_GroupID.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(430, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 134;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GroupMaster
            // 
            this.AcceptControl = this.Chk_IsActive;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1008, 492);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TxtGroupID);
            this.Controls.Add(this.Lbl_GroupID);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "GroupMaster";
            this.Text = "Group Master";
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.groupBox2, 0);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.Lbl_GroupID, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.TxtGroupID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.button1, 0);
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
        private Gramboo.Controls.GrbTextBox TxtGroupID;
        private System.Windows.Forms.Label Lbl_GroupName;
        private Gramboo.Controls.GrbComboBox Cmb_Under;
        private System.Windows.Forms.Label Lbl_GroupID;
        private Gramboo.Controls.GrbTextBox TxtGroupName;
        private System.Windows.Forms.Label Lbl_Under;
        private Gramboo.Controls.GrbDataGridView dgv;
        private Gramboo.Controls.GrbCheckBox chk_offGross;
        private System.Windows.Forms.Label lbl_ItemName;
        private Gramboo.Controls.GrbCheckBox Chk_IsActive;
        private Gramboo.Controls.GrbComboBox Cmb_CategoryName;
        private System.Windows.Forms.Button button1;
    }
}
