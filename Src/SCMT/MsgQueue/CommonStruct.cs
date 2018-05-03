using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsgQueue
{
	public class Target : IComparable
	{
		public string addr;
		public int port;

		public int CompareTo(object obj)
		{
			var temp = obj as Target;
			if (null == temp)
			{
				return -1;
			}

			if (temp.addr == addr && temp.port == port)
			{
				return 0;
			}

			return 1;
		}
	}

	public class SessionData
	{
		public byte[] data;
		public Target target;
	}
}
