using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Gramboo.Database;

namespace KGSJ.Forms.PUR
{
    public partial class SupplierMaster : Gramboo.Controls.GrbForm
    {


        private static SupplierMaster instance;


        public int TxType { get; set; }

        public static SupplierMaster Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SupplierMaster();
                }
                else if (instance.IsDisposed)
                {
                    instance = new SupplierMaster();
                }
                return instance;
            }
        }
        public SupplierMaster()
        {
            InitializeComponent();
        }


        public override bool GenerateID(Table table_name)
        {
            try
            {
                table_name.IdTextBox.Text = Kallans.Classes.Common.GetNextID(table_name, table_name.IdTextBox.DataField, DBConn, Convert.ToInt32(base.txtcompId.Text), Convert.ToInt32(base.txtBranchID.Text)).ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        public override void RefreshData()
        {
            Txt_txtype.Text = TxType.ToString() ;
            Gramboo.General.Setupcombo(cmbSuppType, "PUR.SupplierTypeMaster", "SuppTypeName", "SuppTypeId", "IsActive=1");
            Gramboo.General.Setupcombo(cmbSuppGroup, "PUR.SupplierGroupMaster", "SuppGroupName", "SuppGroupId", "IsActive=1");
            Gramboo.General.Setupcombo(cmbCountry, "GEN.CountryMaster", "CountryName", "CountryId", "IsActive=1");
            Gramboo.General.Setupcombo(cmbState, "GEN.StateMaster", "StateName", "StateId", "IsActive=1" + (cmbCountry.SelectedValue != null ? " AND CountryID=" + cmbCountry.SelectedValue : ""));
            Gramboo.General.Setupcombo(cmbDistrict, "GEN.DistrictMaster", "DistrictName", "DistrictId", "IsActive=1" + (cmbState.SelectedValue != null ? " AND StateID=" + cmbState.SelectedValue : ""));
            Gramboo.General.Setupcombo(Cmb_ModelName, "ITM.ModelMaster", "ModelName", "ModelId", "IsActive=1");
            if (this.txtcompId.Text.Trim() != "")
            {
                Gramboo.General.Setupcombo(cmbCompany, "SYST.CompanyMaster", "Comp_Name", "Comp_Id", "IsActive=1 AND Comp_id <> " + Convert.ToInt16(this.txtcompId.Text) + " ");
                Gramboo.General.Setupcombo(cmbLedger, "ACC.LedgerMaster", "Acc_LedgerName", "Acc_LedgerId", "IsActive=1 AND Company_id = " + Convert.ToInt16(this.txtcompId.Text));
                //new FA.AccountsHelper().LoadLedgerCombo(cmbLedger, this.DBConn, FA.MasterPageLedgerGroup.SupplierMaster, this.txtcompId.Text, this.txtBranchID.Text);
                cmbLedger.SelectedIndex = -1;
            }
        }


        public override bool InitializeTables()
        {
            Table t = new Table("KGSJLAst", "PUR", "SupplierMaster");
            t.PrimaryKeys.Add("SuppId");          
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = txtSuppId;
            Table t1 = new Table("KGSJLAst", "PUR", "SupplierMcWstSetting", true);
            t1.PrimaryKeys.Add("TransID");
            t1.FillView = new Table("KGSJLAst", "PUR", "VSupplierMcWstSetting", true);
            t1.DatagridView = dgv;
            t1.IsDatagridView = true;
            t1.IdTextBox = TxtTranscId;
            t.ChildTables.Add(t1);
            this.TableName = t;
            return true;
        }

        public override void Init()
        {
            base.Init();
            //this.ListForm = SupplierMasterList.Instance;

            rbtOrg.Checked = true;
            txtSupplierName.Focus();
           // chkISActive.Checked = true;
            //btnAddLedger.GrpId =FA.MasterPageLedgerGroup.SupplierMaster;
            cmbLedger.SelectedIndex = -1;
            txtSuppDefltTouch.Text = "100";
            TxtIsactive.Text = "1";
           // dgv.rowheader 
            dgv.DataFields = new List<string>() { "TransID", "SuppId", "ModelId", "ModelName", "Mc", "Wst", "IsActive" };
            dgv.HiddenDataFields = new List<string>() { "TransId", "SuppId", "ModelId", "IsActive" };
            dgv.SummaryColumns = new string[] { "Mc", "Wst" };
            dgv.Fill(new Table("KGSJLAst", "PUR", "VSupplierMcWstSetting", true), "1=2");
            AdjustColumnWidths();
        }
        private void AdjustColumnWidths()
        {
            dgv.RowHeadersVisible = false;
            dgv.Columns[0].Width = 40;

            dgv.Columns["ModelName"].Width = Cmb_ModelName.Width + 4;
            dgv.Columns["Mc"].Width = txtMc.Width + 4;
            dgv.Columns["Wst"].Width = txtWst.Width + 4;
            //dgv.Columns["IsActive"].Width = txtRemarksGrid.Width + 15;

        }

        private void cmbCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbCountry.ValueMember != "" && cmbCountry.SelectedValue != null)
                Gramboo.General.Setupcombo(cmbState, "GEN.StateMaster", "StateName", "StateId", "IsActive=1" + (cmbCountry.SelectedValue != null ? " AND CountryID=" + cmbCountry.SelectedValue : ""));

        }

        private void cmbState_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbState.ValueMember != "" && cmbState.SelectedValue != null)
            {
                Gramboo.General.Setupcombo(cmbDistrict, "GEN.DistrictMaster", "DistrictName", "DistrictId", "IsActive=1" + (cmbState.SelectedValue != null ? "AND StateId=" + cmbState.SelectedValue : ""));
            }
        }

        private void cmbLedger_DropDown(object sender, EventArgs e)
        {
            //Gramboo.General.Setupcombo(cmbLedger, "ACC.LedgerMaster", "Acc_LedgerName", "Acc_LedgerId", "IsActive=1 AND Company_id = " + Convert.ToInt16(this.txtcompId.Text));
            //new FA.AccountsHelper().LoadLedgerCombo(cmbLedger, this.DBConn, FA.MasterPageLedgerGroup.SupplierMaster, this.txtcompId.Text, this.txtBranchID.Text);
        }

        //private void btnAddLedger_AfterAddLedger(object sender, FA.LedgerControl.AddLegerArgs e)
        //{
        //    //new FA.AccountsHelper().LoadLedgerCombo(cmbLedger, this.DBConn,FA.MasterPageLedgerGroup.SupplierMaster, this.txtcompId.Text, this.txtBranchID.Text);
        //    cmbLedger.SelectedValue = e.LedgerId;
        //    cmbLedger.Select();
        //}

        private void btn_add_Click(object sender, EventArgs e)
        {
            //KGSJ.Forms.PUR.SupplierTypeMaster frm = new SupplierTypeMaster();
            //frm.MdiParent = this.ParentForm;
            //((frmMain)this.ParentForm).ShowChild(frm);
            //frm.Focus();
           
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
        //    KGSJ.Forms.PUR.SupplierGroupMaster frm = new SupplierGroupMaster();
        //    frm.MdiParent = this.ParentForm;
        //    ((frmMain)this.ParentForm).ShowChild(frm);
        //    frm.Focus();
        }

        private void cmbSuppType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbSuppType.SelectedValue != null)
            {
                if ((int)cmbSuppType.SelectedValue == 6 || (int)cmbSuppType.SelectedValue == 1)
                {
                    rbg_mode.Enabled = false;
                    if ((int)cmbSuppType.SelectedValue == 6)
                    {
                        rb_wt.Checked = true;
                    }
                    else
                    {
                        rb_cash.Checked = true;
                    }

                }
                else
                {
                    rbg_mode.Enabled = true;
                }
            }
            else
            {
                rbg_mode.Enabled = true; ;
            }

        }

        private void Btn_Add_Click_1(object sender, EventArgs e)
        {
            if (Cmb_ModelName.SelectedValue != null)
            {
                string lt = "0";
                foreach (DataGridViewRow r in dgv.Rows)
                {
                    if (lt.Contains(r.Cells["ModelName"].Value.ToString()) == false)
                    {
                        if (lt.Length > 0)
                        {
                            lt += "," + r.Cells["ModelName"].Value.ToString();
                        }
                        else
                        {
                            lt += r.Cells["ModelName"].Value.ToString();
                        }
                    }
                }
                if (lt.Contains(Cmb_ModelName.Text.ToString()))
                {
                    Cmb_ModelName.ShowMessage("Model Name Already Added");
                    return;
                }
                else
                {
                    txtModelId.Text = Cmb_ModelName.SelectedValue.ToString();   
                    dgv.Save();
                }
            }
        }

        private void cmbSuppType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grbButton1_Click_1(object sender, EventArgs e)
        {
            Save();
        }
    

        //private void cmbSuppType_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    if (cmbSuppType.SelectedValue != null)
        //    {
        //        if ((int)cmbSuppType.SelectedValue == 6 || (int)cmbSuppType.SelectedValue == 1)
        //        {
        //            rbg_mode.Enabled = false;
        //            if ((int)cmbSuppType.SelectedValue == 6)
        //            {
        //                rb_wt.Checked = true;
        //            }
        //            else
        //            {
        //                rb_cash.Checked = true;
        //            }

        //        }
        //        else
        //        {
        //            rbg_mode.Enabled = true;
        //        }
        //    }
        //    else
        //    {
        //        rbg_mode.Enabled = true; ;
        //    }

        //}








    }
}
