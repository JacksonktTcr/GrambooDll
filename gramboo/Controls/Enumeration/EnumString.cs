using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Gramboo.Controls.Enumeration
{
    public class StringValue : System.Attribute
    {
        private string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

    }
    public class ValidationMessage: System.Attribute
    {
        private string _value;

        public ValidationMessage(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

    }
    public static class StringEnum
    {
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            //Check first in our cached results...

            //Look for our 'StringValueAttribute' 

            //in the field's custom attributes

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs =
               fi.GetCustomAttributes(typeof(StringValue),
                                       false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
        public static string GetValidationMessage(Enum value)
        {
            string output = null;
            Type type = value.GetType();
 

            FieldInfo fi = type.GetField(value.ToString());
            ValidationMessage[] attrs =
               fi.GetCustomAttributes(typeof(ValidationMessage),
                                       false) as ValidationMessage[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }
}
