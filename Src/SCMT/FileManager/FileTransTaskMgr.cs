using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUility;
using LogManager;
using SCMTOperationCore.Message.SNMP;
using SnmpSharpNet;

namespace FileManager
{

	public class FileTransTaskMgr
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
			string filePath = Path.GetDirectoryName(fileFullName);
			string fileName = Path.GetFileName(fileFullName);

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

		//停止文件传输操作。taskId作为索引使用
		public static SENDFILETASKRES CancelTransFileTask(long taskId, string targetIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("fileTransRowStatus", FileTransMacro.STR_DESTROY);
			long reqId = 0;

			var ret = CDTCmdExecuteMgr.CmdSetAsync("DelFileTransTask", out reqId, mapName2Value, $".{taskId}", targetIp);
			if (0 == ret)
			{
				HasTransFileWork = false;
				return SENDFILETASKRES.TRANSFILE_TASK_SUCCEED;
			}

			return SENDFILETASKRES.TRANSFILE_TASK_FAILED;
		}

		public static bool HasTransFileWork { get; set; }

		public static bool HasUpgradeWork { get; set; }
		#endregion

		#region 文件上传操作

		public SENDFILETASKRES FileUpload(string dstPath, int nFrameNo, int nSlotNo, int upType, string remoteIp)
		{
			throw new NotImplementedException();
		}

		// rru日志上传操作
		public SENDFILETASKRES RruLogUpload(string dstPath, uint nRruIndexNo, uint nLogFileType, string remoteIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>
			{
				{"topoRRULogDestination", dstPath},
				{"topoRRULogFileType", Convert.ToString(nLogFileType)}
			};

			long reqId = 0;
			var ret = CDTCmdExecuteMgr.CmdSetAsync("UploadRRULog", out reqId, mapName2Value, $".{nRruIndexNo}", remoteIp);
			if (0 == ret)
			{
				return SENDFILETASKRES.TRANSFILE_TASK_SUCCEED;
			}

			return SENDFILETASKRES.TRANSFILE_TASK_FAILED;
		}

		#endregion

		#region 软件包相关的操作

		// 立即激活软件包
		public SENDFILETASKRES SoftPackActive_Immediately(SOFTACT enmuAct, int nIndex, string targetIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("softwarePackActSwitch", Convert.ToString( (byte)enmuAct));

			long reqId = 0;
			var ret = CDTCmdExecuteMgr.CmdSetAsync("softwarePackActSwitch", out reqId, mapName2Value, $".{nIndex}", targetIp);
			if (0 == ret)
			{
				return SENDFILETASKRES.TRANSFILE_TASK_SUCCEED;
			}

			return SENDFILETASKRES.TRANSFILE_TASK_FAILED;
		}

		// 定时激活
		public SENDFILETASKRES SoftPackActive_LaterOn(SOFTACT enmuAct, string time, int nIndex, string targetIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("softwarePackActNeed", Convert.ToString((byte)enmuAct));
			mapName2Value.Add("softwarePackActTime", time);

			long reqId = 0;
			var ret = CDTCmdExecuteMgr.CmdSetAsync("SetSoftwarePackActTime", out reqId, mapName2Value, $".{nIndex}", targetIp);
			if (0 == ret)
			{
				return SENDFILETASKRES.TRANSFILE_TASK_SUCCEED;
			}

			return SENDFILETASKRES.TRANSFILE_TASK_FAILED;
		}

		// 取消定时激活
		public SENDFILETASKRES SoftPackActive_LaterCancel(SOFTACT enmuAct, int nIndex, string targetIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("softwarePackActNeed", Convert.ToString((byte)enmuAct));

			long reqId = 0;
			var ret = CDTCmdExecuteMgr.CmdSetAsync("SetSoftwarePackClearActTime", out reqId, mapName2Value, $".{nIndex}", targetIp);
			if (0 == ret)
			{
				return SENDFILETASKRES.TRANSFILE_TASK_SUCCEED;
			}

			return SENDFILETASKRES.TRANSFILE_TASK_FAILED;
		}

		// 删除文件
		public SENDFILETASKRES SendFileDeleteCmd(string filename, int index, string targetIp)
		{
			Dictionary<string, string> mapName2Value =
				new Dictionary<string, string> {{"softFileName", filename}, {"softDeleteTrigger", "1"}};

			long reqId = 0;
			var ret = CDTCmdExecuteMgr.CmdSetAsync("DelSoftware", out reqId, mapName2Value, $".{index}", targetIp);
			if (0 == ret)
			{
				return SENDFILETASKRES.TRANSFILE_TASK_SUCCEED;
			}

			return SENDFILETASKRES.TRANSFILE_TASK_FAILED;
		}

		// 删除软件包
		public SENDFILETASKRES SendSoftPackDelCmd(int index, string targetIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("softwarePackDeleteTrigger", "1");

			long reqId = 0;
			var ret = CDTCmdExecuteMgr.CmdSetAsync("DelSoftwarePack", out reqId, mapName2Value, $".{index}", targetIp);
			if (0 == ret)
			{
				return SENDFILETASKRES.TRANSFILE_TASK_SUCCEED;
			}

			return SENDFILETASKRES.TRANSFILE_TASK_FAILED;
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

		// 使用同步的方式获取可用的文件传输任务ID
		private static bool GetFileTransAvailableId_Sync(string ip, ref long reqId, ref long taskId)
		{
			CDTLmtbPdu InOutPdu = new CDTLmtbPdu();
			var ret = CDTCmdExecuteMgr.GetInstance().CmdGetSync("GetFileTransNextID", out reqId, ".0", ip, ref InOutPdu);
			if (0 == ret)
			{
				string csFileTrandId;
				if ((InOutPdu.GetValueByMibName(ip, "fileTransNextAvailableIDForOthers", out csFileTrandId)))
				{
					taskId = Int64.Parse(csFileTrandId);
					return true;
				}
			}

			Log.Error("同步获取文件传输可用任务ID出错!");
			return false;
		}

		// 使用异步方式获取可用的文件传输任务ID
		private bool GetFileTransAvailableId(string ip, ref long reqId)
		{
			return (0 == CDTCmdExecuteMgr.GetInstance().CmdGetAsync("GetFileTransNextID", out reqId, ".0", ip));
		}

		private FileTransTaskMgr()
		{
			m_setReqId = new HashSet<long>();
			m_mapIp2FlTrsObjQue = new Dictionary<string, List<CDTAbstractFileTrans>>();
		}

		#endregion

		#region 私有属性

		private byte MaxTransTaskCount = 20;

		private HashSet<long> m_setReqId;

		private Dictionary<string, List<CDTAbstractFileTrans>> m_mapIp2FlTrsObjQue;


		#endregion
	}


	public class CDTAbstractFileTrans
	{
		public CDTAbstractFileTrans(bool bIsReqIdValid)
		{
			m_bIsReqIdValid = bIsReqIdValid;
		}

		public virtual bool StartFileTrans(long unFileTransId, ref long lreqId)
		{
			return true;
		}

		public void SetReqId(long lReqId)
		{
			m_bIsReqIdValid = true;
			m_lReqId = lReqId;
		}
		public long GetReqId()
		{
			return m_lReqId;
		}

		public bool  m_bIsReqIdValid { get; set; }

		public long m_lReqId { get; set; }
	};

	public class CDTCommonFileTrans : CDTAbstractFileTrans
	{
		public CDTCommonFileTrans()
		: base(false)
		{

		}

		// 启动文件下发任务
		public override bool StartFileTrans(long unFileTransId, ref long lReqId)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>
			{
				{"fileTransRowStatus", FileTransRowStatus},
				{"fileTransType", FileTransFileType},
				{"fileTransIndicator", FileTransDirection},
				{"fileTransFTPDirectory", FileTransFtpDir},
				{"fileTransFileName", FileTransFileName},
				{"fileTransNEDirectory", FileTransNeDir}
			};

			CDTLmtbPdu inOutPdu = new CDTLmtbPdu();

			var ret = CDTCmdExecuteMgr.GetInstance().CmdSetSync("AddFileTransTask", out lReqId, mapName2Value, $".{unFileTransId}",
				IpAddr, ref inOutPdu);

			return (0 == ret);
		}

		public string FileTransRowStatus { get; set; }

		public string FileTransFileType { get; set; }

		public string FileTransFtpDir { get; set; }

		public string FileTransFileName { get; set; }

		public string FileTransNeDir { get; set; }

		public string IpAddr { get; set; }

		public string FileTransDirection { get; set; }
	};


}
