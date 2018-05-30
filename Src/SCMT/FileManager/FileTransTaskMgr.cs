using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUility;
using LogManager;
using SCMTOperationCore.Message.SNMP;

namespace FileManager
{
	#region 枚举类型

	public enum TRANSFILETYPE_5216 : byte
	{
		TRANSFILE_unknowtype = 0,
		TRANSFILE_operationLog = 1,     //|操作日志
		TRANSFILE_alterLog,             //|变更日志
		TRANSFILE_omSecurityLog,        //|安全日志
		TRANSFILE_alarmLog,             //|告警日志文件
		TRANSFILE_omKeyLog,             //|重要过程日志
		TRANSFILE_updateLog,            //|升级日志
		TRANSFILE_debugLog,             //|黑匣子日志
		TRANSFILE_statelessAlarmLog,    //|异常日志
		TRANSFILE_eventLog,             //|事件日志
		TRANSFILE_userLog,              //|用户日志
		TRANSFILE_cfgDataConsistency,   //|配置数据一致性文件
		TRANSFILE_stateDataConsistency, //|状态数据一致性文件
		TRANSFILE_dataConsistency,      //|数据一致性文件
		TRANSFILE_curConfig,            //|当前运行配置文件
		TRANSFILE_planConfig,           //|期望配置文件
		TRANSFILE_equipSoftwarePack,    //|主设备软件包
		TRANSFILE_coldPatchPack,        //|主设备冷补丁包
		TRANSFILE_hotPatchPack,         //|主设备热补丁包
		TRANSFILE_rruEquipSoftwarePack, //|RRU软件包
		TRANSFILE_relantEquipSoftwarePack,              //|电调天线软件包
		TRANSFILE_enviromentEquipSoftwarePackPack,      //|环境监控软件包
		TRANSFILE_gpsEquipSoftwarePack,                 //|GPS软件包
		TRANSFILE_1588EquipSoftwarePack,                //|1588软件包
		TRANSFILE_cnssEquipSoftwarePackPack,            //|北斗软件包
		TRANSFILE_generalFile,                          //|普通文件/
		TRANSFILE_lmtMDBFile,                           //|数据库文件
		TRANSFILE_activeAlarmFile,                      //|活跃告警文件
		TRANSFILE_performanceFile,                      //|性能文件
		TRANSFILE_cfgPackFile,                          //|性能文件
		TRANSFILE_snapshotFile = 30,                    //快照配置文件
		TRANSFILE_rncDisa = 49,                         //RNC容灾文件
		TRANSFILE_rarFile = 42,                         //rar压缩文件
	}

	public enum SENDFILETASKRES : byte
	{
		TRANSFILE_TASK_QUERYOIDFAIL,    /*获取OID失败*/
		TRANSFILE_TASK_SUCCEED,         /*传输任务下发成功*/
		TRANSFILE_TASK_FAILED,          /*传输任务下发失败*/
		TRANSFILE_TASK_NOIDLE           /*没有空闲的任务*/
	}

	public enum TRANSDIRECTION : byte
	{
		TRANS_UPLOAD = 1,       /*eNB上传到管理站*/
		TRANS_DOWNLOAD          /*管理站下载到eNB*/
	}

	public enum SOFTACT : byte{
		SOFTACT_CLOSE = 0,
		SOFTACT_OPEN = 1
	}

	#endregion


	public class FileTransTaskMgr : Singleton<FileTransTaskMgr>
	{
		#region 文件传输相关

		// 发起文件传输操作
		public SENDFILETASKRES SendTransFileTask(string targetIp, CDTCommonFileTrans cft,
			ref long taskId,        // TODO ref 和 out 有什么区别？
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

		public CDTCommonFileTrans FormatTransInfo(string targetPath,
			string fileFullName,
			TRANSFILETYPE_5216 filetype,
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
				if (TRANSFILETYPE_5216.TRANSFILE_generalFile == filetype)   //单文件
				{
					fileTransFileName = fileName;
					fileTransNEDirectory = filePath;
				}
				else if (TRANSFILETYPE_5216.TRANSFILE_snapshotFile == filetype)
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
				FileTransFTPDir = ftpWorkPath,
				FileTransFileName = fileTransFileName,
				FileTransNEDir = fileTransNEDirectory,
				FileTransRowStatus = FileTransMacro.STR_CREATANDGO
			};

			return cft;
		}

		//停止文件传输操作
		public SENDFILETASKRES CancelTransFileTask(long taskId, string targetIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("fileTransRowStatus", FileTransMacro.STR_DESTROY);

			//TODO 同步执行snmp命令：DelFileTransTask
			throw new NotImplementedException();
		}

		#endregion

		#region 文件上传操作

		public SENDFILETASKRES FileUpload(string dstPath, int nFrameNo, int nSlotNo, int upType, string remoteIp)
		{
			throw new NotImplementedException();
		}

		// rru日志上传操作
		public SENDFILETASKRES RruLogUpload(string dstPath, uint nRruIndexNo, uint nLogFileType, string remoteIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("topoRRULogDestination", dstPath);
			mapName2Value.Add("topoRRULogFileType", Convert.ToString(nLogFileType));

			//TODO  执行snmp命令：UploadRRULog
			throw new NotImplementedException();
		}

		#endregion

		#region 软件包相关的操作

		// 立即激活软件包
		public SENDFILETASKRES SoftPackActive_Immediately(SOFTACT enmuAct, int nIndex, string targetIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("softwarePackActSwitch", Convert.ToString( (byte)enmuAct));

			//TODO 同步执行snmp命令：SetSoftwarePackAct
			throw new NotImplementedException();
		}

		// 定时激活
		public SENDFILETASKRES SoftPackActive_LaterOn(SOFTACT enmuAct, string time, int nIndex, string targetIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("softwarePackActNeed", Convert.ToString((byte)enmuAct));
			mapName2Value.Add("softwarePackActTime", time);

			//TODO 同步执行snmp命令：SetSoftwarePackActTime
			throw new NotImplementedException();
		}

		// 取消定时激活
		public SENDFILETASKRES SoftPackActive_LaterCancel(SOFTACT enmuAct, int nIndex, string targetIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("softwarePackActNeed", Convert.ToString((byte)enmuAct));

			//TODO 同步执行snmp命令：SetSoftwarePackClearActTime
			throw new NotImplementedException();
		}

		// 删除文件
		public SENDFILETASKRES SendFileDeleteCmd(string filename, int index, string targetIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("softFileName", filename);
			mapName2Value.Add("softDeleteTrigger", "1");

			//TODO 同步执行snmp命令：DelSoftware
			throw new NotImplementedException();
		}

		// 删除软件包
		public SENDFILETASKRES SendSoftPackDelCmd(int index, string targetIp)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("softwarePackDeleteTrigger", "1");

			//TODO 同步执行snmp命令：DelSoftwarePack
			throw new NotImplementedException();
		}

		#endregion

		#region snmp操作结果处理

		// 一个委托。TODO pdu需要修改为实际定义的类型
		public void ResponseDeal(CDTLmtbPdu pdu)
		{

		}

		#endregion


		#region 私有方法

		private bool GetFileTransAvailableId_Sync(string ip, ref long reqId, ref long taskId)
		{
			throw new NotImplementedException();
		}

		private bool GetFileTransAvailableId(string ip, ref long reqId)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region 私有属性

		private byte MaxTransTaskCount = 20;

		#endregion
	}


	public class CDTAbstractFileTrans
	{
		public CDTAbstractFileTrans(bool bIsReqIdValid)
		{
			m_bIsReqIdValid = bIsReqIdValid;
		}

		public virtual bool StartFileTrans(long unFileTransId, ref long lreqID)
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

		public bool StartFileTrans(long unFileTransId, ref long lReqId)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>
			{
				{"fileTransRowStatus", FileTransRowStatus},
				{"fileTransType", FileTransFileType},
				{"fileTransIndicator", FileTransDirection},
				{"fileTransFTPDirectory", FileTransFTPDir},
				{"fileTransFileName", FileTransFileName},
				{"fileTransNEDirectory", FileTransNEDir}
			};

			//同步下发任务

			return true;
		}

		public string FileTransRowStatus { get; set; }

		public string FileTransFileType { get; set; }

		public string FileTransFTPDir { get; set; }

		public string FileTransFileName { get; set; }

		public string FileTransNEDir { get; set; }

		public string IpAddr { get; set; }

		public string FileTransDirection { get; set; }
	};
}
