using System.Collections.Generic;
using LogManager;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan.DevLink
{
	public sealed class LinkBoardRhub : NetPlanLinkBase
	{
		public LinkBoardRhub() : base()
		{
			m_irRecordType = EnumDevType.board_rru;
		}

		#region 虚函数重载

		public override bool DelLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
				return false;
			}

			var rhubClone = m_rhubDev.DeepClone();
			if (!ResetBoardInfoInRhub(rhubClone, m_nRhubPort))
			{
				MibInfoMgr.ErrorTip("删除连接失败，原因：重置rhub设备中板卡信息失败");
				return false;
			}

			if (RecordDataType.NewAdd != rhubClone.m_recordType)
			{
				rhubClone.SetDevRecordType(RecordDataType.Modified);
			}

			// 处理与rhub连接的pico的信息
			if (mapMibInfo.ContainsKey(EnumDevType.rru))
			{
				// board与rhub连通的瞬间，先前rhub与pico的连接上pico设备属性也要设置
				List<DevAttributeInfo> oldPicoList;
				List<DevAttributeInfo> newPicoList;

				var bbi = new BoardBaseInfo();

				if (!UpdatePicoInfo(bbi, mapMibInfo[EnumDevType.rru], out oldPicoList, out newPicoList))
				{
					MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
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

			// 删除board与rhub之间的连接
			var irRecord = GetDevAttributeInfo(m_strIrRecodeIndex, EnumDevType.board_rru);
			MibInfoMgr.DelDevFromMap(mapMibInfo, EnumDevType.board_rru, irRecord);

			mapMibInfo[EnumDevType.rhub].Remove(m_rhubDev);
			mapMibInfo[EnumDevType.rhub].Add(rhubClone);

			// todo rhub级联的情况暂不支持

			Log.Debug($"删除连接成功，连接详细信息：{wholeLink}");
			Log.Debug($"删除类型为：{m_irRecordType.ToString()}，索引为：{irRecord.m_strOidIndex}的记录成功");

			MibInfoMgr.InfoTip("删除连接成功");

			return true;
		}

		public override bool AddLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordNotExistInAdd))
			{
				MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
				return false;
			}

			var rhubClone = m_rhubDev.DeepClone();

			var bbi = GetBoardBaseInfo(m_boardDev);

			if (!SetBoardBaseInfoInRhub(bbi, rhubClone))
			{
				MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
				return false;
			}

			if (!SetOfpLinkInfoInRhub(rhubClone, m_nBoardPort))
			{
				MibInfoMgr.ErrorTip($"添加连接失败，原因：{m_strLatestError}");
				return false;
			}

			if (RecordDataType.NewAdd != rhubClone.m_recordType)
			{
				rhubClone.SetDevRecordType(RecordDataType.Modified);
			}

			if (mapMibInfo.ContainsKey(EnumDevType.rru))
			{
				// board与rhub连通的瞬间，先前rhub与pico的连接上pico设备属性也要设置
				List<DevAttributeInfo> oldPicoList;
				List<DevAttributeInfo> newPicoList;
				if (!UpdatePicoInfo(bbi, mapMibInfo[EnumDevType.rru], out oldPicoList, out newPicoList))
				{
					MibInfoMgr.ErrorTip($"删除连接失败，原因：{m_strLatestError}");
					return false;
				}

				foreach (var pico in oldPicoList)
				{
					mapMibInfo[EnumDevType.rru].Remove(pico);
				}

				var linkrp = new LinkRhubPico();
				foreach (var pico in newPicoList)
				{
					mapMibInfo[EnumDevType.rru].Add(pico);

					// 板卡和rhub连接成功后，才能增加netEthPlanEntry表记录，因为需要bbu的架框槽信息
					linkrp.AddEthPlanRecord(rhubClone, pico, mapMibInfo);
				}
			}

			// todo 当前尚未支持rhub级联的情况，rhub级联后需要计算后面rhub的接入级数、连接的板卡信息等

			// 添加一条记录
			var irRecord = new DevAttributeInfo(EnumDevType.board_rru, m_strIrRecodeIndex);     // 记录仍然是board_rru类型

			mapMibInfo[EnumDevType.rhub].Remove(m_rhubDev);
			mapMibInfo[EnumDevType.rhub].Add(rhubClone);

			AddDevToMap(mapMibInfo, EnumDevType.board_rru, irRecord);

			Log.Debug($"添加连接成功，连接详细信息：{wholeLink}");
			Log.Debug($"添加类型为：{m_irRecordType.ToString()}，索引为：{irRecord.m_strOidIndex}的记录成功");

			MibInfoMgr.InfoTip("添加连接成功");

			return true;
		}

		public override bool CheckLinkIsValid(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo, IsRecordExist checkExist)
		{
			var boardIndex = wholeLink.GetDevIndex(EnumDevType.board);
			if (null == boardIndex)
			{
				m_strLatestError = "获取板卡索引失败";
				return false;
			}

			m_nBoardPort = wholeLink.GetDevIrPort(EnumDevType.board, EnumPortType.bbu_to_rhub);
			if (-1 == m_nBoardPort)
			{
				m_strLatestError = "获取板卡连接rhub设备光口号失败";
				return false;
			}

			m_boardDev = GetDevAttributeInfo(boardIndex, EnumDevType.board);
			if (null == m_boardDev)
			{
				m_strLatestError = $"根据索引{boardIndex}未找到板卡信息";
				return false;
			}

			var rhubIndex = wholeLink.GetDevIndex(EnumDevType.rhub);
			if (null == rhubIndex)
			{
				m_strLatestError = "获取rhub设备索引失败";
				return false;
			}

			m_nRhubPort = wholeLink.GetDevIrPort(EnumDevType.rhub, EnumPortType.rhub_to_bbu);
			if (-1 == m_nRhubPort)
			{
				m_strLatestError = "获取rhub设备连接板卡光口号失败";
				return false;
			}

			m_rhubDev = GetDevAttributeInfo(rhubIndex, EnumDevType.rhub);
			if (null == m_rhubDev)
			{
				m_strLatestError = $"根据索引{rhubIndex}未找到rhub设备信息";
				return false;
			}

			// 确定netIROptPlanEntry中是否已经存在对应的记录
			m_strIrRecodeIndex = $"{boardIndex}.{m_nBoardPort}";
			if (!checkExist.Invoke(m_strIrRecodeIndex, EnumDevType.board_rru))
			{
				var tmp = checkExist == RecordExistInDel ? "不" : "已";
				m_strLatestError = $"索引为{m_strIrRecodeIndex}的光口规划表记录{tmp}存在";
				return false;
			}

			var slotNo = m_boardDev.GetNeedUpdateValue("netBoardSlotNo");
			if (null == slotNo)
			{
				m_strLatestError = $"获取索引为{m_boardDev.m_strOidIndex}板卡设备netBoardSlotNo字段值失败";
				return false;
			}

			var boardType = m_boardDev.GetNeedUpdateValue("netBoardType");
			if (null == boardType)
			{
				m_strLatestError = $"获取索引为{m_boardDev.m_strOidIndex}板卡设备netBoardType字段值失败";
				return false;
			}

			m_strOfpMibName = $"netRHUBOfp{m_nRhubPort}AccessOfpPortNo";                // 板卡的光口号
			if (!m_rhubDev.IsExistField(m_strOfpMibName))
			{
				m_strLatestError = $"获取索引为{m_boardDev.m_strOidIndex}板卡设备{m_strOfpMibName}字段值失败，可能MIB版本差异导致错误";
				return false;
			}

			m_strOfpLinePosMibName = $"netRHUBOfp{m_nRhubPort}AccessLinePosition";      // 接入级数
			if (!m_rhubDev.IsExistField(m_strOfpLinePosMibName))
			{
				m_strLatestError = $"获取索引为{m_boardDev.m_strOidIndex}板卡设备{m_strOfpLinePosMibName}字段值失败，可能MIB版本差异导致错误";
				return false;
			}

			m_strSlotMibName = m_nRhubPort == 1 ? "netRHUBAccessSlotNo" : $"netRHUBOfp{m_nRhubPort}SlotNo";
			if (!m_rhubDev.IsExistField(m_strSlotMibName))
			{
				m_strLatestError = $"获取索引为{m_boardDev.m_strOidIndex}板卡设备{m_strSlotMibName}字段值失败，可能MIB版本差异导致错误";
				return false;
			}

			return true;
		}

		public override DevAttributeInfo GetRecord(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				return null;
			}

			return GetDevAttributeInfo(m_strIrRecodeIndex, m_irRecordType);
		}

		private bool ResetBoardInfoInRhub(DevAttributeInfo rhub, int nRhubPort)
		{
			var bbi = new BoardBaseInfo();
			return SetBoardBaseInfoInRhub(bbi, rhub) && SetOfpLinkInfoInRhub(rhub, -1);
		}

		/// <summary>
		/// 设置rhub设备和board相关的属性
		/// </summary>
		/// <param name="bbi"></param>
		/// <param name="dev"></param>
		/// <returns></returns>
		private bool SetBoardBaseInfoInRhub(BoardBaseInfo bbi, DevAttributeInfo dev)
		{
			var rowstatus = dev.GetNeedUpdateValue("netRHUBRowStatus");
			if (null == rowstatus)
			{
				m_strLatestError = $"获取索引为{dev.m_strOidIndex}rhub设备netRHUBRowStatus字段值失败";
				return false;
			}

			if ("6" == rowstatus && dev.m_recordType != RecordDataType.NewAdd)
			{
				Log.Error($"RHUB{dev.m_strOidIndex}行状态为：待删除");
				//return false;
			}

			if (!dev.SetDevAttributeValue("netRHUBAccessRackNo", bbi.strRackNo))
			{
				m_strLatestError = $"设置索引为{dev.m_strOidIndex}的rhub设备netRHUBAccessRackNo字段值{bbi.strRackNo}失败";
				return false;
			}
			if (!dev.SetDevAttributeValue("netRHUBAccessShelfNo", bbi.strShelfNo))
			{
				m_strLatestError = $"设置索引为{dev.m_strOidIndex}的rhub设备netRHUBAccessShelfNo字段值{bbi.strShelfNo}失败";
				return false;
			}
			if (!dev.SetDevAttributeValue(m_strSlotMibName, bbi.strSlotNo))
			{
				m_strLatestError = $"设置索引为{dev.m_strOidIndex}的rhub设备{m_strSlotMibName}字段值{bbi.strSlotNo}失败";
				return false;
			}

			if (!dev.SetDevAttributeValue("netRHUBAccessBoardType", bbi.strBoardCode))
			{
				m_strLatestError = $"设置索引为{dev.m_strOidIndex}的rhub设备netRHUBAccessBoardType字段值{bbi.strBoardCode}失败";
				return false;
			}

			return true;
		}

		private bool SetOfpLinkInfoInRhub(DevAttributeInfo rhub, int nBoardPort)
		{
			var workMode = rhub.GetNeedUpdateValue("netRHUBOfpWorkMode", false);
			if (null == workMode)
			{
				m_strLatestError = $"获取索引为{rhub.m_strOidIndex}rhub设备netRHUBOfpWorkMode字段值失败";
				return false;
			}

			if (!rhub.SetDevAttributeValue(m_strOfpMibName, nBoardPort.ToString()))
			{
				m_strLatestError = $"设置索引为{rhub.m_strOidIndex}的rhub设备{m_strOfpMibName}字段值{nBoardPort}失败";
				return false;
			}

			var lp = -1 == nBoardPort ? "-1" : "1";

			if (!rhub.SetDevAttributeValue(m_strOfpLinePosMibName, lp))
			{
				m_strLatestError = $"设置索引为{rhub.m_strOidIndex}的rhub设备{m_strOfpLinePosMibName}字段值{lp}失败";
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
		private bool UpdatePicoInfo(BoardBaseInfo bbi, List<DevAttributeInfo> rruList, out List<DevAttributeInfo> picoList, out List<DevAttributeInfo> newPico)
		{
			newPico = new List<DevAttributeInfo>();
			picoList = new List<DevAttributeInfo>();

			if (null == rruList)
			{
				return false;
			}

			var rhubNo = m_rhubDev.m_strOidIndex.Trim('.');

			foreach (var rru in rruList)
			{
				var rruTypeIndex = rru.GetNeedUpdateValue("netRRUTypeIndex");
				if (null == rruTypeIndex)
				{
					m_strLatestError = $"获取索引为{rru.m_strOidIndex}的rru设备netRRUTypeIndex字段值失败";
					return false;
				}

				if (!NPERruHelper.GetInstance().IsPicoDevice(int.Parse(rruTypeIndex)))
				{
					continue;
				}

				var rhubNoInPico = rru.GetNeedUpdateValue("netRRUHubNo");
				if (null == rhubNoInPico)
				{
					m_strLatestError = $"获取索引为{rru.m_strOidIndex}的rru设备netRRUHubNo字段值失败";
					return false;
				}

				// 判断Pico连接的rhub是否是当前正在建立连接的rhub
				if (rhubNoInPico != rhubNo)
				{
					continue;
				}

				var picoClone = rru.DeepClone();
				if (!SetBoardBaseInfoInRru(bbi, picoClone, 1))  // todo pico的端口硬编码
				{
					return false;
				}

				if (RecordDataType.NewAdd != rru.m_recordType)
				{
					picoClone.SetDevRecordType(RecordDataType.Modified);
				}

				newPico.Add(picoClone);
				picoList.Add(rru);
			}

			return true;
		}

		#endregion 虚函数重载

		#region 私有数据区

		private DevAttributeInfo m_rhubDev;
		private DevAttributeInfo m_boardDev;

		private string m_strOfpMibName;
		private string m_strOfpLinePosMibName;
		private string m_strSlotMibName;
		private string m_strIrRecodeIndex;

		private int m_nBoardPort;
		private int m_nRhubPort;

		private EnumDevType m_irRecordType;

		#endregion 私有数据区
	}
}