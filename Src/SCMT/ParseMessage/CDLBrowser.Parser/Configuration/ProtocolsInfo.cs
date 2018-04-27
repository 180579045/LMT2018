// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProtocolsInfo.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the ProtocolManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Windows.Media;
    using System.Xml;
    using System.Xml.Serialization;

    using Common.Logging;

    using SuperLMT.Utils;

    /// <summary>
    /// The protocol manager.
    /// </summary>
    public class ProtocolsInfo
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ProtocolsInfo));

        /// <summary>
        /// The protocols.
        /// </summary>
        private readonly IDictionary<uint, string> protocolNames = new Dictionary<uint, string>();

        /// <summary>
        /// The protocol colors.
        /// </summary>
        private readonly IDictionary<string, Protocol> protocolColors = new Dictionary<string, Protocol>();

        /// <summary>
        /// The nodepath.
        /// </summary>
        private readonly string nodePath = @"/Document/Resources/Protocols/Protocol";

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtocolsInfo"/> class.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="isOnline">
        /// The is Online.
        /// </param>
        public ProtocolsInfo(string filePath, bool isOnline = false)
        {
            this.Protocols = new ObservableCollection<Protocol>();
            if (isOnline)
            {
                this.nodePath = filePath;
            }
            
            // check if some messages which in eNBCDLCommonDef not in serialize file
            this.MergeProtocolsFromConfigFile();
        }

        /// <summary>
        /// Gets the protocols.
        /// </summary>
        public ObservableCollection<Protocol> Protocols { get; private set; }

        /// <summary>
        /// The get protocol name.
        /// </summary>
        /// <param name="protocolId">
        /// The protocol id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetProtocolName(uint protocolId)
        {
            string name;
            if (this.protocolNames.TryGetValue(protocolId, out name))
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
            Protocol proto;
            if (this.protocolColors.TryGetValue(protocolName, out proto))
            {
                return proto.Color;
            }

            return "#FFFFFFFF";
        }

        /// <summary>
        /// The serialize to file.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public void SerializeToFile(string fileName)
        {
            try
            {
                var ser = new XmlSerializer(typeof(ObservableCollection<Protocol>));
                FileStream fs = File.Create(fileName);
                ser.Serialize(fs, this.Protocols);
                fs.Close();
            }
            catch (Exception ex)
            {
                Log.Error("SerilizeToFile error,message = " + ex.Message);
            }
        }

        /// <summary>
        /// The reset color.
        /// </summary>
        public void ResetColor()
        {
            this.Protocols.Clear();
            this.protocolColors.Clear();
            this.protocolNames.Clear();
            this.MergeProtocolsFromConfigFile();
        }

        /// <summary>
        /// The initialize protocols.
        /// </summary>
        /// <param name="nodepath">
        /// The node path.
        /// </param>
        /// <returns>
        /// The <see>
        ///       <cref>List</cref>
        ///     </see> .
        /// </returns>
        private IEnumerable<Protocol> InitializeProtocols(string nodepath)
        {
            try
            {
                var fileStrumentDoc = new XmlDocument();
                fileStrumentDoc.Load(AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\eNBCDLCommonDef.xml");
                var nodes = fileStrumentDoc.SelectNodes(nodepath);
                if (nodes == null)
                {
                    Log.Error(@"initialize protocol type error,nodes empty;please check Configuration\Files\eNBCDLCommonDef.xml");
                    return null;
                }

                var protos = new List<Protocol>();
                foreach (XmlNode node in nodes)
                {
                    Debug.Assert(node.Attributes != null, "node.Attributes != null");
                    if (node.Attributes != null)
                    {
                        XmlAttribute protocolNameAttribute = node.Attributes["Name"];
                        string protocolName = protocolNameAttribute.Value;

                        XmlAttribute protocolIdAttribute = node.Attributes["ID"];
                        uint protocolId = Convert.ToUInt32(protocolIdAttribute.Value, 16);

                        var proto = new Protocol
                            {
                                Name = protocolName,
                                Id = protocolId.ToString(CultureInfo.InvariantCulture),
                                Color = node.Attributes["Color"].Value
                            };

                        protos.Add(proto);
                    }
                }

                return protos;
            }
            catch (Exception ex)
            {
                Log.Error("initialize protocol type error,message = " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The get protocols from config file.
        /// </summary>
        private void MergeProtocolsFromConfigFile()
        {
            try
            {
                var protos = this.InitializeProtocols(this.nodePath);
                foreach (var protocol in protos)
                {
                    if (!this.protocolNames.ContainsKey(Convert.ToUInt32(protocol.Id)))
                    {
                        this.protocolNames.Add(Convert.ToUInt32(protocol.Id), protocol.Name);
                        if (!this.protocolColors.ContainsKey(protocol.Name)) {
                            this.protocolColors.Add(protocol.Name, protocol);
                        }                      
                        this.Protocols.Add(protocol);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("MergeProtocolsFromConfigFile failed,message = " + ex.Message);
            }
        }
    }
}
