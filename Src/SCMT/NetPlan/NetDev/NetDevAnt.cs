using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LinkPath;
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
		/// 在ant中填入天线阵的信息
		/// </summary>
		/// <param name="ant"></param>
		/// <param name="at"></param>
		/// <returns></returns>
		public static bool SetAntTypeInfo(DevAttributeBase ant, AntType at)
		{
			ant.SetFieldOriginValue("netAntArrayVendorName", at.antArrayNotMibVendorName);
			ant.SetFieldOriginValue("netAntArrayModel", at.antArrayModelName);

			if (at.antArrayType.Count > 0)
			{
				ant.SetFieldOriginValue("netAntArrayType", at.antArrayType.First().value, true);
			}

			ant.SetFieldOriginValue("netAntArrayNum", at.antArrayNum);
			ant.SetFieldOriginValue("netAntArrayDistance", at.antArrayDistance);

			return true;
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
				if (AntWcbOpType.skip == op) return true;

				var cmdType = GetSnmCmdTypeFromWcbOpType(op);

				// 生成该天线阵的wcb信息
				var gret = GenerateAntWcbInfo(ant);
				if (-1 == gret)
				{
					return false;
				}

				if (1 == gret)
				{
					return true;
				}

				// 下发天线阵的wcb信息
				if (!SendWcbInfoToEnb(cmdType))
				{
					return false;
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

		/// <summary>
		/// 生成天线阵的wcb信息
		/// </summary>
		/// <param name="ant"></param>
		/// <returns>-1：异常错误，0：成功，1：用户放弃</returns>
		private int GenerateAntWcbInfo(DevAttributeInfo ant)
		{
			var mapKv = new Dictionary<string, string>
			{
				["netAntArrayTypeIndex"] = null,
				["netAntArrayVendorIndex"] = null
			};

			if (!ant.GetNeedUpdateValue(mapKv))
			{
				Log.Error($"查询索引为{ant.m_strOidIndex}天线阵的厂家和类型索引失败，不下发该天线阵的权重信息");
				return -1;
			}

			var strAntNo = ant.m_strOidIndex.TrimStart('.');

			var vi = mapKv["netAntArrayVendorIndex"];
			var ti = mapKv["netAntArrayTypeIndex"];

			// 只有大于8通道的天线阵才执行下面的操作
			var at = NPEAntHelper.GetInstance().GetAntTypeByVendorAndTypeIdx(vi, ti);
			if (null == at)
			{
				Log.Error($"根据厂家索引{vi}和天线阵类型索引{ti}获取天线阵器件库信息失败");
				return -1;
			}

			if (at.antArrayNum < 8)
			{
				Log.Debug($"索引为{ant.m_strOidIndex}的天线阵通道数量小于8，不予下发天线阵权重信息");
				return 1;
			}

			var antWeight = NPEAntHelper.GetInstance().GetAntWeightByNo(vi, ti);
			if (null != antWeight)
			{
				// 生成权重信息
				GenerateAntWeigthDev(antWeight, strAntNo);
			}
			else
			{
				Log.Error($"根据厂家编号{vi}和类型索引{ti}获取天线阵权重信息失败");
				return -1;
			}

			// 生成耦合系数
			var antCoupling = NPEAntHelper.GetInstance().GetCouplingByAntVendorAndType(vi, ti);
			if (null != antCoupling)
			{
				GenerateAntCoupling(antCoupling, strAntNo);
			}
			else
			{
				Log.Error($"根据厂家编号{vi}和类型索引{ti}获取天线阵耦合系数信息失败");
				return 0;	// 只有部分天线阵才有耦合系数
			}

			// todo 波束宽度扫描信息后续添加

			return 0;
		}

		/// <summary>
		/// 生成天线阵权重
		/// </summary>
		/// <param name="aw"></param>
		/// <param name="strAntNo"></param>
		/// <returns></returns>
		private void GenerateAntWeigthDev(AntWeight aw, string strAntNo)
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
		private void GenerateAntCoupling(AntCoupCoe coupCoe, string strAnoNo)
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

		/// <summary>
		/// 下发天线器件库信息
		/// </summary>
		/// <param name="ant"></param>
		public bool DistributeAntTypeInfo(DevAttributeInfo ant)
		{
			var strVendor = GetAntVendorIdx(ant);
			var strType = GetAntTypeIdx(ant);
			if (String.IsNullOrEmpty(strVendor)|| String.IsNullOrEmpty(strType))
			{
				Log.Error($"索引为{ant.m_strOidIndex}的天线阵信息查找厂家索引和类型索引失败");
				return false;
			}

			if (IsExistAntType(strVendor, strType))
			{
				Log.Debug($"已经存在索引为.{strVendor}.{strType}的天线阵器件实例，无需下发天线阵器件库信息");
				return true;
			}

			var antStaticInfo = NPEAntHelper.GetInstance().GetAntTypeByVendorAndTypeIdx(strVendor, strType);
			if (null == antStaticInfo)
			{
				Log.Error($"查找厂家索引为{strVendor}、类型索引为{strType}的天线阵器件库信息失败");
				return false;
			}

			var dev = GenerateAntTypeDev(antStaticInfo);
			if (null == dev)
			{
				Log.Error("生成天线阵类型实例失败");
				return false;
			}

			// 下发信息到基站
			if (!MibInfoMgr.DistributeSnmpData(dev, EnumSnmpCmdType.Add, CSEnbHelper.GetCurEnbAddr()))
			{
				Log.Error($"下发索引为{ant.m_strOidIndex}的天线阵的器件库信息失败");
				return false;
			}

			return true;
		}


		private bool IsExistAntType(string strVendorIdx, string strTypeIdx)
		{
			var idx = $".{strVendorIdx}.{strTypeIdx}";
			var rs = CommLinkPath.GetMibValueFromCmdExeResult(idx, "GetAntennaArrayTypeInfo", "antArrayRowStatus",
				CSEnbHelper.GetCurEnbAddr());
			return "4" == rs;
		}

		private string GetAntVendorIdx(DevAttributeInfo ant)
		{
			return ant.GetNeedUpdateValue("netAntArrayVendorIndex");
		}

		private string GetAntTypeIdx(DevAttributeInfo ant)
		{
			return ant.GetNeedUpdateValue("netAntArrayTypeIndex");
		}

		private DevAttributeBase GenerateAntTypeDev(AntType at)
		{
			var idx = $".{at.antArrayVendor}.{at.antArrayIndex}";
			var dev = new DevAttributeBase("antennaArrayTypeEntry", idx);
			if (dev.m_mapAttributes.Count == 0)
			{
				return null;
			}

			dev.SetFieldOriginValue("antArrayModelName", at.antArrayModelName);
			dev.SetFieldOriginValue("antArrayType", CalculateBitsValue(at.antArrayType).ToString());
			dev.SetFieldOriginValue("antArrayNum", at.antArrayNum.ToString());
			dev.SetFieldOriginValue("antArrayDistance", at.antArrayDistance.ToString());

			return dev;
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
