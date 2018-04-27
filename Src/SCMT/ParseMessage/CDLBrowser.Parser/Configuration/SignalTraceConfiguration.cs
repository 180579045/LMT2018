// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalTraceConfiguration.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the SignalTraceConfigration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using System;
    using System.Xml;

    using Common.Logging;

    using SuperLMT.Utils;

    /// <summary>
    /// The signal trace configuration.
    /// </summary>
    public class SignalTraceConfiguration : ISignalTraceConfiguration
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SignalTraceConfiguration));

        /// <summary>
        /// The xml document.
        /// </summary>
        private XmlDocument xmlDocument;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalTraceConfiguration"/> class.
        /// </summary>
        internal SignalTraceConfiguration()
        {
            this.Initialize();
        }

        /// <summary>
        /// Gets the  packet header config node.
        /// </summary>
        public IConfigNode PacketHeaderConfigNode { get; private set; }

        /// <summary>
        /// Gets the  message header config node.
        /// </summary>
        public IConfigNode MessageHeaderConfigNode { get; private set; }

        /// <summary>
        /// Gets the get message body config node.
        /// </summary>
        public IConfigNode MessageBodyConfigNode { get; private set; }

        /// <summary>
        /// Gets the get command config node.
        /// </summary>
        public IConfigNode GetCommandConfigNode { get; private set; }

        /// <summary>
        /// Gets the get cell command config node.
        /// </summary>
        public IConfigNode GetCellCommandConfigNode { get; private set; }

        /// <summary>
        /// Gets the add command config node.
        /// </summary>
        public IConfigNode AddCommandConfigNode { get; private set; }

        /// <summary>
        /// Gets the delete command config node.
        /// </summary>
        public IConfigNode DeleteCommandConfigNode { get; private set; }

        /// <summary>
        /// The initialize.
        /// </summary>
        private void Initialize()
        {
            try
            {
                XmlElement rootElement = this.GetXmlDocument().DocumentElement;
                if (rootElement != null)
                {
                    this.PacketHeaderConfigNode = new ConfigNode("PacketHeaderConfigNode");
                    XmlNodeList children = rootElement.SelectNodes(@"/Document/MessageCommonHead/HeadNode");
                    if (children != null)
                    {
                        foreach (XmlNode child in children)
                        {
                            this.PacketHeaderConfigNode.Children.Add(new ConfigNode(child.Name).InitializeAttributes(child));
                        }
                    }

                    this.MessageHeaderConfigNode = new ConfigNode("MessageHeaderNode");
                    children = rootElement.SelectNodes(@"/Document/Message/MessageHeader/ParaNode");
                    if (children != null)
                    {
                        foreach (XmlNode child in children)
                        {
                            this.MessageHeaderConfigNode.Children.Add(new ConfigNode(child.Name).InitializeAttributes(child));
                        }
                    }

                    this.MessageBodyConfigNode = new ConfigNode("MessageBodyNode");
                    children = rootElement.SelectNodes(@"/Document/Message/MessageBody/ParaNode");
                    if (children != null)
                    {
                        foreach (XmlNode child in children)
                        {
                            this.MessageBodyConfigNode.Children.Add(new ConfigNode(child.Name).InitializeAttributes(child));
                        }
                    }
                }

                XmlElement traceCommandConfigRootElement = this.GetTraceCommandConfigDocument().DocumentElement;
                if (null != traceCommandConfigRootElement)
                {
                    this.GetCommandConfigNode = new ConfigNode("GetCommandConfigNode");
                    XmlNodeList children = traceCommandConfigRootElement.SelectNodes(@"/TraceInfo/GetTrace/ParaNode");
                    if (children != null)
                    {
                        foreach (XmlNode child in children)
                        {
                            this.GetCommandConfigNode.Children.Add(new ConfigNode(child.Name).InitializeAttributes(child));
                        }
                    }

                    this.GetCellCommandConfigNode = new ConfigNode("GetCellCommandConfigNode");
                    children = traceCommandConfigRootElement.SelectNodes(@"/TraceInfo/GetCellInfo/ParaNode");
                    if (children != null)
                    {
                        foreach (XmlNode child in children)
                        {
                            this.GetCellCommandConfigNode.Children.Add(new ConfigNode(child.Name).InitializeAttributes(child));
                        }
                    }

                    this.AddCommandConfigNode = new ConfigNode("AddCommandConfigNode");
                    children = traceCommandConfigRootElement.SelectNodes(@"/TraceInfo/AddTrace/ParaNode");
                    if (children != null)
                    {
                        foreach (XmlNode child in children)
                        {
                            this.AddCommandConfigNode.Children.Add(new ConfigNode(child.Name).InitializeAttributes(child));
                        }
                    }

                    this.DeleteCommandConfigNode = new ConfigNode("DeleteCommandConfigNode");
                    children = traceCommandConfigRootElement.SelectNodes(@"/TraceInfo/DelTrace/ParaNode");
                    if (children != null)
                    {
                        foreach (XmlNode child in children)
                        {
                            this.DeleteCommandConfigNode.Children.Add(new ConfigNode(child.Name).InitializeAttributes(child));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Initialize error,error message = {0}", ex.Message));
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
                string configFilePath = AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\SignalingMessageDef.xml";
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
        /// The get trace command config document.
        /// </summary>
        /// <returns>
        /// The <see cref="XmlDocument"/>.
        /// </returns>
        private XmlDocument GetTraceCommandConfigDocument()
        {
            try
            {
                var document = new XmlDocument();
                string configFilePath = AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\TracingTaskConfig.xml";

                document.Load(configFilePath);
                return document;
            }
            catch (Exception ex)
            {
                this.xmlDocument = null;
                Log.Error("GetXmlDocument error,message: " + ex.Message);
                return null;
            }
        }
    }
}
