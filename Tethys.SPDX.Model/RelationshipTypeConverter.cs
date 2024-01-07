// ---------------------------------------------------------------------------
// <copyright file="RelationshipTypeConverter.cs" company="Tethys">
//   Copyright (C) 2024 T. Graf
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
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Converts a PrimaryPackagePurpose to its correct string value.
    /// </summary>
    public class RelationshipTypeConverter : JsonConverter
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The relationship types dictionary.
        /// </summary>
        private static Dictionary<string, RelationshipType> mapRelationshipTypes;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes static members of the <see cref="RelationshipTypeConverter"/> class.
        /// </summary>
        static RelationshipTypeConverter()
        {
            InitializeMapRelationshipTypes();
        } // RelationshipTypeConverter()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var rt = (RelationshipType)value;
            writer.WriteValue(EnumToString(rt));
        } // WriteJson()

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = (string)reader.Value;
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentOutOfRangeException("Invalid RelationshipType");
            } // if

            return StringToEnum(token);
        } // ReadJson()

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON; otherwise, <c>false</c>.
        /// </value>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PrimaryPackagePurpose);
        } // CanConvert()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Converts the enums value to a matching string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A string.</returns>
        private static string EnumToString(RelationshipType type)
        {
            foreach (var mapEntry in mapRelationshipTypes)
            {
                if (mapEntry.Value == type)
                {
                    return mapEntry.Key;
                } // if
            } // foreach

            throw new ArgumentOutOfRangeException(nameof(type), "Unknown RelationshipType");
        } // EnumToString()

        /// <summary>
        /// Converts a strings to the matching enum value.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A <see cref="RelationshipType"/> value.</returns>
        private static RelationshipType StringToEnum(string text)
        {
            if (mapRelationshipTypes.ContainsKey(text))
            {
                return mapRelationshipTypes[text];
            } // if

            throw new ArgumentOutOfRangeException(nameof(text), "Unknown RelationshipType");
        } // StringToEnum()

        /// <summary>
        /// Initializes the map relationship types.
        /// </summary>
        private static void InitializeMapRelationshipTypes()
        {
            mapRelationshipTypes = new Dictionary<string, RelationshipType>();
            mapRelationshipTypes.Add("AMENDS", RelationshipType.Amends);
            mapRelationshipTypes.Add("ANCESTOR_OF", RelationshipType.AncestorOf);
            mapRelationshipTypes.Add("BUILD_TOOL_OF", RelationshipType.BuildToolOf);
            mapRelationshipTypes.Add("CONTAINED_BY", RelationshipType.ContainedBy);
            mapRelationshipTypes.Add("CONTAINS", RelationshipType.Contains);
            mapRelationshipTypes.Add("COPY_OF", RelationshipType.CopyOf);
            mapRelationshipTypes.Add("DATA_FILE_OF", RelationshipType.DataFileOf);
            mapRelationshipTypes.Add("DESCENDANT_OF", RelationshipType.DescendantOf);
            mapRelationshipTypes.Add("DISTRIBUTION_ARTIFACT", RelationshipType.DistributionArtifact);
            mapRelationshipTypes.Add("DOCUMENTATION_OF", RelationshipType.DocumentationOf);
            mapRelationshipTypes.Add("DYNAMIC_LINK", RelationshipType.DynamicLink);
            mapRelationshipTypes.Add("EXPANDED_FROM_ARCHIVE", RelationshipType.ExpandedFromArchive);
            mapRelationshipTypes.Add("FILE_ADDED", RelationshipType.FileAdded);
            mapRelationshipTypes.Add("FILE_DELETED", RelationshipType.FileDeleted);
            mapRelationshipTypes.Add("DESCRIBES", RelationshipType.Describes);
            mapRelationshipTypes.Add("DESCRIBED_BY", RelationshipType.DescribedBy);
            mapRelationshipTypes.Add("FILE_MODIFIED", RelationshipType.FileModified);
            mapRelationshipTypes.Add("GENERATED_FROM", RelationshipType.GeneratedFrom);
            mapRelationshipTypes.Add("GENERATES", RelationshipType.Generates);
            mapRelationshipTypes.Add("METAFILE_OF", RelationshipType.MetaFileOf);
            mapRelationshipTypes.Add("OPTIONAL_COMPONENT_OF", RelationshipType.OptionalComponentOf);
            mapRelationshipTypes.Add("OTHER", RelationshipType.Other);
            mapRelationshipTypes.Add("PACKAGE_OF", RelationshipType.PackageOf);
            mapRelationshipTypes.Add("PATCH_APPLIED", RelationshipType.PatchApplied);
            mapRelationshipTypes.Add("PATCH_FOR", RelationshipType.PatchFor);
            mapRelationshipTypes.Add("STATIC_LINK", RelationshipType.StatikLink);
            mapRelationshipTypes.Add("TEST_CASE_OF", RelationshipType.TestCaseOf);
            mapRelationshipTypes.Add("PREREQUISITE_FOR", RelationshipType.PrerequisiteFor);
            mapRelationshipTypes.Add("HAS_PREREQUISITE", RelationshipType.HasPrerequisite);
            mapRelationshipTypes.Add("DEPENDS_ON", RelationshipType.DependesOn);
            mapRelationshipTypes.Add("DEPENDENCY_OF", RelationshipType.DependencyOf);
            mapRelationshipTypes.Add("DEPENDENCY_MANIFEST_OF", RelationshipType.DependencyManifestOf);
            mapRelationshipTypes.Add("BUILD_DEPENDENCY_OF", RelationshipType.BuildDependencyOf);
            mapRelationshipTypes.Add("DEV_DEPENDENCY_OF", RelationshipType.DevDependencyOf);
            mapRelationshipTypes.Add("OPTIONAL_DEPENDENCY_OF", RelationshipType.OptionalDependencyOf);
            mapRelationshipTypes.Add("PROVIDED_DEPENDENCY_OF", RelationshipType.ProvidedDependencyOf);
            mapRelationshipTypes.Add("TEST_DEPENDENCY_OF", RelationshipType.TestDependencyOf);
            mapRelationshipTypes.Add("RUNTIME_DEPENDENCY_OF", RelationshipType.RuntimeDependencyOf);
            mapRelationshipTypes.Add("EXAMPLE_OF", RelationshipType.ExampleOf);
            mapRelationshipTypes.Add("VARIANT_OF", RelationshipType.VariantOf);
            mapRelationshipTypes.Add("DEV_TOOL_OF", RelationshipType.DevToolOf);
            mapRelationshipTypes.Add("TEST_OF", RelationshipType.TestOf);
            mapRelationshipTypes.Add("TEST_TOOL_OF", RelationshipType.TestToolOf);
            mapRelationshipTypes.Add("REQUIREMENT_DESCRIPTION_FOR", RelationshipType.RequirementDescriptionFor);
            mapRelationshipTypes.Add("SPECIFICATION_FOR", RelationshipType.SpecificationFor);
        } // InitializeMapRelationshipTypes()
        #endregion PRIVATE METHODS
    } // RelationshipTypeConverter
}
