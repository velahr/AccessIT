using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ClaimsDocsBizLogic
{
    //start definition of class : Document
    [DataContract]
    public class Document
    {
        //declare public class properties
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string InstanceID { get; set; }
        [DataMember]
        public string DocumentCode { get; set; }
        [DataMember]
        public int DepartmentID { get; set; }
        [DataMember]
        public int ProgramID { get; set; }
        [DataMember]
        public string ProgramCode { get; set; }
        [DataMember]
        public string Review { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string TemplateName { get; set; }
        [DataMember]
        public string StyleSheetName { get; set; }
        [DataMember]
        public DateTime EffectiveDate { get; set; }
        [DataMember]
        public DateTime ExpirationDate { get; set; }
        [DataMember]
        public string ProofOfMailing { get; set; }
        [DataMember]
        public string DataMatx { get; set; }
        [DataMember]
        public string ImportToImageRight { get; set; }
        [DataMember]
        public string ImageRightDocumentID { get; set; }
        [DataMember]
        public string ImageRightDocumentSection { get; set; }
        [DataMember]
        public string ImageRightDrawer { get; set; }
        [DataMember]
        public int ContactNo { get; set; }
        [DataMember]
        public int ContactType { get; set; }
        [DataMember]
        public string CopyAgent { get; set; }
        [DataMember]
        public string CopyInsured { get; set; }
        [DataMember]
        public string CopyLienHolder { get; set; }
        [DataMember]
        public string CopyFinanceCo { get; set; }
        [DataMember]
        public string CopyAttorney { get; set; }
        [DataMember]
        public int DiaryNumberOfDays { get; set; }
        [DataMember]
        public string DiaryAutoUpdate { get; set; }
        [DataMember]
        public int DesignerID { get; set; }
        [DataMember]
        public DateTime LastModified { get; set; }
        [DataMember]
        public string Active { get; set; }
        [DataMember]
        public string AttachedDocument { get; set; }
        [DataMember]
        public DateTime IUDateTime { get; set; }

        [DataMember]
        public List<DocumentGroup> listDocumentGroup { get; set; }
        
        [DataMember]
        public List<DocumentField> lisDocumentField { get; set; }

        [DataMember]
        public string AdditionalDataRequired { get; set; }

        //initialize class properties;
        public Document()
        {
            DocumentID = 0;
            DocumentCode = "";
            DepartmentID = 0;
            ProgramID = 0;
            Review = "";
            Description = "";
            TemplateName = "";
            StyleSheetName = "";
            EffectiveDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            ProofOfMailing = "";
            DataMatx = "";
            ImportToImageRight = "";
            ImageRightDocumentID = "";
            ImageRightDocumentSection = "";
            ImageRightDrawer = "";
            ContactNo = 0;
            ContactType = 0;
            CopyAgent = "";
            CopyInsured = "";
            CopyLienHolder = "";
            CopyFinanceCo = "";
            CopyAttorney = "";
            DiaryNumberOfDays = 0;
            DiaryAutoUpdate = "";
            DesignerID = 0;
            LastModified = DateTime.Now;
            Active = "";
            AttachedDocument = "";
            IUDateTime = DateTime.Now;
        }
    
    }//end class definition of class : Document

    //start definition of class : DocumentGroup
    [DataContract]
    public class DocumentGroup
    {
        //declare public class properties
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public DateTime IUDateTime { get; set; }

        //initialize class properties;
        public DocumentGroup()
        {
            DocumentID = 0;
            GroupID = 0;
            IUDateTime = DateTime.Now;
        }
    }//end class definition of class : Document

    //start definition of class : DocumentLog
    [DataContract]
    public class DocumentLog
    {
        //declare public class properties
        [DataMember]
        public string InstanceID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public int SubmitterID { get; set; }
        [DataMember]
        public DateTime DateSubmitted { get; set; }
        [DataMember]
        public string ApprovalRequired { get; set; }
        [DataMember]
        public int ApproverID { get; set; }
        [DataMember]
        public DateTime DateApproved { get; set; }
        [DataMember]
        public int DeclinerID { get; set; }
        [DataMember]
        public DateTime DateDeclined { get; set; }
        [DataMember]
        public string ReasonDeclined { get; set; }
        [DataMember]
        public DateTime DateGenerated { get; set; }
        [DataMember]
        public string GeneratedErrorCode { get; set; }
        [DataMember]
        public string PolicyNo { get; set; }
        [DataMember]
        public string ClaimNo { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public DateTime IUDateTime { get; set; }

        //initialize class properties;
        public DocumentLog()
        {
            InstanceID = "";
            DocumentID = 0;
            SubmitterID = 0;
            DateSubmitted = DateTime.Now;
            ApprovalRequired = "";
            ApproverID = 0;
            DateApproved = DateTime.Now;
            DeclinerID = 0;
            DateDeclined = DateTime.Now;
            ReasonDeclined = "";
            DateGenerated = DateTime.Now;
            GeneratedErrorCode = "";
            PolicyNo = "";
            ClaimNo = "";
            GroupName = "";
            IUDateTime = DateTime.Now;
        }

    }//end class definition of class : DocumentLog

    //start definition of class : DocumentApprovalQueue
    [DataContract]
    public class DocumentApprovalQueue
    {
        //declare public class properties
        [DataMember]
        public int ApprovalQueueID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string InstanceID { get; set; }
        [DataMember]
        public int SubmitterID { get; set; }
        [DataMember]
        public DateTime DateSubmitted { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public DateTime IUDateTime { get; set; }

        //initialize class properties;
        public DocumentApprovalQueue()
        {
            ApprovalQueueID = 0;
            DocumentID = 0;
            InstanceID = "";
            SubmitterID = 0;
            DateSubmitted = DateTime.Now;
            Content = "";
            IUDateTime = DateTime.Now;
        }

    }//end class definition of class : DocumentApprovalQueue

    //start definition of class : DocumentApprovalQueue
    [DataContract]
    public class DocumentsNeedingApproval
    {
        //declare public class properties
        [DataMember]
        public int ApprovalQueueID { get; set; }
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public String DocumentCode { get; set; }
        [DataMember]
        public string InstanceID { get; set; }
        [DataMember]
        public String Description { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public DateTime DateSubmitted { get; set; }

        //initialize class properties;
        public DocumentsNeedingApproval()
        {
            this.ApprovalQueueID = 0;
            this.DocumentID = 0;
            this.DocumentCode = String.Empty;
            this.Description = String.Empty;
            this.UserName = String.Empty;
            this.DateSubmitted = DateTime.Now;
        }

    }//end class definition of class : DocumentApprovalQueue

    //define ICDDocument Service Contract
    [ServiceContract]
    public interface ICDDocument
    {
        [OperationContract]
        int DocumentCreate(Document objDocument, string strConnectionString);

        [OperationContract]
        Document DocumentRead(Document objDocument, string strConnectionString);

        [OperationContract]
        int DocumentUpdate(Document objDocument, string strConnectionString);

        [OperationContract]
        List<Document> DocumentSearch(string strSQLString, string strConnectionString);

        [OperationContract]
        List<DocumentGroup> DocumentGroupSearch(string strSQLString, string strConnectionString);

        [OperationContract]
        int DocumentGroupClear(Document objDocument, string strConnectionString);

        [OperationContract]
        List<DocumentField> DocumentFieldSearch(string strSQLString, string strConnectionString);

        [OperationContract]
        int DocumentFieldClear(DocumentField objDocumentField, string strConnectionString);

        [OperationContract]
        int DocumentFieldCreate(DocumentField objDocumentField, string strConnectionString);

        [OperationContract]
        List<Document> DocumentAdjusterListSearch(string strSQLString, string strConnectionString);

        [OperationContract]
        int DocumentLogCreate(DocumentLog objDocumentLog, string strConnectionString);

        [OperationContract]
        int DocumentLogUpdate(DocumentLog objDocumentLog, string strConnectionString);

        [OperationContract]
        DocumentLog DocumentLogRead(string vchrInstanceID, string strConnectionString);

        [OperationContract]
        int DocumentApprovalQueueCreate(DocumentApprovalQueue objDocumentApprovalQueue, string strConnectionString);

        [OperationContract]
        int DocumentApprovalQueueUpdate(DocumentApprovalQueue objDocumentApprovalQueue, string strConnectionString);

        [OperationContract]
        int DocumentDecline(DocumentLog objDocumentLog, string strConnectionString);

        [OperationContract]
        int DocumentApprove(DocumentLog objDocumentLog, string strConnectionString);

        [OperationContract]
        List<DocumentApprovalQueue> DocumentApprovalQueueReadALL(string strConnectionString);
        
        [OperationContract]
        DocumentApprovalQueue DocumentApprovalQueueRead(int intDocumentID, string strConnectionString);

        [OperationContract]
        List<DocumentsNeedingApproval> DocumentsNeedingApprovalRead(string strConnectionString);
        
    }//end : public interface ICDDocument

}
