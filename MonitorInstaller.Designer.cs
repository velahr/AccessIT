using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Access.WindowsServices.APPS.Monitor
{
    partial class MonitorInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();

            this.serviceInstaller.DisplayName = "APPS.Monitor";
            this.serviceInstaller.ServiceName = "APPS.Monitor";
            this.serviceInstaller.Description = "Monitors the health of the Access Payment Processing System (APPS).";

            this.serviceInstaller.StartType = ServiceStartMode.Automatic;

            this.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller.Password = null;
            this.serviceProcessInstaller.Username = null;

            this.Installers.AddRange(new System.Configuration.Install.Installer[] { this.serviceProcessInstaller, this.serviceInstaller });
        }

        #endregion
    }
}