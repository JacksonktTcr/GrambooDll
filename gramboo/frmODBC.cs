using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gramboo
{
    public partial class frmODBC : Form
    {
        public frmODBC()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ODBCManager.DSNExists(txtDsn.Text))
            {
                if (MessageBox.Show("DSN Name already exists do you want to modify settings ?", "ODBC",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, 
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    ODBCManager.RemoveDSN(txtDsn.Text);
                }
                else
                {
                    return;
                }
            }

            ODBCManager.CreateDSN(txtDsn.Text, txtDescp.Text, txtServer.Text, cmbDrivers.Text, chkTrusted.Checked, txtDatabase.Text,txtUser.Text,txtPass.Text);



        }

        private void frmODBC_Load(object sender, EventArgs e)
        { 
            foreach (string dr in  ODBCManager.GetInstalledDrivers())
            {
                cmbDrivers.Items.Add(dr);
            }
        }


         
    }
}
