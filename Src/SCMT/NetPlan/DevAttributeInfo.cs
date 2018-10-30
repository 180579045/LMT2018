using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;

namespace NetPlan
{
	public enum RecordDataType
	{
		Original,		// 从enb中查到的原始值
		NewAdd,			// 新增的信息
		Modified,		// enb中查到的数据被修改
		WaitDel			// enb中查到的数据被删除
	}

	// 设备的属性
	public class DevAttributeInfo
	{
		#region 公有属性

		public string m_strOidIndex { get; private set; }      // 该条记录的索引，例如板卡索引：.0.0.2

		// 所有的属性。key:field en name
		public Dictionary<string, MibLeafNodeInfo> m_mapAttributes;

		public EnumDevType m_enumDevType;					// 设备类型枚举值

		public readonly bool m_bIsScalar;					// 设备是否是标量表

		public RecordDataType m_recordType { get; set; }					// 记录类型

		#endregion


		#region 公共接口

		/// <summary>
		/// 新增一个设备调用的构造函数
		/// </summary>
		/// <param name="mEnumDevType">设备类型</param>
		/// <param name="devIndex">设备的序号，用于生成索引</param>
		public DevAttributeInfo(EnumDevType mEnumDevType, int devIndex, bool bIsScalar = false)
		{
			m_bIsScalar = bIsScalar;
			m_enumDevType = mEnumDevType;
			m_mapAttributes = new Dictionary<string, MibLeafNodeInfo>();
			//m_strOidIndex = m_bIsScalar ? ".0" : GerenalDevOidIndex(devIndex);	// 如果是标量，索引就直接设置为.0

			InitDevInfo(mEnumDevType, devIndex);
		}

		/// <summary>
		/// 从基站中查询MIB表得到一个设备属性
		/// </summary>
		/// <param name="mEnumDevType">设备类型</param>
		/// <param name="strIndex">索引字符串</param>
		/// <param name="bIsScalar">是否是标量</param>
		public DevAttributeInfo(EnumDevType mEnumDevType, string strIndex, bool bIsScalar = false)
		{
			m_enumDevType = mEnumDevType;
			m_mapAttributes = new Dictionary<string, MibLeafNodeInfo>();
			m_strOidIndex = strIndex;
			m_bIsScalar = bIsScalar;
			InitDevInfo(mEnumDevType, strIndex);
		}

		/// <summary>
		/// 归并另一个同类型设备的所有属性
		/// </summary>
		/// <param name="rightDev"></param>
		/// <returns></returns>
		public bool MergeAnotherDev(DevAttributeInfo rightDev)
		{
			if (null == rightDev)
			{
				return false;
			}

			// 此处只合并了属性，其他的属性均为处理
			if (null == rightDev.m_mapAttributes)
			{
				return false;
			}

			if (0 == rightDev.m_mapAttributes.Count)
			{
				return true;
			}

			foreach (var kv in rightDev.m_mapAttributes)
			{
				if (m_mapAttributes.ContainsKey(kv.Key))
				{
					continue;
				}

				if (null == kv.Value)
				{
					continue;
				}

				m_mapAttributes.Add(kv.Key, kv.Value);
			}

			return true;
		}

		/// <summary>
		/// 设置字段的值
		/// </summary>
		/// <param name="strFieldName">待修改的字段名</param>
		/// <param name="strLatestValue">最后的值</param>
		/// <returns>修改成功返回true，修改失败返回false</returns>
		public bool SetFieldValue(string strFieldName, string strLatestValue)
		{
			if (string.IsNullOrEmpty(strFieldName) || string.IsNullOrEmpty(strLatestValue))
			{
				return false;
			}

			if (!m_mapAttributes.ContainsKey(strFieldName))
			{
				return false;
			}

			var field = m_mapAttributes[strFieldName];
			if (null == field)
			{
				return false;
			}

			if (!field.SetValue(strLatestValue))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// 设置字段的原始值
		/// </summary>
		/// <param name="strFieldName"></param>
		/// <param name="strOriginValue"></param>
		/// <param name="bConvertToEnum">枚举值是否转为原值</param>
		/// <returns></returns>
		public bool SetFieldOriginValue(string strFieldName, string strOriginValue, bool bConvertToEnum = true)
		{
			if (string.IsNullOrEmpty(strFieldName) || string.IsNullOrEmpty(strOriginValue))
			{
				return false;
			}

			if (!m_mapAttributes.ContainsKey(strFieldName))
			{
				return false;
			}

			var field = m_mapAttributes[strFieldName];
			if (null == field)
			{
				return false;
			}

			var strValue = strOriginValue;
			if (bConvertToEnum)
			{
				var managerRange = field.mibAttri.managerValueRange;
				var mapEnums = MibStringHelper.SplitManageValue(managerRange);
				if (null == mapEnums)
				{
					return false;
				}

				foreach (var kv in mapEnums)
				{
					if (kv.Value == strOriginValue)
					{
						strValue = kv.Key.ToString();
						break;
					}
				}
			}

			return field.SetOriginValue(strValue);
		}

		/// <summary>
		/// 把另一个相同对象的originValue调整为自己的originValue，自己的originValue作为latestValue
		/// </summary>
		/// <param name="oldDev"></param>
		/// <returns></returns>
		public bool AdjustOtherDevOriginValueToMyOrigin(DevAttributeInfo oldDev)
		{
			// 如果index不同不做调整
			if (null == oldDev || !m_strOidIndex.Equals(oldDev.m_strOidIndex))
			{
				return false;
			}

			var oldAttriMap = oldDev.m_mapAttributes;
			foreach (var attribute in m_mapAttributes)
			{
				var mibName = attribute.Key;
				var mli = attribute.Value;
				if (oldAttriMap.ContainsKey(mibName))
				{
					mli.m_strOriginValue = mli.m_strLatestValue;
					mli.m_strOriginValue = oldAttriMap[mibName].m_strOriginValue;
				}
			}

			return true;
		}


		#endregion

		#region 私有成员

		/// <summary>
		/// 生成设备oid索引。
		/// 由于当前MIB的特性，板卡、RRU、天线阵等设备特性，只需要一个设备序号即可生成索引
		/// </summary>
		/// <param name="devIndex"></param>
		/// <returns></returns>
		private string GerenalDevOidIndex(int indexGrade, int devIndex)
		{
			// TODO 暂时先简单处理板卡，RRU，天线阵等设备的索引
			if (m_bIsScalar)
			{
				return ".0";
			}

			var sb = new StringBuilder();
			for (int i = 1; i < indexGrade; i++)
			{
				sb.Append(".0");
			}

			sb.Append($".{devIndex}");

			return sb.ToString();
		}

		/// <summary>
		/// 添加新设备，初始化设备信息
		/// </summary>
		/// <param name="type"></param>
		private void InitDevInfo(EnumDevType type, int devIndex)
		{
			// 根据类型，找到MIB入口，然后找到MIB tbl 信息
			var strEntryName = DevTypeHelper.GetEntryNameFromDevType(type);
			if (null == strEntryName)
			{
				return;
			}

			var tbl = Database.GetInstance().GetMibDataByTableName(strEntryName, CSEnbHelper.GetCurEnbAddr());
			if (null == tbl)
			{
				return;
			}

			var indexGrade = tbl.indexNum;
			m_strOidIndex = GerenalDevOidIndex(indexGrade, devIndex);
			var attributes = NPECmdHelper.GetInstance().GetDevAttributesByEntryName(strEntryName);
			if (null == attributes)
			{
				return;
			}

			m_mapAttributes = attributes;

			// 还需要加上索引列
			if (!m_bIsScalar)
			{
				AddIndexColumnToAttributes(tbl.childList, indexGrade);
			}
		}

		private void InitDevInfo(EnumDevType type, string strIndex)
		{
			// 根据类型，找到MIB入口，然后找到MIB tbl 信息
			var strEntryName = DevTypeHelper.GetEntryNameFromDevType(type);
			if (null == strEntryName)
			{
				return;
			}

			var tbl = Database.GetInstance().GetMibDataByTableName(strEntryName, CSEnbHelper.GetCurEnbAddr());
			if (null == tbl)
			{
				return;
			}

			var indexGrade = tbl.indexNum;
			var attributes = NPECmdHelper.GetInstance().GetDevAttributesByEntryName(strEntryName);
			if (null == attributes)
			{
				return;
			}

			m_mapAttributes = attributes;

			// 还需要加上索引列
			if (!m_bIsScalar)
			{
				AddIndexColumnToAttributes(tbl.childList, indexGrade);
			}
		}

		/// <summary>
		/// 属性中添加索引列
		/// </summary>
		/// <param name="childList"></param>
		/// <param name="indexGrade"></param>
		/// <returns></returns>
		private bool AddIndexColumnToAttributes(List<MibLeaf> childList, int indexGrade)
		{
			for (var i = 1; i <= indexGrade; i++)
			{
				var indexColumn = childList.FirstOrDefault(childLeaf => i == childLeaf.childNo);
				var indexVale = MibStringHelper.GetRealValueFromIndex(m_strOidIndex, i);
				var info = new MibLeafNodeInfo
				{
					m_strOriginValue = indexVale,
					m_bReadOnly = true,
					m_bVisible = true,
					mibAttri = indexColumn
				};
				m_mapAttributes.Add(indexColumn.childNameMib, info);
			}
			return true;
		}

		#endregion

	}

	// 连接的属性
	public class LinkAttributeInfo
	{
		
	}
}
