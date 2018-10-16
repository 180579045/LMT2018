using System;
using System.Collections.Generic;
using System.IO;
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

			return null;
		}

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
}
