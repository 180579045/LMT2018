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
		// 开始干活吧
		ExecuteResult DoHandle(string srcFileFullName, string dstFilePath);
	}
}
