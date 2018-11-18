using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPlan
{
	public class NPRruToCellInfo
	{
		public List<CellAndState> CellIdList;
		public List<string> SupportTxRxStatus;		// 支持的收发方向，只用于呈现在端口归属小区窗口中
		public string RealTRx;						// 用于配置的频道收发方向
		public string SupportFreqBand;				// 支持的通道频段

		public NPRruToCellInfo()
		{
			CellIdList = new List<CellAndState>();
			SupportTxRxStatus = new List<string>();
		}
	}

	public struct CellAndState
	{
		public string cellId;
		public bool bIsFixed;		// true:已配置，UI置为disable，false:规划中小区，UI可以勾选
	}
}
