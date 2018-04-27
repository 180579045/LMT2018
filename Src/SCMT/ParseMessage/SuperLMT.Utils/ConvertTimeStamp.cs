// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConvertTimeStamp.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the ConvertTimeStamp type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils
{
    using System;

    /// <summary>
    /// The convert time stamp.
    /// </summary>
    public class ConvertTimeStamp
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly ConvertTimeStamp Instance = new ConvertTimeStamp();

        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static ConvertTimeStamp Singleton
        {
            get
            {
                return Instance;
            }
        }

        /// <summary>
        /// The convert time stamp to uint.
        /// this function is used for convert time format to integer format
        /// </summary>
        /// <param name="timeStamp">
        /// The time stamp.
        /// </param>
        /// <param name="addSeconds">
        /// The add Seconds.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        public ulong ConvertTimeStampToUlong(string timeStamp, int addSeconds)
        {
            // start from 1.1.1
            var originalTime = new DateTime(1, 1, 1, 0, 0, 0);
            var tickTime = DateTime.Parse(timeStamp).AddSeconds(addSeconds) - originalTime;
            var tick = Convert.ToUInt64(tickTime.Ticks);
            return tick;
        }
        public DateTime ConvertDateTimeFromTicks(long ticks)
        {

            DateTime tStart = new DateTime(ticks);
        
            return tStart;

        }
    }
}
