<?php
unset($id);
$files = scandir(".");
usort($files, "cmp");

function cmp($a, $b)
{
    if ($a == $b) {
        return 0;
    }
    //return ($a < $b) ? -1 : 1;
	return version_compare($a, $b, "<");
}

if ($_REQUEST['id']) {
	$_id = trim($_REQUEST['id']);
	if (in_array(str_replace(".","-",$_id).".htm",$files)) {
		$id = $_id;
	}

}

if (!$id) {
	$id = trim(str_replace("-",".",basename($files[0],".htm")));
}

$version = $id;
$filename = $_file = str_replace(".","-",$id).".htm";
?>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN">
<!-- Created Sun May 30 18:27:36 2010 -->
<html>
<head>
<link rel="stylesheet" href="../xmtuner.css" type="text/css">

<title>XMTuner :: Releases - XMTuner <?php echo"$version"; ?></title>
</head>
<body>
<?php
	include_once"../inc_superbar.php";
	include_once"../inc_header.php";
?>

<div id="body">
<table>
<tr>
	<td valign="top">
<div id="sidebar">
	Releases:<br>
	<?php
		foreach ($files as $file)
		{
			if (strpos($file, ".htm") === false) { continue; }
			$_version = explode(".",$file);
			$_version = str_replace("-",".",$_version[0]);
		
			echo"<a href=\"$_version\">XMTuner $_version</a><br>\n";
		}
	?>
</div>
</td>
<td valign="top">
<div id="rightside">
	<?php
    function remove_HTML($s , $keep = '' , $expand = 'script|style|noframes|select|option|h1|title'){
        /**///prep the string
        $s = ' ' . $s;
       
        /**///initialize keep tag logic
        if(strlen($keep) > 0){
            $k = explode('|',$keep);
            for($i=0;$i<count($k);$i++){
                $s = str_replace('<' . $k[$i],'[{(' . $k[$i],$s);
                $s = str_replace('</' . $k[$i],'[{(/' . $k[$i],$s);
            }
        }
       
        //begin removal
        /**///remove comment blocks
        while(stripos($s,'<!--') > 0){
            $pos[1] = stripos($s,'<!--');
            $pos[2] = stripos($s,'-->', $pos[1]);
            $len[1] = $pos[2] - $pos[1] + 3;
            $x = substr($s,$pos[1],$len[1]);
            $s = str_replace($x,'',$s);
        }
       
        /**///remove tags with content between them
        if(strlen($expand) > 0){
            $e = explode('|',$expand);
            for($i=0;$i<count($e);$i++){
                while(stripos($s,'<' . $e[$i]) > 0){
                    $len[1] = strlen('<' . $e[$i]);
                    $pos[1] = stripos($s,'<' . $e[$i]);
                    $pos[2] = stripos($s,$e[$i] . '>', $pos[1] + $len[1]);
                    $len[2] = $pos[2] - $pos[1] + $len[1];
                    $x = substr($s,$pos[1],$len[2]);
                    $s = str_replace($x,'',$s);
                }
            }
        }
       
        /**///remove remaining tags
        while(stripos($s,'<') > 0){
            $pos[1] = stripos($s,'<');
            $pos[2] = stripos($s,'>', $pos[1]);
            $len[1] = $pos[2] - $pos[1] + 1;
            $x = substr($s,$pos[1],$len[1]);
            $s = str_replace($x,'',$s);
        }
       
        /**///finalize keep tag
        for($i=0;$i<count($k);$i++){
            $s = str_replace('[{(' . $k[$i],'<' . $k[$i],$s);
            $s = str_replace('[{(/' . $k[$i],'</' . $k[$i],$s);
        }
       
        return trim($s);
    }
	
	
		$contents = file_get_contents($filename);
		$contents = remove_HTML($contents, "p|h2|img|h3|ul|li|br|a");
		
		echo"<h2 class=\"red\">XMTuner Release Notes - Version $version</h2>\n";
		print($contents);
	?>
</div>
</td>
</tr>
</table>

</div>
<?php
	include_once"../inc_footer.php";
?>
</body>
</html>