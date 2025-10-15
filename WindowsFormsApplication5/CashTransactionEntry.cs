using Gramboo.Database;
using SAFA.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gramboo;


namespace SAFA.Forms.ACC
{
    public partial class CashTransactionEntry : Gramboo.Controls.GrbForm
    {
        private static CashTransactionEntry instance;
        private DataController dc = new DataController();
        bool flag;
        bool payment = false, Receipt = false, BankPayment = false, BankReceipt = false, TransType = false;
        DateTime date; public String BankId = "0"; string dtpfrom, dtpto; bool msgval;
        bool Message = true; Int64 ClsId = 0; Double editamt = 0;
        public static CashTransactionEntry Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new CashTransactionEntry();
                }
                else if (instance.IsDisposed)
                {
                    instance = new CashTransactionEntry();
                }
                return instance;
            }
        }

        private int _vchtype = 27;
        [Browsable(false)]
        [DefaultValue(27)]
        public int VoucherType
        {
            get
            {
                return _vchtype;
            }

            set
            {
                try
                {
                    _vchtype = value;
                    if (DBConn.GetData(new System.Data.SqlClient.SqlCommand("select * FROM Acc.VoucherTypeMaster WHERE VoucherTypeId=" + value)).Tables[0].Rows.Count <= 0)
                    {


                        throw new Exception("Voucher Type is Invalid");
                    }
                    else
                    {
                        cmb_VoucherTypeId.SelectedValue = VoucherType;
                    }
                }
                catch
                {
                }
            }
        }



        public CashTransactionEntry()
        {
            InitializeComponent();
            date = DateTime.Now;
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

        public override bool InitializeTables()
        {
            Table t = new Table(SAFA.Classes.Common.DbName, "ACC", "CashTransactionEntry");
            t.PrimaryKeys.Add("TransId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.NotUpdatables.Add("VchNo");
            t.IdTextBox = txtEntryNo;

            Table t2 = new Table(SAFA.Classes.Common.DbName, "ACC", "CashTransactionBillClosing", true);
            t.PrimaryKeys.Add("ClsId");
            t2.FillView = new Table(SAFA.Classes.Common.DbName, "ACC", "VCashTransactionBillClosing", true);
            t2.DatagridView = dgv_advanceBill;
            t2.IsDatagridView = true;
            t2.IdTextBox = TxtClsId;
            t.ChildTables.Add(t2);

            this.TableName = t;
            return true;

        }

        public override void Init()
        {
            flag = true;

            base.Init();
            Message = true;
            BtnDocAdd.Visible = false;
            CmbBillNo.SelectedValue = -1;
            CmbBillNo.Enabled = true;
            panel1.Visible = false;


            if (BankReceipt == true)
            {
                Rb_BankRecpt.Checked = true;
            }
            else if (Receipt == true)
            {
                Rb_Receipt.Checked = true;
            }
            else if (BankPayment == true)
            {
                Rb_BankPay.Checked = true;
            }
            else
            {
                Rb_Payment.Checked = true;
            }


            Dtp_dt.Value = date;

            LinkLabelBillDetailes.Text = "Bill Details";

            this.ListForm = ACC.CashTransactionEntryList.Instance;
            AdjustColumWidth();

            txtOne_Two.Visible = false;

            //txtOne_Two.Text = "1";
            label16.Text = "";

            if (this.TableName != null)
                GenerateID(this.TableName);

            lblshopBranch.Visible = false;
            cmb_Branch.Visible = false;
            CmbLedgercode.Focus();
            //if (Rb_Payment.Checked == true)
            //{
            //    cmb_BankAccId.Enabled = false;
            //}
            //else
            //{
            //    cmb_BankAccId.Enabled = true;
            //}
            flag = false;

            dgv_advanceBill.DataFields = new List<string>() { "TransId", "ClsId", "ClsBillId", "BillNo", "RateCuttingMode", "[Rate Cutting]", "Rate", "Weight", "NonWtAmt", "ClsAmount", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Company_id", "Branch_id", "Counter_id", "IsActive" };
            dgv_advanceBill.HiddenDataFields = new List<string>() { "TransId", "ClsId", "ClsBillId", "RateCuttingMode", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Company_id", "Branch_id", "Counter_id", "IsActive" };
            dgv_advanceBill.SummaryColumns = new string[] { "ClsAmount", "Weight" };
            dgv_advanceBill.Fill(new Table(SAFA.Classes.Common.DbName, "ACC", "VCashTransactionBillClosing", true), "1=2");
            dgv_advanceBill.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            dgv_advanceBill.Columns["BillNo"].Width = CmbClsBillNo.Width;


            //dtpfrom = FA.Common.GetFiscalStart(DBConn, DateTime.Now.Date, Convert.ToInt32(txtcompId.Text)).ToString();
            //dtpto = FA.Common.GetFiscalEnd(DBConn, DateTime.Now.Date, Convert.ToInt32(txtcompId.Text)).ToString();

        }

        public override void RefreshData()
        {
            base.RefreshData();

            flag = true;

            Gramboo.General.Setupcombo(cmb_BankAccId, "ACC.BankAccountMaster", "Acc_BankAccName", "Acc_BankAccId", "IsActive='True'  AND  Branch_id=" + txtBranchID.Text);
            Gramboo.General.Setupcombo(cmb_BankId, "GEN.BankMaster", "BankName", "BankId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmb_Branch, "SYST.BranchMaster", "BranchName", "BranchId", "IsActive='True'");
            Gramboo.General.Setupcombo(CmbTCS, "GEN.TCSMaster ", "TcsName", "TcsId", "IsActive='True'");
            //Gramboo.General.Setupcombo(cmbemployee, "EMP.EmployeeMaster", "EmpName", "EmpId", "IsActive='True' AND  Branch_id=" + txtBranchID.Text);
            Cmb_ledgerhead.SelectedValueChanged -= Cmb_ledgerhead_SelectedValueChanged;

            CmbLedgercode.SelectedValueChanged -= CmbLedgercode_SelectedValueChanged;

            string str = "SELECT Acc_LedgerId,Acc_LedgerCode,Acc_LedgerName FROM  ACC.LoadLedgersForCashTransactions(" + txtBranchID.Text + "," + txtcompId.Text + ") order by  Acc_LedgerName";
            SqlDataAdapter da = new SqlDataAdapter(str, DBConn.ConnectionProperties.ConnectionString);
            da.SelectCommand.CommandTimeout = 0;
            DataTable LedagerCode = new DataTable(); DataTable Ledager = new DataTable();
            da.Fill(LedagerCode); da.Fill(Ledager);

            //LedagerCode.DefaultView.Sort = "Ledger Code ASC";
            // LedagerCode = LedagerCode.DefaultView.ToTable();

            CmbLedgercode.DisplayMember = "Acc_LedgerCode";
            CmbLedgercode.ValueMember = "Acc_LedgerId";

            CmbLedgercode.DataSource = LedagerCode;
            CmbLedgercode.Text = "";
            CmbLedgercode.SelectedValue = 0;

            //Ledager.DefaultView.Sort = "[Ledger Name] ASC";
            //Ledager = Ledager.DefaultView.ToTable();

            Cmb_ledgerhead.DisplayMember = "Acc_LedgerName";
            Cmb_ledgerhead.ValueMember = "Acc_LedgerId";

            Cmb_ledgerhead.DataSource = Ledager;
            Cmb_ledgerhead.SelectedValue = 0;
            Cmb_ledgerhead.Text = "";

            //Gramboo.General.Setupcombo(CmbLedgercode, "ACC.LedgerMaster", "acc_ledgercode", "Acc_LedgerId", "IsActive='True'  AND  Branch_id=" + txtBranchID.Text);

            //Gramboo.General.Setupcombo(Cmb_ledgerhead, "ACC.LedgerMaster", "acc_ledgername", "Acc_LedgerId", "IsActive='True'  AND  Branch_id=" + txtBranchID.Text);
            Cmb_ledgerhead.SelectedValueChanged += Cmb_ledgerhead_SelectedValueChanged;

            CmbLedgercode.SelectedValueChanged += CmbLedgercode_SelectedValueChanged;
            populategrid();
            flag = false;
            if (BankId != "0")
            {
                cmb_BankAccId.SelectedValue = BankId;
            }
            if (Rb_Payment.Checked == true || Rb_Receipt.Checked == true)
            {
                cmb_BankAccId.SelectedValue = 0;
            }
        }

        public override bool ValidateControls()
        {
            string str = "select Date as entrydate from ACC.CashTransactionEntry where TransId = " + txtEntryNo.Text;
            //if (IsEditMode)
            //{
            //    if (SAFA.Classes.Common.ChkPermission(txtUserName.Text, Gramboo.GeneralConfig.UserId, this.Text, Convert.ToDateTime(Dtp_dt.Text), Convert.ToInt32(txtBranchID.Text), Convert.ToInt32(txtcompId.Text), 2, str) == false)
            //    { return false; }
            //}
            //else
            //{

            //    if (SAFA.Classes.Common.ChkPermission(txtUserName.Text, Gramboo.GeneralConfig.UserId, this.Text, Convert.ToDateTime(Dtp_dt.Text), Convert.ToInt32(txtBranchID.Text), Convert.ToInt32(txtcompId.Text), 1, str) == false)
            //    { return false; }
            //}


            if (base.ValidateControls())
            {
                if (!IsEditMode)
                {
                    //if (Rb_BankPay.Checked == true && Rb_Cheque.Checked == true)
                    //{
                    //    if (!SAFA.Classes.Common.ValidateCheqNo(DBConn, Convert.ToInt64(cmb_BankAccId.SelectedValue), txtChequeNo.Text))
                    //    {
                    //        txtChequeNo.Focus();
                    //        return false;
                    //    }
                    //}
                }

                payment = Rb_Payment.Checked;

                Receipt = Rb_Receipt.Checked;

                BankPayment = Rb_BankPay.Checked;

                BankReceipt = Rb_BankRecpt.Checked;

                date = Dtp_dt.Value.Date;

                //if (!SoftwareSettings.TxType) { txtOne_Two.Text = "1"; }

                if (txtOne_Two.Text == "")
                {
                    txtOne_Two.Visible = true;
                    txtOne_Two.Focus();
                    return false;
                }
                if (txtOne_Two.Text != "1" && txtOne_Two.Text != "2" && txtOne_Two.Text != "3")
                {
                    txtOne_Two.Visible = true;
                    txtOne_Two.Focus();
                    txtOne_Two.ShowMessage("Enter A Valid Number");
                    return false;
                }

                if (Rb_BankPay.Checked == true || Rb_BankRecpt.Checked)
                {
                    //cmb_BankAccId.AcceptBlankValue = false;
                    if (cmb_BankAccId.SelectedValue == null)
                    {
                        cmb_BankAccId.ShowMessage("Pls Fill the Bank Details");
                        return false;
                    }
                }

                bool BillWise = false;
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                           ("select isnull(BillWise,0) as BillWise from ACC.VLedgerMaster where Acc_LedgerId =" + Cmb_ledgerhead.SelectedValue + " and Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        BillWise = Convert.ToBoolean(dt.Rows[0]["BillWise"].ToString());
                    }
                }

                if (BillWise == true)
                {
                    if (chk_advance.Checked == false && grbRadioButtonGroup1.Value == "")
                    {
                        Gramboo.General.ShowMessage("Choose A Bill to Close or Make the Entry as Advance");
                        return false;
                    }

                    if (grbRadioButtonGroup1.Value != "")
                    {
                        if (grbRadioButtonGroup1.Value == "S")
                        {
                            if (CmbBillNo.SelectedValue != null && CmbBillNo.SelectedValue.ToString().Trim().Length > 2)
                            {
                                if (Math.Round(Convert.ToDouble(TxtValiBillAmt.Text), 2) - Math.Round(Convert.ToDouble(txtAmount.Text), 2) < 0)
                                {
                                    String A = Convert.ToString(Math.Abs(Math.Round(Convert.ToDouble(TxtValiBillAmt.Text), 2) - Math.Round(Convert.ToDouble(txtAmount.Text), 2)));
                                    txtAmount.ShowMessage("you have entered " + A + " ₹ greater than your bill amount");
                                    return false;
                                }
                            }
                            else
                            {
                                CmbBillNo.ShowMessage("Select a bill to close");
                                return false;
                            }
                        }
                        else if (grbRadioButtonGroup1.Value == "G")
                        {
                            if (dgv_advanceBill.RowCount <= 0) { Gramboo.General.ShowMessage("Add Details to advance closing Grid"); return false; }
                        }
                    }
                }

                if (RbtTransType.Value == "P" || RbtTransType.Value == "BP")
                {
                    if (Math.Round(Convert.ToDouble(txt_cashbalance.Text), 2) < 0)
                    {
                        //String A = Convert.ToString(Math.Abs(Math.Round(Convert.ToDouble(txt_cashbalance.Text), 2)));
                        //txt_cashbalance.ShowMessage("Withdrawal Limit Exceeded Add " + A + " ₹ More to Continue");
                        //return false;
                    }
                }

                if (cmb_Branch.SelectedValue == null)
                    cmb_Branch.SelectedValue = txtBranchID.Text;

                if (!IsEditMode)
                    TxtVoucherNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_dt.Value,
                DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
                return true;
            }
            else
            {
                return false;
            }
        }

        private void cashinHand()
        {
            if (flag)
                return;
            Double debit = 0, credit = 0;
            if (txtcompId.Text == "")
                return;

            if (Rb_Payment.Checked || Rb_Receipt.Checked && cmb_BankAccId.SelectedValue == null)
            {
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                      ("Select ACC.GetLedgerIdByName('Cash-in-hand'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                    if (dt.Rows.Count > 0)
                    {
                        txt_accledger_id.Text = dt.Rows[0][0].ToString();
                    }
                using (DataTable dt1 = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                      ("Select Isnull(Debit,0) as Debit,isnull(Credit,0) as Credit  FROM ACC.LedgerOpnForADay(" + txt_accledger_id.Text + ",'" + Dtp_dt.Value.AddDays(1).ToString("dd-MMM-yyyy") + "','" + chkwt.Checked.ToString() + "', " + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                {
                    if (dt1.Rows.Count > 0)
                    {
                        debit = Convert.ToDouble(dt1.Rows[0]["Debit"]);
                        credit = Convert.ToDouble(dt1.Rows[0]["Credit"]);
                        if (debit != 0)
                        {
                            txt_cashbalance.Text = dt1.Rows[0]["Debit"].ToString();
                        }
                        else if (credit != 0)
                        {

                            txt_cashbalance.Text = dt1.Rows[0]["Credit"].ToString();


                        }



                    }
                    else
                    {
                        txt_cashbalance.Text = "0";

                    }

                }
            }
            else if (txt_accledger_id.Text != "")
            {
                using (DataTable dt1 = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                      ("Select Isnull(Debit,0) as Debit,isnull(Credit,0) as Credit  FROM ACC.LedgerOpnForADay(" + txt_accledger_id.Text + ",'" + Dtp_dt.Value.AddDays(1).ToString("dd-MMM-yyyy") + "','" + chkwt.Checked.ToString() + "', " + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                {
                    if (dt1.Rows.Count > 0)
                    {
                        debit = Convert.ToDouble(dt1.Rows[0]["Debit"]);
                        credit = Convert.ToDouble(dt1.Rows[0]["Credit"]);
                        if (debit != 0)
                        {
                            txt_cashbalance.Text = dt1.Rows[0]["Debit"].ToString();
                        }
                        else if (credit != 0)
                        {

                            txt_cashbalance.Text = dt1.Rows[0]["Credit"].ToString();


                        }



                    }
                    else
                    {
                        txt_cashbalance.Text = "0";

                    }

                }
            }


        }

        private void ItemDetails()
        {
            if (flag)
                return;

            if (txtcompId.Text == "")
                return;
            if (CmbLedgercode.Text != "" && CmbLedgercode.SelectedValue != null)
            {
                Double debit = 0, credit = 0;
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                             ("SELECT [Ledger Name],Acc_Category_ID,[Ledger Code] FROM ACC.VLedgerMaster WHERE [Ledger Code] ='" + CmbLedgercode.Text + "'")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        Cmb_ledgerhead.Text = (dt.Rows[0]["Ledger Name"] == DBNull.Value ? "" : dt.Rows[0]["Ledger Name"].ToString());
                        txtcatid.Text = (dt.Rows[0]["Acc_Category_ID"] == DBNull.Value ? "" : dt.Rows[0]["Acc_Category_ID"].ToString());
                        if ((txtcatid.Text == "1") || (txtcatid.Text == "2"))
                        {
                            using (DataTable dt1 = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                      ("Select Isnull(Debit,0) as Debit,isnull(Credit,0) as Credit  FROM ACC.LedgerOpnForADay(" + CmbLedgercode.SelectedValue + ",'" + Dtp_dt.Value.AddDays(1).ToString("dd-MMM-yyyy") + "','" + chkwt.Checked.ToString() + "', " + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])

                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    debit = Convert.ToDouble(dt1.Rows[0]["Debit"]);
                                    credit = Convert.ToDouble(dt1.Rows[0]["Credit"]);
                                    if (debit != 0)
                                    {
                                        txtcreditDebit.Text = dt1.Rows[0]["Debit"].ToString();
                                        label16.Text = "Dr";


                                    }
                                    else if (credit != 0)
                                    {

                                        txtcreditDebit.Text = dt1.Rows[0]["Credit"].ToString();
                                        label16.Text = "Cr";

                                    }
                                    else
                                    {
                                        txtcreditDebit.Text = "0";
                                        label16.Text = "Dr/Cr";
                                    }


                                }

                            }
                            cashinHand();

                        }
                        else
                        {
                            using (DataTable dt1 = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                       ("Select Isnull(Debit,0) as Debit,isnull(Credit,0) as Credit  FROM ACC.LedgerOpnForTrialBalance(" + CmbLedgercode.SelectedValue + ",'" + Dtp_dt.Value.AddDays(1).ToString("dd-MMM-yyyy") + "','" + Dtp_dt.Value.ToString("dd-MMM-yyyy") + "','" + chkwt.Checked.ToString() + "', " + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                            {
                                if (dt1.Rows.Count > 0)
                                {
                                    debit = Convert.ToDouble(dt1.Rows[0]["Debit"]);
                                    credit = Convert.ToDouble(dt1.Rows[0]["Credit"]);
                                    if (debit != 0)
                                    {
                                        txtcreditDebit.Text = dt1.Rows[0]["Debit"].ToString();
                                        label16.Text = "Dr";

                                    }
                                    else if (credit != 0)
                                    {
                                        txtcreditDebit.Text = dt1.Rows[0]["Credit"].ToString();
                                        label16.Text = "Cr";

                                    }
                                    else
                                    {
                                        txtcreditDebit.Text = "0";
                                        label16.Text = "Dr/Cr";

                                    }


                                }
                                cashinHand();

                            }

                        }

                    }

                }

            }
            else
            {
                Cmb_ledgerhead.Text = "";
                txtcreditDebit.Text = "0";

            }
        }

        private void Rb_Payment_CheckedChanged(object sender, EventArgs e)
        {
            TransactionType();
        }

        private void Rb_Receipt_CheckedChanged(object sender, EventArgs e)
        {
            TransactionType();
        }


        private void Rb_BankPay_CheckedChanged(object sender, EventArgs e)
        {
            TransactionType();
        }


        private void Rb_BankRecpt_CheckedChanged(object sender, EventArgs e)
        {
            TransactionType();
        }


        private void grbRadioButton2_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void Rb_Cheque_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb_Cheque.Checked == true)
            {
                cmb_BankAccId.AcceptBlankValue = false;
                txtChequeNo.AcceptBlankValue = false;
                dtpchequeDate.AcceptBlankValue = false;
                //cmb_BankId.AcceptBlankValue = false;
                cmb_BankAccId.Enabled = true;
            }
            else
            {
                cmb_BankAccId.AcceptBlankValue = true;
                txtChequeNo.AcceptBlankValue = true;
                dtpchequeDate.AcceptBlankValue = true;
                cmb_BankId.AcceptBlankValue = true;
            }
        }

        private void populategrid()
        {

        }

        private void AdjustColumWidth()
        {

        }

        private void Cmb_LedgerType_TextChanged(object sender, EventArgs e)
        {

        }

        private void Cmb_ledgerhead_KeyDown(object sender, KeyEventArgs e)
        {



        }
        public override bool Delete()
        {

            string str = "select Date as entrydate from ACC.CashTransactionEntry where TransId = " + txtEntryNo.Text;

            //if (SAFA.Classes.Common.ChkPermission(txtUserName.Text, Gramboo.GeneralConfig.UserId, this.Text, Convert.ToDateTime(Dtp_dt.Text), Convert.ToInt32(txtBranchID.Text), Convert.ToInt32(txtcompId.Text), 3, str) == false)
            //{ return false; }

            if (base.Delete())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Save()
        {



            DialogResult d =
               Gramboo.General.ShowMessage(

               " You Want to Perform This Action ? \n\n" +
               " 1. Press 'Yes' to Save \n" +
               " 2. Press 'No' to Cancel \n"
               , "Save", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

            if (d == DialogResult.No)
            {
                return false;
            }


            else
            {
                BankId = (cmb_BankAccId.SelectedValue == null ? "0" : cmb_BankAccId.SelectedValue.ToString());
                if (base.Save())
                {


                    //using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                    //      ("SELECT * FROM SALE.AddCustomer( '" +TxtSaleId.Text+ "'," +txtcompId.Text+ "," +txtBranchID.Text+ ")")).Tables[0])

                    //using (DataTable dt = DBConn.GetData(new SqlCommand("SELECT * FROM SALE.AddCustomer( '" + TxtSaleId.Text + "'," + txtcompId.Text + "," + txtBranchID.Text + ")")).Tables[0])
                    //if (chk_newCustomer.Checked == true)
                    //{

                    //    SqlCommand cmd = new SqlCommand();
                    //    cmd.CommandText = "SALE.AddCustomer";
                    //    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.AddWithValue("@SalesId", TxtSaleId.Text);
                    //    cmd.Parameters.AddWithValue("@CompanyId", txtcompId.Text);
                    //    cmd.Parameters.AddWithValue("@BranchId", txtBranchID.Text);

                    //    DBConn.ExecuteSqlTransaction(cmd, "AddCustomer");
                    //}

                    //if (SalesReturn != null && txt_ReturnAmount.Text != "0")
                    //{
                    //    if (SalesReturn.Save(false))
                    //    {

                    //        DBConn.ExecuteSqlTransaction(new SqlCommand("Update SALE.SalesMaster Set ReturnId=" + SalesReturn.TxtSaleReturnId.Text + " WHERE SalesId=" + TxtSaleId.Text), "UpdateReturnId");

                    //    }
                    //    else
                    //    {
                    //        return false;
                    //    }
                    //}
                    //CreditCardBill();





                    //if (OldGold != null && txt_OldGoldReceipt.Text != "0")
                    //{

                    //    if (OldGold.Save(false))
                    //    {
                    //        DBConn.ExecuteSqlTransaction(new SqlCommand("Update SALE.SalesMaster Set OldgoldId=" + OldGold.TxtEntryNo.Text + " WHERE SalesId=" + TxtSaleId.Text), "UpdateOldId");
                    //        DBConn.ExecuteSqlTransaction(new SqlCommand("Update SALE.OldGoldReciptMaster Set RecFrom='S' WHERE SalesId=" + TxtSaleId.Text), "UpdateOld");
                    //        if (chk_newCustomer.Checked)
                    //            DBConn.ExecuteSqlTransaction(new SqlCommand("Update t1 set t1.CustID=t2.CustID from SALE.OldGoldReciptMaster t1,SALE.SalesMaster t2 WHERE t1.EntryId=t2.OldgoldId and   t1.EntryId=" + OldGold.TxtEntryNo.Text), "UpdateOld");

                    //        if (d == DialogResult.Yes)
                    //        {
                    //            Print();
                    //        }

                    //        Init();
                    //        return true;

                    //    }
                    //    else
                    //    {
                    //        return false;
                    //    }

                    //}
                    if (d == DialogResult.Yes)
                    {

                        DialogResult d1 =
              Gramboo.General.ShowMessage(

              "Do You Want Print? \n"
              , "Print", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                        if (d1 == DialogResult.Yes)
                        {
                            Print();
                        }
                    }
                    if (Message == true)
                    {
                        //if (SoftwareSettings.IsFile)
                        //{
                        //    DialogResult R = Gramboo.General.ShowMessage("Do you have any files to upload?", "Upload", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                        //    if (R == DialogResult.Yes)
                        //    {
                        //        SAFA.Forms.COM.DocumentUploader du = new SAFA.Forms.COM.DocumentUploader();
                        //        du.CloseClick += new SAFA.Forms.COM.DocumentUploader.CloseClickEventHandler(Closed);
                        //        du.RecTable = "ACC.CashTransactionEntry";
                        //        du.RecId = txtEntryNo.Text;
                        //        du.ShowDialog();
                        //        return false;
                        //    }
                        //}

                    }
                    Message = true;
                    return true;

                }


                else
                {
                    return false;
                }
            }
        }
        //public void Closed(object sender, SAFA.Forms.COM.DocumentUploader.CloseClickEventArgs e)
        //{
        //    Init();
        //}
        public override void Print()
        {
            //base.Print();
            //if (txtOne_Two.Text == "1")
            //{
            //    //SAFA.Classes.DotMarixPrinting.SalesInvoice(Convert.ToInt64(TxtSaleId.Text), false,true);

            //    //base.RefreshData();
            //    CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //    cr = new SAFA.Reports.ACC.RptCashTransactionEntry();
            //    SAFA.Classes.Common.SetDatabaseLogon(cr, DBConn, false, true);
            //    cr.VerifyDatabase();
            //    //cr.SetDatabaseLogon(DBConn.ConnectionProperties.DBUsername, DBConn.ConnectionProperties.DBPassword);
            //    //cr.VerifyDatabase();
            //    //cr.Subreports[0].VerifyDatabase();
            //    cr.RecordSelectionFormula = "{VCashTransactionPrint.TransID}=" + txtEntryNo.Text;
            //    //cr.SetParameterValue("@salesid", TxtSaleId.Text);
            //    //cr.SetParameterValue("@companyid", txtcompId.Text);
            //    //cr.SetParameterValue("@branchid", txtBranchID.Text);
            //    //cr.PrintToPrinter(1, false, 1, 1);

            //    //Gramboo.Controls.GrbReport rpt = new Gramboo.Controls.GrbReport(cr);
            //    //rpt.MdiParent = this.MdiParent;
            //    //((frmMain)this.MdiParent).ShowChild(rpt);
            //    cr.PrintToPrinter(1, false, 0, 0);
            //}
        }
        private void Cmb_ledgerhead_TextChanged(object sender, EventArgs e)
        {
            //if (!flagload)
            //    LoadLedgerHead();

        }

        private void Cmb_ledgerhead_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("+{TAB}");
            }
        }

        private void cmb_BankAccId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_Group_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_Group_SelectedValueChanged(object sender, EventArgs e)
        {
            // cmb_Group.Text = "";
            //if (!flagload)
            //LoadLedgerHead();
        }

        private void cmb_Group_TextChanged(object sender, EventArgs e)
        {

        }


        private void frmCashTransactionEntryNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtBranchID.Text != "")
            {
                //if (SoftwareSettings.TxType)
                //{
                //    if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
                //    {
                //        txtOne_Two.Visible = true;
                //        txtOne_Two.Focus();
                //    }
                //}
            }

            if (e.KeyCode == Keys.Escape)
            {
                panel1.Visible = false;
                PanBillClosing.Visible = false;
                linkLabel1.Text = "Group Bill Closing";
            }
        }

        private void txtcatgId_TextChanged(object sender, EventArgs e)
        {
            // categorydetails();
        }

        private void txtcatid_TextChanged(object sender, EventArgs e)
        {

        }

        private void CmbLedgercode_SelectedValueChanged(object sender, EventArgs e)
        {
            if (flag)
                return;
            if (this.ActiveControl == CmbLedgercode && CmbLedgercode.SelectedValue != null)
            {
                Cmb_ledgerhead.SelectedValue = CmbLedgercode.SelectedValue;

                ItemDetails();
            }
        }

        private void CmbLedgercode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
                return;
            if (this.ActiveControl == CmbLedgercode && CmbLedgercode.SelectedValue != null)
            {
                Cmb_ledgerhead.SelectedValue = CmbLedgercode.SelectedValue;

                ItemDetails();
            }
            //Cmb_ledgerhead.Enabled = false;
        }

        private void Cmb_ledgerhead_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag)
                return;
            if (this.ActiveControl == Cmb_ledgerhead && Cmb_ledgerhead.SelectedValue != null)
            {
                CmbLedgercode.SelectedValue = Cmb_ledgerhead.SelectedValue;

                ItemDetails();
            }

            // CmbLedgercode.Enabled = false;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            //    SAFA.Forms.ACC.LedgerSearch frm = new LedgerSearch();
            //    frm.Parentcontrol = CmbLedgercode;
            //    frm.ParentForm = this;
            //    frm.MdiParent = this.ParentForm;
            //    InitializeTables();
            //    if (!EditMode)
            //        GenerateID(this.TableName);
            //    ((frmMain)this.ParentForm).ShowChild(frm);
            //    frm.Focus();
        }
        private void closing()
        {

            if (flag)
                return;
            Double opening = 0, paid = 0, closing = 0, cashbalance = 0;
            opening = Convert.ToDouble(txtcreditDebit.Text == "" ? "0" : txtcreditDebit.Text);
            if (chk_advance.Checked == true)
            {
                paid = Convert.ToDouble(grbTextBox1.Text == "" ? "0" : grbTextBox1.Text);
            }
            else {
                paid = Convert.ToDouble(txtAmount.Text == "" ? "0" : txtAmount.Text);
            }

            if (label16.Text == "Cr")
            {
                opening = -opening;
            }



            cashinHand();
            cashbalance = Convert.ToDouble(txt_cashbalance.Text);
            if (Rb_Payment.Checked == true || Rb_BankPay.Checked == true)
            {
                closing = opening + paid;
                txt_closing.Text = closing.ToString("f2");
                cashbalance -= paid;
            }
            else if (label16.Text == "Dr" && (Rb_Receipt.Checked == true || Rb_BankRecpt.Checked == true))
            {
                closing = opening - paid;
                txt_closing.Text = closing.ToString("f2");
                cashbalance += paid;

            }
            else if (label16.Text == "Cr" && (Rb_Receipt.Checked == true || Rb_BankRecpt.Checked == true))
            {
                closing = opening - paid;
                txt_closing.Text = closing.ToString("f2");
                cashbalance += paid;
            }
            else if (label16.Text == "Cr" && (Rb_Payment.Checked == true || Rb_BankPay.Checked == true))
            {
                closing = opening + paid;
                txt_closing.Text = closing.ToString("f2");
                cashbalance -= paid;
            }
            else if ((label16.Text == "Dr/Cr" || label16.Text == "Dr. Cr") && (Rb_Payment.Checked == true || Rb_BankPay.Checked == true))
            {
                closing = -1 * paid;
                txt_closing.Text = closing.ToString("f2");
                cashbalance -= paid;
            }
            else if ((label16.Text == "Dr/Cr" || label16.Text == "Dr. Cr") && (Rb_Receipt.Checked == true || Rb_BankRecpt.Checked == true))
            {
                closing = paid;
                txt_closing.Text = closing.ToString("f2");
                cashbalance += paid;
            }

            txt_cashbalance.Text = cashbalance.ToString("f2");
        }
        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (flag)
                return;
            if (txtAmount.Text != "")
            {
                closing();
                if (chk_advance.Checked == true)
                {
                    calcutax();
                }
            }
        }

        private void label16_TextChanged(object sender, EventArgs e)
        {
            if (flag)
                return;
            if (txtAmount.Text != "")
            {
                closing();
            }
        }

        private void txtcreditDebit_TextChanged(object sender, EventArgs e)
        {
            if (flag)
                return;
            if (txtAmount.Text != "")
            {
                closing();
            }
        }

        private void cmb_BankAccId_SelectedValueChanged(object sender, EventArgs e)
        {
            if (flag)
                return;
            if (cmb_BankAccId.SelectedValue != null)
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                ("SELECT Acc_LedgerId FROM ACC.BankAccountMaster WHERE Acc_BankAccId ='" + cmb_BankAccId.SelectedValue + "'")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        txt_accledger_id.Text = (dt.Rows[0]["Acc_LedgerId"] == DBNull.Value ? "" : dt.Rows[0]["Acc_LedgerId"].ToString());
                    }
                    else
                    {
                        txt_accledger_id.Text = "";
                    }
                    cashinHand();
                }

            }
            else
            {
                txt_accledger_id.Text = "";
            }
            // cashinHand();
        }

        private void CashTransactionEntry_Load(object sender, EventArgs e)
        {
            Dtp_dt.MaxDate = DateTime.Now;
        }

        private void txtOne_Two_TextChanged(object sender, EventArgs e)
        {
            //if (Rb_Payment.Checked)
            //{

            //    lblCaption.Text = Rb_Payment.Text;
            //    if (txtAmount.Text != "")
            //    {
            //        closing();
            //    }
            //    cmb_BankAccId.Enabled = false;
            //    if (txtOne_Two.Text == "1" || txtOne_Two.Text == "3")
            //    {

            //        cmb_VoucherTypeId.SelectedValue = 50;
            //        VoucherType = 50;
            //    }
            //    else if (txtOne_Two.Text == "2" )
            //    {
            //        cmb_VoucherTypeId.SelectedValue = 139;
            //        VoucherType = 139;
            //    }
            //    cmb_BankAccId.Enabled = false;
            //    if (txtcompId.Text != "")
            //        RefreshData();
            // }
            //    if (Rb_Receipt.Checked)
            //    {
            //        lblCaption.Text = Rb_Receipt.Text;
            //        this.BackColor = Color.MistyRose;
            //        if (txtOne_Two.Text == "1" || txtOne_Two.Text == "3")
            //        {
            //            cmb_VoucherTypeId.SelectedValue = 51;
            //            VoucherType = 51;
            //        }
            //        else if (txtOne_Two.Text == "2" )
            //        {
            //            cmb_VoucherTypeId.SelectedValue = 140;
            //            VoucherType = 140;
            //        }
            //        if (txtAmount.Text != "")
            //        {
            //            closing();
            //        }
            //        cmb_BankAccId.Enabled = false;
            //        if (txtcompId.Text != "")
            //            RefreshData();
            //    }
            // if (Rb_BankPay.Checked)
            // {
            //     lblCaption.Text = Rb_BankPay.Text;
            //     if (txtOne_Two.Text == "1" || txtOne_Two.Text == "3")
            //     {
            //         cmb_VoucherTypeId.SelectedValue = 52;
            //         VoucherType = 52;
            //     }
            //     else
            //     {
            //          cmb_VoucherTypeId.SelectedValue = 141;
            //          VoucherType = 141;
            //     }
            //     if (txtAmount.Text != "")
            //     {
            //         closing();
            //     }
            //     cmb_BankAccId.Enabled = true;

            //     if (txtcompId.Text != "")
            //         RefreshData();
            // }
            // if (Rb_BankRecpt.Checked)
            //{
            //     lblCaption.Text = Rb_BankRecpt.Text;
            //     if (txtOne_Two.Text == "1" || txtOne_Two.Text == "3")
            //     {
            //         cmb_VoucherTypeId.SelectedValue = 53;
            //         VoucherType = 53;
            //     }
            //     else
            //     {
            //         cmb_VoucherTypeId.SelectedValue = 142;
            //          VoucherType = 142;
            //     }

            //     if (txtAmount.Text != "")
            //     {
            //         closing();
            //     }
            //     cmb_BankAccId.Enabled = true;

            //     if (txtcompId.Text != "")
            //         RefreshData();
            // }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void cmbtds_SelectedValueChanged(object sender, EventArgs e)
        {

            if (chk_advance.Checked = true)
            {

                if (cmbtds.SelectedValue == null || cmbtds.Text == "")
                {
                    cmbtds.SelectedValue = 0;
                    TXT_TDSPerc.Text = "";
                    return;
                }
                if (cmbtds.SelectedValue != null && cmbtds.Text != "")
                {
                    //    TXT_TDSPerc.Text = (DBConn.GetData(new System.Data.SqlClient.SqlCommand("select TdsRate FROM GEN.TDSMaster  where TdsId='" + cmbtds.SelectedValue + "'  ")).Tables[0].Rows[0]["TdsRate"]).ToString();
                    //}
                    //else
                    //{
                    //    TXT_TDSPerc.Clear();
                    //}
                    Double othind, ind, nopan;
                    ind = Convert.ToDouble(DBConn.GetData(new SqlCommand("select top 1 Individual FROM GEN.TDSDetails  where TdsId='" + cmbtds.SelectedValue + "' and date<='" + Dtp_dt.Value + "' order by date desc")).Tables[0].Rows[0][0]);
                    othind = Convert.ToDouble(DBConn.GetData(new SqlCommand("select top 1 Otherthanindividual FROM GEN.TDSDetails  where TdsId='" + cmbtds.SelectedValue + "' and date<='" + Dtp_dt.Value + "' order by date desc")).Tables[0].Rows[0][0]);
                    nopan = Convert.ToDouble(DBConn.GetData(new SqlCommand("select top 1 NoPanno FROM GEN.TDSDetails  where TdsId='" + cmbtds.SelectedValue + "' and date<='" + Dtp_dt.Value + "' order by date desc")).Tables[0].Rows[0][0]);
                    using (DataTable dt = DBConn.GetData(new SqlCommand("select * from  [ACC].[VLedgerMaster]  WHERE Acc_LedgerId=" + Cmb_ledgerhead.SelectedValue + " and HasTDS=1 AND HasPan=0"), "tbl").Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (cmbtds.Text != "")
                            {
                                TXT_TDSPerc.Text = nopan.ToString("f2");
                                calcutax();
                            }
                            else if (txt_tdsamt.Text != "")
                            {
                                TXT_TDSPerc.Text = "0";
                                calcutax();
                            }
                        }
                    }
                    using (DataTable dt = DBConn.GetData(new SqlCommand("select * from  [ACC].[VLedgerMaster]  WHERE Acc_LedgerId=" + Cmb_ledgerhead.SelectedValue + " and HasTDS=1 AND HasPan=1 AND IsIndividual=1"), "tbl").Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (cmbtds.Text != "")
                            {
                                TXT_TDSPerc.Text = ind.ToString("f2");
                                calcutax();
                            }
                            if (txt_tdsamt.Text == "0")
                            {
                                TXT_TDSPerc.Text = "0";
                                calcutax();
                            }
                        }
                    }
                    using (DataTable dt = DBConn.GetData(new SqlCommand("select * from  [ACC].[VLedgerMaster]  WHERE Acc_LedgerId=" + Cmb_ledgerhead.SelectedValue + " and HasTDS=1 AND HasPan=1 AND IsIndividual=0"), "tbl").Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            if (cmbtds.Text != "")
                            {
                                TXT_TDSPerc.Text = othind.ToString("f2");
                                calcutax();
                            }
                            else if (txt_tdsamt.Text != "")
                            {
                                TXT_TDSPerc.Text = "0";
                                calcutax();
                            }
                        }
                    }
                }
            }
            msgval = true;
        }
        void b()
        {

            if (Cmb_ledgerhead.SelectedValue != null)//&& Cmb_LedgerType.Text != "")
            {

                if (cmbtds.Text == "")
                {
                    TXT_TDSPerc.Clear();
                }


                using (DataTable de = DBConn.GetData(new SqlCommand("select HasTDS from  [ACC].[VLedgerMaster]  WHERE Acc_LedgerId=" + Cmb_ledgerhead.SelectedValue + " and HasTDS=1"), "tbl").Tables[0])
                {
                    if (de.Rows.Count > 0)
                    {
                        cmbtds.SelectedValueChanged -= cmbtds_SelectedValueChanged;
                        Gramboo.General.Setupcombo(cmbtds, "GEN.TDSMaster ", "TdsName", "TdsId", "IsActive='True'  AND  Branch_id=" + txtBranchID.Text);
                        cmbtds.SelectedValueChanged += cmbtds_SelectedValueChanged;
                    }
                    else
                    {
                        cmbtds.DataSource = null;
                        TXT_TDSPerc.Text = "";
                    }
                    //if (de.Rows.Count > 0 && dt.Rows.Count > 0)
                    //{
                    //    cmbsgst.SelectedIndex = -1;
                    //    CmbCGST.SelectedIndex = -1;
                    //    CmbIGST.SelectedIndex = -1;
                    //    cmbtds.SelectedIndex = -1;
                    //    TxtTDSper.Text = "";
                    //}
                }
            }
        }
        private void calcutax()
        {

            Double perc = 0, cess = 0, totalperc = 0, tdsperc = 0, billamt = 0, totalcgst = 0, totalsgst = 0, totaligst = 0, totaltds = 0, totalatax = 0, totalataxamt = 0, sgstperc, igstperc, cgstperc;

            double amt = 0, amount = 0, TCSAmt = 0;
            Double TotAmt = 0, TCSPerc = 0;
            //perc = Convert.ToDouble(string.IsNullOrEmpty(txtRate_TaxDetails.Text.Trim()) ? "0.000" : txtRate_TaxDetails.Text);
            /// amt = Convert.ToDouble(dgv_TaxDetails.SummaryRow.SummaryCells["Amount"].Text == "" ? "0.000" : dgv_TaxDetails.SummaryRow.SummaryCells["Amount"].Text);
            // totalperc = Convert.ToDouble(dgv_TaxDetails.SummaryRow.SummaryCells["TaxRate"].Text == "" ? "0.000" : dgv_TaxDetails.SummaryRow.SummaryCells["TaxRate"].Text);
            tdsperc = Convert.ToDouble(string.IsNullOrEmpty(TXT_TDSPerc.Text.Trim()) ? "0.000" : TXT_TDSPerc.Text);
            amt = Convert.ToDouble(string.IsNullOrEmpty(txtAmount.Text.Trim()) ? "0" : txtAmount.Text);
            amount = amt - editamt;
            // cess = Convert.ToDouble(string.IsNullOrEmpty(txtcessamt.Text.Trim()) ? "0.000" : txtcessamt.Text);
            //if (cmbtds.SelectedValue != null && Cmb_ledgerhead.SelectedValue != null)
            //{
            //    using (DataTable dt1 = DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select GEN.TDSCalculation('" + FA.Common.GetFiscalStart(DBConn, DateTime.Now.Date, Convert.ToInt32(GeneralConfig.CompanyID)) + "','" + FA.Common.GetFiscalEnd(DBConn, DateTime.Now.Date, Convert.ToInt32(GeneralConfig.CompanyID)) + "'," + GeneralConfig.CompanyID + "," + GeneralConfig.BranchId + "," + Cmb_ledgerhead.SelectedValue + ",'" + Dtp_dt.Value + "','" + amount + "'," + cmbtds.SelectedValue + ",'CTE')")).Tables[0])
            //    {
            //        if (dt1.Rows.Count > 0)
            //        {
            //            txttax.Text = dt1.Rows[0][0].ToString();
            //        }
            //    }

            //}
            //else
            //{
            txttax.Text = "0";

            //}
            billamt = Convert.ToDouble(txttax.Text);
            //billamt = SAFA.Classes.Common.calculatetax(DBConn, Convert.ToInt64(txtAmount.Text), txtcompId.Text.ToString(), txtBranchID.Text.ToString(), cmbtds.SelectedValue.ToString(), Dtp_dt.Value.ToString(),dtpfrom,dtpto, Cmb_ledgerhead.SelectedValue.ToString());
            //sgstperc = Convert.ToDouble(string.IsNullOrEmpty(txt_sgstper.Text.Trim()) ? "0.000" : txt_sgstper.Text);
            //igstperc = Convert.ToDouble(string.IsNullOrEmpty(txt_igstper.Text.Trim()) ? "0.000" : txt_igstper.Text);
            //cgstperc = Convert.ToDouble(string.IsNullOrEmpty(txt_cgstper.Text.Trim()) ? "0.000" : txt_cgstper.Text);
            //totalcgst = (billamt * (cgstperc / 100));
            //totalsgst = (billamt * (sgstperc / 100));
            //totaligst = (billamt * (igstperc / 100));
            totaltds = (billamt * (tdsperc / 100));

            //grbTextBox4.Text = totalcgst.ToString("f2");
            //txt_sgstamt.Text = totalsgst.ToString("f2");
            //txt_igstamt.Text = totaligst.ToString("f2");
            txt_tdsamt.Text = totaltds.ToString("f2");
            //totalataxamt = (totalcgst + totalsgst + totaligst);
            //txtTaxAmnt.Text = totalataxamt.ToString("f2");
            //totalatax = (cgstperc + sgstperc + igstperc);
            // totalataxamt = (amt);
            // totalatax = (totalperc);
            //txtTaxRatePerc.Text = totalatax.ToString("f2");
            grbTextBox1.Text = (amt - totaltds).ToString("f2");

            TotAmt = Convert.ToDouble((string.IsNullOrEmpty(grbTextBox1.Text.Trim()) ? "0.00" : grbTextBox1.Text));
            TCSPerc = Convert.ToDouble((string.IsNullOrEmpty(TxtTCSPerc.Text.Trim()) ? "0.00" : TxtTCSPerc.Text));
            TCSAmt = (TotAmt) * TCSPerc / 100;
            txt_tcsamt.Text = TCSAmt.ToString("F0");
            grbTextBox1.Text = (TotAmt + TCSAmt).ToString("f2");
            //txt_totalamt.Text = (billamt + totalataxamt).ToString("f2");
            if ((totaltds > (amt * (tdsperc / 100))) && msgval == true)
            {
                Gramboo.General.ShowMessage("TDS will be deducted for the previous transaction");
                msgval = false;
            }




        }

        private void CashTransactionEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            BankId = null;
        }

        private void txtOne_Two_TextChanged_1(object sender, EventArgs e)
        {
            if (Rb_Payment.Checked)
            {

                lblCaption.Text = Rb_Payment.Text;
                if (txtAmount.Text != "")
                {
                    closing();
                }
                cmb_BankAccId.Enabled = false;
                if (txtOne_Two.Text == "1" || txtOne_Two.Text == "3")
                {

                    cmb_VoucherTypeId.SelectedValue = 50;
                    VoucherType = 50;
                }
                else if (txtOne_Two.Text == "2")
                {
                    cmb_VoucherTypeId.SelectedValue = 139;
                    VoucherType = 139;
                }
                cmb_BankAccId.Enabled = false;
                //if (txtcompId.Text != "")
                //    RefreshData();
            }
            if (Rb_Receipt.Checked)
            {
                lblCaption.Text = Rb_Receipt.Text;
                //this.BackColor = Color.MistyRose;
                if (txtOne_Two.Text == "1" || txtOne_Two.Text == "3")
                {
                    cmb_VoucherTypeId.SelectedValue = 51;
                    VoucherType = 51;
                }
                else if (txtOne_Two.Text == "2")
                {
                    cmb_VoucherTypeId.SelectedValue = 140;
                    VoucherType = 140;
                }
                if (txtAmount.Text != "")
                {
                    closing();
                }
                cmb_BankAccId.Enabled = false;
                //if (txtcompId.Text != "")
                //    RefreshData();
            }
        }

        //private void CmbLedgercode_TextChanged(object sender, EventArgs e)
        //{
        //    if(CmbLedgercode.SelectedValue == null)
        //    {
        //        Cmb_ledgerhead.SelectedValue = -1;
        //    }
        //}

        private void TXT_TDSPerc_TextChanged(object sender, EventArgs e)
        {
            if (cmbtds.Text != "")
            {
                calcutax();
            }
            else
            {
                TXT_TDSPerc.Clear();
            }
        }

        private void CmbBillNoFromBillEntry_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbBillNo.SelectedValue == null)
            {
                txtAmount.Text = "";
                TxtValiBillAmt.Text = "";
                return;
            }
            if (Cmb_ledgerhead.SelectedValue != null && CmbBillNo.SelectedValue.ToString() != "0")
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select BalanceAmount FROM ACC.FunPendingBillNo('" + Cmb_ledgerhead.SelectedValue + "'," + txtcompId.Text + "," + txtBranchID.Text + " )where BillId='" + CmbBillNo.SelectedValue + "' and IsActive='True'"), "Tbl").Tables[0])


                    if (dt.Rows.Count > 0)
                    {
                        txtAmount.Text = dt.Rows[0]["BalanceAmount"].ToString();
                        TxtValiBillAmt.Text = dt.Rows[0]["BalanceAmount"].ToString();
                    }
            }
        }

        private void cmbtds_TextChanged(object sender, EventArgs e)
        {
            if (cmbtds.SelectedValue == null || cmbtds.Text == "")
            {
                cmbtds.SelectedValue = 0;
                calcutax();
                TXT_TDSPerc.Text = "";
                txt_tdsamt.Text = "";
                return;
            }

        }

        private void LinkLabelBillDetailes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LinkLabelBillDetailes.Text == "Remove")
            {
                CmbBillNo.Text = "";
                CmbBillNo.SelectedValue = 0;
                CmbBillNo.Enabled = true;
                GetAdvanceBill(Convert.ToInt64(Cmb_ledgerhead.SelectedValue));
                LinkLabelBillDetailes.Text = "Bill Details";
                txtAmount.Text = "";
                return;
            }

            if (panel1.Visible == false && rbSingle.Checked == true)
            {
                panel1.BringToFront();
                panel1.Size = new System.Drawing.Size(600, 250);
                panel1.Visible = true;
                panel1.Parent = this;
                panel1.Location = new Point(529, 247);
                panel1.Show();
                panel1.BringToFront();
                PopulateGrid();
                LinkLabelBillDetailes.Text = "Hide Bill Details ";
            }
            else
            {
                panel1.Visible = false;
                panel1.SendToBack();
                panel1.Hide();
                LinkLabelBillDetailes.Text = "Bill Details";
            }

        }

        private void chk_advance_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_advance.Checked == true)
            {

                b();

                closing();
                cmbtds.Enabled = true;
                TXT_TDSPerc.Enabled = true;
                TXT_TDSPerc.ReadOnly = true;
                txt_tdsamt.ReadOnly = true;
                grbTextBox1.Enabled = true;
                grbTextBox1.ReadOnly = false;
                CmbBillNo.SelectedValue = -1;
                CmbBillNo.Enabled = false;
                CmbBillNo.SelectedValue = 0;
                CmbBillNo.Text = "";
                grbRadioButtonGroup1.Value = "";

            }
            else
            {

                cmbtds.DataSource = null;
                TXT_TDSPerc.Text = "";
                txt_tdsamt.Text = "";
                grbTextBox1.Text = "";
                cmbtds.Enabled = false;
                TXT_TDSPerc.Enabled = false;
                TXT_TDSPerc.ReadOnly = false;
                txt_tdsamt.ReadOnly = false;
                grbTextBox1.ReadOnly = true;
                grbTextBox1.Enabled = false;
                CmbBillNo.Enabled = true;
                closing();
            }
        }

        private void rbGroup_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbGroup.Checked == true)

            //{
            //    if (panalAdvanceBill.Visible == false)
            //    {
            //        panalAdvanceBill.BringToFront();
            //        panalAdvanceBill.Size = new System.Drawing.Size(500, 250);
            //        panalAdvanceBill.Visible = true;

            //        panalAdvanceBill.Parent = this;

            //        panalAdvanceBill.Location = new Point(LinkLabelBillDetailes.Location.X + LinkLabelBillDetailes.Parent.Location.X,
            //       panalAdvanceBill.Parent.Location.Y + LinkLabelBillDetailes.Location.Y - panel1.Height);
            //        panalAdvanceBill.Show();
            //        panalAdvanceBill.BringToFront();
            //        BillDetails();
            //        txtAmount.Text = "0";
            //        CmbBillNoFromBillEntry.Text = " ";
            //       // PopulateGrid();
            //        //LinkLabelBillDetailes.Text = "Hide Bill Details ";

            //    }
            //}
            //else
            //{
            //    panalAdvanceBill.Visible = false;
            //    panalAdvanceBill.SendToBack();
            //    panalAdvanceBill.Hide();
            //   // LinkLabelBillDetailes.Text = "Bill Details";
            //}
        }

        private void rbSingle_CheckedChanged(object sender, EventArgs e)
        {
            GetAdvanceBill(Convert.ToInt64(Cmb_ledgerhead.SelectedValue));
        }

        private void Cmb_ledgerhead_SelectedValueChanged(object sender, EventArgs e)
        {
            if (chk_advance.Checked == true)
            {
                b();
            }

            GetAdvanceBill(Convert.ToInt64(Cmb_ledgerhead.SelectedValue));
        }

        private void grbButton3_Click(object sender, EventArgs e)
        {
            string BillId = "-", Amount = "0";
            if (ClsId > 2)
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                            ("select ClsBillId,ClsAmount from ACC.CashTransactionBillClosing where TransId=" + txtEntryNo.Text + " and ClsId=" + ClsId + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        BillId = dt.Rows[0]["ClsBillId"].ToString();
                        Amount = dt.Rows[0]["ClsAmount"].ToString();
                    }
                }

                if (BillId.ToString().Trim().Length > 2)
                {
                    using (DataTable dt = DBConn.GetData(new SqlCommand("Select Isnull(BalanceAmount,0) as BalanceAmount FROM ACC.FunPendingBillNo('" + Cmb_ledgerhead.SelectedValue + "'," + txtcompId.Text + "," + txtBranchID.Text + " )where BillId='" + BillId + "' and IsActive='True'"), "Tbl").Tables[0])

                        if (dt.Rows.Count > 0)
                        {
                            Double a = Convert.ToDouble(dt.Rows[0]["BalanceAmount"].ToString());
                            TxtClsValiAmt.Text = Convert.ToString((Math.Round(Convert.ToDouble(Amount), 2) + Math.Round(a, 2)));
                        }
                        else { TxtClsValiAmt.Text = Amount; }
                    if (TxtClsValiAmt.Text == "0") { TxtClsValiAmt.Text = Amount; }
                }
            }


            //if (Math.Round(Convert.ToDouble(TxtClsValiAmt.Text), 2) - Math.Round(Convert.ToDouble(TxtClsAmount.Text), 2) < 0)
            //{
            //    String A = Convert.ToString(Math.Abs(Math.Round(Convert.ToDouble(TxtClsValiAmt.Text), 2) - Math.Round(Convert.ToDouble(TxtClsAmount.Text), 2)));
            //    TxtClsAmount.ShowMessage("you have entered " + A + " ₹ greater than your bill amount");
            //    return;
            //}

            TxtRateCutting.Text = CmbRateCutting.SelectedIndex.ToString();

            if (CmbClsBillNo.SelectedValue != null)
            {
                TxtClsBillId.Text = CmbClsBillNo.SelectedValue.ToString();
                dgv_advanceBill.Save();
            }
            txtAmount.Text = dgv_advanceBill.SummaryRow.SummaryCells["ClsAmount"].Text;
            BillDetails();

            TxtRate.Text = TxtMetalRate.Text;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (PanBillClosing.Visible == false && rbGroup.Checked == true)
            {
                if (PanBillClosing.Visible == false)
                {
                    if (TxtMetalRate.Text == "0") { TxtMetalRate.ShowMessage("Blank Values Not Allowed...!!"); return; }
                    if (TxtMetPurity.Text == "0") { TxtMetPurity.ShowMessage("Blank Values Not Allowed...!!"); return; }
                    PanBillClosing.BringToFront();
                    PanBillClosing.Size = new System.Drawing.Size(731, 250);
                    PanBillClosing.Visible = true;
                    PanBillClosing.Parent = this;
                    PanBillClosing.Location = new Point(510, 247);
                    PanBillClosing.Show();
                    PanBillClosing.BringToFront();
                    BillDetails();
                    CmbBillNo.Text = ""; TxtRate.Text = TxtMetalRate.Text;
                    linkLabel1.Text = "Hide Bill Closing";
                }
            }
            else
            {
                PanBillClosing.Visible = false;
                PanBillClosing.SendToBack();
                PanBillClosing.Hide();
                linkLabel1.Text = "Group Bill Closing";
            }
        }

        private void dgv_BillDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgv_BillDetails.Rows[rowIndex];
            CmbBillNo.SelectedValue = row.Cells["BillId"].Value.ToString();
            txtAmount.Text = row.Cells["BalanceAmount"].Value.ToString();

            panel1.Visible = false;
            panel1.SendToBack();
            panel1.Hide();
            LinkLabelBillDetailes.Text = "Bill Details";
        }

        private void CmbClsBillNo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbClsBillNo.SelectedValue == null)
            {
                TxtClsAmount.Text = "";
                TxtClsValiAmt.Text = "";
                return;
            }
            if (Cmb_ledgerhead.SelectedValue != null && CmbClsBillNo.SelectedValue.ToString() != "0")
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select BalanceAmount,NonweightAmount,[Rate Cutting],GoldRate FROM ACC.FunPendingBillNo('" + Cmb_ledgerhead.SelectedValue + "'," + txtcompId.Text + "," + txtBranchID.Text + " )where BillId='" + CmbClsBillNo.SelectedValue + "'and IsActive='True'"), "Tbl").Tables[0])

                    if (dt.Rows.Count > 0)
                    {
                        TxtClsAmount.Text = dt.Rows[0]["BalanceAmount"].ToString();
                        TxtClsValiAmt.Text = dt.Rows[0]["BalanceAmount"].ToString();
                        TxtNonWTAmount.Text = dt.Rows[0]["NonweightAmount"].ToString();
                        CmbRateCutting.SelectedIndex = Convert.ToInt32(dt.Rows[0]["Rate Cutting"].ToString());
                        if (CmbRateCutting.SelectedIndex == 1) { TxtRate.Text = TxtMetalRate.Text; }
                        else { TxtRate.Text = dt.Rows[0]["GoldRate"].ToString(); }
                    }
            }
        }

        private void BtnDocAdd_Click(object sender, EventArgs e)
        {
            //SAFA.Forms.COM.DocumentUploader du = new SAFA.Forms.COM.DocumentUploader();
            //du.CloseClick += new SAFA.Forms.COM.DocumentUploader.CloseClickEventHandler(DocBtn);
            //du.RecTable = "ACC.CashTransactionEntry";
            //du.RecId = txtEntryNo.Text;
            //du.ShowDialog();
        }
        //public void DocBtn(object sender, SAFA.Forms.COM.DocumentUploader.CloseClickEventArgs e)
        //{
        //    DocButtonChange();
        //}
        //void DocButtonChange()
        //{
        //    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
        //                                      ("select DocId From GEN.DocumentMaster where RecTable='ACC.CashTransactionEntry'and RecId=" + txtEntryNo.Text + " and Company_Id=" + txtcompId.Text + " and Branch_Id=" + txtBranchID.Text + "")).Tables[0])
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
                                              ("select Isnull(TCSId,0) as TCSId ,Isnull(TCSPerc,0) as TCSPerc,Isnull(TCSAmount,0) as TCSAmount from [ACC].[CashTransactionEntry] where TransId=" + txtEntryNo.Text + " and Company_Id=" + txtcompId.Text + " and Branch_Id=" + txtBranchID.Text + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    CmbTCS.SelectedValue = dt.Rows[0]["TCSId"].ToString();
                    TxtTCSPerc.Text = dt.Rows[0]["TCSPerc"].ToString();
                    txt_tcsamt.Text = dt.Rows[0]["TCSAmount"].ToString();
                }
            }
        }
        private void dgv_advanceBill_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgv_advanceBill.Rows[rowIndex];
            try
            {
                ClsId = Convert.ToInt64(row.Cells["ClsId"].Value == null ? "0" : row.Cells["ClsId"].Value.ToString());
                if (ClsId != 0)
                {
                    string str;
                    SqlCommand cmd = new SqlCommand();
                    str = "select t2.BillId,t2.BillNo From ACC.CashTransactionBillClosing as t1 inner join ACC.VPendingBillNo as t2 on t1.ClsBillId=t2.BillId where t1.TransId=" + txtEntryNo.Text + " and t1.ClsId=" + ClsId + "";
                    cmd.CommandText = str;
                    CmbClsBillNo.DisplayMember = "BillNo";
                    CmbClsBillNo.ValueMember = "BillId";
                    cmd.CommandTimeout = 0;
                    CmbClsBillNo.DataSource = DBConn.GetData(cmd, "BillNo").Tables[0];
                }
            }
            catch (Exception ex) { }
        }

        void GetAdvanceBill(Int64 LedgerId)
        {
            string Type = "";
            if (Rb_Payment.Checked == true || Rb_BankPay.Checked == true) { Type = "P"; } else if (Rb_Receipt.Checked == true || Rb_BankRecpt.Checked == true) { Type = "R"; }
            string str;
            SqlCommand cmd = new SqlCommand();
            if (Cmb_ledgerhead.SelectedValue != null && rbSingle.Checked == true)
            {
                str = "Select BillId,BillNo from ACC.FunPendingBillNo('" + LedgerId + "'," + txtcompId.Text + "," + txtBranchID.Text + "  ) where Type='" + Type + "' and IsActive='True'";
                cmd.CommandText = str;
                CmbBillNo.DisplayMember = "BillNo";
                CmbBillNo.ValueMember = "BillId";
                cmd.CommandTimeout = 0;
                CmbBillNo.DataSource = DBConn.GetData(cmd, "BillNo").Tables[0];

                CmbBillNo.SelectedValue = 0;
                CmbBillNo.Text = "";
            }
            else
            {
                CmbBillNo.Text = "";
                CmbBillNo.DataSource = null;
            }
        }

        private void CmbTCS_SelectedValueChanged(object sender, EventArgs e)
        {
            TCSRate();
        }

        private void CmbTCS_TextChanged(object sender, EventArgs e)
        {
            TCSRate();
        }

        private void CmbLedgercode_Leave(object sender, EventArgs e)
        {
            if (flag)
                return;
            ItemDetails();
            Cmb_ledgerhead.SelectedValue = 0;
            if (this.ActiveControl == CmbLedgercode && CmbLedgercode.SelectedValue != null)
            {
                Cmb_ledgerhead.SelectedValue = CmbLedgercode.SelectedValue;
            }
            ItemDetails();
        }


        //group bill setting
        void BillDetails()
        {
            string Type = "";
            if (RbtTransType.Value.Contains("P")) { Type = "P"; } else if (RbtTransType.Value.Contains("R")) { Type = "R"; }
            string str;
            SqlCommand cmd = new SqlCommand();
            if (Cmb_ledgerhead.SelectedValue != null)
            {
                str = "Select BillId,BillNo from ACC.FunPendingBillNo('" + Cmb_ledgerhead.SelectedValue + "'," + txtcompId.Text + "," + txtBranchID.Text + "  ) where Type='" + Type + "' and IsActive='True'";
                cmd.CommandText = str;
                CmbClsBillNo.DisplayMember = "BillNo";
                CmbClsBillNo.ValueMember = "BillId";
                CmbClsBillNo.DataSource = DBConn.GetData(cmd, "BillNo").Tables[0];

                CmbClsBillNo.SelectedValue = 0;
                CmbClsBillNo.Text = "";
            }
            else
            {
                CmbClsBillNo.Text = "";
            }
            TxtClsAmount.Text = "";
        }

        private void TxtClsAmount_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == TxtClsAmount)
            {
                Calc_WT();
            }
        }

        void Calc_WT()
        {
            double ClsAmt = 0, NonWtAmt = 0, Wt = 0, Rate = 0;

            if (TxtClsAmount.Text != "")
            {
                ClsAmt = Convert.ToDouble(TxtClsAmount.Text);
                NonWtAmt = Convert.ToDouble(TxtNonWTAmount.Text);

                if (TxtRate.Text != "0")
                {
                    Rate = Convert.ToDouble(TxtRate.Text);
                    Wt = (ClsAmt - NonWtAmt) / Rate;
                }

                if (Wt > 0) { TxtWt.Text = Math.Round(Wt, 3).ToString(); }
                else { TxtWt.Text = "0"; }
            }
        }

        void Calc_AMT()
        {
            double PaymentAmt = 0, NonWtAmt = 0, Wt = 0, Rate = 0;

            Wt = Convert.ToDouble(TxtWt.Text);
            NonWtAmt = Convert.ToDouble(TxtNonWTAmount.Text);

            if (TxtRate.Text != "0")
            {
                Rate = Convert.ToDouble(TxtRate.Text);
                PaymentAmt = Math.Round(Rate * Wt, 2);
            }
            TxtClsAmount.Text = (PaymentAmt + NonWtAmt).ToString("f2");
        }

        private void TxtMetalRate_TextChanged(object sender, EventArgs e)
        {
    
        }

        private void TxtRate_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == TxtRate)
            {
                Calc_WT(); 
            }
        }

        private void TxtWt_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == TxtWt)
            {
                Calc_AMT();
            }
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            List();
        }

        private void grbButton2_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void PopulateGrid()
        {
            string Type = "";
            if (RbtTransType.Value.Contains("P")) { Type = "P"; } else if (RbtTransType.Value.Contains("R")) { Type = "R"; }
            dgv_BillDetails.AutoGenerateColumns = true;
            dgv_BillDetails.ShowSerialNo = true;
            dgv_BillDetails.SummaryColumns = new string[] { "BillAmount", "BalanceAmount" };
            dgv_BillDetails.HiddenDataFields = new List<string>() { "BillId", "RecFromShort","IsActive" };
            dgv_BillDetails.DataSource = this.DBConn.GetData(new SqlCommand("Select * from [ACC].[FunPendingBillNo]('" + Cmb_ledgerhead.SelectedValue + "'," + txtcompId.Text + "," + txtBranchID.Text + "  ) where Type='" + Type + "' and IsActive='True'")).Tables[0];
        }

        public override bool FillData(Dictionary<string, object> PrimaryValues)
        {
            if (base.FillData(PrimaryValues))
            {
                Message = false;
                BtnDocAdd.Visible = true;
                //DocButtonChange();
                TCS();
                Double closingamt=0,openingamt=0;
                string BillId = "-", Amount = "0"; Int64 LedgerId = 0;
                if (IsEditMode)
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                ("SELECT VchNo,Tdsid,VoucherTypeId,ledgerhead,Isnull(BillId,'-') as BillId,Amount  FROM ACC.CashTransactionEntry WHERE TransId =" + (txtEntryNo.Text == null ? "0" : txtEntryNo.Text.ToString()) + "")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            cmbtds.SelectedValue = (dt.Rows[0]["Tdsid"] == DBNull.Value ? "0" : dt.Rows[0]["Tdsid"].ToString());
                            TxtVoucherNo.Text = (dt.Rows[0]["VchNo"] == DBNull.Value ? "0" : dt.Rows[0]["VchNo"].ToString());
                            cmb_VoucherTypeId.SelectedValue = (dt.Rows[0]["VoucherTypeId"] == DBNull.Value ? "0" : dt.Rows[0]["VoucherTypeId"].ToString());
                            LedgerId = Convert.ToInt64(dt.Rows[0]["ledgerhead"].ToString());
                            BillId = dt.Rows[0]["BillId"].ToString();
                            Amount = dt.Rows[0]["Amount"].ToString();
                                                        

                        }
                    }
                    editamt = Convert.ToDouble(Amount);
                    calcutax();
                    closingamt = Convert.ToDouble(txt_closing.Text);
                    txt_closing.Text = (closingamt - editamt).ToString();
                    openingamt = Convert.ToDouble(txtcreditDebit.Text);
                    txtcreditDebit.Text = (openingamt - editamt).ToString();



                if (BillId.ToString().Trim().Length > 2)
                {
                    CmbBillNo.Enabled = false;

                    string str;
                    SqlCommand cmd = new SqlCommand();
                    str = "select t2.BillId,t2.BillNo From ACC.CashTransactionEntry as t1 inner join ACC.VPendingBillNo as t2 on t1.BillId=t2.BillId where t1.TransId=" + txtEntryNo.Text + "";
                    cmd.CommandText = str;
                    CmbBillNo.DisplayMember = "BillNo";
                    CmbBillNo.ValueMember = "BillId";
                    cmd.CommandTimeout = 0;
                    CmbBillNo.DataSource = DBConn.GetData(cmd, "BillNo").Tables[0];
                    txtAmount.Text = Amount;

                    using (DataTable dt = DBConn.GetData(new SqlCommand("Select Isnull(BalanceAmount,0) as BalanceAmount FROM ACC.FunPendingBillNo('" + LedgerId + "'," + txtcompId.Text + "," + txtBranchID.Text + " )where BillId='" + BillId + "' and IsActive='True'"), "Tbl").Tables[0])

                        if (dt.Rows.Count > 0)
                        {
                            Double a = Convert.ToDouble(dt.Rows[0]["BalanceAmount"].ToString());
                            TxtValiBillAmt.Text = Convert.ToString((Math.Round(Convert.ToDouble(Amount),2)+Math.Round(a,2)));
                        }
                        else { TxtValiBillAmt.Text = Amount; }
                    if (TxtValiBillAmt.Text == "0") { TxtValiBillAmt.Text = Amount; }

                    LinkLabelBillDetailes.Text = "Remove";
                }
                else { GetAdvanceBill(LedgerId); }
                return true;
            }
            else
            {
                Message = true;
                return false;
            }
        }

        void TransactionType()
        {
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            if (Cmb_ledgerhead.SelectedValue != null)
            {
                GetAdvanceBill(Convert.ToInt64(Cmb_ledgerhead.SelectedValue));
            }
            if (Rb_Payment.Checked)
            {
                lblCaption.Text = Rb_Payment.Text;
                cmb_BankAccId.Enabled = false;
                if (txtOne_Two.Text == "1" || txtOne_Two.Text == "3" || txtOne_Two.Text == "")
                {
                    cmb_VoucherTypeId.SelectedValue = 50;
                    VoucherType = 50;
                }
                else
                {
                    cmb_VoucherTypeId.SelectedValue = 139;
                    VoucherType = 139;
                }
            }

            else if (Rb_BankPay.Checked)
            {
                lblCaption.Text = Rb_BankPay.Text;
                cmb_VoucherTypeId.SelectedValue = 52;
                cmb_BankAccId.Enabled = true;
                VoucherType = 52;
            }

            else if (Rb_Receipt.Checked)
            {
                lblCaption.Text = Rb_Receipt.Text;

                if (txtOne_Two.Text == "1" || txtOne_Two.Text == "3" || txtOne_Two.Text == "")
                {
                    cmb_VoucherTypeId.SelectedValue = 51;
                    VoucherType = 51;
                }
                else
                {
                    cmb_VoucherTypeId.SelectedValue = 140;
                    VoucherType = 140;
                }
                cmb_BankAccId.Enabled = false;
            }

            else if (Rb_BankRecpt.Checked)
            {
                lblCaption.Text = Rb_BankRecpt.Text;

                cmb_VoucherTypeId.SelectedValue = 53;

                cmb_BankAccId.Enabled = true;
                VoucherType = 53;
            }

            if (txtAmount.Text != "")
            {
                closing();
            }
            if (txtcompId.Text =="" && txtBranchID.Text =="" )
                return;
            if (!IsEditMode)
                TxtVoucherNo.Text = SAFA.Classes.Common.GetNextVoucherNo(VoucherType, Dtp_dt.Value,
            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
        }

        public void TCSRate()
        {
            TxtTCSPerc.Clear(); TCSAmount();
            if (Cmb_ledgerhead.SelectedValue == null)
                return;
            if (CmbTCS.Text == "")
                return;
            if (CmbTCS.SelectedValue != null && CmbTCS.SelectedValue.ToString().Trim().Length > 3)
            {
                Double RecvRate = 0.00, othind = 0.00, ind = 0.00, nopan = 0.00;
                using (DataTable dt = DBConn.GetData(new SqlCommand("select top 1 ReceivableRate,Otherthanindividual,Individual,NoPanno FROM GEN.TCSDetails  where TcsId='" + CmbTCS.SelectedValue + "' and date<='" + Dtp_dt.Value + "'order by date desc")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        ind = Convert.ToDouble(dt.Rows[0]["Individual"].ToString());
                        othind = Convert.ToDouble(dt.Rows[0]["Otherthanindividual"].ToString());
                        nopan = Convert.ToDouble(dt.Rows[0]["NoPanno"].ToString());
                        RecvRate = Convert.ToDouble(dt.Rows[0]["ReceivableRate"].ToString());
                    }
                }
                String HasTds = "", HasPan = "", IsInd = "";
                using (DataTable dt = DBConn.GetData(new SqlCommand("select isnull(HasTDS,'False') as HasTDS,isnull(HasPan,'False') as HasPan,isnull(IsIndividual,'False') as IsIndividual from ACC.LedgerMaster WHERE Acc_LedgerID=" + Cmb_ledgerhead.SelectedValue.ToString() + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        HasPan = dt.Rows[0]["HasPan"].ToString();
                        IsInd = dt.Rows[0]["IsIndividual"].ToString();
                        if (Rb_Payment.Checked == true || Rb_BankPay.Checked == true)
                        {
                            if (HasPan == "False")
                            {
                                TxtTCSPerc.Text = nopan.ToString("F2");
                            }
                            else if (HasPan == "True" && IsInd == "True")
                            {
                                TxtTCSPerc.Text = ind.ToString("F2");
                            }
                            else if (HasPan == "True" && IsInd == "False")
                            {
                                TxtTCSPerc.Text = othind.ToString("F2");
                            }
                        }
                        else if(Rb_Receipt.Checked==true||Rb_BankRecpt.Checked==true)
                        {
                            TxtTCSPerc.Text = RecvRate.ToString("F2");
                        }
                        TCSAmount();
                    }
                }
            }
        }

        private void TCSAmount()
        {
            Double TotAmt = 0, TCSPerc = 0, TCSAmt = 0;
            TotAmt = Convert.ToDouble((string.IsNullOrEmpty(grbTextBox1.Text.Trim()) ? "0.00" : grbTextBox1.Text));
            TCSPerc = Convert.ToDouble((string.IsNullOrEmpty(TxtTCSPerc.Text.Trim()) ? "0.00" : TxtTCSPerc.Text));
            TCSAmt = (TotAmt) * TCSPerc / 100;
            txt_tcsamt.Text = TCSAmt.ToString("F0");
            calcutax();
        }

    }
}
