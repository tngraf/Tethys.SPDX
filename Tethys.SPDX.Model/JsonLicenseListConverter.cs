// ---------------------------------------------------------------------------
// <copyright file="JsonLicenseListConverter.cs" company="Tethys">
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
    using Tethys.SPDX.Model.License;

    /// <summary>
    /// Converts a license list to the SPDX JSON specific information.
    /// </summary>
    public class JsonLicenseListConverter : JsonConverter
    {
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = (IReadOnlyList<AnyLicenseInfo>)value;
            if (list == null)
            {
                return;
            } // if

            writer.WriteStartArray();

            foreach (var licenseInfo in list)
            {
                JsonLicenseConverter.WriteLicenseToJson(writer, licenseInfo);
#if false
                if (licenseInfo is ConjunctiveLicenseSet cl)
                {
                    writer.WriteValue(cl.ToString());
                    continue;
                } // if

                if (licenseInfo is DisjunctiveLicenseSet dl)
                {
                    writer.WriteValue(dl.ToString());
                    continue;
                } // if

                if (licenseInfo is ListedLicenseInfo ll)
                {
                    writer.WriteValue(ll.Id);
                } // if
#endif
            } // foreach

            writer.WriteEndArray();

            ////writer.WriteValue(SpdxNoneLicense.NoLicenseName);
            ////writer.WriteValue(SpdxNoAssertionLicense.NoAssertionLicenseName);
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
            var result = new List<AnyLicenseInfo>();

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
                        var l = JsonLicenseConverter.LicenseExpressionToLicenseObject(licenseText);
                        if (l != null)
                        {
                            result.Add(l);
                            continue;
                        } // if

                        throw new ArgumentOutOfRangeException($"Unknown SPDX license statement: {licenseText}");
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
            return objectType == typeof(IReadOnlyList<AnyLicenseInfo>);
        } // CanConvert()
    } // JsonLicenseListConverter
}
