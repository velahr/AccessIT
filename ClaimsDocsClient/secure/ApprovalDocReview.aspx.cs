using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient;
using ClaimsDocsClient.AppClasses;
using System.Text;
using System.Xml;

namespace ClaimsDocsClient.secure
{
    public partial class ApprovalDocReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                //check for postback
                if (!IsPostBack)
                {
                    String strApprovalId = Request.QueryString["approvalID"].ToString();
                    this.hdnApprovalID.Value = strApprovalId;
                    int approvId = 0;
                    String strDocId = Request.QueryString["docdefid"].ToString();
                    this.hdnDocdefid.Value = strDocId;
                    int docId = 0;
                    String strStateId = Request.QueryString["state"].ToString();
                    this.hdnState.Value = strStateId;
                    int stateId = 0;

                    //verify the doc def id is numeric, don't assume we got an integer
                    if (!int.TryParse(strApprovalId, out approvId))
                    { throw new Exception("Invalid Document Approval Id"); }

                    //verify the doc def id is numeric, don't assume we got an integer
                    if (!int.TryParse(strDocId, out docId))
                    { throw new Exception("Invalid Document Definition Id"); }

                    //verify the state is numeric, don't assume we got an integer
                    if (!int.TryParse(strStateId, out stateId))
                    { throw new Exception("Invalid State Id"); }

                    //display session information
                    DisplaySessionInfo(approvId);

                    // get the Distribution fields to display
                    DisplayDistributionFields(docId);

                    // display the preview hyperlinks at the bottom of the page
                    DisplayPreviewHyperLinks(docId);

                }
                else
                {
                    //// process the submit
                    //ProcessPostBack();
                    ////from this point on we will use "post" instead of "get" 
                    ////so the query strings will not be visibile or needed
                    ////the next page is aware of this
                    //Server.Transfer("CorrespondenceDocumentConfirm.aspx", true);
                }
            }
            catch (Exception ex)
            {
                //handle error
                //blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : Page_Load()";
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
            }

        }

        //define method : DisplaySessionInfo
        private void DisplaySessionInfo(int iApprovId)
        {
            //declare variables
            proxyCDUser.User objUserIs = null;
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = null;
            string strInstanceID = "";

            try
            {
                //get document request session information
                //objDocGenerationRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];

                //check for information
                if (objDocGenerationRequest == null)
                {
                    //go get the DocGenerationRequest object from the DB
                    //using the approvalID
                    objDocGenerationRequest = GetDocGenerationRequest(iApprovId);
                    //save document generation request information in session
                    Session["DocGenerationRequest"] = objDocGenerationRequest;
                }

                //check instance id
                if(string.IsNullOrEmpty(Request.Params["InstanceID"])==false)
                {
                    strInstanceID = Request.Params["InstanceID"].ToString();
                    objDocGenerationRequest.InstanceID = strInstanceID;
                }
                else
                {
                    strInstanceID = "";
                    objDocGenerationRequest=null;
                }

                //double check for information
                if (objDocGenerationRequest != null)
                {
                    // get the user information out of Session
                    if (!Session["CurrentUser"].Equals(null))
                    {
                        objUserIs = (proxyCDUser.User)Session["CurrentUser"];
                    }

                    //check results
                    if (objUserIs != null)
                    {
                        this.lblHdrUser.Text = objUserIs.UserName;
                        this.lblHdrDept.Text = objUserIs.DepartmentName;
                    }
                    else
                    {
                        //show error message
                        this.lblMessage.Text = "Unable to retrieve User information from session.";
                    }

                    //display session information
                    this.lblUser.Text = objDocGenerationRequest.UserName;
                    this.lblMode.Text = objDocGenerationRequest.RunMode;
                    this.lblDepartment.Text = objDocGenerationRequest.UserDepartment;
                    this.lblClaimNumber.Text = objDocGenerationRequest.ClaimNumber.ToString();
                    this.lblProgramCode.Text = objDocGenerationRequest.PolicyNumber.Substring(0, 3);
                    this.lblAddressee.Text = objDocGenerationRequest.AddresseeName;
                }
                else
                {
                    //show error message
                    this.lblMessage.Text = "Unable to retrieve DocGenerationRequest information from session or Database.";
                }
            }
            catch (Exception ex)
            {
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : DisplaySessionInfo() ";
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

            }
        }//end method : DisplaySessionInfo

        //define method : DisplayDistributionFields
        private void DisplayDistributionFields(int iDocId)
        {
            //declare variables
            proxyCDDocument2.CDDocumentClient objDocClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            List<proxyCDDocument2.Document> lstDoc;
            String strSQL = String.Empty;

            try
            {
                //build sql string
                strSQL = strSQL + "SELECT tblDocument.DocumentID, tblDocument.DocumentCode,";
                strSQL = strSQL + " tblDocument.DepartmentID, tblDocument.ProgramID,";
                strSQL = strSQL + " tblProgram.ProgramCode, tblDocument.Description, tblDocument.TemplateName,";
                strSQL = strSQL + " tblDocument.EffectiveDate, tblDocument.ExpirationDate,";
                strSQL = strSQL + " tblDocument.ImageRightDocumentID,";
                strSQL = strSQL + " tblDocument.ImageRightDocumentSection, tblDocument.ImageRightDrawer,";
                strSQL = strSQL + " tblDocument.ContactNo, tblDocument.ContactType,";
                strSQL = strSQL + " tblDocument.DiaryNumberOfDays, tblDocument.DesignerID,";
                strSQL = strSQL + " tblDocument.StyleSheetName,";
                strSQL = strSQL + " CASE Review WHEN 'N' THEN 'No' ELSE 'Yes' END AS Review,";
                strSQL = strSQL + " CASE ProofOfMailing WHEN 'N' THEN 'No' ELSE 'Yes' END AS ProofOfMailing,";
                strSQL = strSQL + " CASE DataMatx WHEN 'N' THEN 'No' ELSE 'Yes' END AS DataMatx,";
                strSQL = strSQL + " CASE ImportToImageRight WHEN 'N' THEN 'No' ELSE 'Yes' END AS ImportToImageRight,";
                strSQL = strSQL + " CASE CopyAgent WHEN 'N' THEN 'No' ELSE 'Yes' END AS CopyAgent,";
                strSQL = strSQL + " CASE CopyInsured WHEN 'N' THEN 'No' ELSE 'Yes' END AS CopyInsured,";
                strSQL = strSQL + " CASE CopyLienHolder WHEN 'N' THEN 'No' ELSE 'Yes' END AS CopyLienHolder,";
                strSQL = strSQL + " CASE CopyFinanceCo WHEN 'N' THEN 'No' ELSE 'Yes' END AS CopyFinanceCo,";
                strSQL = strSQL + " CASE CopyAttorney WHEN 'N' THEN 'No' ELSE 'Yes' END AS CopyAttorney,";
                strSQL = strSQL + " CASE DiaryAutoUpdate WHEN 'N' THEN 'No' ELSE 'Yes' END AS DiaryAutoUpdate,";
                strSQL = strSQL + " CASE Active WHEN 'N' THEN 'No' ELSE 'Yes' END AS Active,";
                strSQL = strSQL + " tblDocument.LastModified, tblDocument.AttachedDocument, tblDocument.IUDateTime";
                strSQL = strSQL + " FROM tblDocument INNER JOIN";
                strSQL = strSQL + " tblProgram ON tblProgram.ProgramID = tblDocument.ProgramID";
                strSQL = strSQL + " WHERE (tblDocument.DocumentID =" + iDocId.ToString() + ")";
                lstDoc = objDocClient.DocumentSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);
                if (lstDoc.Count < 1)
                {
                    throw new Exception("no matching record could be found");
                }
                else
                {
                    lblDocumentCode.Text = lstDoc[0].DocumentCode.ToString();
                    this.hdnDocumentCode.Value = lstDoc[0].DocumentCode.ToString();
                    lblDocumentDesc.Text = lstDoc[0].Description.ToString();
                    this.hdnDocumentDesc.Value = lstDoc[0].Description.ToString();
                    lblImageRight.Text = lstDoc[0].ImportToImageRight.ToString();
                    lblDatamatx.Text = lstDoc[0].DataMatx.ToString();
                    lblProducer.Text = lstDoc[0].CopyAgent.ToString();
                    lblInsured.Text = lstDoc[0].CopyInsured.ToString();
                    lblLienHolder.Text = lstDoc[0].CopyLienHolder.ToString();
                    lblFinanceCo.Text = lstDoc[0].CopyFinanceCo.ToString();
                    lblAttorney.Text = lstDoc[0].CopyAttorney.ToString();
                    lblMailing.Text = lstDoc[0].ProofOfMailing.ToString();
                    lblAttachedDoc.Text = lstDoc[0].AttachedDocument.ToString();

                }
            }
            catch (Exception ex)
            {
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : DisplayDistributionFields() ";
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
                //cleanup
                if (objDocClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocClient.Close();
                }
                objDocClient = null;
            }
        }//end : DisplayDistributionFields

        //define method : DisplayPreviewHyperLinks
        private void DisplayPreviewHyperLinks(int iDocId)
        {
            //declare variables
            List<proxClaimsDocGenerator.DocumentDisplayField> listDisplayFields = new List<ClaimsDocsClient.proxClaimsDocGenerator.DocumentDisplayField>();
            proxyCDDocumentField.CDDocumentFieldClient objDocFieldClient = new ClaimsDocsClient.proxyCDDocumentField.CDDocumentFieldClient();
            List<proxyCDDocumentField.DocumentField> lstDocFields;
            proxClaimsDocGenerator.DocGenerationResponse objDocGenerationResponse = new ClaimsDocsClient.proxClaimsDocGenerator.DocGenerationResponse();
            proxClaimsDocGenerator.DocGenerationRequest objDocRequest = new ClaimsDocsClient.proxClaimsDocGenerator.DocGenerationRequest();
            proxClaimsDocGenerator.ClaimsDocsGeneratorClient objRequest = new ClaimsDocsClient.proxClaimsDocGenerator.ClaimsDocsGeneratorClient();
            String strFieldsSQL = String.Empty;
            StringBuilder sbrMessage = new StringBuilder();

            try
            {
                //call the ClaimsDocsGeneratorClient
                objDocRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];
                objDocRequest.DocumentID = iDocId;
                objDocRequest.InstanceID = objDocRequest.InstanceID;
                objDocRequest.GenerateXML = true;
                objDocRequest.GeneratePDF = true;
                objDocRequest.StoreToImageRight = false;
                objDocRequest.CompanyNumber = AppConfig.CompanyPrefixToCompanyNumber(objDocRequest.PolicyNumber.Substring(0, 3)).ToString();
                objDocGenerationResponse.GroupName = Request.Params["GroupName"].ToString();
                objDocRequest.GroupName = objDocGenerationResponse.GroupName;
                objDocRequest.DisplayFieldsGetFromXML = true;
                objDocGenerationResponse = objRequest.ClaimsDocsGenerateDocument(listDisplayFields, objDocRequest);

                //process response
                switch (objDocGenerationResponse.GeneralResponseCode)
                {
                    case 102: //success
                        //save response in seession
                        Session["DocGenerationResponse"] = (proxClaimsDocGenerator.DocGenerationResponse)objDocGenerationResponse;

                        //check for xml generation
                        if (objDocRequest.GenerateXML == true)
                        {
                            //do not show xml link in production environment
                            switch (AppConfig.RunMode.ToUpper())
                            {
                                case "PRODUCTION":
                                    //DO NOT SHOW XML LINK
                                    break;

                                default:
                                    //show xml document link
                                    this.divPreviewXML.InnerHtml = "<a href=\'" + objDocGenerationResponse.XMLFilePathAndName + "\' target=\"_new\">Preview Generated XML File</a>";
                                    // set the hidden input value so we can use it on a postback
                                    // without have to go back to Documaker
                                    this.hdnPreviewXML.Value = objDocGenerationResponse.XMLFilePathAndName;
                                    break;

                            }//end : switch (AppConfig.RunMode.ToUpper())

                           
                        }
                        else
                        {
                            this.divPreviewXML.InnerHtml = "Unable to generate XML document. <a href='../ClaimsDocsLogViewer.aspx'>See error log </a>";
                            // set the hidden input value so we can use it on a postback
                            // without have to go back to Documaker
                            this.hdnPreviewXML.Value = "Unable to generate XML document, See error log";
                        }

                        //check for pdf generation
                        if (objDocRequest.GeneratePDF == true)
                        {
                            //show pdf document link
                            this.divPreviewPDF.InnerHtml = "<a href=\'" + objDocGenerationResponse.PDFFilePathAndName + "\'  target=\"_new\">Preview Generated PDF Document</a>";

                            //check for image right storage
                            if (objDocRequest.StoreToImageRight == true)
                            {
                                //show imageright link
                                this.divImageRightPDF.InnerHtml = "<a href=\'" + objDocGenerationResponse.ImageRightFilePathAndName + "\'>Preview ImageRight PDF Document</a>";
                            }
                        }

                        break;

                    default: // no success

                        //build and show any xml errors
                        sbrMessage.Append("<hr>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>XML Generation Results</b>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<hr>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>XML File Path : </b>" + objDocGenerationResponse.XMLFilePathAndName);
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>XML Message : </b>" + objDocGenerationResponse.XMLResponseMessage);
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<hr>");

                        //build and show any pdf errors
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>PDF Generation Results</b>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<hr>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>PDF File Path : </b>" + objDocGenerationResponse.PDFFilePathAndName);
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>PDF Message : </b>" + objDocGenerationResponse.PDFResponseMessage);
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<hr>");

                        //build and show any image right errors
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>ImageRight Generation Results</b>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<hr>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>ImageRight File Path : </b>" + objDocGenerationResponse.ImageRightFilePathAndName);
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>ImageRight Message : </b>" + objDocGenerationResponse.ImageRightResponseMessage);
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<hr>");

                        //show message
                        this.divPreviewXML.InnerHtml = sbrMessage.ToString();

                        break;

                }//end : switch (objDocGenerationResponse.GeneralResponseCode)
            }
            catch (Exception ex)
            {
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : DisplayPreviewHyperLinks() ";
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
                //cleanup
                if (objDocFieldClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocFieldClient.Close();
                }
                objDocFieldClient = null;

                if (objRequest.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objRequest.Close();
                }
                objRequest = null;

            }

        }//end : DisplayPreviewHyperLinks

        //define method : ProcessPostBack
        private void ProcessPostBack()
        {
            //declare variables
            proxyCDDocument2.CDDocumentClient objDocClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            proxyCDDocument2.DocumentLog objDocumentLog = new proxyCDDocument2.DocumentLog();
            string strInstanceID = System.Guid.NewGuid().ToString();
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = null;

            try
            {
                //get document request session information
                objDocGenerationRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];

                //check reference
                if (objDocGenerationRequest != null)
                {
                    // populate the value object
                    objDocumentLog.InstanceID = strInstanceID;
                    objDocumentLog.DocumentID = int.Parse(Request.Params["DocDefID"].ToString()); 
                    objDocumentLog.SubmitterID = int.Parse(objDocGenerationRequest.UserIDNumber.ToString());
                    objDocumentLog.DateSubmitted = DateTime.Now;
                    objDocumentLog.ApprovalRequired = "N";
                    objDocumentLog.ApproverID = int.Parse(objDocGenerationRequest.UserIDNumber.ToString());
                    objDocumentLog.DateApproved = DateTime.Now;
                    objDocumentLog.DeclinerID = int.Parse(objDocGenerationRequest.UserIDNumber.ToString());
                    objDocumentLog.DateDeclined = DateTime.Now;
                    objDocumentLog.ReasonDeclined = "Not Applicable";
                    objDocumentLog.DateGenerated = DateTime.Now;
                    objDocumentLog.GeneratedErrorCode = "Not Applicable";
                    objDocumentLog.PolicyNo = objDocGenerationRequest.PolicyNumber;
                    objDocumentLog.ClaimNo = objDocGenerationRequest.ClaimNumber;
                    objDocumentLog.GroupName = objDocGenerationRequest.GroupName;
                    objDocumentLog.IUDateTime = DateTime.Now;

                    //call the service DocumentDecline method
                    if (objDocClient.DocumentDecline(objDocumentLog, AppConfig.CorrespondenceDBConnectionString) == 0)
                    {
                        //redirect to decline page
                        Response.Redirect("Confirmation.aspx?confirmtype=docapproval&state=0&docdefid=" + this.hdnDocdefid.Value + "&approvalID=" + this.hdnApprovalID.Value);
                    }

                }
            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : ProcessPostBack ";
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
                //cleanup
                if (objDocClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocClient.Close();
                }
                objDocClient = null;
            }

        }//end : ProcessPostBack

        //define method : proxClaimsDocGenerator
        private proxClaimsDocGenerator.DocGenerationRequest GetDocGenerationRequest(int iApprovId)
        {
            //declare variables
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = new proxClaimsDocGenerator.DocGenerationRequest();
            proxClaimsDocGenerator.ClaimsDocsGeneratorClient objClaimsDocsGeneratorClient = new ClaimsDocsClient.proxClaimsDocGenerator.ClaimsDocsGeneratorClient();

            try
            {
                //go to the Db and get the DocGenerationRequest data
                //using the approval ID
                objDocGenerationRequest = objClaimsDocsGeneratorClient.DocGenerationRequestInformationGetByApprovalID(iApprovId);
                //Set Run Mode
                objDocGenerationRequest.RunMode = AppConfig.RunMode.ToString();
            }
            catch (Exception ex)
            {
                objDocGenerationRequest = null;
                //handle error
                //blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : GetDocGenerationRequest ";
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
                //cleanup
                if (objClaimsDocsGeneratorClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objClaimsDocsGeneratorClient.Close();
                }
                objClaimsDocsGeneratorClient = null;
            }
            return objDocGenerationRequest;
        } //end : proxClaimsDocGenerator

        //define method : cmdDecline_Click
        protected void cmdDecline_Click(object sender, EventArgs e)
        {
            //declare variables
            try
            {
                //redirect to decline page
                Response.Redirect("ApprovalDocDecline.aspx?state=2&docdefid=" + this.hdnDocdefid.Value + "&approvalID=" + this.hdnApprovalID.Value);
            }
            catch (Exception ex)
            {
                //handle error
                //blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : GetDocGenerationRequest ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objSupport.ClaimsDocsLogCreate(objClaimsLog, AppConfig.CorrespondenceDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objSupport = null;
            }

        } // end: cmdDecline_Click

        //define method : cmdApprove_Click
        protected void cmdApprove_Click(object sender, EventArgs e)
        {
            //declare variables
            proxyCDDocument2.CDDocumentClient objDocClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            proxyCDDocument2.DocumentLog objDocumentLog = new proxyCDDocument2.DocumentLog();
            string strInstanceID = "";
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = null;
            proxClaimsDocGenerator.DocGenerationResponse objDocGenerationResponse = null;
            proxClaimsDocGenerator.ClaimsDocsGeneratorClient objDocumentGenerator = new ClaimsDocsClient.proxClaimsDocGenerator.ClaimsDocsGeneratorClient();
            proxClaimsDocGenerator.ImageRightClaimRequest objImageRightClaimRequest = new ClaimsDocsClient.proxClaimsDocGenerator.ImageRightClaimRequest();
            proxClaimsDocGenerator.ImageRightClaimRequest objImageRightClaimRequestOut = new ClaimsDocsClient.proxClaimsDocGenerator.ImageRightClaimRequest();
            int intResponseIndex = 0;

            try
            {
                 //check instance id
                if (string.IsNullOrEmpty(Request.Params["InstanceID"]) == false)
                {
                    //get instance ID
                    strInstanceID = Request.Params["InstanceID"].ToString();

                    //get document request session information
                    objDocGenerationRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];
                    //get document response from session information
                    objDocGenerationResponse = (proxClaimsDocGenerator.DocGenerationResponse)Session["DocGenerationResponse"];

                    //check reference
                    if ((objDocGenerationRequest != null) && (objDocGenerationResponse != null))
                    {
                        for (intResponseIndex = 0; intResponseIndex < objDocGenerationResponse.ListDocuMakerClaimsDocsResponse.Count; intResponseIndex++)
                        {

                            //setup ImageRight ClaimsRequest
                            objImageRightClaimRequest.FileNumber = objDocGenerationResponse.FileNumber;
                            objImageRightClaimRequest.FolderName = objDocGenerationResponse.FolderName;
                            objImageRightClaimRequest.ClaimsDocsDocID = objDocGenerationResponse.ClaimsDocsDocID;
                            objImageRightClaimRequest.ClaimsNumber = objDocGenerationRequest.ClaimNumber;

                            //if this is a document copy other than the first (i.e. file copy) then
                            //place the double dollar sign (i.e. $$) symbol after the ClaimdDocsDocID
                            //to signify to ImageRight that this is a copy (i.e. AgentCopy, Lien Holder Copy)
                            if (intResponseIndex > 0)
                            {
                                objImageRightClaimRequest.ClaimsDocsDocID = objImageRightClaimRequest.ClaimsDocsDocID + "$$CC";
                            }

                            objImageRightClaimRequest.PackType = objDocGenerationResponse.PackType;
                            objImageRightClaimRequest.DocType = objDocGenerationResponse.DocType;
                            objImageRightClaimRequest.Reason = "ClaimsDocs Generated Document";
                            objImageRightClaimRequest.CreateTask = true;
                            objImageRightClaimRequest.RequestorUserName = objDocGenerationRequest.UserName;
                            //objImageRightClaimRequest.SourceDocumentPath = objDocGenerationResponse.PDFFilePathAndName;
                            objImageRightClaimRequest.SourceDocumentPath = objDocGenerationResponse.ListDocuMakerClaimsDocsResponse[intResponseIndex].DocumentPathAndFileName;

                            //save document to image right
                            objImageRightClaimRequestOut = objDocumentGenerator.ImageRightSaveDocument(objImageRightClaimRequest);
                        }

                       

                        //check results
                        if (objImageRightClaimRequestOut.RequestResult == true)
                        {
                            // populate the value object
                            objDocumentLog.InstanceID = strInstanceID;
                            objDocumentLog.ApproverID = int.Parse(objDocGenerationRequest.UserIDNumber.ToString());
                            objDocumentLog.DateApproved = DateTime.Now;

                            //call the service DocumentApprove method
                            if (objDocClient.DocumentApprove(objDocumentLog, AppConfig.CorrespondenceDBConnectionString) == 0)
                            {
                                //redirect to confirmation page
                                Response.Redirect("Confirmation.aspx?confirmtype=docapproval&state=0&docdefid=" + this.hdnDocdefid.Value + "&approvalID=" + this.hdnApprovalID.Value);
                            }
                        }
                    }
                }
                else
                {

                }//end : if (string.IsNullOrEmpty(Request.Params["InstanceID"]) == false)

            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : cmdApprove_Click() ";
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
                //cleanup
                if (objDocClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocClient.Close();
                }
                objDocClient = null;

                if (objDocumentGenerator.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocumentGenerator.Close();
                }
                objDocumentGenerator = null;

            }
        }//end : cmdApprove_Click

    }//end : public partial class ApprovalDocReview : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.secure
