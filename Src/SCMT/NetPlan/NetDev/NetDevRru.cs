using System.Collections.Generic;
using CommonUtility;
using LinkPath;
using LogManager;

namespace NetPlan
{
	internal sealed class NetDevRru : NetDevBase
	{
		#region 构造函数

		internal NetDevRru(string strTargetIp, NPDictionary mapOriginData) : base(strTargetIp, mapOriginData)
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
				if (!DisRelateRass(dev))
				{
					Log.Error($"和索引为{dev.m_strOidIndex}RRU设备相关的天线阵安装规划表下发失败");
					return false;
				}
				Log.Debug($"和索引为{dev.m_strOidIndex}RRU设备相关的天线阵安装规划表下发成功");

				if (!DistributeDownLinkRecord(dev))
				{
					Log.Error($"和索引为{dev.m_strOidIndex}RRU设备相关的IR口规划或以太网口规划下发失败");
					return false;
				}
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
		/// <returns>key:pico连接rhub的端口号，value:rhub上的连接点信息</returns>
		public static Dictionary<int, LinkEndpoint> GetLinkedRhubInfoFromPico(DevAttributeBase picoDev)
		{
			var rhubNoMib = "netRRUHubNo";
			var rhubNo = picoDev.GetFieldOriginValue(rhubNoMib);
			if (null == rhubNo || "-1" == rhubNo)
			{
				Log.Debug($"索引为{picoDev.m_strOidIndex}pico设备尚未连接到rhub，请确认是否存在错误");
				return null;
			}

			var epMap = new Dictionary<int, LinkEndpoint>();

			for (var i = 1; i <= MagicNum.PICO_TO_RHUB_PORT_CNT; i++)
			{
				var rhubEthMib = $"netRRUOfp{i}AccessEthernetPort";
				var rhubEthNo = picoDev.GetFieldOriginValue(rhubEthMib);
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

		/// <summary>
		/// 从rru属性中，找到连接的bbu的信息。 todo 小心级联的rru
		/// </summary>
		/// <param name="rruDev"></param>
		/// <returns>null:rru没有连接到bbu;其他情况返回一个字典，key:rru的光口号，value:bbu端光口类型</returns>
		public static Dictionary<int, LinkEndpoint> GetLinkedBbuPortEpFromRru(DevAttributeBase rruDev)
		{
			// 两种情况：1.先删除rru到bbu的连接，再删除rru，此时查到的值为-1；2.直接删除rru设备，此时能够查到bbu的信息
			// 查询origin value，保证与基站中的一致，而不是使用latest value。
			var boardTypeInRru = rruDev.GetFieldOriginValue("netRRUAccessBoardType");
			if (null == boardTypeInRru || "-1" == boardTypeInRru)
			{
				// 说明rru没有连接到bbu  todo 部分数据下发失败，导致rru孤立的情况
				return null;
			}

			// 级联模式的rru，连接的板卡的信息和第1级rru连接板卡信息一样
			var resultMap = new Dictionary<int, LinkEndpoint>();

			var rackNo = rruDev.GetFieldOriginValue("netRRUAccessRackNo");
			var shelfNo = rruDev.GetFieldOriginValue("netRRUAccessShelfNo");
			for (var i = 1; i <= MagicNum.RRU_TO_BBU_PORT_CNT; i++)
			{
				var slotMib = GetToBoardSlotMib(i);
				var slotNo = rruDev.GetFieldOriginValue(slotMib);
				if ("-1" == slotNo)
				{
					continue;
				}

				var ofpPortMib = GetToBoardOfpPortMib(i);
				var ofpPort = rruDev.GetFieldOriginValue(ofpPortMib);
				if ("-1" == ofpPort)
				{
					continue;
				}

				//var linePosMib = GetLinePosMib(i);		// todo 此处暂未考虑级联的rru

				var boardIndex = $".{rackNo}.{shelfNo}.{slotNo}";
				var endpoint = new LinkEndpoint
				{
					devType = EnumDevType.board,
					strDevIndex = boardIndex,
					portType = EnumPortType.bbu_to_rru,
					nPortNo = int.Parse(ofpPort)
				};

				resultMap.Add(i, endpoint);
			}

			return resultMap;
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

			// 下发参数
			foreach (var item in rpiDevList)
			{
				if (!base.DistributeToEnb(item))
				{
					Log.Error($"索引为{item.m_strOidIndex}的RRU端口器件库信息下发失败");
					//continue;	// 此处使用continue而不是return，是因为判断端口器件库信息是否存在时，只判断了端口1的信息，不能保证所有的信息都已经被删除
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
		/// <param name="nPortNo"></param>
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

			newRruType.SetFieldLatestValue("rruTypeName", ri.rruTypeName);
			newRruType.SetFieldLatestValue("rruTypeMaxAntPathNum", ri.rruTypeMaxAntPathNum.ToString());
			newRruType.SetFieldLatestValue("rruTypeMaxTxPower", ri.rruTypeMaxTxPower.ToString());
			newRruType.SetFieldLatestValue("rruTypeBandWidth", ri.rruTypeBandWidth.ToString());
			newRruType.SetFieldLatestValue("rruTypeFiberLength", CalculateBitsValue(ri.rruTypeFiberLength).ToString());
			newRruType.SetFieldLatestValue("rruTypeIrCompressMode", CalculateBitsValue(ri.rruTypeIrCompressMode).ToString());
			newRruType.SetFieldLatestValue("rruTypeSupportCellWorkMode", CalculateBitsValue(ri.rruTypeSupportCellWorkMode).ToString());

			return newRruType;
		}

		/// <summary>
		/// 生成一个rru端口类型实例
		/// </summary>
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

				newRpt.SetFieldLatestValue("rruTypePortPathNo", rpi.rruTypePortPathNo.ToString());
				newRpt.SetFieldLatestValue("rruTypePortSupportFreqBand", CalculateBitsValue(rpi.rruTypePortSupportFreqBand).ToString());
				newRpt.SetFieldLatestValue("rruTypePortSupportFreqBandWidth", rpi.rruTypePortSupportFreqBandWidth.ToString());
				newRpt.SetFieldLatestValue("rruTypePortSupportAbandTdsCarrierNum", rpi.rruTypePortSupportAbandTdsCarrierNum.ToString());
				newRpt.SetFieldLatestValue("rruTypePortSupportFBandTdsCarrierNum", rpi.rruTypePortSupportFBandTdsCarrierNum.ToString());
				newRpt.SetFieldLatestValue("rruTypePortCalAIqTxNom", rpi.rruTypePortCalAIqTxNom.ToString());
				newRpt.SetFieldLatestValue("rruTypePortCalAIqRxNom", rpi.rruTypePortCalAIqRxNom.ToString());
				newRpt.SetFieldLatestValue("rruTypePortCalPoutTxNom", rpi.rruTypePortCalPoutTxNom.ToString());
				newRpt.SetFieldLatestValue("rruTypePortCalPinRxNom", rpi.rruTypePortCalPinRxNom.ToString());
				newRpt.SetFieldLatestValue("rruTypePortAntMaxPower", rpi.rruTypePortAntMaxPower.ToString());

				ndabList.Add(newRpt);
			}

			return ndabList;
		}


		#endregion 器件库信息处理

		#region 特殊处理流程

		/// <summary>
		/// 调用SetNetRRUAntennaLcID命令，修改
		/// 1.先删除了本地小区规划，未下发；2.删除天线阵，下发参数。下发失败，原因：该射频通道已经布配本地小区
		/// </summary>
		/// <param name="waitDisDev"></param>
		/// <returns></returns>
		private bool DisRelateRass(DevAttributeBase waitDisDev)
		{
			if (!m_mapOriginData.ContainsKey(EnumDevType.rru_ant))
			{
				return true;
			}

			var rruNo = waitDisDev.m_strOidIndex.Trim('.');
			var listRas = m_mapOriginData[EnumDevType.rru_ant];
			if (null == listRas || listRas.Count == 0)
			{
				return true;
			}

			var listRelateRas = LinkRruAnt.GetRecordsByRruNo(rruNo, listRas);
			return PreDelAntSettingRecord(listRelateRas);
		}

		/// <summary>
		/// 下发和rru关联的ir口规划记录
		/// </summary>
		/// <param name="mapBoardLinkEp">和待删除RRU连接的board信息</param>
		/// <returns></returns>
		private bool DistributeRelateIrRecord(Dictionary<int, LinkEndpoint> mapBoardLinkEp)
		{
			if (null == mapBoardLinkEp)
			{
				return true;
			}

			foreach (var item in mapBoardLinkEp)
			{
				var irRecordIndex = $"{item.Value.strDevIndex}.{item.Value.nPortNo}";
				var irRecord = GetDev(EnumDevType.board_rru, irRecordIndex);
				if (null == irRecord)
				{
					continue;
				}

				if (irRecord.m_recordType != RecordDataType.WaitDel)
				{
					continue;
				}

				if (!DistributeToEnb(irRecord, "DelIROfpPortInfo", "6"))
				{
					Log.Error($"下发删除索引为{irRecord.m_strOidIndex}IR口规划操作失败");
					return false;
				}

				m_mapOriginData[EnumDevType.board_rru].Remove((DevAttributeInfo)irRecord);
			}

			return true;
		}

		/// <summary>
		/// 下发和rru关联的以太网口规划记录
		/// </summary>
		/// <param name="mapRhubLinkEp"></param>
		/// <param name="rruDev"></param>
		/// <returns></returns>
		private bool DistributeRelateEthRecord(Dictionary<int, LinkEndpoint> mapRhubLinkEp, DevAttributeBase rruDev)
		{
			if (null == mapRhubLinkEp || mapRhubLinkEp.Count == 0)
			{
				return true;
			}

			foreach (var item in mapRhubLinkEp)
			{
				var rruPort = item.Key;
				var slotMib = GetToBoardSlotMib(rruPort);
				var slotNo = rruDev.GetFieldOriginValue(slotMib);
				if (null == slotNo || "-1" == slotNo)
				{
					continue;
				}

				var ethRecordIdx = $".0.0.{slotNo}.{item.Value.strDevIndex.Trim('.')}.{item.Value.nPortNo}";
				var ethRecord = GetDev(EnumDevType.rhub_prru, ethRecordIdx);
				if (null == ethRecord || (ethRecord.m_recordType != RecordDataType.WaitDel))
				{
					continue;
				}

				if (!DistributeToEnb(ethRecord, "DelEthPortInfo", "6"))
				{
					Log.Error($"下发删除索引为{ethRecord.m_strOidIndex}以太网口规划信息失败");
					return false;
				}

				m_mapOriginData[EnumDevType.rhub_prru].Remove((DevAttributeInfo)ethRecord);
			}

			return true;
		}

		/// <summary>
		/// 下发和rru相关的南向连接（到rhub或bbu的连接）记录信息
		/// </summary>
		/// <param name="rruDev"></param>
		/// <returns></returns>
		private bool DistributeDownLinkRecord(DevAttributeBase rruDev)
		{
			var rruType = rruDev.GetFieldOriginValue("netRRUTypeIndex");
			if (string.IsNullOrEmpty(rruType) || "-1" == rruType)
			{
				Log.Error("待删除rru的类型索引值无效，无法下发到rhub或bbu的连接");
				return false;
			}

			int rruT;
			if (!int.TryParse(rruType, out rruT))
			{
				Log.Error("待删除rru的类型索引值无效，无法下发到rhub或bbu的连接");
				return false;
			}

			//判断是否是pico设备
			var bIsPico = NPERruHelper.GetInstance().IsPicoDevice(rruT);
			if (bIsPico)
			{
				var listEp = GetLinkedRhubInfoFromPico(rruDev);
				return DistributeRelateEthRecord(listEp, rruDev);
			}
			else
			{
				var listEp = GetLinkedBbuPortEpFromRru(rruDev);
				return DistributeRelateIrRecord(listEp);
			}
		}


		private static string GetToBoardSlotMib(int i)
		{
			return (i == 1 ? "netRRUAccessSlotNo" : $"netRRUOfp{i}SlotNo");
		}

		private static string GetToBoardOfpPortMib(int i)
		{
			return $"netRRUOfp{i}AccessOfpPortNo";
		}

		private static string GetLinePosMib(int i)
		{
			return $"netRRUOfp{i}AccessLinePosition";
		}

		#endregion
	}
}