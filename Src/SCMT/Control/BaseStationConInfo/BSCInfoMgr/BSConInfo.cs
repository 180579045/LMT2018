using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading;
using System.IO;
using System.Text;

using System.Text;
using System.Threading.Tasks;

namespace BaseStationConInfo.BSCInfoMgr
{
    class BSConInfo : IBSConInfo
    {
        Dictionary<string, string> connectBSInfo;
        private static BSConInfo  _instance = null;/// 单例类的句柄
        private static object _syncLock = new object();
        private BSConInfo()///初始化
        {
            //connectBSInfo = new Dictionary<string, string>();
            ReadJsonFileForBSInfo();
        }
        public static BSConInfo GetInstance()
        {
            if (null == _instance)
            {
                lock (_syncLock)
                {
                    if (null == _instance)
                    {
                        _instance = new BSConInfo();
                    }
                }
            }
            return _instance;
        }

        public bool getBaseStationConInfo(Dictionary<string, string> allConInfo)
        {
            if (null == allConInfo)
                return false;

            if (0 == connectBSInfo.Keys.Count)
                return true;
            // 查询
            foreach (var key in connectBSInfo.Keys)
            {
                allConInfo.Add(key, connectBSInfo[key]);
            }

            return true;
        }
        public bool addBaseStationConInfo(string strName, string strIp)
        {
            if ((String.Empty == strName) | (String.Empty == strIp)|( null == connectBSInfo))
                return false;

            ///存在相同 key 或 value
            if ((connectBSInfo.ContainsKey(strName))| (connectBSInfo.Keys.Contains(strIp)))
                return false;

            connectBSInfo.Add(strName, strIp);

            writeBSInfoToJsonFile();
            return true;
        }
        public bool delBaseStationConInfoByName(string strName)
        {
            if ((String.Empty == strName) |  (null == connectBSInfo))
                return false;
            ///不存在返回 false
            if (!connectBSInfo.ContainsKey(strName)) 
                return false;

            connectBSInfo.Remove(strName);
            writeBSInfoToJsonFile();
            return true;
        }

        public void delAllBaseStationConInfo()
        {
            connectBSInfo = null;
            connectBSInfo = new Dictionary<string, string>();
            writeBSInfoToJsonFile();
        }

        private void writeBSInfoToJsonFile()
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            
            string jsonFilePath = currentPath + @"..\..\" + "BaseStationConnectInfo.Json";
            this.WriteFile(jsonFilePath, JsonConvert.SerializeObject(connectBSInfo, Formatting.Indented));
        }

        private void WriteFile(string filepath, string content)
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

        private void ReadJsonFileForBSInfo()
        {
            connectBSInfo = new Dictionary<string, string>();

            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string jsonFilePath = currentPath + @"..\..\" + "BaseStationConnectInfo.Json";
            if (!File.Exists(jsonFilePath))
            {
                return ;
            }
            JObject JObjThree = ReadFile(jsonFilePath);
            if (null == JObjThree)
                return;
            foreach (var obj in JObjThree)
            {
                connectBSInfo.Add(obj.Key.ToString(), obj.Value.ToString());
            }

            
            return ;
        }
        private JObject ReadFile(string sFilePath)
        {
            FileStream fs = new FileStream(sFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            JObject JObj = JObject.Parse(sr.ReadToEnd().ToString());
            fs.Close();
            return JObj;
        }
    }
}
