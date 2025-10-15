using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JMS.Forms.SALE
{
    public partial class frmSalesList : Gramboo.Controls.GrbForm
    {
        private static frmSalesList instance;

        public static frmSalesList Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new frmSalesList();
                }
                else if (instance.IsDisposed)
                {
                    instance = new frmSalesList();
                }
                return instance;
            }
        }
        public frmSalesList()
        {
            InitializeComponent();
        }
        public override void RefreshData()
        {
            PopulateGrid();
        }
        private void PopulateGrid()
        {

            dgv.EntryFormName = Kallans.Forms.SALE.frmSalesEntry.Instance;
            dgv.ShowEdit = true;
            dgv.AutoGenerateColumns = true;
            dgv.ShowSerialNo = true;
            dgv.SummaryColumns = new string[] {  };
            dgv.HiddenDataFields = new List<string>() { "SalesId" };

            dgv.DataSource = this.DBConn.GetData(new SqlCommand("Select  " +
             "* " +
            "From SALE.VSalesMaster WHERE Company_id=1 AND Branch_id=104")).Tables[0];

        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            dgv.Edit(dgv);
        }

        private void grbButton2_Click(object sender, EventArgs e)
        {

           // Console.Clear();
            Gramboo.Controls.FullscreenViewer fsw = new Gramboo.Controls.FullscreenViewer(dgv.Parent);
            fsw.View();
        }
    }
}
