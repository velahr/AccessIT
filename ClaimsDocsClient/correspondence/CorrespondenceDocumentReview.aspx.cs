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

namespace ClaimsDocsClient.correspondence
{
    public partial class CorrespondenceDocumentReview : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables

            try
            {
                //check for postback
                if (!IsPostBack)
                {
                    String strDocId = Request.Form["docdefid"].ToString();
                    int docId = 0;
                    String strStateId = Request.Form["state"].ToString();
                    int stateId = 0;

                    //display session information
                    DisplaySessionInfo();

                    //display buttons
                    DisplayButtons(strStateId, strDocId);

                    //verify the doc def id is numeric, don't assume we got an integer
                    if (IsInteger16(strDocId))
                    { docId = Convert.ToInt16(strDocId); }
                    else
                    { throw new Exception("Invalid Document Definition Id"); }

                    //verify the state is numeric, don't assume we got an integer
                    if (IsInteger16(strStateId))
                    { stateId = Convert.ToInt16(strStateId); }
                    else
                    { throw new Exception("Invalid State Id"); }

                    // get the Distribution fields to display
                    DisplayDistributionFields(docId);

                    // get the "Fields" fields to display
                    DisplayDynamicFields(docId);

                    //cleanup
                    docdefid.Value = strDocId;
                    state.Value = strStateId;
                }
                else
                {
                    // process the submit
                    ProcessPostBack();
                    //from this point on we will use "post" instead of "get" 
                    //so the query strings will not be visibile or needed
                    //the next page is aware of this
                    Server.Transfer("CorrespondenceDocumentConfirm.aspx", true);
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
        }//end method : Page_Load

        //define method : IsInteger16
        private static bool IsInteger16(string theValue)
        {
            try
            {
                Convert.ToInt16(theValue);
                return true;
            }
            catch (Exception ex)
            {
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : IsInteger16() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objSupport.ClaimsDocsLogCreate(objClaimsLog, AppConfig.CorrespondenceDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objSupport = null;

                return false;
            }
        } //end method : IsInteger

        //define method : DisplaySessionInfo
        private void DisplaySessionInfo()
        {
            //declare variables
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = null;

            try
            {
                //get document request session information
                objDocGenerationRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];
                //check for information
                if (objDocGenerationRequest != null)
                {
                    //display session information
                    this.lblUser.Text = objDocGenerationRequest.UserName;
                    this.lblMode.Text = objDocGenerationRequest.RunMode;
                    this.lblDepartment.Text = objDocGenerationRequest.UserDepartment;
                    this.lblClaimNumber.Text = objDocGenerationRequest.ClaimNumber.ToString();
                    this.lblProgramCode.Text = objDocGenerationRequest.PolicyNumber.Substring(0, 3);
                    this.lblAddressee.Text = objDocGenerationRequest.AddresseeName;
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
                    DocumentCode.Value = lstDoc[0].DocumentCode.ToString();
                    lblDocumentDesc.Text = lstDoc[0].Description.ToString();
                    DocumentDesc.Value = lstDoc[0].Description.ToString();
                    lblImageRight.Text = lstDoc[0].ImportToImageRight.ToString();
                    lblDatamatx.Text = lstDoc[0].DataMatx.ToString();
                    lblProducer.Text = lstDoc[0].CopyAgent.ToString();
                    lblInsured.Text = lstDoc[0].CopyInsured.ToString();
                    lblLienHolder.Text = lstDoc[0].CopyLienHolder.ToString();
                    lblFinanceCo.Text = lstDoc[0].CopyFinanceCo.ToString();
                    lblAttorney.Text = lstDoc[0].CopyAttorney.ToString();
                    lblMailing.Text = lstDoc[0].ProofOfMailing.ToString();
                    lblAttachedDoc.Text = lstDoc[0].AttachedDocument.ToString();
                    // set the review flag so that when this page postback
                    // we know if this doc requires a review
                    reviewFlag.Value = lstDoc[0].Review.ToString();
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
                    //close
                    objDocClient.Close();
                }
                objDocClient = null;

            }
        }//end : DisplayDistributionFields

        //define method : DisplayDynamicFields
        private void DisplayDynamicFields(int iDocId)
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
                // get the "Fields" fields to display
                //declare variables
                //we have to go back to the server to the get the field names
                //the only way around this is to store them as hidden fields 
                //on the previous page, this will be very nasty if we have 20
                //hidden fields just for field names.

                //build sql string
                strFieldsSQL = "Select DocumentFieldID, DocumentID, FieldNameIs, FieldTypeIs,";
                strFieldsSQL = strFieldsSQL + " IsFieldRequired, FieldDescription, IUDateTime";
                strFieldsSQL = strFieldsSQL + " FROM dbo.tblDocumentField";
                strFieldsSQL = strFieldsSQL + " WHERE DocumentID = " + iDocId.ToString();
                strFieldsSQL = strFieldsSQL + " Order By DocumentFieldID";
                lstDocFields = objDocFieldClient.DocumentFieldSearch(strFieldsSQL, AppConfig.CorrespondenceDBConnectionString);

                //if any rows are returned build the "Fields" for display
                //otherwise the "Fields" section will only show and empty table
                if (lstDocFields.Count > 0)
                {
                    //create the table row to which the cells will be added

                    TableRow tblRow = new TableRow();
                    proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequestor = new ClaimsDocsClient.proxClaimsDocGenerator.DocGenerationRequest();
                    int ndx = 0;
                    while (ndx < lstDocFields.Count)
                    {
                        //create display field object
                        proxClaimsDocGenerator.DocumentDisplayField objDisplayField = new ClaimsDocsClient.proxClaimsDocGenerator.DocumentDisplayField();

                        //create the field label
                        Label lblCTL = new Label();
                        //get submitted data labels
                        lblCTL.Text = lstDocFields[ndx].FieldNameIs.ToString() + ":";
                        //set display field
                        objDisplayField.DisplayFieldName = lstDocFields[ndx].FieldNameIs.ToString();

                        //create the table cell to hold the label
                        TableCell tblCellLabel = new TableCell();
                        tblCellLabel.HorizontalAlign = HorizontalAlign.Left;
                        tblCellLabel.VerticalAlign = VerticalAlign.Top;
                        tblCellLabel.CssClass = "Heading";
                        //add the label to the cell
                        tblCellLabel.Controls.Add(lblCTL);

                        //create the field VALUE label
                        TextBox txtBoxCTL = new TextBox();
                        //make the text box look like a label
                        txtBoxCTL.ReadOnly = true;
                        txtBoxCTL.CssClass = "FakeLabel";
                        txtBoxCTL.BorderStyle = BorderStyle.None;

                        //is it multi-line or single line
                        switch (lstDocFields[ndx].FieldTypeIs.ToString().ToUpper().Trim())
                        {
                            case "STRING":
                                txtBoxCTL.TextMode = TextBoxMode.SingleLine;
                                break;
                            case "TEXT":
                                txtBoxCTL.TextMode = TextBoxMode.MultiLine;
                                txtBoxCTL.ReadOnly = true;
                                txtBoxCTL.Height = new Unit("138px");
                                txtBoxCTL.Width = new Unit("149px");
                                break;
                            case "NUMBER":
                                txtBoxCTL.TextMode = TextBoxMode.SingleLine;
                                break;
                            case "DATE":
                                //Defect# 112 & 113 (LMS)
                                txtBoxCTL.TextMode = TextBoxMode.SingleLine;
                                break;
                        }

                        //get submittted data
                        txtBoxCTL.Text = Request.Form[lstDocFields[ndx].DocumentFieldID.ToString().Trim()].ToString();
                        //save the value in a session var incase the user goes back to the previous page
                        Session["fld" + ndx.ToString()] = txtBoxCTL.Text;
                        //set display value
                        objDisplayField.DisplayFieldValue = Request.Form[lstDocFields[ndx].DocumentFieldID.ToString().Trim()].ToString();

                        //create the table cell to hold the text box
                        TableCell tblCellTextBox = new TableCell();
                        tblCellTextBox.HorizontalAlign = HorizontalAlign.Left;
                        tblCellTextBox.VerticalAlign = VerticalAlign.Top;
                        //add the label to the cell
                        tblCellTextBox.Controls.Add(txtBoxCTL);

                        //add the cells
                        tblRow.Controls.AddAt(tblRow.Controls.Count, tblCellLabel);
                        tblRow.Controls.AddAt(tblRow.Controls.Count, tblCellTextBox);

                        //add the row to the parent table
                        tblFileds.Controls.AddAt(tblFileds.Controls.Count, tblRow);
                        //create a new row to work with
                        tblRow = new TableRow();

                        //add display field to list
                        listDisplayFields.Add(objDisplayField);

                        ndx++;
                    }
                }
                else
                {
                    // dont show the fields table 
                    tblFileds.Visible = false;
                }

                //clear session display fields
                Session["DisplayFieldList"] = null;

                //save display fields
                if (listDisplayFields.Count > 0)
                {
                    Session["DisplayFieldList"] = (List<proxClaimsDocGenerator.DocumentDisplayField>)listDisplayFields;
                }

                //call the ClaimsDocsGeneratorClient
                objDocRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];
                objDocRequest.DocumentID = iDocId;
                //objDocRequest.InstanceID = objDocRequest.InstanceID;
                objDocRequest.InstanceID = Guid.NewGuid().ToString();
                objDocRequest.GenerateXML = true;
                objDocRequest.GeneratePDF = true;
                objDocRequest.StoreToImageRight = false;
                objDocRequest.CompanyNumber = AppConfig.CompanyPrefixToCompanyNumber(objDocRequest.PolicyNumber.Substring(0, 3)).ToString();
                //save document generation request to session
                Session["DocGenerationRequest"] = (proxClaimsDocGenerator.DocGenerationRequest)objDocRequest;
                //get document generation response
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
                                    PreviewXML.Value = objDocGenerationResponse.XMLFilePathAndName;
                                    break;
                            }//end : switch (AppConfig.RunMode.ToUpper())
                        }
                        else
                        {
                            this.divPreviewXML.InnerHtml = "Unable to generate XML document. <a href='../ClaimsDocsLogViewer.aspx'>See error log </a>";
                            // set the hidden input value so we can use it on a postback
                            // without have to go back to Documaker
                            PreviewXML.Value = "Unable to generate XML document, See error log";
                        }

                        //check for pdf generation
                        if (objDocRequest.GeneratePDF == true)
                        {
                            //show pdf document link
                            this.divPreviewPDF.InnerHtml = "<a href=\'" + objDocGenerationResponse.PDFFilePathAndName + "\' target=\"_new\">Preview Generated PDF Document</a>";

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
                objClaimsLog.MessageIs = "Method : DisplayDynamicFields() ";
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
        }//end method : DisplayDynamicFields

        //define method : ProcessPostBack
        private void ProcessPostBack()
        {
            //declare variables
            proxyCDDocument2.CDDocumentClient objDocClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            proxyCDDocument2.DocumentLog objDocumentLog = new proxyCDDocument2.DocumentLog();
            string strInstanceID = System.Guid.NewGuid().ToString();
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = null;
            proxClaimsDocGenerator.DocGenerationResponse objDocGenerationResponse = new ClaimsDocsClient.proxClaimsDocGenerator.DocGenerationResponse();
            List<proxClaimsDocGenerator.DocumentDisplayField> listDisplayFields = new List<ClaimsDocsClient.proxClaimsDocGenerator.DocumentDisplayField>();
            proxClaimsDocGenerator.ImageRightClaimRequest objImageRightClaimRequest = new ClaimsDocsClient.proxClaimsDocGenerator.ImageRightClaimRequest();
            proxClaimsDocGenerator.ImageRightClaimRequest objImageRightClaimRequestOut = new ClaimsDocsClient.proxClaimsDocGenerator.ImageRightClaimRequest();
            int intResponseIndex = 0;

            try
            {
                //get document request session information
                objDocGenerationRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];

                //check reference
                if (objDocGenerationRequest != null)
                {
                    // populate the value object
                    //objDocumentLog.InstanceID = strInstanceID;
                    objDocumentLog.InstanceID = objDocGenerationRequest.InstanceID;
                    objDocumentLog.DocumentID = int.Parse(Request.Form["docdefid"].ToString());
                    objDocumentLog.SubmitterID = int.Parse(objDocGenerationRequest.UserIDNumber.ToString());
                    objDocumentLog.DateSubmitted = DateTime.Now;
                    if (Request.Form["reviewFlag"].ToUpper().Trim().Equals("YES"))
                    {
                        objDocumentLog.ApprovalRequired = "Y";
                    }
                    else
                    {
                        objDocumentLog.ApprovalRequired = "N";
                    }
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

                    //call the service DocumentLogCreate method
                    objDocClient.DocumentLogCreate(objDocumentLog, AppConfig.CorrespondenceDBConnectionString);

                    //get display list
                    listDisplayFields = (List<proxClaimsDocGenerator.DocumentDisplayField>)Session["DisplayFieldList"];

                    //call the ClaimsDocsGeneratorClient
                    proxClaimsDocGenerator.DocGenerationRequest objDocRequest = new ClaimsDocsClient.proxClaimsDocGenerator.DocGenerationRequest();
                    proxClaimsDocGenerator.ClaimsDocsGeneratorClient objRequest = new ClaimsDocsClient.proxClaimsDocGenerator.ClaimsDocsGeneratorClient();
                    objDocRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];
                    objDocRequest.DocumentID = objDocumentLog.DocumentID;
                    objDocRequest.InstanceID = objDocRequest.InstanceID;
                    objDocRequest.GenerateXML = true;
                    objDocRequest.GeneratePDF = true;
                    objDocRequest.StoreToImageRight = true;

                    //does this document require approval
                    if (objDocumentLog.ApprovalRequired.Equals("N"))
                    {
                        //indicate that the document should be stored in imageright
                        objDocRequest.StoreToImageRight = true;
                        //get company number
                        objDocRequest.CompanyNumber = AppConfig.CompanyPrefixToCompanyNumber(objDocRequest.PolicyNumber.Substring(0, 3)).ToString();

                        //get document response from session information
                        objDocGenerationResponse = (proxClaimsDocGenerator.DocGenerationResponse)Session["DocGenerationResponse"];
                        //get document request session information
                        objDocGenerationRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];

                        if ((objDocGenerationRequest != null) && (objDocGenerationResponse != null))
                        {
                            for (intResponseIndex = 0; intResponseIndex < objDocGenerationResponse.ListDocuMakerClaimsDocsResponse.Count; intResponseIndex++)
                            {
                                //setup ImageRight ClaimsRequest
                                objImageRightClaimRequest.ClaimsNumber = objDocGenerationRequest.ClaimNumber;
                                objImageRightClaimRequest.RequestorUserName = objDocGenerationRequest.UserName;
                                objImageRightClaimRequest.FileNumber = objDocGenerationResponse.FileNumber;
                                objImageRightClaimRequest.FolderName = objDocGenerationResponse.FolderName;
                                objImageRightClaimRequest.ClaimsDocsDocID = objDocGenerationResponse.ClaimsDocsDocID;
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
                                //objImageRightClaimRequest.SourceDocumentPath = objDocGenerationResponse.PDFFilePathAndName;
                                objImageRightClaimRequest.SourceDocumentPath = objDocGenerationResponse.ListDocuMakerClaimsDocsResponse[intResponseIndex].DocumentPathAndFileName;
                                
                                //save document to image right
                                objImageRightClaimRequestOut = objRequest.ImageRightSaveDocument(objImageRightClaimRequest);
                            }
                        }


                    }
                    else
                    {
                        //yes, so save the data to the DocumentApprovalQueue table
                        proxyCDDocument2.DocumentApprovalQueue objDocApprovalQueue = new ClaimsDocsClient.proxyCDDocument2.DocumentApprovalQueue();

                        // populate the value object
                        objDocApprovalQueue.DocumentID = int.Parse(Request.Form["docdefid"].ToString());
                        //objDocApprovalQueue.InstanceID = strInstanceID; // objDocGenerationRequest.InstanceID;
                        objDocApprovalQueue.InstanceID = objDocRequest.InstanceID;
                        objDocApprovalQueue.SubmitterID = objDocGenerationRequest.UserIDNumber;
                        objDocApprovalQueue.DateSubmitted = DateTime.Now;
                        //************************************************************************
                        //even though we are sending the UNC path to the doc
                        //the DocumentApprovalQueueCreate method will take the UNC path
                        //and get the RAW XML and store the RAW XML in the Content field
                        //in the DataBase
                        //************************************************************************
                        objDocApprovalQueue.Content = Request.Form["PreviewXML"].Trim();
                        objDocApprovalQueue.IUDateTime = DateTime.Now;

                        //call the service DocumentApprovalQueueCreate method
                        objDocClient.DocumentApprovalQueueCreate(objDocApprovalQueue, AppConfig.CorrespondenceDBConnectionString);
                    }

                }//end : if (objDocGenerationRequest != null)
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

        //define method : DisplayButtons
        public bool DisplayButtons(string strState, string strDocDefID)
        {
            //declare variables
            bool blnResult = true;
            StringBuilder sbrQueryString = new StringBuilder();
            StringBuilder sbrHTMLButton = new StringBuilder();

            try
            {
                //build query string
                sbrQueryString.Append("CorrespondenceDocumentEntry.aspx?");
                sbrQueryString.Append("State=" + strState);
                sbrQueryString.Append("&DocDefID=" + strDocDefID);

                //build button html
                sbrHTMLButton.Append("<input style=\"width: 8em; text-align: center\" class=\"button\" type=\"button\" value=\"Back\" onclick=\"window.location.replace(\'");
                sbrHTMLButton.Append(sbrQueryString.ToString() + "')\")");
                this.divButtons.InnerHtml = sbrHTMLButton.ToString();

            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : DisplayButtons() ";
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

            //return result
            return (blnResult);
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {

        }



    }//end : public partial class CorrespondenceDocumentReview : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.correspondence
