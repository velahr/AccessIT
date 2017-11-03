using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient;
using ClaimsDocsClient.AppClasses;
using System.IO;

namespace ClaimsDocsClient.secure
{
    public partial class UserList : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables

            try
            {
                //check for postback
                if (this.IsPostBack == false)
                {
                    //show user list
                    UserListRefresh();
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

        //define method : UserListRefresh
        public bool UserListRefresh()
        {
            //declare variables
            bool blnResult = true;
            proxyCDUser.CDUsersClient objUserClient = new ClaimsDocsClient.proxyCDUser.CDUsersClient();
            IEnumerable<proxyCDUser.User> listUser;
            string strSQL = "";

            try
            {
                //build sql string
                strSQL = strSQL + " Select ";
                strSQL = strSQL + "     tblUser.UserID,   ";
                strSQL = strSQL + "     tblUser.UserName,   ";
                strSQL = strSQL + "     tblUser.UserPassword,   ";
                strSQL = strSQL + "     tblUser.DepartmentID,   ";
                strSQL = strSQL + "     tblUser.Administrator,   ";
                strSQL = strSQL + "     tblUser.Title,   ";
                strSQL = strSQL + "     tblUser.Phone,   ";
                strSQL = strSQL + "     tblUser.Signature,   ";
                strSQL = strSQL + "     tblUser.SignatureName,   ";
                strSQL = strSQL + "     tblUser.EMailAddress,   ";
                strSQL = strSQL + "     tblUser.IUDateTime,   ";
                strSQL = strSQL + "     Case Approver When 'N' Then 'No' Else 'Yes' End As Approver,";
                strSQL = strSQL + "     Case Designer When 'N' Then 'No' Else 'Yes' End As Designer,";
                strSQL = strSQL + "     Case Reviewer When 'N' Then 'No' Else 'Yes' End As Reviewer,";
                strSQL = strSQL + "     Case Active When 'N' Then 'No' Else 'Yes' End As Active,";
                strSQL = strSQL + "     tblDepartment.DepartmentName    ";
                strSQL = strSQL + " From ";
                strSQL = strSQL + "    dbo.tblUser";
                strSQL = strSQL + " Inner Join ";
                strSQL = strSQL + "     dbo.tblDepartment";
                strSQL = strSQL + " On";
                strSQL = strSQL + "     dbo.tblDepartment.DepartmentID=tblUser.DepartmentID ";
                strSQL = strSQL + " Order By tblUser.UserName ";

                //get User list
                listUser = objUserClient.UserSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);

                //assign User list to grid data source
                this.gvwData.DataSource = listUser;
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
                objClaimsLog.MessageIs = "Method : UserListRefresh() ";
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
            }

            //return result
            return (blnResult);
        }//end : UserListRefresh

        //define method : UserSortedListRefresh
        public bool UserSortedListRefresh(string strOrderByClause)
        {
            //declare variables
            bool blnResult = true;
            proxyCDUser.CDUsersClient objUserClient = new ClaimsDocsClient.proxyCDUser.CDUsersClient();
            IEnumerable<proxyCDUser.User> listUser;
            string strSQL = "";

            try
            {
                //build sql string
                strSQL = strSQL + " Select ";
                strSQL = strSQL + "     tblUser.UserID,   ";
                strSQL = strSQL + "     tblUser.UserName,   ";
                strSQL = strSQL + "     tblUser.UserPassword,   ";
                strSQL = strSQL + "     tblUser.DepartmentID,   ";
                strSQL = strSQL + "     tblUser.Administrator,   ";
                strSQL = strSQL + "     tblUser.Title,   ";
                strSQL = strSQL + "     tblUser.Phone,   ";
                strSQL = strSQL + "     tblUser.Signature,   ";
                strSQL = strSQL + "     tblUser.SignatureName,   ";
                strSQL = strSQL + "     tblUser.EMailAddress,   ";
                strSQL = strSQL + "     tblUser.IUDateTime,   ";
                strSQL = strSQL + "     Case Approver When 'N' Then 'No' Else 'Yes' End As Approver,";
                strSQL = strSQL + "     Case Designer When 'N' Then 'No' Else 'Yes' End As Designer,";
                strSQL = strSQL + "     Case Reviewer When 'N' Then 'No' Else 'Yes' End As Reviewer,";
                strSQL = strSQL + "     Case Active When 'N' Then 'No' Else 'Yes' End As Active,";
                strSQL = strSQL + "     tblDepartment.DepartmentName    ";
                strSQL = strSQL + " From ";
                strSQL = strSQL + "    dbo.tblUser";
                strSQL = strSQL + " Inner Join ";
                strSQL = strSQL + "     dbo.tblDepartment";
                strSQL = strSQL + " On";
                strSQL = strSQL + "     dbo.tblDepartment.DepartmentID=tblUser.DepartmentID ";
                strSQL = strSQL + " Order By " + strOrderByClause;

                //get User list
                listUser = objUserClient.UserSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);

                //assign User list to grid data source
                this.gvwData.DataSource = listUser;
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
                objClaimsLog.MessageIs = "Method : UserSortedListRefresh() ";
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
            }

            //return result
            return (blnResult);
        }//end : UserSortedListRefresh

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

            try
            {
                //set current page index
                gvwData.PageIndex = e.NewPageIndex;
                //bind data to grid
                UserListRefresh();
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

        
        #region User Methods

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

        //define method : lnkSortByUserName_Click
        protected void lnkSortByUserName_Click(object sender, EventArgs e)
        {
            //UserSortedListRefresh

            //declare variables
            string strSortOrder = "";

            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;
                
                //toggle sort order
                if (strSortOrder.Equals("UserName Desc") == true)
                {
                    strSortOrder = "UserName Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("UserName Asc") == true)
                {
                    strSortOrder = "UserName Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "UserName Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }

                //update list sort order
                UserSortedListRefresh(strSortOrder);
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
                objClaimsLog.MessageIs = "Method : lnkSortByUserName_Click() ";
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
        }//end : lnkSortByUserName_Click

        //define method : lnkSortByDepartment_Click
        protected void lnkSortByDepartment_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";

            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;

                //toggle sort order
                if (strSortOrder.Equals("DepartmentName Desc") == true)
                {
                    strSortOrder = "DepartmentName Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("UserName Asc") == true)
                {
                    strSortOrder = "DepartmentName Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "DepartmentName Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }

                //update list sort order
                UserSortedListRefresh(strSortOrder);
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
                objClaimsLog.MessageIs = "Method : lnkSortByDepartment_Click() ";
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
        }//end : lnkSortByDepartment_Click

        //define method : lnklnkSortByApprover_Click
        protected void lnklnkSortByApprover_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";

            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;

                //toggle sort order
                if (strSortOrder.Equals("Approver Desc") == true)
                {
                    strSortOrder = "Approver Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("Approver Asc") == true)
                {
                    strSortOrder = "Approver Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "Approver Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }

                //update list sort order
                UserSortedListRefresh(strSortOrder);
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
                objClaimsLog.MessageIs = "Method : lnklnkSortByApprover_Click() ";
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
        }//end : lnklnkSortByApprover_Click

        //define method : lnkSortByDesigner_Click
        protected void lnkSortByDesigner_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";

            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;

                //toggle sort order
                if (strSortOrder.Equals("Designer Desc") == true)
                {
                    strSortOrder = "Designer Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("Designer Asc") == true)
                {
                    strSortOrder = "Designer Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "Designer Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }

                //update list sort order
                UserSortedListRefresh(strSortOrder);
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
                objClaimsLog.MessageIs = "Method : lnkSortByDesigner_Click() ";
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
        }//end : lnkSortByDesigner_Click

        //define method : lnkSortByReviewer_Click
        protected void lnkSortByReviewer_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";

            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;

                //toggle sort order
                if (strSortOrder.Equals("Reviewer Desc") == true)
                {
                    strSortOrder = "Reviewer Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("Reviewer Asc") == true)
                {
                    strSortOrder = "Reviewer Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "Reviewer Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }

                //update list sort order
                UserSortedListRefresh(strSortOrder);
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
                objClaimsLog.MessageIs = "Method : lnkSortByReviewer_Click() ";
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
        }//end : lnkSortByReviewer_Click

        //define method : lnkSortByActive_Click
        protected void lnkSortByActive_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";

            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;

                //toggle sort order
                if (strSortOrder.Equals("Active Desc") == true)
                {
                    strSortOrder = "Active Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("Active Asc") == true)
                {
                    strSortOrder = "Active Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "Active Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }

                //update list sort order
                UserSortedListRefresh(strSortOrder);
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
                objClaimsLog.MessageIs = "Method : lnkSortByActive_Click() ";
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
        }//end : lnkSortByActive_Click


        #endregion

    }//end :public partial class UserList : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.secure
