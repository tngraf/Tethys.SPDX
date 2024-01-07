// ---------------------------------------------------------------------------
// <copyright file="SpdxNoneLicense.cs" company="Tethys">
//   Copyright (C) 2019-2024 T. Graf
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
    /// A special license meaning that no license was found.
    /// </summary>
    public class SpdxNoneLicense : AnyLicenseInfo
    {
        #region PUBLIC METHODS
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Constants.None;
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxNoneLicense
}
