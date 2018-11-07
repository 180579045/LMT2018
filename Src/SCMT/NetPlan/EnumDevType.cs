using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPlan
{
	// 设备类型枚举
	public enum EnumDevType
	{
		unknown = -1,
		board = 0,
		rru,
		rhub,
		ant,
		nrLc,
		nrCell,
		nrNetLc,
		nrNetLcCtr,
		lc,
		cell,
		netLc,
		netLcCtr,
		board_rru,          // 板卡与rru设备的连接，对应表：netIROptPlanEntry
		board_rhub,         // 板卡与hub设备的连接，对应表：netIROptPlanEntry
		rru_ant,            // rru与天线之间的连接，对应表：netRRUAntennaSettingEntry
		rhub_prru,          // hub与pico之间的连接，只有netRruEntry表中的netRRUOfp1AccessEthernetPort，netRRUHubNo两个字段
		prru_ant,           // pico与天线之间的连接，对应表：netRRUAntennaSettingEntry
		rru_rru,            // rru之间的级联，对应表：netRruEntry
		rhub_rhub,			// rhub之间的级联，对应表：netRHUBEntry
	}

	public enum EnumPortType
	{
		unknown = -1,
		rhub_to_bbu,
		rhub_to_pico,
        rhub_to_other,
        pico_to_rhub,
		rru_to_bbu,
        rru_to_other,
        rru_to_ant,
		ant_to_rru,
		bbu_to_rru,
		bbu_to_rhub,
        bbu_to_other,
		rru_to_rru,
		rhub_to_rhub,
		pico_to_ant,
		ant_to_pico,
	}

	/// 设备类型助手类
	public static class DevTypeHelper
	{
		public static string GetDevDescString(EnumDevType type)
		{
			string desc = null;
			switch (type)
			{
				case EnumDevType.board:
				case EnumDevType.rru:
				case EnumDevType.rhub:
				case EnumDevType.ant:
					desc = type.ToString();
					break;
				case EnumDevType.board_rru:
					desc = "board-rru";
					break;
				case EnumDevType.rru_ant:
					desc = "rru-ant";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}

			return desc;
		}

		public static EnumDevType GetEnumDevType(string type)
		{
			var devType = (EnumDevType) Enum.Parse(typeof(EnumDevType), type, true);
			return devType;
		}

		public static EnumDevType GetDevTypeFromEntryName(string strEntryName)
		{
			var devType = EnumDevType.unknown;
			if (string.IsNullOrEmpty(strEntryName))
			{
				return devType;
			}

			if (mapDevTypes.ContainsKey(strEntryName))
			{
				return mapDevTypes[strEntryName];
			}

			return devType;
		}

		public static string GetEntryNameFromDevType(EnumDevType type)
		{
			if (EnumDevType.unknown == type)
			{
				return null;
			}

			if (mapDevTypes.ContainsValue(type))
			{
				return mapDevTypes.FirstOrDefault(kv => kv.Value == type).Key;
			}

			return null;
		}

		/// <summary>
		/// 判断是否是有效的设备组合
		/// 在设备间建立连接时用到
		/// </summary>
		/// <param name="src"></param>
		/// <param name="dst"></param>
		/// <returns></returns>
		public static bool IsValidDevCop(EnumDevType src, EnumDevType dst)
		{
			if (EnumDevType.unknown == src || EnumDevType.unknown == dst)
			{
				return false;
			}

			if (!mapValidDevTypeCon.ContainsKey(src))
			{
				return false;
			}

			var validTypeList = mapValidDevTypeCon[src];
			return validTypeList.Contains(dst);
		}

		/// <summary>
		/// 根据两端设备类型获取连接类型
		/// </summary>
		/// <param name="src"></param>
		/// <param name="dst"></param>
		/// <returns></returns>
		public static EnumDevType GetLinkTypeByTwoEp(EnumDevType src, EnumDevType dst)
		{
			// 板卡和rru之间的连接
			if ((EnumDevType.board == src && EnumDevType.rru == dst) ||
			    (EnumDevType.board == dst && EnumDevType.rru == src))
			{
				return EnumDevType.board_rru;
			}

			// 板卡与rhub之间的连接
			if ((EnumDevType.board == src && EnumDevType.rhub == dst) ||
				(EnumDevType.board == dst && EnumDevType.rhub == src))
			{
				return EnumDevType.board_rhub;
			}

			// rru与ant之间的连接
			if ((EnumDevType.rru == src && EnumDevType.ant == dst) ||
			    (EnumDevType.rru == dst && EnumDevType.ant == src))
			{
				return EnumDevType.rru_ant;
			}

			// rhub与pico之间的连接
			if ((EnumDevType.rhub == src && EnumDevType.rru == dst) ||
			    (EnumDevType.rhub == dst && EnumDevType.rru == src))
			{
				return EnumDevType.rhub_prru;
			}

			// todo pico与ant之间的连接不好判断，没有独立的Pico设备类型

			// rru、rhub之间的级联
			if (EnumDevType.rhub == src && src == dst)
			{
				return EnumDevType.rhub_rhub;
			}

			if (EnumDevType.rru == src && src == dst)
			{
				return EnumDevType.rhub_rhub;
			}

			return EnumDevType.unknown;
		}

		#region 私有数据区

		private static readonly Dictionary<string, EnumDevType> mapDevTypes = new Dictionary<string, EnumDevType>()
		{
			["netBoardEntry"] = EnumDevType.board,
			["netRRUEntry"] = EnumDevType.rru,
			["netRHUBEntry"] = EnumDevType.rhub,
			["netAntennaArrayEntry"] = EnumDevType.ant,
			["netRRUAntennaSettingEntry"] = EnumDevType.rru_ant,
			["netIROptPlanEntry"] = EnumDevType.board_rru,
			["netEthPlanEntry"] = EnumDevType.rhub_prru,
			["nrNetLocalCellEntry"] = EnumDevType.nrNetLc,
			["nrLocalCellEntry"] = EnumDevType.nrLc,
			["nrCellEntry"] = EnumDevType.nrCell,
			["netLocalCellEntry"] = EnumDevType.netLc,
			["localCellEntry"] = EnumDevType.lc,
			["cellEntry"] = EnumDevType.cell,
			["nrNetLocalCellCtrlEntry"] = EnumDevType.nrNetLcCtr,
			["netLocalCellCtrlEntry"] = EnumDevType.netLcCtr,
		};

		private static readonly Dictionary<EnumDevType, List<EnumDevType>> mapValidDevTypeCon =
			new Dictionary<EnumDevType, List<EnumDevType>>()
			{
				{EnumDevType.board, new List<EnumDevType>{EnumDevType.rru, EnumDevType.rhub}},
				{EnumDevType.rhub, new List<EnumDevType> { EnumDevType.rhub, EnumDevType.board, EnumDevType.ant}},
				{EnumDevType.rru, new List<EnumDevType> { EnumDevType.rru, EnumDevType.board, EnumDevType.ant}},
				{EnumDevType.ant, new List<EnumDevType> {EnumDevType.rru}}
			};

		#endregion

	}

	/// 端口类型助手类
	public static class PortTypeHelper
	{
		#region 公共接口区

		public static bool IsValidPortCop(EnumPortType srcPortType, EnumPortType dstPortType)
		{
			if (mapValidPort.ContainsKey(srcPortType))
			{
				return mapValidPort[srcPortType] == dstPortType;
			}

			if (mapValidPort.ContainsKey(dstPortType))
			{
				return mapValidPort[dstPortType] == srcPortType;
			}

			return false;
		}

		#endregion

		#region 私有数据区

		private static readonly Dictionary<EnumPortType, EnumPortType> mapValidPort =
			new Dictionary<EnumPortType, EnumPortType>()
			{
				{EnumPortType.bbu_to_rhub, EnumPortType.rhub_to_bbu },
				{EnumPortType.bbu_to_rru, EnumPortType.rru_to_bbu },
				{EnumPortType.rru_to_ant, EnumPortType.ant_to_rru },
				{EnumPortType.rru_to_rru, EnumPortType.rru_to_rru },
				{EnumPortType.rhub_to_rhub, EnumPortType.rhub_to_rhub },
				{EnumPortType.rhub_to_pico, EnumPortType.pico_to_rhub },
			};

		#endregion
	}
}
