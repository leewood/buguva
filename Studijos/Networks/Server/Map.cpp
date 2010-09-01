#include "Map.h"

int Map::findMap(string fileName)
{
    for (int i = 0; i < mapsCount; i++)
    {
        if (mapNames[i] == fileName)
        {
            return i;
        }
    }
    return -1;
}

void mapDef::removeObject(int i)
{
     if (i < objectsCount)
     {
           for (int j = i; j < objectsCount - 1; j++)
           {
              //objects[j] = objects[j + 1];
              objects[j].type = objects[j + 1].type;
              objects[j].x = objects[j + 1].x;
              objects[j].y = objects[j + 1].y;
              objects[j].atk = objects[j + 1].atk;
              objects[j].def = objects[j + 1].def;
              objects[j].magic = objects[j + 1].magic;
              objects[j].health = objects[j + 1].health;
              objects[j].ci = objects[j + 1].ci;
              objects[j].name = objects[j + 1].name;                                                                                                                
           }
           //objects[i] = objects[objectsCount - 1];
           objectsCount--;
           
     }
}

enemy mapDef::calculateMonster(int x, int y)
{
   return calculateObjects(x, y, -1);
}

enemy mapDef::calculateObjects(int x, int y, int ignore)
{
    enemy result;
    result.attack = 0;
    result.defence = 0;
    result.magic = 0;
    result.health = 0;
    for (int i = 0; i < objectsCount; i++)
    {
        if ((objects[i].type == OBJ_USER) && (objects[i].x == x) && (objects[i].y == y) && (i != ignore))
        {
            result.attack += objects[i].atk;
            result.defence += objects[i].def;
            result.magic += objects[i].magic;
            result.health += objects[i].health;
        }
    }
    return result;
}




void Map::addMap(string fileName)
{
    if (findMap(fileName) < 0)
    {
       mapsCount++;
       mapNames[mapsCount - 1] = fileName;
       maps[mapsCount - 1].objectsCount = 0;
    }
}

string Map::loadMap(string fileName)
{
    string newStr = fileName;
    return this->loadMap(newStr, -1);   
}


string Map::loadMap(string fileName, int id)
{

     ifstream fileStream;
     string result = "";
     fileStream.open(fileName.c_str());
     //map
     
     string otherMaps;
     fileStream >> otherMaps;
     fileStream >> otherMaps;
     fileStream >> otherMaps;
     fileStream >> otherMaps;     
     for (int y = 0; y < 18; y++)
       for (int x = 0; x < 40; x++)
       {
           int data;
           fileStream >> data;
           result += byteToStr(data);           
       }
     int count;
     //objects

     fileStream >> count;
     
     int mapPlace = this->findMap(fileName);
     bool updateMap = false;
     if (mapPlace < 0)
     {
         result += byteToStr(count);
         this->addMap(fileName);             
         mapPlace = this->findMap(fileName);
         updateMap = true;
         this->maps[mapPlace].objectsCount = count;
         for (int i = 0; i < count; i++)
         {
             int x, y, type, atk, def, magic, health;
             string name;
             fileStream >> x >> y >> type >> atk >> def >> magic >> health;
             fileStream >> name;

             if (i == id) 
             {
                type = OBJ_ME;
             }             
             result += byteToStr(x) + byteToStr(y) + byteToStr(type) + byteToStr(atk) + byteToStr(def) + byteToStr(magic) + byteToStr(health) + fixedSizeString(name, 20);
             
             if (updateMap)
             {
               maps[mapPlace].objects[i].x = x;
               maps[mapPlace].objects[i].y = y;
               maps[mapPlace].objects[i].type = type;
               maps[mapPlace].objects[i].atk = atk;
               maps[mapPlace].objects[i].def = def;
               maps[mapPlace].objects[i].magic = magic;
               maps[mapPlace].objects[i].health = health;
               maps[mapPlace].objects[i].name = name;
             }
         }     

     } else 
     {
         int count = this->maps[mapPlace].objectsCount;
         result += byteToStr(count);
         for (int i = 0; i < count; i++)
         {
             int x, y, type, atk, def, magic, health;
             string name;

             x = maps[mapPlace].objects[i].x;
             y = maps[mapPlace].objects[i].y;
             type = maps[mapPlace].objects[i].type;
             if (i == id) 
             {
                type = OBJ_ME;
             }                          
             atk = maps[mapPlace].objects[i].atk;
             def = maps[mapPlace].objects[i].def;
             magic = maps[mapPlace].objects[i].magic;
             health = maps[mapPlace].objects[i].health;
             name = maps[mapPlace].objects[i].name;
             result += byteToStr(x) + byteToStr(y) + byteToStr(type) + byteToStr(atk) + byteToStr(def) + byteToStr(magic) + byteToStr(health) + fixedSizeString(name, 20);
         }
         
     }
     fileStream.close();
     return result;     
}


/*
void Map::loadMap(string fileName)
{
     ifstream fileStream;

     fileStream.open(fileName.c_str());
     string otherMaps;
     fileStream >> otherMaps;
     fileStream >> otherMaps;
     fileStream >> otherMaps;
     fileStream >> otherMaps;
     for (int y = 0; y < 18; y++)
       for (int x = 0; x < 40; x++)
       {
           int data;
           fileStream >> data;
       }
     int count;
     //objects
     fileStream >> count;

     int mapPlace = findMap(fileName);
     for (int i = 0; i < count; i++)
     {
         int x, y, type, atk, def, magic, health;
             fileStream >> x >> y >> type >> atk >> def >> health;
             maps[mapPlace].objects[i].x = x;
             maps[mapPlace].objects[i].y = y;
             maps[mapPlace].objects[i].type = type;
             maps[mapPlace].objects[i].atk = atk;
             maps[mapPlace].objects[i].def = def;
             maps[mapPlace].objects[i].magic = magic;
             maps[mapPlace].objects[i].health = health;
             
     }     
     maps[mapPlace].objectsCount = count;
     fileStream.close();
}
*/

void Map::init()
{
   mapsCount = 0;
}
