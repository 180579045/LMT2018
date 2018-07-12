using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 文件读写助手

namespace CommonUtility
{
	public static class FileRdWrHelper
	{
		/// <summary>
		/// 读取文件的全部内容。
		/// </summary>
		/// <param name="filePath">文件路径</param>
		/// <param name="encoding">读取编码，如果为null,则会用utf8编码进行解析</param>
		/// <returns></returns>
		public static string GetFileContent(string filePath, Encoding encoding = null)
		{
			if (!File.Exists(filePath))
			{
				throw new Exception($"文件 {filePath} 不存在");
			}

			if (null == encoding)
			{
				encoding = Encoding.UTF8;
			}

			FileStream fileStream = null;
			StreamReader fileReader = null;
			try
			{
				fileStream = new FileStream(filePath, FileMode.Open);
				fileReader = new StreamReader(fileStream, encoding);
				var fileContent = fileReader.ReadToEnd();
				fileReader.Close();

				return fileContent;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			finally
			{
				fileReader?.Close();
				fileStream?.Close();
			}
		}
	}
}
