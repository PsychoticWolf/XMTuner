<?php
$basepath = "";
function cmp($a, $b)
{
    if ($a == $b) {
        return 0;
    }
    //return ($a < $b) ? -1 : 1;
	return version_compare($a, $b, "<");
}

function remove_nonmsi($_array)
{
	$array = array();
	foreach ($_array as $value) {
		if (pathinfo($value, PATHINFO_EXTENSION) == "msi") {
			$array[] = $value;
		}
	}
	
	return $array;
}

function get_filesize($index) {
	global $files;
	global $basepath;
	$path = $files[$index];
	if ($basepath) { $path = $basepath.$path; }
	$size = filesize($path);
	$size = number_format($size/1024);
	
	return $size."KB";
}

function get_version($index) {
	global $files;
	$file = $files[$index];
	$version = str_replace(array("InstallXMTuner", ".msi"), "", $file);
	$version = str_replace("-", ".", $version);
	
	return $version;
}

if ($skip_ui == true) {
	$files = remove_nonmsi(scandir("."));
	usort($files, "cmp");
	return;
}

$files = remove_nonmsi(scandir("download/"));
usort($files, "cmp");


$basepath = "download/";
$file = $basepath.$files[0];
?>

<div id="download">
<a href="<?php print($file) ?>"><img src="/images/download_large.png" align="left" border="0" width="80">
<h3>Download&nbsp;XMTuner&nbsp;<?php $version = get_version(0); print($version);?>!</h3></a>
<?php echo"Released on ".date("F j, Y",filemtime($file))."<br />\n"; ?>
<?php print(get_filesize(0)); ?> (32-bit, MSI)<br/>
<span style="font-size: 8pt;"><a href="/releases/<?php print($version); ?>">[Release Notes]</a></span>

</div>