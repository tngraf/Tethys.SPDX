// ---------------------------------------------------------------------------
// <copyright file="IDataManager.cs" company="Tethys">
// Copyright (C) 2024 T. Graf
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

namespace Tethys.SPDX.Model
{
    using System.Collections.Generic;
    using Tethys.SPDX.ExpressionParser;
    using Tethys.SPDX.Model.License;

    /// <summary>
    /// Interface for data managers.
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Gets the known document licenses.
        /// </summary>
        IReadOnlyDictionary<string, AnyLicenseInfo> KnownDocumentLicenses { get; }

        /// <summary>
        /// Gets the known document licenses.
        /// </summary>
        IReadOnlyDictionary<string, AnyLicenseInfo> ListedLicenses { get; }

        /// <summary>
        /// Gets the SPDX parsing options.
        /// </summary>
        SpdxParsingOptions SpdxParsingOptions { get; }

        /// <summary>
        /// Finds the license by identifier.
        /// </summary>
        /// <param name="licenseId">The license identifier.</param>
        /// <returns>A <see cref="AnyLicenseInfo"/> object or null.</returns>
        License.License FindLicenseById(string licenseId);

        /// <summary>
        /// Parses an SPDX expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A <see cref="SpdxExpression"/> object.</returns>
        SpdxExpression ParseSpdxExpression(string expression);
    }
}