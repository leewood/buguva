#include "tools.h"

#ifndef FIGHT_H
#define FIGHT_H

#define MAX_FIGHTERS_COUNT 20

class Fight
{
     objectDef fighters[MAX_FIGHTERS_COUNT];
     int fightersCount;
     Screen screen;
     Me* me;
     NetworkClient* client;
     SystemWindow* sys;
     Inventory* inventory;
   public:
     void closeFightMode();
     int openFightMode(int fightMode);
     void updateMyInfoInFightWindow();
     void delFigtherFromFightWindow(int fighterID);
     void paintFightWindow();
     void initFightMode(NetworkClient* client, SystemWindow* sys, Me* me, Inventory* inventory);
     void updateFightWindowInfo(int fighterID, int atk, int def, int magic, int health);
     void clearFighters();
     void addFighter(objectDef fighter);
};

#endif
