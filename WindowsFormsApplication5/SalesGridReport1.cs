using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace JMS.Forms.SALE
{
    public partial class SalesGridReport1 :Gramboo.Controls.GrbGridReport
    {


        private static SalesGridReport1 instance;

        public static SalesGridReport1 Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new SalesGridReport1();
                }
                else if (instance.IsDisposed)
                {
                    instance = new SalesGridReport1();
                }
                return instance;
            }
        }
        public SalesGridReport1()
        {
            InitializeComponent();
            HiddenColumns = new List<string>() { "SalesId", "PriceListId", "EmpId", "JobOrderId", "Company_id", "Branch_id", "Counter_id", "DeptId" };
            SummaryColumns = new List<string> { "Gold Wt", "TotalNetWt", "TotalDiaWt", "TotalStoneWt", "Certification Charge", "TotalMC", "TotalWastage", "Product Value", "Tax Amount", "OtherCharge", "Discount Amount", "NetTotal" };
            Columns = new List<string> { "InvNo","InvDate","DeptId","[Department Name]","[Customer Name]","[Customer Group Name]"," [Cust Type]","[Pay Mode]","[Sales Person]","PricelistName","[Gold Wt]","TotalNetWt","TotalDiaWt","TotalStoneWt"
                ,"[Certification Charge]","TotalMC","TotalWastage","[Product Value]"
                ,"[Tax Amount]","OtherCharge","[Discount Amount]","NetTotal","SalesId","PriceListId"
                ,"EmpId","JobOrderId","Company_id","Branch_id","Counter_id"};

            FillTable = new Gramboo.Database.Table("JMS", "SALE", "VSalesMasterGridReport", true);
           
        }
        public override void RefreshData()
        {
            //criteria = "Company_id=" + txtcompId.Text + " AND   Branch_id=" + txtBranchID.Text + " AND DeptId IN (SELECT DeptId FROM SYST.VUserDepartmentAccess WHERE  AllowAccess='TRUE' AND user_id=" + txtCrUserId.Text + ")";
            base.RefreshData();
        }

        private void SalesGridReport1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToString() == Keys.F5 .ToString())
            {
                RefreshData();
            }
        }

        private void SalesGridReport1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5 )
            {
                RefreshData();
            }
        } 
           
        

       
    }
}
