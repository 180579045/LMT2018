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
using UICore.Controls.Metro;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

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
        public List<ObjNode> SubObj_Lsit { get; set; }           // 孩子节点列表;

        public Grid m_NB_ContentGrid { get; set; }               // 对应的MIB内容容器;
        public MetroScrollViewer m_NB_Base_Contain { get; set; } // 对应的基站基本信息容器;
        public DataGrid m_NB_Content { get; set; }               // 对应的MIB内容;

        abstract public void Add(ObjNode obj);                   // 增加孩子节点;
        abstract public void Remove(ObjNode obj);                // 删除孩子节点;

        public event EventHandler IsExpandedChanged;             // 树形结构展开时;
        public event EventHandler IsSelectedChanged;             // 树形结构节点被选择时;
        public event MouseButtonEventHandler IsRightMouseDown;   // 右键选择节点时;

        /// <summary>
        /// 对象树节点点击事件;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void ClickObjNode(object sender, EventArgs e);
        
        /// <summary>
        /// 递归某个节点的所有孩子节点，并填入对象树容器中;
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
                    // 新建一个对象树节点容器控件;
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
                    item.SubExpender = Lists;                           // 增加子容器,保存叶子节点;
                    item.obj_type = Obj_Node;                           // 将节点添加到容器控件中;
                    item.Click += IsSelectedChanged;                    // 点击事件;
                    item.MouseRightButtonDown += IsRightMouseDown;

                    Obj_Tree.Add(item);

                    // 递归孩子节点;
                    Obj_Node.TraverseChildren(item, Lists, depths + 15);
                }
                
            }
        }

        public void TraverseCollectChildren(MetroExpander Obj_Tree, StackPanel Lists, int depths)
        {
            Thickness margin = new Thickness();
            // 遍历所有对象树节点;
            foreach (var Obj_Node in SubObj_Lsit)
            {
                // 只有枝节点才能够加入对象树;
                if (Obj_Node is ObjTreeNode)
                {
                    // 新建一个对象树节点容器控件;
                    MetroExpander item = new MetroExpander();

                    // 判断孩子中是否还包含枝节点;
                    bool NotContainTree = true;
                    if (Obj_Node.SubObj_Lsit != null)
                    {
                        foreach (var isTree in Obj_Node.SubObj_Lsit)
                        {
                            if (isTree is ObjTreeNode)
                            {
                                NotContainTree = false;
                            }
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
                    item.SubExpender = Lists;                           // 增加子容器,保存叶子节点;
                    item.obj_type = Obj_Node;                           // 将节点添加到容器控件中;
                    item.Click += IsSelectedChanged;                    // 点击事件;
                    //item.MouseRightButtonDown += IsRightMouseDown;

                    Obj_Tree.Add(item);
                    // 递归孩子节点;
                    Obj_Node.TraverseChildren(item, Lists, depths + 15);
                }
                else
                {
                    // 新建一个对象树节点容器控件;
                    MetroExpander item = new MetroExpander();

                    margin.Left = 35 + depths;
                    margin.Top = 8;
                    margin.Bottom = 8;
                    item.Margin = margin;

                    item.Header = Obj_Node.ObjName;
                    item.SubExpender = Lists;                           // 增加子容器,保存叶子节点;
                    item.obj_type = Obj_Node;                           // 将节点添加到容器控件中;
                    item.Click += IsSelectedChanged;                    // 点击事件;
                    //item.MouseRightButtonDown += IsRightMouseDown;

                    Obj_Tree.Add(item);
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
            IsRightMouseDown += ObjNode_IsRightMouseDown;
        }

        private void ObjNode_IsRightMouseDown(object sender, MouseButtonEventArgs e)
        {

            MetroExpander abc = e.Source as MetroExpander;
            Console.WriteLine("111" + (abc.obj_type as ObjNode).ObjName);
            MainWindow.StrNodeName = (abc.obj_type as ObjNode).ObjName;
            //menu.Show(control, new Point(e.X, e.Y));   //在点(e.X, e.Y)处显示menu


            //MetroExpander abc = e.Source as MetroExpander;
            //Console.WriteLine("111" + (abc.obj_type as ObjNode).ObjName);
            ////this.ObjName = (abc.obj_type as ObjNode).ObjName;
            //MenuItem[] formMenuItemList = new MenuItem[1];
            ////formMenuItemList[0] = new MenuItem("添加到收藏", null, new EventHandler(AddToCollect(sender, e)));
            //formMenuItemList[0] = new MenuItem();
            //ContextMenu formMenu = new ContextMenu();
            //formMenu.Items.Add(formMenuItemList);
            //this.ContextMenu = formMenu;
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
        /// 点击枝节点时;
        /// 1、在右侧列表中更新叶子节点;
        /// 2、触发SNMP的GetNext查询;
        /// </summary>
        /// <param name="sender">对应的容器</param>
        /// <param name="e"></param>
        public override void ClickObjNode(object sender, EventArgs e)
        {
            Console.WriteLine("TreeNode Clicked!");

            MetroExpander items = sender as MetroExpander;

            // 清理掉之前填入的Children节点;
            items.SubExpender.Children.Clear();

            // 将叶子节点加入右侧容器;
            if ((items.obj_type as ObjNode).SubObj_Lsit != null)
            {
                foreach (var iter in (items.obj_type as ObjNode).SubObj_Lsit)
                {
                    // 子节点如果是枝节点跳过;
                    if (iter is ObjTreeNode)
                    {
                        continue;
                    }

                    // 初始化对应的内容,并加入到容器中;
                    MetroExpander subitems = new MetroExpander();
                    subitems.Header = iter.ObjName;
                    subitems.Click += iter.ClickObjNode;
                    subitems.obj_type = iter;
                    items.SubExpender.Children.Add(subitems);
                }
            }
            else
            {
                MetroExpander item = sender as MetroExpander;
                ObjNode node = item.obj_type as ObjNode;

                Console.WriteLine("LeafNode Clicked!" + node.ObjName);
            }

        }

        /// <summary>
        /// 添加孩子节点;
        /// </summary>
        /// <param name="obj">孩子节点</param>
        public override void Add(ObjNode obj)
        {
            int index = SubObj_Lsit.IndexOf(obj);
            if (index < 0)
            {
                SubObj_Lsit.Add(obj);
                Console.WriteLine("222" + obj.ObjName);
            }
            else
            {
                Console.WriteLine("添加重复节点;");
            }
            //SubObj_Lsit.Add(obj);
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
    

