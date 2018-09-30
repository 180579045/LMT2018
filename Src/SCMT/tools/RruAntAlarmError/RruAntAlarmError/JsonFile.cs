/*************************************************************************************
* CLR版本：        $$
* 类 名 称：       $ JsonFile $
* 机器名称：       $ machinename $
* 命名空间：       $ MIBDataParser.JSONDataMgr $
* 文 件 名：       $ JsonFile.cs $
* 创建时间：       $ 2018.04.XX $
* 作    者：       $ TangYun $
* 说   明 ：
*     JsonFile模块。
* 修改时间     修 改 人    修改内容：
* 2018.04.xx   唐 芸       创建文件并实现类  JsonFile
*************************************************************************************/
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MIBDataParser.JSONDataMgr
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
            catch (Exception ex)
            {
                //记日志
                Console.WriteLine("open file {0} failed!{1}",filepath,ex.Message);
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
            catch (Exception ex)
            {
                //记日志
                Console.WriteLine("open file {0} failed!{1}", filepath, ex.Message);
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

        public JObject ReadJsonFileForJObject(string sFilePath)
        {
            //JObject JObjOne = ReadJsonFileMethodOne(sFilePath);

            //JObject JObjTwo = ReadJsonFileMethodTwo(sFilePath);

            JObject JObjThree = ReadJsonFileMethodThree(sFilePath);

            //CompareReadJsonMethod(JObjTwo, JObjThree);
            return JObjThree;
        }
        JObject ReadJsonFileMethodOne(string sFilePath)
        {
            StreamReader fs = File.OpenText(sFilePath);
            //JObject JObj = new JObject();
            JObject JObj = (JObject)JToken.ReadFrom(new JsonTextReader(fs));
            fs.Close();
            return JObj;
        }
        JObject ReadJsonFileMethodTwo(string sFilePath)
        {
            FileStream fs = new FileStream(sFilePath, FileMode.Open);//初始化文件流
            byte[] array = new byte[fs.Length];//初始化字节数组
            fs.Read(array, 0, array.Length);//读取流中数据到字节数组中
            fs.Close();//关闭流
            string str = Encoding.Default.GetString(array);//将字节数组转化为字符串
            JObject JObj = JObject.Parse(str);
            return JObj;
        }
        JObject ReadJsonFileMethodThree(string sFilePath)
        {
            FileStream fs = new FileStream(sFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            JObject JObj = JObject.Parse(sr.ReadToEnd().ToString());
            fs.Close();
            return JObj;
        }
        //bool CompareReadJsonMethod(JObject ObjectOne, JObject ObjectTwo)
        //{
        //    int a = 1;
        //    for (int i =0;i<3;i++)
        //    {

        //    }
        //    return true;
        //}

    }
}
