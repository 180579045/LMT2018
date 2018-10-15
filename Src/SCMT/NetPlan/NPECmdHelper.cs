using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using MIBDataParser.JSONDataMgr;
using LmtbSnmp;

namespace NetPlan
{
	public class NPECmdHelper : Singleton<NPECmdHelper>
	{
		#region 公共接口

		// 根据传入的类型获取所有的命令
		public NetPlanMibEntry GetAllCmdByType(string type)
		{
			if (string.IsNullOrEmpty(type) || string.IsNullOrWhiteSpace(type))
			{
				throw new CustomException("传入的设备类型有误");
			}

			//ObjOperCmd retCmd = null;
			//switch (type)
			//{
			//	case "board":
			//		retCmd = _npeCmd.board;
			//		break;
			//	case "rru":
			//		retCmd = _npeCmd.rru;
			//		break;
			//	case "rhub":
			//		retCmd = _npeCmd.rhub;
			//		break;
			//	case "ant":
			//		retCmd = _npeCmd.ant;
			//		break;
			//	case "rru_ant":
			//		retCmd = _npeCmd.rru_ant;
			//		break;
			//	case "board_rru":
			//		retCmd = _npeCmd.board_rru;
			//		break;
			//	default:
			//		break;
			//}

			return null;
		}

		// 获取指定设备的所有MIB信息，并填入到属性框中
		public List<MibLeafNodeInfo> GetDevAttributesFromMib(string devType)
		{
			//var cmdObj = GetAllCmdByType(devType);
			//var getCmdList = cmdObj?.get;
			var devAttributList = new List<MibLeafNodeInfo>();

			//var target = CSEnbHelper.GetCurEnbAddr();
			//if (null == target)
			//{
			//	throw new CustomException("当前未选中基站");
			//}

			//foreach (var getCmd in getCmdList)
			//{
			//	var cmdInfo = SnmpToDatabase.GetCmdInfoByCmdName(getCmd, target);
			//	if (null == cmdInfo)
			//	{
			//		continue;
			//	}

			//	var cmdMibInfoList = SnmpToDatabase.ConvertOidListToMibInfoList(cmdInfo.m_leaflist, target);
			//	devAttributList.AddRange(from cmdMibInfo in cmdMibInfoList where null != cmdMibInfo select new MibLeafNodeInfo {mibAttri = cmdMibInfo});
			//}

			return devAttributList;
		}

		// 获取所有的mib信息和命令信息
		public List<NetPlanMibEntry> GetAllMibEntryAndCmds(string strVersion = "EMB6116")
		{
			return _npeCmd.EMB6116.NetPlanMibEntrys;
		}

		#endregion

		#region 私有方法

		private NPECmdHelper()
		{
			_npeCmd = null;

			try
			{
				var path = FilePathHelper.GetAppPath() + ConfigFileHelper.NetPlanCmdJson;
				var jsonContent = FileRdWrHelper.GetFileContent(path);
				_npeCmd = JsonHelper.SerializeJsonToObject<NPECmd>(jsonContent);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		#endregion


		#region 私有属性

		private readonly NPECmd _npeCmd;

		#endregion

	}

	public class NPECmd
	{
		public GridMibCfg EMB6116;
	}

	public class GridMibCfg
	{
		public string Description;
		public List<NetPlanMibEntry> NetPlanMibEntrys;
	}

	public class NetPlanMibEntry
	{
		public string MibEntry;
		public List<string> Get;
		public List<string> Set;
		public List<string> Add;
		public List<string> Del;

		public NetPlanMibEntry()
		{
			Get = new List<string>();
			Add = new List<string>();
			Set = new List<string>();
			Del = new List<string>();
		}
	}
}
