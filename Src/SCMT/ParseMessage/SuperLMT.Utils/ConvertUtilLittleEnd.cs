// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConvertUtil.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the ConvertUtil type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Common.Logging;

    /// <summary>
    /// The convert utility.
    /// </summary>
    public class ConvertUtilLittleEnd
    {
        /// <summary>
        /// The log.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConvertUtilLittleEnd));

        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly ConvertUtilLittleEnd Instance = new ConvertUtilLittleEnd();

        /// <summary>
        /// The data converters.
        /// </summary>
        private readonly Dictionary<string, Func<byte[], object>> dataConverters = new Dictionary<string, Func<byte[], object>>();

        /// <summary>
        /// Prevents a default instance of the <see cref="ConvertUtil"/> class from being created.
        /// </summary>
        public ConvertUtilLittleEnd()
        {
            this.Initialize();
        }

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static ConvertUtilLittleEnd Singleton
        {
            get
            {
                return Instance;
            }
        }

        /// <summary>
        /// Gets or sets the converter parameter.
        /// </summary>
        public object ConverterParameter { get; set; }

        /// <summary>
        /// The convert byte array to data type object.
        /// </summary>
        /// <param name="dataType">
        /// The data type.
        /// </param>
        /// <param name="rawDataBuffer">
        /// The raw data buffer.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object ConvertByteArrayToDataTypeObject(string dataType, byte[] rawDataBuffer)
        {
            if (string.IsNullOrEmpty(dataType) || rawDataBuffer == null)
            {
                return rawDataBuffer;
            }

            Func<byte[], object> convertUtil;
            if (this.dataConverters.TryGetValue(dataType, out convertUtil))
            {
                if (convertUtil != null)
                {
                    return convertUtil(rawDataBuffer);
                }
            }

            return rawDataBuffer;
        }

        /// <summary>
        /// The covert decimal string to hex 8 byte string.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string CovertDecimalStringToHex8ByteString(string value)
        {
            string strCovertResult = string.Empty;
            uint integerValue;
            if (uint.TryParse(value, out integerValue))
            {
                strCovertResult = string.Format(@"0x{0:x8}", integerValue);
            }

            return strCovertResult;
        }

        /// <summary>
        /// The covert decimal string to hex byte string.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string CovertDecimalStringToHexByteString(string value)
        {
            string hexByteString = string.Empty;
            uint integerValue;
            if (uint.TryParse(value, out integerValue))
            {
                hexByteString = string.Format(@"0x{0:x}", integerValue);
            }

            return hexByteString;
        }

        /// <summary>
        /// The translate bytes to time format.
        /// </summary>
        /// <param name="time">
        /// The time.
        /// </param>
        /// <param name="milliSecond">
        /// The milli second.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public DateTime CovertBytesToTimeFormat(string time, string milliSecond)
        {
            var date = new DateTime();
            try
            {
                var byteTimes = this.CovertStringToBytes(time);
                if (byteTimes != null)
                {
                    int year = this.ConvertBytesToInt32(byteTimes, 0, 1);
                    int month = this.ConvertBytesToInt32(byteTimes, 2, 2);
                    int day = this.ConvertBytesToInt32(byteTimes, 3, 3);
                    int hour = this.ConvertBytesToInt32(byteTimes, 4, 4);
                    int minute = this.ConvertBytesToInt32(byteTimes, 5, 5);
                    int second = this.ConvertBytesToInt32(byteTimes, 6, 6);
                    int millisecond = Convert.ToInt32(milliSecond);

                    date =
                        DateTime.Parse(string.Format("{0}/{1}/{2} {3}:{4}:{5}", year, month, day, hour, minute, second)).ToLocalTime();
                    date = date.AddMilliseconds(millisecond);
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("TranslateBytesToTimeFormat erro msg = {0}", ex.Message));
            }

            return date;
        }

        /// <summary>
        /// The translate string to bytes.
        /// </summary>
        /// <param name="strBuffer">
        /// The str buffer.
        /// </param>
        /// <returns>
        /// The <see>
        ///       <cref>byte[]</cref>
        ///     </see> .
        /// </returns>
        public byte[] CovertStringToBytes(string strBuffer)
        {
            try
            {
                string[] strCollction = strBuffer.Split(' ');
                var bytes = new byte[strCollction.Length];
                var byteIndex = 0;

                foreach (var s in strCollction)
                {
                    bytes[byteIndex] = byte.Parse(s);
                    byteIndex++;
                }

                return bytes;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("CovertStringToBytes erro msg = {0}", ex.Message));

                return null;
            }
        }

        /// <summary>
        /// The convert bytes to int 32.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="startIndex">
        /// The start index.
        /// </param>
        /// <param name="endIndex">
        /// The end index.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int ConvertBytesToInt32(byte[] buffer, int startIndex, int endIndex)
        {
            int value = 0;
            for (int index = startIndex; index <= endIndex; index++)
            {
                value += buffer[index] << ((endIndex - index) * 8);
            }

            return value;
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        private void Initialize()
        {
            this.dataConverters.Add("UINT8", this.ConvertByteArrayToUint8);
            this.dataConverters.Add("UINT8_ARRAY", this.ConvertUint8ArrayToString);
            this.dataConverters.Add("USHORT16", this.ConvertByteArraytoUShort16);
            this.dataConverters.Add("USHORT16_ARRAY", this.ConvertUshort16ArrayToString);
            this.dataConverters.Add("UINT32", this.ConvertByteArrayToUint32);
            this.dataConverters.Add("UINT32_ARRAY", this.ConvertUint32ArrayToString);
            
            this.dataConverters.Add("ENUM", this.ConvertByteArrayToUint8);
            this.dataConverters.Add("CHAR_ARRAY", this.ConvertCharArraytoString);
            this.dataConverters.Add("DateTime", this.ConvertByteArrayToDateTime);

            // PL有符号解析
            this.dataConverters.Add("INT8", this.ConvertByteArrayToInt8);
            this.dataConverters.Add("INT8_ARRAY", this.ConvertInt8ArrayToString);
            this.dataConverters.Add("SHORT16", this.ConvertByteArrayToShort16);
            this.dataConverters.Add("SHORT16_ARRAY", this.ConvertShort16ArrayToString);
            this.dataConverters.Add("INT32_ARRAY", this.CovertInt32ArrayToString);
            this.dataConverters.Add("INT32", this.ConvertByteArrayToInt32);
        }

        /// <summary>
        /// The convert byte array to unsigned integer32 value.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buffer.
        /// </param>
        /// <returns>
        /// The data type object<see cref="object"/>.
        /// </returns>
        private object ConvertByteArrayToUint32(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            var dataBufferToReverse = new byte[4];
            Array.Copy(rawDataBuf, dataBufferToReverse, Math.Min(rawDataBuf.Length, dataBufferToReverse.Length));

            /*原始日志中的数据为小端序，不需要进行转换*/
            try
            {
               // Array.Reverse(dataBufferToReverse);
                uint int32Data = BitConverter.ToUInt32(dataBufferToReverse, 0);
                return int32Data;
            }
            catch (Exception ex)
            {
                Log.Error("ConvertByteArrayToUint32 error,message = " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The convert byte array to unsigned short 16.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buffer.
        /// </param>
        /// <returns>
        /// The unsigned short object<see cref="object"/>.
        /// </returns>
        private object ConvertByteArraytoUShort16(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            var dataBufferToReverse = new byte[2];
            Array.Copy(rawDataBuf, dataBufferToReverse, Math.Min(rawDataBuf.Length, dataBufferToReverse.Length));

            /*原始日志中的数据为小端序，不需要进行转换*/
            try
            {
                //Array.Reverse(dataBufferToReverse);
                ushort int16Data = BitConverter.ToUInt16(dataBufferToReverse, 0);
                return int16Data;
            }
            catch (Exception ex)
            {
                Log.Error("ConvertByteArrayToUShort16 error,message = " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The convert byte array to unsigned 8bits integer.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buffer.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private object ConvertByteArrayToUint8(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            byte uint8 = rawDataBuf[0];

            return uint8;
        }

        /// <summary>
        /// The convert char array to string.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buffer.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private object ConvertCharArraytoString(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            try
            {
                int endPosition = 0;
                int bufferLength = rawDataBuf.Length;

                while ((endPosition < bufferLength) && (0 != rawDataBuf[endPosition]))
                {
                    endPosition++;
                }

                string bufferString = Encoding.ASCII.GetString(rawDataBuf, 0, endPosition);

                return bufferString;
            }
            catch (Exception ex)
            {
                Log.Error("ConvertCharArraytoString error,message = " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The convert unsigned  8bits integer array to string.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buffer.
        /// </param>
        /// <returns>
        /// The string<see cref="object"/>.
        /// </returns>
        private object ConvertUint8ArrayToString(byte[] rawDataBuf)
        {
            /*UINT8_ARRAY 显示补0 将50032变为50 0 32 */

            if (rawDataBuf == null)
            {
                return null;
            }

            try
            {
                var sb = new StringBuilder();
                foreach (byte b in rawDataBuf)
                {
                    string singleByte = string.Format(" {0}", b);
                    sb.Append(singleByte);
                }

                return sb.ToString().TrimStart();
            }
            catch (Exception ex)
            {
                Log.Error("ConvertUint8ArrayToString error,message = " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The convert unsigned short  array to string.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buffer.
        /// </param>
        /// <returns>
        /// The description string of the array<see cref="object"/>.
        /// </returns>
        private object ConvertUshort16ArrayToString(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            /*原始日志中的数据为小端序，不需要进行转换*/
            try
            {
                var strBuild = new StringBuilder();
                for (int i = 0; i < rawDataBuf.Length; i = i + 2)
                {
                    var uint16Buffer = new byte[2];
                    uint16Buffer[0] = rawDataBuf[i];
                    if (i + 1 < rawDataBuf.Length)
                    {
                        uint16Buffer[1] = rawDataBuf[i + 1];
                    }

                    //Array.Reverse(uint16Buffer);
                    ushort uint16Data = BitConverter.ToUInt16(uint16Buffer, 0);
                    string uint16String = string.Format(" {0}", uint16Data);
                    strBuild.Append(uint16String);
                }

                return strBuild.ToString().TrimStart();
            }
            catch (Exception ex)
            {
                Log.Error("ConvertUshort16ArrayToString error,message = " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The convert unsigned integer 32 array to string.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buffer.
        /// </param>
        /// <returns>
        /// The unsigned integer object<see cref="object"/>.
        /// </returns>
        private object ConvertUint32ArrayToString(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            /*原始日志中的数据为小端序，不需要进行转换*/
            try
            {
                var strBuild = new StringBuilder();
                var uint32Buffer = new byte[4];
                int uint32BufferIndex = 0;
                foreach (byte t in rawDataBuf)
                {
                    uint32Buffer[uint32BufferIndex++] = t;
                    if (4 == uint32BufferIndex)
                    {
                       // Array.Reverse(uint32Buffer);
                        uint uint32Data = BitConverter.ToUInt32(uint32Buffer, 0);
                        string uint32String = string.Format(" {0}", uint32Data);
                        strBuild.Append(uint32String);
                        uint32BufferIndex = 0;
                        uint32Buffer = new byte[4];
                    }
                }

                if (0 != uint32BufferIndex)
                {
                    //Array.Reverse(uint32Buffer);
                    uint uint32Data = BitConverter.ToUInt32(uint32Buffer, 0);
                    string uint32String = string.Format(" {0}", uint32Data);
                    strBuild.Append(uint32String);
                }

                return strBuild.ToString().TrimStart();
            }
            catch (Exception ex)
            {
                Log.Error("ConvertUint32ArrayToString error,message = " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The convert byte array to date time.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buffer.
        /// </param>
        /// <returns>
        /// The data time object<see cref="object"/>.
        /// </returns>
        private object ConvertByteArrayToDateTime(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            var seconds = (uint)this.ConvertByteArrayToUint32(rawDataBuf);
            /*文件里记得是从1970.01.01.0.0.0到现在的秒数*/
            var utcOriginalTime = new DateTime(1970, 1, 1, 0, 0, 0);
            var currentTime = utcOriginalTime.AddSeconds(seconds).ToLocalTime();
            string strResult = string.Empty;
            try
            {
                strResult = currentTime.ToString(@"yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                Log.Info(ex.Message);   
            }
            return strResult;
        }

        /// <summary>
        /// The convert byte array to int 8.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buf.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private object ConvertByteArrayToInt8(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            try
            {
                int int8 = (sbyte)rawDataBuf[0]; 
                return int8;
            }
            catch (Exception ex)
            {
                Log.Error("ConvertByteArraytoInt8 error,message = " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The convert int 8 array to string.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buf.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private object ConvertInt8ArrayToString(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            try
            {
                var sb = new StringBuilder();
                foreach (byte b in rawDataBuf)
                {
                    int int8 = (sbyte)b; 
                    string singleByte = string.Format(" {0}", int8);
                    sb.Append(singleByte);
                }

                return sb.ToString().TrimStart();
            }
            catch (Exception ex)
            {
                Log.Error("ConvertUint8ArrayToString error,message = " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The convert byte array to short 16.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buffer.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private object ConvertByteArrayToShort16(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            /*原始日志中的数据为小端序，不需要进行转换*/
            try
            {
                var shortTypeBuffer = new byte[2];
                shortTypeBuffer[0] = rawDataBuf[0];
                shortTypeBuffer[1] = rawDataBuf[1];
                //Array.Reverse(shortTypeBuffer);
                short int16Data = BitConverter.ToInt16(shortTypeBuffer, 0);
                return string.Format(" {0}", int16Data);
            }
            catch (Exception ex)
            {
                Log.Error("ConvertByteArrayToShort16 error,message = " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The convert short 16 array to string.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buffer.
        /// </param>
        /// <returns>
        /// The short array string <see cref="object"/>.
        /// </returns>
        private object ConvertShort16ArrayToString(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            /*原始日志中的数据为小端序，不需要进行转换*/
            try
            {
                var strBuild = new StringBuilder();
                for (int i = 0; i < rawDataBuf.Length; i = i + 2)
                {
                    var shortTypeBytes = new byte[2];
                    shortTypeBytes[0] = rawDataBuf[i];
                    if (i + 1 < rawDataBuf.Length)
                    {
                        shortTypeBytes[1] = rawDataBuf[i + 1];
                    }

                    //Array.Reverse(shortTypeBytes);
                    short int16Data = BitConverter.ToInt16(shortTypeBytes, 0);
                    string int16String = string.Format(" {0}", int16Data);
                    strBuild.Append(int16String);
                }

                return strBuild.ToString().TrimStart();
            }
            catch (Exception ex)
            {
                Log.Error("ConvertShort16ArrayToString error,message = " + ex.Message);
                return null;
            }
        }

        private object CovertInt32ArrayToString(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }
            try
            {
                var strBuild = new StringBuilder();
                var intTypeBytes = new byte[4];
                int int32BufferIndex = 0;
                foreach (byte t in rawDataBuf)
                {
                    intTypeBytes[int32BufferIndex++] = t;
                    if (4 == int32BufferIndex)
                    {                       
                        int int32Data = BitConverter.ToInt32(intTypeBytes, 0);
                        string int32string = string.Format("{0} ", int32Data);
                        strBuild.Append(int32string);
                        int32BufferIndex = 0;
                        intTypeBytes = new byte[4];
                    }
                }
                if (0 != int32BufferIndex)
                {                   
                    int int32Data = BitConverter.ToInt32(intTypeBytes, 0);
                    string int32string = string.Format("{0} ", int32Data);
                    strBuild.Append(int32string);
                }
                return strBuild.ToString().TrimStart();

            }
            catch (System.Exception ex)
            {
                Log.Error("CovertInt32ArrayToString error,message = " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// The convert byte array to int 32.
        /// </summary>
        /// <param name="rawDataBuf">
        /// The raw data buf.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        private object ConvertByteArrayToInt32(byte[] rawDataBuf)
        {
            if (rawDataBuf == null)
            {
                return null;
            }

            /*原始日志中的数据为小端序，不需要进行转换*/
            try
            {
                var intTypeBuffer = new byte[4];
                intTypeBuffer[0] = rawDataBuf[0];
                intTypeBuffer[1] = rawDataBuf[1];
                intTypeBuffer[2] = rawDataBuf[2];
                intTypeBuffer[3] = rawDataBuf[3];
                //Array.Reverse(intTypeBuffer);
                int int32Data = BitConverter.ToInt32(intTypeBuffer, 0);
                return string.Format(" {0}", int32Data);
            }
            catch (Exception ex)
            {
                Log.Error("ConvertByteArrayToInt32 error,message = " + ex.Message);
                return null;
            }
        }
    }
}
