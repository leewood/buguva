SOURCES = testparser.cpp
CONFIG  += qtestlib

DEFINES += QMAKE_BUILD

LIBS += -lqjson
INCLUDEPATH +=  . \
                ../../src
