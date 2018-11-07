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
                    var jsonContent = FileRdWrHelper.GetFileContent(templatePath);
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
            string filepath = FilePathHelper.GetNetPlanTempaltePath() + template.TemplateName;
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
        public string TemplateName;
        public List<RruInfo> rruTypeInfo;
        public List<RHUBEquipment> rHubEquipment;
        public List<AntType> antennaTypeTable;

        public NPTemplate()
        {
            rruTypeInfo = new List<RruInfo>();
            rHubEquipment = new List<RHUBEquipment>();
            antennaTypeTable = new List<AntType>();
        }
    }
}
