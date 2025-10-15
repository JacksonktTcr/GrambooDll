
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;
using System.Windows.Forms;



namespace Gramboo.Classes
{
	class ServerConfigHandler : ConfigurationSection

	{

		private ServerConfigHandler() { }

		#region Public Methods

		///<summary>
		///Get this configuration set from the application's default config file
		///</summary>
		public static ServerConfigHandler Open()
		{
			System.Reflection.Assembly assy = System.Reflection.Assembly.GetEntryAssembly();
			return Open(assy.Location);
		}

		///<summary>
		/// Get this configuration set from a specific config file
		///</summary>
		public static ServerConfigHandler Open(string path)
		{
			if ((object)instance == null)
			{
				if (path.EndsWith(".config", StringComparison.InvariantCultureIgnoreCase))
					spath = path.Remove(path.Length - 7);
				else
					spath = path;
				Configuration config = ConfigurationManager.OpenExeConfiguration(spath);
				if (config.Sections["ServerConfigHandler"] == null)
				{
					instance = new ServerConfigHandler();
					config.Sections.Add("ServerConfigHandler", instance);
					config.Save(ConfigurationSaveMode.Modified);
				}
				else
					instance = (ServerConfigHandler)config.Sections["ServerConfigHandler"];
			}
			return instance;
		}

		///<summary>
		///Create a full copy of the current properties
		///</summary>
		public ServerConfigHandler Copy()
		{
			ServerConfigHandler copy = new ServerConfigHandler();
			string xml = SerializeSection(this, "ServerConfigHandler", ConfigurationSaveMode.Full);
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
			ServerConfigHandler section = (ServerConfigHandler)config.Sections["ServerConfigHandler"];
			//
			// TODO: Add code to copy all properties from "this" to "section"
			//
			section.RemoteOnly = this.RemoteOnly;
			section.ServerProperties = this.ServerProperties;
			config.Save(ConfigurationSaveMode.Full); //Try with "Modified" to see the difference
		}

		#endregion Public Methods

		#region Properties

		public static ServerConfigHandler Default
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

		[ConfigurationProperty("ServerProperties")]
		public ServerProperties ServerProperties
		{
			get
			{
				return (ServerProperties)this["ServerProperties"];
			}
			set
			{
				this["ServerProperties"] = value;
			}
		}
		#endregion Properties

		#region Fields
		private static string spath;
		private static ServerConfigHandler instance = null;
		private static readonly ServerConfigHandler defaultInstance = new ServerConfigHandler();
		#endregion Fields
	}
	public class ServerProperties : ConfigurationElement
	{

		public override bool IsReadOnly()
		{
			return false;
		}
		//private string Cipher = "xzxcsdaeqwzsvgfsdgASDGFX";

		[ConfigurationProperty("DbAServerName", DefaultValue = @"103.94.139.84", IsRequired = true)]
		public string DbAServerName
		{
			get
			{
				return (string)this["DbAServerName"];
			}
			set
			{
				this["DbAServerName"] = value;
			}
		}
		[ConfigurationProperty("DbBServerName", DefaultValue = @"103.94.139.84", IsRequired = true)]
		public string DbBServerName
		{
			get
			{
				return (string)this["DbBServerName"];
			}
			set
			{
				this["DbBServerName"] = value;
			}
		}
		[ConfigurationProperty("DbLAServerName", DefaultValue = @"103.94.139.84", IsRequired = true)]
		public string DbLAServerName
		{
			get
			{
				return (string)this["DbLAServerName"];
			}
			set
			{
				this["DbLAServerName"] = value;
			}
		}

		[ConfigurationProperty("DbADBName", DefaultValue = "DbADBName", IsRequired = true)]
		public string DbADBName
		{
			get
			{
				return (string)this["DbADBName"];
			}
			set
			{
				this["DbADBName"] = value;
			}
		}


		[ConfigurationProperty("DbLADBName", DefaultValue = "DbLADBName", IsRequired = true)]
		public string DbLADBName
		{
			get
			{
				return (string)this["DbLADBName"];
			}
			set
			{
				this["DbLADBName"] = value;
			}
		}


		[ConfigurationProperty("DbBDBName", DefaultValue = "DbBDBName", IsRequired = true)]
		public string DbBDBName
		{
			get
			{
				return (string)this["DbBDBName"];
			}
			set
			{
				this["DbBDBName"] = value;
			}
		}


		[ConfigurationProperty("DbAPassword", DefaultValue = "P@SSW0RD", IsRequired = true)]
		public string DbAPassword
		{
			get
			{
				return (string)this["DbAPassword"];
			}
			set
			{
				this["DbAPassword"] = value;
			}
		}

		[ConfigurationProperty("DbLPassword", DefaultValue = "P@SSW0RD", IsRequired = true)]
		public string DbLPassword
		{
			get
			{
				return (string)this["DbLPassword"];
			}
			set
			{
				this["DbLPassword"] = value;
			}
		}

		[ConfigurationProperty("DbBPassword", DefaultValue = "P@SSW0RD", IsRequired = true)]
		public string DbBPassword
		{
			get
			{
				return (string)this["DbBPassword"];
			}
			set
			{
				this["DbBPassword"] = value;
			}
		}

	}
}
