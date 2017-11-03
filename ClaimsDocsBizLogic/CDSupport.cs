using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.Remoting;
using ClaimsDocsBizLogic.Properties;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Net.Mail;


namespace ClaimsDocsBizLogic
{
    //define class CDSupport
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CDSupport : ICDSupport
    {
        //declare private generic member properties
        static private string _strSMTPHost = "";
        static private string _strEMailDeclineSubject = "";

        //declare private member properties
        static private string _strRunMode = "";
        static private string _strClaimsDocsDBConnectionString = "";
        static private string _strConnectionString = "";
        static private string _strCorrespondenceDBString = "";
        static private string _strClaimsDBConnectionString = "";
        static private string _strAMSDBConnectionString = "";
        static private string _strGenesisDBConnectionString = "";
        static private string _strXMLOutputLocation = "";
        static private string _strXMLUNCOutputLocation = "";
        static private string _strClaimsDocsBizLogicLogFile = "";

        //declare private member DocuMaker Integration properties
        private static string _strDocumakerReplyQueue = "";
        private static string _strDocumakerRequestQueue = "";
        private static string _strDocumakerEnvironment = "";
        private static string _strDocumakerUNCRoot = "";
        private static TimeSpan _tspDocuMakerReceiveTimeout;
        private static int _intDocuMakerPeekPauseInSeconds = -1;
        private static string _strDocumentGenerator = "";
        private static int _intDocuMakerNumberOfTries = -1;
        private static int _intNumberOfResponseTries = -1;
        private static int _intMessageQueueLoopTries = -1;
        private static string _strClaimsDocsNameSpace = "";
        //declare private member ImageRight Integration properties
        private static string _strImageRightWSURL = "";
        private static string _strImageRightLogin = "";
        private static string _strImageRightPassword = "";
        private static string _strImageRightClaimsDrawer = "";
        private static string _strImageRightReason = "";
        private static string _strImageRightDescription = "";
        private static int _intImageRightFlowID = 0;
        private static int _intImageRightStepID = 0;
        private static int _intImageRightPriority = 0;

        //declare public generic properties
        static public string SMTPHost
        {
            get
            {
                if (_strSMTPHost == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strSMTPHost = (string)Settings.Default.DEVMACHINESmtpHost;
                            break;

                        case "DEVELOPMENT":
                            _strSMTPHost = (string)Settings.Default.DEVSmtpHost;
                            break;

                        case "ITQA":
                            _strSMTPHost = (string)Settings.Default.ITQASmtpHost;
                            break;

                        case "UAT":
                            _strSMTPHost = (string)Settings.Default.UATSmtpHost;
                            break;

                        case "PRODUCTION":
                            _strSMTPHost = (string)Settings.Default.PRODSmtpHost;
                            break;

                        default:
                            _strSMTPHost = "";
                            break;
                    }//end switch

                    //return value
                    return (_strSMTPHost);
                }
                else
                {
                    return (_strSMTPHost);
                }
            }
        }//end : SMTPHost
        static public string EMailDeclineSubject
        {
            get
            {
                if (_strEMailDeclineSubject == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strEMailDeclineSubject = (string)Settings.Default.EMailDeclineSubject;
                            break;

                        case "DEVELOPMENT":
                            _strEMailDeclineSubject = (string)Settings.Default.EMailDeclineSubject;
                            break;

                        case "ITQA":
                            _strEMailDeclineSubject = (string)Settings.Default.EMailDeclineSubject;
                            break;

                        case "UAT":
                            _strEMailDeclineSubject = (string)Settings.Default.EMailDeclineSubject;
                            break;

                        case "PRODUCTION":
                            _strEMailDeclineSubject = (string)Settings.Default.EMailDeclineSubject;
                            break;

                        default:
                            _strEMailDeclineSubject = "";
                            break;
                    }//end switch

                    //return value
                    return (_strEMailDeclineSubject);
                }
                else
                {
                    return (_strEMailDeclineSubject);
                }
            }
        }//end : SMTPHost

        //declare public properties
        static public string RunMode
        {
            get
            {
                if (_strRunMode == "")
                {
                    if (string.IsNullOrEmpty(Settings.Default.RunMode) == true)
                    {
                        return ("");
                    }
                    else
                    {
                        return ((string)Settings.Default.RunMode);
                    }
                }
                else
                {
                    return (_strRunMode);
                }
            }
        }
        static public string ClaimsDocsDBConnectionString
        {
            get
            {
                if (_strClaimsDocsDBConnectionString == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strClaimsDocsDBConnectionString = (string)Settings.Default.DEVMACHINEClaimsDocsDBConnectionString;
                            break;

                        case "DEVELOPMENT":
                            _strClaimsDocsDBConnectionString = (string)Settings.Default.DEVClaimsDocsDBConnectionString;
                            break;

                        case "ITQA":
                            _strClaimsDocsDBConnectionString = (string)Settings.Default.ITQAClaimsDocsDBConnectionString;
                            break;

                        case "UAT":
                            _strClaimsDocsDBConnectionString = (string)Settings.Default.UATClaimsDocsDBConnectionString;
                            break;

                        case "PRODUCTION":
                            _strClaimsDocsDBConnectionString = (string)Settings.Default.PRODClaimsDocsDBConnectionString;
                            break;

                        default:
                            _strClaimsDocsDBConnectionString = "";
                            break;
                    }//end switch

                    //return value
                    return (_strClaimsDocsDBConnectionString);
                }
                else
                {
                    return (_strClaimsDocsDBConnectionString);
                }
            }
        }//end : ClaimsDocsDBConnectionString
        static public string ConnectionString
        {
            get
            {
                if (_strConnectionString == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strConnectionString = (string)Settings.Default.DEVMACHINECorrespondenceConnectionString;
                            break;

                        case "DEVELOPMENT":
                            _strConnectionString = (string)Settings.Default.DEVCorrespondenceConnectionString;
                            break;

                        case "ITQA":
                            _strConnectionString = (string)Settings.Default.ITQACorrespondenceConnectionString;
                            break;

                        case "UAT":
                            _strConnectionString = (string)Settings.Default.UATCorrespondenceConnectionString;
                            break;

                        case "PRODUCTION":
                            _strConnectionString = (string)Settings.Default.PRODCorrespondenceConnectionString;
                            break;

                        default:
                            _strConnectionString = "";
                            break;
                    }//end switch

                    //return value
                    return (_strConnectionString);
                }
                else
                {
                    return (_strConnectionString);
                }
            }
        }//end : ConnectionString
        static public string ClaimsDBConnectionString
        {
            get
            {
                if (_strClaimsDBConnectionString == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strClaimsDBConnectionString = (string)Settings.Default.DEVMACHINEClaimsDBConnectionString;
                            break;

                        case "DEVELOPMENT":
                            _strClaimsDBConnectionString = (string)Settings.Default.DEVClaimsDBConnectionString;
                            break;

                        case "ITQA":
                            _strClaimsDBConnectionString = (string)Settings.Default.ITQAClaimsDBConnectionString;
                            break;

                        case "UAT":
                            _strClaimsDBConnectionString = (string)Settings.Default.UATClaimsDBConnectionString;
                            break;

                        case "PRODUCTION":
                            _strClaimsDBConnectionString = (string)Settings.Default.PRODClaimsDBConnectionString;
                            break;

                        default:
                            _strClaimsDBConnectionString = "";
                            break;
                    }//end switch

                    //return value
                    return (_strClaimsDBConnectionString);
                }
                else
                {
                    return (_strClaimsDBConnectionString);
                }
            }
        }//end : ClaimsDBConnectionString
        static public string AMSDBConnectionString
        {
            get
            {
                if (_strAMSDBConnectionString == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strAMSDBConnectionString = (string)Settings.Default.DEVMACHINEAMSDBConnectionString;
                            break;

                        case "DEVELOPMENT":
                            _strAMSDBConnectionString = (string)Settings.Default.DEVAMSDBConnectionString;
                            break;

                        case "ITQA":
                            _strAMSDBConnectionString = (string)Settings.Default.ITQAAMSDBConnectionString;
                            break;

                        case "UAT":
                            _strAMSDBConnectionString = (string)Settings.Default.UATAMSDBConnectionString;
                            break;

                        case "PRODUCTION":
                            _strAMSDBConnectionString = (string)Settings.Default.PRODAMSDBConnectionString;
                            break;

                        default:
                            _strAMSDBConnectionString = "";
                            break;
                    }//end switch

                    //return value
                    return (_strAMSDBConnectionString);
                }
                else
                {
                    return (_strAMSDBConnectionString);
                }
            }
        }//end : AMSDBConnectionString
        static public string CorrespondenceDBString
        {
            get
            {
                if (_strCorrespondenceDBString == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {

                        case "DEVMACHINE":
                            _strCorrespondenceDBString = (string)Settings.Default.DEVMACHINECorrespondenceConnectionString;
                            break;

                        case "DEVELOPMENT":
                            _strCorrespondenceDBString = (string)Settings.Default.DEVCorrespondenceConnectionString;
                            break;

                        case "ITQA":
                            _strCorrespondenceDBString = (string)Settings.Default.ITQACorrespondenceConnectionString;
                            break;

                        case "UAT":
                            _strCorrespondenceDBString = (string)Settings.Default.UATCorrespondenceConnectionString;
                            break;

                        case "PRODUCTION":
                            _strCorrespondenceDBString = (string)Settings.Default.PRODCorrespondenceConnectionString;
                            break;

                        default:
                            _strCorrespondenceDBString = "";
                            break;
                    }//end switch

                    //return value
                    return (_strCorrespondenceDBString);
                }
                else
                {
                    return (_strCorrespondenceDBString);
                }
            }
        }//end : CorrespondenceDBString
        static public string GenesisDBString
        {
            get
            {
                if (_strGenesisDBConnectionString == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strGenesisDBConnectionString = (string)Settings.Default.DEVMACHINEGenesisDBConnectionString;
                            break;


                        case "DEVELOPMENT":
                            _strGenesisDBConnectionString = (string)Settings.Default.DEVGenesisDBConnectionString;
                            break;

                        case "ITQA":
                            _strGenesisDBConnectionString = (string)Settings.Default.ITQAGenesisDBConnecionString;
                            break;

                        case "UAT":
                            _strGenesisDBConnectionString = (string)Settings.Default.UATGenesisDBConnecionString;
                            break;

                        case "PRODUCTION":
                            _strGenesisDBConnectionString = (string)Settings.Default.PRODGenesisDBConnecionString;
                            break;

                        default:
                            _strGenesisDBConnectionString = "";
                            break;
                    }//end switch

                    //return value
                    return (_strGenesisDBConnectionString);
                }
                else
                {
                    return (_strGenesisDBConnectionString);
                }
            }
        }//end : GenesisDBString
        static public string XMLOutputLocation
        {
            get
            {
                if (_strXMLOutputLocation == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strXMLOutputLocation = (string)Settings.Default.DEVMACHINEXMLOutputLocation;
                            break;

                        case "DEVELOPMENT":
                            _strXMLOutputLocation = (string)Settings.Default.DEVXMLOutputLocation;
                            break;

                        case "ITQA":
                            _strXMLOutputLocation = (string)Settings.Default.ITQAXMLOutputLocation;
                            break;

                        case "UAT":
                            _strXMLOutputLocation = (string)Settings.Default.UATXMLOutputLocation;
                            break;

                        case "PRODUCTION":
                            _strXMLOutputLocation = (string)Settings.Default.PRODXMLOutputLocation;
                            break;

                        default:
                            _strXMLOutputLocation = "";
                            break;
                    }//end switch

                    //return value
                    return (_strXMLOutputLocation);
                }
                else
                {
                    return (_strXMLOutputLocation);
                }
            }
        }//end : XMLOutputLocation
        static public string XMLUNCOutputLocation
        {
            get
            {
                if (_strXMLUNCOutputLocation == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strXMLUNCOutputLocation = (string)Settings.Default.DEVMACHINEXMLUNCOutputLocation;
                            break;

                        case "DEVELOPMENT":
                            _strXMLUNCOutputLocation = (string)Settings.Default.DEVXMLUNCOutputLocation;
                            break;

                        case "ITQA":
                            _strXMLUNCOutputLocation = (string)Settings.Default.ITQAXMLUNCOutputLocation;
                            break;

                        case "UAT":
                            _strXMLUNCOutputLocation = (string)Settings.Default.UATXMLUNCOutputLocation;
                            break;

                        case "PRODUCTION":
                            _strXMLUNCOutputLocation = (string)Settings.Default.PRODXMLUNCOutputLocation;
                            break;

                        default:
                            _strXMLUNCOutputLocation = "";
                            break;
                    }//end switch

                    //return value
                    return (_strXMLUNCOutputLocation);
                }
                else
                {
                    return (_strXMLUNCOutputLocation);
                }
            }
        }//end : XMLUNCOutputLocation
        static public string ClaimsDocsBizLogicLogFile
        {
            get
            {
                if (_strClaimsDocsBizLogicLogFile == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strClaimsDocsBizLogicLogFile = (string)Settings.Default.DEVMACHINEClaimsDocsBizLogicLogFile;
                            break;

                        case "DEVELOPMENT":
                            _strClaimsDocsBizLogicLogFile = (string)Settings.Default.DEVClaimsDocsBizLogicLogFile;
                            break;

                        case "ITQA":
                            _strClaimsDocsBizLogicLogFile = (string)Settings.Default.ITQAClaimsDocsBizLogicLogFile;
                            break;

                        case "UAT":
                            _strClaimsDocsBizLogicLogFile = (string)Settings.Default.UATClaimsDocsBizLogicLogFile;
                            break;

                        case "PRODUCTION":
                            _strClaimsDocsBizLogicLogFile = (string)Settings.Default.PRODClaimsDocsBizLogicLogFile;
                            break;

                        default:
                            _strClaimsDocsBizLogicLogFile = "";
                            break;
                    }//end switch

                    //return value
                    return (_strClaimsDocsBizLogicLogFile);
                }
                else
                {
                    return (_strClaimsDocsBizLogicLogFile);
                }
            }
        }//end : ClaimsDocsBizLogicLogFile

        //declare DocuMaker Member Properties
        static public string DocumakerReplyQueue
        {
            get
            {
                if (_strDocumakerReplyQueue == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strDocumakerReplyQueue = (string)Settings.Default.DEVDocumakerReplyQueue;
                            break;

                        case "DEVELOPMENT":
                            _strDocumakerReplyQueue = (string)Settings.Default.DEVDocumakerReplyQueue;
                            break;

                        case "ITQA":
                            _strDocumakerReplyQueue = (string)Settings.Default.ITQADocumakerReplyQueue;
                            break;

                        case "UAT":
                            _strDocumakerReplyQueue = (string)Settings.Default.UATDocumakerReplyQueue;
                            break;

                        case "PRODUCTION":
                            _strDocumakerReplyQueue = (string)Settings.Default.PRODDocumakerReplyQueue;
                            break;

                        default:
                            _strDocumakerReplyQueue = "";
                            break;
                    }//end switch

                    //return value
                    return (_strDocumakerReplyQueue);
                }
                else
                {
                    return (_strDocumakerReplyQueue);
                }
            }
        }//end : DocumakerReplyQueue
        static public string DocumakerRequestQueue
        {
            get
            {
                if (_strDocumakerReplyQueue == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strDocumakerRequestQueue = (string)Settings.Default.DEVDocumakerRequestQueue;
                            break;

                        case "DEVELOPMENT":
                            _strDocumakerRequestQueue = (string)Settings.Default.DEVDocumakerRequestQueue;
                            break;

                        case "ITQA":
                            _strDocumakerRequestQueue = (string)Settings.Default.ITQADocumakerRequestQueue;
                            break;

                        case "UAT":
                            _strDocumakerRequestQueue = (string)Settings.Default.UATDocumakerRequestQueue;
                            break;

                        case "PRODUCTION":
                            _strDocumakerRequestQueue = (string)Settings.Default.PRODDocumakerRequestQueue;
                            break;

                        default:
                            _strDocumakerRequestQueue = "";
                            break;
                    }//end switch

                    //return value
                    return (_strDocumakerRequestQueue);
                }
                else
                {
                    return (_strDocumakerRequestQueue);
                }
            }
        }//end : DocumakerRequestQueue
        static public string DocumakerEnvironment
        {
            get
            {
                if (_strDocumakerEnvironment == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strDocumakerEnvironment = (string)Settings.Default.DEVDocumakerEnvironment;
                            break;

                        case "DEVELOPMENT":
                            _strDocumakerEnvironment = (string)Settings.Default.DEVDocumakerEnvironment;
                            break;

                        case "ITQA":
                            _strDocumakerEnvironment = (string)Settings.Default.ITQADocumakerEnvironment;
                            break;

                        case "UAT":
                            _strDocumakerEnvironment = (string)Settings.Default.UATDocumakerEnvironment;
                            break;

                        case "PRODUCTION":
                            _strDocumakerEnvironment = (string)Settings.Default.PRODDocumakerEnvironment;
                            break;

                        default:
                            _strDocumakerEnvironment = "";
                            break;
                    }//end switch

                    //return value
                    return (_strDocumakerEnvironment);
                }
                else
                {
                    return (_strDocumakerEnvironment);
                }
            }
        }//end : DocumakerEnvironment
        static public string DocumakerUNCRoot
        {
            get
            {
                if (_strDocumakerUNCRoot == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strDocumakerUNCRoot = (string)Settings.Default.DEVDocumakerUNCRoot;
                            break;

                        case "DEVELOPMENT":
                            _strDocumakerUNCRoot = (string)Settings.Default.DEVDocumakerUNCRoot;
                            break;

                        case "ITQA":
                            _strDocumakerUNCRoot = (string)Settings.Default.ITQADocumakerUNCRoot;
                            break;

                        case "UAT":
                            _strDocumakerUNCRoot = (string)Settings.Default.UATDocumakerUNCRoot;
                            break;

                        case "PRODUCTION":
                            _strDocumakerUNCRoot = (string)Settings.Default.PRODDocumakerUNCRoot;
                            break;

                        default:
                            _strDocumakerUNCRoot = "";
                            break;
                    }//end switch

                    //return value
                    return (_strDocumakerUNCRoot);
                }
                else
                {
                    return (_strDocumakerUNCRoot);
                }
            }
        }//end : DocumakerUNCRoot
        static public TimeSpan DocuMakerReceiveTimeout
        {
            get
            {
                if (_tspDocuMakerReceiveTimeout == null)
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _tspDocuMakerReceiveTimeout = (TimeSpan)Settings.Default.DocuMakerReceiveTimeout;
                            break;

                        case "DEVELOPMENT":
                            _tspDocuMakerReceiveTimeout = (TimeSpan)Settings.Default.DocuMakerReceiveTimeout;
                            break;

                        case "ITQA":
                            _tspDocuMakerReceiveTimeout = (TimeSpan)Settings.Default.DocuMakerReceiveTimeout;
                            break;

                        case "UAT":
                            _tspDocuMakerReceiveTimeout = (TimeSpan)Settings.Default.DocuMakerReceiveTimeout;
                            break;

                        case "PRODUCTION":
                            _tspDocuMakerReceiveTimeout = (TimeSpan)Settings.Default.DocuMakerReceiveTimeout;
                            break;

                        default:
                            break;
                    }//end switch

                    //return value
                    return (_tspDocuMakerReceiveTimeout);
                }
                else
                {
                    return (_tspDocuMakerReceiveTimeout);
                }
            }
        }//end : DocuMakerReceiveTimeout
        static public int DocuMakerPeekPauseInSeconds
        {
            get
            {
                if (_intDocuMakerPeekPauseInSeconds == -1)
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _intDocuMakerPeekPauseInSeconds = (int)Settings.Default.DocuMakerPeekPauseInSeconds;
                            break;

                        case "DEVELOPMENT":
                            _intDocuMakerPeekPauseInSeconds = (int)Settings.Default.DocuMakerPeekPauseInSeconds;
                            break;

                        case "ITQA":
                            _intDocuMakerPeekPauseInSeconds = (int)Settings.Default.DocuMakerPeekPauseInSeconds;
                            break;

                        case "UAT":
                            _intDocuMakerPeekPauseInSeconds = (int)Settings.Default.DocuMakerPeekPauseInSeconds;
                            break;

                        case "PRODUCTION":
                            _intDocuMakerPeekPauseInSeconds = (int)Settings.Default.DocuMakerPeekPauseInSeconds;
                            break;

                        default:
                            break;
                    }//end switch

                    //return value
                    return (_intDocuMakerPeekPauseInSeconds);
                }
                else
                {
                    return (_intDocuMakerPeekPauseInSeconds);
                }
            }
        }//end : DocuMakerPeekPauseInSeconds
        static public string DocumentGenerator
        {
            get
            {
                if (_strDocumentGenerator == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strDocumentGenerator = (string)Settings.Default.DocumentGenerator;
                            break;

                        case "DEVELOPMENT":
                            _strDocumentGenerator = (string)Settings.Default.DocumentGenerator;
                            break;

                        case "ITQA":
                            _strDocumentGenerator = (string)Settings.Default.DocumentGenerator;
                            break;

                        case "UAT":
                            _strDocumentGenerator = (string)Settings.Default.DocumentGenerator;
                            break;

                        case "PRODUCTION":
                            _strDocumentGenerator = (string)Settings.Default.DocumentGenerator;
                            break;

                        default:
                            _strDocumentGenerator = "";
                            break;
                    }//end switch

                    //return value
                    return (_strDocumentGenerator);
                }
                else
                {
                    return (_strDocumentGenerator);
                }
            }
        }//end : DocumentGenerator
        static public int DocuMakerNumberOfTries
        {
            get
            {
                if (_intDocuMakerNumberOfTries == -1)
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _intDocuMakerNumberOfTries = (int)Settings.Default.DocuMakerNumberOfTries;
                            break;

                        case "DEVELOPMENT":
                            _intDocuMakerNumberOfTries = (int)Settings.Default.DocuMakerNumberOfTries;
                            break;

                        case "ITQA":
                            _intDocuMakerNumberOfTries = (int)Settings.Default.DocuMakerNumberOfTries;
                            break;

                        case "UAT":
                            _intDocuMakerNumberOfTries = (int)Settings.Default.DocuMakerNumberOfTries;
                            break;

                        case "PRODUCTION":
                            _intDocuMakerNumberOfTries = (int)Settings.Default.DocuMakerNumberOfTries;
                            break;

                        default:
                            break;
                    }//end switch

                    //return value
                    return (_intDocuMakerNumberOfTries);
                }
                else
                {
                    return (_intDocuMakerNumberOfTries);
                }
            }
        }//end : DocuMakerNumberOfTries

        static public int NumberOfResponseTries
        {
            get
            {
                if (_intNumberOfResponseTries == -1)
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _intNumberOfResponseTries = (int)Settings.Default.NumberOfResponseTries;
                            break;

                        case "DEVELOPMENT":
                            _intNumberOfResponseTries = (int)Settings.Default.NumberOfResponseTries;
                            break;

                        case "ITQA":
                            _intNumberOfResponseTries = (int)Settings.Default.NumberOfResponseTries;
                            break;

                        case "UAT":
                            _intNumberOfResponseTries = (int)Settings.Default.NumberOfResponseTries;
                            break;

                        case "PRODUCTION":
                            _intNumberOfResponseTries = (int)Settings.Default.NumberOfResponseTries;
                            break;

                        default:
                            break;
                    }//end switch

                    //return value
                    return (_intNumberOfResponseTries);
                }
                else
                {
                    return (_intNumberOfResponseTries);
                }
            }
        }//end : DocuMakerNumberOfTries
        
        static public int MessageQueueLoopTries
        {
            get
            {
                if (_intMessageQueueLoopTries == -1)
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _intMessageQueueLoopTries = (int)Settings.Default.MessageQueueLoopTries;
                            break;

                        case "DEVELOPMENT":
                            _intMessageQueueLoopTries = (int)Settings.Default.MessageQueueLoopTries;
                            break;

                        case "ITQA":
                            _intMessageQueueLoopTries = (int)Settings.Default.MessageQueueLoopTries;
                            break;

                        case "UAT":
                            _intMessageQueueLoopTries = (int)Settings.Default.MessageQueueLoopTries;
                            break;

                        case "PRODUCTION":
                            _intMessageQueueLoopTries = (int)Settings.Default.MessageQueueLoopTries;
                            break;

                        default:
                            break;
                    }//end switch

                    //return value
                    return (_intMessageQueueLoopTries);
                }
                else
                {
                    return (_intMessageQueueLoopTries);
                }
            }
        }//end : DocuMakerNumberOfTries
        static public string ClaimsDocsNameSpace
        {
            get
            {
                if (_strClaimsDocsNameSpace == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strClaimsDocsNameSpace = (string)Settings.Default.ClaimsDocsNameSpace;
                            break;

                        case "DEVELOPMENT":
                            _strClaimsDocsNameSpace = (string)Settings.Default.ClaimsDocsNameSpace;
                            break;

                        case "ITQA":
                            _strClaimsDocsNameSpace = (string)Settings.Default.ClaimsDocsNameSpace;
                            break;

                        case "UAT":
                            _strClaimsDocsNameSpace = (string)Settings.Default.ClaimsDocsNameSpace;
                            break;

                        case "PRODUCTION":
                            _strClaimsDocsNameSpace = (string)Settings.Default.ClaimsDocsNameSpace;
                            break;

                        default:
                            _strClaimsDocsNameSpace = "";
                            break;
                    }//end switch

                    //return value
                    return (_strClaimsDocsNameSpace);
                }
                else
                {
                    return (_strClaimsDocsNameSpace);
                }
            }
        }//end : ClaimsDocsNameSpace

        //declare ImageRight Member Properties
        static public string ImageRightWSURL
        {
            get
            {
                if (_strImageRightWSURL == "")
                {


                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strImageRightWSURL = (string)Settings.Default.DEVMACHINEImageRightWSURL;
                            break;

                        case "DEVELOPMENT":
                            _strImageRightWSURL = (string)Settings.Default.DEVImageRightWSURL;
                            break;

                        case "ITQA":
                            _strImageRightWSURL = (string)Settings.Default.ITQAImageRightWSURL;
                            break;

                        case "UAT":
                            _strImageRightWSURL = (string)Settings.Default.UATImageRightWSURL;
                            break;

                        case "PRODUCTION":
                            _strImageRightWSURL = (string)Settings.Default.PRODImageRightWSURL;
                            break;

                        default:
                            _strImageRightWSURL = "";
                            break;
                    }//end switch

                    //return value
                    return (_strImageRightWSURL);
                }
                else
                {
                    return (_strImageRightWSURL);
                }
            }
        }//end : ImageRightWSURL
        static public string ImageRightLogin
        {
            get
            {
                if (_strImageRightLogin == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strImageRightLogin = (string)Settings.Default.DEVMACHINEImageRightLogin;
                            break;

                        case "DEVELOPMENT":
                            _strImageRightLogin = (string)Settings.Default.DEVImageRightLogin;
                            break;

                        case "ITQA":
                            _strImageRightLogin = (string)Settings.Default.ITQAImageRightLogin;
                            break;

                        case "UAT":
                            _strImageRightLogin = (string)Settings.Default.UATImageRightLogin;
                            break;

                        case "PRODUCTION":
                            _strImageRightLogin = (string)Settings.Default.PRODImageRightLogin;
                            break;

                        default:
                            _strImageRightLogin = "";
                            break;
                    }//end switch

                    //return value
                    return (_strImageRightLogin);
                }
                else
                {
                    return (_strImageRightLogin);
                }
            }
        }//end : ImageRightLogin
        static public string ImageRightPassword
        {
            get
            {
                if (_strImageRightPassword == "")
                {

                    switch (Settings.Default.RunMode.ToUpper())
                    {
                        case "DEVMACHINE":
                            _strImageRightPassword = (string)Settings.Default.DEVMACHINEImageRightPassword;
                            break;

                        case "DEVELOPMENT":
                            _strImageRightPassword = (string)Settings.Default.DEVImageRightPassword;
                            break;

                        case "ITQA":
                            _strImageRightPassword = (string)Settings.Default.ITQAImageRightPassword;
                            break;

                        case "UAT":
                            _strImageRightPassword = (string)Settings.Default.UATImageRightPassword;
                            break;

                        case "PRODUCTION":
                            _strImageRightPassword = (string)Settings.Default.PRODImageRightPassword;
                            break;

                        default:
                            _strImageRightPassword = "";
                            break;
                    }//end switch

                    //return value
                    return (_strImageRightPassword);
                }
                else
                {
                    return (_strImageRightPassword);
                }
            }
        }//end : ImageRightPassword
        static public string ImageRightClaimsDrawer
        {
            get
            {
                if (_strImageRightClaimsDrawer == "")
                {
                    _strImageRightClaimsDrawer = (string)Settings.Default.ImageRightClaimsDrawer;

                    return (_strImageRightClaimsDrawer);
                }
                else
                {
                    return (_strImageRightClaimsDrawer);
                }
            }
        }//end : ImageRightClaimsDrawer
        static public string ImageRightReason
        {
            get
            {
                if (_strImageRightReason == "")
                {
                    _strImageRightReason = (string)Settings.Default.ImageRightReason;

                    return (_strImageRightReason);
                }
                else
                {
                    return (_strImageRightReason);
                }
            }
        }//end : ImageRightReason
        static public string ImageRightDescription
        {
            get
            {
                if (_strImageRightDescription == "")
                {
                    _strImageRightDescription = (string)Settings.Default.ImageRightDescription;

                    return (_strImageRightDescription);
                }
                else
                {
                    return (_strImageRightDescription);
                }
            }
        }//end : ImageRightDescription
        static public int ImageRightFlowID
        {
            get
            {
                if (_intImageRightFlowID == 0)
                {
                    _intImageRightFlowID = (int)Settings.Default.ImageRightFlowID;

                    return (_intImageRightFlowID);
                }
                else
                {
                    return (_intImageRightFlowID);
                }
            }
        }//end : ImageRightFlowID
        static public int ImageRightStepID
        {
            get
            {
                if (_intImageRightStepID == 0)
                {
                    _intImageRightStepID = (int)Settings.Default.ImageRightStepID;

                    return (_intImageRightStepID);
                }
                else
                {
                    return (_intImageRightStepID);
                }
            }
        }//end : ImageRightStepID
        static public int ImageRightPriority
        {
            get
            {
                if (_intImageRightPriority == 0)
                {
                    _intImageRightPriority = (int)Settings.Default.ImageRightPriority;

                    return (_intImageRightPriority);
                }
                else
                {
                    return (_intImageRightPriority);
                }
            }
        }//end : ImageRightPriority

        //declare private class variables
        private List<ClaimsDocsLog> _listClaimsDocsLog = new List<ClaimsDocsLog>();

        //define method : ClaimsDocsLogCreate
        public int ClaimsDocsLogCreate(ClaimsDocsLog objClaimsDocsLog, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            int intResult = 0;
            StringBuilder sbrMessage = new StringBuilder();

            //start try
            try
            {
                //build message
                sbrMessage.Append("------------------------------------------------");
                sbrMessage.Append("Exception : " + Environment.NewLine);
                sbrMessage.Append("------------------------------------------------");
                sbrMessage.Append(objClaimsDocsLog.ExceptionIs);
                sbrMessage.Append("------------------------------------------------");
                sbrMessage.Append("*************************************************");
                sbrMessage.Append("------------------------------------------------");
                sbrMessage.Append("Stack Trace : " + Environment.NewLine);
                sbrMessage.Append("------------------------------------------------");
                sbrMessage.Append(objClaimsDocsLog.StackTraceIs);
                sbrMessage.Append("------------------------------------------------");

                //log exception to file
                LogExceptionToFile(objClaimsDocsLog.MessageIs, null, sbrMessage.ToString(), false);

                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "spClaimsDocsLogCreate";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (objClaimsDocsLog.ClaimsDocsLogID) and set its properties
                SqlParameter paramClaimsDocsLogID = new SqlParameter();
                paramClaimsDocsLogID.ParameterName = "@intClaimsDocsLogID";
                paramClaimsDocsLogID.SqlDbType = SqlDbType.Int;
                paramClaimsDocsLogID.Direction = ParameterDirection.Input;
                paramClaimsDocsLogID.Value = objClaimsDocsLog.ClaimsDocsLogID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimsDocsLogID);

                //Add input parameter for (objClaimsDocsLog.LogSourceTypeID) and set its properties
                SqlParameter paramLogSourceTypeID = new SqlParameter();
                paramLogSourceTypeID.ParameterName = "@intLogSourceTypeID";
                paramLogSourceTypeID.SqlDbType = SqlDbType.Int;
                paramLogSourceTypeID.Direction = ParameterDirection.Input;
                paramLogSourceTypeID.Value = objClaimsDocsLog.LogSourceTypeID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramLogSourceTypeID);

                //Add input parameter for (objClaimsDocsLog.LogTypeID) and set its properties
                SqlParameter paramLogTypeID = new SqlParameter();
                paramLogTypeID.ParameterName = "@intLogTypeID";
                paramLogTypeID.SqlDbType = SqlDbType.Int;
                paramLogTypeID.Direction = ParameterDirection.Input;
                paramLogTypeID.Value = objClaimsDocsLog.LogTypeID;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramLogTypeID);

                //Add input parameter for (objClaimsDocsLog.MessageIs) and set its properties
                SqlParameter paramMessageIs = new SqlParameter();
                paramMessageIs.ParameterName = "@txtMessageIs";
                paramMessageIs.SqlDbType = SqlDbType.Text;
                paramMessageIs.Direction = ParameterDirection.Input;
                paramMessageIs.Value = objClaimsDocsLog.MessageIs;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramMessageIs);

                //Add input parameter for (objClaimsDocsLog.ExceptionIs) and set its properties
                SqlParameter paramExceptionIs = new SqlParameter();
                paramExceptionIs.ParameterName = "@txtExceptionIs";
                paramExceptionIs.SqlDbType = SqlDbType.Text;
                paramExceptionIs.Direction = ParameterDirection.Input;
                paramExceptionIs.Value = objClaimsDocsLog.ExceptionIs;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramExceptionIs);

                //Add input parameter for (objClaimsDocsLog.StackTraceIs) and set its properties
                SqlParameter paramStackTraceIs = new SqlParameter();
                paramStackTraceIs.ParameterName = "@txtStackTraceIs";
                paramStackTraceIs.SqlDbType = SqlDbType.Text;
                paramStackTraceIs.Direction = ParameterDirection.Input;
                paramStackTraceIs.Value = objClaimsDocsLog.StackTraceIs;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramStackTraceIs);

                //Add input parameter for (objClaimsDocsLog.IUDateTime) and set its properties
                SqlParameter paramIUDateTime = new SqlParameter();
                paramIUDateTime.ParameterName = "@datIUDateTime";
                paramIUDateTime.SqlDbType = SqlDbType.DateTime;
                paramIUDateTime.Direction = ParameterDirection.Input;
                paramIUDateTime.Value = objClaimsDocsLog.IUDateTime;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramIUDateTime);

                //Add input parameter for (objClaimsDocsLog.Result) and set its properties
                SqlParameter paramResult = new SqlParameter();
                paramResult.ParameterName = "@intResult";
                paramResult.SqlDbType = SqlDbType.Int;
                paramResult.Direction = ParameterDirection.InputOutput;
                paramResult.Value = intResult;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramResult);

                //open connection
                cnnConnection.Open();
                //execute command
                cmdCommand.ExecuteNonQuery();
                //get result
                intResult = int.Parse(cmdCommand.Parameters["@intResult"].Value.ToString());

            }//end try
            catch (Exception ex)
            {
                ////handle error
                intResult = 0;
            }
            finally
            {
                //cleanup connection
                if (cnnConnection.State == ConnectionState.Open)
                {
                    cnnConnection.Close();
                }
                //cleanup command
                if (cmdCommand != null)
                {
                    cmdCommand = null;
                }
            }
            //return result
            return (intResult);
        }//end method : ClaimsDocsLogCreate

        //define method : LogExceptionToFile
        static public bool LogExceptionToFile(string strSourceMethod, Exception ex, string strMessage, bool blnSendEMail)
        {
            //provide error handling here
            //declare variables
            string strLogFilePath = CDSupport.ClaimsDocsBizLogicLogFile;
            bool blnResult = true;
            StreamWriter stmWriter;
            StringBuilder sbrLine = new StringBuilder();
            string strOutputFileName = strLogFilePath;
            string strHeaderRow = "Source Method,Message, Exception, Stack Trace";

            try
            {
                //check for exception
                if (ex != null)
                {
                    //build output line
                    sbrLine.Append(DateTime.Now.ToString() + Environment.NewLine + strSourceMethod + "," + strMessage + "," + ex.Message + "," + Environment.NewLine + ex.StackTrace);
                }
                else
                {
                    //build output line
                    sbrLine.Append(DateTime.Now.ToString() + Environment.NewLine + strSourceMethod + "," + strMessage + "," + ",");
                }

                //Check for existence of log file
                FileInfo filLog = new FileInfo(@strOutputFileName);
                //open stream writer
                stmWriter = new StreamWriter(strOutputFileName, true);
                //check for header row
                if (filLog.Exists == false)
                {
                    //insert header row
                    stmWriter.WriteLine(strHeaderRow.ToString());
                }

                //append header line to document
                stmWriter.WriteLine(sbrLine.ToString());
                stmWriter.Close();

                ////if required, send e-mail notification
                //if (blnSendEMail == true)
                //{
                //    SendEMailNotification(0, 0, strSourceMethod, strMessage, ex);
                //}
            }
            catch (Exception except)
            {
                //do nothing
            }
            finally
            {
                //cleanup

                sbrLine = null;
            }
            //return result
            return (blnResult);
        }//end LogExceptionToFile

        //define : ClaimsDocsLogSearch
        public List<ClaimsDocsLog> ClaimsDocsLogSearch(string strSQLString, string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            //start try
            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = strSQLString;
                cmdCommand.CommandType = CommandType.Text;

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                //check for results
                if (rdrReader.HasRows == true)
                {
                    //fill Claims Docs Log object
                    while (rdrReader.Read())
                    {
                        //create new Claims Docs Log Object
                        ClaimsDocsLog objClaimsDocsLog = new ClaimsDocsLog();
                        //fill objClaimsDocsLog object
                        objClaimsDocsLog.ClaimsDocsLogID = int.Parse(rdrReader["ClaimsDocsLogID"].ToString());
                        objClaimsDocsLog.LogSourceTypeID = int.Parse(rdrReader["LogSourceTypeID"].ToString());
                        objClaimsDocsLog.LogTypeID = int.Parse(rdrReader["LogTypeID"].ToString());
                        objClaimsDocsLog.SourceName = rdrReader["LogSourceName"].ToString();
                        objClaimsDocsLog.LogTypeName = rdrReader["LogTypeName"].ToString();
                        objClaimsDocsLog.MessageIs = rdrReader["MessageIs"].ToString();
                        objClaimsDocsLog.ExceptionIs = rdrReader["ExceptionIs"].ToString();
                        objClaimsDocsLog.StackTraceIs = rdrReader["StackTraceIs"].ToString();
                        objClaimsDocsLog.IUDateTime = DateTime.Parse(rdrReader["IUDateTime"].ToString());

                        //add user to user list
                        this._listClaimsDocsLog.Add(objClaimsDocsLog);
                    }
                }

            }//end try
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : ClaimsDocsLogSearch() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
            }
            finally
            {
                //cleanup connection
                if (cnnConnection.State == ConnectionState.Open)
                {
                    cnnConnection.Close();
                }
                //cleanup command
                if (cmdCommand != null)
                {
                    cmdCommand = null;
                }
            }

            //return result
            return (this._listClaimsDocsLog);

        }//end : ClaimsDocsLogSearch

        //define : ValidateWCFServiceCall
        public ValidateResult ValidateWCFServiceCall()
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();

            try
            {
                //setup result
                objValidateResult.ValidCheck = true;
                objValidateResult.ValidationFocus = "WCF Service";
                objValidateResult.ValidationResultMessage = "Successfully called : ValidateWCFServiceCall()";
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }

            //return results
            return (objValidateResult);
        }//end : ValidateWCFServiceCall

        //define : ValidateMessageQueue
        public ValidateResult ValidateMessageQueue()
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();
            ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument ClaimsDocument = new ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument();
            DocumentRequestorDocumaker objDRC = new DocumentRequestorDocumaker(ClaimsDocument);
            string strResultIs = "";

            try
            {
                //get send request
                strResultIs = objDRC.SendRequestTest("This is a test message");

                if (strResultIs.ToUpper().Equals("SEND REQUEST FAILED..."))
                {
                    //setup result
                    objValidateResult.ValidCheck = false;
                    objValidateResult.ValidationFocus = "DocuMaker MSMQ : Request Queue";
                    objValidateResult.ValidationResultMessage = "Failed attempt to send message to message queue : ValidateMessageQueue()";
                }
                else
                {
                    //setup result
                    objValidateResult.ValidCheck = true;
                    objValidateResult.ValidationFocus = "DocuMaker MSMQ : Request Queue";
                    objValidateResult.ValidationResultMessage = "Successfully called : ValidateMessageQueue() w/GUID : " + strResultIs;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }

            //return results
            return (objValidateResult);
        }

        //define : ValidateXMLOutputLocation
        public ValidateResult ValidateXMLOutputLocation()
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();
            //declare variables
            FileInfo filInfo = null;
            string strFilePathAndName = "";
            DirectoryInfo dirInfo;

            try
            {
                //get file path and name
                strFilePathAndName = CDSupport.XMLOutputLocation;
                dirInfo = new DirectoryInfo(strFilePathAndName);

                //determine if the directory exists
                if (dirInfo.Exists == true)
                {
                    //setup result
                    objValidateResult.ValidCheck = true;
                    objValidateResult.ValidationFocus = "XMLOutputLocation";
                    objValidateResult.ValidationResultMessage = "The XML Output Location does exist : " + strFilePathAndName;
                }
                else
                {
                    //setup result
                    objValidateResult.ValidCheck = false;
                    objValidateResult.ValidationFocus = "XMLOutputLocation";
                    objValidateResult.ValidationResultMessage = "The XML Output Location does NOT exist : " + strFilePathAndName;
                }
            }
            catch (Exception ex)
            {
                //handle error
                objValidateResult.ValidCheck = false;
                objValidateResult.ValidationFocus = "XMLOutputLocation";
                objValidateResult.ValidationResultMessage = "The XML Output Location does NOT exist : " + strFilePathAndName + Environment.NewLine + Environment.NewLine + ex.Message;
            }
            finally
            {

            }
            //return results
            return (objValidateResult);
        }//end : ValidateXMLOutputLocation

        //define : ValidateClaimsDocsBizLogicLog
        public ValidateResult ValidateClaimsDocsBizLogicLog()
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();
            //declare variables
            FileInfo filInfo = null;
            string strFilePathAndName = "";
            DirectoryInfo dirInfo;

            try
            {
                //get file path and name
                strFilePathAndName = CDSupport.ClaimsDocsBizLogicLogFile;
                dirInfo = new DirectoryInfo(strFilePathAndName);

                //determine if the directory exists
                if (dirInfo.Exists == true)
                {
                    //setup result
                    objValidateResult.ValidCheck = true;
                    objValidateResult.ValidationFocus = "ClaimsDocsBizLogicLog";
                    objValidateResult.ValidationResultMessage = "The ValidateClaimsDocsBizLogicLog Output Location does exist : " + strFilePathAndName;
                }
                else
                {
                    //setup result
                    objValidateResult.ValidCheck = false;
                    objValidateResult.ValidationFocus = "ClaimsDocsBizLogicLog";
                    objValidateResult.ValidationResultMessage = "The ClaimsDocsBizLogicLog Output Location does NOT exist : " + strFilePathAndName;
                }
            }
            catch (Exception ex)
            {
                //handle error
                objValidateResult.ValidCheck = false;
                objValidateResult.ValidationFocus = "ClaimsDocsBizLogicLog";
                objValidateResult.ValidationResultMessage = "The ClaimsDocsBizLogicLog Output Location does NOT exist : " + strFilePathAndName + Environment.NewLine + Environment.NewLine + ex.Message;
            }
            finally
            {

            }
            //return results
            return (objValidateResult);
        }//end : ValidateClaimsDocsBizLogicLog

        //define : ValidateClaimsDBConnection
        public ValidateResult ValidateClaimsDBConnection()
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();
            //declare variables
            SqlConnection cnnConnection = null;
            string strConnectionString = "";
            try
            {
                //get connection string
                strConnectionString = CDSupport.ClaimsDBConnectionString;

                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //open connection
                cnnConnection.Open();

                //handle success
                objValidateResult.ValidCheck = true;
                objValidateResult.ValidationFocus = "Claims Database Connection";
                objValidateResult.ValidationResultMessage = "Successfully connected to : " + strConnectionString;

            }
            catch (Exception ex)
            {
                //handle error
                objValidateResult.ValidCheck = false;
                objValidateResult.ValidationFocus = "Claims Database Connection";
                objValidateResult.ValidationResultMessage = "Error connecting to : " + strConnectionString + Environment.NewLine + Environment.NewLine + ex.Message;
            }
            finally
            {
                //clean up
                if (cnnConnection != null)
                {
                    //cleanup connection
                    if (cnnConnection.State == ConnectionState.Open)
                    {
                        cnnConnection.Close();
                    }
                }
            }

            //return results
            return (objValidateResult);
        }//end : ValidateClaimsDBConnection

        //define : ValidateClaimsDocsDBConnection
        public ValidateResult ValidateClaimsDocsDBConnection()
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();
            //declare variables
            SqlConnection cnnConnection = null;
            string strConnectionString = "";

            try
            {
                //get connection string
                strConnectionString = CDSupport.ClaimsDocsDBConnectionString;

                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);

                //open connection
                cnnConnection.Open();

                //handle success
                objValidateResult.ValidCheck = true;
                objValidateResult.ValidationFocus = "ClaimsDocs Database";
                objValidateResult.ValidationResultMessage = "Successfully connected to : " + strConnectionString;

            }
            catch (Exception ex)
            {
                //handle error
                objValidateResult.ValidCheck = false;
                objValidateResult.ValidationFocus = "ClaimsDocs Database";
                objValidateResult.ValidationResultMessage = "Error connecting to : " + strConnectionString + Environment.NewLine + Environment.NewLine + ex.Message;
            }
            finally
            {
                //clean up
                if (cnnConnection != null)
                {
                    //cleanup connection
                    if (cnnConnection.State == ConnectionState.Open)
                    {
                        cnnConnection.Close();
                    }
                    cnnConnection = null;
                }
            }

            //return results
            return (objValidateResult);
        }//end : ValidateClaimsDocsDBConnection

        //define : ValidateCorrespondenceDBConnection
        public ValidateResult ValidateCorrespondenceDBConnection()
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();
            //declare variables
            SqlConnection cnnConnection = null;
            string strConnectionString = "";
            try
            {
                //get connection string
                strConnectionString = CDSupport.CorrespondenceDBString;

                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //open connection
                cnnConnection.Open();

                //handle success
                objValidateResult.ValidCheck = true;
                objValidateResult.ValidationFocus = "Correspondence Database";
                objValidateResult.ValidationResultMessage = "Successfully connected to : " + strConnectionString;

            }
            catch (Exception ex)
            {
                //handle error
                objValidateResult.ValidCheck = false;
                objValidateResult.ValidationFocus = "Correspondence Database";
                objValidateResult.ValidationResultMessage = "Error connecting to : " + strConnectionString + Environment.NewLine + Environment.NewLine + ex.Message;
            }
            finally
            {
                //clean up
                if (cnnConnection != null)
                {
                    //cleanup connection
                    if (cnnConnection.State == ConnectionState.Open)
                    {
                        cnnConnection.Close();
                    }
                    cnnConnection = null;
                }
            }

            //return results
            return (objValidateResult);
        }//end : ValidateCorrespondenceDBConnection

        //define : ValidateAMSDBConnection
        public ValidateResult ValidateAMSDBConnection()
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();
            //declare variables
            SqlConnection cnnConnection = null;
            string strConnectionString = "";
            try
            {
                //get connection string
                strConnectionString = CDSupport.AMSDBConnectionString;

                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //open connection
                cnnConnection.Open();

                //handle success
                objValidateResult.ValidCheck = true;
                objValidateResult.ValidationFocus = "AMS Database";
                objValidateResult.ValidationResultMessage = "Successfully connected to : " + strConnectionString;

            }
            catch (Exception ex)
            {
                //handle error
                objValidateResult.ValidCheck = false;
                objValidateResult.ValidationFocus = "AMS Database";
                objValidateResult.ValidationResultMessage = "Error connecting to : " + strConnectionString + Environment.NewLine + Environment.NewLine + ex.Message;
            }
            finally
            {
                //clean up
                if (cnnConnection != null)
                {
                    //cleanup connection
                    if (cnnConnection.State == ConnectionState.Open)
                    {
                        cnnConnection.Close();
                    }
                    cnnConnection = null;
                }
            }

            //return results
            return (objValidateResult);
        }//end : ValidateAMSDBConnection

        //define : ValidateGenesisDBConnection
        public ValidateResult ValidateGenesisDBConnection()
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();
            //declare variables
            SqlConnection cnnConnection = null;
            string strConnectionString = "";
            try
            {
                //get connection string
                strConnectionString = CDSupport.GenesisDBString;

                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //open connection
                cnnConnection.Open();

                //handle success
                objValidateResult.ValidCheck = true;
                objValidateResult.ValidationFocus = "Genesis Database";
                objValidateResult.ValidationResultMessage = "Successfully connected to : " + strConnectionString;

            }
            catch (Exception ex)
            {
                //handle error
                objValidateResult.ValidCheck = false;
                objValidateResult.ValidationFocus = "Genesis Database";
                objValidateResult.ValidationResultMessage = "Error connecting to : " + strConnectionString + Environment.NewLine + Environment.NewLine + ex.Message;
            }
            finally
            {
                //clean up
                if (cnnConnection != null)
                {
                    //cleanup connection
                    if (cnnConnection.State == ConnectionState.Open)
                    {
                        cnnConnection.Close();
                    }
                    cnnConnection = null;
                }
            }

            //return results
            return (objValidateResult);
        }//end : ValidateGenesisDBConnection

        //define : ValidateAMSPointToClaimsLinkedServer
        public ValidateResult ValidateAMSPointToClaimsLinkedServer(int intClaimUKey)
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();
            //declare variables
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            string strConnectionString = "";

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(CDSupport.AMSDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_GetAddressee";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (ClaimUKey) and set its properties
                SqlParameter paramClaimUKey = new SqlParameter();
                paramClaimUKey.ParameterName = "@claimukey";
                paramClaimUKey.SqlDbType = SqlDbType.Int;
                paramClaimUKey.Direction = ParameterDirection.Input;
                paramClaimUKey.Value = intClaimUKey; ;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramClaimUKey);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                if (rdrReader.HasRows == true)
                {
                    //handle success
                    objValidateResult.ValidCheck = true;
                    objValidateResult.ValidationFocus = "Validate AMS Point To Claims Linked Server: Has Rows";
                    objValidateResult.ValidationResultMessage = "Successfully connected to : " + CDSupport.AMSDBConnectionString;
                }
                else
                {
                    //handle success
                    objValidateResult.ValidCheck = true;
                    objValidateResult.ValidationFocus = "Validate AMS Point To Claims Linked Server: No Rows";
                    objValidateResult.ValidationResultMessage = "Successfully connected to : " + CDSupport.AMSDBConnectionString;
                }

            }
            catch (Exception ex)
            {
                //handle error
                objValidateResult.ValidCheck = false;
                objValidateResult.ValidationFocus = "Validate AMS Point To Claims Linked Server";
                objValidateResult.ValidationResultMessage = "Error connecting to : " + strConnectionString + Environment.NewLine + Environment.NewLine + ex.Message;
            }
            finally
            {
                //close reader
                if (rdrReader != null)
                {
                    if (!rdrReader.IsClosed)
                    {
                        rdrReader.Close();
                    }
                    rdrReader = null;
                }

                //clean up
                if (cnnConnection != null)
                {
                    //cleanup connection
                    if (cnnConnection.State == ConnectionState.Open)
                    {
                        cnnConnection.Close();
                    }
                    cnnConnection = null;
                }
            }

            //return results
            return (objValidateResult);
        }//end : ValidateAMSPointToClaimsLinkedServer

        //define : ValidateAMSPointToGenesisLinkedServer
        public ValidateResult ValidateAMSPointToGenesisLinkedServer(string strPolicyNumber)
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();
            //declare variables
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;
            string strConnectionString = "";

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(CDSupport.AMSDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_GetInsuredContactInfo";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (PolicyNo) and set its properties
                SqlParameter paramPolicyNo = new SqlParameter();
                paramPolicyNo.ParameterName = "@policyno";
                paramPolicyNo.SqlDbType = SqlDbType.VarChar;
                paramPolicyNo.Direction = ParameterDirection.Input;
                paramPolicyNo.Value = strPolicyNumber;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNo);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                if (rdrReader.HasRows == true)
                {
                    //handle success
                    objValidateResult.ValidCheck = true;
                    objValidateResult.ValidationFocus = "Validate AMS Point To Genesis Linked Server : Has Rows";
                    objValidateResult.ValidationResultMessage = "Successfully connected to : " + CDSupport.AMSDBConnectionString;
                }
                else
                {
                    //handle success
                    objValidateResult.ValidCheck = true;
                    objValidateResult.ValidationFocus = "Validate AMS Point To Genesis Linked Server : No Rows";
                    objValidateResult.ValidationResultMessage = "Successfully connected to : " + CDSupport.AMSDBConnectionString;
                }

            }
            catch (Exception ex)
            {
                //handle error
                objValidateResult.ValidCheck = false;
                objValidateResult.ValidationFocus = "Validate AMS Point To Genesis Linked Server";
                objValidateResult.ValidationResultMessage = "Error connecting to : " + strConnectionString + Environment.NewLine + Environment.NewLine + ex.Message;
            }
            finally
            {
                //close reader
                if (rdrReader != null)
                {
                    if (!rdrReader.IsClosed)
                    {
                        rdrReader.Close();
                    }
                    rdrReader = null;
                }

                //clean up
                if (cnnConnection != null)
                {
                    if (cnnConnection.State == ConnectionState.Open)
                    {
                        cnnConnection.Close();
                    }
                    cnnConnection = null;
                }
            }

            //return results
            return (objValidateResult);
        }//end : ValidateAMSPointToGenesisLinkedServer

        //define : ValidateClaimsPointToGenesisLinkedServer
        public ValidateResult ValidateClaimsPointToGenesisLinkedServer(int intContactUKey, int intVehicleFound)
        {
            //declare variables
            ValidateResult objValidateResult = new ValidateResult();
            //declare variables
            //declare variables
            SqlConnection cnnConnection = null;
            SqlCommand cmdCommand = new SqlCommand();
            SqlDataReader rdrReader = null;

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(CDSupport.ClaimsDBConnectionString);
                //Create the command and set its properties
                cmdCommand = new SqlCommand();
                cmdCommand.Connection = cnnConnection;
                cmdCommand.CommandText = "dbo.ClaimsDocs_Search4_Insured_Vehicle";
                cmdCommand.CommandType = CommandType.StoredProcedure;

                //add command parameters		
                //Add input parameter for (PolicyNo) and set its properties
                SqlParameter paramPolicyNo = new SqlParameter();
                paramPolicyNo.ParameterName = "@policyno";
                paramPolicyNo.SqlDbType = SqlDbType.VarChar;
                paramPolicyNo.Direction = ParameterDirection.Input;
                paramPolicyNo.Value = 0; ;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramPolicyNo);

                //add command parameters		
                //Add input parameter for (ContactUKey) and set its properties
                SqlParameter paramContactUKey = new SqlParameter();
                paramContactUKey.ParameterName = "@contactukey";
                paramContactUKey.SqlDbType = SqlDbType.Int;
                paramContactUKey.Direction = ParameterDirection.Input;
                paramContactUKey.Value = intContactUKey;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramContactUKey);

                //add command parameters		
                //Add input parameter for (VehicleFound) and set its properties
                SqlParameter paramVehicleFound = new SqlParameter();
                paramVehicleFound.ParameterName = "@vehiclefound";
                paramVehicleFound.SqlDbType = SqlDbType.Int;
                paramVehicleFound.Direction = ParameterDirection.Input;
                paramVehicleFound.Value = intVehicleFound;
                //Add the parameter to the commands parameter collection
                cmdCommand.Parameters.Add(paramVehicleFound);

                //open connection
                cnnConnection.Open();
                //execute command
                rdrReader = cmdCommand.ExecuteReader();

                if (rdrReader.HasRows == true)
                {
                    //handle success
                    objValidateResult.ValidCheck = true;
                    objValidateResult.ValidationFocus = "Validate Claims Point To Genesis Linked Server : Has Rows";
                    objValidateResult.ValidationResultMessage = "Successfully connected to : " + CDSupport.ClaimsDBConnectionString;
                }
                else
                {
                    //handle success
                    objValidateResult.ValidCheck = true;
                    objValidateResult.ValidationFocus = "Validate Claims Point To Genesis Linked Server : No Rows";
                    objValidateResult.ValidationResultMessage = "Successfully connected to : " + CDSupport.ClaimsDBConnectionString;
                }

            }
            catch (Exception ex)
            {
                //handle error
                objValidateResult.ValidCheck = false;
                objValidateResult.ValidationFocus = "Validate Claims Point To Genesis Linked Server";
                objValidateResult.ValidationResultMessage = "Error connecting to : " + CDSupport.ClaimsDBConnectionString + Environment.NewLine + Environment.NewLine + ex.Message;
            }
            finally
            {
                //close reader
                if (rdrReader != null)
                {
                    if (!rdrReader.IsClosed)
                    {
                        rdrReader.Close();
                    }
                    rdrReader = null;
                }

                //clean up
                if (cnnConnection != null)
                {
                    //cleanup connection
                    if (cnnConnection.State == ConnectionState.Open)
                    {
                        cnnConnection.Close();
                    }
                    cnnConnection = null;
                }
            }

            //return results
            return (objValidateResult);
        }//end : ValidateClaimsPointToGenesisLinkedServer

        //define : ValidateAddressRegEx
        public bool ValidateAddressRegEx(string strAddresseeName, string strAddressLine1, string strCity, string strState, string strZipCode)
        {
            //declare variables
            bool blnResult = true;
            string strFullAddressLine = "";

            try
            {
                //check addressee name
                if (string.IsNullOrEmpty(strAddresseeName) == true)
                {
                    //return error
                    return (false);
                }

                //check for blank address line 1
                if (string.IsNullOrEmpty(strAddressLine1) == true)
                {
                    //return error
                    return (false);
                }


                //remove commas
                strCity = strCity.Replace(",", " ");
                strCity = strCity.Trim();
                strState = strState.Replace(",", " ");
                strState = strState.Trim();
                strZipCode = strZipCode.Replace(",", " ");
                strZipCode = strZipCode.Trim();

                //build address string to validate
                strFullAddressLine = strCity + "," + strState + " " + strZipCode;

                //parse address
                blnResult = ParseAddressSegments(strFullAddressLine);
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
            }
            finally
            {
            }

            //return result
            return (blnResult);

        }//end : ValidateAddressRegEx

        //defime method :ParseAddressSegments
        private bool ParseAddressSegments(string addressToParse)
        {
            //declare variables
            bool blnResult = true;
            string strAddressPartList = "";

            try
            {
                //build regular expression string
                StringBuilder pattern = new StringBuilder();
                pattern.Append(@"#Parse address line into named groups (City, State, Zip)" + Environment.NewLine);
                pattern.Append(@"^                         #Begining of string" + Environment.NewLine);
                pattern.Append(@"(                        #Start OR condition" + Environment.NewLine);
                pattern.Append(@"(                        #Begin first condition (City, State, Zip)" + Environment.NewLine);
                pattern.Append(@"(?<City>[A-Za-z\.\-\s]+)  #City" + Environment.NewLine);
                pattern.Append(@"( (?:,\s?) | (?:\s?) )\b  #Comma, comma space, or space" + Environment.NewLine);
                pattern.Append(@"(?<State>[A-Za-z]{2})    #State" + Environment.NewLine);
                pattern.Append(@"(?:\s?)                  #Space" + Environment.NewLine);
                pattern.Append(@"(?<Zip>\d{5}(-\d{4})?)    #Zip" + Environment.NewLine);
                pattern.Append(@") |                      #End first condition" + Environment.NewLine);
                pattern.Append(@"(                        #Begin second condition (City, State)" + Environment.NewLine);
                pattern.Append(@"(?<City>[A-Za-z\s]+)      #City" + Environment.NewLine);
                pattern.Append(@"( (?:,\s?) | (?:\s?) )\b  #Comma, comma space, or space" + Environment.NewLine);
                pattern.Append(@"(?<State>[A-Za-z]{2})    #State" + Environment.NewLine);
                pattern.Append(@"(?:\s?)                  #Space" + Environment.NewLine);
                pattern.Append(@") |                      #End second condition" + Environment.NewLine);
                pattern.Append(@"(                        #Begin third condition (Zip)" + Environment.NewLine);
                pattern.Append(@"(?<Zip>\d{5}(-\d{4})?)    #Zip" + Environment.NewLine);
                pattern.Append(@")                        #End third condition" + Environment.NewLine);
                pattern.Append(@")                        #End OR condition" + Environment.NewLine);
                pattern.Append(@"$                         #End of string" + Environment.NewLine);

                //build regular expression
                Regex rgx = new Regex(pattern.ToString(), RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);
                //match string against regular expression
                Match match = rgx.Match(addressToParse);
                //check for a successful match
                if (match.Success)
                {

                    foreach (string name in rgx.GetGroupNames())
                    {
                        if ((match.Groups[name].Value != String.Empty) && (name == "City" || name == "State" || name == "Zip"))
                        {
                            strAddressPartList = strAddressPartList + "~" + name;
                        }
                    }

                    //check address part list
                    if ((strAddressPartList.IndexOf("City") != -1) && (strAddressPartList.IndexOf("State") != -1) && (strAddressPartList.IndexOf("Zip") != -1))
                    {
                        //valid address
                        blnResult = true;
                    }
                    else
                    {
                        //invalid address
                        blnResult = false;
                    }
                }
                else
                {
                    //invalid address
                    blnResult = false;
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
            }
            finally
            {
            }
            //return result
            return (blnResult);
        }//end : ParseAddressSegments

        //define : ValidateDBConnection
        public string ValidateDBConnection(string strConnectionString)
        {
            //declare variables
            SqlConnection cnnConnection = null;
            string strResult = "Successful Connection...";

            try
            {
                //setup connection
                cnnConnection = new SqlConnection(strConnectionString);
                //open connection
                cnnConnection.Open();

            }
            catch (Exception ex)
            {
                //handle error
                strResult = "Connection Failed...";
            }
            finally
            {
                //clean up
                if (cnnConnection != null)
                {
                    //cleanup connection
                    if (cnnConnection.State == ConnectionState.Open)
                    {
                        cnnConnection.Close();
                    }
                    cnnConnection = null;
                }
            }

            //return results
            return (strResult);
        }//end : ValidateDBConnection

        //define method : EMailSend
        private bool EMailSend()
        {
            //declare variables
            bool blnResult = true;
            LogEntry objLogEntry = new LogEntry();

            try
            {
                //setup log entry
                objLogEntry.Priority = 1;
                objLogEntry.TimeStamp = DateTime.Now;
                objLogEntry.Title = "ClaimsDocs Adjuster Notification";
                objLogEntry.Message = "ClaimsDocs Adjuster Notification";

            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;

            }
            finally
            {
            }

            //return results
            return (blnResult);
        }//end method : EMailSend

        //define method : EMailSend
        public bool SendEMailMessage(EMailSendRequest objEMailSendRequest)
        {
            //declare variables
            bool blnResult = true;

            try
            {
                //setup log entry
                using (MailMessage objMailMessage = new MailMessage(objEMailSendRequest.FromEMailAddress, objEMailSendRequest.ToEMailAddress, objEMailSendRequest.Subject, objEMailSendRequest.Body))
                {
                    SmtpClient objSMTPClient = new SmtpClient(CDSupport.SMTPHost);
                    objSMTPClient.Send(objMailMessage);
                }
            }
            catch (Exception ex)
            {
                //handle error
                blnResult = false;
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : SendEMailMessage() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;

            }
            finally
            {
            }

            //return results
            return (blnResult);
        }//end method : EMailSend

        //define method : RentalDenominatorGetByCompanyNumber
        static public int RentalDenominatorGetByCompanyNumber(int intCompanyNumber)
        {
            //declare variables
            int intRentalDenominator = -1;

            try
            {
                switch (intCompanyNumber)
                {
                    case 40: //AGA
                        intRentalDenominator = 1;
                        break;

                    case 41: //APA
                        intRentalDenominator = 1;
                        break;

                    case 42: //ACA
                        intRentalDenominator = 10;
                        break;

                    case 43: //AAL
                        intRentalDenominator = 1;
                        break;

                    case 44: //AFA
                        intRentalDenominator = 1;
                        break;

                    case 45: //ATX
                        intRentalDenominator = 1;
                        break;

                    case 46: //AAZ
                        intRentalDenominator = 21;
                        break;

                    case 47: //ASC
                        intRentalDenominator = 21;
                        break;

                    case 48: //ALA
                        intRentalDenominator = 15;
                        break;

                    case 49: //ANV
                        intRentalDenominator = 20;
                        break;

                    case 50: //AMS
                        intRentalDenominator = 1;
                        break;

                    case 51: //AOK
                        intRentalDenominator = 20;
                        break;

                    case 52: //ATN
                        intRentalDenominator = 20;
                        break;

                    case 53: //AIN
                        intRentalDenominator = 20;
                        break;

                }//end : switch (intCompanyNumber)

            }
            catch (Exception ex)
            {
                //handle error
                intRentalDenominator = -1;
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : RentalDenominatorGetByCompanyNumber() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
            }
            finally
            {
            }

            //return results
            return (intRentalDenominator);
        }//end method : RentalDenominatorGetByCompanyNumber

        //define method : TowingDenominatorGetByCompanyNumber
        static public int TowingDenominatorGetByCompanyNumber(int intCompanyNumber)
        {
            //declare variables
            int intTowingDenominator = -1;

            try
            {
                switch (intCompanyNumber)
                {
                    case 40: //AGA
                        intTowingDenominator = 1;
                        break;

                    case 41: //APA
                        intTowingDenominator = 1;
                        break;

                    case 42: //ACA
                        intTowingDenominator = 10;
                        break;

                    case 43: //AAL
                        intTowingDenominator = 1;
                        break;

                    case 44: //AFA
                        intTowingDenominator = 1;
                        break;

                    case 45: //ATX
                        intTowingDenominator = 1;
                        break;

                    case 46: //AAZ
                        intTowingDenominator = 21;
                        break;

                    case 47: //ASC
                        intTowingDenominator = 6;
                        break;

                    case 48: //ALA
                        intTowingDenominator = 3;
                        break;

                    case 49: //ANV
                        intTowingDenominator = 2;
                        break;

                    case 50: //AMS
                        intTowingDenominator = 1;
                        break;

                    case 51: //AOK
                        intTowingDenominator = 3;
                        break;

                    case 52: //ATN
                        intTowingDenominator = 3;
                        break;

                    case 53: //AIN
                        intTowingDenominator = 3;
                        break;

                }//end : switch (intCompanyNumber)

            }
            catch (Exception ex)
            {
                //handle error
                intTowingDenominator = -1;
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : TowingDenominatorGetByCompanyNumber() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
            }
            finally
            {
            }

            //return results
            return (intTowingDenominator);
        }//end method : TowingDenominatorGetByCompanyNumber


    }//end : public class CDSupport : ICDSupport
}//end : namespace ClaimsDocsBizLogic
