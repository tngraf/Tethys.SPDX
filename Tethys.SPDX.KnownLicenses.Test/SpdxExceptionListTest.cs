// ---------------------------------------------------------------------------
// <copyright file="SpdxExceptionListTest.cs" company="Tethys">
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Text.Json.Serialization.Metadata;
    using System.Text.Json;

    /// <summary>
    /// Unit tests.
    /// </summary>
    [TestClass]
    public class SpdxExceptionListTest
    {
        /// <summary>
        /// Unit tests.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            // this is just to have a better code coverage
            var obj = new SpdxExceptionList();
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
              ""exceptions"": [],
              ""releaseDate"": ""2023-10-05""
            }";

            var obj = JsonSerializer.Deserialize<SpdxExceptionList>(JsonText);
            Assert.IsNotNull(obj);
            Assert.AreEqual("3.22", obj.LicenseListVersion);
            Assert.AreEqual("2023-10-05", obj.ReleaseDate);
            Assert.IsNotNull(obj.Exceptions);
            Assert.AreEqual(0, obj.Exceptions.Count);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeserializeFromString()
        {
            const string JsonText = @"{
              ""licenseListVersion"": ""3.22"",
              ""exceptions"": [
                {
                      ""reference"": ""./389-exception.json"",
                      ""isDeprecatedLicenseId"": false,
                      ""detailsUrl"": ""./389-exception.html"",
                      ""referenceNumber"": 15,
                      ""name"": ""389 Directory Server Exception"",
                      ""licenseExceptionId"": ""389-exception"",
                      ""seeAlso"": [
                        ""http://directory.fedoraproject.org/wiki/GPL_Exception_License_Text"",
                        ""https://web.archive.org/web/20080828121337/http://directory.fedoraproject.org/wiki/GPL_Exception_License_Text""
                      ]
                    }
                ],
              ""releaseDate"": ""2023-10-05""
            }";

            var options = new JsonSerializerOptions
            {
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers =
                    {
                        TypeResolvers.GetSpdxExceptionListEntryTypeResolvers(),
                    },
                },
            };

            var obj = JsonSerializer.Deserialize<SpdxExceptionList>(JsonText, options);
            Assert.IsNotNull(obj);
            Assert.AreEqual("3.22", obj.LicenseListVersion);
            Assert.AreEqual("2023-10-05", obj.ReleaseDate);
            Assert.IsNotNull(obj.Exceptions);
            Assert.AreEqual(1, obj.Exceptions.Count);

            var entry = obj.Exceptions[0];
            Assert.AreEqual("./389-exception.json", entry.ReferenceFile);
            Assert.AreEqual(false, entry.IsDeprecatedLicenseId);
            Assert.AreEqual("./389-exception.html", entry.DetailsUrl);
            Assert.AreEqual(15, entry.ReferenceNumber);
            Assert.AreEqual("389 Directory Server Exception", entry.Name);
            Assert.AreEqual("389-exception", entry.LicenseExceptionId);
            Assert.IsNotNull(entry.SeeAlso);
            Assert.AreEqual(2, entry.SeeAlso.Count);
            Assert.AreEqual("http://directory.fedoraproject.org/wiki/GPL_Exception_License_Text", entry.SeeAlso[0]);
            Assert.AreEqual("https://web.archive.org/web/20080828121337/http://directory.fedoraproject.org/wiki/GPL_Exception_License_Text", entry.SeeAlso[1]);
        }
    }
}
