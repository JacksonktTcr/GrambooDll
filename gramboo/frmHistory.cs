using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gramboo
{
    public partial class frmHistory : Form
    {
        public frmHistory()
        {
            InitializeComponent();
        }
        [DefaultValue("")]
        public string  TableName { get; set; }
        [DefaultValue(0)]
        public long EntryID { get; set; }
        private void frmHistory_Load(object sender, EventArgs e)
        {
            DataController dc = new DataController();

            if (TableName != "" && EntryID != 0)
            {
                dgvHistory.DataSource = dc.GetData( new System.Data.SqlClient.SqlCommand("Select Date,Action,[User Name],Description,XMLData from SYST.VLog where TableName='" + TableName + "' AND EntryID=" + EntryID),"history").Tables[0];
                dgvHistory.Columns["XMLData"].Visible = false;
            
            }


        }

        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dgvHistory_Click(object sender, EventArgs e)
        {

            if (dgvHistory.CurrentRow != null)
            {
                if (dgvHistory.CurrentRow.Cells["XMLData"].Value.ToString() != "")
                {
                    StringReader theReader = new StringReader(dgvHistory.CurrentRow.Cells["XMLData"].Value.ToString());
                    DataSet theDataSet = new DataSet();
                    theDataSet.ReadXml(theReader);

                    dgvview.DataSource = theDataSet.Tables[0];
                }
            }
        }
    }


}
