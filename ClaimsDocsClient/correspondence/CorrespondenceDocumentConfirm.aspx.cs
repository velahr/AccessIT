using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient;
using ClaimsDocsClient.AppClasses;
using System.Text;

namespace ClaimsDocsClient.correspondence
{
    public partial class CorrespondenceDocumentConfirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

                    //display the document code and desc
                    lblDocumentCode.Text = Request.Form["DocumentCode"].Trim();
                    lblDocumentDesc.Text = Request.Form["DocumentDesc"].Trim();

                    //display the Approval Message if this document requires approval
                    if (Request.Form["reviewFlag"].ToUpper().Trim().Equals("YES"))
                    {
                        lblApproveMsg.ForeColor = System.Drawing.Color.Red;
                        lblApproveMsg.Text = "This Document requires approval by a Manager!";
                    }

                    //cleanup
                    docdefid.Value = strDocId;
                    state.Value = strStateId;

                    ClearSessionFieldValues();

                }
                else
                {
                    // process the submit or done button click
                    
                    //from this point on we will use "post" instead of "get" 
                    //so the query strings will not be visibile or needed
                    //the next page is aware of this
                    String strUrl = "CorrespondenceUserGroup.aspx?" + BuildQueryString();
                    try
                    {
                        ClearSessionFieldValues();
                        // I have no idea why this throws a call stack error WTH is that?
                        Response.Redirect(strUrl, true);
                    }
                    catch (Exception rEX)
                    { }
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

        //define method : BuildQueryString
        private String BuildQueryString()
        {
            //declare variables
            string strQueryString =  string.Empty;
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = null;

            try
            {
                //get document request session information
                objDocGenerationRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];
                //check for information
                if (objDocGenerationRequest != null)
                {
                    //build the querystring
                    strQueryString = "PolicyNo=" + objDocGenerationRequest.PolicyNumber;
                    strQueryString = strQueryString + "&ClaimNo=" + objDocGenerationRequest.ClaimNumber;
                    strQueryString = strQueryString + "&ContactNo=" + objDocGenerationRequest.ContactNumber;
                    strQueryString = strQueryString + "&ContactType=" + objDocGenerationRequest.ContactType;
                    strQueryString = strQueryString + "&UserID=" + objDocGenerationRequest.UserName;
                }
                return strQueryString;
            }
            catch (Exception ex)
            {
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : BuildQueryString() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objSupport.ClaimsDocsLogCreate(objClaimsLog, AppConfig.CorrespondenceDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objSupport = null;
                return string.Empty;
            }
            finally
            {
            }
        }//end method : BuildQueryString

        //define method : ClearSessionFieldValues
        private void ClearSessionFieldValues()
        {
            String strDocId = Request.Form["docdefid"].ToString();
            proxyCDDocumentField.CDDocumentFieldClient objDocFieldClient = new ClaimsDocsClient.proxyCDDocumentField.CDDocumentFieldClient();
            List<proxyCDDocumentField.DocumentField> lstDocFields;
            String strSQL = String.Empty;

            // the user has clicked the done button so we want to make sure that
            // the session variables for the dynamic fields have been zapped
            // just incase they select the same document to add again, if we didn't do
            // this and the user selects the same document, the cached values from
            // the previous document will be displayed/used and that is BAAAD!
            try
            {
                //build sql string
                strSQL = "Select DocumentFieldID, DocumentID, FieldNameIs, FieldTypeIs,";
                strSQL = strSQL + " IsFieldRequired, FieldDescription, IUDateTime";
                strSQL = strSQL + " FROM dbo.tblDocumentField";
                strSQL = strSQL + " WHERE DocumentID = " + strDocId.ToString();
                strSQL = strSQL + " Order By DocumentFieldID";

                lstDocFields = objDocFieldClient.DocumentFieldSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);

                int ndx = 0;

                while (ndx < lstDocFields.Count)
                {
                    HttpContext.Current.Session.Remove("fld" + ndx.ToString()); // = null;
                    ndx++;
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
                objClaimsLog.MessageIs = "Method : ClearSessionFieldValues() ";
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

        }//end method : ClearSessionFieldValues


    }
}
