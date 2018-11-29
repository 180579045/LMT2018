using CfgFileOpStruct;
using MIBDataParser.JSONDataMgr;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CfgFileOperation
{
    //
    class TestCfgForInitAndPatch
    {
        /// <summary>
        /// 校验 init.cfg 
        /// </summary>
        public void BeyondCompareInitCfgMain()
        {
            FileStream fs = new FileStream("BeyondCompareWriteBuf.txt", FileMode.Create);
            //实例化BinaryWriter
            BinaryWriter bw = new BinaryWriter(fs);

            string dataBasePath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\";
            string YSFilePath = dataBasePath + "5GCfg\\init_qyx.cfg";
            string NewFilePath = dataBasePath + "init.cfg";

            // 文件的头比较
            ByCpFileHeadInfo( YSFilePath, NewFilePath);


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
        /// 比较文件头的信息
        /// </summary>
        /// <param name="YSFilePath"></param>
        /// <param name="NewFilePath"></param>
        /// <returns></returns>
        bool ByCpFileHeadInfo(string YSFilePath, string NewFilePath)
        {
            bool re = true;
            StruCfgFileHeader YsFileHead = GetStruCfgFileHeaderInfo(YSFilePath);
            StruCfgFileHeader NewFileHead = GetStruCfgFileHeaderInfo(NewFilePath);


            return re;
        }
        /// <summary>
        /// 解析文件头
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        StruCfgFileHeader GetStruCfgFileHeaderInfo(string filePath)
        {
            int offset = 956;
            byte[] Ysdata = new CfgOp().CfgReadFile(filePath, 0, offset);
            StruCfgFileHeader YsFileHead = new StruCfgFileHeader("init");
            YsFileHead.ParseFileReadBytes(Ysdata);
            return YsFileHead;
        }
        bool IsSameStruCfgFileHeader(StruCfgFileHeader HeadA, StruCfgFileHeader HeadB)
        {
            bool re = true;
            // 只比较意义的字段

            //Stru    956字节 文件头结构体
            //-> u8VerifyStr  1 * 4:Byte[4][4]文件头的校验字段 "ICF"
            if (!BytesCompare_Base64(HeadA.Getu8VerifyStr(), HeadB.Getu8VerifyStr()))
            {
                re = false;
                return re;
            }
            // ->u8HiDeviceType   1 * 1:byte Nodeb基站类型（1 版 = 0、2版 = 2、3版超级基站 = 1）
            if (HeadA.Getu8HiDeviceType() != HeadB.Getu8HiDeviceType())
            {
                re = false;
                return re;
            }
            //-> u8MiDeviceType; 1 * 1:byte TD = 0 / LTE = 1 / 5G = 2的文件
            if (HeadA.Getu8MiDeviceType() != HeadB.Getu8MiDeviceType())
            {
                re = false;
                return re;
            }
            //->u16LoDevType; 2 * 1:ushort 计算大小端 700拆分成 0xbc和0x02组合
            if (HeadA.Getu16LoDevType() != HeadB.Getu16LoDevType())
            {
                re = false;
                return re;
            }
            //->u32IcFileVer 4 * 1:uint 初配文件版本：用来标志当前文件的大版本 = 1,2
            if (HeadA.Getu32IcFileVer() != HeadB.Getu32IcFileVer())
            {
                re = false;
                return re;
            }
            //->u32ReserveVer; 4 * 1:uint 初配文件小版本：用来区分相同大版本下的因取值不同造成的差异，现在这里是最小版本(版本号"5_65_3_6", 截取6)
            if (HeadA.u32ReserveVer != HeadB.u32ReserveVer)
            {
                re = false;
                return re;
            }
            //->u32DataBlk_Location; 4 * 1:uint 数据块起始位置 默认956
            if (HeadA.u32DataBlk_Location != HeadB.u32DataBlk_Location)
            {
                re = false;
                return re;
            }
            //->u8LastMotifyDate[20]; 1 * 20:byte[20]   [20] 文件最新修改的日期, 按字符串存放 
            if (!BytesCompare_Base64(HeadA.Getu8LastMotifyDate(), HeadB.Getu8LastMotifyDate()))
            {
                re = false;
                return re;
            }
            //-> u32IcFile_HeaderVer; 4 * 1:uint 初配文件头版本，用于记录不同的文件头格式、版本
            if (HeadA.u32IcFile_HeaderVer != HeadB.u32IcFile_HeaderVer)
            {
                re = false;
                return re;
            }
            //->u32PublicMibVer; 4 * 1:uint 公共Mib版本号(版本号"5_65_3_6", 截取5)
            if (HeadA.u32PublicMibVer != HeadB.u32PublicMibVer)
            {
                re = false;
                return re;
            }
            //-> u32MainMibVer; 4 * 1:uint Mib主版本号(版本号"5_65_3_6", 截取65)
            if (HeadA.u32MainMibVer != HeadB.u32MainMibVer)
            {
                re = false;
                return re;
            }
            //-> u32SubMibVer; 4 * 1:uint Mib辅助版本号(版本号"5_65_3_6", 截取3)
            if (HeadA.u32SubMibVer != HeadB.u32SubMibVer)
            {
                re = false;
                return re;
            }
            //-> u32IcFile_HeaderLength; 4 * 1:uint 初配文件头部长度 
            if (HeadA.u32IcFile_HeaderLength != HeadB.u32IcFile_HeaderLength)
            {
                re = false;
                return re;
            }
            //-> u8IcFileDesc[256]    1 * 256:byte[256][256] 文件描述 “初配文件”
            byte[] byArrayA = HeadA.Getu8IcFileDesc().Skip(0).Take(9).ToArray();
            byte[] byArrayB = HeadB.Getu8IcFileDesc().Skip(0).Take(9).ToArray();
            if (!BytesCompare_Base64(byArrayA, byArrayB))
            {
                re = false;
                return re;
            }
            //-> u32RevDatType; 4 * 1:uint 保留段数据类别 (1: 文件描述) 
            if (HeadA.u32RevDatType != HeadB.u32RevDatType)
            {
                re = false;
                return re;
            }
            //-> u32IcfFileType; 4 * 1:uint 初配文件类别（1:NB,2:RRS） 2005 - 12 - 22
            if (HeadA.u32IcfFileType != HeadB.u32IcfFileType)
            {
                re = false;
                return re;
            }
            //->u32IcfFileProperty; 4 * 1:uint 初配文件属性（0:正式文件; 1:补充文件）
            if (HeadA.u32IcfFileProperty != HeadB.u32IcfFileProperty)
            {
                re = false;
                return re;
            }
            //-> u32DevType; 4 * 1:uint 设备类型(1:超级基站; 2:紧凑型小基站)
            if (HeadA.u32DevType != HeadB.u32DevType)
            {
                re = false;
                return re;
            }
            //-> u16NeType    2 * 1:ushort 数据文件所属网元类型
            if (HeadA.u16NeType != HeadB.u16NeType)
            {
                re = false;
                return re;
            }
            //-> u8Pading[2]  1 * 2:byte[2][2]

            //-> s8DataFmtVer[12] 1 * 2:sbyte[12]  [12] 数据文件版本（与对应的MIB版本相同）  
            sbyte[] s8DataFmtVerA = HeadA.Gets8DataFmtVer();
            sbyte[] s8DataFmtVerB = HeadB.Gets8DataFmtVer();
            if (!SBytesCompare(s8DataFmtVerA, s8DataFmtVerB))
            {
                re = false;
                return re;
            }
            //-> u8TblNum 1 * 1:byte 数据文件中表的个数  
            if (HeadA.u8TblNum != HeadB.u8TblNum)
            {
                re = false;
                return re;
            }
            //-> u8FileType   1 * 1:byte 配置文件类别(1, init.cfg:cfg,或dfg,2 patch_ex.cfg:pdg,)
            if (HeadA.u8FileType != HeadB.u8FileType)
            {
                re = false;
                return re;
            }
            //-> u8Pad1   1 * 1:byte 保留 
            //-> u8ReserveAreaType    1 * 1:byte 保留空间的含义 = 0

            //  ->u32TblOffset[150]    4 * 150:uint[150][150] 每个表的数据在文件中的起始位置（相对文件头）  
            //-> Reserved[4]  1 * 4:byte[4][4] 保留字段
            return re;
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


        /// <summary>
        /// 比较 每个表的内容
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
                    Console.WriteLine(String.Format("tableName={0}, table head info not all same.", table));
                    re = false;
                    continue;
                }

                // 2. 每个表的叶子的头是否相同 : StruCfgFileFieldInfo[u16FieldNum];
                Dictionary<string, StruCfgFileFieldInfo> YsLeafHeadL = TestGetLeafHeadFieldInfo(YSFilePath, ysTblInfo.u16FieldNum, offsetYs);
                Dictionary<string, StruCfgFileFieldInfo> NewLeafHeadL = TestGetLeafHeadFieldInfo(NewFilePath, newTblInfo.u16FieldNum, offsetNew);
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
                if (!TestIsSameByIndexMain(bw, table, (int)ysTblInfo.u32RecNum, YsLeafHeadL, YsInsts, NewInsts))
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
        /// <summary>
        /// 根据偏移获取所有表名
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="tablesPosL"></param>
        /// <returns></returns>
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
        /// 比较两列表名是否相同(一一对应)
        /// </summary>
        /// <param name="tablesPosLA"></param>
        /// <param name="tablesPosLB"></param>
        /// <returns></returns>
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



        /// <summary>
        /// 获取每个表的偏移位置
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Dictionary<string, uint> GetTableNamesDictByTablesPos(string filePath)
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
                tableNamesDict.Add(tablName, pos);
            }
            bytedata = null;
            cfgOp = null;
            return tableNamesDict;
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
        /// <summary>
        /// 两个表头是否相同
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tblFieldA"></param>
        /// <param name="tblFieldB"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// 获取所有叶子节点的叶子节点头
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="u16FieldNumYs"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 相同的叶子(leafName)的叶子头是否相同
        /// </summary>
        /// <param name="leafHLA"></param>
        /// <param name="leafHLB"></param>
        /// <returns></returns>
        bool TestIsSameLeafHeadInfoList(Dictionary<string, StruCfgFileFieldInfo> leafHLA, Dictionary<string, StruCfgFileFieldInfo> leafHLB)
        {
            bool re = true;
            foreach (string leafName in leafHLA.Keys)
            {
                StruCfgFileFieldInfo leafHA = leafHLA[leafName];
                StruCfgFileFieldInfo leafHB = leafHLB[leafName];
                if (!TestIsSameLeafHeadInfo(leafName, leafHA, leafHB))
                {
                    re = false;
                    Console.WriteLine(String.Format("leafname={0}, leafHeadInfo not all same.", leafName));
                    break;
                }
            }
            return re;
        }
        /// <summary>
        /// 对比叶子节点的每个字段
        /// </summary>
        /// <param name="leafName"></param>
        /// <param name="leafHA"></param>
        /// <param name="leafHB"></param>
        /// <returns></returns>
        bool TestIsSameLeafHeadInfo(string leafName, StruCfgFileFieldInfo leafHA, StruCfgFileFieldInfo leafHB)
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
        List<StruCfgFileFieldInfo> TestGetIndexLeafInfo(string table, Dictionary<string, StruCfgFileFieldInfo> LeafHead)
        {
            List<StruCfgFileFieldInfo> indexLeafsInfo = new List<StruCfgFileFieldInfo>();
            List<string> indexLeafsName = new List<string>();
            string dataBasePath = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\5GCfg\\";
            string dataMdbPath = "lm.mdb";//1.数据库

            // 表内容
            string strSQL = String.Format(
                "select * from MibTree where MIBName='{0}' and DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine", table);
            DataSet TableMibdateSet = CfgGetRecordByAccessDb(dataBasePath + dataMdbPath, strSQL);
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
            DataSet MibLeafsDateSet = CfgGetRecordByAccessDb(dataBasePath + dataMdbPath, strSQL2);
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

            return indexLeafsInfo;
        }
        /// <summary>
        /// 读取数据库，获取数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sqlContent"></param>
        /// <returns></returns>
        public DataSet CfgGetRecordByAccessDb(string fileName, string sqlContent)
        {
            DataSet dateSet = new DataSet();
            AccessDBManager mdbData = new AccessDBManager(fileName);//fileName = "D:\\C#\\SCMT\\lm.mdb";
            try
            {
                mdbData.Open();
                dateSet = mdbData.GetDataSet(sqlContent);
                mdbData.Close();
            }
            finally
            {
                mdbData = null;
            }
            return dateSet;
        }
        
        /// <summary>
        /// 比较索引值是否相同,方式一：
        /// </summary>
        /// <param name="indexLeafsName"></param>
        /// <param name="u32RecNum"></param>
        /// <param name="LeafHead"></param>
        /// <param name="InstsListA"></param>
        /// <param name="InstsListB"></param>
        /// <returns></returns>
        bool TestIsSameIndexList(List<string> indexLeafsName, int u32RecNum, Dictionary<string, StruCfgFileFieldInfo> LeafHead, List<byte[]> InstsListA, List<byte[]> InstsListB)
        {
            List<string> indexListA = TestGetAllIndex(indexLeafsName, u32RecNum, LeafHead, InstsListA);
            List<string> indexListB = TestGetAllIndex(indexLeafsName, u32RecNum, LeafHead, InstsListB);
            return TestIsAllIndexSame(indexListA, indexListB);
        }
        /// <summary>
        /// 获取实例中索引值
        /// </summary>
        /// <param name="indexLeafsName"></param>
        /// <param name="u32RecNum"></param>
        /// <param name="LeafHead"></param>
        /// <param name="InstsList"></param>
        /// <returns></returns>
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
                for (int instNo = u32RecNum - 1; instNo >= 0; instNo--)
                //foreach (var inst in InstsList)                
                {
                    byte[] inst = InstsList[instNo];
                    if (BytesCompare(emtpyArray, inst))
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
                            if (BytesCompare(emtpyArray, inst))
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
        /// <summary>
        /// 字符串比较，方式一：
        /// </summary>
        /// <param name="aArray"></param>
        /// <param name="bArray"></param>
        /// <returns></returns>
        bool BytesCompare(byte[] aArray, byte[] bArray)//bool TestIsByteListSame(byte[] aArray, byte[] bArray)
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
        bool SBytesCompare(sbyte[] saArray, sbyte[] sbArray)//bool TestIsByteListSame(byte[] aArray, byte[] bArray)
        {
            bool re = true;
            for (int i = 0; i < saArray.Length; i++)
            {
                if (saArray[i] != sbArray[i])
                {
                    re = false;
                    break;
                }
            }
            return re;
        }
        /// <summary>
        /// 字符串比较，方式二：
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        bool BytesCompare_Base64(byte[] aArray, byte[] bArray)
        {
            if (aArray == null || bArray == null) return false;
            if (aArray.Length != bArray.Length) return false;
            return string.Compare(Convert.ToBase64String(aArray), Convert.ToBase64String(bArray), false) == 0 ? true : false;
        }
        /// <summary>
        /// 字符串转 uint 翻转
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        uint GetBytesValToUint(byte[] bytes)
        {
            Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToUInt32(OxbytesToString(bytes), 16);
        }
        /// <summary>
        /// 字符串转 uint 不翻转
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        uint GetBytesValToUint2(byte[] bytes)
        {
            //Array.Reverse((byte[])bytes);
            string reStr = OxbytesToString(bytes);
            return Convert.ToUInt32(OxbytesToString(bytes), 16);
        }
        /// <summary>
        /// 字符串安找0x格式逐个转换变成字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 对比列表中字符串是否相同
        /// </summary>
        /// <param name="indexListA"></param>
        /// <param name="indexListB"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 比较索引值是否相同,方式二：
        /// </summary>
        /// <param name="indexLeafsInfo"></param>
        /// <param name="u32RecNum"></param>
        /// <param name="InstsListA"></param>
        /// <param name="InstsListB"></param>
        /// <returns></returns>
        bool TestIsSameIndexList2(List<StruCfgFileFieldInfo> indexLeafsInfo, int u32RecNum, List<byte[]> InstsListA, List<byte[]> InstsListB)
        {
            // Distinct().ToList(),去重，解决bug:例如，indexListA 索引值都是相同的，且这个相同值存在indexListB中，检测不到。
            List<string> indexListA = TestGetAllIndex2(indexLeafsInfo, u32RecNum, InstsListA).Distinct().ToList();
            List<string> indexListB = TestGetAllIndex2(indexLeafsInfo, u32RecNum, InstsListB).Distinct().ToList();
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
        /// <summary>
        /// 获取实例中索引值, 方式二：
        /// </summary>
        /// <param name="indexLeafsInfo"></param>
        /// <param name="u32RecNum"></param>
        /// <param name="InstsList"></param>
        /// <returns></returns>
        List<string> TestGetAllIndex2(List<StruCfgFileFieldInfo> indexLeafsInfo, int u32RecNum, List<byte[]> InstsList)
        {
            //Dictionary<string, byte[]> indexInsts = new Dictionary<string, byte[]>();
            List<string> index = new List<string>();
            StruCfgFileFieldInfo FieldH;
            ushort u16FieldOffset;       /* 字段相对记录头偏移量*/
            ushort u16FieldLen;          /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
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
                    if (BytesCompare(emtpyArray, inst))
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
        /// 依索引值为key，重新排列实例；方式二；
        /// </summary>
        /// <param name="indexLeafsInfo"></param>
        /// <param name="u32RecNum"></param>
        /// <param name="LeafHead"></param>
        /// <param name="InstsList"></param>
        /// <returns></returns>
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
                        if (BytesCompare(emtpyArray, inst))
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
        /// 无索引的表实例比较
        /// </summary>
        /// <param name="bw"></param>
        /// <param name="tableName"></param>
        /// <param name="u32RecNum"></param>
        /// <param name="LeafHead"></param>
        /// <param name="InstsAList"></param>
        /// <param name="InstsBList"></param>
        /// <returns></returns>
        bool TestIsSameInstsList(BinaryWriter bw, string tableName, int u32RecNum, Dictionary<string, StruCfgFileFieldInfo> LeafHead, List<byte[]> InstsAList, List<byte[]> InstsBList)
        {
            bool re = true;
            byte[] instA;
            byte[] instB;
            for (int instNo = 0; instNo < u32RecNum; instNo++)
            {
                instA = InstsAList[instNo];//实例
                instB = InstsBList[instNo];
                if (!TestIsSameInst(instNo.ToString(), bw, tableName, LeafHead, instA, instB))
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
        /// 比较两个实例是否相同，不同时打印。
        /// </summary>
        /// <param name="strIndex"></param>
        /// <param name="bw"></param>
        /// <param name="tableName"></param>
        /// <param name="LeafHead"></param>
        /// <param name="instA"></param>
        /// <param name="instB"></param>
        /// <returns></returns>
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
        /// 定位打印，打印不同的实例的所有叶子内容
        /// </summary>
        /// <param name="strIndex"></param>
        /// <param name="bw"></param>
        /// <param name="tableName"></param>
        /// <param name="LeafHead"></param>
        /// <param name="instA"></param>
        /// <param name="instB"></param>
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


    }
}
