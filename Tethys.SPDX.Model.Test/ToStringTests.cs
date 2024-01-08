// ---------------------------------------------------------------------------
// <copyright file="ToStringTests.cs" company="Tethys">
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
    using Tethys.SPDX.Model.License;
    using Tethys.SPDX.Model.Pointer;

    [TestClass]
    public class ToStringTests
    {
        [TestMethod]
        public void TestToStringMethods()
        {
            var nl = new SpdxNoneLicense();
            var actual = nl.ToString();
            Assert.AreEqual("NONE", actual);

            var nal = new SpdxNoAssertionLicense();
            actual = nal.ToString();
            Assert.AreEqual("NOASSERTION", actual);

            var bo = new ByteOffsetPointer();
            bo.Offset = 999;
            actual = bo.ToString();
            Assert.AreEqual("Byte offset 999", actual);

            var lc = new LineCharPointer();
            lc.LineNumber = 1234;
            actual = lc.ToString();
            Assert.AreEqual("Line number 1234", actual);

            var sep = new StartEndPointer();
            actual = sep.ToString();
            Assert.AreEqual("From: [UNKNOWN] To: [UNKNOWN]", actual);

            sep.StartPointer = bo;
            actual = sep.ToString();
            Assert.AreEqual("From: Byte offset 999 To: [UNKNOWN]", actual);

            sep.EndPointer = lc;
            actual = sep.ToString();
            Assert.AreEqual("From: Byte offset 999 To: Line number 1234", actual);

            var l = new SimpleLicensingInfo();
            l.Id = "GPL-2.0";
            var ol = new OrLaterOperator(l);
            actual = ol.ToString();
            Assert.AreEqual("GPL-2.0+", actual);

            var cs = new Checksum();
            cs.Value = "deadbeef";
            cs.Algorithm = ChecksumAlgorithm.BLAKE2b_512;
            actual = cs.ToString();
            Assert.AreEqual("Alg = BLAKE2b_512: deadbeef", actual);

            var ll = new License();
            actual = ll.ToString();
            Assert.AreEqual("NULL LICENSE", actual);
            ll.Id = "CPL-1.0";
            actual = ll.ToString();
            Assert.AreEqual("CPL-1.0", actual);

            var cl = new ConjunctiveLicenseSet(new[]
            {
                new License { Id = "MIT" },
                new License { Id = "ISC" },
            });
            actual = cl.ToString();
            Assert.AreEqual("(MIT AND ISC)", actual);
            cl.AddMember(new License { Id = "CPL-1.0" });
            actual = cl.ToString();
            Assert.AreEqual("(MIT AND ISC AND CPL-1.0)", actual);

            var dl = new DisjunctiveLicenseSet(new[]
            {
                new License { Id = "BSD-3-Clause" },
                new License { Id = "GPL-2.0" },
            });
            actual = dl.ToString();
            Assert.AreEqual("(BSD-3-Clause OR GPL-2.0)", actual);
            dl.SetMembers(new[]
            {
                new License { Id = "MIT" },
                new License { Id = "ISC" },
            });
            actual = dl.ToString();
            Assert.AreEqual("(MIT OR ISC)", actual);

            var sl = new SimpleLicensingInfo();
            sl.Name = "Some name";
            sl.Id = "LicenseRef-XXX";
            sl.Comment = "Just a test";
            actual = sl.ToString();
            Assert.AreEqual("Some name (LicenseRef-XXX), Just a test", actual);

            var file = new SpdxFile();
            file.Name = "something.c";
            file.SpdxIdentifier = "SPDXRef-1";
            file.LicenseConcluded = dl;
            actual = file.ToString();
            Assert.AreEqual("something.c, SPDXRef-1, (MIT OR ISC)", actual);

            file = new SpdxFile();
            file.Name = "something.c";
            file.SpdxIdentifier = "SPDXRef-1";
            file.AddLicenseInfoFromFile(cl);
            actual = file.ToString();
            Assert.AreEqual("something.c, SPDXRef-1, (MIT AND ISC AND CPL-1.0)", actual);

            var rdf = new RdfBaseItem();
            rdf.Id = "MyId";
            rdf.NodeId = "MyNodeId";
            rdf.Language = "SomeLanguage";
            rdf.DataType = "xxx";
            rdf.About = "SomeAbout";
            rdf.Resource = "Res";
            actual = rdf.ToString();
            Assert.AreEqual("ID=MyId, NodeId=MyNodeId, lang=SomeLanguage, Type=xxx, About=SomeAbout, Resource=Res", actual);
        }
    }
}