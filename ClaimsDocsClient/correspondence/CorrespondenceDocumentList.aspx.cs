using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient;
using ClaimsDocsClient.AppClasses;
using System.IO;
using System.Text;

namespace ClaimsDocsClient.correspondence
{
    public partial class CorrespondenceDocumentList : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables
            bool blnResult = true;
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = new proxClaimsDocGenerator.DocGenerationRequest();

            try
            {
                //check for postback
                if (this.IsPostBack == false)
                {
                    //place claims docs request information into session variable
                    objDocGenerationRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];

                    //place group selection information into request session object
                    if (string.IsNullOrEmpty(Request.Params["GroupID"]) == true)
                    {
                        //indicate failure
                        blnResult = false;
                    }
                    else
                    {
                        //get group id
                        objDocGenerationRequest.GroupID = int.Parse(Request.Params["GroupID"].ToString());
                    }

                    if (string.IsNullOrEmpty(Request.Params["GroupName"]) == true)
                    {
                        //indicate failure
                        blnResult = false;
                    }
                    else
                    {
                        //get user
                        objDocGenerationRequest.GroupName = Request.Params["GroupName"].ToString();
                    }

                    //save updated document request information
                    Session["DocGenerationRequest"] = objDocGenerationRequest;

                    //check results
                    if (objDocGenerationRequest != null)
                    {
                        //show request header information
                        this.lblUser.Text = objDocGenerationRequest.UserName;
                        this.lblMode.Text = objDocGenerationRequest.RunMode;
                        this.lblDepartment.Text = objDocGenerationRequest.UserDepartment;
                        this.lblClaimNumber.Text = objDocGenerationRequest.ClaimNumber.ToString();
                        this.lblProgramCode.Text = objDocGenerationRequest.PolicyNumber.Substring(0, 3);
                        this.lblAddressee.Text = objDocGenerationRequest.AddresseeName;

                        //refresh document list
                        DocumentListRefresh(objDocGenerationRequest.GroupID);
                        //display buttons
                        DisplayButtons();
                    }
                    else
                    {
                        //show error message
                        this.lblMessage.Text = "Unable to retrieve ClaimsDocs request from session.";
                    }
                }//end : if (this.IsPostBack == false)
            }
            catch (Exception ex)
            {
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
        }//end : Page_Load

        //define method : DocumentListRefresh
        public bool DocumentListRefresh(int intDocumentGroupID)
        {
            //declare variables
            bool blnResult = true;
            proxyCDDocument2.CDDocumentClient objDocumentClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            List<proxyCDDocument2.Document> listDocument;
            string strSQL = "";

            try
            {
                //build sql string

                //show all documents if the DocumentGroup is LIT=3
                if (intDocumentGroupID != 3)
                {
                    strSQL = strSQL + " Select ";
                    strSQL = strSQL + "     dbo.tblDocument.DocumentID,    ";
                    strSQL = strSQL + "     dbo.tblDocument.DocumentCode,     ";
                    strSQL = strSQL + "     dbo.tblDocument.Description,    ";
                    strSQL = strSQL + "     Case IsNull(dbo.viwDocumentFieldDistinct.DocumentID,0)    ";
                    strSQL = strSQL + "         When 0 Then 'No'    ";
                    strSQL = strSQL + "         Else 'Yes'    ";
                    strSQL = strSQL + "         End AS 'AdditionalDataRequired',   ";
                    strSQL = strSQL + "     Case dbo.tblDocument.ProofOfMailing    ";
                    strSQL = strSQL + "         When 'N' Then 'No'    ";
                    strSQL = strSQL + "         When 'Y' Then 'Yes'    ";
                    strSQL = strSQL + "     End As ProofOfMailing,    ";
                    strSQL = strSQL + "     dbo.tblDocument.IUDateTime    ";
                    strSQL = strSQL + " FROM     ";
                    strSQL = strSQL + "     dbo.tblDocument    ";
                    strSQL = strSQL + " INNER JOIN ";
                    strSQL = strSQL + "     dbo.tblDocumentGroup ON  ";
                    strSQL = strSQL + " dbo.tblDocument.DocumentID = dbo.tblDocumentGroup.DocumentID ";
                    strSQL = strSQL + " LEFT OUTER JOIN    ";
                    strSQL = strSQL + "     dbo.viwDocumentFieldDistinct    ";
                    strSQL = strSQL + " ON      ";
                    strSQL = strSQL + "     dbo.tblDocument.DocumentID = dbo.viwDocumentFieldDistinct.DocumentID     ";
                    strSQL = strSQL + " Where   ";
                    strSQL = strSQL + "     dbo.tblDocumentGroup.GroupID=" + intDocumentGroupID.ToString();
                    strSQL = strSQL + " And ";
                    strSQL = strSQL + "     dbo.tblDocument.Active='Y'";
                    strSQL = strSQL + " Order By     ";
                    strSQL = strSQL + "     DocumentCode     ";
                }
                else
                {
                    strSQL = strSQL + " Select ";
                    strSQL = strSQL + "     dbo.tblDocument.DocumentID,    ";
                    strSQL = strSQL + "     dbo.tblDocument.DocumentCode,     ";
                    strSQL = strSQL + "     dbo.tblDocument.Description,    ";
                    strSQL = strSQL + "     Case IsNull(dbo.viwDocumentFieldDistinct.DocumentID,0)    ";
                    strSQL = strSQL + "         When 0 Then 'No'    ";
                    strSQL = strSQL + "         Else 'Yes'    ";
                    strSQL = strSQL + "         End AS 'AdditionalDataRequired',   ";
                    strSQL = strSQL + "     Case dbo.tblDocument.ProofOfMailing    ";
                    strSQL = strSQL + "         When 'N' Then 'No'    ";
                    strSQL = strSQL + "         When 'Y' Then 'Yes'    ";
                    strSQL = strSQL + "     End As ProofOfMailing,    ";
                    strSQL = strSQL + "     dbo.tblDocument.IUDateTime    ";
                    strSQL = strSQL + " FROM     ";
                    strSQL = strSQL + "     dbo.tblDocument    ";
                    strSQL = strSQL + " RIGHT OUTER JOIN ";
                    strSQL = strSQL + "     dbo.viwDocumentFieldDistinct ON dbo.tblDocument.DocumentID = dbo.viwDocumentFieldDistinct.DocumentID     ";
                    strSQL = strSQL + " ";
                    strSQL = strSQL + " Where   ";
                    strSQL = strSQL + "     dbo.tblDocument.Active='Y'";
                    strSQL = strSQL + " Order By     ";
                    strSQL = strSQL + "     DocumentCode     ";
                }

                //get document list
                listDocument = objDocumentClient.DocumentAdjusterListSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);

                //assign document list to grid data source
                this.gvwData.DataSource = listDocument;
                this.gvwData.DataBind();
                //show records found
                this.lblRecordCount.Text = listDocument.Count.ToString() + " record(s) found";
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
                objClaimsLog.MessageIs = "Method : DocumentListRefresh() ";
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

        //define method : DocumentSortedListRefresh
        public bool DocumentSortedListRefresh(int intGroupID, string strOrderByClause)
        {
            //declare variables
            bool blnResult = true;
            proxyCDDocument2.CDDocumentClient objDocumentClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            List<proxyCDDocument2.Document> listDocument;
            string strSQL = "";

            try
            {
                //build sql string
                strSQL = strSQL + " Select ";
                strSQL = strSQL + "     dbo.tblDocument.DocumentID,    ";
                strSQL = strSQL + "     dbo.tblDocument.DocumentCode,     ";
                strSQL = strSQL + "     dbo.tblDocument.Description,    ";
                strSQL = strSQL + "     Case IsNull(dbo.viwDocumentFieldDistinct.DocumentID,0 )   ";
                strSQL = strSQL + "         When 0 Then 'No'    ";
                strSQL = strSQL + "         Else 'Yes'    ";
                strSQL = strSQL + "         End AS 'AdditionalDataRequired',   ";
                strSQL = strSQL + "     Case dbo.tblDocument.ProofOfMailing    ";
                strSQL = strSQL + "         When 'N' Then 'No'    ";
                strSQL = strSQL + "         When 'Y' Then 'Yes'    ";
                strSQL = strSQL + "     End As ProofOfMailing,    ";
                strSQL = strSQL + "     dbo.tblDocument.IUDateTime    ";
                strSQL = strSQL + " FROM     ";
                strSQL = strSQL + "     dbo.tblDocument    ";
                strSQL = strSQL + " INNER JOIN ";
                strSQL = strSQL + "     dbo.tblDocumentGroup ON  ";
                strSQL = strSQL + " dbo.tblDocument.DocumentID = dbo.tblDocumentGroup.DocumentID ";
                strSQL = strSQL + " LEFT OUTER JOIN    ";
                strSQL = strSQL + "     dbo.viwDocumentFieldDistinct    ";
                strSQL = strSQL + " ON      ";
                strSQL = strSQL + "     dbo.tblDocument.DocumentID = dbo.viwDocumentFieldDistinct.DocumentID     ";

                //show all documents if the DocumentGroup is LIT=3
                if (intGroupID != 3)
                {
                    strSQL = strSQL + " Where   ";
                    strSQL = strSQL + "     dbo.tblDocumentGroup.GroupID=" + intGroupID.ToString();
                }

                strSQL = strSQL + " Order By " + strOrderByClause;

                //get document list
                listDocument = objDocumentClient.DocumentAdjusterListSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);

                //assign document list to grid data source
                this.gvwData.DataSource = listDocument;
                this.gvwData.DataBind();
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
                objClaimsLog.MessageIs = "Method : DocumentSortedListRefresh() ";
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
        }//end : DocumentSortedListRefresh

        //define method : DisplayButtons
        public bool DisplayButtons()
        {
            //declare variables
            bool blnResult = true;
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = new proxClaimsDocGenerator.DocGenerationRequest();
            StringBuilder sbrQueryString = new StringBuilder();
            StringBuilder sbrHTMLButton = new StringBuilder();

            try
            {
                //place claims docs request information into session variable
                objDocGenerationRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];

                //check for results
                if (objDocGenerationRequest != null)
                {

                    //build query string
                    sbrQueryString.Append("CorrespondenceUserGroup.aspx?");
                    sbrQueryString.Append("PolicyNo=" + objDocGenerationRequest.PolicyNumber);
                    sbrQueryString.Append("&ClaimNo=" + objDocGenerationRequest.ClaimNumber);
                    sbrQueryString.Append("&ContactNo=" + objDocGenerationRequest.ContactNumber);
                    sbrQueryString.Append("&ContactType=" + objDocGenerationRequest.ContactType);
                    sbrQueryString.Append("&UserID=" + objDocGenerationRequest.UserName);
                    sbrQueryString.Append("&GroupID=" + objDocGenerationRequest.GroupID);

                    //build button html
                    sbrHTMLButton.Append("<input style=\"width: 8em; text-align: center\" class=\"button\" type=\"button\" value=\"Back\" onclick=\"window.location.replace(\'");
                    sbrHTMLButton.Append(sbrQueryString.ToString() + "')\")");
                    this.divButtons.InnerHtml = sbrHTMLButton.ToString();
                }
                else
                {
                    //show error message
                    this.lblMessage.Text = "Unable to retrieve ClaimsDocs request from session. Can not build buttons.";
                }

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

        //define : gvwData_SelectedIndexChanged
        protected void gvwData_SelectedIndexChanged(object sender, EventArgs e)
        {
            //declare variables

            try
            {
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
                objClaimsLog.MessageIs = "Method : gvwData_SelectedIndexChanged() ";
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

        }//end : gvwData_SelectedIndexChanged

        //define : gvList_PageIndexChanging
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //declare variables
            int intDocumentGroupID = 0;

            try
            {
                //get group id
                if (string.IsNullOrEmpty(Request.Params["GroupID"]) == true)
                {
                    //indicate failure
                    intDocumentGroupID = 0;
                }
                else
                {
                    //get group id
                    intDocumentGroupID = int.Parse(Request.Params["GroupID"].ToString());
                }
                //set current page index
                gvwData.PageIndex = e.NewPageIndex;
                //bind data to grid
                DocumentListRefresh(intDocumentGroupID);
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
                objClaimsLog.MessageIs = "Method : gvList_PageIndexChanging() ";
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
        }//end : gvList_PageIndexChanging

        //define method : SetGridProperties
        private void SetGridProperties()
        {
            try
            {
                //setup grid properties
            }
            catch (Exception ex)
            {

                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : SetGridProperties() ";
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
        }//end : SetGridProperties

        //define method:Grid_Change
        public void Grid_Change(object sender, DataGridPageChangedEventArgs e)
        {
            //declare variables
            try
            {
            }
            catch (Exception ex)
            {
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : Grid_Change() ";
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
        }//end : Grid_Change

        //define method : lnkSortByDocumentCode_Click
        protected void lnkSortByDocumentCode_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";
            int intGroupID = 0;
            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;
                //get group id
                intGroupID = int.Parse(Request.Params["GroupID"].ToString());

                //toggle sort order
                if (strSortOrder.Equals("DocumentCode Desc") == true)
                {
                    strSortOrder = "DocumentCode Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("DocumentCode Asc") == true)
                {
                    strSortOrder = "DocumentCode Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "DocumentCode Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }

                //update list sort order
                DocumentSortedListRefresh(intGroupID, strSortOrder);
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
                objClaimsLog.MessageIs = "Method : lnkSortByDocumentCode_Click() ";
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
        }//end : lnkSortByDocumentCode_Click

        //define method : lnkSortByDescription_Click
        protected void lnkSortByDescription_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";
            int intGroupID = 0;
            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;
                //get group id
                intGroupID = int.Parse(Request.Params["GroupID"].ToString());


                //toggle sort order
                if (strSortOrder.Equals("Description Desc") == true)
                {
                    strSortOrder = "Description Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("Description Asc") == true)
                {
                    strSortOrder = "Description Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "Description Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }

                //update list sort order
                DocumentSortedListRefresh(intGroupID, strSortOrder);
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
                objClaimsLog.MessageIs = "Method : lnkSortByDescription_Click() ";
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
        }//end : lnkSortByDescription_Click


    }//end : public partial class CorrespondenceDocumentList : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.correspondence
