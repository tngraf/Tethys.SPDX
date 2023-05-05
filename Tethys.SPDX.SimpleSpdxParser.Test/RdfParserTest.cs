// ---------------------------------------------------------------------------
// <copyright file="RdfParserTest.cs" company="Tethys">
//   Copyright (C) 2022-2023 T. Graf
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

namespace Tethys.SPDX.SimpleSpdxParser.Test
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tethys.SimpleSpdxParser;
    using Tethys.SPDX.KnownLicenses;
    using Tethys.SPDX.Model;
    using Tethys.SPDX.Model.License;

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
        /// Another test SPDX file.
        /// </summary>
        private const string SpdxFile3 = @"..\..\..\..\Testdata\yaml-0.1.0.zip.spdx2.rdf.xml";

        /// <summary>
        /// Another test SPDX file.
        /// </summary>
        private const string SpdxFile4 = @"..\..\..\..\Testdata\xz-0.5.8.spdx2.3.rdf.xml";

        /// <summary>
        /// Another test SPDX file.
        /// </summary>
        private const string SpdxFile5 = @"..\..\..\..\Testdata\xz-0.5.8.spdx2.3_concluded.spdx.rdf.xml";

        /// <summary>
        /// Another test SPDX file.
        /// </summary>
        private const string SpdxFile6 = @"..\..\..\..\Testdata\xz-0.5.8.spdx2.3_concluded_attribution.spdx.rdf.xml";

        /// <summary>
        /// Gets an initialized instance of a known license manager.
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

        [TestMethod]
        public void TestReadFromFile2Success()
        {
            // test with an SPDX 2.3 file generated by FOSSology
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new RdfParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromFile(SpdxFile3);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual(0, spdxDoc.Annotations.Count);
            Assert.AreEqual(0, spdxDoc.ExternalDocumentRefs.Count);
            Assert.AreEqual(2, spdxDoc.ExtractedLicenseInfos.Count);

            Assert.AreEqual("BSD-3-Clause_MIPS Technologies", spdxDoc.ExtractedLicenseInfos[0].Name);
            Assert.AreEqual("LicenseRef-fossology-BSD-3-Clause_MIPS-Technologies", spdxDoc.ExtractedLicenseInfos[0].Id);

            Assert.AreEqual("BSD", spdxDoc.ExtractedLicenseInfos[1].Name);
            Assert.AreEqual("LicenseRef-fossology-BSD", spdxDoc.ExtractedLicenseInfos[1].Id);

            Assert.AreEqual(1, spdxDoc.RelationShips.Count);
            Assert.AreEqual(0, spdxDoc.Reviewers.Count);
            Assert.AreEqual("http://stage.fossology.siemens.com/repo/SPDX2_yaml-0.1.0.zip_1680269421.spdx.rdf", spdxDoc.SpdxDocumentNamespace);
        }

        [TestMethod]
        public void TestReadFromFile3Success()
        {
            // test with an SPDX 2.3 file generated by FOSSology
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new RdfParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromFile(SpdxFile4);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual(0, spdxDoc.Annotations.Count);
            Assert.AreEqual(0, spdxDoc.ExternalDocumentRefs.Count);
            Assert.AreEqual(4, spdxDoc.ExtractedLicenseInfos.Count);
            Assert.AreEqual(1, spdxDoc.RelationShips.Count);
            var package = spdxDoc.RelationShips[0].ReleatedElement as SpdxPackage;
            Assert.IsNotNull(package);
            Assert.AreEqual(111, package.Files.Count);
            // xz-0.5.8/LICENSE
            Assert.AreEqual(2, package.Files[0].LicenseInfoFromFiles.Count);
            var license = package.Files[0].LicenseInfoFromFiles[0] as ListedLicenseInfo;
            Assert.IsNotNull(license);
            Assert.AreEqual("BSD-3-Clause", license.Id);

            var license2 = package.Files[0].LicenseInfoFromFiles[1] as ExtractedLicenseInfo;
            Assert.IsNotNull(license2);
            Assert.AreEqual("LicenseRef-fossology-BSD-3-Clause-Pierre-Olivier", license2.Id);

            // xz-0.5.8/cmd/gxz/licenses.go
            // must contain a listed license reference
            Assert.AreEqual(3, package.Files[27].LicenseInfoFromFiles.Count);
            license = package.Files[27].LicenseInfoFromFiles[0] as ListedLicenseInfo;
            Assert.IsNotNull(license);
            Assert.AreEqual("BSD-3-Clause", license.Id);

            Assert.AreEqual(1, spdxDoc.RelationShips.Count);
            Assert.AreEqual(0, spdxDoc.Reviewers.Count);
            Assert.AreEqual("http://stage.fossology.siemens.com/repo/SPDX2_xz-0.5.8.zip_1680261989.spdx.rdf", spdxDoc.SpdxDocumentNamespace);
        }

        [TestMethod]
        public void TestReadFromFile4Success()
        {
            // test with an SPDX 2.3 file generated by FOSSology
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new RdfParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromFile(SpdxFile5);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual(0, spdxDoc.Annotations.Count);
            Assert.AreEqual(0, spdxDoc.ExternalDocumentRefs.Count);
            Assert.AreEqual(4, spdxDoc.ExtractedLicenseInfos.Count);
            Assert.AreEqual(1, spdxDoc.RelationShips.Count);
            var package = spdxDoc.RelationShips[0].ReleatedElement as SpdxPackage;
            Assert.IsNotNull(package);
            Assert.AreEqual(111, package.Files.Count);

            // xz-0.5.8/cmd/gxz/licenses.go
            Assert.AreEqual(3, package.Files[0].LicenseInfoFromFiles.Count);
            var license = package.Files[0].LicenseInfoFromFiles[0] as ListedLicenseInfo;
            Assert.IsNotNull(license);
            Assert.AreEqual("BSD-3-Clause", license.Id);

            var license2 = package.Files[0].LicenseInfoFromFiles[1] as ExtractedLicenseInfo;
            Assert.IsNotNull(license2);
            Assert.AreEqual("LicenseRef-fossology-BSD-3-Clause_MIPS-Technologies", license2.Id);

            license2 = package.Files[0].LicenseInfoFromFiles[2] as ExtractedLicenseInfo;
            Assert.IsNotNull(license2);
            Assert.AreEqual("LicenseRef-fossology-BSD-3-Clause-Pierre-Olivier", license2.Id);

            license = package.Files[0].LicenseConcluded as ListedLicenseInfo;
            Assert.IsNotNull(license);
            Assert.AreEqual("BSD-3-Clause-81f1634e09063200aeea05033ac4ac7c", license.Id);
        }

        [TestMethod]
        public void TestReadFromFile5Success()
        {
            // test with an SPDX 2.3 file generated by FOSSology
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new RdfParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromFile(SpdxFile6);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual(0, spdxDoc.Annotations.Count);
            Assert.AreEqual(0, spdxDoc.ExternalDocumentRefs.Count);
            Assert.AreEqual(4, spdxDoc.ExtractedLicenseInfos.Count);
            Assert.AreEqual(1, spdxDoc.RelationShips.Count);
            var package = spdxDoc.RelationShips[0].ReleatedElement as SpdxPackage;
            Assert.IsNotNull(package);
            Assert.AreEqual(111, package.Files.Count);

            // xz-0.5.8/bits.go
            Assert.AreEqual(1, package.Files[0].LicenseInfoFromFiles.Count);
            var exLicense = package.Files[0].LicenseInfoFromFiles[0] as ExtractedLicenseInfo;
            Assert.IsNotNull(exLicense);
            Assert.AreEqual("LicenseRef-fossology-BSD", exLicense.Id);
            Assert.AreEqual("BSD", exLicense.Name);

            var llicense = package.Files[0].LicenseConcluded as ListedLicenseInfo;
            Assert.IsNotNull(llicense);
            Assert.AreEqual("BSD-3-Clause-871ee238618e143aa808dee254a44d3b", llicense.Id);

            Assert.IsNotNull(package.Files[0].AttributionText);
            Assert.AreEqual("Some dummy acknowledgement", package.Files[0].AttributionText);

            // xz-0.5.8/cmd/gxz/licenses.go
            Assert.AreEqual(3, package.Files[1].LicenseInfoFromFiles.Count);
            llicense = package.Files[1].LicenseInfoFromFiles[0] as ListedLicenseInfo;
            Assert.IsNotNull(llicense);
            Assert.AreEqual("BSD-3-Clause", llicense.Id);

            llicense = package.Files[1].LicenseInfoFromFiles[0] as ListedLicenseInfo;
            Assert.IsNotNull(llicense);
            Assert.AreEqual("LicenseRef-fossology-BSD", exLicense.Id);

            exLicense = package.Files[1].LicenseInfoFromFiles[1] as ExtractedLicenseInfo;
            Assert.IsNotNull(exLicense);
            Assert.AreEqual("LicenseRef-fossology-BSD-3-Clause_MIPS-Technologies", exLicense.Id);

            exLicense = package.Files[1].LicenseInfoFromFiles[2] as ExtractedLicenseInfo;
            Assert.IsNotNull(exLicense);
            Assert.AreEqual("LicenseRef-fossology-BSD-3-Clause-Pierre-Olivier", exLicense.Id);

            llicense = package.Files[1].LicenseConcluded as ListedLicenseInfo;
            Assert.IsNotNull(llicense);
            Assert.AreEqual("LicenseRef-fossology-BSD-3-Clause-Pierre-Olivier", exLicense.Id);
        }

        [TestMethod]
        public void TestReadFromFileXSuccess()
        {
            // test with an SPDX 2.3 file generated by FOSSology
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new RdfParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromFile(
                @"D:\OneDrive - Siemens AG\Documents\SSP\SW Clearing\Vorbereitet\_puppeteer, 19.9.0\puppeteer-puppeteer-v19.9.0.zip.spdx2.rdf.xml");
            Assert.IsNotNull(spdxDoc);
        }
    }
}
