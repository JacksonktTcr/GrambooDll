using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace JMS.Forms.SALE
{
    public partial class SalesMaster : Gramboo.Controls.GrbForm
    {
        //Heading-----------------------------------
        private string InvTitle;
        private string InvSubTitle1;
        private string InvSubTitle2;
        private string InvSubTitle3;
        private string InvSubTitle4;
        private string InvSubTitle5;
       //--------------------------------------------
        private int Comp_pin, Qty, SumQty,DiaNo,ExQty,Qty_Ret;
        private string Company, Comp_Add1, Comp_Add2, Comp_Place, Comp_City, Comp_Dist, Comp_State,Comp_phone,ProdCode,ItemName,
        TinNo, CstNo, InvoiceNo, CustomerName, Address1, PhoneNo, InvoiceDate, oldGoldNo, BranchName;
        private float DisAmt=0, oldGoldRpt=0, netTotal=0, TaxAmt=0,DiaWt=0,DiaCash=0,CashPaid=0,AmtPayable=0,
        FinalAmt = 0, Gwt = 0, StWt = 0, StoneCash = 0, Total = 0, VaPerc = 0, VaPercAftDis = 0, NetWt=0,
        SGwt=0,SStWt=0,SStoneCash=0,SNetWt=0,STotAmt=0,GrandTotal=0,TaxPerc=0;
        private string ExItemname;
        private float ExGwt = 0, ExStWt = 0, ExStCash = 0, ExNetWt = 0, ExAmount = 0,
          SExGwt = 0, SExStWt = 0, SExStCash = 0, SExNetWt = 0, SExAmount = 0;
        private string ProdCode_Ret, ItemName_Ret;
        private float Gwt_Ret = 0, StWt_Ret = 0, NetWt_Ret = 0,VAPerc_Ret=0, StCash_Ret = 0, Amount_Ret = 0,
        SGwt_Ret = 0, SStWt_Ret = 0, SNetWt_Ret = 0, SStCash_Ret = 0, SAmount_Ret=0;
        private float OldGwt=0,OldNetWt=0,OldRate=0,OldAmount=0,OldLessWt=0;
        private string OldVchNo,OldCust,OldItemName,OldVchDate;

        //-------------------------------------------
        // for PrintDialog, PrintPreviewDialog and PrintDocument:
        private System.Windows.Forms.PrintDialog prnDialog;
        private System.Windows.Forms.PrintPreviewDialog prnPreview;
        private System.Drawing.Printing.PrintDocument prnDocument;
        //--------------------------------------------
        private Font InvTitleFont = new Font("Arial",12, FontStyle.Regular);
        // Title Font height
        private int InvTitleHeight;

        // SubTitle Font
        private Font InvSubTitleFont = new Font("Arial",10, FontStyle.Regular);
        // SubTitle Font height
        private int InvSubTitleHeight;
        // Invoice Font
        private Font InvoiceFont = new Font("Arial", 12, FontStyle.Regular);
        // Invoice Font height
        private int InvoiceFontHeight;
        private int CurrentY;
        private int CurrentX;
        private int leftMargin;
        private int rightMargin;
        private int topMargin;
        private int bottomMargin;
        private int InvoiceWidth;
        private int InvoiceHeight;
        private SolidBrush BlackBrush = new SolidBrush(Color.Black);
        // Red Color
        private SolidBrush RedBrush = new SolidBrush(Color.Red);
        // Black Color
        //private SolidBrush BlackBrush = new SolidBrush(Color.Black);
        //-------------------------------------------

        private bool CalcFlag = false;
        private int mrp = 0;
        private int SaleCurrentRow = -1;
        private int SaleCurrentretRow = -1;
        private int SaleExchange =-1;
        private bool FillFlag = false;
        //private JMS.Forms.SALE.OldGoldReceiptNew frmold = new OldGoldReceiptNew();
        private static SalesMaster instance;

        public static SalesMaster Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new SalesMaster();
                }
                else if (instance.IsDisposed)
                {
                    instance = new SalesMaster();
                }
                return instance;
            }
        }

        public override bool ValidateControls()
        {
            txt_TotalDiaWt.Text = "0";
            txt_TotalDiaCash.Text = "0";
            bool f= base.ValidateControls();
            return true;
        }
        public SalesMaster()
        {
            InitializeComponent();
            txtOtherspaid.TextChanged += new EventHandler(PaymentTotalAmount);
            txtCash.TextChanged += new EventHandler(PaymentTotalAmount);
            txtCredit.TextChanged += new EventHandler(PaymentTotalAmount);
            txtCheque.TextChanged += new EventHandler(PaymentTotalAmount);
            txtPaidAmount.TextChanged += new EventHandler(TotalCashPaid);
            txt_OldGoldReceipt.TextChanged += new EventHandler(TotalCashPaid);
            txt_ReturnAmount.TextChanged += new EventHandler(TotalCashPaid);
            txt_TotalPaidAmount.TextChanged += new EventHandler(TotalCashPaid);
            txtAdvancePaid.TextChanged += new EventHandler(TotalCashPaid);
            txtAmountAfterDis.TextChanged += new EventHandler(DiscountAmountCalculation);
            txtProductVal.TextChanged += new EventHandler(DiscountAmountCalculation);
            txtProductVal.TextChanged += new EventHandler(amount_AfterDiscount);
            txtGrandTotal.TextChanged += new EventHandler(NetAmount_Calculation);
            txtNetTotal.TextChanged += new EventHandler(FinalAmount);
            txtNetTotal.TextChanged += new EventHandler(RoundOff);
            txtFinalAmount.TextChanged += new EventHandler(RoundOff);
            txtTaxAmnt.TextChanged += new EventHandler(GrandTotalCalculation);
            txtOtherCharges.TextChanged += new EventHandler(GrandTotalCalculation);
            txtAmountAfterDis.TextChanged += new EventHandler(GrandTotalCalculation);
            txtFinalAmount.TextChanged += new EventHandler(BalanceAmount);
            txt_TotalCashPaid.TextChanged += new EventHandler(BalanceAmount);
            txtBookedGoldRate.TextChanged += new EventHandler(AdvanceDetails1);
            txtGoldlRate.TextChanged += new EventHandler(AdvanceDetails1);
            //txt_TotalGoldWt.TextChanged += new EventHandler(AdvanceDetails1);
            txtBookedWt.TextChanged += new EventHandler(AdvanceDetails1);
            ////---------------------------------------------------------------
            //this.prnDialog = new System.Windows.Forms.PrintDialog();
            //this.prnPreview = new System.Windows.Forms.PrintPreviewDialog();
            //this.prnDocument = new System.Drawing.Printing.PrintDocument();
            //// The Event of 'PrintPage'
            //prnDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(prnDocument_PrintPage);
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
            txtcompId.Text = "1";
            txtBranchID. Text = "1";
            base.RefreshData();
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            cmb_VoucherTypeId.SelectedValue = 9;
            if (!IsEditMode)
                txtInvNo.Text = JMS.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtp_InvDate.Value,
                DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));

            // MASTER                    
            Gramboo.General.Setupcombo(Cmb_CustName, "CRM.CustomerMaster", "CustName", "CustId", "IsActive='True' AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
            Gramboo.General.Setupcombo(Cmb_SalesmanName, "EMP.EmployeeMaster", "EmpName", "EmpId", "IsActive='True' AND Company_id=" + txtcompId.Text);
            Gramboo.General.Setupcombo(Cmb_SupervisiorName, "EMP.EmployeeMaster", "EmpName", "EmpId", "IsActive='True' AND Company_id=" + txtcompId.Text);
            Gramboo.General.Setupcombo(Cmb_BookingNo, "SALE.GoldBooking", "VchNo", "BookId", "IsActive='True' AND Company_id=" + txtcompId.Text);
            Gramboo.General.Setupcombo(cmb_purity, "ITM.VPurityMaster", "[Purity Name]", "PurityId", "[Is Active]='True'");
            Gramboo.General.Setupcombo(Cmb_PurityReturn, "ITM.VPurityMaster", "[Purity Name]", "PurityId", "[Is Active]='True'");
            Gramboo.General.Setupcombo(Cmb_SchemeNo, "SALE.SavingSchemeJoinEntry", "JoinNo", "JoinId", "[IsActive]='True'");
            Gramboo.General.Setupcombo(cmb_BankId, "GEN.BankMaster", "BankName", "BankId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmbCreditCardId, "GEN.CreditCardMaster", "CreditCardName", "CreditCardID", "IsActive='True'");
            Gramboo.General.Setupcombo(Cmb_SalesType, "SALE.SalesTypeMaster", "SalesTypeName", "SalesTypeId", "IsActive='True' AND SalesTypeId= (SELECT MAX(SalesTypeId) FROM SALE.SalesTypeMaster)");

      
            //TAX MASTER
            Gramboo.General.Setupcombo(cmb_Taxname, "PUR.TaxMaster", "TaxName", "TaxId", "IsActive='True'");
            //OTHER CHARGES
            Gramboo.General.Setupcombo(Cmb_Chargename, "PUR.MiscChargeMaster", "ChargeName", "ChargeId", "IsActive='True'");
           
           //SALES DETAILS 
            Gramboo.General.Setupcombo(cmb_modelname, "ITM.ModelMaster", "ModelName", "ModelId", "IsActive='True' AND Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
          //SALES RETURN
           Gramboo.General.Setupcombo(Cmb_ModelNameReturn, "ITM.ModelMaster", "ModelName", "ModelId", "IsActive='True' AND Company_id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
            //SALES EXCHANGE
           Gramboo.General.Setupcombo(Cmb_ItemName_Ex, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True'");

           //txtDeptName.Text = Convert.ToString(((frmMain)this.ParentForm).dept);
           txtDeptName.Text = "11010001";
           if (dtp_InvDate.Value != null)
            {

                txtSilverRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["SilverRate"]).ToString();
                txtGoldlRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString();
                txtPlatinumRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 PlatinumRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["PlatinumRate"]).ToString();

            }
            SalesTax();
        }
        public override bool InitializeTables()
        {
            Table t = new Table("JMS", "SALE", "SalesMaster");
            t.PrimaryKeys.Add("SalesId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = TxtSaleId;
            Table t1 = new Table("JMS", "SALE", "SalesDetails", true);
            t1.PrimaryKeys.Add("TransId");
            t1.FillView = new Table("JMS", "SALE", "VSalesDetails", true);
            t1.DatagridView = dgv_SalesDetails;
            t1.IsDatagridView = true;
            t1.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t1);

            Table t5 = new Table("JMS", "SALE", "SalesDetails", true);
            t5.PrimaryKeys.Add("TransId");
            t5.FillView = new Table("JMS", "SALE", "VSalesReturnDetails", true);
            t5.DatagridView = dgv_Return;
            t5.IsDatagridView = true;
            t5.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t5);

            Table t6 = new Table("JMS", "SALE", "SalesDetails", true);
            t6.PrimaryKeys.Add("TransId");
            t6.FillView = new Table("JMS", "SALE", "VSalesExchange", true);
            t6.DatagridView = dgv_Exchange;
            t6.IsDatagridView = true;
            t6.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t6);  
 

            Table t2 = new Table("JMS", "SALE", "TaxDetails", true);
            t2.PrimaryKeys.Add("TransId");
            t2.FillView = new Table("JMS", "SALE", "VTaxDetails_Sale", true);
            t2.DatagridView = dgv_TaxDetails;
            t2.IsDatagridView = true;
            t2.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t2);

            Table t3 = new Table("JMS", "SALE", "OtherDetails", true);
            t3.PrimaryKeys.Add("TransId");
            t3.FillView = new Table("JMS", "SALE", "VOtherDetails_Sale", true);
            t3.DatagridView = dgv_otherChg;
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
            // this.addNewMasterData1.MasterForm = JMS.Forms.CRM.CustomerMasterr.Instance;
            //   this.addNewMasterData1.ParentForm = SalesMaster.Instance;
            txtInvNo.Focus();
            dgv_SalesDetails.IsDataEntryGrid = true;
            dgv_Return.IsDataEntryGrid = true;
            dgv_Exchange.IsDataEntryGrid = true; 
            //RbtMetalType.DefaultRadioButton = Rb_Gold;

            SalesType();

            dgv_Exchange.DataFields = new List<string>() {"SalesId","TransId","ProdCodeId","ItemId","[ItemName]",
            "ModelId","PurityID","PurityValue","Touch", 
            "Qty","Gwt","MetalRate","[MetalCash]","[DiaNo]","[DiaWt]","[DiaCash]","[StoneWt]","[StoneCash]",
            "MC","Wastage","NetWt","[WastageCash]","VA","TotalAmount","IsReceipt","Type" };
            dgv_Exchange.HiddenDataFields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "IsReceipt", 
            "Type","ModelName","PurityName","Touch","MC","Wastage","WastageCash","VA","Type" };
            dgv_Exchange.SummaryColumns = new string[] { "Qty", "NetWt","MetalCash", "DiaNo", "DiaCash", 
            "DiaWt", "StoneCash", "StoneWt","Gwt", "TotalAmount" };
            dgv_Exchange.Fill(new Table("JMS", "SALE", "VSalesExchange", true), "1=2");
            dgv_Exchange.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            AdjustColumnWidthExchange();
            SaleExchange = -1;

            dgv_Exchange.RowTemplate.Height = Cmb_ItemName_Ex.Height;
            dgv_Exchange.AllowUserToAddRows = false;
            ((System.Data.DataTable)dgv_Exchange.DataSource).Rows.Add(((System.Data.DataTable)dgv_Exchange.DataSource).NewRow());
            dgv_Exchange.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv_Exchange.CurrentCell = dgv_Exchange.Rows[0].Cells["ItemName"];
            dgv_Exchange.BeginEdit(true);
            txtMRP.Text = "";
            SaleExchange = -1;

            //dgv_SalesDetails.DataFields = new List<string>() {"SalesId","TransId","ProdCodeId","ProdCode","ItemId","[ItemName]",
            //    "ModelId","[ModelName]","PurityID","PurityName","Touch", 
            //    "Qty","Gwt","MetalRate","[MetalCash]","[DiaNo]","[DiaWt]","[DiaCash]","[StoneWt]","[StoneCash]",
            //    "MC","Wastage","[NetWt]","[WastageCash]","VA","TotalAmount","IsReceipt" };
            //dgv_SalesDetails.HiddenDataFields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "IsReceipt" };
            //dgv_SalesDetails.SummaryColumns = new string[] { "Qty", "Net Wt","MetalCash", "DiaNo", "DiaCash", 
            //    "DiaWt", "StoneNo", "StoneCash", "StoneWt", "MC", "Wastage", "WastageCash","VA","Gwt", "TotalAmount" };
            //dgv_SalesDetails.Fill(new Table("JMS", "SALE", "VSalesDetails", true), "1=2");
            //dgv_SalesDetails.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            //SaleCurrentRow = -1;

            //dgv_Return.DataFields = new List<string>() {"SalesId","TransId","ProdCodeId","ProdCode","ItemId", "[ItemName]",
            //   "ModelId","[ModelName]","PurityID","PurityName","Touch", 
            //   "Qty","Gwt","MetalRate","[MetalCash]","[DiaNo]","[DiaWt]","[DiaCash]","[StoneWt]","[StoneCash]",
            //   "MC","Wastage","[NetWt]","[WastageCash]","VA","TotalAmount","IsReceipt" };
            //dgv_Return.HiddenDataFields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "IsReceipt" };
            //dgv_Return.SummaryColumns = new string[]{ "Qty", "Net Wt","MetalCash", "DiaNo", "DiaCash", 
            //   "DiaWt", "StoneNo", "StoneCash", "StoneWt", "MC", "Wastage", "WastageCash","VA","Gwt", "TotalAmount" };
            //dgv_Return.Fill(new Table("JMS", "SALE", "VSalesReturnDetails", true), "1=2");
            //dgv_Return.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            //SaleCurrentretRow = -1;


            //CalcFlag = true;

            //dgv_SalesDetails.RowTemplate.Height = Cmb_ProdCode_SDtl.Height;
            //dgv_SalesDetails.AllowUserToAddRows = false;
            //((System.Data.DataTable)dgv_SalesDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_SalesDetails.DataSource).NewRow());
            //dgv_SalesDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //dgv_SalesDetails.CurrentCell = dgv_SalesDetails.Rows[0].Cells["ProdCode"];
            //dgv_SalesDetails.BeginEdit(true);
            //txtMRP.Text = "";
            //SaleCurrentRow = -1;


            //dgv_Return.RowTemplate.Height = Cmb_ProdCodeReturn.Height;
            //dgv_Return.AllowUserToAddRows = false;
            ////if (dgv_Return.Rows.Count == 0)
            //((System.Data.DataTable)dgv_Return.DataSource).Rows.Add(((System.Data.DataTable)dgv_Return.DataSource).NewRow());

            //dgv_Return.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //dgv_Return.CurrentCell = dgv_Return.Rows[0].Cells["ProdCode"];
            //dgv_Return.BeginEdit(true);


            //SaleCurrentretRow = -1;


            dgv_TaxDetails.SummaryRowVisible = true;
            dgv_TaxDetails.DataFields = new List<string>() { "TransId", "SalesId", "TaxId", "TaxName", "TaxRate", "TaxAmount" };
            dgv_TaxDetails.HiddenDataFields = new List<string>() { "TransId", "SalesId", "TaxId" };
            dgv_TaxDetails.SummaryColumns = new string[] { "TaxAmount", "TaxRate" };
            dgv_TaxDetails.Fill(new Table("JMS", "SALE", "VTaxDetails_Sale", true), "1=2");
            dgv_TaxDetails.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            AdjustColumnWidthsTaxDetails();

            dgv_otherChg.SummaryRowVisible = true;
            dgv_otherChg.DataFields = new List<string>() { "TransId", "SalesId", "OtherChrgId", "ChargeName", "Amount", "CalculateTax" };
            dgv_otherChg.HiddenDataFields = new List<string>() { "TransId", "SalesId", "OtherChrgId" };
            dgv_otherChg.SummaryColumns = new string[] { "Amount" };
            dgv_otherChg.Fill(new Table("JMS", "SALE", "VOtherDetails_Sale", true), "1=2");
            dgv_otherChg.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            AdjustColumnWidthsOtherDetails();

           // dgv_SalesDetails.Columns["TotalAmount"].ReadOnly = true;



            //this.ListForm = SalesEntryList.Instance;
            if (this.TableName != null)
                GenerateID(this.TableName);
            CalcFlag = false;
        }
        private void AdjustColumnWidthsTaxDetails()
        {
            dgv_TaxDetails.RowHeadersVisible = false;
            dgv_TaxDetails.Columns[0].Width = 40;
            dgv_TaxDetails.Columns["TaxName"].Width = cmb_Taxname.Width + 10;
            dgv_TaxDetails.Columns["taxRate"].Width = txtRate_TaxDetails.Width + 5;
            dgv_TaxDetails.Columns["TaxAmount"].Width = TxtAmount_TaxDetails.Width + 5;
        }

        private void AdjustColumnWidthsOtherDetails()
        {
            dgv_otherChg.RowHeadersVisible = false;
            dgv_otherChg.Columns[0].Width = 40;
            dgv_otherChg.Columns["ChargeName"].Width = Cmb_Chargename.Width + 10;
            dgv_otherChg.Columns["Amount"].Width = txtAmount_OtherCharge.Width + 5;
            dgv_otherChg.Columns["CalculateTax"].Width = ChkCalCulateTax.Width + 15;
        }


        private void txtAmountAfterDis_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void lnkPayMode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pnlPayment.Visible == false)
            {
                pnlPayment.Size = new System.Drawing.Size(750, 250);
                pnlPayment.Parent = this;
                pnlPayment.Visible = true;
                pnlPayment.BringToFront();

                pnlPayment.Location = new Point(lnkPayMode.Location.X-200  + lnkPayMode.Parent.Location.X-200 ,
                lnkPayMode.Parent.Location.Y - 200 + lnkPayMode.Location.Y - 200);

                pnlPayment.Show();
                pnlPayment.BringToFront();
                lnkPayMode.Text = " Hide Payment Details";

            }
            else
            {
                pnlPayment.Visible = false;
                pnlPayment.SendToBack();
                pnlPayment.Hide();
                lnkPayMode.Text = "Show Payment Details";

            }
        }

        private void lnkBookedDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PnlBookedDetails.Visible == false)
            {
                PnlBookedDetails.Size = new System.Drawing.Size(300, 150);
                PnlBookedDetails.Parent = this;
                PnlBookedDetails.Visible = true;
                PnlBookedDetails.BringToFront();

                PnlBookedDetails.Location = new Point(lnkBookedDetails.Location.X + 100 + lnkBookedDetails.Parent.Location.X + 50,
                lnkBookedDetails.Parent.Location.Y - 100 + lnkBookedDetails.Location.Y - 0);

                PnlBookedDetails.Show();
                PnlBookedDetails.BringToFront();
                lnkBookedDetails.Text = " Hide Booked Details";

            }
            else
            {
                PnlBookedDetails.Visible = false;
                PnlBookedDetails.SendToBack();
                PnlBookedDetails.Hide();
                lnkBookedDetails.Text = "Show Booked Details";

            }
        }

        private void Cmb_CustName_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Cmb_CustName.SelectedValue != null)
            {
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
               ("Select Addr1,Addr2,[Mobile No] FROM CRM.VCustomerMasterList WHERE CustId=" + Cmb_CustName.SelectedValue)).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtAddress1.Text = (dt.Rows[0]["Addr1"] == null ? "" : dt.Rows[0]["Addr1"].ToString());
                        txtAddress2.Text = (dt.Rows[0]["Addr2"] == null ? "" : dt.Rows[0]["Addr2"].ToString());
                        txtPhoneNo.Text = (dt.Rows[0]["Mobile No"] == null ? "" : dt.Rows[0]["Mobile No"].ToString());
                    }

                }

                txtCustId.Text = Cmb_CustName.SelectedValue.ToString();
            }
            else
            {
                txtCustId.Text = "0";
            }
        }

        private void dgv_SalesDetails_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_SalesDetails.SelectedCells.Count > 0)
            {
                if (dgv_SalesDetails.SelectedCells[0].OwningColumn.DataPropertyName == "ProdCode")
                {
                    try
                    {
                        SaleCurrentRow = dgv_SalesDetails.SelectedCells[0].RowIndex;
                        Cmb_ProdCode_SDtl.Parent = tabPage1;
                        Cmb_ProdCode_SDtl.Visible = true;
                        Cmb_ProdCode_SDtl.Text = dgv_SalesDetails.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_SalesDetails.GetCellDisplayRectangle(dgv_SalesDetails.SelectedCells[0].ColumnIndex, dgv_SalesDetails.SelectedCells[0].RowIndex, true).Location;
                        Cmb_ProdCode_SDtl.Location = new System.Drawing.Point(p.X + dgv_SalesDetails.Parent.Location.X, p.Y + dgv_SalesDetails.Parent.Location.Y);


                        Cmb_ProdCode_SDtl.Size = dgv_SalesDetails.GetCellDisplayRectangle(dgv_SalesDetails.SelectedCells[0].ColumnIndex, dgv_SalesDetails.SelectedCells[0].RowIndex, true).Size;
                        Cmb_ProdCode_SDtl.BringToFront();
                        Cmb_ProdCode_SDtl.Focus();
                    }
                    catch { }
                }

                if (dgv_SalesDetails.SelectedCells[0].OwningColumn.DataPropertyName == "ItemName")
                {
                    try
                    {
                        SaleCurrentRow = dgv_SalesDetails.SelectedCells[0].RowIndex;
                        Cmb_ItemName.Visible = true;
                        Cmb_ItemName.Text = dgv_SalesDetails.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_SalesDetails.GetCellDisplayRectangle(dgv_SalesDetails.SelectedCells[0].ColumnIndex, dgv_SalesDetails.SelectedCells[0].RowIndex, true).Location;
                        Cmb_ItemName.Location = new System.Drawing.Point(p.X + dgv_SalesDetails.Parent.Location.X, p.Y + dgv_SalesDetails.Parent.Location.Y);


                        Cmb_ItemName.Size = dgv_SalesDetails.GetCellDisplayRectangle(dgv_SalesDetails.SelectedCells[0].ColumnIndex, dgv_SalesDetails.SelectedCells[0].RowIndex, true).Size;
                        Cmb_ItemName.BringToFront();
                        Cmb_ItemName.Focus();
                    }
                    catch { }
                }

                if (dgv_SalesDetails.SelectedCells[0].OwningColumn.DataPropertyName == "ModelName")
                {
                    try
                    {
                        SaleCurrentRow = dgv_SalesDetails.SelectedCells[0].RowIndex;
                        cmb_modelname.Visible = true;
                        cmb_modelname.Text = dgv_SalesDetails.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_SalesDetails.GetCellDisplayRectangle(dgv_SalesDetails.SelectedCells[0].ColumnIndex, dgv_SalesDetails.SelectedCells[0].RowIndex, true).Location;
                        cmb_modelname.Location = new System.Drawing.Point(p.X + dgv_SalesDetails.Parent.Location.X, p.Y + dgv_SalesDetails.Parent.Location.Y);


                        cmb_modelname.Size = dgv_SalesDetails.GetCellDisplayRectangle(dgv_SalesDetails.SelectedCells[0].ColumnIndex, dgv_SalesDetails.SelectedCells[0].RowIndex, true).Size;
                        cmb_modelname.BringToFront();
                        cmb_modelname.Focus();
                    }
                    catch { }
                }
                if (dgv_SalesDetails.SelectedCells[0].OwningColumn.DataPropertyName == "PurityName")
                {
                    try
                    {
                        SaleCurrentRow = dgv_SalesDetails.SelectedCells[0].RowIndex;
                        cmb_purity.Visible = true;
                        cmb_purity.Text = dgv_SalesDetails.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_SalesDetails.GetCellDisplayRectangle(dgv_SalesDetails.SelectedCells[0].ColumnIndex, dgv_SalesDetails.SelectedCells[0].RowIndex, true).Location;
                        cmb_purity.Location = new System.Drawing.Point(p.X + dgv_SalesDetails.Parent.Location.X, p.Y + dgv_SalesDetails.Parent.Location.Y);


                        cmb_purity.Size = dgv_SalesDetails.GetCellDisplayRectangle(dgv_SalesDetails.SelectedCells[0].ColumnIndex, dgv_SalesDetails.SelectedCells[0].RowIndex, true).Size;
                        cmb_purity.BringToFront();
                        cmb_purity.Focus();
                    }
                    catch { }
                }
            }



        }

        private void dgv_Return_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_Return.SelectedCells.Count > 0)
            {
                if (dgv_Return.SelectedCells[0].OwningColumn.DataPropertyName == "ProdCode")
                {
                    try
                    {
                        SaleCurrentretRow = dgv_Return.SelectedCells[0].RowIndex;
                        Cmb_ProdCodeReturn.Parent = tabPage3;
                        Cmb_ProdCodeReturn.Visible = true;
                        Cmb_ProdCodeReturn.Text = dgv_Return.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_Return.GetCellDisplayRectangle(dgv_Return.SelectedCells[0].ColumnIndex, dgv_Return.SelectedCells[0].RowIndex, true).Location;
                        Cmb_ProdCodeReturn.Location = new System.Drawing.Point(p.X + dgv_Return.Parent.Location.X, p.Y + dgv_Return.Parent.Location.Y);


                        Cmb_ProdCodeReturn.Size = dgv_Return.GetCellDisplayRectangle(dgv_Return.SelectedCells[0].ColumnIndex, dgv_Return.SelectedCells[0].RowIndex, true).Size;
                        Cmb_ProdCodeReturn.BringToFront();
                        Cmb_ProdCodeReturn.Focus();
                    }
                    catch { }
                }
                if (dgv_Return.SelectedCells[0].OwningColumn.DataPropertyName == "ItemName")
                {
                    try
                    {
                        SaleCurrentretRow = dgv_Return.SelectedCells[0].RowIndex;
                        Cmb_ItemNameReturn.Visible = true;
                        Cmb_ItemNameReturn.Text = dgv_Return.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_Return.GetCellDisplayRectangle(dgv_Return.SelectedCells[0].ColumnIndex, dgv_Return.SelectedCells[0].RowIndex, true).Location;
                        Cmb_ItemNameReturn.Location = new System.Drawing.Point(p.X + dgv_Return.Parent.Location.X, p.Y + dgv_Return.Parent.Location.Y);


                        Cmb_ItemNameReturn.Size = dgv_Return.GetCellDisplayRectangle(dgv_Return.SelectedCells[0].ColumnIndex, dgv_Return.SelectedCells[0].RowIndex, true).Size;
                        Cmb_ItemNameReturn.BringToFront();
                        Cmb_ItemNameReturn.Focus();
                    }
                    catch { }
                }
                if (dgv_Return.SelectedCells[0].OwningColumn.DataPropertyName == "PurityName")
                {
                    try
                    {
                        SaleCurrentretRow = dgv_Return.SelectedCells[0].RowIndex;
                        Cmb_PurityReturn.Visible = true;
                        Cmb_PurityReturn.Text = dgv_Return.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_Return.GetCellDisplayRectangle(dgv_Return.SelectedCells[0].ColumnIndex, dgv_Return.SelectedCells[0].RowIndex, true).Location;
                        Cmb_PurityReturn.Location = new System.Drawing.Point(p.X + dgv_Return.Parent.Location.X, p.Y + dgv_Return.Parent.Location.Y);


                        Cmb_PurityReturn.Size = dgv_Return.GetCellDisplayRectangle(dgv_Return.SelectedCells[0].ColumnIndex, dgv_Return.SelectedCells[0].RowIndex, true).Size;
                        Cmb_PurityReturn.BringToFront();
                        Cmb_PurityReturn.Focus();
                    }
                    catch { }
                }
                if (dgv_Return.SelectedCells[0].OwningColumn.DataPropertyName == "ModelName")
                {
                    try
                    {
                        SaleCurrentretRow = dgv_Return.SelectedCells[0].RowIndex;
                        Cmb_ModelNameReturn.Visible = true;
                        Cmb_ModelNameReturn.Text = dgv_Return.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_Return.GetCellDisplayRectangle(dgv_Return.SelectedCells[0].ColumnIndex, dgv_Return.SelectedCells[0].RowIndex, true).Location;
                        Cmb_ModelNameReturn.Location = new System.Drawing.Point(p.X + dgv_Return.Parent.Location.X, p.Y + dgv_Return.Parent.Location.Y);


                        Cmb_ModelNameReturn.Size = dgv_Return.GetCellDisplayRectangle(dgv_Return.SelectedCells[0].ColumnIndex, dgv_Return.SelectedCells[0].RowIndex, true).Size;
                        Cmb_ModelNameReturn.BringToFront();
                        Cmb_ModelNameReturn.Focus();
                    }
                    catch { }

                }
                else
                {
                    //SaleCurrentretRow = -1;
                    //Cmb_ProdCodeReturn.Visible = false;
                }
            }
        }

        private void Cmb_ProdCode_SDtl_Leave(object sender, EventArgs e)
        {
            bool flag = false;

            if (FillFlag || txtcompId.Text == "")
                return;
            Cmb_ProdCode_SDtl.Visible = false;
            if (Cmb_ProdCode_SDtl.SelectedValue != null && Cmb_ProdCode_SDtl.Text.Trim().Length > 0 && dtp_InvDate.Value != null)
            {
                MRP_Details();
                if (((System.Data.DataTable)dgv_SalesDetails.DataSource).Select("ProdCodeId=" + Cmb_ProdCode_SDtl.SelectedValue).Length == 0)
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                        ("SELECT * FROM SALE.AddToSalesDetails(" + Cmb_ProdCode_SDtl.SelectedValue + ",'" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (SaleCurrentRow > -1)
                            {
                                DataRow r = ((System.Data.DataTable)dgv_SalesDetails.DataSource).Rows[SaleCurrentRow];

                                if (dgv_SalesDetails.Rows.Count > SaleCurrentRow)
                                {
                                    foreach (DataGridViewColumn c in dgv_SalesDetails.Columns)
                                    {
                                        if (c.IsDataBound)
                                            r[c.DataPropertyName] = dt.Rows[0][c.DataPropertyName];
                                    }
                                }
                                r.AcceptChanges();
                                flag = true;
                                dgv_SalesDetails.EndEdit();
                            }
                        }
                    }

                }
                //Summary();
            }

            if (dgv_SalesDetails.Rows.Count > 0)
            {
                if (dgv_SalesDetails.Rows[dgv_SalesDetails.Rows.Count - 1].Cells["ProdCodeId"].Value.ToString() != "")
                {
                    ((System.Data.DataTable)dgv_SalesDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_SalesDetails.DataSource).NewRow());

                    dgv_SalesDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv_SalesDetails.CurrentCell = dgv_SalesDetails.Rows[SaleCurrentRow + 1].Cells["ProdCode"];

                }

            }
            //Commented*******************
            // dgv_SalesDetails.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedHeaders);

            //if (Convert.ToSingle(txtMRP.Text) == 0)
            //{
            //    AdvanceDetails1(sender, e);
            //}
        }

        private void Cmb_ItemName_Leave(object sender, EventArgs e)
        {
            Cmb_ItemName.Visible = false;
            if (Cmb_ItemName.Text != "" && Cmb_ItemName.SelectedValue != null)
            {
                dgv_SalesDetails.CurrentRow.Cells["ItemId"].Value = Cmb_ItemName.SelectedValue.ToString();
                dgv_SalesDetails.CurrentRow.Cells["ItemName"].Value = Cmb_ItemName.Text;
                //  enable();
            }
        }
        private void cmb_purity_Leave(object sender, EventArgs e)
        {
            if (cmb_purity.SelectedValue != null)
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select [Purity Value] FROM ITM.VPurityMaster WHERE PurityId=" + cmb_purity.SelectedValue + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        dgv_SalesDetails.CurrentRow.Cells["Touch"].Value = dt.Rows[0]["Purity Value"].ToString();

                    }
                    dgv_SalesDetails.CurrentRow.Cells["PurityId"].Value = cmb_purity.SelectedValue.ToString();
                    dgv_SalesDetails.CurrentRow.Cells["PurityName"].Value = cmb_purity.Text;
                }
            }
            cmb_purity.Visible = false;
        }

        private void cmb_modelname_Leave(object sender, EventArgs e)
        {
            if (cmb_modelname.SelectedValue != null)
            {
                dgv_SalesDetails.CurrentRow.Cells["ModelId"].Value = cmb_modelname.SelectedValue.ToString();
                dgv_SalesDetails.CurrentRow.Cells["ModelName"].Value = cmb_modelname.Text;
            }
            cmb_modelname.Visible = false;
        }

        private void TaxDetails_Button_Click(object sender, EventArgs e)
        {
            cmb_Taxname.Focus();
            if (cmb_Taxname.SelectedValue != null)
            {
                txtTaxId.Text = cmb_Taxname.SelectedValue.ToString();
                dgv_TaxDetails.Save();

            }
        }

        private void btn_Others_Click(object sender, EventArgs e)
        {
            Cmb_Chargename.Focus();
            if (Cmb_Chargename.SelectedValue != null)
            {
                txtOtherChrgId.Text = Cmb_Chargename.SelectedValue.ToString();
                dgv_otherChg.Save();
            }
            UpdateTax();
        }
        public void CalculateTax()
        {
            float Amount = 0;
            foreach (DataGridViewRow r in dgv_otherChg.Rows)
            {
                if ((bool)r.Cells["CalculateTax"].Value == true)
                {
                    Amount += Convert.ToSingle(r.Cells["Amount"].Value.ToString());
                }
            }
            txtCalculatetaxAmount.Text = Amount.ToString();
        }
        public void TaxDetails_Amount()
        {
            float totAftrDis = 0, rate = 0, amount = 0, calTax = 0, caltaxtotal = 0;
            txtCalculatetaxAmount.Text = (string.IsNullOrEmpty(txtCalculatetaxAmount.Text.Trim()) ? "0.00" : txtCalculatetaxAmount.Text);

            if (txtRate_TaxDetails.Text != "" && txtAmountAfterDis.Text != "" && txtCalculatetaxAmount.Text != "")
            {
                totAftrDis = Convert.ToSingle(txtAmountAfterDis.Text);
                calTax = Convert.ToSingle(txtCalculatetaxAmount.Text);
                caltaxtotal = totAftrDis + calTax;
                rate = Convert.ToSingle(txtRate_TaxDetails.Text);
                amount = caltaxtotal * rate / 100;
                txtTaxAmnt.Text = amount.ToString();
            }

            UpdateTax();
        }
        private void UpdateTax()
        {

            foreach (DataGridViewRow r in dgv_TaxDetails.Rows)
            {
                float totAftrDis = 0, rate = 0, amount = 0, calTax = 0, caltaxtot = 0;
                if (r.Cells["TaxRate"].Value != "" && txtAmountAfterDis.Text != "" && txtCalculatetaxAmount.Text != "")
                {
                    totAftrDis = Convert.ToSingle(txtAmountAfterDis.Text);
                    calTax = Convert.ToSingle(txtCalculatetaxAmount.Text);
                    caltaxtot = totAftrDis + calTax;
                    rate = Convert.ToSingle(r.Cells["TaxRate"].Value);
                    amount = caltaxtot * rate / 100;
                    r.Cells["TaxAmount"].Value = amount.ToString();
                }
            }
            txtTaxRatePerc.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxRate"].Text;
            txtTaxAmnt.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxAmount"].Text;
        }
        private void SalesTax()
        {

            if (Cmb_SalesType.SelectedValue != null)
            {
                dgv_TaxDetails.DataSource = this.DBConn.GetData(new SqlCommand("Select CAST(0  as bigint ) AS TransId, CAST(0  as bigint )   AS SalesId,TaxId,TaxName,TaxRate,TaxAmount from SALE.VSalesApplicableMaster  WHERE SalesTypeId='" + Cmb_SalesType.SelectedValue + "'")).Tables[0];

                UpdateTax();
            }
        }

        private void dgv_otherChg_SummaryCalculated(object source, EventArgs e)
        {
            txtOtherCharges.Text = dgv_otherChg.SummaryRow.SummaryCells["Amount"].Text;
            CalculateTax();
            UpdateTax();
        }

        private void Cmb_SalesType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Cmb_SalesType.SelectedValue != null)
            {
                SalesTax();
            }
        }

        private void dgv_SalesDetails_SummaryCalculated(object source, EventArgs e)
        {
            txt_TotalNetWt.Text = dgv_SalesDetails.SummaryRow.SummaryCells["NetWt"].Text;
            txt_TotalMetalCash.Text = dgv_SalesDetails.SummaryRow.SummaryCells["MetalCash"].Text;
            txt_TotalStoneWt.Text = dgv_SalesDetails.SummaryRow.SummaryCells["StoneWt"].Text;
            txt_TotalStoneCash.Text = dgv_SalesDetails.SummaryRow.SummaryCells["StoneCash"].Text;
            txt_TotalDiaNo.Text = dgv_SalesDetails.SummaryRow.SummaryCells["DiaNo"].Text;
            txt_TotalDiaWt.Text = dgv_SalesDetails.SummaryRow.SummaryCells["Diawt"].Text;
            txt_TotalDiaCash.Text = dgv_SalesDetails.SummaryRow.SummaryCells["DiaCash"].Text;
            txt_TotalMC.Text = dgv_SalesDetails.SummaryRow.SummaryCells["MC"].Text;
            txt_TotalWastage.Text = dgv_SalesDetails.SummaryRow.SummaryCells["Wastage"].Text;
            txt_TotalWastageCash.Text = dgv_SalesDetails.SummaryRow.SummaryCells["WastageCash"].Text;
            txt_TotalVA.Text = dgv_SalesDetails.SummaryRow.SummaryCells["VA"].Text;
            txt_TotalTotalWt.Text = dgv_SalesDetails.SummaryRow.SummaryCells["Gwt"].Text;
            txtProductVal.Text = dgv_SalesDetails.SummaryRow.SummaryCells["TotalAmount"].Text;



        }

        private void dgv_TaxDetails_SummaryCalculated(object source, EventArgs e)
        {
            txtTaxRatePerc.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxRate"].Text;
            txtTaxAmnt.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxAmount"].Text;
        }

        private void Cmb_ProdCodeReturn_Leave(object sender, EventArgs e)
        {
            bool flag = false;

            if (FillFlag || txtcompId.Text == "")
                return;
            Cmb_ProdCodeReturn.Visible = false;
            if (Cmb_ProdCodeReturn.SelectedValue != null && Cmb_ProdCodeReturn.Text.Trim().Length > 0)
            {
                MRP_DetailsReturn();
                if (((System.Data.DataTable)dgv_Return.DataSource).Select("ProdCodeId=" + Cmb_ProdCodeReturn.SelectedValue).Length == 0)
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                        ("SELECT * FROM SALE.AddToSalesReturn(" + Cmb_ProdCodeReturn.SelectedValue + ",'" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (SaleCurrentretRow > -1)
                            {
                                DataRow r = ((System.Data.DataTable)dgv_Return.DataSource).Rows[SaleCurrentretRow];

                                if (dgv_Return.Rows.Count > SaleCurrentretRow)
                                {
                                    foreach (DataGridViewColumn c in dgv_Return.Columns)
                                    {
                                        if (c.IsDataBound)
                                            r[c.DataPropertyName] = dt.Rows[0][c.DataPropertyName];
                                    }
                                }
                                r.AcceptChanges();
                                flag = true;
                                dgv_Return.EndEdit();
                            }
                        }
                    }

                }
            }
            if (dgv_Return.Rows.Count > 0)
            {
                if (dgv_Return.Rows[dgv_Return.Rows.Count - 1].Cells["ProdCodeId"].Value.ToString() != "")
                {
                    ((System.Data.DataTable)dgv_Return.DataSource).Rows.Add(((System.Data.DataTable)dgv_Return.DataSource).NewRow());

                    dgv_Return.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv_Return.CurrentCell = dgv_Return.Rows[SaleCurrentretRow + 1].Cells["ProdCode"];

                }

            }
            //  dgv_Return.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedHeaders);

            //if (Convert.ToSingle(txtMRP_Return.Text)==0)
            //{
            //    AdvanceDetails2(sender, e);
            //}

        }

        private void Cmb_ItemNameReturn_Leave(object sender, EventArgs e)
        {
            Cmb_ItemNameReturn.Visible = false;
            if (dgv_Return.CurrentRow == null)
                return;

            if (Cmb_ItemNameReturn.Text != "" && Cmb_ItemNameReturn.SelectedValue != null)
            {
                dgv_Return.CurrentRow.Cells["ItemId"].Value = Cmb_ItemNameReturn.SelectedValue.ToString();
                dgv_Return.CurrentRow.Cells["ItemName"].Value = Cmb_ItemNameReturn.Text;
            }
        }
        private void Cmb_PurityReturn_Leave(object sender, EventArgs e)
        {
            if (Cmb_PurityReturn.SelectedValue != null)
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select [Purity Value] FROM ITM.VPurityMaster WHERE PurityId=" + Cmb_PurityReturn.SelectedValue + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        dgv_Return.CurrentRow.Cells["Touch"].Value = dt.Rows[0]["Purity Value"].ToString();

                    }
                    dgv_Return.CurrentRow.Cells["PurityId"].Value = Cmb_PurityReturn.SelectedValue.ToString();
                    dgv_Return.CurrentRow.Cells["PurityName"].Value = Cmb_PurityReturn.Text;
                }
            }
            Cmb_PurityReturn.Visible = false;
        }
        private void Cmb_ModelNameReturn_Leave(object sender, EventArgs e)
        {
            if (Cmb_ModelNameReturn.SelectedValue != null)
            {
                dgv_Return.CurrentRow.Cells["ModelId"].Value = Cmb_ModelNameReturn.SelectedValue.ToString();
                dgv_Return.CurrentRow.Cells["ModelName"].Value = Cmb_ModelNameReturn.Text;
            }
            Cmb_ModelNameReturn.Visible = false;
        }
        private void PaymentTotalAmount(object sender, EventArgs e)
        {
            float cash = 0, credit = 0, cheque = 0, others = 0, Total = 0;

            txtCheque.Text = (string.IsNullOrEmpty(txtCheque.Text.Trim()) ? "0.00" : txtCheque.Text);
            txtCash.Text = (string.IsNullOrEmpty(txtCash.Text.Trim()) ? "0.00" : txtCash.Text);
            txtCredit.Text = (string.IsNullOrEmpty(txtCredit.Text.Trim()) ? "0.00" : txtCredit.Text);
            txtOtherspaid.Text = (string.IsNullOrEmpty(txtOtherspaid.Text.Trim()) ? "0.00" : txtOtherspaid.Text);
            cash = Convert.ToSingle(txtCash.Text);
            credit = Convert.ToSingle(txtCredit.Text);
            cheque = Convert.ToSingle(txtCheque.Text);
            others = Convert.ToSingle(txtOtherspaid.Text);
            Total = cash + credit + cheque + others;
            txt_TotalPaidAmount.Text = Total.ToString();
        }
        public void TotalCashPaid(object sender, EventArgs e)
        {
            float OldGold = 0, paidAmt = 0, retAmt = 0, CashPaid = 0, AdvPaid = 0, Schemepaid = 0;
            txt_OldGoldReceipt.Text = (string.IsNullOrEmpty(txt_OldGoldReceipt.Text.Trim()) ? "0.00" : txt_OldGoldReceipt.Text);
            txt_ReturnAmount.Text = (string.IsNullOrEmpty(txt_ReturnAmount.Text.Trim()) ? "0.00" : txt_ReturnAmount.Text);
            txt_TotalPaidAmount.Text = (string.IsNullOrEmpty(txt_TotalPaidAmount.Text.Trim()) ? "0.00" : txt_TotalPaidAmount.Text);
            txtAdvancePaid.Text = (string.IsNullOrEmpty(txtAdvancePaid.Text.Trim()) ? "0.00" : txtAdvancePaid.Text);
            txtPaidAmount.Text = (string.IsNullOrEmpty(txtPaidAmount.Text.Trim()) ? "0.00" : txtPaidAmount.Text);

            Schemepaid = Convert.ToSingle(txtPaidAmount.Text);
            OldGold = Convert.ToSingle(txt_OldGoldReceipt.Text);
            paidAmt = Convert.ToSingle(txt_ReturnAmount.Text);
            retAmt = Convert.ToSingle(txt_TotalPaidAmount.Text);
            AdvPaid = Convert.ToSingle(txtAdvancePaid.Text);

            CashPaid = OldGold + paidAmt + retAmt + AdvPaid + Schemepaid;
            txt_TotalCashPaid.Text = CashPaid.ToString();
        }
        public void DiscountAmountCalculation(object sender, EventArgs e)
        {
            float AmtAfterDis = 0, prodVal = 0, disperc = 0, disAmt = 0, val = 0;
            txtAmountAfterDis.Text = (string.IsNullOrEmpty(txtAmountAfterDis.Text.Trim()) ? "0.00" : txtAmountAfterDis.Text);
            txtProductVal.Text = (string.IsNullOrEmpty(txtProductVal.Text.Trim()) ? "0.00" : txtProductVal.Text);


            AmtAfterDis = Convert.ToSingle(txtAmountAfterDis.Text);
            prodVal = Convert.ToSingle(txtProductVal.Text);
            disAmt = prodVal - AmtAfterDis;
            txtDiscAmnt.Text = disAmt.ToString("F2");
            val = prodVal / 100;
             disperc = disAmt / val;
            txtDiscPerc.Text = disperc.ToString("F2");

        }
        public void amount_AfterDiscount(object sender, EventArgs e)
        {

            txtAmountAfterDis.Text = Convert.ToSingle(txtProductVal.Text == "" ? "0" : txtProductVal.Text).ToString("F2");

        }
        public void NetAmount_Calculation(object Sender, EventArgs e)
        {
            if (txtGrandTotal.Text != "")
            {
                txtNetTotal.Text = Math.Round(Convert.ToSingle(txtGrandTotal.Text), 2).ToString("F2");
            }
        }
        public void FinalAmount(object sender, EventArgs e)
        {
            txtFinalAmount.Text = Convert.ToSingle(txtNetTotal.Text == "" ? "0" : txtNetTotal.Text).ToString("F2");
        }

        public void GrandTotalCalculation(object Sender, EventArgs e)
        {
            txtGrandTotal.Text = (Convert.ToSingle((txtTaxAmnt.Text == "" ? "0" : txtTaxAmnt.Text)) + Convert.ToSingle((txtAmountAfterDis.Text == "" ? "0" : txtAmountAfterDis.Text))
                                  + Convert.ToSingle((txtOtherCharges.Text == "" ? "0" : txtOtherCharges.Text))).ToString("F2");
        }

        public void RoundOff(object sender, EventArgs e)
        {
            float NetTotal = 0, finalAmt = 0, RoundOff = 0;
            txtNetTotal.Text = (string.IsNullOrEmpty(txtNetTotal.Text.Trim()) ? "0.00" : txtNetTotal.Text);
            txtFinalAmount.Text = (string.IsNullOrEmpty(txtFinalAmount.Text.Trim()) ? "0.00" : txtFinalAmount.Text);

            NetTotal = Convert.ToSingle(txtNetTotal.Text);
            finalAmt = Convert.ToSingle(txtFinalAmount.Text);
            RoundOff = finalAmt - NetTotal;
            txtRoundOff.Text = Math.Round(Convert.ToSingle(RoundOff), 2).ToString("F2"); //RoundOff.ToString();
        }
        public void BalanceAmount(object Sender, EventArgs e)
        {
            float FinalAmt = 0, TotalCash = 0, Balance = 0;
            txtFinalAmount.Text = (string.IsNullOrEmpty(txtFinalAmount.Text.Trim()) ? "0.00" : txtFinalAmount.Text);
            txt_TotalCashPaid.Text = (string.IsNullOrEmpty(txt_TotalCashPaid.Text.Trim()) ? "0.00" : txt_TotalCashPaid.Text);

            FinalAmt = Convert.ToSingle(txtFinalAmount.Text);
            TotalCash = Convert.ToSingle(txt_TotalCashPaid.Text);
            Balance = FinalAmt - TotalCash;
            txtBalanceAmont.Text = Balance.ToString();
        }

        private void txtAmountAfterDis_TextChanged(object sender, EventArgs e)
        {
            UpdateTax();
        }

        private void dgv_Return_SummaryCalculated(object source, EventArgs e)
        {
            txt_ReturnAmount.Text = dgv_Return.SummaryRow.SummaryCells["TotalAmount"].Text;
        }


        ///////////////////////////////////////GRID TYPING SALES DETAILS ////////////////////////////////////////////////////////////////////////////


        private void dgv_SalesDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "DiaWt" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "Stone Wt")
            {
                CalculateWt();
            }
            if (dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "VA" ||
               // dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "VA" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "DiaCash" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "StoneCash" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "MetalCash")
            {
                TotalAmount();
            }
            if (dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "Touch" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "PurityName")
            {
               // CalculatedWstPerc();
            }
            if (dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "Wastage" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "MetalRate")
            {
               // WastageCash();
            }
            if (dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "DiaCash" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "StoneCash" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "MetalCash"||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName =="VAPerc")
            {
                VAamount(); 
            }

            //if (dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "MC" ||
            //    dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "WastageCash")
            //{
            //    VA();
            //}
            if (dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "ItemName")// ||
            {
                MetalRate();
            }
            if (dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "NetWt" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "MetalRate" ||
                dgv_SalesDetails.Columns[e.ColumnIndex].DataPropertyName == "Touch")
            {
                MetalCash();
            }
            if (dgv_SalesDetails.CurrentRow.Cells["ProdCodeId"].Value.ToString() == "" && (dgv_SalesDetails.CurrentRow.Cells["ItemId"].Value.ToString() != ""))
            {
                if (dgv_SalesDetails.CurrentRow == null)
                    return;
                if (dgv_SalesDetails.CurrentRow.Cells["Qty"].Value.ToString() == "")
                    dgv_SalesDetails.CurrentRow.Cells["Qty"].Value = "0";
                if (dgv_SalesDetails.CurrentRow.Cells["DiaNo"].Value.ToString() == "")
                    dgv_SalesDetails.CurrentRow.Cells["DiaNo"].Value = "0";
                if (dgv_SalesDetails.CurrentRow.Cells["DiaWt"].Value.ToString() == "")
                    dgv_SalesDetails.CurrentRow.Cells["DiaWt"].Value = "0";
                if (dgv_SalesDetails.CurrentRow.Cells["DiaCash"].Value.ToString() == "")
                    dgv_SalesDetails.CurrentRow.Cells["DiaCash"].Value = "0";
                if (dgv_SalesDetails.CurrentRow.Cells["MC"].Value.ToString() == "")
                    //dgv_SalesDetails.CurrentRow.Cells["MC"].Value = "0";
                    if (dgv_SalesDetails.CurrentRow.Cells["Wastage"].Value.ToString() == "")
                        dgv_SalesDetails.CurrentRow.Cells["Wastage"].Value = "0";
                if (dgv_SalesDetails.CurrentRow.Cells["NetWt"].Value.ToString() == "")
                    dgv_SalesDetails.CurrentRow.Cells["NetWt"].Value = "0";
                if (Rb_Diamond.Checked == true)
                // if (DBConn.GetData(new SqlCommand("Select ItemId FROM ITM.VGoldOrnaments WHERE ItemId=" + dgv_SalesDetails.CurrentRow.Cells["ItemId"].Value)).Tables[0].Rows.Count != 0)
                {
                    if (Cmb_ItemName.SelectedValue != null && Cmb_ItemName.Text.Trim().Length > 0 &&
                        Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["Qty"].Value) != 0 &&
                        Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["NetWt"].Value) != 0 &&
                        Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["Gwt"].Value) != 0 &&
                        Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["DiaNo"].Value) != 0 &&
                        Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["DiaWt"].Value) != 0 &&
                        Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["DiaCash"].Value) != 0 &&
                        Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["TotalAmount"].Value) != 0)
                    {
                        if (dgv_SalesDetails.Rows.Count > 0)
                        {
                            if (dgv_SalesDetails.Rows[dgv_SalesDetails.Rows.Count - 1].Cells["ItemID"].Value.ToString() != "")
                            {
                                ((System.Data.DataTable)dgv_SalesDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_SalesDetails.DataSource).NewRow());

                            
                                dgv_SalesDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
                                dgv_SalesDetails.CurrentCell = dgv_SalesDetails.Rows[SaleCurrentRow + 1].Cells["ItemName"];
                                dgv_SalesDetails.Rows[e.RowIndex].Cells["IsReceipt"].Value = 0;
                                dgv_SalesDetails.Rows[e.RowIndex].Cells["Type"].Value = "S";  
                                
                            }
                        }
                    }
                }
                else
                {
                    if (Cmb_ItemName.SelectedValue != null && Cmb_ItemName.Text.Trim().Length > 0 &&
                             Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["Qty"].Value) != 0 &&
                             Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["Touch"].Value) != 0 &&
                              Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["NetWt"].Value) != 0 &&
                              Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["Gwt"].Value) != 0
                           && Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["TotalAmount"].Value) != 0)
                    {
                        if (dgv_SalesDetails.Rows.Count > 0)
                        {
                            if (dgv_SalesDetails.Rows[dgv_SalesDetails.Rows.Count - 1].Cells["ItemID"].Value.ToString() != "")
                            {
                                ((System.Data.DataTable)dgv_SalesDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_SalesDetails.DataSource).NewRow());

                                dgv_SalesDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
                                dgv_SalesDetails.CurrentCell = dgv_SalesDetails.Rows[SaleCurrentRow + 1].Cells["ItemName"];
                                dgv_SalesDetails.Rows[e.RowIndex].Cells["IsReceipt"].Value = 0;
                                dgv_SalesDetails.Rows[e.RowIndex].Cells["Type"].Value = "S"; 
                                    
                            }
                        }
                    }
                }

            }
        }




        public void CalculateWt()
        {
            float NetWt = 0, StoneWt = 0, DiaWt = 0, DiaStWt = 0, TotalWt = 0, Gwt = 0;
            if (dgv_SalesDetails.CurrentRow == null)
                return;
            if (dgv_SalesDetails.CurrentRow.Cells["Gwt"].Value.ToString() != "")
            {
                Gwt = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["Gwt"].Value.ToString());
            }

            if (dgv_SalesDetails.CurrentRow.Cells["StoneWt"].Value.ToString() != "")
            {
                StoneWt = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["StoneWt"].Value.ToString());
            }

            if (dgv_SalesDetails.CurrentRow.Cells["DiaWt"].Value.ToString() != "")
            {
                DiaWt = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["DiaWt"].Value.ToString());
            }

            DiaStWt = Convert.ToSingle((StoneWt + DiaWt) * .2);
            TotalWt = Gwt - DiaStWt;
            dgv_SalesDetails.CurrentRow.Cells["NetWt"].Value = TotalWt.ToString();
        }

        private void TotalAmount()
        {

            float Diacash = 0, Stcash = 0, GoldCash = 0, TotalCash = 0, SetCash = 0,VA=0;//MC = 0, WastCash = 0

            if (dgv_SalesDetails.CurrentRow == null)
                return;
            if (dgv_SalesDetails.CurrentRow.Cells["MetalCash"].Value.ToString() != "")
            {
                GoldCash = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["MetalCash"].Value.ToString());

            }

            if (dgv_SalesDetails.CurrentRow.Cells["StoneCash"].Value.ToString() != "")
            {
                Stcash = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["StoneCash"].Value.ToString());

            }
            if (dgv_SalesDetails.CurrentRow.Cells["DiaCash"].Value.ToString() != "")
            {
                Diacash = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["DiaCash"].Value.ToString());

            }
            if (dgv_SalesDetails.CurrentRow.Cells["VA"].Value.ToString() != "")
            {
                VA = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["VA"].Value.ToString());
  
            }
            //if (dgv_SalesDetails.CurrentRow.Cells["MC"].Value.ToString() != "")
            //{
            //    MC = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["MC"].Value.ToString());

            //}
            //if (dgv_SalesDetails.CurrentRow.Cells["WastageCash"].Value.ToString() != "")
            //{
            //    WastCash = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["WastageCash"].Value.ToString());

            //}
            TotalCash = GoldCash + Stcash + Diacash + VA;

            dgv_SalesDetails.CurrentRow.Cells["TotalAmount"].Value = TotalCash.ToString();

        }

        //VA Amount
        private void VAamount()
        {
            float VAperc = 0, MetalCash = 0, StoneCash = 0, DiaCash = 0, totCash = 0, VA;

            if (dgv_SalesDetails.CurrentRow.Cells["DiaCash"].Value.ToString() != "")
            {
                DiaCash = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["DiaCash"].Value.ToString());

            }
            if (dgv_SalesDetails.CurrentRow.Cells["StoneCash"].Value.ToString() != "")
            {
                StoneCash = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["StoneCash"].Value.ToString());

            }
            if (dgv_SalesDetails.CurrentRow.Cells["MetalCash"].Value.ToString() != "")
            {
                MetalCash = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["MetalCash"].Value.ToString());

            }
            if (dgv_SalesDetails.CurrentRow.Cells["VAPerc"].Value.ToString() != "")
            {
                VAperc = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["VAPerc"].Value.ToString());
            }
            totCash = DiaCash + MetalCash + StoneCash;
            VA = totCash * VAperc / 100;
            dgv_SalesDetails.CurrentRow.Cells["VA"].Value = VA.ToString();

        }


        

        private void WastageCash()
        {
            float Wst = 0, MetalRate = 0, WastageCash = 0;
            if (dgv_SalesDetails.CurrentRow == null)
                return;
            if (dgv_SalesDetails.CurrentRow.Cells["Wastage"].Value.ToString() != "")
            {
                Wst = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["Wastage"].Value.ToString());

            }

            if (dgv_SalesDetails.CurrentRow.Cells["MetalRate"].Value.ToString() != "")
            {
                MetalRate = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["MetalRate"].Value.ToString());

            }
            WastageCash = Wst * MetalRate;
            dgv_SalesDetails.CurrentRow.Cells["WastageCash"].Value = WastageCash.ToString();
        }

        private void VA()
        {
            float MC = 0, VA = 0, WastageCash = 0;
            if (dgv_SalesDetails.CurrentRow == null)
                return;
            if (dgv_SalesDetails.CurrentRow.Cells["MC"].Value.ToString() != "")
            {
                MC = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["MC"].Value.ToString());

            }

            if (dgv_SalesDetails.CurrentRow.Cells["WastageCash"].Value.ToString() != "")
            {
                WastageCash = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["WastageCash"].Value.ToString());

            }
            VA = WastageCash + MC;
            dgv_SalesDetails.CurrentRow.Cells["VA"].Value = VA.ToString();
        }
        private void MetalRate()
        {
            float ItemCategory = 0, Metalrate = 0;
            foreach (DataGridViewRow r in dgv_SalesDetails.Rows)
            {
                if (r.Cells["ItemId"].Value.ToString() != "")
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                    ("SELECT ItemCategoryId FROM ITM.VItemMaster WHERE ItemId =" + r.Cells["ItemId"].Value + "")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {

                            ItemCategory = Convert.ToSingle(dt.Rows[0]["ItemCategoryId"].ToString());
                        }
                        if (ItemCategory == 11)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["BoardRate"]));

                        }
                        else if (ItemCategory == 12)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["SilverRate"]));
                        }
                        else if (ItemCategory == 13)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 PlatinumRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["PlatinumRate"]));
                        }
                        else if (ItemCategory == 15)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["SilverRate"]));
                        }
                        else if (ItemCategory == 16)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["BoardRate"]));
                        }
                        dgv_SalesDetails.CurrentRow.Cells["MetalRate"].Value = Metalrate.ToString();
                    }

                }
            }
        }
        private void MetalCash()
        {
            float MetalRate = 0, NetWt = 0, VA = 0, Touch = 0, MetalCash = 0, Wt916 = 0, ItemCategory = 0;
            if (dgv_SalesDetails.CurrentRow == null)
                return;
            foreach (DataGridViewRow r in dgv_SalesDetails.Rows)
            {
                if (r.Cells["ItemId"].Value.ToString() != "")
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                    ("SELECT ItemCategoryId FROM ITM.VItemMaster WHERE ItemId =" + r.Cells["ItemId"].Value + "")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {

                            ItemCategory = Convert.ToSingle(dt.Rows[0]["ItemCategoryId"].ToString());
                        }
                    }
                }
            }
            if (dgv_SalesDetails.CurrentRow.Cells["MetalRate"].Value.ToString() != "")
            {
                MetalRate = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["MetalRate"].Value.ToString());

            }
            if (dgv_SalesDetails.CurrentRow.Cells["NetWt"].Value.ToString() != "")
            {
                NetWt = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["NetWt"].Value.ToString());
            }

            if (dgv_SalesDetails.CurrentRow.Cells["Touch"].Value.ToString() != "")
            {
                Touch = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["Touch"].Value.ToString());
            }
            if (ItemCategory == 11 || ItemCategory == 16)
            {
                Wt916 = NetWt * Touch / Convert.ToSingle(91.6);
                MetalCash = Wt916 * MetalRate;
            }
            else
            {
                MetalCash = NetWt * MetalRate;
            }
            dgv_SalesDetails.CurrentRow.Cells["MetalCash"].Value = MetalCash.ToString();
        }
        private void CalculatedWstPerc()
        {
            if (dgv_SalesDetails.CurrentRow == null)
                return;
            float purity = 0, touch = 0, wstperc = 0, wst = 0, gwt = 0;

            if (dgv_SalesDetails.CurrentRow.Cells["PurityId"].Value.ToString() != "")
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select PurityValue FROM ITM.PurityMaster WHERE PurityId ="
                    + dgv_SalesDetails.CurrentRow.Cells["PurityId"].Value + "")).Tables[0])//dgv_SalesDetails.CurrentRow.Cells["PurityId"].Value
                {
                    if (dt.Rows.Count > 0)
                    {
                        purity = Convert.ToSingle(dt.Rows[0]["PurityValue"].ToString());
                    }
                }
            }
            else
            {
                purity = 0;
            }
            if (dgv_SalesDetails.CurrentRow.Cells["Touch"].Value.ToString() != "")
            {
                touch = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["Touch"].Value.ToString());
            }
            if (purity > touch)
            {
                wstperc = purity - touch;

            }
            else
            {
                wstperc = touch - purity;
            }
            if (dgv_SalesDetails.CurrentRow.Cells["Gwt"].Value.ToString() != "")
            {
                gwt = Convert.ToSingle(dgv_SalesDetails.CurrentRow.Cells["Gwt"].Value.ToString());
            }
            wst = gwt * wstperc / 100;

            dgv_SalesDetails.CurrentRow.Cells["Wastage"].Value = Math.Round(Convert.ToSingle(wst), 2).ToString("F2");



        }

        ///////////////////////////////////////GRID TYPING SALES RETURN ////////////////////////////////////////////////////////////////////////////

        private void dgv_Return_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
               dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "DiaWt" ||
               dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "StoneWt")
            {
                CalculateWtReturn();
            }
            if ( dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "VA" ||
                dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "DiaCash" ||
                dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "StoneCash" ||
                dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "MetalCash")
            {
                TotalAmountReturn();
            }

            if (dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "Touch" ||
                dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
                dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "PurityName")
            {
              //  CalculatedWstPercReturn();
            }
            if (dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "Wastage" ||
                dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "MetalRate")
            {
              //  WastageCashReturn();
            }
            if (dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "MC" ||
                dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "WastageCash")
            {
              //  VAReturn();
            }
            if (dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "ItemName")// ||
            {
                MetalRateReturn();
            }
            if (dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "NetWt" ||
                dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "MetalRate" ||
                dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "Touch")
            {
                MetalCashReturn();
            }
            if (dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "DiaCash" ||
               dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "StoneCash" ||
               dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "MetalCash" ||
               dgv_Return.Columns[e.ColumnIndex].DataPropertyName == "VAPerc")
            {
                VAamountReturn();
            }
            if (dgv_Return.CurrentRow.Cells["ProdCodeId"].Value.ToString() == null)
            {
                if (dgv_Return.CurrentRow == null)
                    return;
                if (dgv_Return.CurrentRow.Cells["Qty"].Value.ToString() == "")
                    dgv_Return.CurrentRow.Cells["Qty"].Value = "0";
                if (dgv_Return.CurrentRow.Cells["DiaNo"].Value.ToString() == "")
                    dgv_Return.CurrentRow.Cells["DiaNo"].Value = "0";
                if (dgv_Return.CurrentRow.Cells["DiaWt"].Value.ToString() == "")
                    dgv_Return.CurrentRow.Cells["DiaWt"].Value = "0";
                if (dgv_Return.CurrentRow.Cells["DiaCash"].Value.ToString() == "")
                    dgv_Return.CurrentRow.Cells["DiaCash"].Value = "0";
                if (dgv_Return.CurrentRow.Cells["MC"].Value.ToString() == "")
                    dgv_Return.CurrentRow.Cells["MC"].Value = "0";
                if (dgv_Return.CurrentRow.Cells["Wastage"].Value.ToString() == "")
                    dgv_Return.CurrentRow.Cells["Wastage"].Value = "0";
                if (dgv_Return.CurrentRow.Cells["NetWt"].Value.ToString() == "")
                    dgv_Return.CurrentRow.Cells["NetWt"].Value = "0";


                if (Cmb_ItemNameReturn.SelectedValue != null && Cmb_ItemNameReturn.Text.Trim().Length > 0
                        && Convert.ToSingle(dgv_Return.CurrentRow.Cells["Qty"].Value) != 0 &&
                           Convert.ToSingle(dgv_Return.CurrentRow.Cells["NetWt"].Value) != 0 &&
                           Convert.ToSingle(dgv_Return.CurrentRow.Cells["Touch"].Value) != 0 &&
                           Convert.ToSingle(dgv_Return.CurrentRow.Cells["Gwt"].Value) != 0
                        && Convert.ToSingle(dgv_Return.CurrentRow.Cells["TotalAmount"].Value) != 0)
                {
                    if (dgv_Return.Rows.Count > 0)
                    {
                        if (dgv_Return.Rows[dgv_Return.Rows.Count - 1].Cells["ItemID"].Value.ToString() != "")
                        {
                            ((System.Data.DataTable)dgv_Return.DataSource).Rows.Add(((System.Data.DataTable)dgv_Return.DataSource).NewRow());

                            dgv_Return.SelectionMode = DataGridViewSelectionMode.CellSelect;
                            dgv_Return.CurrentCell = dgv_Return.Rows[SaleCurrentretRow + 1].Cells["ItemName"];
                            dgv_Return.Rows[e.RowIndex].Cells["IsReceipt"].Value = 1;
                            dgv_Return.Rows[e.RowIndex].Cells["Type"].Value = "R";
                        }
                    }
                }
            }
        }
        private void VAamountReturn()
        {
            float VAperc = 0, MetalCash = 0, StoneCash = 0, DiaCash = 0, totCash = 0, VA;

            if (dgv_Return.CurrentRow.Cells["DiaCash"].Value.ToString() != "")
            {
                DiaCash = Convert.ToSingle(dgv_Return.CurrentRow.Cells["DiaCash"].Value.ToString());

            }
            if (dgv_Return.CurrentRow.Cells["StoneCash"].Value.ToString() != "")
            {
                StoneCash = Convert.ToSingle(dgv_Return.CurrentRow.Cells["StoneCash"].Value.ToString());

            }
            if (dgv_Return.CurrentRow.Cells["MetalCash"].Value.ToString() != "")
            {
                MetalCash = Convert.ToSingle(dgv_Return.CurrentRow.Cells["MetalCash"].Value.ToString());

            }
            if (dgv_Return.CurrentRow.Cells["VAPerc"].Value.ToString() != "")
            {
                VAperc = Convert.ToSingle(dgv_Return.CurrentRow.Cells["VAPerc"].Value.ToString());
            }
            totCash = DiaCash + MetalCash + StoneCash;
            VA = totCash * VAperc / 100;
            dgv_Return.CurrentRow.Cells["VA"].Value = VA.ToString();

        }

        public void CalculateWtReturn()
        {
            float NetWt = 0, StoneWt = 0, DiaWt = 0, DiaStWt = 0, TotalWt = 0, Gwt = 0;
            if (dgv_Return.CurrentRow == null)
                return;
            if (dgv_Return.CurrentRow.Cells["Gwt"].Value.ToString() != "")
            {
                Gwt = Convert.ToSingle(dgv_Return.CurrentRow.Cells["Gwt"].Value.ToString());
            }

            if (dgv_Return.CurrentRow.Cells["StoneWt"].Value.ToString() != "")
            {
                StoneWt = Convert.ToSingle(dgv_Return.CurrentRow.Cells["StoneWt"].Value.ToString());
            }

            if (dgv_Return.CurrentRow.Cells["DiaWt"].Value.ToString() != "")
            {
                DiaWt = Convert.ToSingle(dgv_Return.CurrentRow.Cells["DiaWt"].Value.ToString());
            }

            DiaStWt = Convert.ToSingle((StoneWt + DiaWt) * .2);
            TotalWt = Gwt - DiaStWt;
            dgv_Return.CurrentRow.Cells["NetWt"].Value = TotalWt.ToString();
        }
       

        private void TotalAmountReturn()
        {

            float Diacash = 0, Stcash = 0, GoldCash = 0, TotalCash = 0, MC = 0, WastCash = 0, SetCash = 0,VA=0;

            if (dgv_Return.CurrentRow == null)
                return;
            if (dgv_Return.CurrentRow.Cells["MetalCash"].Value.ToString() != "")
            {
                GoldCash = Convert.ToSingle(dgv_Return.CurrentRow.Cells["MetalCash"].Value.ToString());

            }

            if (dgv_Return.CurrentRow.Cells["StoneCash"].Value.ToString() != "")
            {
                Stcash = Convert.ToSingle(dgv_Return.CurrentRow.Cells["StoneCash"].Value.ToString());

            }
            if (dgv_Return.CurrentRow.Cells["DiaCash"].Value.ToString() != "")
            {
                Diacash = Convert.ToSingle(dgv_Return.CurrentRow.Cells["DiaCash"].Value.ToString());

            }
            //if (dgv_Return.CurrentRow.Cells["MC"].Value.ToString() != "")
            //{
            //    MC = Convert.ToSingle(dgv_Return.CurrentRow.Cells["MC"].Value.ToString());

            //}
            if (dgv_Return.CurrentRow.Cells["VA"].Value.ToString() != "")
            {
                VA = Convert.ToSingle(dgv_Return.CurrentRow.Cells["VA"].Value.ToString());

            }
            //if (dgv_Return.CurrentRow.Cells["WastageCash"].Value.ToString() != "")
            //{
            //    WastCash = Convert.ToSingle(dgv_Return.CurrentRow.Cells["WastageCash"].Value.ToString());

            //}
            TotalCash = GoldCash + Stcash + Diacash + VA;

            dgv_Return.CurrentRow.Cells["TotalAmount"].Value = TotalCash.ToString();

        }

        private void WastageCashReturn()
        {
            float Wst = 0, MetalRate = 0, WastageCash = 0;
            if (dgv_Return.CurrentRow == null)
                return;
            if (dgv_Return.CurrentRow.Cells["Wastage"].Value.ToString() != "")
            {
                Wst = Convert.ToSingle(dgv_Return.CurrentRow.Cells["Wastage"].Value.ToString());

            }

            if (dgv_Return.CurrentRow.Cells["MetalRate"].Value.ToString() != "")
            {
                MetalRate = Convert.ToSingle(dgv_Return.CurrentRow.Cells["MetalRate"].Value.ToString());

            }
            WastageCash = Wst * MetalRate;
            dgv_Return.CurrentRow.Cells["WastageCash"].Value = WastageCash.ToString();
        }

        private void VAReturn()
        {
            float MC = 0, VA = 0, WastageCash = 0;
            if (dgv_Return.CurrentRow == null)
                return;
            if (dgv_Return.CurrentRow.Cells["MC"].Value.ToString() != "")
            {
                MC = Convert.ToSingle(dgv_Return.CurrentRow.Cells["MC"].Value.ToString());

            }

            if (dgv_Return.CurrentRow.Cells["WastageCash"].Value.ToString() != "")
            {
                WastageCash = Convert.ToSingle(dgv_Return.CurrentRow.Cells["WastageCash"].Value.ToString());

            }
            VA = WastageCash + MC;
            dgv_Return.CurrentRow.Cells["VA"].Value = VA.ToString();
        }
        private void MetalRateReturn()
        {
            float ItemCategory = 0, Metalrate = 0;
            foreach (DataGridViewRow r in dgv_Return.Rows)
            {
                if (r.Cells["ItemId"].Value.ToString() != "")
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                    ("SELECT ItemCategoryId FROM ITM.VItemMaster WHERE ItemId =" + r.Cells["ItemId"].Value + "")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {

                            ItemCategory = Convert.ToSingle(dt.Rows[0]["ItemCategoryId"].ToString());
                        }
                        if (ItemCategory == 11)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["BoardRate"]));

                        }
                        else if (ItemCategory == 12)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["SilverRate"]));
                        }
                        else if (ItemCategory == 13)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 PlatinumRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["PlatinumRate"]));
                        }
                        else if (ItemCategory == 15)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["SilverRate"]));
                        }
                        else if (ItemCategory == 16)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["BoardRate"]));
                        }
                        dgv_Return.CurrentRow.Cells["MetalRate"].Value = Metalrate.ToString();
                    }

                }
            }
        }
        private void MetalCashReturn()
        {
            float MetalRate = 0, NetWt = 0, VA = 0, Touch = 0, MetalCash = 0, Wt916 = 0, ItemCategory = 0;
            if (dgv_Return.CurrentRow == null)
                return;
            foreach (DataGridViewRow r in dgv_Return.Rows)
            {
                if (r.Cells["ItemId"].Value.ToString() != "")
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                    ("SELECT ItemCategoryId FROM ITM.VItemMaster WHERE ItemId =" + r.Cells["ItemId"].Value + "")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {

                            ItemCategory = Convert.ToSingle(dt.Rows[0]["ItemCategoryId"].ToString());
                        }
                    }
                }
            }
            if (dgv_Return.CurrentRow.Cells["MetalRate"].Value.ToString() != "")
            {
                MetalRate = Convert.ToSingle(dgv_Return.CurrentRow.Cells["MetalRate"].Value.ToString());

            }
            if (dgv_Return.CurrentRow.Cells["NetWt"].Value.ToString() != "")
            {
                NetWt = Convert.ToSingle(dgv_Return.CurrentRow.Cells["NetWt"].Value.ToString());
            }

            if (dgv_Return.CurrentRow.Cells["Touch"].Value.ToString() != "")
            {
                Touch = Convert.ToSingle(dgv_Return.CurrentRow.Cells["Touch"].Value.ToString());
            }
            if (ItemCategory == 11 || ItemCategory == 16)
            {
                Wt916 = NetWt * Touch / Convert.ToSingle(91.6);
                MetalCash = Wt916 * MetalRate;
            }
            else
            {
                MetalCash = NetWt * MetalRate;
            }
            dgv_Return.CurrentRow.Cells["MetalCash"].Value = MetalCash.ToString();
        }
        private void CalculatedWstPercReturn()
        {
            if (dgv_Return.CurrentRow == null)
                return;
            float purity = 0, touch = 0, wstperc = 0, wst = 0, gwt = 0;

            if (dgv_Return.CurrentRow.Cells["PurityName"].Value.ToString() != "")
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select PurityValue FROM ITM.PurityMaster WHERE PurityId ="
                    + dgv_Return.CurrentRow.Cells["PurityId"].Value + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        purity = Convert.ToSingle(dt.Rows[0]["PurityValue"].ToString());
                    }
                }
            }
            else
            {
                purity = 0;
            }
            if (dgv_Return.CurrentRow.Cells["Touch"].Value.ToString() != "")
            {
                touch = Convert.ToSingle(dgv_Return.CurrentRow.Cells["Touch"].Value.ToString());
            }
            if (purity > touch)
            {
                wstperc = purity - touch;

            }
            else
            {
                wstperc = touch - purity;
            }
            if (dgv_Return.CurrentRow.Cells["Gwt"].Value.ToString() != "")
            {
                gwt = Convert.ToSingle(dgv_Return.CurrentRow.Cells["Gwt"].Value.ToString());
            }
            wst = gwt * wstperc / 100;

            dgv_Return.CurrentRow.Cells["Wastage"].Value = Math.Round(Convert.ToSingle(wst), 2).ToString("F2");



        }


        //private void LnkOldGoldReceipt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    JMS.Forms.SALE.OldGoldReceiptNew frm = new OldGoldReceiptNew();
        //    frm.MdiParent = this.ParentForm;
        //    frm.s = this;
        //    InitializeTables();
        //    GenerateID(this.TableName);
        //    ((frmMain)this.ParentForm).ShowChild(frm);
        //    frm.Focus();
        //    frm.Tag = "Old Gold Receipt";
        //    frm.fillOldGoldReceipt();
        //}
        //public void fillOldGoldReceipt()
        //{
        //    if (frmold.txtSalesID.Text != "")
        //    {
        //        using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select EntryId FROM SALE.OldGoldReciptMaster WHERE SalesId=" + frmold.txtSalesID.Text)).Tables[0])
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                txtEntryId.Text = dt.Rows[0][0].ToString();
        //                Dictionary<string, object> d = new Dictionary<string, object>();
        //                d.Add("EntryId", txtEntryId.Text);
        //                d.Add("Company_id", txtcompId.Text);
        //                this.FillData(d);
        //            }
        //            else
        //            {
        //                Init();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Init();
        //    }
        //}

        private void chkCash_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCash.Checked == true)
            {
                txtCash.ReadOnly = false;
            }
            else
            {
                txtCash.Clear();
                txtCash.ReadOnly = true;
            }
        }

        private void Chk_Credit_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Credit.Checked == true)
            {
                txtCredit.ReadOnly = false;
                GroupCredit.Enabled = true;
                cmbCreditCardId.AcceptBlankValue = false;
                txtCreditCardNo.AcceptBlankValue = false;
                txtCreditCardUser.AcceptBlankValue = false;
            }
            else
            {
                GroupCredit.Enabled = false;
                txtCredit.Clear();
                txtCredit.ReadOnly = true;
                cmbCreditCardId.Text = "";
                txtCreditCardNo.Clear();
                txtCreditCardUser.Clear();
                cmbCreditCardId.AcceptBlankValue = true;
                txtCreditCardNo.AcceptBlankValue = true;
                txtCreditCardUser.AcceptBlankValue = true;
            }
        }

        private void Chk_Cheque_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Cheque.Checked == true)
            {
                txtCheque.ReadOnly = false;

                groupCheque.Enabled = true;
                cmb_BankId.AcceptBlankValue = false;
                txtChequeNo.AcceptBlankValue = false;
                dtpchequeDate.AcceptBlankValue = false;
            }
            else
            {
                groupCheque.Enabled = false;
                cmb_BankId.Text = "";
                txtCheque.Clear();
                txtCheque.ReadOnly = true;
                txtChequeNo.Clear();

                cmb_BankId.AcceptBlankValue = true;
                txtChequeNo.AcceptBlankValue = true;
                dtpchequeDate.AcceptBlankValue = true;
            }
        }

        private void Cmb_BookingNo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Cmb_BookingNo.SelectedValue != null)
            {
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
               ("Select TotalAmount,GoldRate,GoldWt FROM SALE.GoldBooking WHERE BookId=" + Cmb_BookingNo.SelectedValue)).Tables[0])
                {

                    txtBookedAmount.Text = (dt.Rows[0]["TotalAmount"] == null ? "" : dt.Rows[0]["TotalAmount"].ToString());
                    txtBookedGoldRate.Text = (dt.Rows[0]["GoldRate"] == null ? "" : dt.Rows[0]["GoldRate"].ToString());
                    txtBookedWt.Text = (dt.Rows[0]["GoldWt"] == null ? "" : dt.Rows[0]["GoldWt"].ToString());
                    txtAdvancePaid.Text = (dt.Rows[0]["TotalAmount"] == null ? "" : dt.Rows[0]["TotalAmount"].ToString());

                }
                //if (Convert.ToSingle(txtMRP.Text) == 0)
                // {
                AdvanceDetails1(sender, e);
                // }

                // AdvanceDetails1(sender, e);
            }
            if (Cmb_BookingNo.SelectedValue == null)
            {
                txtAdvancePaid.Text = "0.00";
                txtBookedAmount.Text = "0.00";
                txtBookedGoldRate.Text = "0.00";
                txtBookedWt.Text = "0.00";
                //   if (Convert.ToSingle(txtMRP.Text) == 0)
                //  {
                AdvanceDetails1(sender, e);
                //   }
                //AdvanceDetails1(sender, e);
            }

        }
        public void AdvanceDetails1(object Sender, EventArgs e)
        {
            float pureGoldrate = 0, BookedGoldrate = 0, TotalGoldCash = 0, totGoldWt = 0, BookedWt = 0, wst = 0, wstCash = 0,
            Goldrate916 = 0, BookedGoldCash = 0, totalGoldCashBa = 0, remWt = 0, RemCash = 0, goldwst = 0, goldCash = 0, MetalCash = 0, platinumRate = 0, SilverRate = 0,
            Metal1Cash = 0, Metal2Cash = 0, TotalAmount = 0, goldWt = 0;
            txtSilverRate.Text = (string.IsNullOrEmpty(txtSilverRate.Text.Trim()) ? "0.00" : txtSilverRate.Text);
            txtPlatinumRate.Text = (string.IsNullOrEmpty(txtPlatinumRate.Text.Trim()) ? "0.00" : txtPlatinumRate.Text);
            txtGoldlRate.Text = (string.IsNullOrEmpty(txtGoldlRate.Text.Trim()) ? "0.00" : txtGoldlRate.Text);
            txtBookedGoldRate.Text = (string.IsNullOrEmpty(txtBookedGoldRate.Text.Trim()) ? "0.00" : txtBookedGoldRate.Text);
            txt_TotalGoldWt.Text = (string.IsNullOrEmpty(txt_TotalGoldWt.Text.Trim()) ? "0.00" : txt_TotalGoldWt.Text);
            txtBookedWt.Text = (string.IsNullOrEmpty(txtBookedWt.Text.Trim()) ? "0.00" : txtBookedWt.Text);
            String MetalId1, MetalId2;
            float ItemCatId = 0, NetWt = 0, Touch = 0, NetWt1 = 0;
            // if (Convert.ToSingle(txtMRP.Text) == 0)
            // {
            if (Cmb_BookingNo.SelectedValue != null)
            {

                if (txtBookedGoldRate.Text != "" && txtGoldlRate.Text != "" && txt_TotalGoldWt.Text != "" && txtBookedWt.Text != "")
                {
                    SilverRate = Convert.ToSingle(txtSilverRate.Text);
                    platinumRate = Convert.ToSingle(txtPlatinumRate.Text);
                    BookedWt = Convert.ToSingle(txtBookedWt.Text);
                    foreach (DataGridViewRow r in dgv_SalesDetails.Rows)
                    {
                        if (r.Cells["NetWt"].Value.ToString() != "" && r.Cells["Wastage"].Value.ToString() != "" && r.Cells["Touch"].Value.ToString() != "")
                        {
                            pureGoldrate = Convert.ToSingle(txtGoldlRate.Text);
                            BookedGoldrate = Convert.ToSingle(txtBookedGoldRate.Text);
                            NetWt = Convert.ToSingle(r.Cells["NetWt"].Value.ToString());
                            Touch = Convert.ToSingle(r.Cells["Touch"].Value.ToString());
                            goldWt = NetWt * Touch / Convert.ToSingle(91.6);
                            // goldWt = Convert.ToSingle(r.Cells["NetWt"].Value.ToString());
                            //goldwst = Convert.ToSingle(r.Cells["GoldWt"].Value);
                            wst = Convert.ToSingle(r.Cells["Wastage"].Value);
                            // goldWt = goldwst - wst;
                            //  goldWt =NetWt.ToString (); 


                            if (BookedWt > (goldWt + wst))
                            {
                                wstCash = wst * BookedGoldrate;
                                r.Cells["WastageCash"].Value = wstCash.ToString();
                                MetalCash = goldWt * BookedGoldrate;
                                BookedWt = BookedWt - (goldWt + wst);

                            }
                            else if (BookedWt < (goldWt + wst) && BookedWt > 0)
                            {
                                remWt = (goldWt + wst) - BookedWt;
                                if (BookedWt >= (goldWt + wst))
                                {
                                    MetalCash = goldWt * BookedWt;
                                    wstCash = wst * pureGoldrate;
                                    r.Cells["WastageCash"].Value = wstCash.ToString();
                                }
                                else
                                {
                                    MetalCash = BookedWt * BookedGoldrate + (goldWt - BookedWt) * pureGoldrate;
                                    wstCash = wst * pureGoldrate;
                                    r.Cells["WastageCash"].Value = wstCash.ToString();
                                    BookedWt = BookedWt - (goldWt + wst);
                                }
                            }
                            else
                            {
                                MetalCash = goldWt * pureGoldrate;
                                wstCash = wst * pureGoldrate;
                                r.Cells["WastageCash"].Value = wstCash.ToString();
                            }

                        }
                        if (r.Cells["ItemId"].Value.ToString() != "")// && r.Cells["ProdCodeId"].Value.ToString() != "")
                        {
                            if (r.Cells["ProdCodeId"].Value.ToString() != "")
                                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                 ("SELECT NetWt FROM STK.VProdCodeMaster WHERE ProdCodeId =" + r.Cells["ProdCodeId"].Value + "")).Tables[0])
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        NetWt1 = Convert.ToSingle(dt.Rows[0]["NetWt"].ToString());
                                    }
                                }

                            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                            ("SELECT ItemCategoryId FROM ITM.VItemMaster WHERE ItemId =" + r.Cells["ItemId"].Value + "")).Tables[0])
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    ItemCatId = Convert.ToSingle(dt.Rows[0]["ItemCategoryId"].ToString());
                                }

                            }
                            //if (dgv_SalesDetails.CurrentRow.Cells["NetWt"].Value.ToString() != "")
                            //{
                            //    NetWt1  = Convert.ToSingle(dgv_Return.CurrentRow.Cells["NetWt"].Value.ToString());

                            //}
                            if (ItemCatId == 13)
                            {
                                Metal1Cash = NetWt1 * platinumRate;
                            }
                            else if (ItemCatId == 11)
                            {
                                Metal1Cash = MetalCash;
                            }
                            else if (ItemCatId == 12)
                            {
                                Metal1Cash = NetWt1 * SilverRate;
                            }
                            else if (ItemCatId == 15)
                            {
                                Metal1Cash = NetWt1 * SilverRate;
                            }
                            else if (ItemCatId == 16)
                            {
                                Metal1Cash = MetalCash;
                            }

                            r.Cells["MetalCash"].Value = (Metal1Cash).ToString();

                            if (r.Cells["DiaCash"].Value.ToString() != "" && r.Cells["StoneCash"].Value.ToString() != ""
                            && r.Cells["MetalCash"].Value.ToString() != "" && r.Cells["WastageCash"].Value.ToString() != ""
                            && r.Cells["MC"].Value.ToString() != "")
                            {
                                r.Cells["VA"].Value = ((Convert.ToSingle(r.Cells["WastageCash"].Value))
                                                   + (Convert.ToSingle(r.Cells["MC"].Value)));
                                r.Cells["TotalAmount"].Value = ((Convert.ToSingle(r.Cells["DiaCash"].Value))
                                                   + (Convert.ToSingle(r.Cells["StoneCash"].Value)) +
                                                    +(Convert.ToSingle(r.Cells["MetalCash"].Value)) +
                                                     +(Convert.ToSingle(r.Cells["WastageCash"].Value)) +
                                                      +(Convert.ToSingle(r.Cells["MC"].Value)));

                            }
                        }
                    }
                }
            }
            else
            {
                if (txtBookedGoldRate.Text != "" && txtGoldlRate.Text != "" && txt_TotalGoldWt.Text != "" && txtBookedWt.Text != "")
                {
                    SilverRate = Convert.ToSingle(txtSilverRate.Text);
                    platinumRate = Convert.ToSingle(txtPlatinumRate.Text);
                    BookedWt = Convert.ToSingle(txtBookedWt.Text);
                    foreach (DataGridViewRow r in dgv_SalesDetails.Rows)
                    {
                        if (r.Cells["ItemId"].Value.ToString() != "")
                        {
                            if (r.Cells["NetWt"].Value.ToString() != "0" && r.Cells["Wastage"].Value.ToString() != "" && r.Cells["Touch"].Value.ToString() != "")
                            {
                                pureGoldrate = Convert.ToSingle(txtGoldlRate.Text);
                                BookedGoldrate = Convert.ToSingle(txtBookedGoldRate.Text);
                                // goldwst = Convert.ToSingle(r.Cells["GoldWt"].Value);
                                wst = Convert.ToSingle(r.Cells["Wastage"].Value);
                                // goldWt = goldwst - wst;
                                NetWt = Convert.ToSingle(r.Cells["NetWt"].Value.ToString());
                                Touch = Convert.ToSingle(r.Cells["Touch"].Value.ToString());
                                goldWt = NetWt * Touch / Convert.ToSingle(91.6);
                                // goldWt = Convert.ToSingle(r.Cells["NetWt"].Value.ToString());

                                MetalCash = goldWt * pureGoldrate;
                                wstCash = wst * pureGoldrate;
                                r.Cells["WastageCash"].Value = wstCash.ToString();


                            }
                            if (r.Cells["ProdCodeId"].Value.ToString() != "")
                                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                ("SELECT NetWt FROM STK.VProdCodeMaster WHERE ProdCodeId =" + r.Cells["ProdCodeId"].Value + "")).Tables[0])
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        NetWt1 = Convert.ToSingle(dt.Rows[0]["NetWt"].ToString());
                                    }
                                }
                            if (r.Cells["ItemId"].Value.ToString() != "")
                                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                ("SELECT ItemCategoryId FROM ITM.VItemMaster WHERE ItemId =" + r.Cells["ItemId"].Value + "")).Tables[0])
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        ItemCatId = Convert.ToSingle(dt.Rows[0]["ItemCategoryId"].ToString());
                                    }
                                }
                            if (ItemCatId == 13)
                            {
                                Metal1Cash = NetWt1 * platinumRate;
                            }
                            else if (ItemCatId == 12)
                            {
                                Metal1Cash = NetWt1 * SilverRate;
                            }
                            else if (ItemCatId == 11)
                            {
                                Metal1Cash = MetalCash;
                            }
                            else if (ItemCatId == 15)
                            {
                                Metal1Cash = NetWt1 * SilverRate;
                            }
                            else if (ItemCatId == 16)
                            {
                                Metal1Cash = MetalCash;
                            }
                            r.Cells["MetalCash"].Value = (Metal1Cash).ToString();
                            if (r.Cells["DiaCash"].Value.ToString() != "" && r.Cells["StoneCash"].Value.ToString() != "" &&
                                r.Cells["MetalCash"].Value.ToString() != "" && r.Cells["WastageCash"].Value.ToString() != "" &&
                                r.Cells["MC"].Value.ToString() != "")
                            {
                                r.Cells["VA"].Value = ((Convert.ToSingle(r.Cells["WastageCash"].Value))
                                                  + (Convert.ToSingle(r.Cells["MC"].Value)));
                                r.Cells["TotalAmount"].Value = ((Convert.ToSingle(r.Cells["DiaCash"].Value))
                                       + (Convert.ToSingle(r.Cells["StoneCash"].Value)) +
                                        +(Convert.ToSingle(r.Cells["MetalCash"].Value)) +
                                         +(Convert.ToSingle(r.Cells["WastageCash"].Value)) +
                                          +(Convert.ToSingle(r.Cells["MC"].Value)));


                            }
                        }
                    }
                }
            }
        }


        private void Cmb_BookingNo_TextChanged(object sender, EventArgs e)
        {
            txtAdvancePaid.Text = "0.00";
            txtBookedAmount.Text = "0.00";
            txtBookedGoldRate.Text = "0.00";
            txtBookedWt.Text = "0.00";
        }
        public void MRP_Details()
        {
            if (Cmb_ProdCode_SDtl.SelectedValue != null)
            {
                txtMRP.Text = (DBConn.GetData(new System.Data.SqlClient.SqlCommand("select * From STK.ProdCodeMaster WHERE ProdCodeId =" + Cmb_ProdCode_SDtl.SelectedValue + "")).Tables[0].Rows[0]["MRP"]).ToString();
            }
        }
        public void MRP_DetailsReturn()
        {
            if (Cmb_ProdCodeReturn.SelectedValue != null)
            {
                txtMRP_Return.Text = (DBConn.GetData(new System.Data.SqlClient.SqlCommand("select * From STK.ProdCodeMaster WHERE ProdCodeId =" + Cmb_ProdCodeReturn.SelectedValue + "")).Tables[0].Rows[0]["MRP"]).ToString();
            }

        }

        private void Cmb_ItemName_SelectedValueChanged(object sender, EventArgs e)
        {
          
        }

        private void Cmb_PurityReturn_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void Cmb_SchemeNo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Cmb_SchemeNo.SelectedValue != null)
            {
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                 ("Select CustID,[Customer Name],[Maturity Amount] FROM SALE.VSavingSchemeJoinEntry WHERE JoinId=" + Cmb_SchemeNo.SelectedValue)).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtMaturityAmount.Text = (dt.Rows[0]["Maturity Amount"] == null ? "" : dt.Rows[0]["Maturity Amount"].ToString());
                        Cmb_CustName.Text = (dt.Rows[0]["Customer Name"] == null ? "" : dt.Rows[0]["Customer Name"].ToString());
                        txtCustId.Text = (dt.Rows[0]["CustID"] == null ? "" : dt.Rows[0]["CustID"].ToString());
                    }
                    else
                    {
                        Cmb_CustName.Text = "";
                        txtCustId.Text = "";
                        txtMaturityAmount.Text = "";
                    }
                }
                //if (Cmb_SchemeNo.SelectedValue != null)
                //{
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
               ("SELECT (SUM(PaymentAmount)+t2.OpnBalance) AS PaidAmount FROM SALE.[SavingSchemePaymentEntry ] t1,SALE.SavingSchemeJoinEntry  t2 WHERE t1.JoinId =t2.JoinId AND t1.JoinId =" + Cmb_SchemeNo.SelectedValue + " group by t1.PaymentAmount,t2.OpnBalance ")).Tables[0]) 
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtPaidAmount.Text = (dt.Rows[0]["PaidAmount"] == null ? "" : dt.Rows[0]["PaidAmount"].ToString());
                    }
                    else
                    {
                        txtPaidAmount.Text = "";
                    }
                }
            }
            else
            {
                txtPaidAmount.Text = "";
                txtMaturityAmount.Text = "";
            }
        }
        private void Cmb_SchemeNo_TextChanged(object sender, EventArgs e)
        {
            if (Cmb_SchemeNo.Text == "")
            {
                txtPaidAmount.Text = "";
            }
        }

        public void SalesType()
        {
            using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                // ("SELECT SalesTypename,SalesTypeId FROM SALE.VSalesApplicableMaster WHERE  SalesTypeId= (SELECT MAX(SalesTypeId) FROM SALE.VSalesApplicableMaster)")).Tables[0])
             ("SELECT SalesTypename,SalesTypeId FROM SALE.SalesTypeMaster WHERE  SalesTypeId= (SELECT MAX(SalesTypeId) FROM SALE.SalesTypeMaster)")).Tables[0])
            {
                Cmb_SalesType.SelectedValue = (dt.Rows[0]["SalesTypeId"]);
            }
        }

        private void Rb_Gold_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Gold.Checked == true)
            {
                Gramboo.General.Setupcombo(Cmb_ProdCode_SDtl, "STK.VProdCodeMaster", "ProdCode", "ProdCodeId", "[Is Active]='True' AND ItemCategoryId=11");//AND ItemCategoryId!=12 AND ItemCategoryId!=13");
                Gramboo.General.Setupcombo(Cmb_ProdCodeReturn, "STK.VProdCodeMaster", "ProdCode", "ProdCodeId", "[Is Active]='True' AND ItemCategoryId=11");// AND ItemCategoryId!=12 AND ItemCategoryId!=13");
                Gramboo.General.Setupcombo(Cmb_ItemName, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True' AND ItemCategoryId=11"); //AND ItemCategoryId!=12 AND ItemCategoryId!=13");
                Gramboo.General.Setupcombo(Cmb_ItemNameReturn, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True' AND ItemCategoryId=11");// AND ItemCategoryId!=12 AND ItemCategoryId!=13 ");

                DataFields_Gold_Silver_PlatinumSalesDetails();
                DataFieldsGold_Silver_PlatinumSalesReturn();
                Cmb_BookingNo.Enabled = true;
                txtAdvancePaid.Enabled = true;


            }

        }

       
        

        private void Rb_Silver_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Silver.Checked == true)
            {
                Gramboo.General.Setupcombo(Cmb_ProdCode_SDtl, "STK.VProdCodeMaster", "ProdCode", "ProdCodeId", "[Is Active]='True' AND ItemCategoryId=12");
                Gramboo.General.Setupcombo(Cmb_ProdCodeReturn, "STK.VProdCodeMaster", "ProdCode", "ProdCodeId", "[Is Active]='True' AND ItemCategoryId=12");
                Gramboo.General.Setupcombo(Cmb_ItemName, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True' AND ItemCategoryId=12");
                Gramboo.General.Setupcombo(Cmb_ItemNameReturn, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True' AND ItemCategoryId=12");

                DataFields_Gold_Silver_PlatinumSalesDetails();
                DataFieldsGold_Silver_PlatinumSalesReturn();
                Cmb_BookingNo.Enabled = false;
                txtAdvancePaid.Enabled = false;
            }

        }

        private void Rb_platinum_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_platinum.Checked == true)
            {
                Gramboo.General.Setupcombo(Cmb_ProdCode_SDtl, "STK.VProdCodeMaster", "ProdCode", "ProdCodeId", "[Is Active]='True' AND ItemCategoryId=13");
                Gramboo.General.Setupcombo(Cmb_ProdCodeReturn, "STK.VProdCodeMaster", "ProdCode", "ProdCodeId", "[Is Active]='True' AND ItemCategoryId=13");
                Gramboo.General.Setupcombo(Cmb_ItemName, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True' AND ItemCategoryId=13");
                Gramboo.General.Setupcombo(Cmb_ItemNameReturn, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True' AND ItemCategoryId=13");

                DataFields_Gold_Silver_PlatinumSalesDetails();
                DataFieldsGold_Silver_PlatinumSalesReturn();
                Cmb_BookingNo.Enabled = false;
                txtAdvancePaid.Enabled = false;
            }
        }

        private void Rb_DiamondS_CheckedChanged(object sender, EventArgs e)
        {
            //if (Rb_DiamondS.Checked == true)
            //{
            //    Gramboo.General.Setupcombo(Cmb_ProdCode_SDtl, "STK.VProdCodeMaster", "ProdCode", "ProdCodeId", "[Is Active]='True' AND ItemCategoryId in(15,16)");
            //    Gramboo.General.Setupcombo(Cmb_ProdCodeReturn, "STK.VProdCodeMaster", "ProdCode", "ProdCodeId", "[Is Active]='True' AND ItemCategoryId in(15,16)");
            //    Gramboo.General.Setupcombo(Cmb_ItemName, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True' AND ItemCategoryId in(15,16)");
            //    Gramboo.General.Setupcombo(Cmb_ItemNameReturn, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True' AND ItemCategoryId in(15,16)");

            //    DataFields_DiamondGold_SilverSalesDetails();
            //    DataFields_DiamondGold_SilverSalesreturn();
            //    Cmb_BookingNo.Enabled = false;
            //    txtAdvancePaid.Enabled = false;
            //}
        }


        private void Rb_DiamondG_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Diamond.Checked == true)
            {
                Gramboo.General.Setupcombo(Cmb_ProdCode_SDtl, "STK.VProdCodeMaster", "ProdCode", "ProdCodeId", "[Is Active]='True' AND ItemCategoryId in(15,16)");
                Gramboo.General.Setupcombo(Cmb_ProdCodeReturn, "STK.VProdCodeMaster", "ProdCode", "ProdCodeId", "[Is Active]='True' AND ItemCategoryId in(15,16)");
                Gramboo.General.Setupcombo(Cmb_ItemName, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True' AND ItemCategoryId in(15,16)");
                Gramboo.General.Setupcombo(Cmb_ItemNameReturn, "ITM.VOrnaments", "[Item Name]", "ItemId", "[Is Active]='True' AND ItemCategoryId in(15,16)");

                DataFields_DiamondGold_SilverSalesDetails();
                DataFields_DiamondGold_SilverSalesreturn();
                Cmb_BookingNo.Enabled = true;
                txtAdvancePaid.Enabled = true;
            }

            //else if (Rb_DiamondS.Checked == true)
            //{
            //    DataFields_DiamondGold_Silver();
            //}
            //else
            //{
            //    DataFieldsGold_Silver_Platinum();
            //}

        }

        public void DataFields_Gold_Silver_PlatinumSalesDetails()
        {
            if (Rb_Gold.Checked == true || Rb_platinum.Checked == true || Rb_Silver.Checked == true)
            {
                dgv_SalesDetails.DataSource = null;
                dgv_SalesDetails.DataFields = new List<string>() {"SalesId","TransId","ProdCodeId","ProdCode","ItemId","[ItemName]",
                "ModelId","[ModelName]","PurityID","PurityName","Touch", 
                "Qty","Gwt","MetalRate","[MetalCash]","DiaNo","DiaWt","DiaCash","[StoneWt]","[StoneCash]",
                "MC","Wastage","[NetWt]","[WastageCash]","VAPerc","VA","TotalAmount","IsReceipt","Type" };
                dgv_SalesDetails.HiddenDataFields = new List<string>() { "TransId","SalesId","PurityID","ItemId","ModelId","ProdCodeId","IsReceipt",
                "ModelName","DiaNo","DiaWt","DiaCash","Type","MC","Wastage","WastageCash"};
                dgv_SalesDetails.SummaryColumns = new string[] { "Qty", "Net Wt","MetalCash", 
                "StoneCash", "StoneWt","VA","Gwt", "TotalAmount" };
                dgv_SalesDetails.Fill(new Table("JMS", "SALE", "VSalesDetails", true), "1=2");
                dgv_SalesDetails.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
                AdjustColumnWidthsSalesDetails();
                SaleCurrentRow = -1;

                //dgv_Return.DataFields = new List<string>() {"SalesId","TransId","ProdCodeId","ProdCode","ItemId", "[ItemName]",
                //"ModelId","[ModelName]","PurityID","PurityName","Touch", 
                //"Qty","Gwt","MetalRate","[MetalCash]","DiaNo","DiaWt","DiaCash","[StoneWt]","[StoneCash]",
                //"MC","Wastage","[NetWt]","[WastageCash]","VA","TotalAmount","IsReceipt" };
                //dgv_Return.HiddenDataFields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "IsReceipt", 
                // "ModelName" };//
                //dgv_Return.SummaryColumns = new string[]{ "Qty", "Net Wt","MetalCash",
                //   "DiaNo","DiaWt","DiaCash","StoneCash", "StoneWt", "MC", "Wastage", "WastageCash","VA","Gwt", "TotalAmount" };
                //dgv_Return.Fill(new Table("JMS", "SALE", "VSalesReturnDetails", true), "1=2");
                //dgv_Return.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
                //AdjustColumnWidthsSalesReturn();
                //SaleCurrentretRow = -1;

                CalcFlag = true;


                dgv_SalesDetails.RowTemplate.Height = Cmb_ProdCode_SDtl.Height;
                dgv_SalesDetails.AllowUserToAddRows = false;
                ((System.Data.DataTable)dgv_SalesDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_SalesDetails.DataSource).NewRow());
                dgv_SalesDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgv_SalesDetails.CurrentCell = dgv_SalesDetails.Rows[0].Cells["ProdCode"];
                dgv_SalesDetails.BeginEdit(true);
                txtMRP.Text = "";
                SaleCurrentRow = -1;

             


                //dgv_Return.RowTemplate.Height = Cmb_ProdCodeReturn.Height;
                //dgv_Return.AllowUserToAddRows = false;
                ////if (dgv_Return.Rows.Count == 0)
                //((System.Data.DataTable)dgv_Return.DataSource).Rows.Add(((System.Data.DataTable)dgv_Return.DataSource).NewRow());
                //dgv_Return.SelectionMode = DataGridViewSelectionMode.CellSelect;
                //dgv_Return.CurrentCell = dgv_Return.Rows[0].Cells["ProdCode"];
                //dgv_Return.BeginEdit(true);
                //SaleCurrentretRow = -1;
            }
        }

        public void DataFieldsGold_Silver_PlatinumSalesReturn()
        {
            if (Rb_Gold.Checked == true || Rb_platinum.Checked == true || Rb_Silver.Checked == true)
            {
               // dgv_Return.DataSource = null;
                dgv_Return.DataFields = new List<string>() {"SalesId","TransId","ProdCodeId","ProdCode","ItemId", "[ItemName]",
                "ModelId","[ModelName]","PurityID","PurityName","Touch",
                "Qty","Gwt","MetalRate","[MetalCash]","DiaNo","DiaWt","DiaCash","[StoneWt]","[StoneCash]",
                "MC","Wastage","[NetWt]","[WastageCash]","VAPerc","VA","TotalAmount","IsReceipt","Type" };
                dgv_Return.HiddenDataFields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "IsReceipt", 
                 "ModelName","DiaNo","DiaWt","DiaCash","Type","MC","Wastage","WastageCash" };//
                dgv_Return.SummaryColumns = new string[]{ "Qty", "Net Wt","MetalCash",
                   "DiaNo","DiaWt","DiaCash","StoneCash", "StoneWt","VA","Gwt", "TotalAmount" };
                dgv_Return.Fill(new Table("JMS", "SALE", "VSalesReturnDetails", true), "1=2");
                dgv_Return.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
                AdjustColumnWidthsSalesReturn();
                SaleCurrentretRow = -1;

                CalcFlag = true;

                dgv_Return.RowTemplate.Height = Cmb_ProdCodeReturn.Height;
                dgv_Return.AllowUserToAddRows = false;
                //if (dgv_Return.Rows.Count == 0)
                ((System.Data.DataTable)dgv_Return.DataSource).Rows.Add(((System.Data.DataTable)dgv_Return.DataSource).NewRow());
                dgv_Return.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgv_Return.CurrentCell = dgv_Return.Rows[0].Cells["ProdCode"];
                dgv_Return.BeginEdit(true);
                SaleCurrentretRow = -1;
            }
        }
        public void DataFields_DiamondGold_SilverSalesDetails()
        {
            if (Rb_Diamond.Checked == true)
            {
                dgv_SalesDetails.DataSource = null;
                dgv_SalesDetails.DataFields = new List<string>() {"SalesId","TransId","ProdCodeId","ProdCode","ItemId","ItemName",
               "ModelId","ModelName","PurityID","PurityName","Touch", 
               "Qty","Gwt","MetalRate","MetalCash","DiaNo","DiaWt","DiaCash","StoneWt","StoneCash",
               "MC","Wastage","[NetWt]","[WastageCash]","VAPerc","VA","TotalAmount","IsReceipt","Type" };
                dgv_SalesDetails.HiddenDataFields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId",
                "ProdCodeId", "IsReceipt","ModelName","Type","MC","Wastage","WastageCash" };
                dgv_SalesDetails.SummaryColumns = new string[] { "Qty", "Net Wt","MetalCash", "DiaNo", "DiaCash", 
               "DiaWt", "StoneNo", "StoneCash", "StoneWt","VA","Gwt", "TotalAmount" };
                dgv_SalesDetails.Fill(new Table("JMS", "SALE", "VSalesDetails", true), "1=2");
                dgv_SalesDetails.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
                AdjustColumnWidthsSalesDetails();
                SaleCurrentRow = -1;

                //  dgv_Return.DataFields = new List<string>() {"SalesId","TransId","ProdCodeId","ProdCode","ItemId", "[ItemName]",
                //   "ModelId","ModelName","PurityID","PurityName","Touch", 
                //   "Qty","Gwt","MetalRate","MetalCash","DiaNo","DiaWt","DiaCash","StoneWt","StoneCash",
                //   "MC","Wastage","NetWt","WastageCash","VA","TotalAmount","IsReceipt" };
                //  dgv_Return.HiddenDataFields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId",
                //   "ProdCodeId", "IsReceipt","ModelName" };
                //  dgv_Return.SummaryColumns = new string[]{ "Qty", "Net Wt","MetalCash", "DiaNo", "DiaCash", 
                //"DiaWt","StoneCash", "StoneWt", "MC", "Wastage", "WastageCash","VA","Gwt", "TotalAmount" };
                //  dgv_Return.Fill(new Table("JMS", "SALE", "VSalesReturnDetails", true), "1=2");
                //  dgv_Return.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
                //  AdjustColumnWidthsSalesReturn();
                //  SaleCurrentretRow = -1;

                CalcFlag = true;


                dgv_SalesDetails.RowTemplate.Height = Cmb_ProdCode_SDtl.Height;
                dgv_SalesDetails.AllowUserToAddRows = false;
                ((System.Data.DataTable)dgv_SalesDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_SalesDetails.DataSource).NewRow());
                dgv_SalesDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgv_SalesDetails.CurrentCell = dgv_SalesDetails.Rows[0].Cells["ProdCode"];
                dgv_SalesDetails.BeginEdit(true);
                txtMRP.Text = "";
                SaleCurrentRow = -1;

                //dgv_Return.RowTemplate.Height = Cmb_ProdCodeReturn.Height;
                //dgv_Return.AllowUserToAddRows = false;             
                //((System.Data.DataTable)dgv_Return.DataSource).Rows.Add(((System.Data.DataTable)dgv_Return.DataSource).NewRow());
                //dgv_Return.SelectionMode = DataGridViewSelectionMode.CellSelect;
                //dgv_Return.CurrentCell = dgv_Return.Rows[0].Cells["ProdCode"];
                //dgv_Return.BeginEdit(true);
                //SaleCurrentretRow = -1;
            }
        }
        public void DataFields_DiamondGold_SilverSalesreturn()
        {
            if (Rb_Diamond.Checked == true)
            {
                // dgv_Return.DataSource = null;

                dgv_Return.DataFields = new List<string>() {"SalesId","TransId","ProdCodeId","ProdCode","ItemId", "[ItemName]",
                "ModelId","[ModelName]","PurityID","PurityName","Touch", 
                "Qty","Gwt","MetalRate","[MetalCash]","DiaNo","DiaWt","DiaCash","[StoneWt]","[StoneCash]",
                "MC","Wastage","[NetWt]","[WastageCash]","VAPerc","VA","TotalAmount","IsReceipt","Type" };
                dgv_Return.HiddenDataFields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "IsReceipt", 
                 "ModelName","Type","MC","Wastage","WastageCash"};//
                dgv_Return.SummaryColumns = new string[]{ "Qty", "Net Wt","MetalCash",
                   "DiaNo","DiaWt","DiaCash","StoneCash", "StoneWt","VA","Gwt", "TotalAmount" };
                dgv_Return.Fill(new Table("JMS", "SALE", "VSalesReturnDetails", true), "1=2");
                dgv_Return.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
                AdjustColumnWidthsSalesReturn();
                SaleCurrentretRow = -1;

                CalcFlag = true;

                dgv_Return.RowTemplate.Height = Cmb_ProdCodeReturn.Height;
                dgv_Return.AllowUserToAddRows = false;
                //if (dgv_Return.Rows.Count == 0)
                ((System.Data.DataTable)dgv_Return.DataSource).Rows.Add(((System.Data.DataTable)dgv_Return.DataSource).NewRow());
                dgv_Return.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgv_Return.CurrentCell = dgv_Return.Rows[0].Cells["ProdCode"];
                dgv_Return.BeginEdit(true);
                SaleCurrentretRow = -1;
            }
        }
        private void AdjustColumnWidthsSalesDetails()
        {

            dgv_SalesDetails.RowHeadersVisible = false;

            dgv_SalesDetails.Columns[0].Width = 40;
            //if (dgv_SalesDetails.Columns.Contains("ProdCode"))
            //    dgv_SalesDetails.Columns["ProdCode"].Width = Cmb_ProdCode_SDtl.Width + 10;
            if (dgv_SalesDetails.Columns.Contains("ItemName"))
                dgv_SalesDetails.Columns["ItemName"].Width = Cmb_ItemName.Width + 10;
            if (dgv_SalesDetails.Columns.Contains("PurityName"))
                dgv_SalesDetails.Columns["PurityName"].Width = cmb_purity.Width + 8;
            if (dgv_SalesDetails.Columns.Contains("Touch"))
                dgv_SalesDetails.Columns["Touch"].Width = 50;
            if (dgv_SalesDetails.Columns.Contains("Qty"))
                dgv_SalesDetails.Columns["Qty"].Width = 50;
            if (dgv_SalesDetails.Columns.Contains("Gwt"))
                dgv_SalesDetails.Columns["Gwt"].Width = 50;
            if (dgv_SalesDetails.Columns.Contains("MetalRate"))
                dgv_SalesDetails.Columns["MetalRate"].Width = 70;
            if (dgv_SalesDetails.Columns.Contains("MetalCash"))
                dgv_SalesDetails.Columns["MetalCash"].Width = 80;
            if (dgv_SalesDetails.Columns.Contains("DiaNo"))
                dgv_SalesDetails.Columns["DiaNo"].Width = 50;
            if (dgv_SalesDetails.Columns.Contains("DiaWt"))
                dgv_SalesDetails.Columns["DiaWt"].Width = 50;
            if (dgv_SalesDetails.Columns.Contains("DiaCash"))
                dgv_SalesDetails.Columns["DiaCash"].Width = 70;
            if (dgv_SalesDetails.Columns.Contains("StoneWt"))
                dgv_SalesDetails.Columns["StoneWt"].Width = 70;
            if (dgv_SalesDetails.Columns.Contains("StoneCash"))
                dgv_SalesDetails.Columns["StoneCash"].Width = 70;
            if (dgv_SalesDetails.Columns.Contains("MC"))
                dgv_SalesDetails.Columns["MC"].Width = 50;
            if (dgv_SalesDetails.Columns.Contains("Wastage"))
                dgv_SalesDetails.Columns["Wastage"].Width = 60;
            if (dgv_SalesDetails.Columns.Contains("WastageCash"))
                dgv_SalesDetails.Columns["WastageCash"].Width = 85;
            if (dgv_SalesDetails.Columns.Contains("VA"))
                dgv_SalesDetails.Columns["VA"].Width = 60;
            if (dgv_SalesDetails.Columns.Contains("VAPerc"))
                dgv_SalesDetails.Columns["VAPerc"].Width = 60;
            if (dgv_SalesDetails.Columns.Contains("NetWt"))
                dgv_SalesDetails.Columns["NetWt"].Width = 50;
            if (dgv_SalesDetails.Columns.Contains("TotalAmount"))
                dgv_SalesDetails.Columns["TotalAmount"].Width = 100;
        }


        private void AdjustColumnWidthsSalesReturn()
        {

            dgv_Return.RowHeadersVisible = false;

            dgv_Return.Columns[0].Width = 40;
            //if (dgv_Return.Columns.Contains("ProdCode"))
            //    dgv_Return.Columns["ProdCode"].Width = Cmb_ProdCodeReturn.Width + 10;
            if (dgv_Return.Columns.Contains("ItemName"))
                dgv_Return.Columns["ItemName"].Width = Cmb_ItemNameReturn.Width + 10;
            if (dgv_Return.Columns.Contains("PurityName"))
                dgv_Return.Columns["PurityName"].Width = Cmb_PurityReturn.Width + 8;
            if (dgv_Return.Columns.Contains("Touch"))
                dgv_Return.Columns["Touch"].Width = 50;
            if (dgv_Return.Columns.Contains("Qty"))
                dgv_Return.Columns["Qty"].Width = 50;
            if (dgv_Return.Columns.Contains("Gwt"))
                dgv_Return.Columns["Gwt"].Width = 50;
            if (dgv_Return.Columns.Contains("MetalRate"))
                dgv_Return.Columns["MetalRate"].Width = 70;
            if (dgv_Return.Columns.Contains("MetalCash"))
                dgv_Return.Columns["MetalCash"].Width = 80;
            if (dgv_Return.Columns.Contains("DiaNo"))
                dgv_Return.Columns["DiaNo"].Width = 50;
            if (dgv_Return.Columns.Contains("DiaWt"))
                dgv_Return.Columns["DiaWt"].Width = 50;
            if (dgv_Return.Columns.Contains("DiaCash"))
                dgv_Return.Columns["DiaCash"].Width = 70;
            if (dgv_Return.Columns.Contains("StoneWt"))
                dgv_Return.Columns["StoneWt"].Width = 70;
            if (dgv_Return.Columns.Contains("StoneCash"))
                dgv_Return.Columns["StoneCash"].Width = 70;
            if (dgv_Return.Columns.Contains("MC"))
                dgv_Return.Columns["MC"].Width = 50;
            if (dgv_Return.Columns.Contains("Wastage"))
                dgv_Return.Columns["Wastage"].Width = 60;
            if (dgv_Return.Columns.Contains("WastageCash"))
                dgv_Return.Columns["WastageCash"].Width = 85;
            if (dgv_Return.Columns.Contains("VA"))
                dgv_Return.Columns["VA"].Width = 60;
            if (dgv_Return.Columns.Contains("VAPerc"))
                dgv_Return.Columns["VAPerc"].Width = 60;
            if (dgv_Return.Columns.Contains("NetWt"))
                dgv_Return.Columns["NetWt"].Width = 50;
            if (dgv_Return.Columns.Contains("TotalAmount"))
                dgv_Return.Columns["TotalAmount"].Width = 100;
        }
       

        private void Cmb_SalesType_TextChanged(object sender, EventArgs e)
        {
            //SalesTax();
        }

        private void btnAddNewCustomer_Load(object sender, EventArgs e)
        {

        }

        private void dgv_SalesDetails_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void Cmb_SchemeNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Lnk_SchemeDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PnlSchemeJoinDetails.Visible == false)
            {
                PnlSchemeJoinDetails.Size = new System.Drawing.Size(300, 150);
                PnlSchemeJoinDetails.Parent = this;
                PnlSchemeJoinDetails.Visible = true;
                PnlSchemeJoinDetails.BringToFront();

                PnlSchemeJoinDetails.Location = new Point(Lnk_SchemeDetails.Location.X + 100 + Lnk_SchemeDetails.Parent.Location.X + 50,
                Lnk_SchemeDetails.Parent.Location.Y + 0+ Lnk_SchemeDetails.Location.Y +0);

                PnlSchemeJoinDetails.Show();
                PnlSchemeJoinDetails.BringToFront();
                Lnk_SchemeDetails.Text = " Hide Scheme Details";
            }
            else
            {
                PnlSchemeJoinDetails.Visible = false;
                PnlSchemeJoinDetails.SendToBack();
                PnlSchemeJoinDetails.Hide();
                Lnk_SchemeDetails.Text = "Show Scheme Details";
            }
            
            
        }

        private void Cmb_ProdCode_SDtl_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Cmb_ProdCode_SDtl.SelectedValue != null)
            {
                //dgv_SalesDetails.Columns["TotalAmount"].ReadOnly = true;
                //dgv_SalesDetails.Columns["VA"].ReadOnly = true;
            }
        }
        //****************************************************SALES EXCHANGE************************************************************//

        public void AdjustColumnWidthExchange()
        {
            dgv_Exchange.RowHeadersVisible = false;

            dgv_Exchange.Columns[0].Width = 40;

            if (dgv_Exchange.Columns.Contains("ItemName"))
                dgv_Exchange.Columns["ItemName"].Width = Cmb_ItemName_Ex.Width + 35;
            if (dgv_Exchange.Columns.Contains("PurityValue"))
                dgv_Exchange.Columns["PurityValue"].Width = 80;
            if (dgv_Exchange.Columns.Contains("Qty"))
                dgv_Exchange.Columns["Qty"].Width = 60;
            if (dgv_Exchange.Columns.Contains("Gwt"))
                dgv_Exchange.Columns["Gwt"].Width = 70;
            if (dgv_Exchange.Columns.Contains("MetalRate"))
                dgv_Exchange.Columns["MetalRate"].Width = 80;
            if (dgv_Exchange.Columns.Contains("MetalCash"))
                dgv_Exchange.Columns["MetalCash"].Width = 80;
            if (dgv_Exchange.Columns.Contains("DiaNo"))
                dgv_Exchange.Columns["DiaNo"].Width = 60;
            if (dgv_Exchange.Columns.Contains("DiaWt"))
                dgv_Exchange.Columns["DiaWt"].Width = 60;
            if (dgv_Exchange.Columns.Contains("DiaCash"))
                dgv_Exchange.Columns["DiaCash"].Width = 80;
            if (dgv_Exchange.Columns.Contains("StoneWt"))
                dgv_Exchange.Columns["StoneWt"].Width = 70;
            if (dgv_Exchange.Columns.Contains("StoneCash"))
                dgv_Exchange.Columns["StoneCash"].Width = 80;
            if (dgv_Exchange.Columns.Contains("NetWt"))
                dgv_Exchange.Columns["NetWt"].Width = 60;
            if (dgv_Exchange.Columns.Contains("TotalAmount"))
                dgv_Exchange.Columns["TotalAmount"].Width = 100;
        }

        private void dgv_Exchange_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_Exchange.SelectedCells.Count > 0)
            {
                if (dgv_Exchange.SelectedCells[0].OwningColumn.DataPropertyName == "ItemName")
                {
                    try
                    {
                        SaleExchange = dgv_Exchange.SelectedCells[0].RowIndex;
                        Cmb_ItemName_Ex.Parent = tabPage4;
                        Cmb_ItemName_Ex.Visible = true;
                        Cmb_ItemName_Ex.Text = dgv_Exchange.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_Exchange.GetCellDisplayRectangle(dgv_Exchange.SelectedCells[0].ColumnIndex, dgv_Exchange.SelectedCells[0].RowIndex, true).Location;
                        Cmb_ItemName_Ex.Location = new System.Drawing.Point(p.X + dgv_Exchange.Parent.Location.X, p.Y + dgv_Exchange.Parent.Location.Y);


                        Cmb_ItemName_Ex.Size = dgv_Exchange.GetCellDisplayRectangle(dgv_Exchange.SelectedCells[0].ColumnIndex, dgv_Exchange.SelectedCells[0].RowIndex, true).Size;
                        Cmb_ItemName_Ex.BringToFront();
                        Cmb_ItemName_Ex.Focus();
                    }
                    catch { }
                }
            }
        }

        private void Cmb_ItemName_Ex_Leave(object sender, EventArgs e)
        {
           
        }

        private void dgv_Exchange_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_Exchange.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
               dgv_Exchange.Columns[e.ColumnIndex].DataPropertyName == "DiaWt" ||
               dgv_Exchange.Columns[e.ColumnIndex].DataPropertyName == "StoneWt")
            {
                 CalculateWtExchange();
            }
            if (dgv_Exchange.Columns[e.ColumnIndex].DataPropertyName == "DiaCash" ||
                dgv_Exchange.Columns[e.ColumnIndex].DataPropertyName == "StoneCash" ||
                dgv_Exchange.Columns[e.ColumnIndex].DataPropertyName == "MetalCash")
            {
                  TotalAmountExchange();
            }
            //if (dgv_Exchange.Columns[e.ColumnIndex].DataPropertyName == "ItemName")// ||
            //{
            //    MetalRateExchange();
            //}
            if (dgv_Exchange.Columns[e.ColumnIndex].DataPropertyName == "NetWt" ||
                dgv_Exchange.Columns[e.ColumnIndex].DataPropertyName == "MetalRate" ||
                dgv_Exchange.Columns[e.ColumnIndex].DataPropertyName == "Touch")
            {
                 MetalCashExchange();
            }
            if (dgv_Exchange.CurrentRow == null)
                return;

            if (dgv_Exchange.CurrentRow.Cells["Qty"].Value.ToString() == "")
                dgv_Exchange.CurrentRow.Cells["Qty"].Value = "0";
            if (dgv_Exchange.CurrentRow.Cells["Gwt"].Value.ToString() == "")
                dgv_Exchange.CurrentRow.Cells["Gwt"].Value = "0";
            if (dgv_Exchange.CurrentRow.Cells["MetalRate"].Value.ToString() == "")
                dgv_Exchange.CurrentRow.Cells["MetalRate"].Value = "0";
            if (dgv_Exchange.CurrentRow.Cells["Type"].Value.ToString() == "")
                dgv_Exchange.CurrentRow.Cells["Type"].Value = "E";


            if (Cmb_ItemName_Ex.SelectedValue != null && Cmb_ItemName_Ex.Text.Trim().Length > 0
             && Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["Qty"].Value) != 0
                && Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["Gwt"].Value) != 0
                  && Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["MetalRate"].Value) != 0)
            {
                if (dgv_Exchange.Rows.Count > 0)
                {
                    if (dgv_Exchange.Rows[dgv_Exchange.Rows.Count - 1].Cells["ItemId"].Value.ToString() != "")
                    {
                        ((System.Data.DataTable)dgv_Exchange.DataSource).Rows.Add(((System.Data.DataTable)dgv_Exchange.DataSource).NewRow());

                        dgv_Exchange.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        dgv_Exchange.CurrentCell = dgv_Exchange.Rows[SaleExchange + 1].Cells["ItemName"];



                    }
                }
            }
        }        

        
        private void MetalCashExchange()
        {
            float MetalRate = 0, NetWt = 0, VA = 0, Touch = 0, MetalCash = 0, Wt916 = 0, ItemCategory = 0,PurityValue=0;
            if (dgv_Exchange.CurrentRow == null)
                return;
            foreach (DataGridViewRow r in dgv_Exchange.Rows)
            {
                if (r.Cells["ItemId"].Value.ToString() != "")
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                    ("SELECT ItemCategoryId FROM ITM.VItemMaster WHERE ItemId =" + r.Cells["ItemId"].Value + "")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {

                            ItemCategory = Convert.ToSingle(dt.Rows[0]["ItemCategoryId"].ToString());
                        }
                    }
                }
            }
            if (dgv_Exchange.CurrentRow.Cells["MetalRate"].Value.ToString() != "")
            {
                MetalRate = Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["MetalRate"].Value.ToString());

            }
            if (dgv_Exchange.CurrentRow.Cells["NetWt"].Value.ToString() != "")
            {
                NetWt = Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["NetWt"].Value.ToString());
            }

            if (dgv_Exchange.CurrentRow.Cells["PurityValue"].Value.ToString() != "")
            {
                PurityValue  = Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["PurityValue"].Value.ToString());
            }
            if (ItemCategory == 11 || ItemCategory == 16)
            {
                Wt916 = NetWt * PurityValue / Convert.ToSingle(91.6);
                MetalCash = Wt916 * MetalRate;
            }
            else
            {
                MetalCash = NetWt * MetalRate;
            }
            dgv_Exchange.CurrentRow.Cells["MetalCash"].Value = MetalCash.ToString();
        }
        private void MetalRateExchange()
        {
            float ItemCategory = 0, Metalrate = 0;
            foreach (DataGridViewRow r in dgv_Exchange.Rows)
            {
                if (r.Cells["ItemId"].Value.ToString() != "")
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                    ("SELECT ItemCategoryId FROM ITM.VItemMaster WHERE ItemId =" + r.Cells["ItemId"].Value + "")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {

                            ItemCategory = Convert.ToSingle(dt.Rows[0]["ItemCategoryId"].ToString());
                        }
                        if (ItemCategory == 11)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["BoardRate"]));

                        }
                        else if (ItemCategory == 12)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["SilverRate"]));
                        }
                        else if (ItemCategory == 13)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 PlatinumRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["PlatinumRate"]));
                        }
                        else if (ItemCategory == 15)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["SilverRate"]));
                        }
                        else if (ItemCategory == 16)
                        {
                            Metalrate = (Convert.ToSingle(DBConn.GetData(new SqlCommand("SELECT TOP 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate DESC ")).Tables[0].Rows[0]["BoardRate"]));
                        }
                        dgv_Exchange.CurrentRow.Cells["MetalRate"].Value = Metalrate.ToString();
                    }

                }
            }
        }
        private void TotalAmountExchange()
        {

            float Diacash = 0, Stcash = 0, GoldCash = 0, TotalCash = 0, MC = 0, WastCash = 0, SetCash = 0;

            if (dgv_Exchange.CurrentRow == null)
                return;
            if (dgv_Exchange.CurrentRow.Cells["MetalCash"].Value.ToString() != "")
            {
                GoldCash = Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["MetalCash"].Value.ToString());

            }

            if (dgv_Exchange.CurrentRow.Cells["StoneCash"].Value.ToString() != "")
            {
                Stcash = Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["StoneCash"].Value.ToString());

            }
            if (dgv_Exchange.CurrentRow.Cells["DiaCash"].Value.ToString() != "")
            {
                Diacash = Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["DiaCash"].Value.ToString());

            }

            TotalCash = GoldCash + Stcash + Diacash;

            dgv_Exchange.CurrentRow.Cells["TotalAmount"].Value = TotalCash.ToString();

        }
        public void CalculateWtExchange()
        {
            float NetWt = 0, StoneWt = 0, DiaWt = 0, DiaStWt = 0, TotalWt = 0, Gwt = 0;
            if (dgv_Exchange.CurrentRow == null)
                return;
            if (dgv_Exchange.CurrentRow.Cells["Gwt"].Value.ToString() != "")
            {
                Gwt = Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["Gwt"].Value.ToString());
            }

            if (dgv_Exchange.CurrentRow.Cells["StoneWt"].Value.ToString() != "")
            {
                StoneWt = Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["StoneWt"].Value.ToString());
            }

            if (dgv_Exchange.CurrentRow.Cells["DiaWt"].Value.ToString() != "")
            {
                DiaWt = Convert.ToSingle(dgv_Exchange.CurrentRow.Cells["DiaWt"].Value.ToString());
            }

            DiaStWt = Convert.ToSingle((StoneWt + DiaWt) * .2);
            TotalWt = Gwt - DiaStWt;
            dgv_Exchange.CurrentRow.Cells["NetWt"].Value = TotalWt.ToString();
        }

        private void dgv_Exchange_SummaryCalculated(object source, EventArgs e)
        {
            txtExchangeAmount.Text = dgv_Exchange.SummaryRow.SummaryCells["TotalAmount"].Text;
        }

        private void chkAddressDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAddressDetails.Checked == true)
            {
                grpAdd.Visible = true;
            }
            else
            {
                grpAdd.Visible = false;
            }
        }

        private void Cmb_ItemName_Ex_Leave_1(object sender, EventArgs e)
        {
            Cmb_ItemName_Ex.Visible = false;
            if (Cmb_ItemName_Ex.Text != "" && Cmb_ItemName_Ex.SelectedValue != null)
            {
                dgv_Exchange.CurrentRow.Cells["ItemId"].Value = Cmb_ItemName_Ex.SelectedValue.ToString();
                dgv_Exchange.CurrentRow.Cells["ItemName"].Value = Cmb_ItemName_Ex.Text;
                //  enable();
            }
        }





        private void grpAdd_Enter(object sender, EventArgs e)
        {

        }
        public void PrintDotMatrix()
        {

            //PrintDialog prnDialog;
            //PrintPreviewDialog prnPreview;
            //PrintDocument prnDocument;
            //DataGridTableStyle tableStyle = new DataGridTableStyle();
            //DataGrid ordGrid = new DataGrid();
            using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
              ("Select [Invoice No],BranchName,[Invoice Date],PhoneNo,DiscAmount,GrandTotal,[old Gold No],TaxPerc,TotalTaxAmount,NetTotal,TotalCashPaid,TotalOldGoldReceipt,FinalAmount,[Customer Name],Address1,Address2,Comp_Name,Comp_Addr1,Comp_Addr2,Comp_Place,Comp_City,Comp_District,Comp_State,Comp_Pin,Comp_Phone,Comp_TIN,Comp_CST FROM SALE.VSalesMaster WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    Company = (dt.Rows[0]["Comp_Name"] == null ? "" : dt.Rows[0]["Comp_Name"].ToString());
                    Comp_Add1 = (dt.Rows[0]["Comp_Addr1"] == null ? "" : dt.Rows[0]["Comp_Addr1"].ToString());
                    Comp_Add2 = (dt.Rows[0]["Comp_Addr2"] == null ? "" : dt.Rows[0]["Comp_Addr2"].ToString());
                    Comp_Place = (dt.Rows[0]["Comp_Place"] == null ? "" : dt.Rows[0]["Comp_Place"].ToString());
                    Comp_City = (dt.Rows[0]["Comp_City"] == null ? "" : dt.Rows[0]["Comp_City"].ToString());
                    Comp_Dist = (dt.Rows[0]["Comp_District"] == null ? "" : dt.Rows[0]["Comp_District"].ToString());
                    Comp_State = (dt.Rows[0]["Comp_State"] == null ? "" : dt.Rows[0]["Comp_State"].ToString());
                    Comp_pin = Convert.ToInt32(dt.Rows[0]["Comp_Pin"] == null ? "" : dt.Rows[0]["Comp_Pin"].ToString());
                    Comp_phone = (dt.Rows[0]["Comp_Phone"] == null ? "" : dt.Rows[0]["Comp_Phone"].ToString());
                    TinNo = (dt.Rows[0]["Comp_TIN"] == null ? "" : dt.Rows[0]["Comp_TIN"].ToString());
                    CstNo = (dt.Rows[0]["Comp_CST"] == null ? "" : dt.Rows[0]["Comp_CST"].ToString());
                    InvoiceDate = (dt.Rows[0]["Invoice Date"] == null ? "" : dt.Rows[0]["Invoice Date"].ToString());
                    DisAmt = Convert.ToSingle(dt.Rows[0]["DiscAmount"] == null ? "" : dt.Rows[0]["DiscAmount"].ToString());
                    oldGoldRpt = Convert.ToSingle(dt.Rows[0]["TotalOldGoldReceipt"] == null ? "" : dt.Rows[0]["TotalOldGoldReceipt"].ToString());
                    netTotal = Convert.ToSingle(dt.Rows[0]["NetTotal"] == null ? "" : dt.Rows[0]["NetTotal"].ToString());
                    TaxAmt = Convert.ToSingle(dt.Rows[0]["TotalTaxAmount"] == null ? "" : dt.Rows[0]["TotalTaxAmount"].ToString());
                    FinalAmt = Convert.ToSingle(dt.Rows[0]["FinalAmount"] == null ? "" : dt.Rows[0]["FinalAmount"].ToString());
                    InvoiceNo = (dt.Rows[0]["Invoice No"] == null ? "" : dt.Rows[0]["Invoice No"].ToString());
                    CustomerName  = (dt.Rows[0]["Customer Name"] == null ? "" : dt.Rows[0]["Customer Name"].ToString());
                    Address1 = (dt.Rows[0]["Address1"] == null ? "" : dt.Rows[0]["Address1"].ToString());
                    PhoneNo = (dt.Rows[0]["PhoneNo"] == null ? "" : dt.Rows[0]["PhoneNo"].ToString());
                    GrandTotal = Convert.ToSingle(dt.Rows[0]["GrandTotal"] == null ? "" : dt.Rows[0]["GrandTotal"].ToString());
                    TaxPerc = Convert.ToSingle(dt.Rows[0]["TaxPerc"] == null ? "" : dt.Rows[0]["TaxPerc"].ToString());
                    oldGoldNo = (dt.Rows[0]["old Gold No"] == null ? "" : dt.Rows[0]["old Gold No"].ToString());
                    BranchName = (dt.Rows[0]["BranchName"] == null ? "" : dt.Rows[0]["BranchName"].ToString());
                    CashPaid = Convert.ToSingle(dt.Rows[0]["TotalCashPaid"] == null ? "" : dt.Rows[0]["TotalCashPaid"].ToString());


                }
                AmtPayable = (netTotal - CashPaid);
            }
        }
        //public void  printDetails()
        //{
        //    using (DataTable dtdetails = DBConn.GetData(new System.Data.SqlClient.SqlCommand
        //      ("Select prodCode,ItemName,Qty,Gwt,StoneWt,StoneCash,VAPerc,TotalAmount,VApercAftDis FROM SALE.SalesDotPrint WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0])
        //    {
        //        foreach (DataRow  r in dtdetails.Rows)
        //        {
        //            ProdCode=r["prodCode"].ToString ();
        //            ItemName = r["ItemName"].ToString();
        //            Qty =Convert.ToInt32 (r["Qty"].ToString());
        //            Gwt = Convert.ToSingle(r["Gwt"].ToString());
        //            StWt = Convert.ToSingle(r["StoneWt"].ToString());
        //            StoneCash = Convert.ToSingle(r["StoneCash"].ToString());
        //            VaPerc = Convert.ToSingle(r["VAPerc"].ToString());
        //            Total = Convert.ToSingle(r["TotalAmount"].ToString());
        //            VaPercAftDis = Convert.ToSingle(r["VApercAftDis"].ToString());
        //            Printer p = new Printer();
        //            p.NewLine = true;
        //            p.PrintAlignment = PrintAlign.Left;
        //            p.PrintString("" + ProdCode);
                  
 
        //        }
        //    }
        //}    
        public void ReadInvoiceHead()
        {
            

        }
        public void DataTableReport()
        {
           
        }
        //private void SetInvoiceHead( )
        //{
        // //   ReadInvoiceHead();
        //    PrintDotMatrix();

        //    Printer p = new Printer();
        //    p.StartPage += new JMS.Printer.NextPageHandler(PrintHeader);
        //    p.EndPage += new JMS.Printer.Footer(PrintFooter);  
         
        //    p.PrintAlignment = PrintAlign.Left; 
        //    p.FontSize = 10; 
        //    p.PrintString("INVOICE No.    :"+InvoiceNo );
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.NewLine = false;
        //    p.PrintString("22k",350);
        //    p.PrintString("" + InvoiceDate); 
        //    p.PrintAlignment = PrintAlign.Left;
        //    p.NewLine = true;
        //    p.PrintString("Customer Name  :"+CustomerName);
        //    p.PrintAlignment = PrintAlign.Left;
        //    p.PrintString("Address        :"+Address1 );
        //    p.PrintAlignment = PrintAlign.Left;
        //    p.PrintString(":" + PhoneNo,200);
        //    p.PrintAlignment = PrintAlign.Left;
        //    p.PrintString("");
        //    p.NewLine = true;

        //    p.PrintAlignment = PrintAlign.Center;
        //    p.FontStyle = FontStyle.Bold; 
        //    p.FontSize = 10;
        //    p.PrintString("SALES DETAILS");
        //    p.FontSize = 16;
        //    p.FontStyle = FontStyle.Regular;
        //    p.NewLine = true;
        //    p.FontSize = 9;
        //    p.PrintAlignment = PrintAlign.Left;
        //    p.PrintLine('=',83); 
        ////    p.PrintAlignment = PrintAlign.Left;
        //    p.CurrentX = p.Left;
        //    p.NewLine = true  ;
        //    p.PrintString("ProdCode",70,false);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.NewLine = false;
        //    p.PrintAlignment = PrintAlign.Left;
        //    p.PrintString("ItemName",150);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.PrintString("Qty",30);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.PrintString("Gwt",40);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.PrintString("StWt",45);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.PrintString("NetWt",50);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.PrintString("StoneCash",75);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.PrintString("VA%", 50);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.PrintString("Amount",70);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.PrintString("ApprVA%AftDis",60,true);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.NewLine = true;
        //    p.PrintLine('-', 83);
        //    p.PrintAlignment = PrintAlign.Left;           
        //    using (DataTable dtdetails = DBConn.GetData(new System.Data.SqlClient.SqlCommand
        //    ("Select prodCode,ItemName,Qty,Gwt,StoneWt,NetWt,StoneCash,VAPerc,TotalAmount,VApercAftDis FROM SALE.SalesDotPrint WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0])
        //    {
        //        foreach (DataRow r in dtdetails.Rows)
        //        {
                   
        //            ProdCode = r["prodCode"].ToString();
        //            ItemName = r["ItemName"].ToString();
        //            Qty = Convert.ToInt32(r["Qty"].ToString());
        //            Gwt = Convert.ToSingle(r["Gwt"].ToString());
        //            StWt = Convert.ToSingle(r["StoneWt"].ToString());
        //            NetWt = Convert.ToSingle(r["NetWt"].ToString());
        //            StoneCash = Convert.ToSingle(r["StoneCash"].ToString());
        //            VaPerc = Convert.ToSingle(r["VAPerc"].ToString());
        //            Total = Convert.ToSingle(r["TotalAmount"].ToString());
        //            VaPercAftDis = Convert.ToSingle(r["VApercAftDis"].ToString());
        //            // Printer p = new Printer();
        //            p.NewLine = true;
        //            p.PrintAlignment = PrintAlign.Left;

        //            if (ProdCode != "")
        //            {
        //                p.PrintString("" + ProdCode, 70, false);
        //            }
        //            if (ProdCode == "")
        //            {
        //                p.PrintString("-",70);
        //            }
        //            p.NewLine = false;                   
        //            p.PrintString("" + ItemName,150, true);
        //            p.PrintString("" + Qty, 30, false);
        //            p.PrintAlignment = PrintAlign.Right;  
        //            p.PrintString("" + Gwt, 40, false);
        //            p.PrintAlignment = PrintAlign.Right;  
        //            p.PrintString("" + StWt, 45, false);
        //            p.PrintAlignment = PrintAlign.Right;  
        //            p.PrintString("" + NetWt, 50, false);
        //            p.PrintAlignment = PrintAlign.Right;  
        //            p.PrintString("" + StoneCash, 75, true);
        //            p.PrintAlignment = PrintAlign.Right;  
        //            p.PrintString("" + VaPerc, 50, false);
        //            p.PrintAlignment = PrintAlign.Right;  
        //            p.PrintString("" + Total, 70, false);
        //            p.PrintAlignment = PrintAlign.Right;  
        //            p.PrintString("" + VaPercAftDis, 60, true);
        //            p.PrintAlignment = PrintAlign.Right;  
        //        }

        //    }
        //    p.NewLine = true;
        //    p.PrintLine('-',83);
        //    p.PrintAlignment = PrintAlign.Left;
        //    p.NewLine = true;
        //    p.PrintString("Total",250);
        //    p.PrintAlignment = PrintAlign.Left;
        //    p.NewLine = false;
        //    SGwt = 0; SStWt = 0; SNetWt = 0; SStoneCash = 0; STotAmt = 0;
        //    using (DataTable dtdetails = DBConn.GetData(new System.Data.SqlClient.SqlCommand
        //   ("Select prodCode,ItemName,Qty,Gwt,StoneWt,NetWt,StoneCash,VAPerc,TotalAmount,VApercAftDis FROM SALE.SalesDotPrint WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0])
        //    {
        //        foreach (DataRow r in dtdetails.Rows)
        //        {
        //            SGwt += Convert.ToSingle(r["Gwt"].ToString());
        //            SStWt += Convert.ToSingle(r["StoneWt"].ToString());
        //            SNetWt += Convert.ToSingle(r["NetWt"].ToString());
        //            SStoneCash += Convert.ToSingle(r["StoneCash"].ToString());
        //            STotAmt += Convert.ToSingle(r["TotalAmount"].ToString());
        //        }
        //        p.PrintAlignment = PrintAlign.Right;  
        //        p.PrintString("" + SGwt, 40, true);
        //        p.PrintAlignment = PrintAlign.Right;  
        //        p.PrintString("" + SStWt, 45, false);
        //        p.PrintAlignment = PrintAlign.Right;  
        //        p.PrintString("" + SNetWt, 50, false);
        //        p.PrintAlignment = PrintAlign.Right;  
        //        p.PrintString("" + SStoneCash, 75, false);
        //        p.PrintAlignment = PrintAlign.Right;  
        //        p.PrintString("" + STotAmt,120, false);
        //        p.PrintAlignment = PrintAlign.Right;  
        //    }
        //    p.NewLine = true;
        //    p.PrintLine('-', 83);


        //    using (DataTable dtDia = DBConn.GetData(new System.Data.SqlClient.SqlCommand
        //   ("Select SUM(DiaNo) AS DiaNo,SUM(DiaCash) AS DiaCash,SUM(DiaWt) AS DiaWt FROM SALE.VSalesDetails WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0])
        //    {
        //        if (dtDia.Rows.Count > 0)
        //        {
        //            //DiaNo = Convert.ToInt32(dtDia.Rows[0]["DiaNo"] == null ? "" : dtDia.Rows[0]["DiaNo"].ToString());
        //            //DiaWt = Convert.ToSingle(dtDia.Rows[0]["DiaWt"] == null ? "" : dtDia.Rows[0]["DiaWt"].ToString());
        //            //DiaCash = Convert.ToSingle(dtDia.Rows[0]["DiaCash"] == null ? "" : dtDia.Rows[0]["DiaCash"].ToString());
        //        }
        //    }

        //    if (DBConn.GetData(new SqlCommand("Select * FROM SALE.VSalesDetails WHERE SalesId='" + TxtSaleId.Text + "' AND DiaNo>0")).Tables[0].Rows.Count != 0)
        //    {
        //        p.NewLine = true;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintString("DiaNo  :" + DiaNo, 100);
        //        p.NewLine = false;
        //        p.PrintString("DiaWt  :" + DiaWt, 150);
        //        p.PrintString("DiaCash  :" + DiaCash, 250);
        //        p.NewLine = true;
        //        p.PrintLine('-', 83); 
        //    }
        //    p.NewLine = true;
        //    p.PrintString("");

        //    //SALES EXCHANGE
        //    if (DBConn.GetData(new SqlCommand("Select * FROM SALE.VSalesExchange WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0].Rows.Count != 0)
        //    {
        //        p.NewLine = true;
        //        p.PrintString("");
        //        p.NewLine = true;
        //        p.PrintAlignment = PrintAlign.Center;
        //        p.FontStyle = FontStyle.Bold;
        //        p.FontSize = 10;
        //        p.PrintString("SALES EXCHANGE");
        //        p.NewLine = true;
        //        p.FontSize = 9;
        //        p.FontStyle = FontStyle.Regular;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintLine('=', 83);
        //        p.NewLine = true;
        //        p.PrintString("ItemName", 140, false);
        //        p.NewLine = false;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintString("Qty", 60);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("Gwt", 60);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("StWt", 65);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("NetWt", 60);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("StoneCash", 100);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("Amount", 100);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.NewLine = true;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintLine('-', 83);
        //        using (DataTable dtExchge = DBConn.GetData(new System.Data.SqlClient.SqlCommand
        //       ("SELECT ItemName,Qty,Gwt,StoneWt,StoneCash,NetWt,TotalAmount FROM SALE.VSalesExchange WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0])
        //        {
        //            foreach (DataRow r in dtExchge.Rows)
        //            {
        //                ExItemname = r["ItemName"].ToString();
        //                ExQty = Convert.ToInt32(r["Qty"].ToString());
        //                ExGwt = Convert.ToSingle(r["Gwt"].ToString());
        //                ExStWt = Convert.ToSingle(r["StoneWt"].ToString());
        //                ExStCash = Convert.ToSingle(r["StoneCash"].ToString());
        //                ExNetWt = Convert.ToSingle(r["NetWt"].ToString());
        //                ExAmount = Convert.ToSingle(r["TotalAmount"].ToString());
                       
                      
        //                p.NewLine = true;
        //                p.PrintAlignment = PrintAlign.Left;
        //                p.PrintString("" + ExItemname, 140, false);
        //                p.NewLine = false;
        //                p.PrintString("" + ExQty, 60, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + ExGwt, 60, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + ExStWt, 65, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + ExNetWt, 60, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + ExStCash, 100, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + ExAmount, 100, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //            }

        //        }
        //        p.NewLine = true;
        //        p.PrintLine('-', 83);
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.NewLine = true;
        //        p.PrintString("Total", 200);
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.NewLine = false;
        //        SExGwt = 0; SExStWt = 0; SExNetWt = 0; SExStCash = 0; SExAmount = 0;
        //        using (DataTable dtdetails = DBConn.GetData(new System.Data.SqlClient.SqlCommand
        //       ("SELECT Gwt,StoneWt,StoneCash,NetWt,TotalAmount FROM SALE.VSalesExchange WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0])
        //        {
        //            foreach (DataRow r in dtdetails.Rows)
        //            {
        //                SExGwt += Convert.ToSingle(r["Gwt"].ToString());
        //                SExStWt += Convert.ToSingle(r["StoneWt"].ToString());
        //                SExNetWt += Convert.ToSingle(r["NetWt"].ToString());
        //                SExStCash += Convert.ToSingle(r["StoneCash"].ToString());
        //                SExAmount += Convert.ToSingle(r["TotalAmount"].ToString());
        //            }
        //            p.PrintAlignment = PrintAlign.Right;
        //            p.PrintString("" + SExGwt, 60, true);
        //            p.PrintAlignment = PrintAlign.Right;
        //            p.PrintString("" + SExStWt, 65, false);
        //            p.PrintAlignment = PrintAlign.Right;
        //            p.PrintString("" + SExNetWt, 60, false);
        //            p.PrintAlignment = PrintAlign.Right;
        //            p.PrintString("" + SExStCash, 100, false);
        //            p.PrintAlignment = PrintAlign.Right;
        //            p.PrintString("" + SExAmount, 100, false);
        //            p.PrintAlignment = PrintAlign.Right;
        //        }
        //        p.NewLine = true;
        //        p.PrintLine('-', 83);
        //        using (DataTable dtDia = DBConn.GetData(new System.Data.SqlClient.SqlCommand
        //        ("Select SUM(DiaNo) AS DiaNo,SUM(DiaCash) AS DiaCash,SUM(DiaWt) AS DiaWt FROM SALE.VSalesExchange WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0])
        //        {
        //            DiaNo = Convert.ToInt32(dtDia.Rows[0]["DiaNo"] == null ? "" : dtDia.Rows[0]["DiaNo"].ToString());
        //            DiaWt = Convert.ToSingle(dtDia.Rows[0]["DiaWt"] == null ? "" : dtDia.Rows[0]["DiaWt"].ToString());
        //            DiaCash = Convert.ToSingle(dtDia.Rows[0]["DiaCash"] == null ? "" : dtDia.Rows[0]["DiaCash"].ToString());
        //        }

        //        if (DBConn.GetData(new SqlCommand("Select * FROM SALE.VSalesExchange WHERE SalesId='" + TxtSaleId.Text + "' AND DiaNo>0")).Tables[0].Rows.Count != 0)
        //        {
        //            p.NewLine = true;
        //            p.PrintAlignment = PrintAlign.Left;
        //            p.PrintString("DiaNo  :" + DiaNo, 100);
        //            p.NewLine = false;
        //            p.PrintString("DiaWt  :" + DiaWt, 150);
        //            p.PrintString("DiaCash  :" + DiaCash, 250);
        //            p.NewLine = true;
        //            p.PrintLine('-', 83);
        //        }
        //    }
        //    //SALES RETURN
        //    if (DBConn.GetData(new SqlCommand("SELECT * FROM SALE.VSalesReturnDetails WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0].Rows.Count != 0)
        //    {

        //        p.NewLine = true;
        //        p.PrintAlignment = PrintAlign.Center;
        //        p.FontSize = 10;
        //        p.FontStyle = FontStyle.Bold;
        //        p.PrintString("SALES RETURN");
        //        p.NewLine = true;
        //        p.FontSize = 9;
        //        p.FontStyle = FontStyle.Regular;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintLine('=', 83);
        //        p.NewLine = true;

        //        p.PrintString("ProdCode", 70, false);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.NewLine = false;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintString("ItemName", 130);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("Qty", 40);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("Gwt", 40);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("StWt", 45);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("NetWt", 70);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("StoneCash", 100);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("VA%", 50);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.PrintString("Amount", 100);
        //        p.PrintAlignment = PrintAlign.Right;
        //        p.NewLine = true;
        //        p.PrintAlignment = PrintAlign.Left;
        //        p.PrintLine('-', 83);
        //        // p.NewLine = true;




        //        using (DataTable dtReturn = DBConn.GetData(new System.Data.SqlClient.SqlCommand
        //        ("SELECT prodCode,ItemName,Qty,Gwt,StoneWt,NetWt,StoneCash,VAPerc,TotalAmount FROM SALE.VSalesReturnDetails WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0])
        //        {
        //            foreach (DataRow r in dtReturn.Rows)
        //            {
        //                ProdCode_Ret = r["prodCode"].ToString();
        //                ItemName_Ret = r["ItemName"].ToString();
        //                Qty_Ret = Convert.ToInt32(r["Qty"].ToString());
        //                Gwt_Ret = Convert.ToSingle(r["Gwt"].ToString());
        //                StWt_Ret = Convert.ToSingle(r["StoneWt"].ToString());
        //                NetWt_Ret = Convert.ToSingle(r["NetWt"].ToString());
        //                StCash_Ret = Convert.ToSingle(r["StoneCash"].ToString());
        //                VAPerc_Ret = Convert.ToSingle(r["VAPerc"].ToString());
        //                Amount_Ret = Convert.ToSingle(r["TotalAmount"].ToString());

        //                p.NewLine = true;
        //                p.PrintAlignment = PrintAlign.Left;
        //                p.PrintString("" + ProdCode_Ret, 70, false);                       
        //                p.NewLine = false;
        //                p.PrintString("" + ItemName_Ret, 130, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + Qty_Ret, 40, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + Gwt_Ret, 40, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + StWt_Ret, 45, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + NetWt_Ret, 70, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + StCash_Ret, 100, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + VAPerc_Ret, 50, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + Amount_Ret, 100, false);
        //                p.PrintAlignment = PrintAlign.Right;
                       
        //            }
        //            p.NewLine = true;
        //            p.PrintAlignment = PrintAlign.Left;
        //            p.PrintLine('-', 83);
        //            p.NewLine = true;
        //            p.PrintAlignment = PrintAlign.Left;
        //            p.PrintString("Total",240);
        //            p.NewLine = false;
        //            p.PrintAlignment = PrintAlign.Right;
        //            SGwt_Ret = 0; SStWt_Ret = 0; SNetWt_Ret = 0; SStCash_Ret = 0; SAmount_Ret = 0;
        //            using (DataTable dtdetails = DBConn.GetData(new System.Data.SqlClient.SqlCommand
        //           ("SELECT Gwt,StoneWt,NetWt,StoneCash,TotalAmount FROM SALE.VSalesReturnDetails WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0])
        //            {
        //                foreach (DataRow r in dtdetails.Rows)
        //                {
        //                    SGwt_Ret += Convert.ToSingle(r["Gwt"].ToString());
        //                    SStWt_Ret += Convert.ToSingle(r["StoneWt"].ToString());
        //                    SNetWt_Ret += Convert.ToSingle(r["NetWt"].ToString());
        //                    SStCash_Ret += Convert.ToSingle(r["StoneCash"].ToString());
        //                    SAmount_Ret += Convert.ToSingle(r["TotalAmount"].ToString());
        //                }
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + SGwt_Ret, 40, true);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + SStWt_Ret,45, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + SNetWt_Ret,70, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + SStCash_Ret,100, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.PrintString("" + SAmount_Ret,150, false);
        //                p.PrintAlignment = PrintAlign.Right;
        //                p.NewLine = true;
        //                p.PrintAlignment = PrintAlign.Left;
        //                p.PrintLine('-', 83); 
        //            }
                    
        //        }
        //        using (DataTable dtDia = DBConn.GetData(new System.Data.SqlClient.SqlCommand
        //        ("Select SUM(DiaNo) AS DiaNo,SUM(DiaCash) AS DiaCash,SUM(DiaWt) AS DiaWt FROM SALE.VSalesReturnDetails WHERE SalesId='" + TxtSaleId.Text + "'")).Tables[0])
        //        {
        //            DiaNo = Convert.ToInt32(dtDia.Rows[0]["DiaNo"] == null ? "" : dtDia.Rows[0]["DiaNo"].ToString());
        //            DiaWt = Convert.ToSingle(dtDia.Rows[0]["DiaWt"] == null ? "" : dtDia.Rows[0]["DiaWt"].ToString());
        //            DiaCash = Convert.ToSingle(dtDia.Rows[0]["DiaCash"] == null ? "" : dtDia.Rows[0]["DiaCash"].ToString());
        //        }

        //        if (DBConn.GetData(new SqlCommand("Select * FROM SALE.VSalesReturnDetails WHERE SalesId='" + TxtSaleId.Text + "' AND DiaNo>0")).Tables[0].Rows.Count != 0)
        //        {
        //            p.NewLine = true;
        //            p.PrintAlignment = PrintAlign.Left;
        //            p.PrintString("DiaNo  :" + DiaNo, 100);
        //            p.NewLine = false;
        //            p.PrintString("DiaWt  :" + DiaWt, 150);
        //            p.PrintString("DiaCash  :" + DiaCash, 250);
        //            p.NewLine = true;
        //            p.PrintLine('-', 83);
        //        }


        //    }    
        //    p.FontSize = 9;
 
        //    p.PrintAlignment = PrintAlign.Left;
        //    p.NewLine = true;
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.PrintString("Discount Amount :" + DisAmt,640);
          
        //    p.NewLine = true;
        //    p.PrintString("Amount  :" +GrandTotal,640);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.NewLine = true;
        //    p.PrintString("VAT   :" + TaxAmt,602);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.NewLine =true;
        //    p.PrintString("Net Amount  :" + netTotal,640);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.NewLine = true;
        //    p.PrintString("old Gold Bill No." + oldGoldNo,540);
        //    p.PrintAlignment = PrintAlign.Right;
        //    p.NewLine = false;
        //    p.PrintString(":" + oldGoldRpt,60);
        //    p.NewLine = true;
        //    p.FontSize = 16;
        //    p.FontName = "Times New Roman";
        //    p.PrintString("Amount Payable....:" + AmtPayable);
        //    p.FontSize = 14;
        //    p.FontStyle = FontStyle.Bold;
        //    p.NewLine = true;
        //    p.PrintAlignment = PrintAlign.Right;

          
        //  //  p.PrintLine('=', 40);
        //    p.Print();
            

        //}

        //private void PrintHeader(object sender,EventArgs  e)
        //{

        //    PrintDotMatrix();
        //   e.p.FontSize = 9;
        //   e.p.FontStyle = FontStyle.Regular;

        //   e.p.PrintAlignment = PrintAlign.Left;

        //   e.p.PrintString("TIN No. " + TinNo);
        //   e.p.NewLine = true;
        //   e.p.PrintAlignment = PrintAlign.Left;
        //   e.p.PrintString("CST No. " + CstNo);
        //   e.p.PrintAlignment = PrintAlign.Center;
        //   e.p.FontStyle = FontStyle.Bold;
        //   e.p.FontSize = 16;
        //   e.p.PrintString("" + Company);
        //    //e.p.FontSize = 13;
        //   e.p.PrintAlignment = PrintAlign.Center;
        //   e.p.FontStyle = FontStyle.Regular;
        //   e.p.FontSize = 10;

        //   e.p.NewLine = true;
        //   e.p.FontStyle = FontStyle.Regular;
        //   e.p.PrintString("" + Comp_Add1, 180);
        //   e.p.NewLine = false;
        //   e.p.PrintAlignment = PrintAlign.Left;

        //   e.p.PrintString("," + Comp_Add2, 115);
        //   e.p.PrintString("," + Comp_City, 85);
        //   e.p.PrintString("-" + Comp_pin, 60);
        //   e.p.NewLine = true;
        //   e.p.PrintAlignment = PrintAlign.Center;
        //   e.p.PrintString("PHONE :" + Comp_phone);
        //   e.p.NewLine = true;
        //   e.p.PrintString("BRANCH  :" + BranchName);
        //   e.p.NewLine = true;
        //   e.p.PrintAlignment = PrintAlign.Left;
        //   e.p.PrintLine('=', 83);


           

        //}
        //public void PrintFooter(object sender,FooterPrintEventArgs e)
        //{
        //    PrintDotMatrix();
        //    Printer p = new Printer();
        //    e.p.NewLine = true;
        //    e.p.PrintAlignment = PrintAlign.Left;
        //    e.p.FontStyle = FontStyle.Regular;  
        //    e.p.FontSize = 13;
        //    e.p.PrintLine('-',107);
        //    e.p.NewLine = true;
        //    e.p.PrintAlignment =PrintAlign.Left ;
        //    e.p.FontSize = 10;
        //    e.p.PrintString("" + Company); 

        //}
        //private void prnDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
            
        //}

        //private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        //{
    
        //}

        //private void grbButton1_Click(object sender, EventArgs e)
        //{
        //    //prnDocument.Print();
        //   SetInvoiceHead();
 
        //    Printer p = new Printer();
        //    p.StartPage += new JMS.Printer.NextPageHandler(PrintHeader);
        //   // for (int i = 0; i <= 100; i++)
        // //   {



        //    //    p.FontName = "Courier New";
        //    //   p.PrintAlignment = PrintAlign.Left;   
        //    //   p.PrintString(" This is Line Number :" + i.ToString());
        //   // }
        //    p.EndPage += new JMS.Printer.Footer(PrintFooter);
        //    p.Print();
        //    //p.EndPage += new JMS.Printer.Footer(PrintFooter);
        //    //p.Print();

        //}
        
    }
}  

      

     
        
       
    
         











