using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Gramboo.Controls
{
     [Category("Gramboo Components")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(RadioButton))]
    public sealed  partial class GrbRadioButton : RadioButton   
    {
        

        public GrbRadioButton()
        {
            InitializeComponent();
            this.TrueValue ="" ;
        }
 



        #region Properties

      
   

        /// <summary>
        /// Gets or Sets the current value
        /// </summary>
        [Browsable(true  )]
        [Description("Gets or Set the value when checkbox control is checked")]
        [DefaultValue(null )]
        public string TrueValue{get;set;}

        public enum Level { Parent, Form };

         [DefaultValue(Level.Parent)]
        public Level GroupNameLevel { get; set; }

        public string GroupName { get; set; }

        #endregion

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            try
            {
                if (Checked)
                {
                    var arbControls = (dynamic)null;
                    switch (GroupNameLevel)
                    {
                        case Level.Parent:
                            if (this.Parent != null)
                                arbControls = GetAll(this.Parent, typeof(GrbRadioButton));
                            break;
                        case Level.Form:
                            Form form = this.FindForm();
                            if (form != null)
                                arbControls = GetAll(this.FindForm(), typeof(GrbRadioButton));
                            break;
                    }
                    if (arbControls != null)
                        foreach (Control control in arbControls)
                            if (control != this &&
                                (control as GrbRadioButton).GroupName == this.GroupName)
                                (control as GrbRadioButton).Checked = false;
                    if (this.Parent != null)
                    ((GrbRadioButtonGroup)this.Parent).Value = this.TrueValue;
                }
            }
            catch (Exception ex)
            {
                General.ShowMessage(ex.Message);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            if (!Checked)
                base.OnClick(e);
        }

        //protected override void OnPaint(PaintEventArgs pevent)
        //{
        //    base.OnPaint(pevent);

        //    CheckBoxRenderer.DrawParentBackground(pevent.Graphics, pevent.ClipRectangle, this);

        //    RadioButtonState radioButtonState;
        //    if (Checked)
        //    {
        //        radioButtonState = RadioButtonState.CheckedNormal;
        //        if (Focused)
        //            radioButtonState = RadioButtonState.CheckedHot;
        //        if (!Enabled)
        //            radioButtonState = RadioButtonState.CheckedDisabled;
        //    }
        //    else
        //    {
        //        radioButtonState = RadioButtonState.UncheckedNormal;
        //        if (Focused)
        //            radioButtonState = RadioButtonState.UncheckedHot;
        //        if (!Enabled)
        //            radioButtonState = RadioButtonState.UncheckedDisabled;
        //    }

        //    Size glyphSize = RadioButtonRenderer.GetGlyphSize(pevent.Graphics, radioButtonState);
        //    Rectangle rect = pevent.ClipRectangle;
        //    rect.Width -= glyphSize.Width;
        //    rect.Location = new Point(rect.Left + glyphSize.Width, rect.Top);

        //    RadioButtonRenderer.DrawRadioButton(pevent.Graphics, new System.Drawing.Point(0, rect.Height / 2 - glyphSize.Height / 2), rect, this.Text, this.Font, this.Focused, radioButtonState);
        //}

        private IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent != null)
            {
                if (this.Parent.GetType() != typeof(GrbRadioButtonGroup))
                {
                    throw new NotSupportedException("Unable to add this control . Only RadiobuttonGroup Control can  contain this control");

                }
            }
        }

     

    }
}
