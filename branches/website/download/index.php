<?php
$skip_ui = true;
require_once"inc_download.php";
?>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN">
<!-- Created Sun May 30 18:27:36 2010 -->
<html>
<head>
<link rel="stylesheet" href="../xmtuner.css" type="text/css">

<title>XMTuner :: Download</title>
</head>
<body>
<?php
	include_once"../inc_superbar.php";
	include_once"../inc_header.php";
?>

<div id="body">

<h2 class="red">XMTuner Download</h2>

<div style="position: relative; height: 150px;">
<a href="<?php print($files[0]) ?>"><img src="/images/download_large.png" align="left" border="0" width="128">
<h3>Download XMTuner <?php $version = (get_version(0)); print($version);?>!</h3></a>
<?php echo"Released on ".date("F j, Y",filemtime($files[0]))."<br />\n"; ?>
<?php print(get_filesize(0)); ?> (32-bit, MSI)<br/>
<span style="font-size: 8pt;"><a href="/releases/<?php print($version); ?>">[Release Notes]</a></span>

</div>


<h2>System Requirements</h2>
<ul>
<li><a href="http://www.xmradio.com/player/home/xmhome.action" target="_blank">XM Radio Online</a> or <a href="http://www.sirius.com/player/listen/play.action" target="_blank">Sirius Internet Radio Subscription</a></li>
<li><a href="http://www.microsoft.com/downloads/details.aspx?familyid=333325fd-ae52-4e35-b531-508d977d32a6&displaylang=en" target="_blank">Microsoft .NET Framework 3.5</a></li>
<li>Microsoft Windows XP (SP2+), Windows Vista, or Windows 7</li>
</ul>

<a id="pastreleases"></a>
<h2>Past Releases</h2>
<p>Note! Previous releases of XMTuner are made available for archival purposes only. They are unsupported and may not work at all, as backwards incompatible changes occur.</p>
<ul id="download-list">
	<?php
	$i=1;
	foreach ($files as $file) {
		if ($file == $files[0]) { continue; }
		$version = get_version($i);
		$filesize = get_filesize($i);
		echo "<li><a href=\"$file\">XMTuner $version</a> ($filesize, 32-bit MSI) <a href=\"/releases/$version\">[Details]</a></li>";
		$i++;
	}
	?>
</ul>
</div>
<?php
	include_once"../inc_footer.php";
?>
</body>
</html>