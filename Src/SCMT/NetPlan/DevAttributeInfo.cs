using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LmtbSnmp;
using LogManager;
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

		/// <summary>
		/// 获取指定字段的值
		/// </summary>
		/// <param name="strFieldName">字段名</param>
		/// <param name="bConvertToNum">枚举值是否需要转换为数字</param>
		/// <returns>null:字段不存在</returns>
		public string GetFieldOriginValue(string strFieldName, bool bConvertToNum = true)
		{
			if (string.IsNullOrEmpty(strFieldName))
			{
				throw new ArgumentNullException("传入字段名无效");
			}

			if (!m_mapAttributes.ContainsKey(strFieldName))
			{
				Log.Error($"该设备属性中不包含字段{strFieldName}");
				return null;
			}

			var originValue = m_mapAttributes[strFieldName].m_strOriginValue;
			if (bConvertToNum)
			{
				return SnmpToDatabase.ConvertStringToMibValue(m_mapAttributes[strFieldName].mibAttri, originValue);
			}

			return originValue;
		}

		/// <summary>
		/// 获取字段的最新值
		/// </summary>
		/// <param name="strFieldName">字段名</param>
		/// <param name="bConvertToNum">是否需要转换</param>
		/// <returns></returns>
		public string GetFieldLatestValue(string strFieldName, bool bConvertToNum = true)
		{
			if (string.IsNullOrEmpty(strFieldName))
			{
				throw new ArgumentNullException("传入字段名无效");
			}

			if (!m_mapAttributes.ContainsKey(strFieldName))
			{
				Log.Error($"该设备属性中不包含字段{strFieldName}");
				return null;
			}

			var latestValue = m_mapAttributes[strFieldName].m_strLatestValue;
			if (null != latestValue && bConvertToNum)
			{
				return SnmpToDatabase.ConvertStringToMibValue(m_mapAttributes[strFieldName].mibAttri, latestValue);
			}

			return latestValue;
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

			var attriList = m_mapAttributes.Values.ToList();
			attriList.Sort(new MLNIComparer());

			// 排序后需要重新设置属性
			m_mapAttributes.Clear();
			foreach (var item in attriList)
			{
				m_mapAttributes[item.mibAttri.childNameMib] = item;
			}
		}

		private void InitDevInfo(EnumDevType type, string strIndex)
		{
			// 根据类型，找到MIB入口，然后找到MIB tbl 信息
			var strEntryName = DevTypeHelper.GetEntryNameFromDevType(type);
			if (null == strEntryName)
			{
				Log.Error($"根据{type.ToString()}未找到对应的表名");
				return;
			}

			var targetIp = CSEnbHelper.GetCurEnbAddr();
			var tbl = Database.GetInstance().GetMibDataByTableName(strEntryName, targetIp);
			if (null == tbl)
			{
				Log.Error($"根据表名{strEntryName}未找到基站{targetIp}的表信息");
				return;
			}

			var indexGrade = tbl.indexNum;
			var attributes = NPECmdHelper.GetInstance().GetDevAttributesByEntryName(strEntryName);
			if (null == attributes)
			{
				Log.Error($"根据表名{strEntryName}未找到对应的属性信息");
				return;
			}

			m_mapAttributes = attributes;

			// 还需要加上索引列
			if (!m_bIsScalar)
			{
				AddIndexColumnToAttributes(tbl.childList, indexGrade);
			}

			var attriList = m_mapAttributes.Values.ToList();
			attriList.Sort(new MLNIComparer());

			// 排序后需要重新设置属性
			m_mapAttributes.Clear();
			foreach (var item in attriList)
			{
				m_mapAttributes[item.mibAttri.childNameMib] = item;
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
				if (indexColumn != null)
					m_mapAttributes.Add(indexColumn.childNameMib, info);
			}
			return true;
		}

		#endregion

	}

	/// <summary>
	/// rhub设备，需要区分版本
	/// </summary>
	public class RHubDevAttri : DevAttributeInfo
	{
		public RHubDevAttri(int devIndex, string strDevVersion)
			: base(EnumDevType.rhub, devIndex)
		{
			m_strDevVersion = strDevVersion;
		}

		public RHubDevAttri(string strIndex, string strDevVersion)
			: base(EnumDevType.rhub, strIndex)
		{
			m_strDevVersion = strDevVersion;
		}

		public string m_strDevVersion { get; }
	}

	// 连接的源端或者目的端
	public struct LinkEndpoint
	{
		public EnumDevType devType;		// 设备类型
		public string strDevIndex;		// 设备索引
		public EnumPortType portType;	// 端口类型
		public int nPortNo;				// 端口号
	}

	public struct WholeLink
	{
		public LinkEndpoint m_srcEndPoint;
		public LinkEndpoint m_dstEndPoint;
		public EnumDevType m_linkType;
	}
}
