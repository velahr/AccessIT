using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace ClaimsDocsBizLogic
{
    //define class :  CDDepartments
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CDDepartments : ICDDepartments
    {
        //declare private class variables
        private List<Department> _listDepartment = new List<Department>();

        #region Department Methods

        //define method : DepartmentCreate
        public int DepartmentCreate(Department objDepartment, string strConnectionString)
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
                cmdCommand.CommandText = "spDepartmentCreate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDepartment.DepartmentID) and set its properties
                SqlParameter paramDepartmentID = new SqlParameter();
                paramDepartmentID.ParameterName = "@intDepartmentID";
                paramDepartmentID.SqlDbType = SqlDbType.Int;
                paramDepartmentID.Direction = ParameterDirection.Input;
                paramDepartmentID.Value = objDepartment.DepartmentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDepartmentID);

                //Add input parameter for (objDepartment.DepartmentName) and set its properties
                SqlParameter paramDepartmentName = new SqlParameter();
                paramDepartmentName.ParameterName = "@vchrDepartmentName";
                paramDepartmentName.SqlDbType = SqlDbType.VarChar;
                paramDepartmentName.Direction = ParameterDirection.Input;
                paramDepartmentName.Value = objDepartment.DepartmentName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDepartmentName);

                //Add input parameter for (objDepartment.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objDepartment.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objDepartment.Result) and set its properties
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
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DepartmentCreate() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
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
        }//end method : DepartmentCreate

        //define method : DepartmentRead
        public Department DepartmentRead(Department objDepartment, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intResult = 0;
            Department objDepartmentIs = new Department();

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDepartmentRead";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDepartment.DepartmentID) and set its properties
                SqlParameter paramDepartmentID = new SqlParameter();
                paramDepartmentID.ParameterName = "@intDepartmentID";
                paramDepartmentID.SqlDbType = SqlDbType.Int;
                paramDepartmentID.Direction = ParameterDirection.Input;
                paramDepartmentID.Value = objDepartment.DepartmentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDepartmentID);

                //Add input parameter for (objDepartment.Result) and set its properties
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
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //fill user object
                    while (rdrReader.Read())
                    {
                        //fill user object
                        objDepartmentIs.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                        objDepartmentIs.DepartmentName = rdrReader["DepartmentName"].ToString();
                        objDepartmentIs.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
                    }
                }

            }//end try
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DepartmentRead() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
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
            return (objDepartmentIs);
        }//end method : DepartmentRead

        //define method : DepartmentUpdate
        public int DepartmentUpdate(Department objDepartment, string strConnectionString)
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
                cmdCommand.CommandText = "spDepartmentUpdate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDepartment.DepartmentID) and set its properties
                SqlParameter paramDepartmentID = new SqlParameter();
                paramDepartmentID.ParameterName = "@intDepartmentID";
                paramDepartmentID.SqlDbType = SqlDbType.Int;
                paramDepartmentID.Direction = ParameterDirection.Input;
                paramDepartmentID.Value = objDepartment.DepartmentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDepartmentID);

                //Add input parameter for (objDepartment.DepartmentName) and set its properties
                SqlParameter paramDepartmentName = new SqlParameter();
                paramDepartmentName.ParameterName = "@vchrDepartmentName";
                paramDepartmentName.SqlDbType = SqlDbType.VarChar;
                paramDepartmentName.Direction = ParameterDirection.Input;
                paramDepartmentName.Value = objDepartment.DepartmentName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDepartmentName);

                //Add input parameter for (objDepartment.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objDepartment.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objDepartment.Result) and set its properties
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
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DepartmentUpdate() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
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
        }//end method : DepartmentUpdate

        //define : DepartmentSearch
        public List<Department> DepartmentSearch(string strSQLString, string strConnectionString)
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
                    //fill user object
                    while (rdrReader.Read())
                    {
                        //create new department object
                        Department objDepartment = new Department();
                        //fill department object
                        objDepartment.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                        objDepartment.DepartmentName = rdrReader["DepartmentName"].ToString();
                        objDepartment.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
                        //add user to department list
                        this._listDepartment.Add(objDepartment);
                    }
                }

            }//end try
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DepartmentSearch() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
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
            return (this._listDepartment);
        }//end : DepartmentSearch

        #endregion
    }
}
