// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataTypeUtility.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the DataGenerator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDLBrowser.Parser.Document.DataLength
{
    using System.Collections.Generic;

    using Common.Logging;

    /// <summary>
    /// The data type utility.
    /// </summary>
    public class DataTypeUtility
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(DataTypeUtility));

        /// <summary>
        /// The type 2 length map.
        /// </summary>
        private static readonly IDictionary<string, int> Type2LengthMap = new Dictionary<string, int>();

        /// <summary>
        /// Initializes static members of the <see cref="DataTypeUtility"/> class.
        /// </summary>
        static DataTypeUtility()
        {
            Type2LengthMap.Add("UINT64", 8);
            Type2LengthMap.Add("UINT32", 4);
            Type2LengthMap.Add("UINT32_ARRAY", 1);
            Type2LengthMap.Add("INT32", 4);
            Type2LengthMap.Add("DateTime", 4);
            Type2LengthMap.Add("USHORT16", 2);
            Type2LengthMap.Add("USHORT16_ARRAY", 2);
            Type2LengthMap.Add("SHORT16", 2);
            Type2LengthMap.Add("UINT8", 1);
            Type2LengthMap.Add("UINT8_ARRAY", 1);
            Type2LengthMap.Add("INT8", 1);
            Type2LengthMap.Add("CHAR_ARRAY", 1);
            
            
        }

        /// <summary>
        /// The get data length.
        /// </summary>
        /// <param name="dataType">
        /// The data type.
        /// </param>
        /// <returns>
        /// The length ,-1:error<see cref="int"/>.
        /// </returns>
        public static int GetDataTypeLength(string dataType)
        {
            // 返回值为-1导致解析树阴影错误
            if (string.IsNullOrEmpty(dataType))
            {
                return 0;
            }

            if (!Type2LengthMap.ContainsKey(dataType))
            {
                Log.Info("cant find the data type:" + dataType);
                return 0;
            }

            return Type2LengthMap[dataType];
        }
    }
}
