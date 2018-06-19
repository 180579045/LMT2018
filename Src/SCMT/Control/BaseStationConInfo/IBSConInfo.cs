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
        /// <summary>
        /// 获取基站连接的信息
        /// </summary>
        /// <param name="allConInfo">Key:Name, Value:Ip</param>
        /// <returns></returns>
        bool getBaseStationConInfo(Dictionary<string,string> allConInfo);

        /// <summary>
        /// 增加基站的连接的信息
        /// </summary>
        /// <param name="strName">key，基站的名字</param>
        /// <param name="strIp">value, 基站的ip</param>
        /// <returns></returns>
        bool addBaseStationConInfo(string strName, string strIp);

        /// <summary>
        /// 删除指定名字的基站连接信息
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        bool delBaseStationConInfoByName(string strName);
    }
}
