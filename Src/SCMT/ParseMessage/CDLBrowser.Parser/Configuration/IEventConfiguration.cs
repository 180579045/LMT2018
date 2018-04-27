// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEventConfiguration.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the IEventConfiguration type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    /// <summary>
    /// The EventConfiguration interface.
    /// </summary>
    public interface IEventConfiguration
    {
        /// <summary>
        /// Gets the version.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Gets the event head node.
        /// </summary>
        IConfigNode EventHeadNode { get; }

        /// <summary>
        /// The get event body node by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="IConfigNode"/>.
        /// </returns>
        IConfigNode GetEventBodyNodeById(int id);
    }
}
