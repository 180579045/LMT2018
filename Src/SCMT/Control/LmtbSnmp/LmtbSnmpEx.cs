using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtility;
using DataBaseUtil;
using LogManager;
using MsgQueue;
using SnmpSharpNet;

namespace LmtbSnmp
{
	/// <summary>
	/// 对Snmp相关信息的初始化和原语操作的实现
	/// 注意：此类的实例要通过DTLinkPathMgr.GetSnmpInstance()获取，不可直接实例化后使用
	/// 使用方法：
	/// ------------------------------------------------------------------------------
	///		ILmtbSnmp lmtbSnmp = DTLinkPathMgr.GetSnmpInstance(strIpAddr);
	///		if(0 != lmtbSnmp.SnmpGetSync(strIpAddr, oidList, out queryResults, 2000))
	///		{
	///			return false;
	///		}
	/// ------------------------------------------------------------------------------
	/// </summary>
	public class LmtbSnmpEx : ILmtbSnmp
	{
		// 同步snmp实例
		private SnmpHelper m_SnmpSync = null;

		// 异步snmp实例
		private SnmpHelper m_SnmpAsync = null;

		// 缓存异步SNMP请求时，requestId 与 LmtPdu对应关系
		private Dictionary<long, stru_LmtbPduAppendInfo> m_ReqIdPduInfo = new Dictionary<long, stru_LmtbPduAppendInfo>();

		// 数据库里的节点类型描述到Snmp Syntax的映射
		public static Dictionary<string, SNMP_SYNTAX_TYPE> m_ValType2SynTax = new Dictionary<string, SNMP_SYNTAX_TYPE>
			{
				{ "OCTETS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "PHYADDR", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "TEMP_ID", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "LONG", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32},
				{ "BOOL", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32},
				{ "UINT32", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_UINT32},
				{ "ROWSTATUS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32},
				{ "NULL", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_NULL},
				{ "OID", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OID},
				{ "IPADDR", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_IPADDR},
				{ "TIMETICKS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_TIMETICKS},
				{ "DATETIME", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "INETADDRESS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "VARIABLEPOINTER", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OID},
				{ "DATEANDTIME", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "BITS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_UINT32},
				{ "OCTET STRING", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "INETADDRESSTYPE", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_INT32},
				{ "MACADDRESS", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "UNSIGNED32ARRAY", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "INTEGER32ARRAY", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS},
				{ "MNCMCCTYPE", SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS}
			};

		/// <summary>
		/// 构造方法
		/// </summary>
		public LmtbSnmpEx()
		{
		}
	
		/// <summary>
		/// 功能描述：Snmp的初始化工作，包括：
		/// 1，socket初始化
		/// 2，设置目标Agent端IP地址
		/// 3，设置目标Agent端消息监听端口
		/// 4，设置超时值
		/// 5，设置Read，Write的Community
		/// 6，设置重传次数
		/// 7，设置Snmp的版本号
		/// 8，设置Inform和Trap消息监听端口
		/// 9，注册异步回调函数
		/// 10，创建事情处理线程
		/// </summary>
		/// <param name="commnuity">Commnuity</param>
		/// <param name="destIpAddr">目标Ip</param>
		/// <returns></returns>
		public int SnmpLibStartUp(string commnuity, string destIpAddr)
		{
			Log.Debug("========== SnmpLibStartUp() Start ==========");

			// 建立Snmp回话
			CreateSnmpSession(commnuity, destIpAddr);

			Log.Debug("========== SnmpLibStartUp() End ==========");

			return 0;
		}

		/// <summary>
		/// 创建snmp会话，包括同步、异步两这个snmp连接
		/// </summary>
		/// <param name="commnuity">Commnuity</param>
		/// <param name="destIpAddr">目标Ip</param>
		/// <returns></returns>
		public int CreateSnmpSession(string commnuity, string destIpAddr)
		{
			var status = 0;
			// 同步snmp连接
			m_SnmpSync = new SnmpHelperV2(commnuity, destIpAddr);
			// 异步snmp连接
			m_SnmpAsync = new SnmpHelperV2(commnuity, destIpAddr);

			return status;
		}

		/// <summary>
		/// 同步Get操作
		/// </summary>
		/// <param name="lmtPdu">LmtPdu实例</param>
		/// <param name="requestId">请求编号</param>
		/// <param name="strIpAddr">目的Ip</param>
		/// <param name="timeOut">超时时间</param>
		/// <returns></returns>
		public int SnmpGetSync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr, long timeOut)
		{
			requestId = 0;

			var msg = $"参数: lmtPdu={lmtPdu}, requestId={requestId}, strIpAddr={strIpAddr}, timeOut={timeOut}";
			Log.Debug(msg);

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("参数strIpAddr为空");
				return -1;
			}
			if (lmtPdu == null)
			{
				Log.Error("参数lmtPdu为空");
				return -1;
			}

			var snmp = m_SnmpSync;
			if (null == snmp)
			{
				msg = $"基站[{strIpAddr}]的snmp连接不存在，无法下发snmp命令";
				Log.Error(msg);
				return -1;
			}

			lmtPdu.setReqMsgType((int)PduType.Get);

			Pdu pdu;
			var rs = LmtPdu2SnmpPdu(out pdu, lmtPdu, strIpAddr);
			if (!rs)
			{
				Log.Error("LmtPdu2SnmpPdu()转换错误");
				return -1;
			}

			var result = snmp.GetRequest(pdu);
			if (result == null)
			{
				Log.Error("SNMP request error, response is null.");
				return -1;
			}

			requestId = result.Pdu.RequestId;

			if (!SnmpPdu2LmtPdu(result.Pdu, snmp.m_target, ref lmtPdu, 0, false))
			{
				Log.Error("调用SnmpPdu2LmtPdu()方法错误！");
				return -1;
			}

			// 实例序列化
			var bytes = SerializeHelper.Serialize2Binary(lmtPdu);
			// 发布消息
			PublishHelper.PublishMsg(TopicHelper.SnmpMsgDispose_OnResponse, bytes);

			return 0;
		}

		/// <summary>
		/// 同步Get操作，根据results.Count()是否为0判断是否有返回结果
		/// </summary>
		/// <param name="strIpAddr">目的Ip</param>
		/// <param name="oidList">Oid列表</param>
		/// <param name="results">请求的响应结果</param>
		/// <param name="timeout">超时时间</param>
		/// <returns></returns>
		public int SnmpGetSync(string strIpAddr, List<string> oidList, out Dictionary<string, string> results, long timeout)
		{
			// 初始化out参数
			results = new Dictionary<string, string>();
			// log msg
			string logMsg;

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("参数strIpAddr为空！");
				return -1;
			}
			if (oidList == null)
			{
				Log.Error("参数oidList为空！");
				return -1;
			}

			var snmp = m_SnmpSync;
			if (null == snmp)
			{
				logMsg = $"基站[{strIpAddr}]的snmp连接不存在，无法下发snmp命令";
				Log.Error(logMsg);
				return -1;
			}

			Pdu pdu = new Pdu();

			foreach (string oid in oidList)
			{
				var vb = new Vb(new Oid(oid));
				pdu.VbList.Add(vb);
			}

			SnmpPacket reqResult = null;
			try
			{
				reqResult = snmp.GetRequest(pdu);

			}
			catch(Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}

			if (null != reqResult)
			{
				if (reqResult.Pdu.ErrorStatus != 0)
				{
					SnmpErrorParser.PrintPduError(reqResult.Pdu);
					return -1;
				}
				else
				{
					foreach (var vb in reqResult.Pdu.VbList)
					{
						logMsg = $"Oid={vb.Oid.ToString()}, Type={SnmpConstants.GetTypeName(vb.Value.Type)}, Value={vb.Value.ToString()}";
						Log.Info(logMsg);

						// 根据Mib类型转换为可显示字符串
						string strValue = null;
						SnmpMibUtil.ConvertSnmpVal2MibStr(strIpAddr, vb, out strValue);

						results.Add(vb.Oid.ToString(), strValue);
					}
				}
			}
			else
			{
				Log.Error("SNMP GetNextRequest请求错误");
				return -1;
			}

			return 0;
		}

		/// <summary>
		/// 同步Get操作(仅提供给SnmpSessionWorker调用)
		/// </summary>
		/// <param name="strIpAddr">目的Ip</param>
		/// <param name="queryVbs">LmtbVb实例</param>
		/// <param name="results">请求的响应结果</param>
		/// <param name="timeout">超时时间</param>
		/// <returns></returns>
		public bool SnmpGetSync(string strIpAddr, List<CDTLmtbVb> queryVbs, out Dictionary<string, string> results, long timeout)
		{
			// 初始化out参数
			results = new Dictionary<string, string>();

			// log msg
			string logMsg;
			bool status;

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("参数strIpAddr为空！");
				return false;
			}

			var snmp = m_SnmpSync;
			if (null == snmp)
			{
				logMsg = $"基站[{strIpAddr}]的snmp连接不存在，无法下发snmp命令";
				Log.Error(logMsg);
				return false;
			}

			Pdu pdu;
			PacketQueryPdu(queryVbs, out pdu);

			var ReqResult = snmp.GetRequest(pdu);

			if (null != ReqResult)
			{
				if (ReqResult.Pdu.ErrorStatus != 0)
				{
					SnmpErrorParser.PrintPduError(ReqResult.Pdu);
					status = false;
				}
				else
				{
					foreach (var vb in ReqResult.Pdu.VbList)
					{
						logMsg = $"Oid={vb.Oid.ToString()}, Type={SnmpConstants.GetTypeName(vb.Value.Type)}, Value={vb.Value.ToString()}";
						Log.Info(logMsg);

						// 根据Mib类型转换为可显示字符串
						string strValue = null;
						SnmpMibUtil.ConvertSnmpVal2MibStr(strIpAddr, vb, out strValue);

						results.Add(vb.Oid.ToString(), strValue);
					}
					status = true;
				}
			}
			else
			{
				Log.Error("SNMP GetNextRequest请求错误");
				return false;
			}

			return status;
		}

		

		/// <summary>
		/// 异步Get操作
		/// </summary>
		/// <param name="lmtPdu">LmtPdu实例</param>
		/// <param name="requestId">请求Id</param>
		/// <param name="strIpAddr">目的Ip</param>
		/// <returns></returns>
		public int SnmpGetAsync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr)
		{
			requestId = 0;

			var msg = $"参数: lmtPdu={lmtPdu}, requestId={requestId}, strIpAddr={strIpAddr}";
			Log.Debug(msg);

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("参数strIpAddr为空");
				return -1;
			}
			if (lmtPdu == null)
			{
				Log.Error("参数lmtPdu为空");
				return -1;
			}

			var snmp = m_SnmpAsync;
			if (null == snmp)
			{
				msg = string.Format("基站[{0}]的snmp连接不存在，无法下发snmp命令", strIpAddr);
				Log.Error(msg);
				return -1;
			}

			lmtPdu.setReqMsgType((int)PduType.Get);

			var pdu = new Pdu();
			requestId = pdu.RequestId;

			var rs = LmtPdu2SnmpPdu(out pdu, lmtPdu, strIpAddr);
			if (!rs)
			{
				Log.Error("LmtPdu2SnmpPdu()转换错误");
				return -1;
			}

			var callback = new SnmpAsyncResponse(SnmpCallbackFun);

			var status = snmp.GetRequestAsync(pdu, callback);

			if (!status)
			{
				Log.Error("SNMP Get异步请求错误");
			}

			// 缓存异步消息
			var appendInfo = new stru_LmtbPduAppendInfo();
			lmtPdu.getAppendValue(appendInfo);
			Push_appendInfo(requestId, appendInfo);

			return 0;
		}

		/// <summary>
		/// GetNextRequest
		/// </summary>
		/// <param name="strIpAddr">目的Ip</param>
		/// <param name="queryVbs">CDTLmtbVb实例</param>
		/// <param name="result">响应结果</param>
		/// <param name="timeout">超时时间</param>
		/// <returns></returns>
		public bool GetNextRequest(string strIpAddr, List<CDTLmtbVb> queryVbs, out Dictionary<string, string> result, long timeout)
		{
			result = new Dictionary<string, string>();

			var status = false;
			string logMsg;

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("参数strIpAddr为空");
				return false;
			}
			if (queryVbs == null)
			{
				Log.Error("参数queryVbs为空");
				return false;
			}

			var snmp = m_SnmpSync;
			if (null == snmp)
			{
				logMsg = $"基站[{strIpAddr}]的snmp连接不存在，无法下发snmp命令";
				Log.Error(logMsg);
				return false;
			}

			Pdu pdu;
			PacketQueryPdu(queryVbs, out pdu);

			var ReqResult = (SnmpV2Packet)snmp.GetNextRequest(pdu);

			if (null != ReqResult)
			{
				if (ReqResult.Pdu.ErrorStatus != 0)
				{
					SnmpErrorParser.PrintPduError(ReqResult.Pdu);

					status = false;

					SnmpErrDescHelper.SetLastErrorCode(ReqResult.Pdu.ErrorStatus);

					// 资源不可用的情况需要上报
					if (ReqResult.Pdu.ErrorStatus == SnmpConstants.ErrResourceUnavailable)// endOfMibView
					{
						var errIndex = ReqResult.Pdu.ErrorIndex;
						if (errIndex == 1)
						{
							var errVb = ReqResult.Pdu.VbList.ElementAt(0);
							var errValue = errVb.Value.ToString();
							if (errValue.IndexOf("end-of-mib-view", StringComparison.OrdinalIgnoreCase) >= 0)
							{
								return true;
							}
						}
						else
						{
							ShowLogHelper.Show("执行snmp操作失败，错误码：13，描述：资源不可达", strIpAddr, InfoTypeEnum.ENB_GETOP_ERR_INFO);
							return false;
						}
					}
				}
				else
				{
					foreach (var vb in ReqResult.Pdu.VbList)
					{
						//logMsg = string.Format("ObjectName={0}, Type={1}, Value={2}"
						//	, vb.Oid.ToString(), SnmpConstants.GetTypeName(vb.Value.Type), vb.Value.ToString());
						// Log.Debug(logMsg);
						// 根据Mib类型转换为可显示字符串
						string strValue = null;
						SnmpMibUtil.ConvertSnmpVal2MibStr(strIpAddr, vb, out strValue);

						result.Add(vb.Oid.ToString(), strValue);
					}
					status = true;
				}
			}
			else
			{
				Log.Error("SNMP GetNextRequest请求错误");
				return false;
			}

			return status;
		}

		/// <summary>
		/// GetNextRequest
		/// 说明：
		/// Mib节点的值已经进行了转换,如：日期->2018-11-01 10:20:05；IP->192.168.5.1
		/// </summary>
		/// <param name="strIpAddr">Enb ip,目前是每个基站生成一个实例，索引IP没有到，先保留以便后期变更</param>
		/// <param name="reqOidList">要请求的oid列表</param>
		/// <param name="oidValue">请求结果，oid与值对应关系</param>
		/// <param name="lastOidList">返回最新的oid列表，方便循环查找使用</param>
		/// <returns>
		/// FALSE:错误；TRUE:检索到数据或检索结束
		/// 是否获取到数据要根据oidValue.count()判断，而不是函数的返回值来判断
		/// </returns>
		public bool GetNextRequest(string strIpAddr, List<string> reqOidList, out Dictionary<string, string> oidValue
			,out List<string> lastOidList)
		{
			// 是否获取到数据要根据oidValue.count()判断，而不是函数的返回值来判断

			oidValue = new Dictionary<string, string>();
			lastOidList = new List<string>();

			var status = false;
			string logMsg;

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("strIpAddr is null");
				return false;
			}
			if (reqOidList == null)
			{
				Log.Error("参数reqOidList为空");
				return false;
			}

			var snmp = m_SnmpSync;
			if (null == snmp)
			{
				logMsg = $"基站[{strIpAddr}]的snmp连接不存在，无法下发snmp命令";
				Log.Error(logMsg);
				return false;
			}

			Pdu pdu;
			PacketQueryPdu(reqOidList, out pdu);

			var ReqResult = (SnmpV2Packet)snmp.GetNextRequest(pdu);

			if (null != ReqResult)
			{
				if (ReqResult.Pdu.ErrorStatus != 0)// 状态码
				{
					SnmpErrorParser.PrintPduError(ReqResult.Pdu);
					status = false;
					// 状态码为106 下发报文中绑定变量个数不应大于60个，此时要返回
					if (ReqResult.Pdu.ErrorStatus == 106)
					{

					}
					// 如果ErrorStatus!=0且ErrorIndex=0就表示检索没有结束，就要组装新的Oid
					if (ReqResult.Pdu.ErrorIndex == 0)
					{
						foreach (var vb in ReqResult.Pdu.VbList)
						{
							// 如果返回的Oid与传入的Oid相等则说明有错误，要返回错误，避免造成死循环
							if (reqOidList.Contains(vb.Oid.ToString()))
							{
								return false;
							}
							// 根据Mib类型转换为可显示字符串
							string strValue = null;
							SnmpMibUtil.ConvertSnmpVal2MibStr(strIpAddr, vb, out strValue);

							// 查询结果
							oidValue.Add(vb.Oid.ToString(), strValue);
							// 返回最新的oid，方便循环调用时作为入参使用
							lastOidList.Add(vb.Oid.ToString());
						}
						return true;
					}

					if (ReqResult.Pdu.VbList.Count > 0)
					{
						// 第一个Vb的值
						string firstVbVal = ReqResult.Pdu.VbList.ElementAt(0).Value.ToString();
						// 只有状态码为13并且错误索引为1并且第一个vb的值为endOfMibView，才表示检索结束
						if (ReqResult.Pdu.ErrorStatus == SnmpConstants.ErrResourceUnavailable 
							&& firstVbVal.IndexOf("end-of-mib-view", StringComparison.OrdinalIgnoreCase) >= 0)
						{
							return true;
						}
						else // 其他错误
						{
							logMsg = string.Format("SNMP GetNext错误！ ErrorIndex:{0}, Value:{1}", ReqResult.Pdu.ErrorIndex, firstVbVal);
							Log.Error(logMsg);
							return false;
						}
					}
					
				}
				else
				{
					foreach (var vb in ReqResult.Pdu.VbList)
					{
						// 根据Mib类型转换为可显示字符串
						string strValue = null;
						SnmpMibUtil.ConvertSnmpVal2MibStr(strIpAddr, vb, out strValue);

						// 查询结果
						oidValue.Add(vb.Oid.ToString(), strValue);
						// 返回最新的oid，方便循环调用时作为入参使用
						lastOidList.Add(vb.Oid.ToString());
					}
					status = true;
				}
			}
			else
			{
				Log.Error("SNMP GetNextRequest请求错误");
				return false;
			}

			return status;
		}

		/// <summary>
		/// 使用GetNext循环获取一个表的数据
		/// </summary>
		/// <param name="IpAddr">目标IP，目前没有使用，暂时保留防止以后变更</param>
		/// <param name="oidList">查询Oid列表</param>
		/// <param name="oidValueTable">查询结果，二维表结构，一个Dictionary是一行数据</param>
		/// <returns>
		/// False:发生错误；True:未发生错误；
		/// 是否获取到数据要根据oidValueTable.count()判断，而不是函数的返回值来判断
		/// </returns>
		public bool SnmpGetNextLoop(string strIpAddr, List<string> oidList, out List<Dictionary<string, string>> oidValueTable)
		{
			oidValueTable = new List<Dictionary<string, string>>();
			if (oidList == null)
			{
				Log.Error("参数oidList为空");
				return false;
			}
			List<string> oidListTmp = new List<string>(oidList);

			// 请求返回的最新oid
			List<string> lastOidValue = new List<string>();
			// 一行数据
			Dictionary<string, string> oidValueLine = null;
			// 循环获取每一行数据，直至结束
			while(true)
			{
				GetNextRequest(strIpAddr, oidListTmp, out oidValueLine, out lastOidValue);
				if (oidValueLine.Count() > 0) // 数据存在
				{
					// 查询结果
					oidValueTable.Add(oidValueLine);
					// 新的Oid
					oidListTmp = new List<string>(lastOidValue);
				}
				else // 获取结束
				{
					Log.Info("GetNext检索结束");
					break;
				}
			}

			return true;
		}


		/// <summary>
		/// 同步Set操作
		/// 说明：
		/// 方法返回值为0只代表SNMP消息下发成功，具体SNMP命令的执行结果还要根据SNMP响应状态码来判断
		/// </summary>
		/// <param name="lmtPdu">LmtPdu实例</param>
		/// <param name="requestId">请求Id</param>
		/// <param name="strIpAddr">目标Ip</param>
		/// <param name="timeOut">超时时间</param>
		/// <returns></returns>
		public int SnmpSetSync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr, long timeOut)
		{
			requestId = 0;

			var msg = $"pars: lmtPdu={lmtPdu}, requestId={requestId}, strIpAddr={strIpAddr}, timeOut={timeOut}";
			Log.Debug(msg);

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("参数strIpAddr为空");
				return -1;
			}
			if (lmtPdu == null)
			{
				Log.Error("参数lmtPdu为空");
				return -1;
			}

			var snmp = m_SnmpSync;
			if (null == snmp)
			{
				msg = $"基站[{strIpAddr}]的snmp连接不存在，无法下发snmp命令";
				Log.Error(msg);
				return -1;
			}

			lmtPdu.setReqMsgType((int)PduType.Set);
			lmtPdu.m_SourceIp = strIpAddr;

			var pdu = new Pdu();
			var rs = LmtPdu2SnmpPdu(out pdu, lmtPdu, strIpAddr);

			var result = snmp.SetRequest(pdu);

			if (result == null)
			{
				Log.Error("SNMP request error, response is null.");
				return -1;
			}

/*			if (0 != result.Pdu.ErrorStatus)
			{
				SnmpErrDescHelper.SetLastErrorCode(result.Pdu.ErrorStatus);
				return 2;
			}
*/
			rs = SnmpPdu2LmtPdu(result.Pdu, snmp.m_target, ref lmtPdu, 0, false);

			// 实例序列化
			var bytes = SerializeHelper.Serialize2Binary(lmtPdu);
			// 发布消息
			PublishHelper.PublishMsg(TopicHelper.SnmpMsgDispose_OnResponse, bytes);
			return 0;
		}

		/// <summary>
		/// SNMP Set同步操作(仅提供给SnmpSessionWorker调用)
		/// </summary>
		/// <param name="strIpAddr">目标Ip</param>
		/// <param name="setVbs">CDTLmtbVb实例</param>
		/// <param name="timeOut">超时</param>
		/// <returns></returns>
		public bool SnmpSetSync(string strIpAddr, List<CDTLmtbVb> setVbs, long timeOut)
		{
			//	Log.Debug("========== SnmpSetSync() Start ==========");
			//var logMsg = $"pars: strIpAddr={strIpAddr}, timeOut={timeOut}";
			//			Log.Debug(logMsg);

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("参数strIpAddr为空");
				return false;
			}

			var snmp = m_SnmpSync;
			if (null == snmp)
			{
				Log.Error($"基站[{strIpAddr}]的snmp连接不存在，无法下发snmp命令");
				return false;
			}

			Pdu pdu;
			PacketSetPdu(setVbs, out pdu);

			var result = snmp.SetRequest(pdu);

			if (result == null)
			{
				Log.Error("SNMP set request error, response is null.");
				return false;
			}

			if (result.Pdu.ErrorStatus != 0)
			{
				SnmpErrorParser.PrintPduError(result.Pdu);
				return false;
			}

			return true;
		}

		/// <summary>
		/// 异步Set操作
		/// </summary>
		/// <param name="lmtPdu">CDTLmtbPdu实例</param>
		/// <param name="requestId">请求Id</param>
		/// <param name="strIpAddr">目标Ip</param>
		/// <returns></returns>
		public int SnmpSetAsync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr)
		{
			requestId = 0;

			var msg = $"pars: lmtPdu={lmtPdu}, requestId={requestId}, strIpAddr={strIpAddr}";
			Log.Debug(msg);

			if (string.IsNullOrEmpty(strIpAddr))
			{
				Log.Error("参数strIpAddr为空");
				return -1;
			}
			if (lmtPdu == null)
			{
				Log.Error("参数lmtPdu为空");
				return -1;
			}

			var snmp = m_SnmpAsync;
			if (null == snmp)
			{
				msg = string.Format("基站[{0}]的snmp连接不存在，无法下发snmp命令");
				Log.Error(msg);
				return -1;
			}

			lmtPdu.setReqMsgType((int)PduType.Set);

			var pdu = new Pdu();
			requestId = pdu.RequestId;
			// TODO:
			var rs = LmtPdu2SnmpPdu(out pdu, lmtPdu, strIpAddr);
			if (!rs)
			{
				Log.Error("LmtPdu2SnmpPdu()转换错误");
			}

			var snmpCallback = new SnmpAsyncResponse(this.SnmpCallbackFun);

			rs = snmp.SetRequestAsync(pdu, snmpCallback);

			if (!rs)
			{
				Log.Error("SNMP 异步请求错误");
			}

			// 缓存异步请求信息
			var appendInfo = new stru_LmtbPduAppendInfo();
			lmtPdu.getAppendValue(appendInfo);
			Push_appendInfo(requestId, appendInfo);

			return 0;
		}

		/// <summary>
		/// 将LmtPdu转换为snmpPdu
		/// </summary>
		/// <param name="pdu">Snmp Pud实例</param>
		/// <param name="lmtPdu">CDTLmtbPdu实例</param>
		/// <param name="strRemoteIp">目标Ip</param>
		/// <returns></returns>
		private bool LmtPdu2SnmpPdu(out Pdu pdu, CDTLmtbPdu lmtPdu, string strRemoteIp)
		{
			pdu = new Pdu();
			string strTmpOid;
			string strValue;
			SNMP_SYNTAX_TYPE strSyntaxType;

			var lmtVbCount = lmtPdu.VbCount();
			for (var i = 0; i < lmtVbCount; i++)
			{
				var cDTLmtbVb = lmtPdu.GetVbByIndexEx(i);

				strTmpOid = cDTLmtbVb.Oid;	// todo 存在问题：当oid最后索引是0..35这种值时，出现崩溃
				strSyntaxType = cDTLmtbVb.SnmpSyntax;
				strValue = cDTLmtbVb.Value;

				var vb = new Vb(new Oid(strTmpOid));

				var strNodeType = SnmpMibUtil.GetNodeTypeByOIDInCache(strRemoteIp, strTmpOid);

				// 组装SNMP协议的VB
				SnmpMibUtil.SetVbValue(ref vb, strSyntaxType, strValue, strNodeType);

				pdu.VbList.Add(vb);
			}

			return true;
		}

		/// <summary>
		/// 将snmp类型的pdu转换为LmtSnmp的pdu
		/// </summary>
		/// <param name="pdu">Snmp Pdu实例</param>
		/// <param name="target">目标连接</param>
		/// <param name="lmtPdu">CDTLmtbPdu实例</param>
		/// <param name="reason"></param>
		/// <param name="isAsync"></param>
		private bool SnmpPdu2LmtPdu(Pdu pdu, UdpTarget target, ref CDTLmtbPdu lmtPdu, int reason, bool isAsync)
		{
			if (lmtPdu == null)
			{
				Log.Error("参数[lmtPdu]为空");
				return false;
			}

			var appendInfo = new stru_LmtbPduAppendInfo { m_bIsSync = !isAsync };

			var logMsg = $"snmpPackage.Pdu.Type = {pdu.Type}";
			Log.Debug(logMsg);

			// 判断响应消息类型
			if (pdu.Type != PduType.V2Trap) // 非Trap消息
			{
				if (isAsync)
				{
					//如果该操作是异步Snmp命令发起的，该函数必定在Rsp_CallBack里被调用，lmtPdu里没有任何信息，需要从map里取
					if (false == Pop_appendInfo(pdu.RequestId, ref appendInfo))
					{
						Log.Error($"找不到requestId={pdu.RequestId}的PduAppendInfo实例");
						return false;
					}
				}
				else //如果是同步命令，则lmtPdu里已经包含了Pdu信息
				{
					lmtPdu.getAppendValue(appendInfo);
				}
			}
			else // Trap消息
			{
				appendInfo.m_bIsNeedPrint = true;
			}

			lmtPdu.Clear();
			lmtPdu.m_LastErrorIndex = pdu.ErrorIndex;
			lmtPdu.m_LastErrorStatus = pdu.ErrorStatus;
			lmtPdu.m_requestId = pdu.RequestId;
			lmtPdu.assignAppendValue(appendInfo);

			// 设置IP和端口信息
			var srcIpAddr = target.Address;
			var port = target.Port;
			lmtPdu.m_SourceIp = srcIpAddr.ToString();
			lmtPdu.m_SourcePort = port;

			lmtPdu.reason = reason;
			lmtPdu.m_type = (ushort)pdu.Type;

			//如果是错误的响应，则直接返回
			if (lmtPdu.m_LastErrorStatus != 0 || reason == -5)
			{
				SnmpErrDescHelper.SetLastErrorCode((int)lmtPdu.m_LastErrorStatus);
				return false;
			}

			// 获取MIB前缀
			var prefix = SnmpToDatabase.GetMibPrefix().Trim('.');
			if (string.IsNullOrEmpty(prefix))
			{
				Log.Error("获取MIB前缀失败!");
				return false;
			}

			// 对于Trap消息,我们自己额外构造两个Vb，用来装载时间戳和trap Id
			if (pdu.Type == PduType.V2Trap) // Trap
			{
				// 构造时间戳Vb
				var lmtVb = new CDTLmtbVb();
				lmtVb.Oid = "时间戳";
				// TODO 是这个时间戳吗？？？？
				lmtVb.Value = pdu.TrapSysUpTime.ToString();
				lmtVb.SnmpSyntax = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OID;
				lmtPdu.AddVb(lmtVb);

				// 构造Trap Id Vb
				lmtVb = new CDTLmtbVb();
				lmtVb.Oid = "notifyid";
				// TODO 对吗？？？
				lmtVb.Value = pdu.TrapObjectID.ToString();
				lmtVb.SnmpSyntax = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OID;
				lmtPdu.AddVb(lmtVb);
			}

			foreach (var vb in pdu.VbList)
			{
				logMsg =
					$"ObjectName={vb.Oid.ToString()}, Type={SnmpConstants.GetTypeName(vb.Value.Type)}, Value={vb.Value.ToString()}";
				Log.Debug(logMsg);

				var lmtVb = new CDTLmtbVb();

				lmtVb.Oid = vb.Oid.ToString();

				var strValue = vb.Value.ToString();

				lmtVb.SnmpSyntax = (SNMP_SYNTAX_TYPE)vb.Value.Type;

				// 如果是getbulk响应返回的SNMP_SYNTAX_ENDOFMIBVIEW，则不处理这个vb，继续
				if (lmtVb.SnmpSyntax == SNMP_SYNTAX_TYPE.SNMP_SYNTAX_ENDOFMIBVIEW)
				{
					lmtPdu.isEndOfMibView = true;
					continue;
				}

				// 将SNMP节点值转换为可以显示的文本字符串
				SnmpMibUtil.ConvertSnmpVal2MibStr(target.Address.ToString(), vb, out strValue);

				lmtVb.Value = strValue;
				lmtPdu.AddVb(lmtVb);

			} // end foreach

			//如果得到的LmtbPdu对象里的vb个数为0，说明是是getbulk响应，并且没有任何实例
			//为方便后面统一处理，将错误码设为资源不可得
			if (lmtPdu.VbCount() == 0)
			{
				lmtPdu.m_LastErrorStatus = SnmpConstants.ErrResourceUnavailable;
				lmtPdu.m_LastErrorIndex = 1;
			}

			return true;
		}

		/// <summary>
		/// SNMP请求回调方法
		/// </summary>
		/// <param name="result"></param>
		/// <param name="packet"></param>
		private void SnmpCallbackFun(AsyncRequestResult result, SnmpPacket packet)
		{
			var logMsg = $"SNMP异步请求结果: AsyncRequestResult = {result}";
			Log.Info(logMsg);

			if (result == AsyncRequestResult.NoError)
			{
				var packetv2 = (SnmpV2Packet)packet;

				if (packetv2 == null)
				{
					Log.Error("SNMP request error, response is null.");
					return;
				}

				var lmtPdu = new CDTLmtbPdu();
				var rs = SnmpPdu2LmtPdu(packetv2.Pdu, m_SnmpAsync.m_target, ref lmtPdu, 0, false);

				// TODO
				// 发消息
				if (packetv2.Pdu.Type == PduType.Inform)
				{
				}
			}

			return;
		}

		/// <summary>
		/// 将CDTLmtbVb数组转换为SNMP Pdu
		/// </summary>
		/// <param name="queryVbs"></param>
		/// <param name="pdu"></param>
		public void PacketQueryPdu(List<CDTLmtbVb> queryVbs, out Pdu pdu)
		{
			pdu = new Pdu(PduType.GetNext);

			if (queryVbs == null)
			{
				Log.Error("参数queryVbs为空");
				return;
			}

			foreach (var lmtVb in queryVbs)
			{
				var vb = new Vb(new Oid(lmtVb.Oid));
				pdu.VbList.Add(vb);
			}
		}

		public void PacketQueryPdu(List<string> oidList, out Pdu pdu)
		{
			pdu = new Pdu(PduType.GetNext);
			if (null == oidList)
			{
				return;
			}

			foreach (var oid in oidList)
			{
				pdu.VbList.Add(new Vb(oid));
			}
		}

		/// <summary>
		/// 组装Set Pdu
		/// </summary>
		/// <param name="setVbs"></param>
		/// <param name="setPdu"></param>
		public void PacketSetPdu(List<CDTLmtbVb> setVbs, out Pdu setPdu)
		{
			setPdu = new Pdu(PduType.Set);

			foreach (var lmtbVb in setVbs)
			{
				var vb = new Vb(new Oid(lmtbVb.Oid));

				// 组装SNMP协议的VB
				SnmpMibUtil.SetVbValue(ref vb, lmtbVb.SnmpSyntax, lmtbVb.Value);

				setPdu.VbList.Add(vb);
			}
		}

		/// <summary>
		/// 数据库里的节点类型描述到Snmp Syntax的映射
		/// </summary>
		/// <param name="strType"></param>
		/// <returns></returns>
		public static SNMP_SYNTAX_TYPE GetSyntax(string strType)
		{
			return m_ValType2SynTax[strType];
		}

		/// <summary>
		/// 保存异步SNMP请求信息
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="appendInfo"></param>
		public void Push_appendInfo(long requestId, stru_LmtbPduAppendInfo appendInfo)
		{
			m_ReqIdPduInfo[requestId] = appendInfo;
		}

		/// <summary>
		/// 获取SNMP异步缓存信息
		/// </summary>
		/// <param name="requestId"></param>
		/// <param name="appendInfo"></param>
		/// <returns></returns>
		public bool Pop_appendInfo(long requestId, ref stru_LmtbPduAppendInfo appendInfo)
		{
			var rs = false;
			foreach (var item in m_ReqIdPduInfo)
			{
				if (item.Key == requestId)
				{
					appendInfo = item.Value;
					// 移除
					m_ReqIdPduInfo.Remove(requestId);
					rs = true;
				}
			}

			return rs;
		}
	}
}