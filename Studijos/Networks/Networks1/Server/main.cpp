#define _WIN32_WINNT 0x0501
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>

// Need to link with Ws2_32.lib, Mswsock.lib, and Advapi32.lib


#include <string>
#include <iostream>
#include <windows.h>
#include "Server.h"
#include "ClientInfo.h"
#include "Map.h"

using namespace std;

int __cdecl main(void) 
{

    NetworkServer server;
    Map mainMap;
    mainMap.init();

//    mainMap.addMap("1.map");
    mainMap.loadMap("1.map");
    ClientInfo clients[SOMAXCON];
    

    try
    {
    server.initSocket("27019");
    server.startListen();
    cout << "ZZT RPG Server Ready\nWaiting for connections...\n";
    while (true)
    {
      for (int i = 0; i < SOMAXCON; i++)
      {
         try
         { 
           if (!server.clients[i].con)
           {
             Sleep(10);
             server.acceptConnection(i);
             if (server.clients[i].con)
             {
                                              
               clients[i].clientID = i;
               clients[i].currentMode = 0;
               clients[i].server = &server;
               clients[i].loggedIn = false;
               clients[i].updateData = "";
               clients[i].loginName = "";
               clients[i].mapName = "";
               clients[i].otherClients = clients;
               clients[i].mapPointer = &mainMap;
             }
           }

           if (server.clients[i].con)
           {
             string data = server.receive(i);
            
             if (data != "")
             {
               if (data != "UPDATE:")
               {       
                 cout << i << "->Server: " << data << "\n";
               }
               server.updateID = 0;
               string toUpdate = clients[i].interpretCommand(data);
               
               if (server.updateID > 0)
               {
                  cout << "Updating IDs" << server.updateID << endl;
                  for (int k = 0; k < SOMAXCON; k++)
                  {
                      
                      if ((server.clients[k].con) && (clients[k].myObjectID > server.updateID))
                      {
                          (clients[k].myObjectID)--;
                      }
                      
                  }
                  server.updateID = 0;
               }
               
               if (toUpdate != "")
               {
                  for (int j = 0; j < SOMAXCON; j++)
                  {
                      if ((j != i) && (server.clients[j].con))
                      {
                          clients[j].updateData += toUpdate;
                      }
                  }
               }
              
             }
          }          
         } catch (ServerInitError &e)
         {
            if (e.isCritical())
            {
               throw e;
            }
            cout << e.error << endl;
            if (clients[i].loggedIn)
            {
               clients[i].logoutClient(clients[i].loginName);
            }
            clients[i].loggedIn = false;
            server.disconnect(i);
         }
       }
      }
    server.closeServer();
    } catch (ServerInitError &e)
    {
        cout << e.error << endl;
        server.closeServer();
        system("Pause");
    }
    return 0;
}



