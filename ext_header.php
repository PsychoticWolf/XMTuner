<?php
if ($pwSuperbar == "wiki")
{
	$skipStylesheet = true;
?>
<style type="text/css">
#mw-head
{
	top: 25px !important;
}

#p-logo
{
	top: -135px !important;
}
</style>
<?php
}
?>

<?php
if ($skipInnerStylesheet !== true) {
?>
<style type="text/css">
#superbar {
	font-family: Arial;
	float: right;
	position: fixed;
	right: 0px;
	top: 0px;
	padding-top: 3px;
	width: 350px;
	height: 25px;
	background-color: #CCC;
	color: white;
	text-align: center;
	font-size: 16px;
	font-weight: bold;
	-moz-border-radius-bottomleft: 10px;
	z-index: 1000;
}

#superbar a, #superbar a:visited {
	color: white;
}
#superbar a:hover {
	color: yellow;
	text-decoration: underline;
}
</style>
<?php
}

include_once"inc_superbar.php";
?>