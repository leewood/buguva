<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
  <meta content="text/html; charset=ISO-8859-1" http-equiv="content-type">
  <title>meniu</title>
</head>
<body
 style="color: rgb(192, 192, 192); background-color: rgb(0, 0, 0);"
 alink="#c0c0c0" link="#c0c0c0" vlink="#c0c0c0">
<?php
function returnimages($dirname="./nuotraukos2") {
	 $pattern="(\.jpg$)|(\.png$)|(\.jpeg$)|(\.gif$)";
   $files = array();
	 $curimage=0;
   if($handle = opendir($dirname)) {
       while(false !== ($file = readdir($handle))){
               if(eregi($pattern, $file)){
                     echo 'picsarray[' . $curimage .']="' . $file . '";'."\n";
                     $curimage++;
               }
       }

       closedir($handle);
   }
   return($files);
}
if (empty($_GET["passw"]))
 {
  $a = $_POST["passw"];
$b = $_POST["menu"];
$c = $_POST["action"];
} else {
$a = $_GET["passw"];
$b = $_GET["menu"];
$c = $_GET["action"];
}
if ($a == "pazastys")
  {
    echo "<table border = '0'  width = '100%'><tr><td>\n";
	echo "Keisti: | <a href = 'login.php?passw=$a&menu=1&action=1'> naujienas |";
	echo "<a href = 'login.php?passw=$a&menu=2&action=1'> galerija |";
	echo "<a href = 'login.php?passw=$a&menu=3&action=1'> komentarus |\n";
	echo "</td></tr><tr><td>\n";
    if ($c == 1)
	  {
	     if ($b == 1)
		   {
		     // echo "<br><br><form method='get' action='login.php' name='gal' >Pranesimas:<br>\n";
			 // echo "<textarea cols='25' rows='10' name='pran'></textarea>";
			 // echo "<input name='passw' value='$a' type='hidden'><input name='action' value='0'  type='hidden'>\n";
			 // echo "<input name='menu' value='1'  type='hidden'>\n";
			 // echo "<BR><button value='Ideti'>Ideti</button><br></form>\n";
			 include("ins1.dat");
			 echo "<input name='passw' value='$a' type='hidden'>\n";
			 include("ins2.dat");
			 echo "<script type='text/javascript'>";
$pathstring=pathinfo($_SERVER['PHP_SELF']);
$locationstring="http://" . $_SERVER['HTTP_HOST'].$pathstring['dirname'] . "/nuotraukos2/";



echo 'var locationstring="' . $locationstring . '";';
echo 'var picsarray=new Array();';
returnimages();
echo "</script>";
		   } elseif ($b == 2) {
		      echo "<br><br><form method='post' action='login.php' name='gal' ENCTYPE='multipart/form-data'>Nuotrauka:<br>\n";
echo "<input name='fname' type='file'><br>\n";
echo "Jos sumazinta versija:<BR>\n";
echo "<input name='fnames' type='file'><br>\n";
echo "Komentaras:<br><input name='koment'><br>\n";
echo "Kur ideti<input name='passw' value='$a' type='hidden'><input name='action' value='0'  type='hidden'>\n";
echo "<input name='menu' value='2'  type='hidden'>\n";
echo "<br><select name='gallery'><option value='1'>auskarai</option>\n";
echo "<option value='2'>paveikslai</option><option value='3'>kita</option>\n";
echo "</select><br><button value='Ideti'>Ideti</button><br></form>\n";

		   } elseif ($b == 3) {
		      include("forum.php");
		   }
	  } else {
	  if ($b == 1) {
	  $duom = $_GET["charcount"];
	  $filename = "news.dat";
$fp = @fopen($filename,"r"); 
if ($fp) { 
$nfile=fgets($fp,10); 
fclose($fp); 
} else {
$nfile=0; 
}
$nfile++;
$fin = "news/news$nfile.dat";
$fp = fopen($fin, "w");
fwrite($fp, "<small><small><u>");
fwrite($fp, date("F d, Y H:i:s", time()));
fwrite($fp, "</u></small></small><BR>");
fwrite($fp, $duom);
fclose($fp);
$fp = fopen($filename, "w");
fwrite($fp, $nfile);
fclose($fp);
echo "<BR> Naujienos papildytos";	  
	  
	  
	  
	  } 
	  elseif ($b == 2) {
	  $gallery = $_POST['gallery'];
	  $koment = $_POST['koment'];
	  $uploaddir = "nuotraukos2"; // Where you want the files to upload to - Important: Make sure this folders permissions is 0777!
// ==============
// Upload Part
// ==============
if(is_uploaded_file($_FILES['fname']['tmp_name']))
{
move_uploaded_file($_FILES['fname']['tmp_name'],$uploaddir.'/'.$_FILES['fname']['name']);
}
if(is_uploaded_file($_FILES['fnames']['tmp_name']))
{
move_uploaded_file($_FILES['fnames']['tmp_name'],$uploaddir.'/'.$_FILES['fnames']['name']);
}
$fss = $_FILES['fnames']['name'];
$filename = "gallery$gallery.dat"; 
$fp = @fopen($filename,"r"); 
if ($fp) { 
$counter=fgets($fp,10); 
fclose($fp); // Uždaromas failas
} else {
$counter=0; 
}
$counter++; 
//echo $counter; 
if ($counter > 2) $counter = 0;
$cnt = $counter;
$fp = fopen($filename,"w"); 
if ($fp) {
$counter=fputs($fp,$counter);
fclose($fp);
}

$d = fopen("gallery$gallery.php", "a");
fwrite($d , "<img alt='nera' title='$koment' onclick='zoom(this);' src='nuotraukos2/$fss'>\n");
if ($cnt == 0) fwrite($d, "<BR>\n");
fclose($d);


$filename = "news.dat";
$fp = @fopen($filename,"r"); 
if ($fp) { 
$nfile=fgets($fp,10); 
fclose($fp); 
} else {
$nfile=0; 
}
$nfile++;
$fin = "news/news$nfile.dat";
$fp = fopen($fin, "w");
fwrite($fp, "<small><small><u>");
fwrite($fp, date("F d, Y H:i:s", time()));
fwrite($fp, "</u></small></small><BR>");
fwrite($fp, "Galerijoje nauja nuotrauka <img src='nuotraukos2/$fss'>");
fclose($fp);
$fp = fopen($filename, "w");
fwrite($fp, $nfile);
fclose($fp);
print "<BR>Na ka rodos pavyko ideti, valio!!! :D Naujienas irgi papildziau, URAAA!!! :D:D";
}
	  }
	
	
	
	echo "</td></tr></table>\n";
  } else {
    echo("Klaida, neteisingas slaptazodis");
  }

?>

</body>
</html>
