using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtility;
using LogManager;
using SnmpSharpNet;
using LinkPath;
using LmtbSnmp;

namespace FileManager
{
	public class FileTransTaskMgr : Singleton<FileTransTaskMgr>
	{
		#region 文件传输相关

		// 发起文件传输操作。为了方便操作，所有的文件信息都在cft对象中传入

		public static SENDFILETASKRES SendTransFileTask(string targetIp, CDTCommonFileTrans cft,
			ref long taskId,
			ref long requestId)
		{
			// 获取 requestid 和 taskid
			long reqId = 0, transTaskId = 0;
			for (int i = 0; i < 3; i++)
			{
				if (GetFileTransAvailableId_Sync(targetIp, ref reqId, ref transTaskId))
				{
					taskId = transTaskId;
					if (cft.StartFileTrans(transTaskId, ref reqId))
					{
						requestId = reqId;
						return SENDFILETASKRES.TRANSFILE_TASK_SUCCEED;
					}
				}
			}

			return SENDFILETASKRES.TRANSFILE_TASK_FAILED;
		}

		// 格式化传输文件信息。返回CDTCommonFileTrans对象可以直接用于SendTransFileTask方法的入参
		public static CDTCommonFileTrans FormatTransInfo(string targetPath,
			string fileFullName,
			Transfiletype5216 filetype,
			TRANSDIRECTION direction)
		{
			string filePath = FilePathHelper.GetFileParentFolder(fileFullName);
			string fileName = FilePathHelper.GetFileNameFromFullPath(fileFullName);

			string fileTransNEDirectory = "";
			string ftpWorkPath = "";
			string fileTransFileName = "";

			// 分为上传、下载不同的处理
			if (TRANSDIRECTION.TRANS_UPLOAD == direction)
			{
				fileTransNEDirectory = FileTransMacro.STR_EMPTYPATH;
				if (Transfiletype5216.TRANSFILE_generalFile == filetype)   //单文件
				{
					fileTransFileName = fileName;
					fileTransNEDirectory = filePath;
				}
				else if (Transfiletype5216.TRANSFILE_snapshotFile == filetype)
				{
					fileTransFileName = fileFullName;      //把快照文件名传入
				}
				else if ((byte)filetype >= 101 && (byte)filetype <= 107)
				{
					fileTransFileName = fileFullName;
				}
				else
				{
					fileTransFileName = FileTransMacro.STR_EMPTYPATH;
				}

				ftpWorkPath = targetPath;
			}
			else if (TRANSDIRECTION.TRANS_DOWNLOAD == direction)
			{
				fileTransFileName = fileName;
				fileTransNEDirectory = targetPath;
				ftpWorkPath = filePath;
			}
			else
			{
				Log.Error("传输方向设置错误");
				throw new CustomException("传输方向设置错误");
			}

			CDTCommonFileTrans cft = new CDTCommonFileTrans()
			{
				FileTransFileType = Convert.ToString((byte)filetype),       //文件类型,
				FileTransDirection = Convert.ToString((byte)direction),      //传输方向,
				FileTransFtpDir = ftpWorkPath,
				FileTransFileName = fileTransFileName,
				FileTransNeDir = fileTransNEDirectory,
				FileTransRowStatus = FileTransMacro.STR_CREATANDGO
			};

			return cft;
		}

		// 使用同步的方式获取可用的文件传输任务ID

		private static bool GetFileTransAvailableId_Sync(string ip, ref long reqId, ref long taskId)
		{
			CDTLmtbPdu InOutPdu = new CDTLmtbPdu();
			var ret = CDTCmdExecuteMgr.GetInstance().CmdGetSync("GetFileTransNextID", out reqId, ".0", ip, ref InOutPdu);
			if (0 == ret)
			{
				string csFileTrandId;
				if ((InOutPdu.GetValueByMibName(ip, "fileTransNextAvailableIDForOthers", out csFileTrandId, ".0")))
				{
					taskId = Int64.Parse(csFileTrandId);
					return true;
				}
			}

			Log.Error("同步获取文件传输可用任务ID出错!");
			return false;
		}

		// 使用异步方式获取可用的文件传输任务ID

		private static bool GetFileTransAvailableId(string ip, ref long reqId)
		{
			return (0 == CDTCmdExecuteMgr.GetInstance().CmdGetAsync("GetFileTransNextID", out reqId, ".0", ip));
		}

		#endregion

		#region snmp操作结果处理

		// snmp操作结果处理
		public void ResponseDeal(CDTLmtbPdu pdu)
		{
			if (null == pdu)
			{
				return;
			}

			string boardIp = pdu.m_SourceIp;
			long reqId = pdu.m_requestId;

			if (!m_setReqId.Contains(reqId))
			{
				Log.Debug($"本地没有找到ID为{reqId}的信息");
				return;
			}

			m_setReqId.Remove(reqId);

			if (SnmpConstants.ErrNoError != pdu.m_LastErrorStatus)
			{
				Log.Error("传入的PDU错误");
				SendTransFileTask_Start(boardIp, -1, reqId);
			}
			else
			{
				string fileTransId;
				if (pdu.GetValueByMibName(boardIp, "fileTransNextAvailableIDForOthers", out fileTransId))
				{
					var nFileTransId = Int64.Parse(fileTransId);
					SendTransFileTask_Start(boardIp, nFileTransId, reqId);
				}
				else
				{
					SendTransFileTask_Start(boardIp, -1, reqId);
				}
			}
		}

		private bool SendTransFileTask_Start(string ip, long nFileTransId, long lReqId)
		{
			bool bRet = true;

			if (!m_mapIp2FlTrsObjQue.ContainsKey(ip))
			{
				Log.Error($"没有找到到{ip}的文件传输任务");
				return false;
			}

			var fileTransQueue = m_mapIp2FlTrsObjQue[ip];
			if (null == fileTransQueue)
			{
				fileTransQueue = new List<CDTAbstractFileTrans>();
			}

			if (fileTransQueue.Count > 0)
			{
				if (-1 == nFileTransId)		// 超时或者失败
				{
					Log.Error("查找文件传输ID失败");
					// TODO 删除已有的数据
				}
				else
				{
					var fileTransObj = fileTransQueue.First();
					if (null == fileTransObj)
					{
						return false;
					}

					long reqId = 0;
					if (!fileTransObj.StartFileTrans(nFileTransId, ref reqId))
					{
						bRet = false;
					}

					m_setReqId.Add(reqId);
				}
			}

			foreach (var fileTranObj in fileTransQueue)
			{
				if (null == fileTranObj)
				{
					continue;
				}

				if (!GetFileTransAvailableId(ip, ref lReqId))
				{
					bRet = false;
					fileTransQueue.Remove(fileTranObj);
					break;
				}

				fileTranObj.SetReqId(lReqId);
				m_setReqId.Add(lReqId);
			}

			return bRet;
		}

		#endregion


		#region 私有方法
		
		public FileTransTaskMgr(string boardIp)
		{
			m_setReqId = new HashSet<long>();
			m_mapIp2FlTrsObjQue = new Dictionary<string, List<CDTAbstractFileTrans>>();

			_boardIp = boardIp;
		}

		#endregion

		#region 私有属性

		private byte MaxTransTaskCount = 20;

		private HashSet<long> m_setReqId;

		private Dictionary<string, List<CDTAbstractFileTrans>> m_mapIp2FlTrsObjQue;


		private string _boardIp;

		#endregion
	}
}
