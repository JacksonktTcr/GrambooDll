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
using SAFA.Classes;
using Gramboo.Controls;

namespace SAFA.Forms.SALE
{
   
    public partial class FrmSchemePaymentEntry : Gramboo.Controls .GrbForm 
    {
        private DateTime JoinDate;
            int instAmt;
            bool sendsms;
            long paymentId;
        bool flag; 
        public FrmSchemePaymentEntry SP;
        public String Remark;
        private static FrmSchemePaymentEntry instance;
        string CustPano,SchemeId;
        Double OldWt;

        private static string  SchemeName;

        public static FrmSchemePaymentEntry Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FrmSchemePaymentEntry();
                }
                else if (instance.IsDisposed)
                {
                    instance = new FrmSchemePaymentEntry();
                }
                return instance;
            }
        }        
      

        public FrmSchemePaymentEntry()
        {
            InitializeComponent();
            txtCash.TextChanged += new EventHandler(CalcTotalAmount);
            txtCard .TextChanged += new EventHandler(CalcTotalAmount);
            txtrtgs.TextChanged += new EventHandler(CalcTotalAmount);
            txt_OldGoldReceipt.TextChanged += new EventHandler(CalcTotalAmount);

            txtCash.TextChanged += new EventHandler(CalcTotalWt);
            txtCard.TextChanged += new EventHandler(CalcTotalWt);
            txtrtgs.TextChanged += new EventHandler(CalcTotalWt); 
            txt_OldGoldReceipt.TextChanged += new EventHandler(CalcTotalWt); 
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
            Table t = new Table(SAFA.Classes.Common.DbName, "SALE", "SavingSchemePaymentEntry");
            t.PrimaryKeys.Add("PaymentId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = txtPaymentId;
            this.TableName = t;
            return true;
        }


        public override void Init()
        {
            base.Init();
            grbPrint.Value = "D";
            TxtIsactive.Text = "1";
            txtCash.AcceptBlankValue = false;
            AdjustColumnsWidth();
            Cmb_SchemeNo.Focus(); 
           if (Dtp_VchDate.Value.ToString() != null || txtcompId.Text != null)
           {
               txtgoldrate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + Dtp_VchDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString();

           }

          
        }

        public override void RefreshData()
        {
            base.RefreshData();
            // GetGoldRate();
            // populate_Grid();

             Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            cmb_VoucherTypeId.SelectedValue = 18;
            if (!IsEditMode)
                TxtVchNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_VchDate.Value,
             DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            //     Gramboo.General.Setupcombo(Cmb_JoinName, "SALE.SavingSchemeJoinEntry", "JoinNo", "JoinId", "IsActive='True' AND  Company_id=" + txtcompId.Text + " AND Branch_Id=" + txtBranchID.Text);
            flag = true;
            Gramboo.General.Setupcombo(CmbArea, "GEN.AreaMaster", "AreaName", "AreaId", "IsActive='True'");
            Gramboo.General.Setupcombo(Cmb_SchemeNo, "SALE.PendingSchemeNo", "JoinNo", "JoinId", "Company_id=" + txtcompId.Text + " and Branch_Id=" + txtBranchID.Text);
            //Gramboo.General.Setupcombo(Cmb_SchemeNo, "SALE.SchemeJoiningEntry", "SchemeNo", "JoiningId", "IsActive='True' AND JoiningId NOT IN (SELECT JoinId FROM SALE.SchemeCancellationEntry ) AND Company_id=" + txtcompId.Text + " AND Branch_Id=" + txtBranchID.Text);
            Gramboo.General.Setupcombo(CmbCollAgent, "EMP.EmployeeMaster", "EmpName", "EmpId", "IsCollectionAgent='True'");
            flag = false;
            
             PopulateGrid();
             Gramboo.General.Setupcombo(cmb_state, "GEN.StateCodeMaster", "StateName", "StateID");
            if (Remark != null)
            {
                TxtRemarks.Text = Remark;
            }
        }

        private void totPrePaidamt()
        {

        }

    
        public override bool Save()
        {

            if (base.Save())
            {

                Remark = TxtRemarks.Text;
                if (sendsms )
                {
                    Classes.Common.SubmitSMS(DBConn, "Payment", paymentId, Convert.ToInt32(txtcompId.Text));
                }
                      
                if (ShowMessage("Do you want to print  ? ", "Print", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Print();
                    RefreshData();
                }
                 
                return true;

            }
            else
            {
                return false;

            }
        }



        private void PopulateGrid()
        {

            if (Cmb_SchemeNo.SelectedValue != null &!flag )
            {

                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand

            ("SELECT  *  FROM SALE.VSchemeAllPaymentDetails  WHERE JoinId=" + Cmb_SchemeNo.SelectedValue + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {


                        dgv.ShowDelete = false;
                        dgv.ShowEdit = false;
                        dgv.AutoGenerateColumns = true;
                        dgv.ShowSerialNo = true;
                        dgv.SummaryColumns = new string[] { "Payment Amount", "Gold Wt", "CGST", "SGST", "IGST", "Amount", "OGWT" };
                        dgv.HiddenDataFields = new List<string>() { "JoinId", "GST" , "CGST", "SGST", "IGST"};// [Gold Rate], SALE.SavingSchemePaymentEntry.GoldWt AS [Gold Wt]
                        dgv.DataSource = this.DBConn.GetData(new SqlCommand("Select  JoinId,[Payment Way],[Payment No],[Payment Date],[Payment Amount],[Gold Rate],[Gold Wt],CGST,SGST,IGST,Amount,OGWT,Remarks from SALE.VSchemeAllPaymentDetails WHERE JoinId=" + Cmb_SchemeNo.SelectedValue + " order by [Payment Date] ASC ")).Tables[0];


                    }
                    else
                    {
                        dgv.DataSource = null;
                    }

                }
                AdjustColumnsWidth();
            }
            else
            {
                dgv.DataSource = null;
            }
        }


        public void AdjustColumnsWidth()
        {
            if (dgv.Columns.Contains("Payment Way"))
            {
                dgv.Columns["Payment Way"].Width = 145;
            }  
            if (dgv.Columns.Contains("Payment Date"))
            {
                dgv.Columns["Payment Date"].Width = 140;
            }
            if (dgv.Columns.Contains("Payment Amount"))
            {
                dgv.Columns["Payment Amount"].Width = 140;
            }

            if (dgv.Columns.Contains("Payment No"))
            {
                dgv.Columns["Payment No"].Width = 140;
            }
            if (dgv.Columns.Contains("Remarks"))
            {
                dgv.Columns["Remarks"].Width = 140;
            }

        }

        public override bool ValidateControls()
        {
            if (base.ValidateControls())
            {

                //if (dgv.Rows.Count > 0)
                //{
                //    if (JoinDate.AddDays(350) < Dtp_VchDate.Value.Date)
                //    {

                //        Cmb_SchemeNo.ShowMessage("Account Expired ... Cannot accept installment");
                //        return false;

                //    }
                //}

                if (Cmb_SchemeNo.SelectedValue == null)
                {
                    txtCash.AcceptBlankValue = false;
                    Cmb_SchemeNo.ShowMessage("Select Scheme number");
                    return false;
                }
                DateTime F_date=DateTime.Now, T_Date=DateTime.Now; double Amout = 0.00,OldAmout=00; 
                if(IsEditMode)
                {
                    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                   ("select PaymentAmount From SALE.SavingSchemePaymentEntry where PaymentId=" + txtPaymentId.Text + "")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {
                            OldAmout =Convert.ToDouble(dt.Rows[0]["PaymentAmount"].ToString());
                        }
                    }
                    Amout = Math.Round(Convert.ToDouble(txtPaymentAmount.Text),2)- Math.Round(OldAmout,2);
                }
                else
                {
                    Amout =Convert.ToDouble(txtPaymentAmount.Text);
                }

                SqlCommand cmd1 = new SqlCommand("Select SALE.SchemeValidatePanNo(" + Cmb_SchemeNo.SelectedValue.ToString() + ",'" + F_date + "','" + T_Date + "'," + Math.Round(Amout,2) + "," + txtcompId.Text + "," + txtBranchID.Text + ")");
                if (Convert.ToBoolean(DBConn.GetData(cmd1, "Val").Tables[0].Rows[0][0].ToString()))
                {
                    txtCustName.ShowMessage("Update Customer PanNumber");
                    return false;
                }

                GetWt();
                bool val=true ;
               
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "SALE.ValidateSavingScheme";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@JoinId", Cmb_SchemeNo.SelectedValue);
                    cmd.Parameters.AddWithValue("@Date", Dtp_VchDate.Value.ToString("dd-MMM-yyyy"));
                    cmd.Parameters.AddWithValue("@Amount", txtPaymentAmount.Text);



                    val = DBConn.ExecuteSqlTransaction(cmd, "validate");
                   // val = true ;
                    if (!IsEditMode)
                        TxtVchNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_VchDate.Value,
                     DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
                    paymentId = Convert.ToInt64( txtPaymentId.Text);
                    using (DataTable smsdt = DBConn.GetData(new SqlCommand("Select ISNULL(SendSms,'TRUE') FRom SALE.SavingSchemeJoiningEntry where joinID=" + Cmb_SchemeNo.SelectedValue), "tbl").Tables[0])
                    {
                        sendsms = Convert.ToBoolean( smsdt.Rows[0][0]);
                    }
                
                if (val)
                    return true;
                else
                    return false;
            }
            else
            {

                return false;
            }
        }
        public void SchemeDetails()
        {
            if (Cmb_SchemeNo.SelectedValue != null)
            {
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                ("Select [Customer Name],Address1,Address2,Address3,PhoneNo,[Scheme Name], JoinNo,[Join Date],[Scheme Type],[Benifit Type],[Benifit Amount] as [Benifit],[Instalment Amount],[No Of Instalment],[Total Amount], [Opening Balance],Convwt,StateName,[StateCode],isnull(CustPanNo,0) as CustPanNo,SchemeId,AreaId,AgentID FROM SALE.VSavingSchemeJoiningEntry WHERE JoinId='"
                + Cmb_SchemeNo.SelectedValue + "'")).Tables[0])
               // ("Select JoinId,[Scheme Name],[Scheme No],[Scheme Type],[Benefit Type],[Benefit Amount],[Installment Amount],[No Of Installments],[Total Amount], [Opening Balance] FROM SALE.VSchemeJoining WHERE JoinId='"
               // + Cmb_SchemeNo.SelectedValue + "'")).Tables[0])
                
                {
                    if (dt.Rows.Count > 0)
                    {
                        JoinDate = Convert.ToDateTime(dt.Rows[0]["Join Date"] == DBNull.Value ? "" : dt.Rows[0]["Join Date"].ToString());
                        txtCustName1.Text = (dt.Rows[0]["Customer Name"] == DBNull.Value ? "" : dt.Rows[0]["Customer Name"].ToString());
                        txtCustName.Text = (dt.Rows[0]["Customer Name"] == DBNull.Value ? "" : dt.Rows[0]["Customer Name"].ToString());                     
                        txtSchemeName.Text = (dt.Rows[0]["Scheme Name"] == DBNull.Value ? "" : dt.Rows[0]["Scheme Name"].ToString());
                        txtschemeType.Text = (dt.Rows[0]["Scheme Type"] == DBNull.Value ? "" : dt.Rows[0]["Scheme Type"].ToString());
                        CmbArea.SelectedValue = dt.Rows[0]["AreaId"].ToString();
                        CmbCollAgent.SelectedValue= dt.Rows[0]["AgentID"].ToString();
                        //txtBenifitType.Text = (dt.Rows[0]["Benifit Type"] == null ? "" : dt.Rows[0]["Benifit Type"].ToString());
                        //txtBenifit.Text = (dt.Rows[0]["Benifit"] == null ? "" : dt.Rows[0]["Benifit"].ToString());
                        TxtInstallmentAmount.Text = (dt.Rows[0]["Instalment Amount"] == null ? "" : dt.Rows[0]["Instalment Amount"].ToString());
                        txtTotalAmount.Text = (dt.Rows[0]["Total Amount"] == null ? "" : dt.Rows[0]["Total Amount"].ToString());
                        txtNoOfInstmt.Text = (dt.Rows[0]["No Of Instalment"] == null ? "" : dt.Rows[0]["No Of Instalment"].ToString());
                        //txtSalesAmount.Text = (dt.Rows[0]["Sales Amount"] == null ? "" : dt.Rows[0]["Sales Amount"].ToString());
                        txtOpeningBalance.Text = (dt.Rows[0]["Opening Balance"] == null ? "" : dt.Rows[0]["Opening Balance"].ToString());
                        txthouse.Text = dt.Rows[0]["Address1"] .ToString();
                        txtAddress1.Text = dt.Rows[0]["Address2"]. ToString();
                        txtAddress2.Text = dt.Rows[0]["Address3"]. ToString();
                        txtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
                        cmb_state.Text = dt.Rows[0]["StateName"].ToString();
                        txtstatecode.Text = dt.Rows[0]["StateCode"].ToString();
                        SchemeName = (dt.Rows[0]["Scheme Name"] == null ? "" : dt.Rows[0]["Scheme Name"].ToString());
                        chkConvWt.Checked = Convert.ToBoolean(dt.Rows[0]["ConvWt"] == null ? "false" : dt.Rows[0]["ConvWt"].ToString());
                        CustPano = dt.Rows[0]["CustPanNo"].ToString();
                        SchemeId = dt.Rows[0]["SchemeId"].ToString();
                    }

                    else
                    {
                        txtCustName1.Text = "";
                        txtCustName.Text = "";
                        txtSchemeName.Text = "";
                        txtschemeType.Text = "";
                        //txtBenifitType.Text = "";
                        //txtBenifit.Text = "";
                        TxtInstallmentAmount.Text = "";
                        txtTotalAmount.Text = "";
                        txtNoOfInstmt.Text = "";
                        //txtSalesAmount.Text = "";
                        txtOpeningBalance.Text = "";
                        txthouse.Text = "";
                        txtAddress1.Text = "";
                        txtAddress2.Text = "";
                        txtPhoneNo.Text = "";
                        cmb_state.Text = "";
                        txtstatecode.Text = "";
                        SchemeName = "";
                        TxtTotalWt.Text = "";
                        CustPano = string.Empty;
                        SchemeId = string.Empty;
                        txtbalance.Text = "";                        
                        txtpaidAmount.Text = "";
                        CmbArea.SelectedValue = 0;
                        CmbCollAgent.SelectedValue = 0;
                        CmbArea.Text = "";
                        CmbCollAgent.Text = "";
                     
                    }

                }
            }

            else
            {
                //TxtInstallmentAmount.Text = "";
                //txtbalance.Text = "";
                //txtBenifitType.Text = "";
                //txtschemeType.Text = "";
                //txtTotalAmount.Text = "";
                //txtpaidAmount.Text = "";
                //txtCustName1.Text = "";
                //txtNoOfInstmt.Text = "";
                ////txtSalesAmount.Text = "";
                //txtOpeningBalance.Text = "";

                txtCustName1.Text = "";
                txtCustName.Text = "";
                txtSchemeName.Text = "";
                txtschemeType.Text = "";
                //txtBenifitType.Text = "";
                //txtBenifit.Text = "";
                TxtInstallmentAmount.Text = "";
                txtTotalAmount.Text = "";
                txtNoOfInstmt.Text = "";
                //txtSalesAmount.Text = "";
                txtOpeningBalance.Text = "";
                txthouse.Text = "";
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtPhoneNo.Text = "";
                cmb_state.Text = "";
                txtstatecode.Text = "";
                SchemeName = "";
                TxtTotalWt.Text = "";
                CustPano = string.Empty;
                SchemeId = string.Empty;
                txtbalance.Text = "";
                txtpaidAmount.Text = "";
                CmbArea.SelectedValue = 0;
                CmbCollAgent.SelectedValue = 0;
                CmbArea.Text = "";
                CmbCollAgent.Text = "";

            }

        }

        private void Cmb_SchemeNo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!flag)
            {
                cmbmonth.Text = "";
                txtyear.Text = "";
                SchemeDetails();
                AdjustColumnsWidth();
                txtpaidAmount.Text = "";
                PopulateGrid();
               // monthandyear();
            }

        }


        private void monthandyear()
        {

            if (txtschemeType.Text == "Fixed")
            {

                DateTime joindate;
                float instAmt = 0,OpningBal=0;
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                ("SELECT  JoinDate,OpnBalance FROM SALE.SavingSchemeJoiningEntry WHERE JoinId='"
                + Cmb_SchemeNo.SelectedValue + "'")).Tables[0])
               // ("SELECT  joiningDate,OpningBalance FROM SALE .SchemeJoiningEntry WHERE JoiningId='"
                {
                  
                    if (dt.Rows.Count > 0)
                    {

                        DateTime pdate = Convert.ToDateTime(dt.Rows[0]["JoinDate"] == DBNull.Value ? "" : dt.Rows[0]["JoinDate"].ToString());
                        OpningBal = Convert.ToSingle(dt.Rows[0]["OpnBalance"] == DBNull.Value ? "" : dt.Rows[0]["OpnBalance"].ToString());
                        joindate = (Convert.ToDateTime(pdate.ToString("dd-MMM-yyyy")));
                                           
                        float paidAmt = Convert .ToSingle (txtpaidAmount.Text);
                        float Totpaid = paidAmt + OpningBal;
                        float instaAmt = Convert.ToSingle(TxtInstallmentAmount.Text);

                        int MonthAdd = Convert.ToInt32(Totpaid / instaAmt);
                        
                        String m = joindate.Month.ToString();
                        String y = joindate.Year.ToString();

                        int iMonthNo = Convert.ToInt32(m) + MonthAdd-1;
                        int iyear = Convert.ToInt32(y);

                        if(iMonthNo ==12)
                        {
                            iyear = iyear + 1;
                            iMonthNo = 1;
                            cmbmonth.SelectedIndex = iMonthNo;
                            txtyear.Text = iyear.ToString();
                        }

                        else if (iMonthNo >12)
                        {
                            iyear = iyear + 1;
                            iMonthNo = (iMonthNo-12)+1;
                            cmbmonth.SelectedIndex = iMonthNo;
                            txtyear.Text = iyear.ToString();
                        }

                        else
                        {
                            if (iMonthNo != 0)
                            {
                                iMonthNo = iMonthNo + 1;
                                cmbmonth.SelectedIndex = iMonthNo;
                                txtyear.Text = iyear.ToString();
                            }
                        }


                    }
                }
            }
        } 
        
 
        //public override bool ValidateControls()
        //{
        //    if (base.ValidateControls())
        //    {
        //        float totamount = 0, balance = 0,InstalmentAmt=0,paidamnt=0,currentTot=0;
        //        float salesAmt = 0, paymentAmt = 0;
        //        totamount = (Convert.ToSingle(txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text));
        //        balance = (Convert.ToSingle(txtbalance.Text == "" ? "0" : txtbalance.Text));
        //        paymentAmt = (Convert.ToSingle(txtPaymentAmount.Text == "" ? "0" : txtPaymentAmount.Text));
        //        InstalmentAmt = (Convert.ToSingle(TxtInstallmentAmount.Text == "" ? "0" : TxtInstallmentAmount.Text));
        //        paidamnt = (Convert.ToSingle(txtpaidAmount.Text == "" ? "0" : txtpaidAmount.Text));
        //        //salesAmt = (Convert.ToSingle(txtSalesAmount.Text == "" ? "0" : txtSalesAmount.Text));
        //        currentTot = paidamnt + paymentAmt;
        //        if (Cmb_SchemeNo.Text != "")
        //        {

        //            if (txtschemeType.Text == "Fixed")
        //            {
        //                DateTime paymentdate;
        //                String sDate = Dtp_VchDate.Value.Date.ToString("dd-MMM-yyyy");//ToString();;
        //                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

        //                String dy = datevalue.Day.ToString();
        //                String mn = datevalue.Month.ToString();
        //                String yy = datevalue.Year.ToString();

        //                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
        //     ("SELECT VchDate as [Payment Date] FROM SALE .SavingSchemePaymentEntry WHERE JoinId='"
        //     + Cmb_SchemeNo.SelectedValue + "'")).Tables[0])
        //                {
        //                    if (dt.Rows.Count > 0)
        //                    {
        //                        foreach (DataRow r in dt.Rows)
        //                        {
        //                            DateTime pdate = Convert.ToDateTime(r["Payment Date"] == DBNull.Value ? "" : r["Payment Date"].ToString());
        //                            paymentdate = (Convert.ToDateTime(pdate.ToString("dd-MMM-yyyy")));
        //                            String m = paymentdate.Month.ToString();
        //                            String y = paymentdate.Year.ToString();
        //                            if (y == yy)
        //                            {
        //                                if (m == mn)
        //                                {
        //                                    Dtp_VchDate.ShowMessage("The Payment Already did  for the Selected Month !! ");
        //                                    return false;
        //                                }

        //                            }

        //                        }

        //                    }

        //                }

        //                if (paymentAmt > InstalmentAmt || currentTot > totamount)
        //                {
        //                    if (paymentAmt > InstalmentAmt)
        //                    {
        //                        txtPaymentAmount.ShowMessage("The Payment Amount Exceeds The Maximum Monthly Installment Amount");
        //                        return false;
        //                    }
        //                    else if (currentTot > totamount)
        //                    {
        //                        txtPaymentAmount.ShowMessage("The Payment Amount Exceeds The Maximum Scheme Amount");
        //                        return false;
        //                    }
        //                }                     
        //                return true;
        //            }
        //            else if (txtschemeType.Text == "Variable")
        //            {
        //                if (paymentAmt > totamount || currentTot > totamount)
        //                {
        //                    txtPaymentAmount.ShowMessage("The Payment Amount Exceeds The Maximum Scheme Amount");
        //                    return false;
        //                }
        //                return true;
        //            }
        //             if (salesAmt != 0)
        //            {
        //                if (paymentAmt > balance)
        //                {
        //                    txtPaymentAmount.ShowMessage("The Payment Amount Exceeds The Maximum Scheme Amount");
        //                    return false;
        //                }
        //                return true;
        //            }
        //            return true;
        //        }
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}



        private void BALANCEAMOUNT()
        {
            
            float totAmt = 0, paidAmt = 0, Balance = 0, salesAmt = 0, OpenBalance = 0;
            using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
             ("SELECT SUM(PaymentAmount) as [Payment Amount]  FROM SALE .SavingSchemePaymentEntry WHERE JoinId='"
             + Cmb_SchemeNo.SelectedValue + "'")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    txtpaidAmount.Text = (dt.Rows[0]["Payment Amount"] == null ? "" : dt.Rows[0]["Payment Amount"].ToString());
                    paidAmt = Convert.ToSingle(txtpaidAmount.Text);
                }
            }
            OpenBalance = (Convert.ToSingle(txtOpeningBalance.Text == "" ? "0" : txtOpeningBalance.Text));
            totAmt = (Convert.ToSingle(txtTotalAmount.Text == "" ? "0" : txtTotalAmount.Text));
            //paidAmt = (Convert.ToSingle(txtpaidAmount.Text == "" ? "0" : txtpaidAmount.Text));

           // salesAmt = (Convert.ToSingle(txtSalesAmount.Text == "" ? "0" : txtSalesAmount.Text));
            if (totAmt != 0)
            {
                Balance = totAmt - (paidAmt + OpenBalance);
                txtbalance.Text = Balance.ToString();
            }
            //if (salesAmt != 0)
            //{
            //    Balance = salesAmt - (paidAmt + OpenBalance);
            //    txtbalance.Text = Balance.ToString();
            //}
        }
        private void Cmb_SchemeNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtpaidAmount.Text = "";
            SchemeDetails();
            //taxamt();
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // txtpaidAmount.Text = "";
           // SchemeDetails();
        }


        private void txtOpeningBalance_TextChanged(object sender, EventArgs e)
        {
            BALANCEAMOUNT();
        }




        public override void Print()
        {
            base.Print();

     
        }
      
        private void dgv_SummaryCalculated(object source, EventArgs e)
        {
            //txtpaidAmount.Text = "";
            //TxtTotalWt.Text = "";
            txtpaidAmount.Text = dgv.SummaryRow.SummaryCells["Payment Amount"].Text;
            TxtTotalWt.Text = dgv.SummaryRow.SummaryCells["Gold Wt"].Text;
            txttotamount.Text = dgv.SummaryRow.SummaryCells["Amount"].Text;
            txt_totgst.Text = dgv.SummaryRow.SummaryCells["GST"].Text;
        }


        private void txtgoldrate_TextChanged(object sender, EventArgs e)
        {
            GetWt();

        }

        private void GetWt()
        {

            if (chkConvWt.Checked == true)
            {

                float PaymentAmnt = 0, goldrate = 0, golwt = 0;// salesAmt = 0, OpenBalance = 0;

                PaymentAmnt = (Convert.ToSingle(txt_amt.Text == "" ? "0" : txt_amt.Text));
                goldrate = (Convert.ToSingle(txtgoldrate.Text == "" ? "" : txtgoldrate.Text));


                // salesAmt = (Convert.ToSingle(txtSalesAmount.Text == "" ? "0" : txtSalesAmount.Text));

                if (PaymentAmnt != 0 && goldrate != 0)
                {
                    golwt = PaymentAmnt / goldrate;
                    txtGoldWt.Text = golwt.ToString("f2");
                }
                else
                {
                    txtGoldWt.Text = "";
                }

            }
            else
            {
                txtGoldWt.Text = "0.00";
            }
        }


        private void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {

            //caltotalcash();
            //GetWt();
            //GST();
            //taxamt();
        }
        private void GST()
        {
            float PaymentAmnt = 0, GST = 0, TaxPerc=0;

            PaymentAmnt = (Convert.ToSingle(txtPaymentAmount.Text == "" ? "0" : txtPaymentAmount.Text));
            //goldrate = (Convert.ToSingle(txtgoldrate.Text == "" ? "" : txtgoldrate.Text));
            TaxPerc = 3;
            if (PaymentAmnt != 0)
            {
                GST = Convert.ToSingle(PaymentAmnt-(PaymentAmnt * 100 / (100 + TaxPerc)));
                txtgst.Text = GST.ToString("f2");
                txt_amt.Text = (PaymentAmnt - GST).ToString("f2");
            }
            else
            {
                txtgst.Text = "";
                txt_amt.Text = "";
            }
            taxamt();

        }
        public void taxamt()
        {
            double totaltax, gstamt;
          //  string company;
            //company = txtcompId.Text;
            gstamt = Convert.ToSingle(txtgst.Text);
            if (txtcompId.Text != "")
            {
               // using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
               //("SELECT t2.StateId, t2.Statecode FROM SYST.CompanyMaster t1,GEN.StateCodeMaster t2 WHERE t1.Statecode=t2.StateID And t1.Comp_id ='" + txtcompId.Text + "' AND t2.StateID=" + (cmb_state.SelectedValue == null ? "0" : cmb_state.SelectedValue))).Tables[0])
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                    ("select Comp_StateCode from SYST.BranchMaster where Branchid= " + txtBranchID .Text + "")).Tables[0]) 
                    if (dt.Rows.Count > 0)
                    {
                        if ((cmb_state.SelectedValue != null ? cmb_state.SelectedValue : "0").ToString() == dt.Rows[0]["Comp_StateCode"].ToString())
                        {
                            txt_igst.Text = "0";
                            txt_sgst.Text = (gstamt / 2).ToString("f2");
                            Txt_cgst.Text = (gstamt / 2).ToString("f2");
                        }

                        else
                        {
                            Txt_cgst.Text = "0";
                            txt_sgst.Text = "0";
                            txt_igst.Text = (gstamt).ToString("f2");
                        }
                    }
                
            }

            else
            {
                Txt_cgst.Text = "0";
                txt_sgst.Text = "0";
                txt_igst.Text = "0";
            }
        }
 //       public void loadcustomerdata()
 //       {
 //           string str = "SELECT t1.CustId,t1.CardNo,t1.CustName, t1.HouseName, t1.CustAddr1,t1.CustAddr2,t1.CustMob,t1.CustPhone" +
 //" FROM  CRM.vCustomerMaster t1 " +
 //"where isactive='true'  ";
 //           if (txt_custcardno.Text != "")
 //           {
 //               str += " AND CardNo like '" + txt_custcardno.Text + "%'";
 //           }
 //           if (grbTextBox1.Text != "")
 //           {
 //               str += " AND CustName like '" + grbTextBox1.Text + "%'";
 //           }
 //           if (TxtHouseName.Text != "")
 //           {
 //               str += " AND HouseName like '" + TxtHouseName.Text + "%'";
 //           }
 //           if (TxtAdd1.Text != "")
 //           {
 //               str += " AND CustAddr1 like '" + TxtAdd1.Text + "%'";
 //           }
 //           if (TxtAdd2.Text != "")
 //           {
 //               str += " AND  CustAddr2 like '" + TxtAdd2.Text + "%'";
 //           }

 //           if (TxtPhone.Text != "")
 //           {
 //               str += " AND  (CustPhone like '" + TxtPhone.Text + "%' or CustMob like '" + TxtPhone.Text + "%'  )";
 //           }

 //           SqlCommand cmd = new SqlCommand();
 //           cmd.CommandText = "SALE.CustomerSearch";
 //           cmd.CommandType = CommandType.StoredProcedure;
 //           cmd.Parameters.AddWithValue("@Date", Dtp_VchDate.Value.ToString("dd-MMM-yyyy"));
 //           cmd.Parameters.AddWithValue("@SearchQuery", str);
 //           cmd.Parameters.AddWithValue("@CardNo", (txt_custcardno.Text != "" ? txt_custcardno.Text : "0"));
 //           cmd.Parameters.AddWithValue("@CustName", (TxtCustName.Text != "" ? TxtCustName.Text : "0"));
 //           cmd.Parameters.AddWithValue("@HouseName", (TxtHouseName.Text != "" ? TxtHouseName.Text : "0"));
 //           cmd.Parameters.AddWithValue("@Address1", (TxtAdd1.Text != "" ? TxtAdd1.Text : "0"));
 //           cmd.Parameters.AddWithValue("@Address2", (TxtAdd2.Text != "" ? TxtAdd2.Text : "0"));
 //           cmd.Parameters.AddWithValue("@PhoneNo", (TxtPhone.Text != "" ? TxtPhone.Text : "0"));
 //           cmd.Parameters.AddWithValue("@CompanyId", (txtcompId.Text != "" ? txtcompId.Text : "0"));
 //           cmd.Parameters.AddWithValue("@BranchId", (txtBranchID.Text != "" ? txtBranchID.Text : "0"));

 //           dgvcust.DataFields = new List<string> { "CustId", "CardNo", "[Customer Name]", "HouseName", "Address1", "Address2", "MobileNo", "PhoneNo", "CardBalance", "CreditAmount" };
 //           dgvcust.HiddenDataFields = new List<string> { "CustId" };
 //           dgvcust.Columns["Address1"].Width = 150;
 //           dgv.SummaryColumns = new string[] { "Nos", "Wt", "DiaNo", "DiaWt", "StoneNo", "StoneWt", "NetWt" };
 //           dgvcust.DataSource = DBConn.GetData(cmd).Tables[0];

 //           dgvcust.Columns["Customer Name"].Width = 180;
 //           dgvcust.Columns["Address1"].Width = 280;
 //           dgvcust.Columns["HouseName"].Width = 100;
 //           dgvcust.Columns["Address2"].Width = 100;
 //       }
        private void CalcTotalAmount(object sender, EventArgs e)
        {
            float cash = 0, card = 0,RTGS=0,OGAmount=0;

            cash = Convert.ToSingle(txtCash.Text);
            card = Convert.ToSingle(txtCard.Text);
            RTGS = Convert.ToSingle(txtrtgs.Text);
            OGAmount = Convert.ToSingle(txt_OldGoldReceipt.Text);
            txtPaymentAmount.Text = (cash + card+RTGS+ OGAmount).ToString("f2");
        }
        private void CalcTotalWt(object sender, EventArgs e)
        {
            if (chkConvWt.Checked)
            {
                caloldwt();
                Double cash = 0, card = 0, RTGS = 0, GoldRate = 0, Wt = 0;
                cash = Convert.ToDouble(txtCash.Text);
                card = Convert.ToDouble(txtCard.Text);
                RTGS = Convert.ToDouble(txtrtgs.Text);
                GoldRate = Convert.ToDouble(txtgoldrate.Text);
                Wt = (cash + card + RTGS) / GoldRate;
                grbTextBox6.Text = (Wt + OldWt).ToString("F3");

                if (grbTextBox6.Text == "NaN")
                {
                    grbTextBox6.Text = "0.000";
                }
            }
            

        }
        private void txtCustName_TextChanged(object sender, EventArgs e)
        {

        }

        private void showlink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (custpanel.Visible == false)
            {

                custpanel.Parent = this;
                custpanel.Visible = true;
                custpanel.BringToFront();
                custpanel.Show();
                custpanel.BringToFront();
                Cmb_CustName.Enabled = false;
                txthouse.Enabled = false;
                txtAddress1.Enabled = false;
                txtAddress2.Enabled = false;
                txtPhoneNo.Enabled = false;
            }
            else
            {
                custpanel.Visible = false;
                custpanel.SendToBack();
                custpanel.Hide();
            }           
        }
        public void loadcustomerdata()
        {
            string str = " SELECT t1.JoinId,t1.CustName,t1.Address1,t1.Address2,t1.Address3,t1.MobileNo,t1.PhoneNo,t1.JoinNo,'' as CreditAmount " +
 " FROM  SALE.SavingSchemeJoiningEntry t1 " +
 "where IsActive='true'  ";

            if (grbTextBox1.Text != "")
            {
                str += " AND CustName like '" + grbTextBox1.Text + "%'";
            }
            if (TxtHouseName.Text != "")
            {
                str += " AND Address1 like '" + TxtHouseName.Text + "%'";
            }
            if (TxtAdd1.Text != "")
            {
                str += " AND Address2 like '" + TxtAdd1.Text + "%'";
            }
            if (TxtAdd2.Text != "")
            {
                str += " AND  Address3 like '" + TxtAdd2.Text + "%'";
            }

            if (TxtPhone.Text != "")
            {
                str += " AND  (PhoneNo like '" + TxtPhone.Text + "%' or MobileNo like '" + TxtPhone.Text + "%'  )";
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SALE.SchemeCustomerSearch";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Date", Dtp_VchDate.Value.ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@SearchQuery", str);
            //cmd.Parameters.AddWithValue("@CardNo", (txt_custcardno.Text != "" ? txt_custcardno.Text : "0"));
            //cmd.Parameters.AddWithValue("@CustName", (TxtCustName.Text != "" ? TxtCustName.Text : "0"));
            //cmd.Parameters.AddWithValue("@HouseName", (TxtHouseName.Text != "" ? TxtHouseName.Text : "0"));
            //cmd.Parameters.AddWithValue("@Address1", (TxtAdd1.Text != "" ? TxtAdd1.Text : "0"));
            //cmd.Parameters.AddWithValue("@Address2", (TxtAdd2.Text != "" ? TxtAdd2.Text : "0"));
            //cmd.Parameters.AddWithValue("@PhoneNo", (TxtPhone.Text != "" ? TxtPhone.Text : "0"));
            cmd.Parameters.AddWithValue("@CompanyId", (txtcompId.Text != "" ? txtcompId.Text : "0"));
            cmd.Parameters.AddWithValue("@BranchId", (txtBranchID.Text != "" ? txtBranchID.Text : "0"));

            dgvcust.DataFields = new List<string> { "JoinId","JoinNo","[Customer Name]", "Address1", "Address2", "Address3", "mobileNo", "PhoneNo" };
            dgvcust.HiddenDataFields = new List<string> { "JoinId" };
            //dgvcust.Columns["Address1"].Width = 150;
            //dgv.SummaryColumns = new string[] { "Nos", "Wt", "DiaNo", "DiaWt", "StoneNo", "StoneWt", "NetWt" };
            dgvcust.DataSource = DBConn.GetData(cmd).Tables[0];
            dgvcust.Columns["JoinNo"].Width = 120;
            dgvcust.Columns["Customer Name"].Width = 180;
            dgvcust.Columns["Address1"].Width = 280;
            dgvcust.Columns["Address2"].Width = 100;
            dgvcust.Columns["Address3"].Width = 100;
        }
        private void dgvcust_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int rowIndex = e.RowIndex;
            //DataGridViewRow row = dgvcust.Rows[rowIndex];
            //Cmb_SchemeNo.Text = row.Cells[1].Value.ToString();
            //Cmb_CustName.Text = row.Cells[2].Value.ToString();
            //txthouse.Text = row.Cells[3].Value.ToString();
            //txtAddress1.Text = row.Cells[4].Value.ToString();
            //txtAddress2.Text = row.Cells[5].Value.ToString();
            //txtPhoneNo.Text = row.Cells[6].Value.ToString();
            //custpanel.Visible = false;
        }

        private void grbTextBox1_TextChanged(object sender, EventArgs e)
        {
            //dgvcust.DataSource = this.DBConn.GetData(new SqlCommand("Select JoinNo,[Customer Name],Address1,Address2,Address3,PhoneNo from SALE.VCustomerList where [Customer Name] like '" + grbTextBox1.Text + "%'")).Tables[0];
    
        }

        private void TxtHouseName_TextChanged(object sender, EventArgs e)
        {
            //dgvcust.DataSource = this.DBConn.GetData(new SqlCommand("Select JoinNo,[Customer Name],Address1,Address2,Address3,PhoneNo from SALE.VCustomerList where Address1 like '" + TxtHouseName.Text + "%'")).Tables[0];
    
        }

        private void TxtAdd1_TextChanged(object sender, EventArgs e)
        {
            //dgvcust.DataSource = this.DBConn.GetData(new SqlCommand("Select JoinNo,[Customer Name],Address1,Address2,Address3,PhoneNo from SALE.VCustomerList where Address2 like '" + TxtAdd1.Text + "%'")).Tables[0];      
      
        }

        private void TxtAdd2_TextChanged(object sender, EventArgs e)
        {
           // dgvcust.DataSource = this.DBConn.GetData(new SqlCommand("Select JoinNo,[Customer Name],Address1,Address2,Address3,PhoneNo from SALE.VCustomerList where Address3 like '" + TxtAdd2.Text + "%'")).Tables[0];      
    
        }

        private void TxtPhone_TextChanged(object sender, EventArgs e)
        {
            //dgvcust.DataSource = this.DBConn.GetData(new SqlCommand("Select JoinNo,[Customer Name],Address1,Address2,Address3,PhoneNo from SALE.VCustomerList where PhoneNo like '" + TxtPhone.Text + "%'")).Tables[0];

     
        }

        private void grbTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvcust_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmSchemePaymentEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                if (custpanel.Visible == true)
                {
                    custpanel.Visible = false;
                }

            }
        }

        private void txtgst_TextChanged(object sender, EventArgs e)
        {
            //GST();
            //taxamt();


        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_amt_TextChanged(object sender, EventArgs e)
        {
            GetWt();
        }

        private void cmb_state_SelectedIndexChanged(object sender, EventArgs e)
        {
           // taxamt();
        }

        private void grb_search_Click(object sender, EventArgs e)
        {
           
        }

        private void grb_search_Click_1(object sender, EventArgs e)
        {
            loadcustomerdata();
        }

        private void dgvcust_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgvcust.Rows[rowIndex];

            Gramboo.General.Setupcombo(Cmb_SchemeNo, "SALE.SavingSchemeJoiningEntry", "JoinNo", "JoinId", "IsActive='True' AND JoinId NOT IN (SELECT JoinId FROM SALE.SchemeClosing ) AND Company_id=" + txtcompId.Text + " AND Branch_Id=" + txtBranchID.Text);

            Cmb_SchemeNo.Text = row.Cells["JoinNo"].Value.ToString();
           
            custpanel.Visible = false;
        }

        private void txt_OldGoldReceipt_TextChanged(object sender, EventArgs e)
        {
            caloldwt();
        }
       
        public void caloldwt()
        {
            OldWt = 0;
            double OGAmt = 0, TotalAmt = 0, rate = 0;
            
            if (txt_OldGoldReceipt.Text != "" && txtBranchID.Text != "")
            {
                OGAmt = Convert.ToDouble(txt_OldGoldReceipt.Text);

                //using (DataTable t = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                //     ("select Top 1 BoardRate from GEN.MetalRate where EntryDate= '" + Dtp_JoinDate.Value.Date.ToString("dd-MMM-yyyy") + "' and Branch_id=" + txtBranchID.Text + "")).Tables[0])
                //txtboardrate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + Dtp_VchDate.Value.Date.ToString("dd-MMM-yyyy") + "'  and Branch_id=" + txtBranchID.Text + " Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString();
                //if (txtBranchID.Text == null)
                //{
                //    return;
                //}
                if (txtgoldrate.Text != "")
                {
                    //double PtValue = Convert.ToDouble(t.Rows[0]["BoardRate"].ToString());
                    rate = Convert.ToDouble(txtgoldrate.Text);
                    TotalAmt = OGAmt / rate;
                }
            }

            OldWt = Math.Round(TotalAmt,3);          
        }

        private void grbButton2_Click(object sender, EventArgs e)
        {                       
        }

        private void Cmb_SchemeNo_TextChanged(object sender, EventArgs e)
        {
            SchemeDetails();
            PopulateGrid();
        }

        private void chkConvWt_CheckedChanged(object sender, EventArgs e)
        {
            txtgoldrate.ReadOnly = chkConvWt.Checked;
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {

            frmAuth auth = new frmAuth();
            auth.ShowDialog();
            if ((auth.DialogResult == DialogResult.OK))
            {
                Save();
            }
        }

        private void FrmSchemePaymentEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        //private void SetOldObject()
        //{
        //    using (DataTable dt = DBConn.GetData(new SqlCommand("select OldId,TotalOldGoldReceipt from SALE.VSavingSchemeJoiningEntry where   JoinId=" + txtjoinId.Text), "tbl").Tables[0])
        //    {
        //        if (dt.Rows.Count >= 1)
        //        {

        //            txt_OldGoldReceipt.Tag = dt.Rows[0]["OldId"].ToString();
        //            txt_OldGoldReceipt.Text = dt.Rows[0]["TotalOldGoldReceipt"].ToString();

        //            if (OldGold == null)
        //            {

        //                OldGoldReceiptNew o = new OldGoldReceiptNew();
        //                o.MdiParent = this.MdiParent;

        //                o.SR = this;
        //                o.GetCommonFieldValues();
        //                o.fillOldGoldReceipt();
        //                OldGold = o;

        //            }
        //        }
        //        else
        //        {
        //            txtOldGoldId.Text = "0";
        //            txt_OldGoldReceipt.Text = "0";
        //            txt_OldGoldReceipt.Tag = "";
        //        }
        //    }
        //}
    }
}
