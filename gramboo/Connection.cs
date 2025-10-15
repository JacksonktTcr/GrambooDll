using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gramboo.Controls
{
    public class Connection
    {

        public string ServerName = "";
        public string DatabseName = "";
        public string DBUsername = "";
        public string DBPassword = "";

        MyConfigHandler configSection;


        public Connection()
        {
            ConnectionString = "";
          
        }
        
        /// <summary>
        /// Gets or Sets Connection string for database connection
        /// </summary>
        public string ConnectionString
        { get; set; }

        /// <summary>
        /// Reads Database Properties and decrypts details
        /// </summary>
        public void ReadDatabaseProperties()
        {
            try
            {

                configSection = MyConfigHandler.Open();

                ServerName = Cryptography.DecryptString(configSection.DBProperties.ServerName);
                DatabseName = Cryptography.DecryptString(configSection.DBProperties.DatabaseName);
                DBUsername = Cryptography.DecryptString(configSection.DBProperties.DBUserName);
                DBPassword = Cryptography.DecryptString(configSection.DBProperties.DBPassword);
            }
            catch (Exception ex)
            {
                General.ShowMessage(ex.Message);


            }


        }

        /// <summary>
        /// Reads Database Properties and decrypts details
        /// </summary>
        public bool WriteDatabaseProperties(string ServerName, string DatabaseName, string DBUserName, string DBPassword,bool hidemessage=true)
        {

            try
            {


                configSection = MyConfigHandler.Open();
                configSection.DBProperties.ServerName = Cryptography.EncryptString(ServerName);
                configSection.DBProperties.DatabaseName = Cryptography.EncryptString(DatabaseName);
                configSection.DBProperties.DBUserName = Cryptography.EncryptString(DBUserName);
                configSection.DBProperties.DBPassword = Cryptography.EncryptString(DBPassword);
                configSection.Save();
                if(!hidemessage)
                General.ShowMessage("Database Configured Successfully","Database");

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Message: " + ex.Message);
            }

        }



        /// <summary>
        /// Generate Connection string for sql server database
        /// </summary>
        public string GenerateSQLConnectionString(string Server_Name = "", string Databse_Name = "", string DB_Username = "", string DB_Password = "", bool Integrated_Security = false)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    string _ConnectionString;



                    if (ServerName.Trim().Length == 0 || DatabseName.Trim().Length == 0)
                    {
                        ReadDatabaseProperties();
                        if (ServerName.Trim().Length == 0 || DatabseName.Trim().Length == 0)
                        {
                            if (frmDatabaseSettings.Show(ref ServerName, ref DatabseName, ref  DBUsername, ref DBPassword ) == DialogResult.Cancel)
                            {
                                Application.Exit();
                            }
                            else
                            {
                                this.WriteDatabaseProperties(ServerName, DatabseName, DBUsername, DBPassword);

                            }
                        }
                    }

                    _ConnectionString = "Data Source=" + ServerName + ";Initial Catalog=" + DatabseName + ";Persist Security Info=True;User ID=" + DBUsername + ";Password=" + DBPassword + ";Integrated Security=" + Integrated_Security.ToString() +";Connection Timeout=100";

         
                    cn.ConnectionString = _ConnectionString;
                     
                    try
                    {
                        if (cn.State == ConnectionState.Open || cn.State == ConnectionState.Broken)
                            cn.Close();
                        cn.Open();
                         this.ConnectionString = _ConnectionString;
                    }
                    catch (SqlException ex)
                    {
                        General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
                        //if (ServerName.Trim().Length == 0 || DatabseName.Trim().Length == 0)
                        //{
                        //    ReadDatabaseProperties();
                        //    if (ServerName.Trim().Length == 0 || DatabseName.Trim().Length == 0)
                        //    {
                                if (frmDatabaseSettings.Show(ref ServerName, ref DatabseName, ref  DBUsername, ref DBPassword) == DialogResult.Cancel)
                                {
                                    Application.Exit();
                                }
                                else
                                {
                                    this.WriteDatabaseProperties(ServerName, DatabseName, DBUsername, DBPassword);
                                    GenerateSQLConnectionString();
                                }
                        //    }
                        //}
                       
                        //throw new Exception("New Wxcep");
                                this.ConnectionString = "";
                    }

                    return _ConnectionString;
                }
            }
            catch (Exception ex)

            {
                General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
                return "";
            }
        }


        
    }
}
