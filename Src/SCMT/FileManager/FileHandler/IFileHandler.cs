using LinkPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//文件处理
namespace FileManager.FileHandler
{
	public interface IFileHandler
	{
		// 本地文件到远端
		ExecuteResult DoPutFile(string srcFileFullName, string dstFilePath);

		// 远端文件到本地
		ExecuteResult DoGetFile(string localPath, string remoteFullPath);
	}
}
