// ---------------------------------------------------------------------------
// <copyright file="SpdxWithExpression.cs" company="Tethys">
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
    public class SpdxWithExpression : SpdxExpression
    {
        //// Annex D SPDX license expressions
        //// simple-expression "WITH" license-exception-id

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the expression node.
        /// </summary>
        public SpdxExpression Expression { get; }

        /// <summary>
        /// Gets the license exception node.
        /// </summary>
        public string Exception { get; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxWithExpression"/> class.
        /// </summary>
        /// <param name="expression">The expression node.</param>
        /// <param name="exception">The license exception node.</param>
        public SpdxWithExpression(SpdxExpression expression, string exception)
        {
            this.Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            this.Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        } // SpdxWithExpression()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Expression.ToString()} WITH {this.Exception}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxExpression
}
