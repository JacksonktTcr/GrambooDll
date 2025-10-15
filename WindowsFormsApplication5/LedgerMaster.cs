using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JMS.Forms.ACC
{
    public partial class LedgerMaster : Gramboo.Controls.GrbForm
    {
        private static LedgerMaster instance;

        public static LedgerMaster Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new LedgerMaster();
                }
                else if (instance.IsDisposed)
                {
                    instance = new LedgerMaster();
                }
                return instance;
            }
        }
        public LedgerMaster()
        {
            InitializeComponent();
            this.DefaultPrimaryKeys = new List<string> { };
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

        public override void Init()
        {
            base.Init();
            TxtLedgerName.Focus();
            
           
                Gramboo.GRBConfig config;

                if (Gramboo.GRBConfig.Open() != null)
                {
                    config = Gramboo.GRBConfig.Open();
                }
                else
                {
                    config = new Gramboo.GRBConfig();
                }
            config.Login.EnableAuditTrail=true;
            config.Save();
            LogReferenceColumns = new List<string>(){"Acc_LedgerName"};
        }
        public override bool InitializeTables()
        {
            Table t = new Table("JMSRET", "ACC", "LedgerMaster");
            t.PrimaryKeys.Add("Acc_LedgerId");
            t.IdTextBox = TxtLedgerID;
            this.TableName = t;
            return true;
        }
     
        public override void RefreshData()
        {

            Gramboo.General.Setupcombo(cmb_GroupId, "ACC.GroupMaster", "Acc_GroupName", "Acc_GroupId", "IsActive='True'");
            PopulateGrid();
            TxtLedgerName.Focus();

          
        }
        private void PopulateGrid()
        {
            dgv.EntryFormName = this;
            dgv.ShowEdit = true;
            dgv.IsList = true; 
            dgv.AutoGenerateColumns = true;
            dgv.DataFields = new List<string>() { "Acc_LedgerId", "[Ledger Name]", "Acc_GroupId","[Created By]", "[Created Date]","[Last Modified By]", "[Last Modified Date]" };
            dgv.HiddenDataFields = new List<string>() { "Acc_LedgerId", "Acc_GroupId", "Created_by", "Last_modified_by", "Counter_id", "Company_id", "Branch_id" };
            dgv.Fill(new Table("JMSRET", "ACC", "VLedgerMaster", true), "Company_ID=" + txtcompId.Text);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            Delete();
        }

    }
}
