using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gramboo.Controls
{
    public partial class DataGridPrintOptions : Form
    {
        #region Private Variables

        PrintOption printOptions=null;

        #endregion Private Variables

        #region Properties

        public PrintOption PrintOptions 
        {
            set
            {
                printOptions = value;
                if (printOptions != null)
                {
                    LoadPrintOptions(printOptions);
                }
            }
        }

        #endregion Properties

        #region Constructors

        public DataGridPrintOptions(PrintOption printOption)
        {
            InitializeComponent();
            this.PrintOptions = printOption;
        }

        #endregion Constructors

        #region Events

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (printOptions != null)
            {
                SavePrintOptions(printOptions);
                this.DialogResult = DialogResult.OK;
            }
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void lnkTitleFont_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                this.rTxtTitleText.Font = fontDialog1.Font;
            }
        }

        private void lblLineColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.lblLineColor.BackColor = colorDialog1.Color;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadPrintOptions(this.printOptions);
        }

        #endregion Events

        #region Methods

        private void LoadPrintOptions(PrintOption printOptn)
        {
            rTxtTitleText.Text = printOptions.TitleText;
            rTxtTitleText.Font = printOptions.TitleFont;
            lblLineColor.BackColor = printOptions.LinePen.Color;
            chkFitToWidth.Checked = printOptions.FitPrintPage;
            chkGridLine.Checked = printOptions.DrawGridLine;
            chkHLine.Checked = printOptions.DrawHorizontalLine;
            chkVLine.Checked = printOptions.DrawVerticalGridLine;

            chkIstCln.Items.Clear();

            foreach (DataGridViewColumn cln in printOptn.GridColumns)
            {
                if (cln.Visible)
                {
                    chkIstCln.Items.Add(new GridColum() { Column = cln },true);

                }
            }

        }

        private void SavePrintOptions(PrintOption printOptn)
        {
            printOptions.TitleText = rTxtTitleText.Text.Trim();
            printOptions.TitleFont = rTxtTitleText.Font;
            printOptions.LinePen.Color = lblLineColor.BackColor;
            printOptions.FitPrintPage = chkFitToWidth.Checked;
            printOptions.DrawGridLine = chkGridLine.Checked;
            printOptions.DrawHorizontalLine = chkHLine.Checked;
            printOptions.DrawVerticalGridLine=chkVLine.Checked;

            printOptions.SelectedColumns.Clear();
            foreach (GridColum cln in chkIstCln.CheckedItems)
            {
                printOptions.SelectedColumns.Add(cln.Column);
            }
        }

        private class GridColum
        {
            public DataGridViewColumn Column { get; set; }

            public override string ToString()
            {
                return Column.HeaderText;
            }
        }

        #endregion Methods

    }
}
