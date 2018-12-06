using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace MIBDataParser.JSONDataMgr
{
	// 保存MIB表中表量表的信息
	internal class MibTableInfo
	{
		public List<int> childIndex;        // 保存子项节点的索引号
		public int index;                   // table的索引号

		public MibTableInfo()
		{
			childIndex = new List<int>();
		}
	}

	public class MibJsonData
	{
		//string stringMibJson;
		private JObject mibJObject; // mib节点，表

		//JArray mibJArray;
		private int tableNum;

		private string mibVersion;

		public MibJsonData()
		{
			mibJObject = new JObject();
			tableNum = 0;
		}

		public MibJsonData(string mibVersion)
		{
			mibJObject = new JObject();
			tableNum = 0;
			this.mibVersion = mibVersion;
		}

		public string GetStringMibJson()
		{
			return mibJObject.ToString();
		}

		/// <summary>
		/// 重构前：原始方案是按靠近的两个信息进行比对的，确定父子关系
		/// 重构后：扫描所有的数据，如果是table，就填入字典;
		///		如果是leaf，就在字典中查找，并填入leaf的索引号。此索引不是MIB中的索引
		/// </summary>
		/// <param name="MibdateSet"></param>
		public void MibParseDataSet(DataSet MibdateSet)
		{
			// key:oid
			var mapTableToLeafIndex = new Dictionary<string, MibTableInfo>();

			// key:leaf节点索引号 value：parent oid
			var mapNotFoundParentLeaf = new Dictionary<int, string>();  // 保存未找到父节点的leaf节点索引号

			var table = MibdateSet.Tables[0];
			var tableCount = table.Rows.Count;

			for (var loop = 0; loop < tableCount; loop++)
			{
				var dataRow = table.Rows[loop];
				var bIsLeaf = dataRow["IsLeaf"].ToString().Equals("true", StringComparison.OrdinalIgnoreCase);

				if (bIsLeaf)        // 如果是叶子节点，就尝试找到父节点，建立起连接管理；
				{
					var poid = dataRow["ParentOID"].ToString();
					if (mapTableToLeafIndex.ContainsKey(poid))      // 如果已经添加过枝干节点，就把索引值填入到childIndex中
					{
						mapTableToLeafIndex[poid].childIndex.Add(loop);
					}
					else
					{
						mapNotFoundParentLeaf.Add(loop, poid);  // 没有找到枝干节点，先保存叶子节点的索引
					}
				}
				else
				{
					// 枝干节点不再寻找父节点
					var oid = dataRow["OID"].ToString();
					if (mapTableToLeafIndex.ContainsKey(oid))
					{
						Debug.WriteLine($"oid:{oid} 已经存在，index:{mapTableToLeafIndex[oid].index}, cur index:{loop}");
					}
					else
					{
						mapTableToLeafIndex.Add(oid, new MibTableInfo() { index = loop });
					}
				}
			}

			var listAloneLeaf = new List<int>();        // 真正孤立的节点。TODO 这种情况如何处理？
														// 扫描没有找到父节点的leaf节点列表，建立完整的父子关系
			foreach (var leafIndex in mapNotFoundParentLeaf)
			{
				var poid = leafIndex.Value;
				if (mapTableToLeafIndex.ContainsKey(poid))
				{
					mapTableToLeafIndex[poid].childIndex.Add(leafIndex.Key);
				}
				else
				{
					listAloneLeaf.Add(leafIndex.Key);
				}
			}

			var mibJArray = new JArray();

			// 遍历mapTableToLeafIndex，生成信息
			foreach (var ti in mapTableToLeafIndex)
			{
				var mti = ti.Value;
				if (mti.childIndex.Count > 0)       // 不保存没有叶子节点的节点
				{
					mibJArray.Add(ConvertDbToJson(table, mti));
				}
			}

			mibJObject.Add("mibVersion", mibVersion);
			mibJObject.Add("tableNum", this.tableNum);
			mibJObject.Add("tableList", mibJArray);
		}

		private JObject ConvertDbToJson(DataTable dt, MibTableInfo mti)
		{
			if (null == dt || null == mti)
			{
				return null;
			}

			var childrenIndex = mti.childIndex;
			var pindex = mti.index;
			var pData = dt.Rows[pindex];
			var pOneChild = dt.Rows[childrenIndex[0]];      //取出第一个子节点的数据，用于判断有几个索引
			var jParent = TableToJsonDataResolveTable(pData, pOneChild);
			var jChildList = new JArray();
			foreach (var cIndex in childrenIndex)
			{
				var pChild = dt.Rows[cIndex];
				LeafToJsonData(jParent, jChildList, pChild);
			}
			jParent.Add("childList", jChildList);

			return jParent;
		}

		/// <summary>
		/// 解析 table,在TableToJsonData中。
		/// </summary>
		/// <param name="rowRec"></param>
		/// <param name="indexNum"></param>
		/// <returns></returns>
		private JObject TableToJsonDataResolveTable(DataRow rowRec, DataRow rowRecNext)
		{
			return new JObject {
				{ "nameMib", rowRec["MIBName"].ToString()},
				{ "oid", rowRec["OID"].ToString()},
				{ "nameCh", rowRec["ChFriendName"].ToString()},
				{ "indexNum", TableToJsonDataResolveIndexNum(rowRecNext)},
				{ "mibSyntax", rowRec["MIB_Syntax"].ToString()},
				{ "mibDesc", rowRec["MIBDesc"].ToString()},
			};
		}

		/// <summary>
		/// 解析索引个数，在TableToJsonData中。
		/// </summary>
		/// <param name="rowRecNext"></param>
		/// <returns></returns>
		private int TableToJsonDataResolveIndexNum(DataRow rowRecNext)
		{
			int indexNum = 0;
			if (null != rowRecNext)
			{
				foreach (var index in new List<string> { "Index1OID", "Index2OID", "Index3OID", "Index4OID", "Index5OID", "Index6OID" })
				{
					if ("" != rowRecNext[index].ToString())
					{
						indexNum++;
					}
					else
					{
						break;
					}
				}
			}
			return indexNum;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="childJArray"></param>
		/// <param name="rowRec"></param>
		/// <param name="leafIndex"></param>
		private bool LeafToJsonData(JObject table, JArray childJArray, DataRow rowRec)
		{
			int isMib = 0;
			int UIType = 0;

			// 判断父子关系
			if (!rowRec["ParentOID"].ToString().Equals(table["oid"].ToString()))
				return false;

			if (0 == string.Compare("√", rowRec["IsMIB"].ToString()))//假MIB不写入
				isMib = 1;

			if (0 == string.Compare("enum", rowRec["OMType"].ToString()))
				UIType = 1;//单选下拉框
			else if (0 == string.Compare("DateAndTime", rowRec["OMType"].ToString()))
				UIType = 2;//日期
			else if (0 == string.Compare("BITS", rowRec["OMType"].ToString()))
				UIType = 3;//复选框

			var leafIndex = int.Parse(rowRec["OID"].ToString().Replace(rowRec["ParentOID"] + ".", ""));
			var childJObject = new JObject {
				{ "childNameMib", rowRec["MIBName"].ToString()},
				{ "childNo", leafIndex},
				{ "childOid",rowRec["OID"].ToString()},
				{ "childNameCh", rowRec["ChFriendName"].ToString()},
				{ "isMib", isMib},
				{ "ASNType", rowRec["ASNType"].ToString()},
				{ "OMType", rowRec["OMType"].ToString()},
				{ "UIType", UIType},
				{ "managerValueRange", rowRec["MIBVal_list"].ToString()},
				{ "defaultValue", rowRec["DefaultValue"].ToString()},
				{ "detailDesc", rowRec["chDetailDesc"].ToString()},
				{ "leafProperty", 0},
				{ "unit", rowRec["MIBVal_Unit"].ToString()},
				{ "IsIndex", rowRec["IsIndex"].ToString()},
				{ "mibSyntax", rowRec["MIB_Syntax"].ToString()},
				{ "mibDesc", rowRec["MIBDesc"].ToString()},
				{ "managerWriteAble", rowRec["ManagerWriteAble"].ToString() },
				{ "mibValAllListEng", rowRec["MIBVal_AllList_Eng"].ToString()},//英文
                { "mibValAllList", rowRec["MIBVal_AllList"].ToString()},//中文
			};
			childJArray.Add(childJObject);
			return true;
		}
	}
}