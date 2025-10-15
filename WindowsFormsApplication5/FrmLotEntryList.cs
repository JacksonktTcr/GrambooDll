using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gramboo.Controls;
using Gramboo.Database;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SAFA.Forms.PUR
{
    public partial class FrmLotEntryList : Gramboo.Controls.GrbForm
    {
        private static FrmLotEntryList instance;

        public static FrmLotEntryList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FrmLotEntryList();
                }
                else if (instance.IsDisposed)
                {
                    instance = new FrmLotEntryList();
                }
                return instance;
            }
        }
        public FrmLotEntryList()
        {
            InitializeComponent();
        }
        public override void RefreshData()
        {
            base.RefreshData();
            dgview.EntryFormName = frmLotEntry.Instance;
            dgview.ShowEdit = true;
            dgview.IsList = true;
            dgview.AutoGenerateColumns = true;
            dgview.ShowSerialNo = true;
            //dgview.DataFields = new List<string> { "  QualityId,VchNo,VchDate,ProcessName, Gwt,Stwt, Diawt, NetWt,EmpName, [Created By], [Created Date], [Last Modified By], [Last Modified Date]" };
            dgview.HiddenDataFields = new List<string>() { "LotId" };
            dgview.Fill(new Table(DBConn.ConnectionProperties.DatabseName, "PUR", "VLot", true), "Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text + "AND LotDate >='" + Dtp_FromDate.Value.ToString("dd-MMM-yyyy") + "'AND LotDate <='" + Dtp_toDate.Value.ToString("dd-MMM-yyyy") + "'");
        }
    }
}
