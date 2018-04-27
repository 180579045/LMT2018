// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventTest.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the EventTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.Event
{
    using NUnit.Framework;

    /// <summary>
    /// The event test.
    /// </summary>
    [TestFixture]
    public class EventTest
    {
        /// <summary>
        /// The raw data for test.
        /// </summary>
        private readonly byte[] rawDataForTest = new byte[]
                                            {
                                                0x00, 0x10, 0x07, 0xa6, 0x00, 0x00, 0x17, 0x17, 0x32, 0x00, 0x00, 0x04,
                                                0x02, 0x00, 0x00, 0x06
                                            };

        /// <summary>
        /// The setup.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            //EventParser.Singleton.Version = "1.3.0182";
        }
        
        /// <summary>
        /// The initialize persistent data test.
        /// </summary>
        [Test]
        public void InitializePersistentDataTest()
        {
            //UnitOfWork session = DALManager.GetSingleton().GetUnitOfWork();

            //var evt = new Event(session, 1) { Name = "222" };

            //evt.InitializePersistentData(this.rawDataForTest);

            //Assert.AreEqual(1958, evt.HalfSubFrameNo);

            //session.CommitChanges();
        }
    }
}
