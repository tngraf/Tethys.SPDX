// ---------------------------------------------------------------------------
// <copyright file="ChecksumAlgorithmConverter.cs" company="Tethys">
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
    /// Converts a ChecksumAlgorithm to its correct string value.
    /// </summary>
    public class ChecksumAlgorithmConverter : JsonConverter
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The PrimaryPackagePurpose dictionary.
        /// </summary>
        private static Dictionary<string, ChecksumAlgorithm> mapChecksumAlgorithms;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes static members of the <see cref="ChecksumAlgorithmConverter"/> class.
        /// </summary>
        static ChecksumAlgorithmConverter()
        {
            InitializeMapChecksumAlgorithms();
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
            var ppp = (ChecksumAlgorithm)value;
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
                throw new ArgumentOutOfRangeException("Invalid ChecksumAlgorithm");
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
            return objectType == typeof(ChecksumAlgorithm);
        } // CanConvert()

        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS

        /// <summary>
        /// Converts the enums value to a matching string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A string.</returns>
        private static string EnumToString(ChecksumAlgorithm type)
        {
            foreach (var mapEntry in mapChecksumAlgorithms)
            {
                if (mapEntry.Value == type)
                {
                    return mapEntry.Key;
                } // if
            } // foreach

            throw new ArgumentOutOfRangeException(nameof(type), "Unknown ChecksumAlgorithm");
        } // EnumToString()

        /// <summary>
        /// Converts a strings to the matching enum value.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A <see cref="ChecksumAlgorithm"/> value.</returns>
        private static ChecksumAlgorithm StringToEnum(string text)
        {
            if (mapChecksumAlgorithms.ContainsKey(text))
            {
                return mapChecksumAlgorithms[text];
            } // if

            throw new ArgumentOutOfRangeException(nameof(text), "Unknown ChecksumAlgorithm");
        } // StringToEnum()

        /// <summary>
        /// Initializes the map relationship types.
        /// </summary>
        private static void InitializeMapChecksumAlgorithms()
        {
            mapChecksumAlgorithms = new Dictionary<string, ChecksumAlgorithm>
            {
                { "MD5", ChecksumAlgorithm.MD5 },
                { "SHA1", ChecksumAlgorithm.SHA1 },
                { "SHA224", ChecksumAlgorithm.SHA224 },
                { "SHA256", ChecksumAlgorithm.SHA256 },
                { "SHA384", ChecksumAlgorithm.SHA384 },
                { "SHA512", ChecksumAlgorithm.SHA512 },
                { "BLAKE3", ChecksumAlgorithm.BLAKE3 },
                { "ADLER32", ChecksumAlgorithm.ADLER32 },
                { "SHA3-256", ChecksumAlgorithm.SHA3_256 },
                { "SHA3-384", ChecksumAlgorithm.SHA3_384 },
                { "SHA3-512", ChecksumAlgorithm.SHA3_512 },
                { "BLAKE2b-256", ChecksumAlgorithm.BLAKE2b_256 },
                { "BLAKE2b-384", ChecksumAlgorithm.BLAKE2b_384 },
                { "BLAKE2b-512", ChecksumAlgorithm.BLAKE2b_512 },
                { "MD2", ChecksumAlgorithm.MD2 },
                { "MD4", ChecksumAlgorithm.MD4 },
                { "MD6", ChecksumAlgorithm.MD6 },
            };
        } // InitializeMapChecksumAlgorithms()
        #endregion PRIVATE METHODS
    } // ChecksumAlgorithmConverter
}
