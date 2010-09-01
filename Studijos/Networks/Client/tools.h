
#ifndef TOOLS_H
#define TOOLS_H

#include <stdlib.h>
#include <stdio.h>
#include <iostream>
#include <string>
#include <sstream>
#include "Client.h"
#include "Screen.h"
#include "SystemWin.h"
#include "Models.h"
#include "Map.h"
#include "Me.h"
#include "Inventory.h"
#include "Login.h"
#include "Fight.h"
#include <constream>
#include <conio2.h>

#define MAP_WALL 1
#define MAP_WATER 2
#define MAP_SAND 3
#define MAP_DARK_MATTER 4


#define OBJ_USER 1
#define OBJ_MONSTER 2
#define OBJ_HEALTH_POINT 3
#define OBJ_SWORD 4
#define OBJ_SHIELD 5
#define OBJ_MAGIC_WAND 6
#define OBJ_NPC 7
#define OBJ_ME 8

#define MODE_MOVE 1
#define MODE_ACTIVATE 2
#define MODE_TALK 3

#define UPD_MOVE 0
#define UPD_ADD 1
#define UPD_DEL 2


using namespace conio;
using namespace std;


string intToStr(int data);
int charToByte(char c);
char chr(int data);
int getByteFromStr(string data, int place);

#endif
