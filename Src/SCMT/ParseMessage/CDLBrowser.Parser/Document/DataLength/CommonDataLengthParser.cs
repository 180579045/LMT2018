// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonDataLengthParser.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the CommonDataLengthParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.DataLength
{
    using System;

    using CDLBrowser.Parser.Document.Event;

    using Common.Logging;

    /// <summary>
    /// The common data length parser.
    /// </summary>
    public class CommonDataLengthParser : IDataLengthParser
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(CommonDataLengthParser));

        /// <summary>
        /// The get data length.
        /// </summary>
        /// <param name="eventTreeNode">
        /// The event tree node.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetDataLength(IConfigNodeWrapper eventTreeNode)
        {
            try
            {
                int dataLength = 1;
                string dataLengthAttribute = eventTreeNode.ConfigurationNode.GetAttribute("DataLength");
                if (!string.IsNullOrEmpty(dataLengthAttribute))
                {
                    dataLength = Convert.ToInt32(dataLengthAttribute);
                }

                // int dataTypeLenth = DataTypeUtility.GetDataTypeLength(eventTreeNode.ConfigurationNode.GetAttribute("DataType"));
                // if (-1 == dataTypeLenth)
                // {
                // return -1;
                // }* dataTypeLenth

                return dataLength;
            }
            catch (Exception ex)
            {
                Log.Error("GetDataLength error,message = " + ex.Message);
                return -1;
            }
        }
    }
}
