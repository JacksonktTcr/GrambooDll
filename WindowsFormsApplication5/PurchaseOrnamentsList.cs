using Gramboo.Database;
using SAFA.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAFA.Forms.STK
{
    public partial class PurchaseOrnamentsList : Gramboo.Controls.GrbForm
    {
        private static PurchaseOrnamentsList instance;

        public static PurchaseOrnamentsList Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new PurchaseOrnamentsList();
                }
                else if (instance.IsDisposed)
                {
                    instance = new PurchaseOrnamentsList();
                }
                return instance;
            }
        }
        public PurchaseOrnamentsList()
        {
            InitializeComponent();
        }

        private void grbDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public override void RefreshData()
        {
            base.RefreshData();
         
                grb_list.EntryFormName =  FrmPurchaseOrnamentsEntrySafa.Instance;
            
            grb_list.DataFields = new List<string> {"EntryId","VchNo","VchDate","VoucherTypeId","BranchId","PartyTypeId", 
                      "Created_by","[Party Type]","Created_date","Last_modified_by","Last_modified_date","Company_id", 
                      "Branch_id","Counter_id","IsActive", "BranchName","PartyId", "[Party Name]","[Metal Type]","[Item Name]","SelectedBranch","[Created By]","GWt","StWt","Diawt","Netwt","Nos"};

            grb_list.HiddenDataFields = new List<string> {"EntryId","VoucherTypeId","BranchId","PartyTypeId","Created_By",
                 "Created_date","Last_modified_by","Last_modified_date","Company_id","Branch_id","Counter_id","IsActive", 
               "PartyId","[Created By]","[Last Modified By]"};

            if (!SAFA.Classes.Common.ChkUserType(DBConn, Convert.ToInt64(txtCrUserId.Text)))
            {
                grb_list.HiddenDataFields.AddRange(new string[] { "Created By"});
            }


             //grb_list.SummaryColumns = new string[] { "Nos", "Gwt", "StWt", "NetWt", "StAmt", "Diawt", "DiaRate", "DiaAmt" };
            grb_list.Fill(new Table(SAFA.Classes.Common.DbName, "STK", "VPurchaseList", true), "Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text + " AND VchDate>='" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "'AND VchDate<='" + Dtp_toDate.Value.ToString("dd-MMM-yyyy") + "'");


            
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
