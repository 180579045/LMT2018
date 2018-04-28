/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $SendUIToJsonInterface$
* 机器名称：       $machinename$
* 命名空间：       $SCMT_json.Interface$
* 文 件 名：       $SendUIToJsonInterface.cs$
* 创建时间：       $2018.04.08$
* 作    者：       $luanyibo$
* 说   明 ：
*     UI与json模块的交互消息格式。
* 修改时间     修 改 人    修改内容：
* 2018.04.08   栾义博      创建文件并实现类  UIToJsonInterface
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMT_json.Interface.UISnmp
{
    /// <summary>
    /// UI 与 Json 模块交互的接口
    /// </summary>
    class UIInfoInterface
    {
        //UI 填写
        private string m_requestId;
        //UI 填写
        private string m_messageType;
        //UI ,UI->SNMP方向:不用填写 
        private string m_errorStatus;
        //错误索引  UI不用填写
        private string m_errorIndex;
        //UI 填写 
        private string m_tableNameMib;
        //UI 填写  表索引
        private string m_indexContent;
        private List<UILeafInfo> m_leafLists;


        public List<UILeafInfo> LeafLists
        {
            get { return m_leafLists; }
            set { m_leafLists = value; }
        }

        public void addNewLeafList(string childNameMib, string value)
        {
            UILeafInfo leaf = new UILeafInfo();
            leaf.ChildNameMib = childNameMib;
            leaf.Value = value;

            if (m_leafLists == null)
            {
                m_leafLists = new List<UILeafInfo>();                
            }
            m_leafLists.Add(leaf);
        }

        public string RequestId
        {
            get { return m_requestId; }
            set { m_requestId = value; }
        }

        public string MessageType
        {
            get { return m_messageType; }
            set { m_messageType = value; }
        }

        public string ErrorStatus
        {
            get { return m_errorStatus; }
            set { m_errorStatus = value; }
        }

        public string ErrorIndex
        {
            get { return m_errorIndex; }
            set { m_errorIndex = value; }
        }

        public string TableNameMib
        {
            get { return m_tableNameMib; }
            set { m_tableNameMib = value; }
        }

        public string IndexContent
        {
            get { return m_indexContent; }
            set { m_indexContent = value; }
        }

    }

    /// <summary>
    /// list leafinfo 叶子节点信息
    /// </summary>
    class UILeafInfo
    {
        private string m_childNameMib;
        public string ChildNameMib
        {
            get { return m_childNameMib; }
            set { m_childNameMib = value; }
        }

        private string m_value;
        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
    }


    /// <summary>
    /// Json模块返回给UI模块的SNMP信息
    /// </summary>
    class SNMPInfoInterface
    {
        //UI 填写
        private string m_requestId;
        //UI 填写
        private string m_messageType;
        //UI ,UI->SNMP方向:不用填写 
        private string m_errorStatus;
        //错误索引  UI不用填写
        private string m_errorIndex;

        private List<SnmpOidLeafInfo> m_oidLeafLists;


        public List<SnmpOidLeafInfo> OidLeafLists
        {
            get { return m_oidLeafLists; }
            set { m_oidLeafLists = value; }
        }

        public void addNewOidLeafList(string Oid, string value)
        {
            SnmpOidLeafInfo leaf = new SnmpOidLeafInfo();
            leaf.Oid = Oid;
            leaf.Value = value;

            if (m_oidLeafLists == null)
            {
                m_oidLeafLists = new List<SnmpOidLeafInfo>();
            }
            m_oidLeafLists.Add(leaf);
        }

        public string RequestId
        {
            get { return m_requestId; }
            set { m_requestId = value; }
        }

        public string MessageType
        {
            get { return m_messageType; }
            set { m_messageType = value; }
        }

        public string ErrorStatus
        {
            get { return m_errorStatus; }
            set { m_errorStatus = value; }
        }

        public string ErrorIndex
        {
            get { return m_errorIndex; }
            set { m_errorIndex = value; }
        }

    }

    class SnmpOidLeafInfo
    {
        private string m_oid;
        public string Oid
        {
            get { return m_oid; }
            set { m_oid = value; }
        }

        private string m_value;
        public string Value
        {
            get { return m_value; }
            set { m_value = value; }
        }
    }
}
