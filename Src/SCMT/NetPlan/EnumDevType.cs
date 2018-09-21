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
		board = 0,
		rru,
		rhub,
		ant,
		board_rru,
		rru_ant,

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
	}
}
