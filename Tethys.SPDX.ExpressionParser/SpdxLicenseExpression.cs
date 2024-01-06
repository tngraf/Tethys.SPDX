// ---------------------------------------------------------------------------
// <copyright file="SpdxLicenseExpression.cs" company="Tethys">
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
    public class SpdxLicenseExpression : SpdxExpression
    {
        //// Annex D SPDX license expressions
        //// simple-expression = license-id / license-id"+" / license-ref
        //// we replace this with
        //// simple-expression = SpdxLicenseExpression / SpdxLicenseReference

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the license ID.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Gets a value indicating whether or not later versions of the license is accepted.
        /// </summary>
        public bool OrLater { get; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxLicenseExpression"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="orLater">if set to <c>true</c> [or later].</param>
        public SpdxLicenseExpression(string id, bool orLater)
        {
            this.Id = id ?? throw new ArgumentNullException(nameof(id));
            this.OrLater = orLater;
        } // SpdxLicenseExpression()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <inheritdoc />
        public override string ToString()
        {
            var plus = this.OrLater ? "+" : string.Empty;
            return $"{this.Id}{plus}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxLicenseExpression
}
