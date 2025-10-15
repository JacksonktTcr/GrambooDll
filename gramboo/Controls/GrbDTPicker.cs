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
    [ToolboxBitmap(typeof(DateTimePicker ))]
    public sealed  partial class GrbDTPicker : DateTimePicker,IDataBindinig ,IVisualStyles 
    {
        string _TableName;
        private Color _BorderColor;
        ToolTip tt = new ToolTip();

        public GrbDTPicker()
        {
            InitializeComponent();
            ActiveBackColor = Color.White;
            NormalBackColor = Color.White;
            ActiveBorderColor = Color.FromArgb(77, 144, 254);
           NormalBorderColor = Color.Gray;
            _BorderColor = NormalBorderColor ;
            AcceptBlankValue = true;
            TableName = "";
            DataField = "None";
            this.BindingProperty = "Value";
            Alias = "None";
            ShowComplusoryMark = true;
            BindToDataGridview = false;
        }
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
        /// Gets or Sets Border color of the control when it is has focus 
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Border color of the control when it is has focus ")]
        [DefaultValue(typeof(Color), "77, 144, 254")]
        public Color ActiveBorderColor { get; set; }
        /// <summary>
        /// Gets or Sets background color of the control when it is has focus 
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets background color of the control when it is has focus ")]
        [DefaultValue(typeof(Color), "White")]
        public Color ActiveBackColor { get; set; }
        /// <summary>
        /// Gets or Sets Border color of the control when it is has no focus 
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Border color of the control when it is has no focus ")]
        [DefaultValue(typeof(Color), "Black")]
        public Color NormalBorderColor { get; set; }
        /// <summary>
        /// Gets or Sets background color of the control when it is has no focus 
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets background color of the control when it is has no focus ")]
        [DefaultValue(typeof(Color), "White")]
        public Color NormalBackColor { get; set; }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            this.BackColor = ActiveBackColor;
            _BorderColor = ActiveBorderColor;

        }

        public bool IsBlank()
        {
 
                return false;
           

        }


        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case 15: // this is the WM_PAINT message  
                    // invalidate the TextBox so that it gets refreshed properly  
                    this.Invalidate();
                    // call the default win32 Paint method for the TextBox first  
                    base.WndProc(ref m);
                    // now use our code to draw extra stuff over the TextBox  
                    Graphics g = Graphics.FromHwnd(this.Handle);
                    Brush b = new SolidBrush(_BorderColor);
                    Pen p = new Pen(b);
                    g.DrawLine(p, new Point(0, 0), new Point(this.DisplayRectangle.Width, 0));
                    g.DrawLine(p, new Point(0, this.DisplayRectangle.Height - 1), new Point(this.DisplayRectangle.Width, this.DisplayRectangle.Height - 1));
                    g.DrawLine(p, new Point(0, 0), new Point(0, this.DisplayRectangle.Height));
                    g.DrawLine(p, new Point(this.DisplayRectangle.Width - 1, 0), new Point(this.DisplayRectangle.Width - 1, this.DisplayRectangle.Height));

                    if (!DesignMode && !this.AcceptBlankValue && ShowComplusoryMark )
                    {
                        g = Graphics.FromHwnd(this.Parent.Handle);
                        b = new SolidBrush(Color.Red);


                        g.DrawString("*", new Font("Arial", 14, FontStyle.Regular), b, new PointF(this.Location.X + this.Width + 2, this.Location.Y));
                    }
                    g.Dispose();
                    b.Dispose();
                    p.Dispose();
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);


 
            _BorderColor = NormalBorderColor;
            this.BackColor = NormalBackColor;
        }
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
            tt.ToolTipTitle = Title;
            tt.ToolTipIcon = Icon;
            tt.ShowAlways = true;
            tt.IsBalloon = true;
            tt.SetToolTip(this, Message);
            tt.Show(Message, this, 5000);
        }
    }
}
