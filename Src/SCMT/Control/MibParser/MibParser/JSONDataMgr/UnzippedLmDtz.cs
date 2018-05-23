/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $UnzippedLmDtz$
* 机器名称：       $machinename$
* 命名空间：       $JSONDataMgr$
* 文 件 名：       $UnzippedLmDtz.cs$
* 创建时间：       $2018.04.04$
* 作    者：       $luanyibo$
* 说   明 ：
*     通过配置文件信息，实现解压缩
* 修改时间     修 改 人    修改内容：
* 2018.04.04   栾义博      创建文件并实现类 UnzippedLmDtz
* 2018.04.20   栾义博      修改实现
*************************************************************************************/
using System;
using System.IO;
using System.IO.Compression;

namespace MIBDataParser.JSONDataMgr
{
    /// <summary>
    /// 解压 JsonDataMgr.ini中的压缩文件
    /// </summary>
    class UnzippedLmDtz
    {     
        /// <summary> 解压缩文件 </summary>
        /// <returns> flase/true </returns>
        public bool UnZipFile(out string err)
        {
            err = "";
            string zipfilePath = "";
            string zipName = "";
            string zipFile = "";
            string extractPath = "";

            //1. 获取ini配置文件中的相关信息
            ReadIniFile iniFile = new ReadIniFile();
            string iniFilePath = iniFile.getIniFilePath("JsonDataMgr.ini");
            if (String.Empty == iniFilePath)
            {
                err = "JsonDataMgr.ini找不到！";
                return false;
            }

            try
            {
                zipfilePath = iniFile.IniReadValue(iniFilePath, "ZipFileInfo", "zipfilePath");
                zipName     = iniFile.IniReadValue(iniFilePath, "ZipFileInfo", "zipName");
                extractPath = iniFile.IniReadValue(iniFilePath, "ZipFileInfo", "extractPath");
                zipFile = zipfilePath + zipName;
            }
            catch (Exception ex)
            {
                err = ex.Message;//显示异常信息
                return false;//显示异常信息
            }

            //2. 校验
            if (zipFile == string.Empty)
            {
                err = zipFile + " 压缩文件不能为空！";
                return false;
            }
            if (!File.Exists(zipFile))
            {
                err = zipFile + " 压缩文件不存在！";
                return false;
            }

            //3. 解压缩rar文件
            try
            {
                string outfile = extractPath + "output\\lm.mdb";
                //var dte = Directory.Exists(outfile);
                if (File.Exists(outfile))
                {
                    //err = outfile + "存在";
                    File.Delete(outfile);
                }
                //System.IO.Compression.ZipFile.ExtractToDirectory(@"D:\C#\myUnzipTest1\lm.dtz", @"D:\C#\myUnzipTest1");
                ZipFile.ExtractToDirectory(zipFile, extractPath);
            }
            catch (Exception ex)
            {
                err = ex.Message;//显示异常信息
                return false;
            }
            return true;
        }//解压结束

    }

}
