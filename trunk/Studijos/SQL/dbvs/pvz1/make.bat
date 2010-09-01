db2 prep hotel.sqc bindfile target c
db2 bind hotel.bnd
g:\msvc\msdev\bin\cl -Ig:\msvc\msdev\include hotel.c /link db2api.lib
