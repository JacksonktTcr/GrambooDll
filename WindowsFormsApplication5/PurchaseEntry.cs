using System;
using System.Collections.Generic;
using SAFA.Classes;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gramboo.Database;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Linq;
using System.Xml;
using Gramboo;

namespace SAFA.Forms.PUR
{
    public partial class PurchaseEntry : Gramboo.Controls.GrbForm
    {
        bool Message = true; bool BillAdj = true; bool msgval;

        private int SaleCurrentRow = -1;
        private bool FillFlag = false;

        //public SAFA.Forms.STK.FrmTransferInbox TransferInbox;
        //public SAFA.Forms.ACC.AdjustingEntries ADE;
        private static PurchaseEntry instance;
        private string importfile, RoundTo = "F3";
        //SAFA.Classes.ImportSettings im;

        public static PurchaseEntry Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new PurchaseEntry();
                }
                else if (instance.IsDisposed)
                {
                    instance = new PurchaseEntry();
                }
                return instance;
            }
        }

        public PurchaseEntry()
        {
            InitializeComponent();
            groupBox1.Size = new Size(1240, 500); groupBox1.Location = new Point(4, 136);
            groupBox3.Size = new Size(1240, 80); groupBox3.Location = new Point(4, 629);

            

            txtWstPer.Text += new EventHandler(Wastage);
            txtGrossWt.Text += new EventHandler(Wastage);
            //txtRoundoff.TextChanged += new EventHandler(NetAmount);
            txtTotalAmount.TextChanged += new EventHandler(NetAmount);
            txtTaxAmount.TextChanged += new EventHandler(totalamount);
            txtOtherCharges.TextChanged += new EventHandler(totalamount);
            TxtBillAdjAmt.TextChanged += new EventHandler(totalamount);
            txtDiscountAmount.TextChanged += new EventHandler(totalamount);
            txtGrandTotal.TextChanged += new EventHandler(totalamount);
            TxtTCSAmt.TextChanged += new EventHandler(NetAmount);
            TxtAmountTDS.TextChanged += new EventHandler(NetAmount);

        }



        public override bool GenerateID(Table table_name)
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
            FillFlag = true;

            if (base.FillData(PrimaryValues))
            {
                Message = false;
                BtnDocAdd.Visible = true;
               // DocButtonChange();
                TCS();
                TDS();
                BillAdj = false; bool BillWise = false;
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                      ("select isnull(BillWise,0) as BillWise from ACC.Vtds where PartyType='Supplier' and PartyId =" + Cmb_SupplierId.SelectedValue + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        BillWise = Convert.ToBoolean(dt.Rows[0]["BillWise"].ToString());
                    }
                }
               // if (BillWise == true && rbtGrp1.Value == "2") { BtnBillAdj.Visible = true; BillAdjButtonChange(); } else { BtnBillAdj.Visible = false; }

                Binding_BillAdj_To_Panel(Convert.ToInt64(Cmb_SupplierId.SelectedValue), Convert.ToInt64(TxtPurId.Text == "" ? "0" : TxtPurId.Text));

                FillFlag = false;
                return true;

            }
            else
            {
                Message = true;
                FillFlag = false;
                return false;
            }
        }

        public override void RefreshData()
        {
            base.RefreshData();
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            if (!IsEditMode)
            {
                
                    cmb_VoucherTypeId.SelectedValue = 249;
                 
                //}
                //cmb_VoucherTypeId.SelectedValue = (int)SAFA.Classes.VoucherTypes.PurchaseMaster;
                TxtVoucherNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_Purchasedt.Value,
                        DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            }


            Gramboo.General.Setupcombo(cmb_modelname, "ITM.ModelMaster", "ModelName", "ModelId", "IsActive='True' AND Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
            Gramboo.General.Setupcombo(Cmb_ItemName, "ITM.VItemMaster", "[Item Name]", "ItemId", "[Is Active]='True' and billGroup='" + cmbmetaltype.SelectedValue + "'");
            Gramboo.General.Setupcombo(cmbbranch, "syst.branchmaster", "BranchName", "BranchId", "[IsActive]='True'");
            Gramboo.General.Setupcombo(cmb_purity, "ITM.PurityMaster", "PurityName", "PurityId", "IsActive='True'");
            Cmb_SupplierId.SelectedValueChanged -= Cmb_SupplierId_SelectedValueChanged_1;
            Gramboo.General.Setupcombo(Cmb_SupplierId, "PUR.SupplierMaster", "SuppName", "SuppId", "IsActive='True' AND  Company_Id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
            Cmb_SupplierId.SelectedValueChanged += Cmb_SupplierId_SelectedValueChanged_1;
            Gramboo.General.Setupcombo(Cmb_PurTypeId, "PUR.PurchaseTaxTypeMaster", "PurTypeName", "PurTypeId", "IsActive='True'  AND  Company_Id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
            Cmb_SupplierId.DropDownWidth = 420;
            Gramboo.General.Setupcombo(cmb_Taxname, "PUR.TaxMaster", "TaxName", "TaxId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmb_FloorName, "STK.FloorMaster", "FloorName", "FloorId", "IsActive='True'  AND Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);

            Gramboo.General.Setupcombo(cmbmetaltype, "SALE.VSalesTypes", "MetalTypeName", "MetalType");
            Gramboo.General.Setupcombo(cmbtdsName, "GEN.TDSMaster ", "TdsName", "TdsId", "IsActive='True'");
            Gramboo.General.Setupcombo(CmbTCS, "GEN.TCSMaster ", "TcsName", "TcsId", "IsActive='True'");
            Gramboo.General.Setupcombo(CmbTdsId, "GEN.TDSMaster ", "TdsName", "TdsId", "IsActive='True'");
            CmbTCS.DropDownWidth = 300;
            CmbTdsId.DropDownWidth = 300;


            txtDeptName.Text ="";
            cmbmetaltype.SelectedValueChanged += cmbmetaltype_SelectedValueChanged;
            grb_Retail.CheckedChanged += grb_Retail_CheckedChanged;
            grb_WholeSale.CheckedChanged += grb_WholeSale_CheckedChanged;
        }

        public override bool InitializeTables()
        {

            Table t = new Table(SAFA.Classes.Common.DbName, "PUR", "PurchaseMaster");

            t.PrimaryKeys.Add("PurId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = TxtPurId;
            Table t1 = new Table(SAFA.Classes.Common.DbName, "PUR", "PurchaseDetails", true);
            t1.PrimaryKeys.Add("TransId");
            t1.FillView = new Table(SAFA.Classes.Common.DbName, "PUR", "VPurchaseDetails", true);
            t1.DatagridView = dgv_itemDetails;
            t1.IsDatagridView = true;
            t1.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t1);
            LogView = "PUR.VPurchaseMasterList";

            Table t2 = new Table(SAFA.Classes.Common.DbName, "PUR", "PurchaseTaxDetails", true);
            t1.PrimaryKeys.Add("TransID");
            t2.FillView = new Table(SAFA.Classes.Common.DbName, "PUR", "VPurchaseTaxDetails", true);
            t2.DatagridView = dgv_TaxDetails;
            t2.IsDatagridView = true;
            t2.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t2);

            Table t3 = new Table(SAFA.Classes.Common.DbName, "PUR", "PurchaseOtherCharges", true);
            t1.PrimaryKeys.Add("TransID");
            t3.FillView = new Table(SAFA.Classes.Common.DbName, "PUR", "VPurchaseOtherCharges", true);
            t3.DatagridView = dgv_otherChg;
            t3.IsDatagridView = true;
            t3.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t3);

            this.TableName = t;

            return true;
        }


        public override void Init()
        {
            grb_Retail.CheckedChanged -= grb_Retail_CheckedChanged;
            grb_WholeSale.CheckedChanged -= grb_WholeSale_CheckedChanged;
            cmbmetaltype.SelectedValueChanged -= cmbmetaltype_SelectedValueChanged;
            GrpBillAdj.Controls.Clear();
            FillFlag = true;
            base.Init();
            //this.btnAddNewSupplier.MasterForm = Kallans.Forms.PUR.SupplierMaster.Instance;
            //this.btnAddNewSupplier.ParentForm = PurchaseEntry.Instance;
            txtOne_Two.Text = "1";  
            Message = true;
            BillAdj = true;
            TxtIsactive.Text = "1";
            txtOne_Two.Visible = false;
            rb_supplier.Checked = true;
            ChkLessStone.Checked = true;
            ChkLessDiamond.Checked = true;
           
            // dgv_TaxDetails.SummaryRowVisible = true;
            dgv_TaxDetails.DataFields = new List<string>() { "TransID", "PurID", "TaxID", "TaxName", "TaxRate", "Amount" };
            dgv_TaxDetails.HiddenDataFields = new List<string>() { "TransID", "PurID", "TaxID" };
            dgv_TaxDetails.SummaryColumns = new string[] { "Amount", "TaxRate" };
            dgv_TaxDetails.Fill(new Table(SAFA.Classes.Common.DbName, "PUR", "VPurchaseTaxDetails", true), "1=2");
            dgv_TaxDetails.Columns["col_AutoSlno"].DataPropertyName = "SlNo";

            //dgv_otherChg.SummaryRowVisible = true;
            dgv_otherChg.DataFields = new List<string>() { "TransID", "PurID", "ChargeID", "[Charge Name]", "NetAmount", "SGSTAmt", "CGSTAmt", "IGSTAmt", "CESSAmt", "TdsName", "TdsId", "TdsRate", "TdsAmount", "Amount", "CalculateTax", "IGSTPerc", "CGSTPerc", "SGSTPerc", "CESSPerc" };
            dgv_otherChg.HiddenDataFields = new List<string>() { "TransID", "PurID", "ChargeID", "CalculateTax", "TdsId", "IGSTPerc", "CGSTPerc", "SGSTPerc", "CESSPerc" };
            dgv_otherChg.SummaryColumns = new string[] { "Amount" };
            dgv_otherChg.Fill(new Table(SAFA.Classes.Common.DbName, "PUR", "VPurchaseOtherCharges", true), "1=2");
            dgv_otherChg.Columns["col_AutoSlno"].DataPropertyName = "SlNo";


            SaleCurrentRow = -1;



            this.ListForm = PurchaseEntryList.Instance;

            if (this.TableName != null)
                GenerateID(this.TableName);
            GrpPurTaxDetails.Visible = false;
            GrpPurOtherCharges.Visible = false;
            GrpPurTaxDetails.Parent = this;
            GrpPurOtherCharges.Parent = this;
            txtOtherCharges.Text = (string.IsNullOrEmpty(txtOtherCharges.Text.Trim()) ? "0.00" : txtOtherCharges.Text);
            // txtPurity.Enabled = true;
            Rb_Billcash.TabStop = true;
            Rb_Weight.TabStop = true;

            rbtGrp1.DefaultRadioButton = grbRadioButton1;
            grbRb_pay.DefaultRadioButton = Rb_cash;
            Rb_Billcash.TabStop = true;
            Rb_Weight.TabStop = true;
            Rb_Billcash.TabStop = true;
            grbRadioButton1.TabStop = true;
            chkStone.TabStop = true;
            ChkDia.Checked = true;
            chkStone.Checked = true;

            grb_WholeSale.Checked = true;



            //this.btnAddNewSupplier.MasterForm = SAFA.Forms.PUR.frmSupplierMaster.Instance;
            //this.btnAddNewSupplier.ParentForm = SAFA.Forms.PUR.PurchaseEntry.instance;
            //this.btnAddNewSupplier.ParentControl = Cmb_SupplierId;

            //this.addNewMasterOtherCharge.MasterForm = SAFA.Forms.PUR.MiscelleniousChargeMaster.Instance;
            //this.addNewMasterOtherCharge.ParentForm = SAFA.Forms.PUR.PurchaseEntry.instance;
            //this.addNewMasterOtherCharge.ParentControl = Cmb_Chargename;
            Cmb_SupplierId.Focus();

            cmbtdsName.Text = "";

            FillFlag = false;
            BtnDocAdd.Visible = false; BtnBillAdj.Visible = false;
            dgv_itemDetails.Columns["DiaCash"].HeaderText = "DmdAmt";
            dgv_itemDetails.Columns["StoneCash"].HeaderText = "StoneAmt";

            Binding_BillAdj_To_Panel(Convert.ToInt64(Cmb_SupplierId.SelectedValue), Convert.ToInt64(TxtPurId.Text == "" ? "0" : TxtPurId.Text));
            cmbmetaltype.SelectedValue = "G";
        }
        private void GetDueDate()
        {
            int i; int PaymentTerms = 0;
            if (txtPaymentTerms.Text != "")
            {
                DateTime Purchase = Convert.ToDateTime(Dtp_Purchasedt.Text);
                PaymentTerms = Convert.ToInt16(txtPaymentTerms.Text);

                dtp_Duedate.Value = Purchase.AddDays(PaymentTerms);

            }
        }

        private void AdjustColumnWidths()
        {
            dgv_itemDetails.RowHeadersVisible = false;
            dgv_itemDetails.Columns[0].Width = 50;
            dgv_itemDetails.Columns["JobNo"].Width = 86 + 5;
            dgv_itemDetails.Columns["Item Name"].Width = 269 + 2;
            dgv_itemDetails.Columns["Purity"].Width = 57 + 5;
            dgv_itemDetails.Columns["Touch"].Width = 50;
            //dgv_itemDetails.Columns["Model Name"].Width = cmb_modelname.Width + 18;
            dgv_itemDetails.Columns["Nos"].Width = 50;
            dgv_itemDetails.Columns["Gwt"].Width = 60;
            dgv_itemDetails.Columns["Stno"].Width = 40;
            dgv_itemDetails.Columns["Stwt"].Width = 60;
            dgv_itemDetails.Columns["StoneCash"].Width = 90;
            dgv_itemDetails.Columns["Ot_Stno"].Width = 55;
            dgv_itemDetails.Columns["Ot_StWt"].Width = 60;
            dgv_itemDetails.Columns["Ot_StRate"].Width = 60;
            dgv_itemDetails.Columns["Ot_StAmt"].Width = 80;
            dgv_itemDetails.Columns["Pr_Stno"].Width = 55;
            dgv_itemDetails.Columns["Pr_StWt"].Width = 60;
            dgv_itemDetails.Columns["Pr_StRate"].Width = 60;
            dgv_itemDetails.Columns["Pr_StAmt"].Width = 80;
            dgv_itemDetails.Columns["Diawt"].Width = 60;
            dgv_itemDetails.Columns["DiaCash"].Width = 90;
            dgv_itemDetails.Columns["NetWt1"].Width = 60;
            dgv_itemDetails.Columns["NetWt2"].Width = 60;
            dgv_itemDetails.Columns["NetWt"].Width = 60;
            dgv_itemDetails.Columns["MetalCash1"].Width = 90;
            dgv_itemDetails.Columns["MetalCash2"].Width = 90;
            dgv_itemDetails.Columns["MetalCash"].Width = 90;
            dgv_itemDetails.Columns["MCPerc"].Width = 50;
            dgv_itemDetails.Columns["MC"].Width = 50;
            dgv_itemDetails.Columns["WstPerc"].Width = 50;
            dgv_itemDetails.Columns["Wst"].Width = 50;
            dgv_itemDetails.Columns["MetalRate1"].Width = 70;
            dgv_itemDetails.Columns["MetalRate2"].Width = 70;
            dgv_itemDetails.Columns["Rate"].Width = 70;
            dgv_itemDetails.Columns["Total"].Width = 120;
            dgv_itemDetails.Columns["Model Name"].Visible = false;
            //    dgv_itemDetails.Columns["TotalAmount"].Width = 120;
            dgv_itemDetails.Columns["WT_99_5"].Width = 68;
            dgv_itemDetails.Columns["WT_92"].Width = 65;
            dgv_itemDetails.Columns["MC_WT"].Width = 65;
            dgv_itemDetails.Columns["WT_100"].Width = 65;
            dgv_itemDetails.Columns["WT_99_5"].HeaderText = "WT (99.5)";
            dgv_itemDetails.Columns["WT_100"].HeaderText = "WT (100)";
            dgv_itemDetails.Columns["WT_92"].HeaderText = "WT (92)";
            dgv_itemDetails.Columns["MC_WT"].HeaderText = "MC WT";
            dgv_itemDetails.Columns["NetWt2"].ReadOnly = true;

            dgv_itemDetails.Columns["ProdCode"].ReadOnly = true;
            dgv_itemDetails.Columns["Total"].ReadOnly = true;
            dgv_itemDetails.Columns["NetWt"].ReadOnly = true;
            dgv_itemDetails.Columns["MetalCash"].ReadOnly = true;
            dgv_itemDetails.Columns["Ot_StAmt"].ReadOnly = true;
            dgv_itemDetails.Columns["Pr_StAmt"].ReadOnly = true;
            dgv_itemDetails.Columns["Purity"].ReadOnly = true;
            dgv_itemDetails.Columns["Model Name"].ReadOnly = true;

        }

        private void dgv_GRN_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_itemDetails.SelectedCells.Count > 0)
            {
                SaleCurrentRow = dgv_itemDetails.SelectedCells[0].RowIndex;
                if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "ProdCode")
                {
                    try
                    {

                        // Cmb_ProdCode_SDtl.Parent = dgv_GRN;
                        Cmb_ProdCode.Visible = true;
                        Cmb_ProdCode.Text = dgv_itemDetails.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Location;
                        Cmb_ProdCode.Location = new System.Drawing.Point(p.X + dgv_itemDetails.Parent.Location.X, p.Y + dgv_itemDetails.Parent.Location.Y);


                        Cmb_ProdCode.Size = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Size;
                        Cmb_ProdCode.BringToFront();
                        Cmb_ProdCode.Focus();
                        Cmb_ProdCode.DroppedDown = true;
                    }

                    catch { }
                }
                if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Code")
                {
                    try
                    {

                        // Cmb_ProdCode_SDtl.Parent = dgv_GRN;
                        txtitemcode.Visible = true;
                        txtitemcode.Text = dgv_itemDetails.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Location;
                        Cmb_ProdCode.Location = new System.Drawing.Point(p.X + dgv_itemDetails.Parent.Location.X, p.Y + dgv_itemDetails.Parent.Location.Y);


                        txtitemcode.Size = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Size;
                        txtitemcode.BringToFront();
                        txtitemcode.Focus();

                    }

                    catch { }
                }
                if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Item Name")
                {
                    try
                    {

                        // Cmb_ProdCode_SDtl.Parent = dgv_GRN;
                        Cmb_ItemName.Visible = true;
                        Cmb_ItemName.Text = dgv_itemDetails.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Location;
                        Cmb_ItemName.Location = new System.Drawing.Point(p.X + dgv_itemDetails.Parent.Location.X, p.Y + dgv_itemDetails.Parent.Location.Y);


                        Cmb_ItemName.Size = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Size;
                        Cmb_ItemName.BringToFront();
                        Cmb_ItemName.Focus();
                        // Cmb_ItemName.DroppedDown = true;
                    }

                    catch { }
                }
                if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "JobNo")
                {
                    try
                    {

                        CmbJobNo.Visible = true;
                        CmbJobNo.Text = dgv_itemDetails.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Location;
                        CmbJobNo.Location = new System.Drawing.Point(p.X + dgv_itemDetails.Parent.Location.X, p.Y + dgv_itemDetails.Parent.Location.Y);


                        CmbJobNo.Size = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Size;
                        CmbJobNo.BringToFront();
                        CmbJobNo.Focus();
                        CmbJobNo.DroppedDown = true;

                    }
                    catch { }

                }
                if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Purity")
                {
                    try
                    {

                        cmb_purity.Visible = true;
                        cmb_purity.Text = dgv_itemDetails.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Location;
                        cmb_purity.Location = new System.Drawing.Point(p.X + dgv_itemDetails.Parent.Location.X, p.Y + dgv_itemDetails.Parent.Location.Y);


                        cmb_purity.Size = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Size;
                        cmb_purity.BringToFront();
                        cmb_purity.Focus();
                        cmb_purity.DroppedDown = true;

                    }
                    catch { }

                }
                if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Branch Name")
                {
                    try
                    {

                        cmbbranch.Visible = true;
                        cmbbranch.Text = dgv_itemDetails.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Location;
                        cmbbranch.Location = new System.Drawing.Point(p.X + dgv_itemDetails.Parent.Location.X, p.Y + dgv_itemDetails.Parent.Location.Y);


                        cmbbranch.Size = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Size;
                        cmbbranch.BringToFront();
                        cmbbranch.Focus();
                        cmbbranch.DroppedDown = true;

                    }
                    catch { }

                }
                if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Floor Name")
                {
                    try
                    {

                        cmb_FloorName.Visible = true;
                        cmb_FloorName.Text = dgv_itemDetails.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Location;
                        cmb_FloorName.Location = new System.Drawing.Point(p.X + dgv_itemDetails.Parent.Location.X, p.Y + dgv_itemDetails.Parent.Location.Y);


                        cmb_FloorName.Size = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Size;
                        cmb_FloorName.BringToFront();
                        cmb_FloorName.Focus();


                    }
                    catch { }

                }

                cmb_FloorName.Focus();

            }

            if (SaleCurrentRow > -1)
            {
                if (dgv_itemDetails.SelectedCells.Count > 0)
                {
                    if (dgv_itemDetails.CurrentRow.Cells["Item Name"].Value.ToString() != "")
                    {
                        using (DataTable dt = DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VItemMaster WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value + " and ItemTypeId=3")).Tables[0])
                        {
                            if (dt.Rows.Count > 0)
                            {


                                if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Model Name")
                                {
                                    try
                                    {
                                        SaleCurrentRow = dgv_itemDetails.SelectedCells[0].RowIndex;
                                        cmb_modelname.Visible = true;
                                        cmb_modelname.Text = dgv_itemDetails.SelectedCells[0].Value.ToString();

                                        System.Drawing.Point p = new System.Drawing.Point();
                                        p = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Location;
                                        cmb_modelname.Location = new System.Drawing.Point(p.X + dgv_itemDetails.Parent.Location.X, p.Y + dgv_itemDetails.Parent.Location.Y);


                                        cmb_modelname.Size = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Size;
                                        cmb_modelname.BringToFront();
                                        cmb_modelname.Focus();
                                    }
                                    catch { }

                                }


                            }
                            else
                            {
                                cmb_modelname.Text = "";
                                cmb_modelname.Visible = false;

                                foreach (DataGridViewCell cell in dgv_itemDetails.SelectedCells)
                                {
                                    if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Model Name")
                                    {

                                        string i = "";


                                        cell.Value = (object)i;



                                    }
                                }

                            }
                        }
                    }
                }
            }




            //using (DataTable dt = DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VItemMaster WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value + " and ItemTypeId=3")).Tables[0])
            //{
            //    if (dt.Rows.Count > 0)
            //    {

            //        if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Model Name")
            //        {
            //            try
            //            {
            //                SaleCurrentRow = dgv_itemDetails.SelectedCells[0].RowIndex;
            //                cmb_modelname.Visible = true;
            //                cmb_modelname.Text = dgv_itemDetails.SelectedCells[0].Value.ToString();

            //                System.Drawing.Point p = new System.Drawing.Point();
            //                p = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Location;
            //                cmb_modelname.Location = new System.Drawing.Point(p.X + dgv_itemDetails.Parent.Location.X, p.Y + dgv_itemDetails.Parent.Location.Y);


            //                cmb_modelname.Size = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Size;
            //                cmb_modelname.BringToFront();
            //                cmb_modelname.Focus();
            //            }
            //            catch { }

            //        }
            //    }
            //}



            //cmb_FloorName.Focus(); 

        }

        private void Cmb_ItemName_Leave(object sender, EventArgs e)
        {
            Cmb_ItemName.Visible = false;

            AfterComboLeave(dgv_itemDetails, grbComboBox1, (dgv_itemDetails.Columns.Count > 0 ? dgv_itemDetails.Columns["Item Name"].Index : -1));

            if (Cmb_ItemName.Text != "" && Cmb_ItemName.SelectedValue != null)
            {


                dgv_itemDetails.CurrentRow.Cells["ItemID"].Value = Cmb_ItemName.SelectedValue.ToString();

                dgv_itemDetails.CurrentRow.Cells["Item Name"].Value = Cmb_ItemName.Text.ToString();







                //if (DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM ITM.VItemMaster WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value + " and  ItemTypeId=3")).Tables[0].Rows.Count != 0)
                //{
                //    dgv_itemDetails.Columns["Model Name"].ReadOnly = false;
                //    cmb_modelname.Enabled = true;

                //}
                //else
                //{
                //    dgv_itemDetails.Columns["Model Name"].ReadOnly = true;

                //    cmb_modelname.Enabled = false;

                //}




                if (DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM ITM.VGoldOrnaments WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value)).Tables[0].Rows.Count != 0)
                {
                    dgv_itemDetails.CurrentRow.Cells["Rate"].Value = (DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString();
                }
                else if (DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM ITM.VGoldItems WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value + " and ItemTypeId = 1 and ItemId!=61")).Tables[0].Rows.Count != 0)
                {
                    dgv_itemDetails.CurrentRow.Cells["Rate"].Value = (DBConn.GetData(new SqlCommand("SELECT Top 1 BarRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BarRate"]).ToString();
                }

                else if (DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM ITM.VSilverItems WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value + " and ItemId !=62")).Tables[0].Rows.Count != 0)
                {
                    dgv_itemDetails.CurrentRow.Cells["Rate"].Value = (DBConn.GetData(new SqlCommand("SELECT Top 1 SilverRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'    and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["SilverRate"]).ToString();


                }


                else if (DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM ITM.VPlatinumItem WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value)).Tables[0].Rows.Count != 0)
                {
                    dgv_itemDetails.CurrentRow.Cells["Rate"].Value = (DBConn.GetData(new SqlCommand("SELECT Top 1 PlatinumRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate ,EntryTime DESC ")).Tables[0].Rows[0]["PlatinumRate"]).ToString();


                }


                else if (DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM ITM.ItemMaster WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value + " and ItemId=62")).Tables[0].Rows.Count != 0)
                {
                    dgv_itemDetails.CurrentRow.Cells["Rate"].Value = (DBConn.GetData(new SqlCommand("SELECT Top 1 OldSilverRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["OldSilverRate"]).ToString();
                    cmb_purity.Visible = false;
                    dgv_itemDetails.CurrentRow.Cells["Purity"].ReadOnly = true;
                    dgv_itemDetails.CurrentRow.Cells["Purity"].Value = 0;
                }

                else if (DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM ITM.ItemMaster WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value + "  and ItemId=61")).Tables[0].Rows.Count != 0)
                {
                    dgv_itemDetails.CurrentRow.Cells["Rate"].Value = (DBConn.GetData(new SqlCommand("SELECT Top 1 OldGoldRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["OldGoldRate"]).ToString();
                    cmb_purity.Visible = false;
                    dgv_itemDetails.CurrentRow.Cells["Purity"].ReadOnly = true;
                    dgv_itemDetails.CurrentRow.Cells["Purity"].Value = 0;
                }
                else
                {
                    dgv_itemDetails.CurrentRow.Cells["Rate"].Value = 0;
                }





                enable();
            }



            enable();





        }

        private void cmb_purity_Leave(object sender, EventArgs e)
        {
            cmb_purity.Visible = false;

            AfterComboLeave(dgv_itemDetails, cmb_purity, (dgv_itemDetails.Columns.Count > 0 ? dgv_itemDetails.Columns["Purity Value"].Index : -1));


            if (cmb_purity.SelectedValue != null)
            {



                using (DataTable dt = DBConn.GetData(new SqlCommand("Select [Purity Value] FROM ITM.VPurityMaster WHERE PurityId=" + cmb_purity.SelectedValue + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {

                        dgv_itemDetails.CurrentRow.Cells["Purity Value"].Value = dt.Rows[0]["Purity Value"].ToString();
                        dgv_itemDetails.CurrentRow.Cells["Touch"].Value = dt.Rows[0]["Purity Value"].ToString();


                    }













                    //using (DataTable dt = DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VOrnaments WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value + "")).Tables[0])
                    //{
                    //    if (dt.Rows.Count > 0)
                    //    {



                    //        dgv_itemDetails.CurrentRow.Cells["Rate"].Value = (DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["Rate"]).ToString();

                    //    }
                    //}





                }
                dgv_itemDetails.CurrentRow.Cells["PurityId"].Value = cmb_purity.SelectedValue.ToString();
                dgv_itemDetails.CurrentRow.Cells["Purity"].Value = cmb_purity.Text;


            }
        }

        //       private void Cmb_ProdCode_Leave(object sender, EventArgs e)
        //{
        //    if (Cmb_ProdCode.SelectedValue != null)
        //    {
        //        dgv_itemDetails.CurrentRow.Cells["ProdCode"].Value = Cmb_ProdCode.SelectedValue.ToString();
        //        dgv_itemDetails.CurrentRow.Cells["ProdCodeId"].Value = Cmb_ProdCode.Text;
        //    }
        //    Cmb_ProdCode.Visible = false;
        //}

        private void cmb_modelname_Leave(object sender, EventArgs e)
        {
            if (cmb_modelname.SelectedValue != null)
            {
                dgv_itemDetails.CurrentRow.Cells["ModelId"].Value = cmb_modelname.SelectedValue.ToString();
                dgv_itemDetails.CurrentRow.Cells["Model Name"].Value = cmb_modelname.Text;
            }
            cmb_modelname.Visible = false;
        }

        private void dgv_GRN_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Touch"
             || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Nos" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Gwt"
             || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StWt" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Diawt" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "DiaCash"
             || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StoneCash" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "MC" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Wst" ||
             dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "MCPerc" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "WstPerc" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Rate")
            {
                double i;


                if (!double.TryParse(Convert.ToString(e.FormattedValue), out i))
                {
                    e.Cancel = true;
                    // Gramboo.General.ShowMessage("Enter Decimal value Only");

                }
                cmb_FloorName.Focus();

            }
        }





        private void calculateNetWt()
        {

            Double NetWt = 0, stwt = 0, DiaWt = 0, DiaStWt = 0, TotalWt = 0, gwt = 0, StConwt = 0, DiaConwt = 0;
            String StUnitOfMesurment = "", DiaUnitOfMesurment = "";

            if (dgv_itemDetails.CurrentRow == null)
                return;



            if (dgv_itemDetails.CurrentRow.Cells["ItemID"].Value.ToString() != "")
            {

                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                            ("SELECT StUnitOfMesurment,DiaUnitOfMesurment FROM ITM.VItemMaster as t1 WHERE t1.ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemId"].Value.ToString() + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        StUnitOfMesurment = dt.Rows[0]["StUnitOfMesurment"].ToString();
                        DiaUnitOfMesurment = dt.Rows[0]["DiaUnitOfMesurment"].ToString();
                    }
                }


                if (dgv_itemDetails.CurrentRow.Cells["Gwt"].Value.ToString() != "")
                {
                    gwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Gwt"].Value.ToString());

                }
                if (ChkLessStone.Checked == true)
                {
                    if (dgv_itemDetails.CurrentRow.Cells["StWt"].Value.ToString() != "")
                    {
                        StConwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["StWt"].Value.ToString());

                        if (StUnitOfMesurment == "Cent")
                        {
                            stwt = Math.Round(StConwt, 3) / 500;
                        }
                        else if (StUnitOfMesurment == "Carat")
                        {
                            stwt = Math.Round(StConwt, 3) * 0.2;
                        }
                        else if (StUnitOfMesurment == "Gram")
                        {
                            stwt = StConwt;
                        }
                    }
                }
                else
                {
                    stwt = 0;
                }
                if (ChkLessDiamond.Checked == true)
                {
                    if (dgv_itemDetails.CurrentRow.Cells["Diawt"].Value.ToString() != "")
                    {
                        DiaConwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Diawt"].Value.ToString());

                        if (DiaUnitOfMesurment == "Cent")
                        {
                            DiaWt = Math.Round(DiaConwt, 3) / 100;
                        }
                        else if (DiaUnitOfMesurment == "Carat")
                        {
                            DiaWt = DiaConwt;
                        }
                        else if (DiaUnitOfMesurment == "Gram")
                        {
                            DiaWt = Math.Round(DiaConwt, 3) * 5;
                        }
                    }
                }
                else
                {
                    DiaWt = 0;
                }

                if (gwt != 0)///(Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Gwt"].Value) != 0)
                {

                    NetWt = gwt - (stwt + (DiaWt * .2f));
                    // NetWt = gwt - (stwt + (DiaWt * .2));
                    dgv_itemDetails.CurrentRow.Cells["NetWt"].Value = NetWt.ToString(RoundTo);
                }


            }
            //----------------------------------------------------------------------------

        }



        private void StoneCash()
        {
            double StWt = 0, Rate = 0, StCash = 0;

            if (dgv_itemDetails.CurrentRow.Cells["StWt"].Value.ToString() != "")
            {
                StWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["StWt"].Value.ToString());
            }

            if (dgv_itemDetails.CurrentRow.Cells["Rate"].Value.ToString() != "")
            {
                Rate = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Rate"].Value.ToString());
            }

            if (StWt != 0 && Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Gwt"].Value.ToString()) == 0)
            {
                StCash = StWt * Rate;
                dgv_itemDetails.CurrentRow.Cells["StoneCash"].Value = StCash.ToString();
                dgv_itemDetails.CurrentRow.Cells["StoneCash"].ReadOnly = true;

            }
            else
            {
                dgv_itemDetails.CurrentRow.Cells["StoneCash"].ReadOnly = false;
            }



        }

        private void Diacash()
        {
            double DiaWt = 0, Rate = 0, DiaCash = 0;

            if (dgv_itemDetails.CurrentRow.Cells["Diawt"].Value.ToString() != "")
            {
                DiaWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Diawt"].Value.ToString());
            }

            if (dgv_itemDetails.CurrentRow.Cells["DiaRate"].Value.ToString() != "")
            {
                Rate = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["DiaRate"].Value.ToString());
            }

            if (DiaWt != 0)//&& Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Gwt"].Value.ToString()) == 0)
            {
                DiaCash = DiaWt * Rate;
                dgv_itemDetails.CurrentRow.Cells["DiaCash"].Value = DiaCash.ToString();
                dgv_itemDetails.CurrentRow.Cells["DiaCash"].ReadOnly = true;
            }
            else
            {
                dgv_itemDetails.CurrentRow.Cells["DiaCash"].ReadOnly = false;
            }

        }






        private void UpdateCash()
        {
            if (dgv_itemDetails.CurrentRow == null)
                return;
            if (SaleCurrentRow > -1)
            {

                double MetalRate = 0, NetWt = 0, MetalCash = 0, Touch = 0, MetalTouchRate = 0, Gwt = 0, Nos = 0;
                string calcon = "";




                if (dgv_itemDetails.CurrentRow.Cells["Rate"].Value.ToString() != "")
                {
                    MetalRate = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Rate"].Value.ToString());
                }

                // if (dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString() != "")
                //{
                //Touch = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString());
                //}


                if (dgv_itemDetails.CurrentRow.Cells["ItemId"].Value.ToString() != "")
                {

                    using (DataTable dt = DBConn.GetData(new SqlCommand("Select isnull(calculatedon,'NetWt') as calculatedon  from ITM.ItemMaster where ItemID=" + dgv_itemDetails.CurrentRow.Cells["ItemId"].Value.ToString()), "Calc").Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            calcon = dt.Rows[0]["calculatedon"].ToString();

                        }
                    }

                }


                if (dgv_itemDetails.CurrentRow.Cells["NetWt"].Value.ToString() != "")
                {
                    NetWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["NetWt"].Value.ToString());
                }
                if (dgv_itemDetails.CurrentRow.Cells["Gwt"].Value.ToString() != "")
                {
                    Gwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Gwt"].Value.ToString());
                }
                if (dgv_itemDetails.CurrentRow.Cells["Nos"].Value.ToString() != "")
                {
                    Nos = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Nos"].Value.ToString());
                }


                //MetalTouchRate = (MetalRate / 100) * Touch;
                if (calcon.ToUpper() == "Gwt".ToUpper())
                {
                    MetalCash = Convert.ToDouble((Gwt * MetalRate).ToString());
                    dgv_itemDetails.CurrentRow.Cells["MetalCash"].Value = MetalCash.ToString();
                }
                else if (calcon.ToUpper() == "Nos".ToUpper())
                {
                    MetalCash = Convert.ToDouble((Nos * MetalRate).ToString());
                    dgv_itemDetails.CurrentRow.Cells["MetalCash"].Value = MetalCash.ToString();
                }
                else if (NetWt != 0)
                {
                    MetalCash = Convert.ToDouble((NetWt * MetalRate).ToString());
                    dgv_itemDetails.CurrentRow.Cells["MetalCash"].Value = MetalCash.ToString();
                }
                //if (NetWt != 0)
                //{

                //    dgv_itemDetails.CurrentRow.Cells["MetalCash"].Value = MetalCash.ToString();
                //}
                else
                {

                    dgv_itemDetails.CurrentRow.Cells["MetalCash"].Value = 0;

                }



            }



        }



        private void TotalAmount()
        {

            double Diacash = 0, Stcash = 0, GoldCash = 0, TotalCash = 0, Mc = 0, Discount = 0;

            if (dgv_itemDetails.CurrentRow == null)
                return;
            if (dgv_itemDetails.CurrentRow.Cells["MetalCash"].Value.ToString() != "")
            {
                GoldCash = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["MetalCash"].Value.ToString());

            }

            if (dgv_itemDetails.CurrentRow.Cells["StoneCash"].Value.ToString() != "")
            {
                Stcash = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["StoneCash"].Value.ToString());

            }
            if (dgv_itemDetails.CurrentRow.Cells["DiaCash"].Value.ToString() != "")
            {
                Diacash = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["DiaCash"].Value.ToString());

            }
            if (dgv_itemDetails.CurrentRow.Cells["MC"].Value.ToString() != "")
            {
                Mc = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["MC"].Value.ToString());

            }
            if (dgv_itemDetails.CurrentRow.Cells["Discount"].Value.ToString() != "")
            {
                Discount = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Discount"].Value.ToString());

            }




            //TotalCash = GoldCash + Stcash + Diacash+Mc;

            dgv_itemDetails.CurrentRow.Cells["Total"].Value = Convert.ToDouble(GoldCash + Stcash + Diacash + Mc - Discount).ToString();

        }

        private void dgv_GRN_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgv_itemDetails.RowTemplate.Height = Cmb_ItemName.Height;
            foreach (DataGridViewColumn c in dgv_itemDetails.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dgv_GRN_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void dgv_GRN_CellClick(object sender, DataGridViewCellEventArgs e)
        {



            //if (dgv_itemDetails.SelectedCells.Count > 0)
            //{
            //    SaleCurrentRow = dgv_itemDetails.SelectedCells[0].RowIndex;
            //    if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Item Name")
            //    {
            //        try
            //        {


            //            Cmb_ItemName.Visible = true;
            //            Cmb_ItemName.Text = dgv_itemDetails.SelectedCells[0].Value.ToString();

            //            System.Drawing.Point p = new System.Drawing.Point();
            //            p = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Location;
            //            Cmb_ItemName.Location = new System.Drawing.Point(p.X + dgv_itemDetails.Parent.Location.X, p.Y + dgv_itemDetails.Parent.Location.Y);


            //            Cmb_ItemName.Size = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Size;
            //            Cmb_ItemName.BringToFront();
            //            Cmb_ItemName.Focus();
            //        }
            //        catch { }
            //    }
            //    else
            //    {
            //        Cmb_ItemName.Visible = false;
            //    }

            //}


        }

        private void lnkGrn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            //if (pnlGRN.Visible == false)
            //{
            //    pnlGRN.Size = new System.Drawing.Size(800, 250);
            //    pnlGRN.Visible = true;
            //    pnlGRN.BringToFront();
            //    pnlGRN.Parent = this;

            //    pnlGRN.Location = new Point(lnkGrn.Location.X - 500 + lnkGrn.Parent.Location.X,
            //    lnkGrn.Parent.Location.Y + lnkGrn.Location.Y + 40);
            //    pnlGRN.Show();
            //    pnlGRN.BringToFront();
            //    ShowGRNDetails();
            //    lnkGrn.Text = "Hide GRN Details";
            //}
            //else
            //{
            //    pnlGRN.Visible = false;
            //    pnlGRN.SendToBack();
            //    pnlGRN.Hide();
            //    lnkGrn.Text = "Show GRN Details";
            //}
        }

        private void ShowGRNDetails()
        {
            string Itemidlist1 = "0";

            foreach (DataGridViewRow r in dgv_itemDetails.Rows)
            {
                if (Itemidlist1.Contains(r.Cells["GRNId"].Value.ToString()) == false)
                    Itemidlist1 += "," + r.Cells["GRNId"].Value.ToString();
            }

            dgvGRNview.ShowEdit = true;
            dgvGRNview.AutoGenerateColumns = true;
            dgvGRNview.ShowSerialNo = true;
            dgvGRNview.ShowDelete = false;
            dgvGRNview.ShowEdit = false;
            dgvGRNview.ShowSelectCheckBox = true;
            dgvGRNview.SummaryColumns = new string[] { "Nos", "Gwt", "NetWt", "DiaNo", "Diawt", "StNo", "StWt", "MetalCash", "DiaCash", "StoneCash", "MC", "Wst", "SetNo", "SetWt", "Total" };
            dgvGRNview.HiddenDataFields = new List<string>() { "GRNId", "TransId", "Company_id", "Branch_id", "ItemID", "ProdCodeId", "PurityId", "ModelId", "SetNo", "SetWt", "SetCash", "Metal2" };
            dgvGRNview.DataSource = this.DBConn.GetData(new SqlCommand("Select PurityId,GRNId,TransId,ItemID,ModelId,ProdCodeId,[Item Name],ProdCode,Purity,Touch,[Model Name],Nos,Gwt,MetalCash,DiaNo,Diawt,DiaCash,StNo,StWt,StoneCash,SetNo,SetWt,SetCash,Metal1,Metal2,NetWt,MC,Wst,Total,Company_id,Branch_id FROM PUR.VGetGRN_OrnamentsDetails WHERE [GRN No]='" + cmb_GRNno.Text + "' AND TransId not in (" + Itemidlist1 + ") and TransId not in (select distinct GRNId FROM PUR.PurchaseDetailsOrnament) ")).Tables[0];
            //"PurityId", "PurId", "GRNId", "TransId", "ItemID", "ModelId", "ProdCodeId", "[Item Name]", "ProdCode", "Purity", "Touch", "[Model Name]", "Nos", "Gwt", "DiaNo", "Diawt", "StNo", "StWt", "GoldCash", "DiaCash", "StoneCash", "MC", "Wst", "SetNo", "SetWt", "Total" };        
        }

        private void ChkDia_CheckedChanged(object sender, EventArgs e)
        {
            //UpdateCash();
        }

        private void chkStone_CheckedChanged(object sender, EventArgs e)
        {
            // UpdateCash();

        }

        private void linkLbl_taxDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (GrpPurTaxDetails.Visible == false)
            {
                GrpPurTaxDetails.Size = new System.Drawing.Size(500, 250);

                GrpPurTaxDetails.Location = new Point(linkLbl_taxDetails.Location.X + linkLbl_taxDetails.Parent.Location.X,
                    linkLbl_taxDetails.Parent.Location.Y + linkLbl_taxDetails.Location.Y - GrpPurTaxDetails.Height);
                GrpPurTaxDetails.Visible = true;
                GrpPurTaxDetails.BringToFront();
                GrpPurTaxDetails.Parent = this;
                GrpPurTaxDetails.Show();
                linkLbl_taxDetails.Text = "Hide Tax %";
            }
            else
            {
                GrpPurTaxDetails.Visible = false;
                GrpPurTaxDetails.SendToBack();
                GrpPurTaxDetails.Hide();
                linkLbl_taxDetails.Text = "Add Tax %";

            }

        }

        private void linkLbl_OtherCharges_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (GrpPurOtherCharges.Visible == false)
            {
                GrpPurOtherCharges.Size = new System.Drawing.Size(1080, 250);
                GrpPurOtherCharges.Visible = true;
                GrpPurOtherCharges.BringToFront();
                GrpPurOtherCharges.Parent = this;

                GrpPurOtherCharges.Location = new Point(100, 300);
                GrpPurOtherCharges.Show();
                linkLbl_OtherCharges.Text = "Other Charges";
                Cmb_Chargename.SelectedValueChanged -= Cmb_Chargename_SelectedValueChanged;
                Gramboo.General.Setupcombo(Cmb_Chargename, "PUR.MiscChargeMaster", "ChargeName", "ChargeId", "IsActive='True'  AND  Company_Id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
                Cmb_Chargename.SelectedValueChanged += Cmb_Chargename_SelectedValueChanged;
            }
            else
            {
                GrpPurOtherCharges.Visible = false;
                GrpPurOtherCharges.SendToBack();
                GrpPurOtherCharges.Hide();
                linkLbl_OtherCharges.Text = "Other Charges";
            }
        }

        private void Dtp_Purchasedt_ValueChanged(object sender, EventArgs e)
        {
            GetDueDate();
        }

        private void txtPaymentTerms_TextChanged(object sender, EventArgs e)
        {
            GetDueDate();
        }

        private void Btn_OtherDetails_Click(object sender, EventArgs e)
        {
            if (Cmb_Chargename.SelectedValue != null || cmbtdsName.SelectedValue != null)
            {
                txtChargeId.Text = Cmb_Chargename.SelectedValue.ToString();
                if (cmbtdsName.SelectedValue != null)
                {
                    txtTDSId.Text = cmbtdsName.SelectedValue.ToString();

                }
                else
                {
                    txtTDSId.Text = "0";
                }

                dgv_otherChg.Save();
                TXT_TDSPerc.Clear();
                CalculateTax();
            }
            txtOtherCharges.Text = dgv_otherChg.SummaryRow.SummaryCells["Amount"].Text;
        }

        private void TaxDetails_Button_Click(object sender, EventArgs e)
        {
            if (cmb_Taxname.SelectedValue != null)
            {
                txtTaxId.Text = cmb_Taxname.SelectedValue.ToString();
                dgv_TaxDetails.Save();
            }
            txtTaxPerc.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxRate"].Text;
            txtTaxAmount.Text = dgv_TaxDetails.SummaryRow.SummaryCells["Amount"].Text;
        }

        public void Wastage(object sender, EventArgs e)
        {

        }


        private void UpdateTax()
        {

            foreach (DataGridViewRow r in dgv_TaxDetails.Rows)
            {
                double totAftrDis = 0, rate = 0, amount = 0, calTax = 0, caltaxtot = 0;
                if (r.Cells["TaxRate"].Value != "" && txtAmountAfterDis.Text != "" && txtCalculatetaxAmount.Text != "")
                {
                    totAftrDis = Convert.ToDouble(txtAmountAfterDis.Text);
                    calTax = Convert.ToDouble(txtCalculatetaxAmount.Text);
                    caltaxtot = totAftrDis + calTax;
                    rate = Convert.ToDouble(r.Cells["TaxRate"].Value);
                    amount = caltaxtot * rate / 100;
                    r.Cells["Amount"].Value = amount.ToString("F2");
                }

            }

            txtTaxPerc.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxRate"].Text;
            txtTaxAmount.Text = dgv_TaxDetails.SummaryRow.SummaryCells["Amount"].Text;



        }

        private void dgv_itemDetails_SummaryCalculated(object source, EventArgs e)
        {
            Summary();
            txtGrandTotal.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["Total"].Text).ToString("f2");
            txtTotalWst.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["Wst"].Text).ToString("f2");
            txtTotalMc.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["MC"].Text).ToString("f2");

            txtTotalDiaCash.Text = dgv_itemDetails.SummaryRow.SummaryCells["DiaCash"].Text;
            txtTotalDiawt.Text = Convert.ToSingle(dgv_itemDetails.SummaryRow.SummaryCells["DiaWt"].Text).ToString("f3");
            txtTotalStWt.Text = Convert.ToSingle(dgv_itemDetails.SummaryRow.SummaryCells["StWt"].Text).ToString("f3");
            txtTotalStoneCash.Text = dgv_itemDetails.SummaryRow.SummaryCells["StoneCash"].Text;

            txtTotalMetalCash.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["MetalCash"].Text).ToString("f2");
            txtTotalNetWt.Text = Convert.ToSingle(dgv_itemDetails.SummaryRow.SummaryCells["NetWt"].Text).ToString("f3");
            txtTotalGwt.Text = Convert.ToSingle(dgv_itemDetails.SummaryRow.SummaryCells["Gwt"].Text).ToString("f3");


            discount_Amount();
            amount_AfterDiscount();
            TaxDetails_Amount();
        }




        private void Summary()
        {
            //if (SaleCurrentRow > -1)
            //{

            //    double totDiacash = 0, totDiaWt = 0, totStonecash = 0, totStoneWt = 0, totgoldcash = 0, TotGwt = 0, OrnamtDiaWt = 0, OrnamtDiaCash = 0, OrnamtStWt = 0, OrnamtStCash = 0, ornamtMetalWt = 0, OrnamtmetalCash = 0, TotalDiaWt = 0, TotalDiaCash = 0, TotalStWt = 0, TotalStCash = 0, TotalMetalWt = 0, TotalmetalCash = 0, totGwt = 0, TotalGwt = 0;
            //    foreach (DataGridViewRow r in dgv_itemDetails.Rows)
            //    {
            //        if (DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM ITM.VDiamonds WHERE ItemId=" + Convert.ToInt32(r.Cells["ItemId"].Value.ToString()))).Tables[0].Rows.Count != 0)
            //        {
            //            totDiacash += Convert.ToDouble(r.Cells["Total"].Value.ToString());
            //            totDiaWt += Convert.ToDouble(r.Cells["Gwt"].Value.ToString());

            //        }
            //        else if (DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select * FROM ITM.VStones WHERE ItemId=" + Convert.ToInt32(r.Cells["ItemId"].Value.ToString()))).Tables[0].Rows.Count != 0)
            //        {
            //            totStonecash += Convert.ToDouble(r.Cells["Total"].Value.ToString());
            //            totStoneWt += Convert.ToDouble(r.Cells["Gwt"].Value.ToString());
            //        }
            //        else
            //        {
            //            totgoldcash += Convert.ToDouble(r.Cells["Total"].Value.ToString());
            //            TotGwt += Convert.ToDouble(r.Cells["Gwt"].Value.ToString());

            //        }



            //        OrnamtDiaWt += Convert.ToDouble(r.Cells["DiaWt"].Value.ToString());
            //        OrnamtDiaCash += Convert.ToDouble(r.Cells["DiaCash"].Value.ToString());

            //        OrnamtStWt += Convert.ToDouble(r.Cells["StWt"].Value.ToString());
            //        OrnamtStCash += Convert.ToDouble(r.Cells["StoneCash"].Value.ToString());

            //        ornamtMetalWt += Convert.ToDouble(r.Cells["NetWt"].Value.ToString());
            //        OrnamtmetalCash += Convert.ToDouble(r.Cells["MetalCash"].Value.ToString());
            //        //totGwt += Convert.ToDouble(r.Cells["Gwt"].Value.ToString());

            //    }

            //    //TotalDiaCash=0,=0,=0,=0,=0;

            //    TotalDiaWt = totDiaWt + OrnamtDiaWt;
            //    TotalDiaCash = totDiacash + OrnamtDiaCash;
            //    TotalStWt = totStoneWt + OrnamtStWt;
            //    TotalStCash = totStonecash + OrnamtStCash;
            //    TotalMetalWt = TotGwt + ornamtMetalWt;
            //    TotalmetalCash = totgoldcash + OrnamtmetalCash;
            //    TotalGwt = totDiaWt + totStoneWt + TotGwt + totGwt;

            //    txtTotalDiaCash.Text = TotalDiaCash.ToString("F3");
            //    txtTotalDiawt.Text = TotalDiaWt.ToString("F3");

            //    txtTotalStoneCash.Text = TotalStCash.ToString("F3");
            //    txtTotalStWt.Text = TotalStWt.ToString("F3");

            //    txtTotalMetalCash.Text = TotalmetalCash.ToString("F3");
            //    txtTotalNetWt.Text = TotalMetalWt.ToString("F3");




            //    //txtTotalGwt.Text = TotGwt.ToString("F3");
            //    //txtTotalStWt.Text = totStoneWt.ToString("F3");
            //    //txtTotalDiawt.Text = totDiaWt.ToString("F3");
            //    //txtTotalMc.Text = dgv_PurchaseDetails.SummaryRow.SummaryCells["MC"].Text;
            //    //txtTotalWst.Text = dgv_PurchaseDetails.SummaryRow.SummaryCells["Wst"].Text;

            //}
        }





        public void enable()
        {
            if (Cmb_ItemName.SelectedValue != null)
            {
                if (dgv_itemDetails.CurrentRow != null)
                {
                    /* SAFA.Classes.Common.EnableSpecs(Convert.ToInt32(Cmb_ItemName.SelectedValue), dgv_itemDetails.CurrentRow.Cells["Diawt"],
                      dgv_itemDetails.CurrentRow.Cells["StWt"], dgv_itemDetails.CurrentRow.Cells["NetWt"],
                       dgv_itemDetails.CurrentRow.Cells["Gwt"], dgv_itemDetails.CurrentRow.Cells["MC"],
                        dgv_itemDetails.CurrentRow.Cells["McPerc"], dgv_itemDetails.CurrentRow.Cells["Wst"],
                         dgv_itemDetails.CurrentRow.Cells["WstPerc"], dgv_itemDetails.CurrentRow.Cells["Touch"], DBConn);

                     */

                }


                if (dgv_itemDetails.CurrentRow.Cells["Diawt"].ReadOnly == true)
                {
                    dgv_itemDetails.CurrentRow.Cells["DiaCash"].ReadOnly = true;
                    // dgv_itemDetails.CurrentRow.Cells["MetalRate"].ReadOnly = true;

                }

                if (dgv_itemDetails.CurrentRow.Cells["StWt"].ReadOnly == true)
                {
                    dgv_itemDetails.CurrentRow.Cells["StoneCash"].ReadOnly = true;
                    //dgv_itemDetails.CurrentRow.Cells["MetalRate"].ReadOnly = true;
                }
            }
        }









        private void dgv_otherChg_SummaryCalculated(object source, EventArgs e)
        {
            txtOtherCharges.Text = dgv_otherChg.SummaryRow.SummaryCells["Amount"].Text;
            CalculateTax();
            UpdateTax();
        }

        private void Cmb_SupplierId_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Cmb_SupplierId.SelectedValue != null)
            {
                txtPaymentTerms.Text = (DBConn.GetData(new SqlCommand("select * From  PUR.SupplierMaster Where SuppId=" + Cmb_SupplierId.SelectedValue + "")).Tables[0].Rows[0]["SuppCreditDaysLimit"]).ToString();

            }
            else
            {
                txtPaymentTerms.Text = "0";
            }
        }

        private void cmb_Taxname_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_Taxname.SelectedValue != null)
            {
                txtRate_TaxDetails.Text = (DBConn.GetData(new SqlCommand("select * From  PUR.TaxMaster Where TaxId =" + cmb_Taxname.SelectedValue + "")).Tables[0].Rows[0]["TaxRate"]).ToString();

                TaxDetails_Amount();
            }
        }
        public void TaxDetails_Amount()
        {
            double totAftrDis = 0, rate = 0, amount = 0, calTax = 0, caltaxtotal = 0;
            txtCalculatetaxAmount.Text = (string.IsNullOrEmpty(txtCalculatetaxAmount.Text.Trim()) ? "0.00" : txtCalculatetaxAmount.Text);

            if (txtRate_TaxDetails.Text != "" && txtAmountAfterDis.Text != "" && txtCalculatetaxAmount.Text != "")
            {
                totAftrDis = Convert.ToDouble(txtAmountAfterDis.Text);
                calTax = Convert.ToDouble(txtCalculatetaxAmount.Text);
                caltaxtotal = totAftrDis + calTax;
                rate = Convert.ToDouble(txtRate_TaxDetails.Text);
                amount = caltaxtotal * rate / 100;
                TxtAmount_Tax.Text = Math.Round(amount, 2).ToString("F2");
            }
            UpdateTax();
        }


        public void CalculateTax()
        {
            double Amount = 0;
            foreach (DataGridViewRow r in dgv_otherChg.Rows)
            {


                if ((bool)r.Cells["CalculateTax"].Value == true)
                {
                    Amount += Convert.ToDouble(r.Cells["Amount"].Value.ToString());
                }
            }
            txtCalculatetaxAmount.Text = Amount.ToString("F2");
        }


        private void Cmb_PurTypeId_SelectedValueChanged(object sender, EventArgs e)
        {
            PurchaseTax();
        }
        public void PurchaseTax()
        {
            if (Cmb_PurTypeId.SelectedValue != null)
            {
                dgv_TaxDetails.DataSource = this.DBConn.GetData(new SqlCommand("Select CAST(0  as bigint ) AS TransId, CAST(0  as bigint ) as PurID,TaxId,[Tax Name] as TaxName,[Tax Rate] as TaxRate,Amount from PUR.VPurchaseTaxApplicableMaster  WHERE PurTypeId=" + Cmb_PurTypeId.SelectedValue + "")).Tables[0];
                UpdateTax();
            }
        }

        public void discount_Amount()
        {
            double grand = 0, disperc = 0, disamt = 0;

            txtGrandTotal.Text = (string.IsNullOrEmpty(txtGrandTotal.Text.Trim()) ? "0.00" : txtGrandTotal.Text);
            txtDiscountPerc.Text = (string.IsNullOrEmpty(txtDiscountPerc.Text.Trim()) ? "0.00" : txtDiscountPerc.Text);
            if (txtGrandTotal.Text != "" && txtDiscountPerc.Text != "")
            {
                grand = (Convert.ToDouble(txtGrandTotal.Text));
                disperc = (Convert.ToDouble(txtDiscountPerc.Text));
                disamt = grand * (disperc / 100);
                txtDiscountAmount.Text = disamt.ToString();

            }
            else
            {
                txtDiscountAmount.Text = "0.00";
            }
        }


        // Total Amount Calculation
        public void totalamount(object sender, EventArgs e)
        {
            double totafter = 0, taxamt = 0, otherChar = 0, totAmt = 0, BillAdjAmt = 0;
            txtAmountAfterDis.Text = (string.IsNullOrEmpty(txtAmountAfterDis.Text.Trim()) ? "0.00" : txtAmountAfterDis.Text);
            txtTaxAmount.Text = (string.IsNullOrEmpty(txtTaxAmount.Text.Trim()) ? "0.00" : txtTaxAmount.Text);
            txtOtherCharges.Text = (string.IsNullOrEmpty(txtOtherCharges.Text.Trim()) ? "0.00" : txtOtherCharges.Text);


            if (txtAmountAfterDis.Text != "" && txtTaxAmount.Text != "" && txtOtherCharges.Text != "")
            {
                taxamt = Convert.ToDouble(txtTaxAmount.Text);
                totafter = Convert.ToDouble(txtAmountAfterDis.Text);
                otherChar = Convert.ToDouble(txtOtherCharges.Text);
                BillAdjAmt = Convert.ToDouble(TxtBillAdjAmt.Text);
                totAmt = ((taxamt + totafter + otherChar) - BillAdjAmt);
                txtTotalAmount.Text = Math.Round(totAmt, 2).ToString();

                //  txtTotalAmount.Text = Math.Round(Convert.ToDouble(txtAmountAfterDis.Text), 2) + Math.Round(Convert.ToDouble(txtTaxAmount.Text), 2) + Math.Round(Convert.ToDouble(txtOtherCharges.Text), 2).ToString();
                // txtTotalAmount.Text = Math.Round(Convert.ToDouble(txtAmountAfterDis.Text),2) + Math.Round(Convert.ToDouble(txtTaxAmount.Text),2) +  Math.Round( Convert.ToDouble(txtOtherCharges.Text),2).ToString();
            }
        }

        //  Amount After Discount 

        public void amount_AfterDiscount()
        {

            if (txtGrandTotal.Text != "" && txtDiscountAmount.Text != "")
                txtAmountAfterDis.Text = (Convert.ToDouble(txtGrandTotal.Text) - Convert.ToDouble(txtDiscountAmount.Text)).ToString();
        }

        private void txtGrandTotal_TextChanged(object sender, EventArgs e)
        {
            discount_Amount();
            amount_AfterDiscount();
            TaxDetails_Amount();
            TDSRate();
        }

        private void txtDiscountPerc_TextChanged(object sender, EventArgs e)
        {
            discount_Amount();
            TaxDetails_Amount();
        }

        private void txtRate_TaxDetails_TextChanged(object sender, EventArgs e)
        {
            TaxDetails_Amount();
        }

        public void NetAmount(object sender, EventArgs e)
        {
            double nettotal = 0, TCSAmt = 0, AmtTDS = 0;
            if (txtTotalAmount.Text != "")
            {
                nettotal = Convert.ToDouble(txtTotalAmount.Text) + Convert.ToSingle(txtRoundoff.Text);
                TCSAmt = Convert.ToDouble((string.IsNullOrEmpty(TxtTCSAmt.Text.Trim()) ? "0.00" : TxtTCSAmt.Text));

                AmtTDS = Convert.ToDouble((string.IsNullOrEmpty(TxtAmountTDS.Text.Trim()) ? "0.00" : TxtAmountTDS.Text));

                TxtBillAmount.Text = nettotal.ToString("f2");

                txtNetTotal.Text = (nettotal + TCSAmt - AmtTDS).ToString("F2");

                //txtRoundoff.Text = Math.Round ((Convert.ToDouble(txtNetTotal.Text) - Convert.ToDouble(txtTotalAmount.Text)),2).ToString();
                //  txtRoundoff.Text = (Convert.ToDouble(txtTotalAmount.Text) - Convert.ToDouble(txtNetTotal.Text)).ToString();
                //  txtRoundoff.Text = Math.Round(Convert.ToDouble(txtRoundoff.Text), 2).ToString();

            }
        }

        private void dgv_itemDetails_SizeChanged(object sender, EventArgs e)
        {
            GrpPurTaxDetails.Location = new Point(linkLbl_taxDetails.Location.X + linkLbl_taxDetails.Parent.Location.X,
               linkLbl_taxDetails.Parent.Location.Y + linkLbl_taxDetails.Location.Y - GrpPurTaxDetails.Height);
            GrpPurOtherCharges.Location = new Point(linkLbl_OtherCharges.Location.X + linkLbl_OtherCharges.Parent.Location.X,
            linkLbl_OtherCharges.Parent.Location.Y + linkLbl_BillAdjAmt.Location.Y - GrpPurOtherCharges.Height);
        }

        private void txtDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            amount_AfterDiscount();
        }

        private void dgv_TaxDetails_SummaryCalculated(object source, EventArgs e)
        {
            txtTaxPerc.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxRate"].Text;
            txtTaxAmount.Text = dgv_TaxDetails.SummaryRow.SummaryCells["Amount"].Text;
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            if (FillFlag)
                return;
            DataTable dt = new DataTable();
            dt = (DataTable)dgv_itemDetails.DataSource;




            dgvGRNview.EndEdit();


            foreach (DataGridViewRow r in dgvGRNview.Rows)
            {
                if (r.Cells["col_CheckBox"].Value == null)
                    r.Cells["col_CheckBox"].Value = false;
                //try
                //{
                if ((bool)((DataGridViewCheckBoxCell)r.Cells["col_CheckBox"]).Value == true)
                {

                    DataRow dr = dt.NewRow();

                    {
                        FillFlag = true;
                        dr.ItemArray = DBConn.GetData(new SqlCommand("Select PurityId,  0 as PurId,TransId,0 AS TransId,ItemID,ModelId,ProdCodeId,[Item Name],ProdCode,Purity,Touch,[Model Name],Nos,Gwt,MetalCash,DiaNo,Diawt,DiaCash,StNo,StWt,StoneCash,SetNo,SetWt,SetCash,Metal1,Metal2,NetWt,MC,Wst,Total FROM PUR.VGetGRN_OrnamentsDetails WHERE TransId=" + r.Cells["TransId"].Value.ToString())).Tables[0].Rows[0].ItemArray;
                        dt.Rows.Add(dr);



                        FillFlag = false;
                    }
                }
                //}

                //catch (Exception)
                //{
                //}

            }
            if (dgv_itemDetails.Rows.Count > 0)
            {

                foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                {
                    if (r.Cells["ItemID"].Value.ToString() == "")
                    {
                        dgv_itemDetails.Rows.Remove(r);
                    }
                }
            }
            dgv_itemDetails.RowTemplate.Height = Cmb_ItemName.Height;
            dgv_itemDetails.AllowUserToAddRows = false;
            ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());
            dgv_itemDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[0].Cells["Item Name"];

            dgv_itemDetails.BeginEdit(true);
            ShowGRNDetails();
        }

        private void txtPlatinumRate_TextChanged(object sender, EventArgs e)
        {
            //UpdateCash();
        }

        private void txtSilverRate_TextChanged(object sender, EventArgs e)
        {
            //UpdateCash();
        }

        private void txtGoldRate_TextChanged(object sender, EventArgs e)
        {
            //UpdateCash();
        }

        private void dgv_itemDetails_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {
                if (FillFlag)
                    return;
                if (dgv_itemDetails.Rows.Count == 0)
                {
                    if (dgv_itemDetails.DataSource != null)
                    {

                        ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());
                        dgv_itemDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;

                        dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[0].Cells["Item Name"];

                        dgv_itemDetails.BeginEdit(true);

                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtMetal1_TextChanged(object sender, EventArgs e)
        {
            // CalculateNetWt();
            //  calculateMetalWt();
            // UpdateCash();
        }

        private void txtMetal2_TextChanged(object sender, EventArgs e)
        {
            //CalculateNetWt();
            //calculateMetalWt();
            // UpdateCash();
        }



        private void CalculatedWstPerc()
        {
            if (dgv_itemDetails.CurrentRow == null)

                return;
            double purity = 0, touch = 0, wstperc = 0, wst = 0, gwt = 0;

            if (dgv_itemDetails.CurrentRow.Cells["PurityId"].Value.ToString() != "")
            {




                using (DataTable dt = DBConn.GetData(new SqlCommand("Select PurityValue  FROM ITM.PurityMaster WHERE PurityId =" + dgv_itemDetails.CurrentRow.Cells["PurityId"].Value + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {


                        purity = Convert.ToDouble(dt.Rows[0]["PurityValue"].ToString());

                    }

                }
            }


            else
            {
                purity = 0;
            }
            if (dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString() != "")
            {
                touch = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString());
            }
            if (purity > touch)
            {
                wstperc = purity - touch;

            }
            else
            {
                wstperc = touch - purity;

            }
            dgv_itemDetails.CurrentRow.Cells["WstPerc"].Value = wstperc.ToString();


        }


        private void calculateWst()
        {
            double wstPerc = 0, wst = 0, gwt = 0;
            if (dgv_itemDetails.CurrentRow.Cells["WstPerc"].Value.ToString() != "")
            {
                wstPerc = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["WstPerc"].Value.ToString());

            }
            if (dgv_itemDetails.CurrentRow.Cells["Gwt"].Value.ToString() != "")
            {
                gwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Gwt"].Value.ToString());

            }
            if (wstPerc != 0)
            {
                wst = gwt * wstPerc / 100;
                dgv_itemDetails.CurrentRow.Cells["Wst"].Value = wst.ToString();
            }
            else
            {
                dgv_itemDetails.CurrentRow.Cells["Wst"].Value = 0;
            }
        }


        private void calculateMc()
        {
            double McPerc = 0, Mc = 0, gwt = 0;

            if (dgv_itemDetails.CurrentRow.Cells["MCPerc"].Value.ToString() != "")
            {
                McPerc = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["MCPerc"].Value.ToString());

            }

            if (dgv_itemDetails.CurrentRow.Cells["Gwt"].Value.ToString() != "")
            {
                gwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Gwt"].Value.ToString());

            }

            if (McPerc != 0)
            {
                Mc = gwt * McPerc;
                dgv_itemDetails.CurrentRow.Cells["MC"].Value = Mc.ToString();
            }
            else
            {
                dgv_itemDetails.CurrentRow.Cells["MC"].Value = 0;
            }


        }
        private void CalculateCash(Gramboo.Controls.GrbDataGridView dgv, String WT, String RATE, String AMT)
        {
            if (dgv.CurrentRow == null)
                return;

            Double WT_ = 0.000, RATE_ = 0.00, AMT_ = 0.00;

            if (dgv.CurrentRow.Cells[WT].Value.ToString() != "")
            {
                WT_ = Convert.ToDouble(dgv.CurrentRow.Cells[WT].Value.ToString());
            }
            if (dgv.CurrentRow.Cells[RATE].Value.ToString() != "")
            {
                RATE_ = Convert.ToDouble(dgv.CurrentRow.Cells[RATE].Value.ToString());
            }

            AMT_ = Math.Round(WT_, 3) * Math.Round(RATE_, 2);

            dgv.CurrentRow.Cells[AMT].Value = Math.Round(AMT_, 2).ToString("f2");
        }

        private void calculateMcCash()
        {
            double wst = 0, Mc = 0, Rate = 0;

            if (dgv_itemDetails.CurrentRow.Cells["WstPerc"].Value.ToString() != "")
            {
                wst = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Wst"].Value.ToString());

            }

            if (dgv_itemDetails.CurrentRow.Cells["Rate"].Value.ToString() != "")
            {
                Rate = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Rate"].Value.ToString());

            }

            if (wst != 0)
            {
                Mc = Math.Round(Rate, 2) * Math.Round(wst, 2);
                dgv_itemDetails.CurrentRow.Cells["MC"].Value = Mc.ToString();
            }
            else
            {
                dgv_itemDetails.CurrentRow.Cells["MC"].Value = 0;
            }


        }

        public override bool ValidateControls()
        {
            string str = "select PurDate as entrydate from PUR.PurchaseMaster where PurId=" + TxtPurId.Text;
 

            if (base.ValidateControls())
            {

                foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                {
                    if (r.Cells["ItemId"].Value.ToString().Trim().Length >= 2)
                    {
                        r.Cells["BranchId"].Value = Convert.ToInt32(txtBranchID.Text);
                    }
                }


                Cmb_ProdCode.CheckDuplicates = true;
                foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                {
                    if (r.Cells["ProdCodeId"].Value.ToString().Trim().Length >= 2)
                    {
                        int i = 0;
                        string ProdcodeId = r.Cells["ProdCodeId"].Value.ToString();
                        String Prodcode = r.Cells["ProdCode"].Value.ToString();
                        foreach (DataGridViewRow r1 in dgv_itemDetails.Rows)
                        {
                            if (r1.Cells["ProdCodeId"].Value.ToString() == ProdcodeId)
                            {
                                i++;
                                if (i >= 2)
                                {
                                    Gramboo.General.ShowMessage("Barcode Duplication Found Please Check Barcode " + Prodcode);
                                    return false;
                                }
                            }
                        }
                    }
                }

                DateTime fromdate = Convert.ToDateTime(Dtp_Purchasedt.Text);
                DateTime todate = Convert.ToDateTime(dtp_Duedate.Text);

                double netwt = 0, stoneWt = 0, DiaWt = 0, TotalWt = 0, Gwt = 0, SetWt = 0; int rowNo;


                if (Cmb_SupplierId.SelectedValue.ToString() == null)
                {
                    Gramboo.General.ShowMessage("Select Supplier Name");
                    return false;
                }

                if (dgv_itemDetails.Rows.Count > 0)
                {
                    if (dgv_itemDetails.CurrentRow.Cells["Item Name"].Value.ToString() != "")
                    {

                        //if (Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Metal1"].Value) == 0)
                        //{
                        //    Gramboo.General.ShowMessage("column Metal1 Contain Invalid Value '0' ");
                        //    return false;
                        //}
                        //else if (txtMetal2.Text != "-")
                        //{
                        //    if (Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Metal2"].Value) == 0)
                        //    {
                        //        Gramboo.General.ShowMessage("column Metal2 Contain Invalid Value '0'");
                        //        return false;

                        //    }

                        // }

                        if (dgv_itemDetails.CurrentRow.Cells["Purity"].Value.ToString() == "")
                        {
                            Gramboo.General.ShowMessage("Enter Purity");
                            return false;

                        }
                        //if (Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Touch"].Value) == 0)
                        //{
                        //    Gramboo.General.ShowMessage("column Touch Contain Invalid Value '0' ");
                        //    return false;
                        //}


                        //else if (dgv_itemDetails.CurrentRow.Cells["Model Name"].Value.ToString() == "")
                        //{
                        //    Gramboo.General.ShowMessage("Enter Model Name");
                        //    return false;

                        //}

                        //else if (Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Nos"].Value) == 0)
                        //{
                        //    Gramboo.General.ShowMessage("column Nos Contain Invalid Value '0'");
                        //    return false;
                        //}

                        //else if (Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Gwt"].Value) == 0)
                        //{
                        //    Gramboo.General.ShowMessage("column Gwt Contain Invalid Value '0'");
                        //    return false;
                        //}
                        //foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                        //{
                        //    rowNo = Convert.ToInt16(r.Cells["col_AutoSlno"].Value.ToString());
                        //    netwt = Convert.ToDouble(r.Cells["NetWt"].Value.ToString());
                        //    stoneWt = Convert.ToDouble(r.Cells["StWt"].Value.ToString());
                        //    DiaWt = Convert.ToDouble(r.Cells["DiaWt"].Value.ToString());
                        //    Gwt = Convert.ToDouble(r.Cells["Gwt"].Value.ToString());
                        //    SetWt = Convert.ToDouble(r.Cells["SetWt"].Value.ToString());

                        //    TotalWt = netwt + stoneWt + ((DiaWt + SetWt) * Convert.ToDouble(0.2));

                        //    if (Gwt != TotalWt)
                        //    {
                        //        Gramboo.General.ShowMessage("Row No " + rowNo + " contain Invalid Gwt Value");
                        //        return false;

                        //    }

                        //}


                        if (fromdate > todate)
                        {
                            Dtp_Purchasedt.ShowMessage("Purchase Date Must be Less Than Due Date");

                            return false;

                        }

                     txtOne_Two.Text = "1"; 

                        if (txtOne_Two.Text == "")
                        {
                            txtOne_Two.Text = "1";
                            //txtOne_Two.Visible = true;
                            //txtOne_Two.Focus();

                            //return false;
                        }
                        if (txtOne_Two.Text != "1" && txtOne_Two.Text != "2" && txtOne_Two.Text != "3")
                        {
                            txtOne_Two.Visible = true;
                            txtOne_Two.Focus();
                            txtOne_Two.ShowMessage("Enter A Valid Number");
                            return false;
                        }

                    }

                }
                if (!IsEditMode)
                {
                   
                        cmb_VoucherTypeId.SelectedValue = 249;
                    
                    //}
                    //cmb_VoucherTypeId.SelectedValue = (int)SAFA.Classes.VoucherTypes.PurchaseMaster;
                    TxtVoucherNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_Purchasedt.Value,
                            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
                }

                if ((txtOne_Two.Text == "1" || txtOne_Two.Text == "3"))
                {
                    string IndirectTaxType = "";
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                    ("SELECT ISNULL(IndirectTaxType_PU,'') as IndirectTaxType  FROM PUR.SupplierMaster where SuppId=" + Cmb_SupplierId.SelectedValue + "")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            IndirectTaxType = dt.Rows[0]["IndirectTaxType"].ToString();
                        }
                    }

                    if (IndirectTaxType == "TDS") { if (CmbTdsId.SelectedValue == null) { CmbTdsId.ShowMessage("Blank Values Not Allowed...!!!"); return false; } }
                    else if (IndirectTaxType == "TCS") { if (CmbTCS.SelectedValue == null) { CmbTCS.ShowMessage("Blank Values Not Allowed...!!!"); return false; } }
                }

                //if (ADE.dgview.Rows.Count > 0)
                //{
                //    ADE.dt = Dtp_Purchasedt.Value;
                //    ADE.Amount = Convert.ToDouble(txtNetTotal.Text);
                //    ADE.RecTable = "PUR.PurchaseMaster"; ADE.RecVchNo = TxtVoucherNo.Text;
                //    ADE.RecId = TxtPurId.Text; ADE.RecCode = "PU"; ADE.Get_Details();
                //    ADE.Save();
                //}
                //else
                //{
                //    if (Message == false)
                //    {
                //        string id = TxtPurId.Text;
                //        DBConn.ExecuteSqlTransaction(new SqlCommand("Delete From ACC.AdjustingEntrieDetails where AdjId in (select AdjId from ACC.AdjustingEntrieMaster where RecTable='PUR.PurchaseMaster'and RecId=" + id.ToString() + " and  Branch_id=" + txtBranchID.Text + " and Company_id =" + txtcompId.Text + ") and  Branch_id=" + txtBranchID.Text + " and Company_id =" + txtcompId.Text), "DeleteDetailTbl");
                //        DBConn.ExecuteSqlTransaction(new SqlCommand("Delete From ACC.AdjustingEntrieMaster where RecTable='PUR.PurchaseMaster' and RecId=" + id.ToString() + " and  Branch_id=" + txtBranchID.Text + " and Company_id =" + txtcompId.Text), "DeleteMaster");
                //    }
                //}
                GrpBillAdj.Controls.Clear();

                return true;

            }
            else
            {
                return false;
            }
        }

        public override void Print()
        {

            //if (TxtPurId.Text != "")
            //{
            //    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //    if (SoftwareSettings.CompName == "MANJALI")
            //    {
            //        cr = new SAFA.Reports.PUR.MANJALI.PurchasePrintReportManjaliNew();
            //    }
            //    else
            //    {
            //        cr = new SAFA.Reports.PurchasePrintReport();
            //    }

            //    SAFA.Classes.Common.SetDatabaseLogon(cr, DBConn, false, false);
            //    cr.VerifyDatabase();
            //    cr.RecordSelectionFormula = "{VPurchaseDetailsGridReport.PurId} =" + TxtPurId.Text;

            //    cr.SetParameterValue("@Printed By", txtUserName.Text);
            //    cr.SetParameterValue("@IsContinues", false);

            //    Gramboo.Controls.GrbReport rpt = new Gramboo.Controls.GrbReport(cr);

            //    rpt.MdiParent = this.MdiParent;

            //    ((frmMain)this.MdiParent).ShowChild(rpt);

            //}
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dtp_Duedate_ValueChanged(object sender, EventArgs e)
        {
            GetDueDate();
        }

        private void txtPaymentTerms_TextChanged_1(object sender, EventArgs e)
        {
            GetDueDate();
        }

        private void dgv_itemDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgv_itemDetails.CurrentRow.Cells["Item Name"].Value.ToString() != "")
            //{
            //    if (dgv_itemDetails.CurrentRow.Cells[e.ColumnIndex].ReadOnly)
            //    {
            //        SendKeys.Send("{tab}");
            //    }
            ////}
            //if (dgv_itemDetails.CurrentRow.Cells["Total"] != null || dgv_itemDetails.CurrentRow.Cells["Total"].ToString() != "0.00")
            //{
            //   
            //}
            cmb_FloorName.Focus();
        }

        private void dgv_itemDetails_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

            //if (dgv_itemDetails.CurrentRow.Cells["Item Name"].Value.ToString() != "")
            //{

            //    using (DataTable dt = DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VItemMaster WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value + " and ItemTypeId=3")).Tables[0])
            //    {
            //        //if (dt.Rows.Count > 0)
            //        //{
            //        //    dgv_itemDetails.Columns["Model Name"].ReadOnly = false;
            //        //    //  cmb_modelname.Visible = true;
            //        //    cmb_modelname.Enabled = true;
            //        //}
            //        //else
            //        //{
            //        //    // dgv_itemDetails.Columns["Model Name"].ReadOnly = true;
            //        //    cmb_modelname.Visible = false;
            //        //    // cmb_modelname.Enabled = false;
            //        //}
            //    }
            //}
            //else
            //{
            //    cmb_modelname.Enabled = false;

            //}


            if (dgv_itemDetails.CurrentRow.Cells["Item Name"].Value.ToString() != "")
            {



                if (DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VDiamonds WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value)).Tables[0].Rows.Count != 0)
                {
                    if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "DiaCash")
                    {



                        if (Cmb_ItemName.SelectedValue != null && Cmb_ItemName.Text.Trim().Length > 0
                         && Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Nos"].Value) != 0 && Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Diawt"].Value) != 0)
                        {
                            if (dgv_itemDetails.Rows.Count > 0)
                            {
                                dgv_itemDetails.CurrentRow.Cells["StoneCash"].ReadOnly = true;
                                if (dgv_itemDetails.Rows[dgv_itemDetails.Rows.Count - 1].Cells["ItemID"].Value.ToString() != "")
                                {
                                    ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());

                                    dgv_itemDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
                                    dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[SaleCurrentRow + 1].Cells["Item Name"];



                                }
                            }
                        }
                    }


                }



                else if (DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VStones WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value)).Tables[0].Rows.Count != 0)
                {
                    if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StoneCash")
                    {

                        if (Cmb_ItemName.SelectedValue != null && Cmb_ItemName.Text.Trim().Length > 0
                         && Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Nos"].Value) != 0 && Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["StWt"].Value) != 0)
                        {
                            if (dgv_itemDetails.Rows.Count > 0)
                            {
                                dgv_itemDetails.CurrentRow.Cells["DiaCash"].ReadOnly = true;

                                if (dgv_itemDetails.Rows[dgv_itemDetails.Rows.Count - 1].Cells["ItemID"].Value.ToString() != "")
                                {
                                    ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());


                                    dgv_itemDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
                                    dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[SaleCurrentRow + 1].Cells["Item Name"];

                                }
                            }
                        }
                    }
                }


                else if (DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VMetals WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value)).Tables[0].Rows.Count != 0)
                {
                    if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Wst")
                    {

                        if (Cmb_ItemName.SelectedValue != null && Cmb_ItemName.Text.Trim().Length > 0
                        && dgv_itemDetails.CurrentRow.Cells["PurityId"].Value.ToString() != ""
                        && Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Nos"].Value) != 0 && Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Gwt"].Value) != 0
                       )
                        {
                            dgv_itemDetails.CurrentRow.Cells["DiaCash"].ReadOnly = true;
                            dgv_itemDetails.CurrentRow.Cells["StoneCash"].ReadOnly = true;
                            if (dgv_itemDetails.Rows.Count > 0)
                            {
                                if (dgv_itemDetails.Rows[dgv_itemDetails.Rows.Count - 1].Cells["ItemID"].Value.ToString() != "")
                                {
                                    ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());

                                    dgv_itemDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
                                    dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[SaleCurrentRow + 1].Cells["Item Name"];

                                }
                            }
                        }
                    }

                }

                else if (DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VOrnaments WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value)).Tables[0].Rows.Count != 0)
                {
                    if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Wst")
                    {
                        if (Cmb_ItemName.SelectedValue != null && Cmb_ItemName.Text.Trim().Length > 0
                       && dgv_itemDetails.CurrentRow.Cells["PurityId"].Value.ToString() != "" && dgv_itemDetails.CurrentRow.Cells["ModelId"].Value.ToString() != ""
                       && Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Nos"].Value) != 0 && Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Gwt"].Value) != 0
                       )
                        {
                            dgv_itemDetails.CurrentRow.Cells["DiaCash"].ReadOnly = false;
                            dgv_itemDetails.CurrentRow.Cells["StoneCash"].ReadOnly = false;
                            if (dgv_itemDetails.Rows.Count > 0)
                            {
                                if (dgv_itemDetails.Rows[dgv_itemDetails.Rows.Count - 1].Cells["ItemID"].Value.ToString() != "")
                                {
                                    ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());

                                    dgv_itemDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
                                    dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[SaleCurrentRow + 1].Cells["Item Name"];
                                }
                            }
                        }
                    }
                }

            }

        }



        private void SetComboLocation(Gramboo.Controls.GrbDataGridView dgv, Gramboo.Controls.GrbComboBox cmb, int columnindex, int rowindex)
        {

            int RepairRow = rowindex;
            cmb.Parent = dgv.Parent;
            cmb.Visible = true;
            cmb.Text = dgv.SelectedCells[0].Value.ToString();

            System.Drawing.Point p = new System.Drawing.Point();
            p = dgv.GetCellDisplayRectangle(dgv.SelectedCells[0].ColumnIndex, dgv.SelectedCells[0].RowIndex, true).Location;
            cmb.Location = new System.Drawing.Point(p.X + dgv.Parent.Location.X - 5, p.Y + dgv.Parent.Location.Y - 15);


            cmb.Size = dgv.GetCellDisplayRectangle(dgv.SelectedCells[0].ColumnIndex, dgv.SelectedCells[0].RowIndex, true).Size;
            cmb.BringToFront();
            cmb.Focus();
            cmb.DroppedDown = true;

        }

        private void dgv_itemDetails_KeyDown(object sender, KeyEventArgs e)
        {



        }

        private void dgv_itemDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //if (DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VStones WHERE ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemID"].Value)).Tables[0].Rows.Count != 0)
            //{
            //    if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StoneCash")
            //    {
            //        if (e.ColumnIndex == 5)
            //        {
            //            dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[dgv_itemDetails.Rows.Count - 1].Cells[0];
            //            Cmb_ItemName.Visible = true;
            //        }
            //        else
            //        {
            //            Cmb_ItemName.Visible = false;

            //        }
            //    }
            //}


        }

        private void Cmb_PurTypeId_SelectedValueChanged_1(object sender, EventArgs e)
        {
            if (FillFlag)
                return;
            PurchaseTax();
        }

        private void dgv_itemDetails_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {



        }

        private void btnAddNewSupplier_Load(object sender, EventArgs e)
        {

        }


        private void StoneCtwt(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double Stwt = 0.000, StwtCt = 0.000;
            if (dgv.CurrentRow == null)
                return;
            if (dgv.CurrentRow.Cells["StWt"].Value.ToString() != "")
            {
                Stwt = Convert.ToDouble(dgv.CurrentRow.Cells["StWt"].Value.ToString());
            }
            StwtCt = Math.Round(Stwt, 3) * 5;
            dgv.CurrentRow.Cells["StoneCtWt"].Value = Math.Round(StwtCt, 3).ToString("f3");
        }


        private void StoneCtwtToWT(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double Stwt = 0.000, StwtCt = 0.000;
            if (dgv.CurrentRow == null)
                return;
            if (dgv.CurrentRow.Cells["StoneCtWt"].Value.ToString() != "")
            {
                StwtCt = Convert.ToDouble(dgv.CurrentRow.Cells["StoneCtWt"].Value.ToString());
            }
            Stwt = Math.Round(StwtCt, 3) * 0.2;

            dgv.CurrentRow.Cells["Stwt"].Value = Math.Round(Stwt, 3).ToString("f3");
        }
        private void FillRowWithProdcode(Gramboo.Controls.GrbDataGridView dgv, int rowindex, string type, Int64 floorid, Int64 prodcodeid, bool Isreceipt)
        {

            int branchid = 0;
            string branchname = "";


            branchid = Convert.ToInt32(dgv.Rows[rowindex].Cells["BranchID"].Value.ToString() == "" ? "0" : dgv.Rows[rowindex].Cells["BranchID"].Value.ToString());
            branchname = dgv.Rows[rowindex].Cells["BranchName"].Value.ToString() == "" ? "" : dgv.Rows[rowindex].Cells["BranchName"].Value.ToString();

            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                     ("SELECT * FROM STK.AddtoStockTransferIssue( " + prodcodeid + "," + txtcompId.Text + "," + txtBranchID.Text + ",1,0)")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow r = ((System.Data.DataTable)dgv.DataSource).Rows[rowindex];

                    if (dgv.Rows.Count > rowindex)
                    {
                        foreach (DataGridViewColumn c in dgv.Columns)
                        {
                            if (c.IsDataBound)
                            {
                                try
                                {
                                    r[c.DataPropertyName] = dt.Rows[0][c.DataPropertyName];
                                }
                                catch (Exception ex)
                                {
                                }

                            }
                        }
                    }
                    dgv.Rows[rowindex].Cells["BranchID"].Value = branchid;
                    dgv.Rows[rowindex].Cells["BranchName"].Value = branchname;
                    r.AcceptChanges();
                    dgv.EndEdit();
                    dgv.Rows[rowindex].Cells["ProdCodeId"].Selected = true;
                    Cmb_ProdCode.Focus();
                }
                else
                {
                    Cmb_ProdCode.ShowMessage("Not In Stock");
                }
            }
        }
        private void dgv_itemDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv_itemDetails.Columns["ProdCode"].Index)
            {
                using (DataTable dtprodcode = DBConn.GetData(new SqlCommand("SELECT  STK.GetProdcodeIdFromProdCode('" + (dgv_itemDetails.Rows[e.RowIndex].Cells["Prodcode"].Value.ToString() == "" ? "0" : dgv_itemDetails.Rows[e.RowIndex].Cells["Prodcode"].Value.ToString()) + "'," + txtBranchID.Text + ")  "), "tbl").Tables[0])
                // using (DataTable dtprodcode = DBConn.GetData(new SqlCommand("select prodcodeid from stk.prodcodemaster where prodcode='" + dgv_itemDetails.Rows[e.RowIndex].Cells["Prodcode"].Value.ToString() + "'"), "tbl").Tables[0])
                {


                    dgv_itemDetails.Rows[e.RowIndex].Cells["ProdcodeId"].Value = dtprodcode.Rows[0][0].ToString();
                    //  purity();



                }

            }
            else if (e.ColumnIndex == dgv_itemDetails.Columns["ProdCodeId"].Index)
            {
                if (dgv_itemDetails[e.ColumnIndex, e.RowIndex].Value != null && dgv_itemDetails[e.ColumnIndex, e.RowIndex].Value.ToString() != "0")
                {
                    //   flag = true;

                    FillRowWithProdcode(dgv_itemDetails, e.RowIndex, "S", 0, (Int64)dgv_itemDetails.Rows[e.RowIndex].Cells["ProdcodeId"].Value, false);

                    //  flag = false;
                    bool boxitem = false;
                    using (DataTable dr = DBConn.GetData(new SqlCommand("select Itemid from ITM.ItemMaster where Itemid=" + (dgv_itemDetails.Rows[e.RowIndex].Cells["Itemid"].Value.ToString() == "" ? "0" : dgv_itemDetails.Rows[e.RowIndex].Cells["Itemid"].Value.ToString()) + " AND BoxItem=1")).Tables[0])
                    {
                        if (dr.Rows.Count > 0)
                        {
                            dgv_itemDetails.CurrentRow.Cells["GWt"].Value = "0";
                            dgv_itemDetails.CurrentRow.Cells["Nos"].Value = "0";
                            dgv_itemDetails.CurrentRow.Cells["StWt"].Value = "0";
                            boxitem = true;

                        }
                    }

                    // enable_disable(dgv_itemDetails, e.RowIndex, true);
                    if (dgv_itemDetails.Rows.Count - 1 == e.RowIndex)
                        ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());
                    dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[e.RowIndex + 1].Cells[0];
                    //dgv_itemDetails.BeginEdit(true);
                    dgv_itemDetails.Focus();
                }
            }
            if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StWt")
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "StWt")
                {
                    StoneCtwt(dgv_itemDetails);
                }
            }

            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StoneCtWt")
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "StoneCtWt")
                {
                    StoneCtwtToWT(dgv_itemDetails);
                }
            }


            if (FillFlag)
                return;

            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Item Name" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
                      dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Diawt" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StWt")
            {
                calculateNetWt();
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Ot_Stwt" ||
            dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Pr_Stwt" ||
            dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Ot_Stno" ||
            dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Pr_Stno")
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "Ot_Stwt")
                {
                    CalculateCash(dgv_itemDetails, "Ot_Stwt", "Ot_StRate", "Ot_StAmt");
                }
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "Pr_Stwt")
                {
                    CalculateCash(dgv_itemDetails, "Pr_Stwt", "Pr_StRate", "Pr_StAmt");
                }
                CalculateStoneWT(dgv_itemDetails);
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Ot_StRate")
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "Ot_StRate")
                {
                    CalculateCash(dgv_itemDetails, "Ot_Stwt", "Ot_StRate", "Ot_StAmt");
                    CalculateStoneWT(dgv_itemDetails);
                }
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Pr_StRate")
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "Pr_StRate")
                {
                    CalculateCash(dgv_itemDetails, "Pr_Stwt", "Pr_StRate", "Pr_StAmt");
                    CalculateStoneWT(dgv_itemDetails);
                }
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Nos" ||
                dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "NetWt")
            {
                MetalCash(dgv_itemDetails, false);
                CalcNetwt2(dgv_itemDetails);
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Rate")
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "Rate")
                {
                    MetalCash(dgv_itemDetails, true);
                }
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "NetWt1")
            {
                MetalCash(dgv_itemDetails, false);

                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "NetWt1")
                {
                    CalcNetwt2(dgv_itemDetails);
                }
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "NetWt2")
            {
                MetalCash(dgv_itemDetails, false);
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "MetalRate1" && dgv_itemDetails.Columns[e.ColumnIndex].ReadOnly == false)
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "MetalRate1")
                {
                    MetalCash(dgv_itemDetails, true);
                }
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "MetalRate2" && dgv_itemDetails.Columns[e.ColumnIndex].ReadOnly == false)
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "MetalRate2")
                {
                    MetalCash(dgv_itemDetails, true);
                }
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "PurityId" ||
              dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Touch")
            {
                MetalCash(dgv_itemDetails, false);
                CalculatedWstPerc();
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "WstPerc" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Gwt")
            {
                calculateWst();
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Wst" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Gwt")
            {
                calculateMcCash();
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "MCPerc")
            {
                calculateMc();
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StRate")
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "StRate")
                {
                    CalculateCash(dgv_itemDetails, "StWt", "StRate", "StoneCash");
                }
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Rate" ||
           dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StWt")
            {

                //    if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "StWt")
                //  {
                //   StoneCtwt(dgv_itemDetails);
                //   }
                StoneCash();
                calculateMcCash();



            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "DiaRate" ||
          dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Diawt")
            {
                Diacash();
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "MetalCash" ||
                dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StoneCash" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "DiaCash" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "MC" || dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Discount")
            {
                TotalAmount();
            }

            //cmb_FloorName.Focus();




        }
        private void dgvGRNview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmb_FloorName_Leave(object sender, EventArgs e)
        {

            if (cmb_FloorName.SelectedValue != null)
            {
                dgv_itemDetails.CurrentRow.Cells["FloorId"].Value = cmb_FloorName.SelectedValue.ToString();
                dgv_itemDetails.CurrentRow.Cells["Floor Name"].Value = cmb_FloorName.Text;
            }
            cmb_FloorName.Visible = false;

        }

        private void btn_Type_Click(object sender, EventArgs e)
        {
            
        }

        private void PurchaseEntry_Load(object sender, EventArgs e)
        {
            Dtp_Purchasedt.MaxDate = DateTime.Today;
        }

        private void dgv_itemDetails_RowLeave(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == (dgv_itemDetails.Columns.Count - 4))
            {


                //dgv_itemDetails.ReadOnly = false;
                //int c = dgv_itemDetails.Rows.Count;

                //for (int i = 0; i < c; i++)
                //{
                //    dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[i].Cells[3];

                //    dgv_itemDetails.BeginEdit(true);

                //}
            }


        }

        private void dgv_itemDetails_Leave(object sender, EventArgs e)
        {

        }

        private void dgv_itemDetails_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void dgv_itemDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && dgv_itemDetails.CurrentCell.OwningColumn.DisplayIndex == dgv_itemDetails.Columns.Count - 1)
            {


                if (dgv_itemDetails.Rows.Count > 0)
                {
                    //if (dgv_itemDetails.Rows[dgv_itemDetails.Rows.Count - 1].Cells["ProdcodeID"].Value.ToString() != "") //&&
                    //    //dgv_itemDetails.Rows[dgv_itemDetails.Rows.Count - 1].Cells["FloorId"].Value.ToString() != "")
                    //{
                    ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());

                    dgv_itemDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    //dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[dgv_itemDetails.Rows.Count - 1].Cells[0];
                    dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[dgv_itemDetails.Rows.Count - 1].Cells["Item Name"];
                    e.Handled = true;
                    dgv_itemDetails.BeginEdit(false);
                    //dgv_itemDetails.Focus();
                    // Cmb_ItemName.DroppedDown = true;  
                    //}
                }
            }
        }

        private void cmbbranch_Leave(object sender, EventArgs e)
        {

            AfterComboLeave(dgv_itemDetails, cmbbranch, (dgv_itemDetails.Columns.Count > 0 ? dgv_itemDetails.Columns["Branch Name"].Index : -1));


            if (cmbbranch.Text != "" && cmbbranch.SelectedValue != null)
            {


                dgv_itemDetails.CurrentRow.Cells["branchId"].Value = cmbbranch.SelectedValue.ToString();

                dgv_itemDetails.CurrentRow.Cells["Branch Name"].Value = cmbbranch.Text.ToString();
            }

            cmbbranch.Visible = false;
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void showlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Cmb_SupplierId.SelectedValue != null)
            {

                if (txt_suppBranchId.Text != "")
                {
                    if (issuepanel.Visible == false)
                    {
                        issuepanel.Size = new System.Drawing.Size(600, 400);
                        issuepanel.Parent = this;
                        issuepanel.Visible = true;
                        issuepanel.BringToFront();

                        issuepanel.Location = new Point(this.Location.X + 50, showlink.Location.Y + 10);

                        issuepanel.Show();
                        //if (cmbpartytype.SelectedIndex == 0 && cmbpartyname.SelectedValue != null)
                        //{
                        ornamentsDetails();
                        // }

                    }
                    else
                    {
                        issuepanel.Visible = false;
                        issuepanel.SendToBack();
                        issuepanel.Hide();

                    }
                }
            }
            else
            {
                Cmb_SupplierId.ShowMessage("Pls Select The Supplier");
            }
        }

        public void ornamentsDetails()
        {


            grbdgv.ShowEdit = true;
            grbdgv.AutoGenerateColumns = true;
            grbdgv.ShowSerialNo = true;
            grbdgv.ShowDelete = false;
            grbdgv.ShowEdit = false;
            grbdgv.ShowSelectCheckBox = true;
            grbdgv.HiddenDataFields = new List<string>() { "SalesId", "CustId", "CustBranchId" };
            grbdgv.SummaryColumns = new string[] { "Qty", "Gwt" };

            //SqlCommand cmd = new SqlCommand("select EntryId,VchNo,VchDate,PartyId,Nos,GWt from STK.StockPending where PartyId=" + txtBranchID.Text + " and Branch_id=" + cmbpartyname.SelectedValue);
            //cmd.CommandTimeout = 0;
            //try
            //{
            //DataConnector DataConnect = new DataConnector();
            //DataTable dt = DataConnect.GetDataTable(cmd.CommandText);
            if (rb_supplier.Checked == true)
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("select SalesId,InVNo,InvDate,CustId,Qty,GWt from  PUR.PurchaseStockPending  t1,pur.suppliermaster t2   where t1.Branch_id=t2.PurBranchId and  CustBranchId=" + txtBranchID.Text + " and  t2.SuppId=" + Cmb_SupplierId.SelectedValue)).Tables[0])
                    grbdgv.DataSource = dt;
            }
            else
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("select SalesId,InVNo,InvDate,CustId,Qty,GWt from  PUR.PurchaseStockPending  t1  where  t1.CustId=" + Cmb_SupplierId.SelectedValue)).Tables[0])
                    grbdgv.DataSource = dt;
            }

            //using (DataTable dt = DBConn.GetData(new SqlCommand("select SalesId,PurBranchId,[InVNo],CustId,InvDate,Qty,GWt from PUR.PurchaseStockPending where PurBranchId=" + txt_suppBranchId.Text + " ")).Tables[0])
            //    grbdgv.DataSource = dt;

            foreach (DataGridViewColumn c in grbdgv.Columns)
            {
                if (c.Index > 7 && c.Visible == true)
                    c.Width = 50;
                else
                {
                    if (c.DataPropertyName == "VchDate" || c.DataPropertyName == "ProdCode")
                    {
                        c.Width = 75;
                    }
                    else if (c.DataPropertyName == "Item Name")
                    {
                        c.Width = 100;
                    }
                    else if (c.DataPropertyName == "VchNo")
                    {
                        c.Width = 50;
                    }
                }
            }
            //}
            //catch (Exception ex)
            //{
            //}

        }

        private void Cmb_SupplierId_SelectedValueChanged_1(object sender, EventArgs e)
        {
            if (Cmb_SupplierId.SelectedValue != null)
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
              ("SELECT * FROM PUR.SupplierMaster  WHERE  SuppId =" + Cmb_SupplierId.SelectedValue)).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        TxtGSTNo.Text = (dt.Rows[0]["suppGstNo"] == DBNull.Value ? "0" : dt.Rows[0]["suppGstNo"].ToString());
                        CmbTdsId.SelectedValue = (dt.Rows[0]["TDS_PU"] == DBNull.Value ? "0" : dt.Rows[0]["TDS_PU"].ToString());
                        CmbTCS.SelectedValue = (dt.Rows[0]["TCS_PU"] == DBNull.Value ? "0" : dt.Rows[0]["TCS_PU"].ToString());
                        txt_suppBranchId.Text = (dt.Rows[0]["PurBranchId"] == DBNull.Value ? "" : dt.Rows[0]["PurBranchId"].ToString());
                        RoundTo = (dt.Rows[0]["RoundTo"] == DBNull.Value ? "F3" : dt.Rows[0]["RoundTo"].ToString());
                    }
                }

                Binding_BillAdj_To_Panel(Convert.ToInt64(Cmb_SupplierId.SelectedValue), Convert.ToInt64(TxtPurId.Text == "" ? "0" : TxtPurId.Text));

            }

            joborderid();
            //private void dgv_itemDetails_Leave(object sender, EventArgs e)
            //{

            //}


        }

        private void pendingissue_Btn_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt = (DataTable)dgv_itemDetails.DataSource;
            //  if(dt.Rows.Count>0)
            try
            {
                if (dt.Rows.Count > 0)
                {
                    dgv_itemDetails.Rows.RemoveAt(0);
                }
            }
            catch (Exception)
            {
            }
            grbdgv.EndEdit();
            foreach (DataGridViewRow r in grbdgv.Rows)
            {
                txtInvNo.Text = r.Cells["InVNo"].Value.ToString();
                dtp_InvDate.Text = r.Cells["InvDate"].Value.ToString();
                try
                {
                    if ((bool)(r.Cells["col_CheckBox"].Value == null ? false : r.Cells["col_CheckBox"].Value) == true)
                    {
                        //DataConnect = new DataConnector();
                        //using (System.Data.DataTable dt1 = DataConnect.GetDataTable("Select EntryId,0 as TransId,BranchId,BranchName,PurityId,ItemID, ModelId,ProdCodeId,ProdCode,[Item Name],Purity,Nos, Gwt,DiaNo,Diawt,DiaCash,StWt,StCash,NetWt,Touch,PureWt,Wst,MC,CertificationCharge,MetalCash,Amount,TotalAmount, WastageWt,ActualWt,ItemRemarks,[Mc Perc],[Wst Perc],TransId,IsReceipt,PurId,SalesVA,PurchaseVA FROM STK.VStockTransfer WHERE EntryId=" + r.Cells["EntryId"].Value.ToString()))
                        //  dgv_itemDetails.DataFields = new List<string> { "TransId", "PurId", "Slno", "ProdCodeId", "ItemID", "ModelId", "BranchId", "[Branch Name]", "[Item Name]", "[Model Name]", "PurityId", "Purity", "Touch", "Nos", "Gwt", "Rate", "StWt", "StoneCash", "Diawt", "DiaCash", "NetWt", "MetalCash", "MCPerc", "MC", "WstPerc", "Wst", "Total", "[Purity Value]" };
                        // dgv_itemDetails.HiddenDataFields = new List<string> { "TransId", "PurId", "Slno", "ItemID", "ProdCodeId", "PurityId", "BranchId", "ModelId", "[Model Name]", "Purity Value" };
                        using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand("Select  TransId, PurId,SalesId,0 as Slno, ProdCodeId, ItemID, ModelId, BranchId, [Branch Name], ItemName as [Item Name],Model as [Model Name], PurityId,PurityName as Purity,PurityValue,PurityValue as Touch,Qty as  Nos, Gwt,MetalRate as Rate,StoneWt as StWt, StoneCash, Diawt, DiaCash, NetWt, MetalCash, MCPerc, MC, WstPerc, Wst,WstCash,Total"
                    // "TransId", "PurId","SalesId","Slno", "ProdCodeId", "ItemID", "ModelId", "BranchId", "[Branch Name]", "[Item Name]", "[Model Name]", "PurityId", "Purity", "[Purity Value]","Touch", "Nos", "Gwt", "Rate", "StWt", "StoneCash", "Diawt", "DiaCash", "NetWt", "MetalCash", "MCPerc", "MC", "WstPerc", "Wst","WstCash", "Total" };
                    + " FROM Sale.VsalesDetails WHERE SalesId=" + r.Cells["SalesId"].Value.ToString())).Tables[0])
                        {


                            if (dt1.Rows.Count > 0)
                            {
                                long salesid = Convert.ToInt64(r.Cells["SalesId"].Value.ToString());

                                {
                                    foreach (DataRow row in dt1.Rows)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr.ItemArray = row.ItemArray;
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }

                        grbdgv.Rows.Remove(r);
                    }

                }
                catch (Exception ex)
                {

                }

            }
            issuepanel.Visible = false;
            issuepanel.SendToBack();
            issuepanel.Hide();
        }

        private void chkbarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (dgv_itemDetails.DataSource != null)
                dgv_itemDetails.Columns["Prodcode"].Visible = chkbarcode.Checked;

            if (chkbarcode.Checked == true)
            {
                dgv_itemDetails.HiddenDataFields.Remove("Prodcode");
            }
            else
            {
                dgv_itemDetails.HiddenDataFields.Add("Prodcode");
            }
            dgv_itemDetails.Refresh();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


            grbCheckBox1.Checked = true;
            //chkbarcode.Checked = true;
            if (Cmb_SupplierId.SelectedValue != null)
            {

                OpenFileDialog op = new OpenFileDialog();

                op.Filter = "XML Files(.xml)|*.xml";
                op.CheckFileExists = true;
                if (op.ShowDialog() == DialogResult.OK)
                {

                    if (IsValidXml(op.FileName))
                    {

                        XmlTextReader xmlreader = new XmlTextReader(op.FileName);
                        DataSet ds = new DataSet();
                        ds.ReadXml(xmlreader);
                        xmlreader.Close();

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "STK.ImportFromOrnamentTransfer";
                        cmd.Parameters.Add("@XML ", SqlDbType.Xml).Value = ds.GetXml();
                        cmd.Parameters.AddWithValue("@Company_id", txtcompId.Text);
                        cmd.Parameters.AddWithValue("@Branch_id", txtBranchID.Text);
                        cmd.Parameters.AddWithValue("@RecFrom", "SALE");

                        using (ds = DBConn.GetData(cmd))
                        {

                            DataTable dttt =
                                ds.Tables[0];
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                Gramboo.General.ShowMessage(@"The values in the import file is alredy saved in your database " +
                                    "  \n Please verify data first then try again ", "Verify Data", MessageBoxIcon.Information);
                                //    //    if (im.IsDisposed)
                                //    //    {
                                //    //        im = new Classes.ImportSettings();
                                //    //        this.Controls.Add(im);
                                //    //    }

                                //    //    im.DataSource = ds.Tables[0];

                                //    //    im.Show();
                                //    //    im.BringToFront();
                            }
                            else
                            {
                                XmlTextReader xmlreaderr = new XmlTextReader(op.FileName);
                                DataSet dt = new DataSet();
                                dt.ReadXml(xmlreaderr);
                                xmlreader.Close();

                                ((DataTable)dgv_itemDetails.DataSource).Rows.Clear();

                                foreach (DataRow dr in dt.Tables[0].Rows)
                                {
                                    DataRow r = ((DataTable)dgv_itemDetails.DataSource).NewRow();

                                    r["TransId"] = dr["TransId"];
                                    r["PurityId"] = dr["PurityId"];
                                    r["JobOrderId"] = dr["JobOrderId"];
                                    r["JobNo"] = dr["JobNo"];
                                    r["ProdCode"] = dr["ProdCode"];
                                    r["ProdCodeId"] = dr["ProdCodeId"];
                                    r["ModelId"] = dr["ModelId"];
                                    r["Branch Name"] = dr["Branch Name"];
                                    r["BranchId"] = dr["BranchId"];
                                    r["Model Name"] = r["Model Name"];
                                    r["Item Name"] = dr["ItemName"];
                                    r["Purity"] = dr["PurityName"];
                                    r["Touch"] = dr["Touch"];
                                    r["HUId"] = dr["HUId"];
                                    r["Nos"] = dr["Qty"];
                                    r["Gwt"] = dr["Gwt"];
                                    r["NetWt"] = dr["NetWt"];
                                    r["DiaCash"] = dr["DiaCash"];
                                    r["Diawt"] = dr["Diawt"];
                                    r["DiaRate"] = dr["DiaRate"];
                                    r["Rate"] = dr["MetalRate"];
                                    r["StWt"] = dr["StoneWt"];
                                    r["MetalCash"] = dr["MetalCash"];
                                    r["Wst"] = dr["Wst"];
                                    r["StoneCash"] = dr["StoneCash"];
                                    r["MCPerc"] = dr["McPerc"];
                                    r["MC"] = dr["MC"];
                                    //r["Certi_Agency"] = dr["Supplier Name"];
                                    //r["CertifNo"] = dr["CertifNo"];
                                    r["Total"] = dr["TotalAmount"];
                                    //r["IsReceipt"] = 1;
                                    //r["Id"] = 0;
                                    //r["TransId"] = 0;
                                    //r["IsActive"] = 1;
                                    r["ItemId"] = dr["ItemId"];
                                    r["Branch Name"] = dr["Branch Name"];

                                    ((DataTable)dgv_itemDetails.DataSource).Rows.Add(r);


                                }

                                importfile = op.FileName;
                                chkbarcode.Checked = true;
                            }

                        }

                    }
                }
                else
                {
                    Gramboo.General.ShowMessage("Invalid File", "Import", MessageBoxIcon.Information);
                }

            }
        }
        public bool IsValidXml(string path)
        {
            XDocument doc = XDocument.Load(path);
            var settings = doc.Element("NewDataSet");
            return doc.Element("NewDataSet") != null &&
            doc.Element("NewDataSet").Elements("OrnamentTransfer").Any();
        }
        public override bool Save()
        {
            if (importfile == null)
            {
                importfile = "";
            }
            if (importfile.Length != 0)
            {
                if (!System.IO.File.Exists(System.IO.Path.GetDirectoryName(importfile) + "\\" + System.IO.Path.GetFileNameWithoutExtension(importfile) + "Splitup.xml"))
                {
                    Gramboo.General.ShowMessage("Cannot Find Splitup file");
                    return false;
                }

                if (!System.IO.File.Exists(System.IO.Path.GetDirectoryName(importfile) + "\\" + System.IO.Path.GetFileNameWithoutExtension(importfile) + "SplitupDetail.xml"))
                {
                    DialogResult d = Gramboo.General.ShowMessage(
                       " Cannot Find SplitupDetail file!!\n\n" +
                       " 1. Press 'Yes' to Continue without SplitupDetails \n" +
                       " 2. Press 'No' to Cancel This Action \n", "file not found", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                    if (d == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            if (base.Save())
            {
                if (grbCheckBox1.Checked == true)
                {

                    if (importfile.Length == 0)
                        return true;

                    if (System.IO.File.Exists(System.IO.Path.GetDirectoryName(importfile) + "\\" + System.IO.Path.GetFileNameWithoutExtension(importfile) + "Splitup.xml"))
                    {

                        XmlTextReader xmlreade = new XmlTextReader(System.IO.Path.GetDirectoryName(importfile) + "\\" + System.IO.Path.GetFileNameWithoutExtension(importfile) + "Splitup.xml");
                        DataSet s = new DataSet();
                        s.ReadXml(xmlreade);
                        xmlreade.Close();

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "STK.ImportFromOrnamentTransferSplit";
                        cmd.Parameters.Add("@XML ", SqlDbType.Xml).Value = s.GetXml();
                        cmd.Parameters.AddWithValue("@Company_id", txtcompId.Text);
                        cmd.Parameters.AddWithValue("@Branch_id", txtBranchID.Text);
                        cmd.Parameters.AddWithValue("@RecFrom", "SALE");

                        using (s = DBConn.GetData(cmd))
                        {
                            DataTable dttt =
                                   s.Tables[0];
                            if (s.Tables[0].Rows.Count > 0)
                            {
                                Gramboo.General.ShowMessage(@"The values in the import file is alredy saved in your ProdCodeMaster " +
                                      "  \n Please verify data first then try again ", "Verify Data", MessageBoxIcon.Information);
                                return false;
                                //        if (im.IsDisposed)
                                //        {
                                //            im = new Classes.ImportSettings();
                                //            this.Controls.Add(im);
                                //        }

                                //        im.DataSource = ds.Tables[0];

                                //        im.Show();
                                //        im.BringToFront();
                            }
                            else
                            {

                                XmlTextReader xmlreaderr = new XmlTextReader(System.IO.Path.GetDirectoryName(importfile) + "\\" + System.IO.Path.GetFileNameWithoutExtension(importfile) + "Splitup.xml");
                                DataSet dt = new DataSet();
                                dt.ReadXml(xmlreaderr);
                                xmlreaderr.Close();
                                cmd = new SqlCommand();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "STK.InsertSplitup";
                                cmd.Parameters.Add("@XML ", SqlDbType.Xml).Value = dt.GetXml();
                                cmd.Parameters.AddWithValue("@Company_id", txtcompId.Text);
                                cmd.Parameters.AddWithValue("@Branch_id", txtBranchID.Text);
                                cmd.Parameters.AddWithValue("@RecFrom", "SALE");

                                DBConn.GetData(cmd, "Insert", "");
                                Gramboo.General.ShowMessage("product code is successfully inserted");
                            }

                        }

                    }
                    if (System.IO.File.Exists(System.IO.Path.GetDirectoryName(importfile) + "\\" + System.IO.Path.GetFileNameWithoutExtension(importfile) + "SplitupDetail.xml"))
                    {

                        XmlTextReader xmlreade = new XmlTextReader(System.IO.Path.GetDirectoryName(importfile) + "\\" + System.IO.Path.GetFileNameWithoutExtension(importfile) + "SplitupDetail.xml");
                        DataSet s = new DataSet();
                        s.ReadXml(xmlreade);
                        xmlreade.Close();

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "STK.ImportFromOrnamentTransferSplitDetails";
                        cmd.Parameters.Add("@XML ", SqlDbType.Xml).Value = s.GetXml();
                        cmd.Parameters.AddWithValue("@Company_id", txtcompId.Text);
                        cmd.Parameters.AddWithValue("@Branch_id", txtBranchID.Text);
                        cmd.Parameters.AddWithValue("@RecFrom", "SALE");

                        using (s = DBConn.GetData(cmd))
                        {
                            DataTable dttt =
                                   s.Tables[0];
                            if (s.Tables[0].Rows.Count > 0)
                            {
                                Gramboo.General.ShowMessage(@"The values in the import file is alredy saved in your ProdCodeMasterDetail " +
                                      "  \n Please verify data first then try again ", "Verify Data", MessageBoxIcon.Information);
                                return false;
                                //        if (im.IsDisposed)
                                //        {
                                //            im = new Classes.ImportSettings();
                                //            this.Controls.Add(im);
                                //        }

                                //        im.DataSource = ds.Tables[0];

                                //        im.Show();
                                //        im.BringToFront();
                            }
                            else
                            {

                                XmlTextReader xmlreaderr = new XmlTextReader(System.IO.Path.GetDirectoryName(importfile) + "\\" + System.IO.Path.GetFileNameWithoutExtension(importfile) + "SplitupDetail.xml");
                                DataSet dt = new DataSet();
                                dt.ReadXml(xmlreaderr);
                                xmlreaderr.Close();
                                cmd = new SqlCommand();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "STK.InsertSplitupDetail";
                                cmd.Parameters.Add("@XML ", SqlDbType.Xml).Value = dt.GetXml();
                                cmd.Parameters.AddWithValue("@Company_id", txtcompId.Text);
                                cmd.Parameters.AddWithValue("@Branch_id", txtBranchID.Text);
                                cmd.Parameters.AddWithValue("@RecFrom", "SALE");

                                DBConn.GetData(cmd, "Insert", "");
                                Gramboo.General.ShowMessage("product code Detail is successfully inserted");
                            }

                        }

                    }

                }

                
                Message = true;

                if (BillAdj == true && rbtGrp1.Value == "2")
                {
                    bool BillWise = false;
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                      ("select isnull(BillWise,0) as BillWise from ACC.Vtds where PartyType='Supplier' and PartyId =" + Cmb_SupplierId.SelectedValue + "")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            BillWise = Convert.ToBoolean(dt.Rows[0]["BillWise"].ToString());
                            if (BillWise == false) { return true; }
                        }
                    }

                    DialogResult R = Gramboo.General.ShowMessage("Do you want to adjust this bill amount with receivable bill?", "Adjustment", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                    
                }

                BillAdj = true;


                return true;

            }
            else
            {
                return false;
            }
        }
        //public void Closed(object sender, SAFA.Forms.COM.DocumentUploader.CloseClickEventArgs e)
        //{
        //    if (BillAdj == true && rbtGrp1.Value == "2")
        //    {
        //        bool BillWise = false;
        //        using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
        //                                          ("select isnull(BillWise,0) as BillWise from ACC.Vtds where PartyType='Supplier' and PartyId =" + Cmb_SupplierId.SelectedValue + "")).Tables[0])
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                BillWise = Convert.ToBoolean(dt.Rows[0]["BillWise"].ToString());
        //                if (BillWise == false) { Init(); return; }
        //            }
        //        }

        //        DialogResult R = Gramboo.General.ShowMessage("Do you want to adjust this bill amount with receivable bill?", "Adjustment", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

        //        if (R == DialogResult.Yes)
        //        {
        //            SAFA.Forms.ACC.AdjustingEntries AE = new SAFA.Forms.ACC.AdjustingEntries();
        //            AE.CloseClick += new SAFA.Forms.ACC.AdjustingEntries.CloseClickEventHandler(Closed);
        //            AE.dt = Dtp_Purchasedt.Value;
        //            AE.Amount = Convert.ToDouble(txtNetTotal.Text);
        //            AE.RecTable = "PUR.PurchaseMaster"; AE.RecVchNo = TxtVoucherNo.Text;
        //            AE.RecId = TxtPurId.Text; AE.RecCode = "PU";
        //            AE.BillType = "R"; AE.PartyType = "Supplier";
        //            AE.PartyId = Convert.ToInt64(Cmb_SupplierId.SelectedValue);
        //            AE.ShowDialog();
        //        }
        //    }
        //    else { Init(); };
        //    BillAdj = true;
        //}
        //public void Closed(object sender, SAFA.Forms.ACC.AdjustingEntries.CloseClickEventArgs e)
        //{
        //    Init();
        //}
        private void Cmb_ProdCode_Leave(object sender, EventArgs e)
        {
            if (Cmb_ProdCode.SelectedValue != null)
            {
                dgv_itemDetails.CurrentRow.Cells["ProdCode"].Value = Cmb_ProdCode.SelectedValue.ToString();
                dgv_itemDetails.CurrentRow.Cells["ProdCodeId"].Value = Cmb_ProdCode.Text;
            }
            Cmb_ProdCode.Visible = false;
        }
        private void CmbJobOrderId_Leave(object sender, EventArgs e)
        {

            if (CmbJobNo.Text != "" && CmbJobNo.SelectedValue != null)
            {


                dgv_itemDetails.CurrentRow.Cells["JobOrderId"].Value = CmbJobNo.SelectedValue.ToString();

                dgv_itemDetails.CurrentRow.Cells["JobNo"].Value = CmbJobNo.Text.ToString();
            }

            CmbJobNo.Visible = false;
        }

        void joborderid()
        {
            if (txtBranchID.Text == "")
                return;
            string str;
            SqlCommand cmd = new SqlCommand();
            if (Cmb_SupplierId.SelectedValue != null)
            {
                str = "Select JobOrderID,JobNo from STK.FunSmithIssuePending(" + Cmb_SupplierId.SelectedValue + "," + txtcompId.Text + "," + txtBranchID.Text + ") ";
            }
            else
            {
                str = "Select JobOrderID,JobNo from STK.FunSmithIssuePending('0'," + txtcompId.Text + "," + txtBranchID.Text + ") ";
            }

            cmd.CommandText = str;
            CmbJobNo.DisplayMember = "JobNo";
            CmbJobNo.ValueMember = "JobOrderID";
            CmbJobNo.DataSource = DBConn.GetData(cmd, "JobNo").Tables[0];

        }

        private void CmbJobNo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CmbJobNo_Leave(object sender, EventArgs e)
        {
            if (CmbJobNo.SelectedValue != null)
            {
                dgv_itemDetails.CurrentRow.Cells["JobOrderId"].Value = CmbJobNo.SelectedValue.ToString();
                dgv_itemDetails.CurrentRow.Cells["JobNo"].Value = CmbJobNo.Text;
            }
            CmbJobNo.Visible = false;
        }

        private void txtOne_Two_TextChanged(object sender, EventArgs e)
        {
            
                cmb_VoucherTypeId.SelectedValue = 5; 
                
            txtOne_Two.Visible = false;
        }

        private void PurchaseEntry_KeyDown(object sender, KeyEventArgs e)
        {
             

            if (e.KeyCode == Keys.Escape)
            {
                if (GrpBillAdj.Visible == true)
                {
                    GrpBillAdj.Visible = false;
                }
                if (GrpPurTaxDetails.Visible == true)
                {
                    GrpPurTaxDetails.Visible = false;
                }
                if (GrpPurOtherCharges.Visible == true)
                {
                    GrpPurOtherCharges.Visible = false;
                }
                if (Pan_Mc_Rate_Calc.Visible == true)
                {
                    Pan_Mc_Rate_Calc.Visible = false;
                }
            }
        }

        private void cmbmetaltype_SelectedValueChanged(object sender, EventArgs e)
        {
            Gramboo.General.Setupcombo(Cmb_ItemName, "ITM.VItemMaster", "[Item Name]", "ItemId", "[Is Active]='True' and billGroup='" + cmbmetaltype.SelectedValue + "'");
            SetColumns();
        }
        public void Othertaxcalu()
        {
            float SGSTper = 0, CGSTper = 0, IGSTper = 0, CesssPer = 0, Amt = 0;
            SGSTper = Convert.ToSingle(label36.Text == "" ? "0" : label36.Text);
            CGSTper = Convert.ToSingle(label37.Text == "" ? "0" : label37.Text);
            IGSTper = Convert.ToSingle(label38.Text == "" ? "0" : label38.Text);
            CesssPer = Convert.ToSingle(label49.Text == "" ? "0" : label49.Text);

            if (grbTextBox8.Text != "")
            {
                Amt = Convert.ToSingle(grbTextBox8.Text == "" ? "0" : grbTextBox8.Text);
                grbTextBox1.Text = (Amt * SGSTper / 100).ToString("f2");
                grbTextBox6.Text = (Amt * CGSTper / 100).ToString("f2");
                TxtCESS.Text = (Amt * CesssPer / 100).ToString("f2");
                if (IGSTper != 0)
                {
                    grbTextBox7.Text = (Amt * IGSTper / 100).ToString("f2");
                }
                //if(CesssPer!=0)

                //{
                //    TxtCESS.Text = (Amt * CesssPer / 100).ToString("f2");
                //}
            }
        }
        public void OthertaxAmtcalu()
        {
            double SGSTAmt = 0, CGSTAmt = 0, IGSTAmt = 0, CessAmt = 0, TotlAmt = 0, finalAmt = 0, Amount = 0, tdsperc = 0, TdsAmount = 0;
            float amt = 0;
            amt = Convert.ToSingle(grbTextBox8.Text == "" ? "0" : grbTextBox8.Text);
            
            Amount = Convert.ToSingle(txttax.Text == "" ? "0" : txttax.Text);
            tdsperc = Convert.ToSingle(TXT_TDSPerc.Text == "" ? "0" : TXT_TDSPerc.Text);
            SGSTAmt = Convert.ToSingle(grbTextBox1.Text == "" ? "0" : grbTextBox1.Text);
            CGSTAmt = Convert.ToSingle(grbTextBox6.Text == "" ? "0" : grbTextBox6.Text);
            IGSTAmt = Convert.ToSingle(grbTextBox7.Text == "" ? "0" : grbTextBox7.Text);
            finalAmt = Convert.ToSingle(grbTextBox8.Text == "" ? "0" : grbTextBox8.Text);
            CessAmt = Convert.ToSingle(TxtCESS.Text == "" ? "0" : TxtCESS.Text);
            TdsAmount = Convert.ToInt64(Amount * (tdsperc / 100));
            txt_tdsamt.Text = TdsAmount.ToString("f0");
            TotlAmt = (SGSTAmt + CGSTAmt + IGSTAmt + CessAmt + finalAmt - TdsAmount);
            txtAmount_Charge.Text = TotlAmt.ToString("f2");

            if ((TdsAmount > (amt * (tdsperc / 100))) && msgval == true)
            {
                Gramboo.General.ShowMessage("TDS will be deducted for the previous transaction");
                msgval = false;
            }
        }
        public void taxrateOtherch()
        {
            if (Cmb_SupplierId.SelectedValue != null)
            {
                if (txtcompId.Text != "")
                {
                    using (DataTable t = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                       ("select CGSTper,Sgstper,IGSTper,Cessper from PUR.MiscChargeMaster where ChargeId='" + Cmb_Chargename.SelectedValue + "'")).Tables[0])
                    using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                          ("select Comp_StateCode from SYST.BranchMaster where Branchid= " + txtBranchID.Text + "")).Tables[0])
                    using (DataTable dtt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                         ("select statecode from pur.suppliermaster where SuppId= '" + Cmb_SupplierId.SelectedValue + "'")).Tables[0])
                        if (t.Rows.Count > 0)
                        {
                            if (dtt.Rows[0]["statecode"].ToString().ToString() == dt.Rows[0]["Comp_StateCode"].ToString())
                            {
                                label36.Text = t.Rows[0]["CGSTper"].ToString();
                                label37.Text = t.Rows[0]["Sgstper"].ToString();

                                txtSgstPec.Text = t.Rows[0]["Sgstper"].ToString();
                                txtCgstPerc.Text = t.Rows[0]["CGSTper"].ToString();
                                TxtCessPec.Text = t.Rows[0]["Cessper"].ToString();
                                label49.Text = t.Rows[0]["Cessper"].ToString();







                                label38.Text = "0.00";
                            }
                            else
                            {
                                label38.Text = t.Rows[0]["IGSTper"].ToString();
                                txtIgstPerc.Text = t.Rows[0]["IGSTper"].ToString();
                                label49.Text = "0.00";

                                label37.Text = "0.00";
                                label36.Text = "0.00";
                            }
                            //txtTaxRatePerc.Text = t.Rows[0]["IGSTper"].ToString();
                        }
                }
            }
        }

        private void Cmb_Chargename_SelectedValueChanged(object sender, EventArgs e)
        {
            taxrateOtherch();
            Othertaxcalu();
            OthertaxAmtcalu();
            cmbtdsName.SelectedValue = 0;
            cmbtdsName.Text = "";


        }

        private void grbTextBox8_TextChanged(object sender, EventArgs e)
        {
            Othertaxcalu();
            OthertaxAmtcalu();
        }

        private void cmbtdsName_SelectedValueChanged(object sender, EventArgs e)
        {
            TdsRate();
            msgval = true;
        }
        public void TdsRate()
        {

            if (Cmb_Chargename.SelectedValue == null)
                return;
            if (cmbtdsName.SelectedValue != null && cmbtdsName.SelectedValue.ToString().Trim().Length > 3)
            {
                Int64 LedgerId = 0; String PartyType = "";

                using (DataTable dtSupp = DBConn.GetData(new SqlCommand("select Isnull(Acc_LedgerID,0) as Acc_LedgerID from PUR.SupplierMaster where SuppId='" + Cmb_SupplierId.SelectedValue.ToString() + "'")).Tables[0])
                {
                    if (dtSupp.Rows.Count > 0)
                    {
                        PartyType = "Supplier";
                        LedgerId = Convert.ToInt64(dtSupp.Rows[0]["Acc_LedgerID"].ToString());
                    }
                }

                Double othind = 0.00, ind = 0.00, nopan = 0.00;
                using (DataTable dt = DBConn.GetData(new SqlCommand("select top 1 Otherthanindividual,Individual,NoPanno FROM GEN.TDSDetails  where TdsId='" + cmbtdsName.SelectedValue + "' and date<='" + Dtp_Purchasedt.Value + "' order by date desc")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        ind = Convert.ToDouble(dt.Rows[0]["Individual"].ToString());
                        othind = Convert.ToDouble(dt.Rows[0]["Otherthanindividual"].ToString());
                        nopan = Convert.ToDouble(dt.Rows[0]["NoPanno"].ToString());

                    }
                }
                String HasTds = "", HasPan = "", IsInd = "";
                using (DataTable dt = DBConn.GetData(new SqlCommand("select isnull(HasTDS,'False') as HasTDS,isnull(HasPan,'False') as HasPan,isnull(IsIndividual,'False') as IsIndividual from ACC.Vtds WHERE Acc_LedgerID=" + LedgerId.ToString() + " and PartyType='" + PartyType + "'")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        HasTds = dt.Rows[0]["HasTDS"].ToString();
                        HasPan = dt.Rows[0]["HasPan"].ToString();
                        IsInd = dt.Rows[0]["IsIndividual"].ToString();

                        if (HasTds == "True" && HasPan == "False")
                        {
                            TXT_TDSPerc.Text = nopan.ToString("F2");
                        }
                        else if (HasTds == "True" && HasPan == "True" && IsInd == "True")
                        {
                            TXT_TDSPerc.Text = ind.ToString("F2");
                        }
                        else if (HasTds == "True" && HasPan == "True" && IsInd == "False")
                        {
                            TXT_TDSPerc.Text = othind.ToString("F2");
                        }
                        OthertaxAmtcalu();
                    }
                }
            }
        }


        private void TXT_TDSPerc_TextChanged(object sender, EventArgs e)
        {
            if (cmbtdsName.SelectedValue != "0")
            {
                //CalcTax();
                OthertaxAmtcalu();
            }
            else
            {
                TXT_TDSPerc.Clear();
            }
            TdsRate();
        }

        private void txt_tdsamt_TextChanged(object sender, EventArgs e)
        {
            TdsRate();
        }

        private void cmbtdsName_TextChanged(object sender, EventArgs e)
        {
            if (cmbtdsName.Text == "" || cmbtdsName.Text == null)
            {
                TXT_TDSPerc.Text = "";
                txt_tdsamt.Text = "0";
                OthertaxAmtcalu();
            }
        }

        //private void BtnDocAdd_Click(object sender, EventArgs e)
        //{

        //    SAFA.Forms.COM.DocumentUploader du = new SAFA.Forms.COM.DocumentUploader();
        //    du.CloseClick += new SAFA.Forms.COM.DocumentUploader.CloseClickEventHandler(DocBtn);
        //    du.RecTable = "PUR.PurchaseMaster";
        //    du.RecId = TxtPurId.Text;
        //    du.ShowDialog();
        //}
        //public void DocBtn(object sender, SAFA.Forms.COM.DocumentUploader.CloseClickEventArgs e)
        //{
        //    DocButtonChange();
        //}
        //void DocButtonChange()
        //{
        //    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
        //                                      ("select DocId From GEN.DocumentMaster where RecTable='PUR.PurchaseMaster'and RecId=" + TxtPurId.Text + " and Company_Id=" + txtcompId.Text + " and Branch_Id=" + txtBranchID.Text + "")).Tables[0])
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            BtnDocAdd.Image = SAFA.Properties.Resources.Upload_or_View_Documents;
        //            BtnDocAdd.Text = " Upload/View Document";
        //        }
        //        else
        //        {
        //            BtnDocAdd.Image = SAFA.Properties.Resources.Upload_Documents;
        //            BtnDocAdd.Text = " Upload Document";
        //        }
        //    }
        //}

        void TCS()
        {
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                              ("select Isnull(TCSId,0) as TCSId ,Isnull(TCSPerc,0) as TCSPerc,Isnull(TCSAmount,0) as TCSAmount,ISNULL(MetTaxIncu,0) as MetTaxIncu,ISNULL(MetUnFixed,0) as MetUnFixed,VoucherTypeId,VchNo  from [PUR].[PurchaseMaster] where PurId=" + TxtPurId.Text + " and Company_Id=" + txtcompId.Text + " and Branch_Id=" + txtBranchID.Text + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    CmbTCS.SelectedValue = dt.Rows[0]["TCSId"].ToString();
                    TxtTCSPerc.Text = dt.Rows[0]["TCSPerc"].ToString();
                    TxtTCSAmt.Text = dt.Rows[0]["TCSAmount"].ToString();
                    ChkTaxIncl.Checked = Convert.ToBoolean(dt.Rows[0]["MetTaxIncu"].ToString());
                    ChkUnFixed.Checked = Convert.ToBoolean(dt.Rows[0]["MetUnFixed"].ToString());
                    TxtVoucherNo.Text = dt.Rows[0]["VchNo"].ToString();
                    cmb_VoucherTypeId.SelectedValue = dt.Rows[0]["VoucherTypeId"].ToString();

                }
            }
        }

        void TDS()
        {
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                              ("select Isnull(TDS,0) as TDS ,Isnull(PercTDS,0) as PercTDS,Isnull(AmountTDS,0) as AmountTDS,ISNULL(RateCuttingMode,'') as RateCuttingMode  from [PUR].[PurchaseMaster] where PurId=" + TxtPurId.Text + " and Company_Id=" + txtcompId.Text + " and Branch_Id=" + txtBranchID.Text + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    CmbTdsId.SelectedValue = dt.Rows[0]["TDS"].ToString();
                    TxtPercTDS.Text = dt.Rows[0]["PercTDS"].ToString();
                    TxtAmountTDS.Text = dt.Rows[0]["AmountTDS"].ToString();
                    string RateCuttingMode = dt.Rows[0]["RateCuttingMode"].ToString();
                    if (RateCuttingMode == "99.5")
                    {
                        Rb99_5.Checked = true; dgv_itemDetails.Columns["WT_99_5"].Visible = true;
                        dgv_itemDetails.Columns["WT_100"].Visible = true;
                        dgv_itemDetails.Columns["WT_92"].Visible = false;
                        dgv_itemDetails.Columns["MC_WT"].Visible = false;
                        dgv_itemDetails.Columns["MC"].Visible = false;
                    }
                    else if (RateCuttingMode == "92+MC")
                    {
                        Rb92_MC.Checked = true; dgv_itemDetails.Columns["WT_99_5"].Visible = true;
                        dgv_itemDetails.Columns["WT_100"].Visible = true;
                        dgv_itemDetails.Columns["WT_92"].Visible = true;
                        dgv_itemDetails.Columns["MC_WT"].Visible = true;
                        dgv_itemDetails.Columns["MC"].Visible = true;
                    }
                }
            }
        }

        //private void BtnBillAdj_Click(object sender, EventArgs e)
        //{
        //    //SAFA.Forms.ACC.AdjustingEntries AE = new SAFA.Forms.ACC.AdjustingEntries();
        //    //AE.CloseClick += new SAFA.Forms.ACC.AdjustingEntries.CloseClickEventHandler(BillAdjBtn);
        //    //AE.dt = Dtp_Purchasedt.Value;
        //    //AE.Amount = Convert.ToDouble(txtNetTotal.Text);
        //    //AE.RecTable = "PUR.PurchaseMaster"; AE.RecVchNo = TxtVoucherNo.Text;
        //    //AE.RecId = TxtPurId.Text; AE.RecCode = "PU";
        //    //AE.BillType = "R"; AE.PartyType = "Supplier";
        //    //AE.PartyId = Convert.ToInt64(Cmb_SupplierId.SelectedValue);
        //    //AE.Fill(TxtPurId.Text);
        //    //AE.ShowDialog();
        //}
        //public void BillAdjBtn(object sender, SAFA.Forms.ACC.AdjustingEntries.CloseClickEventArgs e)
        //{
        //    BillAdjButtonChange();
        //}
        //void BillAdjButtonChange()
        //{
        //    ToolTip toolTip1 = new ToolTip();
        //    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
        //                                      ("select AdjId From ACC.AdjustingEntrieMaster where RecTable='PUR.PurchaseMaster'and RecId=" + TxtPurId.Text + " and Company_Id=" + txtcompId.Text + " and Branch_Id=" + txtBranchID.Text + "")).Tables[0])
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            BtnBillAdj.Image = SAFA.Properties.Resources.GreenBill_Icon;
        //            toolTip1.SetToolTip(BtnBillAdj, "View/Modify Adjusted Bills");
        //        }
        //        else
        //        {
        //            BtnBillAdj.Image = SAFA.Properties.Resources.RedBill_Icon;
        //            toolTip1.SetToolTip(BtnBillAdj, "Bill Adjustment");
        //        }
        //    }
        //}

        public override bool Delete()
        {
            string id = TxtPurId.Text;
            string str = "select PurDate as entrydate from PUR.PurchaseMaster where PurId=" + TxtPurId.Text;

             

            if (base.Delete())
            {
                DBConn.ExecuteSqlTransaction(new SqlCommand("Delete From ACC.AdjustingEntrieDetails where AdjId in (select AdjId from ACC.AdjustingEntrieMaster where RecTable='PUR.PurchaseMaster'and RecId=" + id.ToString() + " and  Branch_id=" + txtBranchID.Text + " and Company_id =" + txtcompId.Text + ") and  Branch_id=" + txtBranchID.Text + " and Company_id =" + txtcompId.Text), "DeleteDetailTbl");
                DBConn.ExecuteSqlTransaction(new SqlCommand("Delete From ACC.AdjustingEntrieMaster where RecTable='PUR.PurchaseMaster' and RecId=" + id.ToString() + " and  Branch_id=" + txtBranchID.Text + " and Company_id =" + txtcompId.Text), "DeleteMaster");
                return true;
            }
            else { return false; }
        }
        public void TCSRate()
        {
            TxtTCSPerc.Clear(); TCSAmount();
            if (Cmb_SupplierId.SelectedValue == null)
                return;
            if (CmbTCS.Text == "")
                return;
            if (CmbTCS.SelectedValue != null && CmbTCS.SelectedValue.ToString().Trim().Length > 3)
            {
                Int64 LedgerId = 0; String PartyType = "";
                using (DataTable dtSupp = DBConn.GetData(new SqlCommand("select Isnull(Acc_LedgerID,0) as Acc_LedgerID from PUR.SupplierMaster where SuppId='" + Cmb_SupplierId.SelectedValue.ToString() + "'")).Tables[0])
                {
                    if (dtSupp.Rows.Count > 0)
                    {
                        PartyType = "Supplier";
                        LedgerId = Convert.ToInt64(dtSupp.Rows[0]["Acc_LedgerID"].ToString());
                    }
                }
                Double recvrate = 0.00, othind = 0.00, ind = 0.00, nopan = 0.00;
                using (DataTable dt = DBConn.GetData(new SqlCommand("select top 1 ReceivableRate,Otherthanindividual,Individual,NoPanno FROM GEN.TCSDetails  where TcsId='" + CmbTCS.SelectedValue + "' and date<='" + dtp_InvDate.Value + "'order by date desc")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        ind = Convert.ToDouble(dt.Rows[0]["Individual"].ToString());
                        othind = Convert.ToDouble(dt.Rows[0]["Otherthanindividual"].ToString());
                        nopan = Convert.ToDouble(dt.Rows[0]["NoPanno"].ToString());
                    }
                }
                String HasTds = "", HasPan = "", IsInd = "";
                using (DataTable dt = DBConn.GetData(new SqlCommand("select isnull(HasTDS,'False') as HasTDS,isnull(HasPan,'False') as HasPan,isnull(IsIndividual,'False') as IsIndividual from ACC.Vtds WHERE Acc_LedgerID=" + LedgerId.ToString() + " and PartyType='" + PartyType + "'")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        HasPan = dt.Rows[0]["HasPan"].ToString();
                        IsInd = dt.Rows[0]["IsIndividual"].ToString();

                        if (HasPan == "False")
                        {
                            TxtTCSPerc.Text = nopan.ToString("F3");
                        }
                        else if (HasPan == "True" && IsInd == "True")
                        {
                            TxtTCSPerc.Text = ind.ToString("F3");
                        }
                        else if (HasPan == "True" && IsInd == "False")
                        {
                            TxtTCSPerc.Text = othind.ToString("F3");
                        }
                        TCSAmount();
                    }
                }
            }
        }

        private void TCSAmount()
        {
            Double NetAmt = 0, TCSPerc = 0, TCSAmt = 0;
            NetAmt = Convert.ToDouble((string.IsNullOrEmpty(txtNetTotal.Text.Trim()) ? "0.00" : txtNetTotal.Text));
            TCSPerc = Convert.ToDouble((string.IsNullOrEmpty(TxtTCSPerc.Text.Trim()) ? "0.00" : TxtTCSPerc.Text));
            TCSAmt = NetAmt * TCSPerc / 100;
            TxtTCSAmt.Text = TCSAmt.ToString("F0");
        }

        private void dgv_itemDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void CmbTCS_SelectedValueChanged(object sender, EventArgs e)
        {
            TCSRate();
        }

        private void CmbTCS_TextChanged(object sender, EventArgs e)
        {
            TCSRate();
        }

        public void TDSRate()
        {
            TxtPercTDS.Clear(); TDSAmount();
            if (Cmb_SupplierId.SelectedValue == null)
                return;
            if (CmbTdsId.Text == "")
                return;
            if (CmbTdsId.SelectedValue != null && CmbTdsId.SelectedValue.ToString().Trim().Length > 3)
            {
                Int64 LedgerId = 0; String PartyType = "";
                using (DataTable dtSupp = DBConn.GetData(new SqlCommand("select Isnull(Acc_LedgerID,0) as Acc_LedgerID from PUR.SupplierMaster where SuppId='" + Cmb_SupplierId.SelectedValue.ToString() + "'")).Tables[0])
                {
                    if (dtSupp.Rows.Count > 0)
                    {
                        PartyType = "Supplier";
                        LedgerId = Convert.ToInt64(dtSupp.Rows[0]["Acc_LedgerID"].ToString());
                    }
                }
                Double recvrate = 0.00, othind = 0.00, ind = 0.00, nopan = 0.00;
                using (DataTable dt = DBConn.GetData(new SqlCommand("select top 1 ReceivableRate,Otherthanindividual,Individual,NoPanno FROM GEN.TDSDetails  where TdsId='" + CmbTdsId.SelectedValue + "' and date<='" + dtp_InvDate.Value + "'order by date desc")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        ind = Convert.ToDouble(dt.Rows[0]["Individual"].ToString());
                        othind = Convert.ToDouble(dt.Rows[0]["Otherthanindividual"].ToString());
                        nopan = Convert.ToDouble(dt.Rows[0]["NoPanno"].ToString());
                    }
                }
                String HasTds = "", HasPan = "", IsInd = "";
                using (DataTable dt = DBConn.GetData(new SqlCommand("select isnull(HasTDS,'False') as HasTDS,isnull(HasPan,'False') as HasPan,isnull(IsIndividual,'False') as IsIndividual from ACC.Vtds WHERE Acc_LedgerID=" + LedgerId.ToString() + " and PartyType='" + PartyType + "'")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        HasPan = dt.Rows[0]["HasPan"].ToString();
                        IsInd = dt.Rows[0]["IsIndividual"].ToString();

                        if (HasPan == "False")
                        {
                            TxtPercTDS.Text = nopan.ToString("F3");
                        }
                        else if (HasPan == "True" && IsInd == "True")
                        {
                            TxtPercTDS.Text = ind.ToString("F3");
                        }
                        else if (HasPan == "True" && IsInd == "False")
                        {
                            TxtPercTDS.Text = othind.ToString("F3");
                        }
                        TDSAmount();
                    }
                }
            }
        }

        private void TDSAmount()
        {
            Double GndTAmt = 0, PercTDS = 0, AmountTDS = 0;
            GndTAmt = Convert.ToDouble((string.IsNullOrEmpty(txtGrandTotal.Text.Trim()) ? "0.00" : txtGrandTotal.Text));
            PercTDS = Convert.ToDouble((string.IsNullOrEmpty(TxtPercTDS.Text.Trim()) ? "0.00" : TxtPercTDS.Text));
            AmountTDS = GndTAmt * PercTDS / 100;
            TxtAmountTDS.Text = AmountTDS.ToString("F0");
        }

        private void CmbTdsId_SelectedValueChanged(object sender, EventArgs e)
        {
            TDSRate();
        }

        private void CmbTdsId_TextChanged(object sender, EventArgs e)
        {
            TDSRate();
        }
        private void ItemDetails()
        {


            if (txtitemcode.Text != null)
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                             ("SELECT [Item Name],[Item Code] FROM ITM.VItemMaster WHERE [Item Code] ='" + dgv_itemDetails.CurrentRow.Cells["Code"].Value + "'AND BillGroup='" + cmbmetaltype.SelectedValue + "'")).Tables[0])
                //using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                //             ("SELECT [Item Name],[Item Code] FROM ITM.VItemMaster WHERE ItemId =" + (dgv_SalesDetails.CurrentRow.Cells["ItemId"].Value.ToString() == "" ? "0" : dgv_SalesDetails.CurrentRow.Cells["ItemId"].Value.ToString()) + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        Cmb_ItemName.Text = (dt.Rows[0]["Item Name"] == DBNull.Value ? "" : dt.Rows[0]["Item Name"].ToString());

                    }

                }
            }


        }

        private void dgv_itemDetails_CellLeave_1(object sender, DataGridViewCellEventArgs e)
        {
            ItemDetails();
        }

        private void BtnMetalCashCal_Click(object sender, EventArgs e)
        {
            if (Rb92_MC.Checked == false && Rb99_5.Checked == false) { GrpRateCuttingMode.ShowMessage("Blank Values Not Allowed...!!"); return; }

            if (TxtMetalRate.Text == "0") { TxtMetalRate.ShowMessage("Blank Values Not Allowed...!!"); return; }
            if (TxtMetPurity.Text == "0") { TxtMetPurity.ShowMessage("Blank Values Not Allowed...!!"); return; }
            if (ChkTaxIncl.Checked == true && txtTaxPerc.Text == "0") { txtTaxPerc.ShowMessage("Blank Values Not Allowed...!!"); return; }

            foreach (DataGridViewRow r in dgv_itemDetails.Rows)
            {
                Double Netwt = 0, Touch = 0, Met_Puirty = 0, Met_Rate = 0, WT92 = 0, WT99_5 = 0, Wt100 = 0, MC_WT = 0;
                Met_Puirty = Convert.ToDouble(TxtMetPurity.Text);
                Met_Rate = Convert.ToDouble(TxtMetalRate.Text);

                if (r.Cells["ItemId"].Value.ToString().Trim().Length >= 2)
                {
                    Touch = Convert.ToDouble(r.Cells["Touch"].Value.ToString() == "" ? "0" : r.Cells["Touch"].Value.ToString());
                    Netwt = Convert.ToDouble(r.Cells["Netwt"].Value.ToString() == "" ? "0" : r.Cells["Netwt"].Value.ToString());

                    WT99_5 = Math.Round(((Math.Round(Netwt, 3) * Touch) / Met_Puirty), 3);
                    Wt100 = Math.Round(((Math.Round(Netwt, 3) * Touch) / 100), 3);

                    if (Rb92_MC.Checked == true)
                    {
                        WT92 = Math.Round(((Math.Round(Netwt, 3) * 92 / 100) / Met_Puirty * 100), 3);
                        MC_WT = WT99_5 - WT92;
                    }

                    if (r.Cells["Nos"].Value.ToString() == "") { r.Cells["Nos"].Value = 0.ToString("F0"); }
                    r.Cells["WT_92"].Value = WT92.ToString(RoundTo); r.Cells["WT_99_5"].Value = WT99_5.ToString(RoundTo);
                    r.Cells["WT_100"].Value = Wt100.ToString(RoundTo); r.Cells["MC_WT"].Value = MC_WT.ToString(RoundTo);
                }
            }

            DataTable dtSales = new DataTable(); DataTable dtBillAdj = new DataTable();
            dtSales.Columns.Add("Slno"); dtSales.Columns.Add("Touch"); dtSales.Columns.Add("WT_99_5");
            dtBillAdj.Columns.Add("Slno"); dtBillAdj.Columns.Add("Rate"); dtBillAdj.Columns.Add("WT_99_5");

            DataRow drSales = null; int i = 1;
            foreach (DataGridViewRow dr in dgv_itemDetails.Rows)
            {
                drSales = dtSales.NewRow();
                drSales["Slno"] = i++;
                drSales["Touch"] = dr.Cells["Touch"].Value;
                drSales["WT_99_5"] = (Rb92_MC.Checked == true ? dr.Cells["WT_92"].Value : dr.Cells["WT_99_5"].Value);
                dtSales.Rows.Add(drSales);
            }

            DataRow drBillAdj = null; int x = 1;
            

            drBillAdj = dtBillAdj.NewRow();
            drBillAdj["Slno"] = x++;
            drBillAdj["Rate"] = TxtMetalRate.Text;
            drBillAdj["WT_99_5"] = 0;
            dtBillAdj.Rows.Add(drBillAdj);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SALE.MC_Calc_Wt_Wise";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SaleData", dtSales);
            cmd.Parameters.AddWithValue("@BillAdjData", dtBillAdj);

            DataTable dtLocalCash = DBConn.GetData(cmd).Tables[0];
            int R = 0;
            foreach (DataGridViewRow r in dgv_itemDetails.Rows)
            {

                if (r.Cells["ItemId"].Value.ToString().Trim().Length >= 2)
                {
                    Double MetalCash = 0, MC_ = 0;

                    if ((dtLocalCash.Rows[R]["Touch"].ToString() == Convert.ToDouble(r.Cells["Touch"].Value.ToString()).ToString("f2")) && (Convert.ToDouble(dtLocalCash.Rows[R]["WT_99_5"].ToString()).ToString(RoundTo) == (Rb92_MC.Checked == true ? Convert.ToDouble(r.Cells["WT_92"].Value.ToString()).ToString(RoundTo) : Convert.ToDouble(r.Cells["WT_99_5"].Value.ToString()).ToString(RoundTo))))
                    {
                        MetalCash = Convert.ToDouble(dtLocalCash.Rows[R]["MetalCash"].ToString());
                        MC_ = Convert.ToDouble(r.Cells["MC_WT"].Value.ToString()) * Convert.ToDouble(TxtMetalRate.Text);
                    }
                    R++;
                    if (ChkTaxIncl.Checked == true)
                    {
                        r.Cells["MetalCash"].Value = (MetalCash * 100 / (100 + (Convert.ToDouble((txtTaxPerc.Text == "" ? "0" : txtTaxPerc.Text))))).ToString("F2");
                    }
                    else
                    {
                        r.Cells["MetalCash"].Value = MetalCash.ToString("F2");
                    }

                    r.Cells["MC"].Value = 0.ToString("F2");
                    r.Cells["MC_IncuTax"].Value = 0.ToString("F2");

                    if (Rb92_MC.Checked == true)
                    {
                        if (ChkTaxIncl.Checked == true)
                        {
                            r.Cells["MC"].Value = (MC_ * 100 / (100 + (Convert.ToDouble((txtTaxPerc.Text == "" ? "0" : txtTaxPerc.Text))))).ToString("F2");
                        }
                        else
                        {
                            r.Cells["MC"].Value = MC_.ToString("F2");
                        }
                        r.Cells["MC_IncuTax"].Value = MC_.ToString("F2");
                    }
                    //----Calc TotalAmount-----

                    double Diacash = 0, Stcash = 0, GoldCash = 0, Mc = 0, Discount = 0;

                    if (r.Cells["MetalCash"].Value.ToString() != "")
                    {
                        GoldCash = Convert.ToDouble(r.Cells["MetalCash"].Value.ToString());
                    }

                    if (r.Cells["StoneCash"].Value.ToString() != "")
                    {
                        Stcash = Convert.ToDouble(r.Cells["StoneCash"].Value.ToString());
                    }

                    if (r.Cells["DiaCash"].Value.ToString() != "")
                    {
                        Diacash = Convert.ToDouble(r.Cells["DiaCash"].Value.ToString());
                    }

                    if (r.Cells["MC"].Value.ToString() != "")
                    {
                        Mc = Convert.ToDouble(r.Cells["MC"].Value.ToString());
                    }

                    if (r.Cells["Discount"].Value.ToString() != "")
                    {
                        Discount = Convert.ToDouble(r.Cells["Discount"].Value.ToString());
                    }

                    r.Cells["Total"].Value = Convert.ToDouble(GoldCash + Stcash + Diacash + Mc - Discount).ToString();

                }
            }

            Double TotAmt = 0, Amt_ = 0;
            Amt_ = Convert.ToDouble(TxtCalcAMT.Text);
            TotAmt = Convert.ToDouble(txtTotalAmount.Text);
            if (Amt_ != 0) { txtRoundoff.Text = (Amt_ - TotAmt).ToString("F2"); } else { txtRoundoff.Text = "0"; }
        }

        private void grb_WholeSale_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsEditMode)
            {
                if (grb_WholeSale.Checked)
                {
                    cmb_VoucherTypeId.SelectedValue = 5;
                    TxtVoucherNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_Purchasedt.Value,
                 DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
                    SetColumns();
                }
            }

            //}
            //cmb_VoucherTypeId.SelectedValue = (int)SAFA.Classes.VoucherTypes.PurchaseMaster;
        }

        private void grb_Retail_CheckedChanged(object sender, EventArgs e)
        {
            if (!IsEditMode)
            {
                if (grb_Retail.Checked)
                {
                    cmb_VoucherTypeId.SelectedValue = 249;
                    TxtVoucherNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_Purchasedt.Value,
               DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
                    SetColumns();
                }
            }
        }

        private void linkLbl_BillAdjAmt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (GrpBillAdj.Visible == false)
            {
                GrpBillAdj.Visible = true;
                GrpBillAdj.BringToFront();
                GrpBillAdj.Parent = this;

                Binding_BillAdj_To_Panel(Convert.ToInt64(Cmb_SupplierId.SelectedValue), Convert.ToInt64(TxtPurId.Text == "" ? "0" : TxtPurId.Text));

                GrpBillAdj.Visible = true;
                GrpBillAdj.BringToFront();
                GrpBillAdj.Parent = this;

                GrpBillAdj.Size = new System.Drawing.Size(895, 320);

                GrpBillAdj.Location = new Point(150, 220);


                GrpBillAdj.Show();

            }
            else
            {
                GrpBillAdj.Visible = false;
                GrpBillAdj.SendToBack();
                GrpBillAdj.Hide();
            }
        }

        void Binding_BillAdj_To_Panel(Int64 PartyId, Int64 RecId)
        {
             
        }

        //public void Summary_Calc(object sender, SAFA.Forms.ACC.AdjustingEntries.Grid_SummaryCalculatedEventArgs e)
        //{
        //    TxtBillAdjAmt.Text = ((SAFA.Forms.ACC.AdjustingEntries)sender).Amt.ToString("f2");
        //}

        //public void AutoAdjust_Click(object sender, SAFA.Forms.ACC.AdjustingEntries.AutoAdjust_EventArgs e)
        //{
        //    Double WT_99_5 = (Rb92_MC.Checked == true ? Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["WT (92)"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["WT (92)"].Text) : Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["WT (99.5)"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["WT (99.5)"].Text));
        //    if (WT_99_5 == 0) { WT_99_5 = (Rb92_MC.Checked == true ? Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["WT_92"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["WT_92"].Text) : Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["WT_99_5"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["WT_99_5"].Text)); }
        //    Double MC = (Rb92_MC.Checked == true ? Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["MC_IncuTax"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["MC_IncuTax"].Text) : 0);
        //    ((SAFA.Forms.ACC.AdjustingEntries)sender).Redeem_Weight = WT_99_5;
        //    ((SAFA.Forms.ACC.AdjustingEntries)sender).Redeem_MC = MC;
        //    ((SAFA.Forms.ACC.AdjustingEntries)sender).AutoAdjust();
        //}

        public List<string> SetHiddenColumns(String SalesMode, String MetalType)
        {
            List<string> HiddenColumnList = new List<string>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GEN.Proc_SetHiddenColumns";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecFrom", "Purchase");
            cmd.Parameters.AddWithValue("@HiddenType1", SalesMode);
            cmd.Parameters.AddWithValue("@HiddenType2", MetalType);
            cmd.Parameters.AddWithValue("@HiddenType3", "");
            cmd.Parameters.AddWithValue("@CompanyId", Gramboo.GeneralConfig.CompanyID);
            cmd.Parameters.AddWithValue("@BranchId", Gramboo.GeneralConfig.BranchId);

            DataTable dtHiddenColumnList = DBConn.GetData(cmd).Tables[0];

            var TempHiddenColumnList = (from Rows in (dtHiddenColumnList).AsEnumerable()
                                        select Rows["HiddenColumnList"]).Distinct().ToList();

            foreach (var r in TempHiddenColumnList)
            {
                if (r.ToString().Trim().Length >= 1)
                {
                    HiddenColumnList.Add(r.ToString());
                }
            }

            return HiddenColumnList;
        }

        private void Rb99_5_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb99_5.Checked == true)
            {
                dgv_itemDetails.Columns["WT_99_5"].Visible = true;
                dgv_itemDetails.Columns["WT_100"].Visible = true;
                dgv_itemDetails.Columns["WT_92"].Visible = false;
                dgv_itemDetails.Columns["MC_WT"].Visible = false;
                dgv_itemDetails.Columns["MC"].Visible = false;
            }
        }

        private void Rb92_MC_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb92_MC.Checked == true)
            {
                dgv_itemDetails.Columns["WT_99_5"].Visible = true;
                dgv_itemDetails.Columns["WT_100"].Visible = true;
                dgv_itemDetails.Columns["WT_92"].Visible = true;
                dgv_itemDetails.Columns["MC_WT"].Visible = true;
                dgv_itemDetails.Columns["MC"].Visible = true;
            }
        }
        private void AfterComboLeave(Gramboo.Controls.GrbDataGridView dgv, Gramboo.Controls.GrbComboBox cmb, int valuecolumnindex = -1)
        {

            if (FillFlag || dgv.CurrentCell == null || dgv.CurrentCell.ReadOnly)
                return;


            cmb.Visible = false;
            if (cmb.Text != "")
            {
                if ((valuecolumnindex >= 0 && (cmb.SelectedValue == null || cmb.Text == "")))
                {
                    dgv.CurrentCell.Value = "";
                    dgv.Rows[dgv.CurrentCell.RowIndex].Cells[valuecolumnindex].Value = 0;
                    return;
                }
                else if (valuecolumnindex < 0)
                {

                    if (cmb.CheckDuplicates)
                    {
                        if (((System.Data.DataTable)dgv.DataSource).Select(dgv.CurrentCell.OwningColumn.Name + "='" + cmb.Text + "'").Length != 0)
                        {
                            return;
                        }
                    }
                    dgv.CurrentCell.Value = cmb.Text;
                }
                else
                {
                    if (cmb.CheckDuplicates)
                    {
                        if (((System.Data.DataTable)dgv.DataSource).Select(dgv.CurrentCell.OwningColumn.Name + "='" + cmb.Text + "'").Length != 0)
                        {
                            return;
                        }
                    }
                    dgv.CurrentCell.Value = cmb.Text;
                    if (valuecolumnindex > -1 && cmb.Text != "")
                        dgv.Rows[dgv.CurrentCell.RowIndex].Cells[valuecolumnindex].Value = cmb.SelectedValue;
                }





                dgv.Focus();
            }

        }


        private void ComboKeydown(Gramboo.Controls.GrbDataGridView dgv, Gramboo.Controls.GrbComboBox cmb, KeyEventArgs e)
        {

            if (FillFlag)
                return;

            if (e.KeyValue == 13)
            {

                dgv.Focus();
                // SendKeys.Send("{Enter}");

                cmb.Visible = false;
            }

        }

        private void grbComboBox1_Leave(object sender, EventArgs e)
        {
            //flag = true;
            // MetalRate(dgv_SalesDetails);

            AfterComboLeave(dgv_itemDetails, grbComboBox1, (dgv_itemDetails.Columns.Count > 0 ? dgv_itemDetails.Columns["ItemID"].Index : -1));
            //flag = false;
        }

        private void grbComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            ComboKeydown(dgv_itemDetails, grbComboBox1, e);
            // dgv_SalesDetails.Focus();          
            //  ComboKeydown(dgv_SalesDetails, txt_DisplayName, e);
        }
        private void Cmb_ItemName_KeyDown(object sender, KeyEventArgs e)
        {
            ComboKeydown(dgv_itemDetails, Cmb_ItemName, e);
            // dgv_SalesDetails.Focus();          
            //  ComboKeydown(dgv_SalesDetails, txt_DisplayName, e);
        }

        private void cmbbranch_KeyDown(object sender, KeyEventArgs e)
        {
            ComboKeydown(dgv_itemDetails, cmbbranch, e);
            // dgv_SalesDetails.Focus();          
            //  ComboKeydown(dgv_SalesDetails, txt_DisplayName, e);
        }

        private void cmb_purity_KeyDown(object sender, KeyEventArgs e)
        {
            ComboKeydown(dgv_itemDetails, cmb_purity, e);
            // dgv_SalesDetails.Focus();          
            //  ComboKeydown(dgv_SalesDetails, txt_DisplayName, e);
        }

        private void TxtCalcRate_Validated(object sender, EventArgs e)
        {
            if (Convert.ToDouble(TxtCalcOtChrg.Text) != 0) { txtOtherCharges.ShowMessage("Blank Values Not Allowed...!!"); return; }
            Pan_Mc_Rate_Calc.SendToBack();
            Pan_Mc_Rate_Calc.Visible = false;
            TxtMetalRate.Text = TxtCalcRate.Text;
        }

        private void LinkRateCalc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Pan_Mc_Rate_Calc.BringToFront();
            Pan_Mc_Rate_Calc.Visible = true;
            checkBoxTaxIncu.Visible = true;
        }

        void CalcRate(Double Amt, Double OtChrg, Double WT, bool TxIncu)
        {
            Double Rate = 0, Amt_ = 0, OtChrg_ = 0;
            if (Amt == 0) { TxtCalcRate.Text = "0"; return; }
            if (WT == 0) { TxtCalcRate.Text = "0"; return; }
            if (checkBoxTaxIncu.Checked == true && txtTaxPerc.Text == "0") { txtTaxPerc.ShowMessage("Blank Values Not Allowed...!!"); return; }
            if (checkBoxTaxIncu.Checked == true)
            {
                OtChrg_ = OtChrg + Math.Round(Math.Round(OtChrg * ((Convert.ToDouble((txtTaxPerc.Text == "" ? "0" : txtTaxPerc.Text)))), 2) / 100, 2);
            }
            else
            {
                OtChrg_ = OtChrg;
            }
            Amt_ = Amt;
            Rate = Math.Round(((Amt_ - OtChrg_) / WT), 2);
            TxtCalcRate.Text = Rate.ToString("F2");
        }

        private void TxtCalcAMT_TextChanged(object sender, EventArgs e)
        {
            CalcRate(Convert.ToDouble(TxtCalcAMT.Text), Convert.ToDouble(TxtCalcOtChrg.Text), Convert.ToDouble(TxtCalcWT.Text), checkBoxTaxIncu.Checked);
        }

        private void TxtCalcWT_TextChanged(object sender, EventArgs e)
        {
            CalcRate(Convert.ToDouble(TxtCalcAMT.Text), Convert.ToDouble(TxtCalcOtChrg.Text), Convert.ToDouble(TxtCalcWT.Text), checkBoxTaxIncu.Checked);
        }

        private void TxtCalcOtChrg_TextChanged(object sender, EventArgs e)
        {
            CalcRate(Convert.ToDouble(TxtCalcAMT.Text), Convert.ToDouble(TxtCalcOtChrg.Text), Convert.ToDouble(TxtCalcWT.Text), checkBoxTaxIncu.Checked);
        }

        private void checkBoxTaxIncu_CheckedChanged(object sender, EventArgs e)
        {
            CalcRate(Convert.ToDouble(TxtCalcAMT.Text), Convert.ToDouble(TxtCalcOtChrg.Text), Convert.ToDouble(TxtCalcWT.Text), checkBoxTaxIncu.Checked);
        }

        private void SetColumns()
        {

            dgv_itemDetails.DataFields = new List<string> { "TransId", "PurId", "Slno", "JobOrderId", "JobNo", "ProdCodeId", "ItemID", "ModelId", "[Branch Name]", "ProdCode", "HUId", "Code", "[Item Name]", "[Model Name]", "PurityId", "Purity", "Touch",
                "Nos", "Gwt","Ot_Stno","Ot_Stwt","Ot_StRate","Ot_StAmt","Pr_Stno","Pr_Stwt","Pr_StRate","Pr_StAmt","StNo", "StWt", "StoneCtWt","StRate","StoneCash", "DiaNo", "Diawt", "DiaRate", "DiaCash", "NetWt1", "NetWt2", "NetWt", "WT_99_5", "WT_100", "WT_92",
                "MC_WT", "MetalRate1", "MetalRate2", "Rate", "MetalCash1", "MetalCash2","MetalCash", "MCPerc", "MC", "MC_IncuTax", "WstPerc", "Wst", "Discount", "Total", "[Purity Value]", "BranchId" };

            dgv_itemDetails.HiddenDataFields = new List<string>();
            dgv_itemDetails.HiddenDataFields = SetHiddenColumns(grb_salesmode.Value.ToString(), (cmbmetaltype.SelectedValue != null ? cmbmetaltype.SelectedValue.ToString() : "G"));


           


            dgv_itemDetails.SummaryColumns = new string[] { "Nos", "Gwt", "Ot_Stno", "Ot_Stwt", "Ot_StAmt", "Pr_Stno", "Pr_Stwt", "Pr_StAmt", "Stno", "StWt", "DiaNo", "Diawt", "MetalCash1", "MetalCash2", "MetalCash", "DiaCash", "StoneCash", "MC", "Wst", "NetWt1", "NetWt2", "NetWt", "WT_100", "WT_99_5", "WT_92", "MC_WT", "MC_IncuTax", "Total" };

            dgv_itemDetails.Fill(new Table(SAFA.Classes.Common.DbName, "PUR", "VPurchaseDetails", true), "1=2");
            dgv_itemDetails.Refresh();
            dgv_itemDetails.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            AdjustColumnWidths();

            dgv_itemDetails.RowTemplate.Height = Cmb_ItemName.Height;
            dgv_itemDetails.AllowUserToAddRows = false;
            ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());
            dgv_itemDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;

            dgv_itemDetails.BeginEdit(true);
        }

        private void MetalCash(Gramboo.Controls.GrbDataGridView dgv, bool IsMetalRate)
        {
            if (dgv.CurrentRow == null)
                return;
            if (dgv.CurrentRow.Cells["ItemId"].Value.ToString() == "")
                return;
            Double metalrate1 = 0, metalrate2 = 0, metalrate = 0, NetWt1 = 0, NetWt2 = 0, NetWt = 0, VA = 0, Touch = 0, Touch2 = 0, metalcash = 0, metalcash1 = 0, metalcash2 = 0, Wt916 = 0, MetalId = 0, Gwt = 0, Nos = 0;
            string CalcOn = "", Metal1 = "", Metal2 = "";


            if (dgv.CurrentRow.Cells["ItemId"].Value.ToString() != "")
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                ("SELECT MetalId,[Purity Value],[Purity Value2],[Calculated On],Metal1,Metal2 FROM ITM.VItemMaster WHERE ItemId =" + dgv.CurrentRow.Cells["ItemId"].Value.ToString() + "")).Tables[0])
                {

                    if (dt.Rows.Count > 0)
                    {

                        MetalId = Convert.ToSingle(dt.Rows[0]["MetalId"].ToString());
                        Metal1 = dt.Rows[0]["Metal1"].ToString();
                        Metal2 = dt.Rows[0]["Metal2"].ToString();
                        if (dt.Rows[0]["Purity Value"].ToString() != "")
                        {
                            Touch = Convert.ToSingle(dt.Rows[0]["Purity Value"].ToString());
                        }
                        else
                        {
                            Touch = 0;
                        }
                        if (dt.Rows[0]["Purity Value2"].ToString() != "")
                        {
                            Touch2 = Convert.ToSingle(dt.Rows[0]["Purity Value2"].ToString());
                        }
                        else
                        {
                            Touch2 = 0;
                        }
                        CalcOn = (dt.Rows[0]["Calculated On"].ToString());
                    }

                    else
                    {
                        return;
                    }
                }
            }

            if (cmbmetaltype.SelectedValue.ToString() != "FU")
            {

                if ((dgv.CurrentRow.Cells["Rate"].Value.ToString() != "" && CalcOn == "Nos") || IsMetalRate)
                {
                    metalrate = Convert.ToSingle(dgv.CurrentRow.Cells["Rate"].Value.ToString() == "" ? "" : dgv.CurrentRow.Cells["Rate"].Value.ToString());

                }
                else if (CalcOn == "NetWt" || CalcOn == "Gwt")
                {

                    if (cmbmetaltype.SelectedValue.ToString() == "S")
                    {
                        metalrate = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'  and Branch_id=" + txtBranchID.Text + " Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["SilverRate"]).ToString());
                    }
                    else if (cmbmetaltype.SelectedValue.ToString() == "P")
                    {
                        metalrate = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 PlatinumRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'  and Branch_id=" + txtBranchID.Text + " Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["PlatinumRate"]).ToString());
                    }
                    else if (Touch < 90)
                    {
                        metalrate = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1  MetalRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["MetalRate"]).ToString());
                    }
                    else
                    {
                        metalrate = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString());
                    }
                    dgv.CurrentRow.Cells["Rate"].Value = metalrate;
                }

                else
                {
                    return;
                }
                if (dgv.CurrentRow.Cells["NetWt"].Value.ToString() != "")
                {
                    NetWt = Convert.ToSingle(dgv.CurrentRow.Cells["NetWt"].Value.ToString());
                }
                if (dgv.CurrentRow.Cells["Gwt"].Value.ToString() != "")
                {
                    Gwt = Convert.ToSingle(dgv.CurrentRow.Cells["Gwt"].Value.ToString());
                }
                if (dgv.CurrentRow.Cells["Nos"].Value.ToString() != "")
                {
                    Nos = Convert.ToSingle(dgv.CurrentRow.Cells["Nos"].Value.ToString());
                }

                if (dgv.CurrentRow.Cells["Touch"].Value.ToString() != "" && dgv.CurrentRow.Cells["Touch"].Value.ToString() != "0")
                {
                    Touch = Convert.ToSingle(dgv.CurrentRow.Cells["Touch"].Value.ToString());
                }

                else if (dgv.CurrentRow.Cells["Touch"].Value.ToString() == "" && dgv.CurrentRow.Cells["Touch"].Value.ToString() == "0" && CalcOn == "Gwt")
                {
                    dgv.CurrentRow.Cells["Touch"].Value = Touch;
                }

                if (CalcOn == "NetWt")
                {
                    metalcash = NetWt * metalrate;

                }
                else if (CalcOn == "Gwt")
                {
                    metalcash = Gwt * metalrate;
                }
                else
                {
                    metalcash = Nos * metalrate;
                }

                dgv.CurrentRow.Cells["MetalCash"].Value = metalcash.ToString();
            }
            else if (cmbmetaltype.SelectedValue.ToString() == "FU")
            {

                if ((dgv.CurrentRow.Cells["MetalRate1"].Value.ToString() != "") || IsMetalRate)
                {
                    metalrate1 = Convert.ToSingle(dgv.CurrentRow.Cells["MetalRate1"].Value.ToString() == "" ? "" : dgv.CurrentRow.Cells["MetalRate1"].Value.ToString());
                }
                else
                {
                    if (Metal1 == "S")
                    {
                        metalrate1 = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'  and Branch_id=" + txtBranchID.Text + " Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["SilverRate"]).ToString());
                    }
                    else if (Metal1 == "P")
                    {
                        metalrate1 = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 PlatinumRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'  and Branch_id=" + txtBranchID.Text + " Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["PlatinumRate"]).ToString());
                    }

                    else if (Touch < 90)
                    {
                        metalrate1 = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1  MetalRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["MetalRate"]).ToString());
                    }
                    else
                    {
                        metalrate1 = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString());
                    }
                    dgv.CurrentRow.Cells["MetalRate1"].Value = metalrate1;
                }

                if ((dgv.CurrentRow.Cells["MetalRate2"].Value.ToString() != "") || IsMetalRate)
                {
                    metalrate2 = Convert.ToSingle(dgv.CurrentRow.Cells["MetalRate2"].Value.ToString() == "" ? "" : dgv.CurrentRow.Cells["MetalRate2"].Value.ToString());
                }
                else
                {
                    if (Metal2 == "S")
                    {
                        metalrate2 = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'  and Branch_id=" + txtBranchID.Text + " Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["SilverRate"]).ToString());
                    }
                    else if (Metal2 == "P")
                    {
                        metalrate2 = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 PlatinumRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'  and Branch_id=" + txtBranchID.Text + " Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["PlatinumRate"]).ToString());
                    }

                    else if (Touch2 < 90)
                    {
                        metalrate2 = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1  MetalRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["MetalRate"]).ToString());
                    }
                    else
                    {
                        metalrate2 = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString());
                    }
                    dgv.CurrentRow.Cells["MetalRate2"].Value = metalrate2;
                }

                if (dgv.CurrentRow.Cells["NetWt"].Value.ToString() != "")
                {
                    NetWt = Convert.ToSingle(dgv.CurrentRow.Cells["NetWt"].Value.ToString());
                }
                if (dgv.CurrentRow.Cells["NetWt1"].Value.ToString() != "")
                {
                    NetWt1 = Convert.ToSingle(dgv.CurrentRow.Cells["NetWt1"].Value.ToString());
                }
                if (dgv.CurrentRow.Cells["NetWt2"].Value.ToString() != "")
                {
                    NetWt2 = Convert.ToSingle(dgv.CurrentRow.Cells["NetWt2"].Value.ToString());
                }

                if (dgv.CurrentRow.Cells["Touch"].Value.ToString() != "" && dgv.CurrentRow.Cells["Touch"].Value.ToString() != "0")
                {
                    Touch = Convert.ToSingle(dgv.CurrentRow.Cells["Touch"].Value.ToString());
                }
                else if (dgv.CurrentRow.Cells["Touch"].Value.ToString() == "" && dgv.CurrentRow.Cells["Touch"].Value.ToString() == "0" && CalcOn == "Gwt")
                {
                    dgv.CurrentRow.Cells["Touch"].Value = Touch;
                }

                metalcash1 = NetWt1 * metalrate1;
                dgv.CurrentRow.Cells["MetalCash1"].Value = metalcash1.ToString("f2");
                metalcash2 = NetWt2 * metalrate2;
                dgv.CurrentRow.Cells["MetalCash2"].Value = metalcash2.ToString("f2");
                metalcash = metalcash1 + metalcash2;
                dgv.CurrentRow.Cells["MetalCash"].Value = metalcash.ToString("f2");

                if (metalcash + NetWt != 0)
                {
                    dgv.CurrentRow.Cells["Rate"].Value = (metalcash / NetWt).ToString("f2");
                }

            }
        }

        private void txtNetTotal_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtRoundoff_TextChanged(object sender, EventArgs e)
        {
            txtRoundoff.TextChanged -= new EventHandler(NetAmount);

            if (this.ActiveControl == txtRoundoff)
            {
                txtRoundoff.TextChanged += new EventHandler(NetAmount);
            }
            else
            {
                txtRoundoff.TextChanged -= new EventHandler(NetAmount);
            }
        }

        private void TxtBillAmount_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == TxtBillAmount)
            {
                double nettotal = 0, TCSAmt = 0, AmtTDS = 0;
                if (txtTotalAmount.Text != "")
                {
                    TCSAmt = Convert.ToDouble((string.IsNullOrEmpty(TxtTCSAmt.Text.Trim()) ? "0.00" : TxtTCSAmt.Text));

                    AmtTDS = Convert.ToDouble((string.IsNullOrEmpty(TxtAmountTDS.Text.Trim()) ? "0.00" : TxtAmountTDS.Text));


                    //txtNetTotal.Text = (nettotal + TCSAmt - AmtTDS).ToString("F2");

                    txtRoundoff.Text = Math.Round(((Convert.ToDouble(TxtBillAmount.Text)) - Convert.ToDouble(txtTotalAmount.Text)), 2).ToString();
                }
            }
        }
        private void CalculateStoneWT(Gramboo.Controls.GrbDataGridView dgv)
        {
            if (dgv.CurrentRow == null)
                return;

            Double Ot_Stwt = 0.000, Pr_Stwt = 0.000, Pr_StConwt = 0.000, Stwt = 0.000, Ot_Stno = 0, Pr_Stno = 0, Stno = 0,
                  Ot_StRate = 0.00, Pr_StRate = 0.00, StRate = 0.00, Ot_StAmt = 0.00, Pr_StAmt = 0.00,
                  StAmt = 0.00;

            if (dgv.CurrentRow.Cells["Ot_Stno"].Value.ToString() != "")
            {
                Ot_Stno = Convert.ToDouble(dgv.CurrentRow.Cells["Ot_Stno"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["Ot_Stwt"].Value.ToString() != "")
            {
                Ot_Stwt = Convert.ToDouble(dgv.CurrentRow.Cells["Ot_Stwt"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["Ot_StRate"].Value.ToString() != "")
            {
                Ot_StRate = Convert.ToDouble(dgv.CurrentRow.Cells["Ot_StRate"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["Ot_StAmt"].Value.ToString() != "")
            {
                Ot_StAmt = Convert.ToDouble(dgv.CurrentRow.Cells["Ot_StAmt"].Value.ToString());
            }

            if (dgv.CurrentRow.Cells["Pr_Stno"].Value.ToString() != "")
            {
                Pr_Stno = Convert.ToDouble(dgv.CurrentRow.Cells["Pr_Stno"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["Pr_Stwt"].Value.ToString() != "")
            {
                Pr_StConwt = Convert.ToDouble(dgv.CurrentRow.Cells["Pr_Stwt"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["Pr_StRate"].Value.ToString() != "")
            {
                Pr_StRate = Convert.ToDouble(dgv.CurrentRow.Cells["Pr_StRate"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["Pr_StAmt"].Value.ToString() != "")
            {
                Pr_StAmt = Convert.ToDouble(dgv.CurrentRow.Cells["Pr_StAmt"].Value.ToString());
            }

            if (Pr_StConwt != 0) { Pr_Stwt = Pr_StConwt / 5; }

            Stno = Math.Round(Ot_Stno, 0) + Math.Round(Pr_Stno, 0);
            Stwt = Math.Round(Ot_Stwt, 3) + Math.Round(Pr_Stwt, 3);
            StAmt = Math.Round(Ot_StAmt, 2) + Math.Round(Pr_StAmt, 2);
            if (StAmt != 0) { StRate = Math.Round((Math.Round(StAmt, 2)) / Math.Round(Stwt, 3), 2); }

            dgv.CurrentRow.Cells["Stno"].Value = Math.Round(Stno, 0).ToString("f0");
            dgv.CurrentRow.Cells["StWt"].Value = Math.Round(Stwt, 3).ToString("f3");
            dgv.CurrentRow.Cells["StRate"].Value = Math.Round(StRate, 2).ToString("f2");
            dgv.CurrentRow.Cells["StoneCash"].Value = Math.Round(StAmt, 2).ToString("f2");
        }

        private void CalcNetwt2(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double Netwt = 0.000, Netwt1 = 0.000, Netwt2 = 0.000;
            if (dgv.CurrentRow == null)
                return;
            if (cmbmetaltype.SelectedValue.ToString() != "FU")
                return;
            if (dgv.CurrentRow.Cells["Netwt"].Value.ToString() != "")
            {
                Netwt = Convert.ToDouble(dgv.CurrentRow.Cells["Netwt"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["Netwt1"].Value.ToString() != "")
            {
                Netwt1 = Convert.ToDouble(dgv.CurrentRow.Cells["Netwt1"].Value.ToString());
            }
            Netwt2 = Netwt - Netwt1;
            if (Netwt2 < 0.000)
            {
                Netwt2 = 0;
                dgv.CurrentRow.Cells["Netwt1"].Value = 0;
            }
            dgv.CurrentRow.Cells["Netwt2"].Value = Math.Round(Netwt2, 3).ToString("f3");
        }

    }
}