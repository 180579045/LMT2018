using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using MIBDataParser.JSONDataMgr;

namespace CfgFileOperation
{
    /// <summary>
    /// 4G ： 解析 《RecList.xls》 文件
    /// 5G ： 解析 《参数标定手册》 文件
    /// 格式和逻辑相近
    /// </summary>
    class CfgReadReclistExcel
    {
        CfgReadDBMibTreeToMemory mibTreeMem = null;// 操作读取数据库 lm.mdb 把mibTree 安行顺序读入内存

        List<string> m_vectIndexScope = null;// new List<string>();//2014-2-12 luoxin 索引取值范围

        List<string> m_vectPDGTabName = new List<string>();

        //2014-2-12 luoxin 多实例配置的规划索引
        string m_strPlanIndex = "";
        //2014-2-12 luoxin 当前索引
        string m_strCurTabAndIndex = "";

        private Dictionary<string, string> ColsNameCell = null;//Cell参数表

        private Dictionary<string, string> ColsNamegNB = null; //Cell参数表

        Dictionary<string, string> SheetCellColUe0;
        Dictionary<string, string> SheetGNBColUe0;

        public CfgReadReclistExcel()
        {
            // sheet Cell参数表
            string[] UeType = new string[] { "0:默认", "1:展讯", "2:e500", "3:华为" };
        }

        /// <summary>
        /// 总体处理 reclist
        /// </summary>
        /// <param name="strExcelPath"></param>
        /// <param name="strFileToDirectory"></param>
        /// <param name="strUeType"></param>
        public void ProcessingExcel(string strExcelPath, string strFileToDirectory, string strUeType, CfgOp cfgOp)
        {

            if ((String.Empty == strExcelPath) || (String.Empty == strFileToDirectory) || (String.Empty == strUeType) || (null == cfgOp))
                return;

            CfgExcelOp excelOp = new CfgExcelOp();
            if (excelOp == null)
                return;

            //strExcelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\RecList_V6.00.50.05.40.07.01.xls";
            Excel.Workbook wbook = excelOp.OpenExcel(strExcelPath);
            if (wbook == null)
                return;
            
            // 获取 lm.mdb中数据，存入内存中
            ProcessingMdbData(strFileToDirectory);

            // 几种模式
            if (0 == String.Compare("0:默认", strUeType, true)) // 不区分大小写，相等
            {
                //strExcelPageArray[0] = "gNB参数表-7";
                //strExcelPageArray[1] = "Cell参数表-8";
                ProcessingExcelUeTypeDefault(wbook, excelOp, cfgOp);
            }
            //展讯
            else if (0 == String.Compare(strUeType,"1:展讯", true))
            {
                //strExcelPageArray[0] = "gNB参数表-9";
                //strExcelPageArray[1] = "Cell参数表-10";
            }
            //e500
            else if (0 == String.Compare(strUeType,"2:e500", true))
            {
                //strExcelPageArray[0] = "gNB参数表-11";
                //strExcelPageArray[1] = "Cell参数表-12";
                //strExcelPageArray[]={"gNB参数表-x2", "Cell参数表-y2"};
            }
            //华为
            else if (0 == String.Compare(strUeType, "3:华为", true))
            {

                //strExcelPageArray[0] = "gNB参数表-13";
                //strExcelPageArray[1] = "Cell参数表-14";
                //strExcelPageArray[]={"gNB参数表-x3", "Cell参数表-y3"};
            }
            else
            {
                //strExcelPageArray[0] = "eNB参数表-11";
                //strExcelPageArray[1] = "Cell参数表-17";
                //strExcelPageArray[] = {"eNB参数表-11", "Cell参数表-17"};
            }


            //
            //DealReclistPageData(string strCfgName, _WorksheetPtr ptrSingleSheet, int iPatchFlagColumn, CString strPageName)
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wbook"></param>
        /// <param name="excelOp"></param>
        void ProcessingExcelUeTypeDefault(Excel.Workbook wbook, CfgExcelOp excelOp, CfgOp cfgOp)
        {
            // "Cell参数表"
            SheetCellColUe0 = new Dictionary<string, string>(){
                    { "ProcessIdentity" ,"K" },  // 处理标识
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "E"},      // 默认值
                    { "recommendValue", "J"},    // 推荐值
                    { "End","Q"},                // 结束标志
                };

            // "gNB参数表" eNB
            SheetGNBColUe0 = new Dictionary<string, string>(){
                    { "ProcessIdentity" ,"K" },  // 处理标识
                    { "NodeName", "A"},          // 节点名
                    { "DefaultValue", "Q"},      // 默认值
                    { "recommendValue", "P"},    // 推荐值
                    { "End","K"},                // 结束标志
                };

            Excel.Worksheet wks = excelOp.ReadExcelSheet(wbook, "Cell参数表");//
            if (wks == null)
                return;

            DealReclistPageData(wks, SheetCellColUe0, cfgOp, "Cell参数表");
        }

        void DealReclistPageData(Excel.Worksheet wks, Dictionary<string, string> ColName, CfgOp cfgOp, string strPageName)
        {
            int rowCount = GetEndLineNum(wks, ColName);// wks.UsedRange.Rows.Count;                  // 获取行数

            // 获取所有sheet 每col的数据
            Dictionary<string, object[,]> ColVals = new Dictionary<string, object[,]>();
            foreach (var colName in ColName.Keys)//colName=A,..,Z,AA,...,AZ,BA,...,BW.
            {
                object[,] arry = (object[,])wks.Cells.get_Range(ColName[colName] + "1", ColName[colName] + rowCount).Value2;
                ColVals.Add(colName, arry);
            }

            // 4
            string strCurTableName = "";
            int  m_iCurTabIndexNum = -1;//2014-2-12 luoxin 当前表索引个数
            bool m_bIsMoreInsts = false;//2014-2-12 luoxin 是否多实例配置
            for (int currentLine = 4; currentLine < rowCount + 1; currentLine++)
            {
                bool bIsIndexNode = false;//是否是索引节点
                //结束标记
                if (isEndLine(ColVals, currentLine))
                    break;



                //根据patch标识进行处理
                string strFlag = GetExcelFlag(ColVals, currentLine);
                if (!isEffectiveLine(strFlag))
                    continue;

                // excel cell value
                string CellNodeName = GetCellString(ColVals["NodeName"][currentLine, 1]);

                //节点名
                string NodeName = GetIndexNodeName(CellNodeName);
                //节点值
                string DefaultValue = GetDefaultValue(ColVals["DefaultValue"][currentLine, 1]);
                //是否为索引节点
                bIsIndexNode = IsIndexNode(CellNodeName);


                ///下面是挑选的过程，
                ///1.把reclist 中存在 表名tableName 存起来；
                ///2.把reclist中每个table下的node存起来
                ///3.处理每个reclist中node的默认值。

                /// 1.通过node mibName 找 tableName，判断reclist中表名是否是在当前数据库中存在
                string strTableName = GetTableNameFromDBMibTree(NodeName);
                if (String.Empty == strTableName)
                    continue;

                ///如果表名在数据库中存在，看看reclist excel lineNo 的节点是否同一个表，防止多次处理同一张表
                if (0 != String.Compare(strCurTableName, strTableName, true))// 不区分大小，不相等时
                {
                    ///再次查看是否有这个表
                    if (!cfgOp.m_mapTableInfo.ContainsKey(strTableName))
                        continue;

                    strCurTableName = strTableName;//m_strCurTabName = strTableName;
                    m_vectIndexScope = new List<string>();
                    m_iCurTabIndexNum = GetIndexNumFromDBMibTree(strTableName);
                    m_bIsMoreInsts = false;
                }

                //标量表
                if (0 == m_iCurTabIndexNum)
                {
                    if (!DealScalarTable(cfgOp, strCurTableName, NodeName, strFlag))
                        continue;
                }
                //表量表
                else
                {
                    //处理索引
                    if (bIsIndexNode)
                    {
                        m_vectIndexScope.Add(NodeName);

                        if (0 == String.Compare(strFlag,"2",true))
                        {
                            m_bIsMoreInsts = true;
                            m_strPlanIndex = GetRecommendValue(ColVals, currentLine);
                        }
                    }

                    //处理表量表中节点
                    if (!DealTableWithIndex(cfgOp, m_bIsMoreInsts, NodeName, DefaultValue, strFlag, strCurTableName, m_iCurTabIndexNum, strPageName))
                    {
                        continue;
                    }
                }

            }

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
        string GetIndexNodeName(string NodeName)
        {
            int pos = NodeName.IndexOf('*');
            if (-1 != pos)
                return NodeName.Substring(0, pos);
            return NodeName;
        }

        //2014-2-12 luoxin 处理标量表中节点
        bool DealScalarTable(CfgOp cfgOp, string strCurTableName, string strNodeName, string ProcessIdentity)//string strFlag)
        {
            //做补丁文件
            if (0 == String.Compare(ProcessIdentity, "1", true ))
            {
                // reclist 全局变量
                if (m_vectPDGTabName.FindIndex(e => e.Equals(strCurTableName)) != -1) //不存在：返回-1，存在：返回位置。
                    m_vectPDGTabName.Add(strCurTableName);

                // 在 table 中 存 relict 内容
                if (cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName.ContainsKey(".0"))
                    cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName[".0"].Add(strNodeName);
                else
                {
                    cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName.Add(".0", new List<string>() { strNodeName });
                }
            }

            return true;
        }

        /// <summary>
        /// 对实例的处理
        /// </summary>
        /// <param name="cfgOp"></param>
        /// <param name="m_bIsMoreInsts"></param>
        /// <param name="strNodeName"></param>
        /// <param name="strNodeValue"></param>
        /// <param name="strFlag"></param>
        /// <param name="strPageName"></param>
        /// <returns></returns>
        bool DealTableWithIndex(CfgOp cfgOp, bool m_bIsMoreInsts, string strNodeName, string strNodeValue, string strFlag, string strCurTableName, int m_iCurTabIndexNum, string strPageName)
        {
            //单实例配置
            if (!m_bIsMoreInsts)
            {
                if (!DealSingleConfigTable(cfgOp, strNodeName, strNodeValue, strFlag, strCurTableName))
                    return false;
            }
            //多实例配置
            else
            {
                if (!DealMoreConfigTable(cfgOp, strNodeName, strNodeValue, strFlag, strCurTableName, m_iCurTabIndexNum, strPageName))
                {
                    return false;
                }
            }

            return true;
        }


        //2014-2-12 luoxin 处理单实例配置表节点
        bool DealSingleConfigTable(CfgOp cfgOp, string strNodeName, string strNodeValue, string strFlag, string strCurTableName )
        {
            if (0 == String.Compare(strFlag, "1", true))
            {
                //做补丁文件
                // reclist 全局变量
                if (m_vectPDGTabName.FindIndex(e => e.Equals(strCurTableName)) != -1) //不存在：返回-1，存在：返回位置。
                    m_vectPDGTabName.Add(strCurTableName);


                List<string> InstINumList = cfgOp.m_mapTableInfo[strCurTableName].GetCfgInstsInstantNum();
                foreach (var strIndex in InstINumList)
                {
                    //找到每个实例下的所有叶子
                    List<CfgFileLeafNodeOp> vect_MibNode = cfgOp.m_mapTableInfo[strCurTableName].m_LeafNodes;

                    foreach (var mib in vect_MibNode)
                    {
                        //做补丁文件
                        if (cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName.ContainsKey(strIndex))
                            cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName[strIndex].Add(strNodeName);
                        else
                            cfgOp.m_mapTableInfo[strCurTableName].m_InstIndex2LeafName.Add(strIndex, new List<string>() { strNodeName });
                    }
                }
            }

            return true;
        }

        //2014-2-12 luoxin 处理多实例配置表节点
        bool DealMoreConfigTable(CfgOp cfgOp, string strNodeName, string strNodeValue, string strFlag, string strCurTableName, int m_iCurTabIndexNum, string strPageName)
        {
            string strIndex = "";

            //一维索引
            if (1 == m_iCurTabIndexNum)
            {
                strIndex = "." + m_strPlanIndex;

                //2016-06-27 luoxin 判断实例是否在配置文件中存在
                if (!InstanceIsExist(cfgOp, strIndex, strCurTableName))
                    return false;
                //2016-06-27 luoxin end

                //"gNB参数表"页的一维索引多配置全部表需要增加行状态
                string  strTabAndIndex = strCurTableName + strIndex;
                if (0 == String.Compare(strPageName,"gNB参数表", true))
                {
                    CfgTableOp curtable = cfgOp.m_mapTableInfo[strCurTableName];
                    string strRowStatusName = curtable.GetRowStatusName();

                    //每条实例只增加一次行状态
                    if (0 != String.Compare( strTabAndIndex, m_strCurTabAndIndex, true))
                    {
                        //设置行有效


                        ////ReclistWriteValueToBuffer();
                        //记录当前的索引
                        m_strCurTabAndIndex = strTabAndIndex;
                    }

                    ////2014-4-23 luoxin DTMUC00212701 行状态需要增加到补丁文件中
                    //if (0 == strFlag.CompareNoCase("1"))
                    //{
                    //    pCfgOp->InsertPdgTab(m_strCurTabName);
                    //    m_pCurTabOp->InsertInstOrLeaf(strIndex, strRowStatusName);
                    //}
                    //2014-4-23 luoxin end
                }

                //if (0 == strFlag.CompareNoCase("1"))
                //{
                //    //做补丁文件
                //    pCfgOp->InsertPdgTab(m_strCurTabName);
                //    m_pCurTabOp->InsertInstOrLeaf(strIndex, strNodeName);
                //}

                ////更新节点值
                //if (!m_pCurTabOp->WriteValueToBuffer(strNodeName, strNodeValue, strIndex))
                //{
                //    return false;
                //}
            }

            ////二维索引
            //if (2 == m_iCurTabIndexNum)
            //{
            //    //map<CString,CString>::iterator itBegin = m_mapIndexNameAndScope.begin();
            //    //itBegin++;
            //    //CString strFirstIndexScope = itBegin->second;
            //    vector<CString>::iterator itBegin = m_vectIndexScope.begin();
            //    CString strFirstIndexScope = *itBegin;

            //    strFirstIndexScope.Replace("..", "-");
            //    int iFirstIndexMin = atoi(strFirstIndexScope.Left(strFirstIndexScope.Find('-')));
            //    int iFirstIndexMax = atoi(strFirstIndexScope.Mid(strFirstIndexScope.Find('-') + 1));

            //    for (int iFirstIndex = iFirstIndexMin; iFirstIndex <= iFirstIndexMax; iFirstIndex++)
            //    {
            //        CString strFirstIndex = "";
            //        strFirstIndex.Format(".%d", iFirstIndex);
            //        strIndex = strFirstIndex + "." + m_strPlanIndex;

            //        //2016-06-27 luoxin 判断实例是否在配置文件中存在
            //        if (!InstanceIsExist(m_pCurTabOp, strIndex))
            //        {
            //            return FALSE;
            //        }
            //        //2016-06-27 luoxin end

            //        if (0 == strFlag.CompareNoCase("1"))
            //        {
            //            //做补丁文件
            //            pCfgOp->InsertPdgTab(m_strCurTabName);
            //            m_pCurTabOp->InsertInstOrLeaf(strIndex, strNodeName);
            //        }

            //        //更新节点值
            //        if (!m_pCurTabOp->WriteValueToBuffer(strNodeName, strNodeValue, strIndex))
            //        {
            //            continue;
            //        }
            //    }
            //}

            return true;
        }

        bool ReclistWriteValueToBuffer(CfgTableOp curtable, string strRowStatusName, string strIndex)
        {
            CfgFileLeafNodeOp leafNodeOp;
            if (!curtable.ReclistWriteValueToBuffer(strRowStatusName, strIndex, out leafNodeOp))
            {
                return false;
            }

            CfgOp fileOp = new CfgOp();

            return true;
        }
        public void WriteToBuffer(List<byte[]> byteArray, string value, List<int> bytePosL, string OMType, int strLen, string strValList, string strAsnType)
        {

        }
        //2016-06-27 luoxin 把实例加入到补丁文件之前先判断该实例在配置文件中是否存在
        bool InstanceIsExist(CfgOp cfgOp, string strInstIndex, string strCurTableName)
        {
            List<string> vectInstSet = cfgOp.m_mapTableInfo[strCurTableName].GetCfgInstsInstantNum();
            if (vectInstSet.FindIndex(e => e.Equals(strInstIndex)) != -1) //不存在：返回-1，存在：返回位置。
                return false;
            else
                return true;
        }

        /// <summary>
        /// 从内存中查询节点的表名
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <returns></returns>
        string GetTableNameFromDBMibTree(string strNodeName)
        {
            if (null == mibTreeMem || string.Empty == strNodeName)
                return "";
            if (!mibTreeMem.pMapMibNodeByName.ContainsKey(strNodeName))
                return "";

            return mibTreeMem.pMapMibNodeByName[strNodeName].strTableName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <returns></returns>
        int GetIndexNumFromDBMibTree(string strNodeName)
        {
            if (null == mibTreeMem || string.Empty == strNodeName)
                return -1;
            if (!mibTreeMem.pMapMibNodeByName.ContainsKey(strNodeName))
                return -1;

            return mibTreeMem.pMapMibNodeByName[strNodeName].nIndexNum;
        }


        /// <summary>
        /// 判断是否结束行
        /// </summary>
        /// <param name="wks"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        int GetEndLineNum(Excel.Worksheet wks, Dictionary<string, string> ColName)
        {
            int rowCount = wks.UsedRange.Rows.Count;
            object[,] arry = (object[,])wks.Cells.get_Range(ColName["End"] + "1", ColName["End"] + rowCount).Value2;

            for (int row = 1; row < rowCount + 1; row++)
            {
                if (arry[row, 1] == null)
                    continue;
                if (0==String.Compare("end", arry[row, 1].ToString(), true))
                    return row;
            }
            return rowCount;
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

        /// <summary>
        /// 推荐值, 
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <returns></returns>
        string GetRecommendValue(Dictionary<string, object[,]> ColVals, int lineNo)
        {
            object[,] arry = ColVals["recommendValue"];

            //根据patch标识进行处理
            if (arry[lineNo, 1] == null)
                return "";
            if (String.Empty == arry[lineNo, 1].ToString())
                return "";
            return arry[lineNo, 1].ToString();
        }

        /// <summary>
        /// 是否是有效行： 0,1,2 三种标识需要处理
        /// </summary>
        /// <param name="ColVals"></param>
        /// <param name="lineNo"></param>
        /// <returns>true 需要进行处理 </returns>
        bool isEffectiveLine(string ProcessIdentity)
        {
            if (String.Equals(ProcessIdentity, "0") || String.Equals(ProcessIdentity, "1") || String.Equals(ProcessIdentity, "2"))
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
        /// 单元格信息
        /// </summary>
        /// <param name="cellVal"></param>
        /// <returns></returns>
        string GetCellString(object cellVal)
        {
            if (cellVal == null)
                return "";
            else
                return cellVal.ToString();
        }

        /// <summary>
        /// 节点值
        /// </summary>
        /// <param name="cellVal"></param>
        /// <returns></returns>
        string GetDefaultValue(object cellVal)
        {
            string defaultValue = GetCellString(cellVal);
            if (0 < defaultValue.IndexOf(':'))
            {
                defaultValue = defaultValue.Substring(0, defaultValue.IndexOf(':'));
            }
            return defaultValue;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <param name="leafName"></param>
        /// <returns></returns>
        bool IsIndexNode(string strNodeName, out string leafName)
        {
            if (-1 != strNodeName.IndexOf('*'))
            {
                leafName = strNodeName.Remove(strNodeName.IndexOf('*'));//leafName = strNodeName.Substring(0, strNodeName.IndexOf('*'));
                return true;
            }
            else
            {
                leafName = strNodeName;
                return false;
            }
        }

        /// <summary>
        /// 把lm.mdb mibtree 安行存入内存，方便使用
        /// </summary>
        /// <param name="strFileToDirectory"></param>
        void ProcessingMdbData(string strFileToDirectory)
        {
            //string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
            mibTreeMem = new CfgReadDBMibTreeToMemory();
            mibTreeMem.ReadMibTreeToMemory(strFileToDirectory);

        }

        
    }
}
