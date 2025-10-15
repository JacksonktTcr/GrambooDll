using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Gramboo.Controls;

namespace REGAL.Classes
{
    public partial class FindByVoucherNo : UserControl
    {
        public FindByVoucherNo()
        {
            InitializeComponent();
        }

        public string  TableName { get; set; }
        public string PrimaryKey { get; set; }
        public string VoucherNoColumn { get; set; }
        public int VoucherTypeId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public GrbForm CurrentForm { get; set; }
        public Gramboo.DataController DBCon { get; set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (TableName == null)
            {
                Gramboo.General.ShowMessage("Set Table Name");
                return ;
            }
            else if (CompanyId == null)
            {
                Gramboo.General.ShowMessage("Set Company Id");
                return;
            }
            else if (BranchId == null)
            {
                Gramboo.General.ShowMessage("Set Branch Id");
                return;
            }
                  else if (PrimaryKey == null)
            {
                Gramboo.General.ShowMessage("Set Primary Key Column");
                return;
            }
            else
            {
                long entryId;
                using (DataTable dt = DBCon.GetData(new SqlCommand("Select top 1 " + PrimaryKey + " from "+  TableName + " WHERE "+ VoucherNoColumn +"='"+ txtVchNo.Text  +"' AND company_id=" +
                    CompanyId + " AND Branch_Id= " + BranchId +  (VoucherTypeId == 0 ? "" : " AND VoucherTypeId=" + VoucherTypeId) + " order by " + PrimaryKey + " DESC")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        Dictionary<string, object> d = new Dictionary<string, object>();
                        entryId = (long)dt.Rows[0][0];
                        d.Add(PrimaryKey, entryId);
                        d.Add("Company_id", CompanyId);
                        d.Add("Branch_id", BranchId);
                        CurrentForm.FillData(d);
                        this.Hide();
                    }
                    else
                    {
                        Gramboo.General.ShowMessage("No Data Found");
                    }
                }
            }
        }


    }
}
