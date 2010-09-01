#include "Map.h"

int Map::mapBackground(int x, int y)
{
   int tBackground = BLACK;
   int mapType = map[x][y];
   switch (mapType)
   {
       case MAP_WALL:
            break;
       case MAP_WATER:
            tBackground = BLUE;
            break;
       case MAP_SAND:
            tBackground = YELLOW;
            break;
       case MAP_DARK_MATTER:
            tBackground = BLACK;
            break;
       default:
          break;
   }
   return tBackground; 
}

void Map::paintMap(int x, int y)
{
   int tColor = WHITE;
   int tBackground = mapBackground(x, y);
   char symbol = ' ';

   int mapType = map[x][y];
   switch (mapType)
   {
       case MAP_WALL:
            symbol = '*';
            break;
       case MAP_WATER:
            symbol = ' ';
            break;
       case MAP_SAND:
            symbol = ' ';
            break;
       case MAP_DARK_MATTER:
            symbol = '!';
            break;
       default:
          break;
   }
   textcolor(tColor);
   textbackground(tBackground);
   putchxy(x + 10, y + 1, symbol);     
   
}

void Map::paintObject(int x, int y, int objectType)
{
   int tColor = WHITE;
   int tBackground = mapBackground(x, y);
   char symbol = ' ';
   switch (objectType)
   {
      case OBJ_USER:
           symbol = '#';
           break;
      case OBJ_MONSTER:
           symbol = '$';
           tColor = RED;
           break;
      case OBJ_HEALTH_POINT:
           symbol = 'H';
           tBackground = RED;
           break;
      case OBJ_SWORD:
           symbol = '^';
           break;
      case OBJ_SHIELD:
           symbol = 'O';
           break;
      case OBJ_MAGIC_WAND:
           symbol = '\\';
           break;
      case OBJ_NPC:
           symbol = '#';
           tColor = BLUE;
           break;
      case OBJ_ME:
           symbol = '#';
           tColor = RED;
           break;

      default:
          symbol = ' ';
          break;
   }
   textcolor(tColor);
   textbackground(tBackground);
   putchxy(x + 10, y + 1, symbol);
}

void Map::paintObjects()
{
     for (int i = 0; i < objectsCount; i++)
     {
         paintObject(objects[i].x, objects[i].y, objects[i].type);
     }
}

void Map::paintHighObject(int x, int y)
{
     for (int i = objectsCount - 1; i >= 0; i--)
     {
         if ((objects[i].x == x) && (objects[i].y == y))
         {
             paintObject(x, y, objects[i].type);
             break;
         }
     }     
}

void Map::moveObject(int oldx, int oldy, int newx, int newy, int objectType)
{
   paintMap(oldx, oldy);
   paintHighObject(oldx, oldy);
   paintObject(newx, newy, objectType);
}


void Map::paintFullMap()
{
     for (int y = 0; y < 17; y++)
       for (int x = 0; x < 40; x++)
       {
           paintMap(x, y);
       }
}

int Map::objectsInXYCount(int x, int y)
{
    int count = 0;
    for (int i = 0; i < objectsCount; i++)
    {
        if ((objects[i].x == x) && (objects[i].y == y))
        {
           count++;
        }
    }
    return count;
}

int Map::objectsInXY(int x, int y, int objectNr)
{
    int found = 0;
    int count = 0;
    for (int i = 0; i < objectsCount; i++)
    {
        if ((objects[i].x == x) && (objects[i].y == y))
        {
           count++;
           if (count == objectNr)
           {
              return objects[i].type;
           }
        }
    }
    return 0;
}

void Map::addObject(int x, int y, int type, int index, int atk, int def, int magic, int health, string name)
{
     objectDef obj;
     obj.x = x;
     obj.y = y;
     obj.type = type;
     obj.atk = atk;
     obj.def = def;
     obj.magic = magic;
     obj.health = health;
     obj.name = name;
     if (objectsCount - 1 < index)
     {
         objectsCount = index + 1;
     }
     objects[index] = obj;
     paintObject(x, y, type);
}

void Map::delObject(int id)
{
     #ifdef DEBUG
     cout << "Map->DelObjIn";     
     #endif     
     int x = objects[id].x;
     int y = objects[id].y;
     paintMap(x, y);
     for (int i = id; i < objectsCount - 1; i++)
     {
        objects[id].type = objects[id + 1].type;
        objects[id].x = objects[id + 1].x;        
        objects[id].y = objects[id + 1].y;
        objects[id].atk = objects[id + 1].atk;
        objects[id].def = objects[id + 1].def;
        objects[id].magic = objects[id + 1].magic;
        objects[id].health = objects[id + 1].health;  
        objects[id].name = objects[id + 1].name;                                      
     }
//     objects[id] = objects[objectsCount - 1];
     objectsCount--;
     #ifdef DEBUG
     cout << "Map->DelObjIn";     
     #endif
     if (((Me*)me)->myID >= id)
     {
        ((Me*)me)->myID--;
     }
     paintHighObject(x, y);
     #ifdef DEBUG     
     cout << "Map->DelObjOut";     
     #endif     
}

void Map::loadInicialData(string data)
{
     int place = 0;
     for (int y = 0; y < 18; y++)
       for (int x = 0; x < 40; x++)
       {
           int val = getByteFromStr(data, place);
           place += 2;
           map[x][y] = val;            
       }
     int count = getByteFromStr(data, place);
     place += 2;
     objectsCount = count;
     for (int i = 0; i < count; i++)
     {
           int x = getByteFromStr(data, place);
           place += 2;
           int y = getByteFromStr(data, place);
           place += 2;
           int type = getByteFromStr(data, place);
           place += 2;
           int atk = getByteFromStr(data, place);
           place += 2;
           int def = getByteFromStr(data, place);
           place += 2;
           int magic = getByteFromStr(data, place);
           place += 2;
           int health = getByteFromStr(data, place);
           place += 2;
           string name = data.substr(place, 20);
           place += 20;           
           objectDef obj;
           obj.x = x;
           obj.y = y;
           obj.type = type;
           obj.atk = atk;
           obj.def = def;
           obj.magic = magic;
           obj.health = health;
           obj.name = name;
           objects[i] = obj;
     } 
     paintFullMap();
     paintObjects(); 
     #ifdef DEBUG
       cout << "LoadInicialDataComplete";
     #endif
}

void Map::analizeUpdate(string data)
{
     int x, y, x2, y2, type, id, atk, def, magic, health;
     string name;
     if (data != "N")
     {
       int pos = 0;
       while (pos < data.length())
       {       
       int opt = getByteFromStr(data, pos);
       
       switch (opt)
       {
          case UPD_MOVE:
             x = getByteFromStr(data, pos + 2);
             y = getByteFromStr(data, pos + 4);
             x2 = getByteFromStr(data, pos + 6);
             y2 = getByteFromStr(data, pos + 8);
             id = getByteFromStr(data, pos + 10);
             pos += 12;
             objects[id].x = x2;
             objects[id].y = y2;
             moveObject(x, y, x2, y2, objects[id].type);
             break;
          case UPD_ADD:
             x = getByteFromStr(data, pos + 2);
             y = getByteFromStr(data, pos + 4);
             type = getByteFromStr(data, pos + 6);
             id = getByteFromStr(data, pos + 8);
             atk = getByteFromStr(data, pos + 10);
             def = getByteFromStr(data, pos + 12);
             magic = getByteFromStr(data, pos + 14);
             health = getByteFromStr(data, pos + 16);
             name = data.substr(pos + 18, 20);
             pos += 38;
             addObject(x, y, type, id, atk, def, magic, health, name);
             break;
          case UPD_DEL:
             id = getByteFromStr(data, pos + 2);
             delObject(id);
             pos += 4;
             break;
          case 3:
             id = getByteFromStr(data, pos + 2);
             x =  getByteFromStr(data, pos + 4);
             y = getByteFromStr(data, pos + 6);
             type = getByteFromStr(data, pos + 8);
             atk = getByteFromStr(data, pos + 10);
             def = getByteFromStr(data, pos + 12);
             magic = getByteFromStr(data, pos + 14);
             health = getByteFromStr(data, pos + 16);
             int oldx;
             oldx = (objects[id]).x;
             int oldy; 
             oldy = (objects[id]).y;
             objects[id].atk = atk;
             objects[id].def = def;
             objects[id].magic = magic;
             objects[id].health = health;
             objects[id].x = x;
             objects[id].y = y;
             objects[id].type = type;
             moveObject(oldx, oldy, x, y, type);
             pos += 18;
          default: 
             break;          
       }
       
       }
     }
     

}

/*

void updateVideo()
{
     client.sendToServer("UPDATE");
     string data = client.receive();
     if (data == "INVENTORY")
     {
         client.sendToServer("WAITING");
         stringstream ss;
         string input = client.receive();
         ss << input;
         int count = 0;
         ss >> count;
         inventoryItemsCount = count;
         for (int i = 0; i < count; i++)
         {
             ss >> inventory[i];
         }
         paintInventory();
     } 
     else if (data == "OBJECTS")
     {
         client.sendToServer("WAITING");
         stringstream ss;
         string input = client.receive();
         ss << input;
         int count = 0;
         ss >> count;
         objectsCount = count;
         for (int i = 0; i < count; i++)
         {
             int x, y, type;
             ss >> x, y, type;
             objectDef obj;
             obj.x = x;
             obj.y = y;
             obj.type = type;
             objects[i] = obj;
         }
         paintObjects(); 
     }
     else if (data == "MAP")
     {
         client.sendToServer("WAITING");
         stringstream ss;
         string input = client.receive();
         ss << input;
         int count = 0;
         ss >> count;
         objectsCount = count;
         for (int i = 0; i < count; i++)
         {
             int x, y, type;
             ss >> x, y, type;
             objectDef obj;
             obj.x = x;
             obj.y = y;
             obj.type = type;
             objects[i] = obj;
         }
         paintObjects(); 

     }
}

*/




