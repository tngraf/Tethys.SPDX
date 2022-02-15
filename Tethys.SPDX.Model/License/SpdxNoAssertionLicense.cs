// ---------------------------------------------------------------------------
// <copyright file="SpdxNoAssertionLicense.cs" company="Tethys">
//   Copyright (C) 2019 T. Graf
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
    /// Special class of license to represent no asserted license
    /// license in the file or packages.
    /// </summary>
    public class SpdxNoAssertionLicense : AnyLicenseInfo
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// The no assertion license name.
        /// </summary>
        public const string NoAssertionLicenseName = "NOASSERTION";
        #endregion // PUBLIC PROPERTIES

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
            return NoAssertionLicenseName;
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxNoAssertionLicense
}
