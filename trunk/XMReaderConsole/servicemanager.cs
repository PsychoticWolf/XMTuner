using System;
using System.ServiceProcess;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Configuration.Install;

namespace XMTuner
{
    class servicemanager
    {
        String serName;
        String serDesc;
        String serDisp;
        String context = Application.StartupPath;

        public servicemanager(String serviceName, String serviceDescription, String serviceDisplay)
        {
            serName = serviceName;
            serDesc = serviceDescription;
            serDisp = serviceDisplay;

        }

        public bool Install(ServiceStartMode serviceMode)
        {
            ServiceProcessInstaller PSI = new ServiceProcessInstaller();
            ServiceInstaller SIO = new ServiceInstaller();

            String path =String.Format("/assemblypath={0}",context + "\\XMTunerService.exe");
            String[] cmdline = { path };

            InstallContext iContext = new InstallContext("", cmdline);

            SIO.Context = iContext;
            SIO.DisplayName = serDisp;
            SIO.Description = serDesc;
            SIO.ServiceName = serName;
            SIO.StartType = serviceMode;
            SIO.Parent = PSI;

            ListDictionary state = new ListDictionary();

            SIO.Install(state);

            return true;
        }

        public bool Uninstall()
        {
            ServiceInstaller SIO = new ServiceInstaller();
            InstallContext iContext = new InstallContext("", null);

            SIO.Context = iContext;
            SIO.ServiceName = serName;
            SIO.Uninstall(null);

            return true;
        }
    }
}
