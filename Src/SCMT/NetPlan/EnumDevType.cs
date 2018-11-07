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
		board_rru,
		rru_ant,
		rhub_prru,
		nrNetLc,
		nrLc,
		nrCell,
		netLc,
		lc,
		cell,
		nrNetLcCtr,
		netLcCtr,
	}

	public enum EnumPortType
	{
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
			if ((EnumDevType.board == src && (EnumDevType.rru == dst || EnumDevType.rhub == dst)) ||
			    (EnumDevType.board == dst && (EnumDevType.rru == src || EnumDevType.rhub == src)))
			{
				return EnumDevType.board_rru;
			}

			// rru与ant之间的连接
			if ((EnumDevType.rru == src && EnumDevType.ant == dst) ||
			    (EnumDevType.rru == dst && EnumDevType.ant == src))
			{
				return EnumDevType.rru_ant;
			}

			// todo rhub与pico之间的连接


			// todo rru、rhub之间的级联

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
