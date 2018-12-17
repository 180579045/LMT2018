/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：SnmpHelper.cs
// 文件功能描述：Snmp报文类;
// 创建人：;
// 版本：V1.0
// 创建标识：创建文件;
// 时间：
//----------------------------------------------------------------*/

using System;
using System.Net;
using LogManager;
using SnmpSharpNet;
using System.Collections.Generic;

namespace LmtbSnmp
{
	/// <summary>
	/// 抽象SNMP报文，以便后续扩展SNMPV3;
	/// 注意：此类为底层封装，其他模块不得直接使用此类
	/// </summary>
	public abstract class SnmpHelper
	{
		// 代理目标IP地址
		public string m_DestIPAddr { get; set; }
		// 目的端口                          
		public int m_DestPort { get { return 161; } set { m_DestPort = value; } }
		// 代理目标的Community
		public string m_Community { get; set; }
		// 设置的超时时间(10s超时，以10ms为单位)
		public int m_TimeOut { get { return 2000; } set { m_TimeOut = value; } }
		// 重试次数
		public int m_Retry { get { return 1; } set { m_Retry = value; } }
		// SNMP版本,当前基站使用SNMP固定为Ver.2
		public SnmpVersion m_Version { get { return SnmpVersion.Ver2; } set { m_Version = value; } } 
		// Target
		public UdpTarget m_target { get; set; }
		// 代理参数
		public AgentParameters m_Param { get; set; }

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="commnuity"></param>
		/// <param name="destIpAddr"></param>
		public SnmpHelper(string commnuity, string destIpAddr)
		{
			string logMsg = string.Format("Pars: commnuity={0}, destIpAddr={1}", commnuity, destIpAddr);
			Log.Info(logMsg);

			this.m_DestIPAddr = destIpAddr;
			this.m_Community = commnuity;

			ConnectToAgent(m_Community, m_DestIPAddr);
		}

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="commnuity"></param>
		/// <param name="destIpAddr"></param>
		/// <param name="destPort"></param>
		/// <param name="version"></param>
		/// <param name="timeOut"></param>
		public SnmpHelper(string commnuity, string destIpAddr, int destPort, SnmpVersion version, int timeOut, int retry)
		{
			string logMsg = string.Format("Pars: commnuity={0}, destIpAddr={1}, destPort={2}, version={3}, timeOut={4}, retry={5}"
				, commnuity, destIpAddr, destPort, version, timeOut, retry);
			Log.Info(logMsg);

			this.m_Community = commnuity;
			this.m_DestIPAddr = destIpAddr;
			this.m_DestPort = destPort;
			this.m_Version = version;
			this.m_TimeOut = timeOut;
			this.m_Retry = retry;

			ConnectToAgent(m_Community, m_DestIPAddr);
		}

		/// <summary>
		/// Get请求
		/// </summary>
		/// <param name="pdu"></param>
		/// <returns></returns>
		public abstract SnmpPacket GetRequest(Pdu pdu);

		/// <summary>
		/// 异步Get请求
		/// </summary>
		/// <param name="pdu"></param>
		/// <param name="callback"></param>
		/// <returns></returns>
		public abstract bool GetRequestAsync(Pdu pdu, SnmpAsyncResponse callback);
		
		/// <summary>
		/// GetNext请求
		/// </summary>
		/// <param name="pdu"></param>
		/// <returns></returns>
		public abstract SnmpPacket GetNextRequest(Pdu pdu);

		/// <summary>
		/// Set请求
		/// </summary>
		/// <param name="pdu"></param>
		/// <returns></returns>
		public abstract SnmpPacket SetRequest(Pdu pdu);

		/// <summary>
		/// 异步Set请求
		/// </summary>
		/// <param name="pdu"></param>
		/// <param name="callback"></param>
		/// <returns></returns>
		public abstract bool SetRequestAsync(Pdu pdu, SnmpAsyncResponse callback);

		/// <summary>
		/// Snmp连接代理方法
		/// </summary>
		/// <param name="Community"></param>
		/// <param name="IpAddr"></param>
		/// <returns></returns>
		protected UdpTarget ConnectToAgent(string community, string ipAddr)
		{
			string logMsg = string.Format("Pars: community={0}, ipAddr={1}", community, ipAddr);
			Log.Info(logMsg);

			OctetString OctCommunity = new OctetString(community);
			m_Param = new AgentParameters(OctCommunity);
			m_Param.Version = SnmpVersion.Ver2;

			IPAddress agent = IPAddress.Parse(ipAddr);

			try
			{
				// 创建代理(基站);
				m_target = new UdpTarget(agent, m_DestPort, m_TimeOut, m_Retry);
			}
			catch (Exception ex)
			{
				Log.Error(ex.Message);
				throw;
			}

			return m_target;
		}

	}

	/// <summary>
	/// Snmp报文V2c版本
	/// </summary>
	public class SnmpHelperV2 : SnmpHelper
	{
		// 一个基站对应一个SnmpHelper实例;
		// 保证同一基站同一时刻只有一个线程进行SNMP交互，否则会出现“Invalid request id in reply”异常
		private readonly object lockObj = new object();

		/// <summary>
		/// 构造方法，此类为SNMP的底层封装，其他程序集要通过LmtbSnmpEx实例访问，不可直接访问,因此访问权限为internal修饰
		/// </summary>
		/// <param name="Commnuity"></param>
		/// <param name="ipaddr"></param>
		internal SnmpHelperV2(string Commnuity, string ipaddr) : base(Commnuity, ipaddr)
		{}

		/// <summary>
		/// Get请求
		/// </summary>
		/// <param name="pdu">Pdu实例</param>
		/// <returns></returns>
		public override SnmpPacket GetRequest(Pdu pdu)
		{
			// Log msg
			string logMsg;

			if (m_target == null)
			{
				logMsg = "SNMP代理连接m_target为空";
				Log.Error(logMsg);
				return null;
			}

			if (pdu == null)
			{
				logMsg = "SNMP请求参数pdu为空";
				Log.Error(logMsg);
				return null;
			}

			SnmpV2Packet result;

			pdu.Type = PduType.Get;
			try
			{
				lock (lockObj)
				{
					result = (SnmpV2Packet)m_target.Request(pdu, m_Param);
					// TODO:函数没处理内容
					//HandleGetRequestResult(result);
				}
			}
			catch (SnmpException e1)
			{
				Log.Error(e1.Message);
				throw;
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				throw;
			}

			return result;
		}

		/// <summary>
		/// 异步GetRequest
		/// </summary>
		/// <param name="pdu">Pdu实例</param>
		/// <param name="callback">回调方法</param>
		/// <returns></returns>
		public override bool GetRequestAsync(Pdu pdu, SnmpAsyncResponse callback)
		{
			// Log msg
			string logMsg;

			bool rs = false;
			if (m_target == null)
			{
				logMsg = "SNMP代理连接m_target为空";
				Log.Error(logMsg);
				return false;
			}

			if (pdu == null)
			{
				logMsg = "SNMP请求参数pdu为空";
				Log.Error(logMsg);
				return false;
			}
			pdu.Type = PduType.Get;

			try
			{
				rs = m_target.RequestAsync(pdu, m_Param, callback);
				if (!rs)
				{
					Log.Error("SNMP异步请求错误");
					rs = false;
				}
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				throw;
			}

			return rs;
		}

		/// <summary>
		/// GetNextRequest
		/// </summary>
		/// <param name="pdu">Pdu实例</param>
		/// <returns></returns>
		public override SnmpPacket GetNextRequest(Pdu pdu)
		{
			// Log msg
			string logMsg;
			if (m_target == null)
			{
				logMsg = "SNMP代理连接m_target为空";
				Log.Error(logMsg);
				return null;
			}
			if (pdu == null)
			{
				logMsg = "SNMP请求参数pdu为空";
				Log.Error(logMsg);
				return null;
			}

			pdu.Type = PduType.GetNext;
			SnmpV2Packet result = null;

			try
			{
				lock (lockObj)
				{
					result = (SnmpV2Packet)m_target.Request(pdu, m_Param);
					// TODO: 函数无处理内容
					//HandleGetRequestResult(result);
				}
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				throw e;
			}

			return result;
		}

		private void HandleGetRequestResult(object result)
		{
			string logMsg;
			if (!(result is SnmpV2Packet)) return;

			var newObj = (SnmpV2Packet)Convert.ChangeType(result, typeof(SnmpV2Packet));
			if (newObj == null)
				return;

			var es = newObj.Pdu.ErrorStatus;
			if (es != 0)
			{
				// TODO :当响应为 endOfView时，下面代码会崩
				// logMsg = $"Error in SNMP reply. Error {es}, index {newObj.Pdu.ErrorIndex}, Error Desc {SnmpErrDescHelper.GetErrDescById(es)}";
				//Log.Error(logMsg);
			}
			else
			{
				//foreach (var vb in newObj.Pdu.VbList)
				//{
				//	logMsg = $"ObjectName={vb.Oid}, Type={SnmpConstants.GetTypeName(vb.Value.Type)}, Value={vb.Value}";
				//	Log.Debug(logMsg);
				//}
			}
		}

		/// <summary>
		/// Set请求
		/// </summary>
		/// <param name="pdu">Pdu实例</param>
		/// <returns></returns>
		public override SnmpPacket SetRequest(Pdu pdu)
		{
			// log msg
			string logMsg;

			if (m_target == null)
			{
				logMsg = "SNMP代理连接m_target为空";
				Log.Error(logMsg);
				return null;
			}
			if (pdu == null)
			{
				logMsg = "SNMP请求参数pdu为空";
				Log.Error(logMsg);
				return null;
			}

			pdu.Type = PduType.Set;
			SnmpV2Packet response;

			try
			{
				lock (lockObj)
				{
					response = (SnmpV2Packet)m_target.Request(pdu, m_Param);
				}
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				throw;
			}

			if (response == null)
			{
				logMsg = "SNMP Set 命令错误，response为null";
				Log.Error(logMsg);
				return response;
			}
			else
			{
				if (response.Pdu.ErrorStatus != 0)
				{
					logMsg = $"SNMP响应返回错误， ErrorStatus: {response.Pdu.ErrorStatus}, ErrorIndex: {response.Pdu.ErrorIndex}";
					Log.Error(logMsg);
				}
				else
				{
					foreach (Vb vb in response.Pdu.VbList)
					{
						logMsg = $"SNMP Set响应，Oid={vb.Oid}, Type={vb.Value.Type}, Value={vb.Value}";
						Log.Info(logMsg);
					}
				}
			}

			return response;
		}

		/// <summary>
		/// 异步SetRequest
		/// </summary>
		/// <param name="pdu">Pdu实例</param>
		/// <param name="callback">回调方法</param>
		/// <returns></returns>
		public override bool SetRequestAsync(Pdu pdu, SnmpAsyncResponse callback)
		{
			// log msg
			string logMsg;

			bool rs = false;
			if (m_target == null)
			{
				logMsg = "SNMP代理连接m_target为空";
				Log.Error(logMsg);
				return false;
			}
			if (pdu == null)
			{
				logMsg = "SNMP请求参数pdu为空";
				Log.Error(logMsg);
				return false;
			}
			pdu.Type = PduType.Set;

			try
			{
				rs = m_target.RequestAsync(pdu, m_Param, callback);
				if (!rs)
				{
					logMsg = "SNMP异步请求错误";
					Log.Error(logMsg);
					return false;
				}
			}
			catch (Exception e)
			{
				Log.Error(e.Message);
				throw;
			}

			return rs;
		}

	}
}