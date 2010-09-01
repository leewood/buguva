#include "Screen.h"

#include <constream>
using namespace conio;

void Screen::paintPixel(int x, int y, pixel pix)
{
     textcolor(pix.bgColor);
     textbackground(pix.color);
     putchxy(x, y, pix.symbol);
}

void Screen::paint()
{
     for (int i = 10; i < 50; i++)
     {
         for (int j = 0; i < 18; i++)
         {
             paintPixel(i, j, screenMap[i][j]);
         }
     }
}



using namespace std;

void Screen::window(int x, int y, int x2, int y2, int color, int textColor, string caption)
{
     _Setbk bgColor;
     _setcursortype(_NOCURSOR);
     bgColor.color = color;
     cout << bgColor;
     cout << setclr(textColor);
     cout << setxy(x, y);
     cout << "+";
     int length = caption.length();
     int cLen;
     string curCapt = caption;
     if (x2 - x + 1 >= length + 4)
     {
         cLen = length;
     } 
     else 
     {
        cLen = x2 - x + 1 - 4;
     }
     curCapt = caption.substr(0, cLen);
     cout << "-";
     cout << curCapt;
     for (int i = x + cLen + 2; i < x2; i++)
     {
         cout << "-";
     }
     cout << "+";
     
     for (int j = y + 1; j < y2; j++)
     {
       cout << setxy(x, j);
       cout << "|";       
       for (int i = x + 1; i < x2; i++)
       {
           cout << " ";
       }
       
       cout << "|";
     }
     cout << setxy(x, y2);
     cout << "+";
     for (int i = x + 1; i < x2; i++)
     {
         cout << "-";
     }
     cout << "+";
     
}


void Screen::window(int y, int y2, int color, int textColor, string caption)
{
     _Setbk bgColor;
     _setcursortype(_NOCURSOR);
     bgColor.color = color;
     cout << bgColor;
     cout << setclr(textColor);
     cout << setxy(1, y);
     cout << "+";
     int length = caption.length();
     int cLen;
     string curCapt = caption;
     if (80 - 1 + 1 >= length + 4)
     {
         cLen = length;
     } 
     else 
     {
        cLen = 80 - 1 + 1 - 4;
     }
     curCapt = caption.substr(0, cLen);
     cout << "-";
     cout << curCapt;
     for (int i = 0 + cLen + 2; i < 79; i++)
     {
         cout << "-";
     }     
     cout << "+";
     cout << "+";
     for (int i = 1; i < 79; i++)
     {
         cout << "-";
     }
     cout << "+";
     
     for (int j = y + 1; j < y2 - 1; j++)
     {
       cout << setxy(1, y + 1);
       insline();
       cout << "|";
       cout << setxy(80, y + 1);
       /*
       for (int i = 1; i < 79; i++)
       {
           cout << " ";
       }
       */
       cout << "|";
     }
//     putchxy(80, y2 - 1, '+');
     cout << setxy(2, y + 1);
}

void Screen::paintWindow()
{
   
       
}

int Screen::readKey()
{
      int res = kbhit();
      if (res > 0)
      {
         res = getch();
         if (res == 0)
         {
            kbhit();
            res = getch();
            res = res + 256;
                 
         }
         if (res == 224)
         {
            kbhit();
            res = getch();
            res = res + 256 + 224;
                 
         }
      }
      return res;
}





