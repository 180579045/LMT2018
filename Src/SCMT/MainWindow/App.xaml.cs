using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AtpMessage;
using AtpMessage.LinkMgr;
using MsgQueue;
using MsgDispatcher;

namespace SCMTMainWindow
{
	/// <summary>
	/// App.xaml 的交互逻辑
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			MqInitial.Init();
			ConnectWorker.GetInstance();
			DoMsgDispatcher.GetInstance();
			//AtpInitial.Init();

			//NetElementConfig config = new NetElementConfig()
			//{
			//	TraceIp = "172.27.245.82",
			//	AgentSlot = 2,
			//	Index = 3,
			//	FrameNo = 0,
			//	SlotNo = 1,
			//	TargetIp = "172.27.245.92",
			//	conType = ConnectType.ATP_DIRECT_LINK
			//};

			//LinkMgrActor.GetInstance().ConnectNetElement("172.27.245.92", config);
		}

		protected override void OnExit(ExitEventArgs e)
		{
			AtpInitial.Stop();
			MqInitial.Stop();
		}
	}
}
