using CommonUtility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using LogManager;

namespace MIBDataParser.JSONDataMgr
{
	internal class NameEnInfo
	{
		public string m_oid;
		public bool m_isLeaf;
		public int m_indexNum;
		public string m_nameCh;
		public string m_tableNameEn;
		public List<NameEnInfo> m_sameNameEn;

		public NameEnInfo()
		{
		}

		public NameEnInfo(bool isLeaf, dynamic table, dynamic child)
		{
			this.m_isLeaf = isLeaf;
			this.m_indexNum = int.Parse(table["indexNum"].ToString());
			this.m_tableNameEn = table["nameMib"].ToString();
			if (isLeaf)//Leaf
			{
				this.m_oid = child["childOid"].ToString();
				this.m_nameCh = child["childNameCh"].ToString();
			}
			else// table
			{
				this.m_oid = table["oid"].ToString();
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

	internal class OidInfo
	{
		public bool m_isLeaf;
		public int m_indexNum;
		public string m_nameEn;
		public string m_nameCh;
		public string m_tableNameEn;

		public OidInfo()
		{
		}

		public OidInfo(bool isLeaf, dynamic table, dynamic child)
		{
			this.m_isLeaf = isLeaf;
			this.m_indexNum = int.Parse(table["indexNum"].ToString());
			this.m_tableNameEn = table["nameMib"].ToString();
			if (isLeaf)
			{
				this.m_nameEn = child["childNameMib"].ToString();
				this.m_nameCh = child["childNameCh"].ToString();
			}
			else
			{
				this.m_nameEn = table["nameMib"].ToString();
				this.m_nameCh = table["nameCh"].ToString();
			}
		}
	}

	internal class MibInfoList
	{
		#region 私有数据区

		private Dictionary<string, MibLeaf> nameToMib;
		private Dictionary<string, MibLeaf> oidToMib;
		private Dictionary<string, List<MibLeaf>> mibsWithSameName;
		private MibTree mibTree;

		#endregion 私有数据区

		/*************************************        公共接口实现       ********************************************/

		public bool GeneratedMibInfoList()
		{
			nameToMib = new Dictionary<string, MibLeaf>();
			oidToMib = new Dictionary<string, MibLeaf>();
			mibsWithSameName = new Dictionary<string, List<MibLeaf>>();

			string err = "";
			var jsonfilepath = ReadIniFile.IniReadValue(
				ReadIniFile.GetIniFilePath("JsonDataMgr.ini", out err), "JsonFileInfo", "jsonfilepath");

			try
			{
				if (String.Empty == jsonfilepath)
				{
					Log.Error("GeneratedMibInfoList err: IniReadValue err.");
					return false;
				}
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

			if (oidToMib.ContainsKey(findKey))
			{
				oidInfo = oidToMib[findKey];
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