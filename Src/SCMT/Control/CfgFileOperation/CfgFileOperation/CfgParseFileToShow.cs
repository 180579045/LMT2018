using CfgFileOpStruct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CfgFileOperation
{
    /// <summary>
    /// cfg文件解析后可以呈现给前台，并且可以再次生成文件
    /// </summary>
    class CfgParseFileToShow
    {
        #region 数据
        public CfgParseFileToShow()
        {
            m_mapTableInfo = new Dictionary<string, CfgTableOp>();
        }
        /// <summary>
        /// 1. Cfg文件头结构
        /// </summary>
        StruCfgFileHeader m_cfgFile_Header;                   // 1. Cfg文件头结构
        /// <summary>
        /// 2. 数据块的头 ，为将来堆叠准备
        /// </summary>
        StruDataHead m_dataHeadInfo;                          // 2. 数据块的头 ，为将来堆叠准备
        /// <summary>
        /// 4. 存每个表的内容
        /// </summary>
        public Dictionary<string, CfgTableOp> m_mapTableInfo; // 4. 存每个表的内容
        #endregion

        #region 为前台显示解析文件
        /// <summary>
        /// 把文件解析成结构体
        /// </summary>
        /// <param name="m_filePath"></param>
        public void LoadFileToMemory(string m_filePath)
        {
            bool bBig = IsBigDuan(m_filePath);//是否是大端
            if (bBig)//大端解析
            {
                // 1. 文件头
                m_cfgFile_Header = GetStruCfgFileHeaderInfoByBigEndian(m_filePath);
            }
            else//小端解析
            {

            }
            
            
            // 2. 数据块头
            m_dataHeadInfo   = GetDataHeadFromFile(m_filePath);
            // 3. 每个表的偏移量
            List<uint> TablesOffsetPos = GetTablesOffsetPos(m_filePath, (int)m_dataHeadInfo.u32TableCnt);

            // 4. 处理每张表：所有表的内容(实例、叶子节点等信息)
            foreach (var iTablePos in TablesOffsetPos)
            {
                CfgTableOp table = GetTableInfoFromFile(m_filePath, iTablePos);
                m_mapTableInfo.Add(table.m_strTableName, table);// 保存表信息
            }
        }
        /// <summary>
        /// 是否是大端
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        bool IsBigDuan(string filePath)
        {
            // 大端模式：是指数据的高字节保存在内存的低地址中
            // 小端模式：是指数据的高字节保存在内存的高地址中
            // 例如 700 = 0x02bc,其中 0x02是高字节, 0xbc是低字节
            byte[] bData =CfgReadFile(filePath, 6, 2);
            byte diByte = bData[0];
            if (diByte == 2)
            {
                return true;//大端
            }
            return false;//小端
        }
        /// <summary>
        /// 解析文件头
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private StruCfgFileHeader GetStruCfgFileHeaderInfo(string filePath)
        {
            int offset = 956;
            byte[] Ysdata = CfgReadFile(filePath, 0, offset);
            StruCfgFileHeader YsFileHead = new StruCfgFileHeader("init");
            //YsFileHead.IsBigDuan(Ysdata);
            YsFileHead.ParseFileReadBytes(Ysdata);
            return YsFileHead;
        }
        private StruCfgFileHeader GetStruCfgFileHeaderInfoByBigEndian(string filePath)
        {
            int offset = 956;
            byte[] Ysdata = CfgReadFile(filePath, 0, offset);
            StruCfgFileHeader YsFileHead = new StruCfgFileHeader("init");
            //YsFileHead.IsBigDuan(Ysdata);
            YsFileHead.ParseFileReadBytesByBigEndian(Ysdata);
            return YsFileHead;
        }
        /// <summary>
        /// 获取 数据块 的头
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private StruDataHead GetDataHeadFromFile(string filePath)
        {
            int offset = 956;
            int readCount = 24;
            byte[] Ysdata = CfgReadFile(filePath, offset, readCount);
            StruDataHead ysD = new StruDataHead("");
            ysD.SetValueByBytes(Ysdata);
            return ysD;
        }
        /// <summary>
        /// 获取每个表的偏移量;OM_STRU_IcfIdxTableItem;4字节*表数
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private List<uint> GetTablesOffsetPos(string filePath, int iTableCounts)
        {
            List<uint> TablesPos = new List<uint>();
            byte[] Ysdata;
            // 偏移块
            for (int i = 0; i < iTableCounts; i++)
            {
                Ysdata = CfgReadFile(filePath, (956 + 24 + 4 * i), 4);
                TablesPos.Add(GetBytesValToUint(Ysdata));
            }
            Ysdata = null;
            return TablesPos;
        }
        /// <summary>
        /// 根据偏移位置获取表的内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="iTablePos">偏移位置</param>
        /// <returns></returns>
        private CfgTableOp GetTableInfoFromFile(string filePath, uint iTablePos)
        {
            // 4.1 表的信息: 44 字节
            StruCfgFileTblInfo tblInfo = GetTableHeadInfo(filePath, iTablePos);
            string tableName = Encoding.GetEncoding("GB2312").GetString(tblInfo.u8TblName).TrimEnd('\0');
            // 4.2 叶子节点信息: 60字节 * 叶子个数
            List<StruCfgFileFieldInfo> leafsHead = GetLeafHeadFieldInfo(filePath, tblInfo.u16FieldNum, iTablePos);
            // 4.3. 每个表的实例  解析成索引和实例的模式
            List<byte[]> YsInsts = GetInstsData(filePath, iTablePos, (int)tblInfo.u32RecNum, (int)tblInfo.u16FieldNum, tblInfo.u16RecLen);

            // 保存表信息
            CfgTableOp table = new CfgTableOp();
            table.m_cfgFile_TblInfo = tblInfo;   // 4.1 表的信息: 44 字节
            table.m_strTableName = tableName;    // 表名
            foreach (var fileField in leafsHead)
            {// 4.2 叶子节点信息
                table.m_LeafNodes.Add(new CfgFileLeafNodeOp(fileField));
            }
            table.m_cfgInsts = GetCfgInsts(YsInsts, leafsHead); // 4.3. 每个表的实例
            table.SetTableOffset(iTablePos);                    // 设置偏移 init.cfg
            table.m_tabDimen = GetIndexNodeNum(leafsHead);      // 索引的个数
            return table;
        }
        /// <summary>
        /// 获取每个表的头
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private StruCfgFileTblInfo GetTableHeadInfo(string filePath, uint offset)
        {
            // 跳过 偏移量 获取tableInfo表块介绍
            StruCfgFileTblInfo ysTblInfo = new StruCfgFileTblInfo("");
            int readCount = Marshal.SizeOf(new StruCfgFileTblInfo(""));// 44字节
            byte[] Ysdata = CfgReadFile(filePath, offset, readCount);
            ysTblInfo.SetBytesValue(Ysdata);
            return ysTblInfo;
        }
        /// <summary>
        /// 获取所有叶子节点的叶子节点头
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="u16FieldNumYs"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        private List<StruCfgFileFieldInfo> GetLeafHeadFieldInfo(string filePath, ushort u16FieldNumYs, uint offset)
        {
            int readCount = 0;
            byte[] Ysdata;
            List<StruCfgFileFieldInfo> leafFieldL = new List<StruCfgFileFieldInfo>();
            // 从文件中获取
            for (int i = 0; i < u16FieldNumYs; i++)
            {
                StruCfgFileFieldInfo leafFieldInfo = new StruCfgFileFieldInfo("");
                uint fristLeafFieldInfoPos = offset +
                    (uint)Marshal.SizeOf(new StruCfgFileTblInfo("")) +
                    (uint)Marshal.SizeOf(new StruCfgFileFieldInfo("")) * (uint)i;// 第一个叶子头的位置
                readCount = Marshal.SizeOf(new StruCfgFileFieldInfo(""));// 60
                Ysdata = CfgReadFile(filePath, fristLeafFieldInfoPos, readCount);
                leafFieldInfo.SetValueByBytes(Ysdata);
                //string leafName = Encoding.GetEncoding("GB2312").GetString(leafFieldInfo.Getu8FieldName()).TrimEnd('\0');
                leafFieldL.Add(leafFieldInfo);
            }
            return leafFieldL;
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
        private List<byte[]> GetInstsData(string filePath, uint offset, int u32RecNum, int u16FieldNum, int u16FieldLen)
        {
            List<CfgTableInstanceInfos> m_cfgInsts = new List<CfgTableInstanceInfos>();
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
        /// 同一张表的实例 : 解析成索引和实例形式
        /// </summary>
        /// <param name="YsInsts"></param>
        /// <param name="LeafsHead"></param>
        /// <returns></returns>
        private List<CfgTableInstanceInfos> GetCfgInsts(List<byte[]> YsInsts, List<StruCfgFileFieldInfo> LeafsHead)
        {
            List<CfgTableInstanceInfos> m_cfgInsts = new List<CfgTableInstanceInfos>();
            string strIndex = "";
            ushort u16FieldOffset;       /* 字段相对记录头偏移量*/
            ushort u16FieldLen;          /* 字段长度（"MIBVal_AllList"的长度） 单位：字节 */
            string strFieldTag;          /* 字段是否为关键字 : 是否为索引 'Y' or 'N'*/
            foreach (var inst in YsInsts)
            {
                strIndex = "";
                foreach (var leaf in LeafsHead)
                {
                    u16FieldOffset = leaf.u16FieldOffset;       
                    u16FieldLen = leaf.u16FieldLen;             
                    strFieldTag = leaf.u8FieldTag.ToString();
                    // 组合索引
                    if (0 == string.Compare(strFieldTag, "Y", true))
                    {
                        byte[] indexBytes = inst.Skip(u16FieldOffset).Take(u16FieldLen).ToArray();
                        uint uIndexNum = GetBytesValToUint(indexBytes);
                        strIndex += ("." + uIndexNum.ToString());
                    }
                }
                if ("" == strIndex)// 0维索引：即标量表
                    strIndex = ".0";
                m_cfgInsts.Add(new CfgTableInstanceInfos(strIndex, inst));
            }
            return m_cfgInsts;
        }
        /// <summary>
        /// 获取索引节点的个数
        /// </summary>
        /// <param name="LeafsHead"></param>
        /// <returns></returns>
        private int GetIndexNodeNum(List<StruCfgFileFieldInfo> LeafsHead)
        {
            int iIndexNum = 0;
            string strFieldTag = "";
            foreach (var leaf in LeafsHead)
            {
                strFieldTag = leaf.u8FieldTag.ToString();
                if (0 == string.Compare(strFieldTag, "Y", true))
                {
                    iIndexNum += 1;
                }
            }
            return iIndexNum;
        }
        /// <summary>
        /// 读文件(从readFromOffset读readCount个字节)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="readFromOffset"></param>
        /// <param name="readCount"></param>
        /// <returns></returns>
        private byte[] CfgReadFile(string filePath, long readFromOffset, int readCount)
        {
            byte[] byData = new byte[readCount];
            char[] charData = new char[readCount];
            try
            {
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
            Decoder dec = Encoding.UTF8.GetDecoder();
            dec.GetChars(byData, 0, byData.Length, charData, 0);
            return byData;
        }
        /// <summary>
        /// 字符串转 uint 翻转
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private uint GetBytesValToUint(byte[] bytes)
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
        private uint GetBytesValToUint2(byte[] bytes)
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
        private string OxbytesToString(byte[] bytes)
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
        #endregion

        #region 再次生成文件
        public bool ReWriteFile(string strNewFile)
        {
            CreateFile();
            SaveFile(strNewFile);
            return true;
        }
        /// <summary>
        /// 写文件前的预处理
        /// </summary>
        /// <param name="newFilePath"></param>
        /// <param name="strDBPath"></param>
        /// <returns></returns>
        bool CreateFile()
        {
            //m_cfgFile_Header;// 文件头结构 复用
            //m_dataHeadInfo;  // 数据块的头 复用
            // 偏移头(1.固定偏移;2.表实例偏移)
            int m_tableNum = m_mapTableInfo.Count;
            // 1.固定偏移
            uint iFixedHeadOffset = 0;//固定头偏移 Fixed Head
            iFixedHeadOffset += (uint)Marshal.SizeOf(new StruCfgFileHeader(""));      //文件头：956 字节，
            iFixedHeadOffset += (ushort)Marshal.SizeOf(new StruDataHead("init"));     //数据头：24  字节， sizeof(DataHead);
            iFixedHeadOffset += (uint)m_tableNum * (uint)Marshal.SizeOf(new UInt32());//偏移头：4 * 表个数 字节，每个表的偏移量 
            // 2.表实例偏移
            foreach (var tabName in m_mapTableInfo.Keys.ToList())
            {
                iFixedHeadOffset = SetTableOffset(m_mapTableInfo[tabName], iFixedHeadOffset);//计算表的偏移量
            }
            return true;
        }
        /// <summary>
        /// 计算表的偏移位置 patch
        /// </summary>
        /// <param name="tableOp"></param>
        /// <param name="isDyTable"></param>
        /// <param name="TableOffset"></param>
        /// <returns></returns>
        uint SetTableOffset(CfgTableOp tableOp, uint TableOffset)
        {
            // 表的起始位置：是上一个表的结束位置
            tableOp.SetTableOffset(TableOffset);
            tableOp.SetTableOffsetPDG(TableOffset);
            // 表块：包含3部分
            // 1. 表头
            TableOffset += (uint)Marshal.SizeOf(new StruCfgFileTblInfo("init"));
            // 2. 所有叶子节点的头
            uint sizeofFile = (uint)Marshal.SizeOf(new StruCfgFileFieldInfo());
            string strTableName = tableOp.m_strTableName;
            var table = m_mapTableInfo[strTableName];
            
            uint ileafNum = (uint)table.m_LeafNodes.Count;
            TableOffset += sizeofFile * ileafNum;
            // 3. 实例内容 = 每个实例(所有叶子节点信息的长度) * 实例个数
            uint buflen = (uint)tableOp.m_cfgFile_TblInfo.u16RecLen;// 单个记录的有效长度（单位：字节）
            uint InstNum = (uint)table.m_cfgInsts.Count;// (uint)GetTableInstsNum(tableOp.m_strTableName);// (uint)tableOp.m_InstIndex2LeafName.Count;
            TableOffset += buflen * InstNum;
            return TableOffset;
        }
        /// <summary>
        /// 写 patch_ex 文件
        /// </summary>
        /// <param name="newFilePath"></param>
        bool SaveFile(string newFilePath)
        {
            if (String.Empty == newFilePath)
                return false;

            // 申请内容
            List<byte> allBuff = new List<byte>();

            // 文件头   : 序列化
            byte[] cfgFilebytes = m_cfgFile_Header.StruToByteArray();
            allBuff.AddRange(cfgFilebytes);// 写入头文件

            // 数据块头 : 序列化
            byte[] dtbytes = m_dataHeadInfo.StruToByteArray();
            allBuff.AddRange(dtbytes);  // 数据块的头

            // 偏移块头 : 序列化
            foreach (var tabName in m_mapTableInfo.Keys.ToList())
            {
                uint offset = m_mapTableInfo[tabName].GetTableOffset();
                byte[] tableOffset = BitConverter.GetBytes(offset);
                //Array.Reverse(tableOffset);
                allBuff.AddRange(tableOffset);
            }

            //  表块 实例 : 序列化
            foreach (var tabName in m_mapTableInfo.Keys.ToList())
            {
                //if (String.Equals("nrSearchSpaceEntry", tabName))
                //{
                //    Console.WriteLine("\n");
                //}
                //allBuff.AddRange(tableOp.WriteTofilePDG());
                allBuff.AddRange(m_mapTableInfo[tabName].WriteTofile());
            }
            CfgWriteFile(newFilePath, allBuff.ToArray(), 0);
            return true;
        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        void CfgWriteFile(string filepath, byte[] data, int offset)
        {
            FileStream fs = new FileStream(filepath, FileMode.Create);
            fs.Seek(offset, SeekOrigin.End);// 偏移的位置
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
        }
        #endregion

        #region 前台修改数据
        #endregion

    }
}
