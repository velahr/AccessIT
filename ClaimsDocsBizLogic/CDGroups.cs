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
    //define Group Class
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CDGroups : ICDGroups
    {
        //declare private class variables
        private List<Group> _listGroup = new List<Group>();

        #region Group Methods

        //define method : GroupCreate
        public int GroupCreate(Group objGroup, string strConnectionString)
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
                cmdCommand.CommandText = "spGroupCreate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objGroup.GroupID) and set its properties
                SqlParameter paramGroupID = new SqlParameter();
                paramGroupID.ParameterName = "@intGroupID";
                paramGroupID.SqlDbType = SqlDbType.Int;
                paramGroupID.Direction = ParameterDirection.Input;
                paramGroupID.Value = objGroup.GroupID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGroupID);

                //Add input parameter for (objGroup.DepartmentID) and set its properties
                SqlParameter paramDepartmentID = new SqlParameter();
                paramDepartmentID.ParameterName = "@intDepartmentID";
                paramDepartmentID.SqlDbType = SqlDbType.Int;
                paramDepartmentID.Direction = ParameterDirection.Input;
                paramDepartmentID.Value = objGroup.DepartmentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDepartmentID);

                //Add input parameter for (objGroup.GroupName) and set its properties
                SqlParameter paramGroupName = new SqlParameter();
                paramGroupName.ParameterName = "@vchrGroupName";
                paramGroupName.SqlDbType = SqlDbType.VarChar;
                paramGroupName.Direction = ParameterDirection.Input;
                paramGroupName.Value = objGroup.GroupName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGroupName);

                //Add input parameter for (objGroup.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objGroup.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objGroup.Result) and set its properties
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
                objClaimsLog.MessageIs = "Method : GroupCreate() ";
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
        }//end method : GroupCreate

        //define method : GroupRead
        public Group GroupRead(Group objGroup, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intResult = 0;
            Group objGroupIs = new Group();

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spGroupRead";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objGroup.GroupID) and set its properties
                SqlParameter paramGroupID = new SqlParameter();
                paramGroupID.ParameterName = "@intGroupID";
                paramGroupID.SqlDbType = SqlDbType.Int;
                paramGroupID.Direction = ParameterDirection.Input;
                paramGroupID.Value = objGroup.GroupID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGroupID);

                //Add input parameter for (objGroup.Result) and set its properties
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

                //check for record
                if (rdrReader.HasRows == true)
                {
                    //fill group object
                    while (rdrReader.Read())
                    {
                        objGroupIs.GroupID = int.Parse(rdrReader["GroupID"].ToString());
                        objGroupIs.GroupName = rdrReader["GroupName"].ToString();
                        objGroupIs.DepartmentName = rdrReader["DepartmentName"].ToString();
                        objGroupIs.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                        objGroupIs.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
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
                objClaimsLog.MessageIs = "Method : GroupRead() ";
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
                //close reader
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
            return (objGroupIs);
        }//end method : GroupRead

        //define method : GroupUpdate
        public int GroupUpdate(Group objGroup, string strConnectionString)
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
                cmdCommand.CommandText = "spGroupUpdate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objGroup.GroupID) and set its properties
                SqlParameter paramGroupID = new SqlParameter();
                paramGroupID.ParameterName = "@intGroupID";
                paramGroupID.SqlDbType = SqlDbType.Int;
                paramGroupID.Direction = ParameterDirection.Input;
                paramGroupID.Value = objGroup.GroupID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGroupID);

                //Add input parameter for (objGroup.DepartmentID) and set its properties
                SqlParameter paramDepartmentID = new SqlParameter();
                paramDepartmentID.ParameterName = "@intDepartmentID";
                paramDepartmentID.SqlDbType = SqlDbType.Int;
                paramDepartmentID.Direction = ParameterDirection.Input;
                paramDepartmentID.Value = objGroup.DepartmentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDepartmentID);

                //Add input parameter for (objGroup.GroupName) and set its properties
                SqlParameter paramGroupName = new SqlParameter();
                paramGroupName.ParameterName = "@vchrGroupName";
                paramGroupName.SqlDbType = SqlDbType.VarChar;
                paramGroupName.Direction = ParameterDirection.Input;
                paramGroupName.Value = objGroup.GroupName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGroupName);

                //Add input parameter for (objGroup.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objGroup.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objGroup.Result) and set its properties
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
                objClaimsLog.MessageIs = "Method : GroupUpdate() ";
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
        }//end method : GroupUpdate

        //define : GroupSearch
        public List<Group> GroupSearch(string strSQLString, string strConnectionString)
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
                        //create new group object
                        Group objGroupIs = new Group();
                        //fill user object
                        objGroupIs.GroupID = int.Parse(rdrReader["GroupID"].ToString());
                        objGroupIs.GroupName = rdrReader["GroupName"].ToString();
                        objGroupIs.DepartmentName = rdrReader["DepartmentName"].ToString();
                        objGroupIs.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                        objGroupIs.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
                        //add user to user list
                        this._listGroup.Add(objGroupIs);
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
                objClaimsLog.MessageIs = "Method : GroupSearch() ";
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
                //close reader
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
            return (this._listGroup);
        }//end : GroupSearch

        #endregion

    }//end : public class CDGroups : ICDGroups
}//end : namespace ClaimsDocsBizLogic
