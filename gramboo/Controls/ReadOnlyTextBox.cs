using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Gramboo.Controls
{
   public  class ReadOnlyTextBox : Control 
    {
        StringFormat format;
        #region Designer Generated Code
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {

            }
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        
        #endregion
        #endregion
        public ReadOnlyTextBox()
        {
            InitializeComponent();

            format = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox | StringFormatFlags.MeasureTrailingSpaces);
            format.LineAlignment = StringAlignment.Center;

            this.Height = 10;
            this.Width = 10;
            DataPropertyName = "None";
            this.Padding = new Padding(2);
        }

        public ReadOnlyTextBox(IContainer container)
        {
            container.Add(this);
            InitializeComponent();

            this.TextChanged += new EventHandler(ReadOnlyTextBox_TextChanged);
        }

        private void ReadOnlyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(formatString) && !string.IsNullOrEmpty(Text))
            {
                Text = string.Format(formatString, Text);
            }
        }

        private Color borderColor = Color.Black;

        private bool isSummary;
        public bool IsSummary
        {
            get { return isSummary; }
            set { isSummary = value; }
        }

        

        private bool isLastColumn;
        public bool IsLastColumn
        {
            get { return isLastColumn; }
            set { isLastColumn = value; }
        }

        private string formatString;
        public string FormatString
        {
            get { return formatString; }
            set { formatString = value; }
        }

       [DefaultValue("None")]
        public string DataPropertyName { get; set; }

        private HorizontalAlignment textAlign = HorizontalAlignment.Left;
        [DefaultValue(HorizontalAlignment.Left)]
        public HorizontalAlignment TextAlign
        {
            get { return textAlign; }
            set
            {
                textAlign = value;
                setFormatFlags();
            }
        }

        private StringTrimming trimming = StringTrimming.None;
        [DefaultValue(StringTrimming.None)]
        public StringTrimming Trimming
        {
            get { return trimming; }
            set
            {
                trimming = value;
                setFormatFlags();
            }
        }

        private void setFormatFlags()
        {
            format.Alignment = TextHelper.TranslateAligment(TextAlign);
            format.Trimming = trimming;
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int subWidth = 0;
            Rectangle textBounds;
            string displayText = Text ?? "0";

            // ✅ FIX: Format the text if both format and text are available
            try
            {
                if (!string.IsNullOrEmpty(formatString) && !string.IsNullOrEmpty(Text))
                {
                    displayText = String.Format("{0:" + formatString + "}", Convert.ToDecimal(Text));
                }
            }
            catch
            {
                // If formatting fails, use original text
                displayText = Text ?? "0";
            }

            textBounds = new Rectangle(this.ClientRectangle.X + 2, this.ClientRectangle.Y + 2, this.ClientRectangle.Width - 2, this.ClientRectangle.Height - 2);
            using (Pen pen = new Pen(borderColor))
            {
                if (isLastColumn)
                    subWidth = 1;

                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);
                e.Graphics.DrawRectangle(pen, this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - subWidth, this.ClientRectangle.Height - 1);
                // ✅ FIX: Always draw the text, even if empty
                e.Graphics.DrawString(displayText, Font, Brushes.Black, textBounds, format);
            }
        }
    }
}
