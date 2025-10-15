using Gramboo.Database;
using System;
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


namespace FA.FORMS
{
    public partial class LedgerSearch : Gramboo.Controls.GrbForm
    {
        public LedgerSearch()
        {
            InitializeComponent();
        }
        //public CashTransactionEntry cashTrans;
        private static LedgerSearch instance;
        public static LedgerSearch Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LedgerSearch();
                }
                else if (instance.IsDisposed)
                {
                    instance = new LedgerSearch();
                }
                return instance;
            }
        }

        public Gramboo.Controls.GrbForm ParentForm { get; set; }
        public Gramboo.Controls.GrbComboBox Parentcontrol { get; set; }
   


        //public override bool InitializeTables()
        //{
        //    Table t = new Table(FA.Common.DbName, "ACC", "LedgerMaster");
        //    t.PrimaryKeys.Add("EntryId");
        //    t.NotUpdatables.Add("Company_id");
        //    t.NotUpdatables.Add("Branch_id");
        //    t.NotUpdatables.Add("Counter_id");
        //    //--t.IdTextBox = txtid;
        //    this.TableName = t;
        //    return true;
        //}
        //private void PopulateGrid()
        //{

        //    dgv.EntryFormName = LedgerSearch.Instance;
        //    dgv.ShowEdit = true;
        //    dgv.ShowDelete = true;
        //    dgv.AutoGenerateColumns = true;
        //    dgv.ShowSerialNo = true;
            
           
        //    dgv.Fill(new Table(FA.Common.DbName, "CRM", "VCustomerMasterList", true), "Company_id=" + txtcompId.Text);
        //}
        public override void RefreshData()
        {
            base.RefreshData();
            Gramboo.General.Setupcombo(Cmb_code, "ACC.VLedgerMaster", "[Ledger Code]", "Acc_LedgerId", "IsActive='True' AND Company_id=" + txtcompId.Text);
            load();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }     
        //private void grbButton1_Click(object sender, EventArgs e)
        //{
        //    loadLedgerdata();
        //}
        public void load()
        {
            if (cmb_type.SelectedIndex == 0)
            {
                dgv.HiddenDataFields = new List<string> { "Acc_LedgerId", "Company_id", "Branch_id", "ParentTable", "ParentID", "GST", "Counter_id", "Acc_GroupId", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Acc_Remarks", "Company_id", "Branch_id", "Counter_id", "IsActive" };// "[Is Active]";
                dgv.Fill(new Table(SAFA.Classes.Common.DbName, "ACC", "VLedgerforCostomer", true));
            }
            else if (cmb_type.SelectedIndex == 1)
            {
                dgv.HiddenDataFields = new List<string> { "Acc_LedgerId", "Company_id", "Branch_id", "ParentTable", "ParentID", "GST", "Counter_id", "Acc_GroupId", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Acc_Remarks", "Company_id", "Branch_id", "Counter_id", "IsActive" };// "[Is Active]";
            
                dgv.Fill(new Table(SAFA.Classes.Common.DbName, "ACC", "VLedgerforSupplier", true));
            }
            else if (cmb_type.SelectedIndex == 2)
            {


                dgv.HiddenDataFields = new List<string> { "Acc_LedgerId", "Company_id", "Branch_id", "ParentTable", "ParentID", "GST", "Counter_id", "Acc_GroupId", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Acc_Remarks", "Company_id", "Branch_id", "Counter_id", "IsActive" };// "[Is Active]";
                  dgv.Fill(new Table(SAFA.Classes.Common.DbName, "ACC", "VLedgerforScheme", true));
            }
            else if (cmb_type.SelectedIndex == 3)
            {
                dgv.HiddenDataFields = new List<string> { "Acc_LedgerId", "Company_id", "Branch_id", "ParentTable", "ParentID", "GST", "Counter_id", "Acc_GroupId", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Acc_Remarks", "Company_id", "Branch_id", "Counter_id", "IsActive" };// "[Is Active]";
                dgv.Fill(new Table(SAFA.Classes.Common.DbName, "ACC", "[LedgerMaster]", true), "Acc_GroupId != 27 and Acc_GroupId != 26");
            }
        }
        public void loadLedgerdata()
        {
            string str = " select  Acc_Ledgerid, [Ledger Code],[Ledger Name],Housename,Address1,Address2,Phone from Acc.vLedgerMaster l where 1=1";// and Created_date>='" + dtpFromDate.Value + "'and Created_date<='" + dtpToDate.Value + "'";
                if (txtLedgerCode.Text != "")
                {
                    str += " AND l. [Ledger Code] like '" + txtLedgerCode.Text + "%'";
                }              
                if (txtledgerName.Text != "")
                {
                    str += " AND (l.[Ledger Name] like '" + txtledgerName.Text + "%')";
                }
                if (txtPhoneNo.Text != "")
                {
                    str += " AND l.Phone like '%" + txtPhoneNo.Text + "%'";
                }
                if (txtAddress1.Text != "")
                {
                    str += " AND l.Address1 like '%" + txtAddress1.Text + "%'";
                }
                if (txtAddress2.Text != "")
                {
                    str += " AND l.Address2 like '%" + txtAddress2.Text+"%'";
                }
                if (txthouseName.Text != "")
                {
                    str += " AND l.Housename like '%" + txthouseName.Text + "%'";
                }
                dgv.EntryFormName = FA.FORMS.LedgerSearch.Instance;
                dgv.IsList = true;
                dgv.AutoGenerateColumns = true;
                dgv.DataFields = new List<string> { "Acc_LedgerId", "[Ledger Code]","[Ledger Name]",   "Acc_GroupId", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date", "Company_id", "Branch_id", "Counter_id", "Housename", "Address1", "Address2", "Phone" };
                dgv.HiddenDataFields = new List<string> { "Acc_LedgerId", "Company_id", "Branch_id", "ParentTable", "ParentID", "GST", "Counter_id", "Acc_GroupId", "Created_by", "Created_date", "Last_modified_by", "Last_modified_date","Acc_Remarks", "Company_id", "Branch_id", "Counter_id" };// "[Is Active]";
                dgv.DataSource = this.DBConn.GetData(new SqlCommand(str)).Tables[0];
                AdjustColumWidth();
        }

        private void LedgerSearch_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //CheckTextNull();
            loadLedgerdata();
        }
        public void CheckTextNull()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    TextBox textBox = c as TextBox;
                    if (textBox.Text == string.Empty)
                    {
                        // Text box is empty.
                        // You COULD store information about this textbox is it's tag.
                        MessageBox.Show("Please Choose Any option" );
                    }
                }
            }
        }
        

        private void AdjustColumWidth()
        {
            dgv.Columns["Ledger Code"].Width = 120; 
            dgv.Columns["Ledger Name"].Width = 200;
           // dgv.Columns["Acc_Remarks"].Width = 135;
            dgv.Columns["Housename"].Width = 110;
            dgv.Columns["Address1"].Width = 110;
            dgv.Columns["Address2"].Width = 110;
            dgv.Columns["Phone"].Width = 110;
          //  dgv.Columns["Phone"].Width = 110; 
        }
        private void dtpFromDate_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtLedgerCode_TextChanged(object sender, EventArgs e)
        {
            if(txtLedgerCode.Text!="")
            loadLedgerdata();
            else
            {
                dgv.ClearSelection();
            }
        }

        private void txtledgerName_TextChanged(object sender, EventArgs e)
        {
            if(txtledgerName.Text!="")
            loadLedgerdata();
            else
            {
                dgv.ClearSelection();
            }
        }

        private void txthouseName_TextChanged(object sender, EventArgs e)
        {
            if (txthouseName.Text != "")
                loadLedgerdata();           
        }

        private void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            if(txtAddress1.Text!="")
            loadLedgerdata();           
        }
        private void txtAddress2_TextChanged(object sender, EventArgs e)
        {
            if (txtAddress2.Text != "")
                loadLedgerdata();
        }
        private void txtPhoneNo_TextChanged(object sender, EventArgs e)
        {
            if(txtPhoneNo.Text!="")
            loadLedgerdata();
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            load();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            DataGridViewRow row = dgv.Rows[rowIndex];

           // Gramboo.General.Setupcombo(Cmb_code, "ACC.LedgerMaster", "Acc_LedgerCode", "Acc_LedgerId", "IsActive='True' AND Company_id=" + txtcompId.Text);
           
            if (Parentcontrol != null)
            {
              
                Parentcontrol.SelectedValue = row.Cells["Acc_LedgerId"].Value.ToString();
                 Parentcontrol.Text = row.Cells["Ledger Name"].Value.ToString();
               
               // ParentForm.Show();
                ParentForm.Focus();
                this.Close();

            }
           
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
