using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;

namespace NetPlan
{
	internal sealed class NetDevBoard : NetDevBase
	{
		internal NetDevBoard(string strTargetIp, NPDictionary mapOriginData) : base(strTargetIp, mapOriginData)
		{
		}

		#region 虚函数区

		internal override bool DistributeToEnb(DevAttributeBase dev, bool bDlAntWcb = false)
		{
			if (dev.m_recordType == RecordDataType.WaitDel)
			{
				if (!DistributeRelateIrLinks(dev))
				{
					return false;
				}
			}

			return base.DistributeToEnb(dev, bDlAntWcb);
		}

		#endregion


		#region 静态接口

		public static BoardBaseInfo GetBoardBaseInfo(DevAttributeBase board)
		{
			var boardRs = board.GetNeedUpdateValue("netBoardRowStatus");
			if (null == boardRs)
			{
				Log.Error($"从板卡{board.m_strOidIndex}查询netBoardRowStatus属性值失败");
				return null;
			}

			if ("6" == boardRs && board.m_recordType != RecordDataType.NewAdd)
			{
				Log.Error($"板卡{board.m_strOidIndex}处于待删除状态，所有属性值无效");
				//return null;
			}

			var rackNo = board.GetNeedUpdateValue("netBoardRackNo");
			if (null == rackNo)
			{
				Log.Error($"从板卡{board.m_strOidIndex}查询netBoardRackNo属性值失败");
				return null;
			}

			var shelfNo = board.GetNeedUpdateValue("netBoardShelfNo");
			if (null == shelfNo)
			{
				Log.Error($"从板卡{board.m_strOidIndex}查询netBoardShelfNo属性值失败");
				return null;
			}

			var slotNo = board.GetNeedUpdateValue("netBoardSlotNo");
			if (null == slotNo)
			{
				Log.Error($"从板卡{board.m_strOidIndex}查询netBoardSlotNo属性值失败");
				return null;
			}

			var boardType = board.GetNeedUpdateValue("netBoardType");
			if (null == boardType)
			{
				Log.Error($"从板卡{board.m_strOidIndex}查询netBoardType属性值失败");
				return null;
			}

			var bbi = new BoardBaseInfo
			{
				strRackNo = rackNo,
				strShelfNo = shelfNo,
				strSlotNo = slotNo,
				strBoardCode = boardType
			};

			return bbi;
		}

		#endregion

		#region 私有函数区

		/// <summary>
		/// 删除板卡之前需要先删除和这个板卡相关的连接
		/// </summary>
		/// <param name="dev"></param>
		/// <returns></returns>
		private bool DistributeRelateIrLinks(DevAttributeBase dev)
		{
			var bbi = GetBoardBaseInfo(dev);
			if (null == bbi)
			{
				return false;
			}

			var partIdx = bbi.GetBoardIndex();
			var irLinks = GetDevs(EnumDevType.board_rru, partIdx);
			if (irLinks.Count == 0)
			{
				return true;
			}

			foreach (var ir in irLinks)
			{
				if (!DistributeToEnb(ir, "DelIROfpPortInfo", "6"))
				{
					Log.Error($"索引为{ir.m_strOidIndex}IR规划记录下发失败");
					return false;
				}

				m_mapOriginData[EnumDevType.board_rru].Remove((DevAttributeInfo)ir);
			}

			return true;
		}

		#endregion
	}
}
