#include "ClientInfo.h"

ClientInfo::ClientInfo()
{
   loginName = "none";
   loggedIn = false;
}




string encodeBytesMap(unsigned short int* map, int length)
{
   string result = "";
   for (int i = 0; i < length; i++)
   {
       int curByte = map[i];
       int lowSubByte = curByte % 16;
       int highSubByte = curByte / 16;
       result = subByte(highSubByte) + subByte(lowSubByte);
   }
}

string ClientInfo::encodeMap(string fileName)
{
    return "AFAAFB1234";
}


string clientNames[SOMAXCON];
int clientsCount = 0;

int findClientName(string name)
{
     for (int i = 0; i < clientsCount; i++)
     {
         if (clientNames[i] == name)
         {
            return i;
         }
     }
     return -1;
}

void ClientInfo::logoutClient(string name)
{
     int place = findClientName(name);
     loggedIn = false;
     if (place >= 0)
     {
         for (int i = place; i < clientsCount - 1; i++)
         {
             clientNames[i] = clientNames[i + 1];
         }
         clientsCount--;
     }
     place = mapPointer->findMap(mapName);
     mapPointer->maps[place].removeObject(myObjectID);
     server->updateID = myObjectID + 1;
}


#define LOGIN_OK 0
#define LOGIN_ERR1 1
#define LOGIN_ERR2 2

int ClientInfo::loginClient(string name)
{
    int place = findClientName(name);
    if (place < 0)
    {
       if (clientsCount < SOMAXCON - 1)
       {       
          clientNames[clientsCount] = name;
          clientsCount++;
          mapName = "1.map";
          x = 10;
          y = 10;
          (mapPointer->maps[0].objectsCount)++;
          myObjectID = mapPointer->maps[0].objectsCount - 1;
          objectDef obj;
          obj.x = 10;
          obj.y = 10;
          obj.type = 1;
          obj.ci = clientID;
          atk = 1;
          obj.atk = 1;
          def = 1;
          obj.def = 1;
          magic = 1;
          obj.magic = 1;
          health = 1;
          obj.health = 100;
          obj.name = name;
          mapPointer->maps[0].objects[myObjectID] = obj;
          return LOGIN_OK;
       } else {
          return LOGIN_ERR1;
       }
    }
    else
    {
        return LOGIN_ERR2;
    }
}

string intToStr(int data)
{
       stringstream ss;
       ss << data;
       string s;
       ss >> s;
       return s;
}




string ClientInfo::loadMap(string fileName, int id)
{
     return mapPointer->loadMap(fileName, id);
}

string ClientInfo::interpretCommand(string data)
{
    server->updateID = 0;
    string toUpdate = "";
    server->showInfo = true;
    if (currentMode < 100)
    {
        if (data == "DISCON:")
        {
            server->sendToClient(clientID, "DISCONNECT OK");
            server->clients[clientID].con = false;
            
            if (loggedIn)
            {
              logoutClient(loginName);            
              toUpdate = byteToStr(UPD_DEL) + byteToStr(myObjectID);
            }
            loggedIn = false;
            server->disconnect(clientID);
            currentMode = 0;
            cout << "Client " << clientID << " disconnected\n";                         
        }
        else if (data == "LOGIN:")
        {
             server->sendToClient(clientID, "LOGIN START");             
             currentMode = 100;
        }
        else if (data == "LOGOUT:")
        {
             loggedIn = false;
             server->sendToClient(clientID, "LOGGED OUT");
             logoutClient(loginName);
             toUpdate = byteToStr(UPD_DEL) + byteToStr(myObjectID);
             currentMode = 0;
             
        }
        else if (data == "LOGINNAME:")
        {
             if (!loggedIn)
             {
                server->sendToClient(clientID, "SERVER ERROR: NOT LOGGED IN");
             } 
             else 
             {
                server->sendToClient(clientID, loginName);
             }
        }
        else if (data == "MOVE:")
        {
             currentMode = 102;
             parsing = byteToStr(UPD_MOVE) + byteToStr(x) + byteToStr(y);
             server->sendToClient(clientID, "MOVE START, WAIT X:");
        }
        else if (data == "MAPSEND:")
        {
             currentMode = 101;
             server->sendToClient(clientID, "MAPSEND START");
        }
        else if (data == "REMOBJ:")
        {
             currentMode = 104;
             server->sendToClient(clientID, "OBJDEL START");
        }
        else if (data == "DROPOBJ:")
        {
             currentMode = 105;
             server->sendToClient(clientID, "OBJDROP START");
        }
        else if (data == "OBJUPDATE:")
        {
             currentMode = 106;
             server->sendToClient(clientID, "OBJUPD START");
        }
        else if (data == "UPDATE:")
        {
             if (updateData != "")
             {
                cout << clientID << "->Server: UPDATE:\n";
                server->sendToClient(clientID, updateData);                
             } 
             else 
             {
                server->showInfo = false;
                server->sendToClient(clientID, "N");    
             } 
             updateData = "";
        }
        else if (data == "LOADACCOUNT:")
        {
             currentMode = 103;
             x = 5;
             y = 5;
             
        }
        else
        {
            server->sendToClient(clientID, "SERVER ERROR: INCORRECT COMMAND");
        }
    } 
    else if (currentMode == 100)
    {
         currentMode = 0;
         int code = loginClient(data);
         if (code == 0)
         {
            loginName = data;
            server->sendToClient(clientID, "LOGIN OK");
            loggedIn = true;
            
            toUpdate = byteToStr(UPD_ADD) + byteToStr(x) + byteToStr(y) + byteToStr(1) + byteToStr(myObjectID) + byteToStr(atk) + byteToStr(def) + byteToStr(magic) + byteToStr(health) + fixedSizeString(loginName, 20);
         } 
         else 
         {
            server->sendToClient(clientID, "ERROR" + intToStr(code));
         }
    }
    else if (currentMode == 101)
    {
         currentMode = 0;
         string result = loadMap(data, myObjectID);
         server->sendToClient(clientID, result);  
    }
    else if (currentMode == 102)
    {
         currentMode = 103;
         stringstream ss;
         ss << data;
         ss >> x;
         parsing += byteToStr(x);
         server->sendToClient(clientID, "WAIT Y:");         
    }
    else if (currentMode == 103)
    {
         currentMode = 0;
         stringstream ss;
         ss << data;
         ss >> y;         
         parsing += byteToStr(y) + byteToStr(myObjectID);
         toUpdate = parsing;
         parsing = "";
         server->sendToClient(clientID, "MOVE OK");         
         int place = mapPointer->findMap(mapName);
         mapPointer->maps[place].objects[myObjectID].x = x;
         mapPointer->maps[place].objects[myObjectID].y = y;
    }
    else if (currentMode == 104)
    {
        currentMode = 0;
        stringstream ss;
        ss << data;
        int id;
        ss >> id;
        int place = mapPointer->findMap(mapName);
        mapPointer->maps[place].removeObject(id);        
        toUpdate = "02" + byteToStr(id);
        server->updateID = id + 1;
        server->sendToClient(clientID, "DELOBJ OK");         
    }
    else if (currentMode == 105)
    { 
         currentMode = 0;
         stringstream ss;
         ss << data;
         int id, atk, def, magic, health, x, y, type;
         string name;
         ss >> id >> atk >> def >> magic >> health >> type;
         int place = mapPointer->findMap(mapName);
         int count = mapPointer->maps[place].objectsCount;
         objectDef obj;
         obj.x = x;
         obj.y = y;
         obj.atk = atk;
         obj.def = def;
         obj.magic = magic;
         obj.health = health;
         obj.type = type;
         obj.name = name;
         mapPointer->maps[place].objects[count] = obj;
         (mapPointer->maps[place].objectsCount)++;
         toUpdate = "01" + byteToStr(x) + byteToStr(y) + byteToStr(type) + byteToStr(count) + byteToStr(atk) + byteToStr(def) + byteToStr(magic) + byteToStr(health) + fixedSizeString(name, 20);
         
    }
    else if (currentMode == 6)
    {
         currentMode = 0;
         stringstream ss;
         ss << data;
         int id, atk, def, magic, health, x, y, type;
         string name;
         ss >> atk >> def >> magic >> health >> type >> x >> y >> name;
         int place = mapPointer->findMap(mapName);
         mapPointer->maps[place].objects[id].atk = atk;
         mapPointer->maps[place].objects[id].def = def;
         mapPointer->maps[place].objects[id].magic = magic;
         mapPointer->maps[place].objects[id].health = health;
         x = mapPointer->maps[place].objects[id].x;
         y = mapPointer->maps[place].objects[id].y;
         toUpdate = "03" + byteToStr(id) + byteToStr(x) + byteToStr(y) + byteToStr(type)  + byteToStr(atk) + byteToStr(def) + byteToStr(magic) + byteToStr(health);
    }
    return toUpdate;
}
