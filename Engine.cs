using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Diagnostics;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
/* *** */
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Access.WindowsServices.APPS.Monitor 
{

    /// <summary>
    /// Represents the monitor engine. 
    /// </summary>
    public class Engine
    {
        private DataSet processingInfo;
        private Database db;
        private const string applicationSource = "APPS.Monitor";

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        public Engine()
        {
            this.initialize();
        }

        private void initialize()
        {
            this.processingInfo = null;
            this.db = DatabaseFactory.CreateDatabase();
        }

        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="title">Additional description of the log entry message.</param>
        /// <param name="severity">Log entry as a Microsoft.Practices.EnterpriseLibrary.Logging.LogEntry.Severity enumeration.</param>
        /// <param name="message">The body of the message.</param>
        /// <param name="dic">The dictionary collection of extended properties.</param>
        /// <param name="processName">Name of the current running process.</param>
        internal static void LogException(string title, TraceEventType severity, string message, Dictionary<string, object> dic, string processName)
        {
            LogEntry log = new LogEntry();
            log.Title = title;
            log.Severity = severity;
            log.Message = message;
            log.ProcessName = processName;

            if (dic != null)
            {
                log.ExtendedProperties = dic;
            }

            Logger.Write(log);
        }

        /// <summary>
        /// Retrieves all payment transactions that have not been processed by the APPS Monitor.
        /// </summary>
        private void retrieveProcessingInfo()
        {
            string sqlCommand = "genesis.dbo.sp_apps_get_processing_log_info";      /* store procedure */

            DbCommand dbCommand = this.db.GetStoredProcCommand(sqlCommand);
            this.processingInfo = db.ExecuteDataSet(dbCommand);                     /* query the database and put the results in a dataset */

            processingInfo.Tables[0].TableName = "processingInfo";                  /* rename table for easier accessability */
        }

        /// <summary>
        /// Calculates the average time in seconds for each transaction time.
        /// </summary>
        /// <param name="table">The datatable containing the payment transaction times.</param>
        /// <param name="column">The datacolumn containing a specific transaction time.</param>
        /// <returns>The average time in seconds.</returns>
        private double averageTime(DataTable table, DataColumn column)
        {
            double average = 0;
            double total = 0;
            int nullCount = 0;

            foreach (DataRow row in table.Rows)
            {
                row["processed_by_monitor"] = true;

                if (row[column.ColumnName] == DBNull.Value)
                {
                    nullCount++;
                    continue;
                }
                total += double.Parse(row[column.ColumnName].ToString());
            }
         
            average = total / (table.Rows.Count - nullCount);

            return average;
        }

        /// <summary>
        /// Calculate the payment transaction times.  Returns the payment transaction times if they exceeded their thresholds. 
        /// cc_transaction_time represents the time in seconds it took to retreive a response from Payment Data Systems (PDS).
        /// save_transaction_time represent the time in seconds it took to retreive a response from the ePayLog. 
        /// total_transaction_time is the sum of cc_transaction_time and save_transaction time. 
        /// </summary>
        /// <param name="arrayList">The array of transaction times and thresholds.</param>
        /// <returns>Indicates whether or not if any of the payment transaction times exceeded their thresholds.</returns>
        private bool calculateProcessingTimes(out string[,] arrayList)
        {
            int listEntries = -1;
            //string[,] 
            
            arrayList = new string[3, 3];

            double totalTransactionTime = 0,
                ccTransactionTime = 0,
                saveTransactionTime = 0;

            /* calculate total transaction time */
            totalTransactionTime = this.averageTime(this.processingInfo.Tables["processingInfo"], this.processingInfo.Tables["processingInfo"].Columns["total_transaction_time"]);

            /* calculate CC transaction time */
            ccTransactionTime = this.averageTime(this.processingInfo.Tables["processingInfo"], this.processingInfo.Tables["processingInfo"].Columns["cc_transaction_time"]);

            /* calculate save transaction time */
            saveTransactionTime = this.averageTime(this.processingInfo.Tables["processingInfo"], this.processingInfo.Tables["processingInfo"].Columns["save_transaction_time"]);

            if (totalTransactionTime > double.Parse(ConfigurationManager.AppSettings["total_transaction_time.threshold"]))
            {
                listEntries++;

                arrayList[listEntries, 0] = "Total Transaction Time";
                arrayList[listEntries, 1] = ConfigurationManager.AppSettings["total_transaction_time.threshold"];
                arrayList[listEntries, 2] = ((int)totalTransactionTime).ToString();
            }

            if (ccTransactionTime > double.Parse(ConfigurationManager.AppSettings["cc_transaction_time.threshold"]))
            {
                listEntries++;

                arrayList[listEntries, 0] = "CC Transaction Time";
                arrayList[listEntries, 1] = ConfigurationManager.AppSettings["cc_transaction_time.threshold"];
                arrayList[listEntries, 2] = ((int)ccTransactionTime).ToString();
            }

            if (saveTransactionTime > double.Parse(ConfigurationManager.AppSettings["save_transaction_time.threshold"]))
            {
                listEntries++;

                arrayList[listEntries, 0] = "Save Transaction Time";
                arrayList[listEntries, 1] = ConfigurationManager.AppSettings["save_transaction_time.threshold"];
                arrayList[listEntries, 2] = ((int)saveTransactionTime).ToString();
            }

            return (listEntries > 0);
        }

        /// <summary>
        /// Sends an e-mail notification to the specified recipient if any payment transaction exceeded its set threshold.
        /// </summary>
        /// <param name="list">The list of transaction payment times and thresholds.</param>
        private void sendNotification(string[,] list)
        {

            string mailServer   = ConfigurationManager.AppSettings["mail.server"] != null ? ConfigurationManager.AppSettings["mail.server"] : "corpmail.accessgeneral.com",
                mailSender      = ConfigurationManager.AppSettings["mail.sender"] != null ? ConfigurationManager.AppSettings["mail.sender"] : "apps.monitor@accessgeneral.com",
                mailRecipient   = ConfigurationManager.AppSettings["mail.recipient"] != null ? ConfigurationManager.AppSettings["mail.recipient"] : "itdev@accessgeneral.com",
                mailSubject     = ConfigurationManager.AppSettings["mail.subject"] != null ? ConfigurationManager.AppSettings["mail.subject"] : "Web Payments Alert: The systems is not performing within the configured threshold/s",
                mailBody        = string.Empty;

            /* form the body */
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\r\nThe following transaction/s is/are taking longer than the specified threshold/s");
            sb.AppendLine("\r\nTransaction Name              - Specified Thresholds(*) - Current Avg. Time(*)");

            int len = list.Length / 3;

            for (int c = 0; c < len; c++)
            {
                if (list[c, 0] == null)
                    continue;
                sb.AppendLine(string.Format("\r\n{0}-{1}-{2}", list[c, 0].PadRight(30), " " + list[c, 1].PadRight(24), " " + list[c, 2].PadRight(20)));
            }

            sb.AppendLine();
            sb.AppendLine("\r\n[*] - Time specified in seconds.");

            mailBody = sb.ToString();

            /* create mail message object */
            MailMessage email = new MailMessage(mailSender, mailRecipient, mailSubject, mailBody);

            SmtpClient client = new SmtpClient(mailServer);
            client.Send(email);

            LogException("Web Payments Alert: The systems is not performing within the configured threshold(s)", TraceEventType.Warning, sb.ToString(), null, applicationSource);

        }

        /// <summary>
        /// Updates payment transaction time records as being processed by the APPS Monitor. 
        /// </summary>
        /// <param name="table">The datatable containing the payment transaction times.</param>
        private void updateProcessedTransactions(DataTable table)
        {
            DbCommand updateCmd = this.db.GetStoredProcCommandWithSourceColumns("sp_apps_update_processed_payment", "transaction_id", "processed_by_monitor");
            this.db.UpdateDataSet(this.processingInfo, "processingInfo", null, updateCmd, null, UpdateBehavior.Continue);
        }

        /// <summary>
        /// Starts the APPS monitor engine. 
        /// </summary>
        public void Start()
        {
            string[,] outOfRange;

            try
            {
                this.retrieveProcessingInfo();

                if (this.calculateProcessingTimes(out outOfRange))
                    this.sendNotification(outOfRange);

                if (this.processingInfo.Tables["processingInfo"].GetChanges() == null ? false: true )
                    this.updateProcessedTransactions(this.processingInfo.Tables["processingInfo"]);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Exception.Source", ex.Source);
                dic.Add("Exception.Message", ex.Message);
                dic.Add("Exception.StackTrace", ex.StackTrace);

                if (ex.InnerException != null)
                    dic.Add("Exception.InnerException.Message", ex.InnerException.Message);

                LogException("APPS.Monitor encountered an error", TraceEventType.Critical, string.Empty, dic, applicationSource);
            }
        }
    }
}

/* 
 * Appendix
 * CC = Credit Card 
 */