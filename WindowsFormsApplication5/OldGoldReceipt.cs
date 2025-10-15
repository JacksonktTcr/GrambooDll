using Gramboo.Database;
//using Kallans.Forms.CRM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kallans.Forms.SALE
{
    public partial class OldGoldReceipt : Gramboo.Controls.GrbForm
    {
        private static OldGoldReceipt instance;
        public SalesMaster s ;
        public static OldGoldReceipt Instance
        {
             get
             {
                if (instance == null)
                {
                    instance = new OldGoldReceipt();
                }
                else if (instance.IsDisposed)
                {
                    instance = new OldGoldReceipt();
                }
                return instance;
            }
        }
        public OldGoldReceipt()
        {
            InitializeComponent();
            txtDiaNo.TextChanged += new EventHandler(Wtin916);
            txtPurity.TextChanged += new EventHandler(Wtin916); 
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
                AdjustColumnWidths();
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool InitializeTables()
        {
            Table t = new Table(Kallans.Classes.Common.DbName, "SALE", "OldGoldReciptMaster");
            t.PrimaryKeys.Add("EntryId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = TxtEntryNo;
            Table t1 = new Table(Kallans.Classes.Common.DbName, "SALE", "OldGoldReceiptMaterials", true);
            t1.PrimaryKeys.Add("TransId");
            t1.FillView = new Table(Kallans.Classes.Common.DbName, "SALE", "VOldGoldReceiptMaterials");
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
           // Cmb_SalesNo.Text = "1";
            txtpurity1.Text = "92"; 
           // Cmb_CustomerName.Text = "1";
            dgv.DataFields = new List<string>() { "EntryId", "TransId", "ItemId", "[Item Name]", "Purity", "Nos", "Gwt", "NetWt", "DiaNo",
                "DiaWt","StoneWt","Rate", "Amount", "IsReceipt" };
            dgv.HiddenDataFields = new List<string>() { "EntryId", "TransId", "ItemId", "IsReceipt" };
            dgv.SummaryColumns = new string[] { "Gwt", "NetWt", "Nos", "Rate", "Amount" };
            dgv.Fill(new Table(Kallans.Classes.Common.DbName, "SALE", "VOldGoldReceiptMaterials", true), "1=2");
            dgv.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            AdjustColumnWidths();
            Cmb_CustomerName.Focus();
            this.ListForm = OldGoldReceiptList.Instance;  

            if (this.TableName != null)
                GenerateID(this.TableName);
        }
        public override void RefreshData()
        {
            base.RefreshData();
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            cmb_VoucherTypeId.SelectedValue = 8;
            if (!IsEditMode)
                TxtVoucherNo.Text = Kallans.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_dt.Value,
            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            Gramboo.General.Setupcombo(Cmb_ItemName, "ITM.VItemMaster", "[Item Name]", "ItemId", "[Is Active]='True' AND ItemTypeId IN(1,3)");
            Gramboo.General.Setupcombo(Cmb_CustomerName, "CRM.CustomerMaster","CustName","CustId","IsActive='True'");


            if (Dtp_dt.Value != null)
            {

              //  txtRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 OldGoldRate  from GEN.MetalRate   WHERE  EntryDate<= '" + Dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString();
            }
            if (s != null)
            {
                s.txt_OldGoldReceipt.Text = txtTotAmount.Text;
                s.txt_OldGoldReceipt.Tag = TxtEntryNo.Text;
                txtSalesID.Text = s.TxtSaleId.Text;
            }
        }
        public override bool ValidateControls()
        {
            if (s != null)
            {
                s.txt_OldGoldReceipt.Text = txtTotAmount.Text;
                s.txt_OldGoldReceipt.Tag = TxtEntryNo.Text;
                txtSalesID.Text = s.TxtSaleId.Text;
            }
           
            return base.ValidateControls();
        }

        public void fillOldGoldReceipt()
        {
            
            if (txtSalesID.Text != "")
            {

                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select EntryId FROM SALE.OldGoldReciptMaster WHERE SalesId=" + txtSalesID.Text)).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        TxtEntryNo.Text = dt.Rows[0][0].ToString();
                        Dictionary<string, object> d = new Dictionary<string, object>();
                        d.Add("EntryId", TxtEntryNo.Text);
                        d.Add("Company_id", txtcompId.Text);
                        this.FillData(d);
                    }
                    else
                    {
                        Init();
                    }
                }

            }
            else
            {

                Init();
            }
        }
        private void AdjustColumnWidths()
        {
            dgv.RowHeadersVisible = false;
            dgv.Columns[0].Width = 40;
            dgv.Columns["Item Name"].Width = Cmb_ItemName.Width + 6;
            dgv.Columns["Nos"].Width = TxtNos.Width + 6;
            dgv.Columns["Purity"].Width = TxtNos.Width + 6;       
            dgv.Columns["Gwt"].Width = txtGwt.Width + 6;
            
            dgv.Columns["Rate"].Width = txtRate.Width + 6;
            dgv.Columns["Amount"].Width = txtAmount.Width + 6; 
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            chk_Receipt.Checked = true;  
            if (Cmb_ItemName.SelectedValue != null)
            {
                txtitemid.Text = Cmb_ItemName.SelectedValue.ToString();
                dgv.Save();
            }
            txtTotAmount.Text = dgv.SummaryRow.SummaryCells["Amount"].Text;
        }

        private void Cmb_ItemName_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Cmb_ItemName.SelectedValue != null)
            {
                txtPurity.Text = (DBConn.GetData(new SqlCommand("SELECT * FROM ITM.VItemMaster WHERE ItemId=" + Cmb_ItemName.SelectedValue + "")).Tables[0].Rows[0]["Purity Value"]).ToString();
                if (Dtp_dt.Value != null)
                {
                    if (DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VItemMaster WHERE ItemId=" + Cmb_ItemName.SelectedValue.ToString() + "AND ItemGroupId IN(1,11)")).Tables[0].Rows.Count != 0)
                    {
                        txtRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 OldGoldRate  from GEN.MetalRate   WHERE  EntryDate<= '" + Dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["OldGoldRate"]).ToString();
                    }
                    if (DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VItemMaster WHERE ItemId=" + Cmb_ItemName.SelectedValue.ToString() + "AND ItemGroupId IN(2,12)")).Tables[0].Rows.Count != 0)
                    {
                        txtRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 OldSilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + Dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["OldSilverRate"]).ToString();
                    }
                    if (DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VItemMaster WHERE ItemId=" + Cmb_ItemName.SelectedValue.ToString() + "AND ItemGroupId IN(3,13)")).Tables[0].Rows.Count != 0)
                    {
                        txtRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 PlatinumRate  from GEN.MetalRate   WHERE  EntryDate<= '" + Dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["PlatinumRate"]).ToString();
                    }
                }
            }
        }

        private void txtGwt_TextChanged(object sender, EventArgs e)
        {
            //if (txtPurity.Text != "" && txtGwt.Text != "")
            //{
            //    TxtActual_wt.Text = JMS_RET.Classes.Common.GetActualWeight(Convert.ToSingle(txtPurity.Text), Convert.ToSingle(txtGwt.Text)).ToString();
            //}
            
        }
        public void Amount()
        {
            float rate = 0, wt = 0,Amount;
            txtNetWt.Text = (string.IsNullOrEmpty(txtNetWt.Text.Trim()) ? "0.00" : txtNetWt.Text);
            txtRate.Text = (string.IsNullOrEmpty(txtRate.Text.Trim()) ? "0.00" : txtRate.Text);

            wt = Convert.ToSingle(txtNetWt.Text);
            rate = Convert.ToSingle (txtRate.Text);
            Amount = wt * rate;
            txtAmount.Text = Amount.ToString();  
 
        }
        public void Wtin916(object sender,EventArgs e )
        {
            //float Wt = 0, Gwt = 0, purity = 0,purity1=0;
            //if (Cmb_ItemName.SelectedValue != null)
            //{
            //    if (txtPurity.Text != "" && txtDiaNo.Text != "")
            //    {
            //        purity = Convert.ToSingle(txtPurity.Text);
            //        Gwt = Convert.ToSingle(txtDiaNo.Text);
            //        purity1 = Convert.ToSingle(txtpurity1.Text);   
            //      //  Wt = Gwt * purity /(916/10);
            //        Wt = Gwt * purity / purity1;
            //        TxtWt916.Text = Wt.ToString(); 

            //    }
            //}
        }

        public void NetWt()
        {
            float NetWt = 0, gwt = 0, stwt = 0, DiaWt = 0;

          //  txtNetWt.Text = (string.IsNullOrEmpty(txtNetWt.Text.Trim()) ? "0.00" : txtNetWt.Text);
            txtGwt.Text = (string.IsNullOrEmpty(txtGwt.Text.Trim()) ? "0.00" : txtGwt.Text);
            txtStWt.Text = (string.IsNullOrEmpty(txtStWt.Text.Trim()) ? "0.00" : txtStWt.Text);
            txtDiaWt.Text = (string.IsNullOrEmpty(txtDiaWt.Text.Trim()) ? "0.00" : txtDiaWt.Text);
         //   txtRate.Text = (string.IsNullOrEmpty(txtRate.Text.Trim()) ? "0.00" : txtRate.Text);
            gwt = Convert.ToSingle(txtGwt.Text);
            stwt = Convert.ToSingle(txtStWt.Text);
            DiaWt = Convert.ToSingle(txtDiaWt.Text);

               NetWt = gwt - ((stwt + (DiaWt* Convert.ToSingle(0.2)))) ;
               txtNetWt.Text = NetWt.ToString();  
        }
        private void txtPurity_TextChanged(object sender, EventArgs e)
        {
            //txtGwt_TextChanged(sender, e);
          
        }
        public override void Print()
        {
            if (TxtEntryNo.Text != null)
            {
                //CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //cr = new JMS_RET.REPORTS.SALE.oldGoldReceipt();
                //cr.SetDatabaseLogon(DBConn.ConnectionProperties.DBUsername, DBConn.ConnectionProperties.DBPassword);
                //cr.SetParameterValue("@Printed By", txtUserName.Text);
                //cr.VerifyDatabase();
                //cr.SetParameterValue("@Printed By", txtUserName.Text);
                //cr.RecordSelectionFormula = " {VOldGoldReceiptList.EntryId} =" + TxtEntryNo.Text;
                //Gramboo.Controls.GrbReport rpt = new Gramboo.Controls.GrbReport(cr);
                //rpt.MdiParent = this.MdiParent;

                //((frmMain)this.MdiParent).ShowChild(rpt);
            }
        }

        private void TxtActual_wt_TextChanged(object sender, EventArgs e)
        {
            //Amount(); 
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            Amount();
        }

        private void dgv_SummaryCalculated(object source, EventArgs e)
        {
            txtTotAmount.Text = dgv.SummaryRow.SummaryCells["Amount"].Text;
        }

        private void txtGwt_TextChanged_1(object sender, EventArgs e)
        {
            NetWt();
        }

        private void txtStWt_TextChanged(object sender, EventArgs e)
        {
            NetWt();
        }

        private void txtDiaWt_TextChanged(object sender, EventArgs e)
        {
            NetWt();
        }

        private void txtNetWt_TextChanged(object sender, EventArgs e)
        {
            Amount();
        }

        private void Dtp_dt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            Kallans.Forms.CRM.CustomerMasterr frm = new CustomerMasterr();
            frm.MdiParent = this.ParentForm;
            ((frmMain)this.ParentForm).ShowChild(frm);
            frm.Focus();
        }

    }
}
