// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RadioBoolToStringConverter.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the RadioBoolToIntConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomConverter
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// The radio bool to int converter.
    /// </summary>
    public class RadioBoolToStringConverter : IValueConverter
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
            var parameterValue = System.Convert.ToString(value);
            if (parameterValue == System.Convert.ToString(parameter))
            {
                return true;
            }

            return false;
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
            return parameter;
        }
    }
}
