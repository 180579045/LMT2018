using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;

namespace NetPlan
{
	// 基站型号定义
	public enum SHELFTYPE
	{
		EMB5116 = 5,		// 4G时代基站
		EMB6116 = 10		// 5G时代基站
	}

	// NetPlanElement缩写为NPE
	public class NPEBoardHelper : Singleton<NPEBoardHelper>
	{
		#region 公共方法

		// 根据槽位号，查询支持的板卡类型。返回板卡名列表
		public List<string> GetSlotSupportBoardNames(int slot, SHELFTYPE shelfType = SHELFTYPE.EMB5116)
		{
			var listBoardType = new List<string>();

			if (null == _netPlanBoardInfo) return listBoardType;

			var planShelf = from shelf in _netPlanBoardInfo.shelfEquipment
							where (SHELFTYPE)shelf.equipNEType == shelfType
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
		public List<BoardEquipment> GetSlotSupportBoardInfo(int slot, SHELFTYPE shelfType = SHELFTYPE.EMB5116)
		{
			var listBoardInfo = new List<BoardEquipment>();

			if (null == _netPlanBoardInfo) return listBoardInfo;

			var planShelf = from shelf in _netPlanBoardInfo.shelfEquipment
							where (SHELFTYPE)shelf.equipNEType == shelfType
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

		#endregion

		#region 私有成员

		private NetPlanElement _netPlanBoardInfo;

		#endregion
	}

	public class NetPlanElement
	{
		public List<NetPlanElementType> netPlanElements;
		public List<Shelf> shelfEquipment;
		public List<BoardEquipment> boardEquipment;
		public List<RHUBEquipment> rHubEquipment;
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
		public List<VD> supportBoardType;
	}

	public class Shelf
	{
		public int number;
		public int equipNEType;
		public string equipNETypeName;
		public int totalSlotNum;
		public int supportPlanSlotNum;
		public int columnsUI;
		public List<ShelfSlotInfo> planSlotInfo;
	}

	#endregion

	#region 板卡的规划信息

	public class OfpPortInfo
	{
		public int ofpIndex;
		public List<VD> irOfpPortTransSpeed;
	}

	public class BoardEquipment
	{
		public int number;
		public int boardType;
		public string boardTypeName;
		public int supportEquipType;
		public List<VD> supportConnectElement;
		public int irOfpNum;
		public List<OfpPortInfo> irOfpPortInfo;
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
	}

	#endregion

	#region 网规设备类型

	public class NetPlanElementType
	{
		public int number;
		public string elementName;
	}

	#endregion
}
