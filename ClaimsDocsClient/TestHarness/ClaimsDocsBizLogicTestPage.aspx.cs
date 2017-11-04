using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Web.UI.WebControls;
using ClaimsDocsClient.AppClasses;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace ClaimsDocsClient.TestHarness
{
    public partial class ClaimsDocsBizLogicTestPage : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

        }//end : Page_Load

        //define method : cmdRunValidation_Click
        protected void cmdRunValidation_Click(object sender, EventArgs e)
        {
            //declare variables
            ClaimsDocsClient.proxyCDSupport.CDSupportClient objSupport  = new ClaimsDocsClient.proxyCDSupport.CDSupportClient();
            ClaimsDocsClient.proxyCDSupport.ValidateResult objValidateResult = new ClaimsDocsClient.proxyCDSupport.ValidateResult();
            StringBuilder sbrResult = new StringBuilder();
            string strPolicyNumber = "";
            int intClaimNumber = 0;
            int intContactNumber = 0;
            int intVehicleFound = 0;

            try
            {
                //gather values
                if (!string.IsNullOrEmpty(this.txtPolicyNumber.Text))
                {
                    strPolicyNumber = this.txtPolicyNumber.Text;
                }

                if (!string.IsNullOrEmpty(this.txtContactNumber.Text))
                {
                    intContactNumber = int.Parse(this.txtContactNumber.Text);
                }

                if (!string.IsNullOrEmpty(this.txtClaimNumber.Text))
                {
                    intClaimNumber = int.Parse(this.txtClaimNumber.Text);
                }

                if (!string.IsNullOrEmpty(this.txtVehicleFound.Text))
                {
                    intVehicleFound = int.Parse(this.txtVehicleFound.Text);
                }

                //validate wcf call
                objValidateResult = objSupport.ValidateWCFServiceCall();

                sbrResult.Append("<table width='100%' border='0'>");
                
                sbrResult.Append("  <tr>");
                sbrResult.Append("      <td valign='top'><b>");
                sbrResult.Append("          "   + objValidateResult.ValidationFocus);
                sbrResult.Append("      </td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("          "   + objValidateResult.ValidationResultMessage);
                sbrResult.Append("      </b></td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("");
                sbrResult.Append("      </td>");
                sbrResult.Append("  </tr>");

                objValidateResult = objSupport.ValidateMessageQueue();
                sbrResult.Append("  <tr>");
                sbrResult.Append("      <td valign='top'><b>");
                sbrResult.Append("          " + objValidateResult.ValidationFocus);
                sbrResult.Append("      </td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("          " + objValidateResult.ValidationResultMessage);
                sbrResult.Append("      </b></td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("");
                sbrResult.Append("      </td>");
                sbrResult.Append("  </tr>");

                objValidateResult = objSupport.ValidateXMLOutputLocation();
                sbrResult.Append("  <tr>");
                sbrResult.Append("      <td valign='top'><b>");
                sbrResult.Append("          " + objValidateResult.ValidationFocus);
                sbrResult.Append("      </td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("          " + objValidateResult.ValidationResultMessage);
                sbrResult.Append("      </b></td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("");
                sbrResult.Append("      </td>");
                sbrResult.Append("  </tr>");


                objValidateResult = objSupport.ValidateAMSPointToClaimsLinkedServer(intClaimNumber);
                sbrResult.Append("  <tr>");
                sbrResult.Append("      <td valign='top'> <b>");
                sbrResult.Append("          " + objValidateResult.ValidationFocus);
                sbrResult.Append("      </b></td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("          " + objValidateResult.ValidationResultMessage);
                sbrResult.Append("      </td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("");
                sbrResult.Append("      </td>");
                sbrResult.Append("  </tr>");


                objValidateResult = objSupport.ValidateAMSPointToGenesisLinkedServer(strPolicyNumber);
                sbrResult.Append("  <tr>");
                sbrResult.Append("      <td valign='top'> <b>");
                sbrResult.Append("          " + objValidateResult.ValidationFocus);
                sbrResult.Append("      </b></td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("          " + objValidateResult.ValidationResultMessage);
                sbrResult.Append("      </td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("");
                sbrResult.Append("      </td>");
                sbrResult.Append("  </tr>");

                objValidateResult = objSupport.ValidateClaimsPointToGenesisLinkedServer(intContactNumber,intVehicleFound);
                sbrResult.Append("  <tr>");
                sbrResult.Append("      <td valign='top'><b>");
                sbrResult.Append("          " + objValidateResult.ValidationFocus);
                sbrResult.Append("      </td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("          " + objValidateResult.ValidationResultMessage);
                sbrResult.Append("      </b></td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("");
                sbrResult.Append("      </td>");
                sbrResult.Append("  </tr>");

                objValidateResult = objSupport.ValidateClaimsDocsDBConnection();
                sbrResult.Append("  <tr>");
                sbrResult.Append("      <td valign='top'><b>");
                sbrResult.Append("          " + objValidateResult.ValidationFocus);
                sbrResult.Append("      </td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("          " + objValidateResult.ValidationResultMessage);
                sbrResult.Append("      </b></td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("");
                sbrResult.Append("      </td>");
                sbrResult.Append("  </tr>");

                objValidateResult = objSupport.ValidateCorrespondenceDBConnection();
                sbrResult.Append("  <tr>");
                sbrResult.Append("      <td valign='top'><b>");
                sbrResult.Append("          " + objValidateResult.ValidationFocus);
                sbrResult.Append("      </td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("          " + objValidateResult.ValidationResultMessage);
                sbrResult.Append("      </b></td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("");
                sbrResult.Append("      </td>");
                sbrResult.Append("  </tr>");

                objValidateResult = objSupport.ValidateAMSDBConnection();
                sbrResult.Append("  <tr>");
                sbrResult.Append("      <td valign='top'> <b>");
                sbrResult.Append("          " + objValidateResult.ValidationFocus);
                sbrResult.Append("      </b></td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("          " + objValidateResult.ValidationResultMessage);
                sbrResult.Append("      </td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("");
                sbrResult.Append("      </td>");
                sbrResult.Append("  </tr>");

                objValidateResult = objSupport.ValidateGenesisDBConnection();
                sbrResult.Append("  <tr>");
                sbrResult.Append("      <td valign='top'> <b>");
                sbrResult.Append("          " + objValidateResult.ValidationFocus);
                sbrResult.Append("      </b></td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("          " + objValidateResult.ValidationResultMessage);
                sbrResult.Append("      </td>");
                sbrResult.Append("      <td>");
                sbrResult.Append("");
                sbrResult.Append("      </td>");
                sbrResult.Append("  </tr>");
               
                sbrResult.Append("</table>");

                
                //show results
                this.divResult.InnerHtml = sbrResult.ToString();
            }
            catch (Exception ex)
            {
                //show error message
                this.divResult.InnerHtml = "Error Message :<br><br>"  + ex.Message + "<br><br>Stack Trace :<br><br>" + ex.StackTrace;
            }
            finally
            {
                //cleanup
                if (objSupport.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objSupport.Close();
                }

            }
        }

        //define method : cmdClientImpersonation_Click
        protected void cmdClientImpersonation_Click(object sender, EventArgs e)
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
                objClaimsLog.MessageIs = "Method : cmdClientImpersonation_Click() ";
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
        }//end : cmdClientImpersonation_Click

    }//end : public partial class ClaimsDocsBizLogicTestPage : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.TestHarness
