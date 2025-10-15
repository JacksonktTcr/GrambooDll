using Gramboo;
//using JMS.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Kallans.Forms.GENERALFORMS
{
    public partial class Login : Form
    {
        private Gramboo.GRBConfig config;
        private DataController dc = new DataController();
        public int Dept;
        public String Deptname;
        public int userId;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            DoubleBuffered = true;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Gramboo.General.Setupcombo(dc, cmbCompany, "SYST.CompanyMaster", "Comp_Name", "Comp_id", "IsActive=1 and Comp_id=1");
            Gramboo.General.Setupcombo(dc, CmbDepartment, "STK.DepartmentMaster", "DeptName", "DeptId", "IsActive=1 ");

        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        
        {
            bool flag = true;
         
           if (dc.GetData(new System.Data.SqlClient.SqlCommand("SELECT * FROM SYST.Username WHERE upper(user_name)= Upper('" +
                txtUserName.Text + "') and user_password='" + txtPasswd.Text + "' AND IsActive='True'")).Tables[0].Rows.Count == 0)
            {
                txtUserName.ShowMessage("Invalid User Name or Password");
                txtUserName.Focus();
                flag = false;
            }
           if (cmbBranch.SelectedIndex == -1)
           {
               
               cmbBranch.ShowMessage("Select Branch");
               cmbBranch.Focus();
               flag = false;
           }
           if (cmbCompany.SelectedIndex == -1)
           {
               cmbCompany.ShowMessage("Select Company");
               cmbCompany.Focus();
               flag = false;
           }
           if (CmbDepartment.SelectedIndex == -1)
           {
               CmbDepartment.ShowMessage("Select Department");
               CmbDepartment.Focus();
               flag = false;
           }

           if (cmbCounter.SelectedIndex == -1)
           {
               cmbCounter.ShowMessage("Select Counter");
               cmbCounter.Focus();
               flag = false;
           }
           if (flag)
           {
               WriteToConfig();
               Kallans.Classes.Common.DbName = dc.ConnectionProperties.DatabseName;
               //FA.Common.DbName = dc.ConnectionProperties.DatabseName;
               //frmMain frm = new frmMain();
              // frm.lbluser.Text = txtUserName.Text;
              // frm.Show();
              // frm.lblBranch.Text = cmbBranch.Text;
              // frm.lblBranch.Tag = cmbBranch.SelectedValue;
              // frm.lblCompanyName.Text = cmbCompany.Text;
              // frm.lblCompanyName.Tag = cmbCompany.SelectedValue;
              // frm.lblYear.Text = GetFiscalYear();
              
              // frm.lblCounter.Text = cmbCounter.Text;
              //// frm.dept = Convert.ToInt64(CmbDepartment.SelectedValue);
              // frm.dept = Convert.ToInt64(CmbDepartment.SelectedValue);  
            
              // this.Hide();
           }

        }
        private string GetFiscalYear()
        {
            string syear, Eyear;

            using (SqlConnection cn = new SqlConnection(dc.ConnectionProperties.ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ACC.GETFiscalYearStartDate(" + cmbCompany.SelectedValue + ",'" + DateTime.Now.Date.ToShortDateString() + "')", cn);
                syear = Convert.ToDateTime(cmd.ExecuteScalar()).Year.ToString();
                cmd = new SqlCommand("SELECT ACC.GETFiscalYearEndDate(" + cmbCompany.SelectedValue + ",'" + DateTime.Now.Date.ToShortDateString() + "')", cn);

                Eyear = Convert.ToDateTime(cmd.ExecuteScalar()).Year.ToString();
                cn.Close();

                return syear + "-" + Eyear.Substring(2, 2);
            }
        }
        private void WriteToConfig()
        {
            if (Gramboo.GRBConfig.Open() != null)
            {
                config = Gramboo.GRBConfig.Open();
            }
            else
            {
                config = new Gramboo.GRBConfig();
            }

            config.Login.UserName = txtUserName.Text;
            config.Login.UserId = Convert.ToInt32(dc.GetData(new System.Data.SqlClient.SqlCommand("SELECT * FROM SYST.Username WHERE upper(user_name)= Upper('" +
                txtUserName.Text + "') and user_password='" + txtPasswd.Text + "'")).Tables[0].Rows[0]["User_Id"]);

            config.Login.CompanyID = Convert.ToInt32(cmbCompany.SelectedValue);
            config.Login.BranchId = Convert.ToInt32(cmbBranch.SelectedValue);
            config.Login.CounterId = Convert.ToInt32(cmbCounter.SelectedValue);
            config.Login.EnableAuditTrail = true;

            config.Save();
        }

        private void cmbCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbCompany.ValueMember != "" && cmbCompany.SelectedValue != null)
                Gramboo.General.Setupcombo(dc, cmbBranch, "SYST.BranchMaster", "BranchName", "Branchid", "IsActive=1 AND Company_id=" + cmbCompany.SelectedValue + "");
        }

        private void cmbCounter_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbBranch_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbBranch.ValueMember != "" && cmbBranch.SelectedValue != null)
                Gramboo.General.Setupcombo(dc, cmbCounter, "SYST.CounterMaster", "CounterName", "Counterid", "IsActive=1 AND Branch_id=" + cmbBranch.SelectedValue + "");
        }

        private void Login_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                            Color.WhiteSmoke,
                                                            Color.SteelBlue,
                                                            90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void Login_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void grbButton2_Click(object sender, EventArgs e)
        {
          
        }

        private void grbButton1_Click_1(object sender, EventArgs e)
        {
            //SYST.BranchMaster frm  = SYST.BranchMaster .Instance;
            //frm.MdiParent = this.ParentForm;
            //frm.Show();
            //((frmMain)this.ParentForm).ShowChild(frm);
            //frm.Focus();
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPasswd.Focus();  
        }

        private void txtPasswd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbCompany.Focus();  
        }

        private void cmbCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                CmbDepartment.Focus();
        }

        private void CmbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbBranch.Focus();
        }

        private void cmbBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cmbCounter.Focus();
        }

        private void cmbCounter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin.Focus();
        }

    }
}
