#ifndef SCREEN_H
#define SCREEN_H
#include <conio2.h>
#include <string>
#include <iostream>
#include <sstream>

using namespace std;

struct pixel
{
    int color;
    char symbol;
    int bgColor;
};

class Screen
{
      pixel screenMap[80][25];
      void paint();
      void paintPixel(int x, int y, pixel pix);
      void paintWindow();
      
      public:
      void window(int x, int y, int x2, int y2, int color, int textColor, string caption);
      void window(int y, int y2, int color, int textColor, string caption);
      int readKey();
};

#endif
