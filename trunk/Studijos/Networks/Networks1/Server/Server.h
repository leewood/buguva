
#ifndef NETWORK_SERVER
#define NETWORK_SERVER

#define _WIN32_WINNT 0x0501
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>

#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "27018"

#include <string>
#include <iostream>
#include "ServerInitError.cpp" 


using namespace std;

struct _client
{
       bool con;
       sockaddr_in addr;
       SOCKET cs;
       fd_set set;
       int i;
};

#define SOMAXCON 10

class NetworkServer
{
      public:
        bool showInfo;  
        bool updateID;   
        string port;
        int isError;
        bool listening;
        void closeServer();
        ~NetworkServer();
        int initSocket(string stPort);
        int startListen();
        int acceptConnection(int clientID);
        string receive(int clientID);
        int sendToClient(int clientID, string toSend);
        void disconnect(int clientID);
        _client clients[SOMAXCON];
        //ClientInfo clientInfo[SOMAXCON];
      private:
        WSADATA wsaData;
        SOCKET ListenSocket;
        SOCKET ClientSocket;
        struct addrinfo *result;
        struct addrinfo hints;
        char recvbuf[DEFAULT_BUFLEN];
        int iResult;
        int iSendResult;
        int recvbuflen;                 
        
        
};

#endif
