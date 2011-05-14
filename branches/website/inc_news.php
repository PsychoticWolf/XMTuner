<?php
// MySQL Server Configuration Variables
    $db_server = "localhost"; //MySQL Server Hostname
    $db_user = "wordpress"; // MySQL Username
    $db_pass = "o65jy5t6j92n3q"; // MySQL Password  
    $db_name = "wordpress"; // MySQL Database Name

    $connection = mysql_connect("$db_server","$db_user","$db_pass") or trigger_error("MySQL Error ".mysql_errno().": ".mysql_error()."", E_USER_ERROR);
    $db = mysql_select_db("$db_name", $connection) or trigger_error("MySQL Error ".mysql_errno().": ".mysql_error()."", E_USER_ERROR);


	$sql = "SELECT `post_title`,`guid`,`post_modified` FROM `xmt_posts` WHERE `post_status` = 'publish' && `post_type` = 'post' ORDER BY `post_date` DESC LIMIT 3";
	$sql_result = mysql_query($sql, $connection) or trigger_error("<FONT COLOR=\"#FF0000\"><B>MySQL Error ".mysql_errno().": ".mysql_error()."</B></FONT>", E_USER_NOTICE); 
?>


<div id="news">
<h2 class="red">News</h2>
<ul>
<?php
	while ($row = mysql_fetch_array($sql_result))
	{
		$date = date("l, F j, Y g:ia", strtotime($row['post_modified']));
		if (strtotime($row['post_modified']) >= time()-(86400*3))
		{
			echo"<li style=\"font-weight: bold; font-size: 12pt; color: red;\"><a style=\"color: red;\" href=\"{$row["guid"]}\">{$row["post_title"]}</a><sup>new</sup><span style=\"font-weight: normal; color: #666; padding-left: 15px; font-size: 8pt;\">[Posted: $date]</span></li>\n";
		} else {
			echo"<li><a href=\"{$row["guid"]}\">{$row["post_title"]}</a><span style=\"font-weight: normal; color: #666; padding-left: 15px; font-size: 8pt;\">[Posted: $date]</span></li>\n";
		}
		
	}
?>

</ul>
</div>