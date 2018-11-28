using System.Collections.Generic;
using System.Linq;
using CommonUtility;
using LinkPath;
using LogManager;

namespace NetPlan
{
	internal sealed class NetDevRru : NetDevBase
	{
		/// <summary>
		/// 从pico中查询连接的rhub的编号和端口号
		/// </summary>
		/// <param name="picoDev"></param>
		/// <returns>key:pico连接rhub的端口号，value:rhub到pico的连接点信息</returns>
		public static Dictionary<int, LinkEndpoint> GetLinkedRhubInfoFromPico(DevAttributeInfo picoDev)
		{
			var rhubNoMib = "netRRUHubNo";
			var rhubNo = picoDev.GetNeedUpdateValue(rhubNoMib);
			if (null == rhubNo || "-1" == rhubNo)
			{
				Log.Debug($"索引为{picoDev.m_strOidIndex}pico设备尚未连接到rhub，请确认是否存在错误");
				return null;
			}

			var epMap = new Dictionary<int, LinkEndpoint>();

			for (var i = 1; i <= 2; i++)        // todo pico设备按两个端口算，如果MIB有修改，需要进行处理
			{
				var rhubEthMib = $"netRRUOfp{i}AccessEthernetPort";
				var rhubEthNo = picoDev.GetNeedUpdateValue(rhubEthMib);
				if (null == rhubEthNo || "-1" == rhubEthNo)
				{
					continue;
				}

				int rhubPort;
				if (!int.TryParse(rhubEthNo, out rhubPort))
				{
					Log.Error($"索引为{picoDev.m_strOidIndex}pico设备在端口{i}连接的rhub端口号配置错误，值：{rhubEthNo}");
					continue;
				}

				var ep = new LinkEndpoint
				{
					devType = EnumDevType.rhub,
					strDevIndex = $".{rhubNo}",
					portType = EnumPortType.rhub_to_pico,
					nPortNo = rhubPort
				};

				epMap.Add(i, ep);
			}

			return epMap;
		}

		// todo 下发rruTypeEntry和rruTypePortEntry

		#region 器件库信息处理

		/// <summary>
		/// 根据rru的信息生成和该rru相关的器件库信息
		/// </summary>
		/// <param name="rru"></param>
		/// <returns></returns>
		public bool DistributeRruTypeInfo(DevAttributeInfo rru)
		{
			var strVendor = GetRruVendorIdx(rru);
			var strType = GetRruTypeIdx(rru);
			if (string.IsNullOrEmpty(strVendor) || string.IsNullOrEmpty(strType))
			{
				return false;
			}

			if (IsExistRruType(strVendor, strType))
			{
				Log.Debug($"厂家编号为{strVendor}、类型索引为{strType}的RRU器件库信息已经存在，无需下发");
				return true;
			}

			var rruStaticInfo = NPERruHelper.GetInstance().GetRruTypeInfoByTypeAndVendorIdx(strType, strVendor);
			if (null == rruStaticInfo)
			{
				Log.Error($"根据厂家编号{strVendor}和类型编号{strType}查询RRU器件库信息失败");
				return false;
			}

			var newRruType = GenerateRruTypeDev(rruStaticInfo);
			if (null == newRruType)
			{
				Log.Error("生成rru器件库信息失败");
				return false;
			}

			// 下发参数
			if (!MibInfoMgr.DistributeSnmpData(newRruType, EnumSnmpCmdType.Add, CSEnbHelper.GetCurEnbAddr()))
			{
				Log.Error($"厂家编号为{strVendor}、类型索引为{strType}的RRU器件库信息下发失败");
				return false;
			}

			return true;
		}

		public bool DistributeRruPortTypeInfo(DevAttributeInfo rru)
		{
			var strVendor = GetRruVendorIdx(rru);
			var strType = GetRruTypeIdx(rru);
			if (string.IsNullOrEmpty(strVendor) || string.IsNullOrEmpty(strType))
			{
				return false;
			}

			if (IsExistRruPortType(strVendor, strType))
			{
				Log.Debug($"根据厂家编号{strVendor}和类型编号{strType}查询RRU端口器件库信息失败");
				return true;
			}

			var rpiList = NPERruHelper.GetInstance().GetRruPathInfoByTypeAndVendor(int.Parse(strType), int.Parse(strVendor));
			var rpiDevList = GenerateRruPortTypeDev(rpiList);
			if (null == rpiDevList || rpiDevList.Count == 0)
			{
				Log.Error($"厂家编号为{strVendor}、类型索引为{strType}的RRU端口器件库信息生成失败");
				return false;
			}

			var targetIp = CSEnbHelper.GetCurEnbAddr();
			// 下发参数
			foreach (var item in rpiDevList)
			{
				if (!MibInfoMgr.DistributeSnmpData(item, EnumSnmpCmdType.Add, targetIp))
				{
					Log.Error($"索引为{item.m_strOidIndex}的RRU端口器件库信息下发失败");
					continue;	// todo 此处使用continue而不是return，是因为判断端口器件库信息是否存在时，只判断了端口1的信息，不能保证所有的信息都已经被删除
				}
			}

			return true;
		}

		/// <summary>
		/// 查询rru设备的厂家编号
		/// </summary>
		/// <param name="rru"></param>
		/// <returns></returns>
		private string GetRruVendorIdx(DevAttributeInfo rru)
		{
			return rru.GetNeedUpdateValue("netRRUManufacturerIndex");
		}

		private string GetRruTypeIdx(DevAttributeInfo rru)
		{
			return rru.GetNeedUpdateValue("netRRUTypeIndex");
		}

		/// <summary>
		/// 判断给定厂家编号和类型编号的rru器件库实例是否存在
		/// </summary>
		/// <param name="strVendorIdx"></param>
		/// <param name="strTypeIdx"></param>
		/// <returns></returns>
		private static bool IsExistRruType(string strVendorIdx, string strTypeIdx)
		{
			var idx = $".{strVendorIdx}.{strTypeIdx}";
			var rs = CommLinkPath.GetMibValueFromCmdExeResult(idx, "GetRRUTypeInfo", "rruTypeRowStatus", CSEnbHelper.GetCurEnbAddr());
			return ("4" == rs);
		}

		/// <summary>
		/// 判断是否存在rru端口器件信息。todo 只判定一个端口的信息
		/// </summary>
		/// <param name="strVendorIdx"></param>
		/// <param name="strTypeIdx"></param>
		/// <returns></returns>
		private static bool IsExistRruPortType(string strVendorIdx, string strTypeIdx, int nPortNo = 1)
		{
			var idx = $".{strVendorIdx}.{strTypeIdx}.{nPortNo}";
			var rs = CommLinkPath.GetMibValueFromCmdExeResult(idx, "GetRRUTypePortInfo", "rruTypePortRowStatus",
				CSEnbHelper.GetCurEnbAddr());
			return ("4" == rs);
		}

		/// <summary>
		/// 生成一个rru类型实例，准备下发
		/// </summary>
		/// <param name="ri"></param>
		/// <returns></returns>
		private static DevAttributeBase GenerateRruTypeDev(RruInfo ri)
		{
			var idx = $".{ri.rruTypeManufacturerIndex}.{ri.rruTypeIndex}";
			var newRruType = new DevAttributeBase("rruTypeEntry", idx);
			if (newRruType.m_mapAttributes.Count == 0)
			{
				return null;
			}

			newRruType.SetFieldOriginValue("rruTypeName", ri.rruTypeName);
			newRruType.SetFieldOriginValue("rruTypeMaxAntPathNum", ri.rruTypeMaxAntPathNum.ToString());
			newRruType.SetFieldOriginValue("rruTypeMaxTxPower", ri.rruTypeMaxTxPower.ToString());
			newRruType.SetFieldOriginValue("rruTypeBandWidth", ri.rruTypeBandWidth.ToString());
			newRruType.SetFieldOriginValue("rruTypeFiberLength", CalculateBitsValue(ri.rruTypeFiberLength).ToString());
			newRruType.SetFieldOriginValue("rruTypeIrCompressMode", CalculateBitsValue(ri.rruTypeIrCompressMode).ToString());
			newRruType.SetFieldOriginValue("rruTypeSupportCellWorkMode", CalculateBitsValue(ri.rruTypeSupportCellWorkMode).ToString());

			return newRruType;
		}

		/// <summary>
		/// 生成一个rru端口类型实例
		/// </summary>
		/// <param name="rpi"></param>
		/// <returns></returns>
		private static List<DevAttributeBase> GenerateRruPortTypeDev(IEnumerable<RruPortInfo> rpiList)
		{
			var ndabList = new List<DevAttributeBase>();
			foreach (var rpi in rpiList)
			{
				var idx = $".{rpi.rruTypePortManufacturerIndex}.{rpi.rruTypePortIndex}.{rpi.rruTypePortNo}";
				var newRpt = new DevAttributeBase("rruTypePortEntry", idx);
				if (newRpt.m_mapAttributes.Count == 0)
				{
					continue;
				}

				newRpt.SetFieldOriginValue("rruTypePortPathNo", rpi.rruTypePortPathNo.ToString());
				newRpt.SetFieldOriginValue("rruTypePortSupportFreqBand", CalculateBitsValue(rpi.rruTypePortSupportFreqBand).ToString());
				newRpt.SetFieldOriginValue("rruTypePortSupportFreqBandWidth", rpi.rruTypePortSupportFreqBandWidth.ToString());
				newRpt.SetFieldOriginValue("rruTypePortSupportAbandTdsCarrierNum", rpi.rruTypePortSupportAbandTdsCarrierNum.ToString());
				newRpt.SetFieldOriginValue("rruTypePortSupportFBandTdsCarrierNum", rpi.rruTypePortSupportFBandTdsCarrierNum.ToString());
				newRpt.SetFieldOriginValue("rruTypePortCalAIqTxNom", rpi.rruTypePortCalAIqTxNom.ToString());
				newRpt.SetFieldOriginValue("rruTypePortCalAIqRxNom", rpi.rruTypePortCalAIqRxNom.ToString());
				newRpt.SetFieldOriginValue("rruTypePortCalPoutTxNom", rpi.rruTypePortCalPoutTxNom.ToString());
				newRpt.SetFieldOriginValue("rruTypePortCalPinRxNom", rpi.rruTypePortCalPinRxNom.ToString());
				newRpt.SetFieldOriginValue("rruTypePortAntMaxPower", rpi.rruTypePortAntMaxPower.ToString());

				ndabList.Add(newRpt);
			}

			return ndabList;
		}


		#endregion 器件库信息处理
	}
}