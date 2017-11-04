using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient;
using System.Web.Security;
using ClaimsDocsClient.AppClasses;

namespace ClaimsDocsClient
{
    public partial class ClaimsDocsLogin : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables

            try
            {
                //determine if this is a postback
                if (Page.IsPostBack == true)
                {
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

        //define method : cmdLogin_Click
        protected void cmdLogin_Click(object sender, EventArgs e)
        {
            //declare variables
            proxyCDUser.CDUsersClient objCDUsersClient = new ClaimsDocsClient.proxyCDUser.CDUsersClient();
            proxyCDUser.User objUser = new proxyCDUser.User();
            proxyCDUser.User objUserIs = null;

            try
            {
                //gather user login information
                objUser.UserName = this.txtUserName.Text;
                objUser.UserPassword = this.txtPassword.Text;

                //check for valid user
                objUserIs = objCDUsersClient.UserLogin(objUser, AppConfig.CorrespondenceDBConnectionString);

                //check user login credentials
                if (objUserIs != null)
                {
                    //Save Applicable Session Information
                    ////Set user user id
                    //Session.Contents["UserName"] = objUser.UserName;
                    ////Store the user's fullname in a cookie for personalization purposes
                    //Response.Cookies.Add(new HttpCookie("UserName", objUser.UserName));
                    //Save user information to session
                    Session["CurrentUser"] = (proxyCDUser.User)objUserIs;
                    //Authenticate User and Login to applicable page
                    //Response.Write("HERE");
                    FormsAuthentication.RedirectFromLoginPage("ClaimsDocs", false);

                    //log the successful login
                    ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                    AppSupport objSupport = new AppSupport();
                    //fill log
                    objClaimsLog.ClaimsDocsLogID = 0;
                    objClaimsLog.LogTypeID = 1;
                    objClaimsLog.LogSourceTypeID = 2;
                    objClaimsLog.MessageIs = "User " + objUser.UserName + " successfully logged into the Claims Docs Client Application";
                    objClaimsLog.ExceptionIs = "None";
                    objClaimsLog.StackTraceIs = "None";
                    objClaimsLog.IUDateTime = DateTime.Now;
                    //create log record
                    objSupport.ClaimsDocsLogCreate(objClaimsLog, AppConfig.CorrespondenceDBConnectionString);

                    //cleanup
                    objClaimsLog = null;
                    objSupport = null;
                }
                else
                {
                    //handle error
                    ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                    AppSupport objSupport = new AppSupport();
                    //fill log
                    objClaimsLog.ClaimsDocsLogID = 0;
                    objClaimsLog.LogTypeID = 1;
                    objClaimsLog.LogSourceTypeID = 2;
                    objClaimsLog.MessageIs = "User " + objUser.UserName + " unsuccessfully attempted to log into the Claims Docs Client Application";
                    objClaimsLog.ExceptionIs = "None";
                    objClaimsLog.StackTraceIs = "None";
                    objClaimsLog.IUDateTime = DateTime.Now;
                    //create log record
                    objSupport.ClaimsDocsLogCreate(objClaimsLog, AppConfig.CorrespondenceDBConnectionString);

                    //cleanup
                    objClaimsLog = null;
                    objSupport = null;
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
                objClaimsLog.MessageIs = "Method : cmdLogin_Click() ";
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
                if (objCDUsersClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objCDUsersClient.Close();
                }
                objCDUsersClient = null;
                objUser = null;
            }
        }//end : protected void cmdLogin_Click(object sender, EventArgs e)

    }//end: public partial class ClaimsDocsLogin : System.Web.UI.Page
}//end : namespace ClaimsDocsClient
