// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingDataLengthParser.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the BindingDataLengthParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.DataLength
{
    using System;

    using CDLBrowser.Parser.Document.Event;

    using Common.Logging;

    /// <summary>
    /// The binding data length parser.
    /// </summary>
    public class BindingDataLengthParser : IDataLengthParser
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BindingDataLengthParser));

        /// <summary>
        /// The get data length.
        /// </summary>
        /// <param name="eventTreeNode">
        /// The event tree node.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        public int GetDataLength(IConfigNodeWrapper eventTreeNode)
        {
            string dataLengthAttribute = eventTreeNode.ConfigurationNode.GetAttribute("DataLength");

            try
            {
                IConfigNodeWrapper bindingTreeNode = BindingHelper.GetBindingNode(eventTreeNode, dataLengthAttribute);
                if (dataLengthAttribute.Contains("*") == false)
                {
                    if (null != bindingTreeNode)
                    {
                        // get the binding data length
                        int dataLength = Convert.ToInt32(bindingTreeNode.Value);
                        return dataLength;
                    }
                }
                else
                {
                    if (null != bindingTreeNode)
                    {
                        // get the binding data length
                        int dataLength = Convert.ToInt32(bindingTreeNode.Value);
                        string strscaler = dataLengthAttribute.Substring(dataLengthAttribute.IndexOf("*")+ 1,dataLengthAttribute.IndexOf("}") - dataLengthAttribute.IndexOf("*") - 1).Trim();
                        int scaler = Convert.ToInt32(strscaler);
                        return dataLength * scaler;
                    }
                }


                Log.Error(string.Format("binding syntax error,Id = {0},{1}", eventTreeNode.ConfigurationNode.Id, dataLengthAttribute));
                return -1;
            }
            catch (Exception ex)
            {
                Log.Error("GetDataLength error,message = " + ex.Message);
                return -1;
            }
        }
    }
}
