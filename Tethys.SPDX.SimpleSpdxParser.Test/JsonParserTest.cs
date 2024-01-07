// ---------------------------------------------------------------------------
// <copyright file="JsonParserTest.cs" company="Tethys">
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

namespace Tethys.SPDX.SimpleSpdxParser.Test
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Tethys.SPDX.KnownLicenses;
    using Tethys.SPDX.Model;
    using Tethys.SPDX.Model.License;
    using Tethys.SPDX.Model.Pointer;

    [TestClass]
    public class JsonParserTest
    {
        /// <summary>
        /// The expected data path for the SPDX license data.
        /// </summary>
        private const string ExpectedDataPath = @"..\..\..\..\license-list-data";

        /// <summary>
        /// A test SPDX file.
        /// </summary>
        private const string JsonFileSpdxDocument = @"..\..\..\..\Testdata\only_document.spdx.json";

        /// <summary>
        /// Gets an initialized instance of a known license manager.
        /// </summary>
        /// <returns>A <see cref="KnownLicenseManager"/>.</returns>
        private static KnownLicenseManager GetKnownLicenseManager()
        {
            // initialize (known) licenses.
            var knownLicenseManager = new KnownLicenseManager();

            var licensesFile = Path.Combine(ExpectedDataPath, "licenses.json");
            knownLicenseManager.LoadSpdxLicenseList(licensesFile);

            var detailsFolder = Path.Combine(ExpectedDataPath, "details");
            knownLicenseManager.LoadSpdxSourceFiles(detailsFolder);

            var exceptionsFolder = Path.Combine(ExpectedDataPath, "exceptions");
            knownLicenseManager.LoadSpdxExceptionFiles(exceptionsFolder);

            return knownLicenseManager;
        } // GetKnownLicenseManager()

        [TestMethod]
        public void TestReadLicenseFromSpdxIdSuccess()
        {
            const string Json = "{ \"dataLicense\": \"CC0-1.0\" }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);

            var license = spdxDoc.DataLicense as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("CC0-1.0", license.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", license.Name);
            Assert.IsNotNull(license.LicenseText);
        }

        [TestMethod]
        public void TestReadNoneLicenseFromIdSuccess()
        {
            const string Json = "{ \"dataLicense\": \"NONE\" }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);

            var license = spdxDoc.DataLicense as SpdxNoneLicense;
            Assert.IsNotNull(license);
        }

        [TestMethod]
        public void TestReadNoAssertionLicenseSuccess()
        {
            const string Json = "{ \"dataLicense\": \"NOASSERTION\" }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);

            var license = spdxDoc.DataLicense as SpdxNoAssertionLicense;
            Assert.IsNotNull(license);
        }

        [TestMethod]
        public void TestReadLicenseRefSuccess()
        {
            const string Json = "{ \"dataLicense\": \"CC0-1.0\" }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);

            var license = spdxDoc.DataLicense as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("CC0-1.0", license.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", license.Name);
            Assert.IsNotNull(license.LicenseText);
        }

        [TestMethod]
        public void TestReadLicenseExpressionSuccess()
        {
            const string Json = "{ \"dataLicense\": \"CC0-1.0\" }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);

            var license = spdxDoc.DataLicense as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("CC0-1.0", license.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", license.Name);
            Assert.IsNotNull(license.LicenseText);
        }

        [TestMethod]
        public void TestReadSimpleSpdxDocumentSuccess()
        {
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromFile(JsonFileSpdxDocument);
            Assert.IsNotNull(spdxDoc);
            Assert.IsNotNull(spdxDoc.CreationInfo);
            Assert.AreEqual(3, spdxDoc.CreationInfo.Creators.Count);
            Assert.AreEqual("Organization: Siemens", spdxDoc.CreationInfo.Creators[0]);
            Assert.AreEqual("Tool: Software Clearing Workbench", spdxDoc.CreationInfo.Creators[1]);
            Assert.AreEqual("Person: Thomas Graf", spdxDoc.CreationInfo.Creators[2]);

            Assert.AreEqual("3.22", spdxDoc.CreationInfo.LicenseListVersion);

            Assert.AreEqual(2024, spdxDoc.CreationInfo.CreatedDate.Year);
            Assert.AreEqual(01, spdxDoc.CreationInfo.CreatedDate.Month);
            Assert.AreEqual(05, spdxDoc.CreationInfo.CreatedDate.Day);
            Assert.AreEqual(09, spdxDoc.CreationInfo.CreatedDate.Hour);
            Assert.AreEqual(55, spdxDoc.CreationInfo.CreatedDate.Minute);
            Assert.AreEqual(11, spdxDoc.CreationInfo.CreatedDate.Second);

            Assert.AreEqual("SPDX-2.3", spdxDoc.SpecVersion);

            Assert.AreEqual("http://spdx.org/spdxdocs/spdx-example-007", spdxDoc.SpdxDocumentNamespace);

            Assert.AreEqual("SPDXRef-DOCUMENT", spdxDoc.SpdxIdentifier);


            var license = spdxDoc.DataLicense as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("CC0-1.0", license.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", license.Name);
            Assert.IsNotNull(license.LicenseText);
        }

        [TestMethod]
        public void TestReadAnnotationSuccess()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""SPDXID"": ""SPDXRef-DOCUMENT"",
                ""annotations"": [
                    {
                      ""annotator"": ""Person: Jane Doe"",
                      ""annotationType"": ""OTHER"",
                      ""annotationDate"": ""2024-01-05T05:14:33Z"",
                      ""comment"": ""Document level annotation""
                    },
                    {
                      ""annotator"": ""Person: Thomas Graf"",
                      ""annotationType"": ""REVIEW"",
                      ""annotationDate"": ""2021-01-05T15:14:53Z"",
                      ""comment"": ""Another annotation""
                    }
                  ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual("SPDXRef-DOCUMENT", spdxDoc.SpdxIdentifier);

            var license = spdxDoc.DataLicense as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("CC0-1.0", license.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", license.Name);
            Assert.IsNotNull(license.LicenseText);

            Assert.IsNotNull(spdxDoc.Annotations);
            Assert.AreEqual(2, spdxDoc.Annotations.Count);

            Assert.AreEqual(AnnotationType.Other, spdxDoc.Annotations[0].AnnotationType);
            Assert.AreEqual("Person: Jane Doe", spdxDoc.Annotations[0].Annotator);
            Assert.AreEqual("Document level annotation", spdxDoc.Annotations[0].Comment);
            Assert.AreEqual(new DateTime(2024, 1, 5, 5, 14, 33), spdxDoc.Annotations[0].Date);

            Assert.AreEqual(AnnotationType.Review, spdxDoc.Annotations[1].AnnotationType);
            Assert.AreEqual("Person: Thomas Graf", spdxDoc.Annotations[1].Annotator);
            Assert.AreEqual("Another annotation", spdxDoc.Annotations[1].Comment);
            Assert.AreEqual(new DateTime(2021, 1, 5, 15, 14, 53), spdxDoc.Annotations[1].Date);
        }

        [TestMethod]
        public void TestReadRelationshipsSuccess()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""SPDXID"": ""SPDXRef-DOCUMENT"",
                ""relationShips"": [
                    {
                      ""relationshipType"": ""DESCRIBES"",
                      ""relatedSpdxElement"": ""SPDXRef-DoapSource"",
                      ""comment"": ""Some comment""
                    },
                    {
                      ""relationshipType"": ""BUILD_DEPENDENCY_OF"",
                      ""relatedSpdxElement"": ""NOASSERTION""
                    }
                  ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual("SPDXRef-DOCUMENT", spdxDoc.SpdxIdentifier);

            var license = spdxDoc.DataLicense as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("CC0-1.0", license.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", license.Name);
            Assert.IsNotNull(license.LicenseText);

            Assert.IsNotNull(spdxDoc.RelationShips);
            Assert.AreEqual(2, spdxDoc.RelationShips.Count);
            Assert.AreEqual(RelationshipType.Describes, spdxDoc.RelationShips[0].Type);
            Assert.AreEqual("SPDXRef-DoapSource", spdxDoc.RelationShips[0].RelatedElement.SpdxIdentifier);
            Assert.AreEqual("Some comment", spdxDoc.RelationShips[0].Comment);

            Assert.AreEqual(RelationshipType.BuildDependencyOf, spdxDoc.RelationShips[1].Type);
            Assert.AreEqual("NOASSERTION", spdxDoc.RelationShips[1].RelatedElement.SpdxIdentifier);
        }

        [TestMethod]
        public void TestReadSnippetSuccess()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""SPDXID"": ""SPDXRef-DOCUMENT"",
                ""snippets"": [
                    {
                      ""snippetFromFile"": ""SPDXRef-DoapSource"",
                      ""ranges"": [
                        {
                          ""endPointer"": {
                            ""lineNumber"": 23,
                            ""reference"": ""SPDXRef-DoapSource""
                          },
                          ""startPointer"": {
                            ""lineNumber"": 5,
                            ""reference"": ""SPDXRef-DoapSource""
                          }
                        },
                        {
                          ""endPointer"": {
                            ""offset"": 420,
                            ""reference"": ""SPDXRef-DoapSource""
                          },
                          ""startPointer"": {
                            ""offset"": 310,
                            ""reference"": ""SPDXRef-DoapSource""
                          }
                        }
                      ],
                      ""licenseInfoInSnippets"": [
                        ""LGPL-2.0-only""
                      ],
                      ""licenseConcluded"": ""MIT"",
                      ""licenseInfoFromFiles"": [],
                      ""copyrightText"": ""Copyright 2008-2010 John Smith"",
                      ""licenseComments"": ""The concluded license was taken from package xyz, from which the snippet was copied into the current file. The concluded license information was found in the COPYING.txt file in package xyz."",
                      ""annotations"": [],
                      ""name"": ""from linux kernel"",
                      ""comment"": ""This snippet was identified as significant and highlighted in this Apache-2.0 file, when a commercial scanner identified it as being derived from file foo.c in package xyz which is licensed under GPL-2.0."",
                      ""relationShips"": [],
                      ""SPDXID"": ""SPDXRef-Snippet""
                    }
                  ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual("SPDXRef-DOCUMENT", spdxDoc.SpdxIdentifier);

            var license = spdxDoc.DataLicense as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("CC0-1.0", license.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", license.Name);
            Assert.IsNotNull(license.LicenseText);

            Assert.IsNotNull(spdxDoc.Snippets);
            Assert.AreEqual(1, spdxDoc.Snippets.Count);
            var snippet = spdxDoc.Snippets[0];
            Assert.IsNotNull(snippet);
            Assert.AreEqual("SPDXRef-Snippet", snippet.SpdxIdentifier);
            Assert.AreEqual("SPDXRef-DoapSource", snippet.SnippetFromFile.SpdxIdentifier);
            Assert.AreEqual(1, snippet.LicenseInfoInSnippet.Count);
            var l1 = snippet.LicenseInfoInSnippet[0] as License;
            Assert.IsNotNull(l1);
            Assert.AreEqual("LGPL-2.0-only", l1.Id);
            var l2 = snippet.LicenseConcluded as License;
            Assert.IsNotNull(l2);
            Assert.AreEqual("MIT", l2.Id);
            Assert.AreEqual(0, snippet.LicenseInfoFromFiles.Count);
            Assert.AreEqual("Copyright 2008-2010 John Smith", snippet.CopyrightText);
            Assert.IsTrue(snippet.Comment.StartsWith("This snippet was identified as significant"));
            Assert.IsTrue(snippet.LicenseComments.StartsWith("The concluded license was taken from package xyz"));
            Assert.AreEqual(0, snippet.Annotations.Count);
            Assert.AreEqual("from linux kernel", snippet.Name);

            Assert.AreEqual(2, snippet.Ranges.Count);
            var lcpStart = snippet.Ranges[0].StartPointer as LineCharPointer;
            Assert.IsNotNull(lcpStart);
            Assert.AreEqual(5, lcpStart.LineNumber);
            Assert.AreEqual("SPDXRef-DoapSource", lcpStart.Reference.SpdxIdentifier);
            var lcpEnd = snippet.Ranges[0].EndPointer as LineCharPointer;
            Assert.IsNotNull(lcpEnd);
            Assert.AreEqual(23, lcpEnd.LineNumber);
            Assert.AreEqual("SPDXRef-DoapSource", lcpEnd.Reference.SpdxIdentifier);

            var boStart = snippet.Ranges[1].StartPointer as ByteOffsetPointer;
            Assert.IsNotNull(boStart);
            Assert.AreEqual(310, boStart.Offset);
            Assert.AreEqual("SPDXRef-DoapSource", boStart.Reference.SpdxIdentifier);
            var boEnd = snippet.Ranges[1].EndPointer as ByteOffsetPointer;
            Assert.IsNotNull(boEnd);
            Assert.AreEqual(420, boEnd.Offset);
            Assert.AreEqual("SPDXRef-DoapSource", boEnd.Reference.SpdxIdentifier);

            Assert.IsNotNull(spdxDoc.RelationShips);
            Assert.AreEqual(0, spdxDoc.RelationShips.Count);
        }

        [TestMethod]
        public void TestReadFileSuccess()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""SPDXID"": ""SPDXRef-DOCUMENT"",
                ""files"": [
                    {
                      ""fileName"": ""./src/org/spdx/parser/DOAPProject.java"",
                      ""fileTypes"": [
                        ""SOURCE"",
                        ""ARCHIVE""
                      ],
                      ""checksums"": [
                        {
                          ""algorithm"": ""SHA1"",
                          ""checksumValue"": ""0123456789012345678901234567890123456789""
                        }
                      ],
                      ""fileContributors"": [
                        ""Protecode Inc."",
                        ""Open Logic Inc.""
                      ],
                      ""noticeText"": ""A notice for this file."",
                      ""licenseConcluded"": ""MIT"",
                      ""licenseInfoFromFiles"": [
                        ""LGPL-2.0-only""
                      ],
                      ""copyrightText"": ""Copyright 2010, 2011 Source Auditor Inc."",
                      ""licenseComments"": ""File license comment"",
                      ""attributionTexts"": [
                        ""File attribution text""
                      ],
                      ""annotations"": [],
                      ""comment"": ""Some file comment"",
                      ""relationShips"": [],
                      ""SPDXID"": ""SPDXRef-DoapSource""
                    }
                  ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual("SPDXRef-DOCUMENT", spdxDoc.SpdxIdentifier);

            var license = spdxDoc.DataLicense as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("CC0-1.0", license.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", license.Name);
            Assert.IsNotNull(license.LicenseText);

            Assert.IsNotNull(spdxDoc.Files);
            Assert.AreEqual(1, spdxDoc.Files.Count);
            var file = spdxDoc.Files[0];
            Assert.IsNotNull(file);
            Assert.AreEqual("SPDXRef-DoapSource", file.SpdxIdentifier);
            Assert.AreEqual("./src/org/spdx/parser/DOAPProject.java", file.FileName);

            Assert.AreEqual(2, file.FileTypes.Count);
            Assert.AreEqual(FileType.Source, file.FileTypes[0]);
            Assert.AreEqual(FileType.Archive, file.FileTypes[1]);

            Assert.AreEqual(1, file.Checksums.Count);
            Assert.AreEqual(ChecksumAlgorithm.SHA1, file.Checksums[0].Algorithm);
            Assert.AreEqual("0123456789012345678901234567890123456789", file.Checksums[0].Value);

            Assert.AreEqual(2, file.FileContributors.Count);
            Assert.AreEqual("Protecode Inc.", file.FileContributors[0]);
            Assert.AreEqual("Open Logic Inc.", file.FileContributors[1]);

            Assert.AreEqual("A notice for this file.", file.NoticeText);

            var l1 = file.LicenseConcluded as License;
            Assert.IsNotNull(l1);
            Assert.AreEqual("MIT", l1.Id);

            Assert.AreEqual(1, file.LicenseInfoFromFiles.Count);
            var l2 = file.LicenseInfoFromFiles[0] as License;
            Assert.IsNotNull(l2);
            Assert.AreEqual("LGPL-2.0-only", l2.Id);

            Assert.AreEqual("Copyright 2010, 2011 Source Auditor Inc.", file.CopyrightText);
            Assert.AreEqual("File license comment", file.LicenseComments);

            Assert.AreEqual("Some file comment", file.Comment);

            Assert.AreEqual("File attribution text", file.AttributionText);
            Assert.AreEqual(0, file.Annotations.Count);
            Assert.AreEqual(0, file.RelationShips.Count);

            Assert.IsNotNull(spdxDoc.RelationShips);
            Assert.AreEqual(0, spdxDoc.RelationShips.Count);
        }

        [TestMethod]
        public void TestReadPackageSuccess()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""SPDXID"": ""SPDXRef-DOCUMENT"",
                ""packages"": [
                    {
                      ""filesAnalyzed"": true,
                      ""licenseInfoFromFiles"": [
                        ""GPL-2.0-only"",
                        ""EPL-1.0"",
                        ""CPL-1.0 WITH LZMA-exception""
                      ],
                      ""checksums"": [
                        {
                          ""algorithm"": ""MD5"",
                          ""checksumValue"": ""deadbeefdeadbeefdeadbeefdeadbeef""
                        },
                        {
                          ""algorithm"": ""MD5"",
                          ""checksumValue"": ""deadbeefdeadbeefdeadbeefdeadbeef""
                        }
                      ],
                      ""downloadLocation"": ""NONE"",
                      ""homepage"": ""http://ftp.gnu.org/gnu/glibc"",
                      ""originator"": ""Organization: ExampleCodeInspect (contact@example.com)"",
                      ""packageFileName"": ""glibc-2.11.1.tar.gz"",
                      ""packageVerificationCode"": {
                        ""packageVerificationCodeExcludedFiles"": [
                          ""./package.spdx""
                        ],
                        ""packageVerificationCodeValue"": ""d6a770ba38583ed4bb4525bd96e50461655d2758""
                      },
                      ""versionInfo"": ""99.88"",
                      ""externalRefs"": [
                        {
                          ""comment"": ""External ref comment"",
                          ""referenceCategory"": ""OTHER"",
                          ""referenceType"": ""[idstring]"",
                          ""referenceLocator"": ""cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*""
                        }
                      ],
                      ""builtDate"": ""2024-01-05T09:55:11Z"",
                      ""primaryPackagePurpose"": ""OPERATING-SYSTEM"",
                      ""validUntilDate"": ""2024-01-05T09:55:12Z"",
                      ""Files"": [],
                      ""licenseConcluded"": ""(LGPL-2.0-only OR MIT)"",
                      ""copyrightText"": ""Copyright Google"",
                      ""licenseComments"": ""Some license comment"",
                      ""attributionTexts"": [
                        ""some text""
                      ],
                      ""annotations"": [],
                      ""name"": ""AbrarJahin.DiffMatchPatch, 0.1.0"",
                      ""relationShips"": [],
                      ""SPDXID"": ""SPDXRef-Package-AbrarJahin.DiffMatchPatch""
                    }
                ],
                ""relationships"" : [
                  {
                    ""spdxElementId"" : ""SPDXRef-DOCUMENT"",
                    ""relationshipType"" : ""CONTAINS"",
                    ""relatedSpdxElement"" : ""SPDXRef-Package-AbrarJahin.DiffMatchPatch""
                  }
                ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual("SPDXRef-DOCUMENT", spdxDoc.SpdxIdentifier);

            var license = spdxDoc.DataLicense as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("CC0-1.0", license.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", license.Name);
            Assert.IsNotNull(license.LicenseText);

            Assert.IsNotNull(spdxDoc.RelationShips);
            Assert.AreEqual(1, spdxDoc.RelationShips.Count);
            Assert.AreEqual(RelationshipType.Contains, spdxDoc.RelationShips[0].Type);
            Assert.AreEqual("SPDXRef-Package-AbrarJahin.DiffMatchPatch", spdxDoc.RelationShips[0].RelatedElement.SpdxIdentifier);

            Assert.IsNotNull(spdxDoc.Packages);
            Assert.AreEqual(1, spdxDoc.Packages.Count);
            var package = spdxDoc.Packages[0];
            Assert.IsNotNull(package);
            Assert.AreEqual("SPDXRef-Package-AbrarJahin.DiffMatchPatch", package.SpdxIdentifier);
            Assert.AreEqual(true, package.IsFilesAnalyzed);

            Assert.AreEqual(3, package.LicenseInfoFromFiles.Count);
            license = package.LicenseInfoFromFiles[0] as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("GPL-2.0-only", license.Id);
            license = package.LicenseInfoFromFiles[1] as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("EPL-1.0", license.Id);
            var l2 = package.LicenseInfoFromFiles[2] as SimpleLicensingInfo;
            Assert.IsNotNull(l2);
            Assert.AreEqual("CPL-1.0 WITH LZMA-exception", l2.Id);

            Assert.AreEqual(2, package.Checksums.Count);
            Assert.AreEqual(ChecksumAlgorithm.MD5, package.Checksums[0].Algorithm);
            Assert.AreEqual("deadbeefdeadbeefdeadbeefdeadbeef", package.Checksums[0].Value);

            Assert.AreEqual("NONE", package.DownloadLocation);
            Assert.AreEqual("http://ftp.gnu.org/gnu/glibc", package.Homepage);
            Assert.AreEqual("Organization: ExampleCodeInspect (contact@example.com)", package.Originator);
            Assert.AreEqual("glibc-2.11.1.tar.gz", package.PackageFileName);
            Assert.AreEqual("d6a770ba38583ed4bb4525bd96e50461655d2758", package.PackageVerificationCode.Value);
            Assert.AreEqual(1, package.PackageVerificationCode.ExcludedFileNames.Count);
            Assert.AreEqual("./package.spdx", package.PackageVerificationCode.ExcludedFileNames[0]);
            Assert.AreEqual("99.88", package.VersionInfo);

            Assert.AreEqual(1, package.ExternalRefs.Count);
            var extref = package.ExternalRefs[0];
            Assert.AreEqual("External ref comment", extref.Comment);
            Assert.AreEqual(ReferenceCategory.Other, extref.ReferenceCategory);
            Assert.AreEqual("[idstring]", extref.ReferenceType);
            Assert.AreEqual("cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*", extref.ReferenceLocator);

            Assert.AreEqual(new DateTime(2024, 1, 5, 9, 55, 11), package.BuiltDate);
            Assert.AreEqual(PrimaryPackagePurpose.OperatingSystem, package.PrimaryPackagePurpose);
            Assert.AreEqual(new DateTime(2024, 1, 5, 9, 55, 12), package.ValidUntilDate);
            Assert.AreEqual(0, package.Files.Count);

            var l3 = package.LicenseConcluded as DisjunctiveLicenseSet;
            Assert.IsNotNull(l3);
            l2 = l3.LicenseInfos[0] as SimpleLicensingInfo;
            Assert.IsNotNull(l2);
            Assert.AreEqual("LGPL-2.0-only", l2.Id);
            l2 = l3.LicenseInfos[1] as SimpleLicensingInfo;
            Assert.IsNotNull(l2);
            Assert.AreEqual("MIT", l2.Id);

            Assert.AreEqual("Copyright Google", package.CopyrightText);
            Assert.AreEqual("Some license comment", package.LicenseComments);
            Assert.AreEqual("some text", package.AttributionText);
            Assert.AreEqual(0, package.Annotations.Count);
            Assert.AreEqual("AbrarJahin.DiffMatchPatch, 0.1.0", package.Name);
            Assert.AreEqual(0, package.RelationShips.Count);
        }

        [TestMethod]
        public void TestReadExtractedLicenseInfosSuccess()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""hasExtractedLicensingInfos"" : [
                  {
                    ""licenseId"" : ""LicenseRef-1"",
                    ""extractedText"" : ""(c) Copyright 2000, 2001, HP""
                  },
                  {
                    ""licenseId"" : ""LicenseRef-Beerware-4.2"",
                    ""comment"" : ""The beerware license has..."",
                    ""extractedText"" : ""\""THE BEER-WARE LICENSE\"" (Revision 42)..."",
                    ""name"" : ""Beer-Ware License (Version 42)"",
                    ""seeAlsos"" : [ ""http://people.freebsd.org/~phk/"" ]
                  }
                ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);

            var license = spdxDoc.DataLicense as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("CC0-1.0", license.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", license.Name);
            Assert.IsNotNull(license.LicenseText);

            Assert.AreEqual(2, spdxDoc.ExtractedLicenseInfos.Count);
            Assert.AreEqual("LicenseRef-1", spdxDoc.ExtractedLicenseInfos[0].Id);
            Assert.AreEqual("(c) Copyright 2000, 2001, HP", spdxDoc.ExtractedLicenseInfos[0].ExtractedText);

            Assert.AreEqual("LicenseRef-Beerware-4.2", spdxDoc.ExtractedLicenseInfos[1].Id);
            Assert.AreEqual("\"THE BEER-WARE LICENSE\" (Revision 42)...", spdxDoc.ExtractedLicenseInfos[1].ExtractedText);
            Assert.AreEqual("The beerware license has...", spdxDoc.ExtractedLicenseInfos[1].Comment);
            Assert.AreEqual("Beer-Ware License (Version 42)", spdxDoc.ExtractedLicenseInfos[1].Name);
            Assert.AreEqual("LicenseRef-Beerware-4.2", spdxDoc.ExtractedLicenseInfos[1].Id);
            Assert.AreEqual(1, spdxDoc.ExtractedLicenseInfos[1].SeeAlso.Count);
            Assert.AreEqual("http://people.freebsd.org/~phk/", spdxDoc.ExtractedLicenseInfos[1].SeeAlso[0]);
        }

        [TestMethod]
        public void TestReadFullSpdxDocumentSuccess()
        {
            const string JsonFileFullTest = @"..\..\..\..\Testdata\fulltest.spdx.json";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromFile(JsonFileFullTest);
            Assert.IsNotNull(spdxDoc);
            Assert.IsNotNull(spdxDoc.CreationInfo);
        }

        [TestMethod]
        public void TestReadSpdxExampleFileSuccess()
        {
            // Official SPDX JSON example, take from
            // https://github.com/spdx/spdx-spec/blob/development/v2.3.1/examples/SPDXJSONExample-v2.3.spdx.json
            const string SpdxExampleFile = @"..\..\..\..\Testdata\SPDXJSONExample-v2.3.spdx.json";

            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromFile(SpdxExampleFile);
            Assert.IsNotNull(spdxDoc);
            Assert.IsNotNull(spdxDoc.CreationInfo);

            Assert.AreEqual(1, spdxDoc.ExternalDocumentRefs.Count);

            Assert.AreEqual(3, spdxDoc.Annotations.Count);

            Assert.AreEqual(4, spdxDoc.Packages.Count);
            Assert.AreEqual(5, spdxDoc.Files.Count);
            Assert.AreEqual(1, spdxDoc.Snippets.Count);

            Assert.AreEqual(7, spdxDoc.RelationShips.Count);

            Assert.AreEqual(2, spdxDoc.DocumentDescribes.Count);
            Assert.AreEqual("SPDXRef-File", spdxDoc.DocumentDescribes[0]);
            Assert.AreEqual("SPDXRef-Package", spdxDoc.DocumentDescribes[1]);

            Assert.AreEqual(5, spdxDoc.ExtractedLicenseInfos.Count);
        }

        [TestMethod]
        public void TestReadFossologyResult1Success()
        {
            const string JsonFile = @"..\..\..\..\Testdata\SPDX2_Gaurav.spdx.json";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromFile(JsonFile);
            Assert.IsNotNull(spdxDoc);
            Assert.IsNotNull(spdxDoc.CreationInfo);

            Assert.AreEqual(2263, spdxDoc.Files.Count);
            Assert.AreEqual(1, spdxDoc.Packages.Count);
            Assert.AreEqual(2264, spdxDoc.RelationShips.Count);
            Assert.AreEqual(0, spdxDoc.Annotations.Count);
            Assert.AreEqual(0, spdxDoc.ExternalDocumentRefs.Count);
            Assert.AreEqual(8, spdxDoc.ExtractedLicenseInfos.Count);

            Assert.IsNotNull(spdxDoc.DataLicense);
            var dataLicense = spdxDoc.DataLicense as License;
            Assert.IsNotNull(dataLicense);
            Assert.AreEqual("CC0-1.0", dataLicense.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", dataLicense.Name);

            var licenseInfo = spdxDoc.ExtractedLicenseInfos[2];
            Assert.AreEqual("BSD-3-Clause GMishx", licenseInfo.Name);
            Assert.AreEqual("BSD-3-Clause", licenseInfo.Id);
            Assert.IsTrue(licenseInfo.ExtractedText.Contains("License by Nomos."));

            licenseInfo = spdxDoc.ExtractedLicenseInfos[3];
            Assert.AreEqual("BSD-3-Clause GMishx", licenseInfo.Name);
            Assert.AreEqual("BSD-3-Clause", licenseInfo.Id);
            Assert.IsTrue(licenseInfo.ExtractedText.Contains("Gaurav Mishra"));
        }

        [TestMethod]
        public void TestReadXSuccess()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0""
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);

            var license = spdxDoc.DataLicense as License;
            Assert.IsNotNull(license);
            Assert.AreEqual("CC0-1.0", license.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", license.Name);
            Assert.IsNotNull(license.LicenseText);
        }
    }
}
