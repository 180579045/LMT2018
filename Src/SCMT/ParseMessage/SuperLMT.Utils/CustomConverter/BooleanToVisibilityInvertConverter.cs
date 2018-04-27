// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BooleanToVisibilityInvertConverter.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   The boolean to visibility invert converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomConverter
{
    using System.Windows;

    /// <summary>
    /// The boolean to visibility invert converter.
    /// </summary>
    public class BooleanToVisibilityInvertConverter : BooleanConverter<Visibility>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanToVisibilityInvertConverter"/> class.
        /// </summary>
        public BooleanToVisibilityInvertConverter()
            : base(Visibility.Collapsed, Visibility.Visible)
        {
        }
    }
}
