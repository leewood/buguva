Installation of JavaView (v.3.x)
   for Windows

History:
	17.05.05, 2.00 revised (kp) Revision and installation for Linux put in separate readme_linux.txt.
	06.06.04, 1.11 revised (kp) Comment on jar-separator sign added.
	31.12.03, 1.10 revised (kp) Classfile of tutorial firstApplication renamed to MyApplication.java from main.java.
	30.10.03, 1.00 created (kp) Initial publication as part of JavaView 3.0 based on a previous version.
Author:
	Konrad Polthier

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
	c:\temp

Result:
	c:\temp\javaview.zip
	c:\temp\jv_models.zip
	c:\temp\jv_tutor.zip


2. Use WinZip (or another compression tool) to unzip the files
to another local directory, say, to
	c:\JavaView
Note, the unzipping must keep relative paths and the case writing of filenames.

Result:
	c:\JavaView\readme.txt        (this readme file)
	c:\JavaView\sample.html
	c:\JavaView\sampleTorus.jvx
	c:\JavaView\...               (few other files)
	c:\JavaView\bin\javaview.bat 
	c:\JavaView\bin\javaview
	c:\JavaView\images\...        (some images)
	c:\JavaView\jars\javaview.jar
	c:\JavaView\jars\jvx.jar
	c:\JavaView\jars\vgpapp.jar
	c:\JavaView\jars\...          (some more html files)
	c:\JavaView\rsrc\...          (some language localizations)
	
	c:\JavaView\models\...        (optionally, some precomputed geometries)
	
	c:\JavaView\vgp\tutor\...     (optionally, some Java source code tutorials)
	c:\JavaView\vgp\tutor\firstApplet\MyApplet.java   (... for example)
      Note, up to now there exists is no *.class such as MyApplet.class in
      this installation. All the JavaView functionality is included in the
      three jars\*.jar libaries.

Possible reasons for failure:
   - during unzipping the relative path names are not kept, for example,
     in WinZip enable "Use folder names" on the 'Extract' dialog.
   - when using 'unzip' then use 'unzip -U' to keep the cases writing.


3. We are now ready for a few simple tests:
3a. Running JavaView as Applet
Check if JavaView runs as applet in your favorite browser:
   - Launch four favorite web browser (Internet Explorer, Netscape, Firefore, ...)
   - From the browser menu FILE -> OPEN select the file
        c:\JavaView\sample.html

Result: the browser should show a web page with text and a JavaView applet
        showing a simple cube. Your should be able to rotate the cube
        by pressing and dragging the left mouse in the cube.

Possible reasons for failure:
   - the web browser is not Java enabled.
   - Java is not installed on the computer.


3b. Running JavaView as Application
Check if your Java runtime works:
   - Open a DOS command window
   - Type:
        cd c:\JavaView
        java -cp jars/javaview.jar javaview
Note, in this example we specify a single jar-archive only. Therefore the
menu METHOD->EFFECT will be empty. Below we show how to specify more
JavaView archives.

Result: this should open the JavaView display showing the
        default snail geometry exactly as in MS-Test

Possible reasons for failure:
   - this test requires a proper installation of Sun JDK, any recent version.


3c. Check if the batch file works. This will simplify the invocation of JavaView:
   - Type:
        cd c:\JavaView
        bin\javaview.bat     (this will lauch the file 'javaview.bat')

Result: this should open the JavaView display showing the
        default snail geometry exactly as in 3b.


4. The following command launches a tutorial. The tutorial classes are
hereby taken from the JavaView libraries, that means, the Java sources
from jv_tutor.zip are still not used in this test.
  cd c:\JavaView
  java -cp jars/javaview.jar;jars/jvx.jar;jars/vgpapp.jar vgp.tutor.firstApplication.MyApplication

Result: this should show a JavaView display with a torus.


5. The following command launches an applet tutorial. The tutorial classes are hereby
taken from the JavaView libraries, that means, the Java sources from jv_tutor.zip
are still not used in this test.
  cd c:\JavaView
  appletviewer vgp\tutor\firstApplet\index.html

Result: this should show a JavaView display with a torus.
Explanation: In step 5. the JavaView libraries are specified within the applet tag
in the HTML page index.html.


The following steps will not use the tutorial functionality precompiled in vgpapp.jar
which is therefore not included in the classpath below. Instead, in section you
edit, compile and run the tutorial sources included in jv_tutor.zip:

6. Compile the first tutorial vgp.tutor.firstApplication:
  cd c:\JavaView
  javac -classpath jars/javaview.jar;jars/jvx.jar vgp\tutor\firstApplication\MyApplication.java

Notes:
  - the Sun-compiler 'javac' does not have the shortcut option '-cp'
    but one needs to write '-classpath'
  - the archive 'vgpapp.jar' is not included since we now want to work
    with the tutorial Java source.
  - the path to the source file uses the separator '\' instead of '.'.

7. Launch the compiled tutorial vgp.tutor.firstApplication.
  cd c:\JavaView
  java -cp ./;jars/javaview.jar;jars/jvx.jar vgp.tutor.firstApplication.MyApplication

Notes: the classpath is now modified to include the current directory './'. This
is necessary such that 'java' finds the compiled class. In order to locate the class
file vgp.tutor.firstApplication.MyApplication.class 'java' will search for a file
vgp\tutor\firstApplication\MyApplication.class starting in the current directory.

8. Edit the source file MyApplication.java to change the discretization of the torus.
For explanation I am using notepad but any other ASCII editor is fine.
  cd c:\JavaView
  notepad vgp\tutor\firstApplication\MyApplication.java

  Change the line
    geom.computeTorus(10, 10, 2., 1.);
  to
    geom.computeTorus(5, 5, 2., 2.);

  Save the file and quit notepad.

  Now repeat steps 6. and 7. to see that the torus has changed.

----------------------------------------------------------------------------------
