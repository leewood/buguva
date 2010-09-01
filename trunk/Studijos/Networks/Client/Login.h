#ifndef LOGIN_H
#define LOGIN_H
#include "tools.h"

class Login
{
      Screen screen;
      public:             
        void paintLoginScreen();
        string loginModeResult();
        void paintLoginError(string text);
        int loginMode(NetworkClient* client, Inventory* inventory, Map* map, Me* me, SystemWindow* sys);
      
};

#endif
