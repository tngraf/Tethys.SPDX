// ---------------------------------------------------------------------------
// <copyright file="SpdxSnippetForParsing.cs" company="Tethys">
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

namespace Tethys.SPDX.SimpleSpdxParser
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Tethys.SPDX.Model;
    using Tethys.SPDX.Model.License;
    using Tethys.SPDX.Model.Pointer;

    /// <summary>
    /// This is a special variant of <see cref="SpdxSnippet"/> without
    /// read-only properties. This simplifies reading the information
    /// from JSON files.
    /// </summary>
    /// <seealso cref="SpdxItem" />
    internal class SpdxSnippetForParsing : SpdxItem
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the snippet from file.
        /// </summary>
        [JsonProperty("snippetFromFile")]
        [JsonConverter(typeof(SpdxElementRefConverter))]
        public SpdxFile SnippetFromFile { get; set; }

        /// <summary>
        /// Gets or sets the ranges.
        /// </summary>
        [JsonProperty("ranges")]
        public List<StartEndPointer> Ranges { get; set; }

        /// <summary>
        /// Gets or sets the license information in this snippet.
        /// </summary>
        [JsonProperty("licenseInfoInSnippets")]
        [JsonConverter(typeof(JsonLicenseListConverter))]
        public List<AnyLicenseInfo> LicenseInfoInSnippet { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Creates the final SPDX snippet.
        /// </summary>
        /// <returns>A <see cref="SpdxSnippet"/>.</returns>
        public SpdxSnippet ToSpdxSnippet()
        {
            var result = new SpdxSnippet();

            // SpdxElement properties
            result.SpdxIdentifier = this.SpdxIdentifier;
            result.Comment = this.Comment;
            result.Name = this.Name;
            if (this.Annotations != null)
            {
                result.SetAnnotations(this.Annotations);
            } // if

            // SpdxItem properties
            result.LicenseConcluded = this.LicenseConcluded;
            result.CopyrightText = this.CopyrightText;
            result.Comment = this.Comment;
            result.AttributionText = this.AttributionText;
            result.LicenseComments = this.LicenseComments;

            // SpdxSnippet properties
            result.SnippetFromFile = this.SnippetFromFile;
            result.SetLicenseInfoInSnippet(this.LicenseInfoInSnippet);
            result.SetRanges(this.Ranges);

            return result;
        } // ToSpdxSnippet()
        #endregion PUBLIC METHODS
    } // SpdxSnippetForParsing
}
