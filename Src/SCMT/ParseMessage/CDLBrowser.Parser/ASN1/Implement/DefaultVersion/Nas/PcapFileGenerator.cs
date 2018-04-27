// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PcapFileGenerator.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Description of PcapFileGenerator
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.ASN1.Implement.DefaultVersion.Nas
{
    using System.IO;

    using SuperLMT.Utils;

    /// <summary>
    /// Description of PcapFileGenerator
    /// </summary>
    public sealed class PcapFileGenerator
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static PcapFileGenerator instance = new PcapFileGenerator();

        /// <summary>
        /// The app path.
        /// </summary>
        private string filePath = AppPathUtiliy.Singleton.GetAppPath() + "decode_temp.pcap"; 

        /// <summary>
        /// The header data.
        /// </summary>
        private byte[] headerData = new byte[]
            {
                0xD4, 0xC3, 0xB2, 0xA1, 0x02, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x90
                , 0x01, 0x00, 0x93, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

        /// <summary>
        /// Prevents a default instance of the <see cref="PcapFileGenerator"/> class from being created.
        /// </summary>
        private PcapFileGenerator()
        {
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static PcapFileGenerator Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Gets the pcap file.
        /// </summary>
        public string PcapFile
        {
            get
            {
                return this.filePath;
            }
        }

        /// <summary>
        /// The generate.
        /// </summary>
        /// <param name="nasRawData">
        /// The nas raw data.
        /// </param>
        public void Generate(byte[] nasRawData)
        {
            FileInfo fi = new FileInfo(this.filePath);
            if(fi.Exists)
            {
                fi.Delete();
            }
            var file = new FileStream(this.filePath, FileMode.OpenOrCreate);

            file.Write(this.headerData, 0, this.headerData.Length);

            int dataLength = nasRawData.Length;
            byte[] dataLengthBuffer = System.BitConverter.GetBytes(dataLength);

            file.Write(dataLengthBuffer, 0, 4);
            file.Write(dataLengthBuffer, 0, 4);

            file.Write(nasRawData, 0, dataLength);

            file.Close();
        }
    }
}
