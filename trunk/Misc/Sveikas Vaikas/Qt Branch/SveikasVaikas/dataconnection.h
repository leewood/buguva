#ifndef DATACONNECTION_H
#define DATACONNECTION_H

#include <QObject>
#include <QNetworkAccessManager>
#include <QUrl>
#include <QNetworkRequest>
#include <QNetworkReply>
#include "mainwindow.h"

class DataConnection : public QObject
{
    Q_OBJECT
public:
    explicit DataConnection(QObject *parent = 0);
    void StartRequest(QString url, MainWindow* window);

signals:

public slots:
    void finishedSlot(QNetworkReply* reply);
private:
    QNetworkAccessManager* _nam;
    QNetworkReply* _reply;

    MainWindow* _window;

};

#endif // DATACONNECTION_H
