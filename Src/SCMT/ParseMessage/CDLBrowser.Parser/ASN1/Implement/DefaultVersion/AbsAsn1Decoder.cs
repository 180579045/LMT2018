// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbsAsn1Decoder.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the AbsAsn1Decoder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.ASN1.Implement.DefaultVersion
{
    using System.Linq;
    using System.Text;

    using CDLBrowser.Parser.ASN1.Interface;

    using Common.Logging;
    using System.Threading;

    public class MutexManger
    {
        Mutex mutex = new Mutex();
        public static MutexManger Singleton = new MutexManger();

        private MutexManger()
        {
            
        }
        public void Lock()
        {
            mutex.WaitOne();
        }
        public void Release()
        {
            mutex.ReleaseMutex();
        }
    }

    /// <summary>
    /// The abs asn.1 decoder.
    /// </summary>
    public abstract class AbsAsn1Decoder : IAsn1Decoder
    {

        private Mutex mutex = new Mutex();
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(AbsAsn1Decoder));

        /// <summary>
        /// The output decode buffer.
        /// </summary>
        private readonly byte[] outputDecodeBuffer = new byte[204800];

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
        /// The asn.1 string.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public bool Decode(byte[] bitStream, int bitStreamLength, int pduNo, out string asn1String)
        {
            
            asn1String = null;
            this.outputDecodeBuffer.Initialize();

            //lock (this)
            //{
            MutexManger.Singleton.Lock();
            int result = this.DecodeImpl(bitStream, bitStreamLength, pduNo, this.outputDecodeBuffer);
            int stringByteCount = this.outputDecodeBuffer.TakeWhile(b => b != 0).Count();
                
            bool brest = result == 0 ? true : false;
            if (0 == result)
            {
                asn1String = Encoding.ASCII.GetString(this.outputDecodeBuffer, 0, stringByteCount);
                /*return true;*/
            }
            else
            {
                var promptInfo = Encoding.ASCII.GetString(this.outputDecodeBuffer, 0, stringByteCount);
                //yanjiewa Log.Error(promptInfo);
                asn1String = "\nATTENTION:Wrong Bit Stream Input";
            }
            MutexManger.Singleton.Release();
            return brest;
           /* }*/
            
        }

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
        protected abstract int DecodeImpl(byte[] bitStream, int bitStreamLength, int pduNo, byte[] asn1String);
    }
}
