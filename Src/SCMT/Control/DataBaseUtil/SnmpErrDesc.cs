/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ SnmpErrDesc $
* 机器名称：       $ machinename $
* 命名空间：       $ DataBaseUtil $
* 文 件 名：       $ SnmpErrDesc.cs $
* 创建时间：       $ 2018.10.16 $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     Snmp错误描述模型类。
* 修改时间     修 改 人         修改内容：
* 2018.xx.xx  XXXX            XXXXX
*************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUtil
{
	public class SnmpErrDesc
	{
		// 错误编码
		public string errorID { get; set; }

		// 错误描述中文
		public string errorChDesc { get; set; }
		
		// 错误描述英文
		public string errorEnDesc { get; set; }
	}
}
