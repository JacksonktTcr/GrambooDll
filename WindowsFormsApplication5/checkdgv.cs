using Gramboo.Controls;
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

namespace WindowsFormsApplication5
{
    public partial class checkdgv : GrbForm
    {
        public checkdgv()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            dgviewPending.AutoGenerateColumns = true;
            dgviewPending.ShowSerialNo = true;
            dgviewPending.SummaryColumns = new string[] { };
            //dgviewPending.HiddenDataFields = new List<string>() { "EntryId", "TransId", "ItemID", "PartyName" };
             
            //    dgviewPending.ReadOnly = true; 
            //    dgviewPending.HiddenDataFields = new List<string>() { "EntryId", "TransId", "ItemID", "Select", "PartyName" };
            
            dgviewPending.DataSource = this.DBConn.GetData(new SqlCommand("Select cast('false' as bit) as [Select],EntryId,TransId,VchNo,CustName,VchDate,PartyName,ItemID,ItemName,Nos,Gwt,Diawt,Stwt,Netwt,ActWt from STK.StockTransferIssueAndReceiptPending(15,1,3, 110100000287,'OS','STK',1,101)")).Tables[0];

        }
    }
}
