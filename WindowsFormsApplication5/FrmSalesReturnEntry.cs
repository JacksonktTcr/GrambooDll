using System;
using SAFA.Classes;
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

namespace SAFA.Forms.SALE
{
    public partial class FrmSalesReturnEntry : Gramboo.Controls.GrbForm
    {
        bool BillAdj = true;

        public bool flag;
        bool editflag;

        public double GSTPerc = 0, CESSPerc, CessPerc1;
        public static string dueDate, MetalTpe, CompanyName;
        public static int itemid;
        private static FrmSalesReturnEntry instance;
        private static FrmSalesReturnEntry Salesinstance;
        private static FrmSalesReturnEntry SalesOrderinstance; 
        public static string salesmode;
        public bool AllowInit { get; set; }
        string RecFrom, Entry, BillNo; string Compname = "";

        public static FrmSalesReturnEntry Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new FrmSalesReturnEntry();
                }
                else if (instance.IsDisposed)
                {
                    instance = new FrmSalesReturnEntry();
                }
                else if (instance.SalesMode != salesmode)
                {
                    instance = new FrmSalesReturnEntry();
                }
                return instance;
            }
        }
        public static FrmSalesReturnEntry SalesInstance
        {

            get
            {
                if (Salesinstance == null)
                {
                    Salesinstance = new FrmSalesReturnEntry();
                }
                else if (Salesinstance.IsDisposed)
                {
                    Salesinstance = new FrmSalesReturnEntry();
                }
                return Salesinstance;
            }
        }
        public static FrmSalesReturnEntry SalesOrderInstance
        {

            get
            {
                if (SalesOrderinstance  == null)
                {
                    SalesOrderinstance = new FrmSalesReturnEntry();
                }
                else if (SalesOrderinstance.IsDisposed)
                {
                    SalesOrderinstance = new FrmSalesReturnEntry();
                }
                return SalesOrderinstance;
            }
        }
        public string SalesMode
        {

            get
            {

                return salesmode;
            }

            set
            {
                salesmode = value;

                if (value == "W")
                {
                    grb_salesmode.DefaultRadioButton = grb_WholeSale;
                    grb_WholeSale.Checked = true;                                     
                }
                else
                {
                    grb_salesmode.DefaultRadioButton = grb_Retail;
                    grb_Retail.Checked = true;             
                }
                //cmbMetalType.SelectedValue = "G";
                //ItemTypeChanged(new object(), new EventArgs());

            }
        }      
        public FrmSalesReturnEntry()
        {
            InitializeComponent();
            AllowInit = true;
            foreach (DataGridViewColumn column in dgv_SalesReturn.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void employeesearch(object cmb, EventArgs e)
        {
            string str = "";

            str = ((Gramboo.Controls.GrbComboBox)cmb).Text;
            Gramboo.General.Setupcombo(((Gramboo.Controls.GrbComboBox)cmb), "Emp.VEmployeeSelect", "EmpName", "EmpId", "IsActive='True' AND Company_id=" + txtcompId.Text + " and  Branch_id=" + txtBranchID.Text);

        }
        private void Managersearch(object cmb, EventArgs e)
        {
            string str = "";

            str = ((Gramboo.Controls.GrbComboBox)cmb).Text;
            Gramboo.General.Setupcombo(((Gramboo.Controls.GrbComboBox)cmb), "Emp.VEMangerSelect", "EmpName", "EmpId", "IsActive='True' AND Company_id=" + txtcompId.Text + " and  Branch_id=" + txtBranchID.Text);
      
        }
        private void MarketingExusearch(object cmb, EventArgs e)
        {
            string str = "";

            str = ((Gramboo.Controls.GrbComboBox)cmb).Text;
           
            Gramboo.General.Setupcombo(((Gramboo.Controls.GrbComboBox)cmb), "Emp.VVEMangerSelect", "EmpName", "EmpId");

        }
        public override void Print()
        {           
            
        }

        public override bool InitializeTables()
        {
            Table t = new Table(SAFA.Classes.Common.DbName, "SALE", "SalesReturnMaster");
            t.PrimaryKeys.Add("SalesReturnId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = TxtSaleReturnId;

            Table t1 = new Table(SAFA.Classes.Common.DbName, "SALE", "SalesReturnDetails", true);
            t1.PrimaryKeys.Add("TransId");
            t1.FillView = new Table(SAFA.Classes.Common.DbName, "SALE", "vsalesReturnSetail", true);
            t1.DatagridView = dgv_SalesReturn;
            t1.IsDatagridView = true;
            t1.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t1);

            Table t2 = new Table(SAFA.Classes.Common.DbName, "SALE", "SalesReturnOtherCharges", true);
            t2.PrimaryKeys.Add("OTchId");
            t2.FillView = new Table(SAFA.Classes.Common.DbName, "SALE", "VSalesReturnOtherCharges", true);
            t2.DatagridView = dgv_otherChg;
            t2.IsDatagridView = true;
            t2.IdTextBox = TxtOtchId;
            t.ChildTables.Add(t2);
         
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
            catch (Exception)
            {
                return false;
            }
        }
        private void SetRates()
        {

            if (dtp_InvDate.Value != null || txtcompId.Text != "")
            {
                if (cmbMetalType.SelectedValue.ToString() == "S")
                {
                    txtRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["SilverRate"]).ToString();

                }
                else if (cmbMetalType.SelectedValue.ToString() == "P")
                {
                    txtRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 PlatinumRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["PlatinumRate"]).ToString();
                }
                else
                {
                    txtRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString();

                }


            }
        }

        //private void Print()
        //{
        //    //base.Print();

        //}
        //private void GetProdCodesReturn(bool IsReceipt, Gramboo.Controls.GrbComboBox cmb)
        //{


        //    string str;
        //    string Viewname;
        //    if (txtcompId.Text == "" || txtDeptName.Text == "")
        //        return;
        //    SqlCommand cmd = new SqlCommand();
        //    if (cmbMetalType.SelectedValue.ToString() == "D" || cmbMetalType.SelectedValue.ToString() == "UN" || cmbMetalType.SelectedValue.ToString() == "PR")
        //    {
        //        Viewname = "ITM.VDiamondOrnaments";
        //    }
        //    else if (cmbMetalType.SelectedValue.ToString() == "P")
        //    {
        //        Viewname = "ITM.VPlatinumOrnaments";
        //    }
        //    else if (cmbMetalType.SelectedValue.ToString() == "S")
        //    {
        //        Viewname = "ITM.VSilverOrnaments";
        //    }
        //    else
        //    {
        //        Viewname = "ITM.VGoldOrnaments";
        //    }

        //    if (dgv_SalesReturn.Columns.Contains("FloorId") == false || dgv_SalesReturn.CurrentRow == null)
        //        return;

        //    if (IsReceipt)
        //    {
        //        str = "SELECT ProdCode,ProdCodeId FROM  [STK].[OrnamentsStatus](" + (Convert.ToString(dgv_SalesReturn.CurrentRow.Cells["FloorId"].Value) == "" ? "0" : Convert.ToString(dgv_SalesReturn.CurrentRow.Cells["FloorId"].Value)) + "," + txtDeptName.Text + ",'" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + ") t1 INNER JOIN " + Viewname + " t2 ON t1.ItemId=t2.ItemId  Order By ProdCode";
        //    }
        //    else
        //    {
        //        if (EditMode)
        //        {
        //            str = "SELECT ProdCode,ProdCodeId FROM  [STK].[OrnamentsStatus](" + (Convert.ToString(dgv_SalesReturn.CurrentRow.Cells["FloorId"].Value) == "" ? "0" : Convert.ToString(dgv_SalesReturn.CurrentRow.Cells["FloorId"].Value)) + "," + txtDeptName.Text + ",'" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + ") t1 INNER JOIN " + Viewname + " t2 ON t1.ItemId=t2.ItemId "
        //            + " UNION SELECT prodCode,ProdCodeId FROM STK.ProdCodeMaster WHERE EXISTS (select ProdcodeId FROM SALE.SalesDetails WHERE SalesId=" + TxtSaleReturnId.Text + "  AND SALE.SalesDetails.ProdCodeId=STK.ProdCodeMaster.ProdCodeId) Order By ProdCode";
        //        }
        //        else
        //        {
        //            str = "SELECT ProdCode,ProdCodeId FROM  [STK].[OrnamentsStatus](" + (Convert.ToString(dgv_SalesReturn.CurrentRow.Cells["FloorId"].Value) == "" ? "0" : Convert.ToString(dgv_SalesReturn.CurrentRow.Cells["FloorId"].Value)) + "," + txtDeptName.Text + ",'" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + ") t1 INNER JOIN " + Viewname + " t2 ON t1.ItemId=t2.ItemId  Order By ProdCode";
        //        }
        //    }

        //    cmd.CommandText = str;
        //    cmb.DisplayMember = "ProdCode";
        //    cmb.ValueMember = "ProdCodeId";

        //    cmb.DataSource = DBConn.GetData(cmd, "prodcodes").Tables[0];
        //    cmb.Text = "";


        //}

        private void taxPerc()
        {

            //txtTaxRatePerc.Text = "3";
        //    txt_igstper.Text = "0";
        //    txt_cgstper.Text = "1.5";
        //    txt_sgstper.Text = "1.5";

        }
        public void taxrate()
        {
            if (CmbGST.SelectedValue == null)
                return;
            if (cmb_statename.SelectedValue == null)
                return;

            if (txtcompId.Text != "")
            {
                using (DataTable t = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                     ("select CGSTRate,SGSTRate,IGSTRate,CESS from SALE.GSTMaster where FromDate<='" + dtp_InvDate.Value.ToString("dd-MMM-yyyy") + "' AND ToDate>='" + dtp_InvDate.Value.ToString("dd-MMM-yyy") + "'AND GSTId=" + CmbGST.SelectedValue + "")).Tables[0])
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                      ("select Comp_StateCode from SYST.BranchMaster where BranchId= " + txtBranchID.Text + "")).Tables[0])
                    if (t.Rows.Count > 0)
                    {
                        if (cmb_statename.SelectedValue.ToString() == dt.Rows[0]["Comp_StateCode"].ToString())
                        {
                            txt_cgstper.Text = t.Rows[0]["CGSTRate"].ToString();
                            txt_sgstper.Text = t.Rows[0]["SGSTRate"].ToString();
                            if (grb_Retail.Checked == true)
                            {

                                TxtCESSPerc.Text = t.Rows[0]["CESS"].ToString();


                            }
                            else
                            {
                                TxtCESSPerc.Text = "0.00";
                            }
                            txt_igstper.Text = "0";
                        }
                        else
                        {
                            txt_igstper.Text = t.Rows[0]["IGSTRate"].ToString();
                            if (grb_Retail.Checked == true && cmb_statename.SelectedValue.ToString() == dt.Rows[0]["Comp_StateCode"].ToString())
                            {
                                TxtCESSPerc.Text = t.Rows[0]["CESS"].ToString();
                            }
                            else
                            {
                                TxtCESSPerc.Text = "0.00";
                            }
                            txt_sgstper.Text = "0";
                            txt_cgstper.Text = "0";
                        }

                        GSTPerc = Convert.ToDouble(t.Rows[0]["IGSTRate"].ToString());
                        if (grb_Retail.Checked == true && cmb_statename.SelectedValue.ToString() == dt.Rows[0]["Comp_StateCode"].ToString())
                        {
                            CESSPerc = Convert.ToDouble(t.Rows[0]["CESS"].ToString());

                        }
                        else
                        {
                            CESSPerc = 0;
                        }
                    }
                    else { CmbGST.ShowMessage("This Type has been expired...!!!"); CmbGST.SelectedIndex = -1; CmbGST.Focus(); }
            }
        }

        public void taxamt()
        {
            if (flag || editflag)
                return;
            if (txtcompId.Text == "")
                return;
            double totaltax = 0, netamt = 0, CESSAmt = 0,AmtAfterDisc=0;


            if (txtTxType.Text != "2" || RbRemoveTax.Checked != true)
            {
                if (txtBranchID.Text != "")
                {
                    netamt = Convert.ToDouble(txtTotalReturnAmount.Text) - Convert.ToDouble(TxtOTCharge.Text);

             

                    if (Compname == "CHIRIANKANDATH JEWELLERY")
                    {
                        CESSPerc = 0;
                    }
                    
                    AmtAfterDisc = Math.Round(netamt * 100 / (100 + GSTPerc + CESSPerc)); 
                    
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                   ("SELECT t2.StateId, t2.Statecode FROM SYST.BranchMaster t1,GEN.StateCodeMaster t2 WHERE t1.Comp_StateCode=t2.Stateid And t1.BranchId =" + txtBranchID.Text + " AND t2.Stateid=" + (cmb_statename.SelectedValue == null ? "0" : cmb_statename.SelectedValue))).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {

                            txt_igstamt.Text = "0";
                            txt_sgstamt.Text = ((totaltax ) / 2).ToString("f2");
                            grbTextBox4.Text = ((totaltax) / 2).ToString("f2");
                            TxtCESSAmt.Text = CESSAmt.ToString("f2");
                        }
                        else
                        {
                            grbTextBox4.Text = "0";
                            txt_sgstamt.Text = "0";
                            txt_igstamt.Text = ((totaltax)).ToString("f2");
                            TxtCESSAmt.Text = CESSAmt.ToString("f2");
                        }
                    }
                }
            }
            else
            {
                grbTextBox4.Text = "0";
                txt_sgstamt.Text = "0";
                txt_igstamt.Text = "0";
                TxtCESSAmt.Text = "0";
            }


        }

        public override void Init()
        {
            txt_billNo.Focus();

            if (!AllowInit)
            {
                return;
            }

            base.Init();
          
            TxtIsactive.Text = "1";
            cmbMetalType.Enabled = true;
            PendingItemforSR();
            BillAdj = true;
            BtnBillAdj.Visible = false;
            CmbInvoiceType.SelectedIndex = 0;
 
            // cmb_returnbranch.SelectedValue = txtBranchID.Text;
            Gramboo.General.Setupcombo(cmb_statename, "GEN.StateCodeMaster", "StateName", "StateID");

            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
             ("SELECT t2.StateId, t2.Statecode FROM SYST.BranchMaster t1,GEN.StateCodeMaster t2 WHERE t1.Comp_StateCode=t2.Stateid And t1.BranchId =" + txtBranchID.Text)).Tables[0]) 
            {
                if (dt.Rows.Count > 0)
                {
                    cmb_statename.SelectedValue = (dt.Rows[0]["StateId"] == DBNull.Value ? "" : dt.Rows[0]["StateId"].ToString());
                    txtstatecode.Text = (dt.Rows[0]["StateCode"] == DBNull.Value ? "" : dt.Rows[0]["StateCode"].ToString());
                }
            }
            SetRates();
            if (cmbMetalType.SelectedValue.ToString() == "D" || cmbMetalType.SelectedValue.ToString() == "UN" || cmbMetalType.SelectedValue.ToString() == "PR")
            {
                Gramboo.General.Setupcombo(cmb_purity_Return, "ITM.VPurityMaster", "[Purity Name]", "PurityId", "[Is Active]='True' and PurityId in(5,6)");
            }
           
           // SetColumnsReturn(dgv_SalesReturn);
            if (this.TableName != null)
                GenerateID(this.TableName);
            if (txtBranchID.Text == "106")
            {
                cmbMetalType.SelectedValue = "D";          
            }
            else
            {
                cmbMetalType.SelectedValue = "G";
                if (grb_WholeSale.Checked)
                {
                    cmb_VoucherTypeId.SelectedValue = 117;
                }
                else
                {
                    cmb_VoucherTypeId.SelectedValue = 87;
                }
            }
            CmbSm.Focus();
            Rb_Transfer.Checked = true;
            //DataTable dt1 = DBConn.GetData(new System.Data.SqlClient.SqlCommand("select credit_Ac_id ,CardBankName from Acc.vBankSettings where branchid=" + txtBranchID.Text)).Tables[0];
            //cmbbank.Text = dt1.Rows[0]["CardBankName"].ToString();
            //cmbbank.SelectedValue = dt1.Rows[0]["credit_Ac_id"].ToString();

            DataTable dt2 = DBConn.GetData(new System.Data.SqlClient.SqlCommand("select check_Ac_id ,ChequeBankName from Acc.vBankSettings where branchid=" + txtBranchID.Text)).Tables[0];
            cmb_BankAccId.Text = dt2.Rows[0]["ChequeBankName"].ToString();
            cmb_BankAccId.SelectedValue = dt2.Rows[0]["check_Ac_id"].ToString();
            if (salesmode == "W")
            {
                grb_WholeSale.Checked = true;
            }
            else
            {
                grb_Retail.Checked = true;
            }
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                  ("SELECT sales_retu_invo_type FROM SYST.Settings WHERE Branch_id =" + txtBranchID.Text)).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    string invtype;
                    invtype = dt.Rows[0][0].ToString();
                    if (invtype.ToUpper() == "MRP")
                    {
                        rmrp.Checked = true;
                    }
                    else
                    {
                        rva.Checked = true;
                    }
                }
            }
            
            using (DataTable tt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                                       ("select Comp_Name from SYST.CompanyMaster where Company_id= " + txtcompId.Text + "")).Tables[0])
            {
                if (tt.Rows.Count > 0)
                {

                    Compname = tt.Rows[0]["Comp_Name"].ToString();
                }
            }
        }
        public override bool FillData(Dictionary<string, object> PrimaryValues)
        {
            flag = true;
            editflag = true;
            if (base.FillData(PrimaryValues))
            {
                taxrate();
                getAmt();

                BillAdj = false; bool BillWise = false;
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                      ("select isnull(BillWise,0) as BillWise from ACC.Vtds where PartyType='Customer' and PartyId =" + txtCustId.Text + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        BillWise = Convert.ToBoolean(dt.Rows[0]["BillWise"].ToString());
                    }
                }
                
                //AdjustColumnWidths();
                flag = false;
                editflag = false;
                return true;
            }
            else
            {
                flag = false;
                editflag = false;
                return false;
            }

        }
        private void State()
        {
            //Gramboo.General.Setupcombo(cmb_statename, "GEN.StateCodeMaster", "StateName", "StateID");

            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                        ("SELECT t2.StateId, t2.Statecode FROM SYST.BranchMaster t1,GEN.StateCodeMaster t2 WHERE t1.Comp_StateCode=t2.Stateid And t1.BranchId =" + txtBranchID.Text)).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    cmb_statename.SelectedValue = (dt.Rows[0]["StateId"] == DBNull.Value ? "" : dt.Rows[0]["StateId"].ToString());
                    txtstatecode.Text = (dt.Rows[0]["StateCode"] == DBNull.Value ? "" : dt.Rows[0]["StateCode"].ToString());
                }

            }
        }
        void CompanyNameCheck()
        {
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                 ("select Comp_Name from SYST.CompanyMaster where Comp_id=" + txtcompId.Text + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    CompanyName = dt.Rows[0]["Comp_Name"].ToString();
                }
            }
        }
        public override void RefreshData()
        {
            base.RefreshData();
            string Viewname;
            txt_billNo.Focus(); CompanyNameCheck();
            employeesearch(CmbSm, new EventArgs());
            Managersearch(CmbManager, new EventArgs());
            
            MarketingExusearch(CmbMManager, new EventArgs());
            CmbSm.Enabled=true;
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            // cmb_VoucherTypeId.SelectedValue = (int)SAFA.Classes.VoucherTypes.SalesReturn;
            // cmb_VoucherTypeId.SelectedValue = 66;
            //if (!IsEditMode)
            //   txtInvNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtp_InvDate.Value,
            //    DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));

            // TxtVoucherNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtp_InvDate.Value,
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            Gramboo.General.Setupcombo(Cmb_ModelName_Return, "ITM.ModelMaster", "ModelName", "ModelId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmbMetalType, "SALE.VSalesTypes", "MetalTypeName", "MetalType");
            Gramboo.General.Setupcombo(Cmb_Chargename, "PUR.MiscChargeMaster", "ChargeName", "ChargeId", "IsActive='True'  AND  Company_Id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
            cmbEstno.SelectedValueChanged -= cmbEstno_SelectedValueChanged;
           Gramboo.General.Setupcombo(cmbEstno, "SALE.PendingSalesRtnEstimate", "InvNo", "SalesReturnId", "InvDate='" + dtp_InvDate.Value.ToString("dd-MMM-yyyy") + "' and RECFrom='N'");
            cmbEstno.SelectedValueChanged += cmbEstno_SelectedValueChanged;
            CmbGST.SelectedValueChanged -= CmbGST_SelectedValueChanged;
            Gramboo.General.Setupcombo(CmbGST, "SALE.GSTMaster", "GSTName", "GSTId", "IsActive='True'  AND  Company_Id=" + txtcompId.Text + " AND Branch_id=" + txtBranchID.Text);
            

            CmbSm.Enabled = true;
            if (txtBranchID.Text == "106")
            {

                cmbMetalType.SelectedValue = "D";
                cmb_VoucherTypeId.SelectedValue = 88;
            }
            else
            {
                cmbMetalType.SelectedValue = "G";
                if (grb_WholeSale.Checked== true)
                {
                    cmb_VoucherTypeId.SelectedValue = 117;
                }
                else
                {
                    
                    cmb_VoucherTypeId.SelectedValue = 87;
                }
            }

            //Gramboo.General.Setupcombo(cmbsubitemreturn, "ITM.SubItemMaster", "SubItemName", "SubItemId");
            Gramboo.General.Setupcombo(cmb_purity_Return, "ITM.VPurityMaster", "[Purity Name]", "PurityId", "[Is Active]='True'");
            // txtInvNo.Focus();
            Gramboo.General.Setupcombo(cmb_BankAccId, "ACC.BankAccountMaster", "Acc_BankAccName", "Acc_BankAccId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmb_returnbranch, "SYST.BranchMaster", "BranchName", "BranchId", "IsActive='True'");

            cmb_returnbranch.SelectedValue = txtBranchID.Text;
            if (cmbMetalType.SelectedValue.ToString() == "D" || cmbMetalType.SelectedValue.ToString() == "UN" || cmbMetalType.SelectedValue.ToString() == "PR")
            {
                Viewname = "ITM.VDiamondOrnaments";
               // cmb_VoucherTypeId.SelectedValue = 88;
            }


            else if (cmbMetalType.SelectedValue.ToString() == "P")
            {
                Viewname = "ITM.VPlatinumOrnaments";
               // cmb_VoucherTypeId.SelectedValue = 67;
            }


            else if (cmbMetalType.SelectedValue.ToString() == "S")
            {
                Viewname = "ITM.VSilverOrnaments";
               // cmb_VoucherTypeId.SelectedValue = 67;
            }


            else
            {
                Viewname = "ITM.VGoldOrnaments";
//cmb_VoucherTypeId.SelectedValue = 67;
            }
            Gramboo.General.Setupcombo(Cmb_ItemName_Return, Viewname, "[Item Name]", "ItemId", "[Is Active]='True' ");
           
            Gramboo.General.Setupcombo(cmbtdsName, "GEN.TDSMaster ", "TdsName", "TdsId", "IsActive='True'");
            
           
            OthChGrid();
        }

        private void MetalRate(DataGridViewRow r)
        {

           Int64 MetalId = 0; Double Metalrate = 0;

            if (r.Cells["ProdcodeId"].Value.ToString() != "")
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                ("SELECT isnull(t1.MetalId,0) as MetalId,ISNULL(t2.PurityValue,0) as [Purity Value],ISnull(t2.PurityId,0) as PurityId  FROM ITM.VItemMaster t1 ,STK.VProdCodeMaster t2 WHERE t1.ItemId=t2.ItemId AND  prodcodeid =" + r.Cells["prodcodeid"].Value + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        MetalId = Convert.ToInt64(dt.Rows[0]["MetalId"].ToString());
                        r.Cells["Touch"].Value = Convert.ToDouble(dt.Rows[0]["Purity Value"].ToString());
                        r.Cells["PurityId"].Value = Convert.ToDouble(dt.Rows[0]["PurityId"].ToString());
                        //dgv.CurrentRow.Cells["Purity Value"].Value = Convert.ToDouble(dt.Rows[0]["Purity Value"].ToString()); 
                    }

                    if (txtRate.Text == "0")
                    {
                        SetRates();
                    }

                    Metalrate = Convert.ToDouble(txtRate.Text);

                    r.Cells["MetalRate"].Value = Metalrate.ToString();
                }
            }

            else if (r.Cells["ItemId"].Value.ToString() != "")
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                ("SELECT isnull(MetalId,0) as MetalId,ISNULL([Purity Value],0) as [Purity Value],ISnull(PurityId,0) as PurityId  FROM ITM.VItemMaster WHERE ItemId =" + r.Cells["ItemId"].Value + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        MetalId = Convert.ToInt64(dt.Rows[0]["MetalId"].ToString());
                        r.Cells["Touch"].Value = Convert.ToDouble(dt.Rows[0]["Purity Value"].ToString());
                        r.Cells["PurityId"].Value = Convert.ToDouble(dt.Rows[0]["PurityId"].ToString());
                        //dgv.CurrentRow.Cells["Purity Value"].Value = Convert.ToDouble(dt.Rows[0]["Purity Value"].ToString()); 
                    }

                    if (txtRate.Text == "0")
                    {
                        SetRates();
                    }

                    Metalrate = Convert.ToDouble(txtRate.Text);

                    r.Cells["MetalRate"].Value = Metalrate.ToString();

                }
            }
        }


        private void MetalRate(Gramboo.Controls.GrbDataGridView dgv)
        {


            foreach (DataGridViewRow r in dgv.Rows)
            {
                MetalRate(r);
            }
        }

        public void SetColumnsReturn(Gramboo.Controls.GrbDataGridView dgv_SalesReturn)
        {
            if (cmbMetalType.SelectedValue == null)
                return;
            int Split;
           List<string> datafields = new List<string>();
            List<string> Hiddenfields = new List<string>();
            string[] summaryfields;

            bool hidefloor;


            //if (cmb_FloorName.Items.Count < 2)
            //hidefloor = true;
            // else
            //hidefloor = false;
            // dgv.DataSource = null;
            datafields = new List<string>() {"TransId","ProdCodeId","ProdCode","HuId","ItemId","Code", "ItemName",
                "SalesReturnId","Created_date","Created_by","Last_modified_by","Last_modified_date","Company_id","Branch_id","Counter_id","IsActive",
                    "ModelId","Model","PurityID","PurityName","Touch",
                    "Qty","Gwt","StoneWt","StoneCtWt","MetalRate","DiaNo","DiaWt","DiaCash","CertificationCharge","NetWt","Wastage",
                   "MC","WastageCash","VAPerc","VAPercAfterDisc","VA","IsReceipt","MetalCash","StCashDiff", "StoneCash","Total" ,"Type","Disc","SalesTransId"};

            summaryfields = new string[]{ "Qty","NetWt","MetalCash",
                   "DiaNo","DiaWt","DiaCash","CertificationCharge","StoneCash","StoneWt","VA","Gwt", "Total", "MC","Wastage","WastageCash","StCashDiff" };

            Split = Convert.ToInt32(DBConn.GetData(new System.Data.SqlClient.SqlCommand("select * From SYST.Settings ")).Tables[0].Rows[0]["SplitVa"]);
            if (Split == 1)
            {


                if (cmbMetalType.SelectedValue.ToString() == "D" || cmbMetalType.SelectedValue.ToString() == "UN" || cmbMetalType.SelectedValue.ToString() == "PR")
                {

                    Hiddenfields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId","IsReceipt","StCashDiff" 
                         ,"SalesReturnId","Created_date","Created_by","Last_modified_by","Last_modified_date","Company_id","Branch_id","Counter_id","IsActive",
                        "FloorId","FloorName","Stoneless","CertificationCharge","IsReceipt",
                   "VAPerc","VA", "WastageCash","PurityID","Touch" ,"MetalCash","Type" ,"Disc","SalesTransId"};

                }

                else if (cmbMetalType.SelectedValue.ToString() == "S")
                {
                    Hiddenfields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "IsReceipt", "StCashDiff",
                         "SalesReturnId","Created_date","Created_by","Last_modified_by","Last_modified_date","Company_id","Branch_id","Counter_id","IsActive",
                           "FloorId","FloorName","Stoneless","CertificationCharge","IsReceipt",
                   "VAPerc","VA","DiaNo","WastageCash","DiaWt","DiaCash","PurityID","PurityName","Touch" ,"MetalCash","Type","DiaRate","Disc","SalesTransId" };

                }
                else if (cmbMetalType.SelectedValue.ToString() == "W")
                {
                    Hiddenfields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId","ModelId","Model","PurityID","PurityName","Touch", "ProdCodeId","Gwt","StoneWt","NetWt"
                        , "SalesReturnId","Created_date","Created_by","Last_modified_by","Last_modified_date","Company_id","Branch_id","Counter_id","IsActive",
                          "FloorId","FloorName","Stoneless","CertificationCharge","IsReceipt"
                   ,"Wastage","IsReceipt", "StCashDiff","StoneCash", "VAPerc","VA","DiaNo","MC","WastageCash","DiaWt","DiaCash","PurityID","Type","DiaRate","Disc","VAPercAfterDisc","MetalCash","CertificationCharge","SalesTransId"};

                }
                else
                {

                    Hiddenfields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "IsReceipt",  "StCashDiff",
                         "SalesReturnId","Created_date","Created_by","Last_modified_by","Last_modified_date","Company_id","Branch_id","Counter_id","IsActive",
                           "FloorId","FloorName","Stoneless","CertificationCharge","IsReceipt",
                   "VAPerc","VA","DiaNo","WastageCash","DiaWt","DiaCash","PurityID","PurityName","Touch", "MetalCash","Type","DiaRate","SalesTransId" };

                }


            }


            else
            {

                if (cmbMetalType.SelectedValue.ToString() == "D" || cmbMetalType.SelectedValue.ToString() == "UN" || cmbMetalType.SelectedValue.ToString() == "PR")
                {

                    Hiddenfields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "IsReceipt",  "StCashDiff",
                         "SalesReturnId","Created_date","Created_by","Last_modified_by","Last_modified_date","Company_id","Branch_id","Counter_id","IsActive",
                           "FloorId","FloorName","Stoneless","CertificationCharge","IsReceipt",
                    "MC","Wastage","WastageCash", "PurityID","Touch",  "MetalCash","Type","Disc","SalesTransId" };

                }
                else if (cmbMetalType.SelectedValue.ToString() == "S")
                {
                    Hiddenfields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "IsReceipt", "StCashDiff",
                         "SalesReturnId","Created_date","Created_by","Last_modified_by","Last_modified_date","Company_id","Branch_id","Counter_id","IsActive",
                           "FloorId","FloorName","Stoneless","CertificationCharge","IsReceipt",
                     "MC","Wastage","WastageCash","DiaNo","DiaWt","DiaCash", "PurityID","PurityName","Touch", "MetalCash","Type","DiaRate","Disc","SalesTransId"};

                }
                else if (cmbMetalType.SelectedValue.ToString() == "W")
                {
                    Hiddenfields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId","ModelId","Model","PurityID","PurityName","Touch", "ProdCodeId","Gwt","StoneWt","NetWt"
                         ,"SalesReturnId","Created_date","Created_by","Last_modified_by","Last_modified_date","Company_id","Branch_id","Counter_id","IsActive"
                         ,  "FloorId","FloorName","Stoneless","CertificationCharge","IsReceipt"
                   ,"Wastage","IsReceipt", "StCashDiff","StoneCash", "VAPerc","VA","DiaNo","MC","WastageCash","DiaWt","DiaCash","PurityID" ,"Type","DiaRate","Disc","VAPercAfterDisc","MetalCash","CertificationCharge","SalesTransId" };

                }
                else
                {

                    Hiddenfields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "IsReceipt",  "StCashDiff",
                         "SalesReturnId","Created_date","Created_by","Last_modified_by","Last_modified_date","Company_id","Branch_id","Counter_id","IsActive",
                           "FloorId","FloorName","Stoneless","CertificationCharge","IsReceipt",
                     "MC","Wastage","WastageCash","DiaNo","DiaWt","DiaCash","PurityID","PurityName","Touch", "MetalCash","Type","DiaRate" ,"Disc","SalesTransId"};

                }

            }


            

            //if (hidefloor)
            //{
            //    Hiddenfields.Add("FloorName");
            //}
            //else
            //{
            //    Hiddenfields.Remove("FloorName");
            //}
            GrpMetalCashCalc.Visible = false;
            if (txtBranchID.Text != "")
            {
                if (!SAFA.Classes.Common.StoneCtWtVisible(DBConn, Convert.ToInt32(txtBranchID.Text)))
                {
                    Hiddenfields.AddRange(new List<string> { "StoneCtWt" });

                }
            } 
            dgv_SalesReturn.DataFields = datafields;
            dgv_SalesReturn.HiddenDataFields = Hiddenfields;
            dgv_SalesReturn.SummaryColumns = summaryfields;
            if (dgv_SalesReturn.Columns.Contains("StoneAmt"))
            {
                dgv_SalesReturn.Columns["StoneCash"].HeaderText = "StoneAmt";
                dgv_SalesReturn.Columns["DiaCash"].HeaderText = "DmdAmt";

            }
            if (grb_WholeSale.Checked == true) { Hiddenfields.Remove("Touch"); GrpMetalCashCalc.Visible = true; }

        }

        //int Split;
        //List<string> datafields = new List<string>();
        //List<string> Hiddenfields = new List<string>();
        //string[] summaryfields;


        //if (cmb_FloorName.Items.Count < 2)
        //hidefloor = true;
        // else
        //hidefloor = false;
        // dgv.DataSource = null;
        //datafields = new List<string>() {"SalesReturnId","TransId","ProdCodeId","ProdCode","ItemId", "[ItemName]",
        //        "ModelId","[Model]","PurityID","PurityName","Touch",
        //        "Qty","Gwt","[StoneWt]","MetalRate","DiaNo","DiaWt","DiaCash","NetWt","Wastage","DiaRate",
        //       "MC","[WastageCash]","VAPerc","VA","[MetalCash]","StCashDiff", "[StoneCash]","TotalAmount","FloorId","FloorName","Type","Stoneless"};

        //summaryfields = new string[] { "Gwt","NetWt","TotalAmount" };
        //summaryfields = new string[] {"TotalAmount"};


        //Split = Convert.ToInt32(DBConn.GetData(new System.Data.SqlClient.SqlCommand("select * From SYST.Settings ")).Tables[0].Rows[0]["SplitVa"]);
        //if (Split == 1)
        //{


        //    if (cmbMetalType.SelectedValue.ToString() == "D" || cmbMetalType.SelectedValue.ToString() == "UN" || cmbMetalType.SelectedValue.ToString() == "PR")
        //    {

        //        Hiddenfields = new List<string>() { "TransId", "SalesReturnId", "PurityID", "ItemId", "ModelId", "ProdCodeId","ProdCode", "StCashDiff" ,  "FloorId",
        //       "VAPerc","VA", "WastageCash","PurityID","Touch","FloorName","MetalCash" };

        //    }

        //    else if (cmbMetalType.SelectedValue.ToString() == "S")
        //    {
        //        Hiddenfields = new List<string>() { "TransId", "SalesReturnId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "ProdCode", "FloorId","StCashDiff",
        //       "VAPerc","VA","DiaNo","WastageCash","DiaWt","DiaCash","DiaRate","PurityID","PurityName","Touch","FloorName","MetalCash","Type" };

        //    }
        //    else if (chksubitem.Checked)
        //    {
        //        Hiddenfields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId","SubItemId", "ModelId", "ProdCodeId","ProdCode", "IsReceipt", "FloorId","StCashDiff",
        //       "VAPerc","VA","DiaNo","WastageCash","DiaWt","DiaCash","PurityID","PurityName","Touch","FloorName","MetalCash","Type","DiaRate","DiscountedDiaRate","VAPercAfterDisc" };

        //    }
        //    else
        //    {

        //        Hiddenfields = new List<string>() { "TransId", "SalesReturnId", "PurityID", "ItemId", "ModelId", "ProdCodeId","ProdCode",  "FloorId","StCashDiff",
        //       "VAPerc","VA","DiaNo","WastageCash","DiaWt","DiaCash","DiaRate","PurityID","PurityName","Touch","FloorName","MetalCash","Type" };

        //    }


        //}


        //else
        //{

        //    if (cmbMetalType.SelectedValue.ToString() == "D" || cmbMetalType.SelectedValue.ToString() == "UN" || cmbMetalType.SelectedValue.ToString() == "PR")
        //    {

        //        Hiddenfields = new List<string>() { "TransId", "SalesReturnId", "PurityID", "ItemId", "ModelId", "ProdCodeId","ProdCode",   "FloorId","StCashDiff",
        //        "MC","Wastage","WastageCash", "PurityID","Touch","FloorName","MetalCash" };

        //    }
        //    else if (cmbMetalType.SelectedValue.ToString() == "S")
        //    {
        //        Hiddenfields = new List<string>() { "TransId", "SalesReturnId", "PurityID", "ItemId", "ModelId", "ProdCodeId","ProdCode",  "FloorId","StCashDiff",
        //         "MC","Wastage","WastageCash","DiaNo","DiaWt","DiaCash","DiaRate", "PurityID","PurityName","Touch","FloorName","MetalCash","Type"};

        //    }
        //    else if (chksubitem.Checked)
        //    {
        //        Hiddenfields = new List<string>() { "TransId", "SalesId", "PurityID", "ItemId","SubItemId", "ModelId", "ProdCodeId", "ProdCode","IsReceipt", "FloorId","StCashDiff",
        //         "MC","Wastage","WastageCash","DiaNo","DiaWt","DiaCash","PurityID","PurityName","Touch","FloorName","MetalCash","Type","DiaRate","DiscountedDiaRate","VAPercAfterDisc" };

        //    }
        //    else
        //    {

        //        Hiddenfields = new List<string>() { "TransId", "SalesReturnId", "PurityID", "ItemId", "ModelId", "ProdCodeId", "ProdCode","FloorId","StCashDiff",
        //         "MC","Wastage","WastageCash","DiaNo","DiaWt","DiaCash","DiaRate","PurityID","PurityName","Touch","FloorName","MetalCash","Type"};

        //    }

        //}


        //dgv_SalesReturn.DataFields = datafields;
        //dgv_SalesReturn.HiddenDataFields = Hiddenfields;
        //dgv_SalesReturn.SummaryColumns = summaryfields;

        //dgv_SalesReturn.DataSource = null;
        //dgv_SalesReturn.Fill(new Table(SAFA.Classes.Common.DbName, "SALE", "vsalesReturnSetail", true), "1=2");
        //dgv_SalesReturn.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
        //dgv_SalesReturn.AllowUserToAddRows = false;
        //dgv_SalesReturn.Columns["StoneWt"].ReadOnly = true;

        //dgv.Columns["StoneCash"].ReadOnly = false;
        //dgv_SalesReturn.Columns["MetalRate"].ReadOnly = false;
        //if (cmbMetalType.SelectedValue.ToString() == "S")
        //{
        //    lblType.Text = "Silver";
        //    dgv_SalesReturn.Columns["MetalRate"].ReadOnly = false;


        //}
        //else if (cmbMetalType.SelectedValue.ToString() == "D" || cmbMetalType.SelectedValue.ToString() == "UN" || cmbMetalType.SelectedValue.ToString() == "PR")
        //{
        //    lblType.Text = "Diamond";
        //    dgv_SalesReturn.Columns["StoneWt"].ReadOnly = false;
        //    dgv_SalesReturn.Columns["DiaCash"].ReadOnly = true;
        //}
        //else if (cmbMetalType.SelectedValue.ToString() == "P")
        //{

        //    lblType.Text = "Platinum";
        //    dgv_SalesReturn.Columns["MetalRate"].ReadOnly = false;

        //}
        //else
        //{
        //    lblType.Text = "Gold";

        //}
        //((System.Data.DataTable)dgv_SalesReturn.DataSource).Rows.Add(((System.Data.DataTable)dgv_SalesReturn.DataSource).NewRow());
        //dgv_SalesReturn.SelectionMode = DataGridViewSelectionMode.CellSelect;
        //dgv_SalesReturn.BeginEdit(false);
        //SetColumns(dgv);



        public void CalculateWt(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double StoneWt = 0, DiaWt = 0, TotalWt = 0, Gwt = 0, StConwt = 0, DiaConwt = 0;
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
                Gwt = Convert.ToDouble(dgv.CurrentRow.Cells["Gwt"].Value.ToString());
            }

            if (dgv.CurrentRow.Cells["StoneWt"].Value.ToString() != "")
            {
                StConwt = Convert.ToDouble(dgv.CurrentRow.Cells["StoneWt"].Value.ToString());

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
                DiaConwt = Convert.ToDouble(dgv.CurrentRow.Cells["DiaWt"].Value.ToString());

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

            //if (Convert.ToBoolean(dgv.CurrentRow.Cells["Stoneless"].Value != DBNull.Value) == true)
            //{
            TotalWt = Gwt - StoneWt- (DiaWt *.2f);
            dgv.CurrentRow.Cells["NetWt"].Value = TotalWt.ToString("f3");
            //}
            //else
            //{
            // dgv.CurrentRow.Cells["NetWt"].Value = Gwt.ToString();
            //}
        }
        private void fillDiscountedDiaRate(Gramboo.Controls.GrbDataGridView dgv)
        {
            if (dgv.Columns.Contains("DiaRate") == false)
                return;
            Double FixedDiaDiscRate = Convert.ToInt32(DBConn.GetData(new System.Data.SqlClient.SqlCommand("select * From SYST.Settings ")).Tables[0].Rows[0]["FixedDiaDiscRate"]);

            Double Diarate = 0, DiaWt = 0;
            if (dgv.CurrentRow == null)
                return;
            if (dgv.CurrentRow.Cells["DiaRate"].Value.ToString() != "")
            {
                Diarate = Convert.ToDouble(dgv.CurrentRow.Cells["DiaRate"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["DiaWt"].Value.ToString() != "")
            {
                DiaWt = Convert.ToDouble(dgv.CurrentRow.Cells["DiaWt"].Value.ToString());
            }
            // discounteddiarate = Diarate - FixedDiaDiscRate;
            // dgv.Columns["DiscountedDiaRate"].ReadOnly = true;
            //dgv.CurrentRow.Cells["DiscountedDiaRate"].Value = discounteddiarate.ToString();
            // dgv.CurrentRow.Cells["DiaCash"].Value = (Diarate * DiaWt).ToString("F2");

        }
        private void enable_disable(Gramboo.Controls.GrbDataGridView dgv, int row, bool ReadOnly)
        {
            SetEnable(dgv, "Touch", row, ReadOnly);
            SetEnable(dgv, "Qty", row, ReadOnly);
            //SetEnable(dgv, "Gwt", row, ReadOnly);
            SetEnable(dgv, "MetalRate", row, ReadOnly);
            //SetEnable(dgv, "MC",  row, ReadOnly);
            //SetEnable(dgv, "Wastage",  row, ReadOnly);
            //SetEnable(dgv, "VA",  row, ReadOnly);
            //SetEnable(dgv, "VAPerc",  row, ReadOnly); 

        }

        private void SetEnable(Gramboo.Controls.GrbDataGridView dgv, string Column, int row, bool ReadOnly)
        {
            if (dgv.Columns.Contains(Column))
                dgv.Rows[row].Cells[Column].ReadOnly = ReadOnly;
        }

        private void SetComboLocation(Gramboo.Controls.GrbDataGridView dgv, Gramboo.Controls.GrbComboBox cmb, int columnindex, int rowindex, bool dropdown = true)
        {

            int SaleCurrentRow = rowindex;
            cmb.Parent = dgv.Parent;
            cmb.Visible = true;
            cmb.Text = dgv.SelectedCells[0].Value.ToString();
            System.Drawing.Point p = new System.Drawing.Point();
            p = dgv.GetCellDisplayRectangle(dgv.SelectedCells[0].ColumnIndex, dgv.SelectedCells[0].RowIndex, true).Location;
            cmb.Location = new System.Drawing.Point(p.X + dgv.Parent.Location.X - 2, p.Y);

            //cmb.Location = new System.Drawing.Point(p.X + dgv.Parent.Location.X - 2, p.Y + dgv.Parent.Location.Y - 2);
            cmb.Size = dgv.GetCellDisplayRectangle(dgv.SelectedCells[0].ColumnIndex, dgv.SelectedCells[0].RowIndex, true).Size;
            cmb.BringToFront();
            cmb.Focus();

            //cmb.DroppedDown = dropdown;

        }
        private void WastageCash(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double Wst = 0, MetalRate = 0, WastageCash = 0;
            if (dgv.CurrentRow == null)
                return;
            if (dgv.CurrentRow.Cells["Wastage"].Value.ToString() != "")
            {
                Wst = Convert.ToDouble(dgv.CurrentRow.Cells["Wastage"].Value.ToString());

            }

            if (dgv.CurrentRow.Cells["MetalRate"].Value.ToString() != "")
            {
                MetalRate = Convert.ToDouble(dgv.CurrentRow.Cells["MetalRate"].Value.ToString());

            }
            WastageCash = Wst * MetalRate;
            dgv.CurrentRow.Cells["WastageCash"].Value = WastageCash.ToString();
        }

        private void VA(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double MC = 0, VA = 0, WastageCash = 0;
            if (dgv.CurrentRow == null)
                return;
            if (dgv.CurrentRow.Cells["MC"].Value.ToString() != "")
            {
                MC = Convert.ToDouble(dgv.CurrentRow.Cells["MC"].Value.ToString());

            }

            if (dgv.CurrentRow.Cells["WastageCash"].Value.ToString() != "")
            {
                WastageCash = Convert.ToDouble(dgv.CurrentRow.Cells["WastageCash"].Value.ToString());

            }
            VA = WastageCash + MC;
            dgv.CurrentRow.Cells["VA"].Value = VA.ToString();
        }
        private void StoneCtwt(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double Stwt = 0.000, StwtCt = 0.000;
            if (dgv.CurrentRow == null)
                return;
            if (dgv.CurrentRow.Cells["StoneWt"].Value.ToString() != "")
            {
                Stwt = Convert.ToDouble(dgv.CurrentRow.Cells["StoneWt"].Value.ToString());
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

            dgv.CurrentRow.Cells["StoneWt"].Value = Math.Round(Stwt, 3).ToString("f3");
        }
        public void VAPerc(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double VA = 0, MetalCash = 0, Vaperc = 0;
            if (dgv.CurrentRow.Cells["VA"].Value.ToString() != "")
            {
                VA = Convert.ToDouble(dgv.CurrentRow.Cells["VA"].Value.ToString());
            }
            if (dgv.CurrentRow.Cells["MetalCash"].Value.ToString() != "")
            {
                MetalCash = Convert.ToDouble(dgv.CurrentRow.Cells["MetalCash"].Value.ToString());
            }
            if (MetalCash != 0)
            {
                Vaperc = VA / MetalCash * 100;
                dgv.CurrentRow.Cells["VAPerc"].Value = Vaperc.ToString();
            }


        }

        private void MetalCash(Gramboo.Controls.GrbDataGridView dgv, bool IsMetalRate)
        {

            Double metalrate = 0, NetWt = 0, Touch = 0, metalcash = 0, MetalId = 0, Nos = 0;
            string CalcOn = "";
            if (dgv.CurrentRow == null)
                return;
            //foreach (DataGridViewRow r in dgv.Rows)
            //{
            if (dgv.CurrentRow.Cells["ItemId"].Value.ToString() != "" && dgv.CurrentRow.Cells["Gwt"].Value.ToString() != "")
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                ("SELECT MetalId,ISNULL([Purity Value],0) as [Purity Value] ,[Calculated On] FROM ITM.VItemMaster WHERE ItemId =" + dgv.CurrentRow.Cells["ItemId"].Value + "")).Tables[0])
                {

                    if (dt.Rows.Count > 0)
                    {

                        MetalId = Convert.ToDouble(dt.Rows[0]["MetalId"].ToString());
                        Touch = Convert.ToDouble(dt.Rows[0]["Purity Value"].ToString());
                        CalcOn = (dt.Rows[0]["Calculated On"].ToString());
                    }

                    else
                    {
                        return;
                    }


                }


            }
            else
            {
                return;
            }

            //}

            if (dgv.CurrentRow.Cells["MetalRate"].Value.ToString() != "" && CalcOn == "Nos" || IsMetalRate)
            {
                metalrate = Convert.ToDouble(dgv.CurrentRow.Cells["MetalRate"].Value.ToString());

            }
            else if (CalcOn == "Gwt" || CalcOn == "NetWt")
            {
                if (dgv.CurrentRow.Cells["MetalRate"].Value.ToString() == "")
                {
                    if (cmbMetalType.SelectedValue.ToString() == "S")
                    {
                        metalrate = Convert.ToDouble((DBConn.GetData(new SqlCommand("SELECT Top 1 SilverRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["SilverRate"]).ToString());

                    }
                    else if (cmbMetalType.SelectedValue.ToString() == "P")
                    {

                        metalrate = Convert.ToDouble((DBConn.GetData(new SqlCommand("SELECT Top 1 PlatinumRate  from GEN.MetalRate   WHERE EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["PlatinumRate"]).ToString());
                    }
                    else
                    {
                        metalrate = Convert.ToDouble((DBConn.GetData(new SqlCommand("SELECT Top 1 floor(BoardRate*" + (Touch < 92 ? 18 : 22) + "/22)  as BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString());

                    }


                    dgv.CurrentRow.Cells["MetalRate"].Value = metalrate;
                }
                else
                {
                    metalrate = Convert.ToDouble(dgv.CurrentRow.Cells["MetalRate"].Value.ToString());
                }
                // dgv.CurrentRow.Cells["MetalRate"].ReadOnly = true;
            }

            else
            {
                return;
            }
            if (dgv.CurrentRow.Cells["NetWt"].Value.ToString() != "")
            {
                NetWt = Convert.ToDouble(dgv.CurrentRow.Cells["NetWt"].Value.ToString());

            }
            if (dgv.CurrentRow.Cells["Qty"].Value.ToString() != "")
            {
                Nos = Convert.ToDouble(dgv.CurrentRow.Cells["Qty"].Value.ToString());
            }

            if (dgv.CurrentRow.Cells["Touch"].Value.ToString() != "" && dgv.CurrentRow.Cells["Touch"].Value.ToString() != "0")
            {
                Touch = Convert.ToDouble(dgv.CurrentRow.Cells["Touch"].Value.ToString());
            }

            else
            {
                dgv.CurrentRow.Cells["Touch"].Value = Touch;
            }

            //if (MetalId == 1 || MetalId == 4)
            //{
            //    Wt916 = NetWt * Touch / Convert.ToDouble(92);
            //    metalcash = Wt916 * metalrate;
            //}

            //else
            //{
            if (CalcOn == "Gwt" || CalcOn == "NetWt")
            {
                metalcash = NetWt * metalrate;

            }
            else
            {
                metalcash = Nos * metalrate;
            }

            dgv.CurrentRow.Cells["MetalCash"].Value = metalcash.ToString();
            //ratediff
        }

        private void AfterComboLeave(Gramboo.Controls.GrbDataGridView dgv, Gramboo.Controls.GrbComboBox cmb, int valuecolumnindex = -1)
        {

            if (flag || dgv.CurrentCell == null)
                return;


            cmb.Visible = false;
            if (valuecolumnindex >= 0 && (cmb.SelectedValue == null || cmb.Text == ""))
            {
                dgv.CurrentCell.Value = "";
                dgv.Rows[dgv.CurrentCell.RowIndex].Cells[valuecolumnindex].Value = 0;
                return;
            }
            else if (dgv.CurrentCell == null)
                return;

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
            dgv.Focus();

        }
        private void ComboKeydown(Gramboo.Controls.GrbDataGridView dgv, Gramboo.Controls.GrbComboBox cmb, KeyEventArgs e)
        {

            if (flag || editflag)
                return;

            if (e.KeyValue == 13)
            {

                dgv.Focus();
                // SendKeys.Send("{Enter}");

                cmb.Visible = false;
            }

        }
        private void TotalAmount(Gramboo.Controls.GrbDataGridView dgv)
        {

            Double Diacash = 0, Stcash = 0, GoldCash = 0, TotalCash = 0, VA = 0, MC = 0, WastCash = 0;

            if (dgv.CurrentRow == null)
                return;
            if (dgv.CurrentRow.Cells["MetalCash"].Value.ToString() != "")
            {
                GoldCash = Convert.ToDouble(dgv.CurrentRow.Cells["MetalCash"].Value.ToString());

            }

            if (dgv.CurrentRow.Cells["StoneCash"].Value.ToString() != "")
            {
                Stcash = Convert.ToDouble(dgv.CurrentRow.Cells["StoneCash"].Value.ToString());

            }
            if (dgv.CurrentRow.Cells["DiaCash"].Value.ToString() != "")
            {
                Diacash = Convert.ToDouble(dgv.CurrentRow.Cells["DiaCash"].Value.ToString());

            }
            if (dgv.CurrentRow.Cells["VA"].Value.ToString() != "")
            {
                VA = Convert.ToDouble(dgv.CurrentRow.Cells["VA"].Value.ToString());

            }
            if (dgv.CurrentRow.Cells["MC"].Value.ToString() != "")
            {
                MC = Convert.ToDouble(dgv.CurrentRow.Cells["MC"].Value.ToString());

            }
            if (dgv.CurrentRow.Cells["WastageCash"].Value.ToString() != "")
            {
                WastCash = Convert.ToDouble(dgv.CurrentRow.Cells["WastageCash"].Value.ToString());

            }
            TotalCash = GoldCash + Stcash + Diacash + VA;

            dgv.CurrentRow.Cells["Total"].Value = TotalCash.ToString();
            // UpdateTax();
        }

        //VA Amount
        private void VAamount(Gramboo.Controls.GrbDataGridView dgv)
        {
            Double VAperc = 0, MetalCash = 0, StoneCash = 0, DiaCash = 0, totCash = 0, VA;

            if (dgv.CurrentRow.Cells["DiaCash"].Value.ToString() != "")
            {
                DiaCash = Convert.ToDouble(dgv.CurrentRow.Cells["DiaCash"].Value.ToString());

            }
            if (dgv.CurrentRow.Cells["StoneCash"].Value.ToString() != "")
            {
                StoneCash = Convert.ToDouble(dgv.CurrentRow.Cells["StoneCash"].Value.ToString());

            }
            if (dgv.CurrentRow.Cells["MetalCash"].Value.ToString() != "")
            {
                MetalCash = Convert.ToDouble(dgv.CurrentRow.Cells["MetalCash"].Value.ToString());

            }
            if (dgv.CurrentRow.Cells["VAPerc"].Value.ToString() != "")
            {
                VAperc = Convert.ToDouble(dgv.CurrentRow.Cells["VAPerc"].Value.ToString());
            }
            //totCash = DiaCash + MetalCash + StoneCash;
            totCash = MetalCash;
            VA = totCash * VAperc / 100;
            dgv.CurrentRow.Cells["VA"].Value = VA.ToString();

        }






        private void FrmSalesReturnEntry_Load(object sender, EventArgs e)
        {
           dtp_InvDate.MaxDate=DateTime.Today;
           txt_billNo.Focus();
        }

        private void dgv_SalesReturn_SelectionChanged(object sender, EventArgs e)
        {

            if (dgv_SalesReturn.SelectedCells.Count > 0)
            {
                //if (dgv_SalesReturn.SelectedCells[0].OwningColumn.DataPropertyName == "ProdCode")
                //{
                //    SetComboLocation(dgv_SalesReturn, Cmb_ProdCode_Return, dgv_SalesReturn.SelectedCells[0].ColumnIndex, dgv_SalesReturn.SelectedCells[0].RowIndex, false);
                //}
                if (dgv_SalesReturn.SelectedCells[0].OwningColumn.DataPropertyName == "Code")
                {
                    try
                    {

                        // Cmb_ProdCode_SDtl.Parent = dgv_GRN;
                        txtitemcode.Visible = true;
                        txtitemcode.Text = dgv_SalesReturn.SelectedCells[0].Value.ToString();

                        System.Drawing.Point p = new System.Drawing.Point();
                        p = dgv_SalesReturn.GetCellDisplayRectangle(dgv_SalesReturn.SelectedCells[0].ColumnIndex, dgv_SalesReturn.SelectedCells[0].RowIndex, true).Location;
                        dgv_SalesReturn.Location = new System.Drawing.Point(p.X + dgv_SalesReturn.Parent.Location.X, p.Y + dgv_SalesReturn.Parent.Location.Y);


                        txtitemcode.Size = dgv_SalesReturn.GetCellDisplayRectangle(dgv_SalesReturn.SelectedCells[0].ColumnIndex, dgv_SalesReturn.SelectedCells[0].RowIndex, true).Size;
                        txtitemcode.BringToFront();
                        txtitemcode.Focus();

                    }

                    catch { }
                }
                if (dgv_SalesReturn.SelectedCells[0].OwningColumn.DataPropertyName == "ItemName")
                {
                    SetComboLocation(dgv_SalesReturn, Cmb_ItemName_Return, dgv_SalesReturn.SelectedCells[0].ColumnIndex, dgv_SalesReturn.SelectedCells[0].RowIndex);
                }
                else if (dgv_SalesReturn.SelectedCells[0].OwningColumn.DataPropertyName == "Model")
                {
                    SetComboLocation(dgv_SalesReturn, Cmb_ModelName_Return, dgv_SalesReturn.SelectedCells[0].ColumnIndex, dgv_SalesReturn.SelectedCells[0].RowIndex);
                }
                else if (dgv_SalesReturn.SelectedCells[0].OwningColumn.DataPropertyName == "PurityName")
                {
                    SetComboLocation(dgv_SalesReturn, cmb_purity_Return, dgv_SalesReturn.SelectedCells[0].ColumnIndex, dgv_SalesReturn.SelectedCells[0].RowIndex);
                }
                else if (dgv_SalesReturn.SelectedCells[0].OwningColumn.DataPropertyName == "FloorName")
                {
                    SetComboLocation(dgv_SalesReturn, cmb_Floorsale_Return, dgv_SalesReturn.SelectedCells[0].ColumnIndex, dgv_SalesReturn.SelectedCells[0].RowIndex);
                    //cmb_FloorName.Text = "G";
                }
                else if (dgv_SalesReturn.SelectedCells[0].OwningColumn.DataPropertyName == "Type")
                {
                    SetComboLocation(dgv_SalesReturn, CmbType_Return, dgv_SalesReturn.SelectedCells[0].ColumnIndex, dgv_SalesReturn.SelectedCells[0].RowIndex);
                }
                //else if (dgv_SalesReturn.SelectedCells[0].OwningColumn.DataPropertyName == "SubItemName")
                //{
                //    SetComboLocation(dgv_SalesReturn, cmbsubitemreturn, dgv_SalesReturn.SelectedCells[0].ColumnIndex, dgv_SalesReturn.SelectedCells[0].RowIndex);
                //}
            }
        }
        private void calcmcwst(DataGridViewRow r)
        {
            if (cmbMetalType.SelectedValue.ToString() == "S" == false)
            {
                Double netwt = 0, mcperc = 0, wstperc = 0, mc = 0, wst = 0, metalcash = 0, wstcash = 0, rate = 0;
                long model = 0;

                netwt = (r.Cells["NetWt"].Value.ToString() == "" ? 0 : Convert.ToDouble(r.Cells["NetWt"].Value.ToString()));
                model = (r.Cells["ModelId"].Value.ToString() == "" ? 0 : Convert.ToInt64(r.Cells["ModelId"].Value.ToString()));
                metalcash = (r.Cells["MetalCash"].Value.ToString() == "" ? 0 : Convert.ToDouble(r.Cells["MetalCash"].Value.ToString()));
                rate = (r.Cells["MetalRate"].Value.ToString() == "" ? 0 : Convert.ToDouble(r.Cells["MetalRate"].Value.ToString()));

                if (netwt > 0 && model != 0)
                {
                    using (DataTable dt = DBConn.GetData(new SqlCommand("select MC,Wst FROM sale.MinimumVAMaster WHERE modelid=" + model.ToString())).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            mcperc = Convert.ToDouble(dt.Rows[0]["MC"].ToString());
                            wstperc = Convert.ToDouble(dt.Rows[0]["Wst"].ToString());


                            mc = metalcash * mcperc / 100;
                            wst = netwt * wstperc / 100;
                            wstcash = wst * rate;
                            r.Cells["Wastage"].Value = wst.ToString("f2");
                            r.Cells["MC"].Value = mc.ToString("f2");
                            r.Cells["VAPerc"].Value = (mcperc + wstperc).ToString("f2");
                            r.Cells["VA"].Value = (wstcash + mc).ToString("f2");

                        }
                    }


                }

            }
        }

        //private void FillRowWithProdcode(Gramboo.Controls.GrbDataGridView dgv, int rowindex, string type, Int64 floorid, Int64 prodcodeid, bool Isreceipt)
        //{


        //    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
        //                ("SELECT * FROM SALE.AddToSalesDetails( " + prodcodeid + ",'" + dtp_InvDate.Value.Date.ToString("dd-MMM-yyyy") + "'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            DataRow r = ((System.Data.DataTable)dgv.DataSource).Rows[rowindex];
        //            flag = true;
        //            if (dgv.Rows.Count > rowindex)
        //            {
        //                dgv_SalesReturn.SummaryRowVisible = false;
        //                foreach (DataGridViewColumn c in dgv.Columns)
        //                {
        //                    if (c.IsDataBound)
        //                    {
        //                        r[c.DataPropertyName] = dt.Rows[0][c.DataPropertyName];

        //                    }
        //                }

        //            }

        //            r.AcceptChanges();
        //            dgv.EndEdit();
        //            dgv_SalesReturn.SummaryRowVisible = true;
        //            dgv_SalesReturn.RefreshSummary();
        //            //dgv_SalesReturn.CurrentRow.Cells["Stoneless"].Value = true;
        //            flag = false;
        //            calcmcwst(dgv.CurrentRow);

        //        }
        //    }
        //}

        private void AdjustSalesItemsColumns(Gramboo.Controls.GrbDataGridView dgv)
        {
            if (cmbMetalType.SelectedValue == null)
                return;

            dgv.RowHeadersVisible = false;

            SetColumnWidth(dgv, 0, 40);
            SetColumnWidth(dgv, "SlNo", 30);
            SetColumnWidth(dgv, "FloorName", cmb_Floorsale_Return.Width);
            SetColumnWidth(dgv, "ItemName", Cmb_ItemName_Return.Width);
            SetColumnWidth(dgv, "PurityName", cmb_purity_Return.Width);

            SetColumnWidth(dgv, "Model", 45);
            SetColumnWidth(dgv, "Touch", 50);
            SetColumnWidth(dgv, "Qty", 40);
            SetColumnWidth(dgv, "Gwt", 75);
            SetColumnWidth(dgv, "MetalRate", 70);
            SetColumnWidth(dgv, "MetalCash", 80);
            SetColumnWidth(dgv, "DiaNo", 50);
            SetColumnWidth(dgv, "DiaWt", 50);
            SetColumnWidth(dgv, "DiaCash", 70);
            SetColumnWidth(dgv, "StoneWt", 70);
            SetColumnWidth(dgv, "StoneCash", 70);
            SetColumnWidth(dgv, "MC", 70);
            SetColumnWidth(dgv, "Wastage", 60);
            SetColumnWidth(dgv, "WastageCash", 70);
            SetColumnWidth(dgv, "VA", 70);
            SetColumnWidth(dgv, "VAPerc", 70);
            SetColumnWidth(dgv, "NetWt", 50);
            SetColumnWidth(dgv, "TotalAmount", 100);

            dgv.Columns["NetWt"].ReadOnly = true;
            dgv.Columns["Total"].ReadOnly = true;
            SetColumnWidth(dgv, "StoneLess", 80);
            dgv.Columns["Prodcode"].ReadOnly = true;
            // dgv.Columns["DiaCash"].ReadOnly = true;
            //dgv.Columns["FloorName"].ReadOnly = true;
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



        private void AdjustSalesReturnItemsColumns(Gramboo.Controls.GrbDataGridView dgv)
        {
            AdjustSalesItemsColumns(dgv);
        }
        private void Cmb_ProdCode_Return_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_SalesReturn, Cmb_ProdCode_Return, (dgv_SalesReturn.Columns.Count > 0 ? dgv_SalesReturn.Columns["ProdcodeId"].Index : -1));
        }


        private void Cmb_ItemName_Return_Leave(object sender, EventArgs e)
        {
            //if (dgv_SalesReturn.CurrentRow != null)
            //MetalRate(dgv_SalesReturn.CurrentRow);
            AfterComboLeave(dgv_SalesReturn, Cmb_ItemName_Return, (dgv_SalesReturn.Columns.Count > 0 ? dgv_SalesReturn.Columns["ItemId"].Index : -1));
        }

        private void cmb_purity_Return_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_SalesReturn, cmb_purity_Return, (dgv_SalesReturn.Columns.Count > 0 ? dgv_SalesReturn.Columns["PurityId"].Index : -1));
        }

        private void cmb_Floorsale_Return_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_SalesReturn, cmb_Floorsale_Return, (dgv_SalesReturn.Columns.Count > 0 ? dgv_SalesReturn.Columns["FloorId"].Index : -1));
        }
        private void Cmb_ModelName_Return_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_SalesReturn, Cmb_ModelName_Return, (dgv_SalesReturn.Columns.Count > 0 ? dgv_SalesReturn.Columns["ModelId"].Index : -1));
        }

        private void CmbType_Return_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_SalesReturn, CmbType_Return);
        }
        private void Cmb_ItemName_Return_KeyDown(object sender, KeyEventArgs e)
        {
            // ComboKeydown(dgv_SalesReturn, Cmb_ItemName, e);
        }
        private void dgv_SalesReturn_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


            if (txtcompId.Text == "" || e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (flag || dgv_SalesReturn.CurrentCell == null)
                return;

            if (e.ColumnIndex == dgv_SalesReturn.Columns["ProdCodeID"].Index)
            {

                if (dgv_SalesReturn[e.ColumnIndex, e.RowIndex].Value != null && dgv_SalesReturn[e.ColumnIndex, e.RowIndex].Value.ToString() != "0")
                {
                    flag = true;
                    // FillRowWithProdcode(dgv_SalesReturn, e.RowIndex, dgv_SalesReturn.Rows[e.RowIndex].Cells["Type"].Value.ToString(), 0, (Int64)dgv_SalesReturn.Rows[e.RowIndex].Cells["ProdcodeId"].Value, false);
                    enable_disable(dgv_SalesReturn, e.RowIndex, true);
                    if (dgv_SalesReturn.Rows.Count - 1 == e.RowIndex)
                        ((System.Data.DataTable)dgv_SalesReturn.DataSource).Rows.Add(((System.Data.DataTable)dgv_SalesReturn.DataSource).NewRow());
                    flag = false;
                    //dgv_SalesReturn.BeginEdit(true);
                    dgv_SalesReturn.CurrentCell = dgv_SalesReturn.Rows[e.RowIndex + 1].Cells["Col_AutoSlno"];
                    //SetComboLocation(dgv_SalesReturn, Cmb_ProdCode_SDtl, dgv_SalesReturn.SelectedCells[0].ColumnIndex, dgv_SalesReturn.SelectedCells[0].RowIndex);

                }
                else
                {
                    enable_disable(dgv_SalesReturn, e.RowIndex, false);
                }

            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
              dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "DiaWt" ||
              dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "StoneWt")
            {              
                if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "StoneWt")
                {



                    if (Convert.ToDouble((dgv_SalesReturn.CurrentRow.Cells["StoneWt"].Value == DBNull.Value ? "0" : dgv_SalesReturn.CurrentRow.Cells["StoneWt"].Value)) == 0)
                    {
                        //dgv_SalesReturn.CurrentRow.Cells["StoneCash"].ReadOnly = true;
                    }
                    else
                    {
                        //dgv_SalesReturn.CurrentRow.Cells["StoneCash"].ReadOnly = false;
                    }
                }
                fillDiscountedDiaRate(dgv_SalesReturn);
                // Calculatediadiscount(dgv_SalesReturn);
                CalculateWt(dgv_SalesReturn);
            }         
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "DiaCash" ||
                dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "StoneCash" ||
                dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "MetalCash" ||
                     dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "VA")
            {
                TotalAmount(dgv_SalesReturn);
            }

            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "ItemId"
                || dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "NetWt"
                 || dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "Qty"
                )
            {
                MetalCash(dgv_SalesReturn, false);
            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "MetalRate" && dgv_SalesReturn.Columns[e.ColumnIndex].ReadOnly == false)
            {
                MetalCash(dgv_SalesReturn, true);
            }
            else if (
                dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "Touch" ||
                dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "Wastage")
            {
                MetalCash(dgv_SalesReturn, false);
                WastageCash(dgv_SalesReturn);
            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "WastageCash" ||  dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "VAPerc" ||
                     dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "MC")
            {
                VA(dgv_SalesReturn);
                VAPerc(dgv_SalesReturn);

            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "DiaRate")
            {
                fillDiscountedDiaRate(dgv_SalesReturn);
                //Calculatediadiscount(dgv_SalesReturn);
            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "Stoneless")
            {
                CalculateWt(dgv_SalesReturn);
            }
        }
     
        private void privilagecard()
        {
            
           
        }
   
        //private void showlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    if (custpanel.Visible == false)
        //    {
        //        custpanel.Parent = this;
        //        custpanel.Visible = true;
        //        custpanel.BringToFront();
        //        custpanel.Show();
        //        custpanel.BringToFront();
        //        // panel3.Visible = true;
        //        //txt_CardNo.Visible = true;
        //        //txt_openamount.Visible = true;
        //        //label41.Visible = true;
        //        //label43.Visible = true;
        //        txtCustName.Focus();
        //        txtCustName.Enabled = false;
        //        //txthouse.Enabled = false;
        //        txtAddress1.Enabled = false;
        //        txtAddress2.Enabled = false;
        //        txtPhoneNo.Enabled = false;
        //        custpanel.Location = new Point(75, 150);
        //        custpanel.Size = new Size(this.Width - 200, 400);
        //        txtCustName.Focus();
        //    }

        //    else
        //    {
        //        custpanel.Visible = false;
        //        custpanel.SendToBack();
        //        custpanel.Hide();
        //    }

        //}

        private void FrmSalesReturnEntry_KeyDown(object sender, KeyEventArgs e)
        {
 
             

            if (e.KeyCode == Keys.Escape)
            {
                if (GrbPending.Visible == true)
                {
                    GrbPending.Visible = false;
                }

                if (GrpPurOtherCharges.Visible == true)
                {
                    GrpPurOtherCharges.Visible = false;
                    linkLbl_OtherCharges.Text = "Add Other Charges";
                }
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Space)
            {
                txtTotalReturnAmount.Focus();
            }
        }

        private void showlink_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {          
            //if (SD == null || SD.IsDisposed)
            //{
            //    SAFA.Forms.COM.CustomerSearch SD1 = new SAFA.Forms.COM.CustomerSearch();
            //    SD1.MdiParent = this.ParentForm;
            //    SD1.target = "CRM";
            //    SD1.Mode = grb_salesmode.Value;
            //    SD1.CellButtonClick += new SAFA.Forms.COM.CustomerSearch.CellClickedEventHandler(CellButtonClick);
            //    SD = SD1;
            //    SD1.Show();
            //    SD1.TopMost = true;
            //    SD1.Focus();
            //    SD1.BringToFront();
            //    SD1.TopMost = false;

            //    Cmb_CustomerName.Enabled = false;
            //    txtAddress1.Enabled = false;
            //    txtAddress2.Enabled = false;
            //    txtPhoneNo.Enabled = false;
            //}
            //else
            //{
            //    SD.MdiParent = this.MdiParent;
            //    SD.target = "CRM";
            //    SD.Mode = grb_salesmode.Value;
            //    SD.BringToFront();
            //}
        }
        //public void CellButtonClick(object sender, SAFA.Forms.COM.CustomerSearch.CellClickedEventArgs e)
        //{
        //    txtCustId.Text = e.TargetID;           
        //    //txtHouseName.Text = ((SAFA.Forms.COM.CustomerSearch)sender).HouseName;
        //    //TxtAddress1.Text = ((SAFA.Forms.COM.CustomerSearch)sender).Address1;
        //    //TxtAddress2.Text = ((SAFA.Forms.COM.CustomerSearch)sender).Address2 ;
        //    //txtPhoneNo.Text = ((SAFA.Forms.COM.CustomerSearch)sender).Mob;
        //}
       
        private void chk_newCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_newCustomer.Checked == true)
            {
                showlink.Visible = false;
                Cmb_CustomerName.Focus();
                //if(txtCustName.IndexOf(.,1)
                //{
                //}


                //txt_openamount.Enabled = true;
                Cmb_CustomerName.Enabled = true;
                Cmb_CustomerName.Focus();

                //txthouse.Enabled = true;
                txtAddress1.Enabled = true;
                txtAddress2.Enabled = true;
                txtPhoneNo.Enabled = true;
                cmb_statename.Enabled = true;
                txtpan.Enabled = true;
                Cmb_CustomerName.Text = "";
                txtCustId.Text = "0";
                //txthouse.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtPhoneNo.Text = "";

                //panel3.Visible = true;
                //txt_CardNo.Text = "";
                //txt_openamount.Text = "";
                //panel3.Visible = true;
                //panel1_previlagecard.Visible = true;
                //label41.Visible = true;
                // label43.Visible = true;
                //txt_CardNo.Visible = true;
                //// txt_openamount.Visible = true;
                //label44.Visible = true;
                //label45.Visible = true;
                //label46.Visible = true;
                //txt_bonusAmount.Visible = true;
                //Txt_totalBonus.Visible = true;
                //txt_Redeemamt.Visible = true;               
            }
            else
            {
                showlink.Visible = true;
                //txt_openamount.Enabled = false;
                Cmb_CustomerName.Enabled = false;
                //txthouse.Enabled = false;
                txtAddress1.Enabled = false;
                txtAddress2.Enabled = false;
                txtPhoneNo.Enabled = false;
                cmb_statename.Enabled = false;
                txtpan.Enabled = false;
                // panel3.Visible = false;
                //panel1_previlagecard.Visible = false;
                //txt_openamount = false;
                // label43.Visible = false;
                //txt_CardNo.Visible = false;
                // txt_openamount.Visible = false;
                //label44.Visible = false;
                //label45.Visible = false;
                //label46.Visible = false;
                //txt_bonusAmount.Visible = false;
                //Txt_totalBonus.Visible = false;
                //txt_Redeemamt.Visible = false;
                showlink.Focus();

            }

        }
        public void vouchertype()
        {
            if (salesmode == "W")
            {
                grb_WholeSale.Checked = true;
            }
            else
            {
                grb_Retail.Checked = true;
            }         
              if (grb_Retail.Checked == true)
               {

                   if (cmbMetalType.SelectedValue != null)
                   {
                       if (cmbMetalType.SelectedValue.ToString() == "G")
                           cmb_VoucherTypeId.SelectedValue = 87;
                       else if (cmbMetalType.SelectedValue.ToString() == "D")
                           cmb_VoucherTypeId.SelectedValue = 88;
                       else if (cmbMetalType.SelectedValue.ToString() == "PR")
                           cmb_VoucherTypeId.SelectedValue = 89;
                       else if (cmbMetalType.SelectedValue.ToString() == "UN")
                           cmb_VoucherTypeId.SelectedValue = 90;
                       else if (cmbMetalType.SelectedValue.ToString() == "P")
                           cmb_VoucherTypeId.SelectedValue = 91;
                       else if (cmbMetalType.SelectedValue.ToString() == "S")
                           cmb_VoucherTypeId.SelectedValue = 92;
                       else if (cmbMetalType.SelectedValue.ToString() == "O")
                           cmb_VoucherTypeId.SelectedValue = 127;
                       else
                           cmb_VoucherTypeId.SelectedValue = 87;
                   }
                   else
                   {

                       cmb_VoucherTypeId.SelectedValue = 87;
                   }
               }
               else
               {
                   if (cmbMetalType.SelectedValue != null)
                   {
                       if (cmbMetalType.SelectedValue.ToString() == "G")
                           cmb_VoucherTypeId.SelectedValue = 117;
                       else if (cmbMetalType.SelectedValue.ToString() == "D")
                           cmb_VoucherTypeId.SelectedValue = 118;
                       else if (cmbMetalType.SelectedValue.ToString() == "PR")
                           cmb_VoucherTypeId.SelectedValue = 119;
                       else if (cmbMetalType.SelectedValue.ToString() == "UN")
                           cmb_VoucherTypeId.SelectedValue = 120;
                       else if (cmbMetalType.SelectedValue.ToString() == "P")
                           cmb_VoucherTypeId.SelectedValue = 121;
                       else if (cmbMetalType.SelectedValue.ToString() == "S")
                           cmb_VoucherTypeId.SelectedValue = 122;
                       else
                           cmb_VoucherTypeId.SelectedValue = 117;

                   }
                   else
                   {

                       cmb_VoucherTypeId.SelectedValue = 117;
                   }
               } 
        }
        public void ItemTypeChanged(object sender, EventArgs e)
        {
            if (cmbMetalType.SelectedValue == null)
                return;

            SetColumnsReturn(dgv_SalesReturn);
            if (flag || editflag)
                return;
            string Viewname;
            dgv_SalesReturn.DataSource = null;

            dgv_SalesReturn.SummaryRowVisible = true;
            dgv_SalesReturn.Fill(new Table(SAFA.Classes.Common.DbName, "SALE", "vsalesReturnSetail", true), "1=2");
            dgv_SalesReturn.RefreshSummary(true);
            dgv_SalesReturn.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            dgv_SalesReturn.AllowUserToAddRows = false;

            dgv_SalesReturn.Columns["StoneCash"].ReadOnly = false;
            //dgv.Columns["MetalRate"].ReadOnly = true;

            if (cmbMetalType.SelectedValue.ToString() == "S")
            {
                lblType.Text = "Silver";
                //dgv.Columns["MetalRate"].ReadOnly = false;


            }
            if (cmbMetalType.SelectedValue.ToString() == "D" || cmbMetalType.SelectedValue.ToString() == "UN" || cmbMetalType.SelectedValue.ToString() == "PR")
            {
                lblType.Text = "Diamond";
            }
            if (cmbMetalType.SelectedValue.ToString() == "P")
            {

                lblType.Text = "Platinum";
                //dgv.Columns["MetalRate"].ReadOnly = false;

            }
            if (cmbMetalType.SelectedValue.ToString() == "W")
            {

                lblType.Text = "Watch";
                //dgv.Columns["MetalRate"].ReadOnly = false;


            }
            if (cmbMetalType.SelectedValue.ToString() == "G")
            {
                lblType.Text = "Gold";

            }
            if (salesmode == "W")
            {
                grb_WholeSale.Checked = true;
            }
            else
            {
                grb_Retail.Checked = true;
            }
            vouchertype();
            if (dgv_SalesReturn.Rows.Count == 0)
            {
                ((System.Data.DataTable)dgv_SalesReturn.DataSource).Rows.Add(((System.Data.DataTable)dgv_SalesReturn.DataSource).NewRow());
            }
            dgv_SalesReturn.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv_SalesReturn.BeginEdit(false);

            if (cmbMetalType.SelectedValue.ToString() == "D" || cmbMetalType.SelectedValue.ToString() == "UN" || cmbMetalType.SelectedValue.ToString() == "PR")
            {
                Viewname = "ITM.VDiamondOrnaments";
               // cmb_VoucherTypeId.SelectedValue = 67;
            }


            else if (cmbMetalType.SelectedValue.ToString() == "P")
            {
                Viewname = "ITM.VPlatinumOrnaments";
               // cmb_VoucherTypeId.SelectedValue = 67;
            }


            else if (cmbMetalType.SelectedValue.ToString() == "S")
            {
                Viewname = "ITM.VSilverOrnaments";
               // cmb_VoucherTypeId.SelectedValue = 67;
            }


            else
            {
                Viewname = "ITM.VGoldOrnaments";
               // cmb_VoucherTypeId.SelectedValue = 67;
            }
            SetRates();

            if (!IsEditMode && txtcompId.Text != ""  )
                txtInvNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)(cmb_VoucherTypeId.SelectedValue == null ? 0 : cmb_VoucherTypeId.SelectedValue), dtp_InvDate.Value,
                DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            // Gramboo.General.Setupcombo(Cmb_ItemName, Viewname, "[Item Name]", "ItemId", "[Is Active]='True' ");
            Gramboo.General.Setupcombo(Cmb_ItemName_Return, Viewname, "[Item Name]", "ItemId", "[Is Active]='True' ");
            AdjustSalesReturnItemsColumns(dgv_SalesReturn);
            SetRates();
        }

        private void Cmb_ItemName_Return_Leave_1(object sender, EventArgs e)
        {
            if (dgv_SalesReturn.CurrentRow != null)
                MetalRate(dgv_SalesReturn.CurrentRow);
            AfterComboLeave(dgv_SalesReturn, Cmb_ItemName_Return, (dgv_SalesReturn.Columns.Count > 0 ? dgv_SalesReturn.Columns["ItemId"].Index : -1));

        }

        private void Cmb_ModelName_Return_SelectedIndexChanged(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_SalesReturn, Cmb_ModelName_Return, (dgv_SalesReturn.Columns.Count > 0 ? dgv_SalesReturn.Columns["ModelId"].Index : -1));
        }

        private void Cmb_ItemName_Return_KeyDown_1(object sender, KeyEventArgs e)
        {
            ComboKeydown(dgv_SalesReturn, Cmb_ItemName_Return, e);
        }
        private void Cmb_ItemName_Return_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

       
        private void Cmb_ItemName_Return_SelectedValueChanged_1(object sender, EventArgs e)
        {
        }

        private void cmbsubitemreturn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
        private void dgv_SalesReturn_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
         
            if (txtcompId.Text == "" || e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (flag || dgv_SalesReturn.CurrentCell == null)
                return;

            //if (e.ColumnIndex == dgv_SalesReturn.Columns["ProdCodeID"].Index)
            //{

            //    if (dgv_SalesReturn[e.ColumnIndex, e.RowIndex].Value != null && dgv_SalesReturn[e.ColumnIndex, e.RowIndex].Value.ToString() != "0")
            //    {
            //        flag = true;
            //        FillRowWithProdcode(dgv_SalesReturn, e.RowIndex, dgv_SalesReturn.Rows[e.RowIndex].Cells["Type"].Value.ToString(), 0, (Int64)dgv_SalesReturn.Rows[e.RowIndex].Cells["ProdcodeId"].Value, false);
            //        enable_disable(dgv_SalesReturn, e.RowIndex, true);
            //        if (dgv_SalesReturn.Rows.Count - 1 == e.RowIndex)
            //            ((System.Data.DataTable)dgv_SalesReturn.DataSource).Rows.Add(((System.Data.DataTable)dgv_SalesReturn.DataSource).NewRow());
            //        flag = false;
            //        //dgv_SalesReturn.BeginEdit(true);
            //        dgv_SalesReturn.CurrentCell = dgv_SalesReturn.Rows[e.RowIndex + 1].Cells["Col_AutoSlno"];
            //        SetComboLocation(dgv_SalesReturn, Cmb_ProdCode_SDtl, dgv_SalesReturn.SelectedCells[0].ColumnIndex, dgv_SalesReturn.SelectedCells[0].RowIndex);

            //    }
            //    else
            //    {
            //        enable_disable(dgv_SalesReturn, e.RowIndex, false);
            //    }

            //}
            //else 
            if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
          dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "DiaWt" ||
          dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "StoneWt")
            {
                if (dgv_SalesReturn.CurrentCell.OwningColumn.Name == "StoneWt")
                {
                    StoneCtwt(dgv_SalesReturn);
                }
                if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "StoneWt")
                {



                    if (Convert.ToDouble((dgv_SalesReturn.CurrentRow.Cells["StoneWt"].Value == DBNull.Value ? "0" : dgv_SalesReturn.CurrentRow.Cells["StoneWt"].Value)) == 0)
                    {
                        //dgv_SalesReturn.CurrentRow.Cells["StoneCash"].ReadOnly = true;
                    }
                    else
                    {
                        //dgv_SalesReturn.CurrentRow.Cells["StoneCash"].ReadOnly = false;
                    }
                }
                fillDiscountedDiaRate(dgv_SalesReturn);
                // Calculatediadiscount(dgv_SalesReturn);
                CalculateWt(dgv_SalesReturn);
            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "StoneCtWt")
            {
                if (dgv_SalesReturn.CurrentCell.OwningColumn.Name == "StoneCtWt")
                {
                    StoneCtwtToWT(dgv_SalesReturn);
                }
            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "DiaCash" ||
                dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "StoneCash" ||
                dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "MetalCash" ||
                     dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "VA")
            {
                TotalAmount(dgv_SalesReturn);
            }

            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "ItemId"
                || dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "NetWt"
                 || dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "Qty"
                )
            {
                MetalCash(dgv_SalesReturn, false);
            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "MetalRate" && dgv_SalesReturn.Columns[e.ColumnIndex].ReadOnly == false)
            {
                MetalCash(dgv_SalesReturn, true);
            }
            else if (
                dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "Touch" ||
                dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "Wastage")
            {
                MetalCash(dgv_SalesReturn, false);
                WastageCash(dgv_SalesReturn);
            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName ==  "VAPerc")
            {
                VAamount(dgv_SalesReturn); 

            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "WastageCash" ||
                     dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "MC")
            {
                VA(dgv_SalesReturn);
                VAPerc(dgv_SalesReturn);

            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "DiaRate")
            {
                fillDiscountedDiaRate(dgv_SalesReturn);
                //Calculatediadiscount(dgv_SalesReturn);
            }
            else if (dgv_SalesReturn.Columns[e.ColumnIndex].DataPropertyName == "Stoneless")
            {
                CalculateWt(dgv_SalesReturn);
            }
        }


       
       
        private void ReturnAmt()
        {
            double totamt, retamt;
            double totaltax, netamt;

            netamt = Convert.ToDouble(txtTotAmount.Text);

            totaltax = (netamt * .03);
            //totamt = Convert.ToDouble(txtTotAmount.Text);
            //if (txtTxType.Text == "1")
            //{

            //    txtFinalAmt2.Text = (totaltax + netamt).ToString("f2");
            //}
            //else
            //{
            //    txtFinalAmt2.Text = netamt.ToString("f2");
            //}
        }


        private void cmb_purity_Return_Leave_1(object sender, EventArgs e)
        {
            AfterComboLeave(dgv_SalesReturn, cmb_purity_Return, (dgv_SalesReturn.Columns.Count > 0 ? dgv_SalesReturn.Columns["PurityId"].Index : -1));
        }

        private void dgv_SalesReturn_SummaryCalculated(object source, EventArgs e)
        {

            if (flag || editflag)
                return;
            
            txtTotAmount.Text = dgv_SalesReturn.SummaryRow.SummaryCells["Total"].Text;
            txt_TotalTotalWt.Text = dgv_SalesReturn.SummaryRow.SummaryCells["Gwt"].Text;
            txt_TotalNetWt.Text = dgv_SalesReturn.SummaryRow.SummaryCells["NetWt"].Text;


            taxamt();
            ReturnAmt();
            differencerate();
            //srnetwt=
        }
        

        void calcautodiscperc()
        {
            if (flag || editflag)
                return;
             if (grb_WholeSale.Checked == false)
                return;            
               Double VaDisPerc = 0;
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                   ("select VADiscountPerc From SALE.SalesMaster where SalesId = " + TxtSalesEntryId.Text + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    VaDisPerc =Convert.ToDouble(dt.Rows[0]["VADiscountPerc"].ToString());
                }
            }
            foreach (DataGridViewRow r in dgv_SalesReturn.Rows)
            {

                double va = 0, disc = 0, metalcash = 0, vaafterdisc = 0;
                if ((r.Cells["VA"].Value == null ? "" : r.Cells["VA"].Value.ToString()) != "" && (r.Cells["MetalCash"].Value == null ? "" : r.Cells["MetalCash"].Value.ToString()) != "")
                {
                    va = Convert.ToDouble(r.Cells["VA"].Value);
                    metalcash = Convert.ToDouble(r.Cells["MetalCash"].Value);
                    disc = VaDisPerc;
                    if (metalcash != 0)
                    {
                        vaafterdisc = ((va - (va * disc / 100)) / metalcash) * 100;
                    }
                    r.Cells["VAPercAfterDisc"].Value = vaafterdisc.ToString("f2");
                }
            }
        }

        public  void FillEstimate()
        {
             

        }

        private void differencerate()
        {
            Double srnetwt, srrate=0, totamt;
            srnetwt = Convert.ToDouble(txt_TotalNetWt.Text);
            totamt = Convert.ToDouble(txtTotAmount.Text);
            if (dgv_SalesReturn.Rows.Count > 0)
            {
                srrate =Convert.ToDouble ( dgv_SalesReturn.Rows[0].Cells["MetalRate"].Value .ToString()==""?"0":dgv_SalesReturn.Rows[0].Cells["MetalRate"].Value .ToString());
            }
            txt_srnetwt.Text = srnetwt.ToString("f3");
            txt_srRate.Text = srrate.ToString("f2");

        }
        void TaxOne()
        {
            Grp_Two.Visible = true; txtTxType.Visible = false;
            if (RbRemoveTax.Checked == true) { CmbGST.Visible = false; label126.Visible = false; CmbGST.SelectedIndex = -1; }
            else { CmbGST.Visible = true; label126.Visible = true;   }
            if (txtTxType.Text != "2" || RbRemoveTax.Checked != true)
            {
                taxrate();
                CalculateFinalAmt();

                taxamt();

                txt_cgstper.Visible = true;
                txt_sgstper.Visible = true;
                txt_igstper.Visible = true;
                txt_sgstamt.Visible = true;
                txt_igstamt.Visible = true;
                grbTextBox4.Visible = true;
                label43.Visible = true;
                label44.Visible = true;
                label45.Visible = true;
                label46.Visible = true;
                label47.Visible = true;
                label48.Visible = true;
                TxtCESSPerc.Visible = true;
                TxtCESSAmt.Visible = true;
                label76.Visible = true;
                txtTotAmount.Visible = true;
                label14.Visible = true;
                label3.Visible = true;
                txtTotalReturnAmount.Visible = true;


            }
            else
            {
                txt_cgstper.Visible = false;
                txt_sgstper.Visible = false;
                txt_igstper.Visible = false;
                txt_sgstamt.Visible = false;
                txt_igstamt.Visible = false;
                grbTextBox4.Visible = false;
                TxtCESSPerc.Visible = false;
                TxtCESSAmt.Visible = false;
                label76.Visible = false;
                label43.Visible = false;
                label44.Visible = false;
                label45.Visible = false;
                label46.Visible = false;
                label47.Visible = false;
                label48.Visible = false;
                txt_cgstper.Text = "0";
                txt_sgstper.Text = "0";
                txt_igstper.Text = "0";
                txt_sgstamt.Text = "0";
                txt_igstamt.Text = "0";
                grbTextBox4.Text = "0";
                TxtCESSPerc.Text = "0";
                TxtCESSAmt.Text = "0";
                txtTotAmount.Visible = true;
                label14.Visible = true;
                label3.Visible = true;
                txtTotalReturnAmount.Visible = true;
                CalculateFinalAmt();
            }

            txtTotalReturnAmount.Focus();
        }

        private void txtTxType_TextChanged_1(object sender, EventArgs e)
        {
            if (txtTxType.Text == "1" || txtTxType.Text == "2" || txtTxType.Text == "3")
            {
                if (txtTxType.Text == "2")
                {
                    RbGrpTaxCalc.Visible = true; RbRemoveTax.Checked = true; cmb_VoucherTypeId.SelectedValue = 67;
                    if (!IsEditMode)
                        txtInvNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtp_InvDate.Value,
                         DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
                }
                else
                {
                    RbGrpTaxCalc.Visible = false; RbAddTax.Checked = false; RbRemoveTax.Checked = false; vouchertype(); if (!IsEditMode)
                        txtInvNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtp_InvDate.Value,
                         DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
                }
                TaxOne();
            }
        }

        private void dgv_SalesReturn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }

        }


        public void fillsalesreturnReceipt()
        {
 
        }
       
        private void TxtVoucherNo_Leave(object sender, EventArgs e)
        {
            //if (TxtVoucherNo.Text != "" && txtcompId.Text != "")
            //{
            //    using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
            //   ("Select SALE.EstSymbol(" + (String.IsNullOrEmpty(TxtSaleReturnId.Text.Trim()) ? "0" : TxtSaleReturnId.Text) + ",'" + TxtVoucherNo.Text + "','" + dtp_InvDate.Text + "'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
            //    {
            //        if (dt.Rows.Count > 0)
            //        {
            //            TxtVoucherNo.Text = dt.Rows[0][0].ToString().Trim();
            //        }
            //    }

            //}
        }    
        private void dgv_SalesReturn_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && dgv_SalesReturn.CurrentCell.OwningColumn.DisplayIndex == dgv_SalesReturn.Columns.Count - 1)
            {

                if (dgv_SalesReturn.Rows.Count > 0)
                {
                    if (dgv_SalesReturn.CurrentRow.Cells["ItemID"].Value.ToString() != "0" && dgv_SalesReturn.CurrentRow.Index == dgv_SalesReturn.Rows.Count - 1)
                    {

                        ((System.Data.DataTable)dgv_SalesReturn.DataSource).Rows.Add(((System.Data.DataTable)dgv_SalesReturn.DataSource).NewRow());

                        dgv_SalesReturn.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        dgv_SalesReturn.CurrentCell = dgv_SalesReturn.Rows[dgv_SalesReturn.Rows.Count - 1].Cells[0];
                        e.Handled = true;
                        dgv_SalesReturn.BeginEdit(false);
                        //Cmb_ItemName.DroppedDown = true;  
                        //dgv_SalesReturn.Focus();
                        // SetComboLocation(dgv_SalesReturn, Cmb_ProdCode_SDtl, dgv_SalesReturn.CurrentCell.OwningColumn.Index, dgv_SalesReturn.CurrentCell.RowIndex);

                    }
                }

            }
            else if (e.KeyChar == 19)
            {
                txtTxType.Visible = true;
                txtTxType.Focus();
            }


        }

        private void cmb_statename_SelectedIndexChanged(object sender, EventArgs e)
        {
            //State();
        }

        private void cmb_statename_SelectedValueChanged(object sender, EventArgs e)
        {
            //State();
        }

        private void txtTotalReturnAmount_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void txtTotalReturnAmount_TextChanged(object sender, EventArgs e)
        {
            GetLessAmount();

            
        }

        private void Rb_Cheque_CheckedChanged(object sender, EventArgs e)
        {
            GroupBank.Visible = Rb_Cheque.Checked;
            label22.Visible = false;
            dtpdue.Visible = false;
        }

        private void GetLessAmount()
        {
            

            taxamt();

        }
        public void BillDeatils()
        {
            if (txt_billNo.Text == "" || flag)
                return;
            if (txt_billNo.Text != "")
            {
                //  using (DataTable dt = DBConn.GetData(new SqlCommand("Select  SalesId,[Invoice No],[Invoice Date],MetalType from SALE.VSalesMaster Where  [Invoice No]= '" + txt_billNo.Text + "'and Branch_id=" + cmb_returnbranch.SelectedValue + " and txType in (1,2) order by SalesId desc")).Tables[0])
                using (DataTable dt = DBConn.GetData(new SqlCommand(" Select top 1  SalesId,[Invoice No],[Invoice Date],[Sales Man Name],[Marketing Name],SalesmanId,MetalType,Custid,Code,[Customer Name],HouseName,Address1,Address2,PhoneNo,CustPanno,statecode,MarketingId,1 as TaxTypeId from SALE.VSalesMaster Where  [Invoice No]= '" + txt_billNo.Text + "'and Branch_id=" + cmb_returnbranch.SelectedValue + " and Salesmode='" + grb_salesmode.Value.ToString() + "' and [Invoice Date]<='" + DateTime.Now.ToString("dd-MMM-yyyy") + "' and txType in (1,2) order by SalesId desc")).Tables[0])
                //using (DataTable dt = DBConn.GetData(new SqlCommand("Select top 1  SalesId,[Invoice No],[Invoice Date],[Sales Man Name],SalesmanId,MetalType from SALE.VSalesMaster Where  [Invoice No]= '" + txt_billNo.Text + "'and Branch_id=" + cmb_returnbranch.SelectedValue + " and Salesmode='" + grb_salesmode.Value.ToString() + "' and txType in (1,2) order by SalesId desc")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        grb_returnbilldate.Text = dt.Rows[0]["Invoice Date"].ToString();
                        TxtSalesEntryId.Text = dt.Rows[0]["SalesId"].ToString();
                        cmbMetalType.SelectedValue = dt.Rows[0]["MetalType"].ToString();
                        CmbSm.Text = dt.Rows[0]["Sales Man Name"].ToString();
                        CmbSm.SelectedValue = Convert.ToInt64(dt.Rows[0]["SalesmanId"]);
                        CmbMManager.Text = dt.Rows[0]["Marketing Name"] == null ? "" : dt.Rows[0]["Marketing Name"].ToString();
                        CmbMManager.SelectedValue = Convert.ToInt64(dt.Rows[0]["MarketingId"] == DBNull.Value ? "0" : dt.Rows[0]["MarketingId"].ToString());
                        CmbGST.SelectedValue = dt.Rows[0]["TaxTypeId"].ToString();
                        PendingItemforSR();
                        // Convert.ToInt64(dt.Rows[0]["MarketingId"]== null ? "0" : dt.Rows[0]["MarketingId"].ToString());
                        //txtCustId.Text = dt.Rows[0]["Custid"].ToString();
                        // Cmb_CustomerName.Text = dt.Rows[0]["Customer Name"].ToString();
                        //txtCustCode.Text = dt.Rows[0]["Code"].ToString();
                        //txthouse.Text = dt.Rows[0]["HouseName"].ToString();
                        //txtAddress1.Text = dt.Rows[0]["Address1"].ToString();
                        //txtAddress2.Text = dt.Rows[0]["Address2"].ToString();
                        //txtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
                        //txtpan.Text = dt.Rows[0]["CustPanno"].ToString();
                        SetColumnsReturn(dgv_SalesReturn);
                        cmbMetalType.Enabled = false;
                        //dgv_SalesReturn.Fill(new Table(SAFA.Classes.Common.DbName, "SALE", "VSalesDetailsForSR", true), "SalesId=" + dt.Rows[0]["SalesId"].ToString() + "");                     
                        //CmbSm.Enabled = false;
                        using (DataTable dt2 = DBConn.GetData(new SqlCommand("SELECT ACC_LedgerId FROM CRM.CUSTOMERMASTER where custid='" + txtCustId.Text + "'")).Tables[0])
                        {
                            if (dt2.Rows.Count > 0)
                            {
                                txtAccLedgerId.Text = dt2.Rows[0]["ACC_LedgerId"].ToString();
                            }
                        }
                        CmbSm.Enabled = false;

                    }
                    else
                    {
                        SetColumnsReturn(dgv_SalesReturn);

                        CmbSm.Enabled = true;

                    }
                }         
            }
            else
            { CmbSm.Enabled = true; }

        }
        public void datedifference()
        {
            DateTime endDate;

            DateTime duedate = Convert.ToDateTime((string.IsNullOrEmpty(dtpdue.Value.ToString()) ? "0.00" : dtpdue.Value.ToString()));
            endDate = DateTime.Now.ToLocalTime();
            double date = (duedate - endDate).TotalDays;
            txt_diffdate.Text = date.ToString();
        }
        private void txt_billNo_TextChanged(object sender, EventArgs e)
        {
     
            //if (txt_billNo.Text != "")
            //{
            //    privilagecard();
            //}
            //BillDeatils();
            
        }
         
        private void txtTotAmount_TextChanged(object sender, EventArgs e)
        {
            CalculateFinalAmt();
        }
        private void CalculateFinalAmt()
        {
            if (flag || editflag)
                return;
            calcautodiscperc();
            double totamt = 0;
            double lessamt = 0;
            Double othercharge = 0;
            foreach (DataGridViewRow dr in dgv_SalesReturn.Rows)
            {
                if (dr.Cells["NetWt"].Value.ToString() != "" &&
                    dr.Cells["MetalRate"].Value.ToString() != "" &&
                    dr.Cells["VAPerc"].Value.ToString() != "" &&
                    dr.Cells["VAPercAfterDisc"].Value.ToString() != "")
                {

                    lessamt += Convert.ToDouble(dr.Cells["NetWt"].Value.ToString()) *
                        Convert.ToDouble(dr.Cells["MetalRate"].Value.ToString()) *
                        (Convert.ToDouble(dr.Cells["VAPerc"].Value.ToString()) - Convert.ToDouble(dr.Cells["VAPercAfterDisc"].Value.ToString())) / 100;
                }
            }
            othercharge = Convert.ToDouble(TxtOTCharge.Text);
            totamt = Convert.ToDouble(txtTotAmount.Text);
            if (txtTxType.Text != "2" || RbRemoveTax.Checked != true)
            {
                txt_finalamt.Text = (((totamt * (103+CESSPerc)) / 100) ).ToString("f2");
            }
            else
            {
                txt_finalamt.Text = totamt.ToString("f2");
            }
            //   txt_finalamt.Visible = true;
            if (flag || editflag)
                return;

            if (txtTxType.Text != "2" || RbRemoveTax.Checked != true)
            {
                if(Compname== "CHIRIANKANDATH JEWELLERY")
                {
                    txtTotalReturnAmount.Text = (((((totamt * (103)) / 100)) - ((lessamt * (103)) / 100)) + othercharge).ToString("f0");

                }
                else
                {
                    txtTotalReturnAmount.Text = (((((totamt * (103 + CESSPerc)) / 100)) - ((lessamt * (103 + CESSPerc)) / 100)) + othercharge).ToString("f0");

                }
            }
            else
            {
                txtTotalReturnAmount.Text = (totamt -  lessamt   ).ToString("f0");
            }

        }
        private void txt_finalamt_TextChanged(object sender, EventArgs e)
        {
        
        }
        
        private void txtLessAmt_TextChanged(object sender, EventArgs e)
        {
         
        }
        private void CurrentBonus()
        {
            double returnamt, bonusamt ;
            returnamt = Convert.ToDouble(string.IsNullOrEmpty(txtTotalReturnAmount.Text.Trim()) ? "0.00" : txtTotalReturnAmount.Text);
            bonusamt = Math.Round(returnamt * .005);
            txt_bonusAmount.Text = bonusamt.ToString("f2");
        }

        private void txt_CardNo_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void cardBonus()
        {
            if (txt_CardNo.Text != null)
            {
                double  bonusamt, opening, totalBonus; 
                opening = Convert.ToDouble(string.IsNullOrEmpty(txt_openamount.Text.Trim()) ? "0.00" : txt_openamount.Text);
                bonusamt = Convert.ToDouble(string.IsNullOrEmpty(txt_bonusAmount.Text.Trim()) ? "0.00" : txt_bonusAmount.Text);
                totalBonus = opening - bonusamt;
                Txt_totalBonus.Text = totalBonus.ToString("f2");
            }
        }

        private void txt_bonusAmount_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void txt_openamount_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Txt_totalBonus_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_SalesReturn_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgv_SalesReturn.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv_SalesReturn.RefreshSummary(true);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_type_Click(object sender, EventArgs e)
        { 
        }

        private void FrmSalesReturnEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
           
           
        }
        public void FillCustomer(long custid)
        {
            if (custid == 0 || txtcompId.Text=="")
                return;
           // Gramboo.General.Setupcombo(Cmb_CustomerName, "CRM.CustomerMaster", "CustName", "CustId", "IsActive='True' AND Company_id=" + txtcompId.Text + "AND  CustId=" + custid);
            // write select query from customer master to get below values and test after 
            using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
               ("select * from CRM.CustomerMaster where CustId='"
               + txtCustId.Text + "'")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    txtCustCode.Text = (dt.Rows[0]["Code"] == DBNull.Value ? "" : dt.Rows[0]["Code"].ToString());
                    txt_CardNo.Text = (dt.Rows[0]["CardNo"] == DBNull.Value ? "" : dt.Rows[0]["CardNo"].ToString());
                    Cmb_CustomerName.Text = (dt.Rows[0]["CustName"] == DBNull.Value ? "" : dt.Rows[0]["CustName"].ToString());
                    txthouse.Text = (dt.Rows[0]["HouseName"] == DBNull.Value ? "" : dt.Rows[0]["HouseName"].ToString());
                    txtAddress1.Text = (dt.Rows[0]["CustAddr1"] == DBNull.Value ? "" : dt.Rows[0]["CustAddr1"].ToString());
                    txtAddress2.Text = (dt.Rows[0]["CustAddr2"] == DBNull.Value ? "" : dt.Rows[0]["CustAddr2"].ToString());
                    txtPhoneNo.Text = (dt.Rows[0]["CustMob"] == DBNull.Value ? "" : dt.Rows[0]["CustMob"].ToString());
                    txtpan.Text = (dt.Rows[0]["CustPanNo"] == DBNull.Value ? "" : dt.Rows[0]["CustPanNo"].ToString());
                    //cmb_statename.Text = dt.Rows[0]["StateName"].ToString();
                    cmb_statename.SelectedValue = (dt.Rows[0]["StateId"] == DBNull.Value ? "18" : dt.Rows[0]["StateId"].ToString());
                    taxrate();
                    // Gramboo.General.Setupcombo(Cmb_OrderNo, "SALE.VSalesOrderPendingReport", "[Order No]", "SalesId", "  Company_id=" + txtcompId.Text + " and Branch_Id=" + txtBranchID.Text + " AND  CustId=" + txtCustId.Text);
                }
                else
                {
                    txt_CardNo.Text = "";
                    Cmb_CustomerName.Text = "";
                    txthouse.Text = "";
                    txtAddress1.Text = "";
                    txtAddress2.Text = "";
                    txtPhoneNo.Text = "";
                    txtpan.Text = "";
                }
            }
        }

        private void txtCustId_TextChanged(object sender, EventArgs e)
        {
            if(txtCustId.Text!="")
            FillCustomer(Convert.ToInt64(txtCustId.Text));

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        private void grb_WholeSale_CheckedChanged(object sender, EventArgs e)
        {
            if (grb_WholeSale.Checked)
                ItemTypeChanged(sender, e);
        }

        private void grb_Retail_CheckedChanged(object sender, EventArgs e)
        {
            if (grb_Retail.Checked)
                ItemTypeChanged(sender, e);
                taxrate();
        }

        private void dtpdue_ValueChanged(object sender, EventArgs e)
        {
            datedifference();
        }

        private void rbCredit_CheckedChanged(object sender, EventArgs e)
        {
            datedifference();
            label22.Visible = true;
            dtpdue.Visible = true;
        }

        private void Rb_Transfer_CheckedChanged(object sender, EventArgs e)
        {
            label22.Visible = false;
            dtpdue.Visible = false;
        }

        private void cmb_returnbranch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_billNo_Leave(object sender, EventArgs e)
        {
            if (txt_billNo.Text != "")
            {
                DateTime invoicedate = DateTime.Today.AddYears(-5);


                using (DataTable dt = DBConn.GetData(new SqlCommand(" Select cast(InvDate as datetime) as InvDate from SALE.salesmaster Where InvNo= '" + txt_billNo.Text + "'and Branch_id=" + cmb_returnbranch.SelectedValue + "")).Tables[0])

                {
                    if (dt.Rows.Count > 0)
                    {
                        invoicedate = Convert.ToDateTime(dt.Rows[0]["InvDate"].ToString());
                        if (invoicedate <= DateTime.Today.AddDays(-15))
                        {
                            txtRemark.AcceptBlankValue = false;
                            txtRemark.ShowComplusoryMark = true;
                        }
                        else
                        {
                            txtRemark.AcceptBlankValue = true;
                            txtRemark.ShowComplusoryMark = false;
                        }

                       
                         
                    }
                }

               

            }
            //if (txt_billNo.Text != "")
            //{
            //    privilagecard();
            //}
            //BillDeatils();
        }

        private void rva_CheckedChanged(object sender, EventArgs e)
        {


          

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void cmbMetalType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (GrbPending.Visible == false)
            {
                GrbPending.Parent = this;
                GrbPending.Visible = true;
                GrbPending.BringToFront();
                GrbPending.Show();
                GrbPending.BringToFront();
                GrbPending.Location = new Point(75, 150);
                //GrbPending.Size = new Size(this.Width - 200, 200);
            }

            else
            {
                GrbPending.Visible = false;
                GrbPending.SendToBack();
                GrbPending.Hide();
            }

        }

        void PendingItemforSR()
        {
            if (txt_billNo.Text != null || txt_billNo .Text =="")
            {       
            dgviewpending.AutoGenerateColumns = true;
            dgviewpending.ShowSerialNo = true;
            dgviewpending.SummaryColumns = new string[] { "Qty","Gwt","NetWt","DiaWt","StoneWt" };
            dgviewpending.HiddenDataFields = new List<string>() { "salesid", "SalesTransId" };
            dgviewpending.DataSource = this.DBConn.GetData(new SqlCommand("Select cast('false' as bit) as [Select],salesid,SalesTransId,ProdCode,Code,ItemName,Qty,Gwt,NetWt,DiaWt,StoneWt from SALE.FunSalesReturnPending('" + TxtSalesEntryId.Text + "'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0];
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            taxrate();
            DataTable dt = new DataTable();

            dt = (DataTable)dgv_SalesReturn.DataSource;
            try
            {
                dgv_SalesReturn.Rows.RemoveAt(0);
            }
            catch (Exception)
            {
            }
            dgviewpending.EndEdit();
            foreach (DataGridViewRow r in dgviewpending.Rows)
            {
                try
                {
                    if ((bool)((DataGridViewCheckBoxCell)r.Cells["Select"]).Value == true)
                    {
                        using (System.Data.DataTable dt5 = DBConn.GetData(new SqlCommand("Select TransId,ProdCodeId,ProdCode,HuId,ItemId,Code,ItemName,SalesReturnId,Created_date,Created_by,Last_modified_by,Last_modified_date,Company_id,Branch_id,Counter_id,IsActive,ModelId,Model,PurityID,PurityName,Touch,Qty,Gwt,StoneWt,StoneCtWt,MetalRate,DiaNo,DiaWt,DiaCash,CertificationCharge,NetWt,Wastage, MC,WastageCash,VAPerc,VAPercAfterDisc,VA,IsReceipt,MetalCash,StCashDiff,StoneCash,Total,Type,Disc,SalesTransId from SALE.VSalesDetailsForSR WHERE SalesTransId=" + r.Cells["SalesTransId"].Value.ToString())).Tables[0])
                        {
                            if (dt5.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt5.Rows)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr.ItemArray = row.ItemArray;
                                    dt.Rows.Add(dr);                                  
                                }
                            }

                        }
                        //dgviewpending.Rows.Remove(r);
                    }
                    GrbPending.Visible = false;
                    GrbPending.SendToBack();
                    GrbPending.Hide();
                    type();
                }
                catch (Exception)
                {

                }
                dgv_SalesReturn.Focus();             
            }
           
        }

        void type()
        {
            foreach (DataGridViewRow r in dgv_SalesReturn.Rows)
            {
                r.Cells["IsReceipt"].Value = true;
                r.Cells["Type"].Value = "R";
            }
        }

        private void linkLbl_OtherCharges_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (GrpPurOtherCharges.Visible == false)
            {
                GrpPurOtherCharges.Parent = this;
                GrpPurOtherCharges.Size = new System.Drawing.Size(1000, 250);
                GrpPurOtherCharges.Location = new Point(50, 210);
                GrpPurOtherCharges.Visible = true;
                GrpPurOtherCharges.BringToFront();
                GrpPurOtherCharges.BringToFront();
                GrpPurOtherCharges.Show();
                linkLbl_OtherCharges.Text = "Hide Other Charges";
                cmbtdsName.Text = "";
                cmbtdsName.SelectedValue = "0";
                txt_tdsamt.Text = "";
                TXT_TDSPerc.Text = "";
            }
            else
            {
                GrpPurOtherCharges.Visible = false;
                GrpPurOtherCharges.SendToBack();
                GrpPurOtherCharges.Hide();
                linkLbl_OtherCharges.Text = "Add Other Charges";
            }
        }
        void OthChGrid()
        {
            dgv_otherChg.AutoGenerateColumns = true;
            dgv_otherChg.ShowSerialNo = true;
            dgv_otherChg.DataFields = new List<string> { "SalesReturnId", "OTchId", "ChargeID", "[Charge Name]", "NetAmount", "SGSTAmt", "CGSTAmt", "IGSTAmt", "CESSAmt", "CGSTPer", "IGSTPer", "SGSTPer","CESSPer", "TdsName", "TdsId", "TdsRate", "TdsAmount", "Amount", "Company_id", "Branch_id" };
            dgv_otherChg.SummaryColumns = new string[] { "Amount" };
            dgv_otherChg.HiddenDataFields = new List<string>() { "SalesReturnId", "OTchId", "ChargeID", "CGSTPer", "IGSTPer", "SGSTPer", "CESSPer", "Company_id", "Branch_id", "TdsId" };
            dgv_otherChg.Fill(new Table(SAFA.Classes.Common.DbName, "SALE", "VSalesReturnOtherCharges", true), "1=2");
            dgv_otherChg.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
        }
        public void taxrateOtherch()
        {
            if (cmb_statename.SelectedValue != null)
            {
                if (txtcompId.Text != "")
                {
                    using (DataTable t = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                       ("select CGSTper,Sgstper,IGSTper,Cessper from PUR.MiscChargeMaster where ChargeId='" + Cmb_Chargename.SelectedValue + "'")).Tables[0])
                    using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                          ("select Comp_StateCode from SYST.BranchMaster where Branchid= " + txtBranchID.Text + "")).Tables[0])
                        if (t.Rows.Count > 0)
                        {
                            if (cmb_statename.SelectedValue.ToString() == dt.Rows[0]["Comp_StateCode"].ToString())
                            {
                                label96.Text = t.Rows[0]["CGSTper"].ToString();
                                label93.Text = t.Rows[0]["Sgstper"].ToString();
                                txtSgstPerc.Text = t.Rows[0]["Sgstper"].ToString();
                                txtCgstPec.Text = t.Rows[0]["CGSTper"].ToString();
                                
                                if (grb_Retail.Checked == true)
                                {
                                    TxtCessPec.Text = t.Rows[0]["Cessper"].ToString();
                                    label8.Text = t.Rows[0]["Cessper"].ToString();
                                }
                                else
                                {
                                    TxtCessPec.Text = "0.00";
                                    label8.Text = "0.00";
                                }





                                label99.Text = "0.00";
                            }
                            else
                            {
                                label99.Text = t.Rows[0]["IGSTper"].ToString();
                                txtigstPec.Text = t.Rows[0]["IGSTper"].ToString();
                                label93.Text = "0.00";
                                label96.Text = "0.00";
                                label8.Text = "0.00";
                            }
                            //txtTaxRatePerc.Text = t.Rows[0]["IGSTper"].ToString();
                        }
                }
            }
        }

        private void Cmb_Chargename_SelectedValueChanged(object sender, EventArgs e)
        {
            taxrateOtherch();
            cmbtdsName.Text = "";
            cmbtdsName.SelectedValue = "0";
        }

        private void txtamout_TextChanged(object sender, EventArgs e)
        {
            Othertaxcalu();
            OthertaxAmtcalu();
        }
        public void Othertaxcalu()
        {
            Double SGSTper = 0, CGSTper = 0, IGSTper = 0,Cessper=0, Amt = 0;

            SGSTper = Convert.ToDouble(label93.Text == "" ? "0" : label93.Text);
            CGSTper = Convert.ToDouble(label96.Text == "" ? "0" : label96.Text);
            IGSTper = Convert.ToDouble(label99.Text == "" ? "0" : label99.Text);
            Cessper= Convert.ToDouble(label8.Text == "" ? "0" : label8.Text);
            if (txtamout.Text != "")
            {
                Amt = Convert.ToDouble(txtamout.Text == "" ? "0" : txtamout.Text);
                TxtSGST.Text = (Amt * SGSTper / 100).ToString("f2");
                TxtCGST.Text = (Amt * CGSTper / 100).ToString("f2");
                TxtCESS.Text = (Amt * Cessper / 100).ToString("f2");
                if (IGSTper != 0)
                {
                    TxtIGST.Text = (Amt * IGSTper / 100).ToString("f2");
                }
                //if(Cessper!=0)
                //{
                //    TxtCESS.Text = (Amt * Cessper / 100).ToString("f2");
                //}

            }
        }
        public void OthertaxAmtcalu()
        {
            double SGSTAmt = 0, CGSTAmt = 0, IGSTAmt = 0,CessAmt=0, TotlAmt = 0, finalAmt, Amount = 0, tdsperc = 0, TdsAmount=0;
            Amount = Convert.ToDouble(txtamout.Text == "" ? "0" : txtamout.Text);
            tdsperc = Convert.ToDouble(TXT_TDSPerc.Text == "" ? "0" : TXT_TDSPerc.Text);

            SGSTAmt = Convert.ToDouble(TxtSGST.Text == "" ? "0" : TxtSGST.Text);
            CGSTAmt = Convert.ToDouble(TxtCGST.Text == "" ? "0" : TxtCGST.Text);
            IGSTAmt = Convert.ToDouble(TxtIGST.Text == "" ? "0" : TxtIGST.Text);
            CessAmt = Convert.ToDouble(TxtCESS.Text == "" ? "0" : TxtCESS.Text);
            finalAmt = Convert.ToDouble(txtamout.Text == "" ? "0" : txtamout.Text);
            TdsAmount = Convert.ToInt64(Amount * (tdsperc / 100));
            txt_tdsamt.Text = TdsAmount.ToString("f0");

            TotlAmt = (SGSTAmt + CGSTAmt + IGSTAmt + CessAmt + finalAmt - TdsAmount);
            txtAmount_Charge.Text = TotlAmt.ToString("f2");
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            if (Cmb_Chargename.SelectedValue != null || cmbtdsName.SelectedValue != null)
            {
                txtChargeId.Text = Cmb_Chargename.SelectedValue.ToString();
                //dgv_otherChg.Save();
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
            }
        }

        private void dgv_otherChg_SummaryCalculated(object source, EventArgs e)
        {
            if (flag || editflag)
                return;
            TxtOTCharge.Text = dgv_otherChg.SummaryRow.SummaryCells["Amount"].Text;
        }

        private void TxtOTCharge_TextChanged(object sender, EventArgs e)
        {
            CalculateFinalAmt();
        }
        void getAmt()
        {
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                    ("select OTCharge,ISNULL(TxTypeCal,0) as TxTypeCal,ISNULL(TaxTypeId,1) as TaxTypeId,ISNULL(MetTaxIncu,0) as MetTaxIncu,ISNULL(MetUnFixed,0) as MetUnFixed from SALE.SalesReturnMaster where SalesReturnId=" + TxtSaleReturnId.Text + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    TxtOTCharge.Text = dt.Rows[0]["OTCharge"].ToString();
                    string TxTypeCal = dt.Rows[0]["TxTypeCal"].ToString();
                    ChkTaxIncl.Checked = Convert.ToBoolean(dt.Rows[0]["MetTaxIncu"].ToString());
                    ChkUnFixed.Checked = Convert.ToBoolean(dt.Rows[0]["MetUnFixed"].ToString());
                    if (txtTxType.Text == "2")
                    {
                        if (TxTypeCal == "True") { RbRemoveTax.Checked = true; } else { RbAddTax.Checked = true; CmbGST.SelectedValue = dt.Rows[0]["TaxTypeId"].ToString(); }
                    }
                    else { CmbGST.SelectedValue = dt.Rows[0]["TaxTypeId"].ToString(); }
                }
            }
        }

        void getRecFrom()
        {
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                    ("select isnull(RECFrom,'N') as RECFrom From SALE.SalesReturnMaster where SalesReturnId =" + TxtSaleReturnId.Text + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    RecFrom = dt.Rows[0]["Recfrom"].ToString();
                }
            }
            if (RecFrom == "S")
            {
                Entry = "Sales Entry";
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                   ("select Isnull(InvNo,'') as InvNo  from SALE.SalesMaster where ReturnID=" + TxtSaleReturnId.Text + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        BillNo = dt.Rows[0]["InvNo"].ToString();
                    }
                }
            }
            else if (RecFrom == "SO")
            {
                Entry = "Sales Order";
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                   ("select isnull(InvNo,'') as InvNo from SALE.SalesOrderMaster where ReturnID=" + TxtSaleReturnId.Text + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        BillNo = dt.Rows[0]["InvNo"].ToString();
                    }
                }
            }
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

        private void dgv_otherChg_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn c in dgv_otherChg.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void BtnBillAdj_Click(object sender, EventArgs e)
        {
            //SAFA.Forms.ACC.AdjustingEntries AE = new SAFA.Forms.ACC.AdjustingEntries();
            //AE.CloseClick += new SAFA.Forms.ACC.AdjustingEntries.CloseClickEventHandler(BillAdjBtn);
            //AE.dt = dtp_InvDate.Value;
            //AE.Amount = Convert.ToDouble(txtTotalReturnAmount.Text);
            //AE.RecTable = "SALE.SalesReturnMaster"; AE.RecVchNo = txtInvNo.Text;
            //AE.RecId = TxtSaleReturnId.Text; AE.RecCode = "SR";
            //AE.BillType = "R"; AE.PartyType = "Customer";
            //AE.PartyId = Convert.ToInt64(txtCustId.Text);
            //AE.Fill(TxtSaleReturnId.Text);
            //AE.ShowDialog();
        }
        //public void BillAdjBtn(object sender, SAFA.Forms.ACC.AdjustingEntries.CloseClickEventArgs e)
        //{
        //    BillAdjButtonChange();
        //}
        //void BillAdjButtonChange()
        //{
        //    ToolTip toolTip1 = new ToolTip();
        //    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
        //                                      ("select AdjId From ACC.AdjustingEntrieMaster where RecTable='SALE.SalesReturnMaster'and RecId=" + TxtSaleReturnId.Text + " and Company_Id=" + txtcompId.Text + " and Branch_Id=" + txtBranchID.Text + "")).Tables[0])
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

        private void RbAddTax_CheckedChanged(object sender, EventArgs e)
        {
            if (txtTxType.Text == "1" || txtTxType.Text == "2" || txtTxType.Text == "3")
            {
                TaxOne();
            }
        }

        private void RbRemoveTax_CheckedChanged(object sender, EventArgs e)
        {
            if (txtTxType.Text == "1" || txtTxType.Text == "2" || txtTxType.Text == "3")
            {
                TaxOne();
            }
        }

        private void CmbGST_SelectedValueChanged(object sender, EventArgs e)
        {
            taxrate();
        }

        private void txtInvNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbEstno_SelectedValueChanged(object sender, EventArgs e)
        {
            FillEstimate();
        }

        public override bool Delete()
        {
           
            getRecFrom();
            if (txtRecfrom.Text != RecFrom)
            {
                if (Entry != "")
                {
                    Gramboo.General.ShowMessage(
                  " This Sales Return Entry was received from " + Entry + ". \n\n" +
                  " You’ll need to fix it there. Reference Bill no " + BillNo + " \n", "WARNING!!!!", MessageBoxIcon.Warning, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);
                    return false;
                }
                else
                {
                    Gramboo.General.ShowMessage("You Cannot Delete This Entry");
                    return false;
                }
            }
            else
            {
                string id = TxtSaleReturnId.Text;
                if (base.Delete())
                {
                    DBConn.ExecuteSqlTransaction(new SqlCommand("Delete From ACC.AdjustingEntrieDetails where AdjId in (select AdjId from ACC.AdjustingEntrieMaster where RecTable='SALE.SalesReturnMaster'and RecId=" + id.ToString() + " and  Branch_id=" + txtBranchID.Text + " and Company_id =" + txtcompId.Text + ") and  Branch_id=" + txtBranchID.Text + " and Company_id =" + txtcompId.Text + ""), "DeleteDetailTbl");
                    DBConn.ExecuteSqlTransaction(new SqlCommand("Delete From ACC.AdjustingEntrieMaster where RecTable='SALE.SalesReturnMaster'and RecId=" + id.ToString() + " and  Branch_id=" + txtBranchID.Text + " and Company_id =" + txtcompId.Text), "DeleteMaster");
                    return true;
                }
                else { return false; }
            }
        }

        private void dgv_SalesReturn_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            ItemDetails();
        }

        private void BtnMetalCashCal_Click(object sender, EventArgs e)
        {

            if (TxtMetalRate.Text == "0") { TxtMetalRate.ShowMessage("Blank Values Not Allowed...!!"); return; }
            if (TxtMetPurity.Text == "0") { TxtMetPurity.ShowMessage("Blank Values Not Allowed...!!"); return; }
            if (ChkTaxIncl.Checked == true && (GSTPerc + CESSPerc) == 0) { txtSgstPerc.ShowMessage("Blank Values Not Allowed...!!"); return; }

            foreach (DataGridViewRow r in dgv_SalesReturn.Rows)
            {
                Double Netwt = 0, Touch = 0, Met_Puirty = 0, MetalCash = 0, Met_Rate = 0, Wt = 0;
                Met_Puirty = Convert.ToDouble(TxtMetPurity.Text);
                Met_Rate = Convert.ToDouble(TxtMetalRate.Text);

                if (r.Cells["ItemId"].Value.ToString().Trim().Length >= 2)
                {
                    Touch = Convert.ToDouble(r.Cells["Touch"].Value.ToString() == "" ? "0" : r.Cells["Touch"].Value.ToString());
                    Netwt = Convert.ToDouble(r.Cells["Netwt"].Value.ToString() == "" ? "0" : r.Cells["Netwt"].Value.ToString());

                    Wt = Math.Round(((Netwt * Touch) / Met_Puirty), 2);
                    MetalCash = Math.Round(Wt * Met_Rate, 2);
                    if (ChkTaxIncl.Checked == true)
                    {
                        r.Cells["MetalCash"].Value = (MetalCash * 100 / (100 + (GSTPerc + CESSPerc))).ToString("F2");
                    }
                    else
                    {
                        r.Cells["MetalCash"].Value = MetalCash.ToString("F2");
                    }

                    //----Calc TotalAmount-----

                    Double Diacash = 0, Stcash = 0, GoldCash = 0, TotalCash = 0, VA = 0, MC = 0, WastCash = 0;

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

                    if (r.Cells["VA"].Value.ToString() != "")
                    {
                        VA = Convert.ToDouble(r.Cells["VA"].Value.ToString());
                    }

                    if (r.Cells["MC"].Value.ToString() != "")
                    {
                        MC = Convert.ToDouble(r.Cells["MC"].Value.ToString());
                    }

                    if (r.Cells["WastageCash"].Value.ToString() != "")
                    {
                        WastCash = Convert.ToDouble(r.Cells["WastageCash"].Value.ToString());
                    }

                    TotalCash = GoldCash + Stcash + Diacash + VA;

                    r.Cells["Total"].Value = TotalCash.ToString();

                }
            }
        }
        //public void CellBClickest(object sender, SAFA.Forms.SALE.FrmESTNoGrid.CellClickedEventArgs e)
        //{

        //    cmbEstno.SelectedValue = e.TargetID;

        //}
        private void grbButton2_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    SAFA.Forms.SALE.FrmESTNoGrid frm = new SAFA.Forms.SALE.FrmESTNoGrid();
            //    frm.frmname = "SALE_RTN";
            //    frm.CellButtonClickEst += new SAFA.Forms.SALE.FrmESTNoGrid.CellClickedEventHandler(CellBClickest);
            //    frm.ShowDialog();
            //}
            //catch (Exception ex) { }
        }

        private void CmbInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbtdsName_SelectedValueChanged(object sender, EventArgs e)
        {           
            TdsRate();
        }
        public void TdsRate()
        {

            if (Cmb_Chargename.SelectedValue == null)
                return;
            if (cmbtdsName.SelectedValue != null && cmbtdsName.SelectedValue.ToString().Trim().Length > 3)
            {
                Int64 LedgerId = 0; String PartyType = "";

                using (DataTable dt = DBConn.GetData(new SqlCommand("select Isnull(Acc_LedgerID,0) as Acc_LedgerID from CRM.CustomerMaster where Custid='" + txtCustId.Text + "'")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        PartyType = "Customer";
                        LedgerId = Convert.ToInt64(dt.Rows[0]["Acc_LedgerID"].ToString());
                    }
                }

                Double othind = 0.00, ind = 0.00, nopan = 0.00;

                using (DataTable dt = DBConn.GetData(new SqlCommand("select top 1 Otherthanindividual,Individual,NoPanno FROM GEN.TDSDetails  where TdsId='" + cmbtdsName.SelectedValue + "' and date<='" + dtp_InvDate.Value + "' order by date desc")).Tables[0])
               // using (DataTable dt = DBConn.GetData(new SqlCommand("select Indivi,Othind,Nopan FROM GEN.TDSMaster  where TdsId='" + cmbtdsName.SelectedValue + "'")).Tables[0])
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
        private void ItemDetails()
        {


            if (txtitemcode.Text != null)
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                             ("SELECT [Item Name],[Item Code] FROM ITM.VItemMaster WHERE [Item Code] ='" + dgv_SalesReturn.CurrentRow.Cells["Code"].Value + "'AND BillGroup='" + cmbMetalType.SelectedValue + "'")).Tables[0])
                //using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                //             ("SELECT [Item Name],[Item Code] FROM ITM.VItemMaster WHERE ItemId =" + (dgv_SalesReturn.CurrentRow.Cells["ItemId"].Value.ToString() == "" ? "0" : dgv_SalesReturn.CurrentRow.Cells["ItemId"].Value.ToString()) + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        Cmb_ItemName_Return.Text = (dt.Rows[0]["Item Name"] == DBNull.Value ? "" : dt.Rows[0]["Item Name"].ToString());

                    }

                }
            }


        }

    }
}