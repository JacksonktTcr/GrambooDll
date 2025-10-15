using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gramboo.Database;
using System.Data.SqlClient;
using JMS.Classes;

namespace FA.FORMS
{
    public partial class IssueOrReciept : Gramboo.Controls.GrbForm
    {
        public SqlCommand cmd = new SqlCommand();
        private static IssueOrReciept instance;

        public static IssueOrReciept Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IssueOrReciept();
                }
                else if (instance.IsDisposed)
                {
                    instance = new IssueOrReciept();
                }
                return instance;
            }
        }
        public IssueOrReciept()
        {
            InitializeComponent();
        }
        public override void RefreshData()
        {
            txtcompId.Text = "1";
            txtBranchID.Text = "101";
             Gramboo.General.Setupcombo(CMBLDGName, "ACC.VTransLedgers", "[Ledger Name]", "Acc_LedgerId", "IsActive='True'");
        
            if (!IsEditMode)
                txt_Vchno.Text =  Common.GetNextVoucherNo(2, DTPLDGDate.Value,
                DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            
            cashbalance(); 
            base.RefreshData();
            populateList();
            populategrid();         
            AdjustColumnWidth();
                      
        }
        private void populategrid()
        {
           
            DGV.EntryFormName = this;
            DGV.ShowEdit = false;
            DGV.IsList = true;
            DGV.AutoGenerateColumns = true;
            DGV.HiddenDataFields = new List<string>() { "FREQ", "Company_id", "Branch_id" };
           
            DGV.Fill(new Table(DBConn.ConnectionProperties.DatabseName , "SYST", "VNarration", true));
            DGV.Sort(DGV.Columns["FREQ"], ListSortDirection.Descending);
        }
        public override void Init()
        {
            txtcompId.Text = "1";
            txtBranchID.Text = "101";
            base.Init();

            grbRdBtnPaymode.DefaultRadioButton = Rb_receipt;
        }
        private void populateList()
        {
            
            dgvList.EntryFormName = this;
            dgvList.ShowEdit = true;
            dgvList.IsList = true;
            dgvList.AutoGenerateColumns = true;
            dgvList.DataFields = new List<string>() { "VoucherEntryId", "VoucherTypeId",  "[Voucher No]", "[Voucher Date]", "[Ledger Name]", "[Voucher Amount]", "BillNo", "Narration", "[Created By]", "Created_by", "[Created Date]", "[Last Modified By]", "Last_modified_by", "[Last Modified Date]", "Counter_id", "IsActive", "Company_id", "Branch_id" };
            dgvList.HiddenDataFields = new List<string>() { "VoucherEntryId", "VoucherTypeId", "Counter_id", "IsActive",   "Company_id", "Branch_id" };
            
            dgvList.Fill(new Table(DBConn.ConnectionProperties.DatabseName , "ACC", "VPaymentsAndReceipts", true));
            
        }
        private void AdjustColumnWidth()
        {
            DGV.Columns[0].Width = 100;
            if (DGV.Columns.Contains("Narration List"))
                DGV.Columns["Narration List"].Width = 350;
            if (dgvList.Columns.Contains("Voucher Date"))
                dgvList.Columns["Voucher Date"].Width = 140;
            if (dgvList.Columns.Contains("Voucher Amount"))
                dgvList.Columns["Voucher Amount"].Width = 140;
            if (dgvList.Columns.Contains("Ledger Name"))
                dgvList.Columns["Ledger Name"].Width = 140;
            if (dgvList.Columns.Contains("Created Date"))
                dgvList.Columns["Created Date"].Width = 140;
            if (dgvList.Columns.Contains("Last Modified By"))
                dgvList.Columns["Last Modified By"].Width = 140;
            if (dgvList.Columns.Contains("Last_modified_by"))
                dgvList.Columns["Last_modified_by"].Width = 140;
            if (dgvList.Columns.Contains("Last Modified Date"))
                dgvList.Columns["Last Modified Date"].Width = 140;

     

        }
        public void cashbalance()
        {
            float Credit = 0, Debit = 0;
            using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
               ("Select Debit,Credit from  ACC.GroupOpnForADay("  +18+  ",'" + DTPLDGDate.Value.ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
            {
                Credit = Convert.ToSingle(dt.Rows[0]["Credit"] == null ? "0.00" : dt.Rows[0]["Credit"]);
                Debit = Convert.ToSingle(dt.Rows[0]["Debit"] == null ? "0.00" : dt.Rows[0]["Debit"]);
            }
            if (Credit != 0)
            {
                lblCashBalance.Text ="Cash Balance       "+ Credit.ToString("0.00");
            }
            else
            {
                lblCashBalance.Text ="Cash Balance       "+ Debit.ToString("0.00");
            }
            
        }
        public override bool InitializeTables()
        {
            
              //return base.InitializeTables();
              Table t = new Table( "fa_lite", "ACC", "VoucherEntry", false,  new Table( "fa_lite", "ACC","VPaymentsAndReceipts"));
            t.PrimaryKeys.Add("VoucherEntryId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = txt_Vchno;
            Table t1 = new Table("fa_lite", "ACC", "VoucherEntryDetails", false);
            t1.PrimaryKeys.Add("VchDetId");
            //t1.FillView = new Table(Classes."fa_lite", "ACC", "VoucherEntryDetails", false );
            //t1.DatagridView = dgvList;
            //t1.IsDatagridView = true;
            t1.IdTextBox = txt_Vch_detid;
            t.ChildTables.Add(t1);

            this.TableName = t;
            return true;

        }

        public void CreditDebit()
        {
            float Credit = 0, Debit = 0;
            
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                  ("Select Debit,Credit from  ACC.LedgerOpnForADay("+ CMBLDGName.SelectedValue + ",'"+ DTPLDGDate.Value.ToString("dd-MMM-yyyy")+ "','FALSE'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                {
                    Credit = Convert.ToSingle(dt.Rows[0]["Credit"] == null ? "0.00" : dt.Rows[0]["Credit"]);
                    Debit = Convert.ToSingle(dt.Rows[0]["Debit"] == null ? "0.00" : dt.Rows[0]["Debit"]);
                }
                if (Credit != 0)
                {
                    lbl_ldgerBalnce.Text = "Ledger Balance   "+ Credit.ToString("0.00") + "  (Cr)";
                }
                else 
                {
                    lbl_ldgerBalnce.Text = "Ledger Balance   " + Debit.ToString("0.00") + "  (Dr)";

                }
                
        }

        private void CMBLDGName_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CMBLDGName.SelectedValue != null && txtcompId.Text !="" && txtBranchID.Text !="")
            {
                CreditDebit();
                      
                    

            }
        }

        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void DGV_SelectionChanged(object sender, EventArgs e)
        {
            if (DGV.SelectedRows.Count >0)
            {
                DataGridViewRow row = DGV.SelectedRows[0];
                txtremarks.Text = row.Cells["Narration List"].Value.ToString();
            }
        }

        public override void Print()
        {

        }
        private void Cmb_Metal1_Enter(object sender, EventArgs e)
        {

        }

        private void Rb_cash_CheckedChanged(object sender, EventArgs e)
        {
                


            if (!IsEditMode)
                txt_Vchno.Text =Common.GetNextVoucherNo(2, DTPLDGDate.Value,
                DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            //cmb_VoucherTypeId.SelectedValue = 2;

        }

        private void Rb_credit_CheckedChanged(object sender, EventArgs e)
        {
            
            if (!IsEditMode)
                txt_Vchno.Text =Common.GetNextVoucherNo(3, DTPLDGDate.Value,
                DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            Print();
        }
    }
}
