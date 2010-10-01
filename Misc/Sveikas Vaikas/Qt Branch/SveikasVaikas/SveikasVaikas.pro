#-------------------------------------------------
#
# Project created by QtCreator 2010-09-27T09:01:45
#
#-------------------------------------------------

QT       += core gui network

LIBS += -lqjson -llibzbar-0
INCLUDEPATH += C:\NokiaQtSDK\qjson\src

TARGET = SveikasVaikas
TEMPLATE = app


SOURCES += main.cpp\
        mainwindow.cpp \
    DataConnection.cpp \
    Products.cpp \
    Product.cpp \
    Conservant.cpp \
    Category.cpp

HEADERS  += mainwindow.h \
    DataConnection.h \
    Product.h \
    Conservant.h \
    Category.h \
    Products.h \
    MultiLanguageStrings.h

FORMS    += mainwindow.ui

CONFIG += mobility
MOBILITY = 

symbian {
    TARGET.UID3 = 0xe5f80abe
    # TARGET.CAPABILITY += 
    TARGET.EPOCSTACKSIZE = 0x14000
    TARGET.EPOCHEAPSIZE = 0x020000 0x800000
}

RESOURCES += \
    Pictures.qrc

OTHER_FILES += \
    Default.css \
    DefaultLandscape.css
