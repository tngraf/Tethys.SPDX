// ---------------------------------------------------------------------------
// <copyright file="SpdxLicenseListInfoTest.cs" company="Tethys">
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
    public class SpdxLicenseListInfoTest
    {
        /// <summary>
        /// Unit tests.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            // this is just to have a better code coverage
            var obj = new SpdxLicenseListInfo();
            var actual = obj.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(actual));
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeserializeFromStringEmptyList()
        {
            const string JsonText = @"{
              ""licenseListVersion"": ""3.22"",
              ""licenses"": [],
              ""releaseDate"": ""2023-10-05""
            }";

            var obj = JsonSerializer.Deserialize<SpdxLicenseListInfo>(JsonText);
            Assert.IsNotNull(obj);
            Assert.AreEqual("3.22", obj.LicenseListVersion);
            Assert.AreEqual("2023-10-05", obj.ReleaseDate);
            Assert.IsNotNull(obj.Licenses);
            Assert.AreEqual(0, obj.Licenses.Count);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeserializeFromString()
        {
            const string JsonText = @"{
              ""licenseListVersion"": ""3.22"",
              ""licenses"": [
                {
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
                    }
                ],
              ""releaseDate"": ""2023-10-05""
            }";

            var options = new JsonSerializerOptions
            {
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers = { TypeResolvers.GetSpdxLicenseListEntryTypeResolvers() }
                }
            };

            var obj = JsonSerializer.Deserialize<SpdxLicenseListInfo>(JsonText, options);
            Assert.IsNotNull(obj);
            Assert.AreEqual("3.22", obj.LicenseListVersion);
            Assert.AreEqual("2023-10-05", obj.ReleaseDate);
            Assert.IsNotNull(obj.Licenses);
            Assert.AreEqual(1, obj.Licenses.Count);

            var entry = obj.Licenses[0];
            Assert.AreEqual("https://spdx.org/licenses/0BSD.html", entry.ReferenceFile);
            Assert.AreEqual(false, entry.IsDeprecatedLicenseId);
            Assert.AreEqual("https://spdx.org/licenses/0BSD.json", entry.DetailsUrl);
            Assert.AreEqual(244, entry.ReferenceNumber);
            Assert.AreEqual("BSD Zero Clause License", entry.Name);
            Assert.AreEqual("0BSD", entry.LicenseId);
            Assert.IsNotNull(entry.SeeAlso);
            Assert.AreEqual(2, entry.SeeAlso.Count);
            Assert.AreEqual("http://landley.net/toybox/license.html", entry.SeeAlso[0]);
            Assert.AreEqual("https://opensource.org/licenses/0BSD", entry.SeeAlso[1]);
            Assert.AreEqual(true, entry.IsOsiApproved);
        }
    }
}
