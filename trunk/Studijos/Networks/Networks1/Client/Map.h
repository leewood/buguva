#include "tools.h"

#ifndef MAP_H
#define MAP_H

#define MAX_OBJ_COUNT 800
#define MAP_MAX_X 40
#define MAP_MAX_Y 18


class Map
{
      
      

      string topMap;
      string leftMap;
      string rightMap;
      string bottomMap;
      void* me;
      public:
        Map(void *me)
        {
           this->me = me;
        }     
        int objectsCount;
        objectDef objects[MAX_OBJ_COUNT];
        int map[MAP_MAX_X][MAP_MAX_Y];
        int mapBackground(int x, int y);
        void paintMap(int x, int y);
        void paintObject(int x, int y, int objectType);
        void paintObjects();
        void paintHighObject(int x, int y);
        void moveObject(int oldx, int oldy, int newx, int newy, int objectType);
        void paintFullMap();
        int objectsInXYCount(int x, int y);
        int objectsInXY(int x, int y, int objectNr);
        void addObject(int x, int y, int type, int index, int atk, int def, int magic, int health, string name);
        void delObject(int id);
        void loadInicialData(string data);
        void analizeUpdate(string data);
        
        int mapType(int x, int y)
        {
            if ((x < MAP_MAX_X) && (y < MAP_MAX_Y) && (x >= 0) && (y >= 0))
            {
              return map[x][y];
            } 
            else 
            {
               return 0;
            }
        };
};

#endif
