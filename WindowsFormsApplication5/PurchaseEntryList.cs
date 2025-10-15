using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Gramboo.Database;

namespace SAFA.Forms.PUR
{
    public partial class PurchaseEntryList : Gramboo.Controls.GrbForm
    {

        private static PurchaseEntryList instance;

        public static PurchaseEntryList Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new PurchaseEntryList();
                }
                else if (instance.IsDisposed)
                {
                    instance = new PurchaseEntryList();
                }
                return instance;
            }
        }

        public PurchaseEntryList()
        {
            InitializeComponent();
        }

        public override void RefreshData()
        {
            base.RefreshData();
            
            PopulateGrid();
            ColumnWidth();
        }

        public override void Init()
        {
            base.Init();
            ColumnWidth();         

        }

        private void PopulateGrid()
        {

            dgv.EntryFormName = PurchaseEntry.Instance;
            dgv.ShowEdit = true;
            dgv.ReadOnly = true;
            dgv.AutoGenerateColumns = true;
            dgv.ShowSerialNo = true;
            dgv.SummaryColumns = new string[] { "NetTotal","Gwt","DiaWt","StWt","NetWt" , "NetTotal" , "Grand Total", "Tax Amount" , "Other Charges", "Round Off", "TDSAmount", "TCSAmount", "Total Amount" };
            //dgv.DataFields=new List<string>(){"PurId","[Purchase No]","VoucherTypeId","[Purchase Date]","[Payment Mode]","[Supplier Name]","[Net Total]","[Created By]","[Created Date]","[Last Modified By]","[Last Modified Date]"};
            dgv.HiddenDataFields = new List<string> { "PurId", "Company_id", "Branch_id", "SupplierId", "VoucherTypeId", "[Payment Terms]", "[Invoice No]", "[Invoice Date]", "[Purchase Type Name]", "[Due Date]", "[Bill Type]", "[Delivery Date]", "[Agent Name]", "[Gold Rate]", "[Platinum Rate]", "[Silver Rate]", "BillType", "PurTypeId", "Created_by", "Last_modified_by", "PayMode", "Counter_id" };
            if (!SAFA.Classes.Common.ChkUserType(DBConn, Convert.ToInt64(txtCrUserId.Text)))
            {
                dgv.HiddenDataFields.AddRange(new string[] { "Created By", "Created Date", "Last Modified By", "Last Modified Date" });
            }


            dgv.DataSource = this.DBConn.GetData(new SqlCommand("Select " +
           "PurId,[Purchase No],VoucherTypeId,[Purchase Date],[Invoice No],[Supplier Name],[Total Gwt] as Gwt,[Total DiaWt] as DiaWt,[Total Stone Wt] as StWt,[Total NetWt] as NetWt,[Grand Total],[Tax Perc],[Tax Amount],[Other Charges],[Total Amount],[Round Off],AmountTDS as TDSAmount,TCSAmount,NetTotal,[Payment Mode],[Created By],[Created Date],[Last Modified By],[Last Modified Date],Company_id,Branch_id " +
            "From PUR.VPurchaseMasterList where Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text + "AND [Purchase Date] >='" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "'AND [Purchase Date]<='" + Dtp_toDate.Value.ToString("dd-MMM-yyyy") + "'")).Tables[0];

            // dgv.DataSource = this.DBConn.GetData(new SqlCommand("Select " +
           //"PurId,[Purchase No],VoucherTypeId,[Purchase Date],[Payment Mode],[Supplier Name],[Net Total],[Created By],[Created Date],[Last Modified By],[Last Modified Date],Company_id,Branch_id " +
           // "From PUR.VPurchaseMasterList where Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text + "AND [Invoice Date] >='" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "'AND [Invoice Date]<='" + Dtp_toDate.Value.ToString("dd-MMM-yyyy") + "'")).Tables[0];
        //   // dgv.Fill(new Table(DBConn.ConnectionProperties.DatabseName, "PUR", "[VPurchaseMasterList]"), "Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
        }
        ///*PUR.PurchaseMaster.PurId, PUR.PurchaseMaster.VchNo AS [Purchase No], PUR.PurchaseMaster.VoucherTypeId, cast (PUR.PurchaseMaster.PurDate as datetime) AS [Purchase Date], 
        //              PUR.PurchaseMaster.InvNo AS [Invoice No], cast(PUR.PurchaseMaster.InvDate as Datetime) AS [Invoice Date], PUR.PurchaseMaster.SupplierId, 
        //              PUR.VSupplierMaster. [Supplier Name],  PUR.VSupplierMaster. [Supplier Group], PUR.VSupplierMaster. [Supplier Type] ,   PUR.PurchaseMaster.PayMode, (CASE PUR.PurchaseMaster.PayMode WHEN 1 THEN 'Cash' ELSE 'Weight' END)
        //               AS [Payment Mode], PUR.PurchaseMaster.BillType, (CASE PUR.PurchaseMaster.BillType WHEN 1 THEN 'Cash' ELSE 'Credit' END) AS [Bill Type], 
        //              PUR.PurchaseMaster.PaymentTerms AS [Payment Terms], PUR.PurchaseMaster.DueDate AS [Due Date], PUR.PurchaseMaster.PurTypeId, 
        //              PUR.PurchaseTaxTypeMaster.PurTypeName AS [Purchase Type Name], PUR.PurchaseMaster.GrandTotal AS [Grand Total], 
        //              PUR.PurchaseMaster.TaxPerc AS [Tax Perc], PUR.PurchaseMaster.TaxAmount AS [Tax Amount], PUR.PurchaseMaster.OtherCharges AS [Other Charges], 
        //              PUR.PurchaseMaster.TotalAmount AS [Total Amount], PUR.PurchaseMaster.Roundoff AS [Round Off], PUR.PurchaseMaster.NetTotal AS [Net Total], 
        //              PUR.PurchaseMaster.TotalMetalCash AS [Total Metal Cash], PUR.PurchaseMaster.TotalGwt AS [Total Gwt], PUR.PurchaseMaster.TotalDiaCash AS [Total DiaCash], 
        //              PUR.PurchaseMaster.TotalDiawt AS [Total DiaWt], PUR.PurchaseMaster.TotalStoneCash AS [Total Stone Cash], 
        //              PUR.PurchaseMaster.TotalStoneWt AS [Total Stone Wt], PUR.PurchaseMaster.TotalWst AS [Total Wst], PUR.PurchaseMaster.TotalMc AS [Total Mc], 
        //              PUR.PurchaseMaster.TotalNetWt AS [Total NetWt], PUR.PurchaseMaster.Created_by, SYST.Username.user_name AS [Created By], 
        //              PUR.PurchaseMaster.Created_date AS [Created Date], PUR.PurchaseMaster.Last_modified_by, Username_1.user_name AS [Last Modified By], 
        //              PUR.PurchaseMaster.Company_id, PUR.PurchaseMaster.Branch_id, PUR.PurchaseMaster.Counter_id, PUR.PurchaseMaster.IsActive, 
        //              PUR.PurchaseMaster.Last_modified_date AS [Last Modified Date],  PUR.VSupplierMaster.[Supplier Group] as  [Group Name],  PUR.VSupplierMaster.[Supplier type] as [Type Name],
        //             SuppGstNo*/
        private void ColumnWidth()
        {

            dgv.Columns[0].Width = 40;
            dgv.Columns["Purchase No"].Width = 100;
            dgv.Columns["Supplier Name"].Width = 350;
            dgv.Columns["Gwt"].Width = 70;
            dgv.Columns["Stwt"].Width = 60;
            dgv.Columns["Diawt"].Width = 60;
            dgv.Columns["NetWt"].Width = 70;
            dgv.Columns["Tax Perc"].Width = 50;
            dgv.Columns["Created By"].Width = 100;
            dgv.Columns["Created Date"].Width = 125;
            dgv.Columns["Last modified by"].Width = 125;
            dgv.Columns["Last modified date"].Width = 140;
            dgv.HeaderHtml = setheaderhtml();
        }

        private void refreshButton1_Load(object sender, EventArgs e)
        {

        }


        private string setheaderhtml()
        {
            string html = "<h3 style=\"text-align:center; \"><strong>Company Name</strong></h1> " +
   " <h4 style = \"text-align:center;\" ><strong> Address 1 </strong></h3>" +
   " <h4 style = \"text-align:center;\" ><strong> Address 2 </strong></h3>" +  
    " <h4 style = \"text-align:center;\" ><strong> Report Title & nbsp;</strong></h4> " +
     "  <p><strong> Filrers </strong></p> ";
            return html;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
