// ---------------------------------------------------------------------------
// <copyright file="IoSupport.cs" company="Tethys">
//   Copyright (C) 2018-2021 T. Graf
// </copyright>
//
// Licensed under the Apache License, Version 2.0.
// SPDX-License-Identifier: Apache-2.0
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied.
// ---------------------------------------------------------------------------

namespace Tethys.SPDX.Support
{
    using System.IO;
    using System.Text;

    /// <summary>
    /// IO support methods.
    /// </summary>
    public static class IoSupport
    {
        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's <c>endianness</c> fails.
        /// </summary>
        /// <param name="filename">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            } // using

            if (bom.Length < 2)
            {
                return Encoding.Default;
            } // if

            if (bom[0] == 0xff && bom[1] == 0xfe)
            {
                return Encoding.Unicode; // UTF-16LE
            } // if

            if (bom[0] == 0xfe && bom[1] == 0xff)
            {
                return Encoding.BigEndianUnicode; // UTF-16BE
            } // if

            if (bom.Length < 2)
            {
                return Encoding.Default;
            } // if

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76)
            {
                return Encoding.UTF7;
            } // if

            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            {
                return Encoding.UTF8;
            } // if

            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff)
            {
                return Encoding.UTF32;
            } // if

            return Encoding.ASCII;
        } // GetEncoding()
    } // Support
}
