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
//using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Common;
using System.IO;

namespace Gramboo
{

    public class frmRemoteFolderDialouge
    { 
        public static DialogResult Show(ref string ServerName , ref string UserName, ref string UserPassword,  bool ShowFiles )
        {

            return Show(ref ServerName, ref UserName, ref UserPassword,    ShowFiles, null);
        }

        public static string Path;


        public static DialogResult Show(ref string ServerName, ref string UserName, ref string UserPassword, bool ShowFiles,
                                   BackupLocationValidation validation)
        {
            
            Form form = new Form();
            TreeView treeView1 = new TreeView();
            Button btnOk = new Button();
            Button btnCancel = new Button();
              Server   server = new Server( new ServerConnection(ServerName,UserName, UserPassword) );


            btnOk.Text = "OK";
            btnCancel.Text = "Cancel";
            btnOk.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;

            
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(470, 570);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(64, 23);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;

            // 
            // btnOk
            // 
            btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            btnOk.Location = new System.Drawing.Point(400, 570);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(64, 23);
            btnOk.TabIndex = 4;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;

            // treeView1
            // 

             treeView1.Location = new System.Drawing.Point(1, 1);
            treeView1.Name = "treeView1";
            treeView1.Size = new System.Drawing.Size(526, 550);
           treeView1.TabIndex = 0;
           treeView1.ShowPlusMinus = true;
           treeView1.ImageList = new ImageList();
            treeView1.ImageList.Images.Add(Kallans .Properties.Resources.Folder);

             treeView1.AfterSelect+= delegate(object sender, TreeViewEventArgs e)
    {
        DataSet ds = server.ConnectionContext.ExecuteWithResults(string.Format("exec xp_dirtree '{0}', 1, 1", e.Node.FullPath));
        ds.Tables[0].DefaultView.Sort = "file ASC"; // list directories first, then files
        DataTable d = ds.Tables[0].DefaultView.ToTable();

        foreach (DataRow r in d.Rows)
        {
            int isFile;
            isFile = Convert.ToInt16(r["file"]);

            if (isFile == 1 && ShowFiles == false)
            {
            }
            else
            {

                e.Node.Nodes.Add(new TreeNode(r["subdirectory"].ToString()));
                FileAttributes attr = File.GetAttributes(treeView1.Nodes[treeView1.Nodes.Count - 1].FullPath);

                if (((attr & FileAttributes.Directory) == FileAttributes.Directory))
                {
                    treeView1.Nodes[treeView1.Nodes.Count - 1].ImageIndex = 0;
                }
                else
                {

                }

            }
        }
    };
            
            form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            form.BackColor = System.Drawing.Color.LightSteelBlue;
            form.ClientSize = new System.Drawing.Size(526, 600);
             form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            form.KeyPreview = true;
            form.Name = "frmDatabaseSettings";
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            form.Text = "Select File/Folder";
            form.Controls.Add(btnCancel);
            form.Controls.Add(btnOk);
            form.Controls.Add(treeView1);

            form.Load+=delegate (object Sender,EventArgs e)
            {
                DataTable d = server.EnumAvailableMedia();
        foreach (DataRow r in d.Rows)
            treeView1.Nodes.Add( new TreeNode(r["Name"].ToString() ));
            };

            if (validation != null)
            {
                form.FormClosing += delegate(object sender, FormClosingEventArgs e)
                {
                    if (form.DialogResult == DialogResult.OK)
                    {
                        string errorText = validation("Select folder to backup");
                        if (e.Cancel = (treeView1.SelectedNode==null))
                        {
                            MessageBox.Show(form, errorText, "Validation Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                            treeView1.Focus();
                        }

                        if (ShowFiles == true)
                        {
                            errorText = validation("Select File Name To Restore");
                            FileAttributes attr = File.GetAttributes(treeView1.SelectedNode.FullPath);

                            if (e.Cancel = ((attr & FileAttributes.Directory) != FileAttributes.Directory))
                            {
                                MessageBox.Show(form, errorText, "Validation Error",
                                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                                treeView1.Focus();
                            }
                        }

                    } 
                };


            }
            DialogResult dialogResult = form.ShowDialog();
            Path = treeView1.SelectedNode.FullPath;
            return dialogResult;
        }

     
    }
    public delegate string BackupLocationValidation(string errorMessage);
}
