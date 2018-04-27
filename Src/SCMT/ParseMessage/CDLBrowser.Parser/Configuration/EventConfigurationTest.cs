// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventConfigurationTest.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the EventConfigurationTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using NUnit.Framework;

    /// <summary>
    /// The event configuration test.
    /// </summary>
    [TestFixture]
    public class EventConfigurationTest
    {
        /// <summary>
        /// The configuration 182.
        /// </summary>
        private readonly IEventConfiguration configuration182 = new ConfigurationManager().GetEventConfiguration("1.3.0182");

        /// <summary>
        /// The configuration 181.
        /// </summary>
        private readonly IEventConfiguration configuration181 = new ConfigurationManager().GetEventConfiguration("1.3.0181");

        /// <summary>
        /// The version test.
        /// </summary>
        [Test]
        public void VersionTest()
        {
            StringAssert.AreEqualIgnoringCase(this.configuration182.Version, "1.3.0182");
        }

        /// <summary>
        /// The event head node test.
        /// </summary>
        [Test]
        public void EventHeadNodeTest()
        {
            /*
                     <HeadNode ID="DataLength" Describe="Event Length" DataType="USHORT16"/>
                     <HeadNode ID="Time" Describe="Half SFN" DataType="USHORT16"/>  
                     <HeadNode ID="EventType" Describe="Event Type" DataType="UINT32"/>
                     <HeadNode ID="InterfaceType" Describe="Protocol" DataType="UINT8"/>
                     <HeadNode ID="LocalCellID" Describe="Local Cell ID" DataType="UINT8"/>
                     <HeadNode ID="CellUEIndex" Describe="Cell UE Index" DataType="USHORT16"/>
                     <HeadNode ID="CellID" Describe="Cell ID" DataType="UINT8"/>
                     <HeadNode ID="Pad[3]" Describe="Pad[3]" DataLength="3" DataType="UINT8"/>
             */
            IConfigNode headNode = this.configuration182.EventHeadNode;
            Assert.AreEqual(headNode.Children.Count, 8);

            StringAssert.AreEqualIgnoringCase(headNode.Children[0].GetAttribute("ID"), "DataLength");
            StringAssert.AreEqualIgnoringCase(headNode.Children[1].GetAttribute("ID"), "Time");
            StringAssert.AreEqualIgnoringCase(headNode.Children[2].GetAttribute("ID"), "EventType");
            StringAssert.AreEqualIgnoringCase(headNode.Children[3].GetAttribute("ID"), "InterfaceType");
            StringAssert.AreEqualIgnoringCase(headNode.Children[4].GetAttribute("ID"), "LocalCellID");
            StringAssert.AreEqualIgnoringCase(headNode.Children[5].GetAttribute("ID"), "CellUEIndex");
            StringAssert.AreEqualIgnoringCase(headNode.Children[6].GetAttribute("ID"), "CellID");
            StringAssert.AreEqualIgnoringCase(headNode.Children[7].GetAttribute("ID"), "Pad[3]");

            /*
                     <HeadNode ID="DataLength" Describe="Event Length" DataType="USHORT16"/>
                     <HeadNode ID="Time" Describe="Half SFN" DataType="USHORT16"/>  
                     <HeadNode ID="EventType" Describe="Event Type" DataType="UINT32"/>
                     <HeadNode ID="InterfaceType" Describe="Protocol" DataType="UINT8"/>
                     <HeadNode ID="LocalCellID" Describe="Local Cell ID" DataType="UINT8"/>
                     <HeadNode ID="CellUEIndex" Describe="Cell UE Index" DataType="USHORT16"/>
            */
            IConfigNode headNode181 = this.configuration181.EventHeadNode;
            Assert.AreEqual(headNode181.Children.Count, 6);

            StringAssert.AreEqualIgnoringCase(headNode181.Children[0].GetAttribute("ID"), "DataLength");
            StringAssert.AreEqualIgnoringCase(headNode181.Children[1].GetAttribute("ID"), "Time");
            StringAssert.AreEqualIgnoringCase(headNode181.Children[2].GetAttribute("ID"), "EventType");
            StringAssert.AreEqualIgnoringCase(headNode181.Children[3].GetAttribute("ID"), "InterfaceType");
            StringAssert.AreEqualIgnoringCase(headNode181.Children[4].GetAttribute("ID"), "LocalCellID");
            StringAssert.AreEqualIgnoringCase(headNode181.Children[5].GetAttribute("ID"), "CellUEIndex");
        }

        /// <summary>
        /// The get event body node by id test.
        /// </summary>
        [Test]
        public void GetEventBodyNodeByIdTest()
        {
/*
    <Event ID="0x00000104" Describe="S1 ERAB Release Command" MsgDirection="EPC-&gt;eNB">
    <ParaNode ID="MMEUEID" Describe="MMEUEID" DataType="UINT32" />
    <ParaNode ID="eNBUEID" Describe="eNBUEID" DataType="USHORT16" />
    <ParaNode ID="UEDLAMBR" Describe="UE DL AMBR" DataType="UINT32" />
    <ParaNode ID="UEULAMBR" Describe="UE UL AMBR" DataType="UINT32" />
    <ParaNode ID="ReleasedERABnum" Describe="Released ERAB num" DataType="UINT8" />
    <ParaStruct TotalNum="ReleasedERABnum" Describe="Released ERAB">
      <ParaNode ID="ERABID" Describe="ERAB ID" DataType="UINT8" />
      <ParaNode ID="CAUSETYPE" Describe="CAUSE TYPE" DataType="UINT8" DisplayType="ENUM" ValueRange="1~radioNetwork_chosen|2~transport_chosen|3~nas_chosen|4~protocol_chosen|5~misc_chosen"/>
      <ParaNode ID="CAUSEVALUE" Describe="CAUSE VALUE" DataType="UINT8" />
    </ParaStruct>
  </Event>
*/
            IConfigNode bodyNode182 = this.configuration182.GetEventBodyNodeById(0x00000104);
            Assert.AreEqual(bodyNode182.Children.Count, 6);

            Assert.AreEqual(bodyNode182.Children[5].Children.Count, 3);
            StringAssert.AreEqualIgnoringCase(bodyNode182.Children[5].Children[2].Id, "CAUSEVALUE");
        }
    }
}
