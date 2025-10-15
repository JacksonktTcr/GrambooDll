using Gramboo;
using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Gramboo.Controls.GrbForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        public override bool GenerateID(Table table_name)
        {
            try
            {
                table_name.IdTextBox.Text =GetNextID(table_name, table_name.IdTextBox.DataField, DBConn, 1,101).ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public override void RefreshData()
        {

            //Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            //cmb_VoucherTypeId.SelectedValue = 1;
            ////if (!IsEditMode)
            //TxtVoucherNo.Text = "1";
            //Gramboo.General.Setupcombo(Cmb_LedgerHead, "ACC.LedgerMaster", "Acc_LedgerName", "Acc_LedgerId", "IsActive='True'AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
            ////if (Cmb_LedgerHead.SelectedValue != null)
            ////{
            ////    txtLedgerId.Text = Cmb_LedgerHead.SelectedValue.ToString();


            ////}
            //if (this.TableName != null)
            //    GenerateID(this.TableName);
            cmbAssortId.MultiColumn = true;
            Int64 selval = 0;
            if (cmbAssortId.SelectedValue != null)
            {
                selval = (Int64)cmbAssortId.SelectedValue;
            }

            cmbAssortId.DisplayMember = "VoucherNumber";
            cmbAssortId.ValueMember = "AssortId";
            cmbAssortId.CustomWidths = new int[] { 0, 50, 75, 100, 100, 50, 50, 50, 0, 0 };
            cmbAssortId.DataSource = DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM PUR.VAssortmentPending WHERE PendingWt>0 AND Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text)).Tables[0];
            if (selval != 0)
                cmbAssortId.SelectedValue = selval;
        }

        public   double GetNextID(Table table_name, string field_name, DataController dc, int company_id, int branchID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GEN.NEXTID";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@table_name", (table_name.DatabaseName.ToUpper() == dc.ConnectionProperties.DatabseName.ToUpper() ? "" : table_name.DatabaseName + ".") + table_name.OwnerName + "." + table_name.Name);
            cmd.Parameters.AddWithValue("@field_name", field_name);
            cmd.Parameters.AddWithValue("@company_ID", company_id);
            cmd.Parameters.AddWithValue("@branch_id", branchID);
            cmd.Parameters.AddWithValue("@ResValOut", 0);

            return Convert.ToDouble(dc.GetData(cmd).Tables[0].Rows[0][0]);
        }
        public override bool InitializeTables()
        {
            Table t = new Table("JMS", "ACC", "VoucherEntry");
            t.PrimaryKeys.Add("VoucherEntryId");
            t.IdTextBox = TxtEntryNo;
            Table t1 = new Table("JMS", "ACC", "VoucherEntryDetails");
            t1.PrimaryKeys.Add("VchDetId");
            t1.IdTextBox = TxtTranscId;
            t1.IsDatagridView = false;
            t.ChildTables.Add(t1);
            this.TableName = t;
            return true;


        }
        public override void Init()
        {
            base.Init();
            TxtIsactive.Text = "1";


            grbDataGridView1.HiddenDataFields = new List<string>() { "JobNoId", "Company_id", "Branch_id" };
            grbDataGridView1.SummaryColumns = new string[] { "OrnamentWt", "GattingWt", "MountingWt", "WtLoss1", "StudRetWt", "WtLoss2", "TotalWtLoss", "BalanceWt" };
            grbDataGridView1.DataSource = this.DBConn.GetData(new SqlCommand("Select JobNoId," + "Date,OrnamentWt ,GattingWt,MountingWt,WtLoss1,StudRetWt,WtLoss2,TotalWtLoss,BalanceWt" + " From PROD.VBuffingReport WHERE Company_id=" + txtcompId.Text)).Tables[0];


            //if (Cmb_LedgerHead.SelectedValue != null)
            //{
            //    txtLedgerId.Text = Cmb_LedgerHead.SelectedValue.ToString();


            //}
            //if (this.TableName != null)
            //    GenerateID(this.TableName);

        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            Save();

        }

        private void grbButton2_Click(object sender, EventArgs e)
        { // MessageBox.Show( );
        }
    }
}
