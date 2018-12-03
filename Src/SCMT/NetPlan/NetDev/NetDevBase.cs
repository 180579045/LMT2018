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

		/// <summary>
		/// 计算数据类型为BITS的值。
		/// 在解析出来的json文件中，已经把excel中bit对应的值转换为10进制值
		/// 如rruTypeFiberLength这个节点的取值范围是：0:ten|10km/1:twenty|20km/2:forty|40km，MIB类型为BITS，转换为10进制后就是：1-10km,2-20km,4-40km
		/// 如果一个设备支持多种取值，需要进行加法运算。如果一个rru支持20km和40km拉远，设置对应的数据节点值就应该是2+4=6
		/// </summary>
		/// <param name="listOriginVd"></param>
		/// <returns></returns>
		protected static int CalculateBitsValue(IEnumerable<VD> listOriginVd)
		{
			var tmp = 0;
			return listOriginVd.Where(item => int.TryParse(item.value, out tmp)).Sum(item => tmp);
		}
	}
}
