using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClaimsDocsClient.AppClasses;


namespace ClaimsDocsClient.secure
{
    public partial class ProgramAdmin : System.Web.UI.Page
    {
        //define method : Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            //declare variables
            int intState = -1;
            int intProgramID = 0;

            try
            {
                //check for postback
                if (this.Page.IsPostBack == false)
                {
                    //get state
                    if (int.TryParse(Request.Params["state"].ToString(), out intState) == true)
                    {
                        //get program id
                        if (int.TryParse(Request.Params["programid"].ToString(), out intProgramID) == false)
                        {
                            intProgramID = 0;
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
                        case 1: //Add program
                            this.lblState.Text = intState.ToString();
                            this.lblProgramID.Text = "0";
                            this.lblHeader.Text = "Add Program";
                            break;

                        case 2: //Edit program
                            this.lblState.Text = intState.ToString();
                            this.lblProgramID.Text = intProgramID.ToString();
                            this.lblHeader.Text = "Edit Program";
                            proxyCDProgram.Program objProgram = new ClaimsDocsClient.proxyCDProgram.Program();

                            //show Program
                            objProgram.ProgramID = intProgramID;
                            ProgramShow(objProgram);

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
            int intProgramID = 0;
            string strProgramCode = "";
            proxyCDProgram.Program objProgram = new ClaimsDocsClient.proxyCDProgram.Program();
            proxyCDProgram.CDProgramClient objProgramClient = new ClaimsDocsClient.proxyCDProgram.CDProgramClient();

            try
            {
                //gather values
                intState = int.Parse(this.lblState.Text.ToString());
                intProgramID = int.Parse(this.lblProgramID.Text.ToString());

                //get Program name
                strProgramCode = this.txtProgramCode.Text;

                //process request based on state
                switch (intState)
                {
                    case 1: //add Program
                        //fill Program
                        objProgram.ProgramID = 0;
                        objProgram.ProgramCode = strProgramCode;
                        objProgram.IUDateTime = DateTime.Now;

                        //add Program
                        intResult = objProgramClient.ProgramCreate(objProgram, AppConfig.CorrespondenceDBConnectionString);
                        //check results
                        if (intResult == 0)
                        {
                        }
                        else
                        {
                        }
                        break;

                    case 2: //edit Program
                        //fill Program
                        objProgram.ProgramID = intProgramID;
                        objProgram.ProgramCode = strProgramCode;
                        objProgram.IUDateTime = DateTime.Now;

                        //add Program
                        intResult = objProgramClient.ProgramUpdate(objProgram, AppConfig.CorrespondenceDBConnectionString);
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
                if (objProgramClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objProgramClient.Close();
                }
                objProgramClient = null;
                objProgram = null;
                
            }

        }//end : protected void cmdDo_Click(object sender, EventArgs e)

        //define method : Show Program
        private bool ProgramShow(proxyCDProgram.Program objProgram)
        {
            //declare variables
            bool blnResult = true;
            proxyCDProgram.CDProgramClient objCDProgramClient = new ClaimsDocsClient.proxyCDProgram.CDProgramClient();
            proxyCDProgram.Program objProgramIs;

            try
            {
                //get program
                objProgramIs = objCDProgramClient.ProgramRead(objProgram, AppConfig.CorrespondenceDBConnectionString);
                //check results
                if (objProgramIs != null)
                {
                    //show program
                    this.lblProgramID.Text = objProgramIs.ProgramID.ToString();
                    this.txtProgramCode.Text = objProgramIs.ProgramCode.ToString();
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
                objClaimsLog.MessageIs = "Method : ProgramShow() ";
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
                if (objCDProgramClient.State == System.ServiceModel.CommunicationState.Opened)
                {
                    objCDProgramClient.Close();
                }

                objCDProgramClient = null;
                objProgramIs = null;
            }

            //return
            return (blnResult);

        }//end : private bool ProgramShow(proxyCDProgram.Program objProgram)

    }//end : public partial class ProgramAdmin : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.secure
