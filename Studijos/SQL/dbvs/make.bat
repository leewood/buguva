@SET Include=%Include%;C:\Program Files\SQLLIB\include;C:\Program Files\Microsoft Visual Studio\VC98\include;
@SET Lib=%Lib%;C:\Program Files\Microsoft Visual Studio\VC98\lib;C:\Program Files\SQLLIB\lib;
@SET Path=%Path%;C:\Program Files\Microsoft Visual Studio\VB98;
@echo Demesio! reikalingas mspdb60.dll (Ieskoti: C:\Program Files\Microsoft Visual Studio\VB98)
@db2 connect to biblio user stud using stud
db2 prep program.sqc bindfile target c
db2 bind program.bnd
@db2 connect reset 
"C:\Program Files\Microsoft Visual Studio\VC98\Bin\cl" program.c /link db2api.lib
