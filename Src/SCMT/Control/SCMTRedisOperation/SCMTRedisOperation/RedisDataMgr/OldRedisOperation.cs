using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.IO;
//using ServiceStack.Support;
//using ServiceStack.Redis.Support;
//using ServiceStack.Text;
using ServiceStack.Redis;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace SCMT_redis.RedisDataMgr
{
    class RedisOperation
    {
        RedisClient redisClient = null;

        bool RedisClient(string host, string port, out string err)
        {
            err = "";
            if (String.Empty == host) {
                err += "Input host is Null.";
                host = "127.0.0.1";
            }
            if (String.Empty == port) {
                err += "Input post is Null.";
                port = "6379";
            }
            try {
                redisClient = new RedisClient(host, Int32.Parse(port));
                // 测试是否连接
                redisClient.Set<string>("IsConnect", "Yes");
                redisClient.Del("IsConnect");
            }
            catch {
                err += "连接失败"+host+":"+port;
                return false;
            }
            return true;
        }
        bool RedisDumpAll()
        {
            redisClient.FlushAll();
            return true;
        }
        bool RedisSaveData(DataRedis data, out string err)
        {
            err = "";
            try {
                redisClient.Set<DataRedis>(data.KeyTime, data);
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }
        void RedisSave()
        {
            redisClient.BgSave();//BGSAVE后台创建数据快照
        }
        bool RedisSaveAllData(IDictionary<string, DataRedis> values, out string err)
        {
            err = "";
            try
            {
                redisClient.SetAll<DataRedis>(values);
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            return true;
        }
        bool RedisGetAllKeys(out List<string> KeysList)
        {
            KeysList = new List<string> ();
            KeysList = redisClient.GetAllKeys();
            return true;
        }
        bool RedisGetData(string key, out DataRedis value)
        {
            //value = new DataRedis();
            value = redisClient.Get<DataRedis>(key);
            return true;
        }
        bool RedisGetAllData(out List<DataRedis> value)
        {
            //value = new DataRedis();
            value = new List<DataRedis>();
            //value.Add();

            //value = redisClient.GetAll<DataRedis>();
            return true;
        }

        int effectiveNum(string[] list)
        {
            int len = 0;
            for (int i = 0; i < list.Length; i++){
                if (String.Empty == list[i])
                    break;
                len += 1;
            }
            return len;
        }
        string effectivePattern(string[] list, int effecNum)
        {
            string[] listName = { "年", "月", "日", "时", "分", "秒", "毫秒" };
            string effecPat = "*";
            for (int i=0; i< effecNum; i++){
                effecPat += list[i] + listName[i];
            }
            effecPat += "*";
            return effecPat;
        }
        int findStartBetweenEndDiffNum(string[] listStart, string[] listEnd)
        {
            int diffNum = -1;
            for (int i = 0; i < 7; i++)
                if (!String.Equals(listStart[i], listEnd[i]))
                    break;
                else
                    diffNum += 1;
            return diffNum;
        }

        bool RedisGetDataByDateTime(string startYear,string endYear,
            string startMonth,string endMonth, string startDay,string endDay,
            string startHour, string endHour, string startMin, string endMin,
            string startSecond, string endSecond, string startMill, string endMill, out List<string> keyList)
        {
            keyList = new List<string>();
            //public List<string> SearchKeys(string pattern);
            string[] listStart = {  startYear, startMonth, startDay,startHour, startMin, startSecond,startMill};
            string[] listEnd   = {  endYear,   endMonth,   endDay,  endHour,   endMin,    endSecond, endMill};
            string[] listName  = { "年", "月", "日", "时", "分", "秒", "毫秒" };

            // 可能需要递归。。。
            int differentNum = findStartBetweenEndDiffNum(listStart, listEnd);
            
            List<string> findKeyList = new List<string>();

            List<string> PatternMins;
            this.findPatternList(listStart, listEnd, differentNum + 1, out PatternMins);
            if (PatternMins.Count > 2)
            {
                //i=0,end
                findKeyList = redisClient.SearchKeys(PatternMins[0]);
                findKeyList.AddRange(redisClient.SearchKeys(PatternMins[PatternMins.Count - 1]));
                this.findKeyListRightNo(findKeyList, listStart, listEnd, out keyList);

                //1~end-1
                for (int i = 1; i < PatternMins.Count - 1; i++)
                {
                    //findKeyList.AddRange(redisClient.SearchKeys(patt));
                    keyList.AddRange(redisClient.SearchKeys(PatternMins[i]));
                }
            }
            else
            {
                foreach (var patt in PatternMins)
                {
                    findKeyList.AddRange(redisClient.SearchKeys(patt));
                }
                this.findKeyListRightNo(findKeyList, listStart, listEnd, out keyList);
            }

            return false;
        }

        bool findPatternList(string[] listStart, string[] listEnd, int effecNum,out List<string> pattList)
        {
            pattList = new List<string>();
            List<string> newlistStart = new List<string>(listStart.ToArray());
            int fristDiffStart = int.Parse(listStart[effecNum]);
            int fristDiffEnd = int.Parse(listEnd[effecNum]);

            for (int i = fristDiffStart; i <= fristDiffEnd; i++)
            {
                newlistStart[effecNum] = i.ToString();
                string PatternMins = this.effectivePattern(newlistStart.ToArray(), effecNum + 1);
                pattList.Add(PatternMins);
            }
            return true;
        }


        bool findKeyListRightNo(List<string> keyList, string[] listStart, string[] listEnd, out List<string> newKeyList)
        {
            newKeyList = new List<string>();

            string StrSta = this.DigitsListToString(listStart);
            string StrEnd = this.DigitsListToString(listEnd);
            long intSta = long.Parse(StrSta);
            long intEnd = long.Parse(StrEnd);

            long keyInt = 0;
            foreach (var key in keyList)
            {
                keyInt = long.Parse(this.StringReplaceTime(key));
                if (keyInt >= intSta && keyInt <= intEnd)
                    newKeyList.Add(key);
            }
            return true;
        }

        /// <summary>
        /// 处理占位问题，例如mill毫秒="218",out = "0218"
        /// </summary>
        /// <param name="input"></param>
        /// <param name="n">10的n次方</param>
        /// <returns></returns>
        string DigitsIntToString(string input, int n)
        {
            string a1 = (Math.Pow(10, n) + int.Parse(input)).ToString();
            return a1.Substring(1, a1.Length - 1);
        }
        /// <summary>
        /// 变成一组数,inputList={"2018","2","27","13","39","59","900"},outStr="201802271339590900"
        /// </summary>
        /// <param name="digitsList"></param>
        /// <returns></returns>
        string DigitsListToString(string [] digitsList)
        {
            int[] listDigit = { 4, 2, 2, 2, 2, 2, 4 };
            string strs = "";
            for (int i = 0; i < listDigit.Length; i++)
                strs += this.DigitsIntToString(digitsList[i], listDigit[i]);
            return strs;
        }
        /// <summary>
        /// input="2018年11月25日13时25分23秒100毫秒", outStr="201811251325230100"
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string StringReplaceTime(string key)
        {
            string[] strReplace = { "年", "月", "日", "时", "分", "秒", "毫秒" };
            string[] split = key.Split(strReplace, StringSplitOptions.RemoveEmptyEntries);
            return this.DigitsListToString(split);
        }

        bool RedisGetDataByPattern(string pattern, out List<string> keyList)
        {
            keyList = redisClient.SearchKeys(pattern);
            return true;
        }

        void WriteRedisDbInfo()
        {
            List<string> KeysList;
            RedisGetAllKeys(out KeysList);
            //实例化一个文件流--->与写入文件相关联
            FileStream fs = new FileStream("keyList123.txt", FileMode.Create);
            //获得字节数组
            string testst = string.Join(",\n", KeysList.ToArray());
            byte[] data = new UTF8Encoding().GetBytes(testst);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }


        //public bool Set<T>(string key, T value);
        //public void SetAll<T>(IDictionary<string, T> values);
        //public List<string> GetAllKeys();
        //public T Get<T>(string key);
        //public IList<T> GetAll<T>() where T : class, new();

        bool CalculateMilliseconds(string dateTime, out int[] outIntDate)
        {
            string[] strArr = dateTime.Split(new char[] { '-', ' ', ':', ',' });
            if (strArr.Length != 7)
            {
                outIntDate = null;
                return false;
            }

            int[] intDate = {int.Parse(strArr[0]),
                int.Parse(strArr[1]),
                int.Parse(strArr[2]),
                int.Parse(strArr[3]),
                int.Parse(strArr[4]),
                int.Parse(strArr[5]),
                int.Parse(strArr[6]),
            };
            outIntDate = intDate;

            return true;
        }

        public void test()
        {
            string info = @"从前有个人见人爱的小姑娘，喜欢戴着外婆送给她的一顶红色天鹅绒的帽子，
                于是大家就叫她小红帽。有一天，母亲叫她给住在森林的外婆送食物，\n
            并嘱咐她不要离开大路，走得太远。小红帽在森林中遇见了狼，她从未见过狼，
            也不知道狼性凶残，于是把来森林中的目的告诉了狼。狼知道后诱骗小红帽去采野花，
            自己跑到林中小屋去把小红帽的外婆吃了。并装成外婆，等小红帽来找外婆时，
            狼一口把她吃掉了。后来一个猎人把小红帽和外婆从狼肚里救了出来。";

            string err;
            if (!RedisClient("", "", out err))
                Console.WriteLine(err);

            RedisDumpAll();

            for (int i=0;i<1000;i++)
            {
                DataRedis inputData = new DataRedis();
                DateTime dtime = DateTime.Now;
                inputData.setKeyTime(
                    dtime.Year.ToString(),
                    dtime.Month.ToString(),
                    dtime.Day.ToString(),
                    dtime.Hour.ToString(),
                    dtime.Minute.ToString(),
                    dtime.Second.ToString(),
                    dtime.Millisecond.ToString());

                inputData.DataJson = i.ToString() + ":" + info;
                RedisSaveData(inputData, out err);
                if (err != String.Empty)
                    Console.WriteLine(err);
            }

            List<string> KeysList;
            RedisGetAllKeys(out  KeysList);

            DataRedis value;
            RedisGetData(KeysList[11],out value);
            Console.Read();


            ////Json.NET序列化  
            //string jsonData = JsonConvert.SerializeObject(lstStuModel);
            //Console.WriteLine(jsonData);
            //Console.ReadKey();


            ////Json.NET反序列化  
            //string json = @"{ 'Name':'C#','Age':'3000','ID':'1','Sex':'女'}";
            //Student descJsonStu = JsonConvert.DeserializeObject(json);//反序列化  
        }

        public void test2()
        {
            string info = @"从前有个人见人爱的小姑娘，喜欢戴着外婆送给她的一顶红色天鹅绒的帽子，
                于是大家就叫她小红帽。有一天，母亲叫她给住在森林的外婆送食物，\n
            并嘱咐她不要离开大路，走得太远。小红帽在森林中遇见了狼，她从未见过狼，
            也不知道狼性凶残，于是把来森林中的目的告诉了狼。狼知道后诱骗小红帽去采野花，
            自己跑到林中小屋去把小红帽的外婆吃了。并装成外婆，等小红帽来找外婆时，
            狼一口把她吃掉了。后来一个猎人把小红帽和外婆从狼肚里救了出来。";

            string err;
            if (!RedisClient("", "", out err))
                Console.WriteLine(err);

            RedisDumpAll();


            Random rd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                DataRedis inputData = new DataRedis();
                DateTime dtime = DateTime.Now;
                inputData.setKeyTime2(
                    (rd.Next(2018, 2020)).ToString(),
                    (rd.Next(1,12 )).ToString(),
                    (rd.Next(1, 31)).ToString(),
                    (rd.Next(1, 24)).ToString(),
                    (rd.Next(1, 60)).ToString(),
                    (rd.Next(1, 60)).ToString(),
                    (rd.Next(1, 1000)).ToString());

                inputData.DataJson = i.ToString() + ":" + info;
                RedisSaveData(inputData, out err);
                if (err != String.Empty)
                    Console.WriteLine(err);
            }

            List<string> KeysList;
            RedisGetAllKeys(out KeysList);

            DataRedis value;
            RedisGetData(KeysList[11], out value);

            //实例化一个文件流--->与写入文件相关联
            FileStream fs = new FileStream("keyList123.txt", FileMode.Create);
            //获得字节数组
            string testst = string.Join(",\n", KeysList.ToArray());
            byte[] data = new UTF8Encoding().GetBytes(testst);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();

            List<string> KeysList2;
            //RedisGetDataByPattern(@"^2018年3月(\d+)日(\d+)时(\d+)分(\d+)秒(\d+)毫秒", out KeysList2);

            //RedisGetDataByPattern(@"2018年3月*", out KeysList2);

            RedisGetDataByPattern(@"(?<=2018)年3月*", out KeysList2);

            //RedisGetDataByPattern(@"2018*", out KeysList2);

            Console.Read();


            ////Json.NET序列化  
            //string jsonData = JsonConvert.SerializeObject(lstStuModel);
            //Console.WriteLine(jsonData);
            //Console.ReadKey();


            ////Json.NET反序列化  
            //string json = @"{ 'Name':'C#','Age':'3000','ID':'1','Sex':'女'}";
            //Student descJsonStu = JsonConvert.DeserializeObject(json);//反序列化  
        }

        public void test3()
        {
            string err;
            if (!RedisClient("", "", out err))
                Console.WriteLine(err);

            List<string> KeysList2;
            //RedisGetDataByPattern(@"^2018年3月(\d+)日(\d+)时(\d+)分(\d+)秒(\d+)毫秒", out KeysList2);

            //RedisGetDataByPattern(@"2018年3月*", out KeysList2);
            string tet = String.Format("{0}{1}{2}", "a", "b", "c");
            RedisGetDataByPattern(@"*2018年*月1*", out KeysList2);

            // 2018年1月*日~2月

            //RedisGetDataByPattern(@"2018*", out KeysList2);

            Console.Read();
        }

        public void test4()
        {
            //string [] tes = { "2018", "5", "11", "13", "59", "49", "9000" };
            //int a = findStartBetweenEndDiffNum(tes, tes);

            string info = @"从前有个人见人爱的小姑娘，喜欢戴着外婆送给她的一顶红色天鹅绒的帽子，
                于是大家就叫她小红帽。有一天，母亲叫她给住在森林的外婆送食物，\n
            并嘱咐她不要离开大路，走得太远。小红帽在森林中遇见了狼，她从未见过狼，
            也不知道狼性凶残，于是把来森林中的目的告诉了狼。狼知道后诱骗小红帽去采野花，
            自己跑到林中小屋去把小红帽的外婆吃了。并装成外婆，等小红帽来找外婆时，
            狼一口把她吃掉了。后来一个猎人把小红帽和外婆从狼肚里救了出来。";

            string err;
            if (!RedisClient("", "", out err))
                Console.WriteLine(err);

            //// 写数据库
            //Random rd = new Random();
            //for (int i = 0; i < 100000; i++)
            //{
            //    DataRedis inputData = new DataRedis();
            //    DateTime dtime = DateTime.Now;
            //    inputData.setKeyTime2(
            //        (2018).ToString(),
            //        (rd.Next(1, 12)).ToString(),
            //        (rd.Next(1, 30)).ToString(),
            //        (rd.Next(1, 24)).ToString(),
            //        (rd.Next(1, 60)).ToString(),//分钟
            //        (rd.Next(1, 60)).ToString(),//秒
            //        (rd.Next(1, 1000)).ToString());

            //    inputData.DataJson = i.ToString() + ":" + info;
            //    RedisSaveData(inputData, out err);
            //    if (err != String.Empty)
            //        Console.WriteLine(err);
            //}
            ////写文件
            //this.WriteRedisDbInfo();

            //List<string> KeysList2;
            //RedisGetDataByDateTime("2018", "2018",
            //    "11", "11",//月 
            //    "25", "25",//日
            //    "13", "13",//时
            //    "25", "25",//分
            //    "23", "23",//秒 
            //    "100", "200",//毫秒
            //    out KeysList2);

            // 分不同
            List<string> KeysList3;
            Console.WriteLine("begin ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            RedisGetDataByDateTime("2018", "2018",
                "3", "3",//月 
                "2", "4",//日
                "13", "13",//时
                "30", "31",//分
                "23", "23",//秒 
                "100", "200",//毫秒
                out KeysList3);
            Console.WriteLine("end ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            Console.Read();
        }
    }
}
