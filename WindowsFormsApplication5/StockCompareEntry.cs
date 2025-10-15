using Gramboo.Database;
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

namespace Kallans.Forms.STOCK
{
    public partial class StockCompareEntry : Gramboo.Controls.GrbForm
    {
        private static StockCompareEntry instance;

        bool flag = false;

        public static StockCompareEntry Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new StockCompareEntry();
                }
                else if (instance.IsDisposed)
                {
                    instance = new StockCompareEntry();
                }
                return instance;
            }
        }
        public StockCompareEntry()
        {
            InitializeComponent();
            txtTotNos.TextChanged += new EventHandler(DiffNos);
            txtTotPhyNo.TextChanged += new EventHandler(DiffNos);
            txtTotPhyWt.TextChanged += new EventHandler(DiffWt);
            txtphydiawt.TextChanged += new EventHandler(DiffDiaWt);
            txtdiawt.TextChanged += new EventHandler(DiffDiaWt);
            TxtTotWt.TextChanged += new EventHandler(DiffWt);
            txtCalcCash.TextChanged += new EventHandler(DiffCash);
            txtActCAsh.TextChanged += new EventHandler(DiffCash);
        }
        public override bool GenerateID(Table table_name)
        {
            try
            {
                table_name.IdTextBox.Text = Kallans.Classes.Common.GetNextID(table_name, table_name.IdTextBox.DataField, DBConn, Convert.ToInt32(base.txtcompId.Text), Convert.ToInt32(base.txtBranchID.Text)).ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool FillData(Dictionary<string, object> PrimaryValues)
        {
            if (base.FillData(PrimaryValues))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool InitializeTables()
        {

            Table t = new Table(Kallans.Classes.Common.DbName, "STK", "CompareMaster");
            t.PrimaryKeys.Add("CompId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = txtCompareId;
            Table t1 = new Table(Kallans.Classes.Common.DbName, "STK", "CompareDetails", true);
            t1.PrimaryKeys.Add("TransId");
            t1.FillView = new Table(Kallans.Classes.Common.DbName, "STK", "VCompareDetails", true);
            t1.DatagridView = dgv;
            t1.IsDatagridView = true;
            t1.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t1);



            this.TableName = t;
            return true;
        }
        public override void Init()
        {
            base.Init();
            TxtIsactive.Text = "1";

            dgv.IsDataEntryGrid = true;
            dgv.ShowDelete = false;
            //dgv.DataFields = new List<string> { "TransId","CompId","DeptId", "ItemId", "ItemName", "Qty", "Wt" };
            //dgv.HiddenDataFields = new List<string> {"TransId","CompId","DeptId","ItemId"};
            //dgv.SummaryColumns = new string[] { "Qty", "Wt"};
            //dgv.Fill(new Table(Kallans.Classes.Common.DbName, "STK", "VCompareDetails", true), "1=2");
            //dgv.Columns["col_AutoSlno"].DataPropertyName = "SlNo";

            PopulateGrid();
            Dtp_CompDate_ValueChanged(new object(), new EventArgs());



            //this.ListForm = StockCompareEntryList.Instance;
            if (this.TableName != null)
                GenerateID(this.TableName);

        }
        public override void RefreshData()
        {
            base.RefreshData();
           
        }
        public void PopulateGrid()
        {



            dgv.ShowEdit = false;
            dgv.ShowDelete = false;
            dgv.AutoGenerateColumns = true;//, ,
            dgv.HiddenDataFields = new List<string>() { "TransId", "CompId", "DeptId", "ItemId", "GroupId" };//"CAST(0 AS BIGINT) AS TransId","CAST(0 AS BIGINT) AS CompId",
            dgv.DataFields = new List<string>() { "CompId", "TransId", "ItemId", "ItemName", "Qty", "Wt", "DiaWt", "[Physical No]", "[Physical Wt]", "[Physical DiaWt]", "GroupId" };
            dgv.SummaryColumns = new string[] { "Qty", "Wt", "Physical No", "Physical Wt", "DiaWt", "Physical DiaWt" };
           // dgv.CellEndEdit += dgv_CellEndEdit;

            dgv.SummaryRowVisible = true;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[STK].[CompareClosingStock]";
            //if (Cmb_Department.SelectedValue != null && cmb_FloorName.SelectedValue !=null)
            //{

            cmd.Parameters.AddWithValue("@StockDate", (Dtp_CompDate.Value.Date).ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@DeptId", 0);
            cmd.Parameters.AddWithValue("@CompanyId", txtcompId.Text);
            cmd.Parameters.AddWithValue("@BranchId", txtBranchID.Text);
            cmd.Parameters.AddWithValue("@FloorId", 0);
          //  dgv.DataSource = null;
            dgv.DataSource = DBConn.GetData(cmd, "CompareClosingStock").Tables[0];


                if (txtcompId.Text != "")
                {

                    using (DataTable dt = DBConn.GetData(new SqlCommand("Select ACC.CashTransactionOpening('" + Dtp_CompDate.Value.Date.AddDays(1).ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + ")"), "cash").Tables[0])
                    {
                        txtCalcCash.Text = dt.Rows[0][0].ToString();
                        txtActCAsh.Text = dt.Rows[0][0].ToString();
                    }
                }
            
            AdjustColumnWidths();
            dgv.Columns["ItemName"].ReadOnly = true;
            dgv.Columns["Qty"].ReadOnly = true;
            dgv.Columns["Wt"].ReadOnly = true;
            dgv.Columns["DiaWt"].ReadOnly = true;
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Cmb_Department_SelectedValueChanged(object sender, EventArgs e)
        {

            PopulateGrid();
            AdjustColumnWidths();
            dgv.Columns["ItemName"].ReadOnly = true;
            dgv.Columns["Qty"].ReadOnly = true;
            dgv.Columns["Wt"].ReadOnly = true;
            dgv.Columns["DiaWt"].ReadOnly = true;
        }

        private void dgv_SummaryCalculated(object source, EventArgs e)
        {
            txtTotPhyNo.Text = dgv.SummaryRow.SummaryCells["Physical No"].Text;
            txtTotPhyWt.Text = dgv.SummaryRow.SummaryCells["Physical Wt"].Text;
            txtphydiawt.Text = dgv.SummaryRow.SummaryCells["Physical DiaWt"].Text;
            txtTotNos.Text = dgv.SummaryRow.SummaryCells["Qty"].Text;
            TxtTotWt.Text = dgv.SummaryRow.SummaryCells["Wt"].Text;
            txtdiawt.Text = dgv.SummaryRow.SummaryCells["DiaWt"].Text;
        }
        public void AdjustColumnWidths()
        {
            dgv.RowHeadersVisible = false;

            // dgv.Columns[0].Width = 40;

            if (dgv.Columns.Contains("ItemName"))
                dgv.Columns["ItemName"].Width = 160;
            if (dgv.Columns.Contains("Qty"))
                dgv.Columns["Qty"].Width = 100;
            if (dgv.Columns.Contains("Wt"))
                dgv.Columns["Wt"].Width = 100;
            if (dgv.Columns.Contains("DiaWt"))
                dgv.Columns["DiaWt"].Width = 100;
            if (dgv.Columns.Contains("Physical No"))
                dgv.Columns["Physical No"].Width = 120;
            if (dgv.Columns.Contains("Physical Wt"))
                dgv.Columns["Physical Wt"].Width = 120;
            if (dgv.Columns.Contains("Physical DiaWt"))
                dgv.Columns["Physical DiaWt"].Width = 120;

            int groupid = 0;
            Color c = new Color();
            c = Color.Black;
            foreach (DataGridViewRow r in dgv.Rows)
            {
              
                if (groupid != Convert.ToInt32(r.Cells["GroupId"].Value.ToString()))
                {
                    groupid = Convert.ToInt32(r.Cells["GroupId"].Value.ToString());

                    if (groupid % 2 == 0)
                    {
                        c = Color.Red ;
                    }
                    else
                    {
                        c = Color.Black;
                    }
                }
                dgv.Rows[r.Index].DefaultCellStyle.ForeColor = c; 
            }
        }
        public void DiffNos(object Sender, EventArgs e)
        {
            float Nos = 0, PhyNos = 0, DiffNos = 0;
            txtTotNos.Text = (string.IsNullOrEmpty(txtTotNos.Text.Trim()) ? "0.00" : txtTotNos.Text);
            txtTotPhyNo.Text = (string.IsNullOrEmpty(txtTotPhyNo.Text.Trim()) ? "0.00" : txtTotPhyNo.Text);

            Nos = Convert.ToSingle(txtTotNos.Text);
            PhyNos = Convert.ToSingle(txtTotPhyNo.Text);
            DiffNos = Nos - PhyNos;
            txtDifferNos.Text = DiffNos.ToString();
        }
        public void DiffWt(object Sender, EventArgs e)
        {
            float Wt = 0, PhyWt = 0, DiffWt = 0;
            txtTotPhyWt.Text = (string.IsNullOrEmpty(txtTotPhyWt.Text.Trim()) ? "0.00" : txtTotPhyWt.Text);
            TxtTotWt.Text = (string.IsNullOrEmpty(TxtTotWt.Text.Trim()) ? "0.00" : TxtTotWt.Text);

            PhyWt = Convert.ToSingle(txtTotPhyWt.Text);
            Wt = Convert.ToSingle(TxtTotWt.Text);
            DiffWt = Wt - PhyWt;
            txtDiffWt.Text = DiffWt.ToString("f2");
        }
        public void DiffDiaWt(object Sender, EventArgs e)
        {
            float DiaWt = 0, PhyDiaWt = 0, DiffDiaWt = 0;
            txtphydiawt.Text = (string.IsNullOrEmpty(txtphydiawt.Text.Trim()) ? "0.00" : txtphydiawt.Text);
            txtdiawt.Text = (string.IsNullOrEmpty(txtdiawt.Text.Trim()) ? "0.00" : txtdiawt.Text);

            PhyDiaWt = Convert.ToSingle(txtphydiawt.Text);
            DiaWt = Convert.ToSingle(txtdiawt.Text);
            DiffDiaWt = DiaWt - PhyDiaWt;
            txtdiffdiawt.Text = DiffDiaWt.ToString("f2");
        }


        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void DiffCash(object Sender, EventArgs e)
        {
            double Cash = 0, ActCash = 0, DiffCAsh = 0;

            ActCash = Convert.ToDouble(txtCalcCash.Text);
            Cash = Convert.ToDouble(txtActCAsh.Text);
            DiffCAsh = Cash - ActCash;
            txtDiffCash.Text = DiffCAsh.ToString();
        }
        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //if (dgv.Columns[e.ColumnIndex].DataPropertyName == "Physical No" ||
            //    dgv.Columns[e.ColumnIndex].DataPropertyName == "Physical Wt")

            //{
            //    float i;


            //    if (!float.TryParse(Convert.ToString(e.FormattedValue), out i))
            //    {
            //        e.Cancel = true;

            //}
        }

        private void Dtp_CompDate_ValueChanged(object sender, EventArgs e)
        {
            if (flag)
                return;
            if (txtcompId.Text != "")
            { 
              

                using (DataTable Dt = DBConn.GetData(new SqlCommand("select * from STK.CompareMaster where CompDate='" + Dtp_CompDate.Value.ToString("dd-MMM-yyyy") + "'"), "comp").Tables[0])
                {
                   
                    if (Dt.Rows.Count > 0)
                    {
                        txtCompareId.Text =Dt.Rows[0]["CompId"].ToString();
                        Dictionary<string, object> d = new Dictionary<string, object>();
                        d.Add("Company_id", txtcompId.Text);
                        d.Add("Branch_id", txtBranchID.Text);
                        d.Add("CompId", txtCompareId.Text);
                        flag = true;
                        FillData(d);
                        flag = false ;
                         
                    }

                }
       
                PopulateGrid();
            }
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            Save();
        }
        }      
}
