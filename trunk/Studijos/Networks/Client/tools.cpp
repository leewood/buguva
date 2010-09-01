

#include "tools.h"

string intToStr(int data)
{
       stringstream ss;
       ss << data;
       string s;
       ss >> s;
       return s;
}

char chr(int data)
{
     return (char)data;
}


int charToByte(char c)
{
    switch (c)
    {
       case '0': return 0;
       case '1': return 1;
       case '2': return 2;
       case '3': return 3;
       case '4': return 4;
       case '5': return 5;
       case '6': return 6;
       case '7': return 7;
       case '8': return 8;
       case '9': return 9;
       case 'A': return 10;
       case 'B': return 11;
       case 'C': return 12;
       case 'D': return 13;
       case 'E': return 14;
       case 'F': return 15;
       default: return 0;
    }
    return 0;
}

int getByteFromStr(string data, int place)
{
    char high, low;
    high = data[place];
    low = data[place + 1];
    return charToByte(high) * 16 + charToByte(low);
}
