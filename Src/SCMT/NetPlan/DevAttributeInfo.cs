using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
		Original = 1,		// 从enb中查到的原始值
		NewAdd,			// 新增的信息
		Modified,		// enb中查到的数据被修改
		WaitDel			// enb中查到的数据被删除
	}

	public static class RdtHelper
	{
		/// <summary>
		/// 把RecordDataType枚举值转换为SnmpCmdType
		/// </summary>
		/// <param name="rdt"></param>
		/// <returns></returns>
		public static EnumSnmpCmdType ConvertToSct(this RecordDataType rdt)
		{
			var sct = EnumSnmpCmdType.Invalid;
			switch (rdt)
			{
				case RecordDataType.Original:
					sct = EnumSnmpCmdType.Get;
					break;
				case RecordDataType.NewAdd:
					sct = EnumSnmpCmdType.Add;
					break;
				case RecordDataType.Modified:
					sct = EnumSnmpCmdType.Set;
					break;
				case RecordDataType.WaitDel:
					sct = EnumSnmpCmdType.Del;
					break;
				default:
					break;
			}

			return sct;
		}
	}

	// 设备的属性
	[Serializable]
	public class DevAttributeInfo : DevAttributeBase, ICloneable
	{
		#region 公有属性

		public EnumDevType m_enumDevType { get; set; }					// 设备类型枚举值

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

			InitDevInfo(mEnumDevType, devIndex);

			SetFieldLatestValue(m_strRsMibName, "4");
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
			m_strOidIndex = strIndex;
			m_bIsScalar = bIsScalar;
			InitDevInfo(mEnumDevType);

			SetFieldLatestValue(m_strRsMibName, "4");
		}

		/// <summary>
		/// 归并另一个同类型设备的所有属性
		/// </summary>
		/// <param name="rightDev"></param>
		/// <returns></returns>
		public bool MergeAnotherDev(DevAttributeInfo rightDev)
		{
			// 此处只合并了属性，其他的属性均为处理
			if (rightDev?.m_mapAttributes == null)
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
					mli.m_strLatestValue = mli.m_strOriginValue;
					mli.m_strOriginValue = oldAttriMap[mibName].m_strOriginValue;
				}
			}

			return true;
		}

		#endregion

		#region 私有成员

		/// <summary>
		/// 添加新设备，初始化设备信息
		/// </summary>
		/// <param name="type"></param>
		/// <param name="devIndex"></param>
		private void InitDevInfo(EnumDevType type, int devIndex)
		{
			// 根据类型，找到MIB入口，然后找到MIB tbl 信息
			var strEntryName = DevTypeHelper.GetEntryNameFromDevType(type);
			if (null == strEntryName)
			{
				return;
			}

			m_strEntryName = strEntryName;

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

			var rsMl = tbl.GetRowStatusMibName();
			if (null == rsMl)
			{
				return;
			}

			m_mapAttributes = attributes;
			m_strRsMibName = rsMl.childNameMib;

			AddRowStatusToAttributeMap(rsMl, "6");

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

		private void InitDevInfo(EnumDevType type)
		{
			// 根据类型，找到MIB入口，然后找到MIB tbl 信息
			var strEntryName = DevTypeHelper.GetEntryNameFromDevType(type);
			if (null == strEntryName)
			{
				Log.Error($"根据{type.ToString()}未找到对应的表名");
				return;
			}

			m_strEntryName = strEntryName;

			base.InitDevInfo();
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
				if (null == indexColumn)
				{
					Log.Error("查找信息失败");
					return false;
				}
				var indexVale = MibStringHelper.GetRealValueFromIndex(m_strOidIndex, i);
				var info = new MibLeafNodeInfo
				{
					m_strOriginValue = indexVale,
					m_bReadOnly = !indexColumn.IsEmpoweredModify(),
					m_bVisible = true,
					mibAttri = indexColumn
				};

				m_mapAttributes.Add(indexColumn.childNameMib, info);
			}
			return true;
		}

		#endregion

		//深拷贝
		public new DevAttributeInfo DeepClone()
		{
			using (Stream objectStream = new MemoryStream())
			{
				IFormatter formatter = new BinaryFormatter();
				formatter.Serialize(objectStream, this);
				objectStream.Seek(0, SeekOrigin.Begin);
				return formatter.Deserialize(objectStream) as DevAttributeInfo;
			}
		}

		//浅拷贝
		public new object Clone()
		{
			return this.MemberwiseClone();
		}

		//浅拷贝
		public new DevAttributeInfo ShallowClone()
		{
			return this.Clone() as DevAttributeInfo;
		}
	}

	/// <summary>
	/// rhub设备，需要区分版本
	/// </summary>
	[Serializable]
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
	public class LinkEndpoint
	{
		public EnumDevType devType;		// 设备类型
		public string strDevIndex;		// 设备索引
		public EnumPortType portType;	// 端口类型
		public int nPortNo;				// 端口号

		public LinkEndpoint()
		{
			devType = EnumDevType.unknown;
			strDevIndex = null;
			portType = EnumPortType.unknown;
			nPortNo = -1;
		}

		public override string ToString()
		{
			return $"设备类型：{devType.ToString()},设备索引：{strDevIndex}，端口类型：{portType.ToString()}，端口号：{nPortNo}";
		}
	}

	public class WholeLink
	{
		public LinkEndpoint m_srcEndPoint;
		public LinkEndpoint m_dstEndPoint;
		public EnumDevType m_linkType;

		public WholeLink()
		{
			m_srcEndPoint = new LinkEndpoint();
			m_dstEndPoint = new LinkEndpoint();
			m_linkType = EnumDevType.unknown;
		}

		public WholeLink(LinkEndpoint srcEndpoint, LinkEndpoint dstEndpoint)
		{
			m_srcEndPoint = srcEndpoint;
			m_dstEndPoint = dstEndpoint;
			GetLinkType();
		}

		public EnumDevType GetLinkType()
		{
			var linkType = EnumDevType.unknown;

			var bSrcIsRru = (EnumDevType.rru == m_srcEndPoint.devType);
			var bSrcIsRhub = (EnumDevType.rhub == m_srcEndPoint.devType);
			var bDstIsRru = (EnumDevType.rru == m_dstEndPoint.devType);
			var bDstIsRhub = (EnumDevType.rhub == m_dstEndPoint.devType);

			var srcDevTypeStr = m_srcEndPoint.devType.ToString();
			var dstDevTypeStr = m_dstEndPoint.devType.ToString();

			// 判断设备类型是否相同。todo 例外的是，级联的rru和rhub设备
			if (m_srcEndPoint.devType == m_dstEndPoint.devType && (!bSrcIsRhub && !bSrcIsRru))
			{
				Log.Error($"源设备类型{srcDevTypeStr}和目的设备类型{dstDevTypeStr}相同，且不是rru和rhub。添加连接失败");
				return linkType;
			}

			// 判断级联的设备索引是否相同。级联的设备索引不能相同，也就是不能自己连接自己
			if (m_srcEndPoint.devType == m_dstEndPoint.devType && m_srcEndPoint.strDevIndex == m_dstEndPoint.strDevIndex)
			{
				Log.Error($"源设备类型{srcDevTypeStr}和目的设备类型{dstDevTypeStr}相同，且源和目的的索引值{m_srcEndPoint.strDevIndex}也相同");
				return linkType;
			}

			// 判断源和目的是否是有效的组合
			if (!DevTypeHelper.IsValidDevCop(m_srcEndPoint.devType, m_dstEndPoint.devType))
			{
				Log.Error($"源设备类型{srcDevTypeStr}和目的设备类型{dstDevTypeStr}不是有效的组合");
				return linkType;
			}

			// 判断是否是rhub与pico设备的连接。rhub只能和pico连接，不能和普通的rru设备连接
			var bFlag1 = false;
			var bFlag2 = false;
			if ((bFlag1 = (bSrcIsRru && bDstIsRhub)) || (bFlag2 = (bSrcIsRhub && bDstIsRru)))
			{
				var strRruIndex = m_srcEndPoint.strDevIndex;

				if (bFlag2)
				{
					strRruIndex = m_dstEndPoint.strDevIndex;
				}

				if (!MibInfoMgr.GetInstance().IsPico(strRruIndex))
				{
					Log.Error($"索引为{strRruIndex}的RRU设备不是pico，不能建立到rhub的连接");
					return linkType;
				}
			}

			var srcPortTypeStr = m_srcEndPoint.portType.ToString();
			var dstPortTypeStr = m_dstEndPoint.portType.ToString();

			// 判断源和目的的端口类型是否是有效的组合
			if (!PortTypeHelper.IsValidPortCop(m_srcEndPoint.portType, m_dstEndPoint.portType))
			{
				Log.Error($"源设备端口类型{srcPortTypeStr}和目的设备类型{dstPortTypeStr}不是有效的组合");
				return linkType;
			}

			// 获取连接类型
			linkType = DevTypeHelper.GetLinkTypeByTwoEp(m_srcEndPoint.devType, m_dstEndPoint.devType);
			m_linkType = linkType;
			return linkType;
		}

		/// todo 如果是级联的情况，需要特别注意
		public string GetDevIndex(EnumDevType devType)
		{
			if (m_srcEndPoint.devType == devType)
			{
				return m_srcEndPoint.strDevIndex;
			}

			if (m_dstEndPoint.devType == devType)
			{
				return m_dstEndPoint.strDevIndex;
			}
			return null;
		}

		public int GetDevIrPort(EnumDevType devType, EnumPortType portType)
		{
			if (m_srcEndPoint.devType == devType &&
				m_srcEndPoint.portType == portType)
			{
				return m_srcEndPoint.nPortNo;
			}

			if (m_dstEndPoint.devType == devType &&
			    m_dstEndPoint.portType == portType)
			{
				return m_dstEndPoint.nPortNo;
			}

			return -1;
		}


		public override string ToString()
		{
			return $"源端信息：{m_srcEndPoint},目的端信息：{m_dstEndPoint}";
		}
	}
}
