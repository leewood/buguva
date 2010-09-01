<?php
$a = $_GET["name"];
$b = $_GET["text"];
$d = fopen("for.php", "a");
fwrite($d, "<tr><big style='font-weight: bold;'>\n");
fwrite($d , "$a</big><br>$b<hr style='width: 100%; height: 2px;'></tr>\n");
fclose($d);
include("koment.php");
?>