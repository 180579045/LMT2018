// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecondaryParser.cs" company="datangmobile">
//   Secondary
// </copyright>
// <summary>
//   Defines the SecondaryParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document
{
    using System;
    using System.IO;
    using CDLBrowser.Parser.Configuration;
    using CDLBrowser.Parser.Document.Event;
    using CDLBrowser.Parser.Document.EventTree;

    using Common.Logging;

    /// <summary>
    /// The secondary parser.
    /// </summary>
    public class SecondaryParser
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SecondaryParser));

        /// <summary>
        /// The parser.
        /// </summary>
        private static readonly SecondaryParser Parser = new SecondaryParser();

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static SecondaryParser Singleton
        {
            get
            {
                return Parser;
            }
        }

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public IConfigNodeWrapper Parse(IEvent e)
        {
                try
                {
                    if(e.RawData == null)
                    {
                        return null;
                    }
                    var stream = new MemoryStream(e.RawData);

                    //IEventConfiguration eventConfiguration = new ConfigurationManager().GetEventConfiguration(e.Version);
					IEventConfiguration eventConfiguration = ConfigurationManager.Singleton.GetEventConfiguration(e.Version);
                    IConfigNode eventConfigHeadNode = eventConfiguration.EventHeadNode;

                    if (eventConfigHeadNode == null)
                    {
                        return null;
                    }

                    var rootNode = new EventTreeRootNode { ConfigurationNode = eventConfigHeadNode, EventIndex = e.DisplayIndex };

                    foreach (var child in eventConfigHeadNode.Children)
                    {
                        rootNode.Children.Add(new ConfigNodeWrapper { ConfigurationNode = child, Parent = rootNode });
                    }

                    rootNode.InitializeValue(stream, e.Version);

                    int eventId;
                    if (Int32.TryParse(rootNode.GetChildNodeById("EventType").Value.ToString(), out eventId) == true)
                    {
                        
                        IConfigNode eventConfigBodyNode = eventConfiguration.GetEventBodyNodeById(
                            Convert.ToInt32(eventId));

                        if (eventConfigBodyNode != null)
                        {
                            // wrong "EventType" cause null
                            var eventTreeBodyNode = new EventTreeBodyNode { ConfigurationNode = eventConfigBodyNode, Parent = rootNode };
                            this.GenerateEventTreeChildrenNode(eventTreeBodyNode);
                            rootNode.Children.Add(eventTreeBodyNode);
                            eventTreeBodyNode.InitializeValue(stream, e.Version);
                        }

                        return rootNode;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("二次解析出错，event id = {0},error message = {1}", e.EventIdentifier, ex.Message));
                    return null;
                }
                return null;
            
        }

        /// <summary>
        /// The generate event tree node.
        /// </summary>
        /// <param name="parent">
        /// The parent.
        /// </param>
        public void GenerateEventTreeChildrenNode(ConfigNodeWrapper parent)
        {
            foreach (var child in parent.ConfigurationNode.Children)
            {
                string displayType = child.GetAttribute("DisplayType");
                if (!string.IsNullOrEmpty(displayType))
                {
                    if (0 == string.Compare(displayType, "Asn", StringComparison.OrdinalIgnoreCase))
                    {
                        var eventTreeAsnParentNode = new EventTreeAsnParentNode { ConfigurationNode = null, Parent = parent };
                        eventTreeAsnParentNode.Children.Add(new EventTreeAsnNode { ConfigurationNode = child, Parent = eventTreeAsnParentNode });
                        parent.Children.Add(eventTreeAsnParentNode);
                        continue;
                    }

                    if (0 == string.Compare(displayType, "Bits", StringComparison.OrdinalIgnoreCase))
                    {
                        var bitsNode = new EventTreeBitsNode { ConfigurationNode = child, Parent = parent };
                        parent.Children.Add(bitsNode);
                        continue;
                    }

                    if (0 == string.Compare(displayType, "Convert", StringComparison.OrdinalIgnoreCase))
                    {
                        var eventTreeSpecifiedNode = new EventTreeSpecifiedNode { ConfigurationNode = child, Parent = parent };
                        parent.Children.Add(eventTreeSpecifiedNode);
                        continue;
                    }
                }

                ConfigNodeWrapper eventTreeChildNode;

                string nodeName = child.Name;
                if (0 == string.Compare(nodeName, "ParaStruct", StringComparison.OrdinalIgnoreCase))
                {
                    eventTreeChildNode = new EventTreeBodyStructsNode { ConfigurationNode = child, Parent = parent };
                }
                else
                {
                    eventTreeChildNode = new ConfigNodeWrapper { ConfigurationNode = child, Parent = parent };
                }

                this.GenerateEventTreeChildrenNode(eventTreeChildNode);
                parent.Children.Add(eventTreeChildNode);
            }
        }

        /// <summary>
        /// The get expand node quote.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public IConfigNodeWrapper GetExpandNodeQuote(IEvent e)
        {
            try
            {
                var nodeWrapper = this.Parse(e);
                if (nodeWrapper == null)
                {
                    return null;
                }

                foreach (IConfigNodeWrapper expandDataNode in nodeWrapper.Children)
                {
                    var eventTreeNode = new EventTreeBodyNode();
                    Type eventTreeType = eventTreeNode.GetType();
                    Type nodetype = expandDataNode.GetType();
                    if (eventTreeType == nodetype)
                    {
                        return expandDataNode;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("GetExpandNodeQuoteException event id = {0},error message = {1}", e.EventIdentifier, ex.Message));
                return null;
            }
        }
    }
}
