using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient.AppClasses;

namespace ClaimsDocsClient.secure
{
    //Hardcoded values
    //Review
    //DesignerID

    public partial class DocDefDocDefAdmin : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables
            int intState = -1;
            int intDocumentID = 0;

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
                        //get document id
                        if (int.TryParse(Request.Params["docdefid"].ToString(), out intDocumentID) == false)
                        {
                            intDocumentID = 0;
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
                        case 1: //Add document
                            this.lblState.Text = intState.ToString();
                            this.lblDocumentID.Text = "0";
                            this.lblHeader.Text = "Add Document";
                            break;

                        case 2: //Edit document
                            this.lblState.Text = intState.ToString();
                            this.lblDocumentID.Text = intDocumentID.ToString();
                            this.lblHeader.Text = "Edit Document";
                            proxyCDDocument2.Document objDocument = new proxyCDDocument2.Document();

                            //show document
                            objDocument.DocumentID = intDocumentID;
                            DocumentShow(objDocument);
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
        }//end : protected void Page_Load(object sender, EventArgs e)

        //define method : cmdDo_Click
        protected void cmdDo_Click(object sender, EventArgs e)
        {
            //declare variables
            int intResult = 0;
            int intState = -1;
            int intDocumentID = 0;
            int intIndex = 0;
            proxyCDDocument2.CDDocumentClient objDocumentClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            proxyCDDocument2.Document objDocument = new ClaimsDocsClient.proxyCDDocument2.Document();
            proxyCDDocument2.DocumentField objDocumentField = new proxyCDDocument2.DocumentField();
            List<proxyCDDocument2.DocumentField> listDocumentField = new List<proxyCDDocument2.DocumentField>();
            List<proxyCDDocument2.DocumentGroup> listDocumentGroup = new List<ClaimsDocsClient.proxyCDDocument2.DocumentGroup>();
            int intFieldIndex = 0;
            TextBox objTextBoxName;
            TextBox objTextBoxDesc;
            DropDownList objDropDownListType;
            DropDownList objDropDownListRequired;
            string strFieldPostfix = "";


            try
            {
                //gather values
                intState = int.Parse(this.lblState.Text.ToString());
                intDocumentID = int.Parse(this.lblDocumentID.Text.ToString());

                //process request based on state
                switch (intState)
                {
                    case 1: //add document

                        #region Add Document

                        //fill document
                        objDocument.DocumentID = 0;
                        objDocument.DocumentCode = this.txtDocumentID.Text.ToString();
                        objDocument.DepartmentID = int.Parse(this.cboDepartment.SelectedValue.ToString());
                        objDocument.ProgramID = int.Parse(this.cboProgram.SelectedValue.ToString());
                        objDocument.Review = "N";
                        objDocument.Description = this.txtDescription.Text;
                        objDocument.TemplateName = this.txtTemplateName.Text;
                        objDocument.StyleSheetName = this.txtStyleSheetName.Text;
                        objDocument.EffectiveDate = DateTime.Parse(this.txtEffectiveDate.Text);
                        objDocument.ExpirationDate = DateTime.Parse(this.txtExpirationDate.Text);
                        objDocument.ProofOfMailing = this.cboProofOfMailing.SelectedValue.ToString();
                        objDocument.DataMatx = this.cboImportToDatamatx.SelectedValue.ToString();
                        objDocument.ImportToImageRight = this.cboImportToImageRight.SelectedValue.ToString(); ;
                        objDocument.ImageRightDocumentID = this.txtImageRightDocumentID.Text;
                        objDocument.ImageRightDocumentSection = this.txtImageRightDocumentSession.Text;
                        objDocument.ImageRightDrawer = this.txtImageRightDrawer.Text;
                        
                        if(this.chkCopyToProducer.Checked==true)
                        {
                            objDocument.CopyAgent = "Y";
                        }
                        else
                        {
                            objDocument.CopyAgent = "N";
                        }

                        if (this.chkCopyToInsured.Checked == true)
                        {
                            objDocument.CopyInsured = "Y";
                        }
                        else
                        {
                            objDocument.CopyInsured = "N";
                        }

                        if (this.chkCopyToLienholder.Checked == true)
                        {
                            objDocument.CopyLienHolder = "Y";
                        }
                        else
                        {
                            objDocument.CopyLienHolder = "N";
                        }

                        if (this.chkCopyToFinanceCompany.Checked == true)
                        {
                            objDocument.CopyFinanceCo = "Y";
                        }
                        else
                        {
                            objDocument.CopyFinanceCo = "N";
                        }

                        if (this.chkCopyToAttorney.Checked == true)
                        {
                            objDocument.CopyAttorney = "Y";
                        }
                        else
                        {
                            objDocument.CopyAttorney = "N";
                        }

                        objDocument.DiaryNumberOfDays = int.Parse(this.txtDiaryNumberOfDays.Text.ToString());
                        objDocument.DiaryAutoUpdate = this.cboAutoDiaryUpdate.SelectedValue.ToString();
                        objDocument.DesignerID = 1;
                        objDocument.LastModified = DateTime.Now;
                        objDocument.Active = this.cboActive.SelectedValue.ToString();
                        objDocument.AttachedDocument = this.txtAttachedDocument.Text;
                        objDocument.IUDateTime = DateTime.Now;

                        //get document groups
                        for (intIndex = 0; intIndex < this.lstGroups.Items.Count; intIndex++)
                        {
                            if (this.lstGroups.Items[intIndex].Selected == true)
                            {
                                proxyCDDocument2.DocumentGroup objDocumentGroup = new ClaimsDocsClient.proxyCDDocument2.DocumentGroup();
                                objDocumentGroup.DocumentID = 0;
                                objDocumentGroup.GroupID = int.Parse(this.lstGroups.Items[intIndex].Value.ToString());
                                objDocumentGroup.IUDateTime = DateTime.Now;
                                listDocumentGroup.Add(objDocumentGroup);
                            }
                        }

                        //apply document group list to document
                        objDocument.listDocumentGroup = listDocumentGroup;

                        //get document fields
                        for (intFieldIndex = 1; intFieldIndex <= 20; intFieldIndex++)
                        {
                            //set field post fix
                            strFieldPostfix = "";
                            if (intFieldIndex.ToString().Length == 1)
                            {
                                strFieldPostfix = "0" + intFieldIndex.ToString();
                            }
                            else
                            {
                                strFieldPostfix = intFieldIndex.ToString();
                            }

                            //create document field object
                            proxyCDDocument2.DocumentField objDocumentFieldIs = new proxyCDDocument2.DocumentField();
                            //fill document field
                            objDocumentFieldIs.DocumentFieldID = 0;
                            objDocumentFieldIs.DocumentID = 0;

                            //get controls
                            objTextBoxName = (TextBox)this.FindControl("txtFieldName" + strFieldPostfix);
                            objTextBoxDesc = (TextBox)this.FindControl("txtDescription" + strFieldPostfix );
                            objDropDownListType = (DropDownList)this.FindControl("cboType" + strFieldPostfix);
                            objDropDownListRequired = (DropDownList)this.FindControl("cboRequired" + strFieldPostfix);

                            //save control values
                            if(string.IsNullOrEmpty(objTextBoxName.Text.ToString()))
                            {
                                objDocumentFieldIs.FieldNameIs = "";
                            }
                            else
                            {
                                objDocumentFieldIs.FieldNameIs = objTextBoxName.Text;
                            }

                            if(string.IsNullOrEmpty(objTextBoxDesc.Text.ToString()))
                            {
                                objDocumentFieldIs.FieldDescription = "";
                            }
                            else
                            {
                                objDocumentFieldIs.FieldDescription = objTextBoxDesc.Text;
                            }

                            if(string.IsNullOrEmpty(objDropDownListType.SelectedValue.ToString()))
                            {
                                objDocumentFieldIs.FieldTypeIs = "";
                            }
                            else
                            {
                                objDocumentFieldIs.FieldTypeIs = objDropDownListType.SelectedValue.ToString();
                            }

                            if (string.IsNullOrEmpty(objDropDownListRequired.SelectedValue.ToString()))
                            {
                                objDocumentFieldIs.IsFieldRequired = "";
                            }
                            else
                            {
                                objDocumentFieldIs.IsFieldRequired = objDropDownListRequired.SelectedValue.ToString();
                            }

                            objDocumentFieldIs.IUDateTime = DateTime.Now;

                            //only add the field to the list if both the field name and description have values
                            if ((objDocumentFieldIs.FieldNameIs.Length > 0) && (objDocumentFieldIs.FieldDescription.Length>0))
                            {
                                listDocumentField.Add(objDocumentFieldIs);
                            }
                        }

                        //apply document field list to document
                        objDocument.lisDocumentField = listDocumentField;

                        //add document
                        intResult = objDocumentClient.DocumentCreate(objDocument, AppConfig.CorrespondenceDBConnectionString);
                        //check results
                        if (intResult == 0)
                        {
                        }
                        else
                        {
                            //save state and document id
                            this.lblState.Text = intState.ToString();
                            this.lblDocumentID.Text = intResult.ToString();
                        }

                        #endregion

                        break;

                    case 2: //edit document
                        
                        #region Edit Document

                        //fill document
                        objDocument.DocumentID = intDocumentID;
                        objDocument.DocumentCode = this.txtDocumentID.Text.ToString();
                        objDocument.DepartmentID = int.Parse(this.cboDepartment.SelectedValue.ToString());
                        objDocument.ProgramID = int.Parse(this.cboProgram.SelectedValue.ToString());
                        objDocument.Review = this.cboApprovalRequired.SelectedValue.ToString();
                        objDocument.Description = this.txtDescription.Text;
                        objDocument.TemplateName = this.txtTemplateName.Text;
                        objDocument.StyleSheetName = this.txtStyleSheetName.Text;
                        objDocument.EffectiveDate = DateTime.Parse(this.txtEffectiveDate.Text);
                        objDocument.ExpirationDate = DateTime.Parse(this.txtExpirationDate.Text);
                        objDocument.ProofOfMailing = this.cboProofOfMailing.SelectedValue.ToString();
                        objDocument.DataMatx = this.cboImportToDatamatx.SelectedValue.ToString();
                        objDocument.ImportToImageRight = this.cboImportToImageRight.SelectedValue.ToString(); ;
                        objDocument.ImageRightDocumentID = this.txtImageRightDocumentID.Text;
                        objDocument.ImageRightDocumentSection = this.txtImageRightDocumentSession.Text;
                        objDocument.ImageRightDrawer = this.txtImageRightDrawer.Text;

                        if (this.chkCopyToProducer.Checked == true)
                        {
                            objDocument.CopyAgent = "Y";
                        }
                        else
                        {
                            objDocument.CopyAgent = "N";
                        }

                        if (this.chkCopyToInsured.Checked == true)
                        {
                            objDocument.CopyInsured = "Y";
                        }
                        else
                        {
                            objDocument.CopyInsured = "N";
                        }

                        if (this.chkCopyToLienholder.Checked == true)
                        {
                            objDocument.CopyLienHolder = "Y";
                        }
                        else
                        {
                            objDocument.CopyLienHolder = "N";
                        }

                        if (this.chkCopyToFinanceCompany.Checked == true)
                        {
                            objDocument.CopyFinanceCo = "Y";
                        }
                        else
                        {
                            objDocument.CopyFinanceCo = "N";
                        }

                        if (this.chkCopyToAttorney.Checked == true)
                        {
                            objDocument.CopyAttorney = "Y";
                        }
                        else
                        {
                            objDocument.CopyAttorney = "N";
                        }

                        objDocument.DiaryNumberOfDays = int.Parse(this.txtDiaryNumberOfDays.Text.ToString());
                        objDocument.DiaryAutoUpdate = this.cboAutoDiaryUpdate.SelectedValue.ToString();
                        objDocument.DesignerID = 1;
                        objDocument.LastModified = DateTime.Now;
                        objDocument.Active = this.cboActive.SelectedValue.ToString();
                        objDocument.AttachedDocument = this.txtAttachedDocument.Text;
                        objDocument.IUDateTime = DateTime.Now;

                        //get document groups
                        for (intIndex = 0; intIndex < this.lstGroups.Items.Count; intIndex++)
                        {
                            if (this.lstGroups.Items[intIndex].Selected == true)
                            {
                                proxyCDDocument2.DocumentGroup objDocumentGroup = new ClaimsDocsClient.proxyCDDocument2.DocumentGroup();
                                objDocumentGroup.DocumentID = 0;
                                objDocumentGroup.GroupID = int.Parse(this.lstGroups.Items[intIndex].Value.ToString());
                                objDocumentGroup.IUDateTime = DateTime.Now;
                                listDocumentGroup.Add(objDocumentGroup);
                            }
                        }

                        //apply document group list to document
                        objDocument.listDocumentGroup = listDocumentGroup;

                        //get document fields
                        for (intFieldIndex = 1; intFieldIndex <= 20; intFieldIndex++)
                        {
                            //set field post fix
                            strFieldPostfix = "";
                            if (intFieldIndex.ToString().Length == 1)
                            {
                                strFieldPostfix = "0" + intFieldIndex.ToString();
                            }
                            else
                            {
                                strFieldPostfix = intFieldIndex.ToString();
                            }

                            //create document field object
                            proxyCDDocument2.DocumentField objDocumentFieldIs = new proxyCDDocument2.DocumentField();
                            //fill document field
                            objDocumentFieldIs.DocumentFieldID = 0;
                            objDocumentFieldIs.DocumentID = 0;

                            //get controls
                            objTextBoxName = (TextBox)this.FindControl("txtFieldName" + strFieldPostfix);
                            objTextBoxDesc = (TextBox)this.FindControl("txtDescription" + strFieldPostfix);
                            objDropDownListType = (DropDownList)this.FindControl("cboType" + strFieldPostfix);
                            objDropDownListRequired = (DropDownList)this.FindControl("cboRequired" + strFieldPostfix);

                            //save control values
                            if (string.IsNullOrEmpty(objTextBoxName.Text.ToString()))
                            {
                                objDocumentFieldIs.FieldNameIs = "";
                            }
                            else
                            {
                                objDocumentFieldIs.FieldNameIs = objTextBoxName.Text;
                            }

                            if (string.IsNullOrEmpty(objTextBoxDesc.Text.ToString()))
                            {
                                objDocumentFieldIs.FieldDescription = "";
                            }
                            else
                            {
                                objDocumentFieldIs.FieldDescription = objTextBoxDesc.Text;
                            }

                            if (string.IsNullOrEmpty(objDropDownListType.SelectedValue.ToString()))
                            {
                                objDocumentFieldIs.FieldTypeIs = "";
                            }
                            else
                            {
                                objDocumentFieldIs.FieldTypeIs = objDropDownListType.SelectedValue.ToString();
                            }

                            if (string.IsNullOrEmpty(objDropDownListRequired.SelectedValue.ToString()))
                            {
                                objDocumentFieldIs.IsFieldRequired = "";
                            }
                            else
                            {
                                objDocumentFieldIs.IsFieldRequired = objDropDownListRequired.SelectedValue.ToString();
                            }

                            objDocumentFieldIs.IUDateTime = DateTime.Now;

                            //only add the field to the list if both the field name and description have values
                            if ((objDocumentFieldIs.FieldNameIs.Length > 0) && (objDocumentFieldIs.FieldDescription.Length > 0))
                            {
                                listDocumentField.Add(objDocumentFieldIs);
                            }
                        }

                        //apply document field list to document
                        objDocument.lisDocumentField = listDocumentField;

                        //add document
                        intResult = objDocumentClient.DocumentUpdate(objDocument, AppConfig.CorrespondenceDBConnectionString);
                        //check results
                        if (intResult == 0)
                        {
                        }
                        else
                        {
                            //save state and document id
                            this.lblState.Text = intState.ToString();
                            this.lblDocumentID.Text = intResult.ToString();
                        }

                        #endregion
                        break;

                    default: //do nothing
                        break;

                }//end : switch (intState)

                Page.Response.Redirect("./DocDefDocList.aspx", false);

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
                if (objDocumentClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocumentClient.Close();
                }
                objDocumentClient = null;
                objDocument = null;
            }
        }//end : protected void cmdDo_Click(object sender, EventArgs e)

        //define method : Show List Item(s)
        private bool ShowListItems()
        {
            //declare variables
            bool blnResult = true;
            proxyCDDepartment.CDDepartmentsClient objDepartmentClient = new proxyCDDepartment.CDDepartmentsClient();
            List<proxyCDDepartment.Department> listDepartments;

            proxyCDProgram.CDProgramClient objProgramClient = new proxyCDProgram.CDProgramClient();
            List<proxyCDProgram.Program> listProgram = new List<ClaimsDocsClient.proxyCDProgram.Program>();

            proxyCDGroup.CDGroupsClient objGroupClient = new proxyCDGroup.CDGroupsClient();
            List<proxyCDGroup.Group> listGroup = new List<proxyCDGroup.Group>();

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
                strSQL = "Select ProgramID,ProgramCode, IUDateTime From tblProgram Order By ProgramCode";
                //get program list
                listProgram = objProgramClient.ProgramSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);
                //check program list
                if (listProgram != null)
                {
                    this.cboProgram.DataSource = listProgram;
                    this.cboProgram.DataBind();
                }

                //build sql string
                strSQL = "Select tblDepartment.DepartmentName,tblGroup.* From dbo.tblGroup Inner Join tblDepartment On tblDepartment.DepartmentID=tblGroup.DepartmentID Order By GroupName";
                //get group list
                listGroup = objGroupClient.GroupSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);
                //check group list
                if (listGroup != null)
                {
                    this.lstGroups.DataSource = listGroup;
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
                if (objDepartmentClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDepartmentClient.Close();
                }
                objDepartmentClient = null;

                if (objProgramClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objProgramClient.Close();
                }
                objProgramClient = null;

                if (objGroupClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objGroupClient.Close();
                }
                objGroupClient = null;  


            }

            //return
            return (blnResult);

        }

        //define method : Show Document
        private bool DocumentShow(proxyCDDocument2.Document objDocument)
        {
            //declare variables
            bool blnResult = true;
            proxyCDDocument2.CDDocumentClient objDocumentClient = new ClaimsDocsClient.proxyCDDocument2.CDDocumentClient();
            List<proxyCDDocument2.Document> listDocuments = new List<ClaimsDocsClient.proxyCDDocument2.Document>();
            List<proxyCDDocument2.DocumentGroup> listDocumentGroup = new List<ClaimsDocsClient.proxyCDDocument2.DocumentGroup>();
            proxyCDDocument2.Document objDocumentIs;
            int intListIndex = 0;
            int intIndex = 0;

            try
            {
                //get document definition
                objDocumentIs = objDocumentClient.DocumentRead(objDocument, AppConfig.CorrespondenceDBConnectionString);
                //get document groups
                listDocumentGroup = objDocumentClient.DocumentGroupSearch("Select * From tblDocumentGroup Where DocumentID=" +  objDocument.DocumentID.ToString(), AppConfig.CorrespondenceDBConnectionString);

                //check document
                if (objDocumentIs != null)
                {
                    //show document
                    this.txtDocumentID.Text = objDocumentIs.DocumentCode.ToString();
                    this.txtDescription.Text = objDocumentIs.Description;

                    //select department in drop-down
                    for (intIndex = 0; intIndex < this.cboDepartment.Items.Count; intIndex++)
                    {
                        if (this.cboDepartment.Items[intIndex].Value.ToString().Equals(objDocumentIs.DepartmentID.ToString()))
                        {
                            this.cboDepartment.SelectedIndex = intIndex;
                        }
                    }

                    //select program in drop-down
                    for (intIndex = 0; intIndex < this.cboProgram.Items.Count; intIndex++)
                    {
                        if (this.cboProgram.Items[intIndex].Value.ToString().Equals(objDocumentIs.ProgramID.ToString()))
                        {
                            this.cboProgram.SelectedIndex = intIndex;
                        }
                    }

                    //select document groups
                    for (intListIndex = 0; intListIndex < listDocumentGroup.Count; intListIndex++)
                    {
                        for (intIndex = 0; intIndex < this.lstGroups.Items.Count; intIndex++)
                        {
                            if (this.lstGroups.Items[intIndex].Value.ToString().Equals(listDocumentGroup[intListIndex].GroupID.ToString()))
                            {
                                this.lstGroups.Items[intIndex].Selected = true;
                            }
                        }
                    }

                    this.txtTemplateName.Text = objDocumentIs.TemplateName;
                    this.txtStyleSheetName.Text = objDocumentIs.StyleSheetName;
                    this.txtAttachedDocument.Text = objDocumentIs.AttachedDocument;
                    if(string.IsNullOrEmpty(objDocumentIs.EffectiveDate.ToString())==false)
                    {
                        this.txtEffectiveDate.Text = objDocumentIs.EffectiveDate.Date.ToString("d");
                    }

                    if (string.IsNullOrEmpty(objDocumentIs.ExpirationDate.ToString()) == false)
                    {
                        this.txtExpirationDate.Text = objDocumentIs.ExpirationDate.ToString("d");

                    }
                    if (objDocumentIs.ProofOfMailing.Equals("N"))
                    {
                        this.cboProofOfMailing.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cboProofOfMailing.SelectedIndex = 1;
                    }

                    if (objDocumentIs.DataMatx.Equals("N"))
                    {
                        this.cboImportToDatamatx.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cboImportToDatamatx.SelectedIndex = 1;
                    }

                    //is this the review field?
                    if (objDocumentIs.Review.Equals("N"))
                    {
                        this.cboApprovalRequired.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cboApprovalRequired.SelectedIndex = 1;
                    }

                    if (objDocumentIs.DiaryAutoUpdate.Equals("N"))
                    {
                        this.cboAutoDiaryUpdate.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cboAutoDiaryUpdate.SelectedIndex = 1;
                    }

                    this.txtDiaryNumberOfDays.Text = objDocumentIs.DiaryNumberOfDays.ToString();

                    if (objDocumentIs.ImportToImageRight.Equals("N"))
                    {
                        this.cboImportToImageRight.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cboImportToImageRight.SelectedIndex = 1;
                    }

                    this.txtImageRightDocumentID.Text = objDocumentIs.ImageRightDocumentID.ToString();
                    this.txtImageRightDocumentSession.Text = objDocumentIs.ImageRightDocumentSection.ToString();
                    this.txtImageRightDrawer.Text = objDocumentIs.ImageRightDrawer.ToString();

                    if (objDocumentIs.Active.Equals("N"))
                    {
                        this.cboActive.SelectedIndex = 0;
                    }
                    else
                    {
                        this.cboActive.SelectedIndex = 1;
                    }

                    this.lblLastModified.Text = objDocumentIs.LastModified.ToString();

                    if (objDocumentIs.CopyAgent.Equals("N"))
                    {
                        this.chkCopyToProducer.Checked = false;
                    }
                    else
                    {
                        this.chkCopyToProducer.Checked = true;
                    }

                    if (objDocumentIs.CopyInsured.Equals("N"))
                    {
                        this.chkCopyToInsured.Checked = false;
                    }
                    else
                    {
                        this.chkCopyToInsured.Checked = true;
                    }

                    if (objDocumentIs.CopyLienHolder.Equals("N"))
                    {
                        this.chkCopyToLienholder.Checked = false;
                    }
                    else
                    {
                        this.chkCopyToLienholder.Checked = true;
                    }

                    if (objDocumentIs.CopyFinanceCo.Equals("N"))
                    {
                        this.chkCopyToFinanceCompany.Checked = false;
                    }
                    else
                    {
                        this.chkCopyToFinanceCompany.Checked = true;
                    }

                    if (objDocumentIs.CopyAttorney.Equals("N"))
                    {
                        this.chkCopyToAttorney.Checked = false;
                    }
                    else
                    {
                        this.chkCopyToAttorney.Checked = true;
                    }

                    //show document fields
                    if (ShowDocumentFields(objDocumentIs) == false)
                    {
                    }

                }//end :if (objDocumentIs != null)
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
                objClaimsLog.MessageIs = "Method : DocumentShow() ";
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
                if (objDocumentClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocumentClient.Close();
                }
                objDocumentClient = null;
                objDocumentIs = null;
            }

            //return
            return (blnResult);

        }//end : private bool DocumentShow(proxyCDDocument2.Document objDocument)

        #region Document Field Methods

        private bool ShowDocumentFields(proxyCDDocument2.Document objDocument)
        {
            //declare variables
            bool blnResult = true;
            proxyCDDocumentField.CDDocumentFieldClient objDocumentFieldClient = new ClaimsDocsClient.proxyCDDocumentField.CDDocumentFieldClient();
            List<proxyCDDocumentField.DocumentField> listDocumentField = new List<ClaimsDocsClient.proxyCDDocumentField.DocumentField>();
            string strSQL = "";
            int intIndex = 0;
            int intLoopIndex = 0;

            try
            {
                //build sql string
                strSQL = "Select * From tblDocumentField Where DocumentID=" + objDocument.DocumentID.ToString();

                //get a list of document fields for document
                //KDP-123 listDocumentField = objDocumentFieldClient.DocumentFieldSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);
                listDocumentField = objDocumentFieldClient.DocumentFieldSearch(strSQL, AppConfig.CorrespondenceDBConnectionString);
                //listDocumentField = null;

                //check results
                if (listDocumentField != null)
                {
                    //iterate over list
                    for (intIndex = 0; intIndex < listDocumentField.Count; intIndex++)
                    {
                        //show current items
                        switch (intIndex+1)
                        {
                            case 1:
                                this.txtFieldName01.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription01.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType01.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType01.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType01.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired01.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired01.SelectedIndex = 1;
                                }
                                break;

                            case 2:
                                this.txtFieldName02.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription02.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType02.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType02.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType02.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired02.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired02.SelectedIndex = 1;
                                }
                                break;

                            case 3:
                                this.txtFieldName03.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription03.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType03.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType03.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType03.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired03.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired03.SelectedIndex = 1;
                                }
                                break;

                            case 4:
                                this.txtFieldName04.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription04.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType04.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType04.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType04.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired04.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired04.SelectedIndex = 1;
                                }
                                break;

                            case 5:
                                this.txtFieldName05.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription05.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType05.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType05.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType05.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired05.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired05.SelectedIndex = 1;
                                }
                                break;

                            case 6:
                                this.txtFieldName06.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription06.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType06.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType06.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType06.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired06.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired06.SelectedIndex = 1;
                                }
                                break;

                            case 7:
                                this.txtFieldName07.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription07.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType07.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType07.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType07.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired07.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired07.SelectedIndex = 1;
                                }
                                break;

                            case 8:
                                this.txtFieldName08.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription08.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType08.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType08.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType08.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired08.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired08.SelectedIndex = 1;
                                }
                                break;

                            case 9:
                                this.txtFieldName09.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription09.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType09.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType09.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType09.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired09.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired09.SelectedIndex = 1;
                                }
                                break;

                            case 10:
                                this.txtFieldName10.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription10.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType10.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType10.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType10.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired10.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired10.SelectedIndex = 1;
                                }
                                break;

                            case 11:
                                this.txtFieldName11.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription11.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType11.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType11.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType11.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired11.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired11.SelectedIndex = 1;
                                }
                                break;

                            case 12:
                                this.txtFieldName12.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription12.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType12.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType12.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType12.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired12.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired12.SelectedIndex = 1;
                                }
                                break;

                            case 13:
                                this.txtFieldName13.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription13.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType13.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType13.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType13.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired13.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired13.SelectedIndex = 1;
                                }
                                break;

                            case 14:
                                this.txtFieldName14.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription14.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType14.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType14.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType14.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired14.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired14.SelectedIndex = 1;
                                }
                                break;

                            case 15:
                                this.txtFieldName15.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription15.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType15.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType15.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType15.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired15.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired15.SelectedIndex = 1;
                                }
                                break;

                            case 16:
                                this.txtFieldName16.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription16.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType16.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType16.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType16.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired16.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired16.SelectedIndex = 1;
                                }
                                break;

                            case 17:
                                this.txtFieldName17.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription17.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType17.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType17.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType17.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired17.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired17.SelectedIndex = 1;
                                }
                                break;

                            case 18:
                                this.txtFieldName18.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription18.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType18.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType18.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType18.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired18.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired18.SelectedIndex = 1;
                                }
                                break;

                            case 19:
                                this.txtFieldName19.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription19.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType19.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType19.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType19.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired19.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired19.SelectedIndex = 1;
                                }
                                break;

                            case 20:
                                this.txtFieldName20.Text = listDocumentField[intIndex].FieldNameIs.ToString();
                                this.txtDescription20.Text = listDocumentField[intIndex].FieldDescription.ToString();

                                //select field type
                                for (intLoopIndex = 0; intLoopIndex < this.cboType20.Items.Count; intLoopIndex++)
                                {
                                    if (this.cboType20.Items[intLoopIndex].Value.ToString().ToUpper().Equals(listDocumentField[intIndex].FieldTypeIs.ToString().ToUpper()))
                                    {
                                        this.cboType20.SelectedIndex = intLoopIndex;
                                    }
                                }

                                if (listDocumentField[intIndex].IsFieldRequired.ToString().Equals("N"))
                                {
                                    this.cboRequired20.SelectedIndex = 0;
                                }
                                else
                                {
                                    this.cboRequired20.SelectedIndex = 1;
                                }
                                break;


                            default:
                                //do nothing
                                break;
                        }//end : switch (intIndex)
                    }//end : for (intIndex = 0; intIndex < listDocumentField.Count; intIndex++)
                }//end : if (listDocumentField != null)

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
                //cleanup
                if (objDocumentFieldClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objDocumentFieldClient.Close();
                }
                objDocumentFieldClient = null;
            }

            //return result
            return (blnResult);

        }//end : ShowDocumentFields
        
        //define method : cmdFillForm_Click
        protected void cmdFillForm_Click(object sender, EventArgs e)
        {
            //declare variables

            try
            {
                //fill form                
                this.txtDocumentID.Text = "ACCESS-001";
                this.txtDescription.Text = "Access Test Document Definition";
                this.cboDepartment.SelectedIndex = 2;
                this.cboProgram.SelectedIndex = 3;
                this.lstGroups.Items[0].Selected = true;
                this.lstGroups.Items[1].Selected = true;
                this.lstGroups.Items[2].Selected = true;
                this.lstGroups.Items[3].Selected = true;
                this.lstGroups.Items[4].Selected = true;
                this.txtTemplateName.Text = "Access Test Template Name";
                this.txtStyleSheetName.Text = "Access Test Stylesheet Name";
                this.txtAttachedDocument.Text = "Test Attached Document";
                this.txtEffectiveDate.Text = "03/29/2010";
                this.txtExpirationDate.Text = "03/29/2010";
                this.cboProofOfMailing.SelectedIndex = 1;
                this.txtDiaryNumberOfDays.Text = "7";
                this.txtImageRightDocumentID.Text = "LLC";
                this.txtImageRightDocumentSession.Text = "10400";
                this.txtImageRightDrawer.Text = "CLMS";
                this.chkCopyToProducer.Checked = true;
                this.chkCopyToInsured.Checked = true;
                this.chkCopyToLienholder.Checked = true;
                this.chkCopyToFinanceCompany.Checked = true;
                this.chkCopyToAttorney.Checked = true;

                this.txtFieldName01.Text = "Liability Limits";
                this.txtDescription01.Text = "Liability Limits";
                this.cboType01.SelectedIndex = 1;
                this.cboRequired01.SelectedIndex = 1;

                this.txtFieldName02.Text = "PD Limits";
                this.txtDescription02.Text = "Property Damage Limits";
                this.cboType02.SelectedIndex = 1;
                this.cboRequired02.SelectedIndex = 1;

                this.txtFieldName02.Text = "Med Pymt Limits";
                this.txtDescription02.Text = "Medical Payment Limits";
                this.cboType02.SelectedIndex = 1;
                this.cboRequired02.SelectedIndex = 1;

                
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
                objClaimsLog.MessageIs = "Method : cmdFillForm_Click() ";
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

        }//end : cmdFillForm_Click

        #endregion

    }//end : public partial class DocDefDocDefAdmin : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.secure
