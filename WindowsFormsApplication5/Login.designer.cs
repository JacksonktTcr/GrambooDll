namespace Kallans.Forms.GENERALFORMS
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CmbDepartment = new Gramboo.Controls.GrbComboBox();
            this.cmbCompany = new Gramboo.Controls.GrbComboBox();
            this.txtUserName = new Gramboo.Controls.GrbTextBox();
            this.btnCancel = new Gramboo.Controls.GrbButton();
            this.txtPasswd = new Gramboo.Controls.GrbTextBox();
            this.btnLogin = new Gramboo.Controls.GrbButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCounter = new Gramboo.Controls.GrbComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbBranch = new Gramboo.Controls.GrbComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CmbDepartment);
            this.groupBox1.Controls.Add(this.cmbCompany);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.txtPasswd);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbCounter);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbBranch);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(97, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(431, 266);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(19, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 15);
            this.label8.TabIndex = 3;
            this.label8.Text = "Department";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            // 
            // CmbDepartment
            // 
            this.CmbDepartment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CmbDepartment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CmbDepartment.FormattingEnabled = true;
            this.CmbDepartment.Location = new System.Drawing.Point(115, 100);
            this.CmbDepartment.Name = "CmbDepartment";
            this.CmbDepartment.NormalBorderColor = System.Drawing.Color.Gray;
            this.CmbDepartment.Size = new System.Drawing.Size(295, 21);
            this.CmbDepartment.TabIndex = 3;
            this.CmbDepartment.TableName = "";
            this.CmbDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbDepartment_KeyDown);
            // 
            // cmbCompany
            // 
            this.cmbCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbCompany.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCompany.FormattingEnabled = true;
            this.cmbCompany.Location = new System.Drawing.Point(115, 75);
            this.cmbCompany.Name = "cmbCompany";
            this.cmbCompany.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmbCompany.Size = new System.Drawing.Size(295, 21);
            this.cmbCompany.TabIndex = 2;
            this.cmbCompany.TableName = "";
            this.cmbCompany.SelectedValueChanged += new System.EventHandler(this.cmbCompany_SelectedValueChanged);
            this.cmbCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCompany_KeyDown);
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.Location = new System.Drawing.Point(115, 25);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtUserName.Size = new System.Drawing.Size(295, 20);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            this.txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserName_KeyPress);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(294, 200);
            this.btnCancel.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.MouseDownImage")));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.NormalImage")));
            this.btnCancel.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.OnFocusImage")));
            this.btnCancel.SelectedImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.SelectedImage")));
            this.btnCancel.Size = new System.Drawing.Size(87, 34);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtPasswd
            // 
            this.txtPasswd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPasswd.Location = new System.Drawing.Point(115, 50);
            this.txtPasswd.Name = "txtPasswd";
            this.txtPasswd.NormalBorderColor = System.Drawing.Color.Gray;
            this.txtPasswd.PasswordChar = '*';
            this.txtPasswd.Size = new System.Drawing.Size(295, 20);
            this.txtPasswd.TabIndex = 1;
            this.txtPasswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPasswd_KeyDown);
            // 
            // btnLogin
            // 
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(219)))), ((int)(((byte)(229)))));
            this.btnLogin.FlatAppearance.BorderSize = 2;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.Location = new System.Drawing.Point(201, 200);
            this.btnLogin.MouseDownImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.MouseDownImage")));
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.NormalImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.NormalImage")));
            this.btnLogin.OnFocusImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.OnFocusImage")));
            this.btnLogin.SelectedImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.SelectedImage")));
            this.btnLogin.Size = new System.Drawing.Size(87, 34);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(19, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // cmbCounter
            // 
            this.cmbCounter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCounter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCounter.FormattingEnabled = true;
            this.cmbCounter.Location = new System.Drawing.Point(115, 150);
            this.cmbCounter.Name = "cmbCounter";
            this.cmbCounter.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmbCounter.Size = new System.Drawing.Size(295, 21);
            this.cmbCounter.TabIndex = 5;
            this.cmbCounter.TableName = "";
            this.cmbCounter.SelectedValueChanged += new System.EventHandler(this.cmbCounter_SelectedValueChanged);
            this.cmbCounter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCounter_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Branch";
            // 
            // cmbBranch
            // 
            this.cmbBranch.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBranch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Location = new System.Drawing.Point(115, 125);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.NormalBorderColor = System.Drawing.Color.Gray;
            this.cmbBranch.Size = new System.Drawing.Size(295, 21);
            this.cmbBranch.TabIndex = 4;
            this.cmbBranch.TableName = "";
            this.cmbBranch.SelectedValueChanged += new System.EventHandler(this.cmbBranch_SelectedValueChanged);
            this.cmbBranch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbBranch_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Counter";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Company";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 366);
            this.Controls.Add(this.groupBox1);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Login_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Login_KeyDown);
            this.Resize += new System.EventHandler(this.Login_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private Gramboo.Controls.GrbTextBox txtUserName;
        private Gramboo.Controls.GrbButton btnCancel;
        private Gramboo.Controls.GrbTextBox txtPasswd;
        private Gramboo.Controls.GrbButton btnLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private Gramboo.Controls.GrbComboBox CmbDepartment;
        public Gramboo.Controls.GrbComboBox cmbCompany;
        public Gramboo.Controls.GrbComboBox cmbCounter;
        public Gramboo.Controls.GrbComboBox cmbBranch;
    }
}