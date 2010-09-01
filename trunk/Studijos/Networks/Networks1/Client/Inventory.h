#include "tools.h"

#ifndef INVENTORY_H
#define INVENTORY_H
#define INVENTORY_SIZE 10

class Inventory
{
      objectDef inventory[INVENTORY_SIZE];
      int inventoryItemsCount;
      Screen screen;
      Me* me;
      SystemWindow* sys;
      NetworkClient* client;
      Map* map;
      
      public:
      int inventoryUseKeys[10];
      int useEvent(int key);
      void inventoryDataLine(string data, int line);
      void addToInventory(objectDef objType);
      void useInventoryItem(int itemNr);
      void dropItem(int itemNr);
      void takeItem();
      void paintInventory();
      void removeInventoryItem(int itemNr);
      void inventoryCaptionLine(string data, int line);
      void initInventory(Me* me, SystemWindow* sys, NetworkClient* client, Map* map);
      void updateMyInfoInInventory();
};

#endif
