// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumValueToIntegerConverter.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   The enum value to integer converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomConverter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// The enum value to integer converter.
    /// </summary>
    /// <typeparam name="TEnumType">
    /// enum type
    /// </typeparam>
    public class EnumValueToIntegerConverter<TEnumType> : IValueConverter
    {
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
            return (int)value;
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
            return (TEnumType)value;
        }
    }
}
