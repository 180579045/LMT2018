/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：ObjTreeNode.cs
// 文件功能描述：DataGrid单元格类型;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2018-10-20
// 说明：DataGrid的单元格一共有以下几种类型：
//       1、仅显示字符串的单元格;
//       2、显示枚举类型的单元格;
//       3、显示BIT类型的单元格;
//       4、显示时间类型的单元格;
//       5、如果后续需要新增类型，都需要继承GridCell抽象类
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCMTMainWindow.Component.ViewModel;
using SCMTMainWindow.View;

namespace SCMTMainWindow
{
    
    /// <summary>
    /// 显示常规字符串内容的单元格类型;
    /// </summary>
    public class DataGrid_Cell_MIB : GridCell
    {
        // 单元格中的对象被拖拽到另一个对象上;
        public override void CellDragawayCallback()
        {
        }

        // 编辑该对象时的事件回调函数;
        public override void EditingCallback()
        {
            Console.WriteLine("Editing Callback");
        }

        public override void MouseMoveOnCell()
        {
        }

        public override void SelectionCellChanged(object SelectionObj)
        {
			Console.WriteLine("Editing Callback");
		}
    }

    /// <summary>
    /// 显示枚举类型的单元格类型;
    /// </summary>
    public class DataGrid_Cell_MIB_ENUM : GridCell
    {
        /// <summary>
        /// 要显示的数据集合;
        /// </summary>
        public Dictionary<int, string> m_AllContent { get; set; }
        
        /// <summary>
        /// 当前值;
        /// </summary>
        public int m_CurrentValue { get; set; }

        public DataGrid_Cell_MIB_ENUM()
        {
            m_AllContent = new Dictionary<int, string>();
        }

        /// <summary>
        /// 单元格被拖拽后的事件触发;
        /// </summary>
        public override void CellDragawayCallback()
        {
        }

        /// <summary>
        /// 枚举类型单元格被编辑时的事件触发;
        /// </summary>
        public override void EditingCallback()
        {
            Console.WriteLine("Editing Callback");
        }

        public override void MouseMoveOnCell()
        {
        }

        /// <summary>
        /// 当枚举类型选择发生变化的时候;
        /// 在此处控制用户修改完MIB内容后，下发SNMP、接收返回结果以及界面显示的功能;
        /// </summary>
        /// <param name="SelectionObj"></param>
        public override void SelectionCellChanged(object SelectionObj)
        {
            // 如果是枚举类型的单元格;
            if(SelectionObj is KeyValuePair<int, string>)
            {
                var selectionNo = (KeyValuePair<int, string>)SelectionObj;    // 用户选择枚举类型的KeyValuePair;

                // 如果用户选择没有发生变化的话,直接返回，不做处理;
                if (selectionNo.Key == this.m_CurrentValue)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("用户的选择由" + m_AllContent[this.m_CurrentValue] + "变更为" + selectionNo.Value
                        + "(" + selectionNo.Key + ")，进行相关操作,Oid is " + this.oid);

                    this.m_Content = selectionNo.Value;
                    this.m_CurrentValue = selectionNo.Key;
                }
            }
        }

		/// <summary>
		/// 设置ComboBox值
		/// </summary>
		/// <param name="val"></param>
		public void SetComboBoxValue(int val)
		{
			if (m_AllContent.ContainsKey(val))
			{
				this.m_CurrentValue = val;
				this.m_Content = this.m_AllContent[val];
			}
		}
    }

    public class DataGrid_Cell_MIB_BIT : GridCell
    {
        /// <summary>
        /// 要显示的BIT数据集合;
        /// </summary>
        public Dictionary<int, string> m_AllBit { get; set; }

        public DataGrid_Cell_MIB_BIT()
        {
            m_AllBit = new Dictionary<int, string>();
        }
       
        // 单元格中的对象被拖拽到另一个对象上;
        public override void CellDragawayCallback()
        {
        }

        // 编辑该对象时的事件回调函数;
        public override void EditingCallback()
        {
            //Console.WriteLine("Editing Callback");
            BitParaSetWindow win = new BitParaSetWindow();
            win.InitBITShowContent(m_AllBit);
            win.ShowDialog();

            if (!win.bOK)
                return;

            m_Content = win.strBITShow;
        }

        public override void MouseMoveOnCell()
        {
        }

        public override void SelectionCellChanged(object SelectionObj)
        {
            Console.WriteLine("Editing Callback");
        }
    }

    public class DataGridCell_MIB_MouseEventArgs
    {
        public string HeaderName { get; set; }
        public DyDataGrid_MIBModel SelectedCell { get; set; }
    }
}
