// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WaittingControl.xaml.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   WaittingControl.xaml 的交互逻辑
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomUI
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    /// <summary>
    /// WaittingControl.xaml 的交互逻辑
    /// </summary>
    public partial class WaitingControl
    {
        /// <summary>
        /// The animation timer.
        /// </summary>
        private readonly DispatcherTimer animationTimer;

        /// <summary>
        /// Initializes a new instance of the <see cref="WaitingControl"/> class. 
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public WaitingControl()
        {
            InitializeComponent();
            animationTimer = new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher)
                {
                    Interval = new TimeSpan(0, 0, 0, 0, 75) 
                };
        }

        /// <summary>
        /// The start.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void Start()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.animationTimer.Tick += this.HandleAnimationTick;
            this.animationTimer.Start();
        }

        /// <summary>
        /// The stop.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void Stop()
        {
            this.animationTimer.Stop();
            Mouse.OverrideCursor = null;
            this.animationTimer.Tick -= HandleAnimationTick;
        }

        /// <summary>
        /// The handle animation tick.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void HandleAnimationTick(object sender, EventArgs e)
        {
            SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
        }

        /// <summary>
        /// The handle loaded.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            const double Offset = Math.PI;
            const double Step = Math.PI * 2 / 10.0;

            this.SetPosition(C0, Offset, 0.0, Step);
            this.SetPosition(C1, Offset, 1.0, Step);
            this.SetPosition(C2, Offset, 2.0, Step);
            this.SetPosition(C3, Offset, 3.0, Step);
            this.SetPosition(C4, Offset, 4.0, Step);
            this.SetPosition(C5, Offset, 5.0, Step);
            this.SetPosition(C6, Offset, 6.0, Step);
            this.SetPosition(C7, Offset, 7.0, Step);
            this.SetPosition(C8, Offset, 8.0, Step);
        }

        /// <summary>
        /// The set position.
        /// </summary>
        /// <param name="ellipse">
        /// The ellipse.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <param name="posOffSet">
        /// The position off set.
        /// </param>
        /// <param name="step">
        /// The step.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1407:ArithmeticExpressionsMustDeclarePrecedence", Justification = "Reviewed. Suppression is OK here.")]
        private void SetPosition(Ellipse ellipse, double offset, double posOffSet, double step)
        {
            ellipse.SetValue(Canvas.LeftProperty, viewBox.Width + Math.Sin(offset + posOffSet * step) * viewBox.Width);

            ellipse.SetValue(Canvas.TopProperty, viewBox.Width + Math.Cos(offset + posOffSet * step) * viewBox.Width);
        }

        /// <summary>
        /// The handle unloaded.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// The handle visible changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        private void HandleVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var isVisible = (bool)e.NewValue;

            if (isVisible)
            {
                this.Start();
            }
            else
            {
                this.Stop();
            }
        }
    }
}
