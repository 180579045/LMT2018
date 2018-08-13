using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Compression;// zip
using System.IO;// File

namespace CfgFileOperation
{
    /// <summary>
    /// .cfg 相关的内部基础操作模块。
    /// 打开数据库, 读取数据库, 读写文件等相关基础操作
    /// </summary>
    class CfgFileOpBase
    {
        /// <summary>
        /// 解压缩 dtz 文件
        /// </summary>
        /// <param name="strFileToUnzip"></param>
        /// <param name="strFileToDirectory"></param>
        /// <returns></returns>
        public bool UnzipDtzFile(string strFileToUnzip, string strFileToDirectory)
        {
            string err = "";
            // 需要原始的数据的来源，现在是lm.dtz文件
            
            return true;
        }

        public bool GetDataFromMdb(string strSql)
        {
            return true;
        }


    }
}
