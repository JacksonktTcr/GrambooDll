using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Gramboo.Database
{
    public class Table
    {
       
        private List<string> _PrimaryKeys=new List<string>();
        private List<Table> _ChildTables = new List<Table>();
        private List<string> _NotUpdatables = new List<string>();

        public Table(string databasename,string ownername ,string name,bool isdatagridview=false,Table fillview=null  )
        {
            this.DatabaseName = databasename;
            this.OwnerName = ownername;
            this.Name = name;
            this.IsDatagridView = isdatagridview;
            _PrimaryKeys.Add("");
            FillView = fillview;
            _NotUpdatables.Add("Created_by");
            _NotUpdatables.Add("Created_date");

        }

        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string DatabaseName { get; set; }
        public Table FillView { get; set; }
        public Gramboo.Controls.GrbTextBox IdTextBox { get; set; }
        
        public bool  IsDatagridView { get; set; }
        public Gramboo.Controls.GrbDataGridView DatagridView
        {
            get;
            set;
        }
        public List<string>  PrimaryKeys
        {
            get
            {
                return _PrimaryKeys;
            }
            set
            {
                _PrimaryKeys = value;
            }
        }

        
        public List<string>  NotUpdatables
        {
            get
            {
                return _NotUpdatables;
            }
            set
            {
                _NotUpdatables = value;
            }
        }

        public List<Table > ChildTables
        {
            get
            {
                return _ChildTables;
            }
            set
            {
                _ChildTables = value;
            }
        }


        public string GetName()
        {
            return this.DatabaseName + "." + this.OwnerName + "." + this.Name;
        }

       

    }
}
