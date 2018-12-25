using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MIBDataParser.JSONDataMgr;
using CfgFileOpStruct;
using System.Data;
using System.IO;
using LogManager;

namespace CfgFileOperation
{
    
    /// <summary>
    /// .cfg 文件相关操作的测试代码
    /// </summary>
    class Test
    {
        //static void Main()
        //{
        //    string dfd = "D:\\Git_pro\\mib_parser_5g\\src\\net\\EMB6116_5G_GNB_PZ_V0.99.00_feature\\RecList_V6.00.50.05.40.07.01.xls";
        //    var d = CfgExcelOp.GetInstance();
        //    d.GetSheetEndRow(dfd, "Cell参数表", "end");
        //    d.Dispose();
        //}
        static int Main(string[] args)
        {
            //string d = Console.ReadLine();
            int iRet = 0;
            string strVal = "";
            if (!new Test().GetCommandInfo(args, out strVal))
            {
                Console.WriteLine(String.Format("Err: Para not have 'Command'.\n"));
                return 9;
            }
            Console.WriteLine(String.Format("Command : {0}.\n", strVal));
            // beyond compare : 比较 两个文件
            if (string.Equals(strVal, "BeyondCompare"))
            {
                TestCfgForInitAndPatch initPath = new TestCfgForInitAndPatch();
                initPath.BeyondCompareInitCfgMain(args);
            }
            // 命令行生成init
            else if (string.Equals(strVal, "Cmdline"))
            {
                //Console.OutputEncoding = Encoding.ASCII;
                if (!new Test().CmdlineCreateInitPatch(args))
                {
                    Console.WriteLine(String.Format("Err CmdlineCreateInitPatch Cmd={0}.\n", strVal));
                    iRet = 2;
                }
            }
            else if (string.Equals(strVal, "BeyondComparePatch"))
            {
                new TestCfgForInitAndPatch().BeyondComparePatchExCfgMain(args);
            }
            // 命令行生成 patch
            else if (string.Equals(strVal, "CreatePatch"))
            {
                if (!new Test().CmdlineCreatePatch(args))
                {
                    Console.WriteLine(String.Format("Err CmdlineCreatePatch Cmd={0}.\n", strVal));
                    iRet = 3;
                }
            }
            else
            {
            }
            Console.WriteLine(String.Format("Main End Cmd={0}, return {1}.\n", strVal, iRet));
            
            return iRet;
        }
        void MainOld()
        {
            //new Test().CmdlineCreateInitPatch(args);

            //TestCfgForInitAndPatch initPath = new TestCfgForInitAndPatch();
            ////initPath.Main();
            //initPath.BeyondCompareInitCfgMain();
            //Log.Error("解析板卡到rru的连接，信息缺失");
            //Test test = new Test();

            //test.TestBeyondCompareMain();

            //new Test().testForCreatePatchAndInit();

            //test.TestReadOM_STRU_IcfIdxTableItem();
            //test.testForParseAlarmEx();

            //test.testForReadSelfExcel();

            //test.testForReadRecList();//

            //test.testLoadMibTreeIntoMem();//

            //test.testForReadExcelRruType();

            //test.testForReadExcelAnnt();

            //test.testForOpReadExcelForCfg();

            //test.test4();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="strComInfo"></param>
        /// <returns></returns>
        bool GetCommandInfo(string[] args, out string strComInfo)
        {
            string strKey = "";
            string strVal = "";
            strComInfo = "";
            foreach (var arg in args)
            {
                Console.WriteLine(String.Format("Main arg:({0})", arg));
                int pos = arg.ToString().IndexOf(':');// arg.ToString().IndexOfAny("Command".ToCharArray());
                if (-1 == pos)
                    continue;
                strKey = arg.ToString().Substring(0, pos);
                strVal = arg.ToString().Substring(pos + 1);
                if (String.Equals(strKey, "Command"))// 命令行
                {
                    strComInfo = strVal;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取 数据块 的头
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        StruDataHead GetDataHeadFromFile(string filePath)
        {
            int offset = 956;
            int readCount = 24;
            byte[] Ysdata = new CfgOp().CfgReadFile(filePath, offset, readCount);
            StruDataHead ysD = new StruDataHead("");
            ysD.SetValueByBytes(Ysdata);
            return ysD;
        }

        /// <summary>
        /// 获取每个表的偏移量;OM_STRU_IcfIdxTableItem;4字节*表数
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        List<uint> GetTablesPos(string filePath, int iTableCounts)
        {
            List<uint> TablesPos = new List<uint>();
            byte[] Ysdata;
            CfgOp cfgOp = new CfgOp();
            // 偏移块
            for (int i = 0; i < iTableCounts; i++)
            {
                Ysdata = cfgOp.CfgReadFile(filePath, (956 + 24 + 4 * i), 4);
                TablesPos.Add(GetBytesValToUint(Ysdata));
            }
            Ysdata = null;
            cfgOp = null;
            return TablesPos;
        }

        List<string> GetTableNamesByTablesPos(string filePath, List<uint> tablesPosL)
        {
            CfgOp cfgOp = new CfgOp();
            byte[] bytedata;
            List<string> tableNamesL = new List<string>();
            foreach (uint pos in tablesPosL)
            {
                bytedata = cfgOp.CfgReadFile(filePath, pos, Marshal.SizeOf(new StruCfgFileTblInfo("")));
                StruCfgFileTblInfo tableInfo = new StruCfgFileTblInfo("");
                tableInfo.SetBytesValue(bytedata);
                string tablName = Encoding.GetEncoding("GB2312").GetString(tableInfo.u8TblName).TrimEnd('\0');
                tableNamesL.Add(tablName);
            }
            bytedata = null;
            cfgOp = null;
            return tableNamesL;
        }

        /// <summary>
        /// 获取每个表的偏移位置
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Dictionary<string,uint> GetTableNamesDictByTablesPos(string filePath)
        {
            StruDataHead YsDhead = GetDataHeadFromFile(filePath);
            List<uint> tablesPosL = GetTablesPos(filePath, (int)YsDhead.u32TableCnt);

            CfgOp cfgOp = new CfgOp();
            byte[] bytedata;
            Dictionary<string, uint> tableNamesDict = new Dictionary<string, uint>();
            foreach (uint pos in tablesPosL)
            {
                bytedata = cfgOp.CfgReadFile(filePath, pos, Marshal.SizeOf(new StruCfgFileTblInfo("")));
                StruCfgFileTblInfo tableInfo = new StruCfgFileTblInfo("");
                tableInfo.SetBytesValue(bytedata);
                string tablName = Encoding.GetEncoding("GB2312").GetString(tableInfo.u8TblName).TrimEnd('\0');
                tableNamesDict.Add( tablName, pos);
            }
            bytedata = null;
            cfgOp = null;
            return tableNamesDict;
        }

        bool TestBeyondTableName(List<string> tablesPosLA, List<string> tablesPosLB)
        {
            bool re = true;
            List<string> tablNamesMore;
            List<string> tablNamesLess;
            if (tablesPosLA.Count > tablesPosLB.Count)
            {
                tablNamesMore = tablesPosLA;
                tablNamesLess = tablesPosLB;
            }
            else
            {
                tablNamesMore = tablesPosLB;
                tablNamesLess = tablesPosLA;
            }

            foreach (var table in tablNamesMore)
            {
                if (-1 == tablNamesLess.FindIndex(e => String.Equals(e, table)))
                {
                    Console.WriteLine(String.Format("not have {0}", table));
                    re = false;
                }
            }
            return re;
        }

        bool TestBeyondIcfIdxTableItemTableName()
        {
            return false;
        }

        void TestReadOM_STRU_IcfIdxTableItem()
        {
            string dataBasePath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\";
            string YSFilePath = dataBasePath + "5GCfg\\init_qyx.cfg";
            string NewFilePath = dataBasePath + "init.cfg";

            CfgOp cfgOp = new CfgOp();

            // 获取 tableNum
            int offset = 956;
            int readCount = 24;
            byte[] Ysdata = cfgOp.CfgReadFile(YSFilePath, offset, readCount);
            byte[] Newdata = cfgOp.CfgReadFile(NewFilePath, offset, readCount);

            StruDataHead ysD = new StruDataHead("");
            ysD.SetValueByBytes(Ysdata);
            int tableNumYs = (int)ysD.u32TableCnt;

            StruDataHead newD = new StruDataHead("");
            newD.SetValueByBytes(Newdata);
            int tableNumNew = (int)newD.u32TableCnt;

            // yuanshi
            List<string> tableName = new List<string>();
            offset = 956 + 24;
            //readCount = 4 * tableNumYs;
            for (int i = 1; i <= tableNumYs; i++)
            {
                Ysdata = cfgOp.CfgReadFile(YSFilePath, offset, 4);
                uint nextTablePos = GetBytesValToUint(Ysdata);
                //readCount = Marshal.SizeOf(new StruCfgFileTblInfo(""));
                // StruCfgFileTblInfo
                Ysdata = cfgOp.CfgReadFile(YSFilePath, nextTablePos, Marshal.SizeOf(new StruCfgFileTblInfo("")));
                StruCfgFileTblInfo ddd = new StruCfgFileTblInfo("");
                ddd.SetBytesValue(Ysdata);
                string tablName = System.Text.Encoding.Default.GetString(ddd.u8TblName);
                tableName.Add(tablName);
                offset += 4;
            }

            List<string> tableNameNew = new List<string>();
            offset = 956 + 24;
            //readCount = 4 * tableNumYs;
            for (int i = 1; i <= tableNumNew; i++)
            {
                //offset += 4;
                Ysdata = cfgOp.CfgReadFile(NewFilePath, offset, 4);
                uint nextTablePos = GetBytesValToUint(Ysdata);
                //readCount = Marshal.SizeOf(new StruCfgFileTblInfo(""));
                // StruCfgFileTblInfo
                Ysdata = cfgOp.CfgReadFile(NewFilePath, nextTablePos, Marshal.SizeOf(new StruCfgFileTblInfo("")));
                StruCfgFileTblInfo ddd = new StruCfgFileTblInfo("");
                ddd.SetBytesValue(Ysdata);
                string tablName = System.Text.Encoding.Default.GetString(ddd.u8TblName);
                tableNameNew.Add(tablName);
                offset += 4;
            }

            foreach (var table in tableName)
            {
                string name = tableNameNew.Find(e => String.Equals(e, table));
                if (-1 == tableNameNew.FindIndex(e => String.Equals(e, table)))
                {
                    Console.WriteLine(String.Format("not have {0}", table));
                }
                if (String.Empty == name)
                {
                    Console.WriteLine("not have {0}", name);
                }
            }
        }

        void TestBeyondCompareMain()
        {
            FileStream fs = new FileStream("BeyondCompareWriteBuf.txt", FileMode.Create);
            //实例化BinaryWriter
            BinaryWriter bw = new BinaryWriter(fs);

            string dataBasePath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\";
            string YSFilePath = dataBasePath + "5GCfg\\init_qyx.cfg";
            string NewFilePath = dataBasePath + "init.cfg";

            // 比较 表名是否一致
            if (!TestBeyondCompFileTableNameMain(bw, YSFilePath, NewFilePath))
            {
                Console.WriteLine("tables name not all same.");
                bw.Write("tables name not all same.\n");
            }

            // 比较 每个表的内容
            if (!TestBeyondComFileTableInfoMain(bw, YSFilePath, NewFilePath))
            {
                Console.WriteLine("tables info not all same.");
                bw.Write("tables info not all same.\n");
            }

            //清空缓冲区
            bw.Flush();
            //关闭流
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="YSFilePath"></param>
        /// <param name="NewFilePath"></param>
        /// <returns></returns>
        bool TestBeyondComFileTableInfoMain(BinaryWriter bw, string YSFilePath, string NewFilePath)
        {
            bool re = true;
            // 获取每个表的偏移位置
            Dictionary<string, uint> YsTableNamePosDict = GetTableNamesDictByTablesPos(YSFilePath);
            Dictionary<string, uint> NewTableNamePosDict = GetTableNamesDictByTablesPos(NewFilePath);
            // 根据文件头，获取每个表的内容，进行比较
            foreach (string table in YsTableNamePosDict.Keys)
            {
                re = true;
                //if (!String.Equals(table, "rruTypePortEntry"))
                //{
                //    //bw.Write("antennaBfScanWeightEntry continue.\n");
                //    //Console.WriteLine("antennaBfScanWeightEntry continue");
                //    continue;
                //}

                // 0. 获取tableInfo表块介绍
                uint offsetYs = YsTableNamePosDict[table];  //表的偏移位置
                uint offsetNew = NewTableNamePosDict[table];//表的偏移位置
                StruCfgFileTblInfo ysTblInfo = TestGetTableHeadInfo(YSFilePath, offsetYs); //获取tableInfo表块介绍                
                StruCfgFileTblInfo newTblInfo = TestGetTableHeadInfo(NewFilePath, offsetNew);

                // 1. 每个表的表头是否相同 : StruCfgFileTblInfo
                if (!TestIsSameTableHeadField(table, ysTblInfo, newTblInfo))
                {
                    bw.Write(String.Format("tableName={0}, table head info not all same.\n", table));
                    Console.WriteLine(String.Format("tableName={0}, table head info not all same.",table));
                    re = false;
                    continue;
                }

                // 2. 每个表的叶子的头是否相同 : StruCfgFileFieldInfo[u16FieldNum];
                Dictionary<string, StruCfgFileFieldInfo>  YsLeafHeadL = TestGetLeafHeadFieldInfo(YSFilePath, ysTblInfo.u16FieldNum, offsetYs);
                Dictionary<string, StruCfgFileFieldInfo>  NewLeafHeadL = TestGetLeafHeadFieldInfo(NewFilePath, newTblInfo.u16FieldNum, offsetNew);
                if (!TestIsSameLeafHeadInfoList(YsLeafHeadL, NewLeafHeadL))
                {
                    bw.Write(String.Format("tableName={0}, Leaf head info not all same.\n", table));
                    Console.WriteLine(String.Format("tableName={0}, Leaf head info not all same.", table));
                    re = false;
                    continue;
                }

                // 3. 每个表的实例是否一致
                List<byte[]> YsInsts = GetInstsData(YSFilePath, offsetYs, (int)ysTblInfo.u32RecNum, (int)ysTblInfo.u16FieldNum, ysTblInfo.u16RecLen);
                List<byte[]> NewInsts = GetInstsData(NewFilePath, offsetNew, (int)newTblInfo.u32RecNum, (int)newTblInfo.u16FieldNum, newTblInfo.u16RecLen);
                if (!TestIsSameByIndexMain(bw,table, (int)ysTblInfo.u32RecNum, YsLeafHeadL, YsInsts, NewInsts))
                {
                    bw.Write(String.Format("\n\n"));
                    //Console.WriteLine(String.Format("tableName={0}, insts info not all same.", table));
                    re = false;
                }
                //Console.WriteLine("netLocalCellRruPowerEntry end, time is" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒") + "\n");
            }
            return re;
        }
        /// <summary>
        /// 1. 是否存在相同索引，且个数相同；2.同一个索引的的实例内容是否相同
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="tableName"></param>
        /// <param name="u32RecNum"></param>
        /// <param name="LeafHead"></param>
        /// <param name="InstsAList"></param>
        /// <param name="InstsBList"></param>
        /// <returns></returns>
        bool TestIsSameByIndexMain(BinaryWriter bw, string tableName, int u32RecNum, Dictionary<string, StruCfgFileFieldInfo> LeafHead, List<byte[]> InstsAList, List<byte[]> InstsBList)
        {
            //通过 索引的顺序，来判断每个实例是否相同
            bool re = true;
            // 1. 获取 索引节点的内容()；因为代码在前面判断了表的叶子头是否相同。
            List<StruCfgFileFieldInfo> indexLeafsInfo = TestGetIndexLeafInfo(tableName, LeafHead);

            // 2. 比较索引项
            if (!TestIsSameIndexList2(indexLeafsInfo, u32RecNum, InstsAList, InstsBList))
            {
                re = false;
                bw.Write(String.Format("TalName=({0}) insts index head not all some.\n", tableName));
                Console.WriteLine(String.Format("TalName=({0}) insts index head not all some.", tableName));//不相同，有问题
                return re;
            }
            //Console.WriteLine("TestIsSameIndexList2 end, time is" + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒") + "\n");

            //Console.WriteLine(String.Format("TalName=({0}) insts index headall some.", tableName));
            // 3. 比较同一索引的实例内容
            Dictionary<string, byte[]> indexAInsts;
            Dictionary<string, byte[]> indexBInsts;
            if (indexLeafsInfo.Count == 0)
            {
                indexAInsts = new Dictionary<string, byte[]>() { { "0", InstsAList[0] } };
                indexBInsts = new Dictionary<string, byte[]>() { { "0", InstsBList[0] } };
            }
            else
            {
                indexAInsts = TestGetIndexInsts2(indexLeafsInfo, u32RecNum, LeafHead, InstsAList);
                indexBInsts = TestGetIndexInsts2(indexLeafsInfo, u32RecNum, LeafHead, InstsBList);
                if (indexAInsts.Count != indexBInsts.Count)
                {
                    bw.Write(String.Format("tableName={0}, insts not all same.\n", tableName));
                    Console.WriteLine(String.Format("tableName={0}, insts not all same.", tableName));
                    return false;
                }
                else if (indexAInsts.Count == 0)//a,b都为0
                {
                    if (!TestIsSameInstsList(bw, tableName, u32RecNum, LeafHead, InstsAList, InstsBList))
                    {
                        bw.Write(String.Format("tableName={0}, insts not all same.\n\n\n", tableName));
                        Console.WriteLine(String.Format("tableName={0}, insts not all same.", tableName));
                        re = false;
                    }
                }
            }
            // 依索引比较内容
            if (!TestInstsInfoList(bw, tableName, LeafHead, indexAInsts, indexBInsts))
            {
                //Console.WriteLine(String.Format("tableName={0}, instsA not same beyondComp instsB.", tableName));
                re = false;
            }
            return re;
        }
        bool TestIsSameIndexList(List<string> indexLeafsName, int u32RecNum, Dictionary<string, StruCfgFileFieldInfo> LeafHead, List<byte[]> InstsListA, List<byte[]> InstsListB)
        {
            List<string> indexListA = TestGetAllIndex(indexLeafsName, u32RecNum, LeafHead, InstsListA);
            List<string> indexListB = TestGetAllIndex(indexLeafsName, u32RecNum, LeafHead, InstsListB);
            return TestIsAllIndexSame(indexListA, indexListB);
        }
        /// <summary>
        /// 比较索引值是否相同
        /// </summary>
        /// <param name="indexLeafsInfo"></param>
        /// <param name="u32RecNum"></param>
        /// <param name="InstsListA"></param>
        /// <param name="InstsListB"></param>
        /// <returns></returns>
        bool TestIsSameIndexList2(List<StruCfgFileFieldInfo> indexLeafsInfo, int u32RecNum,  List<byte[]> InstsListA, List<byte[]> InstsListB)
        {
            // Distinct().ToList(),去重，解决bug:例如，indexListA 索引值都是相同的，且这个相同值存在indexListB中，检测不到。
            List<string> indexListA = TestGetAllIndex2(indexLeafsInfo, u32RecNum, InstsListA).Distinct().ToList();
            List<string> indexListB = TestGetAllIndex2(indexLeafsInfo, u32RecNum,  InstsListB).Distinct().ToList();
            if (indexListA.Count != indexListB.Count)
            {
                return false;
            }
            
            IEnumerable<string> intersect = indexListB.Except(indexListA);
            if (intersect.LongCount() != 0)
            {
                return false;
            }
            return true;// TestIsAllIndexSame(indexListA, indexListB);
        }
        bool TestIsAllIndexSame(List<string> indexListA, List<string> indexListB)
        {
            bool re = true;
            if (indexListA.Count != indexListB.Count)
            {
                Console.WriteLine(String.Format(" index num not same a.num({0}), b.num({1})", indexListA.Count, indexListB.Count));
                re = false;
                return re;
            }
            // a 比较
            foreach (var strIndex in indexListA)
            {
                if (-1 == indexListB.FindIndex(e => string.Equals(e, strIndex)))
                {
                    re = false;
                    Console.WriteLine(String.Format("indexA({0}) not in indexB ", strIndex));
                    break;
                }
            }
            //// b 比较
            //foreach (var strIndex in indexListB)
            //{
            //    if (-1 == indexListA.FindIndex(e => string.Equals(e, strIndex)))
            //    {
            //        re = false;
            //        Console.WriteLine(String.Format("indexB ({0}) not in indexA ", strIndex));
            //        break;
            //    }
            //}

            return re;
        }
        List<string> TestGetAllIndex(List<string> indexLeafsName, int u32RecNum, Dictionary<string, StruCfgFileFieldInfo> LeafHead, List<byte[]> InstsList)
        {
            //Dictionary<string, byte[]> indexInsts = new Dictionary<string, byte[]>();
            List<string> index = new List<string>();
            StruCfgFileFieldInfo FieldH;
            ushort u16FieldOffset;       /* 字段相对记录头偏移量*/
            ushort u16FieldLen;          /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
            byte u8FieldType;            /* 字段类型 */
            byte[] emtpyArray = new byte[InstsList[0].LongCount()];
            emtpyArray.Initialize();
            if (indexLeafsName.Count == 0)
            {
                if (InstsList.Count != 1)
                {
                    Console.WriteLine("rrrerr");
                }
                else
                    index.Add("255");
            }
            else
            {
                for (int instNo = u32RecNum-1; instNo >= 0 ; instNo--)
                //foreach (var inst in InstsList)                
                {
                    byte[]inst = InstsList[instNo];
                    if (TestIsByteListSame(emtpyArray, inst))
                    {
                        continue;// 结束
                    }
                    string strIndex = "";
                    int indexNum = 0;
                    foreach (string leaf in LeafHead.Keys)
                    {
                        FieldH = LeafHead[leaf];
                        u16FieldOffset = FieldH.u16FieldOffset;       /* 字段相对记录头偏移量*/
                        u16FieldLen = FieldH.u16FieldLen;             /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
                        u8FieldType = FieldH.u8FieldType;             /* 字段类型 */
                        if (-1 != indexLeafsName.FindIndex(e => String.Equals(e, leaf)))// 是否是索引
                        {
                            // 索引
                            byte[] InA = inst.Skip(u16FieldOffset).Take(u16FieldLen).ToArray();
                            uint uIndexNum = GetBytesValToUint(InA);
                            strIndex += uIndexNum.ToString() + ".";
                            indexNum += 1;
                            if (indexNum == indexLeafsName.Count)
                                break;
                        }
                    }
                    strIndex = strIndex.TrimEnd('.');
                    if ("" == strIndex)
                    {
                        Console.WriteLine("Error.");
                    }
                    else
                    {
                        if (-1 != index.FindIndex(e => String.Equals(e, strIndex)))
                        {
                            if (TestIsByteListSame(emtpyArray, inst))
                            {
                                continue;// 结束
                            }
                            else
                            {
                                //Console.WriteLine(String.Format("have same index({0}), but info not same", strIndex));
                            }
                        }
                        else
                        {
                            index.Add(strIndex);
                        }
                    }
                }
            }
            return index;
        }
        List<string> TestGetAllIndex2(List<StruCfgFileFieldInfo> indexLeafsInfo, int u32RecNum,List<byte[]> InstsList)
        {
            //Dictionary<string, byte[]> indexInsts = new Dictionary<string, byte[]>();
            List<string> index = new List<string>();
            StruCfgFileFieldInfo FieldH;
            ushort u16FieldOffset;       /* 字段相对记录头偏移量*/
            ushort u16FieldLen;          /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
            byte u8FieldType;            /* 字段类型 */
            byte[] emtpyArray = new byte[InstsList[0].LongCount()];
            emtpyArray.Initialize();
            if (indexLeafsInfo.Count == 0)
            {
                if (InstsList.Count != 1)
                {
                    Console.WriteLine("rrrerr");
                }
                else
                    index.Add("255");
            }
            else
            {
                for (int instNo = u32RecNum - 1; instNo >= 0; instNo--)
                //foreach (var inst in InstsList)                
                {
                    byte[] inst = InstsList[instNo];
                    if (TestIsByteListSame(emtpyArray, inst))
                    {
                        continue;// 结束
                    }
                    string strIndex = "";
                    int indexNum = 0;
                    foreach (var LeafHead in indexLeafsInfo)
                    {
                        FieldH = LeafHead;
                        u16FieldOffset = FieldH.u16FieldOffset;       /* 字段相对记录头偏移量*/
                        u16FieldLen = FieldH.u16FieldLen;             /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */

                        // 索引
                        byte[] InA = inst.Skip(u16FieldOffset).Take(u16FieldLen).ToArray();
                        uint uIndexNum = GetBytesValToUint(InA);
                        strIndex += uIndexNum.ToString() + ".";

                    }
                    strIndex = strIndex.TrimEnd('.');
                    if ("" == strIndex)
                    {
                        Console.WriteLine("Error.");
                    }
                    else
                    {
                        //if (-1 != index.FindIndex(e => String.Equals(e, strIndex)))
                        //{
                        //    if (TestIsByteListSame(emtpyArray, inst))
                        //    {
                        //        continue;// 结束
                        //    }
                        //    else
                        //    {
                        //        //Console.WriteLine(String.Format("have same index({0}), but info not same", strIndex));
                        //    }
                        //}
                        //else
                        {
                            index.Add(strIndex);
                        }
                    }
                }
            }
            return index;
        }

        /// <summary>
        /// 比较索引
        /// </summary>
        /// <param name="indexAInsts"></param>
        /// <param name="indexBInsts"></param>
        /// <returns></returns>
        bool TestInstsIndexList(Dictionary<string, byte[]> indexAInsts, Dictionary<string, byte[]> indexBInsts)
        {
            bool re = true;
            List<string> indexA = indexAInsts.Keys.ToList();
            List<string> indexB = indexBInsts.Keys.ToList();
            if (indexA.Count != indexB.Count)
            {
                Console.WriteLine(String.Format(" index num not same a.num({0}), b.num({1})", indexA.Count ,indexB.Count));
                re = false;
                return re;
            }
            // a 比较
            foreach (var strIndex in indexA)
            {
                if (-1 == indexB.FindIndex(e => string.Equals(e, strIndex)))
                {
                    re = false;
                    Console.WriteLine(String.Format("indexA({0}) not in indexB ", strIndex));
                    break;
                }
            }
            // b 比较
            foreach (var strIndex in indexB)
            {
                if (-1 == indexA.FindIndex(e => string.Equals(e, strIndex)))
                {
                    re = false;
                    Console.WriteLine(String.Format("indexB ({0}) not in indexA ", strIndex));
                    break;
                }
            }
            
            return re;
        }
        /// <summary>
        /// 依索引，比较同一个索引的内容
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="LeafHead"></param>
        /// <param name="indexAInsts"></param>
        /// <param name="indexBInsts"></param>
        /// <returns></returns>
        bool TestInstsInfoList(BinaryWriter bw, string tableName, Dictionary<string, StruCfgFileFieldInfo> LeafHead, Dictionary<string, byte[]> indexAInsts, Dictionary<string, byte[]> indexBInsts)
        {
            bool re = true;

            foreach (var strIndex in indexAInsts.Keys)
            {
                byte[] instA = indexAInsts[strIndex];
                byte[] instB = indexBInsts[strIndex];
                if (!TestIsSameInst(strIndex, bw, tableName, LeafHead, instA, instB))
                {
                    re = false;
                    bw.Write(String.Format("({0}),index({1}) inst info no same。\n", tableName, strIndex));
                    Console.WriteLine(String.Format("({0}),index({1}) inst info no same。", tableName, strIndex));
                    if (tableName.Contains("nr"))
                    {
                        bw.Write(String.Format("{0} not same, break.", tableName));
                        break;
                    }
                    TestIsSameInstDebugWrite(strIndex, bw, tableName, LeafHead, instA, instB);
                    //break;
                }
                //else
                //{
                //    bw.Write(String.Format("({0}),index({1}) inst info all same。\n", tableName, strIndex));
                //    TestIsSameInstDebugWrite(strIndex, bw, tableName, LeafHead, instA, instB);
                //}
            }
            return re;
        }
        /// <summary>
        /// 重新组合index，排列实例
        /// </summary>
        /// <param name="indexLeafsName"></param>
        /// <param name="LeafHead"></param>
        /// <param name="InstsList"></param>
        /// <returns></returns>
        Dictionary<string, byte[]> TestGetIndexInsts(List<string> indexLeafsName, int u32RecNum,  Dictionary<string, StruCfgFileFieldInfo> LeafHead, List<byte[]> InstsList)
        {
            Dictionary<string, byte[]> indexInsts = new Dictionary<string, byte[]>();
            StruCfgFileFieldInfo FieldH;
            ushort u16FieldOffset;       /* 字段相对记录头偏移量*/
            ushort u16FieldLen;          /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
            byte u8FieldType;            /* 字段类型 */
            byte[] emtpyArray = new byte[InstsList[0].LongCount()];
            emtpyArray.Initialize();
            if (indexLeafsName.Count == 0)
            {
                if (InstsList.Count != 1)
                {
                    Console.WriteLine("rrrerr");
                }
                else
                {
                    indexInsts.Add("255", InstsList[0]);
                }
                
            }
            else
            {
                foreach (var inst in InstsList)
                {
                    string strIndex = "";
                    int indexNum = 0;
                    foreach (string leaf in LeafHead.Keys)
                    {
                        FieldH = LeafHead[leaf];
                        u16FieldOffset = FieldH.u16FieldOffset;       /* 字段相对记录头偏移量*/
                        u16FieldLen = FieldH.u16FieldLen;             /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
                        u8FieldType = FieldH.u8FieldType;             /* 字段类型 */
                        if (-1 != indexLeafsName.FindIndex(e => String.Equals(e, leaf)))// 是否是索引
                        {
                            // 索引
                            byte[] InA = inst.Skip(u16FieldOffset).Take(u16FieldLen).ToArray();
                            uint uIndexNum = GetBytesValToUint(InA);
                            strIndex += uIndexNum.ToString() + ".";
                            indexNum += 1;
                            if (indexNum == indexLeafsName.Count)
                                break;
                        }
                    }
                    strIndex = strIndex.TrimEnd('.');
                    if ("" == strIndex)
                    {
                        Console.WriteLine("Error.");
                    }
                    else
                    {
                        if (-1 != indexInsts.Keys.ToList().FindIndex(e => String.Equals(e, strIndex)))
                        {
                            if (TestIsByteListSame(emtpyArray, inst))
                            {
                                continue;// 结束
                            }
                            else
                            {
                                Console.WriteLine(String.Format("have same index({0}), but info not same", strIndex));
                            }
                        }
                        else
                        {
                            indexInsts.Add(strIndex, inst);
                        }
                    }
                }
            }
            return indexInsts;
        }

        Dictionary<string, byte[]> TestGetIndexInsts2(List<StruCfgFileFieldInfo> indexLeafsInfo, int u32RecNum, Dictionary<string, StruCfgFileFieldInfo> LeafHead, List<byte[]> InstsList)
        {
            Dictionary<string, byte[]> indexInsts = new Dictionary<string, byte[]>();
            StruCfgFileFieldInfo FieldH;
            ushort u16FieldOffset;       /* 字段相对记录头偏移量*/
            ushort u16FieldLen;          /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
            byte u8FieldType;            /* 字段类型 */
            byte[] emtpyArray = new byte[InstsList[0].LongCount()];
            emtpyArray.Initialize();
            if (indexLeafsInfo.Count == 0)
            {
                if (InstsList.Count != 1)
                {
                    Console.WriteLine("rrrerr");
                }
                else
                {
                    indexInsts.Add("255", InstsList[0]);
                }

            }
            else
            {
                foreach (var inst in InstsList)
                {
                    string strIndex = "";
                    int indexNum = 0;


                    foreach (StruCfgFileFieldInfo LeafHeads in indexLeafsInfo)
                    {
                        //FieldH = LeafHead;//[leaf];
                        u16FieldOffset = LeafHeads.u16FieldOffset;       /* 字段相对记录头偏移量*/
                        u16FieldLen = LeafHeads.u16FieldLen;             /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
                        
                        // 索引
                        byte[] InA = inst.Skip(u16FieldOffset).Take(u16FieldLen).ToArray();
                        uint uIndexNum = GetBytesValToUint(InA);
                        strIndex += uIndexNum.ToString() + ".";

                    }
                    strIndex = strIndex.TrimEnd('.');
                    if ("" == strIndex)
                    {
                        Console.WriteLine("Error.");
                    }
                    else
                    {
                        //if (-1 != indexInsts.Keys.ToList().FindIndex(e => String.Equals(e, strIndex)))
                        //{
                        if (TestIsByteListSame(emtpyArray, inst))
                        {
                            continue;// 结束
                        }
                        //    else
                        //    {
                        //        Console.WriteLine(String.Format("have same index({0}), but info not same", strIndex));
                        //    }
                        //}
                        //else
                        {
                            try
                            {
                                indexInsts.Add(strIndex, inst);
                            }
                            catch
                            {
                                continue;
                            }
                            //indexInsts.Add(strIndex, inst);
                        }
                    }
                }
            }
            return indexInsts;
        }

        /// <summary>
        /// 表索引
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        List<string> TestGetIndexNames(string table)
        {
            List<string> indexLeafsName = new List<string>();
            string dataBasePath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\5GCfg\\";
            string dataMdbPath = "lm.mdb";//1.数据库

            // 表内容
            string strSQL = String.Format(
                "select * from MibTree where MIBName='{0}' and DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine", table);
            DataSet TableMibdateSet = new CfgAccessDBManager().GetRecord(dataBasePath + dataMdbPath, strSQL);
            DataRow TableRow = TableMibdateSet.Tables[0].Rows[0];
            string strTableName = TableRow["MIBName"].ToString().Trim(' ');
            if (!String.Equals(strTableName, table))
            {
                Console.WriteLine(String.Format("inputTalName=({0}),getMdbTalName=({1})不相同，有问题.", table, strTableName));//不相同，有问题
                return null;
            }
            string tableOid = TableRow["OID"].ToString();//做叶子的父节点oid

            // 叶子节点
            string strSQL2 = String.Format(
                "select * from mibtree where ParentOID ='{0}' and IsLeaf <> 0 and ICFWriteAble <> '×' order by ExcelLine", tableOid);
            DataSet MibLeafsDateSet = new CfgAccessDBManager().GetRecord(dataBasePath + dataMdbPath, strSQL2);
            int childcount = MibLeafsDateSet.Tables[0].Rows.Count;
            if (childcount == 0)
            {
                Console.WriteLine(String.Format("TalName=({0}),没有叶子节点.", strTableName));//不相同，有问题
                return null;
            }
            //int tableIndexNum = 0;// 索引个数
            string strleafName = "";
            for (int loop = 0; loop < MibLeafsDateSet.Tables[0].Rows.Count; loop++)//在表之间循环
            {
                DataRow leafRow = MibLeafsDateSet.Tables[0].Rows[loop];
                if ((bool)leafRow["IsIndex"])//是否为索引??????? col=39
                {
                    strleafName = leafRow["MIBName"].ToString();
                    //tableIndexNum++;
                    indexLeafsName.Add(strleafName);
                }
            }

            new CfgAccessDBManager().Close(TableMibdateSet);
            new CfgAccessDBManager().Close(MibLeafsDateSet);
            return indexLeafsName;
        }
        List<StruCfgFileFieldInfo> TestGetIndexLeafInfo(string table, Dictionary<string, StruCfgFileFieldInfo> LeafHead)
        {
            List<StruCfgFileFieldInfo> indexLeafsInfo = new List<StruCfgFileFieldInfo>();
            List<string> indexLeafsName = new List<string>();
            string dataBasePath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\5GCfg\\";
            string dataMdbPath = "lm.mdb";//1.数据库

            // 表内容
            string strSQL = String.Format(
                "select * from MibTree where MIBName='{0}' and DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine", table);
            DataSet TableMibdateSet = new CfgAccessDBManager().GetRecord(dataBasePath + dataMdbPath, strSQL);
            DataRow TableRow = TableMibdateSet.Tables[0].Rows[0];
            string strTableName = TableRow["MIBName"].ToString().Trim(' ');
            if (!String.Equals(strTableName, table))
            {
                Console.WriteLine(String.Format("inputTalName=({0}),getMdbTalName=({1})不相同，有问题.", table, strTableName));//不相同，有问题
                return null;
            }
            string tableOid = TableRow["OID"].ToString();//做叶子的父节点oid

            // 叶子节点
            string strSQL2 = String.Format(
                "select * from mibtree where ParentOID ='{0}' and IsLeaf <> 0 and ICFWriteAble <> '×' order by ExcelLine", tableOid);
            DataSet MibLeafsDateSet = new CfgAccessDBManager().GetRecord(dataBasePath + dataMdbPath, strSQL2);
            int childcount = MibLeafsDateSet.Tables[0].Rows.Count;
            if (childcount == 0)
            {
                Console.WriteLine(String.Format("TalName=({0}),没有叶子节点.", strTableName));//不相同，有问题
                return null;
            }
            //int tableIndexNum = 0;// 索引个数
            string strleafName = "";
            for (int loop = 0; loop < MibLeafsDateSet.Tables[0].Rows.Count; loop++)//在表之间循环
            {
                DataRow leafRow = MibLeafsDateSet.Tables[0].Rows[loop];
                if ((bool)leafRow["IsIndex"])//是否为索引??????? col=39
                {
                    strleafName = leafRow["MIBName"].ToString();
                    //tableIndexNum++;
                    indexLeafsName.Add(strleafName);
                }
            }
            foreach (string leaf in LeafHead.Keys)
            {
                //StruCfgFileFieldInfo FieldH = LeafHead[leaf];
                if (-1 != indexLeafsName.FindIndex(e => String.Equals(e, leaf)))// 是否是索引
                {
                    // 索引
                    indexLeafsInfo.Add(LeafHead[leaf]);
                }
            }

            new CfgAccessDBManager().Close(TableMibdateSet);
            new CfgAccessDBManager().Close(MibLeafsDateSet);
            return indexLeafsInfo;
        }
        //public DataSet CfgGetRecordByAccessDb(string fileName, string sqlContent)
        //{
        //    DataSet dateSet = new DataSet();
        //    AccessDBManager mdbData = new AccessDBManager(fileName);//fileName = "D:\\C#\\SCMT\\lm.mdb";
        //    try
        //    {
        //        mdbData.Open();
        //        dateSet = mdbData.GetDataSet(sqlContent);
        //        mdbData.Close();
        //    }
        //    finally
        //    {
        //        mdbData = null;
        //    }
        //    return dateSet;
        //}
        /// <summary>
        /// 获取表的所有实例
        /// </summary>
        /// <param name="filePath">文件地址</param>
        /// <param name="offset">表头的位置</param>
        /// <param name="u32RecNum">表的实例的个数</param>
        /// /// <param name="u16FieldNum">表的叶子的个数</param>
        /// <param name="u16FieldLen">每个实例的长度</param>
        /// <returns></returns>
        List<byte[]> GetInstsData(string filePath, uint offset, int u32RecNum, int u16FieldNum, int u16FieldLen)
        {
            List<byte[]> re = new List<byte[]>();
            int instOffset = 0;
            instOffset += (int)offset;// 表头位置
            instOffset += Marshal.SizeOf(new StruCfgFileTblInfo(""));// 44字节 叶子节头点位置
            instOffset += Marshal.SizeOf(new StruCfgFileFieldInfo("")) * u16FieldNum;//第一个实例位置
            for (int i = 0; i < u32RecNum; i++)
            {
                int pos = instOffset + u16FieldLen * i;
                byte[] YsInstsData = new CfgOp().CfgReadFile(filePath, pos, u16FieldLen);
                re.Add(YsInstsData);
            }
            return re;
        }
        bool TestIsSameInstsList(BinaryWriter bw, string tableName,int u32RecNum,  Dictionary<string, StruCfgFileFieldInfo> LeafHead, List<byte[]> InstsAList, List<byte[]> InstsBList)
        {
            bool re = true;
            byte[] instA;
            byte[] instB;
            for (int instNo=0; instNo< u32RecNum; instNo++)
            {
                instA = InstsAList[instNo];//实例
                instB = InstsBList[instNo];
                if (!TestIsSameInst(instNo.ToString(), bw,tableName, LeafHead, instA, instB))
                {
                    re = false;
                    bw.Write(String.Format("table({0}), instNo({1}) info no same.\n", tableName));
                    Console.WriteLine(String.Format("table({0}), instNo({1}) info no same", tableName, instNo));
                    break;
                }
            }
            return re;
        }
        /// <summary>
        /// 告警
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="u32RecNum"></param>
        /// <param name="LeafHead"></param>
        /// <param name="InstsAList"></param>
        /// <param name="InstsBList"></param>
        /// <returns></returns>
        bool TestIsSameInstsListAlarm(BinaryWriter bw, string tableName, int u32RecNum, Dictionary<string, StruCfgFileFieldInfo> LeafHead, List<byte[]> InstsAList, List<byte[]> InstsBList)
        {
            bool re = true;

            // 检查所有的告警编号
            if (!TestIsSameAlarmNo(u32RecNum, InstsAList, InstsBList))
            {
                Console.WriteLine(String.Format("告警编号不同"));
                re = false;
            }

            // 如果告警编号没有问题， 以告警编号为key ，重新排列实例
            Dictionary<uint, byte[]> AlarmNoAndInfoA = GetAlarmNoAndInfo(u32RecNum, InstsAList);
            Dictionary<uint, byte[]> AlarmNoAndInfoB = GetAlarmNoAndInfo(u32RecNum, InstsBList);
            // 对比实例
            byte[] instA;
            byte[] instB;
            foreach (uint alno in AlarmNoAndInfoA.Keys)
            {
                instA = AlarmNoAndInfoA[alno];//实例
                instB = AlarmNoAndInfoB[alno];
                //
                if (!TestIsSameInst(alno.ToString(), bw, tableName, LeafHead, instA, instB))
                {
                    re = false;
                    Console.WriteLine(String.Format("table({0}), alarmNo({1}) info no same", tableName, alno));
                    break;
                }
            }
            return re;
        }
        /// <summary>
        /// 告警
        /// </summary>
        /// <param name="u32RecNum"></param>
        /// <param name="InstsAList"></param>
        /// <param name="InstsBList"></param>
        /// <returns></returns>
        bool TestIsSameAlarmNo(int u32RecNum, List<byte[]> InstsAList, List<byte[]> InstsBList)
        {
            List<uint> alarmNoA = GetAllAlarmNoForInsts(u32RecNum, InstsAList);
            List<uint> alarmNoB = GetAllAlarmNoForInsts(u32RecNum, InstsBList);
            bool re = true;
            if (alarmNoA.Count != alarmNoB.Count)
            {
                Console.WriteLine(String.Format("alarm no num not same.A.num={0},b.num={1}.", alarmNoA.Count, alarmNoB.Count));
                re = false;
            }

            if (!TestIsSameAlarmNoBeyondCom(alarmNoA, alarmNoB))
            {
                Console.WriteLine(String.Format("alarmA no all not same than alarmB."));
                re = false;
            }
            if (!TestIsSameAlarmNoBeyondCom(alarmNoB, alarmNoA))
            {
                Console.WriteLine(String.Format("alarmB no all not same than alarmA."));
                re = false;
            }
            return re;
        }
        /// <summary>
        /// 告警
        /// </summary>
        /// <param name="u32RecNum"></param>
        /// <param name="InstsAList"></param>
        /// <returns></returns>
        List<uint> GetAllAlarmNoForInsts(int u32RecNum, List<byte[]> InstsAList)
        {
            byte[] inst;
            uint alarmCauseNo = 0;
            byte[] emtpyArray = new byte[InstsAList[0].LongCount()];
            emtpyArray.Initialize();

            List<uint> alarmNo = new List<uint>();
            for (int instNo = 0; instNo < u32RecNum; instNo++)
            {
                // 判断告警号：数量，内容是否相同
                inst = InstsAList[instNo];//实例
                if (TestIsByteListSame(emtpyArray, inst))
                {
                    Console.WriteLine("===");
                    break;// 结束
                }
                alarmCauseNo = GetBytesValToUint2(inst.Skip(0).Take(4).Reverse().ToArray());
                if (-1 != alarmNo.FindIndex(e => e == alarmCauseNo))
                {
                    // 出现重复
                    Console.WriteLine(String.Format("出现重复的告警编号{0}", alarmCauseNo));
                }
                alarmNo.Add(alarmCauseNo);
            }
            return alarmNo;
        }
        bool TestIsByteListSame(byte[] aArray, byte[] bArray)
        {
            bool re = true;
            for (int i = 0; i < aArray.Length; i++)
            {
                if (aArray[i] != bArray[i])
                {
                    re = false;
                    break;
                }
            }
            return re;
        }
        /// <summary>
        /// 告警
        /// </summary>
        /// <param name="alarmNoA"></param>
        /// <param name="alarmNoB"></param>
        /// <returns></returns>
        bool TestIsSameAlarmNoBeyondCom(List<uint> alarmNoA, List<uint> alarmNoB)
        {
            bool re = true;
            for (int no = 0; no < alarmNoA.Count; no++)
            {
                uint alarmNo = alarmNoA[no];
                int pos = alarmNoB.FindIndex(e => e == alarmNo);
                if (-1 == pos)
                {
                    Console.WriteLine(String.Format("Not have alarm num num={0}.", alarmNo));
                    re = false;
                    //break;
                }
            }
            return re;
        }
        /// <summary>
        /// 告警
        /// </summary>
        /// <param name="u32RecNum"></param>
        /// <param name="InstsAList"></param>
        /// <returns></returns>
        Dictionary<uint, byte[]> GetAlarmNoAndInfo(int u32RecNum, List<byte[]> InstsAList)
        {
            byte[] inst;
            uint alarmCauseNo = 0;
            byte[] emtpyArray = new byte[InstsAList[0].LongCount()];
            emtpyArray.Initialize();

            Dictionary<uint, byte[]> alarmNoAndInfo = new Dictionary<uint, byte[]>();
            for (int instNo = 0; instNo < u32RecNum; instNo++)
            {
                // 判断告警号：数量，内容是否相同
                inst = InstsAList[instNo];//实例
                if (TestIsByteListSame(emtpyArray, inst))
                {
                    break;// 结束
                }
                //
                alarmCauseNo = GetBytesValToUint2(inst.Skip(0).Take(4).Reverse().ToArray());
                if (-1 != alarmNoAndInfo.Keys.ToList().FindIndex(e => e == alarmCauseNo))
                {
                    // 出现重复
                    Console.WriteLine(String.Format("出现重复的告警编号{0}", alarmCauseNo));
                }
                alarmNoAndInfo.Add(alarmCauseNo, inst);
            }
            return alarmNoAndInfo;
        }
        bool TestIsSameInst(string strIndex, BinaryWriter bw, string tableName, Dictionary<string, StruCfgFileFieldInfo> LeafHead, byte[] instA, byte[] instB)
        {
            bool re = true;
            StruCfgFileFieldInfo FieldH;
            ushort u16FieldOffset;       /* 字段相对记录头偏移量*/
            ushort u16FieldLen;          /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
            byte u8FieldType;            /* 字段类型 */

            if (!BytesCompare_Base64(instA, instB))
            {
                foreach (string leafName in LeafHead.Keys)
                {
                    FieldH = LeafHead[leafName];
                    u16FieldOffset = FieldH.u16FieldOffset;       /* 字段相对记录头偏移量*/
                    u16FieldLen = FieldH.u16FieldLen;             /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
                    u8FieldType = FieldH.u8FieldType;             /* 字段类型 */

                    byte[] InA = instA.Skip(u16FieldOffset).Take(u16FieldLen).ToArray();
                    byte[] InB = instB.Skip(u16FieldOffset).Take(u16FieldLen).ToArray();                   
                    if (!BytesCompare_Base64(InA, InB))
                    {
                        re = false;
                        string inAstr = BitConverter.ToString(InA);
                        string inBstr = BitConverter.ToString(InB);
                        //int a = GetBytesValToInt(InA);
                        //int b = GetBytesValToInt(InB);
                        bw.Write(String.Format("({0}),index({2}),({1}),info:a.(0x{3}),b.(0x{4}) no same.\n", tableName, leafName, strIndex, inAstr, inBstr));
                        //bw.Write(String.Format("({0}),index({2}),({1}),info:a.(0x{3}),b.(0x{4}) no same.\n", tableName, leafName, strIndex, a, b));
                        Console.WriteLine(String.Format("table({0}),index({2}),leafName({1}),info:a.({3}),b.({4}).", tableName, leafName, strIndex, inAstr, inBstr));//, Encoding.ASCII.GetString(InA), Encoding.Default.GetString(InB)));
                        break;
                    }
                }
            }
            return re;
        }
        void TestIsSameInstDebugWrite(string strIndex, BinaryWriter bw, string tableName, Dictionary<string, StruCfgFileFieldInfo> LeafHead, byte[] instA, byte[] instB)
        {
            //bool re = true;
            StruCfgFileFieldInfo FieldH;
            ushort u16FieldOffset;       /* 字段相对记录头偏移量*/
            ushort u16FieldLen;          /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
            byte u8FieldType;            /* 字段类型 */

            foreach (string leafName in LeafHead.Keys)
            {
                FieldH = LeafHead[leafName];
                u16FieldOffset = FieldH.u16FieldOffset;       /* 字段相对记录头偏移量*/
                u16FieldLen = FieldH.u16FieldLen;             /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
                u8FieldType = FieldH.u8FieldType;             /* 字段类型 */

                byte[] InA = instA.Skip(u16FieldOffset).Take(u16FieldLen).ToArray();
                byte[] InB = instB.Skip(u16FieldOffset).Take(u16FieldLen).ToArray();
                string inAstr = BitConverter.ToString(InA);
                string inBstr = BitConverter.ToString(InB);
                //int a = GetBytesValToInt(InA);
                //int b = GetBytesValToInt(InB);
                if (!BytesCompare_Base64(InA, InB))
                {
                    bw.Write(String.Format("({0}),index({2}),({1}),info:a.(0x{3}),b.(0x{4}) not bugWrire.\n", tableName, leafName, strIndex, inAstr, inAstr));
                    Console.WriteLine(String.Format("table({0}),index({2}),leafName({1}),info:a.({3}),b.({4}).", tableName, leafName, strIndex, inAstr, inBstr));//, Encoding.ASCII.GetString(InA), Encoding.Default.GetString(InB)));
                }
                else
                {
                    bw.Write(String.Format("({0}),index({2}),({1}),info:a.(0x{3}),b.(0x{4}) yes bugWrire.\n", tableName, leafName, strIndex, inAstr, inAstr));
                    Console.WriteLine(String.Format("table({0}),index({2}),leafName({1}),info:a.({3}),b.({4}).", tableName, leafName, strIndex, inAstr, inBstr));//, Encoding.ASCII.GetString(InA), Encoding.Default.GetString(InB)));
                }
            }
            bw.Write(String.Format("\n"));
        }

        Dictionary<string, StruCfgFileFieldInfo> TestGetLeafHeadFieldInfo(string filePath, ushort u16FieldNumYs, uint offset)
        {
            CfgOp cfgOp = new CfgOp();
            int readCount = 0;
            byte[] Ysdata;
            Dictionary<string, StruCfgFileFieldInfo> leafFieldL = new Dictionary<string, StruCfgFileFieldInfo>();
            for (int i = 0; i < u16FieldNumYs; i++)
            {
                StruCfgFileFieldInfo leafFieldInfo = new StruCfgFileFieldInfo("");
                uint fristLeafFieldInfoPos = offset + (uint)Marshal.SizeOf(new StruCfgFileTblInfo("")) + (uint)Marshal.SizeOf(new StruCfgFileFieldInfo("")) * (uint)i;// 第一个叶子头的位置
                readCount = Marshal.SizeOf(new StruCfgFileFieldInfo(""));// 60
                Ysdata = cfgOp.CfgReadFile(filePath, fristLeafFieldInfoPos, readCount);
                leafFieldInfo.SetValueByBytes(Ysdata);
                string leafName = Encoding.GetEncoding("GB2312").GetString(leafFieldInfo.Getu8FieldName()).TrimEnd('\0');
                leafFieldL.Add(leafName, leafFieldInfo);
            }

            return leafFieldL;
        }
        bool TestIsSameLeafHeadInfoList(Dictionary<string, StruCfgFileFieldInfo> leafHLA, Dictionary<string, StruCfgFileFieldInfo> leafHLB)
        {
            bool re = true;
            foreach (string leafName in leafHLA.Keys)
            {
                StruCfgFileFieldInfo leafHA = leafHLA[leafName];
                StruCfgFileFieldInfo leafHB = leafHLB[leafName];
                if (!TestIsSameLeafHeadInfo(leafName,leafHA, leafHB))
                {
                    re = false;
                    Console.WriteLine(String.Format("leafname={0}, leafHeadInfo not all same.", leafName));
                    break;
                }
            }
            return re;
        }
        bool TestIsSameLeafHeadInfo(string leafName,StruCfgFileFieldInfo leafHA, StruCfgFileFieldInfo leafHB)
        {
            bool re = true;
            /* [48] 字段名 */
            string strFieldNameA = Encoding.GetEncoding("GB2312").GetString(leafHA.Getu8FieldName()).TrimEnd('\0');
            string strFieldNameB = Encoding.GetEncoding("GB2312").GetString(leafHB.Getu8FieldName()).TrimEnd('\0');
            if (-1 == String.Compare(strFieldNameA, strFieldNameB))
            {
                re = false;
                Console.WriteLine(String.Format("FieldName={0}; leafName(a,{1},b.{2}) no same", leafName, strFieldNameA, strFieldNameB));
            }
            else if (leafHA.u16FieldOffset != leafHB.u16FieldOffset)/* 字段相对记录头偏移量*/
            {
                re = false;
                Console.WriteLine(String.Format("FieldName={0}; u16FieldOffset(a,{1},b.{2}) no same", leafName, leafHA.u16FieldOffset, leafHB.u16FieldOffset));
            }
            else if (leafHA.u16FieldLen != leafHB.u16FieldLen)/* 字段长度 单位：字节 */
            {
                re = false;
                Console.WriteLine(String.Format("FieldName={0}; u16FieldLen(a,{1},b.{2}) no same", leafName, leafHA.u16FieldLen, leafHB.u16FieldLen));
            }
            else if (leafHA.u8FieldType != leafHB.u8FieldType)/* 字段类型 */
            {
                re = false;
                Console.WriteLine(String.Format("FieldName={0}; u8FieldType(a,{1},b.{2}) no same", leafName, leafHA.u8FieldType, leafHB.u8FieldType));
            }
            else if (leafHA.u8FieldTag != leafHB.u8FieldTag)/* 字段是否为关键字 */
            {
                re = false;
                Console.WriteLine(String.Format("FieldName={0}; u8FieldTag(a,{1},b.{2}) no same", leafName, leafHA.u8FieldTag, leafHB.u8FieldTag));
            }
            else if (leafHA.u8SaveTag != leafHB.u8SaveTag)/* 字段是否需要存盘 */
            {
                re = false;
                Console.WriteLine(String.Format("FieldName={0}; u8SaveTag(a,{1},b.{2}) no same", leafName, leafHA.u8SaveTag, leafHB.u8SaveTag));
            }
            else if (leafHA.u8ConfigFlag != leafHB.u8ConfigFlag)/* 字段是否可(需要)配置,0:不可配，1：可配*/
            {
                re = false;
                Console.WriteLine(String.Format("FieldName={0}; u8ConfigFlag(a,{1},b.{2}) no same", leafName, leafHA.u8ConfigFlag, leafHB.u8ConfigFlag));
            }
            //Console.WriteLine(String.Format("FieldName={0}; u16FieldOffset(a,{1},b.{2}) ", leafName, leafHA.u16FieldOffset, leafHB.u16FieldOffset));
            //Console.WriteLine(String.Format("FieldName={0}; u16FieldLen(a,{1},b.{2}) ", leafName, leafHA.u16FieldLen, leafHB.u16FieldLen));
            return re;
        }

        /// <summary>
        /// 获取每个表的头
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        StruCfgFileTblInfo TestGetTableHeadInfo(string filePath, uint offset)
        {
            // 跳过 偏移量 获取tableInfo表块介绍
            StruCfgFileTblInfo ysTblInfo = new StruCfgFileTblInfo("");
            int readCount = Marshal.SizeOf(new StruCfgFileTblInfo(""));// 44字节
            byte[] Ysdata = new CfgOp().CfgReadFile(filePath, offset, readCount);
            ysTblInfo.SetBytesValue(Ysdata);
            return ysTblInfo;
        }

        bool TestIsSameTableHeadField(string tableName, StruCfgFileTblInfo tblFieldA, StruCfgFileTblInfo tblFieldB)
        {
            bool re = true;            
            string tablNameA = Encoding.GetEncoding("GB2312").GetString(tblFieldA.u8TblName).TrimEnd('\0');
            string tablNameB = Encoding.GetEncoding("GB2312").GetString(tblFieldB.u8TblName).TrimEnd('\0');
            if (-1 == String.Compare(tablNameA, tablNameB))
            {
                re = false;
                Console.WriteLine(String.Format("TableHead:TableName={0}; tableName(a,{1},b.{2}) no same", tableName, tablNameA, tablNameB));
            }
            else if (tblFieldA.u16DataFmtVer != tblFieldB.u16DataFmtVer)
            {
                re = false;
                Console.WriteLine(String.Format("TableHead:TableName={0}; u16DataFmtVer(a,{1},b.{2}) no same", tableName, tblFieldA.u16DataFmtVer, tblFieldB.u16DataFmtVer));
            }
            else if (tblFieldA.u16FieldNum != tblFieldB.u16FieldNum)
            {
                re = false;
                Console.WriteLine(String.Format("TableHead:TableName={0}; u16FieldNum(a,{1},b.{2}) no same", tableName, tblFieldA.u16FieldNum, tblFieldB.u16FieldNum));
            }
            else if (tblFieldA.u16RecLen != tblFieldB.u16RecLen)
            {
                re = false;
                Console.WriteLine(String.Format("TableHead:TableName={0}; u16RecLen(a,{1},b.{2}) no same", tableName, tblFieldA.u16RecLen, tblFieldB.u16RecLen));
            }
            else if (tblFieldA.u32RecNum != tblFieldB.u32RecNum)
            {
                re = false;
                Console.WriteLine(String.Format("TableHead:TableName={0}; u32RecNum(a,{1},b.{2}) no same", tableName, tblFieldA.u32RecNum, tblFieldB.u32RecNum));
            }
            //Console.WriteLine(String.Format("**** TableHead:TableName={0}; 记录有效长度 u16RecLen (a,{1},b.{2})", tableName, tblFieldA.u16RecLen, tblFieldB.u16RecLen));
            return re;
        }

        bool BytesCompare_Base64(byte[] b1, byte[] b2)
        {
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            return string.Compare(Convert.ToBase64String(b1), Convert.ToBase64String(b2), false) == 0 ? true : false;
        }


        /// <summary>
        /// 包含的表是否相同
        /// </summary>
        /// <param name="YSFilePath"></param>
        /// <param name="NewFilePath"></param>
        /// <returns></returns>
        bool TestBeyondCompFileTableNameMain(BinaryWriter bw, string YSFilePath, string NewFilePath)
        {
            StruDataHead YsDhead = GetDataHeadFromFile(YSFilePath);
            List<uint> YsTablePos = GetTablesPos(YSFilePath, (int)YsDhead.u32TableCnt);
            List<string> YsTableNames = GetTableNamesByTablesPos(YSFilePath, YsTablePos);

            StruDataHead NewDhead = GetDataHeadFromFile(NewFilePath);
            List<uint> NewTablePos = GetTablesPos(NewFilePath, (int)NewDhead.u32TableCnt);
            List<string> NewTableNames = GetTableNamesByTablesPos(NewFilePath, NewTablePos);

            return TestBeyondTableName(YsTableNames, NewTableNames);
        }

        void TestBeyondCompare()
        {
            string dataBasePath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\";
            string YSFilePath = dataBasePath + "5GCfg\\init_qyx.cfg";
            string NewFilePath = dataBasePath + "init.cfg";


            CfgOp cfgOp = new CfgOp();

            // 获取 tableNum
            int offset = 956;
            int readCount = 24;
            byte[] Ysdata = cfgOp.CfgReadFile(YSFilePath, offset, readCount);
            byte[] Newdata = cfgOp.CfgReadFile(NewFilePath, offset, readCount);

            StruDataHead ysD = new StruDataHead("");
            ysD.SetValueByBytes(Ysdata);
            int tableNumYs = (int)ysD.u32TableCnt;

            StruDataHead newD = new StruDataHead("");
            newD.SetValueByBytes(Newdata);
            int tableNumNew = (int)newD.u32TableCnt;

            // 跳过 偏移量 获取tableInfo表块介绍
            StruCfgFileTblInfo ysTblInfo = new StruCfgFileTblInfo("");
            readCount = Marshal.SizeOf(new StruCfgFileTblInfo(""));
            offset = 956 + 24 + 4 * tableNumYs;
            Ysdata = cfgOp.CfgReadFile(YSFilePath, offset, readCount);
            ysTblInfo.SetBytesValue(Ysdata);
            ushort u16FieldNumYs = ysTblInfo.u16FieldNum;//叶子数
            ushort u16RecLenYs = ysTblInfo.u16RecLen;//每个表实例的长度

            StruCfgFileTblInfo newTblInfo = new StruCfgFileTblInfo("");
            offset = 956 + 24 + 4 * tableNumNew;
            Newdata = cfgOp.CfgReadFile(NewFilePath, offset, readCount);
            newTblInfo.SetBytesValue(Newdata);
            ushort u16FieldNumNew = newTblInfo.u16FieldNum;//叶子数
            ushort u16RecLenNew = newTblInfo.u16RecLen;//每个表实例的长度

            //
        }

        void testBeyondCompare1()
        {
            TestStruDataHead te = new TestStruDataHead();
            bool re = te.testBeyondCompare();

        }

      
        string OxbytesToString(byte[] bytes)
        {
            string hexString = string.Empty;
            Array.Reverse((byte[])bytes);
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    strB.Append(bytes[i].ToString("X2"));
                }
                hexString = strB.ToString();
            }
            return hexString;
        }

        uint GetBytesValToUint(byte[] bytes)
        {
            Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToUInt32(OxbytesToString(bytes), 16);
        }
        uint GetBytesValToUint2(byte[] bytes)
        {
            //Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToUInt32(OxbytesToString(bytes), 16);
        }
        int GetBytesValToInt(byte[] bytes)
        {
            Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToInt32(OxbytesToString(bytes), 16);
        }

        byte[] GetBytesValueToParmBytes(byte[] bytes)
        {
            byte[] re = new byte[] { };
            Array.Reverse((byte[])bytes);
            Buffer.BlockCopy((byte[])bytes, 0, (byte[])re, 0, ((byte[])bytes).Length);
            return re;
        }

        object GetByteValueToParm(object objParm, byte[] bytes)
        {
            object re = null; 
            if (objParm is byte)
            {
                re = bytes[0];
            }
            else if (objParm is byte[])
            {
                Array.Reverse((byte[])bytes);
                Buffer.BlockCopy((byte[])bytes, 0, (byte[])re, 0, ((byte[])bytes).Length);
            }
            //else if (objParm is sbyte[])
            //{
            //    Array.Reverse((byte[])bytes);
            //    Buffer.BlockCopy((sbyte[])bytes, 0, (byte[])re, bytePosL[0], ((sbyte[])objParm).Length);
            //    bytePosL[0] += ((sbyte[])objParm).Length;
            //}
            else if (objParm is ushort)
            {
                Array.Reverse((byte[])bytes);
                string reStr = OxbytesToString(bytes);
                re = (ushort)Convert.ToUInt16(reStr, 16);
            }
            else if (objParm is uint)
            {
                Array.Reverse((byte[])bytes);
                string reStr = OxbytesToString(bytes);
                re = Convert.ToUInt32(OxbytesToString(bytes), 16);
                //byte[] TypeToByteArr = BitConverter.GetBytes((uint)objParm); //  数据块起始位置 
                //Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                //bytePosL[0] += TypeToByteArr.Length;
            }
            //else if (objParm is uint[])
            //{
            //    re = new List<uint>();
            //    foreach (var ui in (uint[])objParm)
            //    {
            //        byte[] TypeToByteArr = BitConverter.GetBytes((uint)ui); //  数据块起始位置 
            //        Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
            //        bytePosL[0] += TypeToByteArr.Length;
            //    }
            //}
            else
            {
                Console.WriteLine(String.Format("SetValueToByteArray : new type : value={0}, type={1}", objParm.ToString(), objParm.GetType()));
            }
            return re;
        }

        /// <summary>
        /// 生成 init 和 patch
        /// </summary>
        void testForCreatePatchAndInit()
        {
            FileStream fs = new FileStream("WriteInitPatchLog.txt", FileMode.Create);
            //实例化BinaryWriter
            BinaryWriter bw = new BinaryWriter(fs);
            string dataBasePath  = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\5GCfg\\";
            string dataMdbPath   = "lm.mdb";//1.数据库
            string antennaExPath = "LTE_基站天线广播波束权值参数配置表_5G.xls";//2.天线信息
            string alarmExPath   = "eNB告警信息表.xls";//3.告警信息
            string rruInfoExPath = "RRU基本信息表.xls";//4.RRU信息
            //string reclistExPath = "RecList_V6.00.50.05.40.07.01.xls";//5.
            string reclistExPath = "5G NSA无线网络和业务参数标定手册_V1.00.03-华为版本.xls";//5.reclist
            string selfDefExPath = "自定义_初配数据文件_ENB_5G_00_00_05.xls";//6.自定义文件(init, patch)

            CfgOp cfgOp = new CfgOp();
            Dictionary<string, string> paths = new Dictionary<string, string>() {
                { "DataMdb", dataBasePath+dataMdbPath},
                { "Antenna", dataBasePath+antennaExPath},
                { "Alarm",   dataBasePath+alarmExPath},
                { "RruInfo" ,dataBasePath+rruInfoExPath},
                { "Reclist" ,dataBasePath+reclistExPath},
                { "SelfDef" ,dataBasePath+selfDefExPath},
                { "OutDir" , dataBasePath },
            };
            cfgOp.CreatePatchAndInitCfg(bw, paths);

            //清空缓冲区
            bw.Flush();
            //关闭流
            bw.Close();
            fs.Close();
        }
        /// <summary>
        /// 命令行生成
        /// </summary>
        /// <param name="paths"></param>
        bool CmdlineCreateInitPatch(string[] args)
        {
            bool re = true;
            if (args.Count() == 0)
            {
                Console.WriteLine("Err need input par Count is 0.\n");
                return false;
            }
            Dictionary<string, string> path = new Dictionary<string, string>();
            foreach (var par in args)
            {
                string par_str = par.ToString();
                int pos = par_str.IndexOf(':');
                if (pos != -1)
                {
                    string key = par_str.Substring(0, pos);
                    string val = par_str.Substring(pos + 1);
                    path.Add(key, val);
                    Console.WriteLine(String.Format("Cmdline CreateInitPatch par: key({0}), val({1}).\n", key, val));
                }
            }
            if (!path.ContainsKey("OutDir") || !Directory.Exists(path["OutDir"]))
            {
                Console.WriteLine("Err need output dir info...\n");
                return false;
            }
            string fileName = path["OutDir"]+"LogInitPatch.txt";
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            //实例化BinaryWriter
            BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8, true);
            bw.Write(String.Format("Cmdline CreateInitPatch Start... Time is {0}. \n", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒")).ToArray());
            Console.WriteLine(String.Format("....CmdlineCreateInitPatch Start... Time is ") + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));
            try
            {
                re = new CfgOp().CreatePatchAndInitCfg(bw, path);
            }
            catch
            {
                Console.WriteLine("Err exe Death...");
            }
            finally
            {
                CfgExcelOp.GetInstance().Dispose();
            }
            bw.Write(String.Format("Cmdline CreateInitPatch End. Time is {0}. \n", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒")).ToArray());
            Console.WriteLine(String.Format("....testCmdlineCreateInitPatch End. Time is ") + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

            //清空缓冲区
            bw.Flush();
            //关闭流
            bw.Close();
            fs.Close();
            return re;
        }

        /// <summary>
        /// 命令行生成
        /// </summary>
        /// <param name="paths"></param>
        bool CmdlineCreatePatch(string[] args)
        {
            //////////////
            bool re = true;
            if (args.Count() == 0)
            {
                Console.WriteLine("Err need input par Count is 0.\n");
                return false;
            }
            Dictionary<string, string> path = new Dictionary<string, string>();
            foreach (var par in args)
            {
                string par_str = par.ToString();
                int pos = par_str.IndexOf(':');
                if (pos != -1)
                {
                    string key = par_str.Substring(0, pos);
                    string val = par_str.Substring(pos + 1);
                    path.Add(key, val);
                    Console.WriteLine(String.Format("Cmdline CreateInitPatch par: key({0}), val({1}).\n", key, val));
                }
            }
            if (!path.ContainsKey("OutDir") || !Directory.Exists(path["OutDir"]))
            {
                Console.WriteLine("Err need output dir info...\n");
                return false;
            }
            
            string fileName = path["OutDir"] +"\\\\"+ "LogPatch.txt";
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            //实例化BinaryWriter

            BinaryWriter bw = new BinaryWriter(fs, Encoding.Default, true);
            bw.Write(String.Format("Cmdline CreatePatch Start... Time is {0}. \n", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒")).ToArray());
            Console.WriteLine(String.Format("....CmdlineCreatePatch Start... Time is ") + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

            //try
            {
                CfgOp cf = new CfgOp();
                re = cf.CreatePatchCfg(bw, path);
            }
            //catch
            {
                Console.WriteLine("Err exe Death...");
            }
            //finally
            {
                Console.WriteLine("excel dispose.");
                CfgExcelOp.GetInstance().Dispose();
            }
            bw.Write(String.Format("Cmdline CreateInitPatch End. Time is {0}. \n", DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒")).ToArray());
            Console.WriteLine(String.Format("....testCmdlineCreateInitPatch End. Time is ") + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒"));

            //清空缓冲区
            bw.Flush();
            //关闭流
            bw.Close();
            fs.Close();
            return re;
        }

        void testForParseAlarmEx()
        {
            string alarmMdbPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\LMTAlarm.mdb";
            string alarmExPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\eNB告警信息表.xls";
            CfgParseAlarmExecl alarmEx = new CfgParseAlarmExecl();
            alarmEx.CfgParseAlarmExeclAndMdb(alarmExPath, alarmMdbPath);
        }


        void testForReadSelfExcel()
        {
            // 加载lm.mdb到内存
            CfgOp cfgOp = new CfgOp();
            string strCfgFileName = "";
            string FileToDirectory = "";
            string strDBPath = "";
            string strDBName = ".\\Data\\lmdtz\\lm.dtz";
            //cfgOp.CreateCfgFile(strCfgFileName, FileToDirectory, strDBPath, strDBName);
            BinaryWriter bw = null;
            // reclist
            cfgOp.m_reclistExcel = new CfgParseReclistExcel();
            string excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\RecList_V6.00.50.05.40.07.01.xls";
            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
            string UeType = "0:默认";
            //reclist.ProcessingExcel(excelPath, strFileToDirectory, UeType, cfgOp);
            cfgOp.m_reclistExcel.ProcessingExcel(bw, excelPath, strFileToDirectory, UeType, cfgOp);

            // 自定义文件
            excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\自定义_初配数据文件_ENB_5G_00_00_05.xls";
            //CfgParseSelfExcel selfEx = new CfgParseSelfExcel();
            //selfEx.ProcessingExcel(excelPath, strFileToDirectory, "init", cfgOp);
            //selfEx.ProcessingExcel(excelPath, strFileToDirectory, "patch", cfgOp);
            cfgOp.m_selfExcel = new CfgParseSelfExcel();
            cfgOp.m_selfExcel.ProcessingExcel(bw, excelPath, strFileToDirectory, "init", cfgOp);
            cfgOp.m_selfExcel.ProcessingExcel(bw, excelPath, strFileToDirectory, "patch", cfgOp);
        }

        void testForReadRecList()
        {
            // 加载lm.mdb到内存
            CfgOp cfgOp = new CfgOp();
            string strCfgFileName = "";
            string FileToDirectory = "";
            string strDBPath = "";
            string strDBName = ".\\Data\\lmdtz\\lm.dtz";
            //cfgOp.CreateCfgFile(strCfgFileName, FileToDirectory, strDBPath, strDBName);
            BinaryWriter bw = null;
            // reclist
            //CfgParseReclistExcel reclist = new CfgParseReclistExcel();
            string excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\RecList_V6.00.50.05.40.07.01.xls";
            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
            string UeType = "0:默认";
            cfgOp.m_reclistExcel.ProcessingExcel(bw, excelPath, strFileToDirectory, UeType, cfgOp);
        }

        void testLoadMibTreeIntoMem()
        {
            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
            CfgParseDBMibTreeToMemory mibTree = new CfgParseDBMibTreeToMemory();
            ///mibTree.ReadMibTreeToMemory(null,strFileToDirectory);
            int a = 1;
        }

        void testForReadExcelRruType()
        {
            CfgParseRruExcel rru = new CfgParseRruExcel();
            string excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\RRU基本信息表_ty.xls";
            //string sheetName = "RRU基本信息表";
            //rru.ProcessingExcel(excelPath, sheetName);


            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\LMTDBENODEB70.mdb";
            //List<RRuTypeTabStru> rruList = rru.GetRruTypeInfoData();
            rru.TestMdbAndExcel(excelPath, strFileToDirectory);
            //List<RRuTypePortTabStru> rruPortL = rru.GetRruTypePortInfoData();
        }

        void testForReadExcelAnnt()
        {
            CfgParseAntennaExcel dd = new CfgParseAntennaExcel();
            string excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\LTE_基站天线广播波束权值参数配置表_5G.xls";
            string sheetName = "波束扫描原始值";

            string dataBasePath = "D:\\公司资料\\80patch\\";
            string dataMdbPath = "LMTDBENODEB70_qyx.mdb";//1.数据库

            dd.ProcessingAlarmMdb(dataBasePath+ dataMdbPath);

            dd.ProcessingAntennaExcel(excelPath, sheetName,1000);
            dd.BeyondCompMdbAndExcel();
            //List<Dictionary<string, string>> data = dd.GetBeamScanData();
        }

        void testForOpReadExcelForCfg()
        {
            //CfgExcelOp exOp = new CfgExcelOp();
            var excelOp = CfgExcelOp.GetInstance();
            //excelOp.test(".\\123\\eNB告警信息表.xls");

        }

        void testForCfgFileOp()
        {
            string strCfgFileName = "";
            string FileToDirectory = "";
            string strDBPath = "";
            string strDBName = ".\\Data\\lmdtz\\lm.dtz";

            CfgOp cfgOp = new CfgOp();
            //cfgOp.CreateCfgFile(strCfgFileName, FileToDirectory, strDBPath, strDBName);
            cfgOp.SaveFile_eNB("./path.cfg");
        }

        void test1()
        {
            uint dreamduip = new Test().getIPAddr("192.168.3.144");
            bool re = new Test().TestForCfgFileOpStructMain();
        }
        public void test2()
        {
            string str1 = "1987-04-20 23:05:59"; 
            byte[] strToBytes1 = System.Text.Encoding.UTF8.GetBytes(str1);//可以
            byte[] strToBytes2 = System.Text.Encoding.Default.GetBytes(str1);//可以
            byte[] strToBytes3 = System.Text.Encoding.Unicode.GetBytes(str1);//no
            byte[] strToBytes4 = System.Text.Encoding.ASCII.GetBytes(str1);//可以
            byte[] strToBytes5 = System.Text.Encoding.UTF32.GetBytes(str1);//no
            byte[] strToBytes6 = System.Text.Encoding.UTF7.GetBytes(str1);//可以
            if (str1.Length < 19)
                return ;
            byte b = strToBytes1[4];
            if ((strToBytes1[4] != '-') || (strToBytes1[7] != '-') || (strToBytes1[10] != ' ') || (strToBytes1[13] != ':') || (strToBytes1[16] != ':'))
                return ;
            if ((str1[4] != '-') || (str1[7] != '-') || (str1[10] != ' ') || (str1[13] != ':') || (str1[16] != ':'))
                return ;
        }
        public void test3()
        {
            bool a = IsNumeric("dd");
            bool a2 = IsNumeric2("dd");
            bool a3 = IsNumeric("1234");
            bool a4 = IsNumeric2("1234");
        }
        bool IsNumeric(string InStr)
        {
            //
            //    //当数字字符串的为是少于4时，以下三种都可以转换，任选一种
            //    //如果位数超过4的话，请选用Convert.ToInt32() 和int.Parse()
            //    //result = int.Parse(message);
            
            //cfgHandle.CreateCfgFile("", fileToUnzip, fileToDire);
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(InStr))
            {
                return true;
            }
            else
                return false;
        }
        bool IsNumeric2(string InStr)
        {
            int result = -1;   //result 定义为out 用来输出值
            try
            {
                result = Convert.ToInt32(InStr);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void test4()
        {
            bool a = IsValidateDate(2018,2,40);//日
            bool a2 = IsValidateDate(2018, 13, 31);//月
            bool a3 = IsValidateDate(1, 2, 2);//年
            bool a4 = IsValidateDate(2018, 2, 3);
        }
        bool IsValidateDate(int y, int m, int d)
        {
            int[] a = { 31, (y % 4 == 0 && y % 100 != 0 || y % 400 == 0) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            return m >= 1 && m <= 12 && d >= 1 && d <= a[m - 1];
        }

        bool TestForCfgFileOpStructMain()
        {
            if (!TestForCfgFileOpStructTestOM_OBJ_ID_T())
                return false;
            return true;
        }
        bool TestForCfgFileOpStructTestOM_OBJ_ID_T()
        {
            int intSizeOf = 4 + 30 * 4; // 8+240
            int intSizeOf2 = System.Runtime.InteropServices.Marshal.SizeOf(new OM_OBJ_ID_T());
            if (intSizeOf != intSizeOf2)
            {
                Console.WriteLine(String.Format("OM_OBJ_ID_T sizeof should be {0},but now is {1}"), intSizeOf, intSizeOf2);
                return false;
            }
            return true;
        }
        void test_string_Substring_use()
        {
            string a, b;
            a = "123456789";
            int pos = a.IndexOf('3');
            b = a.Substring(a.Length - 4);//6789
            b = a.Substring(0, 4);         //值为:1234 (起点，长度)
            b = a.Substring(3);            //值为:456789
            b = a.Substring(2, 4);         //值为:3456
        }
        public uint getIPAddr(string ipAddr)
        {
            System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(ipAddr);
            long dreamduip = ipaddress.Address;//转换为 90 03 a8 c0
            long x = dreamduip;
            long aaa = ((((x) & 0x000000ff) << 24) | (((x) & 0x0000ff00) << 8) | (((x) & 0x00ff0000) >> 8) | (((x) & 0xff000000) >> 24));
            return (uint)aaa;
        }
    }
}
