// ---------------------------------------------------------------------------
// <copyright file="PrimaryPackagePurposeConverter.cs" company="Tethys">
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
    public class PrimaryPackagePurposeConverter : JsonConverter
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The PrimaryPackagePurpose dictionary.
        /// </summary>
        private static Dictionary<string, PrimaryPackagePurpose> mapPrimaryPackagePurposes;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes static members of the <see cref="PrimaryPackagePurposeConverter"/> class.
        /// </summary>
        static PrimaryPackagePurposeConverter()
        {
            InitializeMapPrimaryPackagePurposes();
        } // PrimaryPackagePurposeConverter()
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
            var ppp = (PrimaryPackagePurpose)value;
            writer.WriteValue(EnumToString(ppp));
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
                throw new ArgumentOutOfRangeException("Invalid PrimaryPackagePurpose");
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
        private static string EnumToString(PrimaryPackagePurpose type)
        {
            foreach (var mapEntry in mapPrimaryPackagePurposes)
            {
                if (mapEntry.Value == type)
                {
                    return mapEntry.Key;
                } // if
            } // foreach

            throw new ArgumentOutOfRangeException(nameof(type), "Unknown PrimaryPackagePurpose");
        } // EnumToString()

        /// <summary>
        /// Converts a strings to the matching enum value.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A <see cref="PrimaryPackagePurpose"/> value.</returns>
        private static PrimaryPackagePurpose StringToEnum(string text)
        {
            if (mapPrimaryPackagePurposes.ContainsKey(text))
            {
                return mapPrimaryPackagePurposes[text];
            } // if

            throw new ArgumentOutOfRangeException(nameof(text), "Unknown PrimaryPackagePurpose");
        } // StringToEnum()

        /// <summary>
        /// Initializes the map of package purposes.
        /// </summary>
        private static void InitializeMapPrimaryPackagePurposes()
        {
            mapPrimaryPackagePurposes = new Dictionary<string, PrimaryPackagePurpose>
            {
                { "OTHER", PrimaryPackagePurpose.Other },
                { "APPLICATION", PrimaryPackagePurpose.Application },
                { "FRAMEWORK", PrimaryPackagePurpose.Framework },
                { "LIBRARY", PrimaryPackagePurpose.Library },
                { "CONTAINER", PrimaryPackagePurpose.Container },
                { "OPERATING_SYSTEM", PrimaryPackagePurpose.OperatingSystem },
                { "DEVICE", PrimaryPackagePurpose.Device },
                { "FIRMWARE", PrimaryPackagePurpose.Firmware },
                { "SOURCE", PrimaryPackagePurpose.Source },
                { "ARCHIVE", PrimaryPackagePurpose.Archive },
                { "FILE", PrimaryPackagePurpose.File },
                { "INSTALL", PrimaryPackagePurpose.Install },
            };
            } // InitializeMapPrimaryPackagePurposes()
        #endregion PRIVATE METHODS
    } // PrimaryPackagePurposeConverter
}
