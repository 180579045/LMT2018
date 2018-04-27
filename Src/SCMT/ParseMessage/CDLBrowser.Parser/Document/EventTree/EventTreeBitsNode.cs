// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTreeBitsNode.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the EventTreeBitsNode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.EventTree
{
    using CDLBrowser.Parser.Document.DataLength;
    using CDLBrowser.Parser.Document.Event;

    using SuperLMT.Utils;

    /// <summary>
    /// The event tree bits node.
    /// </summary>
    public class EventTreeBitsNode : ConfigNodeWrapper
    {
        /// <summary>
        /// The is data length using binding.
        /// </summary>
        private bool isDataLengthUsingBinding = true;

        /// <summary>
        /// Gets the display content.
        /// </summary>
        public override string DisplayContent
        {
            get
            {
                return this.ConfigurationNode.GetAttribute("Describe") + " (BitSection)";
            }
        }

        /// <summary>
        /// The initialize value.
        /// </summary>
        /// <param name="memoryStream">
        /// The memory stream.
        /// </param>
        public override void InitializeValue(System.IO.Stream memoryStream, string btsversion)
        {
            int dataLength = 0;
            if (this.isDataLengthUsingBinding)
            {
                dataLength = new DataLengthParser().GetDataLength(this, ref this.isDataLengthUsingBinding);
            }

            this.DataLength = dataLength;

            var dataBuffer = new byte[dataLength];
            memoryStream.Read(dataBuffer, 0, dataLength);
            this.Value =ConvertUtil.Singleton.ConvertByteArrayToDataTypeObject(
                    this.ConfigurationNode.GetAttribute("DataType"), dataBuffer);

            this.CreateBitSectionNodes();
        }

        /// <summary>
        /// The clone.
        /// </summary>
        /// <returns>
        /// The <see cref="IConfigNodeWrapper"/>.
        /// </returns>
        public override IConfigNodeWrapper Clone()
        {
            return new EventTreeBitsNode { ConfigurationNode = this.ConfigurationNode, Parent = this.Parent };
        }

        /// <summary>
        /// The create bit section nodes.
        /// </summary>
        private void CreateBitSectionNodes()
        {
            uint primeValue = System.Convert.ToUInt32(this.Value);

            // ValueRange="cell_index~5|half_sfn~11"
            string valueRange = this.ConfigurationNode.GetAttribute("ValueRange");

            string[] bitSections = valueRange.Split('|');
            int startPosition = 0;
            foreach (var bitSection in bitSections)
            {
                string section = bitSection.TrimEnd('|');
                string[] nameAndLength = section.Split('~');

                string sectionName = nameAndLength[0].TrimEnd('~');
                int bitsLength = System.Convert.ToInt32(nameAndLength[1]);
                var sectionNode = new EventTreeBitSectionNode(sectionName, startPosition, bitsLength, primeValue) { Parent = this };
                this.Children.Add(sectionNode);
                startPosition += bitsLength;
            }
        }
    }
}
