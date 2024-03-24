// ---------------------------------------------------------------------------
// <copyright file="SpdxExceptionInfo.cs" company="Tethys">
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

namespace Tethys.SPDX.KnownLicenses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Tethys.SPDX.Interfaces;

    /// <summary>
    /// Information about a specific license exception known by SPDX.
    /// </summary>
    public class SpdxExceptionInfo : ISpdxExceptionInfo
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets a value indicating whether this is a deprecated license identifier.
        /// </summary>
        [JsonPropertyName("isDeprecatedLicenseId")]
        public bool IsDeprecatedLicenseId { get; set; }

        /// <summary>
        /// Gets or sets the license exception text.
        /// </summary>
        [JsonPropertyName("licenseExceptionText")]
        public string LicenseExceptionText { get; set; }

        /// <summary>
        /// Gets or sets the license exception template.
        /// </summary>
        [JsonPropertyName("standardLicenseTemplate")]
        public string LicenseExceptionTemplate { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the license comments.
        /// </summary>
        [JsonPropertyName("licenseComments")]
        public string LicenseComments { get; set; }

        /// <summary>
        /// Gets or sets the license exception identifier.
        /// </summary>
        [JsonPropertyName("licenseExceptionId")]
        public string LicenseExceptionId { get; set; }

        /// <summary>
        /// Gets or sets the see also data.
        /// </summary>
        [JsonPropertyName("seeAlso")]
        public List<string> SeeAlso { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxExceptionInfo"/> class.
        /// </summary>
        public SpdxExceptionInfo()
        {
            this.IsDeprecatedLicenseId = false;
            this.Name = string.Empty;
        } // SpdxExceptionInfo()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{this.Name}: {this.LicenseExceptionId}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxExceptionInfo
}
