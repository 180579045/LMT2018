using System.Collections.Generic;
using System.Linq;
using CommonUtility;
using LinkPath;
using LogManager;
using NetPlan.DevLink;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;

namespace NetPlan
{
	internal sealed class NetDevRru : NetDevBase
	{
		#region 构造函数

		internal NetDevRru(string strTargetIp, MAP_DEVTYPE_DEVATTRI mapOriginData) : base(strTargetIp, mapOriginData)
		{

		}

		#endregion

		#region 虚函数重载

		internal override bool DistributeToEnb(DevAttributeBase dev, bool bDlAntWcb = false)
		{
			// 如果是新增的rru设备，需要先处理器件库
			if (RecordDataType.NewAdd == dev.m_recordType)
			{
				if (!DistributeRruTypeInfo(dev))
				{
					Log.Error($"下发索引为{dev.m_strOidIndex}的RRU器件库信息失败");
					return false;
				}

				if (!DistributeRruPortTypeInfo(dev))
				{
					Log.Error($"下发索引为{dev.m_strOidIndex}的RRU端口器件库信息失败");
					return false;
				}
			}

			// 如果是删除的rru设备，先下发天线阵安装规划表
			if (RecordDataType.WaitDel == dev.m_recordType)
			{
				if (!DisRelateRass(dev, m_mapOriginData))
				{
					Log.Error($"和索引为{dev.m_strOidIndex}RRU设备相关的天线阵安装规划表下发失败");
					return false;
				}
				Log.Debug($"和索引为{dev.m_strOidIndex}RRU设备相关的天线阵安装规划表下发成功");
			}

			// 特殊处理完成后，调用基类的函数下发rru的信息
			return base.DistributeToEnb(dev, bDlAntWcb);
		}

		#endregion


		#region 静态接口

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

			for (var i = 1; i <= MagicNum.PICO_TO_RHUB_PORT_CNT; i++)
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

		#endregion


		#region 器件库信息处理

		/// <summary>
		/// 根据rru的信息生成和该rru相关的器件库信息
		/// </summary>
		/// <param name="rru"></param>
		/// <returns></returns>
		public bool DistributeRruTypeInfo(DevAttributeBase rru)
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
			if (!base.DistributeToEnb(newRruType))
			{
				Log.Error($"厂家编号为{strVendor}、类型索引为{strType}的RRU器件库信息下发失败");
				return false;
			}

			return true;
		}

		public bool DistributeRruPortTypeInfo(DevAttributeBase rru)
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
				if (!base.DistributeToEnb(item))
				{
					Log.Error($"索引为{item.m_strOidIndex}的RRU端口器件库信息下发失败");
					continue;	// 此处使用continue而不是return，是因为判断端口器件库信息是否存在时，只判断了端口1的信息，不能保证所有的信息都已经被删除
				}
			}

			return true;
		}

		/// <summary>
		/// 查询rru设备的厂家编号
		/// </summary>
		/// <param name="rru"></param>
		/// <returns></returns>
		private string GetRruVendorIdx(DevAttributeBase rru)
		{
			return rru.GetNeedUpdateValue("netRRUManufacturerIndex");
		}

		private string GetRruTypeIdx(DevAttributeBase rru)
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

		#region 特殊处理流程

		/// <summary>
		/// 下发相关的天线阵安装规划表记录
		/// </summary>
		/// <param name="waitDisDev"></param>
		/// <param name="mapOriginData"></param>
		/// <returns></returns>
		private bool DisRelateRass(DevAttributeBase waitDisDev, MAP_DEVTYPE_DEVATTRI mapOriginData)
		{
			if (!mapOriginData.ContainsKey(EnumDevType.rru_ant))
			{
				return true;
			}

			var rruNo = waitDisDev.m_strOidIndex.Trim('.');
			var listRas = mapOriginData[EnumDevType.rru_ant];
			if (null == listRas || listRas.Count == 0)
			{
				return true;
			}

			var listRelateRas = LinkRruAnt.GetRecordsByRruNo(rruNo, listRas);
			foreach (var item in listRelateRas)
			{
				// todo 这里如果存在冲突该怎么搞？
				if (!LinkRruAnt.RruHasConnectToAnt(item))
				{
					Log.Debug($"索引为{item.m_strOidIndex}的天线安装规划记录没有配置天线阵的信息，忽略不再下发");
					continue;
				}

				if (!base.DistributeToEnb(item))
				{
					Log.Error($"索引为{item.m_strOidIndex}的天线阵安装规划表记录下发失败");
					return false;
				}
			}

			return true;
		}

		#endregion
	}
}