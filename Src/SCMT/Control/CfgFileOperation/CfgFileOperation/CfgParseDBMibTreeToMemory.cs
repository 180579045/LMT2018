using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MIBDataParser.JSONDataMgr;

namespace CfgFileOperation
{
    /// <summary>
    /// 把 lm.mdb 中的mibtree 数据，读入内存
    /// </summary>
    class CfgParseDBMibTreeToMemory
    {
        public Dictionary<string, CfgParseDBMibTreeStrLineMib> pMapMibNodeByName = null;// MIBName ,CfgReadDBStructMibNode
        public Dictionary<string, CfgParseDBMibTreeStrLineMib> pMapMibNodeByOID = null;// OID ,CfgReadDBStructMibNode

        //private Dictionary<string, ENUM_MIBVALUETYPE> m_mapSynTax2Type = null;
        public CfgParseDBMibTreeToMemory()
        {
            
        }


        public void ReadMibTreeToMemory(string strFileToDirectory)
        {
            //string strFileToDirectory = "";
            strFileToDirectory = "D:\\Git_pro\\SCMT\\Src\\SCMT\\Control\\CfgFileOperation\\CfgFileOperation\\bin\\Debug\\Data\\lmdtz\\lm.mdb";

            string strSQL = "select * from MibTree order by ExcelLine";// ("select * from MibTree where DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine");
            // 连接数据库，获取信息
            DataSet MibdateSet = RecordByAccessDb(strFileToDirectory, strSQL);
            // 处理重组信息
            ParseProcessing(MibdateSet);


            //StruMibNode pTempNode = new StruMibNode();
        }

        /// <summary>
        /// 连接获取数据库信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sqlContent"></param>
        /// <returns></returns>
        private DataSet RecordByAccessDb(string fileName, string sqlContent)
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
