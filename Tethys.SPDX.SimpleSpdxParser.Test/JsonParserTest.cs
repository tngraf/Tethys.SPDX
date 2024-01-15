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
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using Tethys.SPDX.ExpressionParser;
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

            Assert.IsNotNull(spdxDoc.CreationInfo.CreatedDate);
            Assert.AreEqual(2024, spdxDoc.CreationInfo.CreatedDate?.Year);
            Assert.AreEqual(01, spdxDoc.CreationInfo.CreatedDate?.Month);
            Assert.AreEqual(05, spdxDoc.CreationInfo.CreatedDate?.Day);
            Assert.AreEqual(09, spdxDoc.CreationInfo.CreatedDate?.Hour);
            Assert.AreEqual(55, spdxDoc.CreationInfo.CreatedDate?.Minute);
            Assert.AreEqual(11, spdxDoc.CreationInfo.CreatedDate?.Second);

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
                      ""copyrightText"": ""Copyright 2008-2010 John Smith"",
                      ""licenseComments"": ""The concluded license was taken from package xyz, from which the snippet was copied into the current file. The concluded license information was found in the COPYING.txt file in package xyz."",
                      ""name"": ""from linux kernel"",
                      ""comment"": ""This snippet was identified as significant and highlighted in this Apache-2.0 file, when a commercial scanner identified it as being derived from file foo.c in package xyz which is licensed under GPL-2.0."",
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
            Assert.IsNull(snippet.LicenseInfoFromFiles);
            Assert.AreEqual("Copyright 2008-2010 John Smith", snippet.CopyrightText);
            Assert.IsTrue(snippet.Comment.StartsWith("This snippet was identified as significant"));
            Assert.IsTrue(snippet.LicenseComments.StartsWith("The concluded license was taken from package xyz"));
            Assert.IsNull(snippet.Annotations);
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

            Assert.IsNull(spdxDoc.RelationShips);
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
                      ""comment"": ""Some file comment"",
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
            Assert.IsNull(file.Annotations);
            Assert.IsNull(file.RelationShips);

            Assert.IsNull(spdxDoc.RelationShips);
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
            Assert.IsNull(package.RelationShips);
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
        public void TestReadExternalDocumentRefsSuccess()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""SPDXID"": ""SPDXRef-DOCUMENT"",
                ""externalDocumentRefs"": [
                    {
                      ""externalDocumentId"": ""1"",
                      ""spdxDocument"": ""NONE"",
                    },
                    {
                      ""externalDocumentId"": ""2"",
                      ""spdxDocument"": ""NOASSERTION""
                    }
                  ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual("SPDXRef-DOCUMENT", spdxDoc.SpdxIdentifier);

            Assert.IsNotNull(spdxDoc.ExternalDocumentRefs);
            Assert.AreEqual(2, spdxDoc.ExternalDocumentRefs.Count);
            Assert.AreEqual("1", spdxDoc.ExternalDocumentRefs[0].ExternalDocumentId);
            Assert.AreEqual(null, spdxDoc.ExternalDocumentRefs[0].SpdxDocument);

            Assert.AreEqual("2", spdxDoc.ExternalDocumentRefs[1].ExternalDocumentId);
            Assert.AreEqual("NOASSERTION", spdxDoc.ExternalDocumentRefs[1].SpdxDocument.SpdxIdentifier);
        }

        [TestMethod]
        public void TestReadNoneLicense()
        {
            const string Json = @"{
                ""dataLicense"": ""NONE""
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);

            var license = spdxDoc.DataLicense as SpdxNoneLicense;
            Assert.IsNotNull(license);
        }

        [TestMethod]
        public void TestReadNoAssertionLicense()
        {
            const string Json = @"{
                ""dataLicense"": ""NOASSERTION""
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);

            var license = spdxDoc.DataLicense as SpdxNoAssertionLicense;
            Assert.IsNotNull(license);
        }

        [TestMethod]
        public void TestReadConjunctiveLicenseSetLicense()
        {
            const string Json = @"{
                ""dataLicense"": ""MIT AND GPL-3.0""
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.IsNotNull(spdxDoc);

            var license = spdxDoc.DataLicense as ConjunctiveLicenseSet;
            Assert.IsNotNull(license);
            Assert.AreEqual(2, license.LicenseInfos.Count);
            var l1 = license.LicenseInfos[0] as SimpleLicensingInfo;
            Assert.IsNotNull(l1);
            Assert.AreEqual("MIT", l1.Id);

            l1 = license.LicenseInfos[1] as SimpleLicensingInfo;
            Assert.IsNotNull(l1);
            Assert.AreEqual("GPL-3.0", l1.Id);
        }

        [TestMethod]
        public void TestReadSpdxElementRef()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""files"" : [
                ],
                ""relationships"" : [
                  {
                    ""spdxElementId"" : ""SPDXRef-DOCUMENT"",
                    ""relationshipType"" : ""CONTAINS"",
                    ""relatedSpdxElement"" : ""NONE""
                  }
                ],
                ""snippets"" : [
                  {
                    ""snippetFromFile"" : ""NOASSERTION""
                  }
                ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);

            Assert.IsNotNull(spdxDoc);
            Assert.AreEqual(1, spdxDoc.RelationShips.Count);
            Assert.AreEqual(null, spdxDoc.RelationShips[0].RelatedElement);

            Assert.AreEqual(1, spdxDoc.Snippets.Count);
            Assert.AreEqual("NOASSERTION", spdxDoc.Snippets[0].SnippetFromFile.SpdxIdentifier);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void TestReadLineCharPointerInvalidValue()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""snippets"" : [
                  {
                    ""snippetFromFile"" : ""NOASSERTION"",
                    ""ranges"" : [
                        {
                          ""endPointer"" : {
                            ""lineNumber"" : ""ABC"",
                            ""reference"" : ""SPDXRef-DoapSource""
                          },
                          ""startPointer"" : {
                            ""offset"" : 310,
                            ""reference"" : ""SPDXRef-DoapSource""
                          }
                       }
                    ]
                  }
                ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            reader.ReadFromString(Json);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void TestReadLineCharPointerNullValue()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""snippets"" : [
                  {
                    ""snippetFromFile"" : ""NOASSERTION"",
                    ""ranges"" : [
                        {
                          ""endPointer"" : {
                            ""lineNumber"" : null,
                            ""reference"" : ""SPDXRef-DoapSource""
                          },
                          ""startPointer"" : {
                            ""offset"" : 310,
                            ""reference"" : ""SPDXRef-DoapSource""
                          }
                       }
                    ]
                  }
                ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            reader.ReadFromString(Json);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void TestReadByteOffsetPointerInvalidValue()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""snippets"" : [
                  {
                    ""snippetFromFile"" : ""NOASSERTION"",
                    ""ranges"" : [
                        {
                          ""endPointer"" : {
                            ""offset"" : ""ABC"",
                            ""reference"" : ""SPDXRef-DoapSource""
                          },
                          ""startPointer"" : {
                            ""offset"" : 310,
                            ""reference"" : ""SPDXRef-DoapSource""
                          }
                       }
                    ]
                  }
                ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            reader.ReadFromString(Json);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void TestReadByteOffsetPointerNullValue()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""snippets"" : [
                  {
                    ""snippetFromFile"" : ""NOASSERTION"",
                    ""ranges"" : [
                        {
                          ""endPointer"" : {
                            ""offset"" : null,
                            ""reference"" : ""SPDXRef-DoapSource""
                          },
                          ""startPointer"" : {
                            ""offset"" : 310,
                            ""reference"" : ""SPDXRef-DoapSource""
                          }
                       }
                    ]
                  }
                ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            reader.ReadFromString(Json);
        }

        [TestMethod]
        [ExpectedException(typeof(SpdxExpressionException))]
        public void TestReadInvalidLicenseFail()
        {
            const string Json = @"{
                ""dataLicense"": ""XXX""

            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            reader.ReadFromString(Json);
        }

        [TestMethod]
        public void TestReadExtractedLicenseWithSeeAlso()
        {
            const string Json = @"{
                ""dataLicense"": ""CC0-1.0"",
                ""hasExtractedLicensingInfos"" : [
                 {
                    ""licenseId"" : ""LicenseRef-Beerware-4.2"",
                    ""comment"" : ""The beerware license has a couple of other standard variants."",
                    ""extractedText"" : ""\""THE BEER-WARE LICENSE\"" (Revision 42)..."",
                    ""name"" : ""Beer-Ware License (Version 42)"",
                    ""seeAlsos"" : [ ""http://people.freebsd.org/~phk/"" ]
                 }
               ]
            }";
            var knownLicenseManager = GetKnownLicenseManager();
            var reader = new JsonParser(knownLicenseManager);
            var spdxDoc = reader.ReadFromString(Json);
            Assert.AreEqual(1, spdxDoc.ExtractedLicenseInfos.Count);
            Assert.AreEqual(1, spdxDoc.ExtractedLicenseInfos[0].SeeAlso.Count);
            Assert.AreEqual("http://people.freebsd.org/~phk/", spdxDoc.ExtractedLicenseInfos[0].SeeAlso[0]);
        }

        [TestMethod]
        public void TestWriteLicenses()
        {
            const string JsonFile = "licenses.spdx.json";

            // step 1 - create and write SPDX JSON file
            var spdxDoc = new SpdxDocument();
            spdxDoc.SpdxIdentifier = "SPDXRef-DOCUMENT";
            spdxDoc.SpecVersion = "SPDX-2.3";

            spdxDoc.CreationInfo = new SpdxCreatorInformation();
            spdxDoc.CreationInfo.CreatedDate = DateTime.Now;

            spdxDoc.Name = "SPDX SBOM for FOSS disclosure";
            spdxDoc.DataLicense = new ConjunctiveLicenseSet(new[]
            {
                new License { Id = "MIT" },
                new License { Id = "0BSD" },
            });

            var file = new SpdxFile();
            file.FileName = "1";
            file.LicenseConcluded = new SpdxNoneLicense();
            spdxDoc.AddFile(file);

            file = new SpdxFile();
            file.FileName = "2";
            file.LicenseConcluded = new SpdxNoAssertionLicense();
            spdxDoc.AddFile(file);

            var extref = new ExternalDocumentRef();
            extref.SpdxDocument = new SpdxDocument { SpdxIdentifier = "NONE" };
            spdxDoc.AddExternalDocumentRef(extref);

            extref = new ExternalDocumentRef();
            extref.SpdxDocument = new SpdxDocument { SpdxIdentifier = "NOASSERTION" };
            spdxDoc.AddExternalDocumentRef(extref);

            var writer = new Writer.JsonWriter();
            writer.WriteToFile(spdxDoc, JsonFile);
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
            Assert.IsNull(spdxDoc.Annotations);
            Assert.IsNull(spdxDoc.ExternalDocumentRefs);
            Assert.IsNotNull(spdxDoc.ExtractedLicenseInfos);
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
        public void TestWriteReadSuccess()
        {
            const string JsonFile = "test_rw.spdx.json";

            var knownLicenseManager = GetKnownLicenseManager();

            // step 1 - create and write SPDX JSON file
            var spdxDoc = new SpdxDocument();
            spdxDoc.SpdxIdentifier = "SPDXRef-DOCUMENT";
            spdxDoc.SpecVersion = "SPDX-2.3";

            spdxDoc.CreationInfo = new SpdxCreatorInformation();
            spdxDoc.CreationInfo.CreatedDate = DateTime.Now;
            // SPDX 2.3 requires a prefix like "Person", "Organization", or "Tool"
            spdxDoc.CreationInfo.AddCreator("Organization: Siemens");
            spdxDoc.CreationInfo.AddCreator("Tool: Software Clearing Workbench");
            spdxDoc.CreationInfo.AddCreator("Person: Thomas Graf");
            spdxDoc.CreationInfo.LicenseListVersion = knownLicenseManager.LicenseListVersion;

            spdxDoc.Name = "SPDX SBOM for FOSS disclosure";
            spdxDoc.DataLicense = new ListedLicenseInfo
            {
                Id = "CC0-1.0",
            };

            spdxDoc.SpdxDocumentNamespace = "http://spdx.org/spdxdocs/spdx-example-007";

            spdxDoc.AddAnnotation(new Annotation
            {
                Date = DateTime.Now,
                AnnotationType = AnnotationType.Other,
                Annotator = "Person: Jane Doe",
                Comment = "Document level annotation",
            });
            spdxDoc.AddAnnotation(new Annotation
            {
                Date = DateTime.Now,
                AnnotationType = AnnotationType.Review,
                // SPDX 2.3 requires a prefix like "Person", "Organization", or "Tool"
                Annotator = "Person: Thomas Graf",
                Comment = "Another annotation",
            });

            spdxDoc.AddExternalDocumentRef(new ExternalDocumentRef
            {
                ExternalDocumentId = "DocumentRef-spdx-tool-1.2",
                SpdxDocument = new SpdxDocument
                {
                    SpdxDocumentNamespace = "http://spdx.org/spdxdocs/spdx-tools-v1.2-3F2504E0-4F89-41D3-9A0C-0305E82C3301",
                },
                Checksum = new Checksum
                {
                    Algorithm = ChecksumAlgorithm.SHA1,
                    Value = "d6a770ba38583ed4bb4525bd96e50461655d2759",
                },
            });

            var package = new SpdxPackage();
            package.SpdxIdentifier = "SPDXRef-Package-AbrarJahin.DiffMatchPatch";
            package.Name = "AbrarJahin.DiffMatchPatch, 0.1.0";
            package.DownloadLocation = "NONE";
            package.CopyrightText = "Copyright Google";
            package.AttributionText = "some text";
            package.VersionInfo = "99.88";
            package.ValidUntilDate = DateTime.Now;

            package.AddChecksum(new Checksum
            {
                Algorithm = ChecksumAlgorithm.MD5,
                Value = "deadbeefdeadbeefdeadbeefdeadbeef",
            });
            package.AddChecksum(new Checksum
            {
                Algorithm = ChecksumAlgorithm.MD5,
                Value = "deadbeefdeadbeefdeadbeefdeadbeef",
            });

            package.BuiltDate = DateTime.Now;

            package.AddExternalRef(new ExternalRef
            {
                Comment = "External ref comment",
                ReferenceCategory = ReferenceCategory.Other,
                ReferenceLocator = "cpe:2.3:a:pivotal_software:spring_framework:4.1.0:*:*:*:*:*:*:*",
                ReferenceType = ReferenceType.IdString.ToString(),
            });

            package.Homepage = "http://ftp.gnu.org/gnu/glibc";
            package.LicenseComments = "Some license comment";
            package.Originator = "Organization: ExampleCodeInspect (contact@example.com)";
            package.PackageFileName = "glibc-2.11.1.tar.gz";

            package.IsFilesAnalyzed = true;

            // PackageVerificationCode is only allowed when IsFilesAnalyzed == true
            package.PackageVerificationCode = new SpdxPackageVerificationCode
            {
                Value = "d6a770ba38583ed4bb4525bd96e50461655d2758",
            };
            package.PackageVerificationCode.AddExcludedFileName("./package.spdx");
            package.PrimaryPackagePurpose = PrimaryPackagePurpose.OperatingSystem;

            var l1 = new ListedLicenseInfo();
            l1.Id = "LGPL-2.0-only";

            var license = new ListedLicenseInfo();
            license.Id = "MIT";
            package.LicenseConcluded = new DisjunctiveLicenseSet(new List<AnyLicenseInfo> { l1, license });

            package.AddLicenseInfoFromFile(new ListedLicenseInfo { Id = "GPL-2.0-only" });
            package.AddLicenseInfoFromFile(new ListedLicenseInfo { Id = "EPL-1.0" });
            package.AddLicenseInfoFromFile(new ListedLicenseInfo { Id = "CPL-1.0 WITH LZMA-exception" });
            package.AddLicenseInfoFromFile(new SpdxNoneLicense());
            package.AddLicenseInfoFromFile(new SpdxNoAssertionLicense());
            package.AddLicenseInfoFromFile(new ConjunctiveLicenseSet(new []
            {
                new License() { Id = "MIT" },
                new License() { Id = "AGPL-3.0" },
            }));

            spdxDoc.AddPackage(package);
            spdxDoc.AddRelationShip(new RelationShip
            {
                Type = RelationshipType.Describes,
                RelatedElement = package,
            });
            spdxDoc.AddDocumentDescribes(package.SpdxIdentifier);

            var file = new SpdxFile();
            file.SpdxIdentifier = "SPDXRef-DoapSource";
            file.AddChecksum(new Checksum
            {
                Algorithm = ChecksumAlgorithm.SHA1,
                Value = "0123456789012345678901234567890123456789",
            });
            file.CopyrightText = "Copyright 2010, 2011 Source Auditor Inc.";
            file.AddFileContributor("Protecode Inc.");
            file.AddFileContributor("Open Logic Inc.");
            file.FileName = "./src/org/spdx/parser/DOAPProject.java";
            file.AddFileType(FileType.Source);
            file.AddFileType(FileType.Archive);
            file.LicenseComments = "File license comment";
            file.LicenseConcluded = license;
            file.AddLicenseInfoFromFile(l1);
            file.NoticeText = "A notice for this file.";
            file.Comment = "Some file comment";
            file.AttributionText = "File attribution text";

            spdxDoc.AddFile(file);
            spdxDoc.AddRelationShip(new RelationShip
            {
                Type = RelationshipType.Contains,
                RelatedElement = file,
            });

            var snippet = new SpdxSnippet();
            snippet.SpdxIdentifier = "SPDXRef-Snippet";
            snippet.SnippetFromFile = file;
            snippet.Comment =
                "This snippet was identified as significant and highlighted in this Apache-2.0 file, when a commercial scanner identified it as being derived from file foo.c in package xyz which is licensed under GPL-2.0.";
            snippet.CopyrightText = "Copyright 2008-2010 John Smith";
            snippet.LicenseComments =
                "The concluded license was taken from package xyz, from which the snippet was copied into the current file. The concluded license information was found in the COPYING.txt file in package xyz.";
            snippet.LicenseConcluded = license;
            snippet.AddLicenseInfoInSnippet(l1);
            snippet.Name = "from linux kernel";
            snippet.AttributionText = "";
            // you always have to provide line AND binary ranges!
            snippet.AddRange(new StartEndPointer
            {
                StartPointer = new LineCharPointer
                {
                    LineNumber = 5,
                    Reference = file,
                },
                EndPointer = new LineCharPointer
                {
                    LineNumber = 23,
                    Reference = file,
                },
            });
            snippet.AddRange(new StartEndPointer
            {
                StartPointer = new ByteOffsetPointer
                {
                    Offset = 310,
                    Reference = file,
                },
                EndPointer = new ByteOffsetPointer
                {
                    Offset = 420,
                    Reference = file,
                },
            });

            spdxDoc.AddSnippet(snippet);

            var writer = new Writer.JsonWriter();
            writer.WriteToFile(spdxDoc, JsonFile);

            // step 2 - read the created SPDX JSON file
            var reader = new JsonParser(knownLicenseManager);
            var spdxDocRead = reader.ReadFromFile(JsonFile);
            Assert.IsNotNull(spdxDocRead);

            var lr = spdxDocRead.DataLicense as License;
            Assert.IsNotNull(lr);
            Assert.AreEqual("CC0-1.0", lr.Id);
            Assert.AreEqual("Creative Commons Zero v1.0 Universal", lr.Name);
            Assert.IsNotNull(lr.LicenseText);

            Assert.IsNotNull(spdxDocRead.CreationInfo);

            Assert.AreEqual(1, spdxDocRead.Files.Count);
            Assert.AreEqual(1, spdxDocRead.Packages.Count);
            Assert.AreEqual(2, spdxDocRead.RelationShips.Count);
            Assert.AreEqual(2, spdxDocRead.Annotations.Count);
            Assert.AreEqual(1, spdxDocRead.ExternalDocumentRefs.Count);
            Assert.IsNull(spdxDocRead.ExtractedLicenseInfos);
        }
    }
}
