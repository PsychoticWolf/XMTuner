﻿using System;
using System.ServiceProcess;

namespace XMTuner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new XMTunerService()
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
