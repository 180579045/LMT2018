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
		public string TxRxStatus;			// 收发方向
		public string FreqBand;				// 通道频段

		public NPRruToCellInfo(string trStatus)
		{
			CellIdList = new List<CellAndState>();
			TxRxStatus = trStatus;
		}
	}

	public struct CellAndState
	{
		public string cellId;
		public bool bIsFixed;		// true:已配置，UI置为disable，false:规划中小区，UI可以勾选
	}
}
