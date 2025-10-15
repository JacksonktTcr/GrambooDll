using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using Gramboo.Controls;

namespace SAFA.Forms.COM
{
    public partial class CustomerSearch : Gramboo.Controls.GrbForm
    {
        private bool mouseDown, Event = false;
        private Point lastLocation;

        public delegate void CellClickedEventHandler(object sender, CellClickedEventArgs e);
        public event CellClickedEventHandler CellButtonClick;

        public class CellClickedEventArgs : System.EventArgs
        {
            public CellClickedEventArgs()
            {

            }
            public CellClickedEventArgs(string TragetId)
            {
                TargetID = TragetId;
            }
            public string TargetID { get; set; }
        }


        string Target = string.Empty;

        string Id = string.Empty;
        string cardno = string.Empty;
        string code = string.Empty;
        string houseName = string.Empty;
        string addr1 = string.Empty;
        string addr2 = string.Empty;
        string mob = string.Empty;
        string panNo = string.Empty;
        string stateName = string.Empty;
        string stateId = string.Empty;
        string SalesMode = string.Empty;
        string CrAmount = string.Empty;
        string PC_Points = string.Empty;

        public string target
        {
            get { return Target; }
            set { Target = value; }
        }
        public string TragetId
        {
            get { return Id; }
            set { Id = value; }
        }
        public string CardNo
        {
            get { return cardno; }
            set { cardno = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string HouseName
        {
            get { return houseName; }
            set { houseName = value; }
        }
        public string Address1
        {
            get { return addr1; }
            set { addr1 = value; }
        }
        public string Address2
        {
            get { return addr2; }
            set { addr2 = value; }
        }
        public string Mob
        {
            get { return mob ; }
            set { mob = value; }
        }               
        public string PanNo
        {
            get { return panNo; }
            set { panNo = value; }
        }
        public string StateName
        {
            get { return stateName; }
            set { stateName = value; }
        }
        public string StateId
        {
            get { return StateId; }
            set { StateId = value; }
        }
        public string Mode
        {
            get { return SalesMode; }
            set { SalesMode = value; }
        }
        public string CreditAmount
        {
            get { return CrAmount; }
            set { CrAmount = value; }
        }
        public string PrivilageCardPoints
        {
            get { return PC_Points; }
            set { PC_Points = value; }
        }

        public virtual void OnClicked(CellClickedEventArgs e)
        {
            if (CellButtonClick != null)
            {
                CellButtonClick(this, e);
            }
        }

        public CustomerSearch()
        {
            InitializeComponent();
        }

        public void loadcustomerdata()
        {
            if (Event == true)
                return;
            dgview.DataSource = null;
            string str = "SELECT t1.CustId,t1.CardNo,t1.Code,t1.CustName, t1.HouseName, t1.CustAddr1,t1.CustAddr2,t1.CustMob,t1.CustPhone,t1.CustPanNo,t1.CustTypeName" +
 " FROM  CRM.vCustomerMaster t1 " +
 "where isactive='true'  ";
            if (TxtCode.Text != "Code")
            {
                if (TxtCode.Text != "")
                {
                    str += " AND Code like '" + TxtCode.Text + "%'";
                }
            }
            if (txt_cardno.Text != "Card No")
            {
                if (txt_cardno.Text != "")
                {
                    str += " AND CardNo like '" + txt_cardno.Text + "%'";
                }
            }
            if (TxtName.Text != "Name")
            {
                if (TxtName.Text != "")
                {
                    str += " AND CustName like '" + TxtName.Text + "%'";
                }
            }
            if (TxtHouseName.Text != "House Name")
            {
                if (TxtHouseName.Text != "")
                {
                    str += " AND HouseName like '" + TxtHouseName.Text + "%'";
                }
            }
            if (TxtAdd1.Text != "Address1")
            {
                if (TxtAdd1.Text != "")
                {
                    str += " AND CustAddr1 like '" + TxtAdd1.Text + "%'";
                }
            }
            if (TxtAdd2.Text != "Address2")
            {
                if (TxtAdd2.Text != "")
                {
                    str += " AND  CustAddr2 like '" + TxtAdd2.Text + "%'";
                }
            }
            if (TxtPhone.Text != "Phone Number")
            {
                if (TxtPhone.Text != "")
                {
                    str += " AND  (CustPhone like '" + TxtPhone.Text + "%' or CustMob like '" + TxtPhone.Text + "%'  )";
                }
            }
            if (Mode != "A")
            {
                str += " AND  CustTypeName like '" + Mode + "%'";
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SALE.CustomerSearchSales";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@SearchQuery", str);
            cmd.Parameters.AddWithValue("@CompanyId", (txtcompId.Text != "" ? txtcompId.Text : "0"));
            cmd.Parameters.AddWithValue("@BranchId", (txtBranchID.Text != "" ? txtBranchID.Text : "0"));
            dgview.DataFields = new List<string> { "CustId", "CardNo", "Code", "[Customer Name]", "HouseName", "Address1", "Address2", "MobileNo", "PhoneNo", "CardBalance", "CustPanNo" };
            dgview.HiddenDataFields = new List<string> { "CustId" };
            dgview.DataSource = DBConn.GetData(cmd).Tables[0];
            DataTable dt5 = DBConn.GetData(cmd).Tables[0];

            dgview.Columns["Customer Name"].Width = 350;
            dgview.Columns["Address1"].Width = 280;
            dgview.Columns["HouseName"].Width = 100;
            dgview.Columns["Address2"].Width = 100;
            dgview.Columns["CardNo"].Width = 50;
            dgview.Columns["Code"].Width = 80;
            if (dt5.Rows.Count == 0)
            {
                if (txt_cardno.Text == "Card No" & TxtCode.Text == "Code" & TxtName.Text == "Name" & TxtHouseName.Text == "House Name" & TxtAdd1.Text == "Address1" & TxtAdd2.Text == "Address2")
                {
                    if (TxtPhone.Text != "Phone Number")
                    {
                        if (TxtPhone.Text.ToString().Trim().Length > 5)
                        {
                            DialogResult d = Gramboo.General.ShowMessage(
                         " This Phone number does not appear to be \n" + " Associated with a Customer !!\n\n" +
                         " 1. Press 'Yes' to Create new Customer \n" +
                         " 2. Press 'No' to Cancel This Action \n", "Phone number doesn't exist", MessageBoxIcon.Question, MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button1);

                            if (d == DialogResult.Yes)
                            {
                                CRM.frmCustomerMasterr frm = CRM.frmCustomerMasterr.Instance;
                                frm.MdiParent = this.ParentForm;  
                                frm.SenderControl = TxtCustId;
                                frm.txtMobile.Text = TxtPhone.Text;
                                frm.Parentcontrol = Cmb;
                                frm.SenderForm = this;
                                frm.EntryForm = this;
                                frm.MdiParent = this.ParentForm;
                                frm.Focus();
                            }
                        }
                    }
                }
            }
            else if (dt5.Rows.Count > 0)
            {
                //dgview.Rows[0].Cells[0];
            }
        }

        public void LoadSchemeCustomerData()
        {
            if (Event == true)
                return;
            dgview.DataSource = null;
            string str = " SELECT t1.JoinId,t1.CustName,t1.Address1,t1.Address2,t1.Address3,t1.MobileNo,t1.PhoneNo,t1.JoinNo,'' as CreditAmount " +
 " FROM  SALE.SavingSchemeJoiningEntry t1 " +
 "where IsActive='true'  ";
            if (TxtCode.Text != "Code")
            {
                if (TxtCode.Text != "")
                {
                    str += " AND JoinNo like '" + TxtCode.Text + "%'";
                }
            }
            if (txt_cardno.Text != "Card No")
            {
                if (txt_cardno.Text != "")
                {
                    str += " AND CardNo like '" + txt_cardno.Text + "%'";
                }
            }
            if (TxtName.Text != "Name")
            {
                if (TxtName.Text != "")
                {
                    str += " AND CustName like '" + TxtName.Text + "%'";
                }
            }
            if (TxtHouseName.Text != "House Name")
            {
                if (TxtHouseName.Text != "")
                {
                    str += " AND HouseName like '" + TxtHouseName.Text + "%'";
                }
            }
            if (TxtAdd1.Text != "Address1")
            {
                if (TxtAdd1.Text != "")
                {
                    str += " AND CustAddr1 like '" + TxtAdd1.Text + "%'";
                }
            }
            if (TxtAdd2.Text != "Address2")
            {
                if (TxtAdd2.Text != "")
                {
                    str += " AND  CustAddr2 like '" + TxtAdd2.Text + "%'";
                }
            }
            if (TxtPhone.Text != "Phone Number")
            {
                if (TxtPhone.Text != "")
                {
                    str += " AND  (PhoneNo like '" + TxtPhone.Text + "%' or mobileNo like '" + TxtPhone.Text + "%'  )";
                }
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SALE.SchemeCustomerSearch";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Date", DateTime.Now.ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@SearchQuery", str);
            cmd.Parameters.AddWithValue("@CompanyId", (txtcompId.Text != "" ? txtcompId.Text : "0"));
            cmd.Parameters.AddWithValue("@BranchId", (txtBranchID.Text != "" ? txtBranchID.Text : "0"));
            dgview.DataFields = new List<string> { "JoinId", "JoinNo", "[Customer Name]", "Address1", "Address2", "Address3", "mobileNo", "PhoneNo" };
            dgview.HiddenDataFields = new List<string> { "JoinId" };
            dgview.DataSource = DBConn.GetData(cmd).Tables[0];
            DataTable dt5 = DBConn.GetData(cmd).Tables[0];

            dgview.Columns["Customer Name"].Width = 180;
            dgview.Columns["Address2"].Width = 280;
            dgview.Columns["Address1"].Width = 100;
            dgview.Columns["Address3"].Width = 100;

            if (dt5.Rows.Count == 0)
            {

            }
            else if (dt5.Rows.Count > 0)
            {
                //dgview.Rows[0].Cells[0];
            }
        }

        private void dgview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (e.RowIndex == -1)
                return;
            DataGridViewRow row = dgview.Rows[rowIndex];
            if (target == "CRM")
            {
                TragetId = row.Cells["CustId"].Value.ToString();

                CardNo = row.Cells["CardNo"].Value.ToString();
                Code = row.Cells["Code"].Value.ToString();
                HouseName = row.Cells["HouseName"].Value.ToString();
                Address1 = row.Cells["Address1"].Value.ToString();
                Address2 = row.Cells["Address2"].Value.ToString();
                Mob = row.Cells["MobileNo"].Value.ToString();
                PanNo = row.Cells["CustPanNo"].Value.ToString();
                CreditAmount = row.Cells["CreditAmount"].Value.ToString();
                //StateId = row.Cells["StateId"].Value.ToString();
                PrivilageCardPoints = row.Cells["CardBalance"].Value.ToString();
            }
            else if (target == "SCH")
            {
                TragetId = row.Cells["JoinId"].Value.ToString();

                //CardNo = row.Cells["CardNo"].Value.ToString();
                Code = row.Cells["JoinNo"].Value.ToString();
                HouseName = row.Cells["Address1"].Value.ToString();
                Address1 = row.Cells["Address2"].Value.ToString();
                Address2 = row.Cells["Address3"].Value.ToString();
                Mob = row.Cells["MobileNo"].Value.ToString();
                //PanNo = row.Cells["CustPanNo"].Value.ToString();
                //CreditAmount = row.Cells["CreditAmount"].Value.ToString();
                //StateId = row.Cells["StateId"].Value.ToString();        
            }
            OnClicked(new CellClickedEventArgs(this.TragetId));
            this.Dispose();
            this.Close();
        }
        
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (target == "CRM")
            {
                loadcustomerdata();
            }
            else if (target == "SCH")
            {
                LoadSchemeCustomerData();
            }
        }

        private void PanBack_Paint(object sender, PaintEventArgs e)
        {
            System.Windows.Forms.Panel panel = sender as System.Windows.Forms.Panel;
            System.Drawing.Rectangle rect = panel.ClientRectangle;
            rect.Width--;
            rect.Height--;
            e.Graphics.DrawRectangle(Pens.WhiteSmoke, rect);
        }

        private void LabCls_MouseMove(object sender, MouseEventArgs e)
        {
            LabCls.ForeColor = Color.Red;
        }
        private void LabCls_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
        private void LabCls_MouseLeave(object sender, EventArgs e)
        {
            LabCls.ForeColor = Color.RoyalBlue;
        }
        private void CustomerSearch_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        private void CustomerSearch_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void CustomerSearch_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;          
        }
        private void CustomerSearch_Load(object sender, EventArgs e)
        {
            if(target=="CRM")
            {
                btnTarget.Text = "CUSTOMER";
            }
            else if(target=="SH")
            {
                btnTarget.Text = "SHAREHOLDER";
            }
            else if (target == "SCH")
            {
                btnTarget.Text = "SCHEME";
            }
            else
            {
                btnTarget.Text = "";
            }          
        }
        public override void Init()
        {
            base.Init();
            TxtCustId.TextChanged -=TxtCustId_TextChanged;
            CardNoTxt();
            CardNoTxtleave();
            CodeTxt();
            CodeTxtleave();
            NameTxt();
            NameTxtleave();
            HouseNameTxt();
            HouseNameTxtleave();
            Add1Txt();
            Add1Txtleave();
            Add2Txt();
            Add2Txtleave();
            PhoneTxt();
            PhoneTxtleave();
            TxtCode.Focus();
            TxtCustId.TextChanged += TxtCustId_TextChanged;
        }
        void CodeTxt()
        {
            if (TxtCode.Text == "Code")
            {
                TxtCode.Text = "";
                TxtCode.ForeColor = Color.Black;
            }
        }
        void CodeTxtleave()
        {
            if (TxtCode.Text == "")
            {
                TxtCode.Text = "Code";
                TxtCode.ForeColor = Color.Silver;
            }
        }
        void CardNoTxt()
        {
            if (txt_cardno.Text == "Card No")
            {
                txt_cardno.Text = "";
                txt_cardno.ForeColor = Color.Black;
            }
        }
        void CardNoTxtleave()
        {
            if (txt_cardno.Text == "")
            {
                txt_cardno.Text = "Card No";
                txt_cardno.ForeColor = Color.Silver;
            }
        }
        void NameTxt()
        {
            if (TxtName.Text == "Name")
            {
                TxtName.Text = "";
                TxtName.ForeColor = Color.Black;
            }
        }
        void NameTxtleave()
        {
            if (TxtName.Text == "")
            {
                TxtName.Text = "Name";
                TxtName.ForeColor = Color.Silver;
            }
        }
        void HouseNameTxt()
        {
            if (TxtHouseName.Text == "House Name")
            {
                TxtHouseName.Text = "";
                TxtHouseName.ForeColor = Color.Black;
            }
        }
        void HouseNameTxtleave()
        {
            if (TxtHouseName.Text == "")
            {
                TxtHouseName.Text = "House Name";
                TxtHouseName.ForeColor = Color.Silver;
            }
        }
        void Add1Txt()
        {
            if (TxtAdd1.Text == "Address1")
            {
                TxtAdd1.Text = "";
                TxtAdd1.ForeColor = Color.Black;
            }
        }
        void Add1Txtleave()
        {
            if (TxtAdd1.Text == "")
            {
                TxtAdd1.Text = "Address1";
                TxtAdd1.ForeColor = Color.Silver;
            }
        }
        void Add2Txt()
        {
            if (TxtAdd2.Text == "Address2")
            {
                TxtAdd2.Text = "";
                TxtAdd2.ForeColor = Color.Black;
            }
        }
        void Add2Txtleave()
        {
            if (TxtAdd2.Text == "")
            {
                TxtAdd2.Text = "Address2";
                TxtAdd2.ForeColor = Color.Silver;
            }
        }
        void PhoneTxt()
        {
            if (TxtPhone.Text == "Phone Number")
            {
                TxtPhone.Text = "";
                TxtPhone.ForeColor = Color.Black;
            }
        }
        void PhoneTxtleave()
        {
            if (TxtPhone.Text == "")
            {
                TxtPhone.Text = "Phone Number";
                TxtPhone.ForeColor = Color.Silver;
            }
        }

        private void txt_cardno_Enter(object sender, EventArgs e)
        {
            CardNoTxt();
        }
        private void txt_cardno_Leave(object sender, EventArgs e)
        {
            CardNoTxtleave();
        }

        private void TxtCode_Enter(object sender, EventArgs e)
        {
            CodeTxt();
        }

        private void TxtCode_Leave(object sender, EventArgs e)
        {
            CodeTxtleave();
        }

        private void TxtName_Enter(object sender, EventArgs e)
        {
            NameTxt();
        }

        private void TxtName_Leave(object sender, EventArgs e)
        {
            NameTxtleave();
        }

        private void TxtHouseName_Enter(object sender, EventArgs e)
        {
            HouseNameTxt();
        }

        private void TxtHouseName_Leave(object sender, EventArgs e)
        {
            HouseNameTxtleave();
        }

        private void TxtAdd1_Enter(object sender, EventArgs e)
        {
            Add1Txt();
        }

        private void TxtAdd1_Leave(object sender, EventArgs e)
        {
            Add1Txtleave();
        }

        private void TxtAdd2_Enter(object sender, EventArgs e)
        {
            Add2Txt();
        }

        private void TxtAdd2_Leave(object sender, EventArgs e)
        {
            Add2Txtleave();
        }

        private void TxtPhone_Enter(object sender, EventArgs e)
        {
            PhoneTxt();
        }

        private void TxtPhone_Leave(object sender, EventArgs e)
        {
            PhoneTxtleave();
        }

        private void CustomerSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
                this.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (target == "CRM")
                {
                    loadcustomerdata();
                }
                else if (target == "SCH")
                {
                    LoadSchemeCustomerData();
                }

            }
        }
        private void dgview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //int rowIndex = dgview.CurrentRow.Index;
                //DataGridViewRow row = dgview.Rows[rowIndex];
                //TragetId = row.Cells["CustId"].Value.ToString();

                //CardNo = row.Cells["CardNo"].Value.ToString();
                //Code = row.Cells["Code"].Value.ToString();
                //HouseName = row.Cells["HouseName"].Value.ToString();
                //Address1 = row.Cells["Address1"].Value.ToString();
                //Address2 = row.Cells["Address2"].Value.ToString();
                //Mob = row.Cells["MobileNo"].Value.ToString();
                //PanNo = row.Cells["CustPanNo"].Value.ToString();
                //CreditAmount = row.Cells["CreditAmount"].Value.ToString();
                ////StateId = row.Cells["StateId"].Value.ToString();        

                //OnClicked(new CellClickedEventArgs(this.TragetId));
                //this.Dispose();
                //this.Close();
            }
        }

        private void TxtCustId_TextChanged(object sender, EventArgs e)
        {
            if (Event == true)
                return;
            if (target == "CRM")
            {
                loadcustomerdata();
            }
            else if (target == "SCH")
            {
                LoadSchemeCustomerData();
            }
            if (dgview.Rows.Count < 0)
                return;
            dgview_CellDoubleClick(dgview, new DataGridViewCellEventArgs(0, 0));
            Event = true;
        }
    }
}
