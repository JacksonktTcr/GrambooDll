using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks; 
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ThemeCreator
{
    class Serializer
    {



        static public void Serialize(object t, string filePath)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, t);
            stream.Close();


        }
   


    }
}
