// ---------------------------------------------------------------------------
// <copyright file="DisjunctiveLicenseSet.cs" company="Tethys">
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
    using System.Text;

    /// <summary>
    /// A set of licenses where there is a choice of one of the licenses in the set.
    /// </summary>
    /// <seealso cref="LicenseSet" />
    public class DisjunctiveLicenseSet : LicenseSet
    {
        #region CONSTRUCTION
        /// <summary>Initializes a new instance of the <see cref="DisjunctiveLicenseSet"/> class.</summary>
        /// <param name="licenses">The licenses.</param>
        public DisjunctiveLicenseSet(IEnumerable<AnyLicenseInfo> licenses)
            : base(licenses)
        {
        } // DisjunctiveLicenseSet()
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
            var sb = new StringBuilder("(");
            var moreThanOne = false;
            foreach (var licenseInfo in this.LicenseInfos)
            {
                if (moreThanOne)
                {
                    sb.Append(" OR ");
                } // if

                moreThanOne = true;
                sb.Append(licenseInfo);
            } // foreach

            sb.Append(")");

            return sb.ToString();
        } // ToString()
        #endregion // PUBLIC METHODS
    } // DisjunctiveLicenseSet
}
