using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading;
using CommonUtility;

namespace MIBDataParser.JSONDataMgr
{
	class NameEnInfo
	{
		public string m_oid;
		public bool m_isLeaf;
		public int m_indexNum;
		public string m_nameCh;
		public string m_tableNameEn;
		public List<NameEnInfo> m_sameNameEn;

		public NameEnInfo(){}
		public NameEnInfo(bool isLeaf, dynamic table, dynamic child)
		{
			this.m_isLeaf = isLeaf;
			this.m_indexNum = int.Parse(table["indexNum"].ToString());
			this.m_tableNameEn = table["nameMib"].ToString();
			if (isLeaf)//Leaf
			{
				this.m_oid    = child["childOid"].ToString();
				this.m_nameCh = child["childNameCh"].ToString();
			}
			else// table
			{
				this.m_oid    = table["oid"].ToString();
				this.m_nameCh = table["nameCh"].ToString();
			}
		}
		public void AddSameNameEnInfo(NameEnInfo nameInfo)
		{
			if (this.m_sameNameEn == null)
			{
				this.m_sameNameEn = new List<NameEnInfo>();
			}
			this.m_sameNameEn.Add(nameInfo);
		}
	}
	class OidInfo
	{
		public bool m_isLeaf;
		public int m_indexNum;
		public string m_nameEn;
		public string m_nameCh;
		public string m_tableNameEn;

		public OidInfo() { }
		public OidInfo(bool isLeaf, dynamic table, dynamic child)
		{
			this.m_isLeaf = isLeaf;
			this.m_indexNum = int.Parse(table["indexNum"].ToString());
			this.m_tableNameEn = table["nameMib"].ToString();
			if (isLeaf){
				this.m_nameEn = child["childNameMib"].ToString();
				this.m_nameCh = child["childNameCh"].ToString();
			}
			else {
				this.m_nameEn = table["nameMib"].ToString();
				this.m_nameCh = table["nameCh"].ToString();
			}
		}
	}
	class LeafInfo
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
		public string IsIndex;
		public string mibSyntax;
		public string mibDesc;
	}
	class TableInfo
	{
		public string nameMib;
		public string oid;
		public string nameCh;
		public int indexNum;
		public string mibSyntax;
		public string mibDesc;
		public List<LeafInfo> childList = new List<LeafInfo>();
	}

	class MibInfoList
	{
		#region 私有数据区

		private Dictionary<string, MibLeaf> nameToMib;
		private Dictionary<string, MibLeaf> oidToMib;
		private Dictionary<string, List<MibLeaf>> mibsWithSameName;
		private MibTree mibTree;

		#endregion

		/*************************************        公共接口实现       ********************************************/

		public bool GeneratedMibInfoList()
		{
			nameToMib = new Dictionary<string, MibLeaf>();
			oidToMib = new Dictionary<string, MibLeaf>();
			mibsWithSameName = new Dictionary<string, List<MibLeaf>>();

			var jsonfilepath = ReadIniFile.IniReadValue(
				ReadIniFile.GetIniFilePath("JsonDataMgr.ini"), "JsonFileInfo", "jsonfilepath");

			try
			{
				var mibJsonFilePath = jsonfilepath + "mib.json";
				var mibJsonContent = FileRdWrHelper.GetFileContent(mibJsonFilePath, Encoding.GetEncoding("gb2312"));
				mibTree = JsonHelper.SerializeJsonToObject<MibTree>(mibJsonContent);

				//把mibtree分成2份，分别对应mib名和oid字符串，便于查找
				// table也认为是一个leaf节点
				foreach (var mibTemp in mibTree.tableList)
				{
					var tableLeaf = new MibLeaf(mibTemp);

					HandleSameNameMib(mibTemp.nameMib, tableLeaf);
					if (oidToMib.ContainsKey(mibTemp.oid))
					{
						var cname = mibTemp.nameMib;
						var existname = oidToMib[mibTemp.oid].childNameMib;
						var eoid = oidToMib[mibTemp.oid].childOid;
						Debug.WriteLine($"已存在MIB：name={existname},oid={eoid},新添加节点：name={cname},oid={mibTemp.oid}");
					}
					else
					{
						oidToMib.Add(mibTemp.oid, tableLeaf);
					}

					foreach (var mibChild in mibTemp.childList)
					{
						HandleSameNameMib(mibChild.childNameMib, mibChild);
						if (oidToMib.ContainsKey(mibChild.childOid))
						{
							var cname = mibChild.childNameMib;
							var existname = oidToMib[mibChild.childOid].childNameMib;
							var eoid = oidToMib[mibChild.childOid].childOid;
							Debug.WriteLine($"已存在MIB：name={existname},oid={eoid},新添加节点：name={cname},oid={mibChild.childOid}");
						}
						else
						{
							oidToMib.Add(mibChild.childOid, mibChild);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return true;
		}

		// 遇到MIB名相同的情况，保存到另外一个字典中
		private void HandleSameNameMib(string mibName, MibLeaf newNode)
		{
			if (nameToMib.ContainsKey(mibName))
			{
				if (mibsWithSameName.ContainsKey(mibName))
				{
					mibsWithSameName[mibName].Add(newNode);
				}
				else
				{
					var listLeaf = new List<MibLeaf>();
					mibsWithSameName.Add(mibName, listLeaf);
				}
				Console.WriteLine($"已有名为{mibName}MIB信息");
			}
			else
			{
				nameToMib.Add(mibName, newNode);
			}
		}

		/// <summary>
		/// 用节点的英文名，查询节点信息
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public MibLeaf getNameEnInfo(string key)
		{
			if (!nameToMib.ContainsKey(key))
				return null;

			return nameToMib[key];
		}

		public MibLeaf getOidEnInfo(string key)
		{
			// 处理1. 去前缀
			int indexNum = 0;
			string findKey = key.Replace("1.3.6.1.4.1.5105.100.", "");
			MibLeaf oidInfo = null;
			while (findKey.Count(ch => ch == '.') > 4)
			{
				if (!oidToMib.ContainsKey(findKey))
				{
					findKey = findKey.Substring(0, findKey.LastIndexOf("."));
					indexNum += 1;
				}
				else
				{
					oidInfo = oidToMib[findKey];
					break;
					// TODO 此处暂不处理
					//// 表量表有索引，索引个数应该与移位个数相同
					//if (indexNum == oidInfo.m_indexNum)
					//{
					//	break;
					//}
					//// 标量表无索引，但最后一位以".0"占位 例如"1.3.6.1.4.1.5105.100.1.5.2.1.2.4.0" alterationNotiTime 配置变更时间
					//else if ((0 == oidInfo.m_indexNum) && (indexNum == oidInfo.m_indexNum + 1))
					//{
					//	break;
					//}
					//// 索引为唯一OID列表，不能有重复oid存在
					//else
					//{
					//	oidInfo = null;
					//	break;
					//}
				}
			}

			return oidInfo;
		}

		public MibTable getTableInfo(string key)
		{
			var tableList = mibTree.tableList;
			return tableList.FirstOrDefault(table => table.nameMib.Equals(key));
		}
		/*************************************        公共接口实现       ********************************************/
	}
}
