using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan.DevLink
{
	// 专门处理board与rru之间的连接
	public class LinkBoardRru : NetPlanLinkBase
	{
		public LinkBoardRru() : base()
		{
			m_irRecordType = EnumDevType.board_rru;
		}

		public override bool DelLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				return false;
			}

			var oldRecord = GetDevAttributeInfo(m_irRecordIndex, EnumDevType.board_rru);

			// 设置RRU连接的板卡的属性
			var rruClone = m_rruDev.DeepClone();
			if (!ResetRruToBoardInfo(rruClone, m_nRruPort))
			{
				return false;
			}

			if (RecordDataType.NewAdd != rruClone.m_recordType)
			{
				rruClone.m_recordType = RecordDataType.Modified;
			}

			// 删除旧的连接记录
			MibInfoMgr.DelDevFromMap(mapMibInfo, EnumDevType.board_rru, oldRecord);

			mapMibInfo[EnumDevType.rru].Remove(m_rruDev);
			mapMibInfo[EnumDevType.rru].Add(rruClone);

			// todo rru与board的最后一条连接删除时，才设置板类型等信息，先不做

			Log.Debug($"删除连接成功，连接详细信息：{wholeLink}");
			Log.Debug($"删除类型为：{m_irRecordType.ToString()}，索引为：{oldRecord.m_strOidIndex}的记录成功");

			return true;
		}

		public override bool AddLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			mapOriginData = mapMibInfo;
			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordNotExistInAdd))
			{
				return false;
			}

			// 设置netRRUEntry表和netIROptPlanEntry表
			var irRecord = new DevAttributeInfo(EnumDevType.board_rru, m_irRecordIndex) { m_recordType = RecordDataType.NewAdd };

			// 所有的信息全部填在rruClone中，如果失败就直接return
			var rruClone = m_rruDev.DeepClone();
			if (!SetRruToBoardInfo(rruClone, m_nRruPort, m_boardDev, m_nBoardPort))
			{
				return false;
			}

			if (RecordDataType.NewAdd != rruClone.m_recordType)
			{
				rruClone.m_recordType = RecordDataType.Modified;
			}

			// 使用rruClone替代原来的rru，然后增加irRecord记录
			mapMibInfo[EnumDevType.rru].Remove(m_rruDev);
			mapMibInfo[EnumDevType.rru].Add(rruClone);

			AddDevToMap(mapMibInfo, EnumDevType.board_rru, irRecord);

			// todo RRU级联的情况
			Log.Debug($"添加连接成功，连接详细信息：{wholeLink}");
			Log.Debug($"添加类型为：{m_irRecordType.ToString()}，索引为：{irRecord.m_strOidIndex}的记录成功");


			return true;
		}

		public override DevAttributeInfo GetRecord(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			mapOriginData = mapMibInfo;

			if (!CheckLinkIsValid(wholeLink, mapMibInfo, RecordExistInDel))
			{
				return null;
			}

			return GetDevAttributeInfo(m_irRecordIndex, m_irRecordType);
		}

		public override bool CheckLinkIsValid(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo, IsRecordExist checkExist)
		{
			if (null == wholeLink || null == mapMibInfo || mapMibInfo.Count == 0)
			{
				throw new ArgumentNullException();
			}

			var boardIndex = wholeLink.GetDevIndex(EnumDevType.board);
			if (null == boardIndex)
			{
				Log.Error("获取板卡索引失败，请检查添加连接时板卡属性设置是否正确");
				return false;
			}

			m_nBoardPort = wholeLink.GetDevIrPort(EnumDevType.board, EnumPortType.bbu_to_rru);
			if (-1 == m_nBoardPort)
			{
				Log.Error("获取板卡光口号失败，请检查添加连接时板卡属性设置是否正确");
				return false;
			}

			m_boardDev = GetDevAttributeInfo(boardIndex, EnumDevType.board);
			if (null == m_boardDev)
			{
				Log.Error($"根据索引{boardIndex}未找到板卡信息");
				return false;
			}

			m_irRecordIndex = $"{boardIndex}.{m_nBoardPort}";
			var bExist = checkExist.Invoke(m_irRecordIndex, EnumDevType.board_rru);
			if (!bExist)
			{
				return false;
			}

			// rru信息相关
			var rruIndex = wholeLink.GetDevIndex(EnumDevType.rru);
			if (null == rruIndex)
			{
				Log.Error("获取RRU设备索引失败，请检查添加连接时RRU设备属性设置是否正确");
				return false;
			}

			m_nRruPort = wholeLink.GetDevIrPort(EnumDevType.rru, EnumPortType.rru_to_bbu);
			if (-1 == m_nRruPort)
			{
				Log.Error("获取RRU设备光口号失败，请检查添加连接时RRU设备属性设置是否正确");
				return false;
			}

			m_rruDev = GetDevAttributeInfo(rruIndex, EnumDevType.rru);
			if (null == m_rruDev)
			{
				Log.Error($"根据索引{rruIndex}未找到RRU信息");
				return false;
			}

			return true;
		}

		/// <summary>
		/// 设置和板卡相连的RRU的属性信息
		/// </summary>
		/// <returns></returns>
		private bool SetRruToBoardInfo(DevAttributeInfo rruDev, int nRruPort, DevAttributeInfo boardDev, int nBoardPort)
		{
			if (null == rruDev || nRruPort < 0 || nRruPort > MagicNum.RRU_TO_BBU_PORT_CNT - 1 ||
				null == boardDev || nBoardPort < 0 || nBoardPort > MagicNum.BBU_IR_PORT_CNT - 1)
			{
				return false;
			}

			var bbi = GetBoardBaseInfo(boardDev);
			if (null == bbi)
			{
				return false;
			}

			return SetBoardBaseInfoInRru(bbi, rruDev, nRruPort) && SetRruOfpInfo(rruDev, nRruPort, nBoardPort);
		}

		/// <summary>
		/// 重置RRU属性中和板卡相关的所有信息
		/// </summary>
		/// <param name="rruDev"></param>
		/// <param name="nRruPort"></param>
		/// <returns></returns>
		private bool ResetRruToBoardInfo(DevAttributeInfo rruDev, int nRruPort)
		{
			var bbi = new BoardBaseInfo();
			return SetBoardBaseInfoInRru(bbi, rruDev, nRruPort) && SetRruOfpInfo(rruDev, nRruPort, -1);
		}

		#region 私有数据区

		private string m_irRecordIndex;
		private DevAttributeInfo m_boardDev;
		private DevAttributeInfo m_rruDev;
		private int m_nBoardPort;
		private int m_nRruPort;
		private EnumDevType m_irRecordType;

		#endregion

	}



}
