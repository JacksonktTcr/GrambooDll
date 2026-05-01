
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
using System.Data.SqlClient;

namespace Gramboo
{

    public class frmServerSetting
    {


        public static DialogResult Show(ref string DbAServerName, ref string DbADBName, ref string DbLAServerName, ref string DbLADBName,   ref string DbBServerName, ref string DbBDBName, ref string DbAPassword, ref string DbLPassword, ref string DbBPassword)
        {
            SrvInputBoxValidation validation = delegate (string val)
            {
                if (val == "")
                    return "Value cannot be empty.";
                return "";
            };
            return Show(ref DbAServerName, ref DbADBName,  ref DbLAServerName, ref DbLADBName,  ref DbBServerName, ref DbBDBName,ref DbAPassword, ref DbLPassword, ref DbBPassword, validation);
        }




        public static DialogResult Show(ref string DbAServerName, ref string DbADBName, ref string DbLAServerName, ref string DbLADBName, ref string DbBServerName, ref string DbBDBName, ref string DbAPassword, ref string DbLPassword, ref string DbBPassword,
                                     SrvInputBoxValidation validation)
        {

            Form form = new Form();
            //Label label1 = new System.Windows.Forms.Label();
            //TextBox txtServerLocal = new System.Windows.Forms.TextBox();
            //GroupBox groupBox1 = new System.Windows.Forms.GroupBox();
            //Button btnCancel = new System.Windows.Forms.Button();
            //Button btnOk = new System.Windows.Forms.Button();
            //TextBox txtDBLocal = new System.Windows.Forms.TextBox();
            //Label label4 = new System.Windows.Forms.Label();
            //TextBox txtServerB = new System.Windows.Forms.TextBox();
            //Label label3 = new System.Windows.Forms.Label();
            //TextBox txtServerA = new System.Windows.Forms.TextBox();
            //Label label2 = new System.Windows.Forms.Label();
            //PictureBox pictureBox1 = new System.Windows.Forms.PictureBox();
            //Button button1 = new System.Windows.Forms.Button();
            //TextBox txtDBA = new System.Windows.Forms.TextBox();
            //Label label5 = new System.Windows.Forms.Label();
            //TextBox txtDB2 = new System.Windows.Forms.TextBox();
            //Label label6 = new System.Windows.Forms.Label();


               Label label1;
          TextBox txtServerLocal;
          GroupBox groupBox1;
          TextBox txtDB2;
          Label label6;
          TextBox txtDBA;
           Label label5;
          Button btnCancel;
          Button btnOk;
          TextBox txtDBLocal;
          Label label4;
          TextBox txtServerB;
          Label label3;
          TextBox txtServerA;
          Label label2;
          PictureBox pictureBox1;
          Button button1;
        TextBox txtBPassword;
          Label label7;
          TextBox txtAPassword;
         Label label8;
         TextBox txtLPassword;
          Label label9;


          label1 = new System.Windows.Forms.Label();
           txtServerLocal = new System.Windows.Forms.TextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            txtDB2 = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            txtDBA = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            btnCancel = new System.Windows.Forms.Button();
            btnOk = new System.Windows.Forms.Button();
            txtDBLocal = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            txtServerB = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            txtServerA = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            button1 = new System.Windows.Forms.Button();
            txtBPassword = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            txtAPassword = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            txtLPassword = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();

             

            
            txtServerLocal.Text = DbLAServerName;
            txtServerA.Text = DbAServerName;
            txtServerB.Text = DbBServerName;
            txtDBLocal.Text = DbLADBName;
            txtDBA.Text = DbADBName;
            txtDB2.Text = DbBDBName;
            txtLPassword.Text = DbLPassword;
            txtAPassword.Text = DbAPassword;
            txtBPassword.Text = DbBPassword;


            txtLPassword.PasswordChar = '*';
            txtAPassword.PasswordChar = '*';
            txtBPassword.PasswordChar = '*';

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
            groupBox1.Controls.Add(txtBPassword);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(txtAPassword);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(txtLPassword);
            groupBox1.Controls.Add(label9);
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
            groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            groupBox1.ForeColor = System.Drawing.Color.Blue;
            groupBox1.Location = new System.Drawing.Point(162, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(357, 339);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Database Properties";
            // 
            // txtDB2
            // 
            txtDB2.Location = new System.Drawing.Point(135, 179);
            txtDB2.Name = "txtDB2";
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
            // txtDBA
            // 
            txtDBA.Location = new System.Drawing.Point(135, 150);
            txtDBA.Name = "txtDBA";
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
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(212, 305);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(64, 23);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOk.Location = new System.Drawing.Point(142, 305);
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
            // textBox1
            // 
            txtBPassword.Location = new System.Drawing.Point(135, 266);
            txtBPassword.Name = "textBox1";
            txtBPassword.Size = new System.Drawing.Size(200, 23);
            txtBPassword.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label7.Location = new System.Drawing.Point(19, 266);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(86, 17);
            label7.TabIndex = 14;
            label7.Text = "DB2 Passwod";
            // 
            // textBox2
            // 
            txtAPassword.Location = new System.Drawing.Point(135, 237);
            txtAPassword.Name = "textBox2";
            txtAPassword.Size = new System.Drawing.Size(200, 23);
            txtAPassword.TabIndex = 13;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label8.Location = new System.Drawing.Point(19, 237);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(91, 17);
            label8.TabIndex = 12;
            label8.Text = "DB1 Password";
            // 
            // textBox3
            // 
            txtLPassword.Location = new System.Drawing.Point(135, 208);
            txtLPassword.Name = "textBox3";
            txtLPassword.Size = new System.Drawing.Size(200, 23);
            txtLPassword.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label9.Location = new System.Drawing.Point(19, 208);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(98, 17);
            label9.TabIndex = 10;
            label9.Text = "Local Password";
            // 
            // Form5
            // 
 
    

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
            
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
            form.ResumeLayout(false);

            if (validation != null)
            {
                form.FormClosing += delegate (object sender, FormClosingEventArgs e)
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

                         SqlConnection con = new SqlConnection("Data Source=" + txtServerLocal.Text + ";Initial Catalog=" + txtDBLocal.Text + ";Persist Security Info=True;User ID= sa;Password="+txtLPassword.Text+";Integrated Security=false;Connection Timeout=30");
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
            DbAPassword = txtAPassword.Text;
            DbLPassword = txtLPassword.Text;
            DbBPassword = txtBPassword.Text;

            return dialogResult;
        }
    }


    public delegate string SrvInputBoxValidation(string errorMessage);
}
