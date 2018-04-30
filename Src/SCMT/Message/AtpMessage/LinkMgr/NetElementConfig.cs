using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpMessage.LinkMgr
{
	//以下信息来自于nbinformation.xml
	public class NetElementConfig
	{
		public string TraceIp;		//追踪IP地址，就是本地和板卡通信的IP地址
		public byte AgentSlot;		//代理槽位号
		public ushort Index;		//板卡ID
		public ushort FrameNo;
		public ushort SlotNo;
	}
}
