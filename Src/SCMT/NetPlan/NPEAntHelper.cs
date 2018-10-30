using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonUtility;

namespace NetPlan
{
	/// <inheritdoc />
	/// <summary>
	/// 网规天线阵相关信息
	/// </summary>
	public class NPEAntHelper : Singleton<NPEAntHelper>
	{
		#region 公共接口

		/// <summary>
		/// 获取所有天线类型信息
		/// </summary>
		/// <returns>返回一个字典，key:添加厂家字符串，value:该厂家下所有的天线阵列表</returns>
		public Dictionary<string, List<AntType>> GetAllAntTypeInfo()
		{
			if (null == _antInfo)
			{
				return null;
			}

			var mapAntInfo = new Dictionary<string, List<AntType>>();

			var antTypeList = _antInfo.antennaTypeTable;
			foreach (var antType in antTypeList)
			{
				antType.antInfoDesc = antType.FormatDesc();
				var wt = GetAntWeightByNo(antType.antArrayNotMibNumber);
				if (null != wt)
				{
					var feqDesc = wt.FormatSupportFeqBand();
					antType.antInfoDesc += $"支持频段：{feqDesc}";
				}

				if (mapAntInfo.ContainsKey(antType.antArrayNotMibVendorName))
				{
					mapAntInfo[antType.antArrayNotMibVendorName].Add(antType);
				}
				else
				{
					var listAnt = new List<AntType> {antType};
					mapAntInfo.Add(antType.antArrayNotMibVendorName, listAnt);
				}
			}

			return mapAntInfo;
		}

		public AntWeight GetAntWeightByNo(int antNo)
		{
			return _antInfo.antennaWeightTable.FirstOrDefault(antWeight => antNo == antWeight.antArrayNotMibNumber);
		}

		#endregion

		#region 私有接口、数据区

		private NPEAntHelper()
		{
			if (null == _antInfo)
			{
				try
				{
					var path = FilePathHelper.GetAppPath() + ConfigFileHelper.NetPlanAntJson;
					var jsonContent = FileRdWrHelper.GetFileContent(path, Encoding.UTF8);
					_antInfo = JsonHelper.SerializeJsonToObject<WholeAntInfo>(jsonContent);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}
		}

		private readonly WholeAntInfo _antInfo;

		#endregion
	}

	#region 天线阵解析相关类型定义

	public class WholeAntInfo
	{
		public List<AntType> antennaTypeTable;				// 天线类型
		public List<AntWeight> antennaWeightTable;			// 天线权重
		public List<AntCoupCoe> couplingCoeffctTable;		// 耦合系数

		public WholeAntInfo()
		{
			antennaTypeTable = new List<AntType>();
			antennaWeightTable = new List<AntWeight>();
			couplingCoeffctTable = new List<AntCoupCoe>();
		}
	}

	public class AntType
	{
		public int antArrayNotMibNumber;
		public int antArrayVendor;					// 厂家索引
		public string antArrayNotMibVendorName;		// 厂家文本描述
		public int antArrayIndex;					// 天线阵型号索引
		public string antArrayModelName;			// 天线阵型号
		public int antArrayNum;						// 天线阵根数
		public int antArrayDistance;				// 天线阵距离
		public List<VD> antArrayType;				// 天线阵形状
		public List<VD> antArrayNotMibAntLossFlag;
		public List<VD> netAntArrayNotMibHalfPowerBeamWidth;

		public string antInfoDesc;					// 天线阵信息简述

		public AntType()
		{
			antArrayType = new List<VD>();
			antArrayNotMibAntLossFlag = new List<VD>();
			netAntArrayNotMibHalfPowerBeamWidth = new List<VD>();
		}

		// 生成描述信息
		public string FormatDesc()
		{
			var sb = new StringBuilder();
			sb.AppendFormat("{0}天线信息简述：\n", antArrayModelName);
			sb.AppendFormat("天线阵厂家：{0}\n", antArrayNotMibVendorName);
			sb.AppendFormat("天线阵根数：{0:d}\n", antArrayNum);
			sb.AppendFormat("天线阵间距：{0:d} (精度：0.1mm)\n", antArrayDistance);

			var subSb = new StringBuilder();
			foreach (var item in antArrayType)
			{
				var enToDesc = MibStringHelper.SplitMibEnumString(item.desc);
				subSb.AppendFormat("{0},", enToDesc.Values.First());
			}

			sb.AppendFormat("天线阵形状：{0}\n", subSb.ToString().TrimEnd(','));

			subSb.Clear();
			return sb.ToString();
		}
	}

	public class AntWeight
	{
		public int antArrayNotMibNumber;
		public List<Weight> antArrayMultWeight;

		public AntWeight()
		{
			antArrayMultWeight = new List<Weight>();
		}

		// 格式化支持的天线阵频段
		public string FormatSupportFeqBand()
		{
			var feqBandMvr =
				"1:A频段(2010-2025)/2:D频段(2570-2620)/4:E频段(2300-2400)/8:F频段(1880-1920)/32:V频段(1447-1467)/64:B41频段(2496-2690)/128:T频段(1785-1805)/256:B03频段(1805-1880)/512:B01频段(2110-2170)/1024:B44频段(703-803)/2048:B42频段(3400-3600)/4096:N78频段(3400-3600)";
			var mapKv = MibStringHelper.SplitManageValue(feqBandMvr);

			var descList = new List<string>();
			foreach (var item in antArrayMultWeight)
			{
				var band = item.antennaWeightMultFrequencyBand;
				string bandDesc;
				if (mapKv.TryGetValue(band, out bandDesc))
				{
					descList.Add(bandDesc);
				}
			}

			descList = descList.Distinct().ToList();
			var sb = new StringBuilder();
			foreach (var item in descList)
			{
				sb.AppendFormat("{0},", item);
			}
			return sb.ToString().TrimEnd(',');
		}
	}

	public struct Weight
	{
		public int antennaWeightMultFrequencyBand;
		public int antennaWeightMultAntGrpIndex;
		public int antennaWeightMultAntStatusIndex;
		public string antennaWeightMultNotMibAntStatus;
		public int antennaWeightMultAntHalfPowerBeamWidth;
		public int antennaWeightMultAntVerHalfPowerBeamWidth;
		public int antennaWeightMultAntAmplitude0;
		public int antennaWeightMultAntPhase0;
		public int antennaWeightMultAntAmplitude1;
		public int antennaWeightMultAntPhase1;
		public int antennaWeightMultAntAmplitude2;
		public int antennaWeightMultAntPhase2;
		public int antennaWeightMultAntAmplitude3;
		public int antennaWeightMultAntPhase3;
		public int antennaWeightMultAntAmplitude4;
		public int antennaWeightMultAntPhase4;
		public int antennaWeightMultAntAmplitude5;
		public int antennaWeightMultAntPhase5;
		public int antennaWeightMultAntAmplitude6;
		public int antennaWeightMultAntPhase6;
		public int antennaWeightMultAntAmplitude7;
		public int antennaWeightMultAntPhase7;
	}

	public class AntCoupCoe
	{
		public int antArrayNotMibNumber;
		public List<CoupCoe> antArrayCouplingCoeffctInfo;
	}

	public struct CoupCoe
	{
		public int antCouplCoeffFreq;
		public int antCouplCoeffAntGrpIndex;
		public int antCouplCoeffAmplitude0;
		public int antCouplCoeffPhase0;
		public int antCouplCoeffAmplitude1;
		public int antCouplCoeffPhase1;
		public int antCouplCoeffAmplitude2;
		public int antCouplCoeffPhase2;
		public int antCouplCoeffAmplitude3;
		public int antCouplCoeffPhase3;
		public int antCouplCoeffAmplitude4;
		public int antCouplCoeffPhase4;
		public int antCouplCoeffAmplitude5;
		public int antCouplCoeffPhase5;
		public int antCouplCoeffAmplitude6;
		public int antCouplCoeffPhase6;
		public int antCouplCoeffAmplitude7;
		public int antCouplCoeffPhase7;
	}

	#endregion
}
