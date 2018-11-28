using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
//using CommonUtility;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace MIBDataParser.JSONDataMgr
{
	public class CmdMibInfo
	{
		public string m_cmdNameEn { get;} // 命令的英文名
		public string m_tableName { get;  } // 命令的mib表英文名
		public string m_cmdType { get; } //命令类型
		public string m_cmdDesc { get; } //命令描述
		public List<string> m_leaflist { get; } // 命令节点oid
        public Dictionary<string, string> m_leafDefault { get; }//默认值获取{key:Oid,Value:DefaultVal}


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
            if (value["leafOIdListDefault"].ToList().Count() == 2)
            {
                var leafDefault = value["leafOIdListDefault"].ToList();

                string oid = leafDefault[0].ToList()[0].ToString();
                string val = leafDefault[1].ToList()[0].ToString();
                this.m_leafDefault = new Dictionary<string, string>();

                this.m_leafDefault.Add(oid, val);
            }
        }

		public CmdMibInfo()
		{
			m_leaflist = new List<string>();
		}
	}

	public class TrapTypeDef
	{
		public string MibName { get; }
		public int TrapID { get; }
		public string TrapDesc { get; }

		public TrapTypeDef(string strMibName, int nTrapId, string strDesc)
		{
			MibName = strMibName;
			TrapID = nTrapId;
			TrapDesc = strDesc;
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
		public async Task<bool> initDatabase(string connectIp)
		{
			try
			{
				//new Thread(DBInitDateBaseByIpConnect).Start(connectIp);

				var task = Task<bool>.Factory.StartNew(() => DBInitDateBaseByIpConnect(connectIp));
				Task.WaitAll(task);
				return task.Result;
			}
			catch
			{
				//resultInitData(false);
				return false;
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

		public MibTable GetMibDataByTableName(string strTblName, string targetIp)
		{
			MibTable tbl;
			string err;
			if (GetMibDataByTableName(strTblName, out tbl, targetIp, out err))
			{
				return tbl;
			}
			return null;
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

		public List<CmdMibInfo> GetCmdsInfoByEntryName(string strEntryName, string targetIp)
		{
			lock (_syncLock)
			{
				if (!ipToCmdInfoMap.ContainsKey(targetIp))
				{
					return null;
				}

				var cmdInstance = ipToCmdInfoMap[targetIp];
				return cmdInstance?.GetCmdsByTblName(strEntryName);
			}
		}

		/// <summary>
		/// 提供Trap所有类型的查找
		/// 现有10种。
		/// </summary>
		/// <returns></returns>
		public List<TrapTypeDef> GetTrapInfo()
		{
			var trapInfoList = new List<TrapTypeDef>();
			var trap = new TrapTypeDef("alarmNotify", 24, "告警Trap");
			trapInfoList.Add(trap);

			trap = new TrapTypeDef("anrNotification", 200, "ANR事件");
			trapInfoList.Add(trap);

			trap = new TrapTypeDef("alterationNofication", 21, "通用事件Trap");
			trapInfoList.Add(trap);

			trap = new TrapTypeDef("eventGeneralEventTrap", 20, "通用事件Trap");
			trapInfoList.Add(trap);

			trap = new TrapTypeDef("eventSynchronizationTrap", 26, "数据同步事件通知");
			trapInfoList.Add(trap);

			trap = new TrapTypeDef("fileTransNotification", 23, "文件传输结果通知");
			trapInfoList.Add(trap);

			trap = new TrapTypeDef("maintenceStateNotify", 203, "工程状态通知");
			trapInfoList.Add(trap);

			trap = new TrapTypeDef("managementRequestTrap", 22, "管理请求通知");
			trapInfoList.Add(trap);

			trap = new TrapTypeDef("mroNotification", 201, "MRO事件通知");
			trapInfoList.Add(trap);

			trap = new TrapTypeDef("transactionResultNotification", 25, "事务结果通知");
			trapInfoList.Add(trap);

			return trapInfoList;
		}

		public int GetTrapInfoByMibName(string strMibName)
		{
			var til = GetTrapInfo();

			foreach (var item in til)
			{
				if (item.MibName == strMibName)
				{
					return item.TrapID;
				}
			}

			return -1;
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
		private bool DBInitDateBaseByIpConnect(object connectIp)
		{
			Console.WriteLine("Db init : Start..., time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
			// 1. 解压lm.dtz
			if (!DBInitZip(connectIp.ToString()))
			{
				Console.WriteLine("Db init : zip err ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
				//resultInitData(false);
				return false;
			}
			//Console.WriteLine("Db init zip ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

			// 2. 解析lm.dtz => 写json文件(增加，叶子节点的读写属性) 解析.mdb文件
			var bSucceed = DBInitParseMdbToWriteJson();
			if (!bSucceed.Result)
			{
				Console.WriteLine("Db init : writejson err ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
				//resultInitData(false);
				return false;
			}
			//Console.WriteLine("Db init parse mdb ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

			// 3. 解析json 文件
			if (!DBInitParseJsonToMemory(connectIp.ToString()))
			{
				Console.WriteLine("Db init : mib/cmd list err. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
				//resultInitData(false);
				return false;
			}
			//Console.WriteLine("Db init : mib/cmd list ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

			// 4. 结果
			Console.WriteLine("Db init : Ok..., time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
			//resultInitData(true);
			return true;
		}

		/// 1. 解压lm.dtz
		private bool DBInitZip(string connectIp)
		{
			string err = "";
			UnzippedLmDtz unZip = new UnzippedLmDtz();
			if (!unZip.UnZipFile(connectIp, out err))
			{
				//resultInitData(false);
				Console.WriteLine("Err : DBInitZip fail, {0}", err);
				return false;
			}
			return true;
		}
		/// 2. 解析lm.mdb,写json文件; 解析lm.dtz => json文件(增加，叶子节点的读写属性) 解析.mdb文件
		private async Task<bool> DBInitParseMdbToWriteJson()
		{
			var jdm = new JsonDataManager("5.10.11");
			var retTask = await jdm.ConvertAccessDbToJsonForThread().ConfigureAwait(false);
			return retTask;
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
