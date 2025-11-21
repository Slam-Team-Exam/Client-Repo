using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MicroClient
{
    internal class TCPclient
    {
        TcpClient client = new TcpClient();

        string hostname = "127.0.0.1";
        int port = 2313;

        public TCPclient()
        {
            client.Connect(hostname, port);
        }
    }
}
