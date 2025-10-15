using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;


namespace Gramboo.Controls
{
  
  
    public class SummaryControlContainer : Control
    {

        #region Declare variables
        private Hashtable sumBoxHash;
        private GrbDataGridView  dgv;
        private Label sumRowHeaderLabel;
        private SummaryCellCollection _SummaryCells=new SummaryCellCollection(); 

        private int initialHeight;
        public int InitialHeight
        {
            get { return initialHeight; }
            set { initialHeight = value; }
        }

        private bool lastVisibleState;
        public bool LastVisibleState
        {
            get { return lastVisibleState; }
            set { lastVisibleState = value; }
        }        

        private Color summaryRowBackColor;       
        public Color SummaryRowBackColor
        {
            get { return summaryRowBackColor; }
            set { summaryRowBackColor = value;}
        }

        public SummaryCellCollection SummaryCells
        {
            get
            {
                return _SummaryCells;
            }
            set
            {
                _SummaryCells = value;
            }
        }

        /// <summary>
        /// Event is raised when visibility changes and the
        /// lastVisibleState is not the new visible state
        /// </summary>
        public event EventHandler VisibilityChanged;

        #endregion

        #region Constructors

        public SummaryControlContainer(GrbDataGridView dgv)
        {
            if (dgv == null)
                throw new Exception("DataGridView is null!");

            this.dgv = dgv;

            sumBoxHash = new Hashtable();
            sumRowHeaderLabel = new Label();


            //this.dgv.CreateSummary += new EventHandler(dgv_CreateSummary);
            //this.dgv.RowsAdded += new DataGridViewRowsAddedEventHandler(dgv_RowsAdded);
            //this.dgv.RowsRemoved += new DataGridViewRowsRemovedEventHandler(dgv_RowsRemoved);
            this.dgv.CellValueChanged += new DataGridViewCellEventHandler(dgv_CellValueChanged);

            this.dgv.Scroll += new ScrollEventHandler(dgv_Scroll);
            this.dgv.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgv_ColumnWidthChanged);
            this.dgv.RowHeadersWidthChanged += new EventHandler(dgv_RowHeadersWidthChanged);
            this.VisibleChanged += new EventHandler(SummaryControlContainer_VisibleChanged);

            //this.dgv.ColumnAdded += new DataGridViewColumnEventHandler(dgv_ColumnAdded);
            //this.dgv.ColumnRemoved += new DataGridViewColumnEventHandler(dgv_ColumnRemoved);
            this.dgv.ColumnStateChanged += new DataGridViewColumnStateChangedEventHandler(dgv_ColumnStateChanged);
            //this.dgv.ColumnDisplayIndexChanged += new DataGridViewColumnEventHandler(dgv_ColumnDisplayIndexChanged);
    
        }

         
      

  


        private void dgv_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            //resizeSumBoxes();
            //reCreateSumBoxes();
        }

        private void dgv_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            resizeSumBoxes();
        }

        //private void dgv_ColumnRemoved(object sender, DataGridViewColumnEventArgs e)
        //{
        //    reCreateSumBoxes();
        //}

        //private void dgv_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        //{
        //    reCreateSumBoxes();
        //}

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            ReadOnlyTextBox roTextBox = (ReadOnlyTextBox)sumBoxHash[dgv.Columns[e.ColumnIndex]];
            if (roTextBox != null)
            {
                if (roTextBox.IsSummary)
                    calcSummaries();
            }
        }

        //private void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        //{
        //    calcSummaries();
        //}

        //private void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        //{
        //    calcSummaries();
        //}

        private void SummaryControlContainer_VisibleChanged(object sender, EventArgs e)
        {
            if (lastVisibleState != this.Visible)
            {
                OnVisiblityChanged(sender, e);
            }
        }

        protected void OnVisiblityChanged(object sender, EventArgs e)
        {
            if (VisibilityChanged != null)
                VisibilityChanged(sender, e);

            lastVisibleState = this.Visible;
        }

        #endregion

        #region Events and delegates

        //private void dgv_CreateSummary(object sender, EventArgs e)
        //{
        //    reCreateSumBoxes();
        //    calcSummaries();
        //}

        private void dgv_Scroll(object sender, ScrollEventArgs e)
        {
            resizeSumBoxes();
        }

        private void dgv_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            resizeSumBoxes();
        }

        private void dgv_RowHeadersWidthChanged(object sender, EventArgs e)
        {
            resizeSumBoxes();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            resizeSumBoxes();
        }

        private void dgv_Resize(object sender, EventArgs e)
        {
            resizeSumBoxes();
        }

        #endregion

        #region Functions

        /// <summary>
        /// Checks if passed object is of type of integer
        /// </summary>
        /// <param name="o">object</param>
        /// <returns>true/ false</returns>
        protected bool IsInteger(object o)
        {
            if (o is Int64)
                return true;
            if (o is Int32)
                return true;
            if (o is Int16)
                return true;
            return false;
        }

        /// <summary>
        /// Checks if passed object is of type of decimal/ double
        /// </summary>
        /// <param name="o">object</param>
        /// <returns>true/ false</returns>
        protected bool IsDecimal(object o)
        {
            if (o is Decimal)
                return true;
            if (o is Single)
                return true;
            if (o is Double)
                return true;
            return false;
        }

        /// <summary>
        /// Enable manual refresh of the SummaryDataGridView
        /// </summary>
        internal void RefreshSummary(bool ReCreateSummary=false)
        {
            if (dgv.SummaryRowVisible == false)
                return;
            if (ReCreateSummary)
            {
                reCreateSumBoxes();
            }
            calcSummaries();
        }

        /// <summary>
        /// Calculate the Sums of the summary columns
        /// </summary>
        private void calcSummaries()
        {
            if (dgv.SummaryRowVisible == false)
                return;

            foreach (ReadOnlyTextBox roTextBox in sumBoxHash.Values)
            {
                if (roTextBox.IsSummary)
                {
                    roTextBox.Tag = 0;
                    roTextBox.Text = "0";
                    roTextBox.Invalidate();
                }
            }


            if (dgv.SummaryColumns != null && dgv.SummaryColumns.Length > 0 && sumBoxHash.Count > 0)
            {

                foreach (DataGridViewRow dgvRow in dgv.Rows)
                {
                    foreach (DataGridViewCell dgvCell in dgvRow.Cells)
                    {
                        foreach (DataGridViewColumn dgvColumn in sumBoxHash.Keys)
                        {
                            if (dgvCell.OwningColumn.Equals(dgvColumn))
                            {
                                ReadOnlyTextBox sumBox = (ReadOnlyTextBox)sumBoxHash[dgvColumn];

                                if (sumBox != null && sumBox.IsSummary)
                                {
                                    if (dgvCell.Value != null && !(dgvCell.Value is DBNull))
                                    {
                                        if (IsInteger(dgvCell.Value))
                                        {
                                            sumBox.Tag = Convert.ToInt64(sumBox.Tag) + Convert.ToInt64(dgvCell.Value);
                                        }
                                        else if (IsDecimal(dgvCell.Value))
                                        {
                                            sumBox.Tag = Convert.ToDecimal(sumBox.Tag) + Convert.ToDecimal(dgvCell.Value);
                                        }

                                        sumBox.Text = string.Format("{0}", sumBox.Tag);
                                        sumBox.Invalidate();
                                    }
                                }
                            }
                        }
                    }
                   
                }
                dgv.OnSummaryCalculated(dgv, new EventArgs());
            }

        }


        

        /// <summary>
        /// Create summary boxes for each Column of the DataGridView        
        /// </summary>
        public  void reCreateSumBoxes()
        {
            ReadOnlyTextBox sumBox;
            if (dgv.SummaryRowVisible == false)
                return;
            foreach (Control control in sumBoxHash.Values)
            {
                this.Controls.Remove(control);
            }
            sumBoxHash.Clear();
            _SummaryCells.Clear();

            if (dgv.DisplaySumRowHeader)
            {
                sumRowHeaderLabel.Font = new Font(dgv.DefaultCellStyle.Font, dgv.SumRowHeaderTextBold ? FontStyle.Bold : FontStyle.Regular);
                sumRowHeaderLabel.Anchor = AnchorStyles.Left;
                sumRowHeaderLabel.TextAlign = ContentAlignment.MiddleLeft;
                sumRowHeaderLabel.Height = sumRowHeaderLabel.Font.Height;
                sumRowHeaderLabel.Top = Convert.ToInt32(Convert.ToDouble(this.InitialHeight - sumRowHeaderLabel.Height) / 2F);
                sumRowHeaderLabel.Text = dgv.SumRowHeaderText;

                sumRowHeaderLabel.ForeColor = dgv.DefaultCellStyle.ForeColor;
                sumRowHeaderLabel.Margin = new Padding(0);
                sumRowHeaderLabel.Padding = new Padding(0);
                sumRowHeaderLabel.TabIndex = 0;

                this.Controls.Add(sumRowHeaderLabel);
            }
            int iCnt = 0;

            List<DataGridViewColumn> sortedColumns = SortedColumns;
            foreach (DataGridViewColumn dgvColumn in sortedColumns)
            {
                if (dgvColumn.Name != "col_MoveUp" && dgvColumn.Name != "col_MoveDown" && dgvColumn.Name != "col_Delete" && dgvColumn.Name != "col_Edit" &&
                dgvColumn.Name != "col_CheckBox" && dgvColumn.Name != "col_AutoSlno")
                {

                    sumBox = new ReadOnlyTextBox();
                    sumBoxHash.Add(dgvColumn, sumBox);

                    sumBox.Top = 0;
                    sumBox.Height = dgv.RowTemplate.Height;
                    sumBox.BorderColor = dgv.GridColor;
                    sumBox.Name = dgvColumn.Name;
                    if (summaryRowBackColor == null)
                        sumBox.BackColor = dgv.DefaultCellStyle.BackColor;
                    else
                        sumBox.BackColor = summaryRowBackColor;
                    sumBox.BringToFront();

                    if (dgv.ColumnCount - iCnt == 1)
                        sumBox.IsLastColumn = true;

                    if (dgv.SummaryColumns != null && dgv.SummaryColumns.Length > 0)
                    {
                        for (int iCntX = 0; iCntX < dgv.SummaryColumns.Length; iCntX++)
                        {
                            if (dgv.SummaryColumns[iCntX] == dgvColumn.DataPropertyName ||
                                dgv.SummaryColumns[iCntX] == dgvColumn.Name)
                            {
                                dgvColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                                if (!dgvColumn.IsDataBound)
                                {

                                    dgvColumn.CellTemplate.Style.Format = dgv.FormatString;
                                }
                                else
                                {
                                    switch (dgvColumn.ValueType.Name)
                                    {
                                        case "Decimal":
                                            dgvColumn.CellTemplate.Style.Format = dgv.FormatString;
                                            break;
                                        default:
                                            break;

                                    }
                                }

                                sumBox.TextAlign = TextHelper.TranslateGridColumnAligment(dgvColumn.DefaultCellStyle.Alignment);
                                sumBox.IsSummary = true;

                                sumBox.FormatString = dgvColumn.CellTemplate.Style.Format;
                                if (dgvColumn.ValueType == typeof(System.Int32) || dgvColumn.ValueType == typeof(System.Int16) ||
                                    dgvColumn.ValueType == typeof(System.Int64) || dgvColumn.ValueType == typeof(System.Single) ||
                                    dgvColumn.ValueType == typeof(System.Double) || dgvColumn.ValueType == typeof(System.Single) ||
                                    dgvColumn.ValueType == typeof(System.Decimal))
                                    sumBox.Tag = System.Activator.CreateInstance(dgvColumn.ValueType);
                            }
                        }
                    }

                    sumBox.BringToFront();
                    sumBox.TabIndex = iCnt + 1;
                    this.Controls.Add(sumBox);
                    sumBox.DataPropertyName = dgvColumn.Name;
                    _SummaryCells.Add(sumBox);
                    iCnt++;
                }
            }


            calcSummaries();
            resizeSumBoxes();
        }

        /// <summary>
        /// Order the columns in the way they are displayed
        /// </summary>
        private List<DataGridViewColumn> SortedColumns
        {
            get
            {
                List<DataGridViewColumn> result = new List<DataGridViewColumn>();
                DataGridViewColumn column = dgv.Columns.GetFirstColumn(DataGridViewElementStates.None);
                if (column == null)
                    return result;
                result.Add(column);
                while ((column = dgv.Columns.GetNextColumn(column, DataGridViewElementStates.None, DataGridViewElementStates.None)) != null)
                    result.Add(column);

                return result;
            }
        }

        /// <summary>
        /// Resize the summary Boxes depending on the 
        /// width of the Columns of the DataGridView
        /// </summary>
        public  void resizeSumBoxes()
        {
            this.SuspendLayout();
            if (sumBoxHash.Count > 0)
                try
                {
                    int rowHeaderWidth = dgv.RowHeadersVisible ? dgv.RowHeadersWidth - 1 : 0;
                    int sumLabelWidth = dgv.RowHeadersVisible ? dgv.RowHeadersWidth - 1 : 0;
                    int curPos = rowHeaderWidth;

                    if (dgv.ShowSerialNo)
                    {
                        if(dgv.Columns.Contains("Col_AutoSlno"))
                        {
                        curPos += dgv.Columns["Col_AutoSlno"].Width;
                        }
                    }

                    if (dgv.DisplaySumRowHeader && sumLabelWidth > 0)
                    {
                        if (dgv.RightToLeft == RightToLeft.Yes)
                        {
                            if (sumRowHeaderLabel.Dock != DockStyle.Right)
                                sumRowHeaderLabel.Dock = DockStyle.Right;
                        }
                        else
                        {
                            if (sumRowHeaderLabel.Dock != DockStyle.Left)
                                sumRowHeaderLabel.Dock = DockStyle.Left;

                        }
                    }
                    else
                    {
                        if (sumRowHeaderLabel.Visible)
                            sumRowHeaderLabel.Visible = false;
                    }

                    int iCnt = 0;
                    Rectangle oldBounds;

                    foreach (DataGridViewColumn dgvColumn in SortedColumns) //dgv.Columns)
                    {
                        ReadOnlyTextBox sumBox = (ReadOnlyTextBox)sumBoxHash[dgvColumn];


                        if (sumBox != null)
                        {
                            oldBounds = sumBox.Bounds;
                            if (!dgvColumn.Visible)
                            {
                                sumBox.Visible = false;
                                continue;
                            }

                            int from = curPos - dgv.HorizontalScrollingOffset;

                            int width = dgvColumn.Width + (iCnt == 0 ? 0 : 0);

                            if (from < rowHeaderWidth)
                            {
                                width -= rowHeaderWidth - from;
                                from = rowHeaderWidth;
                            }

                            if (from + width > this.Width)
                                width = this.Width - from;

                            if (width < 4)
                            {
                                if (sumBox.Visible)
                                    sumBox.Visible = false;
                            }
                            else
                            {
                                if (this.RightToLeft == RightToLeft.Yes)
                                    from = this.Width - from - width;


                                if (sumBox.Left != from || sumBox.Width != width)
                                    sumBox.SetBounds(from, 0, width, 0, BoundsSpecified.X | BoundsSpecified.Width);

                                if (!sumBox.Visible)
                                    sumBox.Visible = true;
                            }

                            curPos += dgvColumn.Width + (iCnt == 0 ? 0 : 0);
                            if (oldBounds != sumBox.Bounds)
                                sumBox.Invalidate();

                        }
                        iCnt++;
                    }
                }
                finally
                {
                    this.ResumeLayout();
                }
        }

        #endregion
    }


    public   class SummaryCellCollection : ICollection<ReadOnlyTextBox>  
{
        private List<ReadOnlyTextBox> SummaryCells = new List<ReadOnlyTextBox>();

        public SummaryCellCollection()
        {
        }

        public ReadOnlyTextBox this[int index]
        {
            get
            {
                if (index <= SummaryCells.Count - 1)
                {
                    return SummaryCells[index];
                }
                else
                {
                    return null;
                }

            }
        }

        public ReadOnlyTextBox this[string ColumnName]
        {
            get
            {
                ReadOnlyTextBox rt=new ReadOnlyTextBox();
                foreach (ReadOnlyTextBox t in SummaryCells)
                {
                    if (t.DataPropertyName.Trim().ToUpper() == ColumnName.Trim().ToUpper())
                    {
                        rt= t;
                        break;
                    }

                }
                return rt;
            }
        }


        public void Clear()
        {
            SummaryCells.Clear();
        }

        public void Add(ReadOnlyTextBox item)
        {
            SummaryCells.Add(item);
        }

        public bool Contains(ReadOnlyTextBox item)
        {
            return SummaryCells.Contains(item);
        }

        public void CopyTo(ReadOnlyTextBox[] array, int arrayIndex)
        {
            SummaryCells.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return SummaryCells.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ReadOnlyTextBox item)
        {
            return SummaryCells.Remove(item);
        }
 

             IEnumerator<ReadOnlyTextBox> IEnumerable<ReadOnlyTextBox>.GetEnumerator()
             {
                 return this.SummaryCells.GetEnumerator();
             }

             IEnumerator IEnumerable.GetEnumerator()
             {
                 return this.SummaryCells.GetEnumerator();
             }
}
}
