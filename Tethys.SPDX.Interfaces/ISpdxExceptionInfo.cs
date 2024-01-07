// ---------------------------------------------------------------------------
// <copyright file="ISpdxExceptionInfo.cs" company="Tethys">
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

namespace Tethys.SPDX.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for SPDX exception information.
    /// </summary>
    public interface ISpdxExceptionInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether this is a deprecated license identifier.
        /// </summary>
        bool IsDeprecatedLicenseId { get; set; }

        /// <summary>
        /// Gets or sets the license exception text.
        /// </summary>
        string LicenseExceptionText { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the license comments.
        /// </summary>
        string LicenseComments { get; set; }

        /// <summary>
        /// Gets or sets the license exception identifier.
        /// </summary>
        string LicenseExceptionId { get; set; }

        /// <summary>
        /// Gets or sets the license exception template.
        /// </summary>
        string LicenseExceptionTemplate { get; set; }

        /// <summary>
        /// Gets or sets the see also data.
        /// </summary>
        List<string> SeeAlso { get; set; }
    }
}