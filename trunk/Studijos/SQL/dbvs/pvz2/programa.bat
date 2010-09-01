db2 prep programa.sqc bindfile target c
db2 bind programa.bnd
i:\msvc\msdev\bin\cl -Ii:\msvc\msdev\include programa.c /link db2api.lib
