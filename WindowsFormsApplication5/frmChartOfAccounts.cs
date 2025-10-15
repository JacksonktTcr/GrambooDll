using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JMS.Forms.ACC
{
    public partial class frmChartOfAccounts : Gramboo.Controls.GrbForm
    {
        public frmChartOfAccounts()
        {
            InitializeComponent();
        }

        public override void RefreshData()
        {
            base.RefreshData();
            dgvAccounts1.DataSource = DBConn.GetData(new System.Data.SqlClient.SqlCommand("Select Acc_LedgerId ,	GroupName as [Chart Of Accounts]	,level,	orderSequence	,IsGroup FROM ACC.LedgerTree(" + txtcompId.Text + ") order by orderSequence")).Tables[0];
        
            foreach(DataGridViewColumn c  in dgvAccounts1.Columns)
            {
                if (c.DataPropertyName != "Chart Of Accounts" && c.Index>1)
                {
                    c.Visible = false;
                }
                else
                {
                    c.Width = this.Width -100;
                    c.ReadOnly = true;
                }
            }

            foreach (DataGridViewRow r in dgvAccounts1.Rows)
            {
                if (Convert.ToBoolean(r.Cells["IsGroup"].Value.ToString()) == true)
                {
                    r.DefaultCellStyle.ForeColor = Color.Black;



                    r.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);


                }
                else
                {
                    r.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                }
            }

        }

        private void dgvAccounts1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvAccounts1.IsList = true;
                  Dictionary<string,object> prm=new Dictionary<string,object>();

            if (Convert.ToBoolean(dgvAccounts1.Rows[e.RowIndex].Cells["IsGroup"].Value.ToString()) == true)
            {
                dgvAccounts1.EntryFormName = GroupMaster.Instance;
                prm.Add("Acc_GroupId", dgvAccounts1.Rows[e.RowIndex].Cells[0].Value);
            }
            else
            {
                dgvAccounts1.EntryFormName = LedgerMaster.Instance;
                prm.Add("Acc_LedgerId", dgvAccounts1.Rows[e.RowIndex].Cells[0].Value);

            }
            dgvAccounts1.GotoEntryForm(e.RowIndex);
            dgvAccounts1.EntryFormName.FillData(prm);

        }
    }
}
