using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace REGAL.Classes
{
   public class dgvAccounts : Gramboo.Controls.GrbDataGridView
    {
       private List<string> _mergecolumns;
       private bool flag = false;
        public dgvAccounts()
        {
            this.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.FormatString = "F02";
            //this.DefaultCellStyle.Font = new System.Drawing.Font(this.Font.Name, 10);
             
            _mergecolumns=   new List<string>();
        }

       public List<string > MergeColumns {

           get 
           {
               return _mergecolumns;
           }
           set
           {

               _mergecolumns = value;
               MergeColumn();
           }
       }

       private void MergeColumn()
       {
           flag = true;
           if (_mergecolumns.Count > 0)
           {
               foreach (DataGridViewRow dr in this.Rows)
            {
                   bool MFlag=true ;
                if (dr.Index > 0)
                {
                    foreach (string c in _mergecolumns)
                    {
                        if (this.Rows[dr.Index ].Cells[c].Value.ToString() != this.Rows[dr.Index - 1].Cells[c].Value.ToString())
                        {
                            MFlag = false;
                        }

                    }
                    if (MFlag)
                    {
                        foreach (string c in _mergecolumns)
                        {
                            dr.Cells[c].Value = "";

                        }
                    }
                }
            }
           }
           flag = false;
       }

        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);
            if (flag)
                return;

            MergeColumn();
            foreach (DataGridViewColumn c in this.Columns)
            {
                c.SortMode = DataGridViewColumnSortMode.Programmatic; 
            }
        }


        //protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        //{
        //    base.OnCellDoubleClick(e);

        //    if(this.Rows[

        //}
    }
}
