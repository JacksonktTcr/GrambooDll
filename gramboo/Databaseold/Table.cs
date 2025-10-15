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
        private Row r;
        
        public Table()
        {

        }

        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public  List<Column> Columns { get; set; }

        public List<Table> Children { get; set; }

        public List<Column> PrimaryKey { get; set; }

        public List<Row> Rows { get; set; }
        

        public virtual bool  Validate()
        {
          
            try
            {
                using (SqlConnection cn = new SqlConnection(this.ConnectionString))
                {
                    cn.Open();

                    foreach (Column c in this.Columns )
                    {
                        //r.i
                    }

                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
         }


    }


}
