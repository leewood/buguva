SET Include=%Include%;C:\Program Files\SQLLIB\include
SET Lib=G:\MSVC\MSDEV\LIB;C:\Program Files\SQLLIB\lib

db2 prep programa.sqc bindfile target c
db2 bind programa.bnd

g:\msvc\msdev\bin\cl -Ig:\msvc\msdev\include programa.c /link db2api.lib
