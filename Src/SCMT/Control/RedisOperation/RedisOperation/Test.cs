using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
using SCMT_redis.RedisDataMgr;

namespace SCMT_redis
{
    class Test
    {
        static void Main(string[] args)
        {
            //string[] strtest = { "2018年03月", "2018年02" };
            //string ste = "2018年03月";
            //var re = Regex.Matches(ste, "2018");
            //foreach (Match match in Regex.Matches(ste, "^2018年3月"))
            //{
            //    Console.WriteLine("=====", match.Value);
            //}


            RedisOperation test = new RedisOperation();
            test.test5();
        }
    }
}
