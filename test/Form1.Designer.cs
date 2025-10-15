namespace test
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
            this.grbButton1 = new Gramboo.Controls.GrbButton();
            this.grbTextBox3 = new Gramboo.Controls.GrbTextBox();
            this.grbTextBox2 = new Gramboo.Controls.GrbTextBox();
            this.grbTextBox1 = new Gramboo.Controls.GrbTextBox();
            this.grbDataGridView1 = new Gramboo.Controls.GrbDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grbTextBox4 = new Gramboo.Controls.GrbTextBox();
            this.grbTextBox5 = new Gramboo.Controls.GrbTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grbDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // grbButton1
            // 
            this.grbButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.BackgroundImage")));
            this.grbButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.grbButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.grbButton1.FlatAppearance.BorderSize = 2;
            this.grbButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grbButton1.Location = new System.Drawing.Point(489, 373);
            this.grbButton1.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.MouseDownImage")));
            this.grbButton1.Name = "grbButton1";
            this.grbButton1.NormalImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.NormalImage")));
            this.grbButton1.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.OnFocusImage")));
            this.grbButton1.SelectedImage = ((System.Drawing.Image)(resources.GetObject("grbButton1.SelectedImage")));
            this.grbButton1.Size = new System.Drawing.Size(75, 23);
            this.grbButton1.TabIndex = 4;
            this.grbButton1.Text = "grbButton1";
            this.grbButton1.UseVisualStyleBackColor = true;
            this.grbButton1.Click += new System.EventHandler(this.grbButton1_Click);
            // 
            // grbTextBox3
            // 
            this.grbTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grbTextBox3.Location = new System.Drawing.Point(387, 300);
            this.grbTextBox3.Name = "grbTextBox3";
            this.grbTextBox3.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbTextBox3.Size = new System.Drawing.Size(100, 20);
            this.grbTextBox3.TabIndex = 1;
            this.grbTextBox3.Text = "grbTextBox1";
            // 
            // grbTextBox2
            // 
            this.grbTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grbTextBox2.Location = new System.Drawing.Point(175, 299);
            this.grbTextBox2.Name = "grbTextBox2";
            this.grbTextBox2.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbTextBox2.Size = new System.Drawing.Size(100, 20);
            this.grbTextBox2.TabIndex = 1;
            this.grbTextBox2.Text = "grbTextBox1";
            // 
            // grbTextBox1
            // 
            this.grbTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grbTextBox1.Location = new System.Drawing.Point(54, 300);
            this.grbTextBox1.Name = "grbTextBox1";
            this.grbTextBox1.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbTextBox1.Size = new System.Drawing.Size(100, 20);
            this.grbTextBox1.TabIndex = 1;
            this.grbTextBox1.Text = "grbTextBox1";
            // 
            // grbDataGridView1
            // 
            this.grbDataGridView1.AllowGrouping = false;
            this.grbDataGridView1.AllowUserToAddRows = false;
            this.grbDataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grbDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grbDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.grbDataGridView1.DataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("grbDataGridView1.DataFields")));
            this.grbDataGridView1.DisplaySumRowHeader = false;
            this.grbDataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grbDataGridView1.GroupingFields = null;
            this.grbDataGridView1.HiddenDataFields = ((System.Collections.Generic.List<string>)(resources.GetObject("grbDataGridView1.HiddenDataFields")));
            this.grbDataGridView1.Location = new System.Drawing.Point(32, 32);
            this.grbDataGridView1.Name = "grbDataGridView1";
            this.grbDataGridView1.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Transparent;
            this.grbDataGridView1.ShowDelete = true;
            this.grbDataGridView1.ShowEdit = true;
            this.grbDataGridView1.ShowMoveDown = true;
            this.grbDataGridView1.ShowMoveUp = true;
            this.grbDataGridView1.ShowSelectCheckBox = true;
            this.grbDataGridView1.ShowSerialNo = true;
            this.grbDataGridView1.Size = new System.Drawing.Size(455, 261);
            this.grbDataGridView1.SummaryColumns = null;
            this.grbDataGridView1.SummaryRowBackColor = System.Drawing.Color.Empty;
            this.grbDataGridView1.SummaryRowSpace = 0;
            this.grbDataGridView1.SummaryRowVisible = true;
            this.grbDataGridView1.SumRowHeaderText = null;
            this.grbDataGridView1.SumRowHeaderTextBold = false;
            this.grbDataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(617, 109);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // grbTextBox4
            // 
            this.grbTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grbTextBox4.Location = new System.Drawing.Point(374, 73);
            this.grbTextBox4.Name = "grbTextBox4";
            this.grbTextBox4.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbTextBox4.Size = new System.Drawing.Size(100, 20);
            this.grbTextBox4.TabIndex = 1;
            this.grbTextBox4.Text = "grbTextBox1";
            // 
            // grbTextBox5
            // 
            this.grbTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grbTextBox5.Location = new System.Drawing.Point(307, 139);
            this.grbTextBox5.Name = "grbTextBox5";
            this.grbTextBox5.NormalBorderColor = System.Drawing.Color.Gray;
            this.grbTextBox5.Size = new System.Drawing.Size(100, 20);
            this.grbTextBox5.TabIndex = 1;
            this.grbTextBox5.Text = "grbTextBox1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 459);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grbButton1);
            this.Controls.Add(this.grbTextBox3);
            this.Controls.Add(this.grbTextBox5);
            this.Controls.Add(this.grbTextBox4);
            this.Controls.Add(this.grbTextBox2);
            this.Controls.Add(this.grbTextBox1);
            this.Controls.Add(this.grbDataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grbDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gramboo.Controls.GrbDataGridView grbDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private Gramboo.Controls.GrbTextBox grbTextBox1;
        private Gramboo.Controls.GrbTextBox grbTextBox2;
        private Gramboo.Controls.GrbTextBox grbTextBox3;
        private Gramboo.Controls.GrbButton grbButton1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Gramboo.Controls.GrbTextBox grbTextBox4;
        private Gramboo.Controls.GrbTextBox grbTextBox5;
    }
}

