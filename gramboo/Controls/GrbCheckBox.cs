using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gramboo.Controls
{
    [Category("Gramboo Components")] 
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(CheckBox))]
    public  partial  class GrbCheckBox : CheckBox, IDataBindinig
    {

        string _value;
        string _TableName;

        public GrbCheckBox()
        {
            InitializeComponent();
            this.TrueValue = "Y";
            this.FalseValue = "N";
            this.Value = "Y";
            this.BindingProperty = "Value";
            Alias = "None";
            DataField = "None";
            ShowComplusoryMark = true;
            BindToDataGridview = false;
        }



        #region Properties

        /// <summary>
        /// Gets or Sets Database field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Database field assosiated with the control")]
        [DefaultValue("None")]
        public string DataField { get; set; }

        /// <summary>
        /// Gets or Sets Alias for Database field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Alias for Database field assosiated with the control")]
        [DefaultValue("None")]
        public string Alias { get; set; }

        /// <summary>
        /// Gets or Sets Table field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Table field assosiated with the control")]
        [DefaultValue("")]
        public string TableName
        {

            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }

        /// <summary>
        /// Gets or Sets to which property data should be binded
        /// </summary>
        [Browsable(true)]
        [Description(" Gets or Sets to which property data should be binded")]
        [DefaultValue("Text")]
       public  string BindingProperty { get; set; }

        /// <summary>
        /// Gets or Sets whether control accepts blank values
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Description("Gets or Sets whether control accepts blank values")]
        public bool AcceptBlankValue { get; set; }


        /// <summary>
        /// Gets or Sets  Whether show a Red mark near control if it is a compulsory field
        /// </summary>
        [Browsable(true)]
        [Description("  Gets or Sets  Whether show a Red mark near control if it is a compulsory field")]
        [DefaultValue(true)]
        public bool ShowComplusoryMark { get; set; }
        /// <summary>
        /// Gets or Sets  Whether Control Bind to Datagridview
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets  Whether Control Bind to Datagridview")]
        [DefaultValue(false)]
        public bool BindToDataGridview { get; set; }
        /// <summary>
        /// Gets or Set the value when checkbox control is checked
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Set the value when checkbox control is checked")]
        [DefaultValue("Y")]
        public string TrueValue { get; set; }
        /// <summary>
        /// Gets or Set the value when checkbox control is not checked
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Set the value when checkbox control is not checked")]
        [DefaultValue("N" )]
        public string  FalseValue{ get; set; }

        /// <summary>
        /// Gets or Sets the current value
        /// </summary>
        [Browsable(true  )]
        [Description("Gets or Set the value when checkbox control is checked")]
        [DefaultValue("Y")]
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                if (value == TrueValue)
                {
                    this.Checked = true;
                }
                else
                {
                    this.Checked = false;
                }
            }
        }
        #endregion


        public bool IsBlank()
        {
            return false;
        }
      
    }
}
