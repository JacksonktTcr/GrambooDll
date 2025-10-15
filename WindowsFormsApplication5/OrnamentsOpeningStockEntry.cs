using System;
using Gramboo.Database;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Kallans.Forms.STK
{
    public partial class OrnamentsOpeningStockEntry : Gramboo.Controls.GrbForm
    {
        private static OrnamentsOpeningStockEntry instance;

        public static OrnamentsOpeningStockEntry Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new OrnamentsOpeningStockEntry();
                }
                else if (instance.IsDisposed)
                {
                    instance = new OrnamentsOpeningStockEntry();
                }
                return instance;
            }
        }
        public OrnamentsOpeningStockEntry()
        {
            InitializeComponent();
        }
        public override bool FillData(Dictionary<string, object> PrimaryValues)
        {
            if (base.FillData(PrimaryValues))
            {
                AdjustColumnWidths();
                return true;
            }
            else
            {
                return false;
            }
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

        public override bool InitializeTables()
        {
            Table t = new Table(Kallans.Classes.Common.DbName, "STK", "OpeningOpOrnStockMaster");
            t.PrimaryKeys.Add("EntryId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = txtEntryNo;
            Table t1 = new Table(Kallans.Classes.Common.DbName, "STK", "OpeningOrnStockMaterials", true);
            t1.PrimaryKeys.Add("TransId");
            t1.FillView = new Table(Kallans.Classes.Common.DbName, "STK", "VOrnOpeningStockMaterials");
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
            txtcompId.Text = "1";
            txtBranchID.Text = "101";
            //this.ListForm = OrnamentsOpeningStockEntryList.Instance;
            dgv.ShowSerialNo = true;
            TxtIsactive.Text = "1";
            dgv.IsDataEntryGrid=true;
            //Cmb_DepartmentName.Enabled = true;
            dgv.DataFields = new List<string>() { "EntryId", "TransId", "ItemId", "[Item Name]","PurityId" , "Purity", "Qty", "Wt", "DiaNo", "DiaWt", "StoneWt", "NetWt", "Company_id ", "Branch_id", "Counter_id", "IsActive" };
            dgv.HiddenDataFields = new List<string>() { "EntryId", "TransId", "ItemId", "Company_id", "Branch_id", "Counter_id", "PurityId", "IsActive" };
            dgv.SummaryColumns = new string[] { "Qty", "Wt","DiaNo","DiaWt","StoneWt", "NetWt" };
            dgv.Fill(new Table(Kallans.Classes.Common.DbName, "STK", "VOrnOpeningStockMaterials", true), "1=2");
            dgv.Columns["col_AutoSlno"].DataPropertyName = "SlNo";
            AdjustColumnWidths();
         
            //Txtproductcode.CheckDuplicates = true;


            if (this.TableName != null)
                GenerateID(this.TableName);
            Dtp_dt.Focus();

        }
        public override void RefreshData()
        {
            base.RefreshData();
            txtcompId.Text = "1";
            txtBranchID.Text = "101";
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True' ");
            cmb_VoucherTypeId.SelectedValue = 22;
            if (!IsEditMode)
                TxtVoucherNo.Text = Kallans.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, Dtp_dt.Value,
             DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
           //Gramboo.General.Setupcombo(Cmb_DepartmentName, "SYST.VUserDepartmentAccess", "DeptName", "DeptID", "AllowAccess='True' AND user_id=" + txtCrUserId.Text +" And company_id="+ txtcompId.Text);      
          //  Gramboo.General.Setupcombo(Cmb_DepartmentName, "STK.DepartmentMaster", "DeptName", "DeptID", "IsActive='True'AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
            Gramboo.General.Setupcombo(cmbItem, "ITM.VItemMaster", "[Item Name]", "ItemId", "[Is Active]='True'");
            Gramboo.General.Setupcombo(cmb_purity, "ITM.PurityMaster", "PurityName", "PurityId", "IsActive='True'");
           // Gramboo.General.Setupcombo(cmbUCCode, "AMYIMG.dbo.VProductImages", "UCCode", "ImageID");
            //Gramboo.General.Setupcombo(Cmb_CertiCenter, "PROD.VCertificationCenters", "[Supplier Name]", "SuppId", "IsActive='True'AND Company_id=" + txtcompId.Text);
            //Cmb_DepartmentName.SelectedValue = ((frmMain)this.ParentForm).dept;  


            //Gramboo.General.Setupcombo(Cmb_ProdCode, "STK.ProductCodeMaster", "ProdCode", "ProdCodeId", "IsActive='True' AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);


        }


        private void NetWt()
        {
            txtnetwt.Text = (Convert.ToSingle(TxtWt.Text) - Convert.ToSingle(TxtStonewt.Text)).ToString("f2");
        }




        private void AdjustColumnWidths()
        {
            dgv.RowHeadersVisible = false;
            dgv.Columns[0].Width = 35;
            dgv.Columns["Item Name"].Width = cmbItem.Width + 4;
            dgv.Columns["Purity"].Width = cmb_purity.Width + 3;
            dgv.Columns["Qty"].Width = TxtQty.Width + 1;
            dgv.Columns["Wt"].Width = TxtWt.Width +  4;
            dgv.Columns["DiaNo"].Width = txt_DiaNo.Width + 5;
            dgv.Columns["Diawt"].Width = txt_Diawt.Width + 0;
            dgv.Columns["StoneWt"].Width = TxtStonewt.Width + 2;
            //dgv.Columns["CertifNo"].Width = txtCertiNo.Width + 0;
            //dgv.Columns["Certification Agency"].Width = Cmb_CertiCenter.Width + 0;
        }

        //private void btn_add_Click(object sender, EventArgs e)
        //{
          
        //    if (Cmb_SizeRange.Enabled == false)
        //    {
        //        txtSizeRangeId.Text = "0";
        //    }
        //    else
        //    {
        //        txtSizeRangeId.Text = Cmb_SizeRange.SelectedValue.ToString();
        //    }

        //    if (Cmb_PriceRange.Enabled == false)
        //    {
        //        txtPriceRangeId.Text = "0";
        //    }
        //    else
        //    {
        //        txtPriceRangeId.Text = Cmb_PriceRange.SelectedValue.ToString();
        //    }
        //    if (Cmb_Sieve.Enabled == false)
        //    {
        //        txtSieveId.Text = "0";
        //    }
        //    else
        //    {
        //        txtSieveId.Text = Cmb_Sieve.SelectedValue.ToString();
        //    }


        //    if (Cmb_ItemName.SelectedValue != null) 
        //    {
              
        //        txtitemid.Text = Cmb_ItemName.SelectedValue.ToString();
                
        //        dgv.Save();

        //    }
        //       Cmb_ItemName.Focus();
                
        //}
        //public void enable()
        //{
        //    if (Cmb_ItemName.SelectedValue != null)
        //    {
        //        AMY.Classes.Common.enableMaterialsSpecs(Convert.ToInt32(Cmb_ItemName.SelectedValue), Cmb_SizeRange, Cmb_PriceRange, Cmb_Sieve, DBConn);
        //    }
        //}

        private void Cmb_ItemName_SelectedValueChanged(object sender, EventArgs e)
        {
            //enable();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (cmbItem.SelectedValue != null)

            {
                txtItemId.Text = cmbItem.SelectedValue.ToString();
                

 
            }
            if (cmb_purity.SelectedValue == null)
            {
                cmb_purity.ShowMessage("Enter Purity");
                return;
            }
            else
            {
                txtpuritryid.Text = cmb_purity.SelectedValue.ToString();
  
            }
            //if (cmbUCCode.SelectedValue == null)
            //{
            //    txtImageId.Text = "0";

            //}
            //else
            //{
            //    txtImageId.Text = cmbUCCode.SelectedValue.ToString();
            //}
            if (TxtQty.Text == "")
            {
                TxtQty.Text = "0";
            }
            if (txt_DiaNo.Text == "")
            {
                txt_DiaNo.Text = "0";

            }
            if (txt_Diawt.Text == "")
            {
                txt_Diawt.Text = "0";
            }
            //if (txtStoneNo.Text == "")
            //{
            //    txtStoneNo.Text = "0";

            //}
            if (TxtStonewt.Text == "")
            {
                TxtStonewt.Text = "0";
            }
            //if (Cmb_CertiCenter.SelectedValue != null)
            //{
            //    txtcerti_Id.Text = Cmb_CertiCenter.SelectedValue.ToString();
            //}
            //else
            //{
            //    txtcerti_Id.Text = "0";
            //}

        
            //txtProdCodeid.Text = (Cmb_ProdCode.SelectedValue == null ? "0" : Cmb_ProdCode.SelectedValue.ToString());




            //txtProdCodeid.Text = "0";

                //if (checkDuplicate())
                //{
                    dgv.Save();
                //}
                    cmbItem.Focus();
        }

        private void OrnamentsOpeningStockEntry_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void cmbItem_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (cmbItem.SelectedValue != null)
            //{
            //    Gramboo.General.Setupcombo(cmb_purity, "AMY.ITM.VItemMaster", "[Purity Name]", "PurityId", "ItemId=" + cmbItem.SelectedValue);
            //    Gramboo.General.Setupcombo(cmbUCCode, "AMYIMG.dbo.VProductImages", "UCCode", "ImageID", "ItemId=" + cmbItem.SelectedValue);

            //}



            if (cmbItem.SelectedValue != null)
            {
                if (txtcompId.Text.Length == 0)
                {
                    return;
                }

                if (cmbItem.SelectedIndex != -1)
                {
                    using (DataTable dt = DBConn.GetData(new SqlCommand("select PurityId FROM ITM.VItemMaster WHERE ItemId=" + cmbItem.SelectedValue.ToString() + "")).Tables[0])
                    {
                        if (dt.Rows.Count > 0)
                        {

                            cmb_purity.SelectedValue = dt.Rows[0]["PurityId"];
                         

                        }
                        else
                        {
                          //  cmbUCCode.SelectedValue = 0;

                        }

                    }




                }
            }
        }

     
        
        //public bool checkDuplicate()
        //{

          

        //    if (dgv.DataSource != null)
        //    {
        //        foreach (DataGridViewRow r in dgv.Rows)
        //        {
        //            if (Txtproductcode.Text != null)
        //            {
        //                if (dgv.Edit)
        //                {
        //                    if (r.Cells["Product Code"].Value.ToString() == Txtproductcode.Text)
        //                    {
        //                        //Txtproductcode.ShowMessage("ProductCode Already Added");
        //                        return true;

        //                    }
                            
        //                }
        //                else if (r.Cells["Product Code"].Value.ToString() == Txtproductcode.Text)
        //                {
        //                    Txtproductcode.ShowMessage("ProductCode Already Added");
        //                    return false;
 
        //                }
        //            }

        //                return true;
 

        //            }
                
        //                return true;
 
                    
        //        }


        //    return true;



        //}




        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (cmbItem.SelectedValue != null)
            //{
            //    if (txtcompId.Text.Length == 0)
            //    {
            //        return;
            //    }

            //    if (cmbItem.SelectedIndex != -1)
            //    {
            //        using (DataTable dt = DBConn.GetData(new SqlCommand("select ImageID FROM STK.ProductCodeMaster WHERE ItemId=" + cmbItem.SelectedValue.ToString() + "")).Tables[0])
            //        {
            //            if (dt.Rows.Count > 0)
            //            {


            //                cmbUCCode.SelectedValue = dt.Rows[0]["ImageID"];


            //            }
            //            else
            //            {
            //                cmbUCCode.SelectedValue = 0;

            //            }

            //        }



            //    }
            //}
        }

        //private void Txtproductcode_TextChanged(object sender, EventArgs e)
        //{
        //    if (Txtproductcode.Text.Trim().Length >0)
        //    {
        //        using (DataTable dt = DBConn.GetData(new SqlCommand("Select ItemId,PurityId,Gwt,StoneWt,Qty FROM STK.ProductCodeMaster WHERE ProdCode='" + Txtproductcode.Text.ToString() + "' AND IsActive='True' AND Company_id=" + txtcompId.Text + " AND Branch_Id=" + txtBranchID.Text)).Tables[0])
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                cmbItem.SelectedIndex = -1;
        //                cmbItem.SelectedValue = dt.Rows[0]["ItemId"];
        //                cmb_purity.SelectedValue = dt.Rows[0]["PurityId"];
        //                cmbUCCode.SelectedValue = dt.Rows[0]["ImageID"];
        //                TxtWt.Text = dt.Rows[0]["Gwt"].ToString();
        //                txtDiaNo.Text = dt.Rows[0]["DiaNo"].ToString();
        //                txtDiawt.Text = dt.Rows[0]["DiaWt"].ToString();
        //                 txtStoneNo.Text = dt.Rows[0]["StoneNo"].ToString();
        //                TxtStonewt.Text = dt.Rows[0]["StoneWt"].ToString();
        //                TxtQty.Text = dt.Rows[0]["Qty"].ToString();
        //                if (dt.Rows[0]["CertifCenter"].ToString() != "")
        //                    Cmb_CertiCenter.SelectedValue = dt.Rows[0]["CertifCenter"].ToString();
        //                txtCertiNo.Text = dt.Rows[0]["CertifNo"].ToString();

        //            }
        //            else
        //            {

        //                cmbItem.Text = "";
        //                cmbUCCode.SelectedValue = 0;
        //                TxtWt.Text = "";
        //                txtDiaNo.Text = "";
        //                 txtDiawt.Text = "";
        //                txtStoneNo.Text = "";
        //                TxtStonewt.Text = "";
        //                TxtQty.Text = "";
        //                Cmb_CertiCenter.Text = "";
        //                txtCertiNo.Text = "";
        //            }

        //            if 
        //        }

        //    }
        //}

        private void Txtproductcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue  == 13)
            {
                btn_add_Click(sender, new EventArgs());
            }
        }

        private void TxtWt_TextChanged(object sender, EventArgs e)
        {
            NetWt();
        }

        private void TxtStonewt_TextChanged(object sender, EventArgs e)
        {
            NetWt();
        }

        private void TxtVoucherNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Diawt_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        

    }
}
