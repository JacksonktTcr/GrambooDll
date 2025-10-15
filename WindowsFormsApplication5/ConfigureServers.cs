 
using Gramboo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace REGAL.Classes
{
    public class ConfigureServers
    {

        public string 
            DbAServerName =""  ,
            DbAServerPassword = "",
            DbADBName = "",
            DbLAServerName = "",

           DbLAServerPassword = "",
            DbLADBName = "",
            DbBServerName = "",

            DbBServerPassword = "",
            DbBDBName = "";


        ServerConfigHandler configSection;


        public ConfigureServers()
        {
            ConfigureServersString = "";
          
        }
        
        /// <summary>
        /// Gets or Sets ConfigureServers string for database ConfigureServers
        /// </summary>
        public string ConfigureServersString
        { get; set; }

        /// <summary>
        /// Reads Database Properties and decrypts details
        /// </summary>
        public void ReadDatabaseProperties()
        {
            try
            {

                configSection = ServerConfigHandler.Open();

                DbAServerName = Cryptography.DecryptString(configSection.ServerProperties.DbAServerName);
                DbADBName = Cryptography.DecryptString(configSection.ServerProperties.DbADBName);
                DbLAServerName = Cryptography.DecryptString(configSection.ServerProperties.DbLAServerName);
                DbLADBName = Cryptography.DecryptString(configSection.ServerProperties.DbLADBName);
                DbBServerName = Cryptography.DecryptString(configSection.ServerProperties.DbBServerName);
                DbBDBName = Cryptography.DecryptString(configSection.ServerProperties.DbBDBName);
                DbAServerPassword = Cryptography.DecryptString(configSection.ServerProperties.DbAPassword);
                DbBServerPassword = Cryptography.DecryptString(configSection.ServerProperties.DbBPassword);
                DbLAServerPassword = Cryptography.DecryptString(configSection.ServerProperties.DbLAPassword);
            }
            catch (Exception ex)
            {
                General.ShowMessage(ex.Message);


            }


        }

        /// <summary>
        /// Reads Database Properties and decrypts details
        /// </summary>
        public bool WriteDatabaseProperties(string DbAServerName, string DbADBName, string DbLAServerName, string DbLADBName, string DbBServerName, string DbBDBName,string DbAServerPassword,string DbBServerPassword, string DbLAServerPassword)
        {

            try
            {

                //DbAServerName = Cryptography.DecryptString(configSection.ServerProperties.DbAServerName);
                //DbADBName = Cryptography.DecryptString(configSection.ServerProperties.DbADBName);
                //DbLAServerName = Cryptography.DecryptString(configSection.ServerProperties.DbLAServerName);
                //DbLADBName = Cryptography.DecryptString(configSection.ServerProperties.DbLADBName);
                //DbBServerName = Cryptography.DecryptString(configSection.ServerProperties.DbBServerName);
                //DbBDBName = Cryptography.DecryptString(configSection.ServerProperties.DbBDBName);

                configSection = ServerConfigHandler.Open();
                configSection.ServerProperties.DbAServerName = Cryptography.EncryptString(DbAServerName);
                configSection.ServerProperties.DbADBName = Cryptography.EncryptString(DbADBName);
                configSection.ServerProperties.DbLAServerName = Cryptography.EncryptString(DbLAServerName);
                configSection.ServerProperties.DbLADBName = Cryptography.EncryptString(DbLADBName);
                configSection.ServerProperties.DbBServerName = Cryptography.EncryptString(DbBServerName);
                configSection.ServerProperties.DbBDBName = Cryptography.EncryptString(DbBDBName);
                configSection.ServerProperties.DbAPassword = Cryptography.EncryptString(DbAServerPassword);
                configSection.ServerProperties.DbBPassword = Cryptography.EncryptString(DbBServerPassword);
                configSection.ServerProperties.DbLAPassword = Cryptography.EncryptString(DbLAServerPassword);

                configSection.Save();
                General.ShowMessage("Database Configured Successfully","Database");

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Message: " + ex.Message);
            }

        }



        /// <summary>
        /// Generate ConfigureServers string for sql server database
        /// </summary>
        public string GenerateSQLConfigureServersString(string Server_Name = "", string Databse_Name = "", string DB_Username = "", string DB_Password = "", bool Integrated_Security = false,string Location="L")
        {
            try
            {
                using (SqlConnection cn = new SqlConnection())
                {
                    string _ConfigureServersString;



                    if (DbLAServerName.Trim().Length == 0 || DbLADBName.Trim().Length == 0)
                    {
                        ReadDatabaseProperties();
                        if (DbLAServerName.Trim().Length == 0 || DbLADBName.Trim().Length == 0)
                        {
                            if (frmServerSetting.Show(ref DbAServerName, ref  DbADBName, ref DbLAServerName, ref DbLADBName, ref DbBServerName, ref DbBDBName, ref DbLAServerPassword,ref DbAServerPassword,ref DbBServerPassword) == DialogResult.Cancel)
                            {
                                Application.Exit();
                            }
                            else
                            {
                                this.WriteDatabaseProperties(DbAServerName, DbADBName, DbLAServerName, DbLADBName, DbBServerName, DbBDBName, DbAServerPassword, DbBServerPassword, DbLAServerPassword); ;

                            }
                        }
                    }

                    _ConfigureServersString = "Data Source=" + DbLAServerName + ";Initial Catalog=" + DbLADBName + ";Persist Security Info=True;User ID=sa;Password="+DbLAServerPassword+";Integrated Security=" + Integrated_Security.ToString() + ";Connection Timeout=100";


                    cn.ConnectionString = _ConfigureServersString;

                    try
                    {
                        if (cn.State == ConnectionState.Open || cn.State == ConnectionState.Broken)
                            cn.Close();
                        cn.Open();
                        this.ConfigureServersString = _ConfigureServersString;
                    }
                    catch (SqlException ex)
                    {
                        General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
                        //if (DbLAServerName.Trim().Length == 0 || DbLADBName.Trim().Length == 0)
                        //{
                        //    ReadDatabaseProperties();
                        //    if (DbLAServerName.Trim().Length == 0 || DbLADBName.Trim().Length == 0)
                        //    {
                        if (frmServerSetting.Show(ref DbAServerName, ref  DbADBName, ref DbLAServerName, ref DbLADBName, ref DbBServerName, ref DbBDBName, ref DbLAServerPassword, ref DbAServerPassword,ref DbLAServerPassword) == DialogResult.Cancel)
                        {
                            Application.Exit();
                        }
                        else
                        {
                            this.WriteDatabaseProperties(DbAServerName, DbADBName, DbLAServerName, DbLADBName, DbBServerName, DbBDBName,DbAServerPassword,DbBServerPassword,DbLAServerPassword);
                            GenerateSQLConfigureServersString();
                        }
                        //    }
                        //}

                        //throw new Exception("New Wxcep");
                        this.ConfigureServersString = "";
                    }

                    return _ConfigureServersString;
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
