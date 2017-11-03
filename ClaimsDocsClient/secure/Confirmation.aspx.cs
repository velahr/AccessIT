using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using ClaimsDocsClient.AppClasses;

namespace ClaimsDocsClient.secure
{
    public partial class Confirmation : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables
            string strConfirmationType = "";            

            try
            {
                //check for postback
                if (Page.IsPostBack == false)
                {
                    //get confirmation type
                    if(string.IsNullOrEmpty(Request.Params["confirmtype"])==false)
                    {
                        strConfirmationType = Request.Params["confirmtype"].ToString();
                    }
                    else
                    {
                        strConfirmationType  = "Unknown";
                    }

                    //show message based on confirmation type
                    ShowConfirmation(strConfirmationType);
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
        }//end : protected void Page_Load(object sender, EventArgs e)

        //define method : ShowConfirmation
        private void ShowConfirmation(string strConfirmationType)
        {
            //declare variables
            StringBuilder sbrMessage = new StringBuilder();

            try
            {
                //sbrHTMLButton.Append("<input style=\"width: 8em; text-align: center\" class=\"button\" type=\"button\" value=\"Back\" onclick=\"window.location.replace(\'");


                //build message based on confirmation type
                switch (strConfirmationType)
                {
                    case "docapproval":
                        sbrMessage.Append("<table border='0' >");
                        sbrMessage.Append("<tr>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append("<input style=\"width: 8em; text-align: center\" class=\"button\" type=\"button\" value=\"Back\" onclick=\"window.location.replace(\'ApprovalDocList.aspx\')\";>");
                        sbrMessage.Append("</td>");
                        
                        sbrMessage.Append("<td>");
                        sbrMessage.Append("");
                        sbrMessage.Append("");
                        sbrMessage.Append("<input style=\"width: 8em; text-align: center\" class=\"button\" type=\"button\" value=\"Exit\" onclick=\"window.close()\";>");
                        sbrMessage.Append("");
                        sbrMessage.Append("");
                        sbrMessage.Append("</td>");
                        
                        sbrMessage.Append("</tr>");
                        sbrMessage.Append("</table>");

                        break;

                    case "docdeclined":
                        sbrMessage.Append("<table border='0' >");
                        sbrMessage.Append("<tr>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append("<input style=\"width: 8em; text-align: center\" class=\"button\" type=\"button\" value=\"Back\" onclick=\"window.location.replace(\'ApprovalDocList.aspx\')\";>");
                        sbrMessage.Append("</td>");

                        sbrMessage.Append("<td>");
                        sbrMessage.Append("");
                        sbrMessage.Append("");
                        sbrMessage.Append("<input style=\"width: 8em; text-align: center\" class=\"button\" type=\"button\" value=\"Exit\" onclick=\"window.close()\";>");
                        sbrMessage.Append("");
                        sbrMessage.Append("");
                        sbrMessage.Append("</td>");

                        sbrMessage.Append("</tr>");
                        sbrMessage.Append("</table>");
                        break;

                    default:
                        break;


                }//end : switch (strConfirmationType)

                //show message
                this.divHTML.InnerHtml = sbrMessage.ToString();
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
                objClaimsLog.MessageIs = "Method : ShowConfirmation()";
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
        }//end : ShowConfirmation


    }//end : public partial class Confirmation : System.Web.UI.Page

}//end : namespace ClaimsDocsClient.secure
