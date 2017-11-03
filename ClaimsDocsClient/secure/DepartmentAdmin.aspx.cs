using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient.AppClasses;

namespace ClaimsDocsClient.secure
{
    public partial class DepartmentAdmin : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables
            int intState = -1;
            int intDepartmentID = 0;

            try
            {
                //check for postback
                if (this.Page.IsPostBack == false)
                {
                    //get state
                    if (int.TryParse(Request.Params["state"].ToString(), out intState) == true)
                    {
                        //get department id
                        if (int.TryParse(Request.Params["departmentid"].ToString(), out intDepartmentID) == false)
                        {
                            intDepartmentID = 0;
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
                        case 1: //Add department
                            this.lblState.Text = intState.ToString();
                            this.lblDepartmentID.Text = "0";
                            this.lblHeader.Text = "Add Department";
                            break;

                        case 2: //Edit Department
                            this.lblState.Text = intState.ToString();
                            this.lblDepartmentID.Text = intDepartmentID.ToString();
                            this.lblHeader.Text = "Edit Department";
                            proxyCDDepartment.Department objDepartment = new ClaimsDocsClient.proxyCDDepartment.Department();

                            //show department
                            objDepartment.DepartmentID = intDepartmentID;
                            DepartmentShow(objDepartment);


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
            int intDepartmentID = 0;
            string strDepartmentName = "";
            proxyCDDepartment.Department objDepartment = new ClaimsDocsClient.proxyCDDepartment.Department();
            proxyCDDepartment.CDDepartmentsClient objDepartmentClient = new ClaimsDocsClient.proxyCDDepartment.CDDepartmentsClient();

            try
            {
                //gather values
                intState = int.Parse(this.lblState.Text.ToString());
                intDepartmentID = int.Parse(this.lblDepartmentID.Text.ToString());

                //get department name
                strDepartmentName = this.txtDepartmentName.Text;

                //process request based on state
                switch (intState)
                {
                    case 1: //add department
                        //fill department
                        objDepartment.DepartmentID = 0;
                        objDepartment.DepartmentName = strDepartmentName;
                        objDepartment.IUDateTime = DateTime.Now;

                        //add department
                        intResult = objDepartmentClient.DepartmentCreate(objDepartment,AppConfig.CorrespondenceDBConnectionString );
                        //check results
                        if (intResult == 0)
                        {
                        }
                        else
                        {
                        }
                        break;

                    case 2: //edit department
                        //fill department
                        objDepartment.DepartmentID = intDepartmentID;
                        objDepartment.DepartmentName = strDepartmentName;
                        objDepartment.IUDateTime = DateTime.Now;

                        //add department
                        intResult = objDepartmentClient.DepartmentUpdate(objDepartment, AppConfig.CorrespondenceDBConnectionString);
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
                objDepartment = null;
                if (objDepartmentClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDepartmentClient.Close();
                }
                objDepartmentClient = null; 
            }

        }//end : protected void cmdDo_Click(object sender, EventArgs e)

        //define method : Show Department
        private bool DepartmentShow(proxyCDDepartment.Department objDepartment)
        {
            //declare variables
            bool blnResult = true;
            proxyCDDepartment.CDDepartmentsClient objCDDepartmentsClient = new ClaimsDocsClient.proxyCDDepartment.CDDepartmentsClient();
            proxyCDDepartment.Department objDepartmentIs;

            try
            {
                //get department
                objDepartmentIs = objCDDepartmentsClient.DepartmentRead(objDepartment, AppConfig.CorrespondenceDBConnectionString);
                //check results
                if (objDepartmentIs != null)
                {
                    //show department
                    this.lblDepartmentID.Text = objDepartmentIs.DepartmentID.ToString();
                    this.txtDepartmentName.Text = objDepartmentIs.DepartmentName.ToString();
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
                objClaimsLog.MessageIs = "Method : DepartmentShow() ";
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
                if (objCDDepartmentsClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objCDDepartmentsClient.Close();
                }
                objCDDepartmentsClient = null;
                objDepartmentIs = null;
            }

            //return
            return (blnResult);

        }//end : private bool DepartmentShow(proxyCDDepartment.Department objDepartment)

    }//end : public partial class DepartmentAdmin : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.secure
