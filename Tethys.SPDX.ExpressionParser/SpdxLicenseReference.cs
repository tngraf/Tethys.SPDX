// ---------------------------------------------------------------------------
// <copyright file="SpdxLicenseReference.cs" company="Tethys">
//   Copyright (C) 2023-2024 T. Graf
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

namespace Tethys.SPDX.ExpressionParser
{
    using System;

    /// <summary>
    /// Represents an SPDX expression.
    /// </summary>
    public class SpdxLicenseReference : SpdxExpression
    {
        //// Annex D SPDX license expressions
        //// LicenseRef-"(idstring)

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the license reference.
        /// </summary>
        public string LicenseRef { get; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxLicenseReference"/> class.
        /// </summary>
        /// <param name="licenseRef">The license reference.</param>
        public SpdxLicenseReference(string licenseRef)
        {
            this.LicenseRef = licenseRef ?? throw new ArgumentNullException();
        } // SpdxLicenseReference()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <inheritdoc />
        public override string ToString()
        {
            var text = $"{this.LicenseRef}";
            return text;
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxLicenseReference
}
