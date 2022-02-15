// ---------------------------------------------------------------------------
// <copyright file="SpdxSnippet.cs" company="Tethys">
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
        private readonly List<AnyLicenseInfo> licenseInfoInSnippet;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the snippet from file.
        /// </summary>
        public SpdxFile SnippetFromFile { get; set; }

        /// <summary>
        /// Gets or sets the byte range.
        /// </summary>
        public StartEndPointer ByteRange { get; set; }

        /// <summary>
        /// Gets or sets the line range.
        /// </summary>
        public StartEndPointer LineRange { get; set; }

        /// <summary>
        /// Gets the license information in this snippet.
        /// </summary>
        public IReadOnlyList<AnyLicenseInfo> LicenseInfoInSnippet => this.licenseInfoInSnippet;
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxSnippet"/> class.
        /// </summary>
        public SpdxSnippet()
        {
            this.licenseInfoInSnippet = new List<AnyLicenseInfo>();
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
            this.licenseInfoInSnippet.Add(license);
        } // AddLicenseInfoInSnippet()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PROTECTED METHODS
        #endregion // PROTECTED METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        #endregion // PRIVATE METHODS
    } // SpdxSnippet
}
