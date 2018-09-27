using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using MIBDataParser.JSONDataMgr;
using SCMTOperationCore.Message.SNMP;

namespace NetPlan
{
	public class NPECmdHelper : Singleton<NPECmdHelper>
	{
		#region 公共接口

		// 根据传入的类型获取所有的命令
		public ObjOperCmd GetAllCmdByType(string type)
		{
			if (string.IsNullOrEmpty(type) || string.IsNullOrWhiteSpace(type))
			{
				throw new CustomException("传入的设备类型有误");
			}

			ObjOperCmd retCmd = null;
			switch (type)
			{
				case "board":
					retCmd = _npeCmd.board;
					break;
				case "rru":
					retCmd = _npeCmd.rru;
					break;
				case "rhub":
					retCmd = _npeCmd.rhub;
					break;
				case "ant":
					retCmd = _npeCmd.ant;
					break;
				case "rru_ant":
					retCmd = _npeCmd.rru_ant;
					break;
				case "board_rru":
					retCmd = _npeCmd.board_rru;
					break;
				default:
					break;
			}

			return retCmd;
		}

		// TODO 提供一个获取当前操作的基站IP的函数
		// 获取指定设备的所有MIB信息，并填入到属性框中
		public List<MibLeafNodeInfo> GetDevAttributesFromMib(string devType)
		{
			var cmdObj = GetAllCmdByType(devType);
			var getCmdList = cmdObj?.get;
			var devAttributList = new List<MibLeafNodeInfo>();

			foreach (var getCmd in getCmdList)
			{
				var target = "172.27.245.92";   // TODO 硬编码IP地址
				var cmdInfo = Database.GetInstance().GetCmdDataByName(getCmd, target);
				if (null == cmdInfo)
				{
					continue;
				}

				var cmdMibInfoList = SnmpToDatabase.ConvertOidListToMibInfoList(cmdInfo.m_leaflist, target);
				devAttributList.AddRange(from cmdMibInfo in cmdMibInfoList where null != cmdMibInfo select new MibLeafNodeInfo {mibAttri = cmdMibInfo});
			}

			return devAttributList;
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

		private NPECmd _npeCmd;

		#endregion

	}

	public class NPECmd
	{
		public ObjOperCmd board;
		public ObjOperCmd rru;
		public ObjOperCmd rhub;
		public ObjOperCmd ant;
		public ObjOperCmd rru_ant;
		public ObjOperCmd board_rru;
	}

	public class ObjOperCmd
	{
		public List<string> get;
		public List<string> set;
		public List<string> add;
		public List<string> del;

		public ObjOperCmd()
		{
			get = new List<string>();
			set = new List<string>();
			add = new List<string>();
			del = new List<string>();
		}
	}
}
