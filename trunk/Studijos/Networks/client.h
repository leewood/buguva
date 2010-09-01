#include <errno.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#include <sys/types.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <netinet/in.h>

#define MY_PORT 5555 

#define LESS    1
#define MORE    2
#define EQUAL   3

#if 0
int my_send(int fd, void *buf, size_t len);
int my_recv(int fd, void *buf, size_t len);
#endif

