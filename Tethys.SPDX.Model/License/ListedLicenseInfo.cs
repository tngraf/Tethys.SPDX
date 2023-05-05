// ---------------------------------------------------------------------------
// <copyright file="ListedLicenseInfo.cs" company="Tethys">
//   Copyright (C) 2023 T. Graf
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
    /// <summary>
    /// An ListedLicenseInfo represents a License which is included in the
    /// SPDX License List (http://spdx.org/licenses).
    /// This is new in SPDX 2.3.
    /// </summary>
    /// <seealso cref="SimpleLicensingInfo" />
    public class ListedLicenseInfo : SimpleLicensingInfo
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="ListedLicenseInfo"/> class.
        /// </summary>
        public ListedLicenseInfo()
        {
            this.Text = string.Empty;
        } // ListedLicenseInfo()
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
    } // ListedLicenseInfo
}
