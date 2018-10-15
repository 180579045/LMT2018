using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPlan
{
	// 设备的属性
	public class DevAttributeInfo
	{
		#region 公有属性

		public string m_strOidIndex { get; }			// 索引

		public Dictionary<string, MibLeafNodeInfo> m_mapAttributes;		// 所有的属性。key:field en name

		public EnumDevType m_enumDevType;					// 设备类型枚举值

		public readonly bool m_bIsScalar;					// 设备是否是标量表

		public List<string> m_listModifyField;				// 被修改的字段名列表

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
			m_strOidIndex = m_bIsScalar ? ".0" : GerenalDevOidIndex(devIndex);	// 如果是标量，索引就直接设置为.0
			m_listModifyField = new List<string>();
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
			m_listModifyField = new List<string>();
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

			// 判断字段最新的值和原始值是否相同，如果相同，就从m_listModifyField中删除，如果不同，就加入
			AddOrDelFieldFromList(strFieldName, field.IsModified());

			return true;
		}

		#endregion

		#region 私有成员

		/// <summary>
		/// 生成设备oid索引
		/// </summary>
		/// <param name="devIndex"></param>
		/// <returns></returns>
		private string GerenalDevOidIndex(int devIndex)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 添加或者删除修改的字段列表
		/// </summary>
		/// <param name="strField"></param>
		/// <param name="bAdd"></param>
		private void AddOrDelFieldFromList(string strField, bool bAdd)
		{
			// 如果字段被修改且列表中不包含，就添加
			if (bAdd && !m_listModifyField.Contains(strField))
			{
				m_listModifyField.Add(strField);
			}

			// 如果字段未修改，且列表中已经包含，就删除
			if (!bAdd && m_listModifyField.Contains(strField))
			{
				m_listModifyField.Remove(strField);
			}
		}

		#endregion

	}

	// 连接的属性
	public class LinkAttributeInfo
	{
		
	}
}
