// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZipHelper.cs" company="DatangMobile">
//   DatangMobile
// </copyright>
// <summary>
//   The ZipHelper.
//      --压缩帮助类
// ===============================================================================
// Copyright ? Datang Mobile Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// Author:  pengqiang
// History: 
//   2012/9/19 created by pengqiang
// ===============================================================================
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// The zip helper.
    /// </summary>
    public static class ZipHelper
    {
        /// <summary>
        /// The unpack zip package.
        /// </summary>
        /// <param name="strSrcPath">
        /// The str src path.
        /// </param>
        /// <param name="strDestPath">
        /// The str dest path.
        /// </param>
        /// <param name="pFileNum">
        /// The p file num.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        [DllImport("LmtBZipUtil.dll", EntryPoint = "UnpackZipPackage", CallingConvention = CallingConvention.Cdecl)]
        public static extern int UnpackZipPackage(string strSrcPath, string strDestPath, ref int pFileNum);
    }
}
