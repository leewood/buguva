#include <stdlib.h>
#include "client.h"

#include <sys/select.h>
#include <sys/time.h>
#include <sys/types.h>
#include <unistd.h>



int generate_number()
{
    long int r1 = 0;
    long int r2 = 0;
    long long int r3 = 0;
    int gen = 0;


    do {
        r1 = random();
        gen = r1%100;
        fprintf(stderr, "r1 = %ld gen = %d\n", r1, gen);
    } while ( (gen < 1) && (gen > 100));

    return gen;
}

int main()
{
    int num = 0;
    int server_sd;
    int client_sd;
    struct sockaddr_in server_addr;
    struct sockaddr_in client_addr;
    int client_addr_len;
    int ret;
    int recv_size = 0;
    int send_size = 0;
    int buffer;
    int tag = LESS;
    int number = 0;
    int end_flag = 0;

/* for select */
    fd_set read_set;
    fd_set master_set;
    int maxfd;


    FD_ZERO(master_set);
    FD_ZERO(read_set);

    server_sd = socket(PF_INET, SOCK_STREAM, 0);
    if (server_sd == -1)
    {
        perror("socket failed:");
        return -1;
    }

    server_addr.sin_family = AF_INET;
    server_addr.sin_port = htons(MY_PORT);
    /*server_addr.sin_addr.s_addr = inet_addr("127.0.0.1"); */
    server_addr.sin_addr.s_addr = INADDR_ANY; 
    memset(server_addr.sin_zero, 0, sizeof(server_addr.sin_zero));

    ret = bind(server_sd, (const struct sockaddr *)&server_addr, sizeof(server_addr));
    if (ret != 0)
    {
        perror("bind failed:");
        close(server_sd);
        return -1;
    }

    ret = listen(server_sd, 10);
    if (ret != 0)
    {
        perror("listen failed:");
        close(server_sd);
        return -1;
    }

    FD_SET(server_sd, &master_set);
    fd_max = server_sd;


    for (;;)
        read_set = master_set;

        if (select(fd_max, &read_set, NULL, NULL, NULL) ==  -1)
        {
            perror("select");
            return -1;
        }

        for (i = 0; i < fd_max ; i++)
        {
            if (FD_ISSET(i, &read_set)) /* we got signal from socket */
            {
                if (i == server_sd) /* we have connected socket */
                {
                    if ( (newsd = accept(server_sd, (struct sockaddr *)&client_addr, &client_addr_len)) == -1);
                    {
                        perror("accept failed:");
                    }
                    else /* add new connection to socket pool */
                    {
                        FD_SET(newsd, &master_set)
                        if (newfd > max_fd)
                            max_fd = newsd
                        /* we got connection TODO send generated number */
                    }

                }
                else /* got data from clients interpret it */
                {

                    if ((recv_size = recv(i)) <= 0)
                    {
                        if (recv_size == 0) /* connection closed print notification */
                        {
                            printf(...);
                        }
                        else
                            perror("recv");
                        close(i);
                        FD_CLR(i, master_set);

                    }
                    else /* got actual data */
                        /*interpret it */
                    }
                }
        

            }



        }


    
    num = generate_number();

    while (tag != EQUAL)
    {
        recv_size = recv(client_sd, (void *)&buffer, sizeof(buffer), 0);
        if (recv_size == -1)
        {
            /*TODO handle errno = EAGAIN */
            perror("error on recv");
        }
        if (recv_size != sizeof(buffer))
        {
            fprintf(stderr, "WARNING: partial recv from server\n");
        }
           tag = EQUAL; 
        if (buffer < num)
        {
            tag = MORE;
        }
        else if (buffer > num)
        {
            tag = LESS;
        }
        else 
            tag = EQUAL;

        send_size = send(client_sd,(void *)&tag, sizeof(tag), 0);
        if (send_size == -1)
        {
            perror("error on send function\n");
            goto finalize;
        }
        if (send_size != sizeof(number))
        {
            /* if sending large buffer only need to send portion of buffer not send on previos send call */
            fprintf(stderr, "WARNING: partial send to server\n");
        }
    }

finalize:
    close(client_sd);
    close(server_sd);
    return 0;
}
