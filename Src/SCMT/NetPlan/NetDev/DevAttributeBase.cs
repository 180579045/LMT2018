using CommonUtility;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NetPlan
{
	/// <summary>
	/// 无类型设备信息
	/// </summary>
	[Serializable]
	public class DevAttributeBase : ICloneable
	{
		public string m_strOidIndex { get; set; }      // 该条记录的索引，例如板卡索引：.0.0.2

		// 所有的属性。key:field en name
		public Dictionary<string, MibLeafNodeInfo> m_mapAttributes;

		public string m_strEntryName;                   // 设备类型枚举值

		public bool m_bIsScalar;                   // 设备是否是标量表

		public RecordDataType m_recordType { get; protected set; }    // 记录类型

		protected string m_strRsMibName;

		public DevAttributeBase(string strEntryName, string strIdx)
		{
			m_mapAttributes = new Dictionary<string, MibLeafNodeInfo>();

			m_strOidIndex = strIdx;
			m_strEntryName = strEntryName;
			m_recordType = RecordDataType.NewAdd;

			InitDevInfo();

			SetFieldLatestValue(m_strRsMibName, "4");
		}

		protected DevAttributeBase()
		{
			m_recordType = RecordDataType.NewAdd;
			m_mapAttributes = new Dictionary<string, MibLeafNodeInfo>();
		}

		protected void InitDevInfo()
		{
			var tbl = Database.GetInstance().GetMibDataByTableName(m_strEntryName, CSEnbHelper.GetCurEnbAddr());
			if (null == tbl)
			{
				return;
			}

			var indexGrade = tbl.indexNum;
			var attributes = NPECmdHelper.GetInstance().GetDevAttributesByEntryName(m_strEntryName);
			if (null == attributes)
			{
				return;
			}

			m_mapAttributes = attributes;
			//添加行状态
			var rsMl = tbl.GetRowStatusMibName();
			if (null != rsMl)
			{
				m_strRsMibName = rsMl.childNameMib;
				AddRowStatusToAttributeMap(rsMl, "4");
			}
			// 还需要加上索引列
			if (indexGrade > 0)
			{
				m_bIsScalar = false;
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
		/// 判断一个字段是否存在。不同的MIB版本可能存在不一致
		/// </summary>
		/// <param name="strFieldName"></param>
		/// <returns></returns>
		public bool IsExistField(string strFieldName)
		{
			if (string.IsNullOrEmpty(strFieldName))
			{
				throw new ArgumentNullException(strFieldName);
			}

			return m_mapAttributes.ContainsKey(strFieldName);
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
				throw new ArgumentNullException(strFieldName);
			}

			if (!m_mapAttributes.ContainsKey(strFieldName))
			{
				Log.Error($"该设备属性中不包含字段{strFieldName}");
				return null;
			}

			var originValue = m_mapAttributes[strFieldName].m_strOriginValue;
			if (bConvertToNum && null != originValue)
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
				throw new ArgumentNullException(strFieldName);
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

		/// <summary>
		/// 设置字段的值
		/// </summary>
		/// <param name="strFieldName">待修改的字段名</param>
		/// <param name="strLatestValue">最后的值</param>
		/// <param name="bConvertToEnum">是否转换为枚举值</param>
		/// <returns>修改成功返回true，修改失败返回false</returns>
		public bool SetFieldLatestValue(string strFieldName, string strLatestValue, bool bConvertToEnum = true)
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

			var strValue = strLatestValue;
			if (bConvertToEnum)
			{
				int ret;
				if (int.TryParse(strValue, out ret))
				{
					strValue = SnmpToDatabase.ConvertValueToString(field.mibAttri, strLatestValue);
				}
			}

			return field.SetLatestValue(strValue);
		}

		/// <summary>
		/// 设置字段的原始值
		/// </summary>
		/// <param name="strFieldName"></param>
		/// <param name="strOriginValue"></param>
		/// <param name="bConvertToEnum">枚举值是否转为int型值，也就是 : 前面的数字</param>
		/// <returns></returns>
		public bool SetFieldOriginValue(string strFieldName, string strOriginValue, bool bConvertToEnum = false)
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
				int ret;
				if (int.TryParse(strValue, out ret))
				{
					strValue = SnmpToDatabase.ConvertValueToString(field.mibAttri, strOriginValue);
				}
			}

			return field.SetOriginValue(strValue);
		}

		public bool SetFieldOriginValue(string strFieldName, int nOriginValue, bool bConvertToEnum = false)
		{
			return SetFieldOriginValue(strFieldName, nOriginValue.ToString(), bConvertToEnum);
		}

		public bool SetFieldOlValue(string strFieldName, string strOriginValue, bool bConvertToEnum = false)
		{
			return SetFieldOriginValue(strFieldName, strOriginValue, bConvertToEnum) &&
			       SetFieldLatestValue(strFieldName, strOriginValue, bConvertToEnum);
		}

		//深拷贝
		public DevAttributeBase DeepClone()
		{
			using (Stream objectStream = new MemoryStream())
			{
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(objectStream, this);
				objectStream.Seek(0, SeekOrigin.Begin);
				return formatter.Deserialize(objectStream) as DevAttributeBase;
			}
		}

		//浅拷贝
		public object Clone()
		{
			return MemberwiseClone();
		}

		//浅拷贝
		public DevAttributeBase ShallowClone()
		{
			return Clone() as DevAttributeBase;
		}

		/// <summary>
		/// 根据listColumns中的每个元素，在devInfo中找到对应的值，组装成字典，用于下发
		/// </summary>
		/// <param name="listColumns"></param>
		/// <param name="strRs">行状态的值：4，6</param>
		/// <returns></returns>
		public Dictionary<string, string> GenerateName2ValueMap(List<MibLeaf> listColumns, string strRs)
		{
			if (null == listColumns)
			{
				return null;
			}

			var n2v = new Dictionary<string, string>();

			foreach (var leafInfo in listColumns)
			{
				var leafName = leafInfo.childNameMib;

				// 行状态的值特殊处理
				if (leafInfo.IsRowStatus())
				{
					n2v.Add(leafName, strRs);
					continue;
				}

				var value = GetNeedUpdateValue(leafName);
				if (null == value)
				{
					continue;
				}

				n2v.Add(leafName, value);
			}

			return n2v;
		}

		/// <summary>
		/// 获取需要更新的值
		/// </summary>
		/// <param name="strOriginValue"></param>
		/// <param name="strLatestValue"></param>
		/// <returns></returns>
		private string GetNeedUpdateValue(string strOriginValue, string strLatestValue)
		{
			if (string.IsNullOrEmpty(strOriginValue))
			{
				return null;
			}

			if (null == strLatestValue || strLatestValue == strOriginValue)
			{
				return strOriginValue;
			}

			return strLatestValue;
		}

		// todo 这里可能存在bug。
		public string GetNeedUpdateValue(string strFieldName, bool bConvert = true)
		{
			if (string.IsNullOrEmpty(strFieldName))
			{
				throw new ArgumentNullException(strFieldName);
			}

			if (!m_mapAttributes.ContainsKey(strFieldName))
			{
				Log.Error($"索引为{m_strOidIndex}的设备属性中不包含{strFieldName}字段");
				return null;
			}

			var originValue = GetFieldOriginValue(strFieldName, bConvert);
			var latestValue = GetFieldLatestValue(strFieldName, bConvert);

			return GetNeedUpdateValue(originValue, latestValue);
		}

		/// <summary>
		/// 查询指定设备的多个字段值
		/// </summary>
		/// <param name="mapFieldAndValue">多个字段。key:字段名，value:字段值</param>
		/// <param name="bConvertToDigital">枚举值是否转换为数字</param>
		/// <returns>全部查询成功，返回true；其他情况返回false</returns>
		public bool GetNeedUpdateValue(IDictionary<string, string> mapFieldAndValue, bool bConvertToDigital = true)
		{
			for (var i = 0; i < mapFieldAndValue.Count; i++)
			{
				var kv = mapFieldAndValue.ElementAt(i);
				var value = GetNeedUpdateValue(kv.Key, bConvertToDigital);
				if (null == value)
				{
					return false;
				}

				mapFieldAndValue[kv.Key] = value;
			}

			return true;
		}

		/// <summary>
		/// 下发参数成功后，把latest设置为original
		/// </summary>
		public void AdjustLatestValueToOriginal()
		{
			foreach (var item in m_mapAttributes)
			{
				item.Value.SetOriginalValueToLatest();
			}
		}

		/// <summary>
		/// 生成设备oid索引。
		/// 由于当前MIB的特性，板卡、RRU、天线阵等设备特性，只需要一个设备序号即可生成索引
		/// </summary>
		/// <param name="indexGrade"></param>
		/// <param name="devIndex"></param>
		/// <returns></returns>
		protected string GerenalDevOidIndex(int indexGrade, int devIndex)
		{
			if (m_bIsScalar)
			{
				return ".0";
			}

			var sb = new StringBuilder();
			for (var i = 1; i < indexGrade; i++)
			{
				sb.Append(".0");
			}

			sb.Append($".{devIndex}");

			return sb.ToString();
		}

		public bool SetDevAttributeValue(string strFieldName, string strValue)
		{
			if (string.IsNullOrEmpty(strFieldName) || string.IsNullOrEmpty(strValue))
			{
				throw new CustomException("属性值传入参数有误");
			}

			if (!m_mapAttributes.ContainsKey(strFieldName))
			{
				Log.Error($"索引为{m_strOidIndex}的设备中未找到{strFieldName}字段，无法设置字段值");
				return false;
			}

			SetFieldLatestValue(strFieldName, strValue);
			if (m_recordType != RecordDataType.NewAdd)
			{
				SetDevRecordType(RecordDataType.Modified);
			}

			return true;
		}

		/// <summary>
		/// 设置记录类型，用于同步设置行状态的值
		/// </summary>
		/// <param name="recordType"></param>
		public void SetDevRecordType(RecordDataType recordType)
		{
			m_recordType = recordType;
			SetRowStatusValue(recordType);
		}

		#region 私有函数

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
				if (null == indexColumn)
				{
					Log.Error("查找信息失败");
					return false;
				}

				var indexVale = MibStringHelper.GetRealValueFromIndex(m_strOidIndex, i);
				var info = new MibLeafNodeInfo
				{
					m_strLatestValue = indexVale,//索引不会变化，将m_strLatestValue赋值
					m_strOriginValue = indexVale,
					m_bReadOnly = !indexColumn.IsEmpoweredModify(),
					m_bVisible = true,
					mibAttri = indexColumn
				};

				m_mapAttributes.Add(indexColumn.childNameMib, info);
			}
			return true;
		}

		/// <summary>
		/// 设行状态值，校验模块需要
		/// </summary>
		/// <param name="rdt"></param>
		private void SetRowStatusValue(RecordDataType rdt)
		{
			string strRsValue = "4";
			switch (rdt)
			{
				case RecordDataType.Original:
				case RecordDataType.NewAdd:
				case RecordDataType.Modified:
					break;

				case RecordDataType.WaitDel:
					strRsValue = "6";
					break;
			}

			SetFieldLatestValue(m_strRsMibName, strRsValue);
		}

		protected void AddRowStatusToAttributeMap(MibLeaf rsMibLeaf, string strValue)
		{
			var strRs = strValue == "4" ? "行有效" : "行无效";

			if (!m_mapAttributes.ContainsKey(rsMibLeaf.childNameMib))
			{
				var tmp = new MibLeafNodeInfo
				{
					m_bReadOnly = true,
					m_bVisible = false,
					m_strOriginValue = strRs,
					m_strLatestValue = strRs,
					mibAttri = rsMibLeaf
				};
				m_mapAttributes[rsMibLeaf.childNameMib] = tmp;
			}
			else
			{
				SetFieldOriginValue(rsMibLeaf.childNameMib, strValue, true);
			}
		}

		#endregion 私有函数
	}
}