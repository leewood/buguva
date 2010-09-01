#include "tools.h"

#ifndef SYSTEM_WIN_H
#define SYSTEM_WIN_H

class SystemWindow
{
      int linesNr;
      Screen screen;
      public:
             
      void initSystemWindow();
      void outputToSystemMessages(string data);
};

//SystemWindow sys;

#endif
