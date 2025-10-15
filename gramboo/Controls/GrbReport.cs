using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gramboo.Controls
{
    public partial class GrbReport : GrbForm
    {
        public GrbReport()
        {
            InitializeComponent();
            Copies = 1;
        }

        public GrbReport(ReportDocument cr)
        {
            InitializeComponent();
            CrystalReportDocumnet = cr;
            cr.SetDatabaseLogon(DBConn.ConnectionProperties.DBUsername, DBConn.ConnectionProperties.DBPassword,DBConn.ConnectionProperties.ServerName,DBConn.ConnectionProperties.DatabseName ,false );
            
            this.crystalReportViewer1.ReportSource = cr;
            
        }

        public ReportDocument CrystalReportDocumnet { get; set; }
        [DefaultValue(1)]
        public int Copies { get; set; }


        private void GrbReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CrystalReportDocumnet.PrintToPrinter(Copies, false, 0, 0);
                this.Close();
                this.Dispose();

            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }

        }

        private void GrbReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(CrystalReportDocumnet!=null)
            {
                CrystalReportDocumnet.Close();
                CrystalReportDocumnet.Dispose();
            }

        }
    }
}
