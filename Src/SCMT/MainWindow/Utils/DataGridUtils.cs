using CommonUtility;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
using SCMTMainWindow.Component.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SCMTMainWindow.Utils
{
	/// <summary>
	/// DataGrid工具类
	/// </summary>
	public class DataGridUtils
	{
		/// <summary>
		/// 使用CDTLmtbPdu更新DataGrid
		/// </summary>
		/// <param name="dataSource"></param>
		/// <param name="lmtPdu"></param>
		/// <returns></returns>
		public static bool UpdateGridByPdu(Dictionary<string, object> dataSource, CDTLmtbPdu lmtPdu)
		{
			string strMsg = "";

			if (null == lmtPdu)
			{
				strMsg = "参数lmtPdu为空!";
				Log.Error(strMsg);
				return false;
			}

			// 使用lmtPdu更新DataGrid数据
			for (int i = 0; i < lmtPdu.VbCount(); i++)
			{
				// 获取vb
				CDTLmtbVb lmtVb = new CDTLmtbVb();
				lmtPdu.GetVbByIndex(i, ref lmtVb);

				// 去掉oid的索引
				string oid = lmtVb.Oid.Substring(0, lmtVb.Oid.LastIndexOf('.'));
				// 获取节点信息
				MibLeaf reData = null;
				reData = SnmpToDatabase.GetMibNodeInfoByOid(oid, CSEnbHelper.GetCurEnbAddr());
				if (reData == null)
				{
					continue;
				}

				// Mib节点英文名称
				string mibNameEn = reData.childNameMib;

				if (dataSource.ContainsKey(mibNameEn))
				{
					object cellInfo = dataSource[mibNameEn];
					if (typeof(DataGrid_Cell_MIB_ENUM) == cellInfo.GetType())
					{
						DataGrid_Cell_MIB_ENUM cellEnum = (DataGrid_Cell_MIB_ENUM)cellInfo;
						cellEnum.SetComboBoxValue(Convert.ToInt32(lmtVb.Value)); // 设置ComboBox值
					}
					else if (typeof(DataGrid_Cell_MIB) == cellInfo.GetType())
					{
						DataGrid_Cell_MIB cellText = (DataGrid_Cell_MIB)cellInfo;
						cellText.m_Content = lmtVb.Value;
					}
					// TODO: 其他类型
				}
				else
				{
					continue;
				}
			}
			return true;
		}


		/// <summary>
		/// 使用DataGrid行数据组装VB
		/// </summary>
		/// <param name="lineData">DataGrid的行数据</param>
		/// <param name="enName2Value">变更节点的英文名称与值的对应关系</param>
		/// <param name="setVbs">组装后的CDTLmtbVb列表</param>
		/// <param name="strErr">错误信息</param>
		/// <returns></returns>
		public static bool MakeSnmpVbs(Dictionary<string, object> lineData, Dictionary<string, string> enName2Value
			, ref List<CDTLmtbVb> setVbs, out string strErr, int cmdType)
		{
			strErr = "";

			// oid
			string oid = null;
			// Mib节点英文名称
			string mibNameEn = null;
			// Mib节点值
			string nodeVal = null;
			// 节点的旧值
			string nodeOldVal = null;

			if (null == setVbs)
			{
				setVbs = new List<CDTLmtbVb>();
			}

			foreach (KeyValuePair<string, object> item in lineData)
			{
				// 初始化
				oid = null;
				nodeVal = null;

				mibNameEn = item.Key;
				object obj = item.Value;

				if ("indexlist".Equals(mibNameEn)) // 过滤掉DataGrid索引列
				{
					continue;
				}

				// oid
				if (typeof(DataGrid_Cell_MIB_ENUM) == obj.GetType()) // ComboBox
				{
					DataGrid_Cell_MIB_ENUM mibEnum = (DataGrid_Cell_MIB_ENUM)obj;
					oid = mibEnum.oid;
					nodeOldVal = mibEnum.m_CurrentValue.ToString();
				}
				else if (typeof(DataGrid_Cell_MIB) == obj.GetType()) // TextBox
				{
					DataGrid_Cell_MIB mibText = (DataGrid_Cell_MIB)obj;
					oid = mibText.oid;
					nodeOldVal = mibText.m_Content;
				}
				// 值
				if (enName2Value.ContainsKey(mibNameEn))
				{
					nodeVal = enName2Value[mibNameEn];
				}
				else
				{
					nodeVal = nodeOldVal;
				}

				if (string.IsNullOrEmpty(oid))
				{
					strErr = "无法获取oid！";
					Log.Error(strErr);
					return false;
				}

				// 获取Mib节点信息
				MibLeaf reData = SnmpToDatabase.GetMibNodeInfoByName(mibNameEn, CSEnbHelper.GetCurEnbAddr());
				if (reData == null)
				{
					strErr = string.Format("获取Mib节点错误，enName:{0}", mibNameEn);
					Log.Error(strErr);
					return false;
				}

				if (reData.ASNType == "RowStatus" && (cmdType == 3 || cmdType == 4))
				{
					continue;
				}

				// 如果Mib节点类型是Bits，要做一下值的转换
				if (string.Equals("BITS", reData.mibSyntax, StringComparison.OrdinalIgnoreCase))
				{
					uint bitsVal;
					SnmpMibUtil.GetBitsTypeValueFromDesc(reData.managerValueRange, nodeVal, out bitsVal);
					nodeVal = bitsVal.ToString();
				}
				// 组装Vb
				CDTLmtbVb lmtVb = new CDTLmtbVb();
				lmtVb.Oid = oid;
				lmtVb.Value = nodeVal;
				lmtVb.SnmpSyntax = LmtbSnmpEx.GetSyntax(reData.mibSyntax);


				setVbs.Add(lmtVb);
			}

			return true;
		}


		/// <summary>
		/// 根据DataGrid的行数据生成Mib英文名称与值的对应关系
		/// </summary>
		/// <param name="lineData">DataGrid行数据</param>
		/// <param name="enName2Value">英文名称与值对应关系</param>
		/// <returns></returns>
		public static bool MakeEnName2Value(Dictionary<string, object> lineData, ref Dictionary<string, string> enName2Value)
		{
			if (null == enName2Value)
			{
				enName2Value = new Dictionary<string, string>();
			}

			// Mib英文名称
			string mibNameEn = null;
			// Mib节点值
			string mibValue = null;
			foreach (KeyValuePair<string, object> item in lineData)
			{
				mibNameEn = item.Key;

				// Mib值
				if (typeof(DataGrid_Cell_MIB_ENUM) == item.Value.GetType()) // ComboBox
				{
					DataGrid_Cell_MIB_ENUM mibEnum = (DataGrid_Cell_MIB_ENUM)item.Value;
					mibValue = mibEnum.m_CurrentValue.ToString();
				}
				else if (typeof(DataGrid_Cell_MIB) == item.Value.GetType()) // TextBox
				{
					DataGrid_Cell_MIB mibText = (DataGrid_Cell_MIB)item.Value;
					mibValue = mibText.m_Content;
				}

				enName2Value.Add(mibNameEn, mibValue);
			}

			return true;
		}


		/// <summary>
		/// TODO:临时方法!!!!!!!!
		/// </summary>
		/// <param name="lineData">DataGrid行数据</param>
		/// <param name="enName2Value">英文名称与值对应关系</param>
		/// <returns></returns>
		public static bool GetMibIndex(Dictionary<string, object> lineData, int nIdxGrade, out string strIndex)
		{
			strIndex = "";

			// Mib英文名称
			string mibNameEn = null;
			// Mib节点值
			string mibValue = null;
			string oid = null;
			foreach (KeyValuePair<string, object> item in lineData)
			{
				mibNameEn = item.Key;

				if (mibNameEn.Equals("indexlist"))
					continue;

				// Mib值
				if (typeof(DataGrid_Cell_MIB_ENUM) == item.Value.GetType()) // ComboBox
				{
					DataGrid_Cell_MIB_ENUM mibEnum = (DataGrid_Cell_MIB_ENUM)item.Value;
					mibValue = mibEnum.m_CurrentValue.ToString();
					oid = mibEnum.oid;
				}
				else if (typeof(DataGrid_Cell_MIB) == item.Value.GetType()) // TextBox
				{
					DataGrid_Cell_MIB mibText = (DataGrid_Cell_MIB)item.Value;
					mibValue = mibText.m_Content;
					oid = mibText.oid;
				}

				if (!string.IsNullOrEmpty(oid))
				{
					// 截取最后一位索引
					var idx = MibStringHelper.GetIndexValueByGrade(oid, nIdxGrade);
					if (null == idx)
					{
						continue;
					}

					Log.Debug($"根据oid:{oid}截取出{nIdxGrade}维索引：{idx}");
					strIndex = idx;
				}
			}

			return true;
		}

		/// <summary>
		/// 获取选中单元格的的Mib oid和英文名称
		/// </summary>
		/// <param name="cellInfo"></param>
		/// <param name="oid"></param>
		/// <param name="mibNameEn"></param>
		public static bool GetOidAndEnName(DataGridCellInfo cellInfo, out string oid, out string mibNameEn)
		{
			oid = null;
			mibNameEn = null;

			// 中文列名
			string cnHeader = cellInfo.Column.Header.ToString();

			// 行Model
			DyDataGrid_MIBModel mibModel = (DyDataGrid_MIBModel)cellInfo.Item;

			// 英文名称与Mib对象属性的对应关系
			Dictionary<string, object> linePro = mibModel.Properties;

			// 中文名与Mib英文名对应关系
			Dictionary<string, string> cnEnName = mibModel.ColName_Property;

			// 获取英文名
			if (cnEnName.ContainsKey(cnHeader))
			{
				mibNameEn = cnEnName[cnHeader];
			}
			else
			{
				return false;
			}

			// 获取oid
			if (linePro.ContainsKey(mibNameEn))
			{
				object item = linePro[mibNameEn];
				if (typeof(DataGrid_Cell_MIB_ENUM) == item.GetType())
				{
					DataGrid_Cell_MIB_ENUM mibEnum = (DataGrid_Cell_MIB_ENUM)item;
					oid = mibEnum.oid;
				}
				else if (typeof(DataGrid_Cell_MIB) == item.GetType())
				{
					DataGrid_Cell_MIB mibText = (DataGrid_Cell_MIB)item;
					oid = mibText.oid;
				}
			}

			return true;
		}

		/// <summary>
		/// 检查单元格中的数据是否有变化
		/// </summary>
		/// <param name="lineData">行数据</param>
		/// <param name="mibNameEn">Mib英文名称</param>
		/// <param name="strValue">Mib节点值</param>
		/// <returns></returns>
		public static bool IsValueChanged(Dictionary<string, object> lineData, string mibNameEn, string strValue)
		{
			// 无匹配的数据
			if (!lineData.ContainsKey(mibNameEn))
			{
				return false;
			}

			object mibNode = lineData[mibNameEn];

			if (typeof(DataGrid_Cell_MIB_ENUM) == mibNode.GetType()) // ComboBox
			{
				DataGrid_Cell_MIB_ENUM mibEnum = (DataGrid_Cell_MIB_ENUM)mibNode;
				if (mibEnum.m_CurrentValue != Convert.ToInt32(strValue))
				{
					return true; // 改变
				}
			}
			else if (typeof(DataGrid_Cell_MIB) == mibNode.GetType()) // TextBox
			{
				DataGrid_Cell_MIB mibTextBox = (DataGrid_Cell_MIB)mibNode;
				if (string.Compare(mibTextBox.m_Content, strValue) != 0)
				{
					return true; // 改变
				}
			}

			return false;
		}

		/// <summary>
		/// 右键菜单添加修改删除时根据返回的结果获取索引节点显示信息
		/// </summary>
		/// <param name="pdu"></param>
		/// <returns></returns>
		public static bool GetIndexNodeInfoFromLmtbPdu(CDTLmtbPdu lmtPdu, MibTable tbl, ref string oid, ref string showInfo)
		{
			string index = "";
			if (!GetIndexFromLmtbPdu(lmtPdu, ref index))
				return false;

			if (tbl == null)
				return false;

			foreach (MibLeaf leaf in tbl.childList)
			{
				if (leaf.IsIndex.Equals("True"))
				{
					showInfo += leaf.childNameCh + index;
				}
			}
			oid = SnmpToDatabase.GetMibPrefix() + tbl.oid;

			return true;
		}
		/// <summary>
		/// 获取添加修改删除的索引号
		/// </summary>
		/// <param name="lmtPdu"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static bool GetIndexFromLmtbPdu(CDTLmtbPdu lmtPdu, ref string index)
		{
			var strOidPrefix = SnmpToDatabase.GetMibPrefix();
			var lmtVbCount = lmtPdu.VbCount();
			// 根据Vb 中的OID 获取 MibOid
			string strMibOid;
			if (lmtPdu.VbCount() > 0)
			{
				var lmtVb = lmtPdu.GetVbByIndexEx(0);
				var strVbOid = lmtVb?.Oid;
				if (string.IsNullOrEmpty(strVbOid))
				{
					return false;
				}

				// 去掉前缀
				strMibOid = strVbOid.Replace(strOidPrefix, "");

				// 去掉最后一位索引
				var nindex = strMibOid.LastIndexOf('.');
				if (nindex >= strMibOid.Length)
				{
					return false;
				}
				index = strMibOid.Substring(nindex + 1);
			}

			return true;
		}

	}
}
