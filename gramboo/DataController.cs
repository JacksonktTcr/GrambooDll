using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms ;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections;
using Gramboo.Controls;

namespace Gramboo
{

    /*
     * 
    /// <summary>
    ///  Author : Jackson 
    ///  Date   : 08/Jun/2013
    ///  
    /// Caution : You are entered in a restricted area !
    /// 
    ///  This class defines all functions for windows forms for any database project that have been developed or developed in future.
    ///  
    ///  Warning : Extreme care should be taken before changing even a single line of code .
    ///  It may affect the whole behavior of the project.
    ///   
     /// </summary>
    /// 


    */

    public class DataController
    {
        /// <summary>
        /// Default constructor
        /// </summary>

        private Form _FormName;
        private Connection _ConnectionProperties = new Connection();
        private Connection _MirrorConnectionProperties = new Connection();
        private Connection _MirrorConnectionProperties2 = new Connection();


        public DataController()
        {
            _FormName = null;
           
        }

        /// <summary>
        /// Constructor that takes form name as argument
        /// </summary>
        /// <param name="FormName"> Form Name where the object is created </param>
        public DataController(Form FormName)
        {
            _FormName = FormName;
        }

        /// <summary>
        /// Constructor that takes form name as argument
        /// </summary>
        /// <param name="FormName"> Form Name where the object is created </param>
        public DataController(GrbForm FormName)
        {
            _FormName = FormName;
        }

        public Dictionary<string, object> ApplicationSettings { get; set; }

        ///// <summary>
        ///// This function initializes the form
        ///// 
        ///// </summary>
        //public void init()
        //{
        //    General.ClearControls(_FormName);
        //}


        /// <summary>
        /// Returns database connetion properties
        /// </summary>
        public  Connection ConnectionProperties
        {
            get
            {
                return _ConnectionProperties;
            }
        }
        [DefaultValue(false)]
        public bool HasMirrorDB { get; set; }
        /// <summary>
        /// Returns  Mirror database connetion properties
        /// </summary>
        public Connection MirrorConnectionProperties
        {
            get
            {
                return _MirrorConnectionProperties;
            }
        }

        [DefaultValue(false)]
        public bool HasMirrorDB2 { get; set; }
        /// <summary>
        /// Returns  Mirror database connetion properties
        /// </summary>
        public Connection MirrorConnectionProperties2
        {
            get
            {
                return _MirrorConnectionProperties2;
            }
        }


        ///// <summary>
        ///// Opens Connection to database
        ///// </summary>
        ///// <param name="ConnectionString"> Sql Connection String </param>
        ///// <returns>Returns True if connection created otherwise false</returns>
        //    public bool  CheckConnection(string ConnectionString="")
        //    {
        //        using (SqlConnection _Cn = new SqlConnection())
        //        {
        //            try
        //            {


        //                if (ConnectionString == "")
        //                {


        //                    _Cn.ConnectionString = GenerateSQLConnectionString();
        //                }
        //                else
        //                {
        //                    _Cn.ConnectionString = ConnectionString;
        //                }
        //                _Cn.Open();
        //            }
        //            catch (SqlException ex)
        //            {
        //                General.ShowMessage("Unable To Connect Database \n" + ex.Message.ToString(), Application.ProductName, MessageBoxIcon.Error, MessageBoxButtons.OK);
        //                frmDatabaseSettings frm = new frmDatabaseSettings();
        //                frm.Show();
        //                return false;
        //            }
        //            catch (Exception ex)
        //            {
        //                General.ShowMessage("Unable To Connect Database \n" + ex.Message.ToString(), Application.ProductName, MessageBoxIcon.Error, MessageBoxButtons.OK);
        //                frmDatabaseSettings frm = new frmDatabaseSettings();
        //                frm.Show();
        //                return false;
        //            }

        //            return true;
        //        }
        //    }



    

        public DataSet GetData(SqlCommand cmd, string TableName = "Table",string ConnectionString ="" )
        {

            string SqlString="";
            using (SqlConnection cn = new SqlConnection())
            {
                try
                {

                    if (this._ConnectionProperties.ConnectionString != "")

                        cn.ConnectionString = this._ConnectionProperties.ConnectionString;
                    else if (ConnectionString == "")
                        cn.ConnectionString = _ConnectionProperties.GenerateSQLConnectionString();
                    else
                        cn.ConnectionString = ConnectionString;
                    if (cn.ConnectionString.Trim().Length==0)
                    {
                        
                        return null;
                    }
                    if (cn.State == ConnectionState.Open || cn.State == ConnectionState.Broken)
                        cn.Close();
                     
                    cn.Open();
                    
                    SqlDataAdapter MyDA = new SqlDataAdapter();
                    cmd.Connection = cn;
                    MyDA.SelectCommand = cmd;
                    SqlString = cmd.CommandText;
                    DataSet myDS = new DataSet();

                    MyDA.Fill(myDS, TableName);
                    return myDS;

                }
                catch (Exception ex)
                {
                    General.ShowMessage(SqlString +"\n"+ ex.Message, "", MessageBoxIcon.Error);
                     return   null ;
                }
            }
        }

        public   SqlDataReader SeekRecord(string SqlString, SqlConnection cn)
        {


            using (SqlConnection Cn = new SqlConnection())
            {
                try
                {


                    SqlCommand DbCommand;
                    SqlDataReader RsResult;


                    // ----- Try to run the statement. Note that no error trapping is done
                    //       here. It is up to the calling routine to set up error checking.
                    DbCommand = new SqlCommand(SqlString, Cn);


                    RsResult = DbCommand.ExecuteReader();
                    DbCommand = null;
                    return RsResult;
                }
                catch (Exception ex)
                {
                    throw new Exception ( SqlString +"\n"+ ex.Message );
 
                }
            }

        }




        /// <summary>
        /// Insert Data into the given table
        /// </summary>
        /// <param name="TableName">Specify Table name </param>
        /// <param name="SchemaName">Specify Schema Name <example>dbo</example></param>
        /// <returns></returns>
        public bool Insert(string TableName, string SchemaName = "dbo")
        {
            try
            {

                return true;

            }
            catch (Exception ex)
            {
                General.ShowMessage("Error Occured While Inserting : \n" + ex.Message, "Error", MessageBoxIcon.Error, MessageBoxButtons.OK);
                return false;
            }
        }


        public bool ExecuteSqlTransaction(SqlCommand  command, string TransactionName)
        {
            return ExecuteSqlTransaction(new CommandCollection(){command},TransactionName);

        }

        public bool ExecuteSqlTransaction(CommandCollection commandcollection, string TransactionName)
        {
            bool pr=false ;
            return ExecuteSqlTransaction(commandcollection, TransactionName,ref pr);
        }

        public bool ExecuteSqlTransaction(CommandCollection commandcollection, string TransactionName, ref bool PrimaryKeyError,bool SkipPrimaryKeyError=true )
        {

            string sqlstring="";
            if (TransactionName.Length > 32)
                TransactionName = TransactionName.Substring(0,32);

            using (SqlConnection Cn = new SqlConnection())
            {
                bool flag = false;
                if (this._ConnectionProperties.ConnectionString != "")
                {
                    Cn.ConnectionString = _ConnectionProperties.ConnectionString;
                }
                else
                {
                    Cn.ConnectionString = _ConnectionProperties.GenerateSQLConnectionString();
                }
                if (Cn.ConnectionString.Trim().Length == 0)
                {
                    
                    return false  ;
                }
                if (Cn.State == ConnectionState.Open || Cn.State == ConnectionState.Broken)
                    Cn.Close();
                Cn.Open();


                SqlCommand  command = Cn.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = Cn.BeginTransaction(TransactionName);

                // Must assign both transaction object and connection 
                // to Command object for a pending local transaction
                command.Connection = Cn;
                command.Transaction = transaction;

                try
                {
                    foreach (SqlCommand _cmd in commandcollection)
                    {
                        command.CommandText = _cmd.CommandText;
                        command.CommandType = _cmd.CommandType;
                        command.CommandTimeout = 30;
                        foreach (SqlParameter p in _cmd.Parameters)
                        {
                            command.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }
                        sqlstring = command.CommandText;
                        command.ExecuteNonQuery();
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    flag = true;
                }
                catch (SqlException ex)
                {
                    General.ShowMessage(ex.Message +"\n\n " +
                       sqlstring.ToString());
                      
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        General.ShowMessage(ex2.Message, "", MessageBoxIcon.Error);
                        // This catch block will handle any errors that may have occurred 
                        // on the server that would cause the rollback to fail, such as 
                        // a closed connection.
                        //throw new Exception("Rollback Exception Type: " + ex2.GetType() + "\nMessage: {0}"+ ex2.Message);

                    }
                    flag = false;
                }
                catch (Exception ex)
                {
                    flag = false;


                    General.ShowMessage(ex.Message);

                    General.ShowMessage(sqlstring + "\n" + ex.Message, "", MessageBoxIcon.Error);
                    // Attempt to roll back the transaction. 
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        General.ShowMessage(ex2.Message, "", MessageBoxIcon.Error);
                        // This catch block will handle any errors that may have occurred 
                        // on the server that would cause the rollback to fail, such as 
                        // a closed connection.
                        //throw new Exception("Rollback Exception Type: " + ex2.GetType() + "\nMessage: {0}"+ ex2.Message);

                    }
                    //Exception("Commit Exception Type: " + ex.GetType() + "\nMessage: " + ex.Message);


                }

                commandcollection.Reset();
                if (flag && HasMirrorDB)
                {
                    ExecuteSqlTransactionMirror(commandcollection, "MirrorData", ref PrimaryKeyError);
                }
                commandcollection.Reset();
                if (flag && HasMirrorDB2)
                {
                    ExecuteSqlTransactionMirror2(commandcollection, "MirrorData", ref PrimaryKeyError);
                }
                return flag;
            }

        }


        public bool ExecuteSqlTransactionMirror(CommandCollection commandcollection, string TransactionName, ref bool PrimaryKeyError)
        {
            string sqlstring = "";
            if (TransactionName.Length > 32)
                TransactionName = TransactionName.Substring(0, 32);

            using (SqlConnection Cn = new SqlConnection())
            {
                bool flag = false;
                if (this._MirrorConnectionProperties.ConnectionString != "")
                {
                    Cn.ConnectionString = _MirrorConnectionProperties.ConnectionString;
                }
                else
                {
                    Cn.ConnectionString = _MirrorConnectionProperties.GenerateSQLConnectionString();
                }
                if (Cn.ConnectionString.Trim().Length == 0)
                {

                    return false;
                }
                if (Cn.State == ConnectionState.Open || Cn.State == ConnectionState.Broken)
                    Cn.Close();
                Cn.Open();


                SqlCommand command = Cn.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = Cn.BeginTransaction(TransactionName);

                // Must assign both transaction object and connection 
                // to Command object for a pending local transaction
                command.Connection = Cn;
                command.Transaction = transaction;

                try
                {
                    foreach (SqlCommand _cmd in commandcollection)
                    {
                        command.CommandText = _cmd.CommandText.Replace(ConnectionProperties.DatabseName,MirrorConnectionProperties.DatabseName);
                        command.CommandType = _cmd.CommandType;
                        command.CommandTimeout = 30;
                        foreach (SqlParameter p in _cmd.Parameters)
                        {
                            command.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }
                        sqlstring = command.CommandText;
                        command.ExecuteNonQuery();
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    flag = true;
                }
                catch (SqlException ex)
                {
                    General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
                    if (ex.Number == 2627)
                    {
                        PrimaryKeyError = true;
                    }
                    flag = false;
                }
                catch (Exception ex)
                {
                    flag = false;
                    General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);




                    General.ShowMessage(sqlstring + "\n" + ex.Message, "", MessageBoxIcon.Error);
                    // Attempt to roll back the transaction. 
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        General.ShowMessage(ex2.Message, "", MessageBoxIcon.Error);
                        // This catch block will handle any errors that may have occurred 
                        // on the server that would cause the rollback to fail, such as 
                        // a closed connection.
                        //throw new Exception("Rollback Exception Type: " + ex2.GetType() + "\nMessage: {0}"+ ex2.Message);

                    }
                    //Exception("Commit Exception Type: " + ex.GetType() + "\nMessage: " + ex.Message);


                }
                return flag;
            }

        }
        public bool ExecuteSqlTransactionMirror2(CommandCollection commandcollection, string TransactionName, ref bool PrimaryKeyError)
        {
            string sqlstring = "";
            if (TransactionName.Length > 32)
                TransactionName = TransactionName.Substring(0, 32);

            using (SqlConnection Cn = new SqlConnection())
            {
                bool flag = false;
                if (this._MirrorConnectionProperties2.ConnectionString != "")
                {
                    Cn.ConnectionString = _MirrorConnectionProperties2.ConnectionString;
                }
                else
                {
                    Cn.ConnectionString = _MirrorConnectionProperties2.GenerateSQLConnectionString();
                }
                if (Cn.ConnectionString.Trim().Length == 0)
                {

                    return false;
                }
                if (Cn.State == ConnectionState.Open || Cn.State == ConnectionState.Broken)
                    Cn.Close();
                Cn.Open();


                SqlCommand command = Cn.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = Cn.BeginTransaction(TransactionName);

                // Must assign both transaction object and connection 
                // to Command object for a pending local transaction
                command.Connection = Cn;
                command.Transaction = transaction;

                try
                {
                    foreach (SqlCommand _cmd in commandcollection)
                    {
                        command.CommandText = _cmd.CommandText.Replace(ConnectionProperties.DatabseName, MirrorConnectionProperties2.DatabseName);
                        command.CommandType = _cmd.CommandType;
                         command.CommandTimeout = 30;
                        foreach (SqlParameter p in _cmd.Parameters)
                        {
                            command.Parameters.AddWithValue(p.ParameterName, p.Value);
                        }
                        sqlstring = command.CommandText;
                        command.ExecuteNonQuery();
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();
                    flag = true;
                }
                catch (SqlException ex)
                {
                    General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);
                    if (ex.Number == 2627)
                    {
                        PrimaryKeyError = true;
                    }
                    flag = false;
                }
                catch (Exception ex)
                {
                    flag = false;

                    General.ShowMessage(ex.Message, "", MessageBoxIcon.Error);


                    General.ShowMessage(sqlstring + "\n" + ex.Message, "", MessageBoxIcon.Error);
                    // Attempt to roll back the transaction. 
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        General.ShowMessage(ex2.Message, "", MessageBoxIcon.Error);
                        // This catch block will handle any errors that may have occurred 
                        // on the server that would cause the rollback to fail, such as 
                        // a closed connection.
                        //throw new Exception("Rollback Exception Type: " + ex2.GetType() + "\nMessage: {0}"+ ex2.Message);

                    }
                    //Exception("Commit Exception Type: " + ex.GetType() + "\nMessage: " + ex.Message);


                }
                return flag;
            }

        }
        #region Connection Class

       
        #endregion

        #region Command Collection Class
        public class CommandCollection : IEnumerator, IEnumerable
        {
            List<SqlCommand> CommandObjects = new List<SqlCommand>();
            int position = -1;

            public CommandCollection()
            {

            }

            public void Add(string CommandText, SqlParameterCollection paramatercollection = null, CommandType commandType = CommandType.Text, int CommandTimeout = 30)
            {
                SqlCommand cmd = new SqlCommand();
                if (CommandText.Trim().Length == 0)
                {

                    throw new Exception("SQL Query not found");
                }

                cmd.CommandText = CommandText;
                cmd.CommandTimeout = 30;
                cmd.CommandType = commandType;
                if (cmd.CommandType == CommandType.StoredProcedure)
                {
                    foreach (SqlParameter p in paramatercollection)
                    {
                        cmd.Parameters.Add(p);

                    }

                }
                CommandObjects.Add(cmd);


            }

            public void Add(SqlCommand sqlcommand)
            {
                CommandObjects.Add(sqlcommand);
            }

            public void Add(SqlCommand[] sqlcommand)
            {
                if (sqlcommand == null)
                    return;
                foreach (SqlCommand c in sqlcommand)
                {
                    CommandObjects.Add(c);
                }
            }

            public void Clear()
            {
                CommandObjects.Clear();
            }

            public void Remove(int index)
            {
                // Check to see if there is a widget at the supplied index.
                if (index > CommandObjects.Count - 1 || index < 0)
                // If no widget exists, a messagebox is shown and the operation 
                // is cancelled.
                {
                    System.Windows.Forms.MessageBox.Show("Index not valid!");
                }
                else
                {
                    CommandObjects.RemoveAt(index);
                }
            }


            public SqlCommand Item(int index)
            {
                return CommandObjects[index];
            }
            //IEnumerator and IEnumerable require these methods.
            public IEnumerator GetEnumerator()
            {
                return (IEnumerator)this;
            }

            //IEnumerator
            public bool MoveNext()
            {
                position++;
                return (position < CommandObjects.Count);
            }

            //IEnumerable
            public void Reset()
            { position = -1; }

            //IEnumerable
            public object Current
            {
                get { return CommandObjects[position]; }
            }

        }
        #endregion

        #region Table Columns


        //    /// <summary>
        ///// Represent Properties of a column in a table
        ///// </summary>
        //    public class TableColumn
        //    {
        //         string  _ColumnName ;
        //        string  _ColumnType;
        //        bool  _AllowNulls;
        //        object _Value;

        //        public TableColumn ()
        //        {
        //        }

        //        /// <summary>
        //        /// Value of the Column
        //        /// </summary>
        //        public object Value
        //        {
        //            get
        //            {
        //                return this._Value;

        //            }
        //            set
        //            {
        //                this._Value = value;
        //            }
        //        }
        //        /// <summary>
        //        /// Column Name Of the table
        //        /// </summary>
        //        public string ColumnName
        //        {
        //            get
        //            {
        //                return this._ColumnName;
        //            }
        //            set
        //            {
        //                this._ColumnName = value;
        //            }
        //        }
        //        /// <summary>
        //        /// Datatype   Of the Column
        //        /// </summary>
        //        public string  ColumnType
        //        {
        //            get
        //            {
        //                return this._ColumnType;
        //            }
        //            set
        //            {
        //                this._ColumnType = value;
        //            }
        //        }

        //        /// <summary>
        //        /// Shows whether column can accept null values
        //        /// </summary>
        //        public bool AllowNulls
        //        {
        //            get
        //            {
        //                return this._AllowNulls;
        //            }
        //            set
        //            {
        //                this._AllowNulls = value;
        //            }
        //        }

        //    }
        ///// <summary>
        ///// Collection of TableColumn
        ///// </summary>
        //    public class TableColumns : System.Collections.CollectionBase
        //    {

        //        /// <summary>
        //        ///Add new tablecolumn to the collection  
        //        /// </summary>
        //        /// <param name="aTableColumn"> Table column Object </param>
        //        public void Add(TableColumn aTableColumn)
        //        {
        //            List.Add(aTableColumn);
        //        }
        //        /// <summary>
        //        /// Removes an item from the collection
        //        /// </summary>
        //        /// <param name="index"> index of the item </param>
        //        public void Remove(int index)
        //        {
        //            // Check to see if there is a widget at the supplied index.
        //            if (index > Count - 1 || index < 0)
        //            // If no widget exists, a messagebox is shown and the operation 
        //            // is cancelled.
        //            {
        //                System.Windows.Forms.MessageBox.Show("Index not valid!");
        //            }
        //            else
        //            {
        //                List.RemoveAt(index);
        //            }
        //        }

        //        public TableColumn Item(int Index)
        //        {
        //            // The appropriate item is retrieved from the List object and
        //            // explicitly cast to the Widget type, then returned to the 
        //            // caller.
        //            return (TableColumn)List[Index];
        //        }
        //    }
        #endregion

    }
 
}
