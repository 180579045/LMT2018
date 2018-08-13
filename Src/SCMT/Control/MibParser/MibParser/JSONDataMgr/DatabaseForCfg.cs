using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool UnzipDtzForCfg(string strFileToUnzip, string strFileToDirectory, out string err)
        {
            err = "";

            ZipOper zipOp = new ZipOper();
            // 
            zipOp.isFileExist(strFileToUnzip, out err);
            //解压缩前，把lm 和 lm.mdb 删除
            zipOp.delFile(new List<string>() {strFileToDirectory + "lm",
                strFileToDirectory + "lm.mdb",}, out err);
            zipOp.decompressedFile(strFileToUnzip, strFileToDirectory, out err);

            return false;
        }
    }
}
