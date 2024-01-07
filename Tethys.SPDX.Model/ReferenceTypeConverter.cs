// ---------------------------------------------------------------------------
// <copyright file="ReferenceTypeConverter.cs" company="Tethys">
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
    /// Converts a ReferenceType to its correct string value.
    /// </summary>
    public class ReferenceTypeConverter : JsonConverter
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The relationship types dictionary.
        /// </summary>
        private static Dictionary<string, ReferenceType> mapReferenceTypes;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes static members of the <see cref="ReferenceTypeConverter"/> class.
        /// </summary>
        static ReferenceTypeConverter()
        {
            InitializeMapReferenceTypes();
        } // ReferenceTypeConverter()
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
            var rt = (ReferenceType)value;
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
                throw new ArgumentOutOfRangeException("Invalid ReferenceType");
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
            return objectType == typeof(ReferenceType);
        } // CanConvert()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Converts the enums value to a matching string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A string.</returns>
        private static string EnumToString(ReferenceType type)
        {
            foreach (var mapEntry in mapReferenceTypes)
            {
                if (mapEntry.Value == type)
                {
                    return mapEntry.Key;
                } // if
            } // foreach

            throw new ArgumentOutOfRangeException(nameof(type), "Unknown ReferenceType");
        } // EnumToString()

        /// <summary>
        /// Converts a strings to the matching enum value.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A <see cref="ReferenceType"/> value.</returns>
        private static ReferenceType StringToEnum(string text)
        {
            if (mapReferenceTypes.ContainsKey(text))
            {
                return mapReferenceTypes[text];
            } // if

            throw new ArgumentOutOfRangeException(nameof(text), "Unknown ReferenceType");
        } // StringToEnum()

        /// <summary>
        /// Initializes the map relationship types.
        /// </summary>
        private static void InitializeMapReferenceTypes()
        {
            mapReferenceTypes = new Dictionary<string, ReferenceType>
            {
                { "cpe22Type", ReferenceType.Cpe22Type },
                { "cpe23Type", ReferenceType.Cpe23Type },
                { "advisory", ReferenceType.Advisory },
                { "fix", ReferenceType.Fix },
                { "url", ReferenceType.Url },
                { "swid", ReferenceType.SwId },
                { "maven-central", ReferenceType.MavenCentral },
                { "npm", ReferenceType.Npm },
                { "nuget", ReferenceType.NuGet },
                { "bower", ReferenceType.Bower },
                { "purl", ReferenceType.Purl },
                { "swh", ReferenceType.Swh },
                { "gitoid", ReferenceType.GitOid },
                { "[idstring]", ReferenceType.IdString },
            };
        } // InitializeMapReferenceTypes()
        #endregion PRIVATE METHODS
    } // SpdxDocumentRefConverter
}
