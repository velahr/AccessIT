using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ClaimsDocsBizLogic.SchemaClasses;

namespace ClaimsDocsBizLogic
{
    //define enumerations
    public enum GenerateMessage
    {
        XMLGenerated = 100,
        PDFGenerated = 101,
        StoredInImageRight=99,
        UnknownStatus = 98,
        Success = 102,
        ExceptionOccured = 103,
        UnableToGenerateVehicleData = 1,
        UnableToBuildDocumentDefinition = 2,
        UnableToBuildSubmitter = 3,
        UnableToBuildPolicy = 4,
        UnableToBuildCoverages = 5,
        UnableToBuildAddressee = 6,
        UnableToBuildPremiumFinanceCo = 7,
        UnableToBuildLienHolder = 8,
        UnableToBuildNamedInsured = 9,
        UnableToBuildVehicle = 10,
        UnableToBuildAdjuster = 11,
        UnableToBuildProducer = 12,
        UnableToBuildLossDescription = 13,
        UnableToBuildAttorney = 14,
        UnableToBuildWhiteHillAddressee = 15,
        UnableToBuildMetaData = 16,
        UnableToSerializeDocumentToXMLFile = 17,
        UnableToGenerateClaimsDocPDF = 18,
        UnableToGenerateCompanyInformation = 19,
        UnableToStoreFileToImageRight = 20,
        UnableToRetrieveFileFromImageRight = 21

    }//end : GenerateMessage

    //start definition of class : VehicleData
    [DataContract]
    public class VehicleData
    {
        [DataMember]
        public string VehicleUKey { get; set; }
        [DataMember]
        public int CarNumber { get; set; }

        public VehicleData()
        {
            VehicleUKey = "";
            CarNumber = 0;
        }
    }//end : DocumentDisplayField

    //start definition of class : DocumentDisplayField
    [DataContract]
    public class DocumentDisplayField
    {
        [DataMember]
        public string DisplayFieldName { get; set; }
        [DataMember]
        public string DisplayFieldValue { get; set; }

        public DocumentDisplayField()
        {
            DisplayFieldName = "";
            DisplayFieldValue = "";
        }

    }//end : DocumentDisplayField

    //start definition of class : DocGenerationRequest
    [DataContract]
    public class DocGenerationRequest
    {
        //declare public class properties
        [DataMember]
        public string RunMode { get; set; }        
        [DataMember]
        public int DocumentID { get; set; }
        [DataMember]
        public string InstanceID { get; set; }
        [DataMember]
        public string PolicyNumber { get; set; }
        [DataMember]
        public string CompanyNumber { get; set; }
        [DataMember]
        public string ClaimNumber { get; set; }
        [DataMember]
        public int ContactNumber { get; set; }
        [DataMember]
        public int ContactType { get; set; }
        [DataMember]
        public int UserIDNumber { get; set; }
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string UserDepartment { get; set; }
        [DataMember]
        public string VehicleUKey { get; set; }
        [DataMember]
        public int CarNumber { get; set; }
        [DataMember]
        public int GroupID { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public string AddresseeName { get; set; }
        [DataMember]
        public string AddresseeAddressLine1 { get; set; }
        [DataMember]
        public string AddresseeAddressLine2 { get; set; }
        [DataMember]
        public string AddresseeCity { get; set; }
        [DataMember]
        public string AddresseeState { get; set; }
        [DataMember]
        public string AddresseeZipCode { get; set; }
        [DataMember]
        public bool GenerateXML { get; set; }
        [DataMember]
        public bool GeneratePDF { get; set; }
        [DataMember]
        public bool StoreToImageRight { get; set; }
        [DataMember]
        public bool CopyToAgent { get; set; }
        [DataMember]
        public bool CopyToAttorney { get; set; }
        [DataMember]
        public bool CopyToFinanceCompany { get; set; }
        [DataMember]
        public bool CopyToInsured { get; set; }
        [DataMember]
        public bool CopyToLeinholder { get; set; }
        [DataMember]
        public bool DisplayFieldsGetFromXML { get; set; }


        //initialize class properties;
        public DocGenerationRequest()
        {
            RunMode = "";
            DocumentID = 0;
            InstanceID = "";
            PolicyNumber = "";
            CompanyNumber = "";
            ClaimNumber = "";
            ContactNumber = 0;
            ContactType = 0;
            UserIDNumber = 0;
            UserID = "";
            UserName = "";
            UserDepartment = "";
            VehicleUKey = "";
            CarNumber = 0;
            GroupID = 0;
            AddresseeName = "";
            AddresseeAddressLine1 = "";
            AddresseeAddressLine2 = "";
            AddresseeCity = "";
            AddresseeState = "";
            AddresseeZipCode = "";
            GenerateXML = false;
            GeneratePDF = false;
            StoreToImageRight = false;
            DisplayFieldsGetFromXML = false;
        }

    }//end class definition of class : DocGenerationRequest

    //define class : DocuMakerClaimsDocsResponse
    [DataContract]
    public class DocuMakerClaimsDocsResponse
    {
        [DataMember]
        public bool DocumentGenerated { get; set; }
        [DataMember]
        public string DocumentPathAndFileName { get; set; }
        [DataMember]
        public string PrinterName { get; set; }
        [DataMember]
        public string ReceipientType { get; set; }

        //define constructor
        public DocuMakerClaimsDocsResponse()
        {
            DocumentGenerated = false;
            DocumentPathAndFileName = "";
            PrinterName = "";
            ReceipientType = "";
        }

    }//end : DocuMakerClaimsDocsResponse

    //start definition of class : DocGenerationResponse
    [DataContract]
    public class DocGenerationResponse
    {
        [DataMember]
        public List<DocuMakerClaimsDocsResponse> ListDocuMakerClaimsDocsResponse {get;set;}

        [DataMember]
        public string FileNumber { get; set; }
        [DataMember]
        public string ClaimsNumber { get; set; }
        [DataMember]
        public string GroupName { get; set; }
        [DataMember]
        public string ClaimsDocsDocID { get; set; }
        [DataMember]
        public string PackType { get; set; }
        [DataMember]
        public string DocType { get; set; }
        [DataMember]
        public string FolderName { get; set; }
        [DataMember]
        public string DateOfLoss { get; set; }
        [DataMember]
        public int GeneralResponseCode { get; set; }
        [DataMember]
        public string GeneralResponseMessage { get; set; }
        [DataMember]
        public string XMLFilePathAndName { get; set; }
        [DataMember]
        public int XMLResponseCode { get; set; }
        [DataMember]
        public string XMLResponseMessage { get; set; }
        [DataMember]
        public string PDFFilePathAndName { get; set; }
        [DataMember]
        public int PDFResponseCode { get; set; }
        [DataMember]
        public string PDFResponseMessage { get; set; }
        [DataMember]
        public string ImageRightFilePathAndName { get; set; }
        [DataMember]
        public int ImageRightResponseCode { get; set; }
        [DataMember]
        public string ImageRightResponseMessage { get; set; }

        public DocGenerationResponse()
        {
            GeneralResponseCode = 0;
            GeneralResponseMessage = "";

            XMLFilePathAndName = "";
            XMLResponseCode = 0;
            XMLResponseMessage = "";

            PDFFilePathAndName = "";
            PDFResponseCode = 0;
            PDFResponseMessage = "";

            ImageRightFilePathAndName = "";
            ImageRightResponseCode = 0;
            ImageRightResponseMessage = "";
        }

    }//end : DocGenerationResponse

    //start definition of class : ImageRightClaimRequest
    [DataContract]
    public class ImageRightClaimRequest
    {
        [DataMember]
        public string RequestorUserName { get; set; }
        [DataMember]
        public bool RequestResult { get; set; }
        [DataMember]
        public string ImageRightWSUrl { get; set; }
        [DataMember]
        public string ImageRightLogin { get; set; }
        [DataMember]
        public string ImageRightPassword { get; set; }
        [DataMember]
        public string SourceDocumentPath { get; set; }
        [DataMember]
        public string IRDocumentPath { get; set; }
        [DataMember]
        public string ClaimsNumber { get; set; }
        [DataMember]
        public string ClaimsDocsDocID { get; set; }
        [DataMember]
        public string ImageRightClaimsDrawer { get; set; }
        [DataMember]
        public string FileNumber { get; set; }
        [DataMember]
        public string PackType { get; set; }
        [DataMember]
        public string DocType { get; set; }
        [DataMember]
        public string CaptureDate { get; set; }
        [DataMember]
        public string FolderName { get; set; }
        [DataMember]
        public string DateOfLoss { get; set; }
        [DataMember]
        public string DevicePath { get; set; }
        [DataMember]
        public string DocumentKey { get; set; }
        [DataMember]
        public string Reason { get; set; }
        [DataMember]
        public bool CreateTask { get; set; }
        [DataMember]
        public string FlowID { get; set; }
        [DataMember]
        public string StepID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Priority { get; set; }
        [DataMember]
        public string TDIN { get; set; }

        //use constructor to initialize values
        public ImageRightClaimRequest()
        {
            RequestResult = false;
            ImageRightWSUrl = "";
            ImageRightWSUrl = "";
            ImageRightLogin = "";
            ImageRightPassword = "";
            SourceDocumentPath = "";
            IRDocumentPath = "";
            ClaimsNumber = "";
            ClaimsDocsDocID = "";
            ImageRightClaimsDrawer = "";
            FileNumber = "";
            PackType = "";
            DocType = "";
            CaptureDate = "";
            FolderName = "";
            DateOfLoss = "";
            DevicePath = "";
            DocumentKey = "";
            Reason = "";
            CreateTask = false;
            FlowID = "";
            StepID = "";
            Description = "";
            Priority = "";
            TDIN = "";
        }

    }//end : ImageRightPolicyRequest

    //define service contract for IClaimsDocsGenerator
    [ServiceContract ]
    public interface IClaimsDocsGenerator
    {
        [OperationContract]   
        DocGenerationResponse ClaimsDocsGenerateDocument(List<DocumentDisplayField> listDisplayFields, DocGenerationRequest objDocumentGenerationRequest);

        [OperationContract]
        ClaimsDocumentInputDocumentWhitehillPolicyClaimInformation PolicyInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strConnectionString);

        [OperationContract]
        ClaimsDocumentCompanyInformation CompanyInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strConnectionString);

        [OperationContract]
        ClaimsDocumentInputDocumentWhitehillCoverages CoverageInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        ClaimsDocumentInputDocumentWhitehillPremiumFinanceCo PremiumFinanceCoInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        ClaimsDocumentInputDocumentWhitehillLienHolder LienHolderInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        ClaimsDocumentInputDocumentWhitehillNamedInsured NamedInsuredInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        ClaimsDocumentInputDocumentWhitehillVehicle VehicleInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        ClaimsDocumentInputDocumentWhitehillAdjuster AdjusterInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        ClaimsDocumentInputDocumentWhitehillLossDescription LossDescriptionInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        ClaimsDocumentInputDocumentWhitehillProducer ProducerInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        ClaimsDocumentInputDocumentWhitehillAttorney AttorneyInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        ClaimsDocumentAddressee AddresseeInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        DocGenerationRequest DocGenerationRequestInformationGet(DocGenerationRequest objDocumentGenerationRequest);

        [OperationContract]
        VehicleData VehicleDataInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        List<ClaimsDocumentAllCoveragesCar> VehicleCoverageInformationGet(DocGenerationRequest objDocumentGenerationRequest, string strClaimsDBConnectionString);

        [OperationContract]
        ImageRightClaimRequest ImageRightSaveDocument(ImageRightClaimRequest objIRRequest);

        [OperationContract]
        ImageRightClaimRequest ImageRightGetDocument(ImageRightClaimRequest objIRPolicyRequest);

        [OperationContract]
        DocGenerationRequest DocGenerationRequestInformationGetByApprovalID(int intDocID);

    }//end : public interface IClaimsDocsGenerator
}//end : namespace ClaimsDocsBizLogic
