using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.ComponentModel;

namespace Gramboo.Database
{
    public class Column 
    {
        public event DefaulValueChangedEventHandler DefaultValueChanged;
        public delegate void DefaulValueChangedEventHandler(object sender, DefaultValueEventArgs e);

         public Column(string name,SqlDbType datatype, object defaultvalue=null )
        {
            this.Name = name;
            this.DataType = datatype;
            this.DefaultValue = defaultvalue;
            SetDefaultValue();
        }

         public string  Name { get; set; }

         [DbProviderSpecificTypePropertyAttribute(true)]
         public SqlDbType DataType { get; set; }

        [DefaultValue(false )]
        private bool PrimaryKey { get; set; }

        [DefaultValue(false)]
        private bool IsUnique { get; set; }

        [DefaultValue(true )]
        public  bool AllowNull { get; set; }

        [DefaultValue (false )]
        public bool IsAutoIncrement { get; set; }


         [DefaultValue(50)]
        public int Length { get; set; }

         [DefaultValue(2)]
        public int NumericPrecision { get; set; }

         [DefaultValue(null)]
         public object DefaultValue { 
             
             get
             {
                 return this.DefaultValue;
             }
             set
             {
                 try
                 {
                     this.DefaultValue = value;
                     if (DefaultValueChanged != null)
                     {
                         DefaultValueChanged(this, new DefaultValueEventArgs(this  ));
                     }

                 }
                 catch (Exception ex)
                 {
                     throw new Exception(ex.Message, ex);
                 }
             }
             
             }


         [DefaultValue(null)]
         public object Value { get; set; }

         private void SetDefaultValue()
         {
             if (Value == null)
             {
                 if (DefaultValue == null)
                 {
                     switch (this.DataType)
                     {
                         case SqlDbType.Bit:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.BigInt:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.Binary:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.Char:
                             this.DefaultValue = "-";
                             break;
                         case SqlDbType.Date:
                             this.DefaultValue = new DateTime(1111, 1, 1);
                             break;
                         case SqlDbType.DateTime:
                             this.DefaultValue = new DateTime(1111, 1, 1);
                             break;
                         case SqlDbType.DateTime2:
                             this.DefaultValue = new DateTime(1111, 1, 1);
                             break;
                         case SqlDbType.Decimal:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.Float:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.Image:
                             this.DefaultValue = null;
                             break;
                         case SqlDbType.Int:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.Money:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.NChar:
                             this.DefaultValue = "-";
                             break;
                         case SqlDbType.NText:
                             this.DefaultValue = "-";
                             break;
                         case SqlDbType.NVarChar:
                             this.DefaultValue = "-";
                             break;
                         case SqlDbType.Real:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.SmallDateTime:
                             this.DefaultValue = new DateTime(1111, 1, 1); ;
                             break;
                         case SqlDbType.SmallInt:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.SmallMoney:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.Structured:
                             this.DefaultValue = null;
                             break;
                         case SqlDbType.Text:
                             this.DefaultValue = "-";
                             break;
                         case SqlDbType.Time:
                             this.DefaultValue = new DateTime(1111, 1, 1, 0, 0, 0);
                             break;
                         case SqlDbType.TinyInt:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.UniqueIdentifier:
                             this.DefaultValue = Guid.NewGuid();
                             break;
                         case SqlDbType.VarBinary:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.VarChar:
                             this.DefaultValue = "-";
                             break;
                         case SqlDbType.Variant:
                             this.DefaultValue = 0;
                             break;
                         case SqlDbType.Xml:
                             this.DefaultValue = null;
                             break;
                         default:
                             this.DefaultValue = null;
                             break;
                     }

                 }
                 this.Value = this.DefaultValue;
             }
         }
    }
    

}
