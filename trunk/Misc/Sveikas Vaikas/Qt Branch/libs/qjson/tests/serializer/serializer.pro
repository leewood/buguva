SOURCES = testserializer.cpp
CONFIG  += qtestlib

DEFINES += QMAKE_BUILD

LIBS += -lqjson
INCLUDEPATH +=  . \
                ../../src
