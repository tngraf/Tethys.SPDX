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

    /// <summary>
    /// Converts a list of file types to its correct string value.
    /// </summary>
    public class FileTypeConverter : JsonConverter
    {
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
                switch (ft)
                {
                    case FileType.Source:
                        writer.WriteValue("SOURCE");
                        continue;
                    case FileType.Binary:
                        writer.WriteValue("BINARY");
                        continue;
                    case FileType.Other:
                        writer.WriteValue("OTHER");
                        continue;
                    case FileType.Archive:
                        writer.WriteValue("ARCHIVE");
                        continue;
                    case FileType.Application:
                        writer.WriteValue("APPLICATION");
                        continue;
                    case FileType.Audio:
                        writer.WriteValue("AUDIO");
                        continue;
                    case FileType.Image:
                        writer.WriteValue("IMAGE");
                        continue;
                    case FileType.Text:
                        writer.WriteValue("TEXT");
                        continue;
                    case FileType.Video:
                        writer.WriteValue("VIDEO");
                        continue;
                    case FileType.Documentation:
                        writer.WriteValue("DOCUMENTATION");
                        continue;
                    case FileType.Spdx:
                        writer.WriteValue("SPDX");
                        continue;
                } // switch

                throw new NotSupportedException("Unknown FileType value!");
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
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON; otherwise, <c>false</c>.
        /// </value>
        public override bool CanRead
        {
            get { return false; }
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
        }
    } // FileTypeConverter
}
