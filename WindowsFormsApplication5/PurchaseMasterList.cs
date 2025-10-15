using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JMS.Forms.PUR
{
    public partial class PurchaseMasterList : Gramboo.Controls.GrbForm
    {

        private static PurchaseMasterList instance;

        public static PurchaseMasterList Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new PurchaseMasterList();
                }
                else if (instance.IsDisposed)
                {
                    instance = new PurchaseMasterList();
                }
                return instance;
            }
        }
        public PurchaseMasterList()
        {
            InitializeComponent();
        }
        public override void RefreshData()
        {
            PopulateGrid();
        }
        private void PopulateGrid()
        {

            dgv.EntryFormName = PurchaseEntry .Instance;
            dgv.ShowEdit = true;
            dgv.AutoGenerateColumns = true;
            dgv.ShowSerialNo = true;
            dgv.SummaryColumns = new string[] { "GrandTotal", "DiscountAmount", "TaxAmount", "Wastage", "OtherCharges", "TotalAmount", "Roundoff", "NetTotal" };
            dgv.HiddenDataFields = new List<string> { "PurId", "Company_id", "Branch_id" };
            dgv.DataSource = this.DBConn.GetData(new SqlCommand("Select " +
           "PurId,VchNo,[Purchase Date],[Pay Mode],[Invoice No],[Invoice Date], [Bill Type],[Supplier Name],PaymentTerms,DueDate,DeliveryDate,[Agent Name],[Purchase Type],GrandTotal,[Discount%],DiscountAmount,TaxAmount,[Tax%],[Wastage%],Wastage,OtherCharges,TotalAmount,Roundoff,NetTotal,[Created By],[Created Date],[Last Modified By],[Last Modified Date],Company_id,Branch_id " +
            "From PUR.VPurchaseMaster WHERE voucherTypeId=26 AND  Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text)).Tables[0];

        }
    }
}
