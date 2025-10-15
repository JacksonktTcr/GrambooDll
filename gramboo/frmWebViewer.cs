using Gramboo.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gramboo
{
    public partial class frmWebViewer : GrbForm
    {
        public frmWebViewer()
        {
            InitializeComponent();
        }
        string _path;

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                webBrowser1.Url = new Uri(_path);
            }
        }

        public override void Print()
        {
            base.Print();

            webBrowser1.Print();
        }
    }
}
