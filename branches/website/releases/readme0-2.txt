XM Tuner
November 29, 2009
Release 0.2
------------------

This application is designed as an intermediate between the XM Radio Online platform
and a UPnP service provider (such as TVersity or Orb). 

If you encounter any issues, please send us an email at xmtuner@pcfire.net.

Usuability notes:
	- You will need an XMRO account to utilize this application.
	- The default address for the service is http://localhost:19081/feeds/
	- "Playing now" information is available at http://localhost:19081/
	- The port number can be changed in the configuration
	- If you have 0.1 installed, you may retain your config file
	- For Windows Vista/Windows 7, you will need to run with administrator
		privilleges for the server portion

Release Comments:
	- Future releases with more functionality are intended
	- The target platform of this application is Windows XP SP2 or higher
	- The target framework of this applicaiton is .NET Framework 3.5

Changes from 0.1
	- URL builder is available on the Channels tab for Orb and others that
		may require direct access to specialized URLS
	- Logs are now written on application exit
	- Minimize to system tray is now an option
	- New "What's On" page (http://localhost:19081/ by default)

Potential Future Features:
	-Run as a service in Windows (no need to login to Windows)
	

Special Notes for TVersity Users:
	-If you have a limit of 20 channels on Tversity:
		-Set Internet Feeds Maximum Items Per Feed to 0 (unlimited)




Developers:

Chris Crews
Curtis M. Kularski