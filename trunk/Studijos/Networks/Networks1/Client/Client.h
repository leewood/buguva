#ifndef NETWORK_CLIENT
#define NETWORK_CLIENT

#define _WIN32_WINNT 0x0501
#include <winsock2.h>
#include <ws2tcpip.h>
#include <stdlib.h>
#include <stdio.h>

#define DEFAULT_BUFLEN 512
#define DEFAULT_PORT "27018"

#include <string>
#include <iostream>
#include "ClientInitError.cpp" 

using namespace std;


class NetworkClient
{
      public:
        string port;
        int isError;
        NetworkClient();
        ~NetworkClient();
        NetworkClient(string port);
        int initSocket(string server, string stPort);
        int connectToServer();
        string receive();
        void disconnect();
        int sendToServer(string toSend);
      private:
        WSADATA wsaData;
        SOCKET ConnectSocket;
        struct addrinfo *result;
        struct addrinfo *ptr;
        struct addrinfo hints;
        char *sendbuf;
        char recvbuf[DEFAULT_BUFLEN];
        int iResult;
        int recvbuflen;  
        bool connected;            
        
};

#endif
