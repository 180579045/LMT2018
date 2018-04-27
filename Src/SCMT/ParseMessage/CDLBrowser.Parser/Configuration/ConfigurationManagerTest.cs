// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationManagerTest.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the ConfigurationManagerTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Configuration
{
    using NUnit.Framework;

    /// <summary>
    /// The configuration manager test.
    /// </summary>
    [TestFixture]
    public class ConfigurationManagerTest
    {
        /// <summary>
        /// The get event configuration test.
        /// </summary>
        [Test]
        public void GetEventConfigurationTest()
        {
            IEventConfiguration configuration182 = new ConfigurationManager().GetEventConfiguration("1.3.0182");
            Assert.AreSame(configuration182, new ConfigurationManager().GetEventConfiguration("1.3.0182"));

            IEventConfiguration configuration181 = new ConfigurationManager().GetEventConfiguration("1.3.0181");
            Assert.AreNotSame(configuration182, configuration181);
        }
    }
}
