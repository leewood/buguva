<html>
<head>
</head>
<body>
<?php
$query = $_GET['query'];
$dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");
$tquery = "UPDATE pictures SET description = 'aliasis jaspis,turmalitas,pasidabruoti intarpai. ' where id = 120";
//$tquery = $query;
print $tquery;
print "<BR>";
$result = mysql_query($tquery, $dbcon);
print $result;
$tquery = "UPDATE pictures SET small = 'nuotraukos2/HPIM0555-s.jpg' where id = 120";
$result = mysql_query($tquery, $dbcon);

print "<br>";
while ($row = mysql_fetch_object($result))
{
   print $row->id;
   print " ";
   print $row->name;
   print " ";
   print $row->small;
   print " ";
   print $row->description;
   
   print "\n<BR>";
}

?>
</body>
</html>

