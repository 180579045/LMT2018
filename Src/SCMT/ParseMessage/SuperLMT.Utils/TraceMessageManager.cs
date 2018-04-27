// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TraceMessageManager.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the TraceMessageManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Parser.TraceParser
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    using Parser;

    using SuperLMT.Utils;

    /// <summary>
    /// The trace message manager.
    /// </summary>
    public class TraceMessageManager
    {
        ///// <summary>
        ///// The log.
        ///// </summary>
        // private static readonly ILog Log = LogManager.GetLogger(typeof(TraceMessageManager));

        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly TraceMessageManager Instance;

        #region private data member

        /// <summary>
        /// The trace event type 2 xml document dictionary.
        /// </summary>
        private readonly Dictionary<string, XmlDocument> traceEventType2XmlDocumentDictionary = new Dictionary<string, XmlDocument>();

        /// <summary>
        /// The data type to length dictionary.
        /// </summary>
        private Dictionary<string, int> dataTypeToLengthDictionary = new Dictionary<string, int>();

        #endregion

        /// <summary>
        /// Initializes static members of the <see cref="TraceMessageManager"/> class.
        /// </summary>
        static TraceMessageManager()
        {
            Instance = new TraceMessageManager();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="TraceMessageManager"/> class from being created.
        /// </summary>
        private TraceMessageManager()
        {
        }

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static TraceMessageManager Singleton
        {
            get { return Instance; }
        }

        #region 对外呈现函数

        /// <summary>
        /// The get trace common struct tree.
        /// </summary>
        /// <returns>
        /// The <see cref="XmlNode"/>.
        /// </returns>
        public XmlNode GetTraceCommonStructTree()
        {
            XmlNode totalNode = CDLFileStructMgr.Instance.GetCDLFileCommonStructTree();
            this.dataTypeToLengthDictionary = CDLFileStructMgr.Instance.GetDataTypes();
            return totalNode;
        }

        /// <summary>
        /// The get trace head node list.
        /// </summary>
        /// <returns>
        /// The <see cref="XmlNodeList"/>.
        /// </returns>
        public XmlNodeList GetTraceHeadNodeList()
        {
            XmlNode root = this.GetTraceCommonStructTree();
            if (null == root)
            {
                return null;
            }

            return root.SelectNodes(@"/Document/TraceCommonHead/HeadNode");
        }

        /// <summary>
        /// The get trace struct tree by version.
        /// </summary>
        /// <param name="version">
        /// The  version.
        /// </param>
        /// <returns>
        /// The <see cref="XmlNode"/>.
        /// </returns>
        public XmlNode GetTraceStructTreeByVersion(string version)
        {
            XmlDocument findDoc = this.GetTraceStructXmlDocByVersion(version);
            if (null == findDoc)
            {
                string strFilePath = this.GetTraceEventStructFilePath(version);
                findDoc = new XmlDocument();
                try
                {
                    findDoc.Load(strFilePath);
                }
                catch (SystemException)
                {
                    return null;
                }

                this.AddTraceEventStructXmlDoc(version, findDoc);
            }

            return findDoc.DocumentElement;
        }

        /// <summary>
        /// The get data type length.
        /// </summary>
        /// <param name="typeName">
        /// The type name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetDataTypeLength(string typeName)
        {
            int typeNameLength;
            this.dataTypeToLengthDictionary.TryGetValue(typeName, out typeNameLength);

            return typeNameLength;
        }

        /// <summary>
        /// The get trace event struct root.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <returns>
        /// The <see cref="XmlNode"/>.
        /// </returns>
        public XmlNode GetTraceEventStructRoot(string version, string eventType)
        {
            XmlNode xmlDocRoot = this.GetTraceStructTreeByVersion(version);
            if (null == xmlDocRoot)
            {
                return null;
            }

            eventType = ConvertUtil.Instance.CovertDecimStringtoHex8ByteString(eventType);
            string strSelectPath = string.Format(@"//Event[@ID='{0}']", eventType);
            XmlNode xmlEventRoot;
            try
            {
                xmlEventRoot = xmlDocRoot.SelectSingleNode(strSelectPath);
            }
            catch (Exception)
            {
                return null;
            }

            return xmlEventRoot;
        }

        /// <summary>
        /// The get trace event header.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <returns>
        /// The <see cref="XmlNode"/>.
        /// </returns>
        public XmlNode GetTraceEventHeader(string version, string eventType)
        {
            XmlNode xmlDocRoot = this.GetTraceStructXmlDocByVersion(version);
            if (null == xmlDocRoot)
            {
                return null;
            }

            eventType = ConvertUtil.Instance.CovertDecimStringtoHex8ByteString(eventType);
            string strSelectPath = string.Format(@"//Event[@ID='{0}']/TraceEventHeader", eventType);
            XmlNode xmlEventHeaderNode;
            try
            {
                xmlEventHeaderNode = xmlDocRoot.SelectSingleNode(strSelectPath);
            }
            catch (Exception)
            {
                return null;
            }

            return xmlEventHeaderNode;
        }

        /// <summary>
        /// The get trace event body.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <param name="eventType">
        /// The event type.
        /// </param>
        /// <returns>
        /// The <see cref="XmlNode"/>.
        /// </returns>
        public XmlNode GetTraceEventBody(string version, string eventType)
        {
            XmlNode xmlDocRoot = this.GetTraceStructXmlDocByVersion(version);
            if (null == xmlDocRoot)
            {
                return null;
            }

            eventType = ConvertUtil.Instance.CovertDecimStringtoHex8ByteString(eventType);
            string strSelectPath = string.Format(@"//Event[@ID='{0}']/TraceEventBody", eventType);
            XmlNode xmlEventBodyNode;
            try
            {
                xmlEventBodyNode = xmlDocRoot.SelectSingleNode(strSelectPath);
            }
            catch (Exception)
            {
                return null;
            }

            return xmlEventBodyNode;
        }
        

        /// <summary>
        /// The get trace struct xml doc by version.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="XmlDocument"/>.
        /// </returns>
        protected XmlDocument GetTraceStructXmlDocByVersion(string version)
        {
            XmlDocument document;
            this.traceEventType2XmlDocumentDictionary.TryGetValue(version, out document);

            return document;
        }

        /// <summary>
        /// The get trace event struct file path.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string GetTraceEventStructFilePath(string version)
        {
            string filePath = string.Format(@"\config\TraceStruct_{0}.xml", version);

            filePath = AppPathUtiliy.Instance.GetAppPath() + filePath;

            return filePath;
        }

        /// <summary>
        /// The add trace event struct xml doc.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <param name="document">
        /// The document.
        /// </param>
        protected void AddTraceEventStructXmlDoc(string version, XmlDocument document)
        {
            if (string.IsNullOrEmpty(version) || null == document)
            {
                return;
            }

            this.traceEventType2XmlDocumentDictionary.Add(version, document);
        }
        #endregion
    }
}
