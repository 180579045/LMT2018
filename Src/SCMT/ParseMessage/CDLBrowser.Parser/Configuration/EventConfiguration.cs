// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventConfiguration.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   The event configuration.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Collections;
    using Common.Logging;

    using SuperLMT.Utils;

    /// <summary>
    /// The event configuration.
    /// </summary>
    public class EventConfiguration : IEventConfiguration
    {
/*
        /// <summary>
        /// The event header tag.
        /// </summary>
        private const string EventHeaderTag = "EventHeader";

        /// <summary>
        /// The event body tag.
        /// </summary>
        private const string EventBodyTag = "Event";

 */

        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(EventConfiguration));

        /// <summary>
        /// The nodes cache,key is "[name],[version]" format
        /// </summary>
        //private readonly IDictionary<int, IConfigNode> configBodyNodesCache = new Dictionary<int, IConfigNode>();
        private Hashtable eventHashTable = new Hashtable();
        /// <summary>
        /// The event config head node.
        /// </summary>
        private IConfigNode eventConfigHeadNode;

        /// <summary>
        /// The xml document.
        /// </summary>
        private XmlDocument xmlDocument;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventConfiguration"/> class.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        public EventConfiguration(string version)
        {
            this.Version = version;

            try{
                XmlElement rootElement = this.GetXmlDocument().DocumentElement;
                if (rootElement != null)
                {
                    XmlNodeList children = rootElement.SelectNodes(@"/EventStructDef/Event");
                    if (children != null)
                    {
                        foreach (XmlNode child in children)
                        {
                            var xmlElement = child as XmlElement;
                            if (xmlElement != null)
                            {
                                string eventId = xmlElement.GetAttribute("ID");
                                int id = Convert.ToInt32(eventId, 16);
                            
                                var eventBodyNode = new ConfigNode(child.Name);
                                eventBodyNode.InitializeAttributes(xmlElement);
                                this.GenerateChildren(child, eventBodyNode);
                                //this.configBodyNodesCache.Add(id, eventBodyNode);
                                if (eventHashTable.Contains(id) == false)
                                {
                                    eventHashTable[id] = eventBodyNode;
                                }
                                else
                                {

                                    System.Diagnostics.Debug.WriteLine("Duplicated == "+ id);   
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
              System.Diagnostics.Debug.WriteLine(ex.StackTrace);   
            }

        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Gets the event head node.
        /// </summary>
        public IConfigNode EventHeadNode
        {
            get
            {
                return this.eventConfigHeadNode
                       ?? (this.eventConfigHeadNode = this.GenerateEventHeaderNode(this.Version));
            }
        }

        /// <summary>
        /// The get event body node by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNode"/>.
        /// </returns>
        public IConfigNode GetEventBodyNodeById(int id)
        {
            if (eventHashTable.ContainsKey(id) == true)
            {
                return (IConfigNode)this.eventHashTable[id];
            }
            Log.Error(string.Format("找不到Version为{0}，Id为{1}的消息！", this.Version, id));
            return null;
            
        }

        /// <summary>
        /// The generate children.
        /// </summary>
        /// <param name="parentXmlNode">
        /// The parent xml node.
        /// </param>
        /// <param name="parentConfigNode">
        /// The parent config node.
        /// </param>
        private void GenerateChildren(XmlNode parentXmlNode, ConfigNode parentConfigNode)
        {
            foreach (XmlNode xmlNode in parentXmlNode.ChildNodes)
            {
                var childNode = new ConfigNode(xmlNode.Name);
                childNode.InitializeAttributes(xmlNode);
                childNode.Parent = parentConfigNode;
                parentConfigNode.Children.Add(childNode);
                this.GenerateChildren(xmlNode, childNode);
            }
        }

        /// <summary>
        /// The generate event header node.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNode"/>.
        /// </returns>
        private IConfigNode GenerateEventHeaderNode(string version)
        {
            try
            {
                var headerNode = new ConfigNode("EventHeaderNode");

                XmlElement rootElement = this.GetXmlDocument().DocumentElement;
                if (rootElement != null)
                {
                    XmlNodeList children = rootElement.SelectNodes(@"/EventStructDef/EventHeader/HeadNode");
                    if (children != null)
                    {
                        foreach (XmlNode child in children)
                        {
                            headerNode.Children.Add(new ConfigNode(child.Name).InitializeAttributes(child));
                        }
                    }
                }

                return headerNode;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("GenerateEventHeaderNode error,version = {0},error message = {1}", version, ex.Message));
                return null;
            }
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
                string configFilePath = this.GetEventConfigFilePath(this.Version);
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

        /// <summary>
        /// The get event configuration file path.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetEventConfigFilePath(string version)
        {
            return AppPathUtiliy.Singleton.GetAppPath() + string.Format(@"\Configuration\Files\eNBCDL_{0}.xml", version);
        }
    }
}
