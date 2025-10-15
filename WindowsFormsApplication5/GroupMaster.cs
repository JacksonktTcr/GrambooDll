using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FA
{
    public partial class GroupMaster : Gramboo.Controls.GrbForm
    {
        private static GroupMaster instance;

        public static GroupMaster Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new GroupMaster();
                }
                else if (instance.IsDisposed)
                {
                    instance = new GroupMaster();
                }
                return instance;
            }
        }
        public GroupMaster()
        {
            InitializeComponent();
            this.DefaultPrimaryKeys = new List<string> { };

            DBConn.ConnectionProperties.GenerateSQLConnectionString();
            DBConn.ConnectionProperties.GenerateSQLConnectionString();
            if ( "REGALGOLD" == DBConn.ConnectionProperties.DatabseName)
            {
                DBConn.HasMirrorDB = true;
                DBConn.MirrorConnectionProperties.ServerName ="SERVER";
                DBConn.MirrorConnectionProperties.DatabseName = "REGALJ";
                DBConn.MirrorConnectionProperties.DBUsername = DBConn.ConnectionProperties.DBUsername;
                DBConn.MirrorConnectionProperties.DBPassword = DBConn.ConnectionProperties.DBPassword;

            }
            if (  "REGALJ" == DBConn.ConnectionProperties.DatabseName)
            {
                DBConn.HasMirrorDB2 = true;
                DBConn.MirrorConnectionProperties2.ServerName = "SERVER";
                DBConn.MirrorConnectionProperties2.DatabseName = "REGALJEW";
                DBConn.MirrorConnectionProperties2.DBUsername = DBConn.ConnectionProperties.DBUsername;
                DBConn.MirrorConnectionProperties2.DBPassword = DBConn.ConnectionProperties.DBPassword;

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

        [DefaultValue(true)]
        [Browsable(true)]
        public static bool  ShowInfo { get; set; }

        public override void Init()
        {
            base.Init();
            txtcompId.Text = "1";
            txtBranchID.Text = "103";
            txtCounterId.Text = "11030001";
            txtCrUserId.Text = "11010001";
            txtUserName.Text = "admin";
            TxtGroupName.Focus();
        }
        public override bool InitializeTables()
        {
            Table t = new Table(Kallans.Classes.Common.DbName, "ACC", "GroupMaster");
            t.PrimaryKeys.Add("Acc_GroupId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = TxtGroupID;
            this.TableName = t;
            return true;
        }
        private void PopulateGrid()
        {
            dgv.EntryFormName = this;
            dgv.ShowEdit = true;
            dgv.IsList = true; 
            dgv.AutoGenerateColumns = true;
            dgv.DataFields = new List<string>() { "[Category Name]","Acc_GroupId","[Group Name]","Acc_Under","Under","[Created By]", "[Created Date]", "[Last Modified By]", "[Last Modified Date]", "IsActive" };


            if (ShowInfo)
            {
                dgv.HiddenDataFields = new List<string>() { "Acc_GroupId", "Counter_id", "Branch_id", "Last_modified_by", "Acc_Under", "Created_by" };


            }
            else
            {
                dgv.HiddenDataFields = new List<string>() { "Acc_GroupId", "Counter_id", "Branch_id", "Last_modified_by", "Acc_Under", "Created_by", "Created By", "Created Date", "Last Modified By", "Last Modified Date" };

            }

            dgv.Fill(new Table(Kallans.Classes.Common.DbName, "ACC", "VAccGroupMaster", true));
         }
      
        public override void RefreshData()
        {
            txtcompId.Text = "1";
            txtBranchID.Text = "103";
            txtCounterId.Text = "11030001";
            txtCrUserId.Text = "11010001";
            txtUserName.Text = "admin";

            Gramboo.General.Setupcombo(Cmb_CategoryName, "ACC.CategoryMaster", "Acc_Category_Name", "Acc_Category_ID", "IsActive='True'");
            Gramboo.General.Setupcombo(Cmb_Under, "ACC.GroupMaster", "Acc_GroupName", "Acc_GroupId", "IsActive='True'");

            PopulateGrid();


        }
        public override bool Delete()
        {
            int set;
            bool flag = true;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                DataGridViewRow dr = dgv.Rows[i];
                if (dr.Selected == true)
                {
                    set = Convert.ToInt32(DBConn.GetData(new SqlCommand("Select ACC.GroupMaster_Validation('" + dr.Cells["Acc_GroupId"].Value + "')")).Tables[0].Rows[0][0]);//,'" + txtcompId.Text + "','" + txtBranchID.Text + "'
                    {
                        if (set == 1)
                        {
                            flag = false;
                            TxtGroupName.ShowMessage("GroupName Selected Cannot be Deleted");                             
                            return flag;
                        }
                        else
                        {
                            return base.Delete();
                        }
                    }
                }
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Save();
        }


    }
}
