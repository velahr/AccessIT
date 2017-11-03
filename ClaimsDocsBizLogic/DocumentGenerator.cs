using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Messaging;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using ClaimsDocsBizLogic.SchemaClasses;

namespace ClaimsDocsBizLogic
{
    public enum DocumentGeneratorType
    {
        Documaker
    }

    #region DocumentGenerator

    public class DocumentGenerator
    {
        internal List<DocuMakerClaimsDocsResponse> GenerateDocumentSingle([System.Xml.Serialization.XmlElement("ClaimsDocument")] ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument ClaimsDocument, string claimsNumber, string documentDefinitionName)
        {
            DocumentRequestorBase request = null;
            DocumentReceiverBase receipt = null;
            // Get the type of document generator we're using
            //DocumentGenerator docGen = Settings.Default.DocumentGenerator;
            DocumentGeneratorType docGen = DocumentGeneratorType.Documaker;
            string docPath = null;
            string documentKey = null;
            int numberOfTries = CDSupport.DocuMakerNumberOfTries;
            //int numberOfResponseTries = 10;
            int numberOfResponseTries = CDSupport.NumberOfResponseTries;
            int intNumberOfTries = 3;
            List<DocuMakerClaimsDocsResponse> listDocuMakerCDResponse = null;

            for (int i = 1; i <= numberOfTries; i++)
            {
                try
                {
                    request = DocumentRequestorBase.GetInstance(ClaimsDocument, docGen, claimsNumber, documentDefinitionName);
                    documentKey = request.SendRequest();
                }
                catch (Exception ex)
                {
                    continue;
                }

                if (string.IsNullOrEmpty(documentKey))
                {
                    continue;
                }

                try
                {
                    for (intNumberOfTries = 1; intNumberOfTries <= numberOfResponseTries; intNumberOfTries++)
                    {
                        //attempt to get response
                        receipt = DocumentReceiverBase.GetInstance(documentKey, docGen);
                        listDocuMakerCDResponse = receipt.ReceiveResponse(ClaimsDocument);

                        //check valid response
                        if (listDocuMakerCDResponse.Count > 0)
                        {
                            //break out of for loop
                            i = numberOfTries;
                            break;
                        }
                        //pause for a few seconds before trying again
                        System.Threading.Thread.Sleep(1000 * CDSupport.DocuMakerPeekPauseInSeconds);
                    }
                }
                catch (Exception ex)
                {
                    //handle error                
                    ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                    CDSupport objCDSupport = new CDSupport();
                    //fill log
                    objClaimsLog.ClaimsDocsLogID = 0;
                    objClaimsLog.LogTypeID = 3;
                    objClaimsLog.LogSourceTypeID = 1;
                    objClaimsLog.MessageIs = "Method : GenerateDocumentSingle() ";
                    objClaimsLog.ExceptionIs = ex.Message;
                    objClaimsLog.StackTraceIs = ex.StackTrace;
                    objClaimsLog.IUDateTime = DateTime.Now;
                    //create log record
                    objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                    //cleanup
                    objClaimsLog = null;
                    objCDSupport = null;
                    continue;
                }

                // Make sure the document path we got back was valid
                if (listDocuMakerCDResponse.Count > 0)
                {
                    //EntLogger.LogOrThrowError<AIHQuote>(string.Format("Received EMPTY document path from '{0}'!  See the full response in log file.", docGen), logTitle, logCategory, i, numberOfTries, aihQuote);
                }
                else
                {
                    // We got a good path, so exit the loop
                    break;
                }
            }
            //return results
            return (listDocuMakerCDResponse);
        }
    }

    #endregion

    public abstract class DocumentRequestorBase
    {
        protected ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument ClaimsDocument = null;

        protected string _claimsNumber = string.Empty;
        protected string _documentDefinitionName = string.Empty;

        protected internal DocumentRequestorBase(ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument objClaimsDocument, string claimsNumber, string documentDefinitionName)
        {
            //declare variables
            try
            {
                this.ClaimsDocument = objClaimsDocument;
                this._claimsNumber = claimsNumber;
                this._documentDefinitionName = documentDefinitionName;
            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DocumentRequestorBase() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
            }
        }

        protected internal DocumentRequestorBase(ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument objClaimsDocument)
        {
            //declare variables
            try
            {
                this.ClaimsDocument = objClaimsDocument;
            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DocumentRequestorBase() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
            }
        }



        public static DocumentRequestorBase GetInstance(ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument objClaimsDocument, DocumentGeneratorType docGen, string claimsNumber, string documentDefinitionName)
        {
            //declare variables
            DocumentRequestorDocumaker objDocumentRequestorDocumaker = null;

            try
            {
                switch (docGen)
                {
                    case DocumentGeneratorType.Documaker:
                        objDocumentRequestorDocumaker = new DocumentRequestorDocumaker(objClaimsDocument, claimsNumber, documentDefinitionName);
                        break;

                    default:
                        throw new NotImplementedException(string.Format("Unable to create object for the ClaimsDocs document generator requestor '{0}'.", docGen));
                }
            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : GetInstance() ";
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

            //return result
            return (objDocumentRequestorDocumaker);
        }


        public static DocumentRequestorBase GetInstance(ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument objClaimsDocument, DocumentGeneratorType docGen)
        {
            //declare variables
            DocumentRequestorDocumaker objDocumentRequestorDocumaker = null;

            try
            {
                switch (docGen)
                {
                    case DocumentGeneratorType.Documaker:
                        objDocumentRequestorDocumaker = new DocumentRequestorDocumaker(objClaimsDocument);
                        break;

                    default:
                        throw new NotImplementedException(string.Format("Unable to create object for the ClaimsDocs document generator requestor '{0}'.", docGen));
                }
            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : GetInstance() ";
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

            //return result
            return (objDocumentRequestorDocumaker);
        }

        public abstract string SendRequest();

        public abstract string SendRequestTest(string strMessageIs);
    }

    public class DocumentRequestorDocumaker : DocumentRequestorBase
    {

        internal DocumentRequestorDocumaker(ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument ClaimsDocument)
            : base(ClaimsDocument)
        {
        }


        // Make the constructor internal so only the base class can make it
        internal DocumentRequestorDocumaker(ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument ClaimsDocument, string claimsNumber, string documentDefinitionName)
            : base(ClaimsDocument, claimsNumber, documentDefinitionName)
        {
        }


        public override string SendRequest()
        {
            //declare variables
            Guid requestGuid = Guid.NewGuid();

            try
            {
                MessageQueue sendQ = new MessageQueue(CDSupport.DocumakerRequestQueue, QueueAccessMode.Send);
                Message msg = new Message();
                // Streams/writers used for serialization
                MemoryStream memStream;
                XmlTextWriter xw;
                XDocument envDoc = BuildRequestEnvelope(requestGuid);
                string envelopeString = envDoc.Declaration + envDoc.ToString();

                // Serialize AIHQuote into string
                XmlSerializer xs = new XmlSerializer(typeof(ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument));
                memStream = new MemoryStream();
                xw = new XmlTextWriter(memStream, Encoding.UTF8);
                xs.Serialize(xw, ClaimsDocument);

                memStream = (MemoryStream)xw.BaseStream;
                string aihQuoteString = EncodeToBase64(UTF8ByteArrayToString(memStream.ToArray()));
                xw.Close();

                // Combine everything into a complete message string
                DateTime today = DateTime.Today;
                string boundary = string.Format("----=_Part_{0}_{1}.{2}", today.Day, today.ToFileTime(), today.Ticks);
                string multiPartHeader = "Content-Type: multipart/related;\r\n\tboundary=\"" + boundary + "\"";
                string singlePartHeader = "--" + boundary + "\r\nContent-Type: text/xml\r\nContent-Transfer-Encoding: 8bit";
                string dataHeader = "--" + boundary + "\r\nContent-Type: application/ids\r\nContent-Transfer-Encoding: base64\r\nContent-ID: DATA";
                string msgString = string.Format("{0}\r\n\r\n{1}\r\n\r\n{2}\r\n{3}\r\n\r\n{4}\r\n{5}",
                    multiPartHeader, singlePartHeader, envelopeString, dataHeader, aihQuoteString, "--" + boundary + "--");

                // Stream the complete string into the message
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(msgString);
                memStream = new MemoryStream(bytes);
                msg.Label = CDSupport.DocumakerRequestQueue + @"\DocuSend";
                msg.BodyStream = memStream;

                sendQ.Send(msg);
                //sendQ.Close();
            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : SendRequest() ";
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

            return requestGuid.ToString("D");
        }


        public override string SendRequestTest(string strMessageIs)
        {
            //declare variables
            Guid requestGuid = Guid.NewGuid();
            bool blnResult = true;



            try
            {
                MessageQueue sendQ = new MessageQueue(CDSupport.DocumakerRequestQueue, QueueAccessMode.Send);
                Message msg = new Message();
                // Streams/writers used for serialization
                MemoryStream memStream;
                //XmlTextWriter xw;
                XDocument envDoc = BuildRequestEnvelope(requestGuid);
                string envelopeString = envDoc.Declaration + envDoc.ToString();

                //// Serialize AIHQuote into string
                //XmlSerializer xs = new XmlSerializer(typeof(ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument));
                //memStream = new MemoryStream();
                //xw = new XmlTextWriter(strMessageIs, Encoding.UTF8);
                //xs.Serialize(xw, ClaimsDocument);

                //memStream = (MemoryStream)xw.BaseStream;
                //string aihQuoteString = EncodeToBase64(UTF8ByteArrayToString(memStream.ToArray()));
                //xw.Close();

                // Combine everything into a complete message string
                DateTime today = DateTime.Today;
                string boundary = string.Format("----=_Part_{0}_{1}.{2}", today.Day, today.ToFileTime(), today.Ticks);
                string multiPartHeader = "Content-Type: multipart/related;\r\n\tboundary=\"" + boundary + "\"";
                string singlePartHeader = "--" + boundary + "\r\nContent-Type: text/xml\r\nContent-Transfer-Encoding: 8bit";
                string dataHeader = "--" + boundary + "\r\nContent-Type: application/ids\r\nContent-Transfer-Encoding: base64\r\nContent-ID: DATA";
                string msgString = string.Format("{0}\r\n\r\n{1}\r\n\r\n{2}\r\n{3}\r\n\r\n{4}\r\n{5}",
                    multiPartHeader, singlePartHeader, envelopeString, dataHeader, strMessageIs, boundary);

                // Stream the complete string into the message
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(msgString);
                memStream = new MemoryStream(bytes);
                //msg.Label = CDSupport.DocumakerRequestQueue + @"\DocuSend";
                msg.Label = requestGuid.ToString();
                msg.BodyStream = memStream;

                sendQ.Send(msg);
                //sendQ.Close();
            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : SendRequest() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;

                blnResult = false;
            }
            finally
            {
            }

            if (blnResult == true)
            {
                return requestGuid.ToString("D");
            }
            else
            {
                return ("Send Request Failed...");
            }
        }


        #region Utilities

        private XDocument BuildRequestEnvelope(Guid guid)
        {
            XDocument doc = new XDocument();
            doc.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

            XNamespace ns = @"http://schemas.xmlsoap.org/soap/envelope/";

            XElement root = new XElement(ns + "Envelope");

            // Include namespace in the document
            root.Add(new XAttribute(XNamespace.Xmlns + "SOAP-ENV", ns));

            XElement body = new XElement(ns + "Body");
            body.Add(BuildRequestBody(guid).Root);
            root.Add(body);

            doc.Add(root);
            return doc;
        }

        private XDocument BuildRequestBody(Guid guid)
        {
            XDocument body = new XDocument(
                new XElement("DSIMSG",
                    new XAttribute("VERSION", "100.022.0"),
                    new XElement("CTLBLOCK",
                        new XElement("UNIQUE_ID", guid.ToString("D")),
                        new XElement("REQTYPE", "RPD"),
                        new XElement("ATTACHMENT",
                            new XAttribute("TYPE", "BINARY"),
                            new XElement("DELIMITER", "DATA")
                        )
                    ),
                    new XElement("MSGVARS",
                        new XElement("VAR",
                            new XAttribute("NAME", "CLAIMNUMBER"),
                            this._claimsNumber
                        ),
                        new XElement("VAR",
                            new XAttribute("NAME", "CONFIG"),
                            CDSupport.DocumakerEnvironment
                        ),
                        new XElement("VAR",
                            new XAttribute("NAME", "REQTYPE"),
                            "RPD"
                        ),

                        new XElement("VAR",
                            new XAttribute("NAME", "TransactionCode"),
                            this._documentDefinitionName
                        ),

                        new XElement("VAR",
                            new XAttribute("NAME", "userid"),
                            "docucorp"
                        )
                    )
                )
            );

            return body;
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();

            String constructedString = encoding.GetString(characters);

            return (constructedString);
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        private Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();

            Byte[] byteArray = encoding.GetBytes(pXmlString);

            return byteArray;
        }

        public string EncodeToBase64(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }

        public string DecodeFromBase64(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }

        #endregion
    }

    public abstract class DocumentReceiverBase
    {
        protected string documentKey = null;

        protected internal DocumentReceiverBase(string documentKey)
        {
            //declare variables
            try
            {
                //assign document key
                this.documentKey = documentKey;
            }
            catch (Exception ex)
            {
                //handle error
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DocumentReceiverBase() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
            }
        }

        public static DocumentReceiverBase GetInstance(string documentKey, DocumentGeneratorType docGen)
        {
            //declare variables
            DocumentReceiverBase objDocumentReceiverBase = null;

            try
            {
                switch (docGen)
                {
                    case DocumentGeneratorType.Documaker:
                        objDocumentReceiverBase = new DocumentReceiverDocumaker(documentKey);
                        break;
                    default:
                        throw new NotImplementedException(string.Format("Unable to create object for the ClaimsDocs document generator receiver '{0}'.", docGen));
                }
            }
            catch (Exception ex)
            {
                //handle error                
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : GetInstance() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
            }

            //return result
            return (objDocumentReceiverBase);

        }

        public abstract List<DocuMakerClaimsDocsResponse> ReceiveResponse(ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument ClaimsDocument);

    }

    public class DocumentReceiverDocumaker : DocumentReceiverBase
    {
        // Make the constructor internal so only the base class can make it
        internal DocumentReceiverDocumaker(string documentKey)
            : base(documentKey)
        {
        }

        public override List<DocuMakerClaimsDocsResponse> ReceiveResponse(ClaimsDocsBizLogic.SchemaClasses.ClaimsDocument ClaimsDocument)
        {
            //declare variables
            Guid g;
            string docPath = "";
            bool blnDocPathFound = false;
            string strPrinterName = "";
            XElement docPathElem = null;
            List<DocuMakerClaimsDocsResponse> listDocuMakerCDResponse = new List<DocuMakerClaimsDocsResponse>();

            try
            {
                g = new Guid(this.documentKey);
                XDocument doc = GetMessageById(g);

                if (doc == null)
                {
                    //CDSupport.LogExceptionToFile("ReceiveResponse", null, "GetMessageById(g)  : Returned a NULL value" , false);
                    throw new ApplicationException("Failed to parse ClaimsDocs XML document from response message.");
                }

                //get document copies based on request


                //*********** ALWAYS GET FILE COPY FROM PRINTER6
                DocuMakerClaimsDocsResponse objFileCopyResponse = new DocuMakerClaimsDocsResponse();
                strPrinterName = "PRINTER6";
                docPathElem = doc.Descendants("VAR").FirstOrDefault(elem => elem.Attribute("NAME").Value.ToUpper().Equals(strPrinterName));
                if (docPathElem != null)
                {
                    //assign receipent type
                    objFileCopyResponse.ReceipientType = "FILE COPY";
                    //assign printer
                    objFileCopyResponse.PrinterName = "PRINTER6";
                    //indicate that the document was generated
                    objFileCopyResponse.DocumentGenerated = true;
                    //get the document path and file name
                    docPath = docPathElem.Value;
                    int colonPos = docPath.IndexOf(':');
                    if (colonPos >= 0)
                    {
                        docPath = CDSupport.DocumakerUNCRoot + docPath.Substring(colonPos + 1);
                    }
                    objFileCopyResponse.DocumentPathAndFileName = docPath;
                    
                    //indicate success
                    blnDocPathFound = true;                   
                }
                else
                {
                    //indicate that the document was not generated
                    objFileCopyResponse.DocumentGenerated = false;
                    //indicate failure
                    blnDocPathFound = false;
                }

                //add to list
                listDocuMakerCDResponse.Add(objFileCopyResponse);

                //********** GET INSURED COPY UPON REQUEST FROM PRINTER1
                if ((blnDocPathFound==true) && ClaimsDocument.DocumentDefinition.CopyToInsured.ToUpper().Equals("Y"))
                {
                    DocuMakerClaimsDocsResponse objInsuredCopyResponse = new DocuMakerClaimsDocsResponse();
                    strPrinterName = "PRINTER1";
                    docPathElem = doc.Descendants("VAR").FirstOrDefault(elem => elem.Attribute("NAME").Value.ToUpper().Equals(strPrinterName));
                    if (docPathElem != null)
                    {
                        //assign receipent type
                        objInsuredCopyResponse.ReceipientType = "INSURED COPY";
                        //assign printer
                        objInsuredCopyResponse.PrinterName = "PRINTER1";
                        //indicate that the document was generated
                        objInsuredCopyResponse.DocumentGenerated = true;
                        //get the document path and file name
                        docPath = docPathElem.Value;
                        int colonPos = docPath.IndexOf(':');
                        if (colonPos >= 0)
                        {
                            docPath = CDSupport.DocumakerUNCRoot + docPath.Substring(colonPos + 1);
                        }
                        objInsuredCopyResponse.DocumentPathAndFileName = docPath;

                        //indicate success
                        blnDocPathFound = true;
                    }
                    else
                    {
                        //indicate that the document was not generated
                        objInsuredCopyResponse.DocumentGenerated = false;
                        //indicate failure
                        blnDocPathFound = false;                       
                    }

                    //add to list
                    listDocuMakerCDResponse.Add(objInsuredCopyResponse);
                }

                //********** GET PRODUCER/AGENT COPY UPON REQUEST FROM PRINTER2
                if ((blnDocPathFound == true) && ClaimsDocument.DocumentDefinition.CopyToAgent.ToUpper().Equals("Y"))
                {
                    DocuMakerClaimsDocsResponse objProducerCopyResponse = new DocuMakerClaimsDocsResponse();
                    strPrinterName = "PRINTER2";
                    docPathElem = doc.Descendants("VAR").FirstOrDefault(elem => elem.Attribute("NAME").Value.ToUpper().Equals(strPrinterName));
                    if (docPathElem != null)
                    {
                        //assign receipent type
                        objProducerCopyResponse.ReceipientType = "PRODUCER COPY";
                        //assign printer
                        objProducerCopyResponse.PrinterName = "PRINTER2";
                        //indicate that the document was generated
                        objProducerCopyResponse.DocumentGenerated = true;
                        //get the document path and file name
                        docPath = docPathElem.Value;
                        int colonPos = docPath.IndexOf(':');
                        if (colonPos >= 0)
                        {
                            docPath = CDSupport.DocumakerUNCRoot + docPath.Substring(colonPos + 1);
                        }
                        objProducerCopyResponse.DocumentPathAndFileName = docPath;

                        //indicate success
                        blnDocPathFound = true;
                    }
                    else
                    {
                        //indicate that the document was not generated
                        objProducerCopyResponse.DocumentGenerated = false;
                        //indicate failure
                        blnDocPathFound = false;
                    }
                    //add to list
                    listDocuMakerCDResponse.Add(objProducerCopyResponse);
                }

                //********** GET LIEN HOLDER COPY UPON REQUEST FROM PRINTER3
                if ((blnDocPathFound == true) && ClaimsDocument.DocumentDefinition.CopyToLeinholder.ToUpper().Equals("Y"))
                {
                    DocuMakerClaimsDocsResponse objLienHodlerCopyResponse = new DocuMakerClaimsDocsResponse();
                    strPrinterName = "PRINTER3";
                    docPathElem = doc.Descendants("VAR").FirstOrDefault(elem => elem.Attribute("NAME").Value.ToUpper().Equals(strPrinterName));
                    if (docPathElem != null)
                    {
                        //assign receipent type
                        objLienHodlerCopyResponse.ReceipientType = "LIEN HOLDER COPY";
                        //assign printer
                        objLienHodlerCopyResponse.PrinterName = "PRINTER3";
                        //indicate that the document was generated
                        objLienHodlerCopyResponse.DocumentGenerated = true;
                        //get the document path and file name
                        docPath = docPathElem.Value;
                        int colonPos = docPath.IndexOf(':');
                        if (colonPos >= 0)
                        {
                            docPath = CDSupport.DocumakerUNCRoot + docPath.Substring(colonPos + 1);
                        }
                        objLienHodlerCopyResponse.DocumentPathAndFileName = docPath;

                        //indicate success
                        blnDocPathFound = true;
                    }
                    else
                    {
                        //indicate that the document was not generated
                        objLienHodlerCopyResponse.DocumentGenerated = false;
                        //indicate failure
                        blnDocPathFound = false;
                    }

                    //add to list
                    listDocuMakerCDResponse.Add(objLienHodlerCopyResponse);
                }

                //********** GET FINANCE COPY UPON REQUEST FROM PRINTER4
                if ((blnDocPathFound == true) && ClaimsDocument.DocumentDefinition.CopyToFinanceCompany.ToUpper().Equals("Y"))
                {
                    DocuMakerClaimsDocsResponse objFinanceCompanyCopyResponse = new DocuMakerClaimsDocsResponse();
                    strPrinterName = "PRINTER4";
                    docPathElem = doc.Descendants("VAR").FirstOrDefault(elem => elem.Attribute("NAME").Value.ToUpper().Equals(strPrinterName));
                    if (docPathElem != null)
                    {
                        //assign receipent type
                        objFinanceCompanyCopyResponse.ReceipientType = "FINANCE COMPANY COPY";
                        //assign printer
                        objFinanceCompanyCopyResponse.PrinterName = "PRINTER4";
                        //indicate that the document was generated
                        objFinanceCompanyCopyResponse.DocumentGenerated = true;
                        //get the document path and file name
                        docPath = docPathElem.Value;
                        int colonPos = docPath.IndexOf(':');
                        if (colonPos >= 0)
                        {
                            docPath = CDSupport.DocumakerUNCRoot + docPath.Substring(colonPos + 1);
                        }
                        objFinanceCompanyCopyResponse.DocumentPathAndFileName = docPath;

                        //indicate success
                        blnDocPathFound = true;
                    }
                    else
                    {
                        //indicate that the document was not generated
                        objFinanceCompanyCopyResponse.DocumentGenerated = true;
                        //indicate failure
                        blnDocPathFound = false;
                    }

                    //add to list
                    listDocuMakerCDResponse.Add(objFinanceCompanyCopyResponse);
                }

                //********** GET ATTORNEY COPY UPON REQUEST FROM PRINTER5
                if ((blnDocPathFound == true) && ClaimsDocument.DocumentDefinition.CopyToAttorney.ToUpper().Equals("Y"))
                {
                    DocuMakerClaimsDocsResponse objAttorneyCopyResponse = new DocuMakerClaimsDocsResponse();
                    strPrinterName = "PRINTER5";
                    docPathElem = doc.Descendants("VAR").FirstOrDefault(elem => elem.Attribute("NAME").Value.ToUpper().Equals(strPrinterName));
                    if (docPathElem != null)
                    {
                        //assign receipent type
                        objAttorneyCopyResponse.ReceipientType = "ATTORNEY COPY";
                        //assign printer
                        objAttorneyCopyResponse.PrinterName = "PRINTER5";
                        //indicate that the document was generated
                        objAttorneyCopyResponse.DocumentGenerated = true;
                        //get the document path and file name
                        docPath = docPathElem.Value;
                        int colonPos = docPath.IndexOf(':');
                        if (colonPos >= 0)
                        {
                            docPath = CDSupport.DocumakerUNCRoot + docPath.Substring(colonPos + 1);
                        }
                        objAttorneyCopyResponse.DocumentPathAndFileName = docPath;

                        //indicate success
                        blnDocPathFound = true;
                    }
                    else
                    {
                        //indicate that the document was not generated
                        objAttorneyCopyResponse.DocumentGenerated = false;
                        //indicate failure
                        blnDocPathFound = false;
                    }

                    //add to list
                    listDocuMakerCDResponse.Add(objAttorneyCopyResponse);
                }


                //check results
                if (blnDocPathFound == false)
                {
                    //return null list if any of the required document
                    //copies could not be generated
                    listDocuMakerCDResponse = null;
                    throw new ApplicationException("Failed to get ClaimsDocs document path from response.");
                }
            }
            catch (ApplicationException ex)
            {
                //Log exception to file
                CDSupport.LogExceptionToFile("ReceiveResponse", ex, "GetMessageById(g)  : Returned a NULL value", false);

                //handle error                
                listDocuMakerCDResponse = null;
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : ReceiveResponse() ";
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
            //return result
            return (listDocuMakerCDResponse);
        }

        /// <summary>
        /// Gets the response message matching the request message's ID.
        /// The message returned is only the XML portion (the portion we care about).
        /// </summary>
        /// <param name="g">the GUID of the request message</param>
        /// <returns>the response message, or null if not found</returns>
        private XDocument GetMessageById(Guid g)
        {
            //declare variables
            //MessageQueue receiveQ = new MessageQueue(Settings.Default.DocumakerReceiveQueue, QueueAccessMode.Receive);
            MessageQueue receiveQ = new MessageQueue(CDSupport.DocumakerReplyQueue, QueueAccessMode.Receive);
            DateTime beforeChecking = DateTime.Now;
            Message msg = null;
            bool messageFound = false;
            MessageEnumerator msgEnumerator = null;
            XDocument doc = null;
            int intWhileLoopCount = 0;
            int messageQueueLooptries = CDSupport.MessageQueueLoopTries;

            try
            {
                //attempt 10 times
                while (intWhileLoopCount < messageQueueLooptries)
                {
                    //increment
                    intWhileLoopCount+=1;
                    //get message queue enumerator
                    msgEnumerator = receiveQ.GetMessageEnumerator2();

                    // Keep looping until we find a message with UNIQUE_ID = request ID
                    while (msgEnumerator.MoveNext())
                    {
                        //get current message
                        msg = msgEnumerator.Current;

                        //check current message
                        if (msg != null)
                        {
                            //get message body
                            doc = GetMessageBody(msg);

                            //check message body
                            if (doc != null)
                            {
                                //get document unique id
                                XElement id = doc.Descendants("UNIQUE_ID").FirstOrDefault();

                                //check for id and compare message id against guid
                                if (id != null && (new Guid(id.Value)).Equals(g))
                                {
                                    //message id's match so remove from queue
                                    msg = msgEnumerator.RemoveCurrent();
                                    messageFound = true;
                                    break;
                                }
                                else
                                {
                                    //CDSupport.LogExceptionToFile("Message NOT found ", null, "GetMessageById(g)  ", false);
                                }
                            }
                            else
                            {
                                //CDSupport.LogExceptionToFile("doc==null ", null, "GetMessageById(g)  ", false);
                            }
                        }
                        else
                        {
                            //CDSupport.LogExceptionToFile("msg == null " , null, "GetMessageById(g)  ", false);
                        }
                    }

                    //break out of loop when the
                    //message is found
                    if (messageFound)
                        break;
                    
                    //wait 5 seconds
                    Thread.Sleep(3 * 1000);
                    msgEnumerator.Close();
                }
            }
            catch (Exception ex)
            {

                CDSupport.LogExceptionToFile("GetMessageById==>Exception Is : " + ex.ToString(), ex, "GetMessageById(g)  ", false);

                //handle error                
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : GetMessageById() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
            }

            if (msgEnumerator != null)
                msgEnumerator.Close();

            if (!messageFound)
            {
                throw new ApplicationException("Timed out while retrieving response from ClaimsDocs  Generator.");
            }

            //return result
            return doc;
        }

        /// <summary>
        /// Gets the body of a message as an XDocument (to allow for LINQ-To-XML queries).
        /// </summary>
        /// <param name="msg">MSMQ message</param>
        /// <returns>an XDocument containing the message body</returns>
        private XDocument GetMessageBody(Message msg)
        {
            if (msg == null)
                return null;

            // The body of the message
            string body = null;

            using (StreamReader reader = new StreamReader(msg.BodyStream))
            {
                body = reader.ReadToEnd();
            }

            if (string.IsNullOrEmpty(body))
                return null;

            // Log the message body we just received
            //EntLogger.LogAsync(body, "DocuMaker Response", TraceEventType.Information, null);

            // Cut off the initial "non-Xml" portion of the body
            //      So we can convert the rest into XDocument
            int firstTagPos = body.IndexOf('<');
            if (firstTagPos >= 0)
                body = body.Substring(firstTagPos);

            XDocument doc = null;

            try
            {
                doc = XDocument.Parse(body);
            }
            catch (Exception ex)
            {
                //handle error                
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : ClaimsDocsGenerateDocument() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
                return null;
            }

            return doc;
        }
    }

    #region Document Info

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accessgeneral.com/schemas/DocumentGeneration/DocumentInfoV1.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.accessgeneral.com/schemas/DocumentGeneration/DocumentInfoV1.0", IsNullable = false)]
    public partial class DocumentInfo
    {

        private string docKeyField;

        private string docPathField;

        private string docXMLPathField;

        private string docURLField;

        private string docTypeField;

        private string docDateField;

        private string transTypeField;

        private string drawerField;

        private string fileNumberField;

        private DocumentInfoVar[] varsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocKey
        {
            get
            {
                return this.docKeyField;
            }
            set
            {
                this.docKeyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocPath
        {
            get
            {
                return this.docPathField;
            }
            set
            {
                this.docPathField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocXMLPath
        {
            get
            {
                return this.docXMLPathField;
            }
            set
            {
                this.docXMLPathField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocURL
        {
            get
            {
                return this.docURLField;
            }
            set
            {
                this.docURLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocType
        {
            get
            {
                return this.docTypeField;
            }
            set
            {
                this.docTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DocDate
        {
            get
            {
                return this.docDateField;
            }
            set
            {
                this.docDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TransType
        {
            get
            {
                return this.transTypeField;
            }
            set
            {
                this.transTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Drawer
        {
            get
            {
                return this.drawerField;
            }
            set
            {
                this.drawerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FileNumber
        {
            get
            {
                return this.fileNumberField;
            }
            set
            {
                this.fileNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("Var", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public DocumentInfoVar[] Vars
        {
            get
            {
                return this.varsField;
            }
            set
            {
                this.varsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.accessgeneral.com/schemas/DocumentGeneration/DocumentInfoV1.0")]
    public partial class DocumentInfoVar
    {

        private string nameField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    #endregion

    #region Utility

    public class Utility
    {
        public List<DocuMakerClaimsDocsResponse> CallDocumentGeneration(ClaimsDocument objClaimsDocument, string claimsNumber, string documentDefinitionName)
        {
            //declare variables
            string docGenQuoteStr;
            ClaimsDocument docClaimsDocument;
            DocumentInfo info = new DocumentInfo();
            DocumentGenerator objDocumentGenerator = new DocumentGenerator();
            List<DocuMakerClaimsDocsResponse> listDocuMakerClaimsDocsResponse = null;

            try
            {
                docGenQuoteStr = Utility.Serialize(objClaimsDocument, CDSupport.ClaimsDocsNameSpace);
                docClaimsDocument = Utility.DeSerialize<ClaimsDocument>(docGenQuoteStr, CDSupport.ClaimsDocsNameSpace);
                listDocuMakerClaimsDocsResponse = objDocumentGenerator.GenerateDocumentSingle(docClaimsDocument, claimsNumber, documentDefinitionName);
            }
            catch (Exception ex)
            {
                //handle error                
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : CallDocumentGeneration() ";
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

            //return information
            return (listDocuMakerClaimsDocsResponse);
        }

        public static string Serialize(object o, string nameSpace)
        {
            StringWriter output = new StringWriter(new StringBuilder());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            XmlDocument doc = new XmlDocument();

            try
            {
                if (!string.IsNullOrEmpty(nameSpace))
                    ns.Add("", CDSupport.ClaimsDocsNameSpace);
                else
                    ns.Add("", "");

                XmlSerializer ser = new XmlSerializer(o.GetType());
                ser.Serialize(output, o, ns);


                doc.LoadXml(output.ToString());

                if (doc.DocumentElement.Attributes["xmlns"] == null)
                {
                    XmlAttribute attr = doc.CreateAttribute("xmlns");
                    attr.InnerText = nameSpace;
                    doc.DocumentElement.Attributes.Append(attr);
                }
            }
            catch (Exception ex)
            {
                //handle error                
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : Serialize() ";
                objClaimsLog.ExceptionIs = ex.Message;
                objClaimsLog.StackTraceIs = ex.StackTrace;
                objClaimsLog.IUDateTime = DateTime.Now;
                //create log record
                objCDSupport.ClaimsDocsLogCreate(objClaimsLog, CDSupport.ClaimsDocsDBConnectionString);

                //cleanup
                objClaimsLog = null;
                objCDSupport = null;
            }

            //return result
            return doc.InnerXml;
        }

        public static T DeSerialize<T>(string xml, string nameSpace)
        {
            XmlSerializer ser = null;
            StringReader input = null;

            try
            {
                if (!string.IsNullOrEmpty(nameSpace))
                    ser = new XmlSerializer(typeof(T), CDSupport.ClaimsDocsNameSpace);
                else
                    ser = new XmlSerializer(typeof(T));
                input = new StringReader(xml);
            }
            catch (Exception ex)
            {
                //handle error                
                ClaimsDocsLog objClaimsLog = new ClaimsDocsLog();
                CDSupport objCDSupport = new CDSupport();
                //fill log
                objClaimsLog.ClaimsDocsLogID = 0;
                objClaimsLog.LogTypeID = 3;
                objClaimsLog.LogSourceTypeID = 1;
                objClaimsLog.MessageIs = "Method : DeSerialize() ";
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

            //return result
            return (T)(ser.Deserialize(input));
        }

    }

    #endregion

    #region DocuMaker ClaimsDocs Response


    #endregion

}
