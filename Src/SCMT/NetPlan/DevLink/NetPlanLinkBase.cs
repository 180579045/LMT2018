using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan.DevLink
{
	public class NetPlanLinkBase : INetPlanLink
	{
		#region 公共接口

		public virtual bool DelLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			throw new NotImplementedException();
		}

		public virtual bool AddLink(WholeLink wholeLink, ref MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			throw new NotImplementedException();
		}

		public virtual bool CheckLinkIsValid(WholeLink wholeLink, MAP_DEVTYPE_DEVATTRI mapMibInfo)
		{
			throw new NotImplementedException();
		}

		public virtual bool SetBoardBaseInfoInRru(BoardBaseInfo bbi, DevAttributeInfo rru, int nRruIrPort)
		{
			var rruRs = MibInfoMgr.GetNeedUpdateValue(rru, "netRRURowStatus");
			if (null == rruRs)
			{
				Log.Error($"从RRU{rru.m_strOidIndex}查询netRRURowStatus属性值失败");
				return false;
			}

			if ("6" == rruRs && rru.m_recordType != RecordDataType.NewAdd)
			{
				Log.Error($"RRU{rru.m_strOidIndex}处于待删除状态，所有属性值无效");
				return false;
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


		public virtual bool SetRruOfpInfo(DevAttributeInfo rru, int nRruIrPort, int nBoardIrPort)
		{
			var wmo = rru.GetFieldOriginValue("netRRUOfpWorkMode", false);
			var wml = rru.GetFieldLatestValue("netRRUOfpWorkMode", false);

			var workMode = MibInfoMgr.GetNeedUpdateValue(wmo, wml);
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

			var ofpLinePosMib = $"netRRUOfp{nRruIrPort}AccessLinePosition";
			if (!rru.SetFieldLatestValue(ofpLinePosMib, "1"))   // 和板卡相连的rru级联级数为1
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

		public DevAttributeInfo GetDevAttributeInfo(string strIndex, EnumDevType type)
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

			return property.SetValue(strValue);
		}

		public static bool AddDevToMap(MAP_DEVTYPE_DEVATTRI mapData, EnumDevType type, DevAttributeInfo newDev)
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
				var listDev = new List<DevAttributeInfo> {newDev};
				mapData[type] = listDev;
			}
			return true;
		}

		public static BoardBaseInfo GetBoardBaseInfo(DevAttributeInfo board)
		{
			var boardRs = MibInfoMgr.GetNeedUpdateValue(board, "netBoardRowStatus");
			if (null == boardRs)
			{
				Log.Error($"从板卡{board.m_strOidIndex}查询netBoardRowStatus属性值失败");
				return null;
			}

			if ("6" == boardRs && board.m_recordType != RecordDataType.NewAdd)
			{
				Log.Error($"板卡{board.m_strOidIndex}处于待删除状态，所有属性值无效");
				return null;
			}

			var rackNo = MibInfoMgr.GetNeedUpdateValue(board, "netBoardRackNo");
			if (null == rackNo)
			{
				Log.Error($"从板卡{board.m_strOidIndex}查询netBoardRackNo属性值失败");
				return null;
			}

			var shelfNo = MibInfoMgr.GetNeedUpdateValue(board, "netBoardShelfNo");
			if (null == shelfNo)
			{
				Log.Error($"从板卡{board.m_strOidIndex}查询netBoardShelfNo属性值失败");
				return null;
			}

			var slotNo = MibInfoMgr.GetNeedUpdateValue(board, "netBoardSlotNo");
			if (null == slotNo)
			{
				Log.Error($"从板卡{board.m_strOidIndex}查询netBoardSlotNo属性值失败");
				return null;
			}

			var boardType = MibInfoMgr.GetNeedUpdateValue(board, "netBoardType");
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

		#region 私有数据区

		private readonly object m_syncObj = new object();

		public MAP_DEVTYPE_DEVATTRI mapOriginData;

		#endregion
	}

	public class BoardBaseInfo
	{
		public string strRackNo;
		public string strShelfNo;
		public string strSlotNo;
		public string strBoardCode;
	}
}
