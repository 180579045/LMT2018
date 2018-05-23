using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMT_redis
{
    /// <summary>
    /// 存在redis中数据格式
    /// </summary>
    interface IDataRedis
    {
        string KeyTime { get;} //时间序列,到毫秒级 "2014-11-21 15:39:53,504" 1s=1000ms毫秒
        string DataJson { get; set; }//序列化的数据
        bool setKeyTime(string year, string month, string day, string hour, string min, string sec, string mill);
    }
    interface IRedisOperation
    {
        bool RedisClient(string host, string port, out string err);
        bool RedisSaveData(IDataRedis data, out string err);
    }
}
