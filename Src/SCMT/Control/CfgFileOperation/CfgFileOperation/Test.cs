using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MIBDataParser.JSONDataMgr;
using CfgFileOpStruct;


namespace CfgFileOperation
{
    
    /// <summary>
    /// .cfg 文件相关操作的测试代码
    /// </summary>
    class Test
    {
        static void Main(string[] args)
        {
            Test test = new Test();

            test.TestBeyondCompareMain();
            //test.testForCreatePatchAndInit();

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

        Dictionary<string,uint> GetTableNamesDictByTablesPos(string filePath, List<uint> tablesPosL)
        {
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
            string dataBasePath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\";
            string YSFilePath = dataBasePath + "5GCfg\\init_qyx.cfg";
            string NewFilePath = dataBasePath + "init.cfg";

            //bool re = TestBeyondTableName(YsTableNames, NewTableNames);
            // 比较 表名是否一致
            if (!TestBeyondCompFileTableNameMain(YSFilePath, NewFilePath))
                Console.WriteLine("tables name not all same.");

            // 比较 每个表的内容
            if (!TestBeyondComFileTableInfoMain(YSFilePath, NewFilePath))
                Console.WriteLine("tables info not all same.");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="YSFilePath"></param>
        /// <param name="NewFilePath"></param>
        /// <returns></returns>
        bool TestBeyondComFileTableInfoMain(string YSFilePath, string NewFilePath)
        {
            bool re = true;
            StruDataHead YsDhead = GetDataHeadFromFile(YSFilePath);
            List<uint> YsTablePos = GetTablesPos(YSFilePath, (int)YsDhead.u32TableCnt);
            Dictionary<string, uint> YsTableNamePosDict = GetTableNamesDictByTablesPos(YSFilePath, YsTablePos);

            StruDataHead NewDhead = GetDataHeadFromFile(NewFilePath);
            List<uint> NewTablePos = GetTablesPos(NewFilePath, (int)NewDhead.u32TableCnt);
            Dictionary<string, uint> NewTableNamePosDict = GetTableNamesDictByTablesPos(NewFilePath, NewTablePos);


            // 每个表的内容比较
            StruCfgFileTblInfo ysTblInfo;
            StruCfgFileTblInfo newTblInfo;
            Dictionary<string, StruCfgFileFieldInfo> YsLeafHeadL;
            Dictionary<string, StruCfgFileFieldInfo> NewLeafHeadL;
            uint offsetYs;
            uint offsetNew;
            foreach (string table in YsTableNamePosDict.Keys)
            {
                ///1.StruCfgFileTblInfo:44 字节，表内容头;
                ///2.StruCfgFileFieldInfo[u16FieldNum]:60 字节* u16FieldNum，每个叶子的内容介绍;
                ///3.u16RecLen(Stru) * u32RecNum(个数):每个实例内容(大小为u16RecLen) * 实例数.
                if (String.Equals(table, "adjeNBEntry"))
                    Console.WriteLine("Debug");
                // 获取tableInfo表块介绍
                offsetYs = YsTableNamePosDict[table];
                ysTblInfo = TestGetTableHeadInfo(YSFilePath, offsetYs);

                offsetNew = NewTableNamePosDict[table];
                newTblInfo = TestGetTableHeadInfo(NewFilePath, offsetNew);

                // 1. 每个表的表头是否相同 : StruCfgFileTblInfo
                if (!TestIsSameTableHeadField(table, ysTblInfo, newTblInfo))
                {
                    Console.WriteLine(String.Format("tableName={0}, table head info not all same.",table));
                    re = false;
                }

                // 2. 每个表的叶子的头是否相同 : StruCfgFileFieldInfo[u16FieldNum];
                YsLeafHeadL = TestGetLeafHeadFieldInfo(YSFilePath, ysTblInfo.u16FieldNum, offsetYs);
                NewLeafHeadL = TestGetLeafHeadFieldInfo(NewFilePath, newTblInfo.u16FieldNum, offsetNew);
                if (!TestIsSameLeafHeadInfoList(YsLeafHeadL, NewLeafHeadL))
                {
                    Console.WriteLine(String.Format("tableName={0}, Leaf head info not all same.", table));
                    re = false;
                }

                // 3. 每个表的实例是否一致
                List<byte[]> YsInsts = GetInstsData(YSFilePath, offsetYs, (int)ysTblInfo.u32RecNum, (int)ysTblInfo.u16FieldNum, ysTblInfo.u16RecLen);
                List<byte[]> NewInsts = GetInstsData(NewFilePath, offsetNew, (int)newTblInfo.u32RecNum, (int)newTblInfo.u16FieldNum, newTblInfo.u16RecLen);
                if (!TestIsSameInstsList(table, (int)ysTblInfo.u32RecNum, YsLeafHeadL, YsInsts, NewInsts))
                {
                    Console.WriteLine(String.Format("tableName={0}, Leaf head info not all same.", table));
                    re = false;
                }
            }

            return re;
        }

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
                instOffset = u16FieldLen * i;
                byte[] YsInstsData = new CfgOp().CfgReadFile(filePath, instOffset, u16FieldLen);
                re.Add(YsInstsData);
            }
            return re;
        }

        bool TestIsSameInstsList(string tableName,int u32RecNum,  Dictionary<string, StruCfgFileFieldInfo> LeafHead, List<byte[]> InstsAList, List<byte[]> InstsBList)
        {
            bool re = true;
            byte[] instA;
            byte[] instB;
            for (int instNo=0; instNo< u32RecNum; instNo++)
            {
                instA = InstsAList[instNo];//实例
                instB = InstsBList[instNo];
                if (!TestIsSameInst(tableName, LeafHead, instA, instB))
                {
                    re = false;
                    Console.WriteLine(String.Format("table({0}), instNo({1}) info no same", tableName, instNo));
                    break;
                }
            }
            return re;
        }
        bool TestIsSameInst(string tableName, Dictionary<string, StruCfgFileFieldInfo> LeafHead, byte[] instA, byte[] instB)
        {
            bool re = true;
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
                string leafNameA = Encoding.GetEncoding("GB2312").GetString(InA).TrimEnd('\0');
                string leafNameB = Encoding.GetEncoding("GB2312").GetString(InB).TrimEnd('\0');
                if (!BytesCompare_Base64(InA, InB))
                {
                    re = false;
                    Console.WriteLine(String.Format("table({0}),leafName({1})(a.{2},b.{3}) no same", 
                        tableName, leafName, Encoding.ASCII.GetString(InA), Encoding.Default.GetString(InB)));
                    break;
                }
            }
            return re;
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
            //u16DataFmtVer = 0;
            //u8pad = new byte[2];
            //u8TblName = new byte[32];
            //u16FieldNum = 0;
            //u16RecLen = 0;
            //u32RecNum = 0;
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
        /// 
        /// </summary>
        /// <param name="YSFilePath"></param>
        /// <param name="NewFilePath"></param>
        /// <returns></returns>
        bool TestBeyondCompFileTableNameMain(string YSFilePath, string NewFilePath)
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
            string dataBasePath  = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\5GCfg\\";
            string dataMdbPath   = "lm.mdb";//1.数据库
            string antennaExPath = "LTE_基站天线广播波束权值参数配置表_5G.xls";//2.天线信息
            string alarmExPath   = "eNB告警信息表.xls";//3.告警信息
            string rruInfoExPath = "RRU基本信息表.xls";//4.RRU信息
            string reclistExPath = "RecList_V6.00.50.05.40.07.01.xls";//5.reclist
            string selfDefExPath = "自定义_初配数据文件_ENB_5G_00_00_05.xls";//6.自定义文件(init, patch)

            CfgOp cfgOp = new CfgOp();
            Dictionary<string, string> paths = new Dictionary<string, string>() {
                { "DataMdb", dataBasePath+dataMdbPath},
                { "Antenna", dataBasePath+antennaExPath},
                { "Alarm",   dataBasePath+alarmExPath},
                { "RruInfo" ,dataBasePath+rruInfoExPath},
                { "Reclist" ,dataBasePath+reclistExPath},
                { "SelfDef" ,dataBasePath+selfDefExPath},
            };
            cfgOp.OnCreatePatchAndInitCfg(paths);
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

            // reclist
            cfgOp.m_reclistExcel = new CfgParseReclistExcel();
            string excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\RecList_V6.00.50.05.40.07.01.xls";
            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
            string UeType = "0:默认";
            //reclist.ProcessingExcel(excelPath, strFileToDirectory, UeType, cfgOp);
            cfgOp.m_reclistExcel.ProcessingExcel(excelPath, strFileToDirectory, UeType, cfgOp);

            // 自定义文件
            excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\自定义_初配数据文件_ENB_5G_00_00_05.xls";
            //CfgParseSelfExcel selfEx = new CfgParseSelfExcel();
            //selfEx.ProcessingExcel(excelPath, strFileToDirectory, "init", cfgOp);
            //selfEx.ProcessingExcel(excelPath, strFileToDirectory, "patch", cfgOp);
            cfgOp.m_selfExcel = new CfgParseSelfExcel();
            cfgOp.m_selfExcel.ProcessingExcel(excelPath, strFileToDirectory, "init", cfgOp);
            cfgOp.m_selfExcel.ProcessingExcel(excelPath, strFileToDirectory, "patch", cfgOp);
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

            // reclist
            //CfgParseReclistExcel reclist = new CfgParseReclistExcel();
            string excelPath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\123\\RecList_V6.00.50.05.40.07.01.xls";
            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
            string UeType = "0:默认";
            cfgOp.m_reclistExcel.ProcessingExcel(excelPath, strFileToDirectory, UeType, cfgOp);
        }

        void testLoadMibTreeIntoMem()
        {
            string strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";
            CfgParseDBMibTreeToMemory mibTree = new CfgParseDBMibTreeToMemory();
            mibTree.ReadMibTreeToMemory(strFileToDirectory);
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
            dd.ProcessingAntennaExcel(excelPath, sheetName);
            List<Dictionary<string, string>> data = dd.GetBeamScanData();
        }

        void testForOpReadExcelForCfg()
        {
            CfgExcelOp exOp = new CfgExcelOp();
            exOp.test(".\\123\\eNB告警信息表.xls");

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
