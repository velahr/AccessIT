using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient;
using ClaimsDocsClient.AppClasses;

namespace ClaimsDocsClient.secure
{
    public partial class ProgramList : System.Web.UI.Page
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
                    //show Program list
                    ProgramListRefresh("Select * From dbo.tblProgram Order By ProgramCode");
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
        }//end :Page_Load

        #region Grid Methods
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

        //define method : lnkSortByProgramName_Click
        protected void lnkSortByProgramName_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";

            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;
                //toggle sort order
                if (strSortOrder.Equals("ASC") == true)
                {
                    strSortOrder = "DESC";
                    this.lblSortOrder.Text = "DESC";
                }
                else
                {
                    strSortOrder = "ASC";
                    this.lblSortOrder.Text = "ASC";
                }

                //update list sort order
                ProgramListRefresh("Select * From dbo.tblProgram Order By ProgramCode " + strSortOrder);
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
                objClaimsLog.MessageIs = "Method : lnkSortByProgramName_Click() ";
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
        }//end : lnkSortByProgramName_Click

        #endregion

        //define method : ProgramListRefresh
        public bool ProgramListRefresh(string strSQL)
        {
            //declare variables
            bool blnResult = true;
            proxyCDProgram.CDProgramClient objProgramClient = new ClaimsDocsClient.proxyCDProgram.CDProgramClient();
            IEnumerable<proxyCDProgram.Program> listProgram;

            try
            {
                //get program list
                listProgram = objProgramClient.ProgramSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);

                //assign program list to grid data source
                this.grdData.DataSource = listProgram;
                this.grdData.DataBind();
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
                objClaimsLog.MessageIs = "Method : ProgramListRefresh() ";
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
                if (objProgramClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objProgramClient.Close();
                }
                objProgramClient = null;

            }

            //return result
            return (blnResult);
        }

        protected void grdData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }//end : ProgramListRefresh

    }//end : public partial class ProgramList : System.Web.UI.Page




}//end : namespace ClaimsDocsClient.secure
