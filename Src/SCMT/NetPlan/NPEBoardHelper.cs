using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LmtbSnmp;
using SCMTOperationCore.Elements;

namespace NetPlan
{
	// NetPlanElement缩写为NPE
	public class NPEBoardHelper : Singleton<NPEBoardHelper>
	{
		#region 公共方法

		// 根据槽位号，查询支持的板卡类型。返回板卡名列表
		public List<string> GetSlotSupportBoardNames(int slot, EnbTypeEnum shelfType)
		{
			var listBoardType = new List<string>();

			if (null == _netPlanBoardInfo) return listBoardType;

			var planShelf = from shelf in _netPlanBoardInfo.shelfEquipment
							where (EnbTypeEnum)shelf.equipNEType == shelfType
							select shelf;

			var listBoard = from shelf in planShelf
							from psi in shelf.planSlotInfo
							where psi.slotIndex == slot
							select psi.supportBoardType;

			foreach (var supportBoards in listBoard.ToList())
			{
				foreach (var board in supportBoards)
				{
					var e = MibStringHelper.SplitMibEnumString(board.desc);
					listBoardType.AddRange(e.Select(kv => kv.Value));
				}
			}

			return listBoardType;
		}

		// 根据槽位号，查询支持的板卡类型。返回板卡信息列表
		public List<BoardEquipment> GetSlotSupportBoardInfo(int slot, EnbTypeEnum shelfType)
		{
			var listBoardInfo = new List<BoardEquipment>();

			if (null == _netPlanBoardInfo) return listBoardInfo;

			var planShelf = from shelf in _netPlanBoardInfo.shelfEquipment
							where (EnbTypeEnum)shelf.equipNEType == shelfType
							select shelf;

			var listBoard = from shelf in planShelf
							from psi in shelf.planSlotInfo
							where psi.slotIndex == slot
							select psi.supportBoardType;

			foreach (var supportBoards in listBoard.ToList())
			{
				foreach (var board in supportBoards)
				{
					var boardType = int.Parse(board.value);
					listBoardInfo.AddRange(_netPlanBoardInfo.boardEquipment.Where(equip => equip.boardType == boardType));
				}
			}

			return listBoardInfo;
		}

		/// <summary>
		/// 根据板卡型号获取板卡信息
		/// </summary>
		/// <param name="boardType">板卡型号</param>
		/// <returns></returns>
		public BoardEquipment GetBoardInfoByType(int boardType)
		{
			var boardList = _netPlanBoardInfo.boardEquipment;
			return boardList.FirstOrDefault(board => board.boardType == boardType);
		}

		public BoardEquipment GetBoardInfoByType(string strBoardType)
		{
			return GetBoardInfoByType(int.Parse(strBoardType));
		}

		/// <summary>
		/// 根据板卡名，查询板卡的信息
		/// </summary>
		/// <param name="strBoardName"></param>
		/// <returns></returns>
		public BoardEquipment GetBoardInfoByName(string strBoardName)
		{
			if (string.IsNullOrEmpty(strBoardName))
			{
				return null;
			}

			var beList = _netPlanBoardInfo.boardEquipment;
			return beList.FirstOrDefault(board => board.boardTypeName.IndexOf(strBoardName, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		/// <summary>
		/// 获取板卡工作模式描述列表
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<string> GetBoardWorkMode()
		{
			return GetMibValueRangeList("netBoardWorkMode");
		}

		/// <summary>
		/// 获取板卡帧结构类型描述信息列表
		/// </summary>
		/// <returns></returns>
		public static IEnumerable<string> GetBoardIrFrameType()
		{
			return GetMibValueRangeList("netBoardIrFrameType");
		}

		/// <summary>
		/// 获取所有的rhub设备信息
		/// </summary>
		/// <returns></returns>
		public List<RHUBEquipment> GetRhubEquipments()
		{
			return _netPlanBoardInfo?.rHubEquipment;
		}

		/// <summary>
		/// 获取rhub设备的工作模式
		/// </summary>
		/// <returns></returns>
		public static List<string> GetRhubOfpWorkMode()
		{
			return GetMibValueRangeList("netRHUBOfpWorkMode");
		}

		/// <summary>
		/// 根据基站类型获取基站的信息
		/// </summary>
		/// <param name="enbType"></param>
		/// <returns></returns>
		public Shelf GetShelfByEnbType(EnbTypeEnum enbType)
		{
			var shelfList = _netPlanBoardInfo.shelfEquipment;
			if (null != shelfList && shelfList.Count > 0)
			{
				return shelfList.FirstOrDefault(itemShelf => itemShelf.equipNEType == (int) enbType);
			}

			return null;
		}

	    public NetPlanElement GetNetPlanBoardInfo()
	    {
	        return _netPlanBoardInfo;
	    }


		#endregion

		#region 私有方法

		private NPEBoardHelper()
		{
			_netPlanBoardInfo = null;

			try
			{
				var path = FilePathHelper.GetAppPath() + ConfigFileHelper.NetPlanShelfJson;
				var jsonContent = FileRdWrHelper.GetFileContent(path, Encoding.UTF8);
				_netPlanBoardInfo = JsonHelper.SerializeJsonToObject<NetPlanElement>(jsonContent);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		/// <summary>
		/// 查询给定MIB的取值范围
		/// </summary>
		/// <param name="strMibName"></param>
		/// <returns></returns>
		private static List<string> GetMibValueRangeList(string strMibName)
		{
			var wmList = new List<string>();

			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				return wmList;
			}

			var mibInfo = SnmpToDatabase.GetMibNodeInfoByName(strMibName, targetIp);
			if (null == mibInfo)
			{
				return wmList;
			}

			var mvr = mibInfo.managerValueRange;
			var mapMv = MibStringHelper.SplitManageValue(mvr);
			wmList.AddRange(mapMv.Select(kv => kv.Value));
			return wmList;
		}

		#endregion

		#region 私有成员

		private readonly NetPlanElement _netPlanBoardInfo;

		#endregion
	}

	public class NetPlanElement
	{
		public List<NetPlanElementType> netPlanElements;
		public List<Shelf> shelfEquipment;
		public List<BoardEquipment> boardEquipment;
		public List<RHUBEquipment> rHubEquipment;

		public NetPlanElement()
		{
			netPlanElements = new List<NetPlanElementType>();
			shelfEquipment = new List<Shelf>();
			boardEquipment = new List<BoardEquipment>();
			rHubEquipment = new List<RHUBEquipment>();
		}
	}

	#region 机架的信息

	public class VD
	{
		public string value { get; set; }
		public string desc { get; set; }
	}

	public class ShelfSlotInfo
	{
		public int slotIndex { get; set; }
		public List<VD> supportBoardType { get; set; }

		public ShelfSlotInfo()
		{
			supportBoardType = new List<VD>();
		}
	}

	public class Shelf
	{
		public int number { get; set; }
        public int equipNEType { get; set; }
        public string equipNETypeName { get; set; }
        public int totalSlotNum { get; set; }
        public int supportPlanSlotNum { get; set; }
        public int columnsUI { get; set; }
        public List<ShelfSlotInfo> planSlotInfo { get; set; }

		public Shelf()
		{
			planSlotInfo = new List<ShelfSlotInfo>();
		}
	}

	#endregion

	#region 板卡的规划信息

	public class OfpPortInfo
	{
		public int ofpIndex { get; set; }
        public List<VD> irOfpPortTransSpeed { get; set; }

		public OfpPortInfo()
		{
			irOfpPortTransSpeed = new List<VD>();
		}
	}

	public class BoardEquipment
	{
		public int number { get; set; }
        public int boardType { get; set; }
        public string boardTypeName { get; set; }
        public int supportEquipType { get; set; }
        public List<VD> supportConnectElement { get; set; }
        public int irOfpNum { get; set; }
        public List<OfpPortInfo> irOfpPortInfo { get; set; }

		public BoardEquipment()
		{
			supportConnectElement = new List<VD>();
			irOfpPortInfo = new List<OfpPortInfo>();
		}
	}

	#endregion

	#region rhub信息

	public class RHUBEquipment
	{
		public int number;
		public string rHubType;
		public string friendlyUIName;
		public int irOfpNum;
		public List<VD> irOfpPortTransSpeed;
		public int ethPortRNum;
		public List<VD> ethPortTransSpeed;

		public RHUBEquipment()
		{
			irOfpPortTransSpeed = new List<VD>();
			ethPortTransSpeed = new List<VD>();
		}
	}

	#endregion

	#region 网规设备类型

	public class NetPlanElementType
	{
		public int number { get; set; }
        public string elementName { get; set; }
	}

	#endregion
}
