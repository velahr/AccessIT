using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace ClaimsDocsClient.AppClasses
{
    public class AppConfig
    {
        //declare private member properties
        static private string _strRunMode = "";
        static private string _CorrespondenceDBConnectionString = "";

        //define class property members
        static public string RunMode
        {
            get
            {
                if (_strRunMode == "")
                {
                    if (ConfigurationSettings.AppSettings["RunMode"] == null)
                    {
                        return ("");
                    }
                    else
                    {
                        return ((string)ConfigurationSettings.AppSettings["RunMode"]);
                    }
                }
                else
                {
                    return (_strRunMode);
                }
            }
        }//end : RunMode
        static public string CorrespondenceDBConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_CorrespondenceDBConnectionString))
                {
                    switch (AppConfig.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _CorrespondenceDBConnectionString = ConfigurationManager.ConnectionStrings["DEVMACHINECorrespondenceConnectionString"].ToString();
                            break;

                        case "DEVELOPMENT":
                            _CorrespondenceDBConnectionString = ConfigurationManager.ConnectionStrings["DevelopmentCorrespondenceDBConnectionString"].ToString();
                            break;

                        case "ITQA":
                            _CorrespondenceDBConnectionString = ConfigurationManager.ConnectionStrings["ITQACorrespondenceDBConnectionString"].ToString();
                            break;

                        case "UAT":
                            _CorrespondenceDBConnectionString = ConfigurationManager.ConnectionStrings["UATCorrespondenceDBConnectionString"].ToString();
                            break;

                        case "PRODUCTION":
                            _CorrespondenceDBConnectionString = ConfigurationManager.ConnectionStrings["PRODCorrespondenceDBConnectionString"].ToString();
                            break;

                        default:
                            _CorrespondenceDBConnectionString = "";
                            break;
                    }//end switch

                    //return value
                    return (_CorrespondenceDBConnectionString);
                }
                else
                {
                    return (_CorrespondenceDBConnectionString);
                }
            }
        }//end : CorrespondenceDBConnectionString

        #region Support Methods

        //define method : CompanyPrefixToCompanyNumber
        static public int CompanyPrefixToCompanyNumber(string strCompanyPrefix)
        {
            //declare variables
            int intCompanyNumber = 0;

            try
            {
                //translate
                switch(strCompanyPrefix.ToUpper())
                {
                   case "AGA":
                        intCompanyNumber=40;
                        break;

                     case "APA":
                        intCompanyNumber = 41;
                        break;

                    case "ACA":
                        intCompanyNumber=42;
                        break;

                    case "AAL":
                        intCompanyNumber = 43;
                        break;

                    case "AFA":
                        intCompanyNumber = 44;
                        break;

                    case "ATX":
                        intCompanyNumber = 45;
                        break;

                    case "AAZ":
                        intCompanyNumber = 46;
                        break;

                    case "ASC":
                        intCompanyNumber = 47;
                        break;

                    case "ALA":
                        intCompanyNumber = 48;
                        break;

                    case "ANV":
                        intCompanyNumber = 49;
                        break;

                    case "AMS":
                        intCompanyNumber = 50;
                        break;

                    case "AOK":
                        intCompanyNumber = 51;
                        break;

                    case "ATN":
                        intCompanyNumber = 52;
                        break;

                    case "AIN":
                        intCompanyNumber = 53;
                        break;


                }//end : switch(strCompanyPrefix.ToUpper())
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
                objClaimsLog.MessageIs = "Method : CompanyPrefixToCompanyNumber() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objSupport.ClaimsDocsLogCreate(objClaimsLog, AppConfig.CorrespondenceDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objSupport = null;
            }

            //return result
            return (intCompanyNumber);

        }//end : CompanyPrefixToCompanyNumber


        #endregion

    }//end : public class AppConfig
}//end : namespace ClaimsDocsClient.AppClasses
