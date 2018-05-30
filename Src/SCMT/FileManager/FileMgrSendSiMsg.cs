using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using CommonUility;
using LogManager;
using SCMTOperationCore.Control;
using SCMTOperationCore.Message.SI;

//文件管理模块SI消息发送和处理。后面再改，先实现
namespace FileManager
{
	public class FileMgrSendSiMsg
	{
		private string boardAddr;

		public FileMgrSendSiMsg(string ip)
		{
			boardAddr = ip;
		}

		//发送获取基站目录的请求
		public bool SendGetBoardFileInfoReq(string path)
		{
			if (null == path)
			{
				throw new CustomException("传入的路径为null");
			}

			SI_LMTENBSI_GetFileInfoReqMsg reqMsg = new SI_LMTENBSI_GetFileInfoReqMsg(path);
			return SerializeAndSend(reqMsg);
		}

		//发送获取文件属性请求
		public bool SendGetFileAttrReq(string path, string fileName)
		{
			if (null == path || null == fileName)
			{
				throw new CustomException("传入的路径或文件名为null");
			}

			SI_LMTENBSI_GetFileAttribReqMsg reqMsg = new SI_LMTENBSI_GetFileAttribReqMsg();
			reqMsg.SetPathAndName(path, fileName);
			return SerializeAndSend(reqMsg);
		}

		//发送查询容量请求
		public bool SendGetCapacityReq(string path = "/ata2")
		{
			if (null == path)
			{
				throw new CustomException("传入的路径为null");
			}

			SI_LMTENBSI_GetCapacityReqMsg reqMsg = new SI_LMTENBSI_GetCapacityReqMsg(path);
			return SerializeAndSend(reqMsg);
		}

		private bool SerializeAndSend(IASerialize reqMsg)
		{
			try
			{
				byte[] reqBytes = SerializeHelper.SerializeStructToBytes(reqMsg);
				if (null == reqBytes)
				{
					Log.Error("序列化失败");
					return false;
				}

				bool succeed = NodeBControl.SendSiMsg(boardAddr, reqBytes);
				if (!succeed)
				{
					Log.Error("发送失败");
					return false;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			return true;
		}
	}
}
