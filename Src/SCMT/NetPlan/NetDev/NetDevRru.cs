using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;

namespace NetPlan
{
	internal class NetDevRru : NetDevBase
	{
		/// <summary>
		/// 从pico中查询连接的rhub的编号和端口号
		/// </summary>
		/// <param name="picoDev"></param>
		/// <returns>key:pico连接rhub的端口号，value:rhub到pico的连接点信息</returns>
		public static Dictionary<int, LinkEndpoint> GetLinkedRhubInfoFromPico(DevAttributeInfo picoDev)
		{
			var rhubNoMib = "netRRUHubNo";
			var rhubNo = MibInfoMgr.GetNeedUpdateValue(picoDev, rhubNoMib);
			if (null == rhubNo || "-1" == rhubNo)
			{
				Log.Debug($"索引为{picoDev.m_strOidIndex}pico设备尚未连接到rhub，请确认是否存在错误");
				return null;
			}

			var epMap = new Dictionary<int, LinkEndpoint>();

			for (var i = 1; i <= 2; i++)        // todo pico设备按两个端口算，如果MIB有修改，需要进行处理
			{
				var rhubEthMib = $"netRRUOfp{i}AccessEthernetPort";
				var rhubEthNo = MibInfoMgr.GetNeedUpdateValue(picoDev, rhubEthMib);
				if (null == rhubEthNo || "-1" == rhubEthNo)
				{
					continue;
				}

				int rhubPort;
				if (!int.TryParse(rhubEthNo, out rhubPort))
				{
					Log.Error($"索引为{picoDev.m_strOidIndex}pico设备在端口{i}连接的rhub端口号配置错误，值：{rhubEthNo}");
					continue;
				}

				var ep = new LinkEndpoint
				{
					devType = EnumDevType.rhub,
					strDevIndex = $".{rhubNo}",
					portType = EnumPortType.rhub_to_pico,
					nPortNo = rhubPort
				};

				epMap.Add(i, ep);
			}

			return epMap;
		}
	}
}
