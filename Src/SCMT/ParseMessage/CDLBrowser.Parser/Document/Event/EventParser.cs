// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventParser.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the EventParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.Event
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    using CDLBrowser.Parser.Configuration;
    using CDLBrowser.Parser.Document.EventTree;
    using System.Collections.Generic;
    using Common.Logging;
    public class EventParserManager
    {

        private IDictionary<string, EventParser> eventParserMap = new Dictionary<string, EventParser>();
        private static EventParserManager mananger = new EventParserManager();
        public static EventParserManager Instance
        {
            get { return mananger; }
        }

        public void AddEventParser(string version, EventParser evtparser)
        {
            if (eventParserMap.Keys.Contains(version) == false)
            {
                eventParserMap.Add(version,evtparser);
            }
            
        }

        public void RemoveEventParser(string version)
        {
            if (eventParserMap.Keys.Contains(version))
            {
                eventParserMap.Remove(version);
            }
        }

        public EventParser GetParser(string version)
        {
            if (eventParserMap.Keys.Contains(version))
            {
                return eventParserMap[version];
            }
            return null;
        }

    }


    /// <summary>
    /// The event parser.
    /// </summary>
    public class EventParser
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(EventParser));

        /// <summary>
        /// The parser.
        /// </summary>
        private static readonly EventParser Parser = new EventParser();

        /// <summary>
        /// The current event tree head node.
        /// </summary>
        private ConfigNodeWrapper currentEventTreeHeadNode;

        /// <summary>
        /// The version.
        /// </summary>
        private string version;

        /// <summary>
        /// Prevents a default instance of the <see cref="EventParser"/> class from being created.
        /// </summary>
        public EventParser()
        {
        }

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        //public static EventParser Singleton
        //{
        //    get
        //    {
        //        return Parser;
        //    }
        //}

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public string Version
        {
            get
            {
                return this.version;
            }

            set
            {
                if (this.version != value)
                {
                    this.version = value;
                    this.UpdateEventTreeHeadNode();
                }
            }
        }

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="rawData">
        /// The raw Data. discarded~
        /// </param>
        public void Parse(byte[] rawData)
        {
            try
            {
                var stream = new MemoryStream(rawData);
                this.currentEventTreeHeadNode.InitializeValue(stream, Version);
            }
            catch (Exception ex)
            {
                Log.Error("Parse event head failed!(byte)message = " + ex.Message);
            }
        }

        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="stream">
        /// The stream.
        /// </param>
        public void Parse(MemoryStream stream)
        {
            try
            {
                this.currentEventTreeHeadNode.InitializeValue(stream , Version);
            }
            catch (Exception ex)
            {
                Log.Error("Parse event head failed!(MemoryStream)message = " + ex.Message);
            }
        }

        /// <summary>
        /// The get event tree head node.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public IConfigNodeWrapper GetEventTreeHeadNode(string id)
        {
            return this.currentEventTreeHeadNode.GetChildNodeById(id);
        }

        /// <summary>
        /// The get message direction.
        /// </summary>
        /// <param name="evt">
        /// The event id.
        /// </param>
        /// <param name="messageSource">
        /// The message source.
        /// </param>
        /// <param name="messageDestination">
        /// The message destination.
        /// </param>
        public void GetMessageDirection(IEvent evt, out string messageSource, out string messageDestination , out string msgname)
        {
            lock (this)
            {
                messageSource = string.Empty;
                messageDestination = string.Empty;
                msgname = string.Empty;
                IConfigNode eventConfigBodyNode =
                    ConfigurationManager.Singleton.GetEventConfiguration(evt.Version).GetEventBodyNodeById(
                        evt.EventIdentifier);
                if (eventConfigBodyNode == null)
                {
                    return;
                }

                string messageDirectionAttribute = eventConfigBodyNode.GetAttribute("MsgDirection");
                string messageDirection = messageDirectionAttribute;
                msgname = eventConfigBodyNode.GetAttribute("Describe");
                if (messageDirectionAttribute.IndexOf("{", StringComparison.Ordinal) >= 0)
                {
                    var bindingRegex = new Regex(@"{Binding (\w+)\s*}", RegexOptions.IgnoreCase);
                    Match m = bindingRegex.Match(messageDirectionAttribute);
                    IConfigNodeWrapper rootEventTreeNode = SecondaryParser.Singleton.Parse(evt);
                    if (rootEventTreeNode == null)
                    {
                        Log.Error("GetMessageDirection,rootEventTreeNode is null.");
                        return;
                    }
                    if (m.Groups.Count < 1)
                    {
                        Log.Error("No match!!!!!");
                        return;
                    }
                    messageDirection = rootEventTreeNode.GetChildNodeById(m.Groups[1].Value).ValueDescription;
                    string[] directionInfos = messageDirection.Split('{', '}');
                    if (directionInfos.Length > 1)
                    {
                        messageDirection = directionInfos[1].Trim('{', '}');
                    }

                rootEventTreeNode.Dispose();
            }

                if (messageDirection.Contains("->"))
                {
                    /*类似"EPC->eNB"的处理*/
                    var stringSeparators = new[] {"->"};
                    var splitResult = messageDirection.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    /*EPC->eNB拆分后为两个字串*/
                    if (splitResult.Length >= 2)
                    {
                        messageSource = splitResult[0].Trim();
                        messageDestination = splitResult[1].Trim();
                    }
                    else
                    {
                        Log.Error(string.Format(@"Event消息方向填写错误，{0}", messageDirectionAttribute));
                    }
                }
            }
        }

        public int GetEventHeaderItemCount()
        {
            if (currentEventTreeHeadNode != null)
            {
                return currentEventTreeHeadNode.Children.Count;
            }
            return 0;
        }

        /// <summary>
        /// The update event tree head node.
        /// </summary>
        private void UpdateEventTreeHeadNode()
        {
            if (null != this.currentEventTreeHeadNode)
            {
                this.currentEventTreeHeadNode.Dispose();
            }

            IEventConfiguration eventConfiguration = ConfigurationManager.Singleton.GetEventConfiguration(this.version);

            IConfigNode eventConfigHeadNode = eventConfiguration.EventHeadNode;

            if (eventConfigHeadNode == null)
            {
                return;
            }

            this.currentEventTreeHeadNode = new EventTreeRootNode { ConfigurationNode = eventConfigHeadNode };

            foreach (var child in eventConfigHeadNode.Children)
            {
                this.currentEventTreeHeadNode.Children.Add(
                    new ConfigNodeWrapper { ConfigurationNode = child, Parent = this.currentEventTreeHeadNode });
            }
        }
    }
}
