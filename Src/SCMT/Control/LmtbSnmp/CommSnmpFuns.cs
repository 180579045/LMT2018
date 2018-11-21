using System;
using System.Collections.Generic;
using System.Linq;
using CommonUtility;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;

namespace LmtbSnmp
{
	/// <summary>
	/// 提供一些公共功能的函数接口
	/// </summary>
	public class CommSnmpFuns
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
			var mibNodeInfo = SnmpToDatabase.GetMibNodeInfoByName(strMibName, strIpAddr);
			if (mibNodeInfo == null)
			{
				Log.Error($"获取到的Mib信息为空，strMibName={strMibName}");
				return false;
			}

			// 节点取值范围
			var strMibValList = mibNodeInfo.managerValueRange;
			// 节点类型
			var strMibSyntax = mibNodeInfo.mibSyntax;

			var strInMibValue = strMibValue;

			// 处理BITS类型
			if (string.Equals("BITS", strMibSyntax, StringComparison.OrdinalIgnoreCase))
			{
				return GenerateBitsTypeDesc(strInMibValue, strMibValList, out strReValue);
			}

			// TODO 类型为Unsigned32Array时，下面的方法会异常
			//strReValue = SnmpToDatabase.ConvertValueToString(mibNodeInfo, strInMibValue);
			// todo 此处没有处理从描述转为值的情况

			// 根据mib值获取描述或根据描述获取mib值
			// TODO: 看不懂原来的逻辑。。。。先按照"0:本地文件/1:远端文件"格式解析
			if (!string.IsNullOrEmpty(strMibValList))
			{
				var keyValList = strMibValList.Split('/');
				foreach (var item in keyValList)
				{
					var keyVal = item.Split(':');
					if (keyVal.Length < 2) continue;

					if (bValueToDesc) //值翻译为描述
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

			// 如果没有获取到，将mib值返回
			if (string.IsNullOrEmpty(strReValue))
			{
				strReValue = strMibValue;
			}

			return true;
		}

		/// <summary>
		/// 根据取值列表把Mib的数值翻译成实际的意义，或把实际意义翻译成数值
		/// </summary>
		/// <param name="strMibValueList"></param>
		/// <param name="strMibValue"></param>
		/// <param name="strReValue"></param>
		/// <param name="isValueToDesc"></param>
		/// <returns></returns>
		public static bool TranslateMibValue(string strMibValueList, string strMibValue
												, out string strReValue, bool isValueToDesc = true)
		{
			strReValue = "";

			// 根据mib值获取描述或根据描述获取mib值
			//if (!string.IsNullOrEmpty(strMibValueList))
			//{
			//	var keyValList = strMibValueList.Split('/');
			//	foreach (var item in keyValList)
			//	{
			//		var keyVal = item.Split(':');
			//		if (keyVal.Length >= 2)
			//		{
			//			if (isValueToDesc) //值翻译为描述
			//			{
			//				if (string.Equals(strMibValue, keyVal[0], StringComparison.OrdinalIgnoreCase))
			//				{
			//					strReValue = keyVal[1];
			//					break;
			//				}
			//			}
			//			else // 描述翻译为值
			//			{
			//				if (string.Equals(strMibValue, keyVal[1], StringComparison.OrdinalIgnoreCase))
			//				{
			//					strReValue = keyVal[0];
			//					break;
			//				}
			//			}
			//		}
			//	}
			//}
			var kvMap = MibStringHelper.SplitManageValue(strMibValueList);
			if (isValueToDesc)
			{
				int nMibValue;
				if (int.TryParse(strMibValue, out nMibValue))
				{
					if (kvMap.ContainsKey(nMibValue))
					{
						strReValue = kvMap[nMibValue];
					}
				}
			}
			else
			{
				foreach (var item in kvMap)
				{
					if (item.Value.Equals(strMibValue))
					{
						strReValue = item.Key.ToString();
						break;
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
		private static bool GenerateBitsTypeDesc(string strBitsTypeValue, string strValueList, out string strOutput)
		{
			// 初始化
			strOutput = "";
			strBitsTypeValue = strBitsTypeValue.Trim();
			// "0"值
			if ("0".Equals(strBitsTypeValue))
			{
				strOutput = "无效";
				return true;
			}

			// 解析“0:时钟故障/1:传输故障”格式的字符串
			//var dicBit2Desc = new Dictionary<int, string>();
			//var keyValList = strValueList.Split('/');
			//foreach (var item in keyValList)
			//{
			//	var keyVal = item.Split(':');
			//	dicBit2Desc.Add(Convert.ToInt32(keyVal[0]), keyVal[1]);
			//}

			var dicBit2Desc = MibStringHelper.SplitManageValue(strValueList);
			// 将bits值转换为unsigned long
			var u32BitsTypeValue = Convert.ToUInt32(strBitsTypeValue);
			for (var i = 0; i < 32; i++)
			{
				var u32BitValue = (uint)1 << i;
				var u32Tmp = u32BitsTypeValue & u32BitValue;
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
		/// <param name="strNeIp"></param>
		/// <param name="oid"></param>
		/// <param name="strValue"></param>
		/// <param name="strName"></param>
		/// <param name="strValueDesc"></param>
		/// <param name="strUnitName"></param>
		public static bool GetInfoByOID(string strNeIp, string oid, string strValue, out string strName
			, out string strValueDesc, out string strUnitName)
		{
			strName = "";
			strValueDesc = "";
			strUnitName = "";

			// Mib前缀
			var strMibPrefix = SnmpToDatabase.GetMibPrefix();

			var strOidTmp = oid.Replace(strMibPrefix, "");

			// 获取节点信息
			var mibObjInfo = GetParentMibNodeByChildOID(strNeIp, strOidTmp);
			if (mibObjInfo == null)
			{
				Log.Error($"GetInfoByOID() 中根据OID:{strOidTmp}获取网元:{strNeIp}的MIB节点失败");
				return false;
			}

			if (strOidTmp.IndexOf(mibObjInfo.childOid, StringComparison.Ordinal) != 0)
			{
				// 如果MIB节点OID不是原OID的首个位置，则返回失败。
				Log.Error($"MIB节点OID: {mibObjInfo.childOid}不是原OID: {strOidTmp}的首个位置");
				return false;
			}

			// 组字符串
			var strTempDesc = mibObjInfo.mibDesc;
			strName = mibObjInfo.childNameMib;
			var strMibName = strName;

			// 描述截取,格式：告警箱版本(字符串长度1~255(字节))(初配值："")
			var index = strTempDesc.IndexOf('(');
			if (index > 0)
			{
				strTempDesc = strTempDesc.Substring(0, index);
			}
			strName = $"{strTempDesc}({strName})";

			// 值的实际意义,使用TranslateMibValue函数来解析值的描述, 其中包含BITS类型的支持
			strValueDesc = strValue;

			// 值的中文描述
			string strReValue;
			if (!TranslateMibValue(strNeIp, strMibName, strValueDesc, out strReValue))
			{
				Log.Error($"TranslateMibValue函数返回失败, 参数: {strMibName}, {strValueDesc}");
			}
			// 获取到的值的中文描述
			strValueDesc = strReValue;

			// 获取单位
			strUnitName = CommFuns.ParseMibUnit(mibObjInfo.mibDesc);

			// 获取索引
			var strIndex = strOidTmp;
			strIndex = strIndex.Replace(mibObjInfo.childOid, "");
			if (mibObjInfo.IsTable) // 是表显示取索引
			{
				strIndex = strIndex.TrimStart('.');
				strName = $"{strName}{CommString.IDS_INSTANCE}{strIndex}";
			}

			return true;
		}

		/// <summary>
		/// 根据Oid获取实例的Mib信息
		/// </summary>
		/// <param name="strNeIp"></param>
		/// <param name="strMibOid"></param>
		/// <returns></returns>
		public static MibLeaf GetMibNodeInfoByOID(string strNeIp, string strMibOid)
		{
			MibLeaf reData;

			string strError;
			if (!Database.GetInstance().GetMibDataByOid(strMibOid, out reData, strNeIp, out strError))
			{
				reData = null;
			}

			return reData;
		}

		/// <summary>
		/// 通过子节点的OID来查找父节点
		/// </summary>
		/// <param name="strNeIp"></param>
		/// <param name="strChildOid"></param>
		/// <returns></returns>
		private static MibLeaf GetParentMibNodeByChildOID(string strNeIp, string strChildOid)
		{
			if (string.IsNullOrEmpty(strChildOid))
			{
				return null;
			}

			var strParentOid = strChildOid;
			var index = strParentOid.LastIndexOf('.');

			while (index > 0)
			{
				strParentOid = strParentOid.Substring(0, index);

				var reData = GetMibNodeInfoByOID(strNeIp, strParentOid);
				if (reData != null)
				{
					return reData;
				}

				index = strParentOid.LastIndexOf('.');
			}

			return null;
		}

		/// <summary>
		/// Add By Mayi
		/// </summary>
		/// <returns></returns>
		public static string GetNodeTypeByOIDInCache(string strNeIp, string strOid)
		{
			// Mib前缀
			var strMibPrefix = SnmpToDatabase.GetMibPrefix();
			// 去掉Oid的Mib前缀
			var strOidTmp = strOid.Replace(strMibPrefix, "");

			var mibLeaf = GetParentMibNodeByChildOID(strNeIp, strOidTmp);

			return mibLeaf?.mibSyntax;
		}
	}
}