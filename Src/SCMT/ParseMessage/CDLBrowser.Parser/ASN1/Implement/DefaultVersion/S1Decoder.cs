// --------------------------------------------------------------------------------------------------------------------
// <copyright file="S1Decoder.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the S1Decoder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.ASN1.Implement.DefaultVersion
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The s1 decoder.
    /// </summary>
    public class S1Decoder : AbsAsn1Decoder
    {
        /// <summary>
        /// The s1 decode.
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
        [DllImport("Asn1Decode.dll", EntryPoint = "S1Decode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int S1Decode(byte[] bitStream, int bitStreamLength, int pduNo, byte[] asn1String);

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
        /// The asn.1 string.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected override int DecodeImpl(byte[] bitStream, int bitStreamLength, int pduNo, byte[] asn1String)
        {
            return S1Decode(bitStream, bitStreamLength, pduNo, asn1String);
        }
    }
}
