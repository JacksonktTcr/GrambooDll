using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace REGAL
{
    public class Printer
    {

        private Graphics g;
        private string defBrush;
        private SolidBrush defaultBrush;
        private ArrayList prn = new ArrayList();
        private List<PrintContent> prnHeader = new List<PrintContent>();
        //Footer
        private List<PrintContent> prnFooter = new List<PrintContent>();
        //Footer
        private int currentline;
        private bool SetPreferences = false;
        private System.Windows.Forms.PrintDialog prnDialog;
        private System.Windows.Forms.PrintPreviewDialog prnPreview;
        private System.Drawing.Printing.PrintDocument prnDocument;
        private bool _newline;
       // private PrintContent p1;
        float lineheight;
        int pageno = 1;

        public string FontName { get; set; }
        public int FontSize { get; set; }
        public FontStyle FontStyle { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Bottom { get; set; }
        public int Right { get; set; }
        public float PageWidth { get; set; }
        public float PageHeight { get; set; }
        public float CurrentX { get; set; }
        public float CurrentY { get; set; }
        public Brush PrintBrush { get; set; }
        private bool ToHeader { get; set; }
        private bool ToFooter { get; set; }
        private Font printFont;
        public IEnumerator items;
    
        public PrintAlign PrintAlignment { get; set; }

        public delegate void NextPageHandler(object sender, HeaderPrintEventAgrs  e);
      
        public event NextPageHandler StartPage;

        public delegate void Footer(object sender, FooterPrintEventArgs e);

        public event Footer EndPage;


       
      
        public bool NewLine
        {
            get
            {
                return _newline;
            }
            set
            {
                _newline = value;
                CurrentX = Left;
            }
        }

        public Printer()
        {
            prnDialog = new PrintDialog();
            prnPreview = new PrintPreviewDialog();
            prnDocument = new PrintDocument();

          


            FontName = "Courier New";
            FontSize = 12;
            FontStyle = FontStyle.Regular;
            Top = 10;
            Left = 10;
            Bottom = 10;
            Right = 10;
            PageWidth = 8.27f;
            PageHeight = 11.69f;
            CurrentX = 10f;
            CurrentY = 10f;
            printFont = new Font("Courier New", 10);
            defaultBrush = new SolidBrush(Color.Black);
            prn = new ArrayList ();
            prnHeader  = new List<PrintContent>();
            prnFooter = new List<PrintContent>();
            PrintBrush = defaultBrush;
            PrintAlignment = PrintAlign.Left;
            NewLine = true;
            ToHeader = false;
            ToFooter = false;
            SetPreferences = false;
            currentline = 0;
            prnDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(prnDocument_PrintPage);
           // StartPage += new NextPageHandler(OnStartPage);
            //prnDocument.PrintPage += new PrintPageEventHandler(pd_PrintNextPage);
            }





 


       protected virtual void OnStartPage(object sender, HeaderPrintEventAgrs e)
        {
            if (StartPage != null)
            {
                ToHeader = true;
                StartPage(new object(), e);
                ToHeader = false;
            }
            else
            {
                ToHeader = true;
                PrintHeader(g);
                ToHeader = false;
            }
        }

       protected virtual void OnEndPage(object sender, FooterPrintEventArgs e)
       {
           if (EndPage != null)
           {
               //ToFooter = true;
               EndPage(new object(), e);
               //ToFooter = false;
           }
           else
           {
               ToFooter = true;
               //printFooter(g);
               //ToFooter = false;
           }
       }




       private void prnDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
       {

          // int pprid=0;


          // PaperSize[] comboPaperSize  =new PaperSize[prnDocument.PrinterSettings.PaperSizes.Count+1] ;

          // PaperSize pkSize;
          // for (int i = 0; i < prnDocument.PrinterSettings.PaperSizes.Count; i++)
          // {
          //     if (prnDocument.PrinterSettings.PaperSizes[i].PaperName == "10x14")
          //     {
          //         pprid = i;
          //     }

          //     //pkSize = prnDocument.PrinterSettings.PaperSizes[i];
          //     //comboPaperSize[i]=pkSize;
          // }

          // // Create a PaperSize and specify the custom paper size through the constructor and add to combobox.
          // //PaperSize pkCustomSize1 = new PaperSize("First custom size", 1200, 1000);

          // //comboPaperSize[comboPaperSize.GetUpperBound(0)] = pkCustomSize1;
          // prnDocument.DefaultPageSettings.PaperSize =
          //prnDocument.PrinterSettings.PaperSizes[pprid];
           Left = (int)e.PageBounds.Left;
           Top = (int)e.PageBounds.Top;
           Right = (int)e.PageBounds.Right;
           Bottom = (int)e.PageBounds.Bottom;
           PageWidth = (int)e.PageBounds.Width;
           PageHeight = (int)e.PageBounds.Height;

           lineheight = Top;

           g = e.Graphics;
           PrintContent Prev = new PrintContent();


           // lineheight = CurrentY;
           HeaderPrintEventAgrs h = new HeaderPrintEventAgrs(this);
           if (prnHeader.Count == 0)
               OnStartPage(new object(), h);
           PrintHeader(g);


           for (int i = currentline; i < prn.Count; i++)
           {

               PrintContent p = ((PrintContent)prn[i]);
               int length = 0, maxchrperline = 0;
               string line;
               string content = p.Content;
               bool flag;
               float tempy = 0;
               maxchrperline = GetMaxCharPerline(p.Font, p.MaxWidth);
               line = ""; length = 0;
               flag = false;

               while (content.Length > 0)
               {
                   float tempx;

                   if (p.NewLine)
                   {
                       CurrentX = Left;
                       CurrentY = (int)lineheight;
                   }
                   tempx = CurrentX;
                   length = (content.Length > maxchrperline ? maxchrperline : content.Length);
                   if (p.MultipleLine == true)
                   {

                       line = content.Substring(0, length);
                       content = content.Substring(length);
                   }
                   else
                   {
                       line = content.Substring(0, length);
                       content = "";
                   }

                   if (p.PrintAlignment == PrintAlign.Center)

                       CurrentX = ((p.MaxWidth == 0 ? Right : 2 * CurrentX + p.MaxWidth) - (float)g.MeasureString(line, p.Font).Width) / 2f;

                   else if (p.PrintAlignment == PrintAlign.Right)
                       CurrentX = ((p.MaxWidth == 0 ? Right : CurrentX + p.MaxWidth) - (float)g.MeasureString(line, p.Font).Width);
                   else
                   {
                       if (p.NewLine)
                       {
                           CurrentX = Left;
                       }

                   }

                   if (!p.NewLine && !flag)
                   {
                       CurrentY -= (int)g.MeasureString(Prev.Content, Prev.Font).Height - ((int)g.MeasureString(Prev.Content, Prev.Font).Height - (int)g.MeasureString(line, p.Font).Height);
                   }
                   if (content.Length > 0)
                   {
                       flag = true;
                       tempy = CurrentY;
                   }
                   else
                   {
                       flag = false;

                   }
                   g.DrawString(line, p.Font, p.Brush, CurrentX, CurrentY);
                   //  CurrentX += (int)g.MeasureString(s, p.Font).Width;

                   CurrentY += (int)g.MeasureString(line, p.Font).Height;

                   if (p.MaxWidth > 0 && !flag)
                   {
                       CurrentX = tempx + p.MaxWidth;
                   }

                   else if (flag)
                   {
                       CurrentX = tempx;
                       lineheight = CurrentY;

                   }
                   else if (!p.NewLine)
                   {
                       CurrentX += (int)g.MeasureString(line, p.Font).Width;
                   }

                   if (CurrentY > lineheight)
                   {
                       lineheight = CurrentY;
                   }

               }
               Prev = p;
               if (tempy != 0)
                   CurrentY = tempy;

               //if (CurrentY > pa Bottom - Top)
               //{
               //    ////footer**********************
               //    //FooterPrintEventArgs f = new FooterPrintEventArgs(this);
               //    //if (prnFooter.Count == 0)
               //    //  OnEndPage(new object(), f);
               //    //printFooter(g);
               //    ////footer***********************

               //    currentline = i;
               //    pageno++;

               //    if (i < prn.Count)
               //    {
               //        e.HasMorePages = true;
               //        CurrentY = (int)g.MeasureString(content, p.Font).Height;
               //        CurrentY = Bottom + 28;
                     
               //        CurrentX = Right-20;

               //        g.DrawString((pageno - 1).ToString(), printFont, PrintBrush, CurrentX, CurrentY);
               //    }
               //    else
               //        e.HasMorePages = false;
               //    return;
               //    // PrintHeader(g);
               //}
           }
           if (e.HasMorePages == false)
           {
                //if (prnFooter.Count != 0)
               //  printFooter(g);
                CurrentX = Right;
                
                FooterPrintEventArgs f = new FooterPrintEventArgs(this);
                if (prnFooter.Count == 0)
                    OnEndPage(new object(), f);
                printFooter(g);
                CurrentX = Right;
                CurrentY = Bottom;
                g.DrawString((pageno).ToString(), printFont, PrintBrush, CurrentX-15, CurrentY+20);

           }
           //if (e.HasMorePages == true)
           //{
           //    //FooterPrintEventArgs f = new FooterPrintEventArgs(this);
           //    //if (prnFooter.Count == 0)
           //    //    OnEndPage(new object(), f);
           //    //printFooter(g);
           //}
          
           }
           
           
        public void PrintHeader(Graphics g)
        {
            PrintContent Prev = new PrintContent();
            lineheight = Top;
            CurrentY = Top;
            //
            foreach (PrintContent p in prnHeader)
            {

                int length = 0, maxchrperline = 0;
                string line;
                string content = p.Content;
                bool flag;
                float tempy = 0;
                maxchrperline = GetMaxCharPerline(p.Font, p.MaxWidth);
                line = ""; length = 0;
                flag = false;

                while (content.Length > 0)
                {
                    float tempx;

                    if (p.NewLine)
                    {
                        CurrentX = Left;
                        CurrentY = (int)lineheight;
                    }
                    tempx = CurrentX;
                    length = (content.Length > maxchrperline ? maxchrperline : content.Length);
                    if (p.MultipleLine == true)
                    {
                        line = content.Substring(0, length);
                        content = content.Substring(length);
                    }
                    else
                    {
                        line = content.Substring(0, length);
                        content = "";
                    }

                    if (p.PrintAlignment == PrintAlign.Center)

                        CurrentX = ((p.MaxWidth == 0 ? Right : 2 * CurrentX + p.MaxWidth) - (float)g.MeasureString(line, p.Font).Width) / 2f;

                    else if (p.PrintAlignment == PrintAlign.Right)
                        CurrentX = ((p.MaxWidth == 0 ? Right : CurrentX + p.MaxWidth) - (float)g.MeasureString(line, p.Font).Width);
                    else
                    {
                        if (p.NewLine)
                        {
                            CurrentX = Left;
                        }
                    }

                    if (!p.NewLine && !flag)
                    {
                        CurrentY -= (int)g.MeasureString(Prev.Content, Prev.Font).Height - ((int)g.MeasureString(Prev.Content, Prev.Font).Height - (int)g.MeasureString(line, p.Font).Height);
                    }
                    if (content.Length > 0)
                    {
                        flag = true;
                        tempy = CurrentY;
                    }
                    else
                    {
                        flag = false;

                    }
                    g.DrawString(line, p.Font, p.Brush, CurrentX, CurrentY);
                    //  CurrentX += (int)g.MeasureString(s, p.Font).Width;

                    CurrentY += (int)g.MeasureString(line, p.Font).Height;

                    if (p.MaxWidth > 0 && !flag)
                    {
                        CurrentX = tempx + p.MaxWidth;
                    }

                    else if (flag)
                    {
                        CurrentX = tempx;
                        lineheight = CurrentY;

                    }
                    else if (!p.NewLine)
                    {
                        CurrentX += (int)g.MeasureString(line, p.Font).Width;
                    }

                    if (CurrentY > lineheight)
                    {
                        lineheight = CurrentY;
                    }

                }
                Prev = p;
                if (tempy != 0)
                    CurrentY = tempy;
            }

        }
        public void printFooter(Graphics g)
        {
            PrintContent Prev = new PrintContent();
         //   int counter = 0;
            int amtperpage = Right;
            lineheight = Bottom;
            CurrentY = Bottom;
            CurrentX = Left;

            foreach (PrintContent p in prnFooter)
            {
                string content = p.Content;
                g.DrawString(content, p.Font, p.Brush, CurrentX, CurrentY);
                CurrentY += (int)g.MeasureString(content, p.Font).Height;
                lineheight = CurrentY;


            }
            //double pageCount = (double)prn.Count / (double)amtperpage;
            //int pageRequired = Convert.ToInt32(Math.Ceiling(pageCount));
           // foreach (PrintContent p in prnFooter)
           // {
               // string content = p.Content;
            //    double pageCount = (double)prn.Count / (double)amtperpage;
            //    int pageRequired = Convert.ToInt32(Math.Ceiling(pageCount));
            //    counter = 0;
            //    CurrentX = Right;
            //    CurrentY = Bottom;
                
            //for (int page = 1; page <= pageRequired; page++)
            //{
            //    g.DrawString(page.ToString(), printFont, PrintBrush, CurrentX - 10, CurrentY + 22);
            //    page++;
            //}
           // }
        }
            
        
    //    }
        private int GetMaxCharPerline(Font f, float MaxWidth = 0)
        {
            string s = "";
            SizeF size;
            float maxwidth;
            int numberOfCharacters = 0;
            if (MaxWidth == 0)
                maxwidth =  (Right -Left);
            else
                maxwidth = MaxWidth;
            while (true)
            {
                s += "a";
                size = g.MeasureString(s, f);
                if (size.Width > maxwidth)
                {
                    break;
                }
                numberOfCharacters++;
            }
            // numberOfCharacters will now contain your max string length
            return numberOfCharacters;
        }


        public DialogResult ShowPrintDialoug()
        {


            return prnDialog.ShowDialog();
        }

        public void PrintString(string line, int maxwidth, bool multipleline = true)
        {

            if (line.Length == 0)
                return;

            Font f = new Font(FontName, FontSize, FontStyle);

            if (ToHeader)
            {
                prnHeader.Add(new PrintContent(line, f, PrintBrush, PrintAlignment, NewLine, maxwidth, multipleline));
            }
            //footer
            else if(ToFooter)
            {
                prnFooter.Add(new PrintContent(line, f, PrintBrush, PrintAlignment, NewLine, maxwidth, multipleline));   
            }


           //footer
            else 
            {

                prn.Add(new PrintContent(line, f, PrintBrush, PrintAlignment, NewLine, maxwidth, multipleline));

            }

        }


        public void PrintString(string line, int maxwidth)
        {
            PrintString(line, maxwidth, true);
        }
        public void PrintString(string line)
        {
            PrintString(line, 0, true);
        }
        public void PrintString(string line, bool multipleline)
        {
            PrintString(line, 0, multipleline);

        }
        public void PrintLine(char chr, int Nos)
        {
            PrintString(new string(chr, Nos),false);
        }

        public void Drawline()
        {
        }
        public bool Print()
        {
            if (prnDialog.ShowDialog() == DialogResult.OK)
            {
                prnDocument.PrinterSettings = prnDialog.PrinterSettings;
                prnDocument.Print();
                return true;
            }
            else
            {
                return false;
            }
        }
         internal class PrintContent
        {


            public Font Font { get; set; }
            public Brush Brush { get; set; }
            public string Content { get; set; }
            public PrintAlign PrintAlignment { get; set; }
            public bool NewLine { get; set; }
            [DefaultValue(0)]
            public int MaxWidth { get; set; }
            [DefaultValue(false)]
            public bool MultipleLine { get; set; }


            public PrintContent()
            {
            }
            public PrintContent(string content, Font font, Brush brush, PrintAlign align, bool newline, int maxWidth = 0, bool multipleLine = true)
            {
                Content = content;
                Brush = brush;
                Font = font;
                PrintAlignment = align;
                NewLine = newline;
                MaxWidth = maxWidth;
                MultipleLine = multipleLine;
            }
        }

        //class net page event
        //class AllnextPage
        //{
        //    public delegate void NextPage(object sender,EventArgs e);
        //    public event NextPage page;

        //    public void page1()
        //    {

        //    }
           
        //}


    }

    public enum PrintAlign
    {
        Left = 1,
        Right = 2,
        Center = 3

    }

    public  class HeaderPrintEventAgrs : EventArgs
    {
        public Printer p { get; set; }

        public HeaderPrintEventAgrs(Printer pr)
        {
            p = pr;
        }
    }
    public class FooterPrintEventArgs :EventArgs 
    {
        public Printer p { get; set; }

        public FooterPrintEventArgs(Printer pf)
        {
            p = pf;
        }
       
        
    }
 
}
