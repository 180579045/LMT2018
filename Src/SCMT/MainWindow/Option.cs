using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCMTMainWindow
{
    public class Option
    {
        public legend legend;
        public List<series> series = new List<series>();
        public Option(legend legend, List<series> series){
            this.legend = legend;
            this.series = series;
        }
        

        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        // 从一个Json串生成对象信息  
        public static object JsonToObject(string jsonString, object obj)
        {
            return JsonConvert.DeserializeObject(jsonString, obj.GetType());
        }
    }
    public class legend
    {
        public legend(List<string> data)
        {
            this.data = data;
        }
        public List<string> data = new List<string>();
    }
    public class series
    {

        public series(string name, string type, string smooth, string symbol, string stack)
        {
            this.name = name;
            this.type = type;
            this.smooth = smooth;
            this.symbol = symbol;
            this.stack = stack;
        }
        public string name { get; set; }
        public string type { get; set; }
        public string smooth { get; set; }
        public string symbol { get; set; }
        public string stack { get; set; }
    }
}
