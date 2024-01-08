// ---------------------------------------------------------------------------
// <copyright file="SpdxDocumentTests.cs" company="Tethys">
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

namespace Tethys.SPDX.Model.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SpdxDocumentTests
    {
        [TestMethod]
        public void TestPackageMethods()
        {
            var spdxDoc = new SpdxDocument();
            Assert.AreEqual(0, spdxDoc.Packages.Count);
            var package = new SpdxPackage();
            spdxDoc.AddPackage(package);
            Assert.AreEqual(1, spdxDoc.Packages.Count);
            spdxDoc.ClearPackages();
            Assert.AreEqual(0, spdxDoc.Packages.Count);
        }

        [TestMethod]
        public void TestFileMethods()
        {
            var spdxDoc = new SpdxDocument();
            Assert.AreEqual(0, spdxDoc.Files.Count);
            var file = new SpdxFile();
            spdxDoc.AddFile(file);
            Assert.AreEqual(1, spdxDoc.Files.Count);
            spdxDoc.ClearFiles();
            Assert.AreEqual(0, spdxDoc.Files.Count);
        }

        [TestMethod]
        public void TestSnippetMethods()
        {
            var spdxDoc = new SpdxDocument();
            Assert.AreEqual(0, spdxDoc.Snippets.Count);
            var snippet = new SpdxSnippet();
            spdxDoc.AddSnippet(snippet);
            Assert.AreEqual(1, spdxDoc.Snippets.Count);
            spdxDoc.ClearSnippets();
            Assert.AreEqual(0, spdxDoc.Snippets.Count);
        }

        [TestMethod]
        public void TestDocumentDescribesMethods()
        {
            var spdxDoc = new SpdxDocument();
            Assert.AreEqual(0, spdxDoc.DocumentDescribes.Count);
            spdxDoc.AddDocumentDescribes("something");
            Assert.AreEqual(1, spdxDoc.DocumentDescribes.Count);
            spdxDoc.ClearDocumentDescribes();
            Assert.AreEqual(0, spdxDoc.DocumentDescribes.Count);
        }
    }
}