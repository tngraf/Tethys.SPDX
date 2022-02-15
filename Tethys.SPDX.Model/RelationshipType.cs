// ---------------------------------------------------------------------------
// <copyright file="RelationshipType.cs" company="Tethys">
//   Copyright (C) 2018-2019 T. Graf
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
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Different types of relationships between SPDX items.
    /// </summary>
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1602:EnumerationItemsMustBeDocumented",
        Justification = "Reviewed. Suppression is OK here.")]
    public enum RelationshipType
    {
        // ReSharper disable InconsistentNaming
#pragma warning disable CS1591
        DESCRIBES,
        DESCRIBED_BY,
        ANCESTOR_OF,
        BUILD_TOOL_OF,
        CONTAINED_BY,
        CONTAINS,
        COPY_OF,
        DATA_FILE_OF,
        DESCENDANT_OF,
        DISTRIBUTION_ARTIFACT,
        DOCUMENTATION_OF,
        DYNAMIC_LINK,
        EXPANDED_FROM_ARCHIVE,
        FILE_ADDED,
        FILE_DELETED,
        FILE_MODIFIED,
        GENERATED_FROM,
        GENERATES,
        METAFILE_OF,
        OPTIONAL_COMPONENT_OF,
        OTHER,
        PACKAGE_OF,
        PATCH_APPLIED,
        PATCH_FOR,
        AMENDS,
        STATIC_LINK,
        TEST_CASE_OF,
        PREREQUISITE_FOR,
        HAS_PREREQUISITE,
#pragma warning restore CS1591
        // ReSharper enable InconsistentNaming
    } // RelationshipType
}