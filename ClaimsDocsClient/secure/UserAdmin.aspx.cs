using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient.AppClasses;

namespace ClaimsDocsClient.secure
{
    public partial class UserAdmin : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables
            int intState = -1;
            int intUserID = 0;

            try
            {
                //check for postback
                if (this.Page.IsPostBack == false)
                {
                    //show list items
                    ShowListItems();

                    //get state
                    if (int.TryParse(Request.Params["state"].ToString(), out intState) == true)
                    {
                        //get user id
                        if (int.TryParse(Request.Params["userid"].ToString(), out intUserID) == false)
                        {
                            intUserID = 0;
                        }
                    }
                    else
                    {
                        //no state
                        intState = -1;
                    }

                    //setup page
                    switch (intState)
                    {
                        case 1: //Add user
                            this.lblState.Text = intState.ToString();
                            this.lblUserID.Text = "0";
                            this.lblHeader.Text = "Add User";
                            break;

                        case 2: //Edit user
                            this.lblState.Text = intState.ToString();
                            this.lblUserID.Text = intUserID.ToString();
                            this.lblHeader.Text = "Edit User";
                            proxyCDUser.User objUser = new ClaimsDocsClient.proxyCDUser.User();

                            //show user
                            objUser.UserID = intUserID;
                            UserShow(objUser);
                            break;

                        default: //do nothing
                            break;
                    }//end : switch (intState)
                }//end : if (this.Page.IsPostBack == false)
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
            finally
            {

            }
        }

        //define method : cmdDo_Click
        protected void cmdDo_Click(object sender, EventArgs e)
        {
            //declare variables
            int intResult = 0;
            int intState = -1;
            int intUserID = 0;
            int intIndex = 0;
            proxyCDUser.CDUsersClient objUserClient = new ClaimsDocsClient.proxyCDUser.CDUsersClient();
            proxyCDUser.User objUser = new ClaimsDocsClient.proxyCDUser.User();
            List<proxyCDUser.UserGroup> listUserGroups = new List<ClaimsDocsClient.proxyCDUser.UserGroup>(); 

            try
            {
                //gather values
                intState = int.Parse(this.lblState.Text.ToString());
                intUserID = int.Parse(this.lblUserID.Text.ToString());

                //process request based on state
                switch (intState)
                {
                    case 1: //add user
                        //fill user
                        objUser.UserID = 0;

                        objUser.UserName = this.txtUserName.Text;
                        objUser.DepartmentID = int.Parse(this.cboDepartment.SelectedValue.ToString());
                        objUser.Administrator = this.cboAdministrator.SelectedValue;
                        objUser.Approver = this.cboApprover.SelectedValue;
                        objUser.Designer = this.cboDesigner.SelectedValue;
                        objUser.UserPassword = this.txtLoginPassword.Text;
                        objUser.Reviewer = this.cboReviewRequired.SelectedValue;
                        objUser.Phone = this.txtPhoneExtension.Text;
                        objUser.EMailAddress = this.txtEMailAddress.Text;
                        objUser.Title = this.txtTitle.Text;
                        objUser.SignatureName = this.txtSignatureName.Text;
                        objUser.Signature = this.txtSignatureFilePath.Text;

                        for (intIndex = 0; intIndex < this.lstGroups.Items.Count; intIndex++)
                        {
                            if (this.lstGroups.Items[intIndex].Selected == true)
                            {
                                proxyCDUser.UserGroup objUserGroup = new ClaimsDocsClient.proxyCDUser.UserGroup();
                                objUserGroup.UserID = 0;
                                objUserGroup.GroupID = int.Parse(this.lstGroups.Items[intIndex].Value.ToString());
                                objUserGroup.IUDateTime = DateTime.Now;
                                listUserGroups.Add(objUserGroup);
                            }
                        }

                        objUser.listUserGroup = listUserGroups;

                        if (this.chkActive.Checked == true)
                        {
                            objUser.Active = "Y";
                        }
                        else
                        {
                            objUser.Active = "N";
                        }

                        objUser.IUDateTime = DateTime.Now;

                        //add user
                        intResult = objUserClient.UserCreate(objUser, AppConfig.CorrespondenceDBConnectionString);
                        //check results
                        if (intResult == 0)
                        {
                        }
                        else
                        {
                            //change state to edit
                            intState = 2;
                            //save state and user id                            
                            this.lblState.Text = intState.ToString();
                            this.lblUserID.Text = intResult.ToString();
                        }
                        break;

                    case 2: //edit user
                        //fill user
                        objUser.UserID = int.Parse(this.lblUserID.Text);
                        objUser.UserName = this.txtUserName.Text;
                        objUser.DepartmentID = int.Parse(this.cboDepartment.SelectedValue.ToString());
                        objUser.Administrator = this.cboAdministrator.SelectedValue;
                        objUser.Approver = this.cboApprover.SelectedValue;
                        objUser.Designer = this.cboDesigner.SelectedValue;
                        objUser.UserPassword = this.txtLoginPassword.Text;
                        objUser.Reviewer = this.cboReviewRequired.SelectedValue;
                        objUser.Phone = this.txtPhoneExtension.Text;
                        objUser.EMailAddress = this.txtEMailAddress.Text;
                        objUser.Title = this.txtTitle.Text;
                        objUser.SignatureName = this.txtSignatureName.Text;
                        objUser.Signature = this.txtSignatureFilePath.Text;

                        for (intIndex = 0; intIndex < this.lstGroups.Items.Count; intIndex++)
                        {
                            if (this.lstGroups.Items[intIndex].Selected == true)
                            {
                                proxyCDUser.UserGroup objUserGroup = new ClaimsDocsClient.proxyCDUser.UserGroup();
                                objUserGroup.UserID = 0;
                                objUserGroup.GroupID = int.Parse(this.lstGroups.Items[intIndex].Value.ToString());
                                objUserGroup.IUDateTime = DateTime.Now;
                                listUserGroups.Add(objUserGroup);
                            }
                        }

                        objUser.listUserGroup = listUserGroups;

                        if (this.chkActive.Checked == true)
                        {
                            objUser.Active = "Y";
                        }
                        else
                        {
                            objUser.Active = "N";
                        }

                        objUser.IUDateTime = DateTime.Now;

                        //add user
                        intResult = objUserClient.UserUpdate(objUser, AppConfig.CorrespondenceDBConnectionString);
                        //check results
                        if (intResult == 0)
                        {
                        }
                        else
                        {
                            //save state and user id
                            this.lblState.Text = intState.ToString();
                            this.lblUserID.Text = intResult.ToString();
                        }
                        break;

                    default: //do nothing
                        break;

                }//end : switch (intState)

            }
            catch (Exception ex)
            {
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : cmdDo_Click() ";
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
                objUser = null;
                if (objUserClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objUserClient.Close();
                }
                objUserClient = null;
               
            }
        }//end : protected void Page_Load(object sender, EventArgs e)

        //define method : Show List Item(s)
        private bool ShowListItems()
        {
            //declare variables
            bool blnResult = true;
            proxyCDDepartment.CDDepartmentsClient objDepartmentClient = new proxyCDDepartment.CDDepartmentsClient();
            IEnumerable<proxyCDDepartment.Department> listDepartments;

            proxyCDGroup.CDGroupsClient objGroupClient = new proxyCDGroup.CDGroupsClient();
            IEnumerable<proxyCDGroup.Group> listGroups;

            string strSQL = "";

            try
            {
                //build sql string
                strSQL = "Select DepartmentID,DepartmentName,IUDateTime From tblDepartment Order By DepartmentName Asc";
                //get department list
                listDepartments = objDepartmentClient.DepartmentSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);
                //check department list
                if (listDepartments != null)
                {
                    this.cboDepartment.DataSource = listDepartments;
                    this.cboDepartment.DataBind();
                }


                //build sql string
                //strSQL = "Select tblDepartment.DepartmentName,tblGroup.* From dbo.tblGroup Inner Join tblDepartment On tblDepartment.DepartmentID=tblGroup.DepartmentID Order By GroupName";
                strSQL = "Select tblDepartment.DepartmentName + '-->' + tblGroup.GroupName As DepartmentName,tblGroup.* From dbo.tblGroup Inner Join tblDepartment On tblDepartment.DepartmentID=tblGroup.DepartmentID Order By DepartmentName";
                //get group list
                listGroups = objGroupClient.GroupSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);
                //check group list
                if (listGroups != null)
                {
                    this.lstGroups.DataSource = listGroups;
                    this.lstGroups.DataBind();
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
                objClaimsLog.MessageIs = "Method : ShowListItems() ";
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
            }

            //return
            return (blnResult);

        }

        //define method : Show User
        private bool UserShow(proxyCDUser.User objUser)
        {
            //declare variables
            bool blnResult = true;
            proxyCDUser.CDUsersClient objCDUserClient = new proxyCDUser.CDUsersClient();
            List<proxyCDUser.UserGroup> listUserGroups = new List<ClaimsDocsClient.proxyCDUser.UserGroup>();
            proxyCDUser.User objUserIs;
            int intListIndex = 0;
            int intIndex = 0;

            try
            {
                //get user
                objUserIs = objCDUserClient.UserRead(objUser, AppConfig.CorrespondenceDBConnectionString);
                //get user groups
                listUserGroups = objCDUserClient.UserGroupSearch(objUser.UserID, AppConfig.CorrespondenceDBConnectionString);

                //check results
                if (objUserIs != null)
                {
                    //show user
                    this.lblUserID.Text = objUserIs.UserID.ToString();
                    this.txtUserName.Text = objUserIs.UserName.ToString();

                    //select department in drop-down
                    for (intIndex = 0; intIndex < this.cboDepartment.Items.Count; intIndex++)
                    {
                        if (this.cboDepartment.Items[intIndex].Value.ToString().Equals(objUserIs.DepartmentID.ToString()))
                        {
                            this.cboDepartment.SelectedIndex = intIndex;
                        }
                    }

                    if (objUserIs.Administrator.Equals("N"))
                    {
                        this.cboAdministrator.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cboAdministrator.SelectedIndex = 1;
                    }

                    if (objUserIs.Approver.Equals("N"))
                    {
                        this.cboApprover.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cboApprover.SelectedIndex = 1;
                    }

                    if (objUserIs.Designer.Equals("N"))
                    {
                        this.cboDesigner.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cboDesigner.SelectedIndex = 1;
                    }

                    this.txtLoginPassword.Text = objUserIs.UserPassword;
                    this.txtConfirmPassword.Text = objUserIs.UserPassword;

                    if (objUserIs.Reviewer.Equals("N"))
                    {
                        this.cboReviewRequired.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cboReviewRequired.SelectedIndex = 1;
                    }

                    this.txtPhoneExtension.Text = objUserIs.Phone;
                    this.txtEMailAddress.Text = objUserIs.EMailAddress;
                    this.txtTitle.Text = objUserIs.Title;
                    this.txtSignatureName.Text = objUserIs.SignatureName;
                    this.txtSignatureFilePath.Text = objUserIs.Signature;

                    for (intListIndex = 0; intListIndex < listUserGroups.Count; intListIndex++)
                    {
                        for (intIndex = 0; intIndex < this.lstGroups.Items.Count; intIndex++)
                        {
                            if (this.lstGroups.Items[intIndex].Value.ToString().Equals(listUserGroups[intListIndex].GroupID.ToString()))
                            {
                                this.lstGroups.Items[intIndex].Selected = true;
                            }
                        }
                    }

                    if (objUserIs.Active.Equals("Y"))
                    {
                        this.chkActive.Checked = true;
                    }

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
                objClaimsLog.MessageIs = "Method : UserShow() ";
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
                objCDUserClient = null;
                objUserIs = null;
            }

            //return
            return (blnResult);

        }//end : private bool UserShow(proxyCDDepartment.Department objDepartment)

    }//end : public partial class UserAdmin : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.secure
