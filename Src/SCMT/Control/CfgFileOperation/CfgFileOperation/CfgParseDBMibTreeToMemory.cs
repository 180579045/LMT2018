using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MIBDataParser.JSONDataMgr;
using System.IO;

namespace CfgFileOperation
{
    /// <summary>
    /// 把 lm.mdb 中的mibtree 数据，读入内存
    /// </summary>
    public class CfgParseDBMibTreeToMemory
    {
        public Dictionary<string, CfgParseDBMibTreeStrLineMib> pMapMibNodeByName = null;// MIBName ,CfgReadDBStructMibNode
        public Dictionary<string, CfgParseDBMibTreeStrLineMib> pMapMibNodeByOID = null;// OID ,CfgReadDBStructMibNode

        //private Dictionary<string, ENUM_MIBVALUETYPE> m_mapSynTax2Type = null;
        public CfgParseDBMibTreeToMemory()
        {
            
        }
        
        /// <summary>
        /// patch 需要再次加载 数据库
        /// </summary>
        /// <param name="strFileToDirectory"></param>
        public bool ReadMibTreeToMemory(BinaryWriter bw, string strFileToDirectory)
        {
            string strSQL = "select * from MibTree order by ExcelLine";// ("select * from MibTree where DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine");
            // 连接数据库，获取信息
            DataSet MibdateSet = new CfgAccessDBManager().GetRecord(strFileToDirectory, strSQL);
            if (null == MibdateSet)
            {
                bw.Write(String.Format("path({0}), sql({1}) Get Mdb err.\n", strFileToDirectory, strSQL).ToArray());
                return false;
            }
            // 处理重组信息
            ParseProcessing(MibdateSet);
            new CfgAccessDBManager().Close(MibdateSet);
            return true;
        }

        /// <summary>
        /// 从内存中查询节点的表名
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <returns></returns>
        public string GetTableNameFromDBMibTree(string strNodeName)
        {
            if (null == pMapMibNodeByName || string.Empty == strNodeName)
                return "";
            if (!pMapMibNodeByName.ContainsKey(strNodeName))
                return "";

            return pMapMibNodeByName[strNodeName].strTableName;
        }
        /// <summary>
        /// 通过节点名查询其表索引的个数
        /// </summary>
        /// <param name="strNodeName"></param>
        /// <returns></returns>
        public int GetIndexNumFromDBMibTree(string strNodeName)
        {
            if (null == pMapMibNodeByName || string.Empty == strNodeName)
                return -1;
            if (!pMapMibNodeByName.ContainsKey(strNodeName))
                return -1;
            //var ddd = pMapMibNodeByName[strNodeName];
            return pMapMibNodeByName[strNodeName].nIndexNum;
        }

        // 解析处理
        private void ParseProcessing(DataSet MibdateSet)
        {
            // MIBName ,CfgReadDBStructMibNode
            pMapMibNodeByName = new Dictionary<string, CfgParseDBMibTreeStrLineMib>();
            // OID ,CfgReadDBStructMibNode
            pMapMibNodeByOID = new Dictionary<string, CfgParseDBMibTreeStrLineMib>();

            //--end by cuidairui 2009-08-04
            for (int loop = 0; loop < MibdateSet.Tables[0].Rows.Count - 1; loop++)//在表之间循环
            {
                DataRow row = MibdateSet.Tables[0].Rows[loop];
                //if (row["MIBName"].ToString() == "nrCsiRsImEntry")
                //    Console.WriteLine("==");
                //if (row["MIBName"].ToString() == "nrCsiRsimLcId")
                //    Console.WriteLine("==");//"nrCsiRsImLcId"
                //Console.WriteLine(row["MIBName"].ToString());
                
                // 处理每行的MIB数据
                CfgParseDBMibTreeStrLineMib pTempNode = new CfgParseDBMibTreeStrLineMib(row, pMapMibNodeByName, pMapMibNodeByOID);//StruMibNode pTempNode = new StruMibNode();

                if (!pMapMibNodeByOID.ContainsKey(row["OID"].ToString()))
                    pMapMibNodeByOID.Add(row["OID"].ToString(), pTempNode);
                else
                {
                    Console.WriteLine(String.Format("Oid=({0} had same key, mibName=({1}).)", row["OID"].ToString(), row["MIBName"].ToString()));
                }
                
                if (!pMapMibNodeByName.ContainsKey(row["MIBName"].ToString()))
                    pMapMibNodeByName.Add(row["MIBName"].ToString(), pTempNode);
                else
                {
                    Console.WriteLine(String.Format(" mibName=({0}),had same key.)", row["MIBName"].ToString()));
                }
            }
        }

    }


}
