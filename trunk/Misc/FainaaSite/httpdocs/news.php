<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
  <meta content="text/html; charset=ISO-8859-1" http-equiv="content-type">
  <title>meniu</title>
</head>
<body
 style="color: rgb(192, 192, 192); background-color: rgb(0, 0, 0);"
 alink="#c0c0c0" link="#c0c0c0" vlink="#c0c0c0">
<table width = "70%">
<tr>
<td width="40%">
</td>
<td>
<?php
if (empty($_GET["which"]))
  {
     $st = 0;
  } else {
     $st = $_GET["which"];
  }
$filename = "news.dat";
$fp = @fopen($filename,"r"); 
if ($fp) { 
$nfile=fgets($fp,10); 
fclose($fp); 
} else {
$nfile=0; 
}
for ($x = $nfile - $st; (($x>0)&&($x > $nfile - 5 - $st)); $x--)
  {
    include("news/news$x.dat");
	echo "\n<HR>\n";
  }
 $sk = $st - 5;
 if ($st > 0)
   {
      echo "<a href = 'news.php?which=$sk'>|Naujesnes naujienos</a>";
   }
 $st = $st + 5;
 if ($st < $nfile)
   {
      echo "<a href = 'news.php?which=$st'>|Senesnes naujienos|</a>";
   } 
?>
</td>
</tr>
</table>
</body>
</html>
