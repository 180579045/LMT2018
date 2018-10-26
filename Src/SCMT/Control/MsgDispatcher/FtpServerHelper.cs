﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LogManager;
using LinkPath;
using LmtbSnmp;
using System.Net.Sockets;
using System.Net;

// ftp server助手

namespace MsgDispatcher
{
	public static class FtpServerHelper
	{
		// 设置ftp的信息
		public static bool SetFtpServerInfo(string targetIp)
		{
			var index = MatchFtpServerIndex(targetIp);
			if (index.Equals(""))
			{
				return false;
			}

			// 性能文件上传路径
			var pmFileUploadPath = FilePathHelper.GetPmFilePath();

			// 告警日志上传路径
			var alarmFileUpPath = FilePathHelper.GetAlarmFilePath(targetIp);
			FilePathHelper.CreateFolder(alarmFileUpPath);

			var pdu = new CDTLmtbPdu();
			var pmVb = new CDTLmtbVb
			{
				Oid = $"1.3.6.1.4.1.5105.100.1.2.1.1.1.6.{index}",
				Value = pmFileUploadPath,
				SnmpSyntax = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS
			};
			pdu.AddVb(pmVb);

			var alarmVb = new CDTLmtbVb
			{
				Oid = $"1.3.6.1.4.1.5105.100.1.2.1.1.1.7.{index}",
				Value = alarmFileUpPath,
				SnmpSyntax = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS
			};
			pdu.AddVb(alarmVb);

			var ftpAccountVb = new CDTLmtbVb
			{
				Oid = $"1.3.6.1.4.1.5105.100.1.2.1.1.1.11.{index}",
				Value = "lmtb",
				SnmpSyntax = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS
			};
			pdu.AddVb(ftpAccountVb);

			var ftpPwdVb = new CDTLmtbVb
			{
				Oid = $"1.3.6.1.4.1.5105.100.1.2.1.1.1.12.{index}",
				Value = "lmtb",
				SnmpSyntax = SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS
			};
			pdu.AddVb(ftpPwdVb);

			long reqId;
			ILmtbSnmp lmtbSnmpEx = DTLinkPathMgr.GetSnmpInstance(targetIp);
			if (0 != lmtbSnmpEx.SnmpSetSync(pdu, out reqId, targetIp, 1000))
			{
				Log.Error($"设置FTP服务器信息到网元失败!");
				return false;
			}
			Log.Info("设置FTP服务器信息到网元成功!");

			return true;
		}

		#region 私有接口

		private static string MatchFtpServerIndex(string targetIp)
		{
			var ftpServerAddressOid = "1.3.6.1.4.1.5105.100.1.2.1.1.1.5";

			//ENB网元的ftpServerTable表中，只有实例3~5用于存放LMT的FTP Server地址信息，因此只需要查询索引为:3、4、5的实例

			for (int curIndex = 3; curIndex <= 5; curIndex++)
			{
				string fullOid = $"{ftpServerAddressOid}.{curIndex}";


				var ftpServerAddressVb = new CDTLmtbVb
				{
					Oid = fullOid,
					AsnType = "inetaddress"
				};

				CDTLmtbPdu pdu = new CDTLmtbPdu {m_bIsNeedPrint = false};
				pdu.SetSyncId(true);
				pdu.AddVb(ftpServerAddressVb);

				long timeElapsed = 2000; //等待时间2秒
				long reqId;
				ILmtbSnmp lmtbSnmpEx = DTLinkPathMgr.GetSnmpInstance(targetIp);
				if (0 != lmtbSnmpEx.SnmpGetSync(pdu, out reqId, targetIp, timeElapsed))
				{
					continue;
				}

				string addr;
				if (!pdu.GetValueByOID(fullOid, out addr))
				{
					continue;
				}

				if (IsLocalAddress(targetIp, addr))
				{
					return $"{curIndex}";
				}
			}

			Log.Error($"在网元{targetIp}的ftpServerTable表中没有查询到与本地地址相匹配的记录");
			return "";
		}
        /// <summary>
        /// 获取本地管理站的地址后进行比对
        /// </summary>
        /// <param name="targetIp"></param>
        /// <param name="addr"></param>
        /// <returns></returns>
		private static bool IsLocalAddress(string targetIp, string addr)
		{
            string conaddr = SnmpToDatabase.ConvertInetToString(addr);
            string HostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(HostName);
            string localsddr = string.Empty; ;

            for (int i = 0; i < ipEntry.AddressList.Length; i++)
            {
                //IPV4类型 IP地址
                if(ipEntry.AddressList[i].AddressFamily  == AddressFamily.InterNetwork)
                {
                    localsddr = ipEntry.AddressList[i].ToString();

                    if (conaddr.CompareTo(localsddr) == 0)
                        return true;
                }

                //IP6类型 IP地址
                if (ipEntry.AddressList[i].AddressFamily == AddressFamily.InterNetworkV6)
                {
                    localsddr = ipEntry.AddressList[i].ToString();
                    if (conaddr.CompareTo(localsddr) == 0)
                        return true;
                }
            }

			return false;
		}

		#endregion
	}
}
