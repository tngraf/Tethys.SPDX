// ---------------------------------------------------------------------------
// <copyright file="SpdxExpressionException.cs" company="Tethys">
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
    public class SpdxExpressionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxExpressionException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SpdxExpressionException(string message)
            : base(message)
        {
        } // SpdxExpressionException()
    } // SpdxExpressionException
}
