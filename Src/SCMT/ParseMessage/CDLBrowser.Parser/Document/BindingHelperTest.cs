// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingHelperTest.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the BindingHelperTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document
{
    using System.Collections.Generic;

    using CDLBrowser.Parser.Configuration;
    using CDLBrowser.Parser.Document.Event;

    using Moq;

    using NUnit.Framework;

    /// <summary>
    /// The binding helper test.
    /// </summary>
    [TestFixture]
    public class BindingHelperTest
    {
        /// <summary>
        /// The get binding node test.
        /// </summary>
        [Test]
        public void GetBindingNodeTest()
        {
            var eventRootNode = new Mock<IConfigNodeWrapper>();
            var eventNode1 = new Mock<IConfigNodeWrapper>();
            var eventNode2 = new Mock<IConfigNodeWrapper>();
            var eventNode3 = new Mock<IConfigNodeWrapper>();
            var eventNode4 = new Mock<IConfigNodeWrapper>();

            eventRootNode.Setup(p => p.Parent).Returns((IConfigNodeWrapper)null);
            eventRootNode.Setup(p => p.Children).Returns(
                new List<IConfigNodeWrapper> { eventNode1.Object, eventNode2.Object });

            eventNode1.Setup(p => p.Parent).Returns(eventRootNode.Object);
            eventNode2.Setup(p => p.Parent).Returns(eventRootNode.Object);

            eventNode1.Setup(p => p.Children).Returns(
                new List<IConfigNodeWrapper> { eventNode3.Object, eventNode4.Object });

            eventNode3.Setup(p => p.Parent).Returns(eventNode1.Object);
            eventNode4.Setup(p => p.Parent).Returns(eventNode1.Object);

            var configNodeOfNode3 = new Mock<IConfigNode>();
            configNodeOfNode3.Setup(p => p.Id).Returns("MsgDirection");

            eventNode3.Setup(p => p.ConfigurationNode).Returns(configNodeOfNode3.Object);

            IConfigNodeWrapper bindingNode = BindingHelper.GetBindingNode(eventNode2.Object, "{Binding MsgDirection}");
            Assert.AreSame(bindingNode, eventNode3.Object);
        }
    }
}
