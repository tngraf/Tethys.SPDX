// ---------------------------------------------------------------------------
// <copyright file="ReferenceCategory.cs" company="Tethys">
//   Copyright (C) 2018-2024 T. Graf
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
    using Newtonsoft.Json;

    /// <summary>
    /// Enumeration of reference categories.
    /// </summary>
    public enum ReferenceCategory
    {
        /// <summary>
        /// A package manager reference.
        /// </summary>
        PackageManager,

        /// <summary>
        /// A security reference.
        /// </summary>
        Security,

        /// <summary>
        /// Another reference.
        /// </summary>
        Other,

        /// <summary>
        /// A persistent id.
        /// </summary>
        PersistentId,
    } // ReferenceCategory
}
