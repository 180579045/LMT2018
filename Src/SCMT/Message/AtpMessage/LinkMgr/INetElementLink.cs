using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtpMessage.LinkMgr
{
	public interface INetElementLink
	{
		/// <summary>
		/// 从MML移植
		/// TODO 需要适当的调整
		/// </summary>
		/// <param name="netElementAddress"></param>
		/// <param name="isRepeatConnect"></param>
		void Connect(NetElementConfig netElementAddress, bool isRepeatConnect = true);

		void Disconnect();

		bool IsConnected();

	    void OnLogonResult(bool bSucceed);
	}
}
