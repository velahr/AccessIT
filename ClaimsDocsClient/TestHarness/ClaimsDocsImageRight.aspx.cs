using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace ClaimsDocsClient.TestHarness
{
    public partial class ClaimsDocsImageRight : System.Web.UI.Page
    {

        #region Support Classes

        public class ImageRightClaimRequest
        {
            public bool RequestResult { get; set; }
            public string ImageRightWSUrl { get; set; }
            public string ImageRightLogin { get; set; }
            public string ImageRightPassword { get; set; }
            public string SourceDocumentPath { get; set; }
            public string IRDocumentPath { get; set; }
            public string ClaimsNumber { get; set; }
            public string ClaimsDocsDocID { get; set; }
            public string ImageRightClaimsDrawer { get; set; }
            public string FileNumber { get; set; }
            public string PackType { get; set; }
            public string DocType { get; set; }
            public string CaptureDate { get; set; }
            public string FolderName { get; set; }
            public string DateOfLoss { get; set; }
            public string DevicePath { get; set; }
            public string DocumentKey { get; set; }
            public string Reason { get; set; }
            public bool CreateTask { get; set; }
            public string FlowID { get; set; }
            public string StepID { get; set; }
            public string Description { get; set; }
            public string Priority { get; set; }
            public string TDIN { get; set; }
        }//end : ImageRightPolicyRequest

        #endregion


        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

            //declare variables
            try
            {
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }//end : Page_Load

        //end : protected void cmdSaveDocument_Click(object sender, EventArgs e)
        protected void cmdSaveDocument_Click(object sender, EventArgs e)
        {
            //declare variables
            proxClaimsDocGenerator.ImageRightClaimRequest objIRRequest = new proxClaimsDocGenerator.ImageRightClaimRequest();
            proxClaimsDocGenerator.ClaimsDocsGeneratorClient objClaimsDocs = new ClaimsDocsClient.proxClaimsDocGenerator.ClaimsDocsGeneratorClient();
 
            try
            {
                //gather ImageRight save request values
                objIRRequest.RequestResult = true;
                objIRRequest.ImageRightWSUrl = this.txtImageRightWSUrl.Text;
                objIRRequest.ImageRightLogin = this.txtImageRightLogin.Text;
                objIRRequest.ImageRightPassword = this.txtImageRightPassword.Text;

                objIRRequest.SourceDocumentPath = this.txtSourceDocumentPath.Text;
                objIRRequest.ClaimsNumber = this.txtClaimNumber.Text;
                objIRRequest.ClaimsDocsDocID = this.txtCDDocumentID.Text;
                objIRRequest.ImageRightClaimsDrawer = this.txtImageRightClaimsDrawer.Text;
                objIRRequest.FileNumber = this.txtFileNumber.Text;
                objIRRequest.PackType = this.txtPackType.Text;
                objIRRequest.DocType = this.txtDocType.Text;
                objIRRequest.CaptureDate = this.txtCaptureDate.Text;
                objIRRequest.FolderName = this.txtFolderName.Text;
                objIRRequest.DateOfLoss = this.txtDateOfLoss.Text;
                objIRRequest.Reason = this.txtReason.Text;

                objIRRequest.CreateTask = this.chkCreateTask.Checked;
                objIRRequest.FlowID = this.txtFlowID.Text;
                objIRRequest.StepID = this.txtStepID.Text;
                objIRRequest.Description = this.txtDescription.Text;
                objIRRequest.Priority = this.txtPriority.Text;

                //save image right document
                objIRRequest = objClaimsDocs.ImageRightSaveDocument(objIRRequest);
                //check results
                if (objIRRequest.RequestResult == true)
                {
                    //get image right document
                    objIRRequest = objClaimsDocs.ImageRightGetDocument(objIRRequest);
                    //check result
                    if (objIRRequest.RequestResult == true)
                    {
                        //show link
                        this.divDocmentLink.InnerHtml = "<font face='Calibri' size='4'><a href='" + objIRRequest.IRDocumentPath + "'>Click here to view document stored in ImageRight</a></font>";

                    }
                }
            }
            catch (Exception ex)
            {
                //show error
                this.lblMessage.Text = ex.Message;
            }
            finally
            {
                //cleanup
                if (objClaimsDocs.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objClaimsDocs.Close();
                }
                objClaimsDocs = null;
            }

        }//end : protected void cmdSaveDocument_Click(object sender, EventArgs e)


        ////define method : ImageRightSaveDocument
        //public ImageRightClaimRequest ImageRightSaveDocument(proxClaimsDocGenerator.ImageRightClaimRequest objIRRequest)
        //{
        //    //declare variables
        //    proxClaimsDocGenerator.ImageRightClaimRequest

        //    ImageRightWS.IRWebService proxImageRightWS = new wscs.ImageRightWS.IRWebService();
        //    ImageRightWS.IRFile objIRFolderData = new wscs.ImageRightWS.IRFile();
        //    ImageRightWS.IRPage objIRPage = new wscs.ImageRightWS.IRPage();
        //    ImageRightWS.IRPage objIRPageData = null;
        //    ImageRightWS.IRImage objIRImage = new wscs.ImageRightWS.IRImage();
        //    ImageRightWS.IRTask objIRTask = null;
        //    FileStream objFileStream = null;
        //    string strNewTaskID = "";
        //    StringBuilder sbrResult = new StringBuilder();

        //    try
        //    {
        //        //setup file folder
        //        objIRFolderData.Foldernumber = objIRRequest.FileNumber;
        //        objIRFolderData.Drawer = objIRRequest.ImageRightClaimsDrawer;
        //        objIRFolderData.Foldername = objIRRequest.FolderName;
        //        objIRFolderData.Userdata1 = objIRRequest.ClaimsDocsDocID;
        //        objIRFolderData.Userdata2 = objIRRequest.FileNumber;

        //        //setup page
        //        objIRPage.Drawer = objIRRequest.ImageRightClaimsDrawer;
        //        objIRPage.Foldernumber = objIRRequest.FileNumber;
        //        objIRPage.Packagetype = int.Parse(objIRRequest.PackType);
        //        objIRPage.Doctype = objIRRequest.DocType;
        //        objIRPage.Reason = objIRRequest.Reason;

        //        //setup image
        //        ImageRightWS.IRImage objImage = new ImageRightWS.IRImage();
        //        objIRImage.Format = objIRRequest.SourceDocumentPath.Substring(objIRRequest.SourceDocumentPath.Length - 3, 3).ToUpper();

        //        //setup file stream
        //        objFileStream = new FileStream(objIRRequest.SourceDocumentPath, FileMode.Open, FileAccess.Read);
        //        byte[] filebites = new byte[objFileStream.Length];
        //        objFileStream.Read(filebites, 0, filebites.Length);
        //        objIRImage.ImageData = filebites;
        //        objFileStream.Close();
        //        ImageRightWS.IRImage[] imgs = new ImageRightWS.IRImage[1];
        //        imgs[0] = objIRImage;
        //        ImageRightWS.IRPage[] objDoc = null;

        //        //setup service
        //        proxImageRightWS = new wscs.ImageRightWS.IRWebService();
        //        proxImageRightWS.Url = objIRRequest.ImageRightWSUrl;
        //        objDoc = proxImageRightWS.AddDocumentExt(objIRRequest.ImageRightLogin,
        //                                                    objIRRequest.ImageRightPassword,
        //                                                    objIRFolderData,
        //                                                    objIRPage,
        //                                                    imgs
        //                                                 );

        //        //get results
        //        objIRRequest.TDIN = objDoc[0].Tempdin;
        //        objIRRequest.DocumentKey = objIRRequest.TDIN;


        //        //check for task creation
        //        if (objIRRequest.CreateTask == true)
        //        {

        //            //Initialize Task data object
        //            objIRTask = new wscs.ImageRightWS.IRTask();
        //            //setup task data object
        //            objIRTask.Userkey2 = objIRRequest.TDIN;
        //            objIRTask.UserID = "NULL";
        //            objIRTask.FlowID = int.Parse(objIRRequest.FlowID);
        //            objIRTask.StepID = int.Parse(objIRRequest.StepID);
        //            objIRTask.Description = "POS Docs";
        //            objIRTask.Priority = int.Parse(objIRRequest.Priority);

        //            //Initialize Page Data Object
        //            objIRPageData = new wscs.ImageRightWS.IRPage();
        //            objIRPageData.Tempdin = objIRRequest.TDIN;
        //            objIRPageData.Drawer = objIRRequest.ImageRightClaimsDrawer;
        //            objIRPageData.Foldernumber = objIRRequest.FileNumber;

        //            //setup web service reference
        //            proxImageRightWS = new wscs.ImageRightWS.IRWebService();
        //            proxImageRightWS.Url = objIRRequest.ImageRightWSUrl;

        //            //setup ImageRight Task
        //            objIRTask = proxImageRightWS.CreateTask(
        //                                                    objIRRequest.ImageRightLogin,
        //                                                    objIRRequest.ImageRightPassword,
        //                                                    objIRTask,
        //                                                    objIRPageData
        //                                                    );
        //            //get task id
        //            strNewTaskID = objIRTask.TaskID;

        //        }//end : if (objIRRequest.CreateTask == true)
        //        else
        //        {
        //            strNewTaskID = "No Task Created";
        //        }

        //        //log off service
        //        proxImageRightWS.UserLogoff();

        //        //build result string
        //        sbrResult.Append("Save Document to ImageRight Result TDIN : " + objIRRequest.TDIN);
        //        sbrResult.Append("<br>");
        //        sbrResult.Append("Task ID : " + strNewTaskID);

        //        //display results
        //        this.lblMessage.Text = sbrResult.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        //show error
        //        objIRRequest.RequestResult = false;
        //        this.lblMessage.Text = ex.Message;
        //    }
        //    finally
        //    {
        //    }

        //    //return result
        //    return (objIRRequest);
        //} //end : ImageRightSaveDocument

        ////define method : ImageRightGetDocument
        //public ImageRightClaimRequest ImageRightGetDocument(ImageRightClaimRequest objIRPolicyRequest)
        //{
        //    //declare variables
        //    ImageRightWS.IRWebService objIRWebService = null;
        //    string strDevicePath = "";

        //    try
        //    {
        //        //get service object
        //        objIRWebService = new ImageRightWS.IRWebService();
        //        objIRWebService.Url = objIRPolicyRequest.ImageRightWSUrl;
        //        ImageRightWS.IRPage[] objNewPages = null;
        //        ImageRightWS.IRDevice[] objDevice = null;

        //        //check for document key
        //        if (string.IsNullOrEmpty(objIRPolicyRequest.DocumentKey) == true)
        //        {
        //            //find documents
        //            objNewPages = objIRWebService.FindDocuments(
        //                                objIRPolicyRequest.ImageRightLogin,
        //                                objIRPolicyRequest.ImageRightPassword,
        //                                objIRPolicyRequest.ImageRightClaimsDrawer,
        //                                objIRPolicyRequest.FileNumber,
        //                                int.Parse(objIRPolicyRequest.PackType),
        //                                objIRPolicyRequest.DocType,
        //                                objIRPolicyRequest.CaptureDate,
        //                                "",
        //                                "",
        //                                "",
        //                                objIRPolicyRequest.ClaimsDocsDocID
        //                                );
        //        }
        //        else
        //        {
        //            //get pages
        //            objNewPages = objIRWebService.GetPages(
        //                                objIRPolicyRequest.ImageRightLogin,
        //                                objIRPolicyRequest.ImageRightPassword,
        //                                objIRPolicyRequest.ImageRightClaimsDrawer,
        //                                objIRPolicyRequest.FileNumber,
        //                                -1,
        //                                -1,
        //                                objIRPolicyRequest.DocumentKey
        //                                );
        //        }

        //        //check results
        //        if ((objNewPages == null) || (objNewPages.Length == 0))
        //        {
        //            objIRPolicyRequest.SourceDocumentPath = "";
        //            objIRPolicyRequest.DocumentKey = "";
        //        }
        //        else
        //        {
        //            //get the latest document instead of the earliest
        //            ImageRightWS.IRPage objPage = objNewPages[objNewPages.Length - 1];
        //            objDevice = objIRWebService.Devices(
        //                                objIRPolicyRequest.ImageRightLogin,
        //                                objIRPolicyRequest.ImageRightPassword
        //                                );

        //            foreach (ImageRightWS.IRDevice iDev in objDevice)
        //            {
        //                if (iDev.DeviceID == objPage.Deviceid)
        //                {
        //                    strDevicePath = iDev.DevicePath;
        //                }
        //            }

        //            objIRPolicyRequest.IRDocumentPath = strDevicePath + objPage.Filename + "." + objPage.Format;
        //            objIRPolicyRequest.DocumentKey = objPage.Tempdin;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //handle error
        //        objIRPolicyRequest.RequestResult = false;
        //        this.lblMessage.Text = ex.Message;
        //    }
        //    finally
        //    {
        //    }

        //    //return result
        //    return (objIRPolicyRequest);
        //}




    }//end : public partial class ClaimsDocsImageRight : System.Web.UI.Page

}//end : namespace ClaimsDocsClient.TestHarness

