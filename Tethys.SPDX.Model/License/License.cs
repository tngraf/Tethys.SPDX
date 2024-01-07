// ---------------------------------------------------------------------------
// <copyright file="License.cs" company="Tethys">
//   Copyright (C) 2018-2024 T. Graf
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

namespace Tethys.SPDX.Model.License
{
    using Newtonsoft.Json;

    /// <summary>
    /// Describes a License.
    /// <p/>
    /// All licenses have an ID.
    /// Subclasses should extend this class to add additional properties.
    /// </summary>
    /// <seealso cref="SimpleLicensingInfo" />
    public class License : SimpleLicensingInfo
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the standard license header.
        /// </summary>
        [JsonProperty("standardLicenseHeader")]
        public string StandardLicenseHeader { get; set; }

        /// <summary>
        /// Gets or sets the standard license header template.
        /// </summary>
        [JsonProperty("standardLicenseHeaderTemplate")]
        public string StandardLicenseHeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the standard license template.
        /// </summary>
        [JsonProperty("standardLicenseTemplate")]
        public string StandardLicenseTemplate { get; set; }

        /// <summary>
        /// Gets or sets the license text.
        /// </summary>
        [JsonProperty("licenseText")]
        public string LicenseText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is OSI approved
        /// (an approved license on the OSI website).
        /// </summary>
        [JsonProperty("isOsiApproved")]
        public bool IsOsiApproved { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether FSF describes the license as free / <c>libre</c>,
        /// false if FSF describes the license as not free / <c>libre</c> or if FSF does not reference the license.
        /// </summary>
        [JsonProperty("isFsfLibre")]
        public bool IsFsfLibre { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="License"/> is deprecated.
        /// </summary>
        [JsonProperty("isDeprecated")]
        public bool IsDeprecated { get; set; }
        #endregion // PUBLIC PROPERTIES

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
            if (this.Id == null)
            {
                return "NULL LICENSE";
            } // if

            return this.Id;
        } // ToString()
        #endregion // PUBLIC METHODS
    } // License
}
