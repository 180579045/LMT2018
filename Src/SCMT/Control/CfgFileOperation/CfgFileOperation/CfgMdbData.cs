using MIBDataParser.JSONDataMgr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfgFileOperation
{
    /// <summary>
    /// 统一用一个
    /// </summary>
    public class CfgAccessDBManager
    {
        public DataSet GetRecord(string fileName, string sqlContent)
        {
            //if (-1 != fileName.IndexOf("SCMT"))
            //{
            //    int a = 1;
            //}
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
        /// 释放内存
        /// </summary>
        /// <param name="dateSet"></param>
        public void Close(DataSet dateSet)
        {
            dateSet.Dispose();
        }
    }
}
