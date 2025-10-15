using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAFA.Forms.ACC
{
    public partial class CashTransactionEntryList : Gramboo.Controls.GrbForm
    {
        private static CashTransactionEntryList instance;
        public static CashTransactionEntryList Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new CashTransactionEntryList();
                }
                else if (instance.IsDisposed)
                {
                    instance = new CashTransactionEntryList();
                }
                return instance;
            }
        }

        public CashTransactionEntryList()
        {
            InitializeComponent();
        }
        public override void RefreshData()
        {
            base.RefreshData();
            populategrid();
        }
        private void populategrid()
        {
            dgv.IsList = true;
            dgv.ShowEdit = true;
            //dgv.EntryFormName = ACC.frmCashTransactionEntryNew.Instance;
            dgv.EntryFormName = ACC.CashTransactionEntry.Instance;
            dgv.AutoGenerateColumns = true;
            dgv.DataSource = null;
            dgv.HiddenDataFields = new List<string> {"TransId", "IsActive" };

            //dgv.DataSource = this.DBConn.GetData(new SqlCommand("Select " +
            // "TransId,[Transaction Type],VchNo,Date,BranchName,[Ledger Type],[Ledger Head],Amount,[Bank Transaction Type]" + "," +
            // "[Cheque No],[Cheque Date],[Bank Name],[Branch Name],Remarks,IsActive,[Created By],[Created Date],[Last Modified By],[Last Modified Date]" +
            //"From ACC.VCashTransactionEntrylist WHERE Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text + "AND Date >='" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "'AND Date<='" + Dtp_toDate.Value.ToString("dd-MMM-yyyy") + "'Order by  TransId ASC")).Tables[0];


            if (txtBranchID.Text != "")
            {
                dgv.DataSource = this.DBConn.GetData(new SqlCommand("Select " +
                 "TransId,[Transaction Type],VchNo,Date,BranchName,Amount,[Bank Transaction Type]" + "," +
                 "[Cheque No],[Cheque Date],[Bank Name],[Branch Name],Remarks,IsActive,[Ledger Head],[Created By],[Created Date],[Last Modified By],[Last Modified Date]" +
                "From ACC.VCashTransactionEntrylist WHERE Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text + "AND Date >='" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "'AND Date<='" + Dtp_toDate.Value.ToString("dd-MMM-yyyy") + "'Order by  TransId ASC")).Tables[0];

            }
        }

        private void Dtp_FromDate_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
