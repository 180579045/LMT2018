// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecialParser.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   The special parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace CDLBrowser.Parser.ASN1.Implement
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    using Common.Logging;

    using SuperLMT.Utils;

    /// <summary>
    /// The times parser.
    /// </summary>
    public class SpecialParser
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SpecialParser));

        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly SpecialParser Instance;

        /// <summary>
        /// Initializes static members of the <see cref="SpecialParser"/> class.
        /// </summary>
        static SpecialParser()
        {
            Instance = new SpecialParser();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="SpecialParser"/> class from being created.
        /// </summary>
        private SpecialParser()
        {
            this.DecodeInfos = new Dictionary<string, IList<SpecialNode>>();
            this.SecondDecodeDll = new Dictionary<string, SpecialNode>();
        }

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static SpecialParser Singleton
        {
            get
            {
                return Instance;
            }
        }

        /// <summary>
        /// Gets or sets the decode information.
        /// </summary>
        public Dictionary<string, IList<SpecialNode>> DecodeInfos { get; set; }

        /// <summary>
        /// Gets or sets the second decode DLL.
        /// </summary>
        public Dictionary<string, SpecialNode> SecondDecodeDll { get; set; }

        /// <summary>
        /// The get special node.
        /// </summary>
        /// <returns>
        /// The <see>
        ///       <cref>Dictionary</cref>
        ///     </see> .
        /// </returns>
        public Dictionary<string, IList<SpecialNode>> GetSpecialAsnNode()
        {
            if (0 == this.DecodeInfos.Count)
            {
                string xmlPath = this.GetPath();
                var curXml = new XmlDocument();
                try
                {
                    curXml.Load(xmlPath);
                }
                catch (SystemException)
                {
                    return null;
                }

                XmlNodeList typeNodeList;
                try
                {
                    typeNodeList = curXml.SelectNodes(@"/Document/Protocols/Protocol");
                }
                catch (SystemException)
                {
                    return null;
                }

                this.GetNodeInfoFromFile(typeNodeList);
            }

            return this.DecodeInfos;
        }
    
        /// <summary>
        /// The get decode type from file.
        /// </summary>
        /// <returns>
        /// The <see>
        ///       <cref>Dictionary</cref>
        ///     </see> .
        /// </returns>
        public Dictionary<string, SpecialNode> GetDecodeTypeFromFile()
        {
            if (0 == this.DecodeInfos.Count)
            {
                string xmlPath = this.GetPath();
                var curXml = new XmlDocument();
                try
                {
                    curXml.Load(xmlPath);
                    XmlNodeList typeNodeList = curXml.SelectNodes(@"/Document/Protocols/Protocol");

                    if (typeNodeList == null)
                    {
                        return null;
                    }

                    foreach (XmlNode curNode in typeNodeList)
                    {
                        XmlAttributeCollection attributes = curNode.Attributes;
                        if (attributes != null)
                        {
                            XmlAttribute typePort = attributes["Port"];
                            string port = typePort.Value;

                            XmlAttribute decodeDll = attributes["DecodeDll"];
                            string strDll = decodeDll.Value;

                            XmlAttribute id = attributes["ID"];
                            string strId = id.Value;
                            int nId = int.Parse(strId);

                            var curSpeNode = new SpecialNode { DecodePort = port, DecodePdu = nId, DecodeDll = strDll };

                            if (!this.SecondDecodeDll.ContainsKey(port))
                            {
                                this.SecondDecodeDll.Add(port, curSpeNode);
                            }
                        }
                    }

                    return this.SecondDecodeDll;
                }
                catch (Exception)
                {
                    Log.Error("Cannot find secondNodeDecode");
                }
            }

            return null;
        }

        /// <summary>
        /// The get node info from file.
        /// </summary>
        /// <param name="typeNodeList">
        /// The type node list.
        /// </param>
        protected void GetNodeInfoFromFile(XmlNodeList typeNodeList)
        {
            if (typeNodeList == null)
            {
                return;
            }

            foreach (XmlNode curNode in typeNodeList)
            {
                XmlAttributeCollection attributes = curNode.Attributes;
                if (attributes != null)
                {
                    XmlAttribute typeNameAttr = attributes["Name"];
                    string strTypeName = typeNameAttr.Value;
                    int MsgId = Convert.ToInt32(strTypeName, 16);
                    strTypeName = Convert.ToString(MsgId);

                    XmlAttribute typePort = attributes["Port"];
                    string port = typePort.Value;

                    XmlAttribute id = attributes["ID"];
                    string strId = id.Value;
                    int nId = int.Parse(strId);

                    XmlAttribute decodeDll = attributes["DecodeDll"];
                    string strDll = decodeDll.Value;

                    var curSpeNode = new SpecialNode { DecodePort = port, DecodePdu = nId, DecodeName = strTypeName, DecodeDll = strDll };

                    if (!this.DecodeInfos.ContainsKey(strTypeName))
                    {
                        IList<SpecialNode> list = new List<SpecialNode>();
                        list.Add(curSpeNode);
                        this.DecodeInfos.Add(strTypeName, list);
                    }
                    else
                    {
                        var list = this.DecodeInfos[strTypeName];
                        list.Add(curSpeNode);
                    }
                }
            }
        }

        /// <summary>
        /// The get path.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        protected string GetPath()
        {
            string xmlFilePath = AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\SpecialDecode.xml";
            return xmlFilePath;
        }
    }
}
