// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsnTypeConverter.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   The asn type converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.Snmp
{
    using System.Net;

    using SnmpSharpNet;

    /// <summary>
    /// The asn type converter.
    /// </summary>
    public static class AsnTypeConverter
    {
        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="AsnType"/>.
        /// </returns>
        public static AsnType Convert(string value, string type)
        {
            switch (type)
            {
                case "s32[]":
                    return new Oid(value);
                case "u8[]":
                    var ipAddress = new IpAddress(IPAddress.Parse(value));
                    var curBytes = new byte[4];
                    curBytes[0] = ipAddress[0];
                    curBytes[1] = ipAddress[1];
                    curBytes[2] = ipAddress[2];
                    curBytes[3] = ipAddress[3];

                    var octIpAddr = new OctetString(curBytes);
                    return octIpAddr;
                case "enum":
                case "u8":
                case "s32":
                    if (value == string.Empty)
                    {
                        value = "0";
                    }

                    return new Integer32(int.Parse(value));
                case "BITS":
                    if (value == string.Empty)
                    {
                        value = "0";
                    }

                    return new Gauge32(value);
                case "s8[]":
                    if (ReferenceEquals(null, value))
                    {
                        return new OctetString();
                    }

                    return new OctetString(value);
                default:
                    return null;
            }
        }
    }
}
