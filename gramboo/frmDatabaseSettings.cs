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

    public  class frmDatabaseSettings 
    {


        public static DialogResult Show(  ref string ServerName, ref string DatabaseName,  ref string UserName, ref string UserPassword)
        {
            InputBoxValidation validation = delegate(string val)
            {
                if (val == "")
                    return "Value cannot be empty."; 
                return "";
            };
            return Show(ref ServerName, ref  DatabaseName, ref UserName, ref UserPassword, validation);
        }

      


        public static DialogResult Show( ref string ServerName, ref string DatabaseName,  ref string UserName, ref string UserPassword,
                                   InputBoxValidation validation)
        {

            Form form = new Form();
   Label label1=new Label();
         TextBox txtServer=new TextBox();
        GroupBox groupBox1=new GroupBox();
        Button btnCancel=new Button();
        Button btnOk = new Button();
        TextBox txtPassword = new TextBox();
        Label label4 = new Label();
        Label label3 = new Label();
        TextBox txtUser = new TextBox();
        TextBox txtDatabase = new TextBox();
        Label label2 = new Label();
        PictureBox pictureBox1 = new PictureBox();
            
            txtServer.Text = ServerName;
            txtDatabase.Text = DatabaseName;

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
            label1.Size = new System.Drawing.Size(84, 17);
            label1.TabIndex = 0;
            label1.Text = "Server Name";
            // 
            // txtServer
            // 
            txtServer.Location = new System.Drawing.Point(135, 34);
            txtServer.Name = "txtServer";
            txtServer.Size = new System.Drawing.Size(200, 23);
            txtServer.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnCancel);
            groupBox1.Controls.Add(btnOk);
            groupBox1.Controls.Add(txtPassword);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(txtUser);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtDatabase);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(txtServer);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            groupBox1.ForeColor = System.Drawing.Color.Blue;
            groupBox1.Location = new System.Drawing.Point(162, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            groupBox1.Size = new System.Drawing.Size(357, 219);
            groupBox1.TabIndex = 0;
            groupBox1.Text = "Database Properties";
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(271, 167);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(64, 23);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
          
            // 
            // btnOk
            // 
            btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOk.Location = new System.Drawing.Point(201, 167);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(64, 23);
            btnOk.TabIndex = 4;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
           
            // 
            // txtPassword
            // 
            txtPassword.Location = new System.Drawing.Point(135, 121);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new System.Drawing.Size(200, 23);
            txtPassword.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label4.Location = new System.Drawing.Point(19, 121);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(64, 17);
            label4.TabIndex = 0;
            label4.Text = "Password";
            // 
            // txtUser
            // 
            txtUser.Location = new System.Drawing.Point(135, 92);
            txtUser.Name = "txtUser";
            txtUser.Size = new System.Drawing.Size(200, 23);
            txtUser.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label3.Location = new System.Drawing.Point(19, 92);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(74, 17);
            label3.TabIndex = 0;
            label3.Text = "User Name";
            // 
            // txtDatabase
            // 
            txtDatabase.Location = new System.Drawing.Point(135, 63);
            txtDatabase.Name = "txtDatabase";
            txtDatabase.Size = new System.Drawing.Size(200, 23);
            txtDatabase.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label2.Location = new System.Drawing.Point(19, 63);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(102, 17);
            label2.TabIndex = 0;
            label2.Text = "Database Name";
            // 
            // pictureBox1
            // 
            pictureBox1.Image =Gramboo.Properties.Resources.Setting;
            pictureBox1.Location = new System.Drawing.Point(12, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(144, 143);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // frmDatabaseSettings
            // 
            form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            form.BackColor = System.Drawing.Color.LightSteelBlue;
            form.ClientSize = new System.Drawing.Size(526, 238);
            form.Controls.Add(pictureBox1);
            form.Controls.Add(groupBox1);
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;           
            form.KeyPreview = true;
            form.Name = "frmDatabaseSettings";
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            form.Text = "Database Settings";

            if (validation != null)
            {
                form.FormClosing += delegate(object sender, FormClosingEventArgs e)
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        string errorText = validation(txtServer.Text);
                        if (e.Cancel = (errorText != ""))
                        {
                            MessageBox.Show(form, errorText, "Validation Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtServer.Focus();
                            return;
                        }
                        errorText = validation(txtDatabase.Text);
                        if (e.Cancel = (errorText != ""))
                        {
                            MessageBox.Show(form, errorText, "Validation Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtDatabase.Focus();
                            return;
                        }
                           errorText = validation(txtUser.Text);
                        if (e.Cancel = (errorText != ""))
                        {
                            MessageBox.Show(form, errorText, "Validation Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtUser.Focus();
                            return;
                        }
                        errorText = validation(txtPassword.Text);
                        if (e.Cancel = (errorText != ""))
                        {
                            MessageBox.Show(form, errorText, "Validation Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPassword.Focus();
                            return;
                        }

                        SqlConnection con = new SqlConnection("Data Source=" + txtServer.Text + ";Initial Catalog=" + txtDatabase.Text + ";Persist Security Info=True;User ID=" + txtUser.Text + ";Password=" + txtPassword.Text + ";Integrated Security=false;Connection Timeout=30");
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
                                txtServer.Focus();
                            }
                        }


                    }
                };


            }
            DialogResult dialogResult = form.ShowDialog();

           

            ServerName = txtServer.Text;
            DatabaseName = txtDatabase.Text;
            UserName=txtUser.Text;
            UserPassword=txtPassword.Text;
            return dialogResult;
        }
    }


    public delegate string InputBoxValidation(string errorMessage);
}
        

//        private void   btnOk_Click(object sender, EventArgs e)
//        {
//            if (txtServer.Text.Trim() == "")
//            {
//                General.ShowMessage( "Enter Server Name !",  "Message",  MessageBoxIcon.Information, MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button1);
//                txtServer.Focus();
//                return ;
//            }
//            if (txtDatabase.Text.Trim() == "")
//            {
//                General.ShowMessage ("Enter Database Name !", "Message", MessageBoxIcon.Information, MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button1);
//                txtDatabase.Focus();
//                return;
//            }
//            if (txtUser.Text.Trim() == "")
//            {
//                General.ShowMessage("Enter User Name !", "Message", MessageBoxIcon.Information, MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button1);
//                txtUser.Focus();
//                return;
//            }
//            if (txtPassword.Text.Trim() == "")
//            {
//                General.ShowMessage("Enter Password !", "Message", MessageBoxIcon.Information, MessageBoxButtons.OKCancel, MessageBoxDefaultButton.Button1);
//                txtPassword.Focus();
//                return;
//            }

//            using (SqlConnection cn = new SqlConnection())
//            {

//                try
//                {
//                    cn.ConnectionString = "Data Source=" + txtServer.Text.Trim() + ";Initial Catalog=" + txtDatabase.Text.Trim() + ";Persist Security Info=True;User ID=" + txtUser.Text.Trim() + ";Password=" + txtPassword.Text.Trim();
//                    cn.Open();


//                }
//                catch (Exception ex)
//                {
//                    General.ShowMessage("Please check connection parameters : \n" + ex.Message.ToString(), "Error", MessageBoxIcon.Error, MessageBoxButtons.OK);
//                    txtServer.Focus();
//                    return;
//                }
//            }

//            if (Conn.WriteDatabaseProperties(txtServer.Text.Trim(), txtDatabase.Text.Trim(), txtUser.Text.Trim(), txtPassword.Text.Trim()) == true)
//            {
//                Close();
//            }

//        }

//        private void DisplayDatabaseProperties()
//        {
//            Conn.ReadDatabaseProperties();
//            txtServer.Text = Conn.ServerName;
//            txtUser.Text = Conn.DBUsername;
//            txtDatabase.Text = Conn.DatabseName;
//            txtPassword.Text = Conn.DBPassword;


//        }

//        private void frmDatabaseSettings_Load(object sender, EventArgs e)
//        {
//            DisplayDatabaseProperties();
//        }

//        private void frmDatabaseSettings_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)13)
//            {

//                Control p;
//                p = ActiveControl.Parent;
//                p.SelectNextControl(ActiveControl, true, true, true, true);
//            }
//        }
//    }
//}
