// ---------------------------------------------------------------------------
// <copyright file="Program.cs" company="Tethys">
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

namespace SpdxWriterDemo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Tethys.SPDX.KnownLicenses;
    using Tethys.SPDX.Model;
    using Tethys.SPDX.Model.License;
    using Tethys.SPDX.Model.Pointer;
    using Tethys.SPDX.Writer;

    /// <summary>
    /// Main class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The expected data path for the SPDX license data.
        /// </summary>
        private const string ExpectedDataPath = @"..\..\..\..\license-list-data";

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Simple demo for SPDX file writing.");

            try
            {
                var knownLicenseManager = GetKnownLicenseManager();

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
                package.LicenseConcluded = new DisjunctiveLicenseSet(new List<AnyLicenseInfo>{ l1, license});

                package.AddLicenseInfoFromFile(new ListedLicenseInfo { Id = "GPL-2.0-only" });
                package.AddLicenseInfoFromFile(new ListedLicenseInfo { Id = "EPL-1.0" });
                package.AddLicenseInfoFromFile(new ListedLicenseInfo { Id = "CPL-1.0 WITH LZMA-exception" });

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

                var writer = new JsonWriter();
                writer.WriteToFile(spdxDoc, "test.spdx.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing SPDX file: " + ex.Message);
            } // catch
        } // Main()

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
    }
}

