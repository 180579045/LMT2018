using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 网规模块中涉及到的魔数
namespace NetPlan
{
	internal static class MagicNum
	{
		public const int PICO_TO_RHUB_PORT_CNT = 2;         // pico设备按两个端口算，如果MIB有修改，需要进行处理
		public const int RRU_TO_BBU_PORT_CNT = 4;			// rru设备光口数硬编码最大为4
		public const int BBU_IR_PORT_CNT = 8;
		public const int LC_CNT = 36;
	}
}
