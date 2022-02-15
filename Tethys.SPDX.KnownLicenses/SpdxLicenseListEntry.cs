// ---------------------------------------------------------------------------
// <copyright file="SpdxLicenseListEntry.cs" company="Tethys">
//   Copyright (C) 2019 T. Graf
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

    using Newtonsoft.Json;

    using Tethys.SPDX.Interfaces;

    /// <summary>
    /// Information about a specific license known by SPDX.
    /// </summary>
    public class SpdxLicenseListEntry : ISpdxLicenseListEntry
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the reference file.
        /// </summary>
        [JsonProperty(PropertyName = "reference")]
        public string ReferenceFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a deprecated license identifier.
        /// </summary>
        [JsonProperty(PropertyName = "isDeprecatedLicenseId")]
        public bool IsDeprecatedLicenseId { get; set; }

        /// <summary>
        /// Gets or sets the details Url.
        /// </summary>
        [JsonProperty(PropertyName = "detailsUrl")]
        public string DetailsUrl { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the license identifier.
        /// </summary>
        [JsonProperty(PropertyName = "licenseId")]
        public string LicenseId { get; set; }

        /// <summary>
        /// Gets or sets the see also data.
        /// </summary>
        [JsonProperty(PropertyName = "seeAlso")]
        public List<string> SeeAlso { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is OSI approved.
        /// </summary>
        [JsonProperty(PropertyName = "isOsiApproved")]
        public bool IsOsiApproved { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxLicenseListEntry"/> class.
        /// </summary>
        public SpdxLicenseListEntry()
        {
            this.IsDeprecatedLicenseId = false;
            this.Name = string.Empty;
        } // SpdxLicenseListEntry()
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
            return $"{this.Name}: {this.LicenseId}, OSI approved={this.IsOsiApproved}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxLicenseListEntry
}
