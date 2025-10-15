using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JMS.Forms.PROD
{
    public partial class RepairEntryList : Gramboo.Controls.GrbForm
    {

          private static RepairEntryList instance;

        public static RepairEntryList Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new RepairEntryList();
                }
                else if (instance.IsDisposed)
                {
                    instance = new RepairEntryList();
                }
                return instance;
            }
        }
     
        public RepairEntryList()
        {
            InitializeComponent();
        }
        public override void RefreshData()
        {
            PopulateGrid();
        }

        private void PopulateGrid()
        {
            dgv.EntryFormName = RepairEntry .Instance;
            dgv.ShowEdit = true;
            dgv.AutoGenerateColumns = true;
            dgv.ShowSerialNo = true;
            dgv.SummaryColumns = new string[] { "Wastage", "WastagePer", "[Return Wt]", "IssueWt" };
            dgv.HiddenDataFields = new List<string> { "RepairId", "WorkerId", "WorkshopID", "CustId", };
            dgv.DataSource = this.DBConn.GetData(new SqlCommand("Select " +
           "RepairId,VchNo,WorkshopId,[Workshop Type],WorkshopName,WorkerId,WorkerName,CustId,[Customer Name],IssueDate,[Issue Item],[Job Return Date],RetWt,[Wastage%],Wastage,McPergm,MC,[Created By],[Created Date],[Last Modified By],[Last Modified Date] " +
            "From PROD.VRepairList WHERE Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID .Text)).Tables[0];

            //"CustId", "WorkerId", "JobNoId", "Company_id", "WorkshopID" };

        }

    }
}
