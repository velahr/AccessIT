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
    public partial class DocDefDocList : System.Web.UI.Page
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
                    DocumentListRefresh();
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

        //define method : DocumentListRefresh
        public bool DocumentListRefresh()
        {
            //declare variables
            bool blnResult = true;
            proxyCDDocument2.CDDocumentClient objDocumentClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            List<proxyCDDocument2.Document> listDocument;
            string strSQL = "";

            try
            {
                //build sql string
                strSQL = strSQL + " Select ";
                strSQL = strSQL + "     tblDocument.DocumentID,   ";
                strSQL = strSQL + "     tblDocument.DocumentCode,   ";
                strSQL = strSQL + "     tblDocument.DepartmentID,   ";
                strSQL = strSQL + "     tblDocument.ProgramID,   ";
                strSQL = strSQL + "     tblProgram.ProgramCode,   ";
                strSQL = strSQL + "     tblDocument.Description,   ";
                strSQL = strSQL + "     tblDocument.TemplateName,   ";
                strSQL = strSQL + "     tblDocument.EffectiveDate,   ";
                strSQL = strSQL + "     tblDocument.ExpirationDate,   ";
                strSQL = strSQL + "     tblDocument.ImageRightDocumentID,   ";
                strSQL = strSQL + "     tblDocument.ImageRightDocumentSection,   ";
                strSQL = strSQL + "     tblDocument.ImageRightDrawer,   ";
                strSQL = strSQL + "     tblDocument.ContactNo, tblDocument.ContactType,   ";
                strSQL = strSQL + "     tblDocument.DiaryNumberOfDays,   ";
                strSQL = strSQL + "     tblDocument.DesignerID,   ";
                strSQL = strSQL + "     tblDocument.StyleSheetName,   ";                
                strSQL = strSQL + "     Case Review When 'N' Then 'No' Else 'Yes' End As Review,";
                strSQL = strSQL + "     Case ProofOfMailing When 'N' Then 'No' Else 'Yes' End As ProofOfMailing,";
                strSQL = strSQL + "     Case DataMatx When 'N' Then 'No' Else 'Yes' End As DataMatx,";
                strSQL = strSQL + "     Case ImportToImageRight When 'N' Then 'No' Else 'Yes' End As ImportToImageRight,";
                strSQL = strSQL + "     Case CopyAgent When 'N' Then 'No' Else 'Yes' End As CopyAgent,";
                strSQL = strSQL + "     Case CopyInsured When 'N' Then 'No' Else 'Yes' End As CopyInsured,";
                strSQL = strSQL + "     Case CopyLienHolder When 'N' Then 'No' Else 'Yes' End As CopyLienHolder,";
                strSQL = strSQL + "     Case CopyFinanceCo When 'N' Then 'No' Else 'Yes' End As CopyFinanceCo,";
                strSQL = strSQL + "     Case CopyAttorney When 'N' Then 'No' Else 'Yes' End As CopyAttorney,";
                strSQL = strSQL + "     Case DiaryAutoUpdate When 'N' Then 'No' Else 'Yes' End As DiaryAutoUpdate,";
                strSQL = strSQL + "     Case Active When 'N' Then 'No' Else 'Yes' End As Active,";
                strSQL = strSQL + "     tblDocument.LastModified AS LastModified,";
                strSQL = strSQL + "     tblDocument.AttachedDocument ,";
                strSQL = strSQL + "     tblDocument.IUDateTime ";
                strSQL = strSQL + " From ";
                strSQL = strSQL + "    dbo.tblDocument";
                strSQL = strSQL + " Inner Join ";
                strSQL = strSQL + "     dbo.tblProgram On tblProgram.ProgramID=dbo.tblDocument.ProgramID ";
                strSQL = strSQL + " Order By tblDocument.DocumentCode ";

                //get document list
                listDocument = objDocumentClient.DocumentSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);

                //assign document list to grid data source
                this.gvwData.DataSource = listDocument;
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
                if(objDocumentClient.State == System.ServiceModel.CommunicationState.Opened)
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
                strSQL = strSQL + " Select ";
                strSQL = strSQL + "     tblDocument.DocumentID,   ";
                strSQL = strSQL + "     tblDocument.DocumentCode,   ";
                strSQL = strSQL + "     tblDocument.DepartmentID,   ";
                strSQL = strSQL + "     tblDocument.ProgramID,   ";
                strSQL = strSQL + "     tblProgram.ProgramCode,   ";
                strSQL = strSQL + "     tblDocument.Description,   ";
                strSQL = strSQL + "     tblDocument.TemplateName,   ";
                strSQL = strSQL + "     tblDocument.EffectiveDate,   ";
                strSQL = strSQL + "     tblDocument.ExpirationDate,   ";
                strSQL = strSQL + "     tblDocument.ImageRightDocumentID,   ";
                strSQL = strSQL + "     tblDocument.ImageRightDocumentSection,   ";
                strSQL = strSQL + "     tblDocument.ImageRightDrawer,   ";
                strSQL = strSQL + "     tblDocument.ContactNo, tblDocument.ContactType,   ";
                strSQL = strSQL + "     tblDocument.DiaryNumberOfDays,   ";
                strSQL = strSQL + "     tblDocument.DesignerID,   ";
                strSQL = strSQL + "     tblDocument.StyleSheetName,   ";                
                strSQL = strSQL + "     Case Review When 'N' Then 'No' Else 'Yes' End As Review,";
                strSQL = strSQL + "     Case ProofOfMailing When 'N' Then 'No' Else 'Yes' End As ProofOfMailing,";
                strSQL = strSQL + "     Case DataMatx When 'N' Then 'No' Else 'Yes' End As DataMatx,";
                strSQL = strSQL + "     Case ImportToImageRight When 'N' Then 'No' Else 'Yes' End As ImportToImageRight,";
                strSQL = strSQL + "     Case CopyAgent When 'N' Then 'No' Else 'Yes' End As CopyAgent,";
                strSQL = strSQL + "     Case CopyInsured When 'N' Then 'No' Else 'Yes' End As CopyInsured,";
                strSQL = strSQL + "     Case CopyLienHolder When 'N' Then 'No' Else 'Yes' End As CopyLienHolder,";
                strSQL = strSQL + "     Case CopyFinanceCo When 'N' Then 'No' Else 'Yes' End As CopyFinanceCo,";
                strSQL = strSQL + "     Case CopyAttorney When 'N' Then 'No' Else 'Yes' End As CopyAttorney,";
                strSQL = strSQL + "     Case DiaryAutoUpdate When 'N' Then 'No' Else 'Yes' End As DiaryAutoUpdate,";
                strSQL = strSQL + "     Case Active When 'N' Then 'No' Else 'Yes' End As Active,";
                strSQL = strSQL + "     tblDocument.LastModified AS LastModified,";
                strSQL = strSQL + "     tblDocument.AttachedDocument ,";
                strSQL = strSQL + "     tblDocument.IUDateTime ";
                strSQL = strSQL + " From ";
                strSQL = strSQL + "    dbo.tblDocument";
                strSQL = strSQL + " Inner Join ";
                strSQL = strSQL + "     dbo.tblProgram On tblProgram.ProgramID=dbo.tblDocument.ProgramID ";
                strSQL = strSQL + " Order By " + strOrderByClause;

                //get document list
                listDocument = objDocumentClient.DocumentSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);

                //assign document list to grid data source
                this.gvwData.DataSource = listDocument;
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
                //IF YOU ADD A SORT COLUMN YOU MUST EDIT THE 
                //STORED PROC
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
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;

                //toggle sort order
                if (strSortOrder.Equals("Description Desc") == true)
                {
                    strSortOrder = "Description Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("Description Asc") == true)
                {
                    strSortOrder = "Description Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "Description Asc";
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
        }//end : lnkSortByDescription_Click

        //define method : lnkSortByProgramCode_Click
        protected void lnkSortByProgramCode_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";

            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;

                //toggle sort order
                if (strSortOrder.Equals("ProgramCode Desc") == true)
                {
                    strSortOrder = "ProgramCode Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("ProgramCode Asc") == true)
                {
                    strSortOrder = "ProgramCode Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "ProgramCode Asc";
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
                objClaimsLog.MessageIs = "Method : lnkSortByProgramCode_Click() ";
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
        }//end : lnkSortByProgramCode_Click

        //define method : lnkSortByStyleSheetName_Click
        protected void lnkSortByStyleSheetName_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";

            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;

                //toggle sort order
                if (strSortOrder.Equals("StyleSheetName Desc") == true)
                {
                    strSortOrder = "StyleSheetName Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("StyleSheetName Asc") == true)
                {
                    strSortOrder = "StyleSheetName Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "StyleSheetName Asc";
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
                objClaimsLog.MessageIs = "Method : lnkSortByStyleSheetName_Click() ";
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
        }//end : lnkSortByStyleSheetName_Click

        //define method : lnkSortByStyleSheetName_Click
        protected void lnkSortByLastModified_Click(object sender, EventArgs e)
        {
            //declare variables
            string strSortOrder = "";

            try
            {
                //get existing sort order
                strSortOrder = this.lblSortOrder.Text;

                //toggle sort order
                if (strSortOrder.Equals("LastModified Desc") == true)
                {
                    strSortOrder = "LastModified Asc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else if (strSortOrder.Equals("LastModified Asc") == true)
                {
                    strSortOrder = "LastModified Desc";
                    this.lblSortOrder.Text = strSortOrder;
                }
                else
                {
                    strSortOrder = "LastModified Asc";
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
                objClaimsLog.MessageIs = "Method : lnkSortByStyleSheetName_Click() ";
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
        }//end : lnkSortByLastModified_Click


    }//end : public partial class DocDefDocList : System.Web.UI.Page


}//end : namespace ClaimsDocsClient.secure
