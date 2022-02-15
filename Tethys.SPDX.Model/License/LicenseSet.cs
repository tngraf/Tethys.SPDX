// ---------------------------------------------------------------------------
// <copyright file="LicenseSet.cs" company="Tethys">
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

namespace Tethys.SPDX.Model.License
{
    using System.Collections.Generic;

    /// <summary>
    /// A specific form of License information where there is a set of licenses represented.
    /// </summary>
    /// <seealso cref="AnyLicenseInfo" />
    public class LicenseSet : AnyLicenseInfo
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The License information.
        /// </summary>
        private List<AnyLicenseInfo> licenseInfos;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the License information.
        /// </summary>
        public IReadOnlyList<AnyLicenseInfo> LicenseInfos => this.licenseInfos;
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>Initializes a new instance of the <see cref="LicenseSet"/> class.</summary>
        /// <param name="licenses">The licenses.</param>
        public LicenseSet(IEnumerable<AnyLicenseInfo> licenses)
        {
            this.licenseInfos = new List<AnyLicenseInfo>(licenses);
        } // LicenseSet()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Adds the member.
        /// </summary>
        /// <param name="license">The license.</param>
        public void AddMember(AnyLicenseInfo license)
        {
            this.licenseInfos.Add(license);
        } // AddMember()

        /// <summary>
        /// Sets the members of the License set.  Clears any previous members.
        /// </summary>
        /// <param name="licenses">The licenses.</param>
        public void SetMembers(IEnumerable<AnyLicenseInfo> licenses)
        {
            this.licenseInfos = new List<AnyLicenseInfo>(licenses);
        } // SetMembers()
        #endregion // PUBLIC METHODS
    } // LicenseSet
}
