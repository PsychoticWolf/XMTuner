<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN">
<!-- Created Sun May 30 18:27:36 2010 -->
<html>
<head>
<link rel="stylesheet" href="xmtuner.css" type="text/css">
<script src="slideshow.js" type="text/javascript"></script>
<style type="text/css">
P {
	font-family: Arial;
	font-size: 12pt;
}
</style>

<title>XMTuner :: The End of the Road...</title>
</head>
<body>

<?php
	include_once"inc_superbar.php";
	$skipNavbarContent = true;
	$content = "The End of the Road for XMTuner";
	include_once"inc_header.php";
?>
<div id="body">
<div style="margin-right: 290px; margin-left: 50px;">
	<?php
		include_once"download/inc_download.php";
	?>
<div style="height: 240px">
<a href="/screenshots/"><img border=1 src="/screenshots/images/0-6main.png" align="left" width="350" style="margin: 10px; margin-top: 0px;"></a>
<p style="font-weight: bold">Its official, the XMTuner project is now retired.</p>
<p>Recent changes by SiriusXM to their Internet Radio service have made it impossible to continue to support or develop XMTuner. As a result, XMTuner 0.6.4 will be the last version, and it will remain available to download.</p>
<p>The source code for XMTuner 0.6.4 is already available for those who want it, and will also remain available. Additionally, I will be releasing the pre-release source-code for what would have been XMTuner 0.7 for download.</p>
<p>Support will no longer be offered, the Sourceforge.net  forums will remain available for user-to-user support issues.</p>
<p>XMTuner.net will cease operation on March 1, 2013. </p>
</div>
<h2 style="margin-top: 20px">Background</h2>
<p>Changes by SiriusXM over the past two years have greatly interfered with the development of XMTuner, causing many of the features that it had to be degraded or downright broken, and while its ok to continue to use the software for as long as it continues to work, when it comes to putting time and effort into developing newer versions of XMTuner, its simply not worth it.</p>
<p>XMTuner was always a pet project; I built it because I had a need to stream XM radio online streams to my media player, following website changes which broke the previous uXM and uSirius applications which did a simpler version of what XMTuner does today. It was always an imperfect setup for me, and I eventually moved on to just using a dedicated XM Satellite Radio and not using the internet streams as much personally. Not having the strong personal need meant devoting less time to the project, except when it meant improving the application, as a result there was a long pause between XMTuner 0.6 and what would have been 0.7, just as I was getting deep into rewriting huge portions of the XMTuner code, SiriusXM launched the new player to the world, and served as a strong reminder that XMTuner was completely at the mercy of whatever changes SiriusXM sees fit to introduce to its internet radio service. In this case, it meant the end of the support from SiriusXM for the Windows Media based streams that had been in use for years, in favor of an encrypted and much less usable flash format, which XMTuner would have required serious changes to begin to support and many of its features (everything beyond being a desktop-based player, pretty much, especially the media device/web support) would not have been able to be continued without even more time invested. In short, it wasn’t worth it to rewrite the application to support the new player, and I decided that I had no intention or plan to do so.</p>
<p>I did continue to try to finish XMTuner 0.7 and still use the legacy player. Some of the features XMTuner 0.7 would’ve had: A new player interface with radio-style preset buttons, support for Sirius/XM Canada (if possible), not requiring the web-server or administrative privileges on Windows Vista/7 –to be more friendly for those in locked down environments, proxy support for those on corporate networks and XMTuner remote – the ability to control the application from another PC or mobile device.  Until the channel lineup changes broke many of the streams and subsequently the streams would go down unexpectedly, and at that point, I decided that development beyond XMTuner 0.6.4 would end without a change in direction from SiriusXM, which is not likely.</p>
<p>This past August, I ended my subscription to SiriusXM Internet Radio, and reduced my satellite subscription to the Mostly Music package, as I don’t find the SiriusXM Entertainment Channels or their Internet player worth the money. As a result, my time with XMTuner has officially ended. </p>
<p>Building on that, I have learned that SiriusXM has changed their legacy streams, which to my understanding exist (existed?) only for their SiriusXM for Business service, and XMTuner is no longer able to tune a majority of the channels because the streams no longer exist. This represents the final nail in the XMTuner coffin as XMTuner cannot be fixed if there’s no content on SiriusXM’s side. Its been a fun project, but the time has come for it to officially be put to rest.</p>
<p>If there’s a developer interested in taking over the project, I am willing to transfer the domain and the source-code over to them, contact me to discuss this.</p>
<p>Thanks everyone for your support<br/>
-- Chris</p>



</div>
<?php
	include_once"inc_footer.php";
?>

</body>
</html>