using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;

namespace Gramboo.Controls
{
    [Category("Gramboo Components")] 
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(GroupBox))]
    public sealed  partial class GrbRadioButtonGroup : GroupBox, IDataBindinig
    {
 



        string _TableName;
        GrbRadioButton _defaultRadioButton=new GrbRadioButton();
        string _Value;

        public GrbRadioButtonGroup()
        {
            InitializeComponent();
            BindingProperty = "Value";
            AcceptBlankValue = true;
            Alias = "None";
            //if (this.Controls.Count >0)
            //_defaultRadioButton = (GrbRadioButton )this.Controls[0];
            //if ( _defaultRadioButton!=null)
            //Value = _defaultRadioButton.TrueValue;
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
        public string BindingProperty { get; set; }

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
        /// Gets or Sets the current value
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Set the value when checkbox control is checked")]
        [DefaultValue(null)]
        public string Value
        {
            get
            {
                return  _Value;

            }
            set
            {
                _Value = value;
                SelectRadioButton();
            }
        }



        [Editor(typeof(DefaultControlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public GrbRadioButton DefaultRadioButton
        {
            get
            {
                return _defaultRadioButton;

            }
            set
            {

                _defaultRadioButton = value;
                if (_defaultRadioButton != null)
                {
                    _defaultRadioButton.Checked = true;
                    this.Value = _defaultRadioButton.TrueValue;
                }
            }
        }



        #endregion

        /// <summary>
        /// Displays baloon style tooltip message 
        /// </summary>
        /// <param name="Message">Message to be displayed</param>
        /// <param name="Title">Title of the message</param>
        /// <param name="Icon">Icon assosiated with the control</param>
        public void ShowMessage(string Message, string Title = "Information", ToolTipIcon Icon = ToolTipIcon.Info)
        {
            if (DesignMode)
                return;
            //GrambooToolTip.Show(this, Message, Title, GrambooToolTip.BalloonIcon.ShowInformation);
            ToolTip tt = new ToolTip();
            tt.ToolTipTitle = Title;
            tt.ToolTipIcon = Icon;
            tt.ShowAlways = true;
            tt.IsBalloon = true;
            tt.SetToolTip(this, Message);
            tt.Show(Message, this, 5000);
        }

        public bool IsBlank()
        {
            if (this.GetType().GetProperty(this.BindingProperty).GetValue(this, null) == null)
            {
               ShowMessage("Blank values not allowed.", "Warning", ToolTipIcon.Warning);
                return true;
            }
            else if (this.GetType().GetProperty(this.BindingProperty).GetValue(this, null).ToString().Trim() == "")
            {
                ShowMessage("Blank values not allowed.", "Warning", ToolTipIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }

        }
        private void SelectRadioButton()
        {
            if (_Value == null)
                return;
            try
            {
                foreach (GrbRadioButton R in this.Controls)
                {
                    if (R.TrueValue.Trim() == _Value.Trim())
                    {
                        R.Checked = true;
                        return;
                    }
                    else
                    {
                        R.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
            }
        }


        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            if (e.Control.GetType() != typeof(GrbRadioButton))
            {
                throw new NotSupportedException("Unable to add this control . Only Radiobutton Control can be added to this control");
            }

        }
        internal class DefaultControlEditor : ObjectSelectorEditor
        {
            protected override void FillTreeWithData(Selector theSel,
              ITypeDescriptorContext theCtx, IServiceProvider theProvider)
            {
                base.FillTreeWithData(theSel, theCtx, theProvider);  //clear the selection

                GrbRadioButtonGroup aCtl = (GrbRadioButtonGroup)theCtx.Instance;

                foreach (GrbRadioButton aIt in aCtl.Controls)
                {
                    SelectorNode aNd = new SelectorNode(aIt.Name, aIt);

                    theSel.Nodes.Add(aNd);

                    if (aIt == aCtl.DefaultRadioButton)
                        theSel.SelectedNode = aNd;
                }
            }
        }
    }
}
