using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gramboo.Database;
using System.Data.SqlClient;
using System.IO;
namespace SAFA.Forms.STK
{
    public partial class FrmPurchaseOrnamentsEntrySafa : Gramboo.Controls.GrbForm
    {
        string barcodecount = "";
        bool printflag = false;
        int row = -1;
        bool flag = false, editflag = false;
        public FrmPurchaseOrnamentsEntrySafa POSE; 
        private static FrmPurchaseOrnamentsEntrySafa instance;
        private static string set, diarate, VAmode, Vaamt, MinVa, VAPerc, PurStRate, PurDiaRate, marginePercentage, PercentageGrmRate;
        public string MetalRateperGm, ItemId;
        public static FrmPurchaseOrnamentsEntrySafa Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FrmPurchaseOrnamentsEntrySafa();
                }
                else if (instance.IsDisposed)
                {
                    instance = new FrmPurchaseOrnamentsEntrySafa();
                }
                return instance;
            }
        }
        public FrmPurchaseOrnamentsEntrySafa()
        {
            flag = false;
            InitializeComponent();
            txt_netwt.Text = (string.IsNullOrEmpty(txt_netwt.Text.Trim()) ? "0.00" : txt_netwt.Text);
            txt_StWt.Text = (string.IsNullOrEmpty(txt_StWt.Text.Trim()) ? "0.00" : txt_StWt.Text);
            txt_StCtWt.Text = (string.IsNullOrEmpty(txt_StCtWt.Text.Trim()) ? "0.00" : txt_StCtWt.Text);
            txt_stRate.Text = (string.IsNullOrEmpty(txt_stRate.Text.Trim()) ? "0.00" : txt_stRate.Text);
            txt_stamt.Text = (string.IsNullOrEmpty(txt_stamt.Text.Trim()) ? "0.00" : txt_stamt.Text);
            txt_diaNo.Text = (string.IsNullOrEmpty(txt_diaNo.Text.Trim()) ? "0.00" : txt_diaNo.Text);
            txt_DiaWt.Text = (string.IsNullOrEmpty(txt_DiaWt.Text.Trim()) ? "0.00" : txt_DiaWt.Text);
            txt_DiaRate.Text = (string.IsNullOrEmpty(txt_DiaRate.Text.Trim()) ? "0.00" : txt_DiaRate.Text);
            txt_DiaAmt.Text = (string.IsNullOrEmpty(txt_DiaAmt.Text.Trim()) ? "0.00" : txt_DiaAmt.Text);
            txt_PerSalesVA.Text = (string.IsNullOrEmpty(txt_PerSalesVA.Text.Trim()) ? "0.00" : txt_PerSalesVA.Text);
            txt_PerPurVA.Text = (string.IsNullOrEmpty(txt_PerPurVA.Text.Trim()) ? "0.00" : txt_PerPurVA.Text);
            txtPurStRate.Text = (string.IsNullOrEmpty(txtPurStRate.Text.Trim()) ? "0.00" : txtPurStRate.Text);
            txtPurDtRate.Text = (string.IsNullOrEmpty(txtPurDtRate.Text.Trim()) ? "0.00" : txtPurDtRate.Text);
            txt_percentage.Text = (string.IsNullOrEmpty(txt_percentage.Text.Trim()) ? "0.00" : txt_percentage.Text);
            txt_MRP.Text = (string.IsNullOrEmpty(txt_MRP.Text.Trim()) ? "0.00" : txt_MRP.Text);

        }
        private void ItemDetails()
        {
            if (cmb_ItemName.Text != "")
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                            ("SELECT VAPerc,VAamount,MinimumVA , VAMode FROM ITM.ItemMaster WHERE ItemId =" + (cmb_ItemName.SelectedValue == null ? "0" : cmb_ItemName.SelectedValue.ToString()) + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        txt_PerSalesVA.Text = (dt.Rows[0]["VAPerc"] == DBNull.Value ? "" : dt.Rows[0]["VAPerc"].ToString());
                        txtVAamt.Text = (dt.Rows[0]["VAamount"] == DBNull.Value ? "" : dt.Rows[0]["VAamount"].ToString());
                        txtminva.Text = (dt.Rows[0]["MinimumVA"] == DBNull.Value ? "" : dt.Rows[0]["MinimumVA"].ToString());
                        cmbva.Text = (dt.Rows[0]["VAMode"] == DBNull.Value ? "" : dt.Rows[0]["VAMode"].ToString());
                    }
                }
            }
        }
        public override bool GenerateID(Gramboo.Database.Table table_name)
        {
            try
            {
                table_name.IdTextBox.Text = SAFA.Classes.Common.GetNextID(table_name, table_name.IdTextBox.DataField, DBConn, Convert.ToInt32(base.txtcompId.Text), Convert.ToInt32(base.txtBranchID.Text)).ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public override bool FillData(Dictionary<string, object> PrimaryValues)
        {
            flag = true;
            if (base.FillData(PrimaryValues))
            {
                editflag = true;
                flag = false;
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                            ("SELECT PartyId  FROM STK.VPurchaseList WHERE EntryId =" + (txt_entryId.Text == null ? "0" : txt_entryId.Text.ToString()) + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmbpartyname.SelectedValue = (dt.Rows[0]["PartyId"] == DBNull.Value ? "0" : dt.Rows[0]["PartyId"].ToString());
                    }
                }
                pendinggrid();
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
            Table t = new Table(SAFA.Classes.Common.DbName, "STK", "PurchaseOrnamentsEntryMaster");
            t.PrimaryKeys.Add("EntryId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = txt_entryId;
            Table t1 = new Table(SAFA.Classes.Common.DbName, "STK", "PurchaseOrnamentsDetails", true);
            t1.PrimaryKeys.Add("TransId");
            t1.FillView = new Table(SAFA.Classes.Common.DbName, "STK", "VPurchaseOrnamentsDetails");
            t1.DatagridView = grb_list;
            t1.IsDatagridView = true;
            t1.IdTextBox = txt_TransId;
            t.ChildTables.Add(t1);
            this.TableName = t;
            return true;
        }
        public override void Init()
        {
            base.Init();
            ValidateAdmin();
            this.ListForm = SAFA.Forms.STK.PurchaseOrnamentsList.Instance;
            editflag = false;
            grb_list.DataFields = new List<string> {"prodCode","HuId", "TransId","ReceiptId","JobOrderId","JobNo","ItemID","DesignCode","ItemName","PurityId","PurityName","MessureID","Measurement","ModelId","ModelName","Value",
            "Nos","Gwt","StWt","StCtWt","Netwt","Tagwt","StRate","StAmt","DiaNo","Diawt","DiaRate","DiaAmt","MC","Wastage","VAmode","[VA mode]","VAamount","Touch","MinimumVAamount","PerSalesVA","PerPurchaseVA","RatePerGm","prodcodeid","Percentage","MRP","isnull(IsMRP,0) as IsMRP","PurDiaRate","PurStRate","Percentage","PercentageGRM","DesignId"};
            grb_list.HiddenDataFields = new List<string> { "TransId", "VAMode", "ReceiptId", "ModelId", "JobOrderId", "ItemID", "PurityId", "prodcodeid", "MessureID", "DesignId" };
            grb_list.SummaryColumns = new string[] { "Nos", "Gwt", "StWt", "StCtWt", "NetWt", "Tagwt", "StAmt", "Diawt", "DiaAmt", "Netwt", "DiaNo", "MC", "Wastage", "PurDiaRate", "PurStRate", "Percentage", "MRP", "RatePerGm" };
            if (txtBranchID.Text != "")
            {
                if (!SAFA.Classes.Common.JobNoVlidate(DBConn, Convert.ToInt16(txtBranchID.Text)))
                {
                    grb_list.HiddenDataFields.AddRange(new List<string> { "JobOrderId", "JobNo" });
                }
            }
            grb_list.Fill(new Table(SAFA.Classes.Common.DbName, "STK", "VPurchaseOrnamentsDetails", true), "1=2");
            cmb_trans.SelectedIndex = 0;
            txtBarcodeCount.Text = (barcodecount.Length > 0 ? barcodecount : "2");
            AdjustColumnWidths();
            if (!EditMode)
            {
                mrp();
            }            //if (this.TableName != null && !EditMode)
            //    GenerateID(this.TableName);
        }
        private void ValidateAdmin()
        {
            if (txtcompId.Text != null)
            {
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
              ("SELECT user_category_id FROM SYST.Username WHERE user_id='" + txtCrUserId.Text + "'")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        if ((long)dt.Rows[0]["user_category_id"] != 11010001)
                        {
                            dtp_dt.Enabled = false;
                        }
                    }
                }
            }
        }
        public override void RefreshData()
        {
            base.RefreshData();
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            cmb_VoucherTypeId.SelectedValue = 46;
            if (!IsEditMode)
                txt_VchNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtp_dt.Value,
            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            Gramboo.General.Setupcombo(cmbbranch, "SYST.BranchMaster", "BranchName", "BranchId", "[IsActive]='True'");
            Gramboo.General.Setupcombo(CmbJobNo, "STK .GenJobOrderIdMaster", "JobNo", "JobOrderID", "  Company_id=" + txtcompId.Text);
            Gramboo.General.Setupcombo(CmbMessure, "ITM.MeasurmentMaster", "Mesurment", " MeasurmentId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmbModel, "ITM.ModelMaster", "ModelName", " ModelId", "IsActive='True'");
            Gramboo.General.Setupcombo(CmbLoadTo, "STK.VBcodeLoadTo", "LoadType", "Id", "[IsActive]='True'");
            Gramboo.General.Setupcombo(cmb_ItemName, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True'");
            Gramboo.General.Setupcombo(grbCmb_Purity, "ITM.PurityMaster", "PurityName", "PurityId", "IsActive='True'");
            cmb_selectbrch.SelectedValueChanged -= cmb_selectbrch_SelectedValueChanged;
            Gramboo.General.Setupcombo(cmb_selectbrch, "SYST.BranchMaster", "BranchName", "BranchId", "[IsActive]='True'");
            cmb_selectbrch.SelectedValueChanged += cmb_selectbrch_SelectedValueChanged;
            Gramboo.General.Setupcombo(cmbDesigncode, "DES.VFetchDataDesignCodes", "DesignCode", "DesignCodeId", "IsActive='True' and Company_id=" + txtcompId.Text);
            if (txtBranchID.Text != "")
            {
                cmbbranch.SelectedValue = txtBranchID.Text;
            }
            CmbLoadTo.Visible = false;
            label45.Visible = false;
            if (MetalRateperGm != "")
            {
                TxtRatePerGm.Text = MetalRateperGm;
            }
            if (ItemId != null)
            {
                cmb_ItemName.SelectedValue = ItemId;
            }


        }
        private void AdjustColumnWidths()
        {
            grb_list.RowHeadersVisible = false;
            grb_list.Columns["ItemName"].Width = cmb_ItemName.Width + 10;
            grb_list.Columns["PurityName"].Width = grbCmb_Purity.Width + 5;
            grb_list.Columns["DesignCode"].Width = cmbDesigncode.Width + 5;
            grb_list.Columns["Nos"].Width = txt_Nos.Width + 5;
            grb_list.Columns["NetWt"].Width = txt_netwt.Width + 5;
            grb_list.Columns["Gwt"].Width = txt_GWt.Width + 5;
            grb_list.Columns["StWt"].Width = txt_StWt.Width + 5;
            grb_list.Columns["StCtWt"].Width = txt_StCtWt.Width + 5;
            grb_list.Columns["StRate"].Width = txt_stRate.Width + 5;
            grb_list.Columns["MC"].Width = txtmc.Width + 5;
            grb_list.Columns["Wastage"].Width = txtwst.Width + 5;
            grb_list.Columns["StAmt"].Width = txt_stamt.Width + 5;
            grb_list.Columns["DiaNo"].Width = txt_diaNo.Width + 5;
            grb_list.Columns["Diawt"].Width = txt_DiaWt.Width + 5;
            grb_list.Columns["DiaRate"].Width = txt_DiaRate.Width + 5;
            grb_list.Columns["DiaAmt"].Width = txt_DiaAmt.Width + 5;
            grb_list.Columns["PerSalesVA"].Width = txt_PerSalesVA.Width + 5;
            grb_list.Columns["PerPurchaseVA"].Width = txt_PerPurVA.Width + 5;
            grb_list.Columns["MRP"].Width = txt_MRP.Width + 5;
            grb_list.Columns["PurDiaRate"].Width = txtPurDtRate.Width + 5;
            grb_list.Columns["PurStRate"].Width = txtPurStRate.Width + 5;
            grb_list.Columns["Percentage"].Width = txt_percentage.Width + 5;
            grb_list.Columns["PercentageGRM"].Width = txt_grm.Width + 5;
        }
        private void grbCmb_Purity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grbCmb_Purity.SelectedValue != null)
            {
                txt_PurId.Text = grbCmb_Purity.SelectedValue.ToString();
                //using(DataTable dt = DBConn.GetData(new SqlCommand("SELECT PurityValue FROM ITM.PurityMaster WHERE PurityId=" + grbCmb_Purity.SelectedValue)).Tables[0])
                //  if (dt.Rows.Count> 0)
                //  {
                //      txttouch.Text = (dt.Rows[0]["PurityValue"] == DBNull.Value ? "" : dt.Rows[0]["PurityValue"].ToString());
                //  }
            }
            if (grbCmb_Purity.SelectedValue == null)
            {
                txt_PurId.Text = "0";
            }
        }
        private void cmbpartytype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpartytype.SelectedIndex == 0)
            {
                Gramboo.General.Setupcombo(cmbpartyname, "PUR.SupplierMaster", "SuppName", "SuppId", "IsActive='True'AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + " and SuppTypeId=1");
                cmbpartyname.DropDownWidth = 250;
            }
            if (cmbpartytype.SelectedIndex == 1)
            {
                Gramboo.General.Setupcombo(cmbpartyname, "PUR.SupplierMaster", "SuppName", "SuppId", "IsActive='True'AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + "and SuppTypeId=6");
                cmbpartyname.DropDownWidth = 250;
            }
            if (cmbpartytype.SelectedIndex == 2)
            {
                Gramboo.General.Setupcombo(cmbpartyname, "SYST.BranchMaster", "BranchName", "BranchId", "IsActive='True'AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + "");
            }
        }
        private void cmbpartyname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpartyname.SelectedIndex == 0)
            {
                pendinggrid();
            }
            else
            {
                grb_pending.DataSource = null;
            }
        }
        private void pendinggrid()
        {
            string qc;
            if (txtBranchID.Text == "")
                return;
            if (cmbpartyname.SelectedValue != null && cmbbranch.SelectedValue != null && cmb_trans.SelectedIndex != null)
            {
                grb_pending.AutoGenerateColumns = true;
                grb_pending.ShowSerialNo = true;
                grb_pending.HiddenDataFields = new List<string> { "TransId" };
                grb_pending.SummaryColumns = new string[] { "Qty", "Wt", "stwt", "stCtwt", "diawt" };
                if (!SAFA.Classes.Common.JobNoVlidate(DBConn, Convert.ToInt16(txtBranchID.Text)))
                {
                    grb_pending.HiddenDataFields.AddRange(new List<string> { "JobOrderId", "JobNo" });
                }
                if (cmb_trans.SelectedIndex == 0)
                {
                    grb_pending.DataSource = this.DBConn.GetData(new SqlCommand("Select TransId,VchNo,JobNo,ItemName,Purity,Qty,Wt,stwt,StCtWt,diawt,VchDate,PurStRate,PurDiaRate,PercentageGrmRate  FROM PUR.PurchasePending(" + cmbpartyname.SelectedValue.ToString() + "," + cmb_trans.SelectedIndex.ToString() + "," + cmbbranch.SelectedValue.ToString() + "," + txtcompId.Text + "," + txtBranchID.Text + "," + (txt_entryId.Text == "" ? "0" : txt_entryId.Text) + ",'PO') t2 ")).Tables[0];
                }
                else
                {
                    grb_pending.DataSource = this.DBConn.GetData(new SqlCommand("Select TransId,VchNo,JobNo,ItemName,Purity,Qty,Wt,stwt,StCtWt,diawt,VchDate,PurStRate,PurDiaRate,PercentageGrmRate  FROM PUR.PurchaseEntryPending(" + cmbpartyname.SelectedValue.ToString() + "," + cmbbranch.SelectedValue.ToString() + "," + txtcompId.Text + "," + txtBranchID.Text + "," + (txt_entryId.Text == "" ? "0" : txt_entryId.Text) + ",'PO') t2 ")).Tables[0];
                }

                grb_pending.Columns["JobNo"].Width = 100;
                grb_pending.Columns["ItemName"].Width = 100;
                grb_pending.Columns["Qty"].Width = 40;
                grb_pending.Columns["Wt"].Width = 80;
                grb_pending.Columns["stwt"].Width = 80;
                grb_pending.Columns["stCtwt"].Width = 80;
                grb_pending.Columns["diawt"].Width = 80;
                grb_pending.Columns["VchNo"].Width = 50;
                grb_pending.Columns["VchDate"].Width = 100;
                grb_pending.Columns["PurDiaRate"].Width = 100;
                grb_pending.Columns["PurStRate"].Width = 100;
                grb_pending.Columns["PercentageGrmRate"].Width = 100;
            }
        }
        private void getDataSMCode()
        {
            if (txtSMCode.Text != "")
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select DISTINCT EmpName FROM EMP.EmployeeMaster  WHERE  EmpCode = '" + (txtSMCode.Text + "' and Company_id=" + txtcompId.Text + " and Branch_id= " + txtBranchID.Text + ""))).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<string> lst = new List<string>();
                        foreach (DataRow row in dt.Rows)
                        {
                            lst.Add(row["EmpName"].ToString());
                        }
                        listBox1.DataSource = lst;
                        listBox1.Visible = true;
                        listBox1.BringToFront();
                    }
                    else
                    {
                        listBox1.Visible = false;
                    }
                }
            }
            else
            {
                listBox1.Visible = false;
            }
        }
        private void pendingissue_Btn_Click(object sender, EventArgs e)
        {
            if (CmbMessure.Text == "" || grbTextBox1.Text == "0")
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("select * from ITM.ItemMaster where IsMeasurement=1 and ItemName='" + cmb_ItemName.Text + "' AND  Branch_id=" + txtBranchID.Text + "  ")).Tables[0])
                {
                    if (dt.Rows.Count > 0)

                    {
                        CmbMessure.ShowMessage("select the value!");
                        grbTextBox1.ShowMessage("Enter the value!");
                        return;
                    }
                }
            }
            bool chkct;
            bool chkMRP;
            chkct = chkCtWt.Checked;
            chkMRP = chISMRP.Checked;
            //if (chISMRP.Checked == true)
            //{
            //    IsMRP = true;
            //}
            //else
            //{
            //    IsMRP = false;
            //}
            //float gwt, tagwt;
            //gwt = Convert.ToSingle(txt_GWt.Text == "" ? "0" : txt_GWt.Text);
            //tagwt = Convert.ToSingle(txttagWt.Text == "" ? "0" : txttagWt.Text);
            //if (gwt >= tagwt)
            //{
            //    txttagWt.ShowMessage("Tag wt Cannot be less than Gwt");
            //    return;
            //}
            if (txt_GWt.Text == "0")
            {
                txt_GWt.ShowMessage("Wt Should not be 0");
                return;
            }
            if (txt_MRP.Text == "0" && TxtRatePerGm.Text == "0")
            {
                if (txt_PerSalesVA.Text == "0" && cmbva.SelectedIndex == 0 && Convert.ToSingle(txt_MRP.Text) == 0)
                {
                    txt_PerSalesVA.ShowMessage("VA% Should not be 0");
                    return;
                }
            }
            //if  (cmbva.SelectedIndex =-1)
            //{
            //cmbva.SelectedIndex = 0;
            txtVAmodeId.Text = cmbva.SelectedIndex.ToString();
            //}
            if (grb_pending.CurrentRow != null)
            {
                txtrcptid.Text = grb_pending.CurrentRow.Cells["TransId"].Value.ToString();
            }
            string item = cmb_ItemName.Text;
            string purity = grbCmb_Purity.Text;
            string jobnoo = CmbJobNo.Text;
            string unit = CmbMessure.Text;
            string imageid = cmbDesigncode.Text;
            if (txtrcptid.Text != "")
            {
                set = (txttouch.Text == "" ? "0" : txttouch.Text.ToString());
                diarate = (txt_DiaRate.Text == "" ? "0" : txt_DiaRate.Text.ToString());
                VAmode = (cmbva.Text == "" ? "0" : cmbva.Text.ToString());
                Vaamt = (txtVA.Text == "" ? "0" : txtVA.Text.ToString());
                MinVa = (txtminva.Text == "" ? "0" : txtminva.Text.ToString());
                VAPerc = (txt_PerSalesVA.Text == "" ? "0" : txt_PerSalesVA.Text.ToString());
                PurStRate = (txtPurStRate.Text == "" ? "0" : txtPurStRate.Text.ToString());
                PurDiaRate = (txtPurDtRate.Text == "" ? "0" : txtPurDtRate.Text.ToString());
                marginePercentage = (txt_percentage.Text == "" ? "0" : txt_percentage.Text.ToString());
                PercentageGrmRate = (txt_grm.Text == "" ? "0" : txt_grm.Text.ToString());
                MetalRateperGm = (TxtRatePerGm.Text == "" ? "0" : TxtRatePerGm.Text.ToString());
                ItemId = (cmb_ItemName.SelectedValue == null ? "0" : cmb_ItemName.SelectedValue.ToString());
                if (grb_list.Save())
                {
                    int c = Convert.ToInt16(txtBarcodeCount.Text);
                    if (c > 0)
                    {
                        if (grb_list.Rows.Count % c == 0)
                        {
                            printflag = true;
                            if (Save(false))
                            {
                                if (Gramboo.General.ShowMessage("Do you want to print Barcodes ?", "Barcode", MessageBoxIcon.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    long[] barcodes = new long[c];
                                    for (int i = 0; i < c; i++)
                                    {
                                        DataGridViewRow r = grb_list.Rows[grb_list.Rows.Count - (i + 1)];
                                        barcodes[i] = Convert.ToInt64(r.Cells["ProdcodeID"].Value.ToString());
                                    }
                                    
                                }
                            }
                            printflag = false;
                        }
                    }
                }
                cmb_ItemName.Text = item;
                CmbJobNo.Text = jobnoo;
                CmbMessure.Text = unit;
                cmbDesigncode.Text = imageid;
                grbCmb_Purity.Text = purity;
                txt_Nos.Text = "1";
                txttouch.Text = set.ToString();
                txt_DiaRate.Text = diarate.ToString();
                cmbva.Text = VAmode.ToString();
                txtPurStRate.Text = PurStRate.ToString();
                txtPurDtRate.Text = PurDiaRate.ToString();
                txt_percentage.Text = marginePercentage.ToString();
                txt_grm.Text = PercentageGrmRate.ToString();
                TxtRatePerGm.Text = MetalRateperGm.ToString();
                cmb_ItemName.SelectedValue = ItemId.ToString();
                // txtVA.Text = Vaamt.ToString();
                chkCtWt.Checked = chkct;
                chISMRP.Checked = chkMRP;
                txtminva.Text = MinVa.ToString();
                txt_PerSalesVA.Text = VAPerc;
                txt_GWt.Focus();
            }
        }
        private void calcVAAmount()
        {
            double netwt = 0, vapergm = 0, vaamt = 0, minva = 0;
            netwt = Convert.ToDouble(txt_netwt.Text);
            vapergm = Convert.ToDouble(txt_PerSalesVA.Text);
            vaamt = netwt * vapergm;
            minva = Convert.ToDouble(txtminva.Text);
            if (cmbva.SelectedIndex == 1)
            {
                txtVAamt.Text = (vaamt > minva ? vaamt : minva).ToString("F2");
            }
            else
            {
                txtVAamt.Text = "0";
            }
        }
        private void cmb_ItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemDetails();
            if (cmb_ItemName.SelectedValue != null)
            {
                txt_ItemId.Text = cmb_ItemName.SelectedValue.ToString();
            }
        }
        private void grb_pending_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int rowIndex = e.RowIndex;
            //DataGridViewRow row = grb_pending.Rows[rowIndex];
            //txt_Nos.Text = row.Cells[4].Value.ToString();
            //txt_GWt.Text = row.Cells[5].Value.ToString();
            //txtrcptid.Text = row.Cells[1].Value.ToString();
        }
        private void EqualsInQty()
        {
            if (txtrcptid.Text != "")
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("SELECT Distinct ReceiptId FROM STK.VPurchaseOrnamentList WHERE PartyId=" + cmbpartyname.SelectedValue)).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {

                    }
                }
            }
        }
        public override bool ValidateControls()
        {
            double PendNos = 0, PendGwt = 0, EntryNos = 0, EntryWt = 0, StoneEntryWt = 0, Diawt = 0, Stonewt = 0, DiaEntryWt = 0;
            if (!IsEditMode)
                txt_VchNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtp_dt.Value,
            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            bool flag = true;
            if (base.ValidateControls())
            {
                barcodecount = txtBarcodeCount.Text;
                //if (IsEditMode)
                //{
                //    flag = true;
                //    Gramboo.General.ShowMessage("Entry Can't be Updated");
                //    return false;
                //}
                //else
                //{
                //    if (Nos <= Convert.ToDouble(grb_pending.CurrentRow.Cells["Qty"].Value.ToString()) && Gwt <= Convert.ToDouble(grb_pending.CurrentRow.Cells["Wt"].Value.ToString()))
                //    {
                //        flag = true;
                //    }
                //    else
                //    {
                //        Gramboo.General.ShowMessage("Entered Wt Or Qty More Than Actual Wt Or Qty");
                //        flag = false;
                //    }              
            }
            List<string> l = new List<string>();
            foreach (DataGridViewRow r in grb_list.Rows)
            {
                if (!l.Contains(r.Cells["ReceiptId"].Value.ToString()))
                {
                    l.Add(r.Cells["ReceiptId"].Value.ToString());
                }
            }
            foreach (string s in l)
            {
                //float Stonewt=0, Diawt=0;
                EntryNos = Convert.ToInt64(((DataTable)grb_list.DataSource).Compute("SUM(nos)", "ReceiptId=" + s));
                EntryWt = Convert.ToInt64(((DataTable)grb_list.DataSource).Compute("SUM(Gwt)", "ReceiptId=" + s));
                StoneEntryWt = Convert.ToInt64(((DataTable)grb_list.DataSource).Compute("SUM(StWt)", "ReceiptId=" + s));
                DiaEntryWt = Convert.ToInt64(((DataTable)grb_list.DataSource).Compute("SUM(Diawt)", "ReceiptId=" + s));
                if (((DataTable)grb_pending.DataSource).Select("TransId=" + s).Length > 0)
                {
                    PendNos = Convert.ToInt64(((DataTable)grb_pending.DataSource).Compute("SUM(Qty)", "TransId=" + s));
                    PendGwt = Convert.ToInt64(((DataTable)grb_pending.DataSource).Compute("SUM(Wt)", "TransId=" + s));
                    Diawt = Convert.ToInt64(((DataTable)grb_pending.DataSource).Compute("SUM(diawt)", "TransId=" + s));
                    Stonewt = Convert.ToInt64(((DataTable)grb_pending.DataSource).Compute("SUM(stwt)", "TransId=" + s));
                }
                else
                {
                    PendNos = EntryNos;
                    PendGwt = EntryWt;
                    Stonewt = StoneEntryWt;
                    Diawt = DiaEntryWt;
                }
                PendGwt = PendGwt + 2;
                if (((EntryNos > PendNos) || (EntryWt > PendGwt) || (StoneEntryWt > Stonewt) || (DiaEntryWt > Diawt)))
                {
                    flag = false;
                    Gramboo.General.ShowMessage("Entered Wt Or Qty More Than Actual Wt Or Qty");
                }
            }
            return flag;
            // }
            //else
            //{
            //    return false;
            //}
        }
        private void txt_StWt_TextChanged(object sender, EventArgs e)
        {
            double gwt, stwt, DiaWt, netwt;
            gwt = Convert.ToDouble(txt_GWt.Text);
            stwt = Convert.ToDouble(txt_StWt.Text);
            DiaWt = Convert.ToDouble(txt_DiaWt.Text);
            netwt = gwt - stwt - (DiaWt * .2);
            txt_netwt.Text = netwt.ToString();
            //if (this.ActiveControl  == txt_StWt)
            //{
            //    txt_StCtWt.Text = (stwt * .2d).ToString("F3");
            //}
        }
        private void txt_stRate_TextChanged(object sender, EventArgs e)
        {
            double stwt, strate, stamt;
            strate = Convert.ToDouble(txt_stRate.Text);
            stwt = Convert.ToDouble(txt_StWt.Text);
            stamt = stwt * strate;
            txt_stamt.Text = stamt.ToString();
        }
        private void txt_DiaWt_TextChanged(object sender, EventArgs e)
        {
            double diawt, diarate, diaamt;
            diarate = Convert.ToDouble(txt_DiaRate.Text);
            diawt = Convert.ToDouble(txt_DiaWt.Text);
            diaamt = diawt * diarate;
            txt_DiaAmt.Text = diaamt.ToString();
            double gwt, stwt, netwt;
            gwt = Convert.ToDouble(txt_GWt.Text);
            stwt = Convert.ToDouble(txt_StWt.Text);
            netwt = gwt - stwt - (diawt * .2);
            txt_netwt.Text = netwt.ToString();
        }
        private void txt_DiaRate_TextChanged(object sender, EventArgs e)
        {
            double diawt, diarate, diaamt;
            diarate = Convert.ToDouble(txt_DiaRate.Text);
            diawt = Convert.ToDouble(txt_DiaWt.Text);
            diaamt = diawt * diarate;
            txt_DiaAmt.Text = diaamt.ToString();
        }
        public override void Print()
        {
            base.Print();
            if (Gramboo.General.ShowMessage("Do you want to print Barcodes ?", "Barcode", MessageBoxIcon.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (grb_list.SelectedRows.Count > 0)
                {
                    long[] barcodes = new long[grb_list.SelectedRows.Count];
                    int c = 0;
                    foreach (DataGridViewRow r in grb_list.SelectedRows)
                    {
                        if (r.Cells["ProdcodeID"].Value.ToString() != "")
                            barcodes[c] = Convert.ToInt64(r.Cells["ProdcodeID"].Value.ToString());
                        c++;
                    } 
                }
            }
        }
        private void txt_GWt_TextChanged(object sender, EventArgs e)
        {
            double gwt, stwt, netwt, DiaWt;
            gwt = Convert.ToDouble(txt_GWt.Text);
            stwt = Convert.ToDouble(txt_StWt.Text);
            DiaWt = Convert.ToDouble(txt_DiaWt.Text);
            netwt = gwt - stwt - (DiaWt * .2);
            txt_netwt.Text = netwt.ToString();
            MRPCalcu();
        }
        public override bool Save()
        {
            printflag = false;
            return Save(true);
        }
        public override bool Save(bool confirm)
        {
            if (base.Save(confirm))
            {
                //using (DataTable dr = DBConn.GetData(new SqlCommand("select ItemID,BoxItem,prodcodeid from STK.VPurchaseOrnamentsDetails where ItemID=" + txt_ItemId.Text + " AND prodcodeid is not null and BoxItem='True'")).Tables[0])
                //{
                //    if (dr.Rows.Count == 0)
                //    {
                SqlConnection con = new SqlConnection(DBConn.ConnectionProperties.ConnectionString);
                SqlCommand cmd = new SqlCommand("STK.GenerateProdCode", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@EntryId", txt_entryId.Text);
                cmd.Parameters.AddWithValue("@CompanyId", txtcompId.Text);
                cmd.Parameters.AddWithValue("@BranchId", txtBranchID.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                //grb_list.DataSource = this.DBConn.GetData(new SqlCommand("select prodcodeid,prodCode, TransId,ReceiptId,ItemID,ItemName,PurityId,PurityName,Nos,Gwt,StWt,Netwt,StRate,StAmt,DiaNo,Diawt,DiaRate,DiaAmt,MC,Wastage,PerSalesVA,PerPurchaseVA"
                //    + " from STK.VPurchaseOrnamentsDetails where)).Tables[0];
                grb_list.DataFields = new List<string> {"prodCode","HuId", "TransId","ReceiptId","JobOrderId","JobNo","ItemID","DesignCode","ItemName","PurityId","PurityName","MessureID","Measurement","Value",
            "Nos","Gwt","StWt","StCtWt","Netwt","Tagwt","StRate","StAmt","DiaNo","Diawt","DiaRate","DiaAmt","MC","Wastage","VAmode","[VA mode]","VAamount","Touch","MinimumVAamount","PerSalesVA","PerPurchaseVA","RatePerGm","prodcodeid","Percentage","MRP","isnull(IsMRP,0) as IsMRP","PurDiaRate","PurStRate","Percentage","PercentageGRM","DesignId"};
                grb_list.HiddenDataFields = new List<string> { "TransId", "VAMode", "ReceiptId", "JobOrderId", "ItemID", "PurityId", "prodcodeid", "MessureID", "DesignId" };
                grb_list.SummaryColumns = new string[] { "Nos", "Gwt", "StWt", "StCtWt", "NetWt", "Tagwt", "StAmt", "Diawt", "DiaAmt", "Netwt", "DiaNo", "MC", "Wastage", "PurDiaRate", "PurStRate", "Percentage", "MRP", "RatePerGm" };
                grb_list.Fill(new Table(SAFA.Classes.Common.DbName, "STK", "VPurchaseOrnamentsDetails", true), " Branch_id=" + txtBranchID.Text + " AND EntryId=" + txt_entryId.Text, " TransID");
                if (!printflag)
                {
                    if (Gramboo.General.ShowMessage("Do you want to print Barcodes ?", "Barcode", MessageBoxIcon.Question, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        using (DataTable dt = DBConn.GetData(new SqlCommand("select t1.prodcodeid from STK.ProdCodeMaster t1,STK.PurchaseOrnamentsDetails t2  where t1.Purid=t2.TransID AND t2.EntryId=" + txt_entryId.Text + " Order by  t1.prodcodeid"), "dt").Tables[0])
                        {
                            int c = 0;
                            if (dt.Rows.Count > 0)
                            {
                                long[] barcodes = new long[dt.Rows.Count];
                                foreach (DataRow r in dt.Rows)
                                {
                                    barcodes[c] = Convert.ToInt64(r[0].ToString());
                                    c++;
                                }  
                            }
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        private void txt_VchNo_TextChanged(object sender, EventArgs e)
        {
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }
        private void grb_list_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void grb_pending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (cmb_trans.SelectedIndex == 0)
            //{
            int rowIndex = e.RowIndex;
            DataGridViewRow row = grb_pending.Rows[rowIndex];
            cmbDesigncode.Text = "";
            cmbDesigncode.SelectedValue = "0";
            CmbJobNo.Text = row.Cells[3].Value.ToString();
            cmb_ItemName.Text = row.Cells[4].Value.ToString();
            grbCmb_Purity.Text = row.Cells[5].Value.ToString();
            txtPurStRate.Text = row.Cells[12].Value.ToString();
            txtPurDtRate.Text = row.Cells[13].Value.ToString();
            txt_grm.Text = row.Cells[14].Value.ToString();
            //}
            //else
            //{
            //    int rowIndex = e.RowIndex;
            //    DataGridViewRow row = grb_pending.Rows[rowIndex];
            //    //CmbJobNo.Text = row.Cells[2].Value.ToString;
            //    cmb_ItemName.Text = row.Cells[3].Value.ToString();
            //    grbCmb_Purity.Text = row.Cells[4].Value.ToString();
            //    txtPurStRate.Text = row.Cells[11].Value.ToString();
            //    txtPurDtRate.Text = row.Cells[12].Value.ToString();
            //    txt_grm.Text = row.Cells[13].Value.ToString();
            //}        
        }
        private void label20_Click(object sender, EventArgs e)
        {
        }
        private void cmb_ItemName_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_ItemName.SelectedValue != null)
            {
                using (DataTable dr = DBConn.GetData(new SqlCommand("select  ISNULL(min(prodCodeId),0) as prodCodeId  from STK.VPurchaseOrnamentsDetails where ItemID=" + cmb_ItemName.SelectedValue + " AND BoxItem=1 AND Branch_id=" + txtBranchID.Text)).Tables[0])
                {
                    if (dr.Rows.Count != 0)
                    {
                        txtprodcode.Text = dr.Rows[0][0].ToString();
                    }
                    ItemDetails();
                }
            }
        }
        private void cmbbranch_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void label16_Click(object sender, EventArgs e)
        {
        }
        private void txt_PerPurVA_TextChanged(object sender, EventArgs e)
        {
        }
        private void label15_Click(object sender, EventArgs e)
        {
        }
        private void cmbva_SelectedIndexChanged(object sender, EventArgs e)
        {
            calcVAAmount();
        }
        private void txtVA_TextChanged(object sender, EventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
        private void grb_pending_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void txt_ItemId_TextChanged(object sender, EventArgs e)
        {
        }
        private void txtcode_TextChanged(object sender, EventArgs e)
        {
            if (txtcode.Text != "")
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                   ("SELECT [Item Name],[Item Code] FROM ITM.VOrnaments WHERE [Item Code] ='" + txtcode.Text + "'")).Tables[0])
                //using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                //             ("SELECT [Item Name] FROM ITM.VOrnaments WHERE ItemId =" + dgv.CurrentRow.Cells["ItemId"].Value + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmb_ItemName.Text = (dt.Rows[0]["Item Name"] == DBNull.Value ? "" : dt.Rows[0]["Item Name"].ToString());
                    }
                    ItemDetails();
                }
            }
        }
        private void txt_PerSalesVA_TextChanged(object sender, EventArgs e)
        {
            calcVAAmount();
        }
        private void txttouch_Leave(object sender, EventArgs e)
        {
            set = (txttouch.Text == "" ? "0" : txttouch.Text.ToString());
        }
        private void cmb_ItemName_TextChanged(object sender, EventArgs e)
        {
            //ItemDetails();
        }
        private void FrmPurchaseOrnamentsEntrySafa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
            {
                pendingissue_Btn_Click(sender, e);
            }
        }
        private void grb_list_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            // row--;
        }
        private void grb_list_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //row++;
        }
        private void txtSMCode_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == txtSMCode)
            {
                getDataSMCode();
            }
        }
        public void EmpIdSalesMan()
        {
            if (txtSMCode.Text != "" && txtcompId.Text != "")
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select EmpId FROM EMP.EmployeeMaster  WHERE EmpCode = '" + txtSMCode.Text + "'and Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtSalesManId.Text = dt.Rows[0]["EmpId"].ToString();
                    }
                }
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmpIdSalesMan();
        }
        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            txtSMCode.Focus();
            EmpIdSalesMan();
        }
        private void txtSMCode_KeyDown(object sender, KeyEventArgs e)
        {
            listBox1.Visible = true;

            if (e.KeyCode == Keys.Down)
            {
                listBox1.Focus();
            }
            if (e.KeyCode == Keys.Enter && listBox1.Visible)
            {
                listBox1.Visible = false;
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select  EmpCode FROM EMP.EmployeeMaster  WHERE  EmpName = '" + listBox1.Text + "'and Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtSMCode.Text = dt.Rows[0]["EmpCode"].ToString();
                    }
                }
                EmpIdSalesMan();
            }
        }
        private void FrmPurchaseOrnamentsEntrySafa_Load(object sender, EventArgs e)
        {
        }
        private void grb_list_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void txtStCtWt_TextChanged(object sender, EventArgs e)
        {
        }
        private void chkCtWt_CheckedChanged(object sender, EventArgs e)
        {
            txt_StCtWt.Visible = chkCtWt.Checked;
            txt_StCtWt.TabStop = chkCtWt.Checked;
        }
        private void grb_list_SelectionChanged(object sender, EventArgs e)
        { 
        }
        private void grbButton1_Click(object sender, EventArgs e)
        {
             
        }
        private void txt_Nos_TextChanged(object sender, EventArgs e)
        {
        }
        private void txt_netwt_TextChanged(object sender, EventArgs e)
        {
            calcVAAmount();
        }
        private void txtminva_TextChanged(object sender, EventArgs e)
        {
            calcVAAmount();
        }
        private void cmb_trans_SelectedIndexChanged(object sender, EventArgs e)
        {
            pendinggrid();
        }
        private void txt_DiaAmt_TextChanged(object sender, EventArgs e)
        {
        }
        private void label25_Click(object sender, EventArgs e)
        {
        }
        private void txtmc_TextChanged(object sender, EventArgs e)
        {
        }
        private void chISMRP_CheckedChanged(object sender, EventArgs e)
        {
            mrp();
        }
        public void mrp()
        {
            if (txt_MRP.Text == "0" || txt_MRP.Text == null)
            {
                chISMRP.Checked = false;
            }
            else
            {
                chISMRP.Checked = true;
            }
        }
        private void txt_MRP_TextChanged(object sender, EventArgs e)
        {
            mrp();
        }
        private void txt_percentage_TextChanged(object sender, EventArgs e)
        {
            MRPCalcu();
        }
        public void MRPCalcu()
        {
            double GWT, pregrmrate, mrpprec, totalmrp, T1;
            GWT = Convert.ToDouble(txt_GWt.Text);
            pregrmrate = Convert.ToDouble(txt_grm.Text);
            T1 = (pregrmrate * GWT);
            mrpprec = Convert.ToDouble(txt_percentage.Text);
            totalmrp = (T1 * ((100 + mrpprec) / 100));
            if (txt_percentage.Text == "" || txt_percentage.Text == null || txt_percentage.Text == "0")
            {
                txt_MRP.Text = "0";
            }
            else
            {
                txt_MRP.Text = totalmrp.ToString("F0");
            }
        }
        private void grb_list_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn column in grb_list.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void cmb_selectbrch_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_selectbrch.SelectedValue != null)
            {
                CmbLoadTo.Visible = true;
                label45.Visible = true;
            }
            else
            {
                CmbLoadTo.Visible = false;
                label45.Visible = false;
            }
        }

        private void grb_list_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grbButton1_Click_1(object sender, EventArgs e)
        {
            List();
        }

        private void grbButton1_Click_2(object sender, EventArgs e)
        {
            List();
        }

        private void grb_list_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label60_Click(object sender, EventArgs e)
        {

        }

        private void txtrcptid_TextChanged(object sender, EventArgs e)
        {
            string s;
            if (txtrcptid.Text != "")
            {
                foreach (DataGridViewRow r in grb_pending.Rows)
                {
                    if (r.Cells["TransId"].Value.ToString() == txtrcptid.Text)
                    {
                        r.Selected = true;
                        grb_pending.CurrentCell = r.Cells["ItemName"];
                        grb_pending.Select();
                        s = r.Cells["PercentageGrmRate"].Value.ToString();
                        //s. = txt_grm.Text.ToString();
                        if (txt_grm.Text == null || txt_grm.Text == "0" || txt_grm.Text == "0")
                        {
                            txt_grm.Text = Convert.ToString(s);
                        }
                    }
                }
            }
        }
        private void CmbJobNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }
        private void CmbJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (CmbJobNo.SelectedValue != null)
            //{
            //    TxtJobNo.Text = CmbJobNo.SelectedValue.ToString();
            //}
        }
        private void txt_StCtWt_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == txt_StCtWt)
            {
                double stctwt;
                stctwt = Convert.ToDouble(txt_StCtWt.Text);

                txt_StWt.Text = (stctwt * .2d).ToString("f3");
            }
        }
        private void CmbMessure_TextChanged(object sender, EventArgs e)
        {
            if (CmbMessure.SelectedValue != null)
            {
                TxtUnitID.Text = CmbMessure.SelectedValue.ToString();
            }
            else
            {
                TxtUnitID.Text = "0";
            }
        }
        private void CmbJobNo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbJobNo.SelectedValue != null)
            {
                TxtJobNo.Text = CmbJobNo.SelectedValue.ToString();
            }
            if (CmbJobNo.SelectedValue == null)
            {
                TxtJobNo.Text = "0";
            }
        }
        private void TxtRatePerGm_TextChanged(object sender, EventArgs e)
        {
        }
        private void cmbDesigncode_TextChanged(object sender, EventArgs e)
        {
            if (cmbDesigncode.Text == "")
            {
                cmb_ItemName.Text = "";
                cmb_ItemName.SelectedValue = 0;
                txt_GWt.Text = "";
                txt_DiaWt.Text = "";
                txt_StWt.Text = "";
                cmb_ItemName.Enabled = true;
            }
            if (cmbDesigncode.SelectedValue != null)
            {
                Txtimgid.Text = cmbDesigncode.SelectedValue.ToString();
            }
            else
            {
                Txtimgid.Text = "0";
            }
        }
        private void cmbDesigncode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbDesigncode.SelectedValue == null)
            {
                cmb_ItemName.SelectedValue = 0;
                cmb_ItemName.Text = "";
                txt_GWt.Text = "";
                txt_DiaWt.Text = "";
                txt_StWt.Text = "";
                cmb_ItemName.Enabled = true;
                return;
            }
            if (cmbDesigncode.SelectedValue != null || cmbDesigncode.Text == "")
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                  ("select ItemId,Gwt,Diawt,Stwt  from DES.VFetchDataDesignCodes  where DesignCodeId= " + cmbDesigncode.SelectedValue.ToString() + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmb_ItemName.SelectedValue = dt.Rows[0]["ItemId"].ToString();
                        cmb_ItemName.Enabled = false;
                        txt_GWt.Text = dt.Rows[0]["Gwt"].ToString();
                        txt_DiaWt.Text = dt.Rows[0]["Diawt"].ToString();
                        txt_StWt.Text = dt.Rows[0]["Stwt"].ToString();
                    }
                }
            }
        }
        private void CmbDeisgnCodeSearch_Click(object sender, EventArgs e)
        {
        }
    }
}

