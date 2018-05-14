using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AtpMessage.GtsMsgParse;
using AtpMessage.LinkMgr;
using AtpMessage.SessionMgr;

namespace AtpMessage
{
	//ATP内部模块初始化
	public class AtpInitial
	{
		public static int Init()
		{
			SessionService.GetInstance();
			LinkMgrActor.GetInstance();
			GtsMsgParseService.GetInstance().InitService();

			return 0;
		}

		public static void Stop()
		{
			LinkMgrActor.GetInstance().Dispose();
			SessionService.GetInstance().Dispose();
			GtsMsgParseService.GetInstance().Dispose();
		}
	}
}
