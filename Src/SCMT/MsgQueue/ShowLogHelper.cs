using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtility;

// 前台显示日志助手

namespace MsgQueue
{
	public static class ShowLogHelper
	{
		public static void Show(string msg, string targetIp, InfoTypeEnum level = InfoTypeEnum.ENB_INFO)
		{
			var logInfo = new LogInfoStruct(msg, targetIp, level);
			var jsonToSend = JsonHelper.SerializeObjectToString(logInfo);
			PublishHelper.PublishMsg(TopicHelper.SHOW_LOG, jsonToSend);
		}

		public static LogInfoStruct GetLogInfo(byte[] logBytes)
		{
			var logInfoObj = JsonHelper.SerializeJsonToObject<LogInfoStruct>(logBytes);
			return logInfoObj;
		}
	}

	public enum InfoTypeEnum
	{
		INVALID = -1,
		ENB_INFO,				//信息浏览窗口显示的与SCMT自身工作相关的信息主要包括以下几方面： 所处状态、连接NodeB的IP等
		ENB_TASK_DEAL_INFO,		//特殊任务处理情况 SI阶段使用灰色和OM阶段区分。
		SI_STR_INFO,			//直接上报的字符串,文件传输结果,目标文件查询等
		SI_ALARM_INFO,			//启动告警

		/************************************OM阶段告警***************************************/
		OM_BRKDWN_ALARM_INFO,	//故障类告警红色
		OM_EVENT_ALARM_INFO,	//事件类告警黄色
		OM_ALARM_CLEAR_INFO,
		OM_EVENT_NOTIFY_INFO,
		/**************************************************************************************/

		ENB_GETOP_INFO,
		ENB_SETOP_INFO,
		ENB_GETOP_ERR_INFO,
		ENB_SETOP_ERR_INFO,

		//变更通知
		ENB_VARY_INFO,

		//其他事件
		ENB_OTHER_INFO,

		ENB_OTHER_INFO_IMPORT,
		CUSTOM_ERROR_INFO,				//用户定制的输出
		CUSTOM_TIP_INFO,
	}

	public static class InfoTypeConvert
	{
		#region 公共接口

		public static string GetDescByType(InfoTypeEnum type)
		{
			if (!Enum.IsDefined(typeof(InfoTypeEnum), type))
			{
				return null;
			}

			if (m_mapTypeToDesc.ContainsKey(type))
			{
				return m_mapTypeToDesc[type];
			}

			return null;
		}

		public static InfoTypeEnum GetTypeByDesc(string desc)
		{
			if (string.IsNullOrEmpty(desc))
			{
				return InfoTypeEnum.INVALID;
			}

			if (m_mapTypeToDesc.ContainsValue(desc))
			{
				return m_mapTypeToDesc.First(kv => kv.Value == desc).Key;
			}

			return InfoTypeEnum.INVALID;
		}

		#endregion

		#region 私有成员

		private static Dictionary<InfoTypeEnum, string> m_mapTypeToDesc = new Dictionary<InfoTypeEnum, string>
		{
			{InfoTypeEnum.ENB_INFO, "SCMT信息"},
			{InfoTypeEnum.ENB_TASK_DEAL_INFO, "SCMT任务处理" },
			{InfoTypeEnum.SI_STR_INFO, "直接显示字符串" },
			{InfoTypeEnum.SI_ALARM_INFO, "启动告警" },
			{InfoTypeEnum.OM_BRKDWN_ALARM_INFO, "故障类告警提示" },
			{InfoTypeEnum.OM_ALARM_CLEAR_INFO, "告警清除提示" },
			{InfoTypeEnum.OM_EVENT_NOTIFY_INFO, "事件通知" },
			{InfoTypeEnum.ENB_GETOP_INFO, "GET命令响应" },
			{InfoTypeEnum.ENB_SETOP_INFO, "SET命令响应" },
			{InfoTypeEnum.ENB_SETOP_ERR_INFO, "SET命令错误响应" },
			{InfoTypeEnum.ENB_GETOP_ERR_INFO, "GET命令错误响应" },
			{InfoTypeEnum.ENB_VARY_INFO, "变更通知" },
			{InfoTypeEnum.ENB_OTHER_INFO, "其他信息" },
			{InfoTypeEnum.ENB_OTHER_INFO_IMPORT, "其他信息(重要)" },
			{InfoTypeEnum.CUSTOM_ERROR_INFO, "严重错误" },
			{InfoTypeEnum.CUSTOM_TIP_INFO, "提示信息" },
			{InfoTypeEnum.OM_EVENT_ALARM_INFO, "事件类告警提示" },
		};

		#endregion
	}

	public class LogInfoStruct
	{
		public string Msg;
		public string TargetIp;
		public InfoTypeEnum Type;

		public LogInfoStruct(string msg, string targetIp, InfoTypeEnum level)
		{
			Msg = msg;
			TargetIp = targetIp;
			Type = level;
		}
	}
}