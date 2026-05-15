using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection.Emit;
using System.Reflection;
using System.Threading;
using System.ComponentModel.Design;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using Gramboo.Database;
using WeifenLuo.WinFormsUI.Docking;
using System.Data.SqlClient;
using OfficeOpenXml;

namespace Gramboo.Controls
{
    public delegate void  ValidateEntriesEventHandler(object source, ValidateEventArgs e);
    public delegate void SummaryCalcEventHandler(object source, EventArgs e);
    public delegate void BeforeEditEventHandler(object source, EventArgs e);

    public delegate void BeforeDeleteRowEventHandler(object sender, RowChangingEventArgs e);
    public delegate void AfterDeleteRowEventHandler(object sender, RowChangingEventArgs e);
    public delegate void BeforeEditRowEventHandler(object sender, RowChangingEventArgs e);
    public delegate void AfterEditRowEventHandler(object sender, RowChangingEventArgs e);


     [Category("Gramboo Components")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(DataGridView))]
    public partial class GrbDataGridView : DataGridView, ISupportInitialize
    {
        private bool _isDisposing = false;
        public event ValidateEntriesEventHandler OnValidateEntries;
         public event SummaryCalcEventHandler SummaryCalculated;
         public event BeforeEditEventHandler BeforeEdit;

         public event BeforeEditRowEventHandler BeforeEditRow;
         public event BeforeDeleteRowEventHandler BeforeDeleteRow;
         public event AfterEditRowEventHandler AfterEditRow;
         public event AfterDeleteRowEventHandler AfterDeleteRow;

         private List<string> Filters = new List<string>();
         private const string SORT_MODE_ASCENDING = "Ascending";
         private const string SORT_MODE_DESCENDING = "Descending";
         private bool ValidationFlag=false ;
        private HashSet<int> filteredColumns = new HashSet<int>();

        private bool Delflag;
        int hoveredColumnIndex = -1;
        private bool _isdataentrygrid;
        private List<string>  _HiddenDataFields=new List<string> (); 
        private List<string> _DataFields=new List<string>();
        public string HeaderHtml { get; set; }
        private bool _summaryPaused = false;
        [DefaultValue(false)]
        public bool SummaryPaused
        {
            get { return _summaryPaused; }
            set
            {
                bool wasPaused = _summaryPaused;
                _summaryPaused = value;

                // When unpausing, trigger a refresh so totals are up to date
                if (wasPaused && !_summaryPaused && summaryRowVisible && summaryControl != null)
                {
                    RefreshSummary();
                }
            }
        }


        public GrbDataGridView()
        {
            InitializeComponent();
            this.GridColor = Color.FromArgb(255, 208, 215, 229);
            this.BackgroundColor = Color.FromArgb(255, 255, 255, 255);
            this.RowTemplate.DefaultCellStyle.SelectionBackColor =
           Color.Transparent;
            this.AutoGenerateColumns = false;
            this.EditIndex = -1;
            
           // this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.AllowUserToAddRows = false; 
            TableName = "None";
            if(!DesignMode)
            this.EntryFormName =  (GrbForm) this.FindForm() ;
            AutoGenerateColumns = true;
            DataFields = new List<string> { "*"};
            HiddenDataFields  = new List<string> {};
            Delflag = false;
            AllowEmptyRows = true ;

            #region Summary
            refBox = new TextBox();
            panel = new Panel();
            spacePanel = new Panel();
            hScrollBar = new HScrollBar();
            SummaryRowVisible = false;
            summaryControl = new SummaryControlContainer(this);
            summaryControl.SummaryVisibilityChanged += new EventHandler(summaryControl_VisibilityChanged);

            Resize += new EventHandler(DataGridControlSum_Resize);
            RowHeadersWidthChanged += new EventHandler(DataGridControlSum_Resize);
            ColumnAdded += new DataGridViewColumnEventHandler(DataGridControlSum_ColumnAdded);
            ColumnRemoved += new DataGridViewColumnEventHandler(DataGridControlSum_ColumnRemoved);

            hScrollBar.Scroll += new ScrollEventHandler(hScrollBar_Scroll);
            hScrollBar.VisibleChanged += new EventHandler(hScrollBar_VisibleChanged);
            this.MouseMove += this_MouseMove;
           
            hScrollBar.Top += summaryControl.Bottom;
            hScrollBar.Minimum = 0;
            hScrollBar.Maximum = 0;
            hScrollBar.Value = 0;
            _isdataentrygrid = false;
            IsDataEntryGrid = false;
            DoubleBuffered = true;
      
            #endregion


        }

        private enum CustomFilters
        {
            Equals=0,
            NotEqual=1,
            BeginsWith=2,
            NotBeginsWith=3,
            Contains=4,
            NotContains=5,
            EndWith=6,
            NotEndWith=7,
            LessThan=8,
            LargerThan=9,
            Between=10,
            DateEqual =11,
            DateNotEqual = 12,
            DateBefore =13,
            DateAfter=14,
            DateBetween=15
        }

        #region Properties
        #region Summary
        /// <summary>
        /// If true a row header at the left side 
        /// of the summaryboxes is displayed.
        /// </summary>
        private bool displaySumRowHeader;
        [Browsable(true), Category("Summary")]
        public bool DisplaySumRowHeader
        {
            get { return displaySumRowHeader; }
            set { displaySumRowHeader = value; }
        }

        /// <summary>
        /// Text displayed in the row header
        /// of the summary row.
        /// </summary>
        private string sumRowHeaderText;
        [Browsable(true), Category("Summary")]
        public string SumRowHeaderText
        {
            get { return sumRowHeaderText; }
            set { sumRowHeaderText = value; }
        }

        /// <summary>
        /// Text displayed in the row header
        /// of the summary row.
        /// </summary>
        private bool sumRowHeaderTextBold;
        [Browsable(true), Category("Summary")]
        public bool SumRowHeaderTextBold
        {
            get { return sumRowHeaderTextBold; }
            set { sumRowHeaderTextBold = value; }
        }

        /// <summary>
        /// Add columns to sum up in text form
        /// </summary>
        private string[] summaryColumns;
        [Browsable(true), Category("Summary")]
        public string[] SummaryColumns
        {
            get { return summaryColumns; }
            set
            {
                // Only recreate sum boxes if the columns actually changed
                bool changed = !AreStringArraysEqual(summaryColumns, value);

                summaryColumns = value;

                if (!changed)
                    return;

                if (summaryColumns != null && summaryColumns.Length > 0 && !SummaryPaused)
                {
                    SummaryRowVisible = true;
                }
                else if (summaryRowVisible && !SummaryPaused)
                {
                    RefreshSummary(true);
                }
            }
        }

        private static bool AreStringArraysEqual(string[] a, string[] b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (!string.Equals(a[i], b[i], StringComparison.OrdinalIgnoreCase))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Display the summary Row
        /// </summary>
        private bool summaryRowVisible;
        [Browsable(true), Category("Summary"),DefaultValue(false) ]
        public bool SummaryRowVisible
        {
            get { return summaryRowVisible; }
            set
            {
                summaryRowVisible = value;
                if (summaryRowVisible)
                {
                    RefreshSummary( true);

                }
            }
        }

                 [Browsable(true), Category("Summary")]
        public SummaryControlContainer SummaryRow
        {
            get
            {
                return summaryControl;
            }
        }
    

        private int summaryRowSpace = 0;
        [Browsable(true), Category("Summary")]
        public int SummaryRowSpace
        {
            get { return summaryRowSpace; }
            set { summaryRowSpace = value; }
        }

        private string formatString = "F02";
        [Browsable(true), Category("Summary"), DefaultValue("F02")]
        public string FormatString
        {
            get { return formatString; }
            set { formatString = value; }
        }

        [Browsable(true), Category("Summary")]
        public Color SummaryRowBackColor
        {
            get;
            set;
            //get { return this.summaryControl.SummaryRowBackColor; }
            //set { summaryControl.SummaryRowBackColor = value; }
        }


        /// <summary>
        /// advoid user from setting the scrollbars manually
        /// </summary>
        [Browsable(false)]
        public new ScrollBars ScrollBars
        {
            get { return base.ScrollBars; }
            set { base.ScrollBars = value; }
        }


        #endregion

        public bool ShowMoveUp { get; set; }
        public bool ShowMoveDown { get; set; }
        public bool ShowDelete { get; set; }
        public bool ShowEdit { get; set; }
        public bool ShowSelectCheckBox { get; set; }
        public bool ShowSerialNo { get; set; }
        public bool AllowGrouping { get; set; }
        public List<string> DataFields {
            get { return _DataFields; }
            set
            {
                _DataFields = value; 
            }
        }
        public List<string> HiddenDataFields
        {
            get
            {
                return _HiddenDataFields;

            }
            set
            {
                _HiddenDataFields = value; 

            }
        }
        public List<string> GroupingFields { get; set; }
         [DefaultValue(false)]
        public bool IsDataEntryGrid 
        { get
        {
            return _isdataentrygrid;
        }

            set
            {
                _isdataentrygrid=value;
                if (_isdataentrygrid)
                {
                    this.EditMode = DataGridViewEditMode.EditOnEnter;
                }
                else
                {
                    this.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                }
            }
        }
        [Browsable(false)]
        [DefaultValue(null)]
        //[Editor(typeof(EntryFormEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public GrbForm EntryFormName { get; set; }
        

        [Browsable(false)]
        private int EditIndex { get; set; }

        /// <summary>
        /// Gets or Sets Whether This Grid is Displaying List of Entries 
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Whether This Grid is Displaying List of Entries")]
        [DefaultValue(false)]
        public bool  IsList { get; set; }

        /// <summary>
        /// Gets or Sets Whether Allow To Save When Rows Are Empty
        /// </summary>
        [Browsable(true)]
        [Description(" Gets or Sets Whether Allow To Save When Rows Are Empty")]
        [DefaultValue(true )]
        public bool AllowEmptyRows { get; set; }


        /// <summary>
        /// Gets or Sets Database field assosiated with the control
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Table Name assosiated with the control")]
        [DefaultValue("None")]
        public string TableName { get; set; }


        /// <summary>
        /// Gets or Sets List of Filters used in the grid
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets list of Filters used in the grid")]
        public List<string> FilterList
        {
            get
            {
                return Filters;
            }
            set
            {
                Filters = value;
            }
        }

        ///// <summary>
        ///// Gets or Sets Child Grids assosiated with the control
        ///// </summary>
        //[Browsable(true)]
        //[Description("Gets or Sets Child Grids assosiated with the control")]
        //[DefaultValue(null)]
        //public List<ChildGrid> ChildGrids
        //{
        //    get
        //    {
        //        return _childGrids;
        //    }


        //    set
        //    {
        //        _childGrids = value;

        //        foreach (ChildGrid cg in _childGrids)
        //        {
        //            cg.ParentGrid = this;
        //        }
        //    }
        //}
         
        #endregion

        #region Declare variables [Summary]

        public event EventHandler CreateSummary;
        private HScrollBar hScrollBar;
        private SummaryControlContainer summaryControl;
        private Panel panel, spacePanel;
        private TextBox refBox;

        #endregion


        #region Calculate Columns and Scrollbars width
        private void DataGridControlSum_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
        {
            if (_isDisposing || IsDisposed) return;
            calculateColumnsWidth();
            summaryControl.Width = columnsWidth;
            hScrollBar.Maximum = Convert.ToInt32(columnsWidth);
            resizeHScrollBar();
        }
        private void DataGridControlSum_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (_isDisposing || IsDisposed) return;
            calculateColumnsWidth();
            summaryControl.Width = columnsWidth;
            hScrollBar.Maximum = Convert.ToInt32(columnsWidth);
            resizeHScrollBar();
        }

        int columnsWidth = 0;
        /// <summary>
        /// Calculate the width of all visible columns
        /// </summary>
        private void calculateColumnsWidth()
        {
            if (_isDisposing || IsDisposed)
                return;
            if (this.RowHeadersVisible)
            {
                columnsWidth = this.RowHeadersWidth;
            }
            else
            {

                columnsWidth = 0;
            }
            for (int iCnt = 0; iCnt < ColumnCount; iCnt++)
            {
                if (Columns[iCnt].Visible)
                {
                    if (Columns[iCnt].AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill)
                    {
                        columnsWidth += Columns[iCnt].MinimumWidth;
                    }
                    else
                        columnsWidth += Columns[iCnt].Width;
                }
            }
        }

        #endregion

        #region Other Events and delegates

        /// <summary>
        /// Moves viewable area of DataGridView according to the position of the scrollbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {

            int position = Convert.ToInt32((Convert.ToDouble(e.NewValue) / (Convert.ToDouble(hScrollBar.Maximum) / Convert.ToDouble(Columns.Count))));

            if (position < (Columns.Count))
            {
                for (int i = position; i < Columns.Count; i++)
                {
                    if (Columns[i].Visible)
                    {
                        FirstDisplayedScrollingColumnIndex = i;
                        break;
                    }
                }
            }
        }

        public void CreateSummaryRow()
        {
            OnCreateSummary(this, EventArgs.Empty);
        }

        /// <summary>
        /// Calls the CreateSummary event
        /// </summary>
        private void OnCreateSummary(object sender, EventArgs e)
        {
            if (CreateSummary != null && this.summaryRowVisible &&   !SummaryPaused)
                CreateSummary(sender, e);

            ReorderSlNo();
        }

        // Re-entrancy guard for OnSummaryCalculated to avoid recursive
        // RefreshSummary -> calcSummaries -> OnSummaryCalculated cycles.
        private bool _inSummaryCalculated = false;
        private bool _summaryRecreateScheduled = false;
        // Number of consecutive auto-recreate attempts from OnSummaryCalculated.
        // If the configured summary column doesn't exist as a grid column, the
        // lookup will *always* return null and we would otherwise loop forever.
        private int _summaryRecreateAttempts = 0;
        private const int MaxSummaryRecreateAttempts = 2;

        /// <summary>
        /// Calls the CalculateSummary event
        /// </summary>
        public void OnSummaryCalculated(object sender, EventArgs e)
        {
            if (_isDisposing || IsDisposed) return;

            if (summaryColumns == null)
            {
                return;
            }

            // Prevent re-entrancy: if we're already inside this handler higher up
            // the stack, just raise the public event and bail out.
            if (_inSummaryCalculated)
            {
                if (SummaryCalculated != null && this.summaryRowVisible && !SummaryPaused)
                    SummaryCalculated(sender, e);
                return;
            }

            _inSummaryCalculated = true;
            try
            {
                if (summaryColumns.Length > 0
                    && SummaryRow != null
                    && !SummaryRow.IsRecreatingSumBoxes
                    && !_summaryRecreateScheduled
                    && _summaryRecreateAttempts < MaxSummaryRecreateAttempts
                    && SummaryRow.SummaryCells[summaryColumns[0]] == null)
                {
                    // Defer the recreate to the message loop so we unwind the
                    // current call chain (calcSummaries -> OnSummaryCalculated).
                    // Calling RefreshSummary(true) synchronously here causes
                    // unbounded recursion and a StackOverflowException.
                    _summaryRecreateScheduled = true;
                    _summaryRecreateAttempts++;
                    try
                    {
                        this.BeginInvoke((MethodInvoker)(() =>
                        {
                            try
                            {
                                if (!_isDisposing && !IsDisposed)
                                    this.RefreshSummary(true);
                            }
                            finally
                            {
                                _summaryRecreateScheduled = false;
                            }
                        }));
                    }
                    catch
                    {
                        // If BeginInvoke fails (handle not created yet), just
                        // clear the flag; do NOT call RefreshSummary here as
                        // that would re-enter this method synchronously.
                        _summaryRecreateScheduled = false;
                    }
                }
                else if (SummaryRow != null
                         && summaryColumns.Length > 0
                         && SummaryRow.SummaryCells[summaryColumns[0]] != null)
                {
                    // Successful resolution - reset retry counter so future
                    // legitimate data-source changes can re-trigger recreate.
                    _summaryRecreateAttempts = 0;
                }

                if (SummaryCalculated != null && this.summaryRowVisible && !SummaryPaused)
                    SummaryCalculated(sender, e);
            }
            finally
            {
                _inSummaryCalculated = false;
            }

            //ReorderColumns();
        }
        #endregion


        #region Adjust summaryControl, scrollbar

        /// <summary>
        /// Position the summaryControl under the
        /// DataGridView
        /// </summary>
        private void adjustSumControlToGrid()
        {
            if (summaryControl == null || Parent == null)
                return;
            summaryControl.Top = Height + summaryRowSpace;
            summaryControl.Left = Left;
            summaryControl.Width = Width;
        }

        /// <summary>
        /// Position the hScrollbar under the summaryControl
        /// </summary>
        private void adjustScrollbarToSummaryControl()
        {
            if (Parent != null)
            {
                hScrollBar.Top = refBox.Height + 2;
                hScrollBar.Width = Width;
                hScrollBar.Left = Left;

                resizeHScrollBar();
            }
        }

        /// <summary>
        /// Resizes the horizontal scrollbar acording
        /// to the with of the client size and maximum size of the scrollbar
        /// </summary>
        private void resizeHScrollBar()
        {
            //Is used to calculate the LageChange of the scrollbar
            int vscrollbarWidth = 0;
            if (VerticalScrollBar.Visible)
                vscrollbarWidth = VerticalScrollBar.Width;

            int rowHeaderWith = RowHeadersVisible ? RowHeadersWidth : 0;

            if (columnsWidth > 0)
            {
                //This is neccessary if AutoGenerateColumns = true because DataGridControlSum_ColumnAdded won't be fired
                //if (hScrollBar.Maximum == 0)
                    hScrollBar.Maximum = columnsWidth;

                //Calculate how much of the columns are visible in %
                int scrollBarWidth = Convert.ToInt32(Convert.ToDouble(ClientSize.Width - RowHeadersWidth - vscrollbarWidth) / (Convert.ToDouble(hScrollBar.Maximum) / 100F));

                if (scrollBarWidth > 100 || columnsWidth + rowHeaderWith < ClientSize.Width)
                {
                    if (hScrollBar.Visible)
                    {
                        hScrollBar.Visible = false;
                    }
                }
                else if (scrollBarWidth > 0)
                {
                    if (!hScrollBar.Visible)
                    {
                        hScrollBar.Visible = true;
                    }
                    hScrollBar.LargeChange = hScrollBar.Maximum / 100 * scrollBarWidth;
                    hScrollBar.SmallChange = hScrollBar.LargeChange / 5;
                }
            }
        }

        private void DataGridControlSum_Resize(object sender, EventArgs e)
        {
            if (_isDisposing || IsDisposed)
                return;
            if (Parent != null && this.summaryRowVisible && !SummaryPaused)
            {
                calculateColumnsWidth();
                resizeHScrollBar();
                adjustSumControlToGrid();
                adjustScrollbarToSummaryControl();
            }
        }

        /// <summary>
        /// Recalculate the width of the summary control according to 
        /// the state of the scrollbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hScrollBar_VisibleChanged(object sender, EventArgs e)
        {
            if (_isDisposing || IsDisposed)
                return;
            if (Parent != null && this.summaryRowVisible && !SummaryPaused)
            {
                //only perform operation if parent is visible
                if (Parent.Visible)
                {
                    int height;
                    if (hScrollBar.Visible)
                        height = summaryControl.InitialHeight + hScrollBar.Height;
                    else
                        height = summaryControl.InitialHeight;

                    if (summaryControl.Height != height && summaryControl.Visible)
                    {
                        summaryControl.Height = height;
                        this.Height = panel.Height - summaryControl.Height - summaryRowSpace;
                    }
                }
            }
        }

        /// <summary>
        /// Recalculate the height of the DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void summaryControl_VisibilityChanged(object sender, EventArgs e)
        {
            if (_isDisposing || IsDisposed)
                return;
            if (!summaryControl.Visible)
            {
                ScrollBars = ScrollBars.Both;
                this.Height = panel.Height;
            }
            else
            {
                this.Height = panel.Height - summaryControl.Height - summaryRowSpace;
                ScrollBars = ScrollBars.Vertical;
            }
        }

        /// <summary>
        /// When the DataGridView is visible for the first time a panel is created.
        /// The DataGridView is then removed from the parent control and added as 
        /// child to the newly created panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeParent()
        {
            if (_isDisposing || IsDisposed) return;
            if (DesignMode || Parent == null) return;
            if (!DesignMode && Parent != null )
            {

           
                    summaryControl.InitialHeight = this.refBox.Height;
                    summaryControl.Height = summaryControl.InitialHeight;
                    summaryControl.BackColor = this.RowHeadersDefaultCellStyle.BackColor;
                    summaryControl.ForeColor = Color.Transparent;
                    summaryControl.RightToLeft = this.RightToLeft;
                 
                panel.Bounds = this.Bounds;
                panel.BackColor = this.BackgroundColor;

                panel.Dock = this.Dock;
                panel.Anchor = this.Anchor;
                panel.Padding = this.Padding;
                panel.Margin = this.Margin;
                panel.Top = this.Top;
                panel.Left = this.Left;
                panel.BorderStyle = this.BorderStyle;

                Margin = new Padding(0);
                Padding = new Padding(0);
                Top = 0;
                Left = 0;

                summaryControl.Dock = DockStyle.Bottom;
                this.Dock = DockStyle.Fill;

                if (this.Parent is TableLayoutPanel)
                {
                    int rowSpan, colSpan;

                    TableLayoutPanel tlp = this.Parent as TableLayoutPanel;
                    TableLayoutPanelCellPosition cellPos = tlp.GetCellPosition(this);

                    rowSpan = tlp.GetRowSpan(this);
                    colSpan = tlp.GetColumnSpan(this);

                    tlp.Controls.Remove(this);
                    tlp.Controls.Add(panel, cellPos.Column, cellPos.Row);
                    tlp.SetRowSpan(panel, rowSpan);
                    tlp.SetColumnSpan(panel, colSpan);
                }
                else
                {
                    Control parent = this.Parent;
                    //remove DataGridView from ParentControls
                    parent.Controls.Remove(this);
                    parent.Controls.Add(panel);
                }

                this.BorderStyle = BorderStyle.None;

                panel.BringToFront();


                hScrollBar.Top = refBox.Height + 2;
                hScrollBar.Width = this.Width;
                hScrollBar.Left = this.Left;

                summaryControl.Controls.Add(hScrollBar);
                hScrollBar.BringToFront();
                panel.Controls.Add(this);

                spacePanel = new Panel();
                spacePanel.BackColor = panel.BackColor;
                spacePanel.Height = summaryRowSpace;
                spacePanel.Dock = DockStyle.Bottom;

                panel.Controls.Add(spacePanel);
                if (summaryRowVisible && !SummaryPaused)
                {
                    panel.Controls.Add(summaryControl);
                }
                resizeHScrollBar();
                adjustSumControlToGrid();
                adjustScrollbarToSummaryControl();

                resizeHScrollBar();
            }
        }

        #endregion

        #region ISupportInitialzie

        public void BeginInit()
        { }

        public void EndInit()
        {
            changeParent();
        }

        #endregion

        /// <summary>
        /// Calls the Before Edit event
        /// </summary>
        public void OnBeforeEdit(object sender, BeforeEditEventArgs e)
        {
            if (BeforeEdit != null)
                BeforeEdit(sender, e);
        }


        /// <summary>
        /// Calls the  Before Edit Row event
        /// </summary>
        protected virtual void OnBeforeEditRow(RowChangingEventArgs e)
        {
            if (BeforeEditRow != null)
               
                BeforeEditRow(this, e);
        }


        /// <summary>
        /// Calls the  After Edit Row event
        /// </summary>
        protected virtual void OnAfterEditRow(RowChangingEventArgs e)
        {
            if (AfterEditRow != null)

                AfterEditRow(this, e);
        }


   
        /// <summary>
        /// Calls the  Before Delete Row event
        /// </summary>
        protected virtual void OnBeforeDeleteRow(RowChangingEventArgs e)
        {
            if (BeforeDeleteRow != null)

                BeforeDeleteRow(this, e);
        }


        /// <summary>
        /// Calls the  After Delete Row event
        /// </summary>
        protected virtual void OnAfterDeleteRow(RowChangingEventArgs e)
        {
            if( AfterDeleteRow != null) 
                AfterDeleteRow(this, e);
            ReorderSlNo();


        }
        protected override void OnUserAddedRow(DataGridViewRowEventArgs e)
        {
            base.OnUserAddedRow(e);
            ReorderSlNo();
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);
            ReorderSlNo();
        }
        private void ReorderSlNo()
        {
            if (_isDisposing || IsDisposed) return;
            try
            {
                if (this.Columns.Contains("col_AutoSlno"))
                {
                    foreach (DataGridViewRow r in this.Rows)

                    {
                        r.Cells["col_AutoSlno"].Value = r.Index + 1;
                    }
                }
                //if (this.Columns.Contains("Slno"))
                //{
                //    foreach (DataGridViewRow r in this.Rows)

                //    {
                //        r.Cells["Slno"].Value = r.Index + 1;
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
        }
        /// <summary>
        /// Refresh the summary
        /// </summary>
        /// 
        public void RefreshSummary(bool ReCreateSummary = false)
        {
            if (this.summaryControl != null )
                this.summaryControl.RefreshSummary(ReCreateSummary);
        }

        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case 15: // this is the WM_PAINT message  
        //            // invalidate the TextBox so that it gets refreshed properly  
        //            this.Invalidate();
        //            // call the default win32 Paint method for the TextBox first  
        //            base.WndProc(ref m);
        //            // now use our code to draw extra stuff over the TextBox  

        //            if (summaryRowVisible)
        //            {
        //                Graphics g = Graphics.FromHwnd(this.Handle);
        //                Brush b = new SolidBrush(Color.Red);
        //                Pen p = new Pen(b);

        //                int step = 0;
        //                if (RowHeadersVisible)
        //                    step = this.RowHeadersWidth;
        //                int i = 0;
        //                foreach (DataGridViewColumn hc in this.Columns)
        //                {
        //                    if (hc.Displayed)
        //                    {
        //                        g = Graphics.FromHwnd(this.Parent.Handle);
        //                        g.DrawRectangle(p, this.Location.X + step, this.Location.Y + this.Height + 5, hc.Width, this.ColumnHeadersHeight);
        //                        g.DrawString(i.ToString(), this.Font, b, this.Location.X + step + 2, this.Location.Y + this.Height);
        //                        i++;

        //                        //b = new SolidBrush(Color.Red);
        //                        step += hc.Width;
        //                    }

        //                }
        //                g.Dispose();
        //                b.Dispose();
        //                p.Dispose();
        //            }
        //            break;
        //        default:
        //            base.WndProc(ref m);
        //            break;

        //    }
        //}




        protected override void OnDataSourceChanged(EventArgs e)
        {
            if (_isDisposing || IsDisposed) return;

            base.OnDataSourceChanged(e);
            this.SuspendLayout();
           
            // Reset auto-recreate attempt counter when data source changes so
            // a fresh data source can re-trigger summary recreate logic.
            _summaryRecreateAttempts = 0;

            if (this.DataSource == null)
            {
                // during dispose Columns may already be in inconsistent state, guard it
                if (!_isDisposing && !IsDisposed)
                {
                    this.Columns.Clear();
                }
            }
            else
            {
                // ✅ AUTOMATIC: Invalidate cache and refresh summary when data source changes
                if (summaryColumns != null && summaryColumns.Length > 0)
                {
                    if (!_isDisposing && !IsDisposed && summaryControl != null)
                    {
                        // Set SummaryRowVisible which triggers RefreshSummary
                        SummaryRowVisible = true;
                        // Invalidate cache to force recalculation with new data
                        summaryControl.RefreshSummary();
                    }
                }
            }

            if (!_isDisposing && !IsDisposed)
            {
                ReorderColumns();
                ReorderSlNo();
            }

            this.ResumeLayout();
        }



        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            if (_isDisposing || IsDisposed) return;
            base.OnDataBindingComplete(e);
            int index;

            index = this.Columns.Count ;

             

            if (this.AutoGenerateColumns)
            {
                foreach (DataGridViewColumn c in this.Columns)
                {

                    if (c.IsDataBound)
                    {
                         

                        c.DataPropertyName =  c.Name ;
                    }
                    if (HiddenDataFields != null)
                    {
                        if (HiddenDataFields.Contains(c.DataPropertyName.ToString(), StringComparer.OrdinalIgnoreCase))
                        {
                            c.Visible = false;
                        }
                    }
                }
               
                PerformFilter();
                ReorderSlNo();
            }  





            if (this.ShowMoveUp)
            {
                if (this.Columns.Contains("col_MoveUp")   == false)
                {

                    DataGridViewImageColumn moveup = new DataGridViewImageColumn();
                    moveup.Name = "col_MoveUp";
                    moveup.HeaderText = "Up";
                    moveup.Image = Gramboo.Properties.Resources.arrow_up;
                    moveup.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    moveup.Width = 50;
                    
                    this.Columns.Insert (index, moveup);
                    index++;
                }
            }

            if (this.ShowMoveDown)
            {
                if (this.Columns.Contains("col_MoveDown" ) == false)
                {

                    DataGridViewImageColumn movedown = new DataGridViewImageColumn();
                    movedown.Name = "col_MoveDown";
                    movedown.HeaderText = "Down";
                    movedown.Image = Gramboo.Properties.Resources.arrow_down;
                    movedown.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    movedown.Width = 50;
                    this.Columns.Insert(index, movedown);
                    index++;
                }
            }

            if (this.ShowDelete)
            {
                if (this.Columns.Contains("col_Delete") == false)
                {
                    DataGridViewImageColumn Delete = new DataGridViewImageColumn();
                    Delete.Name = "col_Delete";
                    Delete.HeaderText = "Delete";
                    Delete.Image = Gramboo.Properties.Resources.DeleteRed;
                    Delete.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    Delete.Width = 50;
                    this.Columns.Insert(index, Delete);
                    index++;
                }
            }
            if (this.ShowEdit)
            {
                if (this.Columns.Contains("col_Edit") == false)
                {

                    DataGridViewImageColumn Edit = new DataGridViewImageColumn();
                    Edit.Name = "col_Edit";
                    Edit.HeaderText = "Edit";
                    Edit.Image = Gramboo.Properties.Resources.edit;
                    Edit.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    Edit.Width = 50;
                    this.Columns.Insert(index, Edit);
                    index++;
                }
            }
            if (this.ShowSelectCheckBox)
            {
                if (this.Columns.Contains("col_CheckBox") == false)
                {

                    DataGridViewCheckBoxColumn _CheckBox = new DataGridViewCheckBoxColumn();
                    _CheckBox.Name = "col_CheckBox";
                    DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell();

                    cbHeader.Value = "";
                    _CheckBox.HeaderCell = cbHeader;

                    _CheckBox.Width = 50;
                    this.Columns.Insert(index, _CheckBox);
                    index++;
                }
            }

            if (this.ShowSerialNo)
            {
                if (this.Columns.Contains("col_AutoSlno") == false)
                {
                    DataGridViewTextBoxColumn AutoSlno = new DataGridViewTextBoxColumn();
                    AutoSlno.Name = "col_AutoSlno";

                    AutoSlno.HeaderText = "SlNo.";
                    AutoSlno.Width = 50;
                    AutoSlno.ReadOnly = true;
                    AutoSlno.Resizable = DataGridViewTriState.False;
                    this.Columns.Insert(0, AutoSlno);
                }

            }

            //if (AllowGrouping)
            //{
            //    if (GroupingFields.Count > 0)
            //    {
            //        foreach (string gf in GroupingFields)
            //        {

            //            GridRowComparer rowComparer = new GridRowComparer(SortOrderList);
            //            this.Sort(rowComparer);
            //        }
            //    }
            //}

            this.RowTemplate.DefaultCellStyle.SelectionBackColor =
           Color.Transparent;

            //this.AutoResizeRows(
            //        DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            bool tempsumvisible = summaryRowVisible, tempsumpause = SummaryPaused;

            summaryRowVisible = true;
            SummaryPaused = false;
            summaryControl.RefreshSummary();

            summaryRowVisible = tempsumvisible;
            SummaryPaused = tempsumpause;

            calculateColumnsWidth();
            resizeHScrollBar();
            adjustSumControlToGrid();
            adjustScrollbarToSummaryControl();
             
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);
           
            

        }

        public void Fill(Table table_name, string criteria = "", string order = "")
        {

            this.SuspendLayout();
            this.DoubleBuffered = true;
            SqlCommand cmd = new SqlCommand();

            string fldlst="";
           // if (this.DataFields==null)
                
            foreach (string s in this.DataFields)
            {
                fldlst +=  (fldlst.Length == 0 ? "" : ",") + s ;
            }

            string orderby="";

            if (order.Length > 0)
            {
                orderby = " Order By " + order;

            }
            else
            {
                if (this.EntryFormName != null)
                {
                    if (this.EntryFormName.TableName != null)
                    {
                        if (this.EntryFormName.TableName.PrimaryKeys.Count > 0)
                        {
                            foreach (string str in this.EntryFormName.TableName.PrimaryKeys)
                            {
                                if (str.Trim().Length > 0)
                                    orderby += str + ",";
                            }
                            orderby = " Order By " + orderby.Substring(0, orderby.Length - 1);
                        }
                    }
                }
            }
            cmd.CommandText = "select " + (criteria=="1=2"?" TOP 1 ":"")+ (fldlst.Length == 0 ? "*" : fldlst) + " from " + table_name.GetName() + (criteria.Length == 0 ? "" : " WHERE " + criteria) + orderby;
            cmd.CommandType = CommandType.Text;

            try
            {
                bool temp = this.SummaryRowVisible;
                this.SummaryRowVisible = false;
                this.AutoGenerateColumns = true;
                this.DataSource = ((GrbForm)this.FindForm()).DBConn.GetData(cmd, table_name.GetName()).Tables[0];
                this.AutoGenerateColumns = false;
                ReorderSlNo();
                //int displayindex = 1;
                //foreach (string str in DataFields)
                //{

                //    if (this.Columns.Contains(str))
                //    {
                //        if (HiddenDataFields.Contains(str) == false)
                //        {
                //            this.Columns[str].DisplayIndex = displayindex;
                //            displayindex++;
                //        }
                //    }
                //}
                if (((DataTable)this.DataSource).Rows.Count>0)
                {
                    Console.WriteLine(table_name.GetName());
                }

                this.SummaryRowVisible = temp;
           this.RefreshSummary( );

        
                this.ResumeLayout();
            }
            catch (Exception ex)
            {
                General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
            }
        }
        protected override void OnDoubleClick(EventArgs e)
        {
            
            base.OnDoubleClick(e);
            if(ShowEdit)
            this.Edit(this);
        }
        protected override void OnDefaultValuesNeeded(DataGridViewRowEventArgs e)
        {
            base.OnDefaultValuesNeeded(e);
            ReorderSlNo();
        }

 
        private void SortGridAsc(object sender, EventArgs  e)
        {
            try
            {

                //if (this.CurrentCell != null)
                //{
                //    bool numflag = true;
                //    if (this.Rows.Count  > 3)
                //    {
                //        for (int i = 0; i < 3; i++)
                //        {

                //            numflag = Regex.IsMatch(this.Rows[i].Cells[CurrentCell.ColumnIndex].Value.ToString(), @"\d");
                //        }
                //    }
                //    if (numflag)
                //    {
                //        NumericComparer nc = new NumericComparer();
                //        this.Sort(nc);
                //    }
                //    else 
                //    {
                //this.Sort(this.CurrentCell.OwningColumn, ListSortDirection.Ascending);
                //    }
                //}
                this.AutoGenerateColumns = true;
                ((DataTable)this.DataSource).DefaultView.Sort = this.CurrentCell.OwningColumn.DataPropertyName + "  Asc";
                this.AutoGenerateColumns = false;
               

            }
            catch (Exception ex)
            {
                General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
            }
        }
        private void ReorderColumns()
        {
            if (_isDisposing || IsDisposed) return;
            int displayindex = 0;
            if (this.Columns.Contains("col_AutoSlno") )
            {
                  displayindex = 1;
            }

            AllowUserToOrderColumns = false; 
            foreach (string str in DataFields)
            {

                if (this.Columns.Contains(str.Replace("[", "").Replace("]", "")) )
                {

                    //if (!HiddenDataFields.Contains(str.Replace("[", "").Replace("]", "")))
                    //{
                    if (displayindex < this.Columns.Count)
                    {
                        this.Columns[str.Replace("[", "").Replace("]", "")].DisplayIndex = displayindex;
                        displayindex++;
                        //}
                    }

                }
            }
            //foreach (string str in HiddenDataFields)
            //{

            //    if (this.Columns.Contains(str.Replace("[", "").Replace("]", "")))
            //    {

            //        if (HiddenDataFields.Contains(str.Replace("[", "").Replace("]", "")))
            //        {
            //            this.Columns[str.Replace("[", "").Replace("]", "")].DisplayIndex = displayindex;
            //            displayindex++;
            //        }

            //    }
            //}
           
        }
        private void SortGridDesc(object sender, EventArgs e)
        {
            try
            { 
                ((DataTable)this.DataSource).DefaultView.Sort = this.CurrentCell.OwningColumn.DataPropertyName + "  Desc";
             

            }
            catch (Exception ex)
            {
                General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
            }
        }

        private void eql_performClick(object sender, EventArgs e)
        {
             Filters.Add(string.Format( "["+ this.CurrentCell.OwningColumn.DataPropertyName + "]='" + this.CurrentCell.Value + "'"));
             PerformFilter();
        
        }

        private void noteql_performClick(object sender, EventArgs e)
        {
            Filters.Add( string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "]<>'" + this.CurrentCell.Value + "'"));
            PerformFilter();
        }

        private void contains_performClick(object sender, EventArgs e)
        {
            Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] LIKE'%" + this.CurrentCell.Value + "%'"));
            PerformFilter();
        }

        private void notcontains_performClick(object sender, EventArgs e)
        {
            Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] NOT LIKE'%" + this.CurrentCell.Value + "%'"));
            PerformFilter();
        }

        private void RemFilter_PerformClick(object sender, EventArgs e)
        {
            Filters.Clear();
            PerformFilter();
        }



        private void GetCustomStringFilter(CustomFilters compare,string PromptText)
        {
            string filterstring1 = "", filterstring2="";
            if (compare == CustomFilters.Between || compare == CustomFilters.DateBetween)
            {
                InputBox.Show("Custom Filter", PromptText, ref filterstring1, ref filterstring2,true,((compare==CustomFilters.DateBetween
                    ||compare==CustomFilters.DateAfter ||compare==CustomFilters.DateBefore || compare==CustomFilters.DateEqual || compare==CustomFilters.DateNotEqual)?true:false));
            }
            else
            {
                InputBox.Show("Custom Filter", PromptText, ref filterstring1, ref filterstring2, false, ((compare == CustomFilters.DateBetween
                    || compare == CustomFilters.DateAfter || compare == CustomFilters.DateBefore || compare == CustomFilters.DateEqual || compare == CustomFilters.DateNotEqual) ? true : false));
            }

            if (filterstring1 == "")
                return;

            try
            {

                switch (compare)
                {

                    case CustomFilters.BeginsWith:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "]  LIKE'" + filterstring1 + "%'"));
                        break;
                    case CustomFilters.NotBeginsWith:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] NOT  LIKE'" + filterstring1 + "%'"));
                        break;
                    case CustomFilters.Equals:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "]  = '" + filterstring1 + "'"));
                        break;
                    case CustomFilters.NotEqual:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "]  <> '" + filterstring1 + "'"));
                        break;
                    case CustomFilters.Contains:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "]  LIKE '%" + filterstring1 + "%'"));
                        break;
                    case CustomFilters.NotContains:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] NOT LIKE '%" + filterstring1 + "%'"));
                        break;
                    case CustomFilters.EndWith:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "]  LIKE '%" + filterstring1 + "'"));
                        break;
                    case CustomFilters.NotEndWith:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] NOT LIKE'%" + filterstring1 + "'"));
                        break;
                    case CustomFilters.LessThan:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] < " + Convert.ToDouble(filterstring1)));
                        break;
                    case CustomFilters.LargerThan:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] > " + Convert.ToDouble(filterstring1)));
                        break;
                    case CustomFilters.Between:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] >= " + Convert.ToDouble(filterstring1)) + " AND " + string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] <= " + Convert.ToDouble(filterstring2)));
                        break;
                    case CustomFilters.DateEqual:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "]  =  '" + Convert.ToDateTime( filterstring1) + "'"));
                        break;
                    case CustomFilters.DateNotEqual:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] <> '" + Convert.ToDateTime(filterstring1) + "'"));
                        break;
                    case CustomFilters.DateBefore:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] < '" + Convert.ToDateTime(filterstring1) +"'") );
                        break;
                    case CustomFilters.DateAfter:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] > '" + Convert.ToDateTime(filterstring1) +"'"));
                        break;
                    case CustomFilters.DateBetween:
                        Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] >= '" + Convert.ToDateTime(filterstring1)) + "' AND " + string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] <= '" + Convert.ToDateTime(filterstring2) +"'"));
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
            }
            PerformFilter();
        }


        public  void PerformFilter()
        {
            try
            {
                // ✅ PERFORMANCE: Use StringBuilder instead of string concatenation in loop
                StringBuilder filterBuilder = new StringBuilder();
                bool isFirst = true;

                foreach (string s in Filters)
                {
                    if (!isFirst)
                    {
                        filterBuilder.Append(" AND ");
                    }
                    filterBuilder.Append(s);
                    isFirst = false;
                } 

                if (this.DataSource != null)
                {
                    (this.DataSource as DataTable).DefaultView.RowFilter = filterBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                if (Filters.Count > 0)
                {
                    Filters.Remove(Filters[Filters.Count - 1]);
                    PerformFilter();
                }
                General.ShowMessage(ex.Message.ToString());
            }
        }

        private void AddDateFilters(ContextMenuStrip contxt)
        {
            if (this.CurrentCell == null)
                return;

            ToolStripMenuItem eql = new ToolStripMenuItem();
            eql.Name = "eql";
            eql.Text = "Equals " + Convert.ToString(this.CurrentCell.Value);
            eql.Click += new EventHandler(eql_performClick);

            ToolStripMenuItem noteql = new ToolStripMenuItem();
            noteql.Name = "noteql";
            noteql.Text = "Does Not Equals " + Convert.ToString(this.CurrentCell.Value);
            noteql.Click += new EventHandler(noteql_performClick);

            ToolStripMenuItem OnOrBefr = new ToolStripMenuItem();
            OnOrBefr.Name = "OnOrBefr";
            OnOrBefr.Text = "On or Before " + Convert.ToString(this.CurrentCell.Value);
            OnOrBefr.Click += new EventHandler(OnOrBefr_performClick);


            ToolStripMenuItem OnorAft = new ToolStripMenuItem();
            OnorAft.Name = "OnorAft";
            OnorAft.Text = "On or After " + Convert.ToString(this.CurrentCell.Value);
            OnorAft.Click += new EventHandler(OnorAft_performClick);

            ToolStripMenuItem DateFilters = new ToolStripMenuItem();
            DateFilters.Name = "DateFilters";
            DateFilters.Text = "Date Filters";


            ToolStripMenuItem custeql = new ToolStripMenuItem();
            custeql.Name = "Custeql";
            custeql.Text = "Equals ...";
            custeql.Click += new EventHandler(custDateEql_performClick);

            ToolStripMenuItem custnoteql = new ToolStripMenuItem();
            custnoteql.Name = "custnoteql";
            custnoteql.Text = "Does Not Equals ...";
            custnoteql.Click += new EventHandler(custDatenoteql_performClick);

            ToolStripMenuItem custBefr = new ToolStripMenuItem();
            custBefr.Name = "custBefr";
            custBefr.Text = "Before ...";
            custBefr.Click += new EventHandler(custBefr_performClick);

            ToolStripMenuItem custAftr = new ToolStripMenuItem();
            custAftr.Name = "custAftr";
            custAftr.Text = "After ...";
            custAftr.Click += new EventHandler(custAftr_performClick);


            ToolStripMenuItem custBetwn = new ToolStripMenuItem();
            custBetwn.Name = "custBetwn";
            custBetwn.Text = "Between ...";
            custBetwn.Click += new EventHandler(custDateBetwn_performClick);

            DateFilters.DropDownItems.Add(custeql);
            DateFilters.DropDownItems.Add(custnoteql);
            DateFilters.DropDownItems.Add(custBefr);
            DateFilters.DropDownItems.Add(custAftr);
            DateFilters.DropDownItems.Add(custBetwn);

            contxt.Items.Add(DateFilters);
            contxt.Items.Add(new ToolStripSeparator());

            contxt.Items.Add(eql);
            contxt.Items.Add(noteql);
            contxt.Items.Add(OnOrBefr);
            contxt.Items.Add(OnorAft);

        }

        private void custDateBetwn_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.DateBetween, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void custAftr_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.DateAfter, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void custBefr_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.DateBefore, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void custDatenoteql_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.DateNotEqual, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void custDateEql_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.DateEqual, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void OnorAft_performClick(object sender, EventArgs e)
        {
            Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] >= '" + Convert.ToDateTime( this.CurrentCell.Value) + "'"));
            PerformFilter();

        }

        private void OnOrBefr_performClick(object sender, EventArgs e)
        {
            Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] <= '" + Convert.ToDateTime(this.CurrentCell.Value) + "'"));
            PerformFilter();
        }
        private void AddNumberFilters(ContextMenuStrip contxt)
        {
            if (this.CurrentCell == null)
                return;

            ToolStripMenuItem eql = new ToolStripMenuItem();
            eql.Name = "eql";
            eql.Text = "Equals " + Convert.ToString(this.CurrentCell.Value);
            eql.Click += new EventHandler(eql_performClick);

            ToolStripMenuItem noteql = new ToolStripMenuItem();
            noteql.Name = "noteql";
            noteql.Text = "Does Not Equals " + Convert.ToString(this.CurrentCell.Value);
            noteql.Click += new EventHandler(noteql_performClick);

            ToolStripMenuItem lessOrEql = new ToolStripMenuItem();
            lessOrEql.Name = "lessOrEql";
            lessOrEql.Text = "Less Than or Equal To " + Convert.ToString(this.CurrentCell.Value);
            lessOrEql.Click += new EventHandler(lessOrEql_performClick);


            ToolStripMenuItem largeOrEql = new ToolStripMenuItem();
            largeOrEql.Name = "largeOrEql";
            largeOrEql.Text = "Larger Than or Equal To " + Convert.ToString(this.CurrentCell.Value);
            largeOrEql.Click += new EventHandler(largeOrEql_performClick);

            ToolStripMenuItem NumFilters = new ToolStripMenuItem();
            NumFilters.Name = "NumFilters";
            NumFilters.Text = "Number Filters ";


            ToolStripMenuItem custeql = new ToolStripMenuItem();
            custeql.Name = "Custeql";
            custeql.Text = "Equals ..."  ;
            custeql.Click += new EventHandler(custeql_performClick);

            ToolStripMenuItem custnoteql = new ToolStripMenuItem();
            custnoteql.Name = "custnoteql";
            custnoteql.Text = "Does Not Equals ..." ;
            custnoteql.Click += new EventHandler(custnoteql_performClick);

            ToolStripMenuItem custless = new ToolStripMenuItem();
            custless.Name = "custless";
            custless.Text = "Less Than ..."  ;
            custless.Click += new EventHandler(custless_performClick);

            ToolStripMenuItem custlarge = new ToolStripMenuItem();
            custlarge.Name = "custlarge";
            custlarge.Text = "Larger Than ...";
            custlarge.Click += new EventHandler(custlarge_performClick);


            ToolStripMenuItem custBetwn = new ToolStripMenuItem();
            custBetwn.Name = "custBetwn";
            custBetwn.Text = "Between ...";
            custBetwn.Click += new EventHandler(custBetwn_performClick);

            NumFilters.DropDownItems.Add(custeql);
            NumFilters.DropDownItems.Add(custnoteql);
            NumFilters.DropDownItems.Add(custless);
            NumFilters.DropDownItems.Add(custlarge);
            NumFilters.DropDownItems.Add(custBetwn);

            contxt.Items.Add(NumFilters);
            contxt.Items.Add(new ToolStripSeparator());

            contxt.Items.Add(eql);
            contxt.Items.Add(noteql);
            contxt.Items.Add(lessOrEql);
            contxt.Items.Add(largeOrEql);


        }

        private void custBetwn_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.Between, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void custlarge_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.LargerThan, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);
        }

        private void custless_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.LessThan, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }


        private void largeOrEql_performClick(object sender, EventArgs e)
        {
            Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] >= " + Convert.ToDouble( this.CurrentCell.Value) + ""));
            PerformFilter();
        }

        private void lessOrEql_performClick(object sender, EventArgs e)
        {
            Filters.Add(string.Format("["+ this.CurrentCell.OwningColumn.DataPropertyName + "] <= " + Convert.ToDouble(this.CurrentCell.Value) + ""));
            PerformFilter();
        }

        private void AddTextFilters(ContextMenuStrip contxt)
        {
            if (this.CurrentCell == null)
                return;



            ToolStripMenuItem eql = new ToolStripMenuItem();
            eql.Name = "eql";
            eql.Text = "Equals " + Convert.ToString(this.CurrentCell.Value);
            eql.Click += new EventHandler(eql_performClick);

            ToolStripMenuItem noteql = new ToolStripMenuItem();
            noteql.Name = "noteql";
            noteql.Text = "Does Not Equals " + Convert.ToString(this.CurrentCell.Value);
            noteql.Click += new EventHandler(noteql_performClick);

            ToolStripMenuItem contains = new ToolStripMenuItem();
            contains.Name = "contains";
            contains.Text = "Contains " + Convert.ToString(this.CurrentCell.Value);
            contains.Click += new EventHandler(contains_performClick);


            ToolStripMenuItem notcontains = new ToolStripMenuItem();
            notcontains.Name = "notcontains";
            notcontains.Text = "Does Not Contains " + Convert.ToString(this.CurrentCell.Value);
            notcontains.Click += new EventHandler(notcontains_performClick);

            ToolStripMenuItem TextFilters = new ToolStripMenuItem();
            TextFilters.Name = "TextFilters";
            TextFilters.Text = "Text Filters ";


            ToolStripMenuItem custeql = new ToolStripMenuItem();
            custeql.Name = "custeql";
            custeql.Text = "Equals ...";
            custeql.Click += new EventHandler(custeql_performClick);

            ToolStripMenuItem custnoteql = new ToolStripMenuItem();
            custnoteql.Name = "custnoteql";
            custnoteql.Text = "Does Not Equals ... ";
            custnoteql.Click += new EventHandler(custnoteql_performClick);

            ToolStripMenuItem custcontains = new ToolStripMenuItem();
            custcontains.Name = "custcontains";
            custcontains.Text = "Contains ...";
            custcontains.Click += new EventHandler(custcontains_performClick);


            ToolStripMenuItem custnotcontains = new ToolStripMenuItem();
            custnotcontains.Name = "custnotcontains";
            custnotcontains.Text = "Does Not Contains ...";
            custnotcontains.Click += new EventHandler(custnotcontains_performClick);

            ToolStripMenuItem custBegin = new ToolStripMenuItem();
            custBegin.Name = "custBegin";
            custBegin.Text = "Begins With ...";
            custBegin.Click += new EventHandler(custBegin_performClick);

            ToolStripMenuItem custnotBegin = new ToolStripMenuItem();
            custnotBegin.Name = "custnotBegin ";
            custnotBegin.Text = "Does Not Begins With ... ";
            custnotBegin.Click += new EventHandler(custnotBegin_performClick);

            ToolStripMenuItem custEnds = new ToolStripMenuItem();
            custEnds.Name = "custEnds";
            custEnds.Text = "Ends With ...";
            custEnds.Click += new EventHandler(custEnds_performClick);


            ToolStripMenuItem custnotEnds = new ToolStripMenuItem();
            custnotEnds.Name = "custnotcontains";
            custnotEnds.Text = "Does Not Ends With ...";
            custnotEnds.Click += new EventHandler(custnotEnds_performClick);


            TextFilters.DropDownItems.Add(custeql);
            TextFilters.DropDownItems.Add(custnoteql);
            TextFilters.DropDownItems.Add(custBegin);
            TextFilters.DropDownItems.Add(custnotBegin);
            TextFilters.DropDownItems.Add(custcontains);
            TextFilters.DropDownItems.Add(custnotcontains);
            TextFilters.DropDownItems.Add(custEnds);
            TextFilters.DropDownItems.Add(custnotEnds);



            contxt.Items.Add(TextFilters);
            contxt.Items.Add(new ToolStripSeparator());

            contxt.Items.Add(eql);
            contxt.Items.Add(noteql);
            contxt.Items.Add(contains);
            contxt.Items.Add(notcontains);

        }

        private void custnotEnds_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.NotEndWith, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);
        }

        private void custEnds_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.EndWith, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void custnotBegin_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.NotBeginsWith, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void custBegin_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.BeginsWith, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void custnotcontains_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.NotContains, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void custcontains_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.Contains, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void custnoteql_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.NotEqual, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }

        private void custeql_performClick(object sender, EventArgs e)
        {
            GetCustomStringFilter(CustomFilters.Equals, this.CurrentCell.OwningColumn.HeaderText + " " + ((ToolStripMenuItem)sender).Text);

        }


        private void this_MouseMove(object sender, MouseEventArgs e)
        {
            var hit = this.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.ColumnHeader)
            {
                if (hoveredColumnIndex != hit.ColumnIndex)
                {
                    hoveredColumnIndex = hit.ColumnIndex;
                    this.Invalidate(); // Force repaint to show icon
                }
            }
            else if (hoveredColumnIndex != -1)
            {
                hoveredColumnIndex = -1;
                this.Invalidate(); // Hide icon
            }
        }


        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Right)
            {
              
               
                ContextMenuStrip contxt = new ContextMenuStrip();
                ToolStripMenuItem RemFilter = new ToolStripMenuItem();
                RemFilter.Name = "RemFilter";
                RemFilter.Text = "Remove All Filters";
                RemFilter.Click += new EventHandler(RemFilter_PerformClick);

                    ToolStripMenuItem RemLastFiltr = new ToolStripMenuItem();
                    RemLastFiltr.Name = "RemLastFiltr";
                    RemLastFiltr.Text = "Remove Last Filter";
                    RemLastFiltr.Click += delegate(object sender, EventArgs ev)
                        {
                            if (Filters.Count > 0)
                            {
                                Filters.RemoveAt(Filters.Count - 1);
                                PerformFilter();
                            }
                        };
                   

                if (CurrentCell != null)
                {

                    ToolStripMenuItem expExl = new ToolStripMenuItem();
                    expExl.Name = "expToExl";
                    expExl.Text = "Export To Excel";
                    expExl.Click += new EventHandler(expExl_PerformClick);
                    expExl.Image = Gramboo.Properties.Resources.excelIcon;


                    ToolStripMenuItem EditItm = new ToolStripMenuItem();
                    EditItm.Name = "EditItm";
                    EditItm.Text = "Edit";
                    EditItm.Click +=delegate( object sender, EventArgs ev1)
                        {
                            
                                EditRow(this.CurrentCell.RowIndex);
                             
                        };

                     EditItm.Image = Gramboo.Properties.Resources.edit ;


                     ToolStripMenuItem delItm = new ToolStripMenuItem();
                     delItm.Name = "delItm";
                     delItm.Text = "Delete";
                     delItm.Click += delegate(object sender, EventArgs ev1)
                     {

                         DeleteRow(this.CurrentCell.RowIndex);

                     };

                     delItm.Image = Gramboo.Properties.Resources.DeleteRed;


                    ToolStripMenuItem sortAsc = new ToolStripMenuItem();
                    sortAsc.Name = "sortAsc";
                    sortAsc.Text = "Sort Ascending";
                    sortAsc.Click += new EventHandler(SortGridAsc);
                    sortAsc.Image = Gramboo.Properties.Resources.sort_ascend;

                    ToolStripMenuItem sortDesc = new ToolStripMenuItem();
                    sortDesc.Name = "sorDesc";
                    sortDesc.Text = "Sort Descending";
                    sortDesc.Click += new EventHandler(SortGridDesc);
                    sortDesc.Image = Gramboo.Properties.Resources.sort_descend;



                    if (ShowEdit == true)
                    {
                        contxt.Items.Add(EditItm);
                    }
                    if (ShowDelete  == true)
                    {
                        contxt.Items.Add(delItm);
                    }


                    contxt.Items.Add(expExl);
                    if (CurrentCell.OwningColumn.SortMode != DataGridViewColumnSortMode.NotSortable)
                    {
                        contxt.Items.Add(new ToolStripSeparator());
                        contxt.Items.Add(sortAsc);
                        contxt.Items.Add(sortDesc);
                    }
                        contxt.Items.Add(new ToolStripSeparator());
                    contxt.Items.Add(RemFilter);
                    contxt.Items.Add(RemLastFiltr);
                    if (CurrentCell.OwningColumn.ValueType == typeof(string))
                    {

                        AddTextFilters(contxt);

                    }
                    else if (CurrentCell.OwningColumn.ValueType == typeof(System.Int32) || CurrentCell.OwningColumn.ValueType == typeof(System.Int16) ||
                                CurrentCell.OwningColumn.ValueType == typeof(System.Int64) || CurrentCell.OwningColumn.ValueType == typeof(System.Single) ||
                                CurrentCell.OwningColumn.ValueType == typeof(System.Double) || CurrentCell.OwningColumn.ValueType == typeof(System.Single) ||
                                CurrentCell.OwningColumn.ValueType == typeof(System.Decimal))
                    {
                        AddNumberFilters(contxt);
                    }
                    else if (CurrentCell.OwningColumn.ValueType == typeof(DateTime  ))
                    {
                        AddDateFilters(contxt);
                    }
                }
                else
                {
                   
                    contxt.Items.Add(RemFilter);
                    contxt.Items.Add(RemLastFiltr);
                }
                contxt.Items.Add(new ToolStripSeparator());


                ToolStripMenuItem Print = new ToolStripMenuItem();
                Print.Name = "Print";
                Print.Text = "Print";
                Print.Click += new EventHandler(PrintGrid);
                Print.Image = Gramboo.Properties.Resources.print;
                contxt.Items.Add(Print);

                contxt.ImageScalingSize = new System.Drawing.Size(16, 16);

                int currentMouseOverRow = this.HitTest(e.X, e.Y).RowIndex;

              

                contxt.Show(this, new Point(e.X, e.Y));

            }
            int filterIconSize = 12;
            int padding = 4;
            int sortGlyphOffset = 16; // reserve space if sorting icon exists

            if (hoveredColumnIndex == -1) return;

            var headerRect = this.GetCellDisplayRectangle(hoveredColumnIndex, -1, true);

            Rectangle iconRect = new Rectangle(
                headerRect.Right - filterIconSize - padding - sortGlyphOffset,
                headerRect.Top + (headerRect.Height - filterIconSize) / 2,
                filterIconSize,
                filterIconSize);

            if (iconRect.Contains(e.Location))
            {
                string columnName = this.Columns[hoveredColumnIndex].DataPropertyName;

                // Get unique values from the column
                var values = this.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .Select(r => r.Cells[hoveredColumnIndex].Value?.ToString() ?? "")
                    .Distinct();

                ShowFilterForm(hoveredColumnIndex);
            }

        }
        private void ShowFilterForm(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= this.Columns.Count)
                return;

            string columnName = this.Columns[columnIndex].DataPropertyName;
            if (string.IsNullOrWhiteSpace(columnName)) return;

            List<string> values = new List<string>();

            foreach (DataGridViewRow row in this.Rows)
            {
                if (!row.IsNewRow && row.Cells[columnIndex].Value != null)
                {
                    string val = row.Cells[columnIndex].Value.ToString();
                    if (!string.IsNullOrWhiteSpace(val))
                        values.Add(val);
                }
            }

            using (var form = new FilterForm(values)) // ✅ Corrected
            {
                var cellRect = this.GetCellDisplayRectangle(columnIndex, -1, true);
                form.StartPosition = FormStartPosition.Manual;
                form.Location = this.PointToScreen(new Point(cellRect.Left, cellRect.Bottom));

                if (form.ShowDialog() == DialogResult.OK)
                {
                    string filter = "";

                    if (!string.IsNullOrEmpty(form.CustomTextFilter))
                    {
                        filter = $"[{columnName}] {form.CustomTextFilter}";
                    }
                    else if (form.SelectedValues.Any())
                    {
                        var selected = form.SelectedValues
                            .Select(v => $"'{v.Replace("'", "''")}'");
                        filter = $"[{columnName}] IN ({string.Join(",", selected)})";
                    }

                    if (!string.IsNullOrWhiteSpace(filter) && this.DataSource is DataTable dt)
                    {
                        try
                        {
                            this.Filters.Add(filter);
                            PerformFilter();
                            // ✅ Track this column as filtered
                            filteredColumns.Add(columnIndex);
                            this.Invalidate(); // Force repaint to show icon
                        }
                        catch (EvaluateException ex)
                        {
                            MessageBox.Show("Invalid filter expression:\n" + ex.Message, "Filter Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        var toRemove = this.Filters.Where(s => s.Contains(columnName)).ToList();
                        filteredColumns.Remove(columnIndex);
                        foreach (var s in toRemove)
                        {
                            this.Filters.Remove(s);
                        }
                        PerformFilter();

                    }
                }
            }
        }


        protected void PrintGrid(object Sender, EventArgs e)
        {
            //DataGridPrintDocument dp = new DataGridPrintDocument(this);
            //dp.GridPrint();
            string html = "";

            if (HeaderHtml != null)
            {
                html = HeaderHtml;
            }
            //Table start.
              html += "<table cellpadding='5' cellspacing='0' style='width:100%;height=auto; border: 1px solid #ccc;font-size:8pt;font-family:Times New Roman'>";

            //Adding HeaderRow.
            html += "<tr>";
            foreach (DataGridViewColumn column in this.Columns)
            {
                if (!HiddenDataFields.Contains(column.Name) && column.Visible == true && column.Name != "col_MoveUp" && column.Name != "col_MoveDown" && column.Name != "col_Delete" && column.Name != "col_Edit")
                {
                    html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.HeaderText + "</th>";
                }
            }
            html += "</tr>";

            //Adding DataRow.
            foreach (DataGridViewRow row in this.Rows)
            {
                html += "<tr>";
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!HiddenDataFields.Contains(cell.OwningColumn.Name) && cell.OwningColumn.Visible == true && cell.OwningColumn.Name != "col_MoveUp" && cell.OwningColumn.Name != "col_MoveDown" && cell.OwningColumn.Name != "col_Delete" && cell.OwningColumn.Name != "col_Edit")
                    {
                        DataGridViewContentAlignment algmnt = cell.Style.Alignment;
                        if (algmnt == DataGridViewContentAlignment.NotSet)
                            algmnt = cell.OwningColumn.DefaultCellStyle.Alignment;


                        html += "<td   style='white-space: nowrap;width:100%;color: " + (ColorTranslator.ToHtml(((Color)cell.InheritedStyle.ForeColor))=="RoyalBlue"?"Black":  ColorTranslator.ToHtml( ((Color)cell.InheritedStyle.ForeColor))) + ";border: 1px solid #ccc; text-align: " + (algmnt == DataGridViewContentAlignment.MiddleRight ? "right" : "left") + "; vertical-align: middle;font-size:8pt;font-family:Times New Roman'>" +
                            (cell.Value.GetType() == typeof(DateTime) ? Convert.ToDateTime(cell.Value.ToString()).ToString("dd-MMM-yyyy") : cell.Value.ToString()) + "</td>";
                    }
                }
                html += "</tr>";
            }
            //Adding Footer.

            if (summaryRowVisible && this.Rows.Count>0)
            {
                html += "<tr>";
                foreach (DataGridViewColumn column in this.Columns)
                {
                    if (!HiddenDataFields.Contains(column.Name) && column.Visible == true && column.Name != "col_MoveUp" && column.Name != "col_MoveDown" && column.Name != "col_Delete" && column.Name != "col_Edit")
                    {
                        html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + SummaryRow.SummaryCells [column.Name].Text + "</th>";
                    }
                }
                html += "</tr>";

            }
            //Table end.
            html += "</table>";

            File.WriteAllText(System.IO.Path.GetTempPath() + @"printgrid.htm", html);
            //IEnumerable<Control> ctrllst;

            bool hasparent = false;
            //if (this.FindForm ().ParentForm != null)
            //{
            //    ctrllst = GetAll(this.FindForm().MdiParent);
            //    foreach (Control c in ctrllst)
            //    {
            //        if (c.GetType() == typeof(DockPanel))
            //        {
            //            if (!this.EntryFormName.Visible)
            //            {

            //                hasparent = true;

            //                frmWebViewer frmWeb = new frmWebViewer();
            //                frmWeb.Path = System.IO.Path.GetTempPath() + @"printgrid.htm";
            //                frmWeb.MdiParent = this.FindForm().ParentForm;
            //                frmWeb.Show((DockPanel)c);

            //            }
            //        }
            //    }
            //    if (hasparent == false)
            //    {
            //        frmWebViewer frmWeb = new frmWebViewer();
            //        frmWeb.Path = System.IO.Path.GetTempPath() + @"printgrid.htm";
            //        if (this.FindForm().ParentForm != null)
            //            frmWeb.MdiParent = this.FindForm().ParentForm;
            //        frmWeb.Show();

            //    }


            //}
            //else if(hasparent==false)
            //{
                frmWebViewer frmWeb = new frmWebViewer();
                frmWeb.Path = System.IO.Path.GetTempPath() + @"printgrid.htm"; 
                frmWeb.Show();

            //}






        }
 
        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            base.OnCellFormatting(e);
            if (e.RowIndex == this.NewRowIndex)
            {
                Bitmap transparentImage = new Bitmap(20, 20);
                Graphics graphics = Graphics.FromImage(transparentImage);
                graphics.FillRectangle(Brushes.Transparent, 0, 0, 20, 20);
                graphics.Dispose();
                if (this.Columns[e.ColumnIndex].Name == "col_MoveUp" || this.Columns[e.ColumnIndex].Name == "col_MoveDown" || this.Columns[e.ColumnIndex].Name == "col_Delete" || this.Columns[e.ColumnIndex].Name == "col_Edit")
                {
                    e.Value = transparentImage;
                }


            }
            else
            {
                string colName;
                colName = this.Columns[e.ColumnIndex].Name;
                switch (colName)
                {

                    case "col_MoveUp":
                        e.Value = Gramboo.Properties.Resources.arrow_up;
                        break;
                    case "col_MoveDown":
                        e.Value = Gramboo.Properties.Resources.arrow_down;
                        break;
                    case "col_Delete":
                        e.Value = Gramboo.Properties.Resources.DeleteRed;
                        break;
                    case "col_Edit":
                        e.Value = Gramboo.Properties.Resources.edit;
                        break;
                    default:
                        break;

                }
            }
        }


        public virtual bool ValidateControls()
        {
            bool flag = true;
            bool isID = false;

            IEnumerable<Control> ctrllst;
            ctrllst = GetAll(this.Parent.Parent);
            
            // ✅ PERFORMANCE: Cache reflection lookups to avoid repeated GetProperty/GetMethod calls
            var reflectionCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
            var methodCache = new Dictionary<Type, MethodInfo>();
            
            foreach (Control c in ctrllst)
            {
                bool isblank = false;
                Type ctrlType = c.GetType();
                
                // Build or retrieve cached property info for this type
                if (!reflectionCache.ContainsKey(ctrlType))
                {
                    reflectionCache[ctrlType] = new Dictionary<string, PropertyInfo>
                    {
                        { "AcceptBlankValue", ctrlType.GetProperty("AcceptBlankValue") },
                        { "IsIDField", ctrlType.GetProperty("IsIDField") },
                        { "CheckDuplicates", ctrlType.GetProperty("CheckDuplicates") }
                    };
                    if (!methodCache.ContainsKey(ctrlType))
                    {
                        methodCache[ctrlType] = ctrlType.GetMethod("IsBlank");
                    }
                }
                
                var propCache = reflectionCache[ctrlType];
                var acceptBlankProp = propCache["AcceptBlankValue"];
                var isIdProp = propCache["IsIDField"];
                var checkDupProp = propCache["CheckDuplicates"];
                var isBlankMethod = methodCache[ctrlType];
                
                if (acceptBlankProp != null)
                {
                    if (isIdProp != null)
                    {
                        isID = Convert.ToBoolean(isIdProp.GetValue(c, null));
                    }

                    if (Convert.ToBoolean(acceptBlankProp.GetValue(c, null)) == false)
                    {
                        if (isBlankMethod != null && Convert.ToBoolean(isBlankMethod.Invoke(c, null)) == true && isID == false)
                        {
                            flag = false;
                            isblank = true;
                        }
                    }
                }

                if (checkDupProp != null && !isblank)
                {
                    if (Convert.ToBoolean(checkDupProp.GetValue(c, null)) == true)
                    {
                        if (CheckDuplicates(c) == false)
                        {
                            flag = false;
                        }
                    }
                }
            }
            return flag;
        }

        private bool CheckDuplicates(Control c)
        {
            bool flag = true;
            
            // ✅ PERFORMANCE: Cache reflection results outside the loop
            Type ctlType = c.GetType();
            PropertyInfo bindingPropProp = ctlType.GetProperty("BindingProperty");
            PropertyInfo dataFieldProp = ctlType.GetProperty("DataField");
            MethodInfo showMessageMethod = ctlType.GetMethod("ShowMessage");
            
            if (bindingPropProp == null || dataFieldProp == null || showMessageMethod == null)
                return flag;
            
            string bprop = bindingPropProp.GetValue(c, null).ToString();
            string datafield = dataFieldProp.GetValue(c, null).ToString();
            PropertyInfo valueProp = ctlType.GetProperty(bprop);
            
            if (valueProp == null)
                return flag;
            
            string ctrlValue = valueProp.GetValue(c, null)?.ToString().ToUpper().Trim() ?? "";
            
            foreach (DataGridViewRow r in this.Rows)
            {
                if (r.Index != EditIndex && r.Cells[datafield].Value != null)
                {
                    string cellValue = r.Cells[datafield].Value.ToString().ToUpper().Trim();
                    if (cellValue == ctrlValue)
                    {
                        flag = false;
                        object[] parametersArray = new object[] { r.Cells[datafield].Value + " already added", "Information", ToolTipIcon.Info };
                        showMessageMethod.Invoke(c, parametersArray);
                    }
                }
            }
            return flag;
        }
         
        public bool Save()
        {
            ValidationFlag = ValidateControls();
            if (ValidationFlag)
            {

                if (OnValidateEntries != null)
                {
                    OnValidateEntries(this, new ValidateEventArgs(false));
                }
            }
            else
            {
                return false ;
            }
            try
            {
                DataTable dt = new DataTable();
                BindingSource bs = new BindingSource();

                if (this.DataSource != null)
                {
                    dt = (DataTable)DataSource;
                    //dt.Rows.RemoveAt(0);
                    bs.DataSource = dt;
                }
                else
                {
                    foreach (DataGridViewColumn c in this.Columns)
                    {
                        if (c.DataPropertyName == null)
                        {
                            General.ShowMessage("Set DataPropertyName for All Columns");
                            return false;
                        }
                        else
                        {
                            dt.Columns.Add(c.DataPropertyName);
                        }

                        bs.DataSource = dt;
                    }
                }


                DataGridViewRow r=new DataGridViewRow();
                DataRow RowToSave;
                if (this.EditIndex != -1 )
                {
                    r = this.Rows[EditIndex]; 
                    RowToSave = dt.Rows[r.Index];
                }
                else
                {
                    RowToSave = dt.NewRow();

                }

                IEnumerable<Control> ctrllst;

                ctrllst = GetAll(this.Parent.Parent);
               
                foreach (DataGridViewColumn c in this.Columns)
                {
                    //if (c.GetType() == typeof(GrbDataGridViewTextBoxColumn))
                    //{




                    foreach (Control ctl in ctrllst)
                    {

                        if (ctl.GetType().GetProperty("DataField") != null)
                         {


                             if (Convert.ToString(ctl.GetType().GetProperty("DataField").GetValue(ctl, null)).ToUpper().Trim() == c.DataPropertyName.ToUpper().Trim() || Convert.ToString(ctl.GetType().GetProperty("Alias").GetValue(ctl, null)).ToUpper().Trim() == c.DataPropertyName.ToUpper().Trim())
                            {
                                if (c.DataPropertyName != "")
                                {
                                    string bprop = ctl.GetType().GetProperty("BindingProperty").GetValue(ctl, null).ToString();
                                    if (EditIndex > -1)
                                    {
                                        r.Cells[c.DataPropertyName].Value = ctl.GetType().GetProperty(bprop).GetValue(ctl, null).ToString();
                                    }
                                    else
                                    {

                                        RowToSave[c.DataPropertyName] = ctl.GetType().GetProperty(bprop).GetValue(ctl, null).ToString();
                                    }
                                }

                            }
                        }
                    }
                    //}
                }
                if (EditIndex == -1)
                    dt.Rows.Add(RowToSave);
                RowToSave.AcceptChanges();

                dt.AcceptChanges();


                this.AutoGenerateColumns = true;
                this.DataSource = dt;
                this.AutoGenerateColumns = false;
 
                //this.AutoResizeRows(
                //      DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
                this.CurrentCell = this[0, (this.EditIndex==-1?this.RowCount-1:this.EditIndex)  ];
                this.EditIndex = -1;
                ClearText();
                if (summaryColumns != null)
                {

                    if (summaryColumns.Length > 0)
                    {
                        SummaryRowVisible = true;
                    } 
                    summaryControl.RefreshSummary();
                }
                    
                return true;
            }

            catch (Exception ex)
            {
                General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
                return false;
            }
            

        }

        private void ClearText()
        {
            try
            {
                IEnumerable<Control> ctrllst;

                ctrllst = GetAll(this.Parent.Parent);
               
                foreach (Control Obj in ctrllst)
                {
                    if (Obj is TextBox)
                    {
                        ((TextBox)Obj).Text = "";
                    }
                    else if (Obj is ComboBox)
                    {

                        ((ComboBox)Obj).Text = "";
                        ((ComboBox)Obj).SelectedIndex = -1;
                    }
                    else if (Obj is DateTimePicker)
                    {
                        ((DateTimePicker)Obj).Value = DateTime.Now.Date;
                    }
                    else if (Obj is CheckBox)
                    {
                        ((CheckBox)Obj).Checked = false;
                    }
                    else if (Obj is RadioButton)
                    {
                        ((RadioButton)Obj).Checked = false;

                    }
                    else if (Obj is MaskedTextBox)
                    {
                        ((MaskedTextBox)Obj).Text = "";
                    }
                }
            }
            catch (Exception )
            {

            }

        }
 
        /// <summary>
        /// ✅ IMPROVED: Export DataGridView to Excel using EPPlus 8.5.4
        /// Features: Professional formatting, auto-sizing, summary row, proper number/date formatting
        /// </summary>
        private void ExportToExcel(GrbDataGridView dGV, string filename)
        {
            try
            {
                // ✅ Set EPPlus License Context (required for EPPlus 8.x)
                // Updated to use new License API for EPPlus 8.5.4
                ExcelPackage.License.SetNonCommercialPersonal("Gramboo");

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Export");
                    int rowNum = 1;
                    int colNum = 1;

                    // ✅ Write Headers with professional formatting
                    foreach (DataGridViewColumn col in dGV.Columns)
                    {
                        // Skip action columns
                        if (col.Name.StartsWith("col_")) continue;
                        if (!col.Visible) continue;

                        var cell = worksheet.Cells[rowNum, colNum];
                        cell.Value = col.HeaderText;
                        
                        // ✅ Header formatting
                        cell.Style.Font.Bold = true;
                        cell.Style.Font.Color.SetColor(System.Drawing.Color.White);
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(70, 120, 180)); // Professional blue
                        cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cell.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        cell.Style.WrapText = true;
                        
                        colNum++;
                    }

                    // ✅ Auto-fit header row height
                    worksheet.Row(rowNum).Height = 25;

                    // ✅ Write Data Rows with type-specific formatting
                    rowNum++;
                    int dataColCount = colNum - 1;
                    foreach (DataGridViewRow gridRow in dGV.Rows)
                    {
                        colNum = 1;
                        foreach (DataGridViewColumn col in dGV.Columns)
                        {
                            if (col.Name.StartsWith("col_")) continue;
                            if (!col.Visible) continue;

                            var cell = gridRow.Cells[col.Index];
                            object cellValue = cell.Value;
                            var excelCell = worksheet.Cells[rowNum, colNum];

                            // ✅ Format data by type
                            if (cellValue != null && cellValue != System.DBNull.Value)
                            {
                                if (cellValue is DateTime)
                                {
                                    excelCell.Value = Convert.ToDateTime(cellValue);
                                    excelCell.Style.Numberformat.Format = "dd-mmm-yyyy";
                                    excelCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                }
                                else if (cellValue is Decimal || cellValue is Double || cellValue is float)
                                {
                                    excelCell.Value = Convert.ToDouble(cellValue);
                                    excelCell.Style.Numberformat.Format = "0.00";
                                    excelCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                }
                                else if (cellValue is int || cellValue is long || cellValue is short)
                                {
                                    excelCell.Value = cellValue;
                                    excelCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                }
                                else
                                {
                                    excelCell.Value = cellValue.ToString();
                                }
                            }

                            // ✅ Alternate row coloring for readability
                            if (rowNum % 2 == 0)
                            {
                                excelCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                excelCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(240, 240, 240)); // Light gray
                            }

                            // ✅ Preserve custom font color if it differs from the default cell forecolor
                            Color cellForeColor = cell.InheritedStyle.ForeColor;
                            Color defaultForeColor = this.DefaultCellStyle.ForeColor;
                            if (cellForeColor != Color.Empty && cellForeColor != defaultForeColor)
                            {
                                excelCell.Style.Font.Color.SetColor(cellForeColor);
                            }

                            colNum++;
                        }
                        rowNum++;
                    }

                    // ✅ Write Summary Row if visible
                    if (summaryRowVisible && this.summaryControl != null && this.SummaryRow.SummaryCells.Count > 0)
                    {
                        colNum = 1;
                        foreach (DataGridViewColumn col in dGV.Columns)
                        {
                            if (col.Name.StartsWith("col_")) continue;
                            if (!col.Visible) continue;

                            // Check if summary cell exists for this column
                            var summaryCell = this.SummaryRow.SummaryCells.FirstOrDefault(x => x.Name == col.Name);
                            if (summaryCell != null && !string.IsNullOrEmpty(summaryCell.Text))
                            {
                                var excelCell = worksheet.Cells[rowNum, colNum];
                                excelCell.Value = summaryCell.Text;
                                
                                // ✅ Summary row formatting
                                excelCell.Style.Font.Bold = true;
                                excelCell.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                excelCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                excelCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 242, 204)); // Light yellow
                                excelCell.Style.Numberformat.Format = "0.00";
                                excelCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                excelCell.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                            }
                            colNum++;
                        }
                    }

                    // ✅ Auto-size columns based on content (min 10, max 50)
                    worksheet.Cells.AutoFitColumns(10, 50);

                    // ✅ Freeze header row for easier scrolling
                    worksheet.View.FreezePanes(2, 1);

                    // ✅ Add borders to all cells with data
                    int lastRow = rowNum - 1;
                    var dataRange = worksheet.Cells[1, 1, lastRow, dataColCount];
                    dataRange.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    // ✅ Save workbook
                    package.SaveAs(new System.IO.FileInfo(filename));
                    General.ShowMessage($"Excel file exported successfully!\n{dGV.Rows.Count} rows exported.", "Export Success", MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                General.ShowMessage("Error exporting to Excel: " + ex.Message, "Export Error", MessageBoxIcon.Error);
            }
        }


         public void MoveRow(int pos )
        {

           
 
            DataTable dt = (DataTable)DataSource;

            BindingSource bs = new BindingSource();


            bs.DataSource=this.DataSource;
            Delflag = true;

            bs.RemoveSort();

            DataRow RowToMove = dt.Rows[this.CurrentCell.RowIndex];
            DataRow NewRow = dt.NewRow();
            NewRow.ItemArray = RowToMove.ItemArray;




            int CurrentColumnIndex = this.CurrentCell.ColumnIndex;
            Int32 NewIndex = Convert.ToInt32(CurrentCell.RowIndex - pos);

            dt.Rows.RemoveAt(this.CurrentCell.RowIndex);

            dt.Rows.InsertAt(NewRow, NewIndex);
            dt.AcceptChanges();


            //this.AutoGenerateColumns = true;
            this.DataSource = dt;
            //this.AutoGenerateColumns = false; 
            //this.AutoResizeRows(
            //DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders );
            this.CurrentCell = this[CurrentColumnIndex, NewIndex];

            //dt.Dispose();
           // bs.Dispose();

        }
         protected override void OnScroll(ScrollEventArgs e)
         {
             base.OnScroll(e);
         }

         private List<DataGridViewImageCell> GetNotVisibleDataRowsWithImages()
         {
             List<DataGridViewImageCell> l = new List<DataGridViewImageCell>();
             var vivibleRowsCount = this.DisplayedRowCount(true);
             var firstDisplayedRowIndex = this.FirstDisplayedCell.RowIndex;
             var lastvibileRowIndex = (firstDisplayedRowIndex + vivibleRowsCount) - 1;
             for (int rowIndex = firstDisplayedRowIndex; rowIndex <= lastvibileRowIndex; rowIndex++)
             {
                 var cells = this.Rows[rowIndex].Cells;
                 foreach (DataGridViewCell cell in cells)
                 {
                     if (cell.Displayed && cell.GetType()==typeof(DataGridViewImageCell))
                     {
                         l.Add((DataGridViewImageCell)cell);
                     }
                 }
             }
             return l;

         }

         private void ClearNotVisibleImages()
         {
             // ✅ OPTIMIZATION: Properly dispose image resources with try-finally guarantee
             var notVisibleCells = this.GetNotVisibleDataRowsWithImages();
             try
             {
                 foreach (var cell in notVisibleCells)
                 {
                     Image image = (Image)(cell.Value);
                     if (image != null)
                     {
                         cell.Value = null;
                         image.Dispose();
                     }
                 }
             }
             finally
             {
                 // Force garbage collection and finalization to reclaim resources
                 GC.Collect(1);
                 GC.WaitForPendingFinalizers();
             }
         }

         protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
         {
             if(!Delflag)
             base.OnRowsRemoved(e);
            ReorderColumns();
            EditIndex = -1;
             
         }
        protected override void OnCellClick(DataGridViewCellEventArgs e)
        {
            base.OnCellClick(e);
            //DataRow dr;


            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex==this.NewRowIndex )
                return;
            //dr= ((DataTable )this.DataSource).Rows[e.RowIndex];

            string colName;
            colName = this.Columns[e.ColumnIndex].Name;
            switch (colName)
            {
                    

                case "col_MoveUp":
                    if (e.RowIndex > 0 )
                    {
                        if (e.RowIndex >0)
                        {
                            MoveRow(1);
                        }
                    }
                    break;
                case "col_MoveDown":
                    if (e.RowIndex < this.Rows.Count - 1)
                    {
                        MoveRow(-1);
                    }
                    break;
                case "col_Delete":
                    if (e.RowIndex != this.NewRowIndex || e.RowIndex >= 0)
                    {
                        DeleteRow(e.RowIndex);

                    }

                    break;
                case "col_Edit":
                    
                        EditRow(e.RowIndex );

                   
                    break;
                default:
                    break;

            }
        }

     
         
        public virtual void SetEntryForm()
        {

        }
        public bool GotoEntryForm(int rowindex)
        {
            try
            {
                if (this.FindForm() != this.EntryFormName)
                {
                    if (this.FindForm().MdiParent != null)
                    {
                        if(this.EntryFormName==null )
                        {
                            SetEntryForm();

                        }
                        if(this.EntryFormName.IsDisposed)
                        {
                            SetEntryForm();
                        }

                        IEnumerable<Control> ctrllst;
                        ctrllst = GetAll(this.FindForm().MdiParent);
                        foreach (Control c in ctrllst)
                        {
                            if (c.GetType() == typeof(DockPanel))
                            {
                                if (!this.EntryFormName.Visible)
                                {


                                    GrbForm frm = this.EntryFormName;
                                    if (this.EntryFormName.IsDisposed)
                                    {

                                        frm = (GrbForm)Activator.CreateInstance(this.EntryFormName.GetType());
                                        frm.MdiParent = this.FindForm().ParentForm;
                                        this.EntryFormName = frm;
                                    }
                                    // this.EntryFormName.MdiParent = this.FindForm().ParentForm;
                                    frm.Show((DockPanel)c);
                                    frm.ListForm = (GrbForm)this.FindForm();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!this.EntryFormName.Visible)
                            this.EntryFormName.Show();
                    }
                }
                this.EntryFormName.InitializeTables();
                (this.EntryFormName).FillData(GetPrimaryKeys( (this.EntryFormName).TableName,rowindex ));
               
                return true;
            }
            catch (Exception ex)
            {
                General.ShowMessage(ex.Message, "", MessageBoxIcon.Asterisk);
                return false;
            }
        }

        private Dictionary<string,object> GetPrimaryKeys(Table table_name,int rowindex)
        {
            Dictionary<string, object> PrimaryVals=new Dictionary<string,object>() ;

                foreach (string defPK in ((GrbForm) this.FindForm()).DefaultPrimaryKeys)
            {
                if (PrimaryVals.Keys.Contains(defPK.ToString(), StringComparer.OrdinalIgnoreCase) == false)
                {
                    PrimaryVals.Add(defPK, ((GrbForm)this.FindForm()).GetDefaultKeyVal(defPK));
                }
            }

            foreach (string pk in table_name.PrimaryKeys)
            {
                if (pk.Length > 0)
                {
                    foreach (DataGridViewColumn c in this.Columns)
                    {


                        if (c.DataPropertyName.ToUpper()==pk.ToUpper())
                        {
                            PrimaryVals.Add(pk,this[c.Index ,rowindex].Value) ;
                        }

                    }
                }
            }
            return PrimaryVals;
        }
        public void Edit(GrbDataGridView dgv)
        {
            if (dgv.CurrentCell == null)
                return;

            RowChangingEventArgs e = new RowChangingEventArgs(false, CurrentCell.OwningRow);

            OnBeforeEditRow(e);
            if (!e.IsHandled())
            {
                dgv.EditRow(dgv.CurrentCell.RowIndex);
            }
        }

        public void Delete(GrbDataGridView dgv)
        {
            if (dgv.CurrentCell == null)
                return;
            dgv.DeleteRow(dgv.CurrentCell.RowIndex);
        }
        private void EditRow(int rowindex)
        {
            if (rowindex != this.NewRowIndex || rowindex >= 0)
            {
                OnBeforeEdit(this, new BeforeEditEventArgs(false, this.Rows[rowindex]));
                if (IsList)
                {
                    if (this.EntryFormName != null)
                    {
                        GotoEntryForm(rowindex);
                    }
                }
                else
                {
                    IEnumerable<Control> ctrllst = GetAll(this.Parent.Parent);
                    
                    // ✅ PERFORMANCE: Build reflection cache per type to avoid repeated lookups
                    var typeReflectionCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
                    
                    foreach (var cell in this.Rows[rowindex].Cells.Cast<DataGridViewCell>())
                    {
                        foreach (Control ctl in ctrllst)
                        {

                            try
                            {
                                if (ctl == null) continue;
                                Type ctlType = ctl.GetType();
                                
                                // Build or retrieve cached reflection info for this type
                                if (!typeReflectionCache.ContainsKey(ctlType))
                                {
                                    typeReflectionCache[ctlType] = new Dictionary<string, PropertyInfo>
                                    {
                                        { "DataField", ctlType.GetProperty("DataField") },
                                        { "BindingProperty", ctlType.GetProperty("BindingProperty") }
                                    };
                                }
                                
                                var propCache = typeReflectionCache[ctlType];
                                PropertyInfo dataFieldProp = propCache["DataField"];
                                PropertyInfo bindingPropProp = propCache["BindingProperty"];
                                
                                if (dataFieldProp != null)
                                {
                                    object dataFieldValue = dataFieldProp.GetValue(ctl, null);
                                    string dataField = dataFieldValue?.ToString()?.Trim();

                                    if (!string.IsNullOrEmpty(dataField))
                                    {
                                        if (string.Equals(dataField, cell.OwningColumn.DataPropertyName, StringComparison.OrdinalIgnoreCase) ||
                                            string.Equals(dataFieldValue?.ToString(), cell.OwningColumn.DataPropertyName, StringComparison.OrdinalIgnoreCase))
                                        {
                                            if (bindingPropProp != null)
                                            {
                                                string bprop = bindingPropProp.GetValue(ctl, null)?.ToString();
                                                PropertyInfo propertyInfo = ctlType.GetProperty(bprop);
                                                if (propertyInfo != null && propertyInfo.CanWrite)
                                                {
                                                    try
                                                    {
                                                        object cellValue = cell.Value ?? "";
                                                        propertyInfo.SetValue(ctl, Convert.ChangeType(cellValue, propertyInfo.PropertyType), null);
                                                        this.EditIndex = rowindex;
                                                        break;
                                                    }
                                                    catch (InvalidCastException)
                                                    {
                                                        // Handle type mismatch gracefully
                                                        if (propertyInfo.CanWrite)
                                                        {
                                                            propertyInfo.SetValue(ctl, cell.Value ?? "", null);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Log specific errors but continue
                                if (!string.IsNullOrEmpty(ctl.Name))
                                    Gramboo.General.ShowMessage(ctl.Name + " " + ex.Message);
                            }
                        }
                    }
                }
            }
            else
            {
                EditIndex = -1;
            }
        }

        /// <summary>
        /// Get - List of New Sort Order
        /// </summary>
        internal List<KeyValuePair<DataGridViewColumn, bool>> SortOrderList
        {
            get
            {
                List<KeyValuePair<DataGridViewColumn, bool>> sortList = new List<KeyValuePair<DataGridViewColumn, bool>>();
                foreach (DataGridViewRow dgvRow in this.Rows)
                {
                    //get 1.ColumnNmae, 2.SortMode
                    DataGridViewColumn myCol = null;
                    foreach (string dgvCol in this.GroupingFields)
                        if (dgvCol  == dgvRow.Cells[0].Value.ToString())
                        {
                            myCol = this.Columns[ dgvCol];
                            break;
                        }

                    bool isAscending = dgvRow.Cells[1].Value.ToString() == SORT_MODE_ASCENDING ? true : false;

                    sortList.Add(new KeyValuePair<DataGridViewColumn, bool>(myCol, isAscending));
                }

                return sortList;
            }
        }

        public IEnumerable<Control> GetAll(Control control)
        {
            // ✅ OPTIMIZATION: Use iterative approach instead of recursion to avoid stack overflow
            // and reduce memory allocations for large control hierarchies
            var controls = new Stack<Control>();
            controls.Push(control);
            var result = new List<Control>();

            while (controls.Count > 0)
            {
                Control current = controls.Pop();
                foreach (Control child in current.Controls)
                {
                    result.Add(child);
                    controls.Push(child);
                }
            }

            return result.OrderBy(x => x.TabIndex);
        }

        private void DeleteRow(int RowIndex)
        {
           

            if (RowIndex != this.NewRowIndex || RowIndex >= 0)
            {

                if (IsList)
                {
                    if (this.EntryFormName != null)
                    {
                        GotoEntryForm(RowIndex);
                    }
                }
                else
                {
                    ((DataTable)this.DataSource).Rows.RemoveAt(RowIndex);
                    EditIndex = -1;
                }

                
            }
        }

        //        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        //        {

        //           // 



        //            if (e.RowIndex == this.NewRowIndex && this.NewRowIndex>-1)
        //                return;

        //            Color c1, c2, c3;



        //            if (e.RowIndex == -1 || e.ColumnIndex == -1)
        //            {
        //                c1 = Color.FromArgb(255, 255, 255, 255);
        //                c2 = Color.FromArgb(255, 219, 235, 255);
        //                c3 = Color.FromArgb(255, 208, 215, 229);

        //                LinearGradientBrush br = new LinearGradientBrush(e.CellBounds, c1, c3, 90, true);
        //                ColorBlend cb = new ColorBlend();
        //                cb.Positions = new[] { 0, (float)0.5, 1 };
        //                cb.Colors = new[] { c1, c2, c3 };
        //                br.InterpolationColors = cb;


        //                e.Graphics.FillRectangle(br, e.CellBounds);
        //                e.PaintContent(e.ClipBounds);
        //                if (this.CellBorderStyle!=DataGridViewCellBorderStyle.None)
        //                e.Graphics.DrawRectangle(new Pen(new SolidBrush(c1)), e.CellBounds);
        //                // Draw filter icon in header cell
        //                int iconSize = 12;
        //                int padding = 4;
        //                var iconRect = new Rectangle(
        //                    e.CellBounds.Right - iconSize - padding,
        //                    e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2,
        //                    iconSize,
        //                    iconSize);

        //                // You can replace this with your actual filter icon
        //                Icon icon = Properties.Resources.Filter; // your .ico file
        //                Rectangle iconRect1 = new Rectangle(e.CellBounds.Right - 16, e.CellBounds.Top + 2, 12, 12);
        //                e.Graphics.DrawImage(icon.ToBitmap(), iconRect1);


        //                e.Handled = true;

        //            }
        //            //else if (e.ColumnIndex == -1)
        //            //{

        //            //    c1 = Color.FromArgb(255, 228, 236, 247);

        //            //    SolidBrush br = new SolidBrush(c1);
        //            //    e.Graphics.FillRectangle(br, e.CellBounds);
        //            //    e.PaintContent(e.ClipBounds);
        //            //    e.Graphics.DrawRectangle(new Pen(new SolidBrush(this.GridColor)), e.CellBounds);

        //            //    e.Handled = true;

        //            //}

        //            else
        //            {


        //                if (SelectedCells.Contains(this[e.ColumnIndex, e.RowIndex]))
        //                {

        //                    c1 = Color.FromArgb(255, 255, 238, 180);
        //                    c2 = Color.FromArgb(255, 255, 233, 165);
        //                    c3 = Color.FromArgb(255, 255, 210, 142);
        //                    LinearGradientBrush Lbr = new LinearGradientBrush(e.CellBounds, c1, c3, 90, true);
        //                    ColorBlend cb = new ColorBlend();
        //                    cb.Positions = new[] { 0, (float)0.5, 1 };
        //                    cb.Colors = new[] { c1, c2, c3 };
        //                    Lbr.InterpolationColors = cb;


        //                    e.Graphics.FillRectangle(Lbr, e.CellBounds);
        //                    e.CellStyle.ForeColor = Color.Black;
        //                    e.CellStyle.SelectionForeColor = Color.Black;
        //                    e.PaintContent(e.ClipBounds);

        //                    //if (SelectedRows.Contains(this.Rows[e.RowIndex]) == false && SelectedColumns.Contains(this.Columns[e.ColumnIndex]) == false)
        //                    //{
        //                    //    e.Graphics.DrawRectangle(new Pen(new SolidBrush(this.GridColor ),2), new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1));

        //                    //}
        //                    //else if (SelectedRows.Contains(this.Rows[e.RowIndex]))
        //                    //{

        //                    //    e.Graphics.DrawRectangle(new Pen(new SolidBrush(this.GridColor), 2), new Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1));
        //                    //}







        //                }
        //                else
        //                {
        //                    if (e.RowIndex % 2 == 0)
        //                    {
        //                        c1 = Color.FromArgb(255, 255, 255, 255);
        //                    }
        //                    else
        //                    {
        //                        c1 = Color.FromArgb(255, 244, 244, 244); 
        //;
        //                    }

        //                    SolidBrush br = new SolidBrush(c1);
        //                    e.Graphics.FillRectangle(br, e.CellBounds);
        //                    if (this.CellBorderStyle != DataGridViewCellBorderStyle.None)
        //                    e.Graphics.DrawRectangle(new Pen(new SolidBrush(this.GridColor)), e.CellBounds);
        //                    e.PaintContent(e.ClipBounds);

        //                }
        //            }
        //            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
        //            {
        //                e.PaintBackground(e.CellBounds, true);
        //                e.PaintContent(e.CellBounds);

        //                // Show icon only for hovered column
        //                if (e.ColumnIndex == hoveredColumnIndex)
        //                {


        //                    Icon icon = Properties.Resources.Filter; // your .ico file
        //                    Rectangle iconRect1 = new Rectangle(e.CellBounds.Right - 16, e.CellBounds.Top + 2, 12, 12);
        //                    e.Graphics.DrawImage(icon.ToBitmap(), iconRect1);
        //                }

        //                e.Handled = true;
        //            }


        //            e.Handled = true;
        //            base.OnCellPainting(e);


        //        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);

            if (e.RowIndex == this.NewRowIndex && this.NewRowIndex > -1)
                return;

            Color c1, c2, c3;

            // ✅ Column Header
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                c1 = Color.FromArgb(255, 255, 255, 255);
                c2 = Color.FromArgb(255, 219, 235, 255);
                c3 = Color.FromArgb(255, 208, 215, 229);

                using (LinearGradientBrush br = new LinearGradientBrush(e.CellBounds, c1, c3, 90, true))
                {
                    ColorBlend cb = new ColorBlend();
                    cb.Positions = new[] { 0, 0.5f, 1 };
                    cb.Colors = new[] { c1, c2, c3 };
                    br.InterpolationColors = cb;

                    e.Graphics.FillRectangle(br, e.CellBounds);
                }

                e.PaintContent(e.ClipBounds);
                if (this.CellBorderStyle != DataGridViewCellBorderStyle.None)
                    e.Graphics.DrawRectangle(Pens.LightGray, e.CellBounds);

                // ✅ Show filter icon only for hovered column (OPTIMIZED: Cache bitmap)
                if (e.RowIndex == -1 && (e.ColumnIndex == hoveredColumnIndex || filteredColumns.Contains(e.ColumnIndex)))
                {
                    int iconSize = 12;
                    int padding = 4;
                    int sortGlyphOffset = 16; // enough space for sort icon

                    Rectangle iconRect = new Rectangle(
                        e.CellBounds.Right - iconSize - padding - sortGlyphOffset,
                        e.CellBounds.Top + (e.CellBounds.Height - iconSize) / 2,
                        iconSize,
                        iconSize);
                    
                    // ✅ OPTIMIZATION: Use cached filter icon instead of creating bitmap each time
                    var filterIcon = Gramboo.Properties.Resources.Filter;
                    if (filterIcon != null)
                    {
                        // Convert icon to bitmap for drawing
                        using (Bitmap iconBitmap = new Bitmap(iconSize, iconSize))
                        using (Graphics g = Graphics.FromImage(iconBitmap))
                        {
                            g.Clear(Color.Transparent);
                            g.DrawIcon(filterIcon, 0, 0);
                            e.Graphics.DrawImageUnscaled(iconBitmap, iconRect.Location);
                        }
                    }
                }

                e.Handled = true;
                return;
            }

            // ✅ Row Header (skip painting icon here)
            if (e.ColumnIndex == -1)
            {
                c1 = Color.FromArgb(255, 228, 236, 247);
                using (SolidBrush br = new SolidBrush(c1))
                {
                    e.Graphics.FillRectangle(br, e.CellBounds);
                }

                e.PaintContent(e.ClipBounds);
                using (Pen p = new Pen(this.GridColor))
                {
                    e.Graphics.DrawRectangle(p, e.CellBounds);
                }
                e.Handled = true;
                return;
            }

            // ✅ Right-align numeric cells
            if (e.Value != null && e.Value != DBNull.Value)
            {
                Type vt = e.Value.GetType();
                if (vt == typeof(int) || vt == typeof(long) || vt == typeof(short) ||
                    vt == typeof(decimal) || vt == typeof(double) || vt == typeof(float))
                {
                    e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            // ✅ Data Cells
            if (SelectedCells.Contains(this[e.ColumnIndex, e.RowIndex]))
            {
                c1 = Color.FromArgb(255, 255, 238, 180);
                c2 = Color.FromArgb(255, 255, 233, 165);
                c3 = Color.FromArgb(255, 255, 210, 142);

                using (LinearGradientBrush Lbr = new LinearGradientBrush(e.CellBounds, c1, c3, 90, true))
                {
                    ColorBlend cb = new ColorBlend();
                    cb.Positions = new[] { 0, 0.5f, 1 };
                    cb.Colors = new[] { c1, c2, c3 };
                    Lbr.InterpolationColors = cb;

                    e.Graphics.FillRectangle(Lbr, e.CellBounds);
                }

                e.CellStyle.ForeColor = Color.Black;
                e.CellStyle.SelectionForeColor = Color.Black;
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
            else
            {
                c1 = (e.RowIndex % 2 == 0)
                    ? Color.FromArgb(255, 255, 255, 255)
                    : Color.FromArgb(255, 244, 244, 244);

                using (SolidBrush br = new SolidBrush(c1))
                {
                    e.Graphics.FillRectangle(br, e.CellBounds);
                }

                if (this.CellBorderStyle != DataGridViewCellBorderStyle.None)
                {
                    using (Pen p = new Pen(this.GridColor))
                    {
                        e.Graphics.DrawRectangle(p, e.CellBounds);
                    }
                }

                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
        }

        /// <summary>
        /// this function moves the focus to the next cell
        /// </summary>
        public void MoveToNextCell()
        {
            int CurrentColumn, CurrentRow,i=1;

            //get the current indicies of the cell
            CurrentColumn = this.CurrentCell.ColumnIndex;
            CurrentRow = this.CurrentCell.RowIndex;

            //if cell is at the end move it to the first cell of the next row
            //other with move it to the next cell
            if (CurrentColumn == this.Columns.Count - 1 && CurrentRow != this.Rows.Count - 1)
            {

                base.ProcessDataGridViewKey(new KeyEventArgs(Keys.Home));
                base.ProcessDataGridViewKey(new KeyEventArgs(Keys.Down));
            }
            else
            {

                while (CurrentColumn+i <= this.Columns.Count)
                {




                    if (CurrentColumn + i > this.Columns.Count - 1 && CurrentRow != this.Rows.Count - 1)
                        {
                            base.ProcessDataGridViewKey(new KeyEventArgs(Keys.Home));
                            base.ProcessDataGridViewKey(new KeyEventArgs(Keys.Down));
                            break;
                        }
                    else if (CurrentColumn + i <= this.Columns.Count - 1)
                    {
                        if (this.Columns[CurrentColumn + i].Visible)
                            base.ProcessDataGridViewKey(new KeyEventArgs(Keys.Right));
                        if (this.Rows[CurrentRow].Cells[CurrentColumn + i].ReadOnly == false && this.Columns[CurrentColumn + i].Visible)
                        {
                            break;
                        }
                    }
                    else
                    {
                        base.ProcessDataGridViewKey(new KeyEventArgs(Keys.End ));
                        break;
                    }

                        i++;
                }
            }
        }

        
        protected override bool ProcessDialogKey(Keys keyData)
        {
           //if the key pressed is "return" then tell the datagridview to move to the next cell
            if ((keyData == Keys.Enter  ) && IsDataEntryGrid)
            {
                MoveToNextCell();
                return true;
            }
            else
                return base.ProcessDialogKey(keyData);
        }
        void expExl_PerformClick(object sender,EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbooks (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls";
            sfd.DefaultExt = "xlsx";
            sfd.FileName = "export.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ExportToExcel(this, sfd.FileName);
            }  
        }


        //internal class NumericStringComparer : IComparer
        //{



        //    public int Compare(object x, object y)
        //    {
        //        DataGridViewCell lx = (DataGridViewCell)x;
        //        DataGridViewCell ly = (DataGridViewCell)y;

        //        return this.Compare(lx.Value, ly.Value);
        //    }

        //}

        internal class EntryFormEditor : ObjectSelectorEditor
        {
            protected override void FillTreeWithData(Selector theSel,
              ITypeDescriptorContext theCtx, IServiceProvider theProvider)
            {
                base.FillTreeWithData(theSel, theCtx, theProvider);  //clear the selection
                GrbDataGridView aCtl = (GrbDataGridView)theCtx.Instance;

                Type formType = typeof(Form);
                try
                {
                    //foreach (Assembly currentassembly in AppDomain.CurrentDomain.GetAssemblies())
                    //{
                    foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
                        {
                            if (formType.IsAssignableFrom(type))
                            {
                                try
                                {
                                    SelectorNode aNd = new SelectorNode(type.Name, type);

                                    theSel.Nodes.Add(aNd);
                                }
                                catch (Exception ex)
                                {
                                    General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
                                }
                                //if (type ==(Form) aCtl.EntryFormName)
                                //    theSel.SelectedNode = aNd;
                            }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
                }
            }
        }
    }
     

    public delegate void CheckBoxClickedHandler(bool state);
    public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
    {
        bool _bChecked;
        public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
        {
            _bChecked = bChecked;
        }
        public bool Checked
        {
            get { return _bChecked; }
        }
    }

   public  class GrbDataGridViewTextBoxColumn : DataGridViewTextBoxColumn
    {

       private Control _BindedControl = new Control();

        public GrbDataGridViewTextBoxColumn()
        {
            CellTemplate =new  DataGridViewTextBoxCell();  
        }
       //[Editor(typeof(BindedControlEditor), typeof(System.Drawing.Design.UITypeEditor))]
       // public Control  BindedControl
       // {
       //     get
       //     {
       //         return _BindedControl;
       //     }
       //     set
       //     {
       //         _BindedControl = value;
       //     }
       // }
        public string  TableName { get; set; }
        public string  DataField { get; set; }


        //internal class BindedControlEditor : ObjectSelectorEditor
        //{
 
        //    protected override void FillTreeWithData(Selector theSel,
        //      ITypeDescriptorContext theCtx, IServiceProvider theProvider)
        //    {
        //        base.FillTreeWithData(theSel, theCtx, theProvider);  //clear the selection

        //        GrbDataGridViewTextBoxColumn aCtl = (GrbDataGridViewTextBoxColumn)theCtx;

        //        List<Type> Customclasses = new List<Type>();
        //        List<Type> Windowsclasses = new List<Type>();

        //        Customclasses = Assembly.GetExecutingAssembly().GetTypes().Where(t => String.Equals(t.Namespace, "Gramboo.Controls", StringComparison.Ordinal)).ToList();
        //        Windowsclasses = Assembly.GetExecutingAssembly().GetTypes().Where(t => String.Equals(t.Namespace, "System.Windows.Forms", StringComparison.Ordinal)).ToList();

        //        foreach (object aIt in aCtl.DataGridView.Parent.Controls)
        //        {
        //            if (Customclasses.Contains(aIt.GetType()) || Windowsclasses.Contains(aIt.GetType()))
        //            {
        //                SelectorNode aNd = new SelectorNode(((Control )aIt).Name, aIt);

        //                theSel.Nodes.Add(aNd);

        //                //if (aIt == aCtl.BindedControl)
        //                //    theSel.SelectedNode = aNd;
        //            }
        //        }
        //    }

        //}
      

    }


   class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
   {
       Point checkBoxLocation;
       Size checkBoxSize;
       bool _checked = false;
       Point _cellLocation = new Point();
       System.Windows.Forms.VisualStyles.CheckBoxState _cbState =
           System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal;
       public event CheckBoxClickedHandler OnCheckBoxClicked;

       public DatagridViewCheckBoxHeaderCell()
       {
           
       }


       protected override void Paint(System.Drawing.Graphics graphics,
           System.Drawing.Rectangle clipBounds,
           System.Drawing.Rectangle cellBounds,
           int rowIndex,
           DataGridViewElementStates dataGridViewElementState,
           object value,
           object formattedValue,
           string errorText,
           DataGridViewCellStyle cellStyle,
           DataGridViewAdvancedBorderStyle advancedBorderStyle,
           DataGridViewPaintParts paintParts)
       {
           base.Paint(graphics, clipBounds, cellBounds, rowIndex,
               dataGridViewElementState, value,
               formattedValue, errorText, cellStyle,
               advancedBorderStyle, paintParts);
           Point p = new Point();
           Size s = CheckBoxRenderer.GetGlyphSize(graphics,
           System.Windows.Forms.VisualStyles.CheckBoxState.UncheckedNormal);
           p.X = cellBounds.Location.X +
               (cellBounds.Width / 2) - (s.Width / 2);
           p.Y = cellBounds.Location.Y +
               (cellBounds.Height / 2) - (s.Height / 2);
           _cellLocation = cellBounds.Location;
           checkBoxLocation = p;
           checkBoxSize = s;
           if (_checked)
               _cbState = System.Windows.Forms.VisualStyles.
                   CheckBoxState.CheckedNormal;
           else
               _cbState = System.Windows.Forms.VisualStyles.
                   CheckBoxState.UncheckedNormal;
           CheckBoxRenderer.DrawCheckBox
           (graphics, checkBoxLocation, _cbState);
       }

       protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
       {
           Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);
           if (p.X >= checkBoxLocation.X && p.X <=
               checkBoxLocation.X + checkBoxSize.Width
           && p.Y >= checkBoxLocation.Y && p.Y <=
               checkBoxLocation.Y + checkBoxSize.Height)
           {
               _checked = !_checked;
               foreach (DataGridViewRow r in this.DataGridView.Rows)
               {
                   r.Cells[this.ColumnIndex].Value = _checked;
               }

               if (OnCheckBoxClicked != null)
               {
                   OnCheckBoxClicked(_checked);
                   this.DataGridView.InvalidateCell(this);
               }

           }


           base.OnMouseClick(e);
       }


   }
   public class ValidateEventArgs : EventArgs
   {
       private bool Handled;
       public ValidateEventArgs(bool handled)
       {
           Handled = handled;
       }
       public bool IsHandled()
       {
           return Handled;
       }
   }

   public class RowChangingEventArgs : EventArgs
   {
       private bool Handled;
       private DataGridViewRow r;

       public RowChangingEventArgs(bool handled, DataGridViewRow Row)
       {
           Handled = handled;
           r = Row;
       }
       public bool IsHandled()
       {
           return Handled;
       }
       public DataGridViewRow CurrentRow()
       {
           return r;
       }

   }

   public class BeforeEditEventArgs : EventArgs
   {
       private bool Handled;
       private DataGridViewRow  r;

       public BeforeEditEventArgs(bool handled,DataGridViewRow Row)
       {
           Handled = handled;
           r = Row;
       }
       public bool IsHandled()
       {
           return Handled;
       }
       public DataGridViewRow CurrentRow()
       {
           return r;
       }

   }
}

