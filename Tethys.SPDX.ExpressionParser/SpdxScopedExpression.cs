// ---------------------------------------------------------------------------
// <copyright file="SpdxScopedExpression.cs" company="Tethys">
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
    /// Represents an SPDX expression enclosed by parenthesis.
    /// </summary>
    public class SpdxScopedExpression : SpdxExpression
    {
        //// Annex D SPDX license expressions
        //// "(" compound-expression ")"

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the expression node.
        /// </summary>
        public SpdxExpression Expression { get; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxScopedExpression"/> class.
        /// </summary>
        /// <param name="expression">The expression node.</param>
        public SpdxScopedExpression(SpdxExpression expression)
        {
            this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        } // SpdxScopedExpression()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <inheritdoc />
        public override string ToString()
        {
            return $"({this.Expression.ToString()})";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxScopedExpression
}
