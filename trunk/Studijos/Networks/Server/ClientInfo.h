#ifndef CLIENT_INFO
#define CLIENT_INFO

#include <string>
#include <iostream>
#include <sstream>
#include <fstream>


#include "Server.h"
#include "Map.h"
#include "tools.h"

#define UPD_MOVE 0
#define UPD_ADD 1
#define UPD_DEL 2
class ClientInfo
{
      string parsing;
      string loadMap(string fileName, int id);

      int loginClient(string name);
    public:
      void logoutClient(string name);
      string interpretCommand(string data);      
      ClientInfo();
      int clientID;
      NetworkServer* server;
      string encodeMap(string fileName);      
      int currentMode;
      string loginName;
      bool loggedIn;
      int x, y;
      int myObjectID;
      int atk, def, magic, health;
      string mapName;
      string updateData;
      Map* mapPointer;
      ClientInfo *otherClients;
};

#endif
