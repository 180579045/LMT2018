using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;

namespace LmtbSnmp
{
	#region snmp pdu 封装
	[Serializable]
	public class CDTLmtbPdu : CDTObjectRef
	{

		#region 公共接口和属性

		public long m_requestId { get; set; }		//请求消息Id

		public ushort m_type { set; get; }			//收到的Snmp响应报文类型

		//获取回调原因.Response，Trap,超时，中断
		public int reason { get; set; }

		public bool isEndOfMibView { get; set; }		//针对GetBulk命令，是否还会有更多的实例

		public long m_SourcePort { get; set; }			//源端口

		public long m_LastErrorStatus { get; set; }		//出错状态

		public long m_LastErrorIndex { get; set; }		//出错vb索引

		public int m_NotifyId { get; set; }				//在逻辑层经过自定义转换后的 Trap ID

		public bool m_bIsNeedPrint { get; set; }

		public string m_SourceIp { get; set; }

		public void SetSyncId(bool bSync)
		{
			if (null != m_lmtbPduInfo)
			{
				m_lmtbPduInfo.m_appendInfo.m_bIsSync = bSync;
			}
		}

		public bool getSyncId()
		{
			if (null != m_lmtbPduInfo)
			{
				return m_lmtbPduInfo.m_appendInfo.m_bIsSync;
			}

			return false;
		}

		public int getReqMsgType()
		{
			if (null != m_lmtbPduInfo)
			{
				return m_lmtbPduInfo.m_appendInfo.m_msgReqType;
			}

			return 0;
		}

		public void setReqMsgType(int reqMsgType)
		{
			if (null != m_lmtbPduInfo)
			{
				m_lmtbPduInfo.m_appendInfo.m_msgReqType = reqMsgType;
			}
		}

		public string get_CmdName()
		{
			return m_lmtbPduInfo?.m_appendInfo.m_cmdName;
		}

		public void SetCmdName(string cmdName)
		{
			if (null != m_lmtbPduInfo)
			{
				m_lmtbPduInfo.m_appendInfo.m_cmdName = cmdName;
			}
		}

		public int GetVbByIndex(int index, ref CDTLmtbVb obj)
		{
			if (VbCount() == 0)
			{
				return -1;
			}

			if (index < 0 || VbCount() < index)
			{
				return -1;
			}

			obj = m_VbList[index];

			return 0;
		}

		public CDTLmtbVb GetVbByIndexEx(int index)
		{
			if (index < 0 || VbCount() < index)
			{
				return null;
			}

			return m_VbList[index];
		}

		public int VbCount()
		{
			return m_VbList.Count;       //当前vb个数
		}

		//在Pdu里添加Vb
		public void AddVb(CDTLmtbVb pLmtbVb)
		{
			
			m_VbList.Add(pLmtbVb);

			// 需要将oid的索引去掉，然后添加到m_mapVBs中
			if (!m_mapVBs.ContainsKey(pLmtbVb.Oid))
			{
				m_mapVBs.Add(pLmtbVb.Oid, pLmtbVb.Value);
			}
			else
			{
				Log.Debug("OID重复添加");
				m_mapVBs[pLmtbVb.Oid] = pLmtbVb.Value;
			}
		}

		// 在pdu中添加多个vb
		public void AddVb(List<CDTLmtbVb> vbList)
		{
			m_VbList.AddRange(vbList);
			foreach(var pLmtbVb in vbList)
			{
				if (!m_mapVBs.ContainsKey(pLmtbVb.Oid))
				{
					m_mapVBs.Add(pLmtbVb.Oid, pLmtbVb.Value);
				}
				else
				{
					Log.Debug("OID重复添加");
					m_mapVBs[pLmtbVb.Oid] = pLmtbVb.Value;
				}

			}
		}

		public void Clear()
		{
			m_VbList.Clear();
			m_mapVBs.Clear();
		}

		public void RemoveAllVbs()
		{
			m_VbList.Clear();
		}

		//从vb绑定对中查找对应MibName的值。
		public bool GetValueByMibName(string strIPAddr, string strMibName, out string strValue, string lpszIndex = null)
		{
			strValue = "";

			string error = "";
			// 根据ip和mibname从数据库模块找到所需的信息，主要是oid
			var param = new Dictionary<string, MibLeaf> {[strMibName] = null};
			if (!Database.GetInstance().getDataByEnglishName(param, strIPAddr, out error))
			{
				return false;
			}

			var temp = param[strMibName];
			var prefix = SnmpToDatabase.GetMibPrefix().Trim('.');
			var oid = temp.childOid.Trim('.');

			var fulloid = $"{prefix}.{oid}";
			if (lpszIndex != null)
			{
				fulloid = $"{fulloid}.{lpszIndex.Trim('.')}";
			}

			return GetValueByOid(fulloid, out strValue, lpszIndex);
		}

		// 根据oid获取对应的value
		public bool GetValueByOid(string oid, out string strValue, string lpszIndex = null)
		{
			strValue = "";
			if (m_mapVBs.Count == 0)
			{
				RecordOidValue(oid, out lpszIndex);
			}

			// TODO: 老版本的LMT的m_mapVBs中存储的是去掉索引的Oid，现在存的是带索引的Oid，所以修改获取方法为：
			// 只要参数传递的Oid为m_mapVBs中的Oid的子字符串就认为是同一个oid（这样处理应该是对的，原来的RecordOidValue()方法处理太复杂了！）
			/*			if (!m_mapVBs.ContainsKey(oid))
						{
							Log.Error($"PDU中没有OID为{oid}的VB");
							return false;
						}

						strValue = m_mapVBs[oid];
			*/
			string strTmpOid = "";
			foreach(KeyValuePair<string, string> item in m_mapVBs)
			{
				// oid相等或oid为子字符串，就认为是同一个oid
				if (oid.Equals(item.Key) || item.Key.Contains(oid + "."))
				{
					strTmpOid = item.Key;
					break;
				}
			}

			if (string.IsNullOrEmpty(strTmpOid))
			{
				Log.Error($"PDU中没有OID为{oid}的VB");
				return false;
			}

			strValue = m_mapVBs[strTmpOid];

			return true;
		}

		public bool GetAlarmValueByMibName(string strMibName, out string strValue)
		{
			throw new NotImplementedException();
		}

		public bool GetValueByOID(string strOID, out string strValue)
		{
			return GetValueByOid(strOID, out strValue);
		}

		public void assignAppendValue(stru_LmtbPduAppendInfo appendInfo)
		{
			m_lmtbPduInfo.m_appendInfo.AssignValue(out appendInfo);
		}

		public void getAppendValue(stru_LmtbPduAppendInfo appendInfo)
		{
			m_lmtbPduInfo.m_appendInfo.GetValue(out appendInfo);
		}

		void CopyPduInfo(ref CDTLmtbPdu pPdu)
		{
			if (null != m_lmtbPduInfo)
			{
				pPdu.m_lmtbPduInfo = m_lmtbPduInfo;
			}
		}

		// 记录oid对应的数据 原来的代码实现的是个鬼啊
		public bool RecordOidValue(string strMibOID, out string lpszIndex)
		{
			//throw new NotImplementedException();
			lpszIndex = ".1";
			return true;
		}

		#endregion

		#region 私有属性

		private List<CDTLmtbVb> m_VbList;   //用来存储Vb对的容器

		//key为PDU中每一个VB的OID，Value为PDU中每一个oid的value
		private Dictionary<string, string> m_mapVBs;

		private stru_LmtbPduInfo m_lmtbPduInfo;

		#endregion

		// 构造函数
		public CDTLmtbPdu()
		{
			m_VbList = new List<CDTLmtbVb>();
			m_mapVBs = new Dictionary<string, string>();
			m_lmtbPduInfo = new stru_LmtbPduInfo();
		}

		public CDTLmtbPdu(string cmd, bool bIsSync = true, bool bNeedPrint = false)
		: this()
		{
			SetCmdName(cmd);
			SetSyncId(bIsSync);
			m_bIsNeedPrint = bNeedPrint;
		}
	}

	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public class stru_LmtbPduInfo
	{
		public long m_SourcePort;			//源端口
		public long m_LastErrorStatus;		//出错状态
		public long m_LastErrorIndex;		//出错vb索引
		public int m_NotifyId;				//在逻辑层经过自定义转换后的 Trap ID

		//[MarshalAs(UnmanagedType.ByValArray, SizeConst = CommonMacro.MAX_IPADDRSTR_LEN)]
		//public byte[] m_SourceIp;

		public string m_SourceIp;   //源IP地址,即消息来自哪个网元

		//[MarshalAs(UnmanagedType.ByValArray, SizeConst = CommonMacro.MAX_IPADDRSTR_LEN)]
		//public byte[] m_OMTSourceIp;
		public string m_OMTSourceIp;        //标示消息是由哪个OMT发出，用于OMT登陆时,不再使用

		public stru_LmtbPduAppendInfo m_appendInfo;

		public stru_LmtbPduInfo()
		{
			m_appendInfo = new stru_LmtbPduAppendInfo();
		}
	}

	[Serializable]
	public class stru_LmtbPduAppendInfo
	{
		public stru_LmtbPduAppendInfo()
		{
			m_bIsNeedPrint = true;
			m_bIsSync = false;
		}

		public void AssignValue(out stru_LmtbPduAppendInfo appendInfo)
		{
			appendInfo = this;
		}

		public void GetValue(out stru_LmtbPduAppendInfo appendInfoOut)
		{
			appendInfoOut = this;
		}

		public bool m_bIsNeedPrint { get; set; }
		public bool m_bIsSync { get; set; }
		public int m_msgReqType { get; set; } //发送的Snmp请求报文类型
		public long m_timeOut { get; set; }    //发送该Snmp报文设置的超时时间
		public string m_cmdName { get; set; }
	}

	#endregion


	#region snmp vb定义

	// snmp vb 封装
	[Serializable]
	public class CDTLmtbVb : CDTObjectRef
	{
		public CDTLmtbVb()
		{
			m_rawData = new byte[CommonMacro.MAX_VALUE_LEN];
		}

		#region 公共接口和属性

		//设置和获取Oid
		public string Oid { get; set; }

		//设置和获取实例值
		public string Value { get; set; }

		//设置和获取Syntax类型
		public SNMP_SYNTAX_TYPE SnmpSyntax { get; set; }

		public string AsnType { get; set; }

		public int ParentOidLength { get; set; }      /* 父节点的长度 */

		// 设置和获取Octet String类型的原始数据
		public bool SetRawData(byte[] RawDataBytes, int nDataLen)
		{
			if (RawDataBytes.Length < nDataLen || nDataLen > CommonMacro.MAX_VALUE_LEN)
			{
				return false;
			}

			Buffer.BlockCopy(RawDataBytes, 0, m_rawData, 0, nDataLen);
			m_nRowDatalen = nDataLen;

			return true;
		}

		public byte[] GetRawData()
		{
			return m_rawData;
		}

		public void Copy(out CDTLmtbVb pVb)
		{
			pVb = this;
		}

		#endregion

		#region 私有属性

		private readonly byte[] m_rawData;            /*用于存储Octet String的原始数据*/
		private int m_nRowDatalen;

		#endregion
	}

	#endregion

	public enum SNMP_SYNTAX_TYPE
	{
		SNMP_SYNTAX_INIT = 0,
		SNMP_SYNTAX_INT = 0 | 0 | 0x2,
		SNMP_SYNTAX_BITS = 0 | 0 | 0x3,
		SNMP_SYNTAX_OCTETS = 0 | 0 | 0x4,
		SNMP_SYNTAX_NULL = 0 | 0 | 0x5,
		SNMP_SYNTAX_OID = 0 | 0 | 0x6,
		SNMP_SYNTAX_INT32 = 0 | 0 | 0x2,
		SNMP_SYNTAX_IPADDR = 0x40 | 0 | 0,
		SNMP_SYNTAX_CNTR32 = 0x40 | 0 | 0x1,
		SNMP_SYNTAX_GAUGE32 = 0x40 | 0 | 0x2,
		SNMP_SYNTAX_TIMETICKS = 0x40 | 0 | 0x3,
		SNMP_SYNTAX_OPAQUE = 0x40 | 0 | 0x4,
		SNMP_SYNTAX_CNTR64 = 0x40 | 0 | 0x6,
		SNMP_SYNTAX_UINT32 = SNMP_SYNTAX_GAUGE32,
		SNMP_SYNTAX_NOSUCHOBJECT = 0x80 | 0 | 0,
		SNMP_SYNTAX_NOSUCHINSTANCE = 0x80 | 0 | 0x1,
		SNMP_SYNTAX_ENDOFMIBVIEW = 0x80 | 0 | 0x2
	};

	public class CommonMacro
	{
		public const int MAX_VALUE_LEN = 5120;
		public const int MAX_IPADDRSTR_LEN = 24;
	};
};
