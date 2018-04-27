// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataLengthParser.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   The data length parser.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.DataLength
{
    using System;

    using CDLBrowser.Parser.Document.Event;

    /// <summary>
    /// The data length parser.
    /// </summary>
    public class DataLengthParser
    {
        /// <summary>
        /// The instance.
        /// </summary>
        //private static readonly DataLengthParser Instance = new DataLengthParser();

        /// <summary>
        /// The common data length parser.
        /// </summary>
        private readonly IDataLengthParser commonDataLengthParserImpl = new CommonDataLengthParser();

        /// <summary>
        /// The binding data length parser.
        /// </summary>
        private readonly IDataLengthParser bindingDataLengthParserImpl = new BindingDataLengthParser();

        /// <summary>
        /// Prevents a default instance of the <see cref="DataLengthParser"/> class from being created.
        /// </summary>
        public DataLengthParser()
        {
        }

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        //public static DataLengthParser Singleton
        //{
        //    get
        //    {
        //        return Instance;
        //    }
        //}

        /// <summary>
        /// The get data length.
        /// </summary>
        /// <param name="eventTreeNode">
        /// The event tree node.
        /// </param>
        /// <param name="isUsingBinding">
        /// The is Using Binding.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetDataLength(IConfigNodeWrapper eventTreeNode, ref bool isUsingBinding)
        {
            string dataLengthAttribute = eventTreeNode.ConfigurationNode.GetAttribute("DataLength");
            if (null != dataLengthAttribute)
            {
                if (dataLengthAttribute.IndexOf("Binding", StringComparison.Ordinal) > 0)
                {
                    isUsingBinding = true;
                    return this.bindingDataLengthParserImpl.GetDataLength(eventTreeNode);
                }
                else
                {
                    isUsingBinding = false;
                    return this.commonDataLengthParserImpl.GetDataLength(eventTreeNode);
                }
            }
            else
            {
                int dataTypeLenth = DataTypeUtility.GetDataTypeLength(eventTreeNode.ConfigurationNode.GetAttribute("DataType"));
                if ( dataTypeLenth < 1)
                {
                    return 0;
                }
                return dataTypeLenth;
            }

        }
    }
}
