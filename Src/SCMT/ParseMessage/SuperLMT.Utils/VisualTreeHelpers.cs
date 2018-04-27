// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisualTreeHelpers.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   The visual tree helpers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils
{
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// The visual tree helpers.
    /// </summary>
    public class VisualTreeHelpers
    {
        /// <summary>
        /// The find scroll viewer.
        /// </summary>
        /// <param name="myVisual">
        /// The my visual.
        /// </param>
        /// <returns>
        /// The <see cref="ScrollViewer"/>.
        /// </returns>
        public static ScrollViewer FindScrollViewer(Visual myVisual)
        {
            if (myVisual != null)
            {
                if (myVisual is ScrollViewer)
                {
                    return myVisual as ScrollViewer;
                }
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(myVisual); i++)
            {
                // Retrieve child visual at specified index value.
                var childVisual = (Visual)VisualTreeHelper.GetChild(myVisual, i);

                // Do processing of the child visual object.

                // Enumerate children of the child visual object.
                var scrollviewer = FindScrollViewer(childVisual);
                if (null != scrollviewer)
                {
                    return scrollviewer;
                }
            }

            return null;
        }
    }
}
