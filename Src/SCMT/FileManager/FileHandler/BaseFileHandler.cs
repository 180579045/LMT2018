using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.FileHandler
{
	public class BaseFileHandler : IFileHandler
	{
		public BaseFileHandler(string ip)
		{
			boardAddr = ip;
		}

		public virtual bool DoHandle(string srcFileFullName, string dstFilePath)
		{
			throw new NotImplementedException();
		}




		protected string boardAddr;
	}
}
