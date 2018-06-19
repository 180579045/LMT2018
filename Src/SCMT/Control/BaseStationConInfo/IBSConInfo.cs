using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseStationConInfo
{
    /// <summary>
    /// 单例类 GetInstance（）
    /// </summary>
    interface IBSConInfo
    {
        bool getBaseStationConInfo(Dictionary<string,string> allConInfo);
        bool addBaseStationConInfo(string strName, string strIp);
        bool delBaseStationConInfoByName(string strName);
    }
}
