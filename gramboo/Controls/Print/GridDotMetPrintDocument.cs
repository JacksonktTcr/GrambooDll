using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gramboo.Controls
{
    public class GridDotMetPrintDocument
    {
        #region Private Variables

        //10 char per inch.

        int docWidth = 80;//(int) (21F / 0.0353F)/12;
        int docHeight = 66;//(int) ((29.7F / 0.0353F)/12);
        bool needSeparator = true;
        //private  cln = new DataGridViewColumn();
        public List<GridColumn> ColumnCollection = new List<GridColumn>();

        public string GetSeparator { get { return needSeparator ? "|" : string.Empty; } }

        public int CurrentRowPos = 0;

        public int rowCount = 0;

        public string TitleText = "Title";

        public string SubTitle = "Sub Title";

        public int PageNo = 1;

        #endregion Private Variables

        #region Properties

        #endregion Properties

        #region Constructors

        public GridDotMetPrintDocument(DataGridView dgvPrint)
        {

            foreach (DataGridViewColumn cln in dgvPrint.Columns)
            {
                if(cln.Visible)
                ColumnCollection.Add(new GridColumn() { column = cln, RefWidth = docWidth, ColumnCollection = ColumnCollection });
            }

      
            

            PrintDocument doc = new PrintDocument(docWidth, docHeight);

        start:

        rowCount = 0;

            string date = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            doc.AddText(date, docWidth, PrintDocument.Alignment.Right);
            doc.AddNewLine();
            doc.AddText(TitleText, docWidth, PrintDocument.Alignment.Center);
            doc.AddNewLine();
            doc.AddText(SubTitle, docWidth, PrintDocument.Alignment.Center);
            doc.AddNewLine('-');

            

            foreach (GridColumn cln in ColumnCollection)
            {
                if (cln == ColumnCollection.Last())
                {
                    doc.AddText(GetSeparator);
                    doc.AddText(cln.column.HeaderText, cln.PercWidth - 2, PrintDocument.Alignment.Left);
                    doc.AddText(GetSeparator);
                }
                else
                {
                    doc.AddText(GetSeparator);
                    doc.AddText(cln.column.HeaderText, cln.PercWidth - 1, PrintDocument.Alignment.Left);
                }
            }
            doc.AddNewLine('-');

            rowCount += 6;
            //foreach (DataGridViewRow row in dgvPrint.Rows)



            while (CurrentRowPos < dgvPrint.Rows.Count)
            {
                if (rowCount >= 57)
                {
                    doc.AddText(string.Format("Page: {0}", PageNo.ToString()), docWidth, PrintDocument.Alignment.Right);
                    PageNo++;
                    doc.AddNewLine();
                    goto start;
                }

                /* foreach (GridColumn cln in ColumnCollection)
                 {
                     if (cln == ColumnCollection.Last())
                     {
                         doc.AddText(GetSeparator);
                         doc.AddText(Convert.ToString(dgvPrint.Rows[CurrentRowPos].Cells[cln.column.Name].Value),
                             cln.PercWidth - 2,
                             PrintDocument.Alignment.Left);
                         doc.AddText(GetSeparator);
                     }
                     else
                     {
                         doc.AddText(GetSeparator);
                         doc.AddText(Convert.ToString(dgvPrint.Rows[CurrentRowPos].Cells[cln.column.Name].Value),
                             cln.PercWidth - 1,
                             PrintDocument.Alignment.Left);
                     }
                 }*/

                DataTable wrpdDt=Wrapping(dgvPrint.Rows[CurrentRowPos]);

                foreach (DataRow drRow in wrpdDt.Rows)
                {
                    foreach (GridColumn cln in ColumnCollection)
                    {
                        if (cln == ColumnCollection.Last())
                        {
                            doc.AddText(GetSeparator);
                            doc.AddText(Convert.ToString(drRow[cln.column.Name]),
                             cln.PercWidth - 2,
                             PrintDocument.Alignment.Left);
                            doc.AddText(GetSeparator);
                        }
                        else
                        {
                            doc.AddText(GetSeparator);
                            doc.AddText(Convert.ToString(drRow[cln.column.Name]),
                            cln.PercWidth - 1,
                            PrintDocument.Alignment.Left);
                        }

                    }

                    rowCount ++;
                    if (drRow != wrpdDt.AsEnumerable().Last())
                    doc.AddNewLine();
                    rowCount++;
                    
                   
                }

                doc.AddNewLine('-');

                rowCount++;

                CurrentRowPos++;
            }

            System.IO.File.WriteAllText(@"D:\txt.txt", doc.DocumentString);
        }

        #endregion Constructors

        #region Events
        #endregion Events

        #region Methods

        private List<string> WrapTextList(string stringToDivide,int part)
        {
           
            List<string> stringAfterDivide = new List<string>();
           
            int stringLength = stringToDivide.Length;

            for (int i = 0; i < stringLength; i += part)
            {
                if (i + part > stringLength) part = stringLength - i;
                stringAfterDivide.Add(stringToDivide.Substring(i, part));
            }

            return stringAfterDivide;
        }

        private DataTable Wrapping(DataGridViewRow row)
        {
            DataTable dt = new DataTable();

            Dictionary<GridColumn, List<string>> colction = new Dictionary<GridColumn, List<string>>();

            foreach (GridColumn cln in ColumnCollection)
            {
                if (cln == ColumnCollection.Last())
                {   
                    colction.Add(cln,WrapTextList(Convert.ToString(row.Cells[cln.column.Name].Value), cln.PercWidth - 2));
                }
                else
                {
                    colction.Add(cln,WrapTextList(Convert.ToString(row.Cells[cln.column.Name].Value), cln.PercWidth - 1));
                }

                dt.Columns.Add(cln.column.Name);
            }

            foreach (var itm in colction)
            {
                for (int i=0;i<itm.Value.Count;i++)
                {
                    if (i == dt.Rows.Count)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow[itm.Key.column.Name] = itm.Value[i];
                        dt.Rows.Add(newRow);
                    }
                    else
                    {
                        dt.Rows[i][itm.Key.column.Name] = itm.Value[i];
                    }
                }
            }



            return dt;

            //if (cln == ColumnCollection.Last())
            //{
            //    doc.AddText(GetSeparator);
            //    doc.AddText(Convert.ToString(dgvPrint.Rows[CurrentRowPos].Cells[cln.column.Name].Value),
            //        cln.PercWidth - 2,
            //        PrintDocument.Alignment.Left);
            //    doc.AddText(GetSeparator);
            //}
            //else
            //{
            //    doc.AddText(GetSeparator);
            //    doc.AddText(Convert.ToString(dgvPrint.Rows[CurrentRowPos].Cells[cln.column.Name].Value),
            //        cln.PercWidth - 1,
            //        PrintDocument.Alignment.Left);
            //}

        }

        #endregion Methods

        public class GridColumn
        {
            public DataGridViewColumn column { get; set; }
            public int Width { get { return column.Width; } }
            public int RefWidth { get; set; }
            public float WidthPerc
            {
                get 
                {
                    return Width * 100 / ColumnCollection.Sum(C => C.Width);
                }
            }

            public int PercWidth
            {
                get
                {
                    return (int) (RefWidth * WidthPerc / 100);
                }
            }

            public List<GridColumn> ColumnCollection { get; set; }
        }
    }

    public class PrintDocument
    {
        int documentWidth = 0;
        int documentHeight = 0;

        StringBuilder document=new StringBuilder ();

        public string DocumentString { get { return document.ToString(); } }

        public PrintDocument(int width,int height)
        {
            documentWidth = width;
            documentHeight = height;
        }

        public void AddNewLine()
        {   
            document.Append(Environment.NewLine);
        }

        public void AddNewLine(int count)
        {
            for (int i = 0; i < count;i++ )
                document.Append(Environment.NewLine);
        }

        public void AddNewLine(char lineChar)
        {
            document.Append(Environment.NewLine);
            document.Append(lineChar, documentWidth);
            document.Append(Environment.NewLine);
        }

        public void AddNewLine(char lineChar, int count)
        {
            document.Append(Environment.NewLine);
            document.Append(lineChar, count);
            document.Append(Environment.NewLine);
        }

        public void AddText(string text)
        {
            document.Append(text);
        }

        public void AddText(string text, int width, Alignment alignment)
        {
            switch (alignment)
            {
                case Alignment.Left:
                    document.Append(text.PadRight(width));
                    break;
                case Alignment.Center:
                    document.Append(text.PadLeft(width / 2 + text.Length / 2));
                    break;
                case Alignment.Right:
                    document.Append(text.PadLeft(width));
                    break;
            }
        }

        public enum Alignment
        {
            Left,
            Center,
            Right
        }
    }
}
