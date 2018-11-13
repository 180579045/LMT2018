/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ SnmpErrDescHelper $
* 机器名称：       $ machinename $
* 命名空间：       $ LinkPath $
* 文 件 名：       $ SnmpErrDescHelper.cs $
* 创建时间：       $ 2018.10.XX $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     Snmp报文处理类。
* 修改时间     修 改 人         修改内容：
* 2018.10.xx  XXXX            XXXXX
*************************************************************************************/

using CommonUtility;
using DataBaseUtil;
using DataSync;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using MsgQueue;
using SnmpSharpNet;
using SuperLMT.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace LinkPath
{
	/// <summary>
	/// Snmp报文处理类
	/// </summary>
	public class CDTSnmpMsgDispose
	{
		// 记录来自基站最近几条trap的RequestId，用于过滤重复发送的Trap消息
		// 事件Trap id
		Dictionary<string, List<long>> m_ipToRequestIdsDicForEvent = new Dictionary<string, List<long>>();
		// 配置变更Trap id
		Dictionary<string, List<long>> m_ipToRequestIdsDicForConfigChg = new Dictionary<string, List<long>>();
		// 告警Trap id
		Dictionary<string, List<long>> m_ipToRequestIdsDicForAlarm = new Dictionary<string, List<long>>();

		public int FileTransMacro { get; private set; }

		public DTLinkPathMgr m_LinkMgr;

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="linkPathMgr"></param>
		public CDTSnmpMsgDispose(DTLinkPathMgr linkPathMgr)
		{
			// 订阅SNMP模块发来的消息
			 SubscribeHelper.AddSubscribe(TopicHelper.SnmpMsgDispose_OnResponse, CallOnResponse);
			// 订阅Trap消息
			SubscribeHelper.AddSubscribe(TopicHelper.SnmpMsgDispose_OnTrap, CallOnTrap);

			m_LinkMgr = linkPathMgr;

			// TODO
			// alarmDealWorker = (CDTAlarmDealOpr*)AfxBeginThread(RUNTIME_CLASS(CDTAlarmDealOpr));
		}

		#region 订阅消息调用
		/// <summary>
		/// 调用OnResponse方法
		/// </summary>
		/// <param name="msg"></param>
		private void CallOnResponse(SubscribeMsg msg)
		{
			// 消息类型转换
			Log.Info($"msg.Topic = {msg.Topic}");

			var lmtPdu = SerializeHelper.DeserializeWithBinary<CDTLmtbPdu>(msg.Data);
			OnResponse(lmtPdu);
		}

		/// <summary>
		/// 调用OnTrap方法
		/// </summary>
		/// <param name="msg"></param>
		private void CallOnTrap(SubscribeMsg msg)
		{
			// 消息类型转换
			var strTopic = msg.Topic;
			Log.Info($"msg.Topic = {msg.Topic}");

			var lmtPdu = SerializeHelper.DeserializeWithBinary<CDTLmtbPdu>(msg.Data);
			OnTrap(lmtPdu);
		}

		#endregion

		/// <summary>
		/// 处理接收到的Trap消息
		/// </summary>
		/// <param name="lmtPdu"></param>
		public int OnTrap(CDTLmtbPdu lmtPdu)
		{
			if (lmtPdu == null)
			{
				Log.Error("发来的Trap报文为空!");
				return -1;
			}

			// 获取网元IP
			var strNodeIp = lmtPdu.m_SourceIp;
			Log.Info($"收到网元Trap, 网元ip:{strNodeIp}");

			// 验证包的合法性
			string strErrorMsg;
			if (false == CheckPDUValidity(lmtPdu, out strErrorMsg))
			{
				if (strErrorMsg != "")
				{
					// 打印消息
					ShowLogHelper.Show(strErrorMsg, lmtPdu.m_SourceIp, InfoTypeEnum.ENB_OTHER_INFO_IMPORT);
				}
				Log.Error("Trap Pdu验证失败!");
				return -1;
			}

			// 获得该网元所对应的MIB OID前缀
			var strOidPrefix = SnmpToDatabase.GetMibPrefix();
			if (string.IsNullOrEmpty(strOidPrefix))
			{
				Log.Error("获取MIB前缀失败!");
				return -1;
			}

			// 验证是否是认识的Trap类型
			var lmtVb = new CDTLmtbVb();
			lmtPdu.GetVbByIndex(1, ref lmtVb); // 第0个为时间戳，第1个为Trap包的OID
			var intTrapType = 0;
			if(false == CheckTrapOIDValidity(strNodeIp, lmtVb.Value, strOidPrefix, out intTrapType))
			{
				Log.Error(string.Format("验证Trap类型失败,未知Trap类型,OID为{0}！"), lmtVb.Value);
				return -1;
			}

			// TODO: 方便观察消息，生产环境时需去掉
			ShowLogHelper.Show($"Trap消息，TrapType:{intTrapType}", lmtPdu.m_SourceIp
				, InfoTypeEnum.ENB_OTHER_INFO_IMPORT);

			// 按不同类型处理Trap
			switch (intTrapType)
			{
				case 2:
				case 24: // //alarmTraps
					//告警处理  注意添加各告警字段和日志的值
					//验证是否是同一个trap
					if (InterceptRepeatedTrap4Alarm(strNodeIp, lmtPdu.m_requestId))
					{
						Log.Info($"收到相同Trap, request id:{lmtPdu.m_requestId}");
						return 0;
					}

					// TODO

					break;
				case 3:
				case 21: //eventConfigChgTraps
					// 验证是否是同一个trap
					if (InterceptRepeatedTrap4ConfigChg(strNodeIp, lmtPdu.m_requestId))
					{
						Log.Info($"收到相同Trap, request id:{lmtPdu.m_requestId}");
						return 0;
					}
					// 消息处理
					DTDataSyncMgr.GetInstance().DealAlteration(lmtPdu);
					break;
				case 9:
				case 16:	//eventFTPResultTraps
				case 20:    //eventGeneralEventTraps
				case 22:    //eventManagementRequestTraps
				case 23:    //eventFTPResultTraps
				case 25:    //transactionResultTraps
				case 26:    //eventSynchronizationTrap   同步活跃告警和一致性文件事情的处理
				case 200:   //ANR专用事情
				case 201:   //MRO专用事情
				case 202:   //FC专有事件		
				case 203:	//maintenceStateNotify 工程状态通知 
				case 204:   //nodeBlockStateNotify 
					// 事件处理
					// 验证是否是同一个trap
					if(InterceptRepeatedTrap4Event(strNodeIp, lmtPdu.m_requestId))
					{
						Log.Info($"收到相同Trap, id:{lmtPdu.m_requestId}");
						return 0;
					}

					// 事件处理
					if (DealEventTrap(intTrapType, lmtPdu) == false)
					{
						Log.Error($"DealEventTrap事件处理失败,intTrapType:{intTrapType}");
						return -1;
					}

					break;
				default:
					// 未知类型上报
					Log.Error($"CDTSnmpMsgDispose::OnTrap方法中未知类型Trap上报:{lmtVb.Value}");
					break;
			}

			return 0;
		}

		/// <summary>
		/// 处理SNMP模块发来的Get/Set的Response
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <returns></returns>
		public int OnResponse(CDTLmtbPdu lmtPdu)
		{
			Log.Info($"收到网元Response，网元IP:{lmtPdu.m_SourceIp}");

			// 获取MIB前缀
			var prefix = SnmpToDatabase.GetMibPrefix().Trim('.');
			if (string.IsNullOrEmpty(prefix))
			{
				Log.Error("获取MIB前缀失败!");
				return -1;
			}

			// 验证RequestID的合法性，从数据库中读出信息并删除
			var idToTb = new IDToTableStruct
			{
				pduType = lmtPdu.getReqMsgType(),
				messageType = lmtPdu.m_requestId,
				strCmdName = lmtPdu.get_CmdName() ?? ""
			};

			// 验证包的合法性
			var strErrorMsg = "";
			if(false == CheckPDUValidity(lmtPdu, out strErrorMsg))
			{
				if (strErrorMsg != "")
				{
					ShowLogHelper.Show(strErrorMsg, lmtPdu.m_SourceIp, InfoTypeEnum.ENB_OTHER_INFO_IMPORT);
					Log.Error("CheckPDUValidity()函数返回失败");
					return -1;
				}
			}

			// Response PDU处理函数
			if(lmtPdu.reason != -5 ) // SNMP_CLASS_TIMEOUT
			{
				if(lmtPdu.m_LastErrorStatus == 0) // SNMP_ERROR_SUCCESS
				{
					DealSuccResponsePDU(idToTb, lmtPdu);
				} // 如果是非getbulk的错误响应，不需要打印响应，则不用生成出错信息
				else if (lmtPdu.m_bIsNeedPrint || lmtPdu.getReqMsgType() == (int)PduType.GetBulk)
				{
					DealFailResponsePDU(idToTb, lmtPdu);
				}
			}
			else if (lmtPdu.m_bIsNeedPrint)
			{
				var strTimeoutMsg = string.Format(CommString.IDS_STR_MSGDISPOSE_FMT1, lmtPdu.get_CmdName());
				ShowLogHelper.Show(strTimeoutMsg, lmtPdu.m_SourceIp);
			}

			// 向消息分发中心发送，必须注册为同步

			// 如果是同步的Snmp命令，不需要分发该响应
			if (lmtPdu.getSyncId() == false)
			{
				// TODO
				//LRESULT lt;
				//CDtMsgDispCenter::Initstance().ProcessWindowMessage(NULL, WM_APPRESPONSE, (WPARAM)pLmtbPdu, (LPARAM)(&idToTb), lt);

			}

			// 文件管理的处理，通过消息订阅调用
			FileTransTaskMgr.GetInstance().ResponseDeal(lmtPdu);

			return 0;
		}


		/// <summary>
		/// 失败的Response PDU处理函数
		/// </summary>
		/// <param name="idToTable"></param>
		/// <param name="lmtPdu"></param>
		private void DealFailResponsePDU(IDToTableStruct idToTable, CDTLmtbPdu lmtPdu)
		{
			// 保活命令下发的信息不需要打印
			if (idToTable.strCmdName.Equals(CommStructs.EPC_KEEPALIVE_SNMPFUNCNAME))
			{
				// TODO
				// m_pLinkMgr->SetNetElemAlive(strIpAddr);
				return;
			}

			// 获取Snmp错误信息
			var snmpErrDesc = SnmpErrDescHelper.GetErrDescById(lmtPdu.m_LastErrorStatus.ToString());
			var strFailedReason = snmpErrDesc != null ? snmpErrDesc.errorChDesc : lmtPdu.m_LastErrorStatus.ToString();

			// 获取名称、描述信息等信息
			var lmtVb = new CDTLmtbVb();
			var strName = "";
			if (lmtPdu.m_LastErrorIndex > 0)
			{
				var idx = (int)lmtPdu.m_LastErrorIndex - 1;
				if (idx > 0 && idx < lmtPdu.VbCount())
				{
					lmtPdu.GetVbByIndex(idx, ref lmtVb);
					var strUnitName = "";
					var strDesc = "";
					if (false == CommSnmpFuns.GetInfoByOID(lmtPdu.m_SourceIp, lmtVb.Oid, lmtVb.Value
						, out strName, out strDesc, out strUnitName))
					{
						Log.Error($"GetInfoByOID调用不成功,:OID = {lmtVb.Oid}");
					}
				}
				else
				{
					Log.Error($"idx 超出PDU Count数 idx={idx} lmtPdu.VbCount()={lmtPdu.VbCount()}");
				}
			}

			// 后台执行的命令,不要显示在界面上了

			// 控制台显示信息
			var strShowMsg = "";
			var strStyle = "";
			var strTmp = "";

			if (!string.IsNullOrEmpty(strName))
			{
				// 变量 %s %s
				strTmp = $"变量 {strName} {strFailedReason}";
			}
			else
			{
				strTmp = strFailedReason;
			}

			// 操作类型
			var optType = InfoTypeEnum.ENB_OTHER_INFO_IMPORT;
			if (idToTable.pduType == (int)PduType.Set)
			{
				strStyle = CommString.IDS_SETPDU_ERROR; //SET命令响应错误
				optType = InfoTypeEnum.ENB_SETOP_ERR_INFO;
			}
			else if (idToTable.pduType == (int)PduType.Get) //GET命令响应错误
			{
				strStyle = CommString.IDS_GETPDU_ERROR;
				optType = InfoTypeEnum.ENB_GETOP_ERR_INFO;
			}
			else
			{
				strStyle = CommString.IDS_PDU_ERROR;  //命令响应错误
			}

			strShowMsg = $"{strStyle}:{strTmp}";

			ShowLogHelper.Show(strShowMsg, lmtPdu.m_SourceIp, optType);

			// TODO
			//wangyun1 For CmdLine 2011-8-11----------------------------->
			//LPARAM lt;
			//char* pStrMsg = (char*)(LPCTSTR)strMsg;
			//CDtMsgDispCenter::Initstance().ProcessWindowMessage(NULL, WM_MSGCMD_LOG, (WPARAM)pStrMsg, 0, lt);
			//wangyun1 For CmdLine 2011-8-11<-----------------------------

		}

		/// <summary>
		/// 成功的Response PDU处理函数
		/// </summary>
		/// <param name="idToTable"></param>
		/// <param name="lmtPdu"></param>
		private void DealSuccResponsePDU(IDToTableStruct idToTable, CDTLmtbPdu lmtPdu)
		{
			// 保活命令下发的信息不需要打印
			if (idToTable.strCmdName.Equals(CommStructs.EPC_KEEPALIVE_SNMPFUNCNAME)) 
			{
				return;
			}

			InfoTypeEnum infoType;
			if (idToTable.pduType == (int)PduType.Get || idToTable.pduType == (int)PduType.GetBulk)
			{
				// GET命令响应
				infoType = InfoTypeEnum.ENB_GETOP_INFO;
			}
			else if (idToTable.pduType == (int)PduType.Set)
			{
				// SET命令响应
				infoType = InfoTypeEnum.ENB_SETOP_INFO;
			}
			else
			{
				// 命令响应
				infoType = InfoTypeEnum.ENB_INFO;
			}

			// 显示信息
			var strShowMsg = "";

			// 操作结果
			var strOperResult = CommString.IDS_OPERLOG_SUCCESS; // "成功"

			// 遍历vb对，通知上层更新数据
			var vbCount = lmtPdu.VbCount();
			for (var i = 0; i < vbCount; i++)
			{
				var lmtVb = new CDTLmtbVb();
				lmtPdu.GetVbByIndex(i, ref lmtVb);
				// 获取名称及描述等信息
				var strName = "";
				var strDesc = "";
				var strUnitName = "";
				if (lmtPdu.m_bIsNeedPrint 
						&& false == CommSnmpFuns.GetInfoByOID(lmtPdu.m_SourceIp, lmtVb.Oid
							, lmtVb.Value, out strName, out strDesc, out strUnitName))
				{
					Log.Error($"GetInfoByOID调用不成功:OID = {lmtVb.Oid}");
					continue;
				}

				// 长度校验
				if (lmtVb.Value != null && lmtVb.Value.Length > CommStructs.MAX_OID_SIZE)
				{
					Log.Error("PDU的OID长度超过定义的最大长度");
					return;
				}
				if (lmtVb.Oid.Length > CommStructs.MAX_VALUE_LEN)
				{
					Log.Error("PDU的Value长度超过定义的最大长度");
					return;
				}

				// 需要打印信息
				if (lmtPdu.m_bIsNeedPrint)
				{
					var strMsg = string.Format(CommString.IDS_INFOMSGSTYLE, strName, strDesc);
					if (!string.IsNullOrEmpty(strUnitName))
					{
						strMsg = $"{strMsg} ({CommString.IDS_UNITNAME}{strUnitName})";
					}

					// 时间信息
					// TODO:打印信息中已经有统一的时间了，在这在计算时间没意义吧？暂时去掉

					strShowMsg = $"{strShowMsg}{strMsg};\n";

					// 如果是set操作，写操作日志到数据库
					if (idToTable.pduType == (int)PduType.Set)
					{
						// TODO 旧工具也没有这部分的实现
					}
				} // end if
			} // end for

			// 数据写入到数据库中，把数据库联接和PDU发给数据同步工作线程
			// CDTDataSyncMgr::GetInstance()->DealResponse(pAdoConn, pLmtbPdu);
			// TODO

			if (lmtPdu.m_bIsNeedPrint)
			{
				// 去掉最后一个回车
				strShowMsg = strShowMsg.TrimEnd('\n');
				// 往消息输出窗体显示信息
				ShowLogHelper.Show(strShowMsg, lmtPdu.m_SourceIp, infoType);

				// TODO
				//LPARAM lt;
				//char* pStrMsg = (char*)(LPCTSTR)strMsg;
				//CDtMsgDispCenter::Initstance().ProcessWindowMessage(NULL, WM_MSGCMD_LOG, (WPARAM)pStrMsg, 0, lt);

			}
		}

		/// <summary>
		/// 事件Trap处理
		/// </summary>
		/// <param name="intTrapType"></param>
		/// <param name="lmtPdu"></param>
		/// <returns></returns>
		private bool DealEventTrap(int intTrapType, CDTLmtbPdu lmtPdu)
		{
			// 网元IP
			var strNodeBIp = lmtPdu.m_SourceIp;
			string strValue;

			string strEventInfo;
			// 判定事件类型，输出事件信息
			if (false == ClassifyEvent(intTrapType, lmtPdu, out strEventInfo))
			{
				Log.Error($"事件分类处理方法ClassifyEvent返回错误:{strEventInfo}");
				return false;
			}

			// 通用事件
			if (intTrapType == 20)
			{
				// 通用事件中载波状态上报事件不需要处理
				lmtPdu.GetValueByMibName(strNodeBIp, "eventGeneralEventType", out strValue);
				// 8:carrierCheck|载波状态上报
				if (Convert.ToInt32(strValue) == 8)
				{
					Log.Info("载波状态上报事件，不需要处理.");
					return true;//不需进行处理
				}
				else if (Convert.ToInt32(strValue) == 31)
				{
					// 处理通用事件中初始化结果上报
					DealInitEventTrap(lmtPdu);
				}
			}

			// 保存事件信息
			if (false == SaveEventTrap(lmtPdu, strEventInfo))
			{
				Log.Error($"SaveEventTrap返回失败:{strEventInfo}");
			}

			// 文件传输事件处理
			if (intTrapType == 16 || intTrapType == 23) // 文件传输事件
			{
				DealFileTransTrap(lmtPdu);
			}

			// 向消息分发中心发送变更通知
			// TODO
			/*
			LRESULT lt;
			CDtMsgDispCenter::Initstance().ProcessWindowMessage(NULL, WM_APPEVENTTRAP, (WPARAM)iTrapType, (LPARAM)pLmtbPdu, lt);
			*/
			// 将lmtPdu转换为string以便传递
			var strLmtPdu = JsonHelper.SerializeObjectToString(lmtPdu);
			var strPars = $"{{'TrapType' : {intTrapType}, 'LmtPdu' : {strLmtPdu} }}";
			PublishHelper.PublishMsg(TopicHelper.SnmpMsgDispose_AppEventTrap, strPars);

			// fileTransNotiResult
			if (lmtPdu.GetValueByMibName(strNodeBIp, "fileTransNotiResult", out strValue))
			{
				//1:正在传输/2:正在解压缩
				if ("1".Equals(strValue) || "2".Equals(strValue))
				{
					// 不需要打印结果
					return true;
				}
			}

			// 输出信息
			strEventInfo = $"{CommString.IDS_RECEIVE}{strEventInfo}";
			ShowLogHelper.Show(strEventInfo, lmtPdu.m_SourceIp, InfoTypeEnum.OM_EVENT_NOTIFY_INFO);

			return true;
		}

		/// <summary>
		/// 处理事件Trap之文件传输
		/// </summary>
		/// <param name="lmtPdu">上传的PDU</param>
		private void DealFileTransTrap(CDTLmtbPdu lmtPdu)
		{
			// 网元IP
			var strIPAddr = lmtPdu.m_SourceIp;

			string strValue;
			// 文件传输结果
			lmtPdu.GetValueByMibName(strIPAddr, "fileTransNotiResult", out strValue); 
			if (!string.IsNullOrEmpty(strValue) && Convert.ToInt32(strValue) != 0)
			{
				Log.Error("文件传输不成功.");
				return;
			}

			// 文件传输类型
			lmtPdu.GetValueByMibName(strIPAddr, "fileTransNotiIndicator", out strValue);
			// ForTest
			// strValue = "1";
			if (string.IsNullOrEmpty(strValue) || Convert.ToInt32(strValue) != 1)
			{
				Log.Error("不是文件上传.");
				return;
			}

			// 上传的文件类型
			lmtPdu.GetValueByMibName(strIPAddr, "fileTransNotiFileType", out strValue);
			var uploadFileType = 0;
			if (!string.IsNullOrEmpty(strValue))
			{
				uploadFileType = Convert.ToInt32(strValue);
			}
				

			// 含FTP服务器路径的文件名
			lmtPdu.GetValueByMibName(strIPAddr, "fileTransNotiFileName", out strValue);
			if (string.IsNullOrEmpty(strValue))
			{
				Log.Error("文件名为空.");
				return;
			}

			Log.Info($"文件名:{strValue}");
			//将文件路径转换成windows格式
			strValue = strValue.Replace('/', '\\');
			strValue = strValue.Trim();

			// 件是否存在
			if (FilePathHelper.FileExists(strValue) != true)
			{
				Log.Error($"上传的文件{strValue}不存在!");
				return;
			}

			// lm.dtz文件
			if (26 == uploadFileType)
			{
				var strUploadFilePath = strValue;
				// 发布消息
				PublishHelper.PublishMsg(TopicHelper.LoadLmdtzToVersionDb
					, $"{{\"SourceIp\" : \"{lmtPdu.m_SourceIp}\", \"UploadFilePath\" :\"{strUploadFilePath}\" }}");

				return;
			}

			// 文件的扩展名
			var strFileType = strValue.Substring(strValue.Length - 3, 3);
			strFileType = strFileType.ToLower();

			
			if (strFileType == "dcb") // 一致性文件
			{
				int index;
				// 检查这个文件是否在指定目录下,如果在,则解析之
				// 文件全路径
				var strUpLoadFullPath = strValue;

				// 去掉文件名称，获取文件路径
				index = strUpLoadFullPath.LastIndexOf("\\", StringComparison.Ordinal);
				var strUpLoadPath = strUpLoadFullPath.Substring(0, index + 1);

				// 系统指定路径
				var strAppointPath = AppPathUtiliy.Singleton.GetAppPath() + "filestorage\\DATA_CONSISTENCY";

				// 去掉路径中的所有"\"，然后进行比较是否相同
				strUpLoadPath = strUpLoadPath.Replace("\\", "");
				strAppointPath = strAppointPath.Replace("\\", "");
				if (!string.Equals(strUpLoadPath, strAppointPath, StringComparison.OrdinalIgnoreCase))
				{
					Log.Error("上传的文件不在指定路径下，不做解析.");
					return;
				}

				Log.Info($"数据一致性文件, 发送解析消息, 网元标示:{strIPAddr}, 文件路径:{strUpLoadFullPath}");

				// 让数据同步模块解析一致性文件
				// 发布消息
				PublishHelper.PublishMsg(TopicHelper.ParseDataConFile
					, $"{{\"SourceIp\" : \"{lmtPdu.m_SourceIp}\", \"UpdatePath\" : \"{strUpLoadPath}\"}}");

				return;
			}

			Log.Error($"上传其他的文件类型{strFileType}，暂时不处理.");
		}

		/// <summary>
		/// 保存事件Trap信息
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="strEventInfo"></param>
		/// <returns></returns>
		private bool SaveEventTrap(CDTLmtbPdu lmtPdu, string strEventInfo)
		{
			// TODO: 不知道trap消息存放到哪个表中？后续实现

			return true;
		}

		/// <summary>
		/// 初始化事件上报处理
		/// </summary>
		/// <param name="lmtPdu"></param>
		private void DealInitEventTrap(CDTLmtbPdu lmtPdu)
		{
			if (null == lmtPdu)
			{
				return;
			}
			// 基站IP
			var strNodeIp = lmtPdu.m_SourceIp;

			long taskId = 0;
			long requestId = 0;
			//收到初配上报事件, 发起一致性文件上传
			var strDataConsisFolderPath = AppPathUtiliy.Singleton.GetDataConsistencyFolderPath();
			var transFileObj = FileTransTaskMgr.FormatTransInfo(
															strDataConsisFolderPath
															,""
															, Transfiletype5216.TRANSFILE_dataConsistency
															, TRANSDIRECTION.TRANS_UPLOAD);
			if (SENDFILETASKRES.TRANSFILE_TASK_FAILED == FileTransTaskMgr.SendTransFileTask(
				strNodeIp, transFileObj, ref taskId, ref requestId))
			{
				Log.Error($"下发上传数据一致性文件传输任务失败，数据一致性文件目录{strDataConsisFolderPath}，网元IP为{strNodeIp}");

			}
			else
			{
				Log.Info($"下发上传数据一致性文件传输任务成功！--网元IP为{strNodeIp}");
			}

			// TODO
			// pLmtorInfo->bIsEquipInit = TRUE;//设置初配结束标识

			return;
		}

		/// <summary>
		/// 事件识别，组织好描述信息，显示出来
		/// </summary>
		/// <param name="intTrapType"></param>
		/// <param name="lmtPdu"></param>
		/// <param name="strDesc"></param>
		/// <returns></returns>
		private bool ClassifyEvent(int intTrapType, CDTLmtbPdu lmtPdu, out string strDesc)
		{
			// 初始化
			strDesc = "";

			// 返回值
			var sb = new StringBuilder();
			string strValue;
			// 传输结果
			string strTrapResult;
			string strReValue;
			string strGeneralEventType;

			// 网元IP
			var strNodeBIp = lmtPdu.m_SourceIp;

			switch(intTrapType)
			{
				case 22: //managementRequestObjects
						 //eventManagementRequestTraps
						 //managementRequestNEID
					
					// 网元标识
					lmtPdu.GetValueByMibName(strNodeBIp, "equipStartupNotiNEID", out strValue);
					if(!string.IsNullOrEmpty(strValue))
					{
						sb.Append("网元标识:").Append(strValue).Append(";");
					}

					// 软件版本
					lmtPdu.GetValueByMibName(strNodeBIp, "equipStartupNotiNEVersion", out strValue);
					if (!string.IsNullOrEmpty(strValue))
					{
						sb.Append("软件版本:").Append(strValue).Append(";");
					}

					// MIB版本
					lmtPdu.GetValueByMibName(strNodeBIp, "equipStartupNotiMIBVersion", out strValue);
					if (!string.IsNullOrEmpty(strValue))
					{
						sb.Append("MIB版本:").Append(strValue).Append(";");
					}

					// 产生时间
					lmtPdu.GetValueByMibName(strNodeBIp, "equipStartupNotiTime", out strValue);
					if (!string.IsNullOrEmpty(strValue))
					{
						sb.Append("产生时间:").Append(strValue).Append(";");
					}

					break;

				case 16:    // eventFTPResultTraps
				case 23:    // eventFTPResultTraps
							// fileTransTrapName

					// 文件
					lmtPdu.GetValueByMibName(strNodeBIp, "fileTransNotiFileName", out strValue);
					sb.Append("文件").Append(" ").Append(strValue).Append(" ");

					// fileTransTrapFlag
					lmtPdu.GetValueByMibName(strNodeBIp, "fileTransNotiIndicator", out strValue);
					if(!string.IsNullOrEmpty(strValue))
					{
						// 根据Mib值获取其描述
						
						if (false == CommSnmpFuns.TranslateMibValue(strNodeBIp, "fileTransNotiIndicator", strValue, out strReValue))
						{
							return false;
						}
						sb.Append(strReValue).Append(" ");
					}

					// fileTransTrapResult
					lmtPdu.GetValueByMibName(strNodeBIp, "fileTransNotiResult", out strValue);
					strTrapResult = strValue;
					if (!string.IsNullOrEmpty(strValue))
					{
						// 根据Mib值获取其描述
						if (false == CommSnmpFuns.TranslateMibValue(strNodeBIp, "fileTransNotiResult", strValue, out strReValue))
						{
							return false;
						}
						sb.Append(strReValue).Append(" ");
					}

					if ("3".Equals(strTrapResult))
					{
						lmtPdu.GetValueByMibName(strNodeBIp, "fileTransNotiErrorCode", out strValue);
						if (!string.IsNullOrEmpty(strValue))
						{
							if (false == CommSnmpFuns.TranslateMibValue(strNodeBIp, "fileTransNotiErrorCode", strValue, out strReValue))
							{
								return false;
							}
							sb.Append("错误类型为：").Append(strReValue).Append(" ");
						} 
					}

					break;

				case 20:    //eventGeneralEventTraps
							//old: 1:cellBlock|小区阻塞/2:cellUnblock|小区解阻塞/3:masterSlaveConsistency|主备一致性/4:ipoaBuild|IPOA建立结果/6:dynCfgActOn|管理站下载的动态配置文件激活结果/7:softActOnTime|软件定时激活启动/8:carrierCheck|载波状态上报/10:cellPerfStatistics|小区性能统计结果/11:cellR5PerfReport|小区性能统计上报/12:nbapNodeBReset|Nbap的nodeb复位/13:cellPerfDataClear|小区性能统计数据清零/16:masterSlaveSwap|主备用倒换结果/19:boardPowerOn|板卡启动/21:saalCreate|SAAL建立结果/22:saalDestroy|SAAL删除结果/23:pathCreate|Path建立结果/24:pathDestroy|Path删除结果/25:debugUpload|调试日志上传/26:dfgCreate|动态配置文件创建/27:antCfgParse|天线配置文件解析结果/28:gpsUpgrade|GPS升级结果/29:ifuNetworkPlanReq|IFU申请网络规划/30:programSyn|程序同步结果
							//new: 1:cellBlock|小区阻塞/2:cellUnblock|小区解阻塞/3:masterSlaveConsistency|主备一致性/4:ipoaBuild|IPOA建立结果/6:dynCfgActOn|管理站下载的动态配置文件激活结果/7:softActOnTime|软件定时激活启动/8:carrierCheck|载波状态上报/10:cellPerfStatistics|小区性能统计结果/11:cellR5PerfReport|小区性能统计上报/12:nbapNodeBReset|Nbap的nodeb复位/13:cellPerfDataClear|小区性能统计数据清零/16:masterSlaveSwap|主备用倒换结果/19:boardPowerOn|板卡启动/21:saalCreate|SAAL建立结果/22:saalDestroy|SAAL删除结果/23:pathCreate|Path建立结果/24:pathDestroy|Path删除结果/25:debugUpload|调试日志上传/26:dfgCreate|动态配置文件创建/27:antCfgParse|天线配置文件解析结果/28:gpsUpgrade|GPS升级结果/29:ifuNetworkPlanReq|IFU申请网络规划/30:programSyn|程序同步结果)；事件结果 (0:fail/1:success)；事件附加信息 ((1..255)字符串)/31:initCfgResult|初配结果/32:localCellSetup|本地小区建立结果/33:localCellDelete|本地小区删除结果/34:cellSetup|小区建立结果/35:cellDelete|小区删除结果
					//eventGeneralEventType
					lmtPdu.GetValueByMibName(strNodeBIp, "fileTransNotiErrorCode", out strValue);
					strGeneralEventType = strValue;
					if (!string.IsNullOrEmpty(strValue))
					{
						if (false == CommSnmpFuns.TranslateMibValue(strNodeBIp, "eventGeneralEventType", strValue, out strReValue))
						{
							return false;
						}
						sb.Append("事件类型:").Append(strReValue).Append(";");
					}

					// eventGeneralEventResult
					lmtPdu.GetValueByMibName(strNodeBIp, "eventGeneralEventResult", out strValue);
					if (!string.IsNullOrEmpty(strValue))
					{
						if (false == CommSnmpFuns.TranslateMibValue(strNodeBIp, "eventGeneralEventResult", strValue, out strReValue))
						{
							return false;
						}
						sb.Append("结果:").Append(strReValue).Append("; ");
					}

					// eventGeneralEventSource字段解析
					// 先认为该字段为OID，并且后两位为机框号和插槽号
					// 如果按OID处理失败，就直接显示该字段内容
					lmtPdu.GetValueByMibName(strNodeBIp, "eventGeneralEventSource", out strValue);
					if (!string.IsNullOrEmpty(strValue))
					{
						// 机框
						var strFrameNo = "";
						// 槽位
						var strSlotNo = "";
						var strValueTmp = strValue;

						sb.Append("事件产生源:");
						// 截取机框和槽位号
						var intDotIndex = strValueTmp.LastIndexOf('.');
						if (intDotIndex >= 0 & intDotIndex < strValueTmp.Length-1)
						{
							// 槽位号
							strSlotNo = strValueTmp.Substring(intDotIndex + 1, (strValueTmp.Length - intDotIndex));
							strValueTmp = strValueTmp.Substring(0, intDotIndex);
							
							// 机框号
							intDotIndex = strValueTmp.LastIndexOf('.');
							if (intDotIndex >= 0 & intDotIndex < strValueTmp.Length - 1)
							{
								strFrameNo = strValueTmp.Substring(intDotIndex + 1, (strValueTmp.Length - intDotIndex));
								strValueTmp = strValueTmp.Substring(0, intDotIndex);
							}
						}

						// 获取Mib节点信息
						var reDataByOid = new MibLeaf();
						string strError;
						if(false == Database.GetInstance().GetMibDataByOid(strValueTmp, out reDataByOid, strNodeBIp, out strError))
						{
							Log.Error($"获取MIb节点信息错误，oid={strValueTmp}");
							return false;
						}
						if (reDataByOid != null && !string.IsNullOrEmpty(strFrameNo) && !string.IsNullOrEmpty(strSlotNo))
						{
							sb.Append("机框").Append(strFrameNo).Append(",");
							sb.Append("槽位").Append(strSlotNo).Append(";");
						}
						else
						{
							sb.Append(strValue).Append(";");
						}
						sb.Append("  ");

					}

					// eventGeneralEventAdditionInfo字段，事件附加信息
					lmtPdu.GetValueByMibName(strNodeBIp, "eventGeneralEventAdditionInfo", out strValue);
					if (!string.IsNullOrEmpty(strValue))
					{
						sb.Append("附加信息:");

						// 事件编号修改：100:carrierCheck|载波状态上报/101:cellPerfStatistics|小区性能统计结果
						// 102:cellR5PerfReport|小区性能统计上报/103:nbapNodeBReset|Nbap的nodeb复位/104:cellPerfDataClear|小区性能统计数据清零
						switch (Convert.ToInt32(strGeneralEventType))
						{
							case 8:
							//动态频点设置
							case 35:
								//通用事件中上报TPA电流的事件类型
								//return TRUE;
								break;
							case 102:
								//HSDPA小区性能统计上报
								strValue = " (" + GetCellPerfReportInfo(strValue) + ")";
								break;
							case 104:
								//vb
								//小区性能统计数据清零【改】
								strValue = " (" + GetCellPerfDataClearInfo(strValue) + ")";
								break;
							default:
								//其它的直接显示
								break;
						}

						sb.Append(strValue).Append("; ");
					}

					// eventGeneralEventOccurTime字段
					lmtPdu.GetValueByMibName(strNodeBIp, "eventGeneralEventOccurTime", out strValue);
					if (!string.IsNullOrEmpty(strValue))
					{
						sb.Append("产生时间:").Append(strValue).Append("; ");
					}

					break;
				case 25: // transactionResultObjects，事务
					ProcessTransResultEvent(lmtPdu, out strDesc);
					break;
				case 26: // 数据同步事件Trap绑定变量
					ProcessSyncFileEvent(lmtPdu, out strDesc);
					break;
				case 200: // 处理ANR专用事情
					ProcessANREvent(lmtPdu, out strDesc);
					break;
				case 201:  //处理MRO专用事情
					ProcessMROEvent(lmtPdu, out strDesc);
					break;
				case 202:  //处理FC专用事件
					ProcessFCEvent(lmtPdu, out strDesc);
					break;
				case 203:  // maintenceStateNotify工程状态通知事件处理
					ProcessMaintenceStateNotify(lmtPdu, out strDesc);
					break;
				case 204:  // nodeBlockStateNotify工程状态通知事件处理
					ProcessnodeBlockStateNotify(lmtPdu, out strDesc);
					break;
				default:
					Log.Error("ClassifyEvent方法中不能识别的PDU类型.");
					return false;
			}

			strDesc = sb.ToString();

			return true;
		}


		/// <summary>
		/// nodeBlockStateNotify工程状态通知事件处理
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="strDesc"></param>
		/// <returns></returns>
		private bool ProcessnodeBlockStateNotify(CDTLmtbPdu lmtPdu, out string strDesc)
		{
			strDesc = "";

			var strOidPrefix = SnmpToDatabase.GetMibPrefix();
			var strReValue = "";

			// 从第三个报文开始是内容
			for (var i = 2; i < lmtPdu.VbCount(); i++)
			{
				var lmtVb = lmtPdu.GetVbByIndexEx(i);
				if (lmtVb == null)
				{
					continue;
				}
				var strVbOid = lmtVb.Oid;
				if (string.IsNullOrEmpty(strVbOid))
				{
					continue;
				}

				// 根据Vb 中的OID 获取 MibOid
				var strMibOid = "";
				if (false == ConvertVbOidToMibOid(strOidPrefix, strVbOid, out strMibOid))
				{
					continue;
				}

				var mibLeaf = CommSnmpFuns.GetMibNodeInfoByOID(lmtPdu.m_SourceIp, strMibOid);
				if (null == mibLeaf)
				{
					continue;
				}

				var strValue = lmtVb.Value;
				var strAsnTyep = lmtVb.AsnType;
				if (!string.IsNullOrEmpty(strAsnTyep))
				{
					strAsnTyep = strAsnTyep.ToUpper();
					// LONG类型值转换显示
					if ("LONG".Equals(strAsnTyep) && !string.IsNullOrEmpty(mibLeaf.managerValueRange))
					{
						CommSnmpFuns.TranslateMibValue(mibLeaf.managerValueRange, strValue, out strReValue);
					}
				}

				strDesc = $"{strDesc}{mibLeaf.childNameCh}: {strReValue}; ";
			}

			return true;
		}

		/// <summary>
		/// maintenceStateNotify工程状态通知事件处理
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="strDesc"></param>
		/// <returns></returns>
		private bool ProcessMaintenceStateNotify(CDTLmtbPdu lmtPdu, out string strDesc)
		{
			strDesc = "";

			var strOidPrefix = SnmpToDatabase.GetMibPrefix();
			var strReValue = "";

			// 从第三个报文开始是内容
			for (var i = 2; i < lmtPdu.VbCount(); i++)
			{
				var lmtVb = lmtPdu.GetVbByIndexEx(i);
				var strVbOid = lmtVb?.Oid;
				if (string.IsNullOrEmpty(strVbOid))
				{
					continue;
				}

				// 根据Vb 中的OID 获取 MibOid
				var strMibOid = "";
				if (false == ConvertVbOidToMibOid(strOidPrefix, strVbOid, out strMibOid))
				{
					continue;
				}

				var mibLeaf = CommSnmpFuns.GetMibNodeInfoByOID(lmtPdu.m_SourceIp, strMibOid);
				if (null == mibLeaf)
				{
					continue;
				}

				var strValue = lmtVb.Value;
				var strAsnTyep = lmtVb.AsnType;
				if (!string.IsNullOrEmpty(strAsnTyep))
				{
					strAsnTyep = strAsnTyep.ToUpper();
					// LONG类型值转换显示
					if ("LONG".Equals(strAsnTyep) && !string.IsNullOrEmpty(mibLeaf.managerValueRange))
					{
						CommSnmpFuns.TranslateMibValue(mibLeaf.managerValueRange, strValue, out strReValue);
					}
				}

				strDesc = $"{strDesc}{mibLeaf.childNameCh}: {strReValue}; ";
			}

			return true;
		}

		/// <summary>
		/// 根据VbOid获取MibOid
		/// 说明： 根据 VbOid 1.3.6.1.4.1.5105.100.2.1.2.2.2.0 获取到 MibOid 2.1.2.2.2, strOIDPrefix = 1.3.6.1.4.1.5105.100
		/// </summary>
		/// <param name="strOidPrefix"></param>
		/// <param name="strVbOid"></param>
		/// <param name="strMibOid"></param>
		/// <returns></returns>
		private bool ConvertVbOidToMibOid(string strOidPrefix, string strVbOid, out string strMibOid)
		{
			strMibOid = "";
			if (string.IsNullOrEmpty(strOidPrefix) || string.IsNullOrEmpty(strVbOid))
			{
				Log.Error($"参数strOidPrefix:{strOidPrefix}或strVbOid{strVbOid}为空!");
				return false;
			}

			if (strVbOid.IndexOf(strOidPrefix, StringComparison.Ordinal) < 0)
			{
				Log.Error($"Oid前缀与Oid不匹配!strOidPrefix:{strOidPrefix},strVbOid{strVbOid}");
				return false;
			}

			// 去掉前缀 (2.1.2.2.2.0)
			strMibOid = strVbOid.Replace(strOidPrefix, "");

			// 去掉最后一位索引
			var index = strMibOid.LastIndexOf('.');
			if (index < 0)
			{
				return false;
			}
			strMibOid = strMibOid.Substring(0, index);

			return true;
		}

		/// <summary>
		/// 处理FC专用事项
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="strDesc"></param>
		/// <returns></returns>
		private bool ProcessFCEvent(CDTLmtbPdu lmtPdu, out string strDesc)
		{
			strDesc = "";
			var strValue = "";
			var strReValue = "";
			var strNeIp = lmtPdu.m_SourceIp;

			// //网元标识
			lmtPdu.GetValueByMibName(strNeIp, "fcNotiNEID", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = $"{CommString.IDS_NEID}{strValue};";
			}

			// 网元类型
			lmtPdu.GetValueByMibName(strNeIp, "fcNotiNEType", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				CommSnmpFuns.TranslateMibValue(strNeIp, "fcNotiNEType", strValue, out strReValue);
				strDesc = $"{strDesc}{CommString.IDS_NETYPE}{strReValue};";
			}

			var strFcNotiType = "";
			// FC事情类型
			lmtPdu.GetValueByMibName(strNeIp, "fcNotiType", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strFcNotiType = strValue;
				CommSnmpFuns.TranslateMibValue(strNeIp, "fcNotiType", strValue, out strReValue);
				strDesc = $"{strDesc}{CommString.DIS_FC_EVENT_TYPE}{strReValue};";
			}

			// 小区索引
			lmtPdu.GetValueByMibName(strNeIp, "fcNotiCellId", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = $"{strDesc}{CommString.DIS_MRO_NOTI_CELL_ID}{strValue};";
			}

			// 事件产生时间
			lmtPdu.GetValueByMibName(strNeIp, "fcNotiTime", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = $"{strDesc}{CommString.DIS_EVENT_TIME}{strValue};";
			}

			// 建议mib取值列表
			if (lmtPdu.VbCount() >= 11)
			{
				strDesc = $"{strDesc}相关mib节点建议取值如下:\r\n";
				for (var i = 10; i < lmtPdu.VbCount(); i++)
				{
					var lmtVb = new CDTLmtbVb();
					lmtPdu.GetVbByIndex(i, ref lmtVb);
					var strOid = lmtVb.Oid;
					var strVbValue = lmtVb.Value;
					string strName;
					string strValueDesc;
					string strUnitName;
					// 根据OID取出相关信息
					if (false == CommSnmpFuns.GetInfoByOID(
						strNeIp, strOid, strVbValue, out strName, out strValueDesc, out strUnitName))
					{
						Log.Error($"GetInfoByOID()方法返回错误，oid:{strOid}");
						return false;
					}
					strDesc = $"{strDesc}Mib节点:{strName},建议取值:{strVbValue} {strUnitName};\r\n";
				}
				// 删除结尾最后一个回车
				char[] strTrim = { '\r', '\n' };
				strDesc = strDesc.TrimEnd(strTrim);
			}

			return true;
		}

		/// <summary>
		/// 处理MRO专用事项
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="strDesc"></param>
		/// <returns></returns>
		private bool ProcessMROEvent(CDTLmtbPdu lmtPdu, out string strDesc)
		{
			strDesc = "";
			var strValue = "";
			var strReValue = "";
			var strNeIp = lmtPdu.m_SourceIp;

			// transactionResultTrapNEID
			lmtPdu.GetValueByMibName(strNeIp, "mroNotiNEID", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = $"{CommString.IDS_NEID}{strValue};";
			}

			// 网元类型
			lmtPdu.GetValueByMibName(strNeIp, "mroNotiNEType", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				CommSnmpFuns.TranslateMibValue(strNeIp, "mroNotiNEType", strValue, out strReValue);
				strDesc = $"{strDesc}{CommString.IDS_NETYPE}{strReValue};";
			}

			var strMroNotiType = "";
			// MRO事情类型
			lmtPdu.GetValueByMibName(strNeIp, "mroNotiType", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strMroNotiType = strValue;
				CommSnmpFuns.TranslateMibValue(strNeIp, "mroNotiType", strValue, out strReValue);
				strDesc = $"{strDesc}{CommString.DIS_MRO_EVENT_TYPE}{strReValue};";
			}

			// 本小区索引
			lmtPdu.GetValueByMibName(strNeIp, "mroNotiCellId", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = $"{strDesc}{CommString.DIS_MRO_NOTI_CELL_ID}{strValue};";
			}

			// 事件产生时间
			lmtPdu.GetValueByMibName(strNeIp, "mroNotiTime", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = $"{strDesc}{CommString.DIS_EVENT_TIME}{strValue};";
			}

			// 建议mib取值列表
			if (lmtPdu.VbCount() >= 10)
			{
				strDesc = $"{strDesc}相关mib节点建议取值如下:\r\n";
				for (var i = 10; i < lmtPdu.VbCount(); i++)
				{
					var lmtVb = new CDTLmtbVb();
					lmtPdu.GetVbByIndex(i, ref lmtVb);
					var strOid = lmtVb.Oid;
					var strVbValue = lmtVb.Value;
					string strName;
					string strValueDesc;
					string strUnitName;
					// 根据OID取出相关信息
					if (false == CommSnmpFuns.GetInfoByOID(
						strNeIp, strOid, strVbValue, out strName, out strValueDesc, out strUnitName))
					{
						Log.Error($"GetInfoByOID()方法返回错误，oid:{strOid}");
						return false;
					}
					strDesc = $"{strDesc}Mib节点:{strName},建议取值:{strVbValue} {strUnitName};\r\n";
				}
				// 删除结尾最后一个回车
				char[] strTrim = { '\r', '\n' };
				strDesc = strDesc.TrimEnd(strTrim);
			}

			return true;
		}

		/// <summary>
		/// ANR消息处理
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="strDesc"></param>
		/// <returns></returns>
		private bool ProcessANREvent(CDTLmtbPdu lmtPdu, out string strDesc)
		{
			strDesc = "";
			var strValue = "";
			var strReValue = "";
			var strNeIp = lmtPdu.m_SourceIp;

			// transactionResultTrapNEID
			lmtPdu.GetValueByMibName(strNeIp, "anrNotiNEID", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = $"{CommString.IDS_NEID}{strValue};";
			}
			// 网元类型
			lmtPdu.GetValueByMibName(strNeIp, "anrNotiNEType", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				CommSnmpFuns.TranslateMibValue(strNeIp, "anrNotiNEType", strValue, out strReValue);
				strDesc = $"{strDesc}{CommString.IDS_NETYPE}{strReValue};";
			}

			var strAnrNotiType = "";
			// ANR事情类型
			lmtPdu.GetValueByMibName(strNeIp, "anrNotiType", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strAnrNotiType = strValue;
				CommSnmpFuns.TranslateMibValue(strNeIp, "anrNotiType", strValue, out strReValue);
				strDesc = $"{strDesc}{CommString.IDS_ANR_EVENT_TYPE}{strReValue};";
			}

			// 本小区索引
			lmtPdu.GetValueByMibName(strNeIp, "anrNotiLcIdx", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = $"{strDesc}{CommString.IDS_ANR_NOTILCIDX}{strValue};";
			}

			// 邻区关系索引
			if ("1".Equals(strAnrNotiType))
			{
				lmtPdu.GetValueByMibName(strNeIp, "anrNotiAdjRelationIdx", out strValue);
				if (!string.IsNullOrEmpty(strValue))
				{
					strDesc = $"{strDesc}{CommString.IDS_ANR_NOTI_ADJ_RELATION_IDX}{strValue};";
				}
			}

			// 邻区网络类型
			var strAdjCellNetType = "";
			lmtPdu.GetValueByMibName(strNeIp, "anrNotiAdjCellNetType", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strAdjCellNetType = strValue;
				CommSnmpFuns.TranslateMibValue(strNeIp, "anrNotiAdjCellNetType", strValue, out strReValue);
				strDesc = $"{strDesc}{CommString.IDS_ANR_NOTI_ADJ_CELL_NET_TYPE}{strReValue};";
			}

			// 邻区移动国家码
			lmtPdu.GetValueByMibName(strNeIp, "anrNotiAdjCellPlmnMcc", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = $"{strDesc}{CommString.IDS_ANR_NOTI_ADJ_CELL_PLMN_MCC}{strValue};";
			}

			// 邻区移动网络码
			lmtPdu.GetValueByMibName(strNeIp, "anrNotiAdjCellPlmnMnc", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				// MNC第三个数字为255时，不用显示
				strValue = strValue.Replace(",255}", "}");
				strDesc = $"{strDesc}{CommString.IDS_ANR_NOTI_ADJ_CELL_PLMN_MNC}{strValue};";
			}

			// 邻区索引
			lmtPdu.GetValueByMibName(strNeIp, "anrNotiAdjCellId", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				var strAdjCellIdInfos = CommFuns.GenerateAdjCellIdInfo(strAdjCellNetType, strValue);
				strDesc = $"{strDesc}{CommString.IDS_ANR_NOTI_ADJ_CELL_ID}{strAdjCellIdInfos};";
			}

			if ("2".Equals(strAnrNotiType))
			{
				lmtPdu.GetValueByMibName(strNeIp, "anrNotiResult", out strValue);
				if (!string.IsNullOrEmpty(strValue))
				{
					CommSnmpFuns.TranslateMibValue(strNeIp, "anrNotiResult", strValue, out strReValue);
					strDesc = $"{strDesc}{CommString.DIS_EVNET_RESULT}{strReValue};";
				}

				// 只有失败的时候才显示失败原因
				if (!"0".Equals(strValue))
				{
					lmtPdu.GetValueByMibName(strNeIp, "anrNotiFailReason", out strValue);
					if (!string.IsNullOrEmpty(strValue))
					{
						CommSnmpFuns.TranslateMibValue(strNeIp, "anrNotiFailReason", strValue, out strReValue);
						strDesc = $"{strDesc}{CommString.DIS_EVENT_FAIL_RSULT}{strReValue};";
					}
				}
			}

			// 事件产生时间
			lmtPdu.GetValueByMibName(strNeIp, "anrNotiTime", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = $"{strDesc}{CommString.DIS_EVENT_TIME}{strValue};";
			}

			return true;
		}

		/// <summary>
		/// 数据同步事件Trap绑定变量
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="strDesc"></param>
		/// <returns></returns>
		private bool ProcessSyncFileEvent(CDTLmtbPdu lmtPdu, out string strDesc)
		{
			strDesc = "";
			string strValue;
			string strReValue;
			var strNeIp = lmtPdu.m_SourceIp;

			// 网元标示
			lmtPdu.GetValueByMibName(strNeIp, "eventSynchronizationNEID", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				CommSnmpFuns.TranslateMibValue(strNeIp, "eventSynchronizationNEID", strValue, out strReValue);
				strDesc = string.Format("{0}{1}{3};", strDesc, CommString.IDS_NEID, strReValue);
			}

			// 网元ID
			lmtPdu.GetValueByMibName(strNeIp, "eventSynchronizationNEType", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = string.Format("{0}{1}{3};", strDesc, CommString.IDS_NETYPE, strValue);
			}

			// 需要同步的文件类型
			string strFileType;
			lmtPdu.GetValueByMibName(strNeIp, "eventSynchronizationType", out strFileType);
			if (!string.IsNullOrEmpty(strFileType))
			{
				strValue = strFileType;
				CommSnmpFuns.TranslateMibValue(strNeIp, "eventSynchronizationType", strValue, out strReValue);
				strDesc = string.Format("{0}{1}{3};", strDesc, CommString.IDS_SYNCFILETYPE, strReValue);
			}

			// 附加信息
			lmtPdu.GetValueByMibName(strNeIp, "eventSynchronizationAdditionInfo", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = string.Format("{0}{1}{3};", strDesc, CommString.IDS_ADDITIONALINFO, strValue);
			}

			// 事件产生时间
			lmtPdu.GetValueByMibName(strNeIp, "eventSynchronizationOccurTime", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				strDesc = string.Format("{0}{1}{3};", strDesc, CommString.IDS_OCCURTIME, strValue); 
			}

			if ("0".Equals(strFileType))
			{
				Log.Error("无效的文件类型,不会发起任何文件同步操作");
				return false;
			}

			var fileTransType = new int[32];
			fileTransType[0] = (int)Transfiletype5216.TRANSFILE_activeAlarmFile;
			fileTransType[1] = (int)Transfiletype5216.TRANSFILE_dataConsistency;

			// 转换为数字
			 var fileTypeBitsValue = Convert.ToUInt32(strFileType);
			for (var i = 0; i < 32; i++)
			{
				var fileType = 0;
				var strFilePath = FilePathHelper.GetDataPath();
				var value = 1 << i;
				if ((fileTypeBitsValue & value) != 0)
				{
					fileType = fileTransType[i];
					if (fileType == (int)Transfiletype5216.TRANSFILE_dataConsistency)
					{
						strFilePath = FilePathHelper.GetConsistencyFilePath();
					}

					// 下发文件同步任务
					long taskId = 0;
					long reqId = 0;
					var cft = FileTransTaskMgr.FormatTransInfo(strFilePath
						, "", (Transfiletype5216)fileType, TRANSDIRECTION.TRANS_UPLOAD);
					if (SENDFILETASKRES.TRANSFILE_TASK_SUCCEED != 
						FileTransTaskMgr.SendTransFileTask(strNeIp, cft, ref taskId, ref reqId))
					{
						Log.Error($"下发上传同步文件传输任务失败,文件类型:{fileType},网元IP为:{strNeIp}");
					}
					else
					{
						Log.Info($"下发上传同步文件传输任务成功！--网元IP为:{strNeIp}");
					}
				}
			}

			return true;
		}

		/// <summary>
		/// 文件传输结果处理
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="strDesc"></param>
		/// <returns></returns>
		private bool ProcessTransResultEvent(CDTLmtbPdu lmtPdu, out string strDesc)
		{
			strDesc = "";
			string strValue;
			string strReValue;
			var sbReVal = new StringBuilder();

			// 网元IP
			var strIpAddr = lmtPdu.m_SourceIp;

			// transactionResultTrapNEID
			lmtPdu.GetValueByMibName(strIpAddr, "transactionResultNotiNEID", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				sbReVal.Append("网元标识:").Append(strValue).Append("; ");
			}

			//网元类型
			lmtPdu.GetValueByMibName(strIpAddr, "transactionResultNotiNEType", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				CommSnmpFuns.TranslateMibValue(strIpAddr, "transactionResultNotiNEType", strValue, out strReValue);
				sbReVal.Append("网元类型:").Append(strReValue).Append("; ");
			}


			//transactionResultTrapResult
			var bTransSuccess = true;
			lmtPdu.GetValueByMibName(strIpAddr, "transactionResultNotiResult", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				string strTmpVal;
				CommSnmpFuns.TranslateMibValue(strIpAddr, "transactionResultNotiResult", strValue, out strTmpVal);
				if ("1".Equals(strValue)) //失败
				{
					bTransSuccess = false;
					string strErrorValue;
					var sbErrorMsg = new StringBuilder();
					//第一个出错表的OID标示
					lmtPdu.GetValueByMibName(strIpAddr, "transactionResultNotiFirstErrVariableOID", out strErrorValue);
					if (!string.IsNullOrEmpty(strErrorValue))
					{
						sbErrorMsg.Append("第一个出错表的OID标示:").Append(strErrorValue).Append(";");
					}

					//出错原因码
					lmtPdu.GetValueByMibName(strIpAddr, "transactionResultNotiErrCode", out strErrorValue);
					if (!string.IsNullOrEmpty(strErrorValue))
					{
						sbErrorMsg.Append("第一个出错表的出错原因码:").Append(strErrorValue).Append(";");
					}

					sbReVal.Append("事务结果:").Append(strTmpVal).Append(";").Append(sbErrorMsg).Append("; ");

				}
				else
				{
					sbReVal.Append("事务结果:").Append(strTmpVal).Append("; ");
				}
			}

			// transactionResultNotiAddition,事务附加信息
			lmtPdu.GetValueByMibName(strIpAddr, "transactionResultNotiAddition", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				sbReVal.Append("事务附加信息:").Append(strValue).Append("; ");
			}

			//transactionResultTrapTime
			lmtPdu.GetValueByMibName(strIpAddr, "transactionResultNotiTime", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				sbReVal.Append("产生时间:").Append(strValue).Append("; ");
			}

			//如果事务失败，则发起一次数据同步过程
			if (!bTransSuccess)
			{
				long taskId = 0;
				long requestId = 0;

				//收到初配上报事件, 发起一致性文件上传
				var strDataConsisFolderPath = AppPathUtiliy.Singleton.GetDataConsistencyFolderPath();
				var transFileObj = FileTransTaskMgr.FormatTransInfo(
																strDataConsisFolderPath
																, ""
																, Transfiletype5216.TRANSFILE_dataConsistency
																, TRANSDIRECTION.TRANS_UPLOAD);
				if (SENDFILETASKRES.TRANSFILE_TASK_FAILED == FileTransTaskMgr.SendTransFileTask(
					strIpAddr, transFileObj, ref taskId, ref requestId))
				{
					Log.Error($"下发上传数据一致性文件传输任务失败，数据一致性文件目录{strDataConsisFolderPath}，网元IP为{strIpAddr}");

				}
				else
				{
					Log.Info("下发上传数据一致性文件传输任务成功！--网元IP为{0}", strIpAddr);
				}

			}

			return true;
		}

		/// <summary>
		/// 获得事件附加信息(小区性能统计数据清零)
		/// </summary>
		/// <param name="strAddiInfo"></param>
		/// <returns></returns>
		private string GetCellPerfDataClearInfo(string strAddiInfo)
		{
			// 返回值
			var sbReVal = new StringBuilder();
			string strTmp;
			int intTmp;
			// 目前已经解析的数据
			int iReadLength;

			// ET 年
			strTmp = strAddiInfo.Substring(0, 4);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append("本次性能数据清零的时间点：").Append(intTmp);   //hex
			iReadLength = 4;

			//ET 月
			strTmp = strAddiInfo.Substring(iReadLength, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append("-").Append(string.Format("0:D2",intTmp));   //补齐两位，空位补零
			iReadLength = iReadLength + 2;//=6

			//ET 日
			strTmp = strAddiInfo.Substring(iReadLength, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append("-").Append(string.Format("0:D2", intTmp));   //补齐两位，空位补零
			iReadLength = iReadLength + 2;//=8

			//ET 时
			strTmp = strAddiInfo.Substring(iReadLength, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append(" ").Append(string.Format("0:D2", intTmp));   //补齐两位，空位补零
			iReadLength = iReadLength + 2;//=10

			//ET 分
			strTmp = strAddiInfo.Substring(iReadLength, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append(":").Append(string.Format("0:D2", intTmp));   //补齐两位，空位补零
			iReadLength = iReadLength + 2;//12

			//ET 秒
			strTmp = strAddiInfo.Substring(iReadLength, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append(":").Append(string.Format("0:D2", intTmp)).Append("; ");   //补齐两位，空位补零
			iReadLength = iReadLength + 2;//=14

			//LCCID 本地小区
			strTmp = strAddiInfo.Substring(iReadLength, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append("本地小区编号：").Append(intTmp).Append("; ");  
			iReadLength = iReadLength + 2;//=16

			//LCCID 小区编号
			strTmp = strAddiInfo.Substring(iReadLength,4);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append("小区编号：").Append(intTmp).Append("; ");
			iReadLength = iReadLength + 4;//=20

			//循环读取所有对象类型的信息
			while(strAddiInfo.Length > iReadLength)
			{
				strTmp = strAddiInfo.Substring(iReadLength, 2);
				intTmp = Convert.ToInt32(strTmp);

				switch (intTmp)
				{
					case 0: //小区级
						sbReVal.Append("对象类型：小区级。");
						iReadLength = iReadLength + 2;

						break;
					case 1: // 频点级
						sbReVal.Append("对象类型：频点级，");
						iReadLength = iReadLength + 2;

						strTmp = strAddiInfo.Substring(iReadLength, 4);
						intTmp = Convert.ToInt32(strTmp, 16);
						sbReVal.Append("频点值：").Append(intTmp).Append("。");
						iReadLength = iReadLength + 4;

						break;
					default:
						sbReVal.Append("对象类型：未知！");
						iReadLength = strAddiInfo.Length;
						break;
				}

			} //end while

			return sbReVal.ToString();
		}

		/// <summary>
		/// 获得事件附加信息(HSDPA小区性能统计上报)
		/// </summary>
		/// <param name="strAddiInfo"></param>
		/// <returns></returns>
		private string GetCellPerfReportInfo(string strAddiInfo)
		{
			// 返回值
			var sbReVal = new StringBuilder();

			// 临时变量
			string strTmp;
			int intTmp;

			// TI
			strTmp = strAddiInfo.Substring(0, 4);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append("本次上报收集的性能数据时间间隔:").Append(intTmp).Append("秒").Append(";");

			// ET 年
			strTmp = strAddiInfo.Substring(4, 4);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append("收集性能数据结束时间：").Append(intTmp);

			// ET 月
			strTmp = strAddiInfo.Substring(8, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append("-").Append(string.Format("0:D2", intTmp));// 补齐两位，空位补零

			// ET 日
			strTmp = strAddiInfo.Substring(10, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append("-").Append(string.Format("0:D2", intTmp));// 补齐两位，空位补零

			// ET 时
			strTmp = strAddiInfo.Substring(12, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append(" ").Append(string.Format("0:D2", intTmp)); // 补齐两位，空位补零

			// ET 分
			strTmp = strAddiInfo.Substring(14, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append(":").Append(string.Format("0:D2", intTmp)); // 补齐两位，空位补零

			// ET 秒
			strTmp = strAddiInfo.Substring(16, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append(":").Append(string.Format("0:D2", intTmp)); // 补齐两位，空位补零

			// LCCID 本地小区
			strTmp = strAddiInfo.Substring(18, 2);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append("本地小区编号：").Append(intTmp).Append(";");

			// LCCID 小区编号
			strTmp = strAddiInfo.Substring(20, 4);
			intTmp = Convert.ToInt32(strTmp, 16);
			sbReVal.Append("小区编号：").Append(intTmp).Append(".");


			int iFrequenceNum;
			// 计数器数
			int iCounterNum;
			int iFqLoop;
			//循环读取所有对象类型的信息
			var iReadLength = 24;//目前已经解析的数据
			while(strAddiInfo.Length > iReadLength)
			{
				strTmp = strAddiInfo.Substring(iReadLength + 2, 2); // CNum 高八位为频点个数 26
				intTmp = Convert.ToInt32(strTmp, 16); // 频点个数
				iFrequenceNum = intTmp;
				sbReVal.Append("对象类型：频点级，有").Append(intTmp).Append("个频点，");

				strTmp = strAddiInfo.Substring(iReadLength + 2 + 2, 2);// 'CNum 低八位为每个频点的计数器个数
				intTmp = Convert.ToInt32(strTmp, 16);
				iCounterNum = intTmp; // 计数器数
				sbReVal.Append("每个频点有").Append(intTmp).Append("个计数器，");
				iFqLoop = iFrequenceNum;

				sbReVal.Append("各个频点及其对应的计数器的值分别为：");
				for(var i = 0; i < iFqLoop; i++)
				{
					strTmp = strAddiInfo.Substring(iReadLength + 4 + 2 + (i * (4 + (10 * iCounterNum))), 4);// 'Ci 30
					intTmp = Convert.ToInt32(strTmp, 16);
					sbReVal.Append("频点:");

					for(var j = 0; j < iCounterNum; j++)
					{
						sbReVal.Append("计数器");
						strTmp = strAddiInfo.Substring(iReadLength + 4 + 4 + 2 + (j * 10) + (i * (4 + (10 * iCounterNum))), 2);// 'Ci 34
						intTmp = Convert.ToInt32(strTmp, 16);//计数器编号

						string strLabel;
						switch(intTmp)
						{
							case 0:
								strLabel = "吞吐量";
								break;
							case 1:
								strLabel = "用户平均吞吐量";
								break;
							case 2:
								strLabel = "收到的ACK数";
								break;
							case 3:
								strLabel = "收到的NACK数";
								break;
							case 4:
								strLabel = "丢弃的MAC帧数";
								break;
							case 5:
								strLabel = "首次发送后的收到ACK个数";
								break;
							case 6:
								strLabel = "首次发送后的收到NACK个数";
								break;
							case 7:
								strLabel = "证实的用户数据量";
								break;
							case 8:
								strLabel = "在缓存中具有用户数据的TTI数";
								break;
							case 9:
								strLabel = "每个TTI在缓存中具有数据的队列数之和";
								break;
							case 10:
								strLabel = "冲突的SYNC_UL数";
								break;
							case 11:
								strLabel = "丢弃的SYNC_UL数";
								break;
							case 12:
								strLabel = "收到的FPACH数";
								break;
							case 13:
								strLabel = "hsdpa占用的Bru数";
								break;
							case 14:
								strLabel = "hsupa占用的Bru数";
								break;
							case 15:
								strLabel = "确认的MAC-e包比特数";
								break;
							case 16:
								strLabel = "接收到的MAC-e层PDU个数";
								break;
							case 17:
								strLabel = "接收到经过确认的MAC-e层PDU个数";
								break;
							default :
								strLabel = "未知计数意义";
								break;
							
						} 

						sbReVal.Append(intTmp).Append("(").Append(strLabel).Append(")");

						strTmp = strAddiInfo.Substring(iReadLength + 2 + 4 + 4 + 2 + (j * 10) + (i * (4 + (10 * iCounterNum))), 8);// 'Ci 36 计数器取值
						intTmp = Convert.ToInt32(strTmp, 16);

						sbReVal.Append("值为").Append(intTmp);
						if (i != (iFqLoop - 1))
						{
							sbReVal.Append(";");
						}
						else
						{
							sbReVal.Append(j != (iCounterNum - 1) ? ";" : ".");
						}
					} // end for
				}// end for
				iReadLength = iReadLength + 2 + 4 + (iFrequenceNum * (4 + (10 * iCounterNum)));

			} // end while


			return sbReVal.ToString();
		}

		/// <summary>
		/// 检查是否为同一个告警类型的Trap
		/// </summary>
		/// <param name="strIp"></param>
		/// <param name="requestId"></param>
		/// <returns></returns>
		private bool InterceptRepeatedTrap4Alarm(string strIp, long requestId)
		{
			List<long> requestIdList;
			m_ipToRequestIdsDicForAlarm.TryGetValue(strIp, out requestIdList);
			if (requestIdList == null)
			{
				requestIdList = new List<long>();
				m_ipToRequestIdsDicForAlarm.Add(strIp, requestIdList);
			}

			if (requestIdList.Contains(requestId)) // 存在
			{
				return true;
			}

			// 只缓存4个id，多于4个删除
			if (requestIdList.Count > 4)
			{
				requestIdList.RemoveAt(0);
			}

			requestIdList.Add(requestId);

			return false;
		}

		/// <summary>
		/// 检查是否为同一个配置变更类型的Trap
		/// </summary>
		/// <param name="strIp"></param>
		/// <param name="requestId"></param>
		/// <returns></returns>
		private bool InterceptRepeatedTrap4ConfigChg(string strIp, long requestId)
		{
			List<long> requestIdList;
			m_ipToRequestIdsDicForConfigChg.TryGetValue(strIp, out requestIdList);
			if (requestIdList == null)
			{
				requestIdList = new List<long>();
				m_ipToRequestIdsDicForConfigChg.Add(strIp, requestIdList);
			}

			if (requestIdList.Contains(requestId)) // 存在
			{
				return true;
			}

			// 只缓存4个id，多于4个删除
			if (requestIdList.Count > 4)
			{
				requestIdList.RemoveAt(0);
			}

			requestIdList.Add(requestId);

			return false;
		}

		/// <summary>
		/// 检查是否为同一个事件类型的Trap
		/// </summary>
		/// <param name="strIp"></param>
		/// <param name="requestId"></param>
		/// <returns></returns>
		private bool InterceptRepeatedTrap4Event(string strIp, long requestId)
		{
			List<long> requestIdList;
			m_ipToRequestIdsDicForEvent.TryGetValue(strIp, out requestIdList);
			if (requestIdList == null)
			{
				requestIdList = new List<long>();
				m_ipToRequestIdsDicForEvent.Add(strIp, requestIdList);
			}

			if (requestIdList.Contains(requestId)) // 存在
			{
				return true;
			}

			// 只缓存4个id，多于4个删除
			if (requestIdList.Count > 4)
			{
				requestIdList.RemoveAt(0);
			}

			requestIdList.Add(requestId);

			return false;
		}


		/// <summary>
		/// 查验Trap OID是否有效，有效的情况下，返回LMT-eNB自己定义的Trap类型
		/// </summary>
		/// <param name="strIpAddr">网元IP</param>
		/// <param name="strTrapOid">Trap类型Oid</param>
		/// <param name="strOidPrefix">Oid前缀</param>
		/// <param name="intTrapType">Trap类型对应的数值</param>
		/// <returns></returns>
		public bool CheckTrapOIDValidity(string strIpAddr, string strTrapOid, string strOidPrefix, out int intTrapType)
		{
			intTrapType = 0;

			if (string.IsNullOrEmpty(strTrapOid))
			{
				Log.Error("参数strTrapOid为空！");
				return false;
			}
			// 去掉Mib前缀
			var strSubTrapOid = strTrapOid.Replace(strOidPrefix, "");

			if (string.IsNullOrEmpty(strSubTrapOid))
			{
				Log.Error("Trap Oid 截取错误！");
				return false;
			}

			// 根据oid获取Mib节点信息
			var mibLeaf = CommSnmpFuns.GetMibNodeInfoByOID(strIpAddr, strSubTrapOid);
			if (null == mibLeaf)
			{
				Log.Error($"无法获取Mib节点信息，Oid:{strSubTrapOid}");
				return false;
			}
			// Mib名称
			var strMibName = mibLeaf.childNameMib;

			// 查询是否有该类型的Trap
			var trapTypeInfo = Database.GetInstance().GetTrapInfo();
			var strTrapId = trapTypeInfo[strMibName]["TrapID"];
			if (string.IsNullOrEmpty(strTrapId))
			{
				Log.Error("数据库中没有找到相应的Trap类型信息!");
				return false;
			}

			intTrapType = Convert.ToInt32(strTrapId);

			return true;
		}

		/// <summary>
		/// 判断PDU包的合法性
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="strErrorMsg"></param>
		/// <returns></returns>
		public bool CheckPDUValidity(CDTLmtbPdu lmtPdu, out string strErrorMsg)
		{
			strErrorMsg = "";

			// 不能为空
			if (lmtPdu == null)
			{
				strErrorMsg = "接收到的包为空包";
				Log.Error("接收到的包为空包");
				return false;
			}

			// 判断包是否超时
			if (lmtPdu.reason == -5) // SNMP_CLASS_TIMEOUT
			{
				strErrorMsg = "响应超时";
				Log.Error("响应超时");
				return true;
			}

			// 判断包是否出错
			if (lmtPdu.m_LastErrorStatus != 0) // SNMP_ERROR_SUCCESS
			{
				strErrorMsg = $"ErrorStatus: {lmtPdu.m_LastErrorStatus} , ErrorIndex: {lmtPdu.m_LastErrorIndex} .";
				Log.Error($"接收到的包不正确--{strErrorMsg}");
				return true;
			}

			// 判断VB个数
			var vbCount = lmtPdu.VbCount();
			if (vbCount <= 0)
			{
				Log.Error($"接收到的包VB个数不正确:{vbCount}");
				return false;
			}

			//挨个检查VB的OID字段
			for (var i = 0; i < vbCount; i++)
			{
				var lmtVb = new CDTLmtbVb();
				lmtPdu.GetVbByIndex(i, ref lmtVb);
				if (string.IsNullOrEmpty(lmtVb.Oid))
				{
					Log.Error($"接收到的包VB的OID写法为空,不正确:{i}");
					return false;
				}
			}

			return true;
		}
	}
}
