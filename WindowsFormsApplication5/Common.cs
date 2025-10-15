using Gramboo;
using Gramboo.Controls;
using Gramboo.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

namespace SAFA.Classes
{
    public static class Common
    {
        public static string DbName = "";
        public static DataGridViewCellStyle  styl = new DataGridViewCellStyle();
        public static DataGridViewCellStyle hdrstyl = new DataGridViewCellStyle();
        public static bool screelocked = false;
        
        public static double GetNextID(Gramboo.Database.Table table_name, string field_name, DataController dc, int company_id, int branchID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "GEN.NEXTID";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@table_name", (table_name.DatabaseName.ToUpper() == dc.ConnectionProperties.DatabseName.ToUpper() ? "" : table_name.DatabaseName + ".") + table_name.OwnerName + "." + table_name.Name);
            cmd.Parameters.AddWithValue("@field_name", field_name);
            cmd.Parameters.AddWithValue("@company_ID", company_id);
            cmd.Parameters.AddWithValue("@branch_id", branchID);
            cmd.Parameters.AddWithValue("@ResValOut", 0);

            return Convert.ToDouble(dc.GetData(cmd).Tables[0].Rows[0][0]);
        }


        public static string SubmitSMS(DataController dc, string TemplateName, long Rid, int company_id)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "[SYST].[SubmitSms]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TemplateName", TemplateName);
            cmd.Parameters.AddWithValue("@companyID", company_id);
            cmd.Parameters.AddWithValue("@Rid", Rid);

            return dc.ExecuteSqlTransaction(cmd, "tbl").ToString();

        }
        public static bool JobNoVlidate(DataController dc,int Branch_Id)
        {          
            using (System.Data.DataTable dt1 = dc.GetData(new SqlCommand
              ("SELECT isnull(JobOrderId,0)as HasJobNO  FROM SYST.Settings WHERE Branch_id =" + Branch_Id )).Tables[0])
            {
                if (dt1.Rows.Count > 0)
                {
                    return Convert.ToBoolean(dt1.Rows[0][0].ToString());

                }
                else
                {
                    return false;
                }
            }
        }
        public static bool StoneCtWtVisible(DataController dc, int Branch_Id)
        {
            using (System.Data.DataTable dt1 = dc.GetData(new SqlCommand
              ("SELECT isnull(StoneCtWt,0)as StoneCtWt  FROM SYST.Settings WHERE Branch_id =" + Branch_Id)).Tables[0])
            {
                if (dt1.Rows.Count > 0)
                {
                    return Convert.ToBoolean(dt1.Rows[0][0].ToString());

                }
                else
                {
                    return false;
                }
            }
        }
        public static bool ChkUserType(DataController dc, Int64 user_id)
        {
            using (System.Data.DataTable dt = dc.GetData(new SqlCommand
            ("SELECT Upper([User Category Name]) as UserName From SYST.VUserName WHERE Upper([User Category Name]) like 'ADMIN%' And user_id =" + user_id)).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public static bool ChkServerType(DataController dc, int Branch_Id)
        {
            using (System.Data.DataTable dt = dc.GetData(new SqlCommand
            ("SELECT ISNULL(ServerType,'L') as ServerType  FROM SYST.Settings WHERE ServerType like 'L%' And Branch_id =" + Branch_Id)).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }
        public static bool AllowBarcodeEdit(DataController dc, int Branch_Id)
        {
            using (System.Data.DataTable dt = dc.GetData(new SqlCommand
            ("SELECT ISNULL(AllowBarcodeEdit,'False') as AllowBarcodeEdit  FROM SYST.Settings WHERE Branch_id =" + Branch_Id)).Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToBoolean(dt.Rows[0][0].ToString());
                }
                else
                {
                    return false;
                }
            }

        }
        public static bool TestLinkedServerConnection(DataController dc, int Branch_Id)
        {

            if (!SAFA.Classes.Common.ChkServerType(dc, Branch_Id))
            {
                return true;
            }

            using (System.Data.DataTable dt1 = dc.GetData(new SqlCommand
            ("SELECT isnull(IP,0)as IP  FROM SYST.Settings WHERE Branch_id =" + Branch_Id)).Tables[0])
            {
                if (dt1.Rows.Count > 0)
                {
                    string IP = dt1.Rows[0]["IP"].ToString();
                    try
                    {
                        Ping myPing = new Ping();
                        PingReply reply = myPing.Send(IP,1);
                        if (reply.Status.ToString().Equals("Success"))
                        {
                            return true;                          
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch
                    {
                        return false;
                    }       
                                  
                }
                else
                {
                    return false;
                }
            }
        }
        public static bool TestLinkedServer(DataController dc, int Branch_Id)
        {
            if (!SAFA.Classes.Common.ChkServerType(dc, Branch_Id))
            {
                return true;
            }

            bool Connection;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SYST.TestConnection";
            cmd.CommandType = CommandType.StoredProcedure;          
            cmd.CommandTimeout = 0;
            DataTable dt = dc.GetData(cmd).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Connection = Convert.ToBoolean(dt.Rows[0]["Connection"].ToString());
                return Connection;
            }
            else
            {
                return false;
            }
        }
        public static bool TxTypeValidate(DataController dc, int Branch_id)
        {
            using (System.Data.DataTable dt1 = dc.GetData(new SqlCommand
            ("SELECT isnull(TxType,0)as HasTxType  FROM SYST.Settings WHERE Branch_id =" + Branch_id)).Tables[0])
            {
                if (dt1.Rows.Count > 0)
                {
                    return Convert.ToBoolean(dt1.Rows[0][0].ToString());

                }
                else
                {
                    return false;
                }
            }
        }
         public static bool ValidateITR(decimal netamt,string custpan,String CustAdhaar)
        {
            if (Convert.ToDecimal(netamt) >= 200000)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$");
                Match match = regex.Match(custpan);
                if (match.Success == false)
                {
                    System.Text.RegularExpressions.Regex Adhaarregex = new System.Text.RegularExpressions.Regex("^([0-9]){12}$");
                    Match Adhaarmatch = Adhaarregex.Match(CustAdhaar);
                    if (Adhaarmatch.Success == false)
                    {
                        if (custpan.Trim() == "" || custpan == "-" && CustAdhaar.Trim()=="" || CustAdhaar=="0")
                        {
                            Gramboo.General.ShowMessage("Update PanNumber..!!");
                            return false;
                        }
                        else if (custpan.Trim().Length >1)
                        {
                            Gramboo.General.ShowMessage("Invalid PanNumber..!!");
                            return false;
                            //txtCustPAn.ShowMessage("PanNumber Not matched..!!");   
                        }
                        else if (CustAdhaar.Trim() == "" || CustAdhaar == "0")
                        {
                            Gramboo.General.ShowMessage("Update AdhaarNumber..!!");
                            return false;
                        }
                        else 
                        {
                            Gramboo.General.ShowMessage("Invalid AdhaarNumber..!!");
                            return false;
                        }
                    }                 
                }
                else
                {
                    return true;
                }               
            }
           return true;
         }

        public static string SendSMS(DataController dc)
        {
            using (DataTable dt = dc.GetData(new SqlCommand("Select top 1 * from SYST.SmsRegister WHERE Status='FALSE'"), "tbl").Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "[SYST].[SendSms]";
                    cmd.CommandType = CommandType.StoredProcedure;
                    return dc.ExecuteSqlTransaction(cmd, "tbl").ToString();
                }
                else
                {
                    return "";
                }
            }
        }


        public static bool InsertToQueryBase(DataController dc, string Query)
        {


            string str = "INSERT INTO  [SYST].[QueryStackMaster]([Query],TransferStatus) VALUES (@Query,'false')";
            SqlCommand oraCommand = new SqlCommand(str);
            oraCommand.Parameters.AddWithValue("@Query", Query);
            return dc.ExecuteSqlTransaction(oraCommand, "log");

        }

        public static void FillProductCode(GrbComboBox cmb, DataController dc, int companyid, int branchid, DateTime date, string criteria = "")
        {
            using (SqlCommand cmd = new SqlCommand("Select * FROM STK.GetOrnamentStatus('" + date.ToString("dd-MMM-yyyy") + "'," + companyid + "," + branchid + ") " + (criteria.Trim().Length > 0 ? " WHERE " + criteria : "")))
            {
                using (DataTable dt = dc.GetData(cmd, "Prodcodes").Tables[0])
                {
                    cmb.DisplayMember = "ProdCode";
                    cmb.ValueMember = "ProdCodeId";
                    cmb.DataSource = dt;

                }
            }
        }




        public static bool ValidateDelete(string TableName, string validatequery)
        {
            switch (TableName)
            {
                case "":
                    {
                        return true;
                    }
                default:
                    {
                        return false;
                    }

            }
        }




        public static void  InsertTLog(string action,long userid, DataController dc)
        {

            dc.ExecuteSqlTransaction(new SqlCommand("Insert into SYST.TLog(Taction,Tdate,UserId) Values('"+ action +"','"+ DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss tt") +"',"+ userid +")"),"TLog");

        }
        
        
        public static void ExecuteCommand(string command)
        {

            int exitCode;
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            process = Process.Start(processInfo);
            process.WaitForExit();

            // *** Read the streams ***
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            exitCode = process.ExitCode;

            Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
            Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
            Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
            process.Close();
        }

        public static string GetNextVoucherNoTax(int VoucherType, DateTime VoucherDate, DataController dc, int company_id, int branchID)
        {
        //    dc = new DataController();
        //    dc.ConnectionProperties.GenerateSQLConnectionString();
        //       SimpleFA.Classes.ConfigureServers servCon = new    SimpleFA.Classes.ConfigureServers();
        //    servCon.GenerateSQLConfigureServersString();

        //    dc.ConnectionProperties.ServerName = servCon.DbAServerName;
        //    dc.ConnectionProperties.DatabseName = servCon.DbADBName;
        //    dc.ConnectionProperties.GenerateSQLConnectionString();


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "NUM.GenerateVoucherNo";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VchType", VoucherType);
            cmd.Parameters.AddWithValue("@VchDate", VoucherDate);
            cmd.Parameters.AddWithValue("@companyID", company_id);
            cmd.Parameters.AddWithValue("@branchid", branchID);
            cmd.Parameters.AddWithValue("@RETVALOUT", 0);

            return dc.GetData(cmd).Tables[0].Rows[0][0].ToString(); 
        }


         
        public static string GetNextVoucherNo(int VoucherType, DateTime VoucherDate, DataController dc, int company_id, int branchID)
        {
        //      dc = new DataController();

        //    dc.ConnectionProperties.GenerateSQLConnectionString();
        //       SimpleFA.Classes.ConfigureServers servCon = new    SimpleFA.Classes.ConfigureServers();
        //    servCon.GenerateSQLConfigureServersString();

        //    dc.ConnectionProperties.ServerName = servCon.DbBServerName;
        //    dc.ConnectionProperties.DatabseName = servCon.DbBDBName;
        //    dc.ConnectionProperties.GenerateSQLConnectionString();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "NUM.GenerateVoucherNo";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VchType", VoucherType);
            cmd.Parameters.AddWithValue("@VchDate", VoucherDate);
            cmd.Parameters.AddWithValue("@companyID", company_id);
            cmd.Parameters.AddWithValue("@branchid", branchID);
            cmd.Parameters.AddWithValue("@RETVALOUT" , 0);

            return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
        }


        public static float GetActualWeight(float Purity, float GrossWt)
        {
            return GrossWt * Purity / 100f;
        }

        public static void SwitchCompany(int ToCompany, int ToBranch)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType().BaseType == typeof(Gramboo.Controls.GrbForm))
                {

                    ((GrbForm)frm).SwitchAccount(ToCompany, ToBranch);
                }
            }

        }
         
        public static DataGridViewCellStyle GetGridCellStyle()
        {
            //dgv.DefaultCellStyle.ForeColor = Color.Black;
            //dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Ariel", 9, FontStyle.Regular);
            //dgv.RowsDefaultCellStyle.Font = new Font("Ariel", 9, FontStyle.Regular);
            styl.Font =new System.Drawing.Font("Ariel",8,System.Drawing.FontStyle.Regular);
            styl.ForeColor=System.Drawing.Color.Black ;
            
            return styl;
        }


         public static DataGridViewCellStyle GetGridHeaderCellStyle()
        {
            hdrstyl.Font=new System.Drawing.Font("Ariel",8,System.Drawing.FontStyle.Regular);
            return hdrstyl;
        }


        //public static string GetWorkerCashBalance(int WorkerID, DataController dc, int company_id)
        //{
        //    //SqlCommand cmd = new SqlCommand();
        //    //cmd.CommandText = "Select STK.GetWorkerCashBalance(@WorkerID,@CompanyId)";
        //    //cmd.CommandType = CommandType.Text;

        //    //cmd.Parameters.AddWithValue("@WorkerID", WorkerID);
        //    //cmd.Parameters.AddWithValue("@CompanyId", company_id);

        //    //return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
        //    return "0";
        //}
        //public static string GetWorkerWeightBalance(int WorkerID, DataController dc, int company_id)
        //{
        //    //SqlCommand cmd = new SqlCommand();
        //    //cmd.CommandText = "Select STK.GetWorkerWeightBalance(@WorkerID,@CompanyId)";
        //    //cmd.CommandType = CommandType.Text;

        //    //cmd.Parameters.AddWithValue("@WorkerID", WorkerID);a
        //    //cmd.Parameters.AddWithValue("@CompanyId", company_id);

        //    //return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
        //    return "0";
        //}

       // public static void SetDatabaseLogon(ReportDocument cr, DataController DBConn, bool IsSubReport = false)
       //{
       //     for (int i = 0; i < cr.Database.Tables.Count; i++)
       //     {
       //         TableLogOnInfo logOnInfo = new TableLogOnInfo();

       //         logOnInfo = cr.Database.Tables[i].LogOnInfo;

       //        //  Set the connection information for the table in the report.
       //         logOnInfo.ConnectionInfo.ServerName = DBConn.ConnectionProperties.DatabseName;
       //         logOnInfo.ConnectionInfo.DatabaseName = DBConn.ConnectionProperties.DatabseName;
       //         logOnInfo.ConnectionInfo.UserID = DBConn.ConnectionProperties.DBUsername;
       //         logOnInfo.ConnectionInfo.Password = DBConn.ConnectionProperties.DBPassword;
       //         logOnInfo.TableName = cr.Database.Tables[i].Name;

       //         cr.Database.Tables[i].ApplyLogOnInfo(logOnInfo);

       //     }

       //     if (IsSubReport == false)
       //     {
       //         for (int i = 0; i < cr.Subreports.Count; i++)
       //         {

       //             SetDatabaseLogon(cr.Subreports[i], DBConn, true);
       //         }
       //     }
       //     cr.DataSourceConnections[0].SetConnection(DBConn.ConnectionProperties.DatabseName, DBConn.ConnectionProperties.DatabseName, DBConn.ConnectionProperties.DBUsername, DBConn.ConnectionProperties.DBPassword);
       //     cr.VerifyDatabase();
       //}

       

        public static string GetCustomerCashBalance(int CustID, DataController dc, int company_id, DateTime StockDate, int branch_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select  CashBalance FROM SALE.GetCustomerBalance(@Date,@CustID,'FALSE',@CompanyId,@BranchId)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Date", StockDate.ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@CustID", CustID);
            cmd.Parameters.AddWithValue("@BranchId", branch_id);
            cmd.Parameters.AddWithValue("@WorkerID", CustID);
            cmd.Parameters.AddWithValue("@CompanyId", company_id);
            if (dc.GetData(cmd).Tables[0].Rows.Count > 0)
                return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
            else
                return "0";
        }



        public static string GetCustomerWeightBalance(int CustID, DataController dc, int company_id, DateTime StockDate, int branch_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select WtBalance FROM SALE.GetCustomerBalance (@Date,@CustID,'FALSE',@CompanyId,@BranchId)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Date", StockDate);
            cmd.Parameters.AddWithValue("@CustID", CustID);
            cmd.Parameters.AddWithValue("@BranchId", branch_id);
            cmd.Parameters.AddWithValue("@WorkerID", CustID);
            cmd.Parameters.AddWithValue("@CompanyId", company_id);

            if (dc.GetData(cmd).Tables[0].Rows.Count > 0)
                return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
            else
                return "0";
        }
        public static string GetCustomerMCBalance(int CustID, DataController dc, int company_id, DateTime StockDate, int branch_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select MCBalance FROM SALE.GetCustomerBalance (@Date,@CustID,'FALSE',@CompanyId,@BranchId)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Date", StockDate);
            cmd.Parameters.AddWithValue("@CustID", CustID);
            cmd.Parameters.AddWithValue("@BranchId", branch_id);
            cmd.Parameters.AddWithValue("@WorkerID", CustID);
            cmd.Parameters.AddWithValue("@CompanyId", company_id);

            if (dc.GetData(cmd).Tables[0].Rows.Count > 0)
                return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
            else
                return "0";
        }


        public static string GetSupplierCashBalance(int SuppID, DataController dc, int company_id, DateTime StockDate, int branch_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select  CashBalance FROM PUR.GetSupplierBalance(@Date,@SuppID,@CompanyId,@BranchId)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Date", StockDate.ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@SuppID", SuppID);
            cmd.Parameters.AddWithValue("@BranchId", branch_id);
            cmd.Parameters.AddWithValue("@CompanyId", company_id);
            if (dc.GetData(cmd).Tables[0].Rows.Count > 0)
                return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
            else
                return "0";
        }


        public static string GetSupplierWeightBalance(int SuppID, DataController dc, int company_id, DateTime StockDate, int branch_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select WtBalance FROM PUR.GetSupplierBalance (@Date,@SuppID,@CompanyId,@BranchId)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Date", StockDate);
            cmd.Parameters.AddWithValue("@SuppID", SuppID);
            cmd.Parameters.AddWithValue("@BranchId", branch_id);
            cmd.Parameters.AddWithValue("@CompanyId", company_id);

            if (dc.GetData(cmd).Tables[0].Rows.Count > 0)
                return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
            else
                return "0";
        }


        public static string GetSupplierMCBalance(int SuppID, DataController dc, int company_id, DateTime StockDate, int branch_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select MCBalance FROM PUR.GetSupplierBalance (@Date,@SuppID,@CompanyId,@BranchId)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Date", StockDate);
            cmd.Parameters.AddWithValue("@SuppID", SuppID);
            cmd.Parameters.AddWithValue("@BranchId", branch_id);
            cmd.Parameters.AddWithValue("@CompanyId", company_id);

            if (dc.GetData(cmd).Tables[0].Rows.Count > 0)
                return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
            else
                return "0";
        }
        public static string GetSupplierSilverBalance(int SuppID, DataController dc, int company_id, DateTime StockDate, int branch_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select  SilverBalance FROM PUR.GetSupplierBalance(@Date,@SuppID,@CompanyId,@BranchId)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Date", StockDate.ToString("dd-MMM-yyyy"));
            cmd.Parameters.AddWithValue("@SuppID", SuppID);
            cmd.Parameters.AddWithValue("@BranchId", branch_id);
            cmd.Parameters.AddWithValue("@CompanyId", company_id);
            if (dc.GetData(cmd).Tables[0].Rows.Count > 0)
                return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
            else
                return "0";
        }


        public static string GetWorkerCashBalance(int WorkerID, DataController dc, int company_id, DateTime StockDate, int branch_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select STK.GetWorkerCashBalance(@StockDate,@WorkerID,@CompanyId,@BranchId)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@StockDate", StockDate);
            cmd.Parameters.AddWithValue("@BranchId", branch_id);
            cmd.Parameters.AddWithValue("@WorkerID", WorkerID);
            cmd.Parameters.AddWithValue("@CompanyId", company_id);

            return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
        }



        public static string GetWorkerWeightBalance(int WorkerID, DataController dc, int company_id, DateTime StockDate, int branch_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select STK.GetWorkerWeightBalance(@StockDate,@WorkerID,@CompanyId,@BranchId)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@StockDate", StockDate);
            cmd.Parameters.AddWithValue("@BranchId", branch_id);
            cmd.Parameters.AddWithValue("@WorkerID", WorkerID);
            cmd.Parameters.AddWithValue("@CompanyId", company_id);

            return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
        }


        public static string GetWorkShopCashBalance(int WorkShopID, char WorkShopType, DataController dc, int company_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select STK.GetWorkShopCashBalance(@WorkShopID,@WorkShopType,@CompanyId)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@WorkShopID", WorkShopID);
            cmd.Parameters.AddWithValue("@WorkShopType", WorkShopType);
            cmd.Parameters.AddWithValue("@CompanyId", company_id);

            return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();
        }


        public static string GetWorkShopWeightBalance(int WorkShopID, char WorkShopType, DataController dc, int company_id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select STK.GetWorkShopCashBalance(@WorkShopID,@WorkShopType,@CompanyId)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@WorkShopID", WorkShopID);
            cmd.Parameters.AddWithValue("@WorkShopType", WorkShopType);
            cmd.Parameters.AddWithValue("@CompanyId", company_id);

            return dc.GetData(cmd).Tables[0].Rows[0][0].ToString();

        }


        public static void EnableSpecs(long ItemId, System.Windows.Forms.DataGridViewCell Diawt,
             System.Windows.Forms.DataGridViewCell StWt, System.Windows.Forms.DataGridViewCell StCtWt, System.Windows.Forms.DataGridViewCell NetWt,
             System.Windows.Forms.DataGridViewCell Gwt, System.Windows.Forms.DataGridViewCell MC,
             System.Windows.Forms.DataGridViewCell MCPerc, System.Windows.Forms.DataGridViewCell WstPerc,
             System.Windows.Forms.DataGridViewCell Wst, System.Windows.Forms.DataGridViewCell Touch,
             DataController dc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ITM.EnableSpecs";
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter DiaWt = new SqlParameter("@Diawt", DbType.Boolean);
            DiaWt.Direction = ParameterDirection.Output;
            SqlParameter StoWt = new SqlParameter("@StWt", DbType.Boolean);
            StoWt.Direction = ParameterDirection.Output;
            SqlParameter StoctWt = new SqlParameter("@StCtWt", DbType.Boolean);
            StoWt.Direction = ParameterDirection.Output;
            SqlParameter NetWgt = new SqlParameter("@NetWt", DbType.Boolean);
            NetWgt.Direction = ParameterDirection.Output;
            SqlParameter Grwt = new SqlParameter("@Gwt", DbType.Boolean);
            Grwt.Direction = ParameterDirection.Output;
            SqlParameter mc = new SqlParameter("@MC", DbType.Boolean);
            mc.Direction = ParameterDirection.Output;
            SqlParameter mcperc = new SqlParameter("@McPerc", DbType.Boolean);
            mcperc.Direction = ParameterDirection.Output;
            SqlParameter wstp = new SqlParameter("@WstPerc", DbType.Boolean);
            wstp.Direction = ParameterDirection.Output;
            SqlParameter wst = new SqlParameter("@Wst", DbType.Boolean);
            wst.Direction = ParameterDirection.Output;
            SqlParameter Touh = new SqlParameter("@Touch", DbType.Boolean);
            Touh.Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.Add(DiaWt);
            cmd.Parameters.Add(StoWt);
            cmd.Parameters.Add(StoctWt);
            cmd.Parameters.Add(NetWgt);
            cmd.Parameters.Add(Grwt);
            cmd.Parameters.Add(mc);
            cmd.Parameters.Add(mcperc);
            cmd.Parameters.Add(wstp);
            cmd.Parameters.Add(wst);
            cmd.Parameters.Add(Touh);
            dc.GetData(cmd);


            Diawt.ReadOnly = Convert.ToBoolean(cmd.Parameters["@Diawt"].Value);
            StWt.ReadOnly = Convert.ToBoolean(cmd.Parameters["@StWt"].Value);
            StCtWt.ReadOnly = Convert.ToBoolean(cmd.Parameters["@SCtWt"].Value);
            NetWt.ReadOnly = Convert.ToBoolean(cmd.Parameters["@NetWt"].Value);
            Gwt.ReadOnly = Convert.ToBoolean(cmd.Parameters["@Gwt"].Value);
            MC.ReadOnly = Convert.ToBoolean(cmd.Parameters["@MC"].Value);
            MCPerc.ReadOnly = Convert.ToBoolean(cmd.Parameters["@McPerc"].Value);
            WstPerc.ReadOnly = Convert.ToBoolean(cmd.Parameters["@WstPerc"].Value);
            Wst.ReadOnly = Convert.ToBoolean(cmd.Parameters["@Wst"].Value);
            Touch.ReadOnly = Convert.ToBoolean(cmd.Parameters["@Touch"].Value);

            //sizerange.SelectedValue = -1;
            //PriceRange.SelectedValue = -1;
            //Sieve.SelectedValue = -1;
        }





        public static float CalculateMC(float Mcpergm, float Gwt)
        {

            return Gwt * Mcpergm;
        }


        public static float CalculateWastage(float WastagePerc, float Gwt)
        {

            return Gwt * WastagePerc / 100;
        }


        public static float CalculateMCPerPC(float Mcpergm, int Nos)
        {

            return Nos * Mcpergm;
        }


        public static float CalculateWastagePerPC(float WastagePerc, int Nos)
        {

            return Nos * WastagePerc / 100;
        }
        public static void gettables(DataController dc, int branchid)
        {
        //    DataConnector DataConnect = new DataConnector();
        //    using (System.Data.DataTable dt1 = DataConnect.GetDataTable("select * from SYST.MasterTables"))
        //    //using (System.Data.DataTable dt1 =dc.GetData(new SqlCommand("select * from SYST.MasterTables")).Tables[0])
        //    {
        //        foreach (DataRow row in dt1.Rows)
        //        {
        //            using (System.Data.DataTable dt2 = DataConnect.GetDataTable("select * from " + row["TableName"] + " where Branch_id !=" + branchid))
        //            //using (System.Data.DataTable dt2 = dc.GetData(new SqlCommand("select * from " + row["TableName"] + " where Branch_id !=" + branchid)).Tables[0])
        //            {
        //                if (dt2.Rows.Count > 0)
        //                {
        //                    dc.ExecuteSqlTransaction(new SqlCommand("Delete from " + row["TableName"] + " where Branch_id !=" + branchid), "delete");

        //                    string connectionString =
        //                            dc.ConnectionProperties.ConnectionString;
        //                    //Get the DataTable 
        //                    DataTable dtInsertRows = dt2;
        //                    string DestinationTbl = row["TableName"].ToString();
        //                    using (SqlBulkCopy sbc = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
        //                    {
        //                        sbc.DestinationTableName = DestinationTbl;

        //                        // Number of records to be processed in one go
        //                        sbc.BatchSize = dt2.Rows.Count;

        //                        // Add your column mappings here
        //                        foreach (DataColumn c in dt2.Columns)
        //                        {
        //                            sbc.ColumnMappings.Add(c.ColumnName, c.ColumnName);
        //                        }
        //                        //sbc.ColumnMappings.Add("field1", "field3");
        //                        //sbc.ColumnMappings.Add("foo", "bar");

        //                        // Finally write to server
        //                        sbc.WriteToServer(dtInsertRows);
        //                    }
        //                }
        //            }
        //        }
        //    }
        }

        public static void getprodcodedetails(DataController dc, string  entryid)
        {
            //DataConnector DataConnect = new DataConnector();

            //using (System.Data.DataTable dt2 = DataConnect.GetDataTable("select t1.* from STK.ProdCodeMaster t1,STK.StockTransferDetails t2  where t1.ProdCodeId = t2.ProdCodeId AND  TransId in (" + entryid + ") and IsReceipt='false'"))
            // {
            //     foreach (DataRow Row in dt2.Rows)
            //     {
            //         dc.ExecuteSqlTransaction(new SqlCommand("Delete from STK.ProdCodeMaster where ProdCodeId=" + Row["ProdCodeId"].ToString()), "delete");
                     
            //     }

            //     string connectionString = dc.ConnectionProperties.ConnectionString;
            //     DataTable dtInsertRows = dt2;
            //     string DestinationTbl = "STK.ProdCodeMaster";
            //     using (SqlBulkCopy sbc = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
            //     {
            //         sbc.DestinationTableName = DestinationTbl;

            //         // Number of records to be processed in one go
            //         sbc.BatchSize = dt2.Rows.Count;

            //         foreach (DataColumn c in dt2.Columns)
            //         {
            //             sbc.ColumnMappings.Add(c.ColumnName, c.ColumnName);
            //         }

            //         sbc.WriteToServer(dtInsertRows);
            //     }
            //}

            //using (System.Data.DataTable dt2 = DataConnect.GetDataTable("select t1.* from STK.ProductCodeDetail t1,STK.StockTransferDetails t2  where t1.ProdCodeId = t2.ProdCodeId AND  t2.TransId in (" + entryid + ") and IsReceipt='false'"))
            //{
            //    foreach (DataRow Row in dt2.Rows)
            //    {
            //        dc.ExecuteSqlTransaction(new SqlCommand("Delete from STK.ProductCodeDetail where ProdCodeId=" + Row["ProdCodeId"].ToString()), "delete");

            //    }

            //    string connectionString = dc.ConnectionProperties.ConnectionString;
            //    DataTable dtInsertRows = dt2;
            //    string DestinationTbl = "STK.ProductCodeDetail";
            //    using (SqlBulkCopy sbc = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.KeepIdentity))
            //    {
            //        sbc.DestinationTableName = DestinationTbl;

            //        // Number of records to be processed in one go
            //        sbc.BatchSize = dt2.Rows.Count;

            //        foreach (DataColumn c in dt2.Columns)
            //        {
            //            sbc.ColumnMappings.Add(c.ColumnName, c.ColumnName);
            //        }

            //        sbc.WriteToServer(dtInsertRows);
            //    }
            //}
            
        }
        public static void enableMaterialsSpecs(int ItemId, System.Windows.Forms.ComboBox Sieve, DataController dc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ITM.EnableSpecs";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pSieve = new SqlParameter("@Sieve", DbType.Boolean);
            pSieve.Direction = ParameterDirection.Output;
            cmd.Parameters.AddWithValue("@ItemId", ItemId);
            cmd.Parameters.Add(pSieve);
            dc.GetData(cmd);
            Sieve.Enabled = Convert.ToBoolean(cmd.Parameters["@Sieve"].Value);
            Sieve.SelectedValue = -1;
        }



        public static void GetMCandWastage(DataController dc, System.Windows.Forms.TextBox MCPerc, System.Windows.Forms.TextBox MCVal,
            System.Windows.Forms.TextBox WstPerc, System.Windows.Forms.TextBox WstVal, DateTime VchDate,
    int ItemId, string Process, int ProcessType, int WorkerID, int CompanyID, int BranchId, float IssueWt, float RetWt, int Nos, float DiaWt, int DiaNo)
        {


            using (DataTable dt = dc.GetData(new SqlCommand("Select * FROM PROD.CalculateWstMC('" + VchDate + "'," + WorkerID + "," + ItemId + ",'" + Process + "'," + ProcessType + ","
       + IssueWt + "," + RetWt + "," + Nos + "," + DiaWt + "," + DiaNo + "," + CompanyID + "," + BranchId + ")"), "CalculateWstMC").Tables[0])
            {
                if (dt.Rows.Count > 0)
                {
                    MCPerc.Text = dt.Rows[0][0].ToString();
                    MCVal.Text = dt.Rows[0][1].ToString();

                    WstPerc.Text = dt.Rows[0][2].ToString();
                    WstVal.Text = dt.Rows[0][3].ToString();


                }

            }

        }

         

      
        internal static void GetSupplierBalance(DateTime dateTime, int p1, DataController DBConn, int p2, int p3, ref float wtbal, ref double cashbal, ref double mcbal)
        {
            throw new NotImplementedException();
        }





        internal static object GetNextVoucherNo(int p1, DataController DBConn, int p2, int p3)
        {
            throw new NotImplementedException();
        }

        public static float calculatetax(DataController de, float amt, string compid, string branchid, string tdsid, string date,string dtpfrom,string dtpto,string ledgerid)
        {
            double totamt = 0, sintrans, fytrans;
            
                using (System.Data.DataTable dt2 = de.GetData(new SqlCommand("select * from  [GEN].[FunTdscalc]('" + dtpfrom + "','" + dtpto + "','" + compid + "','" + branchid + "','"+ ledgerid +"')"), "tbl").Tables[0])
                {

                    if (dt2.Rows.Count > 0)
                    {
                        totamt = Convert.ToDouble(dt2.Rows[0]["totamt"]);
                    }
                }
            

            sintrans = Convert.ToDouble(de.GetData(new SqlCommand("select top 1 SingleTransaction FROM GEN.TDSDetails  where TdsId='" + tdsid + "' and date<='" + date + "' order by date desc")).Tables[0].Rows[0][0]);
            fytrans = Convert.ToDouble(de.GetData(new SqlCommand("select top 1 FYTransaction FROM GEN.TDSDetails  where TdsId='" + tdsid + "' and date<='" + date + "' order by date desc")).Tables[0].Rows[0][0]);
            if (sintrans == 0 && fytrans > 0)
            {
                if (totamt >= fytrans)
                {
                    return amt;
                }
                else
                {
                    if ((totamt + amt) >= fytrans)
                    {
                        return (Convert.ToInt32(totamt + amt));
                    }
                    else
                    {
                        return 0;
                    }

                }


            }
            else if (sintrans > 0 && fytrans > 0)
            {

                if (amt >= sintrans || totamt >= fytrans)
                {
                    return amt;
                }
                else
                {
                    if ((totamt + amt) > fytrans)
                    {
                        return (Convert.ToInt32(totamt + amt));
                    }
                    else
                    {
                        return 0;
                    }
                }

            }
            else
            {
                return amt;
            }


        }
        public static string SelectPrinter(DataController dc,string print_type)
        {
            string  salePrinter = "", Estprinter = "", BarcodePrinter = "";
            using (DataTable tt = dc.GetData(new System.Data.SqlClient.SqlCommand
                                     ("select EstimatePrinter,BarcodePrinter,SalesPrinter from gen.printersettings where Company_id= " + Gramboo.GeneralConfig.CompanyID + " and Branch_Id=" + Gramboo.GeneralConfig.BranchId + " and CounterId='" + Gramboo.GeneralConfig.CounterId + "'")).Tables[0])
            {
                if (tt.Rows.Count > 0)
                {
                    Estprinter = tt.Rows[0]["EstimatePrinter"].ToString();
                    BarcodePrinter = tt.Rows[0]["BarcodePrinter"].ToString();
                    salePrinter = tt.Rows[0]["SalesPrinter"].ToString();
                }
            }
            if(print_type=="Estimate")
            {
                return Estprinter;
            }
            else if(print_type == "Barcode")
            {
                return BarcodePrinter;

            }
            else
            {
                return salePrinter;
            }

           
        }
    }
}
