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
	}

	// 设备类型助手类
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
			["localCellEntry"] = EnumDevType.lc
		};
	}
}
