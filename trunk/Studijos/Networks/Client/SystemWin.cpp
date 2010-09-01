
#include "SystemWin.h"

void SystemWindow::initSystemWindow()
{
    linesNr = 0;
    screen.window(18, 26, RED, WHITE, "[F1 - System messages]-[F2 - Disconnect]-[F3 - Logout]");     
}

void SystemWindow::outputToSystemMessages(string data)
{
     linesNr++;
     _Setbk bgColor;
     _setcursortype(_NORMALCURSOR);
     bgColor.color = RED;
     cout << bgColor;
     cout << setclr(WHITE);
   
   if (linesNr > 6)
   {
      gotoxy(2, 19);
      delline();
      gotoxy(1, 24);
      insline();     
      cout << "|";
      gotoxy(80, 24);
      cout << "|";
      gotoxy(2, 24);
   } 
   else 
   {
      gotoxy(2, 18 + linesNr);
   }
   cout << data;
}
