// ---------------------------------------------------------------------------
// <copyright file="TokenType.cs" company="Tethys">
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

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tethys.SPDX.ExpressionParser.Test")]

namespace Tethys.SPDX.ExpressionParser
{
    /// <summary>
    /// The token types.
    /// </summary>
    internal enum TokenType
    {
        /// <summary>
        /// A license identifier like MIT, Apache-2.0 or GPL-2.0.
        /// </summary>
        LicenseId,

        /// <summary>
        /// A license reference like LicenseRed-someorg-somename.
        /// </summary>
        LicenseRef,

        /// <summary>
        /// A license exception like Autoconf-exception-2.0.
        /// </summary>
        Exception,

        /// <summary>
        /// A trailing plus sign to indicate "or later".
        /// </summary>
        Plus,

        /// <summary>
        /// A left parenthesis.
        /// </summary>
        Left,

        /// <summary>
        /// A right parenthesis.
        /// </summary>
        Right,

        /// <summary>
        /// The license exception combination keyword..
        /// </summary>
        With,

        /// <summary>
        /// The license conjunction keyword.
        /// </summary>
        And,

        /// <summary>
        /// The license disjunction keyword.
        /// </summary>
        Or,
    } // TokenType
}
