using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan
{
	internal static class NetDevFactory
	{
		internal static NetDevBase GetDevHandler(string strTargetIp, EnumDevType devType, MAP_DEVTYPE_DEVATTRI mapOriginData)
		{
			NetDevBase handler = null;
			switch (devType)
			{
				case EnumDevType.rru:
					handler = new NetDevRru(strTargetIp, mapOriginData);
					break;
				case EnumDevType.ant:
					handler = new NetDevAnt(strTargetIp, mapOriginData);
					break;
				case EnumDevType.nrNetLc:
					handler = new NetDevLc(strTargetIp, mapOriginData);
					break;
				default:
					handler = new NetDevBase(strTargetIp, mapOriginData);
					break;
			}

			return handler;
		}
	}
}
