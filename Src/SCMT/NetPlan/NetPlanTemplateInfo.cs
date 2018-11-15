using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MIBDataParser.JSONDataMgr;

namespace NetPlan
{
    public class NetPlanTemplateInfo : Singleton<NetPlanTemplateInfo>
    {
        private NetPlanTemplateInfo()
        {

        }
        /// <summary>
        /// 从模板文件获取网元信息
        /// </summary>
        /// <param name="templatePath">模板文件路径</param>
        /// <returns></returns>
        public NPTemplate GetTemplateInfoFromFile(string templatePath)
        {
            NPTemplate npTemplate = null;

            try
            {
                if (!string.IsNullOrEmpty(templatePath) && File.Exists(templatePath))
                {
                    var jsonContent = FileRdWrHelper.GetFileContent(templatePath,Encoding.GetEncoding("gb2312"));
                    npTemplate = JsonHelper.SerializeJsonToObject<NPTemplate>(jsonContent);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return npTemplate;
        }

        public void SaveNetPlanTemplate(NPTemplate template)
        {
            string filepath = FilePathHelper.GetNetPlanTempaltePath() + template.templateName;
            JsonSerializerSettings jsonSetting = new JsonSerializerSettings();
            jsonSetting.DefaultValueHandling = DefaultValueHandling.Include;
            string stringTemJson = JsonConvert.SerializeObject(template, Formatting.Indented, jsonSetting);
            
            try
            {
                FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);//找到文件如果文件不存在则创建文件如果存在则覆盖文件
                //清空文件
                fs.SetLength(0);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write(stringTemJson);
                sw.Flush();
                sw.Close();
            }
            catch
            {
                //记日志
                Console.WriteLine("write file " + filepath + " failed!");
            }

            return;
        }
    }
    /// <summary>
    /// 网规模板信息
    /// </summary>
    public class NPTemplate
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string templateName;
        /// <summary>
        /// 网规列数
        /// </summary>
        public int boardColumn;
        /// <summary>
        /// 网规行数
        /// </summary>
        public int boardRow;

        public List<TemBoardInfo> temBoardInfo;
        public List<TemRRUInfo> temRruInfo;
        public List<TemrHUBInfo> temrHubInfo;
        public List<TemAntInfo> temAntInfo;
        public List<WholeLink> temConnectInfo;

        public NPTemplate()
        {
            temBoardInfo = new List<TemBoardInfo>();
            temRruInfo = new List<TemRRUInfo>();
            temrHubInfo = new List<TemrHUBInfo>();
            temAntInfo = new List<TemAntInfo>();
            temConnectInfo = new List<WholeLink>();
        }
    }
    /// <summary>
    /// 模板板卡信息
    /// </summary>
    public class TemBoardInfo
    {
        /// <summary>
        /// 所在槽号
        /// </summary>
        public int slotNum;
        /// <summary>
        /// 板卡名称
        /// </summary>
        public string boardName;
        /// <summary>
        /// 光口个数
        /// </summary>
        public int irNum;
        /// <summary>
        /// 板卡索引
        /// </summary>
        public string OidIndex;
    }
    /// <summary>
    /// 模板RRU信息
    /// </summary>
    public class TemRRUInfo
    {
        public int rruId;
        /// <summary>
        /// rru名称
        /// </summary>
        public string rruName;
        /// <summary>
        /// rru类型索引
        /// </summary>
        public int rruIndex;      
        /// <summary>
        /// rru通道数
        /// </summary>
        public int rruPathNum;
        /// <summary>
        /// rru工作模式
        /// </summary>
        public string rruWorkMode;
        /// <summary>
        /// rru关联的板卡槽号
        /// </summary>
        public int rruAccessSlotNo;

        public string rruOidIndex;
    }
    /// <summary>
    /// 模板rHUB信息
    /// </summary>
    public class TemrHUBInfo
    {
        public int rHUBId;
        public string rHUBName;
        public int rHUBPathNum;
        public string rHUBWorkMode;       
    }
    /// <summary>
    /// 模板天线信息
    /// </summary>
    public class TemAntInfo
    {
        public int antId;
        public string antName;
        /// <summary>
        /// 天线阵的通道数
        /// </summary>
        public int antArrayNum;
        public string antWorkMode;
        public string antOidIndex;
    }
    /// <summary>
    /// 记录网元器件所在位置
    /// </summary>
    public class TemPostion
    {
        public int posX;
        public int posY;
    }
}
