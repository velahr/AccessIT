using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace Access.WindowsServices.APPS.Monitor
{
    /// <summary>
    /// Represents the service monitor class.
    /// </summary>
    partial class Monitor : ServiceBase
    {
        private System.Timers.Timer timer = null;
        private Engine checker = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Monitor"/> class.
        /// Sets the the interval specified by the poll interval setting.
        /// </summary>
        public Monitor()
        {
            InitializeComponent();

            double interval = 10000;
            try
            {
                string servicepollinterval = ConfigurationManager.AppSettings["monitor.pollInterval"];
                interval = Convert.ToDouble(servicepollinterval) * 1000;

                this.checker = new Engine();

            }
            catch (Exception) { }

            timer = new System.Timers.Timer(interval);
            timer.Elapsed += new ElapsedEventHandler(this.timer_Elapsed);

        }

        /// <summary>
        /// Starts the APPS monitor engine every interval specified by the poll interval setting.
        /// Handles the Elapsed event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.timer.Stop();

            checker.Start();

            this.timer.Start();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            this.timer.AutoReset = true;
            this.timer.Enabled = true;
            this.timer.Start();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            this.timer.AutoReset = false;
            this.timer.Enabled = false;
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Pause command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service pauses.
        /// </summary>
        protected override void OnPause()
        {
            this.timer.Stop();
        }

        /// <summary>
        /// When implemented in a derived class, <see cref="M:System.ServiceProcess.ServiceBase.OnContinue"/> runs when a Continue command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service resumes normal functioning after being paused.
        /// </summary>
        protected override void OnContinue()
        {
            this.timer.Start();
        }


    }
}
