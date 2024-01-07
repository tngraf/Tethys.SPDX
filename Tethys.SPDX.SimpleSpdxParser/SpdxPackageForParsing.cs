// ---------------------------------------------------------------------------
// <copyright file="SpdxPackageForParsing.cs" company="Tethys">
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
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Tethys.SPDX.Model;
    using Tethys.SPDX.Model.License;

    /// <summary>
    /// This is a special variant of <see cref="SpdxPackage"/> without
    /// read-only properties. This simplifies reading the information
    /// from JSON files.
    /// </summary>
    internal class SpdxPackageForParsing : SpdxItem
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets a value indicating whether the files are analyzed.
        /// </summary>
        [JsonProperty("filesAnalyzed")]
        public bool IsFilesAnalyzed { get; set; }

        /// <summary>
        /// Gets or sets the license declared.
        /// </summary>
        [JsonProperty("licenseDeclared")]
        [JsonConverter(typeof(JsonLicenseConverter))]
        public AnyLicenseInfo LicenseDeclared { get; set; }

        /// <summary>
        /// Gets or sets the licenses from files.
        /// </summary>
        [JsonProperty("licenseInfoFromFiles")]
        [JsonConverter(typeof(JsonLicenseListConverter))]
        public new List<AnyLicenseInfo> LicenseInfoFromFiles { get; set; }

        /// <summary>
        /// Gets or sets the checksums.
        /// </summary>
        [JsonProperty("checksums")]
        public List<Checksum> Checksums { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the download location.
        /// </summary>
        [JsonProperty("downloadLocation")]
        public string DownloadLocation { get; set; }

        /// <summary>
        /// Gets or sets the homepage.
        /// </summary>
        [JsonProperty("homepage")]
        public string Homepage { get; set; }

        /// <summary>
        /// Gets or sets the originator.
        /// </summary>
        [JsonProperty("originator")]
        public string Originator { get; set; }

        /// <summary>
        /// Gets or sets the name of the package file.
        /// </summary>
        [JsonProperty("packageFileName")]
        public string PackageFileName { get; set; }

        /// <summary>
        /// Gets or sets the package verification code.
        /// </summary>
        [JsonProperty("packageVerificationCode")]
        public SpdxPackageVerificationCode PackageVerificationCode { get; set; }

        /// <summary>
        /// Gets or sets the source information.
        /// </summary>
        [JsonProperty("sourceInfo")]
        public string SourceInfo { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the supplier.
        /// </summary>
        [JsonProperty("supplier")]
        public string Supplier { get; set; }

        /// <summary>
        /// Gets or sets the version information.
        /// </summary>
        [JsonProperty("versionInfo")]
        public string VersionInfo { get; set; }

        /// <summary>
        /// Gets or sets the external refs.
        /// </summary>
        [JsonProperty("externalRefs")]
        public IReadOnlyList<ExternalRef> ExternalRefs { get; set; }

        /// <summary>
        /// Gets or sets the built date.
        /// </summary>
        [JsonProperty("builtDate")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime BuiltDate { get; set; }

        /// <summary>
        /// Gets or sets the primary package purpose.
        /// </summary>
        [JsonProperty("primaryPackagePurpose")]
        [JsonConverter(typeof(PrimaryPackagePurposeConverter))]
        public PrimaryPackagePurpose PrimaryPackagePurpose { get; set; }

        /// <summary>
        /// Gets or sets the "valid until" date.
        /// </summary>
        [JsonProperty("validUntilDate")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime ValidUntilDate { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        public List<SpdxFile> Files { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Converts this instance to a SpdxPackage.
        /// </summary>
        /// <returns>A <see cref="SpdxPackage"/> object.</returns>
        public SpdxPackage ToSpdxPackage()
        {
            var result = new SpdxPackage();

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

            // SpdxFile properties
            result.IsFilesAnalyzed = this.IsFilesAnalyzed;
            result.LicenseDeclared = this.LicenseDeclared;
            if (this.LicenseInfoFromFiles != null)
            {
                result.SetLicenseInfoFromFiles(this.LicenseInfoFromFiles);
            } // if

            if (this.Checksums != null)
            {
                result.SetChecksums(this.Checksums);
            } // if

            result.Description = this.Description;
            result.DownloadLocation = this.DownloadLocation;
            result.Homepage = this.Homepage;
            result.Originator = this.Originator;
            result.PackageFileName = this.PackageFileName;
            result.PackageVerificationCode = this.PackageVerificationCode;
            result.SourceInfo = this.SourceInfo;
            result.Summary = this.Summary;
            result.Supplier = this.Supplier;

            result.VersionInfo = this.VersionInfo;
            result.BuiltDate = this.BuiltDate;
            if (this.ExternalRefs != null)
            {
                result.SetExternalRefs(this.ExternalRefs);
            } // if

            result.PrimaryPackagePurpose = this.PrimaryPackagePurpose;
            result.ValidUntilDate = this.ValidUntilDate;
            if (this.Files != null)
            {
                result.SetFiles(this.Files);
            } // if

            return result;
        } // ToSpdxPackage()
        #endregion // PUBLIC METHODS
    } // SpdxPackageForParsing
}
