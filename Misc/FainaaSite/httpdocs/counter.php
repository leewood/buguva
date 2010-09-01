<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head>
<?php
echo "<script type='text/javascript'>";
echo "var crt = '";
print date("F d, Y H:i:s", time()); 
echo "'</script>";
?>

<script type="text/javascript">

var dateobj=new Date(crt)
var imageclock=new Object()
	imageclock.digits=["c0.gif", "c1.gif", "c2.gif", "c3.gif", "c4.gif", "c5.gif", "c6.gif", "c7.gif", "c8.gif", "c9.gif", "cam.gif", "cpm.gif", "colon.gif"]
	imageclock.instances=0
	var preloadimages=[]
	for (var i=0; i<imageclock.digits.length; i++){ //preload images
		preloadimages[i]=new Image()
		preloadimages[i].src=imageclock.digits[i]
	}

	imageclock.imageHTML=function(timestring){ //return timestring (ie: 1:56:38) into string of images instead
		var sections=timestring.split(":")
		if (sections[0]=="0") //If hour field is 0 (aka 12 AM)
			sections[0]="12"
		else if (sections[0]>=13)
			sections[0]=sections[0]-12+""
		for (var i=0; i<sections.length; i++){
			if (sections[i].length==1)
				sections[i]='<img src="'+imageclock.digits[0]+'" />'+'<img src="'+imageclock.digits[parseInt(sections[i])]+'" />'
			else
				sections[i]='<img src="'+imageclock.digits[parseInt(sections[i].charAt(0))]+'" />'+'<img src="'+imageclock.digits[parseInt(sections[i].charAt(1))]+'" />'
		}
		return sections[0]+'<img src="'+imageclock.digits[12]+'" />'+sections[1]+'<img src="'+imageclock.digits[12]+'" />'+sections[2]
	}

	imageclock.display=function(){
		var clockinstance=this
		this.spanid="clockspan"+(imageclock.instances++)
		document.write('<span id="'+this.spanid+'"></span>')
		this.update()
		setInterval(function(){clockinstance.update()}, 1000)
	}

	imageclock.display.prototype.update=function(){
	
		
		var currenttime=dateobj.getHours()+":"+dateobj.getMinutes()+":"+dateobj.getSeconds() //create time string
		var currenttimeHTML=imageclock.imageHTML(currenttime)+'<img src="'+((dateobj.getHours()>=12)? imageclock.digits[11] : imageclock.digits[10])+'" />'
		document.getElementById(this.spanid).innerHTML=currenttimeHTML
        dateobj.setSeconds(dateobj.getSeconds()+1)
	}


</script>
  <meta content="text/html; charset=ISO-8859-1" http-equiv="content-type">
  <title>meniu</title>
</head>
<body
 style="color: rgb(192, 192, 192); background-color: rgb(0, 0, 0);"
 alink="#c0c0c0" link="#c0c0c0" vlink="#c0c0c0">
<center> 
<script type="text/javascript">

new imageclock.display()

</script>
<?php
$filename = "counter.dat";
$fp = @fopen($filename,"r"); 
if ($fp) { 
$counter=fgets($fp,10); 
fclose($fp); 
} else {
$counter=0; 
}
$counter++; 
echo "|Svetaine aplanke $counter zmones|";
$fp = fopen($filename,"w"); 
if ($fp) {
$counter=fputs($fp,$counter);
fclose($fp);
}
$filename = "time.dat";
$fp = @fopen($filename,"r"); 
if ($fp) { 
$counter=fgets($fp,15); 
fclose($fp); 
} else {
$counter=time();
$fp = fopen($filename,"w"); 
if ($fp) {
$pr=fputs($fp,$counter);
fclose($fp);
} 
}
$tm = time();
$kr = $tm - $counter; 
$xx = $kr;
$mn = $kr / 60;
$kr = $kr % 60;
$h2 = $mn / 60;
$day1 = $h2 / 24;
$h2 = $h2 % 24;
$h = (integer)($mn / 60);
$mn = $mn % 60;
$month1 = $day1 / 30;
$day1 = $day1 % 30;
$year1 = (integer)($month1 / 12);
$month1 = $month1 % 12 +1;
$day1++;
//echo " Svetaine veikia $h val. $mn min. $kr sec.|";
echo "<script type='text/javascript'>";
echo "currenttime = '$month1 $day1, $year1 $h2:$mn:$kr'";
echo "</script>";
?>
<script type="text/javascript">



var montharray=new Array("January","February","March","April","May","June","July","August","September","October","November","December")
var serverdate=new Date(currenttime)

function padlength(what){
var output=(what.toString().length==1)? "0"+what : what
return output
}

function displaytime(){
var x = ((serverdate.getMonth() + ((serverdate.getFullYear() - 1900) * 12) ) * 30 + serverdate.getDate()) - 1
serverdate.setSeconds(serverdate.getSeconds()+1)
var datestring=" "
var timestring=padlength(x)+" d. "+padlength(serverdate.getHours())+"h. "+padlength(serverdate.getMinutes())+" min. "+padlength(serverdate.getSeconds()) + "sec.|"
document.getElementById("servertime").innerHTML=datestring+" "+timestring
}

window.onload=function(){
setInterval("displaytime()", 1000)
}

</script>

Svetaine veikia <span id="servertime"></span>


</center>

</body>
</html>
