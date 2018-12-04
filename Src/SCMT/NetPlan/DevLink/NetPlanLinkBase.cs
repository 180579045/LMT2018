using System;
using System.Collections.Generic;
using System.Linq;
using LogManager;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan.DevLink
{
	public class NetPlanLinkBase : INetPlanLink
	{
		public delegate bool IsRecordExist(string strRecordIndex, EnumDevType recordType);

		#region 公共接口

		public virtual bool DelLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			throw new NotImplementedException();
		}

		public virtual bool AddLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			throw new NotImplementedException();
		}

		public virtual bool CheckLinkIsValid(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo, IsRecordExist checkExist)
		{
			throw new NotImplementedException();
		}

		public virtual DevAttributeInfo GetRecord(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			return null;
		}

		#endregion 公共接口

		#region 基类公共接口

		/// <summary>
		/// 设置RRU属性中和板卡相关的属性信息
		/// </summary>
		/// <param name="bbi"></param>
		/// <param name="rru"></param>
		/// <param name="nRruIrPort"></param>
		/// <returns></returns>
		protected virtual bool SetBoardBaseInfoInRru(BoardBaseInfo bbi, DevAttributeInfo rru, int nRruIrPort)
		{
			var rruRs = rru.GetNeedUpdateValue("netRRURowStatus");
			if (null == rruRs)
			{
				Log.Error($"从RRU{rru.m_strOidIndex}查询netRRURowStatus属性值失败");
				return false;
			}

			if ("6" == rruRs && rru.m_recordType != RecordDataType.NewAdd)
			{
				Log.Error($"RRU{rru.m_strOidIndex}处于待删除状态，所有属性值无效");
				//return false;
			}

			if (!rru.SetFieldLatestValue("netRRUAccessRackNo", bbi.strRackNo))
			{
				Log.Error($"RRU{rru.m_strOidIndex}设置字段netRRUAccessRackNo值失败");
				return false;
			}

			if (!rru.SetFieldLatestValue("netRRUAccessShelfNo", bbi.strShelfNo))
			{
				Log.Error($"RRU{rru.m_strOidIndex}设置字段netRRUAccessShelfNo值失败");
				return false;
			}

			if (!rru.SetFieldLatestValue("netRRUAccessBoardType", bbi.strBoardCode))
			{
				Log.Error($"RRU{rru.m_strOidIndex}设置字段netRRUAccessBoardType值失败");
				return false;
			}

			var slotMibName = nRruIrPort == 1 ? "netRRUAccessSlotNo" : $"netRRUOfp{nRruIrPort}SlotNo";
			if (!rru.SetFieldLatestValue(slotMibName, bbi.strSlotNo))
			{
				Log.Error($"RRU{rru.m_strOidIndex}设置字段{slotMibName}值失败");
				return false;
			}

			return true;
		}

		/// <summary>
		/// 设置RRU属性和板卡连接端口相关的属性信息
		/// </summary>
		/// <param name="rru"></param>
		/// <param name="nRruIrPort"></param>
		/// <param name="nBoardIrPort"></param>
		/// <returns></returns>
		protected virtual bool SetRruOfpInfo(DevAttributeInfo rru, int nRruIrPort, int nBoardIrPort)
		{
			var workMode = rru.GetNeedUpdateValue("netRRUOfpWorkMode", false);
			if (null == workMode)
			{
				Log.Error($"从rru{rru.m_strOidIndex}查询netRRUOfpWorkMode属性值失败");
				return false;
			}

			var ofpPortMib = $"netRRUOfp{nRruIrPort}AccessOfpPortNo";
			if (!rru.SetFieldLatestValue(ofpPortMib, nBoardIrPort.ToString()))
			{
				Log.Error($"RRU{rru.m_strOidIndex}设置字段{ofpPortMib}值失败");
				return false;
			}

			var lp = -1 == nBoardIrPort ? "-1" : "1";   // 如果端口是-1，那就是删除连接，级数上设置为-1

			var ofpLinePosMib = $"netRRUOfp{nRruIrPort}AccessLinePosition";
			if (!rru.SetFieldLatestValue(ofpLinePosMib, lp))
			{
				Log.Error($"RRU{rru.m_strOidIndex}设置字段{ofpPortMib}值失败");
				return false;
			}

			if (workMode.IndexOf("级联", StringComparison.Ordinal) >= 0)
			{
				// todo 级联模式，计算后面rru的级数，设置板卡基本信息
			}

			return true;
		}

		protected DevAttributeInfo GetDevAttributeInfo(string strIndex, EnumDevType type)
		{
			if (string.IsNullOrEmpty(strIndex) || EnumDevType.unknown == type)
			{
				return null;
			}

			if (mapOriginData.ContainsKey(type))
			{
				var devList = mapOriginData[type];
				return devList.FirstOrDefault(dev => dev.m_strOidIndex == strIndex);
			}

			NPLastErrorHelper.SetLastError($"未找到索引为{strIndex}的{type.ToString()}设备");
			return null;
		}

		public bool SetDevFieldValue(DevAttributeInfo dev, string strFieldName, string strValue)
		{
			if (null == dev || string.IsNullOrEmpty(strFieldName) || string.IsNullOrEmpty(strValue))
			{
				Log.Error("设置设备字段值传入参数为null");
				return false;
			}

			var mapAttributes = dev.m_mapAttributes;
			if (null == mapAttributes || mapAttributes.Count == 0)
			{
				Log.Error("设备的属性为空");
				return false;
			}

			if (!mapAttributes.ContainsKey(strFieldName))
			{
				Log.Error($"索引为{dev.m_strOidIndex}的设备没有找到{strFieldName}属性，无法设置属性值");
				return false;
			}

			var property = mapAttributes[strFieldName];
			if (null == property)
			{
				Log.Error($"索引为{dev.m_strOidIndex}的设备{strFieldName}属性值为null");
				return false;
			}

			return property.SetLatestValue(strValue);
		}

		protected static bool AddDevToMap(MAP_DEVTYPE_DEVATTRI mapData, EnumDevType type, DevAttributeInfo newDev)
		{
			if (null == mapData || type == EnumDevType.unknown || null == newDev)
			{
				throw new ArgumentNullException();
			}

			if (mapData.ContainsKey(type))
			{
				var listDev = mapData[type] ?? new List<DevAttributeInfo>();
				listDev.Add(newDev);
			}
			else
			{
				var listDev = new List<DevAttributeInfo> { newDev };
				mapData[type] = listDev;
			}
			return true;
		}

		protected static BoardBaseInfo GetBoardBaseInfo(DevAttributeInfo board)
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

		/// <summary>
		/// 删除连接时，指定类型、索引的记录必须存在
		/// </summary>
		/// <param name="strRecordIndex"></param>
		/// <param name="recordType"></param>
		/// <returns></returns>
		protected bool RecordExistInDel(string strRecordIndex, EnumDevType recordType)
		{
			if (!mapOriginData.ContainsKey(recordType))
			{
				return false;
			}

			var devList = mapOriginData[recordType];
			if (null == devList || devList.Count == 0)
			{
				return false;
			}

			var dev = devList.FirstOrDefault(tmp => tmp.m_strOidIndex == strRecordIndex);
			return (null != dev);
		}

		/// <summary>
		/// 增加连接时，指定类型、索引的记录不能存在
		/// </summary>
		/// <param name="strRecordIndex"></param>
		/// <param name="recordType"></param>
		/// <returns></returns>
		protected bool RecordNotExistInAdd(string strRecordIndex, EnumDevType recordType)
		{
			if (!mapOriginData.ContainsKey(recordType))
			{
				return true;
			}

			var devList = mapOriginData[recordType];
			if (null == devList || devList.Count == 0)
			{
				return true;
			}

			var dev = devList.FirstOrDefault(tmp => tmp.m_strOidIndex == strRecordIndex);
			return (null == dev);
		}

		/// <summary>
		/// 修改dev的记录类型为modify
		/// </summary>
		/// <param name="dev"></param>
		protected void ChangeDevRecordTypeToModify(DevAttributeInfo dev)
		{
			if (null == dev)
			{
				return;
			}

			if (RecordDataType.NewAdd != dev.m_recordType)
			{
				dev.SetDevRecordType(RecordDataType.Modified);
			}
		}

		#endregion 基类公共接口

		#region 私有数据区

		private readonly object m_syncObj = new object();

		public MAP_DEVTYPE_DEVATTRI mapOriginData;

		#endregion 私有数据区
	}

	public class BoardBaseInfo
	{
		public string strRackNo;
		public string strShelfNo;
		public string strSlotNo;
		public string strBoardCode;

		public BoardBaseInfo()
		{
			strRackNo = "-1";
			strShelfNo = "-1";
			strSlotNo = "-1";
			strBoardCode = "0";
		}

		public BoardBaseInfo(string rack, string shelf, string slot, string code)
		{
			strBoardCode = code;
			strShelfNo = shelf;
			strSlotNo = slot;
			strRackNo = rack;
		}

		public string GetBoardIndex()
		{
			return $".{strRackNo}.{strShelfNo}.{strSlotNo}";
		}
	}
}