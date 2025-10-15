using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JMS.Forms.PROD
{
    public partial class frmSetting : Gramboo.Controls.GrbForm
    {
        private static frmSetting instance;
        private bool flag;
        public static frmSetting Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new frmSetting();
                }
                else if (instance.IsDisposed)
                {
                    instance = new frmSetting();
                }
                return instance;
            }
        }


        public frmSetting()
        {
            InitializeComponent();
            rbtSubcontractor.CheckedChanged += new EventHandler(rbtOwn_CheckedChanged);
            rbtOwn.CheckedChanged += new EventHandler(rbtOwn_CheckedChanged);
         
            cmbIssueItem.SelectedValueChanged += new EventHandler(CalculateMcAndWst);
            dtpSetRet.ValueChanged += new EventHandler(CalculateMcAndWst);
            txtIssueWt.TextChanged += new EventHandler(CalculateMcAndWst);
            txtSetRetWt.TextChanged += new EventHandler(CalculateMcAndWst);
            txtSetDiaWt.TextChanged += new EventHandler(CalculateMcAndWst);
            txtSetDiaNos.TextChanged += new EventHandler(CalculateMcAndWst);
            txtNos.TextChanged += new EventHandler(CalculateMcAndWst);           
            dgvItem.SummaryCalculated += new Gramboo.Controls.SummaryCalcEventHandler(SplitWeight);
        }
       
        public override void Init()
        {
            base.Init();
           

            TxtIsactive.Text = "1";
            rbtgType.DefaultRadioButton = rbtOwn;
            Cmb_DepartmentName.Enabled = false; 

           dgvItem.DataFields = new List<string>{"TransId","TransDate","ItemId","[Item Name]","Nos","Wt","SetWt","[Size Range]","SizeRangeId",
            "PriceRangeId","[Price Range]","SieveId","Sieve","IsDamaged","[Transfer Mode]","IsReceipt","IsLost,Wst,MCVal"};
            dgvItem.HiddenDataFields = new List<string> { "TransId", "ItemId", "SizeRangeId", "PriceRangeId", "SieveId", "IsReceipt" };
            dgvItem.SummaryRowVisible = true;
            dgvItem.SummaryColumns = new string[] { "Nos", "Wt", "SetWt", "Wst", "MCVal" };
            dgvItem.Fill(new Table("JMS", "PROD", "VSettingMaterials", true),"1=2");

            flag = false;
            txtissueqty.ReadOnly = true;
            dtpSetRet.Checked = false;
            pnlNewJobNo.Visible = false ;
            //this.ListForm = JMS.Forms.PROD.SettingList.Instance;
            rbtOwn.TabStop = true;
            rbtSubcontractor.TabStop = true;
            AdjustcolumWidthItem();
        }
        
        public override bool InitializeTables()
        {
            Table t = new Table("JMS", "PROD", "SettingMaster");
            t.PrimaryKeys.Add("SettingId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = txtsettingId;

            Table t1 = new Table("JMS", "PROD", "SettingMaterials",true);
            t1.PrimaryKeys.Add("TransId");
            t1.FillView = new Table("JMS", "PROD", "VSettingMaterials");
            t1.DatagridView = dgvItem;
            t1.IsDatagridView = true;
            t1.IdTextBox = TxtTranscId;

            t.ChildTables.Add(t1);

            this.TableName = t;
            return true;

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
        private void rbtOwn_CheckedChanged(object sender, EventArgs e)
        {
            if (txtcompId.Text.Trim().Length > 0)
            {
                if (rbtOwn.Checked == true)
                {
                    cmbSetWorker .Enabled = true;
                    Gramboo.General.Setupcombo(Cmb_WorkShopname, "PROD.WorkShopMaster", "WorkshopName", "WorkshopId", "IsActive='True'  AND Company_id=" + txtcompId.Text);
                }
                else
                {
                    cmbSetWorker.Enabled = false;
                    Gramboo.General.Setupcombo(Cmb_WorkShopname, "PROD.VSubContractors", "[Supplier Name]", "SuppId", "IsActive='True' AND Company_id=" + txtcompId.Text);
                }
            }
        }

        public override void RefreshData()
        {
          

            Gramboo.General.Setupcombo(cmb_VoucherTypeId, "ACC.VoucherTypeMaster", "VoucherTypeName", "VoucherTypeId", "IsActive='True'");
            cmb_VoucherTypeId.SelectedValue = 28;
            if (!IsEditMode)
                TxtVoucherNo.Text = JMS.Classes.Common.GetNextVoucherNo((int)cmb_VoucherTypeId.SelectedValue, dtpsetIssue.Value,
            DBConn, Convert.ToInt32(txtcompId.Text), Convert.ToInt32(txtBranchID.Text));

           Gramboo.General.Setupcombo(cmbSetWorker, "PROD.VWorkerMaster", "EmpName", "EmpId", "IsActive='True'AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
             Gramboo.General.Setupcombo(cmbItem, "ITM.VSettingItems", "[Item Name]", "ItemId", "IsActive='True'");
         
             Gramboo.General.Setupcombo(Cmb_SizeRange, "ITM.SizeRangeMaster", "SizeRangeName", "SizeRangeId", "IsActive='True'");
             Gramboo.General.Setupcombo(Cmb_PriceRange, "ITM.PriceRangeMaster", "PriceRangeName", "PriceRangeId", "IsActive='True'");
             Gramboo.General.Setupcombo(Cmb_Sieve, "ITM.SieveMaster", "SieveName", "SieveId", "IsActive='True'");
             flag = true;
             Gramboo.General.Setupcombo(Cmb_Jobno, "PROD.JobNoGeneration", "JobNo", "JobNoId", "IsActive='True' AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
             Gramboo.General.Setupcombo(cmbIssueItem , "ITM.VSemiFinOrnaments", "[Item Name]", "ItemId", "IsActive='True'");
             Gramboo.General.Setupcombo(cmb_purity, "ITM.PurityMaster", "PurityName", "PurityId", "IsActive='True'");

             Gramboo.General.Setupcombo(Cmb_DepartmentName, "SYST.VUserDepartmentAccess", "DeptName", "DeptID", "AllowAccess='True' AND user_id=" + txtCrUserId.Text + " AND  Company_Id=" + txtcompId.Text);      
    
          //   Gramboo.General.Setupcombo(Cmb_DepartmentName, "STK.DepartmentMaster", "DeptName", "DeptID", "IsActive='True' AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
             //Cmb_DepartmentName.SelectedValue = ((frmMain)this.ParentForm).dept;

             txtSetRetWt.Text = (String.IsNullOrEmpty(txtSetRetWt.Text.Trim()) ? "0.00" : txtSetRetWt.Text);    

           
            flag = false;
           
            base.RefreshData();


          
        }
 
      
        private void enableMaterialsSpecs()
        {
            if (cmbItem.SelectedValue != null)
            {


                JMS.Classes.Common.enableMaterialsSpecs(Convert.ToInt32( cmbItem.SelectedValue), Cmb_SizeRange, Cmb_PriceRange, Cmb_Sieve,DBConn );
            }
        }
      

        private void txtSetRetWt_TextChanged(object sender, EventArgs e)
        { 
            SplitWeight(sender,e );
        }

       

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        { 
            enableMaterialsSpecs();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Cmb_SizeRange.AcceptBlankValue = !Cmb_SizeRange.Enabled;
            Cmb_PriceRange.AcceptBlankValue = !Cmb_PriceRange.Enabled;
            Cmb_Sieve.AcceptBlankValue = !Cmb_Sieve.Enabled;

            if (dgvItem.ValidateControls())
            {
                if (Cmb_SizeRange.SelectedValue==null)
                {
                    txtSizeRangeId.Text = "0";
                }
                else
                {
                    txtSizeRangeId.Text = Cmb_SizeRange.SelectedValue.ToString();
                }

                if (Cmb_PriceRange.SelectedValue==null)
                {
                    txtPriceRangeId.Text = "0";
                }
                else
                {
                    txtPriceRangeId.Text = Cmb_PriceRange.SelectedValue.ToString();
                }
                if (Cmb_Sieve.SelectedValue==null)
                {
                    txtSieve.Text = "0";
                }
                else
                {
                    txtSieve.Text = Cmb_Sieve.SelectedValue.ToString();
                }


                if (chkIsReciept.Checked)
                {
                    txtTransferMode.Text = "Receipt";
                }
                else
                {
                    txtTransferMode.Text = "Issue";
                }
                if (txtSetWt.Text == null)
                {
                    txtSetWt.ShowMessage("Enter SetWt");
                    return;
                }


                txtitemid.Text = cmbItem.SelectedValue.ToString();
                txtItmWst.Text = (String.IsNullOrEmpty(txtItmWst.Text.Trim()) ? "0.00" : txtItmWst.Text);
                txtItmMc.Text = (String.IsNullOrEmpty(txtItmMc.Text.Trim()) ? "0.00" : txtItmMc.Text); 
                txtItmMc.Text = (txtsetMCpergm.Text.Trim().Length == 0 ? 0: Convert.ToInt16(txtNos.Text) * Convert.ToSingle(txtsetMCpergm.Text)).ToString();
                txtItmWst.Text = (txtSetWstPerc.Text.Trim().Length == 0 ? 0: Convert.ToInt16(txtNos.Text) * Convert.ToSingle(txtSetWstPerc.Text)/100).ToString();



                if (validateStoneIssueDate())
                {

                    dgvItem.Save();
                }
                //if (dgvItem.ValidateControls())
                //{

                //    validateStoneIssueDate();
                //    dgvItem.Save();

                //}


                //SplitWeight();
            }
            dtpMaterialIssue.Focus();
        }
         

        
        private void SplitWeight(object sender,EventArgs e)
        {
            int diaNos, StoneNos;
            float DiaWt, StoneWt;

            diaNos = StoneNos =0;
            DiaWt = StoneWt = 0f;
            foreach (DataGridViewRow r in dgvItem.Rows)
            {
                if (r.Cells["ItemId"].Value != null)
                {
                    if (DBConn.GetData(new SqlCommand("Select * FROM ITM.VDiamonds WHERE ItemId=" + Convert.ToInt32(r.Cells["ItemId"].Value.ToString()))).Tables[0].Rows.Count != 0)
                    {
                        if (Convert.ToBoolean(r.Cells["IsReceipt"].Value.ToString()) == true)
                        {
                            diaNos -= Convert.ToInt32(r.Cells["Nos"].Value.ToString());
                            DiaWt -= Convert.ToSingle(r.Cells["Wt"].Value.ToString());

                        }
                        else
                        {
                            diaNos += Convert.ToInt32(r.Cells["Nos"].Value.ToString());
                            DiaWt += Convert.ToSingle(r.Cells["Wt"].Value.ToString());

                        }
                    }
                    else //if (Convert.ToInt32(r.Cells["SizeRangeId"].Value.ToString()) != 0)
                    {
                        if (Convert.ToBoolean(r.Cells["IsReceipt"].Value.ToString()) == true)
                        {
                            StoneNos -= Convert.ToInt32(r.Cells["Nos"].Value.ToString());
                            StoneWt -= Convert.ToSingle(r.Cells["Wt"].Value.ToString());

                        }
                        else
                        {
                            StoneNos += Convert.ToInt32(r.Cells["Nos"].Value.ToString());
                            StoneWt += Convert.ToSingle(r.Cells["Wt"].Value.ToString());

                        }
                    }

                }
            }
            dgvItem.SummaryRow.SummaryCells["Nos"].Text = (StoneNos + diaNos).ToString("F3");
            dgvItem.SummaryRow.SummaryCells["Wt"].Text = (StoneWt + DiaWt ).ToString("F3");

            txtSetDiaNos.Text = diaNos.ToString("D");
            txtSetDiaWt.Text = DiaWt.ToString("F3");
            txtSetStoneNos.Text = StoneNos.ToString("D");
            txtSetStoneWt.Text = StoneWt.ToString("F3");


            if (txtSetRetWt.Text.Trim() != "")
            {
                float NetWt;

                NetWt = Convert.ToSingle(txtSetRetWt.Text) - (DiaWt + StoneWt);

                txtSetNetWt.Text = NetWt.ToString("F3");

            }

          
        }



        private void dtpSetRet_ValueChanged(object sender, EventArgs e)
        {
          
           
            if (dtpSetRet.Checked == false)
            {
                //txtSetRetDate.Text = Convert.ToDateTime("01-JAN-2000").ToShortDateString();
                txtSetRetWt.Enabled = false;
                txtSetMc.Enabled = false;
                txtsetMCpergm.Enabled = false;
                txtSetWst.Enabled = false;
                txtSetWstPerc.Enabled = false;

                txtSetNetWt.Text = "0.00";
                txtSetRetWt.Text = "0.00";

                txtSetMc.Text = "0.00";
                txtsetMCpergm.Text = "0.00";
                txtSetWst.Text = "0.00";
                txtSetWstPerc.Text = "0.00";


            }
            else
            {
               
                txtSetRetWt.Enabled = true;
                txtSetMc.Enabled = true;
                txtsetMCpergm.Enabled = true;
                txtSetWst.Enabled = true;
                txtSetWstPerc.Enabled = true;

               
            
                
            }
            
                
            }



        public override bool ValidateControls()
        {



            if (base.ValidateControls())
            
            {
                // float issuewt = 0; float returwt = 0;
                DateTime fromdate = Convert.ToDateTime(dtpsetIssue.Text);
                DateTime todate = Convert.ToDateTime(dtpSetRet.Text);
                DateTime stoneissuedate = Convert.ToDateTime(dtpMaterialIssue.Text);




                foreach (DataGridViewRow r in dgvItem.Rows)
                {

                    if((DateTime) r.Cells["TransDate"].Value  >todate) 


                    {
                        Gramboo.General.ShowMessage("StoneIssueDate  Must be Less Than Or Equal To Return Date");
                        return false;
                    }

                    if((DateTime)r.Cells["TransDate"].Value <fromdate)
                    {
                        Gramboo.General.ShowMessage("StoneIssueDate Must be Greater Than Or Equal To  IssueDate");
                        return false;
                    }
                   


                   
                }
                if (todate < fromdate)
                {
                    dtpSetRet.ShowMessage("ReturnDate  Must be Greater Than Or Equal To IssueDate");
                    // dtpMaterialIssue.ShowMessage("StoneIssueDate Must be Greater Than Or Equal To  IssueDate");
                    return false;


                }

              
                     return true;



                }
                else
                {
                    return false;
                }

                //return true;
            }
        

        public bool validateStoneIssueDate()
        {
            DateTime fromdate = Convert.ToDateTime(dtpsetIssue.Text);
            DateTime todate = Convert.ToDateTime(dtpSetRet.Text);
            DateTime stoneissuedate = Convert.ToDateTime(dtpMaterialIssue.Text);

            if ((stoneissuedate > todate) || (stoneissuedate < fromdate))
            {
                if (stoneissuedate > todate)
                {
                    dtpMaterialIssue.ShowMessage("StoneIssueDate Must be Less Than Or Equal To RetrunDate ");
                }
                else
                {
                    dtpMaterialIssue.ShowMessage("StoneIssueDate Must be Greater Than Or Equal To IssueDate ");

                }
                return false;
            }

            return true;

        }


        public override bool FillData(Dictionary<string, object> PrimaryValues)
        {
            if ( base.FillData(PrimaryValues))
            {

               txtSetRetWt.Text = (String.IsNullOrEmpty(txtSetRetWt.Text.Trim()) ? "0.00" : txtSetRetWt.Text);
                //AdjustColumnWidths();
                AdjustcolumWidthItem();
      
                return true;

            }
            else
            {
                return false;
            }
        }

       

        private void dtpsetIssue_ValueChanged(object sender, EventArgs e)
        {
            if (dtpsetIssue.Checked)
            {
                //txtSettIssueDate.Text = dtpsetIssue.Value.ToShortDateString();
            }
            else
            {
                //txtSettIssueDate.Text = "01-JAN-2000";
                //dtpsetIssue.Checked = false;
            }
        }

        private void Cmb_Jobno_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txtcompId.Text.Length == 0)
            {
                return;
            }

            if (Cmb_Jobno.SelectedValue != null)
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select * FROM PROD.GetJobNo(" + Cmb_Jobno.SelectedValue.ToString() +"," + Convert.ToInt16(txtcompId.Text) + "," + Convert.ToInt16(txtBranchID.Text) + ") ")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        cmbIssueItem.SelectedValue = dt.Rows[0]["PolishRetItemid1"];
                        txtIssueWt.Text = dt.Rows[0]["PolishRetWt1"].ToString();
                        if (dt.Rows[0]["Qty"].ToString() == "")
                        {
                            txtissueqty.ReadOnly = false;
                        }
                        else
                        {
                            txtretqty.Text = dt.Rows[0]["Qty"].ToString();
                            txtissueqty.Text = dt.Rows[0]["Qty"].ToString();
                            cmb_purity .SelectedValue = dt.Rows[0]["PurityId"].ToString();

                        }

                         

                    }
                    else
                    {
                         
                        cmbIssueItem.SelectedIndex = -1;
                        txtIssueWt.Text = "";
                        txtissueqty.ReadOnly = false;
                    }
                    
                }
            }
        }

        private void Cmb_Jobno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Cmb_Jobno_Leave(object sender, EventArgs e)
        {
            if (flag)
                return;
            Int64 seljobnoid;
            if (Cmb_Jobno.SelectedValue != null)
            {
                seljobnoid = Convert.ToInt64(Cmb_Jobno.SelectedValue);
                using (DataTable dt = DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select SettingId FROM PROD.SettingMaster WHERE JobNoID=" + Cmb_Jobno.SelectedValue.ToString())).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {
                        txtsettingId.Text = dt.Rows[0][0].ToString();
                        Dictionary<string, object> d = new Dictionary<string, object>();
                        d.Add("SettingId", txtsettingId.Text);
                        d.Add("Company_id", txtcompId.Text);
                        this.FillData(d);
                    }
                    else
                    {
                        Init();
                    }
                }

                Cmb_Jobno.SelectedValue = seljobnoid;
                txtjobNoId.Text = seljobnoid.ToString();

            }
            else
            {

                Init();
            }
            
        }

        private void grbComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTargetItem.SelectedValue != null)
            {
                using (DataTable dt = DBConn.GetData(new SqlCommand("Select PurityId  FROM ITM.VItemMaster  WHERE ItemId=" + cmbTargetItem.SelectedValue.ToString() + "")).Tables[0])
                {
                    if (dt.Rows.Count > 0)
                    {

                        cmb_purity.SelectedValue = dt.Rows[0]["PurityId"].ToString();


                    }

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlNewJobNo.Visible = false;
        }

        private void btn_CreateJobNo_Click(object sender, EventArgs e)
        {
            if (cmbTargetItem.SelectedValue != null && cmb_purity.SelectedValue !=null )
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "PROD.GenerateJobNoFromSetting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TargetItemId", cmbTargetItem.SelectedValue);
                cmd.Parameters.AddWithValue("@PurityId", cmb_purity .SelectedValue);
                cmd.Parameters.AddWithValue("@company_ID", txtcompId.Text);
                cmd.Parameters.AddWithValue("@branch_id", txtBranchID.Text);
                cmd.Parameters.AddWithValue("@CounterId", txtCounterId.Text);

                SqlParameter c = new SqlParameter("@JobNoId", SqlDbType.BigInt );
                c.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(c);
                //cmd.Parameters.AddWithValue("@CounterId", txtCounterId.Text);

                //Gramboo.DataController.CommandCollection cmdcol = new Gramboo.DataController.CommandCollection();
                //cmdcol.Add(cmd);
                DBConn.GetData(cmd, "GenerateJobNo");
                Gramboo.General.Setupcombo(Cmb_Jobno, "PROD.JobNoGeneration", "JobNo", "JobNoId", "IsActive='True'");
                Cmb_Jobno.Text = "";
                Cmb_Jobno.SelectedValue = Convert.ToInt64(cmd.Parameters["@JobNoId"].Value);
                txtjobNoId.Text = Convert.ToInt64(cmd.Parameters["@JobNoId"].Value).ToString();
                pnlNewJobNo.Visible = false;
                txtissueqty.ReadOnly = false; 
            }
            else
            {
                cmbTargetItem.ShowMessage("Select Target Item");
            }
        }

        private void btnNewJobNo_Click(object sender, EventArgs e)
        {
            Gramboo.General.Setupcombo(cmbTargetItem, "ITM.VOrnaments", "[Item Name]", "ItemId", "IsActive='True'");
            pnlNewJobNo.Size = new System.Drawing.Size(370, 110);

            pnlNewJobNo.Location = new Point(btn_CreateJobNo.Location.X + btn_CreateJobNo.Size.Width+30, btn_CreateJobNo.Location.Y-40);
            pnlNewJobNo.Visible = true;
            pnlNewJobNo.BringToFront();

        }

        private void frmSetting_Load(object sender, EventArgs e)
        {

        }


        private void CalculateMcAndWst(object sender, EventArgs e)
        {
            if (txtcompId.Text.Trim().Length == 0 || cmbSetWorker.SelectedValue == null)

                return;


            int stno = 0;
            float stwt=0;
            stwt = Convert.ToSingle((txtSetDiaWt.Text.Trim().Length == 0 ? "0" : txtSetDiaWt.Text)) + Convert.ToSingle((txtSetStoneWt.Text.Trim().Length == 0 ? "0" : txtSetStoneWt.Text));
            stno = Convert.ToInt32((txtSetDiaNos.Text.Trim().Length == 0 ? "0" : txtSetDiaNos.Text)) + Convert.ToInt32 ((txtSetStoneNos.Text.Trim().Length == 0 ? "0" : txtSetStoneNos.Text));

            if (cmbIssueItem.SelectedValue != null)
            {
                JMS.Classes.Common.GetMCandWastage(DBConn, txtsetMCpergm, txtSetMc, txtSetWstPerc, txtSetWst,
                    dtpsetIssue.Value, Convert.ToInt32(cmbIssueItem.SelectedValue), "S", 0, Convert.ToInt32(cmbSetWorker.SelectedValue),
                    Convert.ToInt16(txtcompId.Text), Convert.ToInt16(txtBranchID.Text),
                    Convert.ToSingle((txtIssueWt.Text.Trim().Length == 0 ? "0" : txtIssueWt.Text)), Convert.ToSingle((txtSetRetWt.Text.Trim().Length == 0 ? "0" : txtSetRetWt.Text)),
                    Convert.ToInt16((txtNos.Text.Trim().Length == 0 ? "0" : txtNos.Text)),stwt,stno );



            }
        }

        private void cmbSetWorker_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateMcAndWst(sender, e);
        }

        private void txtissueqty_TextChanged(object sender, EventArgs e)
        {
            txtretqty.Text = txtissueqty.Text;
        }

        private void cmbIssueItem_SelectedValueChanged(object sender, EventArgs e)
        {

            //if (cmbIssueItem.SelectedValue!=null)
            //{
            //    using (DataTable dt = DBConn.GetData(new SqlCommand("Select PurityId  FROM ITM.VItemMaster  WHERE ItemId=" + cmbIssueItem.SelectedValue.ToString() + "")).Tables[0])
            //    {
            //        if (dt.Rows.Count > 0)
            //        {

            //            cmb_purity.SelectedValue = dt.Rows[0]["PurityId"].ToString();


            //        }

            //    }
            }

        private void pnlNewJobNo_Paint(object sender, PaintEventArgs e)
        {

        }
        private void AdjustColumnWidths()
        {

            //dgvItem.RowHeadersVisible = false;
            //dgvItem.Columns[0].Width = 30;
            //dgvItem.Columns["Item Name"].Width = cmbItem.Width + 1;
            //dgvItem.Columns["Nos"].Width = txtNos.Width + 1;
            //dgvItem.Columns["Wt"].Width = txtWeight.Width + 1;
            //dgvItem.Columns["SetWt"].Width = txtSetWt.Width + 1;
            //dgvItem.Columns["Size Range"].Width = Cmb_SizeRange.Width + 1;
            //dgvItem.Columns["Price Range"].Width = Cmb_PriceRange.Width + 1;
            //dgvItem.Columns["Sieve"].Width = Cmb_Sieve.Width - 1;
            //dgvItem.Columns["IsDamaged"].Width = chk_Damaged.Width + 3;
            //dgvItem.Columns["IsReceipt"].Width = chkIsReciept.Width + 3;
            //dgvItem.Columns["IsLost"].Width = chk_IsLost.Width + 3;


        }

        private void AdjustcolumWidthItem()
        {
            dgvItem.RowHeadersVisible = false;
            dgvItem.Columns[0].Width = 35;
            dgvItem.Columns["TransDate"].Width = dtpMaterialIssue.Width + 3;
            dgvItem.Columns["Item Name"].Width =cmbItem.Width + 1;
            dgvItem.Columns["Nos"].Width = txtNos.Width + 1;
            dgvItem.Columns["Wt"].Width = txtWeight.Width + 1;
            dgvItem.Columns["SetWt"].Width = txtSetWt.Width + 1;
            dgvItem.Columns["Size Range"].Width = Cmb_SizeRange.Width + 3;
            dgvItem.Columns["Price Range"].Width =Cmb_PriceRange.Width +3;
            dgvItem.Columns["Sieve"].Width =Cmb_Sieve.Width + 3;
            dgvItem.Columns["IsReceipt"].Width = chkIsReciept.Width + 0;
            dgvItem.Columns["IsLost"].Width = chk_IsLost.Width + 0;
            dgvItem.Columns["IsDamaged"].Width = chk_Damaged.Width + 0;

 
        }


       
        private void Cmb_WorkShopname_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((Cmb_WorkShopname.ValueMember != null) && (Cmb_WorkShopname.SelectedValue != null))
            {
                Gramboo.General.Setupcombo(cmbSetWorker, "PROD.VWorkerMaster", "EmpName", "EmpId", "IsActive=1 AND WorkShopID=" + Cmb_WorkShopname.SelectedValue + "");
                if (cmbSetWorker.SelectedValue == null)
                {
                    cmbSetWorker.Text = "";



                }
            }
        }

        private void rbtOwn_CheckedChanged_1(object sender, EventArgs e)
        {
        //    if (txtcompId.Text.Trim().Length > 0)
        //    {
        //        if (rbtOwn.Checked == true)
        //        {
        //            Cmb_WorkShopname.Text = "";
        //            cmbSetWorker.Text = "";
        //            cmbSetWorker.AcceptBlankValue = false;
        //            cmbSetWorker.Enabled = true;
        //            Gramboo.General.Setupcombo(Cmb_WorkShopname, "PROD.WorkShopMaster", "WorkshopName", "WorkshopId", "IsActive='True'  AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
        //            //txtBalanceCash.Text = JMS.Classes.Common.GetWorkerCashBalance(Convert.ToInt32(Cmb_WorkerName.SelectedValue), DBConn, Convert.ToInt32(base.txtcompId.Text));
        //            //txtBalanceWt.Text = JMS.Classes.Common.GetWorkerWeightBalance(Convert.ToInt32(Cmb_WorkerName.SelectedValue), DBConn, Convert.ToInt32(base.txtcompId.Text));
        //        }
        //        else
        //        {
        //            Cmb_WorkShopname.Text = "";
        //            cmbSetWorker.Text = "";
        //            cmbSetWorker.Enabled = false;
        //            cmbSetWorker.AcceptBlankValue = true;
        //            Gramboo.General.Setupcombo(Cmb_WorkShopname, "PROD.VSubContractors", "[Supplier Name]", "SuppId", "IsActive='True' AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
        //            //txtBalanceCash.Text = JMS.Classes.Common.GetWorkShopCashBalance(Convert.ToInt32(Cmb_WorkShopname.SelectedValue), Convert.ToChar("S"), DBConn, Convert.ToInt32(base.txtcompId.Text));
        //            //txtBalanceWt.Text = JMS.Classes.Common.GetWorkShopWeightBalance(Convert.ToInt32(Cmb_WorkShopname.SelectedValue), Convert.ToChar("S"), DBConn, Convert.ToInt32(base.txtcompId.Text));
        //        }
        //    }

            if (txtcompId.Text.Trim().Length > 0)
            {


                if (rbtOwn.Checked == true)
                {
                    cmbSetWorker.Enabled = true;
                    cmbSetWorker.AcceptBlankValue = false;
                    Cmb_WorkShopname.Text = "";
                    cmbSetWorker.Text = "";
                    Gramboo.General.Setupcombo(Cmb_WorkShopname, "PROD.WorkShopMaster", "WorkshopName", "WorkshopId", "IsActive='True'  AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
                    //txtBalanceCash.Text = JMS.Classes.Common.GetWorkerCashBalance(Convert.ToInt32(Cmb_WorkerName.SelectedValue), DBConn, Convert.ToInt32(base.txtcompId.Text));
                    //txtBalanceWt.Text = JMS.Classes.Common.GetWorkerWeightBalance(Convert.ToInt32(Cmb_WorkerName.SelectedValue), DBConn, Convert.ToInt32(base.txtcompId.Text));
                    if (IsEditMode)

                        cmbSetWorker.Enabled = true;
                    cmbSetWorker.AcceptBlankValue = false;
                    Gramboo.General.Setupcombo(Cmb_WorkShopname, "PROD.WorkShopMaster", "WorkshopName", "WorkshopId", "IsActive='True'  AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
                    
                }
                else
                {

                    cmbSetWorker.Text = "";



                    cmbSetWorker.Enabled = false;
                    cmbSetWorker.AcceptBlankValue = true;
                    Gramboo.General.Setupcombo(Cmb_WorkShopname, "PROD.VSubContractors", "[Supplier Name]", "SuppId", "IsActive='True' AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
                    //            //txtBalanceCash.Text = JMS.Classes.Common.GetWorkShopCashBalance(Convert.ToInt32(Cmb_WorkShopname.SelectedValue), Convert.ToChar("S"), DBConn, Convert.ToInt32(base.txtcompId.Text));
                    //            //txtBalanceWt.Text = JMS.Classes.Common.GetWorkShopWeightBalance(Convert.ToInt32(Cmb_WorkShopname.SelectedValue), Convert.ToChar("S"), DBConn, Convert.ToInt32(base.txtcompId.Text));
                    //  
                    if (IsEditMode)
                    {
                        cmbSetWorker.Text = "";
                       // Cmb_WorkShopname.Text = "";
                        cmbSetWorker.Enabled = false;
                        cmbSetWorker.AcceptBlankValue = true;
                        Gramboo.General.Setupcombo(Cmb_WorkShopname, "PROD.VSubContractors", "[Supplier Name]", "SuppId", "IsActive='True' AND Company_id=" + txtcompId.Text + "AND Branch_id=" + txtBranchID.Text);
                    }

                }
            }
        }






            //}

        //private void cmbIssueItem_SelectedIndexChanged(object sender, EventArgs e)
        //{
        
        //}
        
        

    }
}
