using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;


namespace XMTuner
{
    [RunInstaller(true)]
    //[RunInstallerAttribute(true)]

    public class XMServiceInstaller : Installer
    {
        /// <summary>

        /// Public Constructor for WindowsServiceInstaller.

        /// - Put all of your Initialization code here.

        /// </summary>

        public XMServiceInstaller()
        {
            //InitializeComponent();

            ServiceProcessInstaller serviceProcessInstaller = 
                               new ServiceProcessInstaller();
            ServiceInstaller serviceInstall = new ServiceInstaller();

            
            //# Service Account Information

            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information

            serviceInstall.DisplayName = "XMTuner Service";
            serviceInstall.StartType = ServiceStartMode.Automatic;
            serviceInstall.Context = new System.Configuration.Install.InstallContext();
            

            //# This must be identical to the WindowsService.ServiceBase name

            //# set in the constructor of WindowsService.cs

            serviceInstall.ServiceName = "XMTunerService";
            serviceInstall.Description = "Listen to XMRO via UPnP";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstall);
        }

    }
}
