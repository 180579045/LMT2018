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
    /// 解析cfg为了呈现
    /// </summary>
    class CfgParseFileToShow
    {
        public string m_filePath = "";
        public string m_mdbPath = "";

        public void test()
        {
            // 文件头
            StruCfgFileHeader struFileHead = GetStruCfgFileHeaderInfo(m_filePath);
            // 数据块头
            StruDataHead struDataHead = GetDataHeadFromFile(m_filePath);
            // 每个表的偏移量
            List<uint> TablesOffsetPos = GetTablesOffsetPos(m_filePath, (int)struDataHead.u32TableCnt);
            //List<string> TablesName = GetTableNamesByTablesPos(m_filePath, TablesOffsetPos);
            
            // 所有表的内容(实例、叶子节点等信息)
            uint itablePos = TablesOffsetPos[0];
            // 4.1 表的信息: 44 字节
            StruCfgFileTblInfo struTblInfo = GetTableHeadInfo(m_filePath, itablePos); //获取tableInfo表块介绍
            string strTableName = Encoding.GetEncoding("GB2312").GetString(struTblInfo.u8TblName).TrimEnd('\0');
            // 4.2 叶子节点信息: 60字节 * 叶子个数
            
            Dictionary<string, StruCfgFileFieldInfo> LeafsHead = GetLeafHeadFieldInfo(m_filePath, struTblInfo.u16FieldNum, itablePos);
            List<StruCfgFileFieldInfo> indexLeafsHead;
            // 3. 每个表的实例是否一致
            List<byte[]> YsInsts = GetInstsData(m_filePath, itablePos, (int)struTblInfo.u32RecNum, (int)struTblInfo.u16FieldNum, struTblInfo.u16RecLen);
            List<CfgTableInstanceInfos> m_cfgInsts; //表中每个实例的

    }
        /// <summary>
        /// 解析文件头
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        StruCfgFileHeader GetStruCfgFileHeaderInfo(string filePath)
        {
            int offset = 956;
            byte[] Ysdata = CfgReadFile(filePath, 0, offset);
            StruCfgFileHeader YsFileHead = new StruCfgFileHeader("init");
            YsFileHead.ParseFileReadBytes(Ysdata);
            return YsFileHead;
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
        List<uint> GetTablesOffsetPos(string filePath, int iTableCounts)
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
        /// 获取每个表的头
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        StruCfgFileTblInfo GetTableHeadInfo(string filePath, uint offset)
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
        Dictionary<string, StruCfgFileFieldInfo> GetLeafHeadFieldInfo(string filePath, ushort u16FieldNumYs, uint offset)
        {
            int readCount = 0;
            byte[] Ysdata;
            Dictionary<string, StruCfgFileFieldInfo> leafFieldL = new Dictionary<string, StruCfgFileFieldInfo>();
            for (int i = 0; i < u16FieldNumYs; i++)
            {
                StruCfgFileFieldInfo leafFieldInfo = new StruCfgFileFieldInfo("");
                uint fristLeafFieldInfoPos = offset + 
                    (uint)Marshal.SizeOf(new StruCfgFileTblInfo("")) + 
                    (uint)Marshal.SizeOf(new StruCfgFileFieldInfo("")) * (uint)i;// 第一个叶子头的位置
                readCount = Marshal.SizeOf(new StruCfgFileFieldInfo(""));// 60
                Ysdata = CfgReadFile(filePath, fristLeafFieldInfoPos, readCount);
                leafFieldInfo.SetValueByBytes(Ysdata);
                string leafName = Encoding.GetEncoding("GB2312").GetString(leafFieldInfo.Getu8FieldName()).TrimEnd('\0');
                leafFieldL.Add(leafName, leafFieldInfo);
            }

            return leafFieldL;
        }
        List<StruCfgFileFieldInfo> GetIndexLeafsHeadByOrder(Dictionary<string, StruCfgFileFieldInfo> LeafsHead)
        {
            List<StruCfgFileFieldInfo> indexLeafsHead = new List<StruCfgFileFieldInfo>();
            foreach (var field in indexLeafsHead)
            {
                // 判断是否为索引节点
                if (0 != string.Compare(field.u8FieldTag.ToString(), "Y", true) )
                {
                    continue;
                }
            }
            return indexLeafsHead;
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
        /// 读文件(从readFromOffset读readCount个字节)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="readFromOffset"></param>
        /// <param name="readCount"></param>
        /// <returns></returns>
        public byte[] CfgReadFile(string filePath, long readFromOffset, int readCount)
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

    }
}
