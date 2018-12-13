using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogManager;

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

		antWeight,			// 天线权重·
		antCoup,			// 天线耦合系数
		antBfScan,          // 天线波束扫描
		bandWidth,			// 基带带宽
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
		public static EnumDevType GetEnumDevType(string type)
		{
			var devType = (EnumDevType) Enum.Parse(typeof(EnumDevType), type, true);
			return devType;
		}

		/// <summary>
		/// 根据MIB表入口名查询设备类型
		/// </summary>
		/// <param name="strEntryName">MIB表入口名，以entry结尾</param>
		/// <returns></returns>
		public static EnumDevType GetDevTypeFromEntryName(string strEntryName)
		{
			var devType = EnumDevType.unknown;
			if (string.IsNullOrEmpty(strEntryName))
			{
				return devType;
			}

			// 根据表入口名得到Alias字符串
			var alias = NPECmdHelper.GetInstance().GetAliasByEntryName(strEntryName);
			if (string.IsNullOrEmpty(alias))
			{
				Log.Error($"根据表入口{strEntryName}查询对应的alias失败");
				return devType;
			}

			// 把alias转换为类型
			return Enum.TryParse(alias, true, out devType) ? devType : EnumDevType.unknown;
		}

		/// <summary>
		/// 根据设备类型名查询MIB表入口
		/// 类型名和配置文件中的alias一一对应方可
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static string GetEntryNameFromDevType(EnumDevType type)
		{
			if (EnumDevType.unknown == type)
			{
				return null;
			}

			var strDevType = type.ToString();
			return NPECmdHelper.GetInstance().GetEntryNameByAlias(strDevType);
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
				return EnumDevType.rru_rru;
			}

			return EnumDevType.unknown;
		}

		#region 私有数据区

		private static readonly Dictionary<EnumDevType, List<EnumDevType>> mapValidDevTypeCon =
			new Dictionary<EnumDevType, List<EnumDevType>>()
			{
				{EnumDevType.board, new List<EnumDevType>{EnumDevType.rru, EnumDevType.rhub}},
				{EnumDevType.rhub, new List<EnumDevType> { EnumDevType.rhub, EnumDevType.board, EnumDevType.ant, EnumDevType.rru}},
				{EnumDevType.rru, new List<EnumDevType> { EnumDevType.rru, EnumDevType.board, EnumDevType.ant, EnumDevType.rhub}},
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
				{EnumPortType.pico_to_ant, EnumPortType.ant_to_pico }
			};

		#endregion
	}
}
