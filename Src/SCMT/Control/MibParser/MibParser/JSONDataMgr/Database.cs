using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//using CommonUtility;
using Newtonsoft.Json.Linq;


namespace MIBDataParser.JSONDataMgr
{
	public class CmdMibInfo
	{
		public string m_cmdNameEn { get;} // 命令的英文名
		public string m_tableName { get;  } // 命令的mib表英文名
		public string m_cmdType { get; } //命令类型
		public string m_cmdDesc { get; } //命令描述
		public List<string> m_leaflist { get; } // 命令节点oid

		public CmdMibInfo(JObject value, string cmdNameEn)
		{
			this.m_cmdNameEn = cmdNameEn;
			this.m_tableName = value["TableName"].ToString();
			this.m_cmdType = value["CmdType"].ToString();
			this.m_cmdDesc = value["CmdDesc"].ToString();

			this.m_leaflist = new List<string>();
			foreach (var leaf in value["leafOIdList"])
			{
				m_leaflist.Add(leaf.ToString());
			}
		}
	}

	/// <summary>
	/// 数据库操作相关的类
	/// C# sealed修饰符表示密封用于类时，表示该类不能再被继承
	/// </summary>
	public sealed partial class Database : IDatabase
	{
		public  ResultInitData resultInitData; // 委托, 返回初始化的结果

		#region 公有接口

		public static Database GetInstance()
		{
			if (null == _instance)
			{
				lock (_syncLock)
				{
					if (null == _instance)
					{
						_instance = new Database();
					}
				}
			}
			return _instance;
		}

		// 初始化
		/// <summary>
		/// 初始化 线程: 调用 DBInitDateBaseByIpConnect 。
		/// </summary>
		/// <param name="connectIp">基站连接的ip，标记数据库的归属，是哪个基站的数据</param>
		public void initDatabase(string connectIp)
		{
			try
			{
				new Thread(DBInitDateBaseByIpConnect).Start(connectIp);
			}
			catch
			{
				resultInitData(false);
			}
		}

		// 删除数据库列表
		public bool delDatabase(string connectIp)
		{
			if (string.IsNullOrEmpty(connectIp))
			{
				return false;
			}

			lock (_syncLock)
			{
				if (ipToMibInfoMap.ContainsKey(connectIp))
				{
					ipToMibInfoMap.Remove(connectIp);
				}

				if (ipToCmdInfoMap.ContainsKey(connectIp))
				{
					ipToCmdInfoMap.Remove(connectIp);
				}
			}

			return true;
		}

		// 根据MIB名获取MIB信息
		public bool getDataByEnglishName(string nameEn, out MibLeaf reData, string curConnectIp, out string err)
		{
			err = "";
			reData = null;
			// 参数判断
			if (string.IsNullOrEmpty(nameEn) || string.IsNullOrEmpty(curConnectIp))
			{
				err = "nameEn is null , or connectIp is null";
				return false;
			}

			lock (_syncLock)
			{
				if (!ipToMibInfoMap.ContainsKey(curConnectIp))
				{
					err = $"IP{curConnectIp}的数据库尚未初始化";
					return false;
				}

				var mib = ipToMibInfoMap[curConnectIp];
				reData = mib.getNameEnInfo(nameEn);
				if (null == reData)
				{
					err = $"未找到名为{nameEn}的MIB信息";
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// [查询]通过节点英文名字，查询节点信息。支持多节点查找。
		/// </summary>
		/// <param name="reData">其中key为查询的英文名(需要输入)，value为对应的节点信息。</param>
		/// <param name="connectIp">信息归属的标识</param>
		/// <param name="err">查询失败的原因</param>
		/// <returns></returns>
		public bool getDataByEnglishName(Dictionary<string, MibLeaf> reData, string connectIp, out string err)
		{
			err = "";

			if (reData == null || reData.Keys.Count == 0 || string.IsNullOrEmpty(connectIp))
			{
				err = "nameEn is null , or connectIp is null";
				return false;
			}

			// 获取keys
			var dtKeys = new string[reData.Keys.Count];//.Copy(reData.Keys, dtKeys, 0);
			reData.Keys.CopyTo(dtKeys, 0);

			// 查询
			MibLeaf getNameInfo = null;
			foreach (var key in dtKeys)
			{
				if (!getDataByEnglishName(key, out getNameInfo, connectIp,out  err))
				{
					return false;
				}
				reData[key] = getNameInfo;
			}

			return true;
		}

		public bool GetMibDataByOid(string oid, out MibLeaf reData, string curConnectIp, out string err)
		{
			reData = null;
			err = "";

			if (string.IsNullOrEmpty(oid) || string.IsNullOrEmpty(curConnectIp))
			{
				err = "nameEn is null , or connectIp is null";
				return false;
			}

			lock (_syncLock)
			{
				if (!ipToMibInfoMap.ContainsKey(curConnectIp))
				{
					err = $"IP{curConnectIp}的数据库尚未初始化";
					return false;
				}

				var mib = ipToMibInfoMap[curConnectIp];
				reData = mib.getOidEnInfo(oid);
				if (null == reData)
				{
					err = $"未找到OID为{oid}为MIB信息";
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 通过oid获取mib节点信息
		/// </summary>
		/// <param name="oid">不带前缀的oid</param>
		/// <param name="targetIp">目标基站地址</param>
		/// <returns>null:获取失败</returns>
		public MibLeaf GetMibDataByOid(string oid, string targetIp)
		{
			MibLeaf targetLeaf;
			string err;
			if (GetMibDataByOid(oid, out targetLeaf, targetIp, out err))
			{
				return targetLeaf;
			}
			return null;
		}

		public bool GetMibDataByOids(Dictionary<string, MibLeaf> reData, string connectIp, out string err)
		{
			err = "";
			// 参数判断
			if (reData == null || reData.Keys.Count == 0 || string.IsNullOrEmpty(connectIp))
			{
				err = "reData , mibDb or connectIp is err.";
				return false;
			}

			// 获取keys
			var dtKeys = new string[reData.Keys.Count];
			reData.Keys.CopyTo(dtKeys, 0);

			MibLeaf getOidInfo;
			foreach (var key in dtKeys)
			{
				if (!GetMibDataByOid(key, out getOidInfo, connectIp, out err))
					return false;

				reData[key] = getOidInfo;
			}

			return true;
		}

		/// <summary>
		/// 根据传入的表名，查询MIB信息
		/// </summary>
		/// <param name="nameEn"></param>
		/// <param name="reData"></param>
		/// <param name="curConnectIp"></param>
		/// <param name="err"></param>
		/// <returns>true:查询成功，false:查询失败</returns>
		public bool GetMibDataByTableName(string nameEn, out MibTable reData, string curConnectIp, out string err)
		{
			reData = null;
			err = "";

			if (string.IsNullOrEmpty(nameEn) || string.IsNullOrEmpty(curConnectIp))
			{
				err = "input err or mibDb is null";
				return false;
			}

			lock (_syncLock)
			{
				if (!ipToMibInfoMap.ContainsKey(curConnectIp))
				{
					err = $"IP为{curConnectIp}的数据库尚未初始化";
					return false;
				}

				var mib = ipToMibInfoMap[curConnectIp];
				reData = mib.getTableInfo(nameEn.Replace("Table", "Entry"));
				if (null == reData)
				{
					err = $"查找名为{nameEn}的表信息失败";
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 根据表名查询MIB数据，支持同时传入多个表名
		/// </summary>
		/// <param name="strMibName"></param>
		/// <param name="targetIp"></param>
		/// <returns>true:查询成功，false:查询失败</returns>
		public bool GetMibDataByTableName(Dictionary<string, MibTable> reData, string connectIp, out string err)
		{
			err = "";
			// 参数判断
			if (reData == null || reData.Keys.Count == 0 ||string.IsNullOrEmpty(connectIp))
			{
				err = "reData ,mibDb,  or connectIp is err.";
				return false;
			}
			// 获取keys
			var dtKeys = new string[reData.Keys.Count];
			reData.Keys.CopyTo(dtKeys, 0);

			// 查询
			MibTable getTable;
			foreach (var key in dtKeys)
			{
				if (!GetMibDataByTableName(key, out getTable, connectIp, out err))
					return false;

				reData[key] = getTable;
			}

			return true;
		}

		/// <summary>
		/// [查询]通过命令英文名字，查询命令信息。同时查询多个
		/// </summary>
		/// <param name="reData"></param>
		/// <param name="connectIp"></param>
		/// <param name="err"></param>
		/// <returns></returns>
		public bool GetCmdDataByNames(Dictionary<string, CmdMibInfo> reData, string connectIp, out string err)
		{
			err = "";

			// 参数判断
			if (reData == null || reData.Keys.Count == 0 || string.IsNullOrEmpty(connectIp))
			{
				err = "reData is null , reData keys count is 0 or connectIp is null.";
				return false;
			}

			lock (_syncLock)
			{
				if (!ipToCmdInfoMap.ContainsKey(connectIp))
				{
					return false;
				}

				var cmd = ipToCmdInfoMap[connectIp];

				return cmd.getCmdInfoByName(reData, out err);
			}
		}

		/// <summary>
		/// 通过命令英文名字，查询命令信息
		/// </summary>
		/// <param name="cmdName"></param>
		/// <param name="targetIp"></param>
		/// <returns></returns>
		public CmdMibInfo GetCmdDataByName(string cmdName, string targetIp)
		{
			if (string.IsNullOrEmpty(cmdName) || string.IsNullOrEmpty(targetIp))
			{
				//throw new ArgumentNullException();
				return null;
			}

			lock (_syncLock)
			{
				if (!ipToCmdInfoMap.ContainsKey(targetIp))
				{
					return null;
				}

				var cmd = ipToCmdInfoMap[targetIp];
				string err;
				CmdMibInfo cmdData = null;
				if (cmd.getCmdInfoByName(cmdName, out cmdData, out err))
				{
					return cmdData;
				}
			}

			return null;
		}

		/// <summary>
		/// 提供Trap所有类型的查找
		/// 现有10种。
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, Dictionary<string, string>> GetTrapInfo()
		{
			var allTrapInfo = new Dictionary<string, Dictionary<string, string>>() {
				{ "alarmNotify", new Dictionary<string, string>() {{ "TrapID","24"},{ "TrapOid","alarmTrap"},{ "TrapTypeDes","alarmTraps"}}},
				{ "anrNotification", new Dictionary<string, string>() {{ "TrapID","200"},{ "TrapOid", "anrNotification" },{ "TrapTypeDes", "anrNotification" } }},
				{ "alterationNofication", new Dictionary<string, string>() {{ "TrapID","21"},{ "TrapOid", "configChgTrap" },{ "TrapTypeDes", "eventConfigChgTraps" } }},
				{ "eventGeneralEventTrap", new Dictionary<string, string>() {{ "TrapID","20"},{ "TrapOid", "eventGeneralEventTrap" },{ "TrapTypeDes", "eventGeneralEventTraps" } }},
				{ "eventSynchronizationTrap", new Dictionary<string, string>() {{ "TrapID","26"},{ "TrapOid", "eventSynchronizationTrap" },{ "TrapTypeDes", "eventSynchronizationTrap" } }},
				{ "fileTransNotification", new Dictionary<string, string>() {{ "TrapID","23"},{ "TrapOid", "fileTransTrap" },{ "TrapTypeDes", "eventFTPResultTraps" } }},
				{ "maintenceStateNotify", new Dictionary<string, string>() {{ "TrapID","203"},{ "TrapOid", "maintenceStateNotify" },{ "TrapTypeDes", "maintenceStateNotify" } }},
				{ "managementRequestTrap", new Dictionary<string, string>() {{ "TrapID","22"},{ "TrapOid", "managementRequestTrap" },{ "TrapTypeDes", "eventManagementRequestTraps" } }},
				{ "mroNotification", new Dictionary<string, string>() {{ "TrapID","201"},{ "TrapOid", "mroNotification" },{ "TrapTypeDes", "mroNotification" } }},
				{ "transactionResultNotification", new Dictionary<string, string>() {{ "TrapID","25"},{ "TrapOid", "transactionResultTrap" },{ "TrapTypeDes", "transactionResultTraps" } }},
			};
			return allTrapInfo;
		}

		#endregion

		#region 私有数据、属性区

		// 保存每个IP对应的MIB信息
		private Dictionary<string, MibInfoList> ipToMibInfoMap;

		//保存每个IP对应的CMD信息
		private Dictionary<string, CmdInfoList> ipToCmdInfoMap;

		private static Database _instance = null;   // 数据库实例
		private static readonly object _syncLock = new object();	// 数据库同步锁

		#endregion


		#region 私有方法区

		// 构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		private Database()
		{
			ipToMibInfoMap = new Dictionary<string, MibInfoList>();
			ipToCmdInfoMap = new Dictionary<string, CmdInfoList>();
		}

		/// <summary>
		/// 真实执行初始化内容的函数。
		/// </summary>
		/// <param name="connectIp"> 标识数据的归属，查询要用 </param>
		private void DBInitDateBaseByIpConnect(object connectIp)
		{
			Console.WriteLine("Db init : Start..., time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
			// 1. 解压lm.dtz
			if (!DBInitZip())
			{
				Console.WriteLine("Db init : zip err ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
				resultInitData(false);
				return;
			}
			//Console.WriteLine("Db init zip ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

			// 2. 解析lm.dtz => 写json文件(增加，叶子节点的读写属性) 解析.mdb文件
			if (!DBInitParseMdbToWriteJson())
			{
				Console.WriteLine("Db init : writejson err ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
				resultInitData(false);
				return;
			}
			//Console.WriteLine("Db init parse mdb ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

			// 3. 解析json 文件
			if (!DBInitParseJsonToMemory(connectIp.ToString()))
			{
				Console.WriteLine("Db init : mib/cmd list err. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
				resultInitData(false);
				return;
			}
			//Console.WriteLine("Db init : mib/cmd list ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

			// 4. 结果
			Console.WriteLine("Db init : Ok..., time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
			resultInitData(true);
		}

		/// 1. 解压lm.dtz
		private bool DBInitZip()
		{
			string err = "";
			UnzippedLmDtz unZip = new UnzippedLmDtz();
			if (!unZip.UnZipFile(out err))
			{
				resultInitData(false);
				Console.WriteLine("Err : DBInitZip fail, {0}", err);
				return false;
			}
			return true;
		}
		/// 2. 解析lm.mdb,写json文件; 解析lm.dtz => json文件(增加，叶子节点的读写属性) 解析.mdb文件
		private bool DBInitParseMdbToWriteJson()
		{
			// TODO 5.10.11是什么？
			JsonDataManager jdm = new JsonDataManager("5.10.11");
			return jdm.ConvertAccessDbToJsonForThread();
		}
		/// 3. 解析json文件到内存中
		private bool DBInitParseJsonToMemory(string connectIp)
		{
			var mibL = new MibInfoList();// mib 节点

			if (!mibL.GeneratedMibInfoList())
				return false;

			var cmdL = new CmdInfoList();// cmd 节点
			if (!cmdL.GeneratedCmdInfoList())
				return false;

			lock (_syncLock)
			{
				// 如果已经有了该IP对应的数据，重新设置指向的对象即可
				if (ipToMibInfoMap.ContainsKey(connectIp))
				{
					ipToMibInfoMap[connectIp] = mibL;
				}
				else
				{
					ipToMibInfoMap.Add(connectIp, mibL);
				}

				if (ipToCmdInfoMap.ContainsKey(connectIp))
				{
					ipToCmdInfoMap[connectIp] = cmdL;
				}
				else
				{
					ipToCmdInfoMap.Add(connectIp, cmdL);
				}
			}

			return true;
		}
		#endregion
	}


}
