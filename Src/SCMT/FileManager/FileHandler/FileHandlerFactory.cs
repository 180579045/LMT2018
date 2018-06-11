using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FileManager.FileHandler
{
	public class FileHandlerFactory
	{
		// 根据文件的扩展名来创建具体的处理对象
		public static IFileHandler CreateHandler(string ext, string targetIp)
		{
			IFileHandler handler = null;

			string EXT = ext.ToUpper();

			if (EXT.Equals("CFG"))
			{
				handler = new CfgFileHandler(targetIp);
			}
			else if (EXT.Equals("DTZ"))
			{
				handler = new DtzFileHandler(targetIp);
			}
			else
			{
				handler = new BaseFileHandler(targetIp);
			}

			return handler;
		}
	}
}
