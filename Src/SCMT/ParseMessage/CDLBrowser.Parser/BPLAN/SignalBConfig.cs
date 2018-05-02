using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace CDLBrowser.Parser.BPLAN
{
    public class SignalBConfig
    {
        public static int currentID
        { get; set; }

        //通过热键启动解析对应序号的文本
        public static Boolean SetScriptTxt(int ID)
        {
            currentID = ID;
            JsonFile jsonFile = new JsonFile();
            JObject jObject = JObject.Parse(jsonFile.ReadFile(@".\script\script_config.json"));

            //更新json配置文件中的currentId
            jObject["currentConfigId"] = ID;
            //删除再创建
            File.Delete(@".\script\script_config.json");
            jsonFile.WriteFile(@".\script\script_config.json", jObject.ToString());
            return true;
        }

        //从配置文件默认读取解析的序号文本
        public static Boolean StartByScriptXml()
        {
            JsonFile jsonFile = new JsonFile();
            if (false == File.Exists(@".\script\script_config.json"))
            {
                Console.WriteLine("script_config.json is not exist");
                return false;
            }
            string configContent = jsonFile.ReadFile(@".\script\script_config.json");

            //按照默认启动
            JObject jObject = JObject.Parse(configContent);
            string defaultConfigID = jObject["defaultConfigID"].ToString().Trim();
            string currentConfigID = jObject["currentConfigId"].ToString().Trim();

            //更新json配置文件中的currentId
            currentID = int.Parse(defaultConfigID);
            jObject["currentConfigId"] = defaultConfigID;
            //删除再创建
            File.Delete(@".\script\script_config.json");
            jsonFile.WriteFile(@".\script\script_config.json", jObject.ToString());

            return true;
        }

    }
}
