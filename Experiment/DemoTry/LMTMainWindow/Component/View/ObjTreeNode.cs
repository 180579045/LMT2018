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


namespace LMTMainWindow
{
    /// <summary>
    /// 对象树节点抽象类;
    /// 说明:由此可衍生出各种类型的节点，诸如普通查询\小区\传输\分制式显示;
    /// </summary>
    abstract class ObjNode
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

        public abstract void OnClickNode();                      // 树形结构节点被选择时;

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
        public override void OnClickNode()
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

        /// <summary>
        /// 点击叶子节点时的处理方法;
        /// </summary>
        public override void OnClickNode()
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

        /// <summary>
        /// 点击节点时的处理方法;
        /// </summary>
        public override void OnClickNode()
        {
            Console.WriteLine("CellSetupNode Clicked!");
        }
    }

}
    

