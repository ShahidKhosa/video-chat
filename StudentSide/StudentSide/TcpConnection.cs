using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace StudentSide
{
    class TcpConnection
    {
        Socket tcpSocket;
        EndPoint localEP;
        EndPoint serverEP;

        public Socket CreateConnection()
        {
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            localEP = new IPEndPoint(IPAddress.Any, 1450);

        }
    }
}
