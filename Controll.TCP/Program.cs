using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Controll.TCP
{
    class Program
    {
        private static TcpClient _client;
        private static TcpListener _listener;
        private static IPAddress _address;

        static void Main(string[] args)
        {
            _address = Dns.GetHostAddresses(Dns.GetHostName())[1];
            var endPoint = new IPEndPoint(IPAddress.Parse(_address.ToString()), 1234);  
            _listener = new TcpListener(endPoint);  
            _listener.Start();  
            Console.WriteLine(@" 
                    ===================================================  
                    Started listening requests at: {0}:{1}  
                    ===================================================",  
                endPoint.Address, endPoint.Port);  
            _client = _listener.AcceptTcpClient();  
            Console.WriteLine("Connected to client!" + " \n");
            while (_client.Connected)
            {
                var networkStream = _client.GetStream();
                if (networkStream.DataAvailable)
                {
                    var bytes = new byte[1024];
                    networkStream.Read(bytes, 0, 1024);
                    var buffer = bytes.Where(x => x != null).ToArray();

                    var file = Encoding.ASCII.GetString(buffer);
                    StartApp(file);
                }                                                                                                                               
            }                                                                   
            
            _client.Dispose();
            
            _client.Close();
            
        }
        
        private static void StartApp(string fileMame)
        {
            Process.Start(fileMame);
        }
    }
}