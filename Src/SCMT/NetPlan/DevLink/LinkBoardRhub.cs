using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan.DevLink
{
	public sealed class LinkBoardRhub : NetPlanLinkBase
	{
		#region 虚函数重载

		public override bool DelLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			throw new NotImplementedException();
		}

		public override bool AddLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo))
			{
				return false;
			}

			var rhubClone = m_rhubDev.DeepClone();

			var bbi = GetBoardBaseInfo(m_boardDev);

			if (!SetBoardBaseInfoInRhub(bbi, rhubClone))
			{
				return false;
			}

			if (!SetOfpLinkInfoInRhub(rhubClone))
			{
				return false;
			}

			if (RecordDataType.NewAdd != rhubClone.m_recordType)
			{
				rhubClone.m_recordType = RecordDataType.Modified;
			}

			if (mapMibInfo.ContainsKey(EnumDevType.rru))
			{
				// board与rhub连通的瞬间，先前rhub与pico的连接上pico设备属性也要设置
				List<DevAttributeInfo> oldPicoList;
				List<DevAttributeInfo> newPicoList;
				if (!UpdatePicoInfo(mapMibInfo[EnumDevType.rru], out oldPicoList, out newPicoList))
				{
					Log.Error("更新与rhub相连的pico信息失败");
					return false;
				}

				foreach (var pico in oldPicoList)
				{
					mapMibInfo[EnumDevType.rru].Remove(pico);
				}

				foreach (var pico in newPicoList)
				{
					mapMibInfo[EnumDevType.rru].Add(pico);
				}
			}

			// todo 当前尚未支持rhub级联的情况，rhub级联后需要计算后面rhub的接入级数、连接的板卡信息等

			// 添加一条记录
			var irRecord = new DevAttributeInfo(EnumDevType.board_rru, m_strIrRecodeIndex);     // 记录仍然是board_rru类型

			mapMibInfo[EnumDevType.rhub].Remove(m_rhubDev);
			mapMibInfo[EnumDevType.rhub].Add(rhubClone);

			AddDevToMap(mapMibInfo, EnumDevType.board_rru, irRecord);

			return true;
		}


		public override bool CheckLinkIsValid(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			var boardIndex = wholeLink.GetDevIndex(EnumDevType.board);
			if (null == boardIndex)
			{
				Log.Error("获取board索引失败");
				return false;
			}

			m_nBoardPort = wholeLink.GetDevIrPort(EnumDevType.board, EnumPortType.bbu_to_rhub);
			if (-1 == m_nBoardPort)
			{
				Log.Error("获取板卡连接光口失败");
				return false;
			}

			m_boardDev = GetDevAttributeInfo(boardIndex, EnumDevType.board);
			if (null == m_boardDev)
			{
				Log.Error($"根据板卡属性{boardIndex}未找到对应的设备");
				return false;
			}

			var rhubIndex = wholeLink.GetDevIndex(EnumDevType.rhub);
			if (null == rhubIndex)
			{
				Log.Error("获取rhub设备索引失败");
				return false;
			}

			m_nRhubPort = wholeLink.GetDevIrPort(EnumDevType.rhub, EnumPortType.rhub_to_bbu);
			if (-1 == m_nRhubPort)
			{
				Log.Error("获取rhub设备的光口号失败");
				return false;
			}

			m_rhubDev = GetDevAttributeInfo(rhubIndex, EnumDevType.rhub);
			if (null == m_rhubDev)
			{
				Log.Error($"根据rhub设备属性{rhubIndex}未找到对应的设备信息");
				return false;
			}

			// 确定netIROptPlanEntry中是否已经存在对应的记录
			m_strIrRecodeIndex = $"{boardIndex}.{m_nBoardPort}";
			var irRecord = GetDevAttributeInfo(m_strIrRecodeIndex, EnumDevType.board_rru);
			if (null != irRecord)
			{
				Log.Error($"根据板卡索引和光口号组合{m_strIrRecodeIndex}找到已经存在的记录，板卡的每个光口只能连接一个设备");
				return false;
			}

			var slotNo = MibInfoMgr.GetNeedUpdateValue(m_boardDev, "netBoardSlotNo");
			if (null == slotNo)
			{
				Log.Error("获取板卡插槽号失败");
				return false;
			}

			var boardType = MibInfoMgr.GetNeedUpdateValue(m_boardDev, "netBoardType");
			if (null == boardType)
			{
				Log.Error("获取板卡类型失败");
				return false;
			}

			m_strOfpMibName = $"netRHUBOfp{m_nRhubPort}AccessOfpPortNo";                // 板卡的光口号
			if (!m_rhubDev.IsExistField(m_strOfpMibName))
			{
				Log.Error($"当前MIB版本中不包含{m_strOfpMibName}字段，请确认MIB是否正确");
				return false;
			}

			m_strOfpLinePosMibName = $"netRHUBOfp{m_nRhubPort}AccessLinePosition";      // 接入级数
			if (!m_rhubDev.IsExistField(m_strOfpLinePosMibName))
			{
				Log.Error($"当前MIB版本中不包含{m_strOfpLinePosMibName}字段，请确认MIB是否正确");
				return false;
			}

			m_strSlotMibName = m_nRhubPort == 1 ? "netRHUBAccessSlotNo" : $"netRHUBOfp{m_nRhubPort}SlotNo";
			if (!m_rhubDev.IsExistField(m_strSlotMibName))
			{
				Log.Error($"当前MIB版本中不包含{m_strSlotMibName}字段，请确认MIB是否正确");
				return false;
			}

			return true;
		}

		private bool SetBoardBaseInfoInRhub(BoardBaseInfo bbi, DevAttributeInfo dev)
		{
			var rowstatus = MibInfoMgr.GetNeedUpdateValue(dev, "netRHUBRowStatus");
			if (null == rowstatus)
			{
				Log.Error($"从RHUB{dev.m_strOidIndex}查询netRHUBRowStatus属性值失败");
				return false;
			}

			if ("6" == rowstatus && dev.m_recordType != RecordDataType.NewAdd)
			{
				Log.Error($"RHUB{dev.m_strOidIndex}处于待删除状态，所有属性值无效");
				return false;
			}

			if (!MibInfoMgr.SetDevAttributeValue(dev, "netRHUBAccessRackNo", "0"))
			{
				return false;
			}
			if (!MibInfoMgr.SetDevAttributeValue(dev, "netRHUBAccessShelfNo", "0"))
			{
				return false;
			}
			if (!MibInfoMgr.SetDevAttributeValue(dev, m_strSlotMibName, bbi.strSlotNo))
			{
				return false;
			}
			if (!MibInfoMgr.SetDevAttributeValue(dev, "netRHUBAccessBoardType", bbi.strBoardCode))
			{
				return false;
			}

			return true;
		}

		private bool SetOfpLinkInfoInRhub(DevAttributeInfo rhub)
		{
			var wmo = rhub.GetFieldOriginValue("netRHUBOfpWorkMode", false);
			var wml = rhub.GetFieldLatestValue("netRHUBOfpWorkMode", false);

			var workMode = MibInfoMgr.GetNeedUpdateValue(wmo, wml);
			if (null == workMode)
			{
				Log.Error($"从rhub{rhub.m_strOidIndex}查询光口工作模式失败");
				return false;
			}

			if (!MibInfoMgr.SetDevAttributeValue(rhub, m_strOfpMibName, m_nBoardPort.ToString()))
			{
				return false;
			}

			if (!MibInfoMgr.SetDevAttributeValue(rhub, m_strOfpLinePosMibName, "1"))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// 更新和rhub相连的pico信息
		/// </summary>
		/// <param name="rruList"></param>
		/// <param name="picoList">出参，保存原来map中的pico设备</param>
		/// <param name="newPico">出参，保存clone的pico设备</param>
		/// <returns></returns>
		private bool UpdatePicoInfo(List<DevAttributeInfo> rruList, out List<DevAttributeInfo> picoList, out List<DevAttributeInfo> newPico)
		{
			newPico = new List<DevAttributeInfo>();
			picoList = new List<DevAttributeInfo>();

			if (null == rruList)
			{
				return false;
			}

			var rhubNo = m_rhubDev.m_strOidIndex.Trim('.');

			var bbi = GetBoardBaseInfo(m_boardDev);

			foreach (var rru in rruList)
			{
				var rruTypeIndex = MibInfoMgr.GetNeedUpdateValue(rru, "netRRUTypeIndex");
				if (null == rruTypeIndex)
				{
					return false;
				}

				if (!NPERruHelper.GetInstance().IsPicoDevice(int.Parse(rruTypeIndex)))
				{
					continue;
				}

				var rhubNoInPico = MibInfoMgr.GetNeedUpdateValue(rru, "netRRUHubNo");
				if (null == rhubNoInPico)
				{
					return false;
				}

				// 判断Pico连接的rhub是否是当前正在建立连接的rhub
				if (rhubNoInPico != rhubNo)
				{
					continue;
				}

				var picoClone = rru.DeepClone();
				if (!SetBoardBaseInfoInRru(bbi, picoClone, 1))	// todo pico的端口硬编码
				{
					return false;
				}

				newPico.Add(picoClone);
				picoList.Add(rru);
			}

			return true;
		}

		#endregion


		#region 私有数据区

		private DevAttributeInfo m_rhubDev;
		private DevAttributeInfo m_boardDev;

		private string m_strOfpMibName;
		private string m_strOfpLinePosMibName;
		private string m_strSlotMibName;
		private string m_strIrRecodeIndex;

		private int m_nBoardPort;
		private int m_nRhubPort;

		#endregion
	}
}
