// ---------------------------------------------------------------------------
// <copyright file="SpdxParsingOptions.cs" company="Tethys">
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
    /// SPDX parsing options.
    /// </summary>
    [Flags]
    public enum SpdxParsingOptions
    {
        /// <summary>
        /// Default (= no) option.
        /// </summary>
        Default = 0x00,

        /// <summary>
        /// Allow unknown licenses.
        /// </summary>
        AllowUnknownLicenses = 0x01,

        /// <summary>
        /// Allow unknown exceptions.
        /// </summary>
        AllowUnknownExceptions = 0x02,
    } // SpdxParsingOptions
}
