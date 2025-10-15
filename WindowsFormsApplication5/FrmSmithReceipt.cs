using Gramboo.Database;
using System;
using SAFA.Classes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Gramboo;
using System.Globalization;
using System.IO;
using System.Drawing.Imaging;
using SAFA.Reports.STOCK;

namespace SAFA.Forms.STK
{
    public partial class FrmSmithReceipt : Gramboo.Controls.GrbForm
    {
        private DataTable SuppItems;
        private int SaleCurrentRow = -1;
        private bool flag; bool Message = true;
        private static FrmSmithReceipt instance;
        public SAFA.Forms.PROD.FrmBatchInbox BatchInbox;
        public SAFA.Forms.STK.FrmTransferInbox TransferInbox;
        public SAFA.Forms.STOCK.SmithDetaisDateWiseReportForm1 SmithRpt;
        public static string purchaseytype;
        public static string transtype;
        public SAFA.Forms.ACC.FrmSmithBalanceConv STC;
        public string Branchid, BranchName; long Supledgerid;
        public string SuppBranch, CustBaranchid, importfile, SuppMCmode = "", RoundTo = "F3"; bool CopperAdded = false;
        public SAFA.Forms.COM.CustomerSearch SD; bool msgval; Double PenActwt = 0;
        bool ChkSt_DiaCash = false, ChkMC = false, ChkMetalCash = false, ChkTotalAmount = false, CalcMcRate = false, GenerateEway = false;
        ListView mylist = new ListView();

        public static FrmSmithReceipt Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FrmSmithReceipt();
                }
                else if (instance.IsDisposed)
                {
                    instance = new FrmSmithReceipt();
                }
                else if (instance.PurchaseType != purchaseytype && instance.TransType != transtype)
                {
                    instance = new FrmSmithReceipt();
                }
                return instance;
            }
        }
        [DefaultValue("")]
        public string PurchaseType
        {

            get
            {

                return purchaseytype;
            }

            set
            {
                purchaseytype = value;

                if (value == "Customer Jobwork")
                {
                    cmb_Purchasetype.Text = "JobWork";
                    cmbpartytype.SelectedValue = 3;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Supplier Jobwork")
                {
                    cmb_Purchasetype.Text = "JobWork";
                    cmbpartytype.SelectedValue = 1;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Branch Jobwork")
                {
                    cmb_Purchasetype.Text = "JobWork";
                    cmbpartytype.SelectedValue = 0;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Department Jobwork")
                {
                    cmb_Purchasetype.Text = "JobWork";
                    cmbpartytype.SelectedValue = 4;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "RJCU Jobwork")
                {
                    cmb_Purchasetype.Text = "JobWork";
                    cmbpartytype.SelectedValue = 3;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "RJSU Jobwork")
                {
                    cmb_Purchasetype.Text = "JobWork";
                    cmbpartytype.SelectedValue = 1;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Customer Exhibition")
                {
                    cmb_Purchasetype.Text = "Exhibition";
                    cmbpartytype.SelectedValue = 3;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Supplier Exhibition")
                {
                    cmb_Purchasetype.Text = "Exhibition";
                    cmbpartytype.SelectedValue = 1;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Branch Exhibition")
                {
                    cmb_Purchasetype.Text = "Exhibition";
                    cmbpartytype.SelectedValue = 0;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Department Exhibition")
                {
                    cmb_Purchasetype.Text = "Exhibition";
                    cmbpartytype.SelectedValue = 4;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Branch Transfer")
                {
                    cmb_Purchasetype.Text = "BranchTransfer";
                    cmbpartytype.SelectedValue = 0;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Customer Repair")
                {
                    cmb_Purchasetype.Text = "Repair";
                    cmbpartytype.SelectedValue = 3;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Supplier Repair")
                {
                    cmb_Purchasetype.Text = "Repair";
                    cmbpartytype.SelectedValue = 1;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Branch Repair")
                {
                    cmb_Purchasetype.Text = "Repair";
                    cmbpartytype.SelectedValue = 0;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Department Repair")
                {
                    cmb_Purchasetype.Text = "Repair";
                    cmbpartytype.SelectedValue = 4;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Customer Hallmarking")
                {
                    cmb_Purchasetype.Text = "Hallmarking";
                    cmbpartytype.SelectedValue = 3;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Supplier Hallmarking")
                {
                    cmb_Purchasetype.Text = "Hallmarking";
                    cmbpartytype.SelectedValue = 1;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Branch Hallmarking")
                {
                    cmb_Purchasetype.Text = "Hallmarking";
                    cmbpartytype.SelectedValue = 0;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Department Hallmarking")
                {
                    cmb_Purchasetype.Text = "Hallmarking";
                    cmbpartytype.SelectedValue = 4;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Customer Certification")
                {
                    cmb_Purchasetype.Text = "Certification";
                    cmbpartytype.SelectedValue = 3;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Supplier Certification")
                {
                    cmb_Purchasetype.Text = "Certification";
                    cmbpartytype.SelectedValue = 1;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Branch Certification")
                {
                    cmb_Purchasetype.Text = "Certification";
                    cmbpartytype.SelectedValue = 0;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Department Certification")
                {
                    cmb_Purchasetype.Text = "Certification";
                    cmbpartytype.SelectedValue = 4;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Inside Jobwork")
                {
                    cmb_Purchasetype.Text = "InsideJobWork";
                    cmbpartytype.SelectedValue = 1;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Customer Other")
                {
                    cmb_Purchasetype.Text = "Other";
                    cmbpartytype.SelectedValue = 3;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Supplier Other")
                {
                    cmb_Purchasetype.Text = "Other";
                    cmbpartytype.SelectedValue = 1;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Branch Refinery")
                {
                    cmb_Purchasetype.Text = "Other";
                    cmbpartytype.SelectedValue = 0;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Department Other")
                {
                    cmb_Purchasetype.Text = "Other";
                    cmbpartytype.SelectedValue = 4;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Customer Refinery")
                {
                    cmb_Purchasetype.Text = "Refinery";
                    cmbpartytype.SelectedValue = 3;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Supplier Refinery")
                {
                    cmb_Purchasetype.Text = "Refinery";
                    cmbpartytype.SelectedValue = 1;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Branch Refinery")
                {
                    cmb_Purchasetype.Text = "Refinery";
                    cmbpartytype.SelectedValue = 0;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Department Refinery")
                {
                    cmb_Purchasetype.Text = "Refinery";
                    cmbpartytype.SelectedValue = 4;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Department Transfer")
                {
                    cmb_Purchasetype.Size = new Size(120, 21);
                    cmb_Purchasetype.Text = "DepartmentTransfer";
                    cmbpartytype.SelectedValue = 4;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Old Gold")
                {
                    cmb_Purchasetype.Text = "OldGold";
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Customer Melting")
                {
                    cmb_Purchasetype.Text = "Melting";
                    cmbpartytype.SelectedValue = 3;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Supplier Melting")
                {
                    cmb_Purchasetype.Text = "Melting";
                    cmbpartytype.SelectedValue = 1;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Branch Melting")
                {
                    cmb_Purchasetype.Text = "Melting";
                    cmbpartytype.SelectedValue = 0;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Melted OG")
                {
                    cmb_Purchasetype.Text = "Melted OG";
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Test")
                {
                    cmb_Purchasetype.Text = "Test";
                    cmb_Purchasetype.Enabled = false;
                    CmbBatchNo.CheckDuplicates = false;
                }
                else if (value == "Result")
                {
                    cmb_Purchasetype.Text = "Result";
                    cmb_Purchasetype.Enabled = false;
                    CmbBatchNo.CheckDuplicates = false;
                }
                else if (value == "Customer Approval")
                {
                    cmb_Purchasetype.Text = "Approval";
                    cmbpartytype.SelectedValue = 3;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Supplier Approval")
                {
                    cmb_Purchasetype.Text = "Approval";
                    cmbpartytype.SelectedValue = 1;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Branch Approval")
                {
                    cmb_Purchasetype.Text = "Approval";
                    cmbpartytype.SelectedValue = 0;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else if (value == "Department Approval")
                {
                    cmb_Purchasetype.Text = "Approval";
                    cmbpartytype.SelectedValue = 4;
                    cmbpartytype.Enabled = false;
                    cmb_Purchasetype.Enabled = false;
                }
                else
                {
                    cmb_Purchasetype.Text = "Purchase";
                    cmb_Purchasetype.Enabled = false;
                }
            }
        }
        [DefaultValue("")]
        public string TransType
        {
            get
            {
                return transtype;
            }
            set
            {
                transtype = value;
                if (value == "Issue" || value == "0")
                {
                    grb.DefaultRadioButton = grbissue;
                    grbissue.Checked = true;
                }
                else
                {
                    grb.DefaultRadioButton = grbreceipt;
                    grbreceipt.Checked = true;
                }

                grb.Enabled = false;

                if (PurchaseType == null) { PurchaseType = ""; }

                if (PurchaseType.StartsWith("Customer"))
                {
                    this.Text = (value == "0" ? "Issue" : "Receipt") + (value == "0" ? " After " : " For ") + PurchaseType.Substring(9);
                }
                else if (PurchaseType.StartsWith("Supplier"))
                {
                    this.Text = (value == "0" ? "Issue" : "Receipt") + (value == "0" ? " For " : " After ") + PurchaseType.Substring(9);
                }
                else if (purchaseytype.StartsWith("RJSU"))
                {
                    this.Text = "Return" + " To " + PurchaseType.Substring(5) + "er";
                }
                else if (PurchaseType.StartsWith("RJCU"))
                {
                    this.Text = "Return" + " From " + PurchaseType.Substring(5) + "er";
                }
                else
                {
                    this.Text = PurchaseType + " " + (value == "0" ? "Issue" : "Receipt");
                }

                if (!IsEditMode)
                {
                    if (this.Text.Contains("For")) { Rb_For.Checked = true; }
                    else if (this.Text.Contains("After")) { Rb_Afrer.Checked = true; }
                    else if (this.Text.Contains("Return")) { Rb_Return.Checked = true; }
                    txt_purpose.Text = (CultureInfo.CurrentCulture.TextInfo.ToTitleCase(this.Text.ToLower()));
                }
                else if (IsEditMode && (cmb_Purchasetype.SelectedIndex.ToString() != "4" & cmb_Purchasetype.SelectedIndex.ToString() != "16" & cmb_Purchasetype.SelectedIndex.ToString() != "7"))
                {
                    if (GrpEntryMod.Value != "R")
                    {

                        if (PurchaseType.Length > 9)
                        {
                            this.Text = (value == "0" ? "Issue" : "Receipt") + (GrpEntryMod.Value == "F" ? " For " : " After ") + PurchaseType.Substring(9);
                        }
                        else
                        {
                            this.Text = (value == "0" ? "Issue" : "Receipt") + (GrpEntryMod.Value == "F" ? " For " : " After ") + PurchaseType;

                        }
                    }
                    else if (GrpEntryMod.Value == "R")
                    {
                        this.Text = "Return" + (value == "0" ? " To " : " From ") + PurchaseType.Substring(9) + "er";
                    }
                }
                if (IsEditMode && GrpEntryMod.Value == "-")
                {
                    if (this.Text.Contains("For")) { Rb_For.Checked = true; }
                    else if (this.Text.Contains("After")) { Rb_Afrer.Checked = true; }
                    else if (this.Text.Contains("Return")) { Rb_Return.Checked = true; }
                }
                else if (IsEditMode && GrpEntryMod.Value == "")
                {
                    if (this.Text.Contains("For")) { Rb_For.Checked = true; }
                    else if (this.Text.Contains("After")) { Rb_Afrer.Checked = true; }
                    else if (this.Text.Contains("Return")) { Rb_Return.Checked = true; }
                }
            }
        }

        public bool AllowInit { get; set; }

        public FrmSmithReceipt()
        {
            InitializeComponent();
            //groupBox1.Size = new Size(1130, 235); groupBox1.Location = new Point(5, 220);

            AllowInit = true;

            DBConn = SAFA.Classes.Common.DatabaseSettings(DBConn);
            mylist.Location = new Point(1060, 10);
            mylist.Size = new Size(250, 140);
            mylist.View = View.Details;
            mylist.FullRowSelect = true;
            mylist.Columns.Add("ItemName", 70, HorizontalAlignment.Left);
            mylist.Columns.Add("Location", 100, HorizontalAlignment.Left);
            mylist.Columns.Add("Netwt", 65, HorizontalAlignment.Left);

            if (SoftwareSettings.CompName == "MANJALI") { this.Controls.Add(mylist); }

        }

        public override bool GenerateID(Gramboo.Database.Table table_name)
        {
            try
            {
                table_name.IdTextBox.Text = SAFA.Classes.Common.GetNextID(table_name, table_name.IdTextBox.DataField, DBConn, Convert.ToInt32(base.txtcompId.Text), Convert.ToInt32(base.txtBranchID.Text)).ToString();


                if (!IsEditMode)
                    TxtVoucherNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtp_dt.Value,
                DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        void FormText()
        {
            String PurchaseType = "", Type = "";

            using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand
          ("Select  (case t1.TransType when 0 then 'Issue' else 'Receipt' end) as TransType,"
           + " t3.Type, t2.PurchaseType,isnull(t1.ResultOnly,'0') as ResultOnly,VchNo,ISNULL(t1.EntryMod,'') as EntryMod"
           + " from STK.StockTransfermaster as t1 left outer join STK.PurchaseTypeMaster as t2"
           + " on t1.PurchaseType = t2.PurchaseTypeId left outer join STK.VSTPartyType as t3"
           + " on t1.PartyTypeId = t3.Id where entryid=" + txtEntryNo.Text)).Tables[0])
            {
                if (dt1.Rows.Count > 0)
                {
                    transtype = dt1.Rows[0]["TransType"].ToString();
                    String EntryMod = dt1.Rows[0]["EntryMod"].ToString();
                    if (EntryMod != "") { GrpEntryMod.Visible = false; if (EntryMod == "F") { Rb_For.Checked = true; } else if (EntryMod == "A") { Rb_Afrer.Checked = true; } else if (EntryMod == "R") { Rb_Return.Checked = true; } }
                    else { GrpEntryMod.Visible = true; }
                    Type = dt1.Rows[0]["Type"].ToString();
                    PurchaseType = dt1.Rows[0]["PurchaseType"].ToString();
                    ChkVchNo.Checked = Convert.ToBoolean(dt1.Rows[0]["ResultOnly"].ToString());
                    TxtVoucherNo.Text = dt1.Rows[0]["VchNo"].ToString();

                    if (PurchaseType == "Inside JobWork") { PurchaseType = "Inside Jobwork"; }
                    else if (PurchaseType == "BranchTransfer") { PurchaseType = "Branch Transfer"; }
                    else if (PurchaseType == "OldGold") { PurchaseType = "Old Gold"; }
                    //else if (PurchaseType == "Melting") { PurchaseType = "Melting"; }
                    else if (PurchaseType == "Melted OG") { PurchaseType = "Melted OG"; }
                    else if (PurchaseType == "Test") { PurchaseType = "Test"; }
                    else if (PurchaseType == "Result") { PurchaseType = "Result"; }
                    else if (PurchaseType == "Refineryy") { PurchaseType = "Refinery"; }
                    else if (PurchaseType == "DepartmentTransfer") { PurchaseType = "Department Transfer"; }
                    else { PurchaseType = Type + " " + PurchaseType; }
                }
            }
            string a = "";
            if (PurchaseType.Contains("JobWork")) { a = PurchaseType.Replace("JobWork", "Jobwork"); PurchaseType = a; }
            else if (PurchaseType.Contains("HallMarking")) { a = PurchaseType.Replace("HallMarking", "Hallmarking"); PurchaseType = a; }

            if (purchaseytype != PurchaseType) { purchaseytype = PurchaseType; }
        }

        public override bool FillData(Dictionary<string, object> PrimaryValues)
        {
            if (base.FillData(PrimaryValues))
            {
                //flag = true;
                Message = false;
                BtnDocAdd.Visible = true;
                DocButtonChange();
                cmb_Purchasetype.Enabled = false;
                cmbpartytype.Enabled = false;
                FormText();

                if (transtype == "Issue" || transtype == "0")
                {
                    TransType = "0";
                    grb.DefaultRadioButton = grbissue;
                    grb.Value = transtype;
                    grbissue.Checked = true;

                }
                else
                {
                    TransType = "1";
                    grb.DefaultRadioButton = grbreceipt;
                    grb.Value = transtype;
                    grbreceipt.Checked = true;

                }
                BatchWTReadOnly();
                SupplierBranchid();

                string MC_Mode = "";
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                    ("SELECT ISNULL(MC_Mode,'') as MC_Mode  FROM STK.StockTransferMaster where EntryId=" + txtEntryNo.Text + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        MC_Mode = dt.Rows[0]["MC_Mode"].ToString();
                        Grb_MC_Mode.Value = dt.Rows[0]["MC_Mode"].ToString();
                    }
                }

                if (MC_Mode == "")
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                        ("SELECT ISNULL(SuppMCmode,'') as SuppMCmode FROM PUR.SupplierMaster WHERE SuppId='" + cmbpartyname.SelectedValue.ToString() + "'")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            SuppMCmode = dt.Rows[0]["SuppMCmode"].ToString();
                            Grb_MC_Mode.Value = dt.Rows[0]["SuppMCmode"].ToString();
                            if (SuppMCmode == "C") { grb_cash.Checked = true; } else { grb_wst.Checked = true; }
                        }
                    }
                }

                foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                {
                    if (r.Cells["ProdCodeId"].Value.ToString().Trim().Length >= 2)
                    {
                        enable_disable(dgv_itemDetails, r.Index, true);
                        if (SAFA.Classes.Common.AllowBarcodeEdit(DBConn, Convert.ToInt16(txtBranchID.Text)))
                        {
                            r.Cells["Gwt"].ReadOnly = false;
                           r.Cells["Diawt"].ReadOnly = false;
                            //dgv_itemDetails.CurrentRow.Cells["DiaCash"].ReadOnly = false;
                           r.Cells["StWt"].ReadOnly = false;
                            //dgv_itemDetails.CurrentRow.Cells["StCash"].ReadOnly = false;
                          r.Cells["MetalCash"].ReadOnly = false;
                            //dgv_itemDetails.CurrentRow.Cells["MetalRate"].ReadOnly = false;
                        }
                        else

                        {
                           r.Cells["Nos"].ReadOnly = true;
                            //dgv_itemDetails.CurrentRow.Cells["ItemName"].ReadOnly = true;
                            r.Cells["Gwt"].ReadOnly = true;
                           r.Cells["Diawt"].ReadOnly = true;
                            //dgv_itemDetails.CurrentRow.Cells["DiaCash"].ReadOnly = true;
                           r.Cells["StWt"].ReadOnly = true;
                            //dgv_itemDetails.CurrentRow.Cells["StCash"].ReadOnly = true;
                           r.Cells["MetalCash"].ReadOnly = true;
                            //dgv_itemDetails.CurrentRow.Cells["MetalRate"].ReadOnly = true;
                        }

                    }
                }
                return true;
            }
            else
            {
                Message = true;
                return false;
            }
        }
        public override bool InitializeTables()
        {
            Table t = new Table(SAFA.Classes.Common.DbName, "STK", "StockTransferMaster");
            t.PrimaryKeys.Add("EntryId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.NotUpdatables.Add("TxType");
            t.NotUpdatables.Add("VoucherTypeId");
            t.NotUpdatables.Add("VchNo");
            t.NotUpdatables.Add("TransType");

            t.IdTextBox = txtEntryNo;
            Table t1 = new Table(SAFA.Classes.Common.DbName, "STK", "StockTransferDetails", true);
            t1.PrimaryKeys.Add("TransId");
            t1.FillView = new Table(SAFA.Classes.Common.DbName, "STK", "VGoldStockTransferDetails");
            t1.DatagridView = dgv_itemDetails;
            t1.IsDatagridView = true;
            t1.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t1);

            Table t2 = new Table(SAFA.Classes.Common.DbName, "STK", "StockTransferTaxDetails", true);
            t1.PrimaryKeys.Add("TaxTrasnsId");
            t2.FillView = new Table(SAFA.Classes.Common.DbName, "STK", "VStockTransferTaxDetails", true);
            t2.DatagridView = dgv_TaxDetails;
            t2.IsDatagridView = true;
            t2.IdTextBox = TxtTaxTrasnsId;
            t.ChildTables.Add(t2);

            Table t7 = new Table(SAFA.Classes.Common.DbName, "STK", "SmithOtherCharges", true);
            t7.PrimaryKeys.Add("OTchId");
            t7.FillView = new Table(SAFA.Classes.Common.DbName, "STK", "VSmithOtherCharges", true);
            t7.DatagridView = dgv_otherChg;
            t7.IsDatagridView = true;
            t7.IdTextBox = TxtOtchId;
            t.ChildTables.Add(t7);

            Table t8 = new Table(SAFA.Classes.Common.DbName, "STK", "StockTransferPending", true);
            t8.PrimaryKeys.Add("PendingId");
            t8.FillView = new Table(SAFA.Classes.Common.DbName, "STK", "VStockTransferPending", true);
            t8.DatagridView = dgvPending;
            t8.IsDatagridView = true;
            t8.IdTextBox = txtPendingId;
            t.ChildTables.Add(t8);

            Table t9 = new Table(SAFA.Classes.Common.DbName, "STK", "ApprovalSelectionJW", true);
            t9.PrimaryKeys.Add("SelectionId");
            t9.FillView = new Table(SAFA.Classes.Common.DbName, "STK", "VApprovalSelectionJW", true);
            t9.DatagridView = dgvPendingSTK;
            t9.IsDatagridView = true;
            t9.IdTextBox = txtSelectionId;
            t.ChildTables.Add(t9);

            this.TableName = t;
            return true;
        }
        public override void RefreshData()
        {
            base.RefreshData();
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            dtp_dt.Focus();
            //if (chkbarcode.Checked == true)
            //{
            //    Gramboo.General.Setupcombo(Cmb_ProdCode, "STK.ProdCodeMaster", "prodCode", "ProdCodeId", "IsActive='True'");
            //}
            if (SoftwareSettings.CompName == "ORIEL")

            {
                Gramboo.General.Setupcombo(cmb_Custname, "CRM.CustomerMaster", "CustName", "CustId", "IsActive='True'");
            }
            Gramboo.General.Setupcombo(Cmb_ItemName, "ITM.ItemMaster", "[ItemName]", "ItemId", "[IsActive]='True' and BillGroup!='C' ");
            Gramboo.General.Setupcombo(cmb_purity, "ITM.PurityMaster", "PurityName", "PurityId", "IsActive='True'");
            Gramboo.General.Setupcombo(Cmb_Chargename, "PUR.MiscChargeMaster", "ChargeName", "ChargeId", "IsActive='True'  AND  Company_Id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
            using (DataTable dt = DBConn.GetData(new SqlCommand(" SELECT 1     FROM INFORMATION_SCHEMA.COLUMNS    WHERE TABLE_SCHEMA = 'syst' AND TABLE_NAME = 'settings'  and COLUMN_NAME='EnableEway'")).Tables[0])
            {
                if (dt.Rows.Count ==0)
                {
                    GenerateEway = false;
                }
                else
                {
                     
                    GenerateEway = true;
                }
            }
            
            if (GenerateEway)
            {
                Gramboo.General.Setupcombo(txt_tansmode, "STK.TransportationMode", "Description", "Code");
                Gramboo.General.Setupcombo(cmb_DocumentType, "STK.DocumentType", "Description", "Code");
                Gramboo.General.Setupcombo(cmb_SubsupplyType, "STK.SubSupplyType", "Description", "Code");
                Gramboo.General.Setupcombo(cmb_SupplyType, "STK.SupplyType", "Description", "Code");
                Gramboo.General.Setupcombo(cmb_TransactionType, "STK.TransactionType", "Description", "Code");
                Gramboo.General.Setupcombo(cmb_TransporterName, "PUR.VSupplierMaster", "[Supplier Name]", "SuppId", "[Supplier Type]='Transporter'");
            }
            Gramboo.General.Setupcombo(txt_vechileno, "STK.StockTransferMaster", "VehicleNo");
            //Gramboo.General.Setupcombo(cmb_Purchasetype, "PUR.PurchaseTypeMaster", "PurchaseTypeName", "PurchaseTypeId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmbpartytype, "STK.VSTPartyType", "Type", "Id", "IsActive='True'");
            if (SoftwareSettings.IsShowRoom == false) { Gramboo.General.Setupcombo(Cmb_StkType, "STK.VStockgroup", "StockName", "StockType", "STKActive='True'"); }
            else { Gramboo.General.Setupcombo(Cmb_StkType, "STK.VStockgroup", "StockName", "StockType", "IsActive='True'"); }
            //cmb_purity.SelectedValue = 6;
            Gramboo.General.Setupcombo(CmbSm, "Emp.VEmployeeSelect", "EmpName", "EmpId", "IsActive='True' and Company_id=" + txtcompId.Text + " and Branch_Id=" + txtBranchID.Text);
            Gramboo.General.Setupcombo(cmbbranch, "SYST.BranchMaster", "BranchName", "BranchId", "IsActive='True'");
            if (dgv_itemDetails.NewRowIndex > -1)
            {
                dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[dgv_itemDetails.NewRowIndex].Cells["ProdCode"];
                dgv_itemDetails.BeginEdit(true);
            }
            cmb_purity.Visible = false;
            txtDeptName.Text = Convert.ToString(((frmMain)this.ParentForm).dept);

            Cmb_ItemTxTypeId.SelectedValueChanged -= Cmb_PurTypeId_SelectedValueChanged;
            Gramboo.General.Setupcombo(Cmb_ItemTxTypeId, "PUR.PurchaseTaxTypeMaster", "PurTypeName", "PurTypeId", "IsActive='True'  AND  Company_Id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
            Cmb_ItemTxTypeId.DropDownWidth = 250;
            Cmb_ItemTxTypeId.SelectedValueChanged += Cmb_PurTypeId_SelectedValueChanged;

            using (DataTable dt = DBConn.GetData(new SqlCommand("select BarRate from GEN.MetalRate WHERE EntryDate<='" + dtp_dt.Value + "'  and Branch_id=" + txtBranchID.Text + "Order BY EntryDate Desc,EntryTime DESC")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    txtRate.Text = dt.Rows[0][0].ToString();
                }
                else
                {
                    txtRate.Text = "0.00";
                }
            }

            if (cmbpartyname.SelectedIndex == 3)
            {
                txt_touch.Text = 100.ToString();
            }
            OthChGrid(); ItemTaxGrid();
            if (SoftwareSettings.CompName == "GEMERALD")
            {
                CmbSm.AcceptBlankValue = false;
            }
        }
        public override void Init()
        {
            ChkNoBarcodeStock.Visible = false;

            if (SoftwareSettings.CompName == "TTD")
            {

                txtOne_Two.Visible = true;
            }
            else if (SoftwareSettings.CompName == "JISHAGOLD")
            {
                ChkNoBarcodeStock.Visible = true;

            }
            if (SoftwareSettings.CompName == "ORIEL")

            {
                
                cmb_Custname.Visible = true;
                label16.Visible = true;
            }
            else
            {

                cmb_Custname.Visible = false;
                label16.Visible = false;
            }



            base.Init();
            GenerateEway = false;
            Message = true; GrpEntryMod.Visible = false; GrpEntryMod.AcceptBlankValue = true; CopperAdded = false;
            chkbarcode.CheckedChanged -= chkbarcode_CheckedChanged;
            if (transtype == "Issue" || transtype == "0")
            {
                TransType = "0";
                grb.DefaultRadioButton = grbissue;
                grb.Value = transtype;
                grbissue.Checked = true;

            }
            else
            {
                TransType = "1";
                grb.DefaultRadioButton = grbreceipt;
                grb.Value = transtype;
                grbreceipt.Checked = true;

            }

            Cmb_StkType.SelectedIndex = 1;  

            PurchaseTypes();

            if (cmb_Purchasetype.Text != "")
            {
                SmithDetails();
                procd();
                vchno(false);
            }
            dtp_dt.Focus();
            Cmb_ProdCode.Visible = false;
            cmb_purity.SelectedValue = 6;
            txtTotalChargeAmount.Text = "0";
            txtOne_Two.Visible = false;
            chkbarcode.Checked = false;
            if (SoftwareSettings.CompName == "PEENIKA" || SoftwareSettings.CompName == "VATAKARA YARA")
            {
                grbButton3.Visible = true;
                ChkDellivery.Visible = true;
                ChkDellivery.Checked = true;
                txtDelliveryChallanNo.Visible = true;


            }
            else
            {
                grbButton3.Visible = false;
                ChkDellivery.Visible = false;
                ChkDellivery.Checked = false;
                txtDelliveryChallanNo.Visible = false;
            }
            if (!SoftwareSettings.TxType && SoftwareSettings.CompName != "TTD" ) { txtOne_Two.Text = "1"; taxone(); }
            TxtIsactive.Text = "1";
            SetColumns(dgv_itemDetails);
            if (SoftwareSettings.HasJobNo == false)
            {
                dgv_itemDetails.HiddenDataFields.AddRange(new List<string> { "JobOrderId", "JobNo" });
                ChkJobOrderNo.Visible = false;
            }
            DisplayProdcodeColumn();
            AdjustColumnWidths();
            if (this.TableName != null)
                GenerateID(this.TableName);
            SaleCurrentRow = -1;

            this.addNewMasterOtherCharge.MasterForm = SAFA.Forms.PUR.MiscelleniousChargeMaster.Instance;
            this.addNewMasterOtherCharge.ParentForm = SAFA.Forms.STK.FrmSmithReceipt.instance;
            this.addNewMasterOtherCharge.ParentControl = Cmb_Chargename;
            GrpPurOtherCharges.Visible = false;
            chkbarcode.CheckedChanged += chkbarcode_CheckedChanged;
            StkTyeCustomer();
            if ((cmbpartytype.Text == "Supplier" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true) || (cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true))
            {
                linkLabel2.Visible = true;
            }
            else
            {
                linkLabel2.Visible = false;
            }
            if ((cmbpartytype.Text == "Supplier" && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true)) { btn_copper.Visible = true; }
            else if ((cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true)) { btn_copper.Visible = true; } else { btn_copper.Visible = false; }
            BtnDocAdd.Visible = false;
            txtPurity.Text = "99.5";
            linkLabel4.Visible = false;

            if (SoftwareSettings.CompName == "MANJALI")
            {
                mylist.Visible = false;
                if ((cmbpartytype.Text == "Supplier" && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true)) { grb_wst.Checked = true; mylist.BringToFront(); LoadStock(); }
                else if ((cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true)) { grb_cash.Checked = true; linkLabel4.Visible = true; linkLabel4.Location = new Point(535, 119); SelectionPendGrid(); }
                else if ((cmbpartytype.Text == "Supplier" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true)) { grb_cash.Checked = true; }
                else if ((cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true)) { grb_wst.Checked = true; }
            }
        }

        void BtnSmithRpt()
        {
            if (cmbpartyname.SelectedValue == null)
                return;
            if (cmbpartytype.SelectedValue.ToString() == "1" && cmbpartyname.SelectedValue.ToString() != "0")
            {
                BtnSmthiDetailRpt.Visible = true;
                ToolTip t = new ToolTip();
                t.Active = true; t.SetToolTip(BtnSmthiDetailRpt, "Smith Detail Datewise Report");
            }
            else { BtnSmthiDetailRpt.Visible = false; }
        }
        public void AdjustColumnWidths()
        {
            dgv_itemDetails.RowHeadersVisible = false;
            dgv_itemDetails.Columns[0].Width = 40;
            dgv_itemDetails.Columns["BatchNo"].Width = CmbBatchNo.Width;
            dgv_itemDetails.Columns["Item Name"].Width = Cmb_ItemName.Width;
            dgv_itemDetails.Columns["Purity"].Width = cmb_purity.Width;
            dgv_itemDetails.Columns["Nos"].Width = 50;
            dgv_itemDetails.Columns["Gwt"].Width = 65;
            dgv_itemDetails.Columns["StWt"].Width = 65;
            dgv_itemDetails.Columns["Diawt"].Width = 65;
            dgv_itemDetails.Columns["NetWt"].Width = 65;
            dgv_itemDetails.Columns["DiaCash"].Width = 65;
            dgv_itemDetails.Columns["Touch"].Width = 65;
            dgv_itemDetails.Columns["MC"].Width = 95;
            dgv_itemDetails.Columns["Wst"].Width = 65;
            dgv_itemDetails.Columns["PureWt"].Width = 65;
            dgv_itemDetails.Columns["Item Name"].ReadOnly = true;
            dgv_itemDetails.Columns["Purity"].ReadOnly = true;
            dgv_itemDetails.Columns["BranchName"].ReadOnly = true;
            dgv_itemDetails.Columns["PureWt"].ReadOnly = true;
            dgv_itemDetails.Columns["NetWt"].ReadOnly = true;
            dgv_itemDetails.Columns["IssueWt"].ReadOnly = true;
            dgv_itemDetails.Columns["ActualWt"].ReadOnly = true;
            dgv_itemDetails.Columns["WastageWt"].ReadOnly = true;
            dgv_itemDetails.Columns["StCash"].HeaderText = "StoneAmt";
            dgv_itemDetails.Columns["DiaCash"].HeaderText = "DmdAmt";

            if (SoftwareSettings.CompName == "MANJALI" && (cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true))
            {
                dgv_itemDetails.Columns["ActualWt"].ReadOnly = false;
            }
        }
        void joborderid()
        {
            if (txtcompId.Text == "" && txtBranchID.Text == "")
                return;

            string str;
            SqlCommand cmd = new SqlCommand();
            if (cmbpartyname.SelectedValue != null)
            {
                str = "Select JobOrderID,JobNo from STK.FunSmithIssuePending(" + cmbpartyname.SelectedValue + "," + txtcompId.Text + "," + txtBranchID.Text + ") ";
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



        void BatchId()
        {
            if (Cmb_StkType.SelectedValue == null)
                return;
            string str;
            SqlCommand cmd = new SqlCommand();
            if (TxtTranscId.Text == "")
            {
                str = "Select BatchId,BatchNo  from PROD.PendingBatchIdList(" + Gramboo.GeneralConfig.BranchId + "," + Gramboo.GeneralConfig.CompanyID + ") where stktype='" + Cmb_StkType.SelectedValue.ToString() + "'";
            }
            else
            {
                str = "Select BatchId,BatchNo  from PROD.PendingBatchIdList(" + Gramboo.GeneralConfig.BranchId + "," + Gramboo.GeneralConfig.CompanyID + ") where stktype='" + Cmb_StkType.SelectedValue.ToString() + "'";
            }

            cmd.CommandText = str;
            CmbBatchNo.DisplayMember = "BatchNo";
            CmbBatchNo.ValueMember = "BatchId";
            CmbBatchNo.DataSource = DBConn.GetData(cmd, "BatchNo").Tables[0];
            //}
            //else
            //{
            //Gramboo.General.Setupcombo(CmbBatchNo, "PROD.VBatchIdMaster", "BatchNo", "batchid");
            //}

        }

        private void AfterComboLeave(Gramboo.Controls.GrbDataGridView dgv, Gramboo.Controls.GrbComboBox cmb, int valuecolumnindex = -1)
        {

            if (flag || dgv.CurrentCell == null)
                return;


            cmb.Visible = false;
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
                    if (((System.Data.DataTable)dgv.DataSource).Columns.Contains(dgv.CurrentCell.OwningColumn.Name))
                    {
                        if (((System.Data.DataTable)dgv.DataSource).Select(dgv.CurrentCell.OwningColumn.Name + "='" + (cmb.Text == "" ? "0" : cmb.Text) + "'").Length != 0)
                        {
                            return;
                        }
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
        private void Totalamt()
        {
            double StCash = 0, DiaCash = 0, MetalCash = 0, MC = 0, TotalAmt = 0, Amount = 0;
            if (dgv_itemDetails.Rows.Count > 0)
            {
                TotalAmt = Convert.ToDouble(txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text);
                StCash = Convert.ToDouble(txt_totstcash.Text == "" ? "0" : txt_totstcash.Text);
                DiaCash = Convert.ToDouble(txt_totdiacash.Text == "" ? "0" : txt_totdiacash.Text);
                MetalCash = Convert.ToDouble(txt_totmetalcash.Text == "" ? "0" : txt_totmetalcash.Text);
                MC = Convert.ToDouble(txt_totalmc.Text == "" ? "0" : txt_totalmc.Text);

                if (ChkTotalAmount == true)
                {
                    Amount = TotalAmt;
                }
                else
                {
                    Amount = (ChkSt_DiaCash == false ? 0 : StCash + DiaCash) + (ChkMC == false ? 0 : MC) + (ChkMetalCash == false ? 0 : MetalCash);
                }
                TxtItemTotal.Text = TotalAmt.ToString("F2");
                txt_Totalamount.Text = Amount.ToString("f2");
            }   
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
            dgv.CurrentRow.Cells["StCtWt"].Value = Math.Round(StwtCt, 3).ToString("f3");
        }

        private void StoneCtwtToWT(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double Stwt = 0.000, StwtCt = 0.000;
            if (dgv.CurrentRow == null)
                return;
            if (dgv.CurrentRow.Cells["StCtWt"].Value.ToString() != "")
            {
                StwtCt = Convert.ToDouble(dgv.CurrentRow.Cells["StCtWt"].Value.ToString());
            }
            Stwt = Math.Round(StwtCt, 3) * 0.2;

            dgv.CurrentRow.Cells["StWt"].Value = Math.Round(Stwt, 3).ToString("f3");
        }

        private void StoneCash(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double Stwt = 0.000, StCash = 0.000, StRate = 0.00;
            if (dgv.CurrentRow == null)
                return;
            if (dgv.CurrentRow.Cells["StWt"].Value.ToString() != "")
            {
                Stwt = Convert.ToDouble(dgv.CurrentRow.Cells["StWt"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["StRate"].Value.ToString() != "")
            {
                StRate = Convert.ToDouble(dgv.CurrentRow.Cells["StRate"].Value.ToString());
            }
            StCash = Stwt * StRate;
            dgv.CurrentRow.Cells["StCash"].Value = StCash.ToString("f3");
        }

        private void MC_Cash(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double Netwt = 0.000, WstPerc = 0.000, MCRate = 0.00;
            if (dgv.CurrentRow == null)
                return;

            if ((cmbpartytype.Text == "Supplier" && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true)) { dgv_itemDetails.CurrentRow.Cells["MC"].Value = 0; return; }
            else if ((cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true)) { dgv_itemDetails.CurrentRow.Cells["MC"].Value = 0; return; }

            if (SoftwareSettings.CompName == "MANJALI")
            {
                if (dgv_itemDetails.CurrentRow.Cells["Wst Perc"].Value.ToString() != "")
                {
                    WstPerc = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Wst Perc"].Value.ToString());
                }

                if (dgv_itemDetails.CurrentRow.Cells["Netwt"].Value.ToString() != "")
                {
                    Netwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Netwt"].Value.ToString());
                }

                if (dgv_itemDetails.CurrentRow.Cells["MCRate"].Value.ToString() != "")
                {
                    MCRate = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["MCRate"].Value.ToString());
                }

                if (WstPerc != 0)
                {
                    dgv_itemDetails.CurrentRow.Cells["MC"].Value = (Netwt * MCRate).ToString("f2");
                }
            }
        }

        public void MetalCash(Gramboo.Controls.GrbDataGridView dgv, Double Rate_)
        {
            //if (cmb_Purchasetype.SelectedIndex != 15 && SoftwareSettings.CompName != "PEENIKA")
            //{
            //    if ((cmbpartytype.Text == "Supplier" && grbreceipt.Checked == true) || (cmbpartytype.Text == "Customer" && grbissue.Checked == true))
            //        return;
            //}

            Double metalrate = 0, NetWt = 0, VA = 0, Touch = 0, metalcash = 0, Wt916 = 0, MetalId = 0, Gwt = 0, Nos = 0, IssueWt = 0, Rate = 0;
            string CalcOn = "", BillGroup = "";
            if (dgv.CurrentRow == null)
                return;
            //foreach (DataGridViewRow r in dgv.Rows)
            //{
            if (dgv.CurrentRow.Cells["ItemId"].Value.ToString() != "")
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                ("SELECT MetalId,[Purity Value],[Calculated On],BillGroup FROM ITM.VItemMaster WHERE ItemId =" + dgv.CurrentRow.Cells["ItemId"].Value.ToString() + "")).Tables[0])
                {

                    if (dt.Rows.Count > 0)
                    {

                        MetalId = Convert.ToSingle(dt.Rows[0]["MetalId"].ToString());
                        if (dt.Rows[0]["Purity Value"].ToString() != "")
                        {
                            Touch = Convert.ToSingle(dt.Rows[0]["Purity Value"].ToString());
                        }
                        else
                        {
                            Touch = 0;
                        }
                        CalcOn = (dt.Rows[0]["Calculated On"].ToString());
                        BillGroup = (dt.Rows[0]["BillGroup"].ToString());
                    }

                    else
                    {
                        return;
                    }
                }
            }

            if ((dgv.CurrentRow.Cells["Rate"].Value.ToString() != "" && CalcOn == "Nos"))
            {
                metalrate = Convert.ToSingle(dgv.CurrentRow.Cells["Rate"].Value.ToString() == "" ? txtRate.Text : dgv.CurrentRow.Cells["Rate"].Value.ToString());

            }
            else if (CalcOn == "NetWt" || CalcOn == "Gwt")
            {

                if (BillGroup == "S")
                {
                    metalrate = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'  and Branch_id=" + txtBranchID.Text + " Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["SilverRate"]).ToString());
                }
                else if (BillGroup == "P")
                {
                    metalrate = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 PlatinumRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'  and Branch_id=" + txtBranchID.Text + " Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["PlatinumRate"]).ToString());
                }

                else if (Touch > 92)
                {
                    metalrate = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1  BarRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BarRate"]).ToString());
                }
                else if (Touch < 90)
                {
                    metalrate = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1  MetalRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["MetalRate"]).ToString());
                }
                else
                {
                    metalrate = Convert.ToSingle((DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'   and Branch_id=" + txtBranchID.Text + "  Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString());
                }

                if (Rate_ == 0) { dgv.CurrentRow.Cells["Rate"].Value = metalrate.ToString("f2"); }
            }

            else
            {
                return;
            }
            if (dgv.CurrentRow.Cells["Rate"].Value.ToString() != "")
            {
                Rate = Convert.ToSingle(dgv.CurrentRow.Cells["Rate"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["NetWt"].Value.ToString() != "")
            {
                NetWt = Convert.ToSingle(dgv.CurrentRow.Cells["NetWt"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["IssueWt"].Value.ToString() != "")
            {
                IssueWt = Convert.ToSingle(dgv.CurrentRow.Cells["IssueWt"].Value.ToString());
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
                if (SoftwareSettings.CompName == "POPULAR GOLD AND DIAMONDS" && grbreceipt.Checked ==true)
                {
                    metalcash = 0;
                }
                else
                {
                    metalcash = NetWt * Rate;
                }

            }
            else if (CalcOn == "Gwt")
            {
                if (SoftwareSettings.CompName == "POPULAR GOLD AND DIAMONDS" && grbreceipt.Checked == true)
                {
                    metalcash = 0;
                }
                else
                {
                    metalcash = Gwt * Rate;
                }
            }
            else
            {
                metalcash = Nos * Rate;
            }


            //if (SoftwareSettings.CompName == "PEENIKA" && cmb_Purchasetype.Text == "Repair" && grbissue.Checked == true && cmbpartytype.Text == "Customer")
            //{
            //    metalcash = ((Gwt - IssueWt) * Rate);
            //}

            dgv.CurrentRow.Cells["MetalCash"].Value = metalcash.ToString("f2");
        }

        private void ComboKeydown(Gramboo.Controls.GrbDataGridView dgv_itemDetails, Gramboo.Controls.GrbComboBox cmb, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {

                dgv_itemDetails.Focus();
                cmb.Visible = false;
            }
        }
        private void calcufinal()
        {
            Double amt = 0, TaxAmt = 0, Disamt = 0, tds = 0, othch = 0;

            txt_taxAmount.Text = (string.IsNullOrEmpty(txt_taxAmount.Text.Trim()) ? "0.00" : txt_taxAmount.Text);
            txtDisAmnt.Text = (string.IsNullOrEmpty(txtDisAmnt.Text.Trim()) ? "0.00" : txtDisAmnt.Text);
            txt_amt.Text = (string.IsNullOrEmpty(txt_amt.Text.Trim()) ? "0.00" : txt_amt.Text);
            txt_tds.Text = (string.IsNullOrEmpty(txt_tds.Text.Trim()) ? "0.00" : txt_tds.Text);
            Disamt = Convert.ToDouble(txtDisAmnt.Text);
            TaxAmt = Convert.ToDouble(txt_taxAmount.Text);
            tds = Convert.ToDouble(txt_tds.Text);
            othch = Convert.ToDouble(TxtOTCharge.Text);
            amt = TaxAmt + Disamt + tds + othch;
            txt_amt.Text = amt.ToString("f2");
            txtnet.Text = amt.ToString("f2");

        }
        private void calcufinal1()
        {
            Double amt = 0, TaxAmt = 0, Disamt = 0, tds = 0, othch = 0;

            txt_taxAmount.Text = (string.IsNullOrEmpty(txt_taxAmount.Text.Trim()) ? "0.00" : txt_taxAmount.Text);
            txtDisAmnt.Text = (string.IsNullOrEmpty(txtDisAmnt.Text.Trim()) ? "0.00" : txtDisAmnt.Text);
            txt_amt.Text = (string.IsNullOrEmpty(txt_amt.Text.Trim()) ? "0.00" : txt_amt.Text);
            txt_tds.Text = (string.IsNullOrEmpty(txt_tds.Text.Trim()) ? "0.00" : txt_tds.Text);
            Disamt = Convert.ToDouble(txtDisAmnt.Text);
            TaxAmt = Convert.ToDouble(txt_taxAmount.Text);
            tds = Convert.ToDouble(txt_tds.Text);
            othch = Convert.ToDouble(TxtOTCharge.Text);
            amt = TaxAmt + Disamt + othch;
            txt_amt.Text = amt.ToString("f2");
            txtnet.Text = amt.ToString("f2");

        }
        private void SetComboLocation(Gramboo.Controls.GrbDataGridView dgv_itemDetails, Gramboo.Controls.GrbComboBox cmb, int columnindex, int rowindex)
        {

            int SaleCurrentRow = rowindex;
            cmb.Parent = dgv_itemDetails.Parent;
            cmb.Visible = true;
            cmb.Text = dgv_itemDetails.SelectedCells[0].Value.ToString();

            System.Drawing.Point p = new System.Drawing.Point();
            p = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Location;
            cmb.Location = new System.Drawing.Point(p.X + dgv_itemDetails.Parent.Location.X - 5, p.Y + dgv_itemDetails.Parent.Location.Y - 15);


            cmb.Size = dgv_itemDetails.GetCellDisplayRectangle(dgv_itemDetails.SelectedCells[0].ColumnIndex, dgv_itemDetails.SelectedCells[0].RowIndex, true).Size;
            cmb.BringToFront();
            cmb.Focus();
            cmb.DroppedDown = true;

        }

        private void TxtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dgv_itemDetails.Focus();
        }
        public override bool ValidateControls()
        {
            if (dtp_dt.Value.Date != DateTime.Now.Date)
            {
                if (Gramboo.General.ShowMessage("System date and invoice date are different. Do you want to continue?", "Datediffrence", MessageBoxIcon.Question, MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return false;
                }
            }

            if(ChkNoBarcodeStock.Checked)
            {
                foreach(DataGridViewRow r in dgv_itemDetails.Rows)
                {
                    if(r.Cells["Prodcodeid"].Value.ToString()!="0")
                    {
                        r.Cells["Prodcodeid"].Value = "0";
                    }
                }
            }

            if (!IsEditMode && SoftwareSettings.CompName == "MANJALI")
            {
                DataTable TempGrid5 = (dgv_itemDetails.DataSource as DataTable);

                var myInClause_1 = new string[] { "BULLION" };
                var myInClause_2 = new string[] { "COPPER" };

                var ItemName_1 = (from Rows in TempGrid5.AsEnumerable().Where(W => myInClause_1.Contains(W.Field<string>("Item Name")))
                                  select Rows["Item Name"]).Distinct().ToList();
                var ItemName_2 = (from Rows in TempGrid5.AsEnumerable().Where(W => myInClause_2.Contains(W.Field<string>("Item Name")))
                                  select Rows["Item Name"]).Distinct().ToList();

                if ((cmbpartytype.Text == "Supplier" && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true) && CopperAdded == false && SoftwareSettings.CompName == "MANJALI" && ItemName_1.Count == 1 && ItemName_2.Count == 0)
                {

                    DialogResult d =
              Gramboo.General.ShowMessage(

              "Do You Want To Add Copper ? \n\n", "Add Copper", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                    if (d == DialogResult.Yes)
                    {
                        btn_copper.PerformClick();
                        return false;
                    }
                }
            }




                if (!SoftwareSettings.TxType && SoftwareSettings.CompName !="TTD"  ) { txtOne_Two.Text = "1"; taxone(); }

            if (SoftwareSettings.TxType || SoftwareSettings.CompName == "TTD") { if (Convert.ToInt32((string.IsNullOrEmpty(txtOne_Two.Text.Trim()) ? "0" : txtOne_Two.Text)) <= 0) { txtOne_Two.Visible = true; txtOne_Two.Focus(); txtOne_Two.ShowMessage("Blank Value Not Allowed..!!!"); return false; } }

            if (txtOne_Two.Text != "1" && txtOne_Two.Text != "2" && txtOne_Two.Text != "3")
            {
                txtOne_Two.Visible = true;
                txtOne_Two.Focus();
                txtOne_Two.ShowMessage("Please Enter a valid Type to Continue...!!!");
                return false;
            }
            
           

            if (importfile.Length == 0)
            {
                foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                {
                    if (r.Cells["ProdCodeId"].Value.ToString().Trim().Length >= 2)
                    {

                        using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                        ("select ItemID,prodCode from STK.ProdCodeMaster where ProdCodeId=" + r.Cells["ProdCodeId"].Value.ToString() + " and Company_id=" + txtcompId.Text + "  ")).Tables[0])
                        {

                            if (dt.Rows[0]["ItemID"].ToString() != r.Cells["ItemID"].Value.ToString())
                            {
                                string ItemName = "";
                                using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand
                                        ("select ItemName from ITM.ItemMaster where ItemId=" + dt.Rows[0]["ItemID"].ToString() + " and Company_id=" + txtcompId.Text + "  ")).Tables[0])
                                {
                                    if (dt1.Rows.Count > 0)
                                    {
                                        ItemName = dt1.Rows[0]["ItemName"].ToString();
                                    }
                                }

                                Gramboo.General.ShowMessage(
                                                       " ItemName Selected Different from Barcoded Item. \n\n" +
                                                       " Please Correct the Item Name to Continue. \n" +
                                                       " Check Barcode '" + dt.Rows[0]["prodCode"].ToString() + "'" +
                                                       " And the Original ItemName Is '" + ItemName + "'. \n", "WARNING!!!!", MessageBoxIcon.Warning, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);
                                return false;
                            }
                        }

                       
                    }
                }
            }

            if (cmb_Purchasetype.Text == "OldGold" || cmb_Purchasetype.Text == "Melting")
            {
                foreach (DataGridViewRow item in this.dgv_itemDetails.Rows)
                {
                    if ((item.Cells["Gwt"].Value.ToString() == "" ? "0.00" : item.Cells["Gwt"].Value.ToString()) == "0.00" && (item.Cells["StWt"].Value.ToString() == "" ? "0.00" : item.Cells["StWt"].Value.ToString()) == "0.00" && (item.Cells["Diawt"].Value.ToString() == "" ? "0.00" : item.Cells["Diawt"].Value.ToString()) == "0.00")
                    {
                        dgv_itemDetails.Rows.RemoveAt(item.Index);
                    }
                }
            }

            if (((cmbpartytype.Text == "Supplier") && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true) && txtOne_Two.Text != "2" && SoftwareSettings.BlockValidations)
            {
                if (!IsEditMode)
                {
                    AutoFillPending();
                }
                if (pendingsave() == false)
                {
                    return false;
                }
            }
            if (((cmbpartytype.Text == "Customer") && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true) && txtOne_Two.Text != "2" && SoftwareSettings.BlockValidations)
            {
                if (!IsEditMode)
                {
                    AutoFillPending();
                }
                if (pendingsave() == false)
                {
                    return false;
                }
            }
            if (!IsEditMode)
                TxtVoucherNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtp_dt.Value,
            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            //if (txt_touch.Text == "0" || txt_touch.Text == "")
            //{
            //    Gramboo.General.ShowMessage("Enter the touch ");
            //}

            if (ChkBranch.Checked == true)
            {
                if (cmbbranch.SelectedIndex == -1)
                {
                    cmbbranch.ShowMessage("Select Branch");
                    return false;
                }
            }
            if (cmbpartytype.SelectedValue == null)
            {
                cmbpartytype.ShowMessage("Select Party Type");
                return false;
            }
            if (Cmb_StkType.SelectedValue == null)
            {
                Cmb_StkType.ShowMessage("Select Stock Type");
                return false;
            }
            if (cmbpartyname.SelectedIndex == -1)
            {
                cmbpartyname.ShowMessage("Select Party Name");
                return false;
            }
            if (cmb_Purchasetype.SelectedIndex == -1)
            {
                cmb_Purchasetype.ShowMessage("Select Purchase Type");
                return false;
            }
            if (cmbpartytype.SelectedValue != null)
            {
                //if (dgv_itemDetails.CurrentRow.Cells["Rate"].Value.ToString() == "")
                //{
                //    dgv_itemDetails.CurrentRow.Cells["Rate"].Value = "0";
                //}
            }
            if (cmbpartytype.SelectedValue.ToString() == "0")
            {
                foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                {
                    if (r.Cells["Touch"].Value.ToString() == "0")
                    {
                        txt_mcmode.ShowMessage("Please Check The Touch Value");
                    }
                }
            }

            //foreach (DataGridViewRow r in dgv_itemDetails.Rows)
            //{
            //    if (r.Cells["ProdCodeId"].Value.ToString().Trim().Length >= 2)
            //    {
            //        int i = 0;
            //        string ProdcodeId = r.Cells["ProdCodeId"].Value.ToString();
            //        String Prodcode = r.Cells["ProdCode"].Value.ToString();
            //        foreach (DataGridViewRow r1 in dgv_itemDetails.Rows)
            //        {
            //            if (r1.Cells["ProdCodeId"].Value.ToString() == ProdcodeId)
            //            {
            //                i++;
            //                if (i >= 2)
            //                {
            //                    Gramboo.General.ShowMessage("Barcode Duplication Found Please Check Barcode " + Prodcode);
            //                    return false;
            //                }
            //            }
            //        }
            //    }
            //}

            //if (ChkBranch.Checked == false)
            //{
            //    foreach (DataGridViewRow r in dgv_itemDetails.Rows)
            //    {
            //        if (r.Cells["ItemId"].Value.ToString().Trim().Length >= 2)
            //        {
            //            r.Cells["BranchId"].Value = Convert.ToInt32(txtBranchID.Text);
            //        }
            //    }
            //}

            DataTable TempGrid = (dgv_itemDetails.DataSource as DataTable);

            foreach (DataRow row in TempGrid.Rows)
            {
                if (row["Gwt"].ToString() == "") { row["Gwt"] = "0.000"; }
                if (row["Nos"].ToString() == "") { row["Nos"] = "0"; }
            }

            int Gwt_Zero = 0;

            if (cmb_Purchasetype.SelectedIndex.ToString() != "7" & cmb_Purchasetype.SelectedIndex.ToString() != "11")
            {
                var Item_WithOut_Gwt = (from Rows in (TempGrid).AsEnumerable().Where(W => W.Field<Int64?>("ItemId") != null).Where(W => Convert.ToDouble(W.Field<Decimal>("Gwt")) <= 0)
                                        select Rows["Item Name"]).Distinct().ToList();

                foreach (var ItemName in Item_WithOut_Gwt)
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                ("SELECT [Calculated On] as calc FROM ITM.VItemMaster WHERE ITEMID= "+ dgv_itemDetails.CurrentRow.Cells["Itemid"].Value.ToString()+"")).Tables[0])

                        if (dt.Rows.Count >= 1)
                        {
                            string calc;
                            calc = dt.Rows[0]["calc"].ToString();
                            if (calc != "Nos")
                                if (ItemName.ToString().Trim().Length >= 2)
                                {
                                    Gwt_Zero++;
                                    if (Gwt_Zero > 0)
                                    { Gramboo.General.ShowMessage("Please Enter a Valid Weight for Product(" + ItemName.ToString().Trim() + ")..!!"); return false; }
                                }
                        }
                }
            }


            if (grbissue.Checked == true)
            {


                DialogResult d =
          Gramboo.General.ShowMessage(

          "Do You Want To  Generate Eway ? \n\n", "Generate Eway", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                if (d == DialogResult.Yes)
                {

                    GenerateEway = true;

                    //if(Convert.ToSingle( txt_Distance.Text)==0)
                    //{
                    //    txt_Distance.ShowMessage("Enter Distance");
                    //    return false;
                    //}
                    //else if ( txt_tansmode.SelectedValue ==null)
                    //{
                    //    txt_tansmode.ShowMessage("Select Transportation Mode");
                    //    return false;
                    //}
                  //else  if (Common.ValidateRegistrationNumber (txt_vechileno.Text, txt_tansmode.Text.ToString(),"") == false)
                  //  {
                  //      txt_vechileno.ShowMessage("Not a Valid Vehicle Number");
                  //      return false;
                  //  }
                      if (cmb_DocumentType.SelectedValue == null)
                    {
                        cmb_DocumentType.ShowMessage("Select Document Type");
                        return false;
                    }

                    else if (cmb_SupplyType.SelectedValue == null)
                    {
                        cmb_SupplyType.ShowMessage("Select Supply Type");
                        return false;
                    }
                    else if (cmb_SubsupplyType.SelectedValue == null)
                    {
                        cmb_SubsupplyType.ShowMessage("Select Sub Supply Type");
                        return false;
                    }
                    else if (cmb_TransactionType.SelectedValue == null)
                    {
                        cmb_TransactionType.ShowMessage("Select  Transaction Type ");
                        return false;
                    }
                    //else if (cmb_TransporterName.SelectedValue == null)
                    //{
                    //    cmb_TransporterName.ShowMessage("Select   Transporter Name ");
                    //    return false;
                    //}

                }

                if (!IsEditMode)
                {
                    if (dgvPendingSTK.Rows.Count < 0)
                    {
                        // var ItemId_Gwt = (dgv_SalesDetails.DataSource as DataTable).AsEnumerable()
                        //.GroupBy(row => row.Field<Int64>("ItemId"))
                        //.Select(grp => new
                        //{
                        //    Itemid_ = grp.Key,
                        //    itemName_ = grp.Select(row => row.Field<string>("ItemName")),
                        //    Gwt_ = grp.Sum(row => row.Field<Decimal?>("Gwt")) ?? 0
                        //}); ;

                        /*when you know fix  Anonymous object null value while converting it to any datatype then remove this loop and the condition
                         and tempgrid. directily convert the datagrid as table*/

                        var ItemId_Gwt = TempGrid/*(dgv_SalesDetails.DataSource as DataTable)*/.AsEnumerable().Where(W => W.Field<Int64?>("ItemId") != null)
                 .Select(T => new
                 {
                     Itemid_ = T["ItemId"],
                     itemName_ = T["Item Name"],
                     Gwt_ = T["Gwt"],
                     Qty_ = T["Nos"]

                 }).GroupBy(S => new { S.Itemid_, S.itemName_ })
                  .Select(G => new
                  {
                      Itemid_ = G.Key.Itemid_,
                      itemName_ = G.Key.itemName_,
                      Gwt_ = G.Sum(T => Math.Round(Convert.ToDouble(T.Gwt_), 3)),
                      Qty_ = G.Sum(T => Convert.ToInt64(T.Qty_)),
                  });

                        string str = "select Isnull(t1.ItemId,0) as ItemId,Isnull(t1.Wt,0) as Gwt,isnull(t1.Qty,0) as Qty,isnull(t2.MaintainStock,'Only Wt') as MaintainStock,isnull(t2.StockMaintanence,'') as StockMaintanence" +
                            " FROM STK.GetItemwiseOpeningStock( '" + dtp_dt.Value.AddDays(1).ToString("dd-MMM-yyyy") + "'," + txtDeptName.Text + "," + txtcompId.Text + "," + txtBranchID.Text + ") as t1" +
                            " inner join  ITM.ItemMaster as t2 on t1.ItemId=t2.ItemId where t1.STKType='" + Cmb_StkType.SelectedValue.ToString() + "'";
                        SqlDataAdapter da = new SqlDataAdapter(str, DBConn.ConnectionProperties.ConnectionString);
                        da.SelectCommand.CommandTimeout = 0;
                        DataTable Stock = new DataTable();
                        da.Fill(Stock);

                        foreach (var r in ItemId_Gwt)
                        {
                            if (r.Itemid_.ToString().Trim().Length >= 2)
                            {
                                DataTable TempStock = Stock.Clone();
                                DataRow[] TempRows = Stock.Select("ItemId = " + r.Itemid_ + "");
                                foreach (DataRow Rw in TempRows) { TempStock.ImportRow(Rw); }
                                if (SAFA.Classes.SoftwareSettings.ValidateStock == true)
                                {
                                    if (TempStock.Rows.Count > 0)
                                    {
                                        String MaintainStock_d = TempStock.Rows[0]["MaintainStock"].ToString();
                                        Int64 Qty_d = Convert.ToInt64(TempStock.Rows[0]["Qty"].ToString());
                                        double Gwt_d = Convert.ToDouble(TempStock.Rows[0]["Gwt"].ToString());

                                        if (MaintainStock_d.Contains("Wt") || MaintainStock_d.Contains("Barcode Wise"))
                                        {
                                            if (Gwt_d < 0) { Gramboo.General.ShowMessage("The product(" + r.itemName_/*.ToList()[0]*/ + ") is currently out of stock,\n" + "Current Gwt is " + Gwt_d.ToString("f3") + "", "WARNING!!!!", MessageBoxIcon.Warning, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1); return false; }
                                            Double S = Math.Round(Gwt_d - Math.Round(Convert.ToDouble(r.Gwt_), 3), 3);
                                            if (S < 0) { Gramboo.General.ShowMessage("Product(" + r.itemName_/*.ToList()[0]*/ + ") weight(" + r.Gwt_ + ") is greater than stock value(" + Gwt_d.ToString("f3") + "),\n" + "Difference Gwt is " + S.ToString("f3") + "", "WARNING!!!!", MessageBoxIcon.Warning, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1); return false; }
                                        }
                                        if (MaintainStock_d.Contains("Qty"))
                                        {
                                            if (Qty_d < 0) { Gramboo.General.ShowMessage("The product(" + r.itemName_/*.ToList()[0]*/ + ") is currently out of stock,\n" + "Current Qty is " + Qty_d.ToString("f0") + "", "WARNING!!!!", MessageBoxIcon.Warning, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1); return false; }
                                            Double S = Qty_d - Convert.ToInt64(r.Qty_);
                                            if (S < 0) { Gramboo.General.ShowMessage("Product(" + r.itemName_/*.ToList()[0]*/ + ") Qty(" + r.Qty_ + ") is greater than stock value(" + Qty_d.ToString("f0") + "),\n" + "Difference Qty is " + S.ToString("f0") + "", "WARNING!!!!", MessageBoxIcon.Warning, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1); return false; }
                                        }
                                    }

                                    else { Gramboo.General.ShowMessage("The product(" + r.itemName_/*.ToList()[0]*/ + ") is currently out of stock,\n" + "Current Gwt & Qty is 0", "WARNING!!!!", MessageBoxIcon.Warning, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1); return false; }
                                }
                            }
                        }



                        //var Items_Wt_Prod = TempGrid.AsEnumerable().Where(W => W.Field<Int64?>("ItemId") != null && W.Field<Int64?>("ProdCodeId") == null || W.Field<Int64?>("ProdCodeId") == 0)
                        //                    .Select(T => new { Itemid_ = T["ItemId"], itemName_ = T["ItemName"] }).Distinct().ToList();

                        //foreach (var r in Items_Wt_Prod)
                        //{
                        //    if (r.ToString().Trim().Length >= 2)
                        //    {
                        //        DataTable TempStock = Stock.Clone();
                        //        DataRow[] TempRows = Stock.Select("ItemId = " + r.Itemid_ + "");
                        //        foreach (DataRow Rw in TempRows) { TempStock.ImportRow(Rw); }

                        //        if (TempStock.Rows.Count > 0)
                        //        {
                        //            String StockMaintanence_d = TempStock.Rows[0]["StockMaintanence"].ToString();
                        //            if (StockMaintanence_d.Contains("Barcode Wise"))
                        //            {
                        //                Gramboo.General.ShowMessage("you can't sell this Product(" + r.itemName_ + ") without a Barcode", "WARNING!!!!", MessageBoxIcon.Warning, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);
                        //                return false;
                        //            }
                        //        }
                        //    }
                        //    }
                    }
                }

                if (SoftwareSettings.CompName == "SAFA")
                {
                    if (TxtRemarks.Text.ToString().Trim().Length < 2)
                    {
                        TxtRemarks.ShowMessage("Blank values Not Allowed");
                        return false;
                    }

                    if (txt_tansmode.Text.ToString().Trim().Length < 1)
                    {
                        txt_tansmode.ShowMessage("Blank values Not Allowed");
                        return false;
                    }

                    if (txt_vechileno.Text.ToString().Trim().Length < 1)
                    {
                        txt_vechileno.ShowMessage("Blank values Not Allowed");
                        return false;
                    }

                    if (place_supply.Text.ToString().Trim().Length < 1)
                    {
                        place_supply.ShowMessage("Blank values Not Allowed");
                        return false;
                    }

                    if (txt_purpose.Text.ToString().Trim().Length < 1)
                    {
                        txt_purpose.ShowMessage("Blank values Not Allowed");
                        return false;
                    }
                }

                if (cmb_Purchasetype.Text == "OldGold" || cmb_Purchasetype.Text == "JobWork")
                {
                    if (cmbpartyname.SelectedValue != null && cmbpartytype.Text == "Supplier" && SAFA.Classes.SoftwareSettings.IsShowRoom == true && SuppBranch == "107")
                    {
                        if (!SAFA.Classes.Common.TestLinkedServerConnection(DBConn, Convert.ToInt16(txtBranchID.Text)))
                        {
                            Gramboo.General.ShowMessage("cant connect to the Server network right now. please try again later");
                            return false;
                        }

                        if (!SAFA.Classes.Common.TestLinkedServer(DBConn, Convert.ToInt16(txtBranchID.Text)))
                        {
                            Gramboo.General.ShowMessage("The server is temporarily unavailable. If the problem continues, please contact your support team.");
                            return false;
                        }
                    }
                }
                if (cmb_Purchasetype.Text == "Melted OG" || cmb_Purchasetype.Text == "Result")
                {
                    if (cmbpartyname.SelectedValue != null && cmbpartytype.Text == "Customer" && SAFA.Classes.SoftwareSettings.IsShowRoom == false)
                    {
                        if (!SAFA.Classes.Common.TestLinkedServerConnection(DBConn, Convert.ToInt16(txtBranchID.Text)))
                        {
                            Gramboo.General.ShowMessage("cant connect to the Server network right now. please try again later or Contact your administrator");
                            return false;
                        }

                        if (!SAFA.Classes.Common.TestLinkedServer(DBConn, Convert.ToInt16(txtBranchID.Text)))
                        {
                            Gramboo.General.ShowMessage("The server is temporarily unavailable. If the problem continues, please contact your support team.");
                            return false;
                        }

                    }

                }
                if (grbissue.Checked == true && !IsEditMode && SoftwareSettings.BlockValidations)
                {
                    foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                    {
                        double i = 0, J = 0, K = 0;
                        if (r.Cells["BatchId"].Value.ToString().Trim().Length >= 2)
                        {
                            string batchid = r.Cells["BatchId"].Value.ToString();
                            string BatchNo = r.Cells["BatchNo"].Value.ToString();
                            string itemid = r.Cells["ItemId"].Value.ToString();
                            foreach (DataGridViewRow r1 in dgv_itemDetails.Rows)
                            {
                                if (r1.Cells["BatchId"].Value.ToString() == batchid && r1.Cells["Itemid"].Value.ToString() == itemid)
                                {
                                    K = Convert.ToDouble(r1.Cells["Gwt"].Value.ToString());
                                    i = i + K;
                                }
                            }
                            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                           ("SELECT Gwt FROM PROD.FunBatchIdPending('" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "','P'," + txtcompId.Text + "," + txtBranchID.Text + ") where BatchId =" + batchid + " and itemid =" + itemid + " ")).Tables[0])
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    J = Convert.ToDouble(dt.Rows[0]["Gwt"].ToString());
                                }
                            }
                            if (Math.Round(J, 2) - Math.Round(i, 2) < 0)
                            {
                                Gramboo.General.ShowMessage("Invalid WT Check BatchNo " + BatchNo);
                                return false;
                            }
                        }
                    }
                }
            }

            else if (grbreceipt.Checked == true && SoftwareSettings.BlockValidations)
            {
                if (cmb_Purchasetype.Text != "OldGold")
                {
                    if (cmb_Purchasetype.Text != "Melted OG")
                    {
                        if (cmb_Purchasetype.Text != "JobWork")
                        {
                            if (cmb_Purchasetype.Text == "Melting" && cmbpartytype.SelectedValue.ToString() != "3")
                            {
                                foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                                {
                                    double i = 0, J = 0, K = 0, L = 0;
                                    if (r.Cells["BatchId"].Value.ToString().Trim().Length >= 2)
                                    {
                                        string batchid = r.Cells["BatchId"].Value.ToString();
                                        string BatchNo = r.Cells["BatchNo"].Value.ToString();
                                        string itemid = r.Cells["ItemId"].Value.ToString();
                                        foreach (DataGridViewRow r1 in dgv_itemDetails.Rows)
                                        {
                                            if (r1.Cells["BatchId"].Value.ToString() == batchid && r1.Cells["Itemid"].Value.ToString() == itemid)
                                            {
                                                K = (Convert.ToDouble(r1.Cells["Gwt"].Value.ToString()) + Convert.ToDouble(r1.Cells["Mud"].Value.ToString() == "" ? "0" : r1.Cells["Mud"].Value.ToString()));
                                                i = i + K;
                                                L = Convert.ToDouble(r1.Cells["IssueWt"].Value.ToString());
                                                J = J + L;
                                            }
                                        }
                                        if (Math.Round(J, 2) - Math.Round(i, 2) < 0)
                                        {
                                            Gramboo.General.ShowMessage("Invalid WT..!!! WT > ISWT Check BatchNo " + BatchNo);
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (cmb_Purchasetype.SelectedIndex.ToString() != "8" && SoftwareSettings.BlockValidations)
                {

                    double iswt = 0, rcwt = 0, isdiawt = 0, rcdiawt = 0, isstwt = 0, rcstwt = 0, isactwt = 0, rcactwt = 0, tiswt = 0, trcwt = 0, tisdiawt = 0, trcdiawt = 0, tisstwt = 0, trcstwt = 0, tisactwt = 0, trcactwt = 0,
                    isloss = 0, rcloss = 0, tisloss = 0, trcloss = 0;
                    foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                    {
                        string str1 = "", RecFrom1 = r.Cells["IdType"].Value.ToString();
                        if (cmb_Purchasetype.SelectedIndex.ToString() == "15")
                        {
                            str1 = "select * from stk.VStockTransferPendingWtValidation where ReferenceId=" + r.Cells["Issid"].Value.ToString() + " and IdType='" + RecFrom1 + "'";

                            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                (str1)).Tables[0])
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    trcwt = Convert.ToDouble(dt.Rows[0]["IssueWt"].ToString());
                                    rcwt = rcwt + trcwt;

                                    trcdiawt = Convert.ToDouble(dt.Rows[0]["Diawt"].ToString());
                                    rcdiawt = rcdiawt + trcdiawt;

                                    trcstwt = Convert.ToDouble(dt.Rows[0]["StWt"].ToString());
                                    rcstwt = rcstwt + trcstwt;

                                    trcactwt = Convert.ToDouble(dt.Rows[0]["ActualWt"].ToString());
                                    rcactwt = rcactwt + trcactwt;
                                }
                            }
                        }

                        if (cmb_Purchasetype.SelectedIndex.ToString() != "1")
                        {
                            if (r.Cells["Gwt"].Value.ToString() != "")
                            {
                                trcwt = (Convert.ToDouble(r.Cells["Gwt"].Value.ToString()));
                                rcwt = rcwt + trcwt;
                            }
                            if (r.Cells["Diawt"].Value.ToString() != "")
                            {
                                trcdiawt = (Convert.ToDouble(r.Cells["Diawt"].Value.ToString()));
                                rcdiawt = rcdiawt + trcdiawt;
                            }
                            if (r.Cells["StWt"].Value.ToString() != "")
                            {
                                trcstwt = (Convert.ToDouble(r.Cells["StWt"].Value.ToString()));
                                rcstwt = rcstwt + trcstwt;
                            }
                            if (r.Cells["Loss"].Value.ToString() != "")
                            {
                                trcloss = (Convert.ToDouble(r.Cells["Loss"].Value.ToString()));
                                rcloss = rcloss + trcloss;
                            }
                            if (r.Cells["ActualWt"].Value.ToString() != "")
                            {
                                trcactwt = (Convert.ToDouble(r.Cells["ActualWt"].Value.ToString()));
                                rcactwt = rcactwt + trcactwt;
                            }
                        }

                        if (r.Cells["BatchId"].Value.ToString().Trim().Length <= 2 && r.Cells["Issid"].Value.ToString().Trim().Length >= 2)
                        {
                            string RecFrom = "", str = "";
                            RecFrom = r.Cells["IdType"].Value.ToString();
                            if (RecFrom == "STK") { str = "select isnull(Sum(Gwt),0) as IssueWt,isnull(sum(StWt),0) as StWt,isnull(sum(Diawt),0) as Diawt,isnull(sum(ActualWt),0) as ActualWt,isnull(sum(Loss),0) as Loss from  STK.StockTransferDetails where TransId=" + r.Cells["Issid"].Value.ToString() + ""; }
                            else if (RecFrom == "SUPO") { str = "select ABS(isnull(Sum(Gwt),0)) as IssueWt,ABS(isnull(sum(StWt), 0)) as StWt,ABS(isnull(sum(Diawt), 0)) as Diawt,ABS(isnull(sum(Actwt), 0)) as ActualWt,ABS(isnull(sum(0), 0)) as Loss from PUR.SupplierOpeningBalanceWeightDetails where MetalId=" + r.Cells["Issid"].Value.ToString() + ""; }
                            else if (RecFrom == "CO") { str = "select ABS(isnull(Sum(Gwt),0)) as IssueWt,ABS(isnull(sum(StWt), 0)) as StWt,ABS(isnull(sum(Diawt), 0)) as Diawt,ABS(isnull(sum(Actwt), 0)) as ActualWt,ABS(isnull(sum(0), 0)) as Loss from ACC.CustomerOpeningBalanceWeightDetails where MetalId=" + r.Cells["Issid"].Value.ToString() + ""; }

                            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                (str)).Tables[0])
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    tiswt = Convert.ToDouble(dt.Rows[0]["IssueWt"].ToString());
                                    iswt = iswt + tiswt;

                                    tisdiawt = Convert.ToDouble(dt.Rows[0]["Diawt"].ToString());
                                    isdiawt = isdiawt + tisdiawt;

                                    tisstwt = Convert.ToDouble(dt.Rows[0]["StWt"].ToString());
                                    isstwt = isstwt + tisstwt;

                                    tisactwt = Convert.ToDouble(dt.Rows[0]["ActualWt"].ToString());
                                    isactwt = isactwt + tisactwt;

                                    tisloss = Convert.ToDouble(dt.Rows[0]["Loss"].ToString());
                                    isloss = isloss + tisloss;
                                }
                            }
                        }
                    }
                    if ((cmb_Purchasetype.SelectedIndex.ToString() != "3" || SoftwareSettings.CompName != "PEENIKA"))
                    {
                        if (cmb_Purchasetype.SelectedIndex.ToString() == "1")
                        {
                            if (Math.Round(isactwt, 3) != 0)
                            {
                                if (Math.Round(rcactwt, 3) != Math.Round(isactwt, 3))
                                {
                                    Gramboo.General.ShowMessage("Invalid ACTWT..!!! WT > ISWT");
                                    return false;
                                }
                            }
                        }
                        else if ((cmb_Purchasetype.SelectedIndex.ToString() == "3") || cmb_Purchasetype.SelectedIndex.ToString() == "6" || cmb_Purchasetype.SelectedIndex.ToString() == "5")
                        {
                            if ((Math.Round(isactwt, 3) + Math.Round(isloss, 3)) != 0)
                            {
                                if ((Math.Round(rcactwt, 3) + Math.Round(rcloss, 3)) > (Math.Round(isactwt, 3) + Math.Round(isloss, 3)))
                                {
                                    Gramboo.General.ShowMessage("Invalid ACTWT..!!! WT > ISWT");
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (Math.Round(iswt, 3) != 0)
                            {
                                if (Math.Round(rcwt, 3) != Math.Round(iswt, 3))
                                {
                                    Gramboo.General.ShowMessage("Invalid GWT..!!! WT > ISWT");
                                    return false;
                                }
                            }

                            if (Math.Round(isdiawt, 3) != 0)
                            {
                                if (Math.Round(rcdiawt, 3) != Math.Round(isdiawt, 3))
                                {
                                    Gramboo.General.ShowMessage("Invalid DIAWT..!!! WT > ISWT");
                                    return false;
                                }
                            }
                            if (Math.Round(isstwt, 3) != 0)
                            {
                                if (Math.Round(rcstwt, 3) != Math.Round(isstwt, 3))
                                {
                                    Gramboo.General.ShowMessage("Invalid STWT..!!! WT > ISWT");
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            if (base.ValidateControls())
            {

                //foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                //{
                //    if (grbissue.Checked == true)
                //    {
                //        r.Cells["IsReceipt"].Value = 0;
                //    }
                //    if (grbreceipt.Checked == true)
                //    {
                //        r.Cells["IsReceipt"].Value = 1;
                //    }
                //}


                double wtbal = 0, wtbalprev = 0, wtbalcurr = 0;

                if (SoftwareSettings.CompName.ToUpper() == "TTD")
                {
                    if ( purchaseytype == "Supplier Jobwork" && grbreceipt.Checked)
                    {
                        using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                       ("Select [Closing Wt] FROM PUR.GETSUPPLIERBALANCE1DM('" + dtp_dt.Value.AddDays(1).ToString("dd-MMM-yyyy") + "'," + cmbpartyname.SelectedValue + "," + txtcompId.Text + "," + txtBranchID.Text + ",0,0)  ")).Tables[0])
                        {
                            if (dt.Rows.Count > 0)
                            {
                                wtbal = Convert.ToDouble(dt.Rows[0]["Closing Wt"].ToString());
                            }

                        }
                        if (IsEditMode)
                        {
                            using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                          ("Select isnull(TotalActualWt,0) as TotalActualWt FROM STK.STocktransfermaster where entryid=  " + txtEntryNo.Text)).Tables[0])
                            {
                                if (dt.Rows.Count > 0)
                                {

                                    wtbalprev = Convert.ToDouble(dt.Rows[0]["TotalActualWt"].ToString());
                                }

                            }
                        }
                        wtbalcurr = Convert.ToDouble(txttotalactualwt.Text == "" ? "0" : txttotalactualwt.Text); 

                        if ((wtbal + wtbalcurr - wtbalprev) > 0)
                        {
                            Gramboo.General.ShowMessage("Receipt Wt not cannot be higher than Issue Wt");
                            return false;
                        }
                    }

                }


                if (dgvPendingSTK.RowCount > 0)
                {

                    Double Gwt_Pen = 0, DiaWt_Pen = 0, StoneWt_Pen = 0, Netwt_Pen = 0;
                    Double Gwt_SA = 0, DiaWt_SA = 0, StoneWt_SA = 0, Netwt_SA = 0;

                    Gwt_Pen = Convert.ToDouble((dgvPendingSTK.SummaryRow.SummaryCells["Gwt"].Text.Length == 0 ? "0" : dgvPendingSTK.SummaryRow.SummaryCells["Gwt"].Text));
                    DiaWt_Pen = Convert.ToDouble((dgvPendingSTK.SummaryRow.SummaryCells["DiaWt"].Text.Length == 0 ? "0" : dgvPendingSTK.SummaryRow.SummaryCells["DiaWt"].Text));
                    StoneWt_Pen = Convert.ToDouble((dgvPendingSTK.SummaryRow.SummaryCells["StWt"].Text.Length == 0 ? "0" : dgvPendingSTK.SummaryRow.SummaryCells["StWt"].Text));
                    Netwt_Pen = Convert.ToDouble((dgvPendingSTK.SummaryRow.SummaryCells["Netwt"].Text.Length == 0 ? "0" : dgvPendingSTK.SummaryRow.SummaryCells["Netwt"].Text));

                    Gwt_SA = Convert.ToDouble((dgv_itemDetails.SummaryRow.SummaryCells["Gwt"].Text.Length == 0 ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["Gwt"].Text));
                    DiaWt_SA = Convert.ToDouble((dgv_itemDetails.SummaryRow.SummaryCells["DiaWt"].Text.Length == 0 ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["DiaWt"].Text));
                    StoneWt_SA = Convert.ToDouble((dgv_itemDetails.SummaryRow.SummaryCells["StWt"].Text.Length == 0 ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["StWt"].Text));
                    Netwt_SA = Convert.ToDouble((dgv_itemDetails.SummaryRow.SummaryCells["Netwt"].Text.Length == 0 ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["Netwt"].Text));

                    if ((Gwt_Pen != Gwt_SA) || (DiaWt_Pen != DiaWt_SA) || (StoneWt_Pen != StoneWt_SA) || (Netwt_Pen != Netwt_SA))
                    {
                        Gramboo.General.ShowMessage("Pending weight is not equal..!!");
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

        private void CalculateWst()
        {
            if (dgv_itemDetails.CurrentRow == null || (dgv_itemDetails.CurrentRow.Cells["PurityId"].Value == null))
                return;
            Double gwt = 0, Rate = 0, Wst = 0, NetWt = 0, Touch = 0, Purity = 0, diff = 0, MC = 0;
            NetWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["NetWt"].Value == DBNull.Value ? "0" : dgv_itemDetails.CurrentRow.Cells["NetWt"].Value);
            using (DataTable t = DBConn.GetData(new SqlCommand("select PurityValue from ITM.PurityMaster WHERE PurityId=" + (dgv_itemDetails.CurrentRow.Cells["PurityId"].Value == null ? "0" : dgv_itemDetails.CurrentRow.Cells["PurityId"].Value) + "")).Tables[0])
            {
                if (t.Rows.Count > 0)
                {
                    Purity = Convert.ToDouble(t.Rows[0]["PurityValue"]);
                }
            }

            Touch = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Touch"].Value);
            diff = Touch - Purity;
            Rate = Convert.ToDouble(txtRate.Text);

            Wst = NetWt * (diff / 100);
            if (txtBranchID.Text != "")
            {
                if ((cmbpartytype.Text != "Branch") && (cmbpartytype.Text != "Customer") && (cmbpartytype.Text != "Department"))
                {
                    if (grb_wst.Checked == true)
                    {
                        dgv_itemDetails.CurrentRow.Cells["MC"].Value = 0;
                        dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value = Wst.ToString();
                    }
                    else
                    {
                        dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value = 0;
                        MC = Wst * 100 / Convert.ToDouble(txtPurity.Text) * Rate;
                        dgv_itemDetails.CurrentRow.Cells["MC"].Value = MC.ToString();
                    }
                }
                else
                {
                    if(SoftwareSettings.CompName.ToUpper()=="REGAL" && cmbpartytype.Text == "Branch")
                    {
                        if (grb_wst.Checked == true)
                        {
                            dgv_itemDetails.CurrentRow.Cells["MC"].Value = 0;
                            dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value = Wst.ToString();
                        }
                        else
                        {
                            dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value = 0;
                            MC = Wst * 100 / Convert.ToDouble(txtPurity.Text) * Rate;
                            dgv_itemDetails.CurrentRow.Cells["MC"].Value = MC.ToString();
                        }

                    }

                    dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value = Wst.ToString();
                }
            }
        }
        public void loadgrid()
        {
            //SetColumns(dgv_itemDetails);       
            // dgv_itemDetails.IsDataEntryGrid = true;

            // dgv_itemDetails.DataFields = new List<string> { "EntryId","TransId","BranchId","BranchName","PurityId","JobNo","JobOrderID","BatchNo","BatchId", "ItemID", "ModelId","ProdCodeId","ProdCode", "[Item Name]", "Purity",
            //"Nos","Gwt","DiaNo","Diawt","DiaCash","StWt","StCash","NetWt","Rate","MetalCash","Touch", "ActualWt","PureWt","Wst","MC","CertificationCharge","Amount", "WastageWt","TotalAmount","[Mc Perc]","[Wst Perc]","IssueId","IsReceipt","PurId","SalesVA","PurchaseVA"};
            // dgv_itemDetails.HiddenDataFields = new List<string> { "EntryId", "TransId", "JobNo", "JobOrderID", "BatchId", "ItemID", "ProdCodeId", "PureWt", "PurityId", "ModelId", "[Mc Perc]", "[Wst Perc]", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "CertificationCharge", "Amount" };
            //     dgv_itemDetails.SummaryColumns = new string[] { "NetWt", "Nos", "Gwt", "StWt", "Diawt", "DiaCash", "StCash", "MC", "Wst", "WastageWt", "ActualWt", "TotalAmount", "MetalCash" };
            // dgv_itemDetails.Fill(new Table(SAFA.Classes.Common.DbName, "STK", "VGoldStockTransferDetails", true), "1=2");
            // dgv_itemDetails.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            // dgv_itemDetails.RowTemplate.Height = Cmb_ProdCode.Height;
            // dgv_itemDetails.RowTemplate.Height = CmbJobNo.Height;
            // dgv_itemDetails.RowTemplate.Height = CmbBatchNo.Height;
            // ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());
            // dgv_itemDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
            // dgv_itemDetails.BeginEdit(true);
            // AdjustColumnWidths();

        }

        void PartyTypeIdChanged()
        {
            if (cmbpartytype.SelectedValue == null)
                return;
            if (txtBranchID.Text == "" && txtcompId.Text == "")
                return;
            cmbpartyname.Text = "";
            cmbpartyname.SelectedValue = 0; linkLabel3.Visible = false;
            if (cmbpartytype.SelectedValue.ToString() == "0")
            {
                Gramboo.General.Setupcombo(cmbpartyname, "SYST.BranchMaster", "BranchName", "BranchId", "IsActive='True'");
                cmbpartyname.DropDownWidth = 196;

                Gramboo.General.Setupcombo(cmbCode, "SYST.BranchMaster", "BranchCode", "BranchId", "IsActive='True'");
            }
            else if (cmbpartytype.SelectedValue.ToString() == "1")
            {
                if (SoftwareSettings.CompName.ToUpper() == "TTD")
                {
                    Gramboo.General.Setupcombo(cmbpartyname, "PUR.SupplierMaster", "SuppName", "SuppId", "IsActive='True'AND Company_id=" + txtcompId.Text + "   AND SuppTypeId  in (1,6)");
                    cmbpartyname.DropDownWidth = 420;
                    Gramboo.General.Setupcombo(cmbCode, "PUR.SupplierMaster", "Suppcode", "SuppId", " len(Suppcode) >1 AND IsActive='True'AND Company_id=" + txtcompId.Text + "   AND SuppTypeId  in (1,6)");

                }
                else
                {
                    Gramboo.General.Setupcombo(cmbpartyname, "PUR.SupplierMaster", "SuppName", "SuppId", "IsActive='True'AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + " AND SuppTypeId  in (1,6)");
                    cmbpartyname.DropDownWidth = 420;
                    Gramboo.General.Setupcombo(cmbCode, "PUR.SupplierMaster", "Suppcode", "SuppId", " len(Suppcode) >1 AND IsActive='True'AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + " AND SuppTypeId  in (1,6)");

                }
            }
            else if (cmbpartytype.SelectedValue.ToString() == "2")
            {
                //Gramboo.General.Setupcombo(cmbpartyname, "PUR.SupplierMaster", "SuppName", "SuppId", "IsActive='True'AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + " AND SuppTypeId =6");
            }
            else if (cmbpartytype.SelectedValue.ToString() == "3")
            {
                linkLabel3.Visible = true;
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                  ("select * from SYST.BranchMaster WHERE BranchName LIKE '%CJ%' or BranchName like '%MOD%' and Branchid = '" + txtBranchID.Text + "'")).Tables[0])
                {

                    if (SoftwareSettings.CompName == "REGAL" && cmb_Purchasetype.Text == "JobWork")
                    {
                        Gramboo.General.Setupcombo(cmbpartyname, "CRM.VCustomerMaster", "custnameandcode", "custid", "IsActive='True' AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + " AND custtypeid=110100000004 ");
                        // Gramboo.General.Setupcombo(cmbCode, "CRM.VCustomerMaster", "Code", "custid", "IsActive='True' AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + " AND custtypeid=110100000004 ");

                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                        {
                            Gramboo.General.Setupcombo(cmbpartyname, "CRM.VCustomerMaster", "custnameandcode", "custid", "IsActive='True'AND Company_id=" + txtcompId.Text + "");
                        }
                        else
                        {
                            Gramboo.General.Setupcombo(cmbpartyname, "CRM.VCustomerMaster", "custnameandcode", "custid", "IsActive='True' AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + "");
                        }
                    }
                }
                cmbpartyname.DropDownWidth = 420;
            }
            else if (cmbpartytype.SelectedValue.ToString() == "4")
            {
                Gramboo.General.Setupcombo(cmbpartyname, "STK.DepartmentMaster", "DeptName", "DeptID", "IsActive='True'AND Company_id=" + txtcompId.Text + "and Branch_id=" + txtBranchID.Text + " ");
                cmbpartyname.DropDownWidth = 196;
            }

        }

        public override void Print()
        {
            if (txtEntryNo.Text != null)
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();


                if (grbissue.Checked == true)
                {



                    //rptJobworkInvoice rptJobwork = new rptJobworkInvoice();
                    //rptJobwork.SetDatabaseLogon(DBConn.ConnectionProperties.DBUsername, DBConn.ConnectionProperties.DBPassword);
                    //SAFA.Classes.Common.SetDatabaseLogon(rptJobwork, DBConn, false);
                    //rptJobwork.VerifyDatabase();
                    //rptJobwork.RecordSelectionFormula = "{VStockRpt.EntryId} =" + txtEntryNo.Text;

                    //Gramboo.Controls.GrbReport rptjobwork = new Gramboo.Controls.GrbReport(rptJobwork);
                    //rptjobwork.MdiParent = this.MdiParent;

                    //((frmMain)this.MdiParent).ShowChild(rptjobwork);

                    if (SoftwareSettings.CompName == "REGAL" && txtOne_Two.Text == "2" && txtBranchID.Text == "101" && (this.Text.ToUpper() != "BRANCH TRANSFER ISSUE"))
                    {

                        cr = new SAFA.Reports.STK.REGAL.RptGoldStockTransfer_RegaljwprintCORP2();
                        SAFA.Classes.Common.SetDatabaseLogon(cr, DBConn, false, false);
                        cr.VerifyDatabase();
                        cr.SetParameterValue("@EntryId", txtEntryNo.Text);
                        cr.SetParameterValue("@companyid", txtcompId.Text);
                        cr.SetParameterValue("@branchid", txtBranchID.Text);
                        Gramboo.Controls.GrbReport rpt1 = new Gramboo.Controls.GrbReport(cr);
                        rpt1.MdiParent = this.MdiParent;
                        ((frmMain)this.MdiParent).ShowChild(rpt1);
                    }

                    else if (SoftwareSettings.CompName == "VOGUE")
                    {
                        cr = new SAFA.Reports.STOCK.VOGUE.RptGoldStockTransferIssueVOGUE();

                    }
                    else if (SoftwareSettings.CompName == "TTD")
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferIssueTTD();
                    }
                    else if (SoftwareSettings.CompName == "Thottathil")
                    {
                        cr = new SAFA.Reports.STOCK.Thottathil.RptGoldStockTransferIssueThottathil();
                    }
                    else if (SoftwareSettings.CompName == "MANJALI")
                    {
                        cr = new SAFA.Reports.STK.MANJALI.RptGoldStockTransferIssueManjaliNew();
                    }

                    else if (SoftwareSettings.CompName == "HAYATH")
                    {
                        cr = new SAFA.Reports.STK.RptGoldStockTransferIssueHayath();
                    }
                    else if (SoftwareSettings.CompName == "JRGOLD")
                    {
                        cr = new SAFA.Reports.STOCK.JRGOLD.RptGoldStockTransferIssuejrgold();
                    }


                    else if (SoftwareSettings.CompName == "REGAL" && (this.Text.ToUpper() == "BRANCH TRANSFER ISSUE"))
                    {
                        cr = new SAFA.Reports.STK.REGAL.RptGoldStockTransfer_Regal();
                    }
                    else if (SoftwareSettings.CompName == "REGAL" && txtBranchID.Text == "101" && (this.Text.ToUpper() != "BRANCH TRANSFER ISSUE") && txtOne_Two.Text != "2")
                    {
                        cr = new SAFA.Reports.STK.REGAL.RptGoldStockTransfer_Regaljwprint();
                        //cr = new SAFA.Reports.STK.REGAL.RptGoldStockTransfer_RegaljwprintCORP();
                    }
                    else if (SoftwareSettings.CompName == "REGAL" && (this.Text.ToUpper() != "BRANCH TRANSFER ISSUE") && txtOne_Two.Text != "2")
                    {
                        cr = new SAFA.Reports.STK.REGAL.RptGoldStockTransfer_Regaljwprint();
                    }
                    else if (SoftwareSettings.CompName == "LANDMARK" )
                    {
                        
                        cr = new SAFA.Reports.STK.LANDMARK.RptGoldStockTransfer_RegaljwprintLANDMARK();
             

                        //CrystalDecisions.CrystalReports.Engine.ReportDocument cr1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        //cr1 = new SAFA.Reports.STK.LANDMARK.RptGoldStockTransferIssueLandmark();
                        //SAFA.Classes.Common.SetDatabaseLogon(cr, DBConn, false, false);
                        //cr1.VerifyDatabase();
                        //cr1.SetParameterValue("@EntryId", txtEntryNo.Text);
                        //cr1.SetParameterValue("@companyid", txtcompId.Text);  
                        //cr1.SetParameterValue("@branchid", txtBranchID.Text);
                        //Gramboo.Controls.GrbReport rpt2 = new Gramboo.Controls.GrbReport(cr1);
                        //rpt2.MdiParent = this.MdiParent;
                        //((frmMain)this.MdiParent).ShowChild(rpt2);

                    }

                    //else if (SoftwareSettings.CompName == "REGAL")
                    //{      

                    //    if (txtOne_Two.Text == "2")
                    //    {
                    //        cr = new SAFA.Reports.STK.REGAL.RptGoldStockTransfer_RegaljwprintCORP2();
                    //        cr.SetParameterValue("@EntryId", txtEntryNo.Text);
                    //        cr.SetParameterValue("@companyid", txtcompId.Text);
                    //        cr.SetParameterValue("@branchid", txtBranchID.Text);
                    //    }
                    //    else
                    //    {

                    //        if ((this.Text.ToUpper() == "BRANCH TRANSFER ISSUE"))
                    //        {
                    //            cr = new SAFA.Reports.STK.REGAL.RptGoldStockTransfer_Regal();
                    //        }

                    //        else if (SoftwareSettings.CompName == "REGAL" && txtBranchID.Text == "101")
                    //        {
                    //            cr = new SAFA.Reports.STK.REGAL.RptGoldStockTransfer_Regaljwprint();
                    //            //cr = new SAFA.Reports.STK.REGAL.RptGoldStockTransfer_RegaljwprintCORP();
                    //        }
                    //        else
                    //        {
                    //            cr = new SAFA.Reports.STK.REGAL.RptGoldStockTransfer_Regaljwprint();
                    //        }

                    //    }
                    //}
                    else if (SoftwareSettings.CompName == "SALEENA GOLD AND DIAMONDS")
                    {
                        CrystalDecisions.CrystalReports.Engine.ReportDocument cr1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        cr1 = new SAFA.Reports.STOCK.RptGoldStockTransferIssueSaleena();
                        cr1.SetDatabaseLogon(DBConn.ConnectionProperties.DBUsername, DBConn.ConnectionProperties.DBPassword);
                        SAFA.Classes.Common.SetDatabaseLogon(cr1, DBConn, false);
                        cr1.VerifyDatabase();
                        cr1.RecordSelectionFormula = "{VStockRpt.EntryId} =" + txtEntryNo.Text;
                        cr1.SetParameterValue("Heading", this.Text.ToUpper());
                        cr1.SetParameterValue("@OpnWt", Convert.ToSingle(txt_openwt.Text == "" ? "0" : txt_openwt.Text));
                        cr1.SetParameterValue("@clswt", Convert.ToSingle(txt_closewt.Text == "" ? "0" : txt_closewt.Text));
                        Gramboo.Controls.GrbReport rpt1 = new Gramboo.Controls.GrbReport(cr1);
                        rpt1.MdiParent = this.MdiParent;

                        ((frmMain)this.MdiParent).ShowChild(rpt1);
                    }
                    else if (SoftwareSettings.CompName == "PEENIKA")
                    {
                        if (this.Text.ToUpper() == "ISSUE AFTER REPAIR")
                        {
                            cr = new SAFA.Reports.STOCK.PEENIKA.RptGoldStockTransferRepairIssue();
                        }
                        else
                        {
                            cr = new SAFA.Reports.STOCK.PEENIKA.RptGoldStockTransferIssuePeenika();

                        }
                        if (SoftwareSettings.CompName == "PEENIKA")
                        {
                            CrystalDecisions.CrystalReports.Engine.ReportDocument cr1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                            cr1 = new SAFA.Reports.STOCK.PEENIKA.RptGoldStockTransferIssuePeenikaDeliveryChallan();
                            cr1.SetDatabaseLogon(DBConn.ConnectionProperties.DBUsername, DBConn.ConnectionProperties.DBPassword);
                            SAFA.Classes.Common.SetDatabaseLogon(cr1, DBConn, false);
                            cr1.VerifyDatabase();
                            cr1.RecordSelectionFormula = "{VStockRpt.EntryId} =" + txtEntryNo.Text;

                            Gramboo.Controls.GrbReport rpt1 = new Gramboo.Controls.GrbReport(cr1);
                            rpt1.MdiParent = this.MdiParent;

                            ((frmMain)this.MdiParent).ShowChild(rpt1);
                        }
                    }

                    else if (SoftwareSettings.CompName == "LOKA")
                    {
                        if (this.Text.ToUpper() == "ISSUE AFTER REPAIR")
                        {
                            cr = new SAFA.Reports.SALE.LOKA.RptGoldStockTransferIssueLokaRepair();
                        }
                        else
                        {
                            cr = new SAFA.Reports.SALE.LOKA.RptGoldStockTransferIssueLoka();
                        }

                    }

                    else if (SoftwareSettings.CompName == "BLOOMING")
                    {
                        if (this.Text.ToUpper() == "ISSUE AFTER REPAIR")
                        {
                            cr = new SAFA.Reports.SALE.BLOOMING.RptGoldStockTransferIssueLokaRepairBLOOMING();
                        }
                        else
                        {
                            cr = new SAFA.Reports.SALE.BLOOMING.RptGoldStockTransferIssueLokaBLOOMING();
                        }

                    }
                    //else if (SoftwareSettings.CompName == "SALEENA GOLD AND DIAMONDS")
                    //{
                    //    cr = new SAFA.Reports.STOCK.RptGoldStockTransferIssueSaleena();

                    //}
                    else if (SoftwareSettings.CompName == "MAHARAJA")
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferIssueMaharajaNew();

                    }

                    else if (SoftwareSettings.CompName == "MOD")
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferIssueMod();

                    }
                    else if (SoftwareSettings.CompName == "ESSJAY")
                    {
                        cr = new SAFA.Reports.STK.ESSJAY.RptGoldStockTransferIssueESSJAY();

                    }

                    else if (SoftwareSettings.CompName == "NKS")
                    {

                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferIssuenak();
                    }

                    else if (SoftwareSettings.CompName == "MANIKA" && txtBranchID.Text == "103")
                    {
                        if (this.Text.ToUpper() == "ISSUE AFTER JOBWORK")
                        {
                            cr = new SAFA.Reports.STOCK.RptGoldStockTransferIssueMANIKA();

                        }
                        else
                        {
                            cr = new SAFA.Reports.STOCK.RptGoldStockTransferIssue();
                        }


                    }
                    else if (SoftwareSettings.CompName == "SPARKLING")
                    {

                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferIssuenakSPARKLING();
                    }

                    else if (SoftwareSettings.CompName == "TAAISH")
                    {
                        cr = new SAFA.Reports.STK.TAAISH.RptGoldStockTransferIssueTAAISH();
                    }

                    else if (SoftwareSettings.CompName == "FIA")
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferIssuefFIA();
                    }

                    else if (SoftwareSettings.CompName == "POPULAR GOLD AND DIAMONDS")
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferIssuePPLR();
                    }
                    else if (SoftwareSettings.CompName == "ANASWARA")
                    {
                        cr = new SAFA.Reports.STOCK.ANASWARA.RptGoldStockTransferIssueANASWARA();
                    }
                    //else if (SoftwareSettings.CompName == "LUXURY")
                    //{
                    //    cr = new SAFA.Reports.STOCK.JIARA.RptGoldStockTransferIssueJIARA();
                    //}
                    //else if (SoftwareSettings.CompName == "LANDMARK")
                    //{
                    //    cr = new SAFA.Reports.STK.LANDMARK.RptGoldStockTransferIssueLandmark();
                    //}

                    else
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferIssue();
                    }

                  

                }


                else
                {
                    if (SoftwareSettings.CompName == "VOGUE")
                    {
                        cr = new SAFA.Reports.STOCK.VOGUE.RptGoldStockTransferVogue();
                    }
                   else if (SoftwareSettings.CompName == "TTD")
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransfertttd();
                    }
                    else if (SoftwareSettings.CompName == "PEENIKA")
                    {
                        cr = new SAFA.Reports.STOCK.PEENIKA.RptGoldStockTransferPeenika();
                    }
                    else if (SoftwareSettings.CompName == "NKS")
                    {

                        cr = new SAFA.Reports.STOCK.NKS.RptGoldStockTransferNak();
                    }
                    else if (SoftwareSettings.CompName == "MANJALI")
                    {

                        cr = new SAFA.Reports.STK.MANJALI.RptGoldStockTransferManjali();

                    }
                    else if (SoftwareSettings.CompName == "LOKA")
                    {
                        cr = new SAFA.Reports.SALE.LOKA.RptGoldStockTransferReceiptLoka();
                    }
                    else if (SoftwareSettings.CompName == "BLOOMING")
                    {
                        cr = new SAFA.Reports.SALE.BLOOMING.RptGoldStockTransferReceiptLokaBLOOMING();
                    }
                    else if (SoftwareSettings.CompName == "MANIKA" && txtBranchID.Text == "103")
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferManika();
                    }
                    else if (SoftwareSettings.CompName == "TAAISH")
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferTAAISH();
                    }
                    else if (SoftwareSettings.CompName == "REGAL" && txtOne_Two.Text == "2")
                    {
                       cr = new SAFA.Reports.STK.REGAL.RptGoldStockTransfer_RegaljwprintCORP2();
                        cr.SetParameterValue("@EntryId", txtEntryNo.Text);
                        cr.SetParameterValue("@companyid", txtcompId.Text);
                        cr.SetParameterValue("@branchid", txtBranchID.Text);
                        Gramboo.Controls.GrbReport rpt1 = new Gramboo.Controls.GrbReport(cr);
                        rpt1.MdiParent = this.MdiParent;
                    }
                    else if (SoftwareSettings.CompName == "TAAISH")
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferTAAISH();
                    }
                    else if (SoftwareSettings.CompName == "POPULAR GOLD AND DIAMONDS")
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransferPPLR();
                    }

                    //else if (SoftwareSettings.CompName == "SALEENA GOLD AND DIAMONDS")
                    //{
                    //    cr = new SAFA.Reports.STOCK.RptGoldStockTransferSaleena();

                    //}
                    else if (SoftwareSettings.CompName == "SALEENA GOLD AND DIAMONDS")





                    {
                        CrystalDecisions.CrystalReports.Engine.ReportDocument cr1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        cr1 = new SAFA.Reports.STOCK.RptGoldStockTransferSaleena();
                        cr1.SetDatabaseLogon(DBConn.ConnectionProperties.DBUsername, DBConn.ConnectionProperties.DBPassword);
                        SAFA.Classes.Common.SetDatabaseLogon(cr1, DBConn, false);
                        cr1.VerifyDatabase();
                        cr1.RecordSelectionFormula = "{VStockRpt.EntryId} =" + txtEntryNo.Text;
                        cr1.SetParameterValue("Heading", this.Text.ToUpper());
                        cr1.SetParameterValue("@OpnWt", Convert.ToSingle(txt_openwt.Text == "" ? "0" : txt_openwt.Text));
                        cr1.SetParameterValue("@clswt", Convert.ToSingle(txt_closewt.Text == "" ? "0" : txt_closewt.Text));
                        Gramboo.Controls.GrbReport rpt1 = new Gramboo.Controls.GrbReport(cr1);
                        rpt1.MdiParent = this.MdiParent;
                        ((frmMain)this.MdiParent).ShowChild(rpt1);
                    }
                    else if (SoftwareSettings.CompName == "LANDMARK")
                    {
                        CrystalDecisions.CrystalReports.Engine.ReportDocument cr1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransfer();
                        cr1.SetDatabaseLogon(DBConn.ConnectionProperties.DBUsername, DBConn.ConnectionProperties.DBPassword);
                        SAFA.Classes.Common.SetDatabaseLogon(cr1, DBConn, false);
                        cr1.VerifyDatabase();
                        cr1.RecordSelectionFormula = "{VStockRpt.EntryId} =" + txtEntryNo.Text;
                        cr1.SetParameterValue("Heading", this.Text.ToUpper());
                        cr1.SetParameterValue("@OpnWt", Convert.ToSingle(txt_openwt.Text == "" ? "0" : txt_openwt.Text));
                        cr1.SetParameterValue("@clswt", Convert.ToSingle(txt_closewt.Text == "" ? "0" : txt_closewt.Text));
                        Gramboo.Controls.GrbReport rpt1 = new Gramboo.Controls.GrbReport(cr1);
                        rpt1.MdiParent = this.MdiParent;
                        ((frmMain)this.MdiParent).ShowChild(rpt1);
                    }
                    else
                    {
                        cr = new SAFA.Reports.STOCK.RptGoldStockTransfer();
                    }
                }
                if (SoftwareSettings.CompName != "SALEENA GOLD AND DIAMONDS")
                {

                    cr.SetDatabaseLogon(DBConn.ConnectionProperties.DBUsername, DBConn.ConnectionProperties.DBPassword);
                    SAFA.Classes.Common.SetDatabaseLogon(cr, DBConn, false);
                    cr.VerifyDatabase();
                    if (SoftwareSettings.CompName != "VOGUE" && SoftwareSettings.CompName != "LOKA" && SoftwareSettings.CompName != "BLOOMING" && SoftwareSettings.CompName != "REGAL" && SoftwareSettings.CompName != "LANDMARK")
                    {
                        cr.SetParameterValue("Heading", this.Text.ToUpper());
                    }
                    //if (SoftwareSettings.CompName != "LOKA")
                    //{
                    //    cr.SetParameterValue("Heading", this.Text.ToUpper());
                    //}
                    if (SoftwareSettings.CompName == "MANJALI")
                    {


                        if (this.Text.ToUpper() == "RECEIPT AFTER JOBWORK")
                        {
                            cr.SetParameterValue("Heading1", "MATERIAL RECEIPT VOUCHER");
                        }
                        else if (this.Text.ToUpper() == "ISSUE FOR JOBWORK")
                        {
                            cr.SetParameterValue("Heading1", "ISSUE CUM DELIVERY CHALLAN FOR JOBWORK");
                        }
                        else if (this.Text.ToUpper() == "ISSUE FOR HALLMARKING")
                        {
                            cr.SetParameterValue("Heading1", "ISSUE CUM DELIVERY CHALLAN FOR HALLMARKING");
                        }
                        else
                        {
                            cr.SetParameterValue("Heading1", this.Text.ToUpper());
                        }
                    }

                    if (SoftwareSettings.CompName == "NKS" && grbreceipt.Checked == true)
                    {


                        cr.SetParameterValue("@print", "Orginal");

                    }
                    //cr.SetParameterValue("@IsContinues", false);

                    //if (SoftwareSettings.CompName == "NKS")
                    //{
                    //    cr.SetParameterValue("@print", "Orginal");
                    //}

                    cr.RecordSelectionFormula = "{VStockRpt.EntryId} =" + txtEntryNo.Text;

                    Gramboo.Controls.GrbReport rpt = new Gramboo.Controls.GrbReport(cr);
                    rpt.MdiParent = this.MdiParent;

                    ((frmMain)this.MdiParent).ShowChild(rpt);




                }



            }

            if (SoftwareSettings.CompName == "NKS" && grbissue.Checked == false)
            {
                if (txtEntryNo.Text != null)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    cr = new SAFA.Reports.STOCK.NKS.RptGoldStockTransferNak();
                    SAFA.Classes.Common.SetDatabaseLogon(cr, DBConn, false);
                    cr.VerifyDatabase();
                    cr.SetParameterValue("Heading", this.Text.ToUpper());
                    cr.SetParameterValue("@print", "Duplicate");
                    cr.RecordSelectionFormula = "{VStockRpt.EntryId} =" + txtEntryNo.Text;
                    Gramboo.Controls.GrbReport rpt = new Gramboo.Controls.GrbReport(cr);
                    rpt.MdiParent = this.MdiParent;

                    ((frmMain)this.MdiParent).ShowChild(rpt);

                }
            }

            if (SoftwareSettings.CompName == "MANJALI" && grbissue.Checked == true && cmbpartytype.Text == "Customer")
            {
                if (txtEntryNo.Text != null)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    cr = new SAFA.Reports.STK.MANJALI.ManjaliStockTransferBillWorkSheet();
                    SAFA.Classes.Common.SetDatabaseLogon(cr, DBConn, false);
                    cr.VerifyDatabase();
                    cr.RecordSelectionFormula = "{VStockRpt.EntryId} =" + txtEntryNo.Text;
                    Gramboo.Controls.GrbReport rpt = new Gramboo.Controls.GrbReport(cr);
                    rpt.MdiParent = this.MdiParent;

                    ((frmMain)this.MdiParent).ShowChild(rpt);

                }
            }



        }

        private void dgv_itemDetails_SummaryCalculated(object source, EventArgs e)
        {
            if (flag)
                return;
            txttotalpurewt.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["PureWt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["PureWt"].Text).ToString("f3");
            txttotalwastagewt.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["WastageWt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["WastageWt"].Text).ToString("f3");
            txttotalactualwt.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["ActualWt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["ActualWt"].Text).ToString("f3");
            txtTotalAmount.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["TotalAmount"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["TotalAmount"].Text).ToString("f2");
            txt_totalmc.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["MC"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["MC"].Text).ToString("f2");
            //txt_totstcash.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["StCash"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["StCash"].Text).ToString("f2");
            //txt_totdiacash.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["DiaCash"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["DiaCash"].Text).ToString("f2");
            txt_totcert.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["CertificationCharge"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["CertificationCharge"].Text).ToString("f3");
            txt_totmetalcash.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["MetalCash"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["MetalCash"].Text).ToString("f3");
            TxtTotalNetwt.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["Netwt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["Netwt"].Text).ToString("f3");
            txttotalpurewt.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["PureWt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["PureWt"].Text).ToString("f3");

            if (dgv_itemDetails.Columns.Contains("StCash"))
                txt_totstcash.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["StCash"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["StCash"].Text).ToString("f2");
            if (dgv_itemDetails.Columns.Contains("DiaCash"))
                txt_totdiacash.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["DiaCash"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["DiaCash"].Text).ToString("f2");


            if (dgv_itemDetails.Columns.Contains("StAmt"))
                txt_totstcash.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["StAmt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["StAmt"].Text).ToString("f2");
            if (dgv_itemDetails.Columns.Contains("DiaAmt"))
                txt_totdiacash.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["DiaAmt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["DiaAmt"].Text).ToString("f2");

        }

        public override bool Save()
        {
            cmbpartyname.AcceptBlankValue = false;
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
                    DialogResult d1 = Gramboo.General.ShowMessage(
                       " Cannot Find SplitupDetail file!!\n\n" +
                       " 1. Press 'Yes' to Continue without SplitupDetails \n" +
                       " 2. Press 'No' to Cancel This Action \n", "file not found", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                    if (d1 == DialogResult.No)
                    {
                        return false;
                    }
                }
            }

            DialogResult d =
                   Gramboo.General.ShowMessage(

                   " You Want to Perform This Action ? \n\n" +
                   " 1. Press 'Yes' to Save and Print \n" +
                   " 2. Press 'No' to Save Only \n" +
                   " 3. Press 'Cancel' to Cancel This Action \n", "Save", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button1);

            if (d == DialogResult.Cancel)
            {
                return false;
            }

            if (base.Save())
            {
                if (chkIsImport.Checked == true)
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
                        cmd.Parameters.AddWithValue("@RecFrom", "STK");

                        using (s = DBConn.GetData(cmd))
                        {
                            DataTable dttt =
                                   s.Tables[0];
                            if (s.Tables[0].Rows.Count > 0)
                            {
                                Gramboo.General.ShowMessage(@"The values in the import file is alredy saved in your ProdCodeMaster " +
                                      "  \n Please verify data first then try again ", "Verify Data", MessageBoxIcon.Information);
                                return false;
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
                                cmd.Parameters.AddWithValue("@RecFrom", "STK");

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
                        cmd.Parameters.AddWithValue("@RecFrom", "STK");


                        using (s = DBConn.GetData(cmd))
                        {
                            DataTable dttt =
                                   s.Tables[0];
                            if (s.Tables[0].Rows.Count > 0)
                            {
                                Gramboo.General.ShowMessage(@"The values in the import file is alredy saved in your ProdCodeMasterDetail " +
                                      "  \n Please verify data first then try again ", "Verify Data", MessageBoxIcon.Information);
                                return false;
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
                                cmd.Parameters.AddWithValue("@RecFrom", "STK");

                                DBConn.GetData(cmd, "Insert", "");
                                Gramboo.General.ShowMessage("product code Detail is successfully inserted");
                            }

                        }

                    }

                }



                if (txtOne_Two.Text != "2" && purchaseytype == "Customer Jobwork" && transtype=="0")
                {
                    if (SoftwareSettings.GenerateEInvoice == true )
                    {
                        DialogResult d1 =
                        Gramboo.General.ShowMessage(
                        " Do you want to generate e invoice ? \n\n" +
                        " 1. Press 'Yes' to generate and Print \n" +
                        " 2. Press 'No' to Cancel Only \n"
                        , "Generat Einvoice", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);
                        if (d1 == DialogResult.Yes)
                        {
                            string IRN = "", QR = "";
                            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                   ("select ISNULL(IRN,'') as IRN From SALE.SalesQrCode where SalesID=" + txtEntryNo.Text + " and RecFrom = 'Jobwork' ")).Tables[0])
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    IRN = dt.Rows[0]["IRN"].ToString();
                                }
                            }
                            if (IRN == "")
                            {
                                SAFA.Forms.GENERAL.FrmGenerateEInvoice Generat_Einvoice = new SAFA.Forms.GENERAL.FrmGenerateEInvoice();
                                Generat_Einvoice.RecFrom = "Jobwork";
                                Generat_Einvoice.txtsaleid.Text = txtEntryNo.Text;
                                Generat_Einvoice.ShowDialog();

                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandText = "[SALE].[ProcQRCodeUpDate]";
                                cmd.CommandType = CommandType.StoredProcedure;

                                MemoryStream ms = new MemoryStream();
                                SAFA.Classes.Common.GetSignedQRCode(Generat_Einvoice.QRCode).Save(ms, ImageFormat.Jpeg);
                                byte[] QRCode = new byte[ms.Length];
                                ms.Position = 0;
                                ms.Read(QRCode, 0, QRCode.Length);

                                cmd.Parameters.AddWithValue("@SalesId", txtEntryNo.Text);
                                cmd.Parameters.AddWithValue("@RecFrom", "Jobwork");
                                cmd.Parameters.AddWithValue("@qrCode", QRCode);
                                cmd.Parameters.AddWithValue("@IRN", (Generat_Einvoice.IRN == null ? "" : Generat_Einvoice.IRN));
                                cmd.Parameters.AddWithValue("@Ack_No", (Generat_Einvoice.Ack_No == null ? "" : Generat_Einvoice.Ack_No));
                                cmd.Parameters.AddWithValue("@Ack_Date", (Generat_Einvoice.Ack_Date == null ? "" : Generat_Einvoice.Ack_Date));

                                DBConn.ExecuteSqlTransaction(cmd, "QrCode");
                            }
                        }
                    }
                    else
                    {
                        //SqlCommand cmd = new SqlCommand();
                        //cmd.CommandText = "[SALE].[ProcQRCodeUpDate]";
                        //cmd.CommandType = CommandType.StoredProcedure;

                        //MemoryStream ms = new MemoryStream();
                        //SAFA.Classes.Common.GetQrCode(Convert.ToInt64(txtEntryNo.Text), DBConn).Save(ms, ImageFormat.Jpeg);
                        //byte[] photo_aray = new byte[ms.Length];
                        //ms.Position = 0;
                        //ms.Read(photo_aray, 0, photo_aray.Length);

                        //cmd.Parameters.AddWithValue("@SalesId", txtEntryNo.Text);
                        //cmd.Parameters.AddWithValue("@RecFrom", "Jobwork");
                        //cmd.Parameters.AddWithValue("@qrCode", photo_aray);
                        //cmd.Parameters.AddWithValue("@IRN", "");
                        //cmd.Parameters.AddWithValue("@Ack_No", "");
                        //cmd.Parameters.AddWithValue("@Ack_Date", "");

                        //DBConn.ExecuteSqlTransaction(cmd, "QrCode");
                    }
                }



                if (GenerateEway)
                {

                    string IRN = "", QR = "";
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                           ("select ISNULL(IRN,'') as IRN From SALE.SalesQrCode where SalesID=" + txtEntryNo.Text + " and RecFrom = 'StockEway' ")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            IRN = dt.Rows[0]["IRN"].ToString();
                        }
                    }

                    SAFA.Forms.GENERAL.FrmGenerateEWay Generat_Eway = new SAFA.Forms.GENERAL.FrmGenerateEWay();
                    Generat_Eway.RecFrom = "StockEway";
                    Generat_Eway.txtsaleid.Text = txtEntryNo.Text;
                    Generat_Eway.ShowDialog();


                    if (Generat_Eway.IRN != "")
                    {

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "[SALE].[ProcQRCodeUpDate]";
                        cmd.CommandType = CommandType.StoredProcedure;

                        byte[] data = new byte[0];

                        cmd.Parameters.AddWithValue("@SalesId", txtEntryNo.Text);
                        cmd.Parameters.AddWithValue("@RecFrom", "StockEway");
                        cmd.Parameters.AddWithValue("@qrCode", data);
                        cmd.Parameters.AddWithValue("@IRN", (Generat_Eway.IRN == null ? "" : Generat_Eway.IRN));
                        cmd.Parameters.AddWithValue("@Ack_No", (Generat_Eway.Ack_No == null ? "" : Generat_Eway.Ack_No));
                        cmd.Parameters.AddWithValue("@Ack_Date", (Generat_Eway.Ack_Date == null ? "" : Generat_Eway.Ack_Date));

                        DBConn.ExecuteSqlTransaction(cmd, "QrCode");

                    }
                }


                SendData();
                if (CopperAdded == true) { AddToStockConvertion("Save", Convert.ToInt64(txtEntryNo.Text)); }
                if (d == DialogResult.Yes)
                {
                    Print();
                }

                if (Message == true)
                {
                    if (SoftwareSettings.IsFile)
                    {
                        DialogResult R = Gramboo.General.ShowMessage("Do you have any files to upload?", "Upload", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                        if (R == DialogResult.Yes)
                        {
                            SAFA.Forms.COM.DocumentUploader du = new SAFA.Forms.COM.DocumentUploader();
                            du.CloseClick += new SAFA.Forms.COM.DocumentUploader.CloseClickEventHandler(Closed);
                            du.RecTable = "STK.StockTransferMaster";
                            du.RecId = txtEntryNo.Text;
                            du.ShowDialog();
                            return false;
                        }
                    }

                }
                Message = true;

                if (BatchInbox != null)
                {
                    BatchInbox.populategrid();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Closed(object sender, SAFA.Forms.COM.DocumentUploader.CloseClickEventArgs e)
        {
            Init();
        }
        private void CalculatePureWt()
        {
            Double purityValue = 0, NetWt = 0, PureWt = 0, dflttouch = 100, touch = 0, Wst = 0;
            if (dgv_itemDetails.CurrentRow == null)
                return;
            using (DataTable dt = DBConn.GetData(new SqlCommand("Select [Purity Value] FROM ITM.VPurityMaster WHERE PurityId=" + dgv_itemDetails.CurrentRow.Cells["PurityId"].Value + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    purityValue = Convert.ToDouble(dt.Rows[0]["Purity Value"].ToString());
                }
            }

            if (dgv_itemDetails.CurrentRow.Cells["NetWt"].Value.ToString() != "")
            {
                NetWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["NetWt"].Value.ToString());
            }
            if (dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString() != "")
            {
                touch = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString());
            }
            if (dgv_itemDetails.CurrentRow.Cells["Wst"].Value.ToString() != "")
            {
                Wst = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Wst"].Value.ToString());
            }


            PureWt = ((NetWt * touch / 100) + Wst);


            dgv_itemDetails.CurrentRow.Cells["PureWt"].Value = Convert.ToDouble(PureWt.ToString(RoundTo));



        }
        private void dgv_itemDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (flag)
                return;
            if (e.ColumnIndex == dgv_itemDetails.Columns["ProdCode"].Index)
            {

               

                string targetValue = dgv_itemDetails.Rows[e.RowIndex].Cells["Prodcode"].Value.ToString();
                int columnIndex = dgv_itemDetails.Columns["ProdCodeId"].Index; 
                bool valueExists = false;
                if (targetValue.Length > 1)
                {
                    foreach (DataGridViewRow row in dgv_itemDetails.Rows)
                    {
                        if  (e.RowIndex !=row.Index && row.Cells["ProdCode"].Value != null && row.Cells["ProdCode"].Value.ToString() == targetValue)
                        {
                            valueExists = true;
                            break;
                        }
                    }

                    if (valueExists)
                    {
                        MessageBox.Show("Already Scanned");
                    }

                    if (!valueExists)
                    {
                        using (DataTable dtprodcode = DBConn.GetData(new SqlCommand("SELECT  STK.GetProdcodeIdFromProdCode('" + (dgv_itemDetails.Rows[e.RowIndex].Cells["Prodcode"].Value.ToString() == "" ? "0" : dgv_itemDetails.Rows[e.RowIndex].Cells["Prodcode"].Value.ToString()) + "'," + txtBranchID.Text + ")  "), "tbl").Tables[0])
                        // using (DataTable dtprodcode = DBConn.GetData(new SqlCommand("select prodcodeid from stk.prodcodemaster where prodcode='" + dgv_itemDetails.Rows[e.RowIndex].Cells["Prodcode"].Value.ToString() + "'"), "tbl").Tables[0])
                        {



                            //if (IsEditMode == false && grbreceipt.Checked == true)
                            //{
                            //    using (System.Data.DataTable dtcheck = DBConn.GetData(new SqlCommand
                            //                ("select Wt from stk.OrnamentsStatusForProdcodeId('" + dtp_dt.Value.ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + "," + dtprodcode.Rows[0][0].ToString() + ") where Wt>0")).Tables[0])
                            //    {

                            //        if (dtcheck.Rows.Count > 0)
                            //        {
                            //            Gramboo.General.ShowMessage("Item already in Stock.");


                            //        }
                            //        else
                            //        {
                            //            dgv_itemDetails.Rows[e.RowIndex].Cells["ProdcodeId"].Value = Convert.ToInt64(dtprodcode.Rows[0][0].ToString());
                            //            purity();
                            //        }
                            //    }
                            //}
                            //else
                            //{
                                dgv_itemDetails.Rows[e.RowIndex].Cells["ProdcodeId"].Value = Convert.ToInt64(dtprodcode.Rows[0][0].ToString());
                                purity();
                            //}





                        }
                    }
                }

            }
            else if (e.ColumnIndex == dgv_itemDetails.Columns["ProdCodeId"].Index)
            {
                if (dgv_itemDetails[e.ColumnIndex, e.RowIndex].Value != null && dgv_itemDetails[e.ColumnIndex, e.RowIndex].Value.ToString() != "0")
                {
                    flag = true;

                    FillRowWithProdcode(dgv_itemDetails, e.RowIndex, "S", 0, (Int64)dgv_itemDetails.Rows[e.RowIndex].Cells["ProdcodeId"].Value, false);
                    flag = false;
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

                    enable_disable(dgv_itemDetails, e.RowIndex, true);

                    if (SAFA.Classes.Common.AllowBarcodeEdit(DBConn, Convert.ToInt16(txtBranchID.Text)))
                    {
                        dgv_itemDetails.CurrentRow.Cells["Gwt"].ReadOnly = false;
                        dgv_itemDetails.CurrentRow.Cells["Diawt"].ReadOnly = false;
                        //dgv_itemDetails.CurrentRow.Cells["DiaCash"].ReadOnly = false;
                        dgv_itemDetails.CurrentRow.Cells["StWt"].ReadOnly = false;
                        //dgv_itemDetails.CurrentRow.Cells["StCash"].ReadOnly = false;
                        dgv_itemDetails.CurrentRow.Cells["MetalCash"].ReadOnly = false;
                        //dgv_itemDetails.CurrentRow.Cells["MetalRate"].ReadOnly = false;
                    }
                    else

                    {
                        dgv_itemDetails.CurrentRow.Cells["Nos"].ReadOnly = true;
                        //dgv_itemDetails.CurrentRow.Cells["ItemName"].ReadOnly = true;
                        dgv_itemDetails.CurrentRow.Cells["Gwt"].ReadOnly = true;
                        dgv_itemDetails.CurrentRow.Cells["Diawt"].ReadOnly = true;
                        //dgv_itemDetails.CurrentRow.Cells["DiaCash"].ReadOnly = true;
                        dgv_itemDetails.CurrentRow.Cells["StWt"].ReadOnly = true;
                        //dgv_itemDetails.CurrentRow.Cells["StCash"].ReadOnly = true;
                        dgv_itemDetails.CurrentRow.Cells["MetalCash"].ReadOnly = true;
                        //dgv_itemDetails.CurrentRow.Cells["MetalRate"].ReadOnly = true;
                    }

                    if (dgv_itemDetails.Rows.Count - 1 == e.RowIndex)
                        ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());
                    dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[e.RowIndex + 1].Cells[0];

                    //dgv_itemDetails.BeginEdit(true); 
                    //dgv_itemDetails.Focus();
                    //if (SoftwareSettings.CompName != "WESTERN" || SoftwareSettings.CompName != "HAYATH")
                    //{
                    //    CalculateActualWtwhileAdding();
                    //}
                }
                else
                {
                    enable_disable(dgv_itemDetails, e.RowIndex, false);
                }

            }

            else if (e.ColumnIndex == dgv_itemDetails.Columns["BatchId"].Index)
            {
                if (dgv_itemDetails.CurrentRow.Cells["BatchId"].Value.ToString().Trim().Length >= 2)
                {
                    dgv_itemDetails.CurrentRow.Cells["Gwt"].ReadOnly = false;
                    using (DataTable dt = DBConn.GetData(new SqlCommand
                    ("select ItemId,ItemName from [PROD].[FunBatchIdPending]('" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "','P'," + txtcompId.Text + " ," + txtBranchID.Text + ")where  Netwt>0 and BatchId=" + dgv_itemDetails.CurrentRow.Cells["BatchId"].Value + "")).Tables[0])
                    {
                        if (dt.Rows.Count == 1)
                        {
                            string ItemName = dt.Rows[0]["ItemName"].ToString();
                            String itemId = dt.Rows[0]["ItemId"].ToString();
                            dgv_itemDetails.CurrentRow.Cells["ItemId"].Value = itemId;
                            dgv_itemDetails.CurrentRow.Cells["Item Name"].Value = ItemName;
                        }
                        else
                        {
                            if (cmb_Purchasetype.Text == "JobWork")
                            {
                                using (DataTable dt1 = DBConn.GetData(new SqlCommand
                              ("select * From PROD.BatchCreation where BranchId!=" + txtBranchID.Text + " and Branch_id=" + txtBranchID.Text + " and BatchId=" + dgv_itemDetails.CurrentRow.Cells["BatchId"].Value + "")).Tables[0])
                                {
                                    if (dt1.Rows.Count == 1)
                                    {
                                    }
                                    else
                                    {
                                        Cmb_ItemName.DataSource = null;
                                        string str;
                                        SqlCommand cmd = new SqlCommand();
                                        str = "Select ItemId,ItemName  from [PROD].[FunBatchIdPending]('" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "','P'," + txtcompId.Text + " ," + txtBranchID.Text + ")where  Netwt>0 and BatchId=" + dgv_itemDetails.CurrentRow.Cells["BatchId"].Value + "";
                                        cmd.CommandText = str;
                                        Cmb_ItemName.DisplayMember = "ItemName";
                                        Cmb_ItemName.ValueMember = "ItemId";
                                        Cmb_ItemName.DataSource = DBConn.GetData(cmd, "ItemName").Tables[0];

                                    }
                                }
                            }
                            else if (cmb_Purchasetype.Text == "Refinery" && grbissue.Checked == true)
                            {
                                Cmb_ItemName.DataSource = null;
                                string str;
                                SqlCommand cmd = new SqlCommand();
                                str = "Select ItemId,ItemName  from [PROD].[FunBatchIdPending]('" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "','P'," + txtcompId.Text + " ," + txtBranchID.Text + ")where  Netwt>0 and BatchId=" + dgv_itemDetails.CurrentRow.Cells["BatchId"].Value + "";
                                cmd.CommandText = str;
                                Cmb_ItemName.DisplayMember = "ItemName";
                                Cmb_ItemName.ValueMember = "ItemId";
                                Cmb_ItemName.DataSource = DBConn.GetData(cmd, "ItemName").Tables[0];
                            }
                        }
                    }


                }
            }
            else if (e.ColumnIndex == dgv_itemDetails.Columns["Itemid"].Index && dgv_itemDetails.CurrentRow.Cells["BatchId"].Value.ToString().Trim().Length >= 2)
            {
                if (dgv_itemDetails[e.ColumnIndex, e.RowIndex].Value != null && dgv_itemDetails[e.ColumnIndex, e.RowIndex].Value.ToString() != "0")
                {
                    if (dgv_itemDetails.CurrentRow.Cells["BatchId"].Value.ToString().Trim().Length >= 2)
                    {
                        flag = true;

                        FillRowWithBatchid(dgv_itemDetails, e.RowIndex, "S", 0, (Int64)dgv_itemDetails.Rows[e.RowIndex].Cells["BatchId"].Value, (Int64)dgv_itemDetails.Rows[e.RowIndex].Cells["ItemId"].Value, false);
                        dgv_itemDetails.CurrentRow.Cells["IssueWt"].ReadOnly = true;

                        flag = false;

                        if (grbreceipt.Checked == false)
                        {
                            //dgv_itemDetails.CurrentRow.Cells["IssueWt"].Value = 0;
                            if (cmb_Purchasetype.Text != "Result")
                            {
                                dgv_itemDetails.CurrentRow.Cells["TestResult"].Value = 0;
                            }
                            if (cmb_Purchasetype.Text != "Melted OG")
                            {
                                if (cmb_Purchasetype.SelectedIndex.ToString() == "1" && dgv_itemDetails.CurrentRow.Cells["Item Name"].Value.ToString().ToUpper() == "THANKAM")
                                {
                                }
                                else
                                {
                                    dgv_itemDetails.CurrentRow.Cells["Loss"].Value = 0;
                                }
                            }
                        }

                        if (grbreceipt.Checked == true)
                        {
                            dgv_itemDetails.CurrentRow.Cells["GWt"].Value = "0";
                            dgv_itemDetails.CurrentRow.Cells["Nos"].Value = "0";
                            dgv_itemDetails.CurrentRow.Cells["Netwt"].Value = "0";
                            dgv_itemDetails.CurrentRow.Cells["TestResult"].Value = "0";
                            dgv_itemDetails.CurrentRow.Cells["Loss"].Value = "0";
                            if (cmb_Purchasetype.Text != "Melting")
                            {
                                if (cmb_Purchasetype.Text != "Test" && cmb_Purchasetype.Text != "Refinery")
                                {
                                    dgv_itemDetails.CurrentRow.Cells["IssueWt"].Value = 0;
                                }
                            }
                            //boxitem = true;                                    
                        }



                        if (grbissue.Checked == true && cmb_Purchasetype.Text != "Test")
                        {
                            enable_disable(dgv_itemDetails, e.RowIndex, true);
                        }
                        if (SAFA.Classes.SoftwareSettings.IsShowRoom == false && grbissue.Checked == true && cmb_Purchasetype.SelectedIndex.ToString() == "1")
                        {
                            enable_disable(dgv_itemDetails, e.RowIndex, false);
                        }
                        if (dgv_itemDetails.Rows.Count - 1 == e.RowIndex)
                            ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());
                        dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[e.RowIndex + 1].Cells[0];
                        //dgv_itemDetails.BeginEdit(true);

                        //dgv_itemDetails.Focus();                    
                    }
                }
                else
                {
                    enable_disable(dgv_itemDetails, e.RowIndex, false);
                }

            }

            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
          dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StWt" ||
          dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Diawt")
            {
                purity();
                CalculateWt(dgv_itemDetails);
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
                      dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StWt" ||
                dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Rate")
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "StWt")
                {
                    StoneCtwt(dgv_itemDetails);
                }

                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "Rate")
                {
                    MetalCash(dgv_itemDetails, Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Rate"].Value));
                }

                if (SoftwareSettings.CompName == "PEENIKA" && cmb_Purchasetype.Text == "Repair" && grbissue.Checked == true && cmbpartytype.Text == "Customer")
                {
                    if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Rate")
                    {
                        dgv_itemDetails.CurrentRow.Cells["MetalCash"].Value = Math.Abs(Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Gwt"].Value.ToString()) - Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["IssueWt"].Value.ToString())) * Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Rate"].Value.ToString());
                    }
                }
                //MetalCash(dgv_itemDetails);
                CalculateMc();

            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "MCRate" && (SoftwareSettings.CompName == "POPULAR GOLD AND DIAMONDS" || SoftwareSettings.CompName == "ESSJAY"  || SoftwareSettings.CompName == "WESTERN"))
            {
                CalculateMc();
            }

            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StCtWt")
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "StCtWt")
                {
                    StoneCtwtToWT(dgv_itemDetails);
                }
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Mud")
            {
                CalLossWeight();
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StRate")
            {
                StoneCash(dgv_itemDetails);
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "MCRate")
            {
                if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "MCRate")
                {
                    MC_Cash(dgv_itemDetails);
                }
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "NetWt" ||
                dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Touch")
            {


                CalculatePureWt();

                CalculateActualWt();
                // CalculateWastage();
                if (CalcMcRate == false)
                {
                    CalculateWst();

                    CalLossWeight();
                    MetalCash(dgv_itemDetails, 0);
                    CalculatedWstPerc();
                }
                CalculateWastagewt();
                //if (cmbpartytype.Text == "Supplier")
                //{

                //}
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "ItemID")
            {
                bool boxitem = false;

                using (DataTable dt = DBConn.GetData(new SqlCommand("Select PurityID,[Purity Name],[Purity Value],BoxItem FROM ITM.VitemMaster WHERE itemid=" + dgv_itemDetails.CurrentRow.Cells["ItemId"].Value + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        dgv_itemDetails.CurrentRow.Cells["PurityID"].Value = dt.Rows[0]["PurityID"];
                        dgv_itemDetails.CurrentRow.Cells["Purity"].Value = dt.Rows[0]["Purity Name"];
                        dgv_itemDetails.Columns["IssueWt"].ReadOnly = true;
                        if (Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString() == "" ? "0" : dgv_itemDetails.CurrentRow.Cells["Touch"].Value) == 0)
                        {
                            dgv_itemDetails.CurrentRow.Cells["Touch"].Value = dt.Rows[0]["Purity Value"];
                        }
                        boxitem = (dt.Rows[0]["BoxItem"].ToString() == "" ? false : Convert.ToBoolean(dt.Rows[0]["BoxItem"].ToString()));
                    }
                }

                dgv_itemDetails.Focus();
                if (boxitem)
                {
                    using (DataTable dr = DBConn.GetData(new SqlCommand("select ISNULL(min(prodCodeId),0) as prodCodeId from STK.VProdCodeMaster where ItemID=" + dgv_itemDetails.Rows[e.RowIndex].Cells["Itemid"].Value.ToString() + " AND BoxItem=1 AND Branch_id=" + txtBranchID.Text)).Tables[0])
                    {
                        if (dr.Rows.Count != 0)
                        {

                            dgv_itemDetails.Rows[e.RowIndex].Cells["ProdCodeId"].Value = Convert.ToInt64(dr.Rows[0][0].ToString());


                        }
                    }
                }
                //dgv_itemDetails.Columns["GWt"].ReadOnly = false;
                //dgv_itemDetails.Columns["Nos"].ReadOnly = false;
                //dgv_itemDetails.Columns["StWt"].ReadOnly = false;
                if (cmbpartyname.SelectedValue != null)
                {
                    //using (DataTable dt = DBConn.GetData(new SqlCommand("Select  touch FROM [PUR].[vSupplierItemdetails] WHERE itemid=" + dgv_itemDetails.CurrentRow.Cells["ItemId"].Value + " and SuppId=" + cmbpartyname.SelectedValue)).Tables[0])
                    //{
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        if (Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString() == "" ? "0" : dgv_itemDetails.CurrentRow.Cells["Touch"].Value) == 0)
                    //        {
                    //            dgv_itemDetails.CurrentRow.Cells["touch"].Value = dt.Rows[0]["touch"];
                    //        }

                    //    }
                    //}
                }

            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Wst" ||
                 dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Touch")
            {
                CalculatePureWt();
                CalculateWastagewt();

            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "WastageWt" ||
            dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "PureWt")
            {
                CalculateActualWt();
                CalculateMc();
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Loss")
            {
                CalculateActualWt();
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "MetalCash" ||
            dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "CertificationCharge" ||
            dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "MC" ||
            dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "DiaCash" ||
            dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "StCash"
            )
            {
                if (dgv_itemDetails.CurrentCell != null)
                {
                    if (dgv_itemDetails.CurrentCell.OwningColumn.Name == "MC")
                    {
                        CalculateMcRate();
                    }
                    //Totalamt();
                }
                CalBatchTotalAmount();
                CalTotalAmount();
                Cmb_ProdCode.Focus();
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "ActualWt")
            {
                CalBatchTotalAmount();
            }
            else if (dgv_itemDetails.Columns[e.ColumnIndex].DataPropertyName == "Wst Perc")
            {
                CalculateMc();
            }

        }
        //private void dgv_itemDetails_SummaryCalculated(object source, EventArgs e)
        //{


        //    //    txttotalactualwt.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["ActualWt"].Text).ToString("f3");
        //    //    txttotalpurewt.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["PureWt"].Text).ToString("f3");
        //    //    txttotalcertificationcharge.Text =dgv_itemDetails.SummaryRow.SummaryCells["CertificationCharge"].Text;
        //    //   txt_Totalamount.Text =Convert.ToDouble( dgv_itemDetails.SummaryRow.SummaryCells["TotalAmount"].Text).ToString("f2");
        //}


        private void FillRowWithProdcode(Gramboo.Controls.GrbDataGridView dgv, int rowindex, string type, Int64 floorid, Int64 prodcodeid, bool Isreceipt)
        {

            int branchid = 0;
            string branchname = "";


            branchid = Convert.ToInt32(dgv.Rows[rowindex].Cells["BranchID"].Value.ToString() == "" ? "0" : dgv.Rows[rowindex].Cells["BranchID"].Value.ToString());
            branchname = dgv.Rows[rowindex].Cells["BranchName"].Value.ToString() == "" ? "" : dgv.Rows[rowindex].Cells["BranchName"].Value.ToString();

            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                     ("SELECT top 1 * FROM STK.AddtoStockTransferIssue( '" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "' ," + prodcodeid + "," + txtcompId.Text + "," + txtBranchID.Text + ",'" + grb.Value + "',0,'" + grb.Value + "')")).Tables[0])
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

        private void CalculateWastage()
        {
            Double NetWt = 0, wstperc = 0, Wst = 0, touch = 0, purityValue = 0;
            if (dgv_itemDetails.CurrentRow == null)
                return;

            //if (dgv_itemDetails.CurrentRow.Cells["Wst Perc"].Value.ToString() != "")
            //{
            //    wstperc = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Wst Perc"].Value.ToString());
            //}
            if (dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString() != "")
            {
                touch = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString());
            }
            if (dgv_itemDetails.CurrentRow.Cells["NetWt"].Value.ToString() != "")
            {
                NetWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["NetWt"].Value.ToString());
            }
            using (DataTable dt = DBConn.GetData(new SqlCommand("Select [Purity Value] FROM ITM.VPurityMaster WHERE PurityId=" + dgv_itemDetails.CurrentRow.Cells["PurityId"].Value + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    purityValue = Convert.ToDouble(dt.Rows[0]["Purity Value"].ToString());
                }
            }
            Wst = (touch - purityValue) * NetWt / purityValue;
            dgv_itemDetails.CurrentRow.Cells["Wst"].Value = Wst.ToString("f2");
        }
        private void CalculateWastagewt()
        {
            Double purityValue = 0, Wst = 0, wstwt = 0, dflttouch = 100, touch = 0, NetWt = 0;
            if (dgv_itemDetails.CurrentRow == null)
                return;
            //if (dgv_itemDetails.CurrentRow.Cells["PurityId"].Value.ToString() != "")
            //{
            using (DataTable dt = DBConn.GetData(new SqlCommand("Select [Purity Value] FROM ITM.VPurityMaster WHERE PurityId=" + dgv_itemDetails.CurrentRow.Cells["PurityId"].Value + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    purityValue = Convert.ToDouble(dt.Rows[0]["Purity Value"].ToString());
                }
            }
            //  }

            if (cmbpartytype.SelectedValue.ToString() == "1" || cmbpartytype.SelectedValue.ToString() == "2")
            {
                if (txtBranchID.Text == "")
                    return;
                if (cmbpartyname.SelectedValue == null)
                {
                    cmbpartyname.ShowMessage("Select Part Name");
                    return;
                }
                using (DataTable dt1 = DBConn.GetData(new SqlCommand("Select SuppDefltTouch FROM PUR.SupplierMaster WHERE SuppId=" + cmbpartyname.SelectedValue + "  ")).Tables[0])
                {
                    if (dt1.Rows.Count > 0)
                    {
                        dflttouch = Convert.ToDouble(dt1.Rows[0]["SuppDefltTouch"].ToString());
                    }
                }
            }
            if (dgv_itemDetails.CurrentRow.Cells["NetWt"].Value.ToString() != "")
            {
                NetWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["NetWt"].Value.ToString());
            }
            if (dgv_itemDetails.CurrentRow.Cells["Wst"].Value.ToString() != "")
            {
                Wst = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Wst"].Value.ToString());
            }
            if (dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString() != "")
            {
                touch = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString());
            }
            // txt_touch.Text = dflttouch.ToString("f2");
            wstwt = (((touch - purityValue) * NetWt / 100) + Wst);
            dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value = wstwt.ToString();
        }
        private void cmb_purity_KeyDown(object sender, KeyEventArgs e)
        {
            ComboKeydown(dgv_itemDetails, cmb_purity, e);

        }

        private void cmb_purity_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_itemDetails, cmb_purity, dgv_itemDetails.Columns["PurityId"].Index);
        }

        private void cmbbranch_KeyDown(object sender, KeyEventArgs e)
        {
            ComboKeydown(dgv_itemDetails, cmbbranch, e);
        }

        private void cmbbranch_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_itemDetails, cmbbranch, dgv_itemDetails.Columns["BranchId"].Index);
        }

        private void Cmb_ItemName_KeyDown(object sender, KeyEventArgs e)
        {
            ComboKeydown(dgv_itemDetails, Cmb_ItemName, e);
        }

        private void Cmb_ItemName_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_itemDetails, Cmb_ItemName, dgv_itemDetails.Columns["ItemID"].Index);
            //using (DataTable dt1 = DBConn.GetData(new SqlCommand("select PurityId,PurityName,PurityValue from PUR.SupPurity where SuppId= " + (cmbpartyname.SelectedValue == null ? "0" : cmbpartyname.SelectedValue))).Tables[0])
            //{
            //    if (dt1.Rows.Count > 0)
            //    {

            //        dgv_itemDetails.CurrentRow.Cells["PurityID"].Value = dt1.Rows[0]["PurityId"] == DBNull.Value ? "0" : dt1.Rows[0]["PurityId"];
            //        dgv_itemDetails.CurrentRow.Cells["Purity"].Value = dt1.Rows[0]["PurityName"] == DBNull.Value ? "0" : dt1.Rows[0]["PurityName"];
            //        dgv_itemDetails.CurrentRow.Cells["Touch"].Value = dt1.Rows[0]["PurityValue"] == DBNull.Value ? "0" : dt1.Rows[0]["PurityValue"];
            //    }
            //}
            //AfterComboLeave(dgv_itemDetails, cmb_purity, dgv_itemDetails.Columns["Purity"].Index);
        }

        private void dgv_itemDetails_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_itemDetails.CurrentCell != null)
            {
                if (dgv_itemDetails.SelectedCells.Count > 0)
                {
                    SaleCurrentRow = dgv_itemDetails.SelectedCells[0].RowIndex;

                    if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "ProdCode")
                    {
                        SetComboLocation(dgv_itemDetails, Cmb_ProdCode, dgv_itemDetails.CurrentCell.ColumnIndex, dgv_itemDetails.CurrentCell.RowIndex);
                    }

                    if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "BatchNo")
                    {
                        SetComboLocation(dgv_itemDetails, CmbBatchNo, dgv_itemDetails.CurrentCell.ColumnIndex, dgv_itemDetails.CurrentCell.RowIndex);

                    }

                    if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "JobNo")
                    {
                        SetComboLocation(dgv_itemDetails, CmbJobNo, dgv_itemDetails.CurrentCell.ColumnIndex, dgv_itemDetails.CurrentCell.RowIndex);

                    }

                    if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Item Name")
                    {
                        SetComboLocation(dgv_itemDetails, Cmb_ItemName, dgv_itemDetails.CurrentCell.ColumnIndex, dgv_itemDetails.CurrentCell.RowIndex);
                        Cmb_ProdCode.Focus();
                    }


                    else if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Purity")
                    {
                        SetComboLocation(dgv_itemDetails, cmb_purity, dgv_itemDetails.CurrentCell.ColumnIndex, dgv_itemDetails.CurrentCell.RowIndex);
                    }

                    else if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "BranchName")
                    {
                        SetComboLocation(dgv_itemDetails, cmbbranch, dgv_itemDetails.CurrentCell.ColumnIndex, dgv_itemDetails.CurrentCell.RowIndex);
                    }
                    else if (dgv_itemDetails.SelectedCells[0].OwningColumn.DataPropertyName == "Reference No")
                    {
                        SetComboLocation(dgv_itemDetails, CmbRefNo, dgv_itemDetails.CurrentCell.ColumnIndex, dgv_itemDetails.CurrentCell.RowIndex);
                    }

                }
                else
                {
                    Cmb_ProdCode.Visible = false;
                    SaleCurrentRow = -1;
                }
            }
        }

        public void CalculateWt(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double NetWt = 0, StoneWt = 0, DiaWt = 0, DiaStWt = 0, TotalWt = 0, Gwt = 0, StConwt = 0, DiaConwt = 0;
            String StUnitOfMesurment = "", DiaUnitOfMesurment = "";

            if (dgv.CurrentRow == null)
                return;

            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                 ("SELECT StUnitOfMesurment,DiaUnitOfMesurment FROM ITM.VItemMaster as t1 WHERE t1.ItemId=" + dgv.CurrentRow.Cells["ItemId"].Value.ToString() + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    StUnitOfMesurment = dt.Rows[0]["StUnitOfMesurment"].ToString();
                    DiaUnitOfMesurment = dt.Rows[0]["DiaUnitOfMesurment"].ToString();
                }
            }

            if (dgv.CurrentRow.Cells["Gwt"].Value.ToString() != "")
            {
                Gwt = Convert.ToSingle(dgv.CurrentRow.Cells["Gwt"].Value.ToString());
            }

            if (dgv.CurrentRow.Cells["StWt"].Value.ToString() != "")
            {
                StConwt = Convert.ToSingle(dgv.CurrentRow.Cells["StWt"].Value.ToString());

                if (StUnitOfMesurment == "Cent")
                {
                    StoneWt = Math.Round(StConwt, 3) / 500;
                }
                else if (StUnitOfMesurment == "Carat")
                {
                    StoneWt = Math.Round(StConwt, 3) * 0.2;
                }
                else if (StUnitOfMesurment == "Gram")
                {
                    StoneWt = StConwt;
                }
            }

            if (dgv.CurrentRow.Cells["DiaWt"].Value.ToString() != "")
            {
                DiaConwt = Convert.ToSingle(dgv.CurrentRow.Cells["DiaWt"].Value.ToString());

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

            //     DiaStWt = Convert.ToSingle((StoneWt + (DiaWt * .2)));
            TotalWt = Gwt - StoneWt - (DiaWt * .2f);
            dgv.CurrentRow.Cells["NetWt"].Value = TotalWt.ToString(RoundTo);
        }

        private void dgv_itemDetails_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgv_itemDetails.RowTemplate.Height = Cmb_ItemName.Height;
            foreach (DataGridViewColumn c in dgv_itemDetails.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            if (dgv_itemDetails.RowCount <= 0) { Cmb_StkType.Enabled = true; }
            StkTyeCustomer();
        }

        private void dgv_itemDetails_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == 13 && dgv_itemDetails.CurrentCell.OwningColumn.DisplayIndex == dgv_itemDetails.Columns.Count - 1)
            {

                if (dgv_itemDetails.Rows.Count > 0)
                {
                    if (dgv_itemDetails.CurrentRow.Cells["ItemID"].Value.ToString() != "0" && dgv_itemDetails.CurrentRow.Index == dgv_itemDetails.Rows.Count - 1)
                    {

                        ((System.Data.DataTable)dgv_itemDetails.DataSource).Rows.Add(((System.Data.DataTable)dgv_itemDetails.DataSource).NewRow());

                        dgv_itemDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        dgv_itemDetails.CurrentCell = dgv_itemDetails.Rows[dgv_itemDetails.Rows.Count - 1].Cells[0];
                        e.Handled = true;
                        dgv_itemDetails.BeginEdit(false);
                        //Cmb_ItemName.DroppedDown = true;  
                        //dgv_itemDetails.Focus();
                        // SetComboLocation(dgv_itemDetails, Cmb_ProdCode_SDtl, dgv_itemDetails.CurrentCell.OwningColumn.Index, dgv_itemDetails.CurrentCell.RowIndex);

                    }
                }

            }
            else if (e.KeyChar == 19)
            {
                txtOne_Two.Visible = true;
                txtOne_Two.Focus();
            }

        }

        private void grbreceipt_CheckedChanged(object sender, EventArgs e)
        {
            if (dgv_itemDetails.DataSource == null)
                return;
            if (grbreceipt.Checked == true)
            {
                BtnLoad.Visible = false;
                //dgv_itemDetails.Columns["ProdCode"].Visible = false;
                dgv_itemDetails.Columns["JobNo"].Visible = false;

            }
            else
            {
                //if (grb.Value != "")
                //{
                //    dgv_itemDetails.Columns["ProdCode"].Visible = true;
                //}
                ChkJobOrderNo.Checked = false;
                BtnLoad.Visible = true;
            }


            //if(cmb_Purchasetype.Text=)

        }


        private void CalculateActualWt()
        {
            Double PureWt = 0, WastageWt = 0, ActualWt = 0, NetWt = 0, purityValue = 0, touch = 0, defaulttouch = 0, wst = 0, Loss = 0;
            defaulttouch = Convert.ToDouble(txt_touch.Text == "" ? "100" : txt_touch.Text);
            if (dgv_itemDetails.CurrentRow == null)
                return;

            //if (dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value.ToString() != "")
            //{
            //    WastageWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value.ToString());
            //}
            if (dgv_itemDetails.CurrentRow.Cells["Wst"].Value.ToString() != "")
            {
                wst = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Wst"].Value.ToString());
            }
            if (dgv_itemDetails.CurrentRow.Cells["NetWt"].Value.ToString() != "")
            {
                NetWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["NetWt"].Value.ToString());
            }
            if (dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString() != "")
            {
                touch = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Touch"].Value.ToString());
            }
            if (dgv_itemDetails.CurrentRow.Cells["Loss"].Value.ToString() != "")
            {
                Loss = 0 /*Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Loss"].Value.ToString())*/;
            }
            if (dgv_itemDetails.CurrentRow.Cells["PurityId"].Value.ToString() != "")
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select [Purity Value] FROM ITM.VPurityMaster WHERE PurityId=" + dgv_itemDetails.CurrentRow.Cells["PurityId"].Value + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        purityValue = Convert.ToDouble(dt.Rows[0]["Purity Value"].ToString());
                    }
                }
            }
            if (cmbpartytype.Text == "Branch")
            {
                ActualWt = Math.Round((((NetWt + wst + Loss) * touch) / 100), 3);
            }
            else if (cmb_Purchasetype.Text == "Refinery" && grbreceipt.Checked == true)
            {
                ActualWt = Math.Round(((NetWt * touch) / defaulttouch), 3);
            }
            else
            {
                if (grb_cash.Checked == true)
                {
                    ActualWt = Math.Round(((NetWt + Loss) * purityValue) / defaulttouch, 3);
                }
                else
                {
                    ActualWt = Math.Round(((NetWt + wst + Loss) * touch) / defaulttouch, 3);
                }
            }
            dgv_itemDetails.CurrentRow.Cells["ActualWt"].Value = ActualWt.ToString(RoundTo);
        }
        private void CalculateActualWtwhileAdding()
        { 
            if (SoftwareSettings.CompName == "WESTERN" || SoftwareSettings.CompName == "HAYATH")
            {
                return;
            }

                Double PureWt = 0, WastageWt = 0, ActualWt = 0, NetWt = 0, purityValue = 0, touch = 0, defaulttouch = 0, wst = 0, Loss = 0, Gwt = 0, IssueWt = 0, metalcash = 0, metalrate = 0;
            defaulttouch = Convert.ToDouble(txt_touch.Text == "" ? "100" : txt_touch.Text);
            if (dgv_itemDetails.RowCount <= 0)
                return;
            //if (dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value.ToString() != "")
            //{
            //    WastageWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value.ToString());
            //}

            foreach (DataGridViewRow r in dgv_itemDetails.Rows)
            {
                if (r.Cells["ItemId"].Value.ToString() == "")
                    return;

                if (r.Cells["ItemId"].Value.ToString() != "0")
                {
                    if (r.Cells["Rate"].Value.ToString() != "")
                    {
                        metalrate = Convert.ToSingle(Convert.ToSingle(r.Cells["Rate"].Value.ToString()) == 0 ? txtRate.Text : r.Cells["Rate"].Value.ToString());

                    }
                    if (r.Cells["NetWt"].Value.ToString() != "")
                    {
                        NetWt = Convert.ToDouble(r.Cells["NetWt"].Value.ToString());
                    }
                    if (r.Cells["Gwt"].Value.ToString() != "")
                    {
                        Gwt = Convert.ToDouble(r.Cells["Gwt"].Value.ToString());
                    }
                    if (r.Cells["IssueWt"].Value.ToString() != "")
                    {
                        IssueWt = Convert.ToDouble(r.Cells["IssueWt"].Value.ToString());
                    }
                    if (r.Cells["Touch"].Value.ToString() != "")
                    {
                        touch = Convert.ToDouble(r.Cells["Touch"].Value.ToString());
                    }
                    //MessageBox.Show(dgv_itemDetails.CurrentRow.Cells["PurityId"].Value.ToString());
                    if (r.Cells["PurityId"].Value.ToString() != "")
                    {
                        using (DataTable dt = DBConn.GetData(new SqlCommand("Select [Purity Value] FROM ITM.VPurityMaster WHERE PurityId=" + r.Cells["PurityId"].Value + "")).Tables[0])
                        {
                            if (dt.Rows.Count > 0)
                            {
                                purityValue = Convert.ToDouble(dt.Rows[0]["Purity Value"].ToString());
                            }
                        }
                    }
                    if (cmbpartytype.Text == "Branch")
                    {
                        ActualWt = ((NetWt + wst + Loss) * touch) / 100;
                    }
                    else if (cmb_Purchasetype.Text == "Refinery" && grbreceipt.Checked == true)
                    {
                        ActualWt = ((NetWt * touch) / defaulttouch);
                    }
                    else
                    {
                        if (grb_cash.Checked == true)
                        {
                            ActualWt = ((NetWt + Loss) * purityValue) / defaulttouch;
                        }
                        else
                        {
                            ActualWt = ((NetWt + wst + Loss) * touch) / defaulttouch;
                        }
                    }
                    r.Cells["ActualWt"].Value = ActualWt.ToString(RoundTo);

                    if (SoftwareSettings.CompName == "PEENIKA" && cmb_Purchasetype.Text == "Repair" && grbissue.Checked == true && cmbpartytype.Text == "Customer")
                    {
                        metalcash = (Gwt - IssueWt) * metalrate;
                        r.Cells["MetalCash"].Value = metalcash.ToString("f2");
                    }
                }
            }
        }

        //private void dgv_itemDetails_SummaryCalculated(object source, EventArgs e)
        //{
        //    txttotalactualwt.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["ActualWt"].Text).ToString("f3");
        //    txttotalpurewt.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["PureWt"].Text).ToString("f3");
        //    txttotalwastagewt.Text = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["WastageWt"].Text).ToString("f3");

        //}

        void CalBatchTotalAmount()
        {
            Double avgrate, ActualWt, TotalAmount;

            if (dgv_itemDetails.CurrentRow == null)
                return;

            if (dgv_itemDetails.CurrentRow.Cells["BatchId"].Value.ToString() == null)
                return;
            if (dgv_itemDetails.CurrentRow.Cells["BatchId"].Value.ToString().Trim().Length >= 2)
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("select AvgRate from PROD.VBatchIdMaster WHERE BatchId=" + dgv_itemDetails.CurrentRow.Cells["BatchId"].Value + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        avgrate = Convert.ToDouble(dt.Rows[0]["AvgRate"].ToString());
                        if (avgrate != 0)
                        {
                            ActualWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["ActualWt"].Value.ToString());
                            TotalAmount = ActualWt * avgrate;
                            dgv_itemDetails.CurrentRow.Cells["TotalAmount"].Value = TotalAmount.ToString("f2");
                        }
                    }
                }
            }
        }

        void CalTotalAmount()
        {
            Double TotalAmount = 0, StCash = 0, Diacash = 0, crtfctncharge = 0, mc = 0, metalcash = 0;
            if (dgv_itemDetails.CurrentRow == null)
                return;
            if (dgv_itemDetails.CurrentRow.Cells["BatchId"].Value.ToString().Trim().Length <= 2)
            {
                if (dgv_itemDetails.CurrentRow.Cells["StCash"].Value.ToString() != "")
                {
                    StCash = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["StCash"].Value.ToString());
                }
                if (dgv_itemDetails.CurrentRow.Cells["DiaCash"].Value.ToString() != "")
                {
                    Diacash = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["DiaCash"].Value.ToString());
                }
                if (dgv_itemDetails.CurrentRow.Cells["CertificationCharge"].Value.ToString() != "")
                {
                    crtfctncharge = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["CertificationCharge"].Value.ToString());
                }
                if (dgv_itemDetails.CurrentRow.Cells["MC"].Value.ToString() != "")
                {
                    mc = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["MC"].Value.ToString());
                }
                if (dgv_itemDetails.CurrentRow.Cells["MetalCash"].Value.ToString() != "")
                {
                    metalcash = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["MetalCash"].Value.ToString());
                }

                TotalAmount = (StCash + Diacash + crtfctncharge + mc + metalcash);

                dgv_itemDetails.CurrentRow.Cells["TotalAmount"].Value = TotalAmount.ToString("f2");
            }
        }


        void CalLossWeight()
        {
            Double Gwt = 0, Netwt = 0, IsNetwt = 0, Loss = 0, Mud = 0;
            if (dgv_itemDetails.CurrentRow.Cells["BatchId"].Value.ToString() == null)
                return;
            if (cmb_Purchasetype.Text == "Refinery" && grbreceipt.Checked == true)
            {
                DialogResult d1 = Gramboo.General.ShowMessage(
                     " Do You want to Calculate Loss!!\n\n" +
                     " 1. Press 'Yes' to Calculate \n" +
                     " 2. Press 'No' to Cancel This Action \n", "file not found", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                if (d1 == DialogResult.No)
                {
                    dgv_itemDetails.CurrentRow.Cells["Loss"].Value = Loss.ToString(RoundTo);
                    return;
                }
            }

            if (dgv_itemDetails.CurrentRow.Cells["BatchId"].Value.ToString().Trim().Length >= 2)
            {
                if (cmb_Purchasetype.Text == "Melting" && grbreceipt.Checked == true)
                {
                    //using (DataTable dt = DBConn.GetData(new SqlCommand("select NetWt from PROD.VCalLoss WHERE PurchaseType=10 and TransType= 0 and BatchId=" + dgv_itemDetails.CurrentRow.Cells["BatchId"].Value + "")).Tables[0])
                    //{
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        IsNetwt = Convert.ToDouble(dt.Rows[0]["NetWt"].ToString());
                    //    }
                    //}
                    IsNetwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["IssueWt"].Value.ToString());

                    if (dgv_itemDetails.CurrentRow.Cells["Mud"].Value.ToString() != "")
                    {
                        Mud = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Mud"].Value.ToString());
                    }
                }
                else if (cmb_Purchasetype.Text == "Refinery" && grbreceipt.Checked == true)
                {
                    if (dgv_itemDetails.CurrentRow.Cells["IssueWt"].Value.ToString() != "")
                    {
                        IsNetwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["IssueWt"].Value.ToString());
                    }
                }
                else if (cmb_Purchasetype.Text == "Test" && grbreceipt.Checked == true)
                {
                    //using (DataTable dt = DBConn.GetData(new SqlCommand("select NetWt from PROD.VCalLoss WHERE PurchaseType=7 and TransType= 0 and BatchId=" + dgv_itemDetails.CurrentRow.Cells["BatchId"].Value + "")).Tables[0])
                    //{
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //IsNetwt = Convert.ToDouble(dt.Rows[0]["NetWt"].ToString());
                    //}
                    IsNetwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["IssueWt"].Value.ToString());
                    //}
                }

                if (IsNetwt != 0)
                {
                    if (dgv_itemDetails.CurrentRow.Cells["Netwt"].Value.ToString() != "")
                    {
                        Netwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Netwt"].Value.ToString());
                    }

                    Loss = IsNetwt - Netwt - Mud;
                    if (cmb_Purchasetype.Text == "Refinery" && grbreceipt.Checked == true)
                    {
                        Loss = IsNetwt - Netwt;
                    }

                    dgv_itemDetails.CurrentRow.Cells["Loss"].Value = Loss.ToString(RoundTo);
                }
            }
        }

        private void CalculatedWstPerc()
        {
            if (dgv_itemDetails.CurrentRow == null)
                return;
            double purity = 0, touch = 0, wstperc = 0, wst = 0, Netwt = 0, WstPerGram = 0, Rate = 0;
            string MCmode = "";

            if (cmbpartyname.SelectedValue == null)
                return;
            if (cmbpartytype.SelectedValue.ToString() == "1" || cmbpartytype.SelectedValue.ToString() == "2")
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("select SuppMCmode from PUR.SupplierMaster WHERE SuppId =" + cmbpartyname.SelectedValue + " " +
                                                                   "and Company_id=" + txtcompId.Text + "  ")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        MCmode = dt.Rows[0]["SuppMCmode"].ToString();
                    }
                }
            }

            if (MCmode != "W")
            {

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
            }
            else
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("select WstPerGram from PUR.SupplierItemdetails " +
                    "where ItemId=" + dgv_itemDetails.CurrentRow.Cells["ItemId"].Value + " and SuppId=" + cmbpartyname.SelectedValue + " " +
                    "and Company_id=" + txtcompId.Text + "  ")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        WstPerGram = Convert.ToDouble(dt.Rows[0]["WstPerGram"].ToString());
                    }
                }
                if (dgv_itemDetails.CurrentRow.Cells["Netwt"].Value.ToString() != "")
                {
                    Netwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Netwt"].Value.ToString());
                }

                wstperc = Math.Round(Netwt, 3) * Math.Round(WstPerGram, 3);

            }

            dgv_itemDetails.CurrentRow.Cells["Wst Perc"].Value = wstperc.ToString("f2");

        }


        private void CalculateMc()
        {
            if (CalcMcRate == true)
                return;
            double WstPerc = 0, Mc = 0, Rate = 0, WastageWt = 0, metalrate = 0, Netwt = 0;

            if (cmbpartytype.SelectedValue == null || (cmbpartytype.SelectedValue.ToString() == "1" && grbissue.Checked == true))
                return;

            metalrate = (Convert.ToDouble(txtRate.Text == "" ? "0" : txtRate.Text));
            if (cmbpartyname.SelectedValue != null)
            {
                if (dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value.ToString() != "")
                {
                    WastageWt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["WastageWt"].Value.ToString());
                }

                if (dgv_itemDetails.CurrentRow.Cells["Wst Perc"].Value.ToString() != "")
                {
                    WstPerc = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Wst Perc"].Value.ToString());
                }

                if (dgv_itemDetails.CurrentRow.Cells["Netwt"].Value.ToString() != "")
                {
                    Netwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Netwt"].Value.ToString());
                }

                if (dgv_itemDetails.CurrentRow.Cells["Rate"].Value.ToString() != "")
                {
                    Rate = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Rate"].Value.ToString());

                }

                if (SoftwareSettings.CompName.ToUpper() == "MANJALI" )
                {
                    if ((cmbpartytype.Text == "Supplier" && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true)) { dgv_itemDetails.CurrentRow.Cells["MC"].Value = 0; return; }
                    else if ((cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true)) { dgv_itemDetails.CurrentRow.Cells["MC"].Value = 0; return; }

                    if (WstPerc != 0)
                    {
                        Rate = Convert.ToDouble(txtRate.Text);
                        if (chkMetalRateTaxIncl.Checked == false)
                        {
                            Double TxExRate = Math.Round((Rate * 100) / (100 + 3 /* ((Convert.ToDouble(txt_cgstperc.Text)) + Convert.ToDouble(txt_sgstperc.Text)) + Convert.ToDouble(txt_igstperc.Text)*/), 2);
                            Rate = TxExRate; 
                        }

                        Rate = Math.Round((WstPerc * Rate) / Convert.ToDouble(txtPurity.Text), 2);
                        dgv_itemDetails.CurrentRow.Cells["MCRate"].Value = Rate.ToString("f2");
                        dgv_itemDetails.CurrentRow.Cells["MC"].Value = (Netwt * Rate).ToString("f2");
                    }
                }
                //if (SoftwareSettings.CompName.ToUpper() == "POPULAR")
                //{
                //    double mcperc = 0;
                //    mcperc = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["MCPerc"].Value.ToString()==""?"0":dgv_itemDetails.CurrentRow.Cells["MCPerc"].Value.ToString());
                //    dgv_itemDetails.CurrentRow.Cells["MC"].Value = (Netwt * mcperc).ToString("f2");

                //}
                else if (SoftwareSettings.CompName.ToUpper() == "REGAL" && cmbpartytype.Text == "Branch")
                {


                    Rate = Convert.ToDouble(txtRate.Text);
                    if (chkMetalRateTaxIncl.Checked == false)
                    {
                        Double TxExRate = Math.Round((Rate * 100) / (100 + 3 /* ((Convert.ToDouble(txt_cgstperc.Text)) + Convert.ToDouble(txt_sgstperc.Text)) + Convert.ToDouble(txt_igstperc.Text)*/), 2);
                        Rate = TxExRate;
                    }

                    Rate = Math.Round((WstPerc * Rate) / Convert.ToDouble(txtPurity.Text), 2);
                    dgv_itemDetails.CurrentRow.Cells["MCRate"].Value = Rate.ToString("f2");
                    dgv_itemDetails.CurrentRow.Cells["MC"].Value = (Netwt * Rate).ToString("f2");

                }

                if (SoftwareSettings.CompName.ToUpper() == "POPULAR GOLD AND DIAMONDS" || SoftwareSettings.CompName.ToUpper() == "ESSJAY" || SoftwareSettings.CompName.ToUpper() == "WESTERN")
                {
                    double mcperc = 0, MCRate = 0;
                    mcperc = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Mc Perc"].Value.ToString() == "" ? "0" : dgv_itemDetails.CurrentRow.Cells["Mc Perc"].Value.ToString());
                    if (dgv_itemDetails.Columns.Contains("MCRate"))
                    {
                        MCRate = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["MCRate"].Value.ToString() == "" ? "0" : dgv_itemDetails.CurrentRow.Cells["MCRate"].Value.ToString());
                        //dgv_itemDetails.CurrentRow.Cells["MC"].Value = (Netwt * mcperc).ToString("f2");

                        dgv_itemDetails.CurrentRow.Cells["MC"].Value = (Netwt * MCRate).ToString("f2");
                    }
                }


                //else
                //{
                //    dgv_itemDetails.CurrentRow.Cells["MC"].Value = 0;
                //}
            }
        }

        private void CalculateMcRate()
        {
            if (SoftwareSettings.CompName == "MANJALI")
            {
                double MC = 0, Rate = 0, Netwt = 0, Rate_ = 0, Wst_Perc = 0, purityValue = 0;

                if (cmbpartytype.SelectedValue == null || (cmbpartytype.SelectedValue.ToString() == "1" && grbissue.Checked == true))
                    return;

                CalcMcRate = true;

                if ((cmbpartytype.Text == "Supplier" && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true)) { dgv_itemDetails.CurrentRow.Cells["MC"].Value = 0; return; }
                else if ((cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true)) { dgv_itemDetails.CurrentRow.Cells["MC"].Value = 0; return; }

                if (dgv_itemDetails.CurrentRow.Cells["Netwt"].Value.ToString() != "")
                {
                    Netwt = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["Netwt"].Value.ToString());
                }
                if (dgv_itemDetails.CurrentRow.Cells["MC"].Value.ToString() != "")
                {
                    MC = Convert.ToDouble(dgv_itemDetails.CurrentRow.Cells["MC"].Value.ToString());
                }

                Rate = (MC / Netwt);
                dgv_itemDetails.CurrentRow.Cells["MCRate"].Value = Rate.ToString("f2");

                Rate_ = Convert.ToDouble(txtRate.Text);
                if (chkMetalRateTaxIncl.Checked == false)
                {
                    Double TxExRate = Math.Round((Rate_ * 100) / (100 + 3 /* ((Convert.ToDouble(txt_cgstperc.Text)) + Convert.ToDouble(txt_sgstperc.Text)) + Convert.ToDouble(txt_igstperc.Text)*/), 2);
                    Rate_ = TxExRate;
                }

                Wst_Perc = Math.Round((Rate * Convert.ToDouble(txtPurity.Text)) / Rate_, 2);

                dgv_itemDetails.CurrentRow.Cells["Wst Perc"].Value = Wst_Perc.ToString("f2");

                using (DataTable dt = DBConn.GetData(new SqlCommand("Select [Purity Value] FROM ITM.VPurityMaster WHERE PurityId=" + dgv_itemDetails.CurrentRow.Cells["PurityId"].Value + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        purityValue = Convert.ToDouble(dt.Rows[0]["Purity Value"].ToString());
                    }
                }

                dgv_itemDetails.CurrentRow.Cells["Touch"].Value = (purityValue + Wst_Perc).ToString("f2");

                CalcMcRate = false;
            }
        }

        private void LoadProdCodes()
        {
            return;
            SqlCommand cmd = new SqlCommand();
            if (txtcompId.Text == "")
                return;
            Cmb_ProdCode.Visible = false;
            if (txtEntryNo.Text.Trim().Length > 0)
            {
                cmd.CommandText = "SELECT ProdCode,ProdCodeId FROM  [STK].[OrnamentsStatus]('" + txtDeptName.Text + "'," + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + ") "
                    + " UNION SELECT prodCode,ProdCodeId FROM STK.ProdCodeMaster WHERE EXISTS (select ProdcodeID FROM STK.StockTransferDetails WHERE EntryId=" + txtEntryNo.Text + "  AND STK.StockTransferDetails.ProdCodeId=STK.ProdCodeMaster.ProdCodeId) Order By ProdCode";
            }
            else
            {
                cmd.CommandText = "SELECT ProdCode,ProdCodeId FROM  [STK].[OrnamentsStatus]('" + txtDeptName.Text + "'," + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + ")  Order By ProdCode";
                //" + txtBranchID.Text + "," + (Cmb_DepartmentName.SelectedValue == null ? "0" : Cmb_DepartmentName.SelectedValue) + "
            }

            Cmb_ProdCode.DisplayMember = "ProdCode";
            Cmb_ProdCode.ValueMember = "ProdCodeId";

            Cmb_ProdCode.DataSource = DBConn.GetData(cmd, "prodcodes").Tables[0];
            //Cmb_ProdCode.Visible = true;
        }




        private void Cmb_ProdCode_Leave(object sender, EventArgs e)
        {
            //Cmb_ProdCode.Focus();
            AfterComboLeave(dgv_itemDetails, Cmb_ProdCode, -1);

        }


        private void enable_disable(Gramboo.Controls.GrbDataGridView dgv, int row, bool ReadOnly)
        {
            //SetEnable(dgv, "Touch", row, ReadOnly);
            SetEnable(dgv, "Qty", row, ReadOnly);
            SetEnable(dgv, "Gwt", row, ReadOnly);
            //SetEnable(dgv, "MetalRate", row, ReadOnly);
            if (SoftwareSettings.CompName.ToUpper() == "LOKA")
            {

            }
             else   if (SoftwareSettings.CompName.ToUpper() != "REGAL")
            {
                SetEnable(dgv, "MC", row, ReadOnly);
                SetEnable(dgv, "Wastage", row, ReadOnly);
                SetEnable(dgv, "VA", row, ReadOnly);
                SetEnable(dgv, "VAPerc", row, ReadOnly);
            }
             
            SetEnable(dgv, "IssueWt", row, ReadOnly);
        }
        private void SetEnable(Gramboo.Controls.GrbDataGridView dgv, string Column, int row, bool ReadOnly)
        {
            if (dgv.Columns.Contains(Column))
                dgv.Rows[row].Cells[Column].ReadOnly = ReadOnly;
        }

        void OthChGrid()
        {
            dgv_otherChg.AutoGenerateColumns = true;
            dgv_otherChg.ShowSerialNo = true;
            dgv_otherChg.DataFields = new List<string> { "EntryId", "OTchId", "ChargeID", "[Charge Name]", "NetAmount", "SGSTAmt", "CGSTAmt", "IGSTAmt", "TdsName", "TdsId", "TdsRate", "TdsAmount", "Amountt", "Company_id", "Branch_id" };
            dgv_otherChg.SummaryColumns = new string[] { "Amountt", "TdsAmount" };
            dgv_otherChg.HiddenDataFields = new List<string>() { "EntryId", "OTchId", "ChargeID", "TdsId", "Company_id", "Branch_id" };
            dgv_otherChg.Fill(new Table(SAFA.Classes.Common.DbName, "STK", "VSmithOtherCharges", true), "1=2");
            if (dgv_otherChg.Columns.Contains("col_AutoSlno"))
                dgv_otherChg.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
        }
        void ItemTaxGrid()
        {
            dgv_TaxDetails.AutoGenerateColumns = true;
            dgv_TaxDetails.ShowSerialNo = true;
            dgv_TaxDetails.DataFields = new List<string> { "TaxTrasnsId", "EntryId", "TaxID", "TaxName", "TaxRate", "TaxAmt", "Company_id", "Branch_id" };
            dgv_TaxDetails.SummaryColumns = new string[] { "TaxAmt", "TaxRate" };
            dgv_TaxDetails.HiddenDataFields = new List<string>() { "TaxTrasnsId", "EntryId", "TaxID", "Company_id", "Branch_id" };
            dgv_TaxDetails.Fill(new Table(SAFA.Classes.Common.DbName, "STK", "VStockTransferTaxDetails", true), "1=2");
            if (dgv_TaxDetails.Columns.Contains("col_AutoSlno"))
                dgv_TaxDetails.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
        }
        void PendGrid()
        {
            dgvPending.AutoGenerateColumns = true;
            dgvPending.ShowSerialNo = true;
            dgvPending.DataFields = new List<string> { "EntryId", "PendingId", "ReferenceId", "Reference", "RefTrackingID", "Nos", "Gwt", "StWt", "DiaWt", "NetWt", "AvgTouch", "ActWt", "IdType", "Company_id", "Branch_id" };
            dgvPending.SummaryColumns = new string[] { "Nos", "Gwt", "DiaWt", "NetWt", "ActWt" };
            dgvPending.HiddenDataFields = new List<string>() { "EntryId", "PendingId", "ReferenceId", "RefTrackingID", "IdType", "Created_by", "Created_date", "Branch_id", "Last_modified_by", "Last_modified_date", "Company_id", "Branch_id", "Counter_id", "IsActive" };
            dgvPending.Fill(new Table(SAFA.Classes.Common.DbName, "STK", "VStockTransferPending", true), "1=2");
            if (dgvPending.Columns.Contains("col_AutoSlno"))
                dgvPending.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
        }
        private void SmithDetails()
        {
            if (cmbpartytype.SelectedValue == null)
                return;

            if (txtBranchID.Text == "" || txtcompId.Text == "")
                return;
            if (cmbpartytype.SelectedValue.ToString() == "4" || cmbpartytype.SelectedValue.ToString() == "0")
                return;

            string CompStateId = "0";
            using (DataTable Com = DBConn.GetData(new SqlCommand("Select Comp_StateCode FROM syst.branchmaster WHERE branchid=" + txtBranchID.Text)).Tables[0])
            {
                if (Com.Rows.Count > 0)
                {
                    CompStateId = Com.Rows[0]["Comp_StateCode"].ToString();
                }
            }
            if (cmbpartyname.SelectedValue != null && cmbpartytype.SelectedValue.ToString() != "" ) 
                
                
                  {
                if (SoftwareSettings.CompName == "TTD")
                {
                    using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                    ("Select [Closing Wt] FROM PUR.GETSUPPLIERBALANCE1DM('" + dtp_dt.Value.AddDays(1).ToString("dd-MMM-yyyy") + "'," + cmbpartyname.SelectedValue + "," + txtcompId.Text + "," + txtBranchID.Text + ",0,1)  ")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txt_openwt.Text = dt.Rows[0]["Closing Wt"].ToString();
                        }
                        else if (cmbpartyname.SelectedIndex != 3)
                        {
                            txt_touch.Text = "";
                            txt_openwt.Text = "";
                        }
                    }
                }
                else
                {
                    using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                                        ("Select [Closing Wt] FROM PUR.GETSUPPLIERBALANCE1('" + dtp_dt.Value.AddDays(1).ToString("dd-MMM-yyyy") + "'," + cmbpartyname.SelectedValue + "," + txtcompId.Text + "," + txtBranchID.Text + ",0)  ")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txt_openwt.Text = dt.Rows[0]["Closing Wt"].ToString();
                        }
                        else if (cmbpartyname.SelectedIndex != 3)
                        {
                            txt_touch.Text = "";
                            txt_openwt.Text = "";
                        }
                    }
                }

                if (cmbpartytype.SelectedValue.ToString() != "3")
                {
                    using (DataTable dt1 = DBConn.GetData(new SqlCommand("Select Acc_LedgerId,TDS,SuppDefltTouch,StateCode,isnull(SuppPlace,'') as SuppPlace,ISNULL(RoundTo,'F3') as RoundTo,suppGstNo FROM PUR.SupplierMaster WHERE SuppId=" + cmbpartyname.SelectedValue + "  ")).Tables[0])
                    {
                        Int64 tdsid = Convert.ToInt64(dt1.Rows[0]["TDS"] == DBNull.Value ? "0" : dt1.Rows[0]["TDS"].ToString());
                        Int64 Acc_Ledgerid = Convert.ToInt64(dt1.Rows[0]["Acc_LedgerId"] == DBNull.Value ? "0" : dt1.Rows[0]["Acc_LedgerId"].ToString());

                        if (dt1.Rows.Count > 0)
                        {
                            Double SuppDefltTouch = Convert.ToDouble(dt1.Rows[0]["SuppDefltTouch"].ToString());
                            TxtGSTNo.Text = (dt1.Rows[0]["suppGstNo"] == DBNull.Value ? "0" : dt1.Rows[0]["suppGstNo"].ToString());
                            RoundTo = dt1.Rows[0]["RoundTo"].ToString();
                            GSTPerc(CompStateId, dt1.Rows[0]["StateCode"].ToString());
                            if (SuppDefltTouch <= 0)
                            {
                                txt_touch.ShowMessage("Set SuppDefltTouch to Continue");
                                return;
                            }
                            else { txt_touch.Text = SuppDefltTouch.ToString(); }
                            if (grbissue.Checked == true)
                            {
                                if (place_supply.Text.Trim().Length < 2) { place_supply.Text = dt1.Rows[0]["SuppPlace"].ToString(); }
                            }
                        }

                        if (tdsid != 0)
                        {
                            Double othind = 0.00, ind = 0.00, nopan = 0.00;
                            using (DataTable dt = DBConn.GetData(new SqlCommand("select top 1 Otherthanindividual, Individual, NoPanno FROM GEN.TDSDetails  where TdsId = '" + tdsid + "' and date <= '" + dtp_dt.Value + "' order by date desc")).Tables[0])
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    ind = Convert.ToDouble(dt.Rows[0]["Individual"].ToString());
                                    othind = Convert.ToDouble(dt.Rows[0]["Otherthanindividual"].ToString());
                                    nopan = Convert.ToDouble(dt.Rows[0]["NoPanno"].ToString());

                                }
                            }
                            String HasTds = "", HasPan = "", IsInd = "", PartyType = "Supplier";
                            using (DataTable dt = DBConn.GetData(new SqlCommand("select isnull(HasTDS,'False') as HasTDS,isnull(HasPan,'False') as HasPan,isnull(IsIndividual,'False') as IsIndividual from ACC.Vtds WHERE Acc_LedgerID=" + Acc_Ledgerid.ToString() + " and PartyType='" + PartyType + "'")).Tables[0])
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    HasTds = dt.Rows[0]["HasTDS"].ToString();
                                    HasPan = dt.Rows[0]["HasPan"].ToString();
                                    IsInd = dt.Rows[0]["IsIndividual"].ToString();

                                    if (HasTds == "True" && HasPan == "False")
                                    {
                                        txt_tdsperc.Text = nopan.ToString("F2");
                                    }
                                    else if (HasTds == "True" && HasPan == "True" && IsInd == "True")
                                    {
                                        txt_tdsperc.Text = ind.ToString("F2");
                                    }
                                    else if (HasTds == "True" && HasPan == "True" && IsInd == "False")
                                    {
                                        txt_tdsperc.Text = othind.ToString("F2");
                                    }
                                }

                                else
                                {
                                    txt_tdsperc.Text = "0";
                                }
                            }
                        }
                        else
                        {
                            txt_tdsperc.Text = "0";
                        }
                    }
                }

                else if (cmbpartytype.SelectedValue.ToString() == "3")
                {
                    using (DataTable dt4 = DBConn.GetData(new SqlCommand("Select StateId,ISNULL(CustDefltTouch,0) as CustDefltTouch,StateId,isnull(CustPlace,'') as CustPlace,ISNULL(TDS,0) as TDS,ISNULL(Acc_LedgerId,0) as Acc_LedgerId,ISNULL(RoundTo,'F3') as RoundTo,CustGstNo  FROM CRM.CustomerMaster WHERE CustId=" + cmbpartyname.SelectedValue + " and Branch_id=" + txtBranchID.Text + "")).Tables[0])
                    {

                        if (dt4.Rows.Count > 0)
                        {
                            using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                                      ("Select ActWtBalance FROM CRM.GetCustomerOpeningBalance('" + dtp_dt.Value.AddDays(1).ToString("dd-MMM-yyyy") + "'," + cmbpartyname.SelectedValue + "," + txtcompId.Text + "," + txtBranchID.Text + ",0,'0',99,'','')  ")).Tables[0])
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    txt_openwt.Text = dt.Rows[0]["ActWtBalance"].ToString();
                                }
                                else if (cmbpartyname.SelectedIndex != 3)
                                {
                                    txt_touch.Text = "";
                                    txt_openwt.Text = "";
                                }
                            }

                            TxtGSTNo.Text = (dt4.Rows[0]["CustGstNo"] == DBNull.Value ? "0" : dt4.Rows[0]["CustGstNo"].ToString());
                            Int64 tdsid = Convert.ToInt64(dt4.Rows[0]["TDS"] == DBNull.Value ? "0" : dt4.Rows[0]["TDS"].ToString());
                            Int64 Acc_Ledgerid = Convert.ToInt64(dt4.Rows[0]["Acc_LedgerId"] == DBNull.Value ? "0" : dt4.Rows[0]["Acc_LedgerId"].ToString());
                            RoundTo = dt4.Rows[0]["RoundTo"].ToString();
                            GSTPerc(CompStateId, dt4.Rows[0]["StateId"].ToString());
                            Double CustDefltTouch = Convert.ToDouble(dt4.Rows[0]["CustDefltTouch"].ToString());
                            if (CustDefltTouch <= 0) { txt_touch.ShowMessage("Set CustDefltTouch to Continue"); }
                            else { txt_touch.Text = CustDefltTouch.ToString(); }
                            if (grbissue.Checked == true)
                            {
                                if (place_supply.Text.Trim().Length < 2) { place_supply.Text = dt4.Rows[0]["CustPlace"].ToString(); }
                            }

                            if (tdsid != 0)
                            {
                                Double othind = 0.00, ind = 0.00, nopan = 0.00, RcRate = 0.00;
                                using (DataTable dt = DBConn.GetData(new SqlCommand("select top 1 Otherthanindividual, Individual, NoPanno , ReceivableRate FROM GEN.TDSDetails  where TdsId = '" + tdsid + "' and date <= '" + dtp_dt.Value + "' order by date desc")).Tables[0])
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        ind = Convert.ToDouble(dt.Rows[0]["Individual"].ToString());
                                        othind = Convert.ToDouble(dt.Rows[0]["Otherthanindividual"].ToString());
                                        nopan = Convert.ToDouble(dt.Rows[0]["NoPanno"].ToString());
                                        RcRate = Convert.ToDouble(dt.Rows[0]["ReceivableRate"].ToString());
                                    }
                                }
                                String HasTds = "", HasPan = "", IsInd = "", PartyType = "Customer";
                                using (DataTable dt = DBConn.GetData(new SqlCommand("select isnull(HasTDS,'False') as HasTDS,isnull(HasPan,'False') as HasPan,isnull(IsIndividual,'False') as IsIndividual from ACC.Vtds WHERE Acc_LedgerID=" + Acc_Ledgerid.ToString() + " and PartyType='" + PartyType + "'")).Tables[0])
                                {
                                    if (dt.Rows.Count > 0)
                                    {
                                        HasTds = dt.Rows[0]["HasTDS"].ToString();
                                        HasPan = dt.Rows[0]["HasPan"].ToString();
                                        IsInd = dt.Rows[0]["IsIndividual"].ToString();

                                        if (HasTds == "True" && HasPan == "False")
                                        {
                                            txt_tdsperc.Text = nopan.ToString("F2");
                                        }
                                        else if (HasTds == "True" && HasPan == "True" && IsInd == "True")
                                        {
                                            txt_tdsperc.Text = ind.ToString("F2");
                                        }
                                        else if (HasTds == "True" && HasPan == "True" && IsInd == "False")
                                        {
                                            txt_tdsperc.Text = othind.ToString("F2");
                                        }

                                        txt_tdsperc.Text = RcRate.ToString("F2");
                                    }

                                    else
                                    {
                                        txt_tdsperc.Text = "0";
                                    }
                                }
                            }
                            else
                            {
                                txt_tdsperc.Text = "0";
                            }
                        }
                    }
                }
            }
        }

        private void clswet()
        {
            double Openwt = 0, Actwt = 0, closewt = 0, Gwt = 0;
            Openwt = (Convert.ToDouble(txt_openwt.Text == "" ? "0" : txt_openwt.Text));
            Actwt = (Convert.ToDouble(txttotalactualwt.Text == "" ? "0" : txttotalactualwt.Text));
            // closewt = Openwt + Actwt;
            if (grbissue.Checked)
            {
                closewt = Openwt - Actwt;
            }
            else
            {
                closewt = Openwt + Actwt;
            }
            txt_closewt.Text = closewt.ToString("f2");
        }
        private void cmbpartyname_SelectedValueChanged(object sender, EventArgs e)
        {
            if (flag)
                return;
            if (cmbpartyname.SelectedValue != null && this.ActiveControl == cmbpartyname)
            {
                try
                {
                    cmbCode.SelectedValue = cmbpartyname.SelectedValue;
                }
                catch(Exception ex)
                {

                }

            }

            SmithDetails();
            SupplierBranchid();
            if (cmbpartytype.Text == "Supplier" || cmbpartytype.Text == "GoldSmith")
            {
                SupplierItemDetails();
                joborderid(); BtnSmithRpt();
            }
            else { BtnSmthiDetailRpt.Visible = false; }
            if (cmbpartytype.SelectedValue == null && cmbpartyname.SelectedValue == null && Cmb_StkType.SelectedValue == null)
                return;
            LoadPendingCombo();
        }

        private void txt_closewt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttotalactualwt_TextChanged(object sender, EventArgs e)
        {
            clswet();
        }

        private void txt_openwt_TextChanged(object sender, EventArgs e)
        {
            clswet();
        }
        private void calcuTax()
        {
            Double taxPerc = 0, TaxAmt = 0, Disamt = 0, mc = 0, tds = 0, cgst = 0, igst = 0, sgst = 0,SgstAmt=0, cgstAmt=0,igstamt=0;

            txtDisAmnt.Text = (string.IsNullOrEmpty(txtDisAmnt.Text.Trim()) ? "0.00" : txtDisAmnt.Text);
            txt_TaxPercent.Text = (string.IsNullOrEmpty(txt_TaxPercent.Text.Trim()) ? "0.00" : txt_TaxPercent.Text);
            txt_taxAmount.Text = (string.IsNullOrEmpty(txt_taxAmount.Text.Trim()) ? "0.00" : txt_taxAmount.Text);
            txt_totalmc.Text = (string.IsNullOrEmpty(txt_totalmc.Text.Trim()) ? "0.00" : txt_totalmc.Text);
            txt_tds.Text = (string.IsNullOrEmpty(txt_tds.Text.Trim()) ? "0.00" : txt_tds.Text);
            txt_cgstperc.Text = (string.IsNullOrEmpty(txt_cgstperc.Text.Trim()) ? "0.00" : txt_cgstperc.Text);
            txt_sgstperc.Text = (string.IsNullOrEmpty(txt_sgstperc.Text.Trim()) ? "0.00" : txt_sgstperc.Text);
            txt_igstperc.Text = (string.IsNullOrEmpty(txt_igstperc.Text.Trim()) ? "0.00" : txt_igstperc.Text);

            cgst = Convert.ToDouble(txt_cgstperc.Text);
            sgst = Convert.ToDouble(txt_sgstperc.Text);
            igst = Convert.ToDouble(txt_igstperc.Text);
            taxPerc = Convert.ToDouble(txt_TaxPercent.Text);
            Disamt = Convert.ToDouble(txtDisAmnt.Text);
            mc = Convert.ToDouble(txt_totalmc.Text);
            tds = Convert.ToDouble(txt_tds.Text);

            SgstAmt =Math.Round( Disamt * sgst / 100,2);
            cgstAmt = Math.Round(Disamt * cgst / 100, 2);
            igstamt = Math.Round(Disamt * igst / 100, 2);

            //
            //{
            TaxAmt = SgstAmt + cgstAmt + igstamt;

            txt_taxAmount.Text = TaxAmt.ToString("f2");

        }
        private void chkbarcode_CheckedChanged(object sender, EventArgs e)
        {
            DisplayProdcodeColumn();
        }

        private void Cmb_ItemName_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void Rb_Diamond_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void Rb_Gold_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FrmSmithReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtBranchID.Text != "")
            {
                if (SAFA.Classes.Common.TxTypeValidate(DBConn, Convert.ToInt16(txtBranchID.Text)) || SoftwareSettings.CompName=="TTD") 
                {
                    if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
                    {
                        txtOne_Two.Visible = true;
                        txtOne_Two.Focus();
                    }
                }
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (GrpPurOtherCharges.Visible == true)
                {
                    GrpPurOtherCharges.Visible = false;
                    linkLbl_OtherCharges.Text = "Add Other Charges";
                }
                if (panelTestPending.Visible == true)
                {
                    panelTestPending.Visible = false;
                }
                if (PanelPending.Visible == true)
                {
                    PanelPending.Visible = false;
                }
            }
        }

        private void dgv_itemDetails_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void grbissue_CheckedChanged(object sender, EventArgs e)
        {
            //if (grbissue.Checked == false)
            //{

            //    dgv_itemDetails.Columns["ProdCode"].Visible = false;


            //}
            //else
            //{
            //    dgv_itemDetails.Columns["ProdCode"].Visible = true;
            //}
        }
        private void purity()
        {
            //if (cmbpartytype.Text != "Branch")
            //{
            //    using (DataTable dt1 = DBConn.GetData(new SqlCommand("select PurityId,PurityName,PurityValue from PUR.SupPurity where SuppId= " + (cmbpartyname.SelectedValue == null ? "0" : cmbpartyname.SelectedValue))).Tables[0])
            //    {
            //        if (dt1.Rows.Count > 0)
            //        {

            //            dgv_itemDetails.CurrentRow.Cells["PurityID"].Value = dt1.Rows[0]["PurityId"] == DBNull.Value ? "0" : dt1.Rows[0]["PurityId"];
            //            dgv_itemDetails.CurrentRow.Cells["Purity"].Value = dt1.Rows[0]["PurityName"] == DBNull.Value ? "0" : dt1.Rows[0]["PurityName"];
            //            dgv_itemDetails.CurrentRow.Cells["Touch"].Value = dt1.Rows[0]["PurityValue"] == DBNull.Value ? "0" : dt1.Rows[0]["PurityValue"];
            //            //txt_mcmode.Text = dt1.Rows[0]["SuppMCmode"].ToString();
            //        }
            //    }
            //}
        }

        private void Cmb_ItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            purity();
        }
        private void calcuDiscount()
        {

            Double Discount = 0, Totalamt = 0, Taxamount = 0, othch = 0;

            txt_Totalamount.Text = (string.IsNullOrEmpty(txt_Totalamount.Text.Trim()) ? "0.00" : txt_Totalamount.Text);
            txtDiscount.Text = (string.IsNullOrEmpty(txtDiscount.Text.Trim()) ? "0.00" : txtDiscount.Text);
            txtDisAmnt.Text = (string.IsNullOrEmpty(txtDisAmnt.Text.Trim()) ? "0.00" : txtDisAmnt.Text);
            //TxtOTCharge.Text = (string.IsNullOrEmpty(TxtOTCharge.Text.Trim()) ? "0.00" : TxtOTCharge.Text);

            Totalamt = Convert.ToDouble(txt_Totalamount.Text);
            Discount = Convert.ToDouble(txtDiscount.Text);
            othch = Convert.ToDouble(TxtOTCharge.Text);

            Taxamount = (Totalamt - Discount);
            txtDisAmnt.Text = Taxamount.ToString("f2");
            //Taxamount = (Totalamt + othch);
            //txt_Totalamount.Text = Taxamount.ToString("f2");
        }

        private void netamt()
        {

            Double amt = 0, Roundoff = 0, tds = 0, nettot = 0;
            txt_amt.Text = (string.IsNullOrEmpty(txt_amt.Text.Trim()) ? "0.00" : txt_amt.Text);
            txtnet.Text = (string.IsNullOrEmpty(txtnet.Text.Trim()) ? "0.00" : txtnet.Text);
            txtRoundoff.Text = (string.IsNullOrEmpty(txtRoundoff.Text.Trim()) ? "0.00" : txtRoundoff.Text);

            amt = Convert.ToDouble(txt_amt.Text);
            Roundoff = Convert.ToDouble(txtRoundoff.Text);
            nettot = Convert.ToDouble(txtnet.Text);
            tds = Convert.ToDouble(txt_tds.Text);
            if (SoftwareSettings.CompName == "TTD")
            {
                nettot = amt + Roundoff-tds;  
            }
            else
            {
                nettot = amt + Roundoff;
            }
                
            txtnet.Text = nettot.ToString("f2");



        }
        private void calculateTax()
        {
            if (txtcompId.Text == "" && txtBranchID.Text == "")
                return;
            if (cmbpartytype.SelectedValue == null || cmbpartyname.SelectedValue == null)
                return;
            Double cgstper = 0, sgstperc = 0, igstperc = 0, cgstamt = 0, sgstamt = 0, igstamt = 0, totamt = 0, mc = 0, tdsperc = 0, tds = 0;
            String IsGst = "";
            totamt = (Convert.ToDouble(txtDisAmnt.Text == "" ? "0" : txtDisAmnt.Text));
            cgstper = (Convert.ToDouble(txt_cgstperc.Text == "" ? "0" : txt_cgstperc.Text));
            sgstperc = (Convert.ToDouble(txt_sgstperc.Text == "" ? "0" : txt_sgstperc.Text));
            igstperc = (Convert.ToDouble(txt_igstperc.Text == "" ? "0" : txt_igstperc.Text));
            tdsperc = (Convert.ToDouble(txt_tdsperc.Text == "" ? "0" : txt_tdsperc.Text));
            mc = (Convert.ToDouble(txt_totalmc.Text == "" ? "0" : txt_totalmc.Text));

            if ((cmbpartytype.SelectedValue.ToString() == "1" || cmbpartytype.SelectedValue.ToString() == "2") && cmbpartyname.SelectedValue.ToString().Trim().Length > 3)
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                        ("select IsGSTRegistred From PUR.SupplierMaster Where SuppId=" + cmbpartyname.SelectedValue + " and Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        IsGst = dt.Rows[0]["IsGSTRegistred"].ToString();
                    }
                }
            }
            else
            {
                IsGst = "Y";
            }

            if (cmbpartytype.SelectedValue.ToString() != "4")
            {
                tds = totamt * tdsperc / 100;
                txt_tds.Text = tds.ToString("f0");
            }
            if (IsGst == "N")
            {
                //txt_tds.Text = 0.ToString("f2");
                txt_cgstamt.Text = 0.ToString("F2");
                txt_sgst.Text = 0.ToString("F2");
                txt_igstamt.Text = 0.ToString("F2");
                txt_taxAmount.Text = 0.ToString("F3");
                return;
            }

            if (txtOne_Two.Text == "1" || txtOne_Two.Text == "3")
            {

                //{
                cgstamt = totamt * cgstper / 100;
                sgstamt = totamt * sgstperc / 100;
                igstamt = totamt * igstperc / 100;
                //}
                //else
                //{
                //cgstamt = mc * cgstper / 100;
                //sgstamt = mc * sgstperc / 100;
                //igstamt = mc * igstperc / 100;
                //}
                if (cmbpartytype.SelectedValue.ToString() != "4")
                {
                    tds = totamt * tdsperc / 100;
                    txt_tds.Text = tds.ToString("f0");
                }
                txt_cgstamt.Text = cgstamt.ToString("F2");
                txt_sgst.Text = sgstamt.ToString("F2");
                txt_igstamt.Text = igstamt.ToString("F2");
                txt_taxAmount.Text = (cgstamt + sgstamt + igstamt).ToString("F3");
            }
            else
            {
                //txt_tds.Text = 0.ToString("f2");
                txt_cgstamt.Text = 0.ToString("F3");
                txt_sgst.Text = 0.ToString("F3");
                txt_igstamt.Text = 0.ToString("F2");
                txt_taxAmount.Text = 0.ToString("F2");
            }
        }

        private void txt_taxAmount_TextChanged(object sender, EventArgs e)
        {
            if (cmb_Purchasetype.Text == "JobWork")
            {
                calcufinal1();
            }
            else
            {
                calcufinal();
            }
        }

        private void txtDisAmnt_TextChanged(object sender, EventArgs e)
        {
            if (cmb_Purchasetype.Text == "JobWork")
            {  //calcuTax();
                calculateTax();
                calcufinal1();
            }
            else
            {
                calculateTax();
                calcufinal();
            }
        }

        private void txt_TaxPercent_TextChanged(object sender, EventArgs e)
        {
            //calcuTax();
        }

        private void txtRoundoff_TextChanged(object sender, EventArgs e)
        {
            netamt();
        }



        private void txtnet_TextChanged(object sender, EventArgs e)
        {
            netamt();
        }

        private void txt_Totalamount_TextChanged(object sender, EventArgs e)
        {
            calcuDiscount();
            calculateTax();

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
        void taxone()
        {
            calculateTax();
            calcuTax();
            SmithDetails();
            txt_cgstperc.Visible = true;
            txt_sgstperc.Visible = true;
            txt_igstperc.Visible = true;
            txt_taxAmount.Visible = true;
            txt_tdsperc.Visible = true;
            txt_cgstamt.Visible = true;
            txt_sgst.Visible = true;
            txt_igstamt.Visible = true;
            txt_tds.Visible = true;
            txt_tds.Visible = true;
            label23.Visible = true;
            label22.Visible = true;
            label21.Visible = true;
            label19.Visible = true;
            label30.Visible = true;
        }
        private void txtOne_Two_TextChanged(object sender, EventArgs e)
        {
            if (txtOne_Two.Text == "1" || txtOne_Two.Text == "3")
            {
                taxone();
                //calculateTax();
                //calcuTax();
                //SmithDetails();
                //txt_cgstperc.Visible = true;
                //txt_sgstperc.Visible = true;
                //txt_igstperc.Visible = true;
                //txt_taxAmount.Visible = true;
                //txt_tdsperc.Visible = true;
                //txt_cgstamt.Visible = true;
                //txt_sgst.Visible = true;
                //txt_igstamt.Visible = true;
                //txt_tds.Visible = true;
                //txt_tds.Visible = true;
                //label23.Visible = true;
                //label22.Visible = true;
                //label21.Visible = true;
                //label19.Visible = true;
                //label30.Visible = true;
                vchno(false);
            }
            if (txtOne_Two.Text == "2")
            {
                calculateTax();
                txt_cgstperc.Visible = false;
                txt_sgstperc.Visible = false;
                txt_igstperc.Visible = false;
                txt_taxAmount.Visible = false;
                txt_tdsperc.Visible = false;
                txt_cgstamt.Visible = false;
                txt_sgst.Visible = false;
                txt_igstamt.Visible = false;
                txt_tds.Visible = false;
                label23.Visible = false;
                label22.Visible = false;
                label21.Visible = false;
                label19.Visible = false;
                label30.Visible = false;

                txt_taxAmount.Text = "0";
                txt_tds.Text = "0";
                if (grbissue.Checked == true)
                {
                    cmb_VoucherTypeId.SelectedValue = 243;
                }
                else if (grbreceipt.Checked == true)
                {
                    cmb_VoucherTypeId.SelectedValue = 244;
                }

                if (!IsEditMode)
                    TxtVoucherNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtp_dt.Value,
                DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));

                SAFA.Forms.STK.SmithReceiptList frm = new SAFA.Forms.STK.SmithReceiptList();
                frm.Vouchertypeid = (int)cmb_VoucherTypeId.SelectedValue;
                frm.PurchaseTypeId = (int)cmb_Purchasetype.SelectedIndex;
                frm.transtype = this.TransType;
                frm.DataEntryForm = this;
                this.ListForm = frm;
            }
            txtOne_Two.Visible = false;
        }

        private void txt_cgstperc_TextChanged(object sender, EventArgs e)
        {
            calculateTax();
        }

        private void txt_sgstperc_TextChanged(object sender, EventArgs e)
        {
            calculateTax();
        }

        private void txt_igstperc_TextChanged(object sender, EventArgs e)
        {
            calculateTax();
        }

        private void txt_cgstamt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            calcuDiscount();
        }

        private void label42_Click(object sender, EventArgs e)
        {

        }

        private void btn_copper_Click(object sender, EventArgs e)
        {
            Double CopperWT = 0; bool HasBullion = false;

            if ((cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true))
            {
                DialogResult d =
              Gramboo.General.ShowMessage(

              "Do You Want to Generate Stock Conversion ? \n\n", "Stock Conversion", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                if (d == DialogResult.Yes)
                {
                    CopperAdded = true;
                }
                else { CopperAdded = false; }
            }
            else { CopperAdded = false; }

            DataTable dataTable = (DataTable)dgv_itemDetails.DataSource;
            for (int i = dataTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dataTable.Rows[i];
                if (dr["Item Name"].ToString().ToUpper().StartsWith("COPPER"))
                {
                    dr.Delete();
                }
                else if (dr["Item Name"].ToString().ToUpper() == "")
                {
                    dr.Delete();
                }
            }
            dataTable.AcceptChanges();

            foreach (DataGridViewRow Row in dgv_itemDetails.Rows)
            {
                if (Row.Cells["ItemId"].Value.ToString().Trim().Length >= 2)
                {
                    Double Gwt = 0, Netwt = 0, Touch = 0, Wt = 0, TempCopperWT = 0;
                    //if (Row.Cells["Item Name"].Value.ToString().ToUpper().StartsWith("BULLION"))
                    //{
                    Touch = Convert.ToDouble(Row.Cells["Touch"].Value.ToString() == "" ? "0" : Row.Cells["Touch"].Value.ToString());
                    Gwt = Convert.ToDouble(Row.Cells["Gwt"].Value.ToString() == "" ? "0" : Row.Cells["Gwt"].Value.ToString());
                    Netwt = Convert.ToDouble(Row.Cells["Netwt"].Value.ToString() == "" ? "0" : Row.Cells["Netwt"].Value.ToString());

                    Wt = Math.Round(((Math.Round(Gwt, 3) / 92) * Touch), 3);
                    CopperWT += Math.Abs(Math.Round((Wt - Netwt), 3));
                    HasBullion = true;
                    //}
                }
            }

            //if (HasBullion == true)
            //{

            string ItemId = "", ItemName = "", PurityName = "", PurityValue = "", PurityId = "";
            using (DataTable dt = DBConn.GetData(new SqlCommand("Select ItemId,[Item Name],[Purity Name],[Purity Value],PurityId FROM ITM.VItemMaster WHERE [Item Name] LIKE 'Copper%'")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    ItemId = dt.Rows[0]["ItemId"].ToString();
                    ItemName = dt.Rows[0]["Item Name"].ToString();
                    PurityName = dt.Rows[0]["Purity Name"].ToString();
                    PurityValue = dt.Rows[0]["Purity Value"].ToString();
                    PurityId = dt.Rows[0]["PurityId"].ToString();
                }
            }


            DataRow drToAdd = dataTable.NewRow();
            drToAdd["ItemID"] = ItemId;
            drToAdd["Item Name"] = ItemName;
            drToAdd["Purity"] = PurityName;
            drToAdd["PurityId"] = PurityId;
            drToAdd["Nos"] = "0";
            drToAdd["Gwt"] = CopperWT.ToString(RoundTo);
            drToAdd["NetWt"] = CopperWT.ToString(RoundTo);
            drToAdd["Touch"] = PurityValue;
            dataTable.Rows.Add(drToAdd);
            dataTable.AcceptChanges();
            //}
            return;
        }

        private void cmb_Purchasetype_SelectedValueChanged(object sender, EventArgs e)
        {
            SetColumns(dgv_itemDetails);
            using (DataTable dt2 = DBConn.GetData(new SqlCommand("select isnull(PurTypeId,0)as PurTypeId from stk.PurchaseTypeMaster where PurchaseTypeId=" + cmb_Purchasetype.SelectedIndex)).Tables[0])
            {

                if (dt2.Rows.Count > 0)
                {
                    Cmb_ItemTxTypeId.SelectedValue = dt2.Rows[0]["PurTypeId"].ToString();
                    //Gramboo.General.Setupcombo(Cmb_ItemTxTypeId, "PUR.PurchaseTaxTypeMaster", "PurTypeName", "PurTypeId", "IsActive='True'  and PurTypeId=" + dt2.Rows[0]["PurTypeId"].ToString());
                }
            }

        }

        private void grb_Enter(object sender, EventArgs e)
        {

        }

        void PendingLabelText()
        {
            if (cmb_Purchasetype.Text == "Result" || cmb_Purchasetype.Text == "Test") { ChkVchNo.Visible = true; } else { ChkVchNo.Visible = false; }
            //txt_purpose.Text = cmb_Purchasetype.Text.ToString() + " " + (transtype == "0" ? "Issue" : "Receipt");
            //cmb_Purchasetype.ToString();

            if (cmb_Purchasetype.SelectedIndex.ToString() == "7" & grbreceipt.Checked == true)
            {
                linkLabel1.Text = "Test Receipt Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.Text == "Result" & grbissue.Checked == true)
            {
                linkLabel1.Text = "Result Issue Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.Text == "Melting" & grbreceipt.Checked == true)
            {
                linkLabel1.Text = "Melting Receipt Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.Text == "Melting" & grbissue.Checked == true)
            {
                linkLabel1.Text = "Melting Issue Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.Text == "Melted OG" & grbissue.Checked == true)
            {
                linkLabel1.Text = "Melted OG Issue Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.SelectedIndex.ToString() == "12" /*& grbreceipt.Checked == true*/)
            {
                linkLabel1.Text = "Refinery " + (transtype == "0" ? "Issue" : "Receipt") + " Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.SelectedIndex.ToString() == "1")
            {
                linkLabel1.Text = "JobWork " + (transtype == "0" ? "Issue" : "Receipt") + " Pending";
                linkLabel1.Visible = true;

                if (grbreceipt.Checked == true && cmbpartytype.SelectedValue.ToString() == "1")
                {
                    lnkExport.Text = "Import";
                    lnkExport.Visible = true;
                }
                else if (grbissue.Checked == true && cmbpartytype.SelectedValue.ToString() == "3")
                {
                    lnkExport.Text = "Export";
                    lnkExport.Visible = true;
                }
            }
            else if (cmb_Purchasetype.SelectedIndex.ToString() == "2" /*& grbreceipt.Checked == true*/)
            {
                linkLabel1.Text = "Exhibition " + (transtype == "0" ? "Issue" : "Receipt") + " Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.SelectedIndex.ToString() == "3" /*& grbreceipt.Checked == true*/)
            {
                linkLabel1.Text = "Repair " + (transtype == "0" ? "Issue" : "Receipt") + " Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.SelectedIndex.ToString() == "4" /*& grbreceipt.Checked == true*/)
            {
                linkLabel1.Text = "BranchTransfer " + (transtype == "0" ? "Issue" : "Receipt") + " Pending";
                linkLabel1.Visible = true;

                if (grbreceipt.Checked == true)
                {
                    lnkExport.Text = "Import";
                    lnkExport.Visible = true;
                }
                else if (grbissue.Checked == true)
                {
                    lnkExport.Text = "Export";
                    lnkExport.Visible = true;
                }

            }
            else if (cmb_Purchasetype.SelectedIndex.ToString() == "5" /*& grbreceipt.Checked == true*/)
            {
                linkLabel1.Text = "Other " + (transtype == "0" ? "Issue" : "Receipt") + " Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.SelectedIndex.ToString() == "6" /*& grbreceipt.Checked == true*/)
            {
                linkLabel1.Text = "HallMarking " + (transtype == "0" ? "Issue" : "Receipt") + " Pending";
                linkLabel1.Visible = true;

                if (grbissue.Checked == true && cmbpartytype.SelectedValue.ToString() == "1")
                {
                    lnkExport.Text = "Export";
                    lnkExport.Visible = true;
                }
                else if (grbreceipt.Checked == true && cmbpartytype.SelectedValue.ToString() == "3")
                {
                    lnkExport.Text = "Import";
                    lnkExport.Visible = true;
                }
            }
            else if (cmb_Purchasetype.SelectedIndex.ToString() == "8" /*& grbreceipt.Checked == true*/)
            {
                linkLabel1.Text = "Inside JobWork " + (transtype == "0" ? "Issue" : "Receipt") + " Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.SelectedIndex.ToString() == "14" /*& grbreceipt.Checked == true*/)
            {
                linkLabel1.Text = "Certification " + (transtype == "0" ? "Issue" : "Receipt") + " Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.SelectedIndex.ToString() == "15" /*& grbreceipt.Checked == true*/)
            {
                linkLabel1.Text = "Approval " + (transtype == "0" ? "Issue" : "Receipt") + " Pending";
                linkLabel1.Visible = true;
            }
            else if (cmb_Purchasetype.SelectedIndex.ToString() == "16" )
            {
          
                if (grbreceipt.Checked == true  )
                {
                    linkLabel1.Text = "DepartmentTransfer " + (transtype == "0" ? "Issue" : "Receipt") + " Pending";
                    linkLabel1.Visible = true;
                    lnkExport.Text = "Import";
                    lnkExport.Visible = true;
                }
                else if (grbissue.Checked == true)
                {
                    lnkExport.Text = "Export";
                    lnkExport.Visible = true;
                }
            }
            else
            {
                linkLabel1.Text = "";
                linkLabel1.Visible = false;
            }
            if (cmbpartytype.SelectedValue != null)
            {
                if ((cmbpartytype.Text == "Supplier" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true) || (cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true))
                {
                    linkLabel1.Text = "";
                    linkLabel1.Visible = false;
                }
            }
            //loadgrid();
            procd();
        }

        private void txt_totalmc_TextChanged(object sender, EventArgs e)
        {

        }

        private void Cmb_ProdCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ChkJobOrderId_CheckedChanged(object sender, EventArgs e)
        {
            if (dgv_itemDetails.DataSource != null)
                dgv_itemDetails.Columns["JobNo"].Visible = ChkJobOrderNo.Checked;

            if (ChkJobOrderNo.Checked)
            {
                dgv_itemDetails.HiddenDataFields.Remove("JobNo");
                CmbJobNo.Visible = false;
            }
            else
            {
                dgv_itemDetails.HiddenDataFields.Add("JobNo");
                CmbJobNo.Visible = false;
            }
            dgv_itemDetails.Refresh();
        }


        private void CmbJobNo_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void CmbJobNo_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_itemDetails, CmbJobNo, dgv_itemDetails.Columns["JobOrderId"].Index);
        }

        private void CmbJobNo_KeyDown(object sender, KeyEventArgs e)
        {
            ComboKeydown(dgv_itemDetails, Cmb_ItemName, e);
        }

        private void cmb_Purchasetype_TextChanged(object sender, EventArgs e)
        {
            ////SetColumns(dgv_itemDetails);          
            //SmithDetails();
            //this.ListForm = new SmithReceiptList();
            //if (this.ListForm != null)
            //{
            //    ((SmithReceiptList)this.ListForm).Vouchertypeid = Convert.ToInt16(cmb_VoucherTypeId.SelectedValue == null ? "0" : cmb_VoucherTypeId.SelectedValue);
            //    ((SmithReceiptList)this.ListForm).Transtype = this.TransType;

            //}


        }
        public void vchno(bool ReloadGrid)
        {

            if (txtBranchID.Text == "" || txtcompId.Text == "")
                return;
            //BatchId();

            if (grbissue.Checked == true)
            {
                if (cmb_Purchasetype.Text == "Purchase")
                {
                    cmb_VoucherTypeId.SelectedValue = 143;
                }
                else if (cmb_Purchasetype.Text == "Certification")
                {
                    cmb_VoucherTypeId.SelectedValue = 196;
                }
                else if (cmb_Purchasetype.Text == "Melted OG")
                {
                    cmb_VoucherTypeId.SelectedValue = 191;
                }
                else if (cmb_Purchasetype.Text == "Exhibition")
                {
                    cmb_VoucherTypeId.SelectedValue = 144;
                }
                else if (cmb_Purchasetype.Text == "BranchTransfer")
                {
                    cmb_VoucherTypeId.SelectedValue = 145;
                }
                else if (cmb_Purchasetype.Text == "JobWork")
                {
                    cmb_VoucherTypeId.SelectedValue = 6;
                }
                else if (cmb_Purchasetype.Text == "Repair")
                {
                    cmb_VoucherTypeId.SelectedValue = 108;
                }
                else if (cmb_Purchasetype.Text == "Other")
                {
                    cmb_VoucherTypeId.SelectedValue = 150;
                }
                else if (cmb_Purchasetype.Text == "HallMarking")
                {
                    cmb_VoucherTypeId.SelectedValue = 143;
                }
                else if (cmb_Purchasetype.Text == "Test")
                {
                    if (ChkVchNo.Checked == true) { cmb_VoucherTypeId.SelectedValue = 220; }
                    else { cmb_VoucherTypeId.SelectedValue = 157; }
                }
                else if (cmb_Purchasetype.Text == "InsideJobWork")
                {
                    cmb_VoucherTypeId.SelectedValue = 159;
                }
                else if (cmb_Purchasetype.Text == "OldGold")
                {
                    cmb_VoucherTypeId.SelectedValue = 176;
                }
                else if (cmb_Purchasetype.Text == "Refinery")
                {
                    cmb_VoucherTypeId.SelectedValue = 182;
                }
                else if (cmb_Purchasetype.Text == "Result")
                {
                    if (ChkVchNo.Checked == true) { cmb_VoucherTypeId.SelectedValue = 218; }
                    else { cmb_VoucherTypeId.SelectedValue = 180; }
                }
                else if (cmb_Purchasetype.Text == "Melting")
                {
                    cmb_VoucherTypeId.SelectedValue = 178;
                }
                else if (cmb_Purchasetype.Text == "Approval")
                {
                    cmb_VoucherTypeId.SelectedValue = 222;
                }
                else if (cmb_Purchasetype.Text == "DepartmentTransfer")
                {
                    cmb_VoucherTypeId.SelectedValue = 270;
                }
            }
            else if (grbreceipt.Checked == true)
            {
                if (cmb_Purchasetype.Text == "JobWork")
                {
                    cmb_VoucherTypeId.SelectedValue = 15;
                }
                else if (cmb_Purchasetype.Text == "Repair")
                {
                    cmb_VoucherTypeId.SelectedValue = 109;
                }
                else if (cmb_Purchasetype.Text == "Purchase")
                {
                    cmb_VoucherTypeId.SelectedValue = 146;
                }
                else if (cmb_Purchasetype.Text == "Certification")
                {
                    cmb_VoucherTypeId.SelectedValue = 197;
                }
                else if (cmb_Purchasetype.Text == "Melted OG")
                {
                    cmb_VoucherTypeId.SelectedValue = 192;
                }
                else if (cmb_Purchasetype.Text == "Exhibition")
                {
                    cmb_VoucherTypeId.SelectedValue = 147;
                }
                else if (cmb_Purchasetype.Text == "BranchTransfer")
                {
                    cmb_VoucherTypeId.SelectedValue = 148;
                }
                else if (cmb_Purchasetype.Text == "HallMarking")
                {
                    cmb_VoucherTypeId.SelectedValue = 146;
                }
                else if (cmb_Purchasetype.Text == "Other")
                {
                    cmb_VoucherTypeId.SelectedValue = 149;
                }
                else if (cmb_Purchasetype.Text == "OldGold")
                {
                    cmb_VoucherTypeId.SelectedValue = 177;
                }
                else if (cmb_Purchasetype.Text == "Refinery")
                {
                    cmb_VoucherTypeId.SelectedValue = 183;
                }
                else if (cmb_Purchasetype.Text == "Result")
                {
                    if (ChkVchNo.Checked == true) { cmb_VoucherTypeId.SelectedValue = 219; }
                    else { cmb_VoucherTypeId.SelectedValue = 181; }
                }
                else if (cmb_Purchasetype.Text == "Melting")
                {
                    cmb_VoucherTypeId.SelectedValue = 179;
                }
                else if (cmb_Purchasetype.Text == "Test")
                {
                    if (ChkVchNo.Checked == true) { cmb_VoucherTypeId.SelectedValue = 221; }
                    else { cmb_VoucherTypeId.SelectedValue = 158; }
                }
                else if (cmb_Purchasetype.Text == "InsideJobWork")
                {
                    cmb_VoucherTypeId.SelectedValue = 160;
                }
                else if (cmb_Purchasetype.Text == "Approval")
                {
                    cmb_VoucherTypeId.SelectedValue = 223;
                }
                else if (cmb_Purchasetype.Text == "DepartmentTransfer")
                {
                    cmb_VoucherTypeId.SelectedValue = 271;
                }
            }
            if (cmbpartytype.SelectedValue == null)
                return;

            if (cmbpartytype.SelectedValue.ToString() == "3")
            {
                if (grbissue.Checked == true)
                {
                    if (cmb_Purchasetype.Text == "JobWork")
                    {
                        cmb_VoucherTypeId.SelectedValue = 169;
                    }
                    else if (cmb_Purchasetype.Text == "Repair")
                    {
                        cmb_VoucherTypeId.SelectedValue = 171;
                    }
                    else if (cmb_Purchasetype.Text == "OldGold")
                    {
                        cmb_VoucherTypeId.SelectedValue = 176;
                    }
                    else if (cmb_Purchasetype.Text == "Certification")
                    {
                        cmb_VoucherTypeId.SelectedValue = 196;
                    }
                    else if (cmb_Purchasetype.Text == "Result")
                    {
                        if (ChkVchNo.Checked == true) { cmb_VoucherTypeId.SelectedValue = 218; }
                        else { cmb_VoucherTypeId.SelectedValue = 180; }
                    }
                    else if (cmb_Purchasetype.Text == "Test")
                    {
                        cmb_VoucherTypeId.SelectedValue = 157;
                    }
                    
                    else if (cmb_Purchasetype.Text == "Melted OG")
                    {
                        cmb_VoucherTypeId.SelectedValue = 191;
                    }
                    else if (cmb_Purchasetype.Text == "Refinery")
                    {
                        cmb_VoucherTypeId.SelectedValue = 285;
                    }
                    else if (cmb_Purchasetype.Text == "Exhibition")
                    {
                        cmb_VoucherTypeId.SelectedValue = 289;
                    }
                    else if (cmb_Purchasetype.Text == "Melting")
                    {
                        cmb_VoucherTypeId.SelectedValue = 291;
                    }
                    else if (cmb_Purchasetype.Text == "Approval")
                    {
                        cmb_VoucherTypeId.SelectedValue = 294; 
                    }
                    else if (cmb_Purchasetype.Text == "Other")
                    {
                        cmb_VoucherTypeId.SelectedValue = 355;
                    }

                    else
                    {
                        cmb_VoucherTypeId.SelectedValue = 167;
                    }

                }
                else if (grbreceipt.Checked == true)
                {
                   
                    if (cmb_Purchasetype.Text == "JobWork")
                    {
                        cmb_VoucherTypeId.SelectedValue = 170;
                    }
                    else if (cmb_Purchasetype.Text == "Repair")
                    {
                        cmb_VoucherTypeId.SelectedValue = 172;
                    }
                    else if (cmb_Purchasetype.Text == "OldGold")
                    {
                        cmb_VoucherTypeId.SelectedValue = 177;
                    }
                    else if (cmb_Purchasetype.Text == "Certification")
                    {
                        cmb_VoucherTypeId.SelectedValue = 197;
                    }
                    else if (cmb_Purchasetype.Text == "Result")
                    {
                        if (ChkVchNo.Checked == true) { cmb_VoucherTypeId.SelectedValue = 219; }
                        else { cmb_VoucherTypeId.SelectedValue = 181; }
                    }
                    else if (cmb_Purchasetype.Text == "Test")
                    {
                        cmb_VoucherTypeId.SelectedValue = 158;
                    }
                    else if (cmb_Purchasetype.Text == "Refinery")
                    {
                        cmb_VoucherTypeId.SelectedValue = 284;
                    }
                    else if (cmb_Purchasetype.Text == "Melted OG")
                    {
                        cmb_VoucherTypeId.SelectedValue = 192;
                    }
                    else if (cmb_Purchasetype.Text == "Exhibition")
                    {
                        cmb_VoucherTypeId.SelectedValue = 288;
                    }
                    else if (cmb_Purchasetype.Text == "Melting")
                    {
                        cmb_VoucherTypeId.SelectedValue = 290;
                    }
                    else if (cmb_Purchasetype.Text == "Approval")
                    {
                        cmb_VoucherTypeId.SelectedValue = 295;
                    }
                    else if (cmb_Purchasetype.Text == "Other")
                    {
                        cmb_VoucherTypeId.SelectedValue = 356;
                    }

                    else
                    {
                        cmb_VoucherTypeId.SelectedValue = 168;
                    }
                }
            }
            if (cmbpartytype.SelectedValue.ToString() == "4")
            {
                if (grbissue.Checked == true)
                {
                    if (cmb_Purchasetype.Text == "DepartmentTransfer")
                    {
                        cmb_VoucherTypeId.SelectedValue = 270;
                    }
                }
                else if (grbreceipt.Checked == true)
                {
                    if (cmb_Purchasetype.Text == "DepartmentTransfer")
                    {
                        cmb_VoucherTypeId.SelectedValue = 271;
                    }
                }
            }
            if (!IsEditMode)
                TxtVoucherNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtp_dt.Value,
            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));

            if (!IsEditMode)
            {
                if (ReloadGrid == true)
                {
                    SetColumns(dgv_itemDetails);
                }
            }

            SAFA.Forms.STK.SmithReceiptList frm = new SAFA.Forms.STK.SmithReceiptList();
            frm.Vouchertypeid = (int)cmb_VoucherTypeId.SelectedValue;
            frm.PurchaseTypeId = (int)cmb_Purchasetype.SelectedIndex;
            frm.transtype = this.TransType;
            frm.DataEntryForm = this;
            this.ListForm = frm;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void ChkBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (dgv_itemDetails.DataSource != null)
                dgv_itemDetails.Columns["BranchName"].Visible = ChkBranch.Checked;


            if (ChkBranch.Checked)
            {
                dgv_itemDetails.HiddenDataFields.Remove("BranchName");
                cmbbranch.Visible = false;
            }
            else
            {

                dgv_itemDetails.HiddenDataFields.Add("BranchName");

                cmbbranch.Visible = false;
            }
            dgv_itemDetails.Refresh();
        }

        private void dgv_itemDetails_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Cmb_ProdCode.Focus();
        }

        private void procd()
        {
            if (dgv_itemDetails.DataSource == null)
                return;

            if (cmb_Purchasetype.SelectedIndex.ToString() == "1")
            {
                //if (grb.Value == "1")
                //{
                //    dgv_itemDetails.Columns["ProdCode"].Visible = false;
                //}
                //else
                //{

                //    dgv_itemDetails.Columns["ProdCode"].Visible = true;

                //}
                if (dgv_itemDetails.CurrentRow != null)
                    dgv_itemDetails.CurrentRow.Cells["Rate"].Value = "0";

                dgv_itemDetails.Columns["Rate"].Visible = false;

                dgv_itemDetails.Columns["MetalCash"].Visible = false;
            }
            else
            {
                //    if (grb.Value == "1")
                //    {
                //        dgv_itemDetails.Columns["ProdCode"].Visible = false;
                //    }
                //    else
                //    {
                //        dgv_itemDetails.Columns["ProdCode"].Visible = true;
                //    }
                //dgv_itemDetails.Columns["ProdCode"].Visible = false;
                if (cmb_Purchasetype.Text != "")
                {
                    dgv_itemDetails.Columns["Rate"].Visible = true;

                    dgv_itemDetails.Columns["MetalCash"].Visible = true;
                }
            }
        }

        private void CmbBatchNo_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_itemDetails, CmbBatchNo, dgv_itemDetails.Columns["BatchId"].Index);
        }
        void BatchNovis()
        {
            //if (cmb_Purchasetype.Text == "OldGold")
            //{
            //    dgv_itemDetails.Columns["BatchNo"].Visible = true;
            //}
            //else
            //{
            //    dgv_itemDetails.Columns["BatchNo"].Visible = false;
            //}
        }
        private void FillRowWithBatchid(Gramboo.Controls.GrbDataGridView dgv, int rowindex, string type, Int64 floorid, Int64 BatchId, Int64 ItemId, bool Isreceipt)
        {

            int branchid = 0;
            string branchname = "";


            branchid = Convert.ToInt32(dgv.Rows[rowindex].Cells["BranchID"].Value.ToString() == "" ? "0" : dgv.Rows[rowindex].Cells["BranchID"].Value.ToString());
            branchname = dgv.Rows[rowindex].Cells["BranchName"].Value.ToString() == "" ? "" : dgv.Rows[rowindex].Cells["BranchName"].Value.ToString();

            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                     ("SELECT * FROM PROD.AddBatchNotoStockTransfer('" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'," + BatchId + "," + ItemId + "," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
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
                    dgv.Rows[rowindex].Cells["BatchId"].Selected = true;
                    CmbBatchNo.Focus();
                    dgv_itemDetails.CurrentRow.Cells["Gwt"].ReadOnly = false;
                }
                else
                {
                    CmbBatchNo.ShowMessage("Not In Stock");
                }
            }
        }
        void SupplierBranchid()
        {
            if (cmbpartyname.SelectedValue == null)
                return;
            if (grbissue.Checked == true && cmbpartyname.SelectedValue != null && cmbpartytype.Text == "Supplier")
            {
                using (System.Data.DataTable dt2 = DBConn.GetData(new SqlCommand
               ("SELECT isnull(SuppBranch_id,0)as SuppBranch_id  FROM PUR.SupplierMaster  WHERE SuppId =" + cmbpartyname.SelectedValue)).Tables[0])
                {
                    if (dt2.Rows.Count > 0)
                    {

                        SuppBranch = dt2.Rows[0][0].ToString();
                        TxtTranferBranchid.Text = SuppBranch;
                    }
                }
            }
            else if (grbissue.Checked == true && cmbpartyname.SelectedValue != null && cmbpartytype.Text == "Customer")
            {
                using (System.Data.DataTable dt2 = DBConn.GetData(new SqlCommand
             ("SELECT isnull(custbranchid,0)as custbranchid  FROM crm.VCustomerMaster  WHERE Custid =" + cmbpartyname.SelectedValue)).Tables[0])
                {
                    if (dt2.Rows.Count > 0)
                    {

                        CustBaranchid = dt2.Rows[0][0].ToString();
                        TxtTranferBranchid.Text = CustBaranchid;
                    }
                }
            }

            if ((cmbpartytype.Text == "Supplier"))
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                        ("SELECT ISNULL(SuppMCmode,'') as SuppMCmode FROM PUR.SupplierMaster WHERE SuppId='" + cmbpartyname.SelectedValue.ToString() + "'")).Tables[0])
                    {
                    if (dt.Rows.Count > 0)
                    {
                        SuppMCmode = dt.Rows[0]["SuppMCmode"].ToString();
                        Grb_MC_Mode.Value = dt.Rows[0]["SuppMCmode"].ToString();
                        if (SuppMCmode == "C") { grb_cash.Checked = true; } else { grb_wst.Checked = true; }
                    }
                }
            }

            if (SoftwareSettings.CompName == "MANJALI")
            {
                if ((cmbpartytype.Text == "Supplier" && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true)) { grb_wst.Checked = true; }
                else if ((cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbissue.Checked == true)) { grb_cash.Checked = true; }
                else if ((cmbpartytype.Text == "Supplier" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true)) { grb_cash.Checked = true; }
                else if ((cmbpartytype.Text == "Customer" && cmb_Purchasetype.Text == "JobWork" && grbreceipt.Checked == true)) { grb_wst.Checked = true; }
            }
          }
        void SendData()
        {
            if (!SAFA.Classes.Common.ChkServerType(DBConn, Convert.ToInt32(txtBranchID.Text)) && SoftwareSettings.CompName != "SAFA")
            {
                SqlConnection connectionstring = new SqlConnection(DBConn.ConnectionProperties.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connectionstring;
                cmd.CommandText = "STK.InsertPendingTransaction";
                cmd.CommandType = CommandType.StoredProcedure;
                connectionstring.Open();
                cmd.Parameters.AddWithValue("@TransferId", txtEntryNo.Text);
                cmd.Parameters.AddWithValue("@TransferFrom", "STK");
                cmd.Parameters.AddWithValue("@Company_Id ", txtcompId.Text);
                cmd.Parameters.AddWithValue("@Branch_Id", txtBranchID.Text);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
                connectionstring.Close();
            }
            else
            {

                int c = 0;
                foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                {
                    if (r.Cells["ProdCodeId"].Value.ToString() != "" && r.Cells["ProdCodeId"].Value.ToString().Trim().Length >= 2)
                    {
                        c++;
                        break;
                    }
                }

                if (c > 0) { return; }

                if (cmb_Purchasetype.Text == "OldGold" || cmb_Purchasetype.Text == "JobWork" || cmb_Purchasetype.Text == "Melting" || cmb_Purchasetype.Text == "Refinery")
                {
                    if (grbissue.Checked == true && cmbpartyname.SelectedValue != null && cmbpartytype.Text == "Supplier" && SAFA.Classes.SoftwareSettings.IsShowRoom == true && SuppBranch == "107")
                    {

                        SqlConnection connectionstring = new SqlConnection(DBConn.ConnectionProperties.ConnectionString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connectionstring;
                        cmd.CommandText = "PROD.InsertPendingStockTransferTable";
                        cmd.CommandType = CommandType.StoredProcedure;
                        connectionstring.Open();
                        cmd.Parameters.AddWithValue("@Entryid", txtEntryNo.Text);
                        cmd.Parameters.AddWithValue("@TransType", grb.Value);
                        cmd.Parameters.AddWithValue("@Branch_Id ", txtBranchID.Text);
                        cmd.Parameters.AddWithValue("@Type", "Save");
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                        connectionstring.Close();
                    }
                }
                if (cmb_Purchasetype.Text == "Melting" || cmb_Purchasetype.Text == "Result" || cmb_Purchasetype.Text == "JobWork" || cmb_Purchasetype.Text == "Refinery")
                {
                    if (grbissue.Checked == true && cmbpartyname.SelectedValue != null && cmbpartytype.Text == "Customer" && SAFA.Classes.SoftwareSettings.IsShowRoom == false)
                    {
                        SqlConnection connectionstring = new SqlConnection(DBConn.ConnectionProperties.ConnectionString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connectionstring;
                        cmd.CommandText = "PROD.InsertPendingStockTransferTable";
                        cmd.CommandType = CommandType.StoredProcedure;
                        connectionstring.Open();
                        cmd.Parameters.AddWithValue("@Entryid", txtEntryNo.Text);
                        cmd.Parameters.AddWithValue("@TransType", grb.Value);
                        cmd.Parameters.AddWithValue("@Branch_Id ", txtBranchID.Text);
                        cmd.Parameters.AddWithValue("@Type", "Save");
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                        connectionstring.Close();
                    }
                }
            }
        }

        public void SetColumns(Gramboo.Controls.GrbDataGridView dgv)
        {

            flag = true;
            if (cmb_Purchasetype.Text == null)
                return;
            List<string> datafields = new List<string>();
            List<string> Hiddenfields = new List<string>();
            string[] summaryfields;

            if (cmb_Purchasetype.Text.ToString() == "Refinery" && grbreceipt.Checked == true)
            {
                datafields = new List<string>() {"EntryId","TransId","BranchId","BranchName","PurityId","JobNo","JobOrderID","ProdCodeId","ProdCode","BatchNo","BatchId","HUId", "ItemID", "ModelId", "[Item Name]", "Purity",
           "Nos","IssueWt","IssueTouch","IssueActualwt","Gwt","StNo","StWt","Touch","DiaNo","Diawt","DiaCash","StCtWt","StCash","Mud","NetWt","Rate","MetalCash", "ActualWt","PureWt","Wst","MC","CertificationCharge","Amount", "WastageWt","TotalAmount","[Mc Perc]","[Wst Perc]","IssueId","IsReceipt","PurId","SalesVA","PurchaseVA","TestResult","DiaLoss","StLoss","Loss","TransferTransId","IssId", "RecId","TrackingID","IdType"};

            }
            else
            {
                datafields = new List<string>() {"EntryId","TransId","[Reference No]","BranchId","BranchName","PurityId","JobNo","JobOrderID","ProdCodeId","ProdCode","designcode","CustName","BatchNo","BatchId","HUId", "ItemID", "ModelId", "[Item Name]", "Purity",
           "Nos","IssueWt","Gwt","StNo","StWt","StCtWt","StRate","StCash","DiaNo","Diawt","DiaCash","Mud","NetWt","Touch","Rate","MetalCash", "ActualWt","PureWt","Wst","MCRate","MC","CertificationCharge","Amount", "WastageWt","TotalAmount","[Mc Perc]","[Wst Perc]","IssueId","IsReceipt","PurId","SalesVA","PurchaseVA","TestResult","DiaLoss","StLoss","Loss","TransferTransId","IssId", "RecId","TrackingID","IdType"};

            }

            summaryfields = new string[] { "NetWt", "Nos", "Gwt", "DiaNo", "StNo", "StWt", "Diawt", "DiaCash", "StCash", "MC", "Wst", "WastageWt", "ActualWt", "PureWt", "TotalAmount", "MetalCash" };
            if ((cmb_Purchasetype.Text.ToString() == "Test" || cmb_Purchasetype.Text.ToString() == "Result") && ChkVchNo.Checked == true)
            {
                Hiddenfields = new List<string>() { "EntryId","TransId","Reference No","BranchId","BranchName","PurityId","JobNo","JobOrderID","ProdCodeId","ProdCode","BatchId", "ItemID", "ModelId", "[Item Name]", "Purity",
                "Nos","IssueWt","Gwt","DiaNo","StNo","StWt","StCash","Diawt","DiaCash","Mud","NetWt","Touch","Rate","MetalCash", "ActualWt","PureWt","Wst","MC","CertificationCharge","Amount", "WastageWt","TotalAmount","Mc Perc","Wst Perc","IssueId","IsReceipt","PurId","SalesVA","PurchaseVA","DiaLoss","StLoss","Loss","TransferTransId","IssId", "RecId","TrackingID","IdType"};
            }
            else if (cmb_Purchasetype.Text.ToString() == "Test" && grbreceipt.Checked == true)
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "ProdCode", "BatchId", "ItemID", "PureWt", "PurityId", "ModelId", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "CertificationCharge", "Amount", "DiaNo", "StNo", "Diawt", "DiaCash", "StCash", "Mc Perc", "Wst Perc", "SalesVA", "PurchaseVA", "DiaLoss", "StLoss", "TransferTransId", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else if (cmb_Purchasetype.Text.ToString() == "Result")
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "ProdCode", "BatchId", "ItemID", "PureWt", "PurityId", "ModelId", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "CertificationCharge", "Amount", "Loss", "DiaNo", "StNo", "Diawt", "DiaCash", "StCash", "MC", "Mc Perc", "Wst Perc", "SalesVA", "PurchaseVA", "DiaLoss", "StLoss", "TransferTransId", "IssueWt", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else if (cmb_Purchasetype.Text.ToString() == "JobWork" && grbreceipt.Checked == true)
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "BatchId", "ItemID", "PureWt", "PurityId", "ModelId", /*"Mc Perc", "Wst Perc",*/ "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "StNo", "CertificationCharge", "Amount"/*, "WastageWt"*/, "SalesVA", "PurchaseVA", "TestResult"/*,"DiaLoss", "StLoss", "Loss"*/, "TransferTransId", "IssueWt", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else if (cmb_Purchasetype.Text.ToString() == "JobWork")
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "BatchId", "ItemID", "PureWt", "PurityId", "ModelId", /*"Mc Perc", "Wst Perc",*/ "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "StNo", "CertificationCharge", "Amount"/*, "WastageWt"*/, "SalesVA", "PurchaseVA", "TestResult", "DiaLoss", "StLoss", "Loss", "TransferTransId", "IssueWt", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else if (cmbpartytype.Text.ToString() == "Customer" && cmb_Purchasetype.Text.ToString() == "JobWork")
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "BatchId", "ItemID", "PureWt", "PurityId", "ModelId", "Mc Perc", "Wst Perc", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "StNo", "CertificationCharge", "Amount", "TestResult", "Loss", "BranchName", "SalesVA", "PurchaseVA", "DiaLoss", "StLoss", "TransferTransId", "IssueWt", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else if (cmb_Purchasetype.Text.ToString() == "Test" && grbreceipt.Checked == false)
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "ProdCode", "BatchId", "ItemID", "PureWt", "PurityId", "ModelId", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "CertificationCharge", "Amount", "TestResult", "Loss", "DiaNo", "StNo", "Diawt", "DiaCash", "StCash", "Mc Perc", "Wst Perc", "SalesVA", "PurchaseVA", "DiaLoss", "StLoss", "TransferTransId", "IssueWt", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else if (cmb_Purchasetype.Text.ToString() == "Melting" && grbreceipt.Checked == false || cmb_Purchasetype.Text.ToString() == "Refinery" && grbreceipt.Checked == false || cmb_Purchasetype.Text.ToString() == "Melted OG" && grbreceipt.Checked == false)
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "ProdCode", "BatchId", "ItemID", "PureWt", "PurityId", "ModelId", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "CertificationCharge", "Amount", "TestResult", "Loss", "DiaNo", "StNo", "Diawt", "DiaCash", "StCash", "MC", "Mc Perc", "Wst Perc", "SalesVA", "PurchaseVA", "DiaLoss", "StLoss", "TransferTransId", "IssueWt", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else if (cmb_Purchasetype.Text.ToString() == "Melting" && grbreceipt.Checked == true || cmb_Purchasetype.Text.ToString() == "Melted OG" && grbreceipt.Checked == true)
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "ProdCode", "BatchId", "ItemID", "PureWt", "PurityId", "ModelId", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "CertificationCharge", "Amount", "TestResult", "DiaNo", "StNo", "Diawt", "DiaCash", "StCash", "MC", "Mc Perc", "Wst Perc", "SalesVA", "PurchaseVA", "DiaLoss", "StLoss", "TransferTransId", "IssId", "RecId", "TrackingID", "IdType" };
            }
            else if (cmb_Purchasetype.Text.ToString() == "Refinery" && grbreceipt.Checked == true)
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "ProdCode", "BatchId", "ItemID", "PureWt", "PurityId", "ModelId", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "CertificationCharge", "Amount", "TestResult", "DiaNo", "StNo", "Diawt", "DiaCash", "StCash", "MC", "Mc Perc", "Wst Perc", "Wst", "WastageWt", "SalesVA", "PurchaseVA", "DiaLoss", "StLoss", "TransferTransId", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else if (cmb_Purchasetype.Text.ToString() == "OldGold")
            {
                Hiddenfields = new List<string>() { "EntryId","TransId","Reference No","BranchId","BranchName","PurityId","JobNo","JobOrderID","ProdCodeId","ProdCode","BatchId", "ItemID", "ModelId", "Purity",
           "DiaNo","StNo","Diawt","DiaCash","StCash","Purity","Rate","MetalCash","PureWt","Wst","MC","CertificationCharge","Amount", "WastageWt","Mc Perc","Wst Perc","IssueId","IsReceipt","PurId","SalesVA","PurchaseVA","TestResult","DiaLoss","StLoss","Loss", "TransferTransId", "IssueWt","IssId", "RecId","Mud","TrackingID","IdType" };
                chkbarcode.Visible = false;
            }
            else if (cmb_Purchasetype.Text.ToString() == "Exhibition" && grbreceipt.Checked == true)
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "BatchId", "BatchNo", "ItemID", "ModelId", "PureWt", "PurityId", "Mc Perc", "Wst Perc", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "StNo", "StNo", "CertificationCharge", "Amount", "TestResult", "DiaLoss", "StLoss", "Loss", "TransferTransId", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else if (cmb_Purchasetype.Text.ToString() == "Exhibition" && grbreceipt.Checked == true)
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "BatchId", "BatchNo", "ItemID", "ModelId", "PureWt", "PurityId", "Mc Perc", "Wst Perc", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "StNo", "CertificationCharge", "Amount", "TestResult", "DiaLoss", "StLoss", "Loss", "SalesVA", "PurchaseVA", "TransferTransId", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else if ((cmb_Purchasetype.SelectedIndex.ToString() == "3" || cmb_Purchasetype.SelectedIndex.ToString() == "6") && grbreceipt.Checked == true)
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "BatchId", "BatchNo", "ItemID", "ModelId", "PureWt", "PurityId", "Mc Perc", "Wst Perc", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "StNo", "CertificationCharge", "Amount", "TestResult", "DiaLoss", "StLoss", "SalesVA", "PurchaseVA", "TransferTransId", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else if ((cmb_Purchasetype.SelectedIndex.ToString() == "3" || cmb_Purchasetype.SelectedIndex.ToString() == "4" || cmb_Purchasetype.SelectedIndex.ToString() == "6" || cmb_Purchasetype.SelectedIndex.ToString() == "14" || cmb_Purchasetype.SelectedIndex.ToString() == "5") && grbreceipt.Checked == true)
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "BatchId", "BatchNo", "ItemID", "ModelId", "PureWt", "PurityId", "Mc Perc", "Wst Perc", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "StNo", "CertificationCharge", "Amount", "TestResult", "DiaLoss", "StLoss", "SalesVA", "PurchaseVA", "TransferTransId", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }
            else
            {
                Hiddenfields = new List<string>() { "EntryId", "TransId", "Reference No", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "BatchId", "BatchNo", "ItemID", "ModelId", "PureWt", "PurityId", "Mc Perc", "Wst Perc", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo", "StNo", "CertificationCharge", "Amount", "TestResult", "DiaLoss", "StLoss", "Loss", "SalesVA", "PurchaseVA", "TransferTransId", "IssueWt", "IssId", "RecId", "Mud", "TrackingID", "IdType" };
            }

            string TransType; if (grb.Value.ToString() == "False") { TransType = "0"; } else if (grb.Value.ToString() == "True") { TransType = "1"; } else { TransType = grb.Value.ToString(); }

            //if (SoftwareSettings.CompName == "MANJALI")
            //{
            Hiddenfields = new List<string>();
            Hiddenfields = SetHiddenColumns(cmb_Purchasetype.SelectedIndex.ToString(), TransType.ToString(), (cmbpartytype.SelectedValue == null ? "-5" : cmbpartytype.SelectedValue.ToString()));
            //}

            if (SoftwareSettings.HasJobNo == false) { Hiddenfields.AddRange(new List<string> { "JobOrderId", "JobNo" }); }
            if (SoftwareSettings.StoneCtWt == false) { Hiddenfields.AddRange(new List<string> { "StCtWt" }); }
            else { Hiddenfields.AddRange(new List<string> { "StWt" }); }
            //Hiddenfields.Remove("DiaNo");

            dgv.DataFields = datafields;
            dgv.HiddenDataFields = Hiddenfields;
            dgv.SummaryColumns = summaryfields;

            //dgviewAddData.DataFields = datafields;
            //dgviewAddData.HiddenDataFields = Hiddenfields;
            //dgviewAddData.SummaryColumns = summaryfields;
            //dgviewAddData.DataSource = null;
            dgv.DataSource = null;
            dgv.Fill(new Table(SAFA.Classes.Common.DbName, "STK", "VGoldStockTransferDetails", true), "1=2");
            if (dgv.Columns.Contains("col_AutoSlno"))
            {

                dgv.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            }
            dgv.AllowUserToAddRows = false;
            if (dgv.Rows.Count == 0)
            {

                ((System.Data.DataTable)dgv.DataSource).Rows.Add(((System.Data.DataTable)dgv.DataSource).NewRow());
            }
            dgv.IsDataEntryGrid = true;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.BeginEdit(false);
            flag = false;

        }

        void DisplayProdcodeColumn()
        {
            return;

            if (dgv_itemDetails.DataSource != null)
                dgv_itemDetails.Columns["Prodcode"].Visible = chkbarcode.Checked;

            if (chkbarcode.Checked == true)
            {
                dgv_itemDetails.HiddenDataFields.Remove("Prodcode");
                //Gramboo.General.Setupcombo(Cmb_ProdCode, "STK.ProdCodeMaster", "prodCode", "ProdCodeId", "IsActive='True'");
            }
            else
            {
                dgv_itemDetails.HiddenDataFields.Add("Prodcode");
            }
            dgv_itemDetails.Refresh();
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            if (panelTestPending.Visible == false)
            {
                panelTestPending.Parent = this;
                panelTestPending.Visible = true;
                panelTestPending.BringToFront();
                panelTestPending.Show();
                panelTestPending.BringToFront();
                panelTestPending.Location = new Point(24, 16);
                loadPendingTestIssuedata();
            }

            else
            {
                panelTestPending.Visible = false;
                panelTestPending.SendToBack();
                panelTestPending.Hide();
            }

        }

        public void loadPendingTestIssuedata()
        {
            if (cmbpartytype.Text == "" || cmbpartyname.SelectedValue == null)
                return;

            if (cmb_Purchasetype.Text == "Refinery" || cmb_Purchasetype.Text == "Melting" || cmb_Purchasetype.Text == "Test" & grbreceipt.Checked == true || cmb_Purchasetype.Text == "Result" & grbissue.Checked == true || cmb_Purchasetype.Text == "Melted OG" & grbissue.Checked == true)
            {
                dgviewPending.AutoGenerateColumns = true;
                dgviewPending.ShowSerialNo = true;
                dgviewPending.SummaryColumns = new string[] { };
                dgviewPending.HiddenDataFields = new List<string>() { "EntryId", "BatchId", "ItemID", "TransId" };
                dgviewPending.DataSource = this.DBConn.GetData(new SqlCommand("Select cast('false' as bit) as [Select],EntryId,VchNo,VchDate,BatchId,BatchNo,ItemID,ItemName,Nos,Gwt,Stwt,Netwt,BatchId as TransId from PROD.BatchIssueAndReceiptPending('" + cmb_Purchasetype.SelectedIndex.ToString() + "','" + grb.Value + "','" + cmbpartytype.SelectedValue.ToString() + "','" + cmbpartyname.SelectedValue.ToString() + "'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0];
            }
            else if (cmb_Purchasetype.SelectedIndex == 1 || cmb_Purchasetype.SelectedIndex == 2 || cmb_Purchasetype.SelectedIndex == 4 || cmb_Purchasetype.SelectedIndex == 3 || cmb_Purchasetype.SelectedIndex == 6 || cmb_Purchasetype.SelectedIndex == 14 || cmb_Purchasetype.SelectedIndex == 8 || cmb_Purchasetype.SelectedIndex == 5 || cmb_Purchasetype.SelectedIndex == 15 || cmb_Purchasetype.SelectedIndex == 16)
            {
                if (/*grbreceipt.Checked == true &&*/ Cmb_StkType.SelectedValue != null)
                {
                    dgviewPending.AutoGenerateColumns = true;
                    dgviewPending.ShowSerialNo = true;
                    dgviewPending.SummaryColumns = new string[] { };
                    dgviewPending.HiddenDataFields = new List<string>() { "EntryId", "TransId", "ItemID", "PartyName" };
                    if (cmb_Purchasetype.SelectedIndex == 1 && (cmbpartytype.SelectedIndex == 3 && grbreceipt.Checked == true))
                    {
                        dgviewPending.ReadOnly = true;
                        panelTestPending.Size = new Size(900, 500);
                        dgviewPending.HiddenDataFields = new List<string>() { "EntryId", "TransId", "ItemID", "Select", "PartyName" };
                    }
                    dgviewPending.DataSource = this.DBConn.GetData(new SqlCommand("Select cast('false' as bit) as [Select],EntryId,TransId,VchNo,CustName,VchDate,PartyName,ItemID,ItemName,Nos,Gwt,Diawt,Stwt,Netwt,ActWt from STK.StockTransferIssueAndReceiptPending('" + cmb_Purchasetype.SelectedIndex.ToString() + "','" + grb.Value + "','" + cmbpartytype.SelectedValue.ToString() + "','" + cmbpartyname.SelectedValue.ToString() + "','" + Cmb_StkType.SelectedValue.ToString() + "','STK'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0];
                }
                else { if (Cmb_StkType.SelectedValue == null) { panelTestPending.Visible = false; Cmb_StkType.ShowMessage("Please Choose StockType to Continue"); } }

            }


        }

        void LoadComboReference()
        {
            if (cmbpartyname.SelectedValue != null && Cmb_StkType.SelectedValue != null)
            {
                if (txtBranchID.Text == "")
                    return;
                CmbRefNo.DataSource = null;
                string str;
                SqlCommand cmd = new SqlCommand();
                str = "Select EntryId,TransId,VchNo,VchDate,CustName,StockType,PartyName,ItemName,Nos,Gwt,Diawt,Stwt,Netwt,ActWt from STK.StockTransferIssueAndReceiptPending('" + cmb_Purchasetype.SelectedIndex.ToString() + "','" + grb.Value + "','" + (cmbpartytype.SelectedValue != null ? cmbpartytype.SelectedValue.ToString() : "1") + "','" + (cmbpartyname.SelectedValue != null ? cmbpartyname.SelectedValue.ToString() : "0") + "', '" + Cmb_StkType.SelectedValue.ToString() + "','STK'," + txtcompId.Text + "," + txtBranchID.Text + ")";
                cmd.CommandText = str;
                cmd.CommandTimeout = 0;
                CmbRefNo.DisplayMember = "VchNo";
                CmbRefNo.ValueMember = "TransId";
                CmbRefNo.DataSource = DBConn.GetData(cmd, "Pending").Tables[0];
            }
        }
        void LoadPendingCombo()
        {
            if (cmb_Purchasetype.SelectedIndex.ToString() != "1")
                return;

            CmbReference.SelectedValueChanged -= CmbReference_SelectedValueChanged;
            CmbReference.DataSource = null; CmbReference.Text = "";
            if (cmbpartyname.SelectedValue != null && Cmb_StkType.SelectedValue != null)
            {
                if (txtBranchID.Text == "")
                    return;
                int i;
                bool isNumber = int.TryParse(grb.Value, out i);
                if (isNumber == false)
                    return;
                string str;
                SqlCommand cmd = new SqlCommand();
                str = "Select EntryId,TransId,VchNo,VchDate,CustName,Days_,StockType,PartyName,ItemName,Nos,Gwt,Diawt,Stwt,Netwt,ActWt from STK.StockTransferIssueAndReceiptPending('" + cmb_Purchasetype.SelectedIndex.ToString() + "','" + grb.Value + "','" + (cmbpartytype.SelectedValue != null ? cmbpartytype.SelectedValue.ToString() : "1") + "','" + (cmbpartyname.SelectedValue != null ? cmbpartyname.SelectedValue.ToString() : "0") + "', '" + Cmb_StkType.SelectedValue.ToString() + "','STK'," + txtcompId.Text + "," + txtBranchID.Text + ")";
                cmd.CommandText = str;
                cmd.CommandTimeout = 0;
                CmbReference.DisplayMember = "VchNo";
                CmbReference.ValueMember = "TransId";
                CmbReference.DataSource = DBConn.GetData(cmd, "Pending").Tables[0];
                //CmbReference.DataSource = null;
            }
            CmbReference.SelectedValue = 0;
            CmbReference.Text = ""; PendGrid();
            CmbReference.SelectedValueChanged += CmbReference_SelectedValueChanged;
        }
        private void Add_Click(object sender, EventArgs e)
        {
            branch();
            DataTable dt = new DataTable();
            dt = (DataTable)dgv_itemDetails.DataSource;
            try
            {
                dgv_itemDetails.Rows.RemoveAt(0);
            }
            catch (Exception)
            {
            }
            dgviewPending.EndEdit();
            foreach (DataGridViewRow r in dgviewPending.Rows)
            {
                try
                {
                    if (cmb_Purchasetype.Text == "Test" & grbreceipt.Checked == true)
                    {
                        if ((bool)((DataGridViewCheckBoxCell)r.Cells["select"]).Value == true)
                        {
                            //using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand("Select 0 as EntryId,0 as TransId,'' as [Reference No]," + Branchid + " as BranchId,'" + BranchName + "' as BranchName,PurityId,JobNo,JobOrderID,ProdCodeId,ProdCode,BatchNo,BatchId,110100000122 as ItemID,ModelId,'TEST PIECE'[Item Name],Purity,0 as Nos,NetWt as IssueWt,0 as Gwt,0 as StWt,0 as StCash,0 as DiaNo,0 as Diawt,0 as DiaCash,0 as Mud ,0 as NetWt,Touch,Rate,0 as MetalCash,0 as ActualWt,0 as PureWt,0 as Wst,0 as MC,0 as CertificationCharge,0 as Amount,0 as WastageWt,0 as TotalAmount,0 as [Mc Perc],0 as [Wst Perc],IssueId,IsReceipt,PurId,SalesVA,PurchaseVA,0 as TestResult,0 as DiaLoss,0 as StLoss,0 as Loss,TransferTransId,EntryId as IssId,0 as RecId,'' as TrackingID,0 as PoolIdIss,0 as PoolIdRec FROM STK.VGoldStockTransferDetails WHERE EntryId=" + r.Cells["EntryId"].Value.ToString() + " and BatchId=" + r.Cells["BatchId"].Value.ToString() + " and ItemId=" + r.Cells["ItemId"].Value.ToString())).Tables[0])
                            //{
                            using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand("Select * from PROD.BatchIssueAndReceiptPendingAdd(" + r.Cells["EntryId"].Value.ToString() + ",'" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'," + cmb_Purchasetype.SelectedIndex + "," + r.Cells["BatchId"].Value.ToString() + "," + r.Cells["ItemId"].Value.ToString() + "," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    string[] ColumnsToBeDeleted = { "Slno" };

                                    foreach (string ColName in ColumnsToBeDeleted)
                                    {
                                        if (dt1.Columns.Contains(ColName))
                                            dt1.Columns.Remove(ColName);
                                    }
                                    foreach (DataRow row in dt1.Rows)
                                    {
                                        DataRow dr = dt.NewRow();

                                        foreach (DataColumn col in dt1.Columns)
                                        {
                                            if (dt.Columns.Contains(col.ColumnName))
                                            {
                                                dr[col.ColumnName] = row[col];
                                            }
                                        }

                                        dt.Rows.Add(dr);
                                    }
                                }

                            }
                            //dgviewPending.Rows.Remove(r);
                        }
                    }

                    else if (cmb_Purchasetype.Text == "Result" & grbissue.Checked == true)
                    {
                        if ((bool)((DataGridViewCheckBoxCell)r.Cells["select"]).Value == true)
                        {
                            using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand("Select 0 as EntryId,0 as TransId,'' as [Reference No]," + Branchid + " as BranchId,'" + BranchName + "' as BranchName,PurityId,JobNo,JobOrderID,ProdCodeId,ProdCode,BatchNo,BatchId,HUId,ItemID,ModelId,[Item Name],Purity,0 as Nos,IssueWt,Gwt,StWt,StCtWt,StRate,StCash,DiaNo, StNo,Diawt,DiaCash,Mud,NetWt,Touch,Rate,MetalCash,ActualWt,PureWt,Wst,MCRate,MC,CertificationCharge,Amount,WastageWt,TotalAmount,[Mc Perc],[Wst Perc],IssueId,IsReceipt,PurId,SalesVA,PurchaseVA,TestResult,0 as DiaLoss,0 as StLoss,Loss,TransferTransId,0 as IssId,EntryId as RecId,'' as TrackingID,'STK'as IdType FROM STK.VGoldStockTransferDetails WHERE EntryId=" + r.Cells["EntryId"].Value.ToString() + " and BatchId=" + r.Cells["BatchId"].Value.ToString() + " and ItemId=" + r.Cells["ItemId"].Value.ToString())).Tables[0])
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    foreach (DataRow row in dt1.Rows)
                                    {
                                        DataRow dr = dt.NewRow();

                                        foreach (DataColumn col in dt1.Columns)
                                        {
                                            if (dt.Columns.Contains(col.ColumnName))
                                            {
                                                dr[col.ColumnName] = row[col];
                                            }
                                        }

                                        dt.Rows.Add(dr);
                                    }
                                }

                            }
                            //dgviewPending.Rows.Remove(r);
                        }
                    }
                    else if (cmb_Purchasetype.Text == "Refinery" & grbreceipt.Checked == true)
                    {
                        if ((bool)((DataGridViewCheckBoxCell)r.Cells["select"]).Value == true)
                        {
                            using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand("Select * from PROD.BatchIssueAndReceiptPendingAddRefinery('" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + ") WHERE EntryId=" + r.Cells["EntryId"].Value.ToString() + " and BatchId=" + r.Cells["BatchId"].Value.ToString())).Tables[0])
                            //DBConn.GetData(new SqlCommand("Select 0 as EntryId,0 as TransId," + Branchid + " as BranchId,'" + BranchName + "' as BranchName,1 as PurityId,JobNo,JobOrderID,ProdCodeId,ProdCode,BatchNo,BatchId,110100000045 as ItemID,ModelId,'THANKAM' as [Item Name],'Pure' as Purity,0 as Nos,Netwt as IssueWt,Touch as IssueTouch,Actualwt as IssueActualwt,0 as Gwt,100 as Touch,DiaNo,0 as Diawt,0 as DiaCash,0 as StWt,0 as StCash,0 as Mud,0 as NetWt,0 as Rate,0 as MetalCash,0 as ActualWt,0 as PureWt,0 as Wst,0 as MC,0 as CertificationCharge,0 as Amount,0 as WastageWt,0 as TotalAmount,0 as [Mc Perc],0 as [Wst Perc],0 as IssueId,0 as IsReceipt,0 as PurId,0 as SalesVA,0 as PurchaseVA,0 as TestResult,0 as Loss,0 as TransferTransId,TransId as IssId,0 as RecId FROM STK.VGoldStockTransferDetails WHERE EntryId=" + r.Cells["EntryId"].Value.ToString() + " and BatchId=" + r.Cells["BatchId"].Value.ToString() + " and ItemId=" + r.Cells["ItemId"].Value.ToString())).Tables[0])
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    foreach (DataRow row in dt1.Rows)
                                    {
                                        DataRow dr = dt.NewRow();

                                        foreach (DataColumn col in dt1.Columns)
                                        {
                                            if (dt.Columns.Contains(col.ColumnName))
                                            {
                                                dr[col.ColumnName] = row[col];
                                            }
                                        }

                                        dt.Rows.Add(dr);
                                    }
                                }

                            }
                            //dgviewPending.Rows.Remove(r);
                        }
                    }
                    else if (cmb_Purchasetype.Text == "Melting" & grbreceipt.Checked == true)
                    {
                        var touple = GET_ItemId_By_Name("MELTED OG");

                        if ((bool)((DataGridViewCheckBoxCell)r.Cells["select"]).Value == true)
                        {
                            using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand("Select 0 as EntryId,0 as TransId,'' as [Reference No]," + Branchid + " as BranchId,'" + BranchName + "' as BranchName,PurityId,JobNo,JobOrderID,ProdCodeId,ProdCode,BatchNo,BatchId,HUId,'" + touple.Item1.ToString() + "' as ItemID,ModelId,'" + touple.Item2.ToString() + "' as [Item Name],Purity,0 as Nos,NetWt as IssueWt,0 as Gwt,0 as StWt,0 as StCtWt,0 as StRate,0 as StCash,0 as DiaNo,0 as  StNo,0 as Diawt,0 as DiaCash,0 as Mud,0 as NetWt,Touch,Rate,0 as MetalCash,0 as ActualWt,0 as PureWt,0 as Wst,0 as MCRate,0 as MC,0 as CertificationCharge,0 as Amount,0 as WastageWt,0 as TotalAmount,0 as [Mc Perc],0 as [Wst Perc],IssueId,IsReceipt,PurId,SalesVA,PurchaseVA,0 as TestResult,0 as DiaLoss,0 as StLoss,0 as Loss,TransferTransId,EntryId as IssId,0 as RecId,'' as TrackingID,'STK'as IdType FROM STK.VGoldStockTransferDetails WHERE EntryId=" + r.Cells["EntryId"].Value.ToString() + " and BatchId=" + r.Cells["BatchId"].Value.ToString() + " and ItemId=" + r.Cells["ItemId"].Value.ToString())).Tables[0])
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    foreach (DataRow row in dt1.Rows)
                                    {
                                        DataRow dr = dt.NewRow();

                                        foreach (DataColumn col in dt1.Columns)
                                        {
                                            if (dt.Columns.Contains(col.ColumnName))
                                            {
                                                dr[col.ColumnName] = row[col];
                                            }
                                        }

                                        dt.Rows.Add(dr);
                                    }
                                }

                            }
                            //dgviewPending.Rows.Remove(r);

                        }
                    }
                    else if (cmb_Purchasetype.Text == "Melting" & grbissue.Checked == true)
                    {
                        if ((bool)((DataGridViewCheckBoxCell)r.Cells["select"]).Value == true)
                        {
                            using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand("Select * from PROD.BatchIssueAndReceiptPendingAdd(" + r.Cells["EntryId"].Value.ToString() + ",'" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'," + cmb_Purchasetype.SelectedIndex + "," + r.Cells["BatchId"].Value.ToString() + "," + r.Cells["ItemId"].Value.ToString() + "," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    string[] ColumnsToBeDeleted = { "Slno" };

                                    foreach (string ColName in ColumnsToBeDeleted)
                                    {
                                        if (dt1.Columns.Contains(ColName))
                                            dt1.Columns.Remove(ColName);
                                    }

                                    foreach (DataRow row in dt1.Rows)
                                    {
                                        DataRow dr = dt.NewRow();

                                        foreach (DataColumn col in dt1.Columns)
                                        {
                                            if (dt.Columns.Contains(col.ColumnName))
                                            {
                                                dr[col.ColumnName] = row[col];
                                            }
                                        }

                                        dt.Rows.Add(dr);
                                    }
                                }

                            }
                            //dgviewPending.Rows.Remove(r);
                        }
                    }
                    else if (cmb_Purchasetype.SelectedIndex == 1 || cmb_Purchasetype.SelectedIndex == 2 || cmb_Purchasetype.SelectedIndex == 4 || cmb_Purchasetype.SelectedIndex == 3 || cmb_Purchasetype.SelectedIndex == 6 || cmb_Purchasetype.SelectedIndex == 14 || cmb_Purchasetype.SelectedIndex == 8 || cmb_Purchasetype.SelectedIndex == 5 || cmb_Purchasetype.SelectedIndex == 12 || cmb_Purchasetype.SelectedIndex == 15 || cmb_Purchasetype.SelectedIndex == 16)
                    {
                        if (grbreceipt.Checked == true || grbissue.Checked == true)
                        {
                            if ((bool)((DataGridViewCheckBoxCell)r.Cells["select"]).Value == true)
                            {
                                String RecFrom = "STK";
                                if (r.Cells["VchNo"].Value.ToString() == "Supplier Opening") { RecFrom = "SUPO"; } else if (r.Cells["VchNo"].Value.ToString() == "Customer Opening") { RecFrom = "CO"; }
                                using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand
                                                         ("SELECT * FROM [STK].[StockTransferIssueAndReceiptPendingAdd]('" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'," + r.Cells["EntryId"].Value.ToString() + "," + r.Cells["TransId"].Value.ToString() + "," + r.Cells["ItemId"].Value.ToString() + "," + cmbpartytype.SelectedValue + "," + cmb_Purchasetype.SelectedIndex + "," + transtype + ",'" + RecFrom + "'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                                    if (dt1.Rows.Count > 0)
                                    {
                                        Cmb_StkType.Enabled = false;
                                        cmbpartytype.Enabled = false;
                                        // Compare columns between dt and dt1
                                        var missingInDt = dt1.Columns.Cast<DataColumn>()
                                                          .Where(c => !dt.Columns.Contains(c.ColumnName))
                                                          .Select(c => c.ColumnName)
                                                          .ToList();

                                        var missingInDt1 = dt.Columns.Cast<DataColumn>()
                                                           .Where(c => !dt1.Columns.Contains(c.ColumnName))
                                                           .Select(c => c.ColumnName)
                                                           .ToList();

                                        // Debug output
                                        if (missingInDt.Any())
                                            ShowMessage("Columns in dt1 but not in dt: " + string.Join(", ", missingInDt));

                                        if (missingInDt1.Any())
                                            ShowMessage("Columns in dt but not in dt1: " + string.Join(", ", missingInDt1));

                                        foreach (DataRow row in dt1.Rows)
                                        {
                                            DataRow dr = dt.NewRow();

                                            foreach (DataColumn col in dt1.Columns)
                                            {
                                                if (dt.Columns.Contains(col.ColumnName))
                                                {
                                                    dr[col.ColumnName] = row[col];
                                                }
                                            }

                                            dt.Rows.Add(dr);
                                        }
                                    }
                            }
                        }
                    }

                    panelTestPending.Visible = false;
                    panelTestPending.SendToBack();
                    panelTestPending.Hide();
                    BatchWTReadOnly();
                }
                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message);

                }

            }
           

                CalculateActualWtwhileAdding();
            
            ChkSelectAll.Checked = false;
        }

        void DeleteData()
        {
            if (!SAFA.Classes.Common.ChkServerType(DBConn, Convert.ToInt32(txtBranchID.Text)))
            {

            }
            else
            {
                if (SAFA.Classes.SoftwareSettings.IsShowRoom == true)
                {
                    if (cmb_Purchasetype.Text == "Melted OG" || cmb_Purchasetype.Text == "Result" || cmb_Purchasetype.Text == "OldGold" || cmb_Purchasetype.Text == "JobWork")
                    {
                        SqlConnection connectionstring = new SqlConnection(DBConn.ConnectionProperties.ConnectionString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connectionstring;
                        cmd.CommandText = "PROD.InsertPendingStockTransferTable";
                        cmd.CommandType = CommandType.StoredProcedure;
                        connectionstring.Open();
                        cmd.Parameters.AddWithValue("@Entryid", txtEntryNo.Text);
                        cmd.Parameters.AddWithValue("@TransType", grb.Value);
                        cmd.Parameters.AddWithValue("@Branch_Id ", txtBranchID.Text);
                        cmd.Parameters.AddWithValue("@Type", "Delete");
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                        connectionstring.Close();
                    }
                }
            }
        }

        public override bool Delete()
        {

            if (txtBranchID.Text == "")
                return false;
            string TransId = "", BatchId = ""; int PT = cmb_Purchasetype.SelectedIndex;
            bool BatchVal = false;

            foreach (DataGridViewRow r in dgv_itemDetails.Rows)
            {
                if (r.Cells["TransId"].Value.ToString() != "0")
                {
                    if (TransId.Length == 0)
                    {
                        TransId += r.Cells["TransId"].Value.ToString();
                    }
                    else
                    {
                        TransId += "," + r.Cells["TransId"].Value.ToString();
                    }
                }

                if (r.Cells["BatchId"].Value.ToString() != null)
                {
                    if (r.Cells["BatchId"].Value.ToString() != "0")
                    {
                        if (BatchId.Length == 0)
                        {
                            BatchId += r.Cells["BatchId"].Value.ToString();
                        }
                        else
                        {
                            BatchId += "," + r.Cells["BatchId"].Value.ToString();
                        }
                    }
                }
            }
            if (BatchId != "")
            {
                SqlCommand cmd = new SqlCommand("Select PROD.ValidateBatchInboxDelete(" + cmb_Purchasetype.SelectedIndex.ToString() + ",'" + TransId + "','" + BatchId + "'," + txtcompId.Text + "," + txtBranchID.Text + ")");

                if (Convert.ToBoolean(DBConn.GetData(cmd, "Val").Tables[0].Rows[0][0].ToString()))
                {
                    TxtVoucherNo.ShowMessage("Batch Already In Transaction");
                    BatchVal = false;
                    return false;
                }
                else
                {
                    BatchVal = true;
                    return base.Delete();
                }
            }
            else
            {
                string RecId = txtEntryNo.Text;
                if (base.Delete())
                {
                    AddToStockConvertion("Delete", Convert.ToInt64(RecId));
                    if (BatchVal == true) { DeleteData(); }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void branch()
        {
            using (System.Data.DataTable dt3 = DBConn.GetData(new SqlCommand
                              ("select BranchId,BranchName from SYST.BranchMaster where BranchId=" + txtBranchID.Text)).Tables[0])
            {
                if (dt3.Rows.Count > 0)
                {
                    Branchid = dt3.Rows[0]["BranchId"].ToString();
                    BranchName = dt3.Rows[0]["BranchName"].ToString();
                }
            }
        }

        public void BatchWTReadOnly()
        {
            foreach (DataGridViewRow r in dgv_itemDetails.Rows)
            {
                if (grbreceipt.Checked == true)
                {
                    r.Cells["IssueWt"].ReadOnly = true;
                }
                if (r.Cells["BatchId"].Value.ToString().Trim().Length >= 2)
                {
                    r.Cells["IssueWt"].ReadOnly = true;
                    try
                    {
                        r.Cells["IssueActualwt"].ReadOnly = true;
                        r.Cells["IssueTouch"].ReadOnly = true;
                    }
                    catch { }
                }
            }
            if (grbreceipt.Checked == true)
            {
                foreach (DataGridViewRow r in dgv_itemDetails.Rows)
                {
                    if (r.Cells["BatchId"].Value.ToString().Trim().Length >= 2)
                    {
                        r.Cells["BatchNo"].ReadOnly = true;
                        //r.Cells["Item Name"].ReadOnly = true;
                        CmbBatchNo.Enabled = false;
                        //Cmb_ItemName.Enabled = false;
                    }
                }
            }

        }

        private void linkLbl_OtherCharges_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (GrpPurOtherCharges.Visible == false)
            {
                GrpPurOtherCharges.Parent = this;
                GrpPurOtherCharges.Size = new System.Drawing.Size(1000, 250);
                GrpPurOtherCharges.Location = new Point(100, 250);
                GrpPurOtherCharges.Visible = true;
                GrpPurOtherCharges.BringToFront();
                GrpPurOtherCharges.BringToFront();
                GrpPurOtherCharges.Show();
                linkLbl_OtherCharges.Text = "Hide Other Charges";
            }
            else
            {
                GrpPurOtherCharges.Visible = false;
                GrpPurOtherCharges.SendToBack();
                GrpPurOtherCharges.Hide();
                linkLbl_OtherCharges.Text = "Add Other Charges";
            }
        }
        public void Othertaxcalu()
        {
            if (cmbpartytype.SelectedValue == null)
                return;

            if (cmbpartyname.SelectedValue == null)
                return;

            String IsGst = "";
            Double SGSTper = 0, CGSTper = 0, IGSTper = 0, Amt = 0;
            SGSTper = Convert.ToDouble(label93.Text == "" ? "0" : label93.Text);
            CGSTper = Convert.ToDouble(label96.Text == "" ? "0" : label96.Text);
            IGSTper = Convert.ToDouble(label99.Text == "" ? "0" : label99.Text);
            if (txtamout.Text != "")
            {
                if ((cmbpartytype.SelectedValue.ToString() == "1" || cmbpartytype.SelectedValue.ToString() == "2") && cmbpartyname.SelectedValue.ToString().Trim().Length > 3)
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                            ("select IsGSTRegistred From PUR.SupplierMaster Where SuppId=" + cmbpartyname.SelectedValue + " and Company_id=" + txtcompId.Text + "  ")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            IsGst = dt.Rows[0]["IsGSTRegistred"].ToString();
                        }
                    }
                }
                else
                {
                    IsGst = "Y";
                }

                if (IsGst == "N")
                {
                    TxtSGST.Text = 0.ToString("f2");
                    TxtCGST.Text = 0.ToString("F2");
                    TxtIGST.Text = 0.ToString("F2");
                    return;
                }

                Amt = Convert.ToDouble(txtamout.Text == "" ? "0" : txtamout.Text);
                TxtSGST.Text = (Amt * SGSTper / 100).ToString("f2");
                TxtCGST.Text = (Amt * CGSTper / 100).ToString("f2");
                if (IGSTper != 0)
                {
                    TxtIGST.Text = (Amt * IGSTper / 100).ToString("f2");
                }

            }
        }
        public void OthertaxAmtcalu()
        {
            double SGSTAmt = 0, CGSTAmt = 0, IGSTAmt = 0, TotlAmt = 0, finalAmt = 0, TdsAmount = 0, tdsperc = 0;
            SGSTAmt = Convert.ToDouble(TxtSGST.Text == "" ? "0" : TxtSGST.Text);
            CGSTAmt = Convert.ToDouble(TxtCGST.Text == "" ? "0" : TxtCGST.Text);
            IGSTAmt = Convert.ToDouble(TxtIGST.Text == "" ? "0" : TxtIGST.Text);
            finalAmt = Convert.ToDouble(txtamout.Text == "" ? "0" : txtamout.Text);
            tdsperc = Convert.ToDouble(TxtTdsPerc.Text == "" ? "0" : TxtTdsPerc.Text);
            TdsAmount = Convert.ToInt64(finalAmt * (tdsperc / 100));
            TotlAmt = (SGSTAmt + CGSTAmt + IGSTAmt + finalAmt - TdsAmount);
            txt_tdsamt.Text = TdsAmount.ToString("f0");
            txtAmount_Charge.Text = TotlAmt.ToString("f2");
        }
        public void taxrateOtherch()
        {
            if (cmbpartytype.SelectedValue == null)
                return;
            if (cmbpartytype.SelectedValue.ToString() == "1")
            {
                if (cmbpartyname.SelectedValue != null)
                {
                    if (txtcompId.Text != "")
                    {
                        using (DataTable t = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                           ("select CGSTper,Sgstper,IGSTper from PUR.MiscChargeMaster where ChargeId='" + Cmb_Chargename.SelectedValue + "'")).Tables[0])
                        using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                              ("select Comp_StateCode from SYST.BranchMaster where Branchid= " + txtBranchID.Text + "")).Tables[0])
                        using (DataTable dtt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                               ("select statecode from pur.suppliermaster where SuppId= '" + cmbpartyname.SelectedValue + "'")).Tables[0])
                            if (t.Rows.Count > 0)
                            {
                                if (dtt.Rows[0]["statecode"].ToString().ToString() == dt.Rows[0]["Comp_StateCode"].ToString())
                                {
                                    label96.Text = t.Rows[0]["CGSTper"].ToString();
                                    label93.Text = t.Rows[0]["Sgstper"].ToString();
                                    label99.Text = "0.00";
                                }
                                else
                                {
                                    label99.Text = t.Rows[0]["IGSTper"].ToString();
                                    label93.Text = "0.00";
                                    label96.Text = "0.00";
                                }
                                //txtTaxRatePerc.Text = t.Rows[0]["IGSTper"].ToString();
                            }
                    }
                }
            }
            else if (cmbpartytype.SelectedValue.ToString() == "3")
            {
                if (cmbpartyname.SelectedValue != null)
                {
                    if (txtcompId.Text != "")
                    {
                        using (DataTable t = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                           ("select CGSTper,Sgstper,IGSTper from PUR.MiscChargeMaster where ChargeId='" + Cmb_Chargename.SelectedValue + "'")).Tables[0])
                        using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                              ("select Comp_StateCode from SYST.BranchMaster where Branchid= " + txtBranchID.Text + "")).Tables[0])
                        using (DataTable dtt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                               ("select StateId from CRM.CustomerMaster where CustId= '" + cmbpartyname.SelectedValue + "'")).Tables[0])
                            if (t.Rows.Count > 0)
                            {
                                if (dtt.Rows[0]["StateId"].ToString().ToString() == dt.Rows[0]["Comp_StateCode"].ToString())
                                {
                                    label96.Text = t.Rows[0]["CGSTper"].ToString();
                                    label93.Text = t.Rows[0]["Sgstper"].ToString();
                                    label99.Text = "0.00";
                                }
                                else
                                {
                                    label99.Text = t.Rows[0]["IGSTper"].ToString();
                                    label93.Text = "0.00";
                                    label96.Text = "0.00";
                                }
                                //txtTaxRatePerc.Text = t.Rows[0]["IGSTper"].ToString();
                            }
                    }
                }
            }
        }
        private void Cmb_Chargename_SelectedValueChanged(object sender, EventArgs e)
        {
            taxrateOtherch();
            Othertaxcalu();
            OthertaxAmtcalu();


        }

        private void txtamout_TextChanged(object sender, EventArgs e)
        {
            //taxrateOtherch();
            Othertaxcalu();
            OthertaxAmtcalu();
            TdsRate();

        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            if (Cmb_Chargename.SelectedValue != null)
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
                Cmb_Chargename.Focus();
                calculateTax();
                TxtTdsPerc.Clear();
            }
            TxtOTCharge.Text = dgv_otherChg.SummaryRow.SummaryCells["Amountt"].Text;

        }
        public void CalculateOTHTax()
        {
            double Amount = 0;
            //foreach (DataGridViewRow r in dgv_otherChg.Rows)
            //{
            //    if ((bool)r.Cells["CalculateTax"].Value == true)
            //    {
            //        Amount += Convert.ToDouble(r.Cells["Amount"].Value.ToString());
            //    }
            //}
            TxtOTChargeTax.Text = Amount.ToString("F2");
        }
        private void dgv_otherChg_SummaryCalculated(object source, EventArgs e)
        {
            CalculateOTHTax();
            TxtOTCharge.Text = dgv_otherChg.SummaryRow.SummaryCells["Amountt"].Text;
        }

        private void TxtOTCharge_TextChanged(object sender, EventArgs e)
        {

        }

        private void label92_Click(object sender, EventArgs e)
        {

        }

        private void linkLbl_OtherCharges_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (GrpPurOtherCharges.Visible == false)
            {
                GrpPurOtherCharges.Parent = this;
                GrpPurOtherCharges.Size = new System.Drawing.Size(913, 250);
                GrpPurOtherCharges.Location = new Point(100, 150);
                GrpPurOtherCharges.Visible = true;
                GrpPurOtherCharges.BringToFront();
                GrpPurOtherCharges.BringToFront();
                GrpPurOtherCharges.Show();
                linkLbl_OtherCharges.Text = "Hide Other Charges";
                cmbtdsName.SelectedValueChanged -= cmbtdsName_SelectedValueChanged;
                Gramboo.General.Setupcombo(cmbtdsName, "GEN.TDSMaster ", "TdsName", "TdsId", "IsActive='True'");
                cmbtdsName.SelectedValueChanged += cmbtdsName_SelectedValueChanged;
            }
            else
            {
                GrpPurOtherCharges.Visible = false;
                GrpPurOtherCharges.SendToBack();
                GrpPurOtherCharges.Hide();
                linkLbl_OtherCharges.Text = "Add Other Charges";
            }
        }

        private void lnkExport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lnkExport.Text == "Export")
            {
                SaveFileDialog s = new SaveFileDialog();
                s.DefaultExt = "xml";
                s.CheckPathExists = true;
                if (s.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DBConn.GetData(new SqlCommand("Select EntryId,TransId,'' as [Reference No],BranchId,BranchName,PurityId,ISNULL(JobNo,'') as JobNo,JobOrderID,ProdCodeId,ISNULL(ProdCode,'') as ProdCode," +
                        "ISNULL(BatchNo,'') as BatchNo,BatchId,HUId, ItemID, ModelId, [Item Name], Purity, Nos, IssueWt, Gwt, StWt, StCtWt, StCash, DiaNo, StNo, Diawt, DiaCash, Mud, NetWt, Touch, Rate, MetalCash, ActualWt," +
                        " PureWt, Wst, MC, CertificationCharge, Amount, WastageWt, TotalAmount,[Mc Perc],[Wst Perc], IssueId, IsReceipt, PurId, SalesVA, PurchaseVA, TestResult, DiaLoss, StLoss, Loss, 0 as TransferTransId, 0 as IssId," +
                        " 0 as RecId, '-' as TrackingID, '' as IdType FROM STK.VGoldStockTransferDetails WHERE EntryId=" + txtEntryNo.Text), "StockTransfer")
                          .Tables[0])
                    {

                        dt.WriteXml(s.FileName, false);

                        using (DataTable dt1 = DBConn.GetData(new SqlCommand("SELECT t1.ProdCodeId,t3.JobOrderId,StockDate,0 as SupplierId,0 as DeptId,0 as PurId,t1.prodCode,GenFrom,t1.ItemID,t1.PurityId," +
                             "t1.ModelId,t1.Nos,t2.NetWt,t2.Gwt,t2.Diawt,t1.DiaNo,t1.StNo,t1.DiaRate,t2.DiaCash,t2.Stonewt,t1.StoneCtWt,t1.StRate,t2.StoneCash," +
                             "t1.MC,t1.Wst,IsMRP,t2.MRP,t1.VA,t1.VAPerc,MinVA,t1.MCPerc,t1.WstPerc,PurRate,PurDiaRate,PurStRate,PurCost," +
                             "description ,t1.Created_by,t1.Created_date,t1.Last_modified_by,t1.Last_modified_date,t1.Company_id,t1.Branch_id," +
                             "t1.Counter_id,t1.IsActive,VAamount,VAmode,MinimumVAamount,t1.Touch,RatePerGm,MessureID,Value,Tagwt," +
                             "DesignId,SingleStone,StoneId,ISNULL(t1.HUId,'') as HUId,t1.Wax " +
                             "FROM STK.ProdCodeMaster as t1 inner join STK.VProdcodeWeight as t2 " +
                             "on t1.ProdCodeId=t2.ProdCodeId inner join STK.StockTransferDetails as t3 " +
                             "on t1.ProdCodeId=t3.ProdCodeId " +
                             "WHERE  t3.EntryId=" + txtEntryNo.Text), "StockTransferSplitup")
                               .Tables[0])
                        {
                            dt1.WriteXml(System.IO.Path.GetDirectoryName(s.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(s.FileName) + "Splitup.xml", false);
                        }
                        using (DataTable dt2 = DBConn.GetData(new SqlCommand("SELECT t1.TransId,t1.ProdCodeId,t1.ItemName,t1.Qty,t1.Wt,t1.CtWt,t1.PurchaseRate,t1.SalesRate," +
                              "t1.StoneItemId,t1.ShapeID,t1.QualityID,t1.ColorID,t1.Created_by,t1.Created_date," +
                              "t1.Last_modified_by,t1.Last_modified_date,t1.Company_id,t1.Branch_id,t1.Counter_id," +
                              "t1.IsActive,t1.SieveId FROM STK.ProductCodeDetail as t1 inner join STK.ProdCodeMaster as t2 " +
                              "on t1.ProdCodeId=t2.ProdCodeId inner join STK.StockTransferDetails as t3 " +
                              "on t2.ProdCodeId=t3.ProdCodeId " +
                              "where t3.EntryId=" + txtEntryNo.Text), "StockTransferSplitupDetail")
                               .Tables[0])
                        {
                            if (dt2.Rows.Count > 0)
                            {
                                dt2.WriteXml(System.IO.Path.GetDirectoryName(s.FileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(s.FileName) + "SplitupDetail.xml", false);
                            }
                        }
                    }
                }
            }

            else if (lnkExport.Text == "Import")
            {

                if (cmbpartyname.SelectedValue != null)
                {
                    chkIsImport.Checked = true;
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
                            cmd.Parameters.AddWithValue("@RecFrom", "STK");

                            using (ds = DBConn.GetData(cmd))
                            {

                                DataTable dttt =
                                    ds.Tables[0];
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    Gramboo.General.ShowMessage(@"The values in the import file is alredy saved in your database " +
                                        "  \n Please verify data first then try again ", "Verify Data", MessageBoxIcon.Information);
                                }
                                else
                                {
                                    XmlTextReader xmlreaderr = new XmlTextReader(op.FileName);
                                    DataSet dt = new DataSet();
                                    dt.ReadXml(xmlreaderr);
                                    xmlreader.Close();

                                    dgv_itemDetails.SummaryRowVisible = false;
                                    ((DataTable)dgv_itemDetails.DataSource).Rows.Clear();

                                    foreach (DataRow dr in dt.Tables[0].Rows)
                                    {
                                        if (dr["Item Name"].ToString() != "")
                                        {
                                            DataRow r = ((DataTable)dgv_itemDetails.DataSource).NewRow();

                                            r["EntryId"] = dr["EntryId"];
                                            r["TransId"] = dr["TransId"];
                                            r["Reference No"] = dr["Reference No"];
                                            r["BranchId"] = dr["BranchId"];
                                            if (dt.Tables[0].Columns.Contains("BranchName"))
                                                r["BranchName"] = dr["BranchName"];
                                            r["PurityId"] = dr["PurityId"];
                                            r["JobNo"] = dr["JobNo"];
                                            r["JobOrderID"] = dr["JobOrderID"];
                                            r["ProdCodeId"] = dr["ProdCodeId"];
                                            r["ProdCode"] = dr["ProdCode"];
                                            r["BatchNo"] = dr["BatchNo"];
                                            r["BatchId"] = dr["BatchId"];
                                            r["HUId"] = dr["HUId"];

                                            using (DataTable dtitem = DBConn.GetData(new SqlCommand("Select itemid from itm.ItemMaster where itemname='" + dr["Item Name"].ToString() + "' "), "item").Tables[0])
                                            {
                                                if (dtitem.Rows.Count == 0)
                                                {
                                                    Gramboo.General.ShowMessage("Item NAme" + dr["Item Name"].ToString() + " Not Found");
                                                }
                                                else
                                                {
                                                    r["ItemID"] = dtitem.Rows[0]["ItemID"].ToString();

                                                }
                                            }
                                            r["ModelId"] = dr["ModelId"];
                                            r["Item Name"] = dr["Item Name"];
                                            r["Purity"] = dr["Purity"];
                                            r["Nos"] = dr["Nos"];
                                            r["Touch"] = dr["Touch"];
                                            r["IssueWt"] = dr["IssueWt"];
                                            r["Gwt"] = dr["Gwt"];
                                            r["DiaNo"] = dr["DiaNo"];
                                            r["StNo"] = dr["StNo"];
                                            r["Diawt"] = dr["Diawt"];
                                            r["DiaCash"] = dr["DiaCash"];
                                            r["StWt"] = dr["StWt"];
                                            r["StCtWt"] = dr["StCtWt"];
                                            r["StCash"] = dr["StCash"];
                                            r["Mud"] = dr["Mud"];
                                            r["NetWt"] = dr["NetWt"];
                                            r["Rate"] = dr["Rate"];
                                            r["MetalCash"] = dr["MetalCash"];
                                            r["ActualWt"] = dr["ActualWt"];
                                            r["PureWt"] = dr["PureWt"];
                                            r["Wst"] = dr["Wst"];
                                            r["MC"] = dr["MC"];
                                            r["CertificationCharge"] = dr["CertificationCharge"];
                                            r["Amount"] = dr["Amount"];
                                            r["WastageWt"] = dr["WastageWt"];
                                            r["TotalAmount"] = dr["TotalAmount"];
                                            r["Mc Perc"] = dr["Mc Perc"];
                                            r["Wst Perc"] = dr["Wst Perc"];
                                            r["IssueId"] = dr["IssueId"];
                                            r["IsReceipt"] = dr["IsReceipt"];
                                            r["PurId"] = dr["PurId"];
                                            r["SalesVA"] = dr["SalesVA"];
                                            r["PurchaseVA"] = dr["PurchaseVA"];
                                            r["TestResult"] = dr["TestResult"];
                                            r["Loss"] = dr["Loss"];
                                            r["DiaLoss"] = dr["DiaLoss"];
                                            r["StLoss"] = dr["StLoss"];
                                            r["TransferTransId"] = dr["TransferTransId"];
                                            r["IssId"] = dr["IssId"];
                                            r["RecId"] = dr["RecId"];
                                            if (dt.Tables[0].Columns.Contains("TrackingID"))
                                            {
                                                r["TrackingID"] = dr["TrackingID"];
                                            }
                                            r["IdType"] = dr["IdType"];
                                            ((DataTable)dgv_itemDetails.DataSource).Rows.Add(r);
                                        }
                                    }

                                    dgv_itemDetails.SummaryRowVisible = true;
                                    dgv_itemDetails.SummaryRow.reCreateSumBoxes();
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
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            string LoadBranch;
            if (cmbpartytype.SelectedValue.ToString() == "1" || cmbpartytype.SelectedValue.ToString() == "2")
            {
                if (SuppBranch == null)
                    return;
                LoadBranch = SuppBranch;
            }
            else if (cmbpartytype.SelectedValue.ToString() == "3")
            {
                if (CustBaranchid == null)
                    return;
                LoadBranch = CustBaranchid;
            }
            else if (cmbpartytype.SelectedValue.ToString() == "0")
            {
                if (cmbpartyname.SelectedValue == null)
                    return;
                LoadBranch = cmbpartyname.SelectedValue.ToString();
            }
            else
            {
                return;
            }

            if (LoadBranch != "0")
            {
                if (LoadBranch == txtBranchID.Text)
                    return;

                SqlConnection connectionstring = new SqlConnection(DBConn.ConnectionProperties.ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "STK.ProcLoadStockTransferPendingProdcodes";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", dtp_dt.Value.Date.ToString("dd-MMM-yyyy"));
                cmd.Parameters.AddWithValue("@IsReceipt", grb.Value);
                cmd.Parameters.AddWithValue("@TransType", grb.Value);
                cmd.Parameters.AddWithValue("@PurchaseType", cmb_Purchasetype.SelectedIndex);
                cmd.Parameters.AddWithValue("@selectBranchId", (LoadBranch));
                cmd.Parameters.AddWithValue("@Company_id", (txtcompId.Text != "" ? txtcompId.Text : "0"));
                cmd.Parameters.AddWithValue("@Branch_id", (txtBranchID.Text != "" ? txtBranchID.Text : "0"));
                cmd.CommandTimeout = 0;

                dgv_itemDetails.HiddenDataFields = new List<string>() { "EntryId", "TransId", "[Reference No]", "BranchId", "BranchName", "JobNo", "JobOrderID", "ProdCodeId", "BatchId", "ItemID", "PureWt", "PurityId", "ModelId", /*"Mc Perc", "Wst Perc",*/ "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo","StNo","CertificationCharge", "Amount"/*, "WastageWt"*/, "SalesVA", "PurchaseVA", "TestResult", "DiaLoss", "StLoss", "Loss", "TransferTransId", "IssueWt", "IssId", "RecId", "Mud", "TrackingID" };
                dgv_itemDetails.SummaryRowVisible = true;
                dgv_itemDetails.DataFields = new List<string> {"EntryId","TransId","[Reference No]","BranchId","BranchName","PurityId","JobNo","JobOrderID","ProdCodeId","ProdCode","BatchNo","BatchId", "ItemID", "ModelId", "[Item Name]", "Purity",
           "Nos","IssueWt","Gwt","DiaNo","StNo","StWt","StCash","Diawt","DiaCash","Mud","NetWt","Touch","Rate","MetalCash", "ActualWt","PureWt","Wst","MC","CertificationCharge","Amount", "WastageWt","TotalAmount","[Mc Perc]","[Wst Perc]","IssueId","IsReceipt","PurId","SalesVA","PurchaseVA","TestResult","DiaLoss","StLoss","Loss","TransferTransId","IssId", "RecId","TrackingID"};

                dgv_itemDetails.SummaryColumns = new string[] { "NetWt", "Nos", "Gwt", "StWt", "Diawt", "DiaCash", "StCash", "MC", "Wst", "WastageWt", "ActualWt", "TotalAmount", "MetalCash" };

                dgv_itemDetails.DataSource = DBConn.GetData(cmd).Tables[0];
                CalculateActualWtwhileAdding();
            }

            //if (cmb_Purchasetype.Text == "")
            //{
            //    cmb_Purchasetype.ShowMessage("Select Purchase Type");
            //    return;
            //}
            //if (cmbpartyname.SelectedValue != null && cmbpartytype.SelectedValue == 0 && cmb_Purchasetype.SelectedIndex == 1)
            //{

            //    dgv_itemDetails.DataSource = DBConn.GetData(new SqlCommand("Select EntryId,TransId,BranchId,BranchName,PurityId,ItemID,ModelId,ProdCodeId,ProdCode, [Item Name],Purity,Nos,Gwt,StWt,StCash,NetWt,Touch,PureWt,Wst,MC,WastageWt,ActualWt ,[Mc Perc],[Wst Perc],IssueId,IsReceipt,PurId,SalesVA,PurchaseVA from STK.VGoldStockTransferPending t1 WHERE " +
            //        " exists (select top 1  prodcodeid from stk.OrnamentsStatus ('" + dtp_dt.Value + "'," + txtcompId.Text + "," + txtBranchID.Text + ") t2 where t1.prodcodeid=t2.prodcodeid ) and Company_id =" + txtcompId.Text
            //        + " AND BranchId =" + cmbpartyname.SelectedValue.ToString() + " AND Branch_id=" + txtBranchID.Text + " AND ItemCategoryId  not in (14,110200000006)")).Tables[0];
            //    dgv_itemDetails.Columns["SalesVA"].Visible = true;
            //    dgv_itemDetails.Columns["PurchaseVA"].Visible = true;
            //}
            //else if (cmbpartyname.SelectedValue != null && cmbpartytype.SelectedValue == 0 && cmb_Purchasetype.SelectedIndex == 0)
            //{

            //    dgv_itemDetails.DataSource = DBConn.GetData(new SqlCommand("Select EntryId,TransId,BranchId,BranchName,PurityId,ItemID,ModelId,ProdCodeId,ProdCode, [Item Name],Purity,Nos,Gwt,StWt,StCash,NetWt,Touch,PureWt,Wst,MC,WastageWt,ActualWt ,[Mc Perc],[Wst Perc],IssueId,IsReceipt,PurId,SalesVA,PurchaseVA from STK.VGoldStockTransferPending t1 WHERE " +
            //       " exists (select top 1  prodcodeid from stk.OrnamentsStatus ('" + dtp_dt.Value + "'," + txtcompId.Text + "," + txtBranchID.Text + ") t2 where t1.prodcodeid=t2.prodcodeid ) AND Company_id =" + txtcompId.Text
            //       + " AND BranchId =" + cmbpartyname.SelectedValue.ToString() + " AND Branch_id=" + txtBranchID.Text + " AND ItemCategoryId !=110100000012")).Tables[0];
            //    dgv_itemDetails.Columns["SalesVA"].Visible = true;
            //    dgv_itemDetails.Columns["PurchaseVA"].Visible = true;
            //}
        }

        public bool IsValidXml(string path)
        {
            XDocument doc = XDocument.Load(path);
            var settings = doc.Element("NewDataSet");
            return doc.Element("NewDataSet") != null &&
            doc.Element("NewDataSet").Elements("StockTransfer").Any();
        }

        private void cmbtdsName_SelectedValueChanged(object sender, EventArgs e)
        {
            msgval = true;
            TdsRate();

        }

        private void cmbtdsName_TextChanged(object sender, EventArgs e)
        {
            if (cmbtdsName.Text == "" || cmbtdsName.Text == null)
            {
                TxtTdsPerc.Text = "";
                txt_tdsamt.Text = "0";
                OthertaxAmtcalu();
            }
        }

        private void TxtOTCharge_TextChanged_1(object sender, EventArgs e)
        {
            if (cmb_Purchasetype.Text == "JobWork")
            {
                calcufinal1();
            }
            else
            {
                calcufinal();
            }
        }

        private void cmbpartytype_SelectedValueChanged(object sender, EventArgs e)
        {
            cmbpartyname.SelectedValueChanged -= cmbpartyname_SelectedValueChanged;
            PartyTypeIdChanged();
            if (cmbpartytype.Enabled == true && cmb_Purchasetype.Text != "")
            {
                vchno(true);
            }
            cmbpartyname.SelectedValueChanged += cmbpartyname_SelectedValueChanged;
        }

        private void FrmSmithReceipt_Load(object sender, EventArgs e)
        {

            dtp_dt.MaxDate = DateTime.Today;
            
        }

        public void AddDataToGrid(object sender, SAFA.Forms.STK.FrmAddDataToSTKTransferGrid.DataTransferClickEventArgs e)
        {
            var rowsToRemove1 = from DataGridViewRow r in dgv_itemDetails.Rows
                                where r.Cells["ItemId"].Value.ToString() == ""
                                select r;
            foreach (var r in rowsToRemove1)
                dgv_itemDetails.Rows.Remove(r);

            FrmAddDataToSTKTransferGrid ADD = (FrmAddDataToSTKTransferGrid)sender;
            DataTable dt = (dgv_itemDetails.DataSource as DataTable);
            foreach (DataRow row in (ADD.dgviewAddData.DataSource as DataTable).Rows)
            {
                DataRow dr = dt.NewRow();
                dr.ItemArray = row.ItemArray;
                dt.Rows.Add(dr);
            }
            ADD.Close();
        }

        private void dgviewPending_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cmb_Purchasetype.SelectedIndex == 1 && (cmbpartytype.SelectedIndex == 3 && grbreceipt.Checked == true))
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dgviewPending.Rows[rowIndex];

                panelTestPending.SendToBack();
                panelTestPending.Visible = false;

                SAFA.Forms.STK.FrmAddDataToSTKTransferGrid ADD = new SAFA.Forms.STK.FrmAddDataToSTKTransferGrid();
                ADD.DataTransferClick += new SAFA.Forms.STK.FrmAddDataToSTKTransferGrid.DataTransferClickEventHandler(AddDataToGrid);
                ADD.STKEDMode = false;
                ADD.Id = row.Cells["EntryId"].Value.ToString();
                ADD.TransId = row.Cells["TransId"].Value.ToString();
                ADD.VchNo = row.Cells["VchNo"].Value.ToString();
                ADD.VchDate = row.Cells["VchDate"].Value.ToString();
                ADD.Gwt = row.Cells["Gwt"].Value.ToString(); ADD.Diawt = row.Cells["Diawt"].Value.ToString();
                ADD.Stwt = row.Cells["Stwt"].Value.ToString(); ADD.Netwt = row.Cells["Netwt"].Value.ToString();
                ADD.txt_touch = Convert.ToDouble(txt_touch.Text);
                ADD.partyname = cmbpartyname.SelectedValue.ToString(); ADD.partytype = grb.Value;
                ADD.purchasetype = cmb_Purchasetype.ToString(); ADD.grb = grb.Value;
                ADD.dtp_dt = dtp_dt.Value.ToString();
                //ADD.PartyId = Convert.ToInt64(cmb_PartyName.SelectedValue);
                //AE.Fill(txtdbtid.Text);
                SetColumns(ADD.dgviewAddData);
                ADD.RefreshData();
                ADD.ShowDialog();
            }
        }

        private void dgv_itemDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cmb_Purchasetype.SelectedIndex == 1 && (cmbpartytype.SelectedIndex == 3 && grbreceipt.Checked == true))
            {
                int rowIndex = e.RowIndex;
                DataGridViewRow row = dgv_itemDetails.Rows[rowIndex];

                SAFA.Forms.STK.FrmAddDataToSTKTransferGrid ADD = new SAFA.Forms.STK.FrmAddDataToSTKTransferGrid();
                ADD.DataTransferClick += new SAFA.Forms.STK.FrmAddDataToSTKTransferGrid.DataTransferClickEventHandler(EditDataToGrid);
                ADD.STKEDMode = false;

                using (System.Data.DataTable dt5 = DBConn.GetData(new SqlCommand
                    ("Select EntryId,TransId,VchNo,VchDate,CustName,PartyName,ItemID,ItemName,Nos,Gwt,Diawt,Stwt,Netwt,ActWt from STK.StockTransferIssueAndReceiptPending('" + cmb_Purchasetype.SelectedIndex.ToString() + "','" + grb.Value + "','" + cmbpartytype.SelectedValue.ToString() + "','" + cmbpartyname.SelectedValue.ToString() + "','" + Cmb_StkType.SelectedValue.ToString() + "','STK'," + txtcompId.Text + "," + txtBranchID.Text + ") where TransId='" + row.Cells["IssId"].Value.ToString() + "'")).Tables[0])
                {
                    if (dt5.Rows.Count > 0)
                    {
                        ADD.Id = dt5.Rows[0]["EntryId"].ToString();
                        ADD.TransId = dt5.Rows[0]["TransId"].ToString();
                        ADD.VchNo = dt5.Rows[0]["VchNo"].ToString();
                        ADD.VchDate = dt5.Rows[0]["VchDate"].ToString();
                        ADD.Gwt = dt5.Rows[0]["Gwt"].ToString(); ADD.Diawt = dt5.Rows[0]["Diawt"].ToString();
                        ADD.Stwt = dt5.Rows[0]["Stwt"].ToString(); ADD.Netwt = dt5.Rows[0]["Netwt"].ToString();
                    }
                }
                ADD.txt_touch = Convert.ToDouble(txt_touch.Text);
                ADD.partyname = cmbpartyname.SelectedValue.ToString(); ADD.partytype = grb.Value;
                ADD.purchasetype = cmb_Purchasetype.ToString(); ADD.grb = grb.Value;
                ADD.dtp_dt = dtp_dt.Value.ToString();
                SetColumns(ADD.dgviewAddData);
                ADD.RefreshData();
                ADD.dgviewAddData.Rows.RemoveAt(0);
                DataTable dt = (ADD.dgviewAddData.DataSource as DataTable);
                DataTable mt = (dgv_itemDetails.DataSource as DataTable);
                DataTable ms = mt.Copy();
                DataRow[] DrArrCheck = ms.Select("IssId <> " + row.Cells["IssId"].Value.ToString() + "");
                foreach (DataRow DrCheck in DrArrCheck)
                {
                    ms.Rows.Remove(DrCheck);
                }

                foreach (DataRow r in ms.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr.ItemArray = r.ItemArray;
                    dt.Rows.Add(dr);
                }
                var rowsToRemove = from DataGridViewRow r in ADD.dgviewAddData.Rows
                                   where r.Cells["ItemId"].Value.ToString() == ""
                                   select r;

                foreach (var r in rowsToRemove)
                    ADD.dgviewAddData.Rows.Remove(r);

                ADD.ShowDialog();

            }
        }
        public void EditDataToGrid(object sender, SAFA.Forms.STK.FrmAddDataToSTKTransferGrid.DataTransferClickEventArgs e)
        {
            FrmAddDataToSTKTransferGrid ADD = (FrmAddDataToSTKTransferGrid)sender;
            var rowsToRemove1 = from DataGridViewRow r in dgv_itemDetails.Rows
                                where r.Cells["ItemId"].Value.ToString() == ""
                                select r;
            var rowsToRemove2 = from DataGridViewRow r in dgv_itemDetails.Rows
                                where r.Cells["IssId"].Value.ToString() == "" + ADD.Id + ""
                                select r;

            foreach (var r in rowsToRemove1)
                dgv_itemDetails.Rows.Remove(r);
            foreach (var r in rowsToRemove2)
                dgv_itemDetails.Rows.Remove(r);

            DataTable dt = (dgv_itemDetails.DataSource as DataTable);
            foreach (DataRow row in (ADD.dgviewAddData.DataSource as DataTable).Rows)
            {
                DataRow dr = dt.NewRow();
                dr.ItemArray = row.ItemArray;
                dt.Rows.Add(dr);
            }
            ADD.Close();
        }
        public void TdsRate()
        {
            if (Cmb_Chargename.SelectedValue == null)
                return;
            if (cmbtdsName.SelectedValue != null && cmbtdsName.SelectedValue.ToString().Trim().Length > 3 && cmbpartyname.SelectedValue != null)
            {
                Int64 LedgerId = 0; String PartyType = "";
                if (cmbpartytype.SelectedValue.ToString() == "1" || cmbpartytype.SelectedValue.ToString() == "2")
                {
                    using (DataTable dtSupp = DBConn.GetData(new SqlCommand("select Isnull(Acc_LedgerID,0) as Acc_LedgerID from PUR.SupplierMaster where SuppId='" + cmbpartyname.SelectedValue.ToString() + "'")).Tables[0])
                    {
                        if (dtSupp.Rows.Count > 0)
                        {
                            PartyType = "Supplier";
                            LedgerId = Convert.ToInt64(dtSupp.Rows[0]["Acc_LedgerID"].ToString());
                        }
                    }
                }
                else if (cmbpartytype.SelectedValue.ToString() == "3")
                {
                    using (DataTable dt = DBConn.GetData(new SqlCommand("select Isnull(Acc_LedgerID,0) as Acc_LedgerID from CRM.CustomerMaster where Custid='" + cmbpartyname.SelectedValue.ToString() + "'")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            PartyType = "Customer";
                            LedgerId = Convert.ToInt64(dt.Rows[0]["Acc_LedgerID"].ToString());
                        }
                    }
                }
                Supledgerid = LedgerId;
                //Double othind = 0.00, ind = 0.00, nopan = 0.00;
                //using (DataTable dt = DBConn.GetData(new SqlCommand("select Indivi,Othind,Nopan FROM GEN.TDSMaster  where TdsId='" + cmbtdsName.SelectedValue + "'")).Tables[0])
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        ind = Convert.ToDouble(dt.Rows[0]["Indivi"].ToString());
                //        othind = Convert.ToDouble(dt.Rows[0]["Othind"].ToString());
                //        nopan = Convert.ToDouble(dt.Rows[0]["Nopan"].ToString());

                //    }
                //}
                Double recvrate = 0.00, othind = 0.00, ind = 0.00, nopan = 0.00;
                if (grbissue.Checked == true)
                {
                    using (DataTable dt = DBConn.GetData(new SqlCommand("select top 1 ReceivableRate,Otherthanindividual,Individual,NoPanno FROM GEN.TDSDetails  where TdsId='" + cmbtdsName.SelectedValue + "' and date<='" + dtp_dt.Value + "'order by date desc")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            recvrate = Convert.ToDouble(dt.Rows[0]["ReceivableRate"].ToString());
                            TxtTdsPerc.Text = recvrate.ToString("F2");
                            OthertaxAmtcalu();

                        }
                    }
                }
                else
                {
                    //Double othind = 0.00, ind = 0.00, nopan = 0.00;
                    using (DataTable dt = DBConn.GetData(new SqlCommand("select top 1 Otherthanindividual,Individual,NoPanno FROM GEN.TDSDetails  where TdsId='" + cmbtdsName.SelectedValue + "' and date<='" + dtp_dt.Value + "' order by date desc")).Tables[0])
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
                                TxtTdsPerc.Text = nopan.ToString("F2");
                            }
                            else if (HasTds == "True" && HasPan == "True" && IsInd == "True")
                            {
                                TxtTdsPerc.Text = ind.ToString("F2");
                            }
                            else if (HasTds == "True" && HasPan == "True" && IsInd == "False")
                            {
                                TxtTdsPerc.Text = othind.ToString("F2");
                            }
                            TdsAmountCalculation();
                        }
                    }

                }


            }
        }
        public void TdsAmountCalculation()
        {
            double SGSTAmt = 0, CGSTAmt = 0, IGSTAmt = 0, CessAmt = 0, TotlAmt = 0, finalAmt = 0, Amount = 0, tdsperc = 0, TdsAmount = 0;
            Double amt = 0;
            amt = Convert.ToDouble(txtamout.Text == "" ? "0" : txtamout.Text);
            if (cmbtdsName.SelectedValue != null && cmbpartyname != null)
            {
                using (DataTable dt1 = DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select GEN.TDSCalculation('" + FA.Common.GetFiscalStart(DBConn, DateTime.Now.Date, Convert.ToInt32(GeneralConfig.CompanyID)) + "','" + FA.Common.GetFiscalEnd(DBConn, DateTime.Now.Date, Convert.ToInt32(GeneralConfig.CompanyID)) + "'," + GeneralConfig.CompanyID + "," + GeneralConfig.BranchId + "," + Supledgerid + ",'" + dtp_dt.Value + "','" + amt + "'," + cmbtdsName.SelectedValue + ",0)")).Tables[0])
                {
                    if (dt1.Rows.Count > 0)
                    {
                        txttax.Text = dt1.Rows[0][0].ToString();
                    }
                }
            }
            else
            {
                txttax.Text = "0";

            }
            Amount = Convert.ToDouble(txttax.Text == "" ? "0" : txttax.Text);
            tdsperc = Convert.ToDouble(TxtTdsPerc.Text == "" ? "0" : TxtTdsPerc.Text);
            SGSTAmt = Convert.ToDouble(TxtSGST.Text == "" ? "0" : TxtSGST.Text);
            CGSTAmt = Convert.ToDouble(TxtCGST.Text == "" ? "0" : TxtCGST.Text);
            IGSTAmt = Convert.ToDouble(TxtIGST.Text == "" ? "0" : TxtIGST.Text);
            finalAmt = Convert.ToDouble(txtamout.Text == "" ? "0" : txtamout.Text);
            CessAmt = Convert.ToDouble("0");
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

        private void CmbRefNo_Leave(object sender, EventArgs e)
        {
            if (grbreceipt.Checked == true && ((cmb_Purchasetype.SelectedIndex.ToString() == "1" && cmbpartytype.SelectedValue.ToString() == "1") || (cmb_Purchasetype.SelectedIndex.ToString() == "3" && cmbpartytype.SelectedValue.ToString() == "1") || (cmb_Purchasetype.SelectedIndex.ToString() == "6" && cmbpartytype.SelectedValue.ToString() == "1")))
            {
                CmbRefNo.Visible = false;
                if (CmbRefNo.Text != "" && CmbRefNo.SelectedValue != null)
                {
                    dgv_itemDetails.CurrentRow.Cells["Reference No"].Value = CmbRefNo.Text.ToString();
                    dgv_itemDetails.CurrentRow.Cells["IssId"].Value = CmbRefNo.SelectedValue.ToString();
                    using (DataTable dp = DBConn.GetData(new SqlCommand("Select RecFrom from STK.StockTransferIssueAndReceiptPending('" + cmb_Purchasetype.SelectedIndex.ToString() + "','" + grb.Value + "','" + (cmbpartytype.SelectedValue != null ? cmbpartytype.SelectedValue.ToString() : "1") + "','" + (cmbpartyname.SelectedValue != null ? cmbpartyname.SelectedValue.ToString() : "0") + "', '" + Cmb_StkType.SelectedValue.ToString() + "','STK'," + txtcompId.Text + "," + txtBranchID.Text + ")where TransId=" + CmbRefNo.SelectedValue + " and VchNo='" + CmbReference.Text + "'")).Tables[0])
                    {
                        if (dp.Rows.Count > 0)
                        {
                            String RecFrom = dp.Rows[0]["RecFrom"].ToString();

                            string str = "";
                            if (RecFrom == "STK")
                            {
                                str = "Select TrackingID,IssId,TransId,'STK' as IdType from STK.StockTransferDetails where TransId = " + CmbRefNo.SelectedValue.ToString() + " and Company_id = " + txtcompId.Text + " and Branch_id = " + txtBranchID.Text + "";
                            }
                            else if (RecFrom == "Pool")
                            {
                                str = "Select TrackingID,IssId,TransId,'Pool' as IdType from STK.PoolCreationDetails where TransId = " + CmbRefNo.SelectedValue.ToString() + " and Company_id = " + txtcompId.Text + " and Branch_id = " + txtBranchID.Text + "";
                            }
                            else if (RecFrom == "SUPO")
                            {
                                str = "Select case ISNULL(CustId,0) when 0 then '' else cast(EntryId as varchar(100))+'s'+cast(MetalId as varchar(100)) end as TrackingID,MetalId as IssId,MetalId,'SUPO' as IdType from PUR.SupplierOpeningBalanceWeightDetails where MetalId = " + CmbRefNo.SelectedValue.ToString() + " and Company_id = " + txtcompId.Text + " and Branch_id = " + txtBranchID.Text + "";
                            }
                            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                          (str)).Tables[0])
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    if (cmb_Purchasetype.SelectedIndex.ToString() == "3" || cmb_Purchasetype.SelectedIndex.ToString() == "6")
                                    {
                                        dgv_itemDetails.CurrentRow.Cells["IssId"].Value = dt.Rows[0]["TransId"].ToString();
                                        dgv_itemDetails.CurrentRow.Cells["TrackingID"].Value = dt.Rows[0]["TrackingID"].ToString();
                                        dgv_itemDetails.CurrentRow.Cells["IdType"].Value = dt.Rows[0]["IdType"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Cmb_StkType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbpartytype.SelectedValue == null && cmbpartyname.SelectedValue == null && Cmb_StkType.SelectedValue == null)
                return;
            LoadPendingCombo();
            LoadComboReference();
            BatchId();
        }


        private void BtnLoadPool_Click(object sender, EventArgs e)
        {
            chkbarcode.Checked = true;
            SqlConnection connectionstring = new SqlConnection(DBConn.ConnectionProperties.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "STK.ProcLoadStockTransferPendingProdcodesPool";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Date", dtp_dt.Value.Date.ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@IsReceipt", grb.Value);
            cmd.Parameters.AddWithValue("@TransType", grb.Value);
            cmd.Parameters.AddWithValue("@PurchaseType", cmb_Purchasetype.SelectedIndex);
            cmd.Parameters.AddWithValue("@PartyTypeId", cmbpartytype.SelectedValue);
            cmd.Parameters.AddWithValue("@PartyId", cmbpartyname.SelectedValue);
            cmd.Parameters.AddWithValue("@STKType", Cmb_StkType.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Company_id", (txtcompId.Text != "" ? txtcompId.Text : "0"));
            cmd.Parameters.AddWithValue("@Branch_id", (txtBranchID.Text != "" ? txtBranchID.Text : "0"));
            cmd.CommandTimeout = 0;

            dgv_itemDetails.HiddenDataFields = new List<string>() { "EntryId", "TransId", "JobNo", "JobOrderID", "ProdCodeId", "BatchId", "ItemID", "PureWt", "PurityId", "ModelId", "Mc Perc", "Wst Perc", "Amount", "BranchId", "IssueId", "IsReceipt", "PurId", "DiaNo","StNo", "CertificationCharge", "Amount", "WastageWt", "SalesVA", "PurchaseVA", "TestResult", "Loss", "TransferTransId", "IssueWt", "IssId", "RecId", "Mud", "TrackingID" };
            dgv_itemDetails.SummaryRowVisible = true;
            dgv_itemDetails.DataFields = new List<string> { "EntryId","TransId","BranchId","BranchName","PurityId","JobNo","JobOrderID","ProdCodeId","ProdCode","BatchNo","BatchId", "ItemID", "ModelId", "[Item Name]", "Purity",
           "Nos","Touch","IssueWt","Gwt","DiaNo","StNo","Diawt","DiaCash","StWt","StCash","Mud","NetWt","Rate","MetalCash", "ActualWt","PureWt","Wst","MC","CertificationCharge","Amount", "WastageWt","TotalAmount","[Mc Perc]","[Wst Perc]","IssueId","IsReceipt","PurId","SalesVA","PurchaseVA","TestResult","Loss","TransferTransId","IssId", "RecId","TrackingID"};


            dgv_itemDetails.DataSource = DBConn.GetData(cmd).Tables[0];
            CalculateActualWtwhileAdding();
        }

        private void ChkVchNo_CheckedChanged(object sender, EventArgs e)
        {
            vchno(false);
            if (!IsEditMode)
                SetColumns(dgv_itemDetails);
        }

        private void linkLabel2_Click(object sender, EventArgs e)
        {
            if (PanelPending.Visible == false)
            {
                PanelPending.Parent = this;
                PanelPending.Visible = true;
                PanelPending.BringToFront();
                PanelPending.Show();
                PanelPending.BringToFront();
                PanelPending.Location = new Point(24, 16);
                Double Touch = 0, ActWt = 0, Netwt = 0, SuppDfTouch = 0; int i = 0;
                Netwt = Convert.ToDouble(TxtTotalNetwt.Text);
                ActWt = Convert.ToDouble(txttotalactualwt.Text);
                SuppDfTouch = Convert.ToDouble(txt_touch.Text);
                if (ActWt > 0 && Netwt > 0)
                {
                    Touch = (ActWt / Netwt) * SuppDfTouch;
                }
                TxtTouchPending.Text = Touch.ToString("f3");
                if (TxtTouchPending.Text == "NaN") { TxtTouchPending.Text = "0.00"; }
            }
            else
            {
                PanelPending.Visible = false;
                PanelPending.SendToBack();
                PanelPending.Hide();
            }
        }

        private void CmbReference_SelectedValueChanged(object sender, EventArgs e)
        {
            PenActwt = 0;
            if (CmbReference.SelectedValue != null && Cmb_StkType.SelectedValue != null)
            {

                txtReferenceId.Text = CmbReference.SelectedValue.ToString();
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select EntryId,TransId,VchNo,VchDate,CustName,StockType,PartyName,ItemName,Nos,Gwt,Diawt,Stwt,Netwt,ActWt,RecFrom from STK.StockTransferIssueAndReceiptPending('" + cmb_Purchasetype.SelectedIndex.ToString() + "','" + grb.Value + "','" + (cmbpartytype.SelectedValue != null ? cmbpartytype.SelectedValue.ToString() : "1") + "','" + (cmbpartyname.SelectedValue != null ? cmbpartyname.SelectedValue.ToString() : "0") + "', '" + Cmb_StkType.SelectedValue.ToString() + "','STK'," + txtcompId.Text + "," + txtBranchID.Text + ")where TransId=" + CmbReference.SelectedValue + " and VchNo='" + CmbReference.Text + "'")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtnospending.Text = dt.Rows[0]["Nos"].ToString();
                        txtgwtpending.Text = dt.Rows[0]["Gwt"].ToString();
                        txtdwtpending.Text = dt.Rows[0]["Diawt"].ToString();
                        txtstwtpending.Text = dt.Rows[0]["Stwt"].ToString();
                        PenActwt = Convert.ToDouble(dt.Rows[0]["ActWt"].ToString());
                        String RecFrom = dt.Rows[0]["RecFrom"].ToString();

                        string str = "";
                        if (RecFrom == "STK")
                        {
                            str = "Select TrackingID,IssId,TransId,'STK' as IdType from STK.StockTransferDetails where TransId = " + CmbReference.SelectedValue.ToString() + " and Company_id = " + txtcompId.Text + " and Branch_id = " + txtBranchID.Text + "";
                        }
                        else if (RecFrom == "Pool")
                        {
                            str = "Select TrackingID,IssId,TransId,'Pool' as IdType from STK.PoolCreationDetails where TransId = " + CmbReference.SelectedValue.ToString() + " and Company_id = " + txtcompId.Text + " and Branch_id = " + txtBranchID.Text + "";
                        }
                        else if (RecFrom == "SUPO")
                        {
                            str = "Select case ISNULL(CustId,0) when 0 then '' else cast(EntryId as varchar(100))+'s'+cast(MetalId as varchar(100)) end as TrackingID,MetalId as IssId,MetalId,'SUPO' as IdType from PUR.SupplierOpeningBalanceWeightDetails where MetalId = " + CmbReference.SelectedValue.ToString() + " and Company_id = " + txtcompId.Text + " and Branch_id = " + txtBranchID.Text + "";
                        }
                        else if (RecFrom == "CO")
                        {
                            str = "Select cast(OpenId as varchar(100))+'c'+cast(MetalId as varchar(100))  as TrackingID,MetalId as IssId,MetalId,'CO' as IdType from ACC.CustomerOpeningBalanceWeightDetails where MetalId = " + CmbReference.SelectedValue.ToString() + " and Company_id = " + txtcompId.Text + " and Branch_id = " + txtBranchID.Text + "";
                        }

                        using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand
                                                (str)).Tables[0])
                        {
                            if (dt1.Rows.Count > 0)
                            {
                                if (cmb_Purchasetype.SelectedIndex.ToString() == "1")
                                {
                                    TxtRefTrackingID.Text = dt1.Rows[0]["TrackingID"].ToString();
                                    TxtIssPending.Text = dt1.Rows[0]["IssId"].ToString();
                                    if (TxtIssPending.Text == "0")
                                    {
                                        if (RecFrom == "STK")
                                        {
                                            str = "Select TrackingID,IssId,TransId,'STK' as IdType from STK.StockTransferDetails where EntryId = " + CmbReference.SelectedValue.ToString() + " and Company_id = " + txtcompId.Text + " and Branch_id = " + txtBranchID.Text + "";
                                        }
                                        using (System.Data.DataTable dt2 = DBConn.GetData(new SqlCommand
                                               (str)).Tables[0])
                                        {
                                            if (dt2.Rows.Count > 0)
                                            {
                                                TxtIssPending.Text = dt2.Rows[0]["IssId"].ToString();
                                                TxtRefTrackingID.Text = dt2.Rows[0]["TrackingID"].ToString();
                                            }
                                        }
                                    }
                                    TxtIdTypePending.Text = dt1.Rows[0]["IdType"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            else
            {

                txtnospending.Text = "";
                txtgwtpending.Text = "";
                txtdwtpending.Text = "";
                txtstwtpending.Text = "";
                TxtIssPending.Text = "";
            }
        }

        private void grbButton2_Click(object sender, EventArgs e)
        {
            if (CmbReference.SelectedValue != null)
            {
                txtReferenceId.Text = CmbReference.SelectedValue.ToString();
                if (PenActwt < Convert.ToDouble(txtactwtpending.Text))
                {
                    txtactwtpending.ShowMessage("Actwt Is Greater Than Pending");
                    return;
                }
                dgvPending.Save();
            }
            CmbReference.Focus();
        }

        void SupplierItemDetails()
        {
            if (cmbpartyname.SelectedValue == null)
                return;
            if (cmbpartyname.SelectedValue.ToString().Trim().Length < 2)
                return;
            string str = "select ItemId,isnull(Touch,0.0) as Touch,isnull(McPerGram,0.0) as McPerGram," +
                "isnull(WstPerGram,0.0) as WstPerGram,isnull(MCPerPiece,0.0) as MCPerPiece,isnull(WstPerPiece,0.0) as WstPerPiece " +
                "From PUR.SupplierItemdetails where SuppId='" + cmbpartyname.SelectedValue.ToString() + "' and Company_id=" + txtcompId.Text + "  ";
            SqlDataAdapter da = new SqlDataAdapter(str, DBConn.ConnectionProperties.ConnectionString);
            da.SelectCommand.CommandTimeout = 0;
            SuppItems = new DataTable();
            da.Fill(SuppItems);
        }

        void StkTyeCustomer()
        {
            if (cmbpartytype.SelectedValue == null)
                return;
            if (cmbpartytype.SelectedValue.ToString() == "3" && grbreceipt.Checked == true && cmb_Purchasetype.Text != "Approval")
            {
                Cmb_StkType.SelectedValue = "HS";
              //  Cmb_StkType.Enabled = false;
            }
            else if (cmb_Purchasetype.Text == "JobWork" && cmbpartytype.SelectedValue.ToString() == "3" && grbissue.Checked == true && SoftwareSettings.CompName == "MANJALI")
            {
                Cmb_StkType.SelectedValue = "HS";
            }
        }

        private void TxtTouchPending_TextChanged(object sender, EventArgs e)
        {
            calcPendingActWt();
        }

        void calcPendingActWt()
        {
            if (TxtTouchPending.Text != "" & txtnetwtpending.Text != "")
            {
                Double Netwt = 0, AvgTouch = 0, Actwt = 0, SuppDFTouch = 0;
                Netwt = Convert.ToDouble(txtnetwtpending.Text);
                AvgTouch = Convert.ToDouble(TxtTouchPending.Text != "" ? TxtTouchPending.Text : "0");
                SuppDFTouch = Convert.ToDouble(txt_touch.Text != "" ? txt_touch.Text : "0");
                if (AvgTouch > 0)
                {
                    Actwt = (Netwt * AvgTouch) / SuppDFTouch;
                }
                txtactwtpending.Text = Actwt.ToString(RoundTo);
            }
        }

        private void txtactwtpending_TextChanged(object sender, EventArgs e)
        {
            calcPendingTouch();
        }

        void calcPendingTouch()
        {
            if (this.ActiveControl == txtactwtpending)
            {

                double Netwt = 0, AvgTouch = 0, Actwt = 0, SuppDFTouch = 0;
                txtactwtpending.Text = (string.IsNullOrEmpty(txtactwtpending.Text.Trim()) ? "0.00" : txtactwtpending.Text);
                Netwt = Convert.ToDouble(txtnetwtpending.Text);
                Actwt = Convert.ToDouble(txtactwtpending.Text);
                SuppDFTouch = Convert.ToDouble(txt_touch.Text != "" ? txt_touch.Text : "0");
                if (Actwt > 0 && Netwt > 0)
                {
                    AvgTouch = (Actwt / Netwt) / SuppDFTouch;
                }
                TxtTouchPending.Text = AvgTouch.ToString("f3");
            }
        }

        private void txtgwtpending_TextChanged(object sender, EventArgs e)
        {
            PendingNetWt();
        }

        private void txtstwtpending_TextChanged(object sender, EventArgs e)
        {
            PendingNetWt();
        }

        private void txtdwtpending_TextChanged(object sender, EventArgs e)
        {
            PendingNetWt();
        }

        private void BtnSmthiDetailRpt_Click(object sender, EventArgs e)
        {
            if (cmbpartyname.SelectedValue == null)
                return;
            if (cmbpartytype.SelectedValue.ToString() == "1" && cmbpartyname.SelectedValue.ToString() != "0")
            {
                if (SmithRpt == null || SmithRpt.IsDisposed)
                {
                    SAFA.Forms.STOCK.SmithDetaisDateWiseReportForm1 SmithRpt1 = new SAFA.Forms.STOCK.SmithDetaisDateWiseReportForm1();
                    SmithRpt1.MdiParent = this.ParentForm;
                    ((frmMain)this.ParentForm).ShowChild(SmithRpt1);
                    SmithRpt = SmithRpt1;
                    SmithRpt1.Show();
                    SmithRpt1.Focus();
                    SmithRpt1.BringToFront();
                    SmithRpt1.Cmb_Smith.SelectedValue = cmbpartyname.SelectedValue;
                    SmithRpt1.cmb_Purchasetype.SelectedValue = cmb_Purchasetype.SelectedIndex;
                    SmithRpt1.Display();
                }
                else
                {
                    SmithRpt.MdiParent = this.MdiParent;
                    ((frmMain)this.ParentForm).ShowChild(SmithRpt);
                    SmithRpt.Cmb_Smith.SelectedValue = cmbpartyname.SelectedValue;
                    SmithRpt.cmb_Purchasetype.SelectedValue = cmb_Purchasetype.SelectedIndex;
                    SmithRpt.Display();
                    SmithRpt.BringToFront();
                }
            }
            else
            {
                Gramboo.General.ShowMessage("Select Supplier to view this Report");
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (SD == null || SD.IsDisposed)
            {
                SAFA.Forms.COM.CustomerSearch SD1 = new SAFA.Forms.COM.CustomerSearch();
                SD1.MdiParent = this.ParentForm;
                SD1.target = "CRM";
                SD1.Mode = "A";
                SD1.CellButtonClick += new SAFA.Forms.COM.CustomerSearch.CellClickedEventHandler(CellButtonClick);
                SD = SD1;
                SD1.Show();
                SD1.TopMost = true;
                SD1.Focus();
                SD1.BringToFront();
                SD1.TopMost = false;

                //Cmb_CustName.Enabled = false;
                //txthouse.Enabled = false;
                //txtAddress1.Enabled = false;
                //txtAddress2.Enabled = false;
                //txtPhoneNo.Enabled = false;
            }
            else
            {
                SD.MdiParent = this.MdiParent;
                SD.target = "CRM";
                SD.Mode = "A";
                SD.BringToFront();
            }
        }
        public void CellButtonClick(object sender, SAFA.Forms.COM.CustomerSearch.CellClickedEventArgs e)
        {
            cmbpartyname.SelectedValue = e.TargetID;
            //txtCreditAmount.Text = ((SAFA.Forms.COM.CustomerSearch)sender).CreditAmount;
            //TxtAddress1.Text = ((SAFA.Forms.COM.CustomerSearch)sender).Address1;
            //TxtAddress2.Text = ((SAFA.Forms.COM.CustomerSearch)sender).Address2 ;
            //txtPhoneNo.Text = ((SAFA.Forms.COM.CustomerSearch)sender).Mob;
        }

        private void BtnDocAdd_Click(object sender, EventArgs e)
        {
            SAFA.Forms.COM.DocumentUploader du = new SAFA.Forms.COM.DocumentUploader();
            du.CloseClick += new SAFA.Forms.COM.DocumentUploader.CloseClickEventHandler(DocBtn);
            du.RecTable = "STK.StockTransferMaster";
            du.RecId = txtEntryNo.Text;
            du.ShowDialog();
        }
        public void DocBtn(object sender, SAFA.Forms.COM.DocumentUploader.CloseClickEventArgs e)
        {
            DocButtonChange();
        }
        void DocButtonChange()
        {
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                              ("select DocId From GEN.DocumentMaster where RecTable='STK.StockTransferMaster'and RecId=" + txtEntryNo.Text + " and Company_Id=" + txtcompId.Text + " and Branch_Id=" + txtBranchID.Text + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    BtnDocAdd.Image = SAFA.Properties.Resources.Upload_or_View_Documents;
                    BtnDocAdd.Text = " Upload/View Document";
                }
                else
                {
                    BtnDocAdd.Image = SAFA.Properties.Resources.Upload_Documents;
                    BtnDocAdd.Text = " Upload Document";
                }
            }
        }

        private void txtAmount_Charge_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtnetwtpending_TextChanged(object sender, EventArgs e)
        {
            calcPendingActWt();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void PendingNetWt()
        {
            double gwt = 0, stwt = 0, NetWt = 0, DiaWt = 0;
            txtgwtpending.Text = (string.IsNullOrEmpty(txtgwtpending.Text.Trim()) ? "0.00" : txtgwtpending.Text);
            txtstwtpending.Text = (string.IsNullOrEmpty(txtstwtpending.Text.Trim()) ? "0.00" : txtstwtpending.Text);
            txtdwtpending.Text = (string.IsNullOrEmpty(txtdwtpending.Text.Trim()) ? "0.00" : txtdwtpending.Text);

            gwt = Convert.ToDouble(txtgwtpending.Text);
            stwt = Convert.ToDouble(txtstwtpending.Text);
            DiaWt = Convert.ToDouble(txtdwtpending.Text);
            NetWt = (gwt - stwt - (DiaWt * .2));
            txtnetwtpending.Text = NetWt.ToString(RoundTo);
        }

        private void linkLbl_taxDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (GrpPurTaxDetails.Visible == false)
            {
                GrpPurTaxDetails.BringToFront();
                GrpPurTaxDetails.Size = new System.Drawing.Size(500, 250);
                GrpPurTaxDetails.Location = new Point(linkLbl_taxDetails.Location.X + linkLbl_taxDetails.Parent.Location.X,
                linkLbl_taxDetails.Parent.Location.Y + linkLbl_taxDetails.Location.Y - GrpPurTaxDetails.Height);
                GrpPurTaxDetails.Visible = true;
                GrpPurTaxDetails.BringToFront();
                GrpPurTaxDetails.Parent = this;
                GrpPurTaxDetails.Show();
                linkLbl_taxDetails.Text = "Hide Tax Details";
            }
            else
            {
                GrpPurTaxDetails.Visible = false;
                GrpPurTaxDetails.SendToBack();
                GrpPurTaxDetails.Hide();
                linkLbl_taxDetails.Text = "Add Tax Details";

            }
        }

        private void txtTotalAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalAmount.Text != "")
            {
                Totalamt();
            }
        }

        private void TaxDetails_Button_Click(object sender, EventArgs e)
        {
            if (cmb_Taxname.SelectedValue != null)
            {
                txtTaxId.Text = cmb_Taxname.SelectedValue.ToString();
                dgv_TaxDetails.Save();
            }
            txtItemTaxPerc.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxRate"].Text;
            TxtItemTaxAmt.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxAmt"].Text;
        }

        private void dgv_TaxDetails_SummaryCalculated(object source, EventArgs e)
        {
            txtItemTaxPerc.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxRate"].Text;
            TxtItemTaxAmt.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxAmt"].Text;
        }

        private void TxtItemTotal_TextChanged(object sender, EventArgs e)
        {
            if (TxtItemTotal.Text == "")
                return;
            UpdateTax();
        }

        public void PurchaseTax()
        {
            if (Cmb_ItemTxTypeId.SelectedValue != null)
            {
                dgv_TaxDetails.DataSource = this.DBConn.GetData(new SqlCommand("SELECT CAST(0  as bigint ) AS TaxTrasnsId, CAST(0  as bigint ) as EntryId,TaxId,[Tax Name] as TaxName,[Tax Rate] as TaxRate,Amount as TaxAmt FROM PUR.VPurchaseTaxApplicableMaster WHERE PurTypeId=" + Cmb_ItemTxTypeId.SelectedValue + "")).Tables[0];
                UpdateTax();
            }
        }

        private void grb_wst_CheckedChanged(object sender, EventArgs e)
        {
            if (grb_wst.Checked == true) { CalculateActualWtwhileAdding(); }
        }

        private void grb_cash_CheckedChanged(object sender, EventArgs e)
        {
            if (grb_cash.Checked == true) { CalculateActualWtwhileAdding(); }
        }

        private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dgviewPending.Rows)
            {
                if (r.Cells["VchNo"].Value.ToString() != "Auto Generated Total")
                {
                    r.Cells["Select"].Value = ChkSelectAll.Checked;
                }
            }
        }


        private void grbButton3_Click(object sender, EventArgs e)
        {

            if (cmbpartyname.SelectedValue == null)
                return;
            SAFA.Forms.ACC.FrmSmithBalanceConv StockCon = new SAFA.Forms.ACC.FrmSmithBalanceConv();
            if (STC == null || STC.IsDisposed)
            {

                StockCon.MdiParent = this.ParentForm;
                ((frmMain)this.ParentForm).ShowChild(StockCon);
                //SmithRpt = StockCon;

                StockCon.Focus();
                StockCon.smithRC = this;
                //StockCon.CmbPartyTypeId.SelectedValue = cmbpartytype.SelectedValue;
                StockCon.CmbSuppId.SelectedValue = cmbpartyname.SelectedValue;
                StockCon.TxtCash.Text = (txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text);
                StockCon.TxtRate.Text = (txtRate.Text == "" ? "0" : txtRate.Text);
                StockCon.Show();
                StockCon.BringToFront();
            }

            // StockCon.CmbPartyTypeId.SelectedValue = cmbpartytype.SelectedIndex;


            else
            {
                StockCon.MdiParent = this.MdiParent;
                ((frmMain)this.ParentForm).ShowChild(StockCon);
                StockCon.CmbSuppId.SelectedValue = cmbpartytype.SelectedValue;

                StockCon.Show();
                StockCon.BringToFront();
            }
        }


        private void ChkDellivery_CheckedChanged(object sender, EventArgs e)
        {
            if (SoftwareSettings.CompName == "PEENIKA")
            {
                if (ChkDellivery.Checked == true)
                {
                    txtDelliveryChallanNo.Visible = true;

                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                       ("SELECT [STK].[DelliveryChallanGen](" + txtcompId.Text + "," + txtBranchID.Text + ",1)")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            txtDelliveryChallanNo.Text = dt.Rows[0][0].ToString();
                        }

                    }

                }
                else
                {
                    txtDelliveryChallanNo.Text = "";
                }
            }



        }

        private void SetColumnWidth(Gramboo.Controls.GrbDataGridView dgv, string ColumnName, int width)
        {
            if (dgv.Columns.Contains(ColumnName))
                dgv.Columns[ColumnName].Width = width;
        }

        private void SetColumnWidth(Gramboo.Controls.GrbDataGridView dgv, int ColumnIndex, int width)
        {
            if (ColumnIndex <= dgv.Columns.Count - 1)
                dgv.Columns[ColumnIndex].Width = width;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (GrpPendingSTK.Visible == false)
            {
                GrpPendingSTK.BringToFront();
                GrpPendingSTK.Visible = true;
                GrpPendingSTK.Size = new Size(750, 250);
                dgvPendingSTK.SummaryRowVisible = true;

                SetColumnWidth(dgvPendingSTK, 0, 40);
                SetColumnWidth(dgvPendingSTK, "VchNo", 85);
                SetColumnWidth(dgvPendingSTK, "VchDate", 75);
                SetColumnWidth(dgvPendingSTK, "ItemName", 100);
                SetColumnWidth(dgvPendingSTK, "Touch", 50);
                SetColumnWidth(dgvPendingSTK, "Nos", 40);
                SetColumnWidth(dgvPendingSTK, "Gwt", 60);
                SetColumnWidth(dgvPendingSTK, "DiaWt", 50);
                SetColumnWidth(dgvPendingSTK, "StWt", 50);
                SetColumnWidth(dgvPendingSTK, "NetWt", 60);
                SetColumnWidth(dgvPendingSTK, "ActWt", 60);
            }
            else { GrpPendingSTK.Visible = false; GrpPendingSTK.SendToBack(); GrpPickPending.Visible = false; GrpPickPending.SendToBack(); }

        }

        private void linkLabelPickPending_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (GrpPickPending.Visible == false)
            {
                GrpPickPending.BringToFront();
                GrpPickPending.Visible = true;
                GrpPickPending.Size = new Size(680, 300);
                dgv_PickPendingGrid.DataSource = null;

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SALE.ProcPickPending_ApprovalSelection";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RecFrom", "STK");
                cmd.Parameters.AddWithValue("@PurchaseType", 15);
                cmd.Parameters.AddWithValue("@TransType", 1);
                cmd.Parameters.AddWithValue("@CompanyId", (txtcompId.Text != "" ? txtcompId.Text : "0"));
                cmd.Parameters.AddWithValue("@BranchId", (txtBranchID.Text != "" ? txtBranchID.Text : "0"));
                dgv_PickPendingGrid.RowHeadersVisible = false;
                dgv_PickPendingGrid.DataFields = new List<string> { "Select", "EntryId", "TransId", "VchNo", "VchDate", "PartyName", "ItemID", "ItemName", "Nos", "Gwt", "Diawt", "Stwt", "Netwt", "ActWt" };
                dgv_PickPendingGrid.HiddenDataFields = new List<string> { "EntryId", "TransId", "ItemID", "PartyName", "RecFrom", "PartyNameId", "PartyTypeId", "StockType", "Days_", "STKType" };
                dgv_PickPendingGrid.DataSource = DBConn.GetData(cmd).Tables[0];


                SetColumnWidth(dgv_PickPendingGrid, 0, 40);
                SetColumnWidth(dgv_PickPendingGrid, "VchNo", 85);
                SetColumnWidth(dgv_PickPendingGrid, "VchDate", 75);
                SetColumnWidth(dgv_PickPendingGrid, "ItemName", 100);
                SetColumnWidth(dgv_PickPendingGrid, "Touch", 50);
                SetColumnWidth(dgv_PickPendingGrid, "Nos", 40);
                SetColumnWidth(dgv_PickPendingGrid, "Gwt", 60);
                SetColumnWidth(dgv_PickPendingGrid, "DiaWt", 50);
                SetColumnWidth(dgv_PickPendingGrid, "Stwt", 50);
                SetColumnWidth(dgv_PickPendingGrid, "NetWt", 60);
                SetColumnWidth(dgv_PickPendingGrid, "ActWt", 60);

            }
            else { GrpPickPending.Visible = false; GrpPickPending.SendToBack(); }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)dgvPendingSTK.DataSource;
            try
            {
                dgvPendingSTK.Rows.RemoveAt(0);
            }
            catch (Exception)
            {
            }
            dgv_PickPendingGrid.EndEdit();
            foreach (DataGridViewRow r in dgv_PickPendingGrid.Rows)
            {
                try
                {
                    if ((bool)((DataGridViewCheckBoxCell)r.Cells["select"]).Value == true)
                    {
                        String RecFrom = "STK";
                        if (r.Cells["VchNo"].Value.ToString() == "Supplier Opening") { RecFrom = "SUPO"; } else if (r.Cells["VchNo"].Value.ToString() == "Customer Opening") { RecFrom = "CO"; }
                        using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand
                                                 ("SELECT EntryId as EntryId,TransId as SelectionId,IssId as ReferenceId,[Reference No] as Reference,TrackingID as RefTrackingID,ItemId,[Item Name] as ItemName,Nos,Gwt,StWt as StWt,DiaWt,NetWt,Touch,ActualWt as ActWt,IdType,'" + r.Cells["STKType"].Value.ToString() + "' FROM [STK].[StockTransferIssueAndReceiptPendingAdd]('" + dtp_dt.Value.Date.ToString("dd-MMM-yyyy") + "'," + r.Cells["EntryId"].Value.ToString() + "," + r.Cells["TransId"].Value.ToString() + "," + r.Cells["ItemId"].Value.ToString() + "," + r.Cells["PartyTypeId"].Value.ToString() + ",15,1,'" + RecFrom + "'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                            if (dt1.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt1.Rows)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr.ItemArray = row.ItemArray;
                                    dt.Rows.Add(dr);
                                }
                            }
                    }

                    GrpPickPending.Visible = false;
                    GrpPickPending.SendToBack();
                }
                catch (Exception)
                {

                }

            }
        }

        private void Cmb_PurTypeId_SelectedValueChanged(object sender, EventArgs e)
        {
            PurchaseTax();
        }

        private void dgvPendingSTK_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPendingSTK.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
            dgvPendingSTK.Columns[e.ColumnIndex].DataPropertyName == "StWt" ||
             dgvPendingSTK.Columns[e.ColumnIndex].DataPropertyName == "Diawt")
            {
                CalculateWt(dgvPendingSTK);
                dgvPendingSTK.CurrentRow.Cells["ActWt"].Value = dgvPendingSTK.CurrentRow.Cells["NetWt"].Value;
            }
        }

        private void UpdateTax()
        {
            foreach (DataGridViewRow r in dgv_TaxDetails.Rows)
            {
                double ItemTotal = 0, rate = 0, amount = 0, caltaxtot = 0;
                if (r.Cells["TaxRate"].Value != "" && TxtItemTotal.Text != "")
                {
                    ItemTotal = Convert.ToDouble(TxtItemTotal.Text);
                    //calTax = Convert.ToDouble(txtCalculatetaxAmount.Text);
                    caltaxtot = ItemTotal;
                    rate = Convert.ToDouble(r.Cells["TaxRate"].Value);
                    amount = caltaxtot * rate / 100;
                    r.Cells["TaxAmt"].Value = amount.ToString("F2");
                }
            }

            txtItemTaxPerc.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxRate"].Text;
            TxtItemTaxAmt.Text = dgv_TaxDetails.SummaryRow.SummaryCells["TaxAmt"].Text;
        }

        private void cmbCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
                return;
            if (cmbCode.SelectedValue != null && this.ActiveControl == cmbCode)
            {
                try {
                    cmbpartyname.SelectedValue = cmbCode.SelectedValue;
                }
                catch (Exception ex)
                {

                }
                
                
                }


        }

        private void txt_mcmode_TextChanged(object sender, EventArgs e)
        {

        }

        private void Btnimp_Click(object sender, EventArgs e)
        {
            OpenFileDialog op1 = new OpenFileDialog();
            op1.Multiselect = false;
            op1.Filter = "AllFiles|*.*";

            if (op1.ShowDialog() == DialogResult.OK)
            {

                //txt_file.Text = op1.FileName;
                int counter = 0;
                string line;

                // Read the file and display it line by line.
                System.IO.StreamReader file =
                   new System.IO.StreamReader(op1.FileName);
                if (dgv_itemDetails.CurrentRow == null)
                    return;
                while ((line = file.ReadLine()) != null)
                {


                    dgv_itemDetails.CurrentRow.Cells["ProdCode"].Value = line;

                    counter++;
                }

                file.Close(); 
            }
        }

        private void txt_ShippingNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

       
        private bool pendingsave()
        {
            Double Stwt = 0, dwt = 0, actwt = 0, gwt = 0;
            Double pendingstwt = 0, pendingdwt = 0, pendingactwt = 0, pendinggwt = 0;

            foreach (DataGridViewRow r1 in dgv_itemDetails.Rows)
            {

                gwt += Convert.ToDouble(r1.Cells["Gwt"].Value.ToString() == "" ? "0" : r1.Cells["Gwt"].Value);
                dwt += Convert.ToDouble(r1.Cells["Diawt"].Value.ToString() == "" ? "0" : r1.Cells["Diawt"].Value);
                actwt += Convert.ToDouble(r1.Cells["ActualWt"].Value.ToString() == "" ? "0" : r1.Cells["ActualWt"].Value);
                Stwt += Convert.ToDouble(r1.Cells["StWt"].Value.ToString() == "" ? "0" : r1.Cells["StWt"].Value);

            }

            foreach (DataGridViewRow r2 in dgvPending.Rows)
            {

                pendingstwt += Convert.ToDouble(r2.Cells["StWt"].Value.ToString() == "" ? "0" : r2.Cells["StWt"].Value);
                pendingdwt += Convert.ToDouble(r2.Cells["DiaWt"].Value.ToString() == "" ? "0" : r2.Cells["DiaWt"].Value);
                pendingactwt += Convert.ToDouble(r2.Cells["ActWt"].Value.ToString() == "" ? "0" : r2.Cells["ActWt"].Value);
                pendinggwt += Convert.ToDouble(r2.Cells["Gwt"].Value.ToString() == "" ? "0" : r2.Cells["Gwt"].Value);

            }
            if (Math.Round(actwt, 3) != Math.Round(pendingactwt, 3))
            {
                ShowMessage("Pending Act Wt is " + pendingactwt.ToString());
                return false;
            }
            else
            {
                return true;
            }

        }

        void GSTPerc(String CompStateId, String StateId)
        {
            string SAC = "0";
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                    ("SELECT ISNULL(SACId,0) as SACId,St_DiaCash,MC,MetalCash,TotalAmount FROM STK.VPurchaseTypeMaster where PurchaseTypeId=" + cmb_Purchasetype.SelectedIndex + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    SAC = dt.Rows[0]["SACId"].ToString();
                    ChkSt_DiaCash = Convert.ToBoolean(dt.Rows[0]["St_DiaCash"].ToString());
                    ChkMC = Convert.ToBoolean(dt.Rows[0]["MC"].ToString());
                    ChkMetalCash = Convert.ToBoolean(dt.Rows[0]["MetalCash"].ToString());
                    ChkTotalAmount = Convert.ToBoolean(dt.Rows[0]["TotalAmount"].ToString());

                }
                else
                {
                    ChkSt_DiaCash = false;
                    ChkMC = false;
                    ChkMetalCash = false;
                    ChkTotalAmount = false;
                }
            }

            if (SAC != "0")
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                   ("SELECT  top 1 HSNDetailID,CGSTPerc,SGSTPerc,IGSTPerc,CESSPerc FROM ACC.VHSNMasterSAC where HSNId='" + SAC + "' and date<='" + dtp_dt.Value + "'order by date desc")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (CompStateId == StateId)
                        {
                            txt_cgstperc.Text = dt.Rows[0]["CGSTPerc"].ToString();
                            txt_sgstperc.Text = dt.Rows[0]["SGSTPerc"].ToString();
                            txt_igstperc.Text = "0";
                        }
                        else
                        {
                            txt_cgstperc.Text = "0";
                            txt_sgstperc.Text = "0";
                            txt_igstperc.Text = dt.Rows[0]["IGSTPerc"].ToString();
                        }

                    }
                }
            }
        }

        void PurchaseTypes()
        {
            Gramboo.General.Setupcombo(cmbpartytype, "STK.VSTPartyType", "Type", "Id", "IsActive='True'");
            if (purchaseytype == "Customer Jobwork")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "JobWork";
                cmbpartytype.SelectedValue = 3;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Supplier Jobwork")
            {
                cmbpartytype.SelectedValue = 1;
                cmb_Purchasetype.Text = "JobWork";

                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Branch Jobwork")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "JobWork";
                cmbpartytype.SelectedValue = 0;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Department Jobwork")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "JobWork";
                cmbpartytype.SelectedValue = 4;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "RJCU Jobwork")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "JobWork";
                cmbpartytype.SelectedValue = 3;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "RJSU Jobwork")
            {
                cmb_Purchasetype.Text = "JobWork";
                cmbpartytype.SelectedValue = 1;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Customer Exhibition")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Exhibition";
                cmbpartytype.SelectedValue = 3;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Supplier Exhibition")
            {
                cmb_Purchasetype.Text = "Exhibition";
                cmbpartytype.SelectedValue = 1;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Branch Exhibition")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Exhibition";
                cmbpartytype.SelectedValue = 0;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Department Exhibition")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Exhibition";
                cmbpartytype.SelectedValue = 4;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Branch Transfer")
            {
                cmb_Purchasetype.Text = "BranchTransfer";
                cmbpartytype.SelectedValue = 0;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Customer Repair")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Repair";
                cmbpartytype.SelectedValue = 3;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Supplier Repair")
            {
                cmb_Purchasetype.Text = "Repair";
                cmbpartytype.SelectedValue = 1;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Branch Repair")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Repair";
                cmbpartytype.SelectedValue = 0;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Department Repair")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Repair";
                cmbpartytype.SelectedValue = 4;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Customer Hallmarking")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Hallmarking";
                cmbpartytype.SelectedValue = 3;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Supplier Hallmarking")
            {
                cmb_Purchasetype.Text = "Hallmarking";
                cmbpartytype.SelectedValue = 1;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Branch Hallmarking")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Hallmarking";
                cmbpartytype.SelectedValue = 0;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Department Hallmarking")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Hallmarking";
                cmbpartytype.SelectedValue = 4;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Customer Certification")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Certification";
                cmbpartytype.SelectedValue = 3;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Supplier Certification")
            {
                cmb_Purchasetype.Text = "Certification";
                cmbpartytype.SelectedValue = 1;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Branch Certification")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Certification";
                cmbpartytype.SelectedValue = 0;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Department Certification")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Certification";
                cmbpartytype.SelectedValue = 4;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Inside Jobwork")
            {
                cmb_Purchasetype.Text = "InsideJobWork";
                cmbpartytype.SelectedValue = 1;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Customer Other")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Other";
                cmbpartytype.SelectedValue = 3;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Supplier Other")
            {
                cmb_Purchasetype.Text = "Other";
                cmbpartytype.SelectedValue = 1;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Branch Other")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Other";
                cmbpartytype.SelectedValue = 0;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Department Other")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Other";
                cmbpartytype.SelectedValue = 4;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Customer Refinery")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Refinery";
                cmbpartytype.SelectedValue = 3;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Supplier Refinery")
            {
                cmb_Purchasetype.Text = "Refinery";
                cmbpartytype.SelectedValue = 1;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Branch Refinery")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Refinery";
                cmbpartytype.SelectedValue = 0;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Department Refinery")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Refinery";
                cmbpartytype.SelectedValue = 4;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Department Transfer")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "DepartmentTransfer";
                cmbpartytype.SelectedValue = 4;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Old Gold")
            {
                cmb_Purchasetype.Text = "OldGold";
                cmbpartytype.Enabled = true;
            }
            else if (purchaseytype == "Customer Melting")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Melting";
                cmbpartytype.SelectedValue = 3;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Supplier Melting")
            {
                cmb_Purchasetype.Text = "Melting";
                cmbpartytype.SelectedValue = 1;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Branch Melting")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Melting";
                cmbpartytype.SelectedValue = 0;
                cmbpartytype.Enabled = false;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Melted OG")
            {
                cmb_Purchasetype.Text = "Melted OG";
                cmbpartytype.Enabled = true;
            }
            else if (purchaseytype == "Result")
            {
                cmb_Purchasetype.Text = "Result";
                cmbpartytype.Enabled = true;
            }
            else if (purchaseytype == "Test")
            {
                cmb_Purchasetype.Text = "Test";
                cmbpartytype.Enabled = true;
            }
            else if (purchaseytype == "Customer Approval")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Approval";
                cmbpartytype.SelectedValue = 3;
                cmbpartytype.Enabled = true;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Supplier Approval")
            {
                cmb_Purchasetype.Text = "Approval";
                cmbpartytype.SelectedValue = 1;
                cmbpartytype.Enabled = true;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Branch Approval")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Approval";
                cmbpartytype.SelectedValue = 0;
                cmbpartytype.Enabled = true;
                cmb_Purchasetype.Enabled = false;
            }
            else if (purchaseytype == "Department Approval")
            {
                txt_touch.Text = 100.ToString();
                cmb_Purchasetype.Text = "Approval";
                cmbpartytype.SelectedValue = 4;
                cmbpartytype.Enabled = true;
                cmb_Purchasetype.Enabled = false;
            }
            else
            {
                cmb_Purchasetype.Text = "Purchase";
                cmbpartytype.Enabled = true;
            }

            if (cmb_Purchasetype.Text == "Approval")
            {
                cmbpartytype.Enabled = true;
            }
            PendingLabelText();

            if (SoftwareSettings.CompName == "MANJALI") { if (cmb_Purchasetype.Text == "Approval") { cmbpartytype.SelectedValue = 3; } }
            if (cmb_Purchasetype.Text == "Exhibition") { cmbpartytype.Enabled = true; }
            if (SoftwareSettings.CompName == "CHIRIANKANDATH JEWELLERY") { cmbpartytype.Enabled = true; }
            else if (SoftwareSettings.CompName == "SAFA") { if (cmb_Purchasetype.Text == "BranchTransfer") { cmbpartytype.Enabled = true; } }
        }

        void AutoFillPending()
        {
            if (SoftwareSettings.AutoFillSTKPending == true)
            {

                DataTable dtSTK = new DataTable();
                dtSTK.Columns.Add("Nos"); dtSTK.Columns.Add("Gwt"); dtSTK.Columns.Add("Diawt");
                dtSTK.Columns.Add("Stwt"); dtSTK.Columns.Add("Netwt"); dtSTK.Columns.Add("ActWt");

                DataRow drSTK = null;
                drSTK = dtSTK.NewRow();
                drSTK["Nos"] = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["Nos"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["Nos"].Text).ToString("f0");
                drSTK["Gwt"] = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["Gwt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["Gwt"].Text).ToString("f3");
                drSTK["Diawt"] = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["Diawt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["Diawt"].Text).ToString("f3");
                drSTK["Stwt"] = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["Stwt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["Stwt"].Text).ToString("f3");
                drSTK["Netwt"] = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["Netwt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["Netwt"].Text).ToString("f3");
                drSTK["ActWt"] = Convert.ToDouble(dgv_itemDetails.SummaryRow.SummaryCells["ActualWt"].Text == "" ? "0" : dgv_itemDetails.SummaryRow.SummaryCells["ActualWt"].Text).ToString("f3");
                dtSTK.Rows.Add(drSTK);


                Double Touch = 0, ActWt = 0, Netwt = 0, SuppDfTouch = 0;
                Netwt = Convert.ToDouble(TxtTotalNetwt.Text);
                ActWt = Convert.ToDouble(txttotalactualwt.Text);
                SuppDfTouch = Convert.ToDouble(txt_touch.Text);
                if (ActWt > 0 && Netwt > 0)
                {
                    Touch = (ActWt / Netwt) * SuppDfTouch;
                }



                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "STK.Proc_AutoFillSTKPending";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PurchaseType", cmb_Purchasetype.SelectedIndex.ToString());
                cmd.Parameters.AddWithValue("@TransType", grb.Value);
                cmd.Parameters.AddWithValue("@PartyTypeId", cmbpartytype.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@PartyId ", cmbpartyname.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@STKType", Cmb_StkType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@RecFrom", "STK");
                cmd.Parameters.AddWithValue("@CompanyId ", txtcompId.Text);
                cmd.Parameters.AddWithValue("@BranchId ", txtBranchID.Text);
                cmd.Parameters.AddWithValue("@AvgTouch ", Touch.ToString("f2"));
                cmd.Parameters.AddWithValue("@STKData", dtSTK);

                DataTable dtLocalPending = DBConn.GetData(cmd).Tables[0];

                int rowCount = dgvPending.Rows.Count;
                for (int i = 0; i < rowCount; i++) { dgvPending.Rows.RemoveAt(0); }

                foreach (DataRow dr in dtLocalPending.Rows)
                {
                    DataRow r = ((DataTable)dgvPending.DataSource).NewRow();

                    r["PendingId"] = dr["PendingId"];
                    r["EntryId"] = dr["EntryId"];
                    r["ReferenceId"] = dr["ReferenceId"];
                    r["Reference"] = dr["Reference"];
                    r["RefTrackingID"] = dr["RefTrackingID"];
                    r["Nos"] = dr["Nos"];
                    r["Gwt"] = dr["Gwt"];
                    r["StWt"] = dr["StWt"];
                    r["DiaWt"] = dr["DiaWt"];
                    r["NetWt"] = dr["NetWt"];
                    r["AvgTouch"] = dr["AvgTouch"];
                    r["ActWt"] = dr["ActWt"];
                    r["IdType"] = dr["IdType"];
                    ((DataTable)dgvPending.DataSource).Rows.Add(r);
                }
            }
        }

        public List<string> SetHiddenColumns(String PurchaseType, String TransType, String PartyTypeId)
        {
            List<string> HiddenColumnList = new List<string>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GEN.Proc_SetHiddenColumns";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RecFrom", "StockTransfer");
            cmd.Parameters.AddWithValue("@HiddenType1", TransType);
            cmd.Parameters.AddWithValue("@HiddenType2", PurchaseType);
            cmd.Parameters.AddWithValue("@HiddenType3", PartyTypeId);
            cmd.Parameters.AddWithValue("@CompanyId", GeneralConfig.CompanyID);
            cmd.Parameters.AddWithValue("@BranchId", GeneralConfig.BranchId);

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

        public Tuple<String, String> GET_ItemId_By_Name(String Name)
        {
            String ItemId = "0", ItemName = "";

            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                   ("select * from ITM.GetItemNameById('" + Name + "','" + txtcompId.Text + "','" + txtBranchID.Text + "')")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    ItemId = (dt.Rows[0]["ItemId"].ToString());
                    ItemName = (dt.Rows[0]["ItemName"].ToString());
                }
            }
            return new Tuple<string, string>(ItemId, ItemName);

        }

        void AddToStockConvertion(string Type, Int64 RecId)
        {
            SqlConnection connectionstring = new SqlConnection(DBConn.ConnectionProperties.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connectionstring;
            cmd.CommandText = "STK.Proc_AddToStockConvertion";
            cmd.CommandType = CommandType.StoredProcedure;
            connectionstring.Open();
            cmd.Parameters.AddWithValue("@RecId", RecId);
            cmd.Parameters.AddWithValue("@RecFrom", "STK");
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@Company_Id ", Gramboo.GeneralConfig.CompanyID);
            cmd.Parameters.AddWithValue("@Branch_Id", Gramboo.GeneralConfig.BranchId);
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            connectionstring.Close();
        }

        void SelectionPendGrid()
        {
            dgvPendingSTK.AutoGenerateColumns = true;
            dgvPendingSTK.ShowSerialNo = true;
            dgvPendingSTK.ShowDelete = true;
            dgvPendingSTK.SummaryRowVisible = true;
            dgvPendingSTK.DataFields = new List<string> { "EntryId", "SelectionId", "ReferenceId", "Reference", "RefTrackingID", "ItemId", "ItemName", "Nos", "Gwt", "StWt", "DiaWt", "NetWt", "Touch", "ActWt", "IdType", "STKType", "Company_id", "Branch_id" };
            dgvPendingSTK.SummaryColumns = new string[] { "Nos", "Gwt", "StWt", "DiaWt", "NetWt", "ActWt" };
            dgvPendingSTK.HiddenDataFields = new List<string>() { "SelectionId", "EntryId", "ReferenceId", "RefTrackingID", "ItemId", "IdType", "STKType", "Created_by", "Created_date", "Branch_id", "Last_modified_by", "Last_modified_date", "Company_id", "Branch_id", "Counter_id", "IsActive" };
            dgvPendingSTK.Fill(new Table(SAFA.Classes.Common.DbName, "STK", "VApprovalSelectionJW", true), "1=2");
            if (dgvPending.Columns.Contains("col_AutoSlno"))
                dgvPending.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
        }

        void LoadStock()
        {
            mylist.Items.Clear(); mylist.Visible = true;
            DataController dc = new Gramboo.DataController();
            dc.ConnectionProperties.GenerateSQLConnectionString();
            SqlConnection connectionstring = new SqlConnection(dc.ConnectionProperties.ConnectionString);
            connectionstring.Open();
            string str = "EXEC [STK].[StockStatus_StockSummaryReport]'" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "'," + Gramboo.GeneralConfig.CompanyID + "," + Gramboo.GeneralConfig.BranchId + ",'STK'";

            SqlDataAdapter da = new SqlDataAdapter(str, connectionstring);
            da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Color ColorChanage = Color.Red;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    if (dr["Location"].ToString() == "OWN STOCK") { ColorChanage = Color.DarkGreen; }
                    else if (dr["Location"].ToString() == "HOLDING STOCK") { ColorChanage = Color.DarkRed; }
                    else if (dr["Location"].ToString().Contains("TOTAL")) { ColorChanage = Color.Red; }

                    ListViewItem listitem = new ListViewItem(((dr["SN"].ToString()) != "1" ? "" : dr["ItemName"].ToString()), i);
                    listitem.SubItems.Add(dr["Location"].ToString()).ForeColor = ColorChanage;
                    listitem.SubItems.Add(dr["Netwt"].ToString()).ForeColor = ColorChanage;
                    mylist.Items.Add(listitem);
                    listitem.UseItemStyleForSubItems = false;
                    listitem.SubItems[0].ForeColor = ColorChanage;
                }
            }

            connectionstring.Close();
            dt.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        

       

    }
}




