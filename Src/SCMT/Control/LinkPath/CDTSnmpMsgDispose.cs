using CommonUtility;
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
using System.Threading.Tasks;

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

		public int FileTransMacro { get; private set; }

		public DTLinkPathMgr m_LinkMgr;

		/// <summary>
		/// 构造方法
		/// </summary>
		/// <param name="linkPathMgr"></param>
		public CDTSnmpMsgDispose(DTLinkPathMgr linkPathMgr)
		{
			// 订阅SNMP模块发来的消息
			 SubscribeHelper.AddSubscribe(CommString.MSG_KEY_CDTSnmpMsgDispose_OnResponse
				 , CallOnResponse);

			this.m_LinkMgr = linkPathMgr;

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
			string strTopic = msg.Topic;
			Log.Info(string.Format("msg.Topic = {0}", msg.Topic));

			CDTLmtbPdu lmtPdu = SerializeHelper.DeserializeWithBinary<CDTLmtbPdu>(msg.Data);
			this.OnResponse(lmtPdu);

		}
		#endregion

		/// <summary>
		/// 处理接收到的Trap消息
		/// </summary>
		/// <param name="lmtPdu"></param>
		public int OnTrap(Pdu pdu, IPEndPoint nodeIpPort)
		{
			// 将snmp pdu 转换为lmt pdu
			CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
			
			if (false == SnmpPdu2LmtPdu4Trap(pdu, nodeIpPort, ref lmtPdu, 0, true))
			{
				//Log.Error("从SNMP PDU 转换为LMT PDU错误！");
				return -1;
			}
			
			if (lmtPdu == null)
			{
				Log.Error("发来的Trap报文为空!");
				return -1;
			}

			// 获取网元IP
			string strNodeIp = lmtPdu.m_SourceIp;
//			Log.Info(string.Format("收到网元Trap, 网元ip:{0}", strNodeIp));

			// 验证包的合法性
			string strErrorMsg = "";
			if (false == CheckPDUValidity(lmtPdu, out strErrorMsg))
			{
				if (strErrorMsg != "")
				{
					// TODO:
					//CInfoBrowseMgr::GetInstance().PushStrInfo(ENB_OTHER_INFO_IMPORT, strErrorMsg, pLmtbPdu->get_SourceIp());
				}
				Log.Error("Trap Pdu验证失败!");
				return -1;
			}

			// 获得该网元所对应的MIB OID前缀
			string strOidPrefix = SnmpToDatabase.GetMibPrefix().Trim('.');
			if (string.IsNullOrEmpty(strOidPrefix))
			{
				Log.Error(string.Format("获取MIB前缀失败!"));
				return -1;
			}

			// 验证是否是认识的Trap类型
			CDTLmtbVb lmtVb = new CDTLmtbVb();
			lmtPdu.GetVbByIndex(1, ref lmtVb); // 第0个为时间戳，第1个为Trap包的OID
			int intTrapType = 0;
			if(false == CheckTrapOIDValidity(strNodeIp, lmtVb.Value, strOidPrefix, out intTrapType))
			{
//				Log.Error(string.Format("验证Trap类型失败,未知Trap类型,OID为{0}！"), lmtVb.Value);
				return -1;
			}


			// 按不同类型处理Trap
			switch (intTrapType)
			{
				case 2:
				case 24: // //alarmTraps
						 //告警处理  注意添加各告警字段和日志的值
						 //验证是否是同一个trap

					break;
				case 3:
				case 21: //eventConfigChgTraps


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
						Log.Info(string.Format("收到相同Trap, id:{0}", lmtPdu.m_requestId));
						return 0;
					}

					// 事件处理
					if (DealEventTrap(intTrapType, lmtPdu) == false)
					{
						Log.Error(string.Format("DealEventTrap事件处理失败:{0}", intTrapType));
						return -1;
					}

					break;
				default:
					// 未知类型上报
					Log.Error(string.Format("CDTSnmpMsgDispose::OnTrap方法中未知类型Trap上报:{0}", lmtVb.Value));
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
			Log.Info("收到网元Response，网元IP:{0}", lmtPdu.m_SourceIp);

			// 获取MIB前缀
			string prefix = SnmpToDatabase.GetMibPrefix().Trim('.');
			if (string.IsNullOrEmpty(prefix))
			{
				Log.Error(string.Format("获取MIB前缀失败!"));
				return -1;
			}

			// 验证RequestID的合法性，从数据库中读出信息并删除
			IDToTableStruct idToTb = new IDToTableStruct();
			idToTb.pduType = lmtPdu.getReqMsgType();
			idToTb.messageType = lmtPdu.m_requestId;
			if (lmtPdu.get_CmdName() != null)
			{
				idToTb.strCmdName = lmtPdu.get_CmdName();
			}
			else
			{
				idToTb.strCmdName = "";
			}

			// 验证包的合法性
			string strErrorMsg = "";
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
				else if (lmtPdu.m_bIsNeedPrint == true || lmtPdu.getReqMsgType() == (int)PduType.GetBulk)
				{
					DealFailResponsePDU(idToTb, lmtPdu);
				}
            }
			else if (lmtPdu.m_bIsNeedPrint == true)
			{
				string strTimeoutMsg = string.Format(CommString.IDS_STR_MSGDISPOSE_FMT1, lmtPdu.get_CmdName());
				ShowLogHelper.Show(strTimeoutMsg, lmtPdu.m_SourceIp, InfoTypeEnum.ENB_INFO);
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

			// 错误信息
			string strFailedReason = "";

			switch (lmtPdu.m_LastErrorStatus)
			{
				case SnmpConstants.ErrTooBig:
					strFailedReason = CommString.IDS_ERROR_TOO_BIG;//报文太大
					break;
				case SnmpConstants.ErrNoSuchName:
					strFailedReason = CommString.IDS_ERROR_NO_SUCH_NAME;//不存在
					break;
				case SnmpConstants.ErrBadValue:
					strFailedReason = CommString.IDS_ERROR_BAD_VALUE;//错误的值
					break;
				case SnmpConstants.ErrReadOnly:
					strFailedReason = CommString.IDS_ERROR_READ_ONLY;//只读
					break;
				case SnmpConstants.ErrGenError:
					strFailedReason = CommString.IDS_ERROR_GENERAL_VB_ERR;//产生其它错误
					break;
				case SnmpConstants.ErrNoAccess:
					strFailedReason = CommString.IDS_ERROR_NO_ACCESS;//不可访问
					break;
				case SnmpConstants.ErrWrongType:
					strFailedReason = CommString.IDS_ERROR_WRONG_TYPE;//设置类型与要求类型不一致
					break;
				case SnmpConstants.ErrWrongLength:
					strFailedReason = CommString.IDS_ERROR_WRONG_LENGTH;//设置长度与要求长度不一致
					break;
				case SnmpConstants.ErrWrongEncoding:
					strFailedReason = CommString.IDS_ERROR_WRONG_ENCODING;//ASN.1标签编码错误
					break;
				case SnmpConstants.ErrWrongValue:
					strFailedReason = CommString.IDS_ERROR_WRONG_VALUE;//不可赋为设置值
					break;
				case SnmpConstants.ErrNoCreation:
					strFailedReason = CommString.IDS_ERROR_NO_CREATION;//不存在
					break;
				case SnmpConstants.ErrInconsistentValue:
					strFailedReason = CommString.IDS_ERROR_INCONSIST_VAL;//不适合当前环境
					break;
				case SnmpConstants.ErrResourceUnavailable:
					strFailedReason = CommString.IDS_ERROR_RESOURCE_UNAVAIL;//赋值所需资源当前不可得到
					break;
				case SnmpConstants.ErrCommitFailed:
					strFailedReason = CommString.IDS_ERROR_COMITFAIL;//提交失败
					break;
				case SnmpConstants.ErrUndoFailed:
					strFailedReason = CommString.IDS_ERROR_UNDO_FAIL;//撤销失败
					break;
				case SnmpConstants.ErrAuthorizationError:
					strFailedReason = CommString.IDS_ERROR_AUTH_ERR;//授权错误
					break;
				case SnmpConstants.ErrNotWritable:
					strFailedReason = CommString.IDS_ERROR_NOT_WRITEABLE;//不可修改
					break;
				case SnmpConstants.ErrInconsistentName:
					strFailedReason = CommString.IDS_ERROR_INCONSIS_NAME;//不存在,且在当前环境下不能生成
					// TODO 新工具还要一下操作吗？
					// CDTDataSyncMgr::GetInstance()->RemoveCMDData(pAdoConn, pLmtbPdu);
					break;
				case CommNums.SNMP_ERROR_LOGIN:
					strFailedReason = CommString.IDS_ERROR_LOGIN;//LMT-eNB首次登录为错误报文
					break;
				case CommNums.SNMP_ERROR_ACTIONSHIELD:
					strFailedReason = CommString.IDS_ERROR_ACTIONSHIELD;//该操作被屏蔽
					break;
				case CommNums.SNMP_ERROR_ACTIONFAILD:
					strFailedReason = CommString.IDS_ERROR_ACTIONFAILD;//操作失败
					break;
				case CommNums.SNMP_ERROR_OMBUSY:
					strFailedReason = CommString.IDS_ERROR_OMBUSY;//由于OM忙不能操作实现
					break;
				case CommNums.SNMP_ERROR_OBSOLETE:
					strFailedReason = CommString.IDS_ERROR_OBSOLETE;//该操作目前已经废弃
					break;
				case CommNums.SNMP_ERROR_OVERTIME:
					strFailedReason = CommString.IDS_ERROR_OVERTIME;//OM出现超时错误
					break;
				default:
					// TODO:未实现
					/*CDTErrInfo errInfo;
					if (!GetErrDesc(pLmtbPdu->get_LastErrorStatus(), strIpAddr, errInfo))
					{
						//如果获取错误码描述失败，则将错误码赋值给描述
						CString strError;
						strError.Format("%d", pLmtbPdu->get_LastErrorStatus());
						errInfo.errDesc = strError;
					}
					*/
					strFailedReason = "XXXXXXXX";
					break;
			}

			// 获取名称、描述信息等信息
			CDTLmtbVb lmtVb = new CDTLmtbVb();
			string strName = "";
			string strDesc = "";
			string strUnitName = "";
			if (lmtPdu.m_LastErrorIndex > 0)
			{
				int idx = (int)lmtPdu.m_LastErrorIndex - 1;
				if (idx > 0 && idx < lmtPdu.VbCount())
				{
					lmtPdu.GetVbByIndex(idx, ref lmtVb);
					if (false == CommSnmpFuns.GetInfoByOID(lmtPdu.m_SourceIp, lmtVb.Oid, lmtVb.Value
						, out strName, out strDesc, out strUnitName))
					{
						Log.Error(string.Format("GetInfoByOID调用不成功,:OID = {0}", lmtVb.Oid));
					}
				}
				else
				{
					Log.Error(string.Format("idx 超出PDU Count数 idx={0} lmtPdu.VbCount()={1}"
						, idx, lmtPdu.VbCount()));
				}
			}

			// 后台执行的命令,不要显示在界面上了

			// 控制台显示信息
			string strShowMsg = "";
			string strStyle = "";
			string strTmp = "";

			if (strName != null)
			{
				// 变量 %s %s
				strTmp = string.Format("变量 {0} {1}", strName, strFailedReason);
            }
			else
			{
				strTmp = strFailedReason;
			}

			if (idToTable.pduType == (int)PduType.Set)
			{
				strStyle = CommString.IDS_SETPDU_ERROR; //SET命令响应错误
			}
			else if (idToTable.pduType == (int)PduType.Get) //GET命令响应错误
			{
				strStyle = CommString.IDS_GETPDU_ERROR;
            }
			else
			{
				strStyle = CommString.IDS_PDU_ERROR;  //命令响应错误
			}

			strShowMsg = string.Format("{0}:{1}", strStyle, strTmp);


			ShowLogHelper.Show(strShowMsg, lmtPdu.m_SourceIp, InfoTypeEnum.ENB_INFO);

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
			string strShowMsg = "";
			// 操作结果
			string strOperResult = CommString.IDS_OPERLOG_SUCCESS; // "成功"

			// 遍历vb对，通知上层更新数据
			int vbCount = lmtPdu.VbCount();
			for (int i = 0; i < vbCount; i++)
			{
				CDTLmtbVb lmtVb = new CDTLmtbVb();
				lmtPdu.GetVbByIndex(i, ref lmtVb);
				// 获取名称及描述等信息
				string strName = "";
				string strDesc = "";
				string strUnitName = "";
				if (lmtPdu.m_bIsNeedPrint == true 
						&& false == CommSnmpFuns.GetInfoByOID(lmtPdu.m_SourceIp, lmtVb.Oid
							, lmtVb.Value, out strName, out strDesc, out strUnitName))
				{
					Log.Error(string.Format("GetInfoByOID调用不成功:OID = {0}", lmtVb.Oid));
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
				if (lmtPdu.m_bIsNeedPrint == true)
				{
					string strMsg = string.Format("被管对象 {0} ,值为: {1}", strName, strDesc);
					if (!string.IsNullOrEmpty(strUnitName))
					{
						strMsg = string.Format("{0} (单位/精度: {1})", strMsg, strUnitName);
					}

					// 时间信息
					// TODO:打印信息中已经有统一的时间了，在这在计算时间没意义吧？暂时去掉

					strShowMsg = string.Format("{0}{1};\n", strShowMsg, strMsg);

					// 如果是set操作，写操作日志到数据库
					if (idToTable.pduType == (int)PduType.Set)
					{
						// TODO
					}


                } // end if



			} // end for

			// 数据写入到数据库中，把数据库联接和PDU发给数据同步工作线程
			// CDTDataSyncMgr::GetInstance()->DealResponse(pAdoConn, pLmtbPdu);
			// TODO

			if (lmtPdu.m_bIsNeedPrint == true)
			{
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
			string strNodeBIp = lmtPdu.m_SourceIp;
			string strValue;

			string strEventInfo = "";
			// 判定事件类型，输出事件信息
			if (false == ClassifyEvent(intTrapType, lmtPdu, out strEventInfo))
			{
				Log.Error(string.Format("事件分类处理方法ClassifyEvent返回错误:{0}", strEventInfo));
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
				Log.Error(string.Format("SaveEventTrap返回失败:{0}", strEventInfo));
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

			// fileTransNotiResult
			if (true == lmtPdu.GetValueByMibName(strNodeBIp, "fileTransNotiResult", out strValue))
			{
				//1:正在传输/2:正在解压缩
				if ("1".Equals(strValue) || "2".Equals(strValue))
				{
					// 不需要打印结果
					return true;
				}
			}

			// 输出信息
			strEventInfo = "收到" + strEventInfo;
			// TODO
//			CInfoBrowseMgr::GetInstance().PushStrInfo(OM_EVENT_NOTIFY_INFO, strEventInfo, pLmtbPdu->get_SourceIp());

			return true;
		}

		/// <summary>
		/// 处理事件Trap之文件传输
		/// </summary>
		/// <param name="lmtPdu">上传的PDU</param>
		private void DealFileTransTrap(CDTLmtbPdu lmtPdu)
		{
			// 网元IP
			string strIPAddr = lmtPdu.m_SourceIp;

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
			int uploadFileType = 0;
			if (!string.IsNullOrEmpty(strValue))
			{
				uploadFileType = Convert.ToInt32(strValue);
			}
				

			// 含FTP服务器路径的文件名
			lmtPdu.GetValueByMibName(strIPAddr, "fileTransNotiFileName", out strValue);
			// ForTest
			// strValue = "E:\\afyf\\src\\LMT2018\\Src\\SCMT\\MainWindow\\bin\\x86\\Debug\\filestorage\\DATA_CONSISTENCY\\enb_0_20090101000015+8_dataconsistency.dcb";
			if (string.IsNullOrEmpty(strValue))
			{
				Log.Error("文件名为空.");
				return;
			}

			Log.Info(string.Format("文件名:{0}", strValue));
			//将文件路径转换成windows格式
			strValue.Replace('/', '\\');
			strValue.Trim();

			// 件是否存在
			if (FilePathHelper.FileExists(strValue) != true)
			{
				Log.Error(string.Format("上传的文件{0}不存在!", strValue));
				return;
			}

			// lm.dtz文件
			if (26 == uploadFileType)
			{
				// TODO: 后续实现
				/*
				CString uploadFilePath = strValue;

				LRESULT lRes;
				CDtMsgDispCenter::Initstance().ProcessWindowMessage(NULL,
					WM_LOAD_LMDTZ_TO_VERSIONDB,
					(WPARAM)(LPCTSTR)pLmtbPdu->get_SourceIp(),
					(LPARAM)(LPCTSTR)uploadFilePath,
					lRes);
				*/

				string strUploadFilePath = strValue;
				// 发布消息
				PublishHelper.PublishMsg(TopicHelper.LoadLmdtzToVersionDb
					, $"{{\"SourceIp\" : \"{lmtPdu.m_SourceIp}\", \"UploadFilePath\" :\"{strUploadFilePath}\" }}");

				return;
			}

			// 文件的扩展名
			string strFileType = strValue.Substring(strValue.Length - 3, 3);
			strFileType = strFileType.ToLower();

			
			if (strFileType == "dcb") // 一致性文件
			{
				int index;
				// 检查这个文件是否在指定目录下,如果在,则解析之
				// 文件全路径
				string strUpLoadFullPath = strValue;

				// 去掉文件名称，获取文件路径
				index = strUpLoadFullPath.LastIndexOf("\\");
				string strUpLoadPath = strUpLoadFullPath.Substring(0, index + 1);

				// 系统指定路径
				string strAppointPath = AppPathUtiliy.Singleton.GetAppPath() + "filestorage\\DATA_CONSISTENCY";

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
				// TODO
				/* 
				LRESULT lRes;
				CDtMsgDispCenter::Initstance().ProcessWindowMessage(NULL, WM_PARSER_DATACONFILE,
					(WPARAM)(LPCTSTR)pLmtbPdu->get_SourceIp(),
					(LPARAM)(LPCTSTR)strUpdatePath,
					lRes);
				*/

				// 发布消息
				PublishHelper.PublishMsg(TopicHelper.ParseDataConFile
					, $"{{\"SourceIp\" : \"{lmtPdu.m_SourceIp}\", \"UpdatePath\" : \"{strUpLoadPath}\"}}");

				return;
			}
			else
			{
				Log.Error(string.Format("上传其他的文件类型{0}，暂时不处理.", strFileType));

				return;
			}
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
			string strNodeIp = lmtPdu.m_SourceIp;

			long taskId = 0;
			long requestId = 0;
			//收到初配上报事件, 发起一致性文件上传
			string strDataConsisFolderPath = AppPathUtiliy.Singleton.GetDataConsistencyFolderPath();
			// TODO 先保证编译通过
			//var transFileObj = FileTransTaskMgr.FormatTransInfo(
			//												strDataConsisFolderPath
			//												,""
			//												, Transfiletype5216.TRANSFILE_dataConsistency
			//												, TRANSDIRECTION.TRANS_UPLOAD);
			//if (SENDFILETASKRES.TRANSFILE_TASK_FAILED == FileTransTaskMgr.SendTransFileTask(strNodeIp, transFileObj, ref taskId, ref requestId))
			//{
			//	Log.Error(string.Format("下发上传数据一致性文件传输任务失败，数据一致性文件目录{0}，网元IP为{1}", strDataConsisFolderPath, strNodeIp));

			//}
			//else
			//{
			//	Log.Error("下发上传数据一致性文件传输任务成功！--网元IP为{0}", strNodeIp);
			//}

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
			StringBuilder sb = new StringBuilder();
			string strValue;
			// 传输结果
			string strTrapResult;
			string strReValue;
			string strGeneralEventType;

			// 网元IP
			string strNodeBIp = lmtPdu.m_SourceIp;

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
						
						if (false == CommSnmpFuns.TranslateMibValue(strNodeBIp, "fileTransNotiIndicator", strValue, out strReValue, true))
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
						if (false == CommSnmpFuns.TranslateMibValue(strNodeBIp, "fileTransNotiResult", strValue, out strReValue, true))
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
							if (false == CommSnmpFuns.TranslateMibValue(strNodeBIp, "fileTransNotiErrorCode", strValue, out strReValue, true))
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
						if (false == CommSnmpFuns.TranslateMibValue(strNodeBIp, "eventGeneralEventType", strValue, out strReValue, true))
						{
							return false;
						}
						sb.Append("事件类型:").Append(strReValue).Append(";");
					}

					// eventGeneralEventResult
					lmtPdu.GetValueByMibName(strNodeBIp, "eventGeneralEventResult", out strValue);
					if (!string.IsNullOrEmpty(strValue))
					{
						if (false == CommSnmpFuns.TranslateMibValue(strNodeBIp, "eventGeneralEventResult", strValue, out strReValue, true))
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
						string strFrameNo = "";
						// 槽位
						string strSlotNo = "";
						string strValueTmp = strValue;

						sb.Append("事件产生源:");
						// 截取机框和槽位号
						int intDotIndex = strValueTmp.LastIndexOf('.');
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
							Log.Error(string.Format("获取MIb节点信息错误，oid={0}", strValueTmp));
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
				case 26: // 处理同步事情trap
					// TODO : 用到时需实现
					break;
				case 200: // 处理ANR专用事情
						  // TODO : 用到时需实现
					break;
				case 201:  //处理MRO专用事情
						   // TODO : 用到时需实现
					break;
				case 202:  //处理FC专用事件
						   // TODO : 用到时需实现
					break;
				case 203:  // 
						   // TODO : 用到时需实现
					break;
				case 204:  // 
						   // TODO : 用到时需实现
					break;
				default:
					Log.Error("ClassifyEvent方法中不能识别的PDU类型.");
					return false;
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
			StringBuilder sbReVal = new StringBuilder();

			// 网元IP
			string strIpAddr = lmtPdu.m_SourceIp;

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
				CommSnmpFuns.TranslateMibValue(strIpAddr, "transactionResultNotiNEType", strValue, out strReValue, true);
				sbReVal.Append("网元类型:").Append(strReValue).Append("; ");
			}


			//transactionResultTrapResult
			bool bTransSuccess = true;
			lmtPdu.GetValueByMibName(strIpAddr, "transactionResultNotiResult", out strValue);
			if (!string.IsNullOrEmpty(strValue))
			{
				string strTmpVal;
				CommSnmpFuns.TranslateMibValue(strIpAddr, "transactionResultNotiResult", strValue, out strTmpVal, true);
				if ("1".Equals(strValue)) //失败
				{
					bTransSuccess = false;
					string strErrorValue;
					StringBuilder sbErrorMsg = new StringBuilder();
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
				string strDataConsisFolderPath = AppPathUtiliy.Singleton.GetDataConsistencyFolderPath();
				
				//TODO 暂时注销，先保证编译通过
				//var transFileObj = FileTransTaskMgr.FormatTransInfo(
				//	strDataConsisFolderPath
				//	, ""
				//	, Transfiletype5216.TRANSFILE_dataConsistency
				//	, TRANSDIRECTION.TRANS_UPLOAD);
				//if (SENDFILETASKRES.TRANSFILE_TASK_FAILED == FileTransTaskMgr.SendTransFileTask(strIpAddr, transFileObj, ref taskId, ref requestId))
				//{
				//	Log.Error(string.Format("下发上传数据一致性文件传输任务失败，数据一致性文件目录{0}，网元IP为{1}", strDataConsisFolderPath, strIpAddr));

				//}
				//else
				//{
				//	Log.Error("下发上传数据一致性文件传输任务成功！--网元IP为{0}", strIpAddr);
				//}
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
			StringBuilder sbReVal = new StringBuilder();
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
			string strReVal;
			StringBuilder sbReVal = new StringBuilder();

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
			int iReadLength = 24;//目前已经解析的数据
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
				for(int i = 0; i < iFqLoop; i++)
				{
					strTmp = strAddiInfo.Substring(iReadLength + 4 + 2 + (i * (4 + (10 * iCounterNum))), 4);// 'Ci 30
					intTmp = Convert.ToInt32(strTmp, 16);
					sbReVal.Append("频点:");

					for(int j = 0; j < iCounterNum; j++)
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
							if (j != (iCounterNum - 1))
							{
								sbReVal.Append(";");
							}
							else
							{
								sbReVal.Append(".");
							}
						}
					} // end for
				}// end for
				iReadLength = iReadLength + 2 + 4 + (iFrequenceNum * (4 + (10 * iCounterNum)));

			} // end while


			return sbReVal.ToString();
		}

		/// <summary>
		/// 检查是否为同一个Trap
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
			else // 不能存在， 添加
			{
				// 只缓存4个id，多于4个删除
				if (requestIdList.Count() > 4)
				{
					requestIdList.RemoveAt(0);
				}

				requestIdList.Add(requestId);
			}

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
			// 获取Trap Oid
			string strSubTrapOid = strTrapOid.Substring(strOidPrefix.Length + 1);

			if (string.IsNullOrEmpty(strSubTrapOid))
			{
				Log.Error("Trap Oid 截取错误！");
				return false;
			}

			// 根据oid获取Mib节点信息
			var reNodeInfo = new MibLeaf();
			var reData = new Dictionary<string, MibLeaf>();
			reData.Add(strTrapOid, reNodeInfo);
			string strError;

			if(false == Database.GetInstance().GetMibDataByOids(reData, strIpAddr, out strError))
			{
				Log.Error("获取Mib节点信息失败！");
				return false;
			}

			// Mib名称
			string strMibName = reData[strTrapOid].childNameMib;

			// 查询是否有该类型的Trap
			Dictionary<string,Dictionary<string, string>> trapTypeInfo = Database.GetInstance().GetTrapInfo();
			string strTrapId = trapTypeInfo[strMibName]["TrapID"];
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
				strErrorMsg = string.Format("ErrorStatus: {0} , ErrorIndex: {1} ."
					, lmtPdu.m_LastErrorStatus, lmtPdu.m_LastErrorIndex);
				Log.Error(string.Format("接收到的包不正确--{0}", strErrorMsg));
				return true;
			}

			// 判断VB个数
			int vbCount = lmtPdu.VbCount();
			if (vbCount <= 0)
			{
				Log.Error(string.Format("接收到的包VB个数不正确:{0}", vbCount));
				return false;
			}

			//挨个检查VB的OID字段
			for (int i = 0; i < vbCount; i++)
			{
				CDTLmtbVb lmtVb = new CDTLmtbVb();
				lmtPdu.GetVbByIndex(i, ref lmtVb);
				if (string.IsNullOrEmpty(lmtVb.Oid))
				{
					Log.Error(string.Format("接收到的包VB的OID写法为空,不正确:{0}", i));
					return false;
				}

			}

			return true;
		}

		/// <summary>
		/// 将Trap的snmp类型的pdu转换为LmtSnmp的pdu
		/// </summary>
		/// <param name="pdu"></param>
		/// <param name="iPEndPort"></param>
		/// <param name="lmtPdu"></param>
		/// <param name="reason"></param>
		/// <param name="isAsync"></param>
		private bool SnmpPdu2LmtPdu4Trap(Pdu pdu, IPEndPoint iPEndPort, ref CDTLmtbPdu lmtPdu, int reason, bool isAsync)
		{
			string logMsg;
			if (lmtPdu == null)
			{
				Log.Error("参数[lmtPdu]为空");
				return false;
			}

			if (pdu.Type != PduType.V2Trap)
			{
				Log.Error("接收到的不是Trap消息或不是V2Trap消息！");
				return false;
			}

			stru_LmtbPduAppendInfo appendInfo = new stru_LmtbPduAppendInfo();
			appendInfo.m_bIsSync = !isAsync;

			logMsg = string.Format("snmpPackage.Pdu.Type = {0}", pdu.Type);
//			Log.Debug(logMsg);

			appendInfo.m_bIsNeedPrint = true;


			lmtPdu.Clear();
			lmtPdu.m_LastErrorIndex = pdu.ErrorIndex;
			lmtPdu.m_LastErrorStatus = pdu.ErrorStatus;
			lmtPdu.m_requestId = pdu.RequestId;
			lmtPdu.assignAppendValue(appendInfo);

			// 设置IP和端口信息
			IPAddress srcIpAddr = iPEndPort.Address;
			int port = iPEndPort.Port;
			lmtPdu.m_SourceIp = srcIpAddr.ToString();
			lmtPdu.m_SourcePort = port;

			lmtPdu.reason = reason;
			lmtPdu.m_type = (ushort)pdu.Type;


			// TODO
			/*
			LMTORINFO* pLmtorInfo = CDTAppStatusInfo::GetInstance()->GetLmtorInfo(csIpAddr);
			if (pLmtorInfo != NULL && pLmtorInfo->m_isSimpleConnect && pdu.get_type() == sNMP_PDU_TRAP)
			{
				Oid id;
				pdu.get_notify_id(id);
				CString strTrapOid = id.get_printable();
				if (strTrapOid != "1.3.6.1.4.1.5105.100.1.2.2.3.1.1")
				{
					//如果是简单连接网元的非文件传输结果事件，就不要往上层抛送了
					return FALSE;
				}
			}
			*/

			//如果是错误的响应，则直接返回
			if (lmtPdu.m_LastErrorStatus != 0 || reason == -5)
			{
				return true;
			}

			// 获取MIB前缀
			string prefix = SnmpToDatabase.GetMibPrefix().Trim('.');
			if (string.IsNullOrEmpty(prefix))
			{
				Log.Error(string.Format("获取MIB前缀失败!"));
				return false;
			}

			// 对于Trap消息,我们自己额外构造两个Vb，用来装载时间戳和trap Id 
			if (pdu.Type == PduType.V2Trap) // Trap
			{
				// 构造时间戳Vb
				CDTLmtbVb lmtVb = new CDTLmtbVb();
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

			foreach (Vb vb in pdu.VbList)
			{
				logMsg = string.Format("ObjectName={0}, Type={1}, Value={2}"
					, vb.Oid.ToString(), SnmpConstants.GetTypeName(vb.Value.Type), vb.Value.ToString());
				Log.Debug(logMsg);

				CDTLmtbVb lmtVb = new CDTLmtbVb();

				lmtVb.Oid = vb.Oid.ToString();

				// 值是否需要SetVbValue()处理
				bool isNeedPostDispose = true;

				string strValue = vb.Value.ToString();

				// TODO
				lmtVb.SnmpSyntax = (SNMP_SYNTAX_TYPE)vb.Value.Type; //vb.GetType();

				// 如果是getbulk响应返回的SNMP_SYNTAX_ENDOFMIBVIEW，则不处理这个vb，继续
				if (lmtVb.SnmpSyntax == SNMP_SYNTAX_TYPE.SNMP_SYNTAX_ENDOFMIBVIEW)
				{
					lmtPdu.isEndOfMibView = true;
					continue;
				}

				if (SNMP_SYNTAX_TYPE.SNMP_SYNTAX_OCTETS == lmtVb.SnmpSyntax)
				{
					/*对于像inetipAddress和DateandTime需要做一下特殊处理，把内存值转换为显示文本*/
					// CString strNodeType = GetNodeTypeByOIDInCache(csIpAddr, strOID, strMIBPrefix);
					// 获取Mib节点信息
					var reDataByOid = new MibLeaf();
					string strError;
					if (false == Database.GetInstance().GetMibDataByOid(lmtVb.Oid, out reDataByOid, lmtPdu.m_SourceIp, out strError))
					{
						Log.Error($"获取MIb节点信息错误，oid={lmtVb.Oid}");
						return false;
					}
					MibLeaf getNameInfo = null;
					if (!Database.GetInstance().getDataByEnglishName(reDataByOid.childNameMib, out getNameInfo, lmtPdu.m_SourceIp, out strError))
					{
						Log.Error($"获取MIb节点信息错误，oid={lmtVb.Oid}");
						return false;
					}
					string strNodeType = getNameInfo.mibSyntax;

					// string strNodeType = "";
					// strNodeType = "DateandTime";

					if (string.Equals("DateandTime", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValue = SnmpHelper.SnmpDateTime2String((OctetString)vb.Value);
						isNeedPostDispose = false;
					}
					else if (string.Equals("inetaddress", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						IpAddress ipAddr = new IpAddress((OctetString)vb.Value);
						strValue = ipAddr.ToString();
						isNeedPostDispose = false;
					}
					else if (string.Equals("MacAddress", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValue = ((OctetString)vb.Value).ToMACAddressString();
						isNeedPostDispose = false;
					}
					else if (string.Equals("Unsigned32Array", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValue = SnmpHelper.OctetStrToU32Array((OctetString)vb.Value);
						isNeedPostDispose = false;
					}
					else if (string.Equals("Integer32Array", strNodeType, StringComparison.OrdinalIgnoreCase)
						|| "".Equals(strNodeType))
					{
						strValue = SnmpHelper.OctetStrToS32Array((OctetString)vb.Value);
						isNeedPostDispose = false;
					}
					else if (string.Equals("MncMccType", strNodeType, StringComparison.OrdinalIgnoreCase))
					{
						strValue = SnmpHelper.OctetStr2MncMccTypeStr((OctetString)vb.Value);
						isNeedPostDispose = false;
					}
				}

				if (isNeedPostDispose)// 需要再处理
				{
					SnmpHelper.GetVbValue(vb, ref strValue);
				}

				lmtVb.Value = strValue;
				lmtPdu.AddVb(lmtVb);
			} // end foreach


			//如果得到的LmtbPdu对象里的vb个数为0，说明是是getbulk响应，并且没有任何实例
			//为方便后面统一处理，将错误码设为资源不可得
			if (lmtPdu.VbCount() == 0)
			{
				// TODO: SNMP_ERROR_RESOURCE_UNAVAIL
				lmtPdu.m_LastErrorStatus = 13;
				lmtPdu.m_LastErrorIndex = 1;
			}

			return true;
		}


	}
}
