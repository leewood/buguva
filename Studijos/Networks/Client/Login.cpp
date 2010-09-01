#include "Login.h"

int Login::loginMode(NetworkClient* client, Inventory* inventory, Map* map, Me* me, SystemWindow* sys)
{
    string data = "";
    int result = 0;
    paintLoginScreen();
    while (result == 0)
    {
      string loginName = loginModeResult();    
      if ((loginName != "DISCON:") && (loginName != ""))
      {           
         client->sendToServer("LOGIN:");            
         data = client->receive();  
         client->sendToServer(loginName);            
         data = client->receive();  
         if (data == "ERROR2")
         {
            paintLoginError("Such login name already exists");          
         }
         else if (data == "ERROR1")
         {
            paintLoginError("Server is full");
            client->sendToServer("DISCON:");   
            data = client->receive();         
            result = -1;
         } 
         else 
         {
            result = 1;
            textbackground(BLACK);
            clrscr();
            inittextinfo();            
            sys->initSystemWindow();

            client->sendToServer("MAPSEND:");
            client->receive();  
            client->sendToServer("1.map");    
            data = client->receive();
            map->loadInicialData(data);
            /*            
            map->objectsCount++;
            map->objects[map->objectsCount - 1].x = me->myX;
            map->objects[map->objectsCount - 1].y = me->myY;
            map->objects[map->objectsCount - 1].type = OBJ_ME;
            me->myID = map->objectsCount - 1;              
            */
            me->myID = map->objectsCount;
            for (int i = 0; i < map->objectsCount; i++)
            {
                if (map->objects[i].type == OBJ_ME)
                {
                   me->myID = i;
                }
            }
            #ifdef DEBUG
              cout << "MyID ready";
            #endif
            me->myX = map->objects[me->myID].x;
            me->myY = map->objects[me->myID].y;
            me->myAttack = map->objects[me->myID].atk;
            me->myDefence = map->objects[me->myID].def;
            me->myMagic = map->objects[me->myID].magic;
            me->myHealth = map->objects[me->myID].health;            
            //map->paintObject(me->myX, me->myY, OBJ_ME);            
            #ifdef DEBUG
              cout << "PaintInventoryStart";
            #endif
            inventory->initInventory(me, sys, client, map);            
            
         }
      } 
      else 
      {
         if (loginName == "")
         {
            paintLoginError("Login name can't be blank");          
         } 
         else 
         {
            client->sendToServer("DISCON:");
            data = client->receive();
            result = -1;
         }
      }
    }
      
   return result; 
}

void Login::paintLoginScreen()
{
     clrscr();
     textcolor(BLUE);
     gotoxy(30, 2);
     cout << "Welcome To ZZT RPG";
     screen.window(20, 10, 60, 16, RED, WHITE, "Login:");
     gotoxy(25, 12);
     cout << "Login name:";
     gotoxy(25, 15);
     cout << "[F1 - CANCEL]  [Enter - OK]";
     
     gotoxy(25, 13);
     
     textbackground(BLACK);     
     cout << "                                ";
     gotoxy(25, 13);
}

string Login::loginModeResult()
{
       string current = "";
       int res = 0;
       while ((res != 13) && (res != 315))
       {
             res = screen.readKey();
             if (res == 8)
             {
                 current = current.substr(0, current.length() - 1);
                 gotoxy(25, 13);
     
                 textbackground(BLACK);     
                 cout << "                                ";
                 gotoxy(25, 13);
                 cout << current;

             }
             else if ((res > 30) && (res < 256))
             {
                  current = current + chr(res);
                  cout << chr(res);
             } else if (res > 0)
             {
             
/*         stringstream ss;
         ss << res;
         string s;
         ss >> s;
         s = "KBHIT: " + s;
         cout << s;
         */
             }
             
       }
       
       if (res == 315)
       {
          return "DISCON:";
       } 
       else 
       {
          return current;
       }

}

void Login::paintLoginError(string text)
{
     gotoxy(25, 17);
     textcolor(RED);
     cout << text;
     gotoxy(25, 13);
     textcolor(WHITE);
}
