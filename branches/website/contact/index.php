<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN">
<!-- Created Sun May 30 18:27:36 2010 -->
<html>
<head>
<link rel="stylesheet" href="../xmtuner.css" type="text/css">
<title>XMTuner :: Contact</title>
</head>
<body>
	<?php
	include_once"../inc_superbar.php";
	include_once"local_header.php";
?>
<div id="body">

	<h2 class="red">Contact Us</h2>

	<p style="width: 718px">Whether its a suggestion, technical support question or just postitive (or constructively negative) feedback.
	We welcome hearing from our users. To contact us use the form below.</p>
	
	<p style="width: 650px; border: 1px dotted blue; color: OrangeRed; background-color: MistyRose; padding: 5px;"><strong>Notice!</strong> 
	XMTuner is retired. I am no longer providing technical support for the application. Use the Forums or the <a href="http://support.xmtuner.net/wiki/XMTuner:Frequent_Support_Issues" style="font-weight: bold;">Frequent Support Issues</a> page on
	the XMTuner Support wiki. Your issue may already be covered there. For non-support related issues, feel free to contact me.</p>
	<table id="contact">
	<form action="post.php" method="POST">
	<input type="hidden" name="form" value="contactme"/>
		<tr>
			<td class="label">
			Your Name: 
			</td>
			<td class="data">
			<input type="text" size="60" name="Name"/>
			</td>
		</tr>
		<tr>
			<td class="label">
			Your Email Address: 
			</td>
			<td class="data">
			<input type="text" size="60"  name="Email"/>
			</td>
		</tr>


		<tr><td height="15"></td></tr>
		<tr>
			<td colspan="2" style="font-weight: bold">Message:</td>
		</tr>
			<tr>
			<td class="label" colspan="2">
			<input type="text" size="90"  name="Subject" value="[No Subject]"/>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="center"><textarea name="Message" rows="16" cols="80"></textarea>
			<input type="image" src="link_email.png" border="0" title="Send Message"/></td>
		</tr>
		<!--<tr>
			<td colspan="2" align="center"><input type="Submit" name="Submit" value="Send Message"/></td>
		</tr>-->
	</form>
	</table>
</div>
</body>
</html>