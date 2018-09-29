using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
	/// <summary>
	/// 插入到IDToTable表的结构
	/// </summary>
	public class IDToTableStruct
	{
		//命令名最大长度
		public const int MAXCMDNAMELENGTH = 255; 
		
		//使用SNMP_PDU_TYPE枚举
		public int pduType;

		//与下发报文所对应的RequestID
		public long lRequestId;

		//响应消息类型: Get响应 Set响应
		public long messageType;

		//命令名
		public string strCmdName;

	}
}
