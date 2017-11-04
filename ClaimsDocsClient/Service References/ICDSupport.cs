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

    //define service contract for ICDSupport
    [ServiceContract]
    public interface ICDSupport
    {
        [OperationContract]
        int ClaimsDocsLogCreate(ClaimsDocsLog objClaimsDocsLog,string strConnectionString);

        [OperationContract]
        List<ClaimsDocsLog> ClaimsDocsLogSearch(string strSQLString, string strConnectionString);

    }//end : public interface ICDSupport


}//end : namespace ClaimsDocsBizLogic
