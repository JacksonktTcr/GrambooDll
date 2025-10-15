using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Windows.Forms;
 


namespace Gramboo
{
	class MyConfigHandler :  ConfigurationSection

	{
		
		private  MyConfigHandler() { }

		#region Public Methods

		///<summary>
		///Get this configuration set from the application's default config file
		///</summary>
		public static MyConfigHandler Open()
		{
			System.Reflection.Assembly assy = System.Reflection.Assembly.GetEntryAssembly();
			return Open(assy.Location);
		}

		///<summary>
		/// Get this configuration set from a specific config file
		///</summary>
		public static MyConfigHandler Open(string path)
		{
			if ((object)instance == null)
			{
				if (path.EndsWith(".config", StringComparison.InvariantCultureIgnoreCase))
					spath = path.Remove(path.Length - 7);
				else
					spath = path;
				Configuration config = ConfigurationManager.OpenExeConfiguration(spath);
				if (config.Sections["MyConfigHandler"] == null)
				{
					instance = new MyConfigHandler();
					config.Sections.Add("MyConfigHandler", instance);
					config.Save(ConfigurationSaveMode.Modified);
				}
				else
					instance = (MyConfigHandler)config.Sections["MyConfigHandler"];
			}
			return instance;
		}

		///<summary>
		///Create a full copy of the current properties
		///</summary>
		public MyConfigHandler Copy()
		{
			MyConfigHandler copy = new MyConfigHandler();
			string xml = SerializeSection(this, "MyConfigHandler", ConfigurationSaveMode.Full);
			System.Xml.XmlReader rdr = new System.Xml.XmlTextReader(new System.IO.StringReader(xml));
			copy.DeserializeSection(rdr);
			return copy;
		}

		///<summary>
		///Save the current property values to the config file
		///</summary>
		public void Save()
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(spath);
			MyConfigHandler section = (MyConfigHandler)config.Sections["MyConfigHandler"];
			//
			// TODO: Add code to copy all properties from "this" to "section"
			//
			section.RemoteOnly = this.RemoteOnly;
			section.DBProperties = this.DBProperties;
			config.Save(ConfigurationSaveMode.Full); //Try with "Modified" to see the difference
		}

		#endregion Public Methods

		#region Properties

		public static MyConfigHandler Default
		{
			get { return defaultInstance; }
		}
	public override bool IsReadOnly()
	{
		return false;
	}
 // Create a "remoteOnly" attribute.
		[ConfigurationProperty("remoteOnly", DefaultValue = "false", IsRequired = false)]
		public Boolean RemoteOnly
		{
			get
			{ 
				return (Boolean)this["remoteOnly"]; 
			}
			set
			{ 
				this["remoteOnly"] = value; 
			}
		}

		[ConfigurationProperty("DBProperties")]
		public DatabaseProperties DBProperties
		{
			get
			{
				return (DatabaseProperties)this["DBProperties"];
			}
			set
			{
				this["DBProperties"] = value;
			}
		}
		#endregion Properties

		#region Fields
		private static string spath;
		private static MyConfigHandler instance = null;
		private static readonly MyConfigHandler defaultInstance = new MyConfigHandler();
		#endregion Fields
}
public class  DatabaseProperties : ConfigurationElement
{
 
	public override bool IsReadOnly()
	{
		return false;
	}
	//private string Cipher = "xzxcsdaeqwzsvgfsdgASDGFX";
		
		[ConfigurationProperty("ServerName", DefaultValue = @".\SQLEXPRESS", IsRequired = true )]
		public string ServerName
		{
			get
			{
				return (string)this["ServerName"] ; 
			}
			set
			{
				this["ServerName"] =  value ; 
			}
		}

		 [ConfigurationProperty("DatabaseName", DefaultValue = "DataBase", IsRequired = true )]
		public string DatabaseName
		{
			get
			{
				return (string)this["DatabaseName"] ; 
			}
			set
			{
				this["DatabaseName"] = value  ; 
			}
		}


		  [ConfigurationProperty("DBUserName", DefaultValue = "sa", IsRequired = true )]
		 public string DBUserName
		{
			get
			{
				return (string)this["DBUserName"] ; 
			}
			set
			{
				this["DBUserName"] = value ; ; 
			}
		}
			  [ConfigurationProperty("DBPassword", DefaultValue = "P@SSW0RD", IsRequired = true )]
		  public string DBPassword
		{
			get
			{
				return (string)this["DBPassword"]; 
			}
			set
			{
				this["DBPassword"] = value ; 
			}
		}
 
	 
	}
}
