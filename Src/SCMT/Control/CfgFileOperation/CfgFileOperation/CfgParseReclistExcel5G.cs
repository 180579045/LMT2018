using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfgFileOperation
{
    /// <summary>
    /// 1. 目标是什么？
    /// 5G 要求根据参数的选择可以生成多个 patch
    /// 2. 解析规则是相同的
    /// 3. patch的类型是多种的(5种)
    /// 默认(放入bbu打包中)、恢复默认配置、(各个终端:)展讯、e500、华为。
    /// 4. 数据存在问题？
    /// 因为只处理标识为0，1，2的，保存表和要处理的节点的相关信息
    /// </summary>
    class CfgParseReclistExcel5G
    {
        /// <summary>
        /// 写文件句柄
        /// </summary>
        BinaryWriter g_bw = null;
        /// <summary>
        /// reclist 文件地址
        /// </summary>
        string g_strExcelPath = "";
        //2014-2-12 luoxin 当前索引
        string m_strCurTabAndIndex = "";
        /// <summary>
        /// 某种终端要处理的表
        /// </summary>
        List<ReclistTable> RecTable;
        /// <summary>
        /// 某种终端要处理的表名
        /// </summary>
        List<string> m_vectPDGTabName = new List<string>();

        /// <summary>
        /// 5G sheet:Cell参数表、gNB参数表 通用
        /// </summary>
        Dictionary<string, string> SameCol = new Dictionary<string, string>(){
            { "NodeName", "A"},          // 节点名
            { "DefaultValue", "E"},      // 默认值     标识为1使用
            { "End","O"},                // 结束标志 
        };

        /// <summary>
        /// 5G 生成
        /// <处理规则>
        /// 1. 非索引节点 : 处理标识, 节点取值处理.0: 不处理(不写入文件中);1：使用默认值(写入文件);2：使用推荐值(写入文件).
        /// 2. 索引节点时 : 处理标识只有0和1,有2就是错误。
        ///    2.1. 当有索引节点标识为1时, 代表具体的实例的具体索引.例如:
        ///         MIB变量名         | 中文名称          | 取值范围  | 默认值 | 华为:推荐值 | 处理标识
        ///         nrCellUlIotIndex* | 小区上行底噪索引	 | 0..6	     |  0     |             |  1
        ///         含义: ”nrCellUlIotIndex” 在 生成"华为patch"时, 这个值只取 "0"。
        ///    2.2.当所有索引节点标识为0时, 并且存在(<条件1>处理标识为1或2)(<条件2>非索引)的节点才有意义。
        ///         这时表示对所有实例有效，修改这个表的所有实例中这个(标识1~2)节点，节点值修改为"标识"的值.
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="strExcelPath"></param>
        /// <param name="strUeType"></param>
        /// <param name="cfgOp"></param>
        /// <returns></returns>
        public bool ProcessingExcel(BinaryWriter bw, string strExcelPath, string strUeType, CfgOp cfgOp)
        {
            if ((String.Empty == strExcelPath) || (String.Empty == strUeType) || (null == cfgOp) || (null == bw))
                return false;
            if (!File.Exists(strExcelPath))
            {
                bw.Write(String.Format("CfgParseReclistExcel5G : ({0}) 文件不存在！", strExcelPath).ToArray());
                return false;
            }

            g_bw = bw;
            g_strExcelPath = strExcelPath;


            // 几种模式
            if (0 == String.Compare("0:升级发布", strUeType, true)) // 不区分大小写，相等
            {
                ProcessingExcelBbu(cfgOp);
            }
            else if (0 == String.Compare("4:恢复默认配置", strUeType, true)) // 不区分大小写，相等
            {
                ProcessingExcelDefault(cfgOp);
            }
            //展讯
            else if (0 == String.Compare(strUeType, "1:展讯", true))
            {
                //strExcelPageArray[0] = "gNB参数表-9";
                //strExcelPageArray[1] = "Cell参数表-10";
            }
            //e500
            else if (0 == String.Compare(strUeType, "2:e500", true))
            {
                //strExcelPageArray[0] = "gNB参数表-11";
                //strExcelPageArray[1] = "Cell参数表-12";
                //strExcelPageArray[]={"gNB参数表-x2", "Cell参数表-y2"};
            }
            //华为
            else if (0 == String.Compare(strUeType, "3:华为", true))
            {
                // 华为 ： Cell参数表 N; gNB参数表 N;
                //strExcelPageArray[0] = "gNB参数表-13";
                //strExcelPageArray[1] = "Cell参数表-14";
                //strExcelPageArray[]={"gNB参数表-x3", "Cell参数表-y3"};
                //return ProcessingExcel_5GHuaWei(bw, strExcelPath, cfgOp);
            }
            else
            {
                //strExcelPageArray[0] = "eNB参数表-11";
                //strExcelPageArray[1] = "Cell参数表-17";
                //strExcelPageArray[] = {"eNB参数表-11", "Cell参数表-17"};
            }
            return true;
        }

        /// <summary>
        /// 模式 : 0:升级发布
        /// </summary>
        /// <param name="wbook"></param>
        /// <param name="excelOp"></param>
        void ProcessingExcelBbu(CfgOp cfgOp)
        {
            // "Cell参数表"
            Dictionary<string, string> CellCol = new Dictionary<string, string>(){
                    { "ProcessIdentity" ,"H" },  // 处理标识
                    { "recommendValue", "E"},    // 推荐值    标识为2使用
                };
            // 合并两个字典
            CellCol = CellCol.Concat(SameCol).ToDictionary(k => k.Key, v => v.Value);
            DealReclist("Cell参数表", CellCol, cfgOp);

            //// "gNB参数表" 
            //Dictionary<string, string> gNBCol = new Dictionary<string, string>(){
            //        { "ProcessIdentity" ,"H" },  // 处理标识
            //        { "recommendValue", "E"},    // 推荐值    标识为2使用
            //    };
            //DealReclist("gNB参数表", gNBCol, cfgOp);
        }

        /// <summary>
        /// 模式 : 默认值
        /// </summary>
        /// <param name="wbook"></param>
        /// <param name="excelOp"></param>
        void ProcessingExcelDefault(CfgOp cfgOp)
        {
            // "Cell参数表"
            Dictionary<string, string> CellCol = new Dictionary<string, string>(){
                    
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "E"},      // 默认值
                    { "End","O"},                // 结束标志

                    { "ProcessIdentity" ,"Q" },  // 处理标识
                    { "recommendValue", "E"},    // 推荐值默认
                };

            // "gNB参数表" 展讯
            Dictionary<string, string> gNBCol = new Dictionary<string, string>(){                    
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "E"},      // 默认值
                    { "End","Q"},                // 结束标志

                    { "ProcessIdentity" ,"M" },  // 处理标识
                    { "recommendValue", "L"},    // 推荐值
                };

            //Excel.Worksheet wks = excelOp.ReadExcelSheet(strExcelPath, "Cell参数表");//
            //if (wks == null)
            //    return;

            //DealReclist("Cell参数表", SheetCellColUe0, cfgOp);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="strExcelPath"></param>
        /// <param name="strPageName"></param>
        /// <param name="ColName"></param>
        /// <param name="cfgOp"></param>
        /// <returns></returns>
        bool DealReclist(string strPageName, Dictionary<string, string> ColName, CfgOp cfgOp)
        {
            int rEndNo = GetEndLineNum(strPageName, ColName);// 获取结束的行数
            if (rEndNo < 0)
                return false;
            Dictionary<string, object[,]> ColVals = GetExcelRangeValue(strPageName, rEndNo, ColName);// 获取ColName相关的col的数据
            if (null == ColVals)
                return false;

            // 1.把reclist 中存在 表名tableName 存起来；2.把reclist中每个table下的node存起来3.处理每个reclist中node的默认值。
            //每个表的用到的
            string m_strCurTableName = ""; // 当前处理的表名 
            int m_iCurTabIndexNum = -1;  // 2014-2-12 luoxin 当前表索引个数
            bool m_bIsMoreInsts = false; // 2014-2-12 luoxin 是否多实例配置
            //string m_strPlanIndex = "";  // 2014-2-12 luoxin 多实例配置的规划索引
            List<string> m_vectIndexScope = new List<string>();//存索引真实应该取得值，如果0-1取默认值，2取推荐值
            for (int iLine = 4; iLine < rEndNo + 1; iLine++)
            {
                bool bIsIndex = false;//是否是索引节点
                string strFlag = "";      //根据patch标识进行处理
                string nodeName = "";     //节点名NodeName, 
                string nodeValue = "";    //根据 Flag 取不同的值, 0,1取默认值，2取推荐值
                string strTableName = ""; //节点所属于的table
                if (isEndLine(ColVals, iLine))                     // 是否结束行
                    break;
                if (!isEffectiveLine(ColVals, iLine, out strFlag)) // 是否有效行
                    continue;
                if (!GetNodeName(ColVals, iLine, strFlag, out nodeName, out bIsIndex))// 是否为索引节点bIsIndex, 如果是索引节点是否为flag为2。
                    return false;
                if (!GetNodeValueByFlag(ColVals, iLine, strFlag, out nodeValue))     //根据 Flag 取不同的值, 0,1取默认值，2取推荐值
                    continue;
                if (!GetTableName(cfgOp, nodeName, out strTableName)) //节点所属于的table
                    continue;

                if (0 != String.Compare(m_strCurTableName, strTableName, true)) // 是否是新表
                {
                    m_strCurTableName = strTableName;  // 更新表名
                    m_iCurTabIndexNum = GetIndexNumFromDBMibTree(cfgOp, nodeName);// 获取索引个数
                    m_bIsMoreInsts = false;            // 初始化为单实例
                    m_vectIndexScope.Clear();          // 清空
                    if (true == bIsIndex && 0 == m_iCurTabIndexNum)
                        Console.WriteLine("Err: 自相矛盾。标量表不能有索引节点");
                }

                InsertPdgTab(m_strCurTableName, strFlag);// 保存表

                if (0 == m_iCurTabIndexNum)//标量表
                {
                    DealScalarTable(cfgOp, m_strCurTableName, nodeName, strFlag);
                }
                else//表量表
                {
                    //索引节点
                    if (bIsIndex)
                    {
                        m_vectIndexScope.Add(nodeValue);    //真正取值，当形式不是 start .. end时，start和end等于一个值
                        if (0 == String.Compare(strFlag, "1", true))//2018-11-21,quyaxin 多实例场景取值含义变化
                        {
                            m_bIsMoreInsts = true;
                        }
                        continue;//索引节点没有办法设定特殊值
                    }
                    //其他节点//单实例配置
                    if (!m_bIsMoreInsts )
                    {
                        if (strFlag == "0")
                            continue;
                        if (!DealSingleConfigTable(cfgOp, nodeName, nodeValue, strFlag, m_strCurTableName))
                            return false;
                    }
                    //多实例配置
                    else
                    {
                        if (!DealMoreConfigTable(cfgOp, nodeName, nodeValue, strFlag, m_strCurTableName,
                            m_iCurTabIndexNum, strPageName, m_vectIndexScope))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        //2014-2-12 luoxin 处理标量表中节点
        bool DealScalarTable(CfgOp cfgOp, string strCurTableName, string strNodeName, string ProcessIdentity)//string strFlag)
        {
            //做补丁文件
            if (0 == String.Compare(ProcessIdentity, "1", true))
            {
                // reclist 全局变量
                // 在 table 中 存 relict 内容
                if (cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName.ContainsKey(".0"))
                    cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName[".0"].Add(strNodeName);
                else
                {
                    cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName.Add(".0", new List<string>() { strNodeName });
                }
            }
            if (0 == String.Compare(ProcessIdentity, "2", true))
            {
                //do something copy by quyaxin
            }


            return true;
        }

        //2014-2-12 luoxin 处理单实例配置表节点
        bool DealSingleConfigTable(CfgOp cfgOp, string strNodeName, string strNodeValue, string strFlag, string strCurTableName)
        {
            if (0 == String.Compare(strFlag, "1", true))
            {
                List<string> InstINumList = cfgOp.m_mapTableInfo[strCurTableName].GetCfgInstsInstantNum();
                foreach (var strIndex in InstINumList)
                    //foreach (var mib in cfgOp.m_mapTableInfo[strCurTableName].m_LeafNodes)//找到每个实例下的所有叶子
                    cfgOp.m_mapTableInfo[strCurTableName].InsertInstOrLeaf(strIndex, strNodeName);//做补丁文件
            }
            //2018-09-08 quyaxin 导入终端类型
            else if (0 == String.Compare(strFlag, "2", true))
            {
                List<string> InstINumList = cfgOp.m_mapTableInfo[strCurTableName].GetCfgInstsInstantNum();
                foreach (var strIndex in InstINumList)
                {
                    cfgOp.m_mapTableInfo[strCurTableName].InsertInstOrLeaf(strIndex, strNodeName);//做补丁文件
                    //更新节点值
                    if (!WriteValueToBuffer(cfgOp.m_mapTableInfo[strCurTableName], strIndex, strNodeName, strNodeValue))
                        continue;
                }
            }
            else
            {
                //do nothing
            }
            return true;
        }
        //2014-2-12 luoxin 处理多实例配置表节点
        bool DealMoreConfigTable(CfgOp cfgOp, string strNodeName, string strNodeValue, string strFlag, string strCurTableName,
            int m_iCurTabIndexNum, string strPageName, List<string> m_vectIndexScope)//, string m_strPlanIndex)
        {
            //string strIndex = "";

            //一维索引
            if (1 == m_iCurTabIndexNum)
            {
                if (!DealMoreConfigTable1(cfgOp, strNodeName, strNodeValue, strFlag, strCurTableName,
                    strPageName, m_vectIndexScope))
                    return false;
            }
            //二维索引
            if (2 == m_iCurTabIndexNum)
            {
                DealMoreConfigTable2(cfgOp, strNodeName, strNodeValue, strFlag, strCurTableName,
                    strPageName, m_vectIndexScope);
            }
            //二维索引
            if (3 == m_iCurTabIndexNum)
            {
                DealMoreConfigTable3(cfgOp, strNodeName, strNodeValue, strFlag, strCurTableName,
                    strPageName, m_vectIndexScope);
            }

            return true;
        }
        /// <summary>
        /// 一维索引 处理
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="strPageName"></param>
        /// <returns></returns>
        bool DealMoreConfigTable1(CfgOp cfgOp, string strNodeName, string strNodeValue, string strFlag,
            string strCurTableName, string strPageName, List<string> m_vectIndexScope)//, string m_strPlanIndex)
        {
            string strIndex = "." + m_vectIndexScope.Last();//一维索引

            //2016-06-27 luoxin 判断实例是否在配置文件中存在
            if (!InstanceIsExist(cfgOp, strIndex, strCurTableName))
                return false;
            //2016-06-27 luoxin end

            //"gNB参数表"页的一维索引多配置全部表需要增加行状态
            CfgTableOp curtable = cfgOp.m_mapTableInfo[strCurTableName];
            if (0 == String.Compare(strPageName, "gNB参数表", true))
            {
                string strTabAndIndex = strCurTableName + strIndex;
                string strRowStatusName = curtable.GetRowStatusName();

                //每条实例只增加一次行状态
                if (0 != String.Compare(strTabAndIndex, m_strCurTabAndIndex, true))
                {
                    if (!WriteValueToBuffer(curtable, strIndex, strRowStatusName, "4"))//设置行有效
                        return false;
                    m_strCurTabAndIndex = strTabAndIndex;//记录当前的索引
                }

                //2014-4-23 luoxin DTMUC00212701 行状态需要增加到补丁文件中
                if (strFlag == "1"|| strFlag == "2")
                {
                    curtable.InsertInstOrLeaf(strIndex, strRowStatusName);
                }
                //2014-4-23 luoxin end
            }

            if (strFlag == "1" || strFlag == "2")
            {
                curtable.InsertInstOrLeaf(strIndex, strNodeName);
            }

            //更新节点值
            if (!WriteValueToBuffer(curtable, strIndex, strNodeName, strNodeValue))
                return false;

            return true;
        }
        /// <summary>
        /// 二维索引 处理
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="strPageName"></param>
        /// <returns></returns>
        bool DealMoreConfigTable2(CfgOp cfgOp, string strNodeName, string strNodeValue, string strFlag,
            string strCurTableName, string strPageName, List<string> m_vectIndexScope)//, string m_strPlanIndex)
        {
            string index1str = m_vectIndexScope[0];
            string index2str = m_vectIndexScope[1];

            Dictionary<string, List<int>> ValStrToInt = new Dictionary<string, List<int>>();
            ParseIndexStrToInt(m_vectIndexScope, ValStrToInt);



            //string strFirstIndexScope = m_vectIndexScope[0];

            //strFirstIndexScope = strFirstIndexScope.Replace("..", "-");
            //int iFirstIndexMin = int.Parse(strFirstIndexScope.Substring(0, strFirstIndexScope.IndexOf('-')));
            //int iFirstIndexMax = int.Parse(strFirstIndexScope.Substring(strFirstIndexScope.IndexOf('-') + 1));

            //for (int iFirstIndex = iFirstIndexMin; iFirstIndex <= iFirstIndexMax; iFirstIndex++)
            for (int index1 = ValStrToInt[m_vectIndexScope[0]][0]; index1 <= ValStrToInt[m_vectIndexScope[0]][1]; index1++)
                for (int index2 = ValStrToInt[m_vectIndexScope[1]][0]; index2 <= ValStrToInt[m_vectIndexScope[1]][1]; index2++)
            {
                //string strFirstIndex = String.Format(".{0}", iFirstIndex);
                string strIndex = "." + index1 + "." + index2;

                //2016-06-27 luoxin 判断实例是否在配置文件中存在
                if (!InstanceIsExist(cfgOp, strIndex, strCurTableName))
                    return false;
                //2016-06-27 luoxin end

                if (strFlag == "1" || strFlag == "2")
                {
                    cfgOp.m_mapTableInfo[strCurTableName].InsertInstOrLeaf(strIndex, strNodeName);
                }

                //更新节点值
                if (!WriteValueToBuffer(cfgOp.m_mapTableInfo[strCurTableName], strIndex, strNodeName, strNodeValue))
                    continue;
            }
            return true;
        }
        /// <summary>
        /// 三维索引
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strCurTableName"></param>
        /// <param name="strPageName"></param>
        /// <param name="m_vectIndexScope"></param>
        /// <returns></returns>
        bool DealMoreConfigTable3(CfgOp cfgOp, string strNodeName, string strNodeValue, string strFlag,
            string strCurTableName, string strPageName, List<string> m_vectIndexScope)//, string m_strPlanIndex)
        {
            Dictionary<string, List<int>> ValStrToInt = new Dictionary<string, List<int>>();
            ParseIndexStrToInt(m_vectIndexScope, ValStrToInt);

            for (int index1 = ValStrToInt[m_vectIndexScope[0]][0]; index1 <= ValStrToInt[m_vectIndexScope[0]][1]; index1++)
            {
                for (int index2 = ValStrToInt[m_vectIndexScope[1]][0]; index2 <= ValStrToInt[m_vectIndexScope[1]][1]; index2++)
                { 
                    for (int index3 = ValStrToInt[m_vectIndexScope[2]][0]; index3 <= ValStrToInt[m_vectIndexScope[2]][1]; index3++)
                    { 
                        string strIndex = "." + index1 + "." + index2 + "." + index3;

                        //2016-06-27 luoxin 判断实例是否在配置文件中存在
                        if (!InstanceIsExist(cfgOp, strIndex, strCurTableName))
                            return false;
                        //2016-06-27 luoxin end

                        if (strFlag == "1" || strFlag == "2")
                        {
                            cfgOp.m_mapTableInfo[strCurTableName].InsertInstOrLeaf(strIndex, strNodeName);
                        }

                        //更新节点值
                        if (!WriteValueToBuffer(cfgOp.m_mapTableInfo[strCurTableName], strIndex, strNodeName, strNodeValue))
                            continue;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 把索引值拆分
        /// </summary>
        /// <param name="m_vectIndexScope"></param>
        /// <param name="indexValStrToInt"></param>
        /// <returns></returns>
        bool ParseIndexStrToInt(List<string> m_vectIndexScope, Dictionary<string, List<int>> indexValStrToInt)
        {
            foreach (string indexVal in m_vectIndexScope)
            {
                string [] spStr = indexVal.Split("..".ToArray());
                int iMin = int.Parse(spStr.First());
                int iMax = int.Parse(spStr.Last());// 如果没有 ".." 时，猜想和iMin的值一样
                indexValStrToInt.Add(indexVal, new List<int> { iMin, iMax });
            }
            return true;
        }

        /// <summary>
        /// 修改某(strIndex)实例中某(strNodeName)节点在内存中的值(value)数据
        /// 与 CfgParseSelfExcel 中的同名函数，功能相同
        /// </summary>
        /// <param name="curtable">表实例</param>
        /// <param name="strIndex">修改的实例的索引值</param>
        /// <param name="strNodeName">修改的节点的英文名</param>
        /// <param name="value">修改节点的值</param>
        /// <returns></returns>
        bool WriteValueToBuffer(CfgTableOp curtable, string strCurIndex, string strNodeName, string strNodeValue)
        {
            CfgTableInstanceInfos pInstInfo = null;  //实例信息
            CfgFileLeafNodeOp leafNodeOp = null;     //节点属性
            int InstsPos = 0;                        //实例位置

            // 获得表的某个(strIndex)实例信息
            if (!curtable.GetCfgInstsByIndex(strCurIndex, out pInstInfo, out InstsPos))//是否存在这个索引值的实例
                return false;

            // 获得表的某个节点(strNodeName)信息
            if (!curtable.GetLeafNodesByNodeName(strNodeName, out leafNodeOp))
                return false;

            // 组合节点的信息
            byte[] InstMem = pInstInfo.GetInstMem();                           //1.这个表实例的内容的内存;实例化后表中节点依次排列的内容
            ushort u16FieldLen = leafNodeOp.m_struFieldInfo.u16FieldLen;       //2.这个节点字段的长度
            ushort u16FieldOffset = leafNodeOp.m_struFieldInfo.u16FieldOffset; //3.这个节点在实例内存中相对的位置 ;字段相对记录头偏移量
            string strOMType = leafNodeOp.m_struMibNode.strOMType;
            string asnType = leafNodeOp.m_struMibNode.strMibSyntax;            //asnType
            string strDefaultValue = strNodeValue;                             // 修改点, 在InstMem中把strNodeName的值修改为strNodeValue

            // 修改内存数据
            List<byte[]> byteArray = new List<byte[]>() { InstMem };
            List<int> bytePosL = new List<int>() { u16FieldOffset };
            new CfgOp().WriteToBuffer(byteArray, strDefaultValue, bytePosL, strOMType, u16FieldLen, "", asnType);

            // 重新写回去,实验证明数据已经修改，而且不会修改其他字段
            //curtable.SetInstsInfoValueByIndex(InstsPos, byteArray[0]);

            return true;
        }
        //2016-06-27 luoxin 把实例加入到补丁文件之前先判断该实例在配置文件中是否存在
        bool InstanceIsExist(CfgOp cfgOp, string strInstIndex, string strCurTableName)
        {
            List<string> vectInstSet = cfgOp.m_mapTableInfo[strCurTableName].GetCfgInstsInstantNum();
            if (vectInstSet.FindIndex(e => e.Equals(strInstIndex)) == -1) //不存在：返回-1，存在：返回位置。
                return false;
            else
                return true;
        }


        /// <summary>
        /// strFlag 为1或2时，更新 strTabName 表
        /// </summary>
        /// <param name="strTabName"></param>
        public void InsertPdgTab(string strTabName, string strFlag)
        {
            if ("1" == strFlag || "2" == strFlag)
            {
                if (m_vectPDGTabName.FindIndex(e => e.Equals(strTabName)) == -1) //不存在：返回-1，存在：返回位置。
                {
                    m_vectPDGTabName.Add(strTabName);
                }
            }
            return;
        }
        /// <summary>
        /// 获取nodeName对应的table表名
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="nodeName"></param>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        bool GetTableName(CfgOp cfgOp, string nodeName, out string strTableName)
        {
            strTableName = cfgOp.m_mibTreeMem.GetTableNameFromDBMibTree(nodeName);
            if (String.Empty == strTableName || "" == strTableName)
            {
                Console.WriteLine(String.Format("Err ({0}) GetTableName({1}) null, continue.", nodeName, strTableName));
                g_bw.Write(String.Format("Err ({0}) GetTableName({1}) null, continue.", nodeName, strTableName).ToArray());
                return false;//continue;
            }
            if (!cfgOp.m_mapTableInfo.ContainsKey(strTableName))
                return false;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strExcelPath"></param>
        /// <param name="strPageName"></param>
        /// <param name="rowValidCount"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        Dictionary<string, object[,]> GetExcelRangeValue(string strPageName, int rowValidCount, Dictionary<string, string> ColName)
        {
            // 获取所有sheet 每col的数据
            Dictionary<string, object[,]> ColVals = new Dictionary<string, object[,]>();
            foreach (var colName in ColName.Keys)//colName=A,..,Z,AA,...,AZ,BA,...,BW.
            {
                object[,] arry = CfgExcelOp.GetInstance().GetRangeVal(g_strExcelPath, strPageName, ColName[colName] + "1", ColName[colName] + rowValidCount);
                if (null == arry)
                {
                    g_bw.Write(String.Format("Err GetExcelRangeValue ({0}) ({1}) get col({2}) arry null.", g_strExcelPath, strPageName, colName));

                    ColVals = null;
                    return null;
                }
                ColVals.Add(colName, arry);
            }
            return ColVals;
        }
        /// <summary>
        /// 是否是索引节点
        /// </summary>
        /// <param name="NodeName"></param>
        /// <returns></returns>
        bool IsIndexNode(string NodeName)
        {
            if (-1 != NodeName.IndexOf('*'))
                return true;//有'*'是索引节点
            return false;
        }
        /// <summary>
        /// 去掉 * 之后的名字
        /// </summary>
        /// <param name="NodeName"></param>
        /// <returns></returns>
        bool GetNodeName(Dictionary<string, object[,]> ColVals, int lineNo, string strFlag, out string NodeName, out bool bIsIndexNode)
        {
            NodeName = GetCellString(ColVals["NodeName"][lineNo, 1]);
            bIsIndexNode = IsIndexNode(NodeName);           
            if (bIsIndexNode == true && strFlag == "2")
            {
                Console.WriteLine(String.Format("Err lineNo({0}) GetNodeName({1}) is index,but flag({2}) is '2' err.", lineNo, NodeName, strFlag));
                g_bw.Write(String.Format("Err lineNo({0}) GetNodeName({1}) is index,but flag({2}) is '2' err.", lineNo, NodeName, strFlag).ToArray());
                return false;
            }
            int pos = NodeName.IndexOf('*');
            if (-1 != pos)
            {
                NodeName = NodeName.Substring(0, pos);
            }
            return true;
        }
        /// <summary>
        /// 根据flag取节点值
        /// </summary>
        /// <param name="cellVal"></param>
        /// <returns></returns>
        bool GetNodeValueByFlag(Dictionary<string, object[,]> ColVals, int lineNo, string strFlag, out string nodeValue )
        {
            if ("1" == strFlag || "0" == strFlag)
                nodeValue = GetCellString(ColVals["DefaultValue"][lineNo, 1]);
            else if ("2" == strFlag)
                nodeValue = GetCellString(ColVals["recommendValue"][lineNo, 1]);
            else
                nodeValue = "";

            if (nodeValue == "")
                return false;
            if (0 < nodeValue.IndexOf(':'))
                nodeValue = nodeValue.Substring(0, nodeValue.IndexOf(':'));
            return true;

        }
        /// <summary>
        /// 获取索引节点的默认值
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <param name="strFlag"></param>
        /// <param name="nodeValue"></param>
        /// <returns></returns>
        bool GetIndexNodeVal(Dictionary<string, object[,]> ColVals, int lineNo, string strFlag, out string nodeValue)
        {
            nodeValue = "";
            if ("2" == strFlag)//5g 新规则， index node ,flag不等为2。
                return false;
            nodeValue = GetCellString(ColVals["DefaultValue"][lineNo, 1]);
            if (nodeValue == "")
                return false;
            if (0 < nodeValue.IndexOf(':'))
                nodeValue = nodeValue.Substring(0, nodeValue.IndexOf(':'));
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="strNodeName"></param>
        /// <returns></returns>
        int GetIndexNumFromDBMibTree(CfgOp cfgOp, string strNodeName)
        {
            if (null == cfgOp || "" == strNodeName || null == cfgOp.m_mibTreeMem)
                return -1;
            return cfgOp.m_mibTreeMem.GetIndexNumFromDBMibTree(strNodeName); ;
        }

        /// <summary>
        /// 单元格信息
        /// </summary>
        /// <param name="cellVal"></param>
        /// <returns></returns>
        string GetCellString(object cellVal)
        {
            return (cellVal == null) ? "": cellVal.ToString();
        }
        /// <summary>
        /// 找"end" 行,必须存在
        /// </summary>
        /// <param name="wks"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        int GetEndLineNum(string strPageName, Dictionary<string, string> ColName)
        {
            //int rowCount = wks.UsedRange.Rows.Count;
            var exop = CfgExcelOp.GetInstance();
            int rowCount = exop.GetRowCount(g_strExcelPath, strPageName);
            if (-1 == rowCount)
            {
                g_bw.Write(String.Format("Err GetEndLineNum:({0}):({1}), get row count err.", g_strExcelPath, strPageName));
                return -1;// false;
            }

            object[,] arry = exop.GetRangeVal(g_strExcelPath, strPageName, ColName["End"] + "1", ColName["End"] + (rowCount + 1));
            for (int row = 1; row < rowCount + 1; row++)
            {
                if (arry[row, 1] == null)
                    continue;
                if (0 == String.Compare("end", arry[row, 1].ToString(), true))
                    return row;
            }

            g_bw.Write(String.Format("Err GetEndLineNum:({0}):({1}), not exist 'end'.", g_strExcelPath, strPageName));
            return -2;// false;
        }
        /// <summary>
        /// 是否是有效行： 0,1,2 三种标识需要处理
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <returns>true 需要进行处理 </returns>
        bool isEffectiveLine(Dictionary<string, object[,]> ColVals, int lineNo, out string strFlag)
        {
            strFlag = GetExcelFlag(ColVals, lineNo);//根据patch标识进行处理
            if (String.Equals(strFlag, "0") || String.Equals(strFlag, "1") || String.Equals(strFlag, "2"))
                return true;
            else
                return false;
        }
        /// <summary>
        /// 标识  ： ProcessIdentity
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <returns></returns>
        string GetExcelFlag(Dictionary<string, object[,]> ColVals, int lineNo)
        {
            object[,] arry = ColVals["ProcessIdentity"];
            //根据patch标识进行处理
            if (arry[lineNo, 1] == null)
                return "";
            return arry[lineNo, 1].ToString();
        }
        /// <summary>
        /// "end" 标识 结束
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <returns>true 结束</returns>
        bool isEndLine(Dictionary<string, object[,]> ColVals, int lineNo)
        {
            object[,] arry = ColVals["End"];

            //根据patch标识进行处理
            if (arry[lineNo, 1] == null)
                return false;

            string strEndVal = arry[lineNo, 1].ToString();
            if (0 == String.Compare("END", strEndVal, true))
                return true;
            else
                return false;
        }
    }

    class ReclistTable
    {
        string tableName;

        /// <summary>
        /// 可能要保存相关的索引
        /// CfgFileLeafNodeIndexInfoOp 中有相关的取值
        /// </summary>
        List<string> index;
        /// <summary>
        /// 主要保存
        /// 1. 节点名
        /// 2. 最后取值
        /// 3. 
        /// </summary>
        List<CfgFileLeafNodeOp> leafList;
    }
    class ReclistIndex
    {

    }
}
