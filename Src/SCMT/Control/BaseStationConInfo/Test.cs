using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseStationConInfo.Test;

namespace BaseStationConInfo
{
    class BaseStation
    {
        static void Main(string[] args)
        {
            TestBSConInfo test = new TestBSConInfo();
            if (!test.TestBSConInfoM())
            {
                Console.WriteLine("testGetBaseStationConInfo is err");
            }
        }
    }
}
