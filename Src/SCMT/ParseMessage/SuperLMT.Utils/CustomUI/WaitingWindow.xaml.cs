// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WaitingWindow.xaml.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the WaittingWindow type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomUI
{
    using System.Threading;
    using System.Windows.Threading;

    /// <summary>
    /// The waiting window.
    /// </summary>
    public partial class WaitingWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaitingWindow"/> class.
        /// </summary>
        public WaitingWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The set prompt text.
        /// </summary>
        /// <param name="strPrompt">
        /// The prompt.
        /// </param>
        public void SetPromptText(string strPrompt)
        {
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                (ThreadStart)delegate
                    {
                        if (null != this.labelPrompt)
                        {
                            this.labelPrompt.Text = strPrompt;
                        }
                    });
        }

        /// <summary>
        /// The set progress.
        /// </summary>
        /// <param name="progress">
        /// The progress.
        /// </param>
        public void SetProgress(int progress)
        {
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                (ThreadStart)delegate
                    {
                        if (null != this.labelPrompt)
                        {
                            this.progressBar.Value = progress;
                        }
                    });
        }
    }
}