using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MicroClient
{
    public enum MessageType { 
        Hi
    }
    public class TCPclient
    {
        TcpClient client = new TcpClient();

        string hostname = "127.0.0.1";
        int port = 2313;
        NetworkStream _stream;
        StreamWriter writer;
        StreamReader reader;

        public TCPclient(string host, int openPort)
        {
            hostname = host;
            port = openPort;
            client.Connect(hostname, port);
            _stream = client.GetStream();
            reader = new StreamReader(_stream, Encoding.UTF8);
            writer = new StreamWriter(_stream, Encoding.UTF8);
        }

        public async void SendMessages(string message, MessageType type)
        {
            writer.WriteLineAsync("hello");
        }

        public async void ReceiveMessages()
        {
            var response = await reader.ReadLineAsync();
            Console.WriteLine(response);
        }
    }
}
