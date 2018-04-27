﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileType.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   Defines the FileType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SuperLMT.Utils.CustomUI
{
    using System;

    /// <summary>
    /// The file type.
    /// </summary>
    public class FileType
    {
        /// <summary>
        /// The _description.
        /// </summary>
        private readonly string description;

        /// <summary>
        /// The _file extension.
        /// </summary>
        private readonly string fileExtension;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileType"/> class.
        /// </summary>
        /// <param name="description">The description of the file type.</param>
        /// <param name="fileExtension">The file extension. This string has to start with a '.' point.</param>
        /// <exception cref="ArgumentException">description is null or an empty string.</exception>
        /// <exception cref="ArgumentException">fileExtension is null, an empty string or doesn't start with a '.' point character.</exception>
        public FileType(string description, string fileExtension)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("The argument description must not be null or empty.");
            }

            if (string.IsNullOrEmpty(fileExtension))
            {
                throw new ArgumentException("The argument fileExtension must not be null or empty.");
            }

            if (fileExtension[0] != '.')
            {
                throw new ArgumentException("The argument fileExtension must start with the '.' character.");
            }

            this.description = description;
            this.fileExtension = fileExtension;
        }

        /// <summary>
        /// Gets the description of the file type.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// Gets the file extension. This string starts with a '.' point.
        /// </summary>
        public string FileExtension
        {
            get
            {
                return this.fileExtension;
            }
        }
    }
}
