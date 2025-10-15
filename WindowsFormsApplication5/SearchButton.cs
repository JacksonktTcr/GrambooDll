using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gramboo.Controls;
using System.ComponentModel.Design;

namespace REGAL.Classes
{
    public partial class SearchButton : Button
    {
        FindByVoucherNo fbv = new FindByVoucherNo();
        private ContainerControl _containerControl = null;



        public string TableName { get; set; }
        public string PrimaryKey { get; set; } 
            public string VoucherNoColumn { get; set; }

        public int VoucherTypeId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; } 

        public ContainerControl ContainerControl
        {
            get { return _containerControl; }
            set { _containerControl = value; }
        }
        public override ISite Site
        {
            get { return base.Site; }
            set
            {
                base.Site = value;
                if (value == null)
                {
                    return;
                }

                IDesignerHost host = value.GetService(
                    typeof(IDesignerHost)) as IDesignerHost;
                if (host != null)
                {
                    IComponent componentHost = host.RootComponent;
                    if (componentHost is ContainerControl)
                    {
                        ContainerControl = componentHost as ContainerControl;
                    }
                }
            }
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //this.BackgroundImage = Properties.Resources.Search;
            this.BackgroundImageLayout = ImageLayout.Stretch;

        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            fbv.Left = this.Left;
            fbv.Name="fbvSearch";
            fbv.Top = this.Top + this.Height;
            fbv.CompanyId = this.CompanyId;
            fbv.BranchId = this.BranchId;
            fbv.TableName = this.TableName;
            fbv.VoucherTypeId = this.VoucherTypeId;
            fbv.VoucherNoColumn = this.VoucherNoColumn;
            fbv.PrimaryKey = this.PrimaryKey;
            fbv.CurrentForm = (GrbForm)this.ContainerControl;
            fbv.DBCon = ((GrbForm)this.ContainerControl).DBConn;
             this.ContainerControl.Controls.Add(fbv);
            fbv.Show();
            fbv.BringToFront();

        }
    }
}
