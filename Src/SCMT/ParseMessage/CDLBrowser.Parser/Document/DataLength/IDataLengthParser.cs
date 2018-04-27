// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataLengthParser.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the IDataLengthParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.DataLength
{
    using CDLBrowser.Parser.Document.Event;

    /// <summary>
    /// The DataLengthParser interface.
    /// </summary>
    public interface IDataLengthParser
    {
        /// <summary>
        /// The get data length.
        /// </summary>
        /// <param name="eventTreeNode">
        /// The event Tree Node.
        /// </param>
        /// <returns>
        /// The data length ,-1:error<see cref="int"/>.
        /// </returns>
        int GetDataLength(IConfigNodeWrapper eventTreeNode);
    }
}
