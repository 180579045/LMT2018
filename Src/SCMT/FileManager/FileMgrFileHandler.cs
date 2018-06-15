using System;
using System.IO;
using CommonUility;
using FileManager.FileHandler;
using LogManager;
using MsgQueue;
using SCMTOperationCore.Message.SI;

namespace FileManager
{
	public delegate void GetFileInfoRspHandler(byte[] rspData);

	// 只是把原来的函数先简单的封装在一个文件中
	public class FileMgrFileHandler
	{
		#region 委托、事件

		public event GetFileInfoRspHandler GetFileInfoRspArrived;

		#endregion

		#region 构造、析构

		public FileMgrFileHandler(string boardIp)
		{
			this._boardIp = boardIp;

			// 订阅SI事件
			SubscribeHelper.AddSubscribe($"/{_boardIp}/O_SILMTENB_GETFILEINFO_RES", OnGetFileInfoRsp);
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
			string ext = FilePathHelper.GetFileExt(dstFullPath);
			if (null == ext)
			{
				throw new CustomException($"从文件路径{dstFullPath}获取扩展名失败");
			}

			IFileHandler handler = FileHandlerFactory.CreateHandler(ext, _boardIp);
			var result = handler.DoHandle(dstFullPath, remotePath);
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

			FileTransTaskMgr.HasTransFileWork = true;

			// TODO UI模块启动定时器

			return true;
		}

		// 上传文件到本地 OnMenuSinglefileget
		public bool UploadFileToLocal(string localFilePath, string remoteFilePath)
		{
			throw new NotImplementedException("上传文件到本地，尚未实现");
		}


		#endregion

		#region 订阅事件处理方法

		// 查询文件信息响应
		private void OnGetFileInfoRsp(SubscribeMsg msg)
		{
			GetFileInfoRspArrived?.Invoke(msg.Data);
		}

		#endregion

		#region 私有方法

		private bool CanUpdate()
		{
			throw new NotImplementedException();
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
			return FileTransTaskMgr.HasTransFileWork || FileTransTaskMgr.HasUpgradeWork;
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

		#endregion

		#region 私有成员、属性

		private string _boardIp;

		#endregion

	}
}
