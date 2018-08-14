using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data; //DataSet


namespace MIBDataParser.JSONDataMgr
{

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
