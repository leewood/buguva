#include "client.h"

int guess_number()
{
    int num;
    while (1)
    {
        fprintf(stderr, "Try to guess number from 1 to 100");
        fprintf(stderr, "Enter character\n");
        scanf("%d", &num);
        if (num > 1 && num < 100)
            return num;
    }
}

int main()
{
    int sd;
    struct sockaddr_in my_addr;
    int ret;
    int recv_size = 0;
    int send_size = 0;
    int buffer;
    int buffer_len = 0;
    int number = 0;
    int end_flag = 0;
    int yes = 1;

    sd = socket(PF_INET, SOCK_STREAM, 0);
    if (sd == -1)
    {
        perror("failed to create socket");
        return -1;
    }

    my_addr.sin_family = AF_INET;
    my_addr.sin_port = htons(MY_PORT);
    my_addr.sin_addr.s_addr = inet_addr("127.0.0.1");
    memset(my_addr.sin_zero, 0, sizeof(my_addr.sin_zero));

    if (setsockopt(sd, SOL_SOCKET,SO_REUSEADDR,&yes,sizeof(int)) == -1) {
        perror("setsockopt");
        exit(1);
    }

    ret = connect(sd, (const struct sockaddr*)&my_addr, sizeof(my_addr));
    if (ret != 0)
    {
        perror("failed to connect to destination");
        return -1;
    }
   
    number = guess_number();
    fprintf(stderr, "number = %d\n", number);
    while (1)
    {
        send_size = send(sd,(void *)&number, sizeof(number), 0);
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

        recv_size = recv(sd, (void *)&buffer, sizeof(buffer), 0);
        if (recv_size == -1)
        {
            /*TODO handle errno = EAGAIN */
            perror("error on recv");
        }
        if (recv_size != sizeof(buffer))
        {
            fprintf(stderr, "WARNING: partial recv from server\n");
        }
        
        switch((int)buffer)
        {
            case LESS:
                fprintf(stderr, "Number is less than %d\n", number);
                number = guess_number();
            break;
            case MORE:
                fprintf(stderr, "Number is greater than %d\n", number);
                number = guess_number();
            break;
            case EQUAL:
                fprintf(stderr, "Number is equal %d\n", number);
                fprintf(stderr, "You WIN\n");
                end_flag = 1;

            break;
            default:
                fprintf(stderr, "ERROR recieved number: %d\n", buffer);

        }
        fprintf(stderr, "number = %d\n", number);

        if (end_flag)
        {
            break;
        }
    }
    close(sd);
    return 0;

finalize:
    close(sd);
    return -1;
}
