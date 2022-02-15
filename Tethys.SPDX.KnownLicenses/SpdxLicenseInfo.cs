// ---------------------------------------------------------------------------
// <copyright file="SpdxLicenseInfo.cs" company="Tethys">
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
    public class SpdxLicenseInfo : ISpdxLicenseInfo
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets a value indicating whether this is a deprecated license identifier.
        /// </summary>
        [JsonProperty(PropertyName = "isDeprecatedLicenseId")]
        public bool IsDeprecatedLicenseId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this license is <c>FSF libre</c>.
        /// </summary>
        [JsonProperty(PropertyName = "isFsfLibre")]
        public bool IsFsfLibre { get; set; }

        /// <summary>
        /// Gets or sets the license text.
        /// </summary>
        [JsonProperty(PropertyName = "licenseText")]
        public string LicenseText { get; set; }

        /// <summary>
        /// Gets or sets the standard license header template.
        /// </summary>
        [JsonProperty(PropertyName = "standardLicenseHeaderTemplate")]
        public string StandardLicenseHeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the standard license template.
        /// </summary>
        [JsonProperty(PropertyName = "standardLicenseTemplate")]
        public string StandardLicenseTemplate { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the license comments.
        /// </summary>
        [JsonProperty(PropertyName = "licenseComments")]
        public string LicenseComments { get; set; }

        /// <summary>
        /// Gets or sets the license identifier.
        /// </summary>
        [JsonProperty(PropertyName = "licenseId")]
        public string LicenseId { get; set; }

        /// <summary>
        /// Gets or sets the standard license header.
        /// </summary>
        [JsonProperty(PropertyName = "standardLicenseHeader")]
        public string StandardLicenseHeader { get; set; }

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
        /// Initializes a new instance of the <see cref="SpdxLicenseInfo"/> class.
        /// </summary>
        public SpdxLicenseInfo()
        {
            this.IsDeprecatedLicenseId = false;
            this.IsFsfLibre = false;
            this.Name = string.Empty;
        } // SpdxLicenseInfo()
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
    } // SpdxLicenseInfo
}
