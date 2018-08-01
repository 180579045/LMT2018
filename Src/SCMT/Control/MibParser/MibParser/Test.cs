using System;
using System.Collections.Generic;
using MIBDataParser.JSONDataMgr;

namespace MIBDataParser
{
    class NodeB {

        Database dataHandle = Database.GetInstance();

        // 初始化结果返回后处理
        void ResultInitData(bool result)
        {
            if (result)
            {
                Console.WriteLine("init data result is ok");
                testForInitByConnetIp("192.163.2.1");
            }
            else
                Console.WriteLine("init data result is failed");
        }

        public void dosomething(string connectIp)
        {
            // 结果回调
            dataHandle.resultInitData = new ResultInitData(ResultInitData);

            // 初始化
            dataHandle.initDatabase(connectIp);
        }

        /// <summary>
        /// 测试数据库代码
        /// </summary>
        /// <param name="connectIp"></param>
        void testForInitByConnetIp(string connectIp)
        {
            Dictionary<string, Dictionary<string, string>> reTrip = dataHandle.getTrapInfo();

            Dictionary <string, IReDataByEnglishName> reData = new Dictionary<string, IReDataByEnglishName>() {
                { "alarmCausePrimaryAlarmCauseNo",null },
                { "hsdpaCQIReviseLcId", null},
                { "eueTimerT304", null},
                { "cellAdjCellLcId", null}
            };
            dataHandle.testDictExample(reData);


            string err = "";
            // 查询数据 test_1 命令树
            //IReCmdDataByCmdEnglishName reCmdData;
            Dictionary<string, IReCmdDataByCmdEnglishName> reCmdData = 
                new Dictionary<string, IReCmdDataByCmdEnglishName>() {
                { "GetEfdAlarmRule", null},
            };
            if (!dataHandle.getCmdDataByCmdEnglishName(reCmdData, connectIp, out err))
            {
                Console.WriteLine(err);
            }

            // test_2
            Dictionary<string, IReDataByEnglishName> nameInfo = new Dictionary<string, IReDataByEnglishName>() {
                { "alarmCausePrimaryAlarmCauseNo",null },
                //{ "hsdpaCQIReviseLcId", null},
                { "eueTimerT304", null},
                { "cellAdjCellLcId", null},
                { "alarmCauseEntry",null},
            };
            if (!dataHandle.getDataByEnglishName(nameInfo, connectIp, out err))
            {
                Console.WriteLine(err);
            }
            if (null != nameInfo)
                Console.WriteLine("output, {0}", nameInfo["alarmCausePrimaryAlarmCauseNo"].oid);

            IReDataByEnglishName nameEnSigle = new ReDataByEnglishName();
            if (!dataHandle.getDataByEnglishName("alarmCausePrimaryAlarmCauseNo", out nameEnSigle, connectIp, out err))
            {
                Console.WriteLine(err);
            }
            else {
                Console.WriteLine(nameEnSigle.mibDesc);
            }

            // test_3
            //List<IReDataByEnglishName> nameInfoList = new List<IReDataByEnglishName>();
            //List<string> nameEnList = new List<string> { "alarmCausePrimaryAlarmCauseNo",
            //        "hsdpaCQIReviseLcId", "eueTimerT304","cellAdjCellLcId"};
            Dictionary<string, IReDataByEnglishName> nameEnList = 
                new Dictionary<string, IReDataByEnglishName>() {
                    { "alarmCausePrimaryAlarmCauseNo", null},
                    //{ "hsdpaCQIReviseLcId",  null},
                    { "eueTimerT304", null},
                    { "cellAdjCellLcId", null},
                };
            if (!dataHandle.getDataByEnglishName(nameEnList, connectIp, out err))
            {
                Console.WriteLine(err);
            }
            else
            {
                Console.WriteLine("output, ", nameEnList.Values);
            }

            // test_4
            //IReDataByTableEnglishName tableData = new ReDataByTableEnglishName();
            Dictionary<string, IReDataByTableEnglishName> tableData =
                new Dictionary<string, IReDataByTableEnglishName>() {
                    { "alarmCauseTable", null},
                };
            if (!dataHandle.getDataByTableEnglishName(tableData, connectIp, out err))
            {
                Console.WriteLine(err);
            }
            else
            {
                Console.WriteLine( tableData.Values);
            }

            // test_5 : 增加查找标量表节点的校验
            IReDataByOid reDataTest5 = null;
            if (!dataHandle.getDataByOid("1.3.6.1.4.1.5105.100.1.5.2.1.2.4.0", out reDataTest5, connectIp, out err))
            {
                Console.WriteLine(err);
            }
            else
            {
                Console.WriteLine(String.Format("**Test_5 : In_oid={0}, Out_name_en={1}", "1.3.6.1.4.1.5105.100.1.5.2.1.2.4.0", reDataTest5.nameEn));
            }

            Console.WriteLine("testForInitByConnetIp is over.");
            Console.Read();
        }
    }

    class Test
    {
        static void Main(String[] args)
        {
            //
            InitDbByConnectIp();
        }

        static void InitDbByConnectIp()
        {
            Console.WriteLine("begin ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            NodeB b = new NodeB();
            b.dosomething("192.163.2.1");
            Console.WriteLine("end ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
        }

        static void testForCmdJson()
        {
            //解析.mdb文件
            JsonDataManager JsonDataM = new JsonDataManager("5.10.11");
            JsonDataM = new JsonDataManager("5.10.11");
            JsonDataM.ConvertAccessDbToJsonCmdTree();
            Console.WriteLine("write json ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            return;
        }

        static void testForInitDb()
        {
            Console.WriteLine("begin ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            NodeB b = new NodeB();

            //b.dosomething();
            b.dosomething("192.163.2.1");
            Console.WriteLine("end ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));

            //Console.Read();
        }
        
        private static void CloneMe(ICloneable c, string myStr)
        {
            object theClone = c.Clone();
            Console.WriteLine("Your clone is a:{0}",theClone.GetType().Name);
        }

        public void testCmdTree()
        {
            // 2. 解析lm.dtz => json文件(增加，叶子节点的读写属性)
            //解析.mdb文件
            JsonDataManager JsonDataM = new JsonDataManager("5.10.11");
            JsonDataM = new JsonDataManager("5.10.11");
            JsonDataM.ConvertAccessDbToJsonCmdTree();
            Console.WriteLine("write json ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
        }

    }
}
