using System;
using SCMTOperationCore;
using System.Net;

namespace TryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // 客户端可以实例化一个si实例;
            SiElement nb = new SiElement();
            nb.m_IPAddress = IPAddress.Parse("172.27.245.92"); 
            nb.Connect();

            Console.WriteLine("Connecting");
            Console.ReadLine();
        }
    }
}
