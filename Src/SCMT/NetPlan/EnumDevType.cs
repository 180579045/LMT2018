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
		rhub_ant,
		nrNetLc,
		nrLc,
		nrCell
	}

	// 设备类型助手类
	public class DevTypeHelper
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
			EnumDevType devType = (EnumDevType) Enum.Parse(typeof(EnumDevType), type, true);
			return devType;
		}

		public static EnumDevType GetDevTypeFromEntryName(string strEntryName)
		{
			var devType = EnumDevType.unknown;
			if (string.IsNullOrEmpty(strEntryName))
			{
				return devType;
			}

			switch (strEntryName)
			{
				case "netBoardEntry":
					devType = EnumDevType.board;
					break;
				case "netRRUEntry":
					devType = EnumDevType.rru;
					break;
				case "netRHUBEntry":
					devType = EnumDevType.rhub;
					break;
				case "netAntennaArrayEntry":
					devType = EnumDevType.ant;
					break;
				case "netRRUAntennaSettingEntry":
					devType = EnumDevType.rru_ant;
					break;
				case "netIROptPlanEntry":
					devType = EnumDevType.board_rru;
					break;
				case "netEthPlanEntry":
					devType = EnumDevType.rhub_ant;
					break;
				case "nrNetLocalCellEntry":
					devType = EnumDevType.nrNetLc;
					break;
				case "nrLocalCellEntry":
					devType = EnumDevType.nrLc;
					break;
				case "nrCellEntry":
					devType = EnumDevType.nrCell;
					break;
			}

			return devType;
		}
	}
}
