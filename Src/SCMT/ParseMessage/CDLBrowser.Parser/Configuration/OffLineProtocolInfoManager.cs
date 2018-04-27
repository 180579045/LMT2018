// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OffLineProtocolInfoManager.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the OffLineProtocolInfoManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using System.Collections.ObjectModel;
    using System.Windows.Media;

    using SuperLMT.Utils;

    /// <summary>
    /// The off line protocol info manager.
    /// </summary>
    public class OffLineProtocolInfoManager
    {
        /// <summary>
        /// The manager.
        /// </summary>
        private static OffLineProtocolInfoManager manager = new OffLineProtocolInfoManager();

        /// <summary>
        /// The protocols info impl.
        /// </summary>
        private readonly ProtocolsInfo protocolsInfoImpl;

        /// <summary>
        /// Prevents a default instance of the <see cref="OffLineProtocolInfoManager"/> class from being created.
        /// </summary>
        private OffLineProtocolInfoManager()
        {
            this.protocolsInfoImpl = new ProtocolsInfo(AppPathUtiliy.Singleton.GetAppPath() + "\\Configuration\\ProtocolInfos.xml");
        }

        /// <summary>
        /// Gets the protocols.
        /// </summary>
        public ObservableCollection<Protocol> Protocols
        {
            get
            {
                return this.protocolsInfoImpl.Protocols;
            }
        }

        /// <summary>
        /// The get singleton.
        /// </summary>
        /// <returns>
        /// The <see cref="OffLineProtocolInfoManager"/>.
        /// </returns>
        public static OffLineProtocolInfoManager GetSingleton()
        {
            return manager ?? (manager = new OffLineProtocolInfoManager());
        }

        /// <summary>
        /// The reset color.
        /// </summary>
        public void ResetColor()
        {
            this.protocolsInfoImpl.ResetColor();
        }

        /// <summary>
        /// The get protocol name.
        /// </summary>
        /// <param name="protocolId">
        /// The protocol id.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetProtocolName(uint protocolId)
        {
            return this.protocolsInfoImpl.GetProtocolName(protocolId);
        }

        /// <summary>
        /// The get protocol color.
        /// </summary>
        /// <param name="protocolName">
        /// The protocol name.
        /// </param>
        /// <returns>
        /// The <see cref="Color"/>.
        /// </returns>
        public string GetProtocolColor(string protocolName)
        {
            return this.protocolsInfoImpl.GetProtocolColor(protocolName);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.protocolsInfoImpl.SerializeToFile(AppPathUtiliy.Singleton.GetAppPath() + "\\Configuration\\ProtocolInfos.xml");
        }
    }
}
