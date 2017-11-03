using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ClaimsDocsClient.AppClasses
{
    public class ClaimsDocsLog
    {
        //declare public class properties
        public int ClaimsDocsLogID { get; set; }
        public int LogSourceTypeID { get; set; }
        public int LogTypeID { get; set; }
        public string MessageIs { get; set; }
        public string ExceptionIs { get; set; }
        public string StackTraceIs { get; set; }
        public DateTime IUDateTime { get; set; }
    }//end class definition of class : ClaimsDocsLog

    public class AppSupport
    {
        //declare private class variables
        private List<ClaimsDocsLog> _listClaimsDocsLog = new List<ClaimsDocsLog>();

        //define method : ClaimsDocsLogCreate
        public int ClaimsDocsLogCreate(ClaimsDocsLog objClaimsDocsLog, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            int intResult = 0;

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spClaimsDocsLogCreate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objClaimsDocsLog.ClaimsDocsLogID) and set its properties
                SqlParameter paramClaimsDocsLogID = new SqlParameter();
                paramClaimsDocsLogID.ParameterName = "@intClaimsDocsLogID";
                paramClaimsDocsLogID.SqlDbType = SqlDbType.Int;
                paramClaimsDocsLogID.Direction = ParameterDirection.Input;
                paramClaimsDocsLogID.Value = objClaimsDocsLog.ClaimsDocsLogID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimsDocsLogID);

                //Add input parameter for (objClaimsDocsLog.LogSourceTypeID) and set its properties
                SqlParameter paramLogSourceTypeID = new SqlParameter();
                paramLogSourceTypeID.ParameterName = "@intLogSourceTypeID";
                paramLogSourceTypeID.SqlDbType = SqlDbType.Int;
                paramLogSourceTypeID.Direction = ParameterDirection.Input;
                paramLogSourceTypeID.Value = objClaimsDocsLog.LogSourceTypeID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramLogSourceTypeID);

                //Add input parameter for (objClaimsDocsLog.LogTypeID) and set its properties
                SqlParameter paramLogTypeID = new SqlParameter();
                paramLogTypeID.ParameterName = "@intLogTypeID";
                paramLogTypeID.SqlDbType = SqlDbType.Int;
                paramLogTypeID.Direction = ParameterDirection.Input;
                paramLogTypeID.Value = objClaimsDocsLog.LogTypeID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramLogTypeID);

                //Add input parameter for (objClaimsDocsLog.MessageIs) and set its properties
                SqlParameter paramMessageIs = new SqlParameter();
                paramMessageIs.ParameterName = "@txtMessageIs";
                paramMessageIs.SqlDbType = SqlDbType.Text;
                paramMessageIs.Direction = ParameterDirection.Input;
                paramMessageIs.Value = objClaimsDocsLog.MessageIs;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramMessageIs);

                //Add input parameter for (objClaimsDocsLog.ExceptionIs) and set its properties
                SqlParameter paramExceptionIs = new SqlParameter();
                paramExceptionIs.ParameterName = "@txtExceptionIs";
                paramExceptionIs.SqlDbType = SqlDbType.Text;
                paramExceptionIs.Direction = ParameterDirection.Input;
                paramExceptionIs.Value = objClaimsDocsLog.ExceptionIs;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramExceptionIs);

                //Add input parameter for (objClaimsDocsLog.StackTraceIs) and set its properties
                SqlParameter paramStackTraceIs = new SqlParameter();
                paramStackTraceIs.ParameterName = "@txtStackTraceIs";
                paramStackTraceIs.SqlDbType = SqlDbType.Text;
                paramStackTraceIs.Direction = ParameterDirection.Input;
                paramStackTraceIs.Value = objClaimsDocsLog.StackTraceIs;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramStackTraceIs);

                //Add input parameter for (objClaimsDocsLog.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objClaimsDocsLog.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objClaimsDocsLog.Result) and set its properties
                SqlParameter paramResult = new SqlParameter();
                paramResult.ParameterName = "@intResult";
                paramResult.SqlDbType = SqlDbType.Int;
                paramResult.Direction = ParameterDirection.InputOutput;
                paramResult.Value = intResult;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramResult);

                //open connection
                cnnConnection.Open();
                //execute command
                cmdCommand.ExecuteNonQuery();
                //get result
                intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());

            }//end try
            catch (Exception ex)
            {
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : ClaimsDocsLogCreate() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objSupport.ClaimsDocsLogCreate(objClaimsLog, AppConfig.CorrespondenceDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objSupport = null;

            }
            finally
            {
                //cleanup connection
                if (cnnConnection.State == ConnectionState.Open)
                {
                    cnnConnection.Close();
                }
                //cleanup command
                if (cmdCommand != null)
                {
                    cmdCommand = null;
                }
            }
            //return result
            return (intResult);
        }//end method : ClaimsDocsLogCreate

        //define : ClaimsDocsLogSearch
        public IEnumerable<ClaimsDocsLog> ClaimsDocsLogSearch(string strSQLString, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = strSQLString;
                cmdCommand.CommandType = CommandType.Text;

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //fill Claims Docs Log object
                    while (rdrReader.Read())
                    {
                        //create new Claims Docs Log Object
                        ClaimsDocsLog objClaimsDocsLog = new ClaimsDocsLog();
                        //fill objClaimsDocsLog object
                        objClaimsDocsLog.ClaimsDocsLogID = int.Parse(rdrReader["ClaimsDocsLogID"].ToString());
                        objClaimsDocsLog.LogSourceTypeID = int.Parse(rdrReader["LogSourceTypeID"].ToString());
                        objClaimsDocsLog.LogTypeID = int.Parse(rdrReader["LogTypeID"].ToString());
                        objClaimsDocsLog.MessageIs = rdrReader["MessageIs"].ToString();
                        objClaimsDocsLog.ExceptionIs = rdrReader["ExceptionIs"].ToString();
                        objClaimsDocsLog.StackTraceIs = rdrReader["StackTraceIs"].ToString();
                        objClaimsDocsLog.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());

                        //add user to user list
                        this._listClaimsDocsLog.Add(objClaimsDocsLog);
                    }
                }

            }//end try
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : ClaimsDocsLogSearch() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objSupport.ClaimsDocsLogCreate(objClaimsLog, AppConfig.CorrespondenceDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objSupport = null;
            }
            finally
            {
                //check reader
                if (rdrReader != null)
                {
                    if (!rdrReader.IsClosed)
                    {
                        rdrReader.Close();
                    }
                    rdrReader = null;
                }

                //cleanup connection
                if (cnnConnection.State == ConnectionState.Open)
                {
                    cnnConnection.Close();
                }
                //cleanup command
                if (cmdCommand != null)
                {
                    cmdCommand = null;
                }
            }
            //return result

            //return result
            return (this._listClaimsDocsLog);
        }//end : ClaimsDocsLogSearch


    }//end : public class AppSupport
}//end : namespace ClaimsDocsClient.AppClasses
