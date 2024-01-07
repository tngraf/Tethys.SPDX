// ---------------------------------------------------------------------------
// <copyright file="PrimaryPackagePurpose.cs" company="Tethys">
//   Copyright (C) 2024 T. Graf
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
    /// Enumeration of primary package purposes.
    /// </summary>
    public enum PrimaryPackagePurpose
    {
        /// <summary>
        /// Any other purpose.
        /// </summary>
        Other,

        /// <summary>
        /// An application.
        /// </summary>
        Application,

        /// <summary>
        /// A Framework.
        /// </summary>
        Framework,

        /// <summary>
        /// A library.
        /// </summary>
        Library,

        /// <summary>
        /// A container.
        /// </summary>
        Container,

        /// <summary>
        /// An operating system.
        /// </summary>
        OperatingSystem,

        /// <summary>
        /// A device.
        /// </summary>
        Device,

        /// <summary>
        /// A firmware.
        /// </summary>
        Firmware,

        /// <summary>
        /// A source file /archive.
        /// </summary>
        Source,

        /// <summary>
        /// An archive.
        /// </summary>
        Archive,

        /// <summary>
        /// A file.
        /// </summary>
        File,

        /// <summary>
        /// An installer.
        /// </summary>
        Install,
    } // PrimaryPackagePurpose
}
