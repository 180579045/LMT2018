using System;
using System.IO;
using CommonUility;
using FileManager.FileHandler;

namespace FileManager
{
	class FileMgrFileHandler
	{

		//发送文件到远端
		public bool SendFileToRemote(string localFilePath, string remotePath)
		{
			//校验基站端是否允许升级
			if (!CanUpdate())
			{
				return false;
			}

			//创建临时目录等操作


			//获取选择的本地文件
			string srcfile = localFilePath;

			//TODO  copy file to back folder。后面操作使用的都是back file
			string dstfile = "";
			File.Copy(srcfile, dstfile, true);

			//判断是否存在其他正在进行的任务
			if (HasRunningTask())
			{
				return false;
			}

			// 使用工厂模式，创建不同文件的处理对象，在处理对象中进行文件处理
			string ext = GetExtString(dstfile);
			IFileHandler handler = FileHandlerFactory.CreateHandler(ext);
			if (!handler.DoHandle(dstfile, remotePath))
			{
				return false;
			}

			// TODO 启动定时器，定时查询下载进度等操作
			return true;
		}


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

		// 替换路径中的\和//
		private string ReplaceDosPathSlashToLinux(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new CustomException($"{path}字符串为空或null");
			}

			return path.Replace('\\', '/').Replace("//", "/");
		}

		// 判断是否存在有其他正在进行的任务
		private bool HasRunningTask()
		{
			throw new NotImplementedException();
		}

		// 获取路径中文件的扩展名，如果有，全改为大写。否则返回""
		private string GetExtString(string path)
		{
			string ext = "";
			if (!string.IsNullOrWhiteSpace(path) && !string.IsNullOrEmpty(path))
			{
				ext = Path.GetExtension(path);
			}

			return ext.ToUpper();
		}

		#endregion
	}
}
