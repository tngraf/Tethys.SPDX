// ---------------------------------------------------------------------------
// <copyright file="Token.cs" company="Tethys">
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
    /// Implements a token.
    /// </summary>
    internal class Token
    {
        #region PUBLIC PROPERTIES

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public TokenType Type { get; set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value { get; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public Token(TokenType type, string value)
        {
            this.Type = type;
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        } // Token()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Type}: {this.Value}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // Token
}
