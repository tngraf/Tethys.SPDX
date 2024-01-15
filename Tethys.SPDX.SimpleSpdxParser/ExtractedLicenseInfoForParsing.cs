// ---------------------------------------------------------------------------
// <copyright file="ExtractedLicenseInfoForParsing.cs" company="Tethys">
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

namespace Tethys.SPDX.SimpleSpdxParser
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using Tethys.SPDX.Model;
    using Tethys.SPDX.Model.License;

    /// <summary>
    /// An ExtractedLicensingInfo represents a License or licensing notice that was found in the package.
    /// Any License text that is recognized as a License may be represented as a License
    /// rather than an ExtractedLicensingInfo.
    /// </summary>
    /// <seealso cref="ExtractedLicenseInfoForParsing" />
    public class ExtractedLicenseInfoForParsing : SimpleLicensingInfo
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the extracted text.
        /// </summary>
        [JsonProperty("extractedText")]
        public string ExtractedText { get; set; }

        /// <summary>
        /// Gets or sets the 'see also' elements.
        /// </summary>
        [JsonProperty("seeAlsos")]
        public new List<string> SeeAlso { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractedLicenseInfoForParsing"/> class.
        /// </summary>
        public ExtractedLicenseInfoForParsing()
        {
            this.ExtractedText = string.Empty;
        } // ExtractedLicenseInfoForParsing()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS

        /// <summary>
        /// Converts this instance to an ExtractedLicenseInfo.
        /// </summary>
        /// <returns>A <see cref="ExtractedLicenseInfo"/> object.</returns>
        public ExtractedLicenseInfo ToExtractedLicenseInfo()
        {
            var result = new ExtractedLicenseInfo();
            result.Name = this.Name;
            result.Id = this.Id;
            result.Comment = this.Comment;
            if (this.SeeAlso != null)
            {
                result.SetSeeAlso(this.SeeAlso);
            } // if

            result.ExtractedText = this.ExtractedText;

            return result;
        } // ToExtractedLicenseInfo()

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
    } // ExtractedLicenseInfoForParsing
}
