using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient.AppClasses;
using ClaimsDocsClient.proxyCDUser;
using System.Text;

namespace ClaimsDocsClient.correspondence
{
    public partial class CorrespondenceUserGroup : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = new proxClaimsDocGenerator.DocGenerationRequest();
            string strUserName = "";
            proxyCDSupport.CDSupportClient objCDSupport = new ClaimsDocsClient.proxyCDSupport.CDSupportClient();
            StringBuilder sbrMessage = new StringBuilder();
            bool blnResult = true;

            try
            {
               //place request information into session variables
                if (ClaimsDocsSessionSetup() == true)
                {
                    //place claims docs request information into session variable
                    objDocGenerationRequest = (proxClaimsDocGenerator.DocGenerationRequest)Session["DocGenerationRequest"];

                    //show request header information
                    this.lblUser.Text = objDocGenerationRequest.UserName;
                    this.lblMode.Text = objDocGenerationRequest.RunMode;
                    this.lblDepartment.Text = objDocGenerationRequest.UserDepartment;
                    this.lblClaimNumber.Text = objDocGenerationRequest.ClaimNumber.ToString();
                    this.lblProgramCode.Text = objDocGenerationRequest.PolicyNumber.Substring(0, 3);
                    this.lblAddressee.Text = objDocGenerationRequest.AddresseeName;
                    
                    //get username
                    strUserName = objDocGenerationRequest.UserName;

                    //valid addressee address
                    if (objCDSupport.ValidateAddressRegEx(objDocGenerationRequest.AddresseeName, objDocGenerationRequest.AddresseeAddressLine2, objDocGenerationRequest.AddresseeCity, objDocGenerationRequest.AddresseeState, objDocGenerationRequest.AddresseeZipCode) == true)
                    {
                        //show user groups
                        if (ShowUserGroups(strUserName) == false)
                        {
                            //show error message
                            this.lblMessage.Text = "Unable to show User Groups.";
                        }
                    }
                    else
                    {
                        //show error message
                        sbrMessage.Append("<table border='0' width='75%'>");

                        sbrMessage.Append("<tr>");
                        sbrMessage.Append("<td colspan='2'>");
                        sbrMessage.Append("<font color='red'><b>Invalid Address.&nbsp;");
                        //sbrMessage.Append("<br>");
                        sbrMessage.Append("Please use C4 and the address information below to fix the address.  ");
                        sbrMessage.Append("</b></font></td>");
                        sbrMessage.Append("</tr>");

                        sbrMessage.Append("<tr>");
                        sbrMessage.Append("<td colspan='2'>");
                        sbrMessage.Append("<hr>");
                        sbrMessage.Append("</td>");
                        sbrMessage.Append("</tr>");

                        sbrMessage.Append("<tr>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append("Addressee Name : ");
                        sbrMessage.Append("</td>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append(objDocGenerationRequest.AddresseeName);
                        sbrMessage.Append("</td>");
                        sbrMessage.Append("</tr>");

                        sbrMessage.Append("<tr>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append("Address Line 1 : ");
                        sbrMessage.Append("</td>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append(objDocGenerationRequest.AddresseeAddressLine1);
                        sbrMessage.Append("</td>");
                        sbrMessage.Append("</tr>");

                        sbrMessage.Append("<tr>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append("City : ");
                        sbrMessage.Append("</td>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append(objDocGenerationRequest.AddresseeCity);
                        sbrMessage.Append("</td>");
                        sbrMessage.Append("</tr>");

                        sbrMessage.Append("<tr>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append("State : ");
                        sbrMessage.Append("</td>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append(objDocGenerationRequest.AddresseeState);
                        sbrMessage.Append("</td>");
                        sbrMessage.Append("</tr>");

                        sbrMessage.Append("<tr>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append("Zip Code : ");
                        sbrMessage.Append("</td>");
                        sbrMessage.Append("<td>");
                        sbrMessage.Append(objDocGenerationRequest.AddresseeZipCode);
                        sbrMessage.Append("</td>");
                        sbrMessage.Append("</tr>");
                        
                        sbrMessage.Append("</table>");

                        this.divMessage.InnerHtml = sbrMessage.ToString();

                    }
                }
                else
                {
                    //show error message
                    this.lblMessage.Text = "Unable to save request to session.";
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
                if (objCDSupport.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objCDSupport.Close();
                }
                objCDSupport = null;

            }
        }//end : protected void Page_Load(object sender, EventArgs e)

        //define method : ClaimsDocsSessionSetup
        public bool ClaimsDocsSessionSetup()
        {
            //declare variables
            bool blnResult = true;
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = new proxClaimsDocGenerator.DocGenerationRequest();
            proxClaimsDocGenerator.ClaimsDocsGeneratorClient objClaimsDocsGeneratorClient = new ClaimsDocsClient.proxClaimsDocGenerator.ClaimsDocsGeneratorClient();

            try
            {
                //collect request values
                if (string.IsNullOrEmpty(Request.Params["PolicyNo"].ToString()) == true)
                {
                    //indicate failure
                    blnResult = false;
                }
                else
                {
                    //get policy number
                    objDocGenerationRequest.PolicyNumber = Request.Params["PolicyNo"].ToString();
                }

                if (string.IsNullOrEmpty(Request.Params["ClaimNo"].ToString()) == true)
                {
                    //indicate failure
                    blnResult = false;
                }
                else
                {
                    //get claim number
                    objDocGenerationRequest.ClaimNumber = Request.Params["ClaimNo"].ToString();
                }

                if (string.IsNullOrEmpty(Request.Params["ContactNo"].ToString()) == true)
                {
                    //indicate failure
                    blnResult = false;
                }
                else
                {
                    //get contact number
                    objDocGenerationRequest.ContactNumber = int.Parse(Request.Params["ContactNo"].ToString());
                }

                if (string.IsNullOrEmpty(Request.Params["ContactType"].ToString()) == true)
                {
                    //indicate failure
                    blnResult = false;
                }
                else
                {
                    //get contact type
                    objDocGenerationRequest.ContactType = int.Parse(Request.Params["ContactType"].ToString());
                }


                if (string.IsNullOrEmpty(Request.Params["UserID"].ToString()) == true)
                {
                    //indicate failure
                    blnResult = false;
                }
                else
                {
                    //get user
                    objDocGenerationRequest.UserName = Request.Params["UserID"].ToString();
                }

                //Set Run Mode
                objDocGenerationRequest.RunMode = AppConfig.RunMode.ToString();
                objDocGenerationRequest = objClaimsDocsGeneratorClient.DocGenerationRequestInformationGet(objDocGenerationRequest);

                //save document generation request information in session
                Session["DocGenerationRequest"] = objDocGenerationRequest;
            }
            catch (Exception ex)
            {
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : ClaimsDocsSessionSetup() ";
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
                //clean up
                if (objClaimsDocsGeneratorClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objClaimsDocsGeneratorClient.Close();
                }
                objClaimsDocsGeneratorClient = null;
            }

            //return result
            return (blnResult);

        }//end : ClaimsDocsSessionSetup

        //define method : ShowUserGroups
        bool ShowUserGroups(string strUserName)
        {
            //declare variables
            bool blnResult = true;
            CDUsersClient objUser = new CDUsersClient();
            List<UserGroup> listUserGroups = new List<UserGroup>();

            try
            {
                //setup user
                listUserGroups = objUser.UserGroupSearchByUserName(strUserName, AppConfig.CorrespondenceDBConnectionString);

                //check for results
                if (listUserGroups.Count > 0)
                {
                    //apply results
                    this.gvwData.DataSource = listUserGroups;
                    this.gvwData.DataBind();
                    this.divMessage.InnerHtml = "User " + strUserName + " is a member of " + listUserGroups.Count.ToString() + " group(s)";
                }
                else
                {
                    this.divMessage.InnerHtml = "No groups found for user : " + strUserName;
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
                if (objUser.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objUser.Close();
                }
                objUser = null;

            }

            //return result
            return (blnResult);

        }//end : ShowUserGroups

    }//end : public partial class CorrespondenceUserGroup : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.correspondence
