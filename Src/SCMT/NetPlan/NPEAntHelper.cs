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
		/// <returns>返回一个字典，key:厂家字符串，value:该厂家下所有的天线阵列表</returns>
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

		/// <summary>
		/// 根据天线阵类型编号查询天线阵权重值
		/// </summary>
		/// <param name="antNo"></param>
		/// <returns></returns>
		public AntWeight GetAntWeightByNo(int antNo)
		{
			var weightList = _antInfo.antennaWeightTable;
			if (null == weightList || 0 == weightList.Count)
			{
				throw new CustomException("天线阵的权重信息为空");
			}

			foreach (var item in weightList)
			{
				if (item.antArrayNotMibNumber == antNo)
				{
					return item;
				}
			}
			return null;
		}

		/// <summary>
		/// 根据天线厂家名获取厂家索引
		/// </summary>
		/// <param name="strVendorName"></param>
		/// <returns></returns>
		public string GetVendorIndexByName(string strVendorName)
		{
			if (string.IsNullOrEmpty(strVendorName))
			{
				throw new ArgumentNullException(strVendorName);
			}

			return (from item in _antInfo.antennaTypeTable
					where item.antArrayNotMibVendorName == strVendorName
					select item.antArrayVendor.ToString()).FirstOrDefault();
		}

		/// <summary>
		/// 根据天线阵类型名获取类型索引
		/// </summary>
		/// <param name="strModelName"></param>
		/// <returns></returns>
		public string GetTypeIndexByModelName(string strModelName)
		{
			if (string.IsNullOrEmpty(strModelName))
			{
				throw new ArgumentNullException(strModelName);
			}

			return (from item in _antInfo.antennaTypeTable
					where item.antArrayModelName == strModelName
					select item.antArrayIndex.ToString()).FirstOrDefault();
		}

		/// <summary>
		/// 根据厂家索引和类型索引获取天线阵权重信息
		/// </summary>
		/// <param name="strVendorIdx"></param>
		/// <param name="strTypeIdx"></param>
		/// <returns></returns>
		public AntWeight GetAntWeightByNo(string strVendorIdx, string strTypeIdx)
		{
			var antNo = GetAntNoByVendorAndType(strVendorIdx, strTypeIdx);
			return -1 == antNo ? null : GetAntWeightByNo(antNo);
		}

		/// <summary>
		/// 根据厂家索引和类型索引获取耦合信息信息
		/// </summary>
		/// <param name="strVendorIdx"></param>
		/// <param name="strTypeIdx"></param>
		/// <returns></returns>
		public AntCoupCoe GetCouplingByAntVendorAndType(string strVendorIdx, string strTypeIdx)
		{
			var antNo = GetAntNoByVendorAndType(strVendorIdx, strTypeIdx);
			return -1 == antNo ? null : GetAntCoupCoeByNo(antNo);
		}

		/// <summary>
		/// 根据天线阵编号获取该天线阵的耦合系数
		/// </summary>
		/// <param name="antNo"></param>
		/// <returns></returns>
		public AntCoupCoe GetAntCoupCoeByNo(int antNo)
		{
			var coupList = _antInfo.couplingCoeffctTable;
			if (null == coupList || 0 == coupList.Count)
			{
				throw new CustomException("天线阵的权重信息为空");
			}

			return coupList.FirstOrDefault(tmp => antNo == tmp.antArrayNotMibNumber);
		}

		/// <summary>
		/// 根据厂家索引和天线阵索引获取天线阵信息
		/// </summary>
		/// <param name="strVendorIdx"></param>
		/// <param name="strTypeIdx"></param>
		/// <returns></returns>
		public AntType GetAntTypeByVendorAndTypeIdx(string strVendorIdx, string strTypeIdx)
		{
			var atList = _antInfo.antennaTypeTable;
			return atList.FirstOrDefault(item => item.antArrayIndex.ToString() == strTypeIdx && item.antArrayVendor.ToString() == strVendorIdx);
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


		/// <summary>
		/// 根据厂家索引和类型索引获取天线阵编号
		/// </summary>
		/// <param name="strVendorIdx"></param>
		/// <param name="strTypeIdx"></param>
		/// <returns></returns>
		private int GetAntNoByVendorAndType(string strVendorIdx, string strTypeIdx)
		{
			var nVIdx = int.Parse(strVendorIdx);
			var nTIdx = int.Parse(strTypeIdx);

			foreach (var item in _antInfo.antennaTypeTable)
			{
				if (item.antArrayVendor == nVIdx && item.antArrayIndex == nTIdx)
				{
					return item.antArrayNotMibNumber;
				}
			}

			return -1;
		}

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

	/// <summary>
	/// 天线阵类型信息
	/// </summary>
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

	/// <summary>
	/// 多天线权重
	/// </summary>
	public class AntWeight
	{
		public int antArrayNotMibNumber;
		public List<Weight> antArrayMultWeight;

		public AntWeight()
		{
			antArrayMultWeight = new List<Weight>();
		}

		// todo 格式化支持的天线阵频段
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

	/// <summary>
	/// 权重信息
	/// </summary>
	public struct Weight
	{
		public int antennaWeightMultFrequencyBand;
		public int antennaWeightMultAntGrpIndex;
		public int antennaWeightMultAntStatusIndex;
		public string antennaWeightMultNotMibAntStatus;
		public int antennaWeightMultAntHalfPowerBeamWidth;
		public int antennaWeightMultAntVerHalfPowerBeamWidth;
		public int antennaWeightMultAntAmplitude0 { get; set; }
		public int antennaWeightMultAntPhase0 { get; set; }
		public int antennaWeightMultAntAmplitude1 { get; set; }
		public int antennaWeightMultAntPhase1 { get; set; }
		public int antennaWeightMultAntAmplitude2 { get; set; }
		public int antennaWeightMultAntPhase2 { get; set; }
		public int antennaWeightMultAntAmplitude3 { get; set; }
		public int antennaWeightMultAntPhase3 { get; set; }
		public int antennaWeightMultAntAmplitude4 { get; set; }
		public int antennaWeightMultAntPhase4 { get; set; }
		public int antennaWeightMultAntAmplitude5 { get; set; }
		public int antennaWeightMultAntPhase5 { get; set; }
		public int antennaWeightMultAntAmplitude6 { get; set; }
		public int antennaWeightMultAntPhase6 { get; set; }
		public int antennaWeightMultAntAmplitude7 { get; set; }
		public int antennaWeightMultAntPhase7 { get; set; }
	}


	/// <summary>
	/// 天线耦合
	/// </summary>
	public class AntCoupCoe
	{
		public int antArrayNotMibNumber;
		public List<CoupCoe> antArrayCouplingCoeffctInfo;
	}

	/// <summary>
	/// 耦合数据
	/// </summary>
	public struct CoupCoe
	{
		public int antCouplCoeffFreq;
		public int antCouplCoeffAntGrpIndex;
		public int antCouplCoeffAmplitude0 { get; set; }
		public int antCouplCoeffPhase0 { get; set; }
		public int antCouplCoeffAmplitude1 { get; set; }
		public int antCouplCoeffPhase1 { get; set; }
		public int antCouplCoeffAmplitude2 { get; set; }
		public int antCouplCoeffPhase2 { get; set; }
		public int antCouplCoeffAmplitude3 { get; set; }
		public int antCouplCoeffPhase3 { get; set; }
		public int antCouplCoeffAmplitude4 { get; set; }
		public int antCouplCoeffPhase4 { get; set; }
		public int antCouplCoeffAmplitude5 { get; set; }
		public int antCouplCoeffPhase5 { get; set; }
		public int antCouplCoeffAmplitude6 { get; set; }
		public int antCouplCoeffPhase6 { get; set; }
		public int antCouplCoeffAmplitude7 { get; set; }
		public int antCouplCoeffPhase7 { get; set; }
	}

	#endregion
}
