// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NasDecoderImpl.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Description of NasParser
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.ASN1.Implement.DefaultVersion.Nas
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    using Common.Logging;

    using SuperLMT.Utils;

    /// <summary>
    /// Description of NasParser
    /// </summary>
    public sealed class NasDecoderImpl
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(NasDecoderImpl));

        /// <summary>
        /// The instance.
        /// </summary>
        private static NasDecoderImpl instance = new NasDecoderImpl();

        /// <summary>
        /// The parameters.
        /// </summary>
        private string parameters = "-o \"uat:user_dlts:\\\"User 0 (DLT=147)\\\",\\\"nas-eps\\\",\\\"0\\\",\\\"\\\",\\\"0\\\",\\\"\\\"\" -r \"" + PcapFileGenerator.Instance.PcapFile
            + "\" -V";

        /// <summary>
        /// Prevents a default instance of the <see cref="NasDecoderImpl"/> class from being created. 
        /// Prevents a default instance of the <see cref="NasDecoder"/> class from being created.
        /// </summary>
        private NasDecoderImpl()
        {
            this.Deseriliaze(AppPathUtiliy.Singleton.GetAppPath() + "\\Configuration\\NasDecoder.txt");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static NasDecoderImpl Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Gets or sets the wireshark path.
        /// </summary>
        public string WiresharkPath { get; set; }

        /// <summary>
        /// The de serialize.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public void Deseriliaze(string fileName)
        {
            try
            {
                var readFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                var reader = new StreamReader(readFileStream);
                this.WiresharkPath = reader.ReadToEnd().Trim();
            }
            catch (Exception ex)
            {
                Log.Error("SerilizeToFile error,message = " + ex.Message);
                this.WiresharkPath = @"C:\Program Files\Wireshark";
            }
        }

        /// <summary>
        /// The parse.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string Parse()
        {
            var fi = new FileInfo(this.WiresharkPath + "\\" + "tshark.exe");
            if (!fi.Exists)
            {
                var promptInfo = new StringBuilder();
                promptInfo.Append("没有找到NAS解码器，请保证已经安装了WireShark。\r\n");
                promptInfo.Append("如果已经成功安装WireShark，请检查\\Configuration\\NasDecoder.txt的内容。\r\n");
                promptInfo.Append("应配置为WIRESHARK_INSTALL_PATH,其中WIRESHARK_INSTALL_PATH为WireShark安装路径。\r\n");
                promptInfo.Append("配置完成后，请重新启动CDL软件。\r\n");
                return promptInfo.ToString();
            }

            var startInfo = new ProcessStartInfo
                {
                    FileName = this.WiresharkPath + "\\" + "tshark.exe",
                    Arguments = this.parameters,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

            var p = new Process { StartInfo = startInfo };

            p.Start();
            var sb = new StringBuilder();
            int lineIndex = 0;
            while (!p.StandardOutput.EndOfStream)
            {
                if (lineIndex++ > 12)
                {
                    sb.Append(p.StandardOutput.ReadLine());
                    sb.Append("\r\n");
                }
                else
                {
                    p.StandardOutput.ReadLine();
                }
            }

            p.WaitForExit();
            return sb.ToString();
        }
    }
}
