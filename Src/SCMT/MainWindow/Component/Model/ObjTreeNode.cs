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
using UICore.Controls.Metro;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using SCMTOperationCore.Elements;
using SCMTMainWindow.Component.SCMTControl;
using SCMTMainWindow.Component.ViewModel;
using System.Collections.ObjectModel;
using LmtbSnmp;

namespace SCMTMainWindow
{
    /// <summary>
    /// 对象树节点抽象类;
    /// 说明:由此可衍生出各种类型的节点，诸如普通查询\小区\传输\分制式显示;
    /// </summary>
    public abstract class ObjNode
    {
        public string version { get; set; }                            // 对象树版本号;

        public int ObjID { get; set; }                                 // 节点ID;

        public int ObjParentID { get; set; }                           // 父节点ID;

        public string ObjName { get; set; }                            // 节点名称;

        public List<string> OIDList { get; set; }                      // 节点包含OID列表;

        public string ObjTableName { get; set; }                       // 节点对应的表名;

        public List<ObjNode> SubObj_Lsit { get; set; }                 // 孩子节点列表;

        public Grid m_NB_ContentGrid { get; set; }                     // 对应的MIB内容容器;

        public MetroScrollViewer m_NB_Base_Contain { get; set; }       // 对应的基站基本信息容器;

        public DataGrid m_NB_Content { get; set; }                     // 对应的MIB内容;

        protected Dictionary<string, string> name_cn { get; set; }     // 属性名与中文名对应关系;(后续独立挪到DataGridControl中)

        protected Dictionary<string, string> oid_cn { get; set; }      // oid与中文名对应关系;

        protected Dictionary<string, string> oid_en { get; set; }      // oid与英文名对应关系;

        protected ObservableCollection<DyDataGrid_MIBModel> 
            contentlist { get; set; }                                  // 用来保存内容;

        public static MainWindow main { get; set; }                    // 保存与之对应的主窗口;

        public static NodeB nodeb { get; set; }                        // 对应的基站;

        public static DTDataGrid datagrid { get; set; }                // 对应的界面表格;

        public abstract void Add(ObjNode obj);                         // 增加孩子节点;

        public abstract void Remove(ObjNode obj);                      // 删除孩子节点;

        public event EventHandler IsExpandedChanged;                   // 树形结构展开时;
        public event EventHandler IsSelectedChanged;                   // 树形结构节点被选择时;
        public event MouseButtonEventHandler IsRightMouseDown;         // 右键选择节点时;

        protected static string prev_oid = "1.3.6.1.4.1.5105.100.";    // DataBase模块保存的是部分OID，这个是前半部分;
        protected static Dictionary<string, string> GetNextResList;    // GetNext结果;
        protected static int LastColumn = 0;                           // 整行最后一个节点;

        public static string ObjParentOID { get; set; }                // 父节点OID;

        public static int IndexCount { get; set; }                     // 索引个数;

        public static int ChildCount { get; set; }                     // 孩子节点的个数;

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
            var margin = new Thickness();
            // 遍历所有对象树节点;
            foreach (var Obj_Node in SubObj_Lsit)
            {
                // 只有枝节点才能够加入对象树;
                if (Obj_Node is ObjTreeNode)
                {
                    // 新建一个对象树节点容器控件;
                    var item = new MetroExpander();
                    
                    // 判断孩子中是否还包含枝节点;
                    var NotContainTree = true;
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

        /// <summary>
        /// 遍历收藏夹节点;
        /// </summary>
        /// <param name="Obj_Tree"></param>
        /// <param name="Lists"></param>
        /// <param name="depths"></param>
        public void TraverseCollectChildren(MetroExpander Obj_Tree, StackPanel Lists, int depths)
        {
            var margin = new Thickness();
            // 遍历所有对象树节点;
            foreach (var Obj_Node in SubObj_Lsit)
            {
                // 只有枝节点才能够加入对象树;
                if (Obj_Node is ObjTreeNode)
                {
                    // 新建一个对象树节点容器控件;
                    var item = new MetroExpander();

                    // 判断孩子中是否还包含枝节点;
                    var NotContainTree = true;
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
                    var item = new MetroExpander();

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
        public ObjNode(int id, int pid, string version, string name, string tablename)
        {
            this.version = version;
            this.ObjID = id;
            this.ObjParentID = pid;
            this.ObjName = name;
            this.ObjTableName = tablename;
            IsSelectedChanged += ClickObjNode;
            IsRightMouseDown += ObjNode_IsRightMouseDown;
            name_cn = new Dictionary<string, string>();
            oid_cn = new Dictionary<string, string>();
            oid_en = new Dictionary<string, string>();
            contentlist = new ObservableCollection<DyDataGrid_MIBModel>();
            GetNextResList = new Dictionary<string, string>();
        }

        private void ObjNode_IsRightMouseDown(object sender, MouseButtonEventArgs e)
        {
            var abc = e.Source as MetroExpander;
            Console.WriteLine("111" + (abc.obj_type as ObjNode).ObjName);
            MainWindow.m_strNodeName = (abc.obj_type as ObjNode).ObjName;
        }

    }

    /// <summary>
    /// 对象树*普通树枝*节点;
    /// </summary>
    class ObjTreeNode : ObjNode
    {
        public ObjTreeNode(int id, int pid, string version, string name, string tablename) 
            : base(id, pid, version, name, tablename)
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
            var items = sender as MetroExpander;

            // 清理掉之前填入的Children节点;
            if (items != null)
            {
                items.SubExpender.Children.Clear();

                // 将叶子节点加入右侧容器;
                var node = items.obj_type as ObjNode;
                if (null == node)
                    return;

                if (node.SubObj_Lsit != null)
                {
                    foreach (var iter in ((ObjNode) items.obj_type).SubObj_Lsit)
                    {
                        // 子节点如果是枝节点跳过;
                        if (iter is ObjTreeNode)
                        {
                            continue;
                        }

                        // 初始化对应的内容,并加入到容器中;
                        var subitems = new MetroExpander
                        {
                            Header = iter.ObjName,
                            obj_type = iter
                        };

                        subitems.Click += iter.ClickObjNode;
                        items.SubExpender.Children.Add(subitems);
                    }

                    //// 该节点有对应的表可查;
                    //if (node.ObjTableName != @"/")
                    //{
                    //    ObjTreeNode_Click(node);
                    //}
                }
                //else
                //{
                //    // 该节点有对应的表可查;
                //    if (node.ObjTableName != @"/")
                //    {
                //        ObjTreeNode_Click(node);
                //    }
                //}
                // 该节点有对应的表可查;
                if (node.ObjTableName != @"/")
                {
                    ObjTreeNode_Click(node);
                }
            }
        }

        /// <summary>
        /// 添加孩子节点;
        /// </summary>
        /// <param name="obj">孩子节点</param>
        public override void Add(ObjNode obj)
        {
            var index = SubObj_Lsit.IndexOf(obj);
            if (index < 0)
            {
                SubObj_Lsit.Add(obj);
                //Console.WriteLine("222" + obj.ObjName);
            }
            //else
            //{
            //    Console.WriteLine("添加重复节点;");
            //}
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
        private void ObjTreeNode_Click(ObjNode node)
        {
            //Console.WriteLine("TreeNode Clicked to show info!");
            IReDataByTableEnglishName ret = new ReDataByTableEnglishName();
            var GetNextRet = new Dictionary<string, string>();
            var IndexNum = 0;
            contentlist.Clear();
            GetNextResList.Clear();
            ObjParentOID = string.Empty;

            // 目前可以获取到节点对应的中文名以及对应的表名;
            //Console.WriteLine("LeafNode Clicked!" + node.ObjName + " and TableName " + node.ObjTableName);

            var errorInfo = "";
            //根据表名获取该表内所有MIB节点;
            nodeb.db = Database.GetInstance();
            nodeb.db.getDataByTableEnglishName(node.ObjTableName, out ret, nodeb.m_IPAddress.ToString(), out errorInfo);

            var oidlist = new List<string>();             // 填写SNMP模块需要的OIDList;
            name_cn.Clear();
            oid_cn.Clear();
            oid_en.Clear();         // 每个节点都有自己的表数据结构;

            try
            {
                int.TryParse(ret.indexNum, out IndexNum);              // 获取这张表索引的个数;
                IndexCount = int.Parse(ret.indexNum);
                LastColumn = 0;                                        // 初始化判断整表是否读完的判断字段;
                ChildCount = ret.childrenList.Count - IndexNum;
                ObjParentOID = ret.oid;                                // 将父节点OID赋值;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            // 遍历所有子节点，组SNMP的GetNext命令的一行OID集合;
            foreach (var iter in ret.childrenList)
            {
                oidlist.Clear();
                // 索引不参与查询,将所有其他孩子节点进行GetNext查询操作;
                if (int.Parse(iter.childNo) > IndexNum)
                {
                    // 如果不是真MIB，不参与查询;
                    if (iter.isMib != "1")
                    {
                        ChildCount--;
                        continue;
                    }

                    var temp = prev_oid + iter.childOid;
                    name_cn.Add(prev_oid + iter.childNameMib, iter.childNameCh);
                    oid_en.Add(prev_oid + iter.childOid, iter.childNameMib);
                    oid_cn.Add(prev_oid + iter.childOid, iter.childNameCh);
                    oidlist.Add(temp);

                    // 通过GetNext查询单个节点数据;
                    var msg = new SnmpMessageV2c("public", nodeb.m_IPAddress.ToString());
                    msg.GetNextRequestWhenStop(new AsyncCallback(ReceiveResBySingleNode), new AsyncCallback(NotifyMainUpdateDataGrid), oidlist);
                }
                else
                {
                    //ty:增加索引的信息
                    name_cn.Add(prev_oid + iter.childNameMib, iter.childNameCh);
                    oid_en.Add(prev_oid + iter.childOid, iter.childNameMib);
                    oid_cn.Add(prev_oid + iter.childOid, iter.childNameCh);
                }

                // 如果是单个节点遍历，就只能在此处组DataGrid的VM类;
            }

            // 通过GetNext获取整表数据，后来发现基站不支持,如果基站支持后，在此处GetNext即可;
            //SnmpMessageV2c msg = new SnmpMessageV2c("public", nodeb.m_IPAddress.ToString());
            //msg.GetNextRequest(new AsyncCallback(ReceiveRes), oidlist);

        }

        /// <summary>
        /// 按照单个节点进行GetNext;
        /// 该函数将所有数据收集完成后再通知主界面DataGrid更新;
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveResBySingleNode(IAsyncResult ar)
        {
            var res = ar as SnmpMessageResult;

            // 遍历GetNext结果，添加到对应容器当中,GetNextResList容器中保存着全量集;
            foreach (var iter in res.AsyncState as Dictionary<string, string>)
            {
                GetNextResList.Add(iter.Key, iter.Value);
            }

        }
        /// <summary>
        /// ReceiveResBySingleNode的GetNext函数收集完成之后，调用主界面更新DataGrid
        /// </summary>
        /// <param name="ar"></param>
        private void NotifyMainUpdateDataGrid(IAsyncResult ar)
        {
            LastColumn++;

            // 全部节点都已经收集完毕;
            if (LastColumn == ChildCount)
            {
                main.UpdateAllMibDataGrid(GetNextResList, oid_cn, oid_en, contentlist, ObjParentOID, IndexCount);
            }
        }
    }

    /// <summary>
    /// 对象树*普通叶子*节点;
    /// </summary>
    class ObjLeafNode : ObjNode
    {
        public ObjLeafNode(int id, int pid, string version, string name, string tablename)
            : base(id, pid, version, name, tablename)
        {
        }

        /// <summary>
        /// 当点击叶子节点时，会触发GetNext操作;
        /// 注意：基站GetNext不支持全节点查询，最大粒度为Get命令当中的节点数量;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ClickObjNode(object sender, EventArgs e)
        {
            var item = sender as MetroExpander;
            var node = item.obj_type as ObjNode;
            IReDataByTableEnglishName ret = new ReDataByTableEnglishName();
            var GetNextRet = new Dictionary<string, string>();
            var IndexNum = 0;
            contentlist.Clear();
            GetNextResList.Clear();
            ObjParentOID = String.Empty;

            // 目前可以获取到节点对应的中文名以及对应的表名;
            Console.WriteLine("LeafNode Clicked!" + node.ObjName + "and TableName " +this.ObjTableName);

            var errorInfo = "";
            //根据表名获取该表内所有MIB节点;
            nodeb.db = Database.GetInstance();
            nodeb.db.getDataByTableEnglishName(this.ObjTableName, out ret, nodeb.m_IPAddress.ToString(), out errorInfo);
            
            var oidlist = new List<string>();             // 填写SNMP模块需要的OIDList;
            name_cn.Clear();oid_cn.Clear();oid_en.Clear();         // 每个节点都有自己的表数据结构;
            try
            {
                int.TryParse(ret.indexNum, out IndexNum);              // 获取这张表索引的个数;
                IndexCount = int.Parse(ret.indexNum);
                LastColumn = 0;                                        // 初始化判断整表是否读完的判断字段;
                ChildCount = ret.childrenList.Count - IndexNum;
                ObjParentOID = ret.oid;                                // 将父节点OID赋值;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            // 遍历所有子节点，组SNMP的GetNext命令的一行OID集合;
            foreach (var iter in ret.childrenList)
            {
                oidlist.Clear();
                // 索引不参与查询,将所有其他孩子节点进行GetNext查询操作;
                if(int.Parse(iter.childNo) > IndexNum )
                {
                    // 如果不是真MIB，不参与查询;
                    if (iter.isMib != "1")
                    {
                        ChildCount--;
                        continue;
                    }

                    var temp = prev_oid + iter.childOid;
                    name_cn.Add(prev_oid + iter.childNameMib, iter.childNameCh);
                    oid_en.Add(prev_oid + iter.childOid, iter.childNameMib);
                    oid_cn.Add(prev_oid + iter.childOid, iter.childNameCh);
                    oidlist.Add(temp);

                    // 通过GetNext查询单个节点数据;
                    var msg = new SnmpMessageV2c("public", nodeb.m_IPAddress.ToString());
                    msg.GetNextRequestWhenStop(new AsyncCallback(ReceiveResBySingleNode),new AsyncCallback(NotifyMainUpdateDataGrid) ,oidlist);
                }
                else
                {
                    //ty:增加索引的信息
                    name_cn.Add(prev_oid + iter.childNameMib, iter.childNameCh);
                    oid_en.Add(prev_oid + iter.childOid, iter.childNameMib);
                    oid_cn.Add(prev_oid + iter.childOid, iter.childNameCh);

                }

                // 如果是单个节点遍历，就只能在此处组DataGrid的VM类;
            }

            // 通过GetNext获取整表数据，后来发现基站不支持,如果基站支持后，在此处GetNext即可;
            //SnmpMessageV2c msg = new SnmpMessageV2c("public", nodeb.m_IPAddress.ToString());
            //msg.GetNextRequest(new AsyncCallback(ReceiveRes), oidlist);

        }

        /// <summary>
        /// 每当收集完一行数据后，更新主界面中的DataGrid;
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveRes(IAsyncResult ar)
        {
            main.UpdateMibDataGrid(ar, oid_cn, oid_en, contentlist);
        }

        /// <summary>
        /// 按照单个节点进行GetNext;
        /// 该函数将所有数据收集完成后再通知主界面DataGrid更新;
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveResBySingleNode(IAsyncResult ar)
        {
            var res = ar as SnmpMessageResult;

            // 遍历GetNext结果，添加到对应容器当中,GetNextResList容器中保存着全量集;
            foreach (var iter in res.AsyncState as Dictionary<string, string>)
            {
                GetNextResList.Add(iter.Key, iter.Value);
            }
        }

        /// <summary>
        /// ReceiveResBySingleNode的GetNext函数收集完成之后，调用主界面更新DataGrid
        /// </summary>
        /// <param name="ar"></param>
        private void NotifyMainUpdateDataGrid(IAsyncResult ar)
        {
            LastColumn++;

            // 全部节点都已经收集完毕;
            if(LastColumn == ChildCount)
            {
                main.UpdateAllMibDataGrid(GetNextResList, oid_cn, oid_en, contentlist, ObjParentOID, IndexCount);
            }
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
        public ObjCellSetupNode(int id, int pid, string version, string name, string tablename)
            : base(id, pid, version, name, tablename)
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
