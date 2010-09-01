#include "Inventory.h"

int Inventory::useEvent(int key)
{
    for (int i = 0; i < 10; i ++)
    {
        if (inventoryUseKeys[i] == key)
        {
           return i;
        }
    }
    return -1;
}

void Inventory::initInventory(Me* me, SystemWindow* sys, NetworkClient* client, Map* map)
{

   inventoryUseKeys[0] = 41;  
   inventoryUseKeys[1] = 33;  
   inventoryUseKeys[2] = 64;  
   inventoryUseKeys[3] = 35;  
   inventoryUseKeys[4] = 36;  
   inventoryUseKeys[5] = 37;  
   inventoryUseKeys[6] = 94;  
   inventoryUseKeys[7] = 38;  
   inventoryUseKeys[8] = 42;  
   inventoryUseKeys[9] = 40;                             
   inventoryItemsCount = 0;
   this->me = me;
   this->sys = sys;
   this->client = client;
   this->map = map;
   screen.window(60, 1, 80, 17, BLUE, WHITE, "Inventory");
   inventoryCaptionLine("Level:", 1);
   inventoryCaptionLine("Attack lvl:", 2);
   inventoryCaptionLine("Defence lvl:", 3);
   inventoryCaptionLine("Magic lvl:", 4);
   inventoryCaptionLine("Health:", 5);    
   inventoryCaptionLine("  [Items]", 7);
   inventoryDataLine(intToStr(me->myAttack + me->myDefence + me->myMagic), 1);
   inventoryDataLine(intToStr(me->myAttack), 2);
   inventoryDataLine(intToStr(me->myDefence), 3);
   inventoryDataLine(intToStr(me->myMagic), 4);
   inventoryDataLine(intToStr(me->myHealth), 5);

}

void Inventory::useInventoryItem(int itemNr)
{
     if (itemNr < inventoryItemsCount)
     {
       objectDef obj = inventory[itemNr];
       switch (obj.type)
       {
        case OBJ_SWORD:
             if (me->hasGun)
             {
                 addToInventory(me->gun);
             }
             me->hasGun = true;
             me->gun = obj;
             me->myAttack += obj.atk;
     
             break;
        case OBJ_SHIELD:
             if (me->hasShield)
             {
                addToInventory(me->shield);
             }
             me->hasShield = true;
             me->shield = obj;
             me->myDefence += obj.def;
             break;
        case OBJ_MAGIC_WAND:
             if (me->hasMagicWand)
             {
                addToInventory(me->wand);
             }
             me->hasMagicWand = true;
             me->wand = obj;
             me->myMagic += obj.magic;
             break;
        case OBJ_HEALTH_POINT:
             me->myHealth += obj.health;
             break;
       }
       client->sendToServer("OBJUPDATE:");
       client->receive();
       client->sendToServer(intToStr(me->myID) + " " + intToStr(me->myAttack) + " " + intToStr(me->myDefence) + " " + intToStr(me->myMagic) + " " + intToStr(me->myHealth));
       client->receive();     
       removeInventoryItem(itemNr);
       updateMyInfoInInventory();
     } 
     else
     {
        sys->outputToSystemMessages("There is not any item in slot " + intToStr(itemNr));
     }
}


void Inventory::dropItem(int itemNr)
{
     itemNr = itemNr + 1;
     if ((itemNr <= inventoryItemsCount) && (itemNr >= 0))
     {   
         objectDef obj;
         obj.atk = inventory[itemNr].atk;
         obj.def = inventory[itemNr].def;
         obj.magic = inventory[itemNr].magic;
         obj.health = inventory[itemNr].health;
         obj.x = inventory[itemNr].x;
         obj.y = inventory[itemNr].y;
         obj.type = inventory[itemNr].type;
         obj.name = inventory[itemNr].name;
         removeInventoryItem(itemNr);
         map->objects[map->objectsCount] = obj;
         map->objectsCount++;
         map->paintObject(me->myX, me->myY, obj.type);
         cout << "Done4";
         client->sendToServer("DROPOBJ:");
         client->receive();
         client->sendToServer(intToStr(obj.atk) + " " + intToStr(obj.def) + " " + intToStr(obj.magic) + " " + intToStr(obj.health) + " " + intToStr(obj.type) + " " + intToStr(obj.x) + " " + intToStr(obj.y) + " " + obj.name);
         client->receive();     
         cout << "Done3";
     } 
     else
     {
        sys->outputToSystemMessages("There is not any item in slot " + intToStr(itemNr));
     }     
}

void Inventory::takeItem()
{
    
    objectDef obj;
    int result = map->objectsCount;
    for (int i = map->objectsCount - 1; ((i >= 0) && (result == map->objectsCount)); i--)
    {        
        objectDef tmp;
        tmp.type = map->objects[i].type;
        tmp.x = map->objects[i].x;
        tmp.y = map->objects[i].y;
        tmp.atk = map->objects[i].atk;
        tmp.def = map->objects[i].def;
        tmp.magic = map->objects[i].magic;
        tmp.health = map->objects[i].health;
        tmp.name = map->objects[i].name;
        int type = tmp.type;
        if ((type >= OBJ_HEALTH_POINT) && (type <= OBJ_MAGIC_WAND) && (tmp.x == me->myX) && (tmp.y == me->myY))
        {
           obj.x = tmp.x;
           obj.y = tmp.y;
           obj.type = tmp.type;
           obj.name = tmp.name;
           obj.atk = tmp.atk;
           obj.def = tmp.def;
           obj.magic = tmp.magic;
           obj.health = tmp.health;
           result = i;   
        }
    }

    if (result >= 0)
    {
        
        map->delObject(result);
        client->sendToServer("REMOBJ:");
        client->receive();
        client->sendToServer(intToStr(result));
        client->receive();
    } 
    addToInventory(obj);
}

void Inventory::updateMyInfoInInventory()
{
   inventoryDataLine(intToStr(me->myAttack + me->myDefence + me->myMagic), 1);
   inventoryDataLine(intToStr(me->myAttack), 2);
   inventoryDataLine(intToStr(me->myDefence), 3);
   inventoryDataLine(intToStr(me->myMagic), 4);
}

void Inventory::inventoryDataLine(string data, int line)
{
     _Setbk bgColor;
     _setcursortype(_NOCURSOR);
     bgColor.color = BLUE;
     cout << bgColor;
     cout << setclr(WHITE);
     cout << setxy(75, line + 1);     
     cout << data;
     
}

void Inventory::inventoryCaptionLine(string data, int line)
{
     _Setbk bgColor;
     _setcursortype(_NOCURSOR);
     bgColor.color = BLUE;
     cout << bgColor;
     cout << setclr(WHITE);
     cout << setxy(62, line + 1);     
     cout << data;
}

void Inventory::removeInventoryItem(int itemNr)
{
     /*     int objType = inventory[itemNr - 1];

        switch (objType)
        {
           case OBJ_SWORD:
//                myAttack--;
                inventoryDataLine(intToStr(me->myAttack), 2);
                inventoryDataLine(intToStr(me->myAttack + me->myDefence + me->myMagic), 1);

                break;
           case OBJ_SHIELD:
//                myDefence--;
                inventoryDataLine(intToStr(me->myAttack), 3);
                inventoryDataLine(intToStr(me->myAttack + me->myDefence + me->myMagic), 1);

                break;
           case OBJ_MAGIC_WAND:
//                myMagic--;
                inventoryDataLine(intToStr(me->myMagic), 4);
                inventoryDataLine(intToStr(me->myAttack + me->myDefence + me->myMagic), 1);

                break;
           case OBJ_HEALTH_POINT:
//                myHealth--;
                inventoryDataLine(intToStr(me->myHealth), 5);
                inventoryDataLine(intToStr(me->myAttack + me->myDefence + me->myMagic), 1);

                break;
           case OBJ_MONSTER:
                break;
           case OBJ_NPC:
                break;
           default:
                break;
           
        }
*/
     
     inventoryItemsCount--;

     for (int i = itemNr - 1; i < inventoryItemsCount; i++)
     {
         inventory[i - 1].x = inventory[i + 1].x;
         inventory[i].y = inventory[i + 1].y;
         inventory[i].type = inventory[i + 1].type;
         inventory[i].atk = inventory[i + 1].atk;
         inventory[i].def = inventory[i + 1].def;
         inventory[i].magic = inventory[i + 1].magic;
         inventory[i].health = inventory[i + 1].health;
         inventory[i].name = inventory[i + 1].name;
     }

     paintInventory();
}



void Inventory::paintInventory()
{
     for (int i = 0; i < 7; i++)
     {
         if (i < inventoryItemsCount)
         {
               string caption = "";
               objectDef objType = inventory[i];
               caption = objType.name;
               inventoryCaptionLine(intToStr(i + 1) + ". " + caption, i + 9);               
         } 
         else 
         {
              inventoryCaptionLine("                  ", i + 9);
         }
     }
}


void Inventory::addToInventory(objectDef objType)
{
     if (inventoryItemsCount >= 10)
     {
        sys->outputToSystemMessages("NO MORE SLOTS IN INVENTORY LEFT");        
     } else 
     {
        inventoryItemsCount++;
        inventory[inventoryItemsCount - 1] = objType;
        string caption = "";
        /*
        switch (objType)
        {
           case OBJ_SWORD:
  //              myAttack++;
                inventoryDataLine(intToStr(me->myAttack), 2);
                inventoryDataLine(intToStr(me->myAttack + me->myDefence + me->myMagic), 1);
                caption = "Sword";
                break;
           case OBJ_SHIELD:
//                myDefence++;
                inventoryDataLine(intToStr(me->myAttack), 3);
                inventoryDataLine(intToStr(me->myAttack + me->myDefence + me->myMagic), 1);
                caption = "Shield";                
                break;
           case OBJ_MAGIC_WAND:
//                myMagic++;
                inventoryDataLine(intToStr(me->myMagic), 4);
                inventoryDataLine(intToStr(me->myAttack + me->myDefence + me->myMagic), 1);
                caption = "Magic Wand";
                break;
           case OBJ_HEALTH_POINT:
//                myHealth++;
                inventoryDataLine(intToStr(me->myHealth), 5);
                inventoryDataLine(intToStr(me->myAttack + me->myDefence + me->myMagic), 1);
                caption = "Health Point";
                break;
           case OBJ_MONSTER:
                break;
           case OBJ_NPC:
                break;
           default:
                break;
           
        }
        */
        caption = objType.name;
        if (caption.length() > 15)
        {
           caption = caption.substr(0, 15);
        }
        inventoryCaptionLine(intToStr(inventoryItemsCount) + ". " + caption, inventoryItemsCount + 8);
        
     }
}
