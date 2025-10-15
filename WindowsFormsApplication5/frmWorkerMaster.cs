using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JMS.Forms.PROD.Worker
{
    public partial class frmWorkerMaster : Gramboo.Controls.GrbForm
    {
        private static frmWorkerMaster instance;


        public static frmWorkerMaster Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new frmWorkerMaster();
                }
                else if (instance.IsDisposed)
                {
                    instance = new frmWorkerMaster();
                }
                return instance;
            }
        }

        

        public frmWorkerMaster()
        {

            InitializeComponent();
         
        }

        public override void Init()
        {
            base.Init();
            dgvSkills.DataFields = new List<string>() { "EntryId","SkillID","[Skill Name]" };
            dgvSkills.HiddenDataFields = new List<string>() { "SkillID", "EntryId"};
            dgvSkills.Fill(new Table("LAVANYA", "PROD", "VWorkerSkills", true), "1=2");
            AdjustColumnWidths();
            cmbstaff.Focus();
            txtIsActive.Text = "1";
          //  txtEntryID.Text = "0";
        }
        public void AdjustColumnWidths()
        {
            dgvSkills.RowHeadersVisible = false;
            dgvSkills.Columns[0].Width = 50;
            dgvSkills.Columns["Skill Name"].Width = cmbSkills.Width + 0;
          
        }
        public override bool InitializeTables()
        {
            Table t = new Table("LAVANYA", "PROD", "WorkerMaster");
            t.IdTextBox = grbTextBox1;
            t.PrimaryKeys.Add("StaffId");
            t.NotUpdatables.Add("Company_id");
            t.NotUpdatables.Add("Branch_id");
            t.NotUpdatables.Add("Counter_id");
            Table Skills = new Table("LAVANYA", "PROD", "WorkerSkills",true );
            Skills.PrimaryKeys.Add("EntryID");
            Skills.FillView = new Table("LAVANYA", "PROD", "VWorkerSkills");
            Skills.DatagridView = dgvSkills;
            Skills.IdTextBox = txtEntryID;
            t.ChildTables.Add(Skills );

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

        public override void RefreshData()
        {
            txtcompId.Text = "1";
            txtBranchID.Text = "101";
            txtCounterId.Text = "11010001";
            txtCrUserId.Text = "1";
            txtModuserID.Text = "1";

            Gramboo.General.Setupcombo(cmbWorkshop, "PROD.WorkShopMaster", "WorkshopName", "WorkshopId", "IsActive='True' AND Company_id=" + txtcompId.Text);
            Gramboo.General.Setupcombo(cmbstaff, "EMP.EmployeeMaster", "EmpName", "EmpId", "IsActive='True' AND Company_id=" + txtcompId.Text);
            Gramboo.General.Setupcombo(cmbSkills, "PROD.SkillMaster", "SkillName", "SkillId", "IsActive='True'");
            Gramboo.General.Setupcombo(cmbpurity, "ITM.PurityMaster", "PurityName", "PurityId");

         }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtSkillID.Text = cmbSkills.SelectedValue.ToString();
            dgvSkills.Save();
        }       

        private void cmbSkills_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSkillID_TextChanged(object sender, EventArgs e)
        {

        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            grbTextBox1.Text = cmbstaff.SelectedValue.ToString();
            Save();
        }

      
        
                
            
           
        
       

        
      
    }
}
