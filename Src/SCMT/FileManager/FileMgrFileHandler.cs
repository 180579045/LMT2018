using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using CommonUtility;
using FileManager.FileHandler;
using LogManager;
using MsgQueue;
using SCMTOperationCore.Message.SI;
using SCMTOperationCore.Message.SNMP;

namespace FileManager
{
	public delegate void GetFileInfoRspHandler(byte[] rspData);		// 获取文件信息事件

	public delegate void UpdateProcessBar(TProgressBarInfo pbInfo);

	public delegate void MenuClickHandler(IASerialize rsp);

	// 只是把原来的函数先简单的封装在一个文件中
	public class FileMgrFileHandler
	{
		#region 委托、事件

		public event GetFileInfoRspHandler GetFileInfoRspArrived;
		public event UpdateProcessBar UpdateProgressEvent;				// 更新进度条事件
		public event UpdateProcessBar NewProgressEvent;					// 增加一个新的进度条
		public event UpdateProcessBar EndProgressEvent;					// 销毁进度条
		public event MenuClickHandler MenuClickRspEvent;				// 右键菜单响应
		#endregion

		#region 构造、析构

		public FileMgrFileHandler(string boardIp)
		{
			_boardIp = boardIp;
			_operaFailCount = 0;
			_mapTransFileType = new Dictionary<int, string>();
			_mapTraningFileTask = new Dictionary<long, StruFileTransDes>();

			// 订阅SI事件
			SubscribeHelper.AddSubscribe($"/{_boardIp}/O_SILMTENB_GETFILEINFO_RES", OnGetFileInfoRsp);
			SubscribeHelper.AddSubscribe(TopicHelper.QueryEnbCapacityRsp, OnGetCapacityRsp);
		}


		~FileMgrFileHandler()
		{
			
		}

		#endregion

		#region 公有接口

		// 获取板卡上的文件目录信息
		public bool GetBoardFileInfo(string path)
		{
			return FileMgrSendSiMsg.SendGetBoardFileInfoReq(path, _boardIp);
		}

		//发送文件到远端。localFilePath和remotePath等参数已经处理好
		public bool SendFileToRemote(string localFilePath, string remotePath)
		{
			//校验基站端是否允许升级
			if (!CanUpdate())
			{
				return false;
			}

			//判断是否存在其他正在进行的任务
			if (HasRunningTask())
			{
				throw new CustomException("有正在进行传输的文件或正在升级的任务");
			}

			//创建临时目录等操作
			var dstFullPath = CreateTempFile(localFilePath, _boardIp);

			// 使用工厂模式，创建不同文件的处理对象，在处理对象中进行文件处理
			var ext = FilePathHelper.GetFileExt(dstFullPath);
			if (null == ext)
			{
				throw new CustomException($"从文件路径{dstFullPath}获取扩展名失败");
			}

			var handler = FileHandlerFactory.CreateHandler(ext, _boardIp);
			var result = handler.DoPutFile(dstFullPath, remotePath);
			if (ExecuteResult.UpgradeFailed == result)
			{
				Log.Error("升级失败");
				return false;
			}

			if (ExecuteResult.UserCancel == result)
			{
				Log.Info("升级过程中用户主动取消");
				return true;
			}

			var baseHandler = (BaseFileHandler) handler;
			_workingForUpgrade = baseHandler.WorkingForUpgrade;
			_workingForFileTrans = baseHandler.WorkingForFileTrans;

			if (_workingForUpgrade)
			{
				_upgradePackInfo = baseHandler.UFO;
			}

			if (_workingForFileTrans)
			{
				AddFileTransProcess(baseHandler.TFO, baseHandler.WorkingTaskId);
			}

			_timer = new Timer(timerCallback, null, 1000, 1000);

			return true;
		}

		// 上传文件到本地
		public bool UploadFileToLocal(string localFilePath, string remoteFilePath)
		{
			//判断是否存在其他正在进行的任务
			if (HasRunningTask())
			{
				throw new CustomException("有正在进行传输的文件或正在升级的任务");
			}

			var handler = FileHandlerFactory.CreateHandler("gen", _boardIp);
			var result = handler.DoGetFile(localFilePath, remoteFilePath);
			if (ExecuteResult.UpgradeFailed == result)
			{
				Log.Error($"上传文件{remoteFilePath}到本地{localFilePath}失败");
				return false;
			}

			var baseHandler = (BaseFileHandler)handler;
			_workingForUpgrade = baseHandler.WorkingForUpgrade;
			_workingForFileTrans = baseHandler.WorkingForFileTrans;

			if (_workingForUpgrade)
			{
				_upgradePackInfo = baseHandler.UFO;
			}

			if (_workingForFileTrans)
			{
				AddFileTransProcess(baseHandler.TFO, baseHandler.WorkingTaskId);
			}

			_timer = new Timer(timerCallback, null, 1000, 1000);

			return true;
		}
		
		// rru日志上传操作
		public SENDFILETASKRES RruLogUpload(string dstPath, uint nRruIndexNo, uint nLogFileType, string remoteIp)
		{
			var mapName2Value = new Dictionary<string, string>
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

		public SENDFILETASKRES FileUpload(string dstPath, int nFrameNo, int nSlotNo, int upType, string remoteIp)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 保存正在传输的任务信息
		/// </summary>
		private void AddFileTransProcess(CDTCommonFileTrans transFile, long taskId)
		{
			var tranFileFullPath = $"{transFile.FileTransFtpDir.TrimEnd('\\').TrimEnd('/')}\\{transFile.FileTransFileName}";
			var ftd = new StruFileTransDes
			{
				nFileTransTaskId = taskId,
				nFileDownSize = 0,
				emFileTransType = FILETRANSTYPE.COMMONFILE,
				enumFileTransOp = FILETRANSOPER.DownLoading,
				csFilePathAndName = tranFileFullPath
			};

			var title = tranFileFullPath;

			if (transFile.FileTransDirection.Equals("1"))
			{
				ftd.enumFileTransOp = FILETRANSOPER.UpLoading;
				title = $"{tranFileFullPath}({GetFileTypeText(transFile.FileTransFileType)})";
			}

			_mapTraningFileTask[taskId] = ftd;		// 保存进行中的任务
			SetProcInit(title, ftd);
		}

		// 触发进度条初始化
		private void SetProcInit(string showName, StruFileTransDes ftd)
		{
			var state = FILETRANSSTATE.TRANSSTATE_UNKNOWN;
			var op = OPERTYPE.OPERTYPE_UNKNOWN;
			var opText = "";
			var stateText = "";

			var dd = ftd.enumFileTransOp;
			if (dd == FILETRANSOPER.DownLoading)
			{
				state = FILETRANSSTATE.TRANSSTATE_DOWNLOADWAITING;
				op = OPERTYPE.OPERTYPE_DOWNLOAD;
				opText = "文件下载";
				stateText = "文件下载等待中...";
			}
			else
			{
				state = FILETRANSSTATE.TRANSSTATE_UPLOADWAITING;
				op = OPERTYPE.OPERTYPE_UPLOAD;
				opText = "文件上传";
				stateText = "文件上传等待中...";
			}

			var pbInfo = new TProgressBarInfo
			{
				m_csFileName = showName,
				m_lTaskID = ftd.nFileTransTaskId,
				m_nPercent = 0,
				m_eStatus = state,
				m_strStatus = stateText,
				m_eOperationType = op,
				strOperationType = opText
			};

			NewProgressEvent?.Invoke(pbInfo);
		}

		// 更新文件传输的进度条信息
		private void UpdateProgress(long taskId, int precent, FILETRANSOPER op)
		{
			if (!_mapTraningFileTask.ContainsKey(taskId))
			{
				Log.Error($"根据传入的takid={taskId}，未找到对应的任务信息");
				return;
			}

			string stateText = "";
			FILETRANSSTATE state = FILETRANSSTATE.TRANSSTATE_UNKNOWN;
			if (FILETRANSOPER.DownLoading == op)
			{
				state = FILETRANSSTATE.TRANSSTATE_DOWNLOADING;
				stateText = "下载正在进行...";

				if (precent >= 100)
				{
					precent = 100;
					state = FILETRANSSTATE.TRANSSTATE_DOWNLOADFINISHED;
					stateText = "下载已完成";

					_mapTraningFileTask.Remove(taskId);
					_workingForFileTrans = false;
				}
			}
			else if (FILETRANSOPER.UnZipping == op)
			{
				state = FILETRANSSTATE.TRANSSTATE_UPZIPING;
				stateText = "解压正在进行...";

				if (precent >= 100)
				{
					precent = 100;
					state = FILETRANSSTATE.TRANSSTATE_UPZIPFINISHED;
					stateText = "解压已完成";
				}
			}
			else if (FILETRANSOPER.UpLoading == op)
			{
				state = FILETRANSSTATE.TRANSSTATE_UPLOADING;
				stateText = "上传正在进行...";

				if (precent >= 100)
				{
					precent = 100;
					state = FILETRANSSTATE.TRANSSTATE_UPLOADFINISHED;
					stateText = "上传已完成";

					_mapTraningFileTask.Remove(taskId);
					_workingForFileTrans = false;
				}
			}

			var pbInfo = new TProgressBarInfo
			{
				m_nPercent = precent,
				m_lTaskID = taskId,
				m_eStatus = state,
				m_strStatus = stateText
			};

			UpdateProgressEvent?.Invoke(pbInfo);
		}

		#endregion

		#region 软件包相关的操作

		// 立即激活软件包
		public SENDFILETASKRES SoftPackActive_Immediately(SOFTACT enmuAct, int nIndex, string targetIp)
		{
			var mapName2Value = new Dictionary<string, string>();
			mapName2Value.Add("softwarePackActSwitch", Convert.ToString((byte)enmuAct));

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
			var mapName2Value = new Dictionary<string, string>();
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
			var mapName2Value = new Dictionary<string, string>();
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
			var mapName2Value =
				new Dictionary<string, string> { { "softFileName", filename }, { "softDeleteTrigger", "1" } };

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
			var mapName2Value = new Dictionary<string, string>();
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

		#region 订阅事件处理方法、定时器回调

		// 查询文件信息响应
		private void OnGetFileInfoRsp(SubscribeMsg msg)
		{
			GetFileInfoRspArrived?.Invoke(msg.Data);
		}

		// 定时器处理函数
		private void timerCallback(object obj)
		{
			if (_workingForUpgrade)
			{
				SwPackPlanFileDownLoadProcDeal();
			}
			else if (_workingForFileTrans)
			{
				CommonFileTransProcDeal();
			}
			else
			{
				_timer.Change(-1, -1);
				_timer.Dispose();
			}
		}

		// 拖包升级时进度条状态更新
		private void SwPackPlanFileDownLoadProcDeal()
		{
			var index = "";
			long taskId = 0;

			if (!_upgradePackInfo.GetInfo(ref taskId, ref index))
			{
				EndProgressBar();
				_workingForUpgrade = false;
				return;
			}

			var processBarInfo = new TProgressBarInfo
			{
				m_csFileName = _upgradePackInfo.GetFileName(),
				m_lTaskID = taskId
			};

			string swPackPlanUpgradeState;
			string swPackPlanUpgradePercent;
			string swPackPlanUpgradeResult;
			var swPackPlanUpgradeResult2 = "";		//外设软件包要有连个字段来表示结果
			string csGetCmd;
			string PackPlanOpEndingCmd;				//获取全部结束节点值的命令
			string swPackPlanOpEndingIndicator;		//是否全部结束节点：0未结束，1全部结束

			if (_upgradePackInfo.IsMainEqpSw())
			{
				swPackPlanUpgradeState = "swPackPlanUpgradeState";
				swPackPlanUpgradePercent = "swPackPlanUpgradePercent";
				swPackPlanUpgradeResult = "swPackPlanUpgradeResult";
				csGetCmd = "GetSWPackPlanPercent";
				swPackPlanOpEndingIndicator = ("swPackPlanOpEndingIndicator");
				PackPlanOpEndingCmd = ("GetSWPackPlanEndingInd");
			}
			else
			{
				swPackPlanUpgradeState = "peripheralPackPlanUpgradeState";
				swPackPlanUpgradePercent = "peripheralPackPlanUpgradePercent";
				swPackPlanUpgradeResult = "peripheralPackPlanUpgradeResult1";
				swPackPlanUpgradeResult2 = "peripheralPackPlanUpgradeResult2";
				csGetCmd = "GetPeripheralPackPlanPercent";
				swPackPlanOpEndingIndicator = ("peripheralPackPlanOpEndingIndicator");
				PackPlanOpEndingCmd = "GetPeripheralPackPlanEndingInd";
			}

			var pdu = new CDTLmtbPdu();
			long reqId = 0;
			var ret = CDTCmdExecuteMgr.GetInstance().CmdGetSync(csGetCmd, out reqId, index, _boardIp, ref pdu);
			if (0 != ret)
			{
				var retryCount = _upgradePackInfo.iResCount++;
				if (retryCount >= 5)        //如果5次查询均失败,则认为基站复位,需要关闭进度条
				{
					EndProgressBar();
					_workingForUpgrade = false;
				}
				EndUIProcessBar(processBarInfo);
				return;
			}

			string swPackPlanUpgradeStateValue;
			string swPackPlanUpgradePercentValue;
			string swPackPlanUpgradeResultValue;
			string swPackPlanUpgradeResultValue2;
			_upgradePackInfo.iResCount = 0;         //连接失败计数清0

			if (!pdu.GetValueByMibName(_boardIp, swPackPlanUpgradeState, out swPackPlanUpgradeStateValue, ".1")
				|| !pdu.GetValueByMibName(_boardIp, swPackPlanUpgradePercent, out swPackPlanUpgradePercentValue, ".1")
				|| !pdu.GetValueByMibName(_boardIp, swPackPlanUpgradeResult, out swPackPlanUpgradeResultValue, ".1"))
			{
				Log.Error("查询文件传输进度参数失败！");
				return;
			}

			Log.Info($"状态：{swPackPlanUpgradeStateValue} 进行百分比{swPackPlanUpgradePercentValue} 结果：{swPackPlanUpgradeResultValue}");
			var nPercent = int.Parse(swPackPlanUpgradePercentValue);
			processBarInfo.m_nPercent = nPercent;

			if (swPackPlanUpgradeStateValue.Equals(FileTransMacro.STR_DOWNLOADING))
			{
				Log.Info($"文件传输状态：{_upgradePackInfo.GetFileTransStatus()}");

				if (FILETRANSOPER.UnKnown == _upgradePackInfo.GetFileTransStatus())
				{
					_upgradePackInfo.SetFileTransStatus(FILETRANSOPER.DownLoading);
				}

				processBarInfo.m_eOperationType = OPERTYPE.OPERTYPE_DOWNLOAD;
				processBarInfo.m_eStatus = FILETRANSSTATE.TRANSSTATE_DOWNLOADING;
				processBarInfo.m_strStatus = "下载正在进行...";
				SetProcessInfo(processBarInfo);
			}
			else if (swPackPlanUpgradeStateValue.Equals(FileTransMacro.STR_UNZIPPING))      //正在解压
			{
				if (FILETRANSOPER.DownLoading == _upgradePackInfo.GetFileTransStatus())
				{
					_upgradePackInfo.SetFileTransStatus(FILETRANSOPER.UnZipping);
				}
				processBarInfo.m_eOperationType = OPERTYPE.OPERTYPE_UNZIP;
				processBarInfo.m_eStatus = FILETRANSSTATE.TRANSSTATE_UPZIPING;
				processBarInfo.m_strStatus = "解压正在进行...";
				SetProcessInfo(processBarInfo);
			}
			else if (swPackPlanUpgradeStateValue.Equals(FileTransMacro.STR_SYNING))
			{
				if (FILETRANSOPER.UnZipping == _upgradePackInfo.GetFileTransStatus())//由解压转到同步过程
				{
					_upgradePackInfo.SetFileTransStatus(FILETRANSOPER.Syncing);
				}

				processBarInfo.m_eOperationType = OPERTYPE.OPERTYPE_SYN;
				processBarInfo.m_eStatus = FILETRANSSTATE.OPERTYPE_SYNING;
				processBarInfo.m_strStatus = "同步正在进行...";
				SetProcessInfo(processBarInfo);

				if (100 <= nPercent)
				{
					var nRezlt1 = int.Parse(swPackPlanUpgradeResultValue);
					if (0 != nRezlt1)//有一位不是0就是失败
					{
						Log.Error("软件包同步失败!");
						_upgradePackInfo.MakeInvalid();
					}
				}
			}
			else if (swPackPlanUpgradeStateValue.Equals(FileTransMacro.STR_ACTIVATING))		// 正在激活
			{
				if (FILETRANSOPER.UnKnown == _upgradePackInfo.GetFileTransStatus())
				{
					//首次收到激活状态不处理
					Log.Debug("首次收到激活状态不处理!");
					return;
				}

				if (FILETRANSOPER.Activing != _upgradePackInfo.GetFileTransStatus())
				{
					_upgradePackInfo.SetFileTransStatus(FILETRANSOPER.Activing);
				}

				//激活进行结果判断
				var nRezlt1 = int.Parse(swPackPlanUpgradeResultValue);
				if (0 != nRezlt1)	//有一位不是0就是失败
				{
					Log.Info("获取软件包激活结果1失败!");
					_upgradePackInfo.MakeInvalid();
					return;
				}

				if (!_upgradePackInfo.IsMainEqpSw())	//外设软件包
				{
					if (pdu.GetValueByMibName(_boardIp, swPackPlanUpgradeResult2, out swPackPlanUpgradeResultValue2))
					{
						var nRezlt2 = int.Parse(swPackPlanUpgradeResultValue2);
						if (0 != nRezlt2)		//有一位不是0就是失败
						{
							Log.Info("软件包激活失败!");
							_upgradePackInfo.MakeInvalid();
							return;
						}
					}
					else
					{
						Log.Info("获取软件包激活结果2失败!");
						_upgradePackInfo.MakeInvalid();
						return;
					}
				}

				//激活结果都成功的情况下，在设置进度条状态
				processBarInfo.m_eOperationType = OPERTYPE.OPERTYPE_ACTIVE;
				processBarInfo.m_eStatus = FILETRANSSTATE.TRANSSTATE_ACTIVEING;
				processBarInfo.m_strStatus = "正在激活...";
				SetProcessInfo(processBarInfo);
			}

			var EndInOutPdu = new CDTLmtbPdu();
			string swPackPlanOpEndingIndicatorValue;

			var EndResult = CDTCmdExecuteMgr.GetInstance().CmdGetSync(PackPlanOpEndingCmd, out reqId , index, _boardIp, ref EndInOutPdu);
			if (0 != EndResult)
			{
				if (_operaFailCount++ > 3)
				{
					Log.Error($"下发{PackPlanOpEndingCmd}命令失败");
					EndProgressBar();
					_workingForUpgrade = false;
				}
				return;
			}

			_operaFailCount = 0;
			if (!EndInOutPdu.GetValueByMibName(_boardIp, swPackPlanOpEndingIndicator, out swPackPlanOpEndingIndicatorValue))
			{
				Log.Error($"获得节点{swPackPlanOpEndingIndicatorValue}值失败");
				return;
			}

			Log.Info($"查询到{swPackPlanOpEndingIndicator}={swPackPlanOpEndingIndicatorValue}");
			if ("1" == swPackPlanOpEndingIndicatorValue) //全部结束
			{
				EndProgressBar();
				_workingForUpgrade = false;
			}
		}

		// 文件传输任务进度条状态更新
		private void CommonFileTransProcDeal()
		{
			_workingForFileTrans = false;

			var unFinishTaskIdList = GetUnFinishFileTransTaskId();
			var nUnFinishTaskNum = unFinishTaskIdList.Count;
			Log.Info($"查询未完成传输任务个数：{nUnFinishTaskNum}");

			var inOutPdu = new CDTLmtbPdu();

			foreach (var taskId in unFinishTaskIdList)
			{
				var csIndex = $".{taskId}";
				Log.Info($"查询索引为{csIndex}文件传输进度");

				long lrequestId = 0;
				var nGetCmdRezlt = CDTCmdExecuteMgr.GetInstance().CmdGetSync("GetFileTransPercent", out lrequestId, csIndex, _boardIp, ref inOutPdu);
				if (0 == nGetCmdRezlt)
				{
					var nFileTransTaskId = taskId;

					if (!_mapTraningFileTask.ContainsKey(nFileTransTaskId))
					{
						Log.Error($"未找到taskid={nFileTransTaskId}的任务信息");
						continue;
					}

					var pFileTransDes = _mapTraningFileTask[nFileTransTaskId];
					if (null == pFileTransDes)
					{
						Log.Error("获取文件传输描述结构体失败!");
						continue;
					}

					//需要获取当前文件的状态（传输还是解压），和进度，文件名
					string csFileTransState;
					string csFileTransPercent;
					if (inOutPdu.GetValueByMibName(_boardIp, "fileTransState", out csFileTransState, csIndex) &&
						inOutPdu.GetValueByMibName(_boardIp, "fileTransPercent", out csFileTransPercent, csIndex))
					{
						var nPercent = int.Parse(csFileTransPercent);
						Log.Info($"文件id={csIndex},文件状态 {csFileTransState},进度{nPercent}");

						if (FileTransMacro.STATE_VALUE_UNZIPING == csFileTransState)
						{
							UpdateProgress(nFileTransTaskId, nPercent, FILETRANSOPER.UnZipping);
						}
						else
						{
							UpdateProgress(nFileTransTaskId, nPercent, pFileTransDes.enumFileTransOp);
						}
					}
					else
					{
						Log.Error("从PDU中获取变量失败fileTransState fileTransPercent");

						//防止命令下发失败后，进度条一直停在那动不动，收到传输完毕后仍没关闭
						//_workingForFileTrans = true;
						//_nGetSnmpFailCount++;
						//if (_nGetSnmpFailCount > 3)
						//{
						//	_nGetSnmpFailCount = 0;
						//	Log.Error("Get命令返回失败超过3次，取消文件传输任务");
						//	_workingForFileTrans = false;
						//	break;
						//}

						continue;
					}

					unFinishTaskIdList = GetUnFinishFileTransTaskId();
					nUnFinishTaskNum = unFinishTaskIdList.Count;

					Log.Info($"处理后，查询未完成传输任务个数：{nUnFinishTaskNum}");

					if (nUnFinishTaskNum > 0)
					{
						_workingForFileTrans = true;
					}
				}
				else
				{
					//防止命令下发失败后，进度条一直停在那动不动，收到传输完毕后仍没关闭
					_workingForFileTrans = true;
					_nGetSnmpFailCount++;
					if (_nGetSnmpFailCount > 3)
					{
						_nGetSnmpFailCount = 0;
						Log.Error("Get命令返回失败超过3次，取消文件传输任务");
						_workingForFileTrans = false;
					}
					Log.Error($"同步Get命令返回失败GetFileTransTask,Index = {csIndex},Error code = {nGetCmdRezlt}！");
				}
			}
		}

		// 进度条设置为100%。只有拖包升级时才调用这个方法
		private void EndProgressBar()
		{
			if (null != _swUpgradePbBarInfo)
			{
				_swUpgradePbBarInfo.m_nPercent = 100;
				_swUpgradePbBarInfo.m_eOperationType = OPERTYPE.OPERTYPE_FINISHED;
				string stateText = "";
				_swUpgradePbBarInfo.m_eStatus = CheckEnbPhase(true, ref stateText);
				_swUpgradePbBarInfo.m_strStatus = stateText;
				UpdateProgressEvent?.Invoke(_swUpgradePbBarInfo);
			}
		}

		private void EndUIProcessBar(TProgressBarInfo progressBarInfo)
		{
			EndProgressEvent?.Invoke(progressBarInfo);
		}

		// 更新当前进度条的信息。只有拖包升级才调用这个方法
		private void SetProcessInfo(TProgressBarInfo progressBarInfo)
		{
			if (null == _swUpgradePbBarInfo)	// 没有的时候要新增一个进度条
			{
				_swUpgradePbBarInfo = progressBarInfo;
				NewProgressEvent?.Invoke(_swUpgradePbBarInfo);
			}
			else
			{
				_swUpgradePbBarInfo = progressBarInfo;
				UpdateProgressEvent?.Invoke(_swUpgradePbBarInfo);
			}
		}

		private FILETRANSSTATE CheckEnbPhase(bool bEndWork, ref string stateText)
		{
			var tProgressBarInfo = _swUpgradePbBarInfo;
			var state = FILETRANSSTATE.TRANSSTATE_UNKNOWN;

			if (null == stateText)
			{
				stateText = "";
			}
			switch (tProgressBarInfo.m_eOperationType)
			{
				case OPERTYPE.OPERTYPE_DOWNLOAD:    //下载
					if (bEndWork)
					{
						state = FILETRANSSTATE.TRANSSTATE_DOWNLOADFINISHED;
						stateText = "下载已完成";
					}
					else
					{
						state = FILETRANSSTATE.TRANSSTATE_DOWNLOADFAILED;
						stateText = "下载失败";
					}
					break;
				case OPERTYPE.OPERTYPE_UNZIP:       //解压
					if (bEndWork)
					{
						state = FILETRANSSTATE.TRANSSTATE_UPZIPFINISHED;
						stateText = "解压已完成";
					}
					else
					{
						state = FILETRANSSTATE.TRANSSTATE_UPZIPFAILED;
						stateText = "解压失败";
					}
					break;
				case OPERTYPE.OPERTYPE_ACTIVE:      //激活
					if (bEndWork)
					{
						state = FILETRANSSTATE.TRANSSTATE_ACTIVEFINISHED;
						stateText = "激活已完成";
					}
					else
					{
						state = FILETRANSSTATE.TRANSSTATE_ACTIVEFAILED;
						stateText = "激活失败";
					}
					break;
				case OPERTYPE.OPERTYPE_SYN:     //同步
					if (bEndWork)
					{
						state = FILETRANSSTATE.OPERTYPE_SYNFINISHED;
						stateText = "同步已完成";
					}
					else
					{
						state = FILETRANSSTATE.OPERTYPE_SYNFAILED;
						stateText = "同步失败";
					}
					break;
			}
			return state;
		}

		// 获取未完成任务的ID。返回List
		private List<long> GetUnFinishFileTransTaskId()
		{
			return _mapTraningFileTask.Keys.ToList();
		}

		//删除未完成任务
		public void DeleteUnFinishedTransTask(long lTaskID)
		{
			if(_mapTraningFileTask.ContainsKey(lTaskID))
			{
				_mapTraningFileTask.Remove(lTaskID);
			}

			_workingForFileTrans = false;

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
				// TODO 取消成功后，需要处理后续的流程
				return SENDFILETASKRES.TRANSFILE_TASK_SUCCEED;
			}

			return SENDFILETASKRES.TRANSFILE_TASK_FAILED;
		}


		private void OnGetCapacityRsp(SubscribeMsg msg)
		{
			var rspMsg = JsonHelper.SerializeJsonToObject<SubscribeMsg>(msg.Data);
			if (null == rspMsg)
			{
				Log.Error("转换消息为SubscribeMsg失败");
				return;
			}

			var ip = rspMsg.Topic;
			if (ip.Equals(_boardIp))
			{
				var gcRsp = new SI_SILMTENB_GetCapacityRspMsg();
				if (-1 == gcRsp.DeserializeToStruct(rspMsg.Data, 0))
				{
					gcRsp.s8GetResult = 1;
				}

				MenuClickRspEvent?.Invoke(gcRsp);
			}
		}

		#endregion

		#region 私有方法

		private bool CanUpdate()
		{
			// TODO 需要判断的内容需要明确
			return true;
		}

		// 路径字符有效性判断
		private bool IsValidPath(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}

			return (-1 == path.IndexOf("&", StringComparison.Ordinal));
		}

		// 判断是否存在有其他正在进行的任务
		private bool HasRunningTask()
		{
			return _workingForFileTrans || _workingForUpgrade;
		}

		// 创建临时文件
		private string CreateTempFile(string srcFile, string targetIp)
		{
			if (!IsValidPath(srcFile))
			{
				throw new CustomException($"文件路径{srcFile}有非法字符&");
			}

			var dstDirPath = FilePathHelper.GetTempFilesPath() + $"{targetIp}/DTZ/";

			if (!FilePathHelper.DeleteFolder(dstDirPath))
			{
				throw new CustomException($"删除临时目录{dstDirPath}失败");
			}

			if (!FilePathHelper.CreateFolder(dstDirPath))
			{
				throw new CustomException($"创建临时目录{dstDirPath}失败");
			}

			var srcFileName = FilePathHelper.GetFileNameFromFullPath(srcFile);
			if (null == srcFileName)
			{
				throw new CustomException($"从路径{srcFile}中获取文件名失败，文件不存在");
			}

			var dstFilePath = $"{dstDirPath}{srcFileName}";
			var srcFullPath = FilePathHelper.ReplaceDosSlashToLinux(srcFile);
			var dstFullPath = FilePathHelper.ReplaceDosSlashToLinux(dstFilePath);

			if (!FilePathHelper.CopyFile(srcFullPath, dstFullPath))
			{
				throw new CustomException($"复制文件{srcFile}到{dstFilePath}失败！");
			}

			return dstFullPath;
		}

		// 把文件类型枚举值转换为刻度文本
		private string GetFileTypeText(string fileTypeDigit)
		{
			var type = Int32.Parse(fileTypeDigit);
			var text = "";
			if (_mapTransFileType.Count == 0)
			{
				if (!GetTransFileTypeFromDB())
				{
					return text;
				}
			}

			if (_mapTransFileType.ContainsKey(type))
			{
				text = _mapTransFileType[type];
			}

			return text;
		}

		// 从数据库中读取传输文件类型
		private bool GetTransFileTypeFromDB()
		{
			_mapTransFileType = SnmpToDatabase.GetValueRangeByMibName("fileTransType", _boardIp);
			return (null != _mapTransFileType);
		}

		#endregion

		#region 私有成员、属性

		private readonly string _boardIp;
		private Dictionary<int, string> _mapTransFileType;
		private bool _workingForUpgrade;
		private bool _workingForFileTrans;
		private CDTCommonFileTrans _commonFileTransInfo;
		private CSWPackPlanProcInfoMgr _upgradePackInfo;
		private Timer _timer;
		private int _operaFailCount;
		private int _nGetSnmpFailCount;
		private Dictionary<long, StruFileTransDes> _mapTraningFileTask;		// 传输中的任务，key : taskid
		private TProgressBarInfo _swUpgradePbBarInfo;

		#endregion

	}

	public class TProgressBarInfo
	{
		public string m_csNEInfo;                   //网元信息
		public string m_csFileName;					//文件信息
		public int m_nPercent;                      //操作完成百分比
		public FILETRANSSTATE m_eStatus;            //状态
		public OPERTYPE m_eOperationType;           //操作类型
		public string strOperationType;				//操作类型文本描述

		public long m_lTaskID;						//任务ID

		public string m_strStatus;					//状态的文本描述
	};
}
