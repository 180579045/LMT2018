// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Asn1DecoderFactoryTest.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   The asn 1 decoder factory test.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.ASN1.Implement
{
    using System.Diagnostics.CodeAnalysis;

    using CDLBrowser.Parser.ASN1.Interface;

    using NUnit.Framework;

    using Spring.Context.Support;

    /// <summary>
    /// The asn 1 decoder factory test.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    [TestFixture]
    public class Asn1DecoderFactoryTest
    {
        /// <summary>
        /// The xml app context.
        /// </summary>
        private XmlApplicationContext xmlAppContext;

        /// <summary>
        /// The factory.
        /// </summary>
        private IAsn1DecoderFactory factory;

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.xmlAppContext = new XmlApplicationContext(string.Format("assembly://CDLBrowser.Parser/CDLBrowser.Parser/ASN1.AsnParserSpring.xml"));
            this.factory = this.xmlAppContext.GetObject(@"Asn1DecoderFactory") as IAsn1DecoderFactory;
        }

        /// <summary>
        /// The get decoder test.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        [Test]
        public void GetDecoderTest()
        {
            Assert.NotNull(this.factory);

            if (this.factory != null)
            {
                var s115Decoder = this.factory.GetDecoder("S1", "1.5");
                Assert.AreEqual(null, s115Decoder);

                var s1DefaultDecoder = this.factory.GetDecoder("S1", "DefaultVersion");
                Assert.AreNotEqual(null, s1DefaultDecoder);

                var x215Decoder = this.factory.GetDecoder("x2", "1.5");
                Assert.AreEqual(null, x215Decoder);

                var x2DefaultDecoder = this.factory.GetDecoder("x2", "DefaultVersion");
                Assert.AreNotEqual(null, x2DefaultDecoder);

                var rrc15Decoder = this.factory.GetDecoder("rrc", "1.5");
                Assert.AreEqual(null, rrc15Decoder);

                var rrcDefaultDecoder = this.factory.GetDecoder("rrc", "DefaultVersion");
                Assert.AreNotEqual(null, rrcDefaultDecoder);
            }
        }

        /// <summary>
        /// The get decoder default test.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        [Test]
        public void GetDecoderDefaultTest()
        {
            Assert.NotNull(this.factory);
            if (this.factory != null)
            {
                var s1DefaultDecoder = this.factory.GetDecoder("S1");
                Assert.AreNotEqual(null, s1DefaultDecoder);

                var x2DefaultDecoder = this.factory.GetDecoder("X2");
                Assert.AreNotEqual(null, x2DefaultDecoder);

                var rrcDefaultDecoder = this.factory.GetDecoder("RRC");
                Assert.AreNotEqual(null, rrcDefaultDecoder);
            }
        }
    }
}
