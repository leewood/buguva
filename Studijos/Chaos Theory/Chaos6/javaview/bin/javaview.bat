@echo off
rem # USAGE:    Run
rem #             javaview.bat [options] [geometry-file]
rem #           from the windows explorer or command line.
rem #           - the geometry-file is optional
rem #           - use a relative path name to the geometry file
rem #           - each option pair must be enclosed in quotes like "key=value"
rem #
rem # EXAMPLES: Switch to base of your JavaView installation
rem #             cd c:\Programme\JavaView\
rem #           Then run JavaView with a command like
rem #             bin\javaview.bat sample.obj
rem #           or include options such as
rem #             bin\javaview.bat "control=show" "panel=material" sample.obj
rem #             bin\javaview.bat "language=de" sample.obj
rem #
rem # NOTES:    - You may need to edit the variables below
rem #           - Optionally, put a link to this BAT file on your desktop,
rem #             and in the property panel of the link adjust the directory "Start in .."
rem #             to the JavaView base directory such as "c:\program files\JavaView\".
rem #           - You may also create a link to this BAT file in the "SendTo" folder of
rem #             your user profile (e.g. "c:\Documents and Settings\<username>\SendTo\").
rem #             This enables you to open a geometry file with JavaView by right-clicking
rem #             on a selected file, and choosing "send to --> javaview.bat" from the
rem #             pop-up menu. 
rem #
rem # PURPOSE:  launch JavaView, optionally with a 3d-geometry and command line args
rem #
rem # VERSION:  02.02, 2007-02-12 (fk) Use javaw.exe instead of java.exe to avoid console window.
rem #           02.01, 2007-01-02 (fk) Codebase set to parent directory of this file and doc adjusted.
rem #           02.00, 2006-11-12 (kp) Support for Microsoft Java and Win98 dropped.
rem #

rem # NORMAL MODE (for Windows NT and XP):
rem # 
rem # The default JavaView codebase folder is the parent directory of this file.
set jv_cb="%~dp0.."

rem # The variable jv_jar specifies the location of the JavaView archive "javaview.jar"
rem # The two archives "jvx.jar" and "vgpapp.jar" are optional.
set jv_jar=%jv_cb%/jars/javaview.jar;%jv_cb%/jars/jvx.jar;%jv_cb%/jars/vgpapp.jar;%jv_cb%

rem #
rem # We assume that Java is installed on your computer. Otherwise
rem # download and install the latest Java version from Sun (http://java.sun.com).
start javaw -cp %jv_jar% -Xmx512m javaview codebase=%jv_cb% %*

rem # 
rem # The following alternative command increases the memory available to the Java virtual machine
rem # and should be used if Java runs out of memory during a JavaView session.
rem # Here Java's default memory size of 64MB is increased to 1024MB. Adjust to your hardware capacitiy.
rem # start javaw -cp %jv_jar% -Xmx1024m javaview codebase=%jv_cb% %*

