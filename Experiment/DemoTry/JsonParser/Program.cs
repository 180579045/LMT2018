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
            Console.WriteLine("The JSON file first Object:\n" +Obj.First.First);
            Console.WriteLine("The JSON file first Object's next Object:\n" +Obj.First.Next.First[1]);
            Console.WriteLine("The JSON file last Object:" +Obj.Last);

            // 2、通过JSON键值对访问数值;
            Console.WriteLine("According HashVal:" + Obj.First.Next.First[1]["ObjName"]);

            Console.WriteLine("-----------------------------------------------------");

            // 3、通过Linq查询对应的内容;
            IEnumerable<string> ret = from p in Obj.First.Next.First
                                      where (string)p["ObjName"] == "设备软件许可控制"
                                      select (string)p["ObjID"];
            foreach (string iter in ret)
            {
                Console.WriteLine("设备软件许可控制的id是:"+iter);
            }

            // 4、通过Linq筛选对应的内容;
            IEnumerable<string> ret2 = from p in Obj.First.Next.First
                                      select (string)p["ObjID"];
            Console.WriteLine("Count:" +ret2.Count());
            foreach (string iter in ret2)
            {
                Console.Write(iter +",");
            }
            

        }
    }
}
