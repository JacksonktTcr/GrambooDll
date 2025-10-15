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
    public partial class Form3 : Gramboo.Controls.GrbGridReport
    {
        public Form3()
        {
            InitializeComponent();

            HiddenColumns = new List<string>() { "JobNoId", "Company_id", "Branch_id" };
            SummaryColumns = new List<string> { "[Product Wt]", "DiaNo", "Diawt", "StoneNo", "Stonewt" };
            FillTable = new Gramboo.Database.Table("JMS", "PROD", "VOrnamentFinishing", true);
            
            
            Columns = new List<string> { "VchNo", "PolishingDate", "[Workshop Name]", "[Employee Name]", "JobNo", "[Item Name]", "PurityValue", "[Product Wt]", "DiaNo", "Diawt", "StoneNo", "Stonewt" };
            criteria = " Company_id=2 AND Branch_Id=201";
        }

        private void Form3_ParentChanged(object sender, EventArgs e)
        {
            if (this.MdiParent!=null)
            ContainerDockPanel = ((MDIParent1)this.MdiParent).dockPanel1;
        }
    }
}
