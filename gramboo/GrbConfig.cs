using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Gramboo
{
   //public  class GRBConfig
   // {
   //     private GeneralConfig _login = new GeneralConfig();

   //     public  GRBConfig() { }

   //     #region Public Methods
         

   //     ///<summary>
   //     ///Save the current property values to the config file
   //     ///</summary>
   //     public void Save()
   //     {


   //         var path = System.Reflection.Assembly.GetEntryAssembly().Location;
   //         var name = System.IO.Path.GetFileName(path);

   //         XmlSerializer serializer = new XmlSerializer(typeof(GRBConfig));
   //         TextWriter textWriter = new StreamWriter(Path.GetTempPath() + name + ".xml");
   //         serializer.Serialize(textWriter, this);
   //         textWriter.Close();
   //     }

   //     #endregion Public Methods

       

   //     public static  GRBConfig Open()
   //     {

   //         var path = System.Reflection.Assembly.GetEntryAssembly().Location;
   //         var name = System.IO.Path.GetFileName(path);

   //         if (!File.Exists(Path.GetTempPath() + name+".xml"))
   //             return null ;
   //         XmlSerializer deserializer = new XmlSerializer(typeof(GRBConfig));
   //         TextReader textReader = new StreamReader(Path.GetTempPath() +name+ ".xml");
   //         GRBConfig GRBconfig;
   //         GRBconfig = (GRBConfig)deserializer.Deserialize(textReader);
   //         textReader.Close();

   //         return GRBconfig;
   //     }
   //     #region Properties
   //     public GeneralConfig Login
   //     {
   //         get
   //         {
   //             return _login;
   //         }
   //         set
   //         {
   //             _login = value;
   //         }
   //     }
   //     #endregion Properties

      
   // }

    public static class GeneralConfig  
    {


        public static string UserName { get; set; }


        public static int UserId { get; set; }


        public static int CompanyID { get; set; }

        
        public static int BranchId { get; set; }


        public static int CounterId { get; set; }

         [DefaultValue(false)]
        public static bool EnableAuditTrail { get; set; }

         [DefaultValue(false)]
         public static bool SaveQuery { get; set; }

        [DefaultValue(false)]
        public static bool AskForPasswordForChangeData { get; set; }


        [DefaultValue(1)]
        public static int DbId { get; set; }

    }
}

