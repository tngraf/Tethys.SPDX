// ---------------------------------------------------------------------------
// <copyright file="SpdxDocumentRefConverter.cs" company="Tethys">
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

    using Newtonsoft.Json;

    /// <summary>
    /// Converts a SpdxDocument to its document namespace.
    /// </summary>
    public class SpdxDocumentRefConverter : JsonConverter
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the license manager.
        /// </summary>
        public static IDataManager DataManager { get; set; }
        #endregion // PUBLIC PROPERTIES

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
            var spdxDoc = (SpdxDocument)value;
            writer.WriteValue(spdxDoc.SpdxDocumentNamespace);
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
                throw new ArgumentOutOfRangeException("SPDX element reference missing (must not be empty!)");
            } // if

            if (token == Constants.None)
            {
                return null;
            } // if

            var spdxDoc = new SpdxDocument();

            if (token == Constants.NoAssertion)
            {
                spdxDoc.SpdxIdentifier = Constants.NoAssertion;
                return spdxDoc;
            } // if

            if ((DataManager.SpdxParsingOptions != null) && DataManager.SpdxParsingOptions.LoadExternalDocuments)
            {
                throw new NotSupportedException("Loading externally referenced documents not yet supported");
            } // if

            // no further analysis possible
            spdxDoc.SpdxIdentifier = token;
            return spdxDoc;
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
            return objectType == typeof(SpdxDocument);
        } // CanConvert()
        #endregion // PUBLIC METHODS
    } // SpdxDocumentRefConverter
}
