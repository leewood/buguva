<html>
<head>
  <meta content="text/html; charset=ISO-8859-1" http-equiv="content-type">
  <title>meniu</title>
</head>
<body
 style="color: rgb(192, 192, 192); background-color: rgb(0, 0, 0);"
 alink="#c0c0c0" link="#c0c0c0" vlink="#c0c0c0">

<table border = "0" align="center" width="40%">

<?php

include("for.php");
?>

<tr>
<form method="get" action="addkoment.php"
 name="komentaras">Vardas:<br>
  <input name="name"><br>
Komentaras:<br>
  <textarea cols="50" rows="4" name="text"></textarea><br>
  <br>
  <input name="add" value="Prideti" type="submit" onclick="addkoment.php"><br>
</form>
</tr>
</table>


</body>
</html>