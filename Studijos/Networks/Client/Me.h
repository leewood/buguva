#include "tools.h"

#ifndef ME_H
#define ME_H

class Me
{
   Map* map;
   NetworkClient* client;
      
   public:   
   int myAttack;
   int myDefence;
   int myMagic;
   int myHealth;
   int myX;
   int myY;
   int myID;
   bool killed;
   objectDef gun, shield, wand;
   bool hasGun;
   bool hasShield;
   bool hasMagicWand;
   void makeAMove(int newx, int newy);
   void initMe(Map* map, NetworkClient* client)
   {        
      hasGun = false;
      hasShield = false;
      hasMagicWand = false;  
      myAttack = 0;
      myDefence = 0;
      myMagic = 0;
      myHealth = 50;        
      killed = false;
      myID = 0;
      myX = 10;
      myY = 10;
      this->map = map;
      this->client = client;
   };
      
};


#endif
