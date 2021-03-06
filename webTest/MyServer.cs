﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Server
/// </summary>
namespace website
{
    public class MyServer
    {
        public static string message = getMyIP();
        public static bool running = false;

        public static void run(Label l)
        {
            if (!running)
                ThreadPool.QueueUserWorkItem(start, l);
        }
        public static void start(Object o)
        {
            while (true)
            {
                TcpListener server = new TcpListener(IPAddress.Parse(getMyIP()), 1234);
                server.Start();
                running = true;
                TcpClient client = server.AcceptTcpClient();
                message = getRequest(client);
                ((Label)o).Text = message;
                byte[] nytes = Encoding.UTF8.GetBytes("I AM THE SERVER".ToCharArray());

                client.GetStream().Write(nytes, 0, "I AM THE SERVER".Length);
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