// ---------------------------------------------------------------------------
// <copyright file="ISpdxExceptionList.cs" company="Tethys">
//   Copyright (C) 2019-2024 T. Graf
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
    /// The interface for SPDX exception lists.
    /// </summary>
    public interface ISpdxExceptionList
    {
        /// <summary>
        /// Gets the license list.
        /// </summary>
        IReadOnlyList<ISpdxExceptionListEntry> Exceptions { get; }

        /// <summary>
        /// Gets or sets the license list version.
        /// </summary>
        string LicenseListVersion { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        string ReleaseDate { get; set; }
    }
}
