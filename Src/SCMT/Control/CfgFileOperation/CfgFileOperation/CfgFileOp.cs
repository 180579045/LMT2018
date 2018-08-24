using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 引用其他模块内容
using MIBDataParser.JSONDataMgr; // 操作数据库，获取lm.dtz中内容

namespace CfgFileOperation
{
    /// <summary>
    /// .cfg 文件所有的相关操作，对外的接口
    /// </summary>
    public class CfgFileOp
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCfgFileName">创建cfg的文件名字</param>
        /// <param name="FileToUnzip">要解压的文件</param>
        /// <param name="FileToDirectory">解压释放的地方</param>
        /// <returns></returns>
        public bool CreateCfgFile(string strCfgFileName, string FileToUnzip, string FileToDirectory)//const CString &strEquipType, CAdoConnection* pAdoCon, vector<CString>&strTalbename)
        {
            // string FileToUnzip = currentPath + "\\Data\\lmdtz\\lm.dtz";//
            // string FileToDirectory = currentPath + "\\Data\\lmdtz";
            string err = "";

            // 获取原始数据
            /// 1. 解压 lm.dtz -> lm.mdb
            if (!Database.GetInstance().cfgUnzipDtz(FileToUnzip, FileToDirectory, out err))
            {
                return false;
            }

            /// 2. 查数据库，遍历所有的 mibTree 生成 配置文件
            Database.GetInstance().makeMain(FileToDirectory);

            /// sql 获取所有的 table 和 entry 
            //string strSQL = ("select * from MibTree where DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine");
            //var df = Database.GetInstance().cfgGetRecordByAccessDb(FileToDirectory+ "\\lm.mdb", strSQL);

            //if (df.Tables[0].Rows.Count > 0)
            //{
            //    //Database.GetInstance().MibParseDataSet(df);
            //}

            /// 

            Database.GetInstance().setCfgFile_Header();

            return false;
        }
        public void test()
        {
            Database.GetInstance().setCfgFile_Header();
        }
            
    }
}
