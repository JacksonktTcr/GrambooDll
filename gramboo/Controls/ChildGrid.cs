using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gramboo.Controls
{
    class ChildGrid:GrbDataGridView 

    {
         public ChildGrid()
        {
            this.RowTemplate = new ChildDatagridViewRow(); 
        }

        /// <summary>
        /// Gets or Sets Parent Grid of the control 
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Parent Grid of the control  ")]
        [DefaultValue( null )]
        public GrbDataGridView  ParentGrid { get; set; }

        /// <summary>
        /// Gets or Sets Name Displayed on Context Menustrip 
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Name Displayed on Context Menustrip ")]
        [DefaultValue("")]
        public string MenuText { get; set; }

        internal class ChildDatagridViewRow : DataGridViewRow
        {

            public ChildDatagridViewRow()
            {
                
            }

            /// <summary>
            /// Gets or Sets Parent Grid of the control 
            /// </summary>
            [Browsable(true)]
            [Description("Gets or Sets Parent Grid Row of the control  ")]
            [DefaultValue(null)]
            public DataGridViewRow ParentRow { get; set; }
        }
    }
}
