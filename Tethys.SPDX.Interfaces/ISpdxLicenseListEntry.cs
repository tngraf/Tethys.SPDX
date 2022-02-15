// ---------------------------------------------------------------------------
// <copyright file="ISpdxLicenseListEntry.cs" company="Tethys">
//   Copyright (C) 2019-2022 T. Graf
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
    /// The interface for SPDX license list entries.
    /// </summary>
    public interface ISpdxLicenseListEntry
    {
        /// <summary>
        /// Gets or sets the reference file.
        /// </summary>
        string ReferenceFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a deprecated license identifier.
        /// </summary>
        bool IsDeprecatedLicenseId { get; set; }

        /// <summary>
        /// Gets or sets the details Url.
        /// </summary>
        string DetailsUrl { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the license identifier.
        /// </summary>
        string LicenseId { get; set; }

        /// <summary>
        /// Gets or sets the see also data.
        /// </summary>
        List<string> SeeAlso { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is OSI approved.
        /// </summary>
        bool IsOsiApproved { get; set; }
    }
}