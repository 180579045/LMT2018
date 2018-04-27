// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalTraceMessageInfoManager.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the SingnalTraceMessageInfoManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Xml;

    using CDLBrowser.Parser.ASN1.Implement;

    using Common.Logging;

    using SuperLMT.Utils;

    /// <summary>
    /// The signal trace message info manager.
    /// </summary>
    public class SignalTraceMessageInfoManager
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(SignalTraceMessageInfoManager));

        /// <summary>
        /// The manager.
        /// </summary>
        private static SignalTraceMessageInfoManager manager;

        /// <summary>
        /// The protocols.
        /// </summary>
        private readonly IDictionary<uint, string> messageNames = new Dictionary<uint, string>();

        /// <summary>
        /// The message colors.
        /// </summary>
        private readonly IDictionary<string, string> messageColors = new Dictionary<string, string>();

        /// <summary>
        /// The pdu nums. string: messageName int:pduNum
        /// </summary>
        private readonly IDictionary<string, int> pduNums = new Dictionary<string, int>();

        /// <summary>
        /// The decode DLL names.
        /// </summary>
        private readonly IDictionary<string, string> decodeDllNames = new Dictionary<string, string>();

        /// <summary>
        /// The is need NAS decodes.
        /// </summary>
        private readonly IDictionary<string, bool> isNeedSecondDecodes = new Dictionary<string, bool>();

        /// <summary>
        /// The port names.
        /// </summary>
        private readonly IDictionary<string, string> portNames = new Dictionary<string, string>();

        /// <summary>
        /// The PDU number. string: messageName INT:PDUNUM
        /// </summary>
        private readonly IDictionary<string, int> nasPduNums = new Dictionary<string, int>();

        /// <summary>
        /// The second decode.
        /// </summary>
        private IDictionary<string, SpecialNode> secondDecode = new Dictionary<string, SpecialNode>();

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static SignalTraceMessageInfoManager Singleton
        {
            get
            {
                if (manager == null)
                {
                    manager = new SignalTraceMessageInfoManager();
                    manager.InitializeMessages();
                    manager.InitializeSpecialDecode();
                }

                return manager;
            }
        }

        /// <summary>
        /// The get message name.
        /// </summary>
        /// <param name="messageId">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetMessageName(uint messageId)
        {
            string name;
            if (this.messageNames.TryGetValue(messageId, out name))
            {
                return name;
            }

            return "Unknown Message";
        }

        /// <summary>
        /// The get message color.
        /// </summary>
        /// <param name="messageName">
        /// The message Name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetMessageColor(string messageName)
        {
            string color;
            if (this.messageColors.TryGetValue(messageName, out color))
            {
                return color;
            }

            return string.Empty;
        }

        /// <summary>
        /// The get pdu num.
        /// </summary>
        /// <param name="messageName">
        /// The message name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetPduNum(string messageName)
        {
            int pduNo;

            if (this.pduNums.TryGetValue(messageName, out pduNo))
            {
                return pduNo;
            }

            return 0;
        }

        /// <summary>
        /// The get decode dll name.
        /// </summary>
        /// <param name="messageName">
        /// The message name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDecodeDllName(string messageName)
        {
            string decodedllname;

            if (this.decodeDllNames.TryGetValue(messageName, out decodedllname))
            {
                return decodedllname;
            }

            return "RRC";
        }

        /// <summary>
        /// The get second decode flag.
        /// </summary>
        /// <param name="messageName">
        /// The message name.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool GetSecondDecodeFlag(string messageName)
        { 
            bool flag;

            if (this.isNeedSecondDecodes.TryGetValue(messageName, out flag))
            {
                return flag;
            }

            return false;
        }

        /// <summary>
        /// The get port name.
        /// </summary>
        /// <param name="messageName">
        /// The message name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetPortName(string messageName)
        {
            string portname;

            if (this.portNames.TryGetValue(messageName, out portname))
            {
                return portname;
            }

            return string.Empty;
        }

        /// <summary>
        /// The get pdu num.
        /// </summary>
        /// <param name="messageName">
        /// The message name.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetNasPduNum(string messageName)
        {
            int pduNo;

            if (this.nasPduNums.TryGetValue(messageName, out pduNo))
            {
                return pduNo;
            }

            return 0;
        }

        /// <summary>
        /// The get second decode type.
        /// </summary>
        /// <param name="port">
        /// The port.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public SpecialNode GetSecondDecodeType(string port)
        {
            SpecialNode decodeType;

            if (this.secondDecode.TryGetValue(port, out decodeType))
            {
                return decodeType;
            }

            return null;
        }

        /// <summary>
        /// The initialize protocols.
        /// </summary>
        private void InitializeMessages()
        {
            try
            {
                var fileStrumentDoc = new XmlDocument();
                fileStrumentDoc.Load(AppPathUtiliy.Singleton.GetAppPath() + @"\Configuration\Files\eNBCDLCommonDef.xml");

                var nodes = fileStrumentDoc.SelectNodes(@"/Document/Resources/TraceMessageTypes/Message");
                if (nodes == null)
                {
                    Log.Error(
                        @"initialize protocol type error,nodes empty;please check Configuration\Files\eNBCDLCommonDef.xml");
                    return;
                }

                foreach (XmlNode node in nodes)
                {
                    Debug.Assert(node.Attributes != null, "node.Attributes != null");
                    if (node.Attributes != null)
                    {
                        XmlAttribute messageNameAttribute = node.Attributes["Name"];
                        string messageName = messageNameAttribute.Value.Trim();

                        XmlAttribute messageIdAttribute = node.Attributes["ID"];
                        uint messageId = Convert.ToUInt32(messageIdAttribute.Value);

                        XmlAttribute pduNumAttribute = node.Attributes["PduNum"];
                        if (pduNumAttribute != null)
                        {
                            int pduNo = Convert.ToInt32(pduNumAttribute.Value);
                            this.pduNums.Add(messageName, pduNo);
                        }

                        this.messageNames.Add(messageId, messageName);
                        XmlAttribute messageColorAttribute = node.Attributes["Color"];
                        if (messageColorAttribute != null)
                        {
                            string color = messageColorAttribute.Value.Trim();
                            this.messageColors.Add(messageName, color);
                        }

                        XmlAttribute messageDecodeDllNameAttribute = node.Attributes["DecodeDll"];
                        if (messageDecodeDllNameAttribute != null)
                        {
                            string name = messageDecodeDllNameAttribute.Value.Trim();
                            this.decodeDllNames.Add(messageName, name);
                        }

                        XmlAttribute messageNasAttribute = node.Attributes["Nas"];
                        if (messageNasAttribute != null)
                        {
                            bool flag = bool.Parse(messageNasAttribute.Value.Trim());
                            this.isNeedSecondDecodes.Add(messageName, flag);
                        }

                        XmlAttribute portNameAttribute = node.Attributes["Port"];
                        if (portNameAttribute != null)
                        {
                            string name = portNameAttribute.Value.Trim();
                            this.portNames.Add(messageName, name);
                        }

                        XmlAttribute messageNasPduAttribute = node.Attributes["NasPdu"];
                        if (messageNasPduAttribute != null)
                        {
                            int pduNo = Convert.ToInt32(messageNasPduAttribute.Value);
                            this.nasPduNums.Add(messageName, pduNo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("initialize protocol type error,message = " + ex.Message);
            }
        }

        /// <summary>
        /// The initialize special decode.
        /// </summary>
        private void InitializeSpecialDecode()
        {
            try
            {
                this.secondDecode = SpecialParser.Singleton.GetDecodeTypeFromFile();
            }
            catch (Exception ex)
            {
                Log.Error("initialize protocol type error,message = " + ex.Message);
            }
        }
    }
}
