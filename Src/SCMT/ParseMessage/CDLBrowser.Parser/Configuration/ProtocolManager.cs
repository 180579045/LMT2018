// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolManager.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the ProtocolManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows.Media;
    using System.Xml;

    using Common.Logging;

    using SuperLMT.Utils;

    /// <summary>
    /// The protocol manager.
    /// </summary>
    public class ProtocolManager
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProtocolManager));

        /// <summary>
        /// The manager.
        /// </summary>
        private static ProtocolManager manager;

        /// <summary>
        /// The protocols.
        /// </summary>
        private readonly IDictionary<int, string> protocols = new Dictionary<int, string>();

        /// <summary>
        /// The protocol colors.
        /// </summary>
        private readonly IDictionary<string, string> protocolColors = new Dictionary<string, string>();

        public IDictionary<int, string> ProtocolsDic
        {
            get { return protocols; }
        }
        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static ProtocolManager Singleton
        {
            get
            {
                if (manager == null)
                {
                    manager = new ProtocolManager();
                    manager.InitializeProtocols();
                }

                return manager;
            }
        }

        /// <summary>
        /// The get protocol name.
        /// </summary>
        /// <param name="protocolId">
        /// The protocol id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetProtocolName(int protocolId)
        {
            string name;
            if (this.protocols.TryGetValue(protocolId, out name))
            {
                return name;
            }

            return "Unknown Interface";
        }

        /// <summary>
        /// The get protocol color.
        /// </summary>
        /// <param name="protocolName">
        /// The protocol name.
        /// </param>
        /// <returns>
        /// The <see cref="Color"/>.
        /// </returns>
        public string GetProtocolColor(string protocolName)
        {
            string protocolColor;
            if (this.protocolColors.TryGetValue(protocolName, out protocolColor))
            {
                return protocolColor;
            }

            return "#FFFFFFFF";
        }

        /// <summary>
        /// The initialize protocols.
        /// </summary>
        private void InitializeProtocols()
        {
            try
            {
                var fileStrumentDoc = new XmlDocument();
                fileStrumentDoc.Load(AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\eNBCDLCommonDef.xml");
                var nodes = fileStrumentDoc.SelectNodes(@"/Document/Resources/Protocols/Protocol");
                if (nodes == null)
                {
                    Log.Error(@"initialize protocol type error,nodes empty;please check Configuration\Files\eNBCDLCommonDef.xml");
                    return;
                }

                foreach (XmlNode node in nodes)
                {
                    Debug.Assert(node.Attributes != null, "node.Attributes != null");
                    XmlAttribute protocolNameAttribute = node.Attributes["Name"];
                    string protocolName = protocolNameAttribute.Value;

                    XmlAttribute protocolIdAttribute = node.Attributes["ID"];
                    int protocolId = Convert.ToInt32(protocolIdAttribute.Value, 16);

                    this.protocols.Add(protocolId, protocolName);

                    this.protocolColors.Add(protocolName, node.Attributes["Color"].Value);
                }
            }
            catch (Exception ex)
            {
                Log.Error("initialize protocol type error,message = " + ex.Message);
            }
        }
    }
}
