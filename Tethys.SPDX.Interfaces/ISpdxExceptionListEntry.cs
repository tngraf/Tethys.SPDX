// ---------------------------------------------------------------------------
// <copyright file="ISpdxExceptionListEntry.cs" company="Tethys">
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
    using System.Text.Json.Serialization;

    /// <summary>
    /// The interface for SPDX exception list entries.
    /// </summary>
    public interface ISpdxExceptionListEntry
    {
        /// <summary>
        /// Gets or sets the reference file.
        /// </summary>
        [JsonPropertyName("reference")] // required for System.Text.Json!
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
        /// Gets or sets the reference number.
        /// </summary>
        int ReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the license exception identifier.
        /// </summary>
        string LicenseExceptionId { get; set; }

        /// <summary>
        /// Gets or sets the see also data.
        /// </summary>
        List<string> SeeAlso { get; set; }
    }
}