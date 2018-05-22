using System;
using System.Collections.Generic;
using System.Text;

//自定义异常信息，其他模块可以捕获异常以获得错误信息
namespace CommonUility
{
	public class CustomException : Exception
	{
		public CustomException(string msg) : base (msg)
		{

		}

		public CustomException(string msg, Exception e) : base (msg, e)
		{

		}
	}
}
