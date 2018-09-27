using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;

namespace NetPlan
{
	// 解析RRU信息表
	public class NPERruHelper : Singleton<NPERruHelper>
	{
		#region 公有方法

		// 获取所有的RRU类型
		public List<RruInfo> GetAllRruInfo()
		{
			return _npeRru?.rruTypeInfo;
		}

		// 初始化时获取所有的RRU信息
		public Dictionary<string, InitialRruInfo> GetRruInfoMap()
		{
			var retInfo = new Dictionary<string, InitialRruInfo>();

			foreach (var rruInfo in _npeRru?.rruTypeInfo)
			{
				var tempRru = new InitialRruInfo
				{
					rruTypeIndex = rruInfo.rruTypeIndex,
					rruTypeName = rruInfo.rruTypeName,
					rruTypeMaxAntPathNum = rruInfo.rruTypeMaxAntPathNum,
					rruInfoDesc = FormatRruDesc(rruInfo)
				};

				foreach (var wm in rruInfo.rruTypeNotMibSupportNetWorkMode)
				{
					var mapKv = MibStringHelper.SplitMibEnumString(wm.desc);
					tempRru.rruWorkMode.AddRange(mapKv.Select(kv => kv.Value));
				}

				retInfo.Add(rruInfo.rruTypeName, tempRru);
			}

			return retInfo;
		}

		#endregion

		#region 私有方法

		private NPERruHelper()
		{
			_npeRru = null;

			try
			{
				var path = FilePathHelper.GetAppPath() + ConfigFileHelper.NetPlanRruJson;
				var jsonContent = FileRdWrHelper.GetFileContent(path);
				_npeRru = JsonHelper.SerializeJsonToObject<NPERru>(jsonContent);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		// 格式化RRU的概述信息
		private string FormatRruDesc(RruInfo ri)
		{
			var sb = new StringBuilder();
			sb.AppendFormat("{0}设备信息简述：\n", ri.rruTypeName);
			sb.AppendFormat("设备类型名：{0}\n", ri.rruTypeName);
			sb.AppendFormat("最大通道数：{0:d}\n", ri.rruTypeMaxAntPathNum);
			sb.AppendFormat("最大输出功率：{0:d}\n", ri.rruTypeMaxTxPower);
			return sb.ToString();
		}

		#endregion

		#region 私有属性

		private NPERru _npeRru;

		#endregion
	}

	public class NPERru
	{
		public List<RruInfo> rruTypeInfo;
		public List<RruPortInfo> rruTypePortInfo;

		public NPERru()
		{
			rruTypeInfo = new List<RruInfo>();
			rruTypePortInfo = new List<RruPortInfo>();
		}
	}

	public class IrBand
	{
		public string value;
		public string desc;
		public List<VD> bandwidth;

		public IrBand()
		{
			bandwidth = new List<VD>();
		}
	}

	public class RruInfo
	{
		public int rruTypeManufacturerIndex;
		public string rruTypeNotMibManufacturerName;
		public int rruTypeIndex;
		public string rruTypeName;
		public int rruTypeMaxAntPathNum;
		public int rruTypeMaxTxPower;
		public int rruTypeBandWidth;
		public List<VD> rruTypeFiberLength;
		public List<VD> rruTypeIrCompressMode;
		public List<VD> rruTypeSupportCellWorkMode;
		public string rruTypeFamilyName;
		public int rruTypeNotMibMaxePortNo;
		public List<VD> rruTypeNotMibSupportNetWorkMode;
		public List<VD> rruTypeNotMibIrRate;
		public List<IrBand> rruTypeNotMibIrBand;

		public RruInfo()
		{
			rruTypeFiberLength = new List<VD>();
			rruTypeIrCompressMode = new List<VD>();
			rruTypeSupportCellWorkMode = new List<VD>();
			rruTypeNotMibSupportNetWorkMode = new List<VD>();
			rruTypeNotMibIrRate = new List<VD>();
			rruTypeNotMibIrBand = new List<IrBand>();
		}
	}

	public class RruPortInfo
	{
		public int rruTypePortManufacturerIndex;
		public string rruTypePortNotMibManufacturerName;
		public int rruTypePortIndex;
		public int rruTypePortNo;
		public int rruTypePortPathNo;
		public List<VD> rruTypePortSupportFreqBand;
		public int rruTypePortSupportFreqBandWidth;
		public int rruTypePortSupportAbandTdsCarrierNum;
		public int rruTypePortSupportFBandTdsCarrierNum;
		public int rruTypePortCalAIqTxNom;
		public int rruTypePortCalAIqRxNom;
		public int rruTypePortCalPoutTxNom;
		public int rruTypePortCalPinRxNom;
		public int rruTypePortAntMaxPower;
		public List<VD> rruTypePortNotMibRxTxStatus;

		public RruPortInfo()
		{
			rruTypePortSupportFreqBand = new List<VD>();
			rruTypePortNotMibRxTxStatus = new List<VD>();
		}
	}

	// 初始化RRU信息时获取的简短信息
	public class InitialRruInfo
	{
		public int rruTypeIndex;
		public string rruTypeName;
		public int rruTypeMaxAntPathNum;
		public List<string> rruWorkMode;
		public string rruInfoDesc;			// RRU主要信息描述

		public InitialRruInfo()
		{
			rruWorkMode = new List<string>();
		}
	}
}
