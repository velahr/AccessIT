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
    public partial class CorrespondenceDocumentEntry : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables
            proxClaimsDocGenerator.DocGenerationRequest objDocGenerationRequest = null;

            try
            {
                //check for post back
                if (!IsPostBack)
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

                        //add controls
                        if (AddControls() == false)
                        {
                            //show error message
                            this.lblMessage.Text = "Unable to show document fields";
                        }

                        //show buttons
                        if (DisplayButtons() == false)
                        {
                            //show error message
                            this.lblMessage.Text = "Unable to display navigation buttons";
                        }
                    }
                    else
                    {
                    }
                }
                else
                {
                    //from this point on we will use "post" instead of "get" 
                    //so the query strings will not be visibile or needed
                    //the next page is aware of this
                    Server.Transfer("CorrespondenceDocumentReview.aspx", true);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }//end method : Page_Load

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
                    sbrQueryString.Append("CorrespondenceDocumentList.aspx?");
                    sbrQueryString.Append("PolicyNo=" + objDocGenerationRequest.PolicyNumber);
                    sbrQueryString.Append("&ClaimNo=" + objDocGenerationRequest.ClaimNumber);
                    sbrQueryString.Append("&ContactNo=" + objDocGenerationRequest.ContactNumber);
                    sbrQueryString.Append("&ContactType=" + objDocGenerationRequest.ContactType);
                    sbrQueryString.Append("&UserID=" + objDocGenerationRequest.UserName);
                    sbrQueryString.Append("&GroupID=" + objDocGenerationRequest.GroupID);
                    sbrQueryString.Append("&GroupName=" + objDocGenerationRequest.GroupName);

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

        //define method : AddControls
        private bool AddControls()
        {
            //declare variables
            bool blnResult = true;
            String strDocId = Request.QueryString["docdefid"].ToString();
            int docId = 0;
            String strStateId = Request.QueryString["state"].ToString();
            int stateId = 0;
            proxyCDDocumentField.CDDocumentFieldClient objDocFieldClient = new ClaimsDocsClient.proxyCDDocumentField.CDDocumentFieldClient();
            List<proxyCDDocumentField.DocumentField> lstDocFields;
            String strSQL = String.Empty;

            try
            {
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

                //build sql string
                strSQL = "Select DocumentFieldID, DocumentID, FieldNameIs, FieldTypeIs,";
                strSQL = strSQL + " IsFieldRequired, FieldDescription, IUDateTime";
                strSQL = strSQL + " FROM dbo.tblDocumentField";
                strSQL = strSQL + " WHERE DocumentID = " + docId.ToString();
                strSQL = strSQL + " Order By DocumentFieldID";

                lstDocFields = objDocFieldClient.DocumentFieldSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);

                int ndx = 0;

                while (ndx < lstDocFields.Count)
                {
                    //create the field label
                    Label lblCTL = new Label();
                    lblCTL.Text = lstDocFields[ndx].FieldDescription.ToString() + ":";
                    lblCTL.EnableViewState = true;

                    //create the table cell to hold the label
                    TableCell tblCellLabel = new TableCell();
                    tblCellLabel.HorizontalAlign = HorizontalAlign.Left;
                    tblCellLabel.VerticalAlign = VerticalAlign.Top;
                    tblCellLabel.Width = new Unit("1px");
                    tblCellLabel.Wrap = false;
                    //add the label to the cell
                    tblCellLabel.Controls.Add(lblCTL);

                    //create the input field type and it's values
                    TextBox txtBoxCTL = new TextBox();
                    txtBoxCTL.ID = lstDocFields[ndx].DocumentFieldID.ToString();

                    //create the table cell to hold the input
                    TableCell tblCellInput = new TableCell();
                    tblCellInput.Width = new Unit("1px");
                    tblCellInput.Wrap = false;

                    //Defect# 112 & 113 (LMS)
                    //create the field suffix label, this is mainly for the date type fields
                    Label lblSuffixCTL = new Label();
                    lblSuffixCTL.Text = "";
                    lblSuffixCTL.EnableViewState = true;

                    //Defect# 112 & 113 (LMS)
                    //create the table cell to hold the suffix label
                    TableCell tblSuffixCellLabel = new TableCell();
                    tblSuffixCellLabel.HorizontalAlign = HorizontalAlign.Center;
                    tblSuffixCellLabel.VerticalAlign = VerticalAlign.Middle;
                    tblSuffixCellLabel.Width = new Unit("100px");
                    tblSuffixCellLabel.Wrap = false;

                    //create the cell for validation controls
                    TableCell tblCellValidation = new TableCell();
                    tblCellValidation.HorizontalAlign = HorizontalAlign.Center;
                    tblCellValidation.VerticalAlign = VerticalAlign.Middle;
                    tblCellValidation.Width = new Unit("100px");
                    tblCellValidation.Wrap = true;

                    //create the required validator
                    if (lstDocFields[ndx].IsFieldRequired.ToUpper().Trim().Equals("Y"))
                    {
                        //this field is required, so add a required field validator
                        RequiredFieldValidator reqFieldVal = new RequiredFieldValidator();
                        reqFieldVal.Display = ValidatorDisplay.Dynamic;
                        reqFieldVal.Text = "*" + lstDocFields[ndx].FieldDescription.ToString() + " is required";
                        reqFieldVal.ControlToValidate = lstDocFields[ndx].DocumentFieldID.ToString();
                        reqFieldVal.EnableClientScript = true;
                        reqFieldVal.Enabled = true;
                        reqFieldVal.EnableViewState = true;
                        reqFieldVal.ToolTip = reqFieldVal.ErrorMessage;
                        reqFieldVal.Visible = true;
                        reqFieldVal.ID = "reqVal" + lstDocFields[ndx].DocumentFieldID.ToString();
                        //add the req field validator to the validation table cell
                        tblCellValidation.Controls.Add(reqFieldVal);
                    }

                    switch (lstDocFields[ndx].FieldTypeIs.ToString().ToUpper().Trim())
                    {
                        case "STRING":
                            txtBoxCTL.TextMode = TextBoxMode.SingleLine;
                            //since this field accepts strings no regExp validator is needed
                            break;
                        case "TEXT":
                            txtBoxCTL.TextMode = TextBoxMode.MultiLine;
                            txtBoxCTL.Height = new Unit("138px");
                            txtBoxCTL.Width = new Unit("149px");
                            txtBoxCTL.Text = "";
                            //since this field accepts strings no regExp validator is needed
                            break;
                        case "NUMBER":
                            txtBoxCTL.TextMode = TextBoxMode.SingleLine;
                            txtBoxCTL.Text = "";
                            //this field needs a regExp validator for numbers only
                            RegularExpressionValidator regExFieldVal = new RegularExpressionValidator();
                            regExFieldVal.Display = ValidatorDisplay.Dynamic;
                            regExFieldVal.Text = "*" + lstDocFields[ndx].FieldDescription.ToString() + " is numeric only";
                            regExFieldVal.ControlToValidate = lstDocFields[ndx].DocumentFieldID.ToString();
                            regExFieldVal.EnableClientScript = true;
                            regExFieldVal.Enabled = true;
                            regExFieldVal.EnableViewState = true;
                            regExFieldVal.ToolTip = regExFieldVal.ErrorMessage;
                            regExFieldVal.Visible = true;
                            regExFieldVal.ID = "regExVal" + lstDocFields[ndx].DocumentFieldID.ToString();
                            regExFieldVal.ValidationExpression = "\\d+";
                            //add the regEx field validator to the validation table cell
                            tblCellValidation.Controls.Add(regExFieldVal);

                            break;
                        case "DATE":
                            //Defect# 112 & 113 (LMS)
                            txtBoxCTL.TextMode = TextBoxMode.SingleLine;
                            txtBoxCTL.Text = "";
                            //this field needs a regExp validator for numbers only
                            RegularExpressionValidator regExFieldValDate = new RegularExpressionValidator();
                            regExFieldValDate.Display = ValidatorDisplay.Dynamic;
                            regExFieldValDate.Text = "*" + lstDocFields[ndx].FieldDescription.ToString() + " invalid format";
                            regExFieldValDate.ControlToValidate = lstDocFields[ndx].DocumentFieldID.ToString();
                            regExFieldValDate.EnableClientScript = true;
                            regExFieldValDate.Enabled = true;
                            regExFieldValDate.EnableViewState = true;
                            regExFieldValDate.ToolTip = regExFieldValDate.ErrorMessage;
                            regExFieldValDate.Visible = true;
                            regExFieldValDate.ID = "regExVal" + lstDocFields[ndx].DocumentFieldID.ToString();
                            regExFieldValDate.ValidationExpression = "^([1-9]|0[1-9]|1[012])[/]([1-9]|0[1-9]|[12][0-9]|3[01])[/][0-9]{4}$";
                            //add the regEx field validator to the validation table cell
                            tblCellValidation.Controls.Add(regExFieldValDate);

                            lblSuffixCTL.Text = "mm/dd/yyyy";

                            break;
                    }
                    // if there is a session variable for this element, use it and destroy it
                    if (HttpContext.Current.Session["fld" + ndx.ToString()] != null)
                    {
                        txtBoxCTL.Text = HttpContext.Current.Session["fld" + ndx.ToString()].ToString();
                        HttpContext.Current.Session.Remove("fld" + ndx.ToString()); // = null;
                    }
                    else
                    {
                        txtBoxCTL.Text = "";
                    }

                    //add the input to the cell
                    tblCellInput.Controls.Add(txtBoxCTL);

                    //add the label to the cell
                    tblSuffixCellLabel.Controls.Add(lblSuffixCTL);

                    //create the table row to which the cells will be added
                    TableRow tblRow = new TableRow();
                    //add the cells
                    tblRow.Controls.Add(tblCellLabel);
                    tblRow.Controls.Add(tblCellInput);
                    
                    //Defect# 112 & 113 (LMS)
                    //if the suffix field is empty, don't add it.
                    if (!lblSuffixCTL.Text.Equals(""))
                    { 
                        tblRow.Controls.Add(tblSuffixCellLabel); 
                    }
                    tblRow.Controls.Add(tblCellValidation);
       
                    //add the row to the parent table
                    Table1.Controls.AddAt(Table1.Controls.Count, tblRow);

                    ndx++;
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
                objClaimsLog.MessageIs = "Method : AddControls() ";
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

                docdefid.Value = strDocId;
                state.Value = strStateId;
                //clean up
                objDocFieldClient = null;
                lstDocFields = null;
            }

            //return results
            return (blnResult);
        } //end : AddControls

        //define method : IsInteger16
        private static bool IsInteger16(string theValue)
        {
            try
            {
                Convert.ToInt16(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        } //end : IsInteger16

        //define method : Button1_Click
        protected void Button1_Click(object sender, EventArgs e)
        {
        } //IsInteger

    }//end : public partial class CorrespondenceDocumentEntry : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.correspondence
