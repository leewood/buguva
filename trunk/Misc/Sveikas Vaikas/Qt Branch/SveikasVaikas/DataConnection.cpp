#include "DataConnection.h"
#include "Products.h"
 #include <QEventLoop>
 #include <QTimer>

DataConnection::DataConnection(QObject *parent) :
    QObject(parent)
{
    //_nam = new QNetworkAccessManager(this);
    //QObject::connect(_nam, SIGNAL(finished(QNetworkReply*)), this, SLOT(finishedSlot(QNetworkReply*)));

}

void DataConnection::StartRequest(QString url, MainWindow* window)
{
    QUrl qurl(url);
    _window = window;
    //_reply = _nam->get(QNetworkRequest(qurl));
    QNetworkAccessManager manager;
    QEventLoop q;
    QTimer tT;
    tT.setSingleShot(true);
    connect(&tT, SIGNAL(timeout()), &q, SLOT(quit()));
    connect(&manager, SIGNAL(finished(QNetworkReply*)), &q, SLOT(quit()));
    QNetworkReply *reply = manager.get(QNetworkRequest(qurl));
    tT.start(5000); // 5s timeout
    q.exec();
    if(tT.isActive()){
        tT.stop();
        finishedSlot(reply);
    } else {
        // timeout
    }

}

void DataConnection::finishedSlot(QNetworkReply* reply)
{
    QVariant statusCodeV = reply->attribute(QNetworkRequest::HttpStatusCodeAttribute);
    QVariant redirectionTargetUrl = reply->attribute(QNetworkRequest::RedirectionTargetAttribute);    

    QString string;
    if (reply->error() == QNetworkReply::NoError)
    {        
        QByteArray bytes = reply->readAll();
        QString string2(bytes); // string
        string = string2;
    }
    else
    {
        string = "ERROR";
    }
    CProducts result(string, false);
    this->_window->UpdateByResult(result);
    //delete result;
    delete reply;
}
