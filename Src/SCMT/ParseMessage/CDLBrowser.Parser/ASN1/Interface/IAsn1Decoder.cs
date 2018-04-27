// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAsn1Decoder.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the IAsn1Decoder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.ASN1.Interface
{
    /// <summary>
    /// The Asn.1 Decoder interface.
    /// </summary>
    public interface IAsn1Decoder
    {
        /// <summary>
        /// The decode.
        /// </summary>
        /// <param name="bitStream">
        /// The bit stream.
        /// </param>
        /// <param name="bitStreamLength">
        /// The bit stream length.
        /// </param>
        /// <param name="pduNo">
        /// The pdu no.
        /// </param>
        /// <param name="asn1String">
        /// The output asn.1 string.
        /// </param>
        /// <returns>
        /// The result.0 success,other failed<see cref="int"/>.
        /// </returns>
        bool Decode(byte[] bitStream, int bitStreamLength, int pduNo, out string asn1String);
    }
}
