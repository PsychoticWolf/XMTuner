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
