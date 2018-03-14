/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：TrapMessage.cs
// 文件功能描述：Snmp报文Trap类;
// 创建人：郭亮;
// 版本：V1.0
// 创建标识：创建文件;
// 时间：2017-11-6
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using SnmpSharpNet;
using System.Threading;
using System.Windows;

namespace SCMTOperationCore.Message.SNMP
{
    // 后续变成泛型委托;
    public delegate void UpdateTrap(List<string> stringlist);

    public class TrapMessage : SnmpMessage
    {
        public List<string> TrapContent { get; set; }                              // Trap返回的具体内容;
        public static Thread WaitforTrap = new Thread(WaitTrap);                   // 执行Trap接收的线程;
        private static List<UpdateTrap> TrapNodifyList = new List<UpdateTrap>();   // 观察者列表;
        private static volatile bool WaitTrapRunstate = true;                      // 是否接收Trap的标志位;
        private static Socket socket;

        public TrapMessage(string Commnuity, string ipaddr) : base(Commnuity, ipaddr)
        {
        }
        public TrapMessage()
        {
        }

        /// <summary>
        /// 设置观察者列表;
        /// </summary>
        /// <param name="obj">需要被添加的观察者</param>
        public static void SetNodify(UpdateTrap obj)
        {
            TrapNodifyList.Add(obj);

            // 如果是第一次设置SetNodify，则启动Trap监听线程;
            if (TrapNodifyList.Count == 1)
            {
                TrapMessage.WaitforTrap.Start();
            }
        }

        /// <summary>
        /// 统一调用观察者的Update方法;
        /// </summary>
        /// <param name="Ret"></param>
        static public void Nodify(List<string> Ret)
        {
            foreach (UpdateTrap update in TrapNodifyList)
            {
                update(Ret);
            }
        }

        /// <summary>
        /// 接收Trap信息;
        /// </summary>
        static private void WaitTrap()
        {
            // 建立一个Socket实例，接收所有网口的IP地址，并绑定162端口; 
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 162);
            EndPoint ep = (EndPoint)ipep;

            socket.Bind(ep);
            // Disable timeout processing. Just block until packet is received 
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 0);

            int inlen = -1;
            List<string> Ret = new List<string>();

            while (WaitTrapRunstate)
            {
                byte[] indata = new byte[16 * 1024];
                // 16KB receive buffer int inlen = 0;
                IPEndPoint peer = new IPEndPoint(IPAddress.Any, 0);
                EndPoint inep = (EndPoint)peer;

                try
                {
                    inlen = socket.ReceiveFrom(indata, ref inep);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception {0}", ex.Message);
                    inlen = -1;
                }

                // 如果inlen大于0则证明接收到Trap;
                if (inlen > 0)
                {
                    // Check protocol version int 
                    int ver = SnmpPacket.GetProtocolVersion(indata, inlen);
                    if (ver == (int)SnmpVersion.Ver1)
                    {
                        Console.WriteLine("** Receive SNMP Version 1 TRAP data.");
                    }
                    else
                    {
                        // Parse SNMP Version 2 TRAP packet 
                        SnmpV2Packet pkt = new SnmpV2Packet();
                        pkt.decode(indata, inlen);
                        Console.WriteLine("** SNMP Version 2 TRAP received from {0}:", inep.ToString());
                        if ((SnmpSharpNet.PduType)pkt.Pdu.Type != PduType.V2Trap)
                        {
                            Console.WriteLine("*** NOT an SNMPv2 trap ****");
                        }
                        else
                        {
                            Console.WriteLine("*** Community: {0}", pkt.Community.ToString());
                            Console.WriteLine("*** VarBind count: {0}", pkt.Pdu.VbList.Count);
                            Console.WriteLine("*** VarBind content:");
                            foreach (Vb v in pkt.Pdu.VbList)
                            {
                                Console.WriteLine("**** {0} {1}: {2}",
                                   v.Oid.ToString(), SnmpConstants.GetTypeName(v.Value.Type), v.Value.ToString());
                                string temp = "接收到Trap: Oid:" + v.Oid.ToString() + ", TypeName:" +
                                    SnmpConstants.GetTypeName(v.Value.Type) + ", Value:" + v.Value.ToString();

                                Ret.Add(temp);
                            }
                            // 通知观察者;
                            Nodify(Ret);
                        }
                    }
                }
                else
                {
                    if (inlen == 0)
                        Console.WriteLine("Zero length packet received.");
                }

            }
            Console.WriteLine("Trap waiting thread: terminating gracefully.");
        }

        public static void RequestStop()
        {
            if(TrapNodifyList.Count != 0)
            {
                socket.Dispose();                             // 释放线程;
                WaitTrapRunstate = false;                     // 释放监听套接字;
                return;
            }
            else
            {
                return;
            }
        }

        //______________________________________________________________________Trap不需要获取数据，以下全部不继承____
        public override Dictionary<string, string> GetRequest(List<string> PduList, string Community, string IpAddress)
        {
            throw new NotImplementedException();
        }

        public override void SetRequest(Dictionary<string, string> PduList, string Community, string IpAddress)
        {
            throw new NotImplementedException();
        }

        public override void GetRequest(AsyncCallback callback, List<string> PduList, string Community, string IpAddress)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, string> GetRequest(List<string> PduList)
        {
            throw new NotImplementedException();
        }
     
        public override void SetRequest(AsyncCallback callback, List<string> PduList)
        {
            throw new NotImplementedException();
        }

        public override void GetNextRequest(AsyncCallback callback, List<string> PduList)
        {
            throw new NotImplementedException();
        }
    }
}
