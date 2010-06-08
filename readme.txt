XM Tuner
June 8, 2010
Release 0.6
------------------

XMTuner is a media application that provides Sirius and XM Satelite Radio streams to media servers,
set-top media players, game consoles, smart phones and virtually any device capable of playing
streaming media. XMTuner also has its own built in player to play XM Radio right from your desktop.

If you encounter any issues, please send us an email at xmtuner@pcfire.net.

Usability notes:
	- You will need an XM Radio Online or Sirus Internet Radio account to utilize this application.
	- The default address for the feed is http://localhost:19081/feeds/
	- "Playing now" information is available at http://localhost:19081/
	- You may need to open port 19081 on your Windows firewall
	- If after installing the service you wish to not automatically start the service:
		Control Panel> Administrative Tools> Services > XMTuner
		Right-click, Properties. Toggle "Startup type" to "Manual"

System Requirements:
	- XM Tuner requires Windows XP SP2 or higher with Microsoft .NET Framework 3.5

The Service:
		With the release of 0.3 XMTuner added the ability the ability to install a Windows
	service version of the application. This means a lot of things for users, including no
    longer havine the requirement of being logged into the PC to have XMRO on your devices.
    The service installs and will automatically start with Windows by default. The service
    will read your configuration file and run with the same settings. If you make configuration
    changes while the service is running, be sure to save the changes and then using the Service
    Control, click Restart.	The service opperates the same way as the application normally does,
    except without a user interface. 
	
Special Notes for TVersity Users: 
	-If you have a limit of 20 channels on Tversity:
		-Set Internet Feeds Maximum Items Per Feed to 0 (unlimited)
