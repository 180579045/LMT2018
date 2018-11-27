using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 引用其他模块内容
using MIBDataParser.JSONDataMgr; // 操作数据库，获取lm.dtz中内容
using System.IO;
using System.Runtime.InteropServices;
using System.Data;
using CfgFileOpStruct;
using System.Runtime.Serialization;

namespace CfgFileOperation
{
    /// <summary>
    /// .cfg 文件所有的相关操作，对外的接口
    /// </summary>
    class CfgOp
    {
        public CfgParseDBMibTreeToMemory mibTreeLineMen = null;

        public CfgParseAlarmExecl m_alarmExcel = null;
        public CfgParseAntennaExcel m_antennaExcel = null;
        public CfgParseReclistExcel m_reclistExcel = null;
        public CfgParseRruExcel m_rruExcel = null;
        public CfgParseSelfExcel m_selfExcel = null;
        //mibTreeMem = new CfgParseDBMibTreeToMemory();
        public CfgParseDBMibTreeToMemory m_mibTreeMem = null;

        /// <summary>
        /// key:tableName; value : ClassCfgTableOp>
        /// </summary>
        public Dictionary<string, CfgTableOp> m_mapTableInfo; // 存每个表的内容
        /// <summary>
        /// 
        /// </summary>
        StruCfgFileHeader m_cfgFile_Header;            // init Cfg文件头结构
        StruCfgFileHeader m_eNB_pdgFile_Header;        // path Cfg文件头结构
        /// <summary>
        /// 
        /// </summary>
        StruDataHead m_dataHeadInfo;                   // init 数据块的头 ，为将来堆叠准备
        StruDataHead m_dataheadInfo_PDG;               // path 
        /// <summary>
        /// 
        /// </summary>
        bool m_bEmptyCfg ;                             // 是否是空cfg
        /// 
        /// </summary>
        public CfgOp()
        {
            m_mapTableInfo = new Dictionary<string, CfgTableOp>();
            //m_dataHeadInfo = new StruDataHead("init");
            m_bEmptyCfg = true;// 初始化为空cfg
        }
        /// <summary>
        /// 创建 init.cfg path_ex.cfg
        /// </summary>
        public void OnCreatePatchAndInitCfg(Dictionary<string, string> paths)
        {
            //public-1. RRU信息
            m_rruExcel = new CfgParseRruExcel();
            m_rruExcel.ProcessingExcel(paths["RruInfo"], "RRU基本信息表");

            //public-2. 告警信息
            //在CreateCfgFile中就解析了
            m_alarmExcel = new CfgParseAlarmExecl();
            m_alarmExcel.ParseExcel(paths["Alarm"]);

            //public-3. 天线信息
            m_antennaExcel = new CfgParseAntennaExcel();
            //m_antennaExcel.ProcessingAntennaExcel(paths["Antenna"], "波束扫描原始值");

            //public-4. lm.mdb 更新加载数据，整理成表和表实例的结构
            CreateCfgFile(paths);

            //init-1. 自定义 (init)
            m_selfExcel = new CfgParseSelfExcel();
            m_selfExcel.ProcessingExcel(paths["SelfDef"], paths["DataMdb"], "init", this);

            //init-2. 生成 init.cfg 文件
            SaveFile_eNB("init.cfg");

            //patch-1. lm.mdb 以每行为单位加载, reclist使用 
            //m_mibTreeMem = new CfgParseDBMibTreeToMemory();
            //m_mibTreeMem.ReadMibTreeToMemory(paths["DataMdb"]);
            //patch-2. reclist 
            //m_reclistExcel = new CfgParseReclistExcel();
            //m_reclistExcel.ProcessingExcel(paths["Reclist"], paths["DataMdb"], "0:默认", this);
            //patch-3. 自定义 (patch)
            //m_selfExcel.ProcessingExcel(paths["SelfDef"], paths["DataMdb"], "patch", this);

            //7. 开始生成 init.cfg, patch_ex.cfg
            //patch-4. 创建patch_ex.cfg
            //CreateFilePdg_eNB("patch_ex.cfg", paths["DataMdb"]);
            //SaveFilePdg_eNB("patch_ex.cfg");
        }
        void CreatCfg_public(Dictionary<string, string> paths)
        {
            //public-1. RRU信息
            m_rruExcel = new CfgParseRruExcel();
            m_rruExcel.ProcessingExcel(paths["RruInfo"], "RRU基本信息表");

            //public-2. 告警信息
            //在CreateCfgFile中就解析了
            m_alarmExcel = new CfgParseAlarmExecl();
            m_alarmExcel.ParseExcel(paths["Alarm"]);

            //public-3. 天线信息
            m_antennaExcel = new CfgParseAntennaExcel();
            //m_antennaExcel.ProcessingAntennaExcel(paths["Antenna"], "波束扫描原始值");

            //public-4. lm.mdb 更新加载数据，整理成表和表实例的结构
            CreateCfgFile(paths);
        }
        void CreatCfg_init_cfg(Dictionary<string, string> paths)
        {
            //init-1. 自定义 (init)
            m_selfExcel = new CfgParseSelfExcel();
            m_selfExcel.ProcessingExcel(paths["SelfDef"], paths["DataMdb"], "init", this);

            //init-2. 生成 init.cfg 文件
            SaveFile_eNB("init.cfg");
        }
        void CreatCfg_patch_ex_cfg()
        {

        }

        /// <summary>
        /// lm.mdb 更新加载数据，整理成表和表实例的结构
        /// </summary>
        /// <param name="strCfgFileName">创建cfg的文件名字</param>
        /// <param name="FileToDirectory">解压释放的地方</param>
        /// <param name="strDBPath"></param>
        /// <param name="strDBName"></param>
        public void CreateCfgFile(Dictionary<string, string> paths)//string strCfgFileName, string FileToDirectory, string strDBPath, string strDBName)
        {
            // string FileToUnzip = currentPath + "\\Data\\lmdtz\\lm.dtz";//
            // string FileToDirectory = currentPath + "\\Data\\lmdtz";
            //string strFileToDirectory = strDBName.Substring(0, strDBName.Length - strDBName.IndexOf("lm")+1);

            WriteHeaderVersionInfo(paths["DataMdb"]);
            CreateCfgFileBody(paths);
        }
        /// <summary>
        /// 创建init.cfg
        /// </summary>
        /// <param name="newFilePath"></param>
        /// <returns></returns>
        public bool SaveFile_eNB(string newFilePath)
        {
            List<byte> allBuff = new List<byte>();
            if (String.Empty == newFilePath)
                return false;
            //allBuff.AddRange(m_cfgFile_Header.StruToByteArrayReverse());// 写入头文件
            allBuff.AddRange(m_cfgFile_Header.StruToByteArray());         // 写入头文件
            //allBuff.AddRange(m_dataHeadInfo.StruToByteArrayReverse());  // 数据块的头
            allBuff.AddRange(m_dataHeadInfo.StruToByteArray());           // 数据块的头
            foreach (var table in m_mapTableInfo.Values)                  // 设置节点偏移量  
            {
                byte[] tableOffset = BitConverter.GetBytes((uint)table.GetTableOffset());
                //Array.Reverse(tableOffset);
                allBuff.AddRange(tableOffset);
            }

            /// 2.表实例
            foreach (var table in m_mapTableInfo.Values)//写入节点信息
            {
                string strTableName = table.m_strTableName;
                //if (strTableName == "rruTypePortEntry")
                //    Console.WriteLine("1111");
                allBuff.AddRange(table.WriteTofile());
            }

            CfgWriteFile(newFilePath, allBuff.ToArray(), 0);
            return true;
        }
        /// <summary>
        /// 创建 patch_ex.cfg 文件的相关内容;
        /// 输入参数: mapInst2LeafName:保存了生成Pdg文件的表，叶子，以及记录信息.
        /// </summary>
        /// <param name="newFilePath"></param>
        /// <returns></returns>
        public bool CreateFilePdg_eNB(string newFilePath, string strDBPath)
        {
            // 文件头
            WriteHeaderVersionInfoPDG(strDBPath);//Pdg文件头结构
            // 数据块头
            int m_tableNum = m_reclistExcel.GetVectPDGTabNameNum();
            WriteDataHeadInfoPDG((uint)m_tableNum);//Pdg数据块头结构
            // 偏移头(1.固定偏移;2.表实例偏移)
            // 1.固定偏移
            uint iFixedHeadOffset = 0;//固定头偏移 Fixed Head
            iFixedHeadOffset += (uint)Marshal.SizeOf(new StruCfgFileHeader(""));      //文件头：956 字节，
            iFixedHeadOffset += (ushort)Marshal.SizeOf(new StruDataHead("init"));     //数据头：24  字节， sizeof(DataHead);
            iFixedHeadOffset += (uint)m_tableNum * (uint)Marshal.SizeOf(new UInt32());//偏移头：4 * 表个数 字节，每个表的偏移量 
            // 2.表实例偏移
            foreach (var tabName in m_reclistExcel.GetVectPDGTabName())
            {
                if (!m_mapTableInfo.ContainsKey(tabName))
                    continue;
                iFixedHeadOffset = SetTableOffset_PDG(m_mapTableInfo[tabName], iFixedHeadOffset);//计算表的偏移量
            }
            return true;
        }
        /// <summary>
        /// 写 patch_ex 文件
        /// </summary>
        /// <param name="newFilePath"></param>
        public bool SaveFilePdg_eNB(string newFilePath)
        {
            if (String.Empty == newFilePath)
                return false;

            // 申请内容
            List<byte> allBuff = new List<byte>();

            // 文件头   : 序列化
            allBuff.AddRange(m_eNB_pdgFile_Header.StruToByteArrayReverse());// 写入头文件

            // 数据块头 : 序列化
            allBuff.AddRange(m_dataheadInfo_PDG.StruToByteArrayReverse());  // 数据块的头

            // 偏移块头 : 序列化
            foreach (var tabName in m_reclistExcel.GetVectPDGTabName())
            {
                if (!m_mapTableInfo.ContainsKey(tabName))
                    continue;
                byte[] tableOffset = BitConverter.GetBytes((uint)m_mapTableInfo[tabName].GetTableOffsetPDG());
                Array.Reverse(tableOffset);
                allBuff.AddRange(tableOffset);
            }

            //  表块 实例 : 序列化
            foreach (var tabName in m_reclistExcel.GetVectPDGTabName())
            {
                if (!m_mapTableInfo.ContainsKey(tabName))
                    continue;
                CfgTableOp tableOp = m_mapTableInfo[tabName];
                allBuff.AddRange(tableOp.WriteTofilePDG());
            }
            CfgWriteFile(newFilePath, allBuff.ToArray(), 0);
            return true;
        }
        /// <summary>
        /// 文件头
        /// </summary>
        /// <param name="strDBName"></param>
        private void WriteHeaderVersionInfo(string strDBName)//CAdoConnection* pAdoCon,const CString &strFileDesc)
        {
            string strSQL = "select * from SystemParameter where SysParameter = 'MibPublicVersion'";
            DataSet MibdateSet = CfgGetRecordByAccessDb(strDBName, strSQL);
            DataRow row = MibdateSet.Tables[0].Rows[0];
            string strMibVersion = row.ItemArray[1].ToString();//row["MibPublicVersion"].ToString();
            m_cfgFile_Header = new StruCfgFileHeader("init");
            m_cfgFile_Header.Setu8VerifyStr("ICF");
            m_cfgFile_Header.Setu8HiDeviceType(new MacroDefinition().NB_DEVICE);// 暂时
            m_cfgFile_Header.Setu8MiDeviceType(new MacroDefinition().LTE_DEVICE);// = LTE_DEVICE;
            m_cfgFile_Header.Setu16LoDevType();
            m_cfgFile_Header.Setu32IcFileVer("cfg_Version_2");
            m_cfgFile_Header.FillVerInfo(strMibVersion);//4 "5_65_3_6"
            m_cfgFile_Header.u32DataBlk_Location = (uint)Marshal.SizeOf(new StruCfgFileHeader());//956
            m_cfgFile_Header.Setu8LastMotifyDate(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            m_cfgFile_Header.u32IcFile_HeaderVer = new MacroDefinition().LTE_ICF_HEADER;
            m_cfgFile_Header.u32IcFile_HeaderLength = (uint)System.Runtime.InteropServices.Marshal.SizeOf(new StruCfgFileHeader());// new StruCfgFileHeader());
            m_cfgFile_Header.Setu8IcFileDesc("初配文件");
            m_cfgFile_Header.u16NeType = new MacroDefinition().NETTYPE;
            m_cfgFile_Header.Sets8DataFmtVer("ICF");
            m_cfgFile_Header.u8FileType = new MacroDefinition().OM_ENB_CFGORPDF_FILE;
            m_bEmptyCfg = false;
            return;
        }
        /// <summary>
        /// 文件头, patch
        /// </summary>
        /// <param name="strDBName"></param>
        private void WriteHeaderVersionInfoPDG(string strDBName)//CAdoConnection* pAdoCon,const CString &strFileDesc)
        {
            string strSQL = "select * from SystemParameter where SysParameter = 'MibPublicVersion'";
            DataSet MibdateSet = CfgGetRecordByAccessDb(strDBName, strSQL);
            DataRow row = MibdateSet.Tables[0].Rows[0];
            string strMibVersion = row.ItemArray[1].ToString();//row["MibPublicVersion"].ToString();
            m_eNB_pdgFile_Header = new StruCfgFileHeader("init");
            m_eNB_pdgFile_Header.Setu8VerifyStr("ICF");
            m_eNB_pdgFile_Header.Setu8HiDeviceType(new MacroDefinition().NB_DEVICE);// 暂时
            m_eNB_pdgFile_Header.Setu8MiDeviceType(new MacroDefinition().LTE_DEVICE);// = LTE_DEVICE;
            m_eNB_pdgFile_Header.Setu16LoDevType();
            m_eNB_pdgFile_Header.Setu32IcFileVer("cfg_Version_2");
            m_eNB_pdgFile_Header.FillVerInfo(strMibVersion);//4 "5_65_3_6"
            m_eNB_pdgFile_Header.u32DataBlk_Location = (uint)Marshal.SizeOf(new StruCfgFileHeader());//956
            m_eNB_pdgFile_Header.Setu8LastMotifyDate(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            m_eNB_pdgFile_Header.u32IcFile_HeaderVer = new MacroDefinition().LTE_ICF_HEADER;
            m_eNB_pdgFile_Header.u32IcFile_HeaderLength = (uint)System.Runtime.InteropServices.Marshal.SizeOf(new StruCfgFileHeader());// new StruCfgFileHeader());
            m_eNB_pdgFile_Header.Setu8IcFileDesc("初配文件");
            m_eNB_pdgFile_Header.u16NeType = new MacroDefinition().NETTYPE;
            m_eNB_pdgFile_Header.Sets8DataFmtVer("ICF");
            m_eNB_pdgFile_Header.u8FileType = new MacroDefinition().OM_ENB_PDG_FILE;
            //m_bEmptyCfg = false;
            return;
        }
        /// <summary>
        /// 文件体
        /// </summary>
        /// <param name="strFileToDirectory"></param>
        private void CreateCfgFileBody(Dictionary<string, string> paths)//string strFileToDirectory)//string strDBName)
        {
            /// sql 获取所有的 table 和 entry 
            string strSQL = ("select * from MibTree where DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine");
            DataSet MibdateSet = CfgGetRecordByAccessDb(paths["DataMdb"], strSQL);
            uint TableOffset = 0 ;// //设置表的偏移量
            for (int loop = 0; loop <= MibdateSet.Tables[0].Rows.Count - 1; loop++)//在表之间循环
            {
                TableOffset = CreatCfgFile_tabInfo(MibdateSet.Tables[0].Rows[loop], null, paths, TableOffset);
            }

            uint m_tableNum = (uint)m_mapTableInfo.Count;
            foreach (var table in m_mapTableInfo.Values)
            {
                uint iTableOffset = table.GetTableOffset();                           //表内容：所有表、叶子、实例的占位
                iTableOffset += (uint)Marshal.SizeOf(new StruCfgFileHeader(""));      //文件头：956 字节，
                iTableOffset += (ushort)Marshal.SizeOf(new StruDataHead("init"));     //数据头：24  字节， sizeof(DataHead);
                iTableOffset += (uint)m_tableNum * (uint)Marshal.SizeOf(iTableOffset);//偏移头：4 * 表个数 字节，每个表的偏移量 
                table.SetTableOffset(iTableOffset);
            }
            WriteDataHeadInfo((uint)m_tableNum);
        }
        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="strFileToUnzip"></param>
        /// <param name="strFileToDirectory"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        private bool CfgUnzipDtz(string strFileToUnzip, string strFileToDirectory, out string err)
        {
            err = "";
            ZipOper zipOp = new ZipOper();
            if (!zipOp.isFileExist(strFileToUnzip, out err))
            {
                return false;
            }
            string dealFileMid = "";
            if (!strFileToDirectory.EndsWith("\\"))
            {
                dealFileMid = "\\";
            }
            List<string> dealFileL = new List<string>() {
                strFileToDirectory + dealFileMid + "lm",
                strFileToDirectory + dealFileMid + "lm.mdb",
            };
            zipOp.delFile(dealFileL, out err);
            if (!zipOp.decompressedFile(strFileToUnzip, strFileToDirectory, out err))
            {
                return false;
            }
            if (!zipOp.moveFile(dealFileL[0], dealFileL[1], out err))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 根据 sql 获取数据库信息
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
        /// 数据块的头
        /// </summary>
        /// <param name="uintTableNum"></param>
        private void WriteDataHeadInfo(uint uintTableNum)
        {
            m_dataHeadInfo = new StruDataHead("init");
            m_dataHeadInfo.u32DatType = 1;
            m_dataHeadInfo.u32DatVer = 1;
            m_dataHeadInfo.Setu8VerifyStr("BEG\0");
            m_dataHeadInfo.u32TableCnt = uintTableNum;
            return;
        }
        private void WriteDataHeadInfoPDG(uint uintTableNum)
        {
            m_dataheadInfo_PDG = new StruDataHead("init");
            m_dataheadInfo_PDG.u32DatType = 1;
            m_dataheadInfo_PDG.u32DatVer = 1;
            m_dataheadInfo_PDG.Setu8VerifyStr("BEG\0");
            m_dataheadInfo_PDG.u32TableCnt = uintTableNum;
            return;
        }
        /// <summary>
        /// 以表为单位解析
        /// </summary>
        /// <param name="row"></param>
        /// <param name="tableOp"></param>
        /// <param name="strFileToDirectory"></param>
        /// <param name="TableOffset"></param>
        /// <returns></returns>
        private uint CreatCfgFile_tabInfo(DataRow row, CfgTableOp tableOp, Dictionary<string, string> paths, uint TableOffset)//string strFileToDirectory, DataSet MibdateSet)
        {
            string strTableName = row["MIBName"].ToString();
            if (strTableName == "rruTypePortEntry")
                Console.WriteLine("1111");
            //else if (strTableName == "nrCellCfgEntry")
            //    Console.WriteLine("1111");
            //else if (strTableName == "mdtConfigManagement")
            //    Console.WriteLine("1111");
            string strTableContent = row["TableContent"].ToString();// 设置动态表的容量
            bool isDyTable = isDynamicTable(strTableContent);       // 是否为动态表
            //if (df.Tables[0].Rows.Count > 0)
            string strSQL = String.Format("select * from mibtree where ParentOID ='{0}' and IsLeaf <> 0 and ICFWriteAble <> '×' order by ExcelLine", row["OID"].ToString());
            DataSet MibdateSet = CfgGetRecordByAccessDb(paths["DataMdb"], strSQL);
            int childcount = MibdateSet.Tables[0].Rows.Count;
            if (childcount > 0)
            {
                tableOp = new CfgTableOp();
                CreatCfgFile_leafsInfo( MibdateSet, tableOp);                                 // 计算 buflen
                if (isSpecialTable(strTableName))                                               // 是否是告警,是否为RRUType表和RRUTypePort和antennaArrayTypeTable表
                    CreateSpecialTalbe(row, tableOp, MibdateSet, paths);                      //告警信息从告警表获取
                else
                    RecordInstanceMain(tableOp, isDyTable, strTableContent);                  //实例信息
                tableOp.m_cfgFile_TblInfo.u16DataFmtVer = 0;                                  // = USDATAFMTVER;  MIB确定下来后会改变
                tableOp.m_cfgFile_TblInfo.Setu8TblName(strTableName);
                tableOp.m_cfgFile_TblInfo.u16FieldNum = (ushort)childcount;
                tableOp.m_cfgFile_TblInfo.u16RecLen = (ushort)tableOp.GetAllLeafsFieldLens(); // ???? buflen = leafs_info * num  
                tableOp.m_cfgFile_TblInfo.u32RecNum = 0;                                      //创建一个空的初配文件，无任何实例
                tableOp.SetTableOffset(TableOffset);                                          //设置表的偏移量 pCfgTableOp->SetTableOffset(TableOffset);
                tableOp.SetMibInit(true);                                                    
                tableOp.SetTabOID(row["OID"].ToString());
                tableOp.SetChFriendName(row["ChFriendName"].ToString());
                tableOp.SetDytabCont(strTableContent);                                        //设置动态表的容量
                tableOp.SetTabName(strTableName);                                             //设置表名 add by yangyuming
                TableOffset = SetTableOffset_2(tableOp, isDyTable, TableOffset);              //计算表的偏移量
                m_mapTableInfo.Add(strTableName, tableOp);
            }
            return TableOffset;
        }
        /// <summary>
        /// 计算表的偏移量
        /// </summary>
        /// <param name="tableOp"></param>
        /// <param name="isDyTable"></param>
        /// <param name="TableOffset"></param>
        /// <returns></returns>
        uint SetTableOffset( CfgTableOp tableOp, bool isDyTable, uint TableOffset)
        {
            uint TotalRecorNum = 1;
            if (m_bEmptyCfg == false)
            {
                if (true == isDyTable)
                {
                    uint m_cfgInsts_num = tableOp.get_m_cfgInsts_num();
                    uint DytabCont = tableOp.GetDytabCont();//动态表容量
                    if (m_cfgInsts_num != DytabCont)
                    {
                        TotalRecorNum = m_cfgInsts_num > DytabCont ? DytabCont : m_cfgInsts_num;//动态表容量
                        Console.WriteLine(String.Format("Err : {0} 实例计算错误 m_cfgInsts num is {1}, 表头(动态表容量) Num is {2}。。。。。。", tableOp.m_strTableName, m_cfgInsts_num, DytabCont));
                    }
                    else
                        TotalRecorNum = DytabCont;
                    tableOp.SetDyTable(isDyTable);
                }
                else
                    TotalRecorNum *= tableOp.m_struIndex.GetIndexRecorNum();
                tableOp.m_cfgFile_TblInfo.u32RecNum = TotalRecorNum;
                TableOffset += (uint)(TotalRecorNum * tableOp.GetAllLeafsFieldLens());//字段总长;
            }
            TableOffset += (uint)Marshal.SizeOf(new StruCfgFileTblInfo("init"));
            TableOffset += (uint)((uint)Marshal.SizeOf(new StruCfgFileFieldInfo()) * (uint)tableOp.m_LeafNodes.Count);
            return TableOffset;
        }
        uint SetTableOffset_2(CfgTableOp tableOp, bool isDyTable, uint TableOffset)
        {
            uint TotalRecorNum = 1;
            if (m_bEmptyCfg == false)
            {
                if (true == isDyTable)
                {
                    uint DytabCont = tableOp.GetDytabCont();//动态表容量
                    TotalRecorNum = DytabCont;

                    uint m_cfgInsts_num = tableOp.get_m_cfgInsts_num();
                    //uint DytabCont = tableOp.GetDytabCont();//动态表容量
                    if (m_cfgInsts_num != DytabCont)
                    {
                        //TotalRecorNum = m_cfgInsts_num > DytabCont ? DytabCont : m_cfgInsts_num;//动态表容量
                        Console.WriteLine(String.Format("Err : {0} 实例计算错误 m_cfgInsts num is {1}, 表头(动态表容量) Num is {2}。。。。。。", tableOp.m_strTableName, m_cfgInsts_num, DytabCont));
                    }
                    //else
                    //    TotalRecorNum = DytabCont;
                    tableOp.SetDyTable(isDyTable);
                }
                else
                    TotalRecorNum *= tableOp.m_struIndex.GetIndexRecorNum();
                tableOp.m_cfgFile_TblInfo.u32RecNum = TotalRecorNum;
                TableOffset += (uint)(TotalRecorNum * tableOp.GetAllLeafsFieldLens());//字段总长;
            }
            TableOffset += (uint)Marshal.SizeOf(new StruCfgFileTblInfo("init"));
            TableOffset += (uint)((uint)Marshal.SizeOf(new StruCfgFileFieldInfo()) * (uint)tableOp.m_LeafNodes.Count);
            return TableOffset;
        }
        /// <summary>
        /// 计算表的偏移位置 patch
        /// </summary>
        /// <param name="tableOp"></param>
        /// <param name="isDyTable"></param>
        /// <param name="TableOffset"></param>
        /// <returns></returns>
        uint SetTableOffset_PDG(CfgTableOp tableOp, uint TableOffset)
        {
            // 表的起始位置：是上一个表的结束位置
            tableOp.SetTableOffsetPDG(TableOffset);
            // 表块：包含3部分
            // 1. 表头
            TableOffset += (uint)Marshal.SizeOf(new StruCfgFileTblInfo("init"));
            // 2. 所有叶子节点的头
            TableOffset += (uint)((uint)Marshal.SizeOf(new StruCfgFileFieldInfo()) * (uint)tableOp.m_LeafNodes.Count);
            // 3. 实例内容 = 每个实例(所有叶子节点信息的长度) * 实例个数
            uint buflen = (uint)tableOp.m_cfgFile_TblInfo.u16RecLen;// 单个记录的有效长度（单位：字节）
            uint InstNum = (uint)tableOp.m_InstIndex2LeafName.Count;
            TableOffset += buflen * InstNum;
            return TableOffset;
        }
        /// <summary>
        /// 以表下面的叶子节点为单位解析
        /// </summary>
        /// <param name="MibLeafsDateSet"></param>
        /// <param name="tableOp"></param>
        void CreatCfgFile_leafsInfo(DataSet MibLeafsDateSet, CfgTableOp tableOp)//CDtCfgTableOp* pCfgTableOp, CAdoRecordSet* recordset, struIndexInfoCFG* pIndexInfo,OM_STRU_VALUE* pLeafValue, int leafNum, int &tableIndexNum, int &buflen)
        {
            if (MibLeafsDateSet == null || tableOp == null)// || pIndexInfo == NULL)
                return;
            //{
            //    //Database.GetInstance().MibParseDataSet(df);
            ushort buflen = 0;
            int tableIndexNum = 0;// 索引个数
            for (int loop = 0; loop < MibLeafsDateSet.Tables[0].Rows.Count; loop++)//在表之间循环
            {
                DataRow leafRow = MibLeafsDateSet.Tables[0].Rows[loop];
                if ((bool)leafRow["IsIndex"])//是否为索引??????? col=39
                {
                    tableIndexNum++;
                    tableOp.m_struIndex.Add(leafRow);
                }
                CfgFileLeafNodeOp leafNodeOp = new CfgFileLeafNodeOp(leafRow, buflen);
                tableOp.m_LeafNodes.Add(leafNodeOp);
                buflen += leafNodeOp.m_struFieldInfo.u16FieldLen;// 如果将来要改实例中某个节点的值，直接用这个字段
            }
            tableOp.m_tabDimen = tableIndexNum;
            return ;
        }
        /// <summary>
        /// 解析节点的属性
        /// </summary>
        /// <param name="OmType"></param>
        /// <param name="valList"></param>
        /// <param name="strMibSyntax"></param>
        /// <param name="LeaftypeSizeL"></param>
        /// <param name="recordTypeL"></param>
        void GetTypeLen(string OmType, string valList,string strMibSyntax, List<int> LeaftypeSizeL, List<byte> recordTypeL)
        {
            Dictionary<string, byte> Recordtype = new Dictionary<string, byte>(){
                {"s32[]", (byte)MacroDefinition.DATATYPE.OM_OID_VALUE },
                {"u32", (byte)MacroDefinition.DATATYPE.OM_U32_VALUE },
                {"s32", (byte)MacroDefinition.DATATYPE.OM_S32_VALUE },
                {"u8", (byte)MacroDefinition.DATATYPE.OM_U8_VALUE },
                {"s8", (byte)MacroDefinition.DATATYPE.OM_S8_VALUE },
                {"u16", (byte)MacroDefinition.DATATYPE.OM_U16_VALUE },
                {"s16", (byte)MacroDefinition.DATATYPE.OM_S16_VALUE },
                {"float", (byte)MacroDefinition.DATATYPE.OM_FLOAT_VALUE }};
            Dictionary<string, int> iLeaftypeSize = new Dictionary<string, int>(){
                {"s32[]", Marshal.SizeOf(new OM_OBJ_ID_T()) },
                {"u32", Marshal.SizeOf(new uint())},
                {"s32", Marshal.SizeOf(new int())},
                {"u8", Marshal.SizeOf(new byte())},
                {"s8", Marshal.SizeOf(new sbyte())},
                {"u16", Marshal.SizeOf(new ushort())},
                {"s16", Marshal.SizeOf(new short())},
                {"float", Marshal.SizeOf(new float())}};
            List<string> OmTypeList = new List<string>() { "s32[]", "u32",  "s32", "u8","s8","u16", "s16",  "float", };
            if (OmTypeList.Exists(e => e.Equals(OmType)))
            {
                recordTypeL[0] = Recordtype[OmType];
                LeaftypeSizeL[0] = iLeaftypeSize[OmType];
            }
            else if (String.Equals(OmType, "u8[]"))
            {
                if (strMibSyntax.Contains("IPADDR"))
                {
                    recordTypeL[0] = (byte)MacroDefinition.DATATYPE.OM_IP_ADDRESS_VALUE;
                    LeaftypeSizeL[0] = Marshal.SizeOf(new uint());
                }
                else if (strMibSyntax.Contains("INETADDRESS"))
                {
                    recordTypeL[0] = (byte)MacroDefinition.DATATYPE.OM_INETADDRESS_VALUE;
                    LeaftypeSizeL[0] = GetMaxStrLen(valList, OmType); ;
                }
                else if (strMibSyntax.Contains("MACADDRESS"))
                {
                    recordTypeL[0] = (byte)MacroDefinition.DATATYPE.OM_MAC_ADDRESS_VALUE;
                    LeaftypeSizeL[0] = GetMaxStrLen(valList, OmType); ;
                }
                else if (strMibSyntax.Contains("MNCMCCTYPE"))
                {
                    recordTypeL[0] = (byte)MacroDefinition.DATATYPE.OM_MNCMCC_VALUE;
                    LeaftypeSizeL[0] = GetMaxStrLen(valList, OmType); ;
                }
                else if (strMibSyntax.Contains("DATEANDTIME"))  //DataAndTime类型  
                {
                    recordTypeL[0] = (byte)MacroDefinition.DATATYPE.OM_DATATIME_VALUE;
                    LeaftypeSizeL[0] = GetMaxStrLen(valList, OmType); ;
                }
                else
                {
                    recordTypeL[0] = (byte)MacroDefinition.DATATYPE.OM_DATATIME_VALUE;
                    LeaftypeSizeL[0] = GetMaxStrLen(valList, OmType); ;
                }
            }
            else if (String.Equals(OmType, "s8[]"))
            {
                recordTypeL[0] = (byte)MacroDefinition.DATATYPE.OM_EBUFFER_VALUE;
                LeaftypeSizeL[0] = GetMaxStrLen(valList, OmType);
            }
            else if (String.Equals(OmType, "u32[]"))
            {
                recordTypeL[0] = (byte)MacroDefinition.DATATYPE.OM_U32ARRAY_VALUE;
                int len = GetMaxStrLen(valList, OmType);
                LeaftypeSizeL[0] = 4 * (len + 1);
            }
            else      //无效的类型
            {
                recordTypeL[0] = (byte)MacroDefinition.DATATYPE.OM_INVALID_VALUE;
                LeaftypeSizeL[0] = 0;
            }
        }
        int GetMaxStrLen(string ValAllList, string OMType)
        {
            List<string> OmTypeList = new List<string>() { "s8[]", "u8[]", "u32[]" };
            if (OmTypeList.Exists(e => e.Equals(OMType)))
            {
                string[] sArray = ValAllList.Split('-');//字符串类型标识方式“n1-n2”,n2标识该字符串最大长度
                string strMaxValue = sArray[1];
                if (String.Equals(OMType, "s8[]"))
                    return int.Parse(strMaxValue) + 1;
                else
                    return int.Parse(strMaxValue);
            }
            else
                return 0;
        }
        /// <summary>
        /// lm.mdb 属性"TableContent" 判断是否为动态表
        /// </summary>
        /// <param name="strTableContent"></param>
        /// <returns></returns>
        private bool isDynamicTable(string strTableContent)
        {
            if (String.Equals("0", strTableContent) || String.Empty == strTableContent)
                return false;
            return true;
        }
        /// <summary>
        /// 是否为 "alarmCauseEntry","antennaArrayTypeEntry","rruTypeEntry" ,"rruTypePortEntry"
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        private bool isSpecialTable(string strTableName)
        {
            List<string> SpecialTableArr = new List<string>() {
                    "alarmCauseEntry","antennaArrayTypeEntry","rruTypeEntry" ,"rruTypePortEntry","antennaBfScanWeightEntry"};
            if (SpecialTableArr.Exists(e => e.Equals(strTableName)))
                return true;
            return false;
        }
        /// <summary>
        /// 解析"alarmCauseEntry","antennaArrayTypeEntry","rruTypeEntry" ,"rruTypePortEntry"
        /// </summary>
        /// <param name="tableRow"></param>
        /// <param name="tableOp"></param>
        /// <param name="MibdateSet"></param>
        /// <param name="strFileToDirectory"></param>
        private void CreateSpecialTalbe(DataRow tableRow, CfgTableOp tableOp, DataSet MibdateSet, Dictionary<string, string> paths)//string strFileToDirectory)
        {
            int leafNum = MibdateSet.Tables[0].Rows.Count;
            ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            string strTableName = tableRow["MIBName"].ToString();
            string strTableContent = tableRow["TableContent"].ToString();//表容量
            bool isDyTable = isDynamicTable(strTableContent); // 是否为动态表
            int[] indexValue = new int[6];//索引值
            if (string.Equals("alarmCauseEntry", strTableName) && (isDyTable == true))//告警原因 // alarm info
            {
                CreateSpecialTalbeAlarmCauseEntryByExcel(tableRow, tableOp, paths["Alarm"], leafNum);
            }
            else if (string.Equals("antennaArrayTypeEntry", strTableName))//天线器件库信息-天线阵//2012-06-25 luoxin DTMUC00104224 创建配置文件时器件库表下天线阵类型表不做动态表处理，记录设为空
            {
                RecordInstanceMain(tableOp, isDyTable, strTableContent);//实例信息
            }
            else if (string.Equals("rruTypeEntry", strTableName) && (isDyTable == true))//器件库表-射频单元设备类型
            {
                CreateSpecialTalbeRruTypeByEx(tableRow, tableOp, leafNum);
                //string strSQLRruType = ("select  * from rruType");
                //string rruTypePath = strFileToDirectory.Substring(0, strFileToDirectory.Length - strFileToDirectory.IndexOf("lmdtz"));
                //DataSet rruTypedateSet = CfgGetRecordByAccessDb(rruTypePath + "\\LMTDBENODEB70.mdb", strSQLRruType);
                //SetBuffersInfoForRruType(tableRow, rruTypedateSet, tableOp, leafNum);
            }
            else if (string.Equals("rruTypePortEntry", strTableName) && (isDyTable == true))//器件库表-射频单元射频通道设备
            {
                CreateSpecialTalbeRruPortTypeByEx(tableRow, tableOp, leafNum);
                //string strSQLRruTypePort = ("select  * from rruTypePort");
                //string rruTypePortPath = strFileToDirectory.Substring(0, strFileToDirectory.Length - strFileToDirectory.IndexOf("lmdtz"));
                //DataSet rruTypePortdateSet = CfgGetRecordByAccessDb(rruTypePortPath + "\\LMTDBENODEB70.mdb", strSQLRruTypePort);
                //SetBuffersInfoForRruTypePort(tableRow, rruTypePortdateSet, tableOp, leafNum);
            }
            else if (string.Equals("antennaBfScanWeightEntry", strTableName))
            {
                CreateSpecialTalbeAntennaBfScanByEx(tableRow, tableOp, paths["Antenna"], leafNum);
            }
        }
        /// <summary>
        /// (从excel获取)antennaBfScanWeightEntry 天线阵波束扫描天线权值参数表(行)
        /// </summary>
        /// <param name="tableRow"></param>
        /// <param name="tableOp"></param>
        /// <param name="strFileToDirectory"></param>
        /// <param name="leafNum"></param>
        private void CreateSpecialTalbeAntennaBfScanByEx(DataRow tableRow, CfgTableOp tableOp, string strFileToDirectory, int leafNum)
        {
            int iTableNum = int.Parse(tableRow["TableContent"].ToString());// 设置动态表的容量;
            m_antennaExcel.ProcessingAntennaExcel(strFileToDirectory, "波束扫描原始值", iTableNum);
            List<AntArrayBfScanAntWeightTabStru> vectAntArrayBfScanInfo = m_antennaExcel.vectAntArrayBfScanInfo;
            SetBuffersInfoForAntennaBfScanWeightByEx( tableRow, vectAntArrayBfScanInfo, tableOp, leafNum);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableRow"></param>
        /// <param name="tableOp"></param>
        /// <param name="leafNum"></param>
        private void CreateSpecialTalbeRruPortTypeByEx(DataRow tableRow, CfgTableOp tableOp, int leafNum)
        {
            List<RRuTypePortTabStru> rruPortL = m_rruExcel.GetRruTypePortInfoData();
            SetBuffersInfoForRruTypePortByEx(tableRow, rruPortL, tableOp, leafNum);
        }
        private void CreateSpecialTalbeRruPortTypeByMdb(DataRow tableRow, CfgTableOp tableOp, int leafNum, Dictionary<string, string> paths)
        {
            string strSQLRruTypePort = ("select  * from rruTypePort");
            string strFileToDirectory = paths["DataMdb"];
            string rruTypePortPath = strFileToDirectory.Substring(0, strFileToDirectory.Length - strFileToDirectory.IndexOf("lmdtz"));
            DataSet rruTypePortdateSet = CfgGetRecordByAccessDb(rruTypePortPath + "\\LMTDBENODEB70.mdb", strSQLRruTypePort);
            SetBuffersInfoForRruTypePort(tableRow, rruTypePortdateSet, tableOp, leafNum);
        }
        /// <summary>
        /// RruType 
        /// </summary>
        /// <param name="tableRow"></param>
        /// <param name="tableOp"></param>
        /// <param name="leafNum"></param>
        private void CreateSpecialTalbeRruTypeByEx(DataRow tableRow, CfgTableOp tableOp, int leafNum)
        {
            List<RRuTypeTabStru> rruList = m_rruExcel.GetRruTypeInfoData();
            SetBuffersInfoForRruTypeByEx(tableRow, rruList, tableOp, leafNum);
        }
        private void CreateSpecialTalbeRruTypeByMdb(DataRow tableRow, CfgTableOp tableOp, int leafNum, Dictionary<string, string> paths)
        {
            string strSQLRruType = ("select  * from rruType");
            string strFileToDirectory = paths["DataMdb"];
            string rruTypePath = strFileToDirectory.Substring(0, strFileToDirectory.Length - strFileToDirectory.IndexOf("lmdtz"));
            DataSet rruTypedateSet = CfgGetRecordByAccessDb(rruTypePath + "\\LMTDBENODEB70.mdb", strSQLRruType);
            SetBuffersInfoForRruType(tableRow, rruTypedateSet, tableOp, leafNum);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableRow"></param>
        /// <param name="tableOp"></param>
        /// <param name="strFileToDirectory"></param>
        /// <param name="leafNum"></param>
        private void CreateSpecialTalbeAlarmCauseEntryByMdb(DataRow tableRow, CfgTableOp tableOp, string strFileToDirectory, int leafNum)
        {
            string strSQLAlarm = ("select  * from AlarmInform_5216");
            string alarmPath = strFileToDirectory.Substring(0, strFileToDirectory.Length - strFileToDirectory.IndexOf("lmdtz"));
            DataSet AlarmdateSet = CfgGetRecordByAccessDb(alarmPath + "\\LMTAlarm.mdb", strSQLAlarm);
            //foreach (var col in AlarmdateSet.Tables[0].Columns)
            //    Console.WriteLine(String.Format("rowName:{0},value:{1}.",col.ToString(), AlarmdateSet.Tables[0].Rows[0][col.ToString()]));
            SetBuffersInfoForAlarmCauseMdb(tableRow, AlarmdateSet, tableOp, leafNum);
        }

        private void CreateSpecialTalbeAlarmCauseEntryByExcel(DataRow tableRow, CfgTableOp tableOp, string strFileToDirectory, int leafNum)
        {
            //CfgParseAlarmExecl alarmEx = new CfgParseAlarmExecl();
            //alarmEx.ParseExcel(strFileToDirectory);
            var vectAlarmInfoExcel = m_alarmExcel.vectAlarmInfoExcel;
            //string strSQLAlarm = ("select  * from AlarmInform_5216");
            //string alarmPath = strFileToDirectory.Substring(0, strFileToDirectory.Length - strFileToDirectory.IndexOf("lmdtz"));
            //DataSet AlarmdateSet = CfgGetRecordByAccessDb(alarmPath + "\\LMTAlarm.mdb", strSQLAlarm);
            //foreach (var col in AlarmdateSet.Tables[0].Columns)
            //    Console.WriteLine(String.Format("rowName:{0},value:{1}.",col.ToString(), AlarmdateSet.Tables[0].Rows[0][col.ToString()]));
            SetBuffersInfoForAlarmCauseExcel(tableRow, vectAlarmInfoExcel, tableOp, leafNum);
        }
        /// <summary>
        /// rruTypePort 的实例处理
        /// </summary>
        /// <param name="tableRow"></param>
        /// <param name="rruTypePortdateSet"></param>
        /// <param name="tableOp"></param>
        /// <param name="leafNum"></param>
        private void SetBuffersInfoForRruTypePort(DataRow tableRow, DataSet rruTypePortdateSet, CfgTableOp tableOp, int leafNum)
        {
            string strTableContent = tableRow["TableContent"].ToString();//表容量
            int rruTypePortCount = rruTypePortdateSet.Tables[0].Rows.Count; // 数据库中的行有效数据的个数
            int nTableNum = int.Parse(strTableContent);
            List<RRuTypePortTabStru> vectRRUTypePortInfo = new List<RRuTypePortTabStru>();
            for (int loop = 0; loop < rruTypePortCount - 1; loop++)
            {  // 在表之间循环
                if (loop == nTableNum)
                    break;
                vectRRUTypePortInfo.Add(new RRuTypePortTabStru(rruTypePortdateSet.Tables[0].Rows[loop]));
            }
            int nVecSize = (int)vectRRUTypePortInfo.Count;
            int i = 0;
            ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            List<string> vectExitInstIndex = new List<string>();
            bool IsFirst = true;
            var mibNodesStruIndex = tableOp.m_struIndex.m_struIndex;
            for ( int index0 = 0; index0 < mibNodesStruIndex[0].indexNum; index0++)
            {
                for ( int index1 = 0; index1 < mibNodesStruIndex[1].indexNum; index1++)
                {
                    for ( int index2 = 0; index2 < mibNodesStruIndex[2].indexNum; index2++)
                    {
                        if (i == nTableNum)
                            return;
                        List<byte[]> BuffArrL = new List<byte[]>() { new byte[bufLens] };
                        string stInstIndex;
                        if (i < nVecSize)
                        {
                            string strIndex1 = vectRRUTypePortInfo[i].GetRRuTypePortValue(mibNodesStruIndex[0].mibName);
                            int ipos = strIndex1.IndexOf(":");//4:大唐  取数字
                            if (ipos > 0)
                            {
                                string strTempIndex = strIndex1.Substring(ipos);
                                strIndex1 = strTempIndex;
                            }
                            string strIndex2 = vectRRUTypePortInfo[i].GetRRuTypePortValue(mibNodesStruIndex[1].mibName);
                            string strIndex3 = vectRRUTypePortInfo[i].GetRRuTypePortValue(mibNodesStruIndex[2].mibName);
                            stInstIndex = String.Format(".{0}.{1}.{2}", strIndex1, strIndex2, strIndex3);
                            List<int> posL = new List<int>() { 0 };
                            for (int ileafNum = 0; ileafNum < leafNum; ileafNum++)
                            {
                                CfgFileLeafNodeOp mibNode = tableOp.m_LeafNodes[ileafNum];
                                StruMibNode m_struMibNode = tableOp.m_LeafNodes[ileafNum].m_struMibNode;
                                int typeSize = mibNode.m_struFieldInfo.u16FieldLen;//.typeSize;
                                string strCurrentValue = vectRRUTypePortInfo[ileafNum].GetRRuTypePortValue(m_struMibNode.strMibName);
                                WriteToBuffer(BuffArrL, strCurrentValue, posL, m_struMibNode.strOMType, typeSize, m_struMibNode.strMIBVal_AllList, m_struMibNode.strMibSyntax);
                            }
                            vectExitInstIndex.Add(stInstIndex);
                        }
                        else
                        {
                            if (IsFirst)
                            {
                                index1 = 0;//从新开始判断实例索引是否已经使用
                                index0 = 0;//如果实例索引已经存在则继续组织下一索引
                                IsFirst = false;
                            }
                            stInstIndex = ".0.0.0";//stInstIndex.Format(".%d.%d.%d", 0, 0, 0);
                            if (vectExitInstIndex.FindIndex(e => e.Equals(stInstIndex)) != -1) //不存在：返回-1，存在：返回位置。
                                continue;
                            List<int> bytePosL = new List<int>() { 0 };
                            for (int k = 0; k < 3; k++)
                            {
                                var LeafNode = tableOp.m_LeafNodes[k];
                                WriteToBuffer(BuffArrL, "0", bytePosL, LeafNode.m_struMibNode.strOMType, LeafNode.m_struFieldInfo.u16FieldLen, "", LeafNode.m_struMibNode.strMibSyntax);
                            }
                            for (int k = 3; k < leafNum - 1; k++)
                            {
                                WriteToBuffer(BuffArrL, "", bytePosL, tableOp.m_LeafNodes[k].m_struMibNode.strOMType,
                                    tableOp.m_LeafNodes[k].m_struFieldInfo.u16FieldLen, "", tableOp.m_LeafNodes[k].m_struMibNode.strMibSyntax);
                            }
                        }
                        tableOp.m_cfgInsts_add(stInstIndex, BuffArrL[0]);
                        i++;
                    }
                }
            }
            //}
        }
        private void SetBuffersInfoForRruTypePortByEx(DataRow tableRow, List<RRuTypePortTabStru> vectRRUTypePortInfo, CfgTableOp tableOp, int leafNum)
        {
            //string strTableContent = tableRow["TableContent"].ToString();//表容量
            //int rruTypePortCount = rruTypePortdateSet.Tables[0].Rows.Count; // 数据库中的行有效数据的个数
            int nTableNum = int.Parse(tableRow["TableContent"].ToString());//表容量
            //List<RRuTypePortTabStru> vectRRUTypePortInfo = new List<RRuTypePortTabStru>();
            //for (int loop = 0; loop < rruTypePortCount - 1; loop++)
            //{  // 在表之间循环
            //    if (loop == nTableNum)
            //        break;
            //    vectRRUTypePortInfo.Add(new RRuTypePortTabStru(rruTypePortdateSet.Tables[0].Rows[loop]));
            //}
            int nVecSize = (int)vectRRUTypePortInfo.Count;
            int i = 0;
            ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            List<string> vectExitInstIndex = new List<string>();
            bool IsFirst = true;
            var mibNodesStruIndex = tableOp.m_struIndex.m_struIndex;
            for (int index0 = 0; index0 < mibNodesStruIndex[0].indexNum; index0++)
            {
                for (int index1 = 0; index1 < mibNodesStruIndex[1].indexNum; index1++)
                {
                    for (int index2 = 0; index2 < mibNodesStruIndex[2].indexNum; index2++)
                    {
                        if (i == nTableNum)
                            return;
                        List<byte[]> BuffArrL = new List<byte[]>() { new byte[bufLens] };
                        string stInstIndex;
                        if (i < nVecSize)
                        {
                            string strIndex1 = vectRRUTypePortInfo[i].GetRRuTypePortValue(mibNodesStruIndex[0].mibName);
                            int ipos = strIndex1.IndexOf(":");//4:大唐  取数字
                            if (ipos > 0)
                            {
                                string strTempIndex = strIndex1.Substring(0, ipos);
                                strIndex1 = strTempIndex;
                            }
                            string strIndex2 = vectRRUTypePortInfo[i].GetRRuTypePortValue(mibNodesStruIndex[1].mibName);
                            string strIndex3 = vectRRUTypePortInfo[i].GetRRuTypePortValue(mibNodesStruIndex[2].mibName);
                            stInstIndex = String.Format(".{0}.{1}.{2}", strIndex1, strIndex2, strIndex3);
                            List<int> posL = new List<int>() { 0 };
                            for (int ileafNum = 0; ileafNum < leafNum; ileafNum++)
                            {
                                CfgFileLeafNodeOp mibNode = tableOp.m_LeafNodes[ileafNum];
                                StruMibNode m_struMibNode = tableOp.m_LeafNodes[ileafNum].m_struMibNode;
                                int typeSize = mibNode.m_struFieldInfo.u16FieldLen;//.typeSize;
                                string strCurrentValue = vectRRUTypePortInfo[i].GetRRuTypePortValue(m_struMibNode.strMibName);
                                WriteToBuffer(BuffArrL, strCurrentValue, posL, m_struMibNode.strOMType, typeSize, m_struMibNode.strMIBVal_AllList, m_struMibNode.strMibSyntax);
                            }
                            vectExitInstIndex.Add(stInstIndex);
                        }
                        else
                        {
                            if (IsFirst)
                            {
                                index1 = 0;//从新开始判断实例索引是否已经使用
                                index0 = 0;//如果实例索引已经存在则继续组织下一索引
                                IsFirst = false;
                            }
                            stInstIndex = ".0.0.0";//stInstIndex.Format(".%d.%d.%d", 0, 0, 0);
                            if (vectExitInstIndex.FindIndex(e => e.Equals(stInstIndex)) != -1) //不存在：返回-1，存在：返回位置。
                                continue;
                            List<int> bytePosL = new List<int>() { 0 };
                            for (int k = 0; k < 3; k++)
                            {
                                var LeafNode = tableOp.m_LeafNodes[k];
                                WriteToBuffer(BuffArrL, "0", bytePosL, LeafNode.m_struMibNode.strOMType, LeafNode.m_struFieldInfo.u16FieldLen, "", LeafNode.m_struMibNode.strMibSyntax);
                            }
                            for (int k = 3; k < leafNum - 1; k++)
                            {
                                WriteToBuffer(BuffArrL, "", bytePosL, tableOp.m_LeafNodes[k].m_struMibNode.strOMType,
                                    tableOp.m_LeafNodes[k].m_struFieldInfo.u16FieldLen, "", tableOp.m_LeafNodes[k].m_struMibNode.strMibSyntax);
                            }
                        }
                        tableOp.m_cfgInsts_add(stInstIndex, BuffArrL[0]);
                        i++;
                    }
                }
            }
        }
        /// <summary>
        /// "alarmCauseEntry" 的实例处理
        /// </summary>
        /// <param name="tableRow"></param>
        /// <param name="AlarmdateSet"></param>
        /// <param name="tableOp"></param>
        /// <param name="leafNum"></param>
        private void SetBuffersInfoForAlarmCauseMdb(DataRow tableRow, DataSet AlarmdateSet, CfgTableOp tableOp, int leafNum)
        {
            ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            string strTableContent = tableRow["TableContent"].ToString();//表容量
            int nTableNum = int.Parse(strTableContent);// 例如alarmCauseEntry表容量为4000
            int alarmCount = AlarmdateSet.Tables[0].Rows.Count; // 例如一个版本 2178个告警信息 0~2177

            List<StruAlarmInfo> vectAlarmInfo = new List<StruAlarmInfo>();
            for (int loop = 0; loop < alarmCount - 1; loop++){  // 在表之间循环
                vectAlarmInfo.Add(new StruAlarmInfo(AlarmdateSet.Tables[0].Rows[loop]));
            }
            /// 
            int setBufTableNum = 0;//处理的表
            List<string> exitInstIndexList = new List<string>();// 存真实数据库中的告警号
            bool IsFirst = true;
            Console.WriteLine(tableOp.get_m_cfgInsts_num());
            for (int index = 0; index < nTableNum; index++)//告警表是一维
            {
                List<byte[]> BuffArrL = new List<byte[]>() { new byte[bufLens] };
                string strTabIndex;
                if (setBufTableNum < alarmCount - 1)
                {
                    WriteAlarmDataToCfg(BuffArrL, vectAlarmInfo[setBufTableNum], leafNum, tableOp);
                    strTabIndex = "." + vectAlarmInfo[setBufTableNum].GetContrastValue(tableOp.m_LeafNodes[0].m_struMibNode.strMibName);
                    exitInstIndexList.Add(strTabIndex);
                }
                else
                {
                    if (IsFirst)
                    {
                        index = 0;//从新开始判断实例索引是否已经使用
                        IsFirst = false;
                    }

                    strTabIndex = ".0";
                    if (exitInstIndexList.FindIndex(e => e.Equals(strTabIndex)) == -1) //不存在：返回-1，存在：返回位置。
                        continue;
                    List<int> bytePosL = new List<int>() { 0 };
                    WriteToBuffer(BuffArrL, "0", bytePosL, tableOp.m_LeafNodes[0].m_struMibNode.strOMType, 0, "", "");
                    for (int k = 1; k < leafNum-1; k++){
                        WriteToBuffer(BuffArrL, "", bytePosL, tableOp.m_LeafNodes[k].m_struMibNode.strOMType, 
                            tableOp.m_LeafNodes[k].m_struFieldInfo.u16FieldLen, "", tableOp.m_LeafNodes[k].m_struMibNode.strMibSyntax);
                    }
                }
                setBufTableNum++;
                tableOp.m_cfgInsts_add(strTabIndex, BuffArrL[0]);
            }
            Console.WriteLine(tableOp.get_m_cfgInsts_num());
        }
        private void SetBuffersInfoForAlarmCauseExcel(DataRow tableRow, List<StruAlarmInfo> vectAlarmInfo, CfgTableOp tableOp, int leafNum)
        {
            ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            string strTableContent = tableRow["TableContent"].ToString();//表容量
            int nTableNum = int.Parse(strTableContent);// 例如alarmCauseEntry表容量为4000
            //int alarmCount = AlarmdateSet.Tables[0].Rows.Count; // 例如一个版本 2178个告警信息 0~2177
            int alarmCount = vectAlarmInfo.Count;
            int setBufTableNum = 0;//处理的表
            List<string> exitInstIndexList = new List<string>();// 存真实数据库中的告警号
            bool IsFirst = true;
            //Console.WriteLine(tableOp.get_m_cfgInsts_num());
            for (int index = 0; index < nTableNum; index++)//告警表是一维
            {
                List<byte[]> BuffArrL = new List<byte[]>() { new byte[bufLens] };
                string strTabIndex;
                if (setBufTableNum < alarmCount)
                {
                    WriteAlarmDataToCfg(BuffArrL, vectAlarmInfo[setBufTableNum], leafNum, tableOp);
                    strTabIndex = "." + vectAlarmInfo[setBufTableNum].GetContrastValue(tableOp.m_LeafNodes[0].m_struMibNode.strMibName);
                    exitInstIndexList.Add(strTabIndex);
                }
                else
                {
                    if (IsFirst)
                    {
                        index = 0;//从新开始判断实例索引是否已经使用
                        IsFirst = false;
                    }

                    strTabIndex = ".0";
                    if (exitInstIndexList.FindIndex(e => e.Equals(strTabIndex)) == -1) //不存在：返回-1，存在：返回位置。
                        continue;
                    List<int> bytePosL = new List<int>() { 0 };
                    WriteToBuffer(BuffArrL, "0", bytePosL, tableOp.m_LeafNodes[0].m_struMibNode.strOMType, 0, "", "");
                    for (int k = 1; k < leafNum - 1; k++)
                    {
                        WriteToBuffer(BuffArrL, "", bytePosL, tableOp.m_LeafNodes[k].m_struMibNode.strOMType,
                            tableOp.m_LeafNodes[k].m_struFieldInfo.u16FieldLen, "", tableOp.m_LeafNodes[k].m_struMibNode.strMibSyntax);
                    }
                }
                setBufTableNum++;
                tableOp.m_cfgInsts_add(strTabIndex, BuffArrL[0]);
            }
            //Console.WriteLine(tableOp.get_m_cfgInsts_num());
        }
        /// <summary>
        /// rruTypeEntry 的实例处理
        /// </summary>
        /// <param name="tableRow"></param>
        /// <param name="rruTypedateSet"></param>
        /// <param name="tableOp"></param>
        /// <param name="leafNum"></param>
        private void SetBuffersInfoForRruType(DataRow tableRow, DataSet rruTypedateSet, CfgTableOp tableOp, int leafNum)
        {
            int rruTypeCount = rruTypedateSet.Tables[0].Rows.Count; // 数据库中的行有效数据的个数
            int nTableNum = int.Parse(tableRow["TableContent"].ToString());//表容量
            List<RRuTypeTabStru> vectRRUTypeInfo = new List<RRuTypeTabStru>();
            for (int loop = 0; loop < rruTypeCount - 1; loop++)
            {
                if (loop == nTableNum) break ;
                vectRRUTypeInfo.Add(new RRuTypeTabStru(rruTypedateSet.Tables[0].Rows[loop], rruTypedateSet));
            }

            ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            int TableDimen = tableOp.m_tabDimen;// 索引数量
            var m_struIndex = tableOp.m_struIndex.m_struIndex;
            int i = 0;
            List<string> vectExitInstIndex = new List<string>();
            bool IsFirst = true;
            for (int index0 = 0; index0 < m_struIndex[0].indexNum; index0++)
            {
                for ( int index1 = 0; index1 < m_struIndex[1].indexNum; index1++)
                {
                    if (i == nTableNum)
                        return;
                    List<byte[]> BuffArrL = new List<byte[]>() { new byte[bufLens] };
                    string stInstIndex;
                    if (i < vectRRUTypeInfo.Count)
                    {
                        RRuTypeTabStru pRRuType = vectRRUTypeInfo[i];
                        string strIndex1Name = tableOp.m_LeafNodes[0].m_struMibNode.strMibName;
                        string strIndex2Name = tableOp.m_LeafNodes[1].m_struMibNode.strMibName;
                        string strIndex1 = pRRuType.GetRRuLeafValue(strIndex1Name);
                        if (strIndex1.IndexOf(":") > 0)//4:大唐  取数字
                            strIndex1 = strIndex1.Substring(strIndex1.IndexOf(":"));
                        string strIndex2 = pRRuType.GetRRuLeafValue(strIndex2Name);
                        stInstIndex = String.Format(".{0}.{1}", strIndex1, strIndex2);
                        List<int> bytePosL = new List<int>() { 0 };
                        for (int ileafNum = 0; ileafNum < leafNum; ileafNum++)
                        {
                            var mibNode = tableOp.m_LeafNodes[ileafNum];
                            string strCurrentValue = pRRuType.GetRRuLeafValue(mibNode.m_struMibNode.strMibName);
                            int typeSize = (int)mibNode.m_struFieldInfo.u16FieldLen;
                            WriteToBuffer(BuffArrL, strCurrentValue, bytePosL, mibNode.m_struMibNode.strOMType, typeSize, mibNode.m_struMibNode.strMIBVal_AllList, mibNode.m_struMibNode.strMibSyntax);
                        }
                        vectExitInstIndex.Add(stInstIndex);
                    }
                    else
                    {
                        if (IsFirst)
                        {
                            index1 = 0;//从新开始判断实例索引是否已经使用
                            index0 = 0;//如果实例索引已经存在则继续组织下一索引
                            IsFirst = false;
                        }
                        stInstIndex = ".0.0";
                        if (vectExitInstIndex.FindIndex(e => e.Equals(stInstIndex)) != -1) //不存在：返回-1，存在：返回位置。
                            continue;
                        List<int> bytePosL = new List<int>() { 0 };
                        for (int k = 0; k < 2; k++)
                        {
                            var LeafNode = tableOp.m_LeafNodes[k];
                            WriteToBuffer(BuffArrL, "0", bytePosL, LeafNode.m_struMibNode.strOMType,LeafNode.m_struFieldInfo.u16FieldLen, "", LeafNode.m_struMibNode.strMibSyntax);
                        }
                        for (int k = 2; k < leafNum - 1; k++)
                        {
                            WriteToBuffer(BuffArrL, "", bytePosL, tableOp.m_LeafNodes[k].m_struMibNode.strOMType,
                                tableOp.m_LeafNodes[k].m_struFieldInfo.u16FieldLen, "", tableOp.m_LeafNodes[k].m_struMibNode.strMibSyntax);
                        }
                    }
                    i++;
                    tableOp.m_cfgInsts_add(stInstIndex, BuffArrL[0]);
                }
            }
        }
        private void SetBuffersInfoForRruTypeByEx(DataRow tableRow, List<RRuTypeTabStru> vectRRUTypeInfo,CfgTableOp tableOp, int leafNum)
        {
            //int rruTypeCount = rruTypedateSet.Tables[0].Rows.Count; // 数据库中的行有效数据的个数
            int nTableNum = int.Parse(tableRow["TableContent"].ToString());//表容量

            ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            int TableDimen = tableOp.m_tabDimen;// 索引数量
            var m_struIndex = tableOp.m_struIndex.m_struIndex;
            int i = 0;
            List<string> vectExitInstIndex = new List<string>();
            bool IsFirst = true;
            for (int index0 = 0; index0 < m_struIndex[0].indexNum; index0++)
            {
                for (int index1 = 0; index1 < m_struIndex[1].indexNum; index1++)
                {
                    if (i == nTableNum)
                        return;
                    List<byte[]> BuffArrL = new List<byte[]>() { new byte[bufLens] };
                    string stInstIndex;
                    if (i < vectRRUTypeInfo.Count)
                    {
                        RRuTypeTabStru pRRuType = vectRRUTypeInfo[i];
                        string strIndex1Name = tableOp.m_LeafNodes[0].m_struMibNode.strMibName;
                        string strIndex2Name = tableOp.m_LeafNodes[1].m_struMibNode.strMibName;
                        string strIndex1 = pRRuType.GetRRuLeafValue(strIndex1Name);
                        if (strIndex1.IndexOf(":") > 0)//4:大唐  取数字
                            strIndex1 = strIndex1.Substring(strIndex1.IndexOf(":"));
                        string strIndex2 = pRRuType.GetRRuLeafValue(strIndex2Name);
                        stInstIndex = String.Format(".{0}.{1}", strIndex1, strIndex2);
                        List<int> bytePosL = new List<int>() { 0 };
                        for (int ileafNum = 0; ileafNum < leafNum; ileafNum++)
                        {
                            var mibNode = tableOp.m_LeafNodes[ileafNum];
                            string strCurrentValue = pRRuType.GetRRuLeafValue(mibNode.m_struMibNode.strMibName);
                            int typeSize = (int)mibNode.m_struFieldInfo.u16FieldLen;
                            WriteToBuffer(BuffArrL, strCurrentValue, bytePosL, mibNode.m_struMibNode.strOMType, typeSize, mibNode.m_struMibNode.strMIBVal_AllList, mibNode.m_struMibNode.strMibSyntax);
                        }
                        vectExitInstIndex.Add(stInstIndex);
                    }
                    else
                    {
                        if (IsFirst)
                        {
                            index1 = 0;//从新开始判断实例索引是否已经使用
                            index0 = 0;//如果实例索引已经存在则继续组织下一索引
                            IsFirst = false;
                        }
                        stInstIndex = ".0.0";
                        if (vectExitInstIndex.FindIndex(e => e.Equals(stInstIndex)) != -1) //不存在：返回-1，存在：返回位置。
                            continue;
                        List<int> bytePosL = new List<int>() { 0 };
                        for (int k = 0; k < 2; k++)
                        {
                            var LeafNode = tableOp.m_LeafNodes[k];
                            WriteToBuffer(BuffArrL, "0", bytePosL, LeafNode.m_struMibNode.strOMType, LeafNode.m_struFieldInfo.u16FieldLen, "", LeafNode.m_struMibNode.strMibSyntax);
                        }
                        for (int k = 2; k < leafNum - 1; k++)
                        {
                            WriteToBuffer(BuffArrL, "", bytePosL, tableOp.m_LeafNodes[k].m_struMibNode.strOMType,
                                tableOp.m_LeafNodes[k].m_struFieldInfo.u16FieldLen, "", tableOp.m_LeafNodes[k].m_struMibNode.strMibSyntax);
                        }
                    }
                    i++;
                    tableOp.m_cfgInsts_add(stInstIndex, BuffArrL[0]);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableRow"></param>
        /// <param name="vectAntArrayBfScanInfo"></param>
        /// <param name="tableOp"></param>
        /// <param name="leafNum"></param>
        private void SetBuffersInfoForAntennaBfScanWeightByEx(DataRow tableRow, List<AntArrayBfScanAntWeightTabStru> vectAntArrayBfScanInfo, CfgTableOp tableOp, int leafNum)
        {
            //int rruTypeCount = rruTypedateSet.Tables[0].Rows.Count; // 数据库中的行有效数据的个数
            int nTableNum = int.Parse(tableRow["TableContent"].ToString());//表容量

            int nVecSize = vectAntArrayBfScanInfo.Count;
            ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            var m_struIndex = tableOp.m_struIndex.m_struIndex;

            int cfgInstsNum = 0;//填写的实例个数
            List<string> vectExitInstIndex = new List<string>();
            bool IsFirst = true;//是否第二次重新
            for (int index0 = 0; index0 < m_struIndex[0].indexNum; index0++)
            {
                for (int index1 = 0; index1 < m_struIndex[1].indexNum; index1++)
                {
                    for (int index2 = 0; index2 < m_struIndex[2].indexNum; index2++)
                    {
                        for (int index3 = 0; index3 < m_struIndex[3].indexNum; index3++)
                        {
                            for (int index4 = 0; index4 < m_struIndex[4].indexNum; index4++)
                            {
                                // 实例个数 等于 表容量时；
                                if (cfgInstsNum == nTableNum)
                                {
                                    return;
                                }
                                // 实例个数 大于 excel表中数据时
                                if (cfgInstsNum > nVecSize)
                                {
                                    if (IsFirst)
                                    {
                                        index1 = 0;//从新开始判断实例索引是否已经使用
                                        index0 = 0;//如果实例索引已经存在则继续组织下一索引
                                        index2 = 0;
                                        index3 = 0;
                                        IsFirst = false;
                                    }
                                }

                                if (!WriteToBufferByAntennaBf(bufLens, vectExitInstIndex, cfgInstsNum, tableOp, leafNum, vectAntArrayBfScanInfo))
                                    continue;

                                cfgInstsNum++;
                            }
                        }
                    }
                }
            }
        }
        private void SetBuffersInfoForAntennaBfScanWeightByEx2(DataRow tableRow, List<AntArrayBfScanAntWeightTabStru> vectAntArrayBfScanInfo, CfgTableOp tableOp, int leafNum)
        {
            //int rruTypeCount = rruTypedateSet.Tables[0].Rows.Count; // 数据库中的行有效数据的个数
            int nTableNum = int.Parse(tableRow["TableContent"].ToString());//表容量

            int nVecSize = vectAntArrayBfScanInfo.Count;
            ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            var m_struIndex = tableOp.m_struIndex.m_struIndex;

            int cfgInstsNum = 0;//填写的实例个数
            List<string> vectExitInstIndex = new List<string>();
            bool IsFirst = true;
            for ( int index0 = 0; index0 < m_struIndex[0].indexNum; index0++)
            {
                for ( int index1 = 0; index1 < m_struIndex[1].indexNum; index1++)
                {
                    for ( int index2 = 0; index2 < m_struIndex[2].indexNum; index2++)
                    {
                        for ( int index3 = 0; index3 < m_struIndex[3].indexNum; index3++)
                        {
                            for ( int index4 = 0; index4 < m_struIndex[4].indexNum; index4++)
                            {
                                if (cfgInstsNum == nTableNum)
                                {
                                    return;
                                }

                                List<byte[]> BuffArrL = new List<byte[]>() { new byte[bufLens] };
                                string stInstIndex = "";
                                if (cfgInstsNum < nVecSize)
                                {
                                    AntArrayBfScanAntWeightTabStru pAntArrayBfScanInfo = vectAntArrayBfScanInfo[cfgInstsNum];
                                    for (int indexNo = 0; indexNo < 5; indexNo++)
                                    {
                                        string strIndexName = tableOp.m_LeafNodes[indexNo].m_struMibNode.strMibName;
                                        string strIndex = pAntArrayBfScanInfo.GetAntArrayBfScanLeafValue(strIndexName);
                                        stInstIndex += String.Format(".{0}", strIndex);
                                    }

                                    List<int> bytePosL = new List<int>() { 0 };
                                    for (int ileafNum = 0; ileafNum < leafNum; ileafNum++)
                                    {
                                        var mibNode = tableOp.m_LeafNodes[ileafNum];
                                        string strCurrentValue = pAntArrayBfScanInfo.GetAntArrayBfScanLeafValue(mibNode.m_struMibNode.strMibName);
                                        int typeSize = (int)mibNode.m_struFieldInfo.u16FieldLen;
                                        WriteToBuffer(BuffArrL, strCurrentValue, bytePosL, mibNode.m_struMibNode.strOMType, typeSize, mibNode.m_struMibNode.strMIBVal_AllList, mibNode.m_struMibNode.strMibSyntax);
                                    }
                                    vectExitInstIndex.Add(stInstIndex);
                                }
                                else
                                {
                                    if (IsFirst)
                                    {
                                        index1 = 0;//从新开始判断实例索引是否已经使用
                                        index0 = 0;//如果实例索引已经存在则继续组织下一索引
                                        index2 = 0;
                                        index3 = 0;
                                        IsFirst = false;
                                    }
                                    //stInstIndex.Format(".%d.%d.%d",index0,index1,index2);
                                    stInstIndex = ".0.0.0.0.0";//.Format(".%d.%d.%d.%d.%d", 0, 0, 0, 0, 0);
                                    if (vectExitInstIndex.FindIndex(e => e.Equals(stInstIndex)) != -1) //不存在：返回-1，存在：返回位置。
                                        continue;

                                    List<int> bytePosL = new List<int>() { 0 };
                                    for (int k = 0; k < 5; k++)
                                    {
                                        var LeafNode = tableOp.m_LeafNodes[k];
                                        WriteToBuffer(BuffArrL, "0", bytePosL, LeafNode.m_struMibNode.strOMType, LeafNode.m_struFieldInfo.u16FieldLen, "", LeafNode.m_struMibNode.strMibSyntax);
                                    }
                                    for (int k = 5; k < leafNum - 1; k++)
                                    {
                                        WriteToBuffer(BuffArrL, "", bytePosL, tableOp.m_LeafNodes[k].m_struMibNode.strOMType,
                                            tableOp.m_LeafNodes[k].m_struFieldInfo.u16FieldLen, "", tableOp.m_LeafNodes[k].m_struMibNode.strMibSyntax);
                                    }
                                }
                                cfgInstsNum++;
                                tableOp.m_cfgInsts_add(stInstIndex, BuffArrL[0]);
                            }
                        }
                    }
                }
            }
        }
        bool WriteToBufferByAntennaBf(ushort bufLens, List<string> vectExitInstIndex, int cfgInstsNum, CfgTableOp tableOp, int leafNum, List<AntArrayBfScanAntWeightTabStru> vectAntArrayBfScanInfo)
        {
            //ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            List<byte[]> BuffArrL = new List<byte[]>() { new byte[bufLens] };
            string stInstIndex = "";
            if (cfgInstsNum < vectAntArrayBfScanInfo.Count)
            {
                AntArrayBfScanAntWeightTabStru pAntArrayBfScanInfo = vectAntArrayBfScanInfo[cfgInstsNum];
                for (int indexNo = 0; indexNo < 5; indexNo++)
                {
                    string strIndexName = tableOp.m_LeafNodes[indexNo].m_struMibNode.strMibName;
                    string strIndex = pAntArrayBfScanInfo.GetAntArrayBfScanLeafValue(strIndexName);
                    stInstIndex += String.Format(".{0}", strIndex);
                }

                List<int> bytePosL = new List<int>() { 0 };
                for (int ileafNum = 0; ileafNum < leafNum; ileafNum++)
                {
                    var mibNode = tableOp.m_LeafNodes[ileafNum];
                    string strCurrentValue = pAntArrayBfScanInfo.GetAntArrayBfScanLeafValue(mibNode.m_struMibNode.strMibName);
                    int typeSize = (int)mibNode.m_struFieldInfo.u16FieldLen;
                    WriteToBuffer(BuffArrL, strCurrentValue, bytePosL, mibNode.m_struMibNode.strOMType, typeSize, mibNode.m_struMibNode.strMIBVal_AllList, mibNode.m_struMibNode.strMibSyntax);
                }
                vectExitInstIndex.Add(stInstIndex);
            }
            else
            {
                stInstIndex = ".0.0.0.0.0";//.Format(".%d.%d.%d.%d.%d", 0, 0, 0, 0, 0);
                if (vectExitInstIndex.FindIndex(e => e.Equals(stInstIndex)) != -1) //不存在：返回-1，存在：返回位置。
                    return false;

                List<int> bytePosL = new List<int>() { 0 };
                for (int k = 0; k < 5; k++)
                {
                    var LeafNode = tableOp.m_LeafNodes[k];
                    WriteToBuffer(BuffArrL, "0", bytePosL, LeafNode.m_struMibNode.strOMType, LeafNode.m_struFieldInfo.u16FieldLen, "", LeafNode.m_struMibNode.strMibSyntax);
                }
                for (int k = 5; k < leafNum - 1; k++)
                {
                    WriteToBuffer(BuffArrL, "", bytePosL, tableOp.m_LeafNodes[k].m_struMibNode.strOMType,
                        tableOp.m_LeafNodes[k].m_struFieldInfo.u16FieldLen, "", tableOp.m_LeafNodes[k].m_struMibNode.strMibSyntax);
                }
            }
            tableOp.m_cfgInsts_add(stInstIndex, BuffArrL[0]);
            return true;
        }
        /// <summary>
        /// "alarmCauseEntry" 的实例处理 —— 特殊处理
        /// </summary>
        /// <param name="BuffArrL"></param>
        /// <param name="pStrutAlarmInfo"></param>
        /// <param name="leafNum"></param>
        /// <param name="tableOp"></param>
        private void WriteAlarmDataToCfg(List<byte[]> BuffArrL, StruAlarmInfo pStrutAlarmInfo, int leafNum, CfgTableOp tableOp)// OM_STRU_VALUE pLeafValue)
        {
            int pos = 0;
            List<int> posL = new List<int>() { pos };
            for (int ileafNum = 0; ileafNum < leafNum; ileafNum++)
            {
                CfgFileLeafNodeOp mibNode = tableOp.m_LeafNodes[ileafNum];
                StruMibNode m_struMibNode = tableOp.m_LeafNodes[ileafNum].m_struMibNode;
                string strCurrentValue = pStrutAlarmInfo.GetContrastValue(m_struMibNode.strMibName);
                int typeSize = mibNode.m_struFieldInfo.u16FieldLen;//.typeSize;
                WriteToBuffer(BuffArrL, strCurrentValue, posL, m_struMibNode.strOMType, typeSize, m_struMibNode.strMIBVal_AllList, m_struMibNode.strMibSyntax);
            }
            return;
        }
        /// <summary>
        /// 多维索引的实例处理 —— 具体处理
        /// </summary>
        /// <param name="TableDimen"></param>
        /// <param name="bufLens"></param>
        /// <param name="isDyTable"></param>
        /// <param name="indexValue"></param>
        /// <param name="tableOp"></param>
        /// <param name="strTableContent"></param>
        /// <param name="nTableRecord"></param>
        /// <returns></returns>
        private bool RecordInstanceSetCfgInst(int TableDimen, ushort bufLens, bool isDyTable, int[] indexValue, CfgTableOp tableOp, string strTableContent, int nTableRecord)
        {
            if (isDyTable == true && nTableRecord == int.Parse(strTableContent))//动态表的处理
            {
                return false;
            }
            string strInstantNum = "";
            if (TableDimen == 0)//0维索引：即标量表
                strInstantNum = "." + "0";//标量
            else // 多维索引
            {
                for (int i = 0; i < TableDimen; i++)
                {
                    if (isDyTable == true)//modify by yangyuming 增加对动态表的处理，动态表索引全部置成0 2012.3.23
                    {
                        strInstantNum += "." + "0";
                        indexValue[i] = 0;
                    }
                    else
                        strInstantNum += "." + String.Format("{0}", indexValue[i]);
                }
            }
            List<byte[]> BuffArrL = new List<byte[]>() { new byte[bufLens] };
            List<int> bytePosL = new List<int>() { 0 };
            if (TableDimen > 0)//先记录索引字段信息
                RecordIndexInstance(BuffArrL, 0, TableDimen, indexValue, tableOp, bytePosL);
            for (int k = TableDimen; k < tableOp.m_LeafNodes.Count; k++)
            {
                var mibNode = tableOp.m_LeafNodes[k].m_struMibNode;
                string mibName = mibNode.strMibName;
                //if (String.Equals(mibName, "nrCellCfgPdcchDmrsPower"))
                //{
                //    Console.WriteLine("nrCellCfgPdcchDmrsPower");
                //}
                string strIndexOMType2 = mibNode.strOMType;
                ushort typeSize2 = tableOp.m_LeafNodes[k].m_struFieldInfo.u16FieldLen;//typeSize
                string asnType2 = mibNode.strMibSyntax;//asnType
                string strDefaultValue2 = mibNode.strMibDefValue;
                string strValList = mibNode.strMIBVal_AllList;
                if (isDyTable == true)//modify by yangyuming 增加对动态表的处理，动态表内容全部置成0 2012.3.23
                    WriteToBuffer(BuffArrL, "", bytePosL, strIndexOMType2, typeSize2, "", asnType2);
                else
                {
                    try{
                        WriteToBuffer(BuffArrL, strDefaultValue2, bytePosL, strIndexOMType2, typeSize2, strValList, asnType2);
                    }
                    catch //(Exception ex)
                    {
                        Console.WriteLine(String.Format("Cfg Err : WriteToBuffer: {0}'s omType is ({1}) not matches defaultValue ({2}).", mibNode.strMibName, strIndexOMType2, strDefaultValue2)) ;//显示异常信息
                    }
                }
            }
            tableOp.m_cfgInsts_add(strInstantNum, BuffArrL[0]);//真正存 buffs
            return true;
        }
        /// <summary>
        /// 多维实例处理 —— 递归逻辑
        /// </summary>
        /// <param name="indexNo"></param>
        /// <param name="indexValue"></param>
        /// <param name="TableDimen"></param>
        /// <param name="isDyTable"></param>
        /// <param name="tableOp"></param>
        /// <param name="bufLens"></param>
        /// <param name="strTableContent"></param>
        /// <param name="nTableRecord"></param>
        /// <returns></returns>
        private int RecordInstanceRecursive(int indexNo, int[] indexValue, int TableDimen, bool isDyTable, CfgTableOp tableOp, ushort bufLens,string strTableContent, int nTableRecord)
        {
            CfgFileLeafNodeIndexInfoOp m_leaf_list = tableOp.m_struIndex;
            for (int num = 0;  num < m_leaf_list.m_struIndex[indexNo].indexNum; num++)
            {
                if (isDyTable == true && nTableRecord == int.Parse(strTableContent))//动态表的处理
                    return nTableRecord;
                if (m_leaf_list.m_struIndex[indexNo].bEnumIndex == true)//枚举类型
                    indexValue[indexNo] = (int)m_leaf_list.m_struIndex[indexNo].pEnumIndexValue[num];
                else
                    indexValue[indexNo] = num + (int)m_leaf_list.m_struIndex[indexNo].IndexMinValue;
                if (indexNo < TableDimen -1)// 递归 下一个索引
                    nTableRecord = RecordInstanceRecursive( (indexNo+1), indexValue, TableDimen, isDyTable, tableOp, bufLens, strTableContent, nTableRecord);
                else // 如果是最后一维度
                    if (RecordInstanceSetCfgInst(TableDimen, bufLens, isDyTable, indexValue, tableOp, strTableContent, nTableRecord))
                        nTableRecord++;
                //if (nTableRecord == 95)
                //    Console.WriteLine();
            }
            return nTableRecord;
        }
        /// <summary>
        /// "antennaArrayTypeEntry" 和 多维实例处理
        /// </summary>
        /// <param name="tableOp"></param>
        /// <param name="isDyTable"></param>
        /// <param name="strTableContent"></param>
        private void RecordInstanceMain(CfgTableOp tableOp, bool isDyTable, string strTableContent)
        {
            ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            int TableDimen = tableOp.m_tabDimen;// 索引数量
            int nTableRecord = 0;// 完成填写的记录数
            int[] indexValue = new int[6];// 6=TableDimen
            nTableRecord = RecordInstanceRecursive(0, indexValue, TableDimen, isDyTable, tableOp, bufLens, strTableContent, nTableRecord);
        }
        private void RecordInstance(CfgTableOp tableOp, DataSet tableRow, bool isDyTable, string strTableContent)//OM_STRU_VALUE* pLeafValue, struIndexInfoCFG* pStruIndexInfo, int leafNum, int buflen, CString strDyTabCont, BOOL isdyTab)
        {
            int nTableRecord = 0;
            ushort bufLens = tableOp.GetAllLeafsFieldLens();//字段总长
            int TableDimen = tableOp.m_tabDimen;// pLeafValue[0].TableDimen;  //所有的Leaf的TableDimen都一样
            int[] indexValue = new int[6];// 6=TableDimen
            var m_leaf_list = tableOp.m_struIndex;
            
            for (int index0 = 0; index0 < m_leaf_list.m_struIndex[0].indexNum; index0++)
            {
                for (int index1 = 0;index1 < m_leaf_list.m_struIndex[1].indexNum; index1++)
                {
                    for (int index2 = 0; index2 < m_leaf_list.m_struIndex[2].indexNum; index2++)
                    {
                        for (int index3 = 0; index3 < m_leaf_list.m_struIndex[3].indexNum; index3++)
                        {
                            for (int index4 = 0; index4 < m_leaf_list.m_struIndex[4].indexNum; index4++)
                            {
                                for (int index5 = 0; index5 < m_leaf_list.m_struIndex[5].indexNum; index5++)
                                {
                                    if (isDyTable == true && nTableRecord == int.Parse(strTableContent)) { 
                                        return;
                                    }
                                    bool bHasValue = true;
                                    if (m_leaf_list.m_struIndex[0].bEnumIndex == true)//枚举类型
                                        indexValue[0] = (int)m_leaf_list.m_struIndex[0].pEnumIndexValue[index0];
                                    else
                                        indexValue[0] = index0 + (int)m_leaf_list.m_struIndex[0].IndexMinValue;
                                    if (m_leaf_list.m_struIndex[1].bEnumIndex == true)//枚举类型
                                        indexValue[1] = (int)m_leaf_list.m_struIndex[1].pEnumIndexValue[index1];
                                    else
                                        indexValue[1] = index1 + (int)m_leaf_list.m_struIndex[1].IndexMinValue;
                                    if (m_leaf_list.m_struIndex[2].bEnumIndex == true)//枚举类型
                                        indexValue[2] = (int)m_leaf_list.m_struIndex[2].pEnumIndexValue[index2];
                                    else
                                        indexValue[2] = index2 + (int)m_leaf_list.m_struIndex[2].IndexMinValue;
                                    if (m_leaf_list.m_struIndex[3].bEnumIndex == true)//枚举类型
                                        indexValue[3] = (int)m_leaf_list.m_struIndex[3].pEnumIndexValue[index3];
                                    else
                                        indexValue[3] = index3 + (int)m_leaf_list.m_struIndex[3].IndexMinValue;
                                    if (m_leaf_list.m_struIndex[4].bEnumIndex == true)//枚举类型
                                        indexValue[4] = (int)m_leaf_list.m_struIndex[4].pEnumIndexValue[index4];
                                    else
                                        indexValue[4] = index4 + (int)m_leaf_list.m_struIndex[4].IndexMinValue;
                                    if (m_leaf_list.m_struIndex[5].bEnumIndex == true)//枚举类型
                                        indexValue[5] = (int)m_leaf_list.m_struIndex[5].pEnumIndexValue[index5];
                                    else
                                        indexValue[5] = index5 + (int)m_leaf_list.m_struIndex[5].IndexMinValue;
                                    string strInstantNum = "";
                                    if (TableDimen == 0){ 
                                        strInstantNum = "." + "0";//标量
                                    }
                                    else
                                    {
                                        for (int i = 0; i < TableDimen; i++)
                                        {
                                            string tempIndexNum;
                                            if (isDyTable == true)//modify by yangyuming 增加对动态表的处理，动态表索引全部置成0 2012.3.23
                                            {
                                                strInstantNum = strInstantNum + "." + "0";
                                                indexValue[i] = 0;
                                            }
                                            else{
                                                strInstantNum = strInstantNum + "." + String.Format("{0}", indexValue[i]);
                                            }
                                        }
                                    }
                                    byte[] pBuff = new byte[bufLens];
                                    List<byte[]> BuffArrL = new List<byte[]>() { new byte[bufLens] };
                                    List<int> bytePosL = new List<int>() { 0 };
                                    if (TableDimen > 0){
                                        RecordIndexInstance(BuffArrL, 0, TableDimen, indexValue, tableOp, bytePosL);
                                    }
                                    for (int k = TableDimen; k < tableOp.m_LeafNodes.Count; k++)
                                    {
                                        string defaultVal;
                                        if (isDyTable == true) {//modify by yangyuming 增加对动态表的处理，动态表内容全部置成0 2012.3.23
                                            defaultVal = "";
                                        }
                                        else{
                                            defaultVal = m_leaf_list.m_struIndex[k].strDefaultValue;
                                        }
                                        string strTmp = m_leaf_list.m_struIndex[k].strIndexOMType;
                                        string asnType = m_leaf_list.m_struIndex[k].asnType;
                                        int typeSize = m_leaf_list.m_struIndex[k].typeSize;
                                        WriteToBuffer(BuffArrL, defaultVal, bytePosL, strTmp, typeSize, "", m_leaf_list.m_struIndex[k].asnType);
                                    }
                                    tableOp.m_cfgInsts_add(strInstantNum, BuffArrL[0]);
                                    nTableRecord++;
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 实例 ：索引相关的数据值
        /// </summary>
        /// <param name="pBuff"></param>
        /// <param name="StartNo"></param>
        /// <param name="TableDimen"></param>
        /// <param name="indexValue"></param>
        /// <param name="tableOp"></param>
        /// <param name="bytePosL"></param>
        private void RecordIndexInstance(List<byte[]> pBuff, int StartNo,int TableDimen, int[] indexValue, CfgTableOp tableOp, List<int> bytePosL)//struIndexInfoCFG* pStruIndexInfo,
        {
            string defaultVal = "";
            for (int i = StartNo; i < TableDimen; i++)
            {
                //defaultVal += indexValue[i].ToString();//=0//defaultVal.Format("%d", indexValue[i]);
                defaultVal = indexValue[i].ToString();
                string strIndexOMType = tableOp.m_LeafNodes[i].m_struMibNode.strOMType;// pStruIndexInfo[i].strIndexOMType;
                string strValList = "";
                string strAsnType = "";
                WriteToBuffer(pBuff, defaultVal, bytePosL, strIndexOMType, 0, strValList, strAsnType);
            }
        }
        /// <summary>
        /// 重要的数据填写函数
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="value"></param>
        /// <param name="bytePosL"></param>
        /// <param name="OMType"></param>
        /// <param name="strLen"></param>
        /// <param name="strValList"></param>
        /// <param name="strAsnType"></param>
        public void WriteToBuffer(List<byte[]> byteArray, string value, List<int> bytePosL, string OMType, int strLen, string strValList, string strAsnType)
        {
            int posStartF = bytePosL[0];
            if (null == byteArray)
                return;
            else if (String.Empty == value.Trim(' '))
            {
                SetValueToByteArray(byteArray, bytePosL, (uint)0);
                return;
            }
            if (OMType == "s32" || (OMType == "u32"))    //s32,u32
            {
                uint omValue = 0;// 返回类型已经确定
                if (String.Compare(strAsnType, "Bits", true) == 0)
                {
                    if (value.Contains("×") != false)
                        omValue = (uint)0;
                    else
                        GetBitsTypeValueFromValue(value, strValList, out omValue);
                }
                else if ((String.Compare(strAsnType, "Integer32", true) == 0)&& value.IndexOf(":") >0)
                {
                    omValue = (uint)getEnumValue(value);
                }
                else if ((String.Compare(strAsnType, "INTEGER", true) == 0) && value.IndexOf(":") > 0)
                {
                    omValue = (uint)getEnumValue(value);
                }
                else
                {
                    if (value.Contains("0x") != false)
                        omValue = (uint)Convert.ToInt32(value, 16);
                    else if (value.Contains("×") != false)
                        omValue = (uint)0;
                    else
                    {
                        omValue = (uint)long.Parse(value);//4294967295 必须用long.Parse
                    }
                }
                if (OMType == "s32")
                    SetValueToByteArray(byteArray, bytePosL, (int)omValue);
                else if (OMType == "u32")
                    SetValueToByteArray(byteArray, bytePosL, (uint)omValue);
            }
            else if (OMType == "u32[]")
            {
                int posStart = bytePosL[0];
                if (value.Contains("×") != false)
                {
                    value = "0";
                    //bytePosL[0] += strLen;
                }
                //else松懈
                getInt32Array(byteArray, bytePosL, value);//分级调用 SetValueToByteArray
                if (bytePosL[0] - posStart < strLen)
                    bytePosL[0] = posStart + strLen;
            }
            else if (OMType == "enum")   //enum,转换成u32
            {
                if (("×" == value))
                    value = "0";
                SetValueToByteArray(byteArray, bytePosL, (uint)getEnumValue(value));
            }
            else if (OMType == "s32[]")   //Oid
            {
                if (("×" == value))
                    value = "0";
                OM_OBJ_ID_T tmpOID = new OM_OBJ_ID_T(value);// OM_OBJ_ID_T(value);
                SetValueToByteArray(byteArray, bytePosL, tmpOID.StruToByteArray());
            }
            else if (OMType == "s8[]")   //char *
            {
                value = value.Replace("\"", "");//value = value.Trim("\"");
                if (("×" == value))
                    value = "";
                sbyte[] sbArray = (sbyte[])((Array)System.Text.Encoding.Default.GetBytes(value));
                //byte[] mybyte = Encoding.Unicode.GetBytes(value);
                byte[] mybyte2 = Encoding.UTF8.GetBytes(value);
                //string valuess = Encoding.GetEncoding("GB2312").GetString(mybyte2).TrimEnd('\0');
                SetValueToByteArray(byteArray, bytePosL, sbArray);//ASSERT(strLen >= strTmp.GetLength());char* test = (char*)(memcpy(pBuff + pos, strTmp.GetBuffer(), strTmp.GetLength()));
                if (mybyte2.Length < strLen)
                    bytePosL[0] += strLen - mybyte2.Length;

            }
            else if (OMType == "s8")   //char *
            {
                int omValue = 0;
                if (value.Contains("0x") != false)
                    omValue = (int)Convert.ToInt32(value, 16);
                else if (("×" == value))
                    omValue = 0;
                else
                    omValue = (int)int.Parse(value);
                byte[] TypeToByteArr = BitConverter.GetBytes((int)omValue);
                SetValueToByteArray(byteArray, bytePosL, TypeToByteArr[0]);
            }
            else if (OMType == "u8")
            {
                uint omValue = 0;
                if (value.Contains("0x") != false)
                    omValue = (uint)Convert.ToInt32(value, 16);
                else if (("×" == value))
                    omValue = 0;
                else
                    omValue = (uint)int.Parse(value);
                byte[] TypeToByteArr = BitConverter.GetBytes((ushort)omValue);
                SetValueToByteArray(byteArray, bytePosL, TypeToByteArr[0]);
            }
            else if (OMType == "u8[]")
            {
                if (0 == String.Compare("IpAddress", strAsnType, true))//sizeof(ipv4addr)
                {
                    uint IPAddr = getIPAddr(value);
                    SetValueToByteArray(byteArray, bytePosL, IPAddr);
                }
                else if (String.Compare(strAsnType, "InetAddress", true) == 0)
                {
                    int posStart = bytePosL[0];
                    value = value.Replace("\"", ""); //value.Trim("\"");
                    GetInetAddressValue(byteArray, bytePosL, value);
                    if(bytePosL[0] - posStart < strLen)
                        bytePosL[0] = posStart + strLen;
                }
                else if (String.Compare(strAsnType, "MacAddress", true) == 0)
                {
                    getMacAddr(byteArray, bytePosL, value);
                }
                else if (String.Compare(strAsnType, "MncMccType", true) == 0)
                {
                    GetMncMccTypeValue(byteArray, bytePosL, value);
                }
                else     //其他的u8[] 2009-12-21
                {
                    value = value.Replace("\"", "");
                    if (("×" == value))
                        value = "0";
                    if (String.Compare(strAsnType, "DateAndTime", true) == 0)
                        GetDateAndTimeTypeValue(byteArray, bytePosL, value);
                    else
                        SetValueToByteArray(byteArray, bytePosL, value);
                }
            }
            else if (OMType == "u16")   //u16,s16
            {
                uint omValue = 0;
                if (value.Contains("0x") != false)
                    omValue = (uint)Convert.ToInt32(value, 16);
                else if(("×" == value))
                    omValue = 0;
                else
                    omValue = (uint)int.Parse(value);
                SetValueToByteArray(byteArray, bytePosL, (ushort)omValue);
            }
            else if (OMType == "s16")   //u16,s16
            {
                uint omValue = 0;
                if (value.Contains("0x") != false)
                    omValue = (uint)Convert.ToInt32(value, 16);
                else if (("×" == value))
                    omValue = 0;
                else
                    omValue = (uint)int.Parse(value);
                SetValueToByteArray(byteArray, bytePosL, (short)omValue);
            }
            else
            {
                uint omValue = 0;
                if (value.Contains("0x") != false)
                    omValue = (uint)Convert.ToInt32(value, 16);
                else
                    omValue = (uint)int.Parse(value);
                SetValueToByteArray(byteArray, bytePosL, omValue);
            }

            if (bytePosL[0] - posStartF < strLen)
                bytePosL[0] = posStartF + strLen;
        }
        /// <summary>
        /// 类型转换为byte[]
        /// </summary>
        /// <param name="byteAL"></param>
        /// <param name="bytePosL"></param>
        /// <param name="objParm"></param>
        private void SetValueToByteArray(List<byte[]> byteAL, List<int> bytePosL, object objParm)
        {
            if (objParm is byte)
            {
                byteAL[0][bytePosL[0]] = (byte)objParm;
                bytePosL[0] += Marshal.SizeOf(objParm);
            }
            else if (objParm is byte[])
            {
                Buffer.BlockCopy((byte[])objParm, 0, byteAL[0], bytePosL[0], ((byte[])objParm).Length);
                bytePosL[0] += ((byte[])objParm).Length;
            }
            else if (objParm is sbyte[])
            {
                Buffer.BlockCopy((sbyte[])objParm, 0, byteAL[0], bytePosL[0], ((sbyte[])objParm).Length);
                bytePosL[0] += ((sbyte[])objParm).Length;
            }
            else if (objParm is ushort)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((ushort)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is short)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((short)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((uint)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is int)
            {
                byte[] TypeToByteArr = BitConverter.GetBytes((int)objParm); //  数据块起始位置 
                Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                bytePosL[0] += TypeToByteArr.Length;
            }
            else if (objParm is uint[])
            {
                foreach (var ui in (uint[])objParm)
                {
                    byte[] TypeToByteArr = BitConverter.GetBytes((uint)ui); //  数据块起始位置 
                    Buffer.BlockCopy(TypeToByteArr, 0, byteAL[0], bytePosL[0], TypeToByteArr.Length);
                    bytePosL[0] += TypeToByteArr.Length;
                }
            }
            else
            {
                Console.WriteLine(String.Format("SetValueToByteArray : new type : value={0}, type={1}", objParm.ToString(), objParm.GetType()));
            }
        }
        private void getInt32Array(List<byte[]> byteArray,  List<int> bytePosL, string strInput)//void getInt32Array(string strInput, byte[] pszOut)
        {
            string strCurrentValue = strInput.Trim('{').Trim('}');
            int arrayNum = new StruCfgFileFieldInfo().GetDeckNum(strCurrentValue, ",");// GetDeckNum(strCurrentValue, ',');
            SetValueToByteArray(byteArray, bytePosL, (int)arrayNum);//memcpy(pszOut, &arrayNum, sizeof(int));//nBytesNum += 4;
            int iPos = 0;// strCurrentValue.IndexOf(',');
            if ((iPos = strCurrentValue.IndexOf(',')) > 0)
            {
                while ((iPos = strCurrentValue.IndexOf(',')) > 0)
                {
                    string strTempValue = strCurrentValue.Substring(0, iPos);
                    uint nVlaue = (uint)int.Parse(strTempValue);
                    SetValueToByteArray(byteArray, bytePosL, nVlaue);//memcpy(pszOut + nBytesNum, &nVlaue, sizeof(u32));//nBytesNum = nBytesNum + 4;
                    strCurrentValue = strCurrentValue.Substring(iPos + 1) + ",";//strCurrentValue = strPostValue;//strCurrentValue += ",";
                }
            }
            else if (strCurrentValue != "")
            {
                uint nVlaue = 0;
                string strTempValue = strCurrentValue;
                nVlaue = (uint)int.Parse(strTempValue);
                SetValueToByteArray(byteArray, bytePosL, nVlaue);//memcpy(pszOut + nBytesNum, &nVlaue, sizeof(u32));
            }
            return ;
        }
        private bool GetBitsTypeValueFromValue(string strInputValue, string strValList, out uint outValue)
        {
            if (strValList == "")
            {
                outValue = (uint)int.Parse(strInputValue);// atoi(strInputValue);
                return true;
            }
            byte[] IDS_STR_BITS_INVALID = new byte[] { 0x1, 0x64, 0x33 };//#define IDS_STR_BITS_INVALID  16433
            string BitsInvalidCompare = Encoding.Default.GetString(IDS_STR_BITS_INVALID);
            if (String.Empty == strInputValue || String.Compare(BitsInvalidCompare, strInputValue, true) == 0)
            {
                outValue = 0;
                return true;
            }
            Dictionary<string, int> mapDesc2Value = new Dictionary<string, int>();
            List<uint> setOutValue = new List<uint>();
            string strValList1 = strValList;
            string strEach = GetNextDeck(strValList1, "/", out strValList1);//
            while (String.Empty != strEach)//false == strEach.IsEmpty())
            {
                int npos = strEach.IndexOf(':');
                if (npos != -1)
                {
                    string strDesc = strEach.Substring(0, npos);
                    string strDdd = strEach.Substring(npos+1);
                    mapDesc2Value.Add(strDdd, int.Parse(strDesc));
                }
                strEach = GetNextDeck(strValList1, "/", out strValList1);//
            }

            //
            string strInputValue1 = strInputValue;
            strEach = GetNextDeck(strInputValue1, "/", out strInputValue1);// '/');
            while (String.Empty != strEach)
            {
                int npos = strEach.IndexOf(':');
                string strCurValueDesc;
                if (npos != -1)
                {
                    strCurValueDesc = strEach.Substring(npos + 1);
                }
                else
                {
                    strCurValueDesc = strEach;
                }
                // mapDesc2Value 主要给 rru excel 中的赋值方式使用
                // mib 中的赋值，直接使用了bits变int得方式：例如(int)7=(2进制)0111，即第0,1,2位有效的意思。
                if (!mapDesc2Value.ContainsKey(strCurValueDesc))// 不存在
                {
                    outValue = uint.Parse(strCurValueDesc);//"参数取值列表中不存在%s"
                    return false;
                }
                setOutValue.Add((uint)(mapDesc2Value[strCurValueDesc]));
                strEach = GetNextDeck(strInputValue1, "/", out strInputValue1);// '/');
            }
            outValue = 0;
            for (int i =0; i< setOutValue.Count;i++ )
            {
                int a = (int)setOutValue[i];
                int addValue = 1 << a;// (setOutValue[i]);
                outValue |= (uint)addValue;
            }
            return true;
        }
        private string GetNextDeck(string strSrc,  string cSeparator, out string str1)
        {
            str1 = "";
            string strResult = "";
            int iPos = -1;
            iPos = strSrc.IndexOf(cSeparator);// 位置从0开始，-1为没找到
	        while(iPos == 0)
	        {
                string strTemp = strSrc.Remove(0, 1);  // or str=str.Substring(i); 
                string strTemp2 = strSrc.Substring(1);  // or str=str.Substring(i); 
                strSrc = strTemp;
		        iPos = strSrc.IndexOf(cSeparator);
	        }
	        if (iPos >= 0)
	        {
                strResult = strSrc.Substring(0, iPos);
                str1 = "";
                if (iPos<strSrc.Length - 1)
		        {
                    str1 = strSrc.Substring(iPos + 1);//strTemp = strSrc.Mid(iPos+1);
                }
            }
	        else
	        {
		        strResult = strSrc;
                str1 = "";
	        }
	        return strResult;
        }
        private int getEnumValue(string inputStr)
        {
            inputStr = inputStr.Trim(' ');
            if (String.Empty == inputStr)
                return 0;
            string csInputValue = "";
            int index = inputStr.IndexOf(":");
	        if (index>0)  //如果含有枚举注释
		        csInputValue = inputStr.Substring(0,index);
	        else       //只有值的情况
		        csInputValue = inputStr;
            return int.Parse(csInputValue);
        }
        private uint getIPAddr(string ipAddr)
        {
            System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(ipAddr);
            long dreamduip = ipaddress.Address;//转换为 90 03 a8 c0
            long x = dreamduip;
            long aaa =  ((((x) & 0x000000ff) << 24) | (((x) & 0x0000ff00) << 8) | (((x) & 0x00ff0000) >> 8) | (((x) & 0xff000000) >> 24));
            return (uint)aaa;// 返回 c0.a8.03.90
        }
        private void getMacAddr(List<byte[]> byteArray, List<int> bytePosL, string inputStr)//const CString &inputStr,unsigned char* phyArray)
        {
            string subStr = inputStr;
            int nVlaue = 0;
            byte[] TypeToByteArr = null;
            byte[] macAddr = new byte[7];// = { 0 };
            List<byte[]> byteArrayNew = new List<byte[]>() { macAddr };
            int bytePos = 0;
            List<int> bytePosLNew = new List<int>() { bytePos };
            int pos = subStr.IndexOf(':');
            while (pos != -1)
            {
                nVlaue = 0;
                TypeToByteArr = BitConverter.GetBytes((ushort)nVlaue);
                SetValueToByteArray(byteArrayNew, bytePosLNew, TypeToByteArr[0]);//memcpy(phyArray++, &nVlaue, sizeof(char));
                string strTmp = subStr.Substring(pos + 1);// Mid(pos + 1);
                subStr = strTmp;
                pos = subStr.IndexOf(':');
            }
            nVlaue = 0;
            TypeToByteArr = BitConverter.GetBytes((ushort)nVlaue);
            SetValueToByteArray(byteArrayNew, bytePosLNew, TypeToByteArr[0]);//memcpy(phyArray, &nVlaue, sizeof(char));
            Buffer.BlockCopy((byte[])byteArrayNew[0], 0, byteArray[0], bytePosL[0], 6);
        }
        private void GetInetAddressValue(List<byte[]> byteArray, List<int> bytePosL, string strInput)//unsigned char* pszOut)
        {
            int ipos = strInput.IndexOf('.');
            int jpos = strInput.IndexOf(':');
            byte[] TypeToByteArr = null;
            if (ipos > 0) //ipv4
            {
                string strTempIPVaue = strInput;
                while (ipos > 0)
                {
                    string strTempValue = GetNextDeck(strTempIPVaue, ".", out strTempIPVaue);
                    int nVlaue = int.Parse(strTempValue);
                    TypeToByteArr = BitConverter.GetBytes((ushort)nVlaue);
                    SetValueToByteArray(byteArray, bytePosL, TypeToByteArr[0]);//memcpy(pszOut++, &nVlaue, sizeof(char));
                    ipos = strTempIPVaue.IndexOf('.');
                }
                int intLastValue = int.Parse(strTempIPVaue);
                TypeToByteArr = BitConverter.GetBytes((ushort)intLastValue);
                SetValueToByteArray(byteArray, bytePosL, TypeToByteArr[0]);//memcpy(pszOut++, &intLastValue, sizeof(char));
            }
            else if (jpos > 0)//iPV6
            {
                string strIpV6 = "";
                string strTempIp = strInput;
                while (jpos > 0)
                {
                    string strTempValue = GetNextDeck(strTempIp, ":", out strTempIp);
                    strIpV6 = strIpV6 + strTempValue;
                    jpos = strTempValue.IndexOf(':');
                }
                strIpV6 = strIpV6 + strTempIp;
                for (int i = 0; i < strIpV6.Length; i = i + 2)
                {
                    string strTemp = strInput.Substring(i, 2);// strInput.Mid(i, 2);
                    int nVlaue = 0;
                    TypeToByteArr = BitConverter.GetBytes((ushort)nVlaue);
                    SetValueToByteArray(byteArray, bytePosL, TypeToByteArr[0]);//memcpy(pszOut++, &nVlaue, sizeof(char));
                }
            }
        }
        private void GetMncMccTypeValue(List<byte[]> byteArray, List<int> bytePosL, string strInput)//CString strInput, unsigned char* pszOut)
        {
            if (strInput == "")
                return;
            byte[] MncMcc = new byte[4];// = { 0 };
            List<byte[]> byteArrayNew = new List<byte[]>() { MncMcc };
            int bytePos = 0;
            List<int> bytePosLNew = new List<int>() { bytePos };
            byte[] TypeToByteArr = null;
            if (strInput.Length < 3)
            {
                for (int i = 0; i < strInput.Length; i++)
                {
                    string strTempValue = strInput[i].ToString();//strInput.Substring(i, i + 1);// Mid(i, i + 1);
                    int nVlaue = int.Parse(strTempValue);// atoi(strTempValue);
                    TypeToByteArr = BitConverter.GetBytes((ushort)nVlaue);
                    SetValueToByteArray(byteArrayNew, bytePosLNew, TypeToByteArr[0]);//memcpy(pszOut++, &nVlaue, sizeof(char));
                }
                // 最后一位字节补位: 0xFF
                int defauleValue = 255;
                TypeToByteArr = BitConverter.GetBytes((ushort)defauleValue);
                //byte decBytes = Convert.ToByte("255", 16);
                SetValueToByteArray(byteArrayNew, bytePosLNew, TypeToByteArr[0]); //memcpy(pszOut++, &defauleValue, sizeof(char));
            }
            else
            {
                for (int i = 0; i < strInput.Length; i++)
                {
                    string strTempValue = strInput[i].ToString();// strInput.Substring(i, 1);// Mid(i, 1);
                    int nVlaue = int.Parse(strTempValue);//atoi(strTempValue);
                    TypeToByteArr = BitConverter.GetBytes((ushort)nVlaue);
                    //byte decBytes = Convert.ToByte(strTempValue, 16);
                    SetValueToByteArray(byteArrayNew, bytePosLNew, TypeToByteArr[0]);//memcpy(pszOut++, &nVlaue, sizeof(char));
                }
            }
            if (strInput.Length < 2)
            {
                Console.WriteLine(String.Format("{0} len = {1} is to shorter than 3.", strInput, strInput.Length));
            }
            Buffer.BlockCopy((byte[])byteArrayNew[0], 0, byteArray[0], bytePosL[0], 3);
            bytePosL[0] += 3;
            return;
        }
        private void GetDateAndTimeTypeValue(List<byte[]> byteArray, List<int> bytePosL, string strInput)
        {
            if (!CheckDateTimeFormat(strInput))// 判断用户输入的时间格式是否合法
                return ;
            byte[] TypeToByteArr = null;
            SetValueToByteArray(byteArray, bytePosL, (ushort)int.Parse(strInput.Substring(0, 4)));//year
            for (int i = 1; i < 6; i++)//i=1~5  month/day hour/minute/second
            {
                TypeToByteArr = BitConverter.GetBytes((ushort)int.Parse(strInput.Substring(2+3*i, 2)));
                SetValueToByteArray(byteArray, bytePosL, TypeToByteArr[0]);
            }
        }
        private bool CheckDateTimeFormat(string InStr)//const CString& InStr)
        {
            if (InStr.Length < 19)
                return false;
            if ((InStr[4] != '-') || (InStr[7] != '-') || (InStr[10] != ' ') || (InStr[13] != ':') || (InStr[16] != ':'))
                return false;
            if (!IsValidateDate(InStr.Substring(0, 4), InStr.Substring(5, 2), InStr.Substring(8, 2)))// 年月日是否有效
                return false;
            if (!IsValidateTime(InStr.Substring(11, 2), InStr.Substring(14, 2), InStr.Substring(17, 2)))// 24小时格式是否有效
                return false;
            return true;
        }
        private bool IsNumeric(string InStr)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (rex.IsMatch(InStr))
                return true;
            return false;
        }
        private bool IsValidateDate(string year, string month, string day)
        {
            if ((false == IsNumeric(year)) || (false == IsNumeric(month)) || (false == IsNumeric(day)) )
                return false;
            int y = int.Parse(year);
            int m = int.Parse(month);
            int d = int.Parse(day);
            int[] a = { 31, (y % 4 == 0 && y % 100 != 0 || y % 400 == 0) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            return m >= 1 && m <= 12 && d >= 1 && d <= a[m - 1];
        }
        private bool IsValidateTime(string hour, string minute, string second)
        {
            if ((false == IsNumeric(hour)) || (false == IsNumeric(minute)) || (false == IsNumeric(second)))
                return false;
            int h = int.Parse(hour);
            int m = int.Parse(minute);
            int s = int.Parse(second);
            return h >= 0 && h <= 23 && m >= 0 && m <= 59 && s >= 0 && s <= 59;
        }
        public bool TestSaveFile_eNB(string newFilePath = "", string oldFilePath = "", bool NewCfgFile = false)
        {
            FileStream fs = new FileStream(newFilePath, FileMode.Create, FileAccess.Write);//找到文件如果文件不存在则创建文件如果存在则覆盖文件
            fs.SetLength(0);   //清空文件
            return true;
        }
        public void TestsaveFileTest(string newFilePath)
        {
            WriteHeaderVersionInfo("");
            Console.WriteLine(System.Runtime.InteropServices.Marshal.SizeOf(m_cfgFile_Header));
            byte[] byteList = m_cfgFile_Header.StruToByteArray();
            TestWriteFile(newFilePath, byteList, 0);
        }
        public void TestWriteFile(string filepath, byte[] data, int offset)
        {
            FileStream fs = new FileStream(filepath, FileMode.Create);
            fs.Seek(offset, SeekOrigin.End);// 偏移的位置
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
        }

        public void CfgWriteFile(string filepath, byte[] data, int offset)
        {
            FileStream fs = new FileStream(filepath, FileMode.Create);
            fs.Seek(offset, SeekOrigin.End);// 偏移的位置
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
        }
        public byte[] CfgReadFile(string filePath, long readFromOffset, int readCount)
        {
            //byte[] byData = new byte[100];
            //char[] charData = new char[1000];
            byte[] byData = new byte[readCount];
            char[] charData = new char[readCount];
            try
            {
                //FileStream sFile = new FileStream("文件路径", FileMode.Open);
                FileStream sFile = new FileStream(filePath, FileMode.Open);
                //sFile.Seek(55, SeekOrigin.Begin);
                //sFile.Read(byData, 0, 100);
                sFile.Seek(readFromOffset, SeekOrigin.Begin);
                sFile.Read(byData, 0, readCount);//第一个参数是被传进来的字节数组,用以接受FileStream对象中的数据,第2个参数是字节数组中开始写入数据的位置,它通常是0,表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.

                sFile.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine("An IO exception has been thrown!");
                Console.WriteLine(e.ToString());
                Console.ReadLine();
                return null;
            }
            Decoder d = Encoding.UTF8.GetDecoder();
            d.GetChars(byData, 0, byData.Length, charData, 0);
            //Console.WriteLine(charData);
            //Console.ReadLine();
            return byData;
        }
        public void TestsetCfgFile_Header()
        {
            StruCfgFileHeader a = new StruCfgFileHeader("init");
            a.u32IcFileVer = 12;
            Console.WriteLine(System.Runtime.InteropServices.Marshal.SizeOf(a));
        }

        public static T DeepCopy<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(T));
                ser.WriteObject(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = ser.ReadObject(ms);
                ms.Close();
            }
            return (T)retval;
        }


    }
}
