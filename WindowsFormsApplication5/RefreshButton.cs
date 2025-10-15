using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REGAL.Classes
{
    public partial class RefreshButton : UserControl
    {
        public RefreshButton()
        {
            InitializeComponent();
            
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            ((Gramboo.Controls.GrbForm)this.ParentForm).RefreshData();
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ((Gramboo.Controls.GrbForm)this.ParentForm).RefreshData();
        }
    }
     
}
