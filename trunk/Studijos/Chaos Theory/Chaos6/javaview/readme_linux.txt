Installation of JavaView (v.3.0)
   for Linux

History:
	17.05.05, 2.00 revised (ep) Copied and adapted from original readme.txt.
	06.06.04, 1.11 revised (kp) Comment on jar-separator sign added.
	31.12.03, 1.10 revised (kp) Classfile of tutorial firstApplication renamed to MyApplication.java from main.java.
	30.10.03, 1.00 created (kp) Initial publication as part of JavaView 3.0 based on a previous version.
Author:
	Konrad Polthier, Eike Preuss

Steps 1-3 provide detailed instructions to install JavaView by hand
such that the JavaView application can be started.
Steps 4-8 describe how to compile/launch the first JavaView tutorial.

1. Download from
	http://www.javaview.de/download/index.html
the following files
	javaview.zip
	jv_models.zip                 (optionally, but recommended)
	jv_tutor.zip                  (optionally, includes source code of tutorials)
to a local directory on your machine, say,
	~/temp/

Result:
	~/temp/javaview.zip
	~/temp/jv_models.zip
	~/temp/jv_tutor.zip


2. Unzip the files to another local directory, say, to
	~/JavaView
with the commands
	mkdir ~/JavaView
	cd ~/JavaView
	unzip -U ~/temp/javaview.zip
	unzip -U ~/temp/jv_model.zip
	unzip -U ~/temp/jv_tutor.zip
Note, the unzipping must keep relative paths and the case writing of filenames.

Result:
	~/JavaView/readme.txt         (this readme file)
	~/JavaView/sample.html
	~/JavaView/sampleTorus.jvx
	~/JavaView/...                (few other files)
	~/JavaView/bin/javaview.bat
	~/JavaView/bin/javaview
	~/JavaView/images/...         (some images)
	~/JavaView/jars/javaview.jar
	~/JavaView/jars/jvx.jar
	~/JavaView/jars/vgpapp.jar
	~/JavaView/jars/...           (some html files)
	~/JavaView/rsrc/...           (some language localizations)

	~/JavaView/models/...         (optionally, some precomputed geometries)

	~/JavaView/vgp/tutor/...      (optionally, some Java source code tutorials)
	~/JavaView/vgp/tutor/firstApplet/MyApplet.java   (... for example)
      Note, up to now there exists is no *.class such
      as MyApplet.class in this installation. All the JavaView functionality is
      included in the three jars/*.jar libaries.

Possible reasons for failure:
   - during unzipping the relative path names are not kept

3. Please, do a few simple tests now:
3a. Check if JavaView runs in your browser:
   - Launch four favorite web browser (Internet Explorer, Netscape, Firefore, ...)
   - From the browser menu FILE -> OPEN select the file
        ~/JavaView/sample.html

Result: the browser should show a web page with text and a JavaView applet
        showing a simple cube. Your should be able to rotate the cube
        by pressing and dragging the left mouse in the cube.

Possible reasons for failure:
   - the webbrowser is not Java enabled.
   - Java is not installed on the computer.
   - the environment variable CLASSPATH is set on your system (by yourself
     or by some other Java package) and does not include './'.


3b. Running JavaView as Application
Check if your Java runtime works:
    cd ~/JavaView
    java -cp jars/javaview.jar javaview
Note, in this example we specify a single jar-archive only. Therefore the
menu METHOD->EFFECT will be empty. Below we show how to specify more
JavaView archives.

Result: this should open the JavaView display showing the
        default snail geometry

Possible reasons for failure:
   - this test requires a proper installation of Sun JDK, any recent version.
   - The PATH variable must include the bin directory of the jdk installation 
	  in order to be able to find the 'java' executable


3c. Check on Unix if the shell script file works which simplifies the invocation:
   Type:
        cd ~/JavaView
        bin/javaview     (this will lauch the shell script file 'javaview')

Result: this should open the JavaView display showing the
        default snail geometry exactly as in the Sun-Test.

Possible reasons for failure:
	- The script is not executable (error message 'permission denied');
	  Type:
        chmod u+x ~/JavaView/bin/javaview
	- The script has wrong encoding (error message 'bad interpreter'); 
	  Try one of the following commands:
        to_unix ~/JavaView/bin/javaview ~/JavaView/bin/javaview
        fromdos ~/JavaView/bin/javaview
        dos2unix ~/JavaView/bin/javaview

4. The following command launches a tutorial. The tutorial classes are
hereby taken from the JavaView libraries, that means, the Java sources
from jv_tutor.zip are still not used in this test.
  cd ~/JavaView
  java -cp jars/javaview.jar:jars/jvx.jar:jars/vgpapp.jar vgp.tutor.firstApplication.MyApplication

Result: this should show a JavaView display with a torus.


5. The following command launches an applet tutorial. The tutorial classes are hereby
taken from the JavaView libraries, that means, the Java sources from jv_tutor.zip
are still not used in this test.
  cd ~/JavaView
  appletviewer vgp/tutor/firstApplet/index.html

Result: this should show a JavaView display with a torus.
Explanation: In step 5. the JavaView libraries are specified within the applet tag
in the HTML page index.html.


The following steps will not use the tutorial functionality precompiled in vgpapp.jar
which is therefore not included in the classpath below. Instead, in section you
edit, compile and run the tutorial sources included in jv_tutor.zip:

6. Compile the first tutorial vgp.tutor.firstApplication:
  cd ~/javaView
  javac -classpath jars/javaview.jar:jars/jvx.jar vgp/tutor/firstApplication/MyApplication.java

Notes:
  - older versions of the Sun-compiler 'javac' do not have the shortcut option '-cp'
    but one needs to write '-classpath'
  - the archive 'vgpapp.jar' is not included since we now want to work
    with the tutorial Java source.
  - the path to the source file uses the separator '/' instead of '.'.

7. Launch the compiled tutorial vgp.tutor.firstApplication.
  cd ~/JavaView
  java -cp ./:jars/javaview.jar:jars/jvx.jar vgp.tutor.firstApplication.MyApplication

Notes: the classpath is now modified to include the current directory './'. This
is necessary such that 'java' finds the compiled class. In order to locate the class
file vgp.tutor.firstApplication.MyApplication.class 'java' will search for a file
vgp/tutor/firstApplication/MyApplication.class starting in the current directory.

8. Edit the source file MyApplication.java and change the discretization of the torus.
Any ASCII editor such as 'vi' is fine.
  cd ~/JavaView
  vi vgp/tutor/firstApplication/MyApplication.java

  Change the line
    geom.computeTorus(10, 10, 2., 1.);
  to
    geom.computeTorus(5, 5, 2., 2.);

  Save the file and quit th editor.

  Now repeat steps 6. and 7. to see that the torus has changed.

----------------------------------------------------------------------------------
