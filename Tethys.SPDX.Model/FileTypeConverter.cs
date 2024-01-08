// ---------------------------------------------------------------------------
// <copyright file="FileTypeConverter.cs" company="Tethys">
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
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Converts a list of file types to its correct string value.
    /// </summary>
    public class FileTypeConverter : JsonConverter
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The PrimaryPackagePurpose dictionary.
        /// </summary>
        private static Dictionary<string, FileType> mapFileTypes;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes static members of the <see cref="FileTypeConverter"/> class.
        /// </summary>
        static FileTypeConverter()
        {
            InitializeMapFileTypes();
        } // FileTypeConverter()
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
            var list = (IReadOnlyList<FileType>)value;
            if (list == null)
            {
                return;
            } // if

            writer.WriteStartArray();

            foreach (var ft in list)
            {
                writer.WriteValue(EnumToString(ft));
            } // foreach

            writer.WriteEndArray();
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
            var result = new List<FileType>();

            if (reader.TokenType != JsonToken.Null)
            {
                if (reader.TokenType == JsonToken.StartArray)
                {
                    // this does not respect other registered converters
                    var token = JToken.Load(reader);
                    var stringArray = token.ToObject<string[]>();
                    if (stringArray == null)
                    {
                        return result;
                    } // if

                    foreach (var licenseText in stringArray)
                    {
                        var ft = StringToEnum(licenseText);
                        result.Add(ft);
                    } // foreach
                } // if
            } // if

            return result;
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
            return objectType == typeof(IReadOnlyList<FileType>);
        } // CanConvert()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Converts the enum value to a matching string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A string.</returns>
        private static string EnumToString(FileType type)
        {
            foreach (var mapEntry in mapFileTypes)
            {
                if (mapEntry.Value == type)
                {
                    return mapEntry.Key;
                } // if
            } // foreach

            throw new ArgumentOutOfRangeException(nameof(type), "Unknown FileType");
        } // EnumToString()

        /// <summary>
        /// Converts a strings to the matching enum value.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A <see cref="FileType"/> value.</returns>
        private static FileType StringToEnum(string text)
        {
            if (mapFileTypes.ContainsKey(text))
            {
                return mapFileTypes[text];
            } // if

            throw new ArgumentOutOfRangeException(nameof(text), "Unknown FileType");
        } // StringToEnum()

        /// <summary>
        /// Initializes the map of file types.
        /// </summary>
        private static void InitializeMapFileTypes()
        {
            mapFileTypes = new Dictionary<string, FileType>
            {
                { "OTHER", FileType.Other },
                { "APPLICATION", FileType.Application },
                { "SOURCE", FileType.Source },
                { "BINARY", FileType.Binary },
                { "ARCHIVE", FileType.Archive },
                { "AUDIO", FileType.Audio },
                { "IMAGE", FileType.Image },
                { "TEXT", FileType.Text },
                { "VIDEO", FileType.Video },
                { "DOCUMENTATION", FileType.Documentation },
                { "SPDX", FileType.Spdx },
            };
        } // InitializeMapFileTypes()
        #endregion PRIVATE METHODS
    } // FileTypeConverter
}
