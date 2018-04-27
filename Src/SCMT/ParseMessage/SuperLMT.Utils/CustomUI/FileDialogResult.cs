// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileDialogResult.cs" company="DatangMoblie">
//   DatangMoblie
// </copyright>
// <summary>
//   Defines the FileDialogResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomUI
{
    using System.Text;

    /// <summary>
    /// The file dialog result.
    /// </summary>
    public class FileDialogResult
    {
        /// <summary>
        /// The _file name.
        /// </summary>
        private readonly string fileName;

        /// <summary>
        /// The _selected file type.
        /// </summary>
        private readonly FileType selectedFileType;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDialogResult"/> class with null values.
        /// Use this constructor when the user canceled the file dialog box.
        /// </summary>
        public FileDialogResult()
            : this(string.Empty, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDialogResult"/> class.
        /// </summary>
        /// <param name="fileName">The filename entered by the user.</param>
        /// <param name="selectedFileType">The file type selected by the user.</param>
        public FileDialogResult(string fileName, FileType selectedFileType)
        {
            this.fileName = fileName;
            this.selectedFileType = selectedFileType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDialogResult"/> class. 
        /// </summary>
        /// <param name="fileNames">
        /// The multi filenames selected by the user
        /// </param>
        /// <param name="selectedFileType">
        /// The file type selected by the user.
        /// </param>
        public FileDialogResult(string[] fileNames, FileType selectedFileType)
        {
            if (null != fileNames)
            {
                if (fileNames.Length >= 1)
                {
                    var strBuild = new StringBuilder(fileNames[0]);
                    for (int nCurIndex = 1; nCurIndex < fileNames.Length; nCurIndex++)
                    {
                        strBuild.AppendFormat(@"|{0}", fileNames[nCurIndex]);
                    }

                    this.fileName = strBuild.ToString();
                }
            }

            this.selectedFileType = selectedFileType;
        }

        /// <summary>
        /// Gets a value indicating whether this instance contains valid data. This property returns <c>false</c>
        /// when the user canceled the file dialog box.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.FileName != null && this.SelectedFileType != null;
            }
        }

        /// <summary>
        /// Gets the filename entered by the user or <c>null</c> when the user canceled the dialog box.
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }

        /// <summary>
        /// Gets the file type selected by the user or <c>null</c> when the user canceled the dialog box.
        /// </summary>
        public FileType SelectedFileType
        {
            get
            {
                return this.selectedFileType;
            }
        }
    }
}
