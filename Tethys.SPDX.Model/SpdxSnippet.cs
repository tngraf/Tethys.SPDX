// ---------------------------------------------------------------------------
// <copyright file="SpdxSnippet.cs" company="Tethys">
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

namespace Tethys.SPDX.Model
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Tethys.SPDX.Model.License;
    using Tethys.SPDX.Model.Pointer;

    /// <summary>
    /// A snippet contains facts that are specific to only a part of a file.
    /// </summary>
    /// <seealso cref="SpdxItem" />
    public class SpdxSnippet : SpdxItem
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The license information in this snippet.
        /// </summary>
        private List<AnyLicenseInfo> licenseInfoInSnippet;

        /// <summary>
        /// The ranges.
        /// </summary>
        private List<StartEndPointer> ranges;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the snippet from file.
        /// </summary>
        [JsonProperty("snippetFromFile")]
        [JsonConverter(typeof(SpdxElementRefConverter))]
        public SpdxFile SnippetFromFile { get; set; }

        /// <summary>
        /// Gets the ranges.
        /// </summary>
        [JsonProperty("ranges")]
        public IReadOnlyList<StartEndPointer> Ranges => this.ranges;

        /// <summary>
        /// Gets the license information in this snippet.
        /// </summary>
        [JsonProperty("licenseInfoInSnippets")]
        [JsonConverter(typeof(JsonLicenseListConverter))]
        public IReadOnlyList<AnyLicenseInfo> LicenseInfoInSnippet => this.licenseInfoInSnippet;
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxSnippet"/> class.
        /// </summary>
        public SpdxSnippet()
        {
            // initialize all lists with null so that they are not JSON serialized
            this.licenseInfoInSnippet = null;
            this.ranges = null;
        } // SpdxSnippet()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Adds a license information to this snippet.
        /// </summary>
        /// <param name="license">The license.</param>
        public void AddLicenseInfoInSnippet(AnyLicenseInfo license)
        {
            this.licenseInfoInSnippet ??= new List<AnyLicenseInfo>();
            this.licenseInfoInSnippet.Add(license);
        } // AddLicenseInfoInSnippet()

        /// <summary>
        /// Sets the license information in snippet.
        /// </summary>
        /// <param name="newLicenses">The new licenses.</param>
        public void SetLicenseInfoInSnippet(IEnumerable<AnyLicenseInfo> newLicenses)
        {
            this.licenseInfoInSnippet = new List<AnyLicenseInfo>(newLicenses);
        } // SetLicenseInfoInSnippet()

        /// <summary>
        /// Adds a range.
        /// </summary>
        /// <param name="pointer">The pointer.</param>
        public void AddRange(StartEndPointer pointer)
        {
            this.ranges ??= new List<StartEndPointer>();
            this.ranges.Add(pointer);
        } // AddRange()

        /// <summary>
        /// Sets the ranges.
        /// </summary>
        /// <param name="newRanges">The new ranges.</param>
        public void SetRanges(IEnumerable<StartEndPointer> newRanges)
        {
            this.ranges = new List<StartEndPointer>(newRanges);
        } // SetRanges()

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Fromfile:{this.SnippetFromFile.SpdxIdentifier}, #Ranges={this.Ranges}, #LicenseInfos={this.LicenseInfoInSnippet}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxSnippet
}
