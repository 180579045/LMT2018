using System;
using System.Collections.Generic;
using System.Linq;
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
            connectBSInfo = new Dictionary<string, string>();
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
            return true;
        }

        public void delAllBaseStationConInfo()
        {
            connectBSInfo = null;
            connectBSInfo = new Dictionary<string, string>();
        }
    }
}
