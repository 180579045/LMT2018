// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTreeAsnNode.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the EventTreeAsnNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.EventTree
{
    using System;
    using System.Text;

    using CDLBrowser.Parser.ASN1.Implement;
    using CDLBrowser.Parser.ASN1.Interface;
    using CDLBrowser.Parser.Document.Event;
    using System.Collections.Generic;
    using Common.Logging;

    using SuperLMT.Utils;

    /// <summary>
    /// The event tree asn node.
    /// </summary>
    public class EventTreeAsnNode : ConfigNodeWrapper
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(EventTreeAsnNode));

        /// <summary>
        /// The asn 1 decoder factory.
        /// </summary>
        private static readonly IAsn1DecoderFactory Asn1DecoderFactory = AppSpringHelper.Singleton.GetObject("Asn1DecoderFactory") as IAsn1DecoderFactory;

        /// <summary>
        /// The display content.
        /// </summary>
        private string displayContent = string.Empty;

        /// <summary>
        /// Gets the display content.
        /// </summary>
        public override string DisplayContent
        {
            get
            {
                if (!string.IsNullOrEmpty(this.displayContent))
                {
                    return this.displayContent;
                }

                var sb = new StringBuilder();
                
                string decodeDllName = this.ConfigurationNode.GetAttribute("DecodeDll");

                if (string.IsNullOrEmpty(decodeDllName))
                {
                    decodeDllName = "RRC";
                }

                string asnString;
                IConfigNodeWrapper root = this;
                while (root.Parent != null)
                {
                    root = root.Parent;
                }

                IConfigNodeWrapper pduNoConfigNode = root.GetChildNodeById("PduNum");

                if (pduNoConfigNode == null)
                {
                    return null;
                }

                // 第一次解码
                var isDecode = Asn1DecoderFactory.GetDecoder(decodeDllName).Decode(this.Value as byte[], this.DataLength, Convert.ToInt32(pduNoConfigNode.Value), out asnString);

                if (!isDecode)
                {
                    Log.Error(string.Format("ASN解码失败,MessageId={0}", this.Parent.Parent.Id));
                    this.displayContent = asnString;
                    return this.displayContent;
                }

                string parserTimes = this.ConfigurationNode.GetAttribute("Times");
                if (null != parserTimes)
                {
                    int times = int.Parse(parserTimes);
                    asnString = this.TimesParserCode(asnString, times);
                }

                sb.Append(asnString);
                this.displayContent = sb.ToString();
                return this.displayContent;
            }
        }

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public override IConfigNodeWrapper Clone()
        {
            return new EventTreeAsnNode { ConfigurationNode = this.ConfigurationNode, Parent = this.Parent };
        }

        /// <summary>
        /// The times parser code.
        /// </summary>
        /// <param name="asnCode">
        /// The asn code.
        /// </param>
        /// <param name="times">
        /// The times.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string TimesParserCode(string asnCode, int times)
        {
            var totalNodes = SpecialParser.Singleton.GetSpecialAsnNode();
            times--;
            if (times < 0)
            {
                return asnCode;
            }
           
            var specialNode = this.Parent.Parent.Parent.GetChildNodeById("EventType");
            var currentName = specialNode.Value.ToString();
            if (totalNodes == null)
            {
                return null;
            }
            IList<SpecialNode> matchSpecialNode;

            if (totalNodes.TryGetValue(currentName, out matchSpecialNode))
            {

                foreach (var speValue in matchSpecialNode)
                {
                    if (speValue.DecodePort.IndexOf("dedicatedInfoNASList", StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        asnCode = this.DecodeNasList(asnCode, speValue);
                        continue;
                    }

                    var byteStream = this.GetByteStream(asnCode, speValue.DecodePort);
                    if (string.IsNullOrEmpty(byteStream))
                    {
                       // var errorInfo = string.Format("further decode failed because message:{0} does not has keyword:{1}", speValue.DecodeName, speValue.DecodePort);
                        //Log.Error(errorInfo);
                        continue;
                    }

                    var decoder = this.GetDecoder(speValue.DecodeDll);
                    var asnStream = this.DecodeByteStream(byteStream, decoder, speValue.DecodePdu);

                    string newString = "(需二次解析码流)" + byteStream;
                    asnCode = asnCode.Replace(byteStream, newString);
                    asnCode = asnCode + '\n' + asnStream;
                }
            
            }
            /*
            foreach (var totalNode in totalNodes)
            {
                int currentEventId = Convert.ToInt32(totalNode.Key, 16);
                if (currentName == currentEventId)
                {
                    foreach (var speValue in totalNode.Value)
                    {
                        if (speValue.DecodePort.IndexOf("dedicatedInfoNASList", StringComparison.OrdinalIgnoreCase) > 0)
                        {
                            asnCode = this.DecodeNasList(asnCode, speValue);
                            continue;
                        }

                        var byteStream = this.GetByteStream(asnCode, speValue.DecodePort);
                        if (byteStream == null)
                        {
                            var errorInfo = string.Format("further decode failed because message:{0} does not has keyword:{1}", speValue.DecodeName, speValue.DecodePort);
                            Log.Error(errorInfo);
                            continue;
                        }

                        var decoder = this.GetDecoder(speValue.DecodeDll);
                        var asnStream = this.DecodeByteStream(byteStream, decoder, speValue.DecodePdu);

                        string newString = "(需二次解析码流)" + byteStream;
                        asnCode = asnCode.Replace(byteStream, newString);
                        asnCode = asnCode + '\n' + asnStream;
                    }

                    break;
                }
            }
            */
            return asnCode;
        }

        /// <summary>
        /// The get byte stream.
        /// </summary>
        /// <param name="originalString">
        /// The original string.
        /// </param>
        /// <param name="keyWord">
        /// The key word.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetByteStream(string originalString, string keyWord)
        {
            if (originalString.IndexOf(keyWord, StringComparison.OrdinalIgnoreCase) < 0)
            {
                return null;
            }

            int keyPosition = originalString.IndexOf(keyWord, StringComparison.OrdinalIgnoreCase);
            string byteStream = originalString;
            if (keyPosition > 0)
            {
                int startPosition = originalString.IndexOf("'", keyPosition, StringComparison.Ordinal);
                int endPosition = originalString.IndexOf("'", startPosition + 1, StringComparison.Ordinal);
                byteStream = originalString.Substring(startPosition + 1, endPosition - startPosition - 1);
            }
           
            return byteStream;
        }

        /// <summary>
        /// The get decoder.
        /// </summary>
        /// <param name="keyWord">
        /// The key word.
        /// </param>
        /// <returns>
        /// The <see cref="IAsn1Decoder"/>.
        /// </returns>
        private IAsn1Decoder GetDecoder(string keyWord)
        {
            var decoderFactory = AppSpringHelper.Singleton.GetObject(@"Asn1DecoderFactory") as IAsn1DecoderFactory;
            if (decoderFactory == null)
            {
                Log.Error("decoderFactory is empty");
                return null;
            }

            return decoderFactory.GetDecoder(keyWord);
        }

        /// <summary>
        /// The decode byte stream.
        /// </summary>
        /// <param name="byteStream">
        /// The byte stream.
        /// </param>
        /// <param name="decoder">
        /// The decoder.
        /// </param>
        /// <param name="pduNum">
        /// The pdu num.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string DecodeByteStream(string byteStream, IAsn1Decoder decoder, int pduNum)
        {
            if (decoder == null)
            {
                Log.Error("decoder is empty");
                return null;
            }

            byte[] curBytes = this.StringToBytes(byteStream); // convert to bytes
            if (curBytes == null)
            {
                return null;
            }

            string asnStream;
            var isDecode = decoder.Decode(curBytes, curBytes.Length, pduNum, out asnStream);
            if (!isDecode)
            {
               //yanjiewa Log.Error("asnStream is empty");
                return asnStream;
            }

            int finishLength = asnStream.IndexOf('\0');
            if (finishLength != -1)
            {
                asnStream = asnStream.Substring(0, finishLength);
            }

            return asnStream;
        }

        /// <summary>
        /// The string to bytes.
        /// </summary>
        /// <param name="currentString">
        /// The cur string.
        /// </param>
        /// <returns>
        /// The <see cref="byte"/>.
        /// </returns>
        private byte[] StringToBytes(string currentString)
        {
            int byteLength = currentString.Length / 2;
            var curBytes = new byte[byteLength];
            var num = 0;
            try
            {
                while (currentString.Length > 1)
                {
                    string subString = currentString.Substring(0, 2);

                    int hexNum = Convert.ToInt32(subString, 16);
                    byte curByte = Convert.ToByte(hexNum);
                    curBytes[num] = curByte;
                    num++;
                    currentString = currentString.Substring(2, currentString.Length - 2);
                }
            }
            catch (Exception ex)
            {
                Log.Info(ex.Message);
                return null;
            }

            return curBytes;
        }

        /// <summary>
        /// The decode nas list.
        /// </summary>
        /// <param name="asnCode">
        /// The asn code.
        /// </param>
        /// <param name="speValue">
        /// The spe value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string DecodeNasList(string asnCode, SpecialNode speValue)
        {
            var nasList = this.GetNasList(asnCode, speValue.DecodePort);
            var nasDecoder = this.GetDecoder(speValue.DecodeDll);
            foreach (var nasStream in nasList)
            {
                var stream = this.DecodeByteStream(nasStream, nasDecoder, speValue.DecodePdu);
                var nasString = "(需二次解析码流)" + nasStream;
                asnCode = asnCode.Replace(nasStream, nasString);
                asnCode = asnCode + '\n' + stream;
            }

            return asnCode;
        }

        /// <summary>
        /// The get nas list.
        /// </summary>
        /// <param name="originalString">
        /// The original string.
        /// </param>
        /// <param name="keyWord">
        /// The key word.
        /// </param>
        /// <returns>
        /// The <see>
        ///       <cref>string[]</cref>
        ///     </see> .
        /// </returns>
        private string[] GetNasList(string originalString, string keyWord)
        {
            int keyPosition = originalString.IndexOf(keyWord, StringComparison.Ordinal);
            int startPosition = originalString.IndexOf("{", keyPosition, StringComparison.Ordinal);
            int endPosition = originalString.IndexOf("}", startPosition + 1, StringComparison.Ordinal);
            string byteStream = originalString.Substring(startPosition + 2, endPosition - startPosition - 3);

            string[] byteStreams = byteStream.Remove('H').Split('\'');

            return byteStreams;
        }
    }
}
