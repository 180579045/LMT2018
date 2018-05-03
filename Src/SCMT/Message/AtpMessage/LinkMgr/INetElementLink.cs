using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpMessage.LinkMgr
{
	public interface INetElementLink
	{
		void Connect(NetElementConfig netElementAddress, bool isRepeatConnect = true);

		void Disconnect();

		bool IsConnected();

		void OnLogonResult(bool bSucceed);
	}
}
