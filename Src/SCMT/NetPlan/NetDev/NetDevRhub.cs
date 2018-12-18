using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan
{
	internal class NetDevRhub : NetDevBase
	{

		#region 重载函数区

		internal override bool DistributeToEnb(DevAttributeBase dev, bool bDlAntWcb = false)
		{
			if (dev.m_recordType == RecordDataType.WaitDel)
			{
				// 需要先删除与rhub相关的连接
				if (!DistributeRelateEthRecord(dev))
				{
					return false;
				}

				if (!DistributeRelateIrRecord(dev))
				{
					return false;
				}
			}

			return true;
		}

		#endregion


		#region 静态函数区

		/// <summary>
		/// 获取RHUB设备连接的板卡的插槽号。遍历4个光口
		/// todo 需要重构为返回map类型。可能存在rhub不同光口连接到不同板卡的情况
		/// </summary>
		internal static List<string> GetRhubLinkToBoardSlotNo(DevAttributeInfo rhub)
		{
			var bbuSlotList = new List<string>();
			for (var i = 1; i <= MagicNum.RRU_TO_BBU_PORT_CNT; i++)
			{
				var mibName = (i == 1) ? "netRHUBAccessSlotNo" : $"netRHUBOfp{i}SlotNo";
				var boardSlot = rhub.GetNeedUpdateValue(mibName);	// todo 可能存在错误，需要替换成GetOriginValue
				if (null != boardSlot && "-1" != boardSlot)
				{
					bbuSlotList.Add(boardSlot);
				}
			}

			return bbuSlotList.Distinct().ToList();
		}

		/// <summary>
		/// 从rhub设备属性中获取到board的光口连接
		/// </summary>
		/// <param name="rhubDev"></param>
		/// <returns>字典，key:rhub的第几个光口，value:bbu的光口号</returns>
		public static Dictionary<int, string> GetToBbuPortsFromRhub(DevAttributeBase rhubDev)
		{
			var retMap = new Dictionary<int, string>();
			for (var i = 1; i <= MagicNum.RRU_TO_BBU_PORT_CNT; i++)
			{
				var bbuPort = rhubDev.GetFieldOriginValue(GetOfpMibName(i));
				if (null != bbuPort && "-1" != bbuPort)
				{
					retMap.Add(i, bbuPort);
				}
			}

			return retMap;
		}

		private static string GetOfpMibName(int n)
		{
			return $"netRHUBOfp{n}AccessOfpPortNo";
		}

		#endregion

		#region 构造函数

		internal NetDevRhub(string strTargetIp, MAP_DEVTYPE_DEVATTRI mapOriginData) : base(strTargetIp, mapOriginData)
		{
		}

		#endregion

		#region 私有函数

		/// <summary>
		/// 下发关联的以太网口规划记录
		/// </summary>
		/// <param name="rhubDev"></param>
		/// <returns></returns>
		private bool DistributeRelateEthRecord(DevAttributeBase rhubDev)
		{
			var boardSlotList = GetRhubLinkToBoardSlotNo((DevAttributeInfo)rhubDev);
			if (boardSlotList.Count == 0)
			{
				Log.Error($"获取索引{rhubDev.m_strOidIndex}rhub连接的板卡插槽号失败");
				return false;
			}

			foreach (var slot in boardSlotList)
			{
				var partIdx = $".0.0.{slot}{rhubDev.m_strOidIndex}";

				var ethRecordList = GetDevs(EnumDevType.rhub_prru, partIdx);

				foreach (var item in ethRecordList)
				{
					var ethPort = LinkRhubPico.GetRhubToPicoPort(rhubDev);
					if (null == ethPort || "-1" == ethPort)
					{
						Log.Error("[NetPlan] get rhub device to pico eth port failed, result is -1.");
						continue;
					}

					var idx = $"{partIdx}.{ethPort}";
					var ethRecord = GetDev(EnumDevType.rhub_prru, idx);
					if (null == ethRecord)
					{
						Log.Error($"[NetPlan] get index = {idx} eth plan record failed");
						continue;
					}

					if (ethRecord.m_recordType != RecordDataType.WaitDel)
					{
						continue;
					}

					if (!DistributeToEnb(ethRecord, "DelEthPortInfo", "6"))
					{
						Log.Error($"[NetPlan] send cmd DelEthPortInfo to delete index = {ethRecord.m_strOidIndex} eth plan record failed");
						return false;
					}

					m_mapOriginData[EnumDevType.rhub_prru].Remove((DevAttributeInfo)ethRecord);
				}
			}

			return true;
		}

		/// <summary>
		/// 下发关联的IR口规划记录
		/// </summary>
		/// <param name="rhubDev"></param>
		/// <returns></returns>
		private bool DistributeRelateIrRecord(DevAttributeBase rhubDev)
		{
			var boardSlotList = GetRhubLinkToBoardSlotNo((DevAttributeInfo)rhubDev);
			if (boardSlotList.Count == 0)
			{
				Log.Error($"获取索引{rhubDev.m_strOidIndex}rhub连接的板卡插槽号失败");
				return false;
			}

			foreach (var slot in boardSlotList)
			{
				var partIdx = $".0.0.{slot}";
				var mapToBbuPort = GetToBbuPortsFromRhub(rhubDev);
				foreach (var item in mapToBbuPort)
				{
					var irRecordIdx = $"{partIdx}.{item.Value}";
					var irRecord = GetDev(EnumDevType.board_rhub, irRecordIdx);
					if (null == irRecord)
					{
						Log.Error($"[NetPlan] get ir plan record by index {irRecordIdx} failed, result is null");
						continue;
					}

					if (irRecord.m_recordType != RecordDataType.WaitDel)
					{
						continue;
					}

					if (!DistributeToEnb(irRecord, "DelIROfpPortInfo", "6"))
					{
						Log.Error($"[NetPlan] send cmd DelIROfpPortInfo to delete index = {irRecord.m_strOidIndex} ir plan record failed");
						return false;
					}

					m_mapOriginData[EnumDevType.board_rhub].Remove((DevAttributeInfo)irRecord);
				}
			}

				return true;
		}

		#endregion
	}
}
