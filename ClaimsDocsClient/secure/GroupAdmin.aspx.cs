using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient.AppClasses;

namespace ClaimsDocsClient.secure
{
    public partial class GroupAdmin : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables
            int intState = -1;
            int intGroupID = 0;

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
                        //get group id
                        if (int.TryParse(Request.Params["groupid"].ToString(), out intGroupID) == false)
                        {
                            intGroupID = 0;
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
                        case 1: //Add group
                            this.lblState.Text = intState.ToString();
                            this.lblGroupID.Text = "0";
                            this.lblHeader.Text = "Add Group";
                            break;

                        case 2: //Edit group
                            this.lblState.Text = intState.ToString();
                            this.lblGroupID.Text = intGroupID.ToString();
                            this.lblHeader.Text = "Edit Group";
                            proxyCDGroup.Group objGroup = new ClaimsDocsClient.proxyCDGroup.Group();

                            //show group
                            objGroup.GroupID = intGroupID;
                            GroupShow(objGroup);
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
        }//end : Page_Load

        //define method : cmdDo_Click
        protected void cmdDo_Click(object sender, EventArgs e)
        {
            //declare variables
            int intResult = 0;
            int intState = -1;
            int intGroupID = 0;
            int intDepartmentID = 0;
            string strGroupName = "";
            proxyCDGroup.Group objGroup = new ClaimsDocsClient.proxyCDGroup.Group();
            proxyCDGroup.CDGroupsClient  objGroupClient = new proxyCDGroup.CDGroupsClient();

            try
            {
                //gather values
                intState = int.Parse(this.lblState.Text.ToString());
                intGroupID = int.Parse(this.lblGroupID.Text.ToString());
                intDepartmentID = int.Parse(this.cboDepartment.SelectedValue.ToString());

                //get group name
                strGroupName = this.txtGroupName.Text;

                //process request based on state
                switch (intState)
                {
                    case 1: //add group
                        //fill group
                        objGroup.GroupID = intGroupID;
                        objGroup.DepartmentID = intDepartmentID;
                        objGroup.GroupName = strGroupName;
                        objGroup.IUDateTime = DateTime.Now;

                        //add group
                        intResult = objGroupClient.GroupCreate(objGroup, AppConfig.CorrespondenceDBConnectionString);
                        //check results
                        if (intResult == 0)
                        {
                        }
                        else
                        {
                        }
                        break;

                    case 2: //edit group
                        //fill group
                        objGroup.GroupID = intGroupID;
                        objGroup.DepartmentID = intDepartmentID;
                        objGroup.GroupName = strGroupName;
                        objGroup.IUDateTime = DateTime.Now;

                        //update group
                        intResult = objGroupClient.GroupUpdate(objGroup, AppConfig.CorrespondenceDBConnectionString);
                        //check results
                        if (intResult == 0)
                        {
                        }
                        else
                        {
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
                if (objGroupClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objGroupClient.Close();
                }
                objGroupClient = null;

                objGroupClient = null;
                objGroup = null;

            }
        }//end : protected void cmdDo_Click(object sender, EventArgs e)

        //define method : Show List Item(s)
        private bool ShowListItems()
        {
            //declare variables
            bool blnResult = true;
            proxyCDDepartment.CDDepartmentsClient objDepartmentClient = new proxyCDDepartment.CDDepartmentsClient();
            IEnumerable<proxyCDDepartment.Department> listDepartments;
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
                if (objDepartmentClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDepartmentClient.Close();
                }
                objDepartmentClient = null;
            }

            //return
            return (blnResult);

        }

        //define method : Show Group
        private bool GroupShow(proxyCDGroup.Group objGroup)
        {
            //declare variables
            bool blnResult = true;
            proxyCDGroup.CDGroupsClient objCDGroupClient = new proxyCDGroup.CDGroupsClient();
            proxyCDGroup.Group objGroupIs;
            int intIndex =0;
            try
            {
                //get group
                objGroupIs = objCDGroupClient.GroupRead(objGroup, AppConfig.CorrespondenceDBConnectionString);
                //check results
                if (objGroupIs != null)
                {
                    //show group
                    this.lblGroupID.Text = objGroupIs.GroupID.ToString();
                    this.txtGroupName.Text = objGroupIs.GroupName.ToString();

                    //select department in drop-down
                    for (intIndex = 0; intIndex < this.cboDepartment.Items.Count - 1; intIndex++)
                    {
                        if (this.cboDepartment.Items[intIndex].Value.ToString().Equals(objGroupIs.DepartmentID.ToString()))
                        {
                            this.cboDepartment.SelectedIndex = intIndex;
                        }
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
                objClaimsLog.MessageIs = "Method : GroupShow() ";
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
                if (objCDGroupClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objCDGroupClient.Close();
                }
                objCDGroupClient = null;
                objGroupIs = null;
            }

            //return
            return (blnResult);

        }//end : private bool GroupShow(proxyCDDepartment.Department objDepartment)


    }//end : public partial class GroupAdmin : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.secure
