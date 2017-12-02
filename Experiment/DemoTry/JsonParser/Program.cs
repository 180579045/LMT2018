using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

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
            // 通过文件读取JSON对象,将JSON对象保存到JObject;
            using (StreamReader reader = File.OpenText(@"Data\Tree_Reference2.json"))
            {
                Obj = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            }

            // 1、直接通过数组下标访问JSON对象;
            Console.WriteLine(Obj.First.Next.First[1]);

            // 2、通过JSON键值对访问数值;
            Console.WriteLine("The ObjName is: " + Obj.First.Next.First[1]["ObjName"]);

            Console.WriteLine("-----------------------------------------------------");

            // 3、通过Linq查询对应的内容;
            IEnumerable<string> ret = from p in Obj["NodeList"]
                      where (string)p["ObjName"] == "设备软件许可控制"
                      select (string)p["ObjID"];
            
            foreach (string iter in ret)
            {
                Console.WriteLine(iter);
            }

        }
    }
}
