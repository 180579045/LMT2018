using CommonUtility;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using SCMTOperationCore.Elements;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

		/// <summary>
		/// 根据表名查询该表下所有的叶子节点
		/// </summary>
		/// <param name="strEntryName"></param>
		/// <returns></returns>
		public Dictionary<string, MibLeafNodeInfo> GetDevAttributesByEntryName(string strEntryName)
		{
			if (string.IsNullOrEmpty(strEntryName))
			{
				Log.Error("传入MIB表入口名错误");
				return null;
			}

			var target = CSEnbHelper.GetCurEnbAddr();
			if (null == target)
			{
				throw new CustomException("当前未选中基站");
			}

			//var enbType = NodeBControl.GetInstance().GetEnbTypeByIp(target);
			var enbType = EnbTypeEnum.ENB_EMB6116;
			var npMibEntryList = GetAllMibEntryAndCmds(enbType);

			// 根据MIB入口名得到Get命令
			var getCmdsList = GetCmdList(strEntryName, EnumSnmpCmdType.Get);
			if (null == getCmdsList)
			{
				Log.Error("未能获取到Get命令");
				return null;
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
						m_bReadOnly = !mibLeaf.IsEmpoweredModify(),
						m_bVisible = (mibLeaf.ASNType != "RowStatus")   // 行状态不显示
					};

					// todo netRRUEntry表存在问题：默认值为0：unknown，但取值范围中没有0，会设置为null
					if (null == info.m_strOriginValue)
					{
						Log.Error($"字段{mibLeaf.childNameMib}的默认值{mibLeaf.defaultValue}在取值范围{mibLeaf.managerValueRange}中不存在");
						info.m_strOriginValue = "-1";
					}

					if (!ret.ContainsKey(mibLeaf.childNameMib))
					{
						ret.Add(mibLeaf.childNameMib, info);
					}
					else
					{
						ret[mibLeaf.childNameMib] = info;       // 如果已存在，直接更新
					}
				}
			}

			return ret;
		}

		// 获取所有的mib信息和命令信息
		public List<NetPlanMibEntry> GetAllMibEntryAndCmds(EnbTypeEnum enbType = EnbTypeEnum.ENB_EMB6116)
		{
			return EnbTypeEnum.ENB_EMB6116 == enbType ? _npeCmd.EMB6116.NetPlanMibEntrys : null;
		}

		/// <summary>
		/// 根据version和设备类型，获取对应的Set命令列表
		/// </summary>
		/// <param name="strVersion"></param>
		/// <param name="devType"></param>
		/// <param name="cmdType"></param>
		/// <returns></returns>
		public List<string> GetCmdList(EnumDevType devType, EnumSnmpCmdType cmdType, EnbTypeEnum enbType)
		{
			if (EnumDevType.unknown == devType)
			{
				return null;
			}

			var mibEntrys = GetAllMibEntryAndCmds(enbType);
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
		/// 根据表入口名获取对应命令。只能遍历数据库进行匹配
		/// </summary>
		/// <param name="strEntryName"></param>
		/// <param name="cmdType"></param>
		/// <param name="enbType"></param>
		/// <returns></returns>
		public List<string> GetCmdList(string strEntryName, EnumSnmpCmdType cmdType)
		{
			if (cmdType == EnumSnmpCmdType.Invalid)
			{
				return null;
			}

			var devType = DevTypeHelper.GetDevTypeFromEntryName(strEntryName);
			if (devType != EnumDevType.unknown)
			{
				return GetCmdList(devType, cmdType, EnbTypeEnum.ENB_EMB6116);
			}

			var cmdList = Database.GetInstance().GetCmdsInfoByEntryName(strEntryName, CSEnbHelper.GetCurEnbAddr());
			if (null == cmdList)
			{
				return null;
			}

			var cmdStr = cmdType.ToString();

			// todo 此处判断条件是以xx开始，具有局限性
			return cmdList.Select(cmi => cmi.m_cmdNameEn).Where(cmdName => cmdName.StartsWith(cmdStr, true, CultureInfo.CurrentCulture)).ToList();
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

		/// <summary>
		/// 根据表入口名，查询Alias字段的名字
		/// </summary>
		/// <param name="strEntryName"></param>
		/// <returns></returns>
		public string GetAliasByEntryName(string strEntryName)
		{
			return (from tmp in _npeCmd.EMB6116.NetPlanMibEntrys
					where tmp.MibEntry == strEntryName
					select tmp.Alias)
					.FirstOrDefault();
		}

		/// <summary>
		/// 根据别名查询MIB入口名
		/// </summary>
		/// <param name="strAlias"></param>
		/// <returns></returns>
		public string GetEntryNameByAlias(string strAlias)
		{
			return (from tmp in _npeCmd.EMB6116.NetPlanMibEntrys
					where tmp.Alias.Equals(strAlias, StringComparison.InvariantCultureIgnoreCase)
					select tmp.MibEntry)
				.FirstOrDefault();
		}

		#endregion 公共接口

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

		#endregion 私有方法

		#region 私有属性

		private readonly NPECmd _npeCmd;

		#endregion 私有属性
	}

	public class NPECmd
	{
		public GridMibCfg EMB6116;
		//public GridMibCfg EMB5116;
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
		public string InitQuery;        //用于判定初始化网规页面时，是否从基站中查询这个消息
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
		Invalid = -1,
		Get = 1,
		Add,
		Set,
		Del
	}
}