using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Xml;
using ClaimsDocsBizLogic.SchemaClasses;
using System.Xml.Serialization;
using System.IO;
namespace ClaimsDocsBizLogic
{
    // NOTE: If you change the class name "CDDocument" here, you must also update the reference to "CDDocument" in App.config.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CDDocument : ICDDocument
    {
        //declare private class variables
        private List<Document> _listDocument = new List<Document>();
        private List<DocumentGroup> _listDocumentGroup = new List<DocumentGroup>();
        private List<DocumentField> _listDocumentField = new List<DocumentField>();
        private List<DocumentApprovalQueue> _listDocumentApprovalQueue = new List<DocumentApprovalQueue>();
        private List<DocumentsNeedingApproval> _listDocumentsNeedingApproval = new List<DocumentsNeedingApproval>();

        //define method : DocumentCreate
        public int DocumentCreate(Document objDocument, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlTransaction tranTransaction = null;
            int intResult = 0;
            int intNewDocumentID = 0;
            int intIndex = 0;

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //open connection
                cnnConnection.Open();
                //start Location Transaction
                tranTransaction = cnnConnection.BeginTransaction("CDCreateDoc");

                #region Create Document

                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.Transaction = tranTransaction;
                cmdCommand.CommandText = "spDocumentCreate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocument.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocument.DocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentID);

                //Add input parameter for (objDocument.DocumentCode) and set its properties
                SqlParameter paramDocumentCode = new SqlParameter();
                paramDocumentCode.ParameterName = "@vchrDocumentCode";
                paramDocumentCode.SqlDbType = SqlDbType.VarChar;
                paramDocumentCode.Direction = ParameterDirection.Input;
                paramDocumentCode.Value = objDocument.DocumentCode;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentCode);

                //Add input parameter for (objDocument.DepartmentID) and set its properties
                SqlParameter paramDepartmentID = new SqlParameter();
                paramDepartmentID.ParameterName = "@intDepartmentID";
                paramDepartmentID.SqlDbType = SqlDbType.Int;
                paramDepartmentID.Direction = ParameterDirection.Input;
                paramDepartmentID.Value = objDocument.DepartmentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDepartmentID);

                //Add input parameter for (objDocument.ProgramID) and set its properties
                SqlParameter paramProgramID = new SqlParameter();
                paramProgramID.ParameterName = "@intProgramID";
                paramProgramID.SqlDbType = SqlDbType.Int;
                paramProgramID.Direction = ParameterDirection.Input;
                paramProgramID.Value = objDocument.ProgramID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramProgramID);

                //Add input parameter for (objDocument.Review) and set its properties
                SqlParameter paramReview = new SqlParameter();
                paramReview.ParameterName = "@vchrReview";
                paramReview.SqlDbType = SqlDbType.VarChar;
                paramReview.Direction = ParameterDirection.Input;
                paramReview.Value = objDocument.Review;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramReview);

                //Add input parameter for (objDocument.Description) and set its properties
                SqlParameter paramDescription = new SqlParameter();
                paramDescription.ParameterName = "@vchrDescription";
                paramDescription.SqlDbType = SqlDbType.VarChar;
                paramDescription.Direction = ParameterDirection.Input;
                paramDescription.Value = objDocument.Description;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDescription);

                //Add input parameter for (objDocument.TemplateName) and set its properties
                SqlParameter paramTemplateName = new SqlParameter();
                paramTemplateName.ParameterName = "@vchrTemplateName";
                paramTemplateName.SqlDbType = SqlDbType.VarChar;
                paramTemplateName.Direction = ParameterDirection.Input;
                paramTemplateName.Value = objDocument.TemplateName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramTemplateName);

                //Add input parameter for (objDocument.StyleSheetName) and set its properties
                SqlParameter paramStyleSheetName = new SqlParameter();
                paramStyleSheetName.ParameterName = "@vchrStyleSheetName";
                paramStyleSheetName.SqlDbType = SqlDbType.VarChar;
                paramStyleSheetName.Direction = ParameterDirection.Input;
                paramStyleSheetName.Value = objDocument.StyleSheetName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramStyleSheetName);

                //Add input parameter for (objDocument.EffectiveDate) and set its properties
                SqlParameter paramEffectiveDate = new SqlParameter();
                paramEffectiveDate.ParameterName = "@datEffectiveDate";
                paramEffectiveDate.SqlDbType = SqlDbType.DateTime;
                paramEffectiveDate.Direction = ParameterDirection.Input;
                paramEffectiveDate.Value = objDocument.EffectiveDate;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramEffectiveDate);

                //Add input parameter for (objDocument.ExpirationDate) and set its properties
                SqlParameter paramExpirationDate = new SqlParameter();
                paramExpirationDate.ParameterName = "@datExpirationDate";
                paramExpirationDate.SqlDbType = SqlDbType.DateTime;
                paramExpirationDate.Direction = ParameterDirection.Input;
                paramExpirationDate.Value = objDocument.ExpirationDate;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramExpirationDate);

                //Add input parameter for (objDocument.ProofOfMailing) and set its properties
                SqlParameter paramProofOfMailing = new SqlParameter();
                paramProofOfMailing.ParameterName = "@vchrProofOfMailing";
                paramProofOfMailing.SqlDbType = SqlDbType.VarChar;
                paramProofOfMailing.Direction = ParameterDirection.Input;
                paramProofOfMailing.Value = objDocument.ProofOfMailing;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramProofOfMailing);

                //Add input parameter for (objDocument.DataMatx) and set its properties
                SqlParameter paramDataMatx = new SqlParameter();
                paramDataMatx.ParameterName = "@vchrDataMatx";
                paramDataMatx.SqlDbType = SqlDbType.VarChar;
                paramDataMatx.Direction = ParameterDirection.Input;
                paramDataMatx.Value = objDocument.DataMatx;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDataMatx);

                //Add input parameter for (objDocument.ImportToImageRight) and set its properties
                SqlParameter paramImportToImageRight = new SqlParameter();
                paramImportToImageRight.ParameterName = "@vchrImportToImageRight";
                paramImportToImageRight.SqlDbType = SqlDbType.VarChar;
                paramImportToImageRight.Direction = ParameterDirection.Input;
                paramImportToImageRight.Value = objDocument.ImportToImageRight;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramImportToImageRight);

                //Add input parameter for (objDocument.ImageRightDocumentID) and set its properties
                SqlParameter paramImageRightDocumentID = new SqlParameter();
                paramImageRightDocumentID.ParameterName = "@vchrImageRightDocumentID";
                paramImageRightDocumentID.SqlDbType = SqlDbType.VarChar;
                paramImageRightDocumentID.Direction = ParameterDirection.Input;
                paramImageRightDocumentID.Value = objDocument.ImageRightDocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramImageRightDocumentID);

                //Add input parameter for (objDocument.ImageRightDocumentSection) and set its properties
                SqlParameter paramImageRightDocumentSection = new SqlParameter();
                paramImageRightDocumentSection.ParameterName = "@vchrImageRightDocumentSection";
                paramImageRightDocumentSection.SqlDbType = SqlDbType.VarChar;
                paramImageRightDocumentSection.Direction = ParameterDirection.Input;
                paramImageRightDocumentSection.Value = objDocument.ImageRightDocumentSection;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramImageRightDocumentSection);

                //Add input parameter for (objDocument.ImageRightDrawer) and set its properties
                SqlParameter paramImageRightDrawer = new SqlParameter();
                paramImageRightDrawer.ParameterName = "@vchrImageRightDrawer";
                paramImageRightDrawer.SqlDbType = SqlDbType.VarChar;
                paramImageRightDrawer.Direction = ParameterDirection.Input;
                paramImageRightDrawer.Value = objDocument.ImageRightDrawer;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramImageRightDrawer);

                //Add input parameter for (objDocument.CopyAgent) and set its properties
                SqlParameter paramContactNo = new SqlParameter();
                paramContactNo.ParameterName = "@intContactNo";
                paramContactNo.SqlDbType = SqlDbType.Int;
                paramContactNo.Direction = ParameterDirection.Input;
                paramContactNo.Value = objDocument.ContactNo;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramContactNo);

                //Add input parameter for (objDocument.CopyAgent) and set its properties
                SqlParameter paramContactType = new SqlParameter();
                paramContactType.ParameterName = "@intContactType";
                paramContactType.SqlDbType = SqlDbType.Int;
                paramContactType.Direction = ParameterDirection.Input;
                paramContactType.Value = objDocument.ContactType;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramContactType);

                //Add input parameter for (objDocument.CopyAgent) and set its properties
                SqlParameter paramCopyAgent = new SqlParameter();
                paramCopyAgent.ParameterName = "@vchrCopyAgent";
                paramCopyAgent.SqlDbType = SqlDbType.VarChar;
                paramCopyAgent.Direction = ParameterDirection.Input;
                paramCopyAgent.Value = objDocument.CopyAgent;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCopyAgent);

                //Add input parameter for (objDocument.CopyInsured) and set its properties
                SqlParameter paramCopyInsured = new SqlParameter();
                paramCopyInsured.ParameterName = "@vchrCopyInsured";
                paramCopyInsured.SqlDbType = SqlDbType.VarChar;
                paramCopyInsured.Direction = ParameterDirection.Input;
                paramCopyInsured.Value = objDocument.CopyInsured;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCopyInsured);

                //Add input parameter for (objDocument.CopyLienHolder) and set its properties
                SqlParameter paramCopyLienHolder = new SqlParameter();
                paramCopyLienHolder.ParameterName = "@vchrCopyLienHolder";
                paramCopyLienHolder.SqlDbType = SqlDbType.VarChar;
                paramCopyLienHolder.Direction = ParameterDirection.Input;
                paramCopyLienHolder.Value = objDocument.CopyLienHolder;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCopyLienHolder);

                //Add input parameter for (objDocument.CopyFinanceCo) and set its properties
                SqlParameter paramCopyFinanceCo = new SqlParameter();
                paramCopyFinanceCo.ParameterName = "@vchrCopyFinanceCo";
                paramCopyFinanceCo.SqlDbType = SqlDbType.VarChar;
                paramCopyFinanceCo.Direction = ParameterDirection.Input;
                paramCopyFinanceCo.Value = objDocument.CopyFinanceCo;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCopyFinanceCo);

                //Add input parameter for (objDocument.CopyAttorney) and set its properties
                SqlParameter paramCopyAttorney = new SqlParameter();
                paramCopyAttorney.ParameterName = "@vchrCopyAttorney";
                paramCopyAttorney.SqlDbType = SqlDbType.VarChar;
                paramCopyAttorney.Direction = ParameterDirection.Input;
                paramCopyAttorney.Value = objDocument.CopyAttorney;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramCopyAttorney);

                //Add input parameter for (objDocument.DiaryNumberOfDays) and set its properties
                SqlParameter paramDiaryNumberOfDays = new SqlParameter();
                paramDiaryNumberOfDays.ParameterName = "@intDiaryNumberOfDays";
                paramDiaryNumberOfDays.SqlDbType = SqlDbType.Int;
                paramDiaryNumberOfDays.Direction = ParameterDirection.Input;
                paramDiaryNumberOfDays.Value = objDocument.DiaryNumberOfDays;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDiaryNumberOfDays);

                //Add input parameter for (objDocument.DiaryAutoUpdate) and set its properties
                SqlParameter paramDiaryAutoUpdate = new SqlParameter();
                paramDiaryAutoUpdate.ParameterName = "@vchrDiaryAutoUpdate";
                paramDiaryAutoUpdate.SqlDbType = SqlDbType.VarChar;
                paramDiaryAutoUpdate.Direction = ParameterDirection.Input;
                paramDiaryAutoUpdate.Value = objDocument.DiaryAutoUpdate;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDiaryAutoUpdate);

                //Add input parameter for (objDocument.DesignerID) and set its properties
                SqlParameter paramDesignerID = new SqlParameter();
                paramDesignerID.ParameterName = "@intDesignerID";
                paramDesignerID.SqlDbType = SqlDbType.Int;
                paramDesignerID.Direction = ParameterDirection.Input;
                paramDesignerID.Value = objDocument.DesignerID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDesignerID);

                //Add input parameter for (objDocument.LastModified) and set its properties
                SqlParameter paramLastModified = new SqlParameter();
                paramLastModified.ParameterName = "@datLastModified";
                paramLastModified.SqlDbType = SqlDbType.DateTime;
                paramLastModified.Direction = ParameterDirection.Input;
                paramLastModified.Value = objDocument.LastModified;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramLastModified);

                //Add input parameter for (objDocument.Active) and set its properties
                SqlParameter paramActive = new SqlParameter();
                paramActive.ParameterName = "@vchrActive";
                paramActive.SqlDbType = SqlDbType.VarChar;
                paramActive.Direction = ParameterDirection.Input;
                paramActive.Value = objDocument.Active;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramActive);

                //Add input parameter for (objDocument.AttachedDocument) and set its properties
                SqlParameter paramAttachedDocument = new SqlParameter();
                paramAttachedDocument.ParameterName = "@vchrAttachedDocument";
                paramAttachedDocument.SqlDbType = SqlDbType.VarChar;
                paramAttachedDocument.Direction = ParameterDirection.Input;
                paramAttachedDocument.Value = objDocument.AttachedDocument;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramAttachedDocument);

                //Add input parameter for (objDocument.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objDocument.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objDocument.Result) and set its properties
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
                    #region Create Document Group

                    //get document id
                    intNewDocumentID = intResult;

                    //reset result
                    intResult = 0;

                    //add document groups
                    for (intIndex = 0; intIndex < objDocument.listDocumentGroup.Count; intIndex++)
                    {

                        //Create the command and set its properties
                        cmdCommand.Parameters.Clear();
                        cmdCommand.Connection = cnnConnection;
                        cmdCommand.Transaction = tranTransaction;
                        cmdCommand.CommandText = "spDocumentGroupCreate";
                        cmdCommand.CommandType = CommandType.StoredProcedure;

                        //add command parameters		
                        //Add input parameter for (objDocumentGroup.DocumentID) and set its properties
                        SqlParameter paramDGDocumentID = new SqlParameter();
                        paramDGDocumentID.ParameterName = "@intDocumentID";
                        paramDGDocumentID.SqlDbType = SqlDbType.Int;
                        paramDGDocumentID.Direction = ParameterDirection.Input;
                        paramDGDocumentID.Value = intNewDocumentID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDGDocumentID);

                        //Add input parameter for (objDocumentGroup.GroupID) and set its properties
                        SqlParameter paramDGGroupID = new SqlParameter();
                        paramDGGroupID.ParameterName = "@intGroupID";
                        paramDGGroupID.SqlDbType = SqlDbType.Int;
                        paramDGGroupID.Direction = ParameterDirection.Input;
                        paramDGGroupID.Value = objDocument.listDocumentGroup[intIndex].GroupID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDGGroupID);

                        //Add input parameter for (objDocumentGroup.IUDateTime) and set its properties
                        SqlParameter paramDGIUDateTime = new SqlParameter();
                        paramDGIUDateTime.ParameterName = "@datIUDateTime";
                        paramDGIUDateTime.SqlDbType = SqlDbType.DateTime;
                        paramDGIUDateTime.Direction = ParameterDirection.Input;
                        paramDGIUDateTime.Value = objDocument.listDocumentGroup[intIndex].IUDateTime;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDGIUDateTime);

                        //Add input parameter for (objDocumentGroup.Result) and set its properties
                        SqlParameter paramDGResult = new SqlParameter();
                        paramDGResult.ParameterName = "@intResult";
                        paramDGResult.SqlDbType = SqlDbType.Int;
                        paramDGResult.Direction = ParameterDirection.InputOutput;
                        paramDGResult.Value = intResult;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDGResult);

                        //execute command
                        cmdCommand.ExecuteNonQuery();
                        //get result
                        //intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());
                    }


                    #endregion
                }//end : if (intResult != -1)


                //check results
                if (intResult != -1)
                {
                    #region Create Document Fields

                    //reset result
                    intResult = 0;

                    //add document fields
                    for (intIndex = 0; intIndex < objDocument.lisDocumentField.Count; intIndex++)
                    {
                        //Create the command and set its properties
                        cmdCommand.Parameters.Clear();
                        cmdCommand.Connection = cnnConnection;
                        cmdCommand.Transaction = tranTransaction;
                        cmdCommand.CommandText = "spDocumentFieldCreate";
                        cmdCommand.CommandType = CommandType.StoredProcedure;

                        //add command parameters		
                        //Add input parameter for (objDocumentField.DocumentFieldID) and set its properties
                        SqlParameter paramDFDocumentFieldID = new SqlParameter();
                        paramDFDocumentFieldID.ParameterName = "@intDocumentFieldID";
                        paramDFDocumentFieldID.SqlDbType = SqlDbType.Int;
                        paramDFDocumentFieldID.Direction = ParameterDirection.Input;
                        paramDFDocumentFieldID.Value = objDocument.lisDocumentField[intIndex].DocumentFieldID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFDocumentFieldID);

                        //Add input parameter for (objDocumentField.DocumentID) and set its properties
                        SqlParameter paramDFDocumentID = new SqlParameter();
                        paramDFDocumentID.ParameterName = "@intDocumentID";
                        paramDFDocumentID.SqlDbType = SqlDbType.Int;
                        paramDFDocumentID.Direction = ParameterDirection.Input;
                        paramDFDocumentID.Value = intNewDocumentID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFDocumentID);

                        //Add input parameter for (objDocumentField.FieldNameIs) and set its properties
                        SqlParameter paramDFFieldNameIs = new SqlParameter();
                        paramDFFieldNameIs.ParameterName = "@vchrFieldNameIs";
                        paramDFFieldNameIs.SqlDbType = SqlDbType.VarChar;
                        paramDFFieldNameIs.Direction = ParameterDirection.Input;
                        paramDFFieldNameIs.Value = objDocument.lisDocumentField[intIndex].FieldNameIs;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFFieldNameIs);

                        //Add input parameter for (objDocumentField.FieldTypeIs) and set its properties
                        SqlParameter paramDFFieldTypeIs = new SqlParameter();
                        paramDFFieldTypeIs.ParameterName = "@vchrFieldTypeIs";
                        paramDFFieldTypeIs.SqlDbType = SqlDbType.VarChar;
                        paramDFFieldTypeIs.Direction = ParameterDirection.Input;
                        paramDFFieldTypeIs.Value = objDocument.lisDocumentField[intIndex].FieldTypeIs;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFFieldTypeIs);

                        //Add input parameter for (objDocumentField.IsFieldRequired) and set its properties
                        SqlParameter paramIsDFFieldRequired = new SqlParameter();
                        paramIsDFFieldRequired.ParameterName = "@vchrIsFieldRequired";
                        paramIsDFFieldRequired.SqlDbType = SqlDbType.VarChar;
                        paramIsDFFieldRequired.Direction = ParameterDirection.Input;
                        paramIsDFFieldRequired.Value = objDocument.lisDocumentField[intIndex].IsFieldRequired;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramIsDFFieldRequired);

                        //Add input parameter for (objDocumentField.FieldDescription) and set its properties
                        SqlParameter paramDFFieldDescription = new SqlParameter();
                        paramDFFieldDescription.ParameterName = "@vchrFieldDescription";
                        paramDFFieldDescription.SqlDbType = SqlDbType.VarChar;
                        paramDFFieldDescription.Direction = ParameterDirection.Input;
                        paramDFFieldDescription.Value = objDocument.lisDocumentField[intIndex].FieldDescription;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFFieldDescription);

                        //Add input parameter for (objDocumentField.IUDateTime) and set its properties
                        SqlParameter paramDFIUDateTime = new SqlParameter();
                        paramDFIUDateTime.ParameterName = "@datIUDateTime";
                        paramDFIUDateTime.SqlDbType = SqlDbType.DateTime;
                        paramDFIUDateTime.Direction = ParameterDirection.Input;
                        paramDFIUDateTime.Value = objDocument.lisDocumentField[intIndex].IUDateTime;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFIUDateTime);

                        //Add input parameter for (objDocumentField.Result) and set its properties
                        SqlParameter paramDFResult = new SqlParameter();
                        paramDFResult.ParameterName = "@intResult";
                        paramDFResult.SqlDbType = SqlDbType.Int;
                        paramDFResult.Direction = ParameterDirection.InputOutput;
                        paramDFResult.Value = intResult;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFResult);

                        //execute command
                        cmdCommand.ExecuteNonQuery();
                        //get result
                        intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());
                    }


                    #endregion
                }//end : if (intResult != -1)

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
                objClaimsLog.MessageIs = "Method : DocumentCreate() ";
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
        }//end method : DocumentCreate

        //define method : DocumentRead
        public Document DocumentRead(Document objDocument, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intResult = 0;
            Document objDocumentIs = new Document();
            DateTime datTryParse = new DateTime();

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
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
                paramDocumentID.Value = objDocument.DocumentID;
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
                    while (rdrReader.Read())
                    {
                        objDocumentIs.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocumentIs.DocumentCode = rdrReader["DocumentCode"].ToString();
                        objDocumentIs.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                        objDocumentIs.ProgramID = int.Parse(rdrReader["ProgramID"].ToString());
                        objDocumentIs.ProgramCode = rdrReader["ProgramCode"].ToString();
                        objDocumentIs.Review = rdrReader["Review"].ToString();
                        objDocumentIs.Description = rdrReader["Description"].ToString();
                        objDocumentIs.TemplateName = rdrReader["TemplateName"].ToString();
                        objDocumentIs.StyleSheetName = rdrReader["StyleSheetName"].ToString();
                        if (DateTime.TryParse(rdrReader["EffectiveDate"].ToString(), out datTryParse) == true)
                        {
                            objDocumentIs.EffectiveDate = DateTime.Parse(rdrReader["EffectiveDate"].ToString());
                        }

                        if (DateTime.TryParse(rdrReader["ExpirationDate"].ToString(), out datTryParse) == true)
                        {
                            objDocumentIs.ExpirationDate = DateTime.Parse(rdrReader["ExpirationDate"].ToString());
                        }

                        objDocumentIs.ProofOfMailing = rdrReader["ProofOfMailing"].ToString();
                        objDocumentIs.DataMatx = rdrReader["DataMatx"].ToString();
                        objDocumentIs.ImportToImageRight = rdrReader["ImportToImageRight"].ToString();
                        objDocumentIs.ImageRightDocumentID = rdrReader["ImageRightDocumentID"].ToString();
                        objDocumentIs.ImageRightDocumentSection = rdrReader["ImageRightDocumentSection"].ToString();
                        objDocumentIs.ImageRightDrawer = rdrReader["ImageRightDrawer"].ToString();
                        objDocumentIs.CopyAgent = rdrReader["CopyAgent"].ToString();
                        objDocumentIs.CopyInsured = rdrReader["CopyInsured"].ToString();
                        objDocumentIs.CopyLienHolder = rdrReader["CopyLienHolder"].ToString();
                        objDocumentIs.CopyFinanceCo = rdrReader["CopyFinanceCo"].ToString();
                        objDocumentIs.CopyAttorney = rdrReader["CopyAttorney"].ToString();

                        if (string.IsNullOrEmpty(rdrReader["DiaryNumberOfDays"].ToString()) == true)
                        {
                            objDocumentIs.DiaryNumberOfDays = 0;
                        }
                        else
                        {
                            objDocumentIs.DiaryNumberOfDays = int.Parse(rdrReader["DiaryNumberOfDays"].ToString());
                        }
                        
                        if (string.IsNullOrEmpty(rdrReader["DesignerID"].ToString()) == true)
                        {
                            objDocumentIs.DesignerID = 0;
                        }
                        else
                        {
                            objDocumentIs.DesignerID = int.Parse(rdrReader["DesignerID"].ToString());
                        }
                        objDocumentIs.LastModified = DateTime.Parse(rdrReader["LastModified"].ToString());
                        objDocumentIs.Active = rdrReader["Active"].ToString();
                        objDocumentIs.AttachedDocument = rdrReader["AttachedDocument"].ToString();
                        objDocumentIs.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
                    }//end : while (rdrReader.Read())


                }//end : if (rdrReader.HasRows == true)

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
                objClaimsLog.MessageIs = "Method : DocumentRead() ";
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
            return (objDocumentIs);
        }//end method : DocumentRead

        //define method : DocumentUpdate
        public int DocumentUpdate(Document objDocument, string strConnectionString)
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
                tranTransaction = cnnConnection.BeginTransaction("CDUpdateDoc");

                #region Clear Document Groups

                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.Transaction = tranTransaction;
                cmdCommand.CommandText = "spDocumentGroupClear";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocument.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocument.DocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentID);

                //Add input parameter for (objDocument.Result) and set its properties
                SqlParameter paramDCResult = new SqlParameter();
                paramDCResult.ParameterName = "@intResult";
                paramDCResult.SqlDbType = SqlDbType.Int;
                paramDCResult.Direction = ParameterDirection.InputOutput;
                paramDCResult.Value = intResult;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDCResult);

                //execute command
                cmdCommand.ExecuteNonQuery();
                //get result
                intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());

                #endregion


                if (intResult != -1)
                {
                    #region Clear Document Fields

                    //Create the command and set its properties
                    cmdCommand.Parameters.Clear();
                    cmdCommand.Connection = cnnConnection;
                    cmdCommand.Transaction = tranTransaction;
                    cmdCommand.CommandText = "spDocumentFieldClear";
                    cmdCommand.CommandType = CommandType.StoredProcedure;

                    //add command parameters		
                    //Add input parameter for (objDocument.DocumentID) and set its properties
                    SqlParameter paramDFLDDocumentID = new SqlParameter();
                    paramDFLDDocumentID.ParameterName = "@intDocumentID";
                    paramDFLDDocumentID.SqlDbType = SqlDbType.Int;
                    paramDFLDDocumentID.Direction = ParameterDirection.Input;
                    paramDFLDDocumentID.Value = objDocument.DocumentID;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDFLDDocumentID);

                    //Add input parameter for (objDocument.Result) and set its properties
                    SqlParameter paramDFLDResult = new SqlParameter();
                    paramDFLDResult.ParameterName = "@intResult";
                    paramDFLDResult.SqlDbType = SqlDbType.Int;
                    paramDFLDResult.Direction = ParameterDirection.InputOutput;
                    paramDFLDResult.Value = intResult;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDFLDResult);

                    //execute command
                    cmdCommand.ExecuteNonQuery();
                    //get result
                    intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());

                    #endregion
                }

                //check results
                if (intResult != -1)
                {
                    #region Update Document

                    //Create the command and set its properties
                    cmdCommand.Parameters.Clear();
                    cmdCommand.Connection = cnnConnection;
                    cmdCommand.Transaction = tranTransaction;
                    cmdCommand.CommandText = "spDocumentUpdate";
                    cmdCommand.CommandType = CommandType.StoredProcedure;

                    //add command parameters		
                    //Add input parameter for (objDocument.DocumentID) and set its properties
                    SqlParameter paramDCDocumentID = new SqlParameter();
                    paramDCDocumentID.ParameterName = "@intDocumentID";
                    paramDCDocumentID.SqlDbType = SqlDbType.Int;
                    paramDCDocumentID.Direction = ParameterDirection.Input;
                    paramDCDocumentID.Value = objDocument.DocumentID;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDCDocumentID);

                    //Add input parameter for (objDocument.DocumentCode) and set its properties
                    SqlParameter paramDCDocumentCode = new SqlParameter();
                    paramDCDocumentCode.ParameterName = "@vchrDocumentCode";
                    paramDCDocumentCode.SqlDbType = SqlDbType.VarChar;
                    paramDCDocumentCode.Direction = ParameterDirection.Input;
                    paramDCDocumentCode.Value = objDocument.DocumentCode;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDCDocumentCode);

                    //Add input parameter for (objDocument.DepartmentID) and set its properties
                    SqlParameter paramDepartmentID = new SqlParameter();
                    paramDepartmentID.ParameterName = "@intDepartmentID";
                    paramDepartmentID.SqlDbType = SqlDbType.Int;
                    paramDepartmentID.Direction = ParameterDirection.Input;
                    paramDepartmentID.Value = objDocument.DepartmentID;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDepartmentID);

                    //Add input parameter for (objDocument.ProgramID) and set its properties
                    SqlParameter paramProgramID = new SqlParameter();
                    paramProgramID.ParameterName = "@intProgramID";
                    paramProgramID.SqlDbType = SqlDbType.Int;
                    paramProgramID.Direction = ParameterDirection.Input;
                    paramProgramID.Value = objDocument.ProgramID;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramProgramID);

                    //Add input parameter for (objDocument.Review) and set its properties
                    SqlParameter paramReview = new SqlParameter();
                    paramReview.ParameterName = "@vchrReview";
                    paramReview.SqlDbType = SqlDbType.VarChar;
                    paramReview.Direction = ParameterDirection.Input;
                    paramReview.Value = objDocument.Review;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramReview);

                    //Add input parameter for (objDocument.Description) and set its properties
                    SqlParameter paramDescription = new SqlParameter();
                    paramDescription.ParameterName = "@vchrDescription";
                    paramDescription.SqlDbType = SqlDbType.VarChar;
                    paramDescription.Direction = ParameterDirection.Input;
                    paramDescription.Value = objDocument.Description;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDescription);

                    //Add input parameter for (objDocument.TemplateName) and set its properties
                    SqlParameter paramTemplateName = new SqlParameter();
                    paramTemplateName.ParameterName = "@vchrTemplateName";
                    paramTemplateName.SqlDbType = SqlDbType.VarChar;
                    paramTemplateName.Direction = ParameterDirection.Input;
                    paramTemplateName.Value = objDocument.TemplateName;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramTemplateName);

                    //Add input parameter for (objDocument.StyleSheetName) and set its properties
                    SqlParameter paramStyleSheetName = new SqlParameter();
                    paramStyleSheetName.ParameterName = "@vchrStyleSheetName";
                    paramStyleSheetName.SqlDbType = SqlDbType.VarChar;
                    paramStyleSheetName.Direction = ParameterDirection.Input;
                    paramStyleSheetName.Value = objDocument.StyleSheetName;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramStyleSheetName);

                    //Add input parameter for (objDocument.EffectiveDate) and set its properties
                    SqlParameter paramEffectiveDate = new SqlParameter();
                    paramEffectiveDate.ParameterName = "@datEffectiveDate";
                    paramEffectiveDate.SqlDbType = SqlDbType.DateTime;
                    paramEffectiveDate.Direction = ParameterDirection.Input;
                    paramEffectiveDate.Value = objDocument.EffectiveDate;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramEffectiveDate);

                    //Add input parameter for (objDocument.ExpirationDate) and set its properties
                    SqlParameter paramExpirationDate = new SqlParameter();
                    paramExpirationDate.ParameterName = "@datExpirationDate";
                    paramExpirationDate.SqlDbType = SqlDbType.DateTime;
                    paramExpirationDate.Direction = ParameterDirection.Input;
                    paramExpirationDate.Value = objDocument.ExpirationDate;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramExpirationDate);

                    //Add input parameter for (objDocument.ProofOfMailing) and set its properties
                    SqlParameter paramProofOfMailing = new SqlParameter();
                    paramProofOfMailing.ParameterName = "@vchrProofOfMailing";
                    paramProofOfMailing.SqlDbType = SqlDbType.VarChar;
                    paramProofOfMailing.Direction = ParameterDirection.Input;
                    paramProofOfMailing.Value = objDocument.ProofOfMailing;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramProofOfMailing);

                    //Add input parameter for (objDocument.DataMatx) and set its properties
                    SqlParameter paramDataMatx = new SqlParameter();
                    paramDataMatx.ParameterName = "@vchrDataMatx";
                    paramDataMatx.SqlDbType = SqlDbType.VarChar;
                    paramDataMatx.Direction = ParameterDirection.Input;
                    paramDataMatx.Value = objDocument.DataMatx;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDataMatx);

                    //Add input parameter for (objDocument.ImportToImageRight) and set its properties
                    SqlParameter paramImportToImageRight = new SqlParameter();
                    paramImportToImageRight.ParameterName = "@vchrImportToImageRight";
                    paramImportToImageRight.SqlDbType = SqlDbType.VarChar;
                    paramImportToImageRight.Direction = ParameterDirection.Input;
                    paramImportToImageRight.Value = objDocument.ImportToImageRight;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramImportToImageRight);

                    //Add input parameter for (objDocument.ImageRightDocumentID) and set its properties
                    SqlParameter paramImageRightDocumentID = new SqlParameter();
                    paramImageRightDocumentID.ParameterName = "@vchrImageRightDocumentID";
                    paramImageRightDocumentID.SqlDbType = SqlDbType.VarChar;
                    paramImageRightDocumentID.Direction = ParameterDirection.Input;
                    paramImageRightDocumentID.Value = objDocument.ImageRightDocumentID;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramImageRightDocumentID);

                    //Add input parameter for (objDocument.ImageRightDocumentSection) and set its properties
                    SqlParameter paramImageRightDocumentSection = new SqlParameter();
                    paramImageRightDocumentSection.ParameterName = "@vchrImageRightDocumentSection";
                    paramImageRightDocumentSection.SqlDbType = SqlDbType.VarChar;
                    paramImageRightDocumentSection.Direction = ParameterDirection.Input;
                    paramImageRightDocumentSection.Value = objDocument.ImageRightDocumentSection;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramImageRightDocumentSection);

                    //Add input parameter for (objDocument.ImageRightDrawer) and set its properties
                    SqlParameter paramImageRightDrawer = new SqlParameter();
                    paramImageRightDrawer.ParameterName = "@vchrImageRightDrawer";
                    paramImageRightDrawer.SqlDbType = SqlDbType.VarChar;
                    paramImageRightDrawer.Direction = ParameterDirection.Input;
                    paramImageRightDrawer.Value = objDocument.ImageRightDrawer;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramImageRightDrawer);

                    //Add input parameter for (objDocument.CopyAgent) and set its properties
                    SqlParameter paramContactNo = new SqlParameter();
                    paramContactNo.ParameterName = "@intContactNo";
                    paramContactNo.SqlDbType = SqlDbType.Int;
                    paramContactNo.Direction = ParameterDirection.Input;
                    paramContactNo.Value = objDocument.ContactNo;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramContactNo);

                    //Add input parameter for (objDocument.CopyAgent) and set its properties
                    SqlParameter paramContactType = new SqlParameter();
                    paramContactType.ParameterName = "@intContactType";
                    paramContactType.SqlDbType = SqlDbType.Int;
                    paramContactType.Direction = ParameterDirection.Input;
                    paramContactType.Value = objDocument.ContactType;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramContactType);

                    //Add input parameter for (objDocument.CopyAgent) and set its properties
                    SqlParameter paramCopyAgent = new SqlParameter();
                    paramCopyAgent.ParameterName = "@vchrCopyAgent";
                    paramCopyAgent.SqlDbType = SqlDbType.VarChar;
                    paramCopyAgent.Direction = ParameterDirection.Input;
                    paramCopyAgent.Value = objDocument.CopyAgent;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramCopyAgent);

                    //Add input parameter for (objDocument.CopyInsured) and set its properties
                    SqlParameter paramCopyInsured = new SqlParameter();
                    paramCopyInsured.ParameterName = "@vchrCopyInsured";
                    paramCopyInsured.SqlDbType = SqlDbType.VarChar;
                    paramCopyInsured.Direction = ParameterDirection.Input;
                    paramCopyInsured.Value = objDocument.CopyInsured;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramCopyInsured);

                    //Add input parameter for (objDocument.CopyLienHolder) and set its properties
                    SqlParameter paramCopyLienHolder = new SqlParameter();
                    paramCopyLienHolder.ParameterName = "@vchrCopyLienHolder";
                    paramCopyLienHolder.SqlDbType = SqlDbType.VarChar;
                    paramCopyLienHolder.Direction = ParameterDirection.Input;
                    paramCopyLienHolder.Value = objDocument.CopyLienHolder;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramCopyLienHolder);

                    //Add input parameter for (objDocument.CopyFinanceCo) and set its properties
                    SqlParameter paramCopyFinanceCo = new SqlParameter();
                    paramCopyFinanceCo.ParameterName = "@vchrCopyFinanceCo";
                    paramCopyFinanceCo.SqlDbType = SqlDbType.VarChar;
                    paramCopyFinanceCo.Direction = ParameterDirection.Input;
                    paramCopyFinanceCo.Value = objDocument.CopyFinanceCo;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramCopyFinanceCo);

                    //Add input parameter for (objDocument.CopyAttorney) and set its properties
                    SqlParameter paramCopyAttorney = new SqlParameter();
                    paramCopyAttorney.ParameterName = "@vchrCopyAttorney";
                    paramCopyAttorney.SqlDbType = SqlDbType.VarChar;
                    paramCopyAttorney.Direction = ParameterDirection.Input;
                    paramCopyAttorney.Value = objDocument.CopyAttorney;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramCopyAttorney);

                    //Add input parameter for (objDocument.DiaryNumberOfDays) and set its properties
                    SqlParameter paramDiaryNumberOfDays = new SqlParameter();
                    paramDiaryNumberOfDays.ParameterName = "@intDiaryNumberOfDays";
                    paramDiaryNumberOfDays.SqlDbType = SqlDbType.Int;
                    paramDiaryNumberOfDays.Direction = ParameterDirection.Input;
                    paramDiaryNumberOfDays.Value = objDocument.DiaryNumberOfDays;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDiaryNumberOfDays);

                    //Add input parameter for (objDocument.DiaryAutoUpdate) and set its properties
                    SqlParameter paramDiaryAutoUpdate = new SqlParameter();
                    paramDiaryAutoUpdate.ParameterName = "@vchrDiaryAutoUpdate";
                    paramDiaryAutoUpdate.SqlDbType = SqlDbType.VarChar;
                    paramDiaryAutoUpdate.Direction = ParameterDirection.Input;
                    paramDiaryAutoUpdate.Value = objDocument.DiaryAutoUpdate;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDiaryAutoUpdate);

                    //Add input parameter for (objDocument.DesignerID) and set its properties
                    SqlParameter paramDesignerID = new SqlParameter();
                    paramDesignerID.ParameterName = "@intDesignerID";
                    paramDesignerID.SqlDbType = SqlDbType.Int;
                    paramDesignerID.Direction = ParameterDirection.Input;
                    paramDesignerID.Value = objDocument.DesignerID;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramDesignerID);

                    //Add input parameter for (objDocument.LastModified) and set its properties
                    SqlParameter paramLastModified = new SqlParameter();
                    paramLastModified.ParameterName = "@datLastModified";
                    paramLastModified.SqlDbType = SqlDbType.DateTime;
                    paramLastModified.Direction = ParameterDirection.Input;
                    paramLastModified.Value = objDocument.LastModified;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramLastModified);

                    //Add input parameter for (objDocument.Active) and set its properties
                    SqlParameter paramActive = new SqlParameter();
                    paramActive.ParameterName = "@vchrActive";
                    paramActive.SqlDbType = SqlDbType.VarChar;
                    paramActive.Direction = ParameterDirection.Input;
                    paramActive.Value = objDocument.Active;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramActive);

                    //Add input parameter for (objDocument.AttachedDocument) and set its properties
                    SqlParameter paramAttachedDocument = new SqlParameter();
                    paramAttachedDocument.ParameterName = "@vchrAttachedDocument";
                    paramAttachedDocument.SqlDbType = SqlDbType.VarChar;
                    paramAttachedDocument.Direction = ParameterDirection.Input;
                    paramAttachedDocument.Value = objDocument.AttachedDocument;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramAttachedDocument);

                    //Add input parameter for (objDocument.IUDateTime) and set its properties
                    SqlParameter paramIUDateTime = new SqlParameter();
                    paramIUDateTime.ParameterName = "@datIUDateTime";
                    paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                    paramIUDateTime.Direction = ParameterDirection.Input;
                    paramIUDateTime.Value = objDocument.IUDateTime;
                    //Add the parameter to the commands parameter collection
                    cmdCommand.Parameters.Add(paramIUDateTime);

                    //Add input parameter for (objDocument.Result) and set its properties
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

                }//end : if(intResult!=-1)
                //check results
                if (intResult != -1)
                {
                    #region Create Document Group

                    //reset result
                    intResult = 0;

                    //add document groups
                    for (intIndex = 0; intIndex < objDocument.listDocumentGroup.Count; intIndex++)
                    {

                        //Create the command and set its properties
                        cmdCommand.Parameters.Clear();
                        cmdCommand.Connection = cnnConnection;
                        cmdCommand.Transaction = tranTransaction;
                        cmdCommand.CommandText = "spDocumentGroupCreate";
                        cmdCommand.CommandType = CommandType.StoredProcedure;

                        //add command parameters		
                        //Add input parameter for (objDocumentGroup.DocumentID) and set its properties
                        SqlParameter paramDGDocumentID = new SqlParameter();
                        paramDGDocumentID.ParameterName = "@intDocumentID";
                        paramDGDocumentID.SqlDbType = SqlDbType.Int;
                        paramDGDocumentID.Direction = ParameterDirection.Input;
                        paramDGDocumentID.Value = objDocument.DocumentID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDGDocumentID);

                        //Add input parameter for (objDocumentGroup.GroupID) and set its properties
                        SqlParameter paramDGGroupID = new SqlParameter();
                        paramDGGroupID.ParameterName = "@intGroupID";
                        paramDGGroupID.SqlDbType = SqlDbType.Int;
                        paramDGGroupID.Direction = ParameterDirection.Input;
                        paramDGGroupID.Value = objDocument.listDocumentGroup[intIndex].GroupID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDGGroupID);

                        //Add input parameter for (objDocumentGroup.IUDateTime) and set its properties
                        SqlParameter paramDGIUDateTime = new SqlParameter();
                        paramDGIUDateTime.ParameterName = "@datIUDateTime";
                        paramDGIUDateTime.SqlDbType = SqlDbType.DateTime;
                        paramDGIUDateTime.Direction = ParameterDirection.Input;
                        paramDGIUDateTime.Value = objDocument.listDocumentGroup[intIndex].IUDateTime;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDGIUDateTime);

                        //Add input parameter for (objDocumentGroup.Result) and set its properties
                        SqlParameter paramDGResult = new SqlParameter();
                        paramDGResult.ParameterName = "@intResult";
                        paramDGResult.SqlDbType = SqlDbType.Int;
                        paramDGResult.Direction = ParameterDirection.InputOutput;
                        paramDGResult.Value = intResult;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDGResult);

                        //execute command
                        cmdCommand.ExecuteNonQuery();
                        //get result
                        //intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());
                    }


                    #endregion
                }//end : if (intResult != -1)


                //check results
                if (intResult != -1)
                {
                    #region Create Document Fields

                    //reset result
                    intResult = 0;

                    //add document fields
                    for (intIndex = 0; intIndex < objDocument.lisDocumentField.Count; intIndex++)
                    {
                        //Create the command and set its properties
                        cmdCommand.Parameters.Clear();
                        cmdCommand.Connection = cnnConnection;
                        cmdCommand.Transaction = tranTransaction;
                        cmdCommand.CommandText = "spDocumentFieldCreate";
                        cmdCommand.CommandType = CommandType.StoredProcedure;

                        //add command parameters		
                        //Add input parameter for (objDocumentField.DocumentFieldID) and set its properties
                        SqlParameter paramDFDocumentFieldID = new SqlParameter();
                        paramDFDocumentFieldID.ParameterName = "@intDocumentFieldID";
                        paramDFDocumentFieldID.SqlDbType = SqlDbType.Int;
                        paramDFDocumentFieldID.Direction = ParameterDirection.Input;
                        paramDFDocumentFieldID.Value = objDocument.lisDocumentField[intIndex].DocumentFieldID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFDocumentFieldID);

                        //Add input parameter for (objDocumentField.DocumentID) and set its properties
                        SqlParameter paramDFDocumentID = new SqlParameter();
                        paramDFDocumentID.ParameterName = "@intDocumentID";
                        paramDFDocumentID.SqlDbType = SqlDbType.Int;
                        paramDFDocumentID.Direction = ParameterDirection.Input;
                        paramDFDocumentID.Value = objDocument.DocumentID;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFDocumentID);

                        //Add input parameter for (objDocumentField.FieldNameIs) and set its properties
                        SqlParameter paramDFFieldNameIs = new SqlParameter();
                        paramDFFieldNameIs.ParameterName = "@vchrFieldNameIs";
                        paramDFFieldNameIs.SqlDbType = SqlDbType.VarChar;
                        paramDFFieldNameIs.Direction = ParameterDirection.Input;
                        paramDFFieldNameIs.Value = objDocument.lisDocumentField[intIndex].FieldNameIs;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFFieldNameIs);

                        //Add input parameter for (objDocumentField.FieldTypeIs) and set its properties
                        SqlParameter paramDFFieldTypeIs = new SqlParameter();
                        paramDFFieldTypeIs.ParameterName = "@vchrFieldTypeIs";
                        paramDFFieldTypeIs.SqlDbType = SqlDbType.VarChar;
                        paramDFFieldTypeIs.Direction = ParameterDirection.Input;
                        paramDFFieldTypeIs.Value = objDocument.lisDocumentField[intIndex].FieldTypeIs;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFFieldTypeIs);

                        //Add input parameter for (objDocumentField.IsFieldRequired) and set its properties
                        SqlParameter paramIsDFFieldRequired = new SqlParameter();
                        paramIsDFFieldRequired.ParameterName = "@vchrIsFieldRequired";
                        paramIsDFFieldRequired.SqlDbType = SqlDbType.VarChar;
                        paramIsDFFieldRequired.Direction = ParameterDirection.Input;
                        paramIsDFFieldRequired.Value = objDocument.lisDocumentField[intIndex].IsFieldRequired;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramIsDFFieldRequired);

                        //Add input parameter for (objDocumentField.FieldDescription) and set its properties
                        SqlParameter paramDFFieldDescription = new SqlParameter();
                        paramDFFieldDescription.ParameterName = "@vchrFieldDescription";
                        paramDFFieldDescription.SqlDbType = SqlDbType.VarChar;
                        paramDFFieldDescription.Direction = ParameterDirection.Input;
                        paramDFFieldDescription.Value = objDocument.lisDocumentField[intIndex].FieldDescription;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFFieldDescription);

                        //Add input parameter for (objDocumentField.IUDateTime) and set its properties
                        SqlParameter paramDFIUDateTime = new SqlParameter();
                        paramDFIUDateTime.ParameterName = "@datIUDateTime";
                        paramDFIUDateTime.SqlDbType = SqlDbType.DateTime;
                        paramDFIUDateTime.Direction = ParameterDirection.Input;
                        paramDFIUDateTime.Value = objDocument.lisDocumentField[intIndex].IUDateTime;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFIUDateTime);

                        //Add input parameter for (objDocumentField.Result) and set its properties
                        SqlParameter paramDFResult = new SqlParameter();
                        paramDFResult.ParameterName = "@intResult";
                        paramDFResult.SqlDbType = SqlDbType.Int;
                        paramDFResult.Direction = ParameterDirection.InputOutput;
                        paramDFResult.Value = intResult;
                        //Add the parameter to the commands parameter collection
                        cmdCommand.Parameters.Add(paramDFResult);

                        //execute command
                        cmdCommand.ExecuteNonQuery();
                        //get result
                        intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());
                    }

                    #endregion
                }//end : if (intResult != -1)

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
                objClaimsLog.MessageIs = "Method : DocumentUpdate() ";
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
        }//end method : DocumentUpdate

        //define method : DocumentDelete
        public int DocumentDelete(Document objDocument, string strConnectionString)
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
                cmdCommand.CommandText = "spDocumentDelete";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocument.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocument.DocumentID;
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
                objClaimsLog.MessageIs = "Method : DocumentDelete() ";
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
        }//end method : DocumentDelete

        //define method : DocumentSearch
        public List<Document> DocumentSearch(string strSQLString, string strConnectionString)
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

                //fill document
                if (rdrReader.HasRows == true)
                {
                    while (rdrReader.Read())
                    {
                        //create new document object
                        Document objDocumentIs = new Document();
                        objDocumentIs.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocumentIs.DocumentCode = rdrReader["DocumentCode"].ToString();
                        objDocumentIs.DepartmentID = int.Parse(rdrReader["DepartmentID"].ToString());
                        objDocumentIs.ProgramID = int.Parse(rdrReader["ProgramID"].ToString());
                        objDocumentIs.ProgramCode = rdrReader["ProgramCode"].ToString();
                        objDocumentIs.Review = rdrReader["Review"].ToString();
                        objDocumentIs.Description = rdrReader["Description"].ToString();
                        objDocumentIs.TemplateName = rdrReader["TemplateName"].ToString();
                        objDocumentIs.StyleSheetName = rdrReader["StyleSheetName"].ToString();

                        if (string.IsNullOrEmpty(rdrReader["EffectiveDate"].ToString()) == false)
                        {
                            objDocumentIs.EffectiveDate = DateTime.Parse(rdrReader["EffectiveDate"].ToString());
                        }

                        if (string.IsNullOrEmpty(rdrReader["ExpirationDate"].ToString()) == false)
                        {
                            objDocumentIs.ExpirationDate = DateTime.Parse(rdrReader["ExpirationDate"].ToString());
                        }
                        objDocumentIs.ProofOfMailing = rdrReader["ProofOfMailing"].ToString();
                        objDocumentIs.DataMatx = rdrReader["DataMatx"].ToString();
                        objDocumentIs.ImportToImageRight = rdrReader["ImportToImageRight"].ToString();
                        objDocumentIs.ImageRightDocumentID = rdrReader["ImageRightDocumentID"].ToString();
                        objDocumentIs.ImageRightDocumentSection = rdrReader["ImageRightDocumentSection"].ToString();
                        objDocumentIs.ImageRightDrawer = rdrReader["ImageRightDrawer"].ToString();
                        objDocumentIs.ContactNo = int.Parse(rdrReader["ContactNo"].ToString());
                        objDocumentIs.ContactType = int.Parse(rdrReader["ContactType"].ToString());
                        objDocumentIs.CopyAgent = rdrReader["CopyAgent"].ToString();
                        objDocumentIs.CopyInsured = rdrReader["CopyInsured"].ToString();
                        objDocumentIs.CopyLienHolder = rdrReader["CopyLienHolder"].ToString();
                        objDocumentIs.CopyFinanceCo = rdrReader["CopyFinanceCo"].ToString();
                        objDocumentIs.CopyAttorney = rdrReader["CopyAttorney"].ToString();
                        if (string.IsNullOrEmpty(rdrReader["DiaryNumberOfDays"].ToString()) == false)
                        {
                            objDocumentIs.DiaryNumberOfDays = int.Parse(rdrReader["DiaryNumberOfDays"].ToString());
                        }
                        else
                        {
                            objDocumentIs.DiaryNumberOfDays = 0;
                        }
                        objDocumentIs.DiaryAutoUpdate = rdrReader["DiaryAutoUpdate"].ToString();
                        objDocumentIs.DesignerID = int.Parse(rdrReader["DesignerID"].ToString());
                        if (string.IsNullOrEmpty(rdrReader["LastModified"].ToString()) == false)
                        {
                            objDocumentIs.LastModified = DateTime.Parse(rdrReader["LastModified"].ToString());
                        }
                        objDocumentIs.Active = rdrReader["Active"].ToString();
                        objDocumentIs.AttachedDocument = rdrReader["AttachedDocument"].ToString();
                        objDocumentIs.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());

                        this._listDocument.Add(objDocumentIs);
                    }//end : while (rdrReader.Read())
                }//end : if (rdrReader.HasRows == true)

            }//end try
            catch (Exception ex)
            {
                CDSupport.LogExceptionToFile("DocumentSearch", ex, strConnectionString, false);

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
            return (this._listDocument);
        }//end method : DocumentSearch

        //define method : DocumentGroupSearch
        public List<DocumentGroup> DocumentGroupSearch(string strSQLString, string strConnectionString)
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

                //fill document
                if (rdrReader.HasRows == true)
                {
                    while (rdrReader.Read())
                    {
                        //create new document object
                        DocumentGroup objDocumentGroup = new DocumentGroup();
                        objDocumentGroup.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocumentGroup.GroupID = int.Parse(rdrReader["GroupID"].ToString());
                        objDocumentGroup.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
                        this._listDocumentGroup.Add(objDocumentGroup);
                    }//end : while (rdrReader.Read())
                }//end : if (rdrReader.HasRows == true)

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
                objClaimsLog.MessageIs = "Method : DocumentGroupSearch() ";
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
            return (this._listDocumentGroup);
        }//end method : DocumentSearch

        //define method : DocumentGroupClear
        public int DocumentGroupClear(Document objDocument, string strConnectionString)
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
                cmdCommand.CommandText = "[spDocumentGroupClear]";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (DocumentField.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocument.DocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentID);

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
                objClaimsLog.MessageIs = "Method : DocumentGroupClear() ";
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
        }//end : DocumentGroupClear

        //define method : DocumentFieldSearch
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

                //fill document
                if (rdrReader.HasRows == true)
                {
                    while (rdrReader.Read())
                    {
                        //create new document object
                        DocumentField objDocumentField = new DocumentField();
                        objDocumentField.DocumentFieldID = int.Parse(rdrReader["DocumentFieldID"].ToString());
                        objDocumentField.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocumentField.FieldNameIs = rdrReader["FieldNameIs"].ToString();
                        objDocumentField.FieldTypeIs = rdrReader["FieldTypeIs"].ToString();
                        objDocumentField.IsFieldRequired = rdrReader["IsFieldRequired"].ToString();
                        objDocumentField.FieldDescription = rdrReader["FieldDescription"].ToString();
                        objDocumentField.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());

                        this._listDocumentField.Add(objDocumentField);
                    }//end : while (rdrReader.Read())
                }//end : if (rdrReader.HasRows == true)

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
            return (this._listDocumentField);
        }//end method : DocumentFieldSearch

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
                //Add input parameter for (DocumentField.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocumentField.DocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentID);

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

        //define method : DocumentAdjusterListSearch
        public List<Document> DocumentAdjusterListSearch(string strSQLString, string strConnectionString)
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

                //fill document
                if (rdrReader.HasRows == true)
                {
                    while (rdrReader.Read())
                    {
                        //create new document object
                        Document objDocumentIs = new Document();
                        objDocumentIs.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocumentIs.DocumentCode = rdrReader["DocumentCode"].ToString();
                        objDocumentIs.Description = rdrReader["Description"].ToString();
                        objDocumentIs.AdditionalDataRequired = rdrReader["AdditionalDataRequired"].ToString();
                        objDocumentIs.DiaryAutoUpdate = rdrReader["ProofOfMailing"].ToString();
                        objDocumentIs.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());

                        this._listDocument.Add(objDocumentIs);
                    }//end : while (rdrReader.Read())
                }//end : if (rdrReader.HasRows == true)

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
                objClaimsLog.MessageIs = "Method : DocumentAdjusterListSearch() ";
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
            return (this._listDocument);
        }//end : DocumentAdjusterListSearch
        
        //define method : DocumentLogCreate
        public int DocumentLogCreate(DocumentLog objDocumentLog, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            int intResult=0;

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //open connection
                cnnConnection.Open();

                #region Create DocumentLog

                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDocumentLogCreate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocumentLog.InstanceID) and set its properties
                SqlParameter paramInstanceID = new SqlParameter();
                paramInstanceID.ParameterName = "@vchrInstanceID";
                paramInstanceID.SqlDbType = SqlDbType.VarChar;
                paramInstanceID.Direction = ParameterDirection.Input;
                paramInstanceID.Value = objDocumentLog.InstanceID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramInstanceID);

                //Add input parameter for (objDocumentLog.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocumentLog.DocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentID);

                //Add input parameter for (objDocumentLog.SubmitterID) and set its properties
                SqlParameter paramSubmitterID = new SqlParameter();
                paramSubmitterID.ParameterName = "@intSubmitterID";
                paramSubmitterID.SqlDbType = SqlDbType.Int;
                paramSubmitterID.Direction = ParameterDirection.Input;
                paramSubmitterID.Value = objDocumentLog.SubmitterID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramSubmitterID);

                //Add input parameter for (objDocumentLog.DateSubmitted) and set its properties
                SqlParameter paramDateSubmitted = new SqlParameter();
                paramDateSubmitted.ParameterName = "@datDateSubmitted";
                paramDateSubmitted.SqlDbType = SqlDbType.DateTime;
                paramDateSubmitted.Direction = ParameterDirection.Input;
                paramDateSubmitted.Value = objDocumentLog.DateSubmitted;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDateSubmitted);

                //Add input parameter for (objDocumentLog.ApprovalRequired) and set its properties
                SqlParameter paramApprovalRequired = new SqlParameter();
                paramApprovalRequired.ParameterName = "@vchrApprovalRequired";
                paramApprovalRequired.SqlDbType = SqlDbType.VarChar;
                paramApprovalRequired.Direction = ParameterDirection.Input;
                paramApprovalRequired.Value = objDocumentLog.ApprovalRequired;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramApprovalRequired);

                //Add input parameter for (objDocumentLog.ApproverID) and set its properties
                SqlParameter paramApproverID = new SqlParameter();
                paramApproverID.ParameterName = "@intApproverID";
                paramApproverID.SqlDbType = SqlDbType.Int;
                paramApproverID.Direction = ParameterDirection.Input;
                paramApproverID.Value = objDocumentLog.ApproverID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramApproverID);

                //Add input parameter for (objDocumentLog.DateApproved) and set its properties
                SqlParameter paramDateApproved = new SqlParameter();
                paramDateApproved.ParameterName = "@datDateApproved";
                paramDateApproved.SqlDbType = SqlDbType.DateTime;
                paramDateApproved.Direction = ParameterDirection.Input;
                paramDateApproved.Value = objDocumentLog.DateApproved;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDateApproved);

                //Add input parameter for (objDocumentLog.DeclinerID) and set its properties
                SqlParameter paramDeclinerID = new SqlParameter();
                paramDeclinerID.ParameterName = "@intDeclinerID";
                paramDeclinerID.SqlDbType = SqlDbType.Int;
                paramDeclinerID.Direction = ParameterDirection.Input;
                paramDeclinerID.Value = objDocumentLog.DeclinerID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDeclinerID);

                //Add input parameter for (objDocumentLog.DateDeclined) and set its properties
                SqlParameter paramDateDeclined = new SqlParameter();
                paramDateDeclined.ParameterName = "@datDateDeclined";
                paramDateDeclined.SqlDbType = SqlDbType.DateTime;
                paramDateDeclined.Direction = ParameterDirection.Input;
                paramDateDeclined.Value = objDocumentLog.DateDeclined;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDateDeclined);

                //Add input parameter for (objDocumentLog.ReasonDeclined) and set its properties
                SqlParameter paramReasonDeclined = new SqlParameter();
                paramReasonDeclined.ParameterName = "@vchrReasonDeclined";
                paramReasonDeclined.SqlDbType = SqlDbType.VarChar;
                paramReasonDeclined.Direction = ParameterDirection.Input;
                paramReasonDeclined.Value = objDocumentLog.ReasonDeclined;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramReasonDeclined);

                //Add input parameter for (objDocumentLog.DateGenerated) and set its properties
                SqlParameter paramDateGenerated = new SqlParameter();
                paramDateGenerated.ParameterName = "@datDateGenerated";
                paramDateGenerated.SqlDbType = SqlDbType.DateTime;
                paramDateGenerated.Direction = ParameterDirection.Input;
                paramDateGenerated.Value = objDocumentLog.DateGenerated;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDateGenerated);

                //Add input parameter for (objDocumentLog.GeneratedErrorCode) and set its properties
                SqlParameter paramGeneratedErrorCode = new SqlParameter();
                paramGeneratedErrorCode.ParameterName = "@vchrGeneratedErrorCode";
                paramGeneratedErrorCode.SqlDbType = SqlDbType.VarChar;
                paramGeneratedErrorCode.Direction = ParameterDirection.Input;
                paramGeneratedErrorCode.Value = objDocumentLog.GeneratedErrorCode;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGeneratedErrorCode);

                //Add input parameter for (objDocumentLog.PolicyNo) and set its properties
                SqlParameter paramPolicyNo = new SqlParameter();
                paramPolicyNo.ParameterName = "@vchrPolicyNo";
                paramPolicyNo.SqlDbType = SqlDbType.VarChar;
                paramPolicyNo.Direction = ParameterDirection.Input;
                paramPolicyNo.Value = objDocumentLog.PolicyNo;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNo);

                //Add input parameter for (objDocumentLog.ClaimNo) and set its properties
                SqlParameter paramClaimNo = new SqlParameter();
                paramClaimNo.ParameterName = "@vchrClaimNo";
                paramClaimNo.SqlDbType = SqlDbType.VarChar;
                paramClaimNo.Direction = ParameterDirection.Input;
                paramClaimNo.Value = objDocumentLog.ClaimNo;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimNo);

                //Add input parameter for (objDocumentLog.GroupName) and set its properties
                SqlParameter paramGroupName = new SqlParameter();
                paramGroupName.ParameterName = "@vchrGroupName";
                paramGroupName.SqlDbType = SqlDbType.VarChar;
                paramGroupName.Direction = ParameterDirection.Input;
                paramGroupName.Value = objDocumentLog.GroupName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGroupName);

                //Add input parameter for (objDocumentLog.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objDocumentLog.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add output parameter for (objDocumentLog.Result) and set its properties
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
                cmdCommand.Parameters["@intResult"].Value.ToString();
                //intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());

                #endregion


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
                objClaimsLog.MessageIs = "Method : DocumentLogCreate() ";
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
        }//end method : DocumentLogCreate

        //define method : DocumentLogUpdate
        public int DocumentLogUpdate(DocumentLog objDocumentLog, string strConnectionString)
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
                //open connection
                cnnConnection.Open();

                #region Update DocumentLog

                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDocumentLogUpdate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocumentLog.InstanceID) and set its properties
                SqlParameter paramInstanceID = new SqlParameter();
                paramInstanceID.ParameterName = "@vchrInstanceID";
                paramInstanceID.SqlDbType = SqlDbType.VarChar;
                paramInstanceID.Direction = ParameterDirection.Input;
                paramInstanceID.Value = objDocumentLog.InstanceID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramInstanceID);

                //Add input parameter for (objDocumentLog.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocumentLog.DocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentID);

                //Add input parameter for (objDocumentLog.SubmitterID) and set its properties
                SqlParameter paramSubmitterID = new SqlParameter();
                paramSubmitterID.ParameterName = "@intSubmitterID";
                paramSubmitterID.SqlDbType = SqlDbType.Int;
                paramSubmitterID.Direction = ParameterDirection.Input;
                paramSubmitterID.Value = objDocumentLog.SubmitterID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramSubmitterID);

                //Add input parameter for (objDocumentLog.DateSubmitted) and set its properties
                SqlParameter paramDateSubmitted = new SqlParameter();
                paramDateSubmitted.ParameterName = "@datDateSubmitted";
                paramDateSubmitted.SqlDbType = SqlDbType.DateTime;
                paramDateSubmitted.Direction = ParameterDirection.Input;
                paramDateSubmitted.Value = objDocumentLog.DateSubmitted;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDateSubmitted);

                //Add input parameter for (objDocumentLog.ApprovalRequired) and set its properties
                SqlParameter paramApprovalRequired = new SqlParameter();
                paramApprovalRequired.ParameterName = "@vchrApprovalRequired";
                paramApprovalRequired.SqlDbType = SqlDbType.VarChar;
                paramApprovalRequired.Direction = ParameterDirection.Input;
                paramApprovalRequired.Value = objDocumentLog.ApprovalRequired;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramApprovalRequired);

                //Add input parameter for (objDocumentLog.ApproverID) and set its properties
                SqlParameter paramApproverID = new SqlParameter();
                paramApproverID.ParameterName = "@intApproverID";
                paramApproverID.SqlDbType = SqlDbType.Int;
                paramApproverID.Direction = ParameterDirection.Input;
                paramApproverID.Value = objDocumentLog.ApproverID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramApproverID);

                //Add input parameter for (objDocumentLog.DateApproved) and set its properties
                SqlParameter paramDateApproved = new SqlParameter();
                paramDateApproved.ParameterName = "@datDateApproved";
                paramDateApproved.SqlDbType = SqlDbType.DateTime;
                paramDateApproved.Direction = ParameterDirection.Input;
                paramDateApproved.Value = objDocumentLog.DateApproved;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDateApproved);

                //Add input parameter for (objDocumentLog.DeclinerID) and set its properties
                SqlParameter paramDeclinerID = new SqlParameter();
                paramDeclinerID.ParameterName = "@intDeclinerID";
                paramDeclinerID.SqlDbType = SqlDbType.Int;
                paramDeclinerID.Direction = ParameterDirection.Input;
                paramDeclinerID.Value = objDocumentLog.DeclinerID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDeclinerID);

                //Add input parameter for (objDocumentLog.DateDeclined) and set its properties
                SqlParameter paramDateDeclined = new SqlParameter();
                paramDateDeclined.ParameterName = "@datDateDeclined";
                paramDateDeclined.SqlDbType = SqlDbType.DateTime;
                paramDateDeclined.Direction = ParameterDirection.Input;
                paramDateDeclined.Value = objDocumentLog.DateDeclined;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDateDeclined);

                //Add input parameter for (objDocumentLog.ReasonDeclined) and set its properties
                SqlParameter paramReasonDeclined = new SqlParameter();
                paramReasonDeclined.ParameterName = "@vchrReasonDeclined";
                paramReasonDeclined.SqlDbType = SqlDbType.VarChar;
                paramReasonDeclined.Direction = ParameterDirection.Input;
                paramReasonDeclined.Value = objDocumentLog.ReasonDeclined;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramReasonDeclined);

                //Add input parameter for (objDocumentLog.DateGenerated) and set its properties
                SqlParameter paramDateGenerated = new SqlParameter();
                paramDateGenerated.ParameterName = "@datDateGenerated";
                paramDateGenerated.SqlDbType = SqlDbType.DateTime;
                paramDateGenerated.Direction = ParameterDirection.Input;
                paramDateGenerated.Value = objDocumentLog.DateGenerated;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDateGenerated);

                //Add input parameter for (objDocumentLog.GeneratedErrorCode) and set its properties
                SqlParameter paramGeneratedErrorCode = new SqlParameter();
                paramGeneratedErrorCode.ParameterName = "@vchrGeneratedErrorCode";
                paramGeneratedErrorCode.SqlDbType = SqlDbType.VarChar;
                paramGeneratedErrorCode.Direction = ParameterDirection.Input;
                paramGeneratedErrorCode.Value = objDocumentLog.GeneratedErrorCode;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGeneratedErrorCode);

                //Add input parameter for (objDocumentLog.PolicyNo) and set its properties
                SqlParameter paramPolicyNo = new SqlParameter();
                paramPolicyNo.ParameterName = "@vchrPolicyNo";
                paramPolicyNo.SqlDbType = SqlDbType.VarChar;
                paramPolicyNo.Direction = ParameterDirection.Input;
                paramPolicyNo.Value = objDocumentLog.PolicyNo;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNo);

                //Add input parameter for (objDocumentLog.ClaimNo) and set its properties
                SqlParameter paramClaimNo = new SqlParameter();
                paramClaimNo.ParameterName = "@vchrClaimNo";
                paramClaimNo.SqlDbType = SqlDbType.VarChar;
                paramClaimNo.Direction = ParameterDirection.Input;
                paramClaimNo.Value = objDocumentLog.ClaimNo;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimNo);

                //Add input parameter for (objDocumentLog.GroupName) and set its properties
                SqlParameter paramGroupName = new SqlParameter();
                paramGroupName.ParameterName = "@vchrGroupName";
                paramGroupName.SqlDbType = SqlDbType.VarChar;
                paramGroupName.Direction = ParameterDirection.Input;
                paramGroupName.Value = objDocumentLog.GroupName;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramGroupName);

                //Add input parameter for (objDocumentLog.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objDocumentLog.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add output parameter for (objDocumentLog.Result) and set its properties
                SqlParameter paramResult = new SqlParameter();
                paramResult.ParameterName = "@intResult";
                paramResult.SqlDbType = SqlDbType.Int;
                paramResult.Direction = ParameterDirection.Output;
                paramResult.Value = intResult;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramResult);

                //execute command
                cmdCommand.ExecuteNonQuery();
                //get result
                intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());

                #endregion

              

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
                objClaimsLog.MessageIs = "Method : DocumentLogUpdate() ";
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
        }//end method : DocumentLogUpdate

        //define method : DocumentLogRead
        public DocumentLog DocumentLogRead(string vchrInstanceID, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intResult = 0;
            DocumentLog objDocumentLog = new DocumentLog();
            DateTime datTryParse = new DateTime();

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDocumentLogUpdate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocument.InstanceID) and set its properties
                SqlParameter paramInstanceID = new SqlParameter();
                paramInstanceID.ParameterName = "@vchrInstanceID";
                paramInstanceID.SqlDbType = SqlDbType.VarChar;
                paramInstanceID.Direction = ParameterDirection.Input;
                paramInstanceID.Value = vchrInstanceID.Trim();
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramInstanceID);

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
                    while (rdrReader.Read())
                    {
                        objDocumentLog.InstanceID = rdrReader["InstanceID"].ToString();
                        objDocumentLog.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocumentLog.SubmitterID = int.Parse(rdrReader["SubmitterID"].ToString());
                        if (DateTime.TryParse(rdrReader["DateSubmitted"].ToString(), out datTryParse) == true)
                        {
                            objDocumentLog.DateSubmitted = DateTime.Parse(rdrReader["DateSubmitted"].ToString());
                        }
                        objDocumentLog.ApprovalRequired = rdrReader["ApprovalRequired"].ToString();
                        objDocumentLog.ApproverID = int.Parse(rdrReader["ApproverID"].ToString());
                        if (DateTime.TryParse(rdrReader["DateApproved"].ToString(), out datTryParse) == true)
                        {
                            objDocumentLog.DateApproved = DateTime.Parse(rdrReader["DateApproved"].ToString());
                        }
                        objDocumentLog.DeclinerID = int.Parse(rdrReader["DeclinerID"].ToString());
                        if (DateTime.TryParse(rdrReader["DateDeclined"].ToString(), out datTryParse) == true)
                        {
                            objDocumentLog.DateDeclined = DateTime.Parse(rdrReader["DateDeclined"].ToString());
                        }
                        objDocumentLog.ReasonDeclined = rdrReader["ReasonDeclined"].ToString();
                        if (DateTime.TryParse(rdrReader["DateGenerated"].ToString(), out datTryParse) == true)
                        {
                            objDocumentLog.DateGenerated = DateTime.Parse(rdrReader["DateGenerated"].ToString());
                        }
                        objDocumentLog.GeneratedErrorCode = rdrReader["GeneratedErrorCode"].ToString();
                        objDocumentLog.PolicyNo = rdrReader["PolicyNo"].ToString();
                        objDocumentLog.ClaimNo = rdrReader["ClaimNo"].ToString();
                        objDocumentLog.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
                    }//end : while (rdrReader.Read())


                }//end : if (rdrReader.HasRows == true)

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
                objClaimsLog.MessageIs = "Method : DocumentLogRead() ";
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
            return (objDocumentLog);
        }//end method : DocumentLogRead

        //define method : DocumentCreate
        public int DocumentApprovalQueueCreate(DocumentApprovalQueue objDocumentApprovalQueue, string strConnectionString)
        {
            //************************************************************************
            //even though we are sending the UNC path to the doc
            //the DocumentApprovalQueueCreate method will take the UNC path
            //and get the RAW XML and store the RAW XML in the Content field
            //in the DataBase
            //************************************************************************

            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            int intResult = 0;
            
            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //open connection
                cnnConnection.Open();

                #region Create DocumentApprovalQueue

                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDocumentApprovalQueueCreate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocumentApprovalQueue.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocumentApprovalQueue.DocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentID);

                //Add input parameter for (objDocumentApprovalQueue.InstanceID) and set its properties
                SqlParameter paramInstanceID = new SqlParameter();
                paramInstanceID.ParameterName = "@vchrInstanceID";
                paramInstanceID.SqlDbType = SqlDbType.VarChar;
                paramInstanceID.Direction = ParameterDirection.Input;
                paramInstanceID.Value = objDocumentApprovalQueue.InstanceID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramInstanceID);

                //Add input parameter for (objDocumentApprovalQueue.SubmitterID) and set its properties
                SqlParameter paramSubmitterID = new SqlParameter();
                paramSubmitterID.ParameterName = "@intSubmitterID";
                paramSubmitterID.SqlDbType = SqlDbType.Int;
                paramSubmitterID.Direction = ParameterDirection.Input;
                paramSubmitterID.Value = objDocumentApprovalQueue.SubmitterID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramSubmitterID);

                //Add input parameter for (objDocumentApprovalQueue.DateSubmitted) and set its properties
                SqlParameter paramDateSubmitted = new SqlParameter();
                paramDateSubmitted.ParameterName = "@datDateSubmitted";
                paramDateSubmitted.SqlDbType = SqlDbType.DateTime;
                paramDateSubmitted.Direction = ParameterDirection.Input;
                paramDateSubmitted.Value = objDocumentApprovalQueue.DateSubmitted;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDateSubmitted);

                //Add input parameter for (objDocumentApprovalQueue.Content) and set its properties
                SqlParameter paramContent = new SqlParameter();
                paramContent.ParameterName = "@txtContent";
                paramContent.SqlDbType = SqlDbType.Text;
                paramContent.Direction = ParameterDirection.Input;
                //************************************************************************
                //even though we are sending the UNC path to the doc
                //the DocumentApprovalQueueCreate method will take the UNC path
                //and get the RAW XML and store the RAW XML in the Content field
                //in the DataBase
                //************************************************************************
                paramContent.Value = GetRawXMLFromUNCPath(objDocumentApprovalQueue.Content);
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramContent);
                
                //Add input parameter for (objDocumentApprovalQueue.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objDocumentApprovalQueue.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objDocumentApprovalQueue.Result) and set its properties
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
                objClaimsLog.MessageIs = "Method : DocumentApprovalQueueCreate() ";
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
        }//end method : DocumentApprovalQueueCreate

        //define method : DocumentApprovalQueueUpdate
        public int DocumentApprovalQueueUpdate(DocumentApprovalQueue objDocumentApprovalQueue, string strConnectionString)
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
                //open connection
                cnnConnection.Open();

                #region Update DocumentApprovalQueue

                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDocumentApprovalQueueUpdate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocumentApprovalQueue.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = objDocumentApprovalQueue.DocumentID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDocumentID);

                //Add input parameter for (objDocumentApprovalQueue.InstanceID) and set its properties
                SqlParameter paramInstanceID = new SqlParameter();
                paramInstanceID.ParameterName = "@vchrInstanceID";
                paramInstanceID.SqlDbType = SqlDbType.VarChar;
                paramInstanceID.Direction = ParameterDirection.Input;
                paramInstanceID.Value = objDocumentApprovalQueue.InstanceID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramInstanceID);

                //Add input parameter for (objDocumentApprovalQueue.SubmitterID) and set its properties
                SqlParameter paramSubmitterID = new SqlParameter();
                paramSubmitterID.ParameterName = "@intSubmitterID";
                paramSubmitterID.SqlDbType = SqlDbType.Int;
                paramSubmitterID.Direction = ParameterDirection.Input;
                paramSubmitterID.Value = objDocumentApprovalQueue.SubmitterID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramSubmitterID);

                //Add input parameter for (objDocumentApprovalQueue.DateSubmitted) and set its properties
                SqlParameter paramDateSubmitted = new SqlParameter();
                paramDateSubmitted.ParameterName = "@datDateSubmitted";
                paramDateSubmitted.SqlDbType = SqlDbType.DateTime;
                paramDateSubmitted.Direction = ParameterDirection.Input;
                paramDateSubmitted.Value = objDocumentApprovalQueue.DateSubmitted;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDateSubmitted);

                //Add input parameter for (objDocumentApprovalQueue.Content) and set its properties
                SqlParameter paramContent = new SqlParameter();
                paramContent.ParameterName = "@txtContent";
                paramContent.SqlDbType = SqlDbType.Text;
                paramContent.Direction = ParameterDirection.Input;
                paramContent.Value = objDocumentApprovalQueue.Content;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramContent);

                //Add input parameter for (objDocumentApprovalQueue.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objDocumentApprovalQueue.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objDocumentApprovalQueue.Result) and set its properties
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
                objClaimsLog.MessageIs = "Method : DocumentApprovalQueueUpdate() ";
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
        }//end method : DocumentApprovalQueueUpdate

        //define method : DocumentApprovalQueueReadAll
        public List<DocumentApprovalQueue> DocumentApprovalQueueReadALL(string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            DocumentApprovalQueue objDocumentApprovalQueue = new DocumentApprovalQueue();
            DateTime datTryParse = new DateTime();

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDocumentApprovalQueueReadALL";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //fill document
                if (rdrReader.HasRows == true)
                {
                    while (rdrReader.Read())
                    {
                        objDocumentApprovalQueue.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocumentApprovalQueue.InstanceID = rdrReader["InstanceID"].ToString();
                        objDocumentApprovalQueue.SubmitterID = int.Parse(rdrReader["SubmitterID"].ToString());
                        if (DateTime.TryParse(rdrReader["DateSubmitted"].ToString(), out datTryParse) == true)
                        {
                            objDocumentApprovalQueue.DateSubmitted = DateTime.Parse(rdrReader["DateSubmitted"].ToString());
                        }
                        objDocumentApprovalQueue.Content = rdrReader["Content"].ToString();
                        objDocumentApprovalQueue.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());

                        this._listDocumentApprovalQueue.Add(objDocumentApprovalQueue);
                    }//end : while (rdrReader.Read())
                }//end : if (rdrReader.HasRows == true)

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
                objClaimsLog.MessageIs = "Method : DocumentApprovalQueueReadALL() ";
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
            return (this._listDocumentApprovalQueue);
        }//end method : DocumentApprovalQueueReadALL

        //define method : DocumentApprovalQueueRead
        public DocumentApprovalQueue DocumentApprovalQueueRead(int intDocumentID, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            int intResult = 0;
            DocumentApprovalQueue objDocumentApprovalQueue = new DocumentApprovalQueue();
            DateTime datTryParse = new DateTime();

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDocumentApprovalQueueRead";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocument.DocumentID) and set its properties
                SqlParameter paramDocumentID = new SqlParameter();
                paramDocumentID.ParameterName = "@intDocumentID";
                paramDocumentID.SqlDbType = SqlDbType.Int;
                paramDocumentID.Direction = ParameterDirection.Input;
                paramDocumentID.Value = intDocumentID;
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
                    while (rdrReader.Read())
                    {
                        objDocumentApprovalQueue.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocumentApprovalQueue.InstanceID = rdrReader["InstanceID"].ToString();
                        objDocumentApprovalQueue.SubmitterID = int.Parse(rdrReader["SubmitterID"].ToString());
                        if (DateTime.TryParse(rdrReader["DateSubmitted"].ToString(), out datTryParse) == true)
                        {
                            objDocumentApprovalQueue.DateSubmitted = DateTime.Parse(rdrReader["DateSubmitted"].ToString());
                        }
                        objDocumentApprovalQueue.Content = rdrReader["Content"].ToString();
                        objDocumentApprovalQueue.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());
                    }//end : while (rdrReader.Read())


                }//end : if (rdrReader.HasRows == true)

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
                objClaimsLog.MessageIs = "Method : DocumentApprovalQueueRead() ";
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
            return (objDocumentApprovalQueue);
        }//end method : DocumentApprovalQueueRead

        //define method : DocumentsNeedingApprovalRead
        public List<DocumentsNeedingApproval> DocumentsNeedingApprovalRead(string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;            
            DateTime datTryParse = new DateTime();

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spGetDocumentsNeedingApproval";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //fill document
                if (rdrReader.HasRows == true)
                {
                    while (rdrReader.Read())
                    {
                        DocumentsNeedingApproval objDocumentsNeedingApproval = new DocumentsNeedingApproval();

                        objDocumentsNeedingApproval.ApprovalQueueID = int.Parse(rdrReader["ApprovalQueueID"].ToString());
                        objDocumentsNeedingApproval.DocumentID = int.Parse(rdrReader["DocumentID"].ToString());
                        objDocumentsNeedingApproval.InstanceID = rdrReader["InstanceID"].ToString();
                        objDocumentsNeedingApproval.DocumentCode = rdrReader["DocumentCode"].ToString();
                        objDocumentsNeedingApproval.Description = rdrReader["Description"].ToString();
                        objDocumentsNeedingApproval.UserName = rdrReader["UserName"].ToString();
                        objDocumentsNeedingApproval.GroupName = rdrReader["GroupName"].ToString();
                        if (DateTime.TryParse(rdrReader["DateSubmitted"].ToString(), out datTryParse) == true)
                        {
                            objDocumentsNeedingApproval.DateSubmitted = DateTime.Parse(rdrReader["DateSubmitted"].ToString());
                        }

                        this._listDocumentsNeedingApproval.Add(objDocumentsNeedingApproval);
                    }//end : while (rdrReader.Read())
                }//end : if (rdrReader.HasRows == true)

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
                objClaimsLog.MessageIs = "Method : DocumentsNeedingApprovalRead() ";
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
            return (this._listDocumentsNeedingApproval);
        }//end method : DocumentsNeedingApprovalRead

        //define method : DocumentDecline
        public int DocumentDecline(DocumentLog objDocumentLog, string strConnectionString)
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
                //open connection
                cnnConnection.Open();

                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDocumentDecline";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocumentLog.InstanceID) and set its properties
                SqlParameter paramInstanceID = new SqlParameter();
                paramInstanceID.ParameterName = "@varInstanceID";
                paramInstanceID.SqlDbType = SqlDbType.VarChar;
                paramInstanceID.Direction = ParameterDirection.Input;
                paramInstanceID.Value = objDocumentLog.InstanceID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramInstanceID);

                //Add input parameter for (objDocumentLog.paramDeclinerID) and set its properties
                SqlParameter paramDeclinerID = new SqlParameter();
                paramDeclinerID.ParameterName = "@intDeclinerID";
                paramDeclinerID.SqlDbType = SqlDbType.Int;
                paramDeclinerID.Direction = ParameterDirection.Input;
                paramDeclinerID.Value = objDocumentLog.DeclinerID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramDeclinerID);

                //Add input parameter for (objDocumentLog.ReasonDeclined) and set its properties
                SqlParameter paramReasonDeclined = new SqlParameter();
                paramReasonDeclined.ParameterName = "@varReasonDeclined";
                paramReasonDeclined.SqlDbType = SqlDbType.VarChar;
                paramReasonDeclined.Direction = ParameterDirection.Input;
                paramReasonDeclined.Value = objDocumentLog.ReasonDeclined;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramReasonDeclined);

                //Add input parameter for (Result) and set its properties
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

                //send e-mail if an error has not occured
                if (intResult != -1)
                {
                    if (DocumentDeclineSendEMailToAdjuster(objDocumentLog, strConnectionString) != false)
                    {
                        //handle error
                        ClaimsDocsLog objCD = new ClaimsDocsLog();
                        CDSupport objSupport = new CDSupport();
                        //fill log
                        objCD.ClaimsDocsLogID = 0;
                        objCD.LogTypeID = 3;
                        objCD.LogSourceTypeID = 1;
                        objCD.MessageIs = "An error occured while trying to send an decline e-mail message for instance id : " + objDocumentLog.InstanceID;
                        objCD.ExceptionIs = "An error occured while trying to send an decline e-mail message for instance id : " + objDocumentLog.InstanceID;
                        objCD.StackTraceIs = "An error occured while trying to send an decline e-mail message for instance id : " + objDocumentLog.InstanceID;
                        objCD.IUDateTime = DateTime.Now;
                        //create log record
                        objSupport.ClaimsDocsLogCreate(objCD, CDSupport.ClaimsDocsDBConnectionString);

                        //cleanup
                        objCD = null;
                        objSupport = null;
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
                objClaimsLog.MessageIs = "Method : DocumentDecline() ";
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
        }//end method : DocumentDecline

        //define method : DocumentDeclineSendEMailToAdjuster
        private bool DocumentDeclineSendEMailToAdjuster(DocumentLog objDocumentLog, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            CDSupport objSupport = new CDSupport();
            EMailSendRequest objEMailSendRequest = null;
            StringBuilder sbrBody = new StringBuilder();
            DateTime datDateDeclined = new DateTime();
            string strDeclinedBy = "";
            string strPolicyNumber = "";
            string strClaimNumber = "";
            string strDocumentDescription = "";
            string strDocumentCode = "";
            string strDeclineReason = "";
            string strApprovalQueueID = "";
            bool blnResult = true;

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //open connection
                cnnConnection.Open();

                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spGetDocDeclineInformationByInstanceID";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objDocumentLog.InstanceID) and set its properties
                SqlParameter paramInstanceID = new SqlParameter();
                paramInstanceID.ParameterName = "@varInstanceID";
                paramInstanceID.SqlDbType = SqlDbType.VarChar;
                paramInstanceID.Direction = ParameterDirection.Input;
                paramInstanceID.Value = objDocumentLog.InstanceID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramInstanceID);

                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //fill e-mail request
                if (rdrReader.HasRows == true)
                {
                    objEMailSendRequest = new EMailSendRequest();
                    while (rdrReader.Read())
                    {
                        //gather e-mail details
                        objEMailSendRequest.FromEMailAddress = rdrReader["DeclinerEMailAddress"].ToString();
                        objEMailSendRequest.ToEMailAddress = rdrReader["SubmitterEMailAddress"].ToString();
                        //gather message details
                        datDateDeclined = DateTime.Parse(rdrReader["DateDeclined"].ToString());
                        strDeclinedBy = rdrReader["DeclinerName"].ToString();
                        strPolicyNumber = rdrReader["PolicyNo"].ToString();
                        strClaimNumber = rdrReader["ClaimNo"].ToString();
                        strDocumentDescription = rdrReader["Description"].ToString();
                        strDocumentCode = rdrReader["DocumentCode"].ToString();
                        strDeclineReason = rdrReader["ReasonDeclined"].ToString();
                        strApprovalQueueID = rdrReader["ApprovalQueueID"].ToString();
                    }

                    //build message body
                    sbrBody.AppendLine("The document you submitted for approval has been declined. See details below:");
                    sbrBody.AppendLine("********************");
                    sbrBody.AppendLine("Declined Date           : " + datDateDeclined.Date.ToString());
                    sbrBody.AppendLine("Declined By             :"  + strDeclinedBy);
                    sbrBody.AppendLine("Policy Number           :"  + strPolicyNumber);
                    sbrBody.AppendLine("Claims Number           :"  + strClaimNumber);
                    sbrBody.AppendLine("Document Description    :"  + strDocumentDescription);
                    sbrBody.AppendLine("Document Code           :"  + strDocumentCode);
                    sbrBody.AppendLine("********************");
                    sbrBody.AppendLine("Decline Reason          :");
                    sbrBody.AppendLine("********************");
                    sbrBody.AppendLine(strDeclineReason);
                    sbrBody.AppendLine("********************");
                    sbrBody.AppendLine("Approval QueueID / Instance ID  : " + strApprovalQueueID + " / " + objDocumentLog.InstanceID);
                    sbrBody.AppendLine("********************");

                    //apply subject and body
                    objEMailSendRequest.Subject = CDSupport.EMailDeclineSubject;
                    objEMailSendRequest.Body = sbrBody.ToString();

                    //send e-mail message
                    blnResult = objSupport.SendEMailMessage(objEMailSendRequest);
                }

            }//end try
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
                objClaimsLog.MessageIs = "Method : DocumentDeclineSendEMailToAdjuster() ";
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
            //return results
            return (blnResult);
        }//end method : DocumentDeclineSendEMailToAdjuster
				
        //define method : DocumentApprove
        public int DocumentApprove(DocumentLog objDocumentLog, string strConnectionString)
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
                //open connection
                cnnConnection.Open();

                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spDocumentApprove";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		

                //Add input parameter for (objDocumentLog.InstanceID) and set its properties
                SqlParameter paramInstanceID = new SqlParameter();
                paramInstanceID.ParameterName = "@varInstanceID";
                paramInstanceID.SqlDbType = SqlDbType.VarChar;
                paramInstanceID.Direction = ParameterDirection.Input;
                paramInstanceID.Value = objDocumentLog.InstanceID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramInstanceID);

                //Add input parameter for (objDocumentLog.paramApproverID) and set its properties
                SqlParameter paramApproverID = new SqlParameter();
                paramApproverID.ParameterName = "@intApproverID";
                paramApproverID.SqlDbType = SqlDbType.Int;
                paramApproverID.Direction = ParameterDirection.Input;
                paramApproverID.Value = objDocumentLog.ApproverID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramApproverID);

                //Add input parameter for (Result) and set its properties
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
                objClaimsLog.MessageIs = "Method : DocumentApprove() ";
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
        }//end method : DocumentApprove

        //define method : GetRawXMLFromUNCPath
        private String GetRawXMLFromUNCPath(String strUNCPath)
        {
            String strRawXml = String.Empty;
            XmlDocument objXMLDoc = new XmlDocument();

            try
            {
                //read the XML document using the UNC path
                objXMLDoc.Load(strUNCPath);
                //set the return variable equal to the innerXML
                strRawXml = objXMLDoc.InnerXml;
            }
            catch(Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : GetRawXMLFromUNCPath() ";
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
                objXMLDoc = null;
            }
            return strRawXml;
        }//end method : GetRawXMLFromUNCPath

        //define : DocumentGetFromXML
        public ClaimsDocument DocumentGetFromXML(string strXMLSourcePathAndFileName)
        {
            //declare variables
            ClaimsDocument objClaimsDocument = null;

            try
            {
                // Construct an instance of the XmlSerializer with the type
                // of object that is being deserialized.
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClaimsDocument));
                // To read the file, create a FileStream.
                FileStream fileStream = new FileStream(strXMLSourcePathAndFileName, FileMode.Open);
                // Call the Deserialize method and cast to the object type.
                objClaimsDocument = (ClaimsDocument)xmlSerializer.Deserialize(fileStream);
                //close file stream
                fileStream.Close();
            }
            catch (Exception ex)
            {
                //handle error
                objClaimsDocument = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DocumentGetFromXML() ";
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
            return (objClaimsDocument);

        }//end : DocumentGetFromXML

        //define : DocumentMetaDataGetFromXML
        public bool DocumentMetaDataGetFromXML(ClaimsDocument objClaimsDocument, out int intContactNumber, out int intContactType)
        {
            //declare variables
            bool blnResult=true;

            //initialize values
            intContactNumber = 0;
            intContactType = 0;

            //start try
            try
            {
                //check for successful deserialization
                if (objClaimsDocument != null)
                {
                   //get contact number
                    intContactNumber = int.Parse(objClaimsDocument.Input.MetaData.C4ContactNumber);
                    intContactType = int.Parse(objClaimsDocument.Input.MetaData.C4ContactType);
                }


            }//end try
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
                objClaimsLog.MessageIs = "Method : DocumentMetaDataGetFromXML() ";
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
            return (blnResult);
        }//end : DocumentMetaDataGetFromXML

    }//end : public class CDDocument : ICDDocument
}//end : namespace ClaimsDocsBizLogic
