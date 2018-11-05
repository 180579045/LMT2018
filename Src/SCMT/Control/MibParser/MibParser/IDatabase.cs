using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MIBDataParser.JSONDataMgr;

namespace MIBDataParser
{
	#region 定义json类

	//顶级MIB INFO类，保存所有的mib.json文件中信息
	public class MibTree
	{
		public string mibVersion;
		public int tableNum;
		public List<MibTable> tableList;

		public MibTree()
		{
			tableList = new List<MibTable>();
		}
	}

	// mib table类定义
	public class MibTable
	{
		public string nameMib;
		public string oid;		// 表入口oid，不带前缀
		public string nameCh;
		public int indexNum;
		public string mibSyntax;
		public string mibDesc;
		public List<MibLeaf> childList;

		public MibTable()
		{
			childList = new List<MibLeaf>();
		}
	}

	// mib leaf 类定义
	public class MibLeaf
	{
		public string childNameMib;
		public int childNo;
		public string childOid;
		public string childNameCh;
		public int isMib;
		public string ASNType;
		public string OMType;
		public int UIType;
		public string managerValueRange;
		public string defaultValue;
		public string detailDesc;
		public int leafProperty;
		public string unit;
		public string IsIndex;	//取值：True False
		public string mibSyntax;
		public string mibDesc;
		public bool IsTable = false;
		public string managerWriteAble;

		#region 构造函数
		public MibLeaf()
		{
			IsTable = false;
		}

		// 传入mibtable，构造一个leaf对象，目的是便于使用名称进行搜索
		public MibLeaf(MibTable mt)
		{
			childNameCh = mt.nameCh;
			childOid = mt.oid;
			childNameMib = mt.nameMib;
			mibSyntax = mt.mibSyntax;
			mibDesc = mt.mibDesc;
			IsTable = true;
		}

		#endregion

		#region 公共接口

		/// <summary>
		/// 是否被授权可修改该属性
		/// </summary>
		/// <returns></returns>
		public bool IsEmpoweredModify()
		{
			// 管理站可写属性中，如果有w，则认为可以修改
			if (managerWriteAble.IndexOf("w", StringComparison.Ordinal) >= 0)
			{
				return true;
			}

			return false;
		}

		#endregion

	}

	#endregion

	/// <summary>
	/// 委托 : 初始化Database 结果
	/// </summary>
	/// <param name=result>初始化成功,true;失败,false</param>
	public delegate void ResultInitData(bool result);

	/// <summary>
	/// 操作数据库接口 : 初始化, 查询. 没有修改的接口.
	/// </summary>
	public interface IDatabase
	{
		/// <summary>
		/// 获取单实例数据库句柄
		/// </summary>
		/// <returns>Database</returns>
		/// Database GetInstance();

		/// <summary>
		/// 初始化的线程 : 初始化(1.解压lm.dtz;2.解析.mdb;3.解析json;)
		/// </summary>
		/// <param name=connectIp>信息归属的标识</param>
		Task<bool> initDatabase(string connectIp);

		/// <summary>
		/// 删除断链的ip的数据库
		/// </summary>
		/// <param name=connectIp>信息归属的标识</param>
		/// <returns></returns>
		bool delDatabase(string connectIp);

		/// <summary>
		/// 返回初始化 initDatabase 的结果。void ResultInitData(bool result);
		/// </summary>
		/// <param name=result>true: 初始化成功； flase: 初始化失败。</param>
		/// <returns>void</returns>
		/// ResultInitData resultInitData;

		///查询数据
		/// <summary>
		/// [查询]通过节点英文名字，查询节点信息。支持多节点查找。
		/// </summary>
		/// <param name=reData>其中key为查询的英文名(需要输入)，value为对应的节点信息。</param>
		/// <param name=connectIp>信息归属的标识</param>
		/// <param name=err>查询失败的原因</param>
		/// <returns></returns>
		bool getDataByEnglishName(Dictionary<string, MibLeaf> reData, string connectIp, out string err);

		/// <summary>
		/// [查询]通过节点英文名字，查询节点信息。支持单节点查找。
		/// </summary>
		/// <param name=nameEn>节点英文名字</param>
		/// <param name=reData>节点信息</param>
		/// <param name=curConnectIp>信息归属的标识</param>
		/// <returns></returns>
		bool getDataByEnglishName(string nameEn, out MibLeaf reData, string curConnectIp, out string err);

		/// <summary>
		/// [查询]通过节点oid，查询节点信息。支持多节点查找。
		/// </summary>
		/// <param name=reData>其中key为查询的oid(需要输入)，value为对应的节点信息。</param>
		/// <param name=connectIp>信息归属的标识</param>
		/// <param name=err>查询失败的原因</param>
		/// <returns></returns>
		bool GetMibDataByOids(Dictionary<string, MibLeaf> reData, string connectIp, out string err);

		/// <summary>
		/// [查询]通过表的英文名，查询表的信息。支持多表查找。
		/// </summary>
		/// <param name=reData>其中key为查询的表名(需要输入)，value为对应的表的信息。</param>
		/// <param name=connectIp>信息归属的标识</param>
		/// <param name=err>查询失败的原因</param>
		/// <returns></returns>
		bool GetMibDataByTableName(Dictionary<string, MibTable> reData, string connectIp, out string err);

		/// <summary>
		/// [查询]通过命令的英文名，查询命令的信息。支持多个命令查找。
		/// </summary>
		/// <param name=reData>其中key为查询的命令英文名(需要输入)，value为对应的命令的信息。</param>
		/// <param name=connectIp>信息归属的标识</param>
		/// <param name=err>查询失败的原因</param>
		/// <returns></returns>
		bool GetCmdDataByNames(Dictionary<string, CmdMibInfo> reData, string connectIp, out string err);

		/// <summary>
		/// 获取所有的trap 类型
		/// Dictionary<string, Dictionary<string, string>> ; Dictionary<每个TrapOid_ENB5216, 其他信息Dictionary(TrapID,TrapOid,TrapTypeDes)>
		/// Dictionary<string, string> : Dictionary({ TrapID:value},{TrapOid:value},{TrapTypeDes:value})
		/// </summary>
		/// <returns>10组trap</returns>
		Dictionary<string, Dictionary<string, string>> GetTrapInfo();

		/* 为 cfg 操作模块提供相关接口 */
		/// <summary>
		/// 解压lmdtz文件到指定目录下
		/// </summary>
		/// <param name=strFileToUnzip>目标dtz文件</param>
		/// <param name=strFileToDirectory>解压释放目录</param>
		/// <param name=err></param>
		/// <returns></returns>
        /// bool cfgUnzipDtz(string strFileToUnzip, string strFileToDirectory, out string err);

		/// <summary>
		/// 通过 sql 语句获取 数据
		/// </summary>
		/// <param name=fileName></param>
		/// <param name=sqlContent></param>
		/// <returns></returns>
		//Object cfgGetRecordByAccessDb(string fileName, string sqlContent);
	}
}

