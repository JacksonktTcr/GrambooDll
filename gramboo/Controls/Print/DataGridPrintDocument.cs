using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gramboo.Controls
{
    public class DataGridPrintDocument : System.Drawing.Printing.PrintDocument
    {
        #region Private Variables

        DataGridView dgvPrint = null;//Datagridview to be print.
        PrintOption printOption = null;//Print settings (title text,font etc...).
        int currentRowPos = 0;// To store current row postion of gridview, while printing.
        int pageNo = 1;//Consecutive page number.
        int rowsPerPage = 0;//Max No.Of rows in a page.

        #endregion Private Variables

        #region Properties

        #endregion Properties

        #region Constructors

        public DataGridPrintDocument(DataGridView dataGrid)
        {
            this.dgvPrint = dataGrid;
            printOption = new PrintOption()
            {
                FitPrintPage = true,
                GridColumns = dgvPrint.Columns,
                SelectedColumns=new List<DataGridViewColumn> (),
                DrawGridLine=true,
                DrawHorizontalLine=true,
                DrawVerticalGridLine=true
            };
           // this.BeginPrint += DataGridPrintDocument_BeginPrint;
            this.PrintPage += DataGridPrintDocument_PrintPage;

        }

        #endregion Constructors

        #region Events

        void DataGridPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                DrawHeader(e);

                DrawDate(e);

                float topMargin = e.MarginBounds.Top;
                float leftMargin = e.MarginBounds.Left;
                float rightMargin = e.MarginBounds.Right;
                float bottomMargin = e.MarginBounds.Bottom;

                //To maintain right postion for line.
                int rightPosition = printOption.FitPrintPage ? Convert.ToInt32(rightMargin) : 
                                       Convert.ToInt32(leftMargin + GetSumColumnWidth(dgvPrint)) ;

                float tempYRowPostion = topMargin;


                int rowCount = dgvPrint.SelectedRows.Count > 0 ? dgvPrint.SelectedRows.Count : dgvPrint.Rows.Count;


                if (rowCount > 0)
                {
                    DrawColumnHeader(e, ref leftMargin, ref tempYRowPostion, ref rightPosition);
                }

                while (rowCount > currentRowPos)
                {
                    DataGridViewRow gridRow = dgvPrint.Rows[currentRowPos];
                    if (dgvPrint.SelectedRows.Count > 0)
                        gridRow = dgvPrint.SelectedRows[currentRowPos];


                    e.Graphics.DrawLine(printOption.LinePen, 
                        new Point(Convert.ToInt32(leftMargin), Convert.ToInt32(tempYRowPostion)), 
                            new Point(rightPosition, Convert.ToInt32(tempYRowPostion)));

                    if (gridRow.IsNewRow)
                    {
                        if (pageNo == 1)
                        {
                            rowsPerPage = currentRowPos;
                        }
                        DrawColumnLine(dgvPrint, e.Graphics, e.MarginBounds, tempYRowPostion);
                        DrawFooter(e);
                        return;
                    }

                    if (tempYRowPostion + GetRowHeight(gridRow, e) >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                       
                        e.Graphics.DrawLine(printOption.LinePen, new Point(Convert.ToInt32(leftMargin), Convert.ToInt32(topMargin)), new Point(Convert.ToInt32(leftMargin), Convert.ToInt32(tempYRowPostion)));
                        DrawColumnLine(dgvPrint, e.Graphics, e.MarginBounds, tempYRowPostion);
                        if (pageNo == 1)
                        {
                            rowsPerPage = currentRowPos;
                        }
                       
                        pageNo++;
                        e.HasMorePages = true;
                        DrawFooter(e);
                        return;
                    }

                    float tempLeftMargin = leftMargin;

                    foreach (DataGridViewCell cell in gridRow.Cells)
                    {
                        if (cell.Visible && printOption.SelectedColumns.Contains(cell.OwningColumn))
                        {
                            Rectangle rect = new Rectangle((int)tempLeftMargin,
                                (int)(tempYRowPostion),(int) GetCellWidth(cell, e.MarginBounds.Width),(int) GetRowHeight(gridRow, e));

                            //e.Graphics.DrawString(Convert.ToString(cell.Value), GetCellFont(cell), Brushes.Black,
                            //tempLeftMargin, tempYRowPostion, GetStringFormat(cell));

                            if (cell.GetType().Name == "DataGridViewImageCell")
                                e.Graphics.DrawImage((Image)(cell.FormattedValue), rect);
                            else
                                e.Graphics.DrawString(Convert.ToString(cell.Value), GetCellFont(cell), Brushes.Black,
                          rect, GetStringFormat(cell));

                            tempLeftMargin += GetCellWidth(cell, e.MarginBounds.Width);
                        }
                    }

                    tempYRowPostion += GetRowHeight(gridRow, e);

                    currentRowPos++;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        //void DataGridPrintDocument_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
           
        //}

        public void GridPrint()
        {
            PrintDialog prtDlg=new PrintDialog ();
            if (prtDlg.ShowDialog() == DialogResult.OK)
            {
                this.PrinterSettings = prtDlg.PrinterSettings;
            }

            if (new DataGridPrintOptions(printOption).ShowDialog() == DialogResult.OK)
            {
                PrintPreviewDialog prtPrvDlg = new PrintPreviewDialog();
                prtPrvDlg.Document = this;
                if (prtPrvDlg.ShowDialog() != DialogResult.OK)
                {
                    this.PrintPage -= DataGridPrintDocument_PrintPage;
                }
                this.Print();
            }
        }

        #endregion Events

        #region Methods

        /// <summary>
        /// To check whether it is valid row or not.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsInvalidValidRow(DataGridViewRow row)
        {   
            return row.IsNewRow;
        }

        /// <summary>
        /// To draw vertical lines for columns.
        /// </summary>
        /// <param name="grd"></param>
        /// <param name="g"></param>
        /// <param name="marginBound"></param>
        /// <param name="bottom"></param>
        private void DrawColumnLine(DataGridView grd, Graphics g, Rectangle marginBound, float bottom)
        {
           
            float leftMargin = marginBound.Left;

            g.DrawLine(printOption.LinePen, new Point(Convert.ToInt32(leftMargin), Convert.ToInt32(marginBound.Top)), new Point(Convert.ToInt32(leftMargin), Convert.ToInt32(bottom)));
          
            foreach (DataGridViewColumn cln in printOption.SelectedColumns)
            {
                if (cln.Visible)
                {

                    if (printOption.FitPrintPage)
                    {  
                        leftMargin += GetCellWidth(dgvPrint.Rows[0].Cells[cln.Index], marginBound.Width);

                        if (cln == printOption.SelectedColumns.Cast<DataGridViewColumn>().Last())
                        {
                            leftMargin += marginBound.Right - leftMargin;
                        }
                    }
                    else
                        leftMargin += cln.Width;
                 
                   

                    if (!printOption.DrawVerticalGridLine)
                        if (cln != printOption.SelectedColumns.Cast<DataGridViewColumn>().Last())
                            continue;
                  
                    g.DrawLine(printOption.LinePen,
                        new Point(Convert.ToInt32(leftMargin), Convert.ToInt32(marginBound.Top)),
                        new Point(Convert.ToInt32(leftMargin), Convert.ToInt32(bottom)));

              
                }

                 
            }
        }

        /// <summary>
        /// To get row height for given row in given graphics.
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        private float GetRowHeight(DataGridViewRow Row, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //return Row.Cells.Cast<DataGridViewCell>().Select(cell => cell.InheritedStyle.Font.GetHeight(g)).Max<float>();
            //return Row.Height;

            float textColHgt = 0;
            float imgColHgt = 0;

            var txtCol = Row.Cells.Cast<DataGridViewCell>().Where(col => col.GetType() == typeof(DataGridViewTextBoxCell));
            var imgCol = Row.Cells.Cast<DataGridViewCell>().Where(col => col.GetType() == typeof(DataGridViewImageCell));

            if (txtCol!=null && txtCol.Count()>0)
            textColHgt = txtCol.Select(cell => e.Graphics.MeasureString(Convert.ToString(cell.Value), cell.InheritedStyle.Font, (int)GetCellWidth(cell, e.MarginBounds.Width), GetStringFormat(cell))).Select(size => size.Height).Max();
            if (imgCol != null && imgCol.Count() > 0)
            imgColHgt = imgCol.Select(cell => ((Image)cell.FormattedValue).Size.Height).Max();

            if (imgColHgt > textColHgt)
                return imgColHgt;
            else
               return textColHgt;

            //return Row.Cells.Cast<DataGridViewCell>().Select(cell => e.Graphics.MeasureString(Convert.ToString(cell.Value), cell.InheritedStyle.Font,(int) GetCellWidth(cell, e.MarginBounds.Width), GetStringFormat(cell))).Select(size => size.Height).Max();
        }

        /// <summary>
        /// To get row header height.
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        private float GetHeaderRowHeight(DataGridView dgv, Graphics g)
        {
            return dgv.Columns.Cast<DataGridViewColumn>().Select(cln => cln.HeaderCell.InheritedStyle.Font.GetHeight(g)).Max<float>();
        }

        /// <summary>
        /// To get sum all selected column with
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        private int GetSumColumnWidth(DataGridView dgv)
        {   
            return dgv.Columns.Cast<DataGridViewColumn>().Where(col => col.Visible && printOption.SelectedColumns.Contains(col)).Select(col => col.Width).Sum();
        }

        /// <summary>
        /// To get string format for cell.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private StringFormat GetStringFormat(DataGridViewCell cell)
        {
            StringFormat format = new StringFormat() 
            { 
                Trimming=StringTrimming.None,
                FormatFlags=StringFormatFlags.NoClip
            };


            DataGridViewContentAlignment algmnt = cell.Style.Alignment;
            if (algmnt == DataGridViewContentAlignment.NotSet)
                algmnt = cell.OwningColumn.DefaultCellStyle.Alignment;

            
             switch (algmnt)
             {
                 case DataGridViewContentAlignment.BottomLeft:
                     format.Alignment = StringAlignment.Near;
                     format.LineAlignment=StringAlignment.Far;
                     break;
                 case DataGridViewContentAlignment.MiddleLeft:
                     format.Alignment = StringAlignment.Near;
                     format.LineAlignment = StringAlignment.Center;
                     break;
                 case DataGridViewContentAlignment.TopLeft:
                     format.Alignment = StringAlignment.Near;
                     format.LineAlignment = StringAlignment.Near;
                     break;

                 case DataGridViewContentAlignment.BottomCenter:
                     format.Alignment = StringAlignment.Center;
                     format.LineAlignment = StringAlignment.Far;
                     break;
                 case DataGridViewContentAlignment.MiddleCenter:
                     format.Alignment = StringAlignment.Center;
                     format.LineAlignment = StringAlignment.Center;
                     break;
                 case DataGridViewContentAlignment.TopCenter:
                     format.Alignment = StringAlignment.Center;
                     format.LineAlignment = StringAlignment.Near;
                     break;

                 case DataGridViewContentAlignment.BottomRight:
                     format.Alignment = StringAlignment.Far;
                     format.LineAlignment = StringAlignment.Far;
                     break;
                 case DataGridViewContentAlignment.MiddleRight:
                     format.Alignment = StringAlignment.Far;
                     format.LineAlignment = StringAlignment.Center;
                     break;
                 case DataGridViewContentAlignment.TopRight:
                     format.Alignment = StringAlignment.Far;
                     format.LineAlignment = StringAlignment.Near;
                     break;
             }
             
            return format;
        }

        /// <summary>
        /// To get cell font.
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private Font GetCellFont(DataGridViewCell cell)
        {
            return cell.InheritedStyle.Font;
        }

        /// <summary>
        /// To get cell width.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private float GetCellWidth(DataGridViewCell cell, int width)
        {
            if (printOption.FitPrintPage)
            {
                //int totalWidth = GetSumColumnWidth(dgvPrint);
                //float perc = (cell.OwningColumn.Width * 100) / totalWidth;
                //return width * perc / 100;

                //float tolrnceWidth = (dgvPrint.Width - GetSumColumnWidth(dgvPrint)) / 4;

              return   (float)(
                                        Math.Floor(
                                        (double)(
                                        (double)(cell.OwningColumn.Width) /
                                       (double)(GetSumColumnWidth(dgvPrint)) * (double)(GetSumColumnWidth(dgvPrint) *
                                       ((double)width / (double)(GetSumColumnWidth(dgvPrint))
                                       )
                                       )
                                       )));
            }
            else
            {
                return cell.OwningColumn.Width;
            }
        }

        /// <summary>
        /// To draw header for print.
        /// </summary>
        /// <param name="e"></param>
        private void DrawHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {
            SizeF fntSize = e.Graphics.MeasureString(printOption.TitleText, printOption.TitleFont);
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            Rectangle rect = new Rectangle(e.MarginBounds.X, e.PageBounds.Y, e.MarginBounds.Width, e.PageBounds.Bottom - e.MarginBounds.Height - 100);

            e.Graphics.DrawString(printOption.TitleText, printOption.TitleFont, Brushes.Black, rect, format);
        }

        /// <summary>
        /// To Draw footer for print.
        /// </summary>
        /// <param name="e"></param>
        private void DrawFooter(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int count = dgvPrint.SelectedRows.Count > 0 ? dgvPrint.SelectedRows.Count : dgvPrint.Rows.Count;
            //string PageNum = pageNo.ToString() + " of " +
            //               Math.Ceiling((double)(count / rowsPerPage)).ToString();
            string PageNum = string.Format("Page: {0}", pageNo);
            SizeF fntSize = e.Graphics.MeasureString(PageNum, dgvPrint.Font);
            e.Graphics.DrawString(PageNum, dgvPrint.Font, Brushes.Black, e.MarginBounds.Left + e.MarginBounds.Width - fntSize.Width, e.MarginBounds.Bottom + fntSize.Height);
        }

        /// <summary>
        /// To draw date.
        /// </summary>
        /// <param name="e"></param>
        private void DrawDate(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string date = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            SizeF stringSize = e.Graphics.MeasureString(date, dgvPrint.Font);
            e.Graphics.DrawString(date,
                dgvPrint.Font,
                Brushes.Black,
                new Point((int)(e.PageBounds.Right-stringSize.Width),(int)(e.PageBounds.Top+stringSize.Height)));
        }

        /// <summary>
        /// To draw grid column header.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="leftMargin"></param>
        /// <param name="tempYRowPostion"></param>
        /// <param name="rightPosition"></param>
        public void DrawColumnHeader(System.Drawing.Printing.PrintPageEventArgs e,
            ref float leftMargin,
            ref float tempYRowPostion,
            ref int rightPosition)
        {
            float tempLeftMargin = leftMargin;

            foreach (DataGridViewColumn grdCln in printOption.SelectedColumns)
            {
                if (grdCln.Visible)
                {
                    e.Graphics.FillRectangle(printOption.HeadBackColor,
                        new Rectangle((int)(tempLeftMargin), (int)(tempYRowPostion), (int)(GetCellWidth(grdCln.HeaderCell, e.MarginBounds.Width)), (int)(GetHeaderRowHeight(dgvPrint, e.Graphics))));

                    Rectangle rect = new Rectangle((int)tempLeftMargin,
                                (int)(tempYRowPostion), (int)GetCellWidth(grdCln.HeaderCell, e.MarginBounds.Width), (int)(GetHeaderRowHeight(dgvPrint, e.Graphics)));

                    e.Graphics.DrawString(grdCln.HeaderText,
                       GetCellFont(grdCln.HeaderCell),
                       Brushes.Black,
                  rect,
                   GetStringFormat(grdCln.HeaderCell));

                    tempLeftMargin += GetCellWidth(grdCln.HeaderCell, e.MarginBounds.Width);
                }
            }



            e.Graphics.DrawLine(printOption.LinePen,
              new Point(Convert.ToInt32(leftMargin), Convert.ToInt32(tempYRowPostion)),
                  new Point(rightPosition, Convert.ToInt32(tempYRowPostion))
                  );

            tempYRowPostion += GetHeaderRowHeight(dgvPrint, e.Graphics);
        }

        #endregion Methods

        #region Classes

        #endregion Classes

        
    }

    public class PrintOption
    {
        public bool FitPrintPage { get; set; }
        public Pen LinePen = new Pen(Color.Black, 1);
        public string TitleText = "Title";
        public Font TitleFont = new Font(new FontFamily("Arial"), 25, FontStyle.Bold);
        public List<DataGridViewColumn> SelectedColumns { get; set; }
        public DataGridViewColumnCollection GridColumns { get; set; }
        public Brush HeadBackColor = Brushes.LightGray;
        public bool  DrawVerticalGridLine { get; set; }
        public bool DrawGridLine { get; set; }
        public bool DrawHorizontalLine { get; set; }
    }
}
