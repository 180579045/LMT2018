using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan
{
	internal class NetDevLc : NetDevBase
	{
		#region 构造函数

		internal NetDevLc(string strTargetIp, MAP_DEVTYPE_DEVATTRI mapOriginData) : base(strTargetIp, mapOriginData)
		{

		}

		#endregion

		#region 虚函数重载区

		internal override bool DistributeToEnb(DevAttributeBase dev, bool bDlAntWcb = false)
		{
			// 下发之前需要打开布配开关
			if (!NPCellOperator.SendNetPlanSwitchToEnb(true, dev.m_strOidIndex, m_strTargetIp))
			{
				Log.Error($"打开本地小区{dev.m_strOidIndex}的布配开关失败");
				return false;
			}

			// 下发本地小区信息
			var bSucceedLc = base.DistributeToEnb(dev);

			// 下发完成后关闭布配开关。无论是否下发成功都要关闭开关
			var bSucceedSwitch = NPCellOperator.SendNetPlanSwitchToEnb(false, dev.m_strOidIndex, m_strTargetIp);

			return bSucceedLc && bSucceedSwitch;
		}

		#endregion

		#region 非虚函数区



		#endregion

	}
}
