using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCMT_json.JSONDataMgr;
using SCMT_json.Interface.UISnmp;
using Newtonsoft.Json.Linq;
using System.IO;
using SCMT_json.Interface.Redis;
using System.Data;

namespace SCMT_json
{
    class Test
    {
        
        static JsonDataManager JsonDataM;// = new JsonDataManager("5.10.11");;
        static void Main(String[] args)
        {
            Console.WriteLine("begin ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            MibInfoList mibL = new MibInfoList();
            mibL.GeneratedMibInfoList();
            dynamic oidInfo;
            //mibL.getOidEnInfo(@"1.3.6.1.4.1.5105.1.2.100.1.1.5.6.1.20.33",out oidInfo);

            string soid = @"2.2.3.1.1.1.5.4.3.2.1";
            mibL.getOidEnInfo(soid, out oidInfo);
            Console.WriteLine("output, {0}", oidInfo);
#if false           
            // 一. 初始化
            // 1. 解压lm.dtz
            UnzippedLmDtz unZip = new UnzippedLmDtz();
            string err = "";
            if (!unZip.UnZipFile(out err))
            {
                Console.WriteLine("unzip fail, {0}",err);
                return;
            }
            Console.WriteLine("unzip ok ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            

            // 2. 解析lm.dtz => json文件(增加，叶子节点的读写属性)
            //解析.mdb文件
            JsonDataManager JsonDataM = new JsonDataManager("5.10.11");
            JsonDataM = new JsonDataManager("5.10.11");
            JsonDataM.ConvertAccessDbToJson();
            Console.WriteLine("write json ok. ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            
            /*
            // 3. 解析json   => redis(实验，都在一个数据库是否可行。分片技术给不同ip。)
            RedisOperation test = new RedisOperation();
            
            Console.WriteLine("init redis ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            dynamic jsonData;
            if (!test.get_json_data(out jsonData))
                return ;
            Console.WriteLine("get json ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            // save json data into redis
            if (!test.save_json_into_redis(jsonData))
                return ;
            Console.WriteLine("end ====, time is " + DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒"));
            
            // 二. 查询
            // 4. 精确查询  指定的oid、nameEn、nameCh 
            dynamic tableInfo;
            if (!test.get_type0_from_redis("lbc40FcEntry", out tableInfo))
            {
                Console.WriteLine("get_type0_from_redis get {}: fail!", "lbc40FcEntry");
                ///return false;
            }
            
            var nameCh = tableInfo["nameCh"];
            var childList = tableInfo["childList"];
            foreach (var item in childList)
            {
                Console.WriteLine("get_type0(nameEn),nameEn({0}),nameCh({1}),oid({2})....", item["childNameMib"], item["childNameCh"], item["childOid"]);
            }
            */

            //RedisOperation test = new RedisOperation();
            //test.getLeafOidByName("");
            //if ( !test.help_test_example())
            //    Console.WriteLine("====");
            //test.saveJsonFileIntoRedis();
            //var testcon = test.ConnectionRedis("127.0.0.1:6379");
            //var t1 = test.getAllInfoFromRedis(testcon);

#endif
            int i = 1;
            //解压缩文件
            //UnzipDtzTest();

            //test.test();
            // redis 操作
            //RedisOperation test = new RedisOperation();
            //test.saveJsonFileIntoRedis("");
            //test.test();

            //解析.mdb文件
            //JsonDataManager JsonDataM = new JsonDataManager("5.10.11");
            //JsonDataM = new JsonDataManager("5.10.11");
            //JsonDataM.ConvertAccessDbToJson("D:\\C#\\SCMT\\output\\lm.mdb");


            //UI发来的请求消息,得到snmp形式消息
            //UIMessageSendToSNMPTest(JsonDataM);

            //snmp发来的请求消息,得到UI形式消息
            //SnmpMessageSendToUITest(JsonDataM);

            //从snmp中的OID拆分出 IndexContent(表索引)
            //List<string> re = JsonDataM.splitSnmpOidForIndexContentAndOid("2.100.1.1.1.1.1.1.7");

            //UI发来的查询消息
            //Console.WriteLine(JsonDataM.SearchMessageDealfromUI("下一级射频单元开关"));
            //Console.WriteLine("==================");
            //Console.WriteLine(JsonDataM.SearchMessageDealfromUI("1.1.1.1.1.3"));
            //Console.WriteLine("==================");
            //Console.WriteLine(JsonDataM.GetTableTitleInfoByUI("fileUploadEntry"));

            /*
            //JObject resultJObject = new JObject();
            //Console.WriteLine("==================");
            Console.WriteLine(JsonDataM.SearchOidFromJsonByEnglishName("alarmCauseSeverity"));

            //Console.WriteLine("==================");
            Console.WriteLine(JsonDataM.SearchOidFromJsonByEnglishName("rlcPreset40"));

            //messageType: 0 get, 1 set, 2 trap, 3 response, 4 getnext
            JObject trapMessage = new JObject{ {"requestId","121227"}, { "messageType", "0" }, { "errorStatus","0"}, { "errorIndex", "0"} };
            JArray oidList = new JArray
            {
                new JObject { { "OID", "1.3.6.1.6.3.1.1.4.1.0" }, { "value", "1.3.6.1.4.1.5105.100.1.5.2.1.1.1" } },
                new JObject { { "OID", "1.3.6.1.4.1.5105.100.1.5.2.1.2.1.0" }, { "value", "5"} },
                new JObject { { "OID", "1.3.6.1.4.1.5105.100.1.5.2.1.2.2.0" }, { "value", "100"} },
                new JObject { { "OID", "1.3.6.1.4.1.5105.100.1.5.2.1.2.3.0" }, { "value", "3"} },
                new JObject { { "OID", "1.3.6.1.4.1.5105.100.1.5.2.1.2.4.0" }, { "value", "07:e2:03:1d:08:02:28:00:2b:08:00" } },
                new JObject { { "OID", "1.3.6.1.4.1.5105.100.1.2.2.1.2.0" }, { "value", "20"} },
            };
            trapMessage.Add("oidList", oidList);
            Console.WriteLine(trapMessage.ToString());

            //messageType: 0 get, 1 set
            JObject snmpMessage = new JObject { { "requestId", "8236" }, { "messageType", "0" }, { "errorStatus", "0" }, { "errorIndex", "0" } };


            //UI message
            JObject UIGetMessage = new JObject { { "requestId", "8236" }, { "messageType", "0" }, { "errorStatus", "0" }, { "errorIndex", "0" }, {"tableNameMib", "localCellEntry" } };
            JArray UIGetList = new JArray
            {
                new JObject { { "OID", "1.3.6.1.6.3.1.1.4.1.0" }, { "value", "1.3.6.1.4.1.5105.100.1.5.2.1.1.1" } },
                new JObject { { "OID", "1.3.6.1.4.1.5105.100.1.5.2.1.2.1.0" }, { "value", "5"} },
                new JObject { { "OID", "1.3.6.1.4.1.5105.100.1.5.2.1.2.2.0" }, { "value", "100"} },
                new JObject { { "OID", "1.3.6.1.4.1.5105.100.1.5.2.1.2.3.0" }, { "value", "3"} },
                new JObject { { "OID", "1.3.6.1.4.1.5105.100.1.5.2.1.2.4.0" }, { "value", "07:e2:03:1d:08:02:28:00:2b:08:00" } },
                new JObject { { "OID", "1.3.6.1.4.1.5105.100.1.2.2.1.2.0" }, { "value", "20"} },
            };
            */
            return;
        }

        /*解压缩文件
        static string UnzipDtzTest()
        {
            UnzippedLmDtz test = new UnzippedLmDtz();
            string err = test.UnZipFile();
            Console.WriteLine(err);
            return err;
        }*/

        //UI发来的请求消息,得到snmp形式消息
        static bool UIMessageSendToSNMPTest(JsonDataManager JsonDataM)
        {
            // 组消息结构体
            UIInfoInterface UiMessage = new UIInfoInterface();
            UiMessage.RequestId = "121227";
            UiMessage.MessageType = "0";
            UiMessage.ErrorStatus = "0";
            UiMessage.ErrorIndex = "0";
            UiMessage.TableNameMib = "localCellEntry";
            UiMessage.IndexContent = ".2";
            UiMessage.addNewLeafList("lcAntArrayMode", "1");
            UiMessage.addNewLeafList("lcMaxDlPower", "5");

            //Console.WriteLine(UiMessage);

            //发送消息
            SNMPInfoInterface re = JsonDataM.DcMessageDealfromUI(UiMessage);
            Console.WriteLine("======UIMessageSendToSNMP=====");
            Console.WriteLine(re.OidLeafLists[0].Oid);
            Console.WriteLine(re.OidLeafLists[1].Oid);
            return true;
        }

        //snmp发来的请求消息,得到UI形式消息
        static bool SnmpMessageSendToUITest(JsonDataManager JsonDataM)
        {
            // 组消息结构体
            SNMPInfoInterface snmpMessage = new SNMPInfoInterface();
            snmpMessage.RequestId = "121227";
            snmpMessage.MessageType = "0";
            snmpMessage.ErrorStatus = "0";
            snmpMessage.ErrorIndex = "0";
            snmpMessage.addNewOidLeafList("2.4.4.1.1.4.20", "1");
            snmpMessage.addNewOidLeafList("2.4.4.1.1.8.20", "5");

            //发送消息
            UIInfoInterface re = JsonDataM.DcMessageDealfromSnmp(snmpMessage);
            Console.WriteLine("=====SnmpMessageSendToUI======");
            Console.WriteLine(re.IndexContent);
            Console.WriteLine(re.LeafLists[0].ChildNameMib);
            Console.WriteLine(re.LeafLists[1].ChildNameMib);
            return true;
        }
    }
}
