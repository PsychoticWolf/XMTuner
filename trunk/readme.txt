XM Tuner
January 16, 2010
Release 0.4
------------------

This application is designed as an intermediate between the XM Radio Online platform
and a UPnP service provider (such as TVersity or Orb). 

If you encounter any issues, please send us an email at xmtuner@pcfire.net.

Usuability notes:
	- You will need an XMRO account to utilize this application.
	- The default address for the feed is http://localhost:19081/feeds/
	- "Playing now" information is available at http://localhost:19081/
	- You may need to open port 19081 on your Windows firewall
	- The port number can be changed in the configuration
	- Configuration of 0.4 is identical to 0.3, no new configuration is needed
	- If after installing the service you wish to not automatically start the service:
		Control Panel> Administrative Tools> Services > XMTuner
		Right-click, Properties. Toggle "Startup type" to "Manual"
	- Service requires that you check the "Autologin" box in your configuration

Release Comments:
	- XM Tuner requires Windows XP SP2 or higher with Microsoft .NET Framework 3.5

The Service:
		With the release of 0.3 XMTuner added the ability the ability to install a Windows
	service version of the application. This means a lot of things for users, including no
    longer havine the requirement of being logged into the PC to have XMRO on your devices.
    The service installs and will automatically start with Windows by default. The service will
    read your configuration file and run with the same settings (you MUST check the AUTOLOGIN 
	box in Configuration). If you make configuration changes while the service is running, 
	be sure to save the changes and then using the Service Control tab, click Restart.
		The service opperates the same way as the application normally does, except without
	a user interface, although, you may still launch XMTuner to make changes to the service
	or remove it completely if you wish. 
	
Changes from 0.3
    * Internal player functionality
    * Updated "Channels" page works with the plauer
    * "Now Playing" information now in main UI
    * Program Guide added to Channels page
    * "Playing" information history available
    * Notification box on information change
    * PLS, M3U and ASX Playlists
    * Windows UAC now supported
    * More stable and less crashes than 0.3

	
Special Notes for TVersity Users:
	-If you have a limit of 20 channels on Tversity:
		-Set Internet Feeds Maximum Items Per Feed to 0 (unlimited)
