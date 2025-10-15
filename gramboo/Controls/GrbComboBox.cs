using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;
using Gramboo.Controls.PopupControl;

namespace Gramboo.Controls
{
     [Category("Gramboo Components")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(System.Windows.Forms.ComboBox ))]
    public  partial class GrbComboBox : System.Windows.Forms.ComboBox,IDataBindinig,IVisualStyles
    {

        // Internal variables to hold the mouse position and the control
        // position for calculating whether the mouse is inside the drop down
        // and whether we scrolled with the mouse inside the drop down
        private int yPos = 0;
        private int xPos = 0;
        private int scrollPos = 0;
        private int xFactor = -1;
        private int simpleOffset = 0;

        // Import the GetScrollInfo function from user32.dll
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetScrollInfo(IntPtr hWnd, int n, ref ScrollInfoStruct lpScrollInfo);

        // Win32 constants
        private const int SB_VERT = 1;
        private const int SIF_TRACKPOS = 0x10;
        private const int SIF_RANGE = 0x1;
        private const int SIF_POS = 0x4;
        private const int SIF_PAGE = 0x2;
        private const int SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS;

        private const int SCROLLBAR_WIDTH = 17;
        private const int LISTBOX_YOFFSET = 21;

        // Return structure for the GetScrollInfo method
        [StructLayout(LayoutKind.Sequential)]
        private struct ScrollInfoStruct
        {
            public int cbSize;
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }

        public event HoverEventHandler Hover;

        protected virtual void OnHover(HoverEventArgs e)
        {
            HoverEventHandler handler = Hover;
            if (handler != null)
            {
                // Invokes the delegates. 
                handler(this, e);
            }

        }

    
   
        private Color _BorderColor;
        ToolTip tt = new ToolTip();
        private bool _multicolumn;
        const int columnPadding = 5;
        private float[] columnWidths = new float[0];
        private String[] columnNames = new String[0];
        private int valueMemberColumnIndex = 0;
        private Color FocusColor = Color.LightBlue;
        private Color[] AltColors;
        private int[] _customwidths;
        public GrbComboBox()
        {
            InitializeComponent();

            base.FlatStyle = FlatStyle.Flat;
            this.Size = new System.Drawing.Size(100, this.Font.Height + 7);
            ActiveBackColor = Color.White;
            NormalBackColor = Color.White;
            ActiveBorderColor = Color.FromArgb(77, 144, 254);
           NormalBorderColor = Color.Gray;
            _BorderColor = NormalBorderColor;
            MultiColumn = false;
            AcceptBlankValue = true;
             TableName = "";
            DataField = "None";
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.BindingProperty = "Text";
             Alias = "None";
            ShowComplusoryMark = true;
            BindToDataGridview = false;
            _customwidths = null;
         }
        /// <summary>
        /// Gets or Sets whether control accepts blank values
        /// </summary>
        [DefaultValue(true)]
        [Browsable(true)]
        [Description("Gets or Sets whether control accepts blank values")]
        public bool AcceptBlankValue { get; set; }
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

        /// <summary>
        /// Gets or Sets Database field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Table Name assosiated with the control")]
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
        /// Gets or Sets Database field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Table Name assosiated with the control")]
        [DefaultValue("None")]
        public string TableName { get; set; }

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
        /// Gets or Sets to which property data should be binded
        /// </summary>
        [Browsable(true)]
        [Description(" Gets or Sets to which property data should be binded")]
        [DefaultValue("Text")]
       public  string BindingProperty { get; set; }

        /// <summary>
        /// Gets or sets check whether the value is already exists in a collection
        /// </summary>
        [Browsable(true)]
        [Description(" Gets or sets check whether the value is already exists in a collection")]
        [DefaultValue(false)]
        public bool CheckDuplicates { get; set; }

        /// <summary>
        /// Gets or Sets whether the dropdown contains multicolumn values
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets whether the dropdown contains multicolumn values")]
        [DefaultValue(false )]
        public bool MultiColumn
        {
            get
            {
                return _multicolumn;
            }
            set
            {
                _multicolumn = value;
                if (_multicolumn == true)
                {
                    DrawMode = DrawMode.OwnerDrawVariable;
                    //this.FindForm().Controls.Add(dgv);

                }
                else
                {
                    DrawMode = this.DrawMode;
                    //this.FindForm().Controls.Remove(dgv);

                }
            }
        }

        /// <summary>
        /// Gets or Sets Width of the columns in multicolumn dropdown
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Width of the columns in multicolumn dropdown")]
        [DefaultValue(null )]
        public int[] CustomWidths
        {
            get
            {
                return _customwidths;
            }
            set
            {
                _customwidths = value;
                
            }
        }


        public override int SelectedIndex
        {
            get
            {
                //if (this.FindStringExact(this.Text)>-1)
                //{
                    if (_multicolumn)
                    {
                        if (base.SelectedIndex <= 0)
                        {
                            return -1;
                        }
                        else
                        {
                            return base.SelectedIndex;

                        }

                    }
                    else
                    {
                        return base.SelectedIndex;
                    }
                //}
                //else
                //{
                //    return -1;
                //}

            }
            set
            {
                base.SelectedIndex = value;
            }
        }

        public new DrawMode DrawMode
        {
            get
            {
                return base.DrawMode;
            }
            set
            {
                if (value != DrawMode.OwnerDrawVariable && _multicolumn == true)
                {
                    throw new NotSupportedException("Needs to be DrawMode.OwnerDrawVariable");
                }
                base.DrawMode = value;
            }
        }

        public new ComboBoxStyle DropDownStyle
        {
            get
            {
                return base.DropDownStyle;
            }
            set
            {
                if (value == ComboBoxStyle.Simple && _multicolumn == true)
                {
                    throw new NotSupportedException("ComboBoxStyle.Simple not supported");
                }
                base.DropDownStyle = value;
            }
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);

            InitializeColumns();
            if (_multicolumn)
            {
                if (this.DataSource != null)
                {
                    DataRow r;
                    r = ((DataTable)this.DataSource).NewRow();

                    foreach (DataColumn c in ((DataTable)this.DataSource).Columns)
                    {
                        r[c] = DBNull.Value;
                    }
                    if (((DataTable)this.DataSource).Rows.Count > 0)
                    {
                        if (((DataTable)this.DataSource).Rows[0][DisplayMember] != null)
                        {
                            ((DataTable)this.DataSource).Rows.InsertAt(r, 0);
                        }
                    }
                }
            }
        }

        protected override void OnValueMemberChanged(EventArgs e)
        {
            base.OnValueMemberChanged(e);

            InitializeValueMemberColumn();
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);
            if (_multicolumn)
            {
                this.DropDownWidth = (int)CalculateTotalWidth();
             }
        }

        public override bool PreProcessMessage(ref Message msg)
        {
            int WM_SYSKEYDOWN = 0x104;

            bool handled = false;

            if (msg.Msg == WM_SYSKEYDOWN && _multicolumn )
            {
                Keys keyCode = (Keys)msg.WParam & Keys.KeyCode;

                switch (keyCode)
                {
                    case Keys.Down:
                        handled = true;
                        break;
                }
            }

            if (false == handled)
                handled = base.PreProcessMessage(ref msg);

            return handled;
        }

       
        private void InitializeColumns()
        {

            if (DataManager == null)
                return;
            PropertyDescriptorCollection propertyDescriptorCollection = DataManager.GetItemProperties();

            columnWidths = new float[propertyDescriptorCollection.Count];
            columnNames = new String[propertyDescriptorCollection.Count];

            for (int colIndex = 0; colIndex < propertyDescriptorCollection.Count; colIndex++)
            {
                String name = propertyDescriptorCollection[colIndex].Name;
                columnNames[colIndex] = name;
            }
        }

        private void InitializeValueMemberColumn()
        {
            int colIndex = 0;
            foreach (String columnName in columnNames)
            {
                if (String.Compare(columnName, ValueMember, true, CultureInfo.CurrentUICulture) == 0)
                {
                    valueMemberColumnIndex = colIndex;
                    break;
                }
                colIndex++;
            }
        }

        private float CalculateTotalWidth()
        {
            float totWidth = 0;
            foreach (int width in columnWidths)
            {
                totWidth += (width + columnPadding);
            }
            return totWidth + SystemInformation.VerticalScrollBarWidth;
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);

            if (DesignMode)
                return;
            if (CustomWidths == null)
            {
                for (int colIndex = 0; colIndex < columnNames.Length; colIndex++)
                {
                    string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[colIndex]));
                    SizeF sizeF = e.Graphics.MeasureString(item, Font);
                    columnWidths[colIndex] = Math.Max(columnWidths[colIndex], sizeF.Width);
                }
            }
            else
            {
                for (int colIndex = 0; colIndex < columnNames.Length; colIndex++)
                {
                    string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[colIndex]));
                    SizeF sizeF = e.Graphics.MeasureString(item, Font);
                    if (colIndex < CustomWidths.Length)
                    {
                        columnWidths[colIndex] = CustomWidths[colIndex];
                    }
                    else
                    {
                        columnWidths[colIndex] = Math.Max(columnWidths[colIndex], sizeF.Width);
                    }
                }
            }
            float totWidth = CalculateTotalWidth();

            e.ItemWidth = (int)totWidth;
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            this.BackColor = ActiveBackColor;
            _BorderColor = ActiveBorderColor;
            this.Select(this.Text.Length, 0);

            
        }



        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            if (this.SelectedIndex == -1)
            {
                if (this.Text.Trim().Length == 0)
                {
                    this.SelectedItem = null;
                }
                else
                {


                    if (this.FindStringExact(this.Text) > -1)
                    {
                        this.SelectedItem = this.Items[this.FindStringExact(this.Text)];
                        this.Select(this.Text.Length, 1);
                    }
                    else
                    {
                        this.SelectedItem = null;
                    }

                }
            }
        }

        protected override void OnSelectedItemChanged(EventArgs e)
        {
            base.OnSelectedItemChanged(e);
            if (base.SelectedIndex == 0 && _multicolumn)
            {
                this.SelectedItem = null;
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m) 
        {

            //This message code indicates the value in the list is changing
            //32 is for DropDownStyle == Simple
            if ((m.Msg == 308) || (m.Msg == 32))
            {
                int onScreenIndex = 0;

                // Get the mouse position relative to this control
                Point LocalMousePosition = this.PointToClient(Cursor.Position);
                xPos = LocalMousePosition.X;

                if (this.DropDownStyle == ComboBoxStyle.Simple)
                {
                    yPos = LocalMousePosition.Y - (this.ItemHeight + 10);
                }
                else
                {
                    yPos = LocalMousePosition.Y - this.Size.Height - 1;
                }

                // save our y position which we need to ensure the cursor is
                // inside the drop down list for updating purposes
                int oldYPos = yPos;

                // get the 0-based index of where the cursor is on screen
                // as if it were inside the listbox
                while (yPos >= this.ItemHeight)
                {
                    yPos -= this.ItemHeight;
                    onScreenIndex++;
                }

                //if (yPos < 0) { onScreenIndex = -1; }
                ScrollInfoStruct si = new ScrollInfoStruct();
                si.fMask = SIF_ALL;
                si.cbSize = Marshal.SizeOf(si);
                // msg.LParam holds the hWnd to the drop down list that appears
                int getScrollInfoResult = 0;
                getScrollInfoResult = GetScrollInfo(m.LParam, SB_VERT, ref si);

                // k returns 0 on error, so if there is no error add the current
                // track position of the scrollbar to our index
                if (getScrollInfoResult > 0)
                {
                    onScreenIndex += si.nTrackPos;

                    if (this.DropDownStyle == ComboBoxStyle.Simple)
                    {
                        simpleOffset = si.nTrackPos;
                    }
                }

                // Add our offset modifier if we're a simple combobox since we don't
                // continuously receive scrollbar information in this mode.
                // Then make sure the item we're previewing is actually on screen.
                if (this.DropDownStyle == ComboBoxStyle.Simple)
                {
                    onScreenIndex += simpleOffset;
                    if (onScreenIndex > ((this.DropDownHeight / this.ItemHeight) + simpleOffset))
                    {
                        onScreenIndex = ((this.DropDownHeight / this.ItemHeight) + simpleOffset - 1);
                    }
                }

                // Check we're actually inside the drop down window that appears and 
                // not just over its scrollbar before we actually try to update anything
                // then if we are raise the Hover event for this comboBox
                if (!(xPos > this.Width - SCROLLBAR_WIDTH || xPos < 1 || oldYPos < 0 || ((oldYPos > this.ItemHeight * this.MaxDropDownItems) && this.DropDownStyle != ComboBoxStyle.Simple)))
                {
                    HoverEventArgs e = new HoverEventArgs();
                    e.itemIndex = (onScreenIndex > this.Items.Count - 1) ? this.Items.Count - 1 : onScreenIndex;
                    OnHover(e);
                    // if scrollPos doesn't equal the nPos from our ScrollInfoStruct then
                    // the mousewheel was most likely used to scroll the drop down list
                    // while the mouse was inside it - this means we have to manually
                    // tell the drop down to repaint itself to update where it is hovering
                    // still posible to "outscroll" this method but it works better than
                    // without it present
                    if (scrollPos != si.nPos)
                    {
                        Cursor.Position = new Point(Cursor.Position.X + xFactor, Cursor.Position.Y);
                        xFactor = -xFactor;
                    }
                }
                scrollPos = si.nPos;
            }

            switch (m.Msg)
            {
                case 15: // this is the WM_PAINT message  
                     //invalidate the TextBox so that it gets refreshed properly  
                    this.Invalidate();
                     //call the default win32 Paint method for the TextBox first  
                    base.WndProc(ref m);
                     //now use our code to draw extra stuff over the TextBox  
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
        //private void ShowGrid()
        //{
        //    dgv.DataSource = (DataTable)this.DataSource;
        //    this.FindForm().Controls.Add(dgv);
        //    Popup pup = new Popup(dgv);
        //    pup.Show(this);
        //    this.Focus();
        //}
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);



            if (this.AcceptBlankValue == false && this.Text.Trim() == "")
            {
                ShowBaloon("Blank values not allowed.", "Warning", ToolTipIcon.Warning);
            }
             _BorderColor = NormalBorderColor;
            this.BackColor = NormalBackColor;
         }

        //<summary>
        //Displays baloon style tooltip message 
        //</summary>
        //<param name="Message">Message to be displayed</param>
        //<param name="Title">Title of the message</param>
        //<param name="Icon">Icon assosiated with the control</param>
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
        private void ShowBaloon(string Message, string Title = "Information", ToolTipIcon Icon = ToolTipIcon.Info)
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

 


        public bool IsBlank()
        {
            if (this.GetType().GetProperty(this.BindingProperty).GetValue(this, null) == null)
            {
                ShowBaloon("Blank values not allowed.", "Warning", ToolTipIcon.Warning);
                return true;
            }
            else if (this.GetType().GetProperty(this.BindingProperty).GetValue(this, null).ToString().Trim() == "")
            {
                ShowBaloon("Blank values not allowed.", "Warning", ToolTipIcon.Warning);
                return true;
            }
            else
            {
                return false;
            }
 
        }


        private void DrawHeader(DrawItemEventArgs e, ref Rectangle boundsRect, ref int lastRight)
        {
            int colIndex = 0;
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 191, 219, 255)), boundsRect);

            using (Pen linePen = new Pen(Color.RoyalBlue ))
            {
                using (SolidBrush brush = new SolidBrush(ForeColor))
                {
                    foreach (string c in columnNames)
                    {
                        if ((int)columnWidths[colIndex] > 0)
                        {
                            string item = c;
                            e.Graphics.DrawLine(linePen, lastRight, boundsRect.Height - 2, boundsRect.Width, boundsRect.Height - 2);
                            boundsRect.X = lastRight;
                            boundsRect.Width = (int)columnWidths[colIndex] + columnPadding;
                            lastRight = boundsRect.Right;

                            //if (colIndex == valueMemberColumnIndex)
                            //{
                            using (Font boldFont = new Font(Font, FontStyle.Bold))
                            {
                                e.Graphics.DrawString(item, boldFont, brush, boundsRect);
                            }
                            //}
                            //else
                            //{
                            //    e.Graphics.DrawString(item, Font, brush, boundsRect);
                            //}

                            if (colIndex < columnNames.Length - 1)
                            {
                                e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom);
                            }
                        }
                        colIndex++;

                    }

                }
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (DesignMode)
                return;
            AltColors = new Color[] { System.Drawing.ColorTranslator.FromHtml("#FFFFFA"), System.Drawing.ColorTranslator.FromHtml("#E4ECF7") };

            if (e.Index < 0)
                return;
            System.Windows.Forms.ComboBox combo = this;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.FillRectangle(new SolidBrush(FocusColor),
                                         e.Bounds);
            else
            {
                SolidBrush b;

                b = new SolidBrush(AltColors[e.Index % 2]);

                e.Graphics.FillRectangle(b,
                                         e.Bounds);

            }
            if (_multicolumn == false)
            {


                if (this.DataSource == null)
                {
                    e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font,
                                          new SolidBrush(combo.ForeColor),
                                          new Point(e.Bounds.X, e.Bounds.Y));
                }
                else
                {
                    e.Graphics.DrawString(((DataTable)combo.DataSource).Rows[e.Index][this.DisplayMember].ToString(), e.Font,
                                          new SolidBrush(combo.ForeColor),
                                          new Point(e.Bounds.X, e.Bounds.Y));
                }

            }
            else
            {
                //e.DrawBackground();

                Rectangle boundsRect = e.Bounds;
                int lastRight = 0;

                using (Pen linePen = new Pen(Color.RoyalBlue))
                {
                    using (SolidBrush brush = new SolidBrush(ForeColor))
                    {
                        if (e.Index == 0)
                        {
                            DrawHeader(e, ref boundsRect, ref lastRight);
                        }

                        if (columnNames.Length == 0)
                        {
                            e.Graphics.DrawString(Convert.ToString(Items[e.Index]), Font, brush, boundsRect);
                        }
                        else
                        {
                            for (int colIndex = 0; colIndex < columnNames.Length; colIndex++)
                            {
                                if ((int)columnWidths[colIndex] > 0)
                                {
                                    string item = Convert.ToString(FilterItemOnProperty(Items[e.Index], columnNames[colIndex]));

                                    boundsRect.X = lastRight;
                                    boundsRect.Width = (int)columnWidths[colIndex] + columnPadding;
                                    lastRight = boundsRect.Right;

                                    if (item.Length > 0)
                                    {
                                        float f = 0;
                                        if (Single.TryParse(item, out f))
                                        {
                                            using (Graphics g = this.CreateGraphics())
                                            {
                                                string s = "";
                                                SizeF size;
                                                int numberOfCharacters = 0;
                                                float maxWidth = boundsRect.Width;
                                                while (true)
                                                {
                                                    s += "a";
                                                    size = g.MeasureString(s, this.Font);
                                                    if (size.Width > maxWidth)
                                                    {
                                                        break;
                                                    }
                                                    numberOfCharacters++;

                                                    item = item.PadLeft(numberOfCharacters-1);

                                                }
                                                // numberOfCharacters will now contain your max string length
                                            }
                                        }
                                    }
                                    if (colIndex == valueMemberColumnIndex)
                                    {
                                        using (Font boldFont = new Font(Font, FontStyle.Bold))
                                        {
                                            e.Graphics.DrawString(item, boldFont, brush, boundsRect);
                                        }
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(item, Font, brush, boundsRect);
                                    }

                                    if (colIndex < columnNames.Length - 1)
                                    {
                                        e.Graphics.DrawLine(linePen, boundsRect.Right, boundsRect.Top, boundsRect.Right, boundsRect.Bottom);
                                    }
                                }
                            }
                        }
                    }
                }

                e.DrawFocusRectangle();
            }
           //return ;

        }

    }

     /// <summary>
     /// Class that contains data for the hover event 
     /// </summary>
     public class HoverEventArgs : EventArgs
     {
         private int _itemIndex = 0;
         public int itemIndex
         {
             get
             {
                 return _itemIndex;
             }
             set
             {
                 _itemIndex = value;
             }
         }
     }

    /// <summary>
    /// Delegate declaration 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void HoverEventHandler(object sender, HoverEventArgs e);

 }

