using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Gramboo.Database;

namespace JMS.Forms.PUR
{
    public partial class SupplierOpeningBalanceEntry : Gramboo.Controls.GrbForm
    {
        private static SupplierOpeningBalanceEntry instance;

        public static SupplierOpeningBalanceEntry Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new SupplierOpeningBalanceEntry();
                }
                else if (instance.IsDisposed)
                {
                    instance = new SupplierOpeningBalanceEntry();
                }
                return instance;
            }
        }

        public SupplierOpeningBalanceEntry()
        {
            InitializeComponent();
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

        public override void RefreshData()
        {
            base.RefreshData();
            Gramboo.General.Setupcombo(cmb_supplier, "PUR.VSupplierMaster", "[Supplier Name]", "SuppId", "IsActive='True'AND Company_id=" + txtcompId.Text);

           // PopulateGrid();
        
        }
        public override bool InitializeTables()
        {

            Table t = new Table("JMS", "PUR", "SupplierOpeningBalance");
            t.PrimaryKeys.Add("EntryId");
            t.IdTextBox = TxtEntryNo;
            Table t1 = new Table("JMS", "PUR", "SupplierOpeningBalanceDetails", true);
            t1.PrimaryKeys.Add("TransId");
            t1.FillView = new Table("JMS", "PUR", "VSupplierOpeningBalanceDetails", true);
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

            dgv.AutoGenerateColumns = true;
            dgv.DataFields = new List<string>() { "TransId", "BillDate", "BillNo", "DueDate", "Wt", "Cash", "SettlementStatus", "Branch_id", "Company_id" };
            dgv.HiddenDataFields = new List<string>() { "TransId", "Branch_id", "Company_id", "SettlementStatus" };
            dgv.SummaryColumns = new string[] { "Wt", "Cash" };
            dgv.Fill(new Table("JMS", "PUR", "VSupplierOpeningBalanceDetails", true), "1=2");

            txtwtbal.Text = "0";
            txtcashbal.Text = "0";

            //this.ListForm = SupplierOpeningList.Instance;
            
        }
        //private void populategrid()
        //{
        //    dgv.EntryFormName = this;
        //    dgv.IsList = true;
        //    dgv.AutoGenerateColumns = true;
        //    dgv.ShowEdit = true;
        //    dgv.HiddenDataFields = new List<string> { "EntryId", "SuppId", "Branch_id", "Company_id" };
        //    dgv.DataFields = new List<string> { "EntryId", "SuppId", "[Supplier Name]", "[Openig Date]", "WtBalance", "CashBalance", "[Created By]", "[Created Date]", " [Last Modified By]", "[Last Modified Date]", "Branch_id", "Company_id" };
        //    dgv.Fill(new Table("JMS", "PUR", "VSupplierOpeningBalance", true));


        //}

        private void btn_add_Click(object sender, EventArgs e)
        {
            txtstaus.Checked = true;
            dgv.Save();


        }

        private void dgv_BeforeEditRow(object sender, Gramboo.Controls.RowChangingEventArgs e)
        {
            
        }

        private void dgv_BeforeEdit(object source, EventArgs e)
        {
            //e.
        }

        //private void PopulateGrid()
        //{
        //    dgvList.EntryFormName = this;
        //    dgvList.ShowEdit = true;
        //    dgvList.AutoGenerateColumns = true;
        //    dgvList.DataFields = new List<string>() { "EntryId", "[Supplier Name]", 
        //              "[Opening Date]", "WtBalance","CashBalance", 
        //              "MCBalance", "WstBalance","DiaBalance","StoneBalance",  "[Created By]", "[Created Date]", "[Last Modified By]", "[Last Modified Date]" , "Company_id", "Branch_id", "Counter_id" };
        //    dgvList.HiddenDataFields = new List<string>() { "EntryId", "Counter_id", "Branch_id", "Company_id" };
        //    dgvList.SummaryColumns = new string[]{ "WtBalance","CashBalance", 
        //              "MCBalance", "WstBalance","DiaBalance","StoneBalance"};
        //    dgvList.Fill(new Table("JMS", "PUR", "VSupplierOpeningBalance", true), " Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
        //}
    }
}
