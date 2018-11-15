using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CfgFileOpStruct;
using System.Runtime.InteropServices;

namespace CfgFileOperation
{
    /*  table */
    /// <summary>
    /// 
    /// </summary>
    class CfgTableOp
    {
        /*********************        变量                         ***************************/
        uint u32CurTblOffset;                     //  每个索引项 每个表的数据在文件中的起始位置（相对文件头） init.cfg中使用        
        uint u32CurTblOffset_PDG;                 //  作用与u32CurTblOffset相同, patch_ex.cfg中使用，如果这个表不需要写patch,则不赋值.
        bool m_isMibInfoInitial;                  //  信息是否读取过 ? 作用是啥？？？
        /// <summary>
        /// table 的索引数
        /// </summary>
        public int  m_tabDimen;                   //  索引数
        uint m_dyTabContent;                      //  动态表的表容量
        string m_strChFriendName;                 //  
        public string m_strTableName;             //  表名(En)
        string m_strOID;                          //  OID
        bool m_isDyTable;                         //  是否是


        /// <summary>
        /// 叶子节点. string:MIBName, 
        /// </summary>
        public List<CfgFileLeafNodeOp> m_LeafNodes;//MIBName
        public CfgFileLeafNodeIndexInfoOp m_struIndex;
        /// <summary>
        /// 告警
        /// </summary>
        //SetValueToByteArrayReverse
        /// <summary>
        /// 表中每个实例的
        /// </summary>
        public List<CfgTableInstanceInfos> m_cfgInsts; //表中每个实例的
        public void m_cfgInsts_add(string strInstantNumVal, byte[] InstMemVal){ m_cfgInsts.Add(new CfgTableInstanceInfos( strInstantNumVal,  InstMemVal));}
        /// <summary>
        /// 获取实例的个数
        /// </summary>
        /// <returns></returns>
        public uint get_m_cfgInsts_num() { return (uint)m_cfgInsts.LongCount(); }
        /// <summary>
        /// 获取所有实例的索引值
        /// </summary>
        /// <returns></returns>
        public List<string> GetCfgInstsInstantNum()
        {
            List<string> strIndexL = new List<string>();
            foreach (var info in m_cfgInsts)
                strIndexL.Add(info.GetInstantNum());
            return strIndexL;
        }
        /// <summary>
        /// 表信息
        /// </summary>
        public StruCfgFileTblInfo  m_cfgFile_TblInfo;    //  表信息
        //CDTMapNodesInfo m_mapNodesInfo;    //叶子名和其信息的映射
        //CDTMapTableInstInfo        m_mapInstInfo;   //索引和实例信息的映射
        //CDTCfgInsts m_cfgInsts;        //表实例
        //CDTIndexMibNodes m_MibNodes;        //读取数据库的信息

        /*********************        reclist 解析后存在的变量                         ***************************/
        /// <summary>
        /// reclist 解析后存在的变量 用于记录补丁文件;
        /// key : 索引，value ：表实例,即所有叶子节点实例取值.
        /// </summary>
        public Dictionary <string, List<string>> m_InstIndex2LeafName = new Dictionary<string, List<string>>();//用于记录补丁文件 key :".0"
        
        //string m_TableRowStatusName = "";
        ////设置RowStatusName
        //public void SetRowStatusName(string strLeafName)
        //{
        //    m_TableRowStatusName = strLeafName;
        //}
        /// <summary>
        /// 查找 leaf中为"RowStatus"的节点名
        /// </summary>
        /// <returns></returns>
        public string GetRowStatusName()
        {
            foreach (var leafOp in m_LeafNodes)
            {
                if (0 == String.Compare(leafOp.m_struMibNode.strMibSyntax, "RowStatus", true))
                    return leafOp.m_struMibNode.strMibName;
            }
            return "";
        }       
        /// <summary>
        /// 通过 leaf name en 查找 Class LeafNodeOp
        /// </summary>
        /// <param name="strMibNodeName"></param>
        /// <param name="leafNodeOp"></param>
        /// <returns></returns>
        public bool GetLeafNodesByNodeName(string strMibNodeName, out CfgFileLeafNodeOp leafNodeOp)
        {
            int Nodeindex = m_LeafNodes.FindIndex(e => String.Compare(e.m_struMibNode.strMibName, strMibNodeName, true) == 0);
            if (-1 == Nodeindex)
            {
                leafNodeOp = null;
                return false;
            }
            else
            {
                leafNodeOp = m_LeafNodes[Nodeindex];
                return true;
            }
        }
        /// <summary>
        /// 通过索引查找实例
        /// </summary>
        /// <param name="strIndex"></param>
        /// <param name="InstInfoOp"></param>
        /// <returns></returns>
        public bool GetCfgInstsByIndex(string strIndex, out CfgTableInstanceInfos InstInfoOp, out int InstsPos)
        {
            InstsPos = m_cfgInsts.FindIndex(e => e.GetInstantNum() == strIndex);
            if (InstsPos != -1)
            {
                InstInfoOp = m_cfgInsts[InstsPos];// m_cfgInsts.Find(e => e.GetInstantNum() == strIndex);
                return true;
            }
            else
            {
                InstInfoOp = null;
                return false;
            }
        }
        /// <summary>
        /// 方便reclist修改指定值
        /// </summary>
        /// <param name="index">要修改的实例的索引</param>
        /// <param name="newMem">实例的新数值</param>
        /// <returns></returns>
        public bool SetInstsInfoValueByIndex(int index, byte[] newMem)
        {
            if (newMem == null)
                return false;
            m_cfgInsts[index].ReclistSetInstMem(newMem);
            return true;
        }
        /// <summary>
        /// 用于制作补丁文件
        /// </summary>
        /// <param name="RecordIndex">OID</param>
        /// <param name="strLeafName"></param>
        public void InsertInstOrLeaf(string RecordIndex, string strLeafName)
        {
            if (m_InstIndex2LeafName.ContainsKey(RecordIndex))
            {
                var listLeafName = m_InstIndex2LeafName[RecordIndex];
                if(-1 == listLeafName.FindIndex(e => String.Compare(e, strLeafName, true) == 0))
                    m_InstIndex2LeafName[RecordIndex].Add(strLeafName);
            }
            else
                m_InstIndex2LeafName.Add(RecordIndex, new List<string>() { strLeafName });
        }
        
        /*********************        reclist 解析后存在的变量                         ***************************/


        /*********************        功能函数(公有)               **************************/
        public CfgTableOp()
        {
            m_isMibInfoInitial = false;
            // m_isScalar = FALSE;
            m_isDyTable = false;
            // m_tabDimen = 0;
            // m_TableRowStatusName = "";
            m_cfgFile_TblInfo = new StruCfgFileTblInfo("init");
            m_LeafNodes = new List<CfgFileLeafNodeOp>();
            m_cfgInsts = new List<CfgTableInstanceInfos>();
            m_struIndex = new CfgFileLeafNodeIndexInfoOp();
        }

        ////////////////////////////////////////
        // 函数
        /// <summary>
        /// 获取 表的偏移量
        /// </summary>
        /// <returns></returns>
        public uint GetTableOffset(){return u32CurTblOffset;}
        /// <summary>
        /// 设置表的偏移量
        /// </summary>
        /// <param name="tableOffset"></param>
        public void SetTableOffset(uint tableOffset){u32CurTblOffset = tableOffset;}
        /// <summary>
        /// 设置表在patch中的偏移量
        /// </summary>
        /// <param name="tableOffset"></param>
        public void SetTableOffsetPDG(uint tableOffset) { u32CurTblOffset_PDG = tableOffset; }
        /// <summary>
        /// 获取表在patch中的偏移量
        /// </summary>
        /// <returns></returns>
        public uint GetTableOffsetPDG() { return u32CurTblOffset_PDG; }
        /// <summary>
        /// //是否初始化完成
        /// </summary>
        /// <param name="isMibInit"></param>
        public void SetMibInit(bool isMibInit) { m_isMibInfoInitial = isMibInit; }
        /// <summary>
        /// 计算所有 表的叶子占位长度的总和
        /// </summary>
        /// <returns></returns>
        public ushort GetAllLeafsFieldLens()
        {
            ushort bufLen = 0;//字段总长
            if (m_LeafNodes != null && m_LeafNodes.LongCount() > 0)
            {
                foreach (var leaf in m_LeafNodes)
                {
                    bufLen += leaf.m_struFieldInfo.u16FieldLen;
                }
                return bufLen;
            }
            else
                return bufLen;
        }

        public string GetChFriendName() { return m_strChFriendName; }//add yangyuming
        public void SetChFriendName(string chFriendName) { m_strChFriendName = chFriendName; }//add yangyuming
        public string GetTabOID() { return m_strOID; }
        public void SetTabOID(string strIOD) { m_strOID = strIOD; }
        public uint GetDytabCont() { return m_dyTabContent; }
        public void SetDytabCont(string strTableContent)
        {
            if (isDynamicTable( strTableContent))
                m_dyTabContent = (uint)int.Parse(strTableContent);//设置动态表的容量
        }
        /// <summary>
        /// 根据属性"TableContent"判断是否为动态表
        /// </summary>
        /// <param name="strTableContent">判断依据</param>
        /// <returns></returns>
        public bool isDynamicTable(string strTableContent)
        {
            if (String.Equals("0", strTableContent) || String.Empty == strTableContent)
                return false;
            if (!IsNumeric(strTableContent))
                return false;
            return true;
        }
        /// <summary>
        /// 动态表
        /// </summary>
        /// <param name="bDyTable"></param>
        public void SetDyTable(bool bDyTable) { m_isDyTable = bDyTable; }
        /// <summary>
        /// 判断字符串是否是数字
        /// </summary>
        /// <param name="InStr"></param>
        /// <returns></returns>
        public bool IsNumeric(string InStr)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(InStr))
                return true;
            return false;
        }
        /// <summary>
        /// 设置表名
        /// </summary>
        /// <param name="strTabName"></param>
        public void SetTabName(string strTabName){ m_strTableName = strTabName;}

        /// <summary>
        /// 表实例,以字节的形式写入配置文件;(1.表头;2.叶子头;3.实例信息.)
        /// </summary>
        /// <returns></returns>
        public List<byte> WriteTofile()
        {
            List<byte> tableInfo = new List<byte>();

            //1.表节点头部信息 StruCfgFileTblInfo
            //fwrite(&m_cfgFile_TblInfo, 1, sizeof(OM_STRU_CfgFile_TblInfo), pcfgOut);//节点头部信息
            tableInfo.AddRange(m_cfgFile_TblInfo.StruToByteArray());

            //2.每个叶子的头
            foreach (var leaf in m_LeafNodes)
            {
                tableInfo.AddRange(leaf.m_struFieldInfo.StruToByteArray());
            }

            //3.每个叶子的内容 * 每个表实例
            uint bufflong = GetAllLeafsFieldLens();
            uint instNum = 0;
            foreach (var inst in m_cfgInsts)
            {
                tableInfo.AddRange(inst.GetInstMem());
                instNum++;
            }
            if (instNum != m_cfgFile_TblInfo.u32RecNum)
                Console.WriteLine(String.Format("Err : {0} 实例计算错误 m_cfgInsts num is {1}, 表头 Num is {2}。。。。。。", m_strTableName, instNum, m_cfgFile_TblInfo.u32RecNum));
            
            return tableInfo;
        }
        /// <summary>
        /// 表块 的序列化: patch_ex
        /// </summary>
        /// <returns></returns>
        public List<byte> WriteTofilePDG()
        {
            List<byte> tableInfo = new List<byte>();

            //1.表头  StruCfgFileTblInfo
            tableInfo.AddRange(m_cfgFile_TblInfo.StruToByteArray());

            //2.每个叶子的头
            foreach (var leaf in m_LeafNodes)
            {
                // 标记 节点是否可配置
                SetLeafFieldConfigFlagPDG(leaf.m_struFieldInfo, leaf.m_struMibNode.strMibName);
                // 每个叶子节点的头
                tableInfo.AddRange(leaf.m_struFieldInfo.StruToByteArray());
            }

            //3. 表实例 : 表的每个叶子的内容 * 每个表实例
            foreach (var InstleafName in m_InstIndex2LeafName)
            {
                string strInstIndex = InstleafName.Key;
                CfgTableInstanceInfos inst = m_cfgInsts.Find(e => String.Compare(e.GetInstantNum(), strInstIndex, true) == 0);
                if (null != inst)
                    tableInfo.AddRange(inst.GetInstMem());
            }
            return tableInfo;
        }
        
        void SetLeafFieldConfigFlagPDG(StruCfgFileFieldInfo FieldInfo, string strNodeName)
        {
            bool bFind = false;
            foreach (var leafNames in m_InstIndex2LeafName.Values)
            {
                if (-1 != leafNames.FindIndex(e => String.Compare(e, strNodeName, true) == 0))
                {
                    bFind = true;
                    break;
                }
            }

            //如果节点本身是不可配置的即使被选中也是不可配置的，如果是可配置的节点在没有被选中的情况下
            //也是不可配置的。索引字段全部为可配置的。add by yangyuming
            if (true == bFind)
            {
                if (FieldInfo.u8ConfigFlag != (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_NOT_CONFIG_FILE)
                    FieldInfo.u8ConfigFlag = (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_CONFIG_FILE;
            }
            else
            {
                if (FieldInfo.u8ConfigFlag == (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_CONFIG_FILE)
                    FieldInfo.u8ConfigFlag = (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_NOT_CONFIG_FILE;
                else if (FieldInfo.u8ConfigFlag == (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_NOT_CONFIG_FILE && FieldInfo.u8FieldTag == 'Y')
                {
                    FieldInfo.u8ConfigFlag = (byte)MacroDefinition.CONFIGFILEORNOT.OM_IS_CONFIG_FILE;
                }
            }

        }
    }
}
