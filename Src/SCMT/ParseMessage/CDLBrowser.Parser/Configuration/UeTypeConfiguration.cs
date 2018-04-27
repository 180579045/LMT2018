using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDLBrowser.Parser.Configuration
{
    using System.Xml;

    using Common.Logging;

    using SuperLMT.Utils;
    public class UeTypeConfiguration
    {

        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(UeTypeConfiguration));
        /// <summary>
        /// The xml document.
        /// </summary>
        private XmlDocument xmlDocument;
        /// <summary>
        /// The ueTypes cache
        /// </summary>
        private readonly IDictionary<string, string> ueTypeConfigurations = new Dictionary<string, string>();
        /// <summary>
        /// The instance.
        /// </summary>
        private static  UeTypeConfiguration Instance ;
        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static UeTypeConfiguration Singleton
        {
            get
            {
                if (Instance== null)
                   Instance = new UeTypeConfiguration();
                return Instance;
            }
        }
        public string GetUeTypeByCapabilityStr(string capabilityString)
        {
            try
            {
                if (this.ueTypeConfigurations.ContainsKey(capabilityString))
                    return ueTypeConfigurations[capabilityString];
                else
                    return "未知类型";
                   
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("GetUeTypeByCapabilityStr error,capabilityString={0},error message = {1}",capabilityString, ex.Message));
                return "未知类型";
              
            }

        }
        /// <summary>
        /// Prevents a default instance of the <see cref="UeTypeConfiguration"/> class from being created.
        /// </summary>
        private UeTypeConfiguration()
        {
            GenerateUeTypeCache();
        }
        /// <summary>
        /// The get event configuration file path.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetEventConfigFilePath(string filename)
        {
            return AppPathUtiliy.Singleton.GetAppPath() + string.Format(@"\Configuration\Files\{0}.xml", filename);
        }
        /// <summary>
        /// The get xml document.
        /// </summary>
        /// <returns>
        /// The <see cref="XmlDocument"/>.
        /// </returns>
        private XmlDocument GetXmlDocument()
        {
            try
            {
                string configFilePath = this.GetEventConfigFilePath("UeType");
                if (this.xmlDocument == null)
                {
                    this.xmlDocument = new XmlDocument();

                    this.xmlDocument.Load(configFilePath);
                }
            }
            catch (Exception ex)
            {
                this.xmlDocument = null;
                Log.Error("GetXmlDocument error,message: " + ex.Message);
            }

            return this.xmlDocument;
        }
        private void GenerateUeTypeCache()
        {
            try
            {
                XmlElement rootElement = this.GetXmlDocument().DocumentElement;
                if (rootElement != null)
                {
                    XmlNodeList nodeList = rootElement.ChildNodes;
                    if (nodeList != null)
                    {
                        foreach (XmlNode node in nodeList)
                        {
                            var xmlElement = node as XmlElement;
                            this.ueTypeConfigurations.Add(xmlElement.GetAttribute("UeCapabilityString"), xmlElement.GetAttribute("UeType"));
                        }
                    }
                }

                return ;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("GenerateUeTypeCache error,error message = {0}",ex.Message));
                return ;
            }
        }
    }
}
