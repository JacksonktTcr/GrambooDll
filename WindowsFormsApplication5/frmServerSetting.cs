
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GlassMessage;
using System.Data.SqlClient;

namespace Gramboo
{

    public  class frmServerSetting 
    {
        

        public static DialogResult Show(  ref string DbAServerName, ref string DbADBName,  ref string DbLAServerName, ref string DbLADBName ,  ref string DbBServerName, ref string DbBDBName, ref string DbLPassword, ref string DbAPassword, ref string DbBPassword)
        {
            InputBoxValidation validation = delegate(string val)
            {
                if (val == "")
                    return "Value cannot be empty."; 
                return "";
            };
            return Show(ref DbAServerName, ref  DbADBName, ref DbLAServerName, ref DbLADBName,ref DbBServerName, ref DbBDBName, ref   DbLPassword, ref   DbAPassword, ref   DbBPassword, validation);
        }

      
 

        public static DialogResult Show( ref string DbAServerName, ref string DbADBName,  ref string DbLAServerName, ref string DbLADBName ,  ref string DbBServerName, ref string DbBDBName, ref string DbLPassword, ref string DbAPassword, ref string DbBPassword,
                                     InputBoxValidation validation)
        {

            Form form = new Form();
         Label   label1 = new System.Windows.Forms.Label();
           TextBox  txtServerLocal = new System.Windows.Forms.TextBox();
           GroupBox groupBox1 = new System.Windows.Forms.GroupBox();
           Button  btnCancel = new System.Windows.Forms.Button();
           Button btnOk = new System.Windows.Forms.Button();
           TextBox txtDBLocal = new System.Windows.Forms.TextBox();
           Label label4 = new System.Windows.Forms.Label();

            TextBox txtDBLocalPw = new System.Windows.Forms.TextBox();
            Label DBLocalPw = new System.Windows.Forms.Label();

            TextBox txtServerB = new System.Windows.Forms.TextBox();
           Label label3 = new System.Windows.Forms.Label();
            TextBox txtServerA = new System.Windows.Forms.TextBox();
           Label label2 = new System.Windows.Forms.Label();
           PictureBox pictureBox1 = new System.Windows.Forms.PictureBox();
           Button button1 = new System.Windows.Forms.Button();
           TextBox txtDBA = new System.Windows.Forms.TextBox();

            TextBox txtDBAPw = new System.Windows.Forms.TextBox();
            Label DBAlPw = new System.Windows.Forms.Label();

            Label label5 = new System.Windows.Forms.Label();
           TextBox txtDB2 = new System.Windows.Forms.TextBox();
           Label label6 = new System.Windows.Forms.Label();

            TextBox txtDBBPw = new System.Windows.Forms.TextBox();
            Label DBBPw = new System.Windows.Forms.Label();

            groupBox1.SuspendLayout();
            
            txtServerLocal.Text = DbLAServerName;
            txtServerA.Text = DbAServerName;
            txtServerB.Text = DbBServerName;
            txtDBLocal.Text = DbLADBName;
            txtDBA.Text = DbADBName;
            txtDB2.Text = DbBDBName;

            txtDBLocalPw.Text = DbLPassword;
            txtDBAPw.Text = DbAPassword;
            txtDBBPw.Text = DbBPassword;

            btnOk.Text = "OK";
            btnCancel.Text = "Cancel";
            btnOk.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            // label1
            // 
           label1.AutoSize = true;
           label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
           label1.Location = new System.Drawing.Point(19, 34);
           label1.Name = "label1";
           label1.Size = new System.Drawing.Size(79, 17);
           label1.TabIndex = 0;
           label1.Text = "Local Server";
            // 
            // txtServerLocal
            // 
           txtServerLocal.Location = new System.Drawing.Point(135, 34);
           txtServerLocal.Name = "txtServerLocal";
           txtServerLocal.Size = new System.Drawing.Size(200, 23);
           txtServerLocal.TabIndex = 0;
            // 
            // groupBox1
            // 
           groupBox1.Controls.Add(txtDB2);
           groupBox1.Controls.Add(label6);
           groupBox1.Controls.Add(txtDBA);
           groupBox1.Controls.Add(label5);
           groupBox1.Controls.Add(btnCancel);
           groupBox1.Controls.Add(btnOk);
           groupBox1.Controls.Add(txtDBLocal);
           groupBox1.Controls.Add(label4);
           groupBox1.Controls.Add(txtServerB);
           groupBox1.Controls.Add(label3);
           groupBox1.Controls.Add(txtServerA);
           groupBox1.Controls.Add(label2);
           groupBox1.Controls.Add(txtServerLocal);
           groupBox1.Controls.Add(label1);


            groupBox1.Controls.Add(txtDBLocalPw);
            groupBox1.Controls.Add(DBLocalPw);


            groupBox1.Controls.Add(txtDBAPw);
            groupBox1.Controls.Add(DBAlPw);


            groupBox1.Controls.Add(txtDBBPw);
            groupBox1.Controls.Add(DBBPw);

            groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           groupBox1.ForeColor = System.Drawing.Color.Blue;
           groupBox1.Location = new System.Drawing.Point(162, 12);
           groupBox1.Name = "groupBox1";
           groupBox1.Size = new System.Drawing.Size(357, 246);
           groupBox1.TabIndex = 0;
           groupBox1.TabStop = false;
           groupBox1.Text = "Database Properties";
            // 
            // btnCancel
            // 
           btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
           btnCancel.Location = new System.Drawing.Point(271, 208);
           btnCancel.Name = "btnCancel";
           btnCancel.Size = new System.Drawing.Size(64, 23);
           btnCancel.TabIndex = 5;
           btnCancel.Text = "Cancel";
           btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
           btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
           btnOk.Location = new System.Drawing.Point(201, 208);
           btnOk.Name = "btnOk";
           btnOk.Size = new System.Drawing.Size(64, 23);
           btnOk.TabIndex = 4;
           btnOk.Text = "OK";
           btnOk.UseVisualStyleBackColor = true;
            // 
            // txtDBLocal
            // 
           txtDBLocal.Location = new System.Drawing.Point(135, 121);
           txtDBLocal.Name = "txtDBLocal";
           //txtDBLocal.PasswordChar = '*';
           txtDBLocal.Size = new System.Drawing.Size(200, 23);
           txtDBLocal.TabIndex = 3;
            // 
            // label4
            // 
           label4.AutoSize = true;
           label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
           label4.Location = new System.Drawing.Point(19, 121);
           label4.Name = "label4";
           label4.Size = new System.Drawing.Size(58, 17);
           label4.TabIndex = 0;
           label4.Text = "Local DB";
            // 
            // txtServerB
            // 
           txtServerB.Location = new System.Drawing.Point(135, 92);
           txtServerB.Name = "txtServerB";
           txtServerB.Size = new System.Drawing.Size(200, 23);
           txtServerB.TabIndex = 2;
            // 
            // label3
            // 
           label3.AutoSize = true;
           label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
           label3.Location = new System.Drawing.Point(19, 92);
           label3.Name = "label3";
           label3.Size = new System.Drawing.Size(56, 17);
           label3.TabIndex = 0;
           label3.Text = "Server 2";
            // 
            // txtServerA
            // 
           txtServerA.Location = new System.Drawing.Point(135, 63);
           txtServerA.Name = "txtServerA";
           txtServerA.Size = new System.Drawing.Size(200, 23);
           txtServerA.TabIndex = 1;
            // 
            // label2
            // 
           label2.AutoSize = true;
           label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
           label2.Location = new System.Drawing.Point(19, 63);
           label2.Name = "label2";
           label2.Size = new System.Drawing.Size(56, 17);
           label2.TabIndex = 0;
           label2.Text = "Server 1";
            // 
            // pictureBox1
            // 
           //pictureBox1.BackgroundImage = global::REGAL.Properties.Resources.Tools;
           pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
           pictureBox1.Location = new System.Drawing.Point(35, 12);
           pictureBox1.Name = "pictureBox1";
           pictureBox1.Size = new System.Drawing.Size(82, 79);
           pictureBox1.TabIndex = 3;
           pictureBox1.TabStop = false;
            // 
            // button1
            // 
           button1.Location = new System.Drawing.Point(311, 42);
           button1.Name = "button1";
           button1.Size = new System.Drawing.Size(75, 23);
           button1.TabIndex = 19;
           button1.Text = "button1";
           button1.UseVisualStyleBackColor = true;
            // 
            // txtDBA
            // 
           txtDBA.Location = new System.Drawing.Point(135, 150);
           txtDBA.Name = "txtDBA";
           //txtDBA.PasswordChar = '*';
           txtDBA.Size = new System.Drawing.Size(200, 23);
           txtDBA.TabIndex = 7;
            // 
            // label5
            // 
           label5.AutoSize = true;
           label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
           label5.Location = new System.Drawing.Point(19, 150);
           label5.Name = "label5";
           label5.Size = new System.Drawing.Size(31, 17);
           label5.TabIndex = 6;
           label5.Text = "DB1";
            // 
            // txtDB2
            // 
           txtDB2.Location = new System.Drawing.Point(135, 179);
           txtDB2.Name = "txtDB2";
           //txtDB2.PasswordChar = '*';
           txtDB2.Size = new System.Drawing.Size(200, 23);
           txtDB2.TabIndex = 9;
            // 
            // label6
            // 
           label6.AutoSize = true;
           label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
           label6.Location = new System.Drawing.Point(19, 179);
           label6.Name = "label6";
           label6.Size = new System.Drawing.Size(31, 17);
           label6.TabIndex = 8;
           label6.Text = "DB2";
            // 
            // frmServerSetting
            // 
            form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            form.BackColor = System.Drawing.Color.LightSteelBlue;
            form.ClientSize = new System.Drawing.Size(526, 338);
            form.Controls.Add(pictureBox1);
            form.Controls.Add(groupBox1);
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;           
            form.KeyPreview = true;
            form.Name = "frmServerSetting";
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            form.Text = "Database Settings";

            if (validation != null)
            {
                form.FormClosing += delegate(object sender, FormClosingEventArgs e)
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        string errorText = validation(txtServerLocal.Text);
                        if (e.Cancel = (errorText != ""))
                        {
                            MessageBox.Show(form, errorText, "Validation Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtServerLocal.Focus();
                            return;
                        }
                        errorText = validation(txtServerA.Text);
                        if (e.Cancel = (errorText != ""))
                        {
                            MessageBox.Show(form, errorText, "Validation Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtServerA.Focus();
                            return;
                        }
                           errorText = validation(txtServerB.Text);
                        if (e.Cancel = (errorText != ""))
                        {
                            MessageBox.Show(form, errorText, "Validation Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtServerB.Focus();
                            return;
                        }
                        errorText = validation(txtDBLocal.Text);
                        if (e.Cancel = (errorText != ""))
                        {
                            MessageBox.Show(form, errorText, "Validation Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtDBLocal.Focus();
                            return;
                        }

                        SqlConnection con = new SqlConnection("Data Source=" + txtServerLocal.Text + ";Initial Catalog=" + txtDBLocal.Text + ";Persist Security Info=True;User ID= sa;Password="+txtDBLocalPw.Text+";Integrated Security=false;Connection Timeout=30");
                        try
                        {
                            con.Open();
                        }
                        catch (Exception ex)
                        {
                            errorText = ex.Message.ToString();
                            if (e.Cancel = (errorText != ""))
                            {
                                MessageBox.Show(form, ex.Message.ToString(), "Validation Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtServerLocal.Focus();
                            }
                        }


                    }
                };


            }
            DialogResult dialogResult = form.ShowDialog();


            DbLAServerName = txtServerLocal.Text;
            DbAServerName = txtServerA.Text;
            DbBServerName = txtServerB.Text;
            DbLADBName = txtDBLocal.Text;
            DbADBName = txtDBA.Text;
            DbBDBName = txtDB2.Text;

            return dialogResult;
        }
    }


    public delegate string InputBoxValidation(string errorMessage);
}
        