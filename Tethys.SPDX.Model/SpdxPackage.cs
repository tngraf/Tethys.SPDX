// ---------------------------------------------------------------------------
// <copyright file="SpdxPackage.cs" company="Tethys">
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
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using Tethys.SPDX.Model.License;

    /// <summary>
    /// A Package represents a collection of software files that are
    /// delivered as a single functional component.
    /// </summary>
    public class SpdxPackage : SpdxItem
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The checksums.
        /// </summary>
        private List<Checksum> checksums;

        /// <summary>
        /// The external references.
        /// </summary>
        private List<ExternalRef> externalRefs;

        /// <summary>
        /// The files.
        /// </summary>
        private List<SpdxFile> files;
#endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

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
        /// Gets the checksums.
        /// </summary>
        [JsonProperty("checksums")]
        public IReadOnlyList<Checksum> Checksums => this.checksums;

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
        /// Gets the external refs.
        /// </summary>
        [JsonProperty("externalRefs")]
        public IReadOnlyList<ExternalRef> ExternalRefs => this.externalRefs;

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
        /// Gets the files.
        /// </summary>
        public List<SpdxFile> Files => this.files;
#endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxPackage"/> class.
        /// </summary>
        public SpdxPackage()
        {
            this.checksums = new List<Checksum>();
            this.externalRefs = new List<ExternalRef>();
            this.files = new List<SpdxFile>();
        } // SpdxPackage()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Sets the checksums.
        /// </summary>
        /// <param name="chksums">The checksums.</param>
        public void SetChecksums(IEnumerable<Checksum> chksums)
        {
            this.checksums = new List<Checksum>(chksums);
        } // SetChecksums()

        /// <summary>
        /// Adds the checksum.
        /// </summary>
        /// <param name="chksum">The checksum.</param>
        public void AddChecksum(Checksum chksum)
        {
            this.checksums.Add(chksum);
        } // AddChecksum()

        /// <summary>
        /// Adds an external reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        public void AddExternalRef(ExternalRef reference)
        {
            this.externalRefs.Add(reference);
        } // AddExternalRef()

        /// <summary>
        /// Sets the external references.
        /// </summary>
        /// <param name="references">The references.</param>
        public void SetExternalRefs(IEnumerable<ExternalRef> references)
        {
            this.externalRefs = new List<ExternalRef>(references);
        } // SetExternalRefs()

        /// <summary>
        /// Adds a new file.
        /// </summary>
        /// <param name="newFile">The new file.</param>
        public void AddFile(SpdxFile newFile)
        {
            this.files.Add(newFile);
        } // AddFile()

        /// <summary>
        /// Sets the files.
        /// </summary>
        /// <param name="newFiles">The new files.</param>
        public void SetFiles(IEnumerable<SpdxFile> newFiles)
        {
            this.files = new List<SpdxFile>(newFiles);
        } // SetFiles()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PROTECTED METHODS
        #endregion // PROTECTED METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        #endregion // PRIVATE METHODS
    } // SpdxPackage
}
