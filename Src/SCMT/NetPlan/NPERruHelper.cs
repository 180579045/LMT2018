using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using LogManager;

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

			if (_npeRru?.rruTypeInfo == null) return retInfo;

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

		/// <summary>
		/// 根据RRU类型获取RRU信息。RRU type是数字
		/// </summary>
		/// <param name="nRruType"></param>
		/// <returns></returns>
		public RruInfo GetRruInfoByType(int nRruType)
		{
			if (null == _npeRru)
			{
				Log.Error("从RRU器件库中读取的信息为空");
				return null;
			}

			return _npeRru.rruTypeInfo.FirstOrDefault(rru => rru.rruTypeIndex == nRruType);
		}

		/// <summary>
		/// 根据RRU型号，判断是否是pico设备
		/// </summary>
		/// <param name="nRruType"></param>
		/// <returns></returns>
		public bool IsPicoDevice(int nRruType)
		{
			var rruInfo = GetRruInfoByType(nRruType);
			if (null == rruInfo)
			{
				Log.Error($"未找到类型为{nRruType}的rru信息，请更新RRU器件库");
				return false;
			}

			var rruTypeName = rruInfo.rruTypeName;
			return (rruTypeName.IndexOf('p') == 0) ;
		}

		/// <summary>
		/// 根据RRU类型索引和厂家索引获取所有RRU所有通道信息
		/// </summary>
		/// <param name="nTypeIdx">rru类型</param>
		/// <param name="nVendorIdx">厂家编号</param>
		/// <returns></returns>
		public List<RruPortInfo> GetRruPathInfoByTypeAndVendor(int nTypeIdx, int nVendorIdx)
		{
			return _npeRru.rruTypePortInfo.Where(portInfo => portInfo.rruTypePortManufacturerIndex == nVendorIdx && portInfo.rruTypePortIndex == nTypeIdx).ToList();
		}

		/// <summary>
		/// 根据厂家索引和编号索引获取rru类型信息
		/// </summary>
		/// <param name="strTypeIdx"></param>
		/// <param name="strVendorIdx"></param>
		/// <returns></returns>
		public RruInfo GetRruTypeInfoByTypeAndVendorIdx(string strTypeIdx, string strVendorIdx)
		{
			var nType = int.Parse(strTypeIdx);
			var nVendor = int.Parse(strVendorIdx);

			return _npeRru.rruTypeInfo.FirstOrDefault(item => item.rruTypeManufacturerIndex == nVendor && item.rruTypeIndex == nType);
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
	    public NPERru GetNPERru()
	    {
	        return _npeRru;
	    }
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
		public string value { get; set; }
        public string desc { get; set; }
        public List<VD> bandwidth { get; set; }

		public IrBand()
		{
			bandwidth = new List<VD>();
		}
	}

	public class RruInfo
	{
		public int rruTypeManufacturerIndex { get; set; }
        public string rruTypeNotMibManufacturerName { get; set; }
        public int rruTypeIndex { get; set; }
        public string rruTypeName { get; set; }
        public int rruTypeMaxAntPathNum { get; set; }
        public int rruTypeMaxTxPower { get; set; }
        public int rruTypeBandWidth { get; set; }
        public List<VD> rruTypeFiberLength { get; set; }
        public List<VD> rruTypeIrCompressMode { get; set; }
        public List<VD> rruTypeSupportCellWorkMode { get; set; }
        public string rruTypeFamilyName { get; set; }
        public int rruTypeNotMibMaxePortNo { get; set; }
        public List<VD> rruTypeNotMibSupportNetWorkMode { get; set; }
        public List<VD> rruTypeNotMibIrRate { get; set; }
        public List<IrBand> rruTypeNotMibIrBand { get; set; }

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
		public int rruTypePortManufacturerIndex { get; set; }
        public string rruTypePortNotMibManufacturerName { get; set; }
        public int rruTypePortIndex { get; set; }
        public int rruTypePortNo { get; set; }
        public int rruTypePortPathNo { get; set; }
        public List<VD> rruTypePortSupportFreqBand { get; set; }
        public int rruTypePortSupportFreqBandWidth { get; set; }
        public int rruTypePortSupportAbandTdsCarrierNum { get; set; }
        public int rruTypePortSupportFBandTdsCarrierNum { get; set; }
        public int rruTypePortCalAIqTxNom { get; set; }
        public int rruTypePortCalAIqRxNom { get; set; }
        public int rruTypePortCalPoutTxNom { get; set; }
        public int rruTypePortCalPinRxNom { get; set; }
        public int rruTypePortAntMaxPower { get; set; }
        public List<VD> rruTypePortNotMibRxTxStatus { get; set; }

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
