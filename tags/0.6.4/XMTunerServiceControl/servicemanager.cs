/*
 * XMTuner: Copyright (C) 2009-2012 Chris Crews and Curtis M. Kularski.
 * 
 * This file is part of XMTuner.

 * XMTuner is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * XMTuner is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with XMTuner.  If not, see <http://www.gnu.org/licenses/>.
 */

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

        ServiceInstaller SIO = new ServiceInstaller();

        public servicemanager(String serviceName)
        {
            serName = serviceName;
        }

        public servicemanager(String serviceName, String serviceDescription, String serviceDisplay)
        {
            serName = serviceName;
            serDesc = serviceDescription;
            serDisp = serviceDisplay;

        }

        public bool Install(ServiceStartMode serviceMode)
        {
            ServiceProcessInstaller PSI = new ServiceProcessInstaller();

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
            InstallContext iContext = new InstallContext("", null);

            SIO.Context = iContext;
            SIO.ServiceName = serName;
            try {
                SIO.Uninstall(null);
            } catch (System.ComponentModel.Win32Exception) {
                return false;
            }
            return true;
        }

        public void addDependency(String[] dependency)
        {
            SIO.ServicesDependedOn = dependency;
        }
    }
}
