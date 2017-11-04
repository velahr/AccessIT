using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ClaimsDocsClient;
using System.Web.Security;
using ClaimsDocsClient.AppClasses;

namespace ClaimsDocsClient.TestHarness
{
    public partial class ClaimsDocsTestPage : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables

            try
            {
                //check for postback
                if(this.Page.IsPostBack==false)
                {
                    //refresh document list
                    if (DocumentListRefresh() == false)
                    {
                        //handle error
                    }//end : if (DocumentListRefresh() == false)

                    //refresh user group list
                    if (UserGroupListRefresh() == false)
                    {
                    }

                }//end : if(this.Page.IsPostBack==false)
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
                objClaimsLog.MessageIs = "Method : Page_Load() ";
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

        }//end : protected void Page_Load(object sender, EventArgs e)

        protected void cmdTestClaimsDocumentXMLBuild_Click(object sender, EventArgs e)
        {
            //declare variables
            ClaimsDocsClient.proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = new ClaimsDocsClient.proxClaimsDocGenerator.DocGenerationRequest();
            ClaimsDocsClient.proxClaimsDocGenerator.ClaimsDocsGeneratorClient objCDGClient = new ClaimsDocsClient.proxClaimsDocGenerator.ClaimsDocsGeneratorClient();
            string strClaimsDocsXMLPathAndFileName = "";
            List<proxClaimsDocGenerator.DocumentDisplayField> listDisplayFields = new List<ClaimsDocsClient.proxClaimsDocGenerator.DocumentDisplayField>();
            proxClaimsDocGenerator.DocGenerationResponse objDocGenerationResponse = new ClaimsDocsClient.proxClaimsDocGenerator.DocGenerationResponse();
            StringBuilder sbrMessage = new StringBuilder();

            try
            {
                //setup document generation request
                objDocGenerationRequest.GenerateXML = true;
                objDocGenerationRequest.GeneratePDF = true;
                objDocGenerationRequest.StoreToImageRight = true;
                objDocGenerationRequest.InstanceID = this.txtInstanceID.Text;
                objDocGenerationRequest.DocumentID = int.Parse(this.cboDocumentList.SelectedValue.ToString());
                objDocGenerationRequest.PolicyNumber = this.txtPolicyNumber.Text;
                objDocGenerationRequest.ClaimNumber = this.txtClaimNumber.Text;
                objDocGenerationRequest.ContactNumber = int.Parse(this.txtContactNumber.Text);
                objDocGenerationRequest.ContactType = int.Parse(this.txtContactType.Text);
                objDocGenerationRequest.UserName = this.txtUserName.Text;
                objDocGenerationRequest.GroupName = this.cboUserGroup.SelectedValue.ToString();
                objDocGenerationRequest.CompanyNumber = AppConfig.CompanyPrefixToCompanyNumber(objDocGenerationRequest.PolicyNumber.Substring(0,3)).ToString();

                //build xml
                objDocGenerationResponse = objCDGClient.ClaimsDocsGenerateDocument(listDisplayFields, objDocGenerationRequest);

                //process response
                switch (objDocGenerationResponse.GeneralResponseCode)
                {
                    case 102: //success
                        //check for xml generation
                        if (objDocGenerationRequest.GenerateXML == true)
                        {
                            //show xml document link
                            this.divPreviewXML.InnerHtml = "<a href=\'" + objDocGenerationResponse.XMLFilePathAndName + "\'>Preview Generated XML File</a>";

                            //check for pdf generation
                            if (objDocGenerationRequest.GeneratePDF == true)
                            {
                                //show pdf document link
                                this.divPreviewPDF.InnerHtml = "<a href=\'" + objDocGenerationResponse.PDFFilePathAndName + "\'>Preview Generated PDF Document</a>";

                                //check for image right storage
                                if (objDocGenerationRequest.StoreToImageRight == true)
                                {
                                    //show imageright link
                                    this.divImageRightPDF.InnerHtml = "<a href=\'" + objDocGenerationResponse.ImageRightFilePathAndName + "\'>Preview ImageRight PDF Document</a>";
                                }
                            }

                        }
                        break;

                    default: // no success
                        //build and show any xml errors
                        sbrMessage.Append("<hr>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<h2>XML Generation Results</h2>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>XML File Path : </b>" + objDocGenerationResponse.XMLFilePathAndName);
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>XML Message : </b>" + objDocGenerationResponse.XMLResponseMessage);
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<hr>");

                        //build and show any pdf errors
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<h2>PDF Generation Results</h2>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>PDF File Path : </b>" + objDocGenerationResponse.PDFFilePathAndName);
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<b>PDF Message : </b>" + objDocGenerationResponse.PDFResponseMessage);
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<hr>");

                        //build and show any image right errors
                        sbrMessage.Append("<br>");
                        sbrMessage.Append("<h2>ImageRight Generation Results</h2>");
                        sbrMessage.Append("<br>");
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
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : Page_Load() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objSupport.ClaimsDocsLogCreate(objClaimsLog, AppConfig.CorrespondenceDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objSupport = null;

                Response.Write(ex.Message);
                Response.Write("<br><br><br>");
                Response.Write(ex.StackTrace);
            }
            finally
            {
                //cleanup
                if (objCDGClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objCDGClient.Close();
                }
                objCDGClient = null;
                //objDocument = null;
            }


        }//end : protected void cmdTestClaimsDocumentXMLBuild_Click(object sender, EventArgs e)

        //define method : DocumentListRefresh
        private bool DocumentListRefresh()
        {
            //declare variables
            bool blnResult = true;
            string strSQL = "";
            proxyCDDocument2.CDDocumentClient objDocumentClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            List<proxyCDDocument2.Document> listDocument = new List<ClaimsDocsClient.proxyCDDocument2.Document>();
            int intIndex=0;

            try
            {
                //build sql string
                strSQL = strSQL + " Select ";
                strSQL = strSQL + "     tblDocument.DocumentID,   ";
                strSQL = strSQL + "     tblDocument.DocumentCode,   ";
                strSQL = strSQL + "     tblDocument.DepartmentID,   ";
                strSQL = strSQL + "     tblDocument.ProgramID,   ";
                strSQL = strSQL + "     tblProgram.ProgramCode,   ";
                strSQL = strSQL + "     tblDocument.Description,   ";
                strSQL = strSQL + "     tblDocument.TemplateName,   ";
                strSQL = strSQL + "     tblDocument.EffectiveDate,   ";
                strSQL = strSQL + "     tblDocument.ExpirationDate,   ";
                strSQL = strSQL + "     tblDocument.ImageRightDocumentID,   ";
                strSQL = strSQL + "     tblDocument.ImageRightDocumentSection,   ";
                strSQL = strSQL + "     tblDocument.ImageRightDrawer,   ";
                strSQL = strSQL + "     tblDocument.ContactNo, tblDocument.ContactType,";
                strSQL = strSQL + "     tblDocument.DiaryNumberOfDays,   ";
                strSQL = strSQL + "     tblDocument.DesignerID,   ";
                strSQL = strSQL + "     tblDocument.StyleSheetName,   ";
                strSQL = strSQL + "     Case Review When 'N' Then 'No' Else 'Yes' End As Review,";
                strSQL = strSQL + "     Case ProofOfMailing When 'N' Then 'No' Else 'Yes' End As ProofOfMailing,";
                strSQL = strSQL + "     Case DataMatx When 'N' Then 'No' Else 'Yes' End As DataMatx,";
                strSQL = strSQL + "     Case ImportToImageRight When 'N' Then 'No' Else 'Yes' End As ImportToImageRight,";
                strSQL = strSQL + "     Case CopyAgent When 'N' Then 'No' Else 'Yes' End As CopyAgent,";
                strSQL = strSQL + "     Case CopyInsured When 'N' Then 'No' Else 'Yes' End As CopyInsured,";
                strSQL = strSQL + "     Case CopyLienHolder When 'N' Then 'No' Else 'Yes' End As CopyLienHolder,";
                strSQL = strSQL + "     Case CopyFinanceCo When 'N' Then 'No' Else 'Yes' End As CopyFinanceCo,";
                strSQL = strSQL + "     Case CopyAttorney When 'N' Then 'No' Else 'Yes' End As CopyAttorney,";
                strSQL = strSQL + "     Case DiaryAutoUpdate When 'N' Then 'No' Else 'Yes' End As DiaryAutoUpdate,";
                strSQL = strSQL + "     Case Active When 'N' Then 'No' Else 'Yes' End As Active,";
                strSQL = strSQL + "     tblDocument.LastModified ,";
                strSQL = strSQL + "     tblDocument.AttachedDocument ,";
                strSQL = strSQL + "     tblDocument.IUDateTime ";
                strSQL = strSQL + " From ";
                strSQL = strSQL + "    dbo.tblDocument";
                strSQL = strSQL + " Inner Join ";
                strSQL = strSQL + "     dbo.tblProgram On tblProgram.ProgramID=dbo.tblDocument.ProgramID ";
                strSQL = strSQL + " Order By tblDocument.DocumentCode ";

                //get document list
                listDocument = objDocumentClient.DocumentSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);

                //apply document list to combo box
                this.cboDocumentList.DataSource = listDocument;
                this.cboDocumentList.DataBind();

                for (intIndex = 0; intIndex < this.cboDocumentList.Items.Count; intIndex++)
                {
                    if (this.cboDocumentList.Items[intIndex].Value.Equals("44"))
                    {
                        this.cboDocumentList.SelectedIndex = intIndex;
                    }

                }//end : for (intIndex = 0; intIndex < this.cboDocumentList.Items.Count; intIndex)

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
                objClaimsLog.MessageIs = "Method : Page_Load() ";
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
                if (objDocumentClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocumentClient.Close();
                }
                objDocumentClient = null;

            }
            
            //return result
            return (blnResult);

        }//end : DocumentListRefresh

        //define method : UserGroupListRefresh
        private bool UserGroupListRefresh()
        {
            //declare variables
            bool blnResult = true;
            string strSQL = "";
            proxyCDUser.CDUsersClient objUserClient = new ClaimsDocsClient.proxyCDUser.CDUsersClient();
            proxyCDDocument2.CDDocumentClient objDocumentClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            List<proxyCDUser.UserGroup> listUserGroup = new List<proxyCDUser.UserGroup>();

            try
            {
                //build sql string
                listUserGroup = objUserClient.UserGroupSearchByUserName(this.txtUserName.Text, AppConfig.CorrespondenceDBConnectionString);

                //apply document list to combo box
                this.cboUserGroup.DataSource = listUserGroup;
                this.cboUserGroup.DataBind();
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
                objClaimsLog.MessageIs = "Method : Page_Load() ";
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
                if (objUserClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objUserClient.Close();
                }
                objUserClient = null;

                if (objDocumentClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocumentClient.Close();
                }
                objDocumentClient = null;

            }

            //return result
            return (blnResult);

        }

        protected void cmdSendEMail_Click(object sender, EventArgs e)
        {
            //declare variables
            bool blnResult = true;
            proxyCDSupport.CDSupportClient objCDSupportClient = new ClaimsDocsClient.proxyCDSupport.CDSupportClient();
            proxyCDSupport.EMailSendRequest objEMailSendRequest = new ClaimsDocsClient.proxyCDSupport.EMailSendRequest();

            try
            {
                //gather e-mail send request values
                if (string.IsNullOrEmpty(this.txtFromEMailAddress.Text))
                {
                    this.lblEMailResults.Text = "Invalid From E-Mail Address";
                }
                else
                {
                    objEMailSendRequest.FromEMailAddress = this.txtFromEMailAddress.Text;
                }

                if (string.IsNullOrEmpty(this.txtToEMailAddress.Text))
                {
                    this.lblEMailResults.Text = "Invalid To E-Mail Address";
                }
                else
                {
                    objEMailSendRequest.ToEMailAddress = this.txtToEMailAddress.Text;
                }

                if (string.IsNullOrEmpty(this.txtSubject.Text))
                {
                    this.lblEMailResults.Text = "Invalid Subject";
                }
                else
                {
                    objEMailSendRequest.Subject = this.txtSubject.Text;
                }

                if (string.IsNullOrEmpty(this.txtMessage.Text))
                {
                    this.lblEMailResults.Text = "Invalid Message";
                }
                else
                {
                    objEMailSendRequest.Body = this.txtMessage.Text;
                }

                //send e-mail
                if (objCDSupportClient.SendEMailMessage(objEMailSendRequest) == false)
                {
                    this.lblEMailResults.Text = "Failed to send e-mail";
                }
                else
                {
                    this.lblEMailResults.Text = "E-Mail sent successfully.";
                }

            }
            catch (Exception ex)
            {
                //handle error
                this.lblEMailResults.Text = ex.Message;
                blnResult = false;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : cmdSendEMail_Click() ";
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

        protected void cmdDeserializeXML_Click(object sender, EventArgs e)
        {
            ////declare variables
            //ClaimsDocsClient.proxyCDDocumentField.CDDocumentFieldClient objDisplayFieldClient = new ClaimsDocsClient.proxyCDDocumentField.CDDocumentFieldClient();
            //string strFileToDeserialize = "";
            //List<proxyCDDocumentField.DocumentDisplayField> listDocumentField = null;

            //try
            //{
            //    //get file to deserialize
            //    if (string.IsNullOrEmpty(this.txtFileToDeserialize.Text) == false)
            //    {
            //        strFileToDeserialize = this.txtFileToDeserialize.Text;
            //        listDocumentField = objDisplayFieldClient.DisplayFieldsGetFromXMLFile(strFileToDeserialize);
            //        if (listDocumentField != null)
            //        {
            //            if(listDocumentField.Count>0)
            //            {
            //                this.lblXMLFileDeserializationResults.Text = "Field Count : " + listDocumentField.Count.ToString();
            //            }
            //            else
            //            {
            //                this.lblXMLFileDeserializationResults.Text = "Field Count : 0";
            //            }
            //        }
            //        else
            //        {
            //            this.lblXMLFileDeserializationResults.Text = "Field Count : 0";
            //        }
            //    }
            //    else
            //    {
            //        this.lblXMLFileDeserializationResults.Text = "Invalid Deserialization File...";
            //    }

            //    //deserialize 
            //    //objDisplayFieldClient.

            //}
            //catch (Exception ex)
            //{
            //    //handle error
            //    this.lblEMailResults.Text = ex.Message;
            //    ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
            //    AppSupport objSupport = new AppSupport();
            //    //fill log
            //    objClaimsLog.ClaimsDocsLogID = 0;
            //    objClaimsLog.LogTypeID = 3;
            //    objClaimsLog.LogSourceTypeID = 2;
            //    objClaimsLog.MessageIs = "Method : cmdDeserializeXML_Click() ";
            //    objClaimsLog.ExceptionIs = ex.Message;
            //    objClaimsLog.StackTraceIs = ex.StackTrace;
            //    objClaimsLog.IUDateTime = DateTime.Now;
            //    //create log record
            //    objSupport.ClaimsDocsLogCreate(objClaimsLog, AppConfig.CorrespondenceDBConnectionString);

            //    //cleanup
            //    objClaimsLog = null;
            //    objSupport = null;
            //}
            //finally
            //{
            //}

        }//end : cmdDeserializeXML_Click

    }//end : public partial class ClaimsDocsTestPage : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.TestHarness
