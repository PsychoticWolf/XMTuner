<?php
require_once"dbconfig.php";
$sitekey = 'xmtuner';
unset($Email, $Name, $Subject, $Message);

if ($_REQUEST["form"] == "contactme")
{
	if (strpos($_REQUEST['Email'], '@') !== false) {
		$Email = strip_tags($_REQUEST['Email']);
	}
	
	$Name = strip_tags($_REQUEST['Name']);
	if (!$Name) { $Name = $Email; }
	$Subject = strip_tags($_REQUEST['Subject']);
	$Message = strip_tags($_REQUEST['Message']);
	$IP = $_SERVER['REMOTE_ADDR'];
	
	if (!$Email || !$Name || !$Subject || !$Message) {
		die("Missing required field(s)");
	}

	$Subject = "[XMTuner] ".$Subject;

	$sql = "INSERT INTO `contactme` (`ID`, `SiteKey`, `Name`, `Email`, `Type`, `Subject`, `Body`, `IP`) VALUES ('', '$sitekey', '$Name', '$Email', '$Type', '$Subject', '$Message', '$IP');";
	$sql_result = mysql_query($sql, $connection) or trigger_error("<FONT COLOR=\"#FF0000\"><B>MySQL Error ".mysql_errno().": ".mysql_error()."</B></FONT>", E_USER_ERROR);

	$contactname = "XMTuner";
	$contactemail = "xmtuner@pcfire.net";
	
	$footer = "--\r\nSent via the Contact Form at http://www.xmtuner.net/";

	$myemail = $Email;
	$myname = $Name;

	$message = $Message . "\r\n\r\n" . $footer;

	$headers = "MIME-Version: 1.0\r\n"; 
	$headers .= "Content-type: text/plain; charset=iso-8859-1\r\n"; 
	$headers .= "From: ".$Name." <".$Email.">\r\n"; 
	$headers .= "To: ".$contactname." <".$contactemail.">\r\n"; 
	$headers .= "To: ".$Name." <".$Email.">\r\n"; 
	$headers .= "Reply-To: ".$myname."<".$myemail.">\r\n"; 
	$headers .= "X-Mailer: XMTuner.Net Website Mailer\r\n";
	$headers .= "X-User-IP: $IP\r\n";
	
	$retval = mail($contactemail, $Subject, $message, $headers);
	//echo"<p>$contactemail, $Subject, $message, $headers</p>";
	
//Template Follows...
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
<link rel="stylesheet" href="../xmtuner.css" type="text/css">
<title>XM Tuner :: Contact</title>
</head>
<body>
<?php
	include_once"../inc_superbar.php";
	include_once"local_header.php";
?>
<div id="body">
	<h2 class="red">Contact Us</h2>

<?php
	
	if (!$retval) {
		echo"<h4 style=\"color: red;\">An error occured when trying to send your message. Please try again.</h4>";
	} else {
		echo"<h4>Your message has been sent.<br>A copy has been sent to your e-mail address for your records.</h4>";
	}
?>
</div>
</body>
</html>
<?php
}
?>