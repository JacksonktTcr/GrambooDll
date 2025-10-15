using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace KallansCare.Forms.SCH
{
    public partial class AkshahayanidhiJoiningEntry : Gramboo.Controls.GrbForm
    {
        private static AkshahayanidhiJoiningEntry instance;
        //private KallansCare.Forms.CRM.frmCustomerMaster Crm = new CRM.frmCustomerMaster();


        public static AkshahayanidhiJoiningEntry Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AkshahayanidhiJoiningEntry();
                }
                else if (instance.IsDisposed)
                {
                    instance = new AkshahayanidhiJoiningEntry();
                }
                return instance;
            }
        }
        public AkshahayanidhiJoiningEntry()
        {
            InitializeComponent();
            txt_totalnos.Text = "";
        }
        public override bool GenerateID(Table table_name)
        {
            try
            {
                table_name.IdTextBox.Text = REGAL.Classes.Common.GetNextID(table_name, table_name.IdTextBox.DataField, DBConn, Convert.ToInt32(base.txtcompId.Text), Convert.ToInt32(base.txtBranchID.Text)).ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public override bool InitializeTables()
        {
            Table t = new Table(REGAL.Classes.Common.DbName, "SCH", "AkshayanidhiJoiningEntry ");
            t.PrimaryKeys.Add("JoinId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = Txt_joinid;
            this.TableName = t;
            return true;
        }

        public override void RefreshData()
        {
            base.RefreshData();
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            cmb_VoucherTypeId.SelectedValue = 2;
            // cmb_VoucherTypeId.SelectedValue = (int)KallansCare.Classes.VoucherTypes.AkshayaJoiningEntry;

            if (!IsEditMode)
                txt_receiptNo.Text = REGAL.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_vchDate.Value,
            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));

            Gramboo.General.Setupcombo(cmb_Area, "SCH.AreaMaster", "AreaName", "AreaId");// " Company_id=" + txtcompId.Text + " AND Branch_Id=" + txtBranchID.Text);
            Gramboo.General.Setupcombo(cmb_schemetype, "SCH.TypeMaster", "TypeName", "TypeId", "IsActive='True'");
            //PopulateGrid();
            SetRates();
            txt_totalnos.Text = "";
        }
        public override void Init()
        {
           
            base.Init();
            txt_SchemeId.Focus();
            TxtIsactive.Text = "1";
            grb_MsgDelivered.Checked = true;
            chclctagent.Checked = true;
            rbcare.Checked = true;
            SetRates();
            //AdjustColumnsWidth();
            //txt_totalnos.Text ="12";
            txt_instAmount.Text = ""; 
            //this.ListForm = AkshayanidhiJoiningList.Instance;
            

        }
        private void GetWt()
        {
            float PaymentAmnt = 0, goldrate = 0, golwt = 0;// salesAmt = 0, OpenBalance = 0;

            PaymentAmnt = (Convert.ToSingle(txt_instAmount.Text == "" ? "0" : txt_instAmount.Text));
            goldrate = (Convert.ToSingle(txt_GoldRate.Text == "" ? "0" : txt_GoldRate.Text));


            // salesAmt = (Convert.ToSingle(txtSalesAmount.Text == "" ? "0" : txtSalesAmount.Text));

            if (PaymentAmnt != 0 && goldrate != 0)
            {
                golwt = PaymentAmnt / goldrate;
                txt_goldwt.Text = golwt.ToString("f3");
            }
            else
            {
                txt_goldwt.Text = "";
            }
        }

        private void Cmb_SchemeName_SelectedValueChanged(object sender, EventArgs e)
        {


        }
        private void Cmb_CustName_SelectedValueChanged(object sender, EventArgs e)
        {
        }


        //private void PopulateGrid()
        //{
        //    dgv.EntryFormName = this;
        //    dgv.ShowEdit = true;
        //    dgv.IsList = true;
        //    dgv.AutoGenerateColumns = true;
        //    dgv.DataFields = new List<string>() { "SchemeNo","ReceiptNo", "VchDate","CustomerName","HouseName",
        //      "Address1","Address2","AreaName","PhNo","MobileNo","TypeName","GoldRate","InstAmount","NoOfInstlment","Goldweight","VchNo","EndDate","PromoterName","NomineeName","NomineeAddress1","NomineeAddress2","NomineePhNo","[Created By]", "[Created Date]", "[Last Modified By]", "[Last Modified Date]","[Is Active]"};
        //    dgv.HiddenDataFields = new List<string>() {"ReceiptNo","VchDate","EmpName","HouseName",
        //      "Address2","AreaName","GoldRate","NoOfInstlment","EndDate","PromoterName","NomineeName","NomineeAddress1","NomineeAddress2","NomineePhNo","[Created By]", "[Created Date]", "[Last Modified By]", "[Last Modified Date]",
        //        "Branch_id","Counter_id"};
        //    dgv.Fill(new Table(KallansCare.Classes.Common.DbName, "SCH", "VAkshayanidhiJoiningEntry", true));
        //}


        //public void AdjustColumnsWidth()
        //{
        //    dgv.Columns["SchemeNo"].Width = 80;
        //    dgv.Columns["VchDate"].Width = 90;
        //    dgv.Columns["TypeName"].Width = 120;
        //    dgv.Columns["CustomerName"].Width = 130;
        //    dgv.Columns["Address1"].Width = 100;
        //    dgv.Columns["PhNo"].Width = 100;
        //    dgv.Columns["MobileNo"].Width = 100;
        //    dgv.Columns["EndDate"].Width = 100;
        //    dgv.Columns["InstAmount"].Width = 100;
        //    dgv.Columns["Created By"].Width = 100;
        //    dgv.Columns["Created Date"].Width = 125;
        //    dgv.Columns["Last Modified By"].Width = 125;
        //    dgv.Columns["Last Modified Date"].Width = 140;
        //    dgv.Columns["Is Active"].Width = 75;
        //}


        private void cmb_empname_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LnkOldGoldReceipt_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }


        private void Cmb_CustName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Cmb_SchemeName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Dtp_JoinDate_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void SchemeJoiningEntry_Load(object sender, EventArgs e)
        {

        }
        private void SetRates()
        {

            if (Dtp_vchDate.Value != null || txtcompId.Text != null)
            {
                try
                {
                    txt_GoldRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate  from GEN.MetalRate   WHERE  EntryDate<= '" + Dtp_vchDate.Value.Date.ToString("dd-MMM-yyyy") + "'Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString();
                }
                catch
                {
                }
            }
        }

        private void rbcare_CheckedChanged(object sender, EventArgs e)
        {
            if (rbcare.Checked)
            {
                //rbcare.Focus();
                lblrcvdbrnch.Visible = true;
                lbl_jewellaryName.Visible = false;
                Gramboo.General.Setupcombo(cmb_party, "SYST.BranchMaster", "BranchName", "BranchId", "IsActive='True'");
                //Gramboo.General.Setupcombo(cmb_salesman, "EMP.EmployeeMaster", "EmpName", "EmpId", "IsActive='True'and EmpTypeId=2");

            }
            else
            {
               
                lblrcvdbrnch.Visible = false;
            }
        }

        private void rbshop_CheckedChanged(object sender, EventArgs e)
        {
            if (rbshop.Checked)
            {
            
                lbl_jewellaryName.Visible = true;
                
                Gramboo.General.Setupcombo(cmb_party, "GEN.JewelleryMasterNew", "ShopName", "ShopId", "IsActive='True'");
                //Gramboo.General.Setupcombo(cmb_salesman, "EMP.EmployeeMaster", "EmpName", "EmpId", "IsActive='True'and EmpTypeId=3" );

            }
            else
            {
                lbl_jewellaryName.Visible = false;
               
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
           
        }

        private void txt_GoldRate_TextChanged(object sender, EventArgs e)
        {
            GetWt();
        }

        private void txt_instAmount_TextChanged(object sender, EventArgs e)
        {
            GetWt();
        }

        private void cmb_salesman_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_SchemeId_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmb_party_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_party.SelectedValue != null)
            {
                Gramboo.General.Setupcombo(cmb_salesman, "EMP.EmployeeMaster", "EmpName", "EmpId", "IsActive='True' And PartyId=" + cmb_party.SelectedValue);
                //Gramboo.General.Setupcombo(cmb_salesman, "EMP.EmployeeMaster", "EmpName", "EmpId", "IsActive='True' And PartyId=" + cmb_party.SelectedValue);
 
            }

        }
        private void SchemeType()
        {
            if (cmb_schemetype.SelectedValue != null)
            {
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
                ("Select InstallmentNo,Amount FROM SCH.VAmountTypeMaster WHERE TypeId='"
                + cmb_schemetype.SelectedValue + "'")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {

                        txt_totalnos.Text = (dt.Rows[0]["InstallmentNo"] == DBNull.Value ? "" : dt.Rows[0]["InstallmentNo"].ToString());
                        txt_instAmount.Text = (dt.Rows[0]["Amount"] == DBNull.Value ? "" : dt.Rows[0]["Amount"].ToString());
                    }
                }
            }
        }
        public override bool Save()
        {
            {
                if (base.Save())
                {
                    Init();
                    return true;
                }
                else
                    return false;
            }
        }
        private void cmb_salesman_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           

        }

        private void lbl_jewellaryName_Click(object sender, EventArgs e)
        {

        }

        private void cmb_schemetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            SchemeType();
        }

        private void txt_totalnos_TextChanged(object sender, EventArgs e)
        {
            if (txt_totalnos.Text != "0")
            {
                dtpend.Value = Dtp_vchDate.Value.AddMonths(Convert.ToInt32(txt_totalnos.Text));
                dtpclosing.Value = Dtp_vchDate.Value.AddMonths(Convert.ToInt32(txt_totalnos.Text));
            }
        }

        private void dtpend_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Lbl_minvalue_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Lbl_Sizename_Click(object sender, EventArgs e)
        {

        }
        public override bool FillData(Dictionary<string, object> PrimaryValues)
        {
            bool flag = false;
            if (base.FillData(PrimaryValues))
            {
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand
               ("Select EmpId FROM SCH.AkshayanidhiJoiningEntry WHERE  SchemeNo='" + txt_SchemeId.Text+"'")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmb_salesman.SelectedValue = dt.Rows[0][0].ToString();
                    }
                 
                }
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private void grbCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (grbCheckBox1.Checked == true)
            {
                dtpissue.Visible = true;
            }
            else
            {
                dtpissue.Visible = false;
            }
        }

        private void grbCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (grbCheckBox2.Checked == true)
            {
                dtpduplicate.Visible = true;
            }
            else
            {
                dtpduplicate.Visible = false;
            }
        }
        public override bool ValidateControls()
        {

            if (!IsEditMode)
                txt_receiptNo.Text = REGAL.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_vchDate.Value,
            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            return true;
        }

        private void AkshahayanidhiJoiningEntry_Activated(object sender, EventArgs e)
        {
          
        }

        private void AkshahayanidhiJoiningEntry_Enter(object sender, EventArgs e)
        {
            txt_SchemeId.Focus();
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            Save();
        }
    }
}
