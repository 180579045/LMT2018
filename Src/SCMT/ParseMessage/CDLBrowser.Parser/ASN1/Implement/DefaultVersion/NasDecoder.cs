// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NasDecoder.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   The nas decoder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.ASN1.Implement.DefaultVersion
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The nas decoder.
    /// </summary>
    public class NasDecoder : AbsAsn1Decoder
    {
        /// <summary>
        /// The nas decode.
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
        [DllImport("Asn1Decode.dll", EntryPoint = "NasDecode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int NasDecode(byte[] bitStream, int bitStreamLength, int pduNo, byte[] asn1String);

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
            return NasDecode(bitStream, bitStreamLength, pduNo, asn1String);
        }
    }
}
