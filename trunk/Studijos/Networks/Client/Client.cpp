#include "Client.h"

NetworkClient::NetworkClient()
{
   NetworkClient(DEFAULT_PORT);
}

NetworkClient::~NetworkClient()
{
   disconnect();
}

NetworkClient::NetworkClient(string stPort)
{
   port = stPort;   
   connected = false;                                                                                                      
}
            
            
int NetworkClient::initSocket(string server, string stPort)
{
    isError = 0;          
    port = stPort;
    result = NULL;
    ptr = NULL;
    recvbuflen = DEFAULT_BUFLEN;  
    // Initialize Winsock
    iResult = WSAStartup(MAKEWORD(2,2), &wsaData);
    if (iResult != 0) {
        printf("WSAStartup failed: %d\n", iResult);
        system("pause");        
        return 1;
    }

    ZeroMemory( &hints, sizeof(hints) );
    hints.ai_family = AF_UNSPEC;
    hints.ai_socktype = SOCK_STREAM;
    hints.ai_protocol = IPPROTO_TCP;

    // Resolve the server address and port
    iResult = getaddrinfo(server.c_str(), port.c_str(), &hints, &result);
    if ( iResult != 0 ) {
        printf("getaddrinfo failed: %d\n", iResult);
        WSACleanup();
        system("pause");        
        return 1;
    }

}            
                
int NetworkClient::connectToServer()
{
    // Attempt to connect to an address until one succeeds
    for(ptr=result; ptr != NULL ;ptr=ptr->ai_next) {

        // Create a SOCKET for connecting to server
        ConnectSocket = socket(ptr->ai_family, ptr->ai_socktype, 
            ptr->ai_protocol);
        if (ConnectSocket == INVALID_SOCKET) {
            printf("Error at socket(): %ld\n", WSAGetLastError());
            freeaddrinfo(result);
            WSACleanup();
            system("pause");            
            return 1;
        }

        // Connect to server.
        iResult = connect( ConnectSocket, ptr->ai_addr, (int)ptr->ai_addrlen);
        if (iResult == SOCKET_ERROR) {
            closesocket(ConnectSocket);
            ConnectSocket = INVALID_SOCKET;
            continue;
        }
        break;
    }

    freeaddrinfo(result);

    if (ConnectSocket == INVALID_SOCKET) {
        printf("Unable to connect to server!\n");
        WSACleanup();
        system("pause");        
        return 1;
    }
    connected = true;
    return 0;
}

void NetworkClient::disconnect()
{
    if (connected)
    {
      sendToServer("DISCON:");
      closesocket(ConnectSocket);
      WSACleanup();   
    }
}

string NetworkClient::receive()
{
             
        // Receive until the peer shuts down the connection
        
    string result = "";
    bool toContinue = true;
    do {
        int total = 0;
        while (total < recvbuflen)
        {
           iResult = recv(ConnectSocket, recvbuf + total, recvbuflen - total, 0);       
           total += iResult;
        }
        if (iResult > 0) {
            //printf("Bytes received: %d\n", iResult);
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
            //printf("Connection closing...\n");
            toContinue = false;
            isError = 1;
        }
        else  
        {
            //printf("recv failed: %d\n", WSAGetLastError());
            toContinue = false;
            isError = 1;
        }
     } while (toContinue);
     
  return result;
}

int NetworkClient::sendToServer(string toSend)
{
    
        // Send an initial buffer
        int pos = 0;
        int length = toSend.length();
        //cout << "Sending: " << toSend << "\n";
        sendbuf = new char[DEFAULT_BUFLEN];
        int newLen = DEFAULT_BUFLEN;
        while (pos < length)
        {
            for (int i = 0; (i < DEFAULT_BUFLEN); i++)
            {
                if (pos < length)
                {
                   sendbuf[i] = toSend[pos];
                   pos++;
                } else {
                   sendbuf[i] = '\0';
                   pos++;
                }
            }
            int total = 0;
            while (total < newLen)
            {
               iResult = send(ConnectSocket, sendbuf + total, newLen - total, 0 );
               if (iResult == SOCKET_ERROR) 
               {
                  return 1;
               }               
               total += iResult;
            }
        }
    

    // shutdown the connection since no more data will be sent
  //  iResult = shutdown(ConnectSocket, SD_SEND);
  //  if (iResult == SOCKET_ERROR) {
  //      printf("shutdown failed: %d\n", WSAGetLastError());
  //      closesocket(ConnectSocket);
  //      WSACleanup();
  //      system("pause");        
  //      return 1;
  //  }
    delete(sendbuf);
        return 0;
}


