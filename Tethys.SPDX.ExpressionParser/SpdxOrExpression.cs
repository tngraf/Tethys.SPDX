// ---------------------------------------------------------------------------
// <copyright file="SpdxOrExpression.cs" company="Tethys">
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
    public class SpdxOrExpression : SpdxExpression
    {
        //// Annex D SPDX license expressions
        //// compound-expression "OR" compound-expression

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the left side of the expression.
        /// </summary>
        public SpdxExpression Left { get; }

        /// <summary>
        /// Gets the right side of the expression.
        /// </summary>
        public SpdxExpression Right { get; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxOrExpression"/> class.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        public SpdxOrExpression(SpdxExpression left, SpdxExpression right)
        {
            this.Left = left ?? throw new ArgumentNullException(nameof(left));
            this.Right = right ?? throw new ArgumentNullException(nameof(right));
        } // SpdxOrExpression()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Left.ToString()} OR {this.Right.ToString()}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxOrExpression
}
