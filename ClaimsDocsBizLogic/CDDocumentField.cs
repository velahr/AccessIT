using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using ClaimsDocsBizLogic.SchemaClasses;
using System.Xml.Serialization;
using System.IO;
namespace ClaimsDocsBizLogic
{
    //define class : CDDocumentField
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CDDocumentField : ICDDocumentField
    {
        //declare private class variables
        private List<DocumentField> _listDocumentField = new List<DocumentField>();

        //define method : DocumentFieldCreate
        public int DocumentFieldCreate(DocumentField objDocumentField, string strConnectionString)
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
                cmdCommand.CommandText = "spDocumentFieldCreate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocumentField.DocumentFieldID) and set its properties
                SqlParameter paramDocumentFieldID = new SqlParameter();
                paramDocumentFieldID.ParameterName = "@intDocumentFieldID";
                paramDocumentFieldID.SqlDbType = SqlDbType.Int;
                paramDocumentFieldID.Direction = ParameterDirection.Input;
                paramDocumentFieldID.Value = objDocumentField.DocumentFieldID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentFieldID);

                //Add input parameter for (objDocumentField.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocumentField.DocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentID);

                //Add input parameter for (objDocumentField.FieldNameIs) and set its properties
                SqlParameter paramFieldNameIs = new SqlParameter();
                paramFieldNameIs.ParameterName = "@vchrFieldNameIs";
                paramFieldNameIs.SqlDbType = SqlDbType.VarChar;
                paramFieldNameIs.Direction = ParameterDirection.Input;
                paramFieldNameIs.Value = objDocumentField.FieldNameIs;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramFieldNameIs);

                //Add input parameter for (objDocumentField.FieldTypeIs) and set its properties
                SqlParameter paramFieldTypeIs = new SqlParameter();
                paramFieldTypeIs.ParameterName = "@vchrFieldTypeIs";
                paramFieldTypeIs.SqlDbType = SqlDbType.VarChar;
                paramFieldTypeIs.Direction = ParameterDirection.Input;
                paramFieldTypeIs.Value = objDocumentField.FieldTypeIs;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramFieldTypeIs);

                //Add input parameter for (objDocumentField.IsFieldRequired) and set its properties
                SqlParameter paramIsFieldRequired = new SqlParameter();
                paramIsFieldRequired.ParameterName = "@vchrIsFieldRequired";
                paramIsFieldRequired.SqlDbType = SqlDbType.VarChar;
                paramIsFieldRequired.Direction = ParameterDirection.Input;
                paramIsFieldRequired.Value = objDocumentField.IsFieldRequired;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIsFieldRequired);

                //Add input parameter for (objDocumentField.FieldDescription) and set its properties
                SqlParameter paramFieldDescription = new SqlParameter();
                paramFieldDescription.ParameterName = "@vchrFieldDescription";
                paramFieldDescription.SqlDbType = SqlDbType.VarChar;
                paramFieldDescription.Direction = ParameterDirection.Input;
                paramFieldDescription.Value = objDocumentField.FieldDescription;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramFieldDescription);

                //Add input parameter for (objDocumentField.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objDocumentField.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objDocumentField.Result) and set its properties
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
                objClaimsLog.MessageIs = "Method : DocumentFieldCreate() ";
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
        }//end method : DocumentFieldCreate

        //define method : DocumentFieldRead
        public DocumentField DocumentFieldRead(DocumentField objDocumentField, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            DocumentField objDocumentFieldIs = new DocumentField();
            int intResult = 0;

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDocumentFieldRead";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocumentField.DocumentFieldID) and set its properties
                SqlParameter paramDocumentFieldID = new SqlParameter();
                paramDocumentFieldID.ParameterName = "@intDocumentFieldID";
                paramDocumentFieldID.SqlDbType = SqlDbType.Int;
                paramDocumentFieldID.Direction = ParameterDirection.Input;
                paramDocumentFieldID.Value = objDocumentField.DocumentFieldID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentFieldID);

                //Add input parameter for (objDocumentField.Result) and set its properties
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
                        //fill DocumentField object
                        objDocumentField.DocumentFieldID = int.Parse(rdrReader["DocumentFieldID"].ToString());
                        objDocumentField.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocumentField.FieldNameIs = rdrReader["FieldNameIs"].ToString();
                        objDocumentField.FieldTypeIs = rdrReader["FieldTypeIs"].ToString();
                        objDocumentField.IsFieldRequired = rdrReader["IsFieldRequired"].ToString();
                        objDocumentField.FieldDescription = rdrReader["FieldDescription"].ToString();
                        objDocumentField.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
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
                objClaimsLog.MessageIs = "Method : DocumentFieldRead() ";
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
            return (objDocumentFieldIs);
        }//end method : DocumentFieldRead

        //define method : DocumentFieldUpdate
        public int DocumentFieldUpdate(DocumentField objDocumentField, string strConnectionString)
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
                cmdCommand.CommandText = "spDocumentFieldUpdate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocumentField.DocumentFieldID) and set its properties
                SqlParameter paramDocumentFieldID = new SqlParameter();
                paramDocumentFieldID.ParameterName = "@intDocumentFieldID";
                paramDocumentFieldID.SqlDbType = SqlDbType.Int;
                paramDocumentFieldID.Direction = ParameterDirection.Input;
                paramDocumentFieldID.Value = objDocumentField.DocumentFieldID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentFieldID);

                //Add input parameter for (objDocumentField.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocumentField.DocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentID);

                //Add input parameter for (objDocumentField.FieldNameIs) and set its properties
                SqlParameter paramFieldNameIs = new SqlParameter();
                paramFieldNameIs.ParameterName = "@vchrFieldNameIs";
                paramFieldNameIs.SqlDbType = SqlDbType.VarChar;
                paramFieldNameIs.Direction = ParameterDirection.Input;
                paramFieldNameIs.Value = objDocumentField.FieldNameIs;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramFieldNameIs);

                //Add input parameter for (objDocumentField.FieldTypeIs) and set its properties
                SqlParameter paramFieldTypeIs = new SqlParameter();
                paramFieldTypeIs.ParameterName = "@vchrFieldTypeIs";
                paramFieldTypeIs.SqlDbType = SqlDbType.VarChar;
                paramFieldTypeIs.Direction = ParameterDirection.Input;
                paramFieldTypeIs.Value = objDocumentField.FieldTypeIs;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramFieldTypeIs);

                //Add input parameter for (objDocumentField.IsFieldRequired) and set its properties
                SqlParameter paramIsFieldRequired = new SqlParameter();
                paramIsFieldRequired.ParameterName = "@vchrIsFieldRequired";
                paramIsFieldRequired.SqlDbType = SqlDbType.VarChar;
                paramIsFieldRequired.Direction = ParameterDirection.Input;
                paramIsFieldRequired.Value = objDocumentField.IsFieldRequired;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIsFieldRequired);

                //Add input parameter for (objDocumentField.FieldDescription) and set its properties
                SqlParameter paramFieldDescription = new SqlParameter();
                paramFieldDescription.ParameterName = "@vchrFieldDescription";
                paramFieldDescription.SqlDbType = SqlDbType.VarChar;
                paramFieldDescription.Direction = ParameterDirection.Input;
                paramFieldDescription.Value = objDocumentField.FieldDescription;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramFieldDescription);

                //Add input parameter for (objDocumentField.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objDocumentField.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objDocumentField.Result) and set its properties
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
                objClaimsLog.MessageIs = "Method : DocumentFieldUpdate() ";
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
        }//end method : DocumentFieldUpdate

        //define method : DocumentFieldDelete
        public int DocumentFieldDelete(DocumentField objDocumentField, string strConnectionString)
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
                cmdCommand.CommandText = "spDocumentFieldDelete";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocumentField.DocumentFieldID) and set its properties
                SqlParameter paramDocumentFieldID = new SqlParameter();
                paramDocumentFieldID.ParameterName = "@intDocumentFieldID";
                paramDocumentFieldID.SqlDbType = SqlDbType.Int;
                paramDocumentFieldID.Direction = ParameterDirection.Input;
                paramDocumentFieldID.Value = objDocumentField.DocumentFieldID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentFieldID);

                //Add input parameter for (objDocumentField.Result) and set its properties
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
                objClaimsLog.MessageIs = "Method : DocumentFieldDelete() ";
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
        }//end method : DocumentFieldDelete

        //define : DocumentFieldSearch
        public List<DocumentField> DocumentFieldSearch(string strSQLString, string strConnectionString)
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
                        //create new DocumentField object
                        DocumentField objDocumentField = new DocumentField();
                        //fill DocumentField object
                        objDocumentField.DocumentFieldID = int.Parse(rdrReader["DocumentFieldID"].ToString());
                        objDocumentField.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocumentField.FieldNameIs = rdrReader["FieldNameIs"].ToString();
                        objDocumentField.FieldTypeIs = rdrReader["FieldTypeIs"].ToString();
                        objDocumentField.IsFieldRequired = rdrReader["IsFieldRequired"].ToString();
                        objDocumentField.FieldDescription = rdrReader["FieldDescription"].ToString();
                        objDocumentField.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
                        //add user to department list
                        this._listDocumentField.Add(objDocumentField);
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
                objClaimsLog.MessageIs = "Method : DocumentFieldSearch() ";
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
            return (this._listDocumentField);
        }//end : DocumentFieldSearch

        //define method : DocumentFieldClear
        public int DocumentFieldClear(DocumentField objDocumentField, string strConnectionString)
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
                cmdCommand.CommandText = "spDocumentFieldClear";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objUserGroup.UserID) and set its properties
                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@intDocumentID";
                paramUserID.SqlDbType = SqlDbType.Int;
                paramUserID.Direction = ParameterDirection.Input;
                paramUserID.Value = objDocumentField.DocumentID;
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
                objClaimsLog.MessageIs = "Method : DocumentFieldClear() ";
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
        }//end method : DocumentFieldClear

        //define : DisplayFieldsGetFromXMLFile
        public List<DocumentDisplayField> DisplayFieldsGetFromXMLFile(ClaimsDocument objClaimsDocument)
        {
            //declare variables
            List <DocumentDisplayField> listDisplayFields = new List<DocumentDisplayField>();
            int intIndex = 0;

            //start try
            try
            {
                //check for successful deserialization
                if (objClaimsDocument != null)
                {
                    //get display fields
                    for (intIndex = 0; intIndex < objClaimsDocument.Input.MetaData.DisplayFields.Length; intIndex++)
                    {
                        //get display field
                        DocumentDisplayField objDisplayField = new DocumentDisplayField();
                        //fill display field
                        objDisplayField.DisplayFieldName =  objClaimsDocument.Input.MetaData.DisplayFields[intIndex].Name;
                        objDisplayField.DisplayFieldValue = objClaimsDocument.Input.MetaData.DisplayFields[intIndex].Value;
                        //add display field to list
                        listDisplayFields.Add(objDisplayField);
                    }

                }


            }//end try
            catch (Exception ex)
            {
                //handle error
                listDisplayFields = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DisplayFieldsGetFromXMLFile() ";
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
                //clean up
                objClaimsDocument = null;
            }
            //return result
            return (listDisplayFields);
        }//end : DisplayFieldsGetFromXMLFile


    }//end : public class CDDocumentField : ICDDocumentField
}//end : namespace ClaimsDocsBizLogic
