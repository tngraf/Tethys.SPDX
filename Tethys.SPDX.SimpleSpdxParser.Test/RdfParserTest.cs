// ---------------------------------------------------------------------------
// <copyright file="RdfParserTest.cs" company="Tethys">
//   Copyright (C) 2022 T. Graf
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tethys.SimpleSpdxParser.Test
{
    using System.IO;

    using Tethys.SPDX.KnownLicenses;

    [TestClass]
    public class RdfParserTest
    {
        /// <summary>
        /// The expected data path for the SPDX license data.
        /// </summary>
        private const string ExpectedDataPath = @"..\..\..\..\license-list-data";

        /// <summary>
        /// A test SPDX file.
        /// </summary>
        private const string SpdxFile1 = @"..\..\..\..\Testdata\ngx-logger-master.zip.spdx2.rdf.xml";

        /// <summary>
        /// Another test SPDX file.
        /// </summary>
        private const string SpdxFile2 = @"..\..\..\..\Testdata\SPDXRdfExample-v2.2.spdx.rdf.xml";

        /// <summary>
        /// Gets an initilaized instance of a known license manager.
        /// </summary>
        /// <returns>A <see cref="KnownLicenseManager"/>.</returns>
        private static KnownLicenseManager GetKnownLicenseManager()
        {
            // initialize (known) licenses.
            var knownLicenseManager = new KnownLicenseManager();

            var detailsFolder = Path.Combine(ExpectedDataPath, "details");
            knownLicenseManager.LoadSpdxSourceFiles(detailsFolder);
            Assert.IsTrue(knownLicenseManager.Licenses.Count > 400);

            var exceptionsFolder = Path.Combine(ExpectedDataPath, "exceptions");
            knownLicenseManager.LoadSpdxSourceFiles(exceptionsFolder);
            Assert.IsTrue(knownLicenseManager.Licenses.Count > 430);

            return knownLicenseManager;
        } // GetKnownLicenseManager()

        [TestMethod]
        public void TestReadFromFileSuccess()
        {
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new RdfParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromFile(SpdxFile1);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual(0, spdxDoc.Annotations.Count);
            Assert.AreEqual(0, spdxDoc.ExternalDocumentRefs.Count);
            Assert.AreEqual(2, spdxDoc.ExtractedLicenseInfos.Count);
            Assert.AreEqual("No_license_found", spdxDoc.ExtractedLicenseInfos[0].Name);
            Assert.AreEqual("LicenseRef-No_license_found", spdxDoc.ExtractedLicenseInfos[0].Id);

            Assert.AreEqual("MIT License", spdxDoc.ExtractedLicenseInfos[1].Name);
            Assert.AreEqual("LicenseRef-MIT", spdxDoc.ExtractedLicenseInfos[1].Id);

            Assert.AreEqual(1, spdxDoc.RelationShips.Count);
            Assert.AreEqual(0, spdxDoc.Reviewers.Count);
            Assert.AreEqual("http://fossology.siemens.com/repo/SPDX2_ngx-logger-master.zip_1644217891-spdx.rdf", spdxDoc.SpdxDocumentNamespace);
        }

        [TestMethod]
        public void TestReadFromFileCheckExtras2()
        {
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new RdfParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromFile(SpdxFile2);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual(3, spdxDoc.Annotations.Count);
            Assert.AreEqual("Person: Joe Reviewer", spdxDoc.Annotations[0].Annotator);
            Assert.IsTrue(spdxDoc.Annotations[0].Comment.StartsWith("This is just an example"));
            Assert.IsNotNull(spdxDoc.Snippet);
            Assert.IsNotNull(spdxDoc.Snippet.LineRange);
            Assert.IsNotNull(spdxDoc.Snippet.LineRange.EndPointer);
        }
    }
}
