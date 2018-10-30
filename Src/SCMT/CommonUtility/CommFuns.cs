/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ CommFuns $
* 机器名称：       $ machinename $
* 命名空间：       $ CommonUtility $
* 文 件 名：       $ CommFuns.cs $
* 创建时间：       $ 2018.09.XX $
* 作    者：       $ fengyanfeng $
* 说   明 ：
*     提供一些公共功能的函数接口。
* 修改时间     修 改 人         修改内容：
* 2018.09.xx  XXXX            XXXXX
*************************************************************************************/

using LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
	/// <summary>
	/// 提供一些公共功能的函数接口
	/// </summary>
	public class CommFuns
	{
		/// <summary>
		/// 根据Mib描述解析出单位来
		/// </summary>
		/// <param name="strMibDesc"></param>
		/// <returns></returns>
		public static string ParseMibUnit(string strMibDesc)
		{
			Log.Debug(string.Format("strMibDesc = {0}", strMibDesc));

			string strUnit = "";
			if (string.IsNullOrEmpty(strMibDesc))
			{
				return "";
			}

			// 一般格式为{单位\精度:second}, 再取出":"后的字符
			int indexLeft = strMibDesc.IndexOf('{');
			int indexRight = strMibDesc.LastIndexOf('}');
			if (indexLeft < 0 || indexRight < 0) // "{"和"}"不同时存在
			{
				return "";
			}

			strUnit = strMibDesc.Substring(indexLeft, indexRight - indexLeft);

			// 截取":"后面部分
			int indexM = strUnit.IndexOf(':');
			if (indexM >= 0)
			{
				strUnit = strUnit.Substring(indexM);
			}

			// 小数点需要补上0
			if (strUnit.StartsWith("."))
			{
				strUnit = "0" + strUnit;
			}

			return strUnit;
		}

		/// <summary>
		/// 生成邻区索引
		/// </summary>
		/// <param name="strNetWorkType"></param>
		/// <param name="strAdjCellId"></param>
		/// <returns></returns>
		public static string GenerateAdjCellIdInfo(string strNetWorkType, string strAdjCellId)
		{
			// 返回值
			string reValue = "";

			if (string.IsNullOrEmpty(strAdjCellId))
			{
				return strAdjCellId;
			}

			string strAdjCellIdTmp = strAdjCellId.TrimStart('{');
			strAdjCellIdTmp = strAdjCellIdTmp.TrimEnd('}');
			string[] idList = strAdjCellIdTmp.Split(',');
			reValue = strAdjCellIdTmp;

			if (idList.Length != 3)
			{
				return reValue;
			}
			
			if ("0".Equals(strNetWorkType))
			{
				reValue = string.Format("{{eNBId: {0},AdjCellId: {1}}}", idList[0], idList[1]);
			}
			else if ("1".Equals(strNetWorkType) || "2".Equals(strNetWorkType))
			{
				reValue = string.Format("{{LACId: {0},RNCId: {1},AdjCellId: {2}}}", idList[0], idList[1], idList[2]);
			}
			else if ("3".Equals(strNetWorkType))
			{
				reValue = string.Format("{{LACId: {0},AdjCellId: {1}}}", idList[0], idList[1]);
			}

			return reValue;
		}

	}
}
