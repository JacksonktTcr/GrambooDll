using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Gramboo.Database;
using System.Data.SqlClient;
using WeifenLuo.WinFormsUI.Docking;

namespace Gramboo.Controls
{

    public delegate void RefershDataEventHandler (object source, EventArgs  e);

    public partial class GrbGridReport : GrbForm 
    {
 
 
        
        bool flag;

        private List<string> _SummaryColumns = new List<string> { };
        private List<string> _Columns = new List<string> { };
        private List<string> _HiddenColumns = new List<string> { };
 
        public GrbGridReport()
        {

            InitializeComponent();
            this.TopLevel = false;
            ReportText = "View Report";
            criteria = "1=1";
            piccollpseorshow.BringToFront();
        }


        public Table FillTable { get; set; }

        [DefaultValue("1=1")]
        public string criteria { get; set; }

        

        public List<string> SummaryColumns
        {
            get
            {
                return _SummaryColumns;
            }
            set
            {
                _SummaryColumns = value;

                foreach (string str in _SummaryColumns)
                {
                    if(FieldList.Items.Contains(str))
                    {
                        FieldList.SetItemCheckState(FieldList.Items.IndexOf(str) , CheckState.Checked);
                    }
                }
            }
        }

        public List<string> Columns
        {
            get
            {
                return _Columns;
            }
            set
            {
                _Columns = value;

                FieldList.Items.Clear();
                foreach (string str in _Columns)
                {
                    FieldList.Items.Add(str);
                }
            }
        }

        public List<string> HiddenColumns
        {
            get
            {
                return _HiddenColumns;
            }
            set
            {
                _HiddenColumns = value;
                foreach (string str in _HiddenColumns)
                {
                    FieldList.Items.Remove (str);
                }
            }
        }



 

        /// <summary>
        /// Gets or Sets Text To dispaly
        /// </summary>
        [Browsable(true)]
        [Description("Gets or Sets Text To dispaly")]
        [DefaultValue( "View Report")]
        public string ReportText { get; set; }



       
        

         void dgvFilters_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (!flag)
            {
                List<string> filters = new List<string>();

                foreach (DataGridViewRow dr in dgvFilters.Rows)
                {
                    filters.Add(dr.Cells["Filter"].Value.ToString());
                }
                dgv.FilterList = filters;

                dgv.PerformFilter();
            }
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            flag = true ;
            if (dgv.FilterList.Count > 0)
            {
                using (DataTable dt = new DataTable())
                {
                    dt.Columns.Add("Filter", typeof(string));
                    foreach (string str in dgv.FilterList)
                    {
                        dt.Rows.Add(str);
                    }
                    dgvFilters.ShowDelete = true;
                    dt.AcceptChanges();

                    dgvFilters.DataSource = dt;
                    dgvFilters.Columns[0].Width = 200; 
                    
                }
            }
            flag = false ;
        }


        public override void Init()
        {
            LoadFlag = true;
            base.Init();
            LoadFlag = false;
        }

        public override void RefreshData()
        {
            base.RefreshData();
            FillGrid();
        }
        
      

        public void FillGrid( )
        {

            List<int> i=new List<int>();
            foreach (string str in _SummaryColumns)
            {
                string s1;
                s1 = str;
                s1 = s1.Replace("[", "");
                s1 = s1.Replace("]", "");
                s1 = s1.Replace(" ", "");

                foreach (string fldstr in FieldList.Items)
                {
                    string s;
                    s = fldstr;
                    s = s.Replace("[", "");
                    s = s.Replace("]", "");
                    s = s.Replace(" ", "");
                    if (s.ToUpper() == s1.ToUpper())
                    {
                        i.Add(FieldList.Items.IndexOf(fldstr));

                    }
                }
            }

            if (FieldList.CheckedItems.Count == 0)
            {
                foreach (int j in i)
                {
                    FieldList.SetItemCheckState(j, CheckState.Checked);
                }
            }
            if (FillTable != null)
            {
                dgv.DataFields = this.Columns;
                dgv.HiddenDataFields = this.HiddenColumns;
                dgv.SummaryColumns = this.SummaryColumns.ToArray();
                dgv.DataSource = null;
                dgv.DataSource = DBConn .GetData(new SqlCommand(GetSelectQuery()), FillTable.Name).Tables[0];
            }

        }

      

        private string GetSelectQuery()
        {
            string strList="";
            string strGroup="";
            string filter =  "";
            string s;

            foreach (string c in FieldList.CheckedItems)
            {
               
                s = c;
                s = s.Replace("[", "");
                s = s.Replace("]", "");

                if (!SummaryColumns.Contains(s))
                {
                    strGroup += (strGroup.Length == 0 ? "" : ",") + c;
                    strList += (strList.Length == 0 ? "" : ",") + c;


                }
                else
                {
             
                     if ( FieldList.CheckedItems.Contains(c))
                     {

                        strList += (strList.Length == 0 ? "" : ",") + "ISNULL(SUM(" + c + "),0) AS " + c;
                    }
                }

            }

            if (strGroup.Length == 0)
            {
               

                foreach (string c in FieldList.Items )
                {

                    s = c;
                    s = s.Replace("[", "");
                    s = s.Replace("]", "");
                    if (!SummaryColumns.Contains(s))
                    {
                        strGroup +=   (strGroup.Length == 0 ? "" : ",") +c ;

                    }

                }

                strList = strGroup +","+ strList;
            }


            foreach (string f in dgv.FilterList)
            {
                filter += " AND " + f;
            }

            return "SELECT " + strList + " FROM " + FillTable.GetName() + " WHERE " + (criteria.Length == 0 ? "1=1" :criteria ) + filter  + " GROUP BY "+ strGroup + " Order BY "+ strGroup  ;
        }

        private void piccollpseorshow_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed)
            {
                piccollpseorshow.Image = Gramboo.Properties.Resources.Collapse;
                splitContainer1.Panel2Collapsed = false;
                
            }
            else
            {
                piccollpseorshow.Image = Gramboo.Properties.Resources.Expand;
                splitContainer1.Panel2Collapsed = true;
            }
        }

       

      
    }

  
}
