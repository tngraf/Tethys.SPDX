// ---------------------------------------------------------------------------
// <copyright file="PointerConverter.cs" company="Tethys">
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
    using Tethys.SPDX.Model.Pointer;

    /// <summary>
    /// Converts a list of file types to its correct string value.
    /// </summary>
    public class PointerConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // default handling
            serializer.Serialize(writer, value);
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
            if (reader.TokenType == JsonToken.StartObject)
            {
                reader.Read();
            } // if

            if (reader.TokenType != JsonToken.PropertyName)
            {
                throw new JsonSerializationException("Invalid SPDX range value");
            } // if

            var propertyName = (string)reader.Value;
            reader.Read();
            if (propertyName == "lineNumber")
            {
                // parse LineCharPointer
                var result = new LineCharPointer();
                if (reader.Value == null)
                {
                    throw new JsonSerializationException("Invalid SPDX range value");
                } // if

                if (reader.TokenType != JsonToken.Integer)
                {
                    throw new JsonSerializationException("Invalid SPDX range value");
                } // if

                // ReSharper disable once PossibleInvalidCastException
                result.LineNumber = (int)(long)reader.Value;
                reader.Read();
                reader.Read();
                result.Reference = new SpdxElement();
                result.Reference.SpdxIdentifier = (string)reader.Value;

                reader.Read(); // move to next token

                return result;
            }
            else
            {
                // parse ByteOffsetPointer
                var result = new ByteOffsetPointer();
                if (reader.Value == null)
                {
                    throw new JsonSerializationException("Invalid SPDX range value");
                } // if

                if (reader.TokenType != JsonToken.Integer)
                {
                    throw new JsonSerializationException("Invalid SPDX range value");
                } // if

                // ReSharper disable once PossibleInvalidCastException
                result.Offset = (int)(long)reader.Value;
                reader.Read();
                reader.Read();
                result.Reference = new SpdxElement();
                result.Reference.SpdxIdentifier = (string)reader.Value;
                reader.Read(); // move to next token

                return result;
            } // if
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
            return objectType == typeof(SinglePointer);
        } // CanConvert()
    } // PointerConverter
}
