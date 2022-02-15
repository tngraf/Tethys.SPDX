// ---------------------------------------------------------------------------
// <copyright file="SpdxLicenseListInfo.cs" company="Tethys">
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
    public class SpdxLicenseListInfo : SpdxLicenseListInfoBase, ISpdxLicenseListInfo
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The licenses.
        /// </summary>
        private readonly List<SpdxLicenseListEntry> licenses;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets license list.
        /// </summary>
        [JsonProperty(PropertyName = "licenses")]
        public IReadOnlyList<ISpdxLicenseListEntry> Licenses => this.licenses;
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxLicenseListInfo"/> class.
        /// </summary>
        public SpdxLicenseListInfo()
        {
            this.licenses = new List<SpdxLicenseListEntry>();
        } // SpdxLicenseListInfo()
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
            return $"{this.LicenseListVersion}: {this.ReleaseDate}, #={this.Licenses.Count}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxLicenseListInfo
}
