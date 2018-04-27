// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecondaryParserTest.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the SecondaryParserTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.AddIn.OffLine.Document
{
    using CDLBrowser.Parser.Document;
    using CDLBrowser.Parser.Document.Event;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// The secondary parser test.
    /// </summary>
    [TestFixture]
    public class SecondaryParserTest
    {
        /// <summary>
        /// The visit test.
        /// </summary>
        [Test]
        public void VisitTest()
        {
            var eventMoq = new Mock<IEvent>();
            eventMoq.Setup(e => e.RawData)
                    .Returns(
                        new byte[]
                            {
                                0x00, 0x10, 0x07, 0xa6, 0x00, 0x00, 0x17, 0x17, 0x32, 0x00, 0x00, 0x04, 0x02, 0x00, 0x00, 
                                0x06
                            });
            eventMoq.Setup(e => e.Version)
                    .Returns("1.3.0170");

            eventMoq.Setup(e => e.DisplayIndex)
                    .Returns(1);
            IConfigNodeWrapper rootNode = new SecondaryParser().Parse(eventMoq.Object);
            Assert.IsNotNull(rootNode);

            string eventTreeContent = string.Empty;
            this.GetChildrenDisplayContent(rootNode, ref eventTreeContent);
            //StringAssert.AreEqualIgnoringCase(
            //    eventTreeContent, 
            //    "Event 1  (16 bytes)Event Length :  16Half SFN :  1958Event Type :  MACRRC_MIMOCHANGE (0x00001717)Protocol :  RRC--L2 (0x32)Local Cell ID :  0Cell UE Index :  4MACRRC_MIMOCHANGE  (MAC --> RRC)TmModex :  2Pad[3] :  0 0 6");
        }

        /// <summary>
        /// The get children display content.
        /// </summary>
        /// <param name="parentNode">
        /// The parent node.
        /// </param>
        /// <param name="content">
        /// The content.
        /// </param>
        private void GetChildrenDisplayContent(IConfigNodeWrapper parentNode, ref string content)
        {
            content += parentNode.DisplayContent;
            foreach (var eventTreeNode in parentNode.Children)
            {
                this.GetChildrenDisplayContent(eventTreeNode, ref content);
            }
        }
    }
}
