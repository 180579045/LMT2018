using System.Collections.Generic;
using CommonUtility;
using LogManager;
using MsgQueue;
using SCMTOperationCore.Control;
using SCMTOperationCore.Message.SNMP;

// 与基站的连接变化等相关的处理
// 单例类

namespace SCMTOperationCore.Message.MsgDispatcher
{
	internal class NetAddr
	{
		public string TargetIp;
	}

	public class ConnectWorker : Singleton<ConnectWorker>
	{
		#region 构造、析构

		private ConnectWorker()
		{
			// 订阅消息
			SubscribeHelper.AddSubscribe(TopicHelper.EnbConnectedMsg, OnConnect);
		}

		#endregion

		#region 公有接口

		

		#endregion

		#region 订阅消息处理函数

		// 消息处理事件
		private void OnConnect(SubscribeMsg msg)
		{
			var netAddr = JsonHelper.SerializeJsonToObject<NetAddr>(msg.Data);
			var ip = netAddr.TargetIp;


			//Step 1.设置MIB前缀
			//SetMibPrefix(ip);

			//Step 2.查询设备的MIB版本号
			var mibVersionNo = QueryMibVersionNo(ip);
			if (null == mibVersionNo)
			{
				ShowLogHelper.Show("查询MIB版本号失败!", ip);
			}
			else
			{
				ShowLogHelper.Show($"查询MIB版本号成功:{mibVersionNo}", ip);
			}

			//Step 3.设置FTPServer信息到网元
			if (FtpServerHelper.SetFtpServerInfo(ip))
			{
				ShowLogHelper.Show("设置FTP服务器信息到网元成功!", ip);
			}
			else
			{
				ShowLogHelper.Show("设置FTP服务器信息到网元失败!", ip);
			}

			//Step 4.匹配MIB版本
			//MibSyncHelper.MatchMib(ip, mibVersionNo);

			//Step 5.加载Mib到内存中
			//LoadMibInfotoMemory(ip);

			//Step 6.发起数据一致性文件上传
			//UploadDataConsistencyFile(ip);

			//Step 7.发起告警日志文件上传
			FileTransHelper.UploadAlarmLogFile(ip);

			//Setp 8.接入成功后处理
			HandleConnected(ip);
		}

		#endregion

		#region 私有函数

		// 下发SNMP命令查询MIB版本号，查询失败返回null
		private string QueryMibVersionNo(string targetIp)
		{
			var oidList = new List<string>() { "1.9.1.2.0", "1.9.1.1.0" };
			var vbList = SnmpToDatabase.ConvertOidToVb(oidList, null);

			var pdu = new CDTLmtbPdu() { m_bIsNeedPrint = false };
			pdu.AddVb(vbList);
			pdu.SetSyncFlag(true);

			long requestId;
			bool bSucceed = false;
			LmtbSnmpEx lmtbSnmpEx = DTLinkPathMgr.GetSnmpInstance(targetIp);
			for (int i = 0; i < 3; i++)
			{
				var rs = lmtbSnmpEx.SnmpGetSync(pdu, out requestId, targetIp, 3000);
				if (rs == 0)
				{
					bSucceed = true;
					break;
				}
			}

			if (!bSucceed)
			{
				Log.Error($"查询基站的MIB版本失败");
				return null;
			}

			var mibVersionOid = SnmpToDatabase.GetMibPrefix() + "1.9.1.2.0";
			string mibVersion;
			if (!pdu.GetValueByOID(mibVersionOid, out mibVersion))
			{
				Log.Error($"获取MIB版本号失败");
				return null;
			}

			var mibNodeType = SnmpToDatabase.GetMibPrefix() + "1.9.1.1.0";
			string nodeType;
			if (!pdu.GetValueByOID(mibNodeType, out nodeType))
			{
				Log.Error($"获取设备类型失败");
				return null;
			}

			NodeBControl.GetInstance().SetNetElementType(targetIp, nodeType);

			mibVersion.Trim('\0').Replace('.', '_');

			return mibVersion;
		}

		private void HandleConnected(string ip)
		{
			
		}
		#endregion
	}
}
