// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationManager.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the ConfigurationMananger type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using System.Collections.Generic;
    using System.Xml;
    using System.Text.RegularExpressions;
    using System.IO;
    using SuperLMT.Utils;

    /// <summary>
    /// The configuration manager.
    /// </summary>
    public class ConfigurationManager
    {
        /// <summary>
        /// The manager.
        /// </summary>
        private static readonly ConfigurationManager Manager = new ConfigurationManager();

        /// <summary>
        /// The nodes cache,key is "[name],[version]" format
        /// </summary>
        private readonly IDictionary<string, IEventConfiguration> eventConfigurations = new Dictionary<string, IEventConfiguration>();

        /// <summary>
        /// The cdl file common head node.
        /// </summary>
        private IConfigNode cdlFileCommonHeadNode;

        /// <summary>
        /// The events head node.
        /// </summary>
        private IConfigNode eventsHeadNode;
        /// <summary>
        /// The signal trace configuration.
        /// </summary>
        //private ISignalTraceConfiguration signalTraceConfiguration = new SignalTraceConfiguration();

        /// <summary>
        /// Prevents a default instance of the <see cref="ConfigurationManager"/> class from being created.
        /// </summary>
        public ConfigurationManager()
        {
        }

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static ConfigurationManager Singleton
        {
            get
            {
                return Manager;
            }
        }
      
        /// <summary>
        /// Gets or sets the current configuration.
        /// </summary>
        public IEventConfiguration CurrentConfiguration { get; set; }

        /// <summary>
        /// Gets the cdl file common head node.
        /// </summary>
        public IConfigNode CDLFileCommonHeadNode
        {
            get
            {
                if (ReferenceEquals(this.cdlFileCommonHeadNode, null))
                {
                    this.cdlFileCommonHeadNode = new ConfigNode("FileCommonHeadNode");
                    var fileStrumentDoc = new XmlDocument();
                    fileStrumentDoc.Load(AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\eNBCDLCommonDef.xml");
                    var nodes = fileStrumentDoc.SelectNodes(@"/Document/FileHeader/HeadNode");
                    if (nodes != null)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            this.cdlFileCommonHeadNode.Children.Add(new ConfigNode(node.Name).InitializeAttributes(node));
                          
                        }
                    }
                }

                return this.cdlFileCommonHeadNode;
            }
        }

        /// <summary>
        /// Gets the signal trace configuration.
        /// </summary>
        //public ISignalTraceConfiguration SignalTraceConfiguration
        //{
        //    get
        //    {
        //        return this.signalTraceConfiguration ?? (this.signalTraceConfiguration = new SignalTraceConfiguration());
        //    }
        //}

        /// <summary>
        /// Gets the cdl event head node.
        /// </summary>
        public IConfigNode CDLEventsHeadNode
        {
            get
            {
                if (ReferenceEquals(this.eventsHeadNode, null))
                {
                    this.eventsHeadNode = new ConfigNode("eventsHeadNode");
                    var fileStrumentDoc = new XmlDocument();
                    fileStrumentDoc.Load(AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\eNBCDLCommonDef.xml");
                    var nodes = fileStrumentDoc.SelectNodes(@"/Document/Events/EventsHeader/HeadNode");
                    if (nodes != null)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            this.eventsHeadNode.Children.Add(new ConfigNode(node.Name).InitializeAttributes(node));
                        }
                    }
                }

                return this.eventsHeadNode;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IList<EventsStatisticRecord> CDLEventsStatisticItems()
        {
                IList<EventsStatisticRecord> eventsStatisticItems = new List<EventsStatisticRecord>();
                    var nodes = GetEventStatisticXml().FirstChild.ChildNodes;
                    Regex regex = new Regex("<!--(.*?)");
                    if (nodes != null)
                    {
                        foreach (XmlNode node in nodes)
                        {
                            if (!regex.IsMatch(node.OuterXml))
                            {
                                string eventName = node.Attributes[0].Value.ToString().Trim();
                                foreach (XmlNode typenode in node.ChildNodes)
                                {
                                    string typeName = typenode.Attributes[0].Value.ToString().Trim();
                                    foreach (XmlNode argumentnode in typenode.ChildNodes)
                                    {
                                        string argumentName = argumentnode.Attributes[0].Value.ToString().Trim();
                                        eventsStatisticItems.Add(new EventsStatisticRecord(eventName, typeName, argumentName));
                                    }


                                }
                            }
                        }
                    }

                    return eventsStatisticItems;
            

        }
        /// <summary>
        /// Get EventStatistic Xml
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetEventStatisticXml()
        {
            var fileStrumentDoc = new XmlDocument();
            fileStrumentDoc.Load(AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\EventsStatistic.xml");
            return fileStrumentDoc;
        }
        /// <summary>
        /// Write EventStatistic Xml
        /// </summary>
        /// <param name="xmlDoc"></param>
        public void WriteEventStatisticXml(XmlDocument xmlDoc)
        {
            if (File.Exists(AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\EventsStatistic.xml"))
            {
                File.Delete(AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\EventsStatistic.xml");
            }
           xmlDoc.Save(AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\EventsStatistic.xml");
        }
       
        /// <summary>
        /// The get event configuration.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="IEventConfiguration"/>.
        /// </returns>
        public IEventConfiguration GetEventConfiguration(string version)
        {
            if (version == null)
                return null;

            string key = this.GenerateKey("EventConfiguration", version);
            if (!this.eventConfigurations.ContainsKey(key))
            {
                this.eventConfigurations.Add(key, new EventConfiguration(version));
            }

            return this.eventConfigurations[key];
        }

        /// <summary>
        /// The generate key,"[name],[version]"
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GenerateKey(string name, string version)
        {
            return string.Format("[{0}],[{1}]", name, version);
        }

        public UeTypeConfiguration GetUeTypeConfiguration()
        {
            return UeTypeConfiguration.Singleton;
        }
    }
}
