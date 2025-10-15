using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gramboo.Controls
{
    public partial class frmAuth : Form
    {
        bool validate;
        public string Action { get; set; }
        public Gramboo.Database.Table Table { get; set; }

        public frmAuth()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            validate = false;
            DataController dc = new DataController();
            dc.ConnectionProperties.GenerateSQLConnectionString();

            if (dc.GetData(new System.Data.SqlClient.SqlCommand("SELECT * FROM SYST.Username WHERE upper(user_name)= Upper('" +
                GeneralConfig.UserName+ "') and user_password='" + txtConfirmPassword.Text + "' AND IsActive='True'")).Tables[0].Rows.Count == 0)
            {
                txtConfirmPassword.ShowMessage ("Invalid User Name or Password");
                txtConfirmPassword.Focus(); 
                return;
            }
            if(txtRemarks.Text.Length<10)
            {
                txtRemarks.ShowMessage("Enter Remarks .. Minimum 10 Characters ");
                txtRemarks.Focus();
                return;

            }
            validate = true;
            btnConfirm.DialogResult = DialogResult.OK;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            validate = true;
            btnCancel.DialogResult = DialogResult.Cancel;
        }

        private void frmAuth_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !validate;

        }
    }
}
