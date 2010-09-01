#include "Me.h"

void Me::makeAMove(int newx, int newy)
{
     string data = "";
     if ((newx >= 0) && (newx < 40) && (newy < 17) && (newy >= 0))
     {
       int mapType = map->mapType(newx, newy);
       bool canMove = (mapType == 0) || ((mapType == MAP_SAND) && (myDefence > 10)) || ((mapType == MAP_WATER) && (myMagic > 10));
       if (canMove)
       {
            client->sendToServer("MOVE:");
            data = client->receive();
            client->sendToServer(intToStr(newx));
            data = client->receive();
            client->sendToServer(intToStr(newy));    
            data = client->receive();
            //sys.outputToSystemMessages(">" + data);   
            map->objects[myID].x = newx;
            map->objects[myID].y = newy;                    
            map->moveObject(myX, myY, newx, newy, OBJ_ME);
            myX = newx;
            myY = newy;                        
            //interpretObjects(myX, myY, MODE_MOVE);

       }
     } 
     else 
     {
        if (newx < 0)
        {
            newx = 39;
            client->sendToServer("MAPSEND:");
            client->receive();
//            client->sendToServer(leftMap);
            data = client->receive();
            //loadInicialData(data);
            client->sendToServer("ADD:");
            client->receive();
            client->sendToServer(intToStr(39) + " " + intToStr(newy));
            client->receive();
            //interpretObjects(myX, myY, MODE_MOVE);
        } 
        else  if (newx >= 40)
        {
            newx = 0;
            client->sendToServer("MAPSEND:");
            client->receive();
//            client->sendToServer(rightMap);
            data = client->receive();
            //loadInicialData(data);
            client->sendToServer("ADD:");
            client->receive();
            client->sendToServer(intToStr(newx) + " " + intToStr(newy));
            client->receive();
              
        }
        else  if (newy >= 17)
        {
            newy = 0;
            client->sendToServer("MAPSEND:");
            client->receive();
//            client->sendToServer(bottomMap);
            data = client->receive();
            //loadInicialData(data);
            client->sendToServer("ADD:");
            client->receive();
            client->sendToServer(intToStr(newx) + " " + intToStr(newy));
            client->receive();
              
        } 
        else
        {
            newy = 16;
            client->sendToServer("MAPSEND:");
            client->receive();
//            client->sendToServer(topMap);
            data = client->receive();
            //loadInicialData(data);
            client->sendToServer("ADD:");
            client->receive();
            client->sendToServer(intToStr(newx) + " " + intToStr(newy));
            client->receive();
              
        }
        
        
        
     }           
}
