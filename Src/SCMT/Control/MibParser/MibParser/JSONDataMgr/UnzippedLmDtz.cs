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
using System.Collections.Generic;
using CommonUtility;

namespace MIBDataParser.JSONDataMgr
{
    public class ZipOper
    {
        /// <summary>
        /// 查看文件是否真实存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool isFileExist(string filePath, out string err)
        {
            err = "";
            // 校验
            if (filePath == string.Empty)
            {
                err = filePath + " 文件名不能为空！";
                return false;
            }
            if (!File.Exists(filePath))
            {
                err = filePath + " 文件不存在！";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 重命令文件
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool moveFile(string sourceFileName, string destFileName, out string err)
        {
            err = "";
            try
            {
                File.Move(sourceFileName, destFileName);
            }
            catch (Exception ex)
            {
                err = "moveFile err:" + "sourceFileName:" + sourceFileName + " destFileName:" + destFileName +" ."+ ex.Message;//显示异常信息
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool delFile(List<string> filePathList, out string err)
        {
            bool re = true;
            err = "";
            string outErr = "";
            foreach (var filePath in filePathList)
            {
                try
                {
                    if (isFileExist(filePath, out outErr))
                    {
                        File.Delete(filePath);
                    }
                    else
                    {
                        err += outErr;
                    }
                }
                catch
                {
                    err += "(del异常:" + filePath + ")";
                    re = false;
                }
            }
            if (!re)
                err = "删除文件 delFile err:" + err;
            return re;
        }

        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="sourceFile">解压缩的文件</param>
        /// <param name="destPath">解放后的位置</param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool decompressedFile(string sourceFile, string destPath, out string err)
        {
            err = "";
            try
            {
                ZipFile.ExtractToDirectory(sourceFile, destPath);
            }
            catch (Exception ex)
            {
                err = "decompressedFile 解压缩文件 err:" + " sourceFile:" + sourceFile + " destPath:" + destPath + ex.Message;//显示异常信息
                return false;
            }
            return true;
        }


    }

    /// <summary>
    /// 解压 JsonDataMgr.ini中的压缩文件
    /// </summary>
    class UnzippedLmDtz : ZipOper
    {     
        /// <summary> 解压缩文件-废弃 </summary>
        /// <returns> flase/true </returns>
        public bool UnZipFileOld(out string err)
        {
            err = "";
            string zipfilePath = "";
            string zipName = "";
            string zipFile = "";
            string extractPath = "";

            //1. 获取ini配置文件中的相关信息
            ReadIniFile iniFile = new ReadIniFile();
            string iniFilePath = ReadIniFile.GetIniFilePath("JsonDataMgr.ini", out err);
            if (String.Empty == iniFilePath)
            {
                err = err + "JsonDataMgr.ini找不到！";
                return false;
            }

            try
            {
                zipfilePath = ReadIniFile.IniReadValue(iniFilePath, "ZipFileInfo", "zipfilePath");
                zipName     = ReadIniFile.IniReadValue(iniFilePath, "ZipFileInfo", "zipName");
                extractPath = ReadIniFile.IniReadValue(iniFilePath, "ZipFileInfo", "extractPath");
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
                string outfile = extractPath + "\\lm.mdb";
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

        /// <summary>
        /// 解压缩zip文件
        /// </summary>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool UnZipFile(string connectIp, out string err)
        {
            err = "";
            string errNew = "";

            //1. 获取ini配置文件中的相关信息
            Dictionary<string, string> readFile;
            if (!UzipReadIni(connectIp, out readFile, out errNew))
            {
                err += errNew;
                return false;
            }

            //2. 校验
            if (!isFileExist(readFile["zipFile"], out errNew))
            {
                err += errNew;
                return false;
            }

            //3. 解压缩rar文件
            if (!UzipExtractToDirectory(readFile["zipFile"], readFile["extractPath"], out errNew))
            {
                err += errNew;
                return false;
            }
            return true;
        }//解压结束

        /// <summary>
        /// 读取.ini配置文件中压缩文件的相关路径等
        /// </summary>
        /// <param name="readFile"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool UzipReadIni(string connectIp, out Dictionary<string,string> readFile, out string err)
        {
            string path = connectIp;
            if (connectIp.Contains(":"))
                path = connectIp.Replace(":", "-");

            err = "";
            readFile = null;

            //获取ini配置文件中的相关信息
            ReadIniFile iniFile = new ReadIniFile();
            string iniFilePath = ReadIniFile.GetIniFilePath("JsonDataMgr.ini", out err);
            // 1.校验
            if (String.Empty == iniFilePath)
            {
                err = "JsonDataMgr.ini找不到！" + err;
                return false;
            }
            
            // 2. 获取信息
            try
            {
                var appPath = FilePathHelper.GetAppPath();
                readFile = new Dictionary<string, string>() {
                    //注释部分为从基站获取的lm.dtz文件地址，由于解压失败，现在还是使用本地lm.dtz
                    /*{ "zipFile",
                        ( appPath + ReadIniFile.IniReadValue(iniFilePath, "ZipFileInfo", "zipfilePath") + path + "\\" +
                            ReadIniFile.IniReadValue(iniFilePath, "ZipFileInfo", "zipName")) },
                    { "extractPath",
                        appPath + ReadIniFile.IniReadValue(iniFilePath, "ZipFileInfo", "extractPath") + path + "\\"},*/

                    { "zipFile",
                        ( appPath + ReadIniFile.IniReadValue(iniFilePath, "ZipFileInfo", "zipfilePath") +
                            ReadIniFile.IniReadValue(iniFilePath, "ZipFileInfo", "zipName")) },
                    { "extractPath",
                        appPath + ReadIniFile.IniReadValue(iniFilePath, "ZipFileInfo", "extractPath")}};
            }
            catch (Exception ex)
            {
                err = ex.Message;//显示异常信息
                return false;//显示异常信息
            }
            return true;
        }

        /// <summary>
        /// 解压缩文件前处理
        /// </summary>
        /// <param name="sourceArchiveFileName"></param>
        /// <param name="destinationDirectoryName"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        bool UzipExtractToDirectoryPreDeal(string sourceArchiveFileName, string destinationDirectoryName, out string err)
        {
            err = "";
            //解压缩前，把lm 和 lm.mdb 删除
            delFile( new List<string> {destinationDirectoryName + "lm", destinationDirectoryName + "lm.mdb", destinationDirectoryName + "\\output\\lm.mdb"}, out err);

            return true;
        }
        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="sourceArchiveFileName"></param>
        /// <param name="destinationDirectoryName"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        bool UzipExtractToDirectoryDeal(string sourceArchiveFileName, string destinationDirectoryName, out string err)
        {
            if (!decompressedFile(sourceArchiveFileName, destinationDirectoryName, out err))
            {
                return false;
            }             
            else
                return true;
        }
        /// <summary>
        /// 解压后处理:重命名
        /// </summary>
        /// <param name="destinationDirectoryName"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        bool UzipRenameDirectoryAssisDeal(string destinationDirectoryName, out string err)
        {
	        if (!moveFile(destinationDirectoryName + "\\lm.mdb", destinationDirectoryName + "lm.mdb", out err))
	        {
		        return moveFile(destinationDirectoryName + "\\output\\lm.mdb", destinationDirectoryName + "lm.mdb",
			        out err);
	        }

	        return true;
        }

        /// <summary>
        /// 解压缩文件相关的操作
        /// </summary>
        /// <param name="sourceArchiveFileName"></param>
        /// <param name="destinationDirectoryName"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool UzipExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName, out string err)
        {
            err = "";
            // 预处理
            if ( !UzipExtractToDirectoryPreDeal(sourceArchiveFileName, destinationDirectoryName, out err))
            {
                return false;
            }
            // 处理  解压缩rar文件
            if(!UzipExtractToDirectoryDeal(sourceArchiveFileName, destinationDirectoryName, out err))
            {
                return false;
            }
            // 后处理
            // 解压后处理:重命名
            if (!UzipRenameDirectoryAssisDeal(destinationDirectoryName, out err))
            {
                return false;
            }
            return true;
        }

    }
    
}
