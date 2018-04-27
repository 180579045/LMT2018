// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDialogService.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the IDialogService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomUI
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The DialogService interface.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Shows the open file dialog box that allows a user to specify a file that should be opened.
        /// </summary>
        /// <param name="fileTypes">
        /// The supported file types.
        /// </param>
        /// <param name="defaultFileType">
        /// Default file type.
        /// </param>
        /// <param name="defaultFileName">
        /// Default filename. The directory name is used as initial directory when it is specified.
        /// </param>
        /// <returns>
        /// A FileDialogResult object which contains the filename selected by the user.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// fileTypes must not be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// fileTypes must contain at least one item.
        /// </exception>
        FileDialogResult ShowOpenFileDialog(IEnumerable<FileType> fileTypes, FileType defaultFileType, string defaultFileName);

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void ShowMessage(string message);

        /// <summary>
        /// Shows the message as warning.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void ShowWarning(string message);

        /// <summary>
        /// Shows the message as error.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        void ShowError(string message);

        /// <summary>
        /// Shows the specified question.
        /// </summary>
        /// <param name="message">
        /// The question.
        /// </param>
        /// <returns>
        /// <c>true</c> for yes, <c>false</c> for no and <c>null</c> for cancel.
        /// </returns>
        bool? ShowQuestion(string message);

        /// <summary>
        /// Shows the specified yes/no question.
        /// </summary>
        /// <param name="message">
        /// The question.
        /// </param>
        /// <returns>
        /// <c>true</c> for yes and <c>false</c> for no.
        /// </returns>
        bool ShowYesNoQuestion(string message);

        /// <summary>
        /// The show waitting window.
        /// </summary>
        /// <param name="strMessage">
        /// The str message.
        /// </param>
        void ShowWaittingWindow(string strMessage);

        /// <summary>
        /// The close waitting window.
        /// </summary>
        void CloseWaittingWindow();
    }
}
