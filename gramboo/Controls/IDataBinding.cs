using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel;
using Gramboo.Database;

namespace Gramboo.Controls
{
    /// <summary>
    /// Implements databinding for gramboo controls
    /// </summary>
    interface IDataBindinig
    {
        /// <summary>
        /// Gets or Sets Database field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Database field assosiated with the control")]
        string   DataField { get; set; }

        /// <summary>
        /// Gets or Sets Alias for Database field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Alias for Database field assosiated with the control")]
        string Alias { get; set; }


        /// <summary>
        /// Gets or Sets Table field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Table field assosiated with the control")]
        string   TableName { get; set; }


        /// <summary>
        /// Gets or Sets to which property data should be binded
        /// </summary>
        [Browsable(true)]
        [Description(" Gets or Sets to which property data should be binded")]
        string BindingProperty{ get; set; }
         
        bool AcceptBlankValue { get; set; }

        /// <summary>
        /// Gets or Sets  Whether show a Red mark near control if it is a compulsory field
        /// </summary>
        [Browsable(true)]
        [Description("  Gets or Sets  Whether show a Red mark near control if it is a compulsory field")]
        [DefaultValue(true)]
        bool ShowComplusoryMark { get; set; }


        /// <summary>
        /// Gets or Sets  Whether Control Bind to Datagridview
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets  Whether Control Bind to Datagridview")]
        [DefaultValue(false)]
        bool BindToDataGridview { get; set; }

        bool IsBlank();


       
    }


}
