using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMT_redis.RedisDataMgr
{
    class DataRedis : IDataRedis
    {
        private string strKeyTime { set; get; }//时间序列,到毫秒级"201808131628330697"

        public string DataJson { get; set; }//序列化的数据
        public string KeyTime  { get {return strKeyTime;}}
        public bool setKeyTime(string year, string month, string day, string hour, string min, string sec, string mill)
        {
            string[] Inputlist = { year, month, day, hour, min, sec, mill };
            int   [] Digitlist = { 4, 2, 2, 2, 2, 2, 4 };

            //两层校验
            try{
                foreach (var input in Inputlist){
                    int.Parse(input);
                    if (String.Empty == input){
                        return false;
                    }
                }
            }
            catch{
                return false;
            }

            this.strKeyTime = "";
            for (int i = 0; i < Digitlist.Length; i++){
                this.strKeyTime += this.DigitsIntToString(Inputlist[i], Digitlist[i]);
            }
            return true;
        }

        /// 
        private string DigitsIntToString(string input, int FourDigits)
        {
            string str = (Math.Pow(10, FourDigits) + int.Parse(input)).ToString();
            return str.Substring(1, str.Length - 1);
        }
    }
}
