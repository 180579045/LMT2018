using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 网规设备相关
/// </summary>
namespace NetPlan
{
	internal class NetDevBase
	{
		internal virtual bool DistributeDevToEnb(DevAttributeInfo dev)
		{
			throw new NotImplementedException();
		}


		internal EnumSnmpCmdType GetSnmCmdTypeFromWcbOpType(AntWcbOpType opType)
		{
			var cmdType = EnumSnmpCmdType.Invalid;
			switch (opType)
			{
				case AntWcbOpType.skip:
					break;
				case AntWcbOpType.only_add:
					cmdType = EnumSnmpCmdType.Add;
					break;
				case AntWcbOpType.only_del:
					cmdType = EnumSnmpCmdType.Del;
					break;
				case AntWcbOpType.only_set:
					cmdType = EnumSnmpCmdType.Set;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(opType), opType, null);
			}

			return cmdType;
		}
	}
}
