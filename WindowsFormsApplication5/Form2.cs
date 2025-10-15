using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Form2 : Gramboo.Controls.GrbForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        public override void RefreshData()
        {
            base.RefreshData();

            //Gramboo.General.Setupcombo(grbComboBox1, "ITM.ItemMaster", "ItemName", "ItemId");
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void grbTextBox1_TextChanged(object sender, EventArgs e)
        {
            double i;

            i = Convert.ToDouble(grbTextBox1.Text);
        }

        private void grbButton1_Click(object sender, EventArgs e)
        {
            //Gramboo.GRBConfig config;
            //if (Gramboo.GRBConfig.Open() != null)
            //{
            //    config = Gramboo.GRBConfig.Open();
            //}
            //else
            //{
            //    config = new Gramboo.GRBConfig();
            //}

            //config.Login.CompanyID = 1;
            //config.Save();
            //Save();

            byte[] b = grbPictureBox1.BinaryValue;
        }
    }
}
