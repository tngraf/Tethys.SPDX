// ---------------------------------------------------------------------------
// <copyright file="RelationshipType.cs" company="Tethys">
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
    /// <summary>
    /// Different types of relationships between SPDX items.
    /// </summary>
    public enum RelationshipType
    {
        /// <summary>
        /// Is to be used when SPDXRef-DOCUMENT describes SPDXRef-A.
        /// </summary>
        Describes,

        /// <summary>
        /// Is to be used when SPDXRef-A is described by SPDXREF-Document.
        /// </summary>
        DescribedBy,

        /// <summary>
        /// Is to be used when SPDXRef-A is an ancestor (same lineage but pre-dates) SPDXRef-B.
        /// </summary>
        AncestorOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is used to build SPDXRef-B.
        /// </summary>
        BuildToolOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is contained by SPDXRef-B.
        /// </summary>
        ContainedBy,

        /// <summary>
        /// Is to be used when SPDXRef-A contains SPDXRef-B.
        /// </summary>
        Contains,

        /// <summary>
        /// Is to be used when SPDXRef-A is an exact copy of SPDXRef-B.
        /// </summary>
        CopyOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is a data file used in SPDXRef-B.
        /// </summary>
        DataFileOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is a descendant of (same lineage but postdates) SPDXRef-B.
        /// </summary>
        DescendantOf,

        /// <summary>
        /// Is to be used when distributing SPDXRef-A requires that SPDXRef-B also be distributed.
        /// </summary>
        DistributionArtifact,

        /// <summary>
        /// Is to be used when SPDXRef-A provides documentation of SPDXRef-B.
        /// </summary>
        DocumentationOf,

        /// <summary>
        /// Is to be used when SPDXRef-A dynamically links to SPDXRef-B.
        /// </summary>
        DynamicLink,

        /// <summary>
        /// Is to be used when SPDXRef-A is expanded from the archive SPDXRef-B.
        /// </summary>
        ExpandedFromArchive,

        /// <summary>
        /// Is to be used when SPDXRef-A is a file that was added to SPDXRef-B.
        /// </summary>
        FileAdded,

        /// <summary>
        /// Is to be used when SPDXRef-A is a file that was deleted from SPDXRef-B.
        /// </summary>
        FileDeleted,

        /// <summary>
        /// Is to be used when SPDXRef-A is a file that was modified from SPDXRef-B.
        /// </summary>
        FileModified,

        /// <summary>
        /// Is to be used when SPDXRef-A was generated from SPDXRef-B.
        /// </summary>
        GeneratedFrom,

        /// <summary>
        /// Is to be used when SPDXRef-A generates SPDXRef-B.
        /// </summary>
        Generates,

        /// <summary>
        /// Is to be used when SPDXRef-A is a meta file of SPDXRef-B.
        /// </summary>
        MetaFileOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is an optional component of SPDXRef-B.
        /// </summary>
        OptionalComponentOf,

        /// <summary>
        /// Is to be used for a relationship which has not been defined in the formal SPDX specification.
        /// A description of the relationship should be included in the Relationship comments field.
        /// </summary>
        Other,

        /// <summary>
        /// Is to be used when SPDXRef-A is used as a package as part of SPDXRef-B.
        /// </summary>
        PackageOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is a patch file that has been applied to SPDXRef-B.
        /// </summary>
        PatchApplied,

        /// <summary>
        /// Is to be used when SPDXRef-A is a patch file for (to be applied to) SPDXRef-B.
        /// </summary>
        PatchFor,

        /// <summary>
        /// Is to be used when (current) SPDXRef-DOCUMENT amends the SPDX information in SPDXRef-B.
        /// </summary>
        Amends,

        /// <summary>
        /// Is to be used when SPDXRef-A statically links to SPDXRef-B.
        /// </summary>
        StatikLink,

        /// <summary>
        /// Is to be used when SPDXRef-A is a test case used in testing SPDXRef-B.
        /// </summary>
        TestCaseOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is a prerequisite for SPDXRef-B.
        /// </summary>
        PrerequisiteFor,

        /// <summary>
        /// Is to be used when SPDXRef-A has as a prerequisite SPDXRef-B.
        /// </summary>
        HasPrerequisite,

        /// <summary>
        /// Is to be used when SPDXRef-A depends on SPDXRef-B.
        /// </summary>
        DependesOn,

        /// <summary>
        /// Is to be used when SPDXRef-A is dependency of SPDXRef-B.
        /// </summary>
        DependencyOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is a manifest file that lists a set of dependencies for SPDXRef-B.
        /// </summary>
        DependencyManifestOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is a build dependency of SPDXRef-B.
        /// </summary>
        BuildDependencyOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is a development dependency of SPDXRef-B.
        /// </summary>
        DevDependencyOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is an optional dependency of SPDXRef-B.
        /// </summary>
        OptionalDependencyOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is a to be provided dependency of SPDXRef-B.
        /// </summary>
        ProvidedDependencyOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is a test dependency of SPDXRef-B.
        /// </summary>
        TestDependencyOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is a dependency required for the execution of SPDXRef-B.
        /// </summary>
        RuntimeDependencyOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is an example of SPDXRef-B.
        /// </summary>
        ExampleOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is a variant of (same lineage but not clear which came first) SPDXRef-B.
        /// </summary>
        VariantOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is used as a development tool for SPDXRef-B.
        /// </summary>
        DevToolOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is used for testing SPDXRef-B.
        /// </summary>
        TestOf,

        /// <summary>
        /// Is to be used when SPDXRef-A is used as a test tool for SPDXRef-B.
        /// </summary>
        TestToolOf,

        /// <summary>
        /// Is to be used when SPDXRef-A describes, illustrates, or specifies a requirement statement for SPDXRef-B.
        /// </summary>
        RequirementDescriptionFor,

        /// <summary>
        /// Is to be used when SPDXRef-A describes, illustrates, or defines a design specification for SPDXRef-B.
        /// </summary>
        SpecificationFor,
    } // RelationshipType
}