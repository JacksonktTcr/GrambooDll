using Gramboo.Database;
using System;
using SAFA.Classes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Text;
//using System.Transactions;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace SAFA.Forms.CRM
{
    public partial class frmCustomerMasterr : Gramboo.Controls.GrbForm
    {
        bool flag = false; bool checkarea; string Compname = ""; int MrkFlag = 0;
        public frmCustomerMasterr CM;
        //public SAFA.Forms.GENERAL.FrmAreaMaster AR;
        //public SAFA.Forms.COM.CustomerSearch SD;
        private static frmCustomerMasterr instance;

        //public SALE.frmSalesEntry s1;
        //public SALE.frmSalesOrder SO;

        public Gramboo.Controls.GrbForm EntryForm { get; set; }
        public Gramboo.Controls.GrbComboBox Parentcontrol { get; set; }


        public static frmCustomerMasterr Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new frmCustomerMasterr();
                }
                else if (instance.IsDisposed)
                {
                    instance = new frmCustomerMasterr();
                }
                return instance;
            }
        }
        public Control SenderControl { get; set; }
        public Gramboo.Controls.GrbForm SenderForm { get; set; }

        public string SenderProperty { get; set; }

        public frmCustomerMasterr()
        {
            this.SuspendLayout();
            this.DoubleBuffered = true;
            InitializeComponent();

            
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
        public override void Init()
        {
            if (this.IsDisposed)
                return;
            base.Init();
            flag = true;
            cmb_Panchayath.SelectedValue = -1;
            Branchgrid();
            CurrentBranchsetting();
            TxtCustName.Focus();

            rbtAuto.Checked = true; RbNone.Checked = true; RbNone_SA.Checked = true;
            
            // lnk1.Visible = false;
            chkISActive.Checked = true;
            txtCustomerName.Text = "";
            txtCustomerName.Focus();
            txtid.Text = "default";
            
            txtDefaultTouch.Enabled = false;
            //this.ListForm = this;
            //  loaddata();
            //  btnAddLedger.GrpId = FA.MasterPageLedgerGroup.CustomerMaster;
            // cmbLedger.SelectedIndex = -1;
            DataTable dt1 = DBConn.GetData(new System.Data.SqlClient.SqlCommand("select Comp_District,Comp_State,Comp_Country FROM SYST.BranchMaster where branchid=" + txtBranchID.Text)).Tables[0];
            if (Compname == "MOD")
            {
                cmb_district.Text = "";
            }
            else
            {
                cmb_district.Text = dt1.Rows[0]["Comp_District"].ToString();
            }
            cmb_state.Text = dt1.Rows[0]["Comp_State"].ToString();
            cmb_country.Text = dt1.Rows[0]["Comp_Country"].ToString();
            rbtMale.TabStop = true;
            dgvspl.DataFields = new List<string>() { "TransId", "CustId", "DateId", "[DateName]", "SpecialDate", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Company_id", "Branch_id", "Counter_id" };
            dgvspl.HiddenDataFields = new List<string>() { "TransId", "CustId", "DateId", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Company_id", "Branch_id", "Counter_id" };
            //dgvspl.SummaryColumns = new string[] { "TaxAmount", "TaxRate" };
            dgvspl.Fill(new Table(SAFA.Classes.Common.DbName, "CRM", "VCustomerDetails", true), "1=2");
            dgvspl.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            AdjustColumnWidthsSpecialDates();
            txt_cuatpanno.Enabled = true;
            
            Code();
        }

        public override void RefreshData()
        {
            MrkFlag = 0;
            if (this.IsDisposed)
                return;
            base.RefreshData();
            

            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");

            //cmb_VoucherTypeId.SelectedValue = 72;
            //if (txtBranchID.Text != "")
            //{
            //    if (!IsEditMode)
            //        txtcode.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, grbDTPicker1.Value,

            //            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            //}
            Gramboo.General.Setupcombo(cmbCustType, "CRM.CustomerTypeMaster", "CustTypeName", "CustTypeId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmbarea, "GEN.AreaMaster", "AreaName", "AreaId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmbspl, "GEN.SpecialDayMaster", "SpecialDayName", "SpecialDayId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmb_district, "Gen.DistrictMaster", "DistrictName");
            Gramboo.General.Setupcombo(cmb_state, "GEN.StateCodeMaster", "StateName", "StateID");
            Gramboo.General.Setupcombo(cmb_country, "GEN.CountryMaster", "CountryName");
            Gramboo.General.Setupcombo(cmb_Panchayath, "GEN.PanchayatMaster", "PanchayathName", "PanchayathId");
            Gramboo.General.Setupcombo(CmbTdsid, "GEN.TDSMaster", "Tdsname", "Tdsid", "IsActive='True'");
            Gramboo.General.Setupcombo(CmbTcsId, "GEN.TCSMaster ", "TcsName", "TcsId", "IsActive='True'");
            Gramboo.General.Setupcombo(CmbTdsid_SA, "GEN.TDSMaster", "Tdsname", "Tdsid", "IsActive='True'");
            Gramboo.General.Setupcombo(CmbTcsId_SA, "GEN.TCSMaster ", "TcsName", "TcsId", "IsActive='True'");
            //Gramboo.General.Setupcombo(cmb_branch, "syst.BranchMaster", "BranchName","BranchId");
            Gramboo.General.Setupcombo(cmb_religion, "GEN.ReligionMaster", "Religion", "EntryId");
            //Gramboo.General.Setupcombo(cmb_Scheme, "SALE.SavingSchemeJoiningEntry", "JoinNo", "JoinId");
            Gramboo.General.Setupcombo(cmb_Occupation, "GEN.OccupationMaster", "OccupationName", "OccupationId");
            Gramboo.General.Setupcombo(cmb_branchName, "SYST.BranchMaster", "BranchName", "BranchId", "[IsActive]='True'");
            //Gramboo.General.Setupcombo(CmbCareof, "CRM.VCustomerMaster", "CustnameandCode", "CustId", "[IsActive]='True'");

            //Gramboo.General.Setupcombo(cmbCountry, "GEN.CountryMaster", "CountryName", "CountryId", "IsActive=1");
            // Gramboo.General.Setupcombo(cmbState, "GEN.StateMaster", "StateName", "StateId", "IsActive=1" + (cmbCountry.SelectedValue != null ? " AND CountryID=" + cmbCountry.SelectedValue : ""));
            //Gramboo.General.Setupcombo(cmbDistrict, "GEN.DistrictMaster", "DistrictName", "DistrictId", "IsActive=1" + (cmbState.SelectedValue != null ? " AND StateID=" + cmbState.SelectedValue : ""));
            if (this.txtcompId.Text.Trim() != "")
            {
                //new FA.AccountsHelper().LoadLedgerCombo(CmbLedgerId, this.DBConn, FA.MasterPageLedgerGroup.SupplierMaster, this.txtcompId.Text, this.txtBranchID.Text);
                //CmbLedgerId.SelectedIndex = -1;
                LoadLedgerHead();
            }
            populategrid();

            //new FA.AccountsHelper().LoadLedgerCombo(CmbLedgerId, this.DBConn, FA.MasterPageLedgerGroup.CustomerMaster, this.txtcompId.Text, this.txtBranchID.Text);
            //CmbLedgerId.SelectedIndex = -1;         
            Code();
        }

        private void LoadLedgerHead()
        {
            CmbLedgerId.SelectedValueChanged -= CmbLedgerId_SelectedValueChanged;
            string str = "SELECT Acc_LedgerId,Acc_LedgerCode,Acc_LedgerName FROM  ACC.LoadLedgersForCashTransactions(" + txtBranchID.Text + "," + txtcompId.Text + ") order by  Acc_LedgerName";
            SqlDataAdapter da = new SqlDataAdapter(str, DBConn.ConnectionProperties.ConnectionString);
            da.SelectCommand.CommandTimeout = 0;
            DataTable Ledager = new DataTable();
            da.Fill(Ledager);

            CmbLedgerId.DisplayMember = "Acc_LedgerName";
            CmbLedgerId.ValueMember = "Acc_LedgerId";

            CmbLedgerId.DataSource = Ledager;
            CmbLedgerId.SelectedValue = 0;
            CmbLedgerId.Text = "";
            CmbLedgerId.DropDownWidth = 500;
            if (flag == false)
                CmbLedgerId.SelectedValueChanged += CmbLedgerId_SelectedValueChanged;
        }

        //public void AutoComplete()
        //{
        //    txtCustomerName.AutoCompleteMode = AutoCompleteMode.Suggest;
        //    txtCustomerName.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
        //    getData(DataCollection);
        //    txtCustomerName.AutoCompleteCustomSource = DataCollection;

        //}


        public override bool InitializeTables()
        {
            Table t = new Table(SAFA.Classes.Common.DbName, "CRM", "CustomerMaster");
            t.PrimaryKeys.Add("CustId");
            t.NotUpdatables.Add("Id");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");

            Table t1 = new Table(SAFA.Classes.Common.DbName, "CRM", "CustomerDetails", true);
            t1.PrimaryKeys.Add("TransId");
            t1.FillView = new Table(SAFA.Classes.Common.DbName, "CRM", "VCustomerDetails", true);
            t1.DatagridView = dgvspl;
            t1.IsDatagridView = true;
            t1.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t1);
            t.IdTextBox = txtCustId;
            Table t2 = new Table(SAFA.Classes.Common.DbName, "CRM", "CustomerBranchDetail");
            t2.PrimaryKeys.Add("DetailId");
            t2.FillView = new Table(SAFA.Classes.Common.DbName, "CRM", "VCustomerBranchDetail");
            t2.DatagridView = dgvview;
            t2.IsDatagridView = true;
            t2.IdTextBox = TxtDetailId;
            t.ChildTables.Add(t2);

            Table t3 = new Table(SAFA.Classes.Common.DbName, "CRM", "SchemeDetails");
            t3.PrimaryKeys.Add("SchemeDetailId");
            t3.FillView = new Table(SAFA.Classes.Common.DbName, "CRM", "VSchemeDetails");
            t3.DatagridView = dgvScheme;
            t3.IsDatagridView = true;
            t3.IdTextBox = txtSchemedetailId;
            t.ChildTables.Add(t3);





            this.TableName = t;
            return true;
        }
        //public override bool ValidateControls()
        //{
        //    bool flag = true;
        //    flag = base.ValidateControls();
        //    CompanyVisibility();
        //    return flag;
        //}

        void ManualCheck()
        {


            if (txtCustId.Text == "")
            {
                return;
            }
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                ("SELECT Code,VoucherTypeId FROM CRM.CustomerMaster WHERE CustId =" + txtCustId.Text + "")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    string check = dt.Rows[0]["VoucherTypeId"].ToString();
                    if (check == "0")
                    {
                        chk_manual.Checked = true;
                        txtcode.Text = dt.Rows[0]["Code"].ToString();
                    }
                    else
                    {
                        chk_manual.Checked = false;
                    }
                }
            }
        }
        public override bool Save()
        {
            if (base.Save())
            {
                if (rbtAuto.Checked == true)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "[ACC].[ProcCreateCustomerLedger]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //connectionstring.Open(); 
                    cmd.Parameters.AddWithValue("@CustID", txtCustId.Text);
                    cmd.Parameters.AddWithValue("@CompanyId", txtcompId.Text);
                    cmd.Parameters.AddWithValue("@BranchId", txtBranchID.Text);
                    DBConn.ExecuteSqlTransaction(cmd, "CustLedger");
                }

                if (SenderControl != null)
                {
                    Gramboo.Controls.GrbTextBox cmb = new Gramboo.Controls.GrbTextBox();
                    cmb = (Gramboo.Controls.GrbTextBox)SenderControl;
                    cmb.Text = "0";
                    cmb.Text = txtCustId.Text;
                    SenderForm.Focus();
                    SenderForm.BringToFront();
                    this.Hide();
                }
                if (Parentcontrol != null)
                {

                    //EntryForm.Show();
                    EntryForm.Focus();
                    EntryForm.BringToFront();
                    this.Hide();

                }
                return true;
            }

            else
            {
                return false;
            }
        }

        public void fill(string custId)
        {
            if (custId != "" || custId != "0")
            {

                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select CustId FROM crm.customermaster WHERE CustId=" + custId)).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtCustId.Text = dt.Rows[0][0].ToString();
                        Dictionary<string, object> d = new Dictionary<string, object>();
                        d.Add("CustId", txtCustId.Text);
                        // d.Add("Company_id", txtcompId.Text);
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

        public override bool FillData(Dictionary<string, object> PrimaryValues)
        {
            CmbLedgerId.SelectedValueChanged -= CmbLedgerId_SelectedValueChanged; flag = true;
            if (base.FillData(PrimaryValues))
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                    ("SELECT ISNULL(RoundTo,'') as RoundTo FROM CRM.CustomerMaster where CustId=" + txtCustId.Text + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["RoundTo"].ToString() == "")
                        {
                            Rb3Digit.Checked = true;
                        }
                    }
                }
                if (dgvview.Rows.Count == 0)
                {
                    Branchgrid();

                }
                else
                {
                    FillBranchgrid();
                    DataTable dt = new DataTable();
                    dt = (DataTable)dgvview.DataSource;
                    Int64 branchid = 0;






                    using (System.Data.DataTable dt5 = DBConn.GetData(new SqlCommand("Select cast(0 as biGINT) as CustId, cast(0 as biGINT) as DetailId,cast(BranchId as bigint) as BranchId,BranchName,cast('false' as bit) as [SelectBranch],Company_id from SYST.BranchMaster WHERE BranchId !=" + txtBranchID.Text + "")).Tables[0])
                    {
                        if (dt5.Rows.Count > 0)
                        {
                            for (int i = dt5.Rows.Count - 1; i >= 0; i--)
                            {
                                branchid = Convert.ToInt64(dt5.Rows[i]["BranchId"].ToString());


                                using (System.Data.DataTable dt4 = DBConn.GetData(new SqlCommand("Select BranchId from CRM.VCustomerBranchDetail WHERE BranchId =" + branchid.ToString() + " and CustId=" + txtCustId.Text + " ")).Tables[0])
                                    if (dt4.Rows.Count > 0)
                                    {
                                        DataRow dr = dt5.Rows[i];
                                        dr.Delete();
                                    }
                            }
                            dt5.AcceptChanges();
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


                    }


                }
                custpanel.Visible = false;
                ManualCheck();

                SqlCommand cmd = new SqlCommand("Select GEN.ValidatePanNoDelete('CRM'," + txtCustId.Text + "," + txtcompId.Text + "," + txtBranchID.Text + ")");
                if (!Convert.ToBoolean(DBConn.GetData(cmd, "Val").Tables[0].Rows[0][0].ToString()))
                {
                    //txt_cuatpanno.Enabled = false;
                }
                else
                {
                    txt_cuatpanno.Enabled = true;
                }
                flag = false;
                CmbLedgerId.SelectedValueChanged += CmbLedgerId_SelectedValueChanged;
                return true;
            }
            else
            {
                flag = false;
                CmbLedgerId.SelectedValueChanged += CmbLedgerId_SelectedValueChanged;
                return false;
            }
            flag = false; CmbLedgerId.SelectedValueChanged += CmbLedgerId_SelectedValueChanged;
        }



        private void cmbCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            //  if (cmbCountry.ValueMember != "" && cmbCountry.SelectedValue != null)
            //     Gramboo.General.Setupcombo(cmbState, "GEN.StateMaster", "StateName", "StateId", "IsActive=1" + (cmbCountry.SelectedValue != null ? " AND CountryID=" + cmbCountry.SelectedValue : ""));

        }

        private void cmbState_SelectedValueChanged(object sender, EventArgs e)
        {
            // if (cmbState.ValueMember != "" && cmbState.SelectedValue != null)
            // Gramboo.General.Setupcombo(cmbDistrict, "GEN.DistrictMaster", "DistrictName", "DistrictId", "IsActive=1" + (cmbState.SelectedValue != null ? " AND StateID=" + cmbState.SelectedValue : ""));

        }


        //private void PopulateGrid()
        //{
        //    dgv.DataFields = new List<string>() { "CustId", "CustName", "CompanyId", "Comp_Name", "Visible" };
        //    dgv.HiddenDataFields = new List<string>() { "CustId", "CompanyId" };

        //    dgv.Fill(new Table(AMY.Classes.Common.DbName, "CRM", "VCustomerVisibility", true), "1=2");
        //}

        private void frmCustomerMaster_Load(object sender, EventArgs e)
        {
            cmb_district.Text = cmb_district.Text.ToUpper();
            cmb_state.Text = cmb_state.Text.ToUpper();
            cmb_country.Text = cmb_country.Text.ToUpper();
        }

        private void cmbCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void AdjustColumnWidthsSpecialDates()
        {
            dgvspl.RowHeadersVisible = false;
            dgvspl.Columns[0].Width = 40;
            dgvspl.Columns["DateName"].Width = cmbspl.Width;
            dgvspl.Columns["SpecialDate"].Width = dtpdate.Width + 5;
            //dgvspl.Columns["TaxAmount"].Width = TxtAmount_TaxDetails.Width + 5;
        }
        void Branchgrid()
        {

            dgvview.ShowEdit = true;
            dgvview.AutoGenerateColumns = true;


            dgvview.DataFields = new List<string> { "DetailId", "BranchName", "BranchId", "SelectBranch" };

            dgvview.HiddenDataFields = new List<string>() { "DetailId", "BranchId" };
            dgvview.Fill(new Table(SAFA.Classes.Common.DbName, "GEN", "VBranchSelectDetails", true));
            dgvview.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
        }
        void FillBranchgrid()
        {
            dgvview.DataSource = null;
            dgvview.ShowEdit = true;
            dgvview.AutoGenerateColumns = true;
            dgvview.DataFields = new List<string> { "CustId", "DetailId", "BranchId", "BranchName", "SelectBranch", "Company_id" };

            dgvview.HiddenDataFields = new List<string>() { "DetailId", "BranchId", "CustId", "Company_id" };


            dgvview.Fill(new Table(SAFA.Classes.Common.DbName, "CRM", "VCustomerBranchDetail", true), "CustId=" + txtCustId.Text + "");
            dgvview.Columns["col_AutoSlno"].DataPropertyName = "SlNo";


        }
        void CurrentBranchsetting()
        {
            if (txtBranchID.Text != "")
            {
                string Currentbarnchid = "";
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                          ("select BranchId from SYST.BranchMaster where BranchId= " + txtBranchID.Text + "")).Tables[0])

                    if (dt.Rows.Count > 0)
                    {
                        Currentbarnchid = dt.Rows[0]["BranchId"].ToString();




                        foreach (DataGridViewRow r in dgvview.Rows)
                        {

                            if ((r.Cells["BranchId"]).Value.ToString() == Currentbarnchid)

                            {
                                ((DataGridViewCheckBoxCell)r.Cells["SelectBranch"]).Value = true;
                            }


                        }


                    }
            }
        }


        public override bool Delete()
        {
            int set;
            bool flag = true;

            set = Convert.ToInt32(DBConn.GetData(new SqlCommand("Select CRM.CustomerMaster_Validation('" + txtCustId.Text + "')")).Tables[0].Rows[0][0]);//,'" + txtcompId.Text + "','" + txtBranchID.Text + "

            if (set == 1)
            {
                flag = false;

                txtCustomerName.ShowMessage("Customer Name Selected Cannot be Deleted");
                return flag;
            }
            else
            {
                return base.Delete();
            }
            // }
            // }
            //  }
            // return base.Delete();
        }

        private void lstCustomer_MouseDown(object sender, MouseEventArgs e)
        {


        }

        private void txtCustomerName_DragEnter(object sender, DragEventArgs e)
        {

        }
        private void txtCustomerName_PreviewDragOver(object sender, DragEventArgs e)
        {

        }
        private void txtCustomerName_DragOver(object sender, DragEventArgs e)
        {

        }

        private void lstCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if(lstCustomer.Visible)
            // txtCustomerName.Text = lstCustomer.SelectedItem.ToString();

        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            // if(this.ActiveControl==txtCustomerName)
            // getData();
            //// fillValues();
            // FillValues1();
            //// lstCustomer.Visible = false; 

        }
        //private void getData()
        //{
        //    if (txtCustomerName.Text != "")
        //    {
        //        //LIKE \'%" + (txtCustomerName.Text + "'"))).Tables[0])
        //        //'" + (txtCustomerName.Text + "%' or GEN.edit_distance(CustName,'" + txtCustomerName.Text + "')<=1 "))).Tables[0])
        //        //   using (DataTable dt = DBConn.GetData(new SqlCommand("Select DISTINCT CustName FROM CRM.CustomerMaster  WHERE CustName LIKE '" + (txtCustomerName.Text + "%' or GEN.edit_distance(CustName,'" + txtCustomerName.Text + "')<=1 AND Company_id=" + txtcompId.Text))).Tables[0])
        //        // using (DataTable dt = DBConn.GetData(new SqlCommand("Select CustName FROM CRM.CustomerMaster WHERE CustName LIKE \'%" + (txtCustomerName.Text + "%\'"))).Tables[0])
        //        using (DataTable dt = DBConn.GetData(new SqlCommand("Select DISTINCT CustName FROM CRM.VCustomerMasterVisibility  WHERE Company_id=" + txtcompId.Text + " AND CustName LIKE '" + (txtCustomerName.Text + "%' or GEN.edit_distance(CustName,'" + txtCustomerName.Text + "')<=1 "))).Tables[0])
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                List<string> lst = new List<string>();
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                    lst.Add(row["CustName"].ToString());
        //                }
        //                lstCustomer.DataSource = lst;
        //            }
        //            else
        //            {
        //                lstCustomer.Visible = false;
        //                Clear();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Clear();
        //    }
        //}


        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == (char)Keys.Space);
        }
        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            //lstCustomer.Visible = true;
            //if (e.KeyCode == Keys.Down)
            //{
            //   lstCustomer.Focus();
            //}
            //if (e.KeyCode == Keys.Enter && lstCustomer.Visible)
            //{
            //    txtCustomerName.Text = lstCustomer.Text;
            //    lstCustomer.Visible = false; 
            //}
        }
        private void txtCustomerName_Enter(object sender, EventArgs e)
        {
            //lstCustomer.Visible = false;
            //if (lstCustomer.Text != "")
            //{
            //    txtCustomerName.Text = lstCustomer.SelectedItem.ToString();

            //}
            //fillValues();
            //FillValues1();

        }
        //public void fillValues()
        //{
        //    if (txtCustomerName.Text != "")
        //    {

        //        using (DataTable dt = DBConn.GetData(new SqlCommand("Select * FROM CRM.CustomerMaster WHERE CustName ='" + txtCustomerName.Text + "'AND Company_id=" + txtcompId.Text)).Tables[0])
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                txtCustId.Text = dt.Rows[0]["CustId"].ToString();
        //                cmbCustType.SelectedValue  = dt.Rows[0]["CustTypeId"].ToString ();
        //                cmbCustGroup.SelectedValue = dt.Rows[0]["CustGroupId"];
        //                grbDTPicker1.Value = Convert.ToDateTime(dt.Rows[0]["CustRegDate"]);
        //                rbggender.Value = dt.Rows[0]["CustGender"].ToString();
        //                txtAddr1.Text = dt.Rows[0]["CustAddr1"].ToString();
        //                txtAddr2.Text = dt.Rows[0]["CustAddr2"].ToString();
        //                txtPlace.Text = dt.Rows[0]["CustPlace"].ToString();
        //                txtCity.Text = dt.Rows[0]["CustCity"].ToString();
        //                cmbDistrict.SelectedValue = dt.Rows[0]["DistrictId"];
        //                cmbCountry.SelectedValue = dt.Rows[0]["CountryId"];
        //                cmbState.SelectedValue = dt.Rows[0]["StateId"];
        //                txtPin.Text = dt.Rows[0]["CustPin"].ToString();
        //                txtContPersn.Text = (dt.Rows[0]["CustContPerson"] == null ? "" : dt.Rows[0]["CustContPerson"].ToString());
        //                txtContNo.Text = dt.Rows[0]["CustContPhone"].ToString();
        //                txtPhoneNo.Text = dt.Rows[0]["CustPhone"].ToString();
        //                txtMobile.Text = dt.Rows[0]["CustMob"].ToString();
        //                //txtEmail.Text = (dt.Rows[0]["CustEmail"] == null ? "-" : dt.Rows[0]["CustEmail"].ToString());
        //                txtEmail.Text = dt.Rows[0]["CustEmail"].ToString();
        //                txtFax.Text = dt.Rows[0]["CustFax"].ToString();
        //                txtWeb.Text = dt.Rows[0]["CustWebsite"].ToString();
        //                txtCrCashLimit.Text = dt.Rows[0]["CustCreditCashLimit"].ToString();
        //                txtCrdaylmit.Text = dt.Rows[0]["CustCreditDaysLimit"].ToString();
        //                txtCustCreditWtLimit.Text = dt.Rows[0]["CustCreditWtLimit"].ToString();
        //                cmbLedger.SelectedValue = dt.Rows[0]["Acc_LedgerId"];
        //                cmbCompany.SelectedValue = dt.Rows[0]["LinkCompanyID"];
        //                txtTin.Text = dt.Rows[0]["CustTIN"].ToString();
        //                txtCst.Text = dt.Rows[0]["CustCST"].ToString();
        //                txtCustCIN_No.Text = dt.Rows[0]["CustCIN_No"].ToString();
        //                chkISActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
        //            }
        //        }
        //        dgv.DataSource = this.DBConn.GetData(new SqlCommand("Select CustId,TransId,CompanyId,[Company Name],Visible FROM CRM.VCustomerVisibility WHERE CustName ='" + txtCustomerName.Text + "'AND Company_id=" + txtcompId.Text)).Tables[0];
        //    }
        //}
        //public void FillValues1()
        //{
        //    if (txtCustomerName.Text != "")
        //    {

        //        using (DataTable dt = DBConn.GetData(new SqlCommand("Select * FROM CRM.VCustomerMaster WHERE [Customer Name]='" + txtCustomerName.Text + "'AND Company_id=" + txtcompId.Text)).Tables[0])
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                cmbCustType.SelectedValue = dt.Rows[0]["CustTypeId"].ToString();
        //                cmbCustType.Text = dt.Rows[0]["Customer Type"].ToString();
        //                cmbCustGroup.SelectedValue = dt.Rows[0]["CustGroupId"];
        //                cmbCustGroup.Text = dt.Rows[0]["CustGroupName"].ToString ();
        //            }
        //        }
        //    }
        //}

        public void Clear()
        {
            txtCustId.Text = "";
            cmbCustType___.Text = "";
            //cmbCustGroup.Text = "";
            rbggender.Value = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            txtPlace.Text = "";
            txtCity.Text = "";
            //cmbDistrict.Text = "";
            // cmbCountry.Text = "";
            // cmbState.Text = "";
            cmb_district.Text = "";
            cmb_state.Text = "";
            cmb_country.Text = "";
            CmbCareof.Text = "";
            txtPin.Text = "";
            //txtContPersn.Text = "";
            txtContNo.Text = "";
            txtPhoneNo.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            //txtFax.Text = "";
            //txtWeb.Text = "";
            //txtCrCashLimit.Text = "";
            //txtCrdaylmit.Text = "";
            // txtCustCreditWtLimit.Text = "";
            //cmbLedger.Text = "";
            // cmbCompany.Text = "";
            // txtTin.Text = "";
            // txtCst.Text = "";
            // txtCustCIN_No.Text = "";
            chkISActive.Checked = false;
        }
        private void txtCustomerName_Leave(object sender, EventArgs e)
        {
            txtCustomerName.Text = txtCustomerName.Text.ToUpper();
        }

        private void lstCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                //    txtCustomerName.Text = lstCustomer.Text;
                // lstCustomer.Visible = false;
            }
        }

      
        public override bool ValidateControls()
        {
            if (!IsEditMode)
            {


                if (MrkFlag == 0)
                {
                    if (txtPhoneNo.Text != "" || txtMobile.Text != "")
                    {
                        using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand("SELECT top 1* FROM MRK.AddressMaster where CustPhoneNumber='" + (txtPhoneNo.Text == "" ? "0" : txtPhoneNo.Text) + "' or CustPhoneNumber='" + txtMobile.Text + "'")).Tables[0])
                            if (dt.Rows.Count > 0)
                            {
                                if (Gramboo.General.ShowMessage(" Do you want to load the data from Marketing customer details ?", "Marketing Customer Details", MessageBoxIcon.Question, MessageBoxButtons.YesNo) == DialogResult.No)
                                {
                                    MrkFlag = 0;
                                    return true;
                                }
                                else
                                {
                                    FillMrkCustomerdata((dt.Rows[0]["custid"]).ToString());
                                    MrkFlag = 1;
                                    return false;

                                }
                            }
                    }
                }
            }


            if (base.ValidateControls())
            {
                

                if (checkarea == true)
                {
                    if (cmbarea.SelectedValue == null)
                    {
                        Gramboo.General.ShowMessage("Select area ");
                        return false;
                    }
                }
                if (rbtManul.Checked == true)
                {
                    if (CmbLedgerId.SelectedValue == null)
                    {
                        Gramboo.General.ShowMessage("Choose a LedgerHead ");
                        CmbLedgerId.Focus();
                        return false;

                    }
                }
                bool existingphone = false;

                if (IsEditMode == false)
                {
                    using (System.Data.DataTable dt1 = DBConn.GetData(new SqlCommand("SELECT top 1 * FROM CRM.CustomerMaster where (len(CustMob ) >5 AND ( CustMob='" + txtMobile.Text + "' OR CustMob='" + (txtPhoneNo.Text) + "')) or (len(CustPhone ) >5 AND ( CustPhone='" + txtMobile.Text + "' OR CustPhone='" + (txtPhoneNo.Text) + "')) ")).Tables[0])
                    {
                        if (dt1.Rows.Count > 0)
                        {
                            existingphone = true;
                        }

                    }
                }


                if (existingphone)
                {
                    // Gramboo.General.ShowMessage("Mobile No Already Exist");
                    // DialogResult dialougresult = MessageBox.Show("Mobile No Already Exist", "Mobile No", MessageBoxButtons.YesNo);
                    if (ShowMessage("Mobile No Already Exist", "Mobile No", MessageBoxIcon.Question, MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        txtMobile.Focus();
                        return true;
                    }
                    else
                    {
                        ShowMessage("Please Change the Phone Number Or Mobile Number");
                        return false;
                    }
                    //if (dialougresult != DialogResult.Yes)
                    //{
                    //    return false;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Please Change the Phone Number Or Mobile Number");
                    //    txtMobile.Focus();
                    //    return false;
                    //}
                }
                if (!IsEditMode)
                {
                    if (txt_cardno.Text != "")
                    {
                        using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand("SELECT CardNo FROM CRM.CustomerMaster where CardNo='" + txt_cardno.Text + "' and CustId=" + (txtCustId.Text))).Tables[0])

                            if (dt.Rows.Count > 0)
                            {
                                txt_cardno.ShowMessage("CardNo Already Exist");
                                return false;
                            }
                    }
                }
                if (cmb_state.Text == "")
                {
                    cmb_state.ShowMessage("Update StateName..!!");
                    return false;
                }
                if (txt_cuatpanno.Text != "")
                {
                    if (txt_cuatpanno.Text.ToString().Trim().Length >= 1)
                    {
                        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$");
                        Match match = regex.Match(txt_cuatpanno.Text);

                        if (match.Success == false)
                        {
                            txt_cuatpanno.ShowMessage("Incorrect PanNumber..!!");
                            txt_cuatpanno.Focus();
                            return false;
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("Select GEN.ValildateGovIds('" + txt_cuatpanno.Text + "','PAN'," + txtcompId.Text + "," + txtBranchID.Text + ")");

                            if (!Convert.ToBoolean(DBConn.GetData(cmd, "Val").Tables[0].Rows[0][0].ToString()))
                            {
                                DialogResult d = Gramboo.General.ShowMessage("PanNumber already exists ? \n\n" +
                                " Press Yes to Save, No to Cancel \n", "Save", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                                if (d == DialogResult.No)
                                {
                                    txt_cuatpanno.ShowMessage("PanNumber already exists");
                                    txt_cuatpanno.Focus();
                                    return false;
                                }
                            }
                        }
                    }

                }
                if (txt_custaadharno.Text != "")
                {
                    if (txt_custaadharno.Text.ToString().Trim().Length >= 1)
                    {
                        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^([0-9]){12}$");
                        Match match = regex.Match(txt_custaadharno.Text);

                        if (match.Success == false)
                        {
                            txt_custaadharno.ShowMessage("Incorrect AadhaarNumber..!!");
                            txt_custaadharno.Focus();
                            return false;
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("Select GEN.ValildateGovIds('" + txt_custaadharno.Text + "','Aadhaar'," + txtcompId.Text + "," + txtBranchID.Text + ")");

                            if (!Convert.ToBoolean(DBConn.GetData(cmd, "Val").Tables[0].Rows[0][0].ToString()))
                            {
                                DialogResult d = Gramboo.General.ShowMessage("AadhaarNumber already exists ? \n\n" +
                                " Press Yes to Save, No to Cancel \n", "Save", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                                if (d == DialogResult.No)
                                {
                                    txt_custaadharno.ShowMessage("AadhaarNumber already exists");
                                    txt_custaadharno.Focus();
                                    return false;
                                }
                            }
                        }
                    }

                }
                if (cmbCustType.SelectedValue.ToString() == "110100000012")
                {
                    if (txtDefaultTouch.Text == "" || Convert.ToSingle(txtDefaultTouch.Text) <= 0)
                    {
                        txtDefaultTouch.ShowMessage("Enter DefaultTouch ..!!");
                        txtDefaultTouch.Focus();

                        return false;
                    }
                }

                if (cmbCustType.SelectedValue.ToString() == "110100000004")
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$");
                    Match match = regex.Match(txt_custgstno.Text);
                    if (txt_custgstno.Text.ToString().Trim().Length >= 1)
                    {
                        if (match.Success == false)
                        {
                            txt_custgstno.ShowMessage("Incorrect GSTNumber..!!");
                            txt_custgstno.Focus();

                            return false;
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("Select GEN.ValildateGovIds('" + txt_custgstno.Text + "','GST'," + txtcompId.Text + "," + txtBranchID.Text + ")");

                            if (!Convert.ToBoolean(DBConn.GetData(cmd, "Val").Tables[0].Rows[0][0].ToString()))
                            {
                                DialogResult d = Gramboo.General.ShowMessage("GSTNumber already exists ? \n\n" +
                                " Press Yes to Save, No to Cancel \n", "Save", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);
                                if (d == DialogResult.No)
                                {
                                    txt_custgstno.ShowMessage("GSTNumber already exists");
                                    txt_custgstno.Focus();
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        txt_custgstno.ShowMessage("Please Enter GSTNumber..!!");
                        return false;
                    }
                }

                Code();
                return true;

            }
            else
            {
                Code();
                return false;
            }
        }

        public void loadcustomerdata()
        {
            string str = "SELECT t1.CustId,t1.CardNo,t1.CustName, t1.HouseName, t1.CustAddr1,t1.CustAddr2,t1.CustMob,t1.CustPhone,t1.code,t1.CustPanNo FROM  CRM.vCustomerMaster t1 where 1=1";
            if (TxtCustCode.Text != "")
            {
                str += " AND code like '" + TxtCustCode.Text + "%'";
            }
            if (txt_custcardno.Text != "")
            {
                str += " AND CardNo like '" + txt_custcardno.Text + "%'";
            }
            if (TxtCustName.Text != "")
            {
                str += " AND CustName like '" + TxtCustName.Text + "%'";
            }
            if (TxtHouseName.Text != "")
            {
                str += " AND HouseName like '" + TxtHouseName.Text + "%'";
            }
            if (TxtAdd1.Text != "")
            {
                str += " AND CustAddr1 like '" + TxtAdd1.Text + "%'";
            }
            if (TxtAdd2.Text != "")
            {
                str += " AND  CustAddr2 like '" + TxtAdd2.Text + "%'";
            }

            if (TxtPhone.Text != "")
            {
                str += " AND  (CustPhone like '" + TxtPhone.Text + "%' or CustMob like '" + TxtPhone.Text + "%'  )";
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SALE.CustomerSearch";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Date", grbDTPicker1.Value.ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@SearchQuery", str);
            //cmd.Parameters.AddWithValue("@CardNo", (txt_custcardno.Text != "" ? txt_custcardno.Text : "0"));
            //cmd.Parameters.AddWithValue("@CustName", (TxtCustName.Text != "" ? TxtCustName.Text : "0"));
            //cmd.Parameters.AddWithValue("@HouseName", (TxtHouseName.Text != "" ? TxtHouseName.Text : "0"));
            //cmd.Parameters.AddWithValue("@Address1", (TxtAdd1.Text != "" ? TxtAdd1.Text : "0"));
            //cmd.Parameters.AddWithValue("@Address2", (TxtAdd2.Text != "" ? TxtAdd2.Text : "0"));
            //cmd.Parameters.AddWithValue("@PhoneNo", (TxtPhone.Text != "" ? TxtPhone.Text : "0"));
            cmd.Parameters.AddWithValue("@CompanyId", (txtcompId.Text != "" ? txtcompId.Text : "0"));
            cmd.Parameters.AddWithValue("@BranchId", (txtBranchID.Text != "" ? txtBranchID.Text : "0"));

            dgvcust.DataFields = new List<string> { "CustId", "CardNo", "[Customer Name]", "HouseName", "Address1", "Address2", "MobileNo", "PhoneNo", "code", "CustPanNo", "CardBalance", "CreditAmount" };
            dgvcust.HiddenDataFields = new List<string> { "CustId" };
            //dgvcust.Columns["Address1"].Width = 150;
            //dgv.SummaryColumns = new string[] { "Nos", "Wt", "DiaNo", "DiaWt", "StoneNo", "StoneWt", "NetWt" };
            dgvcust.DataSource = DBConn.GetData(cmd).Tables[0];

            dgvcust.Columns["Customer Name"].Width = 180;
            dgvcust.Columns["Address1"].Width = 280;
            dgvcust.Columns["HouseName"].Width = 100;
            dgvcust.Columns["Address2"].Width = 100;
            dgvcust.IsList = true;
            dgvcust.EntryFormName = this;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (custpanel.Visible == false)
            {
                custpanel.Parent = this;
                custpanel.Visible = true;
                custpanel.BringToFront();
                custpanel.Show();
                custpanel.BringToFront();
                TxtCustName.Focus();
                //Cmb_CustName.Enabled = false;
                //txthouse.Enabled = false;
                //txtAddress1.Enabled = false;
                //txtAddress2.Enabled = false;
                // txtPhoneNo.Enabled = false;
                custpanel.Location = new Point(100, 100);
                custpanel.Size = new Size(this.Width - 200, 400);
            }

            else
            {
                custpanel.Visible = false;
                custpanel.SendToBack();
                custpanel.Hide();
            }


        }

        private void dgvcust_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvcust_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grb_search_Click(object sender, EventArgs e)
        {
            loadcustomerdata();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvspl_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            cmbspl.Focus();
            if (cmbspl.SelectedValue != null)
            {
                txtsplid.Text = cmbspl.SelectedValue.ToString();
                dgvspl.Save();
            }
        }

        private void txtAddr2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtstate_TextChanged(object sender, EventArgs e)
        {

        }

        private void chk_manual_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_manual.Checked == true)
            {
                cmb_VoucherTypeId.SelectedValue = 0;
                txtcode.ReadOnly = false;
                txtcode.Text = "";
                txtcode.Focus();


            }
            else
            {
                Code();
                txtcode.ReadOnly = true;
                //RefreshData();
            }
        }

        //private void txtPhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == '+' || e.KeyChar == (char)Keys.Back);
        //}

        private void frmCustomerMasterr_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void frmCustomerMasterr_Enter(object sender, EventArgs e)
        {


        }

        

        private void txtAddr2_Leave(object sender, EventArgs e)
        {
            txtAddr2.Text = txtAddr2.Text.ToUpper();
        }

        private void txtAddr1_Leave(object sender, EventArgs e)
        {

        }

        private void txtAddr1_Leave_1(object sender, EventArgs e)
        {
            txtAddr1.Text = txtAddr1.Text.ToUpper();
        }

        private void grbTextBox1_Leave(object sender, EventArgs e)
        {
            grbTextBox1.Text = grbTextBox1.Text.ToUpper();
        }

        private void txtPlace_Leave(object sender, EventArgs e)
        {
            txtPlace.Text = txtPlace.Text.ToUpper();
        }

        private void txtCity_Leave(object sender, EventArgs e)
        {
            txtCity.Text = txtCity.Text.ToUpper();
        }

        private void cmbarea_Leave(object sender, EventArgs e)
        {
            cmbarea.Text = cmbarea.Text.ToUpper();
        }

        private void cmb_district_Leave(object sender, EventArgs e)
        {
            cmb_district.Text = cmb_district.Text.ToUpper();
        }

        //private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        //}

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void txtCity_Leave_1(object sender, EventArgs e)
        {
            txtCity.Text = txtCity.Text.ToUpper();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void cmb_state_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_state.SelectedValue != null)
            {
                txtStateid.Text = cmb_state.SelectedValue.ToString();
            }
            else if (cmb_state.Text == "")
            {
                txtStateid.Text = "18";
            }



        }
   
        private void cmb_state_TextChanged(object sender, EventArgs e)
        {
            if (cmb_state.Text == "")
            {
                txtStateid.Text = "18";
            }
        }

        private void cmbCustType_SelectedValueChanged(object sender, EventArgs e)
        {
            Code();
            if (cmbCustType.SelectedValue != null)
            {


                if (cmbCustType.Text == "Wholesale")
                {
                    rbtManul.Checked = true;
                    txtRelationName.AcceptBlankValue = true;
                    txtDefaultTouch.Enabled = true;
                    //txtRelationName.ShowComplusoryMark = false;

                }
                else
                {
                    rbtAuto.Checked = true;
                    // txtRelationName.AcceptBlankValue = false;
                    txtDefaultTouch.Enabled = false;
                    //txtRelationName.ShowComplusoryMark = true;

                }
            }
        }
        void Code()
        {
            if (chk_manual.Checked == false)
            {
                if (cmbCustType.SelectedValue != null)
                {
                    if (cmbCustType.SelectedValue.ToString() == "110100000005")
                    {
                        cmb_VoucherTypeId.SelectedValue = 72;

                        if (txtBranchID.Text != "")
                        {
                            if (!IsEditMode)
                                txtcode.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, grbDTPicker1.Value,

                                    DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
                        }

                        txt_custgstno.Enabled = false;
                    }
                    else
                    {
                        cmb_VoucherTypeId.SelectedValue = 194;

                        if (txtBranchID.Text != "")
                        {
                            if (!IsEditMode)
                                txtcode.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, grbDTPicker1.Value,

                                    DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
                        }

                        txt_custgstno.Enabled = true;
                    }
                }
            }
            else
            {
                cmb_VoucherTypeId.SelectedValue = 0;
                txtcode.ReadOnly = false;
                if (!IsEditMode)
                    //txtcode.Text = "";
                    txtcode.Focus();
            }
        }
        private void txt_cuatpanno_Validated(object sender, EventArgs e)
        {
            if (txt_cuatpanno.Text != "")
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$");
                Match match = regex.Match(txt_cuatpanno.Text);
                if (match.Success == false)
                {
                    txt_cuatpanno.ShowMessage("Incorrect PanNumber..!!");
                    txt_cuatpanno.Focus();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Select GEN.ValildateGovIds('" + txt_cuatpanno.Text + "','PAN'," + txtcompId.Text + "," + txtBranchID.Text + ")");

                    if (!Convert.ToBoolean(DBConn.GetData(cmd, "Val").Tables[0].Rows[0][0].ToString()))
                    {
                        txt_cuatpanno.ShowMessage("PanNumber already exists");
                    }
                }

            }
        }

        private void txt_custgstno_Validated(object sender, EventArgs e)
        {
            if (txt_custgstno.Text != "")
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$");
                Match match = regex.Match(txt_custgstno.Text);
                if (match.Success == false)
                {
                    txt_custgstno.ShowMessage("Incorrect GSTNumber..!!");
                    txt_custgstno.Focus();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Select GEN.ValildateGovIds('" + txt_custgstno.Text + "','GST'," + txtcompId.Text + "," + txtBranchID.Text + ")");

                    if (!Convert.ToBoolean(DBConn.GetData(cmd, "Val").Tables[0].Rows[0][0].ToString()))
                    {
                        txt_custgstno.ShowMessage("GSTNumber already exists");
                    }
                }
            }
        }

        private void rbtManul_CheckedChanged(object sender, EventArgs e)
        {
           
        }


         

        private void frmCustomerMasterr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (custpanel.Visible == true)
                {
                    custpanel.Visible = false;
                }
            }
        }

        private void txt_custaadharno_Validated(object sender, EventArgs e)
        {
            if (txt_custaadharno.Text != "")
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(txt_custaadharno.Text.Trim(), @"^([0-9]){12}$"))
                {
                    txt_custaadharno.ShowMessage("Incorrect AadharNumber..!!");
                    txt_custaadharno.Focus();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("Select GEN.ValildateGovIds('" + txt_custaadharno.Text + "','Aadhaar'," + txtcompId.Text + "," + txtBranchID.Text + ")");

                    if (!Convert.ToBoolean(DBConn.GetData(cmd, "Val").Tables[0].Rows[0][0].ToString()))
                    {
                        txt_custaadharno.ShowMessage("AadhaarNumber already exists");
                    }
                }
            }

        }

        private void txtPhoneNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMobile_Validated(object sender, EventArgs e)
        {
            if (txtMobile.Text != "")
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[0-9]+$");
                Match match = regex.Match(txtMobile.Text);
                if (match.Success == false)
                {
                    txtMobile.ShowMessage("Incorrect Phone number..!!");
                    txtMobile.Focus();
                }
            }
        }

        private void txtPhoneNo_Validated(object sender, EventArgs e)
        {

            if (txtPhoneNo.Text != "")
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[0-9]+$");
                Match match = regex.Match(txtPhoneNo.Text);
                if (match.Success == false)
                {
                    txtPhoneNo.ShowMessage("Incorrect Phone number..!!");
                    txtPhoneNo.Focus();
                }
            }

        }

        private void cmbarea_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbarea.SelectedValue != null || cmbarea.SelectedValue != "0")
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                   ("SELECT * FROM GEN.VAreaDistrict WHERE AreaId ='" + cmbarea.SelectedValue + "'")).Tables[0])

                    if (dt.Rows.Count > 0)
                    {
                        cmb_district.Text = dt.Rows[0]["DistrictName"].ToString();
                        cmb_state.Text = dt.Rows[0]["StateName"].ToString();
                        cmb_country.Text = dt.Rows[0]["CountryName"].ToString();
                        cmb_Panchayath.Text = dt.Rows[0]["PanchayathName"].ToString();
                        //txtPhoneNo.Focus();

                    }

            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtPlace_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbggender_Enter(object sender, EventArgs e)
        {

        }

        private void btn_Area_Click(object sender, EventArgs e)
        {
             
        }

        private void cmbarea_TextChanged(object sender, EventArgs e)
        {

            //if (cmbarea.SelectedValue != null || cmbarea.SelectedValue != "0")
            //{
            //    using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
            //                                       ("SELECT * FROM GEN.VAreaDistrict WHERE AreaId ='" + cmbarea.SelectedValue + "'")).Tables[0])

            //        if (dt.Rows.Count > 0)
            //        {
            //            cmb_district.Text = dt.Rows[0]["DistrictName"].ToString();
            //            cmb_state.Text = dt.Rows[0]["StateName"].ToString();
            //            cmb_country.Text = dt.Rows[0]["CountryName"].ToString();
            //            cmb_Panchayath.Text = dt.Rows[0]["PanchayathName"].ToString();
            //            txtPhoneNo.Focus();

            //        }

            //}
            //RefreshData();
        }

        private void CmbRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void ChkAllBranch_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAllBranch.Checked == true)
            {
                try
                {
                    foreach (DataGridViewRow r in dgvview.Rows)
                    {
                        if ((bool)((DataGridViewCheckBoxCell)r.Cells["SelectBranch"]).Value == false)
                        {
                            ((DataGridViewCheckBoxCell)r.Cells["SelectBranch"]).Value = true;
                        }
                        ((DataGridViewCheckBoxCell)r.Cells["SelectBranch"]).ReadOnly = true;
                    }
                }
                catch (Exception)
                {

                }

            }
            else
            {
                foreach (DataGridViewRow r in dgvview.Rows)
                {
                    if (r.Cells["Branchid"].Value.ToString() == Gramboo.GeneralConfig.BranchId.ToString())
                    {
                        ((DataGridViewCheckBoxCell)r.Cells["SelectBranch"]).Value = true;
                    }
                    else
                    {
                        ((DataGridViewCheckBoxCell)r.Cells["SelectBranch"]).Value = false;
                    }

                    ((DataGridViewCheckBoxCell)r.Cells["SelectBranch"]).ReadOnly = false;
                }
            }
        }

        private void cmb_Scheme_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_Scheme.SelectedValue != null)
            {
                txtSchemeName.Text = cmb_Scheme.Text;

            }
        }
        public void populategrid()
        {
            dgvScheme.DataFields = new List<string>() { "SchemeDetailId", "CustId", "SchemeId", "JoinNo", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Company_id", "Branch_id" };
            //DgvBankdetails.SummaryColumns = new string[] { };
            dgvScheme.HiddenDataFields = new List<string> { "SchemeDetailId", "CustId", "SchemeId", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Company_id", "Branch_id" };
            dgvScheme.Fill(new Table(SAFA.Classes.Common.DbName, "CRM", "VSchemeDetails", true), "1=2");
            //dgvBankDetails.Columns["SuppBankname"].HeaderText = "Bank Name";


        }

        private void grbButton2_Click(object sender, EventArgs e)
        {
            if (cmb_Scheme.SelectedValue != null)
            {
                dgvScheme.Save();
            }
        }
        public void loadMrkCustomerdata()
        {
            string str = "SELECT t1.CustId,t1.[Customer Name], t1.[House Name] , t1.Address1,t1.Address2,t1.[Phone Number] FROM  MRK.vAddressMaster t1 where 1=1";

            //if (txt_custcardno.Text != "")
            //{
            //    str += " AND CardNo like '" + txt_custcardno.Text + "%'";
            //}
            if (txtmrkCustName.Text != "")
            {
                str += " AND [Customer Name] like '" + txtmrkCustName.Text + "%'";
            }
            if (txtmrkHouseName.Text != "")
            {
                str += " AND [House Name] like '" + txtmrkHouseName.Text + "%'";
            }
            if (txtmrkAddr1.Text != "")
            {
                str += " AND Address1 like '" + txtmrkAddr1.Text + "%'";
            }
            if (txtmrkAddr2.Text != "")
            {
                str += " AND  Address2 like '" + txtmrkAddr2.Text + "%'";
            }

            if (txtmrkPhone.Text != "")
            {
                str += " AND  ([Phone Number] like '" + txtmrkPhone.Text + "%'  )";
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "MRK.CustomerSearch";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Date", grbDTPicker1.Value.ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@SearchQuery", str);
            cmd.Parameters.AddWithValue("@CompanyId", (txtcompId.Text != "" ? txtcompId.Text : "0"));
            cmd.Parameters.AddWithValue("@BranchId", (txtBranchID.Text != "" ? txtBranchID.Text : "0"));

            dgvMrkCust.DataFields = new List<string> { "CustId", "[Customer Name]", "HouseName", "Address1", "Address2", "PhoneNo" };
            dgvMrkCust.HiddenDataFields = new List<string> { "CustId" };
            //dgvcust.Columns["Address1"].Width = 150;
            //dgv.SummaryColumns = new string[] { "Nos", "Wt", "DiaNo", "DiaWt", "StoneNo", "StoneWt", "NetWt" };
            dgvMrkCust.DataSource = DBConn.GetData(cmd).Tables[0];

            dgvMrkCust.Columns["Customer Name"].Width = 180;
            dgvMrkCust.Columns["Address1"].Width = 280;
            dgvMrkCust.Columns["HouseName"].Width = 100;
            dgvMrkCust.Columns["Address2"].Width = 100;



        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MrkCustomerPanel.Visible == false)
            {
                MrkCustomerPanel.Parent = this;
                MrkCustomerPanel.Visible = true;
                MrkCustomerPanel.BringToFront();
                MrkCustomerPanel.Show();
                MrkCustomerPanel.BringToFront();
                txtmrkCustName.Focus();
                //Cmb_CustName.Enabled = false;
                //txthouse.Enabled = false;
                //txtAddress1.Enabled = false;
                //txtAddress2.Enabled = false;
                // txtPhoneNo.Enabled = false;
                MrkCustomerPanel.Location = new Point(100, 100);
                MrkCustomerPanel.Size = new Size(this.Width - 200, 400);
            }

            else
            {
                MrkCustomerPanel.Visible = false;
                MrkCustomerPanel.SendToBack();
                MrkCustomerPanel.Hide();
            }


        }

        private void grbButton3_Click(object sender, EventArgs e)
        {
            loadMrkCustomerdata();
        }

        private void dgvMrkCust_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgvMrkCust.Rows[rowIndex];



            FillMrkCustomerdata((row.Cells["CustId"].Value).ToString());


            MrkCustomerPanel.Visible = false;
            MrkFlag = 1;





        }
        public void FillMrkCustomerdata(String a)
        {
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand("SELECT * FROM MRK.vAddressMaster where CustID=" + a)).Tables[0])
                if (dt.Rows.Count > 0)
                {
                    txtCustomerName.Text = dt.Rows[0]["Customer Name"] == null ? "" : dt.Rows[0]["Customer Name"].ToString();
                    txtAddr1.Text = (dt.Rows[0]["House Name"] == DBNull.Value ? "" : dt.Rows[0]["House Name"].ToString());
                    txtAddr2.Text = (dt.Rows[0]["Address1"] == DBNull.Value ? "" : dt.Rows[0]["Address1"].ToString());
                    cmb_religion.SelectedValue = dt.Rows[0]["CustReligionID"] == DBNull.Value ? "0" : dt.Rows[0]["CustReligionID"];
                    cmbCustType.SelectedValue = dt.Rows[0]["CustCategoryID"] == DBNull.Value ? "0" : dt.Rows[0]["CustCategoryID"];
                    rbggender.Value = dt.Rows[0]["CustGender"] == DBNull.Value ? "0" : dt.Rows[0]["CustGender"].ToString();
                    txtPin.Text = dt.Rows[0]["Pincode"] == DBNull.Value ? "0" : dt.Rows[0]["Pincode"].ToString();
                    grbTextBox1.Text = dt.Rows[0]["Address2"] == null ? "0" : dt.Rows[0]["Address2"].ToString();
                    cmbarea.SelectedValue = dt.Rows[0]["CustAreaID"] == DBNull.Value ? "0" : dt.Rows[0]["CustAreaID"];
                    txtMobile.Text = dt.Rows[0]["Phone Number"] == DBNull.Value ? "0" : dt.Rows[0]["Phone Number"].ToString();
                    txtPhoneNo.Text = dt.Rows[0]["Phone Number"] == DBNull.Value ? "0" : dt.Rows[0]["Phone Number"].ToString();
                    txtEmail.Text = dt.Rows[0]["Email ID"] == DBNull.Value ? "0" : dt.Rows[0]["Email ID"].ToString();

                }

        }
        private void txtAddr1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvcust_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {

        }

        private void txt_cuatpanno_TextChanged(object sender, EventArgs e)
        {
            string PanNo, word;

            char Panlstvalu;
            PanNo = Convert.ToString(txt_cuatpanno.Text);
            if (txt_cuatpanno.Text == "")
            {
                grbfirm.Checked = false;
                grbcompany.Checked = false;
                grbindi.Checked = false;
            }
            if (txt_cuatpanno.Text != "" && txt_cuatpanno.TextLength > 4)
            {
                //int index = 0;
                //int end = gjg.Length - 4;
                PanNo.Trim();

                word = PanNo.Substring(0, 4);
                Panlstvalu = word[word.Length - 1];

                if (Panlstvalu.ToString().ToUpper() == "P")
                {
                    grbindi.Checked = true;
                }
                else if (Panlstvalu.ToString().ToUpper() == "C")
                {
                    grbcompany.Checked = true;
                }
                else if (Panlstvalu.ToString().ToUpper() == "F")
                {
                    grbfirm.Checked = true;
                }
                else
                {
                    grbfirm.Checked = false;
                    grbcompany.Checked = false;
                    grbindi.Checked = false;
                }

            }
        }

        private void CmbLedgerId_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CmbLedgerId.SelectedValue == null)
                return;
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                   ("SELECT CustName+'('+Code+')' as CustnameandCode FROM CRM.CustomerMaster where Acc_LedgerId='" + CmbLedgerId.SelectedValue.ToString() + "'")).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    Gramboo.General.ShowMessage(
                          "  You Have Already Created Customer with This Ledger (" + dt.Rows[0]["CustnameandCode"].ToString() + "). \n" +
                          "  Please Choose Another Ledger to Continue...!! \n", "WARNING!!!!", MessageBoxIcon.Warning, MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand("SELECT * FROM PUR.SupplierMaster where Acc_LedgerID='" + CmbLedgerId.SelectedValue.ToString() + "'")).Tables[0])
                if (dt.Rows.Count > 0)
                {
                    txtCustomerName.Text = dt.Rows[0]["SuppName"] == null ? "" : dt.Rows[0]["SuppName"].ToString();
                    txtAddr1.Text = (dt.Rows[0]["SuppAddr1"] == DBNull.Value ? "" : dt.Rows[0]["SuppAddr1"].ToString());
                    txtAddr2.Text = (dt.Rows[0]["SuppAddr2"] == DBNull.Value ? "" : dt.Rows[0]["SuppAddr2"].ToString());
                    cmbCustType.SelectedValue = 110100000004;
                    txtPin.Text = dt.Rows[0]["SuppPin"] == DBNull.Value ? "0" : dt.Rows[0]["SuppPin"].ToString();
                    txtDefaultTouch.Text = dt.Rows[0]["SuppDefltTouch"] == DBNull.Value ? "0" : dt.Rows[0]["SuppDefltTouch"].ToString();
                    txt_cuatpanno.Text = dt.Rows[0]["suppPanNo"] == DBNull.Value ? "0" : dt.Rows[0]["suppPanNo"].ToString();
                    txt_custgstno.Text = dt.Rows[0]["suppGstNo"] == DBNull.Value ? "0" : dt.Rows[0]["suppGstNo"].ToString();
                    txtMobile.Text = dt.Rows[0]["SuppContPhone"] == DBNull.Value ? "0" : dt.Rows[0]["SuppContPhone"].ToString();
                    cmb_state.SelectedValue = dt.Rows[0]["StateCode"] == DBNull.Value ? "0" : dt.Rows[0]["StateCode"].ToString();
                    txtPlace.Text = dt.Rows[0]["SuppPlace"] == DBNull.Value ? "0" : dt.Rows[0]["SuppPlace"].ToString();
                    cmb_country.Text = "INDIA";
                }
        }

        private void RbTDS_CheckedChanged(object sender, EventArgs e)
        {
            if (RbTDS.Checked == false && RbTCS.Checked == false) { CmbTdsid.Enabled = false; CmbTcsId.Enabled = false; }
            if (RbTDS.Checked == true) { CmbTcsId.SelectedValue = 0; CmbTcsId.Visible = false; CmbTdsid.Visible = true; CmbTdsid.Enabled = true; }
        }

        private void RbTCS_CheckedChanged(object sender, EventArgs e)
        {
            if (RbTDS.Checked == false && RbTCS.Checked == false) { CmbTdsid.Enabled = false; CmbTcsId.Enabled = false; }
            if (RbTCS.Checked == true) { CmbTdsid.SelectedValue = 0; CmbTdsid.Visible = false; CmbTcsId.Visible = true; CmbTcsId.Enabled = true; }
        }

        private void RbNone_CheckedChanged(object sender, EventArgs e)
        {
            if (RbNone.Checked == true)
            {
                CmbTdsid.SelectedValue = 0; CmbTcsId.Enabled = false;
                CmbTcsId.SelectedValue = 0; CmbTdsid.Enabled = false;
            }
        }

        private void RbTDS_SA_CheckedChanged(object sender, EventArgs e)
        {
            if (RbTDS_SA.Checked == false && RbTCS_SA.Checked == false) { CmbTdsid_SA.Enabled = false; CmbTcsId_SA.Enabled = false; }
            if (RbTDS_SA.Checked == true) { CmbTcsId_SA.SelectedValue = 0; CmbTcsId_SA.Visible = false; CmbTdsid_SA.Visible = true; CmbTdsid_SA.Enabled = true; }

        }

        private void RbTCS_SA_CheckedChanged(object sender, EventArgs e)
        {
            if (RbTDS_SA.Checked == false && RbTCS_SA.Checked == false) { CmbTdsid_SA.Enabled = false; CmbTcsId_SA.Enabled = false; }
            if (RbTCS_SA.Checked == true) { CmbTdsid_SA.SelectedValue = 0; CmbTdsid_SA.Visible = false; CmbTcsId_SA.Visible = true; CmbTcsId_SA.Enabled = true; }

        }

        private void RbNone_SA_CheckedChanged(object sender, EventArgs e)
        {
            if (RbNone_SA.Checked == true)
            {
                CmbTdsid_SA.SelectedValue = 0; CmbTcsId_SA.Enabled = false;
                CmbTcsId_SA.SelectedValue = 0; CmbTdsid_SA.Enabled = false;
            }
        }
    }
}







