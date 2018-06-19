using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseStationConInfo.BSCInfoMgr;

namespace BaseStationConInfo.Test
{
    class TestBSConInfo
    {
        public bool TestBSConInfoM()
        {
            if (!testInitBaseStationConInfo())
                return false;
            if (!testGetBaseStationConInfo())
                return false;
            if (!testAddBaseStationConInfo())
                return false;
            if (!testDelBaseStationConInfoByName())
                return false;
            return true;
        }

        /// <summary>
        /// 出事化test
        /// </summary>
        /// <returns></returns>
        private bool testInitBaseStationConInfo()
        {
            testDumpAll();
            //if (!testInitBaseStationConInfo1())
            //    return false;
            testInitBaseStationConInfo0();
            if (!testInitBaseStationConInfo2())
                return false;

            testDumpAll();
            return true;
        }
        private void testInitBaseStationConInfo0()
        {
            BSConInfo bsci = BSConInfo.GetInstance();
            string strName = "25981_";
            string strIp = "192.168.1.";
            for (int i = 0; i < 4; i++)
            {
                if (!bsci.addBaseStationConInfo(strName + i.ToString(), strIp + i.ToString()))
                {
                    Console.WriteLine("addBaseStationConInfo Test3 is err.");
                    return ;
                }
            }
        }
        private bool testInitBaseStationConInfo1()
        {
            BSConInfo bsci = BSConInfo.GetInstance();
            //Dictionary<string, string> cankaoValue = new Dictionary<string, string>();
            string strName = "25981_";
            string strIp = "192.168.1.";
            for (int i = 0; i < 4; i++)
            {
                //cankaoValue.Add(strName + i.ToString(), strIp + i.ToString());
                if (!bsci.addBaseStationConInfo(strName + i.ToString(), strIp + i.ToString()))
                {
                    Console.WriteLine("addBaseStationConInfo Test3 is err.");
                    return false;
                }
            }
            return true;
        }
        private bool testInitBaseStationConInfo2()
        {
            BSConInfo bsci = BSConInfo.GetInstance();
            Dictionary<string, string> cankaoValue = new Dictionary<string, string>();
            string strName = "25981_";
            string strIp = "192.168.1.";
            for (int i = 0; i < 4; i++)
            {
                cankaoValue.Add(strName + i.ToString(), strIp + i.ToString());
            }

            Dictionary<string, string> allConInfo = new Dictionary<string, string>();
            if (!bsci.getBaseStationConInfo(allConInfo))
            {
                Console.WriteLine("getBaseStationConInfo Test1 is err");
                return false;
            }
            foreach (var key in allConInfo.Keys)
            {
                if (!String.Equals(allConInfo[key], cankaoValue[key]))
                    return false;
            }

            return true;
        }


        private bool testGetBaseStationConInfo()
        {
            //sBSConInfo bsci = BSConInfo.GetInstance();
            //1. null input
            if (!testGetBaseStationConInfo1())
            {
                Console.WriteLine("getBaseStationConInfo Test1 is err");
                return false;
            }

            //2. no null input
            if (!testGetBaseStationConInfo2())
            {
                Console.WriteLine("getBaseStationConInfo Test2 is err");
                return false;
            }

            //3. add same info
            if(!testGetBaseStationConInfo3())
            {
                Console.WriteLine("getBaseStationConInfo Test3 is err");
                return false;
            }

            // 清空
            testDumpAll();
            return true;
        }
        private bool testAddBaseStationConInfo()
        {
            testDumpAll();
            if (!testAddBaseStationConInfo1())
            {
                Console.WriteLine("getBaseStationConInfo Test1 is err");
                return false;
            }

            testDumpAll();
            return true;
        }
        private bool testDelBaseStationConInfoByName()
        {
            testDumpAll();
            if (!testDelBaseStationConInfoByName1())
                return false;
            testDumpAll();
            return true;
        }

        private void testDumpAll()
        {
            BSConInfo.GetInstance().delAllBaseStationConInfo();
        }
        private bool testGetBaseStationConInfo1()
        {
            BSConInfo bsci = BSConInfo.GetInstance();
            Dictionary<string, string> allConInfo = null;

            //1. null input
            if (!bsci.getBaseStationConInfo(allConInfo))
            {
                Console.WriteLine("getBaseStationConInfo Test1 is OK");
                return true;
            }
            else
            {
                Console.WriteLine("getBaseStationConInfo Test1 is err");
                return false;
            }
        }
        private bool testGetBaseStationConInfo2()
        {
            BSConInfo bsci = BSConInfo.GetInstance();
            Dictionary<string, string> allConInfo = new Dictionary<string, string>();

            if (bsci.getBaseStationConInfo(allConInfo))
            {
                Console.WriteLine("getBaseStationConInfo Test2 is OK");
                return true;
            }
            else
            {
                Console.WriteLine("getBaseStationConInfo Test2 is err");
                return false;
            }
        }
        private bool testGetBaseStationConInfo3()
        {
            BSConInfo bsci = BSConInfo.GetInstance();
            Dictionary<string, string> allConInfo = new Dictionary<string, string>();
            Dictionary<string, string> cankaoValue = new Dictionary<string, string>();
            string strName = "25981_";
            string strIp = "192.168.1.";
            for (int i = 0; i < 4; i++)
            {
                cankaoValue.Add(strName + i.ToString(), strIp + i.ToString());
                if (!bsci.addBaseStationConInfo(strName + i.ToString(), strIp + i.ToString()))
                {
                    Console.WriteLine("addBaseStationConInfo Test3 is err.");
                    return false;
                }
            }
            if (!bsci.getBaseStationConInfo(allConInfo))
            {
                Console.WriteLine("getBaseStationConInfo Test1 is err");
                return false;
            }
            foreach (var key in cankaoValue.Keys)
            {
                if (!String.Equals(cankaoValue[key], allConInfo[key]))
                    return false;
            }
            return true;
        }
        private bool testAddBaseStationConInfo1()
        {
            if (testGetBaseStationConInfo3())
            {
                return true;
            }
            else
                return false;
        }
        private bool testDelBaseStationConInfoByName1()
        {
            BSConInfo bsci = BSConInfo.GetInstance();

            //1.add some info, get all
            Dictionary<string, string> cankaoValue = new Dictionary<string, string>();
            string strName = "25981_";
            string strIp = "192.168.1.";
            for (int i = 0; i < 4; i++)
            {
                cankaoValue.Add(strName + i.ToString(), strIp + i.ToString());
                if (!bsci.addBaseStationConInfo(strName + i.ToString(), strIp + i.ToString()))
                {
                    Console.WriteLine("addBaseStationConInfo Test3 is err.");
                    return false;
                }
            }
            Dictionary<string, string> allConInfoOne = new Dictionary<string, string>();
            if (!bsci.getBaseStationConInfo(allConInfoOne))
            {
                Console.WriteLine("getBaseStationConInfo Test1 is err");
                return false;
            }

            //2. del one data, get all
            string strDelName = "25981_" + "2";
            if (!bsci.delBaseStationConInfoByName(strDelName))
            {
                Console.WriteLine("delBaseStationConInfoByName is err.");
                return false;
            }
            Dictionary<string, string> allConInfoTwo = new Dictionary<string, string>();
            if (!bsci.getBaseStationConInfo(allConInfoTwo))
            {
                Console.WriteLine("getBaseStationConInfo Test1 is err");
                return false;
            }

            //3. compare one ,two 
            if (false== allConInfoOne.Keys.Contains(strDelName) ||
                true == allConInfoTwo.Keys.Contains(strDelName))
                return false;

            return true;
        }


    }
}
