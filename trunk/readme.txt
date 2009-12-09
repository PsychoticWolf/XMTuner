XM Tuner
December 8, 2009
Release 0.3
------------------

This application is designed as an intermediate between the XM Radio Online platform
and a UPnP service provider (such as TVersity or Orb). 

If you encounter any issues, please send us an email at xmtuner@pcfire.net.

Usuability notes:
	- You will need an XMRO account to utilize this application.
	- The default address for the service is http://localhost:19081/feeds/
	- "Playing now" information is available at http://localhost:19081/
	- You may need to open port 19081 on your Windows firewall
	- The port number can be changed in the configuration
	- You WILL need a new configuration if you were running a previous build
	- If after installing the service you wish to not automatically start the service:
		Control Panel> Administrative Tools> Services > XMTuner
		Right-click, Properties. Toggle "Startup type" to "Manual"
	- Service requires that you check the "Autologin" box for your configuration

Release Comments:
	- Future releases with more functionality are intended
	- The target platform of this application is Windows XP SP2 or higher
	- The target framework of this applicaiton is .NET Framework 3.5

The Service:
		With this release comes the ability to install a Windows service version of the
	application. This means a lot of things for users, including no longer havine the 
	requirement of being logged into the PC to have XMRO on your devices. The service 
	installs and will automatically start with Windows by default. The service will read
	your configuration file and run with the same settings (you MUST check the AUTOLOGIN 
	box in Configuration). If you make configuration changes while the service is running, 
	be sure to save the changes and then using the Service Control tab, click Restart.
		The service opperates the same way as the application normally does, except without
	a user interface, although, you may still launch XMTuner to make changes to the service
	or remove it completely if you wish. 
	
Changes from 0.2
	- Ability to run server as a service (no need to stay logged in to Windows)
	- Service control mechanism in the user interface
	- Cleaner UI
	- Fixed several crash-worthy bugs
	- Caching of channel data
	- Refreshing of "playing" info every 30 seconds
	- MP3 Transcoding via TVersity
	- Custom addresses via configuration
	- New configuration format
	- New configuration location
	- Substancial UI enhancement
	- Updated "What's On" Page
	- Automatically checks for new versions
	

Special Notes for TVersity Users:
	-If you have a limit of 20 channels on Tversity:
		-Set Internet Feeds Maximum Items Per Feed to 0 (unlimited)



