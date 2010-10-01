#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "Products.h"

namespace Ui {
    class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = 0);
    ~MainWindow();
    void SetLabel(QString test);
    void SetPicture(QString value);
    void UpdateByResult(CProducts products);
    QString LoadCSS(QString fileName);

private:
    Ui::MainWindow *ui;

private slots:
    void on_pushButton_clicked();
    void Resized(int i);
};

#endif // MAINWINDOW_H
