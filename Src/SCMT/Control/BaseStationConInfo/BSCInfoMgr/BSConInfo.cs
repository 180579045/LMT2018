using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CommonUtility;
using LogManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FriendName = System.String;
using IpAddr = System.String;

namespace BaseStationConInfo.BSCInfoMgr
{
	public class BSConInfo : IBSConInfo
	{
		Dictionary<FriendName, IpAddr> connectBSInfo;
		private static BSConInfo  _instance;/// 单例类的句柄
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
			if ((string.Empty == strName) | (string.Empty == strIp)|( null == connectBSInfo))
				return false;

			// 存在相同 key 或 value
			if ((connectBSInfo.ContainsKey(strName))| (connectBSInfo.Keys.Contains(strIp)))
				return false;

			connectBSInfo.Add(strName, strIp);

			writeBSInfoToJsonFile();
			return true;
		}
		public bool delBaseStationConInfoByName(string strName)
		{
			if ((string.Empty == strName) |  (null == connectBSInfo))
				return false;
            // 不存在返回 false
            if (!connectBSInfo.ContainsKey(strName))
                return false;

            connectBSInfo.Remove(strName);
			writeBSInfoToJsonFile();
			return true;
        }
        public bool modifyBaseStationConInfoFriendlyName(string strName, string strIP)
        {
            if ((string.Empty == strName) || (null == connectBSInfo) || (string.Empty == strIP))
                return false;
            // 不存在返回 false
            foreach(var item in connectBSInfo)
            {
                if(item.Value == strIP)
                {
                    connectBSInfo.Remove(item.Key);
                    connectBSInfo.Add(strName, strIP);
                    writeBSInfoToJsonFile();
                    return true;
                }
            }

            return false;
        }

        public bool modifyBaseStationConInfoIP(string strName, string strIP)
        {
            if ((string.Empty == strName) || (null == connectBSInfo) || (string.Empty == strIP))
                return false;
            // 不存在返回 false
            if (!connectBSInfo.ContainsKey(strName))
                return false;

            connectBSInfo[strName] = strIP;
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
			string currentPath = FilePathHelper.GetAppPath();
			string jsonFilePath = currentPath + ConfigFileHelper.NodebListJson;
			WriteFile(jsonFilePath, JsonConvert.SerializeObject(connectBSInfo, Formatting.Indented));
		}

		private void WriteFile(string filepath, string content)
		{
			try
			{
				FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write);//找到文件如果文件不存在则创建文件如果存在则覆盖文件
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

			string currentPath = FilePathHelper.GetAppPath();
			string jsonFilePath = currentPath + ConfigFileHelper.NodebListJson;
			if (!File.Exists(jsonFilePath))
			{
				Log.Error($"文件：{jsonFilePath}不存在，可能还没有创建过");
				return ;
			}
			JObject JObjThree = ReadFile(jsonFilePath);
			if (null == JObjThree)
			{
				Log.Error("readfile函数获取到json内容为null");
				return;
			}

			foreach (var obj in JObjThree)
			{
				connectBSInfo.Add(obj.Key, obj.Value.ToString());
			}
		}

		private JObject ReadFile(string sFilePath)
		{
			FileStream fs = new FileStream(sFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
			JObject JObj = JObject.Parse(sr.ReadToEnd());
			fs.Close();
			return JObj;
		}
	}
}
