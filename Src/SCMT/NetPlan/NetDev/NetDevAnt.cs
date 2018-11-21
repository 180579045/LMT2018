using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LogManager;


/// <summary>
/// 天线阵设备
/// </summary>
namespace NetPlan
{
	internal class NetDevAnt : NetDevBase
	{
		/// <summary>
		/// 下发天线阵设备信息到基站
		/// </summary>
		/// <returns></returns>
		internal override bool DistributeDevToEnb(DevAttributeInfo dev)
		{
			return base.DistributeDevToEnb(dev);
		}

		/// <summary>
		/// 下发天线阵信息
		/// </summary>
		/// <param name="ant">天线阵设备</param>
		/// <param name="bSendWcb">是否下发天线阵权重、耦合系数、波束扫描等信息</param>
		/// <returns></returns>
		internal bool DistributeDevToEnb(DevAttributeInfo ant, bool bSendWcb)
		{
			if (bSendWcb)
			{
				var op = GetWcbOpType(ant);
				if (AntWcbOpType.skip != op)
				{
					var cmdType = GetSnmCmdTypeFromWcbOpType(op);

					// 生成该天线阵的wcb信息
					if (!GenerateAntWcbInfo(ant))
					{
						return false;
					}

					// 下发天线阵的wcb信息
					if (!SendWcbInfoToEnb(cmdType))
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// 根据天线阵的信息，判断wcb信息如果操作
		/// ant状态为newadd，则直接调用AddAntennaWeightMultAntInfo命令
		/// ant状态为original，直接跳过，不处理
		/// ant状态为modify，需要判断天线阵类型是否被修改了，如果修改了类型，则调用SetAntennaWeightMultAntInfo命令
		/// ant状态为waitdel，直接删除
		/// </summary>
		/// <param name="ant"></param>
		/// <returns></returns>
		private AntWcbOpType GetWcbOpType(DevAttributeInfo ant)
		{
			switch (ant.m_recordType)
			{
				case RecordDataType.Original:
					return AntWcbOpType.skip;
				case RecordDataType.NewAdd:
					return AntWcbOpType.only_add;
				case RecordDataType.WaitDel:
					return AntWcbOpType.only_del;
				case RecordDataType.Modified:
					if (!IsSameTypeAntWithPrevious(ant))
					{
						return AntWcbOpType.only_set;
					}
					break;
			}

			return AntWcbOpType.skip;
		}

		/// <summary>
		/// 判断天线阵是否是原来的天线阵
		/// </summary>
		/// <param name="ant"></param>
		/// <returns></returns>
		private bool IsSameTypeAntWithPrevious(DevAttributeInfo ant)
		{
			var otype = ant.GetFieldOriginValue("netAntArrayTypeIndex");
			var ovendor = ant.GetFieldOriginValue("netAntArrayVendorIndex");
			var ltype = ant.GetFieldLatestValue("netAntArrayTypeIndex");
			var lvendor = ant.GetFieldLatestValue("netAntArrayVendorIndex");
			return otype == ltype && ovendor == lvendor;
		}


		private bool GenerateAntWcbInfo(DevAttributeInfo ant)
		{
			var mapKv = new Dictionary<string, string>
			{
				["netAntArrayTypeIndex"] = null,
				["netAntArrayVendorIndex"] = null
			};

			if (!MibInfoMgr.GetNeedUpdateValue(ant, mapKv))
			{
				Log.Error($"查询索引为{ant.m_strOidIndex}天线阵的厂家和类型索引失败，不下发该天线阵的权重信息");
				return false;
			}

			var strAntNo = ant.m_strOidIndex.TrimStart('.');

			var vi = mapKv["netAntArrayVendorIndex"];
			var ti = mapKv["netAntArrayTypeIndex"];
			var antWeight = NPEAntHelper.GetInstance().GetAntWeightByNo(vi, ti);
			if (null != antWeight)
			{
				// 生成权重信息
				GeneralAntWeigthDev(antWeight, strAntNo);
			}
			else
			{
				Log.Error($"根据厂家编号{vi}和类型索引{ti}获取天线阵权重信息失败");
				return false;
			}

			// 生成耦合系数
			var antCoupling = NPEAntHelper.GetInstance().GetCouplingByAntVendorAndType(vi, ti);
			if (null != antCoupling)
			{
				GeneralAntCoupling(antCoupling, strAntNo);
			}
			else
			{
				Log.Error($"根据厂家编号{vi}和类型索引{ti}获取天线阵耦合系数信息失败");
				return false;
			}

			// todo 波束宽度扫描信息后续添加

			return true;
		}


		/// <summary>
		/// 生成天线阵权重
		/// </summary>
		/// <param name="aw"></param>
		/// <param name="strAntNo"></param>
		/// <returns></returns>
		private void GeneralAntWeigthDev(AntWeight aw, string strAntNo)
		{
			var witList = aw.antArrayMultWeight;

			foreach (var item in witList)
			{
				var fb = item.antennaWeightMultFrequencyBand;
				var gi = item.antennaWeightMultAntGrpIndex;
				var si = item.antennaWeightMultAntStatusIndex;
				var idx = $".{strAntNo}.{fb}.{gi}.{si}";
				var dev = new DevAttributeInfo(EnumDevType.antWeight, idx);
				if (dev.m_mapAttributes.Count == 0)
				{
					continue;
				}

				dev.SetFieldOriginValue("antennaWeightMultAntHalfPowerBeamWidth", item.antennaWeightMultAntHalfPowerBeamWidth.ToString());
				dev.SetFieldOriginValue("antennaWeightMultAntVerHalfPowerBeamWidth", item.antennaWeightMultAntVerHalfPowerBeamWidth.ToString());

				dynamic amplitudeValue;
				dynamic phaseValue;

				for (var i = 0; i < 8; i++)
				{
					var amp = $"antennaWeightMultAntAmplitude{i}";
					var pha = $"antennaWeightMultAntPhase{i}";
					if (!UtilityHelper.GetFieldValueOfType<Weight>(item, amp, out amplitudeValue) ||
						!UtilityHelper.GetFieldValueOfType<Weight>(item, pha, out phaseValue))
					{
						break;
					}

					dev.SetFieldOriginValue(amp, amplitudeValue.ToString());
					dev.SetFieldOriginValue(pha, phaseValue.ToString());
				}

				SaveWcbDev(EnumDevType.antWeight, dev);
			}
		}


		/// <summary>
		/// 生成天线阵的耦合系数
		/// </summary>
		/// <param name="coupCoe"></param>
		/// <param name="strAnoNo"></param>
		private void GeneralAntCoupling(AntCoupCoe coupCoe, string strAnoNo)
		{
			var coupList = coupCoe.antArrayCouplingCoeffctInfo;

			foreach (var item in coupList)
			{
				var fb = item.antCouplCoeffFreq;
				var gi = item.antCouplCoeffAntGrpIndex;
				var idx = $".{strAnoNo}.{fb}.{gi}";

				var dev = new DevAttributeInfo(EnumDevType.antCoup, idx);
				if (dev.m_mapAttributes.Count == 0)
				{
					continue;
				}

				dynamic amplitudeValue;
				dynamic phaseValue;

				for (var i = 0; i < 8; i++)
				{
					var amp = $"antCouplCoeffAmplitude{i}";
					var pha = $"antCouplCoeffPhase{i}";
					if (!UtilityHelper.GetFieldValueOfType<Weight>(item, amp, out amplitudeValue) ||
						!UtilityHelper.GetFieldValueOfType<Weight>(item, pha, out phaseValue))
					{
						break;
					}

					dev.SetFieldOriginValue(amp, amplitudeValue.ToString());
					dev.SetFieldOriginValue(pha, phaseValue.ToString());
				}

				SaveWcbDev(EnumDevType.antCoup, dev);
			}
		}


		private bool SaveWcbDev(EnumDevType type, DevAttributeInfo dev)
		{
			List<DevAttributeInfo> refList = null;
			switch (type)
			{
				case EnumDevType.antWeight:
					refList = m_antWeightList ?? new List<DevAttributeInfo>();
					break;
				case EnumDevType.antCoup:
					refList = m_antCouplingList ?? new List<DevAttributeInfo>();
					break;
				case EnumDevType.antBfScan:
					refList = m_antBfScanList ?? new List<DevAttributeInfo>();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}

			refList.Add(dev);

			return true;
		}

		/// <summary>
		/// 下发天线阵的wcb信息到基站
		/// </summary>
		/// <param name="opType"></param>
		/// <returns></returns>
		private bool SendWcbInfoToEnb(EnumSnmpCmdType cmdType)
		{
			var targetIp = CSEnbHelper.GetCurEnbAddr();
			if (null == targetIp)
			{
				Log.Error("尚未选中基站");
				return false;
			}

			return m_antWeightList.All(item => MibInfoMgr.DistributeSnmpData(item, cmdType, targetIp)) &&
			       m_antCouplingList.All(item => MibInfoMgr.DistributeSnmpData(item, cmdType, targetIp)) &&
				   m_antBfScanList.All(item => MibInfoMgr.DistributeSnmpData(item, cmdType, targetIp));
		}


		#region 私有数据区

		private List<DevAttributeInfo> m_antWeightList;
		private List<DevAttributeInfo> m_antCouplingList;
		private List<DevAttributeInfo> m_antBfScanList;

		#endregion
	}

	internal enum AntWcbOpType
	{
		skip = 0,
		only_add,
		only_del,
		only_set,
	}

}
