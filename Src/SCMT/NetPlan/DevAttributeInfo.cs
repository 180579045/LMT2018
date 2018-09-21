using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPlan
{
	// 设备的属性
	public class DevAttributeInfo
	{
		public string m_strOidIndex { get; set; }                        // 区分属性
		public List<MibLeafNodeInfo> m_listAttributes;      // 所有的属性
		public EnumDevType m_enumDevType { get; set; }      // 设备类型枚举值

		#region 属性值的获取和设置



		#endregion

	}

	// 连接的属性
	public class LinkAttributeInfo : DevAttributeInfo
	{
		
	}
}
