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

		#endregion

		#region 私有属性

		private NPERru _npeRru;

		#endregion
	}

	public class NPERru
	{
		public List<RruInfo> rruTypeInfo;
		public List<RruPortInfo> rruTypePortInfo;
	}

	public class IrBand
	{
		public string value;
		public string desc;
		public List<VD> bandwidth;
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
	}
}
