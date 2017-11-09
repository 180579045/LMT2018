using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using SnmpSharpNet;
using System.Threading;

namespace Snmp_dll
{
    public delegate void UpdateTrap(List<string> stringlist);

    public class TrapMessage : SnmpMessage
    {
        public List<string> TrapContent { get; set; }               // Trap返回的具体内容;
        public static Thread WaitforTrap = new Thread(WaitTrap);    // 执行Trap接收的线程;
        private static List<UpdateTrap> TrapShow = new List<UpdateTrap>();                   // 观察者列表;

        /// <summary>
        /// 设置观察者列表;
        /// </summary>
        /// <param name="obj">需要被添加的观察者</param>
        public static void SetNodify(UpdateTrap obj)
        {
            TrapShow.Add(obj);
        }

        /// <summary>
        /// 统一调用观察者的Update方法;
        /// </summary>
        /// <param name="Ret"></param>
        static public void Nodify(List<string> Ret)
        {
            foreach(UpdateTrap update in TrapShow)
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
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 162);
            EndPoint ep = (EndPoint)ipep;

            socket.Bind(ep);
            // Disable timeout processing. Just block until packet is received 
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 0);
            bool run = true;
            int inlen = -1;
            List<string> Ret = new List<string>();

            while (run)
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
                                string temp = "**** Oid:" + v.Oid.ToString() + ", Name:" + SnmpConstants.GetTypeName(v.Value.Type) + ", Value:" + v.Value.ToString();
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
        }

        public override Dictionary<string, string> GetRequest(List<string> PduList, string Community, string IpAddress)
        {
            throw new NotImplementedException();
        }

    }
}
