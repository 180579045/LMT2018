using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPlan
{
	internal class NetDevRhub
	{
		/// <summary>
		/// 获取RHUB设备连接的板卡的插槽号。遍历4个光口
		/// </summary>
		public static string GetRhubLinkToBoardSlotNo(DevAttributeInfo rhub)
		{
			for (var i = 1; i < 5; i++)
			{
				var mibName = (i == 1) ? "netRHUBAccessSlotNo" : $"netRHUBOfp{i}SlotNo";
				var boardSlot = rhub.GetNeedUpdateValue(mibName);
				if (null != boardSlot && "-1" != boardSlot)
				{
					return boardSlot;
				}
			}

			return "-1";
		}
	}
}
