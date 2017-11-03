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
using ClaimsDocsBizLogic.DocumentStorageWS1;
using System.Xml;
using System.Messaging;
using System.Threading;
using System.Xml.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ServiceModel;

namespace ClaimsDocsBizLogic
{
    //ClaimsDocs_GetAddressee

    //start definition of class : PackDocType
    public class PackDocType
    {
        public int PackType { get; set; }
        public string DocType { get; set; }
    }

    //define class : ClaimsDocsGenerator
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class ClaimsDocsGenerator : IClaimsDocsGenerator
    {
        //define private member variables
        private ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument objCDXML = new ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument();
        private ClaimsDocsBizLogic.SchemaClasses.ClaimsDocumentDocumentDefinition objDocDef = new ClaimsDocsBizLogic.SchemaClasses.ClaimsDocumentDocumentDefinition();
        private DocGenerationResponse objDocGenerationResponse = new DocGenerationResponse();
        private Dictionary<string, string> dicOfCoverageCodes = new Dictionary<string, string>();
        private Dictionary<string, string> dicOfCoverageCodeDescriptions = new Dictionary<string, string>();
        private Dictionary<string, decimal> dicPolicyCandL = new Dictionary<string, decimal>();

        #region Support Methods

        //define method : HandleNullOrBlank
        string HandleNullOrBlank(string strStringIs)
        {
            //declare variables
            string strReturnValue = "";

            try
            {
                if (string.IsNullOrEmpty(strStringIs) == true)
                {
                    strReturnValue = "";
                }
                else
                {
                    strReturnValue = strStringIs;
                }
            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : HandleNullOrBlank() ";
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

            }

            //return results
            return (strReturnValue);

        }//end : HandleNullOrBlank

        //define method : EntGetPhone
        private static string EntGetPhone(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            StringBuilder phoneDigits = new StringBuilder();

            // Capture all digits from the phone number
            MatchCollection matches = Regex.Matches(input, @"\d");
            foreach (Match match in matches)
            {
                phoneDigits.Append(match.Value);
            }

            // Insert the hyphens in the proper places
            if (phoneDigits.Length >= 3)
                phoneDigits.Insert(3, '-');
            if (phoneDigits.Length >= 7)
                phoneDigits.Insert(7, '-');

            //replace left parenthesis
            phoneDigits = phoneDigits.Replace("(", "");

            //replace right parenthesis
            phoneDigits = phoneDigits.Replace(")", "");

            return phoneDigits.ToString();
        }

        //define method : PackDocTypeGet
        public PackDocType PackDocTypeGet(string strClaimsDocGroup)
        {
            //declare variables
            PackDocType objPackDocType = new PackDocType();

            try
            {
                //set pack and doc type based on Claims Doc Group
                switch (strClaimsDocGroup.ToUpper())
                {
                    case "MD":
                        objPackDocType.PackType = 10200;
                        objPackDocType.DocType = "CORM";
                        break;

                    case "BI":
                        objPackDocType.PackType = 10300;
                        objPackDocType.DocType = "CORB";
                        break;

                    case "LIT":
                        objPackDocType.PackType = 10400;
                        objPackDocType.DocType = "LITC";
                        break;

                    case "MDLC":
                        objPackDocType.PackType = 10200;
                        objPackDocType.DocType = "MD";
                        break;

                    case "PIP":
                        objPackDocType.PackType = 10500;
                        objPackDocType.DocType = "PCOR";
                        break;

                    case "PCOR":
                        objPackDocType.PackType = 10500;
                        objPackDocType.DocType = "PIP";
                        break;

                    case "PFRM":
                        objPackDocType.PackType = 10500;
                        objPackDocType.DocType = "PIP";
                        break;

                    case "TLOS":
                        objPackDocType.PackType = 10200;
                        objPackDocType.DocType = "MD";
                        break;

                    case "SIU":
                        objPackDocType.PackType = 10900;
                        objPackDocType.DocType = "SIUC";
                        break;

                    case "SAL":
                        objPackDocType.PackType = 10800;
                        objPackDocType.DocType = "CRSL";
                        break;

                    case "SUBRO":
                        objPackDocType.PackType = 10600;
                        objPackDocType.DocType = "CORS";
                        break;

                    case "SYSTEM":
                        break;

                    case "UASPECIAL":
                        break;

                    case "CSR":
                        break;

                    case "US":
                        break;

                    case "PSS":
                        break;

                    case "QA":
                        break;

                    case "PSS2":
                        break;

                }//end : switch (strClaimsDocGroup.ToUpper())

            }
            catch (Exception ex)
            {
                //handle error
                objPackDocType = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : ClaimsDocsGenerateDocument() ";
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
            }
            //return result
            return (objPackDocType);
        }//end : PackDocTypeGet

        //define method : LoadCoverageCodeFromDB
        public bool LoadCoverageCodeFromDB(string strConnectionString)
        {
            //define variables
            bool blnResult = true;
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            string strSQLString = "";
            string strTryGetValue = "";

            try
            {
                //build sql string
                strSQLString = strSQLString + "Select ";
                strSQLString = strSQLString + " Company,";
                strSQLString = strSQLString + " CoverageCd, ";
                strSQLString = strSQLString + " CoverageCode, ";
                strSQLString = strSQLString + " CoverageDesc ";
                strSQLString = strSQLString + "From ";
                strSQLString = strSQLString + " LK_IA_ImportCoverages ";
                strSQLString = strSQLString + "Order By ";
                strSQLString = strSQLString + "Company, ";
                strSQLString = strSQLString + "CoverageCode ";

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
                rdrReader = cmdCommand.ExecuteReader(CommandBehavior.CloseConnection);

                //check for values
                if (rdrReader.HasRows == true)
                {
                    //read values
                    while (rdrReader.Read())
                    {
                        //add coverage code value to dictionary
                        strTryGetValue = "";
                        dicOfCoverageCodes.TryGetValue(rdrReader["Company"].ToString() + rdrReader["CoverageCode"].ToString(), out strTryGetValue);
                        if (strTryGetValue == null)
                        {
                            this.dicOfCoverageCodes.Add(rdrReader["Company"].ToString() + rdrReader["CoverageCode"].ToString(), rdrReader["CoverageCd"].ToString());
                        }

                        //add coverage code description value to dictionary
                        strTryGetValue = "";
                        dicOfCoverageCodeDescriptions.TryGetValue(rdrReader["Company"].ToString() + rdrReader["CoverageCode"].ToString(), out strTryGetValue);
                        if (strTryGetValue == null)
                        {
                            this.dicOfCoverageCodeDescriptions.Add(rdrReader["Company"].ToString() + rdrReader["CoverageCode"].ToString(), rdrReader["CoverageDesc"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : LoadCoverageCodeFromDB() ";
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
            return (blnResult);
        }//end : LoadCoverageCodeFromDB

        //define : CoverageCodeFromNumberToString
        private string CoverageCodeFromNumberToString(string strCompanyNumber, string strCoverageCodeNumericValue)
        {
            //declare variables
            string strCoverageCodeDescription = "???";

            try
            {
                //remove this code after we've populated the coverage code
                //database with the codes in the switch statement
                switch (strCoverageCodeNumericValue)
                {
                    case "AFAUTXT":
                        strCoverageCodeDescription = "UTXT";
                        break;

                    case "065":
                        strCoverageCodeDescription = "FG";
                        break;

                    case "040":
                        strCoverageCodeDescription = "UMPD";
                        break;

                    case "081":
                        strCoverageCodeDescription = "PIP";
                        break;

                    case "700":
                        strCoverageCodeDescription = "LL";
                        break;

                    case "710":
                        strCoverageCodeDescription = "AEQ";
                        break;

                    case "730":
                        strCoverageCodeDescription = "ADDA";
                        break;

                    default:
                        //get coverage code
                        dicOfCoverageCodes.TryGetValue(strCompanyNumber + strCoverageCodeNumericValue, out strCoverageCodeDescription);
                        if (strCoverageCodeDescription == null)
                        {
                            strCoverageCodeDescription = "???";
                        }
                        break;
                }//end : switch (strCoverageCodeNumericValue)
            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : CoverageCodeFromNumberToString() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);
            }
            finally
            {
            }
            //return results
            return (strCoverageCodeDescription);
        }//end : CoverageCodeFromNumberToString


        public string EncodeToBase64(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        private String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();

            String constructedString = encoding.GetString(characters);

            return (constructedString);
        }

        #endregion

        #region Data Access Methods

        //define method : DocumentDefinitionGet
        public ClaimsDocumentDocumentDefinition DocumentDefinitionGet(DocGenerationRequest objDocGenerationRequest, string stringClaimsDocDBConnectionString)
        {
            //declare variables
            ClaimsDocumentDocumentDefinition objDocumentDefinition = null;
            List<ClaimsDocumentDocumentDefinitionFieldDefinition> listDocumentDefinitionFields = new List<ClaimsDocumentDocumentDefinitionFieldDefinition>();
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intResult = 0;
            DateTime datTryParse;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(stringClaimsDocDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDocumentRead";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocument.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocGenerationRequest.DocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentID);

                //Add input parameter for (objDocument.Result) and set its properties
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

                //fill document
                if (rdrReader.HasRows == true)
                {
                    //create objDocumentDefinition object
                    objDocumentDefinition = new ClaimsDocumentDocumentDefinition();

                    while (rdrReader.Read())
                    {
                        objDocumentDefinition.ID = rdrReader["DocumentID"].ToString();
                        objDocumentDefinition.CompanyCode = objDocGenerationRequest.PolicyNumber.Substring(0, 3);
                        objDocumentDefinition.ProgramID = rdrReader["ProgramCode"].ToString();
                        objDocumentDefinition.Active = rdrReader["Active"].ToString();
                        objDocumentDefinition.CopyToAgent = rdrReader["CopyAgent"].ToString();
                        objDocumentDefinition.CopyToAttorney = rdrReader["CopyAttorney"].ToString();
                        objDocumentDefinition.CopyToFinanceCompany = rdrReader["CopyFinanceCo"].ToString();
                        objDocumentDefinition.CopyToInsured = rdrReader["CopyInsured"].ToString();
                        objDocumentDefinition.CopyToLeinholder = rdrReader["CopyLienHolder"].ToString();
                        objDocumentDefinition.DepartmentID = rdrReader["DepartmentID"].ToString();
                        objDocumentDefinition.Description = rdrReader["Description"].ToString();

                        if (string.IsNullOrEmpty(rdrReader["DesignerID"].ToString()) == true)
                        {
                            objDocumentDefinition.DesignerID = "0";
                        }
                        else
                        {
                            objDocumentDefinition.DesignerID = rdrReader["DesignerID"].ToString();
                        }

                        if (DateTime.TryParse(rdrReader["EffectiveDate"].ToString(), out datTryParse) == true)
                        {
                            objDocumentDefinition.EffectiveDate = rdrReader["EffectiveDate"].ToString();
                        }

                        objDocumentDefinition.ImageRightDocumentID = rdrReader["ImageRightDocumentID"].ToString();
                        objDocumentDefinition.ImageRightDocumentSection = rdrReader["ImageRightDocumentSection"].ToString();
                        objDocumentDefinition.ImageRightDrawer = rdrReader["ImageRightDrawer"].ToString();
                        objDocumentDefinition.ImportToDataMatx = rdrReader["DataMatx"].ToString();
                        objDocumentDefinition.ImportToImageRight = rdrReader["ImportToImageRight"].ToString();
                        objDocumentDefinition.LastModified = rdrReader["LastModified"].ToString();
                        objDocumentDefinition.DocumentDefinitionName = rdrReader["DocumentCode"].ToString();
                        objDocumentDefinition.ProgramID = rdrReader["ProgramID"].ToString();
                        objDocumentDefinition.ProofOfMailing = rdrReader["ProofOfMailing"].ToString();
                        objDocumentDefinition.ReviewRequired = rdrReader["Review"].ToString();
                        objDocumentDefinition.StyleSheetName = rdrReader["StyleSheetName"].ToString();
                        objDocumentDefinition.TemplateName = rdrReader["TemplateName"].ToString();
                        objDocumentDefinition.AttachedDocument = rdrReader["AttachedDocument"].ToString();
                        objDocumentDefinition.DiaryAutoUpdate = rdrReader["DiaryAutoUpdate"].ToString();
                        objDocumentDefinition.KEY1 = "CLAIMS";
                        objDocumentDefinition.KEY2 = objDocGenerationRequest.GroupName;

                        //get list of document definition fields
                        listDocumentDefinitionFields = DocumentDefinitionFieldGet("Select * From tblDocumentField Where DocumentID=" + objDocGenerationRequest.DocumentID.ToString(), stringClaimsDocDBConnectionString);
                        //check for fields
                        if (listDocumentDefinitionFields != null)
                        {
                            //apply fields to document definition
                            objDocumentDefinition.AllFieldDefinitions = listDocumentDefinitionFields.ToArray();
                        }
                    }//end : while (rdrReader.Read())

                }//end : if (rdrReader.HasRows == true)
            }
            catch (Exception ex)
            {
                //handle error
                objDocumentDefinition = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DocumentDefinitionGet() ";
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

            }

            //return result
            return (objDocumentDefinition);

        }//end : DocumentDefinitionGet

        //define : DocumentDefinitionFieldGet
        public List<ClaimsDocumentDocumentDefinitionFieldDefinition> DocumentDefinitionFieldGet(string strSQLString, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            List<ClaimsDocumentDocumentDefinitionFieldDefinition> listDocumentField = null;

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
                    //create list
                    listDocumentField = new List<ClaimsDocumentDocumentDefinitionFieldDefinition>();

                    //fill user object
                    while (rdrReader.Read())
                    {
                        //create new DocumentField object
                        ClaimsDocumentDocumentDefinitionFieldDefinition objDocumentField = new ClaimsDocumentDocumentDefinitionFieldDefinition();
                        //fill DocumentField object
                        objDocumentField.ID = rdrReader["DocumentFieldID"].ToString();
                        objDocumentField.Name = rdrReader["FieldNameIs"].ToString();
                        objDocumentField.Type = rdrReader["FieldTypeIs"].ToString();
                        objDocumentField.Required = rdrReader["IsFieldRequired"].ToString();
                        objDocumentField.Description = rdrReader["FieldDescription"].ToString();
                        //add field to field list
                        listDocumentField.Add(objDocumentField);
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
                objClaimsLog.MessageIs = "Method : DocumentDefinitionFieldGet() ";
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

            //return result
            return (listDocumentField);
        }//end : DocumentDefinitionFieldGet

        //define method : PolicyInformationGet
        public ClaimsDocumentInputDocumentWhitehillPolicyClaimInformation PolicyInformationGet(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            ClaimsDocumentInputDocumentWhitehillPolicyClaimInformation objPolicyInformation = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            Program objProgramIs = new Program();

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_Claims";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (PolicyNumber) and set its properties
                SqlParameter paramPolicyNumber = new SqlParameter();
                paramPolicyNumber.ParameterName = "@PolicyNo";
                paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                paramPolicyNumber.Direction = ParameterDirection.Input;
                paramPolicyNumber.Value = objDocGenerationRequest.PolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNumber);

                //Add input parameter for (laimNumber) and set its properties
                SqlParameter paramClaimNumber = new SqlParameter();
                paramClaimNumber.ParameterName = "@ClaimNo";
                paramClaimNumber.SqlDbType = SqlDbType.VarChar;
                paramClaimNumber.Direction = ParameterDirection.Input;
                paramClaimNumber.Value = objDocGenerationRequest.ClaimNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimNumber);

                //Add input parameter for (UKeyContact) and set its properties
                SqlParameter paramUKeyContact = new SqlParameter();
                paramUKeyContact.ParameterName = "@ukey_Contact";
                paramUKeyContact.SqlDbType = SqlDbType.Int;
                paramUKeyContact.Direction = ParameterDirection.Input;
                paramUKeyContact.Value = objDocGenerationRequest.ContactNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramUKeyContact);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    objPolicyInformation = new ClaimsDocumentInputDocumentWhitehillPolicyClaimInformation();
                    //fill user object
                    while (rdrReader.Read())
                    {
                        //fill user object
                        objPolicyInformation.Department = rdrReader["Department"].ToString();
                        objPolicyInformation.User = rdrReader["User"].ToString();
                        objPolicyInformation.ProgramCode = rdrReader["ProgramCode"].ToString();
                        switch (CDSupport.RunMode)
                        {
                            case "DEVMACHINE":
                            case "DEVELOPMENT":
                            case "ITQA":
                            case "UAT":
                            case "PRODUCTION":
                                objPolicyInformation.ClaimNumber = rdrReader["ClaimNo"].ToString();
                                break;

                            default:
                                objPolicyInformation.ClaimNumber = rdrReader["ClaimNumber"].ToString();
                                break;
                        }

                        objPolicyInformation.PolicyNumber = rdrReader["PolicyNumber"].ToString();
                        objPolicyInformation.PolicyTerm = rdrReader["PolicyTerm"].ToString();
                        objPolicyInformation.PolicyEffectiveDate = rdrReader["PolicyEffectiveDate"].ToString();
                        objPolicyInformation.PolicyLastExpirationDate = rdrReader["PolicyLastExpirationDate"].ToString();
                        objPolicyInformation.PolicyCancellationDate = rdrReader["PolicyCancellationDate"].ToString();
                        objPolicyInformation.PolicyLastEndorsementDate = rdrReader["PolicyLastEndorsementDate"].ToString();
                        objPolicyInformation.PolicyStatus = rdrReader["PolicyStatus"].ToString();
                        objPolicyInformation.ProgramCode = rdrReader["Mode"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objPolicyInformation = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : PolicyInformationGet() ";
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
            }

            //return results
            return (objPolicyInformation);
        }//end method : PolicyInformationGet

        //define method : CompanyInformationGet
        public ClaimsDocumentCompanyInformation CompanyInformationGet(DocGenerationRequest objDocGenerationRequest, string strGenesisDBConnectionString)
        {
            //declare variables
            ClaimsDocumentCompanyInformation objCompanyInfo = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            Program objProgramIs = new Program();

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strGenesisDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_GetCompanyInfo";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (PolicyNumber) and set its properties
                SqlParameter paramPolicyNumber = new SqlParameter();
                paramPolicyNumber.ParameterName = "@CompanyNumber";
                paramPolicyNumber.SqlDbType = SqlDbType.Char;
                paramPolicyNumber.Direction = ParameterDirection.Input;
                paramPolicyNumber.Value = objDocGenerationRequest.CompanyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNumber);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    objCompanyInfo = new ClaimsDocumentCompanyInformation();
                    //fill user object
                    while (rdrReader.Read())
                    {
                        //fill user object
                        objCompanyInfo.AccessName = rdrReader["AccessName"].ToString();
                        objCompanyInfo.CompanyLongName = rdrReader["CompanyLongName"].ToString();
                        objCompanyInfo.CompanyShortName = rdrReader["CompanyShortName"].ToString();
                        objCompanyInfo.GeneralName = rdrReader["GeneralName"].ToString();
                        objCompanyInfo.GeneralAddress = rdrReader["GeneralAddress"].ToString();
                        objCompanyInfo.GeneralCSZ = rdrReader["GeneralCSZ"].ToString();
                        objCompanyInfo.DirBillName = rdrReader["DirBillName"].ToString();
                        objCompanyInfo.DirBillAddress = rdrReader["DirBillAddress"].ToString();
                        objCompanyInfo.DirBillCSZ = rdrReader["DirBillCSZ"].ToString();
                        objCompanyInfo.ClaimsName = rdrReader["ClaimsName"].ToString();
                        objCompanyInfo.ClaimsAddress = rdrReader["ClaimsAddress"].ToString();
                        objCompanyInfo.ClaimsCSZ = rdrReader["ClaimsCSZ"].ToString();
                        objCompanyInfo.UnderwritingPhone = EntGetPhone(rdrReader["UnderwritingPhone"].ToString());
                        objCompanyInfo.IVRPhone = EntGetPhone(rdrReader["IVRPhone"].ToString());
                        objCompanyInfo.ClaimsPhone = EntGetPhone(rdrReader["ClaimsPhone"].ToString());
                        objCompanyInfo.Fax = EntGetPhone(rdrReader["Fax"].ToString());
                        objCompanyInfo.Website = rdrReader["Website"].ToString();
                        objCompanyInfo.ImageRightFaxCoverValue_UW = rdrReader["imagerightfaxcovervalue_UW"].ToString();
                        objCompanyInfo.ImageRightFaxCoverValue_CLAIMS = rdrReader["imagerightfaxcovervalue_CLAIMS"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objCompanyInfo = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : CompanyInformationGet() ";
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
            }

            //return results
            return (objCompanyInfo);
        }//end method : CompanyInformationGet

        //define method : SubmitterInformationGet
        public ClaimsDocumentSubmitter SubmitterInformationGet(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            ClaimsDocumentSubmitter objSubmitter = null;
            ClaimsDocumentSubmitterDepartment objSubmitterDepartment = new ClaimsDocumentSubmitterDepartment();
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intResult = 0;

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
                paramUserID.Value = objDocGenerationRequest.UserID;
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
                    //create submitter object
                    objSubmitter = new ClaimsDocumentSubmitter();

                    //fill submitter object
                    while (rdrReader.Read())
                    {
                        objSubmitter.SubmitterID = rdrReader["UserID"].ToString();
                        objSubmitter.Name = rdrReader["UserName"].ToString();
                        objSubmitter.Phone = rdrReader["Phone"].ToString();
                        objSubmitter.ReviewRequired = rdrReader["Reviewer"].ToString();
                        objSubmitter.SignatureFileName = rdrReader["Signature"].ToString();
                        objSubmitter.SignatureName = rdrReader["SignatureName"].ToString();
                        objSubmitter.EMail = rdrReader["EMailAddress"].ToString();
                        objSubmitter.Title = rdrReader["Title"].ToString();
                        objSubmitter.Active = rdrReader["Active"].ToString();
                        objSubmitter.Approver = rdrReader["Approver"].ToString();
                        objSubmitter.Designer = rdrReader["Designer"].ToString();

                        //get submitter department information
                        objSubmitterDepartment.ID = rdrReader["DepartmentID"].ToString();
                        objSubmitterDepartment.Name = rdrReader["DepartmentName"].ToString();
                        //assign submitter department
                        objSubmitter.Department = objSubmitterDepartment;
                    }
                }//end : if (rdrReader.HasRows == true)

                //close data reader
                rdrReader.Close();

                //get submitter group list
                //clear command parameters
                cmdCommand.Parameters.Clear();
                cmdCommand.CommandText = "spUserGroupList";
                //add command parameters		
                //Add input parameter for (UserID) and set its properties
                SqlParameter paramGUserID = new SqlParameter();
                paramGUserID.ParameterName = "@intUserID";
                paramGUserID.SqlDbType = SqlDbType.Int;
                paramGUserID.Direction = ParameterDirection.Input;
                paramGUserID.Value = objDocGenerationRequest.UserID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGUserID);

                //Add input parameter for (objGroup.Result) and set its properties
                SqlParameter paramGResult = new SqlParameter();
                paramGResult.ParameterName = "@intResult";
                paramGResult.SqlDbType = SqlDbType.Int;
                paramGResult.Direction = ParameterDirection.InputOutput;
                paramGResult.Value = intResult;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGResult);

                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //define submitter group list
                    List<ClaimsDocumentSubmitterGroup> listSubmitterGroup = new List<ClaimsDocumentSubmitterGroup>();

                    //fill submitter group object
                    while (rdrReader.Read())
                    {
                        //create new submitter group object
                        ClaimsDocumentSubmitterGroup objSubmitterGroup = new ClaimsDocumentSubmitterGroup();
                        ClaimsDocumentSubmitterGroupDepartment objSubmitterGroupDepartment = new ClaimsDocumentSubmitterGroupDepartment();
                        //fill user object
                        objSubmitterGroup.ID = rdrReader["GroupID"].ToString();
                        objSubmitterGroup.Name = rdrReader["GroupName"].ToString();
                        objSubmitterGroupDepartment.ID = rdrReader["DepartmentID"].ToString();
                        objSubmitterGroupDepartment.Name = rdrReader["DepartmentName"].ToString();
                        //assign submitter group department
                        objSubmitterGroup.Department = objSubmitterGroupDepartment;
                        //add submitter group to list
                        listSubmitterGroup.Add(objSubmitterGroup);
                    }
                    //assign groups to submitter
                    objSubmitter.AllGroups = listSubmitterGroup.ToArray();
                }
            }
            catch (Exception ex)
            {
                //handle error
                objSubmitter = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : SubmitterInformationGet() ";
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

                if (cnnConnection != null)
                {
                    if (cnnConnection.State == ConnectionState.Open)
                    {
                        cnnConnection.Close();
                    }
                    cnnConnection = null;
                }
            }
            //return results
            return (objSubmitter);
        }//end method : SubmitterInformationGet

        //Method xAddresseeInformationGet is used for debugging purposes
        public ClaimsDocumentAddressee xAddresseeInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strAMSDBConnectionString)
        {
            ClaimsDocumentAddressee objAddresseeInformation = new ClaimsDocumentAddressee();
            ClaimsDocumentAddresseeAddressLine objLine1 = new ClaimsDocumentAddresseeAddressLine();
            ClaimsDocumentAddresseeAddressLine objLine2 = new ClaimsDocumentAddresseeAddressLine();
            ClaimsDocumentAddresseeAddressLine objLine3 = new ClaimsDocumentAddresseeAddressLine();
            List<ClaimsDocumentAddresseeAddressLine> listAddressLines = new List<ClaimsDocumentAddresseeAddressLine>();

            try
            {

                //fill user object
                objAddresseeInformation.AddresseeName = "TEST Address";
                objAddresseeInformation.AddresseeAddressLine1 = "123Peachtree Street";
                objAddresseeInformation.AddresseeAddressLine2 = "Suite P";
                objAddresseeInformation.AddresseeAddressLine3 = "Floor 43";
                objAddresseeInformation.AddresseePhoneNumber = "404-000-0000";
                objAddresseeInformation.AddresseeFaxNumber = "404-000-0000";
                objAddresseeInformation.AddresseeEmailAddress = "kphifer@accessgeneral.com";
                objAddresseeInformation.AddresseeCity = "Atlanta";
                objAddresseeInformation.AddresseeState = "GA";
                objAddresseeInformation.AddresseeZip = "30047";
                objAddresseeInformation.AddresseeZip4 = "009";
                objAddresseeInformation.AddresseePostNetZip = "30047";
                objAddresseeInformation.MatchLevel = "TESTER";

                //get address lines
                objLine1.Line = objAddresseeInformation.AddresseeAddressLine1;
                objLine2.Line = objAddresseeInformation.AddresseeAddressLine2;
                objLine3.Line = objAddresseeInformation.AddresseeAddressLine3;

                //add address lines to list
                listAddressLines.Add(objLine1);
                listAddressLines.Add(objLine2);
                listAddressLines.Add(objLine3);
                //apply addressee lines to addressee
                objAddresseeInformation.AllAddressLines = listAddressLines.ToArray();
            }
            catch (Exception ex)
            {
                //handle error
                objAddresseeInformation = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : AddresseeInformationGet() ";
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
            }

            return (objAddresseeInformation);
        }

        //define method : AddresseeInfo/rmationGet
        public ClaimsDocumentAddressee AddresseeInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strAMSDBConnectionString)
        {
            //declare variables
            ClaimsDocumentAddressee objAddresseeInformation = null;
            ClaimsDocumentAddresseeAddressLine objLine1 = new ClaimsDocumentAddresseeAddressLine();
            ClaimsDocumentAddresseeAddressLine objLine2 = new ClaimsDocumentAddresseeAddressLine();
            ClaimsDocumentAddresseeAddressLine objLine3 = new ClaimsDocumentAddresseeAddressLine();
            List<ClaimsDocumentAddresseeAddressLine> listAddressLines = new List<ClaimsDocumentAddresseeAddressLine>();
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strAMSDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_GetAddressee";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (ClaimUKey) and set its properties
                SqlParameter paramClaimUKey = new SqlParameter();
                paramClaimUKey.ParameterName = "@claimukey";
                paramClaimUKey.SqlDbType = SqlDbType.Int;
                paramClaimUKey.Direction = ParameterDirection.Input;
                paramClaimUKey.Value = objDocumentGenerationRequest.ContactNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimUKey);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    objAddresseeInformation = new ClaimsDocumentAddressee();
                    //fill user object
                    while (rdrReader.Read())
                    {
                        //fill user object
                        objAddresseeInformation.AddresseeName = rdrReader["AddresseeName"].ToString();
                        objAddresseeInformation.AddresseeAddressLine1 = rdrReader["AddresseeAddressLine1"].ToString();
                        objAddresseeInformation.AddresseeAddressLine2 = rdrReader["AddresseeAddressLine2"].ToString();
                        objAddresseeInformation.AddresseeAddressLine3 = rdrReader["AddresseeAddressLine3"].ToString();
                        objAddresseeInformation.AddresseePhoneNumber = rdrReader["AddresseePhoneNumber"].ToString();
                        objAddresseeInformation.AddresseeFaxNumber = rdrReader["AddresseeFaxNumber"].ToString();
                        objAddresseeInformation.AddresseeEmailAddress = rdrReader["AddresseeEmailAddress"].ToString();
                        objAddresseeInformation.AddresseeCity = rdrReader["AddresseeCity"].ToString();
                        objAddresseeInformation.AddresseeState = rdrReader["AddresseeState"].ToString();
                        objAddresseeInformation.AddresseeZip = rdrReader["AddresseeZip"].ToString();
                        objAddresseeInformation.AddresseeZip4 = rdrReader["AddresseeZip4"].ToString();
                        objAddresseeInformation.AddresseePostNetZip = rdrReader["AddresseePostNetZip"].ToString();
                        objAddresseeInformation.MatchLevel = rdrReader["MatchLevel"].ToString();

                        //get address lines
                        objLine1.Line = objAddresseeInformation.AddresseeAddressLine1;
                        objLine2.Line = objAddresseeInformation.AddresseeAddressLine2;
                        objLine3.Line = objAddresseeInformation.AddresseeAddressLine3;

                        //add address lines to list
                        listAddressLines.Add(objLine1);
                        listAddressLines.Add(objLine2);
                        listAddressLines.Add(objLine3);
                        //apply addressee lines to addressee
                        objAddresseeInformation.AllAddressLines = listAddressLines.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objAddresseeInformation = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : AddresseeInformationGet() ";
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
            }

            //return results
            return (objAddresseeInformation);
        }

        //define method : WhiteHillAddresseeInformationGet
        public ClaimsDocumentInputDocumentWhitehillAddressee WhiteHillAddresseeInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strAMSDBConnectionString)
        {
            //declare variables
            ClaimsDocumentInputDocumentWhitehillAddressee objAddresseeInformation = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {

                //**********************************
                //setup connection
                cnnConnection = new SqlConnection(strAMSDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_GetAddressee";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (ClaimUKey) and set its properties
                SqlParameter paramClaimUKey = new SqlParameter();
                paramClaimUKey.ParameterName = "@claimukey";
                paramClaimUKey.SqlDbType = SqlDbType.Int;
                paramClaimUKey.Direction = ParameterDirection.Input;
                paramClaimUKey.Value = objDocumentGenerationRequest.ContactNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimUKey);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    objAddresseeInformation = new ClaimsDocumentInputDocumentWhitehillAddressee();
                    //fill user object
                    while (rdrReader.Read())
                    {
                        //fill user object
                        objAddresseeInformation.AddresseeName = rdrReader["AddresseeName"].ToString();
                        objAddresseeInformation.AddresseeAddressLine1 = rdrReader["AddresseeAddressLine1"].ToString();
                        objAddresseeInformation.AddresseeAddressLine2 = rdrReader["AddresseeAddressLine2"].ToString();
                        objAddresseeInformation.AddresseeAddressLine3 = rdrReader["AddresseeAddressLine3"].ToString();
                        objAddresseeInformation.AddresseePhoneNumber = rdrReader["AddresseePhoneNumber"].ToString();
                        objAddresseeInformation.AddresseeFaxNumber = rdrReader["AddresseeFaxNumber"].ToString();
                        objAddresseeInformation.AddresseeEmailAddress = rdrReader["AddresseeEmailAddress"].ToString();
                        objAddresseeInformation.AddresseeCity = rdrReader["AddresseeCity"].ToString();
                        objAddresseeInformation.AddresseeState = rdrReader["AddresseeState"].ToString();
                        objAddresseeInformation.AddresseeZip = rdrReader["AddresseeZip"].ToString();
                        objAddresseeInformation.AddresseeZip4 = rdrReader["AddresseeZip4"].ToString();
                        objAddresseeInformation.AddresseePostNetZip = rdrReader["AddresseePostNetZip"].ToString();
                        objAddresseeInformation.MatchLevel = rdrReader["MatchLevel"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objAddresseeInformation = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : WhiteHillAddresseeInformationGet() ";
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
            }
            //return results
            return (objAddresseeInformation);
        }//end : WhiteHillAddresseeInformationGet

        //define method : CoverageInformationGet
        public ClaimsDocumentInputDocumentWhitehillCoverages CoverageInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            ClaimsDocumentInputDocumentWhitehillCoverages objCoverage = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_Coverages";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (PolicyNumber) and set its properties
                SqlParameter paramPolicyNumber = new SqlParameter();
                paramPolicyNumber.ParameterName = "@policyno";
                paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                paramPolicyNumber.Direction = ParameterDirection.Input;
                paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNumber);

                //Add input parameter for (CarNumber) and set its properties
                SqlParameter paramCarNumber = new SqlParameter();
                paramCarNumber.ParameterName = "@carno";
                paramCarNumber.SqlDbType = SqlDbType.Int;
                paramCarNumber.Direction = ParameterDirection.Input;
                paramCarNumber.Value = objDocumentGenerationRequest.CarNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCarNumber);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //rdrReader.NextResult();

                    //create coverage object
                    objCoverage = new ClaimsDocumentInputDocumentWhitehillCoverages();
                    //fill coverage object
                    while (rdrReader.Read())
                    {
                        objCoverage.AccidentalDeath = rdrReader["Accidental_Death"].ToString();
                        objCoverage.ArbitrationAgreementWaiver = rdrReader["Arbitration_Agreement_Waiver"].ToString();
                        objCoverage.AutoClub = rdrReader["Auto_Club"].ToString();
                        objCoverage.BillingServiceCharges = rdrReader["BillingService_Charges"].ToString();
                        objCoverage.BodilyInjury = rdrReader["Bodily_Injury"].ToString();
                        objCoverage.BrokersFee = rdrReader["Brokers_Fee"].ToString();
                        objCoverage.CIGA = rdrReader["CIGA"].ToString();
                        objCoverage.Collision = rdrReader["Collision"].ToString();
                        objCoverage.CollissionDeductibleWaiver = rdrReader["Collission_Deductible_Waiver"].ToString();
                        objCoverage.CombinedFPB = rdrReader["Combined_FPB"].ToString();
                        objCoverage.CombinedSingleLimits = rdrReader["Combined_Single_Limits"].ToString();
                        objCoverage.Comprehensive = rdrReader["Comprehensive"].ToString();
                        objCoverage.ExpenseConstant = rdrReader["Expense_Constant"].ToString();
                        objCoverage.ExtraMedical = rdrReader["Extra_Medical"].ToString();
                        objCoverage.FilingFee = rdrReader["Filing_Fee"].ToString();
                        objCoverage.Funeral = rdrReader["Funeral"].ToString();
                        objCoverage.IncomeLoss = rdrReader["Income_Loss"].ToString();
                        objCoverage.Medical = rdrReader["Medical"].ToString();
                        objCoverage.PersonalFamilyProtection = rdrReader["Personal_Family_Protection"].ToString();
                        objCoverage.PolicyFee = rdrReader["Policy_Fee"].ToString();
                        objCoverage.PropertyDamage = rdrReader["Property_Damage"].ToString();
                        objCoverage.RentalReimbursement = rdrReader["Rental_Reimbursement"].ToString();
                        objCoverage.SpecialEquipment = rdrReader["Special_Equipment"].ToString();
                        objCoverage.Towing = rdrReader["Towing"].ToString();
                        objCoverage.UnderInsuredMotoristStacked = rdrReader["UnderInsured_Motorist_Stacked"].ToString();
                        objCoverage.UnderInsuredMotoristUnStacked = rdrReader["UnderInsured_Motorist_UnStacked"].ToString();
                        objCoverage.UninsuredMotoristBI = rdrReader["Uninsured_Motorist_BI"].ToString();
                        objCoverage.UninsuredMotoristPD = rdrReader["Uninsured_Motorist_PD"].ToString();
                        objCoverage.UnInsuredMotoristStacked = rdrReader["UnInsured_Motorist_Stacked"].ToString();
                        objCoverage.UnInsuredMotoristUnStacked = rdrReader["UnInsured_Motorist_UnStacked"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                //handle error
                objCoverage = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : CoverageInformationGet() ";
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
            }

            //return results
            return (objCoverage);

        }//end : CoverageInformationGet

        //define method : PremiumFinanceCoInformationGet
        public ClaimsDocumentInputDocumentWhitehillPremiumFinanceCo PremiumFinanceCoInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            ClaimsDocumentInputDocumentWhitehillPremiumFinanceCo objPremiumFinanceCo = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_PremiumFinance";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (PolicyNumber) and set its properties
                SqlParameter paramPolicyNumber = new SqlParameter();
                paramPolicyNumber.ParameterName = "@policyno";
                paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                paramPolicyNumber.Direction = ParameterDirection.Input;
                paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNumber);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();


                objPremiumFinanceCo = new ClaimsDocumentInputDocumentWhitehillPremiumFinanceCo();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //create PremiumFinanceCo object
                    //objPremiumFinanceCo = new ClaimsDocumentInputDocumentWhitehillPremiumFinanceCo();
                    //fill coverage object
                    while (rdrReader.Read())
                    {
                        objPremiumFinanceCo.PremiumFinanceCoEmailAddress = rdrReader["PremiumFinanceCoEmailAddress"].ToString();
                        objPremiumFinanceCo.PremiumFinanceCoName = rdrReader["PremiumFinanceCoName"].ToString();
                        objPremiumFinanceCo.PremiumFinanceCoAddressLine1 = rdrReader["PremiumFinanceCoAddress1"].ToString();
                        objPremiumFinanceCo.PremiumFinanceCoAddressLine2 = rdrReader["PremiumFinanceCoAddressLine2"].ToString();
                        objPremiumFinanceCo.PremiumFinanceCoAddressLine3 = rdrReader["PremiumFinanceCoAddressLine3"].ToString();
                        objPremiumFinanceCo.PremiumFinanceCoPhoneNumber = rdrReader["PremiumFinanceCoPhoneNumber"].ToString();
                        objPremiumFinanceCo.PremiumFinanceCoFaxNumber = rdrReader["PremiumFinanceCoFaxNumber"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objPremiumFinanceCo = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : PremiumFinanceCoInformationGet() ";
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

            }

            //return results
            return (objPremiumFinanceCo);

        }//end : PremiumFinanceCoInformationGet

        //define method : LienHolderInformationGet
        public ClaimsDocumentInputDocumentWhitehillLienHolder LienHolderInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            ClaimsDocumentInputDocumentWhitehillLienHolder objLienHolder = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_LeinHolder";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (PolicyNumber) and set its properties
                SqlParameter paramPolicyNumber = new SqlParameter();
                paramPolicyNumber.ParameterName = "@policyno";
                paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                paramPolicyNumber.Direction = ParameterDirection.Input;
                paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNumber);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //create LienHolder object
                    objLienHolder = new ClaimsDocumentInputDocumentWhitehillLienHolder();
                    //fill coverage object
                    while (rdrReader.Read())
                    {
                        objLienHolder.LienHolderName = rdrReader["LeinHolderName"].ToString();
                        objLienHolder.LienHolderAddressLine1 = rdrReader["LeinHolderAddress1"].ToString();
                        objLienHolder.LienHolderAddressLine2 = rdrReader["LeinHolderAddressLine2"].ToString();
                        objLienHolder.LienHolderAddressLine3 = rdrReader["LeinHolderAddressLine3"].ToString();
                        objLienHolder.LienHolderPhoneNumber = rdrReader["LeinHolderPhoneNumber"].ToString();
                        objLienHolder.LienHolderFaxNumber = rdrReader["LeinHolderFaxNumber"].ToString();
                        objLienHolder.LienHolderEmailAddress = rdrReader["LeinHolderEmailAddress"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objLienHolder = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : LienHolderInformationGet() ";
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

            }

            //return results
            return (objLienHolder);

        }//end : LienHolderInformationGet

        //Method xNamedInsuredInformationGet is used for debugging purposes
        public ClaimsDocumentInputDocumentWhitehillNamedInsured xNamedInsuredInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //create objNamedInsured object
            ClaimsDocumentInputDocumentWhitehillNamedInsured objNamedInsured = new ClaimsDocumentInputDocumentWhitehillNamedInsured();

            objNamedInsured.NamedInsuredFirstName = "HARDCODED";
            objNamedInsured.NamedInsuredMiddleInitial = "HARDCODED";
            objNamedInsured.NamedInsuredLastName = "HARDCODED";
            objNamedInsured.NamedInsuredAddressLine1 = "HARDCODED";
            objNamedInsured.NamedInsuredAddressLine2 = "HARDCODED";
            objNamedInsured.NamedInsuredAddressLine3 = "HARDCODED";
            objNamedInsured.NamedInsuredCity = "HARDCODED";
            objNamedInsured.NamedInsuredState = "HARDCODED";
            objNamedInsured.NamedInsuredZip = "HARDCODED";
            objNamedInsured.NamedInsuredZip4 = "HARDCODED";
            objNamedInsured.NamedInsuredPhoneNumber = "HARDCODED";
            objNamedInsured.NamedInsuredFaxNumber = "HARDCODED";
            objNamedInsured.NamedInsuredEmailAddress = "HARDCODED";
            objNamedInsured.NamedInsuredGaragingAddr = "HARDCODED";
            objNamedInsured.NamedInsuredGaragingCity = "HARDCODED";
            objNamedInsured.NamedInsuredGaragingState = "HARDCODED";
            objNamedInsured.NamedInsuredGaragingZip = "HARDCODED";
            objNamedInsured.NamedInsuredPostNetZip = "HARDCODED";
            objNamedInsured.NamedInsuredFullName = "HARDCODED";
            objNamedInsured.MatchLevel = "HARDCODED";

            return (objNamedInsured);


        }

        //define method : NamedInsuredInformationGet
        public ClaimsDocumentInputDocumentWhitehillNamedInsured NamedInsuredInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {

            //declare variables
            ClaimsDocumentInputDocumentWhitehillNamedInsured objNamedInsured = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_GetInsuredContactInfo";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (PolicyNumber) and set its properties
                SqlParameter paramPolicyNumber = new SqlParameter();
                paramPolicyNumber.ParameterName = "@policyno";
                paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                paramPolicyNumber.Direction = ParameterDirection.Input;
                paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNumber);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //create objNamedInsured object
                    objNamedInsured = new ClaimsDocumentInputDocumentWhitehillNamedInsured();
                    //fill coverage object
                    while (rdrReader.Read())
                    {
                        objNamedInsured.NamedInsuredFirstName = rdrReader["NamedInsuredFirstName"].ToString();
                        objNamedInsured.NamedInsuredMiddleInitial = rdrReader["NamedInsuredMiddleInitial"].ToString();
                        objNamedInsured.NamedInsuredLastName = rdrReader["NamedInsuredLastName"].ToString();
                        objNamedInsured.NamedInsuredAddressLine1 = rdrReader["NamedInsuredAddressLine1"].ToString();
                        objNamedInsured.NamedInsuredAddressLine2 = rdrReader["NamedInsuredAddressLine2"].ToString();
                        objNamedInsured.NamedInsuredAddressLine3 = rdrReader["NamedInsuredAddressLine3"].ToString();
                        objNamedInsured.NamedInsuredCity = rdrReader["NamedInsuredCity"].ToString();
                        objNamedInsured.NamedInsuredState = rdrReader["NamedInsuredState"].ToString();
                        objNamedInsured.NamedInsuredZip = rdrReader["NamedInsuredZip"].ToString();
                        objNamedInsured.NamedInsuredZip4 = rdrReader["NamedInsuredZip4"].ToString();
                        objNamedInsured.NamedInsuredPhoneNumber = rdrReader["NamedInsuredPhoneNumber"].ToString();
                        objNamedInsured.NamedInsuredFaxNumber = rdrReader["NamedInsuredFaxNumber"].ToString();
                        objNamedInsured.NamedInsuredEmailAddress = rdrReader["NamedInsuredEmailAddress"].ToString();
                        objNamedInsured.NamedInsuredGaragingAddr = rdrReader["NamedInsuredGaragingAddr"].ToString();
                        objNamedInsured.NamedInsuredGaragingCity = rdrReader["NamedInsuredGaragingCity"].ToString();
                        objNamedInsured.NamedInsuredGaragingState = rdrReader["NamedInsuredGaragingState"].ToString();
                        objNamedInsured.NamedInsuredGaragingZip = rdrReader["NamedInsuredGaragingZip"].ToString();
                        objNamedInsured.NamedInsuredPostNetZip = rdrReader["NamedInsuredPostNetZip"].ToString();
                        objNamedInsured.NamedInsuredFullName = rdrReader["NamedInsuredFullName"].ToString();
                        objNamedInsured.MatchLevel = rdrReader["MatchLevel"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objNamedInsured = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : NamedInsuredInformationGet() ";
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

            }

            //return results
            return (objNamedInsured);

        }//end : LienHolderInformationGet

        //define method : VehicleInformationGet
        public ClaimsDocumentInputDocumentWhitehillVehicle VehicleInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            ClaimsDocumentInputDocumentWhitehillVehicle objVehicle = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_Vehicle";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (PolicyNumber) and set its properties
                SqlParameter paramPolicyNumber = new SqlParameter();
                paramPolicyNumber.ParameterName = "@policyno";
                paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                paramPolicyNumber.Direction = ParameterDirection.Input;
                paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNumber);

                //Add input parameter for (CarNumber) and set its properties
                SqlParameter paramCarNumber = new SqlParameter();
                paramCarNumber.ParameterName = "@carno";
                paramCarNumber.SqlDbType = SqlDbType.Int;
                paramCarNumber.Direction = ParameterDirection.Input;
                paramCarNumber.Value = objDocumentGenerationRequest.CarNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCarNumber);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //create Vehicle object
                    objVehicle = new ClaimsDocumentInputDocumentWhitehillVehicle();
                    //fill coverage object
                    while (rdrReader.Read())
                    {
                        objVehicle.VehicleSerialNumber = rdrReader["VehicleSerialNumber"].ToString();
                        objVehicle.VehicleYear = rdrReader["VehicleYear"].ToString();
                        objVehicle.VehicleMake = rdrReader["VehicleMake"].ToString();
                        objVehicle.VehicleModel = rdrReader["VehicleModel"].ToString();
                        objVehicle.VehiclePlateNumber = rdrReader["VehiclePlateNumber"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objVehicle = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : VehicleInformationGet() ";
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

            }

            //return results
            return (objVehicle);

        }//end : VehicleInformationGet

        //define method : AdjusterInformationGet
        public ClaimsDocumentInputDocumentWhitehillAdjuster AdjusterInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            ClaimsDocumentInputDocumentWhitehillAdjuster objAdjuster = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_Adjuster";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (ClaimNumber) and set its properties
                SqlParameter paramClaimNumber = new SqlParameter();
                paramClaimNumber.ParameterName = "@ClaimNo";
                paramClaimNumber.SqlDbType = SqlDbType.VarChar;
                paramClaimNumber.Direction = ParameterDirection.Input;
                paramClaimNumber.Value = objDocumentGenerationRequest.ClaimNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimNumber);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //create Adjuster object
                    objAdjuster = new ClaimsDocumentInputDocumentWhitehillAdjuster();
                    //fill coverage object
                    while (rdrReader.Read())
                    {
                        objAdjuster.AdjusterPhoneNumber = rdrReader["AdjusterPhoneNumber"].ToString();
                        objAdjuster.AdjusterFirstName = rdrReader["AdjusterFirstName"].ToString();
                        objAdjuster.AdjusterLastName = rdrReader["AdjusterLastName"].ToString();
                        objAdjuster.AdjusterMiddleInitial = rdrReader["AdjusterMiddleInitial"].ToString();
                        objAdjuster.AdjusterEmailAddress = rdrReader["AdjusterEmailAddress"].ToString();
                        objAdjuster.AdjusterFaxNumber = rdrReader["AdjusterFaxNumber"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objAdjuster = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : AdjusterInformationGet() ";
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

            }

            //return results
            return (objAdjuster);

        }//end : AdjusterInformationGet

        //define method : LossDescription
        public ClaimsDocumentInputDocumentWhitehillLossDescription LossDescriptionInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            ClaimsDocumentInputDocumentWhitehillLossDescription objLossDescription = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_LossDescription";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (ClaimNumber) and set its properties
                SqlParameter paramClaimNumber = new SqlParameter();
                paramClaimNumber.ParameterName = "@ClaimNo";
                paramClaimNumber.SqlDbType = SqlDbType.VarChar;
                paramClaimNumber.Direction = ParameterDirection.Input;
                paramClaimNumber.Value = objDocumentGenerationRequest.ClaimNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimNumber);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //create LossDescription object
                    objLossDescription = new ClaimsDocumentInputDocumentWhitehillLossDescription();
                    //fill coverage object
                    while (rdrReader.Read())
                    {
                        objLossDescription.ClaimNumber = rdrReader["ClaimNumber"].ToString();
                        objLossDescription.ClaimDateOfLoss = rdrReader["ClaimDateOfLoss"].ToString();
                        objLossDescription.ClaimReportDate = rdrReader["ClaimReportDate"].ToString();
                        objLossDescription.LossDescriptionStreetAddress = rdrReader["LossDescriptionStreetAddress"].ToString();
                        objLossDescription.LossDescriptionLocation = rdrReader["LossDescriptionLocation"].ToString();
                        objLossDescription.LossDescriptionCity = rdrReader["LossDescriptionCity"].ToString();
                        objLossDescription.LossDescriptionState = rdrReader["LossDescriptionState"].ToString();
                        objLossDescription.DayofLoss = rdrReader["DayofLoss"].ToString();
                        objLossDescription.MonthofLoss = rdrReader["MonthofLoss"].ToString();
                        objLossDescription.YearofLoss = rdrReader["YearofLoss"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objLossDescription = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : LossDescriptionInformationGet() ";
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

            }

            //return results
            return (objLossDescription);

        }//end : LossDescriptionInformationGet

        //define method : ProducerInformationGet
        public ClaimsDocumentInputDocumentWhitehillProducer ProducerInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            ClaimsDocumentInputDocumentWhitehillProducer objProducer = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                //cmdCommand.CommandText = "WhiteHill_XMLGeneration_Producer";
                cmdCommand.CommandText = "ClaimsDocs_GetProducerInfo";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (PolicyNumber) and set its properties
                SqlParameter paramPolicyNumber = new SqlParameter();
                paramPolicyNumber.ParameterName = "@policyno";
                paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                paramPolicyNumber.Direction = ParameterDirection.Input;
                paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNumber);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //create Producer object
                    objProducer = new ClaimsDocumentInputDocumentWhitehillProducer();
                    //fill coverage object
                    while (rdrReader.Read())
                    {
                        objProducer.ProducerState = rdrReader["ProducerState"].ToString();
                        objProducer.ProducerCode = rdrReader["ProducerCode"].ToString();
                        objProducer.ProducerZip = rdrReader["ProducerZip"].ToString();
                        objProducer.ProducerName = rdrReader["ProducerName"].ToString();
                        objProducer.ProducerPhoneNumber = rdrReader["ProducerPhoneNumber"].ToString();
                        objProducer.ProducerFaxNumber = rdrReader["ProducerFaxNumber"].ToString();
                        objProducer.ProducerEmailAddress = rdrReader["ProducerEmailAddress"].ToString();
                        objProducer.ProducerAddressLine1 = rdrReader["ProducerAddressLine1"].ToString();
                        objProducer.ProducerAddressLine2 = rdrReader["ProducerAddressLine2"].ToString();
                        objProducer.ProducerAddressLine3 = rdrReader["ProducerAddressLine3"].ToString();
                        objProducer.MatchLevel = rdrReader["MatchLevel"].ToString();
                        objProducer.MailPostNetZip = rdrReader["MailPostNetZip"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objProducer = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : ProducerInformationGet() ";
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

            }

            //return results
            return (objProducer);

        }//end : ProducerInformationGet

        //define method : AttorneyInformationGet
        public ClaimsDocumentInputDocumentWhitehillAttorney AttorneyInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            ClaimsDocumentInputDocumentWhitehillAttorney objAttorney = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_GetAttorney";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (ClaimUKey) and set its properties
                SqlParameter paramClaimUKey = new SqlParameter();
                paramClaimUKey.ParameterName = "@ClaimUKey";
                paramClaimUKey.SqlDbType = SqlDbType.Int;
                paramClaimUKey.Direction = ParameterDirection.Input;
                paramClaimUKey.Value = objDocumentGenerationRequest.ContactNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimUKey);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //create Attorney object
                    objAttorney = new ClaimsDocumentInputDocumentWhitehillAttorney();
                    //fill coverage object
                    while (rdrReader.Read())
                    {
                        objAttorney.AttorneyFirstName = rdrReader["AttorneyFirstName"].ToString();
                        objAttorney.AttorneyMiddleInitial = rdrReader["AttorneyMiddleInitial"].ToString();
                        objAttorney.AttorneyLastName = rdrReader["AttorneyLastName"].ToString();
                        objAttorney.AttorneyAddressLine1 = rdrReader["AttorneyAddressLine1"].ToString();
                        objAttorney.AttorneyAddressLine2 = rdrReader["AttorneyAddressLine2"].ToString();
                        objAttorney.AttorneyAddressLine3 = rdrReader["AttorneyAddressLine3"].ToString();
                        objAttorney.AttorneyCity = rdrReader["AttorneyCity"].ToString();
                        objAttorney.AttorneyState = rdrReader["AttorneyState"].ToString();
                        objAttorney.AttorneyZip = rdrReader["AttorneyZip"].ToString();
                        objAttorney.AttorneyZip4 = rdrReader["AttorneyZip4"].ToString();
                        objAttorney.AttorneyPhoneNumber = rdrReader["AttorneyPhoneNumber"].ToString();
                        objAttorney.AttorneyFaxNumber = rdrReader["AttorneyFaxNumber"].ToString();
                        objAttorney.AttorneyEmailAddress = rdrReader["AttorneyEmailAddress"].ToString();
                        objAttorney.MailPostNetZip = rdrReader["MailPostNetZip"].ToString();
                        objAttorney.MatchLevel = rdrReader["MatchLevel"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objAttorney = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : AttorneyInformationGet() ";
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

            }

            //return results
            return (objAttorney);

        }//end : AttorneyInformationGet

        //define method : DocGenerationRequestInformationGet
        public DocGenerationRequest DocGenerationRequestInformationGet(DocGenerationRequest objDocumentGenerationRequest)
        {
            //declare variables
            DocGenerationRequest objDocGenerationRequestIs = new DocGenerationRequest();
            ClaimsDocumentSubmitter objSubmitter = new ClaimsDocumentSubmitter();
            CDUsers objCDUser = new CDUsers();
            User objUser = new User();
            ClaimsDocumentAddressee objAddressee = new ClaimsDocumentAddressee();
            ClaimsDocumentSubmitterDepartment objDepartment = new ClaimsDocumentSubmitterDepartment();

            try
            {
                //get user
                objUser.UserName = objDocumentGenerationRequest.UserName;
                objUser = objCDUser.UserReadByUserName(objUser, CDSupport.ClaimsDocsDBConnectionString);
                //get addressee
                objAddressee = this.AddresseeInformationGet(objDocumentGenerationRequest, CDSupport.AMSDBConnectionString);
                //assgin request
                objDocGenerationRequestIs = objDocumentGenerationRequest;
                //set request values
                objDocGenerationRequestIs.UserIDNumber = objUser.UserID;
                objDocGenerationRequestIs.UserDepartment = objUser.DepartmentName;
                objDocGenerationRequestIs.AddresseeName = objAddressee.AddresseeName;
                objDocGenerationRequestIs.AddresseeAddressLine1 = objAddressee.AddresseeAddressLine1;
                objDocGenerationRequestIs.AddresseeAddressLine2 = objAddressee.AddresseeAddressLine2;
                objDocGenerationRequestIs.AddresseeCity = objAddressee.AddresseeCity;
                objDocGenerationRequestIs.AddresseeState = objAddressee.AddresseeState;
                objDocGenerationRequestIs.AddresseeZipCode = objAddressee.AddresseeZip;
            }
            catch (Exception ex)
            {
                //handle error
                objDocGenerationRequestIs = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DocGenerationRequest() ";
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
                objSubmitter = null;
                objAddressee = null;
            }

            //return results
            return (objDocGenerationRequestIs);

        }//end : DocGenerationRequestInformationGet

        //define method : DocGenerationRequestInformationGetBase
        public DocGenerationRequest DocGenerationRequestInformationGetByApprovalID(int intApprovalID)
        {
            //declare variables
            DocGenerationRequest objDocGenerationRequest_tmp = new DocGenerationRequest();
            DocGenerationRequest objDocGenerationRequest_out = new DocGenerationRequest();
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(CDSupport.ClaimsDocsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spGetDocGenerationRequestInformationByApprovalID";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (ApprovalQueueID) and set its properties
                SqlParameter paramApprovalQueueID = new SqlParameter();
                paramApprovalQueueID.ParameterName = "@intApprovalQueueID";
                paramApprovalQueueID.SqlDbType = SqlDbType.Int;
                paramApprovalQueueID.Direction = ParameterDirection.Input;
                paramApprovalQueueID.Value = intApprovalID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramApprovalQueueID);

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
                        objDocGenerationRequest_tmp.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocGenerationRequest_tmp.PolicyNumber = rdrReader["PolicyNo"].ToString();
                        objDocGenerationRequest_tmp.ClaimNumber = rdrReader["ClaimNo"].ToString();
                        objDocGenerationRequest_tmp.ContactNumber = int.Parse(rdrReader["ContactNo"].ToString());
                        objDocGenerationRequest_tmp.ContactType = int.Parse(rdrReader["ContactType"].ToString());
                        objDocGenerationRequest_tmp.UserName = rdrReader["UserName"].ToString();
                    }
                    // now that we have a base DocGenerationRequest
                    // go get the rest of data to complete a DocGenerationRequest
                    objDocGenerationRequest_out = DocGenerationRequestInformationGet(objDocGenerationRequest_tmp);
                }
            }
            catch (Exception ex)
            {
                //handle error
                objDocGenerationRequest_tmp = null;
                objDocGenerationRequest_out = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DocGenerationRequestInformationGetByApprovalID() ";
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
            }

            //return results
            return (objDocGenerationRequest_out);

        }//end : DocGenerationRequestInformationGetBase

        //define method : VehicleDataInformationGet
        public VehicleData VehicleDataInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            VehicleData objVehicleData = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_Search4_Insured_Vehicle";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (PolicyNumber) and set its properties
                SqlParameter paramPolicyNumber = new SqlParameter();
                paramPolicyNumber.ParameterName = "@policyno";
                paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                paramPolicyNumber.Direction = ParameterDirection.Input;
                paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNumber);

                //Add input parameter for (ContactUKey) and set its properties
                SqlParameter paramCarNumber = new SqlParameter();
                paramCarNumber.ParameterName = "@ContactUKey";
                paramCarNumber.SqlDbType = SqlDbType.Int;
                paramCarNumber.Direction = ParameterDirection.Input;
                paramCarNumber.Value = objDocumentGenerationRequest.ContactNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCarNumber);

                //Add input parameter for (VehicleFound) and set its properties
                SqlParameter paramVehicleFound = new SqlParameter();
                paramVehicleFound.ParameterName = "@VehicleFound";
                paramVehicleFound.SqlDbType = SqlDbType.Int;
                paramVehicleFound.Direction = ParameterDirection.Output;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramVehicleFound);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //create Vehicle object
                    objVehicleData = new VehicleData();
                    //fill vehicle object
                    while (rdrReader.Read())
                    {
                        objVehicleData.VehicleUKey = rdrReader["VehicleUKey"].ToString();
                        objVehicleData.CarNumber = int.Parse(rdrReader["CarNo"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                objVehicleData = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : VehicleDataInformationGet() ";
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

            }

            //return results
            return (objVehicleData);

        }//end : VehicleDataInformationGet

        //define method : ClaimGetVehicleList
        public List<ClaimsDocumentAllCoveragesCar> ClaimGetVehicleList(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            List<ClaimsDocumentAllCoveragesCar> listCars = new List<ClaimsDocumentAllCoveragesCar>();
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intIndex = 0;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_VehicleCoverages";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                #region Get Car(s) Parameter Setup

                //add command parameters		
                //Add input parameter for (@intVehicleInfoRequestTypeID) and set its properties
                SqlParameter paramVehicleInfoRequestTypeID = new SqlParameter();
                paramVehicleInfoRequestTypeID.ParameterName = "@intVehicleInfoRequestTypeID";
                paramVehicleInfoRequestTypeID.SqlDbType = SqlDbType.Int;
                paramVehicleInfoRequestTypeID.Direction = ParameterDirection.Input;
                paramVehicleInfoRequestTypeID.Value = 1; //1 = Get the VIN of vehicles associated with claim
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramVehicleInfoRequestTypeID);

                //Add input parameter for (PolicyNumber) and set its properties
                SqlParameter paramPolicyNumber = new SqlParameter();
                paramPolicyNumber.ParameterName = "@varPolicyNo";
                paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                paramPolicyNumber.Direction = ParameterDirection.Input;
                paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNumber);

                //Add input parameter for (ClaimNumber) and set its properties
                SqlParameter paramClaimNumber = new SqlParameter();
                paramClaimNumber.ParameterName = "@varClaimNo";
                paramClaimNumber.SqlDbType = SqlDbType.VarChar;
                paramClaimNumber.Direction = ParameterDirection.Input;
                paramClaimNumber.Value = objDocumentGenerationRequest.ClaimNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimNumber);

                //Add input parameter for (VIN) and set its properties
                SqlParameter paramVIN = new SqlParameter();
                paramVIN.ParameterName = "@varVIN";
                paramVIN.SqlDbType = SqlDbType.VarChar;
                paramVIN.Direction = ParameterDirection.Input;
                paramVIN.Value = "";
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramVIN);

                //Add input parameter for (CompanyNumber) and set its properties
                SqlParameter paramCompanyNumber = new SqlParameter();
                paramCompanyNumber.ParameterName = "@varCompanyNumber";
                paramCompanyNumber.SqlDbType = SqlDbType.VarChar;
                paramCompanyNumber.Direction = ParameterDirection.Input;
                paramCompanyNumber.Value = objDocumentGenerationRequest.CompanyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCompanyNumber);

                //Add input parameter for (Coverage) and set its properties
                SqlParameter paramCoverage = new SqlParameter();
                paramCoverage.ParameterName = "@varCoverage";
                paramCoverage.SqlDbType = SqlDbType.VarChar;
                paramCoverage.Direction = ParameterDirection.Input;
                paramCoverage.Value = "";
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCoverage);

                //Add input parameter for (LimitCode) and set its properties
                SqlParameter paramLimitCode = new SqlParameter();
                paramLimitCode.ParameterName = "@intLimitCode";
                paramLimitCode.SqlDbType = SqlDbType.Int;
                paramLimitCode.Direction = ParameterDirection.Input;
                paramLimitCode.Value = 0;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramLimitCode);

                #endregion

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //fill vehicle object
                    while (rdrReader.Read())
                    {
                        if (string.IsNullOrEmpty(rdrReader["VIN"].ToString()) == false)
                        {
                            if (rdrReader["VIN"].ToString().ToUpper().Equals("UNK") == false)
                            {
                                //make sure we are not adding a duplicate VIN
                                //this code is here to accomodate duplicat VIN results
                                //generated from the stored procedure results
                                if (listCars.Count > 0)
                                {
                                    if (listCars[intIndex].VIN.ToUpper().Equals(rdrReader["VIN"].ToString().ToUpper()) == false)
                                    {
                                        //create car object
                                        ClaimsDocumentAllCoveragesCar objCar = new ClaimsDocumentAllCoveragesCar();
                                        objCar.VIN = rdrReader["vin"].ToString();
                                        objCar.CarYear = rdrReader["Year"].ToString();
                                        objCar.CarMake = rdrReader["Make"].ToString();
                                        objCar.CarModel = rdrReader["Model"].ToString();
                                        //add to list
                                        listCars.Add(objCar);
                                    }
                                }
                                else
                                {
                                    //create car object
                                    ClaimsDocumentAllCoveragesCar objCar = new ClaimsDocumentAllCoveragesCar();
                                    objCar.VIN = rdrReader["vin"].ToString();
                                    objCar.CarYear = rdrReader["Year"].ToString();
                                    objCar.CarMake = rdrReader["Make"].ToString();
                                    objCar.CarModel = rdrReader["Model"].ToString();
                                    //add to list
                                    listCars.Add(objCar);

                                }

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                listCars = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : ClaimGetVehicleList() ";
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
            }

            //return results
            return (listCars);

        }//end : ClaimGetVehicleList

        //define method : VehicleDeductibeInformationGet
        private Dictionary<string, decimal> VehicleDeductibeInformationGet(string strPolicyNumber, string strGenesisDBConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            Dictionary<string, decimal> dicPolicyCandL = new Dictionary<string, decimal>();

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strGenesisDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "CR_Vehicle_Covs_Lmts_Prem";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //Add input parameter for (objPolicyDeclaration.PolicyNo) and set its properties
                SqlParameter paramPolicyNo = new SqlParameter();
                paramPolicyNo.ParameterName = "@PolicyNo";
                paramPolicyNo.SqlDbType = SqlDbType.VarChar;
                paramPolicyNo.Direction = ParameterDirection.Input;
                paramPolicyNo.Value = strPolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNo);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader(CommandBehavior.CloseConnection);

                //check for rows
                if (rdrReader.HasRows == true)
                {
                    while (rdrReader.Read())
                    {
                        //add values to list
                        switch (rdrReader["CovCode"].ToString())
                        {
                            case "060": //Comprehensive
                            case "065": //Comprehensive with Full Safety Glass Coverage
                            case "070": //Collision
                            case "075": //Collision Deductible Waiver
                                dicPolicyCandL.Add(rdrReader["CarNo"].ToString().Trim() + rdrReader["CovCode"].ToString().Trim(), decimal.Parse(rdrReader["LimitDesc"].ToString()));
                                break;
                        }//end : switch (rdrPolicyCandL["CovAbbr"].ToString())
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
                objClaimsLog.MessageIs = "Method : VehicleDeductibeInformationGet() ";
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
            return (dicPolicyCandL);
        }//end method : VehicleDeductibeInformationGet

        //define method : LoadPolicyDeclarationCoveragesDeductibleInfoDB
        private Dictionary<string, decimal> LoadPolicyDeclarationCoveragesDeductibleInfoDB(string strPolicyNumber, string strConnectionString)
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
                cmdCommand.CommandText = "CR_Vehicle_Covs_Lmts_Prem";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //Add input parameter for (objPolicyDeclaration.PolicyNo) and set its properties
                SqlParameter paramPolicyNo = new SqlParameter();
                paramPolicyNo.ParameterName = "@PolicyNo";
                paramPolicyNo.SqlDbType = SqlDbType.VarChar;
                paramPolicyNo.Direction = ParameterDirection.Input;
                paramPolicyNo.Value = strPolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNo);

                //open connection
                cnnConnection.Open();
                //execute command

                rdrReader = cmdCommand.ExecuteReader(CommandBehavior.CloseConnection);

                //check for rows
                if (rdrReader.HasRows == true)
                {
                    while (rdrReader.Read())
                    {
                        //add values to list
                        switch (rdrReader["CovCode"].ToString())
                        {
                            case "060": //Comprehensive
                            case "065": //Comprehensive with Full Safety Glass Coverage
                            case "070": //Collision
                            case "075": //Collision Deductible Waiver
                                dicPolicyCandL.Add(rdrReader["CarNo"].ToString().Trim() + rdrReader["CovCode"].ToString().Trim(), decimal.Parse(rdrReader["LimitDesc"].ToString()));
                                break;
                        }//end : switch (rdrPolicyCandL["CovAbbr"].ToString())
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
                objClaimsLog.MessageIs = "Method : LoadPolicyDeclarationCoveragesDeductibleInfoDB() ";
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

                if (cmdCommand != null)
                {
                    cmdCommand = null;
                }
            }
            //return result
            return (dicPolicyCandL);
        }//end method : VehicleGetFilteredCoverageList

        //define method : VehicleGetFilteredCoverageList
        public List<ClaimsDocumentAllCoveragesCar> VehicleGetFilteredCoverageList(DocGenerationRequest objDocumentGenerationRequest, List<ClaimsDocumentAllCoveragesCar> listCars, string strClaimsDBConnectionString)
        {
            //declare variables
            List<ClaimsDocumentAllCoveragesCarCarCoverage> listCarCoverages = new List<ClaimsDocumentAllCoveragesCarCarCoverage>();
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intCarIndex = 0;
            bool blnAddCoverage = false;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_VehicleCoverages";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                for (intCarIndex = 0; intCarIndex < listCars.Count; intCarIndex++)
                {

                    if (cmdCommand.Parameters.Count > 0)
                    {
                        cmdCommand.Parameters.Clear();
                    }

                    #region Get Vehicle Coverage Parameter Setup


                    //add command parameters		
                    //Add input parameter for (@intVehicleInfoRequestTypeID) and set its properties
                    SqlParameter paramVehicleInfoRequestTypeID = new SqlParameter();
                    paramVehicleInfoRequestTypeID.ParameterName = "@intVehicleInfoRequestTypeID";
                    paramVehicleInfoRequestTypeID.SqlDbType = SqlDbType.Int;
                    paramVehicleInfoRequestTypeID.Direction = ParameterDirection.Input;
                    paramVehicleInfoRequestTypeID.Value = 2;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramVehicleInfoRequestTypeID);

                    //Add input parameter for (PolicyNumber) and set its properties
                    SqlParameter paramPolicyNumber = new SqlParameter();
                    paramPolicyNumber.ParameterName = "@varPolicyNo";
                    paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                    paramPolicyNumber.Direction = ParameterDirection.Input;
                    paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramPolicyNumber);

                    //Add input parameter for (ClaimNumber) and set its properties
                    SqlParameter paramClaimNumber = new SqlParameter();
                    paramClaimNumber.ParameterName = "@varClaimNo";
                    paramClaimNumber.SqlDbType = SqlDbType.VarChar;
                    paramClaimNumber.Direction = ParameterDirection.Input;
                    paramClaimNumber.Value = objDocumentGenerationRequest.ClaimNumber;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramClaimNumber);

                    //Add input parameter for (VIN) and set its properties
                    SqlParameter paramVIN = new SqlParameter();
                    paramVIN.ParameterName = "@varVIN";
                    paramVIN.SqlDbType = SqlDbType.VarChar;
                    paramVIN.Direction = ParameterDirection.Input;
                    paramVIN.Value = listCars[intCarIndex].VIN;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramVIN);

                    //Add input parameter for (CompanyNumber) and set its properties
                    SqlParameter paramCompanyNumber = new SqlParameter();
                    paramCompanyNumber.ParameterName = "@varCompanyNumber";
                    paramCompanyNumber.SqlDbType = SqlDbType.VarChar;
                    paramCompanyNumber.Direction = ParameterDirection.Input;
                    paramCompanyNumber.Value = objDocumentGenerationRequest.CompanyNumber;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramCompanyNumber);

                    //Add input parameter for (Coverage) and set its properties
                    SqlParameter paramCoverage = new SqlParameter();
                    paramCoverage.ParameterName = "@varCoverage";
                    paramCoverage.SqlDbType = SqlDbType.VarChar;
                    paramCoverage.Direction = ParameterDirection.Input;
                    paramCoverage.Value = "";
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramCoverage);

                    //Add input parameter for (LimitCode) and set its properties
                    SqlParameter paramLimitCode = new SqlParameter();
                    paramLimitCode.ParameterName = "@intLimitCode";
                    paramLimitCode.SqlDbType = SqlDbType.Int;
                    paramLimitCode.Direction = ParameterDirection.Input;
                    paramLimitCode.Value = 0;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramLimitCode);

                    #endregion

                    if (cnnConnection.State == ConnectionState.Closed)
                    {
                        //open connection
                        cnnConnection.Open();
                    }

                    if (rdrReader != null)
                    {
                        if (rdrReader.IsClosed == false)
                        {
                            rdrReader.Close();
                        }
                    }

                    //execute command
                    rdrReader = cmdCommand.ExecuteReader();

                    //check for coverages for this car
                    if (rdrReader.HasRows == true)
                    {
                        while (rdrReader.Read())
                        {
                            //reset flag
                            blnAddCoverage = false;

                            //create car coverage object
                            ClaimsDocumentAllCoveragesCarCarCoverage objCarCoverage = new ClaimsDocumentAllCoveragesCarCarCoverage();

                            //get car coverage information
                            objCarCoverage.CarNumber = rdrReader["carno"].ToString();
                            objCarCoverage.CompanyNumber = rdrReader["Company"].ToString();
                            objCarCoverage.CoverageCodeNumber = rdrReader["Coverage"].ToString();
                            objCarCoverage.CoverageCode = CoverageCodeFromNumberToString(objCarCoverage.CompanyNumber, objCarCoverage.CoverageCodeNumber);
                            objCarCoverage.lp1e = rdrReader["lp_1e"].ToString();
                            objCarCoverage.Amount = rdrReader["original"].ToString();

                            //only add applicable coverages to list
                            switch (objCarCoverage.CoverageCodeNumber)
                            {
                                case "040": //
                                    //
                                    if (objDocumentGenerationRequest.PolicyNumber.ToUpper().Substring(0, 3).Equals("APA"))
                                    {
                                        blnAddCoverage = false;
                                    }
                                    else
                                    {
                                        blnAddCoverage = true;
                                    }
                                    break;

                                case "100": //Special Equipment
                                    //Special Equipment
                                    if (objDocumentGenerationRequest.PolicyNumber.ToUpper().Substring(0, 3).Equals("APA"))
                                    {
                                        blnAddCoverage = false;
                                    }
                                    else
                                    {
                                        blnAddCoverage = true;
                                    }
                                    break;

                                case "700": //ignore this coverage code : LOAN/LEASE
                                case "710": //ignore this coverage code : Additional Equipment
                                case "080": //ignore this coverage code : Personal Injury Protection
                                case "920": //ignore this coverage code : Billing / Service Charges
                                case "935": //ignore this coverage code : Auto Club
                                    //do not add coverage
                                    blnAddCoverage = false;
                                    break;

                                case "730": //Accidental Death Indemnity
                                    //check for valid states for Accidental Death Indemnity
                                    if (objDocumentGenerationRequest.PolicyNumber.ToUpper().Substring(0, 3).Equals("ATX"))
                                    {
                                        blnAddCoverage = true;
                                    }
                                    else
                                    {
                                        blnAddCoverage = false;
                                    }
                                    break;

                                case "081": //PERSONAL INJURY PROTECTION
                                    //check for valid states for PERSONAL INJURY PROTECTION
                                    if (objDocumentGenerationRequest.PolicyNumber.ToUpper().Substring(0, 3).Equals("ATX"))
                                    {
                                        blnAddCoverage = true;
                                    }
                                    else
                                    {
                                        blnAddCoverage = false;
                                    }
                                    break;

                                case "810": //ignore this coverage code : Policy Fee for all states except : AGA
                                    //This section accomodates AGA's need to gather policy fee information
                                    //from coverage data
                                    //if (objDocumentGenerationRequest.PolicyNumber.Substring(0, 3).ToUpper().Equals("AGA"))
                                    //{
                                    //    decimal.TryParse(rdrReader["PolicyFee"].ToString(), out decPolicyFee);
                                    //}
                                    blnAddCoverage = true;
                                    break;

                                default:
                                    blnAddCoverage = true;
                                    break;
                            }//end : switch (objCarCoverage.CoverageCodeNumber)

                            //check add car coverage add flag
                            if (blnAddCoverage == true)
                            {
                                //add car coverage information to list
                                listCarCoverages.Add(objCarCoverage);
                            }
                        }
                    }

                    //apply coverage to car
                    listCars[intCarIndex].CarCoverages = listCarCoverages.ToArray();

                }//end : for(intCarIndex=0;intCarIndex<listCars.Count;intCarIndex++)
            }
            catch (Exception ex)
            {
                //handle error
                listCarCoverages = null;
                objDocGenerationResponse.GeneralResponseCode = (int)GenerateMessage.ExceptionOccured;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : VehicleGetCoverageList() ";
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
            }

            //return results
            return (listCars);

        }//end : VehicleGetCoverageList

        //define method : VehicleGetCoverageLimits
        public List<ClaimsDocumentAllCoveragesCar> VehicleGetCoverageLimits(DocGenerationRequest objDocumentGenerationRequest, List<ClaimsDocumentAllCoveragesCar> listCars, string strClaimsDBConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intCarIndex = 0;
            int intCoverageIndex = 0;
            int intRentalDenominator = 0;
            int intTowingDenominator = 0;
            decimal decDeductibleAmount = 0;
            bool blnAddPerPersonLimit = false;
            bool blnAddPerAccidentLimit = false;
            bool blnAddDeductible = false;

            try
            {
                //get rental denominator
                intRentalDenominator = CDSupport.RentalDenominatorGetByCompanyNumber(int.Parse(objDocumentGenerationRequest.CompanyNumber));
                //get towing denominator
                intTowingDenominator = CDSupport.TowingDenominatorGetByCompanyNumber(int.Parse(objDocumentGenerationRequest.CompanyNumber));

                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_VehicleCoverages";
                cmdCommand.CommandType = CommandType.StoredProcedure;
                //open connection
                cnnConnection.Open();

                #region Car Parameter Setup

                for (intCarIndex = 0; intCarIndex < listCars.Count; intCarIndex++)
                {
                    for (intCoverageIndex = 0; intCoverageIndex < listCars[intCarIndex].CarCoverages.Length; intCoverageIndex++)
                    {
                        #region Setup Limit Retrieval Parameters

                        //add command parameters		
                        //Add input parameter for (@intVehicleInfoRequestTypeID) and set its properties
                        SqlParameter paramVehicleInfoRequestTypeID = new SqlParameter();
                        paramVehicleInfoRequestTypeID.ParameterName = "@intVehicleInfoRequestTypeID";
                        paramVehicleInfoRequestTypeID.SqlDbType = SqlDbType.Int;
                        paramVehicleInfoRequestTypeID.Direction = ParameterDirection.Input;
                        paramVehicleInfoRequestTypeID.Value = 3; //3 = Get detailed vechicle coverage information
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramVehicleInfoRequestTypeID);

                        //Add input parameter for (PolicyNumber) and set its properties
                        SqlParameter paramPolicyNumber = new SqlParameter();
                        paramPolicyNumber.ParameterName = "@varPolicyNo";
                        paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                        paramPolicyNumber.Direction = ParameterDirection.Input;
                        paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramPolicyNumber);

                        //Add input parameter for (ClaimNumber) and set its properties
                        SqlParameter paramClaimNumber = new SqlParameter();
                        paramClaimNumber.ParameterName = "@varClaimNo";
                        paramClaimNumber.SqlDbType = SqlDbType.VarChar;
                        paramClaimNumber.Direction = ParameterDirection.Input;
                        paramClaimNumber.Value = objDocumentGenerationRequest.ClaimNumber;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramClaimNumber);

                        //Add input parameter for (VIN) and set its properties
                        SqlParameter paramVIN = new SqlParameter();
                        paramVIN.ParameterName = "@varVIN";
                        paramVIN.SqlDbType = SqlDbType.VarChar;
                        paramVIN.Direction = ParameterDirection.Input;
                        paramVIN.Value = listCars[intCarIndex].VIN;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramVIN);

                        //Add input parameter for (CompanyNumber) and set its properties
                        SqlParameter paramCompanyNumber = new SqlParameter();
                        paramCompanyNumber.ParameterName = "@varCompanyNumber";
                        paramCompanyNumber.SqlDbType = SqlDbType.VarChar;
                        paramCompanyNumber.Direction = ParameterDirection.Input;
                        paramCompanyNumber.Value = objDocumentGenerationRequest.CompanyNumber;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramCompanyNumber);

                        //Add input parameter for (Coverage) and set its properties
                        SqlParameter paramCoverage = new SqlParameter();
                        paramCoverage.ParameterName = "@varCoverage";
                        paramCoverage.SqlDbType = SqlDbType.VarChar;
                        paramCoverage.Direction = ParameterDirection.Input;
                        paramCoverage.Value = listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCodeNumber;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramCoverage);

                        //Add input parameter for (LimitCode) and set its properties
                        SqlParameter paramLimitCode = new SqlParameter();
                        paramLimitCode.ParameterName = "@intLimitCode";
                        paramLimitCode.SqlDbType = SqlDbType.Int;
                        paramLimitCode.Direction = ParameterDirection.Input;
                        paramLimitCode.Value = listCars[intCarIndex].CarCoverages[intCoverageIndex].lp1e;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramLimitCode);

                        #endregion

                        //execute command
                        rdrReader = cmdCommand.ExecuteReader();

                        //check for results
                        if (rdrReader.HasRows == true)
                        {
                            //create car coverage limit list
                            List<ClaimsDocumentAllCoveragesCarCarCoverageLimit> listLimits = new List<ClaimsDocumentAllCoveragesCarCarCoverageLimit>();
                            List<ClaimsDocumentAllCoveragesCarCarCoverageDeductible> listDeductibles = new List<ClaimsDocumentAllCoveragesCarCarCoverageDeductible>();

                            while (rdrReader.Read())
                            {
                                //set flags
                                blnAddPerPersonLimit = false;
                                blnAddPerAccidentLimit = false;
                                blnAddDeductible = false;
                                listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageDesc = rdrReader["LimitLongDesc"].ToString();

                                #region Per Person Limit Processing

                                //process limit based on coverage code
                                switch (listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCode.Trim().ToUpper())
                                {
                                    case "BI":
                                    case "UM":
                                    case "UMBI":
                                    case "UMEL":
                                    case "UM-STACKED":
                                    case "UM-UNSTACKED":
                                    case "UIM":
                                    case "UIMBI":
                                    case "UIM-STACKED":
                                    case "UIM-UNSTACKED":
                                    case "TL":
                                    case "TOW":
                                    case "RREIM":
                                    case "MEDPM":
                                    case "RENT":
                                        blnAddPerPersonLimit = true;
                                        break;

                                    default:
                                        //do nothing
                                        blnAddPerPersonLimit = false;
                                        break;

                                }//end : switch (listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCodeNumber)

                                //determine if the limit should be added
                                //to the list
                                if (blnAddPerPersonLimit == true)
                                {
                                    //gather limit details
                                    ClaimsDocumentAllCoveragesCarCarCoverageLimit objPerPersonLimit = new ClaimsDocumentAllCoveragesCarCarCoverageLimit();

                                    //check for the need to perform special processing
                                    switch (listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCodeNumber)
                                    {
                                        case "TL":
                                        case "TOW":
                                            objPerPersonLimit.FormatInteger = rdrReader["PerPersonLimit"].ToString();
                                            switch (objDocumentGenerationRequest.PolicyNumber.Substring(0, 3).ToUpper())
                                            {
                                                case "APA":
                                                    objPerPersonLimit.FormatInteger = Convert.ToInt32(int.Parse(objPerPersonLimit.FormatInteger) / intTowingDenominator).ToString();
                                                    break;

                                                default:
                                                    objPerPersonLimit.FormatInteger = Convert.ToInt32(int.Parse(objPerPersonLimit.FormatInteger) / intTowingDenominator).ToString();
                                                    break;
                                            }
                                            break;

                                        case "RREIM":
                                        case "RENT":
                                            objPerPersonLimit.FormatInteger = rdrReader["PerPersonLimit"].ToString();
                                            objPerPersonLimit.FormatInteger = Convert.ToInt32(int.Parse(objPerPersonLimit.FormatInteger) / intRentalDenominator).ToString();
                                            break;

                                        default:
                                            objPerPersonLimit.FormatInteger = rdrReader["PerPersonLimit"].ToString();
                                            objPerPersonLimit.LimitAppliesToCd = "PerPersonLimit";
                                            break;
                                    }

                                    //add limit to list
                                    listLimits.Add(objPerPersonLimit);
                                }

                                #endregion

                                #region Per Accident Limit Processing

                                //process limit based on coverage code
                                switch (listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCode.Trim().ToUpper())
                                {
                                    case "BI":
                                    case "PD":
                                    case "UM":
                                    case "UMBI":
                                    case "UMEL":
                                    case "UM-STACKED":
                                    case "UM-UNSTACKED":
                                    case "UMPD":
                                    case "UIM":
                                    case "UIMBI":
                                    case "UIM-STACKED":
                                    case "UIM-UNSTACKED":
                                    case "PIP":
                                    //case "MEDPM":
                                    case "COMP":
                                    case "COLL":
                                    case "FG":
                                    case "TL":
                                    case "TOW":
                                    case "RREIM":
                                    case "RENT":
                                    case "ADDA":
                                    case "ADDAB":
                                    case "ACCDEATH":
                                        blnAddPerAccidentLimit = true;
                                        break;

                                    default:
                                        //do nothing
                                        blnAddPerAccidentLimit = false;
                                        break;

                                }//end : switch (listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCodeNumber)

                                //determine if the limit should be added
                                //to the list
                                if (blnAddPerAccidentLimit == true)
                                {
                                    //gather limit details
                                    ClaimsDocumentAllCoveragesCarCarCoverageLimit objPerAccidentLimit = new ClaimsDocumentAllCoveragesCarCarCoverageLimit();

                                    //check for the need to perform special processing
                                    switch (listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCodeNumber)
                                    {
                                        case "TL":
                                        case "TOW":
                                            //objPerAccidentLimit.FormatInteger = rdrReader["PerPDAccident"].ToString();
                                            objPerAccidentLimit.FormatInteger = rdrReader["PerAccidentLimit"].ToString();
                                            switch (objDocumentGenerationRequest.PolicyNumber.Substring(0, 3).ToUpper())
                                            {
                                                case "APA":
                                                    objPerAccidentLimit.FormatInteger = Convert.ToInt32(int.Parse(objPerAccidentLimit.FormatInteger) / intTowingDenominator).ToString();
                                                    break;

                                                default:
                                                    objPerAccidentLimit.FormatInteger = Convert.ToInt32(int.Parse(objPerAccidentLimit.FormatInteger) / intTowingDenominator).ToString();
                                                    break;
                                            }
                                            break;

                                        case "RREIM":
                                        case "RENT":
                                            //objPerAccidentLimit.FormatInteger = rdrReader["PerPDAccident"].ToString();
                                            objPerAccidentLimit.FormatInteger = rdrReader["PerAccidentLimit"].ToString();
                                            objPerAccidentLimit.FormatInteger = Convert.ToInt32(int.Parse(objPerAccidentLimit.FormatInteger) / intRentalDenominator).ToString();
                                            break;

                                        default:
                                            //objPerAccidentLimit.FormatInteger = rdrReader["PerPDAccident"].ToString();
                                            objPerAccidentLimit.FormatInteger = rdrReader["PerAccidentLimit"].ToString();
                                            //objPerAccidentLimit.LimitAppliesToCd = "PerPDAccident";
                                            objPerAccidentLimit.LimitAppliesToCd = "PerAccidentLimit";
                                            break;
                                    }

                                    //add limit to list
                                    listLimits.Add(objPerAccidentLimit);
                                }

                                #endregion

                                #region Deductible Processing

                                //deductible processing
                                switch (listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCodeNumber)
                                {
                                    case "UMPD":
                                    case "UIMPD":
                                    case "PIP":
                                    case "COMP":
                                    case "COLL":
                                    case "FG":
                                        blnAddDeductible = true;
                                        break;

                                    default:

                                        break;

                                }//end : switch (listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCodeNumber)

                                //determine if the deductible should be added to the list
                                if (blnAddDeductible == true)
                                {
                                    decDeductibleAmount = 0;
                                    dicPolicyCandL.TryGetValue(intCarIndex.ToString() + listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCode, out decDeductibleAmount);
                                    //add deductibel : only if the value is not 0.00
                                    if (decDeductibleAmount.ToString().Equals("0") == false)
                                    {
                                        ClaimsDocumentAllCoveragesCarCarCoverageDeductible objDeductible = new ClaimsDocumentAllCoveragesCarCarCoverageDeductible();
                                        objDeductible.FormatInteger = Convert.ToInt32(decDeductibleAmount).ToString();
                                        objDeductible.DeductibleAppliesToCd = listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCode;
                                        objDeductible.DeductibleTypeCD = listCars[intCarIndex].CarCoverages[intCoverageIndex].CoverageCode;
                                        listDeductibles.Add(objDeductible);
                                    }
                                }

                                #endregion
                            }

                            //add limits to coverage
                            listCars[intCarIndex].CarCoverages[intCoverageIndex].Limit = listLimits.ToArray();
                            //add deductibles to coverage
                            listCars[intCarIndex].CarCoverages[intCoverageIndex].Deductible = listDeductibles.ToArray();
                        }

                        //close reader
                        rdrReader.Close();
                        //clear parameters
                        cmdCommand.Parameters.Clear();
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                //handle error
                listCars = null;
                objDocGenerationResponse.GeneralResponseCode = (int)GenerateMessage.ExceptionOccured;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : VehicleGetCoverageList() ";
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
            }

            //return results
            return (listCars);

        }//end : VehicleGetCoverageLimits

        //define method : VehicleGetCoverageList
        public List<ClaimsDocumentAllCoveragesCar> VehicleGetCoverageList(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            //List<ClaimsDocumentAllCoveragesCarCarCoverage> listCoverages = new List<ClaimsDocumentAllCoveragesCarCarCoverage>();
            List<ClaimsDocumentAllCoveragesCar> listCars = new List<ClaimsDocumentAllCoveragesCar>();
            Dictionary<string, decimal> dicCarDeductibles = new Dictionary<string, decimal>();

            try
            {
                //get a list of cars
                listCars = ClaimGetVehicleList(objDocumentGenerationRequest, strClaimsDBConnectionString);

                //check for cars
                if (listCars != null)
                {
                    //get policy deductibles
                    dicCarDeductibles = LoadPolicyDeclarationCoveragesDeductibleInfoDB(objDocumentGenerationRequest.PolicyNumber, CDSupport.GenesisDBString);

                    //get a list of filtered coverages
                    listCars = VehicleGetFilteredCoverageList(objDocumentGenerationRequest, listCars, strClaimsDBConnectionString);
                    //check for coverages
                    if (listCars != null)
                    {
                        listCars = VehicleGetCoverageLimits(objDocumentGenerationRequest, listCars, strClaimsDBConnectionString);
                    }
                }//end : if (listCars == null)
            }
            catch (Exception ex)
            {
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : VehicleGetCoverageList() ";
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

            }

            //return coverage cars
            return (listCars);

        }//end : VehicleGetCoverageList



        #region Source Methods
        //define method : VehicleCoverageGet
        public List<ClaimsDocumentAllCoveragesCarCarCoverage> VehicleCoverageGet(DocGenerationRequest objDocumentGenerationRequest, List<ClaimsDocumentAllCoveragesCar> listCars, string strClaimsDBConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            List<ClaimsDocumentAllCoveragesCarCarCoverage> listCarCoverages = new List<ClaimsDocumentAllCoveragesCarCarCoverage>();
            int intCarIndex = 0;
            bool blnAddCoverage = false;
            bool blnEliminateCoverageCode = false;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_VehicleCoverages";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                for (intCarIndex = 0; intCarIndex < listCars.Count; intCarIndex++)
                {
                    #region Get Vehicle Coverage Parameter Setup

                    //add command parameters		
                    //Add input parameter for (@intVehicleInfoRequestTypeID) and set its properties
                    SqlParameter paramVehicleInfoRequestTypeID = new SqlParameter();
                    paramVehicleInfoRequestTypeID.ParameterName = "@intVehicleInfoRequestTypeID";
                    paramVehicleInfoRequestTypeID.SqlDbType = SqlDbType.Int;
                    paramVehicleInfoRequestTypeID.Direction = ParameterDirection.Input;
                    paramVehicleInfoRequestTypeID.Value = 2; //1 = Get Company, Coverage and Limit Code associated
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramVehicleInfoRequestTypeID);

                    //Add input parameter for (PolicyNumber) and set its properties
                    SqlParameter paramPolicyNumber = new SqlParameter();
                    paramPolicyNumber.ParameterName = "@varPolicyNo";
                    paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                    paramPolicyNumber.Direction = ParameterDirection.Input;
                    paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramPolicyNumber);

                    //Add input parameter for (ClaimNumber) and set its properties
                    SqlParameter paramClaimNumber = new SqlParameter();
                    paramClaimNumber.ParameterName = "@varClaimNo";
                    paramClaimNumber.SqlDbType = SqlDbType.VarChar;
                    paramClaimNumber.Direction = ParameterDirection.Input;
                    paramClaimNumber.Value = objDocumentGenerationRequest.ClaimNumber;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramClaimNumber);

                    //Add input parameter for (VIN) and set its properties
                    SqlParameter paramVIN = new SqlParameter();
                    paramVIN.ParameterName = "@varVIN";
                    paramVIN.SqlDbType = SqlDbType.VarChar;
                    paramVIN.Direction = ParameterDirection.Input;
                    paramVIN.Value = listCars[intCarIndex].VIN;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramVIN);

                    //Add input parameter for (CompanyNumber) and set its properties
                    SqlParameter paramCompanyNumber = new SqlParameter();
                    paramCompanyNumber.ParameterName = "@varCompanyNumber";
                    paramCompanyNumber.SqlDbType = SqlDbType.VarChar;
                    paramCompanyNumber.Direction = ParameterDirection.Input;
                    paramCompanyNumber.Value = objDocumentGenerationRequest.CompanyNumber;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramCompanyNumber);

                    //Add input parameter for (Coverage) and set its properties
                    SqlParameter paramCoverage = new SqlParameter();
                    paramCoverage.ParameterName = "@varCoverage";
                    paramCoverage.SqlDbType = SqlDbType.VarChar;
                    paramCoverage.Direction = ParameterDirection.Input;
                    paramCoverage.Value = "";
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramCoverage);

                    //Add input parameter for (LimitCode) and set its properties
                    SqlParameter paramLimitCode = new SqlParameter();
                    paramLimitCode.ParameterName = "@intLimitCode";
                    paramLimitCode.SqlDbType = SqlDbType.Int;
                    paramLimitCode.Direction = ParameterDirection.Input;
                    paramLimitCode.Value = 0;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramLimitCode);

                    #endregion

                    //open connection
                    cnnConnection.Open();
                    //execute command
                    rdrReader = cmdCommand.ExecuteReader();

                    //check for coverages for this car
                    if (rdrReader.HasRows == true)
                    {
                        while (rdrReader.Read())
                        {
                            //create car coverage object
                            ClaimsDocumentAllCoveragesCarCarCoverage objCarCoverage = new ClaimsDocumentAllCoveragesCarCarCoverage();

                            //get car coverage information
                            objCarCoverage.CarNumber = rdrReader["carno"].ToString();
                            objCarCoverage.CompanyNumber = rdrReader["Company"].ToString();
                            objCarCoverage.CoverageCodeNumber = rdrReader["Coverage"].ToString();
                            objCarCoverage.CoverageCode = CoverageCodeFromNumberToString(objCarCoverage.CompanyNumber, objCarCoverage.CoverageCodeNumber);
                            objCarCoverage.lp1e = rdrReader["lp_1e"].ToString();
                            objCarCoverage.Amount = rdrReader["original"].ToString();

                            //only add applicable coverages to list
                            switch (objCarCoverage.CoverageCodeNumber)
                            {
                                case "100": //Special Equipment

                                    break;



                                default:
                                    blnAddCoverage = true;
                                    break;
                            }//end : switch (objCarCoverage.CoverageCodeNumber)


                            //add car coverage information to list
                            listCarCoverages.Add(objCarCoverage);
                        }
                    }

                }//end : for(intCarIndex=0;intCarIndex<listCars.Count;intCarIndex++)
            }
            catch (Exception ex)
            {
                //handle error
                listCars = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : VehicleCoverageGet() ";
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

            }

            //return result
            return (listCarCoverages);
        }//end : VehicleCoverageGet

        //define method : VehicleCoverageInformationGet
        public List<ClaimsDocumentAllCoveragesCar> VehicleCoverageInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            ClaimsDocumentAllCoveragesCarCarCoverage objCarCoverage = null;
            List<ClaimsDocumentAllCoveragesCar> listCars = new List<ClaimsDocumentAllCoveragesCar>();
            ClaimsDocumentAllCoveragesCarCarCoverage objClaimsDocumentCarCoverage = null;
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intCarIndex = 0;
            int intCoverageIndex = 0;
            int intIndex = 0;
            string strCoverageCode = "";
            string strVINs = "";

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_XMLGeneration_VehicleCoverages";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                #region Get Car(s) Parameter Setup

                //add command parameters		
                //Add input parameter for (@intVehicleInfoRequestTypeID) and set its properties
                SqlParameter paramVehicleInfoRequestTypeID = new SqlParameter();
                paramVehicleInfoRequestTypeID.ParameterName = "@intVehicleInfoRequestTypeID";
                paramVehicleInfoRequestTypeID.SqlDbType = SqlDbType.Int;
                paramVehicleInfoRequestTypeID.Direction = ParameterDirection.Input;
                paramVehicleInfoRequestTypeID.Value = 1; //1 = Get the VIN of vehicles associated with claim
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramVehicleInfoRequestTypeID);

                //Add input parameter for (PolicyNumber) and set its properties
                SqlParameter paramPolicyNumber = new SqlParameter();
                paramPolicyNumber.ParameterName = "@varPolicyNo";
                paramPolicyNumber.SqlDbType = SqlDbType.VarChar;
                paramPolicyNumber.Direction = ParameterDirection.Input;
                paramPolicyNumber.Value = objDocumentGenerationRequest.PolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNumber);

                //Add input parameter for (ClaimNumber) and set its properties
                SqlParameter paramClaimNumber = new SqlParameter();
                paramClaimNumber.ParameterName = "@varClaimNo";
                paramClaimNumber.SqlDbType = SqlDbType.VarChar;
                paramClaimNumber.Direction = ParameterDirection.Input;
                paramClaimNumber.Value = objDocumentGenerationRequest.ClaimNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimNumber);

                //Add input parameter for (VIN) and set its properties
                SqlParameter paramVIN = new SqlParameter();
                paramVIN.ParameterName = "@varVIN";
                paramVIN.SqlDbType = SqlDbType.VarChar;
                paramVIN.Direction = ParameterDirection.Input;
                paramVIN.Value = "";
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramVIN);

                //Add input parameter for (CompanyNumber) and set its properties
                SqlParameter paramCompanyNumber = new SqlParameter();
                paramCompanyNumber.ParameterName = "@varCompanyNumber";
                paramCompanyNumber.SqlDbType = SqlDbType.VarChar;
                paramCompanyNumber.Direction = ParameterDirection.Input;
                paramCompanyNumber.Value = objDocumentGenerationRequest.CompanyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCompanyNumber);

                //Add input parameter for (Coverage) and set its properties
                SqlParameter paramCoverage = new SqlParameter();
                paramCoverage.ParameterName = "@varCoverage";
                paramCoverage.SqlDbType = SqlDbType.VarChar;
                paramCoverage.Direction = ParameterDirection.Input;
                paramCoverage.Value = "";
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCoverage);

                //Add input parameter for (LimitCode) and set its properties
                SqlParameter paramLimitCode = new SqlParameter();
                paramLimitCode.ParameterName = "@intLimitCode";
                paramLimitCode.SqlDbType = SqlDbType.Int;
                paramLimitCode.Direction = ParameterDirection.Input;
                paramLimitCode.Value = 0;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramLimitCode);

                #endregion

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //fill vehicle object
                    while (rdrReader.Read())
                    {
                        if (string.IsNullOrEmpty(rdrReader["VIN"].ToString()) == false)
                        {
                            //make sure the VIN does not already exist in the list
                            for (intIndex = 0; intIndex < listCars.Count; intIndex++)
                            {
                                //make sure we are not adding a duplicate VIN
                                //this code is here to accomodate duplicat VIN results
                                //generated from the stored procedure results
                                if (listCars[intIndex].VIN.ToUpper().Equals(rdrReader["VIN"].ToString().ToUpper()) == false)
                                {
                                    //create car object
                                    ClaimsDocumentAllCoveragesCar objCar = new ClaimsDocumentAllCoveragesCar();
                                    objCar.VIN = rdrReader["vin"].ToString();
                                    //add to list
                                    listCars.Add(objCar);
                                }
                            }
                        }
                    }
                }

                //close the reader
                rdrReader.Close();

                //check for cars
                if (listCars.Count != 0)
                {
                    for (intCarIndex = 0; intCarIndex < listCars.Count; intCarIndex++)
                    {
                        //create a new car coverage list
                        List<ClaimsDocumentAllCoveragesCarCarCoverage> listClaimsDocumentCarCoverage = new List<ClaimsDocumentAllCoveragesCarCarCoverage>();

                        //remove VIN parameter from command list
                        cmdCommand.Parameters.Remove(paramVIN);
                        //update VIN paramter
                        paramVIN.Value = listCars[intCarIndex].VIN.ToString();
                        //add VIN paramter back to list
                        cmdCommand.Parameters.Add(paramVIN);

                        //remove VehicleInfoRequestTypeID parameter from command list
                        cmdCommand.Parameters.Remove(paramVehicleInfoRequestTypeID);
                        //update paramVehicleInfoRequestTypeID
                        paramVehicleInfoRequestTypeID.Value = 2; //2 = Get Company, Coverage and Limit Code associated
                        //add paramVehicleInfoRequestTypeID back to list
                        cmdCommand.Parameters.Add(paramVehicleInfoRequestTypeID);

                        //execute command
                        rdrReader = cmdCommand.ExecuteReader();

                        //check for results
                        if (rdrReader.HasRows == true)
                        {
                            while (rdrReader.Read())
                            {
                                //Eliminate coverages here



                                //create car coverage object
                                objCarCoverage = new ClaimsDocumentAllCoveragesCarCarCoverage();

                                //get car coverage information
                                objCarCoverage.CarNumber = rdrReader["carno"].ToString();
                                objCarCoverage.CompanyNumber = rdrReader["Company"].ToString();
                                //strCoverageCode = rdrReader["Coverage"].ToString();
                                objCarCoverage.CoverageCodeNumber = rdrReader["Coverage"].ToString();
                                objCarCoverage.CoverageCode = CoverageCodeFromNumberToString(objCarCoverage.CompanyNumber, objCarCoverage.CoverageCodeNumber);
                                objCarCoverage.lp1e = rdrReader["lp_1e"].ToString();
                                objCarCoverage.Amount = rdrReader["original"].ToString();

                                //add car coverage information to list
                                listClaimsDocumentCarCoverage.Add(objCarCoverage);
                            }
                        }

                        //close reader
                        rdrReader.Close();

                        //get vehicle coverage and limit information
                        for (intCoverageIndex = 0; intCoverageIndex < listClaimsDocumentCarCoverage.Count; intCoverageIndex++)
                        {
                            //remove VehicleInfoRequestTypeID parameter from command list
                            cmdCommand.Parameters.Remove(paramVehicleInfoRequestTypeID);
                            //update paramVehicleInfoRequestTypeID
                            paramVehicleInfoRequestTypeID.Value = 3; //3 = Get detailed vechicle coverage information
                            //add paramVehicleInfoRequestTypeID back to list
                            cmdCommand.Parameters.Add(paramVehicleInfoRequestTypeID);

                            //remove CompanyNumber parameter from command list
                            cmdCommand.Parameters.Remove(paramCompanyNumber);
                            //update paramCompanyNumber
                            paramCompanyNumber.Value = listClaimsDocumentCarCoverage[intCoverageIndex].CompanyNumber.ToString();
                            //add paramCompanyNumber back to list
                            cmdCommand.Parameters.Add(paramCompanyNumber);

                            //remove paramCoverage parameter from command list
                            cmdCommand.Parameters.Remove(paramCoverage);
                            //update paramCoverage
                            paramCoverage.Value = listClaimsDocumentCarCoverage[intCoverageIndex].CoverageCodeNumber.ToString();
                            //add paramCoverage back to list
                            cmdCommand.Parameters.Add(paramCoverage);

                            //remove paramLimitCode parameter from command list
                            cmdCommand.Parameters.Remove(paramLimitCode);
                            //update paramLimitCode
                            paramLimitCode.Value = int.Parse(listClaimsDocumentCarCoverage[intCoverageIndex].lp1e.ToString());
                            //add paramLimitCode back to list
                            cmdCommand.Parameters.Add(paramLimitCode);

                            //execute command
                            rdrReader = cmdCommand.ExecuteReader();

                            //check for results
                            if (rdrReader.HasRows == true)
                            {
                                //create car coverage limit list
                                List<ClaimsDocumentAllCoveragesCarCarCoverageLimit> listLimits = new List<ClaimsDocumentAllCoveragesCarCarCoverageLimit>();

                                while (rdrReader.Read())
                                {
                                    listClaimsDocumentCarCoverage[intCoverageIndex].CoverageDesc = rdrReader["LimitLongDesc"].ToString();
                                    //gather limit details
                                    ClaimsDocumentAllCoveragesCarCarCoverageLimit objPerPersonLimit = new ClaimsDocumentAllCoveragesCarCarCoverageLimit();
                                    objPerPersonLimit.FormatInteger = rdrReader["PerPersonLimit"].ToString();
                                    objPerPersonLimit.LimitAppliesToCd = "PerPersonLimit";
                                    listLimits.Add(objPerPersonLimit);

                                    ClaimsDocumentAllCoveragesCarCarCoverageLimit objPerAccidentLimit = new ClaimsDocumentAllCoveragesCarCarCoverageLimit();
                                    objPerAccidentLimit.FormatInteger = rdrReader["PerAccidentLimit"].ToString();
                                    objPerAccidentLimit.LimitAppliesToCd = "PerAccidentLimit";
                                    listLimits.Add(objPerAccidentLimit);

                                    ClaimsDocumentAllCoveragesCarCarCoverageLimit objPerPDAccidentLimit = new ClaimsDocumentAllCoveragesCarCarCoverageLimit();
                                    objPerPDAccidentLimit.FormatInteger = rdrReader["PerPDAccident"].ToString();
                                    objPerPDAccidentLimit.LimitAppliesToCd = "PerPDAccidentLimit";
                                    listLimits.Add(objPerPDAccidentLimit);
                                }

                                //add limits to coverage
                                listClaimsDocumentCarCoverage[intCoverageIndex].Limit = listLimits.ToArray();
                            }

                            //close reader
                            rdrReader.Close();
                            listCars[intCarIndex].CarCoverages = listClaimsDocumentCarCoverage.ToArray();
                        }
                    }//end for (intIndex = 0; intIndex < listClaimsDocumentCarCoverage.Count; intIndex++)
                }
            }
            catch (Exception ex)
            {
                //handle error
                objClaimsDocumentCarCoverage = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : VehicleCoverageInformationGet() ";
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

            }

            //return results
            return (listCars);
        }//end : VehicleCoverageInformationGet

        #endregion

        ////define method : MetaDataHeaderInformationGet
        //public ClaimsDocumentInputMetaData MetaDataHeaderInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString)
        //{
        //    //declare variables
        //    ClaimsDocumentInputDocumentWhitehillAttorney objAttorney = null;
        //    //declare variables
        //    SqlConnection cnnConnection = null;
        //    SqlCommand cmdCommand = new SqlCommand();
        //     null;

        //    try
        //    {
        //        //setup connection
        //        cnnConnection = new SqlConnection(strClaimsDBConnectionString);
        //        //Create the command and set its properties
        //        cmdCommand = new SqlCommand();
        //        cmdCommand.Connection = cnnConnection;
        //        cmdCommand.CommandText = "dbo.WhiteHill_XMLGeneration_Attorney";
        //        cmdCommand.CommandType = CommandType.StoredProcedure;

        //        //add command parameters		
        //        //Add input parameter for (ClaimUKey) and set its properties
        //        SqlParameter paramClaimUKey = new SqlParameter();
        //        paramClaimUKey.ParameterName = "@ClaimUKey";
        //        paramClaimUKey.SqlDbType = SqlDbType.Int;
        //        paramClaimUKey.Direction = ParameterDirection.Input;
        //        paramClaimUKey.Value = objDocumentGenerationRequest.ContactNumber;
        //        //Add the parameter to the commands parameter collection
        //        cmdCommand.Parameters.Add(paramClaimUKey);

        //        //Add input parameter for (ContactType) and set its properties
        //        SqlParameter paramContactType = new SqlParameter();
        //        paramContactType.ParameterName = "@ContactType";
        //        paramContactType.SqlDbType = SqlDbType.Int;
        //        paramContactType.Direction = ParameterDirection.Input;
        //        paramContactType.Value = 0;
        //        //Add the parameter to the commands parameter collection
        //        cmdCommand.Parameters.Add(paramContactType);

        //        //open connection
        //        cnnConnection.Open();
        //        //execute command
        //        rdrReader = cmdCommand.ExecuteReader();

        //        //check for results
        //        if (rdrReader.HasRows == true)
        //        {
        //            //create Attorney object
        //            objAttorney = new ClaimsDocumentInputDocumentWhitehillAttorney();
        //            //fill coverage object
        //            while (rdrReader.Read())
        //            {
        //                objAttorney.AttorneyFirstName = rdrReader["AttorneyFirstName"].ToString();
        //                objAttorney.AttorneyMiddleInitial = rdrReader["AttorneyMiddleInitial"].ToString();
        //                objAttorney.AttorneyLastName = rdrReader["AttorneyLastName"].ToString();
        //                objAttorney.AttorneyAddressLine1 = rdrReader["AttorneyAddressLine1"].ToString();
        //                objAttorney.AttorneyAddressLine2 = rdrReader["AttorneyAddressLine2"].ToString();
        //                objAttorney.AttorneyAddressLine3 = rdrReader["AttorneyAddressLine3"].ToString();
        //                objAttorney.AttorneyCity = rdrReader["AttorneyCity"].ToString();
        //                objAttorney.AttorneyState = rdrReader["AttorneyState"].ToString();
        //                objAttorney.AttorneyZip = rdrReader["AttorneyZip"].ToString();
        //                objAttorney.AttorneyZip4 = rdrReader["AttorneyZip4"].ToString();
        //                objAttorney.AttorneyPhoneNumber = rdrReader["AttorneyPhoneNumber"].ToString();
        //                objAttorney.AttorneyFaxNumber = rdrReader["AttorneyFaxNumber"].ToString();
        //                objAttorney.AttorneyEmailAddress = rdrReader["AttorneyEmailAddress"].ToString();
        //                //objAttorney.MailPostNetZip = rdrReader["MailPostNetZip"].ToString();
        //                objAttorney.MatchLevel = rdrReader["MatchLevel"].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle error
        //        objAttorney = null;
        //        ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
        //        CDSupport objCDSupport = new CDSupport();
        //        //fill log
        //        objClaimsLog.ClaimsDocsLogID = 0;
        //        objClaimsLog.LogTypeID = 3;
        //        objClaimsLog.LogSourceTypeID = 1;
        //        objClaimsLog.MessageIs = "Method : AttorneyInformationGet() ";
        //        objClaimsLog.ExceptionIs = ex.Message;
        //        objClaimsLog.StackTraceIs = ex.StackTrace;
        //        objClaimsLog.IUDateTime = DateTime.Now;
        //        //create log record
        //        objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

        //        //cleanup
        //        objClaimsLog = null;
        //        objCDSupport = null;
        //    }
        //    finally
        //    {
        //close reader
        //if (rdrReader != null)
        //{
        //    if (!rdrReader.IsClosed)
        //    {
        //        rdrReader.Close();
        //    }
        //    rdrReader = null;
        //}

        ////cleanup connection
        //if (cnnConnection.State == ConnectionState.Open)
        //{
        //    cnnConnection.Close();
        //}

        //    }

        //    //return results
        //    return (objAttorney);

        //}//end : AttorneyInformationGet


        #endregion

        #region XML Construction Methods

        //define method : ClaimsDocsGenerateDocument
        public DocGenerationResponse ClaimsDocsGenerateDocument(List<DocumentDisplayField> listDisplayFields, DocGenerationRequest objDocGenerationRequest)
        {
            //declare variables
            bool blnContinue = true;
            CDUsers objUserClient = new CDUsers();
            ImageRightClaimRequest objIRClaimsRequest = new ImageRightClaimRequest();
            ImageRightClaimRequest objIRClaimsRequestSaveResult = new ImageRightClaimRequest();
            string strXMLDocumentOutputPathAndFileName = "";
            string strPDFDocumentOutputPathAndFileName = "";
            List<DocuMakerClaimsDocsResponse> listDocuMakerClaimsDocsResponse = null;
            User objUser = new User();
            VehicleData objVehicleData = null;
            int intGenerateMessageID = (int)GenerateMessage.UnknownStatus;
            string strGenerateMessage = "";
            int intCopies = 0;
            string strClaimsDocsXMLFilePathAndName = "";
            ClaimsDocument objClaimsDocument = null;
            CDDocument objDocument = new CDDocument();
            int intContactNumber = 0;
            int intContactType = 0;
            FileInfo documentFile;

            try
            {
                //set header information
                this.objCDXML.InstanceID = objDocGenerationRequest.InstanceID;
                this.objCDXML.DateFormatMMddyyy = DateTime.Now.ToString("d");
                this.objCDXML.DateFormatMMMMdyyyy = DateTime.Now.ToString("D");

                //should display fields be generated from an existing xml file?
                //note: this is a requirement when there is a need to regenerate
                //xml from a saved document that requires management approval
                if (objDocGenerationRequest.DisplayFieldsGetFromXML == true)
                {
                    CDDocumentField objDocumentField = new CDDocumentField();
                    //clear existin display fields list
                    listDisplayFields.Clear();
                    //build display field source xml file name from instance id
                    strClaimsDocsXMLFilePathAndName = CDSupport.XMLOutputLocation + this.objCDXML.InstanceID + ".xml";

                    objClaimsDocument = objDocument.DocumentGetFromXML(strClaimsDocsXMLFilePathAndName);

                    //get display fields
                    listDisplayFields = objDocumentField.DisplayFieldsGetFromXMLFile(objClaimsDocument);

                    //get meta data from xml
                    objDocument.DocumentMetaDataGetFromXML(objClaimsDocument, out intContactNumber, out intContactType);
                    objDocGenerationRequest.ContactNumber = intContactNumber;
                    objDocGenerationRequest.ContactType = intContactType;
                }

                //get user details
                objUser.UserName = objDocGenerationRequest.UserName;
                objUser = objUserClient.UserReadByUserName(objUser, CDSupport.ClaimsDocsDBConnectionString);
                objDocGenerationRequest.UserID = objUser.UserID.ToString();

                //get vehicle data if the contact type = 6
                if ((objDocGenerationRequest.ContactType == 6) || (objDocGenerationRequest.ContactType == 8))
                {
                    //get vehicle data
                    objVehicleData = VehicleDataInformationGet(objDocGenerationRequest, CDSupport.ClaimsDBConnectionString);

                    //check for vehicle data
                    if (objVehicleData == null)
                    {
                        //intGenerateMessageID = (int)GenerateMessage.UnableToGenerateVehicleData;
                        //blnContinue = false;
                    }
                    else
                    {
                        //apply vehicle data to request object
                        objDocGenerationRequest.VehicleUKey = objVehicleData.VehicleUKey;
                        objDocGenerationRequest.CarNumber = objVehicleData.CarNumber;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //build company information section of document
                    blnContinue = BuildCompanyInfo(objDocGenerationRequest, CDSupport.GenesisDBString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToGenerateCompanyInformation;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //build document definition section of document
                    blnContinue = BuildDocumentDefinition(objDocGenerationRequest, CDSupport.ClaimsDocsDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildDocumentDefinition;
                    }

                    //check for insured contact type = 6
                    if ((objDocGenerationRequest.ContactType == 6) || (objDocGenerationRequest.ContactType == 8))
                    {
                        //determine if this document type requires coverage information
                        switch (this.objCDXML.DocumentDefinition.DocumentDefinitionName.ToUpper())
                        {
                            case "EOBVI01":
                            case "EOBNVIP01":
                            case "EOBNVI01":
                                //build coverages
                                BuildVehicleCoverages(objDocGenerationRequest, CDSupport.ClaimsDBConnectionString);
                                break;

                            default:
                                //do not build coverages
                                break;

                        }//end : switch (this.objCDXML.DocumentDefinition.DocumentDefinitionName.ToUpper())
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //Build Sumitter
                    blnContinue = BuildSubmitter(objDocGenerationRequest, CDSupport.ClaimsDocsDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildSubmitter;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    blnContinue = BuildPolicy(objDocGenerationRequest, CDSupport.ClaimsDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildPolicy;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    blnContinue = BuildCoverages(objDocGenerationRequest, CDSupport.ClaimsDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildCoverages;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //build Addressee
                    blnContinue = BuildAddressee(objDocGenerationRequest, CDSupport.AMSDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildAddressee;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //build PremiumFinanceCo
                    blnContinue = BuildPremiumFinanceCo(objDocGenerationRequest, CDSupport.ClaimsDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildPremiumFinanceCo;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //build LienHolder
                    blnContinue = BuildLienHolder(objDocGenerationRequest, CDSupport.ClaimsDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildLienHolder;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //build NamedInsured
                    blnContinue = BuildNamedInsured(objDocGenerationRequest, CDSupport.AMSDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildNamedInsured;
                    }
                }

                //check for continuation
                if ((blnContinue == true) && ((objDocGenerationRequest.ContactType == 6) || (objDocGenerationRequest.ContactType == 8)))
                {
                    //build Vehicle
                    blnContinue = BuildVehicle(objDocGenerationRequest, CDSupport.ClaimsDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildVehicle;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //build adjuster
                    blnContinue = BuildAdjuster(objDocGenerationRequest, CDSupport.ClaimsDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildAdjuster;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //build producter
                    blnContinue = BuildProducer(objDocGenerationRequest, CDSupport.AMSDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildProducer;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //buildl loss description
                    blnContinue = BuildLossDescription(objDocGenerationRequest, CDSupport.ClaimsDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildLossDescription;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //buildl attorney
                    blnContinue = BuildAttorney(objDocGenerationRequest, CDSupport.AMSDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildAttorney;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //build Whitehill Addressee
                    blnContinue = BuildWhiteHillAddressee(objDocGenerationRequest, CDSupport.AMSDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildWhiteHillAddressee;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //build Input Meta Data
                    blnContinue = BuildMetaData(listDisplayFields, objDocGenerationRequest, CDSupport.ClaimsDBConnectionString);
                    if (blnContinue == false)
                    {
                        intGenerateMessageID = (int)GenerateMessage.UnableToBuildMetaData;
                    }
                }

                //check for continuation
                if (blnContinue == true)
                {
                    //fill default response values
                    objDocGenerationResponse.FileNumber = objDocGenerationRequest.PolicyNumber.Substring(0, 3) + objDocGenerationRequest.PolicyNumber.Substring(6, 6);
                    objDocGenerationResponse.ClaimsNumber = objDocGenerationRequest.ClaimNumber;
                    objDocGenerationResponse.ClaimsDocsDocID = this.objCDXML.DocumentDefinition.DocumentDefinitionName;
                    objDocGenerationResponse.GroupName = objDocGenerationRequest.GroupName;
                    objDocGenerationResponse.FolderName = this.objCDXML.Input.Document.Whitehill.NamedInsured.NamedInsuredLastName + ", " + this.objCDXML.Input.Document.Whitehill.NamedInsured.NamedInsuredFirstName;
                    objDocGenerationResponse.DateOfLoss = this.objCDXML.Input.Document.Whitehill.LossDescription.DayofLoss;

                    PackDocType objPackDocType = new PackDocType();
                    objPackDocType = PackDocTypeGet(objDocGenerationResponse.GroupName);
                    objDocGenerationResponse.PackType = objPackDocType.PackType.ToString();
                    objDocGenerationResponse.DocType = objPackDocType.DocType;

                    //check for XML Generation Request
                    if (objDocGenerationRequest.GenerateXML == true)
                    {
                        //XML Generation
                        strXMLDocumentOutputPathAndFileName = SerializeDocumentToXMLFile();
                        //check results
                        if (string.IsNullOrEmpty(strXMLDocumentOutputPathAndFileName) == false)
                        {
                            //fill XML document response
                            objDocGenerationResponse.XMLFilePathAndName = strXMLDocumentOutputPathAndFileName;
                            objDocGenerationResponse.XMLResponseCode = (int)GenerateMessage.XMLGenerated;
                            objDocGenerationResponse.XMLResponseMessage = "XML Successfully Generated";
                        }
                        else
                        {
                            //set failure message
                            intGenerateMessageID = (int)GenerateMessage.UnableToSerializeDocumentToXMLFile;
                            //fill XML document response
                            objDocGenerationResponse.XMLFilePathAndName = strXMLDocumentOutputPathAndFileName;
                            objDocGenerationResponse.XMLResponseCode = (int)GenerateMessage.UnableToSerializeDocumentToXMLFile;
                            objDocGenerationResponse.XMLResponseMessage = "XML Generation Failure";
                            blnContinue = false;
                        }

                        //PDF Generation
                        //Note : A PDF Can not be generated without a successful XML Generation
                        if ((blnContinue = true) && (objDocGenerationRequest.GenerateXML == true) && (objDocGenerationRequest.GeneratePDF == true))
                        {
                            string claimsNumber = objDocGenerationRequest.ClaimNumber ?? string.Empty;
                            string documentDefinitionName = objDocGenerationResponse.ClaimsDocsDocID;
                            //generate pdf document(s)
                            listDocuMakerClaimsDocsResponse = GenerateClaimsDocPDF(claimsNumber, documentDefinitionName);
                            //check results
                            if (listDocuMakerClaimsDocsResponse.Count > 0)
                            {
                                //save list of responses
                                objDocGenerationResponse.ListDocuMakerClaimsDocsResponse = listDocuMakerClaimsDocsResponse;

                                //fill PDF document response
                                objDocGenerationResponse.PDFFilePathAndName = listDocuMakerClaimsDocsResponse[0].DocumentPathAndFileName;
                                objDocGenerationResponse.PDFResponseCode = (int)GenerateMessage.PDFGenerated;
                                objDocGenerationResponse.PDFResponseMessage = "PDF Successfully Generated";

                                //Note : ImageRight storage is not possible without XML and PDF Generation
                                if ((blnContinue = true) && (objDocGenerationRequest.GenerateXML == true) && (objDocGenerationRequest.GeneratePDF == true) && (objDocGenerationRequest.StoreToImageRight == true))
                                {
                                    //start ImageRight Copy processing
                                    if (listDocuMakerClaimsDocsResponse.Count > 0)
                                    {
                                        //process each copy
                                        for (intCopies = 0; intCopies < listDocuMakerClaimsDocsResponse.Count; intCopies++)
                                        {
                                            documentFile = new FileInfo(listDocuMakerClaimsDocsResponse[intCopies].DocumentPathAndFileName);
                                            if (documentFile.Length != 0)
                                            {
                                                //reset image right request object
                                                objIRClaimsRequest = null;
                                                //fill image right storage request
                                                objIRClaimsRequest = ClaimsDocsImageRightRequestFill(listDocuMakerClaimsDocsResponse[intCopies].DocumentPathAndFileName, objDocGenerationRequest);
                                                //make sure request has been setup
                                                if (objIRClaimsRequest != null)
                                                {
                                                    //save the document to ImageRight
                                                    objIRClaimsRequest = ImageRightSaveDocument(objIRClaimsRequest);
                                                    //check request result
                                                    if (objIRClaimsRequest.RequestResult == true)
                                                    {
                                                        //get saved document ImageRight location
                                                        objIRClaimsRequest = ImageRightGetDocument(objIRClaimsRequest);
                                                        if (objIRClaimsRequest.RequestResult == true)
                                                        {
                                                            //fill PDF document response
                                                            objDocGenerationResponse.ImageRightFilePathAndName = objIRClaimsRequest.IRDocumentPath;
                                                            objDocGenerationResponse.ImageRightResponseCode = (int)GenerateMessage.StoredInImageRight;
                                                            objDocGenerationResponse.ImageRightResponseMessage = "File Successfully Stored in ImageRight";

                                                            //indicate overall success
                                                            objDocGenerationResponse.GeneralResponseCode = (int)GenerateMessage.Success;
                                                        }
                                                        else
                                                        {
                                                            //set failure message
                                                            intGenerateMessageID = (int)GenerateMessage.UnableToRetrieveFileFromImageRight;
                                                            //fill PDF document response
                                                            objDocGenerationResponse.ImageRightFilePathAndName = objIRClaimsRequest.IRDocumentPath;
                                                            objDocGenerationResponse.ImageRightResponseCode = (int)GenerateMessage.UnableToRetrieveFileFromImageRight;
                                                            objDocGenerationResponse.ImageRightResponseMessage = "Unable to retrieve document from ImageRight";
                                                            blnContinue = false;
                                                        }//end : if (objIRClaimsRequest.RequestResult == true)
                                                    }
                                                }
                                                else
                                                {
                                                    //set failure message
                                                    intGenerateMessageID = (int)GenerateMessage.UnableToStoreFileToImageRight;
                                                    //fill PDF document response
                                                    objDocGenerationResponse.ImageRightFilePathAndName = "";
                                                    objDocGenerationResponse.ImageRightResponseCode = (int)GenerateMessage.UnableToStoreFileToImageRight;
                                                    objDocGenerationResponse.ImageRightResponseMessage = "Unable to store document to ImageRight";
                                                    blnContinue = false;

                                                }//end : if (objIRClaimsRequest != null)
                                            }// end : if (listDocuMakerClaimsDocsResponse.Count > 0)
                                            else
                                            {
                                                //set failure message
                                                intGenerateMessageID = (int)GenerateMessage.UnableToStoreFileToImageRight;
                                                //fill PDF document response
                                                objDocGenerationResponse.ImageRightFilePathAndName = "";
                                                objDocGenerationResponse.ImageRightResponseCode = (int)GenerateMessage.UnableToStoreFileToImageRight;
                                                objDocGenerationResponse.ImageRightResponseMessage = "Unable to store document to ImageRight: 0 Byte";
                                                blnContinue = false;
                                            }
                                        }//end : for (intCopies = 0; intCopies < listDocuMakerClaimsDocsResponse.Count; intCopies++)
                                    }//end ImageRight Copy processing
                                }
                                else
                                {
                                    //indicate success with xml and pdf generation
                                    objDocGenerationResponse.GeneralResponseCode = (int)GenerateMessage.Success;
                                }
                            }
                            else
                            {
                                //set failure message
                                intGenerateMessageID = (int)GenerateMessage.UnableToGenerateClaimsDocPDF;
                                //fill PDF document response
                                objDocGenerationResponse.PDFFilePathAndName = strPDFDocumentOutputPathAndFileName;
                                objDocGenerationResponse.PDFResponseCode = (int)GenerateMessage.UnableToGenerateClaimsDocPDF;
                                objDocGenerationResponse.PDFResponseMessage = "PDF Generation Failure";
                                blnContinue = false;
                            }
                        }//end : if ((blnContinue=true) && (objDocGenerationRequest.GenerateXML == true) && (objDocGenerationRequest.GeneratePDF == true))
                        else
                        {
                            //indicate success with just xml generation
                            objDocGenerationResponse.GeneralResponseCode = (int)GenerateMessage.Success;
                        }
                    }

                }//end : if (blnContinue == true)
                else
                {
                    //handle non-xml and/or pdf error
                    objDocGenerationResponse.GeneralResponseCode = intGenerateMessageID;
                    switch (intGenerateMessageID)
                    {
                        case (int)GenerateMessage.UnknownStatus:
                            strGenerateMessage = "Unknown Status";
                            break;

                        case (int)GenerateMessage.UnableToGenerateVehicleData:
                            strGenerateMessage = "Unable to generate vehicle data";
                            break;

                        case (int)GenerateMessage.UnableToBuildDocumentDefinition:
                            strGenerateMessage = "Unable to Build Document Definition";
                            break;

                        case (int)GenerateMessage.UnableToBuildSubmitter:
                            strGenerateMessage = "Unable to Build Submitter";
                            break;

                        case (int)GenerateMessage.UnableToBuildPolicy:
                            strGenerateMessage = "Unable to Build Policy";
                            break;

                        case (int)GenerateMessage.UnableToBuildCoverages:
                            strGenerateMessage = "Unable to Build Coverages";
                            break;

                        case (int)GenerateMessage.UnableToBuildAddressee:
                            strGenerateMessage = "Unable to Build Addressee";
                            break;

                        case (int)GenerateMessage.UnableToBuildPremiumFinanceCo:
                            strGenerateMessage = "Unable to Build Premium Finance Co";
                            break;

                        case (int)GenerateMessage.UnableToBuildLienHolder:
                            strGenerateMessage = "Unable to Build LienHolder";
                            break;

                        case (int)GenerateMessage.UnableToBuildNamedInsured:
                            strGenerateMessage = "Unable to Build Named Insured";
                            break;

                        case (int)GenerateMessage.UnableToBuildVehicle:
                            strGenerateMessage = "Unable to Build Vehicle";
                            break;

                        case (int)GenerateMessage.UnableToBuildAdjuster:
                            strGenerateMessage = "Unable to Build Adjuster";
                            break;

                        case (int)GenerateMessage.UnableToBuildProducer:
                            strGenerateMessage = "Unable to Build Producer";
                            break;

                        case (int)GenerateMessage.UnableToBuildLossDescription:
                            strGenerateMessage = "Unable to Build Loss Description";
                            break;

                        case (int)GenerateMessage.UnableToBuildAttorney:
                            strGenerateMessage = "Unable to Build Attorney";
                            break;

                        case (int)GenerateMessage.UnableToBuildWhiteHillAddressee:
                            strGenerateMessage = "Unable to Build Claims Addressee";
                            break;

                        case (int)GenerateMessage.UnableToBuildMetaData:
                            strGenerateMessage = "Unable to Build Metadata";
                            break;

                        case (int)GenerateMessage.UnableToSerializeDocumentToXMLFile:
                            strGenerateMessage = "Unable to Serialize Document to XML File";
                            break;

                        case (int)GenerateMessage.UnableToGenerateClaimsDocPDF:
                            strGenerateMessage = "Unable to Generate ClaimsDoc PDF";
                            break;

                        default:
                            strGenerateMessage = "An unexpected error condition was encountered...";
                            break;
                    }//end : switch (intGenerateMessageID)

                    objDocGenerationResponse.GeneralResponseMessage = strGenerateMessage;
                }


            }
            catch (Exception ex)
            {
                //handle error
                objDocGenerationResponse.GeneralResponseCode = (int)GenerateMessage.ExceptionOccured;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : ClaimsDocsGenerateDocument() ";
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

            }

            //return results
            return (objDocGenerationResponse);

        }//end : ClaimsDocsGenerateDocument

        //define method : BuildDocumentDefinition
        public bool BuildDocumentDefinition(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentDocumentDefinition objDocumentDefinition = null;

            try
            {
                //get document definition information
                objDocumentDefinition = DocumentDefinitionGet(objDocGenerationRequest, strConnectionString);

                //check for results
                if (objDocumentDefinition == null)
                {
                    //handle no data return
                    blnResult = false;
                }
                else
                {
                    //assign document definition informatin
                    this.objCDXML.DocumentDefinition = objDocumentDefinition;
                }//end : if (objDocumentIs == null)
            }
            catch (Exception ex)
            {
                //handle error
                objDocumentDefinition = null;
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildDocumentDefinition() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);
            }
            finally
            {
                //clean up
                objDocumentDefinition = null;
            }
            //return results
            return (blnResult);
        }//end : BuildDocumentDefinition

        //define method : BuildPolicy
        public bool BuildPolicy(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillPolicyClaimInformation objPolicyInformation = null;
            ClaimsDocumentInputDocumentWhitehill objWhitehill = new ClaimsDocumentInputDocumentWhitehill();

            try
            {
                //get policy information
                objPolicyInformation = PolicyInformationGet(objDocGenerationRequest, strConnectionString);

                //check for results
                if (objPolicyInformation == null)
                {
                    //handle no data return
                    blnResult = false;
                }
                else
                {
                    //assign policy claim informatin
                    objWhitehill.PolicyClaimInformation = objPolicyInformation;
                    //apply policy information to whitehill
                    ClaimsDocumentInput objInput = new ClaimsDocumentInput();
                    ClaimsDocumentInputDocument objInputDocument = new ClaimsDocumentInputDocument();
                    objInputDocument.Whitehill = objWhitehill;
                    objInput.Document = objInputDocument;

                    this.objCDXML.Input = objInput;
                }//end : if (objDocumentIs == null)
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildPolicy() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objPolicyInformation = null;
            }
            finally
            {
                //clean up
                objWhitehill = null;
                objPolicyInformation = null;
            }
            //return results
            return (blnResult);
        }//end : BuildPolicy

        //define method : BuildCompanyInfo
        public bool BuildCompanyInfo(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentCompanyInformation objCompanyInfo = null;

            try
            {
                //get policy information
                objCompanyInfo = CompanyInformationGet(objDocGenerationRequest, strConnectionString);

                //check for results
                if (objCompanyInfo == null)
                {
                    //handle no data return
                    blnResult = false;
                }
                else
                {
                    //assign company informatin
                    this.objCDXML.CompanyInformation = objCompanyInfo;
                }//end : if (objDocumentIs == null)
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildCompanyInfo() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objCompanyInfo = null;
            }
            finally
            {
                //clean up
                objCompanyInfo = null;
            }
            //return results
            return (blnResult);
        }//end : BuildCompanyInfo

        //define method : BuildSubmitter
        public bool BuildSubmitter(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentSubmitter objSubmitter = null;

            try
            {
                //get submitter information
                objSubmitter = SubmitterInformationGet(objDocGenerationRequest, strConnectionString);

                //check for results
                if (objSubmitter == null)
                {
                    //handle no data return
                    blnResult = false;
                }
                else
                {
                    //assign submitter information
                    this.objCDXML.Submitter = objSubmitter;
                }//end : if (objDocumentIs == null)
            }
            catch (Exception ex)
            {
                //handle error
                objSubmitter = null;
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildSubmitter() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);
            }
            finally
            {
                //clean up
                objSubmitter = null;
            }
            //return results
            return (blnResult);

        }//end : BuildSubmitter

        //define method : BuildAddressee
        public bool BuildAddressee(DocGenerationRequest objDocGenerationRequest, string strAMSDBConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentAddressee objAddressee = new ClaimsDocumentAddressee();

            try
            {
                //get addressee
                objAddressee = AddresseeInformationGet(objDocGenerationRequest, strAMSDBConnectionString);

                //check results
                if (objAddressee != null)
                {
                    //add addressee to document
                    this.objCDXML.Addressee = objAddressee;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildAddressee() ";
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

            }

            //return results
            return (blnResult);

        }//end : BuildAddressee

        //define method : BuildCoverages
        public bool BuildCoverages(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillCoverages objCoverageIs = new ClaimsDocumentInputDocumentWhitehillCoverages();

            try
            {
                //get Coverage
                objCoverageIs = this.CoverageInformationGet(objDocGenerationRequest, strConnectionString);

                //check for coverage
                if (objCoverageIs != null)
                {
                    //apply Coverages to document
                    this.objCDXML.Input.Document.Whitehill.Coverages = objCoverageIs;
                }
                else
                {
                    //handle error
                    blnResult = false;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildCoverages() ";
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
                //cleanup
                objCoverageIs = null;
            }

            //return result
            return (blnResult);

        }//end : BuildCoverages

        //define method : BuildPremiumFinanceCo
        public bool BuildPremiumFinanceCo(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillPremiumFinanceCo objPremiumFinanceCoIs = new ClaimsDocumentInputDocumentWhitehillPremiumFinanceCo();

            try
            {
                //get Coverage
                objPremiumFinanceCoIs = this.PremiumFinanceCoInformationGet(objDocGenerationRequest, strConnectionString);

                //check for objPremiumFinanceCo
                if (objPremiumFinanceCoIs != null)
                {
                    //apply PremiumFinanceCo to document
                    this.objCDXML.Input.Document.Whitehill.PremiumFinanceCo = objPremiumFinanceCoIs;
                }
                else
                {
                    //handle error
                    blnResult = false;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildPremiumFinanceCo() ";
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
                //cleanup
                objPremiumFinanceCoIs = null;
            }

            //return result
            return (blnResult);

        }//end : BuildPremiumFinanceCo

        //define method : BuildLienHolder
        public bool BuildLienHolder(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillLienHolder objLeinHolderIs = new ClaimsDocumentInputDocumentWhitehillLienHolder();

            try
            {
                //get LeinHolder
                objLeinHolderIs = this.LienHolderInformationGet(objDocGenerationRequest, strConnectionString);

                //check for objLeinHolder
                if (objLeinHolderIs != null)
                {
                    //apply PremiumFinanceCo to document
                    this.objCDXML.Input.Document.Whitehill.LienHolder = objLeinHolderIs;
                }
                else
                {
                    //handle error
                    blnResult = false;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildLeinHolder() ";
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
                //cleanup
                objLeinHolderIs = null;
            }

            //return result
            return (blnResult);

        }//end : BuildLienHolder

        //define method : BuildNamedInsured
        public bool BuildNamedInsured(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillNamedInsured objNamedInsuredIs = new ClaimsDocumentInputDocumentWhitehillNamedInsured();

            try
            {
                //get NamedInsured
                objNamedInsuredIs = this.NamedInsuredInformationGet(objDocGenerationRequest, strConnectionString);

                //check for objNamedInsuredIs
                if (objNamedInsuredIs != null)
                {
                    //apply PremiumFinanceCo to document
                    this.objCDXML.Input.Document.Whitehill.NamedInsured = objNamedInsuredIs;
                }
                else
                {
                    //handle error
                    blnResult = false;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildNamedInsured() ";
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
                //cleanup
                objNamedInsuredIs = null;
            }

            //return result
            return (blnResult);

        }//end : BuildNamedInsured

        //define method : BuildVehicle
        public bool BuildVehicle(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillVehicle objVehicle = new ClaimsDocumentInputDocumentWhitehillVehicle();

            try
            {
                //get objVehicle
                objVehicle = this.VehicleInformationGet(objDocGenerationRequest, strConnectionString);

                //check for objVehicle
                if (objVehicle != null)
                {
                    //apply objVehicle to document
                    this.objCDXML.Input.Document.Whitehill.Vehicle = objVehicle;
                }
                else
                {
                    //handle error
                    blnResult = false;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildVehicle() ";
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
                //cleanup
                objVehicle = null;
            }

            //return result
            return (blnResult);

        }//end : BuildVehicle

        //define method : BuildVehicleCoverages
        public bool BuildVehicleCoverages(DocGenerationRequest objDocGenerationRequest, string strClaimsDBConnectionString)
        {
            //declare variables
            bool blnResult = true;
            List<ClaimsDocumentAllCoveragesCar> listCars = null;
            ClaimsDocumentAllCoverages objAllCoverages = new ClaimsDocumentAllCoverages();

            try
            {
                //load coverage codes
                LoadCoverageCodeFromDB(CDSupport.GenesisDBString);

                //get car coverage list
                listCars = VehicleGetCoverageList(objDocGenerationRequest, strClaimsDBConnectionString);

                //check for results
                if (listCars == null)
                {
                    //handle no data return
                    blnResult = false;
                }
                else
                {
                    //this.objCDXML.AllCoverages.AllCarCoverages = listCars.ToArray();
                    objAllCoverages.AllCarCoverages = listCars.ToArray();
                    this.objCDXML.AllCoverages = objAllCoverages;
                }//end : if (objDocumentIs == null)
            }
            catch (Exception ex)
            {
                //handle error
                //listCarCoverages = null;
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildVehicleCoverages() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);
            }
            finally
            {
                //clean up
                //listCarCoverages = null;
            }
            //return results
            return (blnResult);
        }//end : BuildVehicleCoverages

        //define method : BuildAdjuster
        public bool BuildAdjuster(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillAdjuster objAdjuster = new ClaimsDocumentInputDocumentWhitehillAdjuster();

            try
            {
                //get objAdjuster
                objAdjuster = this.AdjusterInformationGet(objDocGenerationRequest, strConnectionString);

                //check for objAdjuster
                if (objAdjuster != null)
                {
                    //apply objAdjuster to document
                    this.objCDXML.Input.Document.Whitehill.Adjuster = objAdjuster;
                }
                else
                {
                    //handle error
                    blnResult = false;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildAdjuster() ";
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
                //cleanup
                objAdjuster = null;
            }

            //return result
            return (blnResult);

        }//end : BuildAdjuster

        //define method : BuildLossDescription
        public bool BuildLossDescription(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillLossDescription objLossDescription = new ClaimsDocumentInputDocumentWhitehillLossDescription();

            try
            {
                //get objLossDescription
                objLossDescription = this.LossDescriptionInformationGet(objDocGenerationRequest, strConnectionString);

                //check for objLossDescription
                if (objLossDescription != null)
                {
                    //apply objAdjuster to document
                    this.objCDXML.Input.Document.Whitehill.LossDescription = objLossDescription;
                }
                else
                {
                    //handle error
                    blnResult = false;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildLossDescription() ";
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
                //cleanup
                objLossDescription = null;
            }

            //return result
            return (blnResult);

        }//end : BuildLossDescription

        //define method : BuildAttorney
        public bool BuildAttorney(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillAttorney objAttorney = new ClaimsDocumentInputDocumentWhitehillAttorney();

            try
            {
                //get objAttorney
                objAttorney = this.AttorneyInformationGet(objDocGenerationRequest, strConnectionString);

                //check for objAttorney
                if (objAttorney != null)
                {
                    //apply objAttorney to document
                    this.objCDXML.Input.Document.Whitehill.Attorney = objAttorney;
                }
                else
                {
                    //handle error
                    blnResult = false;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildAttorney() ";
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
                //cleanup
                objAttorney = null;
            }

            //return result
            return (blnResult);

        }//end : BuildAttorney

        //define method : BuildProducer
        public bool BuildProducer(DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillProducer objProducer = new ClaimsDocumentInputDocumentWhitehillProducer();

            try
            {
                //get Producer
                objProducer = this.ProducerInformationGet(objDocGenerationRequest, strConnectionString);

                //check for objProducer
                if (objProducer != null)
                {
                    //apply objProducerto document
                    this.objCDXML.Input.Document.Whitehill.Producer = objProducer;
                }
                else
                {
                    //handle error
                    blnResult = false;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildProducer() ";
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
                //cleanup
                objProducer = null;
            }

            //return result
            return (blnResult);

        }//end : BuildProducer

        //define method : BuildWhiteHillAddressee
        public bool BuildWhiteHillAddressee(DocGenerationRequest objDocGenerationRequest, string strAMSDBConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillAddressee objAddressee = new ClaimsDocumentInputDocumentWhitehillAddressee();

            try
            {
                //get addressee
                objAddressee = WhiteHillAddresseeInformationGet(objDocGenerationRequest, strAMSDBConnectionString);

                //check results
                if (objAddressee != null)
                {
                    //add addressee to document
                    this.objCDXML.Input.Document.Whitehill.Addressee = objAddressee;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildWhiteHillAddressee() ";
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

            }

            //return results
            return (blnResult);

        }//end : BuildWhiteHillAddressee

        //define method : SplitStringIntoSetOfChars
        public static string[] SplitStringIntoSetOfChars(string strValue, int intMaxCharacters)
        {
            //declare variables
            string[] arrString = null;
            int intCnt = 0;

            try
            {
                //make sure we have a string
                if (!string.IsNullOrEmpty(strValue))
                {
                    //is the string longer than intMaxCharacters
                    if (strValue.Length < intMaxCharacters)
                    {
                        //place into single index array
                        arrString = new string[1];
                        arrString[0] = strValue;
                    }
                    else
                    {
                        //determine if we need to add an additional index
                        //for remaining text
                        if ((strValue.Length % intMaxCharacters) > 0)
                        {
                            arrString = new string[(int)(strValue.Length / intMaxCharacters) + 1];
                        }
                        else
                        {
                            arrString = new string[(strValue.Length / intMaxCharacters)];
                        }

                        //process string
                        do
                        {
                            arrString[intCnt] = strValue.Substring(0, intMaxCharacters);
                            strValue = strValue.Substring(intMaxCharacters);
                            intCnt++;
                        } while (strValue.Length > intMaxCharacters);

                        //get string remainder
                        if (strValue.Length > 0)
                        {
                            arrString[intCnt] = strValue;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                //handle error
                arrString = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : SplitStringIntoSetOfChars() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);
            }
            finally
            {
            }

            //return string array
            return (arrString);
        }//end : SplitStringIntoSetOfChars

        //define method : BuildMetaData
        public bool BuildMetaData(List<DocumentDisplayField> listDisplayFields, DocGenerationRequest objDocGenerationRequest, string strConnectionString)
        {
            //declare variables
            bool blnResult = true;
            ClaimsDocumentInputDocumentWhitehillPolicyClaimInformation objPolicyInformation = null;
            ClaimsDocumentInputDocumentWhitehill objWhitehill = new ClaimsDocumentInputDocumentWhitehill();
            ClaimsDocumentInputMetaData objMetaData = new ClaimsDocumentInputMetaData();
            List<ClaimsDocumentInputMetaDataDisplayField> listDisplayFieldList = new List<ClaimsDocumentInputMetaDataDisplayField>();
            List<ClaimsDocumentInputMetaDataAddress> listAddresses = new List<ClaimsDocumentInputMetaDataAddress>();
            int intIndex = 0;
            string strCurrentString = "";
            string[] arrDisplayFieldValues;
            int intCountValues = 0;
            int intCnt = 0;

            try
            {
                //get policy information
                objPolicyInformation = PolicyInformationGet(objDocGenerationRequest, strConnectionString);

                //check for results
                if (objPolicyInformation == null)
                {
                    //handle no data return
                    blnResult = false;
                }
                else
                {
                    //get display fields
                    for (intIndex = 0; intIndex < listDisplayFields.Count; intIndex++)
                    {
                        //get new display field
                        switch (listDisplayFields[intIndex].DisplayFieldName.ToString().ToUpper())
                        {
                            case "LIABILITYREASON":
                            case "POLICYSTATEMENT":
                            case "POSITION":
                            case "REASON":
                            case "UNDEFINEDINFO":
                                //get current string
                                strCurrentString = listDisplayFields[intIndex].DisplayFieldValue.ToString();

                                arrDisplayFieldValues = SplitStringIntoSetOfChars(strCurrentString, 1024);
                                if (arrDisplayFieldValues != null)
                                {
                                    //get the number of values
                                    intCountValues = arrDisplayFieldValues.Length;

                                    //create list
                                    for (intCnt = 0; intCnt < intCountValues; intCnt++)
                                    {
                                        ClaimsDocumentInputMetaDataDisplayField objMetaDataDisplayFieldList = new ClaimsDocumentInputMetaDataDisplayField();
                                        objMetaDataDisplayFieldList.Name = listDisplayFields[intIndex].DisplayFieldName.ToString();
                                        objMetaDataDisplayFieldList.Value = arrDisplayFieldValues[intCnt].ToString();
                                        //add field to list
                                        listDisplayFieldList.Add(objMetaDataDisplayFieldList);
                                    }
                                }

                                break;

                            default:
                                ClaimsDocumentInputMetaDataDisplayField objMetaDataDisplayField = new ClaimsDocumentInputMetaDataDisplayField();
                                objMetaDataDisplayField.Name = listDisplayFields[intIndex].DisplayFieldName.ToString();
                                objMetaDataDisplayField.Value = listDisplayFields[intIndex].DisplayFieldValue.ToString();
                                //add field to list
                                listDisplayFieldList.Add(objMetaDataDisplayField);
                                break;
                        }

                    }


                    //apply display fields
                    objMetaData.DisplayFields = listDisplayFieldList.ToArray();

                    //meta data header information from policy information
                    objMetaData.Department = objPolicyInformation.Department;
                    objMetaData.Policy = objPolicyInformation.PolicyNumber;
                    objMetaData.Program = objPolicyInformation.ProgramCode;
                    objMetaData.User = objPolicyInformation.User;
                    objMetaData.C4ContactNumber = objDocGenerationRequest.ContactNumber.ToString();
                    objMetaData.C4ContactType = objDocGenerationRequest.ContactType.ToString();

                    //get meta data addressee information from whitehill addressee
                    if (this.objCDXML.Input.Document.Whitehill.Addressee != null)
                    {
                        //create new address object
                        ClaimsDocumentInputMetaDataAddress objAddressAddressee = new ClaimsDocumentInputMetaDataAddress();
                        ClaimsDocumentInputMetaDataAddressAddressLines objAddressLinesAddressee = new ClaimsDocumentInputMetaDataAddressAddressLines();
                        //setup address
                        objAddressAddressee.AddressType = "addressee";
                        objAddressAddressee.EMail = this.objCDXML.Input.Document.Whitehill.Addressee.AddresseeEmailAddress;
                        objAddressAddressee.Fax = this.objCDXML.Input.Document.Whitehill.Addressee.AddresseeFaxNumber;
                        objAddressAddressee.Name = this.objCDXML.Input.Document.Whitehill.Addressee.AddresseeName;
                        objAddressAddressee.Phone = this.objCDXML.Input.Document.Whitehill.Addressee.AddresseePhoneNumber;
                        //get address lines
                        ClaimsDocumentInputMetaDataAddressAddressLines objAddressLineAddressee = new ClaimsDocumentInputMetaDataAddressAddressLines();
                        objAddressLineAddressee.AddressLine1 = this.objCDXML.Input.Document.Whitehill.Addressee.AddresseeAddressLine1;
                        objAddressLineAddressee.AddressLine2 = this.objCDXML.Input.Document.Whitehill.Addressee.AddresseeAddressLine1;
                        objAddressLineAddressee.AddressLine3 = this.objCDXML.Input.Document.Whitehill.Addressee.AddresseeAddressLine1;
                        //add address lines to address
                        objAddressAddressee.AddressLines = objAddressLineAddressee;
                        //add address to meta data address list
                        listAddresses.Add(objAddressAddressee);
                    }

                    //get meta data insured address information from whitehill named insured addressee
                    if (this.objCDXML.Input.Document.Whitehill.NamedInsured != null)
                    {
                        //create new address object
                        ClaimsDocumentInputMetaDataAddress objAddressNamedInsured = new ClaimsDocumentInputMetaDataAddress();
                        ClaimsDocumentInputMetaDataAddressAddressLines objAddressLinesNamedInsured = new ClaimsDocumentInputMetaDataAddressAddressLines();
                        //setup address
                        objAddressNamedInsured.AddressType = "insured";
                        objAddressNamedInsured.EMail = this.objCDXML.Input.Document.Whitehill.NamedInsured.NamedInsuredEmailAddress;
                        objAddressNamedInsured.Fax = this.objCDXML.Input.Document.Whitehill.NamedInsured.NamedInsuredFaxNumber;
                        objAddressNamedInsured.Name = this.objCDXML.Input.Document.Whitehill.NamedInsured.NamedInsuredFullName;
                        objAddressNamedInsured.Phone = this.objCDXML.Input.Document.Whitehill.NamedInsured.NamedInsuredPhoneNumber;
                        //get address lines
                        ClaimsDocumentInputMetaDataAddressAddressLines objAddressLineNamedInsured = new ClaimsDocumentInputMetaDataAddressAddressLines();
                        objAddressLineNamedInsured.AddressLine1 = this.objCDXML.Input.Document.Whitehill.NamedInsured.NamedInsuredAddressLine1;
                        objAddressLineNamedInsured.AddressLine2 = this.objCDXML.Input.Document.Whitehill.NamedInsured.NamedInsuredAddressLine2;
                        objAddressLineNamedInsured.AddressLine3 = this.objCDXML.Input.Document.Whitehill.NamedInsured.NamedInsuredAddressLine3;
                        //add address lines to address
                        objAddressNamedInsured.AddressLines = objAddressLineNamedInsured;
                        //add address to meta data address list
                        listAddresses.Add(objAddressNamedInsured);
                    }

                    //get meta data producer address information from whitehill producer addressee
                    if (this.objCDXML.Input.Document.Whitehill.Producer != null)
                    {
                        //create new address object
                        ClaimsDocumentInputMetaDataAddress objAddressProducer = new ClaimsDocumentInputMetaDataAddress();
                        ClaimsDocumentInputMetaDataAddressAddressLines objAddressLinesProducer = new ClaimsDocumentInputMetaDataAddressAddressLines();
                        //setup address
                        objAddressProducer.AddressType = "producer";
                        objAddressProducer.EMail = this.objCDXML.Input.Document.Whitehill.Producer.ProducerEmailAddress;
                        objAddressProducer.Fax = this.objCDXML.Input.Document.Whitehill.Producer.ProducerFaxNumber;
                        objAddressProducer.Name = this.objCDXML.Input.Document.Whitehill.Producer.ProducerName;
                        objAddressProducer.Phone = this.objCDXML.Input.Document.Whitehill.Producer.ProducerPhoneNumber;
                        //get address lines
                        ClaimsDocumentInputMetaDataAddressAddressLines objAddressLineProducer = new ClaimsDocumentInputMetaDataAddressAddressLines();
                        objAddressLineProducer.AddressLine1 = this.objCDXML.Input.Document.Whitehill.Producer.ProducerAddressLine1;
                        objAddressLineProducer.AddressLine2 = this.objCDXML.Input.Document.Whitehill.Producer.ProducerAddressLine2;
                        objAddressLineProducer.AddressLine3 = this.objCDXML.Input.Document.Whitehill.Producer.ProducerAddressLine3;
                        //add address lines to address
                        objAddressProducer.AddressLines = objAddressLineProducer;
                        //add address to meta data address list
                        listAddresses.Add(objAddressProducer);
                    }

                    //get meta data attorney address information from whitehill attorney addressee
                    if (this.objCDXML.Input.Document.Whitehill.Attorney != null)
                    {
                        //create new address object
                        ClaimsDocumentInputMetaDataAddress objAddressAttorney = new ClaimsDocumentInputMetaDataAddress();
                        ClaimsDocumentInputMetaDataAddressAddressLines objAddressLinesAttorney = new ClaimsDocumentInputMetaDataAddressAddressLines();
                        //setup address
                        objAddressAttorney.AddressType = "attorney";
                        objAddressAttorney.EMail = this.objCDXML.Input.Document.Whitehill.Attorney.AttorneyEmailAddress;
                        objAddressAttorney.Fax = this.objCDXML.Input.Document.Whitehill.Attorney.AttorneyFaxNumber;
                        objAddressAttorney.Name = this.objCDXML.Input.Document.Whitehill.Attorney.AttorneyFirstName + " " + this.objCDXML.Input.Document.Whitehill.Attorney.AttorneyLastName;
                        objAddressAttorney.Phone = this.objCDXML.Input.Document.Whitehill.Attorney.AttorneyPhoneNumber;
                        //get address lines
                        ClaimsDocumentInputMetaDataAddressAddressLines objAddressLineAttorney = new ClaimsDocumentInputMetaDataAddressAddressLines();
                        objAddressLineAttorney.AddressLine1 = this.objCDXML.Input.Document.Whitehill.Producer.ProducerAddressLine1;
                        objAddressLineAttorney.AddressLine2 = this.objCDXML.Input.Document.Whitehill.Producer.ProducerAddressLine2;
                        objAddressLineAttorney.AddressLine3 = this.objCDXML.Input.Document.Whitehill.Producer.ProducerAddressLine3;
                        //add address lines to address
                        objAddressAttorney.AddressLines = objAddressLineAttorney;
                        //add address to meta data address list
                        listAddresses.Add(objAddressAttorney);
                    }

                    //add addresses to meta data
                    objMetaData.Addresses = listAddresses.ToArray();

                    this.objCDXML.Input.MetaData = objMetaData;
                }//end : if (objDocumentIs == null)
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : BuildMetaData() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objPolicyInformation = null;
            }
            finally
            {
                //clean up
                objWhitehill = null;
                objPolicyInformation = null;
            }
            //return results
            return (blnResult);
        }//end : BuildMetaData

        //define method : ClaimsDocsImageRightRequestFill(DocGenerationRequest objDocGenerationRequest)
        public ImageRightClaimRequest ClaimsDocsImageRightRequestFill(string strSourceDocumentPathAndFileName, DocGenerationRequest objDocGenerationRequest)
        {
            //declare variables
            ImageRightClaimRequest objIRClaimsRequest = new ImageRightClaimRequest();
            PackDocType objPackDocType = new PackDocType();

            try
            {
                //setup service
                objIRClaimsRequest.ImageRightWSUrl = CDSupport.ImageRightWSURL;
                objIRClaimsRequest.ImageRightLogin = CDSupport.ImageRightLogin;
                objIRClaimsRequest.ImageRightPassword = CDSupport.ImageRightPassword;

                //setup document storage parameters
                objIRClaimsRequest.SourceDocumentPath = strSourceDocumentPathAndFileName;
                objIRClaimsRequest.FileNumber = objDocGenerationRequest.PolicyNumber.Substring(0, 3) + objDocGenerationRequest.PolicyNumber.Substring(6, 6);
                objIRClaimsRequest.ClaimsNumber = objDocGenerationRequest.ClaimNumber;
                objIRClaimsRequest.ClaimsDocsDocID = this.objCDXML.DocumentDefinition.CompanyCode;

                objIRClaimsRequest.RequestorUserName = objDocGenerationRequest.UserName;
                objPackDocType = PackDocTypeGet(objDocGenerationRequest.GroupName);
                objIRClaimsRequest.PackType = objPackDocType.PackType.ToString();
                objIRClaimsRequest.DocType = objPackDocType.DocType;
                objIRClaimsRequest.ImageRightClaimsDrawer = CDSupport.ImageRightClaimsDrawer;
                objIRClaimsRequest.CaptureDate = DateTime.Now.ToString();
                objIRClaimsRequest.FolderName = this.objCDXML.Input.Document.Whitehill.NamedInsured.NamedInsuredLastName + ", " + this.objCDXML.Input.Document.Whitehill.NamedInsured.NamedInsuredFirstName;
                objIRClaimsRequest.DateOfLoss = this.objCDXML.Input.Document.Whitehill.LossDescription.DayofLoss;
                objIRClaimsRequest.Reason = CDSupport.ImageRightReason;

                //setup task parameters
                objIRClaimsRequest.CreateTask = true;
                objIRClaimsRequest.FlowID = CDSupport.ImageRightFlowID.ToString();
                objIRClaimsRequest.StepID = CDSupport.ImageRightStepID.ToString(); ;
                objIRClaimsRequest.Description = CDSupport.ImageRightDescription;
                objIRClaimsRequest.Priority = CDSupport.ImageRightPriority.ToString();

            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : ClaimsDocsImageRightRequestFill() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objIRClaimsRequest = null;
            }
            finally
            {

            }

            //return result
            return (objIRClaimsRequest);
        }//end : ClaimsDocsImageRightRequestFill

        #endregion

        #region XML Document Input, Output, Export, Import Methods

        //define method : SerializeDocumentToXMLFile
        private string SerializeDocumentToXMLFile()
        {
            //declare variables
            XmlSerializer serializer = new XmlSerializer(typeof(ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument));
            string strClaimsDocsXMLFilePathAndName = "";
            string strClaimsDocsXMLUNCPathAndFileName = "";

            try
            {
                strClaimsDocsXMLFilePathAndName = CDSupport.XMLOutputLocation + this.objCDXML.InstanceID + ".xml";
                strClaimsDocsXMLUNCPathAndFileName = CDSupport.XMLUNCOutputLocation + this.objCDXML.InstanceID + ".xml";
                //create file stream
                Stream fs = new FileStream(strClaimsDocsXMLFilePathAndName, FileMode.Create);
                //create xml writer
                XmlWriter writer = new XmlTextWriter(fs, Encoding.UTF8);
                // Serialize using the XmlTextWriter.
                serializer.Serialize(writer, objCDXML);
                //close writer
                writer.Close();
            }
            catch (Exception ex)
            {
                //handle error
                strClaimsDocsXMLFilePathAndName = "";
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : SerializeDocumentToXMLFile() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
            }
            finally
            {
            }
            //return
            return (strClaimsDocsXMLUNCPathAndFileName);
        }//end : SerializeDocumentToXMLFile

        //define method : GenerateClaimsDocPDF
        //private string GenerateClaimsDocPDF(out string strDocumentXMLFilePathAndName)
        private List<DocuMakerClaimsDocsResponse> GenerateClaimsDocPDF(string claimsNumber, string documentDefinitionName)
        {
            //declare variables
            XmlSerializer serializer = new XmlSerializer(typeof(ClaimsDocument));
            Utility objUtility = new Utility();
            List<DocuMakerClaimsDocsResponse> listDocuMakerClaimsDocsResponse = null;

            try
            {
                //generate document and get results
                listDocuMakerClaimsDocsResponse = objUtility.CallDocumentGeneration(this.objCDXML, claimsNumber, documentDefinitionName);
            }
            catch (Exception ex)
            {
                //handle err;
                listDocuMakerClaimsDocsResponse = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : GenerateClaimsDocPDF() ";
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
            }

            //return results
            return (listDocuMakerClaimsDocsResponse);

        }//end : GenerateClaimsDocPDF

        #endregion

        #region ImageRight Methods

        //define method : ImageRightSaveDocument
        //define method : ImageRightSaveDocument
        public ImageRightClaimRequest ImageRightSaveDocument(ImageRightClaimRequest objIRRequest)
        {

            try
            {

                List<DocumentStorageWS1.DocumentInfoVar> vars = new List<DocumentStorageWS1.DocumentInfoVar>();
                DocumentStorageWSSoapClient docStorage = new DocumentStorageWSSoapClient("DocumentStorageWSSoap");


                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "taskDescription",
                    Value = "ClAIMS SAVE DOCUMENT"
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "FolderNumber",
                    Value = objIRRequest.ClaimsNumber
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "Drawer",
                    Value = CDSupport.ImageRightClaimsDrawer
                });


                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "FolderName",
                    Value = objIRRequest.FolderName
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "DocType",
                    Value = objIRRequest.DocType
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "UserData1",
                    Value = objIRRequest.DateOfLoss
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "UserData2",
                    Value = objIRRequest.FileNumber
                });


                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "PackageType",
                    Value = objIRRequest.PackType
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "Reason",
                    Value = objIRRequest.Reason
                });


                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "Format",
                    Value = objIRRequest.SourceDocumentPath.Substring(objIRRequest.SourceDocumentPath.Length - 3, 3).ToUpper()
                });

                objIRRequest.ImageRightLogin = CDSupport.ImageRightLogin;
                objIRRequest.ImageRightPassword = CDSupport.ImageRightPassword;

                //set request result to true
                objIRRequest.RequestResult = true;

                //setup file stream
                using (FileStream objFileStream = new FileStream(objIRRequest.SourceDocumentPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] filebites = new byte[objFileStream.Length];
                    objFileStream.Read(filebites, 0, filebites.Length);

                    vars.Add(new DocumentStorageWS1.DocumentInfoVar
                    {
                        Name = "Base64FileContents",
                        Value = System.Convert.ToBase64String(filebites)
                    });
                    objFileStream.Close();
                }

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "FlowID",
                    Value = CDSupport.ImageRightFlowID.ToString()
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "StepID",
                    Value = CDSupport.ImageRightStepID.ToString()
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "Description",
                    Value = objIRRequest.ClaimsDocsDocID
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "Priority",
                    Value = CDSupport.ImageRightPriority.ToString()
                });

            
                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "CreateTask",
                    Value = System.Convert.ToString( objIRRequest.CreateTask)
                });

 
                //check for task creation
                if (objIRRequest.CreateTask == true)
                {
                    string UserID;
                    switch (CDSupport.RunMode.ToUpper())
                    {
                        case "PRODUCTION":
                            UserID = null;
                            break;
                        default:
                            
                            if (objIRRequest.RequestorUserName.Length > 10)
                            {
                                UserID = objIRRequest.RequestorUserName.ToUpper().Substring(0, 10);
                            }
                            else
                            {
                                UserID = objIRRequest.RequestorUserName.ToUpper();
                            }
                            break;
                    }
                    vars.Add(new DocumentStorageWS1.DocumentInfoVar
                    {
                        Name = "TaskUserID",
                        Value = UserID
                    });
                }
                var transactionID = Guid.Empty;
                CreateTaskTrxRequest request = new CreateTaskTrxRequest(transactionID, vars.ToArray(), "ClmsDocs");
                var TaskID = docStorage.CreateTaskTrx(request);

            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : GenerateClaimsDocPDF() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
                objIRRequest.RequestResult = false;
            }
            finally
            {

            }

            //return result
            return (objIRRequest);
        } //end : ImageRightSaveDocument

        //define method : ImageRightGetDocument
        public ImageRightClaimRequest ImageRightGetDocument(ImageRightClaimRequest objIRPolicyRequest)
        {
            try
            {
                //declare variables
                List<DocumentStorageWS1.DocumentInfoVar> vars = new List<DocumentStorageWS1.DocumentInfoVar>();
                DocumentStorageWSSoapClient docStorage = new DocumentStorageWSSoapClient("DocumentStorageWSSoap");

                objIRPolicyRequest.CaptureDate = DateTime.Now.ToString("d");

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "FolderNumber",
                    Value = objIRPolicyRequest.FileNumber
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "Drawer",
                    Value = objIRPolicyRequest.ImageRightClaimsDrawer
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "PackageType",
                    Value = objIRPolicyRequest.PackType
                });

                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "DocType",
                    Value = objIRPolicyRequest.DocType
                });
                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "DateCaptured",
                    Value = objIRPolicyRequest.CaptureDate
                });
                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "ReferenceNumber",
                    Value = objIRPolicyRequest.ClaimsDocsDocID
                });
                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "DocumentKey",
                    Value = objIRPolicyRequest.DocumentKey
                });
                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "DocID",
                    Value = "-1"
                });
                vars.Add(new DocumentStorageWS1.DocumentInfoVar
                {
                    Name = "PageID",
                    Value = "-1"
                });

                DocumentStorageWS1.DocumentInfoVar[] docVars = vars.ToArray();
                var transactionID = Guid.Empty;
                FindDocumentRequest request = new FindDocumentRequest(transactionID, docVars);
                docStorage.FindDocument(request);

                if (docVars != null)
                {
                    string devicePath = GetValueFromVarsArray(docVars, "DevicePath");
                    string pageFilename = GetValueFromVarsArray(docVars, "PageFileName");
                    string pageFormat = GetValueFromVarsArray(docVars, "PageFormat");
                    string pageDateCaptured = GetValueFromVarsArray(docVars, "PageDateCaptured");
                    string pageReason = GetValueFromVarsArray(docVars, "PageReason");
                    string tempDin = GetValueFromVarsArray(docVars, "TempDin");

                    objIRPolicyRequest.IRDocumentPath = devicePath + pageFilename + "." + pageFormat;
                    objIRPolicyRequest.DocumentKey = tempDin;
                }


            }
            catch (Exception ex)
            {
                //handle error
                objIRPolicyRequest.RequestResult = false;
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : GenerateClaimsDocPDF() ";
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
            }

            //return result
            return (objIRPolicyRequest);
        }

        private static string GetValueFromVarsArray(DocumentStorageWS1.DocumentInfoVar[] vars, string key)
        {
            if (vars == null) return string.Empty;

            string value = string.Empty;

            DocumentStorageWS1.DocumentInfoVar info = vars.FirstOrDefault(v => v.Name.Equals(key, StringComparison.CurrentCultureIgnoreCase));
            if (info != null)
            {
                value = info.Value;
            }
            return (value == string.Empty) ? string.Empty : value;
        }


        #endregion


    }//end : ClaimsDocsGenerator
}//end : namespace ClaimsDocsBizLogic


