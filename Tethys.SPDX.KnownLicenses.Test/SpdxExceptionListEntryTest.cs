// ---------------------------------------------------------------------------
// <copyright file="SpdxExceptionListEntryTest.cs" company="Tethys">
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
    public class SpdxExceptionListEntryTest
    {
        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            // this is just to have a better code coverage
            var obj = new SpdxExceptionListEntry();
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
            }";
            var obj = JsonSerializer.Deserialize<SpdxExceptionListEntry>(JsonText);
            Assert.IsNotNull(obj);
            Assert.AreEqual("./389-exception.json", obj.ReferenceFile);
            Assert.AreEqual(false, obj.IsDeprecatedLicenseId);
            Assert.AreEqual("./389-exception.html", obj.DetailsUrl);
            Assert.AreEqual(15, obj.ReferenceNumber);
            Assert.AreEqual("389 Directory Server Exception", obj.Name);
            Assert.AreEqual("389-exception", obj.LicenseExceptionId);
            Assert.IsNotNull(obj.SeeAlso);
            Assert.AreEqual(2, obj.SeeAlso.Count);
            Assert.AreEqual("http://directory.fedoraproject.org/wiki/GPL_Exception_License_Text", obj.SeeAlso[0]);
            Assert.AreEqual("https://web.archive.org/web/20080828121337/http://directory.fedoraproject.org/wiki/GPL_Exception_License_Text", obj.SeeAlso[1]);
        }

        /// <summary>
        /// Unit test.
        /// </summary>
        [TestMethod]
        public void TestDeserializeFromStringToInterface()
        {
            const string JsonText = @"{
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
            }";

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                IgnoreReadOnlyFields = true,
                IgnoreReadOnlyProperties = true,
                PreferredObjectCreationHandling = JsonObjectCreationHandling.Populate,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow, // only for debugging
                ReferenceHandler = null,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers = { TypeResolvers.GetSpdxExceptionListEntryTypeResolvers() }
                }
            };

            var obj = JsonSerializer.Deserialize<ISpdxExceptionListEntry>(JsonText, options);
            Assert.IsNotNull(obj);
            Assert.AreEqual("./389-exception.json", obj.ReferenceFile);
            Assert.AreEqual(false, obj.IsDeprecatedLicenseId);
            Assert.AreEqual("./389-exception.html", obj.DetailsUrl);
            Assert.AreEqual(15, obj.ReferenceNumber);
            Assert.AreEqual("389 Directory Server Exception", obj.Name);
            Assert.AreEqual("389-exception", obj.LicenseExceptionId);
            Assert.IsNotNull(obj.SeeAlso);
            Assert.AreEqual(2, obj.SeeAlso.Count);
            Assert.AreEqual("http://directory.fedoraproject.org/wiki/GPL_Exception_License_Text", obj.SeeAlso[0]);
            Assert.AreEqual("https://web.archive.org/web/20080828121337/http://directory.fedoraproject.org/wiki/GPL_Exception_License_Text", obj.SeeAlso[1]);
        }
    }
}
