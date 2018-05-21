using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections;
using SCMTOperationCore.Connection;

namespace SCMTOperationCore.Elements
{
	/// <summary>
	/// 所有网元类型的基类;
	/// </summary>
	public abstract class Element
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="friendName">友好名。可查可改</param>
		/// <param name="neIp">网元IP，可查可改</param>
		/// <param name="nePort">网元端口，可查可改。默认为5000端口</param>
		public Element(string friendName, IPAddress neIp, ushort nePort = 5000)
		{
			FriendlyName = friendName;
			NeAddress = neIp;
			NePort = nePort;
		}

		public string FriendlyName { get; set; }

		//IPAddress可以是IPV6的地址
		public IPAddress NeAddress { get; set; }

		public ushort NePort { get; set; }
	}
}
