using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gramboo.Controls
{
    public class SummaryControlContainer : Control
    {
        #region Fields

        private readonly Dictionary<DataGridViewColumn, ReadOnlyTextBox> sumBoxMap;
        private readonly GrbDataGridView dgv;
        private readonly Label sumRowHeaderLabel;
        private SummaryCellCollection _summaryCells = new SummaryCellCollection();

        // Guard to prevent re-entrant calls that can be triggered by layout/events
        private bool _isRecreatingSumBoxes = false;

        private int initialHeight;
        private bool lastVisibleState;
        private Color summaryRowBackColor;
        
        // Performance optimization: cache summary totals
        private readonly Dictionary<int, decimal> summaryCache = new Dictionary<int, decimal>();
        private bool cacheValid = false;

        #endregion

        #region Properties

        public int InitialHeight
        {
            get { return initialHeight; }
            set { initialHeight = value; }
        }

        // (get_Item moved into SummaryCellCollection for compatibility)

        public bool LastVisibleState
        {
            get { return lastVisibleState; }
            set { lastVisibleState = value; }
        }

        public Color SummaryRowBackColor
        {
            get { return summaryRowBackColor; }
            set { summaryRowBackColor = value; }
        }

        public SummaryCellCollection SummaryCells
        {
            get { return _summaryCells; }
            set { _summaryCells = value ?? new SummaryCellCollection(); }
        }

        // Diagnostic accessor for external checks
        internal bool IsRecreatingSumBoxes
        {
            get { return _isRecreatingSumBoxes; }
        }

        // ✅ NEW: Public wrapper method for safe summary cell access
        public string GetTextOrZero(string columnName)
        {
            if (_summaryCells != null)
            {
                return _summaryCells.GetTextOrZero(columnName);
            }
            return "0";
        }

        public event EventHandler SummaryVisibilityChanged;

        #endregion

        #region Constructor

        public SummaryControlContainer(GrbDataGridView dgv)
        {
            if (dgv == null)
                throw new ArgumentNullException(nameof(dgv));

            this.dgv = dgv;
            this.sumBoxMap = new Dictionary<DataGridViewColumn, ReadOnlyTextBox>();
            this.sumRowHeaderLabel = new Label();

            // Subscribe to events
            this.dgv.CellValueChanged += dgv_CellValueChanged;
            this.dgv.Scroll += dgv_Scroll;
            this.dgv.ColumnWidthChanged += dgv_ColumnWidthChanged;
            this.dgv.RowHeadersWidthChanged += dgv_RowHeadersWidthChanged;
            this.dgv.ColumnStateChanged += dgv_ColumnStateChanged;
            this.dgv.Resize += dgv_Resize;
            
            // ✅ NEW: Cache invalidation events
            this.dgv.DataSourceChanged += dgv_DataSourceChanged;
            this.dgv.RowsAdded += dgv_RowsAdded;
            this.dgv.RowsRemoved += dgv_RowsRemoved;
            this.dgv.UserDeletingRow += dgv_UserDeletingRow;

            this.VisibleChanged += SummaryControlContainer_VisibleChanged;
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dgv != null)
                {
                    dgv.CellValueChanged -= dgv_CellValueChanged;
                    dgv.Scroll -= dgv_Scroll;
                    dgv.ColumnWidthChanged -= dgv_ColumnWidthChanged;
                    dgv.RowHeadersWidthChanged -= dgv_RowHeadersWidthChanged;
                    dgv.ColumnStateChanged -= dgv_ColumnStateChanged;
                    dgv.Resize -= dgv_Resize;
                    
                    // ✅ NEW: Unsubscribe from cache invalidation events
                    dgv.DataSourceChanged -= dgv_DataSourceChanged;
                    dgv.RowsAdded -= dgv_RowsAdded;
                    dgv.RowsRemoved -= dgv_RowsRemoved;
                    dgv.UserDeletingRow -= dgv_UserDeletingRow;
                }

                this.VisibleChanged -= SummaryControlContainer_VisibleChanged;
                
                // Clear cache
                summaryCache.Clear();
            }

            base.Dispose(disposing);
        }

        #endregion

        #region Event handlers

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            DataGridViewColumn column = dgv.Columns[e.ColumnIndex];

            ReadOnlyTextBox roTextBox;
            if (sumBoxMap.TryGetValue(column, out roTextBox) && roTextBox != null && roTextBox.IsSummary)
            {
                // Invalidate cache and recalculate only affected column
                cacheValid = false;
                calcSingleColumnSummary(column);
            }
        }

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

        private void dgv_ColumnStateChanged(object sender, DataGridViewColumnStateChangedEventArgs e)
        {
            resizeSumBoxes();
        }

        private void dgv_Resize(object sender, EventArgs e)
        {
            resizeSumBoxes();
        }

        private void SummaryControlContainer_VisibleChanged(object sender, EventArgs e)
        {
            if (lastVisibleState != this.Visible)
            {
                OnSummaryVisibilityChanged(sender, e);
            }
        }

        protected virtual void OnSummaryVisibilityChanged(object sender, EventArgs e)
        {
            SummaryVisibilityChanged?.Invoke(sender, e);
            lastVisibleState = this.Visible;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            resizeSumBoxes();
        }

        #endregion

        #region Cache Invalidation Handlers

        // ✅ NEW: Data source changed - invalidate cache and recalculate
        private void dgv_DataSourceChanged(object sender, EventArgs e)
        {
            cacheValid = false;
            calcSummaries();
        }

        // ✅ NEW: Rows added - invalidate cache and recalculate
        private void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            cacheValid = false;
            calcSummaries();
        }

        // ✅ NEW: Rows removed - invalidate cache and recalculate
        private void dgv_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            cacheValid = false;
            calcSummaries();
        }

        // ✅ NEW: User deleting row - invalidate cache (will recalc after delete)
        private void dgv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            cacheValid = false;
        }

        #endregion

        #region Helpers

        protected bool IsInteger(object o)
        {
            return o is Int16 || o is Int32 || o is Int64;
        }

        protected bool IsDecimal(object o)
        {
            return o is Decimal || o is Single || o is Double;
        }

        private bool IsSupportedNumericType(Type type)
        {
            if (type == null) return false;

            return type == typeof(short) ||
                   type == typeof(int) ||
                   type == typeof(long) ||
                   type == typeof(float) ||
                   type == typeof(double) ||
                   type == typeof(decimal);
        }

        private bool ShouldSkipColumn(DataGridViewColumn dgvColumn)
        {
            if (dgvColumn == null)
                return true;

            string name = dgvColumn.Name;

            return name == "col_MoveUp" ||
                   name == "col_MoveDown" ||
                   name == "col_Delete" ||
                   name == "col_Edit" ||
                   name == "col_CheckBox" ||
                   name == "col_AutoSlno" ||
                   name == "Col_AutoSlno";
        }

        private bool IsSummaryColumn(DataGridViewColumn dgvColumn)
        {
            if (dgv.SummaryColumns == null || dgv.SummaryColumns.Length == 0 || dgvColumn == null)
                return false;

            string colName = dgvColumn.Name ?? string.Empty;
            string dataPropertyName = dgvColumn.DataPropertyName ?? string.Empty;

            for (int i = 0; i < dgv.SummaryColumns.Length; i++)
            {
                string summaryCol = dgv.SummaryColumns[i];
                if (string.Equals(summaryCol, colName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(summaryCol, dataPropertyName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private List<DataGridViewColumn> SortedColumns
        {
            get
            {
                List<DataGridViewColumn> result = new List<DataGridViewColumn>();

                DataGridViewColumn column = dgv.Columns.GetFirstColumn(DataGridViewElementStates.None);
                while (column != null)
                {
                    result.Add(column);
                    column = dgv.Columns.GetNextColumn(column, DataGridViewElementStates.None, DataGridViewElementStates.None);
                }

                return result;
            }
        }

        #endregion

        #region Public methods

        internal void RefreshSummary(bool reCreateSummary = false)
        {
            if (!dgv.SummaryRowVisible) return;

            cacheValid = false;

            if (reCreateSummary)
            {
                reCreateSumBoxes();
                return; // let reCreateSumBoxesInternal schedule calcSummaries via BeginInvoke
            }

            calcSummaries();
        }

        public void reCreateSumBoxes()
        {
            reCreateSumBoxesInternal();
        }

        private void reCreateSumBoxesInternal()
        {
            // Prevent re-entrant calls which can be triggered by layout/events
            if (_isRecreatingSumBoxes)
                return;

            _isRecreatingSumBoxes = true;
            this.SuspendLayout();

            try
            {
                foreach (Control control in sumBoxMap.Values)
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }

                sumBoxMap.Clear();
                _summaryCells.Clear();
                summaryCache.Clear();

                if (dgv.DisplaySumRowHeader)
                {
                    // Avoid assigning Font unnecessarily (can trigger layout events)
                    var desiredStyle = dgv.SumRowHeaderTextBold ? FontStyle.Bold : FontStyle.Regular;
                    var baseFont = dgv.DefaultCellStyle.Font ?? this.Font;

                    bool needNewFont = sumRowHeaderLabel.Font == null
                        || sumRowHeaderLabel.Font.Style != desiredStyle
                        || !sumRowHeaderLabel.Font.FontFamily.Equals(baseFont.FontFamily)
                        || Math.Abs(sumRowHeaderLabel.Font.Size - baseFont.Size) > 0.01f;

                    if (needNewFont)
                    {
                        try
                        {
                            sumRowHeaderLabel.Font = new Font(baseFont, desiredStyle);
                        }
                        catch
                        {
                            // fallback: keep existing font if new creation fails
                        }
                    }

                    sumRowHeaderLabel.Anchor = AnchorStyles.Left;
                    sumRowHeaderLabel.TextAlign = ContentAlignment.MiddleLeft;
                        sumRowHeaderLabel.Height = sumRowHeaderLabel.Font.Height;
                    sumRowHeaderLabel.Top = Convert.ToInt32((double)(this.InitialHeight - sumRowHeaderLabel.Height) / 2D);
                    sumRowHeaderLabel.Text = dgv.SumRowHeaderText;
                    sumRowHeaderLabel.ForeColor = dgv.DefaultCellStyle.ForeColor;
                    sumRowHeaderLabel.Margin = new Padding(0);
                    sumRowHeaderLabel.Padding = new Padding(0);
                    sumRowHeaderLabel.TabIndex = 0;

                    if (!this.Controls.Contains(sumRowHeaderLabel))
                        this.Controls.Add(sumRowHeaderLabel);
                }
                else
                {
                    if (this.Controls.Contains(sumRowHeaderLabel))
                        this.Controls.Remove(sumRowHeaderLabel);
                }

                int tabIndex = 1;
                List<DataGridViewColumn> sortedColumns = SortedColumns;
                int visibleColumnCount = sortedColumns.Count;

                // ✅ NEW: Create summary cells for ALL columns (including hidden ones)
                for (int i = 0; i < visibleColumnCount; i++)
                {
                    DataGridViewColumn dgvColumn = sortedColumns[i];

                    if (ShouldSkipColumn(dgvColumn))
                        continue;

                    ReadOnlyTextBox sumBox = new ReadOnlyTextBox();
                    sumBoxMap[dgvColumn] = sumBox;

                    sumBox.Top = 0;
                    sumBox.Height = dgv.RowTemplate.Height;
                    sumBox.BorderColor = dgv.GridColor;
                    sumBox.Name = dgvColumn.Name;
                    sumBox.BackColor = summaryRowBackColor.IsEmpty
                        ? dgv.DefaultCellStyle.BackColor
                        : summaryRowBackColor;
                    sumBox.TabIndex = tabIndex++;
                    sumBox.DataPropertyName = dgvColumn.Name;
                    sumBox.IsLastColumn = (i == visibleColumnCount - 1);

                    if (IsSummaryColumn(dgvColumn))
                    {
                        if (!dgvColumn.IsDataBound)
                        {
                            dgvColumn.CellTemplate.Style.Format = dgv.FormatString;
                        }
                        else if (dgvColumn.ValueType != null && dgvColumn.ValueType == typeof(decimal))
                        {
                            dgvColumn.CellTemplate.Style.Format = dgv.FormatString;
                        }

                        sumBox.TextAlign = TextHelper.TranslateGridColumnAligment(dgvColumn.DefaultCellStyle.Alignment);
                        sumBox.IsSummary = true;
                        sumBox.FormatString = dgvColumn.CellTemplate.Style.Format;

                        if (IsSupportedNumericType(dgvColumn.ValueType))
                            sumBox.Tag = Activator.CreateInstance(dgvColumn.ValueType);
                        else
                            sumBox.Tag = 0m;
                    }
                    else
                    {
                        sumBox.IsSummary = false;
                        sumBox.Tag = null;
                    }

                    // ✅ NEW: Only add visible columns to the control collection
                    // Hidden columns will have their summary boxes created but not added to UI
                    if (dgvColumn.Visible)
                    {
                        this.Controls.Add(sumBox);
                        sumBox.BringToFront();
                    }

                    _summaryCells.Add(sumBox);
                }
               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                
            }
            finally
            {
                this.ResumeLayout();
                _isRecreatingSumBoxes = false;
            }

            try { calcSummaries(); } catch { }
            try { resizeSumBoxes(); } catch { }
        }

        public void resizeSumBoxes()
        {
            if (sumBoxMap.Count == 0)
                return;

            this.SuspendLayout();

            try
            {
                int rowHeaderWidth = dgv.RowHeadersVisible ? dgv.RowHeadersWidth - 1 : 0;
                int sumLabelWidth = rowHeaderWidth;
                int curPos = rowHeaderWidth;

                if (dgv.ShowSerialNo && dgv.Columns.Contains("Col_AutoSlno"))
                {
                    curPos += dgv.Columns["Col_AutoSlno"].Width;
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

                    if (!sumRowHeaderLabel.Visible)
                        sumRowHeaderLabel.Visible = true;
                }
                else
                {
                    if (sumRowHeaderLabel.Visible)
                        sumRowHeaderLabel.Visible = false;
                }

                List<DataGridViewColumn> columns = SortedColumns;

                for (int i = 0; i < columns.Count; i++)
                {
                    DataGridViewColumn dgvColumn = columns[i];

                    ReadOnlyTextBox sumBox;
                    if (!sumBoxMap.TryGetValue(dgvColumn, out sumBox) || sumBox == null)
                        continue;

                    if (!dgvColumn.Visible)
                    {
                        // ✅ NEW: Keep hidden column summary boxes hidden but maintain their data
                        if (sumBox.Visible)
                            sumBox.Visible = false;
                        
                        // ✅ NEW: Remove from controls if it exists
                        if (this.Controls.Contains(sumBox))
                            this.Controls.Remove(sumBox);

                        continue;
                    }
                    else
                    {
                        // ✅ NEW: If column becomes visible and summary box is not in controls, add it
                        if (!this.Controls.Contains(sumBox))
                            this.Controls.Add(sumBox);
                    }

                    int from = curPos - dgv.HorizontalScrollingOffset;
                    int width = dgvColumn.Width;

                    if (from < rowHeaderWidth)
                    {
                        width -= (rowHeaderWidth - from);
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
                        {
                            sumBox.SetBounds(from, 0, width, dgv.RowTemplate.Height,
                                BoundsSpecified.X | BoundsSpecified.Y | BoundsSpecified.Width | BoundsSpecified.Height);
                        }

                        if (!sumBox.Visible)
                            sumBox.Visible = true;
                    }

                    curPos += dgvColumn.Width;
                }
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        #endregion

        #region Summary calculation (OPTIMIZED)

        // ✅ OPTIMIZATION: Calculate only the affected column instead of recalculating all
        private void calcSingleColumnSummary(DataGridViewColumn column)
        {
            if (_isRecreatingSumBoxes)
                return;

            if (!dgv.SummaryRowVisible || dgv.SummaryPaused)
                return;

            ReadOnlyTextBox sumBox;
            if (!sumBoxMap.TryGetValue(column, out sumBox) || !sumBox.IsSummary)
                return;

            decimal total = CalculateColumnTotal(column.Index);
            sumBox.Tag = total;
            sumBox.Text = FormatSummaryValue(total, sumBox.FormatString);
            sumBox.Invalidate();
            dgv.OnSummaryCalculated(dgv, EventArgs.Empty);
        }

        // Guard to prevent overlapping background calculations
        private bool _calcPending = false;

        private void calcSummaries()
        {
            if (_isRecreatingSumBoxes)
                return;

            if (!dgv.SummaryRowVisible || dgv.SummaryPaused)
                return;

            if (sumBoxMap.Count == 0)
                return;

            if (cacheValid && summaryCache.Count > 0)
            {
                ApplyCachedSummaries();
                dgv.OnSummaryCalculated(dgv, EventArgs.Empty);
                return;
            }

            // Reset boxes to "0" immediately on the UI thread
            foreach (ReadOnlyTextBox box in sumBoxMap.Values)
            {
                if (box.IsSummary)
                {
                    box.Tag = 0m;
                    box.Text = "0";
                }
            }

            if (dgv.Rows.Count == 0 || dgv.SummaryColumns == null || dgv.SummaryColumns.Length == 0)
            {
                cacheValid = false;
                dgv.OnSummaryCalculated(dgv, EventArgs.Empty);
                return;
            }

            // If a calculation is already running, just mark cache invalid and let it finish
            if (_calcPending)
            {
                cacheValid = false;
                return;
            }

            _calcPending = true;
            cacheValid = false;

            // Snapshot the data needed for calculation so the background thread
            // does not touch live UI/DataGridView objects
            var snapshot = SnapshotColumnData();

            Task.Run(() =>
            {
                var results = new Dictionary<int, decimal>();
                foreach (var entry in snapshot)
                {
                    decimal total = 0m;
                    foreach (object val in entry.Value)
                    {
                        decimal parsed;
                        if (TryConvertToDecimal(val, out parsed))
                            total += parsed;
                    }
                    results[entry.Key] = total;
                }
                return results;
            }).ContinueWith(task =>
            {
                _calcPending = false;

                if (task.IsFaulted || task.IsCanceled)
                    return;

                var results = task.Result;

                if (_isRecreatingSumBoxes)
                    return;

                summaryCache.Clear();
                foreach (var kvp in results)
                    summaryCache[kvp.Key] = kvp.Value;

                foreach (KeyValuePair<DataGridViewColumn, ReadOnlyTextBox> kvp in sumBoxMap)
                {
                    ReadOnlyTextBox sumBox = kvp.Value;
                    if (sumBox == null || !sumBox.IsSummary)
                        continue;

                    decimal total;
                    if (results.TryGetValue(kvp.Key.Index, out total))
                    {
                        sumBox.Tag = total;
                        sumBox.Text = FormatSummaryValue(total, sumBox.FormatString);
                        sumBox.Invalidate();
                    }
                }

                cacheValid = true;
                dgv.OnSummaryCalculated(dgv, EventArgs.Empty);

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Snapshots cell values for summary columns so the background thread
        // never touches live DataGridView objects (not thread-safe).
        // Key = column index, Value = list of raw cell values.
        private Dictionary<int, List<object>> SnapshotColumnData()
        {
            var snapshot = new Dictionary<int, List<object>>();

            foreach (KeyValuePair<DataGridViewColumn, ReadOnlyTextBox> kvp in sumBoxMap)
            {
                if (kvp.Value == null || !kvp.Value.IsSummary)
                    continue;

                int colIndex = kvp.Key.Index;
                var values = new List<object>(dgv.Rows.Count);

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row == null || row.IsNewRow)
                        continue;
                    values.Add(row.Cells[colIndex].Value);
                }

                snapshot[colIndex] = values;
            }

            return snapshot;
        }

        // ✅ OPTIMIZATION: Extracted calculation logic for reuse
        private decimal CalculateColumnTotal(int colIndex)
        {
            decimal total = 0m;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row == null || row.IsNewRow)
                    continue;

                object value = row.Cells[colIndex].Value;
                if (value == null || value == DBNull.Value)
                    continue;

                decimal parsed;
                if (TryConvertToDecimal(value, out parsed))
                {
                    total += parsed;
                }
            }

            return total;
        }

        private void ApplyCachedSummaries()
        {
            foreach (KeyValuePair<DataGridViewColumn, ReadOnlyTextBox> kvp in sumBoxMap)
            {
                DataGridViewColumn column = kvp.Key;
                ReadOnlyTextBox sumBox = kvp.Value;

                if (sumBox == null || !sumBox.IsSummary)
                    continue;

                decimal total;
                if (summaryCache.TryGetValue(column.Index, out total))
                {
                    sumBox.Tag = total;
                    sumBox.Text = FormatSummaryValue(total, sumBox.FormatString);
                    sumBox.Invalidate();  // ✅ Force repaint
                }
            }
        }

        private bool TryConvertToDecimal(object value, out decimal result)
        {
            result = 0m;

            if (value == null || value == DBNull.Value)
                return false;

            if (value is decimal)
            {
                result = (decimal)value;
                return true;
            }

            if (value is int)
            {
                result = (int)value;
                return true;
            }

            if (value is long)
            {
                result = (long)value;
                return true;
            }

            if (value is short)
            {
                result = (short)value;
                return true;
            }

            if (value is float)
            {
                result = Convert.ToDecimal((float)value);
                return true;
            }

            if (value is double)
            {
                result = Convert.ToDecimal((double)value);
                return true;
            }

            return decimal.TryParse(Convert.ToString(value), out result);
        }

        private string FormatSummaryValue(decimal value, string formatString)
        {
            if (string.IsNullOrWhiteSpace(formatString))
                return value.ToString();

            try
            {
                return value.ToString(formatString);
            }
            catch
            {
                return value.ToString();
            }
        }

        #endregion
    }

    // ✅ NEW: Safe wrapper to prevent null reference exceptions
    public class SafeSummaryCell
    {
        private readonly ReadOnlyTextBox _cell;
        private const string _defaultValue = "0";

        public SafeSummaryCell(ReadOnlyTextBox cell)
        {
            _cell = cell;
        }

        // ✅ Return actual cell text value (don't override with defaults)
        public string Text
        {
            get
            {
                try
                {
                    if (_cell != null)
                        return _cell.Text ?? _defaultValue;
                    return _defaultValue;
                }
                catch
                {
                    return _defaultValue;
                }
            }
            set
            {
                try
                {
                    if (_cell != null)
                        _cell.Text = value ?? _defaultValue;
                }
                catch
                {
                    // Silently fail if unable to set
                }
            }
        }

        // ✅ Expose Tag property from underlying cell
        public object Tag
        {
            get
            {
                try
                {
                    return _cell?.Tag;
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    if (_cell != null)
                        _cell.Tag = value;
                }
                catch
                {
                    // Silently fail if unable to set
                }
            }
        }

        // ✅ Access the actual cell if needed
        public ReadOnlyTextBox Cell
        {
            get { return _cell; }
        }

        // ✅ Check if cell exists
        public bool Exists
        {
            get { return _cell != null; }
        }
    }

    public class SummaryCellCollection : ICollection<ReadOnlyTextBox>
    {
        private readonly List<ReadOnlyTextBox> summaryCells = new List<ReadOnlyTextBox>();

        public SummaryCellCollection()
        {
        }

        public ReadOnlyTextBox this[int index]
        {
            get
            {
                if (index >= 0 && index < summaryCells.Count)
                    return summaryCells[index];

                return null;
            }
        }

        // ✅ PRIMARY: String indexer returns ReadOnlyTextBox for binary compatibility with older compiled code.
        // Matches against DataPropertyName OR Name so the lookup doesn't spuriously
        // return null when callers pass the column Name (which previously triggered
        // an infinite RefreshSummary -> calcSummaries -> OnSummaryCalculated loop).
        public ReadOnlyTextBox this[string columnName]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(columnName))
                    return null;

                string key = columnName.Trim();

                foreach (ReadOnlyTextBox t in summaryCells)
                {
                    if (t == null) continue;

                    if (string.Equals(t.DataPropertyName?.Trim(), key, StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(t.Name?.Trim(), key, StringComparison.OrdinalIgnoreCase))
                    {
                        return t;
                    }
                }

                return null;
            }
        }

        // ✅ NEW: Internal method to get actual cell (not wrapped)
        private ReadOnlyTextBox GetActualCell(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName))
                return null;

            string key = columnName.Trim();

            foreach (ReadOnlyTextBox t in summaryCells)
            {
                if (t == null) continue;

                if (string.Equals(t.DataPropertyName?.Trim(), key, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(t.Name?.Trim(), key, StringComparison.OrdinalIgnoreCase))
                {
                    return t;
                }
            }

            return null;
        }



        // ✅ NEW: Safe access method - TryGet pattern
        public bool TryGetCell(string columnName, out ReadOnlyTextBox cell)
        {
            cell = GetActualCell(columnName);
            return cell != null;
        }

        // ✅ NEW: Get cell text safely with default value
        public string GetCellText(string columnName, string defaultText = "")
        {
            try
            {
                var cell = GetActualCell(columnName);
                return cell?.Text ?? defaultText;
            }
            catch
            {
                return defaultText;
            }
        }

        // ✅ NEW: Get cell value (Tag) safely with default value
        public decimal GetCellValue(string columnName, decimal defaultValue = 0m)
        {
            try
            {
                var cell = GetActualCell(columnName);
                if (cell != null && cell.Tag is decimal decValue)
                    return decValue;
                if (cell != null && cell.Tag != null && decimal.TryParse(cell.Tag.ToString(), out decimal parsed))
                    return parsed;
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        // ✅ NEW: Check if column exists
        public bool ContainsColumn(string columnName)
        {
            return GetActualCell(columnName) != null;
        }

        // ✅ NEW: Get all column names
        public List<string> GetColumnNames()
        {
            var names = new List<string>();
            foreach (ReadOnlyTextBox cell in summaryCells)
            {
                if (!string.IsNullOrWhiteSpace(cell?.DataPropertyName))
                    names.Add(cell.DataPropertyName);
            }
            return names;
        }

        // ✅ NEW: Get text or "0" if null (simple solution)
        public string GetTextOrZero(string columnName)
        {
            try
            {
                var safeSummaryCell = this[columnName];
                if (safeSummaryCell != null && !string.IsNullOrWhiteSpace(safeSummaryCell.Text))
                    return safeSummaryCell.Text;
                return "0";
            }
            catch
            {
                return "0";
            }
        }

        public void Clear()
        {
            summaryCells.Clear();
        }

        public void Add(ReadOnlyTextBox item)
        {
            if (item != null)
                summaryCells.Add(item);
        }

        public bool Contains(ReadOnlyTextBox item)
        {
            return summaryCells.Contains(item);
        }

        public void CopyTo(ReadOnlyTextBox[] array, int arrayIndex)
        {
            summaryCells.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return summaryCells.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ReadOnlyTextBox item)
        {
            return summaryCells.Remove(item);
        }

        public IEnumerator<ReadOnlyTextBox> GetEnumerator()
        {
            return summaryCells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return summaryCells.GetEnumerator();
        }
    }
}