using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form2 : Gramboo.Controls.GrbForm 
    {
        public Form2()
        {
            InitializeComponent();
            //Columns = new List<string> { "[PolishingDate]", "[Item Name]", "[Employee Name]", "JobNo", "[Product Wt]", "DiaNo", "DiaWt", "StoneNo", "StoneWt" };
            //SummaryColumns = new List<string> { "[Product Wt]", "DiaNo", "DiaWt", "StoneNo", "StoneWt" };
            //FillTable = new Gramboo.Database.Table("JMS20DEC", "PROD", "VOrnamentFinishing", true);

          //  HiddenColumns = new List<string>() { "JobNoId", "Company_id", "Branch_id" };
          //  SummaryColumns = new List<string> { "[Product Wt]", "DiaNo", "Diawt", "StoneNo", "Stonewt" };
          //FillTable = new Gramboo.Database.Table("JMS", "PROD", "VOrnamentFinishing", true);
          //  Columns = new List<string> { "VchNo", "PolishingDate", "[Workshop Name]", "[Employee Name]", "JobNo", "[Item Name]", "PurityValue", "[Product Wt]", "DiaNo", "Diawt", "StoneNo", "Stonewt" };
          //  criteria = " Company_id=1 AND Branch_Id=101";

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            float a;
            a = Convert.ToSingle(".232");
        }

         
    }
}
