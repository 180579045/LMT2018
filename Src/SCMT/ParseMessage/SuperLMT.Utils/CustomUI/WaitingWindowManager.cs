// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WaitingWindowManager.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the WaitingWindowManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomUI
{
    using System;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// The waiting window manager.
    /// </summary>
    public class WaitingWindowManager : DispatcherObject
    {
        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly WaitingWindowManager Instance = new WaitingWindowManager();

        /// <summary>
        /// The waiting window.
        /// </summary>
        private WaitingWindow waitingWindow;

        /// <summary>
        /// The begin time.
        /// </summary>
        private DateTime beginTime;
        
        /// <summary>
        /// Gets the singleton.
        /// </summary>
        public static WaitingWindowManager Singleton
        {
            get
            {
                return Instance;
            }
        }

        /// <summary>
        /// The show waiting window.
        /// </summary>
        public void ShowWaitingWindow()
        {
            this.beginTime = DateTime.Now;
            this.waitingWindow = new WaitingWindow { Owner = Application.Current.MainWindow };
            this.waitingWindow.ShowDialog();
        }

        /// <summary>
        /// The close waiting window.
        /// </summary>
        public void CloseWindow()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            
            this.Dispatcher.BeginInvoke(
                DispatcherPriority.Normal,
                (ThreadStart)delegate
                    {
                        if (null != this.waitingWindow)
                        {
                            this.waitingWindow.Close();
                            this.waitingWindow = null;
                        }
                    });
        }

        /// <summary>
        /// The update progress.
        /// </summary>
        /// <param name="progress">
        /// The progress.
        /// </param>
        public void UpdateProgress(int progress)
        {
            if (null != this.waitingWindow)
            {
                this.waitingWindow.SetProgress(progress);
            }
        }

        /// <summary>
        /// The update prompt info.
        /// </summary>
        /// <param name="promptInfo">
        /// The prompt info.
        /// </param>
        public void UpdatePromptInfo(string promptInfo)
        {
            if (null != this.waitingWindow)
            {
                this.waitingWindow.SetPromptText(promptInfo);
            }
        }

        /// <summary>
        /// The get duration.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDuration()
        {
            var currentTime = DateTime.Now;
            var durationTime = currentTime - this.beginTime;
            return durationTime.TotalMilliseconds.ToString("0.0");
        }
    }
}
