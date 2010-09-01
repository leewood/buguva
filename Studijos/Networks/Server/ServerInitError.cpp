#include <string>
#include <iostream>
#include <winsock2.h>

using namespace std;

class ServerInitError
{
      public:
      string error;
      int errorCode;
      ServerInitError(string errorCaption)
      {
         error = errorCaption;
         errorCode = 0;
      };
      
      ServerInitError(int errorCode)
      {
         this->errorCode = errorCode;
         this->error = errorCodeToString(errorCode);
      };      
      
      
      bool isCritical()
      {
           switch (errorCode)
           {
              case 0: return false;
              case WSAECONNRESET: return false;
              case WSAENOTSOCK: return false;
              default: return true;
           }
      }
      
      string errorCodeToString(int errorCode)
      {
             switch (errorCode)
             {
                case WSA_INVALID_HANDLE: return "Specified event object handle is invalid.";
                case WSA_NOT_ENOUGH_MEMORY: return "Insufficient memory available.";
                case WSA_INVALID_PARAMETER: return "One or more parameters are invalid.";
                case WSA_OPERATION_ABORTED: return "Overlapped operation aborted.";
                case WSA_IO_INCOMPLETE: return "Overlapped I/O event object not in signaled state.";
                case WSA_IO_PENDING: return "Overlapped operations will complete later. ";
                case WSAEINTR: return "Interrupted function call.";
                case WSAEBADF: return "File handle is not valid.";
                case WSAEACCES: return "Permission denied.";
                case WSAEFAULT: return "Bad address.";
                case WSAEINVAL: return "Invalid argument.";
                case WSAEMFILE: return "Too many open files.";
                case WSAEWOULDBLOCK: return "Resource temporarily unavailable.";
                case WSAEINPROGRESS: return "Operation now in progress.";
                case WSAEALREADY: return "Operation already in progress.";
                case WSAENOTSOCK: return "Socket operation on nonsocket.";
                case WSAEDESTADDRREQ: return "Destination address required.";
                case WSAEMSGSIZE: return "Message too long.";
                case WSAEPROTOTYPE: return "Protocol wrong type for socket.";
                case WSAENOPROTOOPT: return "Bad protocol option.";
                case WSAEPROTONOSUPPORT: return "Protocol not supported.";
                case WSAESOCKTNOSUPPORT: return "Socket type not supported.";
                case WSAEOPNOTSUPP: return "Operation not supported.";
                case WSAEPFNOSUPPORT: return "Protocol family not supported.";
                case WSAEAFNOSUPPORT: return "Address family not supported by protocol family.";
                case WSAEADDRINUSE: return "Address already in use.";
                case WSAEADDRNOTAVAIL: return "Cannot assign requested address.";
                case WSAENETDOWN: return "Network is down.";
                case WSAENETUNREACH: return "Network is unreachable.";
                case WSAENETRESET: return "Network dropped connection on reset.";
                case WSAECONNABORTED: return "Software caused connection abort.";
                case WSAECONNRESET: return "Connection reset by peer.";
                case WSAENOBUFS: return "No buffer space available.";
                case WSAEISCONN: return "Socket is already connected.";
                case WSAENOTCONN: return "Socket is not connected.";
                case WSAESHUTDOWN: return "Cannot send after socket shutdown.";
                case WSAETOOMANYREFS: return "Too many references.";
                case WSAETIMEDOUT: return "Connection timed out.";
                case WSAECONNREFUSED: return "Connection refused.";
                case WSAELOOP: return "Cannot translate name.";
                case WSAENAMETOOLONG: return "Name too long.";
                case WSAEHOSTDOWN: return "Host is down.";
                case WSAEHOSTUNREACH: return "No route to host.";
                case WSAENOTEMPTY: return "Directory not empty.";
                case WSAEPROCLIM: return "Too many processes.";
                case WSAEUSERS: return "User quota exceeded.";
                case WSAEDQUOT: return "Disk quota exceeded.";
                case WSAESTALE: return "Stale file handle reference.";
                case WSAEREMOTE: return "Item is remote.";
                case WSASYSNOTREADY: return "Network subsystem is unavailable.";                
                case WSAVERNOTSUPPORTED: return "Winsock.dll version out of range.";
                case WSANOTINITIALISED: return "Successful WSAStartup not yet performed.";
                case WSAEDISCON: return "Graceful shutdown in progress.";
                case WSAENOMORE: return "No more results.";
                case WSAECANCELLED: return "Call has been canceled.";
                case WSAEINVALIDPROCTABLE: return "Procedure call table is invalid.";
                case WSAEINVALIDPROVIDER: return "Service provider is invalid.";
                case WSAEPROVIDERFAILEDINIT: return "Service provider failed to initialize.";
                case WSASYSCALLFAILURE: return "System call failure.";
                case WSASERVICE_NOT_FOUND: return "Service not found.";
                case WSATYPE_NOT_FOUND: return "Class type not found.";
                case WSA_E_NO_MORE: return "No more results.";
                case WSA_E_CANCELLED: return "Call was canceled.";
                case WSAEREFUSED: return "Database query was refused.";
                case WSAHOST_NOT_FOUND: return "Host not found.";
                case WSATRY_AGAIN: return "Nonauthoritative host not found.";
                case WSANO_RECOVERY: return "This is a nonrecoverable error.";
                case WSANO_DATA: return "Valid name, no data record of requested type.";
                case WSA_QOS_RECEIVERS: return "QOS receivers.";
                case WSA_QOS_SENDERS: return "QOS senders.";
                case WSA_QOS_NO_SENDERS: return "No QOS senders.";
                case WSA_QOS_NO_RECEIVERS: return "QOS no receivers.";
                case WSA_QOS_REQUEST_CONFIRMED: return "QOS request confirmed.";
                case WSA_QOS_ADMISSION_FAILURE: return "QOS admission error.";
                case WSA_QOS_POLICY_FAILURE: return "QOS policy failure.";
                case WSA_QOS_BAD_STYLE: return "QOS bad style.";
                case WSA_QOS_BAD_OBJECT: return "QOS bad object.";
                case WSA_QOS_TRAFFIC_CTRL_ERROR: return "QOS traffic control error.";
                case WSA_QOS_GENERIC_ERROR: return "QOS generic error.";
                case WSA_QOS_ESERVICETYPE: return "QOS service type error.";
                case WSA_QOS_EFLOWSPEC: return "QOS flowspec error.";
                case WSA_QOS_EPROVSPECBUF: return "Invalid QOS provider buffer.";
                case WSA_QOS_EFILTERSTYLE: return "Invalid QOS filter style.";
                case WSA_QOS_EFILTERTYPE: return "Invalid QOS filter type.";
                case WSA_QOS_EFILTERCOUNT: return "Incorrect QOS filter count.";
                case WSA_QOS_EOBJLENGTH: return "Invalid QOS object length.";
                case WSA_QOS_EFLOWCOUNT: return "Incorrect QOS flow count.";
//                case WSA_QOS_EUNKOWNPSOBJ: return "Unrecognized QOS object.";
                case WSA_QOS_EPOLICYOBJ: return "Invalid QOS policy object.";
                case WSA_QOS_EFLOWDESC: return "Invalid QOS flow descriptor.";
                case WSA_QOS_EPSFLOWSPEC: return "Invalid QOS provider-specific flowspec.";
                case WSA_QOS_EPSFILTERSPEC: return "Invalid QOS provider-specific filterspec.";
                case WSA_QOS_ESDMODEOBJ: return "Invalid QOS shape discard mode object.";
                case WSA_QOS_ESHAPERATEOBJ: return "Invalid QOS shaping rate object.";
                case WSA_QOS_RESERVED_PETYPE: return "Reserved policy QOS element type.";
                default: return "";
             }
      };
      
      
      string errorCodeToDetailedString(int errorCode)
      {
             switch (errorCode)
             {
                    case WSA_INVALID_HANDLE: return "An application attempts to use an event object, but the specified handle is not valid. Note that this error is returned by the operating system, so the error number may change in future releases of Windows.";
                    case WSA_NOT_ENOUGH_MEMORY: return "An application used a Windows Sockets function that directly maps to a Windows function. The Windows function is indicating a lack of required memory resources. Note that this error is returned by the operating system, so the error number may change in future releases of Windows.";
                    case WSA_INVALID_PARAMETER: return "An application used a Windows Sockets function which directly maps to a Windows function. The Windows function is indicating a problem with one or more parameters. Note that this error is returned by the operating system, so the error number may change in future releases of Windows.";
                    case WSA_OPERATION_ABORTED: return "An overlapped operation was canceled due to the closure of the socket, or the execution of the SIO_FLUSH command in WSAIoctl. Note that this error is returned by the operating system, so the error number may change in future releases of Windows.";
                    case WSA_IO_INCOMPLETE: return "The application has tried to determine the status of an overlapped operation which is not yet completed. Applications that use WSAGetOverlappedResult (with the fWait flag set to FALSE) in a polling mode to determine when an overlapped operation has completed, get this error code until the operation is complete. Note that this error is returned by the operating system, so the error number may change in future releases of Windows.";
                    case WSA_IO_PENDING: return "The application has initiated an overlapped operation that cannot be completed immediately. A completion indication will be given later when the operation has been completed. Note that this error is returned by the operating system, so the error number may change in future releases of Windows.";
                    case WSAEINTR: return "A blocking operation was interrupted by a call to WSACancelBlockingCall.";
                    case WSAEBADF: return "The file handle supplied is not valid.";
                    case WSAEACCES: return "An attempt was made to access a socket in a way forbidden by its access permissions. An example is using a broadcast address for sendto without broadcast permission being set using setsockopt(SO_BROADCAST). Another possible reason for the WSAEACCES error is that when the bind function is called (on Windows NT 4 SP4 or later), another application, service, or kernel mode driver is bound to the same address with exclusive access. Such exclusive access is a new feature of Windows NT 4 SP4 and later, and is implemented by using the SO_EXCLUSIVEADDRUSE option.";
                    case WSAEFAULT: return "The system detected an invalid pointer address in attempting to use a pointer argument of a call. This error occurs if an application passes an invalid pointer value, or if the length of the buffer is too small. For instance, if the length of an argument, which is a sockaddr structure, is smaller than the sizeof(sockaddr).";
                    case WSAEINVAL: return "Some invalid argument was supplied (for example, specifying an invalid level to the setsockopt function). In some instances, it also refers to the current state of the socket�for instance, calling accept on a socket that is not listening.";
                    case WSAEMFILE: return "Too many open sockets. Each implementation may have a maximum number of socket handles available, either globally, per process, or per thread.";
                    case WSAEWOULDBLOCK: return "This error is returned from operations on nonblocking sockets that cannot be completed immediately, for example recv when no data is queued to be read from the socket. It is a nonfatal error, and the operation should be retried later. It is normal for WSAEWOULDBLOCK to be reported as the result from calling connect on a nonblocking SOCK_STREAM socket, since some time must elapse for the connection to be established.";
                    case WSAEINPROGRESS: return "A blocking operation is currently executing. Windows Sockets only allows a single blocking operation�per- task or thread�to be outstanding, and if any other function call is made (whether or not it references that or any other socket) the function fails with the WSAEINPROGRESS error.";
                    case WSAEALREADY: return "An operation was attempted on a nonblocking socket with an operation already in progress�that is, calling connect a second time on a nonblocking socket that is already connecting, or canceling an asynchronous request (WSAAsyncGetXbyY) that has already been canceled or completed.";
                    case WSAENOTSOCK: return "An operation was attempted on something that is not a socket. Either the socket handle parameter did not reference a valid socket, or for select, a member of an fd_set was not valid.";
                    case WSAEDESTADDRREQ: return "A required address was omitted from an operation on a socket. For example, this error is returned if sendto is called with the remote address of ADDR_ANY.";
                    case WSAEMSGSIZE: return "A message sent on a datagram socket was larger than the internal message buffer or some other network limit, or the buffer used to receive a datagram was smaller than the datagram itself.";
                    case WSAEPROTOTYPE: return "A protocol was specified in the socket function call that does not support the semantics of the socket type requested. For example, the ARPA Internet UDP protocol cannot be specified with a socket type of SOCK_STREAM.";
                    case WSAENOPROTOOPT: return "An unknown, invalid or unsupported option or level was specified in a getsockopt or setsockopt call.";
                    case WSAEPROTONOSUPPORT: return "The requested protocol has not been configured into the system, or no implementation for it exists. For example, a socket call requests a SOCK_DGRAM socket, but specifies a stream protocol.";
                    case WSAESOCKTNOSUPPORT: return "The support for the specified socket type does not exist in this address family. For example, the optional type SOCK_RAW might be selected in a socket call, and the implementation does not support SOCK_RAW sockets at all.";
                    case WSAEOPNOTSUPP: return "The attempted operation is not supported for the type of object referenced. Usually this occurs when a socket descriptor to a socket that cannot support this operation is trying to accept a connection on a datagram socket.";
                    case WSAEPFNOSUPPORT: return "The protocol family has not been configured into the system or no implementation for it exists. This message has a slightly different meaning from WSAEAFNOSUPPORT. However, it is interchangeable in most cases, and all Windows Sockets functions that return one of these messages also specify WSAEAFNOSUPPORT.";
                    case WSAEAFNOSUPPORT: return "An address incompatible with the requested protocol was used. All sockets are created with an associated address family (that is, AF_INET for Internet Protocols) and a generic protocol type (that is, SOCK_STREAM). This error is returned if an incorrect protocol is explicitly requested in the socket call, or if an address of the wrong family is used for a socket, for example, in sendto.";
                    case WSAEADDRINUSE: return "Typically, only one usage of each socket address (protocol/IP address/port) is permitted. This error occurs if an application attempts to bind a socket to an IP address/port that has already been used for an existing socket, or a socket that was not closed properly, or one that is still in the process of closing. For server applications that need to bind multiple sockets to the same port number, consider using setsockopt (SO_REUSEADDR). Client applications usually need not call bind at all�connect chooses an unused port automatically. When bind is called with a wildcard address (involving ADDR_ANY), a WSAEADDRINUSE error could be delayed until the specific address is committed. This could happen with a call to another function later, including connect, listen, WSAConnect, or WSAJoinLeaf.";
                    case WSAEADDRNOTAVAIL: return "The requested address is not valid in its context. This normally results from an attempt to bind to an address that is not valid for the local computer. This can also result from connect, sendto, WSAConnect, WSAJoinLeaf, or WSASendTo when the remote address or port is not valid for a remote computer (for example, address or port 0).";
                    case WSAENETDOWN: return "A socket operation encountered a dead network. This could indicate a serious failure of the network system (that is, the protocol stack that the Windows Sockets DLL runs over), the network interface, or the local network itself.";
                    case WSAENETUNREACH: return "A socket operation was attempted to an unreachable network. This usually means the local software knows no route to reach the remote host.";
                    case WSAENETRESET: return "The connection has been broken due to keep-alive activity detecting a failure while the operation was in progress. It can also be returned by setsockopt if an attempt is made to set SO_KEEPALIVE on a connection that has already failed.";
                    case WSAECONNABORTED: return "An established connection was aborted by the software in your host computer, possibly due to a data transmission time-out or protocol error.";
                    case WSAECONNRESET: return "An existing connection was forcibly closed by the remote host. This normally results if the peer application on the remote host is suddenly stopped, the host is rebooted, the host or remote network interface is disabled, or the remote host uses a hard close (see setsockopt for more information on the SO_LINGER option on the remote socket). This error may also result if a connection was broken due to keep-alive activity detecting a failure while one or more operations are in progress. Operations that were in progress fail with WSAENETRESET. Subsequent operations fail with WSAECONNRESET.";
                    case WSAENOBUFS: return "An operation on a socket could not be performed because the system lacked sufficient buffer space or because a queue was full.";
                    case WSAEISCONN: return "A connect request was made on an already-connected socket. Some implementations also return this error if sendto is called on a connected SOCK_DGRAM socket (for SOCK_STREAM sockets, the to parameter in sendto is ignored) although other implementations treat this as a legal occurrence.";
                    case WSAENOTCONN: return "A request to send or receive data was disallowed because the socket is not connected and (when sending on a datagram socket using sendto) no address was supplied. Any other type of operation might also return this error�for example, setsockopt setting SO_KEEPALIVE if the connection has been reset.";
                    case WSAESHUTDOWN: return "A request to send or receive data was disallowed because the socket had already been shut down in that direction with a previous shutdown call. By calling shutdown a partial close of a socket is requested, which is a signal that sending or receiving, or both have been discontinued.";
                    case WSAETOOMANYREFS: return "Too many references to some kernel object.";
                    case WSAETIMEDOUT: return "A connection attempt failed because the connected party did not properly respond after a period of time, or the established connection failed because the connected host has failed to respond.";
                    case WSAECONNREFUSED: return "No connection could be made because the target computer actively refused it. This usually results from trying to connect to a service that is inactive on the foreign host�that is, one with no server application running.";
                    case WSAELOOP: return "Cannot translate a name.";
                    case WSAENAMETOOLONG: return "A name component or a name was too long.";
                    case WSAEHOSTDOWN: return "A socket operation failed because the destination host is down. A socket operation encountered a dead host. Networking activity on the local host has not been initiated. These conditions are more likely to be indicated by the error WSAETIMEDOUT.";
                    case WSAEHOSTUNREACH: return "A socket operation was attempted to an unreachable host. See WSAENETUNREACH.";
                    case WSAENOTEMPTY: return "Cannot remove a directory that is not empty.";
                    case WSAEPROCLIM: return "A Windows Sockets implementation may have a limit on the number of applications that can use it simultaneously.WSAStartup may fail with this error if the limit has been reached.";
                    case WSAEUSERS: return "Ran out of user quota.";
                    case WSAEDQUOT: return "Ran out of disk quota.";
                    case WSAESTALE: return "The file handle reference is no longer available.";
                    case WSAEREMOTE: return "The item is not available locally.";
                    case WSASYSNOTREADY: return "This error is returned by WSAStartup if the Windows Sockets implementation cannot function at this time because the underlying system it uses to provide network services is currently unavailable. Users should check: * That the appropriate Windows Sockets DLL file is in the current path. * That they are not trying to use more than one Windows Sockets implementation simultaneously. If there is more than one Winsock DLL on your system, be sure the first one in the path is appropriate for the network subsystem currently loaded. * The Windows Sockets implementation documentation to be sure all necessary components are currently installed and configured correctly.";
                    case WSAVERNOTSUPPORTED: return "The current Windows Sockets implementation does not support the Windows Sockets specification version requested by the application. Check that no old Windows Sockets DLL files are being accessed.";
                    case WSANOTINITIALISED: return "Either the application has not called WSAStartup or WSAStartup failed. The application may be accessing a socket that the current active task does not own (that is, trying to share a socket between tasks), or WSACleanup has been called too many times.";
                    case WSAEDISCON: return "Returned by WSARecv and WSARecvFrom to indicate that the remote party has initiated a graceful shutdown sequence.";
                    case WSAENOMORE: return "No more results can be returned by the WSALookupServiceNext function.";
                    case WSAECANCELLED: return "A call to the WSALookupServiceEnd function was made while this call was still processing. The call has been canceled.";
                    case WSAEINVALIDPROCTABLE: return "The service provider procedure call table is invalid. A service provider returned a bogus procedure table to Ws2_32.dll. This is usually caused by one or more of the function pointers being NULL.";
                    case WSAEINVALIDPROVIDER: return "The requested service provider is invalid. This error is returned by the WSCGetProviderInfo and WSCGetProviderInfo32 functions if the protocol entry specified could not be found. This error is also returned if the service provider returned a version number other than 2.0.";
                    case WSAEPROVIDERFAILEDINIT: return "The requested service provider could not be loaded or initialized. This error is returned if either a service provider's DLL could not be loaded (LoadLibrary failed) or the provider's WSPStartup or NSPStartup function failed.";
                    case WSASYSCALLFAILURE: return "A system call that should never fail has failed. This is a generic error code, returned under various conditions. Returned when a system call that should never fail does fail. For example, if a call to WaitForMultipleEvents fails or one of the registry functions fails trying to manipulate the protocol/namespace catalogs. Returned when a provider does not return SUCCESS and does not provide an extended error code. Can indicate a service provider implementation error.";
                    case WSASERVICE_NOT_FOUND: return "No such service is known. The service cannot be found in the specified name space.";
                    case WSATYPE_NOT_FOUND: return "The specified class was not found.";
                    case WSA_E_NO_MORE: return "No more results can be returned by the WSALookupServiceNext function.";
                    case WSA_E_CANCELLED: return "A call to the WSALookupServiceEnd function was made while this call was still processing. The call has been canceled.";
                    case WSAEREFUSED: return "A database query failed because it was actively refused.";
                    case WSAHOST_NOT_FOUND: return "No such host is known. The name is not an official host name or alias, or it cannot be found in the database(s) being queried. This error may also be returned for protocol and service queries, and means that the specified name could not be found in the relevant database.";
                    case WSATRY_AGAIN: return "This is usually a temporary error during host name resolution and means that the local server did not receive a response from an authoritative server. A retry at some time later may be successful.";
                    case WSANO_RECOVERY: return "This indicates that some sort of nonrecoverable error occurred during a database lookup. This may be because the database files (for example, BSD-compatible HOSTS, SERVICES, or PROTOCOLS files) could not be found, or a DNS request was returned by the server with a severe error.";
                    case WSANO_DATA: return "The requested name is valid and was found in the database, but it does not have the correct associated data being resolved for. The usual example for this is a host name-to-address translation attempt (using gethostbyname or WSAAsyncGetHostByName) which uses the DNS (Domain Name Server). An MX record is returned but no A record�indicating the host itself exists, but is not directly reachable.";
                    case WSA_QOS_RECEIVERS: return "At least one QOS reserve has arrived.";
                    case WSA_QOS_SENDERS: return "At least one QOS send path has arrived.";
                    case WSA_QOS_NO_SENDERS: return "There are no QOS senders.";
                    case WSA_QOS_NO_RECEIVERS: return "There are no QOS receivers.";
                    case WSA_QOS_REQUEST_CONFIRMED: return "The QOS reserve request has been confirmed.";
                    case WSA_QOS_ADMISSION_FAILURE: return "A QOS error occurred due to lack of resources.";
                    case WSA_QOS_POLICY_FAILURE: return "The QOS request was rejected because the policy system couldn't allocate the requested resource within the existing policy.";
                    case WSA_QOS_BAD_STYLE: return "An unknown or conflicting QOS style was encountered.";
                    case WSA_QOS_BAD_OBJECT: return "A problem was encountered with some part of the filterspec or the provider-specific buffer in general.";
                    case WSA_QOS_TRAFFIC_CTRL_ERROR: return "An error with the underlying traffic control (TC) API as the generic QOS request was converted for local enforcement by the TC API. This could be due to an out of memory error or to an internal QOS provider error.";
                    case WSA_QOS_GENERIC_ERROR: return "A general QOS error.";
                    case WSA_QOS_ESERVICETYPE: return "An invalid or unrecognized service type was found in the QOS flowspec.";
                    case WSA_QOS_EFLOWSPEC: return "An invalid or inconsistent flowspec was found in the QOS structure.";
                    case WSA_QOS_EPROVSPECBUF: return "An invalid QOS provider-specific buffer.";
                    case WSA_QOS_EFILTERSTYLE: return "An invalid QOS filter style was used.";
                    case WSA_QOS_EFILTERTYPE: return "An invalid QOS filter type was used.";
                    case WSA_QOS_EFILTERCOUNT: return "An incorrect number of QOS FILTERSPECs were specified in the FLOWDESCRIPTOR.";
                    case WSA_QOS_EOBJLENGTH: return "An object with an invalid ObjectLength field was specified in the QOS provider-specific buffer.";
                    case WSA_QOS_EFLOWCOUNT: return "An incorrect number of flow descriptors was specified in the QOS structure.";
    //                case WSA_QOS_EUNKOWNPSOBJ: return "An unrecognized object was found in the QOS provider-specific buffer.";
                    case WSA_QOS_EPOLICYOBJ: return "An invalid policy object was found in the QOS provider-specific buffer.";
                    case WSA_QOS_EFLOWDESC: return "An invalid QOS flow descriptor was found in the flow descriptor list.";
                    case WSA_QOS_EPSFLOWSPEC: return "An invalid or inconsistent flowspec was found in the QOS provider-specific buffer.";
                    case WSA_QOS_EPSFILTERSPEC: return "An invalid FILTERSPEC was found in the QOS provider-specific buffer.";
                    case WSA_QOS_ESDMODEOBJ: return "An invalid shape discard mode object was found in the QOS provider-specific buffer.";
                    case WSA_QOS_ESHAPERATEOBJ: return "An invalid shaping rate object was found in the QOS provider-specific buffer.";
                    case WSA_QOS_RESERVED_PETYPE: return "A reserved policy element was found in the QOS provider-specific buffer.";

                    default: return "";
                    
             }
     };
};
