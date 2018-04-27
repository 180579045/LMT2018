// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumToBoolConverter.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   The enum to bool converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomConverter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// The enum to bool converter.
    /// </summary>
    public class EnumToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets the enum value.
        /// </summary>
        public string EnumValue { get; set; }

        /// <summary>
        /// The convert.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() == this.EnumValue;
        }

        /// <summary>
        /// The convert back.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="targetType">
        /// The target type.
        /// </param>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
