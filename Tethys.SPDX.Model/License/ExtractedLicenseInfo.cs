// ---------------------------------------------------------------------------
// <copyright file="ExtractedLicenseInfo.cs" company="Tethys">
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
    /// An ExtractedLicensingInfo represents a License or licensing notice that was found in the package.
    /// Any License text that is recognized as a License may be represented as a License
    /// rather than an ExtractedLicensingInfo.
    /// </summary>
    /// <seealso cref="SimpleLicensingInfo" />
    public class ExtractedLicenseInfo : SimpleLicensingInfo
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the extracted text.
        /// </summary>
        [JsonProperty("extractedText")]
        public string ExtractedText { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractedLicenseInfo"/> class.
        /// </summary>
        public ExtractedLicenseInfo()
        {
            this.ExtractedText = string.Empty;
        } // ExtractedLicenseInfo()
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
            return this.Id;
        } // ToString()
        #endregion // PUBLIC METHODS
    } // ExtractedLicenseInfo
}
