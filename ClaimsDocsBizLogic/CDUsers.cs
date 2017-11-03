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
    //define CDUsers class
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CDUsers : ICDUsers
    {
        //declare private class variables
        private List<User> _lstUser = new List<User>();
        private List<UserGroup> _lstUserGroup = new List<UserGroup>();

        #region User Methods

        //define method : UserLogin
        public User UserLogin(User objUser, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            StringBuilder sbrSQL = new StringBuilder();
            User objUserIs = null;

            //start try
            try
            {
                //build search string
                sbrSQL.Append("Select *, dbo.tblDepartment.DepartmentName From dbo.tblUser ");
                sbrSQL.Append("Inner Join dbo.tblDepartment on dbo.tblDepartment.DepartmentID = dbo.tblUser.DepartmentID ");
                sbrSQL.Append("Where UserName='" + objUser.UserName + "' And UserPassword='" + objUser.UserPassword + "'");

                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = sbrSQL.ToString();
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
                        //check case
                        if (
                            (string.Compare(objUser.UserName, rdrReader["UserName"].ToString(), false) == 0)
                            &&
                            (string.Compare(objUser.UserPassword, rdrReader["UserPassword"].ToString(), false) == 0)
                            )
                        {

                            objUserIs = new User();
                            //get user details
                            objUserIs.UserID = int.Parse(rdrReader["UserID"].ToString());
                            objUserIs.UserName = rdrReader["UserName"].ToString();
                            objUserIs.UserPassword = rdrReader["UserPassword"].ToString();
                            objUserIs.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                            objUserIs.DepartmentName = rdrReader["DepartmentName"].ToString();
                            objUserIs.Approver = rdrReader["Approver"].ToString();
                            objUserIs.Designer = rdrReader["Designer"].ToString();
                            objUserIs.Administrator = rdrReader["Administrator"].ToString();
                            objUserIs.Reviewer = rdrReader["Reviewer"].ToString();
                            objUserIs.Title = rdrReader["Title"].ToString();
                            objUserIs.Phone = rdrReader["Phone"].ToString();
                            objUserIs.Signature = rdrReader["Signature"].ToString();
                            objUserIs.Active = rdrReader["Active"].ToString();
                            objUserIs.SignatureName = rdrReader["SignatureName"].ToString();
                            objUserIs.EMailAddress = rdrReader["EMailAddress"].ToString();
                        }
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
                objClaimsLog.MessageIs = "Method : UserLogin() ";
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
            return (objUserIs);
        }//end : UserLogin

        //define method : UserCreate
        public int UserCreate(User objUser, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlTransaction tranTransaction = null;
            int intUserID = 0;
            int intResult = 0;
            int intIndex = 0;

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //open connection
                cnnConnection.Open();
                //start Location Transaction
                tranTransaction = cnnConnection.BeginTransaction("ClaimsDocsCreateUser");

                #region Create User
               
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.Transaction = tranTransaction;
                cmdCommand.CommandText = "spUserCreate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objUser.UserID) and set its properties
                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@intUserID";
                paramUserID.SqlDbType = SqlDbType.Int;
                paramUserID.Direction = ParameterDirection.Input;
                paramUserID.Value = objUser.UserID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramUserID);

                //Add input parameter for (objUser.UserName) and set its properties
                SqlParameter paramUserName = new SqlParameter();
                paramUserName.ParameterName = "@vchrUserName";
                paramUserName.SqlDbType = SqlDbType.VarChar;
                paramUserName.Direction = ParameterDirection.Input;
                paramUserName.Value = objUser.UserName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramUserName);

                //Add input parameter for (objUser.UserPassword) and set its properties
                SqlParameter paramUserPassword = new SqlParameter();
                paramUserPassword.ParameterName = "@vchrUserPassword";
                paramUserPassword.SqlDbType = SqlDbType.VarChar;
                paramUserPassword.Direction = ParameterDirection.Input;
                paramUserPassword.Value = objUser.UserPassword;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramUserPassword);

                //Add input parameter for (objUser.DepartmentID) and set its properties
                SqlParameter paramDepartmentID = new SqlParameter();
                paramDepartmentID.ParameterName = "@intDepartmentID";
                paramDepartmentID.SqlDbType = SqlDbType.Int;
                paramDepartmentID.Direction = ParameterDirection.Input;
                paramDepartmentID.Value = objUser.DepartmentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDepartmentID);

                //Add input parameter for (objUser.Approver) and set its properties
                SqlParameter paramApprover = new SqlParameter();
                paramApprover.ParameterName = "@vchrApprover";
                paramApprover.SqlDbType = SqlDbType.VarChar;
                paramApprover.Direction = ParameterDirection.Input;
                paramApprover.Value = objUser.Approver;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramApprover);

                //Add input parameter for (objUser.Designer) and set its properties
                SqlParameter paramDesigner = new SqlParameter();
                paramDesigner.ParameterName = "@vchrDesigner";
                paramDesigner.SqlDbType = SqlDbType.VarChar;
                paramDesigner.Direction = ParameterDirection.Input;
                paramDesigner.Value = objUser.Designer;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDesigner);

                //Add input parameter for (objUser.Administrator) and set its properties
                SqlParameter paramAdministrator = new SqlParameter();
                paramAdministrator.ParameterName = "@vchrAdministrator";
                paramAdministrator.SqlDbType = SqlDbType.VarChar;
                paramAdministrator.Direction = ParameterDirection.Input;
                paramAdministrator.Value = objUser.Administrator;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramAdministrator);

                //Add input parameter for (objUser.Reviewer) and set its properties
                SqlParameter paramReviewer = new SqlParameter();
                paramReviewer.ParameterName = "@vchrReviewer";
                paramReviewer.SqlDbType = SqlDbType.VarChar;
                paramReviewer.Direction = ParameterDirection.Input;
                paramReviewer.Value = objUser.Reviewer;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramReviewer);

                //Add input parameter for (objUser.Title) and set its properties
                SqlParameter paramTitle = new SqlParameter();
                paramTitle.ParameterName = "@vchrTitle";
                paramTitle.SqlDbType = SqlDbType.VarChar;
                paramTitle.Direction = ParameterDirection.Input;
                paramTitle.Value = objUser.Title;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramTitle);

                //Add input parameter for (objUser.Phone) and set its properties
                SqlParameter paramPhone = new SqlParameter();
                paramPhone.ParameterName = "@vchrPhone";
                paramPhone.SqlDbType = SqlDbType.VarChar;
                paramPhone.Direction = ParameterDirection.Input;
                paramPhone.Value = objUser.Phone;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPhone);

                //Add input parameter for (objUser.Signature) and set its properties
                SqlParameter paramSignature = new SqlParameter();
                paramSignature.ParameterName = "@vchrSignature";
                paramSignature.SqlDbType = SqlDbType.VarChar;
                paramSignature.Direction = ParameterDirection.Input;
                paramSignature.Value = objUser.Signature;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramSignature);

                //Add input parameter for (objUser.Active) and set its properties
                SqlParameter paramActive = new SqlParameter();
                paramActive.ParameterName = "@vchrActive";
                paramActive.SqlDbType = SqlDbType.VarChar;
                paramActive.Direction = ParameterDirection.Input;
                paramActive.Value = objUser.Active;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramActive);

                //Add input parameter for (objUser.SignatureName) and set its properties
                SqlParameter paramSignatureName = new SqlParameter();
                paramSignatureName.ParameterName = "@vchrSignatureName";
                paramSignatureName.SqlDbType = SqlDbType.VarChar;
                paramSignatureName.Direction = ParameterDirection.Input;
                paramSignatureName.Value = objUser.SignatureName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramSignatureName);

                //Add input parameter for (objUser.EMailAddress) and set its properties
                SqlParameter paramEMailAddress = new SqlParameter();
                paramEMailAddress.ParameterName = "@vchrEMailAddress";
                paramEMailAddress.SqlDbType = SqlDbType.VarChar;
                paramEMailAddress.Direction = ParameterDirection.Input;
                paramEMailAddress.Value = objUser.EMailAddress;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramEMailAddress);

                //Add input parameter for (objUser.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objUser.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objUser.Result) and set its properties
                SqlParameter paramResult = new SqlParameter();
                paramResult.ParameterName = "@intResult";
                paramResult.SqlDbType = SqlDbType.Int;
                paramResult.Direction = ParameterDirection.InputOutput;
                paramResult.Value = intResult;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramResult);
               
                //execute command
                cmdCommand.ExecuteNonQuery();
                //get result
                intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());

                #endregion


                //check results
                if (intResult != -1)
                {
                    #region Create User Groups

                    //get user id from result
                    intUserID = intResult;

                    ////reset result
                    //intResult = 0;

                    for (intIndex = 0; intIndex < objUser.listUserGroup.Count; intIndex++)
                    {
                        //Create the command and set its properties
                        cmdCommand.Parameters.Clear();
                        cmdCommand.Connection = cnnConnection;
                        cmdCommand.Transaction = tranTransaction;
                        cmdCommand.CommandText = "spUserGroupCreate";
                        cmdCommand.CommandType = CommandType.StoredProcedure;

                        //add command parameters		
                        //Add input parameter for (objUserGroup.UserID) and set its properties
                        SqlParameter paramUGUserID = new SqlParameter();
                        paramUGUserID.ParameterName = "@intUserID";
                        paramUGUserID.SqlDbType = SqlDbType.Int;
                        paramUGUserID.Direction = ParameterDirection.Input;
                        paramUGUserID.Value = intUserID; //this._lstUserGroup[intIndex].UserID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramUGUserID);

                        //Add input parameter for (objUserGroup.GroupID) and set its properties
                        SqlParameter paramUGGroupID = new SqlParameter();
                        paramUGGroupID.ParameterName = "@intGroupID";
                        paramUGGroupID.SqlDbType = SqlDbType.Int;
                        paramUGGroupID.Direction = ParameterDirection.Input;
                        paramUGGroupID.Value = objUser.listUserGroup[intIndex].GroupID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramUGGroupID);

                        //Add input parameter for (objUserGroup.IUDateTime) and set its properties
                        SqlParameter paramUGIUDateTime = new SqlParameter();
                        paramUGIUDateTime.ParameterName = "@datIUDateTime";
                        paramUGIUDateTime.SqlDbType = SqlDbType.DateTime;
                        paramUGIUDateTime.Direction = ParameterDirection.Input;
                        paramUGIUDateTime.Value = objUser.listUserGroup[intIndex].IUDateTime;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramUGIUDateTime);

                        //Add input parameter for (objUserGroup.Result) and set its properties
                        SqlParameter paramUGResult = new SqlParameter();
                        paramUGResult.ParameterName = "@intResult";
                        paramUGResult.SqlDbType = SqlDbType.Int;
                        paramUGResult.Direction = ParameterDirection.InputOutput;
                        paramUGResult.Value = intResult;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramUGResult);

                        //execute command
                        cmdCommand.ExecuteNonQuery();
                        //get result
                        intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());

                    }//end : for (intIndex = 0; intIndex < this._lstUserGroup.Count; intIndex++)

                    #endregion
                }

                //check results
                if (intResult != -1)
                {
                    //commit transaction
                    tranTransaction.Commit();
                }
                else
                {
                    //rollback transaction
                    tranTransaction.Rollback();
                }


            }//end try
            catch (Exception ex)
            {
                //rollback transaction
                tranTransaction.Rollback();
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : UserCreate() ";
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
        }//end method : UserCreate

        //define method : UserRead
        public User UserRead(User objUser, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intResult = 0;
            User objUserIs = new User();

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spUserRead";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objUser.UserID) and set its properties
                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@intUserID";
                paramUserID.SqlDbType = SqlDbType.Int;
                paramUserID.Direction = ParameterDirection.Input;
                paramUserID.Value = objUser.UserID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramUserID);

                //Add input parameter for (objUser.Result) and set its properties
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
                        objUserIs.UserID = int.Parse(rdrReader["UserID"].ToString());
                        objUserIs.UserName = rdrReader["UserName"].ToString();
                        objUserIs.DepartmentName = rdrReader["DepartmentName"].ToString();
                        objUserIs.UserPassword = rdrReader["UserPassword"].ToString();
                        objUserIs.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                        objUserIs.Approver = rdrReader["Approver"].ToString();
                        objUserIs.Designer = rdrReader["Designer"].ToString();
                        objUserIs.Administrator = rdrReader["Administrator"].ToString();
                        objUserIs.Reviewer = rdrReader["Reviewer"].ToString();
                        objUserIs.Title = rdrReader["Title"].ToString();
                        objUserIs.Phone = rdrReader["Phone"].ToString();
                        objUserIs.Signature = rdrReader["Signature"].ToString();
                        objUserIs.Active = rdrReader["Active"].ToString();
                        objUserIs.SignatureName = rdrReader["SignatureName"].ToString();
                        objUserIs.EMailAddress = rdrReader["EMailAddress"].ToString();
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
                objClaimsLog.MessageIs = "Method : UserRead() ";
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
            return (objUserIs);
        }//end method : UserRead

        //define method : UserReadByUserName
        public User UserReadByUserName(User objUser, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intResult = 0;
            User objUserIs = new User();

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spUserReadByUserName";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parametersspUserReadByUserName
                //Add input parameter for (paramUserName) and set its properties
                SqlParameter paramUserName = new SqlParameter();
                paramUserName.ParameterName = "@varUserName";
                paramUserName.SqlDbType = SqlDbType.VarChar;
                paramUserName.Direction = ParameterDirection.Input;
                paramUserName.Value = objUser.UserName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramUserName);

                //Add input parameter for (objUser.Result) and set its properties
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
                        objUserIs.UserID = int.Parse(rdrReader["UserID"].ToString());
                        objUserIs.UserName = rdrReader["UserName"].ToString();
                        objUserIs.DepartmentName = rdrReader["DepartmentName"].ToString();
                        objUserIs.UserPassword = rdrReader["UserPassword"].ToString();
                        objUserIs.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                        objUserIs.Approver = rdrReader["Approver"].ToString();
                        objUserIs.Designer = rdrReader["Designer"].ToString();
                        objUserIs.Administrator = rdrReader["Administrator"].ToString();
                        objUserIs.Reviewer = rdrReader["Reviewer"].ToString();
                        objUserIs.Title = rdrReader["Title"].ToString();
                        objUserIs.Phone = rdrReader["Phone"].ToString();
                        objUserIs.Signature = rdrReader["Signature"].ToString();
                        objUserIs.Active = rdrReader["Active"].ToString();
                        objUserIs.SignatureName = rdrReader["SignatureName"].ToString();
                        objUserIs.EMailAddress = rdrReader["EMailAddress"].ToString();
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
                objClaimsLog.MessageIs = "Method : UserReadByUserName() ";
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
            return (objUserIs);
        }//end method : UserReadByUserName

        //define method : UserUpdate
        public int UserUpdate(User objUser, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlTransaction tranTransaction = null;
            int intResult = 0;
            int intIndex = 0;

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //open connection
                cnnConnection.Open();
                //start Location Transaction
                tranTransaction = cnnConnection.BeginTransaction("ClaimsDocsUpdateUser");

                #region Clear User Groups

                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.Transaction = tranTransaction;
                cmdCommand.CommandText = "spUserGroupClear";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objUserGroup.UserID) and set its properties
                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@intUserID";
                paramUserID.SqlDbType = SqlDbType.Int;
                paramUserID.Direction = ParameterDirection.Input;
                paramUserID.Value = objUser.UserID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramUserID);

                //Add input parameter for (objUserGroup.Result) and set its properties
                SqlParameter paramResult = new SqlParameter();
                paramResult.ParameterName = "@intResult";
                paramResult.SqlDbType = SqlDbType.Int;
                paramResult.Direction = ParameterDirection.InputOutput;
                paramResult.Value = intResult;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramResult);

                //execute command
                cmdCommand.ExecuteNonQuery();
                //get result
                intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());

                #endregion


                 //check results
                if (intResult != -1)
                {
                    #region Update User Groups

                    //reset result
                    intResult = 0;

                    //Create the command and set its properties
                    cmdCommand.Parameters.Clear();
                    cmdCommand.Connection = cnnConnection;
                    cmdCommand.Transaction = tranTransaction;
                    cmdCommand.CommandText = "spUserUpdate";
                    cmdCommand.CommandType = CommandType.StoredProcedure;

                    //add command parameters		
                    //Add input parameter for (objUser.UserID) and set its properties
                    SqlParameter paramCDUserID = new SqlParameter();
                    paramCDUserID.ParameterName = "@intUserID";
                    paramCDUserID.SqlDbType = SqlDbType.Int;
                    paramCDUserID.Direction = ParameterDirection.Input;
                    paramCDUserID.Value = objUser.UserID;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramCDUserID);

                    //Add input parameter for (objUser.UserName) and set its properties
                    SqlParameter paramUserName = new SqlParameter();
                    paramUserName.ParameterName = "@vchrUserName";
                    paramUserName.SqlDbType = SqlDbType.VarChar;
                    paramUserName.Direction = ParameterDirection.Input;
                    paramUserName.Value = objUser.UserName;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramUserName);

                    //Add input parameter for (objUser.UserPassword) and set its properties
                    SqlParameter paramUserPassword = new SqlParameter();
                    paramUserPassword.ParameterName = "@vchrUserPassword";
                    paramUserPassword.SqlDbType = SqlDbType.VarChar;
                    paramUserPassword.Direction = ParameterDirection.Input;
                    paramUserPassword.Value = objUser.UserPassword;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramUserPassword);

                    //Add input parameter for (objUser.DepartmentID) and set its properties
                    SqlParameter paramDepartmentID = new SqlParameter();
                    paramDepartmentID.ParameterName = "@intDepartmentID";
                    paramDepartmentID.SqlDbType = SqlDbType.Int;
                    paramDepartmentID.Direction = ParameterDirection.Input;
                    paramDepartmentID.Value = objUser.DepartmentID;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDepartmentID);

                    //Add input parameter for (objUser.Approver) and set its properties
                    SqlParameter paramApprover = new SqlParameter();
                    paramApprover.ParameterName = "@vchrApprover";
                    paramApprover.SqlDbType = SqlDbType.VarChar;
                    paramApprover.Direction = ParameterDirection.Input;
                    paramApprover.Value = objUser.Approver;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramApprover);

                    //Add input parameter for (objUser.Designer) and set its properties
                    SqlParameter paramDesigner = new SqlParameter();
                    paramDesigner.ParameterName = "@vchrDesigner";
                    paramDesigner.SqlDbType = SqlDbType.VarChar;
                    paramDesigner.Direction = ParameterDirection.Input;
                    paramDesigner.Value = objUser.Designer;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDesigner);

                    //Add input parameter for (objUser.Administrator) and set its properties
                    SqlParameter paramAdministrator = new SqlParameter();
                    paramAdministrator.ParameterName = "@vchrAdministrator";
                    paramAdministrator.SqlDbType = SqlDbType.VarChar;
                    paramAdministrator.Direction = ParameterDirection.Input;
                    paramAdministrator.Value = objUser.Administrator;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramAdministrator);

                    //Add input parameter for (objUser.Reviewer) and set its properties
                    SqlParameter paramReviewer = new SqlParameter();
                    paramReviewer.ParameterName = "@vchrReviewer";
                    paramReviewer.SqlDbType = SqlDbType.VarChar;
                    paramReviewer.Direction = ParameterDirection.Input;
                    paramReviewer.Value = objUser.Reviewer;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramReviewer);

                    //Add input parameter for (objUser.Title) and set its properties
                    SqlParameter paramTitle = new SqlParameter();
                    paramTitle.ParameterName = "@vchrTitle";
                    paramTitle.SqlDbType = SqlDbType.VarChar;
                    paramTitle.Direction = ParameterDirection.Input;
                    paramTitle.Value = objUser.Title;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramTitle);

                    //Add input parameter for (objUser.Phone) and set its properties
                    SqlParameter paramPhone = new SqlParameter();
                    paramPhone.ParameterName = "@vchrPhone";
                    paramPhone.SqlDbType = SqlDbType.VarChar;
                    paramPhone.Direction = ParameterDirection.Input;
                    paramPhone.Value = objUser.Phone;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramPhone);

                    //Add input parameter for (objUser.Signature) and set its properties
                    SqlParameter paramSignature = new SqlParameter();
                    paramSignature.ParameterName = "@vchrSignature";
                    paramSignature.SqlDbType = SqlDbType.VarChar;
                    paramSignature.Direction = ParameterDirection.Input;
                    paramSignature.Value = objUser.Signature;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramSignature);

                    //Add input parameter for (objUser.Active) and set its properties
                    SqlParameter paramActive = new SqlParameter();
                    paramActive.ParameterName = "@vchrActive";
                    paramActive.SqlDbType = SqlDbType.VarChar;
                    paramActive.Direction = ParameterDirection.Input;
                    paramActive.Value = objUser.Active;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramActive);

                    //Add input parameter for (objUser.SignatureName) and set its properties
                    SqlParameter paramSignatureName = new SqlParameter();
                    paramSignatureName.ParameterName = "@vchrSignatureName";
                    paramSignatureName.SqlDbType = SqlDbType.VarChar;
                    paramSignatureName.Direction = ParameterDirection.Input;
                    paramSignatureName.Value = objUser.SignatureName;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramSignatureName);

                    //Add input parameter for (objUser.EMailAddress) and set its properties
                    SqlParameter paramEMailAddress = new SqlParameter();
                    paramEMailAddress.ParameterName = "@vchrEMailAddress";
                    paramEMailAddress.SqlDbType = SqlDbType.VarChar;
                    paramEMailAddress.Direction = ParameterDirection.Input;
                    paramEMailAddress.Value = objUser.EMailAddress;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramEMailAddress);

                    //Add input parameter for (objUser.IUDateTime) and set its properties
                    SqlParameter paramIUDateTime = new SqlParameter();
                    paramIUDateTime.ParameterName = "@datIUDateTime";
                    paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                    paramIUDateTime.Direction = ParameterDirection.Input;
                    paramIUDateTime.Value = objUser.IUDateTime;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramIUDateTime);

                    //Add input parameter for (objUser.Result) and set its properties
                    SqlParameter paramCDResult = new SqlParameter();
                    paramCDResult.ParameterName = "@intResult";
                    paramCDResult.SqlDbType = SqlDbType.Int;
                    paramCDResult.Direction = ParameterDirection.InputOutput;
                    paramCDResult.Value = intResult;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramCDResult);

                    //execute command
                    cmdCommand.ExecuteNonQuery();
                    //get result
                    intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());
                }

                #endregion


                //check results
                if (intResult != -1)
                {
                    #region Create User Groups

                    //reset result
                    intResult = 0;

                    for (intIndex = 0; intIndex < objUser.listUserGroup.Count; intIndex++)
                    {
                        //Create the command and set its properties
                        //cmdCommand = new SqlCommand();
                        cmdCommand.Parameters.Clear();
                        cmdCommand.Connection = cnnConnection;
                        cmdCommand.Transaction = tranTransaction;
                        cmdCommand.CommandText = "spUserGroupCreate";
                        cmdCommand.CommandType = CommandType.StoredProcedure;

                        //add command parameters		
                        //Add input parameter for (objUserGroup.UserID) and set its properties
                        SqlParameter paramUGUserID = new SqlParameter();
                        paramUGUserID.ParameterName = "@intUserID";
                        paramUGUserID.SqlDbType = SqlDbType.Int;
                        paramUGUserID.Direction = ParameterDirection.Input;
                        paramUGUserID.Value = objUser.UserID; //this._lstUserGroup[intIndex].UserID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramUGUserID);

                        //Add input parameter for (objUserGroup.GroupID) and set its properties
                        SqlParameter paramUGGroupID = new SqlParameter();
                        paramUGGroupID.ParameterName = "@intGroupID";
                        paramUGGroupID.SqlDbType = SqlDbType.Int;
                        paramUGGroupID.Direction = ParameterDirection.Input;
                        paramUGGroupID.Value = objUser.listUserGroup[intIndex].GroupID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramUGGroupID);

                        //Add input parameter for (objUserGroup.IUDateTime) and set its properties
                        SqlParameter paramUGIUDateTime = new SqlParameter();
                        paramUGIUDateTime.ParameterName = "@datIUDateTime";
                        paramUGIUDateTime.SqlDbType = SqlDbType.DateTime;
                        paramUGIUDateTime.Direction = ParameterDirection.Input;
                        paramUGIUDateTime.Value = objUser.listUserGroup[intIndex].IUDateTime;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramUGIUDateTime);

                        //Add input parameter for (objUserGroup.Result) and set its properties
                        SqlParameter paramUGResult = new SqlParameter();
                        paramUGResult.ParameterName = "@intResult";
                        paramUGResult.SqlDbType = SqlDbType.Int;
                        paramUGResult.Direction = ParameterDirection.InputOutput;
                        paramUGResult.Value = intResult;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramUGResult);

                        //execute command
                        cmdCommand.ExecuteNonQuery();
                        //get result
                        intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());

                    }//end : for (intIndex = 0; intIndex < this._lstUserGroup.Count; intIndex++)

                    #endregion
                }

                //check results
                if (intResult != -1)
                {
                    //commit transaction
                    tranTransaction.Commit();
                }
                else
                {
                    //rollback transaction
                    tranTransaction.Rollback();
                }


            }//end try
            catch (Exception ex)
            {
                //rollback transaction
                tranTransaction.Rollback();
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : spUserUpdate() ";
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
        }//end method : UserUpdate

        //define : UserSearch
        public List<User> UserSearch(string strSQLString, string strConnectionString)
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
                        //create new user object
                        User objUserIs = new User();
                        //fill user object
                        objUserIs.UserID = int.Parse(rdrReader["UserID"].ToString());
                        objUserIs.UserName = rdrReader["UserName"].ToString();
                        objUserIs.DepartmentName = rdrReader["DepartmentName"].ToString();
                        objUserIs.UserPassword = rdrReader["UserPassword"].ToString();
                        objUserIs.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                        objUserIs.Approver = rdrReader["Approver"].ToString();
                        objUserIs.Designer = rdrReader["Designer"].ToString();
                        objUserIs.Administrator = rdrReader["Administrator"].ToString();
                        objUserIs.Reviewer = rdrReader["Reviewer"].ToString();
                        objUserIs.Title = rdrReader["Title"].ToString();
                        objUserIs.Phone = rdrReader["Phone"].ToString();
                        objUserIs.Signature = rdrReader["Signature"].ToString();
                        objUserIs.Active = rdrReader["Active"].ToString();
                        objUserIs.SignatureName = rdrReader["SignatureName"].ToString();
                        objUserIs.EMailAddress = rdrReader["EMailAddress"].ToString();
                        objUserIs.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
                        //add user to user list
                        this._lstUser.Add(objUserIs);
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
                objClaimsLog.MessageIs = "Method : UserSearch() ";
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
            return (this._lstUser);
        }//end : UserSearch

        //define : UserGroupSearch
        public List<UserGroup> UserGroupSearch(int intUserID,string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intResult = 0;

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spUserGroupList";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (UserID) and set its properties
                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@intUserID";
                paramUserID.SqlDbType = SqlDbType.Int;
                paramUserID.Direction = ParameterDirection.Input;
                paramUserID.Value = intUserID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramUserID);

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

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //fill user object
                    while (rdrReader.Read())
                    {
                        //create new user object
                        UserGroup objUserGroupIs = new UserGroup();
                        //fill user object
                        objUserGroupIs.UserID = int.Parse(rdrReader["UserID"].ToString());
                        objUserGroupIs.GroupID = int.Parse(rdrReader["GroupID"].ToString());
                        objUserGroupIs.GroupName = rdrReader["GroupName"].ToString();
                        objUserGroupIs.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                        objUserGroupIs.DepartmentName = rdrReader["DepartmentName"].ToString();
                        objUserGroupIs.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
                        //add user to user list
                        this._lstUserGroup.Add(objUserGroupIs);
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
                objClaimsLog.MessageIs = "Method : UserSearch() ";
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
            return (this._lstUserGroup);
        }//end : UserSearch

        //define : UserGroupSearchByUserName
        public List<UserGroup> UserGroupSearchByUserName(string strUserName, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intResult = 0;

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spUserGroupListByUserName";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (UserID) and set its properties
                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@varUserName";
                paramUserID.SqlDbType = SqlDbType.VarChar;
                paramUserID.Direction = ParameterDirection.Input;
                paramUserID.Value = strUserName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramUserID);

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

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //fill user object
                    while (rdrReader.Read())
                    {
                        //create new user object
                        UserGroup objUserGroupIs = new UserGroup();
                        //fill user object
                        objUserGroupIs.UserID = int.Parse(rdrReader["UserID"].ToString());
                        objUserGroupIs.GroupID = int.Parse(rdrReader["GroupID"].ToString());
                        objUserGroupIs.GroupName = rdrReader["GroupName"].ToString();
                        objUserGroupIs.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                        objUserGroupIs.DepartmentName = rdrReader["DepartmentName"].ToString();
                        objUserGroupIs.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
                        //add user to user list
                        this._lstUserGroup.Add(objUserGroupIs);
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
                objClaimsLog.MessageIs = "Method : UserGroupSearchByUserName() ";
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
            return (this._lstUserGroup);
        }//end : UserGroupSearchByUserName

        //define method : UserGroupClear
        public int UserGroupClear(UserGroup objUserGroup, string strConnectionString)
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
                cmdCommand.CommandText = "spUserGroupClear";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objUserGroup.UserID) and set its properties
                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@intUserID";
                paramUserID.SqlDbType = SqlDbType.Int;
                paramUserID.Direction = ParameterDirection.Input;
                paramUserID.Value = objUserGroup.UserID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramUserID);

                //Add input parameter for (objUserGroup.Result) and set its properties
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
                objClaimsLog.MessageIs = "Method : UserGroupClear() ";
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
        }//end method : UserGroupClear

        //define method : UserGroupCreate
        public int UserGroupCreate(UserGroup objUserGroup, string strConnectionString)
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
                cmdCommand.CommandText = "spUserGroupCreate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objUserGroup.UserID) and set its properties
                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@intUserID";
                paramUserID.SqlDbType = SqlDbType.Int;
                paramUserID.Direction = ParameterDirection.Input;
                paramUserID.Value = objUserGroup.UserID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramUserID);

                //Add input parameter for (objUserGroup.GroupID) and set its properties
                SqlParameter paramGroupID = new SqlParameter();
                paramGroupID.ParameterName = "@intGroupID";
                paramGroupID.SqlDbType = SqlDbType.Int;
                paramGroupID.Direction = ParameterDirection.Input;
                paramGroupID.Value = objUserGroup.GroupID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGroupID);

                //Add input parameter for (objUserGroup.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objUserGroup.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objUserGroup.Result) and set its properties
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
                objClaimsLog.MessageIs = "Method : UserGroupCreate() ";
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
        }//end method : UserGroupCreate

        #endregion

    }//end : public class CDUsers : ICDUsers
}//end : namespace ClaimsDocsBizLogic
