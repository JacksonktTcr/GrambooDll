using Gramboo.Database;
using SAFA.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SAFA.Forms.GENERAL
{
    public partial class MetalRate : Gramboo.Controls.GrbForm
    {
        private static MetalRate instance;

        public static MetalRate Instance
        {

            get
            {
                if (instance == null)
                {
                    instance = new MetalRate();
                }
                else if (instance.IsDisposed)
                {
                    instance = new MetalRate();
                }
                return instance;
            }
        }
        public MetalRate()
        {
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
        public override bool InitializeTables()
        {
            Table t = new Table(SAFA.Classes.Common.DbName, "GEN", "MetalRate");
            t.PrimaryKeys.Add("MetalRateId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            t.IdTextBox = txtMetalRateId;
            this.TableName = t;
            return true;
        }

        public override void RefreshData()
        {
            txtEntryTime.Text = System.DateTime.Now.ToString();
            PopulateGrid();


        }
        public override void Init()
        {
            base.Init();
            AdjustColumnsWidth();
            txtSilverRate.Focus();
            if (DBConn.GetData(new SqlCommand("SELECT Top 1 MetalRate from GEN.MetalRate Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows.Count > 0)
            {
                txtmetalrate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 MetalRate from GEN.MetalRate Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["MetalRate"]).ToString();
                txtBarRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 BarRate from GEN.MetalRate Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BarRate"]).ToString();
                txtBoardRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 BoardRate from GEN.MetalRate Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["BoardRate"]).ToString();
                txtOldGoldRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 OldGoldRate from GEN.MetalRate Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["OldGoldRate"]).ToString();
                txtSilverRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 SilverRate from GEN.MetalRate Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["SilverRate"]).ToString();
                txtOldSilverRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 OldSilverRate from GEN.MetalRate Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["OldSilverRate"]).ToString();
                txtPlatinumRate.Text = (DBConn.GetData(new SqlCommand("SELECT Top 1 PlatinumRate from GEN.MetalRate Order BY EntryDate Desc,EntryTime DESC ")).Tables[0].Rows[0]["PlatinumRate"]).ToString();
            }
        }
        public void AdjustColumnsWidth()
        {

            dgv.Columns["Entry Date"].Width = 120;
            dgv.Columns["Bar Rate"].Width = 170;
            dgv.Columns["Board Rate"].Width = 170;
            dgv.Columns["Old Gold Rate"].Width = 170;
            dgv.Columns["Silver Rate"].Width = 170;
            dgv.Columns["MetalRate"].Width = 170;
            dgv.Columns["Old Silver Rate"].Width = 170;
            dgv.Columns["Platinum Rate"].Width = 170;
            dgv.Columns["Created By"].Width = 100;
            dgv.Columns["Created Date"].Width = 125;
            dgv.Columns["Last Modified By"].Width = 125;
            dgv.Columns["Last Modified Date"].Width = 140;
            dgv.Columns["Is Active"].Width = 75;
        }
        private void PopulateGrid()
        {
            dgv.EntryFormName = this;
            dgv.ShowEdit = true;
            dgv.IsList = true;
            dgv.AutoGenerateColumns = true;
            dgv.DataFields = new List<string>() { "MetalRateId", "[Entry Date]", "[Entry Time]", "[Bar Rate]", "[Board Rate]", "[Old Gold Rate]","MetalRate","[Silver Rate]", " [Old Silver Rate]", "[Platinum Rate]", "[Created By]", "[Created Date]", "[Last Modified By]", "[Last Modified Date]", "[Is Active]", "Counter_id", "Company_id", "Branch_id" };
            dgv.HiddenDataFields = new List<string>() { "MetalRateId",  "Counter_id", "Company_id", "Branch_id" };

            if (!SAFA.Classes.Common.ChkUserType(DBConn, Convert.ToInt64(txtCrUserId.Text)))
            {
                dgv.HiddenDataFields.AddRange(new string[] { "Created By", "Created Date", "Last Modified By", "Last Modified Date" });
            }
            dgv.Fill(new Table(SAFA.Classes.Common.DbName, "GEN", "VMetalRate", true), "Company_ID=" + txtcompId.Text, "[Entry Date] desc,[Entry Time] desc");
             
        }

        private void MetalRate_Load(object sender, EventArgs e)
        {
            
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            base.Save();
            this.Close();
        }

        
    }
}
