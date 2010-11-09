<?php
// MySQL Server Configuration Variables
    $db_server = "localhost"; //MySQL Server Hostname
    $db_user = "giti"; // MySQL Username
    $db_pass = "koe8l29oa4layi"; // MySQL Password  
    $db_name = "sites"; // MySQL Database Name

    $connection = mysql_connect("$db_server","$db_user","$db_pass") or trigger_error("MySQL Error ".mysql_errno().": ".mysql_error()."", E_USER_ERROR);
    $db = mysql_select_db("$db_name", $connection) or trigger_error("MySQL Error ".mysql_errno().": ".mysql_error()."", E_USER_ERROR);
?>