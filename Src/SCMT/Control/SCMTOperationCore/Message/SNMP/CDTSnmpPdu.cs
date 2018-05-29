using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SCMTOperationCore.Message.SNMP
{
	public class CDTLmtbPdu : CDTObjectRef
	{
		public CDTLmtbPdu()
		{ }

		//设置和获取源端口
		public void set_SourcePort(long SourcePort)
		{
			if (null != m_lmtbPduInfo)
			{
				m_lmtbPduInfo.m_SourcePort = SourcePort;
			}
		}

		public long get_SourcePort()
		{
			if (null != m_lmtbPduInfo)
			{
				return m_lmtbPduInfo.m_SourcePort;
			}

			return 0;
		}

		//设置和获取出错状态
		public void set_LastErrorStatus(long LastErrorStatus)
		{
			if (null != m_lmtbPduInfo)
			{
				m_lmtbPduInfo.m_LastErrorStatus = LastErrorStatus;
			}
		}

		public long get_LastErrorStatus()
		{
			if (null != m_lmtbPduInfo)
			{
				return m_lmtbPduInfo.m_LastErrorStatus;
			}

			return 0;
		}

		//设置和获取出错vb索引
		public void set_LastErrorIndex(long LastErrorIndex)
		{
			if (null != m_lmtbPduInfo)
			{
				m_lmtbPduInfo.m_LastErrorIndex = LastErrorIndex;
			}
		}

		public long get_LastErrorIndex()
		{
			if (null != m_lmtbPduInfo)
			{
				return m_lmtbPduInfo.m_LastErrorIndex;
			}

			return 0;
		}

		//设置和获取Notify Id
		public void set_NotifyId(int NotifyId)
		{
			if (null != m_lmtbPduInfo)
			{
				m_lmtbPduInfo.m_NotifyId = NotifyId;
			}
		}

		public int get_NotifyId()
		{
			if (null != m_lmtbPduInfo)
			{
				return m_lmtbPduInfo.m_NotifyId;
			}

			return 0;
		}

		public void setPrintId(bool bNotPrint)
		{
			if (null != m_lmtbPduInfo)
			{
				m_lmtbPduInfo.m_appendInfo.m_bIsNeedPrint = bNotPrint;
			}
		}

		public bool getPrintId()
		{
			if (null != m_lmtbPduInfo)
			{
				return m_lmtbPduInfo.m_appendInfo.m_bIsNeedPrint;
			}

			return false;
		}

		public void setSyncId(bool bSync)
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

		//设置和获取源IP
		public void set_SourceIp(string SourceIp)
		{
			if (null != m_lmtbPduInfo)
			{
				m_lmtbPduInfo.m_SourceIp = SourceIp;
			}
		}

		public void SetOmtSourceIp(string inSourceIp)
		{
			if (null != m_lmtbPduInfo)
			{
				m_lmtbPduInfo.m_OMTSourceIp = inSourceIp;
			}
		}

		public string get_SourceIp()
		{
			if (null != m_lmtbPduInfo)
			{
				return m_lmtbPduInfo.m_SourceIp;
			}

			return null;
		}

		public string get_omtSourceIp()
		{
			if (null != m_lmtbPduInfo)
			{
				return m_lmtbPduInfo.m_OMTSourceIp;
			}

			return null;
		}

		public string get_CmdName()
		{
			if (null != m_lmtbPduInfo)
			{
				return m_lmtbPduInfo.m_appendInfo.m_cmdName;
			}

			return null;
		}

		public void setCmdName(string cmdName)
		{
			if (null != m_lmtbPduInfo)
			{
				m_lmtbPduInfo.m_appendInfo.m_cmdName = cmdName;
			}
		}

		public bool IsEndOfMibView()
		{
			return isEndOfMibView;
		}

		public void SetEndOfMibViewFlag(bool bEndOfMibView)
		{
			isEndOfMibView = bEndOfMibView;
		}

		//获取回调原因
		public int get_Reason()
		{
			return reason;
		}

		public void set_Reason(int rval)
		{
			reason = rval;
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

			obj = m_VbTable[index];

			return 0;
		}

		public CDTLmtbVb GetVbByIndexEx(int index)
		{
			if (index < 0 || VbCount() < index)
			{
				return null;
			}

			return m_VbTable[index];
		}

		public int VbCount()
		{
			return m_VbTable.Count;       //当前vb个数
		}

		//在Pdu里田间Vb
		public void AddVb(CDTLmtbVb pLmtbVb)
		{
			m_VbTable.Add(pLmtbVb);
		}

		public void Clear()
		{
			m_VbTable.Clear();
			m_mapVBs.Clear();
		}

		public int Clone(CDTLmtbPdu pLmtbPdu)
		{
			if (null == pLmtbPdu)
			{
				return -1;
			}

			if (this == pLmtbPdu)
			{
				return 0;
			}

			//TODO 赋值

			return 0;
		}

		public void RemoveAllVbs()
		{
			m_VbTable.Clear();
		}

		//int GetVbIndexByMibName(class CAdoConnection* pAdoConn, const char* strMibName)	//从vb绑定对中查找对应MibName的索引
		//{

		//}

		//从vb绑定对中查找对应MibName的值
		public bool GetValueByMibName(string strIPAddr, string strMibName, out string strValue, string lpszIndex = null)    //从vb绑定对中查找对应MibName的值
		{
			throw new NotImplementedException();
		}

		public bool GetAlarmValueByMibName(string strMibName, out string strValue)
		{
			throw new NotImplementedException();
		}

		public bool GetValueByOID(string strOID, out string strValue)
		{
			throw new NotImplementedException();
		}

		public void assignAppendValue(stru_LmtbPduAppendInfo appendInfo)
		{
			m_lmtbPduInfo.m_appendInfo.assignValue(out appendInfo);
		}

		public void getAppendValue(stru_LmtbPduAppendInfo appendInfo)
		{
			m_lmtbPduInfo.m_appendInfo.getValue(out appendInfo);
		}

		public void SetRequestId(long id)
		{
			m_requestId = id;
		}

		long GetRequestId()
		{
			return m_requestId;
		}

		public void SetPduType(ushort tp)
		{
			m_type = tp;
		}
		ushort GetPduType()
		{
			return m_type;
		}

		public void CopyPduInfo(ref CDTLmtbPdu pPdu)
		{
			if (null != m_lmtbPduInfo)
			{
				pPdu.m_lmtbPduInfo = m_lmtbPduInfo;
			}
		}

		public bool RecordOidValue(string strMibOID, out string lpszIndex)
		{
			throw new NotImplementedException();
		}

		/*张新发2008年6月2日添加，用来标识函数回调原因，可以判断是否是超时回调*/
		private int reason;			//回调原因,Response，Trap,超时，中断
		private long m_requestId;	//请求消息Id

		private ushort m_type; //收到的Snmp响应报文类型
		private bool isEndOfMibView;  //针对GetBulk命令，是否还会有更多的实例

		private List<CDTLmtbVb> m_VbTable = new List<CDTLmtbVb>();   //用来存储Vb对的容器

		//key为PDU中每一个VB的OID，Value为PDU中每一个VB的value
		private Dictionary<string, string> m_mapVBs = new Dictionary<string, string>();

		private stru_LmtbPduInfo m_lmtbPduInfo;
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
	}

	public class stru_LmtbPduAppendInfo
	{
		public stru_LmtbPduAppendInfo()
		{
			m_bIsNeedPrint = true;
			m_bIsSync = false;
		}

		public void assignValue(out stru_LmtbPduAppendInfo appendInfo)
		{
			appendInfo = this;
		}

		public void getValue(out stru_LmtbPduAppendInfo appendInfo_out)
		{
			appendInfo_out = this;
		}

		public bool m_bIsNeedPrint { get; set; }
		public bool m_bIsSync { get; set; }
		public int m_msgReqType { get; set; } //发送的Snmp请求报文类型
		public long m_timeOut { get; set; }    //发送该Snmp报文设置的超时时间
		public string m_cmdName { get; set; }
	}

	[Serializable, StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public class stru_LmtbVbInfo
	{
		public SNMP_SYNTAX_TYPE m_SnmpSyntax;
		public string m_Value;
		public string m_Oid;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = CommonMacro.MAX_VALUE_LEN)]
		public byte[] m_rawData;			/*用于存储Octet String的原始数据*/
		public int m_nRowDatalen;
		public int m_nParentOidLength;      /* 父节点的长度 */

	public stru_LmtbVbInfo()
	{
		m_rawData = new byte[CommonMacro.MAX_VALUE_LEN];
	}
};

	public class CDTLmtbVb : CDTObjectRef
	{
		public CDTLmtbVb()
		{
			m_lmtbVbInfo = new stru_LmtbVbInfo();
		}

		//设置和获取Oid
		public void set_Oid(string Oid)
		{
			if (null != m_lmtbVbInfo)
			{
				m_lmtbVbInfo.m_Oid = Oid;
			}
		}

		public string get_Oid()
		{
			if (null != m_lmtbVbInfo)
			{
				return m_lmtbVbInfo.m_Oid;
			}

			return null;
		}

		//设置和获取实例值
		public void set_Value(string Value)
		{
			if (null != m_lmtbVbInfo)
			{
				m_lmtbVbInfo.m_Value = Value;
			}
		}

		public string get_Value()
		{
			if (null != m_lmtbVbInfo)
			{
				return m_lmtbVbInfo.m_Value;
			}

			return null;
		}

		//设置和获取Syntax类型
		public void set_Syntax(SNMP_SYNTAX_TYPE Syntax)
		{
			if (null != m_lmtbVbInfo)
			{
				m_lmtbVbInfo.m_SnmpSyntax = Syntax;
			}
		}

		public SNMP_SYNTAX_TYPE get_Syntax()
		{
			if (null != m_lmtbVbInfo)
			{
				return m_lmtbVbInfo.m_SnmpSyntax;
			}

			return SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INIT;
		}

		/*设置和获取Octet String类型的原始数据*/
		public bool SetRawData(byte[] RawDataBytes, int nDataLen)
		{
			if (null == m_lmtbVbInfo || RawDataBytes.Length < nDataLen || nDataLen > CommonMacro.MAX_VALUE_LEN)
			{
				return false;
			}

			Buffer.BlockCopy(RawDataBytes, 0, m_lmtbVbInfo.m_rawData, 0, nDataLen);
			m_lmtbVbInfo.m_nRowDatalen = nDataLen;

			return true;
		}
		byte[] GetRawData()
		{
			if (null == m_lmtbVbInfo)
			{
				return null;
			}

			return m_lmtbVbInfo.m_rawData;
		}

		public void SetAsnType(string asnType)
		{
			if (null == asnType)
			{
				return;
			}

			this.asnType = asnType;
		}

		public string GetAsnType()
		{
			return asnType;
		}

		public void Copy(out CDTLmtbVb pVb)
		{
			pVb = this;
		}

		private stru_LmtbVbInfo m_lmtbVbInfo;
		private string asnType;

		/* added by liuwei6 20130308 增加ParentOID长度 */
		public void SetParentOidLength(int nParentOidLength)
		{
			if (null != m_lmtbVbInfo)
			{
				m_lmtbVbInfo.m_nParentOidLength = nParentOidLength;
			}
		}

		public int GetParentOidLength()
		{
			if (null != m_lmtbVbInfo)
			{
				return m_lmtbVbInfo.m_nParentOidLength;
			}

			return 0;
		}
	}

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
