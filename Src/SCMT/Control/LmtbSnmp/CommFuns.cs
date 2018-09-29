using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmtbSnmp
{
	/// <summary>
	/// 提供一些公共功能的函数接口
	/// </summary>
	public class CommFuns
	{
		/// <summary>
		/// 把Mib的数值翻译成描述，或者是把描述翻译为实际的值
		/// </summary>
		/// <param name="strIpAddr"></param>
		/// <param name="strMibName">Mib名称</param>
		/// <param name="strMibValue">Mib的值或Mib的描述</param>
		/// <param name="strReValue">返回值，Mib描述或值</param>
		/// <param name="bValueToDesc">true:将值翻译为描述；false:将描述翻译为值</param>
		/// <returns></returns>
		public static bool TranslateMibValue(string strIpAddr, string strMibName, string strMibValue, out string strReValue, bool bValueToDesc = true)
		{
			strReValue = "";

			// 节点信息
			MibLeaf mibNodeInfo;
			string strError = "";
			if (false == Database.GetInstance().getDataByEnglishName(strMibName, out mibNodeInfo, strIpAddr, out strError))
			{
				Log.Error(string.Format("获取Mib信息失败，strMibName={0}", strMibName));
				return false;
			}
			if (mibNodeInfo == null)
			{
				Log.Error(string.Format("获取到的Mib信息为空，strMibName={0}", strMibName));
				return false;
			}

			// 节点取值范围
			string strMibValList = mibNodeInfo.managerValueRange;
			// 节点类型
			string strMibSyntax = mibNodeInfo.mibSyntax;

			string strInMibValue = strMibValue;
			// 处理BITS类型
			if (string.Equals("BITS", strMibSyntax, StringComparison.OrdinalIgnoreCase))
			{
				return GenerateBitsTypeDesc(strInMibValue, strMibValList, out strReValue);
			}

			// 根据mib值获取描述或根据描述获取mib值
			// TODO: 看不懂原来的逻辑。。。。先按照"0:本地文件/1:远端文件"格式解析 
			if (!string.IsNullOrEmpty(strMibValList))
			{
				string[] keyValList = strMibValList.Split('/');
				foreach (string item in keyValList)
				{
					string[] keyVal = item.Split(':');
					if (keyVal.Count() >= 2)
					{
						if (bValueToDesc == true) //值翻译为描述
						{
							if (string.Equals(strMibValue, keyVal[0], StringComparison.OrdinalIgnoreCase))
							{
								strReValue = keyVal[1];
								break;
							}
						}
						else // 描述翻译为值
						{
							if (string.Equals(strMibValue, keyVal[1], StringComparison.OrdinalIgnoreCase))
							{
								strReValue = keyVal[0];
								break;
							}
						}
					}

				}
			}

			// 如果没有获取到，将mib值返回
			if (string.IsNullOrEmpty(strReValue))
			{
				strReValue = strMibValue;
			}
			return true;
		}

		/// <summary>
		/// 将Bits类型的数值翻译成具体的位描述意义
		/// </summary>
		/// <param name="strBitsTypeValue"></param>
		/// <param name="strValueList"></param>
		/// <param name="strOutput"></param>
		/// <returns></returns>
		/// ===========================================================================
		/// Bit值=>0< strValueList=>0:时钟故障/1:传输故障< 输出=>无效<
		/// Bit值=>1< strValueList=>0:时钟故障/1:传输故障< 输出=>时钟故障<
		/// Bit值=>2< strValueList=>0:时钟故障/1:传输故障< 输出=>传输故障<
		/// Bit值=>3< strValueList=>0:时钟故障/1:传输故障< 输出=>时钟故障/传输故障<
		/// Bit值=>4< strValueList=>0:时钟故障/1:传输故障< 输出=>4<
		/// ===========================================================================
		public static bool GenerateBitsTypeDesc(string strBitsTypeValue, string strValueList, out string strOutput)
		{
			// 初始化
			strOutput = "";

			strBitsTypeValue.Trim();
			// "0"值
			if ("0".Equals(strBitsTypeValue))
			{
				strOutput = "无效";
				return true;
			}

			// 解析“0:时钟故障/1:传输故障”格式的字符串
			Dictionary<int, string> dicBit2Desc = new Dictionary<int, string>();
			string[] keyValList = strValueList.Split('/');
			foreach (string item in keyValList)
			{
				string[] keyVal = item.Split(':');
				dicBit2Desc.Add(Convert.ToInt32(keyVal[0]), keyVal[1]);
			}

			// 将bits值转换为unsigned long
			UInt32 u32BitsTypeValue = Convert.ToUInt32(strBitsTypeValue);
			for (int i = 0; i < 32; i++)
			{
				UInt32 u32BitValue = (UInt32)1 << i;
				UInt32 u32Tmp = u32BitsTypeValue & u32BitValue;
				if (u32Tmp != 0 & dicBit2Desc.ContainsKey(i))
				{
					strOutput += dicBit2Desc[i];
					strOutput += @"/";
				}
			}

			strOutput = strOutput.TrimEnd('/');

			// 如果没有解析结果, 把原值填上
			if (string.IsNullOrEmpty(strOutput))
			{
				strOutput = strBitsTypeValue;
			}

			return true;
		}


		/// <summary>
		/// 通过OID和对应的值，获得相关的信息（名字、描述、以及单位）
		/// </summary>
		/// <param name="oid"></param>
		/// <param name="strValue"></param>
		/// <param name="strName"></param>
		/// <param name="strValueDesc"></param>
		/// <param name="strUnitName"></param>
		public static bool GetInfoByOID(string oid, string strValue, out string strName
			, out string strValueDesc, out string strUnitName)
		{
			strName = "";
			strValueDesc = "";
			strUnitName = "";

			// TODO: 待实现

			return true;
		}

	}
}
