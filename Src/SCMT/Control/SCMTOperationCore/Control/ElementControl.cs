using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using CommonUility;
using SCMTOperationCore.Elements;

namespace SCMTOperationCore.Control
{
	public abstract class ElementControl
	{
		#region 虚函数区，子类重写该部分接口

		//增加网元
		public virtual Element AddElement(string ip, string friendlyName, ushort port = 5000)
		{
			throw new NotImplementedException("please call implement class func");
		}

		//删除网元
		public virtual bool DelElement(string ip)
		{
			throw new NotImplementedException("please call implement class func");
		}

		#endregion


		#region 私有属性区



		#endregion
	}
}
