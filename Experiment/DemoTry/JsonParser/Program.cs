using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonParser
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadJsonFile();
            System.Console.ReadKey();
        }

        static void LoadJsonFile()
        {
            JObject Obj = new JObject();
            using (StreamReader reader = File.OpenText(@"Data\Tree_Reference2.json"))
            {
                Obj = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            }
            string ret1 = (string)Obj["ObjTree"]["version"];

            // 数组中是这样访问;
            var ret = from p in Obj["ObjTree"]["Nodes"][2]["Nodes"]
                      where (string)p["name"] == "设备软件许可控制"
                      select (string)p["id"];

            var SftChdCnt = from cnt in Obj["ObjTree"]["Nodes"]
                            where (int)cnt["childrencount"] == 1
                            select (string)cnt["name"];

            foreach(var a in Obj)
            {

            }

            foreach (var iter in ret)
            {
                Console.WriteLine(iter);
            }
            foreach(var iter in SftChdCnt)
            {
                Console.WriteLine(iter);
            }
            
        }
    }
}
