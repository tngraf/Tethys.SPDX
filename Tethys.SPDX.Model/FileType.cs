// ---------------------------------------------------------------------------
// <copyright file="FileType.cs" company="Tethys">
//   Copyright (C) 2018 T. Graf
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

namespace Tethys.SPDX.Model
{
    /// <summary>
    /// File type enumeration.
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// An application.
        /// </summary>
        Application,

        /// <summary>
        /// An archive.
        /// </summary>
        Archive,

        /// <summary>
        /// An audio file.
        /// </summary>
        Audio,

        /// <summary>
        /// A binary file.
        /// </summary>
        Binary,

        /// <summary>
        /// A documentation file.
        /// </summary>
        Documentation,

        /// <summary>
        /// An image file.
        /// </summary>
        Image,

        /// <summary>
        /// Some other file.
        /// </summary>
        Other,

        /// <summary>
        /// A source code file.
        /// </summary>
        Source,

        /// <summary>
        /// A SPDX  file.
        /// </summary>
        Spdx,

        /// <summary>
        /// A text file.
        /// </summary>
        Text,

        /// <summary>
        /// A video file.
        /// </summary>
        Video,
    } // FileType
}
