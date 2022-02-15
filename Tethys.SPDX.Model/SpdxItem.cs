// ---------------------------------------------------------------------------
// <copyright file="SpdxItem.cs" company="Tethys">
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
    /// An SpdxItem is a potentially copyrightable work.
    /// </summary>
    /// <seealso cref="SpdxElement" />
    public class SpdxItem : SpdxElement
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The license information from files.
        /// </summary>
        private List<AnyLicenseInfo> licenseInfoFromFiles;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the license concluded.
        /// </summary>
        public AnyLicenseInfo LicenseConcluded { get; set; }

        /// <summary>
        /// Gets the license information from files.
        /// </summary>
        public IReadOnlyList<AnyLicenseInfo> LicenseInfoFromFiles => this.licenseInfoFromFiles;

        /// <summary>
        /// Gets or sets the copyright text.
        /// </summary>
        public string CopyrightText { get; set; }

        /// <summary>
        /// Gets or sets the license comments.
        /// </summary>
        public string LicenseComments { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxItem"/> class.
        /// </summary>
        public SpdxItem()
        {
            this.licenseInfoFromFiles = new List<AnyLicenseInfo>();
        } // SpdxItem()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Sets the license information from files.
        /// </summary>
        /// <param name="fileLicenses">The file licenses.</param>
        public void SetLicenseInfoFromFiles(IEnumerable<AnyLicenseInfo> fileLicenses)
        {
            this.licenseInfoFromFiles = new List<AnyLicenseInfo>(fileLicenses);
        } // SetLicenseInfoFromFiles()

        /// <summary>
        /// Adds the license information from file.
        /// </summary>
        /// <param name="fileLicense">The file license.</param>
        public void AddLicenseInfoFromFile(AnyLicenseInfo fileLicense)
        {
            this.licenseInfoFromFiles.Add(fileLicense);
        } // AddLicenseInfoFromFile()
        #endregion // PUBLIC METHODS
    } // SpdxItem
}
