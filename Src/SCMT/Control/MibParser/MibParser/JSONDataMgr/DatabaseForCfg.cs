using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data; //DataSet


namespace MIBDataParser.JSONDataMgr
{
    /// c ++  : c #
    /// u8    : byte
    /// u16   : ushort
    /// u32   : uint
    /// s8    : sbyte
    /*
     * 文件头
     * 使用 [StructLayout(LayoutKind.Sequential)] 后，自动4字节对齐
     */
    [StructLayout(LayoutKind.Sequential)]
    class CfgFile_Header
    {
        byte[] u8VerifyStr = new byte[4];         // [4]文件头的校验字段 "ICF"  
        byte u8HiDeviceType = new byte();                      //
        byte u8MiDeviceType;                      //
        ushort u16LoDevType;
        uint u32IcFileVer;                        //  初配文件版本：用来标志当前文件的大版本
        uint u32ReserveVer;                       //  初配文件小版本：用来区分相同大版本下的因取值不同造成的差异，现在这里是最小版本
        uint u32DataBlk_Location;                 //  数据块起始位置 
        byte[] u8LastMotifyDate = new byte[20];   //  [20]文件最新修改的日期, 按字符串存放 
        uint u32IcFile_HeaderVer;                 //  初配文件头版本，用于记录不同的文件头格式、版本 
        uint u32PublicMibVer;                     //  公共Mib版本号
        uint u32MainMibVer;                       //  Mib主版本号
        uint u32SubMibVer;                        //  Mib辅助版本号
        uint u32IcFile_HeaderLength;              //  初配文件头部长度 
        byte[] u8IcFileDesc = new byte[256];      //   [256]文件描述 
        uint u32RevDatType;                       //  保留段数据类别 (1: 文件描述) 
        uint u32IcfFileType;                      //  初配文件类别（1:NB,2:RRS） 2005-12-22 
        uint u32IcfFileProperty;                  //  初配文件属性（0:正式文件;1:补充文件）
        uint u32DevType;                          //  设备类型(1:超级基站;2:紧凑型小基站)

        ushort u16NeType;                         //  数据文件所属网元类型
        byte[] u8Pading = new byte[2];            //  [2]
        sbyte[] s8DataFmtVer = new sbyte[12];     //  [12] 数据文件版本（与对应的MIB版本相同）  
        byte u8TblNum;                            //  数据文件中表的个数  
        byte u8FileType;                          //  配置文件类别(1:cfg或dfg,2:pdg)  
        byte u8Pad1;                              //  保留 
        byte u8ReserveAreaType;                   //  保留空间的含义 =0  
        //==================================================================//
        uint[] u32TblOffset = new uint[150];      // [150] 每个表的数据在文件中的起始位置（相对文件头）  
        byte[] reserved = new byte[4];            // [4] 保留字段 
    }
    /// <summary>
    /// 为cfg 提供的相关接口实现
    /// </summary>
    public sealed partial class Database : IDatabase
    {
        /// <summary>
        /// 解压lmdtz文件到指定目录下
        /// </summary>
        /// <param name="strFileToUnzip">目标dtz文件</param>
        /// <param name="strFileToDirectory">解压释放目录</param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool cfgUnzipDtz(string strFileToUnzip, string strFileToDirectory, out string err)
        {
            err = "";
            ZipOper zipOp = new ZipOper();
            // 校验 ：是否存在
            if (!zipOp.isFileExist(strFileToUnzip, out err))
            {
                return false;
            }
            // 前处理：解压缩前，把lm 和 lm.mdb 删除
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
            // 解压 ：
            if (!zipOp.decompressedFile(strFileToUnzip, strFileToDirectory, out err))
            {
                return false;
            }
            // 后处理：改名字
            if (!zipOp.moveFile(dealFileL[0], dealFileL[1], out err))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 通过 sql 语句获取 数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sqlContent"></param>
        /// <returns></returns>
        public DataSet cfgGetRecordByAccessDb(string fileName, string sqlContent)
        {
            //fileName = "D:\\C#\\SCMT\\lm.mdb";
            DataSet dateSet = new DataSet();
            //To do:将lm.dtz改名lm.rar,再解压缩,再改名成为mdb
            AccessDBManager mdbData = new AccessDBManager(fileName);
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

        public bool makeMain(string FileToDirectory)
        {

            /// sql 获取所有的 table 和 entry 
            string strSQL = ("select * from MibTree where DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine");
            DataSet tableEntryData = cfgGetRecordByAccessDb(FileToDirectory + "\\lm.mdb", strSQL);

            // table, entry
            MibParseDataSet(FileToDirectory, tableEntryData);
            return true;
        }


        //string ALARMCAUSEENTRY = ("alarmCauseEntry");
        //string ANTENNATYPEENTRY = ("antennaArrayTypeEntry");
        //string ANNTENNAWEITHTENTRY = ("antennaWeightEntry");
        //string RRUTYPEENTRY = ("rruTypeEntry");
        //string RRUTYPEPORTENTRY = ("rruTypePortEntry");

        //string ADJCELLEUTRAENTRY	= "cellAdjEutraCellEntry";
        //string ADJCELLUTRAFDDENTRY = "cellAdjUtraFddCellEntry";
        //string ADJCELLUTRATDDENTRY = "cellAdjUtraTddCellEntry";
        //string ADJCELLGERANENTRY = "cellAdjGeranCellEntry";
        //string ADJCELLCDMA2000ENTRY = "cellAdjCdma2000CellEntry";

        public bool MibParseDataSet(string FileToDirectory, DataSet MibdateSet)
        {
            //JArray mibJArray = new JArray();
            //JObject objRec = null;
            //JArray childJArray = null;

            // unsigned int TableOffset = sizeof(OM_STRU_CfgFile_Header);
            int TableOffset = 123456;
            object pAdoCon = new object();//?
            List<string> listTabName = new List<string>();//?

            List <string> tableList = new List<string>();
            int tableNum = MibdateSet.Tables[0].Rows.Count;
            for (int loop = 0; loop < tableNum - 1; loop++)
            {
                DataRow row = MibdateSet.Tables[0].Rows[loop];
                string strNodeOid = row["MIBName"].ToString();
                string strTableName = row["MIBName"].ToString();
                string strTableContent = row["TableContent"].ToString();
                string strChFriendName = row["ChFriendName"].ToString();

                bool isDyTable = false; //是否为动态表
                bool bSpecialTable = false;//是否是告警,是否为RRUType表和RRUTypePort和antennaArrayTypeTable表
                bool bRRUAndAntennaTable = false;//
                if(String.Equals("0", strTableContent) || String.Empty == strTableContent)
                {
                    isDyTable = false;
                }
                else
                {
                    isDyTable = true;
                }
                //新的天线和告RRu表的处理和告警表的处理方式是一样的
                List<string> specialList = new List<string>() {
                    "alarmCauseEntry","antennaArrayTypeEntry","rruTypeEntry" ,"rruTypePortEntry",
                };
                if (specialList.Exists(e => e.Equals(strTableName)))
                {
                    bSpecialTable = true;
                }
                
                if (!CreatCfgFile_tabInfo(
                    FileToDirectory, 
                    strTableName, 
                    TableOffset, 
                    strNodeOid, 
                    isDyTable, 
                    strTableContent, 
                    bSpecialTable, 
                    listTabName, 
                    strChFriendName))
                {
                    return false;
                }

            }
            return true;
        }

        /*=============================================
        函 数 名: CreateCfgFile
        功能描述: 创建配置文件--获取表信息
        输入参数:
        作    者: wangliwei 
        附加说明: modify yangyuming add ,CString chFirendName
        ==============================================*/
        bool CreatCfgFile_tabInfo(string FileToDirectory, string strTableName,  int TableOffset, string strNodeOid,
            bool isDyTable, string strTableContent, bool bSpecialTal, List<string> vectTabName, string chFirendName)
        {
            //CAdoRecordSet recordset;
            //recordset.SetConnection(pAdoCon);
            //m_pAdoConn = pAdoCon;//add by yangyuming 2012.1.17导出出配数据时出现异常，由于在创建特殊表时未查到数据库链接
            ////strSQL.Format("select * from mibtree where ParentOID ='%s' and IsLeaf <> 0 and ICFWriteAble <> '×' order by ExcelLine", NodeOid);
            //if (!recordset.Open(strSQL))
            //    return true;

            // 查找 这个表的所有叶子节点
            string strSQL = String.Format("select * from mibtree where ParentOID ='{0}' and IsLeaf <> 0 and ICFWriteAble <> '×' order by ExcelLine", strNodeOid);
            DataSet leafInfos = cfgGetRecordByAccessDb(FileToDirectory + "\\lm.mdb", strSQL);

            IntPtr[] sbs = new IntPtr[2];
            return true;
        }
    }
}
