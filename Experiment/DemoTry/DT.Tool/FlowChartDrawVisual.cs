// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlowChartDrawVisual.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the FlowChartDrawVisual type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DT.Tools.FlowChart
{
    using System.Windows.Media;
    using Point = System.Windows.Point;

    /// <summary>
    /// The flow chart draw visual.
    /// </summary>
    public class FlowChartDrawVisual : DrawingVisual
    {
        /// <summary>
        /// Gets or sets the oid.
        /// </summary>
        public int OID { get; set; }

        /// <summary>
        /// Gets or sets the parser id.
        /// </summary>
        public int ParserId { get; set; }

        /// <summary>
        /// Gets or sets the start point.
        /// </summary>
        public Point StartPoint { get; set; }

        /// <summary>
        /// Gets or sets the stop point.
        /// </summary>
        public Point StopPoint { get; set; }
    }
}
