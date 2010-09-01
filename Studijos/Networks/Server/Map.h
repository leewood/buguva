#ifndef MAP_H
#define MAP_H

#define OBJ_USER 1
#define OBJ_MONSTER 2
#define OBJ_HEALTH_POINT 3
#define OBJ_SWORD 4
#define OBJ_SHIELD 5
#define OBJ_MAGIC_WAND 6
#define OBJ_NPC 7
#define OBJ_ME 8

#include <string>
#include <iostream>
#include <fstream>
#include "tools.h"

using namespace std;

struct objectDef
{
   int x, y, type, ci;
   int atk, def, magic, health;
   string name;
};

struct enemy
{
   int attack, defence, magic, health;
   string name;
};

struct mapDef
{
    objectDef objects[20];
    int objectsCount;
    void removeObject(int i);
    enemy calculateMonster(int x, int y);
    enemy calculateObjects(int x, int y, int ignore);
    
};

class Map
{
    string mapNames[10];
    int mapsCount;
    
    public:
      mapDef maps[10];
      void init();
      int findMap(string fileName);
      void addMap(string fileName); 
      string loadMap(string fileName);   
      string loadMap(string fileName, int id);       
};



#endif
