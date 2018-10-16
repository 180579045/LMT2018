using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;

namespace NetPlan
{
	// 具体的MIB信息及该字段对应的值
	public class MibLeafNodeInfo
	{
		public MibLeaf mibAttri;

		public string m_strOriginValue { get; set; }				// 字段的原始值，用于判断该字段是否被修改过

		public string m_strLatestValue { get; set; }				// 字段最后设置的值

		public bool m_bReadOnly { get; set; }						// 属性栏是否只读

		public bool m_bVisible { get; set; }							// 该属性是否可见

		//public string m_strIndex { get; set; }						// 保存该项的索引值

		public MibLeafNodeInfo()
		{
			mibAttri = new MibLeaf();
			m_bReadOnly = false;
			m_bVisible = true;
		}

		/// <summary>
		/// 设置字段的值，需要判断范围什么的是否合法
		/// </summary>
		/// <param name="strLatestValue"></param>
		/// <returns>true:设置成功,其他情况返回false</returns>
		public bool SetValue(string strLatestValue)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 判断这个字段的最新值和原始值是否相同
		/// </summary>
		/// <returns></returns>
		public bool IsModified()
		{
			if (null == m_strLatestValue)
			{
				return false;
			}

			return !m_strOriginValue.Equals(m_strLatestValue, StringComparison.OrdinalIgnoreCase);  // 忽略大小写
		}

	}
}
