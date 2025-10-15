using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using Gramboo;
using Gramboo.Database;
using Gramboo.Controls;

namespace SAFA.Forms.ACC
{
    public partial class AdjustingEntries : Gramboo.Controls.GrbForm
    {
        public string RecTable = "", RecId = "", RecCode = "", RecVchNo = "", BillType = "", PartyType = "", ValAmt = "";
        public Double Amount, Amt = 0, Redeem_Weight = 0, Redeem_MC = 0, Redeem_Amount = 0, AvgMetalRate = 0, WT = 0; public Int64 PartyId = 0;
        Int64 LedgerId = 0, TransId = 0; int i = 0;
        public DateTime dt;
        bool ed = false;

        public delegate void CloseClickEventHandler(object sender, CloseClickEventArgs e);
        public event CloseClickEventHandler CloseClick;
        public class CloseClickEventArgs : System.EventArgs
        {

        }

        public delegate void Grid_SummaryCalculated(object source, Grid_SummaryCalculatedEventArgs e);
        public event Grid_SummaryCalculated Summary;
        public class Grid_SummaryCalculatedEventArgs : System.EventArgs
        {

        }

        public delegate void AutoAdjust_EventHandler(object sender, AutoAdjust_EventArgs e);
        public event AutoAdjust_EventHandler AutoAdjustClick;
        public class AutoAdjust_EventArgs : System.EventArgs
        {

        }
        public AdjustingEntries()
        {
            InitializeComponent();
        }
        public override bool InitializeTables()
        {
            Table t = new Table(SAFA.Classes.Common.DbName, "ACC", "AdjustingEntrieMaster");
            t.PrimaryKeys.Add("AdjId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = TxtAdjId;

            Table t1 = new Table(SAFA.Classes.Common.DbName, "ACC", "AdjustingEntrieDetails", true);
            t1.PrimaryKeys.Add("TransId");
            t1.NotUpdatables.Add("Company_id");
            t1.NotUpdatables.Add("Branch_id");
            t1.NotUpdatables.Add("Counter_id");
            t1.FillView = new Table(SAFA.Classes.Common.DbName, "ACC", "VAdjustingEntrieDetails");
            t1.DatagridView = dgview;
            t1.IsDatagridView = true;
            t1.IdTextBox = TxtTransId;

            t.ChildTables.Add(t1);


            this.TableName = t;
            return true;

        }
        public override bool GenerateID(Table table_name)
        {
            try
            {
                table_name.IdTextBox.Text = SAFA.Classes.Common.GetNextID(table_name, table_name.IdTextBox.DataField, DBConn, Convert.ToInt32(base.txtcompId.Text), Convert.ToInt32(base.txtBranchID.Text)).ToString();
                return true;
            }
            catch (Exception) { return false; }
        }
        public override void Init()
        {
            if (ed == true) { CmbPendingBill.Text = ""; dgview.SummaryRowVisible = true; RefreshData(); return; }
            base.Init();
            dgview.DataFields = new List<string> { "AdjId", "TransId", "BillId", "BillNo", "PendingAmt",  "PendingMCAmt", "MCAmt", "PendingNonWtAmt", "NonWtAmt", "RateCuttingMode", "[Rate Cutting]", "Rate", "PendingWeight", "Weight", "PaymentAmt" };
            dgview.HiddenDataFields = new List<string> { "AdjId", "TransId", "BillId", "RateCuttingMode", "PendingNonWtAmt", "PendingMCAmt", "PendingWeight", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Company_id", "Branch_id", "Counter_id", "IsActive" };
            dgview.SummaryColumns = new string[] { "PendingAmt", "PaymentAmt", "NonWtAmt", "MCAmt", "Weight", "PendingAmt", "PendingNonWtAmt", "PendingWeight" };
            dgview.Fill(new Table(SAFA.Classes.Common.DbName, "ACC", "VAdjustingEntrieDetails", true), "1=2");
            dgview.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            dgview.Columns["BillNo"].Width = 140;
            dgview.Columns["PendingAmt"].Width = 110;
            dgview.Columns["PaymentAmt"].Width = 110;
        }

        public override void RefreshData()
        {
            base.RefreshData();
            Get_Details();
            GetLedgerId(PartyType);
            PendingBillNo(LedgerId, BillType);
            dgview.HiddenDataFields = new List<string> { "AdjId", "TransId", "BillId", "RateCuttingMode", "PendingNonWtAmt", "PendingMCAmt", "PendingWeight", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Company_id", "Branch_id", "Counter_id", "IsActive" };
            dgview.SummaryColumns = new string[] { "PendingAmt", "PaymentAmt", "NonWtAmt", "MCAmt", "Weight", "PendingAmt", "PendingNonWtAmt", "PendingWeight" };
            CmbPendingBill.CheckDuplicates = true;
        }

        public void Get_Details()
        {
            TxtRecTable.Text = RecTable;
            TxtRecId.Text = RecId;
            TxtRecVchNo.Text = RecVchNo;
            TxtRecCode.Text = RecCode;
            DtpDate.Value = dt;
            TxtAmount.Text = Amount.ToString("f2");
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            /*dgview.SummaryRowVisible = true;*/
            validateAmt();
            if (CmbPendingBill.SelectedValue == null) { CmbPendingBill.ShowMessage("Choose A BillNo"); return; }
            if (Convert.ToDouble(TxtPaymentAmt.Text) <= 0) { TxtPaymentAmt.ShowMessage("Cannot Accept This Amount"); return; }

            if (Math.Round(Convert.ToDouble(TxtPendingAmt.Text), 2) - Math.Round(Convert.ToDouble(TxtPaymentAmt.Text), 2) < 0)
            {
                String A = Convert.ToString(Math.Abs(Math.Round(Convert.ToDouble(TxtPendingAmt.Text), 2) - Math.Round(Convert.ToDouble(TxtPaymentAmt.Text), 2)));
                TxtPaymentAmt.ShowMessage("you have entered " + A + " ₹ greater than your bill amount");
                return;
            }

            if (ValAmt == "")
            {
                if (Math.Round(Convert.ToDouble(TxtAmount.Text), 2) - Math.Round(Convert.ToDouble(TxtPaymentAmt.Text), 2) < 0)
                {
                    String A = Convert.ToString(Math.Abs(Math.Round(Convert.ToDouble(TxtAmount.Text), 2) - Math.Round(Convert.ToDouble(TxtPaymentAmt.Text), 2)));
                    TxtAmount.ShowMessage("Withdrawal Limit Exceeded Add " + A + " ₹ More to Continue");
                    return;
                }
            }

            Double Weight = 0, Met_Rate = 0;
            Met_Rate = Convert.ToDouble(TxtMetalRate.Text);
            Weight = Convert.ToDouble(TxtWt.Text == "" ? "0" : TxtWt.Text);
            TxtCurMetalCash.Text = Math.Round(Weight * Met_Rate, 2).ToString("F2");

            TxtRateCutting.Text = CmbRateCutting.SelectedIndex.ToString();
            TxtBillId.Text = CmbPendingBill.SelectedValue.ToString();
            dgview.Save(); PendingBillNo(LedgerId, BillType); TransId = 0;
        }

        private void dgview_SummaryCalculated(object source, EventArgs e)
        {
            Amt = Convert.ToDouble(dgview.SummaryRow.SummaryCells["PaymentAmt"].Text);
            WT = Convert.ToDouble(dgview.SummaryRow.SummaryCells["Weight"].Text);
            TxtAmount.Text = (Math.Round(Amount, 2) - Math.Round(Amt, 2)).ToString("F2");
            OnSummary(new Grid_SummaryCalculatedEventArgs());
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void dgview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgview.Rows[rowIndex];
            try
            {
                dgview.SummaryRowVisible = true; TransId = 0;
                Double Amt = Convert.ToDouble(row.Cells["PaymentAmt"].Value == null ? "0" : row.Cells["PaymentAmt"].Value.ToString());
                TxtAmount.Text = (Math.Round(Convert.ToDouble(TxtAmount.Text), 2) + Math.Round(Amt, 2)).ToString();

                TransId = Convert.ToInt64(row.Cells["TransId"].Value == null ? "0" : row.Cells["TransId"].Value.ToString());

                if (TransId != 0)
                {
                    string str;
                    SqlCommand cmd = new SqlCommand();
                    str = "select t2.BillId,t2.BillNo From ACC.AdjustingEntrieDetails as t1 inner join ACC.VPendingBillNo as t2 on t1.BillId=t2.BillId where t1.AdjId=" + TxtAdjId.Text + " and t1.TransId=" + TransId + "";
                    cmd.CommandText = str;
                    CmbPendingBill.DisplayMember = "BillNo";
                    CmbPendingBill.ValueMember = "BillId";
                    cmd.CommandTimeout = 0;
                    CmbPendingBill.DataSource = DBConn.GetData(cmd, "BillNo").Tables[0];
                }
            }
            catch (Exception ex) { }
        }

        //110100005858 - LLP 
        void PendingBillNo(Int64 LedgerId, string Type)
        {
            CmbPendingBill.SelectedValueChanged -= CmbPendingBill_SelectedValueChanged;
            bool BillWise = false;
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                       ("select isnull(BillWise,0) as BillWise from ACC.LedgerMaster where Acc_LedgerId =" + LedgerId + " and Company_id=" + Gramboo.GeneralConfig.CompanyID + " and Branch_id=" + Gramboo.GeneralConfig.BranchId + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    BillWise = Convert.ToBoolean(dt.Rows[0]["BillWise"].ToString());
                }
            }
            string str;
            SqlCommand cmd = new SqlCommand();
            if (LedgerId != 0 && BillWise == true)
            {
                str = "Select BillId,BillNo from ACC.FunPendingBillNo('" + LedgerId + "'," + Gramboo.GeneralConfig.CompanyID + "," + Gramboo.GeneralConfig.BranchId + "  )where IsActive='True' and Type='" + Type + "'";
                cmd.CommandText = str;
                CmbPendingBill.DisplayMember = "BillNo";
                CmbPendingBill.ValueMember = "BillId";
                CmbPendingBill.DataSource = DBConn.GetData(cmd, "BillNo").Tables[0];
            }
            CmbPendingBill.Text = "";
            CmbPendingBill.SelectedValueChanged += CmbPendingBill_SelectedValueChanged;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
            Init();
        }

        void GetLedgerId(string PartyType)
        {
            if (PartyType == "")
                return;
            if (PartyType == "Supplier")
            {
                using (DataTable dtSupp = DBConn.GetData(new SqlCommand("select Isnull(Acc_LedgerID,0) as Acc_LedgerID from PUR.SupplierMaster where SuppId='" + PartyId.ToString() + "'")).Tables[0])
                {
                    if (dtSupp.Rows.Count > 0)
                    {

                        LedgerId = Convert.ToInt64(dtSupp.Rows[0]["Acc_LedgerID"].ToString());
                    }
                }
            }
            else if (PartyType == "Customer")
            {
                using (DataTable dtCust = DBConn.GetData(new SqlCommand("select Isnull(Acc_LedgerID,0) as Acc_LedgerID from CRM.CustomerMaster where Custid='" + PartyId.ToString() + "'")).Tables[0])
                {
                    if (dtCust.Rows.Count > 0)
                    {
                        LedgerId = Convert.ToInt64(dtCust.Rows[0]["Acc_LedgerID"].ToString());
                    }
                }
            }
        }

        private void AdjustingEntries_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnCloseClick(new CloseClickEventArgs());
        }

        private void TxtPaymentAmt_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == TxtPaymentAmt)
            {
                Calc_WT();
            }
        }

        void Calc_WT()
        {
            double PaymentAmt = 0, NonWtAmt = 0,MCAmt=0, Wt = 0, Rate = 0;

            if (TxtPaymentAmt.Text != "")
            {
                PaymentAmt = Convert.ToDouble(TxtPaymentAmt.Text);
                NonWtAmt = Convert.ToDouble(TxtNonWTAmount.Text);
                MCAmt = Convert.ToDouble(TxtMcAmt.Text);

                if (TxtRate.Text != "0")
                {
                    Rate = Convert.ToDouble(TxtRate.Text);
                    Wt = (PaymentAmt - (NonWtAmt+ MCAmt)) / Rate;
                }
                if (Wt > 0) { TxtWt.Text = Math.Round(Wt, 3).ToString(); }
                else { TxtWt.Text = "0"; }
            }
        }

        void Calc_AMT()
        {
            double PaymentAmt = 0, NonWtAmt = 0,MCAmt=0, Wt = 0, Rate = 0;

            Wt = Convert.ToDouble(TxtWt.Text);
            NonWtAmt = Convert.ToDouble(TxtNonWTAmount.Text);
            MCAmt = Convert.ToDouble(TxtMcAmt.Text);

            if (TxtRate.Text != "0")
            {
                Rate = Convert.ToDouble(TxtRate.Text);
                PaymentAmt = Math.Round(Rate * Wt, 2);
            }
            TxtPaymentAmt.Text = (PaymentAmt + (NonWtAmt+ MCAmt)).ToString("f2");
        }

        void Calc_NonwtAmt()
        {
            double PendingAmt = 0, PendingNonWtAmt = 0, NonWtAmt = 0;

            PendingNonWtAmt = Convert.ToDouble(TxtPendingNonWTAmount.Text);
            PendingAmt = (Convert.ToDouble(TxtPendingAmt.Text) - PendingNonWtAmt);

            NonWtAmt = Convert.ToDouble(TxtNonWTAmount.Text);

            TxtPaymentAmt.Text = (PendingAmt + NonWtAmt).ToString("f2");
        }

        private void TxtMetalRate_TextChanged(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow r in dgview.Rows)
            //{
            //    Double Weight = 0, MetalCash = 0, Met_Rate = 0;
            //    Met_Rate = Convert.ToDouble(TxtMetalRate.Text);

            //    if (r.Cells["BillId"].Value.ToString().Trim().Length >= 2)
            //    {
            //        Weight = Convert.ToDouble(r.Cells["Weight"].Value.ToString() == "" ? "0" : r.Cells["Weight"].Value.ToString());
            //        MetalCash = Math.Round(Weight * Met_Rate, 2);
            //        r.Cells["Current_Metal_Cash"].Value = MetalCash.ToString("F2");
            //    }
            //}
        }

        private void TxtWt_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == TxtWt)
            {
                Calc_AMT();
            }
        }

        private void TxtNonWTAmount_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == TxtNonWTAmount)
            {
                Calc_NonwtAmt();
            }
        }

        private void BtnAutoAdjust_Click(object sender, EventArgs e)
        {
            OnAutoAdjustClick(new AutoAdjust_EventArgs());
        }

        private void TxtRate_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == TxtRate)
            {
                Calc_WT();
            }
        }

        public virtual void OnCloseClick(CloseClickEventArgs e)
        {
            if (CloseClick != null) { CloseClick(this, e); }
        }

        public virtual void OnSummary(Grid_SummaryCalculatedEventArgs e)
        {
            if (Summary != null) { Summary(this, e); }
        }

        public virtual void OnAutoAdjustClick(AutoAdjust_EventArgs e)
        {
            if (AutoAdjustClick != null) { AutoAdjustClick(this, e); }
        }
        private void CmbPendingBill_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbPendingBill.SelectedValue == null)
            {
                TxtPendingAmt.Text = "";
                TxtPaymentAmt.Text = "";
                return;
            }
            if (CmbPendingBill.SelectedValue.ToString() != "0")
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select BalanceAmount,MCAmount,NonweightAmount,[Rate Cutting],GoldRate,[Balance WT 99.5] FROM ACC.FunPendingBillNo('" + LedgerId + "'," + txtcompId.Text + "," + txtBranchID.Text + " )where BillId='" + CmbPendingBill.SelectedValue + "' and IsActive='True'"), "Tbl").Tables[0])


                    if (dt.Rows.Count > 0)
                    {
                        TxtPendingAmt.Text = dt.Rows[0]["BalanceAmount"].ToString();
                        TxtPaymentAmt.Text = dt.Rows[0]["BalanceAmount"].ToString();
                        TxtPendingMcAmt.Text = dt.Rows[0]["MCAmount"].ToString();
                        TxtMcAmt.Text = dt.Rows[0]["MCAmount"].ToString();
                        TxtPendingNonWTAmount.Text = dt.Rows[0]["NonweightAmount"].ToString();
                        TxtNonWTAmount.Text = dt.Rows[0]["NonweightAmount"].ToString();
                        CmbRateCutting.SelectedIndex = Convert.ToInt32(dt.Rows[0]["Rate Cutting"].ToString());
                        TxtWt.Text = dt.Rows[0]["Balance WT 99.5"].ToString();
                        if (CmbRateCutting.SelectedIndex == 1)
                        {
                            TxtRate.ReadOnly = true; TxtRate.Text = dt.Rows[0]["GoldRate"].ToString();
                            if (TxtRate.Text == "0")
                            { TxtRate.ReadOnly = true; TxtRate.Text = TxtMetalRate.Text; Calc_WT(); }
                        }
                        else { TxtRate.ReadOnly = true; TxtRate.Text = dt.Rows[0]["GoldRate"].ToString(); }
                    }
            }
        }
        public override bool Save()
        {
            if (base.Save()) { return false; } else { return false; }
        }
        public override bool Delete()
        {
            if (base.Delete()) { ed = false; return false; } else { return false; }
        }
        public void Fill(string RecId)
        {
            if (RecId == null)
                return;
            using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand("select AdjId From ACC.AdjustingEntrieMaster where RecId='" + RecId.ToString() + "' and RecTable='" + RecTable.ToString() + "' and Company_id=" + GeneralConfig.CompanyID + " and Branch_id=" + GeneralConfig.BranchId + " ")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    ed = true;
                    TxtAdjId.Text = dt.Rows[0][0].ToString();
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    d.Add("AdjId", TxtAdjId.Text);
                    d.Add("Company_id", GeneralConfig.CompanyID);
                    d.Add("Branch_id", GeneralConfig.BranchId);
                    this.FillData(d);
                    RefreshData();

                }
                else
                {
                    ed = false;
                }
            }

        }

        void validateAmt()
        {
            string BillId = "-", Amount = "0";
            if (TransId > 2)
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                            ("select BillId,PaymentAmt From ACC.AdjustingEntrieDetails where AdjId=" + TxtAdjId.Text + " and TransId=" + TransId + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        BillId = dt.Rows[0]["BillId"].ToString();
                        Amount = dt.Rows[0]["PaymentAmt"].ToString();
                    }
                }

                if (BillId.ToString().Trim().Length > 2)
                {
                    using (DataTable dt = DBConn.GetData(new SqlCommand("Select Isnull(BalanceAmount,0) as BalanceAmount FROM ACC.FunPendingBillNo('" + LedgerId + "'," + txtcompId.Text + "," + txtBranchID.Text + " )where BillId='" + BillId + "' and IsActive='True'"), "Tbl").Tables[0])

                        if (dt.Rows.Count > 0)
                        {
                            Double a = Convert.ToDouble(dt.Rows[0]["BalanceAmount"].ToString());
                            TxtPendingAmt.Text = Convert.ToString((Math.Round(Convert.ToDouble(Amount), 2) + Math.Round(a, 2)));
                        }
                        else { TxtPendingAmt.Text = Amount; }
                    if (TxtPendingAmt.Text == "0") { TxtPendingAmt.Text = Amount; }
                }
            }
        }

        public void AutoAdjust()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ACC.Proc_Auto_Adjust_Bill_Adjustments";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LedgerId", LedgerId);
            cmd.Parameters.AddWithValue("@Redeem_Rate", 0);
            cmd.Parameters.AddWithValue("@Redeem_Weight", Redeem_Weight);
            cmd.Parameters.AddWithValue("@Redeem_MC", Redeem_MC);
            cmd.Parameters.AddWithValue("@Redeem_Amount", Redeem_Amount);
            cmd.Parameters.AddWithValue("@Redeem_Type", BillType);
            cmd.Parameters.AddWithValue("@CompanyId", GeneralConfig.CompanyID);
            cmd.Parameters.AddWithValue("@BranchId", GeneralConfig.BranchId);

            DataTable dtTempPending = DBConn.GetData(cmd).Tables[0];

            int rowCount = dgview.Rows.Count;
            for (int i = 0; i < rowCount; i++) { dgview.Rows.RemoveAt(0); }

            foreach (DataRow dr in dtTempPending.Rows)
            {
                DataRow r = ((DataTable)dgview.DataSource).NewRow();

                r["AdjId"] = dr["AdjId"];
                r["TransId"] = dr["TransId"];
                r["BillId"] = dr["BillId"];
                r["BillNo"] = dr["BillNo"];
                r["PendingAmt"] = dr["PendingAmt"];
                r["PendingNonWtAmt"] = dr["PendingNonWtAmt"];
                r["NonWtAmt"] = dr["NonWtAmt"];
                r["PendingMCAmt"] = dr["PendingMCAmt"];
                r["MCAmt"] = dr["MCAmt"];
                r["RateCuttingMode"] = dr["RateCuttingMode"];
                r["Rate Cutting"] = dr["Rate Cutting"];
                r["Rate"] = dr["Rate"];
                r["PendingWeight"] = dr["PendingWeight"];
                r["Weight"] = dr["Weight"];
                r["PaymentAmt"] = dr["PaymentAmt"];
                //r["Current_Metal_Cash"] = dr["Current_Metal_Cash"];

                ((DataTable)dgview.DataSource).Rows.Add(r);
            }
            AvgMetalRate = Math.Round((Convert.ToDouble(dgview.SummaryRow.SummaryCells["PaymentAmt"].Text)) / (Convert.ToDouble(dgview.SummaryRow.SummaryCells["Weight"].Text)), 2);
        }
    }
}