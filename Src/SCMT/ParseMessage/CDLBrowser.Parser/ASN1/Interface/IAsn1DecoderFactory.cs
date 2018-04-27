// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAsn1DecoderFactory.cs" company="datangmoible">
//   datangmoible
// </copyright>
// <summary>
//   Defines the IAsn1DecoderFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.ASN1.Interface
{
    /// <summary>
    /// The Asn.1 Decoder Factory interface.
    /// </summary>
    public interface IAsn1DecoderFactory
    {
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
        IAsn1Decoder GetDecoder(string decoderType, string version);

        /// <summary>
        /// The get decoder.
        /// </summary>
        /// <param name="decoderType">
        /// The decoder type.
        /// </param>
        /// <returns>
        /// The decoder<see cref="IAsn1Decoder"/>.
        /// </returns>
        IAsn1Decoder GetDecoder(string decoderType);
    }
}
