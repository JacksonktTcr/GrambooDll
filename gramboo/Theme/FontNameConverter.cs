using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThemeCreator
{
    public class FontNameConverter : StringConverter
    {

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

        public override StandardValuesCollection
            GetStandardValues(
            ITypeDescriptorContext context)
        {

            List<string> fonts = new List<string>();

            foreach (FontFamily font in System.Drawing.FontFamily.Families)
            {
                fonts.Add(font.Name);
            }



            
          // string str = String.Join(",", fonts);
            return new StandardValuesCollection(fonts.ToArray());


            //return new StandardValuesCollection(
            //    new string[] { "Algerian", "Arial", "Tahoma", "Century", "Baskerville Old Face", "Bauhaus 93", "Bell MT", "Berlin Sans FB", "Book Antiqua", "Bookshelf Symbol 7" });
        }

    }
}
