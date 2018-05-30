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
		public static IFileHandler CreateHandler(string ext)
		{
			IFileHandler handler = null;

			string EXT = ext.ToUpper();

			if (EXT.Equals("CFG"))
			{
				handler = new CfgFileHandler();
			}
			else if (EXT.Equals("DTZ"))
			{
				handler = new DtzFileHandler();
			}
			else
			{
				handler = new BaseFileHandler();
			}

			return handler;
		}
	}
}
