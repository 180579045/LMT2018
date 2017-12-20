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
    public delegate void OnNodeClick();
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
        /// 将所有节点填入对象树中;
        /// </summary>
        public void TraverseChildren(MetroExpander Obj_Tree, StackPanel Lists, int depths)
        {
            foreach(var items in SubObj_Lsit)
            {
                if (items is ObjTreeNode)
                {
                    MetroExpander item = new MetroExpander();
                    

                    bool NotContainTree = true;
                    foreach(var isTree in items.SubObj_Lsit)
                    {
                        if(isTree is ObjTreeNode)
                        {
                            NotContainTree = false;
                        }
                    }
                    
                    if (NotContainTree)
                    {
                        item.Header = "   " + items.ObjName;
                    }
                    else
                    {
                        item.Header = items.ObjName;
                    }
                    item.SubExpender = Lists;
                    item.obj_type = items;
                    item.Click += IsSelectedChanged;

                    Obj_Tree.Add(item);
                    items.TraverseChildren(item, Lists, depths + 5);
                }
//                 else if (items is ObjLeafNode)
//                 {
//                     MetroExpander item = new MetroExpander();
//                     item.Header = items.ObjName;
//                     Lists.Children.Add(item);
//                 }
                
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
            IsSelectedChanged += ObjTreeNode_IsSelectedChanged;
        }

        private void ObjTreeNode_IsSelectedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("TreeNode Clicked!");
            MetroExpander items = sender as MetroExpander;
            items.SubExpender.Children.Clear();
            foreach (var iter in (items.obj_type as ObjNode).SubObj_Lsit )
            {
                MetroExpander subitems = new MetroExpander();
                subitems.Header = iter.ObjName;
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
            IsSelectedChanged += ObjLeafNode_IsSelectedChanged;
        }

        private void ObjLeafNode_IsSelectedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("LeafNode Clicked!");
        }

        /// <summary>
        /// 点击叶子节点时的处理方法;
        /// </summary>
        public void ObjLeafNode_Click()
        {
            Console.WriteLine("LeafNode Clicked!");
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
    

