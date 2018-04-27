// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RRCDecoder.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the RRCDecoder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.ASN1.Implement.DefaultVersion
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The rrc decoder.
    /// </summary>
    public class RRCDecoder : AbsAsn1Decoder
    {
        /// <summary>
        /// The rrc decode.
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
        /// The asn.1 string.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("Asn1Decode.dll", EntryPoint = "RRCDecode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int RRCDecode(byte[] bitStream, int bitStreamLength, int pduNo, byte[] asn1String);

        /// <summary>
        /// The decode impl.
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
        /// The asn 1 string.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected override int DecodeImpl(byte[] bitStream, int bitStreamLength, int pduNo, byte[] asn1String)
        {
            return RRCDecode(bitStream, bitStreamLength, pduNo, asn1String);
        }
    }
}
