using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using ClaimsDocsClient;
using ClaimsDocsClient.AppClasses;
using System.Text;

namespace ClaimsDocsClient
{
    public partial class ClaimsDocsLogViewer : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables
            
            try
            {
                if (this.IsPostBack == false)
                {
                    //refresh claims log list
                    GridViewBind();
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

        //define : GridViewBind
        private bool GridViewBind()
        {
            //declare variables
            bool blnResult = true;
            DataView datView = null;

            try
            {
                //get batch document list
                datView = ClaimsLogGetList();

                //check for records and show list
                this.gvwData.DataSource = datView;
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
                objClaimsLog.MessageIs = "Method : GridViewBind() ";
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

            //return results
            return (blnResult);

        }//end : GridViewBind

        //define method : ClaimsLogGetList
        public DataView ClaimsLogGetList()
        {
            //declare variables
            SqlDataAdapter sqlAdapter = null;
            DataSet datSet = null;
            DataView datView = null;
            StringBuilder sbrSQL = new StringBuilder();

            //start try
            try
            {
                //build sql string 
                //build sql string
                sbrSQL.Append("Select   ");
                sbrSQL.Append("     tblClaimsDocsLog.ClaimsDocsLogID,");
                sbrSQL.Append("     tblClaimsDocsLog.LogSourceTypeID,");
                sbrSQL.Append("     tblClaimsDocsLog.LogTypeID,");
                sbrSQL.Append("     tblLogSourceType.LogSourceName,");
                sbrSQL.Append("     tblLogType.LogTypeName,");
                sbrSQL.Append("     MessageIs,");
                sbrSQL.Append("     ExceptionIs,");
                sbrSQL.Append("     StackTraceIs,");
                sbrSQL.Append("     tblClaimsDocsLog.IUDateTime ");
                sbrSQL.Append("From ");
                sbrSQL.Append("     tblClaimsDocsLog    ");
                sbrSQL.Append("Inner Join   ");
                sbrSQL.Append("     tblLogSourceType On tblLogSourceType.LogSourceTypeID=tblClaimsDocsLog.LogSourceTypeID   ");
                sbrSQL.Append("Inner Join   ");
                sbrSQL.Append("     tblLogType On tblLogType.LogTypeID=tblClaimsDocsLog.LogTypeID   ");
                sbrSQL.Append("Order By ");
                sbrSQL.Append("     tblClaimsDocsLog.IUDateTime Desc");

                //setup connection
                sqlAdapter = new SqlDataAdapter(sbrSQL.ToString(), AppConfig.CorrespondenceDBConnectionString);

                datSet = new DataSet();
                datView = new DataView();

                sqlAdapter.Fill(datSet, "Table");
                datView = datSet.Tables["Table"].DefaultView;
            }//end try
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                AppSupport objSupport = new AppSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 2;
                objClaimsLog.MessageIs = "Method : ClaimsLogGetList() ";
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
            return (datView);
            //cleanup command
        }//end method : ClaimsLogGetList

        //define : gvList_PageIndexChanging
        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //declare variables

            try
            {
                //set current page index
                gvwData.PageIndex = e.NewPageIndex;
                //bind data to grid
                GridViewBind();
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


    }//end : public partial class ClaimsDocsLogViewer : System.Web.UI.Page
}//end : namespace ClaimsDocsClient
