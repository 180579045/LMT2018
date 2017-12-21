/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：ObjTreeNode.cs
// 文件功能描述：对象树节点类;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2017-11-20
//----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arthas.Controls.Metro;
using System.Windows.Controls;
using System.Windows;

namespace SCMTMainWindow
{
    /// <summary>
    /// 对象树节点抽象类;
    /// 说明:由此可衍生出各种类型的节点，诸如普通查询\小区\传输\分制式显示;
    /// </summary>
    public abstract class ObjNode
    {
        public string version { get; set; }                      // 对象树版本号;
        public int ObjID { get; set; }                           // 节点ID;
        public int ObjParentID { get; set; }                     // 父节点ID;
        public string ObjName { get; set; }                      // 节点名称;
        public List<string> OIDList { get; set; }                // 节点包含OID列表;
        public List<MibCommand> MibCmdLsit { get; set; }         // 节点包含命令列表;
        public List<ObjNode> SubObj_Lsit { get; set; }           // 孩子节点列表;

        abstract public void Add(ObjNode obj);                   // 增加孩子节点;
        abstract public void Remove(ObjNode obj);                // 删除孩子节点;

        public event EventHandler IsExpandedChanged;             // 树形结构展开时;
        public event EventHandler IsSelectedChanged;             // 树形结构节点被选择时;

        /// <summary>
        /// 对象树点击事件;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void ClickObjNode(object sender, EventArgs e);
        
        /// <summary>
        /// 将所有节点填入对象树中;
        /// </summary>
        public void TraverseChildren(MetroExpander Obj_Tree, StackPanel Lists, int depths)
        {
            Thickness margin = new Thickness();
            // 遍历所有对象树节点;
            foreach (var Obj_Node in SubObj_Lsit)
            {
                // 只有枝节点才能够加入对象树;
                if (Obj_Node is ObjTreeNode)
                {
                    MetroExpander item = new MetroExpander();
                    
                    // 判断孩子中是否还包含枝节点;
                    bool NotContainTree = true;
                    foreach(var isTree in Obj_Node.SubObj_Lsit)
                    {
                        if(isTree is ObjTreeNode)
                        {
                            NotContainTree = false;
                        }
                    }
                    
                    // 将孩子没有枝节点的节点进行缩进处理;
                    if (NotContainTree)
                    {
                        margin.Left = 35 + depths;
                        margin.Top = 8;
                        margin.Bottom = 8;
                        item.Margin = margin;
                    }
                    else
                    {
                        margin.Left = 20 + depths;
                        margin.Top = 8;
                        margin.Bottom = 8;
                        item.ARMargin = margin;
                    }

                    item.Header = Obj_Node.ObjName;
                    item.SubExpender = Lists;                        // 增加容器;
                    item.obj_type = Obj_Node;                        // 将节点添加到容器中;
                    item.Click += IsSelectedChanged;                 // 点击事件;

                    Obj_Tree.Add(item);

                    // 递归孩子节点;
                    Obj_Node.TraverseChildren(item, Lists, depths + 15);
                }
                
            }
        }

        /// <summary>
        /// 初始化参数列表;
        /// </summary>
        public ObjNode(int id, int pid, string version, string name)
        {
            this.version = version;
            this.ObjID = id;
            this.ObjParentID = pid;
            this.ObjName = name;
            IsSelectedChanged += ClickObjNode;
        }
        
    }
    /// <summary>
    /// 对象树*普通树枝*节点;
    /// </summary>
    class ObjTreeNode : ObjNode
    {
        public ObjTreeNode(int id, int pid, string version, string name) 
            : base(id, pid, version, name)
        {
            SubObj_Lsit = new List<ObjNode>();
        }

        /// <summary>
        /// 点击枝节点时，在右侧列表中更新叶子节点;
        /// </summary>
        /// <param name="sender">对应的容器</param>
        /// <param name="e"></param>
        public override void ClickObjNode(object sender, EventArgs e)
        {
            Console.WriteLine("TreeNode Clicked!");

            MetroExpander items = sender as MetroExpander;
            items.SubExpender.Children.Clear();

            // 将叶子节点加入右侧容器;
            foreach (var iter in (items.obj_type as ObjNode).SubObj_Lsit )
            {
                if (iter is ObjTreeNode)
                {
                    continue;
                }

                // 初始化对应的内容;
                MetroExpander subitems = new MetroExpander();
                subitems.Header = iter.ObjName;
                subitems.Click += iter.ClickObjNode;
                subitems.obj_type = iter;
                items.SubExpender.Children.Add(subitems);
            }
            
        }

        /// <summary>
        /// 添加孩子节点;
        /// </summary>
        /// <param name="obj">孩子节点</param>
        public override void Add(ObjNode obj)
        {
            SubObj_Lsit.Add(obj);
        }

        /// <summary>
        /// 删除孩子节点;
        /// </summary>
        /// <param name="obj">孩子节点</param>
        public override void Remove(ObjNode obj)
        {
            SubObj_Lsit.Remove(obj);
        }
        
        /// <summary>
        /// 点击树枝节点时的处理方法;
        /// </summary>
        public void ObjTreeNode_Click()
        {
            Console.WriteLine("TreeNode Clicked!");
        }

    }

    /// <summary>
    /// 对象树*普通叶子*节点;
    /// </summary>
    class ObjLeafNode : ObjNode
    {
        public ObjLeafNode(int id, int pid, string version, string name)
            : base(id, pid, version, name)
        {
        }

        public override void ClickObjNode(object sender, EventArgs e)
        {
            MetroExpander item = sender as MetroExpander;
            ObjNode node = item.obj_type as ObjNode;

            Console.WriteLine("LeafNode Clicked!" + node.ObjName);
            MetroExpander.NBContent_Grid.Visibility = Visibility.Visible;
            MetroExpander.NBBase_Grid.Visibility = Visibility.Hidden;
        }
        
        public override void Add(ObjNode obj)
        {
            throw new NotImplementedException();
        }

        public override void Remove(ObjNode obj)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 对象树*小区建立*节点;
    /// </summary>
    class ObjCellSetupNode : ObjNode
    {
        public ObjCellSetupNode(int id, int pid, string version, string name)
            : base(id, pid, version, name)
        {
        }

        public override void ClickObjNode(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void Add(ObjNode obj)
        {
            throw new NotImplementedException();
        }

        public override void Remove(ObjNode obj)
        {
            throw new NotImplementedException();
        }

    }

}
    

