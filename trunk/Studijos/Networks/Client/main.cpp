

#include "tools.h"



NetworkClient client;
Inventory inventory;
SystemWindow sys;
Me me;
Map map(&me);



int interpretObjects(int x, int y, int mode)
{
     int objectsInPlace[30];
     int fightType = 0;
     int count = map.objectsInXYCount(x, y);
     for (int i = 0; i < count; i++)
     {
         objectsInPlace[i] = map.objectsInXY(x, y, i + 1);
     }
     switch(mode)
     {
        case MODE_MOVE:
        {
           bool isMonster = false;
           for (int i = 0; i < count; i++)
           {
              if (objectsInPlace[i] == OBJ_MONSTER)
              {
                 isMonster = true;
              }
           }
           if (isMonster)
           {
              sys.outputToSystemMessages("MONSTER FOUND, FIGHT START");
              mode = 2;
              fightType = 1;
           }
        }
        break;
        case MODE_ACTIVATE:
        {
           bool isPlayer;
           for (int i = 0; i < count; i++)
           {
              if (objectsInPlace[i] == OBJ_USER)
              {
                 isPlayer = true;
              }
              
           }
           int isItem = -1;
           
           for (int i = count - 1; i >= 0; i--)
           {
              if ((objectsInPlace[i] >= OBJ_HEALTH_POINT) && (objectsInPlace[i] <= OBJ_MAGIC_WAND))
              {
                 isItem = i;
                 break;
              }
           }
           if (isPlayer)
           {
              sys.outputToSystemMessages("PLAYER FOUND, FIGHT START");
              mode = 2;
              fightType = 2;
           } 
           else if (isItem >= 0)
           {
              inventory.takeItem();  
           }
                      
        }
     }
     return fightType;
}

Login login;
Fight fight;
int fightType;

int __cdecl main(int argc, char **argv) 
{
    int inactivity = 0;
    if (argc != 2) {
        printf("usage: %s server-name\n", argv[0]);
        system("pause");
        return 1;
    }
    int mode = 0;
    me.initMe(&map, &client);
    Screen screen;
    string server;
    server.assign(argv[1]);
    client.initSocket(server, "27019");    
    if (client.connectToServer() > 0) return 1;
    string data = "";
    string errorText = "";
    bool loginEnd = false;
    bool loginContinue = false;
    fight.initFightMode(&client, &sys, &me, &inventory);
    while (data != "DISCONNECT OK")
    {

       if (mode == 0)
       {
           int result = login.loginMode(&client, &inventory, &map, &me, &sys);
           mode = 1;
           if (result < 0)
           {
              data = "DISCONNECT OK"; 
           }
       }
       else if (mode == 2)
       {
           switch (fightType)
           {
              case 1:
                   fight.clearFighters();
                   for (int i = map.objectsCount - 1; i >= 0; i--)
                   {
                       if (map.objects[i].type == OBJ_USER)
                       {
                         fight.addFighter(map.objects[i]);
                       }
                   }
                   break;
              case 2:
                   fight.clearFighters();
                   for (int i = map.objectsCount - 1; i >= 0; i--)
                   {
                       if (map.objects[i].type == OBJ_MONSTER)
                       {
                         fight.addFighter(map.objects[i]);
                       }
                   }
                   break;
                   
           }
           int result = fight.openFightMode(fightType);           
           mode = 1;
           if (result < 0)
           {
              data = "DISCONNECT OK";
           } 
           else 
           {
              map.paintFullMap();
              map.paintObjects();
           }
       } else {                 
         int res = screen.readKey();
         if (res > 0)
         {         
            stringstream ss;
            ss << res;
            string s;
            ss >> s;
            s = "KBHIT: " + s;
            int inventoryEvent = inventory.useEvent(res);
            if (inventoryEvent > -1)
            {
               inventory.useInventoryItem(inventoryEvent);
            }                               
            else if ((res >= 48) && (res <= 57))
            {
               inventory.dropItem(res - 48);
            }            
            else if (res == 13)
            {
               fightType = interpretObjects(me.myX, me.myY, MODE_ACTIVATE);
            }
            if (res == 315)
            {
               sys.outputToSystemMessages("?:");
               string toSend = "";
               cin >> toSend;
               client.sendToServer(toSend);    
               data = client.receive();
               if (data != "MAPSEND START") 
               {
                 sys.outputToSystemMessages(">" + data);
               } 
               else 
               {
                 sys.outputToSystemMessages("?:");
                 string toSend = "";
                 cin >> toSend;
                 client.sendToServer(toSend);    
                 data = client.receive();
                 map.loadInicialData(data);                 
               }                  
            } 
            else if (res == 555)
            {
              me.makeAMove(me.myX - 1, me.myY);                      
              fightType = interpretObjects(me.myX, me.myY, MODE_MOVE);
            }
            else if (res == 557)
            {
              me.makeAMove(me.myX + 1, me.myY);
              fightType = interpretObjects(me.myX, me.myY, MODE_MOVE);              
            }
            else if (res == 552)
            {
              me.makeAMove(me.myX, me.myY - 1);
              fightType = interpretObjects(me.myX, me.myY, MODE_MOVE);              
            }
            else if (res == 560)
            {
              me.makeAMove(me.myX, me.myY + 1);
              fightType = interpretObjects(me.myX, me.myY, MODE_MOVE);              
            }          
            else if (res == 316)
            {
              client.sendToServer("DISCON:");
              data = client.receive();
            }    
            else if (res == 317)
            {
              client.sendToServer("LOGOUT:");
              data = client.receive();
              mode = 0;  
            }             
            else 
            {
              sys.outputToSystemMessages(s);
            }          
      } else {
         inactivity++;
         if (inactivity > 300)
         {
            inactivity = 0;
            if (client.sendToServer("UPDATE:") > 0)
            {
               sys.outputToSystemMessages("SERVER ERROR, DISCONNECTING:\n");                                
               system("pause");
               return 1;
            }
            data = client.receive();
            map.analizeUpdate(data);
//            sys.outputToSystemMessages(">" + data);
         }
      }
      }
    }
    system("pause");
    
    return 0;
}
