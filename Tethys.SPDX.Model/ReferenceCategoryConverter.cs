// ---------------------------------------------------------------------------
// <copyright file="ReferenceCategoryConverter.cs" company="Tethys">
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
    /// Converts a ReferenceCategory to its correct string value.
    /// </summary>
    public class ReferenceCategoryConverter : JsonConverter
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The PrimaryPackagePurpose dictionary.
        /// </summary>
        private static Dictionary<string, ReferenceCategory> mapReferenceCategories;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes static members of the <see cref="ReferenceCategoryConverter"/> class.
        /// </summary>
        static ReferenceCategoryConverter()
        {
            InitializeMapReferenceCategories();
        } // ReferenceCategoryConverter()
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
            var rc = (ReferenceCategory)value;
            writer.WriteValue(EnumToString(rc));
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
                throw new ArgumentOutOfRangeException("Invalid ReferenceCategory");
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
            return objectType == typeof(ReferenceCategory);
        } // CanConvert()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Converts the enums value to a matching string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A string.</returns>
        private static string EnumToString(ReferenceCategory type)
        {
            foreach (var mapEntry in mapReferenceCategories)
            {
                if (mapEntry.Value == type)
                {
                    return mapEntry.Key;
                } // if
            } // foreach

            throw new ArgumentOutOfRangeException(nameof(type), "Unknown ReferenceCategory");
        } // EnumToString()

        /// <summary>
        /// Converts a strings to the matching enum value.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A <see cref="ReferenceCategory"/> value.</returns>
        private static ReferenceCategory StringToEnum(string text)
        {
            if (mapReferenceCategories.ContainsKey(text))
            {
                return mapReferenceCategories[text];
            } // if

            throw new ArgumentOutOfRangeException(nameof(text), "Unknown ReferenceCategory");
        } // StringToEnum()

        /// <summary>
        /// Initializes the map of reference categories.
        /// </summary>
        private static void InitializeMapReferenceCategories()
        {
            mapReferenceCategories = new Dictionary<string, ReferenceCategory>
            {
                { "OTHER", ReferenceCategory.Other },
                { "PACKAGE-MANAGER", ReferenceCategory.PackageManager },
                { "SECURITY", ReferenceCategory.Security },
                { "PERSISTENT-ID", ReferenceCategory.PersistentId },
            };
        } // InitializeMapReferenceCategories()
        #endregion PRIVATE METHODS
    } // ReferenceCategoryConverter
}
