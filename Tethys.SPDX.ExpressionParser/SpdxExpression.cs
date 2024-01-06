// ---------------------------------------------------------------------------
// <copyright file="SpdxExpression.cs" company="Tethys">
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
    /*************************************************************************
     * SPDX Expressions
     * ----------------
     * idstring = 1*(ALPHA / DIGIT / "-" / "." )
     *
     * license-id = <short form license identifier in Annex A.1>
     *
     * license-exception-id = <short form license exception identifier in Annex A.2>
     *
     * license-ref = ["DocumentRef-"(idstring)":"]"LicenseRef-"(idstring)
     *
     * simple-expression = license-id / license-id"+" / license-ref
     *
     * compound-expression = (simple-expression /
     *    simple-expression "WITH" license-exception-id /
     *    compound-expression "AND" compound-expression /
     *    compound-expression "OR" compound-expression /
     *    "(" compound-expression ")" )
     *
     * license-expression = (simple-expression / compound-expression)
     *
     ************************************************************************/

    /// <summary>
    /// Represents an SPDX expression.
    /// </summary>
    public abstract class SpdxExpression
    {
        /// <summary>
        /// Converts an <see cref="SpdxExpression"/> to a string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public new abstract string ToString();
    } // SpdxExpression
}
