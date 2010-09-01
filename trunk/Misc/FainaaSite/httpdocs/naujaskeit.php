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

$failas = "naujaskeit.php";

function returnimages($dirname="./nuotraukos2") 
{
   $pattern="(\.jpg$)|(\.png$)|(\.jpeg$)|(\.gif$)";
   $files = array();
   $curimage=0;
   if($handle = opendir($dirname)) 
   {
       while(false !== ($file = readdir($handle)))
	   {
           if(eregi($pattern, $file))
		   {
                echo 'picsarray[' . $curimage .']="' . $file . '";'."\n";
                $curimage++;
            }
       }
       closedir($handle);
   }
   return($files);
}

function sqlimages()
{
    $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
    mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");


    $tquery = "SELECT id, parent, small, description from pictures";
    $result = mysql_query($tquery, $dbcon);
    $files = array();
    $curimage=0;    
    while ($row = mysql_fetch_object($result))
	{
    	echo 'picsarray[' . $row->id .']="' . $row->small . '";'."\n";   
		echo 'descarray[' . $row->id .']="' . $row->description . '";'."\n";   
		echo 'pararray[' . $row->id .']="' . $row->parent . '";'."\n";   
		echo 'idarray[' . $row->id .']="' . $row->id . '";'."\n";   
		$curimage++;
	}
    $tquery = "SELECT id, fullname from gallery";
    $result = mysql_query($tquery, $dbcon);
    $files = array();
	$curpar = 0;
	while ($row = mysql_fetch_object($result))
	{
    	echo 'galleryarray[' . $row->id .']="' . $row->fullname . '";'."\n";   
		if ($curpar < $row->id)
		  $curpar = $row->id;
	}
	echo "var maxg =$curpar;";
	mysql_close($dbcon);
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
	echo "Keisti: | <a href = '$failas?passw=$a&menu=1&action=1'> naujienas |";
	echo "<a href = '$failas?passw=$a&menu=3&action=1'> komentarus |\n";
	echo "</td>";
	echo "</tr><tr><BR>";
	echo "Dirbti su galerijomis: |<a href = '$failas?passw=$a&menu=10&action=1'>Sukurti nauja |</a>";
	echo "<a href = '$failas?passw=$a&menu=9&action=1'> Perkelti |</a>";
	echo "<a href = '$failas?passw=$a&menu=8&action=1'> Pasalinti |</a>";
	echo "</tr><tr><BR>";
	echo "Keisti nuostatas: |<a href = '$failas?passw=$a&menu=11&action=1'> Pagrindines nuostatos |</a>";
	echo "</tr><tr><BR>";	
	echo "Dirbti su nuotraukomis: |<a href = '$failas?passw=$a&menu=2&action=1'>Ideti nauja |</a>";
	echo "<a href = '$failas?passw=$a&menu=7&action=1'> Keisti senas |</a>";
	echo "</tr><tr><td>\n";

    if ($c == 1)
	  {
	     if ($b == 1)
		   {
		     // echo "<br><br><form method='get' action='$failas' name='gal' >Pranesimas:<br>\n";
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
		   } elseif ($b == 8) {
             echo "<br><br><form method='post' action='$failas' name='gal' ENCTYPE='multipart/form-data'>";

             echo "Kuria galerija:<input name='passw' value='$a' type='hidden'><input name='action' value='0' type='hidden'>\n";
             echo "<input name='menu' value='8'  type='hidden'>\n";

             $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
             mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");


             $tquery = "SELECT id, name, picture, fullname, description, parent from gallery";
             $result = mysql_query($tquery, $dbcon);
             echo "<br><select name='mid'>";
             while ($row = mysql_fetch_object($result))
             {
                print "<option value='$row->id'>$row->fullname</option>\n";
             };

             echo "</select><br><button value='Ideti'>Trinti</button><br></form>\n";


             mysql_close($dbcon);		   
		   } elseif ($b == 7){
		 
		   	 include("ins3.dat");
			 echo "<input name='passw' value='$a' type='hidden'>\n";
			 include("ins4.dat");
			 echo "<input name='passw' value='$a' type='hidden'>\n";
			 include("ins5.dat");
			 echo "<input name='passw' value='$a' type='hidden'>\n";
			 include("ins6.dat");
			 echo "<input name='passw' value='$a' type='hidden'>\n";
		     echo "</form></td></tr></table><br style='clear: left' />";
			 echo "<script type='text/javascript'>";
             $pathstring=pathinfo($_SERVER['PHP_SELF']);
             $locationstring="http://" . $_SERVER['HTTP_HOST'].$pathstring['dirname'] . "/nuotraukos2/";



             echo 'var locationstring="' . $locationstring . '";';
             echo 'var picsarray=new Array();';
			 echo 'var descarray=new Array();';
			 echo 'var idarray=new Array();';
			 echo 'var pararray=new Array();';
			 echo 'var galleryarray=new Array();';
             sqlimages();
			 echo "showaa(this);";
             echo "</script>";		
			 
		   
		   } elseif ($b == 9){
             echo "<br><br><form method='post' action='$failas' name='gal' ENCTYPE='multipart/form-data'>";

             echo "Kuria galerija:<input name='passw' value='$a' type='hidden'><input name='action' value='0' type='hidden'>\n";
             echo "<input name='menu' value='9'  type='hidden'>\n";

             $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
             mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");


             $tquery = "SELECT id, name, picture, fullname, description, parent from gallery";
             $result = mysql_query($tquery, $dbcon);
             echo "<br><select name='mid'>";
             while ($row = mysql_fetch_object($result))
             {
                print "<option value='$row->id'>$row->fullname</option>\n";
             };
             echo "</select><br>";
             echo "Kur:";
             $tquery = "SELECT id, name, picture, fullname, description, parent from gallery";
             $result = mysql_query($tquery, $dbcon);
             echo "<br><select name='pid'>";
             while ($row = mysql_fetch_object($result))
             {
                print "<option value='$row->id'>$row->fullname</option>\n";
             };


             echo "</select><br><button value='Ideti'>Perkelti</button><br></form>\n";
 

             mysql_close($dbcon);		   
		   } elseif ($b == 11) {
             echo "<br><br><form method='post' action='$failas' name='gal' ENCTYPE='multipart/form-data'>";
             $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
             mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");


             $tquery = "SELECT id, name, value from config";
             $result = mysql_query($tquery, $dbcon);	
             while ($row = mysql_fetch_object($result))
             {			 
                print "<input type = 'radio' name = 'id' value='$row->id'>$row->name = $row->value<BR>\n";
			 }
			 echo "Keisti i:<br>";
			 echo "<input type='text' name='value'><br>";
			 echo "<input type='hidden' name='passw' value='pazastys'>";
			 echo "<input type='hidden' name='menu' value='11'>";
			 echo "<input type='hidden' name='action' value='0'>";
		     echo "<br><button value='Ideti'>Pakeisti</button><br></form>\n";
			 mysql_close($dbcon);
			 
		   } elseif ($b == 10) {
             echo "<br><br><form method='post' action='$failas' name='gal'  ENCTYPE='multipart/form-data'>Galerijos paveiksliukas:<br>\n";
             echo "<input name='fname' type='file'><br>\n";
             echo "Pavadinimas:<br><input name='name'><br>\n";
             echo "Komentaras:<br><input name='koment'><br>\n";
             echo "Kokiai galerijai priklausys:<input name='passw' value='$a' type='hidden'><input name='action'  value='0'  type='hidden'>\n";
             echo "<input name='menu' value='10'  type='hidden'>\n";

             $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
             mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");


             $tquery = "SELECT id, name, picture, fullname, description, parent from gallery";
             $result = mysql_query($tquery, $dbcon);
             echo "<br><select name='parent'>";
             while ($row = mysql_fetch_object($result))
             {
                print "<option value='$row->id'>$row->fullname</option>\n";
             }
             echo "</select><br><button value='Ideti'>Ideti</button><br></form>\n";
             mysql_close($dbcon);		   
		   } elseif ($b == 2) {
		     echo "<br><br><form method='post' action='$failas' name='gal'  ENCTYPE='multipart/form-data'>Nuotrauka:<br>\n";
             echo "<input name='fname' type='file'><br>\n";
             echo "Jos sumazinta versija:<BR>\n";
             echo "<input name='fnames' type='file'><br>\n";
             echo "Komentaras:<br><input name='koment'><br>\n";
             echo "Kur ideti<input name='passw' value='$a' type='hidden'><input name='action' value='0'  type='hidden'>\n";
             echo "<input name='menu' value='2'  type='hidden'>\n";

             $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
             mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");


             $tquery = "SELECT id, name, picture, fullname, description, parent from gallery";
             $result = mysql_query($tquery, $dbcon);
             echo "<br><select name='gallery'>";
             while ($row = mysql_fetch_object($result))
             {
                print "<option value='$row->id'>$row->fullname</option>\n";
             }
             echo "</select><br><button value='Ideti'>Ideti</button><br></form>\n";
             mysql_close($dbcon);

		   } elseif ($b == 3) {
		      include("forum.php");
		   }
	  } else {
	     if ($b == 1) {
	       $duom = $_GET["charcount"];
	       $filename = "news.dat";
           $fp = @fopen($filename,"r"); 
           if ($fp) 
		   { 
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
	     } elseif ($b == 10) {
	       $gallery = $_POST['name'];
	       $parent = $_POST['parent'];
	       $koment = $_POST['koment'];
	       $uploaddir = "gallery"; // Where you want the files to upload to - Important: Make sure this folders permissions is 0777!

		   $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");

		   mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");

		   $tquery = "SELECT id, fullname from gallery where id = $parent";

		   $result = mysql_query($tquery, $dbcon);

		   $row = mysql_fetch_object($result);
           $ff = $row->fullname; 
		   print "$parent<br>\n";
           print "$ff<BR>\n";		   
		   $fullname = $ff."$gallery/";

          
		   $tquery = "INSERT INTO gallery VALUES(DEFAULT, '$gallery', $parent, '$fullname', 'none', '$koment');";
           print "$tquery<BR>\n";
		   $result = mysql_query($tquery, $dbcon);


		   $tquery = "SELECT id from gallery where fullname = '$fullname'";
           print "$tquery<BR>\n";   
		   $result = mysql_query($tquery, $dbcon);

		   $row = mysql_fetch_object($result);

		   $tid = $row->id;
           print "$tid<BR>\n";

		   if(is_uploaded_file($_FILES['fname']['tmp_name']))
           {
              $kk = "$uploaddir/gallery$tid.jpg";

			  move_uploaded_file($_FILES['fname']['tmp_name'],$kk);
           }
           $fss = $kk;

		   $tquery = "UPDATE gallery SET picture = '$fss' WHERE id = $tid";

		   $result = mysql_query($tquery, $dbcon);
	       mysql_close($dbcon);
           print "galerija $fullname sukurta";
	  
       	 } elseif ($b == 9) {
	       $pid = $_POST['pid'];
	       $mid = $_POST['mid'];	 
	       $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
           mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");
           $tquery = "UPDATE gallery SET parent = $pid where id = $mid;";
           $result = mysql_query($tquery, $dbcon); 
           $tquery = "SELECT fullname FROM gallery WHERE id = $pid";
           $result = mysql_query($tquery, $dbcon); 
           $row = mysql_fetch_object($result);
           $fn = $row->fullname;
           $tquery = "SELECT name FROM gallery WHERE id = $mid";
           $result = mysql_query($tquery, $dbcon); 
           $row = mysql_fetch_object($result);
           $fn = $fn . "$row->name/";
           $tquery = "UPDATE gallery SET fullname = '$fn' where id = $mid;";
           $result = mysql_query($tquery, $dbcon); 
 
           print "galerija $fn perkelta";
           mysql_close($dbcon);
       	 } elseif ($b == 6) {
	       $mid = $_GET['mid'];	 
	       $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
           mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");
           $tquery = "DELETE FROM pictures where id = $mid;";
           $result = mysql_query($tquery, $dbcon); 
 
           print "nuotrauka sunaikinta";
           mysql_close($dbcon);
       	 } elseif ($b == 5) {
	       $pid = $_GET['parent'];
	       $mid = $_GET['mid'];
           print "$pid<BR>$mid";		   
	       $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
           mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");
           $tquery = "UPDATE pictures SET parent = $pid where id = $mid;";
		   print "<BR>$tquery<br>";
           $result = mysql_query($tquery, $dbcon); 
 
           print "nuotrauka perkelta";
           mysql_close($dbcon);		   
       	 } elseif ($b == 4) {
	       $desc = $_GET['desc'];
	       $mid = $_GET['mid'];	 
		   print "$desc<BR>$mid";
	       $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
           mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");
           $tquery = "UPDATE pictures SET description = '$desc' where id = $mid;";
		   print "<BR>$tquery<BR>";
           $result = mysql_query($tquery, $dbcon); 
 
           print "nuotraukos aprasymas pakeistas";
           mysql_close($dbcon);		   
		   } elseif ($b == 11) {
		   
	       $mid = $_POST['id'];	 
		   $val = $_POST['value'];
	       $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
           mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");
           $tquery = "UPDATE config SET value = $val where id = $mid;";
           $result = mysql_query($tquery, $dbcon); 
 
           print "nuostatos pakeistos";
           mysql_close($dbcon);
		   
		   } elseif ($b == 8) {
	       $mid = $_POST['mid'];	 
	       $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
           mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");
           $tquery = "DELETE from pictures where id = $mid;";
           $result = mysql_query($tquery, $dbcon); 
 
           print "galerija pasalinta";
           mysql_close($dbcon);
         } elseif ($b == 2) {
	       $gallery = $_POST['gallery'];
	       $koment = $_POST['koment'];
	       $uploaddir = "nuotraukos2"; // Where you want the files to upload to - Important: Make sure this folders permissions is 0777!

		   if(is_uploaded_file($_FILES['fname']['tmp_name']))
           {
              move_uploaded_file($_FILES['fname']['tmp_name'],$uploaddir.'/'.$_FILES['fname']['name']);
           }
           if(is_uploaded_file($_FILES['fnames']['tmp_name']))
           {
              move_uploaded_file($_FILES['fnames']['tmp_name'],$uploaddir.'/'.$_FILES['fnames']['name']);
           }
           $fss = $_FILES['fnames']['name'];
 

           $dbcon = mysql_connect("sql106.byethost9.com", "b9_854338", "pazastys") or die("No server");
           mysql_select_db("b9_854338_fainaa", $dbcon) or die("Error selecting db");
 

           $tquery = "INSERT INTO pictures VALUES(DEFAULT, '$gallery', 'nuotraukos2/$fss', 'nuotraukos2/$fss', '$koment');";
           $result = mysql_query($tquery, $dbcon);


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
