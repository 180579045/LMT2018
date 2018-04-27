// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Asn1DecoderFactory.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the Asn1DecoderFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.ASN1.Implement
{
    using System.Collections.Generic;

    using CDLBrowser.Parser.ASN1.Interface;

    /// <summary>
    /// The asn.1 decoder factory.
    /// </summary>
    public class Asn1DecoderFactory : IAsn1DecoderFactory
    {
        /// <summary>
        /// The decoders.
        /// </summary>
        private readonly IDictionary<string, IAsn1Decoder> decoders;

        /// <summary>
        /// Initializes a new instance of the <see cref="Asn1DecoderFactory"/> class.
        /// </summary>
        /// <param name="decoders">
        /// The decoders.
        /// </param>
        private Asn1DecoderFactory(IDictionary<string, IAsn1Decoder> decoders)
        {
            this.decoders = decoders;
        }

        /// <summary>
        /// The get decoder.
        /// </summary>
        /// <param name="decoderType">
        /// The decoder type.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// The decoder<see cref="IAsn1Decoder"/>.
        /// </returns>
        public IAsn1Decoder GetDecoder(string decoderType, string version)
        {
            decoderType = decoderType.ToUpper();
            IAsn1Decoder decoder;
            if (this.decoders.TryGetValue(decoderType + version, out decoder))
            {
                return decoder;
            }

            return null;
        }

        /// <summary>
        /// The get decoder.
        /// </summary>
        /// <param name="decoderType">
        /// The decoder type.
        /// </param>
        /// <returns>
        /// The decoder<see cref="IAsn1Decoder"/>.
        /// </returns>
        public IAsn1Decoder GetDecoder(string decoderType)
        {
            decoderType = decoderType.ToUpper();
            const string Version = "DefaultVersion";
            return this.GetDecoder(decoderType, Version);
        }
    }
}
