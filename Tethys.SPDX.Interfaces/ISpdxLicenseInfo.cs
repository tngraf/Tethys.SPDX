// ---------------------------------------------------------------------------
// <copyright file="ISpdxLicenseInfo.cs" company="Tethys">
//   Copyright (C) 2019-2022 T. Graf
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

namespace Tethys.SPDX.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for SPDX license information.
    /// </summary>
    public interface ISpdxLicenseInfo
    {
        /// <summary>
        /// Gets or sets a value indicating whether this is a deprecated license identifier.
        /// </summary>
        bool IsDeprecatedLicenseId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this license is <c>FSF libre</c>.
        /// </summary>
        bool IsFsfLibre { get; set; }

        /// <summary>
        /// Gets or sets the license text.
        /// </summary>
        string LicenseText { get; set; }

        /// <summary>
        /// Gets or sets the standard license header template.
        /// </summary>
        string StandardLicenseHeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the standard license template.
        /// </summary>
        string StandardLicenseTemplate { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the license comments.
        /// </summary>
        string LicenseComments { get; set; }

        /// <summary>
        /// Gets or sets the license identifier.
        /// </summary>
        string LicenseId { get; set; }

        /// <summary>
        /// Gets or sets the standard license header.
        /// </summary>
        string StandardLicenseHeader { get; set; }

        /// <summary>
        /// Gets or sets the see also data.
        /// </summary>
        List<string> SeeAlso { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is OSI approved.
        /// </summary>
        bool IsOsiApproved { get; set; }
    }
}