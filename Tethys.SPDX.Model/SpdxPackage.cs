// ---------------------------------------------------------------------------
// <copyright file="SpdxPackage.cs" company="Tethys">
//   Copyright (C) 2018 T. Graf
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

    using Tethys.SPDX.Model.License;

    /// <summary>
    /// A Package represents a collection of software files that are
    /// delivered as a single functional component.
    /// </summary>
    public class SpdxPackage : SpdxItem
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The licenses from files.
        /// </summary>
        private readonly List<AnyLicenseInfo> licensesFromFiles;

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
        public bool IsFilesAnalyzed { get; set; }

        /// <summary>
        /// Gets or sets the license declared.
        /// </summary>
        public AnyLicenseInfo LicenseDeclared { get; set; }

        /// <summary>
        /// Gets the licenses from files.
        /// </summary>
        public IReadOnlyList<AnyLicenseInfo> LicensesFromFiles => this.licensesFromFiles;

        /// <summary>
        /// Gets the checksums.
        /// </summary>
        public IReadOnlyList<Checksum> Checksums => this.checksums;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the download location.
        /// </summary>
        public string DownloadLocation { get; set; }

        /// <summary>
        /// Gets or sets the homepage.
        /// </summary>
        public string Homepage { get; set; }

        /// <summary>
        /// Gets or sets the originator.
        /// </summary>
        public string Originator { get; set; }

        /// <summary>
        /// Gets or sets the name of the package file.
        /// </summary>
        public string PackageFileName { get; set; }

        /// <summary>
        /// Gets or sets the package verification code.
        /// </summary>
        public SpdxPackageVerificationCode PackageVerificationCode { get; set; }

        /// <summary>
        /// Gets or sets the source information.
        /// </summary>
        public string SourceInfo { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the supplier.
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// Gets or sets the version information.
        /// </summary>
        public string VersionInfo { get; set; }

        /// <summary>
        /// Gets the external refs.
        /// </summary>
        public IReadOnlyList<ExternalRef> ExternalRefs => this.externalRefs;

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
            this.licensesFromFiles = new List<AnyLicenseInfo>();
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

        /// <summary>
        /// Adds the license information from files.
        /// </summary>
        /// <param name="license">The license.</param>
        public void AddLicenseInfoFromFiles(AnyLicenseInfo license)
        {
            this.licensesFromFiles.Add(license);
        } // AddLicenseInfoFromFiles()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PROTECTED METHODS
        #endregion // PROTECTED METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        #endregion // PRIVATE METHODS
    } // SpdxPackage
}
