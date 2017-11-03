using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClaimsDocsBizLogic
{
    //define Data Contract for ClaimsDocsLog
    
    [DataContract]
    public class ClaimsDocsLog
    {
        //declare public class properties
        [DataMember]
        public int ClaimsDocsLogID { get; set; }
        [DataMember]
        public int LogSourceTypeID { get; set; }
        [DataMember]
        public int LogTypeID { get; set; }
        [DataMember]
        public string SourceName { get; set; }
        [DataMember]
        public string LogTypeName { get; set; }
        [DataMember]
        public string MessageIs { get; set; }
        [DataMember]
        public string ExceptionIs { get; set; }
        [DataMember]
        public string StackTraceIs { get; set; }
        [DataMember]
        public DateTime IUDateTime { get; set; }
    }//end class definition of class : ClaimsDocsLog

    [DataContract]
    public class ValidateResult
    {
        //declare public class properties
        [DataMember]
        public bool ValidCheck { get; set; }
        [DataMember]
        public string ValidationFocus { get; set; }
        [DataMember]
        public string ValidationResultMessage { get; set; }

    }//end class definition of class : ClaimsDocsLog

    [DataContract]
    public class EMailSendRequest
    {
        [DataMember]
        public string FromEMailAddress { get; set; }
        [DataMember]
        public string ToEMailAddress { get; set; }
        [DataMember]
        public string Subject {get;set;}
        [DataMember]
        public string Body { get; set; }

    }//end : EMailSendRequest

    //define service contract for ICDSupport
    [ServiceContract]
    public interface ICDSupport
    {
        [OperationContract]
        int ClaimsDocsLogCreate(ClaimsDocsLog objClaimsDocsLog,string strConnectionString);

        [OperationContract]
        List<ClaimsDocsLog> ClaimsDocsLogSearch(string strSQLString, string strConnectionString);

        [OperationContract]
        ValidateResult ValidateWCFServiceCall();

        [OperationContract]
        ValidateResult ValidateXMLOutputLocation();

        [OperationContract]
        ValidateResult ValidateMessageQueue();

        [OperationContract]
        ValidateResult ValidateClaimsDBConnection();

        [OperationContract]
        ValidateResult ValidateClaimsDocsDBConnection();

        [OperationContract]
        ValidateResult ValidateCorrespondenceDBConnection();

        [OperationContract]
        ValidateResult ValidateAMSDBConnection();

        [OperationContract]
        ValidateResult ValidateGenesisDBConnection();

        [OperationContract]
        ValidateResult ValidateAMSPointToClaimsLinkedServer(int intClaimUKey);

        [OperationContract]
        ValidateResult ValidateAMSPointToGenesisLinkedServer(string strPolicyNumber);

        [OperationContract]
        ValidateResult ValidateClaimsPointToGenesisLinkedServer(int intContactUKey, int intVehicleFound);

        [OperationContract]
        bool ValidateAddressRegEx(string strAddresseeName, string strAddressLine1, string strCity, string strState, string strZipCode);

        [OperationContract]
        bool SendEMailMessage(EMailSendRequest objEMailSendRequest);


    }//end : public interface ICDSupport

}//end : namespace ClaimsDocsBizLogic
