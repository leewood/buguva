#include "Server.h"





            
            
int NetworkServer::initSocket(string stPort)
{
           isError = 0;          
           port = stPort;
           ListenSocket = INVALID_SOCKET;
           ClientSocket = INVALID_SOCKET;
           result = NULL; 
           recvbuflen = DEFAULT_BUFLEN;  
           listening = false;             
           for (int j = 0; j < SOMAXCON; j++)
           {
               clients[j].con = false;
               clients[j].cs = INVALID_SOCKET;               
           }
    // Initialize Winsock
           iResult = WSAStartup(MAKEWORD(2,2), &wsaData);

             if (iResult != 0) 
             {
                throw ServerInitError(iResult);
              }
    
             ZeroMemory(&hints, sizeof(hints));
             hints.ai_family = AF_INET;
             hints.ai_socktype = SOCK_STREAM;
             hints.ai_protocol = IPPROTO_TCP;
             hints.ai_flags = AI_PASSIVE;

             // Resolve the NetworkServer address and port
             iResult = getaddrinfo(NULL, port.c_str(), &hints, &result);
             
             if ( iResult != 0 ) 
             {
               WSACleanup();
               throw ServerInitError(iResult);
             }

             // Create a SOCKET for connecting to server
             ListenSocket = socket(result->ai_family, result->ai_socktype, result->ai_protocol);
             if (ListenSocket == INVALID_SOCKET) 
             {
                int error = WSAGetLastError();
                freeaddrinfo(result);
                WSACleanup();
                throw ServerInitError(error);
             }
             
             int i = 1;
             setsockopt(ListenSocket, SOL_SOCKET, SO_REUSEADDR, (char *)&i, sizeof(i));
             // Setup the TCP listening socket
             iResult = bind( ListenSocket, result->ai_addr, (int)result->ai_addrlen);
             if (iResult == SOCKET_ERROR) 
             {
                int error = WSAGetLastError();
                freeaddrinfo(result);
                closesocket(ListenSocket);
                WSACleanup();
                throw ServerInitError(error);
             }

             freeaddrinfo(result);      
                          
}            
                
int NetworkServer::startListen()
{

      iResult = listen(ListenSocket, SOMAXCON);
      if (iResult == SOCKET_ERROR) 
      {
         int error = WSAGetLastError();
         closesocket(ListenSocket);
         WSACleanup();
         throw ServerInitError(error);
         return 1;
      }
      unsigned long b = 1;
      ioctlsocket(ListenSocket, FIONBIO, &b);

    return 0;
}

int NetworkServer::acceptConnection(int clientID)
{
    // Accept a client socket
    _client x = clients[clientID];
    //ClientSocket 
    x.i = 670;
    x.cs = accept(ListenSocket, (sockaddr *)&x.addr, &x.i);
    
    if ((x.cs == INVALID_SOCKET) || (x.cs == SOCKET_ERROR) || (x.cs == 0)){
        x.con = false;      
        if (x.cs == SOCKET_ERROR)
        {
            int error = WSAGetLastError();
            if (error != WSAEWOULDBLOCK)
            {
               throw ServerInitError(error);
            }
        }
        
        return 1;
    } 
    else 
    {
       sockaddr_in addr = (sockaddr_in)x.addr;   
       x.con = true;
       cout << "Client " << clientID << " accepted (IP:" << inet_ntoa(addr.sin_addr) << ")\n";
       FD_ZERO(&x.set);
       FD_SET(x.cs, &x.set);
    }
    clients[clientID] = x;
    return 0;
}

string NetworkServer::receive(int clientID)
{
        // Receive until the peer shuts down the connection
        
    string result = "";
    bool toContinue = true;
    _client x = clients[clientID];
    if (FD_ISSET(x.cs, &x.set))
    {
    do {
        int total = 0;
        while (total < recvbuflen) 
        {
           iResult = recv(x.cs, recvbuf + total, recvbuflen - total, 0);        
           if (iResult == SOCKET_ERROR)
           {
               int error = WSAGetLastError();
               if (error != WSAEWOULDBLOCK)
               {
                 throw ServerInitError(error);
               } 
               else 
               {
                  return "";
               }
           }
           total += iResult;
        }
        if (iResult > 0) {
            for (int i = 0; i < recvbuflen; i++)
            {
                if ((recvbuf[i] != '\0') && (toContinue))
                {
                   result += recvbuf[i];
                } else {
                   toContinue = false;
                }
            }
            
        }         
        else if (iResult == 0)
        {
            printf("Connection closing...\n");
            disconnect(clientID);
            toContinue = false;
            isError = 1;
        }
        else  
        {
            //printf("recv failed: %d\n", WSAGetLastError());
            //closesocket(x.cs);
            //WSACleanup();
            toContinue = false;
            isError = 1;
        }
     } while (toContinue);
     }
  clients[clientID] = x;
  return result;
}

void NetworkServer::disconnect(int clientID)
{
     _client x = clients[clientID];
     x.i = -1;
     clients[clientID] = x;     
     if (x.cs) 
     {
         x.con = false;      
         clients[clientID] = x;
         if (closesocket(x.cs) == SOCKET_ERROR)
         {
             int error = WSAGetLastError();
             throw ServerInitError(error);
         }
     }
     x.con = false;
     clients[clientID] = x;

}

int NetworkServer::sendToClient(int clientID, string source)
{
        string toSend(source);
        int pos = 0;
        int length = toSend.length();
        if (showInfo)
        {
          cout << "Server->" << clientID << ": " << toSend << "\n";
        }       
        int newLen = DEFAULT_BUFLEN;
        _client x = clients[clientID];
        while (pos < length)
        {
            for (int i = 0; i < DEFAULT_BUFLEN; i++)
            {
                if (pos < length)
                {
                   recvbuf[i] = toSend[pos];
                   pos++;
                } else {
                   recvbuf[i] = '\0';                   
                   pos++;
                }
            }
            
            int total = 0;
            while (total < newLen)
            {
               iSendResult = send( x.cs, recvbuf + total, newLen - total, 0 );
               total += iSendResult;
               if ((iSendResult == SOCKET_ERROR) || (iSendResult == 0)) 
               {
                  int error = WSAGetLastError();                  
                  disconnect(clientID);
                  WSACleanup();
                  x.con = false;
                  clients[clientID] = x;    
                  throw ServerInitError(error);            
                  return 1;
               }               
            }
        }
        clients[clientID] = x;
        return 0;
}

void NetworkServer::closeServer()
{
    closesocket(ListenSocket);
}

NetworkServer::~NetworkServer()
{
   closeServer();
}




