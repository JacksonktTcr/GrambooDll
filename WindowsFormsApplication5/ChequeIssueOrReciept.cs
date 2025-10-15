using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Gramboo.Database;

namespace FA.FORMS
{
    public partial class ChequeIssueOrReciept : Gramboo.Controls.GrbForm
    {
        public SqlCommand cmd = new SqlCommand();
        private static ChequeIssueOrReciept instance;

        public static ChequeIssueOrReciept Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChequeIssueOrReciept();
                }
                else if (instance.IsDisposed)
                {
                    instance = new ChequeIssueOrReciept();
                }
                return instance;
            }
        }
        public ChequeIssueOrReciept()
        {
            InitializeComponent();
        }
        public override void RefreshData()
        {
            base.RefreshData();
            txtcompId.Text = "1";
            txtBranchID.Text = "101";
          
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            Gramboo.General.Setupcombo(CMBLDGName, "ACC.VTransLedgers", "[Ledger Name]", "Acc_LedgerId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmb_BankName, "ACC.VBankLedgers", "[Ledger Name]", "Acc_LedgerId");

            cmb_VoucherTypeId.SelectedValue = 1;
            if (!IsEditMode)
                txt_VchNo.Text = JMS.Classes.Common.GetNextVoucherNo( 1, DTPVch_date.Value,
                DBConn, 1 , 101);
            //Gramboo.General.Setupcombo(cmbTesCntre, "STK.VTestingCenters", "[Supplier Name]", "SuppId", "[Is Active]='True'");
            
            populategrid();
            
           
            populateList();
          //  AdjustColumnWidth();
           

        }
           private void populategrid()
        {
            
            //DGV_Narration.EntryFormName = this;
            DGV_Narration.ShowEdit = false;
            //DGV_Narration.IsList = true;
            DGV_Narration.AutoGenerateColumns = true;
            DGV_Narration.HiddenDataFields = new List<string>() { "FREQ", "Company_id", "Branch_id" };
            
            DGV_Narration.Fill(new Table(DBConn.ConnectionProperties.DatabseName , "ACC", "VNarration", true));
            DGV_Narration.Sort(DGV_Narration.Columns["FREQ"], ListSortDirection.Descending);
        }
           private void populateList()
           {
               //txtcompId.Text = "1";
               //txtBranchID.Text = "101";
              
               dgvList.ShowEdit = true;
               dgvList.EntryFormName = this;
                dgvList.IsList = true;
               dgvList.AutoGenerateColumns = true;
               dgvList.DataFields = new List<string>() { "VoucherEntryId", "VoucherTypeId", "[Voucher No]", "[Voucher Date]", "[Ledger Name]", "[Bank Name]", "ChequeDate", "ChequeNo", "[Voucher Amount]", "BillNo", "Narration", "[Created By]", "Created_by", "[Created Date]", "[Last Modified By]", "Last_modified_by", "[Last Modified Date]", "Counter_id", "IsActive", "LedgerId", "Company_id", "Branch_id" };
               dgvList.HiddenDataFields = new List<string>() { "VoucherEntryId", "VoucherTypeId", "Counter_id", "IsActive", "LedgerId", "Company_id", "Branch_id" };

               dgvList.Fill(new Table(DBConn.ConnectionProperties.DatabseName, "ACC", "VChequePaymentsAndReceipts", true), "Company_id=" + txtcompId.Text);
               
           }
           public void CreditDebit()
           {
               float Credit = 0, Debit = 0;
               using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
              ("Select Debit,Credit from  ACC.LedgerOpnForADay('" + CMBLDGName.SelectedValue + "','" + DTPVch_date.Value.ToString("dd-MMM-yyyy") + "','FALSE'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
               {
                   Credit = Convert.ToSingle(dt.Rows[0]["Credit"] == null ? "0.00" : dt.Rows[0]["Credit"]);
                   Debit = Convert.ToSingle(dt.Rows[0]["Debit"] == null ? "0.00" : dt.Rows[0]["Debit"]);
               }
               if (Credit != 0)
               {
                   lbl_ldgerBalnce.Text = "Ledger Balance   " + Credit.ToString("0.00") + "  (Cr)";
               }
               else
               {
                   lbl_ldgerBalnce.Text = "Ledger Balance   " + Debit.ToString("0.00") + "  (Dr)";

               }

           }
           private void AdjustColumnWidth()
           {
              
               if (DGV_Narration.Columns.Contains("Narration List"))
                   DGV_Narration.Columns["Narration List"].Width = 320;

               if (dgvList.Columns.Contains("Voucher Amount"))
                   dgvList.Columns["Voucher Amount"].Width = 140;

               if (dgvList.Columns.Contains("Created Date"))
                   dgvList.Columns["Created Date"].Width = 140;

               if (dgvList.Columns.Contains("Last Modified Date"))
                   dgvList.Columns["Last Modified Date"].Width = 140;

               if (dgvList.Columns.Contains("Last Modified By"))
                   dgvList.Columns["Last Modified By"].Width = 140;

               if (dgvList.Columns.Contains("Last_modified_by"))
                   dgvList.Columns["Last_modified_by"].Width = 140;

               if (dgvList.Columns.Contains("Ledger Name"))
                   dgvList.Columns["Ledger Name"].Width = 140;

               if (dgvList.Columns.Contains("Voucher Date"))
                   dgvList.Columns["Voucher Date"].Width = 140;
           }
           public override bool InitializeTables()
           {
               Table t = new Table("FA_LITE", "ACC", "VoucherEntry", false, new Table("FA_LITE", "ACC", "VChequePaymentsAndReceipts"));
               t.PrimaryKeys.Add("VoucherEntryId");
               t.NotUpdatables.Add("Company_id");
               t.NotUpdatables.Add("Branch_id");
               t.NotUpdatables.Add("Counter_id");
               t.IdTextBox = txt_VchNo;
               Table t1 = new Table("FA_LITE", "ACC", "VoucherEntryDetails", true);
               t1.PrimaryKeys.Add("VchDetId");
               t1.FillView = new Table("FA_LITE", "ACC", "VoucherEntryDetails", true);
               //t1.DatagridView = dgvList;
               //t1.IsDatagridView = true;
               t1.IdTextBox = TxtTranscId ;
               t.ChildTables.Add(t1);
               this.TableName = t;
               return true;

           }

       public override bool GenerateID(Table table_name)
        {
            try
            {
                table_name.IdTextBox.Text = JMS.Classes.Common.GetNextID(table_name, table_name.IdTextBox.DataField, DBConn, Convert.ToInt32(base.txtcompId.Text), Convert.ToInt32(base.txtBranchID.Text)).ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

           private void Rb_cash_CheckedChanged(object sender, EventArgs e)
           {
                 
        }

        private void Rb_credit_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void cmb_LdgName_SelectedValueChanged(object sender, EventArgs e)
        {
            CreditDebit();
        }

        private void DGV_Narration_SelectionChanged(object sender, EventArgs e)
        {
            if (DGV_Narration.SelectedRows.Count > 0)
            {
                DataGridViewRow row = DGV_Narration.SelectedRows[0];
                txtnarration.Text = row.Cells["Narration List"].Value.ToString();
            }
        }

        private void cmb_BankName_SelectedValueChanged(object sender, EventArgs e)
        {
            cashbalance();
        }

        private void cashbalance()
        {
            if (cmb_BankName.SelectedValue != null && txtcompId.Text != "" && txtBranchID.Text != "")
            {
                float Credit = 0, Debit = 0;
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
               ("Select Debit,Credit from  ACC.LedgerOpnForADay(" + cmb_BankName.SelectedValue + ",'" + DTPVch_date.Value.ToString("dd-MMM-yyyy") + "','FALSE'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                {
                    Credit = Convert.ToSingle(dt.Rows[0]["Credit"] == null ? "0.00" : dt.Rows[0]["Credit"]);
                    Debit = Convert.ToSingle(dt.Rows[0]["Debit"] == null ? "0.00" : dt.Rows[0]["Debit"]);
                }
                if (Credit != 0)
                {
                    lblBankBlnce.Text = "Cash Balance     " + Credit.ToString("0.00") + "  (Cr)";
                }
                else
                {
                    lblBankBlnce.Text = "Cash Balance     " + Debit.ToString("0.00") + "  (Dr)";

                }


            }
        }

      
        
        private void TxtEntryNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void grbButton2_Click(object sender, EventArgs e)
        {
            if (CMBLDGName.SelectedValue != null && cmb_BankName .SelectedValue !=null)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "ACC.InsertChequeTrans";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@VchEntryId", TxtEntryNo.Text);
                cmd.Parameters.AddWithValue("@IsRecipt", (Rb_Rpt.Checked ? true : false));
                cmd.Parameters.AddWithValue("@Vchno", txt_VchNo.Text);
                cmd.Parameters.AddWithValue("@VchDate", DTPVch_date.Value);
                cmd.Parameters.AddWithValue("@LedgerId", CMBLDGName.SelectedValue);
                cmd.Parameters.AddWithValue("@BankId", cmb_BankName.SelectedValue);
                cmd.Parameters.AddWithValue("@GoldRate", txtGoldRate.Text);
                cmd.Parameters.AddWithValue("@ChequeNo", txt_ChequeNo.Text);
                cmd.Parameters.AddWithValue("@ChequeDate", dtp_Cheque.Value);
                cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                cmd.Parameters.AddWithValue("@Narration", txtnarration.Text);
                cmd.Parameters.AddWithValue("@RefBillNo", txt_billno.Text);
                cmd.Parameters.AddWithValue("@CompanyId", txtcompId.Text);
                cmd.Parameters.AddWithValue("@BranchId", txtBranchID.Text);
                cmd.Parameters.AddWithValue("@CounterId", txtCounterId.Text);
                cmd.Parameters.AddWithValue("@userId", txtCrUserId.Text);
                Gramboo.DataController.CommandCollection cmdcol = new Gramboo.DataController.CommandCollection();
                cmdcol.Add(cmd);
                DBConn.ExecuteSqlTransaction(cmdcol, "InsertChequeTrans");
                populateList();

            }
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            print();
        }
        private void print()
        {
          
        }
       
    }
}
