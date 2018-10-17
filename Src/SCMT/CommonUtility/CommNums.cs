/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ CommNums $
* 机器名称：       $ machinename $
* 命名空间：       $ DataBaseUtil $
* 文 件 名：       $ CommNums.cs $
* 创建时间：       $ 2018.10.XX $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     公共数字变量定义。
* 修改时间     修 改 人         修改内容：
* 2018.xx.xx  XXXX            XXXXX
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
	/// <summary>
	/// 数字定义
	/// </summary>
	public static class CommNums
	{
		//OM 自定义错误宏
		public const int SNMP_ERROR_LOGIN        = 31;
		public const int SNMP_ERROR_ACTIONSHIELD = 32;
		public const int SNMP_ERROR_ACTIONFAILD = 33;
		public const int SNMP_ERROR_OMBUSY = 34;
		public const int SNMP_ERROR_OBSOLETE = 35;
		public const int SNMP_ERROR_OVERTIME = 36;
	}
}
