using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LmtbSnmp
{
	public interface ILmtbSnmp
	{
		int SnmpLibStartUp(string commnuity, string destIpAddr);

		int SnmpGetSync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr, long timeOut);

		int SnmpGetSync(string strIpAddr, List<string> oidList, out Dictionary<string, string> results, long timeout);

		bool SnmpGetSync(string strIpAddr, List<CDTLmtbVb> queryVbs, out Dictionary<string, string> results, long timeout);

		int SnmpGetAsync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr);

		bool GetNextRequest(string strIpAddr, List<CDTLmtbVb> queryVbs, out Dictionary<string, string> result, long timeout);

		bool GetNextRequest(string strIpAddr, List<string> reqOidList, out Dictionary<string, string> oidValue
			, out List<string> lastOidList);

		int SnmpSetSync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr, long timeOut);

		bool SnmpSetSync(string strIpAddr, List<CDTLmtbVb> setVbs, long timeOut);

		int SnmpSetAsync(CDTLmtbPdu lmtPdu, out long requestId, string strIpAddr);
	}
}
