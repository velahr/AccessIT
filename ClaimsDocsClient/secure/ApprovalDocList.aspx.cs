using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient;
using ClaimsDocsClient.AppClasses;
using System.IO;
using System.Text;
using System.Data;

namespace ClaimsDocsClient.secure
{
    public partial class ApprovalDocList : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables
            proxyCDUser.User objUserIs = null;

            try
            {
                //check for postback
                if (this.IsPostBack == false)
                {
                    // get the user information out of Session
                    if (!Session["CurrentUser"].Equals(null))
                    {
                        objUserIs = (proxyCDUser.User)Session["CurrentUser"];
                    }

                    //check results
                    if (objUserIs != null)
                    {
                        //show request header information
                        this.lblUser.Text = objUserIs.UserName;
                        this.lblDepartment.Text = objUserIs.DepartmentName;

                        //refresh document list
                        DocumentListRefresh();
                    }
                    else
                    {
                        //show error message
                        this.lblMessage.Text = "Unable to retrieve User information from session.";
                    }
                }//end : if (this.IsPostBack == false)
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

        //define method : DocumentListRefresh
        public bool DocumentListRefresh()
        {
            //declare variables
            bool blnResult = true;
            proxyCDDocument2.CDDocumentClient objDocumentClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            List<proxyCDDocument2.DocumentsNeedingApproval> listDocument;

            try
            {
                //get document list
                listDocument = objDocumentClient.DocumentsNeedingApprovalRead(AppConfig.CorrespondenceDBConnectionString);

                //assign document list to grid data source
                this.gvwData.DataSource = listDocument;
                this.gvwData.DataBind();
                //show records found
                this.lblRecordCount.Text = listDocument.Count.ToString() + " record(s) found";
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
                objClaimsLog.MessageIs = "Method : DocumentListRefresh() ";
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
                if (objDocumentClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocumentClient.Close();
                }
                objDocumentClient = null;
            }

            //return result
            return (blnResult);
        }//end : DocumentListRefresh

        //define method : DocumentSortedListRefresh
        public bool DocumentSortedListRefresh(string strOrderByClause)
        {
            //declare variables
            bool blnResult = true;
            proxyCDDocument2.CDDocumentClient objDocumentClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            List<proxyCDDocument2.Document> listDocument;
            string strSQL = "";

            try
            {
                //build sql string
                strSQL = strSQL + "SELECT tblDocument.DocumentCode, tblDocument.Description, ";
                strSQL = strSQL + "tblUser.UserName, tblDocumentApprovalQueue.DateSubmitted, ";
                strSQL = strSQL + "tblDocumentApprovalQueue.SubmitterID, tblDocumentApprovalQueue.ApprovalQueueID, ";
                strSQL = strSQL + "tblDocumentApprovalQueue.DocumentID, tblDocumentApprovalQueue.InstanceID, ";
                strSQL = strSQL + "tblDocumentApprovalQueue.Content, tblDocumentApprovalQueue.IUDateTime  ";
                strSQL = strSQL + "FROM tblDocumentApprovalQueue INNER JOIN  ";
                strSQL = strSQL + "tblDocument ON tblDocumentApprovalQueue.DocumentID = tblDocument.DocumentID INNER JOIN ";
                strSQL = strSQL + "tblUser ON tblDocumentApprovalQueue.SubmitterID = tblUser.UserID ";
                strSQL = strSQL + " Order By " + strOrderByClause;

                //get document list
                listDocument = objDocumentClient.DocumentAdjusterListSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);

                //assign document list to grid data source
                this.gvwData.DataSource = listDocument;
                this.gvwData.DataBind();
                //show records found
                this.lblRecordCount.Text = listDocument.Count.ToString() + " record(s) found";
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
                objClaimsLog.MessageIs = "Method : DocumentSortedListRefresh() ";
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
                if (objDocumentClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocumentClient.Close();
                }
                objDocumentClient = null;
            }

            //return result
            return (blnResult);
        }//end : DocumentSortedListRefresh

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
            int intDocumentGroupID = 0;

            try
            {
                //set current page index
                gvwData.PageIndex = e.NewPageIndex;
                //bind data to grid
                DocumentListRefresh();
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

        //define method : lnkSortByDocumentCode_Click
        protected void lnkSortByDocumentCode_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";

            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;

                //toggle sort order
                if (strSortOrder.Equals("DocumentCode Desc") == true)
                {
                    strSortOrder = "DocumentCode Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("DocumentCode Asc") == true)
                {
                    strSortOrder = "DocumentCode Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "DocumentCode Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }

                //update list sort order
                DocumentSortedListRefresh(strSortOrder);
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
                objClaimsLog.MessageIs = "Method : lnkSortByDocumentCode_Click() ";
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
        }//end : lnkSortByDocumentCode_Click

        //define method : lnkSortByDescription_Click
        protected void lnkSortByDescription_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";

            try
            {
                gvwData.Sort("Description", SortDirection.Ascending);
                ////get existing sort order
                //strSortOrder = this.lblSortOrder.Text;

                ////toggle sort order
                //if (strSortOrder.Equals("Description Desc") == true)
                //{
                //    strSortOrder = "Description Asc";
                //    this.lblSortOrder.Text = strSortOrder;
                //}
                //else if (strSortOrder.Equals("Description Asc") == true)
                //{
                //    strSortOrder = "Description Desc";
                //    this.lblSortOrder.Text = strSortOrder;
                //}
                //else
                //{
                //    strSortOrder = "Description Asc";
                //    this.lblSortOrder.Text = strSortOrder;
                //}

                ////update list sort order
                //DocumentSortedListRefresh(strSortOrder);
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
                objClaimsLog.MessageIs = "Method : lnkSortByDescription_Click() ";
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

        // Checks the date to make sure its less than a day old. If it is longer, color the rows text red
        protected void gvwData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Grab the document date and calculate today - 1
                DateTime docDate = (DateTime)DataBinder.Eval(e.Row.DataItem, "DateSubmitted");
                DateTime oneDay = DateTime.Now;
                oneDay = oneDay.AddDays(-1);

                // Color the row text red if the docdate is more than one day old
                if (docDate <= oneDay)
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        //private string ConvertSortDirectionToSql(SortDirection sortDirection)
        //{
        //    string newSortDirection = String.Empty;

        //    switch (sortDirection)
        //    {
        //        case SortDirection.Ascending:
        //            newSortDirection = "ASC";
        //            break;

        //        case SortDirection.Descending:
        //            newSortDirection = "DESC";
        //            break;
        //    }

        //    return newSortDirection;
        //}

        protected void gvwData_Sorting(object sender, GridViewSortEventArgs e)
        {
            proxyCDDocument2.CDDocumentClient objDocumentClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            List<proxyCDDocument2.DocumentsNeedingApproval> listDocument;

            //get document list
            listDocument = objDocumentClient.DocumentsNeedingApprovalRead(AppConfig.CorrespondenceDBConnectionString);

            if (listDocument != null)
            {
                switch(e.SortExpression)
                {
                    // Sort the List based on the field passed
                    case "UserName":
                        listDocument = listDocument.OrderBy(row => row.UserName).ThenBy(row => row.DateSubmitted).ToList();
                        break;
                    case "DocumentCode":
                        listDocument = listDocument.OrderBy(row => row.DocumentCode).ToList();
                        break;
                    case "Description":
                        listDocument = listDocument.OrderBy(row => row.Description).ToList();
                        break;
                    case "DateSubmitted":
                        listDocument = listDocument.OrderBy(row => row.DateSubmitted).ToList();
                        break;
                }
                //listDocument.Sort((row1, row2) => row1.UserName.CompareTo(row2.UserName));
                gvwData.DataSource = listDocument;
                gvwData.DataBind();
            }
        }//end : lnkSortByDescription_Click
    }
}
