﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using MIBDataParser.JSONDataMgr;
using LmtbSnmp;
using MIBDataParser;

namespace NetPlan
{
	public class NPECmdHelper : Singleton<NPECmdHelper>
	{
		#region 公共接口

		// 获取指定设备的所有MIB信息，并填入到属性框中
		public List<MibLeafNodeInfo> GetDevAttributesFromMib(string devType)
		{
			var enumType = DevTypeHelper.GetEnumDevType(devType);
			if (EnumDevType.unknown == enumType)
			{
				return null;
			}

			var entryName = DevTypeHelper.GetEntryNameFromDevType(enumType);
			if (string.IsNullOrEmpty(entryName))
			{
				return null;
			}

			var mapAttributes = GetDevAttributesByEntryName(entryName);
			if (null == mapAttributes || 0 == mapAttributes.Count)
			{
				return null;
			}

			var devAttributList = mapAttributes.Values.ToList();
			devAttributList.Sort(new MLNIComparer());

			return devAttributList;
		}

		public Dictionary<string, MibLeafNodeInfo> GetDevAttributesByEntryName(string strEntryName)
		{
			if (string.IsNullOrEmpty(strEntryName))
			{
				return null;
			}

			var npMibEntryList = _npeCmd.EMB6116.NetPlanMibEntrys;

			// 根据MIB入口名得到Get命令
			var getCmdsList = (from entryObj in npMibEntryList where entryObj.MibEntry.Equals(strEntryName) select entryObj.Get).FirstOrDefault();

			if (null == getCmdsList)
			{
				return null;
			}

			var target = CSEnbHelper.GetCurEnbAddr();
			if (null == target)
			{
				throw new CustomException("当前未选中基站");
			}

			var ret = new Dictionary<string, MibLeafNodeInfo>();

			// 遍历Get命令，得到所有的MIB表项。
			foreach (var getCmd in getCmdsList)
			{
				var cmdInfo = SnmpToDatabase.GetCmdInfoByCmdName(getCmd, target);
				if (null == cmdInfo)
				{
					continue;
				}

				var cmdMibInfoList = SnmpToDatabase.ConvertOidListToMibInfoList(cmdInfo.m_leaflist, target);
				foreach (var mibLeaf in cmdMibInfoList)
				{
					if (null == mibLeaf || 0 == mibLeaf.isMib)
						continue;

					var defaultValue = MibStringHelper.GetMibDefaultValue(mibLeaf.defaultValue);
					var info = new MibLeafNodeInfo
					{
						mibAttri = mibLeaf,
						m_strOriginValue = SnmpToDatabase.ConvertValueToString(mibLeaf, defaultValue), // 原始值设置为默认值
						m_bReadOnly = (mibLeaf.IsIndex == "True"),		// 索引只读
						m_bVisible = (mibLeaf.ASNType != "RowStatus")	// 行状态不显示
					};

					if (!ret.ContainsKey(mibLeaf.childNameMib))
					{
						ret.Add(mibLeaf.childNameMib, info);
					}
					else
					{
						ret[mibLeaf.childNameMib] = info;		// 如果已存在，直接更新
					}
				}
			}

			return ret;
		}

		// 获取所有的mib信息和命令信息
		public List<NetPlanMibEntry> GetAllMibEntryAndCmds(string strVersion)
		{
			if (strVersion == "EMB6116")
			{
				return _npeCmd.EMB6116.NetPlanMibEntrys;
			}

			if (strVersion == "EMB5116")
			{
				return _npeCmd.EMB5116.NetPlanMibEntrys;
			}

			return null;
		}

		/// <summary>
		/// 根据version和设备类型，获取对应的Set命令列表
		/// </summary>
		/// <param name="strVersion"></param>
		/// <param name="devType"></param>
		/// <param name="cmdType"></param>
		/// <returns></returns>
		public List<string> GetCmdList(EnumDevType devType, EnumSnmpCmdType cmdType, string strVersion = "EMB5116")
		{
			if (string.IsNullOrEmpty(strVersion) || EnumDevType.unknown == devType)
			{
				return null;
			}

			var mibEntrys = GetAllMibEntryAndCmds(strVersion);
			if (null == mibEntrys)
			{
				return null;
			}

			var entryName = DevTypeHelper.GetEntryNameFromDevType(devType);
			if (string.IsNullOrEmpty(entryName))
			{
				return null;
			}

			List<string> cmds = null;
			switch (cmdType)
			{
				case EnumSnmpCmdType.Get:
					cmds = (from mibEntry in mibEntrys where mibEntry.MibEntry.Equals(entryName) select mibEntry.Get).FirstOrDefault();
					break;
				case EnumSnmpCmdType.Set:
					cmds = (from mibEntry in mibEntrys where mibEntry.MibEntry.Equals(entryName) select mibEntry.Set).FirstOrDefault();
					break;
				case EnumSnmpCmdType.Add:
					cmds = (from mibEntry in mibEntrys where mibEntry.MibEntry.Equals(entryName) select mibEntry.Add).FirstOrDefault();
					break;
				case EnumSnmpCmdType.Del:
					cmds = (from mibEntry in mibEntrys where mibEntry.MibEntry.Equals(entryName) select mibEntry.Del).FirstOrDefault();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(cmdType), cmdType, null);
			}

			return cmds;
		}

		/// <summary>
		/// 转换同类命令为MibLeaf
		/// </summary>
		/// <param name="cmdLists"></param>
		/// <returns></returns>
		public Dictionary<string, List<MibLeaf>> GetSameTypeCmdMibLeaf(IEnumerable<string> cmdLists)
		{
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (string.IsNullOrEmpty(targetIp))
			{
				throw new CustomException("尚未选中基站"); // TODO 选中基站后再操作其他功能，可能存在bug
			}

			// key:命令名， value：该命令对应的leaf oid转换为mibleafnodeInfo list
			var mapCmdNameToMibLeafInfoList = new Dictionary<string, List<MibLeaf>>();
			foreach (var cmdName in cmdLists)
			{
				var cmdInfo = SnmpToDatabase.GetCmdInfoByCmdName(cmdName, targetIp);
				if (null == cmdInfo)
				{
					return null;
				}

				var oidList = cmdInfo.m_leaflist;
				var mibLeafInfoList = SnmpToDatabase.ConvertOidListToMibInfoList(oidList, targetIp);
				var leafInfoList = mibLeafInfoList as IList<MibLeaf> ?? mibLeafInfoList.ToList();
				if (!leafInfoList.Any())
				{
					continue;
				}

				mapCmdNameToMibLeafInfoList.Add(cmdName, leafInfoList.ToList());
			}

			return mapCmdNameToMibLeafInfoList;
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
		public GridMibCfg EMB5116;
	}

	public class GridMibCfg
	{
		public string Description;
		public List<NetPlanMibEntry> NetPlanMibEntrys;
	}

	public class NetPlanMibEntry
	{
		public string MibEntry;
		public string Alias;
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

	public enum EnumSnmpCmdType
	{
		Get = 0,
		Set,
		Add,
		Del
	}
}
