using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("col1", typeof(int));
            dt.Columns.Add("col2", typeof(string ));
            dt.Columns.Add("col3", typeof(DateTime));
            dt.Columns.Add("col4", typeof(int));

            dt.Rows.Add("1", "asc", DateTime.Now, "10");
            dt.Rows.Add("2", "asc43", DateTime.Now, "170");
            dt.Rows.Add("3", "afdsc", DateTime.Now, "10");
            dt.Rows.Add("4", "asfdc", DateTime.Now, "810");

            dt.AcceptChanges();
            grbDataGridView1.SummaryColumns = new string[]  { "col4" };
            grbDataGridView1.DataSource = dt;

        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            //grbPictureBox2.BinaryValue = grbPictureBox1.BinaryValue;
        }
    }
}

