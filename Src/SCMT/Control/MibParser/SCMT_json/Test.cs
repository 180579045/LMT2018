using System;
using SCMT_json.JSONDataMgr;

namespace SCMT_json
{
    class NodeB {
        Database test;
        void ResultInitData(bool result)
        {
            if (result)
            {
                Console.WriteLine("init data result is ok");

                // 查询数据
                IReDataByEnglishName nameInfo = new ReDataByEnglishName();
                test.getDataByEnglishName("srsResourceSetId", out nameInfo);
                Console.WriteLine("output, {0}", nameInfo.oid);
            }
            else
                Console.WriteLine("init data result is failed");
        }

        public void dosomething()
        {
            test = new Database();


            if (!test.initDatabase())
            {
                Console.WriteLine("note b initDatabase faild. ");
            }
            else {
                Console.WriteLine("test.initDatabase ok. ");
            }
            test.resultInitData = new ResultInitData(ResultInitData);

        }
    }

    class Test
    {
        static void Main(String[] args)
        {
            // 2. 解析lm.dtz => json文件(增加，叶子节点的读写属性)
            //解析.mdb文件
            //JsonDataManager JsonDataM = new JsonDataManager("5.10.11");
            //JsonDataM = new JsonDataManager("5.10.11");
            //JsonDataM.ConvertAccessDbToJsonCmdTree();
            //Console.WriteLine("write json ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            //return;


            Console.WriteLine("begin ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            NodeB b = new NodeB();
            b.dosomething();
            Console.WriteLine("end ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));

            Console.Read();
            //if (!test.getDateByOid("1.3.6.1.4.1.5105.1.2.100.1.1.5.6.1.20.33", out te))
            { }
        }

        static void Main123(String[] args)
        {
            //bool isTest = false;
            Console.WriteLine("begin ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));

            // 初始化
            // 1. 解压lm.dtz
            UnzippedLmDtz unZip = new UnzippedLmDtz();
            string err = "";
            if (!unZip.UnZipFile(out err))
            {
                Console.WriteLine("Err:Unzip fail, {0}", err);
                return;
            }
            Console.WriteLine("unzip ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));

            // 2. 解析lm.dtz => json文件(增加，叶子节点的读写属性)
            //解析.mdb文件
            JsonDataManager JsonDataM = new JsonDataManager("5.10.11");
            JsonDataM = new JsonDataManager("5.10.11");
            JsonDataM.ConvertAccessDbToJson();
            Console.WriteLine("write json ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));

            // 3. 解析json 文件
            MibInfoList mibL = new MibInfoList();
            mibL.GeneratedMibInfoList();
            Console.WriteLine("mib list ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            //mibL.getOidEnInfo(@"1.3.6.1.4.1.5105.1.2.100.1.1.5.6.1.20.33",out oidInfo);

            // 4. 查询 精确查询  指定的oid、nameEn、nameCh 
            dynamic oidInfo;
            string soid = @"2.2.3.1.1.1.5.4.3.2.1";
            mibL.getOidEnInfo(soid, out oidInfo);
            Console.WriteLine("output, {0}", oidInfo["nameMib"]);

            dynamic nameInfo;
            mibL.getNameEnInfo("srsResourceSetId",out nameInfo);
            Console.WriteLine("output, {0}", nameInfo["oid"]);
            
            Console.WriteLine("end ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            return;
        }

        private static void CloneMe(ICloneable c, string myStr)
        {
            object theClone = c.Clone();
            Console.WriteLine("Your clone is a:{0}",theClone.GetType().Name);
        }

        static void test()
        {
            string myStr = "hello";
            //CloneMe(myStr);
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
