/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ StateObject $
* 机器名称：       $ machinename $
* 命名空间：       $ CommonUtility $
* 文 件 名：       $ StateObject.cs $
* 创建时间：       $ 2018.11.XX $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     socket状态信息类
* 修改时间     修 改 人         修改内容：
* 2018.10.xx  XXXX            XXXXX
*************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
	public class StateObject
	{
		public Socket workSocket { set; get; }
		public byte[] buffer { get; set; }

		public StateObject()
		{
			buffer = new byte[64 * 1024];
		}
	}
}
