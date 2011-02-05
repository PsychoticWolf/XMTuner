<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN">
<!-- Created Sun May 30 18:27:36 2010 -->
<html>
<head>
<link rel="stylesheet" href="xmtuner.css" type="text/css">
<script src="slideshow.js" type="text/javascript"></script>

<title>XMTuner :: Sirius|XM Satelite Radio on your media player</title>
</head>
<body>

<?php
	include_once"inc_superbar.php";
	include_once"inc_header.php";
?>

<div style="font-size: 14pt; font-weight: bold; color: #ffffff; text-align: justify; background-color: #ffcc00; padding: 15px; margin: 0px; text-shadow: 2px 2px 2px #000000;">
						On February 4th, 2011, SiriusXM launched a new website combining xmradio.com and sirius.com. Along with thie new website is a completely new listen online player.
						This major change to the player has broken XMTuner 0.6.1. XMTuner 0.6.2 has been released which patches this issue. For more details see the <a href="http://blog.xmtuner.net/">XMTuner Blog</a>.
</div>

<div id="show-parent">
	<div id="show"><div id="show-index" style="float: left;"></div></div>

</div>
<div id="body">
<div style="margin-right: 290px;">
	<?php
		include_once"download/inc_download.php";
	?>
<h2 class="red">About</h2>

<p>XMTuner allows you to access the Sirius|XM Satelite Radio streams (both XM Radio Online and Sirius Internet Radio) on your UPnP media player. XMTuner serves as a proxy or interpreter between the SiriusXM web radio
pages and your device or media server, making the streams much more accessible. XMTuner supports a variety of devices, such as the Xbox 360, Playstation 3, Nintendo Wii consoles, the D-Link DSM-320 and similar set-top players, several internet radio
capable receivers and televisions, as well as a variety of smartphones such as the Blackberry, Palm, Android, and iPhone.</p>
<p>In addition, XMTuner features its own built-in player, so you can play your favorite channels right from your desktop, with no bulky browser needed.</p>
</div>

<h2>Features</h2>
<ul>
	<li>XM and Sirius Radio Support</li>
	<li>Integrated Media Player - not just for set-tops and mobiles, but your desktop too.</li>
	<li>Customizable Recently Played History, Integrated SiriusXM Program Guide</li>
	<li>Now Playing website</li>
	<li>Works with a variety of devices and UPnP servers</li>
	<li>Runs either a desktop application or in the background as a windows service.</li>
	<li>Server can generate RSS Feeds as well as Playlists (PLS, ASX and M3U) for wide device and server compatibility</li>
	<li>Supports transcoding streams to other formats using TVersity for devices that don't work with windows media.</li>
	<li>Built-in Updater keeps you current.</li>
	<li>and much more!</li>
</ul>

<h2>XMTuner 0.6.2</h2>
<h3>February 4, 2011</h3>
		<p>XMTuner 0.6.2 is a bugfix release for XMTuner 0.6.1. This release patches around the new player SiriusXM introduced today.</p>
		
		<h3>Known Issues with 0.6.2:</h3>
		<ul>
		<li>Channel Icons, Descriptions and Homepage links don't work properly</li>
		<li>Cached data may cause issues, if you run into problems, clear your cache files by clicking the refresh channel data button in settings.</li>
		</ul>

<h2>XMTuner 0.6.1</h2>
<h3>November 8, 2010</h3>
<p>XMTuner 0.6.1 is a bugfix release for XMTuner 0.6. This fixes the login error experienced by XM users after changes to the XMRO login page.</p>

<h2>XMTuner 0.6</h2>
<h3>June 13, 2010</h3>
<p>XMTuner 0.6 is a new major release. Focus on this release has been improving and simplifying features, primarily in the main UI, as well as lots of bugfixing. New to this release is the "XM Service Control"
utility. This little application is the new home for the "Service" tab for users of the XMTuner service, letting you manage and configure the service as well as see its log file, without having to run the 
main XMTuner program. Other new features include support for favorite channels, an improved history tab that better matches the rest of the application; Addition of views (such as by category) for the channels
tab; The URL builder panel, which takes the UI for generating links to playlists, feeds, channels, etc, for configuring your devices and places it in a single location, which can be hidden away when you're done
with it; XMTuner is now resizable, no more being stuck only seeing a few channels; On Windows 7 and Vista, Aero glass is now featured in the UI, so it better fits in with the rest of your desktop.</p>

<p>Behind the scenes, the server now supports channel playlists for better compatibility with some devices and media servers, the transcoder (if enabled) can now provide wav streams as well as mp3. XMTuner now 
supports windows power management, and will shut down its server when your computer sleeps, and upon wake-up restart back to its previous state. Lots of crashes and exceptions have been fixed, too many to list
here. XMTuner 0.6 also now supports auto-retrying your stream when there's a built-in player error, as part of a drive to improve the application's performance both in responsiveness and long-term usage. XMTuner
starts up faster, is easier to use, and plays longer than before.</p>
		
		<h3>Changes in 0.6</h3>
		<ul>
			<li>Improved Service Control with new XMTuner Service Control utility</li>
			<li>Favorite Channels
			<li>New History Tab
			<li>URL Builder
			<li>Channels tab now supports sorting by category
			<li>Aero UI for Windows Vista / Windows 7 users
			<li>UI Improvements, such as being resizable, always on top, etc.
			<li>History length can now be customized and the notification box can be disabled, if desired.
			<li>Improvements to TVersity transcoder support - can now transcode to WAV as well as MP3
			<li>Channel Playlists now supported for feeds for servers and devices that need them.
			<li>Support for Windows Power Management
			<li>Auto relogin on login failure
			<li>Auto-retry on player errors. 
			<li>Lots of fixes for long-term usage and playback (can now stream reliably for 24+ hours)
			<li>Improved error messages for when things inevitably go wrong.
			<li>Lots of crash (exception) errors fixed
			<li>Performance has been improved (both for UI like the channels tab and things like startup time.)
			<li>Automatically check for relogin in the background in addition to when things happen
			<li>and more...</li>
		</ul>


		<h2>Support</h2>
		<p>Having a problem? The following resources are available to you to help get it solved. </p>
		
		<div style="margin-left: 20px; width: 700px;">
		
		<h3><a href="http://sourceforge.net/projects/xmtuner/forums/forum/1050609" target="_blank">SourceForge Forum</a></h3>
		<p>Our sourceforge page has a interactive forum where you can ask your question and get help from other users for your particular problem.</p>
		
		<h3><a href="http://wiki.xmtuner.net/">XMTuner WIKI</a></h3>
		<p>We're building a Knowledge base WIKI full of information about XMTuner which can be helpful in resolving problems. Even if you're not having a problem,
		we would love to hear from you, help us build our database of working devices and configruations.</p>
		
		<h3><a href="/contact/">Contact the Developers</a></h3>
		<p>Alright, so you've tried the above options and nothing quite worked out. That's ok. Send us an e-mail directly, and we'll try to help you out.</p>
		
		<h3><a href="https://bugzilla.pcfire.net/" target="_blank">XMTuner Bugzilla</a></h3>
		<p>Found a problem? Let us know. If you're familar with software bug trackers, file a bug in our bugzilla. If not, we still want to know, so see the
		above link and send us an e-mail.</p>
		</div>
</div>
<?php
	include_once"inc_footer.php";
?>

</body>
</html>