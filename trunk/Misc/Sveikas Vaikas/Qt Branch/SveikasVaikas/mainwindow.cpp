#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "dataconnection.h"
#include "Product.h"
#include <QFile>
#include <QTextStream>
#include <QIODevice>
#include <QDesktopWidget>
#include <zbar.h>


MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);    
    QObject::connect(QApplication::desktop(), SIGNAL(resized(int)), this, SLOT(Resized(int)));
    Resized(0);
}

void MainWindow::Resized(int i)
{


   QDesktopWidget* desktop = QApplication::desktop();
   QWidget* wi = desktop->screen(i);
   if (wi->width() > 500)
   {
       QString s = LoadCSS(":/DefaultLandscape.css");
       this->setStyleSheet(s);
   }
   else
   {
       QString s = LoadCSS(":/Default.css");
       this->setStyleSheet(s);
   }
}

QString MainWindow::LoadCSS(QString fileName)
{
    QFile myFile(fileName);
    QString s;
    if (!myFile.open(QIODevice::ReadOnly))
    {
        s = "Error";
    }
    else
    {
      QTextStream stream( &myFile );
      s = stream.readAll();

    }
    myFile.close();
    return s;
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::SetLabel(QString test)
{
    //this->ui->label->setText(test);
}

void MainWindow::SetPicture(QString value)
{
    this->ui->pictureLabel->setStyleSheet("image: url(:/new/prefix1/rc/" + value + ");");
}

void MainWindow::UpdateByResult(CProducts products)
{
    if (products.HasError())
    {
       this->ui->dataLabelHazard->setText(products.GetHazardMessage());
       this->ui->dataLabelManufacturer->setText("");
       this->ui->dataLabelProduct->setText("");
       this->ui->pictureLabel->setStyleSheet("image: url(:/new/prefix1/rc/busy.bmp);");
    }
    else
    {
        CProduct product = products.GetProduct(0);
        this->ui->dataLabelProduct->setText(product.GetName());
        this->ui->dataLabelManufacturer->setText(product.GetCompany());
        this->ui->dataLabelHazard->setText(product.GetHazardMessage());
        this->ui->pictureLabel->setStyleSheet("image: url(:/new/prefix1/rc/" + product.GetHazardImage() + ");");
    }
}

void MainWindow::on_pushButton_clicked()
{
    unsigned major, minor;
//    zbar::zbar_version(&major, &minor);
    DataConnection data;
    data.StartRequest("http://sveikasvaikas.lt/spring/products/byBarcode.json?q=4770237049528", this);
}
