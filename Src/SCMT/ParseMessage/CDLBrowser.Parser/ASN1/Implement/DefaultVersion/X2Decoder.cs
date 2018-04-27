// --------------------------------------------------------------------------------------------------------------------
// <copyright file="X2Decoder.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the X2Decoder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.ASN1.Implement.DefaultVersion
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The x 2 decoder.
    /// </summary>
    public class X2Decoder : AbsAsn1Decoder
    {
        /// <summary>
        /// The x 2 decode.
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
        [DllImport("Asn1Decode.dll", EntryPoint = "X2Decode", CallingConvention = CallingConvention.Cdecl)]
        public static extern int X2Decode(byte[] bitStream, int bitStreamLength, int pduNo, byte[] asn1String);

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
            return X2Decode(bitStream, bitStreamLength,  pduNo, asn1String);
        }
    }
}
