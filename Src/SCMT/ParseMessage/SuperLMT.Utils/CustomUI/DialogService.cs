// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogService.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the DialogService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomUI
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Windows;

    using Microsoft.Win32;

    using SuperLMT.Utils;

    /// <summary>
    /// The dialog service.
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// The show open file dialog.
        /// Shows the open file dialog box that allows a user to specify a file that should be opened.
        /// </summary>
        /// <param name="fileTypes">
        /// The file types.
        /// The supported file types.
        /// </param>
        /// <param name="defaultFileType">
        /// The default file type.
        /// Default file type.
        /// </param>
        /// <param name="defaultFileName">
        /// The default file name.
        /// Default filename. The directory name is used as initial directory when it is specified.
        /// </param>
        /// <returns>
        /// The A FileDialogResult object which contains the filename selected by the user.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// fileTypes must not be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// fileTypes must contain at least one item
        /// </exception>
        public FileDialogResult ShowOpenFileDialog(IEnumerable<FileType> fileTypes, FileType defaultFileType, string defaultFileName)
        {
            if (fileTypes == null)
            {
                throw new ArgumentNullException("fileTypes");
            }

            if (!fileTypes.Any())
            {
                throw new ArgumentException("The fileTypes collection must contain at least one item.");
            }

            var dialog = new OpenFileDialog { Multiselect = true };

            return ShowFileDialog(Application.Current.MainWindow, dialog, fileTypes, defaultFileType, defaultFileName);
        }

        /// <summary>
        /// The show file dialog.
        /// </summary>
        /// <param name="owner">
        /// The owner.
        /// </param>
        /// <param name="dialog">
        /// The dialog.
        /// </param>
        /// <param name="fileTypes">
        /// The file types.
        /// </param>
        /// <param name="defaultFileType">
        /// The default file type.
        /// </param>
        /// <param name="defaultFileName">
        /// The default file name.
        /// </param>
        /// <returns>
        /// The <see cref="FileDialogResult"/>.
        /// </returns>
        private static FileDialogResult ShowFileDialog(
            object owner,
            FileDialog dialog,
            IEnumerable<FileType> fileTypes,
            FileType defaultFileType,
            string defaultFileName)
        {
            int filterIndex = fileTypes.ToList().IndexOf(defaultFileType);
            if (filterIndex >= 0)
            {
                dialog.FilterIndex = filterIndex + 1;
            }

            if (!string.IsNullOrEmpty(defaultFileName))
            {
                dialog.FileName = Path.GetFileName(defaultFileName);
                string directory = Path.GetDirectoryName(defaultFileName);
                if (!string.IsNullOrEmpty(directory))
                {
                    dialog.InitialDirectory = directory;
                }
            }

            dialog.Filter = CreateFilter(fileTypes);
            if (dialog.ShowDialog(owner as Window) == true)
            {
                filterIndex = dialog.FilterIndex - 1;
                if (filterIndex >= 0 && filterIndex < fileTypes.Count())
                {
                    defaultFileType = fileTypes.ElementAt(filterIndex);
                }
                else
                {
                    defaultFileType = null;
                }

                return new FileDialogResult(dialog.FileNames, defaultFileType);
            }

            return null;
        }

        /// <summary>
        /// The create filter.
        /// </summary>
        /// <param name="fileTypes">
        /// The file types.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string CreateFilter(IEnumerable<FileType> fileTypes)
        {
            string filter = string.Empty;
            foreach (FileType fileType in fileTypes)
            {
                if (!String.IsNullOrEmpty(filter))
                {
                    filter += "|";
                }

                filter += fileType.Description + "|";

                string[] strExtensions = fileType.FileExtension.Split(';');
                foreach (string strCurExt in strExtensions)
                {
                    filter += "*" + strCurExt + ";";
                }

                filter = filter.TrimEnd(';');
            }
            return filter;
        }

        /// <summary>
        /// Gets the message box result.
        /// </summary>
        private static MessageBoxResult MessageBoxResult
        {
            get
            {
                return MessageBoxResult.None;
            }
        }

        /// <summary>
        /// Gets the message box options.
        /// </summary>
        private static MessageBoxOptions MessageBoxOptions
        {
            get
            {
                return CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft ? MessageBoxOptions.RtlReading : MessageBoxOptions.None;
            }
        }

        /// <summary>
        /// The show message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void ShowMessage(string message)
        {
            Window ownerWindow = Application.Current.MainWindow;
            if (ownerWindow != null)
            {
                MessageBox.Show(
                    ownerWindow,
                    message,
                    ApplicationInfo.ProductName,
                    MessageBoxButton.OK,
                    MessageBoxImage.None,
                    MessageBoxResult,
                    MessageBoxOptions);
            }
            else
            {
                MessageBox.Show(
                    message,
                    ApplicationInfo.ProductName,
                    MessageBoxButton.OK,
                    MessageBoxImage.None,
                    MessageBoxResult,
                    MessageBoxOptions);
            }
        }

        /// <summary>
        /// The show warning.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void ShowWarning(string message)
        {
            Window ownerWindow = Application.Current.MainWindow; 
            if (ownerWindow != null)
            {
                MessageBox.Show(
                    ownerWindow,
                    message,
                    ApplicationInfo.ProductName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning,
                    MessageBoxResult,
                    MessageBoxOptions);
            }
            else
            {
                MessageBox.Show(
                    message,
                    ApplicationInfo.ProductName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning,
                    MessageBoxResult,
                    MessageBoxOptions);
            }
        }

        /// <summary>
        /// The show error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public void ShowError(string message)
        {
            Window ownerWindow = Application.Current.MainWindow; ;
            if (ownerWindow != null)
            {
                MessageBox.Show(
                    ownerWindow,
                    message,
                    ApplicationInfo.ProductName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error,
                    MessageBoxResult,
                    MessageBoxOptions);
            }
            else
            {
                MessageBox.Show(
                    message,
                    ApplicationInfo.ProductName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error,
                    MessageBoxResult,
                    MessageBoxOptions);
            }
        }

        /// <summary>
        /// The show question.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="bool?"/>.
        /// </returns>
        public bool? ShowQuestion(string message)
        {
            Window ownerWindow = Application.Current.MainWindow;
            MessageBoxResult result;
            if (ownerWindow != null)
            {
                result = MessageBox.Show(
                    ownerWindow,
                    message,
                    ApplicationInfo.ProductName,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No,
                    MessageBoxOptions);
            }
            else
            {
                result = MessageBox.Show(
                    message,
                    ApplicationInfo.ProductName,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No,
                    MessageBoxOptions);
            }

            if (result == MessageBoxResult.Yes)
            {
                return true;
            }

            if (result == MessageBoxResult.No)
            {
                return false;
            }

            return null;
        }

        /// <summary>
        /// The show yes no question.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool ShowYesNoQuestion(string message)
        {
            Window ownerWindow = Application.Current.MainWindow;
            MessageBoxResult result;
            if (ownerWindow != null)
            {
                result = MessageBox.Show(
                    ownerWindow,
                    message,
                    ApplicationInfo.ProductName,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No,
                    MessageBoxOptions);
            }
            else
            {
                result = MessageBox.Show(
                    message,
                    ApplicationInfo.ProductName,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question,
                    MessageBoxResult.No,
                    MessageBoxOptions);
            }

            return result == MessageBoxResult.Yes;
        }

        /// <summary>
        /// The show waitting window.
        /// </summary>
        /// <param name="strMessage">
        /// The str message.
        /// </param>
        public void ShowWaittingWindow(string strMessage)
        {
            //if (needWaiting)
            //{
               // WaittingWndMgr.Instance.ShowWaittingWnd(strMessage);
            //}
            //else
            //{
            //    WaittingWndMgr.Instance.ShowNoWaittingWnd(strMessage);
            //}
            
        }

        /// <summary>
        /// The close waitting window.
        /// </summary>
        public void CloseWaittingWindow()
        {
            //if (needWaiting)
            //{
              //  WaittingWndMgr.Instance.CloseWaittingWnd();
            //}
            //else
            //{
            //    WaittingWndMgr.Instance.CloseNoWaittingWnd();
            //}
        }
    }
}
