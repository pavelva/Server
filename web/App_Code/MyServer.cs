using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;

/// <summary>
/// Summary description for Server
/// </summary>
namespace website
{
    public class MyServer
    {
        public static string message = getMyIP();
        public static bool running = false;

        public static void run()
        {
            if(!running)
                ThreadPool.QueueUserWorkItem(start);
        }
        public static void start(Object o)
        {
            while (true)
            {
                //TcpListener server = new TcpListener(IPAddress.Parse(getMyIP()), 1234);
                //server.Start();
                //running = true;
                //TcpClient client = server.AcceptTcpClient();
                //message = getRequest(client);
            }
        }

        private static string getRequest(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            StringBuilder data = new StringBuilder();

            while (!stream.DataAvailable) ;

            Byte[] bytes = new Byte[client.Available];

            stream.Read(bytes, 0, bytes.Length);

            data.Append(Encoding.UTF8.GetString(bytes));
            //console.writeLine("Client Request String:\n" + data.ToString());
            //console.writeLine("Client Address: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
            return data.ToString();
        }

        private static string getMyIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            Console.WriteLine(localIP);

            return localIP;
        }
    }
}