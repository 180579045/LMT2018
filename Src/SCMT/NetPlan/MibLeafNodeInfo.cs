using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;

namespace NetPlan
{
	// 属性值，扩展自 ReDataByTableEnglishNameChild
	public class MibLeafNodeInfo
	{
		public MibLeaf mibAttri;

		public string m_strRealValue { get; set; }                   // 属性栏中填写的真实值
		public bool m_bReadOnly { get; set; }                        // 属性栏是否只读
		public bool m_bVisible { get; set; }						// 该属性是否可见

		public string m_strIndex { get; set; }						// 保存该项的索引值

		public MibLeafNodeInfo()
		{
			mibAttri = new MibLeaf();
			m_bReadOnly = false;
			m_bVisible = true;
		}

		// 其他方法，用于m_strRealValue的转换

	}
}
