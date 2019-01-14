/*----------------------------------------------------------------
// Copyright (C) 2019 大唐移动通信设备有限公司 版权所有;
//
// 文件名：MainWindowVM.cs
// 文件功能描述：基站详细信息VM类,Mvvm架构改造后;
//               应把之前所有主函数都放入这里;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2019-1-6
//----------------------------------------------------------------*/

using SCMTOperationCore.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTMainWindow.ViewModel
{
    /// <summary>
    /// 基站信息主页签的VM层;
    /// </summary>
    public class NodeBMainTabVM : TabBase
    {
        private NodeB nodeB { get; set; }            // 这个基站页签对应的基站;
        private ObjNode ObjNode { get; set; }        // 这个基站页签对应的对象树;
        

        public NodeBMainTabVM()
        {
            Console.WriteLine("NodeBMianTab Initial!");
            
        }
    }
}
