using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CommonUility
{
	// 文件路径操作助手
	public class FilePathHelper
	{
		// 获取当前程序运行路径。后面带有/
		public static string GetAppPath()
		{
			return System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
		}

		// 获取程序的Data目录。最后带有/
		public static string GetDataPath()
		{
			return GetAppPath() + "Data/";
		}

		// 获取程序目录下config目录，最后带有/
		public static string GetConfigPath()
		{
			return GetAppPath() + "Config/";
		}

		/// 获取tempfiles路径，后面追加调用时的时间。最后带有/
		public static string GetTempFilesPath()
		{
			var foldername = TimeHelper.GetFolderNameWithTime();
			return $"{GetDataPath()}tempfiles/{foldername}/";
		}

		// 根据传入的路径获取文件名。文件不存在返回null
		public static string GetFileNameFromFullPath(string fullPath)
		{
			var fi = new FileInfo(fullPath);
			if (fi.Exists)
			{
				return fi.Name;
			}

			return null;
		}

		// 根据传入的路径获取路径信息。文件不存在返回null
		public static string GetFileParentFolder(string fileFullPath)
		{
			var fi = new FileInfo(fileFullPath);
			if (fi.Exists)
			{
				return fi.DirectoryName;
			}

			return null;
		}

		// 根据传入的路径获取文件的扩展名，全部转换为大写
		public static string GetFileExt(string fileFullPath)
		{
			var fi = new FileInfo(fileFullPath);
			if (fi.Exists)
			{
				return fi.Extension.ToUpper();
			}

			return null;
		}

		// 删除文件夹
		public static bool DeleteFolder(string path)
		{
			try
			{
				var di = new DirectoryInfo(path);
				if (di.Exists)
				{
					di.Delete(true);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}

			return true;
		}

		// 删除指定的文件
		public static bool DeleteFile(string filePath)
		{
			try
			{
				var file = new FileInfo(filePath);
				if (file.Exists)
				{
					file.Delete();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}

			return true;
		}

		// 创建文件夹
		public static bool CreateFolder(string path)
		{
			try
			{
				var di = new DirectoryInfo(path);
				if (!di.Exists)
				{
					di.Create();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}

			return true;
		}

		// 拷贝文件，目的文件夹不存在会自动创建目录
		public static bool CopyFile(string srcPath, string dstPath, bool bOverWrite = true)
		{
			try
			{
				if (!File.Exists(srcPath))
				{
					return false;
				}

				var dstFi = new FileInfo(dstPath);
				if (!dstFi.Directory.Exists)
				{
					if (!CreateFolder(dstFi.DirectoryName))
					{
						return false;
					}
				}

				File.Copy(srcPath, dstPath, bOverWrite);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}

			return true;
		}

		// 替换路径中的\和//为/
		public static string ReplaceDosSlashToLinux(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new CustomException($"{path}字符串为空或null");
			}

			return path.Replace('\\', '/').Replace("//", "/");
		}

		// 判断文件是否存在
		public static bool FileExists(string filePath)
		{
			return File.Exists(filePath);
		}
	}
}
