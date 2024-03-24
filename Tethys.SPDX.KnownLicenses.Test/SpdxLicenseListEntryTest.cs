// ---------------------------------------------------------------------------
// <copyright file="SpdxLicenseListEntryTest.cs" company="Tethys">
//   Copyright (C) 2022-2024 T. Graf
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

namespace Tethys.SPDX.KnownLicenses.Test
{
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Text.Json.Serialization.Metadata;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tethys.SPDX.Interfaces;

    /// <summary>
    /// Unit tests.
    /// </summary>
    [TestClass]
    public class SpdxLicenseListEntryTest
    {
        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            // this is just to have a better code coverage
            var obj = new SpdxLicenseListEntry();
            var actual = obj.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(actual));
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeserializeFromString()
        {
            const string JsonText = @"{
              ""reference"": ""https://spdx.org/licenses/0BSD.html"",
              ""isDeprecatedLicenseId"": false,
              ""detailsUrl"": ""https://spdx.org/licenses/0BSD.json"",
              ""referenceNumber"": 244,
              ""name"": ""BSD Zero Clause License"",
              ""licenseId"": ""0BSD"",
              ""seeAlso"": [
                ""http://landley.net/toybox/license.html"",
                ""https://opensource.org/licenses/0BSD""
              ],
              ""isOsiApproved"": true
            }";
            var obj = JsonSerializer.Deserialize<SpdxLicenseListEntry>(JsonText);
            Assert.IsNotNull(obj);
            Assert.AreEqual("https://spdx.org/licenses/0BSD.html", obj.ReferenceFile);
            Assert.AreEqual(false, obj.IsDeprecatedLicenseId);
            Assert.AreEqual("https://spdx.org/licenses/0BSD.json", obj.DetailsUrl);
            Assert.AreEqual(244, obj.ReferenceNumber);
            Assert.AreEqual("BSD Zero Clause License", obj.Name);
            Assert.AreEqual("0BSD", obj.LicenseId);
            Assert.IsNotNull(obj.SeeAlso);
            Assert.AreEqual(2, obj.SeeAlso.Count);
            Assert.AreEqual("http://landley.net/toybox/license.html", obj.SeeAlso[0]);
            Assert.AreEqual("https://opensource.org/licenses/0BSD", obj.SeeAlso[1]);
            Assert.AreEqual(true, obj.IsOsiApproved);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeserializeFromStringToInterface()
        {
            const string JsonText = @"{
              ""isDeprecatedLicenseId"": false,
              ""detailsUrl"": ""https://spdx.org/licenses/0BSD.json"",
              ""reference"": ""https://spdx.org/licenses/0BSD.html"",
              ""referenceNumber"": 244,
              ""name"": ""BSD Zero Clause License"",
              ""licenseId"": ""0BSD"",
              ""seeAlso"": [
                ""http://landley.net/toybox/license.html"",
                ""https://opensource.org/licenses/0BSD""
              ],
              ""isOsiApproved"": true
            }";

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                IgnoreReadOnlyFields = true,
                IgnoreReadOnlyProperties = true,
                PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
                // UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow, // only for debugging
                ReferenceHandler = null,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers = { TypeResolvers.GetSpdxLicenseListEntryTypeResolvers() }
                }
            };

            var obj = JsonSerializer.Deserialize<ISpdxLicenseListEntry>(JsonText, options);
            Assert.IsNotNull(obj);

            // Deserialization works only for the property name "Reference".
            // For all other names it fails (via the interface, but not via the class),
            // when the JSON property name has not been defined in the *INTERFACE*.
            Assert.AreEqual("https://spdx.org/licenses/0BSD.html", obj.ReferenceFile);
            Assert.AreEqual(false, obj.IsDeprecatedLicenseId);
            Assert.AreEqual("https://spdx.org/licenses/0BSD.json", obj.DetailsUrl);
            Assert.AreEqual(244, obj.ReferenceNumber);
            Assert.AreEqual("BSD Zero Clause License", obj.Name);
            Assert.AreEqual("0BSD", obj.LicenseId);
            Assert.IsNotNull(obj.SeeAlso);
            Assert.AreEqual(2, obj.SeeAlso.Count);
            Assert.AreEqual("http://landley.net/toybox/license.html", obj.SeeAlso[0]);
            Assert.AreEqual("https://opensource.org/licenses/0BSD", obj.SeeAlso[1]);
            Assert.AreEqual(true, obj.IsOsiApproved);
        }
    }
}
