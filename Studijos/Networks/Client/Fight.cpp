
#include "Fight.h"

void Fight::closeFightMode()
{
     //paintFullMap();
     //paintObjects();
}

#define FIGHT_MODE_MONSTERS 1
#define FIGHT_MODE_USERS 2

void Fight::initFightMode(NetworkClient* client, SystemWindow* sys, Me* me, Inventory* inventory)
{
   this->client = client;
   this->sys = sys;
   this->me = me;
   this->inventory = inventory;
}

void Fight::clearFighters()
{
     fightersCount = 0;
}

void Fight::addFighter(objectDef obj)
{
     fighters[fightersCount] = obj;
     fightersCount++;
}

int Fight::openFightMode(int fightMode)
{
     Screen screen;
     int leftToKill = fightersCount;
     paintFightWindow();
     client->sendToServer("FIGHT:");
     client->receive();
     client->sendToServer(intToStr(fightMode) + " " + intToStr(me->myX) + " " + intToStr(me->myY));
     client->receive();
     bool end = false;
     while (!end)
     {
         int key = screen.readKey();
         if (key > 0)
         {       
                 int inventoryEvent = inventory->useEvent(key);
                 if (inventoryEvent >= 0)
                 {
                    inventory->useInventoryItem(inventoryEvent);
                    updateMyInfoInFightWindow();                                    
                 }
                 else if ((key >= 48) && (key <= 57))
                 {
                     int fighterID = key - 48;
                     objectDef fighter = fighters[fighterID];
                     if (me->myMagic + me->myAttack > fighter.atk + fighter.magic)
                     {
                         fighter.health -= me->myMagic + me->myAttack - fighter.atk - fighter.magic;            
                         if (fighter.health <= 0)
                         {
                            if (fightMode == FIGHT_MODE_USERS)
                            {
                               client->sendToServer("KILLED:");
                               client->receive();
                               client->sendToServer(intToStr(fighter.realID));
                               client->receive();
                            }
                            delFigtherFromFightWindow(fighterID);                
                            leftToKill--;
                            if (leftToKill == 0)
                            {
                                return 0;
                            }
                         }
                     } 
                     else 
                     {
                         me->myHealth -= -(me->myMagic + me->myAttack - fighter.atk - fighter.magic);
                     }
                     updateFightWindowInfo(fighterID, fighters[fighterID].atk, fighters[fighterID].def, fighters[fighterID].magic, fighters[fighterID].health);
                     updateMyInfoInFightWindow();
                                          
                 }
         }
         if (me->myHealth <= 0)
         {
            sys->outputToSystemMessages("You lose...");
            client->sendToServer("DISCON:");
            client->receive();
            return -1;
         }
     }
     return 0;
}

void Fight::updateMyInfoInFightWindow()
{
     gotoxy(12, 15);
     cout << "                                         ";
     gotoxy(12, 15);
     cout << "My Info - A:" << me->myAttack << " D:" << me->myDefence << " M:" << me->myMagic << " H:" << me->myHealth;      
}

void Fight::delFigtherFromFightWindow(int fighterID)
{
     int x = 12 + (fighterID % 3) * 20;
     int y = 4 + (fighterID / 3) * 3;
     gotoxy(x, y);
     cout << "                    ";
     gotoxy(x, y + 1);
     cout << "                    ";     
}

void Fight::paintFightWindow()
{
     int x, y;
     y = 4;
     x = 12;
     screen.window(10, 2, 60, 16, RED, WHITE, "Fight Window");
     for (int i = 0; i < fightersCount; i++)
     {
         gotoxy(x, y);
         cout << fighters[i].name;
         gotoxy(x, y + 1);
         cout << "A:" << fighters[i].atk << " D:" << fighters[i].def << " M:" << fighters[i].magic << " H:"<< fighters[i].health;
         x += 20;
         if (x >= 60)
         {
               x = 12;
               y += 3;
         }
     }
     
    updateMyInfoInFightWindow();
}

void Fight::updateFightWindowInfo(int fighterID, int atk, int def, int magic, int health)
{
     int x = 12 + (fighterID % 3) * 20;
     int y = 5 + (fighterID / 3) * 3;
     gotoxy(x, y);
     cout << "                    ";
     gotoxy(x, y);
     cout << "A:" << atk << " D:" << def << " M:" << magic << " H:"<< health;
}
