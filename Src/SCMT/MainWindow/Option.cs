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
        public xAxis xAxis;

        public Option(legend legend, List<series> series)
        {
            this.legend = legend;
            this.series = series;
        }

        public Option(legend legend, List<series> series, xAxis xAxis)
        {
            this.legend = legend;
            this.series = series;
            this.xAxis = xAxis;
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
        public series(string name, string type, bool smooth, string symbol, string stack, double[] data)
        {
            this.name = name;
            this.type = type;
            this.smooth = smooth;
            this.symbol = symbol;
            this.stack = stack;
            this.data = data;
        }

        public string name { get; set; }
        public string type { get; set; }
        public bool smooth { get; set; }
        public string symbol { get; set; }
        public string stack { get; set; }
        public double[] data { get; set; }
    }

    // ECharts 横坐标类型
    public class xAxis
    {
        public xAxis(string[] data)
        {
            this.data = data;
        }

        // 后续可以添加更多参数
        public xAxis(string type, bool boundaryGap, string[] data)
        {
            this.type = type;
            this.boundaryGap = boundaryGap;
            this.data = data;
        }

        public string[] data { get; set; }
        // 后续可以添加更多属性
        public string type { get; set; }
        public bool boundaryGap { get; set; }
    }
}
