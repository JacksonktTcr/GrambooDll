using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeCreator
{
    class FontSizeConverter : System.ComponentModel.TypeConverter
    {

         private ArrayList values;
         public FontSizeConverter()
        {
            // Initializes the standard values list with defaults.
            values = new ArrayList(new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 72 });
        }




        public override bool
           GetStandardValuesSupported(
           ITypeDescriptorContext context)
        {
            //True - means show a Combobox
            //and False for show a Modal 
            return true;
        }

        public override bool
            GetStandardValuesExclusive(
            ITypeDescriptorContext context)
        {
            //False - a option to edit values 
            //and True - set values to state readonly
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context)
        {
            // Passes the local integer array.
            StandardValuesCollection svc =
                new StandardValuesCollection(values);
            return svc;
        }

        //public override StandardValuesCollection
        //    GetStandardValues(
        //    ITypeDescriptorContext context)
        //{

        //    //List<string> fonts = new List<string>();

        //    //foreach (FontFamily font in System.Drawing.FontFamily.Families)
        //    //{
        //    //    fonts.Add(font.Name);
        //    //}

        //    // string str = String.Join(",", fonts);

      

        //        return new StandardValuesCollection(
        //            new int[] { 8, 9, 10, 11,12,14,16,18,20,22,24,26,28,36,72 });
        //}

        public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
        }


            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if( value.GetType() == typeof(string) )
            {
                // Parses the string to get the integer to set to the property.
                int newVal = int.Parse((string)value);
            
                // Tests whether new integer is already in the list.
                if( !values.Contains(newVal) )
                {
                    // If the integer is not in list, adds it in order.
                    values.Add(newVal);
                    values.Sort();
                }                                
                // Returns the integer value to assign to the property.
                return newVal;
            }
            else
                return base.ConvertFrom(context, culture, value);
        }
    }
    
}

