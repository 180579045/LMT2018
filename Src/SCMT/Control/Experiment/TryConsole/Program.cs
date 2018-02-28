using System;
using SCMTOperationCore;
using System.Net;

namespace TryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            NodeBElement nb = new NodeBElement();
            nb.m_IPAddress = new IPAddress(new byte[] { 172, 27, 245, 92 });
            nb.Connect();

            Console.WriteLine("Connecting");
            Console.ReadLine();
        }
    }
}
