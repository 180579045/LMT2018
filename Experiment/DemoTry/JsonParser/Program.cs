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
        }

        static void LoadJsonFile()
        {
            using (StreamReader reader = File.OpenText(@"Data\Tree_Reference.json"))
            {
                JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            }
        }
    }
}
