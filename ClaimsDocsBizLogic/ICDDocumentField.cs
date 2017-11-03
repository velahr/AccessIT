using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ClaimsDocsBizLogic.SchemaClasses;

namespace ClaimsDocsBizLogic
{
    //start definition of class : DocumentField
    [DataContract]
    public class DocumentField
    {
        //declare public class properties
        [DataMember]
        public int DocumentFieldID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string FieldNameIs { get; set; }
        [DataMember]
        public string FieldTypeIs { get; set; }
        [DataMember]
        public string IsFieldRequired { get; set; }
        [DataMember]
        public string FieldDescription { get; set; }
        [DataMember]
        public DateTime IUDateTime { get; set; }

        //initialize class properties;
        public DocumentField()
        {
            DocumentFieldID = 0;
            DocumentID = 0;
            FieldNameIs = "";
            FieldTypeIs = "";
            IsFieldRequired = "";
            FieldDescription = "";
            IUDateTime = DateTime.Now;
        }
    }//end class definition of class : tblDocumentField

    //define ICDDocumentField Service Contract
    [ServiceContract]
    public interface ICDDocumentField
    {
        [OperationContract]
        int DocumentFieldCreate(DocumentField objDocument, string strConnectionString);

        [OperationContract]
        DocumentField DocumentFieldRead(DocumentField objDocument, string strConnectionString);

        [OperationContract]
        int DocumentFieldUpdate(DocumentField objDocumentField, string strConnectionString);

        [OperationContract]
        List<DocumentField> DocumentFieldSearch(string strSQLString, string strConnectionString);

        [OperationContract]
        int DocumentFieldClear(DocumentField objDocumentField, string strConnectionString);

        [OperationContract]
        List<DocumentDisplayField> DisplayFieldsGetFromXMLFile(ClaimsDocument objClaimsDocument);

    }//end : public interface ICDDocumentField
}//end : namespace ClaimsDocsBizLogic
