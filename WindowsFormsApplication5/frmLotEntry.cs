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

namespace SAFA.Forms.PUR
{
    public partial class frmLotEntry : Gramboo.Controls.GrbForm
    {
        bool flag = false, editflag = false;
        bool printflag = false; bool checkQc;
        private static frmLotEntry instance;

        public static frmLotEntry Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new frmLotEntry();
                }
                else if (instance.IsDisposed)
                {
                    instance = new frmLotEntry();
                }
                return instance;
            }
        }
        public frmLotEntry()
        {
            InitializeComponent();
            string qc;
            using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                     ("select Qc from SYST.Settings")).Tables[0])
                qc = dt.Rows[0]["Qc"].ToString();
            if (qc == "True")
            {
                checkQc = true;
            }
            else
            {
                cmb_purpose.SelectedValue = "BE";
                //

            }
            cmb_trans.Items.Clear();
            cmb_trans.Items.Add("StockTransfer");
            cmb_trans.Items.Add("Purchase");  
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
            Table t = new Table(SAFA.Classes.Common.DbName, "PUR", "LotEntry");
            t.PrimaryKeys.Add("LotId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = txtId;

            Table t1 = new Table(SAFA.Classes.Common.DbName, "PUR", "LotEntryDetails", true);
            t1.PrimaryKeys.Add("LotDetId");
            t1.FillView = new Table(SAFA.Classes.Common.DbName, "PUR", "VLotEntry", true);
            t1.DatagridView = dgv;
            t1.IsDatagridView = true;
            t1.IdTextBox = txtdetailsId;
            t.ChildTables.Add(t1);

            this.TableName = t;
            return true;
        }
  

        public override void Init()
        {
            base.Init();
            this.ListForm = FrmLotEntryList.Instance;
            label12.Visible = false;
            cmb_selectbrch.Visible = false;
            chkISActive.Checked = true;
            LoadGrid(false);
            if (checkQc==false)
            {
                cmb_purpose.SelectedValue = "BE";
                cmb_purpose.Enabled = false;
               
            }


        }
        public override void RefreshData()
        {
            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            Gramboo.General.Setupcombo(grbCmb_Purity, "ITM.PurityMaster", "PurityName", "PurityId", "IsActive='True'");

            cmb_VoucherTypeId.SelectedValue = 209;
            if (!IsEditMode)
                txtLotNo.Text = SAFA.Classes.Common.GetNextVoucherNoTax((int)cmb_VoucherTypeId.SelectedValue, grbDTPicker1.Value,
                DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));

            Gramboo.General.Setupcombo(cmbBranch, "syst.branchmaster", "BranchName", "BranchId", "[IsActive]='True'");

            cmbpartytype.SelectedValueChanged -= cmbpartytype_SelectedValueChanged;
            Gramboo.General.Setupcombo(cmbpartytype, "STK.VSTPartyType", "Type", "Id", "IsActive='True'");
            cmbpartytype.SelectedValueChanged += cmbpartytype_SelectedValueChanged;

            Gramboo.General.Setupcombo(cmbitemname, "ITM.VItemMaster", "[Item Name]", "ItemId", "[Is Active]='True'");
            //Gramboo.General.Setupcombo(cmb_selectbrch, "SYST.BranchMaster", "BranchName", "BranchId", "[IsActive]='True'");
            Gramboo.General.Setupcombo(cmb_purpose, "Pur.VlotPurpose", "PurposeName", "Purpose");
           
            if (txtBranchID.Text != "")
            {
                cmbBranch.SelectedValue = txtBranchID.Text;
            }
            TxtDeptId.Text = "101";
        }
        public override bool ValidateControls()
        {
            if(cmbsupplier.SelectedValue == null)
            {
                Gramboo.General.ShowMessage("Select Supplier Name");
                return false;

            }
            else if(cmb_trans.Text.Length < 5)
            {
                Gramboo.General.ShowMessage("Select Trans Type");
                return false;
                
            }
            else if(dgv.Rows.Count<1)
            {
                Gramboo.General.ShowMessage("Enter Item Details");
                return false;

            }
            //if(cmb_purpose.SelectedValue.ToString() =="BE" &&cmb_selectbrch.SelectedValue==null)
            //{
            //    cmb_selectbrch.SelectedValue = txtBranchID.Text;
            //}
            double  PendGwt = 0,  EntryWt = 0, StoneEntryWt = 0, Diawt = 0, Stonewt = 0, DiaEntryWt = 0;
            if (!IsEditMode)
                txtLotNo.Text = SAFA.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, grbDTPicker1.Value,
            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));
            bool flag = true;
                      
            List<string> l = new List<string>();
            foreach (DataGridViewRow r in dgv.Rows)
            {
                if (!l.Contains(r.Cells["ReceiptId"].Value.ToString()))
                {
                    l.Add(r.Cells["ReceiptId"].Value.ToString());
                }
            }
            if (cmb_trans.Text != "Stock")
            {
                foreach (string s in l)
                {
                    //float Stonewt=0, Diawt=0;

                    EntryWt = Convert.ToInt64(((DataTable)dgv.DataSource).Compute("SUM(Gwt)", "ReceiptId=" + s));
                    StoneEntryWt = Convert.ToInt64(((DataTable)dgv.DataSource).Compute("SUM(StWt)", "ReceiptId=" + s));
                    DiaEntryWt = Convert.ToInt64(((DataTable)dgv.DataSource).Compute("SUM(Diawt)", "ReceiptId=" + s));
                    if (((DataTable)grb_pending.DataSource).Select("TransId=" + s).Length > 0)
                    {
                        PendGwt = Convert.ToInt64(((DataTable)grb_pending.DataSource).Compute("SUM(Wt)", "TransId=" + s));
                        Diawt = Convert.ToInt64(((DataTable)grb_pending.DataSource).Compute("SUM(diawt)", "TransId=" + s));
                        Stonewt = Convert.ToInt64(((DataTable)grb_pending.DataSource).Compute("SUM(stwt)", "TransId=" + s));
                    }
                    else
                    {

                        PendGwt = EntryWt;
                        Stonewt = StoneEntryWt;
                        Diawt = DiaEntryWt;
                    }
                    PendGwt = PendGwt + 2;
                    if (((EntryWt > PendGwt) || (StoneEntryWt > Stonewt) || (DiaEntryWt > Diawt)))
                    {
                        flag = false;
                        Gramboo.General.ShowMessage("Entered Wt  More Than Actual Wt ");
                    }
                }
            }
            return flag;
            
        }
        public override bool Save()
        {
            printflag = false;
            return Save(true);
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            if (cmb_trans.Text != "Stock")
            {
                if (txtreceiptid.Text == "0")
                {
                    Gramboo.General.ShowMessage("Pick A Pending Entry From The Grid To Continue..!!");
                    return;
                }
            }

            if (cmbitemname.SelectedValue != null)
            {
                double txtst, txtdwt;
                txtst= Convert.ToSingle(string.IsNullOrEmpty(txtstwt.Text.Trim()) ? "0.000" : txtstwt.Text);
                txtdwt=Convert.ToSingle(string.IsNullOrEmpty(txtdiawt.Text.Trim()) ? "0.000" : txtdiawt.Text);
                txtstwt.Text = txtst.ToString("f3");
                txtdiawt.Text = txtdwt.ToString("f3");
                dgv.Save();
                
            }
        }

        private void cmbitemname_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtitemname.Text = cmbitemname.Text.ToString();
        }

        public override bool FillData(Dictionary<string, object> PrimaryValues)
        {
            flag = true;
            if (base.FillData(PrimaryValues))
            {
                editflag = true;
                flag = false;
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                            ("SELECT PartyNameId  FROM PUR.VLotList WHERE LotId =" + (txtId.Text == null ? "0" : txtId.Text.ToString()) + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmbsupplier.SelectedValue = (dt.Rows[0]["PartyNameId"] == DBNull.Value ? "0" : dt.Rows[0]["PartyNameId"].ToString());
                    }
                }
                pendinggrid();
                
                return true;
            }
            else
            {
                return false;
            }
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            pendinggrid();
        }

        private void cmbsupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            pendinggrid();
        }

        private void cmb_trans_SelectedIndexChanged(object sender, EventArgs e)
        {
            pendinggrid();
        }

        private void grb_pending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = grb_pending.Rows[rowIndex];
            cmbitemname.Text = grb_pending.CurrentRow.Cells["ItemName"].Value.ToString();
            txtqty.Text = grb_pending.CurrentRow.Cells["Qty"].Value.ToString();
            txtreceiptid.Text = grb_pending.CurrentRow.Cells["TransId"].Value.ToString();
            txtgwt.Text = grb_pending.CurrentRow.Cells["Wt"].Value.ToString();
            txtstwt.Text = grb_pending.CurrentRow.Cells["stwt"].Value.ToString();
            txtdiawt.Text = grb_pending.CurrentRow.Cells["diawt"].Value.ToString();
            if (cmb_trans.SelectedIndex.ToString() == "2")
            {
                TxtTrackingId.Text = grb_pending.CurrentRow.Cells["TrackingID"].Value.ToString();
            }
            else { TxtTrackingId.Text = ""; }




        }

        private void pendinggrid()
        {
            if (cmbsupplier.SelectedValue != null)
            {
                if (txtBranchID.Text == "")
                    return;
                if (cmb_trans.Text == "Stock")
                {
                    grb_pending.DataSource = null;
                    return;
                }
                grb_pending.AutoGenerateColumns = true;
                grb_pending.ShowSerialNo = true;
                grb_pending.HiddenDataFields = new List<string> { "TransId", "TrackingID" };
                grb_pending.SummaryColumns = new string[] { "Qty", "Wt", "stwt", "stCtwt", "diawt" };
                if (!SAFA.Classes.Common.JobNoVlidate(DBConn, Convert.ToInt16(txtBranchID.Text)))
                {
                    grb_pending.HiddenDataFields.AddRange(new List<string> { "JobOrderId", "JobNo" });
                }
                if (checkQc == true)
                {
                    if (cmb_purpose.SelectedValue != null && cmb_purpose.SelectedValue.ToString() == "QC")
                    {
                        if (cmb_trans.SelectedIndex == 1)
                        {
                            grb_pending.DataSource = this.DBConn.GetData(new SqlCommand("Select TransId,VchNo,ItemName,Qty,Wt,stwt,diawt,VchDate  FROM PUR.PurchaseEntryPendingLot(" + cmbsupplier.SelectedValue.ToString() + "," + cmbBranch.SelectedValue.ToString() + "," + txtcompId.Text + "," + txtBranchID.Text + "," + (txtId.Text == "" ? "0" : txtId.Text) + ") t2 ")).Tables[0];
                        }
                        else
                        {
                            grb_pending.DataSource = this.DBConn.GetData(new SqlCommand("Select TransId,VchNo,ItemName,Qty,Wt,stwt,diawt,VchDate,TrackingID  FROM PUR.PurchasePendingLot(" + cmbsupplier.SelectedValue.ToString() + "," + cmb_trans.SelectedIndex.ToString() + "," + cmbBranch.SelectedValue.ToString() + "," + txtcompId.Text + "," + txtBranchID.Text + "," + (txtId.Text == "" ? "0" : txtId.Text) + ") t2 ")).Tables[0];
                        }
                    }
                    else
                    {
                        if (cmb_purpose.SelectedValue != null)
                        {
                            grb_pending.DataSource = this.DBConn.GetData(new SqlCommand("Select TransId,VchNo,ItemName,Qty,Wt,stwt,diawt,VchDate,TrackingID  FROM PUR.LotEntryPendingBarcode(" + cmbsupplier.SelectedValue.ToString() + "," + txtcompId.Text + "," + txtBranchID.Text + ",'" + cmb_trans.Text + "'," + (txtId.Text == "" ? "0" : txtId.Text) + ") t2 ")).Tables[0];
                        }
                    }
                }
                else
                {
                    if (cmb_trans.SelectedIndex == 1)
                    {
                        grb_pending.DataSource = this.DBConn.GetData(new SqlCommand("Select TransId,VchNo,ItemName,Qty,Wt,stwt,diawt,VchDate  FROM PUR.PurchaseEntryPendingLot(" + cmbsupplier.SelectedValue.ToString() + "," + txtBranchID.Text + "," + txtcompId.Text + "," + txtBranchID.Text + "," + (txtId.Text == "" ? "0" : txtId.Text) + ") t2 ")).Tables[0];
                    }
                    else
                    {
                        grb_pending.DataSource = this.DBConn.GetData(new SqlCommand("Select TransId,VchNo,ItemName,Qty,Wt,stwt,diawt,VchDate  FROM PUR.PurchasePendingLot(" + cmbpartytype.SelectedValue.ToString() + "," + cmbsupplier.SelectedValue.ToString() + "," + cmb_trans.SelectedIndex.ToString() + "," + cmbBranch.SelectedValue.ToString() + "," + txtcompId.Text + "," + txtBranchID.Text + "," + (txtId.Text == "" ? "0" : txtId.Text) + ") t2 ")).Tables[0];
                    }
                }
            }
            else
            {
                grb_pending.DataSource = null;
            }
        }
        private void txtgwt_TextChanged(object sender, EventArgs e)
        {
            Calc();
        }

        private void txtstwt_TextChanged(object sender, EventArgs e)
        {
            Calc();
        }

        private void txtdiawt_TextChanged(object sender, EventArgs e)
        {
            Calc();
        }

        private void cmb_trans_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            pendinggrid();
        }

        private void Calc()
        {
            double Gwt = 0, StWt = 0, DiaWt = 0, NetWt = 0, DiaWtt = 0;
            Gwt = Convert.ToSingle(string.IsNullOrEmpty(txtgwt.Text.Trim()) ? "0.000" : txtgwt.Text);
            StWt = Convert.ToSingle(string.IsNullOrEmpty(txtstwt.Text.Trim()) ? "0.000" : txtstwt.Text);
            DiaWt = Convert.ToSingle(string.IsNullOrEmpty(txtdiawt.Text.Trim()) ? "0.000" : txtdiawt.Text);


            String StUnitOfMesurment = "", DiaUnitOfMesurment = "";

            if (cmbitemname.SelectedValue != null)
            {
                using (System.Data.DataTable dt = DBConn.GetData(new SqlCommand
                                                  ("SELECT StUnitOfMesurment,DiaUnitOfMesurment FROM ITM.VItemMaster as t1 WHERE t1.ItemId=" + cmbitemname.SelectedValue + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        StUnitOfMesurment = dt.Rows[0]["StUnitOfMesurment"].ToString();
                        DiaUnitOfMesurment = dt.Rows[0]["DiaUnitOfMesurment"].ToString();
                    }
                }
            }

            if (Convert.ToSingle(string.IsNullOrEmpty(txtstwt.Text.Trim()) ? "0.000" : txtstwt.Text) != 0)
            {
                double StConwt = Convert.ToSingle(txtstwt.Text);

                if (StUnitOfMesurment == "Cent")
                {
                    StWt = Math.Round(StConwt, 3) / 500;
                }
                else if (StUnitOfMesurment == "Carat")
                {
                    StWt = Math.Round(StConwt, 3) * 0.2;
                }
                else if (StUnitOfMesurment == "Gram")
                {
                    StWt = StConwt;
                }
            }

            if (Convert.ToSingle(string.IsNullOrEmpty(txtdiawt.Text.Trim()) ? "0.000" : txtdiawt.Text) != 0)
            {
                double DiaConwt = Convert.ToSingle(txtdiawt.Text);

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



            DiaWtt = (DiaWt * (0.2));

            NetWt = (Gwt - (StWt + DiaWtt));
            txtnetwt.Text = NetWt.ToString("f3");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cmb_purpose_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(cmb_purpose.SelectedValue!=null)
            {
                
                if (cmb_purpose.SelectedValue.ToString() == "BE")
                {
                    label12.Visible = false;
                    cmb_selectbrch.Visible = false;
                    cmb_selectbrch.SelectedValue = -1;
                    //label12.Visible = true;
                    //cmb_selectbrch.Visible = true;
                    //cmb_selectbrch.SelectedValue = txtBranchID.Text;
                }
                else
                {
                    label12.Visible = false;
                    cmb_selectbrch.Visible = false;
                    cmb_selectbrch.SelectedValue = -1;
                }
            }
            pendinggrid();
        }

        private void cmb_selectbrch_SelectedIndexChanged(object sender, EventArgs e)
        {
            pendinggrid();
        }

        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //string temp;
            //temp=dgv.CurrentRow.Cells["ReceiptId"].Value.ToString();
            //grb_pending.ClearSelection();
            //foreach (DataGridViewRow row in grb_pending.Rows)
            //{
            //    if(row.Cells["TransId"].Value.ToString()==temp )
            //    {
            //        row.Selected = true;
            //    }
            //}
            
        }

        private void txtreceiptid_TextChanged(object sender, EventArgs e)
        {
            string s;
            if (txtreceiptid.Text != "")
            {
                foreach (DataGridViewRow r in grb_pending.Rows)
                {
                    if (r.Cells["TransId"].Value.ToString() == txtreceiptid.Text)
                    {
                        r.Selected = true;
                        grb_pending.CurrentCell = r.Cells["ItemName"];
                        grb_pending.Select();
                        //s = r.Cells["PercentageGrmRate"].Value.ToString();
                        ////s. = txt_grm.Text.ToString();
                        //if (txt_grm.Text == null || txt_grm.Text == "0" || txt_grm.Text == "0")
                        //{
                        //    txt_grm.Text = Convert.ToString(s);
                        //}
                    }
                }
            }
        }

        private void cmbpartytype_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbpartytype.SelectedValue == null)
                return;
            cmb_trans.SelectedValue = 0; cmb_trans.Text = "";
            cmbsupplier.SelectedValue = 0; cmbsupplier.Text = "";
            if (cmbpartytype.SelectedValue.ToString() == "1")
            {
                Gramboo.General.Setupcombo(cmbsupplier, "PUR.SupplierMaster", "SuppName", "SuppId", "IsActive='True'AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + " and SuppTypeId=1");
                cmbsupplier.DropDownWidth = 350;
            }
            else if (cmbpartytype.SelectedValue.ToString() == "2")
            {
                Gramboo.General.Setupcombo(cmbsupplier, "PUR.SupplierMaster", "SuppName", "SuppId", "IsActive='True'AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + "and SuppTypeId=6");
                cmbsupplier.DropDownWidth = 350;
            }
            else if(cmbpartytype.SelectedValue.ToString() == "0")
            {
                Gramboo.General.Setupcombo(cmbsupplier, "SYST.BranchMaster", "BranchName", "BranchId", "IsActive='True'AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + "");
                cmbsupplier.DropDownWidth = 150;
            }
            else if(cmbpartytype.SelectedValue.ToString() == "4")
            {
                Gramboo.General.Setupcombo(cmbsupplier, "STK.DepartmentMaster", "DeptName", "DeptID", "IsActive='True'AND Company_id=" + txtcompId.Text + " and Branch_id=" + txtBranchID.Text + "");
                cmbsupplier.DropDownWidth = 150;
            }
        }
        void LoadGrid(bool Fill)
        {

            dgv.DataFields = new List<string> {"LotDetId", "LotId", "ItemId", "[ItemName]","PurityId","PurityName","Touch",
            "Qty", "Gwt","Ot_Stno","Ot_Stwt","Ot_StRate","Ot_StAmt","Pr_Stno","Pr_Stwt","Pr_StRate","Pr_StAmt","Stno","StWt","StRate","StAmt","DiaNo","Diawt","DiaRate","DiaAmt","Netwt","MC", "ReceiptId", "TrackingID"};

            //dgv.HiddenDataFields = new List<string>();
            //dgv.HiddenDataFields = SetHiddenColumns((cmb_trans.Text != "" ? cmb_trans.Text.ToString() : "S"), (cmbpartytype.SelectedValue != null ? cmbpartytype.SelectedValue.ToString() : "1"), (CmbMetalType.SelectedValue != null ? CmbMetalType.SelectedValue.ToString() : "G"));


            dgv.HiddenDataFields = new List<string> { "LotDetId", "LotId", "ItemId", "PurityId", "TrackingID", "Company_id", "Branch_id", "IsActive", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Counter_id", "ReceiptId", "JobOrderId" };

            dgv.SummaryColumns = new string[] { "Qty", "Gwt", "Ot_Stno", "Ot_Stwt", "Ot_StRate", "Ot_StAmt", "Pr_Stno", "Pr_Stwt", "Pr_StRate", "Pr_StAmt", "Stno", "StWt", "StRate", "StAmt", "DiaNo", "Diawt", "DiaRate", "DiaAmt", "Netwt" ,"MC"};

            dgv.DataSource = null;

            if (Fill == false)
            {
                dgv.Fill(new Table(SAFA.Classes.Common.DbName, "PUR", "VLotEntry", true), "1=2");
            }
            else
            {
                dgv.Fill(new Table(SAFA.Classes.Common.DbName, "PUR", "VLotEntry", true), " Branch_id=" + txtBranchID.Text + " AND LotId=" + txtId.Text, " LotId");
            }
            dgv.AllowUserToAddRows = false;


            dgv.Columns["NetWt"].ReadOnly = true;
            dgv.Columns["DiaAmt"].ReadOnly = true;
            dgv.Columns["StAmt"].ReadOnly = true;
            dgv.Columns["Ot_StAmt"].ReadOnly = true;
            dgv.Columns["Pr_StAmt"].ReadOnly = true;

            if (dgv.Rows.Count == 0)
            {
                ((System.Data.DataTable)dgv.DataSource).Rows.Add(((System.Data.DataTable)dgv.DataSource).NewRow());
            }
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv.BeginEdit(false);
            AdjustSalesItemsColumns(dgv);
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.SelectedCells.Count > 0)
            {
                if (dgv.SelectedCells[0].OwningColumn.DataPropertyName == "ItemName")
                {
                    SetComboLocation(dgv, cmbitemname, dgv.SelectedCells[0].ColumnIndex, dgv.SelectedCells[0].RowIndex);
                }
                else if (dgv.SelectedCells[0].OwningColumn.DataPropertyName == "PurityName")
                {
                    SetComboLocation(dgv, grbCmb_Purity, dgv.SelectedCells[0].ColumnIndex, dgv.SelectedCells[0].RowIndex);
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

        private void cmbitemname_KeyDown(object sender, KeyEventArgs e)
        {
            ComboKeydown(dgv, cmbitemname, e);
        }
        private void ComboKeydown(Gramboo.Controls.GrbDataGridView dgv, Gramboo.Controls.GrbComboBox cmb, KeyEventArgs e)
        {

            if (flag)
                return;

            if (e.KeyValue == 13)
            {

                dgv.Focus();
                // SendKeys.Send("{Enter}");

                cmb.Visible = false;
            }

        }

        private void AfterComboLeave(Gramboo.Controls.GrbDataGridView dgv, Gramboo.Controls.GrbComboBox cmb, int valuecolumnindex = -1)
        {

            //if (flag || dgv.CurrentCell == null || dgv.CurrentCell.ReadOnly)
            //    return;


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

        private void cmbitemname_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv, cmbitemname, (dgv.Columns.Count > 0 ? dgv.Columns["ItemId"].Index : -1));
           
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void dgv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && dgv.CurrentCell.OwningColumn.DisplayIndex == dgv.Columns.Count - 1)
            {
                if (dgv.Rows.Count > 0)
                {
                    if (dgv.CurrentRow.Cells["ItemID"].Value.ToString() != "0" && dgv.CurrentRow.Index == dgv.Rows.Count - 1)
                    {
                      

                        ((System.Data.DataTable)dgv.DataSource).Rows.Add(((System.Data.DataTable)dgv.DataSource).NewRow());

                        dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                        dgv.CurrentCell = dgv.Rows[dgv.Rows.Count - 1].Cells[0];
                        e.Handled = true;
                        dgv.BeginEdit(false);
                    }
                }
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
       //     dgv.CurrentRow.Cells["StCtWt"].Value = Math.Round(StwtCt, 3).ToString("f3");
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
            dgv.CurrentRow.Cells["StAmt"].Value = Math.Round(StAmt, 2).ToString("f2");
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
            else { dgv.CurrentRow.Cells["StWt"].Value = 0; }

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
            else { dgv.CurrentRow.Cells["DiaWt"].Value = 0; }

            //     DiaStWt = Convert.ToSingle((StoneWt + (DiaWt * .2)));
            TotalWt = Gwt - StoneWt - (DiaWt * .2f);
            dgv.CurrentRow.Cells["NetWt"].Value = TotalWt.ToString("f3");
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
        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Columns[e.ColumnIndex].DataPropertyName == "Gwt" ||
             dgv.Columns[e.ColumnIndex].DataPropertyName == "StWt" ||
              dgv.Columns[e.ColumnIndex].DataPropertyName == "Diawt")
            {
                if (dgv.CurrentCell.OwningColumn.Name == "StWt")
                {
                    StoneCtwt(dgv);
                }

                CalculateWt(dgv);
            }
            else if (dgv.Columns[e.ColumnIndex].DataPropertyName == "Ot_Stwt" ||
            dgv.Columns[e.ColumnIndex].DataPropertyName == "Pr_Stwt" ||
            dgv.Columns[e.ColumnIndex].DataPropertyName == "Ot_Stno" ||
            dgv.Columns[e.ColumnIndex].DataPropertyName == "Pr_Stno")
            {
                if (dgv.CurrentCell.OwningColumn.Name == "Ot_Stwt")
                {
                    CalculateCash(dgv, "Ot_Stwt", "Ot_StRate", "Ot_StAmt");
                }
                if (dgv.CurrentCell.OwningColumn.Name == "Pr_Stwt")
                {
                    CalculateCash(dgv, "Pr_Stwt", "Pr_StRate", "Pr_StAmt");
                }
                CalculateStoneWT(dgv);
            }
            else if (dgv.Columns[e.ColumnIndex].DataPropertyName == "Ot_StRate")
            {
                if (dgv.CurrentCell.OwningColumn.Name == "Ot_StRate")
                {
                    CalculateCash(dgv, "Ot_Stwt", "Ot_StRate", "Ot_StAmt");
                    CalculateStoneWT(dgv);
                }
            }
            else if (dgv.Columns[e.ColumnIndex].DataPropertyName == "Pr_StRate")
            {
                if (dgv.CurrentCell.OwningColumn.Name == "Pr_StRate")
                {
                    CalculateCash(dgv, "Pr_Stwt", "Pr_StRate", "Pr_StAmt");
                    CalculateStoneWT(dgv);
                }
            }
            else if (dgv.Columns[e.ColumnIndex].DataPropertyName == "StRate")
            {
                if (dgv.CurrentCell.OwningColumn.Name == "StRate")
                {
                    CalculateCash(dgv, "StWt", "StRate", "StAmt");
                }
            }
            else if (dgv.Columns[e.ColumnIndex].DataPropertyName == "DiaRate")
            {
                if (dgv.CurrentCell.OwningColumn.Name == "DiaRate")
                {
                    CalculateCash(dgv, "Diawt", "DiaRate", "DiaAmt");
                }
            }
        }
        
        private void SetColumnWidth(Gramboo.Controls.GrbDataGridView dgv, string ColumnName, int width)
        {
            if (dgv.Columns.Contains(ColumnName))
                dgv.Columns[ColumnName].Width = width;
        }

        private void grbCmb_Purity_KeyDown(object sender, KeyEventArgs e)
        {
            ComboKeydown(dgv, grbCmb_Purity, e);
        }

        private void grbCmb_Purity_Leave(object sender, EventArgs e)
        {
            AfterComboLeave(dgv, grbCmb_Purity, (dgv.Columns.Count > 0 ? dgv.Columns["PurityId"].Index : -1));
        }

        private void grbButton2_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void grb_pending_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AdjustSalesItemsColumns(Gramboo.Controls.GrbDataGridView dgv)
        {

            dgv.RowHeadersVisible = false;

            SetColumnWidth(dgv, "slno", 40);
            SetColumnWidth(dgv, "ItemName", 130);
            SetColumnWidth(dgv, "Touch", 50);
            SetColumnWidth(dgv, "Qty", 40);
            SetColumnWidth(dgv, "Gwt", 60);
            SetColumnWidth(dgv, "DiaNo", 40);
            SetColumnWidth(dgv, "DiaWt", 60);
            SetColumnWidth(dgv, "DiaRate", 60);
            SetColumnWidth(dgv, "DiaAmt", 70);
            SetColumnWidth(dgv, "Stno", 40);
            SetColumnWidth(dgv, "StWt", 60);
            SetColumnWidth(dgv, "StRate", 60);
            SetColumnWidth(dgv, "StAmt", 80);
            SetColumnWidth(dgv, "Ot_Stno", 55);
            SetColumnWidth(dgv, "Ot_StWt", 60);
            SetColumnWidth(dgv, "Ot_StRate", 60);
            SetColumnWidth(dgv, "Ot_StAmt", 80);
            SetColumnWidth(dgv, "Pr_Stno", 55);
            SetColumnWidth(dgv, "Pr_StWt", 60);
            SetColumnWidth(dgv, "Pr_StRate", 60);
            SetColumnWidth(dgv, "Pr_StAmt", 80);
            SetColumnWidth(dgv, "MC", 70);
            SetColumnWidth(dgv, "VA mode", 80);
            SetColumnWidth(dgv, "PerSalesVA", 80);
            SetColumnWidth(dgv, "VAamount", 80);
            SetColumnWidth(dgv, "NetWt", 60);
            SetColumnWidth(dgv, "PurityName", 40);
            SetColumnWidth(dgv, "PurMCRate", 60);

        }
    }
}
