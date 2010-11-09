<?php
$files = scandir("images/");
rsort($files);

$_captions = explode("\r\n", file_get_contents("captions.txt"));
foreach($_captions as $_caption) {
	$_caption = explode("|", $_caption);
	$captions[$_caption[0]] = $_caption[1];
}
unset($_captions, $_caption);

?>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN">
<!-- Created Sun May 30 18:27:36 2010 -->
<html>
<head>
<link rel="stylesheet" href="../xmtuner.css" type="text/css">

<script type="text/javascript" src="highslide/highslide.js"></script>
<link rel="stylesheet" type="text/css" href="highslide/highslide.css" />
<script type="text/javascript">
	hs.graphicsDir = 'highslide/graphics/';
	hs.wrapperClassName = 'wide-border';
</script>


<title>XMTuner :: Screenshots</title>
</head>
<body>
<?php
	include_once"../inc_superbar.php";
	include_once"../inc_header.php";
?>

<div id="body">
	<h2 class="red">XMTuner Screenshots</h2>
	<p>Screenshots of XMTuner - Newest versions are listed first. In general (for versions 0.3 and newer), if a screenshot of
	a specific item is available for an older version but not a newer one, then the older one is still current.</p>
	
	<?php
		//echo"<pre>"; var_dump($files); echo"</pre>";
		foreach ($files as $file) {
			$pathinfo = pathinfo($file);
			if (strtolower($pathinfo['extension']) != 'png' || strpos($file,"_resize") !== false) { continue; }
			$version = str_replace("-",".",substr($pathinfo['basename'],0,3));
			
			if ($past_version != $version) {
				if ($past_version) { echo"</tr></table>\n\n"; }
			
				echo"<h2>XMTuner $version</h2>\n";
				echo"<table><tr>\n";
				$total_width = 0;
			}
			
			echo"<td align=\"center\">";
			$resizepath = $pathinfo['filename']."_resize.".$pathinfo['extension'];
			if (file_exists("images/".$resizepath)) {
				$imginfo = getimagesize("images/$resizepath");
				$width = $imginfo[0];
			
				echo"<a href=\"images/$file\" class=\"highslide\" onclick=\"return hs.expand(this)\"><img src=\"images/$resizepath\" ".$imginfo[3]." border=0 alt=\"$file\"></a>\n";
			} else {
				$imginfo = getimagesize("images/$file");
				if ($imginfo[1] > 160) {
					$width = ceil($imginfo[0] * 160 / $imginfo[1]); 
				}
				echo"<a href=\"images/$file\" class=\"highslide\" onclick=\"return hs.expand(this)\"><img src=\"images/$file\" border=0 height=\"160\" alt=\"$file\"></a>\n";
			}
			if ($captions[$file]) {
				$caption = $captions[$file];
				echo"<div class=\"highslide-caption\">$caption</div>\n";
				echo"<br/><span class=\"caption\">$caption</span>\n";
			}
			echo"</td>\n";
			
			$past_version = $version;
			$total_width = $total_width + $width;
			if ($total_width >= 800) {
				$total_width = 0;
				echo"</tr>\n<tr>\n";
			}
		}
		echo"</tr></table>";
	?>
	
</div>
<?php
	include_once"../inc_footer.php";
?>
</body>
</html>