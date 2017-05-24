using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Access.WindowsServices.APPS.Monitor
{
    [RunInstaller(true)]
    public partial class MonitorInstaller : Installer
    {
        private ServiceInstaller serviceInstaller;
        private ServiceProcessInstaller serviceProcessInstaller;

        public MonitorInstaller()
        {
            InitializeComponent();
        }
    }
}