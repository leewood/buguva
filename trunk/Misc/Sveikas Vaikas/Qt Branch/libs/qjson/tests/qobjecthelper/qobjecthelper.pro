CONFIG  += qtestlib

DEFINES += QMAKE_BUILD

HEADERS = person.h

SOURCES = testqobjecthelper.cpp \
          person.cpp

LIBS += -lqjson
INCLUDEPATH +=  . \
                ../../src
