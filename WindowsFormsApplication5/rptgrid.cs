using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace pdemo
{
    public partial class rptgrid : Gramboo.Controls.GrbGridReport
    {
        private static rptgrid instance;

         public static rptgrid Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new rptgrid();
                }
                else if (instance.IsDisposed)
                {
                    instance = new rptgrid();
                }
                return instance;
            }
        }
        public rptgrid()
        {
            InitializeComponent();
            //HiddenColumns = new List<string>() { "item_id" };
            SummaryColumns = new List<string> { "[quantity]", "[total]", "[discount]", "[tot_amt]" };
            FillTable = new Gramboo.Database.Table("SampleDemo", "SALE", "VSalesReport", true);
            Columns = new List<string> { "[billno]", "[billdate]", "[cust_name]", 
                "[item_name]", "[rate]", "[quantity]", "[total]", "[discount]", "[tot_amt]" };
        }
        public override void Init()
        {
            base.Init();
           
        }
        public override void RefreshData()
        {
            base.RefreshData();
            criteria = "1=1";
           // criteria = " Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text;
            //dgv.DefaultCellStyle.ForeColor = Color.RoyalBlue;
            //dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
            //dgv.RowsDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);
          
        }
    }
}
