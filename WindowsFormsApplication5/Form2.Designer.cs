namespace WindowsFormsApplication5
{
    partial class Form2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grbRadioButtonGroup1 = new Gramboo.Controls.GrbRadioButtonGroup();
            this.grbRadioButton2 = new Gramboo.Controls.GrbRadioButton();
            this.grbRadioButton1 = new Gramboo.Controls.GrbRadioButton();
            this.grbDataGridView1 = new Gramboo.Controls.GrbDataGridView();
            this.grbComboBox1 = new Gramboo.Controls.GrbComboBox();
            this.grbTextBox1 = new Gramboo.Controls.GrbTextBox();
            this.grbButton1 = new Gramboo.Controls.GrbButton();
            this.grbPictureBox1 = new Gramboo.Controls.GrbPictureBox();
            this.grbRadioButtonGroup1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grbDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grbPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // grbRadioButtonGroup1
            // 
            this.grbRadioButtonGroup1.BindingProperty = "Value";
            this.grbRadioButtonGroup1.Controls.Add(this.grbRadioButton2);
            this.grbRadioButtonGroup1.Controls.Add(this.grbRadioButton1);
            this.grbRadioButtonGroup1.DataField = "PaymentMode";
            this.grbRadioButtonGroup1.DefaultRadioButton = this.grbRadioButton1;
            this.grbRadioButtonGroup1.Location = new System.Drawing.Point(198, 28);
            this.grbRadioButtonGroup1.Name = "grbRadioButtonGroup1";
            this.grbRadioButtonGroup1.Size = new System.Drawing.Size(200, 100);
            this.grbRadioButtonGroup1.TabIndex = 17;
            this.grbRadioButtonGroup1.TableName = null;
            this.grbRadioButtonGroup1.TabStop = false;
            this.grbRadioButtonGroup1.Text = "grbRadioButtonGroup1";
            this.grbRadioButtonGroup1.Value = "1";
            // 
            // grbRadioButton2
            // 
            this.grbRadioButton2.GroupName = null;
            this.grbRadioButton2.Location = new System.Drawing.Point(37, 70);
            this.grbRadioButton2.Name = "grbRadioButton2";
            this.grbRadioButton2.Size = new System.Drawing.Size(104, 24);
            this.grbRadioButton2.TabIndex = 1;
            this.grbRadioButton2.Text = "grbRadioButton2";
            this.grbRadioButton2.TrueValue = "2";
            this.grbRadioButton2.UseVisualStyleBackColor = true;
            // 
            // grbRadioButton1
            // 
            this.grbRadioButton1.Checked = true;
            this.grbRadioButton1.GroupName = null;
            this.grbRadioButton1.Location = new System.Drawing.Point(25, 19);
            this.grbRadioButton1.Name = "grbRadioButton1";
            this.grbRadioButton1.Size = new System.Drawing.Size(104, 24);
            this.grbRadioButton1.TabIndex = 0;
            this.grbRadioButton1.TabStop = true;
            this.grbRadioButton1.Text = "grbRadioButton1";
            this.grbRadioButton1.TrueValue = "1";
            this.grbRadioButton1.UseVisualStyleBackColor = true;
            // 
            // grbDataGridView1
            // 
            this.grbDataGridView1.AllowGrouping = false;
            this.grbDataGridView1.AllowUserToAddRows = false;
            this.grbDataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grbDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grbDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grbDataGridView1.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("grbDataGridView1.DataFields")));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.RoyalBlue;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grbDataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.grbDataGridView1.DisplaySumRowHeader = false;
            this.grbDataGridView1.FilterList = ((System.Collections.Generic.List<string>)(resources.GetObject("grbDataGridView1.FilterList")));
            this.grbDataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grbDataGridView1.GroupingFields = null;
            this.grbDataGridView1.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("grbDataGridView1.HiddenDataFields")));
            this.grbDataGridView1.Location = new System.Drawing.Point(120, 219);
            this.grbDataGridView1.Name = "grbDataGridView1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grbDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grbDataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.grbDataGridView1.ShowDelete = false;
            this.grbDataGridView1.ShowEdit = false;
            this.grbDataGridView1.ShowMoveDown = false;
            this.grbDataGridView1.ShowMoveUp = false;
            this.grbDataGridView1.ShowSelectCheckBox = false;
            this.grbDataGridView1.ShowSerialNo = false;
            this.grbDataGridView1.Size = new System.Drawing.Size(240, 150);
            this.grbDataGridView1.SummaryColumns = null;
            this.grbDataGridView1.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.grbDataGridView1.SummaryRowSpace = 0;
            this.grbDataGridView1.SumRowHeaderText = null;
            this.grbDataGridView1.SumRowHeaderTextBold = false;
            this.grbDataGridView1.TabIndex = 18;
            // 
            // grbComboBox1
            // 
            this.grbComboBox1.AcceptBlankValue = false;
            this.grbComboBox1.BindingProperty = "SelectedValue";
            this.grbComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.grbComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grbComboBox1.FormattingEnabled = true;
            this.grbComboBox1.Location = new System.Drawing.Point(421, 129);
            this.grbComboBox1.Name = "grbComboBox1";
            this.grbComboBox1.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbComboBox1.Size = new System.Drawing.Size(100, 21);
            this.grbComboBox1.TabIndex = 19;
            this.grbComboBox1.TableName = "";
            // 
            // grbTextBox1
            // 
            this.grbTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grbTextBox1.InputMask = Gramboo.Controls.GrbTextBox.Mask.Decimal;
            this.grbTextBox1.Location = new System.Drawing.Point(421, 306);
            this.grbTextBox1.Name = "grbTextBox1";
            this.grbTextBox1.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbTextBox1.Size = new System.Drawing.Size(100, 20);
            this.grbTextBox1.TabIndex = 20;
            this.grbTextBox1.Text = "0";
            this.grbTextBox1.TextChanged += new System.EventHandler(this.grbTextBox1_TextChanged);
            // 
            // grbButton1
            // 
            this.grbButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.BackgroundImage")));
            this.grbButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grbButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.grbButton1.FlatAppearance.BorderSize = 2;
            this.grbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grbButton1.Location = new System.Drawing.Point(263, 167);
            this.grbButton1.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.MouseDownImage")));
            this.grbButton1.Name = "grbButton1";
            this.grbButton1.NormalImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.NormalImage")));
            this.grbButton1.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.OnFocusImage")));
            this.grbButton1.SelectedImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.SelectedImage")));
            this.grbButton1.Size = new System.Drawing.Size(75, 23);
            this.grbButton1.TabIndex = 21;
            this.grbButton1.Text = "grbButton1";
            this.grbButton1.UseVisualStyleBackColor = true;
            this.grbButton1.Click += new System.EventHandler(this.grbButton1_Click);
            // 
            // grbPictureBox1
            // 
            this.grbPictureBox1.Browsable = true;
            this.grbPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("grbPictureBox1.Image")));
            this.grbPictureBox1.Location = new System.Drawing.Point(326, 47);
            this.grbPictureBox1.Name = "grbPictureBox1";
            this.grbPictureBox1.Size = new System.Drawing.Size(282, 175);
            this.grbPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.grbPictureBox1.TabIndex = 22;
            this.grbPictureBox1.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(626, 444);
            this.Controls.Add(this.grbPictureBox1);
            this.Controls.Add(this.grbButton1);
            this.Controls.Add(this.grbTextBox1);
            this.Controls.Add(this.grbComboBox1);
            this.Controls.Add(this.grbDataGridView1);
            this.Controls.Add(this.grbRadioButtonGroup1);
            this.Name = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.Controls.SetChildIndex(this.txtUserName, 0);
            this.Controls.SetChildIndex(this.txtCrUserId, 0);
            this.Controls.SetChildIndex(this.txtModuserID, 0);
            this.Controls.SetChildIndex(this.txtcompId, 0);
            this.Controls.SetChildIndex(this.txtcreatedDate, 0);
            this.Controls.SetChildIndex(this.txtModifiedDate, 0);
            this.Controls.SetChildIndex(this.txtBranchID, 0);
            this.Controls.SetChildIndex(this.txtCounterId, 0);
            this.Controls.SetChildIndex(this.grbRadioButtonGroup1, 0);
            this.Controls.SetChildIndex(this.grbDataGridView1, 0);
            this.Controls.SetChildIndex(this.grbComboBox1, 0);
            this.Controls.SetChildIndex(this.grbTextBox1, 0);
            this.Controls.SetChildIndex(this.grbButton1, 0);
            this.Controls.SetChildIndex(this.grbPictureBox1, 0);
            this.grbRadioButtonGroup1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grbDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grbPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gramboo.Controls.GrbRadioButtonGroup grbRadioButtonGroup1;
        private Gramboo.Controls.GrbRadioButton grbRadioButton2;
        private Gramboo.Controls.GrbRadioButton grbRadioButton1;
        private Gramboo.Controls.GrbDataGridView grbDataGridView1;
        private Gramboo.Controls.GrbComboBox grbComboBox1;
        private Gramboo.Controls.GrbTextBox grbTextBox1;
        private Gramboo.Controls.GrbButton grbButton1;
        private Gramboo.Controls.GrbPictureBox grbPictureBox1;
    }
}
