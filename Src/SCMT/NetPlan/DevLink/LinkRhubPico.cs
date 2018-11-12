using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LogManager;

namespace NetPlan.DevLink
{
	public sealed class LinkRhubPico : NetPlanLinkBase
	{
		#region 虚函数区

		public override bool AddLink(WholeLink wholeLink, ref Dictionary<EnumDevType, List<DevAttributeInfo>> mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordNotExistInAdd))
			{
				return false;
			}

			var picoClone = m_picoDev.DeepClone();

			if (RecordDataType.NewAdd != picoClone.m_recordType)
			{
				picoClone.m_recordType = RecordDataType.Modified;
			}

			// 判断rhub是否已经连接到板卡，如果已经连接到板卡，就设置pico中板卡的属性信息
			if (HasConnectedToBoard(m_rhubDev))
			{
				var bbi = GetBoardInfoFromRhub(RecordNotExistInAdd);
				if (null == bbi)
				{
					return false;
				}

				if (!SetBoardBaseInfoInRru(bbi, picoClone, m_nPicoPort))
				{
					return false;
				}
			}

			// 设置pico中连接rhub的信息
			var hubNo = int.Parse(m_rhubDev.m_strOidIndex.Trim('.'));
			if (!SetRhubInfoInPico(picoClone, m_nRhubEthPort, hubNo))
			{
				return false;
			}

			var newRecord = new DevAttributeInfo(EnumDevType.rhub_prru, m_strEthRecordIndex);

			AddDevToMap(mapMibInfo, EnumDevType.rhub_prru, newRecord);

			mapMibInfo[EnumDevType.rru].Remove(m_picoDev);
			mapMibInfo[EnumDevType.rru].Add(picoClone);

			return true;
		}

		public override bool DelLink(WholeLink wholeLink, ref Dictionary<EnumDevType, List<DevAttributeInfo>> mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				return false;
			}

			var picoClone = m_picoDev.DeepClone();
			var bbi = new BoardBaseInfo();
			if (!SetBoardBaseInfoInRru(bbi, picoClone, m_nPicoPort))
			{
				return false;
			}

			if (!SetRhubInfoInPico(picoClone, -1, -1))
			{
				return false;
			}

			if (HasConnectedToBoard(m_rhubDev))
			{
				bbi = GetBoardInfoFromRhub(RecordNotExistInAdd);
				if (null == bbi)
				{
					return false;
				}

				var record = GetDevAttributeInfo(m_strEthRecordIndex, EnumDevType.rhub_prru);
				MibInfoMgr.DelDevFromMap(mapMibInfo, EnumDevType.rhub_prru, record);
			}

			if (RecordDataType.NewAdd != picoClone.m_recordType)
			{
				picoClone.m_recordType = RecordDataType.Modified;
			}

			mapMibInfo[EnumDevType.rru].Remove(m_picoDev);
			mapMibInfo[EnumDevType.rru].Add(picoClone);

			return true;
		}

		public override bool CheckLinkIsValid(WholeLink wholeLink, Dictionary<EnumDevType, List<DevAttributeInfo>> mapMibInfo, IsRecordExist checkExist)
		{
			var rhubIndex = wholeLink.GetDevIndex(EnumDevType.rhub);
			if (null == rhubIndex)
			{
				Log.Error("获取rhub设备的索引失败");
				return false;
			}

			m_nRhubEthPort = wholeLink.GetDevIrPort(EnumDevType.rhub, EnumPortType.rhub_to_pico);
			if (-1 == m_nRhubEthPort)
			{
				Log.Error("获取rhub设备连接pico的端口失败");
				return false;
			}

			m_rhubDev = GetDevAttributeInfo(rhubIndex, EnumDevType.rhub);
			if (null == m_rhubDev)
			{
				Log.Error($"根据rhub设备索引{rhubIndex}获取设备属性信息失败");
				return false;
			}

			var picoIndex = wholeLink.GetDevIndex(EnumDevType.rru);
			if (null == picoIndex)
			{
				Log.Error("获取pico设备的索引失败");
				return false;
			}

			m_nPicoPort = wholeLink.GetDevIrPort(EnumDevType.rru, EnumPortType.pico_to_rhub);
			if (-1 == m_nPicoPort)
			{
				Log.Error("获取pico设备连接rhub的端口失败");
				return false;
			}

			m_picoDev = GetDevAttributeInfo(picoIndex, EnumDevType.rru);
			if (null == m_picoDev)
			{
				Log.Error($"根据pico索引{picoIndex}获取设备属性信息失败");
				return false;
			}

			return true;
		}


		private BoardBaseInfo GetBoardInfoFromRhub(IsRecordExist checkExist)
		{
			var boardSlot = MibInfoMgr.GetNeedUpdateValue(m_rhubDev, "netRHUBAccessSlotNo");
			if (null == boardSlot)
			{
				Log.Error("从hub设备中查询连接的板卡插槽号失败");
				return null;
			}

			// 查询板卡相关的信息
			var boardIndex = $".0.0.{boardSlot}";
			var board = GetDevAttributeInfo(boardIndex, EnumDevType.board);
			if (null == board)
			{
				Log.Error($"根据索引{boardIndex}未找到对应的板卡信息");
				return null;
			}

			var boardType = MibInfoMgr.GetNeedUpdateValue(board, "netBoardType", false);
			if (null == boardType || "-1" == boardType)
			{
				Log.Error("rhub连接板卡的类型信息错误");
				return null;
			}

			// 查询以太网连接是否存在
			m_strEthRecordIndex = $"{boardIndex}{m_rhubDev.m_strOidIndex}.{m_nRhubEthPort}";
			if (!checkExist.Invoke(m_strEthRecordIndex, EnumDevType.rhub_prru))
			{
				return null;
			}
			//var record = GetDevAttributeInfo(m_strEthRecordIndex, EnumDevType.rhub_prru);
			//if (null != record)
			//{
			//	Log.Error($"根据索引{m_strEthRecordIndex}找到rhub以太网口记录,一个网口只能有一个连接");
			//	return null;
			//}

			var bbi = new BoardBaseInfo
			{
				strBoardCode = boardType,
				strRackNo = "0",
				strShelfNo = "0",
				strSlotNo = boardSlot
			};

			return bbi;
		}

		private static bool HasConnectedToBoard(DevAttributeInfo rhub)
		{
			if (null == rhub)
			{
				throw new ArgumentNullException();
			}

			// 查询rhub设备是否已经建立到board的连接
			var boardSlot = MibInfoMgr.GetNeedUpdateValue(rhub, "netRHUBAccessSlotNo");
			if (null == boardSlot)
			{
				throw new CustomException("获取rhub连接板卡插槽号返回null");
			}

			return ("-1" == boardSlot);
		}

		private bool SetRhubInfoInPico(DevAttributeInfo dev, int nEthPort, int nHubNo)
		{
			if (!MibInfoMgr.SetDevAttributeValue(dev, "netRRUOfp1AccessEthernetPort", nEthPort.ToString()))
			{
				return false;
			}

			if (!MibInfoMgr.SetDevAttributeValue(dev, "netRRUHubNo", nHubNo.ToString()))
			{
				return false;
			}

			return true;
		}

		#endregion


		#region 私有数据区

		private int m_nPicoPort;
		private int m_nRhubEthPort;
		private DevAttributeInfo m_picoDev;
		private DevAttributeInfo m_rhubDev;
		private string m_strEthRecordIndex;

		#endregion
	}
}
