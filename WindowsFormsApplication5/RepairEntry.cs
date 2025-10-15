using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace JMS.Forms.PROD
{
    public partial class RepairEntry : Gramboo.Controls.GrbForm
    {

        private static RepairEntry instance;
        private bool flag;
        public static RepairEntry Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new RepairEntry();
                }
                else if (instance.IsDisposed)
                {
                    instance = new RepairEntry();
                }
                return instance;
            }
        }
        public RepairEntry()
        {
            InitializeComponent();
            dtp_JobRetDate.ValueChanged += new EventHandler(CalculateMcAndWst);
            dtp_JobRetDate.ValueChanged += new EventHandler(CalculateMcAndWstWorker);
            txtSetDiaWt.TextChanged += new EventHandler(CalculateMcAndWst);
            txtSetDiaNos.TextChanged += new EventHandler(CalculateMcAndWst);

            dgv.SummaryCalculated += new Gramboo.Controls.SummaryCalcEventHandler(SplitWeight);
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
            Table t = new Table("JMS", "PROD", "RepairMaster");
            t.PrimaryKeys.Add("RepairId");
            t.IdTextBox = txtRepairNo;
            Table t1 = new Table("JMS", "PROD", "RepairMaterials", true);
            t1.PrimaryKeys.Add("TransId");

            t1.FillView = new Table("JMS", "PROD", "VRepairMaterialsStone");
            t1.DatagridView = dgv;
            t1.IsDatagridView = true;
            t1.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t1);

            Table t2 = new Table("JMS", "PROD", "RepairMaterials", true);
            t2.PrimaryKeys.Add("TransId");

            t2.FillView = new Table("JMS", "PROD", "VRepairMaterialsMetal");
            t2.DatagridView = dgvMetal;
            t2.IsDatagridView = true;
            t2.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t2);

            Table t3 = new Table("JMS", "PROD", "RepairMaterialsStock", true);
            t3.PrimaryKeys.Add("TransId");

            t3.FillView = new Table("JMS", "PROD", "VRepairMaterialsStock");
            t3.DatagridView = dgvStk ;
            t3.IsDatagridView = true;
            t3.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t3);
            this.TableName = t;
            return true;
        }
        public override void Init()
        {

            base.Init();
            TxtIsactive.Text = "1";
            dgv.DataFields = new List<string>() { "RepairId", "TransId", "SizeRangeId", "ItemId", "TransDate", "[Item Name]", "SizeRangeName", "PriceRangeName", "SieveName", "Nos", "Wt", "IsReceipt", "PriceRangeId", "IsDamaged", "IsLost", "SieveId,wst,MCVal" };
            dgv.HiddenDataFields = new List<string>() { "RepairId", "TransId", "ItemId", "SieveId", "SizeRangeId", "PriceRangeId"};
            dgv.SummaryColumns = new string[] { "Wt", "Nos" };
            dgv.Fill(new Table("JMS", "PROD", "VRepairMaterialsStone", true), "1=2");
            dgv.Columns["col_AutoSlno"].DataPropertyName = "SlNo";

            dgvMetal.DataFields = new List<string>() { "RepairId", "TransId", "SizeRangeId", "ItemId", "TransDate", "[Item Name]", "SizeRangeName", "PriceRangeName", "SieveName", "Nos", "Wt", "IsReceipt", "PriceRangeId", "IsDamaged", "IsLost", "SieveId","wst","MCVal" };
            dgvMetal.HiddenDataFields = new List<string>() { "RepairId", "TransId", "ItemId", "SieveId", "SizeRangeId", "PriceRangeId", "SizeRangeName", "PriceRangeName", "SieveName","IsReceipt", "IsLost", "IsDamaged","Nos" };
            dgvMetal.SummaryColumns = new string[] { "Wt", "wst", "MCVal" };
            dgvMetal.Fill(new Table("JMS", "PROD", "VRepairMaterialsMetal", true), "1=2");
            dgvMetal.Columns["col_AutoSlno"].DataPropertyName = "SlNo";

            dgvStk.DataFields = new List<string>() { "RepairId", "TransId", "ItemId", "TransDate", "[Item Name]",   "Wt", "IsReceipt" };
            dgvStk.HiddenDataFields = new List<string>() { "RepairId", "TransId", "ItemId" };
            dgvStk.SummaryColumns = new string[] { "Wt" };
            dgvStk.Fill(new Table("JMS", "PROD", "VRepairMaterialsStock", true), "1=2");
            dgvStk.Columns["col_AutoSlno"].DataPropertyName = "SlNo";

            AdjustColumnWidths();
            this.ListForm = RepairEntryList.Instance;

            //chk_Damaged.Enabled = true;
            //chkISReceipt.Enabled = true;
            //ChkLost.Enabled = true;
 
            if (this.TableName != null)
                GenerateID(this.TableName);

        }

        private void AdjustColumnWidths()
        {
            dgv.RowHeadersVisible = false;
            dgv.Columns[0].Width = 50;
            dgv.Columns["TransDate"].Width = dtp_TransDate.Width + 10;
            dgv.Columns["Item Name"].Width = Cmb_ItemName.Width + 13;
            dgv.Columns["SieveName"].Width = Cmb_Sieve.Width + 5;
            dgv.Columns["SizeRangeName"].Width = Cmb_SizeRange.Width + 5;
            dgv.Columns["PriceRangeName"].Width = Cmb_PriceRange.Width + 5;
            dgv.Columns["Nos"].Width = TxtNos.Width + 5;
            dgv.Columns["Wt"].Width = txtWt.Width + 10;
            dgv.Columns["IsReceipt"].Width = chkISReceipt.Width + 7;
            dgv.Columns["IsDamaged"].Width = chk_Damaged.Width + 7;
            dgv.Columns["IsLost"].Width = ChkLost.Width + 7;



            dgvMetal.RowHeadersVisible = false;
            dgvMetal.Columns[0].Width = 30;
            dgvMetal.Columns["TransDate"].Width = dtpMetaltransdate.Width + 8;
            dgvMetal.Columns["Item Name"].Width = cmbMetalItem.Width + 8;
            dgvMetal.Columns["Wt"].Width = txtMetalWt.Width + 8;

            dgvStk.RowHeadersVisible = false;
            dgvStk.Columns[0].Width = 30;
            dgvStk.Columns["TransDate"].Width = dtpStk.Width + 4;
            dgvStk.Columns["Item Name"].Width = cmbItemStk.Width + 4;
            dgvStk.Columns["Wt"].Width = txtStkWt.Width + 4;
            dgvStk.Columns["IsReceipt"].Width = chkStkRec.Width + 4;
        }
        public override void RefreshData()
        {
            base.RefreshData();
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            cmb_VoucherTypeId.SelectedValue = 32;
            if (!IsEditMode)
                TxtVoucherNo.Text = JMS.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_Issuedt.Value,
             DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            Gramboo.General.Setupcombo(Cmb_WorkShopName, "PROD.WorkShopMaster", "WorkshopName", "WorkshopId", "IsActive='True'AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
           // Gramboo.General.Setupcombo(Cmb_customerName, "CRM.CustomerMaster", "CustName", "CustId", "IsActive='True'AND Company_id=" + txtcompId.Text + "");
            Gramboo.General.Setupcombo(Cmb_JobNo, "PROD.JobNoGeneration", "JobNo", "JobNoId", "IsActive='True' AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
            Gramboo.General.Setupcombo(Cmb_Sieve, "ITM.SieveMaster", "SieveName", "SieveId", "IsActive='True'");
            Gramboo.General.Setupcombo(Cmb_Item_Master, "ITM.VOrnaments", "[Item Name]", "ItemId", "IsActive='True'");
            Gramboo.General.Setupcombo(Cmb_SizeRange, "ITM.SizeRangeMaster", "SizeRangeName", "SizeRangeId", "IsActive='True'");
            Gramboo.General.Setupcombo(Cmb_PriceRange, "ITM.PriceRangeMaster", "PriceRangeName", "PriceRangeId", "IsActive='True'");
            Gramboo.General.Setupcombo(Cmb_ItemName, "ITM.VSettingItems", "[Item Name]", "ItemId", "IsActive='True'");

            Gramboo.General.Setupcombo(cmbMetalItem, "ITM.VGattingItems", "[Item Name]", "ItemId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmbItemStk, "ITM.VGattingItems", "[Item Name]", "ItemId", "IsActive='True'");

            Gramboo.General.Setupcombo(Cmb_customerName, "CRM.VCustomerMasterVisibility", "CustName", "CustId", "IsActive='True' AND CompanyId=" + txtcompId.Text);

            //txtIssueWt.Enabled = false;
            //txtReturnWt.Enabled = false;
            txtIssueWt.AcceptBlankValue = true;
            txtReturnWt.AcceptBlankValue = true;
        }

        public void enable()
        {
            if (Cmb_ItemName.SelectedValue != null)
            {
                JMS.Classes.Common.enableMaterialsSpecs(Convert.ToInt32(Cmb_ItemName.SelectedValue), Cmb_SizeRange, Cmb_PriceRange, Cmb_Sieve, DBConn);
            }
        }


        private void Cmb_WorkShopName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Cmb_WorkShopName_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((Cmb_WorkShopName.ValueMember != null) && (Cmb_WorkShopName.SelectedValue != null))
            {
                Gramboo.General.Setupcombo(Cmb_WorkerName, "PROD.VWorkerMaster", "EmpName", "EmpId", "IsActive=1 AND WorkShopID=" + Cmb_WorkShopName.SelectedValue + "");
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            //if (Cmb_JobName .SelectedValue == null)
            //{
            //    Cmb_JobName.Text = "0";
            //}
            Cmb_SizeRange.AcceptBlankValue = !Cmb_SizeRange.Enabled;
            Cmb_PriceRange.AcceptBlankValue = !Cmb_PriceRange.Enabled;
            Cmb_Sieve.AcceptBlankValue = !Cmb_Sieve.Enabled;

            if (dgv.ValidateControls())
            {
                if (Cmb_SizeRange.SelectedValue == null)
                {
                    txtSizeRangeId.Text = "0";
                }
                else
                {
                    txtSizeRangeId.Text = Cmb_SizeRange.SelectedValue.ToString();
                }

                if (Cmb_PriceRange.SelectedValue == null)
                {
                    txtPriceRangeId.Text = "0";
                }
                else
                {
                    txtPriceRangeId.Text = Cmb_PriceRange.SelectedValue.ToString();
                }
                if (Cmb_Sieve.SelectedValue == null)
                {
                    txtSieveId.Text = "0";
                }
                else
                {
                    txtSieveId.Text = Cmb_Sieve.SelectedValue.ToString();
                }


                if (Cmb_ItemName.SelectedValue != null)
                {
                    TxtNos.Text = (string.IsNullOrEmpty(TxtNos.Text.Trim()) ? "0" : TxtNos.Text);
                    txtItmMc.Text = (txtsetMCpergm.Text.Trim().Length == 0 ? 0 : Convert.ToInt16(TxtNos.Text) * Convert.ToSingle(txtsetMCpergm.Text)).ToString();
                    txtItmWst.Text = (txtSetWstPerc.Text.Trim().Length == 0 ? 0 : Convert.ToInt16(TxtNos.Text) * Convert.ToSingle(txtSetWstPerc.Text) / 100).ToString();

                    //txtPriceRangeId.Text = Cmb_PriceRange .SelectedValue.ToString();
                    // txtSizeRangeId.Text = Cmb_SizeRange .SelectedValue.ToString();
                    txtitemid.Text = Cmb_ItemName.SelectedValue.ToString();
                    //txtSieveId.Text = Cmb_Sieve .SelectedValue.ToString();




                }

                if (validateTransDate())
                {
                    dgv.Save();



                }
            }
        
            

            chk_Damaged.Enabled = true;
            chkISReceipt.Enabled = true;
            ChkLost.Enabled = true;
            dtp_TransDate.Focus();
        }

        private void Cmb_ItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            enable();
        }
        private void grbButton1_Click(object sender, EventArgs e)
        {
            chkmetalDamage.Checked = false;
            chkMetalRec.Checked = true;
            txtMetalNos.Text = "1";
           
            txtMetalsizerange.Text = "0";

            txtmetalPriceRange.Text = "0";

            txtMetalSieve.Text = "0";
            if (cmbMetalItem.SelectedValue != null)
            {
                txtMetalItemId.Text = cmbMetalItem.SelectedValue.ToString();
                txtMetalWt.Text = (string.IsNullOrEmpty(txtMetalWt.Text.Trim()) ? "0" : txtMetalWt.Text);
                txtMcValMetalWorker.Text = (txtMcpergmWkr.Text.Trim().Length == 0 ? 0 : Convert.ToSingle(txtMetalWt.Text) * Convert.ToSingle(txtMcpergmWkr.Text)).ToString();
                txtWstValMetalWorker.Text = (txtWstperWkr.Text.Trim().Length == 0 ? 0 : Convert.ToSingle(txtMetalWt.Text) * Convert.ToSingle(txtWstperWkr.Text) / 100).ToString();
            }
           
            dtpMetaltransdate.Focus();
            if (TransMetalDateValidation())
            {
                dgvMetal.Save();
 
            }
        }

        public void updateMcWstmetal()
        {
            float MetalWt = 0, McPergm = 0, Wstperc = 0,mcval=0,Wst=0;
            foreach (DataGridViewRow r in dgvMetal.Rows)
            {
                MetalWt = (r.Cells["Wt"].Value.ToString().Length == 0 ? 0 : Convert.ToSingle(r.Cells["Wt"].Value.ToString()));
                McPergm = (txtMcpergmWkr.Text.Trim().Length == 0 ? 0 : Convert.ToSingle(txtMcpergmWkr.Text));
                Wstperc = (txtWstperWkr.Text.Trim().Length == 0 ? 0 : Convert.ToSingle(txtWstperWkr.Text));
                mcval = McPergm * MetalWt;
                Wst = MetalWt * Wstperc / 100;
                r.Cells["McVal"].Value = mcval.ToString();
                r.Cells["Wst"].Value = Wst.ToString(); 
            }
        }


        public override bool ValidateControls()
        {




            if (base.ValidateControls())
            {
                DateTime fromdate = Convert.ToDateTime(Dtp_Issuedt.Text);
                DateTime todate = Convert.ToDateTime(dtp_JobRetDate.Text);
                if (dgv.Rows.Count <= 0 && dgvMetal.Rows.Count <= 0 && dgvStk.Rows.Count <= 0)
                {
                    Gramboo.General.ShowMessage("Add Details to Grid");
                    return false;
                }
                if (todate < fromdate)
                {
                    dtp_JobRetDate.ShowMessage("Return date must be  greater than Or equal to Issue Date");
                    return false;

                }
                

                foreach (DataGridViewRow r in dgv.Rows)
                {
                    if ((DateTime)r.Cells["TransDate"].Value < fromdate)
                    {
                        Gramboo.General.ShowMessage("StoneIssueDate Must be Greater Than Or Equal To  IssueDate");
                        return false;
                    }
                    if ((DateTime)r.Cells["TransDate"].Value > todate)
                    {
                        Gramboo.General.ShowMessage("StoneIssueDate  Must be Less Than Or Equal To Return Date");
                        return false;
                    }
                }

                foreach (DataGridViewRow r1 in dgvMetal.Rows)
                {
                    if ((DateTime)r1.Cells["TransDate"].Value < fromdate)
                    {
                        Gramboo.General.ShowMessage("Metal Receipt Date Must be Greater Than Or Equal To  IssueDate");
                        return false;
                    }

                    if ((DateTime)r1.Cells["TransDate"].Value > todate)
                    {
                        Gramboo.General.ShowMessage("Metal Receipt Date   Must be Less Than Or Equal To Return Date");
                        return false;
                    }
                }



                return true;
            }
            else
            {
                return false;
            }
        }



        private void chkBox()
        {
            if (ChkLost.Checked == true)
            {
                chk_Damaged.Enabled = false;
                chkISReceipt.Enabled = false;
                ChkLost.Enabled = true;

            }
            else
            {
                chk_Damaged.Enabled = true;
                chkISReceipt.Enabled = true;
 
            }




            if ((chkISReceipt.Checked == true)||(chk_Damaged.Checked == true))
            {
                chkISReceipt.Enabled = true;
                chk_Damaged.Enabled = true;
                ChkLost.Enabled = false;
            }
            else
            {
                ChkLost.Enabled = true;
 
            }
            //if (chk_Damaged.Checked == true)
            //{
            //    chk_Damaged.Enabled = true;
            //    chkISReceipt.Enabled = true;
            //    ChkLost.Enabled = false;
            //}
            //else
            //{
            //    ChkLost.Enabled = true;
 
            //}
        }
               
        

        public bool validateTransDate()


        {
            DateTime trandate = Convert.ToDateTime(dtp_TransDate.Text);
            DateTime fromdate = Convert.ToDateTime(Dtp_Issuedt.Text);
            DateTime todate = Convert.ToDateTime(dtp_JobRetDate.Text);
            if (trandate < fromdate)
            {
                dtp_TransDate.ShowMessage("StoneIssueDate Must be Greater Than Or Equal To IssueDate ");
                return false;
 
            }
            if (trandate > todate)
            {
                dtp_TransDate.ShowMessage("StoneIssueDate Must be Less Than Or Equal To Return Date ");
                return false;
            }
            return true;
 

        }

        public bool TransMetalDateValidation()
        {
            DateTime trandate = Convert.ToDateTime(dtpMetaltransdate.Text);
            DateTime fromdate = Convert.ToDateTime(Dtp_Issuedt.Text);
            DateTime todate = Convert.ToDateTime(dtp_JobRetDate.Text);
            if (trandate < fromdate)
            {
                dtpMetaltransdate.ShowMessage("Date Must be Greater Than Or Equal To IssueDate ");
                return false;

            }
            if (trandate > todate)
            {
                dtpMetaltransdate.ShowMessage(" Date Must be Less Than Or Equal To Return Date ");
                return false;
            }
            return true;
 
 
        }

        public bool TransStockDateValidation()
        {
            DateTime trandate = Convert.ToDateTime(dtpStk.Text);
            DateTime fromdate = Convert.ToDateTime(Dtp_Issuedt.Text);
            DateTime todate = Convert.ToDateTime(dtp_JobRetDate.Text);
            if (trandate < fromdate)
            {
                dtpStk.ShowMessage("Date Must be Greater Than Or Equal To IssueDate ");
                return false;

            }
            if (trandate > todate)
            {
                dtpStk.ShowMessage("Date Must be Less Than Or Equal To Return Date ");
                return false;
            }
            return true;


        }

        

        private void rbtOwn_CheckedChanged(object sender, EventArgs e)
        {
            if (txtcompId.Text == "")
                return;

            if (rbtOwn.Checked == true)
            {
                Cmb_WorkerName.Enabled = true;
                Gramboo.General.Setupcombo(Cmb_WorkShopName, "PROD.WorkShopMaster", "WorkshopName", "WorkshopId", "IsActive='True'  AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
            }
            else
            {
                Cmb_WorkerName.Enabled = false;
                Gramboo.General.Setupcombo(Cmb_WorkShopName, "PROD.VSubContractors", "[Supplier Name]", "SuppId", "IsActive='True' AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
            }
        }

        private void CalculateMcAndWst(object sender, EventArgs e)
        {
            if (txtcompId.Text.Trim().Length == 0 || Cmb_WorkerName.SelectedValue == null)
                return;

            JMS.Classes.Common.GetMCandWastage(DBConn, txtsetMCpergm, txtSetMc, txtSetWstPerc, txtSetWst,
                dtp_JobRetDate.Value.Date, 0, "S", 0, Convert.ToInt32(Cmb_WorkerName.SelectedValue),
                Convert.ToInt16(txtcompId.Text), Convert.ToInt16(txtBranchID.Text),
                0,
               0,
              0,
                Convert.ToSingle((txtSetDiaWt.Text.Trim().Length == 0 ? "0" : txtSetDiaWt.Text)) + Convert.ToSingle((txtSetStoneWt.Text.Trim().Length == 0 ? "0" : txtSetStoneWt.Text)),
                (int)Convert.ToSingle((txtSetDiaNos.Text.Trim().Length == 0 ? "0" : txtSetDiaNos.Text)) + (int)Convert.ToSingle((txtSetStoneNos.Text.Trim().Length == 0 ? "0" : txtSetStoneNos.Text)));
        }
        private void CalculateMcAndWstWorker(object sender, EventArgs e)
        {
            if (txtcompId.Text.Trim().Length == 0 || Cmb_WorkerName.SelectedValue == null)
                return;

            JMS.Classes.Common.GetMCandWastage(DBConn, txtMcpergmWkr, txtMcWkr, txtWstperWkr, txtWstWkr,
                dtp_JobRetDate.Value.Date, 0, "G", 0, Convert.ToInt32(Cmb_WorkerName.SelectedValue),
                Convert.ToInt16(txtcompId.Text), Convert.ToInt16(txtBranchID.Text),
                0,
               0,
              0,
                0,
                0);
        }


        private void SplitWeight(object sender, EventArgs e)
        {
            int diaNos, StoneNos;
            float DiaWt, StoneWt;

            diaNos = StoneNos = 0;
            DiaWt = StoneWt = 0f;
            foreach (DataGridViewRow r in dgv.Rows)
            {
                if (DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM ITM.VDiamonds WHERE ItemId=" + Convert.ToInt32(r.Cells["ItemId"].Value.ToString()))).Tables[0].Rows.Count != 0)
                {
                    if (Convert.ToBoolean(r.Cells["IsReceipt"].Value.ToString()) == true)
                    {
                        diaNos -= Convert.ToInt32(r.Cells["Nos"].Value.ToString());
                        DiaWt -= Convert.ToSingle(r.Cells["Wt"].Value.ToString());
                    }

                    else
                    {
                        diaNos += Convert.ToInt32(r.Cells["Nos"].Value.ToString());
                        DiaWt += Convert.ToSingle(r.Cells["Wt"].Value.ToString());

                    }
                }
                else //if (Convert.ToInt32(r.Cells["SizeRangeId"].Value.ToString()) != 0)
                {
                    if (Convert.ToBoolean(r.Cells["IsReceipt"].Value.ToString()) == true)
                    {
                        StoneNos -= Convert.ToInt32(r.Cells["Nos"].Value.ToString());
                        StoneWt -= Convert.ToSingle(r.Cells["Wt"].Value.ToString());

                    }
                    else
                    {
                        StoneNos += Convert.ToInt32(r.Cells["Nos"].Value.ToString());
                        StoneWt += Convert.ToSingle(r.Cells["Wt"].Value.ToString());

                    }
                }
            }
            
            dgv.SummaryRow.SummaryCells["Nos"].Text = (StoneNos + diaNos).ToString("F3");
            dgv.SummaryRow.SummaryCells["Wt"].Text = (StoneWt + DiaWt).ToString("F3");

            txtSetDiaNos.Text = diaNos.ToString("F3");
            txtSetDiaWt.Text = DiaWt.ToString("F3");
            txtSetStoneNos.Text = StoneNos.ToString("F3");
            txtSetStoneWt.Text = StoneWt.ToString("F3");

        }

        private void Cmb_JobNo_SelectedValueChanged(object sender, EventArgs e)
        {
            //txtReturnWt.Enabled = true;
            //txtIssueWt.Enabled = true;
            if (Cmb_JobNo.SelectedValue != null)
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select * FROM PROD.GetJobNo(" + Cmb_JobNo.SelectedValue.ToString() + "," + Convert.ToInt16(txtcompId.Text) + "," + Convert.ToInt16(txtBranchID.Text) + ") ")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        Cmb_Item_Master.SelectedValue = dt.Rows[0]["ItemID"];
                        txtIssueWt.Text = dt.Rows[0]["CurrentWt"].ToString();
                        txtReturnWt.AcceptBlankValue = false;
                        txtIssueWt.AcceptBlankValue = false;
                        Cmb_Item_Master.AcceptBlankValue = false;
                    }

                }
            }
        }
        private void txtRetWt_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Dtp_Issuedt_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void grbTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Cmb_WorkerName_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateMcAndWst(sender, e);
            CalculateMcAndWstWorker(sender, e);
            if (Cmb_WorkerName.SelectedValue != null)
            {
                updateMcWstmetal();
            }
        }

        private void lnkNew_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Gramboo.General.Setupcombo(cmbProdCode, "ITM.VOrnaments", "[Item Name]", "ItemId", "IsActive='True'");
            pnlNewJobNo.Size = new System.Drawing.Size(370, 80);

            pnlNewJobNo.Location = new Point(btn_CreateJobNo.Location.X + btn_CreateJobNo.Size.Width + 30, btn_CreateJobNo.Location.Y - 40);
            pnlNewJobNo.Visible = true;
            pnlNewJobNo.BringToFront();
        }

        private void btn_CreateJobNo_Click(object sender, EventArgs e)
        {
            if (cmbProdCode.SelectedValue != null)
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PROD.GenerateJobNoFromSetting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetItemId", cmbProdCode.SelectedValue);
                cmd.Parameters.AddWithValue("@company_ID", txtcompId.Text);
                cmd.Parameters.AddWithValue("@branch_id", txtBranchID.Text);
                cmd.Parameters.AddWithValue("@CounterId", txtCounterId.Text);

                SqlParameter c = new SqlParameter("@JobNoId", SqlDbType.BigInt);
                c.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(c);
                //cmd.Parameters.AddWithValue("@CounterId", txtCounterId.Text);

                //Gramboo.DataController.CommandCollection cmdcol = new Gramboo.DataController.CommandCollection();
                //cmdcol.Add(cmd);
                DBConn.GetData(cmd, "GenerateJobNo");
                Gramboo.General.Setupcombo(Cmb_JobNo, "PROD.JobNoGeneration", "JobNo", "JobNoId", "IsActive='True'");

                Cmb_JobNo.SelectedValue = Convert.ToInt64(cmd.Parameters["@JobNoId"].Value);
                pnlNewJobNo.Visible = false;
            }
            else
            {
                cmbProdCode.ShowMessage("Select Target Item");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlNewJobNo.Visible = false;
        }

        private void Cmb_JobNo_Leave(object sender, EventArgs e)
        {
           
        }

        private void chkISReceipt_CheckedChanged(object sender, EventArgs e)
        {
            chkBox();
        }

        private void chk_Damaged_CheckedChanged(object sender, EventArgs e)
        {
            chkBox();
        }

        private void ChkLost_CheckedChanged(object sender, EventArgs e)
        {
            chkBox();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnAddStk_Click(object sender, EventArgs e)
        {
            
            //chkStkRec.Checked = true;
              
            if (cmbItemStk .SelectedValue != null)
            {
                txtItemIdStk.Text = cmbItemStk.SelectedValue.ToString();
            }

           dtpStk.Focus();
            if (TransStockDateValidation())
            {
                dgvStk.Save();

            }
        }

        private void txtMetalWt_TextChanged(object sender, EventArgs e)
        {

        }

    }

}

