/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ JsonFile $
* 机器名称：       $ machinename $
* 命名空间：       $ SCMT_json.JSONDataMgr $
* 文 件 名：       $ JsonFile.cs $
* 创建时间：       $ 2018.04.XX $
* 作    者：       $ TangYun $
* 说   明 ：
*     JsonFile模块。
* 修改时间     修 改 人    修改内容：
* 2018.04.xx   唐 芸       创建文件并实现类  JsonFile
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMT_json.JSONDataMgr
{
    class JsonFile
    {
        public string ReadFile(string filepath)
        {
            string filestr = "";
            try
            {
                FileStream file = new FileStream(filepath, FileMode.Open);
                StreamReader fileread = new StreamReader(file);
                filestr = fileread.ReadLine();
                fileread.Close();
            }
            catch (Exception e)
            {
                //记日志
                Console.WriteLine("open file " + filepath + " failed!");
            }
            return filestr;
        }
        /// <summary>
        /// 读取所有的json文件内容
        /// </summary>
        /// <param name="filepath"> 文件 </param>
        /// <returns></returns>
        public string ReadFileToEnd(string filepath)
        {
            string filestr = "";
            try
            {
                FileStream file = new FileStream(filepath, FileMode.Open);
                StreamReader fileread = new StreamReader(file);
                filestr = fileread.ReadToEnd();
                fileread.Close();
            }
            catch (Exception e)
            {
                //记日志
                Console.WriteLine("open file " + filepath + " failed!");
            }
            return filestr;
        }
        public void WriteFile(string filepath, string content)
        {
            try
            {
                FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);//找到文件如果文件不存在则创建文件如果存在则覆盖文件
                //清空文件
                fs.SetLength(0);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write(content);
                sw.Flush();
                sw.Close();
            }
            catch 
            {
                //记日志
                Console.WriteLine("write file " + filepath + " failed!");
            }
        }
    }
}
