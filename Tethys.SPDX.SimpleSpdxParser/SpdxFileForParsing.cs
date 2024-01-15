// ---------------------------------------------------------------------------
// <copyright file="SpdxFileForParsing.cs" company="Tethys">
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
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Tethys.SPDX.Model;
    using Tethys.SPDX.Model.License;

    /// <summary>
    /// This is a special variant of <see cref="SpdxFile"/> without
    /// read-only properties. This simplifies reading the information
    /// from JSON files.
    /// </summary>
    internal class SpdxFileForParsing : SpdxItemForParsing
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the license information from files.
        /// </summary>
        [JsonProperty("licenseInfoFromFiles")]
        [JsonConverter(typeof(JsonLicenseListConverter))]
        public new List<AnyLicenseInfo> LicenseInfoFromFiles { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file types.
        /// </summary>
        [JsonProperty("fileTypes")]
        [JsonConverter(typeof(FileTypeConverter))]
        public List<FileType> FileTypes { get; set; }

        /// <summary>
        /// Gets or sets the checksums.
        /// </summary>
        [JsonProperty("checksums")]
        public List<Checksum> Checksums { get; set; }

        /// <summary>
        /// Gets or sets the file contributors.
        /// </summary>
        [JsonProperty("fileContributors")]
        public List<string> FileContributors { get; set; }

        /// <summary>
        /// Gets or sets the notice text.
        /// </summary>
        [JsonProperty("noticeText")]
        public string NoticeText { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxFileForParsing"/> class.
        /// </summary>
        public SpdxFileForParsing()
        {
        } // SpdxFileForParsing()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Converts this instance to a SpdxFile.
        /// </summary>
        /// <returns>A <see cref="SpdxFile"/> object.</returns>
        public SpdxFile ToSpdxFile()
        {
            var result = new SpdxFile();

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
            if (this.LicenseInfoFromFiles != null)
            {
                result.SetLicenseInfoFromFiles(this.LicenseInfoFromFiles);
            } // if

            // SpdxFile properties
            result.FileName = this.FileName;
            if (this.FileTypes != null)
            {
                result.SetFileTypes(this.FileTypes);
            } // if

            if (this.Checksums != null)
            {
                result.SetChecksums(this.Checksums);
            } // if

            if (this.FileContributors != null)
            {
                result.SetFileContributors(this.FileContributors);
            } // if

            result.NoticeText = this.NoticeText;

            return result;
        } // ToSpdxFile()
        #endregion // PUBLIC METHODS
    } // SpdxFileForParsing
}
